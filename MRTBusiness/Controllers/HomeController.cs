using MRTBusiness.Code;
using MRTBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MRTBusiness.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            VMHomeIndex dat = new VMHomeIndex();

            MelotonAPI melotonAPI = new MelotonAPI();
            var meNodes = melotonAPI.GetNodes();

            foreach (var mn in meNodes)
            {
                dat.MelotonNodeName.Add(mn.Address);
                dat.MelotonNodeBlockCount.Add((uint)mn.Blockcount);
            }


            using (MDB mdb = new MDB())
            {
                dat.FinishTaskCount = mdb.Find<DbTask>(m => m.State == 303).Count;
                dat.UnFinishTaskCount = mdb.Find<DbTask>(m => m.State != 303).Count;
            }


            MaratonAPI maratonAPI = new MaratonAPI();
            var maratonNodes = maratonAPI.ServantList();


            foreach (var item in maratonNodes)
            {
                dat.MaratonNodeName.Add(item.id);
                var mn = new VMHomeIndexMaratonNode();
                mn.data.Add(item.memory / (1024));
                mn.label = item.id;

                dat.MaratonNode.Add(mn);
            }

            return View(dat);
        }

    }
}