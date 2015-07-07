using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using Brainsbits.LLB.maestras;

using TSHAK.Components;
using Brainsbits.LLB.seleccion;
using System.Data;
using Brainsbits.LLB.comercial;
using System.Web;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using Brainsbits.LLB.seguridad;
using System.Configuration;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;

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
    private String GLO_ID_CIUDAD = "";
    private Decimal GLO_ID_CENTRO_C = 0;
    private Decimal GLO_ID_SUB_C = 0;

    private enum Acciones
    {
        Inicio = 0,
        Buscar,
        SeleccionUbicacion,
        UbicacionSeleccionada,
        ResultadosExamenes
    }

    private enum AccionesProceso
    { 
        Paso1ConfigurarExamenes = 0,
        Paso2RegistrarResultadosExamenes
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }

    private enum Impresiones
    {
        OrdenExamen=0,
        AutosRecomendacion
    }

    ReportDocument reporte;

    #endregion variables

    #region constructores
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = "EXAMENES MEDICOS";

        if (IsPostBack == false)
        {
            Iniciar();
        }
    }


    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);

        try
        {
            reporte.Dispose();
            reporte = null;
            reporte.Close();
        }
        catch
        {
        }

        try
        {
            GC.Collect();
        }
        catch
        {
        }
    }

    #endregion constructores

    #region metodos
    private void configurar_paneles_popup(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_mensaje.CssClass = "panel_popup";
    }

    private void Configurar()
    {
        configurar_paneles_popup(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    private void direccionado_desde_hoja_de_trabajo(Decimal ID_EMPRESA, Decimal ID_SOLICITUD, Decimal ID_REQUERIMIENTO, Decimal ID_OCUPACION, String NUM_DOC_IDENTIDAD)
    {
        Cargar(ID_REQUERIMIENTO, ID_SOLICITUD, ID_EMPRESA, ID_OCUPACION);
        Cargar(Acciones.Inicio);
    }

    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = false;
                Panel_BOTONES_INTERNOS.Visible = false;
                Panel_FORM_BOTONES.Visible = false;
                Panel_RESULTADOS_GRID.Visible = false;
                Panel_INFORMACION_CANDIDATO_SELECCION.Visible = false;
                Panel_informacionUbicacionTrabajador.Visible = false;

                Panel_CONFIGURACION_EXAMENES.Visible = false;
                Button_ACEPTAR_UBICACION.Visible = false;

                Panel_GRILLA_CONFIGURACION_EXAMENES.Visible = false;

                Panel_RESULTADOS_EXAMENES_MEDICOS.Visible = false;

                Panel_CONFIGURACION_FORMA_PAGO.Visible = false;
                Panel_FORMA_CONSIGNACION_BANCARIA.Visible = false;
                Panel_CUENTA_BANCARIA.Visible = false;

                Panel_GUARDAR_PROCESO.Visible = false;
                Button_DescartarPorExamenes.Visible = false;

                Panel_BOTONES_MENU.Visible = false;
                Button_Imprimir_Ordenes.Visible = false;
                Button_Imprimir_Carta.Visible = false;
                Button_Imprimir_Autos.Visible = false;

                Panel_informarDescarte.Visible = false;
                break;
            case Acciones.UbicacionSeleccionada:
                Button_ACEPTAR_UBICACION.Visible = false;
                Panel_CONFIGURACION_EXAMENES.Visible = false;
                break;
        }
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_FORM_BOTONES.Visible = true;
                break;
            case Acciones.Buscar:
                Panel_FORM_BOTONES.Visible = true;
                Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.SeleccionUbicacion:
                Panel_BOTONES_INTERNOS.Visible = true;
                Panel_FORM_BOTONES.Visible = true;
                Panel_INFORMACION_CANDIDATO_SELECCION.Visible = true;
                Panel_CONFIGURACION_EXAMENES.Visible = true;
                Button_ACEPTAR_UBICACION.Visible = true;
                break;
            case Acciones.UbicacionSeleccionada:
                Panel_GRILLA_CONFIGURACION_EXAMENES.Visible = true;
                Panel_CONFIGURACION_FORMA_PAGO.Visible = true;
                Panel_GUARDAR_PROCESO.Visible = true;
                Button_Guardar_PROCESO.Visible = true;
                break;
            case Acciones.ResultadosExamenes:
                Panel_BOTONES_INTERNOS.Visible = true;
                Panel_FORM_BOTONES.Visible = true;
                Panel_INFORMACION_CANDIDATO_SELECCION.Visible = true;
                Panel_RESULTADOS_EXAMENES_MEDICOS.Visible = true;
                Panel_CONFIGURACION_FORMA_PAGO.Visible = true;
                Panel_GUARDAR_PROCESO.Visible = true;

                Panel_BOTONES_MENU.Visible = true;
                Button_Imprimir_Ordenes.Visible = true;
                Button_Imprimir_Carta.Visible = true;
                Button_Imprimir_Autos.Visible = true;
                break;
        }
    }

    private void configurarCaracteresAceptadosBusqueda(Boolean letras, Boolean numeros)
    {
        FilteredTextBoxExtender_TextBox_BUSCAR_Numbers.Enabled = numeros;
        FilteredTextBoxExtender_TextBox_BUSCAR_Letras.Enabled = letras;
    }

    private void cargar_DropDownList_BUSCAR()
    {
        DropDownList_BUSCAR.Items.Clear();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Ninguno", "");
        DropDownList_BUSCAR.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("NÚMERO IDENTIFICACIÓN", "NUM_DOC_IDENTIFICACION");
        DropDownList_BUSCAR.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("NOMBRES", "NOMBRES");
        DropDownList_BUSCAR.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("APELLIDOS", "APELLIDOS");
        DropDownList_BUSCAR.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("EMPRESA", "RAZ_SOCIAL");
        DropDownList_BUSCAR.Items.Add(item);

        DropDownList_BUSCAR.DataBind();
    }

    private void iniciar_seccion_de_busqueda()
    {
        TextBox_BUSCAR.Text = "";
        cargar_DropDownList_BUSCAR();
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                configurarCaracteresAceptadosBusqueda(true, true);

                HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "NINGUNA";
                HiddenField_FILTRO_DROP.Value = String.Empty;
                HiddenField_FILTRO_DATO.Value = String.Empty;

                iniciar_seccion_de_busqueda();
                break;
        }
    }

    protected void Iniciar()
    {
        Configurar();

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        if ((String.IsNullOrEmpty(QueryStringSeguro["empresa"]) == false) && (String.IsNullOrEmpty(QueryStringSeguro["solicitud"]) == false) && (String.IsNullOrEmpty(QueryStringSeguro["requerimiento"]) == false) && (String.IsNullOrEmpty(QueryStringSeguro["docID"]) == false))
        {
            Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["empresa"]);
            Decimal ID_SOLICITUD = Convert.ToDecimal(QueryStringSeguro["solicitud"]);
            Decimal ID_REQUERIMIENTO = Convert.ToDecimal(QueryStringSeguro["requerimiento"]);
            Decimal ID_OCUPACION = Convert.ToDecimal(QueryStringSeguro["cargo"]);
            String NUM_DOC_IDENTIDAD = QueryStringSeguro["docID"].ToString().Trim();

            HiddenField_ID_EMPRESA.Value = ID_EMPRESA.ToString();
            HiddenField_ID_SOLICITUD.Value = ID_SOLICITUD.ToString();
            HiddenField_ID_REQUERIMIENTO.Value = ID_REQUERIMIENTO.ToString();
            HiddenField_ID_OCUPACION.Value = ID_OCUPACION.ToString();
            HiddenField_NUM_DOC_IDENTIDAD.Value = NUM_DOC_IDENTIDAD;

            direccionado_desde_hoja_de_trabajo(ID_EMPRESA, ID_SOLICITUD, ID_REQUERIMIENTO, ID_OCUPACION, NUM_DOC_IDENTIDAD);
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);

            Panel_INFO_ADICIONAL_MODULO.Visible = true;
            Label_INFO_ADICIONAL_MODULO.Text = "UTILICE EL BUSCADOR PARA ENCONTRAR A QUIEN SE LE DESEA REALIZAR EL CONTRATO";
        }
    }

    private void ocultar_mensaje(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.Style.Add("display", "none");
        panel_mensaje.Style.Add("display", "none");

        panel_fondo.Visible = false;
        panel_mensaje.Visible = false;
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

    private void Cargar_GridView_RESULTADOS_BUSQUEDA(DataTable tablaResultadosBusqueda)
    {
        GridView_RESULTADOS_BUSQUEDA.DataSource = tablaResultadosBusqueda;
        GridView_RESULTADOS_BUSQUEDA.DataBind();

        DataRow filaParaColocarColor;
        int contadorAlertasBajas = 0;
        int contadorAlertasMedias = 0;
        int contadorAlertasAltas = 0;
        int contadorAlertasNinguna = 0;

        for (int i = 0; i < GridView_RESULTADOS_BUSQUEDA.Rows.Count; i++)
        {
            filaParaColocarColor = tablaResultadosBusqueda.Rows[(GridView_RESULTADOS_BUSQUEDA.PageIndex * GridView_RESULTADOS_BUSQUEDA.PageSize) + i];

            if (filaParaColocarColor["ALERTA"].ToString().Trim() == "ALTA")
            {
                GridView_RESULTADOS_BUSQUEDA.Rows[i].BackColor = System.Drawing.Color.Red;
                contadorAlertasAltas += 1;
            }
            else
            {
                if (filaParaColocarColor["ALERTA"].ToString().Trim() == "MEDIA")
                {
                    GridView_RESULTADOS_BUSQUEDA.Rows[i].BackColor = System.Drawing.Color.Yellow;
                    contadorAlertasMedias += 1;
                }
                else
                {
                    if (filaParaColocarColor["ALERTA"].ToString().Trim() == "BAJA")
                    {
                        GridView_RESULTADOS_BUSQUEDA.Rows[i].BackColor = System.Drawing.Color.Green;
                        contadorAlertasBajas += 1;
                    }
                    else
                    {
                        GridView_RESULTADOS_BUSQUEDA.Rows[i].BackColor = System.Drawing.Color.Gray;
                        contadorAlertasNinguna += 1;
                    }
                }
            }

            if (i == (GridView_RESULTADOS_BUSQUEDA.Rows.Count - 1))
            {
                GridView_RESULTADOS_BUSQUEDA.Rows[i].Cells[1].Text = "";
            }
        }

        Label_ALERTA_ALTA.Text = contadorAlertasAltas.ToString();
        Label_ALERTA_MEDIA.Text = contadorAlertasMedias.ToString();
        Label_ALERTA_BAJA.Text = contadorAlertasBajas.ToString();
    }

    private void Buscar()
    {
        Ocultar(Acciones.Inicio);

        String datosCapturados = HiddenField_FILTRO_DATO.Value;
        String campo = HiddenField_FILTRO_DROP.Value;

        requisicion _requicision = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaResultadosBusqueda = new DataTable();

        if (campo == "NUM_DOC_IDENTIFICACION")
        {
            tablaResultadosBusqueda = _requicision.ObtenerPersonasPorContratarPorNumDocIdentificacion(datosCapturados);
        }
        else if (campo == "NOMBRES")
        {
            tablaResultadosBusqueda = _requicision.ObtenerPersonasPorContratarPorNombres(datosCapturados);
        }
        else if (campo == "APELLIDOS")
        {
            tablaResultadosBusqueda = _requicision.ObtenerPersonasPorContratarPorApellidos(datosCapturados);
        }
        else if (campo == "RAZ_SOCIAL")
        {
            tablaResultadosBusqueda = _requicision.ObtenerPersonasPorContratarPorEmpresa(datosCapturados);
        }

        if (tablaResultadosBusqueda.Rows.Count <= 0)
        {
            if (_requicision.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _requicision.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron resultados para a busqueda realizada.", Proceso.Error);
            }
        }
        else
        {
            Mostrar(Acciones.Buscar);
            Cargar_GridView_RESULTADOS_BUSQUEDA(tablaResultadosBusqueda);
        }
    }

    private void cargarDatosTrabajador(String NOMBRE_TRABAJADOR, String NUM_DOC_IDENTIDAD, String RAZ_SOCIAL, String CARGO, String ID_OCUPACION)
    {
        Label_NOMBRE_TRABAJADOR.Text = NOMBRE_TRABAJADOR;
        Label_NUM_DOC_IDENTIDAD.Text = NUM_DOC_IDENTIDAD;
        Label_RAZ_SOCIAL.Text = RAZ_SOCIAL;
        Label_CARGO.Text = ID_OCUPACION.Trim() + "-" + CARGO.Trim();
    }

    private void cargar_menu_botones_modulos_internos()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = "contratacion";
        QueryStringSeguro["nombre_area"] = "CONTRATOS Y RELACIONES LABORALES";

        QueryStringSeguro["empresa"] = HiddenField_ID_EMPRESA.Value;
        QueryStringSeguro["solicitud"] = HiddenField_ID_SOLICITUD.Value;
        QueryStringSeguro["requerimiento"] = HiddenField_ID_REQUERIMIENTO.Value;
        QueryStringSeguro["cargo"] = HiddenField_ID_OCUPACION.Value;
        QueryStringSeguro["docID"] = HiddenField_NUM_DOC_IDENTIDAD.Value;

        QueryStringSeguro["persona"] = HiddenField_persona.Value;

        TableRow filaTabla;
        TableCell celdaTabla;
        HyperLink link;
        System.Web.UI.WebControls.Image imagen;

        int contadorFilas = 0;

        filaTabla = new TableRow();
        filaTabla.ID = "row_" + contadorFilas.ToString();

        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_6_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_descartes";
        QueryStringSeguro["accion"] = "cargar";
        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable dataTable = _radicacionHojasDeVida.ObtenerRegSolicitudesingresoPorIdSolicitud(Convert.ToInt32(HiddenField_ID_SOLICITUD.Value));

        QueryStringSeguro["num_doc_identidad"] = dataTable.Rows[0]["num_doc_identidad"].ToString();
        QueryStringSeguro["nombre_modulo"] = "DESCARTE DE PERSONAL";
        link.NavigateUrl = "~/contratacion/descartePersonal.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        imagen = new System.Web.UI.WebControls.Image();
        imagen.ImageUrl = "~/imagenes/areas/bDescartesContratacionEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bDescartesContratacionAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bDescartesContratacionEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);

        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_1_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_examenes";

        QueryStringSeguro["accion"] = "cargar";
        QueryStringSeguro["nombre_modulo"] = "EXAMENES Y CUENTA BANCARIA";
        link.NavigateUrl = "~/contratacion/examenesEmpleado.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());

        link.CssClass = "botones_menu_principal";
        imagen = new System.Web.UI.WebControls.Image();
        imagen.ImageUrl = "~/imagenes/areas/bExamenCuentaEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bExamenCuentaAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bExamenCuentaEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);


        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_2_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_afiliaciones";
        QueryStringSeguro["accion"] = "cargar";
        QueryStringSeguro["nombre_modulo"] = "AFILIACIONES";
        link.NavigateUrl = "~/contratacion/afiliaciones.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        imagen = new System.Web.UI.WebControls.Image();
        imagen.ImageUrl = "~/imagenes/areas/bAfiliacionesEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bAfiliacionesAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bAfiliacionesEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);



        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_3_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_elaborar_contrato";
        QueryStringSeguro["accion"] = "cargar";
        QueryStringSeguro["nombre_modulo"] = "ELABORAR CONTRATO";
        link.NavigateUrl = "~/contratacion/activarEmpleado.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        imagen = new System.Web.UI.WebControls.Image();
        imagen.ImageUrl = "~/imagenes/areas/bElaborarContratoEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bElaborarContratoAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bElaborarContratoEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);


        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_4_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_servicios";
        QueryStringSeguro["accion"] = "cargar";
        QueryStringSeguro["nombre_modulo"] = "SERVICIOS COMPLEMENTARIOS";
        link.NavigateUrl = "~/maestros/EntregasEmpleado.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        imagen = new System.Web.UI.WebControls.Image();
        imagen.ImageUrl = "~/imagenes/areas/bServiciosComplementariosEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bServiciosComplementariosAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bServiciosComplementariosEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);


        Table_MENU.Rows.Add(filaTabla);

        contadorFilas = 0;

        filaTabla = new TableRow();
        filaTabla.ID = "M1_row_" + contadorFilas.ToString();

        celdaTabla = new TableCell();
        celdaTabla.ID = "M1_row_1_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_volver";
        QueryStringSeguro["accion"] = "inicial";
        link.NavigateUrl = "~/contratacion/hojaTrabajoContratacion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        imagen = new System.Web.UI.WebControls.Image();
        imagen.ImageUrl = "~/imagenes/areas/bVolverHojaTrabajoEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bVolverHojaTrabajoAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bVolverHojaTrabajoEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);


        Table_MENU_1.Rows.Add(filaTabla);

    }

    private void cargar_DropDownList_CIUDAD(Decimal ID_EMPRESA)
    {
        DropDownList_CIUDAD_TRABAJADOR.Items.Clear();

        cobertura _cobertura = new cobertura(Session["idEmpresa"].ToString());
        DataTable tablaCoberturaEmpresa = _cobertura.obtenerCoberturaDeUnCliente(ID_EMPRESA);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_CIUDAD_TRABAJADOR.Items.Add(item);

        foreach (DataRow fila in tablaCoberturaEmpresa.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["Ciudad"].ToString(), fila["Código Ciudad"].ToString());
            DropDownList_CIUDAD_TRABAJADOR.Items.Add(item);
        }

        DropDownList_CIUDAD_TRABAJADOR.DataBind();
    }

    private void cargar_DropDownList_CENTRO_COSTO(Decimal ID_EMPRESA, String ID_CIUDAD)
    {
        DropDownList_CC_TRABAJADOR.Items.Clear();

        centroCosto _centroCosto = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaCCCiudad = _centroCosto.ObtenerCentrosDeCostoPorIdEmpresaIdCiudad(ID_EMPRESA, ID_CIUDAD);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione", "");
        DropDownList_CC_TRABAJADOR.Items.Add(item);

        foreach (DataRow fila in tablaCCCiudad.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOM_CC"].ToString(), fila["ID_CENTRO_C"].ToString());
            DropDownList_CC_TRABAJADOR.Items.Add(item);
        }

        DropDownList_CC_TRABAJADOR.DataBind();
    }

    private void cargar_DropDownList_SUB_CENTRO(Decimal ID_EMPRESA, Decimal ID_CENTRO_C)
    {
        DropDownList_SUB_CENTRO_TRABAJADOR.Items.Clear();

        subCentroCosto _subCentroCosto = new subCentroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaSUB_C = _subCentroCosto.ObtenerSubCentrosDeCostoPorIdEmpresaIdCentroCosto(ID_EMPRESA, ID_CENTRO_C);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione", "");
        DropDownList_SUB_CENTRO_TRABAJADOR.Items.Add(item);

        foreach (DataRow fila in tablaSUB_C.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOM_SUB_C"].ToString(), fila["ID_SUB_C"].ToString());
            DropDownList_SUB_CENTRO_TRABAJADOR.Items.Add(item);
        }

        DropDownList_SUB_CENTRO_TRABAJADOR.DataBind();
    }

    private void inhabilitar_DropDownList_SUB_CENTRO()
    {
        DropDownList_SUB_CENTRO_TRABAJADOR.Enabled = false;
        DropDownList_SUB_CENTRO_TRABAJADOR.Items.Clear();
        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione", "");
        DropDownList_SUB_CENTRO_TRABAJADOR.Items.Add(item);
        DropDownList_SUB_CENTRO_TRABAJADOR.DataBind();
    }

    private void inhabilitar_DropDownList_CENTRO_COSTO()
    {
        DropDownList_CC_TRABAJADOR.Enabled = false;
        DropDownList_CC_TRABAJADOR.Items.Clear();
        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione", "");
        DropDownList_CC_TRABAJADOR.Items.Add(item);
        DropDownList_CC_TRABAJADOR.DataBind();
    }

    private void inhabilitar_DropDownList_CIUDAD_TRABAJADOR()
    {
        DropDownList_CIUDAD_TRABAJADOR.Enabled = false;
        DropDownList_CIUDAD_TRABAJADOR.Items.Clear();
        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione", "");
        DropDownList_CIUDAD_TRABAJADOR.Items.Add(item);
        DropDownList_CIUDAD_TRABAJADOR.DataBind();
    }

    private void colorear_indicadores_de_ubicacion(Boolean ciudad, Boolean centro_c, Boolean sub_c, System.Drawing.Color color)
    {
        Label_CIUDAD_SELECCIONADA.BackColor = System.Drawing.Color.Transparent;
        Label_CC_SELECCIONADO.BackColor = System.Drawing.Color.Transparent;
        Label_SUBC_SELECCIONADO.BackColor = System.Drawing.Color.Transparent;

        if (ciudad == true)
        {
            Label_CIUDAD_SELECCIONADA.BackColor = color;
        }

        if (centro_c == true)
        {
            Label_CC_SELECCIONADO.BackColor = color;
        }

        if (sub_c == true)
        {
            Label_SUBC_SELECCIONADO.BackColor = color;
        }
    }

    private void CargarSeccionUbicacionTrabajador()
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        cargar_DropDownList_CIUDAD(ID_EMPRESA);
        inhabilitar_DropDownList_CENTRO_COSTO();
        inhabilitar_DropDownList_SUB_CENTRO();

        colorear_indicadores_de_ubicacion(false, false, false, System.Drawing.Color.Transparent);
        HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";
    }

    private void Cargar_GridView_EXAMENES_REALIZADOS_desde_tabla(DataTable tablaExamenes)
    {
        GridView_EXAMENES_REALIZADOS.DataSource = tablaExamenes;
        GridView_EXAMENES_REALIZADOS.DataBind();

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;

        for (int i = 0; i < GridView_EXAMENES_REALIZADOS.Rows.Count; i++)
        {
            DataRow filaTabla = tablaExamenes.Rows[i];

            CheckBox checkValidado = GridView_EXAMENES_REALIZADOS.Rows[i].FindControl("CheckBox_EXAMENES_ENTREGADOS") as CheckBox;
            TextBox textoAutoRecomendacion = GridView_EXAMENES_REALIZADOS.Rows[i].FindControl("TextBox_Autos_Recomendacion") as TextBox;
            HyperLink link = GridView_EXAMENES_REALIZADOS.Rows[i].FindControl("HyperLink_ARCHIVO_EXAMEN") as HyperLink;
            FileUpload fileUpload = GridView_EXAMENES_REALIZADOS.Rows[i].FindControl("FileUpload_ARCHIVO") as FileUpload;

            if (filaTabla["VALIDADO"].ToString() == "S")
            {
                checkValidado.Checked = true;

                checkValidado.Enabled = false;
                textoAutoRecomendacion.Enabled = false;
                fileUpload.Visible = false;
            }
            else
            {
                checkValidado.Checked = false;

                checkValidado.Enabled = true;
                textoAutoRecomendacion.Enabled = true;
                fileUpload.Visible = true;
            }

            if (DBNull.Value.Equals(filaTabla["OBSERVACIONES"]) == false)
            {
                textoAutoRecomendacion.Text = filaTabla["OBSERVACIONES"].ToString().Trim();
            }
            else
            {
                textoAutoRecomendacion.Text = "";
            }

            if (DBNull.Value.Equals(filaTabla["ARCHIVO_EXAMEN"]) == false)
            {
                QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

                link.Target = "_blank";

                QueryStringSeguro["registro"] = filaTabla["REGISTRO"].ToString();

                link.NavigateUrl = "~/contratacion/visorArchivosExamenes.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());

                link.Visible = true;
                link.Enabled = true;
            }
            else
            {
                link.Visible = false;
                link.Enabled = false;
                link.NavigateUrl = null;
            }
        }
    }

    private void Cargar_forma_Pago(DataRow filaSolicitud, Boolean TIENE_CUENTA)
    {
        Int32 FORMA_PAGO_VARIABLE = 0;

        if (filaSolicitud["PAR_FORMA_PAGO_VARIABLE"].ToString().Trim() == "1")
        {
            FORMA_PAGO_VARIABLE = 1;
        }
     
        String FORMA_PAGO = filaSolicitud["FORMA_PAGO"].ToString().Trim();
        Decimal ID_ENTIDAD = 0;

        if (DBNull.Value.Equals(filaSolicitud["ID_ENTIDAD"]) == false)
        {
            ID_ENTIDAD = Convert.ToDecimal(filaSolicitud["ID_ENTIDAD"]);
        }

        String TIPO_CUENTA = filaSolicitud["TIPO_CUENTA"].ToString().Trim();

        String NUM_CUENTA = filaSolicitud["NUM_CUENTA"].ToString().Trim();

        cargar_DropDownList_forma_pago();

        try
        {
            DropDownList_forma_pago.SelectedValue = FORMA_PAGO;
        }
        catch
        {
            DropDownList_forma_pago.SelectedIndex = 0;
        }

        if (FORMA_PAGO_VARIABLE == 1)
        {
            Panel_FORMA_CONSIGNACION_BANCARIA.Visible = true;

            if (CheckBox_TIENE_CUENTA.Checked)
            {
                cargar_DropDownList_entidad_bancaria_todos();

                Panel_CUENTA_BANCARIA.Visible = true;
                CheckBox_TIENE_CUENTA.Checked = true;

                if (String.IsNullOrEmpty(NUM_CUENTA) == false)
                {
                    TextBox_Numero_CuentaS.Text = NUM_CUENTA;
                }
                else
                {
                    TextBox_Numero_CuentaS.Text = "";
                }
            }
            else
            {
                cargar_DropDownList_entidad_bancaria();

                Panel_CUENTA_BANCARIA.Visible = true;
                CheckBox_TIENE_CUENTA.Checked = false;
                TextBox_Numero_CuentaS.Text = NUM_CUENTA;
            }

            cargar_DropDownList_TIPO_CUENTAS();

            try
            {
                DropDownList_entidad_bancaria.SelectedValue = ID_ENTIDAD.ToString();
            }
            catch
            {
                DropDownList_entidad_bancaria.SelectedIndex = 0;
            }

            try
            {
                DropDownList_TIPO_CUENTAS.SelectedValue = TIPO_CUENTA;
            }
            catch
            {
                DropDownList_TIPO_CUENTAS.SelectedIndex = 0;
            }
        }
        else
        {
            Panel_FORMA_CONSIGNACION_BANCARIA.Visible = false;
            Panel_CUENTA_BANCARIA.Visible = false;
            CheckBox_TIENE_CUENTA.Checked = false;
            TextBox_Numero_CuentaS.Text = "";
        }
    }

    private Boolean CargarUbicacionTrabajadorTemporal(Decimal ID_REQUERIMIENTO, Decimal ID_SOLICITUD)
    {
        Boolean resultadoTieneCuenta = false;

        ConRegContratoTemporal _contratoTemporal = new ConRegContratoTemporal(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaInfoTemporal = _contratoTemporal.ObtenerConRegContratosTemporalPorIdRequerimientoIdSolicitud(ID_REQUERIMIENTO, ID_SOLICITUD);

        if (tablaInfoTemporal.Rows.Count <= 0)
        {
            if (_contratoTemporal.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _contratoTemporal.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontro información temporal de contrato. Consulte al Administrador del sistema.", Proceso.Advertencia);
            }

            Panel_informacionUbicacionTrabajador.Visible = false;
        }
        else
        {
            Panel_informacionUbicacionTrabajador.Visible = true;

            DataRow filaInfoContrato = tablaInfoTemporal.Rows[0];

            if (filaInfoContrato["TIENE_CUENTA"].ToString().Trim() == "True")
            {
                resultadoTieneCuenta = true;
            }

            if (DBNull.Value.Equals(filaInfoContrato["ID_SUB_C"]) == false)
            {
                Label_infoUbicacionTrabajador.Text = "UBICACIÓN: SUB CENTRO DE COSTO " + filaInfoContrato["NOMBRE_SUB_C"].ToString().Trim();
                HiddenField_ID_SUB_C_SELECCIONADO.Value = filaInfoContrato["ID_SUB_C"].ToString().Trim();
                HiddenField_ID_CENTRO_C_SELECCIONADO.Value = "0";
                HiddenField_ID_CIUDAD_SELECCIONADA.Value = null;
            }
            else
            {
                if (DBNull.Value.Equals(filaInfoContrato["ID_CENTRO_C"]) == false)
                {
                    Label_infoUbicacionTrabajador.Text = "UBICACIÓN: CENTRO DE COSTO " + filaInfoContrato["NOMBRE_CENTRO_C"].ToString().Trim();
                    HiddenField_ID_SUB_C_SELECCIONADO.Value = "0";
                    HiddenField_ID_CENTRO_C_SELECCIONADO.Value = filaInfoContrato["ID_CENTRO_C"].ToString().Trim();
                    HiddenField_ID_CIUDAD_SELECCIONADA.Value = null;
                }
                else
                {
                    Label_infoUbicacionTrabajador.Text = "UBICACIÓN: CIUDAD " + filaInfoContrato["NOMBRE_CIUDAD"].ToString().Trim();
                    HiddenField_ID_SUB_C_SELECCIONADO.Value = "0";
                    HiddenField_ID_CENTRO_C_SELECCIONADO.Value = "0";
                    HiddenField_ID_CIUDAD_SELECCIONADA.Value = filaInfoContrato["ID_CIUDAD"].ToString().Trim(); ;
                }
            }
        }

        return resultadoTieneCuenta;
    }

    private void Cargar(Decimal ID_REQUERIMIENTO, Decimal ID_SOLICITUD, Decimal ID_EMPRESA, Decimal ID_OCUPACION)
    {
        HiddenField_ID_REQUERIMIENTO.Value = ID_REQUERIMIENTO.ToString();
        HiddenField_ID_SOLICITUD.Value = ID_SOLICITUD.ToString();
        HiddenField_ID_EMPRESA.Value = ID_EMPRESA.ToString();
        HiddenField_ID_OCUPACION.Value = ID_OCUPACION.ToString();

        Ocultar(Acciones.Inicio);

        radicacionHojasDeVida _solIngreso = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        _solIngreso.ActualizarEstadoProcesoRegSolicitudesIngreso(Convert.ToInt32(ID_REQUERIMIENTO), Convert.ToInt32(ID_SOLICITUD), "EN EXAMENES", Session["USU_LOG"].ToString());

        condicionesContratacion CondicionesContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString()); 
        DataTable _tablaReq = CondicionesContratacion.ObtenerComRequerimientoPorIdRequerimiento(ID_REQUERIMIENTO);
        DataRow _filaReq = _tablaReq.Rows[0];
        String CIUDAD_REQ = _filaReq["CIUDAD_REQ"].ToString().Trim();
        HiddenField_CIUDAD_REQ.Value = CIUDAD_REQ;

        perfil _PERFIL = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaPerfil = _PERFIL.ObtenerPorRegistro(Convert.ToInt32(_filaReq["REGISTRO_PERFIL"]));
        DataRow filaPerfil = tablaPerfil.Rows[0];
        Decimal ID_PERFIL = Convert.ToDecimal(_filaReq["REGISTRO_PERFIL"]);
        ID_OCUPACION = Convert.ToDecimal(filaPerfil["ID_OCUPACION"]);
        if (!string.IsNullOrEmpty(filaPerfil["CODIGO"].ToString())) Label_RIESGO.Text =  filaPerfil["CODIGO"].ToString();
        HiddenField_ID_PERFIL.Value = ID_PERFIL.ToString();
        HiddenField_ID_OCUPACION.Value = ID_OCUPACION.ToString();

        DataTable tablasol = _solIngreso.ObtenerRegSolicitudesingresoPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));
        DataRow filaSolIngreso = tablasol.Rows[0];
        String NOMBRE_TRABAJADOR = filaSolIngreso["NOMBRES"].ToString().Trim() + " " + filaSolIngreso["APELLIDOS"].ToString().Trim();
        String NUM_DOC_IDENTIDAD_COMPLETO = filaSolIngreso["TIP_DOC_IDENTIDAD"].ToString().Trim() + " " + filaSolIngreso["NUM_DOC_IDENTIDAD"].ToString().Trim();
        String NUM_DOC_IDENTIDAD = filaSolIngreso["NUM_DOC_IDENTIDAD"].ToString().Trim();
        HiddenField_NUM_DOC_IDENTIDAD.Value = NUM_DOC_IDENTIDAD;

        cliente _empresa = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaEmpresa = _empresa.ObtenerEmpresaConIdEmpresa(ID_EMPRESA);
        DataRow filaEmpresa = tablaEmpresa.Rows[0];
        String RAZ_SOCIAL = filaEmpresa["RAZ_SOCIAL"].ToString().Trim();
        HiddenField_ID_EMPRESA.Value = filaEmpresa["ID_EMPRESA"].ToString().Trim();

        cargo _cargo = new cargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaOcupacion = _cargo.ObtenerOcupacionPorIdOcupacion(ID_OCUPACION);
        DataRow filaOcupacion = tablaOcupacion.Rows[0];
        String CARGO = filaOcupacion["NOM_OCUPACION"].ToString().Trim();

        HiddenField_persona.Value = ID_SOLICITUD.ToString() + "," + ID_REQUERIMIENTO.ToString() + "," + ID_OCUPACION.ToString() + "," + ID_EMPRESA.ToString() + "," + NUM_DOC_IDENTIDAD.Trim();

        cargar_menu_botones_modulos_internos();

        if (!String.IsNullOrEmpty(filaSolIngreso["NUM_CUENTA"].ToString())) CheckBox_TIENE_CUENTA.Checked = true;


        cargarDatosTrabajador(NOMBRE_TRABAJADOR, NUM_DOC_IDENTIDAD_COMPLETO, RAZ_SOCIAL, CARGO, ID_OCUPACION.ToString());

        ordenExamenes _ordenes = new ordenExamenes(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaOrdenes = _ordenes.ObtenerConRegOrdenExamenPorSolicitud(Convert.ToInt32(ID_REQUERIMIENTO), Convert.ToInt32(ID_SOLICITUD)); 

        if (tablaOrdenes.Rows.Count <= 0)
        {
            if (_ordenes.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _ordenes.MensajeError, Proceso.Error);
            }
            else
            {

                Mostrar(Acciones.SeleccionUbicacion);

                HiddenField_ESTADO_PROCESO.Value = AccionesProceso.Paso1ConfigurarExamenes.ToString();

                CargarSeccionUbicacionTrabajador();

                Panel_INFO_ADICIONAL_MODULO.Visible = true;
                Label_INFO_ADICIONAL_MODULO.Text = "SELECCIONE LA UBICACIÓN DONDE TRABAJARÁ LA PERSONA SELECICONADA";
            }
        }
        else
        {
            HiddenField_ESTADO_PROCESO.Value = AccionesProceso.Paso2RegistrarResultadosExamenes.ToString();

            Boolean TIENE_CUENTA = CargarUbicacionTrabajadorTemporal(ID_REQUERIMIENTO, ID_SOLICITUD);

            Cargar_GridView_EXAMENES_REALIZADOS_desde_tabla(tablaOrdenes);

            Cargar_forma_Pago(filaSolIngreso, TIENE_CUENTA);

            Mostrar(Acciones.ResultadosExamenes);

            if (filaSolIngreso["PAR_FORMA_PAGO_VARIABLE"].ToString().Trim() == "1")
            {
                if (TIENE_CUENTA == false)
                {
                    Button_Imprimir_Carta.Visible = true;
                }
                else
                {
                    Button_Imprimir_Carta.Visible = false;
                }
            }
            else
            {
                Button_Imprimir_Carta.Visible = false;
            }

            Panel_INFO_ADICIONAL_MODULO.Visible = false;
            Label_INFO_ADICIONAL_MODULO.Text = "";

            Button_DescartarPorExamenes.Visible = true;
        }
    }

    private void Desactivar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.UbicacionSeleccionada:
                DropDownList_CIUDAD_TRABAJADOR.Enabled = false;
                DropDownList_CC_TRABAJADOR.Enabled = false;
                DropDownList_SUB_CENTRO_TRABAJADOR.Enabled = false;
                break;
        }
    }

    private void ObtenerVariablesUbicacionGlobales()
    {
        if (DropDownList_SUB_CENTRO_TRABAJADOR.SelectedIndex > 0)
        {
            GLO_ID_SUB_C = Convert.ToDecimal(DropDownList_SUB_CENTRO_TRABAJADOR.SelectedValue);
            GLO_ID_CENTRO_C = 0;
            GLO_ID_CIUDAD = null;
        }
        else
        {
            if (DropDownList_CC_TRABAJADOR.SelectedIndex > 0)
            {
                GLO_ID_SUB_C = 0;
                GLO_ID_CENTRO_C = Convert.ToDecimal(DropDownList_CC_TRABAJADOR.SelectedValue);
                GLO_ID_CIUDAD = null;
            }
            else
            {
                GLO_ID_SUB_C = 0;
                GLO_ID_CENTRO_C = 0;
                GLO_ID_CIUDAD = DropDownList_CIUDAD_TRABAJADOR.SelectedValue.Trim();
            }
        }
    }

    private void Cargar_DropDownList_Proveedor(Decimal ID_PRODUCTO, String ID_CIUDAD, DropDownList drop)
    {
        drop.Items.Clear();

        drop.Items.Add(new System.Web.UI.WebControls.ListItem("Seleccione...", ""));

        examenesEmpleado _proveedores = new examenesEmpleado(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable listaProveedores = _proveedores.ObtenerAlmRegProductosProveedorPorProductoRegional(Convert.ToInt32(ID_PRODUCTO), ID_CIUDAD); 

        foreach (DataRow proveedor in listaProveedores.Rows)
        {
            if (DBNull.Value.Equals(proveedor["UBI_SECTOR"]) == false)
            {
                drop.Items.Add(new System.Web.UI.WebControls.ListItem(proveedor["NOM_PROVEEDOR"].ToString().Trim() + " (" + proveedor["NOMBRE_CIUDAD"].ToString().Trim() + " - " + proveedor["UBI_SECTOR"].ToString().Trim() + ")", proveedor["REGISTRO"].ToString().Trim()));
            }
            else
            {
                drop.Items.Add(new System.Web.UI.WebControls.ListItem(proveedor["NOM_PROVEEDOR"].ToString().Trim() + " (" + proveedor["NOMBRE_CIUDAD"].ToString().Trim() + ")", proveedor["REGISTRO"].ToString().Trim()));
            }
        }

        drop.DataBind();
    }

    private void Cargar_GridView_Examenes_Configurados_desde_tabla(DataTable tablaExamenes)
    {

        ObtenerVariablesUbicacionGlobales();

        GridView_Examenes_Configurados.DataSource = tablaExamenes;
        GridView_Examenes_Configurados.DataBind();

        registroContrato _contrato = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablainformacionCiudadCCYSUbCC = _contrato.ObtenerInformacionCompletaIdCiudadIdCentroCIdSubC(GLO_ID_CIUDAD, GLO_ID_CENTRO_C, GLO_ID_SUB_C);

        String ID_CIUDAD = null;

        if (tablainformacionCiudadCCYSUbCC.Rows.Count <= 0)
        {   

            Decimal ID_REQUISICION = Convert.ToDecimal(HiddenField_ID_REQUERIMIENTO.Value);

            requisicion _req = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablareq = _req.ObtenerComRequerimientoPorIdRequerimiento(ID_REQUISICION);
            DataRow filareq = tablareq.Rows[0];

            ID_CIUDAD = filareq["CIUDAD_REQ"].ToString().Trim();

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La ciudad de los laboratorios clínicos se traerá desde la requisición.", Proceso.Correcto);
        }
        else
        { 
            DataRow filaInfoCiudadCentroYSubC = tablainformacionCiudadCCYSUbCC.Rows[0];

            ID_CIUDAD = filaInfoCiudadCentroYSubC["ID_CIUDAD"].ToString();
        }

        for (int i = 0; i < GridView_Examenes_Configurados.Rows.Count; i++)
        {
            DataRow filaTabla = tablaExamenes.Rows[i];

            Decimal ID_PRODUCTO = Convert.ToDecimal(filaTabla["ID_PRODUCTO"]);

            DropDownList dropProveedores = GridView_Examenes_Configurados.Rows[i].FindControl("DropDownList_Proveedor") as DropDownList;
            Cargar_DropDownList_Proveedor(ID_PRODUCTO, ID_CIUDAD, dropProveedores);
        }
    }

    private void cargar_DropDownList_forma_pago()
    {

        DropDownList_forma_pago.Items.Clear();

        DropDownList_forma_pago.Items.Add(new System.Web.UI.WebControls.ListItem("Seleccione...", ""));

        parametro forma_pago = new parametro(Session["idEmpresa"].ToString());
        DataTable TABLA_FORMA_PAGO = forma_pago.ObtenerParametrosPorTabla(tabla.PARAMETROS_FORMA_PAGO);

        foreach (DataRow fila in TABLA_FORMA_PAGO.Rows)
        {
            DropDownList_forma_pago.Items.Add(new System.Web.UI.WebControls.ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString()));
        }

        DropDownList_forma_pago.DataBind();
    }

    private void cargar_DropDownList_entidad_bancaria()
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        String CIUDAD_REQ = HiddenField_CIUDAD_REQ.Value;

        DropDownList_entidad_bancaria.Items.Clear();

        DropDownList_entidad_bancaria.Items.Add(new System.Web.UI.WebControls.ListItem("Seleccione...", ""));

        bancosPorEmpresa _banco = new bancosPorEmpresa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaBancos = _banco.ObtenerconBancoEmpresaPorCiudadEmpresa(CIUDAD_REQ, Convert.ToInt32(ID_EMPRESA));

        foreach (DataRow fila in tablaBancos.Rows)
        {
            DropDownList_entidad_bancaria.Items.Add(new System.Web.UI.WebControls.ListItem(fila["NOM_BANCO"].ToString(), fila["REGISTRO"].ToString()));
        }

        DropDownList_entidad_bancaria.DataBind();
    }

    private void cargar_DropDownList_entidad_bancaria_todos()
    {
        DropDownList_entidad_bancaria.Items.Clear();

        DropDownList_entidad_bancaria.Items.Add(new System.Web.UI.WebControls.ListItem("Seleccione...", ""));

        bancos _banc = new bancos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaBancos = _banc.ObtenerTodosLosBancos();

        foreach (DataRow fila in tablaBancos.Rows)
        {
            DropDownList_entidad_bancaria.Items.Add(new System.Web.UI.WebControls.ListItem(fila["NOM_BANCO"].ToString(), fila["REGISTRO"].ToString()));
        }

        DropDownList_entidad_bancaria.DataBind();
    }

    private void cargar_DropDownList_TIPO_CUENTAS()
    {
        DropDownList_TIPO_CUENTAS.Items.Clear();

        DropDownList_TIPO_CUENTAS.Items.Add(new System.Web.UI.WebControls.ListItem("Seleccione...", ""));

        parametro tipo_parametro = new parametro(Session["idEmpresa"].ToString());
        DataTable TABLA_TIPO_parametro = tipo_parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_TIPO_CUENTA);

        foreach (DataRow fila in TABLA_TIPO_parametro.Rows)
        {
            DropDownList_TIPO_CUENTAS.Items.Add(new System.Web.UI.WebControls.ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString()));
        }

        DropDownList_TIPO_CUENTAS.DataBind();
    }

    private void GuardarPaso1_ConfiguracionExamenes()
    {
        Decimal ID_REQUERIMIENTO = Convert.ToDecimal(HiddenField_ID_REQUERIMIENTO.Value);
        Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        Decimal ID_OCUPACION = Convert.ToDecimal(HiddenField_ID_OCUPACION.Value);

        String ID_CIUDAD_SELECCIONADA = null;
        Decimal ID_CENTRO_C_SELECCIONADO = 0;
        Decimal ID_SUB_C_SELECCIONADO = 0;
        if (HiddenField_ID_SUB_C_SELECCIONADO.Value != "0")
        {
            ID_SUB_C_SELECCIONADO = Convert.ToDecimal(HiddenField_ID_SUB_C_SELECCIONADO.Value);
        }
        else
        {
            if (HiddenField_ID_CENTRO_C_SELECCIONADO.Value != "0")
            {
                ID_CENTRO_C_SELECCIONADO = Convert.ToDecimal(HiddenField_ID_CENTRO_C_SELECCIONADO.Value);
            }
            else
            {
                ID_CIUDAD_SELECCIONADA = HiddenField_ID_CIUDAD_SELECCIONADA.Value;
            }
        }


        String FORMA_PAGO = DropDownList_forma_pago.SelectedValue;
        Decimal ID_ENTIDAD = 0;
        String TIPO_CUENTA = null;
        String NUM_CUENTA = null;
        Boolean TIENE_CUENTA = false;
        
        if ((DropDownList_forma_pago.SelectedValue == "CONSIGNACIÓN BANCARIA") || (DropDownList_forma_pago.SelectedValue == "DISPERSION") || (DropDownList_forma_pago.SelectedValue == "ACH"))
        {
            ID_ENTIDAD = Convert.ToDecimal(DropDownList_entidad_bancaria.SelectedValue);
            TIPO_CUENTA = DropDownList_TIPO_CUENTAS.SelectedValue;

            if (CheckBox_TIENE_CUENTA.Checked == true)
            {
                TIENE_CUENTA = true;
                NUM_CUENTA = TextBox_Numero_CuentaS.Text.Trim();
            }
        }

        examenesEmpleado _REGISTRO = new examenesEmpleado(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        List<examenesEmpleado> listaExamenes = new List<examenesEmpleado>();

        for (int i = 0; i < GridView_Examenes_Configurados.Rows.Count; i++)
        {
            DropDownList dropLab = GridView_Examenes_Configurados.Rows[i].FindControl("DropDownList_Proveedor") as DropDownList;

            Int32 lab = Convert.ToInt32(dropLab.SelectedValue.ToString());
            Int32 id_Examen = Convert.ToInt32(GridView_Examenes_Configurados.DataKeys[i].Values["ID_PRODUCTO"]);
            Decimal registro = _REGISTRO.ObtenerAlmRegProductosProveedorPorProductoProveedor(id_Examen, lab);
            DateTime fecha = System.DateTime.Today;

            examenesEmpleado ex = new examenesEmpleado(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            ex.IdLab = lab;
            ex.IdExamen = id_Examen;
            ex.Fecha = fecha;
            ex.IdRequerimientos = Convert.ToInt32(ID_REQUERIMIENTO);
            ex.IdSolIngreso = Convert.ToInt32(ID_SOLICITUD);
            ex.RegistroAlmacen = Convert.ToInt32(registro);
            ex.Valida = false;
            listaExamenes.Add(ex);
        }

        examenesEmpleado ex1 = new examenesEmpleado(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean resultado = ex1.adicionarOrdenesExamenes(listaExamenes, ID_SOLICITUD, ID_ENTIDAD, NUM_CUENTA, FORMA_PAGO, TIPO_CUENTA, ID_REQUERIMIENTO, ID_CIUDAD_SELECCIONADA, ID_CENTRO_C_SELECCIONADO, ID_SUB_C_SELECCIONADO, 0, ID_EMPRESA, TIENE_CUENTA);

        if (resultado == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, ex1.MensajeError, Proceso.Error);
        }
        else
        {
            Cargar(ID_REQUERIMIENTO, ID_SOLICITUD, ID_EMPRESA, ID_OCUPACION);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Las ordenes de Examenes y la configuración fue realizada correctamente.<br>A continuación imprima la orden de examenes y/o la carta de apertura de cuenta.", Proceso.Correcto);
        }
    }

    private void GuardarPaso2_ResultadosExamenes()
    {
        tools _tools = new tools();

        Int32 ID_REQUERIMIENTO = Convert.ToInt32(HiddenField_ID_REQUERIMIENTO.Value);
        Int32 ID_SOLICITUD = Convert.ToInt32(HiddenField_ID_SOLICITUD.Value);
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        Decimal ID_OCUPACION = Convert.ToDecimal(HiddenField_ID_OCUPACION.Value);

        String FORMA_PAGO = DropDownList_forma_pago.SelectedValue;
        Decimal ID_ENTIDAD = 0;
        String TIPO_CUENTA = null;
        String NUM_CUENTA = null;
        if ((DropDownList_forma_pago.SelectedValue == "CONSIGNACIÓN BANCARIA") || (DropDownList_forma_pago.SelectedValue == "DISPERSION") || (DropDownList_forma_pago.SelectedValue == "ACH"))
        {
            ID_ENTIDAD = Convert.ToDecimal(DropDownList_entidad_bancaria.SelectedValue);
            TIPO_CUENTA = DropDownList_TIPO_CUENTAS.SelectedValue;

            if (CheckBox_TIENE_CUENTA.Checked == true)
            {
                NUM_CUENTA = TextBox_Numero_CuentaS.Text.Trim();
            }

            NUM_CUENTA = TextBox_Numero_CuentaS.Text;
        }

        ordenExamenes _ordenes = new ordenExamenes(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaOrdenes = _ordenes.ObtenerConRegOrdenExamenPorSolicitud(ID_REQUERIMIENTO, ID_SOLICITUD);
        examenesEmpleado _REGISTRO = new examenesEmpleado(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        List<examenesEmpleado> listaExamenesActualizar = new List<examenesEmpleado>();

        for (int i = 0; i < GridView_EXAMENES_REALIZADOS.Rows.Count; i++)
        {
            DataRow filaInfoOrdenExamen = tablaOrdenes.Rows[i];

            Int32 registro = Convert.ToInt32(GridView_EXAMENES_REALIZADOS.DataKeys[i].Values["REGISTRO"]);
            Int32 orden = Convert.ToInt32(GridView_EXAMENES_REALIZADOS.DataKeys[i].Values["ID_ORDEN"]);
            Int32 producto = Convert.ToInt32(GridView_EXAMENES_REALIZADOS.DataKeys[i].Values["ID_PRODUCTO"]);
            CheckBox validar = GridView_EXAMENES_REALIZADOS.Rows[i].FindControl("CheckBox_EXAMENES_ENTREGADOS") as CheckBox;

            Int32 proveedor;
            DataTable tabla = _REGISTRO.ObtenerConRegExamenesEmpleadoPorRegistro(registro);
            DataRow fila = tabla.Rows[0];
            proveedor = Convert.ToInt32(fila["REGISTRO_PRODUCTOS_PROVEEDOR"]);

            if (filaInfoOrdenExamen["VALIDADO"].ToString().Trim() == "N")
            {
                if (validar.Checked == true)
                {
                    DateTime fecha = System.DateTime.Today;

                    TextBox autos = GridView_EXAMENES_REALIZADOS.Rows[i].FindControl("TextBox_Autos_Recomendacion") as TextBox;

                    FileUpload archivoCargado = GridView_EXAMENES_REALIZADOS.Rows[i].FindControl("FileUpload_ARCHIVO") as FileUpload;

                    Byte[] ARCHIVO_EXAMEN = null;
                    Int32 ARCHIVO_TAMANO = 0;
                    String ARCHIVO_EXTENSION = null;
                    String ARCHIVO_TYPE = null;
                    if (archivoCargado.HasFile == true)
                    {
                        using (BinaryReader reader = new BinaryReader(archivoCargado.PostedFile.InputStream))
                        {
                            ARCHIVO_EXAMEN = reader.ReadBytes(archivoCargado.PostedFile.ContentLength);
                            ARCHIVO_TAMANO = archivoCargado.PostedFile.ContentLength;
                            ARCHIVO_TYPE = archivoCargado.PostedFile.ContentType;
                            ARCHIVO_EXTENSION = _tools.obtenerExtensionArchivo(archivoCargado.PostedFile.FileName);
                        }
                    }

                    examenesEmpleado _ex = new examenesEmpleado();

                    _ex.AutoRecomendacion = autos.Text.Trim();
                    _ex.registro = registro;
                    _ex.IdOrden = orden;
                    _ex.ARCHIVO_EXAMEN = ARCHIVO_EXAMEN;
                    _ex.ARCHIVO_EXTENSION = ARCHIVO_EXTENSION;
                    _ex.ARCHIVO_TAMANO = ARCHIVO_TAMANO;
                    _ex.ARCHIVO_TYPE = ARCHIVO_TYPE;
                    _ex.Fecha = fecha;
                    _ex.IdExamen = producto;
                    _ex.IdLab = proveedor;
                    _ex.IdRequerimientos = ID_REQUERIMIENTO;
                    _ex.IdSolIngreso = ID_SOLICITUD;
                    _ex.RegistroAlmacen = registro;
                    _ex.Valida = true;

                    listaExamenesActualizar.Add(_ex);
                }
            }
        }

        examenesEmpleado _examenes = new examenesEmpleado(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean resultado = _examenes.actualizarExamenesYFormaPago(listaExamenesActualizar, ID_SOLICITUD, ID_ENTIDAD, NUM_CUENTA, FORMA_PAGO, TIPO_CUENTA);

        if (resultado == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _examenes.MensajeError, Proceso.Error);
        }
        else
        {
            Cargar(ID_REQUERIMIENTO, ID_SOLICITUD, ID_EMPRESA, ID_OCUPACION);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Los Resultados de los examenes y la configuración de forma de pago fue realizada correctamente.", Proceso.Correcto);
        }
    }

    private void Imprimir(Impresiones impresion)
    {
        String uspReporte = "";
        String nombreReporte = "";

        switch (impresion)
        {
            case Impresiones.OrdenExamen:
                uspReporte = "RPT_CONTRATACION_ORDEN_EXAMEN " 
                    + Session["idEmpresa"].ToString()
                    + ", " + HiddenField_ID_REQUERIMIENTO.Value
                    + ", " + HiddenField_ID_SOLICITUD.Value
                    + ", " + HiddenField_ID_PERFIL.Value;
                nombreReporte = "RPT_CONTRATACION_ORDEN_EXAMEN.rpt";
                break;
            case Impresiones.AutosRecomendacion:
                uspReporte = "RPT_CONTRATACION_AUTOS_RECOMENDACION "
                    + Session["idEmpresa"].ToString()
                    + ", " + HiddenField_ID_REQUERIMIENTO.Value
                    + ", " + HiddenField_ID_SOLICITUD.Value;
                nombreReporte = "RPT_CONTRATACION_AUTOS_RECOMENDACION.rpt";
                break;
        }
        String cadenaDeConeccion = "";
        if (Session["idEmpresa"].ToString() == "1")
        {
            cadenaDeConeccion = ConfigurationManager.ConnectionStrings["siser"].ConnectionString;
        }
        else
        {
            cadenaDeConeccion = ConfigurationManager.ConnectionStrings["sister"].ConnectionString;
        }

        SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(cadenaDeConeccion);
        String user = builder.UserID;
        string pass = builder.Password;
        String server = builder.DataSource;
        String db = builder.InitialCatalog;

        SqlConnection conn = new SqlConnection(cadenaDeConeccion);

        try
        {
            using (SqlCommand comando = new SqlCommand(uspReporte, conn))
            {
                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    DataSet ds = new DataSet();
                    adaptador.Fill(ds);

                    reporte = new ReportDocument();

                    reporte.Load(Server.MapPath("~/Reportes/Contratacion/" + nombreReporte));
                    reporte.SetDataSource(ds.Tables[0]);
                    reporte.DataSourceConnections[0].SetConnection(server, db, user, pass);

                    using (var mStream = (MemoryStream)reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat))
                    {
                        Response.AddHeader("Content-Disposition", "attachment;FileName=RPT_PDF.pdf");
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ContentType = "application/pdf";
                        Response.BinaryWrite(mStream.ToArray());
                    }
                    Response.End();
                } 
            } 
        } 
        catch (Exception ex)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, ex.Message, Proceso.Error);
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
                conn.Close();
            conn.Dispose();
        }

    }

    #endregion metodos

    #region eventos
    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE, Panel_MENSAJES);
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
        TextBox_BUSCAR.Text = "";
    }

    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "CON_FILTRO";
        HiddenField_FILTRO_DROP.Value = DropDownList_BUSCAR.SelectedValue;
        HiddenField_FILTRO_DATO.Value = TextBox_BUSCAR.Text;

        Buscar();
    }

    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
    }

    protected void GridView_RESULTADOS_BUSQUEDA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal ID_REQUERIMIENTO = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_REQUERIMIENTO"]);
            Decimal ID_SOLICITUD = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_SOLICITUD"]);
            Decimal ID_EMPRESA = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_EMPRESA"]);
            Decimal ID_OCUPACION = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_OCUPACION"]);

            Cargar(ID_REQUERIMIENTO, ID_SOLICITUD, ID_EMPRESA, ID_OCUPACION);
        }
    }

    protected void DropDownList_CIUDAD_TRABAJADOR_SelectedIndexChanged(object sender, EventArgs e)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        Decimal ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);

        if (DropDownList_CIUDAD_TRABAJADOR.SelectedIndex <= 0)
        {
            colorear_indicadores_de_ubicacion(false, false, false, System.Drawing.Color.Transparent);
            HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";

            inhabilitar_DropDownList_CENTRO_COSTO();
            inhabilitar_DropDownList_SUB_CENTRO();
        }
        else
        {
            condicionesContratacion _condicionesContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            String ID_CIUDAD = DropDownList_CIUDAD_TRABAJADOR.SelectedValue;

            DataTable tablaCondicionContratacion = _condicionesContratacion.ObtenerCondicionComercialPorIdPerfilIdCiudad(ID_PERFIL, ID_CIUDAD);

            if (tablaCondicionContratacion.Rows.Count <= 0)
            {
                colorear_indicadores_de_ubicacion(true, false, false, System.Drawing.Color.Red);
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";
            }
            else
            {
                colorear_indicadores_de_ubicacion(true, false, false, System.Drawing.Color.Green);
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "S";
            }

            cargar_DropDownList_CENTRO_COSTO(ID_EMPRESA, ID_CIUDAD);
            DropDownList_CC_TRABAJADOR.Enabled = true;
            inhabilitar_DropDownList_SUB_CENTRO();
        }
    }

    protected void DropDownList_CC_TRABAJADOR_SelectedIndexChanged(object sender, EventArgs e)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        Decimal ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);
        String ID_CIUDAD = DropDownList_CIUDAD_TRABAJADOR.SelectedValue;

        condicionesContratacion _condicionesContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        if (DropDownList_CC_TRABAJADOR.SelectedIndex <= 0)
        {
            DataTable tablaCondicionContratacion = _condicionesContratacion.ObtenerCondicionComercialPorIdPerfilIdCiudad(ID_PERFIL, ID_CIUDAD);

            if (tablaCondicionContratacion.Rows.Count <= 0)
            {
                colorear_indicadores_de_ubicacion(true, false, false, System.Drawing.Color.Red);
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";
            }
            else
            {
                colorear_indicadores_de_ubicacion(true, false, false, System.Drawing.Color.Green);
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "S";
            }

            inhabilitar_DropDownList_SUB_CENTRO();
        }
        else
        {
            Decimal ID_CENTRO_C = Convert.ToDecimal(DropDownList_CC_TRABAJADOR.SelectedValue);

            DataTable tablaCondicionContratacion = _condicionesContratacion.ObtenerCondicionComercialPorIdPerfilIdCentroC(ID_PERFIL, ID_CENTRO_C);

            if (tablaCondicionContratacion.Rows.Count <= 0)
            {
                colorear_indicadores_de_ubicacion(false, true, false, System.Drawing.Color.Red);
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";
            }
            else
            {
                colorear_indicadores_de_ubicacion(false, true, false, System.Drawing.Color.Green);
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "S";
            }

            cargar_DropDownList_SUB_CENTRO(ID_EMPRESA, ID_CENTRO_C);
            DropDownList_SUB_CENTRO_TRABAJADOR.Enabled = true;
        }
    }

    protected void DropDownList_SUB_CENTRO_TRABAJADOR_SelectedIndexChanged(object sender, EventArgs e)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        Decimal ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);
        String ID_CIUDAD = DropDownList_CIUDAD_TRABAJADOR.SelectedValue;
        Decimal ID_CENTRO_C = Convert.ToDecimal(DropDownList_CC_TRABAJADOR.SelectedValue);

        condicionesContratacion _condicionesContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        if (DropDownList_SUB_CENTRO_TRABAJADOR.SelectedIndex <= 0)
        {
            DataTable tablaCondicionContratacion = _condicionesContratacion.ObtenerCondicionComercialPorIdPerfilIdCentroC(ID_PERFIL, ID_CENTRO_C);

            if (tablaCondicionContratacion.Rows.Count <= 0)
            {
                colorear_indicadores_de_ubicacion(false, true, false, System.Drawing.Color.Red);
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";
            }
            else
            {
                colorear_indicadores_de_ubicacion(false, true, false, System.Drawing.Color.Green);
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "S";
            }
        }
        else
        {
            Decimal ID_SUB_C = Convert.ToDecimal(DropDownList_SUB_CENTRO_TRABAJADOR.SelectedValue);

            DataTable tablaCondicionContratacion = _condicionesContratacion.ObtenerCondicionComercialPorIdPerfilIdSubC(ID_PERFIL, ID_SUB_C);

            if (tablaCondicionContratacion.Rows.Count <= 0)
            {
                colorear_indicadores_de_ubicacion(false, false, true, System.Drawing.Color.Red);
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";
            }
            else
            {
                colorear_indicadores_de_ubicacion(false, false, true, System.Drawing.Color.Green);
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "S";
            }
        }
    }

    protected void Button_ACEPTAR_UBICACION_Click(object sender, EventArgs e)
    {
        if (HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value == "S")
        {
            Decimal ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);
            Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);

            ObtenerVariablesUbicacionGlobales();

            HiddenField_ID_CIUDAD_SELECCIONADA.Value = GLO_ID_CIUDAD;
            HiddenField_ID_CENTRO_C_SELECCIONADO.Value = GLO_ID_CENTRO_C.ToString();
            HiddenField_ID_SUB_C_SELECCIONADO.Value = GLO_ID_SUB_C.ToString();

            cargar_menu_botones_modulos_internos();

            Ocultar(Acciones.UbicacionSeleccionada);
            Desactivar(Acciones.UbicacionSeleccionada);
            Mostrar(Acciones.UbicacionSeleccionada);

            radicacionHojasDeVida _sol = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaSol = _sol.ObtenerRegSolicitudesingresoPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));
            DataRow filaSol = tablaSol.Rows[0];

            configuracionElementosPerfil _examenes = new configuracionElementosPerfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaExamenes = _examenes.ObtenerConRegElementosTrabajoExamenes(Convert.ToInt32(ID_PERFIL), GLO_ID_CIUDAD, Convert.ToInt32(GLO_ID_CENTRO_C), Convert.ToInt32(GLO_ID_SUB_C), Convert.ToInt32(0), filaSol["SEXO"].ToString());

            Cargar_GridView_Examenes_Configurados_desde_tabla(tablaExamenes);

            GridView_EXAMENES_REALIZADOS.DataSource = null;
            GridView_EXAMENES_REALIZADOS.DataBind();

            cargar_DropDownList_forma_pago();

        }
        else
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La ubicación seleccionada no tiene configurada condiciones de contratación para el cargo.", Proceso.Advertencia);
        }
    }

    protected void DropDownList_forma_pago_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_forma_pago.SelectedIndex <= 0)
        {
            Panel_FORMA_CONSIGNACION_BANCARIA.Visible = false;
            
            Panel_CUENTA_BANCARIA.Visible = false;
            TextBox_Numero_CuentaS.Text = "";
        }
        else
        {
            if ((DropDownList_forma_pago.SelectedValue == "CONSIGNACIÓN BANCARIA") || (DropDownList_forma_pago.SelectedValue == "DISPERSION") || (DropDownList_forma_pago.SelectedValue == "ACH"))
            {
                Panel_FORMA_CONSIGNACION_BANCARIA.Visible = true;

                CheckBox_TIENE_CUENTA.Checked = false;

                if (HiddenField_ESTADO_PROCESO.Value == AccionesProceso.Paso1ConfigurarExamenes.ToString())
                {
                    Panel_CUENTA_BANCARIA.Visible = false;
                }
                else
                {
                    Panel_CUENTA_BANCARIA.Visible = true;
                }

                TextBox_Numero_CuentaS.Text = "";

                cargar_DropDownList_entidad_bancaria();
                cargar_DropDownList_TIPO_CUENTAS();
            }
            else
            {
                Panel_FORMA_CONSIGNACION_BANCARIA.Visible = false;

                Panel_CUENTA_BANCARIA.Visible = false;
                TextBox_Numero_CuentaS.Text = "";
            }
        }
    }

    protected void CheckBox_TIENE_CUENTA_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_TIENE_CUENTA.Checked)
        {
            cargar_DropDownList_entidad_bancaria_todos();
            Panel_CUENTA_BANCARIA.Visible = true;
            TextBox_Numero_CuentaS.Text = "";
        }
        else
        {
            cargar_DropDownList_entidad_bancaria();
            Panel_CUENTA_BANCARIA.Visible = false;
        }
    }

    protected void Button_Guardar_PROCESO_Click(object sender, EventArgs e)
    {
        if (HiddenField_ESTADO_PROCESO.Value == AccionesProceso.Paso1ConfigurarExamenes.ToString())
        {
            GuardarPaso1_ConfiguracionExamenes();
        }
        else
        {
            GuardarPaso2_ResultadosExamenes();
        }
    }

    protected void Button_Imprimir_Click(object sender, EventArgs e)
    {
        if (HiddenField_ESTADO_PROCESO.Value != AccionesProceso.Paso2RegistrarResultadosExamenes.ToString()) 
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se puede imprimir las ordenes de examenes, no se ha realizado la configuración de Examenes medicos. Verifique por favor", Proceso.Advertencia);
        else Imprimir(Impresiones.OrdenExamen);
    }

    protected void Button_Imprimir_Carta_Click(object sender, EventArgs e)
    {
        cargar_menu_botones_modulos_internos();

        if (HiddenField_ESTADO_PROCESO.Value != AccionesProceso.Paso2RegistrarResultadosExamenes.ToString())
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se puede imprimir la carta de apertura de cuenta, no se ha realizado la configuración de forma de pago. Verifique por favor", Proceso.Advertencia);
        }
        else
        {

            Byte[] archivo_apertura;

            maestrasInterfaz _maestra = new maestrasInterfaz();

            parametro _parametro = new parametro(Session["idEmpresa"].ToString());
            DataTable tablaConfiguracionCuentas = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_CUENTA_FORMATO_APERTURA_CUENTA);

            if (tablaConfiguracionCuentas.Rows.Count <= 0)
            {
                if (_parametro.MensajeError != null)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _parametro.MensajeError, Proceso.Error);
                }
                else
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se ha configurado los parametros de cuentas asociadas para pago de nómina.", Proceso.Error);
                }
            }
            else
            {
                Boolean cuentaEncontrada = false;

                String datosCuenta = String.Empty;

                if (DropDownList_entidad_bancaria.SelectedValue == "16")
                {
                    foreach (DataRow filaCuenta in tablaConfiguracionCuentas.Rows)
                    {
                        if (Session["idEmpresa"].ToString() == "1")
                        {
                            Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);
                            Decimal ID_REQUERIMIENTO = Convert.ToDecimal(HiddenField_ID_REQUERIMIENTO.Value);

                            if (filaCuenta["VARIABLE"].ToString().Trim() == "CUENTA_BANCO_AVVILLAS_SERTEMPO")
                            {
                                datosCuenta = filaCuenta["CODIGO"].ToString();
                                cuentaEncontrada = true;

                                String[] datosArray = datosCuenta.Split('*');


                                archivo_apertura = _maestra.GenerarPDFAperturaBancoAvvilla("Bogotá", DateTime.Now, DropDownList_entidad_bancaria.SelectedItem.Text, Label_NOMBRE_TRABAJADOR.Text, Label_NUM_DOC_IDENTIDAD.Text, Label_NOMBRE_TRABAJADOR.Text, ID_SOLICITUD, ID_REQUERIMIENTO, "Nombre:.", "Cargo:.", datosArray[0]);

                                String filename = "apertura_cuenta_" + DropDownList_entidad_bancaria.SelectedItem.Text.Trim();
                                filename = filename.Replace(' ', '_');

                                Response.Clear();
                                Response.BufferOutput = false; 
                                Response.ContentType = "application/pdf";
                                Response.AddHeader("Content-Disposition", "attachment;FileName=" + filename + ".pdf");
                                Response.BinaryWrite(archivo_apertura);
                                Response.End();

                                break;
                            }
                        }
                        else
                        {
                        }

                    }
                }
                else
                {
                    if (DropDownList_entidad_bancaria.SelectedValue == "1")
                    {
                        foreach (DataRow filaCuenta in tablaConfiguracionCuentas.Rows)
                        {
                            if (Session["idEmpresa"].ToString() == "1")
                            {
                                if (filaCuenta["VARIABLE"].ToString().Trim() == "CUENTA_BANCO_BOGOTA_SERTEMPO")
                                {
                                    datosCuenta = filaCuenta["CODIGO"].ToString();
                                    cuentaEncontrada = true;

                                    String[] datosArray = datosCuenta.Split('*');

                                    archivo_apertura = _maestra.GenerarPDFAperturaBancoBogotaCreditRotativo(DateTime.Now, DropDownList_entidad_bancaria.SelectedItem.Text, Label_NOMBRE_TRABAJADOR.Text, Label_NUM_DOC_IDENTIDAD.Text, "JOSE VIDAL HERNANDEZ SUAREZ", "Jefe Nacional de Contratación", "TEL: 3217088 Ext. 1211", datosArray[0], datosArray[1], datosArray[2]);

                                    String filename = "apertura_cuenta_" + DropDownList_entidad_bancaria.SelectedItem.Text.Trim();
                                    filename = filename.Replace(' ', '_');

                                    Response.Clear();
                                    Response.BufferOutput = false; 
                                    Response.ContentType = "application/pdf";
                                    Response.AddHeader("Content-Disposition", "attachment;FileName=" + filename + ".pdf");
                                    Response.BinaryWrite(archivo_apertura);
                                    Response.End();

                                    break;
                                }
                            }
                            else
                            {
                            }

                        }
                    }
                    else
                    {
                        if (DropDownList_entidad_bancaria.SelectedValue == "4")
                        {
                            foreach (DataRow filaCuenta in tablaConfiguracionCuentas.Rows)
                            {
                                if (Session["idEmpresa"].ToString() == "1")
                                {
                                    if (filaCuenta["VARIABLE"].ToString().Trim() == "CUENTA_BANCO_BANCOLOMBIA_SERTEMPO")
                                    {
                                        datosCuenta = filaCuenta["CODIGO"].ToString();
                                        cuentaEncontrada = true;

                                        Decimal ID_REQUERIMIENTO = Convert.ToDecimal(HiddenField_ID_REQUERIMIENTO.Value);

                                        String[] datosArray = datosCuenta.Split('*');

                                        archivo_apertura = _maestra.GenerarPDFAperturaBancoBancolombia("Bogotá", DateTime.Now, DropDownList_entidad_bancaria.SelectedItem.Text, datosArray[0], datosArray[1], Label_NOMBRE_TRABAJADOR.Text, Label_NUM_DOC_IDENTIDAD.Text, Label_CARGO.Text, ID_REQUERIMIENTO, Label_RAZ_SOCIAL.Text, "SERVICIOS TEMPORALES PROFESIONALES BOGOTA S.A.", "AREA CONTRATACIÓN");

                                        String filename = "apertura_cuenta_" + DropDownList_entidad_bancaria.SelectedItem.Text.Trim();
                                        filename = filename.Replace(' ', '_');

                                        Response.Clear();
                                        Response.BufferOutput = false; 
                                        Response.ContentType = "application/pdf";
                                        Response.AddHeader("Content-Disposition", "attachment;FileName=" + filename + ".pdf");
                                        Response.BinaryWrite(archivo_apertura);
                                        Response.End();

                                        break;
                                    }
                                }
                                else
                                {
                                }

                            }
                        }
                        else
                        {

                        }
                    }
                }
                if (String.IsNullOrEmpty(datosCuenta) == false)
                {

                }
                else
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información de cuenta para el banco seleccionado.", Proceso.Advertencia);
                }
            }
        }
    }

    protected void Button_DescartarPorExamenes_Click(object sender, EventArgs e)
    {
        Panel_informarDescarte.Visible = true;
        TextBox_ObservacionesDescarte.Text = "";

        TextBox_ObservacionesDescarte.Focus();
    }

    protected void Button_InformarDescarte_Click(object sender, EventArgs e)
    {
        Decimal ID_REQUERIMIENTO = Convert.ToDecimal(HiddenField_ID_REQUERIMIENTO.Value);
        Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);

        ConRegContratoTemporal _contratoTemporal = new ConRegContratoTemporal(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        Boolean correcto = _contratoTemporal.DescartarConRegContratosTemporal(ID_REQUERIMIENTO, ID_SOLICITUD, TextBox_ObservacionesDescarte.Text.Trim());

        if (correcto == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _contratoTemporal.MensajeError, Proceso.Error);
        }
        else
        {
            parametro _parametro = new parametro(Session["idEmpresa"].ToString());

            DataTable tablaRolParaInformar = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_ROL_INFORMAR_DESCARTE_PROCESO_CONTRATACION);

            if (tablaRolParaInformar.Rows.Count <= 0)
            {
                tools _tools = new tools();
                SecureQueryString QueryStringSeguro;
                QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

                QueryStringSeguro["img_area"] = "contratacion";
                QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
                QueryStringSeguro["nombre_modulo"] = "HOJA DE TRABAJO DE CONTRATACIÓN";
                QueryStringSeguro["accion"] = "descarteContratacionNoRol";

                Response.Redirect("~/contratacion/hojaTrabajoContratacion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
            }
            else
            {
                DataRow filaParametroRol = tablaRolParaInformar.Rows[0];

                String NOMBRE_ROL = filaParametroRol["CODIGO"].ToString().Trim();

                usuario _usuario = new usuario(Session["idEmpresa"].ToString());
                DataTable tablaUsuariosAInformar = _usuario.ObtenerListaUsuariosSistemaActivosPorNombreRol(NOMBRE_ROL);

                if (tablaUsuariosAInformar.Rows.Count <= 0)
                {
                    tools _tools = new tools();
                    SecureQueryString QueryStringSeguro;
                    QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

                    QueryStringSeguro["img_area"] = "contratacion";
                    QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
                    QueryStringSeguro["nombre_modulo"] = "HOJA DE TRABAJO DE CONTRATACIÓN";
                    QueryStringSeguro["accion"] = "descarteContratacionNoUsuarios";

                    Response.Redirect("~/contratacion/hojaTrabajoContratacion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
                }
                else
                {
                    Int32 contadorEnvios = 0;
                    Int32 contadorErrores = 0;

                    StreamReader archivo_original = new StreamReader(Server.MapPath(@"~\plantillas_reportes\email_informativo_descarte_contratacion.htm"));
                    String html_formato_plantilla_email = archivo_original.ReadToEnd();
                    archivo_original.Dispose();
                    archivo_original.Close();

                    String NOMBRE_CANDIDATO = Label_NOMBRE_TRABAJADOR.Text;
                    String NUMERO_IDENTIFICACION = Label_NUM_DOC_IDENTIDAD.Text;
                    String OBSERVACIONES_DESCARTE = TextBox_ObservacionesDescarte.Text.Trim();

                    String NOMBRE_USUARIO;
                    DataTable tablaUsuario = _usuario.ObtenerUsuarioPorUsuLog(Session["USU_LOG"].ToString());
                    DataRow filaUsuario = tablaUsuario.Rows[0];

                    if (filaUsuario["USU_TIPO"].ToString().ToUpper() == "PLANTA")
                    {
                        NOMBRE_USUARIO = filaUsuario["NOMBRES"].ToString().Trim() + " " + filaUsuario["APELLIDOS"].ToString().Trim();
                    }
                    else
                    {
                        NOMBRE_USUARIO = filaUsuario["NOMBRES_EXTERNO"].ToString().Trim() + " " + filaUsuario["APELLIDOS_EXTERNO"].ToString().Trim();
                    }

                    html_formato_plantilla_email = html_formato_plantilla_email.Replace("[NOMBRE_CANDIDATO]", NOMBRE_CANDIDATO);
                    html_formato_plantilla_email = html_formato_plantilla_email.Replace("[NUMERO_IDENTIFICACION]", NUMERO_IDENTIFICACION);
                    html_formato_plantilla_email = html_formato_plantilla_email.Replace("[OBSERVACIONES_DESCARTE]", OBSERVACIONES_DESCARTE);
                    html_formato_plantilla_email = html_formato_plantilla_email.Replace("[NOMBRE_USUARIO]", NOMBRE_USUARIO);

                    tools _tools = new tools();

                    foreach (DataRow filausuario in tablaUsuariosAInformar.Rows)
                    {
                        if (DBNull.Value.Equals(filausuario["USU_MAIL"]) == false)
                        {
                            try
                            {
                                _tools.enviarCorreoConCuerpoHtml(filausuario["USU_MAIL"].ToString().Trim(), "DESCARTE EN CONTRATACIÓN", html_formato_plantilla_email);
                                contadorEnvios += 1;
                            }
                            catch
                            {
                                contadorErrores += 1;
                            }
                        }
                        else
                        {
                            contadorErrores += 1;
                        }
                    }

                    if (contadorEnvios <= 0)
                    {
                        SecureQueryString QueryStringSeguro;
                        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

                        QueryStringSeguro["img_area"] = "contratacion";
                        QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
                        QueryStringSeguro["nombre_modulo"] = "HOJA DE TRABAJO DE CONTRATACIÓN";
                        QueryStringSeguro["accion"] = "descarteContratacionNoEmail";

                        Response.Redirect("~/contratacion/hojaTrabajoContratacion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
                    }
                    else
                    {
                        SecureQueryString QueryStringSeguro;
                        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

                        QueryStringSeguro["img_area"] = "contratacion";
                        QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
                        QueryStringSeguro["nombre_modulo"] = "HOJA DE TRABAJO DE CONTRATACIÓN";
                        QueryStringSeguro["accion"] = "descarteContratacionOk";

                        Response.Redirect("~/contratacion/hojaTrabajoContratacion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
                    }
                }
            }
        }
    }

    protected void Button_Imprimir_Autos_Click(object sender, EventArgs e)
    {
        Imprimir(Impresiones.AutosRecomendacion);
    }

    #endregion eventos

}