using DataBaseConnector;
using omdedaran.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace omdedaran.OtherModels
{
    public class DataClass
    {

        ///////////////////////////////////////////////about    TeamMembers
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
        ///////////////////////////////////////////////BLOG     BLOGPost
        public List<tbl_BLOG> BLOG(string NamePage, string Valuepage)
        {
            PDBC db = new PDBC("PandaMarketCMS", true);
            db.Connect();

            List<tbl_BLOG> List_BLG = new List<tbl_BLOG>();
            string query = "";

            query = " SELECT [PandaMarketCMS].[dbo].[tbl_BLOG_Post].[Id], [Title],[Text_min],[Date]  ,[name] as'Cat_Id'";
            query += " ,(SELECT [ad_firstname]FROM [PandaMarketCMS].[dbo].[tbl_ADMIN_main] where[id_Admin]=[PandaMarketCMS].[dbo].[tbl_BLOG_Post].[WrittenBy_AdminId]) as'firstname'  ";
            query += " ,(SELECT [ad_lastname]FROM [PandaMarketCMS].[dbo].[tbl_ADMIN_main] where[id_Admin]=[PandaMarketCMS].[dbo].[tbl_BLOG_Post].[WrittenBy_AdminId]) as'lastname'  ";
            query += " ,(SELECT count([Id])FROM [PandaMarketCMS].[dbo].[tbl_BLOG_Comment]where [PostId] =[PandaMarketCMS].[dbo].[tbl_BLOG_Post].[Id] )as'Comments' ";
            query += " FROM [PandaMarketCMS].[dbo].[tbl_BLOG_Post] ";
            query += " inner join [PandaMarketCMS].[dbo].[tbl_BLOG_Categories] on [PandaMarketCMS].[dbo].[tbl_BLOG_Post].[Cat_Id] = [PandaMarketCMS].[dbo].[tbl_BLOG_Categories].[Id] ";
            query += " where [PandaMarketCMS].[dbo].[tbl_BLOG_Post].[Is_Deleted]  like 0 AND  [PandaMarketCMS].[dbo].[tbl_BLOG_Post].[Is_Disabled] like 0 ";
            query += " And [PandaMarketCMS].[dbo].[tbl_BLOG_Categories].[Is_Deleted]  like 0 AND  [PandaMarketCMS].[dbo].[tbl_BLOG_Categories].[Is_Disabled] like 0 ";




            if (NamePage == "post")
            {
                query += " order by [Date] desc ";
            }
            else if (NamePage == "Categories")
            {
                query += $" And  [name] = N'{Valuepage}' order by [Date] desc ";

            }
            else if (NamePage == "Search")
            {
                query += $"And [PandaMarketCMS].[dbo].[tbl_BLOG_Post].Title like N'%{Valuepage}%' OR [PandaMarketCMS].[dbo].[tbl_BLOG_Post].Text like N'%{Valuepage}%' order by([PandaMarketCMS].[dbo].[tbl_BLOG_Post].weight) DESC,[Date] DESC";
            }
            else if (NamePage == "tag")
            {
                query = BLOG_Tags(Valuepage, true)[0].name;
            }


            /*
                    */

            using (DataTable dt = db.Select(query))
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    tbl_BLOG BLG = new tbl_BLOG();


                    BLG.Id = dt.Rows[i]["Id"].ToString();
                    BLG.Title = dt.Rows[i]["Title"].ToString();
                    BLG.Text_min = dt.Rows[i]["Text_min"].ToString();
                    BLG.Date = dt.Rows[i]["Date"].ToString();
                    BLG.Cat_Id = dt.Rows[i]["Cat_Id"].ToString();
                    BLG.Comments = dt.Rows[i]["Comments"].ToString();
                    BLG.WrittenBy_AdminId = dt.Rows[i]["firstname"].ToString() + " " + dt.Rows[i]["lastname"].ToString();
                    BLG.list_pic = Pic_BLOG(dt.Rows[i]["Id"].ToString());
                    List_BLG.Add(BLG);

                }
            }

            return List_BLG;
        }
        public List<tbl_BLOG> Pic_BLOG(string id)
        {
            List<tbl_BLOG> List_BLG = new List<tbl_BLOG>();
            PDBC db = new PDBC("PandaMarketCMS", true);
            db.Connect();

            string query = "SELECT [PostId],[PicSizeType],[PicAddress],[PicThumbnailAddress]";
            query += "FROM [PandaMarketCMS].[dbo].[tbl_ADMIN_UploadStructure_ImageAddress]inner join [PandaMarketCMS].[dbo].[tbl_BLOG_Pic_Connector]";
            query += " on [PandaMarketCMS].[dbo].[tbl_BLOG_Pic_Connector].PicId =[PandaMarketCMS].[dbo].[tbl_ADMIN_UploadStructure_ImageAddress].PicID ";
            query += $" where PostId ={id}";


            using (DataTable dt = db.Select(query))
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tbl_BLOG BLG = new tbl_BLOG();
                    BLG.PicId = id;
                    BLG.PicSizeType = dt.Rows[i]["PicSizeType"].ToString();
                    BLG.PicAddress = dt.Rows[i]["PicAddress"].ToString();
                    BLG.PicThumbnailAddress = dt.Rows[i]["PicThumbnailAddress"].ToString();
                    List_BLG.Add(BLG);
                }
            }

            return List_BLG;
        }
        public List<tbl_BLOG> BLOG_Categories()
        {
            List<tbl_BLOG> List_BLG = new List<tbl_BLOG>();
            PDBC db = new PDBC("PandaMarketCMS", true);
            db.Connect();

            string query = " SELECT  [name] ,( SELECT count(Id)";
            query += " FROM [PandaMarketCMS].[dbo].[tbl_BLOG_Post]where [Cat_Id] =[PandaMarketCMS].[dbo].[tbl_BLOG_Categories].[Id])as 'count'";
            query += " FROM  [PandaMarketCMS].[dbo].[tbl_BLOG_Categories]";
            query += " where [Is_Deleted]  like 0 AND  [Is_Disabled] like 0 order by id desc ";

            using (DataTable dt = db.Select(query))
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    tbl_BLOG BLG = new tbl_BLOG();

                    BLG.name = dt.Rows[i]["name"].ToString();
                    BLG.count = dt.Rows[i]["count"].ToString();
                    List_BLG.Add(BLG);
                }
            }

            return List_BLG;
        }
        public List<tbl_BLOG> BLOG_Tags(string Valuepage, bool IsVal)
        {
            List<tbl_BLOG> List_BLG = new List<tbl_BLOG>();
            PDBC db = new PDBC("PandaMarketCMS", true);
            db.Connect();
            string query;


            if (IsVal)
            {
                query = "  SELECT [PandaMarketCMS].[dbo].[tbl_BLOG_Post].[Id], [Title],[Text_min],[Date] ,[PandaMarketCMS].[dbo].[tbl_BLOG_Categories].[name] as'Cat_Id'";
                query += "  ,(SELECT [ad_firstname]FROM [PandaMarketCMS].[dbo].[tbl_ADMIN_main] where[id_Admin]=[PandaMarketCMS].[dbo].[tbl_BLOG_Post].[WrittenBy_AdminId]) as'firstname'";
                query += "  ,(SELECT [ad_lastname]FROM [PandaMarketCMS].[dbo].[tbl_ADMIN_main] where[id_Admin]=[PandaMarketCMS].[dbo].[tbl_BLOG_Post].[WrittenBy_AdminId]) as'lastname' ";
                query += "  ,(SELECT count([Id])FROM [PandaMarketCMS].[dbo].[tbl_BLOG_Comment]where [PostId] =[PandaMarketCMS].[dbo].[tbl_BLOG_Post].[Id] )as'Comments' ";
                query += "  FROM [PandaMarketCMS].[dbo].[tbl_BLOG_Post] ";
                query += "  inner join [PandaMarketCMS].[dbo].[tbl_BLOG_TagConnector] on [PandaMarketCMS].[dbo].[tbl_BLOG_TagConnector].[Tag_Id] = [PandaMarketCMS].[dbo].[tbl_BLOG_Post].[Id]";
                query += "  inner join [PandaMarketCMS].[dbo].[tbl_BLOG_Tags] on [PandaMarketCMS].[dbo].[tbl_BLOG_TagConnector].[Tag_Id] = [PandaMarketCMS].[dbo].[tbl_BLOG_Tags].[Id]";
                query += "  inner join [PandaMarketCMS].[dbo].[tbl_BLOG_Categories] on [PandaMarketCMS].[dbo].[tbl_BLOG_Post].[Cat_Id] = [PandaMarketCMS].[dbo].[tbl_BLOG_Categories].[Id] ";
                query += "  where [PandaMarketCMS].[dbo].[tbl_BLOG_Post].[Is_Deleted]  like 0 AND  [PandaMarketCMS].[dbo].[tbl_BLOG_Post].[Is_Disabled] like 0 ";
                query += "  And [PandaMarketCMS].[dbo].[tbl_BLOG_Categories].[Is_Deleted]  like 0 AND  [PandaMarketCMS].[dbo].[tbl_BLOG_Categories].[Is_Disabled] like 0 ";
                query += "  And  [PandaMarketCMS].[dbo].[tbl_BLOG_Tags].[Is_Deleted]  like 0 AND  [PandaMarketCMS].[dbo].[tbl_BLOG_Tags].[Is_Disabled] like 0";
                query += $" And  [PandaMarketCMS].[dbo].[tbl_BLOG_Tags].[name] = N'{Valuepage}' order by [Date] desc ";



                tbl_BLOG BLG = new tbl_BLOG
                {
                    name = query
                };
                List_BLG.Add(BLG);
            }
            else
            {
                if (Valuepage== "مشاهده همه")
                {
                    query = " SELECT [Name] ";
                    query += " FROM [PandaMarketCMS].[dbo].[tbl_BLOG_Tags] ";
                    query += " where [Is_Deleted]  like 0 AND  [Is_Disabled] like 0 order by id desc ";
                    using (DataTable dt = db.Select(query))
                    {

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            tbl_BLOG BLG = new tbl_BLOG();

                            BLG.name = dt.Rows[i]["name"].ToString();

                            List_BLG.Add(BLG);
                        }
                    }
                }
                else
                {

                    query = " SELECT top 10 [Name] ";
                    query += " FROM [PandaMarketCMS].[dbo].[tbl_BLOG_Tags] ";
                    query += " where [Is_Deleted]  like 0 AND  [Is_Disabled] like 0 order by id desc ";
                    using (DataTable dt = db.Select(query))
                    {

                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            tbl_BLOG BLG = new tbl_BLOG();

                            BLG.name = dt.Rows[i]["name"].ToString();

                            List_BLG.Add(BLG);
                        }
                    }
                }

            }
            return List_BLG;
        }
        public List<tbl_BLOG> TabS(string Value)
        {
            PDBC db = new PDBC("PandaMarketCMS", true);
            db.Connect();

            List<tbl_BLOG> List_BLG = new List<tbl_BLOG>();
            string query = "";
            if (Value == "new")
            {
                query = " SELECT top 5 [PandaMarketCMS].[dbo].[tbl_BLOG_Post].[Id], [Title],[Date],[name]";
                query += " ,(SELECT count([Id])FROM [PandaMarketCMS].[dbo].[tbl_BLOG_Comment]where [PostId] =[PandaMarketCMS].[dbo].[tbl_BLOG_Post].[Id] )as'Comments' ";
                query += " FROM [PandaMarketCMS].[dbo].[tbl_BLOG_Post]";
                query += " inner join [PandaMarketCMS].[dbo].[tbl_BLOG_Categories] on [PandaMarketCMS].[dbo].[tbl_BLOG_Post].[Cat_Id] = [PandaMarketCMS].[dbo].[tbl_BLOG_Categories].[Id]";
                query += " where [PandaMarketCMS].[dbo].[tbl_BLOG_Post].[Is_Deleted]  like 0 AND  [PandaMarketCMS].[dbo].[tbl_BLOG_Post].[Is_Disabled] like 0";
                query += " And [PandaMarketCMS].[dbo].[tbl_BLOG_Categories].[Is_Deleted]  like 0 AND  [PandaMarketCMS].[dbo].[tbl_BLOG_Categories].[Is_Disabled] like 0 order by Date desc ";
            }
            else if (Value == "like")
            {
                query = " SELECT top 5 [PandaMarketCMS].[dbo].[tbl_BLOG_Post].[Id], [PandaMarketCMS].[dbo].[tbl_BLOG_Post].[Title],[PandaMarketCMS].[dbo].[tbl_BLOG_Post].[Date],[PandaMarketCMS].[dbo].[tbl_BLOG_Categories].[name] ";
                query += " ,(SELECT count([Id])FROM [PandaMarketCMS].[dbo].[tbl_BLOG_Comment]where [PostId] =[PandaMarketCMS].[dbo].[tbl_BLOG_Post].[Id] )as'Comments' ";
                query += "  FROM [PandaMarketCMS].[dbo].[tbl_BLOG_Post] ";
                query += "  inner join [PandaMarketCMS].[dbo].[tbl_BLOG_Comment]   on [PandaMarketCMS].[dbo].[tbl_BLOG_Post].[Id] = [PandaMarketCMS].[dbo].[tbl_BLOG_Comment].[PostId]";
                query += "  inner join [PandaMarketCMS].[dbo].[tbl_BLOG_Categories] on [PandaMarketCMS].[dbo].[tbl_BLOG_Post].[Cat_Id] = [PandaMarketCMS].[dbo].[tbl_BLOG_Categories].[Id]";
                query += "  where [PandaMarketCMS].[dbo].[tbl_BLOG_Post].[Is_Deleted]  like 0 AND  [PandaMarketCMS].[dbo].[tbl_BLOG_Post].[Is_Disabled] like 0 ";
                query += "  And [PandaMarketCMS].[dbo].[tbl_BLOG_Categories].[Is_Deleted]  like 0 AND  [PandaMarketCMS].[dbo].[tbl_BLOG_Categories].[Is_Disabled] like 0 ";
                query += "  And [tbl_BLOG_Comment].[PostId] = [tbl_BLOG_Post].[Id]";
                query += "  GROUP BY [tbl_BLOG_Post].[Id] , [tbl_BLOG_Post].[Title], [tbl_BLOG_Post].[Date],[tbl_BLOG_Categories].[name] ";
                query += "  HAVING COUNT([PostId]) > 5 order by count([PostId]) desc";
            }


            using (DataTable dt = db.Select(query))
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    tbl_BLOG BLG = new tbl_BLOG();


                    BLG.Id = dt.Rows[i]["Id"].ToString();
                    BLG.Title = dt.Rows[i]["Title"].ToString();
                    BLG.Date = dt.Rows[i]["Date"].ToString();
                    BLG.Comments = dt.Rows[i]["Comments"].ToString();

                    foreach (var item in Pic_BLOG(dt.Rows[i]["Id"].ToString()))
                    {
                        BLG.PicAddress = item.PicAddress;
                    }

                    List_BLG.Add(BLG);

                }
            }

            return List_BLG;
        }

    }
}