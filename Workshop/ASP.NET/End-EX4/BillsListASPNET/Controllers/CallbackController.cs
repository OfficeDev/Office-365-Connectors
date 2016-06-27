using BillsListASPNET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BillsListASPNET.Controllers
{
    public class CallbackController : Controller
    {
        // GET: Callback
        [Route("callback")]
        [HttpGet]
        public ActionResult Index()
        {
            var error = Request["error"];
            var state = Request["state"];
            if (!String.IsNullOrEmpty(error))
            {
                return RedirectToAction("Error", "Home", null);
            }
            else
            {
                var group = Request["group_name"];
                var webhook = Request["webhook_url"];
                Subscription sub = new Subscription();
                sub.GroupName = group;
                sub.WebHookUri = webhook;

                //return the partial subscription and add state and categories to ViewData
                ViewData.Add("state", state);
                ViewData.Add("categories", ItemsController.categories);
                return View(sub);
            }
        }

        [Route("callback")]
        [HttpPost]
        public ActionResult Index(Subscription sub)
        {
            //save the subscription
            using (BillsListEntities entities = new BillsListEntities())
            {
                entities.Subscriptions.Add(sub);
                entities.SaveChanges();

                //redirect back to the original location the user was at
                return Redirect(Request["state"]);
            }
        }
    }
}