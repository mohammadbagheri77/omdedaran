using omdedaran.Models;
using omdedaran.OtherModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using omdedaran.Other;
using DataBaseConnector;

namespace omdedaran.Controllers
{
    public class MSController : Controller
    {
        // GET: MS


        ////////////////////////{ start : Index }////////////////////////1
        ///طراحی نشده
        public ActionResult Index()
        {

            return View();
        }
        /// /////////////////////{ end : Index }////////////////////////


        ////////////////////////{ start : about }////////////////////////2

        public ActionResult about()
        {
            DataClass tbt = new DataClass();


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
        ///مثال 
        ////url = MS/blog?NamePage=post&page=1
        ////url = MS/blog?NamePage=Categories&Valuepage=اخبار پاندایی&page=1
        public ActionResult blog(int page, string NamePage, string Valuepage)
        {
            int recordsPerPage;
            string Pvp = "";
            DataClass tbt = new DataClass();
            var blog = default(IEnumerable<tbl_BLOG>);

            if (Valuepage != null)
            {
                if (NamePage == "tag" && Valuepage == "مشاهده همه")
                {
                    recordsPerPage = 100;
                    blog = tbt.BLOG_Tags(NamePage, false).ToPagedList(page, recordsPerPage);
                }
                else
                {
                    recordsPerPage = 10;
                    blog = tbt.BLOG(NamePage, Valuepage).ToPagedList(page, recordsPerPage);
                }
                Pvp = Valuepage;
            }
            else
            {
                if (NamePage == "tag" && Valuepage == "مشاهده همه")
                {
                    recordsPerPage = 100;
                    blog = tbt.BLOG_Tags(NamePage, false).ToPagedList(page, recordsPerPage);
                }
                else
                {
                    recordsPerPage = 10;
                    blog = tbt.BLOG(NamePage, Valuepage).ToPagedList(page, recordsPerPage);
                }

                Pvp = " ";
            }




            var _blogclass = new blogclass()
            {
                BLOG = blog,
                BLOG_Categories = tbt.BLOG_Categories("", false),
                BLOG_Tags = tbt.BLOG_Tags(" ", false),
                TabS1 = tbt.TabS("new"),
                TabS2 = tbt.TabS("like"),
                pages = new page()
                {
                    PnamePage = NamePage,
                    Pvaluepage = Pvp
                }

            };



            return View(_blogclass);
        }

        /// /////////////////////{ end : blog }////////////////////////
        /// 
        ////////////////////////{ start : blog_post }////////////////////////6
        ////مثال
        ////url = MS/blog_post?IdPage=1
        public ActionResult blog_post(string IdPage)
        {

            DataClass tbt = new DataClass();

            var _blogclass = new blogclass()
            {
                BLOGPOST = tbt.BLOGPOST(IdPage),
                BLOG_Categories = tbt.BLOG_Categories("", false),
                BLOG_Tags = tbt.BLOG_Tags(" ", false),
                TabS1 = tbt.TabS("new"),
                TabS2 = tbt.TabS("like"),


            };

            return View(_blogclass);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult comment_post(tbl_BLOG tbl)
        {

            string query_new;
            string res = " ";


            PDBC db = new PDBC("PandaMarketCMS", true);
            db.Connect();

            List<ExcParameters> paramss = new List<ExcParameters>();
            ExcParameters parameters = new ExcParameters();

            query_new = "INSERT INTO [dbo].[tbl_BLOG_Comment]([Email],[message],[Name],[PostId])VALUES(@Email ,@message ,@Name ,@PostId)";

            parameters = new ExcParameters()
            {
                _KEY = "@Email",
                _VALUE = tbl.Email
            };
            paramss.Add(parameters);
            parameters = new ExcParameters()
            {
                _KEY = "@message",
                _VALUE =tbl.message
            };
            paramss.Add(parameters);
            parameters = new ExcParameters()
            {
                _KEY = "@Name",
                _VALUE =tbl.name
            };
            paramss.Add(parameters);
            parameters = new ExcParameters()
            {
                _KEY = "@PostId",
                _VALUE =tbl.Id
            };
            paramss.Add(parameters);
           
            res = db.Script(query_new, paramss);



            return Redirect("blog_post?IdPage=" + tbl.Id);
        }
        /// /////////////////////{ end : blog_post }////////////////////////

        ////////////////////////{ start : terms }////////////////////////7
        public ActionResult terms()
        {
            return View();
        }
        /// /////////////////////{ end : terms }////////////////////////
















    }
}