using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MRTBusiness.Models
{
    public class DbAttachment : DbModel
    {
        public string Name { get; set; }
        public string RemotePath { get; set; }
        public string Path { get; set; }
        public Int64 Size { get; set; }
        public int State { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime CreateTime { get; set; }
    }
}