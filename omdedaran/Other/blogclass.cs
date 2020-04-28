using omdedaran.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace omdedaran.Other
{
    public class blogclass
    {
        public IEnumerable<tbl_BLOG> BLOG { set; get; }
        public List<tbl_BLOG> BLOG_Categories { set; get; }
        public List<tbl_BLOG> BLOG_Tags { set; get; }
        public List<tbl_BLOG> TabS1 { set; get; }
        public List<tbl_BLOG> TabS2 { set; get; }
        public  page pages { set; get; }

    }
    public class page {
            
           public string Pvaluepage { set; get; }
           public string PnamePage  { set; get; }

         }
}