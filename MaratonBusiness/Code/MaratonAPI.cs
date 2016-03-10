using MaratonBusiness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaratonBusiness.Code
{
    public class MaratonAPI
    {
        string ip = "10.0.0.219";
        int port = 91;

        public MaratonAPI()
        {

        }

        public MaratonAPI(string ip)
        {
            this.ip = ip;
        }

        public Message.MessageServantStateReply ServantList()
        {
            XSocket sock = new XSocket();
            sock.Connect(this.ip, this.port);
            Message.MessageServantState state = new Message.MessageServantState();
            state.Code = 0;
            sock.Send<Message.MessageServantState>(Message.MessageServantState.SerializeToBytes(state));
            var msg = sock.Receive() as Message.MessageServantStateReply;
            sock.Close();
            return msg;
        }


        public Message.MessageTaskDeliverReply TaskDeliver(DbTask task , DbPipeline line)
        {
            XSocket sock = new XSocket();
            Message.MessageTaskDeliverReply msg;
            sock.Connect(this.ip, this.port);

            Message.MessageTaskDeliver td = new Message.MessageTaskDeliver();
            td.Id = task.Id;
            td.Input = task.Inputs;
            td.Servants = task.Servants;
            td.Resources = new List<string>();
            td.IsParallel = line.IsParallel;
            td.Pipeline = (new Message.MessagePipeline() {
                Id = line.Id,
                Name = line.Name,
                Pipes = new List<Message.MessagePipe>()
            }); 

            using (MDB db = new MDB())
            {
                for (int i = 0; i < line.PipeIds.Count; i++)
                {
                    var pipe = db.FindOne<DbPipe>(x => x.Id == line.PipeIds[i]);
                    td.Pipeline.Pipes.Add(new Message.MessagePipe() {
                        Id = pipe.Id,
                        Executor = pipe.Executor,
                        MultipleInput = pipe.IsMultipleInput ,
                        MultipleThread = pipe.IsMultipleThread ,
                        Parameters = pipe.Parameters ,
                        Name = pipe.Name  });
                }
            }

            sock.Send<Message.MessageTaskDeliver>(Message.MessageTaskDeliver.SerializeToBytes(td));
            msg = sock.Receive() as Message.MessageTaskDeliverReply;
            sock.Close();
            return msg;
        }
    }
}