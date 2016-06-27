using BillsListASPNET.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BillsListASPNET.Controllers
{
    public class CallbackController : Controller
    {
        // GET: Callback
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

                //save the subscription
                using (BillsListEntities entities = new BillsListEntities())
                {
                    entities.Subscriptions.Add(sub);
                    entities.SaveChanges();
                    return Redirect(state);
                }
            }
        }
    }
}