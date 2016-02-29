using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Driver;
using MongoDB.Bson;
using MaratonBusiness.Models;
using System.Linq.Expressions;
using System.Reflection;

namespace MaratonBusiness.Code
{
    public class MDB : IDisposable
    {
        MongoDB.Driver.MongoClient client;
        MongoDB.Driver.IMongoDatabase db;
        public MDB()
        {
            client = new MongoClient("mongodb://10.0.0.20/MBM");
            db = client.GetDatabase("MBM");
        }

        public IMongoCollection<T> Document<T>() where T : DbModel
        {
            var doc = db.GetCollection<T>(typeof(T).Name);
            return doc;
        }

        public T FindOne<T>(Expression<Func<T, bool>> filter) where T : DbModel
        {
            var doc = this.Document<T>();
            return  doc.Find(filter).FirstOrDefault();
        }

        public List<T> Find<T>(Expression<Func<T, bool>> filter) where T : DbModel
        {
            var doc = this.Document<T>();
            return doc.Find(filter).ToList();
        }

        public long Delete<T>(Expression<Func<T, bool>> filter) where T : DbModel
        {
            var doc = this.Document<T>();
            return doc.DeleteMany(filter).DeletedCount;
        }

        public bool Insert<T>(params T[] instance) where T : DbModel
        {
            var doc = this.Document<T>();
            doc.InsertMany(instance);
            return true;
        }

        public T UpdateOne<T>(Expression<Func<T, bool>> filter, T instance ) where T : DbModel
        {
            if (instance == null)
                return null;

            var doc = this.Document<T>();

            List<UpdateDefinition<T>> updates = new List<UpdateDefinition<T>>();
            MongoDB.Driver.UpdateDefinitionBuilder<T> builder = new UpdateDefinitionBuilder<T>();


            Type t = instance.GetType();
            var ps = t.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var p in ps)
            {
                if (p.Name == "Id")
                    continue;
                updates.Add(builder.Set(p.Name, p.GetValue(instance)));
            }

            doc.UpdateOne(filter, builder.Combine(updates));

            return instance;
        }

        public void Dispose()
        {
            db = null;
            client = null;
        }
    }
}