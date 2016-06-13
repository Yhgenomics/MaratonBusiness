using MRTBusiness.Code;
using MRTBusiness.Models;
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

            //using (MDB mdb = new MDB())
            //{
            //    foreach (var item in mod)
            //    {
            //        DbServant dbs = new DbServant();
            //        dbs.cpu = item.cpu;
            //        dbs.Id = item.id;
            //        dbs.memory = item.memory;
            //        dbs.state = item.state;
            //        dbs.type = item.type;

            //        mdb.UpdateOne<DbServant>(dbs);
            //    }

            //    var ret = mdb.Find<DbServant>(x=>true).ToArray();
            //    return View(ret);
            //}

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
                cpus.Add(new { name = item.id , data = new List<object>() { new { x = ""  ,  y = item.sysinfo_cpu_user }  } });
                mems.Add(new { name = item.id, data = new List<object>() { new { x = ""  ,  y = item.sysinfo_mem_uesed } } });
            }

            return Json(new {
                mem = mems ,
                cpu = cpus
            });
        }
}
}