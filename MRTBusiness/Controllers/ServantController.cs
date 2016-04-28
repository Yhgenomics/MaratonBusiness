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
    }
}