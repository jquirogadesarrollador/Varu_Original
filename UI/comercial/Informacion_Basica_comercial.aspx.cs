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
        NuevoContrato,
        cargar_GridView_RESULTADOS_BUSQUEDA
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }


    private enum AccionesGrilla
    {
        Ninguna,
        Nuevo,
        Modificar,
        Eliminar
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
                Button_NUEVO_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
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
                Button_NUEVO_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
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

                this.txt_descripcion.Enabled = false;
                this.CheckBox_SI.Enabled = false;
                this.CheckBox_NO.Enabled = false;
                this.txt_aclaracion.Enabled = false;

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
                Panel_FORM_BOTONES_PIE.Visible = false;
                Button_NUEVO_1.Visible = false;

                Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.CargarContrato:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Panel_FORM_BOTONES_PIE.Visible = false;
                Button_MODIFICAR_1.Visible = true;

                Panel_FORMULARIO.Visible = true;
                Panel_CONTROL_REGISTRO.Visible = true;
                Panel_IDENTIFICADOR.Visible = true;
                Panel_OBJETIVO_CONTRATO.Visible = true;
                Panel_FIRMADO.Visible = true;
                Panel_Enviado.Visible = true;
                break;
            case Acciones.ModificarContrato:
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;

                Panel_HistorialEnvios.Visible = true;
                Panel_BotonesAccion.Visible = true;
                Button_NUEVA_ACCION.Visible = true;
                break;
            case Acciones.NuevoContrato:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Panel_FORM_BOTONES_PIE.Visible = false;
                Button_GUARDAR_1.Visible = true;
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

    private void cargar_GridView_RESULTADOS_BUSQUEDA_Administrador()
    {
        contratosServicio _contratosServico = new contratosServicio(Session["idEmpresa"].ToString());
        Int32 operaciones = 1;
        String Codigo = "";
        String Descripcion = "";
        DataTable tablaContratosOriginal = _contratosServico.ObtenerITEMSInformacionBasicapor(operaciones, Codigo, Descripcion);
        this.div_GridViewAdministradorInfoBasicaComercia.Visible = true;
        GridViewAdministradorInfoBasicaComercia.Visible = true;
        GridViewAdministradorInfoBasicaComercia.DataSource = tablaContratosOriginal;
        GridViewAdministradorInfoBasicaComercia.DataBind();
        ACTUALIZAR.Visible = false;
        Button3.Visible = false;
        this.txt_Nuevo_Registro.Text = "";

        for (int i = 0; i < GridViewAdministradorInfoBasicaComercia.Rows.Count; i++)
        {
            GridViewAdministradorInfoBasicaComercia.Rows[i].Cells[2].Enabled = false;
            GridViewRow filaGrilla = GridViewAdministradorInfoBasicaComercia.Rows[i];

            Label TXT_SI_NO = filaGrilla.FindControl("TXT_SINO") as Label;
            String _txtCodigo = TXT_SI_NO.Text;
            if (_txtCodigo == "X")
            {
                GridViewAdministradorInfoBasicaComercia.Rows[i].Cells[0].Enabled = false; 
                GridViewAdministradorInfoBasicaComercia.Rows[i].Cells[1].Enabled = true;  
                GridViewAdministradorInfoBasicaComercia.Rows[i].Cells[2].Enabled = false; 

                TextBox lblDESCRIPCION = filaGrilla.FindControl("TXTDESCRIPCION") as TextBox;
                lblDESCRIPCION.Enabled = false; 
                lblDESCRIPCION.BackColor = System.Drawing.ColorTranslator.FromHtml("#BDC0C6");
                lblDESCRIPCION.BorderColor = System.Drawing.ColorTranslator.FromHtml("#BDC0C6");
                filaGrilla.BackColor = System.Drawing.ColorTranslator.FromHtml("#BDC0C6");
            }
            else
            {
                GridViewAdministradorInfoBasicaComercia.Rows[i].Cells[0].Enabled = false;
                GridViewAdministradorInfoBasicaComercia.Rows[i].Cells[1].Enabled = true;
                GridViewAdministradorInfoBasicaComercia.Rows[i].Cells[2].Enabled = false;
            }
        }
    }
    private void cargar_GridView_RESULTADOS_BUSQUEDA()
    {

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String reg = QueryStringSeguro["reg"].ToString();
        contratosServicio _contratosServico = new contratosServicio(Session["idEmpresa"].ToString());

        DataTable tablaContratosOriginal = _contratosServico.ObtenerInformacionBasicaporId_Empresa(Convert.ToDecimal(reg));

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

        for (int i = 0; i < GridView_RESULTADOS_BUSQUEDA.Rows.Count; i++)
        {
            GridView_RESULTADOS_BUSQUEDA.Rows[i].Cells[2].Enabled = false;
            GridViewRow filaGrilla = GridView_RESULTADOS_BUSQUEDA.Rows[i];

            Label TXT_SI_NO = filaGrilla.FindControl("TXT_SINO") as Label;
            String _txtCodigo = TXT_SI_NO.Text;
            Label _TXT_ESTADO_REG = filaGrilla.FindControl("TXT_ESTADO_REG") as Label;
            String _txtESTADO = _TXT_ESTADO_REG.Text;
            if (_txtCodigo != "X")
            {
                RadioButtonList lblRadioSINO = filaGrilla.FindControl("RadioButtonListSN") as RadioButtonList;
                TextBox lblDESCRIPCION = filaGrilla.FindControl("TXT_ACLARACION") as TextBox;
                lblRadioSINO.SelectedValue = _txtESTADO;
                lblRadioSINO.Enabled = false;
                lblDESCRIPCION.Enabled = false; 
                lblDESCRIPCION.BackColor = System.Drawing.ColorTranslator.FromHtml("#BDC0C6");
                lblDESCRIPCION.BorderColor = System.Drawing.ColorTranslator.FromHtml("#BDC0C6");
                filaGrilla.BackColor = System.Drawing.ColorTranslator.FromHtml("#BDC0C6");
            }
            else
            {
                GridView_RESULTADOS_BUSQUEDA.Rows[i].Cells[0].Enabled = false;
                GridView_RESULTADOS_BUSQUEDA.Rows[i].Cells[1].Enabled = false;
                GridView_RESULTADOS_BUSQUEDA.Rows[i].Cells[2].Enabled = true;
            }
        
        }
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                        tools _tools = new tools();
                        SecureQueryString QueryStringSeguro;
                        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);
                        try
                        {
                            String reg = QueryStringSeguro["reg"].ToString();
                            cargar_GridView_RESULTADOS_BUSQUEDA();
                            GridViewAdministradorInfoBasicaComercia.Visible = false;
                            this.div_GridViewAdministradorInfoBasicaComercia.Visible = false;
                        }
                        catch
                        {
                            cargar_GridView_RESULTADOS_BUSQUEDA_Administrador();
                            GridViewAdministradorInfoBasicaComercia.Visible = true;
                            this.div_GridViewAdministradorInfoBasicaComercia.Visible = true;
                        }
                break;
            case Acciones.NuevoContrato:
                HiddenField_REGISTRO_CONTRATO.Value = "";

                TextBox_NUMERO_CONTRATO.Text = "";

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
                this.txt_descripcion.Enabled = false;
                this.CheckBox_SI.Enabled = false;
                this.CheckBox_NO.Enabled = false;
                this.txt_aclaracion.Enabled = false;

                DropDownList_TIPO_SERVICIO_RESPECTIVO.Enabled = true;
                TextBox_DetalleServicioRespectivo.Enabled = true;

                CheckBox_FIRMADO.Enabled = true;
                CheckBox_ENVIO_CTE.Enabled = true;
                break;
            case Acciones.NuevoContrato:
                TextBox_NUMERO_CONTRATO.Enabled = true;

                this.txt_descripcion.Enabled = false;
                this.CheckBox_SI.Enabled = false;
                this.CheckBox_NO.Enabled = false;
                this.txt_aclaracion.Enabled = false;


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
    }

    private void Guardar()
    {

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

    private void Cargar(DataRow filaInfoContrato)
    {
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
    protected void CheckBoxGrid_SI_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void ACTUALIZAR_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);


        for (int i = 0; i < GridView_RESULTADOS_BUSQUEDA.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_RESULTADOS_BUSQUEDA.Rows[i];

            Label txtCodigo = filaGrilla.FindControl("lblCODIGO") as Label; 
            TextBox lblDESCRIPCION = filaGrilla.FindControl("TXT_ACLARACION") as TextBox;
            Decimal EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"].ToString());
            RadioButtonList lblRadioSINO = filaGrilla.FindControl("RadioButtonListSN") as RadioButtonList;
            Label txtSiNo = filaGrilla.FindControl("lblCODIGO") as Label; 
            String _lblDESCRIPCION = Convert.ToString(lblDESCRIPCION.Text);
            String _ESTADO = lblRadioSINO.SelectedValue;


            Label TXT_SI_NO = filaGrilla.FindControl("TXT_SINO") as Label;
            String _txtCodigo = txtCodigo.Text;
            String _txtCodigoActualiza = TXT_SI_NO.Text;

            Informacion_Basica_comercial _contratosServico = new Informacion_Basica_comercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            Decimal TABLA_CONTEO = _contratosServico.Info_basica_existe(_txtCodigo, EMPRESA);
                if (TABLA_CONTEO > 0)
                {
                    if (_txtCodigoActualiza == "X")
                    {
                        Decimal tablaContratosOriginal = _contratosServico.ActualizarInformacionBasicaComercial(_txtCodigo, _lblDESCRIPCION, EMPRESA, _ESTADO);
                    }
                    else
                    {
                    }
                }
                else
                {
                    Decimal tablaContratosOriginal = _contratosServico.AdicionarInformacionBasicaComercial(_txtCodigo, _lblDESCRIPCION, EMPRESA, _ESTADO);                  
                }
        }
        cargar_GridView_RESULTADOS_BUSQUEDA();
    }
    protected void GridView_RESULTADOS_BUSQUEDA_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        switch (e.CommandName)
        {
            case "Editar":
                ActivarFilaGrilla(GridView_RESULTADOS_BUSQUEDA, indexSeleccionado, 2);
                break;
            case "Borrar":
                tools _tools = new tools();
                SecureQueryString QueryStringSeguro;
                QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

                GridViewRow filaGrilla = GridView_RESULTADOS_BUSQUEDA.Rows[indexSeleccionado];
                Label txtCodigo = filaGrilla.FindControl("lblCODIGO") as Label; 
                Label TXT_SI_NO = filaGrilla.FindControl("TXT_SINO") as Label;
                String _txtCodigo = txtCodigo.Text;
                Label _TXT_ESTADO_REG = filaGrilla.FindControl("TXT_ESTADO_REG") as Label;
                String _txtESTADO = _TXT_ESTADO_REG.Text;
                Decimal EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"].ToString());

                String _lblDESCRIPCION = "";
                String _ESTADO = "";

                Informacion_Basica_comercial _contratosServico = new Informacion_Basica_comercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                Decimal TABLA_CONTEO = _contratosServico.Info_basica_existe(_txtCodigo, EMPRESA);
                Decimal tablaContratosOriginal = _contratosServico.ActualizarInformacionBasicaComercial(_txtCodigo, _lblDESCRIPCION, EMPRESA, _ESTADO);
                cargar_GridView_RESULTADOS_BUSQUEDA();
                break;
            case "Grabar":
                GrabarFilaGrilla(GridView_RESULTADOS_BUSQUEDA, indexSeleccionado, 2);
                break;
        }
    }
    private void ActivarFilaGrilla(GridView grilla, int fila, int colInicio)
    {
        GridViewRow filaGrilla = grilla.Rows[fila];
        Label TXT_SI_NO = filaGrilla.FindControl("TXT_SINO") as Label;
        String _txtCodigo = TXT_SI_NO.Text;
        Label _TXT_ESTADO_REG = filaGrilla.FindControl("TXT_ESTADO_REG") as Label;
        String _txtESTADO = _TXT_ESTADO_REG.Text;
        for (int i = 0; i < grilla.Columns.Count; i++)
        {
            grilla.Rows[fila].Cells[i].Enabled = true;
            RadioButtonList lblRadioSINO = filaGrilla.FindControl("RadioButtonListSN") as RadioButtonList;
            TextBox lblDESCRIPCION = filaGrilla.FindControl("TXT_ACLARACION") as TextBox;
            lblRadioSINO.SelectedValue = _txtESTADO;
            TXT_SI_NO.Text = "X";
            lblRadioSINO.Enabled = true;
            lblDESCRIPCION.Enabled = true;
            grilla.Rows[fila].BackColor = System.Drawing.ColorTranslator.FromHtml("#50CE04");  
            lblDESCRIPCION.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F2EC");
            GridView_RESULTADOS_BUSQUEDA.Rows[i].Cells[2].Enabled = true;
        }
    }
    private void ActivarFilaGrilla_2(GridView grilla, int fila, int colInicio)
    {
        GridViewRow filaGrilla = grilla.Rows[fila];
        Label TXT_SI_NO = filaGrilla.FindControl("TXT_SINO") as Label;
        String _txtCodigo = TXT_SI_NO.Text;
        Label _TXT_ESTADO_REG = filaGrilla.FindControl("TXT_ESTADO_REG") as Label;
        String _txtESTADO = _TXT_ESTADO_REG.Text;
        for (int i = 0; i < grilla.Columns.Count; i++)
        {
            grilla.Rows[fila].Cells[i].Enabled = true;
            TXT_SI_NO.Text = "X";
            TextBox lblDESCRIPCION = filaGrilla.FindControl("TXTDESCRIPCION") as TextBox;
            lblDESCRIPCION.Enabled = false; 
            lblDESCRIPCION.BackColor = System.Drawing.ColorTranslator.FromHtml("#BDC0C6");
            lblDESCRIPCION.BorderColor = System.Drawing.ColorTranslator.FromHtml("#BDC0C6");
            filaGrilla.BackColor = System.Drawing.ColorTranslator.FromHtml("#BDC0C6");
            lblDESCRIPCION.Enabled = true;

            grilla.Rows[fila].BackColor = System.Drawing.ColorTranslator.FromHtml("#50CE04");  
            lblDESCRIPCION.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F2EC");
            grilla.Rows[i].Cells[2].Enabled = true;
        }
    }
    private void GrabarFilaGrilla(GridView grilla, int fila, int colInicio)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        GridViewRow filaGrilla = GridView_RESULTADOS_BUSQUEDA.Rows[fila];

        Label txtCodigo = filaGrilla.FindControl("lblCODIGO") as Label; 
        String _txtCodigo = txtCodigo.Text;
        TextBox lblDESCRIPCION = filaGrilla.FindControl("TXT_ACLARACION") as TextBox;
        Decimal EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"].ToString());
        RadioButtonList lblRadioSINO = filaGrilla.FindControl("RadioButtonListSN") as RadioButtonList;
        Label txtSiNo = filaGrilla.FindControl("lblCODIGO") as Label; 
        String _lblDESCRIPCION = Convert.ToString(lblDESCRIPCION.Text);
        String _ESTADO = lblRadioSINO.SelectedValue;

            Informacion_Basica_comercial _contratosServico = new Informacion_Basica_comercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            Decimal tablaContratosOriginal = _contratosServico.ActualizarInformacionBasicaComercial(_txtCodigo, _lblDESCRIPCION, EMPRESA, _ESTADO);
            cargar_GridView_RESULTADOS_BUSQUEDA();
    }

    private void GrabarFilaGrilla_2(GridView grilla, int fila, int colInicio)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        GridViewRow filaGrilla = GridViewAdministradorInfoBasicaComercia.Rows[fila];

        Label txtCodigo = filaGrilla.FindControl("lblCODIGO") as Label; 
        String _txtCodigo = txtCodigo.Text;
        TextBox lblDESCRIPCION = filaGrilla.FindControl("TXTDESCRIPCION") as TextBox;
        Label txtSiNo = filaGrilla.FindControl("lblCODIGO") as Label; 
        String _lblDESCRIPCION = lblDESCRIPCION.Text;

        Int32 operaciones = 3 ;

        contratosServicio _contratosServico = new contratosServicio(Session["idEmpresa"].ToString());
        DataTable tablaContratosOriginal = _contratosServico.ObtenerITEMSInformacionBasicapor(operaciones, _txtCodigo, _lblDESCRIPCION);
        cargar_GridView_RESULTADOS_BUSQUEDA_Administrador();
    }
    private void Borrar_Grilla(GridView grilla, int fila, int colInicio)
    {
        GridViewRow filaGrilla = GridView_RESULTADOS_BUSQUEDA.Rows[fila];
        Label TXT_SI_NO = filaGrilla.FindControl("TXT_SINO") as Label;
        String _txtCodigo = TXT_SI_NO.Text;
        Label _TXT_ESTADO_REG = filaGrilla.FindControl("TXT_ESTADO_REG") as Label;
        String _txtESTADO = _TXT_ESTADO_REG.Text;
        for (int i = 0; i < grilla.Columns.Count; i++)
        {
            grilla.Rows[fila].Cells[i].Enabled = true;
            RadioButtonList lblRadioSINO = filaGrilla.FindControl("RadioButtonListSN") as RadioButtonList;
            TextBox lblDESCRIPCION = filaGrilla.FindControl("TXT_ACLARACION") as TextBox;
            lblRadioSINO.SelectedValue = _txtESTADO;
            TXT_SI_NO.Text = "X";
            lblRadioSINO.Enabled = true;
            lblDESCRIPCION.Enabled = true;
            grilla.Rows[fila].BackColor = System.Drawing.ColorTranslator.FromHtml("#50CE04"); 
            lblDESCRIPCION.BackColor = System.Drawing.ColorTranslator.FromHtml("#F0F2EC");
            cargar_GridView_RESULTADOS_BUSQUEDA();
            GridView_RESULTADOS_BUSQUEDA.Rows[i].Cells[2].Enabled = true;
        }
    }
    protected void GridViewAdministradorInfoBasicaComercia_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);
        switch (e.CommandName)
        {
            case "Editar":
                ActivarFilaGrilla_2(GridViewAdministradorInfoBasicaComercia, indexSeleccionado, 2);
                break;
            case "Borrar":
                tools _tools = new tools();
                SecureQueryString QueryStringSeguro;
                QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

                GridViewRow filaGrilla = GridViewAdministradorInfoBasicaComercia.Rows[indexSeleccionado];

                Label txtCodigo = filaGrilla.FindControl("lblCODIGO") as Label; 
                String _txtCodigo = txtCodigo.Text;
                TextBox lblDESCRIPCION = filaGrilla.FindControl("TXTDESCRIPCION") as TextBox;
                Label txtSiNo = filaGrilla.FindControl("lblCODIGO") as Label; 
                String _lblDESCRIPCION = lblDESCRIPCION.Text;

                Int32 operaciones = 4 ;

                contratosServicio _contratosServico = new contratosServicio(Session["idEmpresa"].ToString());
                DataTable tablaContratosOriginal = _contratosServico.ObtenerITEMSInformacionBasicapor(operaciones, _txtCodigo, _lblDESCRIPCION);
                cargar_GridView_RESULTADOS_BUSQUEDA_Administrador();
                break;
            case "Grabar":
                GrabarFilaGrilla_2(GridView_RESULTADOS_BUSQUEDA, indexSeleccionado, 2);
                break;
        }
    }
    protected void Grabar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String _lblDESCRIPCION = this.txt_Nuevo_Registro.Text;
        Int32 operaciones = 2;
        String _txtCodigo = "";

        contratosServicio _contratosServico = new contratosServicio(Session["idEmpresa"].ToString());
        DataTable tablaContratosOriginal = _contratosServico.ObtenerITEMSInformacionBasicapor(operaciones, _txtCodigo, _lblDESCRIPCION);
        cargar_GridView_RESULTADOS_BUSQUEDA_Administrador();
    }
}