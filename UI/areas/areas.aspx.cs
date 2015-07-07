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
using Brainsbits.LLB;
using System.Configuration;
 
public partial class areas_Default : System.Web.UI.Page
{
    SecureQueryString QueryStringSeguro;

    int anchoBotonMenu = 215;
    int altoBotonMenu = 178;

    private void cargar_informacion_areas_y_modulo()
    {
        Label_NOMBRE_MODULO.Text = "MENÚ PRINCIPAL";
    }

    private void cargar_info_usuario_session()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());


        QueryStringSeguro["img_area"] = "cambiopassword";
        QueryStringSeguro["nombre_area"] = "CAMBIO DE PASSWORD";
        QueryStringSeguro["nombre_modulo"] = "CAMBIO DE PASSWORD";
        QueryStringSeguro["accion"] = "inicial";

        HyperLink_CAMBIAR_PASSWORD.NavigateUrl = "~/seguridad/cambioPassword.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        HyperLink_CAMBIAR_PASSWORD.Target = "_blank";
        HyperLink_CAMBIAR_PASSWORD.Text = "Cambiar Password";
        HyperLink_CAMBIAR_PASSWORD.ToolTip = "Clic aquí para cambiar su password de de acceso al Sistema.";
    }

    private TableCell cargarCelda(int i, int contadorFilas, DataRow filaArea)
    {
        tools _tools = new tools();

        TableCell celdaTabla;
        HyperLink link;
        Image imagen;

        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_" + i.ToString() + "_row_" + contadorFilas.ToString();

        link = new HyperLink();
        link.ID = "link_" + filaArea["NOMBRE_IMAGEN"].ToString().Trim();

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
        QueryStringSeguro["img_area"] = filaArea["NOMBRE_IMAGEN"].ToString().Trim();
        QueryStringSeguro["nombre_area"] = filaArea["NOMBRE"].ToString().Trim();
        if (filaArea["NOMBRE_MODULO_INICIAL"] == DBNull.Value)
        {
            QueryStringSeguro["nombre_modulo"] = "FALTA CONFIGURACIÓN DE MODULOS Y BOTONES";
        }
        else
        {
            QueryStringSeguro["nombre_modulo"] = filaArea["NOMBRE_MODULO_INICIAL"].ToString().Trim().ToUpper();
        }

        QueryStringSeguro["accion"] = "inicial";
        link.NavigateUrl = "~" + filaArea["MODULO_INICIAL"].ToString().Trim() + "?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";

        Boolean AreaHabilitada = true;

        if (ConfigurationManager.AppSettings["habilitarSeguridad"] == "true")
        {
            if (filaArea["AREA_ACTIVA"].ToString().ToUpper() == "FALSE")
            {
                AreaHabilitada = false;
            }
            else
            {
                if (filaArea["SEGURIDAD_HABILITADA"].ToString().ToUpper() == "FALSE")
                {
                    AreaHabilitada = true;
                }
                else
                {
                    if (Convert.ToInt32(filaArea["CONTADOR_PERMISOS"]) <= 0)
                    {
                        AreaHabilitada = false;
                    }
                    else
                    {
                        AreaHabilitada = true;
                    }
                }
            }
        }
        else
        {
            if (filaArea["AREA_ACTIVA"].ToString().ToUpper() == "FALSE")
            {
                AreaHabilitada = false;
            }
            else
            {
                AreaHabilitada = true;
            }
        }

        link.Enabled = AreaHabilitada;

        imagen = new Image();

        if(AreaHabilitada == false)
        {
            imagen.ImageUrl = "~/imagenes/areas/bMenuPrincipal" + filaArea["NOMBRE_IMAGEN"].ToString().Trim() + "Inactivo.png";
        }
        else
        {
            imagen.ImageUrl = "~/imagenes/areas/bMenuPrincipal" + filaArea["NOMBRE_IMAGEN"].ToString().Trim() + "Estandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bMenuPrincipal" + filaArea["NOMBRE_IMAGEN"].ToString().Trim() + "Accion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bMenuPrincipal" + filaArea["NOMBRE_IMAGEN"].ToString().Trim() + "Estandar.png'");
        }

        imagen.CssClass = "botones_menu_principal";
        imagen.Width = anchoBotonMenu;
        imagen.Height = altoBotonMenu;

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        return celdaTabla;
    }

    private void crgar_menu_botones()
    {
        String USU_LOG = Session["USU_LOG"].ToString();

        tools _tools = new tools();

        seguridad _seguridad = new seguridad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaAreas = _seguridad.ObtenerAreaTodasConContadorPermisos(USU_LOG);

        if (tablaAreas.Rows.Count <= 0)
        {
            Session.RemoveAll();
            Response.Redirect("~/Seguridad/Login.aspx");
        }
        else
        {

            DataRow filaArea;

            TableRow filaTabla = new TableRow();

            int contadorFilas = 0;

            for (int i = 0; i < tablaAreas.Rows.Count; i++)
            {
                filaArea = tablaAreas.Rows[i];

                if ((i >= 0) && (i < 4))
                {
                    if (i == 0)
                    {
                        contadorFilas = 0;

                        filaTabla = new TableRow();
                        filaTabla.ID = "row_" + contadorFilas.ToString();
                    }

                    filaTabla.Cells.Add(cargarCelda(i, contadorFilas, filaArea));

                    if ((i == 3) || (i == (tablaAreas.Rows.Count - 1)))
                    {
                        Table_MENU.Rows.Add(filaTabla);
                    }
                }

                if ((i >= 4) && (i < 8))
                {
                    if (i == 4)
                    {
                        contadorFilas += 1;

                        filaTabla = new TableRow();
                        filaTabla.ID = "row_" + contadorFilas.ToString();
                    }

                    filaTabla.Cells.Add(cargarCelda(i, contadorFilas, filaArea));

                    if ((i == 7) || (i == (tablaAreas.Rows.Count - 1)))
                    {
                        Table_MENU_1.Rows.Add(filaTabla);
                    }
                }

                if ((i >= 8) && (i < 12))
                {
                    if (i == 8)
                    {
                        contadorFilas += 1;

                        filaTabla = new TableRow();
                        filaTabla.ID = "row_" + contadorFilas.ToString();
                    }

                    filaTabla.Cells.Add(cargarCelda(i, contadorFilas, filaArea));

                    if ((i == 11) || (i == (tablaAreas.Rows.Count - 1)))
                    {
                        Table_MENU_2.Rows.Add(filaTabla);
                    }
                }

                if ((i >= 12) && (i < 16))
                {
                    if (i == 12)
                    {
                        contadorFilas += 1;

                        filaTabla = new TableRow();
                        filaTabla.ID = "row_" + contadorFilas.ToString();
                    }

                    filaTabla.Cells.Add(cargarCelda(i, contadorFilas, filaArea));

                    if ((i == 15) || (i == (tablaAreas.Rows.Count - 1)))
                    {
                        Table_MENU_3.Rows.Add(filaTabla);
                    }
                }
            }
        }
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
        maestrasInterfaz _maestrasInterfaz = new maestrasInterfaz();

        if (_maestrasInterfaz.verificarSessionesSeguridad() == false)
        {
            Response.Redirect("~/seguridad/login.aspx");
        }
        else
        {
            cargar_informacion_areas_y_modulo();

            if (IsPostBack == false)
            {
                cargar_info_usuario_session();

                crgar_menu_botones();

                Session["URL_ANTERIOR"] = HttpContext.Current.Request.RawUrl;
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