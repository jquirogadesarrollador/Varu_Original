using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brainsbits.LLB.seleccion;
using System.Data;
using System.Web;
using System.Collections.Generic;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB;
using System.IO;
using Brainsbits.LLB.seguridad;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using TSHAK.Components;



public partial class seleccion_referencias : System.Web.UI.Page
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
        Inicio = 0,
        CargarSolicitud,
        CargarReferencia,
        NuevaReferencia,
        ModificarReferencia,
        SeleccionTipoReferencia
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }

    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = false;
                Panel_FORM_BOTONES_ENCABEZADO.Visible = false;
                Button_NUEVO.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_IMPRIMIR.Visible = false;

                Panel_RESULTADOS_GRID.Visible = false;

                Panel_DATOS_CANDIDATO.Visible = false;

                Panel_SeleccionTipoReferencia.Visible = false;

                Panel_FORMULARIO.Visible = false;
                Panel_CONTROL_REGISTRO.Visible = false;
                Panel_INFORMAION_REFERENCIA.Visible = false;
                Panel_TIPO_REFERENCIA.Visible = false;
                Panel_PREGUNTAS.Visible = false;

                Panel_Calificaciones.Visible = false;

                Panel_FORM_BOTONES_PIE.Visible = false;
                Button_NUEVO_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                Button_IMPRIMIR_1.Visible = false;
                break;
            case Acciones.ModificarReferencia:
                Button_NUEVO.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_IMPRIMIR.Visible = false;

                Panel_CONTROL_REGISTRO.Visible = false;

                Button_NUEVO_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                Button_IMPRIMIR_1.Visible = false;
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
            case Acciones.CargarSolicitud:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.CargarReferencia:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Button_NUEVO.Visible = true;
                Button_MODIFICAR.Visible = true;
                Button_IMPRIMIR.Visible = true;

                Panel_DATOS_CANDIDATO.Visible = true;

                Panel_SeleccionTipoReferencia.Visible = true;

                Panel_FORMULARIO.Visible = true;
                Panel_CONTROL_REGISTRO.Visible = true;
                Panel_INFORMAION_REFERENCIA.Visible = true;
                Panel_PREGUNTAS.Visible = true;

                Panel_Calificaciones.Visible = true;

                Panel_FORM_BOTONES_PIE.Visible = true;
                Button_NUEVO_1.Visible = true;
                Button_MODIFICAR_1.Visible = true;
                Button_IMPRIMIR_1.Visible = true;
                break;
            case Acciones.NuevaReferencia:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_DATOS_CANDIDATO.Visible = true;

                Panel_SeleccionTipoReferencia.Visible = true;

                Panel_FORMULARIO.Visible = true;
                Panel_INFORMAION_REFERENCIA.Visible = true;
                Panel_PREGUNTAS.Visible = true;

                Panel_Calificaciones.Visible = true;

                Panel_FORM_BOTONES_PIE.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
                break;
            case Acciones.ModificarReferencia:
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
                break;
            case Acciones.SeleccionTipoReferencia:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_DATOS_CANDIDATO.Visible = true;
                Panel_SeleccionTipoReferencia.Visible = true;

                Panel_FORM_BOTONES_PIE.Visible = true;
                Button_CANCELAR_1.Visible = true;
                break;
        }
    }

    private void Desactivar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                DropDownList_TipoConfirmacionReferencia.Enabled = false;
                
                TextBox_FCH_CRE.Enabled = false;
                TextBox_FCH_MOD.Enabled = false;
                TextBox_HOR_CRE.Enabled = false;
                TextBox_HOR_MOD.Enabled = false;
                TextBox_USU_CRE.Enabled = false;
                TextBox_USU_MOD.Enabled = false;
                
                DropDownList_TIPO_REFERENCIA.Enabled = false;
                TextBox_NOMBRE_INFORMANTE.Enabled = false;
                TextBox_CARGO_INFORMANTE.Enabled = false;
                TextBox_Nombrejefe.Enabled = false;
                TextBox_CargoJefe.Enabled = false;
                TextBox_EMPRESA_TRABAJO.Enabled = false;
                TextBox_Numero_Telefono.Enabled = false;
                TextBox_EmpresaTemporal.Enabled = false;
                TextBox_TelefonoEmpresaTemporal.Enabled = false;
                DropDownList_TipoContrato.Enabled = false;
                TextBox_FECHA_INGRESO.Enabled = false;
                TextBox_FECHA_RETIRO.Enabled = false;
                TextBox_ULTIMO_CARGO.Enabled = false;               
                TextBox_ULTIMO_SALARIO.Enabled = false;
                TextBox_Comisiones.Enabled = false;
                TextBox_Bonos.Enabled = false;
                TextBox_MotivoRetiro.Enabled = false;
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

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_BUSCAR.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("Número de documento", "NUMERO_DOCUMENTO");
        DropDownList_BUSCAR.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("Nombre", "NOMBRE");
        DropDownList_BUSCAR.Items.Add(item);

        DropDownList_BUSCAR.DataBind();
    }

    private void iniciar_seccion_de_busqueda()
    {
        TextBox_BUSCAR.Text = "";
        cargar_DropDownList_BUSCAR();
    }

    private void CargarGrillaPreguntasNuevasDesdeTabla(DataTable tablaPreguntas)
    {
        GridView_PREGUNTAS.DataSource = tablaPreguntas;
        GridView_PREGUNTAS.DataBind();

        for (int i = 0; i < GridView_PREGUNTAS.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_PREGUNTAS.Rows[i];
            DataRow filaTabla = tablaPreguntas.Rows[i];
            Label pregunta = filaGrilla.FindControl("Label_PREGUNTA") as Label;
            TextBox texto = filaGrilla.FindControl("TextBox_RESPUESTA") as TextBox;

            pregunta.Text = filaTabla["CONTENIDO"].ToString().Trim();
            texto.Text = "";
        }

    }

    private void CargarGrillaPreguntasNuevas()
    {
        Decimal ID_CATEGORIA = Convert.ToDecimal(DropDownList_TipoConfirmacionReferencia.SelectedValue);

        referencia _referencia = new referencia(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaPreguntas = _referencia.ObtenerPreguntasActivas(ID_CATEGORIA);

        if (tablaPreguntas.Rows.Count <= 0)
        {
            if (_referencia.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _referencia.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se han configurado Preguntas para la confirmación de referencia Laboral para cargos de tipo (" + DropDownList_TipoConfirmacionReferencia.SelectedItem.Text + ").", Proceso.Advertencia);
            }

            GridView_PREGUNTAS.DataSource = null;
            GridView_PREGUNTAS.DataBind();
        }
        else
        {
            CargarGrillaPreguntasNuevasDesdeTabla(tablaPreguntas);
        }
    }

    private void CargarDropCalificaciones(DropDownList drop)
    {
        drop.Items.Clear();
        drop.Items.Add(new System.Web.UI.WebControls.ListItem("Seleccione...",""));

        drop.Items.Add(new System.Web.UI.WebControls.ListItem("EXCELENTE","EXCELENTE"));
        drop.Items.Add(new System.Web.UI.WebControls.ListItem("BUENO","BUENO"));
        drop.Items.Add(new System.Web.UI.WebControls.ListItem("REGULAR","REGULAR"));
        drop.Items.Add(new System.Web.UI.WebControls.ListItem("MALO","MALO"));

        drop.DataBind();

        drop.SelectedIndex = 0;
    }

    private void CargarGrillaC_desdetabla(DataTable tablaPar)
    {
        GridView_CalificacionesReferencia.DataSource = tablaPar;
        GridView_CalificacionesReferencia.DataBind();

        for (int i = 0; i < GridView_CalificacionesReferencia.Rows.Count; i++)
        {
            DataRow filaTabla = tablaPar.Rows[i];
            GridViewRow filaGrilla = GridView_CalificacionesReferencia.Rows[i];

            Label label_Cualidad = filaGrilla.FindControl("Label_Cualidad") as Label;
            DropDownList drop_Calificaciones = filaGrilla.FindControl("DropDownList_Calificacion") as DropDownList;

            label_Cualidad.Text = filaTabla["CODIGO"].ToString().Trim();

            CargarDropCalificaciones(drop_Calificaciones);
        }
    }

    private void CargarGrillaCualidades()
    {
        parametro _par = new parametro(Session["idEmpresa"].ToString());
        DataTable tablaPar = _par.ObtenerParametrosPorTabla(tabla.PARAMETROS_CALIFICACIONES_REFERENCIA);

        if (tablaPar.Rows.Count <= 0)
        {
            if (_par.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _par.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se han parametrizado CUALIDADES para evaluar.", Proceso.Advertencia);
            }

            GridView_CalificacionesReferencia.DataSource = null;
            GridView_CalificacionesReferencia.DataBind();
        }
        else
        {

            CargarGrillaC_desdetabla(tablaPar);
        }

        Decimal ID_CATEGORIA = Convert.ToDecimal(DropDownList_TipoConfirmacionReferencia.SelectedValue);

        referencia _referencia = new referencia(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaPreguntas = _referencia.ObtenerPreguntasActivas(ID_CATEGORIA);

        if (tablaPreguntas.Rows.Count <= 0)
        {
            if (_referencia.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _referencia.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se han configurado Preguntas para la confirmación de referencia Laboral para cargos de tipo (" + DropDownList_TipoConfirmacionReferencia.SelectedItem.Text + ").", Proceso.Advertencia);
            }

            GridView_PREGUNTAS.DataSource = null;
            GridView_PREGUNTAS.DataBind();
        }
        else
        {
            CargarGrillaPreguntasNuevasDesdeTabla(tablaPreguntas);
        }
    }

    private void ActivarFilasGrilla(GridView grilla)
    {
        for (int i = 0; i < grilla.Rows.Count; i++)
        {
            for (int j = 0; j < grilla.Columns.Count; j++)
            {
                grilla.Rows[i].Cells[j].Enabled = true;
            }
        }
    }

    private void cargar_DropDownList_TipoConfirmacionReferencia()
    {
        referencia _ref = new referencia(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCategoriasReferencias = _ref.ObtenerCategoriasActivas();

        DropDownList_TipoConfirmacionReferencia.Items.Clear();

        DropDownList_TipoConfirmacionReferencia.Items.Add(new System.Web.UI.WebControls.ListItem("Seleccione...", ""));

        if (tablaCategoriasReferencias.Rows.Count <= 0)
        {
            if (_ref.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _ref.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información sobre Confirmación de Referencias, El Usuario encargado debe realizar la Adminitración de Confirmación de Referencias (Categorías y preguntas).", Proceso.Advertencia);
            }
        }
        else
        {
            foreach (DataRow filaTabla in tablaCategoriasReferencias.Rows)
            {
                DropDownList_TipoConfirmacionReferencia.Items.Add(new System.Web.UI.WebControls.ListItem(filaTabla["NOMBRE_CAT"].ToString().Trim(), filaTabla["ID_CATEGORIA"].ToString()));
            }
        }

        DropDownList_TipoConfirmacionReferencia.DataBind();
    }

    private void Cargar_DropDownList_TipoContrato()
    {
        DropDownList_TipoContrato.Items.Clear();

        DropDownList_TipoContrato.Items.Add(new System.Web.UI.WebControls.ListItem("Seleccione...",""));

        DropDownList_TipoContrato.Items.Add(new System.Web.UI.WebControls.ListItem("OBRA O LABOR", "OBRA O LABOR"));
        DropDownList_TipoContrato.Items.Add(new System.Web.UI.WebControls.ListItem("FIJO", "FIJO"));
        DropDownList_TipoContrato.Items.Add(new System.Web.UI.WebControls.ListItem("INDEFINIDO", "INDEFINIDO"));
        DropDownList_TipoContrato.Items.Add(new System.Web.UI.WebControls.ListItem("CONVENIO APRENDIZAJE", "CONVENIO APRENDIZAJE"));

        DropDownList_TipoContrato.DataBind();
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

                HiddenField_ARCHIVO.Value = "";
                HiddenField_ID_REQUERIMIENTO.Value = "";
                break;
            case Acciones.NuevaReferencia:
                Cargar_DropDownList_TipoContrato();

                CargarGrillaPreguntasNuevas();

                ActivarFilasGrilla(GridView_PREGUNTAS);

                CargarGrillaCualidades();

                ActivarFilasGrilla(GridView_CalificacionesReferencia);
                break;
            case Acciones.CargarReferencia:
                Cargar_DropDownList_TipoContrato();

                cargar_DropDownList_TipoConfirmacionReferencia();
                break;
            case Acciones.SeleccionTipoReferencia:
                cargar_DropDownList_TipoConfirmacionReferencia();
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
        configurar_paneles_popup(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    private void Iniciar()
    {
        Configurar();
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = "CONFIRMACIÓN REFERENCIAS LABORALES";

        if (!IsPostBack)
        {
            Iniciar();
        }
    }
    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.ModificarReferencia);
        Mostrar(Acciones.ModificarReferencia);
        Activar(Acciones.ModificarReferencia);

        ActivarFilasGrilla(GridView_PREGUNTAS);
        ActivarFilasGrilla(GridView_CalificacionesReferencia);

        HiddenField_TIPO_ACTUALIZACION_REFERENCIA.Value = referencia.TiposActualizacionReferencia.Actualizacion.ToString();
    }

    private void Guardar()
    {
        Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);
        String ARCHIVO = HiddenField_ARCHIVO.Value;
        Decimal ID_REQUERIMIENTO = 0;
        try
        {
            ID_REQUERIMIENTO = Convert.ToDecimal(HiddenField_ID_REQUERIMIENTO.Value);
        }
        catch
        {
            ID_REQUERIMIENTO = 0;
        }

        String NOMBRE_INFORMANTE = TextBox_NOMBRE_INFORMANTE.Text.Trim();
        String CARGO_INFORMANTE = TextBox_CARGO_INFORMANTE.Text.Trim();

        String NOMBRE_JEFE = TextBox_Nombrejefe.Text.Trim();
        String CARGO_JEFE = TextBox_CargoJefe.Text.Trim();

        String EMPRESA_TRABAJO = TextBox_EMPRESA_TRABAJO.Text.Trim();
        String NUM_TELEFONO = TextBox_Numero_Telefono.Text.Trim();
        String EMPRESA_TEMPORAL = TextBox_EmpresaTemporal.Text.Trim();
        String TELEFONO_TEMPORAL = TextBox_TelefonoEmpresaTemporal.Text.Trim();

        String TIPO_CONTRATO = DropDownList_TipoContrato.SelectedValue;
        DateTime FECHA_INGRESO = new DateTime();
        try
        {
            FECHA_INGRESO = Convert.ToDateTime(TextBox_FECHA_INGRESO.Text);
        }
        catch
        {
            FECHA_INGRESO = new DateTime();
        }
            
        DateTime FECHA_RETIRO = new DateTime();
        try
        {
            FECHA_RETIRO = Convert.ToDateTime(TextBox_FECHA_RETIRO.Text);
        }
        catch
        {
            FECHA_RETIRO = new DateTime();
        }

        String ULTIMO_CARGO = TextBox_ULTIMO_CARGO.Text.Trim();
        
        Decimal ULTIMO_SALARIO = Convert.ToDecimal(TextBox_ULTIMO_SALARIO.Text);
        String COMISIONES = TextBox_Comisiones.Text.Trim();
        String BONOS = TextBox_Bonos.Text.Trim();

        String MOTIVO_RETIRO = TextBox_MotivoRetiro.Text.Trim();

        String VOLVER_A_CONTRATAR = null;

        String CUALIDADES_CALIFICACION = null;
        foreach (GridViewRow filaGrilla in GridView_CalificacionesReferencia.Rows)
        { 
            Label label_cualidad = filaGrilla.FindControl("Label_Cualidad") as Label;
            DropDownList drop_calificaciones = filaGrilla.FindControl("DropDownList_Calificacion") as DropDownList;

            if (CUALIDADES_CALIFICACION == null)
            {
                CUALIDADES_CALIFICACION = label_cualidad.Text + ":" + drop_calificaciones.SelectedValue;
            }
            else
            {
                CUALIDADES_CALIFICACION += ";" + label_cualidad.Text + ":" + drop_calificaciones.SelectedValue;
            }
        }

        Decimal ID_CATEGORIA = Convert.ToDecimal(DropDownList_TipoConfirmacionReferencia.SelectedValue);

        List<respuestaReferencia> listaRespuestas = new List<respuestaReferencia>();
        respuestaReferencia _respuestaParaLista;
        for (int i = 0; i < GridView_PREGUNTAS.Rows.Count; i++)
        {
            _respuestaParaLista = new respuestaReferencia();

            _respuestaParaLista.ID_PREGUNTA = Convert.ToDecimal(GridView_PREGUNTAS.DataKeys[i].Values["ID_PREGUNTA"]);
            _respuestaParaLista.ID_REFERENCIA = 0;
            _respuestaParaLista.ID_RESPUESTA = 0;

            TextBox respuesta = GridView_PREGUNTAS.Rows[i].FindControl("TextBox_RESPUESTA") as TextBox;
            _respuestaParaLista.RESPUESTA = respuesta.Text;

            listaRespuestas.Add(_respuestaParaLista);
        }

        referencia _referencia = new referencia(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        referencia.TiposActualizacionReferencia tipoActualizacion = (referencia.TiposActualizacionReferencia)Enum.Parse(typeof(referencia.TiposActualizacionReferencia), HiddenField_TIPO_ACTUALIZACION_REFERENCIA.Value);

        Decimal ID_REFERENCIA = 0;

        if (tipoActualizacion == referencia.TiposActualizacionReferencia.CreacionLimpia)
        {
            ID_REFERENCIA = _referencia.Adicionar(ID_SOLICITUD, EMPRESA_TRABAJO, FECHA_INGRESO, FECHA_RETIRO, ULTIMO_CARGO, NOMBRE_INFORMANTE, CARGO_INFORMANTE, ULTIMO_SALARIO, NUM_TELEFONO, VOLVER_A_CONTRATAR, null, null, ID_CATEGORIA, listaRespuestas, CUALIDADES_CALIFICACION, NOMBRE_JEFE, CARGO_JEFE, EMPRESA_TEMPORAL, TELEFONO_TEMPORAL, TIPO_CONTRATO, COMISIONES, BONOS, MOTIVO_RETIRO);
        }
        else
        { 
            ID_REFERENCIA = _referencia.AdicionarConHistorial(ID_SOLICITUD, EMPRESA_TRABAJO, FECHA_INGRESO, FECHA_RETIRO, ULTIMO_CARGO, NOMBRE_INFORMANTE, CARGO_INFORMANTE, ULTIMO_SALARIO, NUM_TELEFONO, VOLVER_A_CONTRATAR, null, null, listaRespuestas, ID_CATEGORIA, CUALIDADES_CALIFICACION, NOMBRE_JEFE, CARGO_JEFE, EMPRESA_TEMPORAL, TELEFONO_TEMPORAL, TIPO_CONTRATO, COMISIONES, BONOS, MOTIVO_RETIRO);
        }

        if (ID_REFERENCIA <= 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _referencia.MensajeError, Proceso.Error);
        }
        else
        {
            Cargar(ID_SOLICITUD, ID_REFERENCIA, ARCHIVO, ID_REQUERIMIENTO);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La confirmación de referencia laboral para el candidato: " + Label_NOMBRES.Text + ", fue creada correctamente", Proceso.Correcto);
        }
    }

    private void Modificar()
    {
        Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);
        Decimal ID_REFERENCIA = Convert.ToDecimal(HiddenField_ID_REFERENCIA.Value);

        String ARCHIVO = HiddenField_ARCHIVO.Value;
        Decimal ID_REQUERIMIENTO = 0;
        try
        {
            ID_REQUERIMIENTO = Convert.ToDecimal(HiddenField_ID_REQUERIMIENTO.Value);
        }
        catch
        {
            ID_REQUERIMIENTO = 0;
        }

        String NOMBRE_INFORMANTE = TextBox_NOMBRE_INFORMANTE.Text.Trim();
        String CARGO_INFORMANTE = TextBox_CARGO_INFORMANTE.Text.Trim();

        String NOMBRE_JEFE = TextBox_Nombrejefe.Text.Trim();
        String CARGO_JEFE = TextBox_CargoJefe.Text.Trim();

        String EMPRESA_TRABAJO = TextBox_EMPRESA_TRABAJO.Text.Trim();
        String NUM_TELEFONO = TextBox_Numero_Telefono.Text.Trim();
        String EMPRESA_TEMPORAL = TextBox_EmpresaTemporal.Text.Trim();
        String TELEFONO_TEMPORAL = TextBox_TelefonoEmpresaTemporal.Text.Trim();

        String TIPO_CONTRATO = DropDownList_TipoContrato.SelectedValue;

        DateTime FECHA_INGRESO = new DateTime();
        try
        {
            FECHA_INGRESO = Convert.ToDateTime(TextBox_FECHA_INGRESO.Text);
        }
        catch
        {
            FECHA_INGRESO = new DateTime();
        }

        DateTime FECHA_RETIRO = new DateTime();
        try
        {
            FECHA_RETIRO = Convert.ToDateTime(TextBox_FECHA_RETIRO.Text);
        }
        catch
        {
            FECHA_RETIRO = new DateTime();
        }

        String ULTIMO_CARGO = TextBox_ULTIMO_CARGO.Text.Trim();

        Decimal ULTIMO_SALARIO = Convert.ToDecimal(TextBox_ULTIMO_SALARIO.Text);
        String COMISIONES = TextBox_Comisiones.Text.Trim();
        String BONOS = TextBox_Bonos.Text.Trim();

        String MOTIVO_RETIRO = TextBox_MotivoRetiro.Text.Trim();

        String VOLVER_A_CONTRATAR = null;

        String CUALIDADES_CALIFICACION = null;
        foreach (GridViewRow filaGrilla in GridView_CalificacionesReferencia.Rows)
        {
            Label label_cualidad = filaGrilla.FindControl("Label_Cualidad") as Label;
            DropDownList drop_calificaciones = filaGrilla.FindControl("DropDownList_Calificacion") as DropDownList;

            if (CUALIDADES_CALIFICACION == null)
            {
                CUALIDADES_CALIFICACION = label_cualidad.Text + ":" + drop_calificaciones.SelectedValue;
            }
            else
            {
                CUALIDADES_CALIFICACION += ";" + label_cualidad.Text + ":" + drop_calificaciones.SelectedValue;
            }
        }

        List<respuestaReferencia> listaRespuestas = new List<respuestaReferencia>();
        respuestaReferencia _respuestaParaLista;
        for (int i = 0; i < GridView_PREGUNTAS.Rows.Count; i++)
        {
            _respuestaParaLista = new respuestaReferencia();

            _respuestaParaLista.ID_PREGUNTA = Convert.ToDecimal(GridView_PREGUNTAS.DataKeys[i].Values["ID_PREGUNTA"]);
            _respuestaParaLista.ID_REFERENCIA = ID_REFERENCIA;
            _respuestaParaLista.ID_RESPUESTA = Convert.ToDecimal(GridView_PREGUNTAS.DataKeys[i].Values["ID_RESPUESTA"]);

            TextBox respuesta = GridView_PREGUNTAS.Rows[i].FindControl("TextBox_RESPUESTA") as TextBox;
            _respuestaParaLista.RESPUESTA = respuesta.Text;

            listaRespuestas.Add(_respuestaParaLista);
        }

        referencia _referencia = new referencia(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean verificado = _referencia.Actualizar(ID_REFERENCIA, EMPRESA_TRABAJO, FECHA_INGRESO, FECHA_RETIRO, ULTIMO_CARGO, NOMBRE_INFORMANTE, CARGO_INFORMANTE, ULTIMO_SALARIO, NUM_TELEFONO, VOLVER_A_CONTRATAR, null, null, listaRespuestas, CUALIDADES_CALIFICACION, NOMBRE_JEFE, CARGO_JEFE, EMPRESA_TEMPORAL, TELEFONO_TEMPORAL, TIPO_CONTRATO, COMISIONES, BONOS, MOTIVO_RETIRO);

        if (verificado == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _referencia.MensajeError, Proceso.Error);
        }
        else
        {
            Cargar(ID_SOLICITUD, ID_REFERENCIA, ARCHIVO, ID_REQUERIMIENTO);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La Referencia Laboral para el candidato: " + Label_NOMBRES.Text + ", fue modificada correctamente", Proceso.Correcto);
        }
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(HiddenField_ID_REFERENCIA.Value) == true)
        {
            Guardar();
        }
        else
        {
            Modificar();
        }
    }

    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        if ((String.IsNullOrEmpty(HiddenField_ID_REFERENCIA.Value) == true) || (HiddenField_ID_REFERENCIA.Value == "0"))
        {
            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Cargar(Acciones.Inicio);
        }
        else
        {
            Decimal ID_REFERENCIA = 0;
            try
            {
                ID_REFERENCIA = Convert.ToDecimal(HiddenField_ID_REFERENCIA.Value);
            }
            catch
            {
                ID_REFERENCIA = 0;
            }

            Decimal ID_SOLICITUD = 0;
            try
            {
                ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);
            }
            catch
            {
                ID_SOLICITUD = 0;
            }
            
            String ARCHIVO = HiddenField_ARCHIVO.Value;

            Decimal ID_REQUERIMIENTO = 0;
            try
            {
                ID_REQUERIMIENTO = Convert.ToDecimal(HiddenField_ID_REQUERIMIENTO.Value);
            }
            catch
            {
                ID_REQUERIMIENTO = 0;
            }

            Cargar(ID_SOLICITUD, ID_REFERENCIA, ARCHIVO, ID_REQUERIMIENTO);
        }
    }

    protected void Button_IMPRIMIR_Click(object sender, EventArgs e)
    {
        Decimal ID_REFERENCIA = Convert.ToDecimal(HiddenField_ID_REFERENCIA.Value);
        Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);

        maestrasInterfaz _maestras = new maestrasInterfaz();

        Byte[] archivo = _maestras.GenerarPDFReferencia(ID_REFERENCIA, ID_SOLICITUD);

        String filename = "ConfirmacionReferencia";

        Response.Clear();
        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", "attachment;FileName=" + filename + ".pdf");
        Response.BinaryWrite(archivo);
        Response.End();
    }

    protected void DropDownList_BUSCAR_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (DropDownList_BUSCAR.SelectedValue)
        {
            case "":
                configurarCaracteresAceptadosBusqueda(true, true);
                break;
            case "NUMERO_DOCUMENTO":
                configurarCaracteresAceptadosBusqueda(false, true);
                break;
            case "NOMBRE":
                configurarCaracteresAceptadosBusqueda(true, false);
                break;
        }

        TextBox_BUSCAR.Focus();
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

    private void Buscar()
    {
        Ocultar(Acciones.Inicio);

        referencia _referencia = new referencia(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable _dataTable = new DataTable();
        switch (this.DropDownList_BUSCAR.SelectedValue)
        {
            case "NUMERO_DOCUMENTO":
                _dataTable = _referencia.ObtenerPorNumeroDocumento(this.TextBox_BUSCAR.Text);
                break;
            case "NOMBRE":
                _dataTable = _referencia.ObtenerPorNombre(this.TextBox_BUSCAR.Text);
                break;
        }

        if (_dataTable.Rows.Count > 0)
        {
            GridView_RESULTADOS_BUSQUEDA.DataSource = _dataTable;
            GridView_RESULTADOS_BUSQUEDA.DataBind();

            Mostrar(Acciones.CargarSolicitud);
        }
        else
        {
            if (!String.IsNullOrEmpty(_referencia.MensajeError))
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Consulte con el Administrador: " + _referencia.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información para " + this.DropDownList_BUSCAR.SelectedItem + " : " + this.TextBox_BUSCAR.Text, Proceso.Advertencia);
            }

            Mostrar(Acciones.Inicio);
        }

        _dataTable.Dispose();
    }
        
    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "CON_FILTRO";
        HiddenField_FILTRO_DROP.Value = DropDownList_BUSCAR.SelectedValue;
        HiddenField_FILTRO_DATO.Value = TextBox_BUSCAR.Text;

        HiddenField_ID_REFERENCIA.Value = "";
        HiddenField_ID_SOLICITUD.Value = "";
        HiddenField_ARCHIVO.Value = "";
        HiddenField_ID_REQUERIMIENTO.Value = "";

        Buscar();
    }
    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;

        Buscar();
    }

    private void Limpiar(Acciones accion)
    { 
        switch(accion)
        {
            case Acciones.NuevaReferencia:
                HiddenField_ID_REFERENCIA.Value = "";
                DropDownList_TIPO_REFERENCIA.Items.Clear();

                TextBox_NOMBRE_INFORMANTE.Text = "";
                TextBox_CARGO_INFORMANTE.Text = "";

                TextBox_Nombrejefe.Text = "";
                TextBox_CargoJefe.Text = "";

                TextBox_EMPRESA_TRABAJO.Text = "";
                TextBox_Numero_Telefono.Text = "";

                TextBox_EmpresaTemporal.Text = "";
                TextBox_TelefonoEmpresaTemporal.Text = "";

                DropDownList_TipoContrato.Items.Clear();

                TextBox_FECHA_INGRESO.Text = "";
                TextBox_FECHA_RETIRO.Text = "";

                TextBox_ULTIMO_CARGO.Text = "";

                TextBox_ULTIMO_SALARIO.Text = "";
                TextBox_Comisiones.Text = "";
                TextBox_Bonos.Text = "";

                TextBox_MotivoRetiro.Text = "";
                break;
        }
    }

    private void Activar(Acciones accion)
    {
        switch (accion)
        { 
            case Acciones.NuevaReferencia:
                DropDownList_TIPO_REFERENCIA.Enabled = true;
                TextBox_NOMBRE_INFORMANTE.Enabled = true;
                TextBox_CARGO_INFORMANTE.Enabled = true;
                TextBox_Nombrejefe.Enabled = true;
                TextBox_CargoJefe.Enabled = true;
                TextBox_EMPRESA_TRABAJO.Enabled = true;
                TextBox_Numero_Telefono.Enabled = true;
                TextBox_EmpresaTemporal.Enabled = true;
                TextBox_TelefonoEmpresaTemporal.Enabled = true;
                DropDownList_TipoContrato.Enabled = true;
                TextBox_ULTIMO_CARGO.Enabled = true;
                TextBox_FECHA_INGRESO.Enabled = true;
                TextBox_FECHA_RETIRO.Enabled = true;
                TextBox_ULTIMO_SALARIO.Enabled = true;
                TextBox_Comisiones.Enabled = true;
                TextBox_Bonos.Enabled = true;
                TextBox_MotivoRetiro.Enabled = true;
                break;
            case Acciones.ModificarReferencia:
                DropDownList_TIPO_REFERENCIA.Enabled = true;
                TextBox_NOMBRE_INFORMANTE.Enabled = true;
                TextBox_CARGO_INFORMANTE.Enabled = true;
                TextBox_Nombrejefe.Enabled = true;
                TextBox_CargoJefe.Enabled = true;
                TextBox_EMPRESA_TRABAJO.Enabled = true;
                TextBox_Numero_Telefono.Enabled = true;
                TextBox_EmpresaTemporal.Enabled = true;
                TextBox_TelefonoEmpresaTemporal.Enabled = true;
                DropDownList_TipoContrato.Enabled = true;
                TextBox_ULTIMO_CARGO.Enabled = true;
                TextBox_FECHA_INGRESO.Enabled = true;
                TextBox_FECHA_RETIRO.Enabled = true;
                TextBox_ULTIMO_SALARIO.Enabled = true;
                TextBox_Comisiones.Enabled = true;
                TextBox_Bonos.Enabled = true;
                TextBox_MotivoRetiro.Enabled = true;
                break;
            case Acciones.SeleccionTipoReferencia:
                DropDownList_TipoConfirmacionReferencia.Enabled = true;
                break;
        }
    }

    private void CargardatosControlregistro(DataRow filaReferencia)
    {
        TextBox_USU_CRE.Text = filaReferencia["USU_CRE"].ToString();
        try
        {
            TextBox_FCH_CRE.Text = DateTime.Parse(filaReferencia["FCH_CRE"].ToString()).ToShortDateString();
            TextBox_HOR_CRE.Text = DateTime.Parse(filaReferencia["FCH_CRE"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_CRE.Text = "";
            TextBox_HOR_CRE.Text = "";
        }
        TextBox_USU_MOD.Text = filaReferencia["USU_MOD"].ToString();
        try
        {
            TextBox_FCH_MOD.Text = DateTime.Parse(filaReferencia["FCH_MOD"].ToString()).ToShortDateString();
            TextBox_HOR_MOD.Text = DateTime.Parse(filaReferencia["FCH_MOD"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_MOD.Text = "";
            TextBox_HOR_MOD.Text = "";
        }
    }

    private void CargarDatosReferencia(DataRow filaReferencia)
    {
        try
        {
            DropDownList_TipoConfirmacionReferencia.SelectedValue = filaReferencia["ID_CATEGORIA"].ToString().Trim();
        }
        catch
        {
            DropDownList_TipoConfirmacionReferencia.SelectedIndex = 0;
        }

        TextBox_EMPRESA_TRABAJO.Text = filaReferencia["EMPRESA_TRABAJO"].ToString().Trim().ToUpper();

        try
        {
            TextBox_FECHA_INGRESO.Text = Convert.ToDateTime(filaReferencia["FECHA_INGRESO"]).ToShortDateString();
        }
        catch
        {
            TextBox_FECHA_INGRESO.Text = "";
        }

        try
        {
            TextBox_FECHA_RETIRO.Text = Convert.ToDateTime(filaReferencia["FECHA_RETIRO"]).ToShortDateString();
        }
        catch
        {
            TextBox_FECHA_RETIRO.Text = "";
        }

        TextBox_ULTIMO_CARGO.Text = filaReferencia["ULTIMO_CARGO"].ToString().Trim().ToUpper();

        TextBox_NOMBRE_INFORMANTE.Text = filaReferencia["NOMBRE_INFORMANTE"].ToString().Trim().ToUpper();

        TextBox_CARGO_INFORMANTE.Text = filaReferencia["CARGO_INFORMANTE"].ToString().Trim().ToUpper();

        try
        {
            TextBox_ULTIMO_SALARIO.Text = Convert.ToDecimal(filaReferencia["ULTIMO_SALARIO"]).ToString();
        }
        catch
        {
            TextBox_ULTIMO_SALARIO.Text = "";
        }

        TextBox_Numero_Telefono.Text = filaReferencia["NUM_TELEFONO"].ToString().Trim().ToUpper();

        TextBox_Nombrejefe.Text = filaReferencia["NOMBRE_JEFE"].ToString().Trim().ToUpper();

        TextBox_CargoJefe.Text = filaReferencia["CARGO_JEFE"].ToString().Trim().ToUpper();

        TextBox_EmpresaTemporal.Text = filaReferencia["EMPRESA_TEMPORAL"].ToString().Trim().ToUpper();

        TextBox_TelefonoEmpresaTemporal.Text = filaReferencia["NUM_TELEFONO_TEMPOAL"].ToString().Trim().ToUpper();

        try
        {
            DropDownList_TipoContrato.SelectedValue = filaReferencia["TIPO_CONTRATO"].ToString().Trim();
        }
        catch
        {
            DropDownList_TipoContrato.SelectedIndex = 0;
        }

        TextBox_Comisiones.Text = filaReferencia["COMISIONES"].ToString().Trim();

        TextBox_Bonos.Text = filaReferencia["BONOS"].ToString().Trim();

        TextBox_MotivoRetiro.Text = filaReferencia["MOTIVO_RETIRO"].ToString().Trim();
    }

    private void CargarGrillaPreguntasConRespuesta(DataTable tablaRespuestas)
    {
        GridView_PREGUNTAS.DataSource = tablaRespuestas;
        GridView_PREGUNTAS.DataBind();

        for (int i = 0; i < GridView_PREGUNTAS.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_PREGUNTAS.Rows[i];
            DataRow filaTabla = tablaRespuestas.Rows[i];
            Label pregunta = filaGrilla.FindControl("Label_PREGUNTA") as Label;
            TextBox texto = filaGrilla.FindControl("TextBox_RESPUESTA") as TextBox;

            pregunta.Text = filaTabla["CONTENIDO"].ToString().Trim();
            texto.Text = filaTabla["RESPUESTA"].ToString().Trim();
        }
    }

    private void CargarPreguntasAsociadasAReferencia(Decimal ID_REFERENCIA)
    {
        referencia _referencia = new referencia(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaPreguntas = _referencia.ObtenerPreguntasRespuestasReferencia(ID_REFERENCIA);

        if (tablaPreguntas.Rows.Count <= 0)
        {
            if (_referencia.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _referencia.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron preguntas y respuestas asociadas a la confirmación de referencia Laboral seleccionada.", Proceso.Advertencia);
            }
        }   
        else
        {
            CargarGrillaPreguntasConRespuesta(tablaPreguntas);
        }
    }

    private void deshabilitarFilasGrilla(GridView grilla)
    {
        for (int i = 0; i < grilla.Rows.Count; i++)
        {
            for (int j = 0; j < grilla.Columns.Count; j++)
            {
                grilla.Rows[i].Cells[j].Enabled = false;                
            }
        }
    }

    private void CargarGrillaCualidades_desdetabla_con_calificaiones(DataTable tablaCalificaciones)
    {
        GridView_CalificacionesReferencia.DataSource = tablaCalificaciones;
        GridView_CalificacionesReferencia.DataBind();

        for (int i = 0; i < GridView_CalificacionesReferencia.Rows.Count; i++)
        {
            DataRow filaTabla = tablaCalificaciones.Rows[i];
            GridViewRow filaGrilla = GridView_CalificacionesReferencia.Rows[i];

            Label label_Cualidad = filaGrilla.FindControl("Label_Cualidad") as Label;
            DropDownList drop_Calificaciones = filaGrilla.FindControl("DropDownList_Calificacion") as DropDownList;

            label_Cualidad.Text = filaTabla["CUALIDAD"].ToString();
            CargarDropCalificaciones(drop_Calificaciones);
            try
            {
                drop_Calificaciones.SelectedValue = filaTabla["CALIFICACION"].ToString();
            }
            catch
            {
                drop_Calificaciones.SelectedIndex = 0;
            }
        }
    }

    private void CargarCalificacionesReferencia(DataRow filaReferencia)
    {
        DataTable tablaCalificaciones = new DataTable();

        tablaCalificaciones.Columns.Add("CUALIDAD");
        tablaCalificaciones.Columns.Add("CALIFICACION");

        if(DBNull.Value.Equals(filaReferencia["CUALIDADES_CALIFICACION"]) == true)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La Confirmación de referencia laboral seleccionada no posee calificación de cualidades.", Proceso.Advertencia);

            GridView_CalificacionesReferencia.DataSource = null;
            GridView_CalificacionesReferencia.DataBind();
        }
        else
        {
            String[] cualidadesCalificacionesArray = filaReferencia["CUALIDADES_CALIFICACION"].ToString().Trim().Split(';');

            foreach (String cualidadCalificacion in cualidadesCalificacionesArray)
            {
                DataRow filaCalificaciones = tablaCalificaciones.NewRow();

                filaCalificaciones["CUALIDAD"] = cualidadCalificacion.Split(':')[0];
                filaCalificaciones["CALIFICACION"] = cualidadCalificacion.Split(':')[1];

                tablaCalificaciones.Rows.Add(filaCalificaciones);
            }

            CargarGrillaCualidades_desdetabla_con_calificaiones(tablaCalificaciones);
        }
    }

    private void Cargar(Decimal ID_SOLICITUD, Decimal ID_REFERENCIA, String ARCHIVO, Decimal ID_REQUERIMIENTO)
    {
        HiddenField_ID_SOLICITUD.Value = ID_SOLICITUD.ToString();
        HiddenField_ID_REFERENCIA.Value = ID_REFERENCIA.ToString();
        HiddenField_ID_REQUERIMIENTO.Value = ID_REQUERIMIENTO.ToString();
        HiddenField_ARCHIVO.Value = ARCHIVO;

        Ocultar(Acciones.Inicio);

            if (ID_REFERENCIA != 0)
            {
                Mostrar(Acciones.CargarReferencia);
                Desactivar(Acciones.Inicio);
                Cargar(Acciones.CargarReferencia);

                referencia _referencia = new referencia(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                DataTable tablaReferencia = _referencia.ObtenerPorIdReferencia(ID_REFERENCIA);
                DataRow filaReferencia = tablaReferencia.Rows[0];

                CargardatosControlregistro(filaReferencia);

                CargarDatosReferencia(filaReferencia);

                CargarCalificacionesReferencia(filaReferencia);
                
                CargarPreguntasAsociadasAReferencia(ID_REFERENCIA);

                deshabilitarFilasGrilla(GridView_PREGUNTAS);
                deshabilitarFilasGrilla(GridView_CalificacionesReferencia);

                HiddenField_TIPO_ACTUALIZACION_REFERENCIA.Value = referencia.TiposActualizacionReferencia.Nunguna.ToString();
            }
            else
            { 
                Mostrar(Acciones.SeleccionTipoReferencia);
                Activar(Acciones.SeleccionTipoReferencia);
                Limpiar(Acciones.NuevaReferencia);
                Cargar(Acciones.SeleccionTipoReferencia);

                HiddenField_TIPO_ACTUALIZACION_REFERENCIA.Value = referencia.TiposActualizacionReferencia.CreacionLimpia.ToString();
            }
    }

    protected void GridView_RESULTADOS_BUSQUEDA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal ID_SOLICITUD = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_SOLICITUD"]);
            Decimal ID_REFERENCIA = 0;
            String ARCHIVO = GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ARCHIVO"].ToString();
            Decimal ID_REQUERIMIENTO = 0;

            try
            {
                ID_REFERENCIA = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_REFERENCIA"]);
            }
            catch
            {
                ID_REFERENCIA = 0;
            }

            try
            {
                ID_REQUERIMIENTO = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_REQUERIMIENTO"]);
            }
            catch
            {
                ID_REQUERIMIENTO = 0;
            }

            this.Label_DOCUMENTO_IDENTIDAD.Text = this.GridView_RESULTADOS_BUSQUEDA.Rows[indexSeleccionado].Cells[3].Text;
            this.Label_NOMBRES.Text = this.GridView_RESULTADOS_BUSQUEDA.Rows[indexSeleccionado].Cells[1].Text
                + " " + this.GridView_RESULTADOS_BUSQUEDA.Rows[indexSeleccionado].Cells[2].Text;
            if (this.GridView_RESULTADOS_BUSQUEDA.Rows[indexSeleccionado].Cells[4].Text != "&nbsp;")
            {
                this.Label_CARGO.Text = HttpUtility.HtmlDecode(this.GridView_RESULTADOS_BUSQUEDA.Rows[indexSeleccionado].Cells[4].Text);
            }
            else
            {
                this.Label_CARGO.Text = "Desconocido";
            }

            Cargar(ID_SOLICITUD, ID_REFERENCIA, ARCHIVO, ID_REQUERIMIENTO);
        }
    }

    private void ocultar_mensaje(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.Style.Add("display", "none");
        panel_mensaje.Style.Add("display", "none");

        panel_fondo.Visible = false;
        panel_mensaje.Visible = false;
    }

    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    protected void Button_NUEVO_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.SeleccionTipoReferencia);
        Activar(Acciones.SeleccionTipoReferencia);
        Limpiar(Acciones.NuevaReferencia);
        Cargar(Acciones.SeleccionTipoReferencia);

        HiddenField_ID_REFERENCIA.Value = "";

        HiddenField_TIPO_ACTUALIZACION_REFERENCIA.Value = referencia.TiposActualizacionReferencia.CreacionEHistorial.ToString();
    }

    protected void DropDownList_TipoConfirmacionReferencia_SelectedIndexChanged(object sender, EventArgs e)
    {
        Mostrar(Acciones.NuevaReferencia);
        Activar(Acciones.NuevaReferencia);
        Limpiar(Acciones.NuevaReferencia);
        Cargar(Acciones.NuevaReferencia);
    }
}