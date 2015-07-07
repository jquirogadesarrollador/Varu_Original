using System;
using System.Web.UI.WebControls;
using Brainsbits.LLB.maestras;
using TSHAK.Components;
using Brainsbits.LLB.comercial;
using System.Data;
using Brainsbits.LLB;
using System.Collections.Generic;
using Brainsbits.LLB.seguridad;
using System.Web;

public partial class _Default : System.Web.UI.Page
{

    private String NOMBRE_AREA = tabla.NOMBRE_AREA_GESTION_COMERCIAL;

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
        CargarContrato,
        ModificarContrato,
        NuevoContrato
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }

    private enum AccionesGrilla
    {
        Nuevo = 0,
        Ninguna
    }

    private void configurar_paneles_popup(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_mensaje.CssClass = "panel_popup";
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

                Panel_FORM_BOTONES_ENCABEZADO.Visible = false;
                Panel_FORM_BOTONES_PIE.Visible = false;
                Button_NUEVO.Visible = false;
                Button_NUEVO_1.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_CANCELAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;

                Panel_RESULTADOS_GRID.Visible = false;

                Panel_FORMULARIO.Visible = false;
                Panel_CONTROL_REGISTRO.Visible = false;
                Panel_IDENTIFICADOR.Visible = false;
                Panel_OBJETIVO_CONTRATO.Visible = false;
                Panel_FIRMADO.Visible = false;
                Panel_Enviado.Visible = false;
                Panel_HistorialEnvios.Visible = false;
                Panel_BotonesAccion.Visible = false;
                Button_NUEVA_ACCION.Visible = false;
                Button_GUARDAR_ACCION.Visible = false;
                Button_CANCELAR_ACCION.Visible = false;
                break;
            case Acciones.ModificarContrato:
                Button_NUEVO.Visible = false;
                Button_NUEVO_1.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_CANCELAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;

                Panel_CONTROL_REGISTRO.Visible = false;

                Button_NUEVA_ACCION.Visible = false;
                Button_GUARDAR_ACCION.Visible = false;
                Button_CANCELAR_ACCION.Visible = false;
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

                TextBox_NUMERO_CONTRATO.Enabled = false;

                TextBox_fecha_vencimiento_contrato_comercial.Enabled = false;
                TextBox_inicio_contrato_comercial.Enabled = false;

                DropDownList_TIPO_SERVICIO_RESPECTIVO.Enabled = false;
                TextBox_DetalleServicioRespectivo.Enabled = false;

                CheckBox_FIRMADO.Enabled = false;
                CheckBox_ENVIO_CTE.Enabled = false;
                break;
        }
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Panel_FORM_BOTONES_PIE.Visible = true;
                Button_NUEVO.Visible = true;
                Button_NUEVO_1.Visible = true;

                Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.CargarContrato:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Panel_FORM_BOTONES_PIE.Visible = true;
                Button_MODIFICAR.Visible = true;
                Button_MODIFICAR_1.Visible = true;

                Panel_FORMULARIO.Visible = true;
                Panel_CONTROL_REGISTRO.Visible = true;
                Panel_IDENTIFICADOR.Visible = true;
                Panel_OBJETIVO_CONTRATO.Visible = true;
                Panel_FIRMADO.Visible = true;
                Panel_Enviado.Visible = true;
                break;
            case Acciones.ModificarContrato:
                Button_GUARDAR.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR.Visible = true;
                Button_CANCELAR_1.Visible = true;

                Panel_HistorialEnvios.Visible = true;
                Panel_BotonesAccion.Visible = true;
                Button_NUEVA_ACCION.Visible = true;
                break;
            case Acciones.NuevoContrato:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Panel_FORM_BOTONES_PIE.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR.Visible = true;
                Button_CANCELAR_1.Visible = true;

                Panel_FORMULARIO.Visible = true;
                Panel_IDENTIFICADOR.Visible = true;

                Panel_OBJETIVO_CONTRATO.Visible = true;

                Panel_FIRMADO.Visible = true;

                Panel_Enviado.Visible = true;

                Panel_HistorialEnvios.Visible = true;
                Panel_BotonesAccion.Visible = true;
                Button_NUEVA_ACCION.Visible = true;
            break;
        }
    }

    private void cargar_GridView_RESULTADOS_BUSQUEDA()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String reg = QueryStringSeguro["reg"].ToString();
        contratosServicio _contratosServico = new contratosServicio(Session["idEmpresa"].ToString());

        DataTable tablaContratosOriginal = _contratosServico.ObtenerContratosDeServicioPorIdEmpresa(Convert.ToDecimal(reg));

        if (tablaContratosOriginal.Rows.Count <= 0)
        {
            if (_contratosServico.MensajeError == null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros de Contratos de Servicio para esta empresa.", Proceso.Advertencia);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _contratosServico.MensajeError, Proceso.Error);
            }

            Panel_RESULTADOS_GRID.Visible = false;
        }
        else
        {
            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaContratosOriginal;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
        }
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                cargar_GridView_RESULTADOS_BUSQUEDA();
                break;
            case Acciones.NuevoContrato:
                HiddenField_REGISTRO_CONTRATO.Value = "";

                TextBox_NUMERO_CONTRATO.Text = "";

                cargar_DropDownList_TIPO_SERVICIO_RESPECTIVO();
                TextBox_DetalleServicioRespectivo.Text = "";

                CheckBox_FIRMADO.Checked = false;
                
                CheckBox_ENVIO_CTE.Checked = false;
                HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();

                GridView_HistorialEnvios.DataSource = null;
                GridView_HistorialEnvios.DataBind();

                HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_FILA_SELECCIONADA_GRILLA.Value = "";
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

    protected void Button_NUEVO_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.NuevoContrato);
        Activar(Acciones.NuevoContrato);
        Cargar(Acciones.NuevoContrato);
    }

    private void Activar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.ModificarContrato:
                TextBox_NUMERO_CONTRATO.Enabled = true;
                TextBox_fecha_vencimiento_contrato_comercial.Enabled = true;
                TextBox_inicio_contrato_comercial.Enabled = true;

                DropDownList_TIPO_SERVICIO_RESPECTIVO.Enabled = true;
                TextBox_DetalleServicioRespectivo.Enabled = true;

                CheckBox_FIRMADO.Enabled = true;
                CheckBox_ENVIO_CTE.Enabled = true;
                break;
            case Acciones.NuevoContrato:
                TextBox_NUMERO_CONTRATO.Enabled = true;

                TextBox_fecha_vencimiento_contrato_comercial.Enabled = true;
                TextBox_inicio_contrato_comercial.Enabled = true;


                DropDownList_TIPO_SERVICIO_RESPECTIVO.Enabled = true;
                TextBox_DetalleServicioRespectivo.Enabled = true;

                CheckBox_FIRMADO.Enabled = true;
                CheckBox_ENVIO_CTE.Enabled = true;
                break;
        }
    }

    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.ModificarContrato);
        Activar(Acciones.ModificarContrato);
        Mostrar(Acciones.ModificarContrato);
    }

    private void Modificar()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

        Decimal REGISTRO_CONTRATO = Convert.ToDecimal(HiddenField_REGISTRO_CONTRATO.Value);

        String NUMERO_CONTRATO = TextBox_NUMERO_CONTRATO.Text.Trim();

        DateTime FECHA_INICIO = Convert.ToDateTime(TextBox_inicio_contrato_comercial.Text);
        DateTime FECHA_VENCE = Convert.ToDateTime(TextBox_fecha_vencimiento_contrato_comercial.Text);

        String TIPO_CONTRATO = DropDownList_TIPO_SERVICIO_RESPECTIVO.SelectedValue;
        String OBJ_CONTRATO = TextBox_DetalleServicioRespectivo.Text.Trim();
        
        String FIRMADO = "N";
        if (CheckBox_FIRMADO.Checked == true)
        {
            FIRMADO = "S";
        }

        String ENVIADO = "N";
        if (CheckBox_ENVIO_CTE.Checked == true)
        {
            ENVIADO = "S";
        }

        List<HistorialEnvioDevolucion> listaEnviosDevoluciones = new List<HistorialEnvioDevolucion>();
    
        for (int i = 0; i < GridView_HistorialEnvios.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_HistorialEnvios.Rows[i];

            Decimal ID_ACCION = Convert.ToDecimal(GridView_HistorialEnvios.DataKeys[i].Values["ID_ACCION"]);

            if (ID_ACCION <= 0)
            {
                Decimal ID_CONTRATO = Convert.ToDecimal(HiddenField_REGISTRO_CONTRATO.Value);
                
                TextBox textoFechaAccion = filaGrilla.FindControl("TextBox_FECHA_ACCION") as TextBox;
                DateTime FECHA_ACCION = Convert.ToDateTime(textoFechaAccion.Text);

                DropDownList dropTipoAccion = filaGrilla.FindControl("DropDownList_TIPO_ACCION") as DropDownList;
                String TIPO_ACCION = dropTipoAccion.SelectedValue;

                TextBox textoObservaciones = filaGrilla.FindControl("TextBox_OBSERVACIONES_ACCION") as TextBox;
                String OBSERVACIONES = textoObservaciones.Text.Trim();

                HistorialEnvioDevolucion _envioDevolucion = new HistorialEnvioDevolucion();

                _envioDevolucion.FECHA_ACCION = FECHA_ACCION;
                _envioDevolucion.ID_ACCION = ID_ACCION;
                _envioDevolucion.ID_CONTRATO = ID_CONTRATO;
                _envioDevolucion.OBSERVACIONES = OBSERVACIONES;
                _envioDevolucion.TIPO_ACCION = TIPO_ACCION;

                listaEnviosDevoluciones.Add(_envioDevolucion);
            }
        }

        contratosServicio _contratosServicio = new contratosServicio(Session["idEmpresa"].ToString());

        Boolean verificador = _contratosServicio.Actualizar(REGISTRO_CONTRATO, NUMERO_CONTRATO, FECHA_INICIO, FECHA_VENCE, TIPO_CONTRATO, OBJ_CONTRATO, FIRMADO, ENVIADO, Session["USU_LOG"].ToString(), listaEnviosDevoluciones, FECHA_INICIO, FECHA_VENCE);

        if (verificador == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _contratosServicio.MensajeError, Proceso.Error);
        }
        else
        {

            DataTable tablainfoContrato = _contratosServicio.ObtenerContratosDeServicioIdContrato(REGISTRO_CONTRATO);
            Cargar(tablainfoContrato.Rows[0]);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El Contrato de Servicio / Servicio Respectivo fue modificado correctamente.", Proceso.Correcto);
        }   
    }

    private void Guardar()
    {

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

        DateTime FECHA_INICIO = Convert.ToDateTime(TextBox_inicio_contrato_comercial.Text);

        String OBJ_CONTRATO = TextBox_DetalleServicioRespectivo.Text.Trim();

        DateTime FECHA_VENCE = Convert.ToDateTime(TextBox_fecha_vencimiento_contrato_comercial.Text);

        String FIRMADO = "N";
        if (CheckBox_FIRMADO.Checked == true)
        {
            FIRMADO = "S";
        }

        String ENVIADO = "N";
        if (CheckBox_ENVIO_CTE.Checked == true)
        {
            ENVIADO = "S";
        }

        String NUMERO_CONTRATO = TextBox_NUMERO_CONTRATO.Text.Trim();

        String TIPO_CONTRATO = DropDownList_TIPO_SERVICIO_RESPECTIVO.SelectedValue;

        List<HistorialEnvioDevolucion> listaEnviosDevoluciones = new List<HistorialEnvioDevolucion>();

        for (int i = 0; i < GridView_HistorialEnvios.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_HistorialEnvios.Rows[i];

            Decimal ID_ACCION = Convert.ToDecimal(GridView_HistorialEnvios.DataKeys[i].Values["ID_ACCION"]);

            if (ID_ACCION <= 0)
            {
                Decimal ID_CONTRATO = Convert.ToDecimal(HiddenField_REGISTRO_CONTRATO.Value);

                TextBox textoFechaAccion = filaGrilla.FindControl("TextBox_FECHA_ACCION") as TextBox;
                DateTime FECHA_ACCION = Convert.ToDateTime(textoFechaAccion.Text);

                DropDownList dropTipoAccion = filaGrilla.FindControl("DropDownList_TIPO_ACCION") as DropDownList;
                String TIPO_ACCION = dropTipoAccion.SelectedValue;

                TextBox textoObservaciones = filaGrilla.FindControl("TextBox_OBSERVACIONES_ACCION") as TextBox;
                String OBSERVACIONES = textoObservaciones.Text.Trim();

                HistorialEnvioDevolucion _envioDevolucion = new HistorialEnvioDevolucion();

                _envioDevolucion.FECHA_ACCION = FECHA_ACCION;
                _envioDevolucion.ID_ACCION = ID_ACCION;
                _envioDevolucion.ID_CONTRATO = ID_CONTRATO;
                _envioDevolucion.OBSERVACIONES = OBSERVACIONES;
                _envioDevolucion.TIPO_ACCION = TIPO_ACCION;

                listaEnviosDevoluciones.Add(_envioDevolucion);
            }
        }

        contratosServicio _contratosServicio = new contratosServicio(Session["idEmpresa"].ToString());


        Decimal REGISTRO_CONTRATO = _contratosServicio.Adicionar(ID_EMPRESA, FECHA_INICIO, OBJ_CONTRATO, FECHA_VENCE, FIRMADO, ENVIADO, Session["USU_LOG"].ToString(), NUMERO_CONTRATO, TIPO_CONTRATO, listaEnviosDevoluciones, FECHA_INICIO, FECHA_VENCE);

        if (REGISTRO_CONTRATO <= 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _contratosServicio.MensajeError, Proceso.Error);
        }
        else
        {
            DataTable tablainfoContrato = _contratosServicio.ObtenerContratosDeServicioIdContrato(REGISTRO_CONTRATO);
            Cargar(tablainfoContrato.Rows[0]);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El Contrato de Servicio / Servicio Respectivo fue creado correctamente y se le asignó el ID: " + REGISTRO_CONTRATO.ToString() + ".", Proceso.Correcto);
        }   
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(HiddenField_REGISTRO_CONTRATO.Value) == false)
        {
            Modificar();
        }
        else
        {
            Guardar();
        }
    }

    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }

    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;

        cargar_GridView_RESULTADOS_BUSQUEDA();
    }

    private void cargar_informacion_registro_control(DataRow filaInfoContrato)
    {
        TextBox_USU_CRE.Text = filaInfoContrato["USU_CRE"].ToString();
        try
        {
            TextBox_FCH_CRE.Text = DateTime.Parse(filaInfoContrato["FCH_CRE"].ToString()).ToShortDateString();
            TextBox_HOR_CRE.Text = DateTime.Parse(filaInfoContrato["FCH_CRE"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_CRE.Text = "";
            TextBox_HOR_CRE.Text = "";
        }
        TextBox_USU_MOD.Text = filaInfoContrato["USU_MOD"].ToString();
        try
        {
            TextBox_FCH_MOD.Text = DateTime.Parse(filaInfoContrato["FCH_MOD"].ToString()).ToShortDateString();
            TextBox_HOR_MOD.Text = DateTime.Parse(filaInfoContrato["FCH_MOD"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_MOD.Text = "";
            TextBox_HOR_MOD.Text = "";
        }

    }

    private void cargar_GridView_HistorialEnvios_desde_tabla(DataTable tablaHistorial)
    {
        GridView_HistorialEnvios.DataSource = tablaHistorial;
        GridView_HistorialEnvios.DataBind();

        GridViewRow filaGrilla;
        DataRow filaTabla;

        for (int i = 0; i < tablaHistorial.Rows.Count; i++)
        {
            filaGrilla = GridView_HistorialEnvios.Rows[i];
            filaTabla = tablaHistorial.Rows[i];

            TextBox textoFechaAccion = filaGrilla.FindControl("TextBox_FECHA_ACCION") as TextBox;
            try
            {
                textoFechaAccion.Text = Convert.ToDateTime(filaTabla["FECHA_ACCION"]).ToShortDateString();
            }
            catch
            {
                textoFechaAccion.Text = "";
            }

            DropDownList dropTipoAccion = filaGrilla.FindControl("DropDownList_TIPO_ACCION") as DropDownList;
            try
            {
                dropTipoAccion.SelectedValue = filaTabla["TIPO_ACCION"].ToString();
            }
            catch
            {
                dropTipoAccion.Text = "";
            }

            TextBox textoObservaciones = filaGrilla.FindControl("TextBox_OBSERVACIONES_ACCION") as TextBox;
            textoObservaciones.Text = filaTabla["OBSERVACIONES"].ToString().Trim();
        }
    }

    private void CargarHistorialEnviosDevoluciones(Decimal REGISTRO)
    {
        contratosServicio _contrato = new contratosServicio(Session["idEmpresa"].ToString());

        DataTable tablaHistorial = _contrato.ObtenerHistorialEnviosDevolucionesPorIdContrato(REGISTRO);

        if (tablaHistorial.Rows.Count <= 0)
        {
            if (_contrato.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _contrato.MensajeError, Proceso.Error);
            }


            Panel_HistorialEnvios.Visible = false;
        }
        else
        {
            Panel_HistorialEnvios.Visible = true;

            cargar_GridView_HistorialEnvios_desde_tabla(tablaHistorial);
        }
    }

    private void inhabilitarTodasFilasGrilla(GridView grilla, int colInicio)
    {
        for (int i = 0; i < grilla.Rows.Count; i++)
        {
            for (int j = colInicio; j < grilla.Columns.Count; j++)
            {
                grilla.Rows[i].Cells[j].Enabled = false;
            }
        }
    }

    private void inhabilitarFilaGrilla(GridView grilla, int numFila, int colInicio)
    {
        for (int j = colInicio; j < grilla.Columns.Count; j++)
        {
            grilla.Rows[numFila].Cells[j].Enabled = false;
        }
    }

    private void habilitarFilaGrilla(GridView grilla, int numFila, int colInicio)
    {
        for (int j = colInicio; j < grilla.Columns.Count; j++)
        {
            grilla.Rows[numFila].Cells[j].Enabled = true;
        }
    }

    private void cargar_DropDownList_TIPO_SERVICIO_RESPECTIVO()
    {
        DropDownList_TIPO_SERVICIO_RESPECTIVO.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());
        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_SERVICIOS_RESPECTIVOS);

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_TIPO_SERVICIO_RESPECTIVO.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_TIPO_SERVICIO_RESPECTIVO.Items.Add(item);
        }

        DropDownList_TIPO_SERVICIO_RESPECTIVO.DataBind();
    }

    private void Cargar(DataRow filaInfoContrato)
    {

        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Mostrar(Acciones.CargarContrato);

        Decimal REGISTRO = Convert.ToDecimal(filaInfoContrato["REGISTRO"]);

        cargar_informacion_registro_control(filaInfoContrato);

        HiddenField_REGISTRO_CONTRATO.Value = filaInfoContrato["REGISTRO"].ToString().Trim();

        TextBox_NUMERO_CONTRATO.Text = filaInfoContrato["NUMERO_CONTRATO"].ToString().Trim();

        TextBox_inicio_contrato_comercial.Text = Convert.ToDateTime(filaInfoContrato["FECHA"]).ToShortDateString(); 
        TextBox_fecha_vencimiento_contrato_comercial.Text = Convert.ToDateTime(filaInfoContrato["FCH_VENCE"]).ToShortDateString();

        cargar_DropDownList_TIPO_SERVICIO_RESPECTIVO();
        try
        {
            DropDownList_TIPO_SERVICIO_RESPECTIVO.SelectedValue = filaInfoContrato["TIPO_CONTRATO"].ToString().Trim();
        }
        catch
        {
            DropDownList_TIPO_SERVICIO_RESPECTIVO.SelectedIndex = 0;
        }
        TextBox_DetalleServicioRespectivo.Text = filaInfoContrato["OBJ_CONTRATO"].ToString().Trim();

        if (filaInfoContrato["FIRMADO"].ToString().ToUpper() == "S")
        {
            CheckBox_FIRMADO.Checked = true;
        }
        else
        {
            CheckBox_FIRMADO.Checked = false;
        }

        if (filaInfoContrato["ENVIO_CTE"].ToString().ToUpper() == "S")
        {
            CheckBox_ENVIO_CTE.Checked = true;
        }
        else
        {
            CheckBox_ENVIO_CTE.Checked = false;
        }
        CargarHistorialEnviosDevoluciones(REGISTRO);
        inhabilitarTodasFilasGrilla(GridView_HistorialEnvios, 0);


        DateTime fechaActual = DateTime.Parse(DateTime.Now.ToShortDateString());
        DateTime fechaVencimientoContrato = Convert.ToDateTime(filaInfoContrato["FCH_VENCE"]);

        if (fechaVencimientoContrato < fechaActual)
        {
            Button_MODIFICAR.Visible = false;
            Button_MODIFICAR_1.Visible = false;
            Panel_FORM_BOTONES_PIE.Visible = false;
        }
        else
        {
            Button_MODIFICAR.Visible = true;
            Button_MODIFICAR_1.Visible = true;
            Panel_FORM_BOTONES_PIE.Visible = true;
        }
    }

    protected void GridView_RESULTADOS_BUSQUEDA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal REGISTRO = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["REGISTRO"]);

            contratosServicio _contratosServicio = new contratosServicio(Session["idEmpresa"].ToString());
            DataTable tablainfoContrato = _contratosServicio.ObtenerContratosDeServicioIdContrato(REGISTRO);

            Cargar(tablainfoContrato.Rows[0]);
        }
    }

    private DataTable obtenerDataTableDe_GridView_HistorialEnvios()
    {
        DataTable tablaResultado = new DataTable();
        tablaResultado.Columns.Add("ID_ACCION");
        tablaResultado.Columns.Add("FECHA_ACCION");
        tablaResultado.Columns.Add("TIPO_ACCION");
        tablaResultado.Columns.Add("OBSERVACIONES");

        DataRow filaTablaResultado;

        Decimal ID_ACCION = 0;
        String FECHA_ACCION;
        String TIPO_ACCION;
        String OBSERVACIONES;

        for (int i = 0; i < GridView_HistorialEnvios.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_HistorialEnvios.Rows[i];

            ID_ACCION = Convert.ToDecimal(GridView_HistorialEnvios.DataKeys[i].Values["ID_ACCION"]);

            TextBox textoFechaAccion = filaGrilla.FindControl("TextBox_FECHA_ACCION") as TextBox;
            try
            {
                FECHA_ACCION = Convert.ToDateTime(textoFechaAccion.Text).ToShortDateString();
            }
            catch
            {
                FECHA_ACCION = "";
            }

            DropDownList dropTipoAccion = filaGrilla.FindControl("DropDownList_TIPO_ACCION") as DropDownList;
            TIPO_ACCION = dropTipoAccion.SelectedValue;

            TextBox textoObservaciones = filaGrilla.FindControl("TextBox_OBSERVACIONES_ACCION") as TextBox;
            OBSERVACIONES = textoObservaciones.Text.Trim();

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_ACCION"] = ID_ACCION;
            filaTablaResultado["FECHA_ACCION"] = FECHA_ACCION;
            filaTablaResultado["TIPO_ACCION"] = TIPO_ACCION;
            filaTablaResultado["OBSERVACIONES"] = OBSERVACIONES;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }

    protected void Button_NUEVA_ACCION_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerDataTableDe_GridView_HistorialEnvios();
        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_ACCION"] = 0;
        filaNueva["FECHA_ACCION"] = "";
        filaNueva["TIPO_ACCION"] = "";
        filaNueva["OBSERVACIONES"] = "";

        tablaDesdeGrilla.Rows.Add(filaNueva);

        cargar_GridView_HistorialEnvios_desde_tabla(tablaDesdeGrilla);

        inhabilitarTodasFilasGrilla(GridView_HistorialEnvios, 0);
        habilitarFilaGrilla(GridView_HistorialEnvios, GridView_HistorialEnvios.Rows.Count - 1, 0);
        

        HiddenField_ID_ACCION.Value = String.Empty;
        HiddenField_FECHA_ACCION.Value = String.Empty;
        HiddenField_TIPO_ACCION.Value = String.Empty;
        HiddenField_OBSERVACIONES.Value = String.Empty; ;

        Button_NUEVA_ACCION.Visible = false;
        Button_GUARDAR_ACCION.Visible = true;
        Button_CANCELAR_ACCION.Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_SELECCIONADA_GRILLA.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();
    }

    protected void Button_GUARDAR_ACCION_Click(object sender, EventArgs e)
    {
        int FILA_SELECCIONADA = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA.Value);

        inhabilitarFilaGrilla(GridView_HistorialEnvios, FILA_SELECCIONADA, 0);

        Button_GUARDAR_ACCION.Visible = false;
        Button_CANCELAR_ACCION.Visible = false;
        Button_NUEVA_ACCION.Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
    }
    protected void Button_CANCELAR_ACCION_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = obtenerDataTableDe_GridView_HistorialEnvios();

        if (HiddenField_ACCION_GRILLA.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }

        cargar_GridView_HistorialEnvios_desde_tabla(tablaGrilla);

        inhabilitarTodasFilasGrilla(GridView_HistorialEnvios, 0);

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();

        Button_NUEVA_ACCION.Visible = true;
        Button_GUARDAR_ACCION.Visible = false;
        Button_CANCELAR_ACCION.Visible = false;
    }
}