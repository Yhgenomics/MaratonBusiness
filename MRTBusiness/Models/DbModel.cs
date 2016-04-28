using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MRTBusiness.Models
{
    public class DbModel
    {
        public DbModel()
        {
            this.Id = Guid.NewGuid().ToString().Replace("-", "");
            Increase = DateTime.Now.ToFileTimeUtc();
        }

        public string Id { get; set; }
        public long Increase { get; set; }
    }
}