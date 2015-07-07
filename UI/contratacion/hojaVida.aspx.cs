using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brainsbits.LLB.seleccion;
using System.Data;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB.maestras;
using System.IO;
using Brainsbits.LLB;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using Brainsbits.LLB.seguridad;
using TSHAK.Components;
using Brainsbits.LLB.programasRseGlobal;
using System.Configuration;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;



public partial class _Default : System.Web.UI.Page
{
    #region variables
    private System.Drawing.Color colorContratoActivo = System.Drawing.ColorTranslator.FromHtml("#50CE04");
    private System.Drawing.Color colorSeleccionado = System.Drawing.ColorTranslator.FromHtml("#699DF0");

    private System.Drawing.Color colorNo = System.Drawing.ColorTranslator.FromHtml("#F8523A");
    private System.Drawing.Color colorSi = System.Drawing.ColorTranslator.FromHtml("#50CE04");

    private enum Acciones
    {
        Inicio = 0, 
        buscar = 1,
        cargarContratos = 2,
        sinContratos = 3,
        arp = 4,
        contratoSeleccionado = 5,
        eps = 6,
        ccf = 7,
        afp = 8,
        entregas = 9,
        actasDescago = 10,
        tutelas = 11, 
        demandas = 12,
        derechosPeticion = 13,
        incapacidades = 14, 
        impresionesBasicas = 15,
        conceptosFijos = 16,
        Pagos = 17, 
        detalle_pago=18,
        actividades
    }

    private enum Impresiones
    {
        OrdenExamen = 0,
        AutosRecomendacion
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }

    private enum Contratos
    {
        Ninguno = 0,
        Indefinido,
        Integral,
        ObraLabor,
        Universitario,
        Sena
    }

    private enum ClaseContrato
    {
        O_L = 0,
        IN,
        I,
        T_F,
        L_C_C_D_A,
        L_S_C_D_A
    }

    ReportDocument reporte;

    #endregion variables
    #region metodos
    private void Ocultar(Acciones accion)
    {
        switch (accion)
        { 
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = false;
                Panel_MENSAJE_POPUP.Visible = false;
                Panel_FORM_BOTONES_ENCABEZADO.Visible = false;

                Panel_MENSAJES.Visible = false;

                Panel_RESULTADOS_GRID.Visible = false;

                Panel_FORMULARIO.Visible = false;
                Panel_DATOS_PERSONA_SELECCIONDA.Visible = false;
                Panel_GRILLA_CONTRATOS.Visible = false;
                Panel_AGRUPADOR_DATOS_INFORMATIVOS.Visible = false;
                break;
            case Acciones.arp:
                Panel_MENSAJE_ARP.Visible = false;
                break;
            case Acciones.eps:
                Panel_MENSAJE_EPS.Visible = false;
                break;
            case Acciones.ccf:
                Panel_MENSAJE_CCF.Visible = false;
                break;
            case Acciones.afp:
                Panel_MENSAJE_AFP.Visible = false;
                break;
            case Acciones.entregas:
                Panel_INFO_ENTREGAS.Visible = false;
                Panel_MENSAJE_ENTREGAS.Visible = false;
                break;
            case Acciones.actasDescago:
                Panel_MENSAJE_ACTAS_DESCARGO.Visible = false;
                break;
            case Acciones.tutelas:
                Panel_MENSAJE_TUTELAS.Visible = false;
                break;
            case Acciones.demandas:
                Panel_MENSAJE_DEMANDAS.Visible = false;
                break;
            case Acciones.derechosPeticion:
                Panel_MENSAJE_DERECHOS_PETICION.Visible = false;
                break;
            case Acciones.incapacidades:
                Panel_INFO_INCAPACIDADES.Visible = false;
                Panel_MENSAJE_INCAPACIDADES.Visible = false;
                break;
            case Acciones.impresionesBasicas:
                Panel_INFO_IMPRESIONES_BASICAS.Visible = false;
                Panel_MENSAJE_IMPRESIONES_BASICAS.Visible = false;
                break;
            case Acciones.Pagos:
                Panel_INFO_PAGOS.Visible = false;
                Panel_MENSAJE_PAGOS.Visible = false;
                GridView_DETALLE_PAGOS.Visible = false;
                PNL_DETALLE_PAGO.Visible = false;
                break;
            case Acciones.conceptosFijos:
                Panel_INFO_CONCEPTOS_FIJOS.Visible = false;
                Panel_MENSAJES_CONCEPTOS_FIJOS.Visible = false;
                break;
            case Acciones.actividades:
                Panel_INFO_ACTIVIDADES_PROGRAMAS.Visible = false;
                Panel_MENSAJES_ACTIVIDADES.Visible = false;
                Panel_ActividadesBinestar.Visible = false;
                Panel_ActividadesRse.Visible = false;
                Panel_ActividadesGlobalSalud.Visible = false;
                Panel_ActividadOperaciones.Visible = false;
                PanelActividadesGestionHumana.Visible = false;
                break;
        }
    }
    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                break;
            case Acciones.buscar:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.cargarContratos:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Panel_FORMULARIO.Visible = true;
                Panel_DATOS_PERSONA_SELECCIONDA.Visible = true;
                Panel_GRILLA_CONTRATOS.Visible = true;
                break;
            case Acciones.sinContratos:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Panel_DATOS_PERSONA_SELECCIONDA.Visible = true;
                break;
            case Acciones.contratoSeleccionado:
                Panel_AGRUPADOR_DATOS_INFORMATIVOS.Visible = true;
                Panel_INFO_AFILIACIONES.Visible = true;
                break;
            case Acciones.entregas:
                break;
            case Acciones.incapacidades:
                Panel_INFO_INCAPACIDADES.Visible = true;
                break;
            case Acciones.Pagos:
                Panel_INFO_PAGOS.Visible = true;
                break;
            case Acciones.detalle_pago:
                GridView_DETALLE_PAGOS.Visible = true;
                PNL_DETALLE_PAGO.Visible = true;
                break;
            case Acciones.impresionesBasicas:
                Panel_INFO_IMPRESIONES_BASICAS.Visible = true;
                break;
            case Acciones.conceptosFijos:
                Panel_INFO_CONCEPTOS_FIJOS.Visible = true;
                break;
        }
    }
    private void cargar_DropDownList_BUSCAR()
    {
        DropDownList_BUSCAR.Items.Clear();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Ninguno", "");
        DropDownList_BUSCAR.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("Núm identificación", "NUM_DOC_IDENTIDAD");
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
    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:

                cargar_DropDownList_BUSCAR();
                configurarCaracteresAceptadosBusqueda(true, true);
                break;
        }
    }

    private void Cargar(Decimal ID_SOLICITUD)
    {
        HiddenField_ID_SOLICITUD.Value = ID_SOLICITUD.ToString();

        Decimal ID_ENTREVISTA = 0;

        radicacionHojasDeVida _rad = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaSolicitud = _rad.ObtenerRegSolicitudesingresoPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));
        DataRow filaSolicitud = tablaSolicitud.Rows[0];

        hojasVida _hoja = new hojasVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaEntrevista = _hoja.ObtenerSelRegEntrevistasPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));

        if (tablaEntrevista.Rows.Count > 0)
        {
            DataRow filaEntrevista = tablaEntrevista.Rows[0];

            ID_ENTREVISTA = Convert.ToDecimal(filaEntrevista["REGISTRO"]);
        }

        CargarFamilia(filaSolicitud, ID_ENTREVISTA);
     }

    private void cargamos_DropDownList_ESTADO_CIVIL()
    {
        DropDownList_ESTADO_CIVIL.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_ESTADO_CIVIL);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_ESTADO_CIVIL.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_ESTADO_CIVIL.Items.Add(item);
        }
        DropDownList_ESTADO_CIVIL.DataBind();
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
    private void Cargar_DropDownList_CabezaFamilia()
    {
        DropDownList_CabezaFamilia.Items.Clear();

        DropDownList_CabezaFamilia.Items.Add(new System.Web.UI.WebControls.ListItem("Seleccione...", ""));

        DropDownList_CabezaFamilia.Items.Add(new System.Web.UI.WebControls.ListItem("NO", "N"));
        DropDownList_CabezaFamilia.Items.Add(new System.Web.UI.WebControls.ListItem("SI", "S"));

        DropDownList_CabezaFamilia.DataBind();
    }

    private void Cargar_DropDownList_TipoFamiliar(DropDownList drop)
    {
        drop.Items.Clear();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccionae...", "");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("ABUELA", "ABUELA");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("ABUELO", "ABUELO");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("MAMA", "MAMA");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("PAPA", "PAPA");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("ESPOSA", "ESPOSA");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("ESPOSO", "ESPOSO");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("HIJA", "HIJA");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("HIJO", "HIJO");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("HIJASTRA", "HIJASTRA");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("HIJASTRO", "HIJASTRO");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("TIA", "TIA");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("TIO", "TIO");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("PRIMA", "PRIMA");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("PRIMO", "PRIMO");
        drop.Items.Add(item);

        drop.DataBind();
    }
    private void Cargar_Grilla_Composicionfamiliar_Desdetabla(DataTable tablaComposicion)
    {
        tools _tools = new tools();

        GridView_ComposicionFamiliar.DataSource = tablaComposicion;
        GridView_ComposicionFamiliar.DataBind();

        for (int i = 0; i < GridView_ComposicionFamiliar.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ComposicionFamiliar.Rows[i];
            DataRow filaTabla = tablaComposicion.Rows[i];

            DropDownList droptipoFamiliar = filaGrilla.FindControl("DropDownList_TipoFamiliar") as DropDownList;
            Cargar_DropDownList_TipoFamiliar(droptipoFamiliar);
            droptipoFamiliar.SelectedValue = filaTabla["ID_TIPO_FAMILIAR"].ToString().Trim();

            TextBox textoNombres = filaGrilla.FindControl("TextBox_NombresFamiliar") as TextBox;
            textoNombres.Text = filaTabla["NOMBRES"].ToString().Trim();

            TextBox textoApellidos = filaGrilla.FindControl("TextBox_ApellidosFamiliar") as TextBox;
            textoApellidos.Text = filaTabla["APELLIDOS"].ToString().Trim();

            TextBox textoFechaFamiliar = filaGrilla.FindControl("TextBox_FechaNacimientoFamiliar") as TextBox;
            Label labelEdad = filaGrilla.FindControl("Label_Edad") as Label;
            if (String.IsNullOrEmpty(filaTabla["FECHA_NACIMIENTO"].ToString().Trim()) == false)
            {
                textoFechaFamiliar.Text = Convert.ToDateTime(filaTabla["FECHA_NACIMIENTO"]).ToShortDateString();

                try
                {
                    labelEdad.Text = "Edad: " + _tools.ObtenerEdadDesdeFechaNacimiento(Convert.ToDateTime(filaTabla["FECHA_NACIMIENTO"])).ToString();
                }
                catch
                {
                    labelEdad.Text = "Edad: Desconocida.";
                }
            }
            else
            {
                textoFechaFamiliar.Text = "";
                labelEdad.Text = "Edad: Desconocida.";
            }

            TextBox textoProfesion = filaGrilla.FindControl("TextBox_ProfesionFamiliar") as TextBox;
            textoProfesion.Text = filaTabla["PROFESION"].ToString().Trim();

            CheckBox checkExtranjero = filaGrilla.FindControl("CheckBox_Extranjero") as CheckBox;
            Panel panelViveEn = filaGrilla.FindControl("Panel_ViveEn") as Panel;
            DropDownList dropDepartamento = filaGrilla.FindControl("DropDownList_DepartamentoFamiliar") as DropDownList;
            DropDownList dropCiudad = filaGrilla.FindControl("DropDownList_CiudadFamiliar") as DropDownList;
            if (String.IsNullOrEmpty(filaTabla["ID_CIUDAD"].ToString().Trim()) == false)
            {
                if (filaTabla["ID_CIUDAD"].ToString().Trim() == "EXTRA")
                {
                    checkExtranjero.Checked = true;
                    panelViveEn.Visible = false;
                    cargar_DropDownList_DEPARTAMENTO(dropDepartamento);
                    dropCiudad.Items.Clear();
                }
                else
                {
                    DataRow filaInfoCiudadYDepartamento = obtenerDatosCiudadViveFamiliar(filaTabla["ID_CIUDAD"].ToString().Trim());
                    cargar_DropDownList_DEPARTAMENTO(dropDepartamento);
                    dropDepartamento.SelectedValue = filaInfoCiudadYDepartamento["ID_DEPARTAMENTO"].ToString().Trim();
                    cargar_DropDownList_CIUDAD(filaInfoCiudadYDepartamento["ID_DEPARTAMENTO"].ToString().Trim(), dropCiudad);
                    dropCiudad.SelectedValue = filaInfoCiudadYDepartamento["ID_CIUDAD"].ToString().Trim();
                }
            }
            else
            {
                checkExtranjero.Checked = false;
                panelViveEn.Visible = true;
                cargar_DropDownList_DEPARTAMENTO(dropDepartamento);
                dropCiudad.Items.Clear();
            }

            CheckBox check = filaGrilla.FindControl("CheckBox_ViveConElFamiliar") as CheckBox;
            if (filaTabla["VIVE_CON_EL"].ToString().Trim().ToUpper() == "TRUE")
            {
                check.Checked = true;
            }
            else
            {
                check.Checked = false;
            }
        }

        ObtenerNumeroDeHijos();
    }

    private void cargar_DropDownList_CIUDAD(String idDepartamento, DropDownList drop)
    {
        drop.Items.Clear();

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
        DataTable tablaCiudades = _ciudad.ObtenerCiudadesPorIdDepartamento(idDepartamento);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione Ciudad", "");
        drop.Items.Add(item);

        foreach (DataRow fila in tablaCiudades.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOMBRE"].ToString(), fila["ID_CIUDAD"].ToString());
            drop.Items.Add(item);
        }

        drop.DataBind();
    }

    private DataRow obtenerDatosCiudadViveFamiliar(String idCiudad)
    {
        DataRow resultado = null;

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());

        DataTable tablaIdDepartamento = _ciudad.ObtenerIdDepartamentoConIdCiudad(idCiudad);

        if (tablaIdDepartamento.Rows.Count > 0)
        {
            resultado = tablaIdDepartamento.Rows[0];
        }

        return resultado;
    }

    private void cargar_DropDownList_DEPARTAMENTO(DropDownList drop)
    {
        drop.Items.Clear();

        departamento _departamento = new departamento(Session["idEmpresa"].ToString());
        DataTable tablaDepartamentos = _departamento.ObtenerTodasLosDepartamentos();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        drop.Items.Add(item);

        foreach (DataRow fila in tablaDepartamentos.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOMBRE"].ToString(), fila["ID_DEPARTAMENTO"].ToString());
            drop.Items.Add(item);
        }

        drop.DataBind();
    }

    private void CargarComposicionFamiliar(Decimal ID_ENTREVISTA)
    {
        hojasVida _hojasVida = new hojasVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablainfofamiliar = _hojasVida.ObtenerSelRegComposicionFamiliar(ID_ENTREVISTA);

        if (tablainfofamiliar.Rows.Count <= 0)
        {
            if (_hojasVida.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _hojasVida.MensajeError, Proceso.Error);
            }

            GridView_ComposicionFamiliar.DataSource = null;
            GridView_ComposicionFamiliar.DataBind();
        }
        else
        {
            Cargar_Grilla_Composicionfamiliar_Desdetabla(tablainfofamiliar);
        }
    }

    private void ObtenerNumeroDeHijos()
    {
        String numHijosActual = "0";

        foreach (GridViewRow filaGrilla in GridView_ComposicionFamiliar.Rows)
        {
            DropDownList dropFamiliar = filaGrilla.FindControl("DropDownList_TipoFamiliar") as DropDownList;

            if ((dropFamiliar.SelectedValue == "HIJO") || (dropFamiliar.SelectedValue == "HIJA"))
            {
                numHijosActual += 1;
            }
        }

        TextBox_NUM_HIJOS.Text = numHijosActual;
    }
    private void CargarFamilia(DataRow filaSolicitud, Decimal ID_ENTREVISTA)
    {
        cargamos_DropDownList_ESTADO_CIVIL();
        try { DropDownList_ESTADO_CIVIL.SelectedValue = filaSolicitud["ESTADO_CIVIL"].ToString().Trim(); }
        catch { DropDownList_ESTADO_CIVIL.SelectedIndex = 0; }

        Cargar_DropDownList_CabezaFamilia();
        try { DropDownList_CabezaFamilia.SelectedValue = filaSolicitud["C_FMLIA"].ToString(); }
        catch { DropDownList_CabezaFamilia.SelectedIndex = 0; }

        if (ID_ENTREVISTA <= 0)
        {
            GridView_ComposicionFamiliar.DataSource = null;
            this.Panel_MENSAJE_INFORMACION_FAMILIAR.Visible = true;
            this.Label_MENSAJE_INFORMACION_FAMILIAR.Text = "No se encontró composición Familiar.";
        }
        else
        {
            this.Panel_MENSAJE_INFORMACION_FAMILIAR.Visible = false;
            this.Label_MENSAJE_INFORMACION_FAMILIAR.Text = string.Empty;
            CargarComposicionFamiliar(ID_ENTREVISTA);
        }

        ObtenerNumeroDeHijos();
    }


    private void CargarInterfazCOIdEspecifico(Decimal ID_SOLICITUD)
    {
        radicacionHojasDeVida _rad = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaSolicitud = _rad.ObtenerRegSolicitudesingresoPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));

        if (tablaSolicitud.Rows.Count <= 0)
        {
            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);

            Informar(Panel_MENSAJES, Label_MENSAJE, "No se encontró información de la Solicitud de Ingreso seleccionada.", Proceso.Error);
        }
        else
        {
            DataRow filaSolicitud = tablaSolicitud.Rows[0];

            String NOMBRE_PERSONA_SELECCIONADA = filaSolicitud["NOMBRES"].ToString().Trim() + " " + filaSolicitud["APELLIDOS"].ToString().Trim();
            String NUM_DOCUMENTO_PERSONA_SELECCIONADA = filaSolicitud["TIP_DOC_IDENTIDAD"].ToString().Trim() + " " + filaSolicitud["NUM_DOC_IDENTIDAD"].ToString().Trim();
            String MAIL_PERSONA_SELECCIONADA = filaSolicitud["E_MAIL"].ToString().Trim();
            String DIRECCION_PERSONA_SELECCIONADA = filaSolicitud["DIR_ASPIRANTE"].ToString().Trim();
            String TELEFONO_PERSONA_SELECCIONADA = filaSolicitud["TEL_ASPIRANTE"].ToString().Trim();


            registroContrato _registroContrato = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaContratos = _registroContrato.ObtenerContratosPorIdSolicitud(ID_SOLICITUD);

            cargar_datos_persona_seleccionada(ID_SOLICITUD, NOMBRE_PERSONA_SELECCIONADA, NUM_DOCUMENTO_PERSONA_SELECCIONADA, MAIL_PERSONA_SELECCIONADA, DIRECCION_PERSONA_SELECCIONADA, TELEFONO_PERSONA_SELECCIONADA);

 
            Ocultar(Acciones.Inicio);

            if (tablaContratos.Rows.Count <= 0)
            {
                if (_registroContrato.MensajeError == null)
                {
                    Mostrar(Acciones.sinContratos);
                    Informar(Panel_MENSAJES, Label_MENSAJE, "No se encontraron contratos asociados a la persona seleccionada.", Proceso.Error);
                }
                else
                {
                    Mostrar(Acciones.sinContratos);
                    Informar(Panel_MENSAJES, Label_MENSAJE, _registroContrato.MensajeError, Proceso.Error);
                }
            }
            else
            {
                Mostrar(Acciones.cargarContratos);
                Cargar(tablaContratos);
            }           
        }
    }

    private void Iniciar()
    {
        Panel_INFO_ENTREGAS.Visible = false;

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_SOLICITUD = 0;
        try
        {
            ID_SOLICITUD = Convert.ToDecimal(QueryStringSeguro["id_solicitud"]);
        }
        catch
        {
            ID_SOLICITUD = 0;
        }

        if (ID_SOLICITUD != 0)
        {
            CargarInterfazCOIdEspecifico(ID_SOLICITUD);
            
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);
        }
    }
    #endregion mtodos
    protected void Page_Load(object sender, EventArgs e)
    {
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

    protected void DropDownList_BUSCAR_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_BUSCAR.SelectedValue == "")
        {
            configurarCaracteresAceptadosBusqueda(true, true);
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
    private void Buscar()
    {
        String datoCapturado = TextBox_BUSCAR.Text.Trim();

        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.buscar);

        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString()); 
        DataTable tablaInfotrabajador = new DataTable();

        switch (this.DropDownList_BUSCAR.SelectedValue)
        {
            case "NUM_DOC_IDENTIDAD":
                tablaInfotrabajador = _radicacionHojasDeVida.ObtenerRegSolicitudesingresoPorNumDocIdentidad(datoCapturado);
                break;
            case "NOMBRES":
                tablaInfotrabajador = _radicacionHojasDeVida.ObtenerRegSolicitudesingresoPorNombres(datoCapturado);
                break;
            case "APELLIDOS":
                tablaInfotrabajador = _radicacionHojasDeVida.ObtenerRegSolicitudesingresoPorApellidos(datoCapturado);
                break;
        }

        if (tablaInfotrabajador.Rows.Count > 0)
        {
            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaInfotrabajador;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
        }
        else
        {
            if (!String.IsNullOrEmpty(_radicacionHojasDeVida.MensajeError))
            {
                Informar(Panel_MENSAJES, Label_MENSAJE, "Error: Consulte con el Administrador: " + _radicacionHojasDeVida.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_MENSAJES, Label_MENSAJE, "ADVERTENCIA: No se encontró información para " + this.DropDownList_BUSCAR.SelectedItem + " : " + this.TextBox_BUSCAR.Text, Proceso.Error);
            }

            Panel_RESULTADOS_GRID.Visible = false;
        }
    }
    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        Buscar();
    }
    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;
        Buscar();
    }
    private void Cargar(DataTable tablaContratos)
    {
        GridView_CONTRATOS.DataSource = tablaContratos;
        GridView_CONTRATOS.DataBind();

        GridViewRow fila;
        for (int i = 0; i < GridView_CONTRATOS.Rows.Count; i++)
        {
            fila = GridView_CONTRATOS.Rows[i];
            if (GridView_CONTRATOS.DataKeys[i].Values["CONTRATO_ESTADO"].ToString() == "Activo")
            {
                GridView_CONTRATOS.Rows[i].BackColor = colorContratoActivo;
            }
            else
            {
                GridView_CONTRATOS.Rows[i].BackColor = System.Drawing.Color.Transparent;
            }
        }
    }
    private void cargar_datos_persona_seleccionada(Decimal ID_SOLICITUD, String NOMBRE_PERSONA_SELECCIONADA, String NUM_DOCUMENTO_PERSONA_SELECCIONADA, String MAIL_PERSONA_SELECCIONADA,
        string DIRECCION_PERSONA_SELECCIONADA, string TELEFONO_PERSONA_SELECCIONADA)
    {
        Label_ID_SOLICITUD.Text = ID_SOLICITUD.ToString();
        Label_NUM_DOCUMENTO_PERSONA_SELECCIONADA.Text = NUM_DOCUMENTO_PERSONA_SELECCIONADA;
        Label_NOMBRE_PERSONA_SELECCIONADA.Text = NOMBRE_PERSONA_SELECCIONADA;
        Label_MAIL_PERSONA_SELECCIONADA.Text = MAIL_PERSONA_SELECCIONADA;
        Label_DIRECCION_PERSONA_SELECCIONADA.Text = DIRECCION_PERSONA_SELECCIONADA;
        Label_TELEFONO_PERSONA_SELECCIONADA.Text = TELEFONO_PERSONA_SELECCIONADA;
    }


    protected void GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged(object sender, EventArgs e)
    {
        Decimal ID_SOLICITUD = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["ID_SOLICITUD"]);
        String NOMBRE_PERSONA_SELECCIONADA = GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[2].Text + " " + GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[3].Text;
        String NUM_DOCUMENTO_PERSONA_SELECCIONADA = GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[4].Text + " " + GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[5].Text;
        String MAIL_PERSONA_SELECCIONADA = GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[6].Text;
        String DIRECCION_PERSONA_SELECCIONADA = GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[7].Text;
        String TELEFONO_PERSONA_SELECCIONADA = GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[8].Text;

        registroContrato _registroContrato = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaContratos = _registroContrato.ObtenerContratosPorIdSolicitud(ID_SOLICITUD);

        cargar_datos_persona_seleccionada(ID_SOLICITUD, NOMBRE_PERSONA_SELECCIONADA, NUM_DOCUMENTO_PERSONA_SELECCIONADA, MAIL_PERSONA_SELECCIONADA, DIRECCION_PERSONA_SELECCIONADA, TELEFONO_PERSONA_SELECCIONADA);

        Cargar(ID_SOLICITUD);

        Ocultar(Acciones.Inicio);

        if (tablaContratos.Rows.Count <= 0)
        {
            if (_registroContrato.MensajeError == null)
            {
                Mostrar(Acciones.sinContratos);
                Informar(Panel_MENSAJES, Label_MENSAJE, "No se encontraron contratos asociados a la persona seleccionada.", Proceso.Error);
            }
            else
            {
                Mostrar(Acciones.sinContratos);
                Informar(Panel_MENSAJES, Label_MENSAJE, _registroContrato.MensajeError, Proceso.Error);
            }
        }
        else
        {
            Mostrar(Acciones.cargarContratos);
            Cargar(tablaContratos);
        }
    }


    private void poner_color_a_grilla_contratos()
    {
        for (int i = 0; i < GridView_CONTRATOS.Rows.Count; i++)
        {
            if (GridView_CONTRATOS.DataKeys[i].Values["CONTRATO_ESTADO"].ToString() == "Activo")
            {
                GridView_CONTRATOS.Rows[i].BackColor = colorContratoActivo;
            }
            else
            {
                GridView_CONTRATOS.Rows[i].BackColor = System.Drawing.Color.Transparent;
            }
        }
    }
    private void poner_color_a_grilla_pagos()
    {
        for (int i = 0; i < GridView_PAGOS.Rows.Count; i++)
        {
            if (GridView_PAGOS.DataKeys[i].Values["CODIGO"].ToString() == "Activo")
            {
                GridView_PAGOS.Rows[i].BackColor = colorContratoActivo;
            }
            else
            {
                GridView_PAGOS.Rows[i].BackColor = System.Drawing.Color.Transparent;
            }
        }
    }
    private void cargar_afiliaciones_arp(Decimal ID_CONTRATO, Decimal ID_EMPLEADO, Decimal ID_REQUERIMIENTO)
    { 
        Decimal ID_SOLICITUD = Convert.ToDecimal(Label_ID_SOLICITUD.Text);

        afiliacion _afiliacion = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaAfiliacionARP = _afiliacion.ObtenerconafiliacionArpPorSolicitudRequerimientoHV(Convert.ToInt32(ID_SOLICITUD), Convert.ToInt32(ID_REQUERIMIENTO));

        Ocultar(Acciones.arp);

        if (tablaAfiliacionARP.Rows.Count <= 0)
        {
            if (_afiliacion.MensajeError == null)
            {
                Informar(Panel_MENSAJE_ARP, Label_MENSAJE_ARP, "no se encontraron afiliaciones a ARL para este contrato.", Proceso.Error);
            }
            else
            {
                Informar(Panel_MENSAJE_ARP, Label_MENSAJE_ARP, _afiliacion.MensajeError, Proceso.Error);
            }

            GridView_ARP.DataSource = null;
            GridView_ARP.DataBind();
        }
        else
        {
            GridView_ARP.DataSource = tablaAfiliacionARP;
            GridView_ARP.DataBind();

            GridView_ARP.Rows[0].BackColor = colorContratoActivo;
        }
    }
    private void cargar_afiliaciones_eps(Decimal ID_CONTRATO, Decimal ID_EMPLEADO, Decimal ID_REQUERIMIENTO)
    {
        Decimal ID_SOLICITUD = Convert.ToDecimal(Label_ID_SOLICITUD.Text);

        afiliacion _afiliacion = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaAfiliacionEPS = _afiliacion.ObtenerconafiliacionEpsPorSolicitudRequerimientoHV(Convert.ToInt32(ID_SOLICITUD), Convert.ToInt32(ID_REQUERIMIENTO));

        Ocultar(Acciones.eps);

        if (tablaAfiliacionEPS.Rows.Count <= 0)
        {
            if (_afiliacion.MensajeError == null)
            {
                Informar(Panel_MENSAJE_EPS, Label_MENSAJE_EPS, "no se encontraron afiliaciones a EPS para este contrato.", Proceso.Error);
            }
            else
            {
                Informar(Panel_MENSAJE_EPS, Label_MENSAJE_EPS, _afiliacion.MensajeError, Proceso.Error);
            }

            GridView_EPS.DataSource = null;
            GridView_EPS.DataBind();
        }
        else
        {
            GridView_EPS.DataSource = tablaAfiliacionEPS;
            GridView_EPS.DataBind();

            GridView_EPS.Rows[0].BackColor = colorContratoActivo;
        }
    }
    private void cargar_afiliaciones_ccf(Decimal ID_CONTRATO, Decimal ID_EMPLEADO, Decimal ID_REQUERIMIENTO)
    {
        Decimal ID_SOLICITUD = Convert.ToDecimal(Label_ID_SOLICITUD.Text);

        afiliacion _afiliacion = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaAfiliacionCCF = _afiliacion.ObtenerconafiliacionCajasCPorSolicitudRequerimientoHV(Convert.ToInt32(ID_SOLICITUD), Convert.ToInt32(ID_REQUERIMIENTO));

        Ocultar(Acciones.ccf);

        if (tablaAfiliacionCCF.Rows.Count <= 0)
        {
            if (_afiliacion.MensajeError == null)
            {
                Informar(Panel_MENSAJE_CCF, Label_MENSAJE_CCF, "no se encontraron afiliaciones a CCF para este contrato.", Proceso.Error);
            }
            else
            {
                Informar(Panel_MENSAJE_CCF, Label_MENSAJE_CCF, _afiliacion.MensajeError, Proceso.Error);
            }

            GridView_CCF.DataSource = null;
            GridView_CCF.DataBind();
        }
        else
        {
            GridView_CCF.DataSource = tablaAfiliacionCCF;
            GridView_CCF.DataBind();

            GridView_CCF.Rows[0].BackColor = colorContratoActivo;
        }
    }
    private void cargar_afiliaciones_afp(Decimal ID_CONTRATO, Decimal ID_EMPLEADO, Decimal ID_REQUERIMIENTO)
    {
        Decimal ID_SOLICITUD = Convert.ToDecimal(Label_ID_SOLICITUD.Text);

        afiliacion _afiliacion = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaAfiliacionAFP = _afiliacion.ObtenerconafiliacionfpensionesPorSolicitudRequerimiento_HV(Convert.ToInt32(ID_SOLICITUD), Convert.ToInt32(ID_REQUERIMIENTO));

        Ocultar(Acciones.afp);

        if (tablaAfiliacionAFP.Rows.Count <= 0)
        {
            if (_afiliacion.MensajeError == null)
            {
                Informar(Panel_MENSAJE_AFP, Label_MENSAJE_AFP, "no se encontraron afiliaciones a AFP para este contrato.", Proceso.Error);
            }
            else
            {
                Informar(Panel_MENSAJE_AFP, Label_MENSAJE_AFP, _afiliacion.MensajeError, Proceso.Error);
            }

            GridView_AFP.DataSource = null;
            GridView_AFP.DataBind();
        }
        else
        {
            GridView_AFP.DataSource = tablaAfiliacionAFP;
            GridView_AFP.DataBind();

            GridView_AFP.Rows[0].BackColor = colorContratoActivo;
        }
    }
    private void cargar_afiliaciones(Decimal ID_CONTRATO, Decimal ID_EMPLEADO, Decimal ID_REQUERIMIENTO)
    {
        cargar_afiliaciones_arp(ID_CONTRATO,ID_EMPLEADO,ID_REQUERIMIENTO);

        cargar_afiliaciones_eps(ID_CONTRATO, ID_EMPLEADO, ID_REQUERIMIENTO);

        cargar_afiliaciones_ccf(ID_CONTRATO, ID_EMPLEADO, ID_REQUERIMIENTO);

        cargar_afiliaciones_afp(ID_CONTRATO, ID_EMPLEADO, ID_REQUERIMIENTO);
    }
    private void cargar_entregas(Decimal ID_EMPLEADO)
    {
        AsignacionServiciosComplementarios _AsignacionServiciosComplementarios = new AsignacionServiciosComplementarios(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaEntregas = _AsignacionServiciosComplementarios.ObtenerAsignacionSCporIdEmpleado(ID_EMPLEADO);

        Ocultar(Acciones.entregas);

        Mostrar(Acciones.entregas);

        if (tablaEntregas.Rows.Count <= 0)
        {
            if (_AsignacionServiciosComplementarios.MensajeError == null)
            {
                Informar(Panel_MENSAJE_ENTREGAS, Label_MENSAJE_ENTREGAS, "No se encontraron entregas para este contrato.", Proceso.Error);
            }
            else
            {
                Informar(Panel_MENSAJE_ENTREGAS, Label_MENSAJE_ENTREGAS, _AsignacionServiciosComplementarios.MensajeError, Proceso.Error);
            }

            GridView_ENTREGAS.DataSource = null;
            GridView_ENTREGAS.DataBind();
        }
        else
        {
            GridView_ENTREGAS.DataSource = tablaEntregas;
            GridView_ENTREGAS.DataBind();
        }
    }
    private void cargar_actas_descargo(Decimal ID_EMPLEADO)
    {
        descargo _descargo = new descargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaActas = _descargo.ObtenerPorIdEmpleado(ID_EMPLEADO);

        Ocultar(Acciones.actasDescago);

        if (tablaActas.Rows.Count <= 0)
        {
            if (_descargo.MensajeError == null)
            {
                Informar(Panel_MENSAJE_ACTAS_DESCARGO, Label_MENSAJE_ACTAS_DESCARGO, "No se encontraron descargos ni procesos disciplinarios para este contrato.", Proceso.Error);
            }
            else
            {
                Informar(Panel_MENSAJE_ACTAS_DESCARGO, Label_MENSAJE_ACTAS_DESCARGO, _descargo.MensajeError, Proceso.Error);
            }

            GridView_ACTAS_DESCARGO.DataSource = null;
            GridView_ACTAS_DESCARGO.DataBind();
        }
        else
        {
            GridView_ACTAS_DESCARGO.DataSource = tablaActas;
            GridView_ACTAS_DESCARGO.DataBind();
        }
    }
    private void cargar_tutelas(Decimal ID_CONTRATO)
    {
        Decimal ID_SOLICITUD = Convert.ToDecimal(Label_ID_SOLICITUD.Text);

        actosjuridicos _actosjuridicos = new actosjuridicos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaTutelas = _actosjuridicos.ObtenerTutelasPorIdSolidcitudRegistroContrato(ID_SOLICITUD, ID_CONTRATO);

        Ocultar(Acciones.tutelas);

        if (tablaTutelas.Rows.Count <= 0)
        {
            if (_actosjuridicos.MensajeError == null)
            {
                Informar(Panel_MENSAJE_TUTELAS, Label_MENSAJE_TUTELAS, "No se encontraron tutelas para este contrato.", Proceso.Error);
            }
            else
            {
                Informar(Panel_MENSAJE_TUTELAS, Label_MENSAJE_TUTELAS, _actosjuridicos.MensajeError, Proceso.Error);
            }

            GridView_TUTELAS.DataSource = null;
            GridView_TUTELAS.DataBind();
        }
        else
        {
            GridView_TUTELAS.DataSource = tablaTutelas;
            GridView_TUTELAS.DataBind();
        }
    }
    private void cargar_demandas(Decimal ID_CONTRATO)
    {
        Decimal ID_SOLICITUD = Convert.ToDecimal(Label_ID_SOLICITUD.Text);

        actosjuridicos _actosjuridicos = new actosjuridicos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaDemandas = _actosjuridicos.ObtenerDemandasPorIdSolidcitudRegistroContrato(ID_SOLICITUD, ID_CONTRATO);

        Ocultar(Acciones.demandas);

        if (tablaDemandas.Rows.Count <= 0)
        {
            if (_actosjuridicos.MensajeError == null)
            {
                Informar(Panel_MENSAJE_DEMANDAS, Label_MENSAJE_DEMANDAS, "No se encontraron demandas para este contrato.", Proceso.Error);
            }
            else
            {
                Informar(Panel_MENSAJE_DEMANDAS, Label_MENSAJE_DEMANDAS, _actosjuridicos.MensajeError, Proceso.Error);
            }

            GridView_DEMANDAS.DataSource = null;
            GridView_DEMANDAS.DataBind();
        }
        else
        {
            GridView_DEMANDAS.DataSource = tablaDemandas;
            GridView_DEMANDAS.DataBind();
        }
    }
    private void cargar_derechos_peticion(Decimal ID_CONTRATO)
    {
        Decimal ID_SOLICITUD = Convert.ToDecimal(Label_ID_SOLICITUD.Text);

        actosjuridicos _actosjuridicos = new actosjuridicos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaDerechos = _actosjuridicos.ObtenerDerechosPorIdSolidcitudRegistroContrato(ID_SOLICITUD, ID_CONTRATO);

        Ocultar(Acciones.derechosPeticion);

        if (tablaDerechos.Rows.Count <= 0)
        {
            if (_actosjuridicos.MensajeError == null)
            {
                Informar(Panel_MENSAJE_DERECHOS_PETICION, Label_MENSAJE_DERECHOS_PETICION, "No se encontraron derechos de petición para este contrato.", Proceso.Error);
            }
            else
            {
                Informar(Panel_MENSAJE_DERECHOS_PETICION, Label_MENSAJE_DERECHOS_PETICION, _actosjuridicos.MensajeError, Proceso.Error);
            }

            GridView_DERECHOS_PETICION.DataSource = null;
            GridView_DERECHOS_PETICION.DataBind();
        }
        else
        {
            GridView_DERECHOS_PETICION.DataSource = tablaDerechos;
            GridView_DERECHOS_PETICION.DataBind();
        }
    }
    private void cargar_informacion_juridica(Decimal ID_EMPLEADO, Decimal ID_CONTRATO)
    {
        cargar_actas_descargo(ID_EMPLEADO);
        
        cargar_tutelas(ID_CONTRATO);

        cargar_demandas(ID_CONTRATO);

        cargar_derechos_peticion(ID_CONTRATO);
    }
    private void cargar_incapacidades(Decimal ID_CONTRATO)
    {
        incapacidad _incapacidad = new incapacidad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaIncapacidades = _incapacidad.ObtenerPorIdContrato(ID_CONTRATO);

        Ocultar(Acciones.incapacidades);

        Mostrar(Acciones.incapacidades);

        if (tablaIncapacidades.Rows.Count <= 0)
        {
            if (_incapacidad.MensajeError == null)
            {
                Informar(Panel_MENSAJE_INCAPACIDADES, Label_MENSAJE_INCAPACIDADES, "No se encontraron incapacidades para este contrato.", Proceso.Error);
            }
            else
            {
                Informar(Panel_MENSAJE_INCAPACIDADES, Label_MENSAJE_INCAPACIDADES, _incapacidad.MensajeError, Proceso.Error);
            }

            GridView_INCAPACIDADES.DataSource = null;
            GridView_INCAPACIDADES.DataBind();
        }
        else
        {
            GridView_INCAPACIDADES.DataSource = tablaIncapacidades;
            GridView_INCAPACIDADES.DataBind();
        }
    }

    private void cargar_pagos(Decimal ID_CONTRATO)
    {
        Pagos _Pagos = new Pagos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaPagos = _Pagos.ObtenerPorIdContrato(ID_CONTRATO);
        Ocultar(Acciones.Pagos);
        Mostrar(Acciones.Pagos);
        if (tablaPagos.Rows.Count <= 0)
        {
            if (_Pagos.MensajeError == null)
            {
                Informar(Panel_MENSAJE_PAGOS, Label_MENSAJE_PAGOS, "No se encontraron pagos para este contrato.", Proceso.Error);
            }
            else
            {
                Informar(Panel_MENSAJE_PAGOS, Label_MENSAJE_PAGOS, _Pagos.MensajeError, Proceso.Error);
            }

            GridView_PAGOS.DataSource = null;
            GridView_PAGOS.DataBind();
        }
        else
        {
            GridView_PAGOS.DataSource = tablaPagos;
            GridView_PAGOS.DataBind();
        }
    }
    private void cargar_detalle_pagos(Decimal ID_LIQ_NOMINA_EMPLEADOS)
    {
        Pagos _Pagos = new Pagos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaDetallePagos = _Pagos.ObtenerdetallePorIdContrato(ID_LIQ_NOMINA_EMPLEADOS);
        Mostrar(Acciones.detalle_pago);
        if (tablaDetallePagos.Rows.Count <= 0)
        {
            if (_Pagos.MensajeError == null)
            {
                Informar(Panel_MENSAJE_PAGOS, Label_MENSAJE_PAGOS, "No se encontraron pagos para este contrato.", Proceso.Error);
            }
            else
            {
                Informar(Panel_MENSAJE_PAGOS, Label_MENSAJE_PAGOS, _Pagos.MensajeError, Proceso.Error);
            }
            GridView_DETALLE_PAGOS.DataSource = null;
            GridView_DETALLE_PAGOS.DataBind();
            GridView_DETALLE_PAGOS.Focus();
        }
        else
        {
            GridView_DETALLE_PAGOS.DataSource = tablaDetallePagos;
            GridView_DETALLE_PAGOS.DataBind();
        }
    }
    private void cargar_impresiones_basicas(Decimal ID_CONTRATO)
    {
        Ocultar(Acciones.impresionesBasicas);

        Mostrar(Acciones.impresionesBasicas);

        HiddenField_ID_CONTRATO.Value = ID_CONTRATO.ToString();
    }
    private void cargar_conceptos_fijos(Decimal ID_EMPLEADO)
    {
        ConceptosNominaEmpleado _ConceptosNominaEmpleado = new ConceptosNominaEmpleado(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaConceptosFijos = _ConceptosNominaEmpleado.ObtenerNomConceptosEmpleadosPorIdEmpleado(Convert.ToInt32(ID_EMPLEADO));

        Ocultar(Acciones.conceptosFijos);

        Mostrar(Acciones.conceptosFijos);

        if (tablaConceptosFijos.Rows.Count <= 0)
        {
            if (_ConceptosNominaEmpleado.MensajeError == null)
            {
                Informar(Panel_MENSAJE_INCAPACIDADES, Label_MENSAJE_INCAPACIDADES, "No se encontraron conceptos fijos para este contrato.", Proceso.Error);
            }
            else
            {
                Informar(Panel_MENSAJE_INCAPACIDADES, Label_MENSAJE_INCAPACIDADES, _ConceptosNominaEmpleado.MensajeError, Proceso.Error);
            }

            GridView_CONCEPTOS_FIJOS.DataSource = null;
            GridView_CONCEPTOS_FIJOS.DataBind();
        }
        else
        {
            GridView_CONCEPTOS_FIJOS.DataSource = tablaConceptosFijos;
            GridView_CONCEPTOS_FIJOS.DataBind();

            DataRow fila;
            for (int i = 0; i < tablaConceptosFijos.Rows.Count; i++)
            { 
                fila = tablaConceptosFijos.Rows[i];
                if (fila["ACTIVO"].ToString().Trim() == "S")
                {
                    GridView_CONCEPTOS_FIJOS.Rows[i].BackColor = colorSi;
                }
                else
                {
                    GridView_CONCEPTOS_FIJOS.Rows[i].BackColor = colorNo;
                }
            }
        }
    }

    private void Cargar_GridViewActividades(Programa.Areas area, GridView grid, DataTable tablaActividades, String img_area, String nombre_area, Int32 proceso)
    {
        grid.DataSource = tablaActividades;
        grid.DataBind();

        for (int i = 0; i < grid.Rows.Count; i++)
        {
            GridViewRow filaGrilla = grid.Rows[i];
            DataRow filaTabla = tablaActividades.Rows[i];

            tools _tools = new tools();
            SecureQueryString QueryStringSeguro;
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

            QueryStringSeguro["img_area"] = img_area;
            QueryStringSeguro["nombre_area"] = nombre_area;
            QueryStringSeguro["nombre_modulo"] = "VISOR DE ACTIVIDADES";
            QueryStringSeguro["accion"] = "inicial";

            QueryStringSeguro["id_detalle"] = filaTabla["ID_DETALLE"].ToString().Trim();

            QueryStringSeguro["proceso"] = proceso.ToString();

            HyperLink link = filaGrilla.FindControl("HyperLink_VisorActividad") as HyperLink;
            link.Target = "_blank";
            link.NavigateUrl = "~/maestros/visorActividades.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        }
    }

    private void cargar_actividades_programas(Decimal ID_EMPLEADO)
    {
        Programa _prog = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaActividadesBienestar = _prog.ObtenerActividadesALasQueAsistioEmpleadoPorArea(Programa.Areas.BS, ID_EMPLEADO);
        DataTable tablaActividadesRse = _prog.ObtenerActividadesALasQueAsistioEmpleadoPorArea(Programa.Areas.RSE, ID_EMPLEADO);
        DataTable tablaActividadesSalud = _prog.ObtenerActividadesALasQueAsistioEmpleadoPorArea(Programa.Areas.GLOBALSALUD, ID_EMPLEADO);
        DataTable tablaActividadesOperaciones = _prog.ObtenerActividadesALasQueAsistioEmpleadoPorArea(Programa.Areas.OPERACIONES, ID_EMPLEADO);
        DataTable tablaActividadesGestionHumana = _prog.ObtenerActividadesALasQueAsistioEmpleadoPorArea(Programa.Areas.GESTIONHUMANA, ID_EMPLEADO);
        
        Ocultar(Acciones.actividades);

        Panel_INFO_ACTIVIDADES_PROGRAMAS.Visible = true;

        if ((tablaActividadesBienestar.Rows.Count <= 0) && (tablaActividadesRse.Rows.Count <= 0) && (tablaActividadesSalud.Rows.Count <= 0) && (tablaActividadesOperaciones.Rows.Count <= 0) && (tablaActividadesGestionHumana.Rows.Count <= 0))
        {
            if (_prog.MensajeError != null)
            {
                Informar(Panel_MENSAJES_ACTIVIDADES, Label_MENSAJES_ACTIVIDADES, _prog.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_MENSAJES_ACTIVIDADES, Label_MENSAJES_ACTIVIDADES, "El colaborador seleccionado no ha participado en actividades.", Proceso.Correcto);
            }
        }
        else
        {
            if (tablaActividadesBienestar.Rows.Count > 0)
            {
                Panel_ActividadesBinestar.Visible = true;
                Cargar_GridViewActividades(Programa.Areas.BS, GridView_ActividadesBienestar, tablaActividadesBienestar, "bienestarsocial", "BIENESTAR SOCIAL", (int)tabla.proceso.ContactoBienestarSocial);
            }

            if (tablaActividadesRse.Rows.Count > 0)
            {
                Panel_ActividadesRse.Visible = true;
                Cargar_GridViewActividades(Programa.Areas.RSE, GridView_ActividadesRse, tablaActividadesRse, "rse", "RSE -RESPONSABILIDAD SOCIAL EMPRESARIAL-", (int)tabla.proceso.ContactoRse);
            }

            if (tablaActividadesSalud.Rows.Count > 0)
            {
                Panel_ActividadesGlobalSalud.Visible = true;
                Cargar_GridViewActividades(Programa.Areas.GLOBALSALUD, GridView_ActividadesSaludOcupacional, tablaActividadesSalud, "globalsalud", "SALUD OCUPACIONAL", (int)tabla.proceso.ContactoGlobalSalud);
            }


            if (tablaActividadesOperaciones.Rows.Count > 0)
            {
                Panel_ActividadOperaciones.Visible = true;
                Cargar_GridViewActividades(Programa.Areas.OPERACIONES, GridView_ActividadesOperaciones, tablaActividadesOperaciones, "operaciones", "OPERACIONES", (int)tabla.proceso.ContactoOperaciones);
            }

            if (tablaActividadesGestionHumana.Rows.Count > 0)
            {
                PanelActividadesGestionHumana.Visible = true;
                Cargar_GridViewActividades(Programa.Areas.GESTIONHUMANA, GridView_ActividadesGestionHumana, tablaActividadesGestionHumana, "gestionhumana", "GESTIÓN HUMANA", (int)tabla.proceso.ContactoGestionHumana);
            }
        }
    }

    private void cargar_datos_contrato_seleccionado(Decimal ID_CONTRATO, Decimal ID_EMPLEADO, Decimal ID_REQUERIMIENTO)
    { 
        cargar_impresiones_basicas(ID_CONTRATO);

        cargar_afiliaciones(ID_CONTRATO, ID_EMPLEADO, ID_REQUERIMIENTO);

        cargar_entregas(ID_EMPLEADO);

        cargar_informacion_juridica(ID_EMPLEADO, ID_CONTRATO);

        cargar_incapacidades(ID_CONTRATO);

        cargar_pagos(ID_CONTRATO);

        cargar_conceptos_fijos(ID_EMPLEADO);

        cargar_actividades_programas(ID_EMPLEADO);
    }
    private void cargar_detalle_del_pago(Decimal ID_LIQ_NOMINA_EMPLEADOS)
    {
        cargar_detalle_pagos(ID_LIQ_NOMINA_EMPLEADOS);   
    }
    protected void GridView_CONTRATOS_SelectedIndexChanged(object sender, EventArgs e)
    {
        Decimal ID_CONTRATO = Convert.ToDecimal(GridView_CONTRATOS.SelectedDataKey["ID_CONTRATO"]);
        Decimal ID_EMPLEADO = Convert.ToDecimal(GridView_CONTRATOS.SelectedDataKey["ID_EMPLEADO"]);
        Decimal ID_REQUERIMIENTO = Convert.ToDecimal(GridView_CONTRATOS.SelectedDataKey["ID_REQUERIMIENTO"]);
        Decimal ID_PERFIL = Convert.ToDecimal(GridView_CONTRATOS.SelectedDataKey["ID_PERFIL"]);

        HiddenField_ID_PERFIL.Value = ID_PERFIL.ToString();
        Label_ID_REQUERIMIENTO.Text = ID_REQUERIMIENTO.ToString();

        poner_color_a_grilla_contratos();
        GridView_CONTRATOS.SelectedRow.BackColor = colorSeleccionado;

        Mostrar(Acciones.contratoSeleccionado);
        cargar_datos_contrato_seleccionado(ID_CONTRATO, ID_EMPLEADO, ID_REQUERIMIENTO);

        Panel_SELECCION_IMPRESION_CONTRATO.Visible = false;

    }

    protected void GridView_CONTRATOS_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_CONTRATOS.PageIndex = e.NewPageIndex;

        Decimal ID_SOLICITUD = Convert.ToDecimal(Label_ID_SOLICITUD.Text);

        registroContrato _registroContrato = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaContratos = _registroContrato.ObtenerContratosPorIdSolicitud(ID_SOLICITUD);

        Cargar(tablaContratos);
    }
    protected void Button_CLAUSULAS_Click(object sender, EventArgs e)
    {
        Decimal ID_CONTRATO = Convert.ToDecimal(HiddenField_ID_CONTRATO.Value);

        registroContrato _registroContrato = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoClausulas = _registroContrato.ObtenerInfoParaImprimirClausulas(Convert.ToDecimal(HiddenField_ID_CONTRATO.Value));

        if (tablaInfoClausulas.Rows.Count <= 0)
        {
            if (_registroContrato.MensajeError != null)
            {
                Informar(Panel_MENSAJE_IMPRESIONES_BASICAS, Label_MENSAJE_IMPRESIONES_BASICAS, _registroContrato.MensajeError, Proceso.Error);
            }
            else
            {
                DataTable tablaCon = _registroContrato.ObtenerConRegContratosPorRegistro(Convert.ToInt32(ID_CONTRATO));
                DataRow fila = tablaCon.Rows[0];
                _registroContrato.ActualizarConRegContratosImpresos(Convert.ToInt32(ID_CONTRATO), fila["CONTRATO_IMPRESO"].ToString(), "S");

                Informar(Panel_MENSAJE_IMPRESIONES_BASICAS, Label_MENSAJE_IMPRESIONES_BASICAS, "ADVERTENCIA: No existen clausulas para este contrato y perfil, puede continuar, con los siguientes tramites.", Proceso.Error);
            }
        }
        else
        {
            tools _tools = new tools();

            DataTable tablaCon = _registroContrato.ObtenerConRegContratosPorRegistro(Convert.ToInt32(ID_CONTRATO));
            DataRow fila = tablaCon.Rows[0];
            _registroContrato.ActualizarConRegContratosImpresos(Convert.ToInt32(ID_CONTRATO), fila["CONTRATO_IMPRESO"].ToString(), "S");

            StreamReader archivo_original = new StreamReader(Server.MapPath(@"~\plantillas_reportes\clausulas.htm"));

            String html_clausula = archivo_original.ReadToEnd();

            archivo_original.Dispose();
            archivo_original.Close();

            String html_completo = "<html><body>";

            Int32 contadorClausulas = 0;

            foreach (DataRow filaClausula in tablaInfoClausulas.Rows)
            {
                if (contadorClausulas == 0)
                {
                    html_completo = html_clausula;
                }
                else
                {
                    html_completo += "<div>linea para paginacion de pdf</div>";
                    html_completo += html_clausula;
                }

                if (Session["idEmpresa"].ToString() == "1")
                {
                    html_completo = html_completo.Replace("[NOMBRE_EMPRESA]", tabla.VAR_NOMBRE_SERTEMPO);
                }
                else
                {
                    html_completo = html_completo.Replace("[NOMBRE_EMPRESA]", tabla.VAR_NOMBRE_EYS);
                }
                html_completo = html_completo.Replace("[NOMBRE_TRABAJADOR]", filaClausula["NOMBRES"].ToString().Trim() + " " + filaClausula["APELLIDOS"].ToString().Trim());
                html_completo = html_completo.Replace("[NOMBRE_CLAUSULA]", filaClausula["NOMBRE"].ToString().Trim());
                html_completo = html_completo.Replace("[ENCABEZADO_CLAUSULA]", filaClausula["ENCABEZADO"].ToString().Trim());
                html_completo = html_completo.Replace("[CONTENIDO_CLAUSULA]", filaClausula["DESCRIPCION"].ToString().Trim());
                html_completo = html_completo.Replace("[DIAS]", DateTime.Now.Day.ToString());
                html_completo = html_completo.Replace("[MES]", _tools.obtenerNombreMes(DateTime.Now.Month));
                html_completo = html_completo.Replace("[ANNO]", DateTime.Now.Year.ToString());

                contadorClausulas += 1;
            }

            html_completo += "</body></html>";

            String filename = "clausulas_contrato_" + ID_CONTRATO.ToString();

            HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;FileName=" + filename + ".pdf");

            Response.Clear();
            Response.ContentType = "application/pdf";


            iTextSharp.text.Document document = new iTextSharp.text.Document(new Rectangle(595, 842), 40, 40, 80, 40);

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
            PageEventHandler.tipoDocumento = "clausula";

            document.Open();

            String tempFile = Path.GetTempFileName();

            using (StreamWriter tempwriter = new StreamWriter(tempFile, false))
            {
                tempwriter.Write(html_completo);
            }

            List<IElement> htmlarraylist = HTMLWorker.ParseToList(new StreamReader(tempFile), new StyleSheet());

            foreach (IElement element in htmlarraylist)
            {
                if (element.Chunks.Count > 0)
                {
                    if (element.Chunks[0].Content == "linea para paginacion de pdf")
                    {
                        document.NewPage();
                    }
                    else
                    {
                        document.Add(element);
                    }
                }
                else
                {
                    document.Add(element);
                }
            }

            document.Close();
            writer.Close();

            Response.End();

            File.Delete(tempFile);
        }
    }
    protected void Button_ENTREGAS_Click(object sender, EventArgs e)
    {

    }
    protected void Button_AUTOS_Click(object sender, EventArgs e)
    {
        Imprimir(Impresiones.AutosRecomendacion);

    }
    protected void Button_CONTRATO_Click(object sender, EventArgs e)
    {
        Panel_SELECCION_IMPRESION_CONTRATO.Visible = true;

        RadioButtonList_impresion_contrato.ClearSelection();
        CheckBox_ConCarnet.Checked = false;

    }
    
    private void previsualizar_clausula(Decimal ID_CON_REG_CLAUSULAS_PERFIL)
    {
        condicionesContratacion _condicionesContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoClausula = _condicionesContratacion.obtenerClausulasPorIdCluasula(ID_CON_REG_CLAUSULAS_PERFIL);
        DataRow filaInfo = tablaInfoClausula.Rows[0];

        StreamReader archivo = new StreamReader(Server.MapPath(@"~\plantillas_reportes\clausulas.htm"));

        tools _tools = new tools();

        String html_completo = "<html><body>";
        html_completo += archivo.ReadToEnd();
        html_completo += "</body></html>";

        archivo.Dispose();
        archivo.Close();

        if (Session["idEmpresa"].ToString() == "1")
        {
            html_completo = html_completo.Replace("[NOMBRE_EMPRESA]", tabla.VAR_NOMBRE_SERTEMPO);
        }
        else
        {
            html_completo = html_completo.Replace("[NOMBRE_EMPRESA]", tabla.VAR_NOMBRE_EYS);
        }
        html_completo = html_completo.Replace("[NOMBRE_TRABAJADOR]", "NOMBRE DEL TRABAJADOR");
        html_completo = html_completo.Replace("[NOMBRE_CLAUSULA]", filaInfo["NOMBRE"].ToString());
        html_completo = html_completo.Replace("[ENCABEZADO_CLAUSULA]", filaInfo["ENCABEZADO"].ToString());
        html_completo = html_completo.Replace("[CONTENIDO_CLAUSULA]", filaInfo["DESCRIPCION"].ToString());

        usuario _usuario = new usuario(Session["idEmpresa"].ToString());
        DataTable tablaInfoUsuario = _usuario.ObtenerInicioSesionPorUsuLog(Session["USU_LOG"].ToString());
        if (tablaInfoUsuario.Rows.Count <= 0)
        {
            html_completo = html_completo.Replace("[CIUDAD_FIRMA]", "Desconocida");
        }
        else
        {
            DataRow filaInfoUsuario = tablaInfoUsuario.Rows[0];
            html_completo = html_completo.Replace("[CIUDAD_FIRMA]", filaInfoUsuario["NOMBRE_CIUDAD"].ToString());
        }

        html_completo = html_completo.Replace("[DIAS]", DateTime.Now.Day.ToString());
        html_completo = html_completo.Replace("[MES]", _tools.obtenerNombreMes(DateTime.Now.Month));
        html_completo = html_completo.Replace("[ANNO]", DateTime.Now.Year.ToString());

        String filename = "previsulizador_clausula";

        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;FileName=" + filename + ".pdf");

        Response.Clear();
        Response.ContentType = "application/pdf";

        iTextSharp.text.Document document = new iTextSharp.text.Document(PageSize.A4, 40, 40, 80, 40);

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
        PageEventHandler.tipoDocumento = "clausula";

        document.Open();

        String tempFile = Path.GetTempFileName();

        using (StreamWriter tempwriter = new StreamWriter(tempFile, false))
        {
            tempwriter.Write(html_completo);
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
    protected void GridView_CONCEPTOS_FIJOS_RowCommand(object sender, GridViewCommandEventArgs e)
    { 
        int filaSeleccionada = Convert.ToInt32(e.CommandArgument);

        Decimal ID_CON_REG_CLAUSULAS_PERFIL;

        ID_CON_REG_CLAUSULAS_PERFIL = Convert.ToDecimal(GridView_CONCEPTOS_FIJOS.DataKeys[filaSeleccionada].Values["ID_CLAUSULA"]);

        if (e.CommandName == "ver")
        {
            previsualizar_clausula(ID_CON_REG_CLAUSULAS_PERFIL);
        }
    }

    protected void GridView_PAGOS_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int filaSeleccionada = Convert.ToInt32(e.CommandArgument);

        poner_color_a_grilla_pagos();

        Decimal ID_LIQ_NOMINA_EMPLEADOS;

        ID_LIQ_NOMINA_EMPLEADOS = Convert.ToDecimal(GridView_PAGOS.DataKeys[filaSeleccionada].Values["CODIGO"]);

        if (e.CommandName == "Desprendible")
        {
            String uspReporte = "Desprendibles_De_Pago_web '" + ID_LIQ_NOMINA_EMPLEADOS + "'";
            String nombreReporte = "RPT_Desprendible_de_pago";
            String Ruta_Reporte = "~/Reportes/Nomina/" + nombreReporte + ".rpt";
            GenerarReporte(uspReporte, nombreReporte, Ruta_Reporte);     
        }

    }
    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    private void ocultar_mensaje(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.Style.Add("display", "none");
        panel_mensaje.Style.Add("display", "none");

        panel_fondo.Visible = false;
        panel_mensaje.Visible = false;
    }
    protected void GridView_PAGOS_SelectedIndexChanged(object sender, EventArgs e)
    {
        Decimal ID_LIQ_NOMINA_EMPLEADOS = Convert.ToDecimal(GridView_PAGOS.SelectedDataKey["CODIGO"]);
        poner_color_a_grilla_pagos();
        GridView_PAGOS.SelectedRow.BackColor = colorSeleccionado;

        Mostrar(Acciones.Pagos);
        cargar_detalle_del_pago(ID_LIQ_NOMINA_EMPLEADOS);
    }
    protected void image_cerrar_detalle_Click(object sender, ImageClickEventArgs e)
    {
        PNL_DETALLE_PAGO.Visible = false;
    }
    protected void image_cerrar_detalle_Click1(object sender, ImageClickEventArgs e)
    {
        PNL_DETALLE_PAGO.Visible = false;
    }

    protected void GridView_PAGOS_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_PAGOS.PageIndex = e.NewPageIndex;
        Decimal ID_CONTRATO = Convert.ToDecimal(GridView_CONTRATOS.SelectedDataKey["ID_CONTRATO"]);
        cargar_pagos(ID_CONTRATO);
    }
    protected void Button_solicitud_examenes_Click(object sender, EventArgs e)
    {

    }
    protected void Button_APERTURA_CUENTA_Click(object sender, EventArgs e)
    {

    }


    protected void Cert_Laboral_Click(object sender, EventArgs e)
    {
        String @CEDULA = this.HiddenField_ID_CONTRATO.Value;
        String uspReporte = "CERTIFICADO_LABORAL_SISER_WEB '" + @CEDULA + "'";
        String nombreReporte = "RPT_GESTIONHUMANA_CERTIFICACION_LABORAL";
        String Ruta_Reporte = "~/Reportes/gestionHumana/" + nombreReporte + ".rpt";
        GenerarReporte(uspReporte, nombreReporte,Ruta_Reporte);
    }

    public void GenerarReporte(String uspReporte,String nombreReporte, String Ruta_Reporte)
    {
        Pagos _Pagos = new Pagos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());


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

                    reporte.Load(Server.MapPath(Ruta_Reporte));
                    reporte.SetDataSource(ds.Tables[0]);
                    reporte.DataSourceConnections[0].SetConnection(server, db, user, pass);

                    if (this.CheckBox_Exell.Checked == true)
                    {
                        using (var mStream = (MemoryStream)reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.ExcelRecord))
                        {
                            Response.AddHeader("Content-Disposition", "attachment;FileName=" + nombreReporte + ".xls");
                            Response.Clear();
                            Response.Buffer = true;
                            Response.ContentType = "application/vnd.ms-excel";
                            Response.BinaryWrite(mStream.ToArray());
                        }
                        Response.End();
                    } 

                    else
                    {
                        using (var mStream = (MemoryStream)reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat))
                        {
                            Response.AddHeader("Content-Disposition", "attachment;FileName=" + nombreReporte + ".pdf");
                            Response.Clear();
                            Response.Buffer = true;
                            Response.ContentType = "application/pdf";
                            Response.BinaryWrite(mStream.ToArray());
                        }
                        Response.End();
                    } 

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

    private void ImprimirContratoO_L_COMPLETO(DataRow filaInfoContrato)
    {
        tools _tools = new tools();

        Boolean CarnetIncluido = CheckBox_ConCarnet.Checked;

        StreamReader archivo;

        if (Session["idEmpresa"].ToString() == "1")
        {
            if (CarnetIncluido == true)
            {
                archivo = new StreamReader(Server.MapPath(@"~\plantillas_reportes\contrato_sertempo_obra_labor.htm"));
            }
            else
            {
                archivo = new StreamReader(Server.MapPath(@"~\plantillas_reportes\contrato_sertempo_obra_labor_carnet_aparte.htm"));
            }
        }
        else
        {
            if (CarnetIncluido == true)
            {
                archivo = new StreamReader(Server.MapPath(@"~\plantillas_reportes\contrato_eys_labor_contratada.htm"));
            }
            else
            {
                archivo = new StreamReader(Server.MapPath(@"~\plantillas_reportes\contrato_eys_labor_contratada_carnet_aparte.htm"));
            }
        }


        String html = archivo.ReadToEnd();

        archivo.Dispose();
        archivo.Close();

        String filename;

        if (Session["idEmpresa"].ToString() == "1")
        {
            html = html.Replace("[DIR_LOGO_EMPLEADOR]", tabla.DIR_IMAGENES_PARA_PDF + "/logo_sertempo.png");
            html = html.Replace("[MENSAJE_LOGO]", "SERVICIOS TEMPORALES PROFESIONALES");
            html = html.Replace("[NOMBRE_EMPLEADOR]", tabla.VAR_NOMBRE_SERTEMPO);
            html = html.Replace("[DOMICILO_EMPLEADOR]", tabla.VAR_DOMICILIO_SERTEMPO);
            html = html.Replace("[DESCRIPCION_CARGO]", filaInfoContrato["DSC_FUNCIONES"].ToString().Trim().ToUpper());
            html = html.Replace("[SERVICIO_RESPECTIVO]", filaInfoContrato["DESCRIPCION"].ToString().Trim().ToUpper());
            html = html.Replace("[EMPRESA_USUARIA]", filaInfoContrato["RAZ_SOCIAL"].ToString().Trim().ToUpper());
            html = html.Replace("[DESCRIPCION_SALARIO]", filaInfoContrato["DESCRIPCION_SALARIO"].ToString().Trim().ToUpper());
            html = html.Replace("[DIR_FIRMA_EMPLEADOR]", tabla.DIR_IMAGENES_PARA_PDF + "/firma_autos_recomendacion.jpg");
            filename = "contrato_sertempo";
        }
        else
        {
            html = html.Replace("[DIR_LOGO_EMPLEADOR]", tabla.DIR_IMAGENES_PARA_PDF + "/logo_eficiencia.jpg");
            html = html.Replace("[NOMBRE_EMPLEADOR]", tabla.VAR_NOMBRE_EYS);
            html = html.Replace("[DOMICILO_EMPLEADOR]", tabla.VAR_DOMICILIO_EYS);
            html = html.Replace("[FUNCION_CARGO]", filaInfoContrato["DSC_FUNCIONES"].ToString().Trim().ToUpper());
            html = html.Replace("[ACTIVIDAD_CONTRATADA]", filaInfoContrato["DSC_FUNCIONES"].ToString().Trim().ToUpper());
            html = html.Replace("[EMPRESA_DESTACA]", filaInfoContrato["RAZ_SOCIAL"].ToString().Trim().ToUpper());
            html = html.Replace("[DESCRIPCION_SALARIO]", filaInfoContrato["DESCRIPCION_SALARIO"].ToString().Trim().ToUpper());
            html = html.Replace("[DIR_FIRMA_EMPLEADOR]", tabla.DIR_IMAGENES_PARA_PDF + "/firma_autos_recomendacion.jpg");
            filename = "contrato_eys";
        }


        html = html.Replace("[CARGO_TRABAJADOR]", filaInfoContrato["NOM_OCUPACION"].ToString().Trim().ToUpper());
        html = html.Replace("[NOMBRE_TRABAJADOR]", filaInfoContrato["APELLIDOS"].ToString().Trim().ToUpper() + " " + filaInfoContrato["NOMBRES"].ToString().Trim().ToUpper());
        html = html.Replace("[TIPO_DOCUMENTO_IDENTIDAD]", filaInfoContrato["TIP_DOC_IDENTIDAD"].ToString().Trim().ToUpper());
        html = html.Replace("[DOC_IDENTIFICACION]", filaInfoContrato["NUM_DOC_IDENTIDAD"].ToString().Trim().ToUpper());
        html = html.Replace("[SALARIO]", String.Format("$ {0:N2}", Convert.ToDecimal(filaInfoContrato["SALARIO"]).ToString()));
        html = html.Replace("[PERIODO_PAGO]", filaInfoContrato["periodo_pago"].ToString().Trim());
        html = html.Replace("[FECHA_INICIACION]", Convert.ToDateTime(filaInfoContrato["FECHA_INICIA"]).ToLongDateString());
        html = html.Replace("[CARNE_VALIDO_HASTA]", Convert.ToDateTime(filaInfoContrato["FECHA_TERMINA"]).ToLongDateString());

        usuario _usuario = new usuario(Session["idEmpresa"].ToString());
        DataTable tablaInfoUsuario = _usuario.ObtenerInicioSesionPorUsuLog(Session["USU_LOG"].ToString());
        if (tablaInfoUsuario.Rows.Count <= 0)
        {
            html = html.Replace("[CIUDAD_FIRMA]", "Desconocida");
        }
        else
        {
            DataRow filaInfoUsuario = tablaInfoUsuario.Rows[0];
            html = html.Replace("[CIUDAD_FIRMA]", filaInfoUsuario["NOMBRE_CIUDAD"].ToString());
        }

        DateTime fechaHoy = DateTime.Now;
        html = html.Replace("[DIAS_FIRMA]", fechaHoy.Day.ToString());
        html = html.Replace("[MES_FIRMA]", _tools.obtenerNombreMes(fechaHoy.Month));
        html = html.Replace("[ANNO_FIRMA]", fechaHoy.Year.ToString());

        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;FileName=" + filename + ".pdf");

        Response.Clear();
        Response.ContentType = "application/pdf";


        iTextSharp.text.Document document = new iTextSharp.text.Document(new Rectangle(612, 936), 15, 15, 9, 9);

        iTextSharp.text.pdf.PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);

        pdfEvents PageEventHandler = new pdfEvents();
        writer.PageEvent = PageEventHandler;

        PageEventHandler.tipoDocumento = "contrato";

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

    protected void Button_ImprimrContrato1_Click(object sender, EventArgs e)
    {
        if (!(String.IsNullOrEmpty(HiddenField_ID_CONTRATO.Value)))
        {
            registroContrato _registroContrato = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaInfoContrato = _registroContrato.ObtenerInfoParaImprimirContrato(Convert.ToDecimal(HiddenField_ID_CONTRATO.Value));
            NumToLetra numToLetra = new NumToLetra();
            string salario_en_letras = ""; 

            if (tablaInfoContrato.Rows.Count <= 0)
            {
                if (_registroContrato.MensajeError != null)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información para generar el contrato.", Proceso.Error);
                }
                else
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _registroContrato.MensajeError, Proceso.Error);
                }
            }
            else
            {
                String uspReporte = "";
                String nombreReporte = "";
                string nombre_empresa = null;
                string nit_empresa = null;
                string telefono_empresa = null;
                string direccion_empresa = null;

                if (Session["idEmpresa"].ToString() == "1")
                {
                    nombre_empresa = tabla.VAR_NOMBRE_SERTEMPO;
                    nit_empresa = tabla.VAR_NIT_SERTEMPO;
                    telefono_empresa = tabla.VAR_TELEFONO_SERTEMPO;
                    direccion_empresa = tabla.VAR_DOMICILIO_SERTEMPO;
                }
                else
                {
                    if (Session["idEmpresa"].ToString() == "3")
                    {
                        nombre_empresa = tabla.VAR_NOMBRE_EYS;
                        nit_empresa = tabla.VAR_NIT_EYS;
                        telefono_empresa = tabla.VAR_TELEFONO_EYS;
                        direccion_empresa = tabla.VAR_DOMICILIO_EYS;
                    }
                }

                DataRow filaInfoContrato = tablaInfoContrato.Rows[0];

                salario_en_letras = numToLetra.Convertir(Convert.ToDecimal(filaInfoContrato["SALARIO"]).ToString(), true);

                if (filaInfoContrato["PRACTICANTE_UNIVERSITARIO"].ToString().ToUpper() == "S")
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se puede reimprimir el contrato de aprendizaje.", Proceso.Advertencia);
                }
                else if ((filaInfoContrato["SENA_ELECTIVO"].ToString().ToUpper() == "S") || (filaInfoContrato["SENA_LEC"].ToString().ToUpper() == "S") || (filaInfoContrato["SENA_PRO"].ToString().ToUpper() == "S") || (filaInfoContrato["SENA_PRODUCTIVO"].ToString().ToUpper() == "S"))
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se puede reimprimir el contrato de aprendizaje.", Proceso.Advertencia);
                }
                else if (filaInfoContrato["CLASE_CONTRATO"].ToString().ToUpper().Trim() == ClaseContrato.I.ToString())
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se puede reimprimir el contrato integral.", Proceso.Advertencia);
                }
                else
                {
                    if (filaInfoContrato["CLASE_CONTRATO"].ToString().ToUpper().Trim() == ClaseContrato.L_C_C_D_A.ToString())
                    {
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se puede reimprimir el contrato.", Proceso.Advertencia);
                    }
                    else
                    {
                        if (filaInfoContrato["CLASE_CONTRATO"].ToString().ToUpper().Trim() == ClaseContrato.L_S_C_D_A.ToString())
                        {
                        }
                        else
                        {
                            if (filaInfoContrato["CLASE_CONTRATO"].ToString().ToUpper().Trim() == ClaseContrato.O_L.ToString())
                            {

                                if (RadioButtonList_impresion_contrato.SelectedValue == "COMPLETO")
                                {
                                    ImprimirContratoO_L_COMPLETO(filaInfoContrato);
                                }
                                else
                                {
                                    if (RadioButtonList_impresion_contrato.SelectedValue == "PREIMPRESO")
                                    {
                                        uspReporte = "usp_con_reg_contratos_ObtenerInfo_para_imprimir_contrato " + filaInfoContrato["REGISTRO_CONTRATO"].ToString();
                                        nombreReporte = "RPT_CONTRATACION_OBRA_LABOR_PREIMPRESO.rpt "; 
                                    }
                                }
                            }
                            else
                            {
                                if (filaInfoContrato["CLASE_CONTRATO"].ToString().ToUpper().Trim() == ClaseContrato.T_F.ToString())
                                {
                                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se puede reimprimir el contrato.", Proceso.Advertencia);
                                }
                                else
                                {
                                    if (filaInfoContrato["CLASE_CONTRATO"].ToString().ToUpper().Trim() == ClaseContrato.IN.ToString())
                                    {
                                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se puede reimprimir el contrato integral.", Proceso.Advertencia);
                                    }
                                }
                            }
                        }
                    }
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
        }
        else
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El contrato no ha sido guardado. Debe diligenciar todos los datos de contrato y luego Guardarlos, para poder imprimir el correspondiente documento.", Proceso.Advertencia);
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
                    + ", " + Label_ID_REQUERIMIENTO.Text
                    + ", " + HiddenField_ID_SOLICITUD.Value
                    + ", " + HiddenField_ID_PERFIL.Value;
                nombreReporte = "RPT_CONTRATACION_ORDEN_EXAMEN.rpt";
                break;
            case Impresiones.AutosRecomendacion:
                uspReporte = "RPT_CONTRATACION_AUTOS_RECOMENDACION "
                    + Session["idEmpresa"].ToString()
                    + ", " + Label_ID_REQUERIMIENTO.Text
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

    protected void Button_solicitud_examenes_Click1(object sender, EventArgs e)
    {
        Imprimir(Impresiones.OrdenExamen);
    }
}
