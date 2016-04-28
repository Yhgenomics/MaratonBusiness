using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MRTBusiness.Models
{
    public class MaratonResult
    {
        public MaratonResult()
        {
            this.data = new List<string>();
        }

        public string taskid { get; set; }
        public string pipelineid { get; set; }
        public int status { get; set; }
        public List<string> data { get; set; }
    }
}