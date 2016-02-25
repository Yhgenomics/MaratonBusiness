using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaratonBusiness.Models
{
    public class DbModel
    {
        public DbModel()
        {
            this.Id = Guid.NewGuid().ToString().Replace("-", "");
        }

        public string Id { get; set; }
    }
}