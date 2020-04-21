using omdedaran.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace omdedaran.Controllers
{
    public class MSController : Controller
    {
        // GET: MS

        
        ////////////////////////{ start : Index }////////////////////////1
        public ActionResult Index()
        {
            return View();
        }
        /// /////////////////////{ end : Index }////////////////////////


        ////////////////////////{ start : about }////////////////////////2
      
        public ActionResult about()
        {
            tbl_BLOG_TeamMembers tbt = new tbl_BLOG_TeamMembers();
            

            return View(tbt.Service());
        }
        /// /////////////////////{ end : about }////////////////////////


        ////////////////////////{ start : contact }////////////////////////3
        public ActionResult contact()
        {
            return View();
        }
        /// /////////////////////{ end : contact }////////////////////////



        ////////////////////////{ start : login }////////////////////////4
        public ActionResult login()
        {
            return View();
        }
        /// /////////////////////{ end : login }////////////////////////


        ////////////////////////{ start : blog }////////////////////////5
        public ActionResult blog()
        {
            return View();
        }
        /// /////////////////////{ end : blog }////////////////////////


        ////////////////////////{ start : terms }////////////////////////6
        public ActionResult terms()
        {
            return View();
        }
        /// /////////////////////{ end : terms }////////////////////////
















    }
}