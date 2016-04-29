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
                try
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

                        string args = string.Format("-m p -a {0} -p {1} -r {2} -l {3}",
                            "10.0.0.11",
                            "112",
                            f.RemotePath,
                            f.Path);

                        var fileName = System.Web.HttpRuntime.AppDomainAppPath + "Exe/meloton";

                        System.IO.File.WriteAllText("/wwwroot/maratonbus/log.log", "");
                        System.IO.File.AppendAllText("/wwwroot/maratonbus/log.log", fileName + "\r\n");
                        System.IO.File.AppendAllText("/wwwroot/maratonbus/log.log", args + "\r\n");

                        process = new Process();
                        process.StartInfo.FileName = fileName;
                        process.StartInfo.Arguments = args;
                        process.StartInfo.UseShellExecute = false;   // 是否使用外壳程序 
                        process.StartInfo.CreateNoWindow = true;   //是否在新窗口中启动该进程的值 
                        process.StartInfo.RedirectStandardOutput = true;
                        process.Start();
                        process.WaitForExit();

                        System.IO.File.AppendAllText("/wwwroot/maratonbus/log.log", process.StandardOutput.ReadToEnd());

                        using (MDB mdb = new MDB())
                        {
                            f.State = 2;
                            mdb.UpdateOne<DbAttachment>(x => x.Id == f.Id, f);
                        }

                        System.IO.File.Delete(f.Path);
                    }
                }
                catch
                {
                    thrProcess = null;
                    process = null;
                }
            });
            thrProcess.Start();
        }
    }
}