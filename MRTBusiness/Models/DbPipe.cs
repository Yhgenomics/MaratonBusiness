using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MRTBusiness.Models
{
    public class DbPipe : DbModel
    {
        public DbPipe()
        {
            this.Parameters = new List<string>();
        }
        public string Name { get; set; }
        public string Executor { get; set; }
        public bool IsMultipleInput { get; set; }
        public bool IsMultipleThread { get; set; }
        public List<string> Parameters { get; set; }
    }
}