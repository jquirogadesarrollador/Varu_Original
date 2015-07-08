using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Microsoft.Reporting.WebForms;

using Brainsbits.LLB.maestras;

using TSHAK.Components;
using Brainsbits.LLB.seguridad;
using System.Data;
using Brainsbits.LLB;

public partial class _Default : System.Web.UI.Page
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
        link.ID = "link_acoset";
        QueryStringSeguro["nombre_modulo"] = "REGISTRO ACOSET";
        link.NavigateUrl = "~/seleccion/acoset.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bRegistroAcosetEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bRegistroAcosetAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bRegistroAcosetEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);






        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_2_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_reclutamiento";
        QueryStringSeguro["nombre_modulo"] = "FUENTES DE RECLUTAMIENTO";
        link.NavigateUrl = "~/seleccion/fuentesReclutamiento.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bFuentesReclutamientoEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bFuentesReclutamientoAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bFuentesReclutamientoEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);






        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_3_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_categoria_pruebas";
        QueryStringSeguro["nombre_modulo"] = "CATEGORÍA DE PRUEBAS Y PRUEBAS";
        link.NavigateUrl = "~/seleccion/categoriasyPruebas.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bCategoriaPruebasEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bCategoriaPruebasAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bCategoriaPruebasEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);






        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_4_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_cargos_trabajo";
        QueryStringSeguro["nombre_modulo"] = "CARGOS DE TRABAJO";
        link.NavigateUrl = "~/seleccion/cargos.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bCargosTrabajoEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bCargosTrabajoAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bCargosTrabajoEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);





        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_5_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_administracion_documentos";
        QueryStringSeguro["nombre_modulo"] = "DOCUMENTACIÓN PARA VALIDACIÓN DE PROCESO DE CONTRATACIÓN";
        link.NavigateUrl = "~/seleccion/Documentos.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bAdministracionDocumentosEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bAdministracionDocumentosAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bAdministracionDocumentosEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);



        Table_MENU.Rows.Add(filaTabla);





        contadorFilas = 0;

        filaTabla = new TableRow();
        filaTabla.ID = "TM1_row_" + contadorFilas.ToString();



        celdaTabla = new TableCell();
        celdaTabla.ID = "TM1_cell_1_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_derogaciones";
        QueryStringSeguro["nombre_modulo"] = "DEROGACIONES";
        link.NavigateUrl = "~/seleccion/derogaciones.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bDerogacionesEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bDerogacionesAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bDerogacionesEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);






        celdaTabla = new TableCell();
        celdaTabla.ID = "TM1_cell_2_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_preguntas_referencia_loboral";
        QueryStringSeguro["nombre_modulo"] = "PREGUNTAS PARA REFERENCIA LABORAL";
        link.NavigateUrl = "~/seleccion/preguntasRefLaboral.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bPreguntasReferenciaLaboralEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bPreguntasReferenciaLaboralAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bPreguntasReferenciaLaboralEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);






        celdaTabla = new TableCell();
        celdaTabla.ID = "TM1_cell_3_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_diccionario_competencias";
        QueryStringSeguro["nombre_modulo"] = "DICCIONARIO DE COMPETENCIAS Y HABILIDADES";
        link.NavigateUrl = "~/seleccion/diccionarioCompetencias.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bDiccionarioCompetenciasEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bDiccionarioCompetenciasAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bDiccionarioCompetenciasEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);






        celdaTabla = new TableCell();
        celdaTabla.ID = "TM1_cell_4_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_fabrica_assesment";
        QueryStringSeguro["nombre_modulo"] = "FABRICA DE ASSESMENT CENTER";
        link.NavigateUrl = "~/seleccion/FabricaAssesment.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bFabricaAssesmentEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bFabricaAssesmentAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bFabricaAssesmentEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);




        Table_MENU_1.Rows.Add(filaTabla);
    }

    private void cargar_reporte_defecto()
    {




    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = "ADMINISTRACIÓN";
        if (IsPostBack == false)
        {
            cargar_menu_botones_internos();

            cargar_reporte_defecto();
        }
    }
}