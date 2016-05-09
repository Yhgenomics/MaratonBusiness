using MRTBusiness.Code;
using MRTBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MRTBusiness.Controllers
{
    public class TaskController : Controller
    {
        public ActionResult index(int? pageId)
        {
            VMTaskIndex mod = new VMTaskIndex();
            using (MDB db = new MDB())
            {
                mod.CurrentPage = pageId.GetValueOrDefault();
                mod.Tasks = db.Find<DbTask>(x => true).OrderByDescending(x=>x.Increase).Skip(mod.CurrentPage * mod.PageSize).Take(mod.PageSize).ToList();
                mod.TotalCount = db.Find<DbTask>(x => true).Count;
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

            MaratonAPI api = new MaratonAPI();
            var ls = api.ServantList();

            if( ls == null )
            {
                ls = new List<Message.MessageServantStateReply>();
            }

            foreach (var item in ls)
            {
                task.Servants.Add(item.id);
            }
           
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
                var task = db.Find<DbTask>(x => x.Id == id && x.State == 0).FirstOrDefault();
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
               
                if (result.code == 0)
                {
                    task.ExecuteTime = DateTime.Now;
                    task.State = 1;
                    db.UpdateOne<DbTask>(x => x.Id == id, task);
                } 
                else
                {
                    task.State = 2;
                    db.UpdateOne<DbTask>(x => x.Id == id, task);
                }
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


        public ActionResult result(string id)
        {
            using (MDB db = new MDB())
            {
                var task = db.FindOne<DbTask>(x => x.Id == id);
                if (task == null)
                    return View(new DbTask());

                return View(task);
                
            }
        }
    }
}