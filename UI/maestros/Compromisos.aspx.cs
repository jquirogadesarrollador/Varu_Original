using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brainsbits.LLB.programasRseGlobal;
using System.Data;
using Brainsbits.LLB.maestras;
using TSHAK.Components;
using Brainsbits.LLB;
using System.IO;
using Brainsbits.LLB.comercial;

public partial class _Compromisos : System.Web.UI.Page
{
    private System.Drawing.Color colorRojo = System.Drawing.ColorTranslator.FromHtml("#F8523A");
    private System.Drawing.Color colorAmarilla = System.Drawing.ColorTranslator.FromHtml("#FFF200");
    private System.Drawing.Color colorVerde = System.Drawing.ColorTranslator.FromHtml("#50CE04");

    private enum Acciones
    {
        Inicio = 0,
        CargarCompromiso,
        Actualizar,
        FinalizarCompromiso
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }

    private enum Listas
    {
        SubProgramas = 0,
        Actividades,
        EstadosActividades,
        TiposActividad,
        SectoresActividad,
        Regionales,
        Encargados
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

                Panel_FORM_BOTONES_ENCABEZADO.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_Finalizar.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_Volver.Visible = false;
                Button_CrearActividad.Visible = false;

                Panel_RESULTADOS_GRID.Visible = false;

                Panel_CompromioSeleccionado.Visible = false;
                Panel_SeguimientoCompromiso.Visible = false;
                Panel_NuevoAdjunto.Visible = false;
                Button_NuevoAdjunto.Visible = false;
                Button_GuardarAdjunto.Visible = false;
                Button_CancelarAdjunto.Visible = false;
                Button_FinalizarSeguimiento.Visible = false;

                Panel_FinalizarCompromiso.Visible = false;

                Panel_BOTONES_PIE.Visible = false;
                Button_Guardar_1.Visible = false;
                Button_Actualizar_1.Visible = false;
                Button_Finalizar_1.Visible = false;
                Button_Cancelar_1.Visible = false;
                Button_vOLVER_1.Visible = false;
                Button_CrearActividad_1.Visible = false;

                Panel_InfoActividad.Visible = false;


                break;
            case Acciones.Actualizar:
                Button_MODIFICAR.Visible = false;
                Button_Finalizar.Visible = false;
                Button_Volver.Visible = false;
                Button_GUARDAR.Visible = false;

                Panel_NuevoAdjunto.Visible = false;
                Button_GuardarAdjunto.Visible = false;
                Button_CancelarAdjunto.Visible = false;
                Button_FinalizarSeguimiento.Visible = false;

                Button_Actualizar_1.Visible = false;
                Button_Finalizar_1.Visible = false;
                Button_vOLVER_1.Visible = false;
                Button_Guardar_1.Visible = false;
                break;
            case Acciones.FinalizarCompromiso:
                Button_MODIFICAR.Visible = false;
                Button_Finalizar.Visible = false;
                Button_Volver.Visible = false;

                Button_Actualizar_1.Visible = false;
                Button_Finalizar_1.Visible = false;
                Button_vOLVER_1.Visible = false;

                break;
        }
    }


    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.CargarCompromiso:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Button_MODIFICAR.Visible = true;
                Button_Finalizar.Visible = true;
                Button_Volver.Visible = true;
                Button_CrearActividad.Visible = true;
                Button_CrearActividad_1.Visible = false;

                Panel_CompromioSeleccionado.Visible = true;
                Panel_SeguimientoCompromiso.Visible = true;

                Panel_BOTONES_PIE.Visible = true;
                Button_Actualizar_1.Visible = true;
                Button_Finalizar_1.Visible = true;
                Button_vOLVER_1.Visible = true;
                break;
            case Acciones.Actualizar:
                Button_CANCELAR.Visible = true;

                Panel_SeguimientoCompromiso.Visible = true;

                Button_NuevoAdjunto.Visible = true;
                Button_FinalizarSeguimiento.Visible = true;

                Button_Cancelar_1.Visible = true;
                break;
            case Acciones.FinalizarCompromiso:
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_FinalizarCompromiso.Visible = true;

                Button_Guardar_1.Visible = true;
                Button_Cancelar_1.Visible = true;
                break;
        }
    }

    private void CargarGrillaCompromisosPendientes()
    {
        Programa.Areas AREA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);

        Programa _prog = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCompromisosPendientes = _prog.ObtenerCompromisosPendientes(AREA);


        GridView_Compromisos.DataSource = tablaCompromisosPendientes;
        GridView_Compromisos.DataBind();
    }

    private void ConfigurarAreaRseGlobal()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        int proceso = Convert.ToInt32(QueryStringSeguro["proceso"]);

        HiddenField_ID_AREA.Value = _tools.ObtenerIdAreaProceso(proceso);
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:

                ConfigurarAreaRseGlobal();

                CargarGrillaCompromisosPendientes();

                break;
        }
    }

    private void Desactivar(Acciones accion)
    {
        switch (accion)
        { 
            case Acciones.Inicio:
                DropDownList_Tipo.Enabled = false;
                DropDownList_Sector.Enabled = false;
                DropDownList_EstadoActividad.Enabled = false;
                TextBox_DescripcionActividad.Enabled = false;
                break;
        }
    }

    private void Iniciar()
    {
        Configurar();
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Title = "ADMINISTRACIÓN DE COMPROMISOS";

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

    private void Guardar()
    {
        tools _tools = new tools();

        Decimal ID_MAESTRA_COMPROMISO = Convert.ToDecimal(HiddenField_ID_MAESTRA_COMPROMISO.Value);

        DateTime FECHA_EJECUCION = Convert.ToDateTime(TextBox_FechaFinalizacion.Text);
        String HORA_EJECUCION = TimePicker_HoraFinalizacion.Text;
        String DESCRIPCION = TextBox_DescripcionFinalizacion.Text.ToString();

        Programa _prog = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean correcto = _prog.FinalizarCompromiso(ID_MAESTRA_COMPROMISO, MaestraCompromiso.EstadosCompromiso.CERRADO.ToString(), FECHA_EJECUCION, HORA_EJECUCION, DESCRIPCION);

        if (correcto == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _prog.MensajeError, Proceso.Error);
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El copromiso fue terminado correctamente.", Proceso.Correcto);
        }
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        Guardar();
    }
    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Actualizar);
        Mostrar(Acciones.Actualizar);

    }
    protected void Button_Finalizar_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.FinalizarCompromiso);
        Mostrar(Acciones.FinalizarCompromiso);

        TextBox_FechaFinalizacion.Text = DateTime.Now.ToShortDateString();
        TextBox_DescripcionFinalizacion.Visible = true;
    }

    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(HiddenField_ID_MAESTRA_COMPROMISO.Value) == false)
        {
            Decimal ID_MAESTRA_COMPROMISO = Convert.ToDecimal(HiddenField_ID_MAESTRA_COMPROMISO.Value);

            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.CargarCompromiso);

            Cargar(ID_MAESTRA_COMPROMISO);
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);
        }
    }

    private void CargarInfoBasicaCompromiso(Decimal ID_MAESTRA_COMPROMISO)
    {
        Programa _prog = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCompromiso = _prog.ObtenerInformacionCompromiso(ID_MAESTRA_COMPROMISO);

        if (tablaCompromiso.Rows.Count <= 0)
        {
            if (_prog.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _prog.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información del Compromiso seleccionado", Proceso.Advertencia);
            }
        }
        else
        {
            DataRow filaCompromiso = tablaCompromiso.Rows[0];

            Label_TipoGenera.Text = filaCompromiso["TIPO_GENERA"].ToString().Trim();
            Label_DescripcionOrigen.Text = filaCompromiso["NOMBRE_ACTIVIDAD_GENERA"].ToString().Trim();
            Label_FechaP.Text = Convert.ToDateTime(filaCompromiso["FECHA_P"]).ToShortDateString();
            Label_Compromiso.Text = filaCompromiso["COMPROMISO"].ToString().Trim();
            Label_Responsable.Text = filaCompromiso["NOMBRE_USU_LOG_RESPONSABLE"].ToString();
            Label_Observaciones.Text = filaCompromiso["OBSERVACIONES"].ToString().Trim();
            Label_Estado.Text = filaCompromiso["ESTADO"].ToString().Trim();
        }
    }

    private void Cargar_GridView_AdjuntosInforme(DataTable tablaSeguimientos)
    {
        GridView_AdjuntosInforme.DataSource = tablaSeguimientos;
        GridView_AdjuntosInforme.DataBind();

        for (int i = 0; i < GridView_AdjuntosInforme.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_AdjuntosInforme.Rows[i];
            DataRow filaTabla = tablaSeguimientos.Rows[i];

            HyperLink link = filaGrilla.FindControl("HyperLink_ARCHIVO_ADJUNTO") as HyperLink;

            if (DBNull.Value.Equals(filaTabla["ARCHIVO"]) == true)
            {
                link.Enabled = false;
            }
            else
            {
                tools _tools = new tools();
                SecureQueryString QueryStringSeguro;
                QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

                QueryStringSeguro["id_seguimiento"] = filaTabla["ID_SEGUIMIENTO"].ToString().Trim();

                link.Enabled = true;
                link.NavigateUrl = "~/maestros/visorArchivSeguimientoCompromiso.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            }
        }
    }

    private void CargarSeguimientoCompromiso(Decimal ID_MAESTRA_COMPROMISO)
    {
        Programa _prog = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaSeguiminetos = _prog.ObtenerSeguimientosCompromiso(ID_MAESTRA_COMPROMISO);

        if (tablaSeguiminetos.Rows.Count <= 0)
        {
            if (_prog.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _prog.MensajeError, Proceso.Error);
            }

            Panel_SeguimientoCompromiso.Visible = false;
        }
        else
        {
            Cargar_GridView_AdjuntosInforme(tablaSeguiminetos);
        }
    }

    private void Cargar(Decimal ID_MAESTRA_COMPROMISO)
    {
        HiddenField_ID_MAESTRA_COMPROMISO.Value = ID_MAESTRA_COMPROMISO.ToString();

        CargarInfoBasicaCompromiso(ID_MAESTRA_COMPROMISO);

        CargarSeguimientoCompromiso(ID_MAESTRA_COMPROMISO);
    }

    protected void GridView_Compromisos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal ID_MAESTRA_COMPROMISO = Convert.ToDecimal(GridView_Compromisos.DataKeys[indexSeleccionado].Values["ID_MAESTRA_COMPROMISO"]);

            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.CargarCompromiso);

            Cargar(ID_MAESTRA_COMPROMISO);
        }
    }

    protected void GridView_Compromisos_PreRender(object sender, EventArgs e)
    {
        for (int i = 0; i < GridView_Compromisos.Rows.Count; i++)
        {
            String ALERTA = GridView_Compromisos.DataKeys[i].Values["ALERTA"].ToString();

            if (ALERTA == "BAJA")
            {
                GridView_Compromisos.Rows[i].BackColor = colorVerde;
            }
            else
            {
                if (ALERTA == "MEDIA")
                {
                    GridView_Compromisos.Rows[i].BackColor = colorAmarilla;
                }
                else
                {
                    GridView_Compromisos.Rows[i].BackColor = colorRojo;
                }
            }
        }
    }

    protected void Button_Volver_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }

    protected void Button_NuevoAdjunto_Click(object sender, EventArgs e)
    {
        Panel_NuevoAdjunto.Visible = true;

        Label_FechaNuevoSeguimiento.Text = DateTime.Now.ToShortDateString();

        TextBox_SeguimientoNuevo.Text = "";
        TextBox_DescripcionAdjunto.Text = "";

        Button_NuevoAdjunto.Visible = false;
        Button_GuardarAdjunto.Visible = true;
        Button_CancelarAdjunto.Visible = true;
    }
    protected void Button_GuardarAdjunto_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();

        Decimal ID_MAESTRA_COMPROMISO = Convert.ToDecimal(HiddenField_ID_MAESTRA_COMPROMISO.Value);

        Byte[] ARCHIVO = null;
        Int32 ARCHIVO_TAMANO = 0;
        String ARCHIVO_EXTENSION = null;
        String ARCHIVO_TYPE = null;
        if (FileUpload_Adjunto.HasFile == true)
        {
            using (BinaryReader reader = new BinaryReader(FileUpload_Adjunto.PostedFile.InputStream))
            {
                ARCHIVO = reader.ReadBytes(FileUpload_Adjunto.PostedFile.ContentLength);
                ARCHIVO_TAMANO = FileUpload_Adjunto.PostedFile.ContentLength;
                ARCHIVO_TYPE = FileUpload_Adjunto.PostedFile.ContentType;
                ARCHIVO_EXTENSION = _tools.obtenerExtensionArchivo(FileUpload_Adjunto.PostedFile.FileName);
            }
        }

        DateTime FECHA_SEGUIMIENTO = Convert.ToDateTime(Label_FechaNuevoSeguimiento.Text);
        String SEGUIMIENTO = TextBox_SeguimientoNuevo.Text.Trim();
        String DESCRIPCION = TextBox_DescripcionAdjunto.Text.Trim();

        Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Decimal ID_SEGUIMIENTO = _programa.AdicionarSeguimientoCompromiso(ID_MAESTRA_COMPROMISO, SEGUIMIENTO, DESCRIPCION, ARCHIVO, ARCHIVO_TAMANO, ARCHIVO_EXTENSION, ARCHIVO_TYPE, FECHA_SEGUIMIENTO);

        if (ID_SEGUIMIENTO <= 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _programa.MensajeError, Proceso.Error);
        }
        else
        {
            CargarSeguimientoCompromiso(ID_MAESTRA_COMPROMISO);

            Panel_NuevoAdjunto.Visible = false;
            Button_NuevoAdjunto.Visible = true;
            Button_GuardarAdjunto.Visible = false;
            Button_CancelarAdjunto.Visible = false;

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El seguimiento fue actualizado correctamente.", Proceso.Correcto);
        }
    }

    protected void Button_CancelarAdjunto_Click(object sender, EventArgs e)
    {
        Panel_NuevoAdjunto.Visible = false;
        Button_NuevoAdjunto.Visible = true;
        Button_GuardarAdjunto.Visible = false;
        Button_CancelarAdjunto.Visible = false;
    }

    protected void Button_FinalizarSeguimiento_Click(object sender, EventArgs e)
    {
        Decimal ID_MAESTRA_COMPROMISO = Convert.ToDecimal(HiddenField_ID_MAESTRA_COMPROMISO.Value);

        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.CargarCompromiso);

        Cargar(ID_MAESTRA_COMPROMISO);
    }

    private void Cargar(Listas lista, DropDownList drop)
    {
        Decimal ID_MAESTRA_COMPROMISO = Convert.ToDecimal(HiddenField_ID_MAESTRA_COMPROMISO.Value);

        Programa _prog = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaResultado = _prog.ObtenerIdProgramaGeneralDesdeIdMaestraCompromiso(ID_MAESTRA_COMPROMISO);

        Decimal ID_PROGRAMA_GENERAL = 0;
        Decimal ID_EMPRESA = 0;
        if(tablaResultado.Rows.Count > 0)
        {
            DataRow filaResultado = tablaResultado.Rows[0];

            ID_PROGRAMA_GENERAL = Convert.ToDecimal(filaResultado["ID_PROGRAMA_GENERAL"]);
            ID_EMPRESA = Convert.ToDecimal(filaResultado["ID_EMPRESA"]);
        }
        
        Programa.Areas AREA_PROGRAMA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);

        switch (lista)
        {
            case Listas.SubProgramas:
                DataTable tablaProgramas = _prog.ObtenerSubProgramasDeUnProgramaGeneral(ID_PROGRAMA_GENERAL);

                drop.Items.Clear();
                drop.Items.Add(new ListItem("Seleccione...", ""));

                foreach (DataRow fila in tablaProgramas.Rows)
                {
                    Decimal ID_DETALLE_GENERAL = Convert.ToDecimal(fila["ID_DETALLE_GENERAL"]);
                    Decimal ID_SUB_PROGRAMA = Convert.ToDecimal(fila["ID_SUBPROGRAMA"]);

                    DataTable tablaActividadesPrograma = _prog.ObtenerActividadesPorDetalleGeneralPadre(ID_DETALLE_GENERAL);

                    if (tablaActividadesPrograma.Rows.Count > 0)
                    {
                        drop.Items.Add(new ListItem(fila["NOMBRE_SUB_PROGRAMA"].ToString().Trim(), ID_DETALLE_GENERAL.ToString() + ":" + ID_SUB_PROGRAMA.ToString()));
                    }
                }

                drop.DataBind();
                break;
            case Listas.EstadosActividades:
                drop.Items.Clear();
                parametro _parametroAC = new parametro(Session["idEmpresa"].ToString());
                DataTable tablaParametrosAC = _parametroAC.ObtenerParametrosPorTabla(tabla.PARAMETROS_ESTADO_ACTIVIDAD_RSE_GLOBAL);
                ListItem itemAC = new ListItem("Seleccione...", "");
                drop.Items.Add(itemAC);
                foreach (DataRow fila in tablaParametrosAC.Rows)
                {
                    itemAC = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
                    drop.Items.Add(itemAC);
                }
                drop.DataBind();
                break;
            case Listas.TiposActividad:

                drop.Items.Clear();

                TipoActividad _tipoActividad = new TipoActividad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                DataTable tablaParametrosTA = _tipoActividad.ObtenerTiposActividadPorAreayEstado(AREA_PROGRAMA, true);
                drop.Items.Add(new ListItem("Seleccione...", ""));

                foreach (DataRow fila in tablaParametrosTA.Rows)
                {
                    drop.Items.Add(new ListItem(fila["NOMBRE"].ToString(), fila["NOMBRE"].ToString()));
                }
                drop.DataBind();
                break;
            case Listas.SectoresActividad:
                drop.Items.Clear();
                parametro _parametroSA = new parametro(Session["idEmpresa"].ToString());
                DataTable tablaParametrosSA = _parametroSA.ObtenerParametrosPorTabla(tabla.PARAMETROS_SECTORES_ACTIVIDAD);
                drop.Items.Add(new ListItem("Seleccione...", ""));
                foreach (DataRow fila in tablaParametrosSA.Rows)
                {
                    drop.Items.Add(new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString()));
                }
                drop.DataBind();
                break;
            case Listas.Regionales:
                drop.Items.Clear();
                regional _regional = new regional(Session["idEmpresa"].ToString());
                DataTable tablaRegionales = _regional.ObtenerTodasLasRegionales();

                ListItem item = new ListItem("Seleccione...", "");
                drop.Items.Add(item);

                foreach (DataRow fila in tablaRegionales.Rows)
                {
                    item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_REGIONAL"].ToString());
                    drop.Items.Add(item);
                }

                drop.DataBind();
                break;
            case Listas.Encargados:

                DataTable tablaEncargados = _prog.ObtenerUsuariosSistemaActivos();

                drop.Items.Add(new ListItem("Seleccione...", ""));

                foreach (DataRow fila in tablaEncargados.Rows)
                {
                    item = new ListItem(fila["NOMBRE_USUARIO"].ToString(), fila["USU_LOG"].ToString());
                    drop.Items.Add(item);
                }

                drop.DataBind();
                break;
        }
    }

    private void CargarValidadorPresupuesto(decimal ID_PRESUPUESTO)
    {
        presupuesto _presupuesto = new presupuesto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaPresupuesto = _presupuesto.ObtenerPresupuestoPorId(ID_PRESUPUESTO);

        DataRow fila = tablaPresupuesto.Rows[0];

        RangeValidator_TextBox_PresupuestoAsignado.MinimumValue = "0";
        RangeValidator_TextBox_PresupuestoAsignado.MaximumValue = Convert.ToDecimal(fila["PRESUPUESTO"]).ToString();
    }

    private void CargarValidadorPersonalCitado(Decimal ID_EMPRESA)
    {
        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaDatos = _cliente.ObtenerNumEmpleadosActivosPorIdEmpresa(ID_EMPRESA, "S", "S");

        if (tablaDatos.Rows.Count <= 0)
        {
            if (_cliente.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se pudo determinar el personal activo actual de la empresa, no se realizará el control de personal citado.", Proceso.Advertencia);
            }

            Label_PersonalCitadoMaximo.Visible = false;
            RangeValidator_TextBox_PersonalCitado.Enabled = false;
            ValidatorCalloutExtender_TextBox_PersonalCitado_1.Enabled = false;

        }
        else
        {
            Int32 contadorPersonalActivo = Convert.ToInt32(tablaDatos.Rows[0]["NUM_EMPLEADOS"]);

            if (contadorPersonalActivo <= 0)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La empresa no tiene personal activo, no se realizará el control de personal citado.", Proceso.Advertencia);

                Label_PersonalCitadoMaximo.Visible = false;
                RangeValidator_TextBox_PersonalCitado.Enabled = false;
                ValidatorCalloutExtender_TextBox_PersonalCitado_1.Enabled = false;
            }
            else
            {
                Label_PersonalCitadoMaximo.Visible = true;
                Label_PersonalCitadoMaximo.Text = contadorPersonalActivo.ToString();
                RangeValidator_TextBox_PersonalCitado.Enabled = true;
                ValidatorCalloutExtender_TextBox_PersonalCitado_1.Enabled = true;

                RangeValidator_TextBox_PersonalCitado.MinimumValue = "1";
                RangeValidator_TextBox_PersonalCitado.MaximumValue = contadorPersonalActivo.ToString();
            }
        }
    }

    protected void Button_CrearActividad_Click(object sender, EventArgs e)
    {
        Decimal ID_MAESTRA_COMPROMISO = Convert.ToDecimal(HiddenField_ID_MAESTRA_COMPROMISO.Value);

        Panel_InfoActividad.Visible = true;

        DropDownList_SubPrograma.Items.Clear();

        DropDownList_IdActividad.Items.Clear();
        DropDownList_Tipo.Items.Clear();
        DropDownList_Sector.Items.Clear();
        DropDownList_EstadoActividad.Items.Clear();
        TextBox_DescripcionActividad.Text = "";

        TextBox_Resumen.Text = "";
        TextBox_FechaActividad.Text = "";

        TimePicker_HoraInicioActividad.SelectedTime = new DateTime();
        TimePicker_HoraFinActividad.SelectedTime = new DateTime();

        TextBox_PresupuestoAsignado.Text = "";
        TextBox_PersonalCitado.Text = "";

        DropDownList_Encargado.Items.Clear();

        DropDownList_REGIONAL.Items.Clear();
        DropDownList_DEPARTAMENTO.Items.Clear();
        DropDownList_CIUDAD.Items.Clear();

        Cargar(Listas.SubProgramas, DropDownList_SubPrograma);
        Cargar(Listas.Regionales, DropDownList_REGIONAL);
        Cargar(Listas.Encargados, DropDownList_Encargado);

        Programa _prog = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaResultado = _prog.ObtenerIdProgramaGeneralDesdeIdMaestraCompromiso(ID_MAESTRA_COMPROMISO);

        Decimal ID_PRESUPUESTO = 0;
        decimal ID_EMPRESA = 0;
        if (tablaResultado.Rows.Count > 0)
        {
            DataRow filaresultado = tablaResultado.Rows[0];

            ID_PRESUPUESTO = Convert.ToDecimal(filaresultado["ID_PRESUPUESTO"]);
            ID_EMPRESA = Convert.ToDecimal(filaresultado["ID_EMPRESA"]);
        }
        
        CargarValidadorPresupuesto(ID_PRESUPUESTO);

        CargarValidadorPersonalCitado(ID_EMPRESA);
    }

    private void CargarActividadesDeSubPrograma(DataTable tablaActividades, DropDownList drop)
    {
        drop.Items.Clear();

        drop.Items.Add(new ListItem("Seleccione...", ""));

        foreach (DataRow f in tablaActividades.Rows)
        {
            drop.Items.Add(new ListItem(f["NOMBRE_ACTIVIDAD"].ToString().Trim(), f["ID_DETALLE_GENERAL"].ToString().Trim() + ":" + f["ID_ACTIVIDAD"].ToString().Trim()));
        }

        drop.DataBind();
    }

    protected void DropDownList_SubPrograma_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_SubPrograma.SelectedIndex <= 0)
        {
            DropDownList_IdActividad.SelectedIndex = 0;
            DropDownList_Tipo.SelectedIndex = 0;
            DropDownList_Sector.SelectedIndex = 0;
            DropDownList_EstadoActividad.SelectedIndex = 0;
            TextBox_DescripcionActividad.Text = "";
        }
        else
        {
            Decimal ID_DETALLE_GENERAL_PADRE = Convert.ToDecimal(DropDownList_SubPrograma.SelectedValue.Split(':')[0]);

            Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaActividades = _programa.ObtenerActividadesPorDetalleGeneralPadre(ID_DETALLE_GENERAL_PADRE);

            CargarActividadesDeSubPrograma(tablaActividades, DropDownList_IdActividad);

            Cargar(Listas.TiposActividad, DropDownList_Tipo);
            Cargar(Listas.SectoresActividad, DropDownList_Sector);
            Cargar(Listas.EstadosActividades, DropDownList_EstadoActividad);

            DropDownList_IdActividad.SelectedIndex = 0;
            DropDownList_Tipo.SelectedIndex = 0;
            DropDownList_Sector.SelectedIndex = 0;
            DropDownList_EstadoActividad.SelectedIndex = 0;
            TextBox_DescripcionActividad.Text = "";
        }
    }

    protected void DropDownList_IdActividad_SelectedIndexChanged(object sender, EventArgs e)
    {
        Programa.Areas AREA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);

        if (DropDownList_IdActividad.SelectedIndex <= 0)
        {
            DropDownList_Tipo.SelectedIndex = 0;
            DropDownList_Sector.SelectedIndex = 0;
            DropDownList_EstadoActividad.SelectedIndex = 0;
            TextBox_DescripcionActividad.Text = "";
        }
        else
        {
            Decimal ID_ACTIVIDAD = Convert.ToDecimal(DropDownList_IdActividad.SelectedValue.Split(':')[1]);

            ActividadRseGlobal _act = new ActividadRseGlobal(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaAct = _act.ObtenerActividadPorId(ID_ACTIVIDAD, AREA);

            DataRow filaAct = tablaAct.Rows[0];

            DropDownList_Tipo.SelectedValue = filaAct["TIPO"].ToString().Trim();
            DropDownList_Sector.SelectedValue = filaAct["SECTOR"].ToString().Trim();
            DropDownList_EstadoActividad.SelectedValue = filaAct["ACTIVO"].ToString().Trim();

            TextBox_DescripcionActividad.Text = filaAct["DESCRIPCION"].ToString().Trim();
        }
    }

    private void inhabilitar_DropDownList_DEPARTAMENTO()
    {
        DropDownList_DEPARTAMENTO.Enabled = false;
        DropDownList_DEPARTAMENTO.Items.Clear();
        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_DEPARTAMENTO.Items.Add(item);
        DropDownList_DEPARTAMENTO.DataBind();
    }

    private void inhabilitar_DropDownList_CIUDAD()
    {
        DropDownList_CIUDAD.Enabled = false;
        DropDownList_CIUDAD.Items.Clear();
        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_CIUDAD.Items.Add(item);
        DropDownList_CIUDAD.DataBind();
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

    protected void DropDownList_REGIONAL_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_REGIONAL.SelectedValue == "")
        {
            inhabilitar_DropDownList_CIUDAD();
            inhabilitar_DropDownList_DEPARTAMENTO();
        }
        else
        {
            cargar_DropDownList_DEPARTAMENTO(DropDownList_REGIONAL.SelectedValue.ToString());
            DropDownList_DEPARTAMENTO.Enabled = true;
            inhabilitar_DropDownList_CIUDAD();
        }
    }

    private void cargar_DropDownList_CIUDAD(String idRegional, String idDepartamento)
    {
        DropDownList_CIUDAD.Items.Clear();

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
        DataTable tablaCiudades = _ciudad.ObtenerCiudadesPorIdDepartamentoEIdRegional(idRegional, idDepartamento);

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_CIUDAD.Items.Add(item);

        foreach (DataRow fila in tablaCiudades.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_CIUDAD"].ToString());
            DropDownList_CIUDAD.Items.Add(item);
        }

        DropDownList_CIUDAD.DataBind();
    }

    protected void DropDownList_DEPARTAMENTO_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_DEPARTAMENTO.SelectedValue == "")
        {
            inhabilitar_DropDownList_CIUDAD();
        }
        else
        {
            cargar_DropDownList_CIUDAD(DropDownList_REGIONAL.SelectedValue.ToString(), DropDownList_DEPARTAMENTO.SelectedValue.ToString());
            DropDownList_CIUDAD.Enabled = true;
        }
    }
    protected void Button_GAURDAR_ACTIVIDAD_Click(object sender, EventArgs e)
    {
        Decimal ID_MAESTRA_COMPROMISO = Convert.ToDecimal(HiddenField_ID_MAESTRA_COMPROMISO.Value);

        Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoAdicional = _programa.ObtenerIdProgramaGeneralDesdeIdMaestraCompromiso(ID_MAESTRA_COMPROMISO);

        DataRow filaadicional = tablaInfoAdicional.Rows[0];

        Decimal PRESUPUESTO_APROBADO = Convert.ToDecimal(TextBox_PresupuestoAsignado.Text.Trim());
        
        Decimal ID_PROGRAMA = Convert.ToDecimal(filaadicional["ID_PROGRAMA"]);
        Decimal ID_PROGRAMA_GENERAL = Convert.ToDecimal(filaadicional["ID_PROGRAMA_GENERAL"]);

        Decimal ID_EMPRESA = Convert.ToDecimal(filaadicional["ID_EMPRESA"]);
        Decimal ID_PRESUPUESTO = Convert.ToDecimal(filaadicional["ID_PRESUPUESTO"]);

        Int32 ANNO = DateTime.Now.Year;

        Programa.Areas AREA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);

        Decimal ID_DETALLE_GENERAL = Convert.ToDecimal(DropDownList_IdActividad.SelectedValue.Split(':')[0]);
        Decimal ID_ACTIVIDAD = Convert.ToDecimal(DropDownList_IdActividad.SelectedValue.Split(':')[1]);

        String RESUMEN_ACTIVIDAD = TextBox_Resumen.Text.Trim();

        DateTime FECHA_ACTIVIDAD = Convert.ToDateTime(TextBox_FechaActividad.Text.Trim());

        String HORA_INICIO = TimePicker_HoraInicioActividad.SelectedTime.ToShortTimeString();
        String HORA_FIN = TimePicker_HoraFinActividad.SelectedTime.ToShortTimeString();

        Int32 PERSONAL_CITADO = Convert.ToInt32(TextBox_PersonalCitado.Text.Trim());

        String ENCARGADO = DropDownList_Encargado.SelectedValue;

        String ID_CIUDAD = DropDownList_CIUDAD.SelectedValue;


        Boolean verificado = _programa.AdicionarDetalleActividad(ID_PROGRAMA_GENERAL, ID_PROGRAMA, ID_EMPRESA, AREA, ID_PRESUPUESTO, ANNO, ID_ACTIVIDAD, FECHA_ACTIVIDAD, HORA_INICIO, HORA_FIN, PRESUPUESTO_APROBADO, PERSONAL_CITADO, ENCARGADO, ID_CIUDAD, RESUMEN_ACTIVIDAD, ID_DETALLE_GENERAL);

        if (verificado == true)
        {
            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.CargarCompromiso);

            Cargar(ID_MAESTRA_COMPROMISO);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "lLa actividad fue registrada correctamente, ahora aparecerá en el programador.", Proceso.Correcto);
        }
        else
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _programa.MensajeError, Proceso.Error);
        }
    }
    protected void Button_CANCELAR_ACTIVIDAD_Click(object sender, EventArgs e)
    {
        Decimal ID_MAESTRA_COMPROMISO = Convert.ToDecimal(HiddenField_ID_MAESTRA_COMPROMISO.Value);

        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.CargarCompromiso);

        Cargar(ID_MAESTRA_COMPROMISO);
    }
}