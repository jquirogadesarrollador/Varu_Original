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

    private String NOMBRE_AREA = tabla.NOMBRE_AREA_CONTRATACION;

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

        QueryStringSeguro["img_area"] = "contratacion";
        QueryStringSeguro["nombre_area"] = "CONTRATOS Y RELACIONES LABORALES";
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
        link.ID = "link_ARP";
        QueryStringSeguro["nombre_modulo"] = "ADMINISTRADORA DE RIESGOS LABORALES";
        link.NavigateUrl = "~/contratacion/arl.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bARLEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bARLAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bARLEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);






        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_2_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_EPS";
        QueryStringSeguro["nombre_modulo"] = "ENTIDAD PROMOTORA DE SALUD";
        link.NavigateUrl = "~/contratacion/eps.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bEPSEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bEPSAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bEPSEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);





        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_3_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_AFP";
        QueryStringSeguro["nombre_modulo"] = "ADMINISTRADORAS DE FONDOS DE PENSIONES";
        link.NavigateUrl = "~/contratacion/afp.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bAFPEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bAFPAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bAFPEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);






        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_4_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_CCF";
        QueryStringSeguro["nombre_modulo"] = "CAJA DE COMPENSACIÓN FAMILIAR";
        link.NavigateUrl = "~/contratacion/ccf.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bCCFEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bCCFAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bCCFEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);








        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_5_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_proveedores_examenes";
        QueryStringSeguro["nombre_modulo"] = "PROVEEDORES DE EXÁMENES MÉDICOS";
        link.NavigateUrl = "~/contratacion/ExamenesLaboratorio.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bExamenesLaboratorioEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bExamenesLaboratorioAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bExamenesLaboratorioEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);

        Table_MENU.Rows.Add(filaTabla);



        contadorFilas = 0;

        filaTabla = new TableRow();
        filaTabla.ID = "M1_row_" + contadorFilas.ToString();





        celdaTabla = new TableCell();
        celdaTabla.ID = "M1_cell_2_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_lista_documentos_entregables";
        QueryStringSeguro["nombre_modulo"] = "LISTA DE DOCUMENTOS ENTREGABLES AL TRABAJADOR";
        link.NavigateUrl = "~/contratacion/listaDocumentosEntregables.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bListaDocumentosETrabajadorEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bListaDocumentosETrabajadorAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bListaDocumentosETrabajadorEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);





        celdaTabla = new TableCell();
        celdaTabla.ID = "M1_cell3_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_motivos_retiro";
        QueryStringSeguro["nombre_modulo"] = "CONFIGURACIÓN MOTIVOS DE ROTACIÓN Y RETIRO";
        link.NavigateUrl = "~/contratacion/adminRotacionRetiro.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bAdminMotivosRotacionRetiroEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bAdminMotivosRotacionRetiroAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bAdminMotivosRotacionRetiroEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);


        
        Table_MENU_1.Rows.Add(filaTabla);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = "ADMINISTRACION";
        if (IsPostBack == false)
        {
            cargar_menu_botones_internos();
        }
    }
}