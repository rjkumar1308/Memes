using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MemesAPI.Controllers
{
    public class CRUDController : Controller
    {
        // GET: Login
        public ActionResult ReadData()
        {
            return View();
        }
        public ActionResult Options()
        {
            return View();
        }
        public ActionResult AddNewMeme()
        {
            return View();
        }
    }
}