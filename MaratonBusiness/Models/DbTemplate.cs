using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaratonBusiness.Models
{
    public class DbTemplate : DbModel
    {
        public DbTemplate()
        {
            this.Parameters = new List<string>();
        }
        public string Name { get; set; }
        public string Executor { get; set; }
        public List<string> Parameters { get; set; }
    }
}