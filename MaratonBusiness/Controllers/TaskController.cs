using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaratonBusiness.Controllers
{
    public class TaskController : Controller
    {
        // GET: Task
        public ActionResult index()
        {
            return View();
        }
    }
}