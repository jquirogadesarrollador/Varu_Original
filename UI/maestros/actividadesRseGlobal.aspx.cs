using System;
using System.Web.UI.WebControls;
using Brainsbits.LLB.maestras;
using TSHAK.Components;
using Brainsbits.LLB;
using Brainsbits.LLB.programasRseGlobal;
using System.Data;
using Brainsbits.LLB.seguridad;
using System.Web;

public partial class _actividadesRseGlobal : System.Web.UI.Page
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

        int proceso = Convert.ToInt32(QueryStringSeguro["proceso"]);
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

    private enum Acciones
    {
        Inicio = 0,
        Nuevo,
        Cargar,
        Modificar
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
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

    private void configurarCaracteresAceptadosBusqueda(Boolean letras, Boolean numeros)
    {
        FilteredTextBoxExtender_TextBox_BUSCAR_Numbers.Enabled = numeros;
        FilteredTextBoxExtender_TextBox_BUSCAR_Letras.Enabled = letras;
    }

    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = false;

                Panel_FORM_BOTONES.Visible = false;
                Button_NUEVO.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_CANCELAR.Visible = false;

                Panel_RESULTADOS_GRID.Visible = false;

                Panel_InformacionActividad.Visible = false;
                Panel_CONTROL_REGISTRO.Visible = false;
                Panel_Actividad.Visible = false;
                Panel_Estado.Visible = false;
                break;
            case Acciones.Modificar:
                Button_NUEVO.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_CANCELAR.Visible = false;

                Panel_CONTROL_REGISTRO.Visible = false;
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

                TextBox_Nombre.Enabled = false;
                DropDownList_Tipo.Enabled = false;
                DropDownList_Sector.Enabled = false;
                TextBox_Descripcion.Enabled = false;
                DropDownList_EstadoActividad.Enabled = false;
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

                Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.Nuevo:
                Panel_FORM_BOTONES.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_InformacionActividad.Visible = true;
                Panel_Actividad.Visible = true;
                break;
            case Acciones.Cargar:
                Panel_FORM_BOTONES.Visible = true;
                Button_MODIFICAR.Visible = true;
                Button_NUEVO.Visible = true;

                Panel_InformacionActividad.Visible = true;
                Panel_CONTROL_REGISTRO.Visible = true;
                Panel_Actividad.Visible = true;
                Panel_Estado.Visible = true;
                break;
            case Acciones.Modificar:
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;
                break;
        }
    }

    private void ConfigurarAreaRseGlobal()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        int proceso = Convert.ToInt32(QueryStringSeguro["proceso"]);

        HiddenField_ID_AREA.Value = _tools.ObtenerIdAreaProceso(proceso);
    }

    private void cargar_DropDownList_BUSCAR()
    {
        DropDownList_BUSCAR.Items.Clear();

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_BUSCAR.Items.Add(item);

        item = new ListItem("Id Actividad", "ID_ACTIVIDAD");
        DropDownList_BUSCAR.Items.Add(item);

        item = new ListItem("Nombre", "NOMBRE");
        DropDownList_BUSCAR.Items.Add(item);

        item = new ListItem("Tipo", "TIPO");
        DropDownList_BUSCAR.Items.Add(item);

        item = new ListItem("Sector", "SECTOR");
        DropDownList_BUSCAR.Items.Add(item);

        DropDownList_BUSCAR.DataBind();
    }

    private void iniciar_seccion_de_busqueda()
    {
        TextBox_BUSCAR.Text = "";
        cargar_DropDownList_BUSCAR();
    }

    private void cargar_GridView_RESULTADOS_BUSQUEDA()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Programa.Areas AREA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);

        ActividadRseGlobal _actividad = new ActividadRseGlobal(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaActividades = _actividad.ObtenerActividadesPorArea(AREA);

        if (tablaActividades.Rows.Count <= 0)
        {
            if (_actividad.MensajeError == null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros de Actividades.", Proceso.Advertencia);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _actividad.MensajeError, Proceso.Error);
            }

            Panel_RESULTADOS_GRID.Visible = false;
        }
        else
        {
            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaActividades;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
        }
    }

    private void Cargar_DropDownList_Tipo(DropDownList drop)
    {
        Programa.Areas AREA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);

        drop.Items.Clear();

        TipoActividad _tipoActividad = new TipoActividad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaParametros = _tipoActividad.ObtenerTiposActividadPorAreayEstado(AREA, true);

        ListItem item = new ListItem("Seleccione...", "");
        drop.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["NOMBRE"].ToString());
            drop.Items.Add(item);
        }
        drop.DataBind();
    }

    private void Cargar_DropDownList_Sector(DropDownList drop)
    {
        drop.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_SECTORES_ACTIVIDAD);

        ListItem item = new ListItem("Seleccione...", "");
        drop.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            drop.Items.Add(item);
        }
        drop.DataBind();
    }

    private void Cargar_DropDownList_EstadoActividad(DropDownList drop)
    {
        drop.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_ESTADO_ACTIVIDAD_RSE_GLOBAL);

        ListItem item = new ListItem("Seleccione...", "");
        drop.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            drop.Items.Add(item);
        }
        drop.DataBind();
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                configurarCaracteresAceptadosBusqueda(true, true);

                ConfigurarAreaRseGlobal();

                HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "INICIAL";
                HiddenField_FILTRO_DROP.Value = String.Empty;
                HiddenField_FILTRO_DATO.Value = String.Empty;

                iniciar_seccion_de_busqueda();

                cargar_GridView_RESULTADOS_BUSQUEDA();
                break;
            case Acciones.Cargar:
                Cargar_DropDownList_Tipo(DropDownList_Tipo);
                Cargar_DropDownList_Sector(DropDownList_Sector);
                Cargar_DropDownList_EstadoActividad(DropDownList_EstadoActividad);
                break;
            case Acciones.Nuevo:
                Cargar_DropDownList_Tipo(DropDownList_Tipo);
                Cargar_DropDownList_Sector(DropDownList_Sector);
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            Iniciar();
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
    private void Activar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Nuevo:
                TextBox_Nombre.Enabled = true;
                TextBox_Descripcion.Enabled = true;
                DropDownList_Tipo.Enabled = true;
                DropDownList_Sector.Enabled = true;
                break;
            case Acciones.Modificar:
                TextBox_Nombre.Enabled = true;
                DropDownList_Tipo.Enabled = true;
                DropDownList_Sector.Enabled = true;
                TextBox_Descripcion.Enabled = true;
                DropDownList_EstadoActividad.Enabled = true;
                break;
        }
    }

    private void Limpiar(Acciones accion)
    {
        HiddenField_ID_ACTIVIDAD.Value = "";

        switch (accion)
        {
            case Acciones.Nuevo:
                TextBox_Nombre.Text = "";
                TextBox_Descripcion.Text = "";
                break;
        }
    }

    protected void Button_NUEVO_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Nuevo);
        Desactivar(Acciones.Inicio);
        Activar(Acciones.Nuevo);
        Limpiar(Acciones.Nuevo);
        Cargar(Acciones.Nuevo);
    }
    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Modificar);
        Mostrar(Acciones.Modificar);
        Activar(Acciones.Modificar);
    }

    private void Guardar()
    {
        String NOMBRE = TextBox_Nombre.Text.Trim();

        String TIPO = DropDownList_Tipo.SelectedValue;
        String SECTOR = DropDownList_Sector.SelectedValue;

        String DESCRIPCION = TextBox_Descripcion.Text.Trim();

        Programa.Areas AREA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);

        ActividadRseGlobal _actividad = new ActividadRseGlobal(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Decimal ID_ACTIVIDAD = _actividad.Adicionar(NOMBRE, DESCRIPCION, TIPO, SECTOR, AREA);

        if (ID_ACTIVIDAD <= 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _actividad.MensajeError, Proceso.Error);
        }
        else
        {
            Cargar(ID_ACTIVIDAD);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La Actividad " + NOMBRE + " fue creada correctamente y se le asignó el ID: " + ID_ACTIVIDAD.ToString(), Proceso.Correcto);
        }
    }

    private void Actualizar()
    {
        Decimal ID_ACTIVIDAD = Convert.ToDecimal(HiddenField_ID_ACTIVIDAD.Value);

        String NOMBRE = TextBox_Nombre.Text.Trim();

        String TIPO = DropDownList_Tipo.SelectedValue;
        String SECTOR = DropDownList_Sector.SelectedValue;

        String DESCRIPCION = TextBox_Descripcion.Text.Trim();

        Boolean ACTIVO = true;

        if (DropDownList_EstadoActividad.SelectedValue == "False")
        {
            ACTIVO = false;
        }

        ActividadRseGlobal _actividad = new ActividadRseGlobal(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean verificado = _actividad.Actualizar(ID_ACTIVIDAD, NOMBRE, DESCRIPCION, TIPO, SECTOR, ACTIVO);

        if (verificado == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _actividad.MensajeError, Proceso.Error);
        }
        else
        {
            Cargar(ID_ACTIVIDAD);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La Actividad " + NOMBRE + " fue Actualizada correctamente.", Proceso.Correcto);
        }
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(HiddenField_ID_ACTIVIDAD.Value) == true)
        {
            Guardar();
        }
        else
        {
            Actualizar();
        }
    }
    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(HiddenField_ID_ACTIVIDAD.Value) == true)
        {
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);
        }
        else
        {
            Decimal ID_ACTIVIDAD = Convert.ToDecimal(HiddenField_ID_ACTIVIDAD.Value);

            Cargar(ID_ACTIVIDAD);
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
            if (DropDownList_BUSCAR.SelectedValue == "ID_ACTIVIDAD")
            {
                configurarCaracteresAceptadosBusqueda(false, true);
            }
            else
            {
                if (DropDownList_BUSCAR.SelectedValue == "NOMBRE")
                {
                    configurarCaracteresAceptadosBusqueda(true, false);
                }
                else
                {
                    if (DropDownList_BUSCAR.SelectedValue == "TIPO")
                    {
                        configurarCaracteresAceptadosBusqueda(true, false);
                    }
                    else
                    {
                        if (DropDownList_BUSCAR.SelectedValue == "SECTOR")
                        {
                            configurarCaracteresAceptadosBusqueda(true, false);
                        }
                    }
                }
            }
        }

        TextBox_BUSCAR.Text = "";
    }

    private void Buscar()
    {
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);

        String datosCapturados = HiddenField_FILTRO_DATO.Value;
        String campo = HiddenField_FILTRO_DROP.Value;

        Programa.Areas AREA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);

        ActividadRseGlobal _actividad = new ActividadRseGlobal(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaResultadosBusqueda = new DataTable();

        if (campo == "ID_ACTIVIDAD")
        {
            tablaResultadosBusqueda = _actividad.ObtenerActividadPorId(Convert.ToDecimal(datosCapturados), AREA);
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "NOMBRE")
            {
                tablaResultadosBusqueda = _actividad.ObtenerActividadesPorNombre(datosCapturados, AREA);
            }
            else
            {
                if (DropDownList_BUSCAR.SelectedValue == "TIPO")
                {
                    tablaResultadosBusqueda = _actividad.ObtenerActividadesPorTipo(datosCapturados, AREA);
                }
                else 
                {
                    if (DropDownList_BUSCAR.SelectedValue == "SECTOR")
                    {
                        tablaResultadosBusqueda = _actividad.ObtenerActividadesPorSector(datosCapturados, AREA);
                    }   
                }
            }
        }

        if (tablaResultadosBusqueda.Rows.Count <= 0)
        {
            if (_actividad.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _actividad.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros que cumplieran los datos de busqueda.", Proceso.Advertencia);
            }

            Panel_RESULTADOS_GRID.Visible = false;
        }
        else
        {
            Panel_RESULTADOS_GRID.Visible = true;

            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaResultadosBusqueda;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
        }
    }

    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "CON_FILTRO";

        HiddenField_FILTRO_DATO.Value = TextBox_BUSCAR.Text.Trim();
        HiddenField_FILTRO_DROP.Value = DropDownList_BUSCAR.SelectedValue;

        Buscar();
    }
    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;

        if (HiddenField_TIPO_BUSQUEDA_ACTUAL.Value == "INICIAL")
        {
            cargar_GridView_RESULTADOS_BUSQUEDA();
        }
        else
        {
            Buscar();
        }
    }

    private void CargarControlRegistro(DataRow filaActividad)
    {
        TextBox_USU_CRE.Text = filaActividad["USU_CRE"].ToString();
        try
        {
            TextBox_FCH_CRE.Text = DateTime.Parse(filaActividad["FCH_CRE"].ToString()).ToShortDateString();
            TextBox_HOR_CRE.Text = DateTime.Parse(filaActividad["FCH_CRE"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_CRE.Text = "";
            TextBox_HOR_CRE.Text = "";
        }
        TextBox_USU_MOD.Text = filaActividad["USU_MOD"].ToString();
        try
        {
            TextBox_FCH_MOD.Text = DateTime.Parse(filaActividad["FCH_MOD"].ToString()).ToShortDateString();
            TextBox_HOR_MOD.Text = DateTime.Parse(filaActividad["FCH_MOD"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_MOD.Text = "";
            TextBox_HOR_MOD.Text = "";
        }
    }

    private void CargarInformacionActividad(DataRow filaActividad)
    {
        TextBox_Nombre.Text = filaActividad["NOMBRE"].ToString().Trim();

        DropDownList_Tipo.SelectedValue = filaActividad["TIPO"].ToString().Trim();
        DropDownList_Sector.SelectedValue = filaActividad["SECTOR"].ToString().Trim();

        TextBox_Descripcion.Text = filaActividad["DESCRIPCION"].ToString().Trim();

        DropDownList_EstadoActividad.SelectedValue = filaActividad["ACTIVO"].ToString().Trim();
    }

    private void Cargar(Decimal ID_ACTIVIDAD)
    {
        HiddenField_ID_ACTIVIDAD.Value = ID_ACTIVIDAD.ToString();

        Programa.Areas AREA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);

        ActividadRseGlobal _actividad = new ActividadRseGlobal(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaActividad = _actividad.ObtenerActividadPorId(ID_ACTIVIDAD, AREA);

        if (tablaActividad.Rows.Count <= 0)
        {
            if (String.IsNullOrEmpty(_actividad.MensajeError) == false)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _actividad.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información de la actividad seleccionada", Proceso.Error);
            }
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Cargar);
            Cargar(Acciones.Cargar);

            DataRow filaActividad = tablaActividad.Rows[0];

            CargarControlRegistro(filaActividad);

            CargarInformacionActividad(filaActividad);
        }
    }

    protected void GridView_RESULTADOS_BUSQUEDA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal ID_ACTIVIDAD = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_ACTIVIDAD"]);

            Cargar(ID_ACTIVIDAD);
        }
    }
}