using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;

namespace MRTBusiness.Models
{
    public class CFService
    {
        public string MaratonAddress { get; set; }
        public int MaratonPort { get; set; }

        public string MelotonAddress { get; set; }
        public int MelotonPort { get; set; }


        static Mutex safe_config_mutex = new Mutex();
        public void Save()
        {
            safe_config_mutex.WaitOne();
            var json = Newtonsoft.Json.JsonConvert.SerializeObject(this);
            try
            {
                System.IO.File.WriteAllText( HttpContext.Current.Server.MapPath("~") + "\\service.json", json );
            }
            catch {

            }
            safe_config_mutex.ReleaseMutex();
        }


        static public CFService Config()
        {
            CFService result = null;
            safe_config_mutex.WaitOne();
            try
            {
                var json = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~") + "\\service.json");
                result = Newtonsoft.Json.JsonConvert.DeserializeObject<CFService>(json);
            }
            catch
            {
                result = new CFService();
            }
            safe_config_mutex.ReleaseMutex();
            return result;
        }

    }
}