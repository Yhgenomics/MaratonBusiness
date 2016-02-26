using MaratonBusiness.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MaratonBusiness.Models;
using MongoDB.Driver;

namespace MaratonBusiness.Controllers
{
    public class PipeController : Controller
    {
        // GET: Template
        public ActionResult index()
        {
            using (MDB mdb = new MDB())
            {
                var dbt = mdb.Document<DbPipe>().Find(x => true).ToCursor().ToList();
                return View(dbt);
            }

            return View();
        }

        public ActionResult create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult create(FormCollection form)
        {
            using (MDB mdb = new MDB())
            {
                DbPipe dbt = new DbPipe();
                dbt.Name = form["Name"];
                dbt.Executor = form["Executor"];
                dbt.Parameters = form["Parameters"].Split(new string[] { "\r\n","\r","\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                mdb.Document<DbPipe>().InsertOne(dbt);
            }

            return RedirectToAction("index");
        }


        public ActionResult edit(string id)
        {
            using (MDB mdb = new MDB())
            {
                var dbt = mdb.Document<DbPipe>().Find(x => x.Id == id).ToCursor().FirstOrDefault();
                return View(dbt);
            }

            return View();
        }

        [HttpPost]
        public ActionResult edit(FormCollection form)
        {
            using (MDB mdb = new MDB())
            {
                DbPipe dbt = new DbPipe();
                dbt.Id = form["Id"];
                dbt.Name = form["Name"];
                dbt.Executor = form["Executor"];
                dbt.Parameters = form["Parameters"].Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
                UpdateDefinitionBuilder<DbPipe> builder = new UpdateDefinitionBuilder<DbPipe>();

                var t = mdb.UpdateOne<DbPipe>(x => x.Id == dbt.Id, dbt);
            }

            return RedirectToAction("index");
        }

        public ActionResult delete(string id)
        {
            using (MDB mdb = new MDB())
            {
                mdb.Document<DbPipe>().DeleteOne(t => t.Id == id);
            }

            return RedirectToAction("index");
        }
    }
}