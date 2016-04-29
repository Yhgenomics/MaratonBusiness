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
                string name = this.Request.Form[0];
                int block = 0; 
                int blockNum = 0;

                int.TryParse(this.Request.Form[1], out block);
                int.TryParse(this.Request.Form[2], out blockNum);

                for (int i = 0; i < this.Request.Files.Count; i++)
                {
                    HttpPostedFileBase file = this.Request.Files[i];
                    var localPath = Server.MapPath("/TempFile/" + this.Request.Form[0] );

                    var fs = System.IO.File.Open(localPath, System.IO.FileMode.OpenOrCreate);
                    fs.Position = block * file.ContentLength;

                    byte[] buffer = new byte[1024 * 1024];
                    while (true)
                    {
                        var reads = file.InputStream.Read(buffer, 0, buffer.Length);

                        if (reads == 0)
                            break;

                        fs.Write(buffer, 0, reads);
                    }

                    fs.Close();

                    if (block == (blockNum-1))
                    {
                        DbAttachment dbAttr = new DbAttachment();
                        dbAttr.Name = name;
                        dbAttr.Path = localPath;
                        dbAttr.RemotePath = "/fastq/" + DateTime.Now.ToString("yyyy_MM_dd") + "/" + dbAttr.Id + ".fastq";
                        dbAttr.CreateTime = DateTime.Now;
                        dbAttr.State = 0;
                        mdb.Insert<DbAttachment>(dbAttr);
                    }
                }
            }

            YHFSUploader.Upload();

            return Json(new { code = 0, msg = "" });
        }
    }
}