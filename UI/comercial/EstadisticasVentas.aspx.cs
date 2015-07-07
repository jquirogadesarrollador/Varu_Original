using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Brainsbits.LLB.maestras;

using TSHAK.Components;
using Brainsbits.LLB.seguridad;
using System.Data;
using Brainsbits.LLB;

public partial class _Default : System.Web.UI.Page
{

    private String NOMBRE_AREA = tabla.NOMBRE_AREA_GESTION_COMERCIAL;

    private void RolPermisos()
    {
        #region variables
        int contadorPermisos = 0;
        #endregion variables

        seguridad _seguridad = new seguridad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        tools _tools = new tools();

        String rutaScript = _tools.obtenerRutaVerdaderaScript(Request.ServerVariables["SCRIPT_NAME"]);

        DataTable tablaInformacionPermisos = _seguridad.ObtenerPermisosBotones(NOMBRE_AREA, rutaScript);

        maestrasInterfaz _maestrasInterfaz = new maestrasInterfaz();

        contadorPermisos = _maestrasInterfaz.RolPermisos(this, tablaInformacionPermisos);

        if (contadorPermisos <= 0)
        {
            SecureQueryString QueryStringSeguro;
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

            QueryStringSeguro["img_area"] = "restringido";
            QueryStringSeguro["nombre_area"] = "ACCESO RESTRINGIDO";
            QueryStringSeguro["nombre_modulo"] = "ACCESO RESTRINGIDO";
            QueryStringSeguro["accion"] = "inicial";

            Response.Redirect("~/sinPermisos/sinPermisos.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
        }
        else
        {
            Session["URL_ANTERIOR"] = HttpContext.Current.Request.RawUrl;
        }
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        RolPermisos();
    }

    private void cargar_menu_botones_internos()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = "comercial";
        QueryStringSeguro["nombre_area"] = "COMERCIAL";
        QueryStringSeguro["accion"] = "inicial";

        TableRow filaTabla;
        TableCell celdaTabla;
        HyperLink link;
        Image imagen;

        int contadorFilas = 0;

        filaTabla = new TableRow();
        filaTabla.ID = "row_" + contadorFilas.ToString();

        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_1_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_adminsitracion_presupuesto";
        QueryStringSeguro["nombre_modulo"] = "ADMINSITRACIÓN PRESUPUESTO";
        link.NavigateUrl = "~/comercial/Presupuestos.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bPresupuestoComercialEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bPresupuestoComercialAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bPresupuestoComercialEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);



        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_2_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_comportamiento_ventas";
        QueryStringSeguro["nombre_modulo"] = "COMPORTAMIENTO VENTAS";
        link.NavigateUrl = "~/comercial/comportamientoVenta.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bComportamientoVentasEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bComportamientoVentasAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bComportamientoVentasEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);

        
        
        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_3_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_AIU";
        QueryStringSeguro["nombre_modulo"] = "AIU";
        link.NavigateUrl = "~/comercial/VentasAUI.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bAIUEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bAIUAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bAIUEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);


        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_4_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_Cumplimiento_Ventas";
        QueryStringSeguro["nombre_modulo"] = "CUMPLIMIENTO DE VENTAS";
        link.NavigateUrl = "~/comercial/Cumplimiento.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bCumplimientoVentasEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bCumplimientoVentasAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bCumplimientoVentasEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);



        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_5_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_Comparativo_Ventas";
        QueryStringSeguro["nombre_modulo"] = "COMPARATIVO DE VENTAS";
        link.NavigateUrl = "~/comercial/ComparativoVentas.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bComparativoVentasEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bComparativoVentasAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bComparativoVentasEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);




        Table_MENU.Rows.Add(filaTabla);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            this.Title = "ESTADÍSTICAS DE VENTAS";
            cargar_menu_botones_internos();
        }
    }
}