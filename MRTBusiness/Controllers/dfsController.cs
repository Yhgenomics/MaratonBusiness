using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MaratonBusiness.Controllers
{
    public class dfsController : Controller
    {
        string queryJson(string url)
        {
            WebClient wc = new WebClient();
            string ip;
            int port;

            //ip = System.Configuration.ConfigurationManager.AppSettings["MelotonIP"];
            //int.TryParse(System.Configuration.ConfigurationManager.AppSettings["MelotonPort"], out port);

            var t = Models.CFService.Config();
            ip = t.MelotonAddress;
            port = t.MelotonPort;

            string result = wc.DownloadString("http://" + ip + ":" + port + url);

            return result;
        }

        public void query(string url)
        {
            WebClient wc = new WebClient();
            string ip;
            int port;

            //ip = System.Configuration.ConfigurationManager.AppSettings["MelotonIP"];
            //int.TryParse(System.Configuration.ConfigurationManager.AppSettings["MelotonPort"], out port);
            //
            var t = Models.CFService.Config();
            ip = t.MelotonAddress;
            port = t.MelotonPort;
            string result = wc.DownloadString("http://" + ip + ":" + port + url);

            Response.Write(result);
        }

        // GET: dfs
        public ActionResult index(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = "/";
            }

            var jsonstr = queryJson("/v1/dir?path="+path);
            try
            {
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.DFSDir>(jsonstr);
                if(result==null)
                    result = new Models.DFSDir();

                result.File = (from j in result.File orderby j.Path select j).ToList();


                return View(result);
            }
            catch(Exception eee)
            {
                Debug.Print(eee.Message);
                return View(new Models.DFSDir());
            }

        }

        public ActionResult node()
        {
            var jsonstr = queryJson("/v1/node");
            try
            {
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Models.DFSNodeMeta>>(jsonstr);

                if (result == null)
                {
                    result = new List<Models.DFSNodeMeta>();
                }

                return View(result);
            }
            catch
            {
                return View(new List<Models.DFSNodeMeta>());
            }

        }

        public ActionResult FileDetail(string path)
        {
            var jsonstr = queryJson("/v1/file?path="+path);
            try
            {
                var result = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.DFSFile>(jsonstr);
                result.Block = (from j in result.Block orderby j.Partid select j).ToList();
                return View(result);
            }
            catch
            {
                return View(new Models.DFSFile());
            }
        }

    }
}