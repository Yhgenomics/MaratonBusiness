using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MRTBusiness.Models
{
    #region Dir
    public class DFSDirInfo
    {
        public string Path { get; set; }
    }

    public class DFSDirFile
    {
        public Int64 Blockcount { get; set; }
        public string Path { get; set; }
        public Int64 Size { get; set; }
    }

    public class DFSDir
    {
        public DFSDir()
        {
            this.Dir = new List<DFSDirInfo>();
            this.File = new List<DFSDirFile>();
        }

        public List<DFSDirInfo> Dir { get; set; }
        public Int64 Dircount { get; set; }
        public List<DFSDirFile> File { get; set; }
        public Int64 Filecount { get; set; }
    }
    #endregion

    #region File
    public class DFSFileNode
    {
        public string Address { get; set; }
    }

    public class DFSFileBlock
    {
        public DFSFileBlock()
        {
            this.Node = new List<DFSFileNode>();
        }

        public List<DFSFileNode> Node { get; set; }
        public int Nodecount { get; set; }
        public int Partid { get; set; }
        public Int64 Size { get; set; }
    }

    public class DFSFile
    {
        public DFSFile()
        {
            this.Block = new List<DFSFileBlock>();
        }

        public List<DFSFileBlock> Block { get; set; }
        public string Path { get; set; }
        public Int64 Size { get; set; }
    }
    #endregion

    #region Node
    public class DFSNodeMeta
    {
        public string Address { get; set; }
        public int Alive { get; set; }
        public int Blockcount { get; set; }
        public int Cpu { get; set; }
        public Int64 Memory { get; set; }
    }
    #endregion

}