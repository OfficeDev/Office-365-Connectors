using BillsListASPNET.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BillsListASPNET.Controllers
{
    [Authorize]
    public class ItemsController : Controller
    {
        public static List<string> categories = new List<string>() {"antiques", "appliances", "arts & crafts",
            "atvs, utvs, snowmobiles", "auto parts", "baby & kid stuff", "barter", "bicycles", "boats", "books & magazines",
            "business/commercial", "cars & trucks", "cds, dvds, vhs", "cell phones", "clothing & accessories",
            "collectables", "computers", "electronics", "farm & garden", "free stuff", "furniture" ,"garage & moving sales",
            "general for sale", "health & beauty", "heavy equipment", "household items", "jewelry", "materials",
            "motorcycles/scooters", "musical instruments", "photo/video", "rvs", "sporting goods", "tickets",
            "tools", "toys & games", "video gaming", "wanted" };

        public ActionResult Index()
        {
            using (BillsListEntities entities = new BillsListEntities())
            {
                return View(entities.Items.OrderByDescending(i => i.PostDate).ToList());
            }
        }

        [Route("items/detail/{id}")]
        public ActionResult Detail(int id)
        {
            using (BillsListEntities entities = new BillsListEntities())
            {
                return View(entities.Items.FirstOrDefault(i => i.Id == id));
            }
        }

        [Route("items/category")]
        public ActionResult Category(string c)
        {
            ViewData.Add("category", c);
            using (BillsListEntities entities = new BillsListEntities())
            {
                return View(entities.Items.Where(i => i.Category == c).OrderByDescending(i => i.PostDate).ToList());
            }
        }

        [Route("items/my")]
        public ActionResult My()
        {
            var user = System.Security.Claims.ClaimsPrincipal.Current.FindFirst("preferred_username").Value.ToLower();
            using (BillsListEntities entities = new BillsListEntities())
            {
                return View(entities.Items.Where(i => i.Owner == user).OrderByDescending(i => i.PostDate).ToList());
            }
        }

        [Route("items/create")]
        [HttpGet]
        public ActionResult Create()
        {
            ViewData.Add("categories", categories);
            return View();
        }

        [Route("items/create")]
        [HttpPost]
        public async Task<ActionResult> Create(Item item)
        {
            //convert the image to a base64 string
            if (Request.Files.Count > 0)
            {
                byte[] bytes = new byte[Request.Files[0].InputStream.Length];
                Request.Files[0].InputStream.Read(bytes, 0, bytes.Length);
                item.Image = "data:image/jpg;base64, " + Convert.ToBase64String(bytes);
            }

            //set additional properties
            item.Owner = System.Security.Claims.ClaimsPrincipal.Current.FindFirst("preferred_username").Value.ToLower();
            item.PostDate = DateTime.Now;

            //save the item to the database
            using (BillsListEntities entities = new BillsListEntities())
            {
                entities.Items.Add(item);
                var id = entities.SaveChanges();

                //loop through subscriptions and call webhooks for each
                foreach (var sub in entities.Subscriptions)
                {
                    await callWebhook(sub.WebHookUri, item);
                }

                return RedirectToAction("Detail", new { id = item.Id });
            }
        }

        [Route("items/delete/{id}")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var user = System.Security.Claims.ClaimsPrincipal.Current.FindFirst("preferred_username").Value.ToLower();
            using (BillsListEntities entities = new BillsListEntities())
            {
                var item = entities.Items.FirstOrDefault(i => i.Id == id && i.Owner == user);
                if (item != null)
                {
                    entities.Items.Remove(item);
                    entities.SaveChanges();
                }
                return RedirectToAction("My");
            }
        }
        
        private async Task callWebhook(string webhook, Item item)
        {
            var imgString = "https://billslist.azurewebsites.net/images/logo_40.png";
            if (Request.Files.Count > 0)
            {
                //resize the image
                Request.Files[0].InputStream.Position = 0;
                Image img = Image.FromStream(Request.Files[0].InputStream);
                var newImg = (Image)(new Bitmap(img, new Size(40, 40)));

                //convert the stream
                using (var stream = new System.IO.MemoryStream())
                {
                    newImg.Save(stream, ImageFormat.Jpeg);
                    stream.Position = 0;
                    var bytes = new byte[stream.Length];
                    stream.Read(bytes, 0, bytes.Length);
                    imgString = "data:image/jpg;base64, " + Convert.ToBase64String(bytes);
                }
            }

            //prepare the json payload
            var json = @"
                {
                    'summary': 'A new listing was posted to BillsList',
                    'sections': [
                        {
                            'activityTitle': 'New BillsList listing',
                            'activitySubtitle': '" + item.Title + @"',
                            'activityImage': '" + imgString + @"',
                            'facts': [
                                {
                                    'name': 'Category',
                                    'value': '" + item.Category + @"'
                                },
                                {
                                    'name': 'Price',
                                    'value': '$" + item.Price + @"'
                                },
                                {
                                    'name': 'Listed by',
                                    'value': '" + item.Owner + @"'
                                }
                            ]
                        }
                    ],
                    'potentialAction': [
                        {
                            '@context': 'http://schema.org',
                            '@type': 'ViewAction',
                            'name': 'View in BillsList',
                            'target': [
                                'https://localhost:44300/items/detail/" + item.Id + @"'
                            ]
                        }
                    ]}";

            //prepare the http POST
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            using (var response = await client.PostAsync(webhook, content))
            {
                //TODO: check response.IsSuccessStatusCode
            }
        }
    }
}