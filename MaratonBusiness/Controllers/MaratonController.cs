using MaratonBusiness.Code;
using MaratonBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaratonBusiness.Controllers
{
    public class MaratonController : Controller
    {
        [HttpPost]
        public void result(MaratonResult json)
        {
            if (json == null)
                return;

            MaratonAPI api = new MaratonAPI();
            DbTask task = null;
            DbPipeline pipeline = null;

            using (MDB mdb = new MDB())
            {
                task = mdb.Find<DbTask>(x => x.Id == json.taskid).FirstOrDefault();
                pipeline = mdb.Find<DbPipeline>(x => x.Id == json.pipelineid).FirstOrDefault();

                if( task == null || 
                    pipeline == null)
                {
                    return;
                }

                if ( json.status == 3 )
                {

                    for (int i = 0; i < task.Pipelines.Count; i++)
                    {
                        if (task.Pipelines[i] != pipeline.Id)
                            continue;

                        if ( i < (task.Pipelines.Count - 1) )
                        {
                            var p = mdb.FindOne<DbPipeline>(x => x.Id == task.Pipelines[i + 1]);
                            task.Inputs = json.data;
                            api.TaskDeliver(task, p);
                            return;
                        }
                        else if (i == (task.Pipelines.Count - 1))
                        {
                            task.State = json.status;
                            task.Result = json.data;
                            mdb.UpdateOne<DbTask>(x => x.Id == json.taskid, task);
                        }
                    }
                }
                else
                {
                    task.State = json.status;
                    mdb.UpdateOne<DbTask>(x => x.Id == json.taskid, task);
                }
            }
        }
    }
}