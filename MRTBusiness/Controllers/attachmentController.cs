using MRTBusiness.Code;
using MRTBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MRTBusiness.Controllers
{
    public class attachmentController : Controller
    {
        // GET: attachment
        public ActionResult index()
        {
            List<Models.DbAttachment> result = null;
            using (MDB mdb = new MDB())
            {
                result = mdb.Find<Models.DbAttachment>(x => true).OrderByDescending(x=>x.Increase).ToList();
                if (result == null)
                    result = new List<Models.DbAttachment>();
            }
            return View(result);
        }

        public ActionResult delete(string id)
        {
            using (MDB mdb = new MDB())
            {
                var result = mdb.Delete<Models.DbAttachment>(x => x.Id == id && x.State!=2);
                return RedirectToAction("index");
            }
        }

        [HttpPost]
        public JsonResult upload()
        {
            if (this.Request.Files.Count == 0)
            {
                return Json(new { code = 1, msg = "no files" });
            }

            using (MDB mdb = new MDB())
            {
                for (int i = 0; i < this.Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = this.Request.Files[i];
                    DbAttachment dbAttr = new DbAttachment();
                    var localPath = Server.MapPath("/TempFile/" + dbAttr.Id + ".fastq");
                    file.SaveAs(localPath);

                    dbAttr.Name = file.FileName;
                    dbAttr.Path = localPath;
                    dbAttr.RemotePath = "/fastq/" + DateTime.Now.ToString("yyyy_MM_dd") + "/" + dbAttr.Id+".fastq";
                    dbAttr.CreateTime = DateTime.Now;
                    dbAttr.State = 0;
                    mdb.Insert<DbAttachment>(dbAttr);
                }
            }

            YHFSUploader.Upload();

            return Json(new { code = 0, msg = "" });
        }
    }
}