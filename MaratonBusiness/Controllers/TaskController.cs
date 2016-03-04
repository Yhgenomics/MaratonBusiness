using MaratonBusiness.Code;
using MaratonBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaratonBusiness.Controllers
{
    public class TaskController : Controller
    {
        public ActionResult index(int? pageId)
        {
            VMTaskIndex mod = new VMTaskIndex();
            using (MDB db = new MDB())
            {
                var t = db.Find<DbTask>(x => true).Skip(pageId.GetValueOrDefault() * mod.PageSize).Take(mod.PageSize).ToList();
                mod.Tasks = t;
                mod.TotalCount = db.Find<DbTask>(x => true).Count;
                mod.CurrentPage = pageId.GetValueOrDefault();
            }

            return View(mod);
        }

        public ActionResult pipelinelist()
        {
            using (MDB db = new MDB())
            {
                var t = db.Find<DbPipeline>(x => true).ToList();
                return View(t);
            }
        }

        public ActionResult servantlist()
        {

            return View();
        }


        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(FormCollection form)
        {
            return RedirectToAction("create");
        }
    }
}