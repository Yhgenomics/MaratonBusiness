using MaratonBusiness.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaratonBusiness.Controllers
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