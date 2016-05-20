using MRTBusiness.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace MRTBusiness.Code
{
    public class YHFSClient
    {
        const string MasterAddr = "10.0.0.11";
        const int MasterPort = 112;

        public string Upload(string remote, string local)
        {
            var cfg = CFService.Config();
            Process process = null;
            string args = string.Format("-m p -a {0} -p {1} -r {2} -l {3}",
                MasterAddr,
                MasterPort,
                remote,
                local);

            var log = System.Web.HttpRuntime.AppDomainAppPath + "Exe/log.log";
            System.IO.File.WriteAllText(log, args);

            var fileName = System.Web.HttpRuntime.AppDomainAppPath + "Exe/meloton";
             
            process = new Process();
            process.StartInfo.FileName = fileName;
            process.StartInfo.Arguments = args;
            process.StartInfo.UseShellExecute = false;   // 是否使用外壳程序 
            process.StartInfo.CreateNoWindow = true;   //是否在新窗口中启动该进程的值 
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            process.WaitForExit();

            return process.StandardOutput.ReadToEnd();
        }

        public string Download(string remote, string local)
        {
            var cfg = CFService.Config();
            Process process = null;
            string args = string.Format("-m p -a {0} -p {1} -r {2} -l {3}",
                MasterAddr,
                MasterPort,
                remote,
                local);

            var fileName = System.Web.HttpRuntime.AppDomainAppPath + "Exe/meloton";

            process = new Process();
            process.StartInfo.FileName = fileName;
            process.StartInfo.Arguments = args;
            process.StartInfo.UseShellExecute = false;   // 是否使用外壳程序 
            process.StartInfo.CreateNoWindow = true;   //是否在新窗口中启动该进程的值 
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            process.WaitForExit();

            return process.StandardOutput.ReadToEnd();
        }
    }
}