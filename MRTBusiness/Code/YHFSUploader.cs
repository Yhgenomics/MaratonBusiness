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
        public static void Upload()
        {  
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
                            return;
                        }

                        using (MDB mdb = new MDB())
                        {
                            f.State = 1;
                            mdb.UpdateOne<DbAttachment>(x => x.Id == f.Id, f);
                        }

                        YHFSClient client = new YHFSClient();
                        string log = client.Upload(f.RemotePath, f.Path);

                        System.IO.File.AppendAllText("/wwwroot/maratonbus/log.log", log);

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
                }
            });
            thrProcess.Start();
        }
    }
}