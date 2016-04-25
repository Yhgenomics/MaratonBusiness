using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaratonBusiness.Models
{
    public class VMHomeIndexMaratonNode
    {
        public VMHomeIndexMaratonNode()
        {
            data = new List<int>();
        }

        public string label { get; set; }
        public List<int> data { get; set; }

    }
    public class VMHomeIndex
    {
        public VMHomeIndex()
        {
            this.MelotonNodeName = new List<string>();
            this.MelotonNodeBlockCount = new List<uint>();
            this.MaratonNode = new List<VMHomeIndexMaratonNode>();
            this.MaratonNodeName = new List<string>();
        }
        public List<string> MelotonNodeName { get; set; }
        public List<uint> MelotonNodeBlockCount
        {
            get; set;
        }
        public List<string> MelotonNodesColor
        {
            get
            {
                Random rnd = new Random();

                List<string> ret = new List<string>();
                foreach (var item in this.MelotonNodeBlockCount)
                {
                    var c = (uint)(rnd.NextDouble()* 0xFFFFFF);
                    ret.Add("#"+c.ToString("X6"));
                }
                return ret;
            }
        }

        public int FinishTaskCount { get; set; }
        public int UnFinishTaskCount { get; set; }
        public int PeddingTaskCount { get; set; }

        public List<VMHomeIndexMaratonNode> MaratonNode { get; set; }
        public List<string> MaratonNodeName { get; set; }

    }
}