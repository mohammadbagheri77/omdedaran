using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace omdedaran.Models
{
    public class tbl_Customer_Main
    {
        public string id_Customer { set; get; }
        public string C_regDate { set; get; }
        public string C_Mobile { set; get; }
        public string C_FirstName { set; get; }
        public string C_LastNAme { set; get; }
        public string C_Description { set; get; }
        public string C_ISActivate { set; get; }
        public string C_ActivationToken { set; get; }
        public string C_ActivateDate { set; get; }
        public string C_Password { set; get; }
        /////////////////////////////////
        public bool remember_me { set; get; }
    }
}