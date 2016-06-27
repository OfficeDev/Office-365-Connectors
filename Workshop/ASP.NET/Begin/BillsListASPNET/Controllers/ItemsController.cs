using BillsListASPNET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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

                //TODO: loop through entities.Subscriptions and call webhook

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

        //Add o365-callwebhook snippet here
    }
}