using MRTBusiness.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;

namespace MRTBusiness.Code
{
    public class YHFSUploader
    {
        static Thread thrProcess;
        static Process process;
        public static void Upload()
        {
            if (process != null)
                return;

            if (thrProcess != null)
                return;

            thrProcess = new Thread(() =>
            {
                while (true)
                {
                    DbAttachment f = new DbAttachment();
                    using (MDB mdb = new MDB())
                    {
                        f = mdb.Find<DbAttachment>(x => x.State == 0).FirstOrDefault();
                    }

                    if (f == null)
                    {
                        thrProcess = null;
                        process = null;
                        return;
                    }

                    using (MDB mdb = new MDB())
                    {
                        f.State = 1;
                        mdb.UpdateOne<DbAttachment>(x => x.Id == f.Id, f);
                    }

                    ProcessStartInfo start = new ProcessStartInfo();
                    
                    var fileName = System.IO.Path.Combine(System.Web.HttpRuntime.AppDomainAppPath) + "Exe/meloton";
                    start.FileName = fileName;
                    start.Arguments = string.Format("-m p -a {0} -p {1} -r {2} -l {3}",
                        "10.0.0.11",
                        "112",
                        f.RemotePath,
                        f.Path);
                    process = new Process();
                    process.StartInfo = start;
                    process.Start();
                    process.WaitForExit();

                    using (MDB mdb = new MDB())
                    {
                        f.State = 2;
                        mdb.UpdateOne<DbAttachment>(x => x.Id == f.Id, f);
                    }

                    System.IO.File.Delete(f.Path);
                }
            });
            thrProcess.Start();
        }
    }
}