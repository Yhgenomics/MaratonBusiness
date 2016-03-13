using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaratonBusiness.Models
{
    public class DbTask : DbModel
    {
        public DbTask()
        {
            Resources = new List<string>();
            Servants = new List<string>();
            Pipelines = new List<string>();
            Inputs = new List<string>();
            Result = new List<string>();
        }

        public string Name { get; set; }
        public List<string> Resources { get; set; }
        public List<string> Inputs { get; set; }
        public List<string> Servants { get; set; }
        public List<string> Pipelines { get; set; }

        public List<string> Result { get; set; }

        public int State { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime ExecuteTime { get; set; }
        public DateTime FinishTime { get; set; }
        public int Duratation { get; set; }
    }
}