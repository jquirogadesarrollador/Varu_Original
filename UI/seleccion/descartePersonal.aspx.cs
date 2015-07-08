using System;
using System.Web.UI.WebControls;
using Brainsbits.LLB.seleccion;
using System.Data;
using Brainsbits.LLB;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;
using TSHAK.Components;
using System.Web;

public partial class _DescartePersonalSeleccion : System.Web.UI.Page
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
        SolicitudIngreso = 1,
        Bitacora = 2,
        BotonesAccion = 3,
        Descarte = 4
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }

    #endregion variables

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
                Panel_botones_accion.Visible = false;
                Panel_RESULTADOS_GRID.Visible = false;
                Panel_DATOS_SOLICITUD_INGRESO.Visible = false;
                Panel_BITACORA_HOJA.Visible = false;
                Panel_DESCARTE.Visible = false;
                Panel_TIPOS_DESCARTE.Visible = false;
                Panel_MOTIVO_DESCARTE.Visible = false;
                Panel_OBSERVACIONES_DESCARTE.Visible = false;
                Button_DESCARTAR.Visible = false;
                Button_CANCELAR_DESCARTE.Visible = false;
                break;
        }
    }

    private void Desactivar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                RadioButtonList_TIPOS_DESCARTE.Enabled = false;
                DropDownList_LISTA_MOTIVOS_DESCARTE.Enabled = false;
                TextBox_OBSERVACIONES_DESCARTE.Enabled = false;
                break;
        }
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_botones_accion.Visible = true;
                break;
            case Acciones.SolicitudIngreso:
                Panel_DATOS_SOLICITUD_INGRESO.Visible = true;
                break;
            case Acciones.Bitacora:
                Panel_BITACORA_HOJA.Visible = true;
                break;
            case Acciones.BotonesAccion:
                Panel_botones_accion.Visible = true;
                break;
            case Acciones.Descarte:
                Panel_DESCARTE.Visible = true;
                Panel_TIPOS_DESCARTE.Visible = true;
                Panel_OBSERVACIONES_DESCARTE.Visible = true;
                Button_DESCARTAR.Visible = true;
                Button_CANCELAR_DESCARTE.Visible = true;
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

        ListItem item = new ListItem("Seleccione", "");
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
            case Acciones.Descarte:
                RadioButtonList_TIPOS_DESCARTE.SelectedIndex = -1;
                TextBox_OBSERVACIONES_DESCARTE.Text = "";

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

    #region constructor
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = "DESCARTE DE PERSOANL -SELECCIÓN-";

        if (IsPostBack == false)
        {
            Iniciar();
        }
    }
    #endregion constructor

    private void ocultar_mensaje(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.Style.Add("display", "none");
        panel_mensaje.Style.Add("display", "none");

        panel_fondo.Visible = false;
        panel_mensaje.Visible = false;
    }

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
        Mostrar(Acciones.Inicio);

        String datosCapturados = HiddenField_FILTRO_DATO.Value;
        String campo = HiddenField_FILTRO_DROP.Value;

        radicacionHojasDeVida _SolIngreso = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaResultadosBusqueda = new DataTable();

        if (campo == "NOMBRES")
        {
            tablaResultadosBusqueda = _SolIngreso.ObtenerRegSolicitudesingresoPorNombres(datosCapturados);
        }
        else
        {
            if (campo == "APELLIDOS")
            {
                tablaResultadosBusqueda = _SolIngreso.ObtenerRegSolicitudesingresoPorApellidos(datosCapturados);
            }
            else
            {
                if (campo == "NUM_DOC_IDENTIDAD")
                {
                    tablaResultadosBusqueda = _SolIngreso.ObtenerRegSolicitudesingresoPorNumDocIdentidad(datosCapturados);
                }
            }
        }

        if (tablaResultadosBusqueda.Rows.Count <= 0)
        {
            if (_SolIngreso.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _SolIngreso.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "ADVERTENCIA: No se encontraron registros que cumplieran los datos de busqueda.", Proceso.Advertencia);
            }
        }
        else
        {
            Panel_RESULTADOS_GRID.Visible = true;
            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaResultadosBusqueda;
            GridView_RESULTADOS_BUSQUEDA.DataBind();

            DataRow filaSolicitud;
            for (int i = 0; i < GridView_RESULTADOS_BUSQUEDA.Rows.Count; i++)
            {
                filaSolicitud = tablaResultadosBusqueda.Rows[(GridView_RESULTADOS_BUSQUEDA.PageIndex * GridView_RESULTADOS_BUSQUEDA.PageSize) + i];

                if ((filaSolicitud["ARCHIVO"].ToString().ToUpper().Trim() == "CONTRATADO"))
                {
                    GridView_RESULTADOS_BUSQUEDA.Rows[i].Cells[0].Enabled = false;
                    GridView_RESULTADOS_BUSQUEDA.Rows[i].Cells[0].Text = "";
                }
            }
        }
    }

    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "CON_FILTRO";
        HiddenField_FILTRO_DROP.Value = DropDownList_BUSCAR.SelectedValue;
        HiddenField_FILTRO_DATO.Value = TextBox_BUSCAR.Text;

        Buscar();
    }

    private void cargar_datos_solicitud_ingreso(DataRow filaInfoSolicitud)
    {
        Mostrar(Acciones.SolicitudIngreso);

        HiddenField_ID_SOLICITUD.Value = filaInfoSolicitud["ID_SOLICITUD"].ToString().Trim();

        if ((filaInfoSolicitud["ARCHIVO"].ToString().Trim().ToUpper() == "EN CLIENTE") || (filaInfoSolicitud["ARCHIVO"].ToString().Trim().ToUpper() == "POR CONTRATAR") || (filaInfoSolicitud["ARCHIVO"].ToString().Trim().ToUpper() == "CONTRATADO"))
        {
            HiddenField_ID_REQUERIMIENTO.Value = filaInfoSolicitud["ID_REQUERIMIENTO"].ToString().Trim();
        }
        else
        {
            HiddenField_ID_REQUERIMIENTO.Value = "";
        }

        Label_NUM_DOC_IDENTIDAD.Text = filaInfoSolicitud["TIP_DOC_IDENTIDAD"].ToString().Trim() + " " + filaInfoSolicitud["NUM_DOC_IDENTIDAD"].ToString().Trim();
        Label_NOMBRE_SOLICITUD_INGRESO.Text = filaInfoSolicitud["NOMBRES"].ToString().Trim().ToUpper() + " " + filaInfoSolicitud["APELLIDOS"].ToString().Trim().ToUpper();

        if (filaInfoSolicitud["NOMBRE_CIUDAD"] != DBNull.Value)
        {
            Label_CIUDAD_SOLICITUD_INGRESO.Text = filaInfoSolicitud["NOMBRE_CIUDAD"].ToString().Trim().ToUpper();
        }
        else
        {
            Label_CIUDAD_SOLICITUD_INGRESO.Text = "DESCONOCIDA";
        }

        Label_DIRECCION_SOLICITUD_INGRESO.Text = filaInfoSolicitud["DIR_ASPIRANTE"].ToString().Trim().ToUpper();
        Label_SECTOR_SOLICITUD_INGRESO.Text = filaInfoSolicitud["SECTOR"].ToString().ToUpper().Trim();
        Label_TELEFONO_SOLICITUD_INGRESO.Text = filaInfoSolicitud["TEL_ASPIRANTE"].ToString().Trim();
        Label_E_MAIL_SOLICITUD_INGRESO.Text = filaInfoSolicitud["E_MAIL"].ToString().Trim();

        Label_ESTADO_SOLICITUD_INGRESO.Text = filaInfoSolicitud["ARCHIVO"].ToString().Trim().ToUpper();
    }

    private void cargar_datos_bitacora(DataRow filaInfoSolicitud)
    {
        int ID_SOLICITUD = Convert.ToInt32(filaInfoSolicitud["ID_SOLICITUD"]);

        regRegsitrosHojaVida _regRegsitrosHojaVida = new regRegsitrosHojaVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaRegRegistro = _regRegsitrosHojaVida.ObtenerPorIdSolicitud(ID_SOLICITUD);

        if (tablaRegRegistro.Rows.Count > 0)
        {
            Mostrar(Acciones.Bitacora);
            GridView_BITACORA.DataSource = tablaRegRegistro;
            GridView_BITACORA.DataBind();
        }
    }

    private void Activar(Acciones accion)
    {
        switch (accion)
        { 
            case Acciones.Descarte:
                RadioButtonList_TIPOS_DESCARTE.Enabled = true;
                DropDownList_LISTA_MOTIVOS_DESCARTE.Enabled = true;
                TextBox_OBSERVACIONES_DESCARTE.Enabled = true;

                break;
        }
    }

    private void cargar_seccion_descarte(DataRow filaInfoSolicitud)
    {
        if (filaInfoSolicitud["ARCHIVO"].ToString().Trim().ToUpper() == "DESCARTADO SELECCION")
        {
        }
        else
        { 
            Mostrar(Acciones.Descarte);
            Activar(Acciones.Descarte);
            Cargar(Acciones.Descarte);
        }
    }

    private void Cargar(Decimal ID_SOLICITUD)
    {
        HiddenField_ID_SOLICITUD.Value = ID_SOLICITUD.ToString();

        DataTable tablaInfoSolicitud;

        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        tablaInfoSolicitud = _radicacionHojasDeVida.ObtenerRegSolicitudesingresoPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));

        if (String.IsNullOrEmpty(_radicacionHojasDeVida.MensajeError) == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _radicacionHojasDeVida.MensajeError, Proceso.Error);
        }
        else
        {
            if (tablaInfoSolicitud.Rows.Count <= 0)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información de la solicitud seleccionada.", Proceso.Advertencia);
            }
            else
            {
                Ocultar(Acciones.Inicio);
                Mostrar(Acciones.BotonesAccion);

                DataRow filaInfoSolicitud = tablaInfoSolicitud.Rows[0];

                cargar_datos_solicitud_ingreso(filaInfoSolicitud);

                cargar_datos_bitacora(filaInfoSolicitud);

                cargar_seccion_descarte(filaInfoSolicitud);
            }
        }
    }

    protected void GridView_RESULTADOS_BUSQUEDA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal ID_SOLICITUD = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_SOLICITUD"]);

            Cargar(ID_SOLICITUD);
        }
    }

    private void CargarDropMotivosDescarte()
    {
        DropDownList_LISTA_MOTIVOS_DESCARTE.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_DESCARTE_SELECCION);

        ListItem item = new ListItem("Seleccione...", "");
        
        DropDownList_LISTA_MOTIVOS_DESCARTE.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            if(fila["VARIABLE"].ToString().Trim() == RadioButtonList_TIPOS_DESCARTE.SelectedValue)
            {
                item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
                DropDownList_LISTA_MOTIVOS_DESCARTE.Items.Add(item);
            }
        }
        DropDownList_LISTA_MOTIVOS_DESCARTE.DataBind();
    }

    protected void RadioButtonList_TIPOS_DESCARTE_SelectedIndexChanged(object sender, EventArgs e)
    {
        if ((RadioButtonList_TIPOS_DESCARTE.SelectedValue == "DESC. OTROS") || (RadioButtonList_TIPOS_DESCARTE.SelectedValue == "POR CLIENTE"))
        {
            Panel_MOTIVO_DESCARTE.Visible = false;
            DropDownList_LISTA_MOTIVOS_DESCARTE.SelectedIndex = -1;
        }
        else
        {
            CargarDropMotivosDescarte();
            Panel_MOTIVO_DESCARTE.Visible = true;
        }

        TextBox_OBSERVACIONES_DESCARTE.Text = "";
    }

    private void RealizarDescarte()
    {
        Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);

        Decimal ID_REQUERIMIENTO = 0;
        if (String.IsNullOrEmpty(HiddenField_ID_REQUERIMIENTO.Value) == false)
        {
            ID_REQUERIMIENTO = Convert.ToDecimal(HiddenField_ID_REQUERIMIENTO.Value);
        }

        String CLASE_REGISTRO = RadioButtonList_TIPOS_DESCARTE.SelectedValue;
        String COMENTARIOS = TextBox_OBSERVACIONES_DESCARTE.Text.Trim();
        String MOTIVO = null;
        if ((RadioButtonList_TIPOS_DESCARTE.SelectedValue != "DESC. OTROS") && (RadioButtonList_TIPOS_DESCARTE.SelectedValue != "POR CLIENTE"))
        {
            MOTIVO = DropDownList_LISTA_MOTIVOS_DESCARTE.SelectedValue;
        }

        regRegsitrosHojaVida _regRegsitrosHojaVida = new regRegsitrosHojaVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Decimal REGISTRO = _regRegsitrosHojaVida.AdicionarRegRegistrosHojaVida(ID_SOLICITUD, CLASE_REGISTRO, COMENTARIOS, MOTIVO, ID_REQUERIMIENTO);

        if (REGISTRO <= 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _regRegsitrosHojaVida.MensajeError, Proceso.Error);
        }
        else
        {
            Cargar(ID_SOLICITUD);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Se realizó el descarte correctamente.", Proceso.Correcto);
        }
    }

    protected void Button_DESCARTAR_Click(object sender, EventArgs e)
    {
        RealizarDescarte();
    }
    protected void Button_CANCELAR_DESCARTE_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
    }
    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;

        Buscar();
    }
    #endregion eventos
    
}