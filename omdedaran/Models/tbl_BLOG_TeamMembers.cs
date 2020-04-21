using DataBaseConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace omdedaran.Models
{
    public class tbl_BLOG_TeamMembers
    {
      public string Id { get; set; }
      public string Name { get; set; }
      public string Job { get; set; }
      public string Tozihat { get; set; }
      public string github { get; set; }
      public string Linkedin { get; set; }
      public string Instagram { get; set; }
      public string ImagePath { get; set; }
      public string ImageValue { get; set; }
    
        //////////////////////////////////

        public List<tbl_BLOG_TeamMembers> Service()
        {
            PDBC db = new PDBC("PandaMarketCMS", true);
            db.Connect();

            List<tbl_BLOG_TeamMembers> TeamMembers = new List<tbl_BLOG_TeamMembers>();


            using (DataTable dt = db.Select("SELECT [Id],[Name],[Job],[Tozihat],[github],[Linkedin],[Instagram],[ImagePath],[ImageValue]FROM [PandaMarketCMS].[dbo].[tbl_BLOG_TeamMembers]"))
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    tbl_BLOG_TeamMembers tbt = new tbl_BLOG_TeamMembers();

                    tbt.Id = dt.Rows[i]["Id"].ToString();
                    tbt.Name = dt.Rows[i]["Name"].ToString();
                    tbt.Job = dt.Rows[i]["Job"].ToString();
                    tbt.ImagePath = dt.Rows[i]["ImagePath"].ToString();
                    tbt.ImageValue = dt.Rows[i]["ImageValue"].ToString();
                    TeamMembers.Add(tbt);

                }

            }


            return TeamMembers;
        }



    }

}