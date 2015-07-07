using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using Brainsbits.LLB;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.comercial;
using Brainsbits.LLB.seleccion;
using Brainsbits.LLB.contratacion;

using TSHAK.Components;
using System.IO;
using Brainsbits.LLB.seguridad;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using Brainsbits.LLB.nomina;
using Brainsbits.LLB.operaciones;
using ICSharpCode.SharpZipLib.Zip;
using System.Configuration;

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
    private System.Drawing.Color colorMorado = System.Drawing.ColorTranslator.FromHtml("#7100E1");
    
    private System.Drawing.Color colorSeleccionado = System.Drawing.ColorTranslator.FromHtml("#699DF0");

    private System.Drawing.Color colorSinAuditar = System.Drawing.ColorTranslator.FromHtml("#FFB3B3");
    private System.Drawing.Color colorAuditado = System.Drawing.ColorTranslator.FromHtml("#B7FFB7");

    private enum Acciones
    {
        Inicio = 0,
        buscarContratos,
        contratosEncontrados,
        contratoSeleccionado,
        libretaMilitar, 
        auditoriaCompleta, 
        pensionado,
        nuevoConcepto,
        conceptosIncluidos, 
        contrato,
        CargarEnvioArchivos,
        BuscarAuditados,
        BuscarSinAuditar
    }
    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia
    }
    private enum Presentacion
    { 
        HojaDeTrabajo = 1,
        ContratosActivos = 2
    }
    private enum EntidadesAfiliacion
    { 
        ARP = 0,
        EPS = 1,
        CAJA = 2,
        AFP = 3
    }
    private enum TipoBusqueda
    {
        POR_AUDITAR = 0,
        YA_AUDITADOS = 1
    }
    private enum SeccionesAuditoria
    {
        SolicitudIngreso = 0,
        AfiliacionARP,
        AfiliacionEPS,
        AfiliacionCCF,
        AfiliacionAFP,
        Contrato, 
        ConceptosFijos, 
        ExamenesAutosRecomendacion, 
        EnvioArchivos
    }
    private enum AccionesEnvio
    {
        correo = 0,
        download
    }
    private enum SeccionEnvio
    { 
        Seleccion = 0,
        Contratacion
    }
    #endregion variables

    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = false;

                Panel_OPCION_TIPO_HOJA.Visible = false;
                Panel_LINK_HOJA_TRABAJO.Visible = false;
                Panel_LINK_SELECCIONAR_CONTRATO.Visible = false;
                
                Button_AUDITADO_2.Visible = false;


                Panel_MENSAJES.Visible = false;

                Panel_RESULTADOS_BUSQUEDA_CONTRATOS.Visible = false;

                Panel_RESULTADOS_GRID.Visible = false;

                Panel_PRINCIPAL.Visible = false;

                Panel_INFORMACION_FINAL.Visible = false;

                Panel_FORM_BOTONES_1.Visible = false;
                Button_AUDITADO_1.Visible = false;
                Button_VOLVER_1.Visible = false;

                panel_TIPO_PENSIONADO.Visible = false;

                Panel_MENSAJES_CONCEPTOS_FIJOS.Visible = false;
                Panel_CONCEPTOS_FIJOS_PARAMETRIZADOS.Visible = false;
                Panel_NUEVO_CONCEPTO_FIJO.Visible = false;
                CheckBox_PER_1.Visible = false;
                CheckBox_PER_2.Visible = false;
                CheckBox_PER_3.Visible = false;
                CheckBox_PER_4.Visible = false;

                Panel_ARCHIVO_AFILIACION_ARP.Visible = false;
                Panel_SUBIR_ARCHIVO_AFILIACION_ARP.Visible = false;
                Panel_ARCHIVO_AFILIACION_EPS.Visible = false;
                Panel_SUBIR_ARCHIVO_AFILIACION_EPS.Visible = false;
                Panel_ARCHIVO_AFILIACION_CAJA.Visible = false;
                Panel_SUBIR_ARCHIVO_AFILIACION_CAJA.Visible = false;
                Panel_ARCHIVO_AFILIACION_AFP.Visible = false;
                Panel_SUBIR_ARCHIVO_AFILIACION_AFP.Visible = false;

                Panel_MENSAJE_EXAMENES.Visible = false;
                GridView_EXAMENES_REALIZADOS.Enabled = false;
                Button_IMPRIMIR_AUTOS.Visible = false;

                break;
            case Acciones.libretaMilitar:
                Panel_LIB_MILITAR.Visible = false;
                break;
            case Acciones.auditoriaCompleta:
                Button_AUDITADO_1.Visible = false;
                Button_AUDITADO_2.Visible = false;

                CollapsiblePanelExtender_SOLICITUD_INGRESO.Collapsed = true;
                CollapsiblePanelExtender_SOLICITUD_INGRESO.ClientState = "true";

                CollapsiblePanelExtender_AFILIACION_ARP.Collapsed = true;
                CollapsiblePanelExtender_AFILIACION_ARP.ClientState = "true";

                CollapsiblePanelExtender_AFILIACION_EPS.Collapsed = true;
                CollapsiblePanelExtender_AFILIACION_EPS.ClientState = "true";

                CollapsiblePanelExtender_AFILIACION_CCF.Collapsed = true;
                CollapsiblePanelExtender_AFILIACION_CCF.ClientState = "true";

                CollapsiblePanelExtender_AFILIACION_AFP.Collapsed = true;
                CollapsiblePanelExtender_AFILIACION_AFP.ClientState = "true";

                CollapsiblePanelExtender_CONCEPTOS_FIJOS.Collapsed = true;
                CollapsiblePanelExtender_CONCEPTOS_FIJOS.ClientState = "true";

                CollapsiblePanelExtender_CONTRATO.Collapsed = true;
                CollapsiblePanelExtender_CONTRATO.ClientState = "true";

                CollapsiblePanelExtender_EXAMENES.Collapsed = true;
                CollapsiblePanelExtender_EXAMENES.ClientState = "true";

                Panel_INFORMACION_FINAL.Visible = false;
                break;
            case Acciones.pensionado:
                panel_TIPO_PENSIONADO.Visible = false;
                break;
            case Acciones.nuevoConcepto:
                Panel_MENSAJES.Visible = false;
                Panel_MENSAJES_CONCEPTOS_FIJOS.Visible = false;
                Panel_NUEVO_CONCEPTO_FIJO.Visible = false;
                CheckBox_PER_1.Visible = false;
                CheckBox_PER_2.Visible = false;
                CheckBox_PER_3.Visible = false;
                CheckBox_PER_4.Visible = false;
                Button_ACTUALZIAR_CONCEPTOS_FIJOS.Visible = false;
                break;
            case Acciones.conceptosIncluidos:
                Panel_CONCEPTOS_FIJOS_PARAMETRIZADOS.Visible = false;
                break;
            case Acciones.contrato:
                Panel_MENSAJE_CONTRATO.Visible = false;
                Panel_SALARIO_NOM_VALOR_UNIDAD.Visible = false;
                Panel_SALARIO.Visible = false;
                RadioButtonList_ESTADOS_SENA.Enabled = false;
                break;
            case Acciones.CargarEnvioArchivos:
                Panel_MENSAJE_ENVIOARCHOVOS.Visible = false;
                Panel_SELECCION_DE_ARCHIVOS_A_ENVIAR.Visible = false;
                Button_REVISAR_DOCUMENTOS.Visible = false;
                Button_ACTUALIZAR_ENVIOARCHIVOS.Visible = false;
                break;
        }
    }
    
    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_OPCION_TIPO_HOJA.Visible = true;
                Panel_LINK_SELECCIONAR_CONTRATO.Visible = true;


                Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.buscarContratos:
                Panel_OPCION_TIPO_HOJA.Visible = true;
                Panel_LINK_HOJA_TRABAJO.Visible = true;
                Panel_FORM_BOTONES_Y_BUSQUEDA.Visible = true;
                break;
            case Acciones.contratosEncontrados:
                Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.contratoSeleccionado:
                Panel_INFO_ADICIONAL_MODULO.Visible = true;

                Panel_OPCION_TIPO_HOJA.Visible = true;
                if (HiddenField_PRESENTACION.Value == Presentacion.ContratosActivos.ToString())
                {
                    LinkButton_LINK_HOJA_TRABAJO.Visible = true;
                    Panel_FORM_BOTONES_Y_BUSQUEDA.Visible = true;
                }
                else
                {
                    LinkButton_LINK_SELECCIONAR_CONTRATO.Visible = true;
                }

                Panel_PRINCIPAL.Visible = true;

                Panel_FORM_BOTONES_1.Visible = true;

                Button_VOLVER_1.Visible = true;
                Button_VOLVER_2.Visible = true;

                Panel_SUBIR_ARCHIVO_AFILIACION_ARP.Visible = true;
                Panel_SUBIR_ARCHIVO_AFILIACION_EPS.Visible = true;
                Panel_SUBIR_ARCHIVO_AFILIACION_CAJA.Visible = true;
                Panel_SUBIR_ARCHIVO_AFILIACION_AFP.Visible = true;

                break;
            case Acciones.libretaMilitar:
                Panel_LIB_MILITAR.Visible = true;
                break;
            case Acciones.auditoriaCompleta:
                Button_AUDITADO_1.Visible = true;
                Button_AUDITADO_2.Visible = true;

                Panel_INFORMACION_FINAL.Visible = true;
                break;
            case Acciones.pensionado:
                panel_TIPO_PENSIONADO.Visible = true;
                break;
            case Acciones.nuevoConcepto:
                Panel_NUEVO_CONCEPTO_FIJO.Visible = true;
                break;
            case Acciones.conceptosIncluidos:
                Panel_CONCEPTOS_FIJOS_PARAMETRIZADOS.Visible = true;
                break;
            case Acciones.CargarEnvioArchivos:
                Panel_SELECCION_DE_ARCHIVOS_A_ENVIAR.Visible = true;
                Button_REVISAR_DOCUMENTOS.Visible = true;
                Button_ACTUALIZAR_ENVIOARCHIVOS.Visible = true;
                break;

        }
    }

    private Boolean SoloFaltaEnvioDocumentos(DataRow fila)
    {
        Boolean resultado = true;

        if (Convert.ToInt32(fila["AUDITORIA_CON_AFILIACION_ARP"]) <= 0)
        {
            resultado = false;
        }

        if (Convert.ToInt32(fila["AUDITORIA_CON_AFILIACION_CAJAS_C"]) <= 0)
        {
            resultado = false;
        }

        if (Convert.ToInt32(fila["AUDITORIA_CON_AFILIACION_EPS"]) <= 0)
        {
            resultado = false;
        }

        if (Convert.ToInt32(fila["AUDITORIA_CON_AFILIACION_F_PENSIONES"]) <= 0)
        {
            resultado = false;
        }

        if (Convert.ToInt32(fila["AUDITORIA_CON_REG_CONTRATOS"]) <= 0)
        {
            resultado = false;
        }

        if (Convert.ToInt32(fila["AUDITORIA_CON_REG_EXAMENES_EMPLEADO"]) <= 0)
        {
            resultado = false;
        }

        if (Convert.ToInt32(fila["AUDITORIA_NOM_CONCEPTOS_EMPLEADO"]) <= 0)
        {
            resultado = false;
        }

        if (Convert.ToInt32(fila["AUDITORIA_REG_SOLICITUDES_INGRESO"]) <= 0)
        {
            resultado = false;
        }

        if (Convert.ToInt32(fila["AUDITORIA_CON_CONFIGURACON_DOCS_ENTREGABLES"]) > 0)
        {
            resultado = false;
        }

        return resultado;
    }

    private void colorear_filas_grilla_hoja_trabajo(DataTable tablaAuditoria)
    {
        int contadorVerdes = 0;
        int contadorAmarillos = 0;
        int contadorRojos = 0;
        int contadorMorados = 0;

        for (int i = 0; i < GridView_HOJA_DE_TRABAJO.Rows.Count; i++)
        {
            DataRow fila = tablaAuditoria.Rows[(GridView_HOJA_DE_TRABAJO.PageIndex * GridView_HOJA_DE_TRABAJO.PageSize) + i];

            if (SoloFaltaEnvioDocumentos(fila) == false)
            {
                if (fila["ALERTA"].ToString() == "ALTA")
                {
                    GridView_HOJA_DE_TRABAJO.Rows[i].BackColor = colorNo;
                }
                else
                {
                    if (fila["ALERTA"].ToString() == "MEDIA")
                    {
                        GridView_HOJA_DE_TRABAJO.Rows[i].BackColor = colorMedio;
                    }
                    else
                    {
                        GridView_HOJA_DE_TRABAJO.Rows[i].BackColor = colorSi;
                    }
                }
            }
            else
            {
                GridView_HOJA_DE_TRABAJO.Rows[i].BackColor = colorMorado;
            }
        }


        for (int i = 0; i < tablaAuditoria.Rows.Count; i++)
        {
            DataRow fila = tablaAuditoria.Rows[i];

            if (SoloFaltaEnvioDocumentos(fila) == false)
            {
                if (fila["ALERTA"].ToString() == "ALTA")
                {
                    contadorRojos += 1;
                }
                else
                {
                    if (fila["ALERTA"].ToString() == "MEDIA")
                    {
                        contadorAmarillos += 1;
                    }
                    else
                    {
                        contadorVerdes += 1;
                    }
                }
            }
            else
            {
                contadorMorados += 1;
            }
        }

        Label_ALERTA_BAJA.Text = contadorVerdes.ToString();
        Label_ALERTA_MEDIA.Text = contadorAmarillos.ToString();
        Label_ALERTA_ALTA.Text = contadorRojos.ToString();
        Label_ALERTA_ENVIO.Text = contadorMorados.ToString();
    }
    
    private void cargar_hoja_trabajo_auditoria()
    {
        
        requisicion _requisicion = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        
        DataTable tablaInfoAuditoria = _requisicion.ObtenerPersonasPorAuditar();

        GridView_HOJA_DE_TRABAJO.DataSource = tablaInfoAuditoria;
        GridView_HOJA_DE_TRABAJO.DataBind();

        colorear_filas_grilla_hoja_trabajo(tablaInfoAuditoria);
    }
    
    private void cargar_DropDownList_BUSCAR()
    {
        DropDownList_BUSCAR.Items.Clear();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Ninguno", "");
        DropDownList_BUSCAR.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("Número identificación", "NUM_DOC_IDENTIDAD");
        DropDownList_BUSCAR.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("Nombres", "NOMBRES");
        DropDownList_BUSCAR.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("Apellidos", "APELLIDOS");
        DropDownList_BUSCAR.Items.Add(item);

        DropDownList_BUSCAR.DataBind();
    }
    
    private void configurarCaracteresAceptadosBusqueda(Boolean letras, Boolean numeros)
    {
        FilteredTextBoxExtender_TextBox_BUSCAR_Numbers.Enabled = numeros;
        FilteredTextBoxExtender_TextBox_BUSCAR_Letras.Enabled = letras;
    }
    
    private void cargar_DropDownList_PERIODO_PAGO(DropDownList drop)
    {
        drop.Items.Clear();
        parametro _parametro = new parametro(Session["idEmpresa"].ToString());
        DataTable tablaParametro = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_PERIODO_PAGO);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione", "");
        drop.Items.Add(item);

        foreach (DataRow fila in tablaParametro.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            drop.Items.Add(item);
        }

        drop.DataBind();
    }
    
    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:

                HiddenField_PRESENTACION.Value = Presentacion.HojaDeTrabajo.ToString();

                HiddenField_TIPO_BUSQUEDA.Value = TipoBusqueda.POR_AUDITAR.ToString();

                cargar_hoja_trabajo_auditoria();
                cargar_DropDownList_BUSCAR();
                break;
            case Acciones.buscarContratos:
                HiddenField_PRESENTACION.Value = Presentacion.ContratosActivos.ToString();

                HiddenField_TIPO_BUSQUEDA.Value = TipoBusqueda.YA_AUDITADOS.ToString();

                cargar_DropDownList_BUSCAR();
                configurarCaracteresAceptadosBusqueda(true, true);
                break;
            case Acciones.nuevoConcepto:
                cargar_DropDownList_PERIODO_PAGO(DropDownList_FORMA_PAGO_NOMINA);
                CheckBox_PER_1.Checked = false;
                CheckBox_PER_2.Checked = false;
                CheckBox_PER_3.Checked = false;
                CheckBox_PER_4.Checked = false;
                TextBox_CAN_PRE.Text = "";
                TextBox_VAL_PRE.Text = "";
                break;
        }
    }

    private void configurar_paneles_popup(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_mensaje.CssClass = "panel_popup";
    }

    private void Configurar()
    {
        configurar_paneles_popup(Panel_FONDO_AVISO, Panel_AVISO);
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
        if (IsPostBack == false)
        {
            Iniciar();
        }
    }
    
    protected void Button_AUDITADO_Click(object sender, EventArgs e)
    {
        int ID_SOLICITUD = Convert.ToInt32(TextBox_ID_SOLICITUD.Text);
        int ID_REQUERIMIENTO = Convert.ToInt32(HiddenField_ID_REQUERIMIENTO.Value);
        Decimal ID_CONTRATO = Convert.ToDecimal(HiddenField_ID_CONTRATO.Value);

        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        if (_radicacionHojasDeVida.ActualizarEstadoProcesoRegSolicitudesIngreso(ID_REQUERIMIENTO, ID_SOLICITUD, "AUDITADO", Session["USU_LOG"].ToString()) == false)
        {
            Informar(Panel_MENSAJES, Label_MENSAJE, _radicacionHojasDeVida.MensajeError, Proceso.Error);
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);

            Informar(Panel_MENSAJES, Label_MENSAJE, "El contrato número: " + ID_CONTRATO.ToString() + " fue auditado correctamente. ", Proceso.Correcto);
        }
    }

    protected void Button_VOLVER_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }
    
    protected void DropDownList_BUSCAR_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_BUSCAR.SelectedValue == "")
        {
            configurarCaracteresAceptadosBusqueda(true, true);
            DropDownList_BUSCAR.Focus();
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "NUM_DOC_IDENTIDAD")
            {
                configurarCaracteresAceptadosBusqueda(false, true);
            }
            else
            {
                if (DropDownList_BUSCAR.SelectedValue == "NOMBRES")
                {
                    configurarCaracteresAceptadosBusqueda(true, false);
                }
                else
                {
                    if (DropDownList_BUSCAR.SelectedValue == "APELLIDOS")
                    {
                        configurarCaracteresAceptadosBusqueda(true, false);
                    }
                }
            }

            TextBox_BUSCAR.Focus();
        }
        TextBox_BUSCAR.Text = "";
    }
    
    private void Informar(Panel panel, Label label, String mensaje, Proceso proceso)
    {
        panel.Visible = true;
        label.Visible = true;

        label.Text = mensaje;
        switch (proceso)
        {
            case Proceso.Correcto:
                label.ForeColor = System.Drawing.Color.Green;
                break;
            case Proceso.Error:
                label.ForeColor = System.Drawing.Color.Red;
                break;
        }
    }
    
    private void Buscar(Acciones accion)
    {
        String datoCapturado = HiddenField_CONTRATOS_DATO.Value;
        requisicion Requisicion = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable dataTable = new DataTable();
        
        if (accion.Equals(Acciones.BuscarAuditados))
        {
            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.buscarContratos);
            switch (HiddenField_CONTRATOS_DROP.Value)
            {
                case "NUM_DOC_IDENTIDAD":
                    dataTable = Requisicion.ObtenerContratosPosiblesDeAuditarPorNumIdentidad(datoCapturado);
                    break;
                case "NOMBRES":
                    dataTable = Requisicion.ObtenerContratosPosiblesDeAuditarPorNombresEmpleado(datoCapturado);
                    break;
                case "APELLIDOS":
                    dataTable = Requisicion.ObtenerContratosPosiblesDeAuditarPorApellidosEmpleado(datoCapturado);
                    break;
            }

            if (dataTable.Rows.Count > 0)
            {
                Mostrar(Acciones.contratosEncontrados);
                Cargar(dataTable);
            }
            else
            {
                if (Requisicion.MensajeError == null)
                {
                    Informar(Panel_MENSAJES, Label_MENSAJE, "ADVERTENCIA: No se encontraron contratos activos para esta busqueda", Proceso.Error);
                }
                else
                {
                    Informar(Panel_MENSAJES, Label_MENSAJE, Requisicion.MensajeError, Proceso.Error);
                }
            }
        }
        else
        {
            dataTable = Requisicion.ObtenerPersonasPorAuditar(datoCapturado);
            Cargar(dataTable);
            if (!dataTable.Rows.Count.Equals(0)) colorear_filas_grilla_hoja_trabajo(dataTable);
        }
    }
    
    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        HiddenField_CONTRATOS_DATO.Value = TextBox_BUSCAR.Text;
        HiddenField_CONTRATOS_DROP.Value = DropDownList_BUSCAR.SelectedValue;

        if (HiddenField_TIPO_BUSQUEDA.Value == TipoBusqueda.YA_AUDITADOS.ToString()) Buscar(Acciones.BuscarAuditados);
        else Buscar(Acciones.BuscarSinAuditar);
    }
    
    private void llenar_info_adicional_con_solicitud(DataRow filaInfoSolicitud)
    {
        String mensaje = null;

        mensaje = filaInfoSolicitud["APELLIDOS"].ToString().Trim() + " " + filaInfoSolicitud["NOMBRES"].ToString().Trim() + "<br>";
        mensaje += filaInfoSolicitud["TIP_DOC_IDENTIDAD"].ToString().Trim() + " " + filaInfoSolicitud["NUM_DOC_IDENTIDAD"].ToString().Trim();

        Label_INFO_ADICIONAL_MODULO.Text = mensaje;
    }
    
    private void cargar_DropDownList_SEXO()
    {
        DropDownList_SEXO.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_SEXO);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Ninguno", "");
        DropDownList_SEXO.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_SEXO.Items.Add(item);
        }
        DropDownList_SEXO.DataBind();
    }
    
    private void cargamos_DropDownList_TIP_DOC_IDENTIDAD()
    {
        DropDownList_TIP_DOC_IDENTIDAD.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_TIP_DOC_ID);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Ninguno", "");
        DropDownList_TIP_DOC_IDENTIDAD.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_TIP_DOC_IDENTIDAD.Items.Add(item);
        }
        DropDownList_TIP_DOC_IDENTIDAD.DataBind();
    }

    private void Cargar(DataTable dataTable)
    {
        if (!dataTable.Rows.Count.Equals(0)) 
        {
            GridView_HOJA_DE_TRABAJO.DataSource = dataTable;
            GridView_HOJA_DE_TRABAJO.DataBind();
        }
        else Informar(Panel_MENSAJES, Label_MENSAJE, "ADVERTENCIA: No se encontraron registros", Proceso.Advertencia);
    }

    private DataRow obtenerIdDepartamentoIdCiudad(String idCiudad)
    {
        DataRow resultado = null;

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());

        DataTable tablaIdDepartamentoIdCiudad = _ciudad.ObtenerIdDepartamentoConIdCiudad(idCiudad);

        if (tablaIdDepartamentoIdCiudad.Rows.Count > 0)
        {
            resultado = tablaIdDepartamentoIdCiudad.Rows[0];
        }

        return resultado;
    }
    
    private void cargar_DropDownList_DEPARTAMENTO_CEDULA()
    {
        DropDownList_DEPARTAMENTO_CEDULA.Items.Clear();

        departamento _departamento = new departamento(Session["idEmpresa"].ToString());
        DataTable tablaDepartamentos = _departamento.ObtenerTodasLosDepartamentos();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione Departamento", "");
        DropDownList_DEPARTAMENTO_CEDULA.Items.Add(item);

        foreach (DataRow fila in tablaDepartamentos.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOMBRE"].ToString(), fila["ID_DEPARTAMENTO"].ToString());
            DropDownList_DEPARTAMENTO_CEDULA.Items.Add(item);
        }

        DropDownList_DEPARTAMENTO_CEDULA.DataBind();
    }
    
    private void cargar_DropDownList_CIU_CEDULA(String idDepartamento)
    {
        DropDownList_CIU_CEDULA.Items.Clear();

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
        DataTable tablaCiudades = _ciudad.ObtenerCiudadesPorIdDepartamento(idDepartamento);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione Ciudad", "");
        DropDownList_CIU_CEDULA.Items.Add(item);

        foreach (DataRow fila in tablaCiudades.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOMBRE"].ToString(), fila["ID_CIUDAD"].ToString());
            DropDownList_CIU_CEDULA.Items.Add(item);
        }

        DropDownList_CIU_CEDULA.DataBind();
    }
    
    private void inhabilitar_DropDownList_CIU_CEDULA()
    {
        DropDownList_CIU_CEDULA.Enabled = false;
        DropDownList_CIU_CEDULA.Items.Clear();
        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione Ciudad", "");
        DropDownList_CIU_CEDULA.Items.Add(item);
        DropDownList_CIU_CEDULA.DataBind();
    }
    
    private void cargar_DropDownList_CAT_LIC_COND()
    {
        DropDownList_CAT_LIC_COND.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_CAT_LICENCIA_CONDUCCION);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Ninguno", "");
        DropDownList_CAT_LIC_COND.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_CAT_LIC_COND.Items.Add(item);
        }
        DropDownList_CAT_LIC_COND.DataBind();
    }
    
    private void cargar_DropDownList_DEPARTAMENTO_ASPIRANTE()
    {
        DropDownList_DEPARTAMENTO_ASPIRANTE.Items.Clear();

        departamento _departamento = new departamento(Session["idEmpresa"].ToString());
        DataTable tablaDepartamentos = _departamento.ObtenerTodasLosDepartamentos();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione Departamento", "");
        DropDownList_DEPARTAMENTO_ASPIRANTE.Items.Add(item);

        foreach (DataRow fila in tablaDepartamentos.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOMBRE"].ToString(), fila["ID_DEPARTAMENTO"].ToString());
            DropDownList_DEPARTAMENTO_ASPIRANTE.Items.Add(item);
        }

        DropDownList_DEPARTAMENTO_ASPIRANTE.DataBind();
    }
    
    private void cargar_DropDownList_CIU_ASPIRANTE(String idDepartamento)
    {
        DropDownList_CIU_ASPIRANTE.Items.Clear();

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
        DataTable tablaCiudades = _ciudad.ObtenerCiudadesPorIdDepartamento(idDepartamento);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione Ciudad", "");
        DropDownList_CIU_ASPIRANTE.Items.Add(item);

        foreach (DataRow fila in tablaCiudades.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOMBRE"].ToString(), fila["ID_CIUDAD"].ToString());
            DropDownList_CIU_ASPIRANTE.Items.Add(item);
        }

        DropDownList_CIU_ASPIRANTE.DataBind();
    }
    
    private void inhabilitar_DropDownList_CIU_ASPIRANTE()
    {
        DropDownList_CIU_ASPIRANTE.Enabled = false;
        DropDownList_CIU_ASPIRANTE.Items.Clear();
        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione Ciudad", "");
        DropDownList_CIU_ASPIRANTE.Items.Add(item);
        DropDownList_CIU_ASPIRANTE.DataBind();
    }
    
    private void cargar_seccion_solicitud_ingreso(Decimal ID_SOLICITUD, Decimal ID_EMPLEADO)
    {
        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoSolicitud = _radicacionHojasDeVida.ObtenerRegSolicitudesingresoPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));
        DataRow filaTablaInfoSolicitud = tablaInfoSolicitud.Rows[0];

        llenar_info_adicional_con_solicitud(filaTablaInfoSolicitud);

        auditoriaContratos _auditoriaContratos = new auditoriaContratos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaUltimaAuditoria = _auditoriaContratos.ObtenerUltimaAuditoriaPorTablaYEmpleado(tabla.REG_SOLICITUDES_INGRESO, ID_EMPLEADO);
        if (tablaUltimaAuditoria.Rows.Count > 0)
        {
            DataRow filaInfoAuditoria = tablaUltimaAuditoria.Rows[0];
            Panel_CABEZA_SOLICITUD_INGRESO.BackColor = colorAuditado;
            Label_SOLICITUD_INGRESO_AUDITADO.Text = "(AUDITADO) - " + Convert.ToDateTime(filaInfoAuditoria["FECHA_AUDITORIA"]).ToShortDateString();
        }
        else
        {
            Panel_CABEZA_SOLICITUD_INGRESO.BackColor = colorSinAuditar;
            Label_SOLICITUD_INGRESO_AUDITADO.Text = "(SIN AUDITAR)";

            if (String.IsNullOrEmpty(HiddenField_SECCIONES_SIN_AUDITORIA.Value) == true)
            {
                HiddenField_SECCIONES_SIN_AUDITORIA.Value = SeccionesAuditoria.SolicitudIngreso.ToString();
            }
            else
            {
                HiddenField_SECCIONES_SIN_AUDITORIA.Value = ";" + SeccionesAuditoria.SolicitudIngreso.ToString();
            }
        }

        TextBox_ID_SOLICITUD.Text = filaTablaInfoSolicitud["ID_SOLICITUD"].ToString().Trim();
        TextBox_ID_SOLICITUD.Enabled = false;

        Label_ID_EMPLEADO.Text = ID_EMPLEADO.ToString();

        TextBox_APELLIDOS.Text = filaTablaInfoSolicitud["APELLIDOS"].ToString().Trim();
        TextBox_NOMBRES.Text = filaTablaInfoSolicitud["NOMBRES"].ToString().Trim();
        cargar_DropDownList_SEXO();
        DropDownList_SEXO.SelectedValue = filaTablaInfoSolicitud["SEXO"].ToString();
        TextBox_FCH_NACIMIENTO.Text = Convert.ToDateTime(filaTablaInfoSolicitud["FCH_NACIMIENTO"]).ToShortDateString();

        cargamos_DropDownList_TIP_DOC_IDENTIDAD();
        try
        {
            DropDownList_TIP_DOC_IDENTIDAD.SelectedValue = filaTablaInfoSolicitud["TIP_DOC_IDENTIDAD"].ToString().Trim();
        }
        catch 
        {
            DropDownList_TIP_DOC_IDENTIDAD.SelectedIndex = 0;
        }
       
        TextBox_NUM_DOC_IDENTIDAD.Text = filaTablaInfoSolicitud["NUM_DOC_IDENTIDAD"].ToString().Trim();

        DataRow filaInfoCiudadYDepartamento = obtenerIdDepartamentoIdCiudad(filaTablaInfoSolicitud["CIU_CEDULA"].ToString().Trim());
        if (filaInfoCiudadYDepartamento != null)
        {
            cargar_DropDownList_DEPARTAMENTO_CEDULA();
            DropDownList_DEPARTAMENTO_CEDULA.SelectedValue = filaInfoCiudadYDepartamento["ID_DEPARTAMENTO"].ToString().Trim();

            cargar_DropDownList_CIU_CEDULA(filaInfoCiudadYDepartamento["ID_DEPARTAMENTO"].ToString().Trim());
            DropDownList_CIU_CEDULA.SelectedValue = filaInfoCiudadYDepartamento["ID_CIUDAD"].ToString().Trim();
        }
        else
        {
            cargar_DropDownList_DEPARTAMENTO_CEDULA();
            DropDownList_DEPARTAMENTO_CEDULA.Enabled = true;
            inhabilitar_DropDownList_CIU_CEDULA();
        }

        Ocultar(Acciones.libretaMilitar);
        if (DropDownList_SEXO.SelectedValue == "M")
        {
            Mostrar(Acciones.libretaMilitar);
            TextBox_LIB_MILITAR.Text = filaTablaInfoSolicitud["LIB_MILITAR"].ToString().Trim();
        }

        try
        {
            TextBox_ASPIRACION_SALARIAL.Text = Convert.ToInt32(filaTablaInfoSolicitud["ASPIRACION_SALARIAL"]).ToString();
        }
        catch
        {
            TextBox_ASPIRACION_SALARIAL.Text = "";
        }

        TextBox_TEL_ASPIRANTE.Text = filaTablaInfoSolicitud["TEL_ASPIRANTE"].ToString().Trim();
        TextBox_E_MAIL.Text = filaTablaInfoSolicitud["E_MAIL"].ToString().Trim();
        cargar_DropDownList_CAT_LIC_COND();
        try
        {
            DropDownList_CAT_LIC_COND.SelectedValue = filaTablaInfoSolicitud["CAT_LIC_COND"].ToString().Trim();
        }
        catch
        {
            DropDownList_CAT_LIC_COND.SelectedIndex = 0;
        }
        
        filaInfoCiudadYDepartamento = obtenerIdDepartamentoIdCiudad(filaTablaInfoSolicitud["CIU_ASPIRANTE"].ToString().Trim());
        if (filaInfoCiudadYDepartamento != null)
        {
            cargar_DropDownList_DEPARTAMENTO_ASPIRANTE();
            DropDownList_DEPARTAMENTO_ASPIRANTE.SelectedValue = filaInfoCiudadYDepartamento["ID_DEPARTAMENTO"].ToString().Trim();

            cargar_DropDownList_CIU_ASPIRANTE(filaInfoCiudadYDepartamento["ID_DEPARTAMENTO"].ToString().Trim());
            DropDownList_CIU_ASPIRANTE.SelectedValue = filaInfoCiudadYDepartamento["ID_CIUDAD"].ToString().Trim();
        }
        else
        {
            cargar_DropDownList_DEPARTAMENTO_ASPIRANTE();
            DropDownList_DEPARTAMENTO_CEDULA.Enabled = true;
            inhabilitar_DropDownList_CIU_ASPIRANTE();
        }
        TextBox_SECTOR.Text = filaTablaInfoSolicitud["SECTOR"].ToString();

        TextBox_DIR_ASPIRANTE.Text = filaTablaInfoSolicitud["DIR_ASPIRANTE"].ToString().Trim();
    }
    
    private void cargar_DropDownList_ENTIDAD_ARP()
    {
        DropDownList_ENTIDAD_ARP.Items.Clear();

        arp _arp = new arp(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaRegionales = _arp.ObtenerTodasLasARP();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_ENTIDAD_ARP.Items.Add(item);

        foreach (DataRow fila in tablaRegionales.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOM_ENTIDAD"].ToString(), fila["ID_ARP"].ToString());
            DropDownList_ENTIDAD_ARP.Items.Add(item);
        }

        DropDownList_ENTIDAD_ARP.DataBind();
        DropDownList_ENTIDAD_ARP.Enabled = true;
    }
    
    
    private void cargar_arp(Decimal ID_AFILIACION_ARP, Decimal ID_SOLICITUD, Decimal ID_EMPLEADO)
    {
        Decimal ID_CONTRATO = Convert.ToDecimal(HiddenField_ID_CONTRATO.Value);

        afiliacion _afilicaion = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        
        DataTable tablaARPParaReq = _afilicaion.ObtenerconafiliacionArpPorRegistro(Convert.ToInt32(ID_AFILIACION_ARP));


        DataTable tablaHistorialARP = _afilicaion.ObtenerconafiliacionArpPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));

        if (tablaHistorialARP.Rows.Count <= 0)
        {
            Panel_GRILLA_ARP.Visible = false;
        }
        else
        {
            Panel_GRILLA_ARP.Visible = true;
            GridView_ARP.DataSource = tablaHistorialARP;
            GridView_ARP.DataBind();
        }

        cargar_DropDownList_ENTIDAD_ARP();

        DataRow filaARP;
        if (tablaARPParaReq.Rows.Count > 0)
        {
            filaARP = tablaARPParaReq.Rows[0];


            auditoriaContratos _auditoriaContratos = new auditoriaContratos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            
            
            DataTable tablaUltimaAuditoria = _auditoriaContratos.ObtenerUltimaAuditoriaPorTablaYEmpleado(tabla.CON_AFILIACION_ARP, ID_EMPLEADO);
            
            if (tablaUltimaAuditoria.Rows.Count > 0)
            {
                DataRow filaInfoAuditoria = tablaUltimaAuditoria.Rows[0];
                Panel_CABEZA_AFILIACION_ARP.BackColor = colorAuditado;
                Label_AFILIACION_ARP_AUDITORIA.Text = "(AUDITADO) - " + Convert.ToDateTime(filaInfoAuditoria["FECHA_AUDITORIA"]).ToShortDateString();
            }
            else
            {
                Panel_CABEZA_AFILIACION_ARP.BackColor = colorSinAuditar;
                Label_AFILIACION_ARP_AUDITORIA.Text = "(SIN AUDITAR)";

                if (String.IsNullOrEmpty(HiddenField_SECCIONES_SIN_AUDITORIA.Value) == true)
                {
                    HiddenField_SECCIONES_SIN_AUDITORIA.Value = SeccionesAuditoria.AfiliacionARP.ToString();
                }
                else
                {
                    HiddenField_SECCIONES_SIN_AUDITORIA.Value = ";" + SeccionesAuditoria.AfiliacionARP.ToString();
                }
            }

            try
            {
                TextBox_FECHA_R_ARP.Text = Convert.ToDateTime(filaARP["FECHA_R"]).ToShortDateString();
            }
            catch
            {
                TextBox_FECHA_R_ARP.Text = "";
            }
            HiddenField_FECHA_R_ARP.Value = TextBox_FECHA_R_ARP.Text;


            try
            {
                TextBox_FECHA_RADICACION_ARP.Text = Convert.ToDateTime(filaARP["FECHA_RADICACION"]).ToShortDateString();
            }
            catch
            {
                TextBox_FECHA_RADICACION_ARP.Text = "";
            }
            HiddenField_FECHA_RADICACION_ARP.Value = TextBox_FECHA_RADICACION_ARP.Text;

            DataTable TablaArchivoRadicacion = _afilicaion.ObtenerDocsRadicacionPorCOntratoYEntidad(ID_CONTRATO, EntidadesAfiliacion.ARP.ToString());
            if (TablaArchivoRadicacion.Rows.Count > 0)
            {
                SecureQueryString QueryStringSeguro;
                tools _tools = new tools();

                Panel_ARCHIVO_AFILIACION_ARP.Visible = true;

                QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
                QueryStringSeguro["contrato"] = ID_CONTRATO.ToString();
                QueryStringSeguro["afiliacion"] = EntidadesAfiliacion.ARP.ToString();

                HyperLink_ARCHIVO_AFILIACION_ARP.NavigateUrl = "~/contratacion/visorDocsAfiliaciones.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            }
            else
            {
                Panel_ARCHIVO_AFILIACION_ARP.Visible = false;
            }

            try
            {
                DropDownList_ENTIDAD_ARP.SelectedValue = filaARP["ID_ARP"].ToString();
            }
            catch
            {
                DropDownList_ENTIDAD_ARP.ClearSelection();
            }
            HiddenField_ENTIDAD_ARP.Value = filaARP["ID_ARP"].ToString();



            TextBox_OBS_ARP.Text = filaARP["OBSERVACIONES"].ToString().Trim();
            HiddenField_OBS_ARP.Value = filaARP["OBSERVACIONES"].ToString().Trim();

            Label_ID_AFLIACION_ARP.Text = ID_AFILIACION_ARP.ToString();
        }
        else
        {
            HiddenField_FECHA_R_ARP.Value = "";
            HiddenField_FECHA_RADICACION_ARP.Value = "";

            Label_ID_AFLIACION_ARP.Text = "Sin asignar";

            TextBox_FECHA_R_ARP.Text = "";
            TextBox_FECHA_RADICACION_ARP.Text = "";

            DropDownList_ENTIDAD_ARP.ClearSelection();
            HiddenField_ENTIDAD_ARP.Value = "";

            TextBox_OBS_ARP.Text = "";
            HiddenField_OBS_ARP.Value = "";

            Panel_CABEZA_AFILIACION_ARP.BackColor = colorSinAuditar;
            Label_AFILIACION_ARP_AUDITORIA.Text = "(SIN AUDITAR)";

            if (String.IsNullOrEmpty(HiddenField_SECCIONES_SIN_AUDITORIA.Value) == true)
            {
                HiddenField_SECCIONES_SIN_AUDITORIA.Value = SeccionesAuditoria.AfiliacionARP.ToString();
            }
            else
            {
                HiddenField_SECCIONES_SIN_AUDITORIA.Value = ";" + SeccionesAuditoria.AfiliacionARP.ToString();
            }


            Panel_ARCHIVO_AFILIACION_ARP.Visible = false;
        }
    }

   
    private void cargar_eps(Decimal ID_AFILIACION_EPS, Decimal ID_SOLICITUD, Decimal ID_EMPLEADO)
    {
        Decimal ID_CONTRATO = Convert.ToDecimal(HiddenField_ID_CONTRATO.Value);

        afiliacion _afilicaion = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

       
        DataTable tablaAfiliacion = _afilicaion.ObtenerconafiliacionEpsPorRegistro(Convert.ToInt32(ID_AFILIACION_EPS));

       
        DataTable tablaHistorial = _afilicaion.ObtenerconafiliacionEpsPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));

        if (tablaHistorial.Rows.Count <= 0)
        {
            Panel_GRILLA_EPS.Visible = false;
        }
        else
        {
            Panel_GRILLA_EPS.Visible = true;
            GridView_EPS.DataSource = tablaHistorial;
            GridView_EPS.DataBind();
        }

        cargar_DropDownList_ENTIDAD_EPS();

        DataRow fila;
        if (tablaAfiliacion.Rows.Count > 0)
        {
            fila = tablaAfiliacion.Rows[0];

            auditoriaContratos _auditoriaContratos = new auditoriaContratos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            
            DataTable tablaUltimaAuditoria = _auditoriaContratos.ObtenerUltimaAuditoriaPorTablaYEmpleado(tabla.CON_AFILIACION_EPS, ID_EMPLEADO);
            if (tablaUltimaAuditoria.Rows.Count > 0)
            {
                DataRow filaInfoAuditoria = tablaUltimaAuditoria.Rows[0];
                Panel_CABEZA_AFILIACION_EPS.BackColor = colorAuditado;
                Label_AFILIACION_EPS_AUDITADA.Text = "(AUDITADO) - " + Convert.ToDateTime(filaInfoAuditoria["FECHA_AUDITORIA"]).ToShortDateString();
            }
            else
            {
                Panel_CABEZA_AFILIACION_EPS.BackColor = colorSinAuditar;
                Label_AFILIACION_EPS_AUDITADA.Text = "(SIN AUDITAR)";

                if (String.IsNullOrEmpty(HiddenField_SECCIONES_SIN_AUDITORIA.Value) == true)
                {
                    HiddenField_SECCIONES_SIN_AUDITORIA.Value = SeccionesAuditoria.AfiliacionEPS.ToString();
                }
                else
                {
                    HiddenField_SECCIONES_SIN_AUDITORIA.Value = ";" + SeccionesAuditoria.AfiliacionEPS.ToString();
                }
            }

            try
            {
                TextBox_FECHA_R_EPS.Text = Convert.ToDateTime(fila["FECHA_R"]).ToShortDateString();
            }
            catch
            {
                TextBox_FECHA_R_EPS.Text = "";
            }
            HiddenField_FECHA_R_EPS.Value = TextBox_FECHA_R_EPS.Text;

            try
            {
                TextBox_FECHA_RADICACION_EPS.Text = Convert.ToDateTime(fila["FECHA_RADICACION"]).ToShortDateString();
            }
            catch
            {
                TextBox_FECHA_RADICACION_EPS.Text = "";
            }
            HiddenField_FECHA_RADICACION_EPS.Value = TextBox_FECHA_RADICACION_EPS.Text;



            DataTable TablaArchivoRadicacion = _afilicaion.ObtenerDocsRadicacionPorCOntratoYEntidad(ID_CONTRATO, EntidadesAfiliacion.EPS.ToString());
            if (TablaArchivoRadicacion.Rows.Count > 0)
            {
                SecureQueryString QueryStringSeguro;
                tools _tools = new tools();

                Panel_ARCHIVO_AFILIACION_EPS.Visible = true;

                QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
                QueryStringSeguro["contrato"] = ID_CONTRATO.ToString();
                QueryStringSeguro["afiliacion"] = EntidadesAfiliacion.EPS.ToString();

                HyperLink_ARCHIVO_AFILIACION_EPS.NavigateUrl = "~/contratacion/visorDocsAfiliaciones.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            }
            else
            {
                Panel_ARCHIVO_AFILIACION_EPS.Visible = false;
            }


            try
            {
                DropDownList_ENTIDAD_EPS.SelectedValue = fila["ID_EPS"].ToString();
            }
            catch
            {
                DropDownList_ENTIDAD_EPS.ClearSelection();
            }
            HiddenField_ENTIDAD_EPS.Value = fila["ID_EPS"].ToString();

            TextBox_OBS_EPS.Text = fila["OBSERVACIONES"].ToString().Trim();
            HiddenField_OBS_EPS.Value = fila["OBSERVACIONES"].ToString().Trim();

            Label_ID_AFILIACION_EPS.Text = ID_AFILIACION_EPS.ToString();
        }
        else
        {
            HiddenField_FECHA_R_EPS.Value = "";
            HiddenField_FECHA_RADICACION_EPS.Value = "";

            Label_ID_AFILIACION_EPS.Text = "Sin asignar";

            TextBox_FECHA_R_EPS.Text = "";
            TextBox_FECHA_RADICACION_EPS.Text = "";

            DropDownList_ENTIDAD_EPS.ClearSelection();
            HiddenField_ENTIDAD_EPS.Value = "";

            TextBox_OBS_EPS.Text = "";
            HiddenField_OBS_EPS.Value = "";

            Panel_CABEZA_AFILIACION_EPS.BackColor = colorSinAuditar;
            Label_AFILIACION_EPS_AUDITADA.Text = "(SIN AUDITAR)";

            if (String.IsNullOrEmpty(HiddenField_SECCIONES_SIN_AUDITORIA.Value) == true)
            {
                HiddenField_SECCIONES_SIN_AUDITORIA.Value = SeccionesAuditoria.AfiliacionEPS.ToString();
            }
            else
            {
                HiddenField_SECCIONES_SIN_AUDITORIA.Value = ";" + SeccionesAuditoria.AfiliacionEPS.ToString();
            }

            Panel_ARCHIVO_AFILIACION_EPS.Visible = false;
        }
    }


    private void limpiar_DropDownList(DropDownList drop)
    {
        drop.Items.Clear();
        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        drop.Items.Add(item);
        drop.DataBind();
    }

    private void llenarDropDepartamentos(decimal registro)
    {
        DropDownList_DepartamentoCajaC.Items.Clear();

        DropDownList_DepartamentoCajaC.Items.Add(new System.Web.UI.WebControls.ListItem("Seleccione...", ""));

        afiliacion _afi = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaDepartamentos = _afi.ObtenerDepartamentosCajaC(registro, "CCF");

        foreach (DataRow fila in tablaDepartamentos.Rows)
        {
            DropDownList_DepartamentoCajaC.Items.Add(new System.Web.UI.WebControls.ListItem(fila["DEPARTAMENTO"].ToString(), fila["ID_DEPARTAMENTO"].ToString()));
        }

        DropDownList_DepartamentoCajaC.DataBind();

    }

    private void llenarDropCiudades(string id_departamento, Decimal registro)
    {
        DropDownList_CiudadCajaC.Items.Clear();

        DropDownList_CiudadCajaC.Items.Add(new System.Web.UI.WebControls.ListItem("Seleccione...", ""));

        afiliacion _afi = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCiudades = _afi.ObtenerCiudadesCajaC(registro, "CCF", id_departamento);

        foreach (DataRow fila in tablaCiudades.Rows)
        {
            DropDownList_CiudadCajaC.Items.Add(new System.Web.UI.WebControls.ListItem(fila["CIUDAD"].ToString(), fila["ID_CIUDAD"].ToString()));
        }

        DropDownList_CiudadCajaC.DataBind();

    }  

    
    private void cargar_caja(Decimal ID_AFILIACION_CAJA, Decimal ID_SOLICITUD, Decimal ID_EMPLEADO)
    {
        Decimal ID_CONTRATO = Convert.ToDecimal(HiddenField_ID_CONTRATO.Value);

        afiliacion _afilicaion = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

      
        DataTable tablaAfiliacion = _afilicaion.ObtenerconafiliacionCajasCPorRegistro(Convert.ToInt32(ID_AFILIACION_CAJA));

        
        DataTable tablaHistorial = _afilicaion.ObtenerconafiliacionCajasCPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));

        if (tablaHistorial.Rows.Count <= 0)
        {
            Panel_GRILLA_CAJA.Visible = false;
        }
        else
        {
            Panel_GRILLA_CAJA.Visible = true;
            GridView_CAJA.DataSource = tablaHistorial;
            GridView_CAJA.DataBind();
        }

        cargar_DropDownList_ENTIDAD_CAJA();

        DataRow fila;
        if (tablaAfiliacion.Rows.Count > 0)
        {
            fila = tablaAfiliacion.Rows[0];


            auditoriaContratos _auditoriaContratos = new auditoriaContratos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

          
            DataTable tablaUltimaAuditoria = _auditoriaContratos.ObtenerUltimaAuditoriaPorTablaYEmpleado(tabla.CON_AFILIACION_CAJAS_C, ID_EMPLEADO);

            if (tablaUltimaAuditoria.Rows.Count > 0)
            {
                DataRow filaInfoAuditoria = tablaUltimaAuditoria.Rows[0];
                Panel_CABEZA_AFILIACION_CCF.BackColor = colorAuditado;
                Label_AFILIACION_CCF_AUDITADA.Text = "(AUDITADO) - " + Convert.ToDateTime(filaInfoAuditoria["FECHA_AUDITORIA"]).ToShortDateString();
            }
            else
            {
                Panel_CABEZA_AFILIACION_CCF.BackColor = colorSinAuditar;
                Label_AFILIACION_CCF_AUDITADA.Text = "(SIN AUDITAR)";

                if (String.IsNullOrEmpty(HiddenField_SECCIONES_SIN_AUDITORIA.Value) == true)
                {
                    HiddenField_SECCIONES_SIN_AUDITORIA.Value = SeccionesAuditoria.AfiliacionCCF.ToString();
                }
                else
                {
                    HiddenField_SECCIONES_SIN_AUDITORIA.Value = ";" + SeccionesAuditoria.AfiliacionCCF.ToString();
                }
            }

            try
            {
                TextBox_FECHA_R_CAJA.Text = Convert.ToDateTime(fila["FECHA_R"]).ToShortDateString();
            }
            catch
            {
                TextBox_FECHA_R_CAJA.Text = "";
            }
            HiddenField_FECHA_R_CAJA.Value = Convert.ToDateTime(fila["FECHA_R"]).ToShortDateString();

            try
            {
                TextBox_FECHA_RADICACION_CAJA.Text = Convert.ToDateTime(fila["FECHA_RADICACION"]).ToShortDateString();
            }
            catch
            {
                TextBox_FECHA_RADICACION_CAJA.Text = "";
            }
            HiddenField_FECHA_RADICACION_CAJA.Value = TextBox_FECHA_RADICACION_CAJA.Text;

            DataTable TablaArchivoRadicacion = _afilicaion.ObtenerDocsRadicacionPorCOntratoYEntidad(ID_CONTRATO, EntidadesAfiliacion.CAJA.ToString());
            if (TablaArchivoRadicacion.Rows.Count > 0)
            {
                SecureQueryString QueryStringSeguro;
                tools _tools = new tools();

                Panel_ARCHIVO_AFILIACION_CAJA.Visible = true;

                QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
                QueryStringSeguro["contrato"] = ID_CONTRATO.ToString();
                QueryStringSeguro["afiliacion"] = EntidadesAfiliacion.CAJA.ToString();

                HyperLink_ARCHIVO_AFILIACION_CAJA.NavigateUrl = "~/contratacion/visorDocsAfiliaciones.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            }
            else
            {
                Panel_ARCHIVO_AFILIACION_CAJA.Visible = false;
            }


            try
            {
                DropDownList_ENTIDAD_Caja.SelectedValue = fila["ID_CAJA_C"].ToString();
            }
            catch
            {
                DropDownList_ENTIDAD_Caja.ClearSelection();
            }

            HiddenField_ENTIDAD_Caja.Value = fila["ID_CAJA_C"].ToString();

            TextBox_OBS_CAJA.Text = fila["OBSERVACIONES"].ToString().Trim();
            HiddenField_OBS_CAJA.Value = fila["OBSERVACIONES"].ToString().Trim();

            Label_ID_AFILIACION_CAJA_C.Text = ID_AFILIACION_CAJA.ToString();


            llenarDropDepartamentos(Convert.ToDecimal(fila["ID_CAJA_C"]));

            if (DropDownList_ENTIDAD_Caja.SelectedValue == "43" || DropDownList_ENTIDAD_Caja.SelectedItem.Text.ToUpper() == "SIN CAJA")
            {
                Panel_CiudadCaja.Visible = false;
                DropDownList_DepartamentoCajaC.ClearSelection();
                DropDownList_CiudadCajaC.Items.Clear();
                DropDownList_CiudadCajaC.Items.Add(new System.Web.UI.WebControls.ListItem("Seleccione...", ""));
            }
            else
            {
                Panel_CiudadCaja.Visible = true;

                try
                {
                    DropDownList_DepartamentoCajaC.SelectedValue = fila["ID_DEPARTAMENTO"].ToString();
                    llenarDropCiudades(fila["ID_DEPARTAMENTO"].ToString(), Convert.ToDecimal(fila["ID_CAJA_C"]));
                    DropDownList_CiudadCajaC.SelectedValue = fila["ID_CIUDAD"].ToString();
                }
                catch
                {
                    limpiar_DropDownList(DropDownList_CiudadCajaC);
                }
            }
        }
        else
        {
            HiddenField_FECHA_R_CAJA.Value = "";
            HiddenField_FECHA_RADICACION_CAJA.Value = "";

            Label_ID_AFILIACION_CAJA_C.Text = "Sin asignar";

            TextBox_FECHA_R_CAJA.Text = "";
            TextBox_FECHA_RADICACION_CAJA.Text = "";

            DropDownList_ENTIDAD_Caja.ClearSelection();
            HiddenField_ENTIDAD_Caja.Value = "";

            TextBox_OBS_CAJA.Text = "";
            HiddenField_OBS_CAJA.Value = "";

            Panel_CABEZA_AFILIACION_CCF.BackColor = colorSinAuditar;
            Label_AFILIACION_CCF_AUDITADA.Text = "(SIN AUDITAR)";

            if (String.IsNullOrEmpty(HiddenField_SECCIONES_SIN_AUDITORIA.Value) == true)
            {
                HiddenField_SECCIONES_SIN_AUDITORIA.Value = SeccionesAuditoria.AfiliacionCCF.ToString();
            }
            else
            {
                HiddenField_SECCIONES_SIN_AUDITORIA.Value = ";" + SeccionesAuditoria.AfiliacionCCF.ToString();
            }

            Panel_ARCHIVO_AFILIACION_CAJA.Visible = false;

            Panel_CiudadCaja.Visible = true;

            limpiar_DropDownList(DropDownList_DepartamentoCajaC);
            limpiar_DropDownList(DropDownList_CiudadCajaC);
        }
    }
    
    private void cargar_afp(Decimal ID_AFILIACION_F_PENSIONES, Decimal ID_SOLICITUD, Decimal ID_EMPLEADO)
    {
        Decimal ID_CONTRATO = Convert.ToDecimal(HiddenField_ID_CONTRATO.Value);

        afiliacion _afilicaion = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

      
        DataTable tablaAfiliacion = _afilicaion.ObtenerconafiliacionfpensionesPorRegistro(Convert.ToInt32(ID_AFILIACION_F_PENSIONES));

      
        DataTable tablaHistorial = _afilicaion.ObtenerconafiliacionfpensionesPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));

        if (tablaHistorial.Rows.Count <= 0)
        {
            Panel_GRILLA_AFP.Visible = false;
        }
        else
        {
            Panel_GRILLA_AFP.Visible = true;
            GridView_AFP.DataSource = tablaHistorial;
            GridView_AFP.DataBind();
        }

        cargar_DropDownList_ENTIDAD_AFP();
        cargar_DropDownList_pensionado();
        cargar_DropDownList_tipo_Pensionado();

        DataRow fila;
        if (tablaAfiliacion.Rows.Count > 0)
        {
            fila = tablaAfiliacion.Rows[0];

            auditoriaContratos _auditoriaContratos = new auditoriaContratos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaUltimaAuditoria = _auditoriaContratos.ObtenerUltimaAuditoriaPorTablaYEmpleado(tabla.CON_AFILIACION_F_PENSIONES, ID_EMPLEADO);
            if (tablaUltimaAuditoria.Rows.Count > 0)
            {
                DataRow filaInfoAuditoria = tablaUltimaAuditoria.Rows[0];
                Panel_CABEZA_AFILIACION_AFP.BackColor = colorAuditado;
                Label_AFILIACION_AFP_AUDITADA.Text = "(AUDITADO) - " + Convert.ToDateTime(filaInfoAuditoria["FECHA_AUDITORIA"]).ToShortDateString();
            }
            else
            {
                Panel_CABEZA_AFILIACION_AFP.BackColor = colorSinAuditar;
                Label_AFILIACION_AFP_AUDITADA.Text = "(SIN AUDITAR)";

                if (String.IsNullOrEmpty(HiddenField_SECCIONES_SIN_AUDITORIA.Value) == true)
                {
                    HiddenField_SECCIONES_SIN_AUDITORIA.Value = SeccionesAuditoria.AfiliacionAFP.ToString();
                }
                else
                {
                    HiddenField_SECCIONES_SIN_AUDITORIA.Value = ";" + SeccionesAuditoria.AfiliacionAFP.ToString();
                }
            }


            try
            {
                TextBox_FECHA_R_AFP.Text = Convert.ToDateTime(fila["FECHA_R"]).ToShortDateString();
            }
            catch
            {
                TextBox_FECHA_R_AFP.Text = "";
            }
            HiddenField_FECHA_R_AFP.Value = TextBox_FECHA_R_AFP.Text;

            try
            {
                TextBox_FECHA_RADICACION_AFP.Text = Convert.ToDateTime(fila["FECHA_RADICACION"]).ToShortDateString();
            }
            catch
            {
                TextBox_FECHA_RADICACION_AFP.Text = "";
            }
            HiddenField_FECHA_RADICACION_AFP.Value = TextBox_FECHA_RADICACION_AFP.Text;



            DataTable TablaArchivoRadicacion = _afilicaion.ObtenerDocsRadicacionPorCOntratoYEntidad(ID_CONTRATO, EntidadesAfiliacion.AFP.ToString());
            if (TablaArchivoRadicacion.Rows.Count > 0)
            {
                SecureQueryString QueryStringSeguro;
                tools _tools = new tools();

                Panel_ARCHIVO_AFILIACION_AFP.Visible = true;

                QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
                QueryStringSeguro["contrato"] = ID_CONTRATO.ToString();
                QueryStringSeguro["afiliacion"] = EntidadesAfiliacion.AFP.ToString();

                HyperLink_ARCHIVO_AFILIACION_AFP.NavigateUrl = "~/contratacion/visorDocsAfiliaciones.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            }
            else
            {
                Panel_ARCHIVO_AFILIACION_AFP.Visible = false;
            }


            try
            {
                DropDownList_AFP.SelectedValue = fila["ID_F_PENSIONES"].ToString();
            }
            catch
            {
                DropDownList_AFP.ClearSelection();
            }
            HiddenField_ENTIDAD_AFP.Value = DropDownList_AFP.SelectedValue;

            Ocultar(Acciones.pensionado);
            if (fila["PENSIONADO"] == DBNull.Value)
            {
                DropDownList_pensionado.SelectedIndex = 0;
                HiddenField_pensionado.Value = "";
                DropDownList_tipo_pensionado.SelectedIndex = 0;
                HiddenField_tipo_pensionado.Value = "";
                TextBox_Numero_resolucion_tramite.Text = "";
                HiddenField_resolucion_tramite.Value = "";
            }
            else
            {
                if (fila["PENSIONADO"].ToString().Trim() == "S")
                {
                    Mostrar(Acciones.pensionado);
                    DropDownList_pensionado.SelectedValue = fila["PENSIONADO"].ToString();
                    HiddenField_pensionado.Value = DropDownList_pensionado.SelectedValue;
                    DropDownList_tipo_pensionado.SelectedValue = fila["TIPO_PENSIONADO"].ToString();
                    HiddenField_tipo_pensionado.Value = DropDownList_tipo_pensionado.SelectedValue;
                    TextBox_Numero_resolucion_tramite.Text = fila["NUMERO_RESOLUCION_TRAMITE"].ToString();
                    HiddenField_resolucion_tramite.Value = TextBox_Numero_resolucion_tramite.Text;
                }
                else
                {
                    DropDownList_pensionado.SelectedValue = fila["PENSIONADO"].ToString();
                    HiddenField_pensionado.Value = DropDownList_pensionado.SelectedValue;
                    DropDownList_tipo_pensionado.SelectedIndex = 0;
                    HiddenField_tipo_pensionado.Value = "";
                    TextBox_Numero_resolucion_tramite.Text = "";
                    HiddenField_resolucion_tramite.Value = "";
                }
            }
            
            TextBox_OBS_AFP.Text = fila["OBSERVACIONES"].ToString().Trim();
            HiddenField_OBS_AFP.Value = fila["OBSERVACIONES"].ToString().Trim();

            Label_ID_AFILIACION_F_PENSIONES.Text = ID_AFILIACION_F_PENSIONES.ToString();
        }
        else
        {
            HiddenField_FECHA_R_AFP.Value = "";
            HiddenField_FECHA_RADICACION_AFP.Value = "";


            Label_ID_AFILIACION_F_PENSIONES.Text = "Sin asignar";

            TextBox_FECHA_R_AFP.Text = "";
            TextBox_FECHA_RADICACION_AFP.Text = "";

            DropDownList_AFP.ClearSelection();
            HiddenField_ENTIDAD_AFP.Value = "";

            DropDownList_pensionado.SelectedIndex = 0;
            HiddenField_pensionado.Value = "";
            DropDownList_tipo_pensionado.SelectedIndex = 0;
            HiddenField_tipo_pensionado.Value = "";
            TextBox_Numero_resolucion_tramite.Text = "";
            HiddenField_resolucion_tramite.Value = "";

            TextBox_OBS_AFP.Text = "";
            HiddenField_OBS_AFP.Value = "";

            Panel_CABEZA_AFILIACION_AFP.BackColor = colorSinAuditar;
            Label_AFILIACION_AFP_AUDITADA.Text = "(SIN AUDITAR)";

            if (String.IsNullOrEmpty(HiddenField_SECCIONES_SIN_AUDITORIA.Value) == true)
            {
                HiddenField_SECCIONES_SIN_AUDITORIA.Value = SeccionesAuditoria.AfiliacionAFP.ToString();
            }
            else
            {
                HiddenField_SECCIONES_SIN_AUDITORIA.Value = ";" + SeccionesAuditoria.AfiliacionAFP.ToString();
            }

            Panel_ARCHIVO_AFILIACION_AFP.Visible = false;
        }
    }
    
    private void cargar_DropDownList_ENTIDAD_EPS()
    {
        DropDownList_ENTIDAD_EPS.Items.Clear();

        eps _eps = new eps(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaRegionales = _eps.ObtenerTodasLasEPS();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione EPS", "");
        DropDownList_ENTIDAD_EPS.Items.Add(item);

        foreach (DataRow fila in tablaRegionales.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOM_ENTIDAD"].ToString(), fila["ID_EPS"].ToString());
            DropDownList_ENTIDAD_EPS.Items.Add(item);
        }

        DropDownList_ENTIDAD_EPS.DataBind();
        DropDownList_ENTIDAD_EPS.Enabled = true;
    }

    private void cargar_DropDownList_ENTIDAD_CAJA()
    {
        DropDownList_ENTIDAD_Caja.Items.Clear();

        cajaCompensacionFamiliar _ccf = new cajaCompensacionFamiliar(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaRegionales = _ccf.ObtenerTodasLasCajasCompensacionFamiliar();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione CCF", "");
        DropDownList_ENTIDAD_Caja.Items.Add(item);

        foreach (DataRow fila in tablaRegionales.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOM_ENTIDAD"].ToString(), fila["ID_CAJA_C"].ToString());
            DropDownList_ENTIDAD_Caja.Items.Add(item);
        }

        DropDownList_ENTIDAD_Caja.DataBind();
        DropDownList_ENTIDAD_Caja.Enabled = true;
    }

    private void cargar_DropDownList_ENTIDAD_AFP()
    {
        DropDownList_AFP.Items.Clear();

        fondoPensiones _AFP = new fondoPensiones(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaRegionales = _AFP.ObtenerTodosLosFondosDePensiones();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione AFP", "");
        DropDownList_AFP.Items.Add(item);

        foreach (DataRow fila in tablaRegionales.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOM_ENTIDAD"].ToString(), fila["ID_F_PENSIONES"].ToString());
            DropDownList_AFP.Items.Add(item);
        }

        DropDownList_AFP.DataBind();
        DropDownList_AFP.Enabled = true;
    }
    
    private void cargar_DropDownList_pensionado()
    {
        DropDownList_pensionado.Items.Clear();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Ninguno", "");
        DropDownList_pensionado.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("SI", "S");
        DropDownList_pensionado.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("NO", "N");
        DropDownList_pensionado.Items.Add(item);

        DropDownList_pensionado.DataBind();
    }
    
    private void cargar_DropDownList_tipo_Pensionado()
    {
        DropDownList_tipo_pensionado.Items.Clear();
        parametro _parametro = new parametro(Session["idEmpresa"].ToString());
        DataTable tablaParametro = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_TIPO_PENSIONADO);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione Tipo Pensionado", "");
        DropDownList_tipo_pensionado.Items.Add(item);

        foreach (DataRow fila in tablaParametro.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_tipo_pensionado.Items.Add(item);
        }
    }

   
    private void carar_seccion_afiliaciones(Decimal ID_SOLICITUD, Decimal ID_REQUERIMIENTO, Decimal ID_EMPLEADO)
    {
        registroContrato _registroContrato = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoContrato = _registroContrato.obtenerInfoNomEmpleadoPorIdSolicitudIdRequerimiento(ID_SOLICITUD, ID_REQUERIMIENTO, ID_EMPLEADO);

        DataRow filaInfoContrato = tablaInfoContrato.Rows[0];

        Decimal ID_AFILIACION_ARP = Convert.ToDecimal(filaInfoContrato["ID_ARP"]);

        Decimal ID_AFILIACION_CAJA_C = 0;
        try
        {
            ID_AFILIACION_CAJA_C = Convert.ToDecimal(filaInfoContrato["ID_CAJA_C"]);
        }
        catch
        {
            ID_AFILIACION_CAJA_C = 0;
        }

        Decimal ID_AFILIACION_EPS = Convert.ToDecimal(filaInfoContrato["ID_EPS"]);
        Decimal ID_AFILIACION_F_PENSIONES = Convert.ToDecimal(filaInfoContrato["ID_F_PENSIONES"]);

        
        cargar_arp(ID_AFILIACION_ARP, ID_SOLICITUD, ID_EMPLEADO);

       
        cargar_eps(ID_AFILIACION_EPS, ID_SOLICITUD, ID_EMPLEADO);

       
        cargar_caja(ID_AFILIACION_CAJA_C, ID_SOLICITUD, ID_EMPLEADO);

       
        cargar_afp(ID_AFILIACION_F_PENSIONES, ID_SOLICITUD, ID_EMPLEADO);
    }
    
    private void presentar_interfaz_segun_resultado()
    {
        if (comprobar_proceso_auditoria() == true)
        {
            Ocultar(Acciones.auditoriaCompleta);
            if (HiddenField_PRESENTACION.Value == Presentacion.HojaDeTrabajo.ToString())
            {
                Mostrar(Acciones.auditoriaCompleta);
            }
        }
        else
        {
            Ocultar(Acciones.auditoriaCompleta);
        }
    }

    private void cargar_todas_clausulas_activas(Decimal idEmpleado)
    {
        Clausula clausula = new Clausula(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable dataTable = clausula.ObtenerContratacionPorIdEmpleado(idEmpleado);
        if (!dataTable.Rows.Count.Equals(0))
        {
            Panel_MENSAJES_CONCEPTOS_FIJOS.Visible = false;
            GridView_LISTA_CLAUSULAS_PARAMETRIZADAS.DataSource = dataTable;
            GridView_LISTA_CLAUSULAS_PARAMETRIZADAS.DataBind();
            Cargar(dataTable, GridView_LISTA_CLAUSULAS_PARAMETRIZADAS);
        }
        else Informar(Panel_MENSAJES_CONCEPTOS_FIJOS, Label_MENSAJES_CONCEPTOS_FIJOS, "No se encontraron clausulas asociadas la perfil del trabajador seleccionado.", Proceso.Advertencia);
    }

    private void Cargar(DataTable dataTable, GridView gridView)
    {
        tools _tools = new tools();
        SecureQueryString secureQueryString;
        secureQueryString = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        gridView.DataSource = dataTable;
        gridView.DataBind();

        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            DataRow dataRow = dataTable.Rows[i];
            if (!DBNull.Value.Equals(dataRow["ID_CLAUSULA"]))
            {
                gridView.Rows[i].Cells[1].Text = dataRow["ID_CLAUSULA"].ToString();
                secureQueryString["ID_CLAUSULA"] = dataRow["ID_CLAUSULA"].ToString();
            }

            if (!DBNull.Value.Equals(dataRow["ID_EMPLEADO"]))
            {
                gridView.Rows[i].Cells[2].Text = dataRow["ID_EMPLEADO"].ToString();
                secureQueryString["ID_EMPLEADO"] = dataRow["ID_EMPLEADO"].ToString();
            }
            if (!string.IsNullOrEmpty(dataRow["TIPO_CLAUSULA"].ToString())) gridView.Rows[i].Cells[3].Text = dataRow["TIPO_CLAUSULA"].ToString();
            if (!string.IsNullOrEmpty(dataRow["DESCRIPCION"].ToString())) gridView.Rows[i].Cells[4].Text = dataRow["DESCRIPCION"].ToString();

            HyperLink hyperLink = gridView.Rows[i].FindControl("HyperLink_ARCHIVO") as HyperLink;

            if (!DBNull.Value.Equals(dataRow["ARCHIVO"]))
            {
                hyperLink.NavigateUrl = "~/contratacion/VisorDocumentosClausulasContratacion.aspx?data=" + HttpUtility.UrlEncode(secureQueryString.ToString());
            }
            else hyperLink.Enabled = false;
        }
    }

    private void cargar_conceptos_fijos_parametrizados(Decimal ID_EMPLEADO)
    {
        ConceptosNominaEmpleado _ConceptosNominaEmpleado = new ConceptosNominaEmpleado(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaConceptosParametrizadosActivos = _ConceptosNominaEmpleado.ObtenerNomConceptosEmpleadosPorIdEmpleadoConceptosFijos(Convert.ToInt32(ID_EMPLEADO));

        if (tablaConceptosParametrizadosActivos.Rows.Count <= 0)
        {
            Ocultar(Acciones.conceptosIncluidos);

            GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.DataSource = null;
            GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.DataBind();

            Informar(Panel_MENSAJES_CONCEPTOS_FIJOS, Label_MENSAJES_CONCEPTOS_FIJOS, "No existen conceptos fijos parametrizados actualmente", Proceso.Error);
        }
        else
        {
            Mostrar(Acciones.conceptosIncluidos);

            GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.DataSource = tablaConceptosParametrizadosActivos;
            GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.DataBind();
        }
    }
    
    private void cargar_seccion_conceptos_fijos(Decimal ID_PERFIL, Decimal ID_EMPLEADO)
    {
        auditoriaContratos _auditoriaContratos = new auditoriaContratos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaUltimaAuditoria = _auditoriaContratos.ObtenerUltimaAuditoriaPorTablaYEmpleado(tabla.NOM_CONCEPTOS_EMPLEADO, ID_EMPLEADO);
        if (tablaUltimaAuditoria.Rows.Count > 0)
        {
            DataRow filaInfoAuditoria = tablaUltimaAuditoria.Rows[0];
            Panel_CABEZA_CONCEPTOS_FIJOS.BackColor = colorAuditado;
            Label_CONCEPTOS_FIJOS_AUDITADA.Text = "(AUDITADO) - " + Convert.ToDateTime(filaInfoAuditoria["FECHA_AUDITORIA"]).ToShortDateString();
        }
        else
        {
            Panel_CABEZA_CONCEPTOS_FIJOS.BackColor = colorSinAuditar;
            Label_CONCEPTOS_FIJOS_AUDITADA.Text = "(SIN AUDITAR)";

            if (String.IsNullOrEmpty(HiddenField_SECCIONES_SIN_AUDITORIA.Value) == true)
            {
                HiddenField_SECCIONES_SIN_AUDITORIA.Value = SeccionesAuditoria.ConceptosFijos.ToString();
            }
            else
            {
                HiddenField_SECCIONES_SIN_AUDITORIA.Value = ";" + SeccionesAuditoria.ConceptosFijos.ToString();
            }
        }

        cargar_todas_clausulas_activas(Convert.ToDecimal(Label_ID_EMPLEADO.Text));

        cargar_conceptos_fijos_parametrizados(ID_EMPLEADO);

        Panel_DatosNuevaClausula.Visible = false;
    }

    private void Cargar_GridView_EXAMENES_REALIZADOS_desde_tabla(DataTable tablaExamenes)
    {
        GridView_EXAMENES_REALIZADOS.DataSource = tablaExamenes;
        GridView_EXAMENES_REALIZADOS.DataBind();

        for (int i = 0; i < GridView_EXAMENES_REALIZADOS.Rows.Count; i++)
        {
            DataRow filaTabla = tablaExamenes.Rows[i];

            TextBox textoAutoRecomendacion = GridView_EXAMENES_REALIZADOS.Rows[i].FindControl("TextBox_Autos_Recomendacion") as TextBox;

            if (DBNull.Value.Equals(filaTabla["OBSERVACIONES"]) == false)
            {
                textoAutoRecomendacion.Text = filaTabla["OBSERVACIONES"].ToString().Trim();
            }
            else
            {
                textoAutoRecomendacion.Text = "";
            }
        }
    }

    private void cargar_seccion_examenes_autos(Decimal ID_EMPLEADO)
    {
        Decimal ID_REQUERIMIENTO = Convert.ToDecimal(HiddenField_ID_REQUERIMIENTO.Value);
        Decimal ID_SOLICITUD = Convert.ToDecimal(TextBox_ID_SOLICITUD.Text);

        auditoriaContratos _auditoriaContratos = new auditoriaContratos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaUltimaAuditoria = _auditoriaContratos.ObtenerUltimaAuditoriaPorTablaYEmpleado(tabla.CON_REG_EXAMENES_EMPLEADO, ID_EMPLEADO);
        if (tablaUltimaAuditoria.Rows.Count > 0)
        {
            DataRow filaInfoAuditoria = tablaUltimaAuditoria.Rows[0];
            Panel_CABEZA_EXAMENES.BackColor = colorAuditado;
            Label_EXAMENES_AUDITORIA.Text = "(AUDITADO) - " + Convert.ToDateTime(filaInfoAuditoria["FECHA_AUDITORIA"]).ToShortDateString();
        }
        else
        {
            Panel_CABEZA_EXAMENES.BackColor = colorSinAuditar;
            Label_EXAMENES_AUDITORIA.Text = "(SIN AUDITAR)";

            if (String.IsNullOrEmpty(HiddenField_SECCIONES_SIN_AUDITORIA.Value) == true)
            {
                HiddenField_SECCIONES_SIN_AUDITORIA.Value = SeccionesAuditoria.ExamenesAutosRecomendacion.ToString();
            }
            else
            {
                HiddenField_SECCIONES_SIN_AUDITORIA.Value = ";" + SeccionesAuditoria.ExamenesAutosRecomendacion.ToString();
            }
        }


        ordenExamenes _ordenes = new ordenExamenes(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaOrdenes = _ordenes.ObtenerConRegOrdenExamenPorSolicitud(Convert.ToInt32(ID_REQUERIMIENTO), Convert.ToInt32(ID_SOLICITUD));

        if(tablaOrdenes.Rows.Count <= 0)
        {
            Informar(Panel_MENSAJE_EXAMENES, Label_MENSAJE_EXAMENES, "No se encontraron examenes aplicados al empleado seleccionado.", Proceso.Error);

            GridView_EXAMENES_REALIZADOS.DataSource = null;
            GridView_EXAMENES_REALIZADOS.DataBind();

            Button_IMPRIMIR_AUTOS.Visible = false;
        }
        else
        {
            Cargar_GridView_EXAMENES_REALIZADOS_desde_tabla(tablaOrdenes);

            Button_IMPRIMIR_AUTOS.Visible = ExistenAutosRecomendacion(tablaOrdenes);
        }
    }
    
    private void cargar_DropDownList_CARGO_TRABAJADOR(Decimal ID_EMPRESA, String SEXO)
    {
        DropDownList_CARGO_TRABAJADOR.Items.Clear();

        perfil _perfil = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaPefiles = _perfil.ObtenerPerfilesPorEmpresaYSexo(ID_EMPRESA, SEXO);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_CARGO_TRABAJADOR.Items.Add(item);

        foreach (DataRow fila in tablaPefiles.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["ID_OCUPACION"].ToString() + "-" + fila["NOM_OCUPACION"].ToString(), fila["ID_PERFIL"].ToString());
            DropDownList_CARGO_TRABAJADOR.Items.Add(item);
        }
        DropDownList_CARGO_TRABAJADOR.DataBind();
    }
    
    private void cargar_DropDownList_CIUDAD(Decimal ID_EMPRESA)
    {
        DropDownList_CIUDAD_TRABAJADOR.Items.Clear();

        cobertura _cobertura = new cobertura(Session["idEmpresa"].ToString());
        DataTable tablaCoberturaEmpresa = _cobertura.obtenerCoberturaDeUnCliente(ID_EMPRESA);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione", "");
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

    private void cargar_DropDownList_RIESGO_EMPLEADO(Decimal ID_EMPRESA)
    {
        DropDownList_RIESGO_EMPLEADO.Items.Clear();

        empresasRiesgos _empresasRiesgos = new empresasRiesgos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaDatos = _empresasRiesgos.ObtenerRoesgosPorEmpresa(ID_EMPRESA);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione", "");
        DropDownList_RIESGO_EMPLEADO.Items.Add(item);

        foreach (DataRow fila in tablaDatos.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["DESCRIPCION_RIESGO"].ToString() + " - " + fila["CODIGO"].ToString(), fila["DESCRIPCION_RIESGO"].ToString());
            DropDownList_RIESGO_EMPLEADO.Items.Add(item);
        }
        DropDownList_RIESGO_EMPLEADO.DataBind();
    }

   
    private void cargar_ubicacion_trabajador_segun_ciudad_cc_subc(DataRow filaInfoContrato)
    { 
        String ID_CIUDAD = filaInfoContrato["ID_CIUDAD"].ToString().Trim();
        Decimal ID_CENTRO_C = 0;
        Decimal ID_SUB_C = 0;

        Decimal ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);
        Decimal ID_EMPRESA = Convert.ToDecimal(filaInfoContrato["ID_EMPRESA"]);

        try
        {
            ID_CENTRO_C = Convert.ToDecimal(filaInfoContrato["ID_CENTRO_C"]);
        }
        catch
        {
            ID_CENTRO_C = 0;
        }

        try
        {
            ID_SUB_C = Convert.ToDecimal(filaInfoContrato["ID_SUB_C"]);
        }
        catch
        {
            ID_SUB_C = 0;
        }

        HiddenField_CIUDAD_TRABAJADOR.Value = ID_CIUDAD;
        HiddenField_CENTRO_C_TRABAJADOR.Value = ID_CENTRO_C.ToString();
        HiddenField_SUB_C_TRABAJADOR.Value = ID_SUB_C.ToString();
        
        HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";

        cargar_DropDownList_RIESGO_EMPLEADO(ID_EMPRESA);

        try
        {
            DropDownList_RIESGO_EMPLEADO.SelectedValue = Convert.ToDecimal(filaInfoContrato["RIESGO"]).ToString();
        }
        catch
        { 
            DropDownList_RIESGO_EMPLEADO.SelectedIndex = 0;
        }
        HiddenField_RIESGO_INICIAL.Value = DropDownList_RIESGO_EMPLEADO.SelectedValue;

        if (String.IsNullOrEmpty(HiddenField_RIESGO_INICIAL.Value) == true)
        {
            Label_RIESGO_INICIAL.Text = "Riesgo Inicial: Desconocido";
            Label_RIESGO_INICIAL.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            Label_RIESGO_INICIAL.Text = "Riesgo Inicial: " + HiddenField_RIESGO_INICIAL.Value;
            Label_RIESGO_INICIAL.ForeColor = System.Drawing.Color.Green;
        }

        condicionesContratacion _condicionesContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        if (ID_SUB_C != 0)
        {
            subCentroCosto _subCentroCosto = new subCentroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

           
            DataTable tablaInfoSubC = _subCentroCosto.ObtenerSubCentroDeCostoPorIdSubCConInfoDeCCyCiudad(ID_SUB_C);
            DataRow filaInfoSubC = tablaInfoSubC.Rows[0];

            cargar_DropDownList_CIUDAD(ID_EMPRESA);
            DropDownList_CIUDAD_TRABAJADOR.SelectedValue = filaInfoSubC["ID_CIUDAD"].ToString().Trim();

            cargar_DropDownList_CENTRO_COSTO(ID_EMPRESA, filaInfoSubC["ID_CIUDAD"].ToString().Trim());
            DropDownList_CC_TRABAJADOR.SelectedValue = filaInfoSubC["ID_CENTRO_C"].ToString().Trim();

            cargar_DropDownList_SUB_CENTRO(ID_EMPRESA, Convert.ToDecimal(filaInfoSubC["ID_CENTRO_C"]));
            try
            {
                DropDownList_SUB_CENTRO_TRABAJADOR.SelectedValue = ID_SUB_C.ToString();
            }
            catch
            {
                DropDownList_SUB_CENTRO_TRABAJADOR.ClearSelection();
            }

            DropDownList_CIUDAD_TRABAJADOR.Enabled = true;
            DropDownList_CC_TRABAJADOR.Enabled = true;
            DropDownList_SUB_CENTRO_TRABAJADOR.Enabled = true;

         
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
        else
        {
            if (ID_CENTRO_C != 0)
            {
                centroCosto _centroCosto = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

             
                DataTable tablaInfoCentroC = _centroCosto.ObtenerCentrosDeCostoPorIdCentroCosto(ID_CENTRO_C);
                DataRow filaInfoCentroC = tablaInfoCentroC.Rows[0];

                cargar_DropDownList_CIUDAD(ID_EMPRESA);
                DropDownList_CIUDAD_TRABAJADOR.SelectedValue = filaInfoCentroC["ID_CIUDAD"].ToString().Trim();

                cargar_DropDownList_CENTRO_COSTO(ID_EMPRESA, filaInfoCentroC["ID_CIUDAD"].ToString().Trim());
                DropDownList_CC_TRABAJADOR.SelectedValue = ID_CENTRO_C.ToString();

                cargar_DropDownList_SUB_CENTRO(ID_EMPRESA, ID_CENTRO_C);

                DropDownList_CIUDAD_TRABAJADOR.Enabled = true;
                DropDownList_CC_TRABAJADOR.Enabled = true;
                DropDownList_SUB_CENTRO_TRABAJADOR.Enabled = true;

            
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
                if (String.IsNullOrEmpty(ID_CIUDAD) == false)
                {
                    cargar_DropDownList_CIUDAD(ID_EMPRESA);
                    DropDownList_CIUDAD_TRABAJADOR.SelectedValue = ID_CIUDAD;

                    cargar_DropDownList_CENTRO_COSTO(ID_EMPRESA, ID_CIUDAD);

                    DropDownList_CIUDAD_TRABAJADOR.Enabled = true;
                    DropDownList_CC_TRABAJADOR.Enabled = true;
                    inhabilitar_DropDownList_SUB_CENTRO();

                
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
                }
            }
        }

    }
    
    private void cargar_ubicacion_trabajador(DataRow filaInfoContrato)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(filaInfoContrato["ID_EMPRESA"]);
        String SEXO = filaInfoContrato["SEXO"].ToString().Trim();

        HiddenField_ID_EMPRESA.Value = filaInfoContrato["ID_EMPRESA"].ToString().Trim();
        Label_EMPRESA_TRABAJADOR.Text = filaInfoContrato["RAZ_SOCIAL"].ToString();

        cargar_DropDownList_CARGO_TRABAJADOR(ID_EMPRESA, SEXO);
        try
        {
            DropDownList_CARGO_TRABAJADOR.SelectedValue = filaInfoContrato["ID_PERFIL"].ToString().Trim();
            HiddenField_ID_PERFIL.Value = filaInfoContrato["ID_PERFIL"].ToString().Trim();
        }
        catch
        {
            DropDownList_CARGO_TRABAJADOR.ClearSelection();
            HiddenField_ID_PERFIL.Value = String.Empty;
        }

        HiddenField_ID_PERFIL.Value = filaInfoContrato["ID_PERFIL"].ToString().Trim();

        cargar_ubicacion_trabajador_segun_ciudad_cc_subc(filaInfoContrato);
    }
    
    private void cargar_DropDownList_Salario_integral()
    {
        DropDownList_SALARIO_INTEGRAL.Items.Clear();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Ninguno", "");
        DropDownList_SALARIO_INTEGRAL.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("SI", "S");
        DropDownList_SALARIO_INTEGRAL.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("No", "N");
        DropDownList_SALARIO_INTEGRAL.Items.Add(item);

        DropDownList_SALARIO_INTEGRAL.DataBind();
    }
    
    private void cargar_datos_salariales(DataRow filaInfoContrato)
    {

        Panel_SALARIO_NOM_VALOR_UNIDAD.Visible = false;
        Panel_SALARIO.Visible = false;

        TextBox_SALARIO_NOMINA.Text = "";
        TextBox_SALARIO.Text = "";

        cargar_DropDownList_Salario_integral();

        if (filaInfoContrato["PAGO_DIAS_PRODUCTIVIDAD"].ToString().Trim() == "S")
        {
            HiddenField_PAGO_DIAS_PRODUCTIVIDAD.Value = "S";
            HiddenField_SALARIO.Value = "";

            Panel_SALARIO_NOM_VALOR_UNIDAD.Visible = true;

            TextBox_SALARIO_NOMINA.Text = String.Format("{0:N0}",Convert.ToDecimal(filaInfoContrato["VALOR_NOMINA"]));
            HiddenField_VALOR_NOMINA.Value = TextBox_SALARIO_NOMINA.Text;
        }
        else
        {
            HiddenField_PAGO_DIAS_PRODUCTIVIDAD.Value = "N";
            Panel_SALARIO.Visible = true;
            TextBox_SALARIO.Text = String.Format("{0:N0}",Convert.ToDecimal(filaInfoContrato["SALARIO"]));
            HiddenField_SALARIO.Value = TextBox_SALARIO.Text;
        }

        DropDownList_SALARIO_INTEGRAL.SelectedValue = filaInfoContrato["SAL_INT"].ToString().Trim().ToUpper();
        HiddenField_SAL_INT.Value = DropDownList_SALARIO_INTEGRAL.SelectedValue;

    }
    
    private void cargar_DropDownList_CLASE_CONTRATO()
    {
        DropDownList_CLASE_CONTRATO.Items.Clear();
        parametro _parametro = new parametro(Session["idEmpresa"].ToString());
        DataTable tablaParametro = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_CLASE_CONTRATO);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione", "");
        DropDownList_CLASE_CONTRATO.Items.Add(item);

        foreach (DataRow fila in tablaParametro.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_CLASE_CONTRATO.Items.Add(item);
        }

        DropDownList_CLASE_CONTRATO.DataBind();
    }
    
    private void cargar_DropDownList_TIPO_CONTRATO()
    {
        DropDownList_TIPO_CONTRATO.Items.Clear();
        parametro _parametro = new parametro(Session["idEmpresa"].ToString());
        DataTable tablaParametro = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_TIPO_CONTRATO);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione", "");
        DropDownList_TIPO_CONTRATO.Items.Add(item);

        foreach (DataRow fila in tablaParametro.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_TIPO_CONTRATO.Items.Add(item);
        }

        DropDownList_TIPO_CONTRATO.DataBind();
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






    
    private void cargar_datos_basicos_de_contrato(DataRow filaInfoContrato)
    {

        if (!string.IsNullOrEmpty(filaInfoContrato["fecha_inicia_contrato_empresa"].ToString())) TextBox_fecha_inicia_contrato_empresa.Text = filaInfoContrato["fecha_inicia_contrato_empresa"].ToString();
        if (!string.IsNullOrEmpty(filaInfoContrato["fecha_vence_contrato_empresa"].ToString())) TextBox_fecha_vence_contrato_empresa.Text = filaInfoContrato["fecha_vence_contrato_empresa"].ToString();
        if (!string.IsNullOrEmpty(filaInfoContrato["objeto_contrato"].ToString())) TextBox_objeto_contrato.Text = filaInfoContrato["objeto_contrato"].ToString();
        if (!string.IsNullOrEmpty(filaInfoContrato["funciones"].ToString())) TextBox_funciones_cargo.Text = filaInfoContrato["funciones"].ToString();

        cargar_DropDownList_entidad_bancaria_todos();
        cargar_DropDownList_FORMAS_DE_PAGO(DropDownList_forma_pago);
        cargar_DropDownList_TIPO_CUENTA(DropDownList_TIPO_CUENTA);
        cargar_DropDownList_PERIODO_PAGO(DropDownList_PERIODO_PAGO);
        cargar_DropDownList_ChequeReg(DropDownList_ChequeReg);

        RadioButtonList_ESTADOS_SENA.SelectedValue = "NINGUNO";
        cargar_DropDownList_CLASE_CONTRATO();
        cargar_DropDownList_TIPO_CONTRATO();

        TextBox_FECHA_INICIO.Text = "";
        TextBox_FECHA_FIN.Text = "";

        DropDownList_PERIODO_PAGO.SelectedValue = filaInfoContrato["PERIODO_PAGO"].ToString().Trim();
        HiddenField_PERIODO_PAGO.Value = DropDownList_PERIODO_PAGO.SelectedValue;

        if ((filaInfoContrato["CONTROL_CONTRATO"].ToString().Trim().ToUpper() == "CORRECTO") || (DBNull.Value.Equals(filaInfoContrato["CONTROL_CONTRATO"]) == true))
        {
            CheckBox_DobleRegistro.Checked = false;
            CheckBox_NoLaboro.Checked = false;

            Panel_FORMA_PAGO.Visible = true;

            try
            {
                DropDownList_forma_pago.SelectedValue = filaInfoContrato["FORMA_PAGO"].ToString().Trim();
            }
            catch
            {
                DropDownList_forma_pago.ClearSelection();  
            }

            if ((DropDownList_forma_pago.SelectedValue == "CONSIGNACIÓN BANCARIA") || (DropDownList_forma_pago.SelectedValue == "DISPERSION") || (DropDownList_forma_pago.SelectedValue == "ACH"))
            {
                Panel_ENTIDAD_BANCARIA_Y_CUENTA.Visible = true;
                Panel_PagoCheque.Visible = false;

                try
                {
                    DropDownList_entidad_bancaria.SelectedValue = filaInfoContrato["ID_ENTIDAD"].ToString().Trim();
                }
                catch
                {
                    DropDownList_entidad_bancaria.ClearSelection();
                }

                try
                {
                    DropDownList_TIPO_CUENTA.SelectedValue = filaInfoContrato["TIPO_CUENTA"].ToString().Trim();
                }
                catch
                {
                    DropDownList_TIPO_CUENTA.ClearSelection();
                }
                
                TextBox_NUMERO_CUENTA.Text = filaInfoContrato["NUM_CUENTA"].ToString().Trim();

            }
            else
            {
                if (DropDownList_forma_pago.SelectedValue == "CHEQUE")
                {
                    Panel_ENTIDAD_BANCARIA_Y_CUENTA.Visible = false;
                    Panel_PagoCheque.Visible = true;

                    try
                    {
                        DropDownList_ChequeReg.SelectedValue = filaInfoContrato["CHEQUE_REG"].ToString().Trim();
                    }
                    catch
                    {
                        DropDownList_ChequeReg.ClearSelection();
                    }
                }
                else
                {
                    Panel_ENTIDAD_BANCARIA_Y_CUENTA.Visible = false;
                    Panel_PagoCheque.Visible = false;
                    DropDownList_entidad_bancaria.ClearSelection();
                    DropDownList_TIPO_CUENTA.ClearSelection();
                    TextBox_NUMERO_CUENTA.Text = "";
                }
            }
        }
        else
        {
            Panel_FORMA_PAGO.Visible = false;

            Panel_ENTIDAD_BANCARIA_Y_CUENTA.Visible = false;
            DropDownList_entidad_bancaria.ClearSelection();
            DropDownList_TIPO_CUENTA.ClearSelection();
            TextBox_NUMERO_CUENTA.Text = "";

            if (filaInfoContrato["CONTROL_CONTRATO"].ToString().Trim().ToUpper() == "NO LABORO")
            {
                CheckBox_DobleRegistro.Checked = false;
                CheckBox_NoLaboro.Checked = true;
            }
            else
            {
                CheckBox_DobleRegistro.Checked = true;
                CheckBox_NoLaboro.Checked = false;
            }
        }

        if (filaInfoContrato["PRACTICANTE_UNIVERSITARIO"].ToString().Trim() == "S")
        {
            RadioButtonList_ESTADOS_SENA.SelectedValue = "PRACTICANTE";
        }
        else
        {
            if (filaInfoContrato["SENA_ELECTIVO"].ToString().Trim() == "S")
            {
                RadioButtonList_ESTADOS_SENA.SelectedValue = "LECTIVO";
            }
            else
            {
                if (filaInfoContrato["SENA_PRODUCTIVO"].ToString().Trim() == "S")
                {
                    RadioButtonList_ESTADOS_SENA.SelectedValue = "PRODUCTIVO";
                }
            }
        }

        HiddenField_ESTADOS_SENA.Value = RadioButtonList_ESTADOS_SENA.SelectedValue;

        try
        {
            DropDownList_CLASE_CONTRATO.SelectedValue = filaInfoContrato["CLASE_CONTRATO"].ToString().Trim();
        }
        catch
        {
            DropDownList_CLASE_CONTRATO.SelectedIndex = 0;
        }
        HiddenField_CLASE_CONTRATO.Value = DropDownList_CLASE_CONTRATO.SelectedValue;

        try
        {
            DropDownList_TIPO_CONTRATO.SelectedValue = filaInfoContrato["TIPO_CONTRATO"].ToString().Trim();
        }
        catch
        {
            DropDownList_TIPO_CONTRATO.SelectedIndex = 0;
        }
        HiddenField_TIPO_CONTRATO.Value = DropDownList_TIPO_CONTRATO.SelectedValue;


        if (!DBNull.Value.Equals(filaInfoContrato["FECHA_INICIA"]))
        {
            TextBox_FECHA_INICIO.Text = Convert.ToDateTime(filaInfoContrato["FECHA_INICIA"]).ToShortDateString();
            HiddenField_FECHA_INICIA.Value = TextBox_FECHA_INICIO.Text;
        }

        if (!DBNull.Value.Equals(filaInfoContrato["FECHA_TERMINA"]))
        {
            TextBox_FECHA_FIN.Text = Convert.ToDateTime(filaInfoContrato["FECHA_TERMINA"]).ToShortDateString();
            HiddenField_FECHA_TERMINA.Value = TextBox_FECHA_FIN.Text;
        }
    }
    
    private void cargar_DropDownList_FORMAS_DE_PAGO(DropDownList drop)
    {
        drop.Items.Clear();

        parametro forma_pago = new parametro(Session["idEmpresa"].ToString());
        DataTable TABLA_FORMA_PAGO = forma_pago.ObtenerParametrosPorTabla(tabla.PARAMETROS_FORMA_PAGO);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione", "");
        drop.Items.Add(item);
        
        foreach (DataRow fila in TABLA_FORMA_PAGO.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            drop.Items.Add(item);
        }
        drop.DataBind();
    }
    
    private void cargar_DropDownList_TIPO_CUENTA(DropDownList drop)
    {
        drop.Items.Clear();

        parametro forma_pago = new parametro(Session["idEmpresa"].ToString());
        DataTable TABLA_TIPO_CUENTA = forma_pago.ObtenerParametrosPorTabla(tabla.PARAMETROS_TIPO_CUENTA);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione", "");
        drop.Items.Add(item);

        foreach (DataRow fila in TABLA_TIPO_CUENTA.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            drop.Items.Add(item);
        }
        drop.DataBind();
    }

    private void cargar_DropDownList_ChequeReg(DropDownList drop)
    {
        drop.Items.Clear();

        parametro cheque_reg = new parametro(Session["idEmpresa"].ToString());
        DataTable TABLA_CHEQUE_REG = cheque_reg.ObtenerParametrosPorTabla(tabla.PARAMETROS_CHEQUE_TIPO);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        drop.Items.Add(item);

        foreach (DataRow fila in TABLA_CHEQUE_REG.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            drop.Items.Add(item);
        }

        drop.DataBind();
    }
    
    private void cargar_DropDownList_ENTIDAD_BANCARIA(Decimal ID_EMPRESA)
    {
        DropDownList_entidad_bancaria.Items.Clear();

        bancosPorEmpresa _banco = new bancosPorEmpresa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaBancos = _banco.ObtenerconBancoEmpresaPorEmpresa(ID_EMPRESA);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione", "");
        DropDownList_entidad_bancaria.Items.Add(item);

        foreach (DataRow fila in tablaBancos.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOM_BANCO"].ToString(), fila["REGISTRO"].ToString());
            DropDownList_entidad_bancaria.Items.Add(item);
        }
        DropDownList_entidad_bancaria.DataBind();
    }
    
    private void cargar_seccion_contratos(Decimal ID_EMPLEADO)
    { 
        Ocultar(Acciones.contrato);

        auditoriaContratos _auditoriaContratos = new auditoriaContratos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaUltimaAuditoria = _auditoriaContratos.ObtenerUltimaAuditoriaPorTablaYEmpleado(tabla.CON_REG_CONTRATOS, ID_EMPLEADO);
        if (tablaUltimaAuditoria.Rows.Count > 0)
        {
            DataRow filaInfoAuditoria = tablaUltimaAuditoria.Rows[0];
            Panel_CABEZA_CONTRATO.BackColor = colorAuditado;
            Label_CONTRATO_AUDITADA.Text = "(AUDITADO) - " + Convert.ToDateTime(filaInfoAuditoria["FECHA_AUDITORIA"]).ToShortDateString();
        }
        else
        {
            Panel_CABEZA_CONTRATO.BackColor = colorSinAuditar;
            Label_CONTRATO_AUDITADA.Text = "(SIN AUDITAR)";

            if (String.IsNullOrEmpty(HiddenField_SECCIONES_SIN_AUDITORIA.Value) == true)
            {
                HiddenField_SECCIONES_SIN_AUDITORIA.Value = SeccionesAuditoria.Contrato.ToString();
            }
            else
            {
                HiddenField_SECCIONES_SIN_AUDITORIA.Value = ";" + SeccionesAuditoria.Contrato.ToString();
            }
        }
        registroContrato _registroContrato = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        
       
        DataTable tablaInfoContrato = _registroContrato.ObtenerDatosContratoParaAuditar(ID_EMPLEADO);

        if (!tablaInfoContrato.Rows.Count.Equals(0))
        {
            DataRow filaInfoContrato = tablaInfoContrato.Rows[0];

            cargar_ubicacion_trabajador(filaInfoContrato);

            cargar_datos_salariales(filaInfoContrato);

            cargar_datos_basicos_de_contrato(filaInfoContrato);

        }
    }

    private void Desactivar(Acciones accion)
    { 
        switch(accion)
        {
            case Acciones.CargarEnvioArchivos:
                CheckBoxList_DOCUMENTOS_SELECCION.Enabled = false;
                TextBox_EMAIL_SELECCION.Enabled = false;
                CheckBoxList_DOCUEMENTOS_CONTRATACION.Enabled = false;
                TextBox_EMAIL_CONTRATACION.Enabled = false;
                break;
        }
    }

    private void SeleccionarDocsSeleccion(DataRow fila)
    {
        String[] listaDocsSeleccionados = fila["DOCUMENTOS_SELECCION"].ToString().Trim().Split(';');

        foreach (System.Web.UI.WebControls.ListItem item in CheckBoxList_DOCUMENTOS_SELECCION.Items)
        {
            Boolean seleccionar = false;

            for (int i = 0; i < listaDocsSeleccionados.Length; i++)
            {
                if (item.Value == listaDocsSeleccionados[i])
                {
                    seleccionar = true;
                    break;
                }
            }

            item.Selected = seleccionar;
        }

    }

    private void SeleccionarDocsContratacion(DataRow fila)
    {
        String[] listaDocsSeleccionados = fila["DOCUMENTOS_CONTRATACION"].ToString().Trim().Split(';');

        foreach (System.Web.UI.WebControls.ListItem item in CheckBoxList_DOCUEMENTOS_CONTRATACION.Items)
        {
            Boolean seleccionar = false;

            for (int i = 0; i < listaDocsSeleccionados.Length; i++)
            {
                if (item.Value == listaDocsSeleccionados[i])
                {
                    seleccionar = true;
                    break;
                }
            }

            item.Selected = seleccionar;
        }
    }

    private void SeleccionarEmailSeleccion(DataRow fila)
    {
        Decimal ID_CONTACTO = !DBNull.Value.Equals(fila["ID_CONTACTO_SELECCION"]) ? Convert.ToDecimal(fila["ID_CONTACTO_SELECCION"]) : 0;
        contactos _contactos = new contactos(Session["idEmpresa"].ToString());
        DataTable tablaContacto = _contactos.ObtenerContactosPorRegistro(ID_CONTACTO);
        if (!tablaContacto.Rows.Count.Equals(0))
        {
            DataRow filaContacto = tablaContacto.Rows[0];
            Label_NOMBRE_CONTACTO_SELECCION.Text = filaContacto["CONT_NOM"].ToString().Trim();
            TextBox_EMAIL_SELECCION.Text = filaContacto["CONT_MAIL"].ToString().Trim();
        }
    }

    private void SeleccionarEmailContratacion(DataRow fila)
    {
        Decimal ID_CONTACTO = !DBNull.Value.Equals(fila["ID_CONTACTO_CONTRATACION"]) ? Convert.ToDecimal(fila["ID_CONTACTO_CONTRATACION"]) : 0;
        contactos _contactos = new contactos(Session["idEmpresa"].ToString());
        DataTable tablaContacto = _contactos.ObtenerContactosPorRegistro(ID_CONTACTO);

        if (!tablaContacto.Rows.Count.Equals(0))
        {
            DataRow filaContacto = tablaContacto.Rows[0];
            Label_NOMBRE_CONTACTO_CONTRATACION.Text = filaContacto["CONT_NOM"].ToString().Trim();
            TextBox_EMAIL_CONTRATACION.Text = filaContacto["CONT_MAIL"].ToString().Trim();
        }
    }

    private void cargar_seccion_envio_archivos(Decimal ID_EMPLEADO)
    {
        auditoriaContratos _auditoriaContratos = new auditoriaContratos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaUltimaAuditoria = _auditoriaContratos.ObtenerUltimaAuditoriaPorTablaYEmpleado(tabla.CON_CONFIGURACON_DOCS_ENTREGABLES, ID_EMPLEADO);
        if (tablaUltimaAuditoria.Rows.Count > 0)
        {
            DataRow filaInfoAuditoria = tablaUltimaAuditoria.Rows[0];
            Panel_CABEZA_ENVIOARCHIVOS.BackColor = colorAuditado;
            Label_ENVIOARCHIVOS_AUDITORIA.Text = "(AUDITADO) - " + Convert.ToDateTime(filaInfoAuditoria["FECHA_AUDITORIA"]).ToShortDateString();
        }
        else
        {
            if (String.IsNullOrEmpty(HiddenField_SECCIONES_SIN_AUDITORIA.Value) == true)
            {
                HiddenField_SECCIONES_SIN_AUDITORIA.Value = SeccionesAuditoria.ExamenesAutosRecomendacion.ToString();
            }
            else
            {
                HiddenField_SECCIONES_SIN_AUDITORIA.Value = ";" + SeccionesAuditoria.ExamenesAutosRecomendacion.ToString();
            }
        }

        Ocultar(Acciones.CargarEnvioArchivos);
        Desactivar(Acciones.CargarEnvioArchivos);

        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        ConfDocEntregable _confDocEntregable = new ConfDocEntregable(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaDos = _confDocEntregable.ObtenerPorEmpresa(ID_EMPRESA);

        if (!tablaDos.Rows.Count.Equals(0))
        {
            DataRow filaDocs = tablaDos.Rows[0];

            if (filaDocs["ENTREGA_DOCUMENTOS"].ToString().ToUpper() == "FALSE")
            {
                Mostrar(Acciones.CargarEnvioArchivos);
            }
            else
            {
                Mostrar(Acciones.CargarEnvioArchivos);
                SeleccionarDocsSeleccion(filaDocs);
                SeleccionarDocsContratacion(filaDocs);
                SeleccionarEmailSeleccion(filaDocs);
                SeleccionarEmailContratacion(filaDocs);
            }
        }
    }

    private void Cargar(Decimal ID_SOLICITUD, Decimal ID_EMPLEADO, Decimal ID_REQUERIMIENTO, Decimal ID_PERFIL, Decimal ID_CONTRATO)
    {
        HiddenField_ID_REQUERIMIENTO.Value = ID_REQUERIMIENTO.ToString();
        HiddenField_ID_PERFIL.Value = ID_PERFIL.ToString();
        HiddenField_ID_CONTRATO.Value = ID_CONTRATO.ToString();

        cargar_seccion_solicitud_ingreso(ID_SOLICITUD, ID_EMPLEADO);

        carar_seccion_afiliaciones(ID_SOLICITUD, ID_REQUERIMIENTO, ID_EMPLEADO);

        cargar_seccion_contratos(ID_EMPLEADO);

        cargar_seccion_conceptos_fijos(ID_PERFIL, ID_EMPLEADO);

        cargar_seccion_examenes_autos(ID_EMPLEADO);
        
        cargar_seccion_envio_archivos(ID_EMPLEADO);

        presentar_interfaz_segun_resultado();
    }
    
    protected void GridView_RESULTADOS_BUSQUEDA_CONTRATOS_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int filaSeleccionada = Convert.ToInt32(e.CommandArgument);

        Decimal ID_EMPRESA;
        Decimal ID_REQUERIMIENTO;
        Decimal ID_SOLICITUD;
        Decimal ID_EMPLEADO;
        Decimal ID_CONTRATO;
        Decimal ID_PERFIL;

        if (e.CommandName == "seleccionar")
        {
            ID_EMPRESA = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA_CONTRATOS.DataKeys[filaSeleccionada].Values["ID_EMPRESA"]);
            ID_REQUERIMIENTO = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA_CONTRATOS.DataKeys[filaSeleccionada].Values["ID_REQUERIMIENTO"]);
            ID_SOLICITUD = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA_CONTRATOS.DataKeys[filaSeleccionada].Values["ID_SOLICITUD"]);
            ID_EMPLEADO = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA_CONTRATOS.DataKeys[filaSeleccionada].Values["ID_EMPLEADO"]);
            ID_CONTRATO = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA_CONTRATOS.DataKeys[filaSeleccionada].Values["REGISTRO_CONTRATO"]);
            ID_PERFIL = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA_CONTRATOS.DataKeys[filaSeleccionada].Values["REGISTRO_PERFIL"]);

            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.contratoSeleccionado);
            Cargar(ID_SOLICITUD, ID_EMPLEADO, ID_REQUERIMIENTO, ID_PERFIL, ID_CONTRATO);
        }
    }
    
    protected void DropDownList_SEXO_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_SEXO.SelectedValue == "M")
        {
            Mostrar(Acciones.libretaMilitar);
        }
        else
        {
            Ocultar(Acciones.libretaMilitar);
        }
    }
    
    protected void DropDownList_DEPARTAMENTO_CEDULA_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_DEPARTAMENTO_CEDULA.SelectedValue == "")
        {
            inhabilitar_DropDownList_CIU_CEDULA();
        }
        else
        {
            String ID_DEPARTAMENTO = DropDownList_DEPARTAMENTO_CEDULA.SelectedValue.ToString();
            cargar_DropDownList_CIU_CEDULA(ID_DEPARTAMENTO);
            DropDownList_CIU_CEDULA.Enabled = true;
        }
    }
    
    protected void DropDownList_DEPARTAMENTO_ASPIRANTE_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_DEPARTAMENTO_ASPIRANTE.SelectedValue == "")
        {
            inhabilitar_DropDownList_CIU_ASPIRANTE();
        }
        else
        {
            String ID_DEPARTAMENTO = DropDownList_DEPARTAMENTO_ASPIRANTE.SelectedValue.ToString();
            cargar_DropDownList_CIU_ASPIRANTE(ID_DEPARTAMENTO);
            DropDownList_CIU_ASPIRANTE.Enabled = true;
        }
    }
    
    private Boolean comprobar_proceso_auditoria()
    {
        if (Label_SOLICITUD_INGRESO_AUDITADO.Text == "(SIN AUDITAR)")
        {
            return false;
        }
        else
        {
            if (Label_AFILIACION_ARP_AUDITORIA.Text == "(SIN AUDITAR)")
            {
                return false;
            }
            else
            {
                if (Label_AFILIACION_EPS_AUDITADA.Text == "(SIN AUDITAR)")
                {
                    return false;
                }
                else
                {
                    if (Label_AFILIACION_CCF_AUDITADA.Text == "(SIN AUDITAR)")
                    {
                        return false;
                    }
                    else 
                    {
                        if (Label_AFILIACION_AFP_AUDITADA.Text == "(SIN AUDITAR)")
                        {
                            return false;
                        }
                        else
                        {
                            if (Label_CONCEPTOS_FIJOS_AUDITADA.Text == "(SIN AUDITAR)")
                            {
                                return false;
                            }
                            else
                            {
                                if (Label_CONTRATO_AUDITADA.Text == "(SIN AUDITAR)")
                                {
                                    return false;
                                }
                                else
                                {
                                    if (Label_EXAMENES_AUDITORIA.Text == "(SIN AUDITAR)")
                                    {
                                        return false;
                                    }
                                    else
                                    {
                                        if (Label_ENVIOARCHIVOS_AUDITORIA.Text == "(SIN AUDITAR)")
                                        {
                                            return false;
                                        }
                                        else
                                        {
                                            return true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            } 
        }
    }
    
    private void Actualizar_solicitud()
    {
        Decimal ID_EMPLEADO = Convert.ToDecimal(Label_ID_EMPLEADO.Text);
        Decimal ID_SOLICITUD = Convert.ToDecimal(TextBox_ID_SOLICITUD.Text);

        String APELLIDOS = TextBox_APELLIDOS.Text.ToUpper().Trim();
        String NOMBRES = TextBox_NOMBRES.Text.ToUpper().Trim();
        String SEXO = DropDownList_SEXO.SelectedValue;
        DateTime FCH_NACIMIENTO = Convert.ToDateTime(TextBox_FCH_NACIMIENTO.Text);

        String TIP_DOC_IDENTIDAD = DropDownList_TIP_DOC_IDENTIDAD.SelectedValue.ToString();
        String NUM_DOC_IDENTIDAD = TextBox_NUM_DOC_IDENTIDAD.Text.Trim();
        String CIU_CEDULA = DropDownList_CIU_CEDULA.SelectedValue;
        String TEL_ASPIRANTE = TextBox_TEL_ASPIRANTE.Text.Trim();
        String E_MAIL = TextBox_E_MAIL.Text.Trim();

        String CAT_LIC_COND = null;
        if (DropDownList_CAT_LIC_COND.SelectedIndex > 0)
        {
            CAT_LIC_COND = DropDownList_CAT_LIC_COND.SelectedValue;
        }

        String DIR_ASPIRANTE = TextBox_DIR_ASPIRANTE.Text.Trim().ToUpper();
        String CIU_ASPIRANTE = DropDownList_CIU_ASPIRANTE.SelectedValue;
        String SECTOR = TextBox_SECTOR.Text.ToUpper().Trim();
        Decimal SALARIO = Convert.ToDecimal(TextBox_ASPIRACION_SALARIAL.Text);
        String LIB_MILITAR = TextBox_LIB_MILITAR.Text.ToUpper().Trim();

        Boolean ACTUALIZAR_ESTADO_PROCESO = true;
        if (HiddenField_PRESENTACION.Value == Presentacion.ContratosActivos.ToString())
        {
            ACTUALIZAR_ESTADO_PROCESO = false;
        }

        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        Boolean verificador = _radicacionHojasDeVida.ActualizarRegSolicitudesingreso(ID_SOLICITUD, APELLIDOS, NOMBRES, TIP_DOC_IDENTIDAD, NUM_DOC_IDENTIDAD, CIU_CEDULA, LIB_MILITAR, CAT_LIC_COND, DIR_ASPIRANTE, SECTOR, CIU_ASPIRANTE, TEL_ASPIRANTE, SEXO, FCH_NACIMIENTO, E_MAIL, SALARIO, ID_EMPLEADO,ACTUALIZAR_ESTADO_PROCESO);

        if (verificador == false)
        {
            Informar(Panel_MENSAJES, Label_MENSAJE, _radicacionHojasDeVida.MensajeError, Proceso.Error);
        }
        else
        {
            Informar(Panel_MENSAJES, Label_MENSAJE, "La información de la solicitud fue actualizada correctamente.", Proceso.Correcto);
        }

        cargar_seccion_solicitud_ingreso(ID_SOLICITUD, ID_EMPLEADO);

        presentar_interfaz_segun_resultado();
    }
    
    protected void Button_Actualizar_Click(object sender, EventArgs e)
    {
        Actualizar_solicitud();
    }
    
    protected void GridView_HOJA_DE_TRABAJO_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int filaSeleccionada = Convert.ToInt32(e.CommandArgument);

        Decimal ID_EMPRESA;
        Decimal ID_REQUERIMIENTO;
        Decimal ID_SOLICITUD;
        Decimal ID_EMPLEADO;
        Decimal ID_CONTRATO;
        Decimal ID_PERFIL;

        if (e.CommandName == "seleccionar")
        {
            ID_EMPRESA = Convert.ToDecimal(GridView_HOJA_DE_TRABAJO.DataKeys[filaSeleccionada].Values["ID_EMPRESA"]);
            HiddenField_ID_EMPRESA.Value = ID_EMPRESA.ToString();
            ID_REQUERIMIENTO = Convert.ToDecimal(GridView_HOJA_DE_TRABAJO.DataKeys[filaSeleccionada].Values["ID_REQUERIMIENTO"]);
            ID_SOLICITUD = Convert.ToDecimal(GridView_HOJA_DE_TRABAJO.DataKeys[filaSeleccionada].Values["ID_SOLICITUD"]);
            ID_EMPLEADO = Convert.ToDecimal(GridView_HOJA_DE_TRABAJO.DataKeys[filaSeleccionada].Values["ID_EMPLEADO"]);
            ID_CONTRATO = Convert.ToDecimal(GridView_HOJA_DE_TRABAJO.DataKeys[filaSeleccionada].Values["REGISTRO_CONTRATO"]);
            ID_PERFIL = Convert.ToDecimal(GridView_HOJA_DE_TRABAJO.DataKeys[filaSeleccionada].Values["REGISTRO_PERFIL"]);

            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.contratoSeleccionado);
            Cargar(ID_SOLICITUD, ID_EMPLEADO, ID_REQUERIMIENTO, ID_PERFIL, ID_CONTRATO);
        }
    }
    
    protected void LinkButton_LINK_HOJA_TRABAJO_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }

    protected void LinkButton_LINK_SELECCIONAR_CONTRATO_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.buscarContratos);
        Cargar(Acciones.buscarContratos);
    }
    
    protected void GridView_ARP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int filaSeleccionada = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "copiar")
        {
            try
            {
                TextBox_FECHA_R_ARP.Text = Convert.ToDateTime(GridView_ARP.Rows[filaSeleccionada].Cells[5].Text).ToShortDateString();
            }
            catch
            {
                TextBox_FECHA_R_ARP.Text = "";
            }

            try
            {
                TextBox_FECHA_RADICACION_ARP.Text = Convert.ToDateTime(GridView_ARP.Rows[filaSeleccionada].Cells[6].Text).ToShortDateString();
            }
            catch
            {
                TextBox_FECHA_RADICACION_ARP.Text = "";
            }

            try
            {
                DropDownList_ENTIDAD_ARP.SelectedValue = GridView_ARP.DataKeys[filaSeleccionada].Values["ID_ARP"].ToString();
            }
            catch
            {
                DropDownList_ENTIDAD_ARP.ClearSelection();
            }
            TextBox_OBS_ARP.Text = GridView_ARP.Rows[filaSeleccionada].Cells[8].Text;
        }
    }

    
    protected void Button_ACTUALIZAR_AFILIACION_ARP_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();

        Decimal ID_SOLICITUD = Convert.ToDecimal(TextBox_ID_SOLICITUD.Text);
        Decimal ID_REQUERIMIENTO = Convert.ToDecimal(HiddenField_ID_REQUERIMIENTO.Value);
        Decimal ID_EMPLEADO = Convert.ToDecimal(Label_ID_EMPLEADO.Text);
        Decimal ID_CONTRATO = Convert.ToDecimal(HiddenField_ID_CONTRATO.Value);

        DateTime FECHA_R;
        DateTime FECHA_RADICACION;
        String OBSERVACIONES;
        Decimal REGISTRO_AFILIACION_ARP;

        Decimal ID_ARP = Convert.ToDecimal(DropDownList_ENTIDAD_ARP.SelectedValue);
        FECHA_R = Convert.ToDateTime(TextBox_FECHA_R_ARP.Text);
        FECHA_RADICACION = Convert.ToDateTime(TextBox_FECHA_RADICACION_ARP.Text);
        OBSERVACIONES = TextBox_OBS_ARP.Text.Trim().ToUpper();

        Boolean ACTUALIZAR_ESTADO_PROCESO = true;
        if (HiddenField_PRESENTACION.Value == Presentacion.ContratosActivos.ToString())
        {
            ACTUALIZAR_ESTADO_PROCESO = false;
        }

        Byte[] ARCHIVO_RADICACION = null;
        Int32 ARCHIVO_RADICACION_TAMANO = 0;
        String ARCHIVO_RADICACION_EXTENSION = null;
        String ARCHIVO_RADICACION_TYPE = null;
        if (FileUpload_ARCHIVO_AFILIACION_ARP.HasFile == true)
        {
            using (BinaryReader reader = new BinaryReader(FileUpload_ARCHIVO_AFILIACION_ARP.PostedFile.InputStream))
            {
                ARCHIVO_RADICACION = reader.ReadBytes(FileUpload_ARCHIVO_AFILIACION_ARP.PostedFile.ContentLength);
                ARCHIVO_RADICACION_TAMANO = FileUpload_ARCHIVO_AFILIACION_ARP.PostedFile.ContentLength;
                ARCHIVO_RADICACION_TYPE = FileUpload_ARCHIVO_AFILIACION_ARP.PostedFile.ContentType;
                ARCHIVO_RADICACION_EXTENSION = _tools.obtenerExtensionArchivo(FileUpload_ARCHIVO_AFILIACION_ARP.PostedFile.FileName);
            }
        }

        afiliacion _afiliacion = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        if (Label_ID_AFLIACION_ARP.Text == "Sin asignar")
        {

          
            REGISTRO_AFILIACION_ARP = _afiliacion.AdicionarconafiliacionArpAuditoria(ID_SOLICITUD, ID_ARP, FECHA_R, OBSERVACIONES, ID_REQUERIMIENTO,ID_EMPLEADO, ACTUALIZAR_ESTADO_PROCESO, FECHA_RADICACION, ID_CONTRATO, EntidadesAfiliacion.ARP.ToString(), ARCHIVO_RADICACION, ARCHIVO_RADICACION_TAMANO, ARCHIVO_RADICACION_EXTENSION, ARCHIVO_RADICACION_TYPE);

            if (REGISTRO_AFILIACION_ARP == 0)
            {
                Informar(Panel_MENSAJES, Label_MENSAJE, _afiliacion.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_MENSAJES, Label_MENSAJE, "La información de la afilicaión a ARL fue actualizada correctamente.", Proceso.Correcto);
            }

            cargar_arp(REGISTRO_AFILIACION_ARP, ID_SOLICITUD, ID_EMPLEADO);

            presentar_interfaz_segun_resultado();
        }
        else
        {

            if ((HiddenField_ENTIDAD_ARP.Value != DropDownList_ENTIDAD_ARP.SelectedValue) || (HiddenField_FECHA_R_ARP.Value != TextBox_FECHA_R_ARP.Text) || (HiddenField_FECHA_RADICACION_ARP.Value != TextBox_FECHA_RADICACION_ARP.Text) || (HiddenField_OBS_ARP.Value != TextBox_OBS_ARP.Text) || (ARCHIVO_RADICACION != null))
            {
                REGISTRO_AFILIACION_ARP = _afiliacion.AdicionarconafiliacionArpAuditoria(ID_SOLICITUD, ID_ARP, FECHA_R, OBSERVACIONES, ID_REQUERIMIENTO, ID_EMPLEADO, ACTUALIZAR_ESTADO_PROCESO, FECHA_RADICACION, ID_CONTRATO, EntidadesAfiliacion.ARP.ToString(), ARCHIVO_RADICACION, ARCHIVO_RADICACION_TAMANO, ARCHIVO_RADICACION_EXTENSION, ARCHIVO_RADICACION_TYPE);

                if (REGISTRO_AFILIACION_ARP == 0)
                {
                    Informar(Panel_MENSAJES, Label_MENSAJE, _afiliacion.MensajeError, Proceso.Error);
                }
                else
                {
                    Informar(Panel_MENSAJES, Label_MENSAJE, "La información de la afilicaión a ARL fue actualizada correctamente.", Proceso.Correcto);
                }
            }
            else
            {
                REGISTRO_AFILIACION_ARP = Convert.ToDecimal(Label_ID_AFLIACION_ARP.Text);

                auditoriaContratos _auditoriaContratos = new auditoriaContratos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

             
                Decimal ID_AUDITORIA = _auditoriaContratos.ActualizarAuditoriaContratosPorSeccionYEstadoProceso(ID_EMPLEADO, tabla.CON_AFILIACION_ARP, REGISTRO_AFILIACION_ARP, ACTUALIZAR_ESTADO_PROCESO, ID_SOLICITUD, tabla.VAR_ESTADO_PROCESO_CON_AFILIACION_ARP);

                if (ID_AUDITORIA == 0)
                {
                    Informar(Panel_MENSAJES, Label_MENSAJE, _auditoriaContratos.MensajeError, Proceso.Error);
                }
                else
                {
                    Informar(Panel_MENSAJES, Label_MENSAJE, "La información de la afilicaión a ARL fue actualizada correctamente (solo auditoría).", Proceso.Correcto);
                }
            }

            cargar_arp(REGISTRO_AFILIACION_ARP, ID_SOLICITUD, ID_EMPLEADO);

            presentar_interfaz_segun_resultado();
        }
    }

   
    protected void GridView_EPS_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int filaSeleccionada = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "copiar")
        {
            try
            {
                TextBox_FECHA_R_EPS.Text = Convert.ToDateTime(GridView_EPS.Rows[filaSeleccionada].Cells[5].Text).ToShortDateString();
            }
            catch
            {
                TextBox_FECHA_R_EPS.Text = "";
            }

            try
            {
                TextBox_FECHA_RADICACION_EPS.Text = Convert.ToDateTime(GridView_EPS.Rows[filaSeleccionada].Cells[6].Text).ToShortDateString();
            }
            catch
            {
                TextBox_FECHA_RADICACION_EPS.Text = "";
            }

            try
            {
                DropDownList_ENTIDAD_EPS.SelectedValue = GridView_EPS.DataKeys[filaSeleccionada].Values["ID_EPS"].ToString();
            }
            catch
            {
                DropDownList_ENTIDAD_EPS.ClearSelection();
            }
            TextBox_OBS_EPS.Text = GridView_ARP.Rows[filaSeleccionada].Cells[8].Text;
        }
    }
    
    protected void Button_ACTUALIZAR_AFILIACION_EPS_Click(object sender, EventArgs e)
    {
        Decimal ID_CONTRATO = Convert.ToDecimal(HiddenField_ID_CONTRATO.Value);

        tools _tools = new tools();

        Decimal ID_SOLICITUD = Convert.ToDecimal(TextBox_ID_SOLICITUD.Text);
        Decimal ID_REQUERIMIENTO = Convert.ToDecimal(HiddenField_ID_REQUERIMIENTO.Value);
        Decimal ID_EMPLEADO = Convert.ToDecimal(Label_ID_EMPLEADO.Text);

        DateTime FECHA_R;
        DateTime FECHA_RADICACION;
        String OBSERVACIONES;
        Decimal REGISTRO_AFILIACION;

        Decimal ID_ENTIDAD = Convert.ToDecimal(DropDownList_ENTIDAD_EPS.SelectedValue);
        FECHA_R = Convert.ToDateTime(TextBox_FECHA_R_EPS.Text);
        FECHA_RADICACION = Convert.ToDateTime(TextBox_FECHA_RADICACION_EPS.Text);
        OBSERVACIONES = TextBox_OBS_EPS.Text.Trim().ToUpper();

        Boolean ACTUALIZAR_ESTADO_PROCESO = true;
        if (HiddenField_PRESENTACION.Value == Presentacion.ContratosActivos.ToString())
        {
            ACTUALIZAR_ESTADO_PROCESO = false;
        }

        Byte[] ARCHIVO_RADICACION = null;
        Int32 ARCHIVO_RADICACION_TAMANO = 0;
        String ARCHIVO_RADICACION_EXTENSION = null;
        String ARCHIVO_RADICACION_TYPE = null;
        if (FileUpload_ARCHIVO_AFILIACION_EPS.HasFile == true)
        {
            using (BinaryReader reader = new BinaryReader(FileUpload_ARCHIVO_AFILIACION_EPS.PostedFile.InputStream))
            {
                ARCHIVO_RADICACION = reader.ReadBytes(FileUpload_ARCHIVO_AFILIACION_EPS.PostedFile.ContentLength);
                ARCHIVO_RADICACION_TAMANO = FileUpload_ARCHIVO_AFILIACION_EPS.PostedFile.ContentLength;
                ARCHIVO_RADICACION_TYPE = FileUpload_ARCHIVO_AFILIACION_EPS.PostedFile.ContentType;
                ARCHIVO_RADICACION_EXTENSION = _tools.obtenerExtensionArchivo(FileUpload_ARCHIVO_AFILIACION_EPS.PostedFile.FileName);
            }
        }

        afiliacion _afiliacion = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        if (Label_ID_AFILIACION_EPS.Text == "Sin asignar")
        {

           
            REGISTRO_AFILIACION = _afiliacion.AdicionarconafiliacionEpsAuditoria(ID_SOLICITUD, ID_ENTIDAD, FECHA_R, OBSERVACIONES, ID_REQUERIMIENTO, ID_EMPLEADO, ACTUALIZAR_ESTADO_PROCESO, FECHA_RADICACION, ID_CONTRATO, EntidadesAfiliacion.EPS.ToString(), ARCHIVO_RADICACION, ARCHIVO_RADICACION_TAMANO, ARCHIVO_RADICACION_EXTENSION, ARCHIVO_RADICACION_TYPE);

            if (REGISTRO_AFILIACION == 0)
            {
                Informar(Panel_MENSAJES, Label_MENSAJE, _afiliacion.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_MENSAJES, Label_MENSAJE, "La información de la afilicaión a EPS fue actualizada correctamente.", Proceso.Correcto);
            }

            cargar_eps(REGISTRO_AFILIACION, ID_SOLICITUD, ID_EMPLEADO);

            presentar_interfaz_segun_resultado();
        }
        else
        {

            if ((HiddenField_ENTIDAD_EPS.Value != DropDownList_ENTIDAD_EPS.SelectedValue) || (HiddenField_FECHA_R_EPS.Value != TextBox_FECHA_R_EPS.Text) || (HiddenField_FECHA_RADICACION_EPS.Value != TextBox_FECHA_RADICACION_EPS.Text) || (HiddenField_OBS_EPS.Value != TextBox_OBS_EPS.Text) || (ARCHIVO_RADICACION != null))
            {
                REGISTRO_AFILIACION = _afiliacion.AdicionarconafiliacionEpsAuditoria(ID_SOLICITUD, ID_ENTIDAD, FECHA_R, OBSERVACIONES, ID_REQUERIMIENTO, ID_EMPLEADO, ACTUALIZAR_ESTADO_PROCESO, FECHA_RADICACION, ID_CONTRATO, EntidadesAfiliacion.EPS.ToString(), ARCHIVO_RADICACION, ARCHIVO_RADICACION_TAMANO, ARCHIVO_RADICACION_EXTENSION, ARCHIVO_RADICACION_TYPE);

                if (REGISTRO_AFILIACION == 0)
                {
                    Informar(Panel_MENSAJES, Label_MENSAJE, _afiliacion.MensajeError, Proceso.Error);
                }
                else
                {
                    Informar(Panel_MENSAJES, Label_MENSAJE, "La información de la afilicaión a EPS fue actualizada correctamente.", Proceso.Correcto);
                }
            }
            else
            {
                REGISTRO_AFILIACION = Convert.ToDecimal(Label_ID_AFILIACION_EPS.Text);

                auditoriaContratos _auditoriaContratos = new auditoriaContratos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

               
                Decimal ID_AUDITORIA = _auditoriaContratos.ActualizarAuditoriaContratosPorSeccionYEstadoProceso(ID_EMPLEADO, tabla.CON_AFILIACION_EPS, REGISTRO_AFILIACION, ACTUALIZAR_ESTADO_PROCESO, ID_SOLICITUD, tabla.VAR_ESTADO_PROCESO_CON_AFILIACION_EPS);

                if (ID_AUDITORIA == 0)
                {
                    Informar(Panel_MENSAJES, Label_MENSAJE, _auditoriaContratos.MensajeError, Proceso.Error);
                }
                else
                {
                    Informar(Panel_MENSAJES, Label_MENSAJE, "La información de la afilicaión a EPS fue actualizada correctamente (solo auditoría).", Proceso.Correcto);
                }
            }

            cargar_eps(REGISTRO_AFILIACION, ID_SOLICITUD, ID_EMPLEADO);

            presentar_interfaz_segun_resultado();
        }
    }

  
    protected void GridView_CAJA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int filaSeleccionada = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "copiar")
        {
            try
            {
                TextBox_FECHA_R_CAJA.Text = Convert.ToDateTime(GridView_CAJA.Rows[filaSeleccionada].Cells[5].Text).ToShortDateString();
            }
            catch
            {
                TextBox_FECHA_R_CAJA.Text = "";
            }

            try
            {
                TextBox_FECHA_RADICACION_CAJA.Text = Convert.ToDateTime(GridView_CAJA.Rows[filaSeleccionada].Cells[6].Text).ToShortDateString();
            }
            catch
            {
                TextBox_FECHA_RADICACION_CAJA.Text = "";
            }

            try
            {
                DropDownList_ENTIDAD_Caja.SelectedValue = GridView_CAJA.DataKeys[filaSeleccionada].Values["ID_CAJA_C"].ToString();
            }
            catch
            {
                DropDownList_ENTIDAD_Caja.ClearSelection();
            }

            TextBox_OBS_CAJA.Text = GridView_CAJA.Rows[filaSeleccionada].Cells[8].Text;
        }
    }

   
    protected void Button_ACTUALIZAR_AFILIACION_CAJA_C_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();

        Decimal ID_CONTRATO = Convert.ToDecimal(HiddenField_ID_CONTRATO.Value);

        Decimal ID_SOLICITUD = Convert.ToDecimal(TextBox_ID_SOLICITUD.Text);
        Decimal ID_REQUERIMIENTO = Convert.ToDecimal(HiddenField_ID_REQUERIMIENTO.Value);
        Decimal ID_EMPLEADO = Convert.ToDecimal(Label_ID_EMPLEADO.Text);

        DateTime FECHA_R;
        DateTime FECHA_RADICACION;
        String OBSERVACIONES;
        Decimal REGISTRO_AFILIACION;

        Decimal ID_ENTIDAD = Convert.ToDecimal(DropDownList_ENTIDAD_Caja.SelectedValue);
        FECHA_R = Convert.ToDateTime(TextBox_FECHA_R_CAJA.Text);
        FECHA_RADICACION = Convert.ToDateTime(TextBox_FECHA_RADICACION_CAJA.Text);
        OBSERVACIONES = TextBox_OBS_CAJA.Text.Trim().ToUpper();

        Boolean ACTUALIZAR_ESTADO_PROCESO = true;
        if (HiddenField_PRESENTACION.Value == Presentacion.ContratosActivos.ToString())
        {
            ACTUALIZAR_ESTADO_PROCESO = false;
        }

        Byte[] ARCHIVO_RADICACION = null;
        Int32 ARCHIVO_RADICACION_TAMANO = 0;
        String ARCHIVO_RADICACION_EXTENSION = null;
        String ARCHIVO_RADICACION_TYPE = null;
        if (FileUpload_ARCHIVO_AFILIACION_CAJA.HasFile == true)
        {
            using (BinaryReader reader = new BinaryReader(FileUpload_ARCHIVO_AFILIACION_CAJA.PostedFile.InputStream))
            {
                ARCHIVO_RADICACION = reader.ReadBytes(FileUpload_ARCHIVO_AFILIACION_CAJA.PostedFile.ContentLength);
                ARCHIVO_RADICACION_TAMANO = FileUpload_ARCHIVO_AFILIACION_CAJA.PostedFile.ContentLength;
                ARCHIVO_RADICACION_TYPE = FileUpload_ARCHIVO_AFILIACION_CAJA.PostedFile.ContentType;
                ARCHIVO_RADICACION_EXTENSION = _tools.obtenerExtensionArchivo(FileUpload_ARCHIVO_AFILIACION_CAJA.PostedFile.FileName);
            }
        }

        afiliacion _afiliacion = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        if (Label_ID_AFILIACION_CAJA_C.Text == "Sin asignar")
        {

    
            REGISTRO_AFILIACION = _afiliacion.AdicionarconafiliacionCajasCAuditoria(ID_SOLICITUD, ID_ENTIDAD, FECHA_R, OBSERVACIONES, ID_REQUERIMIENTO, ID_EMPLEADO, ACTUALIZAR_ESTADO_PROCESO, FECHA_RADICACION, ID_CONTRATO, EntidadesAfiliacion.CAJA.ToString(), ARCHIVO_RADICACION, ARCHIVO_RADICACION_TAMANO, ARCHIVO_RADICACION_EXTENSION, ARCHIVO_RADICACION_TYPE, DropDownList_CiudadCajaC.SelectedValue);

            if (REGISTRO_AFILIACION == 0)
            {
                Informar(Panel_MENSAJES, Label_MENSAJE, _afiliacion.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_MENSAJES, Label_MENSAJE, "La información de la afilicaión a caja de c. fue actualizada correctamente.", Proceso.Correcto);
            }

            cargar_caja(REGISTRO_AFILIACION, ID_SOLICITUD, ID_EMPLEADO);

            presentar_interfaz_segun_resultado();
        }
        else
        {

            if ((HiddenField_ENTIDAD_Caja.Value != DropDownList_ENTIDAD_Caja.SelectedValue) || (HiddenField_FECHA_R_CAJA.Value != TextBox_FECHA_R_CAJA.Text) || (HiddenField_FECHA_RADICACION_CAJA.Value != TextBox_FECHA_RADICACION_CAJA.Text)|| (HiddenField_OBS_CAJA.Value != TextBox_OBS_CAJA.Text) || (ARCHIVO_RADICACION != null))
            {
      
                REGISTRO_AFILIACION = _afiliacion.AdicionarconafiliacionCajasCAuditoria(ID_SOLICITUD, ID_ENTIDAD, FECHA_R, OBSERVACIONES, ID_REQUERIMIENTO, ID_EMPLEADO, ACTUALIZAR_ESTADO_PROCESO, FECHA_RADICACION, ID_CONTRATO, EntidadesAfiliacion.CAJA.ToString(), ARCHIVO_RADICACION, ARCHIVO_RADICACION_TAMANO, ARCHIVO_RADICACION_EXTENSION, ARCHIVO_RADICACION_TYPE, DropDownList_CiudadCajaC.SelectedValue);

                if (REGISTRO_AFILIACION == 0)
                {
                    Informar(Panel_MENSAJES, Label_MENSAJE, _afiliacion.MensajeError, Proceso.Error);
                }
                else
                {
                    Informar(Panel_MENSAJES, Label_MENSAJE, "La información de la afilicaión a caja de c. fue actualizada correctamente.", Proceso.Correcto);
                }
            }
            else
            {
                REGISTRO_AFILIACION = Convert.ToDecimal(Label_ID_AFILIACION_CAJA_C.Text);

                auditoriaContratos _auditoriaContratos = new auditoriaContratos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

      
                Decimal ID_AUDITORIA = _auditoriaContratos.ActualizarAuditoriaContratosPorSeccionYEstadoProceso(ID_EMPLEADO, tabla.CON_AFILIACION_CAJAS_C, REGISTRO_AFILIACION, ACTUALIZAR_ESTADO_PROCESO, ID_SOLICITUD, tabla.VAR_ESTADO_PROCESO_CON_AFILIACION_CCF);

                if (ID_AUDITORIA == 0)
                {
                    Informar(Panel_MENSAJES, Label_MENSAJE, _auditoriaContratos.MensajeError, Proceso.Error);
                }
                else
                {
                    Informar(Panel_MENSAJES, Label_MENSAJE, "La información de la afilicaión a caja de c. fue actualizada correctamente (solo auditoría).", Proceso.Correcto);
                }
            }

            cargar_caja(REGISTRO_AFILIACION, ID_SOLICITUD, ID_EMPLEADO);

            presentar_interfaz_segun_resultado();
        }
    }
    
    protected void GridView_AFP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int filaSeleccionada = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "copiar")
        {
            try
            {
                TextBox_FECHA_R_AFP.Text = Convert.ToDateTime(GridView_AFP.Rows[filaSeleccionada].Cells[5].Text).ToShortDateString();
            }
            catch
            {
                TextBox_FECHA_R_AFP.Text = "";
            }

            try
            {
                TextBox_FECHA_RADICACION_AFP.Text = Convert.ToDateTime(GridView_AFP.Rows[filaSeleccionada].Cells[6].Text).ToShortDateString();
            }
            catch
            {
                TextBox_FECHA_RADICACION_AFP.Text = "";
            }

            try
            {
                DropDownList_AFP.SelectedValue = GridView_AFP.DataKeys[filaSeleccionada].Values["ID_F_PENSIONES"].ToString();
            }
            catch
            {
                DropDownList_AFP.ClearSelection();
            }
            TextBox_OBS_AFP.Text = GridView_AFP.Rows[filaSeleccionada].Cells[8].Text;
        }
    }
    
    protected void Button_ACTUALIZAR_AFILIACION_F_PENSIONES_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();

        Decimal ID_SOLICITUD = Convert.ToDecimal(TextBox_ID_SOLICITUD.Text);
        Decimal ID_REQUERIMIENTO = Convert.ToDecimal(HiddenField_ID_REQUERIMIENTO.Value);
        Decimal ID_EMPLEADO = Convert.ToDecimal(Label_ID_EMPLEADO.Text);
        Decimal ID_CONTRATO = Convert.ToDecimal(HiddenField_ID_CONTRATO.Value);

        DateTime FECHA_R;
        DateTime FECHA_RADICACION;
        String PENSIONADO = "N";
        String TIPO_PENSIONADO = null;
        String NUMERO_RESOLUCIOON_TRAMITE = null;
        String OBSERVACIONES;
        Decimal REGISTRO_AFILIACION;

        Decimal ID_ENTIDAD = Convert.ToDecimal(DropDownList_AFP.SelectedValue);
        FECHA_R = Convert.ToDateTime(TextBox_FECHA_R_AFP.Text);
        FECHA_RADICACION = Convert.ToDateTime(TextBox_FECHA_RADICACION_AFP.Text);

        if (DropDownList_pensionado.SelectedValue == "S")
        {
            PENSIONADO = "S";
            TIPO_PENSIONADO = DropDownList_tipo_pensionado.SelectedValue;
            NUMERO_RESOLUCIOON_TRAMITE = TextBox_Numero_resolucion_tramite.Text;
        }

        OBSERVACIONES = TextBox_OBS_AFP.Text.Trim().ToUpper();

        Boolean ACTUALIZAR_ESTADO_PROCESO = true;
        if (HiddenField_PRESENTACION.Value == Presentacion.ContratosActivos.ToString())
        {
            ACTUALIZAR_ESTADO_PROCESO = false;
        }



        Byte[] ARCHIVO_RADICACION = null;
        Int32 ARCHIVO_RADICACION_TAMANO = 0;
        String ARCHIVO_RADICACION_EXTENSION = null;
        String ARCHIVO_RADICACION_TYPE = null;
        if (FileUpload_ARCHIVO_AFILIACION_AFP.HasFile == true)
        {
            using (BinaryReader reader = new BinaryReader(FileUpload_ARCHIVO_AFILIACION_AFP.PostedFile.InputStream))
            {
                ARCHIVO_RADICACION = reader.ReadBytes(FileUpload_ARCHIVO_AFILIACION_AFP.PostedFile.ContentLength);
                ARCHIVO_RADICACION_TAMANO = FileUpload_ARCHIVO_AFILIACION_AFP.PostedFile.ContentLength;
                ARCHIVO_RADICACION_TYPE = FileUpload_ARCHIVO_AFILIACION_AFP.PostedFile.ContentType;
                ARCHIVO_RADICACION_EXTENSION = _tools.obtenerExtensionArchivo(FileUpload_ARCHIVO_AFILIACION_AFP.PostedFile.FileName);
            }
        }



        afiliacion _afiliacion = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        if (Label_ID_AFILIACION_F_PENSIONES.Text == "Sin asignar")
        {


            REGISTRO_AFILIACION = _afiliacion.AdicionarconafiliacionfpensionesAuditoria(ID_SOLICITUD, ID_ENTIDAD, FECHA_R, OBSERVACIONES, PENSIONADO, ID_REQUERIMIENTO, TIPO_PENSIONADO, NUMERO_RESOLUCIOON_TRAMITE, ID_EMPLEADO, ACTUALIZAR_ESTADO_PROCESO, FECHA_RADICACION, ID_CONTRATO, EntidadesAfiliacion.AFP.ToString(), ARCHIVO_RADICACION, ARCHIVO_RADICACION_TAMANO, ARCHIVO_RADICACION_EXTENSION, ARCHIVO_RADICACION_TYPE);

            if (REGISTRO_AFILIACION == 0)
            {
                Informar(Panel_MENSAJES, Label_MENSAJE, _afiliacion.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_MENSAJES, Label_MENSAJE, "La información de la afilicaión a fondo de p. fue actualizada correctamente.", Proceso.Correcto);
            }

            cargar_afp(REGISTRO_AFILIACION, ID_SOLICITUD, ID_EMPLEADO);

            presentar_interfaz_segun_resultado();
        }
        else
        {

            if ((HiddenField_ENTIDAD_AFP.Value != DropDownList_AFP.SelectedValue) || (HiddenField_FECHA_R_AFP.Value != TextBox_FECHA_R_AFP.Text) || (HiddenField_FECHA_RADICACION_AFP.Value != TextBox_FECHA_RADICACION_AFP.Text) || (HiddenField_OBS_AFP.Value != TextBox_OBS_AFP.Text) || (DropDownList_pensionado.SelectedValue != HiddenField_pensionado.Value) || (DropDownList_tipo_pensionado.SelectedValue != HiddenField_tipo_pensionado.Value) || (TextBox_Numero_resolucion_tramite.Text != HiddenField_resolucion_tramite.Value) || (ARCHIVO_RADICACION != null))
            {
        
                REGISTRO_AFILIACION = _afiliacion.AdicionarconafiliacionfpensionesAuditoria(ID_SOLICITUD, ID_ENTIDAD, FECHA_R, OBSERVACIONES, PENSIONADO, ID_REQUERIMIENTO, TIPO_PENSIONADO, NUMERO_RESOLUCIOON_TRAMITE, ID_EMPLEADO, ACTUALIZAR_ESTADO_PROCESO, FECHA_RADICACION, ID_CONTRATO, EntidadesAfiliacion.AFP.ToString(), ARCHIVO_RADICACION,ARCHIVO_RADICACION_TAMANO, ARCHIVO_RADICACION_EXTENSION, ARCHIVO_RADICACION_TYPE);

                if (REGISTRO_AFILIACION == 0)
                {
                    Informar(Panel_MENSAJES, Label_MENSAJE, _afiliacion.MensajeError, Proceso.Error);
                }
                else
                {
                    Informar(Panel_MENSAJES, Label_MENSAJE, "La información de la afilicaión a fondo de p. fue actualizada correctamente.", Proceso.Correcto);
                }
            }
            else
            {
                REGISTRO_AFILIACION = Convert.ToDecimal(Label_ID_AFILIACION_F_PENSIONES.Text);

                auditoriaContratos _auditoriaContratos = new auditoriaContratos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            
                Decimal ID_AUDITORIA = _auditoriaContratos.ActualizarAuditoriaContratosPorSeccionYEstadoProceso(ID_EMPLEADO, tabla.CON_AFILIACION_F_PENSIONES, REGISTRO_AFILIACION, ACTUALIZAR_ESTADO_PROCESO, ID_SOLICITUD, tabla.VAR_ESTADO_PROCESO_CON_AFILIACION_FONDO);

                if (ID_AUDITORIA == 0)
                {
                    Informar(Panel_MENSAJES, Label_MENSAJE, _auditoriaContratos.MensajeError, Proceso.Error);
                }
                else
                {
                    Informar(Panel_MENSAJES, Label_MENSAJE, "La información de la afilicaión a fondo de p. fue actualizada correctamente (solo auditoría).", Proceso.Correcto);
                }
            }

            cargar_afp(REGISTRO_AFILIACION, ID_SOLICITUD, ID_EMPLEADO);

            presentar_interfaz_segun_resultado();
        }
    }
    
    protected void DropDownList_pensionado_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_pensionado.SelectedValue == "S")
        {
            cargar_DropDownList_tipo_Pensionado();
            panel_TIPO_PENSIONADO.Visible = true;
            TextBox_Numero_resolucion_tramite.Text = "";
        }
        else
        {
            panel_TIPO_PENSIONADO.Visible = false;
            TextBox_Numero_resolucion_tramite.Text = "";
        }
    }
    
    protected void Button_CANCELAR_INGRESO_CLAUSULA_Click(object sender, EventArgs e)
    {

    }
    
    protected void Button_PREVISUALIZAR_CLAUSULA_Click(object sender, EventArgs e)
    {

    }
    
    protected void Button_INGRESAR_CLAUSULA_NUEVA_Click(object sender, EventArgs e)
    {

    }

    private void cargar_check_periodos(String periodos)
    {
        if (periodos == "1")
        {
            CheckBox_PER_1.Visible = true;
            CheckBox_PER_2.Visible = false;
            CheckBox_PER_3.Visible = false;
            CheckBox_PER_4.Visible = false;
        }
        else
        {
            if (periodos == "2")
            {
                CheckBox_PER_1.Visible = true;
                CheckBox_PER_2.Visible = true;
                CheckBox_PER_3.Visible = false;
                CheckBox_PER_4.Visible = false;
            }
            else
            {
                if (periodos == "0")
                {
                    CheckBox_PER_1.Visible = false;
                    CheckBox_PER_2.Visible = false;
                    CheckBox_PER_3.Visible = false;
                    CheckBox_PER_4.Visible = false;
                }
                else
                {
                    if (periodos == "4")
                    {
                        CheckBox_PER_1.Visible = true;
                        CheckBox_PER_2.Visible = true;
                        CheckBox_PER_3.Visible = true;
                        CheckBox_PER_4.Visible = false;
                    }
                    else
                    {
                        CheckBox_PER_1.Visible = true;
                        CheckBox_PER_2.Visible = true;
                        CheckBox_PER_3.Visible = true;
                        CheckBox_PER_4.Visible = true;
                    }
                }
            }
        }

        CheckBox_PER_1.Checked = false;
        CheckBox_PER_2.Checked = false;
        CheckBox_PER_3.Checked = false;
        CheckBox_PER_4.Checked = false;
    }
    
    private void cargar_DropDownList_CONCEPTOS_FIJOS()
    {
        DropDownList_CONCEPTOS_FIJOS.Items.Clear();
        conceptosNomina _conceptosNomina = new conceptosNomina(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaConceptos = _conceptosNomina.ObtenerConceptosFijos();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione", "");
        DropDownList_CONCEPTOS_FIJOS.Items.Add(item);

        foreach (DataRow fila in tablaConceptos.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOMBRE"].ToString(), fila["CODIGO"].ToString());
            DropDownList_CONCEPTOS_FIJOS.Items.Add(item);
        }

        DropDownList_CONCEPTOS_FIJOS.DataBind();
    }
    
    private void Cargar_nuevo_concepto(Decimal idClausula)
    {
        Label_ID_CLAUSULA_RELACIONADA.Text = idClausula.ToString();

        Decimal ID_REQUERIMIENTO = Convert.ToDecimal(HiddenField_ID_REQUERIMIENTO.Value);
        Decimal ID_SOLICITUD = Convert.ToDecimal(TextBox_ID_SOLICITUD.Text);
        Decimal ID_EMPLEADO = Convert.ToDecimal(Label_ID_EMPLEADO.Text);

        registroContrato _registroContrato = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoContrato = _registroContrato.obtenerInfoNomEmpleadoPorIdSolicitudIdRequerimiento(ID_SOLICITUD, ID_REQUERIMIENTO, ID_EMPLEADO);
        DataRow filaInfoContrato = tablaInfoContrato.Rows[0];
        cargar_DropDownList_PERIODO_PAGO(DropDownList_FORMA_PAGO_NOMINA);
        DropDownList_FORMA_PAGO_NOMINA.SelectedValue = filaInfoContrato["PERIODO_PAGO"].ToString().Trim();

        cargar_check_periodos(filaInfoContrato["PERIODO_PAGO"].ToString().Trim());

        cargar_DropDownList_CONCEPTOS_FIJOS();
        TextBox_CAN_PRE.Text = "";
        TextBox_VAL_PRE.Text = "";

    }
    
    protected void GridView_LISTA_CLAUSULAS_PARAMETRIZADAS_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int fila = Convert.ToInt32(e.CommandArgument);

        Decimal id_clausula;

        id_clausula = Convert.ToDecimal(GridView_LISTA_CLAUSULAS_PARAMETRIZADAS.DataKeys[fila].Values["ID_CLAUSULA"]); ;

        if (e.CommandName == "seleccionar")
        {
            for (int i = 0; i < GridView_LISTA_CLAUSULAS_PARAMETRIZADAS.Rows.Count; i++)
            {
                GridView_LISTA_CLAUSULAS_PARAMETRIZADAS.Rows[i].BackColor = System.Drawing.Color.Transparent;
            }
            GridView_LISTA_CLAUSULAS_PARAMETRIZADAS.Rows[fila].BackColor = colorSeleccionado;
            Ocultar(Acciones.nuevoConcepto);
            Mostrar(Acciones.nuevoConcepto);
            Cargar_nuevo_concepto(id_clausula);
        }
        Button_ADICIONAR_CONCEPTO_FIJO.Focus();
    }
    
    private DataTable obtenerDataTableDe_GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS()
    {
        DataTable tablaResultado = new DataTable();
        tablaResultado.Columns.Add("REGISTRO");
        tablaResultado.Columns.Add("ID_EMPLEADO");
        tablaResultado.Columns.Add("ID_CONCEPTO");
        tablaResultado.Columns.Add("DESC_CONCEPTO");
        tablaResultado.Columns.Add("CAN_PRE");
        tablaResultado.Columns.Add("VAL_PRE");
        tablaResultado.Columns.Add("PERIODOS_LIQUIDACION");
        tablaResultado.Columns.Add("ID_CLAUSULA");
        tablaResultado.Columns.Add("LIQ_Q_1");
        tablaResultado.Columns.Add("LIQ_Q_2");
        tablaResultado.Columns.Add("LIQ_Q_3");
        tablaResultado.Columns.Add("LIQ_Q_4");

        DataRow filaTablaResultado;

        Decimal REGISTRO = 0;
        Decimal ID_EMPLEADO = 0;
        Decimal ID_CONCEPTO = 0;
        String DESC_CONCEPTO = null;
        int CAN_PRE = 0;
        Decimal VAL_PRE = 0;
        String PERIODOS_LIQUIDACION = null;
        Decimal  ID_CLAUSULA = 0;
        String LIQ_Q_1 = null;
        String LIQ_Q_2 = null;
        String LIQ_Q_3 = null;
        String LIQ_Q_4 = null;

        GridViewRow filaGrilla;
        for (int i = 0; i < GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.Rows.Count; i++)
        {
            filaGrilla = GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.Rows[i];

            REGISTRO = Convert.ToDecimal(GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.DataKeys[i].Values["REGISTRO"]);

            ID_EMPLEADO = Convert.ToDecimal(GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.DataKeys[i].Values["ID_EMPLEADO"]);

            ID_CONCEPTO = Convert.ToDecimal(GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.DataKeys[i].Values["ID_CONCEPTO"]);

            DESC_CONCEPTO = HttpUtility.HtmlDecode(filaGrilla.Cells[4].Text);

            CAN_PRE = Convert.ToInt32(filaGrilla.Cells[5].Text);

            VAL_PRE = Convert.ToDecimal(filaGrilla.Cells[6].Text); 

            PERIODOS_LIQUIDACION = HttpUtility.HtmlDecode(filaGrilla.Cells[7].Text);

            ID_CLAUSULA = 0;
            try
            {
                ID_CLAUSULA = Convert.ToDecimal(GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.DataKeys[i].Values["ID_CLAUSULA"]);
            }
            catch 
            {
                ID_CLAUSULA = 0;
            }

            LIQ_Q_1 = GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.DataKeys[i].Values["LIQ_Q_1"].ToString();
            LIQ_Q_2 = GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.DataKeys[i].Values["LIQ_Q_2"].ToString();
            LIQ_Q_3 = GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.DataKeys[i].Values["LIQ_Q_3"].ToString();
            LIQ_Q_4 = GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.DataKeys[i].Values["LIQ_Q_4"].ToString();

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["REGISTRO"] = REGISTRO;
            filaTablaResultado["ID_EMPLEADO"] = ID_EMPLEADO;
            filaTablaResultado["ID_CONCEPTO"] = ID_CONCEPTO;
            filaTablaResultado["DESC_CONCEPTO"] = DESC_CONCEPTO;
            filaTablaResultado["CAN_PRE"] = CAN_PRE;
            filaTablaResultado["VAL_PRE"] = String.Format("{0:N0}", VAL_PRE);
            filaTablaResultado["PERIODOS_LIQUIDACION"] = PERIODOS_LIQUIDACION;
            filaTablaResultado["ID_CLAUSULA"] = ID_CLAUSULA;
            filaTablaResultado["LIQ_Q_1"] = LIQ_Q_1;
            filaTablaResultado["LIQ_Q_2"] = LIQ_Q_2;
            filaTablaResultado["LIQ_Q_3"] = LIQ_Q_3;
            filaTablaResultado["LIQ_Q_4"] = LIQ_Q_4;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }
    
    private void sin_fila_seleccionada_en_grilla(GridView grilla)
    {
        foreach (GridViewRow fila in grilla.Rows)
        {
            fila.BackColor = System.Drawing.Color.Transparent;
        }
    }
    
    protected void Button_ADICIONAR_CONCEPTO_FIJO_Click(object sender, EventArgs e)
    {
        if ((CheckBox_PER_1.Checked == true) || (CheckBox_PER_2.Checked == true) || (CheckBox_PER_3.Checked == true) || (CheckBox_PER_4.Checked == true))
        {
            Ocultar(Acciones.nuevoConcepto);
            Ocultar(Acciones.conceptosIncluidos);

            DataTable tablaDatosActualesGrilla = obtenerDataTableDe_GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS();
            Boolean verificador = true;

            Decimal ID_CONCEPTO = Convert.ToDecimal(DropDownList_CONCEPTOS_FIJOS.SelectedValue);
            Decimal ID_EMPLEADO = Convert.ToDecimal(Label_ID_EMPLEADO.Text);
            Decimal ID_CLAUSULA = Convert.ToDecimal(Label_ID_CLAUSULA_RELACIONADA.Text);

            foreach (DataRow fila in tablaDatosActualesGrilla.Rows)
            {
                if (Convert.ToDecimal(fila["ID_CONCEPTO"]) == ID_CONCEPTO)
                {
                    verificador = false;
                    break;
                }
            }

            if (verificador == true)
            {
                DataRow filaNueva = tablaDatosActualesGrilla.NewRow();

                filaNueva["REGISTRO"] = 0;
                filaNueva["ID_EMPLEADO"] = ID_EMPLEADO;
                filaNueva["ID_CONCEPTO"] = ID_CONCEPTO;
                filaNueva["DESC_CONCEPTO"] = DropDownList_CONCEPTOS_FIJOS.SelectedItem.Text;
                filaNueva["CAN_PRE"] = Convert.ToInt32(TextBox_CAN_PRE.Text);
                filaNueva["VAL_PRE"] = Convert.ToDecimal(TextBox_VAL_PRE.Text);

                filaNueva["LIQ_Q_1"] = "N";
                filaNueva["LIQ_Q_2"] = "N";
                filaNueva["LIQ_Q_3"] = "N";
                filaNueva["LIQ_Q_4"] = "N";

                String PERIODOS_LIQUIDACION = "";
                int contador_periodos = 0;

                if (CheckBox_PER_1.Checked == true)
                {
                    PERIODOS_LIQUIDACION = "1";
                    contador_periodos += 1;
                    filaNueva["LIQ_Q_1"] = "S";
                }

                if (CheckBox_PER_2.Checked == true)
                {
                    if (contador_periodos > 0)
                    {
                        PERIODOS_LIQUIDACION += ", 2";
                    }
                    else
                    {
                        PERIODOS_LIQUIDACION += "2";
                    }
                    contador_periodos += 1;
                    filaNueva["LIQ_Q_2"] = "S";
                }

                if (CheckBox_PER_3.Checked == true)
                {
                    if (contador_periodos > 0)
                    {
                        PERIODOS_LIQUIDACION += ", 3";
                    }
                    else
                    {
                        PERIODOS_LIQUIDACION += "3";
                    }
                    contador_periodos += 1;
                    filaNueva["LIQ_Q_3"] = "S";
                }

                if (CheckBox_PER_4.Checked == true)
                {
                    if (contador_periodos > 0)
                    {
                        PERIODOS_LIQUIDACION += ", 4";
                    }
                    else
                    {
                        PERIODOS_LIQUIDACION += "4";
                    }
                    contador_periodos += 1;
                    filaNueva["LIQ_Q_4"] = "S";
                }

                filaNueva["PERIODOS_LIQUIDACION"] = PERIODOS_LIQUIDACION;
                filaNueva["ID_CLAUSULA"] = ID_CLAUSULA;

                tablaDatosActualesGrilla.Rows.Add(filaNueva);

                GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.DataSource = tablaDatosActualesGrilla;
                GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.DataBind();

                Informar(Panel_MENSAJES_CONCEPTOS_FIJOS, Label_MENSAJES_CONCEPTOS_FIJOS, "El concepto fijo " + DropDownList_CONCEPTOS_FIJOS.SelectedItem.Text + " fue ingresado correctamente.", Proceso.Correcto);

                Button_AdicionarClausula.Focus();
            }
            else
            {
                Informar(Panel_MENSAJES_CONCEPTOS_FIJOS, Label_MENSAJES_CONCEPTOS_FIJOS, "El concepto fijo " + DropDownList_CONCEPTOS_FIJOS.SelectedItem.Text + " ya hace parte de la lista de conceptos incluidos, no se puede repetir.", Proceso.Error);
                Button_AdicionarClausula.Focus();
            }

            if (GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.Rows.Count > 0)
            {
                Mostrar(Acciones.conceptosIncluidos);
            }

            sin_fila_seleccionada_en_grilla(GridView_LISTA_CLAUSULAS_PARAMETRIZADAS);

            Button_ACTUALZIAR_CONCEPTOS_FIJOS.Visible = true;
            Button_ACTUALZIAR_CONCEPTOS_FIJOS.Focus();
        }
        else
        {
            Informar(Panel_MENSAJES_CONCEPTOS_FIJOS, Label_MENSAJES_CONCEPTOS_FIJOS, "Debe selecionar por lo menos un périodo en el que se debe liquidar el concepto fijo", Proceso.Error);
        }
    }
    
    protected void GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable tablaConceptosGrilla = obtenerDataTableDe_GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS();

        int filaSeleccionada = Convert.ToInt32(e.CommandArgument);

        tablaConceptosGrilla.Rows.RemoveAt(filaSeleccionada);

        GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.DataSource = tablaConceptosGrilla;
        GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.DataBind();

        if (GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.Rows.Count > 0)
        {
            Mostrar(Acciones.conceptosIncluidos);
        }
        else
        {
            Ocultar(Acciones.conceptosIncluidos);
        }
    }
    
    protected void Button_CANCELAR_ADICION_CONCEPTO_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.nuevoConcepto);

        sin_fila_seleccionada_en_grilla(GridView_LISTA_CLAUSULAS_PARAMETRIZADAS);

        Button_ACTUALZIAR_CONCEPTOS_FIJOS.Visible = true;
    }
    
    protected void Button_ACTUALZIAR_CONCEPTOS_FIJOS_Click(object sender, EventArgs e)
    {
        if (Panel_NUEVO_CONCEPTO_FIJO.Visible == true)
        {
            Informar(Panel_MENSAJES, Label_MENSAJE, "La sección 'NUEVO CONCEPTO FIJO' esta abierta, por favor termine la creación o cancelela para porder continuar.", Proceso.Error);
        }
        else
        { 
            Decimal ID_SOLICITUD = Convert.ToDecimal(TextBox_ID_SOLICITUD.Text);
            Decimal ID_REQUERIMIENTO = Convert.ToDecimal(HiddenField_ID_REQUERIMIENTO.Value);
            Decimal ID_EMPLEADO = Convert.ToDecimal(Label_ID_EMPLEADO.Text);

            Boolean ACTUALIZAR_ESTADO_PROCESO = true;
            if (HiddenField_PRESENTACION.Value == Presentacion.ContratosActivos.ToString())
            {
                ACTUALIZAR_ESTADO_PROCESO = false;
            }

            Decimal verificador = 0;

            List<ConceptosNominaEmpleado> listaConceptos = new List<ConceptosNominaEmpleado>();
            ConceptosNominaEmpleado _ConceptosNominaEmpleadoParaLista;

            for (int i = 0; i < GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.Rows.Count; i++)
            {
                _ConceptosNominaEmpleadoParaLista = new ConceptosNominaEmpleado(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

                _ConceptosNominaEmpleadoParaLista.CAN_PRE = Convert.ToInt32(GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.Rows[i].Cells[5].Text);
                try
                {
                    _ConceptosNominaEmpleadoParaLista.ID_CLAUSULA = Convert.ToDecimal(GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.DataKeys[i].Values["ID_CLAUSULA"]);
                }
                catch
                {
                    _ConceptosNominaEmpleadoParaLista.ID_CLAUSULA = 0;
                }

                _ConceptosNominaEmpleadoParaLista.ID_CONCEPTO = Convert.ToDecimal(GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.DataKeys[i].Values["ID_CONCEPTO"]);
                _ConceptosNominaEmpleadoParaLista.LIQ_Q_1 = GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.DataKeys[i].Values["LIQ_Q_1"].ToString();
                _ConceptosNominaEmpleadoParaLista.LIQ_Q_2 = GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.DataKeys[i].Values["LIQ_Q_2"].ToString();
                _ConceptosNominaEmpleadoParaLista.LIQ_Q_3 = GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.DataKeys[i].Values["LIQ_Q_3"].ToString();
                _ConceptosNominaEmpleadoParaLista.LIQ_Q_4 = GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.DataKeys[i].Values["LIQ_Q_4"].ToString();
                _ConceptosNominaEmpleadoParaLista.REGISTRO = Convert.ToDecimal(GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.DataKeys[i].Values["REGISTRO"]);
                _ConceptosNominaEmpleadoParaLista.VAL_PRE = Convert.ToDecimal(GridView_CONCEPTOS_FIJOS_PARAMETRIZADOS.Rows[i].Cells[6].Text);

                listaConceptos.Add(_ConceptosNominaEmpleadoParaLista);
            }

            ConceptosNominaEmpleado _ConceptosNominaEmpleado = new ConceptosNominaEmpleado(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            verificador = _ConceptosNominaEmpleado.ActualizarConceptosFijosEmpleado(ID_EMPLEADO, listaConceptos, ACTUALIZAR_ESTADO_PROCESO, ID_SOLICITUD);
            if (verificador == 0)
            {
                Informar(Panel_MENSAJES, Label_MENSAJE, _ConceptosNominaEmpleado.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_MENSAJES, Label_MENSAJE, "La auditoría a la sección de conceptos fijos fue realizada correctamente.", Proceso.Error);

                Decimal ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);

                cargar_seccion_conceptos_fijos(ID_PERFIL, ID_EMPLEADO);

                presentar_interfaz_segun_resultado();
            }
        }
    }
    
    protected void DropDownList_CARGO_TRABAJADOR_SelectedIndexChanged(object sender, EventArgs e)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        if (DropDownList_CARGO_TRABAJADOR.SelectedIndex <= 0)
        {
            HiddenField_ID_PERFIL.Value = "";
            inhabilitar_DropDownList_CIUDAD_TRABAJADOR();
            inhabilitar_DropDownList_CENTRO_COSTO();
            inhabilitar_DropDownList_SUB_CENTRO();
        }
        else
        {
            HiddenField_ID_PERFIL.Value = DropDownList_CARGO_TRABAJADOR.SelectedValue;
            cargar_DropDownList_CIUDAD(ID_EMPRESA);
            DropDownList_CIUDAD_TRABAJADOR.Enabled = true;
            inhabilitar_DropDownList_CENTRO_COSTO();
            inhabilitar_DropDownList_SUB_CENTRO();
        }
        
        colorear_indicadores_de_ubicacion(false, false, false, System.Drawing.Color.Transparent);
    }
    
    private void ColorearRiesgoInicial()
    {
        String RIESGO_INICIAL = HiddenField_RIESGO_INICIAL.Value;
        String RIESGO_CONFIGURADO = DropDownList_RIESGO_EMPLEADO.SelectedValue;

        if (RIESGO_INICIAL == RIESGO_CONFIGURADO)
        {
            if (String.IsNullOrEmpty(RIESGO_INICIAL) == true)
            {
                Label_RIESGO_INICIAL.Text = "Riesgo Inicial: Desconocido";
                Label_RIESGO_INICIAL.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                Label_RIESGO_INICIAL.Text = "Riesgo Inicial: " + RIESGO_INICIAL;
                Label_RIESGO_INICIAL.ForeColor = System.Drawing.Color.Green;
            }
        }
        else
        {
            Label_RIESGO_INICIAL.Text = "Riesgo Inicial: " + RIESGO_INICIAL;
            Label_RIESGO_INICIAL.ForeColor = System.Drawing.Color.Red;
        }
    }
    
    protected void DropDownList_CIUDAD_TRABAJADOR_SelectedIndexChanged(object sender, EventArgs e)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        cargar_DropDownList_RIESGO_EMPLEADO(ID_EMPRESA);

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

            Decimal ID_PERFIL = Convert.ToDecimal(DropDownList_CARGO_TRABAJADOR.SelectedValue);
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

                DataRow filaInfoCondicion = tablaCondicionContratacion.Rows[0];
                try
                {
                    DropDownList_RIESGO_EMPLEADO.SelectedValue = Convert.ToDecimal(filaInfoCondicion["RIESGO"]).ToString();
                }
                catch
                {
                    DropDownList_RIESGO_EMPLEADO.SelectedIndex = 0;
                }
            }

            cargar_DropDownList_CENTRO_COSTO(ID_EMPRESA, ID_CIUDAD);
            DropDownList_CC_TRABAJADOR.Enabled = true;
            inhabilitar_DropDownList_SUB_CENTRO();
        }

        ColorearRiesgoInicial();
    }
    
    protected void DropDownList_CC_TRABAJADOR_SelectedIndexChanged(object sender, EventArgs e)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        Decimal ID_PERFIL = Convert.ToDecimal(DropDownList_CARGO_TRABAJADOR.SelectedValue);
        String ID_CIUDAD = DropDownList_CIUDAD_TRABAJADOR.SelectedValue;
        condicionesContratacion _condicionesContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        cargar_DropDownList_RIESGO_EMPLEADO(ID_EMPRESA);

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

                DataRow filaInfoCondicion = tablaCondicionContratacion.Rows[0];
                try
                {
                    DropDownList_RIESGO_EMPLEADO.SelectedValue = Convert.ToDecimal(filaInfoCondicion["RIESGO"]).ToString();
                }
                catch
                {
                    DropDownList_RIESGO_EMPLEADO.SelectedIndex = 0;
                }
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

                DataRow filaInfoCondicion = tablaCondicionContratacion.Rows[0];
                try
                {
                    DropDownList_RIESGO_EMPLEADO.SelectedValue = Convert.ToDecimal(filaInfoCondicion["RIESGO"]).ToString();
                }
                catch
                {
                    DropDownList_RIESGO_EMPLEADO.SelectedIndex = 0;
                }
            }

            cargar_DropDownList_SUB_CENTRO(ID_EMPRESA, ID_CENTRO_C);
            DropDownList_SUB_CENTRO_TRABAJADOR.Enabled = true;
        }


        ColorearRiesgoInicial();
    }
    
    protected void DropDownList_SUB_CENTRO_TRABAJADOR_SelectedIndexChanged(object sender, EventArgs e)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        Decimal ID_PERFIL = Convert.ToDecimal(DropDownList_CARGO_TRABAJADOR.SelectedValue);
        String ID_CIUDAD = DropDownList_CIUDAD_TRABAJADOR.SelectedValue;
        Decimal ID_CENTRO_C = Convert.ToDecimal(DropDownList_CC_TRABAJADOR.SelectedValue);

        cargar_DropDownList_RIESGO_EMPLEADO(ID_EMPRESA);

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

                DataRow filaInfoCondicion = tablaCondicionContratacion.Rows[0];
                try
                {
                    DropDownList_RIESGO_EMPLEADO.SelectedValue = Convert.ToDecimal(filaInfoCondicion["RIESGO"]).ToString();
                }
                catch
                {
                    DropDownList_RIESGO_EMPLEADO.SelectedIndex = 0;
                }
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

                DataRow filaInfoCondicion = tablaCondicionContratacion.Rows[0];
                try
                {
                    DropDownList_RIESGO_EMPLEADO.SelectedValue = Convert.ToDecimal(filaInfoCondicion["RIESGO"]).ToString();
                }
                catch
                {
                    DropDownList_RIESGO_EMPLEADO.SelectedIndex = 0;
                }

                DropDownList_RIESGO_EMPLEADO.Enabled = true;
            }
        }

        ColorearRiesgoInicial();
    }

    private void ProcederConActualizacion(Boolean cargarInfoContrato)
    {
        Decimal ID_CONTRATO = Convert.ToDecimal(HiddenField_ID_CONTRATO.Value);
        Decimal ID_SOLICITUD = Convert.ToDecimal(TextBox_ID_SOLICITUD.Text);
        Decimal ID_EMPLEADO = Convert.ToDecimal(Label_ID_EMPLEADO.Text);
        Decimal ID_PERFIL = Convert.ToDecimal(DropDownList_CARGO_TRABAJADOR.SelectedValue);
        Decimal RIESGO = Convert.ToDecimal(DropDownList_RIESGO_EMPLEADO.SelectedValue);
        Decimal ID_SUB_C = 0;
        Decimal ID_CENTRO_C = 0;
        String ID_CIUDAD = null;

        if (DropDownList_SUB_CENTRO_TRABAJADOR.SelectedIndex > 0)
        {
            ID_SUB_C = Convert.ToDecimal(DropDownList_SUB_CENTRO_TRABAJADOR.SelectedValue);
        }
        else
        {
            if (DropDownList_CC_TRABAJADOR.SelectedIndex > 0)
            {
                ID_CENTRO_C = Convert.ToDecimal(DropDownList_CC_TRABAJADOR.SelectedValue);
            }
            else
            {
                ID_CIUDAD = DropDownList_CIUDAD_TRABAJADOR.SelectedValue;
            }
        }

        String PAGO_DIAS_PRODUCTIVIDAD = "N";
        Decimal VALOR_NOMINA = 0;
        Decimal VALOR_CONTRATO = 0;
        Decimal SALARIO = 0;

        Boolean continuar = true;
        tools _tools = new tools();

        if (String.IsNullOrEmpty(TextBox_SALARIO.Text) == true)
        {
            if (String.IsNullOrEmpty(TextBox_SALARIO_NOMINA.Text) == true)
            {
                Informar(Panel_MENSAJE_CONTRATO, Label_MENSAJE_CONTRATO, "El Salario es obligatorio digitarlo.", Proceso.Advertencia);
                continuar = false;
            }
            else
            {
                if (_tools.IsNumeric(TextBox_SALARIO_NOMINA.Text) == false)
                {
                    Informar(Panel_MENSAJE_CONTRATO, Label_MENSAJE_CONTRATO, "El Salario digitado no tiene un formato numerico valido.", Proceso.Advertencia);
                    continuar = false;
                }
                else
                {
                    SALARIO = Convert.ToDecimal(TextBox_SALARIO_NOMINA.Text);
                }
            }
        }
        else
        {
            if (_tools.IsNumeric(TextBox_SALARIO.Text) == false)
            {
                Informar(Panel_MENSAJE_CONTRATO, Label_MENSAJE_CONTRATO, "El Salario digitado no tiene un formato numerico valido.", Proceso.Advertencia);
                continuar = false;
            }
            else
            {
                SALARIO = Convert.ToDecimal(TextBox_SALARIO.Text);
            }
        }

        if (continuar == true)
        {
            String SAL_INT = DropDownList_SALARIO_INTEGRAL.SelectedValue;

            String FORMA_PAGO = null;
            Decimal ID_ENTIDAD = 0;
            String TIPO_CUENTA = null;
            String NUM_CUENTA = null;
            String CHEQUE_REG = null;
            String CONTROL_CONTRATO = "CORRECTO";


            if (CheckBox_NoLaboro.Checked == true)
            {
                CONTROL_CONTRATO = "NO LABORO";
            }

            if (CheckBox_DobleRegistro.Checked == true)
            {
                CONTROL_CONTRATO = "NO INGRESO";
            }

            if ((CheckBox_NoLaboro.Checked == true) || (CheckBox_DobleRegistro.Checked == true))
            {
                FORMA_PAGO = string.Empty;
                ID_ENTIDAD = 0;
                TIPO_CUENTA = string.Empty;
                CHEQUE_REG = String.Empty;
            }
            else
            {
                FORMA_PAGO = DropDownList_forma_pago.SelectedValue;

                if ((FORMA_PAGO == "CONSIGNACIÓN BANCARIA") || (FORMA_PAGO == "DISPERSION") || (FORMA_PAGO == "ACH"))
                {
                    if (DropDownList_entidad_bancaria.SelectedIndex > 0)
                    {
                        ID_ENTIDAD = Convert.ToDecimal(DropDownList_entidad_bancaria.SelectedValue);
                    }

                    if (DropDownList_TIPO_CUENTA.SelectedIndex > 0)
                    {
                        TIPO_CUENTA = DropDownList_TIPO_CUENTA.SelectedValue;
                    }

                    if (TextBox_NUMERO_CUENTA.Text.Trim().Length > 0)
                    {
                        NUM_CUENTA = TextBox_NUMERO_CUENTA.Text.Trim();
                    }

                }
                else
                {
                    if (FORMA_PAGO == "CHEQUE")
                    {
                        if (DropDownList_ChequeReg.SelectedIndex > 0)
                        {
                            CHEQUE_REG = DropDownList_ChequeReg.SelectedValue;
                        }
                    }
                }
            }

            String TIPO_CONTRATO = DropDownList_TIPO_CONTRATO.SelectedValue;

            String CLASE_CONTRATO = DropDownList_CLASE_CONTRATO.SelectedValue;

            Boolean ACTUALIZAR_ESTADO_PROCESO = true;
            if (HiddenField_PRESENTACION.Value == Presentacion.ContratosActivos.ToString())
            {
                ACTUALIZAR_ESTADO_PROCESO = false;
            }

            String PERIODO_PAGO = DropDownList_PERIODO_PAGO.SelectedValue;

            registroContrato _registroContrato = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            Boolean verificador = _registroContrato.ActualizarContratoDesdeAuditoria(ID_CONTRATO, ID_EMPLEADO, RIESGO, TIPO_CONTRATO, ID_PERFIL, ID_SUB_C, ID_CENTRO_C, ID_CIUDAD, PAGO_DIAS_PRODUCTIVIDAD, VALOR_NOMINA, VALOR_CONTRATO, SALARIO, SAL_INT, FORMA_PAGO, ID_ENTIDAD, NUM_CUENTA, ID_SOLICITUD, ACTUALIZAR_ESTADO_PROCESO, TIPO_CUENTA, Convert.ToDateTime(TextBox_FECHA_INICIO.Text), Convert.ToDateTime(TextBox_FECHA_FIN.Text), PERIODO_PAGO, CLASE_CONTRATO, CONTROL_CONTRATO, CHEQUE_REG);

            if (verificador == false)
            {
                Informar(Panel_MENSAJES, Label_MENSAJE, _registroContrato.MensajeError, Proceso.Error);
                Informar(Panel_MENSAJE_CONTRATO, Label_MENSAJE_CONTRATO, _registroContrato.MensajeError, Proceso.Error);
            }
            else
            {
                if (cargarInfoContrato == true)
                {
                    Informar(Panel_MENSAJES, Label_MENSAJE, "La información basica del contrato fue actualizada correctamente.", Proceso.Correcto);
                    Informar(Panel_MENSAJE_CONTRATO, Label_MENSAJE_CONTRATO, "La información basica del contrato fue actualizada correctamente.", Proceso.Correcto);

                    cargar_seccion_contratos(ID_EMPLEADO);

                    presentar_interfaz_segun_resultado();
                }
            }
        }
    }

    private void MostrarAviso(String mensaje)
    {
        Panel_FONDO_AVISO.Style.Add("display", "block");
        Panel_AVISO.Style.Add("display", "block");

        Label_AVISO.Font.Bold = true;
        Label_AVISO.ForeColor = System.Drawing.Color.Orange;

        Panel_FONDO_AVISO.Visible = true;
        Panel_AVISO.Visible = true;

        Label_AVISO.Text = mensaje;
    }

    protected void Button_ACTUALZIAR_CONTRATO_Click(object sender, EventArgs e)
    {

        if (CheckBox_NoLaboro.Checked == true || CheckBox_DobleRegistro.Checked == true)
        {
            if (CheckBox_NoLaboro.Checked == true)
            {
                MostrarAviso("Al seleccionar NO LABORÓ el contrato quedará automáticamente con fecha de retiro igual a la fecha de ingreso.<br>Desea realizar los cambios?");
            }
            else
            {
                MostrarAviso("Al seleccionar NO INGRESO el contrato quedará automáticamente INACTIVO, esta decisión no puede ser reversada.<br>Desea realizar los cambios?");
            }
        }
        else
        {
            ProcederConActualizacion(true);
        }
    }
    
    protected void Button_ACTUALIZAR_EXAMENES_Click(object sender, EventArgs e)
    {
        Decimal ID_EMPLEADO = Convert.ToDecimal(Label_ID_EMPLEADO.Text);
        Decimal ID_SOLICITUD = Convert.ToDecimal(TextBox_ID_SOLICITUD.Text);

        Boolean ACTUALIZAR_ESTADO_PROCESO = true;
        if (HiddenField_PRESENTACION.Value == Presentacion.ContratosActivos.ToString())
        {
            ACTUALIZAR_ESTADO_PROCESO = false;
        }

        auditoriaContratos _auditoriaContratos = new auditoriaContratos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Decimal ID_AUDITORIA = _auditoriaContratos.ActualizarAuditoriaContratosPorSeccionYEstadoProceso(ID_EMPLEADO, tabla.CON_REG_EXAMENES_EMPLEADO, 1, ACTUALIZAR_ESTADO_PROCESO, ID_SOLICITUD, tabla.VAR_ESTADO_PROCESO_EXAMENES);

        if (ID_AUDITORIA <= 0)
        {
            Informar(Panel_MENSAJES, Label_MENSAJE, _auditoriaContratos.MensajeError, Proceso.Error);
        }
        else
        {
            Informar(Panel_MENSAJES, Label_MENSAJE, "La auditoría a la sección de examenes y autos de recomendación fue realizada correctamente.", Proceso.Correcto);

            cargar_seccion_examenes_autos(ID_EMPLEADO);

            presentar_interfaz_segun_resultado();
        }
    }
    
    private Boolean ExistenAutosRecomendacion(DataTable tablaOrdenes)
    {

        foreach (DataRow filaExamenes in tablaOrdenes.Rows)
        {
            if (!(String.IsNullOrEmpty(filaExamenes["OBSERVACIONES"].ToString().Trim())))
            {
                return true;
            }
        }

        return false;
    }

    protected void Button_IMPRIMIR_AUTOS_Click(object sender, EventArgs e)
    {

        Decimal ID_SOLICITUD = Convert.ToDecimal(TextBox_ID_SOLICITUD.Text);
        Decimal ID_REQUISICION = Convert.ToDecimal(HiddenField_ID_REQUERIMIENTO.Value);
        String EMPRESA = null;

        Boolean hayAutos = false;
        String armadoDeAutos = "";

        ordenExamenes _ordenes = new ordenExamenes(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaOrdenes = _ordenes.ObtenerConRegOrdenExamenPorSolicitud(Convert.ToInt32(ID_REQUISICION), Convert.ToInt32(ID_SOLICITUD));
        foreach (DataRow filaExamenes in tablaOrdenes.Rows)
        {
            if (!(String.IsNullOrEmpty(filaExamenes["OBSERVACIONES"].ToString().Trim())))
            {
                if (hayAutos == false)
                {
                    armadoDeAutos = filaExamenes["OBSERVACIONES"].ToString().Trim();
                }
                else
                {
                    armadoDeAutos += "; " + filaExamenes["OBSERVACIONES"].ToString().Trim();
                }
                hayAutos = true;
            }
        }

        if (hayAutos)
        {

            radicacionHojasDeVida _sol = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaSol = _sol.ObtenerRegSolicitudesingresoPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));
            DataRow filaSol = tablaSol.Rows[0];

            DateTime FECHA_AUTO = DateTime.Now;
            String NOMBRE_TRABAJADOR = TextBox_NOMBRES.Text.Trim() + " " + TextBox_APELLIDOS.Text.Trim();
            String TELEFONO_TRABAJADOR = TextBox_TEL_ASPIRANTE.Text.Trim();
            String TIPO_DOCUMENTO_IDENTIDAD = DropDownList_TIP_DOC_IDENTIDAD.SelectedValue;
            String NUMERO_DOCUMENTO_IDENTIDAD = TextBox_NUM_DOC_IDENTIDAD.Text.Trim();
            String AUTOS_RECOMENDACION = armadoDeAutos;
            String NOMBRE_EMPLEADOR = null;
            if (Session["idEmpresa"].ToString() == "1")
            {
                NOMBRE_EMPLEADOR = tabla.VAR_NOMBRE_SERTEMPO;
            }
            else
            {
                NOMBRE_EMPLEADOR = tabla.VAR_NOMBRE_EYS;
            }

            StreamReader archivo = new StreamReader(Server.MapPath(@"~\plantillas_reportes\autos_recomendacion.htm"));

            String html = archivo.ReadToEnd();

            archivo.Dispose();
            archivo.Close();

            html = html.Replace("[FECHA_AUTO]", FECHA_AUTO.ToLongDateString());
            html = html.Replace("[NOMBRE_TRABAJADOR]", NOMBRE_TRABAJADOR);
            html = html.Replace("[TELEFONO_TRABAJADOR]", TELEFONO_TRABAJADOR);
            html = html.Replace("[TIPO_DOCUMENTO_IDENTIDAD]", TIPO_DOCUMENTO_IDENTIDAD);
            html = html.Replace("[NUMERO_DOCUMENTO_IDENTIDAD]", NUMERO_DOCUMENTO_IDENTIDAD);
            html = html.Replace("[AUTOS_RECOMENDACION]", AUTOS_RECOMENDACION);
            html = html.Replace("[NOMBRE_EMPLEADOR]", NOMBRE_EMPLEADOR);
            html = html.Replace("[DIR_FIRMA_SALUD]", tabla.DIR_IMAGENES_PARA_PDF + "/firma_autos_recomendacion.jpg");

            String filename = "auto_recomendacion_" + ID_SOLICITUD.ToString();

            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;FileName=" + filename + ".pdf");

            Response.Clear();
            Response.ContentType = "application/pdf";


            iTextSharp.text.Document document = new iTextSharp.text.Document(new Rectangle(612, 397), 25, 25, 75, 30);

            iTextSharp.text.pdf.PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);

            pdfEvents PageEventHandler = new pdfEvents();
            writer.PageEvent = PageEventHandler;

            if (Session["idEmpresa"].ToString() == "1")
            {
                PageEventHandler.dirImagenHeader = Server.MapPath("~/imagenes/reportes/logo_sertempo.png");
            }
            else
            {
                PageEventHandler.dirImagenHeader = Server.MapPath("~/imagenes/reportes/logo_eficiencia.png");
            }
            PageEventHandler.fechaImpresion = DateTime.Now;
            PageEventHandler.tipoDocumento = "autos_recomendacion";

            document.Open();

            String tempFile = Path.GetTempFileName();

            using (StreamWriter tempwriter = new StreamWriter(tempFile, false))
            {
                tempwriter.Write(html);
            }

            List<IElement> htmlarraylist = HTMLWorker.ParseToList(new StreamReader(tempFile), new StyleSheet());
            foreach (IElement element in htmlarraylist)
            {
                document.Add(element);
            }

            document.Close();
            writer.Close();

            Response.End();

            File.Delete(tempFile);
        }
    }
    
    protected void GridView_HOJA_DE_TRABAJO_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_HOJA_DE_TRABAJO.PageIndex = e.NewPageIndex;

        cargar_hoja_trabajo_auditoria();
    }


    
    protected void DropDownList_RIESGO_EMPLEADO_SelectedIndexChanged(object sender, EventArgs e)
    {
        ColorearRiesgoInicial();
    }

    private Dictionary<String, byte[]> ObtenerArchivosSeleccionados(String prefijoNombreArchivo, SeccionEnvio seccion)
    {
        Decimal ID_SOLICITUD = Convert.ToDecimal(TextBox_ID_SOLICITUD.Text.Trim());
        Decimal ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);
        Decimal ID_REQUERIMIENTO = Convert.ToDecimal(HiddenField_ID_REQUERIMIENTO.Value);
        Decimal ID_CONTRATO = Convert.ToDecimal(HiddenField_ID_CONTRATO.Value);
        Decimal ID_EMPLEADO = Convert.ToDecimal(Label_ID_EMPLEADO.Text);

        Operacion _operacion = new Operacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaContrato = _operacion.ObtenerInformacionContratoPorIdEmpleado(ID_EMPLEADO);
        DataRow filaContrato = tablaContrato.Rows[0];

        Decimal ID_REFERENCIA = 0;
        if (String.IsNullOrEmpty(filaContrato["ID_REFERENCIA"].ToString().Trim()) == false)
        {
            ID_REFERENCIA = Convert.ToDecimal(filaContrato["ID_REFERENCIA"]);
        }
        
        Dictionary<String, byte[]> listaArchivos = new Dictionary<String, byte[]>();

        maestrasInterfaz _maestrasInterfaz = new maestrasInterfaz();

        switch(seccion)
        {
            case SeccionEnvio.Seleccion:
                if (CheckBoxList_DOCUMENTOS_SELECCION.Items[0].Selected == true)
                {
                    listaArchivos.Add(prefijoNombreArchivo + "INFORME_SELECCION.pdf", _maestrasInterfaz.GenerarPDFEntrevista(ID_SOLICITUD, ID_PERFIL));
                }

                if (CheckBoxList_DOCUMENTOS_SELECCION.Items[1].Selected == true)
                {
                    Dictionary<String, byte[]> archivosPruebas = _maestrasInterfaz.ObtenerArchivosPruebas(prefijoNombreArchivo, ID_PERFIL, ID_SOLICITUD);

                    foreach (KeyValuePair<String, byte[]> archivoPrueba in archivosPruebas)
                    {
                        listaArchivos.Add(archivoPrueba.Key, archivoPrueba.Value);
                    }
                }

                if (ID_REFERENCIA > 0)
                {
                    if (CheckBoxList_DOCUMENTOS_SELECCION.Items[2].Selected == true)
                    {
                        listaArchivos.Add(prefijoNombreArchivo + "CONFIRMACION_REFERENCIAS_LABORALES.pdf", _maestrasInterfaz.GenerarPDFReferencia(ID_REFERENCIA, ID_SOLICITUD));
                    }
                }
                break;
            case SeccionEnvio.Contratacion:
                if (CheckBoxList_DOCUEMENTOS_CONTRATACION.Items[0].Selected == true)
                {
                    Dictionary<String, byte[]> archivosExamenes = _maestrasInterfaz.ObtenerArchivosExamenes(prefijoNombreArchivo, ID_SOLICITUD, ID_REQUERIMIENTO);

                    foreach (KeyValuePair<String, byte[]> archivoExamen in archivosExamenes)
                    {
                        listaArchivos.Add(archivoExamen.Key, archivoExamen.Value);
                    }
                }


                if (CheckBoxList_DOCUEMENTOS_CONTRATACION.Items[1].Selected == true)
                {
                    byte[] archivoExamenes = _maestrasInterfaz.GenerarPDFExamenes(ID_CONTRATO, ID_SOLICITUD, ID_REQUERIMIENTO);
                    if (archivoExamenes != null)
                    {
                        listaArchivos.Add(prefijoNombreArchivo + "AUTOS_RECOMENDACION.pdf", archivoExamenes);
                    }
                }

                if (CheckBoxList_DOCUEMENTOS_CONTRATACION.Items[2].Selected == true)
                {
                    listaArchivos.Add(prefijoNombreArchivo + "CONTRATO.pdf", _maestrasInterfaz.GenerarPDFContrato(ID_CONTRATO));
                }

                if (CheckBoxList_DOCUEMENTOS_CONTRATACION.Items[3].Selected == true)
                {
                    byte[] archivoClausulas = _maestrasInterfaz.GenerarPDFClausulas(ID_CONTRATO);

                    if (archivoClausulas != null)
                    {
                        listaArchivos.Add(prefijoNombreArchivo + "CLAUSULAS_CONTRATO.pdf", archivoClausulas);
                    }
                }


                if (CheckBoxList_DOCUEMENTOS_CONTRATACION.Items[4].Selected == true)
                {
                    Dictionary<String, byte[]> archivosAfiliaciones = _maestrasInterfaz.ObtenerArchivosAfiliaciones(prefijoNombreArchivo, ID_CONTRATO);

                    foreach (KeyValuePair<String, byte[]> archivoAfiliacion in archivosAfiliaciones)
                    {
                        listaArchivos.Add(archivoAfiliacion.Key, archivoAfiliacion.Value);
                    }
                }
                break;
        }
        
        return listaArchivos;
    }

    private void DownloadArchivo(String prefijoNombreArchivo, Dictionary<String, byte[]> listaArchivosSeleccion, Dictionary<String, byte[]> listaArchivosContratacion)
    {
        Response.Clear();
        Response.BufferOutput = false; 
        String archiveName = String.Format(prefijoNombreArchivo + "DOCUMENTACION_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd"));
        Response.ContentType = "application/zip";
        Response.AddHeader("content-disposition", "filename=" + archiveName);

        using (ZipOutputStream s = new ZipOutputStream(Response.OutputStream))
        {
            s.SetLevel(9); 

            foreach (KeyValuePair<String, byte[]> archivo in listaArchivosSeleccion)
            {
                ZipEntry entry = new ZipEntry(archivo.Key);



                entry.DateTime = DateTime.Now;
                s.PutNextEntry(entry);

                s.Write(archivo.Value, 0, (int)archivo.Value.Length);
            }

            foreach (KeyValuePair<String, byte[]> archivo in listaArchivosContratacion)
            {
                ZipEntry entry = new ZipEntry(archivo.Key);



                entry.DateTime = DateTime.Now;
                s.PutNextEntry(entry);

                s.Write(archivo.Value, 0, (int)archivo.Value.Length);
            }


            s.Finish();

            s.Close();
        }

        Response.Close();
    }

    private Boolean EnviarArchivo(String prefijoNombreArchivo, SeccionEnvio seccion, Dictionary<String, byte[]> listaArchivos)
    {
        String archiveName;
        if(seccion == SeccionEnvio.Seleccion)
        {
            archiveName = String.Format(prefijoNombreArchivo + "DOCUMENTACION_SELECCION_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd"));
        }
        else
        {
            archiveName = String.Format(prefijoNombreArchivo + "DOCUMENTACION_CONTRATACION_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd"));
        }
        
        MemoryStream streamArchivoAEnviar = new MemoryStream();

        ZipOutputStream s = new ZipOutputStream(streamArchivoAEnviar);
            s.SetLevel(9); 

            foreach (KeyValuePair<String, byte[]> archivo in listaArchivos)
            {
                ZipEntry entry = new ZipEntry(archivo.Key);



                entry.DateTime = DateTime.Now;
                s.PutNextEntry(entry);

                s.Write(archivo.Value, 0, (int)archivo.Value.Length);
            }


        StreamReader archivo_mensaje_correo = new StreamReader(Server.MapPath(@"~\plantillas_reportes\email_envio_docs_trabajador.htm"));

        String html_mensaje_correo = archivo_mensaje_correo.ReadToEnd();

        archivo_mensaje_correo.Dispose();
        archivo_mensaje_correo.Close();

        html_mensaje_correo = html_mensaje_correo.Replace("[NOMBRE_CLIENTE]", Label_EMPRESA_TRABAJADOR.Text.Trim());
        html_mensaje_correo = html_mensaje_correo.Replace("[NUMERO_CONTRATO]", HiddenField_ID_CONTRATO.Value);
        html_mensaje_correo = html_mensaje_correo.Replace("[NOMBRE_TRABAJADOR]", TextBox_NOMBRES.Text.Trim() + " " + TextBox_APELLIDOS.Text.Trim());
        html_mensaje_correo = html_mensaje_correo.Replace("[NUM_DOC_IDENTIDAD]", DropDownList_TIP_DOC_IDENTIDAD.SelectedItem.Text + " " + TextBox_NUM_DOC_IDENTIDAD.Text.Trim());
        if (seccion == SeccionEnvio.Seleccion)
        {
            html_mensaje_correo = html_mensaje_correo.Replace("[NOMBRE_CONTACTO_CLIENTE]", Label_NOMBRE_CONTACTO_SELECCION.Text.Trim());
        }
        else
        {
            html_mensaje_correo = html_mensaje_correo.Replace("[NOMBRE_CONTACTO_CLIENTE]", Label_NOMBRE_CONTACTO_CONTRATACION.Text.Trim());
        }
        

        usuario _usuario = new usuario(Session["idEmpresa"].ToString());
        DataTable tablaUsuario = _usuario.ObtenerUsuarioPorUsuLog(Session["USU_LOG"].ToString());
        DataRow filaUsuario = tablaUsuario.Rows[0];

        if (filaUsuario["USU_TIPO"].ToString().ToUpper() == "PLANTA")
        {
            html_mensaje_correo = html_mensaje_correo.Replace("[USUARIO_ENVIO]", filaUsuario["NOMBRES"].ToString().Trim() + " " + filaUsuario["APELLIDOS"].ToString().Trim());
        }
        else
        {
            html_mensaje_correo = html_mensaje_correo.Replace("[USUARIO_ENVIO]", filaUsuario["NOMBRES_EXTERNO"].ToString().Trim() + " " + filaUsuario["APELLIDOS_EXTERNO"].ToString().Trim());
        }

        tools _tools = new tools();

        if (seccion == SeccionEnvio.Seleccion)
        {
            if (_tools.enviarCorreoConCuerpoHtmlyArchivoAdjunto(TextBox_EMAIL_SELECCION.Text, "DOCUMENTACION: CONTRATO " + HiddenField_ID_CONTRATO.Value + " - " + DropDownList_TIP_DOC_IDENTIDAD.SelectedItem.Text.Trim() + " " + TextBox_NUM_DOC_IDENTIDAD.Text.Trim() + " - " + TextBox_NOMBRES.Text.Trim() + " " + TextBox_APELLIDOS.Text.Trim(), html_mensaje_correo, streamArchivoAEnviar, archiveName) == false)
            {
                Informar(Panel_MENSAJE_ENVIOARCHOVOS, Label_MENSAJE_ENVIOARCHIVOS, "Error al intentar enviar el correo al contácto de selección: " + _tools.MensajError, Proceso.Error);
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            if (_tools.enviarCorreoConCuerpoHtmlyArchivoAdjunto(TextBox_EMAIL_CONTRATACION.Text, "DOCUMENTACION: CONTRATO " + HiddenField_ID_CONTRATO.Value + " - " + DropDownList_TIP_DOC_IDENTIDAD.SelectedItem.Text.Trim() + " " + TextBox_NUM_DOC_IDENTIDAD.Text.Trim() + " - " + TextBox_NOMBRES.Text.Trim() + " " + TextBox_APELLIDOS.Text.Trim(), html_mensaje_correo, streamArchivoAEnviar, archiveName) == false)
            {
                Informar(Panel_MENSAJE_ENVIOARCHOVOS, Label_MENSAJE_ENVIOARCHIVOS, "Error al intentar enviar el correo al contácto de contratación: " + _tools.MensajError, Proceso.Error);
                return false;
            }
            else
            {
                return true;
            } 
        }


        s.Finish();

        s.Close();

    }

    private void ArmarYEnviarArchivos(AccionesEnvio accion)
    {
        String prefijoNombreArchivo = TextBox_NUM_DOC_IDENTIDAD.Text.Trim() + "-" + HiddenField_ID_CONTRATO.Value + "-";

        Dictionary<String, byte[]> listaArchivosSeleccion = ObtenerArchivosSeleccionados(prefijoNombreArchivo, SeccionEnvio.Seleccion);
        Dictionary<String, byte[]> listaArchivosContratacion = ObtenerArchivosSeleccionados(prefijoNombreArchivo, SeccionEnvio.Contratacion);

        if (accion == AccionesEnvio.download)
        {
            DownloadArchivo(prefijoNombreArchivo, listaArchivosSeleccion, listaArchivosContratacion);
        }
        else
        {
            Boolean verificado = true;

            if (ConfigurationManager.AppSettings["extensionesImagenesPermitidas"].ToLower() == "true")
            {
                if (listaArchivosSeleccion.Count > 0)
                {
                    if (EnviarArchivo(prefijoNombreArchivo, SeccionEnvio.Seleccion, listaArchivosSeleccion) == false)
                    {
                        verificado = false;
                    }
                }

                if (verificado == true)
                {
                    if (listaArchivosContratacion.Count > 0)
                    {
                        if (EnviarArchivo(prefijoNombreArchivo, SeccionEnvio.Contratacion, listaArchivosContratacion) == false)
                        {
                            verificado = false;
                        }
                    }
                }
            }

            if (verificado == true)
            {
                Decimal ID_EMPLEADO = Convert.ToDecimal(Label_ID_EMPLEADO.Text.Trim());
                Decimal ID_SOLICITUD = Convert.ToDecimal(TextBox_ID_SOLICITUD.Text.Trim());

                Boolean ACTUALIZAR_ESTADO_PROCESO = true;
                if (HiddenField_PRESENTACION.Value == Presentacion.ContratosActivos.ToString())
                {
                    ACTUALIZAR_ESTADO_PROCESO = false;
                }

                auditoriaContratos _auditoriaContratos = new auditoriaContratos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

                Decimal ID_AUDITORIA = _auditoriaContratos.ActualizarAuditoriaContratosPorSeccionYEstadoProceso(ID_EMPLEADO, tabla.CON_CONFIGURACON_DOCS_ENTREGABLES, 1, ACTUALIZAR_ESTADO_PROCESO, ID_SOLICITUD, tabla.VAR_ESTADO_PROCESO_ENVIO_ARCHIVOS);

                if (ID_AUDITORIA <= 0)
                {
                    Informar(Panel_MENSAJES, Label_MENSAJE, _auditoriaContratos.MensajeError, Proceso.Error);
                }
                else
                {
                    Informar(Panel_MENSAJES, Label_MENSAJE, "La auditoría a la sección de envío de documentación al cliente fue realizada correctamente.", Proceso.Correcto);

                    cargar_seccion_envio_archivos(ID_EMPLEADO);

                    presentar_interfaz_segun_resultado();
                }
            }
        }
    }

    protected void Button_REVISAR_DOCUMENTOS_Click(object sender, EventArgs e)
    {
        Boolean seleccion = false;
        Boolean contratacion = false;
        foreach (System.Web.UI.WebControls.ListItem item in CheckBoxList_DOCUMENTOS_SELECCION.Items)
        {
            if (item.Selected == true)
            {
                seleccion = true;
                break;
            }
        }

        foreach (System.Web.UI.WebControls.ListItem item in CheckBoxList_DOCUEMENTOS_CONTRATACION.Items)
        {
            if (item.Selected == true)
            {
                contratacion = true;
                break;
            }
        }

        if (seleccion == false && contratacion == false)
        {
            Informar(Panel_MENSAJE_ENVIOARCHOVOS, Label_MENSAJE_ENVIOARCHIVOS, "Debe seleccionar por lo menos un documento para enviar.", Proceso.Error);
        }
        else
        {
            ArmarYEnviarArchivos(AccionesEnvio.download);
        }
    }
    
    protected void Button_ACTUALIZAR_ENVIOARCHIVOS_Click(object sender, EventArgs e)
    {
        Boolean seleccion = false;
        Boolean contratacion = false;
        foreach (System.Web.UI.WebControls.ListItem item in CheckBoxList_DOCUMENTOS_SELECCION.Items)
        {
            if (item.Selected == true)
            {
                seleccion = true;
                break;
            }
        }

        foreach (System.Web.UI.WebControls.ListItem item in CheckBoxList_DOCUEMENTOS_CONTRATACION.Items)
        {
            if (item.Selected == true)
            {
                contratacion = true;
                break;
            }
        }

        if (seleccion == false && contratacion == false)
        {
            Informar(Panel_MENSAJE_ENVIOARCHOVOS, Label_MENSAJE_ENVIOARCHIVOS, "Debe seleccionar por lo menos un documento para enviar.", Proceso.Error);
        }
        else
        {
            ArmarYEnviarArchivos(AccionesEnvio.correo);
        }
    }

    private void Cargar(DropDownList dropDownList, string parametro)
    {
        dropDownList.Items.Clear();
        parametro _parametro = new parametro(Session["idEmpresa"].ToString());
        DataTable dataTable = _parametro.ObtenerParametrosPorTabla(parametro);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione", "");
        dropDownList.Items.Add(item);
        foreach (DataRow dataRow in dataTable.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(dataRow["DESCRIPCION"].ToString(), dataRow["CODIGO"].ToString());
            dropDownList.Items.Add(item);
        }
        dropDownList.DataBind();
    }

    private void GuardarNuevaClausula()
    {
        tools _tools = new tools();
        Byte[] archivo = null;
        Int32 archivo_tamaño = 0;
        decimal id_empleado_clausula = 0;
        String archivo_extension = null;
        String archivo_tipo = null;
        decimal id_ocupacion = -1;

        Decimal ID_CONTRATO = Convert.ToDecimal(HiddenField_ID_CONTRATO.Value);

        try
        {
            if (FileUpload_ArchivoClausula.HasFile == true)
            {
                using (BinaryReader reader = new BinaryReader(FileUpload_ArchivoClausula.PostedFile.InputStream))
                {
                    archivo = reader.ReadBytes(FileUpload_ArchivoClausula.PostedFile.ContentLength);
                    archivo_tamaño = FileUpload_ArchivoClausula.PostedFile.ContentLength;
                    archivo_tipo = FileUpload_ArchivoClausula.PostedFile.ContentType;
                    archivo_extension = _tools.obtenerExtensionArchivo(FileUpload_ArchivoClausula.PostedFile.FileName);
                }

                id_ocupacion = -1;
                
                Clausula clausula = new Clausula(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                id_empleado_clausula = clausula.AdicionarClausulaYClausulaEmpleado(DropDownList_ID_TIPO_CLAUSULA.SelectedValue, "1", TextBox_DESCRIPCION.Text, -1, id_ocupacion, archivo, archivo_tamaño, archivo_extension, archivo_tipo, ID_CONTRATO);

                if (id_empleado_clausula > 0)
                {

                    Decimal ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);
                    Decimal ID_EMPLEADO = Convert.ToDecimal(Label_ID_EMPLEADO.Text);

                    cargar_seccion_conceptos_fijos(ID_PERFIL, ID_EMPLEADO);

                    Panel_DatosNuevaClausula.Visible = false;
                    DropDownList_ID_TIPO_CLAUSULA.ClearSelection();
                    TextBox_DESCRIPCION.Text = "";

                    PanelAdicionarClausula.Visible = true;
                    Button_AdicionarClausula.Visible = true;

                    Button_AdicionarClausula.Focus();
                }
                else
                {
                    Informar(Panel_MENSAJES_CONCEPTOS_FIJOS, Label_MENSAJES_CONCEPTOS_FIJOS, "No se pudo crear la clausula especificada.", Proceso.Error);
                    Button_ProcesarNuevaClausula.Focus();
                }
            }
        }
        catch (Exception e)
        {
            Informar(Panel_MENSAJES_CONCEPTOS_FIJOS, Label_MENSAJES_CONCEPTOS_FIJOS, e.Message, Proceso.Error);
        }
    }

    protected void Button_AdicionarClausula_Click(object sender, EventArgs e)
    {
        Button_AdicionarClausula.Visible = false;
        PanelAdicionarClausula.Visible = false;

        Cargar(DropDownList_ID_TIPO_CLAUSULA, tabla.PARAMETROS_TIPOS_CLAUSULA);
        DropDownList_ID_TIPO_CLAUSULA.ClearSelection();

        Panel_DatosNuevaClausula.Visible = true;
    }

    protected void Button_ProcesarNuevaClausula_Click(object sender, EventArgs e)
    {
        GuardarNuevaClausula();
    }

    protected void Button_CancelarnuevaClausula_Click(object sender, EventArgs e)
    {
        DropDownList_ID_TIPO_CLAUSULA.ClearSelection();
        TextBox_DESCRIPCION.Text = "";

        Panel_DatosNuevaClausula.Visible = false;
        
        Button_AdicionarClausula.Visible = true;
        PanelAdicionarClausula.Visible = true;

    }

    protected void DropDownList_forma_pago_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_forma_pago.SelectedIndex <= 0)
        {
            Panel_ENTIDAD_BANCARIA_Y_CUENTA.Visible = false;
            Panel_PagoCheque.Visible = false;

            TextBox_NUMERO_CUENTA.Text = "";
        }
        else
        {
            if ((DropDownList_forma_pago.SelectedValue == "CONSIGNACIÓN BANCARIA") || (DropDownList_forma_pago.SelectedValue == "DISPERSION") || (DropDownList_forma_pago.SelectedValue == "ACH"))
            {
                Panel_ENTIDAD_BANCARIA_Y_CUENTA.Visible = true;
                Panel_PagoCheque.Visible = false;

                cargar_DropDownList_entidad_bancaria_todos();
                cargar_DropDownList_TIPO_CUENTA(DropDownList_TIPO_CUENTA);

                TextBox_NUMERO_CUENTA.Text = "";
            }
            else
            {
                if (DropDownList_forma_pago.SelectedValue == "CHEQUE")
                {
                    Panel_ENTIDAD_BANCARIA_Y_CUENTA.Visible = false;
                    Panel_PagoCheque.Visible = true;
                }
                else
                {
                    Panel_ENTIDAD_BANCARIA_Y_CUENTA.Visible = false;
                    Panel_PagoCheque.Visible = false;
                    TextBox_NUMERO_CUENTA.Text = "";
                }
            }
        }
    }

    protected void CheckBox_NoLaboro_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_NoLaboro.Checked == true)
        {
            CheckBox_DobleRegistro.Checked = false;
            Panel_FORMA_PAGO.Visible = false;
        }
        else
        {
            if (CheckBox_DobleRegistro.Checked == false)
            {
                Panel_FORMA_PAGO.Visible = true;
            }
        }
    }

    protected void CheckBox_DobleRegistro_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_DobleRegistro.Checked == true)
        {
            CheckBox_NoLaboro.Checked = false;
            Panel_FORMA_PAGO.Visible = false;
        }
        else
        {
            if (CheckBox_NoLaboro.Checked == false)
            {
                Panel_FORMA_PAGO.Visible = true;
            }
        }
    }

    protected void DropDownList_FORMA_PAGO_NOMINA_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_FORMA_PAGO_NOMINA.SelectedIndex <= 0)
        {
            cargar_check_periodos("0");
        }
        else
        {
            cargar_check_periodos(DropDownList_FORMA_PAGO_NOMINA.SelectedValue);
        }
    }

    private void ocultar_mensaje(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.Style.Add("display", "none");
        panel_mensaje.Style.Add("display", "none");

        panel_fondo.Visible = false;
        panel_mensaje.Visible = false;
    }

    protected void Button_ACEPTAR_AVISO_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_AVISO, Panel_AVISO);
   
        if(CheckBox_DobleRegistro.Checked == true)
        {
            ProcederConActualizacion(false);

            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);

            Informar(Panel_MENSAJES, Label_MENSAJE, "El contrato fue INACTIVADO correctamente. ", Proceso.Correcto);

            ocultar_mensaje(Panel_FONDO_AVISO, Panel_AVISO);

        }
        else
        {
            ocultar_mensaje(Panel_FONDO_AVISO, Panel_AVISO);

            ProcederConActualizacion(true);
        }
    }
    protected void DropDownList_DepartamentoCajaC_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_DepartamentoCajaC.SelectedIndex > 0)
        {
            llenarDropCiudades(DropDownList_DepartamentoCajaC.SelectedValue, Convert.ToDecimal(DropDownList_ENTIDAD_Caja.SelectedValue));
        }
        else
        {
            DropDownList_CiudadCajaC.Items.Clear();
            DropDownList_CiudadCajaC.Items.Add(new System.Web.UI.WebControls.ListItem("Seleccione...", ""));
            DropDownList_CiudadCajaC.Items.Clear();
        }
    }

    protected void DropDownList_ENTIDAD_Caja_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_ENTIDAD_Caja.SelectedIndex > 0)
        {
            if (DropDownList_ENTIDAD_Caja.SelectedValue == "43" || DropDownList_ENTIDAD_Caja.SelectedItem.Text.ToUpper() == "SIN CAJA")
            {
                Panel_CiudadCaja.Visible = false;
                DropDownList_DepartamentoCajaC.ClearSelection();
                DropDownList_CiudadCajaC.ClearSelection();
            }
            else
            {
                Panel_CiudadCaja.Visible = true;
                llenarDropDepartamentos(Convert.ToDecimal(DropDownList_ENTIDAD_Caja.SelectedValue));
            }
        }
        else
        {
            DropDownList_DepartamentoCajaC.Items.Clear();
            DropDownList_DepartamentoCajaC.Items.Add(new System.Web.UI.WebControls.ListItem("Seleccione...", ""));
            DropDownList_DepartamentoCajaC.Items.Clear();
        }

        DropDownList_CiudadCajaC.Items.Clear();
        DropDownList_CiudadCajaC.Items.Add(new System.Web.UI.WebControls.ListItem("Seleccione...", ""));
        DropDownList_CiudadCajaC.Items.Clear();
    }
}
   