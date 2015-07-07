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

    #region variables
    private System.Drawing.Color colorNo = System.Drawing.ColorTranslator.FromHtml("#F8523A");
    private System.Drawing.Color colorMedio = System.Drawing.ColorTranslator.FromHtml("#FFF200");
    private System.Drawing.Color colorSi = System.Drawing.ColorTranslator.FromHtml("#50CE04");
    private System.Drawing.Color colorVencido = System.Drawing.ColorTranslator.FromHtml("#699DF0");
    private System.Drawing.Color colorDescarte = System.Drawing.ColorTranslator.FromHtml("#EEEEEE");

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }
    #endregion variables

    #region constructores
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = "HOJA DE TRABAJO";

        if (IsPostBack == false)
        {
            Configurar();

            cargar_menu_botones_modulos_internos();

            tools _tools = new tools();
            SecureQueryString QueryStringSeguro;
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

            String accion = QueryStringSeguro["accion"].ToString();
            iniciar_seccion_de_busqueda();
            configurarCaracteresAceptadosBusqueda(true, true);
            if (accion == "inicial")
            {
                iniciar_interfaz_inicial();
            }
            else
            {
                if (accion == "examenesConfigurados")
                {
                    iniciar_interfaz_inicial();
                }
                else
                {
                    if (accion == "descarteContratacionNoRol")
                    {
                        iniciar_interfaz_inicial();

                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se ha configurado un ROL o PERFIL DE USUARIO al que se le informa los descartes de contratación. (no se pudo enviar el E-Mail informativo).", Proceso.Advertencia);
                    }
                    else
                    {
                        if (accion == "descarteContratacionNoUsuarios")
                        {
                            iniciar_interfaz_inicial();

                            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron usuarios del sistema con el ROL asignado para informar descartes en contratación, por lo tanto no se envió el EMail informativo. (No se pudo enviar el E-Mail informativo).", Proceso.Advertencia);
                        }
                        else
                        {
                            if (accion == "descarteContratacionNoEmail")
                            {
                                iniciar_interfaz_inicial();

                                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El candidato fue Descartado correctamente, Pero no se ha podido informar sobre este descarte.", Proceso.Advertencia);
                            }
                            else
                            {
                                if (accion == "descarteContratacionOk")
                                {
                                    iniciar_interfaz_inicial();

                                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El descarte se realizó correctamente.", Proceso.Correcto);
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    #endregion constructores

    #region metodos
    private void configurarCaracteresAceptadosBusqueda(Boolean letras, Boolean numeros)
    {
        FilteredTextBoxExtender_TextBox_BUSCAR_Numbers.Enabled = numeros;
        FilteredTextBoxExtender_TextBox_BUSCAR_Letras.Enabled = letras;
    }
    
    private void cargar_GridView_HOJA_DE_TRABAJO()
    {
        Label_ALERTA_ALTA.Text = string.Empty;
        Label_ALERTA_MEDIA.Text = string.Empty;
        Label_Contrato_Vencido.Text = string.Empty;

        requisicion _requisicion = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaRequisicionesHojaTrabajoOriginal = _requisicion.ObtenerPersonasPorContratar();

        GridView_HOJA_DE_TRABAJO.DataSource = tablaRequisicionesHojaTrabajoOriginal;
        GridView_HOJA_DE_TRABAJO.DataBind();

        DataRow filaParaColocarColor;
        int contadorContratoVencido = 0;
        int contadorAlertasMedias = 0;
        int contadorAlertasAltas = 0;
        int contadorAlertasNinguna = 0;

        DateTime fechaActual = Convert.ToDateTime(DateTime.Now.ToShortDateString());
        for (int i = 0; i < tablaRequisicionesHojaTrabajoOriginal.Rows.Count; i++)
        {
            filaParaColocarColor = tablaRequisicionesHojaTrabajoOriginal.Rows[(GridView_HOJA_DE_TRABAJO.PageIndex * GridView_HOJA_DE_TRABAJO.PageSize) + i];

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
                    GridView_HOJA_DE_TRABAJO.Rows[i].BackColor = System.Drawing.Color.Gray;
                    contadorAlertasNinguna += 1;
                }
            }

            if (Convert.ToDateTime(filaParaColocarColor["FECHA_VENCE"]) < fechaActual)
            {
                GridView_HOJA_DE_TRABAJO.Rows[i].Cells[0].Enabled = false;
                GridView_HOJA_DE_TRABAJO.Rows[i].Cells[0].Text = "";
                GridView_HOJA_DE_TRABAJO.Rows[i].BackColor = colorVencido;
                contadorContratoVencido += 1;
            }

            if (filaParaColocarColor["ESTADO_PROCESO"].ToString().Trim() == "EN DESCARTE")
            {
                GridView_HOJA_DE_TRABAJO.Rows[i].Cells[0].Enabled = false;
                GridView_HOJA_DE_TRABAJO.Rows[i].Cells[0].Text = "";
                GridView_HOJA_DE_TRABAJO.Rows[i].BackColor = colorDescarte;
                contadorContratoVencido += 1;
            }
        }

        Label_ALERTA_ALTA.Text = contadorAlertasAltas.ToString() + " Personas con plazo de contratación vencido.";
        Label_ALERTA_MEDIA.Text = contadorAlertasMedias.ToString() + " Personas en plazo de contratación.";
        Label_Contrato_Vencido.Text = contadorContratoVencido.ToString() + " Personas con objeto de contrato vencido.";
    }

    private void cargar_DropDownList_BUSCAR()
    {
        DropDownList_BUSCAR.Items.Clear();

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_BUSCAR.Items.Add(item);
        item = new ListItem("NÚMERO IDENTIFICACIÓN", "NUM_DOC_IDENTIFICACION");
        DropDownList_BUSCAR.Items.Add(item);
        item = new ListItem("NOMBRES", "NOMBRES");
        DropDownList_BUSCAR.Items.Add(item);
        item = new ListItem("APELLIDOS", "APELLIDOS");
        DropDownList_BUSCAR.Items.Add(item);
        item = new ListItem("EMPRESA", "RAZ_SOCIAL");
        DropDownList_BUSCAR.Items.Add(item);
        item = new ListItem("REGIONAL", "REGIONAL");
        DropDownList_BUSCAR.Items.Add(item);
        item = new ListItem("CIUDAD", "CIUDAD");
        DropDownList_BUSCAR.Items.Add(item);
        DropDownList_BUSCAR.DataBind();
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

    private void cargar_menu_botones_modulos_internos()
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
        link.ID = "link_condicion_contratacion";
        QueryStringSeguro["nombre_modulo"] = "CONDICIONES USUARIAS DE CONTRATACION";
        QueryStringSeguro["proceso"] = ((int)tabla.proceso.ContactoContratacion).ToString();
        QueryStringSeguro["grid_pagina"] = "0";
        QueryStringSeguro["filtro"] = "SIN_FILTRO";
        QueryStringSeguro["drop"] = String.Empty;
        QueryStringSeguro["dato"] = String.Empty;
        link.NavigateUrl = "~/maestros/condicionesUsuarias.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bCondicionContratacionEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bCondicionContratacionAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bCondicionContratacionEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);


        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_2_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_control_servicios_respectivos";
        QueryStringSeguro["nombre_modulo"] = "CONTROL DE SERVICIOS RESPECTIVOS";
        link.NavigateUrl = "~/comercial/hojaTrabajoComercial.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        link.Enabled = false;
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bControlServiciosRespectivosEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bControlServiciosRespectivosAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bControlServiciosRespectivosEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);





































        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_3_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_alaboracion_contrato";
        QueryStringSeguro["nombre_modulo"] = "ELABORACIÓN DE CONTRATO";
        link.NavigateUrl = "~/contratacion/examenesEmpleado.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bElaContratoEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bElaContratoAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bElaContratoEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);



        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_4_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_auditoria";
        QueryStringSeguro["nombre_modulo"] = "AUDITORÍA DE CONTRATOS";
        link.NavigateUrl = "~/contratacion/auditoria.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bAuditoriaContratosEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bAuditoriaContratosAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bAuditoriaContratosEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);



        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_5_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_incapacidades";
        QueryStringSeguro["nombre_modulo"] = "INCAPACIDADES";
        link.NavigateUrl = "~/contratacion/incapacidades.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bIncapacidadesEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bIncapacidadesAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bIncapacidadesEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);





        Table_MENU.Rows.Add(filaTabla);







        contadorFilas = 0;

        filaTabla = new TableRow();
        filaTabla.ID = "row_T1_" + contadorFilas.ToString();









        
        
        
        
        
        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_2_T1_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_retiros";
        QueryStringSeguro["nombre_modulo"] = "RETIROS";
        link.NavigateUrl = "~/contratacion/Retiros.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bRetirosEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bRetirosAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bRetirosEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);






        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_3_T1_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_entrevistas_rotacion_retiro";
        QueryStringSeguro["nombre_modulo"] = "ENTREVISTAS DE ROTACÓN Y RETIROS";
        QueryStringSeguro["proceso"] = ((int)tabla.proceso.ContactoSeleccion).ToString();
        link.NavigateUrl = "~/contratacion/entrevistasRotacionRetiros.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bEntrevistaRotacionRetiroEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bEntrevistaRotacionRetiroAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bEntrevistaRotacionRetiroEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);



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
        celdaTabla.ID = "cell_5_T1_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_autoliquidacion";
        QueryStringSeguro["nombre_modulo"] = "AUTOLIQUIDACIÓN";
        link.NavigateUrl = "~/contratacion/Autoliquidaciones.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bAutoliquidacionEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bAutoliquidacionAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bAutoliquidacionEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);




        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_6_T1_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_reportes";
        QueryStringSeguro["nombre_modulo"] = "REPORTES";
        link.NavigateUrl = "~/Reportes/contratacion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
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



        Table_MENU_1.Rows.Add(filaTabla);









        contadorFilas = 0;

        filaTabla = new TableRow();
        filaTabla.ID = "row_T2_" + contadorFilas.ToString();














        Table_MENU_2.Rows.Add(filaTabla);











        contadorFilas = 0;

        filaTabla = new TableRow();
        filaTabla.ID = "cell_1_T3_row_" + contadorFilas.ToString();


        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_2_T3_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_administracion";
        QueryStringSeguro["nombre_modulo"] = "ADMINISTRACION";
        link.NavigateUrl = "~/contratacion/administracion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bMenuAdministracionEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bMenuAdministracionAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bMenuAdministracionEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);





        Table_MENU_3.Rows.Add(filaTabla);








    }

    private void iniciar_interfaz_inicial()
    {
        cargar_GridView_HOJA_DE_TRABAJO();
    }

    private void iniciar_seccion_de_busqueda()
    {
        TextBox_BUSCAR.Text = "";
        cargar_DropDownList_BUSCAR();
    }

    private void ocultar_mensaje(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.Style.Add("display", "none");
        panel_mensaje.Style.Add("display", "none");

        panel_fondo.Visible = false;
        panel_mensaje.Visible = false;
    }

    private void Configurar()
    {
        configurar_paneles_popup(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    private void configurar_paneles_popup(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_mensaje.CssClass = "panel_popup";
    }
    #endregion metodos

    #region eventos
    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    protected void GridView_HOJA_DE_TRABAJO_SelectedIndexChanged(object sender, EventArgs e)
    {

        String ID_SOLICITUD = GridView_HOJA_DE_TRABAJO.SelectedDataKey["ID_SOLICITUD"].ToString();
        String ID_REQUERIMIENTO = GridView_HOJA_DE_TRABAJO.SelectedDataKey["ID_REQUERIMIENTO"].ToString();
        String ID_EMPRESA = GridView_HOJA_DE_TRABAJO.SelectedDataKey["ID_EMPRESA"].ToString();
        String ID_OCUPACION = GridView_HOJA_DE_TRABAJO.SelectedDataKey["ID_OCUPACION"].ToString();
        String NUM_DOC_IDENTIDAD = GridView_HOJA_DE_TRABAJO.SelectedDataKey["NUM_DOC_IDENTIDAD"].ToString();

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = "seleccion";
        QueryStringSeguro["nombre_area"] = "CONTRATACION";
        QueryStringSeguro["nombre_modulo"] = "ELABORAR CONTRATO";
        QueryStringSeguro["accion"] = "cargar_inicial";
        QueryStringSeguro["requerimiento"] = ID_REQUERIMIENTO;
        QueryStringSeguro["solicitud"] = ID_SOLICITUD;
        QueryStringSeguro["empresa"] = ID_EMPRESA;
        QueryStringSeguro["cargo"] = ID_OCUPACION;
        QueryStringSeguro["docID"] = NUM_DOC_IDENTIDAD;

        Response.Redirect("~/contratacion/examenesEmpleado.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }

    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {

        String datosCapturados = TextBox_BUSCAR.Text.ToUpper();
        String campo = DropDownList_BUSCAR.SelectedValue.ToString();

        requisicion _requicision = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaResultadosBusqueda = new DataTable();

        if (DropDownList_BUSCAR.SelectedValue == "NUM_DOC_IDENTIFICACION")
        {
            tablaResultadosBusqueda = _requicision.ObtenerPersonasPorContratarPorNumDocIdentificacion(datosCapturados);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "NOMBRES")
        {
            tablaResultadosBusqueda = _requicision.ObtenerPersonasPorContratarPorNombres(datosCapturados);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "APELLIDOS")
        {
            tablaResultadosBusqueda = _requicision.ObtenerPersonasPorContratarPorApellidos(datosCapturados);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "RAZ_SOCIAL")
        {
            tablaResultadosBusqueda = _requicision.ObtenerPersonasPorContratarPorEmpresa(datosCapturados);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "REGIONAL")
        {
            tablaResultadosBusqueda = _requicision.ObtenerPersonasPorContratarRegional(datosCapturados);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "CIUDAD")
        {
            tablaResultadosBusqueda = _requicision.ObtenerPersonasPorContratarCiudad(datosCapturados);
        }

        if (tablaResultadosBusqueda.Rows.Count <= 0)
        {
            if (_requicision.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _requicision.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros que cumplieran los datos de busqueda.", Proceso.Advertencia);
            }

            Panel_HOJA_DE_TRABAJO.Visible = false;
        }
        else
        {
            Panel_HOJA_DE_TRABAJO.Visible = true;
            GridView_HOJA_DE_TRABAJO.DataSource = tablaResultadosBusqueda;
            GridView_HOJA_DE_TRABAJO.DataBind();
            DataRow filaParaColocarColor;
            int contadorContratoVencido = 0;
            int contadorAlertasBajas = 0;
            int contadorAlertasMedias = 0;
            int contadorAlertasAltas = 0;
            int contadorAlertasNinguna = 0;
            DateTime fechaActual = Convert.ToDateTime(DateTime.Now.ToShortDateString());

            for (int i = 0; i < tablaResultadosBusqueda.Rows.Count; i++)
            {
                filaParaColocarColor = tablaResultadosBusqueda.Rows[(GridView_HOJA_DE_TRABAJO.PageIndex * GridView_HOJA_DE_TRABAJO.PageSize) + i];

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

                if (Convert.ToDateTime(filaParaColocarColor["FECHA_VENCE"]) < fechaActual)
                {
                    GridView_HOJA_DE_TRABAJO.Rows[i].Cells[0].Enabled = false;
                    GridView_HOJA_DE_TRABAJO.Rows[i].Cells[0].Text = "";
                    GridView_HOJA_DE_TRABAJO.Rows[i].BackColor = colorVencido;
                    contadorContratoVencido += 1;
                }
            }

            Label_ALERTA_ALTA.Text = string.Empty;
            Label_ALERTA_MEDIA.Text = string.Empty;
            Label_Contrato_Vencido.Text = string.Empty;

            Label_ALERTA_ALTA.Text = contadorAlertasAltas.ToString() + " Personas con plazo de contratación vencido.";
            Label_ALERTA_MEDIA.Text = contadorAlertasMedias.ToString() + " Personas en plazo de contratación.";
            Label_Contrato_Vencido.Text = contadorContratoVencido.ToString() + " Personas con objeto de contrato vencido.";
        }
    }

    protected void DropDownList_BUSCAR_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_BUSCAR.SelectedValue == "")
        {
            configurarCaracteresAceptadosBusqueda(true, true);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "NUM_DOC_IDENTIFICACION")
        {
            configurarCaracteresAceptadosBusqueda(false, true);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "NOMBRES")
        {
            configurarCaracteresAceptadosBusqueda(true, false);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "APELLIDOS")
        {
            configurarCaracteresAceptadosBusqueda(true, false);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "RAZ_SOCIAL")
        {
            configurarCaracteresAceptadosBusqueda(true, false);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "REGIONAL")
        {
            configurarCaracteresAceptadosBusqueda(true, false);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "CIUDAD")
        {
            configurarCaracteresAceptadosBusqueda(true, false);
        }

        TextBox_BUSCAR.Text = "";
    }

    #endregion eventos
}