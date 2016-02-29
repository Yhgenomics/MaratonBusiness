using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaratonBusiness.Models
{
    public class DbPipeline : DbModel
    {
        public DbPipeline()
        {
            PipeIds = new List<string>();
        }

        public string Name { get; set; }
        public List<string> PipeIds { get; set; }
    }
}