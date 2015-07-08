using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brainsbits.LLB.seleccion;
using System.Data;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB;
using Brainsbits.LLB.comercial;
using Brainsbits.LLB.seguridad;
using TSHAK.Components;
using System.Web;

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

    private enum Acciones
    {
        Inicio = 0,
        CargarReqCancelada = 1,
        CargarReqCumplida = 2,
        CargarReqNormal = 3,
        Cancelar = 4,
        Cumplir = 5
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

    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = false;
                Panel_FORM_BOTONES.Visible = false;

                CheckBoxList_CANCELADO_CUMPLIDO.Visible = false;

                Panel_GRID_RESULTADOS.Visible = false;

                Panel_FORMULARIO.Visible = false;
                Panel_CONTROL_REGISTRO.Visible = false;
                Panel_DATOS_FORMULARIO.Visible = false;
                Panel_EMPRESA_FECHAS.Visible = false;
                Panel_DATOS_ENVIO.Visible = false;
                Panel_CONTRATO_Y_SERVICIOS_RESPECTIVOS.Visible = false;
                Panel_ESPECIFICACIONES_UBICACIONES.Visible = false;
                Panel_OBSERVACIONES.Visible = false;

                Panel_BOTONES_ACCIONES_REQUISICION.Visible = false;

                Panel_EfectividadOportunidad.Visible = false;

                Panel_CANCELAR_REQUISICION.Visible = false;
                Panel_CANCELACION_POR_CLIENTE.Visible = false;
                Panel_CUMPLIR_REQUISICION.Visible = false;

                Button_GUARDAR_CANCELACION.Visible = false;
                Button_GUARDAR_CUMPLIR.Visible = false;
                break;
            case Acciones.Cumplir:
                Panel_CUMPLIR_REQUISICION.Visible = false;
                Button_GUARDAR_CUMPLIR.Visible = false;
                break;
            case Acciones.Cancelar:
                Panel_CANCELAR_REQUISICION.Visible = false;
                Panel_CANCELACION_POR_CLIENTE.Visible = false;
                Button_GUARDAR_CANCELACION.Visible = false;
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

                DropDownList_TIP_REQ.Enabled = false;
                DropDownList_ID_EMPRESA.Enabled = false;
                TextBox_FECHA_R.Enabled = false;
                TextBox_FECHA_REQUERIDA.Enabled = false;
                DropDownList_ENVIO_CANDIDATOS.Enabled = false;
                TextBox_DIR_ENVIO.Enabled = false;
                TextBox_CIUDAD_ENVIO.Enabled = false;
                TextBox_EMAIL_ENVIO.Enabled = false;
                TextBox_TEL_ENVIO.Enabled = false;
                TextBox_COND_ENVIO.Enabled = false;
                TextBox_FECHA_VENCE_SERVICIO.Enabled = false;
                TextBox_DESCRIPCION_SERVICIO.Enabled = false;
                DropDownList_PERFILES.Enabled = false;
                TextBox_EDAD_MIN.Enabled = false;
                TextBox_EDAD_MAX.Enabled = false;
                TextBox_CANTIDAD.Enabled = false;
                TextBox_SEXO.Enabled = false;
                TextBox_EXPERIENCIA.Enabled = false;
                TextBox_NIVEL_ACADEMICO.Enabled = false;
                TextBox_FUNCIONES.Enabled = false;
                DropDownList_REGIONAL.Enabled = false;
                DropDownList_DEPARTAMENTO.Enabled = false;
                DropDownList_CIUDAD_REQ.Enabled = false;
                TextBox_DURACION.Enabled = false;
                TextBox_HORARIO.Enabled = false;
                TextBox_SALARIO.Enabled = false;
                TextBox_OBS_REQUERIMIENTO.Enabled = false;

                RadioButtonList_TIPO_CANCELACION.Enabled = false;
                DropDownList_MOTIVO_CANCELACION.Enabled = false;
                DropDownList_MOTIVO_CUMPLIR.Enabled = false;

                CheckBox_ReqEfectiva.Enabled = false;
                CheckBox_ReqOportuna.Enabled = false;
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
            case Acciones.CargarReqCancelada:
                Panel_FORM_BOTONES.Visible = true;
                Panel_FORMULARIO.Visible = true;
                Panel_CONTROL_REGISTRO.Visible = true;
                Panel_DATOS_FORMULARIO.Visible = true;
                Panel_EMPRESA_FECHAS.Visible = true;
                Panel_DATOS_ENVIO.Visible = true;
                Panel_CONTRATO_Y_SERVICIOS_RESPECTIVOS.Visible = true;
                Panel_ESPECIFICACIONES_UBICACIONES.Visible = true;
                Panel_OBSERVACIONES.Visible = true;

                Panel_CANCELAR_REQUISICION.Visible = true;
                Panel_CANCELACION_POR_CLIENTE.Visible = true;

                Panel_EfectividadOportunidad.Visible = true;
                break;
            case Acciones.CargarReqCumplida:
                Panel_FORM_BOTONES.Visible = true;
                Panel_FORMULARIO.Visible = true;
                Panel_CONTROL_REGISTRO.Visible = true;
                Panel_DATOS_FORMULARIO.Visible = true;
                Panel_EMPRESA_FECHAS.Visible = true;
                Panel_DATOS_ENVIO.Visible = true;
                Panel_CONTRATO_Y_SERVICIOS_RESPECTIVOS.Visible = true;
                Panel_ESPECIFICACIONES_UBICACIONES.Visible = true;
                Panel_OBSERVACIONES.Visible = true;

                Panel_CUMPLIR_REQUISICION.Visible = true;

                Panel_EfectividadOportunidad.Visible = true;
                break;
            case Acciones.CargarReqNormal:
                Panel_FORM_BOTONES.Visible = true;
                Panel_FORMULARIO.Visible = true;
                Panel_CONTROL_REGISTRO.Visible = true;
                Panel_DATOS_FORMULARIO.Visible = true;
                Panel_EMPRESA_FECHAS.Visible = true;
                Panel_DATOS_ENVIO.Visible = true;
                Panel_CONTRATO_Y_SERVICIOS_RESPECTIVOS.Visible = true;
                Panel_ESPECIFICACIONES_UBICACIONES.Visible = true;
                Panel_OBSERVACIONES.Visible = true;

                Panel_BOTONES_ACCIONES_REQUISICION.Visible = true;

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

        item = new ListItem("Código de Empresa", "COD_EMPRESA");
        DropDownList_BUSCAR.Items.Add(item);

        item = new ListItem("Razón social", "RAZ_SOCIAL");
        DropDownList_BUSCAR.Items.Add(item);

        item = new ListItem("Código de req", "ID_REQUERIMIENTO");
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
                HiddenField_CUMPLIDO.Value = "";
                HiddenField_CANCELADO.Value = "";

                iniciar_seccion_de_busqueda();
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
        Page.Header.Title = "CANCELACIÓN Y CUMPLIMIENTO DE REQUISICIONES";

        if (IsPostBack == false)
        {
            Iniciar();
        }
    }
    protected void DropDownList_BUSCAR_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_BUSCAR.SelectedValue == "")
        {
            CheckBoxList_CANCELADO_CUMPLIDO.Visible = false;
            configurarCaracteresAceptadosBusqueda(true, true);
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "COD_EMPRESA")
            {
                CheckBoxList_CANCELADO_CUMPLIDO.Visible = true;
                configurarCaracteresAceptadosBusqueda(false, true);
                CheckBoxList_CANCELADO_CUMPLIDO.SelectedIndex = -1;

                HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "CON_FILTRO";
            }
            else
            {
                if (DropDownList_BUSCAR.SelectedValue == "RAZ_SOCIAL")
                {
                    CheckBoxList_CANCELADO_CUMPLIDO.Visible = true;
                    configurarCaracteresAceptadosBusqueda(true, false);
                    CheckBoxList_CANCELADO_CUMPLIDO.SelectedIndex = -1;
                }
                else
                {
                    if (DropDownList_BUSCAR.SelectedValue == "ID_REQUERIMIENTO")
                    {
                        CheckBoxList_CANCELADO_CUMPLIDO.Visible = false;
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
        Desactivar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);

        String datosCapturados = HiddenField_FILTRO_DATO.Value;
        String campo = HiddenField_FILTRO_DROP.Value;

        String CUMPLIDO = HiddenField_CUMPLIDO.Value;
        String CANCELADO = HiddenField_CANCELADO.Value;

        requisicion _requisicion = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaResultadosBusqueda = new DataTable();

        if (campo == "RAZ_SOCIAL")
        {
            tablaResultadosBusqueda = _requisicion.ObtenerComRequerimientoPorRazSocial(datosCapturados, CANCELADO, CUMPLIDO);
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "COD_EMPRESA")
            {
                tablaResultadosBusqueda = _requisicion.ObtenerComRequerimientoPorCodEmpresa(datosCapturados, CANCELADO, CUMPLIDO);
            }
            else
            {
                if (DropDownList_BUSCAR.SelectedValue == "ID_REQUERIMIENTO")
                {
                    tablaResultadosBusqueda = _requisicion.ObtenerComRequerimientoPorIdRequerimiento(Convert.ToDecimal(datosCapturados));
                }
            }
        }

        if (tablaResultadosBusqueda.Rows.Count <= 0)
        {
            if (_requisicion.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _requisicion.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros que cumplieran los datos de busqueda.", Proceso.Advertencia);
            }

            Panel_GRID_RESULTADOS.Visible = false;
        }
        else
        {
            Panel_GRID_RESULTADOS.Visible = true;

            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaResultadosBusqueda;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
        }
    }

    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "CON_FILTRO";
        HiddenField_FILTRO_DROP.Value = DropDownList_BUSCAR.SelectedValue;
        HiddenField_FILTRO_DATO.Value = TextBox_BUSCAR.Text;

        HiddenField_CUMPLIDO.Value = "N";
        HiddenField_CANCELADO.Value = "N";
        if (CheckBoxList_CANCELADO_CUMPLIDO.Visible == true)
        {
            if (CheckBoxList_CANCELADO_CUMPLIDO.Items[0].Selected == true)
            {
                HiddenField_CUMPLIDO.Value = "S";
            }

            if (CheckBoxList_CANCELADO_CUMPLIDO.Items[1].Selected == true)
            {
                HiddenField_CANCELADO.Value = "S";
            }
        }

        Buscar();
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

    private void cargar_informacion_control_registro(DataRow filaTablaInfoRequisicion)
    {
        TextBox_USU_CRE.Text = filaTablaInfoRequisicion["USU_CRE"].ToString();
        try
        {
            TextBox_FCH_CRE.Text = DateTime.Parse(filaTablaInfoRequisicion["FCH_CRE"].ToString()).ToShortDateString();
            TextBox_HOR_CRE.Text = DateTime.Parse(filaTablaInfoRequisicion["FCH_CRE"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_CRE.Text = "";
            TextBox_HOR_CRE.Text = "";
        }
        TextBox_USU_MOD.Text = filaTablaInfoRequisicion["USU_MOD"].ToString();
        try
        {
            TextBox_FCH_MOD.Text = DateTime.Parse(filaTablaInfoRequisicion["FCH_MOD"].ToString()).ToShortDateString();
            TextBox_HOR_MOD.Text = DateTime.Parse(filaTablaInfoRequisicion["FCH_MOD"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_MOD.Text = "";
            TextBox_HOR_MOD.Text = "";
        }
    }

    private void cargar_DropDownList_TIP_REQ()
    {
        DropDownList_TIP_REQ.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_TIPO_REQ);

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_TIP_REQ.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_TIP_REQ.Items.Add(item);
        }
        DropDownList_TIP_REQ.DataBind();
    }

    private void cargar_DropDownList_ID_EMPRESA()
    {
        DropDownList_ID_EMPRESA.Items.Clear();

        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaEmpresasActivas = _cliente.ObtenerTodasLasEmpresasActivas();

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_ID_EMPRESA.Items.Add(item);

        foreach (DataRow fila in tablaEmpresasActivas.Rows)
        {
            item = new ListItem(fila["RAZ_SOCIAL"].ToString(), fila["ID_EMPRESA"].ToString());
            DropDownList_ID_EMPRESA.Items.Add(item);
        }

        DropDownList_ID_EMPRESA.DataBind();
    }

    private void cargar_info_datos_empresa(DataRow filaInforeq)
    {
        cargar_DropDownList_TIP_REQ();
        DropDownList_TIP_REQ.SelectedValue = filaInforeq["TIPO_REQ"].ToString().Trim();
        cargar_DropDownList_ID_EMPRESA();

        try
        {
            DropDownList_ID_EMPRESA.SelectedValue = filaInforeq["ID_EMPRESA"].ToString().Trim();
        }
        catch
        {
            DropDownList_ID_EMPRESA.SelectedIndex = 0;
        }
    }

    private void cargar_fechas(DataRow filaInfoReq)
    {
        try
        {
            TextBox_FECHA_R.Text = Convert.ToDateTime(filaInfoReq["FECHA_R"]).ToShortDateString();
        }
        catch
        {
            TextBox_FECHA_R.Text = "";
        }

        try
        {
            TextBox_FECHA_REQUERIDA.Text = Convert.ToDateTime(filaInfoReq["FECHA_REQUERIDA"]).ToShortDateString();
        }
        catch
        {
            TextBox_FECHA_REQUERIDA.Text = "";
        }
    }

    private DataRow ObtenerEnvioDeCandidatoPorRegistro(Decimal REGISTRO)
    {
        DataRow resultado = null;

        envioCandidato _envioCandidato = new envioCandidato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoSolicitante = _envioCandidato.ObtenerEnvioDeCandidatoPorRegistro(REGISTRO);

        if (tablaInfoSolicitante.Rows.Count > 0)
        {
            resultado = tablaInfoSolicitante.Rows[0];
        }

        return resultado;
    }

    private void cargar_datos_envio(DataRow filaInfoReq)
    {
        if (DropDownList_ID_EMPRESA.SelectedValue == "")
        {
            DropDownList_ENVIO_CANDIDATOS.Items.Clear();
            TextBox_DIR_ENVIO.Text = "";
            TextBox_CIUDAD_ENVIO.Text = "";
            TextBox_TEL_ENVIO.Text = "";
            TextBox_EMAIL_ENVIO.Text = "";
            TextBox_COND_ENVIO.Text = "";
        }
        else
        {
            Decimal REGISTRO_ENVIO = 0;

            try
            {
                REGISTRO_ENVIO = Convert.ToDecimal(filaInfoReq["REGISTRO_ENVIO_CANDIDATOS"]);
            }
            catch
            {
                REGISTRO_ENVIO = 0;
            }

            envioCandidato _envioCandidato = new envioCandidato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaEnvio = _envioCandidato.ObtenerInformacionEnvioPorRegistro(REGISTRO_ENVIO);

            if (tablaEnvio.Rows.Count > 0)
            {
                DataRow filaEnvio = tablaEnvio.Rows[0];

                DropDownList_ENVIO_CANDIDATOS.Items.Add(new ListItem(filaEnvio["NOMBRE_CONTACTO"].ToString(), filaEnvio["REGISTRO"].ToString()));
                DropDownList_ENVIO_CANDIDATOS.SelectedValue = REGISTRO_ENVIO.ToString();

                TextBox_DIR_ENVIO.Text = filaEnvio["DIR_ENVIO"].ToString().Trim();
                TextBox_CIUDAD_ENVIO.Text = filaEnvio["NOMBRE_CIUDAD"].ToString().Trim();
                TextBox_TEL_ENVIO.Text = filaEnvio["TEL_ENVIO"].ToString().Trim();
                TextBox_EMAIL_ENVIO.Text = filaEnvio["MAIL_CONTACTO"].ToString().Trim();
                TextBox_COND_ENVIO.Text = filaEnvio["COND_ENVIO"].ToString().Trim();
            }
            else
            {
                DropDownList_ENVIO_CANDIDATOS.Items.Clear();
                TextBox_DIR_ENVIO.Text = "";
                TextBox_CIUDAD_ENVIO.Text = "";
                TextBox_TEL_ENVIO.Text = "";
                TextBox_EMAIL_ENVIO.Text = "";
                TextBox_COND_ENVIO.Text = "";
            }
        }
    }

    private void cargar_datos_servicio_respectivo(DataRow filaInfoReq)
    {
        if (DropDownList_ID_EMPRESA.SelectedValue == "")
        {
            Label_ID_SERVICIO_RESPECTIVO.Text = "DESCONOCIDO";
            TextBox_DESCRIPCION_SERVICIO.Text = "NO SE NCONTRÓ INFORMACIÓN DEL SERVICIO RESPECTIVO ASOCIADO A ESTA REQUISICIÓN.";
            TextBox_FECHA_VENCE_SERVICIO.Text = "";
        }
        else
        {
            Decimal ID_SERVICIO_RESPECTIVO = 0;
            try
            {
                ID_SERVICIO_RESPECTIVO = Convert.ToDecimal(filaInfoReq["ID_SERVICIO_RESPECTIVO"]);
            }
            catch
            {
                ID_SERVICIO_RESPECTIVO = 0;
            }

            if (ID_SERVICIO_RESPECTIVO == 0)
            {
                Label_ID_SERVICIO_RESPECTIVO.Text = "DESCONOCIDO";
                TextBox_DESCRIPCION_SERVICIO.Text = "NO SE NCONTRÓ INFORMACIÓN DEL SERVICIO RESPECTIVO ASOCIADO A ESTA REQUISICIÓN.";
                TextBox_FECHA_VENCE_SERVICIO.Text = "";
            }
            else
            {
                contratosServicio _contratosServicio = new contratosServicio(Session["idEmpresa"].ToString());
                DataTable tablaServicoRespectivo = _contratosServicio.ObtenerContratosDeServicioIdContrato(ID_SERVICIO_RESPECTIVO);

                if (tablaServicoRespectivo.Rows.Count <= 0)
                {
                    Label_ID_SERVICIO_RESPECTIVO.Text = "DESCONOCIDO";
                    TextBox_DESCRIPCION_SERVICIO.Text = "NO SE NCONTRÓ INFORMACIÓN DEL SERVICIO RESPECTIVO ASOCIADO A ESTA REQUISICIÓN.";
                    TextBox_FECHA_VENCE_SERVICIO.Text = "";
                }
                else
                {
                    DataRow infoServicio = tablaServicoRespectivo.Rows[0];

                    Label_ID_SERVICIO_RESPECTIVO.Text = infoServicio["REGISTRO"].ToString();
                    TextBox_DESCRIPCION_SERVICIO.Text = infoServicio["OBJ_CONTRATO"].ToString().Trim();
                    TextBox_FECHA_VENCE_SERVICIO.Text = Convert.ToDateTime(infoServicio["FCH_VENCE"]).ToShortDateString();
                }
            }
        }
    }

    private DataRow ObtenerPerfilPorRegistro(int REGISTRO)
    {
        DataRow resultado = null;

        perfil _perfil = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoPerfil = _perfil.ObtenerPorRegistro(REGISTRO);

        if (tablaInfoPerfil.Rows.Count > 0)
        {
            resultado = tablaInfoPerfil.Rows[0];
        }

        return resultado;
    }

    private void cargar_datos_perfil(DataRow filaInfoReq)
    {
        if (DropDownList_ID_EMPRESA.SelectedValue == "")
        {
            DropDownList_PERFILES.Items.Clear();
            TextBox_EDAD_MIN.Text = "";
            TextBox_EDAD_MAX.Text = "";
            TextBox_CANTIDAD.Text = "";
            TextBox_SEXO.Text = "";
            TextBox_EXPERIENCIA.Text = "";
            TextBox_NIVEL_ACADEMICO.Text = "";
            TextBox_FUNCIONES.Text = "";
        }
        else
        {
            Decimal REGISTRO_PERFIL = 0;
            try
            {
                REGISTRO_PERFIL = Convert.ToDecimal(filaInfoReq["REGISTRO_PERFIL"]);
            }
            catch
            {
                REGISTRO_PERFIL = 0;
            }

            perfil _perfil = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaPerfil = _perfil.ObtenerPorRegistro(REGISTRO_PERFIL);

            if (tablaPerfil.Rows.Count > 0)
            {
                DataRow filaPerfil = tablaPerfil.Rows[0];

                DropDownList_PERFILES.Items.Add(new ListItem(filaPerfil["NOM_OCUPACION"].ToString(), filaPerfil["REGISTRO"].ToString()));
                DropDownList_PERFILES.SelectedValue = REGISTRO_PERFIL.ToString();

                TextBox_EDAD_MIN.Text = filaPerfil["EDAD_MIN"].ToString().Trim();
                TextBox_EDAD_MAX.Text = filaPerfil["EDAD_MAX"].ToString().Trim();
                TextBox_CANTIDAD.Text = filaInfoReq["CANTIDAD"].ToString().Trim();
                TextBox_SEXO.Text = filaPerfil["SEXO"].ToString().Trim();
                TextBox_EXPERIENCIA.Text = filaPerfil["EXPERIENCIA_DESCRIPCION"].ToString().Trim();
                TextBox_NIVEL_ACADEMICO.Text = filaPerfil["DESCRIPCION"].ToString().Trim();
                TextBox_FUNCIONES.Text = filaPerfil["DSC_FUNCIONES"].ToString().Trim();
            }
            else
            {
                DropDownList_PERFILES.Items.Clear();
                TextBox_EDAD_MIN.Text = "";
                TextBox_EDAD_MAX.Text = "";
                TextBox_CANTIDAD.Text = "";
                TextBox_SEXO.Text = "";
                TextBox_EXPERIENCIA.Text = "";
                TextBox_NIVEL_ACADEMICO.Text = "";
                TextBox_FUNCIONES.Text = "";
            }
        }
    }

    private DataRow obtenerDatosCiudadCliente(String idCiudad)
    {
        DataRow resultado = null;

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());

        DataTable tablaEmpresa = _ciudad.ObtenerCiudadPorIdCiudad(idCiudad);

        if (tablaEmpresa.Rows.Count > 0)
        {
            resultado = tablaEmpresa.Rows[0];
        }

        return resultado;
    }

    private void cargar_DropDownList_REGIONAL()
    {
        DropDownList_REGIONAL.Items.Clear();

        regional _regional = new regional(Session["idEmpresa"].ToString());
        DataTable tablaRegionales = _regional.ObtenerTodasLasRegionales();

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_REGIONAL.Items.Add(item);

        foreach (DataRow fila in tablaRegionales.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_REGIONAL"].ToString());
            DropDownList_REGIONAL.Items.Add(item);
        }

        DropDownList_REGIONAL.DataBind();
    }

    private void cargar_DropDownList_DEPARTAMENTO(String id)
    {
        DropDownList_DEPARTAMENTO.Items.Clear();

        departamento _departamento = new departamento(Session["idEmpresa"].ToString());
        DataTable tablaDepartamentos = _departamento.ObtenerDepartamentosPorIdRegional(id);

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_DEPARTAMENTO.Items.Add(item);

        foreach (DataRow fila in tablaDepartamentos.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_DEPARTAMENTO"].ToString());
            DropDownList_DEPARTAMENTO.Items.Add(item);
        }

        DropDownList_DEPARTAMENTO.DataBind();
    }

    private void cargar_DropDownList_CIUDAD_REQ(String idRegional, String idDepartamento)
    {
        DropDownList_CIUDAD_REQ.Items.Clear();

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
        DataTable tablaCiudades = _ciudad.ObtenerCiudadesPorIdDepartamentoEIdRegional(idRegional, idDepartamento);

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_CIUDAD_REQ.Items.Add(item);

        foreach (DataRow fila in tablaCiudades.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_CIUDAD"].ToString());
            DropDownList_CIUDAD_REQ.Items.Add(item);
        }

        DropDownList_CIUDAD_REQ.DataBind();
    }

    private void cargar_contrato_ubicacion(DataRow filaInfoReq)
    {
        DataRow filaInfoCiudadEmpresa = obtenerDatosCiudadCliente(filaInfoReq["CIUDAD_REQ"].ToString().Trim());
        if (filaInfoCiudadEmpresa != null)
        {
            cargar_DropDownList_REGIONAL();
            DropDownList_REGIONAL.SelectedValue = filaInfoCiudadEmpresa["ID_REGIONAL"].ToString().Trim();
            DropDownList_REGIONAL.DataBind();
            cargar_DropDownList_DEPARTAMENTO(filaInfoCiudadEmpresa["ID_REGIONAL"].ToString().Trim());
            DropDownList_DEPARTAMENTO.SelectedValue = filaInfoCiudadEmpresa["ID_DEPARTAMENTO"].ToString().Trim();
            DropDownList_DEPARTAMENTO.DataBind();
            cargar_DropDownList_CIUDAD_REQ(filaInfoCiudadEmpresa["ID_REGIONAL"].ToString().Trim(), filaInfoCiudadEmpresa["ID_DEPARTAMENTO"].ToString().Trim());
            DropDownList_CIUDAD_REQ.SelectedValue = filaInfoCiudadEmpresa["ID_CIUDAD"].ToString().Trim();
            DropDownList_CIUDAD_REQ.DataBind();
        }
        else
        {
            DropDownList_REGIONAL.Items.Clear();
            DropDownList_DEPARTAMENTO.Items.Clear();
            DropDownList_CIUDAD_REQ.Items.Clear();
        }

        TextBox_DURACION.Text = filaInfoReq["NOMBRE_DURACION"].ToString().Trim();
        TextBox_HORARIO.Text = filaInfoReq["NOMBRE_HORARIO"].ToString().Trim();
        Int32 SALARIO = 0;
        try
        {
            SALARIO = Convert.ToInt32(filaInfoReq["SALARIO"]);
        }
        catch
        {
            SALARIO = 0;
        }
    }

    private void cargar_DropDownList_MOTIVO_CANCELACION()
    {
        DropDownList_MOTIVO_CANCELACION.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_MOTIVO_CANCELA_REQ);

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_MOTIVO_CANCELACION.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_MOTIVO_CANCELACION.Items.Add(item);
        }
        DropDownList_MOTIVO_CANCELACION.DataBind();
    }

    private void cargar_DropDownList_MOTIVO_CUMPLIR()
    {
        DropDownList_MOTIVO_CUMPLIR.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_MOTIVO_CUMPLIDO_REQ);

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_MOTIVO_CUMPLIR.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_MOTIVO_CUMPLIR.Items.Add(item);
        }
        DropDownList_MOTIVO_CUMPLIR.DataBind();
    }

    private void cargar_info_req_cancelada(DataRow filaInofReq)
    {
        if (filaInofReq["TIPO_CANCELA"].ToString().Trim().ToUpper() == "CLIENTE")
        {
            RadioButtonList_TIPO_CANCELACION.Items[0].Selected = true;
            RadioButtonList_TIPO_CANCELACION.Items[1].Selected = false;

            Panel_CANCELACION_POR_CLIENTE.Visible = true;

            cargar_DropDownList_MOTIVO_CANCELACION();

            try
            {
                DropDownList_MOTIVO_CANCELACION.SelectedValue = filaInofReq["MOTIVO_CANCELA"].ToString().Trim();
            }
            catch
            {
                DropDownList_MOTIVO_CANCELACION.SelectedIndex = -1;
            }
        }
        else
        {
            if (filaInofReq["TIPO_CANCELA"].ToString().Trim().ToUpper() == "INTERNA")
            {
                RadioButtonList_TIPO_CANCELACION.Items[0].Selected = false;
                RadioButtonList_TIPO_CANCELACION.Items[1].Selected = true;

                Panel_CANCELACION_POR_CLIENTE.Visible = false;

                DropDownList_MOTIVO_CANCELACION.Items.Clear();
            }
            else
            {
                RadioButtonList_TIPO_CANCELACION.Items[0].Selected = false;
                RadioButtonList_TIPO_CANCELACION.Items[1].Selected = false;

                Panel_CANCELACION_POR_CLIENTE.Visible = false;

                DropDownList_MOTIVO_CANCELACION.Items.Clear();

                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La Requisición se encuentra CANCELADA. No se pudo determinar correctamente los motivos de la cancelación.", Proceso.Advertencia);
            }
        }
    }

    private void cargar_info_req_cumplida(DataRow filaInfoReq)
    {
        cargar_DropDownList_MOTIVO_CUMPLIR();

        try
        {
            DropDownList_MOTIVO_CUMPLIR.SelectedValue = filaInfoReq["MOTIVO_CUMPLIDO"].ToString().Trim();
        }
        catch
        {
            DropDownList_MOTIVO_CUMPLIR.SelectedIndex = 0;
        }

        if (DropDownList_MOTIVO_CUMPLIR.SelectedIndex <= 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La requisición seleccionada ya se encuentra CUMPLIDA. No se pudo determinar el motivo.", Proceso.Advertencia);
        }
    }

    private void Cargar(Decimal ID_REQUERIMIENTO)
    {
        HiddenField_ID_REQUERIMIENTO.Value = ID_REQUERIMIENTO.ToString();

        requisicion _requisicion = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable TablaInfoRequisicion = _requisicion.ObtenerComRequerimientoPorIdRequerimiento(ID_REQUERIMIENTO);

        if (TablaInfoRequisicion.Rows.Count <= 0)
        {
            if (_requisicion.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _requisicion.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _requisicion.MensajeError, Proceso.Advertencia);
            }
        }
        else
        {
            Ocultar(Acciones.Inicio);

            DataRow filainfoReq = TablaInfoRequisicion.Rows[0];

            if (filainfoReq["CANCELADO"].ToString().Trim() == "S")
            {
                Mostrar(Acciones.CargarReqCancelada);
                Desactivar(Acciones.Inicio);
            }
            else
            {
                if (filainfoReq["CUMPLIDO"].ToString().Trim() == "S")
                {
                    Mostrar(Acciones.CargarReqCumplida);
                    Desactivar(Acciones.Inicio);
                }
                else
                {
                    Mostrar(Acciones.CargarReqNormal);
                    Desactivar(Acciones.Inicio);
                }
            }

            cargar_informacion_control_registro(filainfoReq);

            cargar_info_datos_empresa(filainfoReq);

            cargar_fechas(filainfoReq);

            cargar_datos_envio(filainfoReq);

            cargar_datos_servicio_respectivo(filainfoReq);

            cargar_datos_perfil(filainfoReq);

            cargar_contrato_ubicacion(filainfoReq);

            TextBox_OBS_REQUERIMIENTO.Text = filainfoReq["OBS_REQUERIMIENTO"].ToString();

            if (filainfoReq["CANCELADO"].ToString().Trim() == "S")
            {
                cargar_info_req_cancelada(filainfoReq);
                cargarDatosEfectiviadOportunidadSegunReq(ID_REQUERIMIENTO);
            }
            else
            {
                if (filainfoReq["CUMPLIDO"].ToString().Trim() == "S")
                {
                    cargarDatosEfectiviadOportunidadSegunReq(ID_REQUERIMIENTO);
                    cargar_info_req_cumplida(filainfoReq);
                }
                else
                {
                    Mostrar(Acciones.CargarReqNormal);
                }
            }
        }
    }

    protected void GridView_RESULTADOS_BUSQUEDA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal ID_REQUERIMIENTO = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_REQUERIMIENTO"]);

            Cargar(ID_REQUERIMIENTO);
        }
    }

    private void Activar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Cancelar:
                RadioButtonList_TIPO_CANCELACION.Enabled = true;
                DropDownList_MOTIVO_CANCELACION.Enabled = true;

                break;
        }
    }

    private void cargarDatosEfectiviadOportunidadSegunReq(Decimal ID_REQUERIMIENTO)
    {
        requisicion _req = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaReq = _req.ObtenerComRequerimientoPorIdRequerimiento(ID_REQUERIMIENTO);

        if (tablaReq.Rows.Count <= 0)
        {
            if (_req.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _req.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información de la requisición seleccionada. no se pudo determinar el estado actual de OPORTUNIDAD Y EFECTIVIDAD.", Proceso.Advertencia);
            }

            CheckBox_ReqEfectiva.Checked = false;
            CheckBox_ReqOportuna.Checked = false;
        }
        else
        {
            DataRow filaReq = tablaReq.Rows[0];

            if (filaReq["CUMPLE_OPORTUNO"].ToString().Trim().ToUpper() == "S")
            {
                CheckBox_ReqOportuna.Checked = true;
            }
            else
            {
                CheckBox_ReqOportuna.Checked = false;
            }

            if (filaReq["CUMPLE_EFECTIVA"].ToString().Trim().ToUpper() == "S")
            {
                CheckBox_ReqEfectiva.Checked = true;
            }
            else
            {
                CheckBox_ReqEfectiva.Checked = false;
            }
        }
    }

    protected void Button_CANCELAR_REQUISICION_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Cumplir);

        Panel_EfectividadOportunidad.Visible = true;
        CheckBox_ReqEfectiva.Checked = false;
        CheckBox_ReqOportuna.Checked = false;
        CheckBox_ReqEfectiva.Enabled = true;
        CheckBox_ReqOportuna.Enabled = true;

        cargarDatosEfectiviadOportunidadSegunReq(Convert.ToDecimal(HiddenField_ID_REQUERIMIENTO.Value));

        Panel_CANCELAR_REQUISICION.Visible = true;

        RadioButtonList_TIPO_CANCELACION.SelectedIndex = -1;
        RadioButtonList_TIPO_CANCELACION.Enabled = true;
        Panel_CANCELACION_POR_CLIENTE.Visible = false;

        Button_GUARDAR_CANCELACION.Visible = true;

        Button_GUARDAR_CANCELACION.Focus();
    }

    protected void RadioButtonList_TIPO_CANCELACION_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList_TIPO_CANCELACION.SelectedValue.ToUpper() == "CLIENTE")
        {
            Panel_CANCELACION_POR_CLIENTE.Visible = true;
            cargar_DropDownList_MOTIVO_CANCELACION();
            DropDownList_MOTIVO_CANCELACION.Enabled = true;
        }
        else
        {
            if (RadioButtonList_TIPO_CANCELACION.SelectedValue.ToUpper() == "INTERNA")
            {
                Panel_CANCELACION_POR_CLIENTE.Visible = false;
            }
        }
    }
    protected void Button_CUMPLIR_REQUISICION_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Cancelar);

        Panel_CUMPLIR_REQUISICION.Visible = Visible;

        Panel_EfectividadOportunidad.Visible = true;
        CheckBox_ReqEfectiva.Checked = false;
        CheckBox_ReqOportuna.Checked = false;
        CheckBox_ReqEfectiva.Enabled = true;
        CheckBox_ReqOportuna.Enabled = true;

        cargarDatosEfectiviadOportunidadSegunReq(Convert.ToDecimal(HiddenField_ID_REQUERIMIENTO.Value));

        cargar_DropDownList_MOTIVO_CUMPLIR();
        DropDownList_MOTIVO_CUMPLIR.Enabled = true;

        Button_GUARDAR_CUMPLIR.Visible = true;

        Button_GUARDAR_CUMPLIR.Focus();
    }

    private void GuardarCancelacion()
    {
        Decimal ID_REQUISICION = Convert.ToDecimal(HiddenField_ID_REQUERIMIENTO.Value);

        String CANCELADO = "S";
        String TIPO_CANCELA = RadioButtonList_TIPO_CANCELACION.SelectedValue;
        String MOTIVO_CANCELA = null;
        if (TIPO_CANCELA == "CLIENTE")
        {
            MOTIVO_CANCELA = DropDownList_MOTIVO_CANCELACION.SelectedValue;
        }
        String CUMPLIDO = "N";

        String CUMPLE_OPORTUNO = "N";
        if (CheckBox_ReqOportuna.Checked == true)
        {
            CUMPLE_OPORTUNO = "S";
        }

        String CUMPLE_EECTIVA = "N";
        if (CheckBox_ReqEfectiva.Checked == true)
        {
            CUMPLE_EECTIVA = "S";
        }

        String MOTIVO_CUMPLIDO = null;

        requisicion _requisicion = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean resultado = _requisicion.ActualizarConRequerimeintosBanderas(ID_REQUISICION, CUMPLIDO, CUMPLE_OPORTUNO, CUMPLE_EECTIVA, MOTIVO_CUMPLIDO, CANCELADO, TIPO_CANCELA, MOTIVO_CANCELA);

        if (resultado == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _requisicion.MensajeError, Proceso.Error);
        }
        else
        {
            Cargar(ID_REQUISICION);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La requisición #: " + ID_REQUISICION.ToString() + " fue cancelada correctamente.", Proceso.Correcto);
        }
    }

    protected void Button_GUARDAR_CANCELACION_Click(object sender, EventArgs e)
    {
        GuardarCancelacion();
    }

    private void GuardarCumplir()
    {
        Decimal ID_REQUISICION = Convert.ToDecimal(HiddenField_ID_REQUERIMIENTO.Value);

        String CANCELADO = "N";
        String TIPO_CANCELA = null;
        String MOTIVO_CANCELA = null;
        String CUMPLIDO = "S";

        String CUMPLE_OPORTUNO = "N";
        if (CheckBox_ReqOportuna.Checked == true)
        {
            CUMPLE_OPORTUNO = "S";
        }

        String CUMPLE_EECTIVA = "N";
        if (CheckBox_ReqEfectiva.Checked == true)
        {
            CUMPLE_EECTIVA = "S";
        }

        String MOTIVO_CUMPLIDO = DropDownList_MOTIVO_CUMPLIR.SelectedValue;

        requisicion _requisicion = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        parametro _p = new parametro(Session["idEmpresa"].ToString());
        Int32 contratadosPorReq = 0;
        Boolean correcto = true;
        Boolean ajustarCantidad = false;

        DataTable tablaMotivoCumplido = _p.ObtenerDescripcionParametroPorCodigo(tabla.PARAMETROS_MOTIVO_CUMPLIDO_REQ, MOTIVO_CUMPLIDO);
        if (tablaMotivoCumplido.Rows.Count > 0)
        {
            DataRow filaMotivoCumplido = tablaMotivoCumplido.Rows[0];
            if (filaMotivoCumplido["VARIABLE"].ToString().Trim().ToUpper() == "AJUSTAR_CANTIDAD")
            {
                ajustarCantidad = true;
            }
        }

        if (ajustarCantidad == true)
        {
            DataTable tablaConteoContratatos = _requisicion.ObtenerTrabajadoresContratadosPorReq(ID_REQUISICION);

            if (tablaConteoContratatos.Rows.Count > 0)
            {
                DataRow filaConteoContratados = tablaConteoContratatos.Rows[0];
                contratadosPorReq = Convert.ToInt32(filaConteoContratados["CONTRATADOS"].ToString());
            }
        }

        if (ajustarCantidad == true)
        {
            if (contratadosPorReq == 0)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El requerimiento no puede ser cumplido utilizando el motivo: " + MOTIVO_CUMPLIDO + ", porque no se contrataron personas.", Proceso.Advertencia);
                correcto = false;
            }
        }

        if (correcto == true)
        {
            Boolean resultado = _requisicion.ActualizarConRequerimeintosBanderas(ID_REQUISICION, CUMPLIDO, CUMPLE_OPORTUNO, CUMPLE_EECTIVA, MOTIVO_CUMPLIDO, CANCELADO, TIPO_CANCELA, MOTIVO_CANCELA);

            if (resultado == false)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _requisicion.MensajeError, Proceso.Error);
            }
            else
            {
                Cargar(ID_REQUISICION);

                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La requisición #: " + ID_REQUISICION.ToString() + " fue cumplida correctamente.", Proceso.Correcto);
            }
        }
    }

    protected void Button_GUARDAR_CUMPLIR_Click(object sender, EventArgs e)
    {
        GuardarCumplir();
    }
}