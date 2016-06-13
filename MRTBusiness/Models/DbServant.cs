using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MRTBusiness.Models
{
    public class DbServant : DbModel
    {
        public string id { get; set; }

        public int state { get; set; }

        public int cpu { get; set; }

        public int memory { get; set; }

        public int type { get; set; }
        /// <summary>
        /// 内核数
        /// </summary>
        public int sysinfo_cpu_num { get; set; }
        /// <summary>
        /// 系统占用率 %
        /// </summary>
        public float sysinfo_cpu_sys { get; set; }
        /// <summary>
        /// 用户占用率 %
        /// </summary>
        public float sysinfo_cpu_user { get; set; }
        /// <summary>
        /// 15分钟内平均负载 %
        /// </summary>
        public float sysinfo_load_15min { get; set; }
        /// <summary>
        /// 1分钟内平均负载 %
        /// </summary>
        public float sysinfo_load_1min { get; set; }
        /// <summary>
        /// 5分钟内平均负载 %
        /// </summary>
        public float sysinfo_load_5min { get; set; }
        /// <summary>
        /// 总内存数，单位MB
        /// </summary>
        public float sysinfo_mem_total { get; set; }
        /// <summary>
        /// 实际可用内存数，单位MB
        /// </summary>
        public float sysinfo_mem_uesed { get; set; }
    }
}