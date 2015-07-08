using System;
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

public partial class _RadicacionHojaVida : System.Web.UI.Page
{
    private String NOMBRE_AREA = tabla.NOMBRE_AREA_SELECCION;
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

    private enum Acciones
    { 
        Inicio = 0
    }

    private void cargar_menu_botones_internos()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = "seleccion";
        QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
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
        link.ID = "link_radicacion_masiva";
        QueryStringSeguro["nombre_modulo"] = "RADICACIÓN MASIVA DE SOLICITUD DE INGRESO";
        link.NavigateUrl = "~/seleccion/radicacionMasiva.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bRadicacionMasivaEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bRadicacionMasivaAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bRadicacionMasivaEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);





        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_2_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_radicacion_manual";
        QueryStringSeguro["nombre_modulo"] = "RADICACIÓN MANUAL DE SOLICITUD DE INGRESO";
        link.NavigateUrl = "~/seleccion/solicitudIngreso.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bRadicacionManualEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bRadicacionManualAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bRadicacionManualEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);




        Table_MENU.Rows.Add(filaTabla);
    }

    private void Configurar()
    { 
    
    }

    private void Ocultar(Acciones accion)
    {
        switch (accion)
        { 
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = false;
                Panel_BOTONES_INTERNOS.Visible = false;
                break;
        }
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = true;
                Panel_BOTONES_INTERNOS.Visible = true;
                break;
        }
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                cargar_menu_botones_internos();

                Label_INFO_ADICIONAL_MODULO.Text = "Seleccione La Metodología Para Realizar La Radicación.";
                break;
        }
    }

    private void Iniciar()
    {
        Configurar();
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = "RADICACIÓN DE HOJAS DE VIDA";

        if (IsPostBack == false)
        {
            Iniciar();
        }
    }
}