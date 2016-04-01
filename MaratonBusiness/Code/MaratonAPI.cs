using MaratonBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace MaratonBusiness.Code
{
    public class MaratonAPI
    {
        string ip = "10.0.0.219";
        int port = 91;

        string PostData(string url , string json)
        {
            WebClient wc = new WebClient();
            wc.Headers["Content-Type"] = "application/json";
            return wc.UploadString("http://" + ip + url, "POST", json);
        }

        T PostData<T>(string url , string json)
        {
            WebClient wc = new WebClient();
            string data = wc.UploadString("http://" + ip+ ":" + port + url, "POST", json);
            try
            {
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(data);
            }
            catch
            {
                return default(T);
            }
        }


        public MaratonAPI()
        {
            this.ip = System.Web.Configuration.WebConfigurationManager.AppSettings["MaratonIP"];
            this.port = int.Parse(System.Web.Configuration.WebConfigurationManager.AppSettings["MaratonPort"]);
        }

        public MaratonAPI(string ip)
        {
            this.ip = ip;
        }

        public List<Message.MessageServantStateReply> ServantList()
        {
            List<Message.MessageServantStateReply> result = new List<Message.MessageServantStateReply>();
            Message.MessageServantState state = new Message.MessageServantState();
            state.code = 0;
            var msg = PostData<List<Message.MessageServantStateReply>>("/servant/status", Newtonsoft.Json.JsonConvert.SerializeObject(state));
            result.AddRange(msg);
            //sock.Send<Message.MessageServantState>(Message.MessageServantState.SerializeToBytes(state));
            //var msg = sock.Receive() as Message.MessageServantStateReply;
            //sock.Close();
            return result;
        }

        public Message.MessageTaskDeliverReply TaskDeliver(DbTask task , DbPipeline line)
        {
            var servants = ServantList();

            Message.MessageTaskDeliver td = new Message.MessageTaskDeliver();
            td.id = task.Id;
            td.input = task.Inputs;
            td.servants = servants.Select(m=>m.id).ToList();
            td.resources = new List<string>();
            td.isParallel = line.IsParallel;
            td.originalID = td.id;
            td.pipeline = (new Message.MessagePipeline() {
                id = line.Id,
                name = line.Name,
                pipes = new List<Message.MessagePipe>()
            }); 

            using (MDB db = new MDB())
            {
                for (int i = 0; i < line.PipeIds.Count; i++)
                {
                    var pipe = db.FindOne<DbPipe>(x => x.Id == line.PipeIds[i]);
                    var p = new Message.MessagePipe()
                    {
                        id = pipe.Id,
                        executor = pipe.Executor,
                        multipleInput = pipe.IsMultipleInput,
                        multipleThread = pipe.IsMultipleThread,
                        parameters = pipe.Parameters,
                        name = pipe.Name
                    };
                     
                    td.pipeline.pipes.Add(p);
                }
            }

            var msg = PostData<Message.MessageTaskDeliverReply>("/task/deliver", Newtonsoft.Json.JsonConvert.SerializeObject(td));
            //sock.Send<Message.MessageTaskDeliver>(Message.MessageTaskDeliver.SerializeToBytes(td));
            //msg = sock.Receive() as Message.MessageTaskDeliverReply;
            //sock.Close();
            return msg;
        }
    }
} 