using MRTBusiness.Code;
using MRTBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MRTBusiness.Controllers
{
    public class MaratonController : Controller
    {
        bool TryFinishTask(MaratonResult json, DbTask task, DbPipeline pipeline)
        {
            MaratonAPI api = new MaratonAPI();
            using (MDB mdb = new MDB())
            {
                if (json.status == 303)
                {
                    for (int i = 0; i < task.Pipelines.Count; i++)
                    {
                        if (task.Pipelines[i] != pipeline.Id)
                            continue;

                        if (i < (task.Pipelines.Count - 1))
                        {
                            var p = mdb.FindOne<DbPipeline>(x => x.Id == task.Pipelines[i + 1]);
                            task.Inputs = json.data;
                            api.TaskDeliver(task, p);
                            return true;
                        }
                        else if (i == (task.Pipelines.Count - 1))
                        {
                            task.State = json.status;
                            task.Result = json.data;
                            task.FinishTime = DateTime.Now;
                            task.Duratation = (int)((task.FinishTime - task.ExecuteTime).TotalSeconds);
                            mdb.UpdateOne<DbTask>(x => x.Id == json.taskid, task);
                            return true;
                        }
                    }
                    return true;
                }
                else
                {
                    task.State = json.status;
                    mdb.UpdateOne<DbTask>(x => x.Id == json.taskid, task);
                    return true;
                }
            }
        }
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

                if (task == null ||
                    pipeline == null)
                {
                    return;
                }

                TryFinishTask(json, task, pipeline);

                var newTask = mdb.Find<DbTask>(x => x.State == 2).FirstOrDefault();
                var newpipeline = mdb.Find<DbPipeline>(x => x.Id == newTask.Pipelines[0]).FirstOrDefault();

                if (newTask == null) return;
                if (newpipeline == null) return;

                var result = api.TaskDeliver(newTask, newpipeline);

                if (result.code == 0)
                {
                    newTask.ExecuteTime = DateTime.Now;
                    newTask.State = 1;
                    mdb.UpdateOne<DbTask>(x => x.Id == newTask.Id, newTask);
                }
                else
                {
                    newTask.State = 2;
                    mdb.UpdateOne<DbTask>(x => x.Id == newTask.Id, newTask);
                }
            }
        }
        [HttpPost]
        public void log(MaratonLog json)
        {
            if (json.errorMask != 0)
            {
                return;
            }

            using (MDB mdb = new MDB())
            {
                DbLog log = mdb.Find<DbLog>(x => 
                    x.TaskID == json.taskID && 
                    x.SubtaskID == json.subtaskID && 
                    x.ServantID == json.servantID).FirstOrDefault();

                bool isNew = false;
                if (log == null)
                {
                    isNew = true;
                    log = new DbLog();
                }

                log.ErrorMask = json.errorMask;
               
                Base64Decoder b64d = new Base64Decoder();
                byte[] encodingContent = b64d.GetDecoded(json.content);
                log.Content = System.Text.ASCIIEncoding.Default.GetString(encodingContent);
                log.TaskID = json.taskID;
                log.SubtaskID = json.subtaskID;
                log.ServantID = json.servantID;

                if( isNew )
                {
                    mdb.Insert<DbLog>(log);
                }
                else
                {
                    mdb.UpdateOne<DbLog>(x => 
                            x.TaskID == json.taskID &&
                            x.SubtaskID == json.subtaskID &&
                            x.ServantID == json.servantID , log);
                }
            }
        }
    }
}