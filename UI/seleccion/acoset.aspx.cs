using System.Web.UI.WebControls;
using System;
using Brainsbits.LLB.maestras;
using System.Data;
using Brainsbits.LLB;
using System.Collections.Generic;
using System.IO;
using Brainsbits.LLB.seguridad;
using TSHAK.Components;
using System.Web;


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

    #region variables
    private enum Acciones
    {
        Inicio = 0,
        Busqueda = 1,
        Nuevo = 2,
        CargarRegistro = 3,
        ModificarRegistro = 4,
        SubidaMasiva = 5
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }

    private enum ErroresSubidaMasiva
    { 
        ERROR_TIP_DOC_IDENTIDAD = 0,
        ERROR_ENTIDAD_REPORTA = 1,
        ERROR_ESTRUCTURA_ARCHIVO = 2
    }
    #endregion variables

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

    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = false;

                Panel_FORM_BOTONES.Visible = false;
                Button_NUEVO.Visible = false;
                Button_NUEVO_1.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_CANCELAR_1.Visible = false;
                Button_HABILITAR_SUBIDA_MASIVA.Visible = false;
                Button_HABILITAR_SUBIDA_MASIVA_1.Visible = false;

                Panel_RESULTADOS_GRID.Visible = false;

                Panel_FORMULARIO.Visible = false;
                Panel_CONTROL_REGISTRO.Visible = false;
                Panel_DATOS_CONTACTO.Visible = false;
                Panel_ENTIDAD_REPORTA.Visible = false;
                Panel_MOTIVO_REPORTE.Visible = false;
                Panel_ESTADO_REGISTRO.Visible = false;
                Panel_MOTIVO_ESTADO.Visible = false;

                Panel_FORM_BOTONES_PIE.Visible = false;

                Panel_SUBIDA_NASIVA.Visible = false;
                Panel_INFO_DATOS_SUBIDA_MASIVA.Visible = false;
                Panel_FILEUPLOAD_ARCHIVO_PLANO.Visible = false;
                Button_CONFIRMAR_INFO_aRCHIVP_PLANO.Visible = false;
                Button_GUARDAR_REGISTROS_MASIVOS.Visible = false;
                Button_CANCELAR_SUBIDA_MASIVA.Visible = false;

                Panel_GRILLA_ERRORES_SUBIDA_MASIVA.Visible = false;

                break;
            case Acciones.ModificarRegistro:

                Button_NUEVO.Visible = false;
                Button_NUEVO_1.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_CANCELAR_1.Visible = false;

                Panel_CONTROL_REGISTRO.Visible = true;

                break;
        }
    }

    private void Desactivar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                TextBox_FCH_CRE.Enabled = false;
                TextBox_FCH_MOD.Enabled = false;
                TextBox_HOR_CRE.Enabled = false;
                TextBox_HOR_MOD.Enabled = false;
                TextBox_USU_CRE.Enabled = false;
                TextBox_USU_MOD.Enabled = false;

                DropDownList_TIP_DOC.Enabled = false;
                TextBox_NUM_DOC.Enabled = false;
                TextBox_NOMBRES.Enabled = false;
                TextBox_APELLIDOS.Enabled = false;
                DropDownList_ENTIDAD_REPORTA.Enabled = false;
                TextBox_MOTIVO_REPORTE.Enabled = false;
                DropDownList_ESTADO_REGISTRO.Enabled = false;
                TextBox_MOTIVO_ESTADO.Enabled = false;

                break;
        }
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_FORM_BOTONES.Visible = true;
                Button_NUEVO.Visible = true;
                Button_HABILITAR_SUBIDA_MASIVA.Visible = true;
                break;
            case Acciones.Busqueda:
                Panel_FORM_BOTONES.Visible = true;
                Button_NUEVO.Visible = true;
                Button_HABILITAR_SUBIDA_MASIVA.Visible = true;

                Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.Nuevo:
                Panel_FORM_BOTONES.Visible = true;
                Panel_FORM_BOTONES_PIE.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR.Visible = true;
                Button_CANCELAR_1.Visible = true;

                Panel_FORMULARIO.Visible = true;
                Panel_DATOS_CONTACTO.Visible = true;
                Panel_ENTIDAD_REPORTA.Visible = true;
                Panel_MOTIVO_REPORTE.Visible = true;
                break;
            case Acciones.CargarRegistro:
                Panel_FORM_BOTONES.Visible = true;
                Panel_FORM_BOTONES_PIE.Visible = true;
                Button_MODIFICAR.Visible = true;
                Button_MODIFICAR_1.Visible = true;
                Button_CANCELAR.Visible = true;
                Button_CANCELAR_1.Visible = true;

                Panel_FORMULARIO.Visible = true;
                Panel_CONTROL_REGISTRO.Visible = true;
                Panel_DATOS_CONTACTO.Visible = true;
                Panel_ENTIDAD_REPORTA.Visible = true;
                Panel_MOTIVO_REPORTE.Visible = true;
                Panel_ESTADO_REGISTRO.Visible = true;
                Panel_MOTIVO_ESTADO.Visible = true;
                break;
            case Acciones.ModificarRegistro:
                Button_GUARDAR.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR.Visible = true;
                Button_CANCELAR_1.Visible = true;
                break;
            case Acciones.SubidaMasiva:
                Panel_FORM_BOTONES.Visible = true;

                Panel_SUBIDA_NASIVA.Visible = true;
                Panel_FILEUPLOAD_ARCHIVO_PLANO.Visible = true;

                Button_CONFIRMAR_INFO_aRCHIVP_PLANO.Visible = true;
                Button_CANCELAR_SUBIDA_MASIVA.Visible = true;
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

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_BUSCAR.Items.Add(item);
        item = new ListItem("Numero de Identidad", "NUM_DOC_IDENTIDAD");
        DropDownList_BUSCAR.Items.Add(item);
        item = new ListItem("Nombres", "NOMBRES");
        DropDownList_BUSCAR.Items.Add(item);
        item = new ListItem("Apellidos", "APELLIDOS");
        DropDownList_BUSCAR.Items.Add(item);

        DropDownList_BUSCAR.DataBind();
    }

    private void iniciar_seccion_de_busqueda()
    {
        TextBox_BUSCAR.Text = "";
        cargar_DropDownList_BUSCAR();
    }

    private void cargar_DropDownList_TIPO_DOC()
    {
        DropDownList_TIP_DOC.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaUsuarios = _parametro.ObtenerParametrosPorTabla("TIP_DOC_ID");

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_TIP_DOC.Items.Add(item);

        foreach (DataRow fila in tablaUsuarios.Rows)
        {
            item = new ListItem(fila["descripcion"].ToString(), fila["codigo"].ToString());
            DropDownList_TIP_DOC.Items.Add(item);
        }

        DropDownList_TIP_DOC.DataBind();
    }

    private void cargar_DropDownList_ENTIDAD_REPORTA()
    {
        DropDownList_ENTIDAD_REPORTA.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaUsuarios = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_ENTIDAD_ACOSET);

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_ENTIDAD_REPORTA.Items.Add(item);

        foreach (DataRow fila in tablaUsuarios.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_ENTIDAD_REPORTA.Items.Add(item);
        }

        DropDownList_ENTIDAD_REPORTA.DataBind();
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                configurarCaracteresAceptadosBusqueda(true, true);

                iniciar_seccion_de_busqueda();

                HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "SIN_FILTRO";
                HiddenField_FILTRO_DROP.Value = String.Empty;
                HiddenField_FILTRO_DATO.Value = String.Empty;
                break;
            case Acciones.Nuevo:
                cargar_DropDownList_TIPO_DOC();
                cargar_DropDownList_ENTIDAD_REPORTA();
                break;
        }
    }

    private void Iniciar()
    {
        Configurar();
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }

    private void Limpiar(Acciones accion)
    {
        switch(accion)
        {
            case Acciones.Nuevo:
                TextBox_NUM_DOC.Text = "";
                TextBox_NOMBRES.Text = "";
                TextBox_APELLIDOS.Text = "";
                TextBox_MOTIVO_REPORTE.Text = "";
                TextBox_MOTIVO_ESTADO.Text = "";

                HiddenField_REGISTRO_ACOSET.Value = "";
                break;
        }
    }

    private void Activar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Nuevo:
                DropDownList_TIP_DOC.Enabled = true;
                TextBox_NUM_DOC.Enabled = true;
                TextBox_NOMBRES.Enabled = true;
                TextBox_APELLIDOS.Enabled = true;
                DropDownList_ENTIDAD_REPORTA.Enabled = true;
                TextBox_MOTIVO_REPORTE.Enabled = true;
                break;
            case Acciones.ModificarRegistro:
                DropDownList_TIP_DOC.Enabled = true;
                TextBox_NUM_DOC.Enabled = true;
                TextBox_NOMBRES.Enabled = true;
                TextBox_APELLIDOS.Enabled = true;
                DropDownList_ENTIDAD_REPORTA.Enabled = true;
                TextBox_MOTIVO_REPORTE.Enabled = true;

                DropDownList_ESTADO_REGISTRO.Enabled = true;
                TextBox_MOTIVO_ESTADO.Enabled = true;
                break;
        }
    }

    private void cargar_informacion_registro_control(DataRow filaRegistro)
    {
        TextBox_USU_CRE.Text = filaRegistro["USU_CRE"].ToString();
        try
        {
            TextBox_FCH_CRE.Text = DateTime.Parse(filaRegistro["FCH_CRE"].ToString()).ToShortDateString();
            TextBox_HOR_CRE.Text = DateTime.Parse(filaRegistro["FCH_CRE"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_CRE.Text = "";
            TextBox_HOR_CRE.Text = "";
        }
        TextBox_USU_MOD.Text = filaRegistro["USU_MOD"].ToString();
        try
        {
            TextBox_FCH_MOD.Text = DateTime.Parse(filaRegistro["FCH_MOD"].ToString()).ToShortDateString();
            TextBox_HOR_MOD.Text = DateTime.Parse(filaRegistro["FCH_MOD"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_MOD.Text = "";
            TextBox_HOR_MOD.Text = "";
        }
    }

    private void cargar_datos_personales(DataRow filaRefistro)
    {
        cargar_DropDownList_TIPO_DOC();
        try
        {
            DropDownList_TIP_DOC.SelectedValue = filaRefistro["TIP_DOC_IDENTIDAD"].ToString().Trim();
        }
        catch
        {
            DropDownList_TIP_DOC.SelectedIndex = -1;
        }

        TextBox_NUM_DOC.Text = filaRefistro["NUM_DOC_IDENTIDAD"].ToString().Trim();

        TextBox_NOMBRES.Text = filaRefistro["NOMBRES"].ToString().Trim();
        TextBox_APELLIDOS.Text = filaRefistro["APELLIDOS"].ToString().Trim();

        cargar_DropDownList_ENTIDAD_REPORTA();
        try
        {
            DropDownList_ENTIDAD_REPORTA.SelectedValue = filaRefistro["ENTIDAD_REPORTA"].ToString().Trim();
        }
        catch
        {
            DropDownList_ENTIDAD_REPORTA.SelectedIndex = -1;
        }

        TextBox_MOTIVO_REPORTE.Text = filaRefistro["OBS_ACOSET"].ToString().Trim();

        if (filaRefistro["ACTIVO"] == DBNull.Value)
        {
            DropDownList_ESTADO_REGISTRO.SelectedIndex = 0;
        }
        else
        {
            if (filaRefistro["ACTIVO"].ToString().Trim().ToUpper() == "TRUE")
            {
                DropDownList_ESTADO_REGISTRO.SelectedValue = "True";
                Panel_MOTIVO_ESTADO.Visible = false;
            }
            else
            {
                DropDownList_ESTADO_REGISTRO.SelectedValue = "False";
                Panel_MOTIVO_ESTADO.Visible = true;
                TextBox_MOTIVO_ESTADO.Text = filaRefistro["MOTIVO_ESTADO"].ToString().Trim();
            }
        }
    }

    private void Cargar(Decimal REGISTRO)
    {
        HiddenField_REGISTRO_ACOSET.Value = REGISTRO.ToString();

        acoset _acoset = new acoset(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaRegistro = _acoset.ObtenerRegAcosetPorRegistro(Convert.ToInt32(REGISTRO));

        if (tablaRegistro.Rows.Count <= 0)
        {
            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Cargar(Acciones.Inicio);

            if (_acoset.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _acoset.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información del registro especificado.", Proceso.Error);
            }
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.CargarRegistro);
            Desactivar(Acciones.Inicio);

            DataRow filaRegistro = tablaRegistro.Rows[0];

            cargar_informacion_registro_control(filaRegistro);

            cargar_datos_personales(filaRegistro);
        }

    }   

    private void Guardar()
    {
        String NOMBRES = TextBox_NOMBRES.Text.ToUpper().Trim();
        String APELLIDOS = TextBox_APELLIDOS.Text.ToUpper().Trim();
        String TIP_DOC_IDENTIDAD = DropDownList_TIP_DOC.SelectedValue;
        String NUM_DOC_IDENTIDAD = TextBox_NUM_DOC.Text.ToUpper().Trim();

        String OBS_ACOSET = TextBox_MOTIVO_REPORTE.Text.Trim().ToUpper();

        String ENTIDAD_REPORTA = DropDownList_ENTIDAD_REPORTA.SelectedValue;

        acoset _acoset = new acoset(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        
        Decimal REGISTRO = _acoset.AdicionarRegAcoset(APELLIDOS, NOMBRES, TIP_DOC_IDENTIDAD, NUM_DOC_IDENTIDAD, OBS_ACOSET, ENTIDAD_REPORTA);

        if (REGISTRO == 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _acoset.MensajeError, Proceso.Error);
        }
        else
        {
            Cargar(REGISTRO);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El numero de identificación: " + NUM_DOC_IDENTIDAD + " fue registrado correctamente en la Base de datos ACOSET.", Proceso.Correcto);
        }
    }

    private void Modificar()
    {
        Decimal REGISTRO = Convert.ToDecimal(HiddenField_REGISTRO_ACOSET.Value);
        String APELLIDOS = TextBox_APELLIDOS.Text.Trim().ToUpper();
        String NOMBRES = TextBox_NOMBRES.Text.Trim().ToUpper();

        String TIP_DOC_IDENTIDAD = DropDownList_TIP_DOC.SelectedValue;
        String NUM_DOC_IDENTIDAD = TextBox_NUM_DOC.Text.Trim();

        String ENTIDAD_REPORTA = DropDownList_ENTIDAD_REPORTA.SelectedValue;

        String OBS_ACOSET = TextBox_MOTIVO_REPORTE.Text.Trim().ToUpper();

        Boolean ACTIVO = true;

        if(DropDownList_ESTADO_REGISTRO.SelectedValue == "False")
        {
            ACTIVO = false;
        }

        String MOTIVO_ESTADO = null;

        if(String.IsNullOrEmpty(TextBox_MOTIVO_ESTADO.Text) == false)
        {
            MOTIVO_ESTADO = TextBox_MOTIVO_ESTADO.Text.Trim().ToUpper();
        }

        acoset _acoset = new acoset(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        Boolean verificador = _acoset.ActualizarAcoset(REGISTRO, APELLIDOS, NOMBRES, TIP_DOC_IDENTIDAD, NUM_DOC_IDENTIDAD, OBS_ACOSET, ENTIDAD_REPORTA, ACTIVO, MOTIVO_ESTADO);

        if(verificador == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _acoset.MensajeError, Proceso.Error);
        }
        else
        {
            Cargar(REGISTRO);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El registro Acoset asociado al número de identificación: " + NUM_DOC_IDENTIDAD + " fue modificado correctamente.", Proceso.Correcto);
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

    private void Buscar()
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Busqueda);

        String datosCapturados = HiddenField_FILTRO_DATO.Value;
        String campo = HiddenField_FILTRO_DROP.Value;

        acoset _acoset = new acoset(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaResultadosBusqueda = new DataTable();

        if (DropDownList_BUSCAR.SelectedValue == "NOMBRES")
        {
            tablaResultadosBusqueda = _acoset.ObtenerRegAcosetPorNombre(datosCapturados);
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "APELLIDOS")
            {
                tablaResultadosBusqueda = _acoset.ObtenerRegAcosetPorApellido(datosCapturados);
            }
            else
            {
                if (DropDownList_BUSCAR.SelectedValue == "NUM_DOC_IDENTIDAD")
                {
                    tablaResultadosBusqueda = _acoset.ObtenerRegAcosetPorNumeroID(datosCapturados);
                }
            }
        }

        if (tablaResultadosBusqueda.Rows.Count <= 0)
        {
            if (_acoset.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _acoset.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La busqueda no arrojó resultados.", Proceso.Advertencia);
            }

            Panel_RESULTADOS_GRID.Visible = false;
        }
        else
        {
            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaResultadosBusqueda;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
        }
    }

    #endregion metodos

    #region constructor
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (IsPostBack == false)
        {
            Iniciar();
        }
    }
    #endregion constructor

    #region eventos
    protected void Button_NUEVO_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Nuevo);
        Limpiar(Acciones.Nuevo);
        Activar(Acciones.Nuevo);
        Cargar(Acciones.Nuevo);
    }
    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.ModificarRegistro);
        Mostrar(Acciones.ModificarRegistro);
        Activar(Acciones.ModificarRegistro);
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(HiddenField_REGISTRO_ACOSET.Value) == true)
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
        if (String.IsNullOrEmpty(HiddenField_REGISTRO_ACOSET.Value) == true)
        {
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);
        }
        else
        {
            Decimal REGISTRO = Convert.ToDecimal(HiddenField_REGISTRO_ACOSET.Value);

            Cargar(REGISTRO);
        }
    }

    protected void DropDownList_BUSCAR_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_BUSCAR.SelectedValue == "")
        {
            configurarCaracteresAceptadosBusqueda(false, false);
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
                else
                {
                    if (DropDownList_BUSCAR.SelectedValue == "NUM_DOC_IDENTIDAD")
                    {
                        configurarCaracteresAceptadosBusqueda(false, true);
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

        Buscar();
    }

    protected void GridView_RESULTADOS_BUSQUEDA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal REGISTRO = Convert.ToInt32(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["REGISTRO"]);

            Cargar(REGISTRO);
        }
    }

    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;

        Buscar();
    }

    protected void DropDownList_ESTADO_REGISTRO_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_ESTADO_REGISTRO.SelectedValue == "True")
        {
            Panel_MOTIVO_ESTADO.Visible = false;
            TextBox_MOTIVO_ESTADO.Text = "";
        }
        else
        {
            if (DropDownList_ESTADO_REGISTRO.SelectedValue == "False")
            {
                Panel_MOTIVO_ESTADO.Visible = true;
                TextBox_MOTIVO_ESTADO.Text = "";
            }
            else
            {
                Panel_MOTIVO_ESTADO.Visible = false;
                TextBox_MOTIVO_ESTADO.Text = "";
            }
        }
    }

    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    protected void Button_HABILITAR_SUBIDA_MASIVA_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.SubidaMasiva);

        Button_CANCELAR_SUBIDA_MASIVA.Focus();
    }

    protected void Button_CANCELAR_SUBIDA_MASIVA_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);

        Session.Remove("listaRegistrosAcoset");
    }
    #endregion eventos

    private DataTable InicializarTablaErrores()
    {
        DataTable tablaErrores = new DataTable();

        tablaErrores.Columns.Add("TIPO_ERROR");
        tablaErrores.Columns.Add("LINEA");
        tablaErrores.Columns.Add("MENSAJE");

        return tablaErrores;
    }

    protected void Button_CONFIRMAR_INFO_aRCHIVP_PLANO_Click(object sender, EventArgs e)
    {
        Session.Remove("listaRegistrosAcoset");

        List<acoset> listaRegistros = new List<acoset>();
        acoset _acosetParaLista;

        Boolean verificador = true;
        String filaArchivo = null;
        String[] filaArchivoArray;

        Int32 contadorRegistrosTotales = 0;
        Int32 contadorRegistrosProcesados = 0;

        DataTable tablaErrores = InicializarTablaErrores();
        DataRow filaError;

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());
        DataTable tablaEntidadesAcoset = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_ENTIDAD_ACOSET);
        Boolean datoCorrecto = true;

        DataTable tablaTipDocs = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_TIP_DOC_ID);

        if (FileUpload_ARCHIVO_PLANO.HasFile == true)
        {
            using (StreamReader reader = new StreamReader(FileUpload_ARCHIVO_PLANO.PostedFile.InputStream))
            {
                while ((filaArchivo = reader.ReadLine()) != null)
                {
                    contadorRegistrosTotales += 1;

                    filaArchivoArray = filaArchivo.Split(';');

                    _acosetParaLista = new acoset(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

                    verificador = true;

                    if (filaArchivoArray.Length == 6)
                    {
                        _acosetParaLista.ACTIVO = true;
                        _acosetParaLista.APELLIDOS = filaArchivoArray[0].ToUpper();

                        datoCorrecto = false;
                        foreach(DataRow filaEntidad in tablaEntidadesAcoset.Rows)
                        {
                            if(filaEntidad["CODIGO"].ToString().Trim().ToUpper() == filaArchivoArray[5].ToUpper())
                            {
                                datoCorrecto = true;
                                break;
                            }
                        }
                        if(datoCorrecto == true)
                        {
                            _acosetParaLista.ENTIDAD_REPORTA = filaArchivoArray[5].ToUpper();
                        }
                        else
                        {
                            filaError = tablaErrores.NewRow();

                            filaError["TIPO_ERROR"] = ErroresSubidaMasiva.ERROR_ENTIDAD_REPORTA.ToString();
                            filaError["LINEA"] = contadorRegistrosTotales.ToString();
                            filaError["MENSAJE"] = "El nombre de la entidad que reporta no es correcto.";

                            tablaErrores.Rows.Add(filaError);

                            verificador = false;
                        }

                        _acosetParaLista.MOTIVO_ESTADO = null;
                        _acosetParaLista.NOMBRES = filaArchivoArray[1].ToUpper();
                        _acosetParaLista.NUM_DOC_IDENTIDAD = filaArchivoArray[3].ToUpper();
                        _acosetParaLista.OBS_ACOSET = filaArchivoArray[4].ToUpper();
                        _acosetParaLista.REGISTRO = 0;

                        datoCorrecto = false;
                        foreach(DataRow filaTipoDoc in tablaTipDocs.Rows)
                        {
                            if(filaTipoDoc["CODIGO"].ToString().ToUpper() == filaArchivoArray[2].ToUpper())
                            {
                                datoCorrecto = true;
                                break;
                            }
                        }
                        if(datoCorrecto == true)
                        {
                            _acosetParaLista.TIP_DOC_IDENTIDAD = filaArchivoArray[2].ToUpper();
                        }
                        else
                        {
                            filaError = tablaErrores.NewRow();

                            filaError["TIPO_ERROR"] = ErroresSubidaMasiva.ERROR_TIP_DOC_IDENTIDAD.ToString();
                            filaError["LINEA"] = contadorRegistrosTotales.ToString();
                            filaError["MENSAJE"] = "El tipo de documento de identidad no es correcto.";

                            tablaErrores.Rows.Add(filaError);

                            verificador = false;
                        }
                    }
                    else
                    {
                        filaError = tablaErrores.NewRow();

                        filaError["TIPO_ERROR"] = ErroresSubidaMasiva.ERROR_ESTRUCTURA_ARCHIVO.ToString();
                        filaError["LINEA"] = contadorRegistrosTotales.ToString();
                        filaError["MENSAJE"] = "La línea no tiene la estructura correcta: (APELLIDOS ; NOMBRES ; TIP_DOC_IDENTIDAD ; NUM_DOC_IDENTIDAD ; MOTIVO_REPORTE ; ENTIDAD_REPORTA).";

                        tablaErrores.Rows.Add(filaError);

                        verificador = false;
                    }

                    if (verificador == true)
                    {
                        listaRegistros.Add(_acosetParaLista);

                        contadorRegistrosProcesados += 1;
                    }
                }
            }

            if (tablaErrores.Rows.Count > 0)
            {
                
                Panel_GRILLA_ERRORES_SUBIDA_MASIVA.Visible = true;
                Panel_FILEUPLOAD_ARCHIVO_PLANO.Visible = true;
                Button_CONFIRMAR_INFO_aRCHIVP_PLANO.Visible = true;
                Button_CANCELAR_SUBIDA_MASIVA.Visible = true;

                Button_GUARDAR_REGISTROS_MASIVOS.Visible = false;
                Panel_INFO_DATOS_SUBIDA_MASIVA.Visible = false;

                GridView_ERRORES_SUBIDA_MASIVA.DataSource = tablaErrores;
                GridView_ERRORES_SUBIDA_MASIVA.DataBind();

                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El archivo contiene errores y no puede ser procesado, por favor revise la lista de errores.", Proceso.Error);

                Session.Remove("listaRegistrosAcoset");

                Button_CANCELAR_SUBIDA_MASIVA.Focus();
            }
            else
            {
                Session["listaRegistrosAcoset"] = listaRegistros;

                Panel_GRILLA_ERRORES_SUBIDA_MASIVA.Visible = false;
                Panel_FILEUPLOAD_ARCHIVO_PLANO.Visible = true;
                Button_CONFIRMAR_INFO_aRCHIVP_PLANO.Visible = false;
                Button_CANCELAR_SUBIDA_MASIVA.Visible = true;
                Button_GUARDAR_REGISTROS_MASIVOS.Visible = true;
                Panel_INFO_DATOS_SUBIDA_MASIVA.Visible = true;

                Label_NUM_REGISTROS_ARCHIVO.Text = contadorRegistrosProcesados.ToString();

                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El archivo fue verificado, y cumple con las especificaciones, puede continuar con el cargue masivo de egistros Acoset", Proceso.Correcto);

                Button_CANCELAR_SUBIDA_MASIVA.Focus();
            }
        }
        else
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP,Panel_MENSAJES, Label_MENSAJE, "ERROR: Por favor seleccione el archivo con la información de los registros Acoset.", Proceso.Error);

            Session.Remove("listaRegistrosAcoset");

            Button_CANCELAR_SUBIDA_MASIVA.Focus();
        }
    }
    protected void Button_GUARDAR_REGISTROS_MASIVOS_Click(object sender, EventArgs e)
    {
        List<acoset> listaRegistrosAcoset = new List<acoset>();
    
        try
        {
            listaRegistrosAcoset = (List<acoset>)(Session["listaRegistrosAcoset"]);
        }
        catch
        {
            listaRegistrosAcoset = new List<acoset>();    
        }
        
        acoset _acoset = new acoset(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean verificador = _acoset.AdicionarRegAcosetMasivo(listaRegistrosAcoset);

        if (verificador == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _acoset.MensajeError, Proceso.Error);
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La carga masiva de reportes Acoset fue realizada correctamente. Se procesaron " + listaRegistrosAcoset.Count.ToString() + " registros correctamente.", Proceso.Correcto);
        }
    }
}