using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using Brainsbits.LLB.seleccion;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB;

using TSHAK.Components;
using Brainsbits.LLB.seguridad;

public partial class _Default : System.Web.UI.Page
{
    private String NOMBRE_AREA = null;
    private void RolPermisos()
    {
        #region variables
        int contadorPermisos = 0;
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        #endregion variables

        seguridad _seguridad = new seguridad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        String rutaScript = _tools.obtenerRutaVerdaderaScript(Request.ServerVariables["SCRIPT_NAME"]);

        NOMBRE_AREA = _tools.ObtenerTablaNombreArea(proceso);

        DataTable tablaInformacionPermisos = _seguridad.ObtenerPermisosBotones(NOMBRE_AREA, rutaScript);

        maestrasInterfaz _maestrasInterfaz = new maestrasInterfaz();

        contadorPermisos = _maestrasInterfaz.RolPermisos(this, tablaInformacionPermisos);

        if (contadorPermisos <= 0)
        {
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

    private String img_area = "operaciones";
    private String nombre_area = "OPERACIONES";
    private Int32 proceso = (int)tabla.proceso.ContactoOperaciones;
    private String tipoMotivoCancelacion = "CANCELACION";
    private String tipoMotivoReprogramacion = "REPROGRAMACION";
    private String page_header = "ADMINISTRACIÓN -OPERACIONES-";

    private enum Acciones
    { 
        Inicio = 0
    }

    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_BOTONES_INTERNOS.Visible = false;
                break;
        }
    }

    private void Mostrar(Acciones accion)
    { 
        switch(accion)
        {
            case Acciones.Inicio:
                Panel_BOTONES_INTERNOS.Visible = true;
                break;
        }
    }

    private void cargar_menu_botones_modulos_internos()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = img_area;
        QueryStringSeguro["nombre_area"] = nombre_area;
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
        link.ID = "link_sub_programas";
        QueryStringSeguro["nombre_modulo"] = "ADMINISTRACIÓN DE SUB PROGRAMAS";
        QueryStringSeguro["proceso"] = proceso.ToString();
        link.NavigateUrl = "~/maestros/subProgramasRseGlobal.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bAdminSubProgramasEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bAdminSubProgramasAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bAdminSubProgramasEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);





        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_2_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_actividades";
        QueryStringSeguro["nombre_modulo"] = "ADMINISTRACIÓN DE ACTIVIDADES";
        QueryStringSeguro["proceso"] = proceso.ToString();
        link.NavigateUrl = "~/maestros/actividadesRseGlobal.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bAdminActividadesEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bAdminActividadesAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bAdminActividadesEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);






        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_3_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_entidades_colaboradoras";
        QueryStringSeguro["nombre_modulo"] = "ADMINISTRACIÓN DE ENTIDADES COLABORADORAS";
        QueryStringSeguro["proceso"] = proceso.ToString();
        link.NavigateUrl = "~/maestros/entidadesColaboradoras.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bAdminEntidadesColaboradorasEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bAdminEntidadesColaboradorasAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bAdminEntidadesColaboradorasEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);





        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_4_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_motivos_cancelacion";
        QueryStringSeguro["nombre_modulo"] = "MOTIVOS DE CANCELACIÓN DE ACTIVIDADES";
        QueryStringSeguro["proceso"] = proceso.ToString();
        QueryStringSeguro["tipo"] = tipoMotivoCancelacion;
        link.NavigateUrl = "~/maestros/AdminMotivosActProgComp.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bAdminMotivosCancelacionEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bAdminMotivosCancelacionAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bAdminMotivosCancelacionEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);







        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_5_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_motivos_reprogramacion";
        QueryStringSeguro["nombre_modulo"] = "MOTIVOS DE REPROGRAMACIÓN DE ACTIVIDADES";
        QueryStringSeguro["proceso"] = proceso.ToString();
        QueryStringSeguro["tipo"] = tipoMotivoReprogramacion;
        link.NavigateUrl = "~/maestros/AdminMotivosActProgComp.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bAdminMotivosReprogramacionEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bAdminMotivosReprogramacionAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bAdminMotivosReprogramacionEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);

        Table_MENU.Rows.Add(filaTabla);




        contadorFilas = 1;

        filaTabla = new TableRow();
        filaTabla.ID = "_M1_row_" + contadorFilas.ToString();




        celdaTabla = new TableCell();
        celdaTabla.ID = "M1_cell_1_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_tipos_actividad";
        QueryStringSeguro["nombre_modulo"] = "TIPOS DE ACTIVIDAD";
        QueryStringSeguro["proceso"] = proceso.ToString();
        link.NavigateUrl = "~/maestros/TiposActividad.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bTiposActividadEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bTiposActividadAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bTiposActividadEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);



        Table_MENU_1.Rows.Add(filaTabla);
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                cargar_menu_botones_modulos_internos();
                break;
        }
    }

    private void Iniciar()
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = page_header;

        if (IsPostBack == false)
        {
            Iniciar();
        }
    }
}