using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        String url;

        try
        {
            url = Session["URL_ANTERIOR"].ToString();
        }
        catch
        {
            Session.Clear();
            Session.RemoveAll();
            url = "~/seguridad/login.aspx";
        }

        HyperLink_ANTERIOR_1.NavigateUrl = url;
        HyperLink_ANTERIOR.NavigateUrl = url;
        Button_ANTERIOR.PostBackUrl = url;
    }
}