using MRTBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MRTBusiness.Controllers
{
    public class serviceController : Controller
    {
        // GET: service
        public ActionResult index()
        {
            return View(CFService.Config());
        }

        [HttpPost]
        public ActionResult index(FormCollection formdata)
        {
            CFService service = new CFService();
            service.MaratonAddress = formdata["MaratonAddress"];
            service.MaratonPort = int.Parse(formdata["MaratonPort"]);

            service.MelotonAddress = formdata["MelotonAddress"];
            service.MelotonPort = int.Parse(formdata["MelotonPort"]);

            service.Save();

            return RedirectToAction("index");
        }

    }
}