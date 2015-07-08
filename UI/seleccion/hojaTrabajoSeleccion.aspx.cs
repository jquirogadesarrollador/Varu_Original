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
using Brainsbits.LLB.programasRseGlobal;

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

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        if (String.IsNullOrEmpty(QueryStringSeguro["proceso"]) == true)
        {
            NOMBRE_AREA = tabla.NOMBRE_AREA_SELECCION;
        }
        else
        {
            int proceso = Convert.ToInt32(QueryStringSeguro["proceso"]);
            NOMBRE_AREA = _tools.ObtenerTablaNombreArea(proceso);
        }

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

    private System.Drawing.Color colorNo = System.Drawing.ColorTranslator.FromHtml("#F8523A");
    private System.Drawing.Color colorMedio = System.Drawing.ColorTranslator.FromHtml("#FFF200");
    private System.Drawing.Color colorSi = System.Drawing.ColorTranslator.FromHtml("#50CE04");

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }

    private void Informar(Panel panel_fondo, System.Web.UI.WebControls.Image imagen_mensaje, Panel panel_mensaje, Label label_mensaje, String mensaje, Proceso proceso)
    {
        panel_fondo.Style.Add("display", "block");
        panel_mensaje.Style.Add("display", "block");

        label_mensaje.Font.Bold = true;

        switch (proceso)
        {
            case Proceso.Correcto:
                label_mensaje.ForeColor = System.Drawing.Color.Green;
                imagen_mensaje.ImageUrl = "~/imagenes/plantilla/ok_popup.png";
                break;
            case Proceso.Error:
                label_mensaje.ForeColor = System.Drawing.Color.Red;
                imagen_mensaje.ImageUrl = "~/imagenes/plantilla/error_popup.png";
                break;
            case Proceso.Advertencia:
                label_mensaje.ForeColor = System.Drawing.Color.Orange;
                imagen_mensaje.ImageUrl = "~/imagenes/plantilla/advertencia_popup.png";
                break;
        }

        panel_fondo.Visible = true;
        panel_mensaje.Visible = true;


        label_mensaje.Text = mensaje;
    }

    #region CARGAR DROP Y DRIDS
    private void cargar_GridView_HOJA_DE_TRABAJO()
    {
        GridView_HOJA_DE_TRABAJO.PageIndex = Convert.ToInt32(HiddenField_PAGINA_GRID.Value);


        requisicion _requisicion = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaRequisicionesHojaTrabajoOriginal = new DataTable();

        if (HiddenField_TIPO_BUSQUEDA_ACTUAL.Value == "SIN_FILTRO")
        {
            tablaRequisicionesHojaTrabajoOriginal = _requisicion.ObtenerTablaRequerimientosUsuario();
        }
        else
        {
            if (HiddenField_FILTRO_DROP.Value == "ID_REGIONAL")
            {
                tablaRequisicionesHojaTrabajoOriginal = _requisicion.ObtenerComRequerimientoPorUsuLogFiltroRegional(HiddenField_FILTRO_DATO.Value);
            }
            else
            {
                if (HiddenField_FILTRO_DROP.Value == "ID_CIUDAD")
                {
                    tablaRequisicionesHojaTrabajoOriginal = _requisicion.ObtenerComRequerimientoPorUsuLogFiltroCiudad(HiddenField_FILTRO_DATO.Value);
                }
                else
                {
                    if (HiddenField_FILTRO_DROP.Value == "ID_CLIENTE")
                    {
                        tablaRequisicionesHojaTrabajoOriginal = _requisicion.ObtenerComRequerimientoPorUsuLogFiltroCliente(HiddenField_FILTRO_DATO.Value);
                    }
                    else
                    {
                        if (HiddenField_FILTRO_DROP.Value == "ID_REQUERIMIENTO")
                        {
                            tablaRequisicionesHojaTrabajoOriginal = _requisicion.ObtenerComRequerimientoPorUsuLogFiltroREQ(HiddenField_FILTRO_DATO.Value);
                        }
                    }
                }
            }
        }

        tablaRequisicionesHojaTrabajoOriginal.AcceptChanges();

        DataRow filaTablaRequisicionesCreada = tablaRequisicionesHojaTrabajoOriginal.NewRow();

        filaTablaRequisicionesCreada["NUMERACION"] = DBNull.Value;
        filaTablaRequisicionesCreada["ID_REQUERIMIENTO"] = DBNull.Value;
        filaTablaRequisicionesCreada["COD_EMPRESA"] = DBNull.Value;
        filaTablaRequisicionesCreada["RAZ_SOCIAL"] = DBNull.Value;
        filaTablaRequisicionesCreada["TIPO_REQ"] = DBNull.Value;
        filaTablaRequisicionesCreada["FECHA_REQUERIDA"] = DBNull.Value;
        filaTablaRequisicionesCreada["COD_OCUPACION"] = DBNull.Value;
        filaTablaRequisicionesCreada["NOM_OCUPACION"] = DBNull.Value;
        filaTablaRequisicionesCreada["CANTIDAD"] = tablaRequisicionesHojaTrabajoOriginal.Compute("SUM(CANTIDAD)",null);
        filaTablaRequisicionesCreada["ENVIADOS"] = tablaRequisicionesHojaTrabajoOriginal.Compute("SUM(ENVIADOS)", null);
        filaTablaRequisicionesCreada["CONTRATAR"] = tablaRequisicionesHojaTrabajoOriginal.Compute("SUM(CONTRATAR)", null);
        filaTablaRequisicionesCreada["FALTAN"] = tablaRequisicionesHojaTrabajoOriginal.Compute("SUM(FALTAN)", null);
        filaTablaRequisicionesCreada["ALERTA"] = "NINGUNA";
        filaTablaRequisicionesCreada["FECHA_R"] = DBNull.Value;
        filaTablaRequisicionesCreada["ESTADO"] = DBNull.Value;
        filaTablaRequisicionesCreada["CUMPLIDO"] = DBNull.Value;
        filaTablaRequisicionesCreada["CANCELADO"] = DBNull.Value;
        filaTablaRequisicionesCreada["USU_CRE"] = DBNull.Value;

        tablaRequisicionesHojaTrabajoOriginal.Rows.Add(filaTablaRequisicionesCreada);

        tablaRequisicionesHojaTrabajoOriginal.AcceptChanges();

        GridView_HOJA_DE_TRABAJO.DataSource = tablaRequisicionesHojaTrabajoOriginal;
        GridView_HOJA_DE_TRABAJO.DataBind();
        
        DataRow filaParaColocarColor;
        int contadorAlertasBajas = 0;
        int contadorAlertasMedias = 0;
        int contadorAlertasAltas = 0;
        int contadorAlertasNinguna = 0;

        for (int i = 0; i < tablaRequisicionesHojaTrabajoOriginal.Rows.Count; i++)
        {
            filaParaColocarColor = tablaRequisicionesHojaTrabajoOriginal.Rows[i];

            if (filaParaColocarColor["ALERTA"].ToString().Trim() == "ALTA")
            {
                GridView_HOJA_DE_TRABAJO.Rows[i].BackColor = colorNo;
                contadorAlertasAltas += 1;
            }
            else
            {
                if (filaParaColocarColor["ALERTA"].ToString().Trim() == "MEDIA")
                {
                    GridView_HOJA_DE_TRABAJO.Rows[i].BackColor = colorMedio;
                    contadorAlertasMedias += 1;
                }
                else
                {
                    if (filaParaColocarColor["ALERTA"].ToString().Trim() == "BAJA")
                    {
                        GridView_HOJA_DE_TRABAJO.Rows[i].BackColor = colorSi;
                        contadorAlertasBajas += 1;
                    }
                    else
                    {
                        GridView_HOJA_DE_TRABAJO.Rows[i].BackColor = System.Drawing.Color.Gray;
                        contadorAlertasNinguna += 1;
                    }
                }
            }

            if (i == (GridView_HOJA_DE_TRABAJO.Rows.Count - 1))
            {
                GridView_HOJA_DE_TRABAJO.Rows[i].Cells[1].Text = "";
            }
        }

        Label_ALERTA_ALTA.Text = contadorAlertasAltas.ToString();
        Label_ALERTA_MEDIA.Text = contadorAlertasMedias.ToString();
        Label_ALERTA_BAJA.Text = contadorAlertasBajas.ToString();

    }
    #endregion

    #region metodos que se cargan al inciar la pagina
    private void cargar_menu_botones_modulos_internos()
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
        link.ID = "link_base_datos";
        QueryStringSeguro["nombre_modulo"] = "BASE DE DATOS (ASPIRANTES - DISPONIBLES - LABORARON)";
        link.NavigateUrl = "~/seleccion/baseDatos.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Enabled = true;
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bBaseDeDatosEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bBaseDeDatosAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bBaseDedatosEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);



        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_2_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_radicacion";
        QueryStringSeguro["nombre_modulo"] = "RADICACIÓN HOJAS DE VIDA";
        link.NavigateUrl = "~/seleccion/radicacionHojaVida.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bRadicacionHojaEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bRadicacionHojaAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bRadicacionHojaEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);





        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_3_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_entrevistas";
        QueryStringSeguro["nombre_modulo"] = "ENTREVISTAS";
        link.NavigateUrl = "~/seleccion/Entrevista.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bEntrevistasEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bEntrevistasAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bEntrevistasEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);





        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_4_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_referencas";
        QueryStringSeguro["nombre_modulo"] = "REFERENCIAS";
        link.NavigateUrl = "~/seleccion/referencias.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bReferenciasEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bReferenciasAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bReferenciasEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);





        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_5_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_descarte_personal";
        QueryStringSeguro["nombre_modulo"] = "DESCARTE DE PERSONAL";
        link.NavigateUrl = "~/seleccion/descartePersonal.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bDescartePersonalSeleccionEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bDescartePersonalSeleccionAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bDescartePersonalSeleccionEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);




        Table_MENU.Rows.Add(filaTabla);




        contadorFilas = 0;

        filaTabla = new TableRow();
        filaTabla.ID = "menu1_row_" + contadorFilas.ToString();



        celdaTabla = new TableCell();
        celdaTabla.ID = "menu1_cell_1_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_condiciones_usuarias";
        QueryStringSeguro["nombre_modulo"] = "CONDICIONES USUARIAS DE SELECCIÓN";
        QueryStringSeguro["proceso"] = ((int)tabla.proceso.ContactoSeleccion).ToString();
        link.NavigateUrl = "~/maestros/condicionesUsuarias.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bCondicionesUsuariasEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bCondicionesUsuariasAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bCondicionesUsuariasEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);





        celdaTabla = new TableCell();
        celdaTabla.ID = "menu1_cell_2_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_requisiciones";
        QueryStringSeguro["nombre_modulo"] = "REQUISICIONES";
        link.NavigateUrl = "~/seleccion/registroAtencionRequisiciones.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bRequisicionesEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bRequisicionesAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bRequisicionesEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);





        celdaTabla = new TableCell();
        celdaTabla.ID = "menu1_cell_3_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_cancelar_cumplir_req";
        QueryStringSeguro["nombre_modulo"] = "CANCELAR Y CUMPLIR REQUISICIONES";
        link.NavigateUrl = "~/seleccion/cancelacionyCumplimientoRequisiciones.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bCancelarcumplirRequisicionEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bCancelarcumplirRequisicionAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bCancelarcumplirRequisicionEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);





        celdaTabla = new TableCell();
        celdaTabla.ID = "menu1_cell_4_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_documentos";
        QueryStringSeguro["nombre_modulo"] = "DOCS. PARA CONTRATACIÓN";
        link.NavigateUrl = "~/seleccion/requisitos.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bDocumentosEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bDocumentosAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bDocumentosEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);

        Table_MENU.Rows.Add(filaTabla);



        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_4_T1_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_hojaVida";
        QueryStringSeguro["nombre_modulo"] = "HOJA DE VIDA DEL COLABORADOR";
        link.NavigateUrl = "~/contratacion/hojaVida.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bHojaVidaEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bHojaVidaAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bHojaVidaEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);



        celdaTabla = new TableCell();
        celdaTabla.ID = "menu1_cell_5_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_adminnistracion";
        QueryStringSeguro["nombre_modulo"] = "ADMINISTRACIÓN CONTRATACIÓN";
        link.NavigateUrl = "~/seleccion/administracion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bMenuAdministracionEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bMenuAdministracionAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bMenuAdministracionEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);



        Table_MENU_1.Rows.Add(filaTabla);





        contadorFilas = 0;

        filaTabla = new TableRow();
        filaTabla.ID = "menu2_row_" + contadorFilas.ToString();


        celdaTabla = new TableCell();
        celdaTabla.ID = "t2_cell_1_row_Reclutamiento" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_Reclutamiento";
        QueryStringSeguro["nombre_modulo"] = "Reclutamiento";
        QueryStringSeguro["proceso"] = ((int)tabla.proceso.ContactoSeleccion).ToString();
        link.NavigateUrl = "~/Seleccion/hojaTrabajoReclutamiento.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/plantilla/Reclutamiento.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/plantilla/ReclutamientoAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/plantilla/Reclutamiento.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);

        Table_MENU_2.Rows.Add(filaTabla);


        celdaTabla = new TableCell();
        celdaTabla.ID = "t2_cell_1_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_reportes";
        QueryStringSeguro["nombre_modulo"] = "REPORTES";
        QueryStringSeguro["proceso"] = ((int)tabla.proceso.ContactoSeleccion).ToString();
        link.NavigateUrl = "~/Reportes/seleccion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bReportesEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bReportesAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bReportesEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);

        Table_MENU_2.Rows.Add(filaTabla);

    }

    private void iniciar_interfaz_inicial()
    {
        cargar_GridView_HOJA_DE_TRABAJO();
    }

    #endregion

    private void configurarCaracteresAceptadosBusqueda(Boolean letras, Boolean numeros)
    {
        FilteredTextBoxExtender_TextBox_BUSCAR_Numbers.Enabled = numeros;
        FilteredTextBoxExtender_TextBox_BUSCAR_Letras.Enabled = letras;
    }

    private void cargar_DropDownList_BUSCAR()
    {
        DropDownList_BUSCAR.Items.Clear();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_BUSCAR.Items.Add(item);
        
        item = new System.Web.UI.WebControls.ListItem("Regional", "ID_REGIONAL");
        DropDownList_BUSCAR.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("Ciudad", "ID_CIUDAD");
        DropDownList_BUSCAR.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("Cliente", "ID_CLIENTE");
        DropDownList_BUSCAR.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("Número de requicición", "ID_REQUERIMIENTO");
        DropDownList_BUSCAR.Items.Add(item);

        DropDownList_BUSCAR.DataBind();
    }

    private void iniciar_seccion_de_busqueda()
    {
        TextBox_BUSCAR.Text = "";
        cargar_DropDownList_BUSCAR();
    }

    private void configurar_paneles_popup(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_mensaje.CssClass = "panel_popup";
    }

    private void Configurar()
    {
        configurar_paneles_popup(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = "HOJA DE TRABAJO";


        cargar_menu_botones_modulos_internos();

        if (IsPostBack == false)
        {
            Configurar();


            configurarCaracteresAceptadosBusqueda(true, true);

            iniciar_seccion_de_busqueda();

            HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "SIN_FILTRO";
            HiddenField_FILTRO_DROP.Value = String.Empty;
            HiddenField_FILTRO_DATO.Value = String.Empty;
            HiddenField_PAGINA_GRID.Value = "0";
            
            tools _tools = new tools();
            SecureQueryString QueryStringSeguro;
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

            String accion = QueryStringSeguro["accion"].ToString();

            if (accion == "inicial")
            {
                iniciar_interfaz_inicial();
            }
        }
    }

    protected void GridView_HOJA_DE_TRABAJO_SelectedIndexChanged(object sender, EventArgs e)
    {
        String ID_REQUERIMIENTO = GridView_HOJA_DE_TRABAJO.SelectedDataKey["ID_REQUERIMIENTO"].ToString();

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = "seleccion";
        QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
        QueryStringSeguro["nombre_modulo"] = "REGISTRO, ATENCIÓN DE REQUISICIONES";
        QueryStringSeguro["accion"] = "cargar";
        QueryStringSeguro["requerimiento"] = ID_REQUERIMIENTO;

        Response.Redirect("~/seleccion/registroAtencionRequisiciones.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }

    protected void DropDownList_BUSCAR_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_BUSCAR.SelectedValue == "")
        {
            configurarCaracteresAceptadosBusqueda(true, true);
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "ID_REGIONAL")
            {
                configurarCaracteresAceptadosBusqueda(true, false);
            }
            else
            {
                if (DropDownList_BUSCAR.SelectedValue == "ID_CIUDAD")
                {
                    configurarCaracteresAceptadosBusqueda(true, false);
                }
                else
                {
                    if (DropDownList_BUSCAR.SelectedValue == "ID_CLIENTE") 
                    {
                        configurarCaracteresAceptadosBusqueda(true, false);
                    }
                    else
                    {
                        if (DropDownList_BUSCAR.SelectedValue == "ID_REQUERIMIENTO") 
                        {
                            configurarCaracteresAceptadosBusqueda(false, true);
                        }
                    }
                }
            }
        }

        TextBox_BUSCAR.Text = "";
    }









    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "CON_FILTRO";
        HiddenField_FILTRO_DROP.Value = DropDownList_BUSCAR.SelectedValue;
        HiddenField_FILTRO_DATO.Value = TextBox_BUSCAR.Text;
        HiddenField_PAGINA_GRID.Value = "0";

        cargar_GridView_HOJA_DE_TRABAJO();
    }

    private void ocultar_mensaje(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.Style.Add("display", "none");
        panel_mensaje.Style.Add("display", "none");

        panel_fondo.Visible = false;
        panel_mensaje.Visible = false;
    }

    protected void Button_CERRAR_MENSAJE_Click(object sender, System.EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }
    protected void GridView_HOJA_DE_TRABAJO_PreRender(object sender, EventArgs e)
    {
        GridView_HOJA_DE_TRABAJO.Rows[GridView_HOJA_DE_TRABAJO.Rows.Count - 1].Cells[1].Enabled = false;
        GridView_HOJA_DE_TRABAJO.Rows[GridView_HOJA_DE_TRABAJO.Rows.Count - 1].Cells[1].Text = String.Empty;
    }
    protected void TextBox_BUSCAR_TextChanged(object sender, EventArgs e)
    {

    }
}