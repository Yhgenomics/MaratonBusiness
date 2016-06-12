using MRTBusiness.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MRTBusiness.Controllers
{
    public class ServantController : Controller
    {
        // GET: Servant
        public ActionResult index()
        {
            MaratonAPI api = new MaratonAPI();
            var mod = api.ServantList();
            return View(mod);
        }

        [HttpPost]
        public JsonResult state()
        {
            MaratonAPI api = new MaratonAPI();
            var rep = api.ServantList();

            List<object> mems = new List<object>();
            List<object> cpus = new List<object>();

            foreach (var item in rep)
            {
                cpus.Add(new { name = item.id , data = new List<object>() { new { x = ""  ,  y = item.cpu }  } });
                mems.Add(new { name = item.id, data = new List<object>() { new { x = ""  ,  y = item.memory } } });
            }

            return Json(new {
                mem = mems ,
                cpu = cpus
            });
        }
}
}