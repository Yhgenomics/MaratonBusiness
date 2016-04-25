using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaratonBusiness.Models
{
    public class DbStep : DbModel
    {
        public string TemplateId { get; set; }
        public string Name { get; set; }
        public string Executor { get; set; }
        public string ParentId { get; set; }
    }
}