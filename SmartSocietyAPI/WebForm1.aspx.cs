﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SmartSocietyAPI
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SmartSocietyAPI.General ServiceObject = new SmartSocietyAPI.General();
            Response.Write(ServiceObject.GetData(1));
        }
    }
}