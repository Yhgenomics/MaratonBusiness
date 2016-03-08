using MaratonBusiness.Code;
using MaratonBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaratonBusiness.Controllers
{
    public class callbackController : Controller
    {
        public void pipelinefinish(string taskid,string pipelineid,int state)
        {
            using (MDB db = new MDB())
            {
                MaratonAPI api = new MaratonAPI();
                DbTask task = db.Find<DbTask>(x => x.Id == taskid).FirstOrDefault();
                task.State = state;

                if ( task == null)
                {
                    return;
                }

                for (int i = 0; i < task.Pipelines.Count; i++)
                {
                    if (task.Pipelines[i] != pipelineid)
                        continue;

                    if( task.Pipelines[i] == pipelineid &&
                        i < (task.Pipelines.Count - 1) )
                    {
                        api.TaskDeliver(task, db.FindOne<DbPipeline>(x => x.Id == task.Pipelines[i + 1]));
                    } 
                }

                db.UpdateOne<DbTask>(x => x.Id == taskid, task);
            }
        }
    }
}