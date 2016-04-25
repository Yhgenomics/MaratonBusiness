using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaratonBusiness.Models
{
    public class VMTaskIndex : Pagination
    {
        public VMTaskIndex()
        {
            Tasks = new List<DbTask>();
        }

        public List<DbTask> Tasks { get; set; }
    }
}