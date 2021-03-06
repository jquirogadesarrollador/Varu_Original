﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Brainsbits.LLB.maestras;

using TSHAK.Components;
using Brainsbits.LLB;
using Brainsbits.LLB.seguridad;
using System.Data;

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
        link.ID = "link_grupos_empresariales";
        QueryStringSeguro["nombre_modulo"] = "GRUPOS EMPRESARIALES";
        link.NavigateUrl = "~/comercial/GruposEmpresariales.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bGruposEmpresarialesEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bGruposEmpresarialesAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bGruposEmpresarialesEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);






        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_2_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_ciiu";
        QueryStringSeguro["nombre_modulo"] = "ACTIVIDADES ECONÓMICAS (CIIU)";
        link.NavigateUrl = "~/comercial/actividadesCIIU.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bCIIUEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bCIIUAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bCIIUEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);


        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_3_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_ciiu";
        QueryStringSeguro["nombre_modulo"] = "PERMISOS MANUAL DEL CLIENTE";
        link.NavigateUrl = "~/Operaciones/Permisos.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bPermisosManualDelClienteEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bPermisosManualDelClienteAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bPermisosManualDelClienteEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);



        celdaTabla = new TableCell();
        celdaTabla.ID = "t1_cell_2_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_Informacion_basica_comercial";
        QueryStringSeguro["nombre_modulo"] = "Información Basica Comercial";
        link.NavigateUrl = "~/comercial/Informacion_Basica_comercial.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bInformacionBasicaComercial.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bInformacionBasicaComercialAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bInformacionBasicaComercial.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);


        Table_MENU.Rows.Add(filaTabla);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            this.Title = "ADMINISTRACIÓN COMERCIAL";
            cargar_menu_botones_internos();
        }
    }
}