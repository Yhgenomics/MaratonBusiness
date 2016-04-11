using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaratonBusiness.Models
{
    #region Dir
    public class DFSDirInfo
    {
        public string Path { get; set; }
    }

    public class DFSDirFile
    {
        public int Blockcount { get; set; }
        public string Path { get; set; }
        public int Size { get; set; }
    }

    public class DFSDir
    {
        public DFSDir()
        {
            this.Dir = new List<DFSDirInfo>();
            this.File = new List<DFSDirFile>();
        }

        public IEnumerable<DFSDirInfo> Dir { get; set; }
        public int Dircount { get; set; }
        public IEnumerable<DFSDirFile> File { get; set; }
        public int Filecount { get; set; }
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

        public IEnumerable<DFSFileNode> Node { get; set; }
        public int Nodecount { get; set; }
        public int Partid { get; set; }
        public int Size { get; set; }
    }

    public class DFSFile
    {
        public DFSFile()
        {
            this.Block = new List<DFSFileBlock>();
        }

        public IEnumerable<DFSFileBlock> Block { get; set; }
        public string Path { get; set; }
        public int Size { get; set; }
    }
    #endregion

    #region Node
    public class DFSNodeMeta
    {
        public string Address { get; set; }
        public int Alive { get; set; }
        public int Blockcount { get; set; }
        public int Cpu { get; set; }
        public long Memory { get; set; }
    }
    #endregion

}