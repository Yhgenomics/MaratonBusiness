using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace MRTBusiness.Code
{
    public class MelotonAPI
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

        T GetData<T>(string url)
        {
            string data = queryJson(url);
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
            }
            catch
            {
                return default(T);
            }
        }

        public List<Models.DFSNodeMeta> GetNodes()
        {
            return GetData<List<Models.DFSNodeMeta>>("/v1/node");
        }


    }
}