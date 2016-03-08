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

        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(FormCollection form)
        {
            DbTask task = new DbTask();
            task.Name = form["Name"];
            task.CreateTime = DateTime.Now;
            task.Inputs.AddRange(form["Inputs"].Split(new string[] { "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries));
            task.Pipelines.AddRange(form["Pipelines"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
            task.Servants.AddRange(form["Servants"].Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries));
            task.State = 0;

            using (MDB db = new MDB())
            {
                db.Insert<DbTask>(task);
            }

            return RedirectToAction("index");
        }

        public ActionResult start(string id)
        {
            using (MDB db = new MDB())
            {
                var task = db.Find<DbTask>(x => x.Id == id).FirstOrDefault();
                if (task == null)
                {
                    return RedirectToAction("index");
                }

                var pipeline = db.Find<DbPipeline>(x => x.Id == task.Pipelines[0]).FirstOrDefault();

                if (pipeline == null)
                {
                    return RedirectToAction("index");
                }

                MaratonAPI api = new MaratonAPI();
                var result = api.TaskDeliver(task, pipeline);

                //if( result.Code == 0 )
                //{

                //}
            }

            return RedirectToAction("index");
        }

        public ActionResult delete(string id)
        {
            using (MDB db = new MDB())
            {
                db.Delete<DbTask>(x => x.Id == id);
            }

            return RedirectToAction("index");
        }
    }
}