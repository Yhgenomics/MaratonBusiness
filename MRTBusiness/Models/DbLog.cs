using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MRTBusiness.Models
{
    public class DbLog : DbModel
    {
        public int ErrorMask { get; set; }
        public string TaskID { get; set; }
        public string SubtaskID { get; set; }
        public string ServantID { get; set; }
        public string Content { get; set; }
    }
}