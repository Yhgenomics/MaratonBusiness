using MRTBusiness.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MRTBusiness.Models;
using MongoDB.Driver;

namespace MRTBusiness.Controllers
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
                dbt.IsMultipleInput = form["IsMultipleInput"] != "false";
                dbt.IsMultipleThread = form["IsMultipleThread"] != "false";
                dbt.Parameters = form["Parameters"].Split(new string[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                UpdateDefinitionBuilder<DbPipe> builder = new UpdateDefinitionBuilder<DbPipe>();
                mdb.UpdateOne<DbPipe>(x => x.Id == dbt.Id, dbt);

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