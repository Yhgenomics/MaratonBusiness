using MRTBusiness.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MRTBusiness.Controllers
{
    public class PipelineController : Controller
    {
        public ActionResult index()
        {
            using (MDB db = new MDB())
            {
                var pps = db.Find<Models.DbPipeline>(x => true).ToList();
                return View(pps);
            }
        }

        public ActionResult pipelist()
        {
            using (MDB db = new MDB())
            {
                var pps = db.Find<Models.DbPipe>(x => true).ToList();
                return View(pps);
            }
        }

        public ActionResult create()
        {
            return View();
        } 

        [HttpPost]
        public ActionResult create(FormCollection form)
        {
            using (MDB db = new MDB())
            {
                Models.DbPipeline line = new Models.DbPipeline();
                line.Name = form["Name"];
                line.PipeIds.AddRange(form["Pipes"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
                line.ServantIds.AddRange(form["Servants"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
                line.IsParallel = form["IsParallel"] != "false";
                db.Insert(line);
                return RedirectToAction("index");
            }
        }

        public ActionResult edit(string id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult edit(FormCollection form)
        {
            using (MDB db = new MDB())
            {
                Models.DbPipeline line = new Models.DbPipeline();
                line.Id = form["Id"];
                line.Name = form["Name"];
                line.PipeIds.AddRange(form["Pipes"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
                line.IsParallel = form["IsParallel"] != "false";
                db.UpdateOne(x => x.Id == line.Id, line);
                return Redirect("index");
            }
        }

        public ActionResult delete(string id)
        {
            using (MDB db = new MDB())
            {
                db.Delete<Models.DbPipeline>(x => x.Id == id);
                return RedirectToAction("index");
            }
        }


        public ActionResult servantlist()
        {
            MaratonAPI api = new MaratonAPI();
            return View(api.ServantList());
        }

    }
}