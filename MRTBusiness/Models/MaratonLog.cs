using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MRTBusiness.Models
{
    public class MaratonLog
    {
        public int errorMask { get; set; }
        public string taskID { get; set; }
        public string subtaskID { get; set; }
        public string servantID { get; set; }
        public string content { get; set; }
    }
}