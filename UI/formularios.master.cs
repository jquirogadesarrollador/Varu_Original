using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using TSHAK.Components;

using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;

public partial class formularios : System.Web.UI.MasterPage
{
    SecureQueryString QueryStringSeguro;

    private void cargar_info_usuario_session()
    {
        tools _tools = new tools();
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = "cambiopassword";
        QueryStringSeguro["nombre_area"] = "CAMBIO DE PASSWORD";
        QueryStringSeguro["nombre_modulo"] = "CAMBIO DE PASSWORD";
        QueryStringSeguro["accion"] = "inicial";

        HyperLink_CAMBIAR_PASSWORD.NavigateUrl = "~/seguridad/cambioPassword.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        HyperLink_CAMBIAR_PASSWORD.Target = "_blank";
        HyperLink_CAMBIAR_PASSWORD.Text = "Cambiar Password";
        HyperLink_CAMBIAR_PASSWORD.ToolTip = "Clic aquí para cambiar su password de de acceso al Sistema.";


    }

    protected void Page_Init(object sender, EventArgs e) 
    {
        maestrasInterfaz _maestrasInterfaz = new maestrasInterfaz();

        if (_maestrasInterfaz.verificarSessionesSeguridad() == false)
        {
            Session.Add("SESSION_CADUCADA", "True");

            Response.Redirect("~/seguridad/login.aspx");
        }
        else
        {
            if (Session["USU_TIPO"].ToString() == "PUBLICO")
            {
                Session.RemoveAll();

                Response.Redirect("~/seguridad/login.aspx");
            }
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            maestrasInterfaz _maestrasInterfaz = new maestrasInterfaz();

            if (_maestrasInterfaz.verificarSessionesSeguridad() == true)
            {

                //cargamos informacion del usuario conectado
                cargar_info_usuario_session();

                tools _tools = new tools();
                QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

                String img_area = QueryStringSeguro["img_area"].ToString();
                String nombre_area = QueryStringSeguro["nombre_area"].ToString();
                String nombre_modulo = QueryStringSeguro["nombre_modulo"].ToString();
                Label_NOMBRE_MODULO.Text = nombre_modulo;
            }
            else
            {
                Response.Redirect("~/seguridad/login.aspx");
            }
        }
    }

    protected void LinkButton_CERRAR_SESION_Click(object sender, EventArgs e)
    {
        Session.Clear();
        Session.RemoveAll();
        Session.Abandon();

        Response.Redirect("~/seguridad/login.aspx");
    }
}
