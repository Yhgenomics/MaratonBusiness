using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaratonBusiness.Code
{
    public class MaratonAPI
    {
        string ip = "";
        int port = 102;
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
    }
}