﻿using System;
using System.Text;

namespace SmartSocietyAPI
{
    public partial class WebForm1 : System.Web.UI.Page
    {

        private string Encryptdata(string password)
        {
            string strmsg = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            strmsg = Convert.ToBase64String(encode);
            return strmsg;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            SmartSocietyAPI.Admin ServiceObject = new SmartSocietyAPI.Admin();
            SmartSocietyAPI.General ServiceObject1 = new SmartSocietyAPI.General();
            Response.Write(Encryptdata("12345"));
            Response.Write(ServiceObject1.ViewGateKeeping(false,"0","0","-1"));
        }
    }
}