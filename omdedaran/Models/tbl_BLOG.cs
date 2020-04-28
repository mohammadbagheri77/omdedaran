﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace omdedaran.Models
{
    public class tbl_BLOG
    {
        ///////////////////////////////////BLOG_Post
        public string Id { get; set; }
        public string Title { get; set; }
        public string Text_min { get; set; }
        public string Text { get; set; }
        public string WrittenBy_AdminId { get; set; }
        public string Date { get; set; }
        public string weight { get; set; }
        public string IsImportant { get; set; }
        public string Is_Deleted { get; set; }
        public string Is_Disabled { get; set; }
        public string Cat_Id { get; set; }
        public string GroupId { get; set; }
        public string TypeId { get; set; }
        public string Comments { get; set; }

        /////////////////////////////////////BLOG_Categories 
        public string name { get; set; }
        public string count { get; set; }
        /////////////////////////////////////tbl_BLOG_Pic_Connector
        public string PostId { get; set; }
        public string PicId { get; set; }
        /////////////////////////////////////tbl_ADMIN_UploadStructure_ImageAddress
        public string PicSizeType { get; set; }
        public string PicAddress { get; set; }
        public string PicThumbnailAddress { get; set; }
        /////////////////////////////////////////////
        public List<tbl_BLOG> list_pic { get; set; }
      
    }
}