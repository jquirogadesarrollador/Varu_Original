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
using System.Web.Util;
using Brainsbits.LLB.seguridad;

public partial class _PresupuestoPrograma : System.Web.UI.Page
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
    private System.Drawing.Color colorSeleccionado = System.Drawing.ColorTranslator.FromHtml("#699DF0");

    Programa.Areas GLO_AREA_RSE_GLOBAL;

    private enum Acciones
    {
        Inicio = 0, 
        PresGenSeleccionado, 
        PresupuestoEmpresaSeleccionado, 
        ModificarPreGen,
        ModificarDetallePresGen,
        CambioPaginaDetallePresGen
    }

    private enum AccionesGrilla
    {
        Ninguna,
        Nuevo,
        Modificar,
        Eliminar
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
                Panel_RESULTADOS_GRID.Visible = false;
                Panel_DatosPresupuestoAnioSeleccionado.Visible = false;
                Panel_DetallesPresupuestoGeneral.Visible = false;
                Panel_HistorialDetallePresupuestoGeneral.Visible = false;
                Panel_DetallePorMes.Visible = false;
                Panel_GrillaDetallesActividadesDelMes.Visible = false;
                break;
            case Acciones.PresGenSeleccionado:
                Panel_HistorialDetallePresupuestoGeneral.Visible = false;
                Panel_DetallePorMes.Visible = false;
                Panel_GrillaDetallesActividadesDelMes.Visible = false;
                break;
            case Acciones.PresupuestoEmpresaSeleccionado:
                Panel_GrillaDetallesActividadesDelMes.Visible = false;
                break;
            case Acciones.ModificarPreGen:
                Panel_DatosPresupuestoAnioSeleccionado.Visible = false;
                Panel_DetallesPresupuestoGeneral.Visible = false;
                Panel_HistorialDetallePresupuestoGeneral.Visible = false;
                Panel_DetallePorMes.Visible = false;
                Panel_GrillaDetallesActividadesDelMes.Visible = false;
                break;
            case Acciones.ModificarDetallePresGen:
                Panel_HistorialDetallePresupuestoGeneral.Visible = false;
                Panel_DetallePorMes.Visible = false;
                Panel_GrillaDetallesActividadesDelMes.Visible = false;
                break;
            case Acciones.CambioPaginaDetallePresGen:
                Panel_HistorialDetallePresupuestoGeneral.Visible = false;
                Panel_DetallePorMes.Visible = false;
                Panel_GrillaDetallesActividadesDelMes.Visible = false;
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
            case Acciones.PresGenSeleccionado:
                Panel_DatosPresupuestoAnioSeleccionado.Visible = true;
                Panel_DetallesPresupuestoGeneral.Visible = true;
                break;
            case Acciones.PresupuestoEmpresaSeleccionado:
                Panel_HistorialDetallePresupuestoGeneral.Visible = true;
                break;
        }
    }

    private void Desactivar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                TextBox_PresupuestoEneroAsignado.Enabled = false;
                TextBox_PresupuestoEneroEjecutado.Enabled = false;

                TextBox_PresupuestoFebreroAsignado.Enabled = false;
                TextBox_PresupuestoFebreroEjecutado.Enabled = false;

                TextBox_PresupuestoMarzoAsignado.Enabled = false;
                TextBox_PresupuestoMarzoEjecutado.Enabled = false;

                TextBox_PresupuestoAbrilAsignado.Enabled = false;
                TextBox_PresupuestoAbrilEjecutado.Enabled = false;

                TextBox_PresupuestoMayoAsignado.Enabled = false;
                TextBox_PresupuestoMayoEjecutado.Enabled = false;

                TextBox_PresupuestoJunioAsignado.Enabled = false;
                TextBox_PresupuestoJunioEjecutado.Enabled = false;

                TextBox_PresupuestoJulioAsignado.Enabled = false;
                TextBox_PresupuestoJulioEjecutado.Enabled = false;

                TextBox_PresupuestoAgostoAsignado.Enabled = false;
                TextBox_PresupuestoAgostoEjecutado.Enabled = false;

                TextBox_PresupuestoSeptiembreAsignado.Enabled = false;
                TextBox_PresupuestoSeptiembreEjecutado.Enabled = false;

                TextBox_PresupuestoOctubreAsignado.Enabled = false;
                TextBox_PresupuestoOctubreEjecutado.Enabled = false;

                TextBox_PresupuestoNoviembreAsignado.Enabled = false;
                TextBox_PresupuestoNoviembreEjecutado.Enabled = false;

                TextBox_PresupuestoDicimebreAsignado.Enabled = false;
                TextBox_PresupuestoDiciembreEjecutado.Enabled = false;

                break;
        }
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

    private DataTable Obtener_EstructuraDataTablePara_GridView_PresupuestosAnio()
    {
        DataTable tablaResultado = new DataTable();

        tablaResultado.Columns.Add("ID_PRES_GEN",typeof(Decimal));
        tablaResultado.Columns.Add("ANIO", typeof(Int32));
        tablaResultado.Columns.Add("ID_AREA", typeof(String));
        tablaResultado.Columns.Add("MONTO", typeof(Decimal));
        tablaResultado.Columns.Add("EJECUTADO", typeof(Decimal));
        tablaResultado.Columns.Add("ASIGNADO", typeof(Decimal));
        tablaResultado.Columns.Add("DESCRIPCION", typeof(String));

        return tablaResultado;

    }

    private DataTable AdicionarAnioActual(DataTable tablaPresupuestosGenerales)
    {
        ConfigurarAreaRseGlobal();

        DataTable tablaParaGrilla = Obtener_EstructuraDataTablePara_GridView_PresupuestosAnio();

        DataRow filaParaGrilla = tablaParaGrilla.NewRow();

        filaParaGrilla["ID_PRES_GEN"] = 0;
        filaParaGrilla["ANIO"] = DateTime.Now.Year;
        filaParaGrilla["ID_AREA"] = GLO_AREA_RSE_GLOBAL.ToString();
        filaParaGrilla["MONTO"] = 0.00;
        filaParaGrilla["EJECUTADO"] = 0.00;
        filaParaGrilla["ASIGNADO"] = 0.00;
        filaParaGrilla["DESCRIPCION"] = "";

        tablaParaGrilla.Rows.Add(filaParaGrilla);

        foreach (DataRow filaOriginal in tablaPresupuestosGenerales.Rows)
        {
            DataRow filaNuevaGrilla = tablaParaGrilla.NewRow();

            filaNuevaGrilla["ID_PRES_GEN"] = filaOriginal["ID_PRES_GEN"];
            filaNuevaGrilla["ANIO"] = filaOriginal["ANIO"];
            filaNuevaGrilla["ID_AREA"] = GLO_AREA_RSE_GLOBAL.ToString();
            filaNuevaGrilla["MONTO"] = filaOriginal["MONTO"];
            filaNuevaGrilla["EJECUTADO"] = filaOriginal["EJECUTADO"];
            filaNuevaGrilla["ASIGNADO"] = filaOriginal["ASIGNADO"];
            filaNuevaGrilla["DESCRIPCION"] = filaOriginal["DESCRIPCION"];

            tablaParaGrilla.Rows.Add(filaNuevaGrilla);
        }

        return tablaParaGrilla;
    }

    private void Cargar_GridView_PresupuestosAnio_DesdeDataTable(DataTable tablaParaGrilla)
    {
        GridView_PresupuestosAnio.DataSource = tablaParaGrilla;
        GridView_PresupuestosAnio.DataBind();

        for (int i = 0; i < GridView_PresupuestosAnio.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_PresupuestosAnio.Rows[i];
            DataRow filaTabla = tablaParaGrilla.Rows[i];

            Decimal monto = 0;
            Decimal asignado = 0;
            Decimal ejecutado = 0;
            Decimal saldo = 0;

            try
            {
                monto = Convert.ToDecimal(filaTabla["MONTO"]);
            }
            catch
            {
                monto = 0;
            }

            try
            {
                asignado = Convert.ToDecimal(filaTabla["ASIGNADO"]);
            }
            catch
            {
                asignado = 0;
            }

            try
            {
                ejecutado = Convert.ToDecimal(filaTabla["EJECUTADO"]);
            }
            catch
            {
                ejecutado = 0;
            }

            saldo = monto - asignado;

            TextBox textoMonto = filaGrilla.FindControl("TextBox_Monto") as TextBox;
            textoMonto.Text = Convert.ToDecimal(filaTabla["MONTO"]).ToString();

            TextBox textoAsignado = filaGrilla.FindControl("TextBox_Asignado") as TextBox;
            textoAsignado.Text = Convert.ToDecimal(filaTabla["ASIGNADO"]).ToString();
            
            TextBox textoEjecutado = filaGrilla.FindControl("TextBox_Ejecutado") as TextBox;
            textoEjecutado.Text = Convert.ToDecimal(filaTabla["EJECUTADO"]).ToString();

            TextBox textoSaldo = filaGrilla.FindControl("TextBox_Saldo") as TextBox;
            textoSaldo.Text = saldo.ToString();

            TextBox textoDescripcion = filaGrilla.FindControl("TextBox_Descripcion") as TextBox;
            textoDescripcion.Text = filaTabla["DESCRIPCION"].ToString().Trim();
        }
    }

    private Int32 UltimoAnioEnTabla(DataTable tablaPresupuestosGenerales)
    {
        Int32 anioMax = 0;

        foreach (DataRow fila in tablaPresupuestosGenerales.Rows)
        {
            Int32 anioSeleccionado = Convert.ToInt32(fila["ANIO"]);

            if (anioSeleccionado > anioMax)
            {
                anioMax = anioSeleccionado;
            }
        }

        return anioMax;
    }

    private void Cargar_GridView_PresupuestosAnio()
    {
        DataTable tablaParaGrilla = new DataTable();

        Boolean correcto = true;

        ConfigurarAreaRseGlobal();

        presupuesto _pres = new presupuesto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaPresupuestosGenerales = _pres.ObtenerPresupuestosGeneralesArea(GLO_AREA_RSE_GLOBAL);

        if (tablaPresupuestosGenerales.Rows.Count <= 0)
        {
            if (_pres.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _pres.MensajeError, Proceso.Error);

                correcto = false;
            }
            else
            {
                tablaParaGrilla = AdicionarAnioActual(tablaPresupuestosGenerales);
            }
        }
        else
        {
            if (UltimoAnioEnTabla(tablaPresupuestosGenerales) < DateTime.Now.Year)
            {
                tablaParaGrilla = AdicionarAnioActual(tablaPresupuestosGenerales);
            }
            else
            {
                tablaParaGrilla = tablaPresupuestosGenerales;
            }
        }

        if (correcto == true)
        { 
            Cargar_GridView_PresupuestosAnio_DesdeDataTable(tablaParaGrilla);
        }
    }

    private void ConfigurarAreaRseGlobal()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        int proceso = Convert.ToInt32(QueryStringSeguro["proceso"]);

        HiddenField_ID_AREA.Value = _tools.ObtenerIdAreaProceso(proceso);

        GLO_AREA_RSE_GLOBAL = _tools.ObtenerEnumIdAreaProceso(proceso);
    }

    private void InhabilitarFilasGrilla(GridView grilla, int colInicio)
    {
        for (int i = 0; i < grilla.Rows.Count; i++)
        {
            for (int j = colInicio; j < grilla.Columns.Count; j++)
            {
                grilla.Rows[i].Cells[j].Enabled = false;
            }
        }
    }

    private void ConfigurarBotonesEdicion_GridView_PresupuestosAnio(GridView grilla, Boolean bSeleccionar, Boolean bModificar, Boolean bGuardar, Boolean bCancelar)
    { 
        grilla.Columns[0].Visible = bSeleccionar;

        grilla.Columns[1].Visible = bModificar;

        grilla.Columns[2].Visible = bGuardar;

        grilla.Columns[3].Visible = bCancelar;
    }





    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                ConfigurarAreaRseGlobal();

                HiddenField_INDEX_GRIDVIEW_PRES_GEN.Value = "-1";
                HiddenField_PAGE_GRIDVIEW_PRES_GEN.Value = "0";
                Cargar_GridView_PresupuestosAnio();

                InhabilitarFilasGrilla(GridView_PresupuestosAnio, 4);
                ConfigurarBotonesEdicion_GridView_PresupuestosAnio(GridView_PresupuestosAnio, true, true, false, false);

                HiddenField_ACCION_GRILLA_PRES_GEN.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_ACCION_GRILLA_DET_PRES_GEN.Value = AccionesGrilla.Ninguna.ToString();
                break;
        }
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
        Page.Header.Title = "PRESUPUESTOS";

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

    private void HabilitarFila_GridView_PresupuestosAnio(GridView grilla, int fila)
    {
        grilla.Rows[fila].Cells[5].Enabled = true;
        grilla.Rows[fila].Cells[9].Enabled = true;
    }

    private void HabilitarFila_GridView_DetallesPresupuestoGeneral(GridView grilla, int fila)
    {
        grilla.Rows[fila].Cells[6].Enabled = true;
        grilla.Rows[fila].Cells[10].Enabled = true;
    }

    private void PermitirOprimirBotonesDeGuardadoYCancelarEnFilaSeleccionada_GridView_PresupuestosAnio(GridView grilla, Int32 fila)
    {
        for (int i = 0; i < grilla.Rows.Count; i++)
        {
            if (i != fila)
            {
                grilla.Rows[i].Cells[2].Enabled = false;
                grilla.Rows[i].Cells[3].Enabled = false;
            }
            else
            {
                grilla.Rows[i].Cells[2].Enabled = true;
                grilla.Rows[i].Cells[3].Enabled = true;
            }
        }
    }

    private void PermitirOprimirBotonesDeGuardadoYCancelarEnFilaSeleccionada_GridView_DetallesPresupuestoGeneral(GridView grilla, Int32 fila)
    {
        for (int i = 0; i < grilla.Rows.Count; i++)
        {
            if (i != fila)
            {
                grilla.Rows[i].Cells[2].Enabled = false;
                grilla.Rows[i].Cells[3].Enabled = false;
            }
            else
            {
                grilla.Rows[i].Cells[2].Enabled = true;
                grilla.Rows[i].Cells[3].Enabled = true;
            }
        }
    }

    private void Cargar_GridView_DetallesPresupuestoGeneral_DesdeDataTable(DataTable tablaPresupuestosEmpresa)
    {
        tools _tools = new tools();

        GridView_DetallesPresupuestoGeneral.SelectedIndex = Convert.ToInt32(HiddenField_INDEX_GRID_DET_PRES_GEN.Value);
        GridView_DetallesPresupuestoGeneral.PageIndex = Convert.ToInt32(HiddenField_PAGE_GRID_DET_PRES_GEN.Value);

        GridView_DetallesPresupuestoGeneral.DataSource = tablaPresupuestosEmpresa;
        GridView_DetallesPresupuestoGeneral.DataBind();

        String letraSeleciconada = "A";
        if (String.IsNullOrEmpty(HiddenField_LETRA_PAGINACION_EMPRESA.Value) == false)
        { 
            letraSeleciconada = HiddenField_LETRA_PAGINACION_EMPRESA.Value;
        }

        for (int i = 0; i < GridView_DetallesPresupuestoGeneral.Rows.Count; i++)
        {
            DataRow filaTabla = tablaPresupuestosEmpresa.Rows[(GridView_DetallesPresupuestoGeneral.PageIndex * GridView_DetallesPresupuestoGeneral.PageSize) + i];
            
            Decimal presupuesto = 0;
            Decimal asignado = 0;
            Decimal ejecutado = 0;
            Decimal saldo = 0;

            Decimal ID_EMPRESA = Convert.ToDecimal(GridView_DetallesPresupuestoGeneral.DataKeys[i].Values["ID_EMPRESA"]);

            try
            {
                presupuesto = Convert.ToDecimal(filaTabla["PRESUPUESTO"]);
            }
            catch
            {
                presupuesto = 0;
            }

            try
            {
                asignado = Convert.ToDecimal(filaTabla["ASIGNADO"]);
            }
            catch
            {
                asignado = 0;
            }

            try
            {
                ejecutado = Convert.ToDecimal(filaTabla["EJECUTADO"]);
            }
            catch
            {
                ejecutado = 0;
            }

            saldo = presupuesto - (asignado + ejecutado);

            TextBox textoPresupuesto = GridView_DetallesPresupuestoGeneral.Rows[i].FindControl("TextBox_Presupuesto") as TextBox;
            textoPresupuesto.Text = Convert.ToDecimal(filaTabla["PRESUPUESTO"]).ToString();

            TextBox textoAsignado = GridView_DetallesPresupuestoGeneral.Rows[i].FindControl("TextBox_AsignadoEmpresa") as TextBox;
            textoAsignado.Text = Convert.ToDecimal(filaTabla["ASIGNADO"]).ToString();

            TextBox textoEjecutado = GridView_DetallesPresupuestoGeneral.Rows[i].FindControl("TextBox_EjecutadoEmpresa") as TextBox;
            textoEjecutado.Text = Convert.ToDecimal(filaTabla["EJECUTADO"]).ToString();

            TextBox textoSaldo = GridView_DetallesPresupuestoGeneral.Rows[i].FindControl("TextBox_SaldoEmpresa") as TextBox;
            textoSaldo.Text = saldo.ToString();

            TextBox textoObservaciones = GridView_DetallesPresupuestoGeneral.Rows[i].FindControl("TextBox_ObservacionesEmpresa") as TextBox;
            textoObservaciones.Text = filaTabla["OBSERVACIONES"].ToString().Trim();

            if (HiddenField_ID_AREA.Value == Programa.Areas.GESTIONHUMANA.ToString())
            {
                if (ID_EMPRESA == _tools.ObtenerIdEmpleadorPorSession(Convert.ToInt32(Session["idEmpresa"])))
                {
                    GridView_DetallesPresupuestoGeneral.Rows[i].Visible = true;
                }
                else
                {
                    GridView_DetallesPresupuestoGeneral.Rows[i].Visible = false;
                }
            }
            else
            {
                GridView_DetallesPresupuestoGeneral.Rows[i].Visible = filaTabla["RAZ_SOCIAL"].ToString().Trim().ToUpper().StartsWith(letraSeleciconada.ToUpper());
            }
        }
    }

    private void ConfigurarBotonesEdicion_GridView_DetallesPresupuestoGeneral(GridView grilla, Boolean bSeleccionar, Boolean bModificar, Boolean bGuardar, Boolean bCancelar)
    {
        grilla.Columns[0].Visible = bSeleccionar;

        grilla.Columns[1].Visible = bModificar;

        grilla.Columns[2].Visible = bGuardar;

        grilla.Columns[3].Visible = bCancelar;
    }

    private void CargarPresupuestoGeneralSeleccionado(Decimal ID_PRES_GEN, Int32 ANIO, Decimal MONTO, Decimal ASIGNADO, Decimal EJECUTADO)
    {
        tools _tools = new tools();

        HiddenField_ID_PRES_GEN_SELECCIONADO.Value = ID_PRES_GEN.ToString();
        HiddenField_ANNO_SELECCIONADO.Value = ANIO.ToString();
        HiddenField_MONTO_PRES_GEN_SELECCIONADO.Value = MONTO.ToString();
        HiddenField_ASIGNADO_PRES_GEN_SELECCIONADO.Value = ASIGNADO.ToString();
        HiddenField_EJECUTADO_PRES_GEN_SELECCIONADO.Value = EJECUTADO.ToString();

        presupuesto _pres = new presupuesto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaHistorial = _pres.ObtenerHistPresupuestoGeneral(ID_PRES_GEN);
        if (tablaHistorial.Rows.Count <= 0)
        {
            if (_pres.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _pres.MensajeError, Proceso.Error);
            }

            Panel_DatosPresupuestoAnioSeleccionado.Visible = false;
        }
        else
        {
            Panel_DatosPresupuestoAnioSeleccionado.Visible = true;

            GridView_HistorialPresupuestoGeneral.DataSource = tablaHistorial;
            GridView_HistorialPresupuestoGeneral.DataBind();
        }

        DataTable tablaPresupuestosEmpresas = null;

        if (HiddenField_ID_AREA.Value != Programa.Areas.GESTIONHUMANA.ToString())
        {
            tablaPresupuestosEmpresas = _pres.ObtenerPresupuestosEmpresasesPorPresGen(ID_PRES_GEN, ANIO, 0);
        }
        else
        {
            Decimal ID_EMPRESA = _tools.ObtenerIdEmpleadorPorSession(Convert.ToInt32(Session["idEmpresa"]));
            tablaPresupuestosEmpresas = _pres.ObtenerPresupuestosEmpresasesPorPresGen(ID_PRES_GEN, ANIO, ID_EMPRESA);
        }

        HiddenField_INDEX_GRID_DET_PRES_GEN.Value = "-1";
        HiddenField_PAGE_GRID_DET_PRES_GEN.Value = "0";

        if (tablaPresupuestosEmpresas.Rows.Count <= 0)
        {
            if (_pres.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _pres.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información de empresa y presupuestos para el año seleccionado.", Proceso.Advertencia);
            }

            GridView_DetallesPresupuestoGeneral.PageIndex = 0;
            GridView_DetallesPresupuestoGeneral.DataSource = null;
            GridView_DetallesPresupuestoGeneral.DataBind();

            Panel_DetallesPresupuestoGeneral.Visible = false;
        }
        else
        {
            Panel_DetallesPresupuestoGeneral.Visible = true;

            Cargar_GridView_DetallesPresupuestoGeneral_DesdeDataTable(tablaPresupuestosEmpresas);

            InhabilitarFilasGrilla(GridView_DetallesPresupuestoGeneral, 4);
            ConfigurarBotonesEdicion_GridView_DetallesPresupuestoGeneral(GridView_DetallesPresupuestoGeneral, true, true, false, false);
        }
    }

    protected void GridView_PresupuestosAnio_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        ConfigurarAreaRseGlobal();

        Decimal ID_PRES_GEN = Convert.ToDecimal(GridView_PresupuestosAnio.DataKeys[indexSeleccionado].Values["ID_PRES_GEN"]);
        Int32 ANIO = Convert.ToInt32(GridView_PresupuestosAnio.DataKeys[indexSeleccionado].Values["ANIO"]);
        Decimal MONTO = Convert.ToDecimal(GridView_PresupuestosAnio.DataKeys[indexSeleccionado].Values["MONTO"]);
        Int32 pagina = GridView_PresupuestosAnio.PageIndex;
        Decimal ASIGNADO = Convert.ToDecimal(GridView_PresupuestosAnio.DataKeys[indexSeleccionado].Values["ASIGNADO"]);
        Decimal EJECUTADO = Convert.ToDecimal(GridView_PresupuestosAnio.DataKeys[indexSeleccionado].Values["EJECUTADO"]);

        if (e.CommandName == "seleccionar")
        {
            if (ID_PRES_GEN <= 0)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El año seleccioando no se ha configurado.", Proceso.Advertencia);
            }
            else
            {
                HiddenField_INDEX_GRIDVIEW_PRES_GEN.Value = indexSeleccionado.ToString();
                HiddenField_PAGE_GRIDVIEW_PRES_GEN.Value = GridView_PresupuestosAnio.PageIndex.ToString();

                Ocultar(Acciones.PresGenSeleccionado);
                Mostrar(Acciones.PresGenSeleccionado);

                HiddenField_LETRA_PAGINACION_EMPRESA.Value = "A";

                CargarPresupuestoGeneralSeleccionado(ID_PRES_GEN, ANIO, MONTO, ASIGNADO, EJECUTADO);

            }
        }
        else
        {
            if (e.CommandName == "modificar")
            {
                HiddenField_FILA_SELECCIONADA_GRILLA_PRES_GEN.Value = indexSeleccionado.ToString();

                HabilitarFila_GridView_PresupuestosAnio(GridView_PresupuestosAnio, indexSeleccionado);

                HiddenField_ACCION_GRILLA_PRES_GEN.Value = AccionesGrilla.Modificar.ToString();
                HiddenField_FILA_SELECCIONADA_GRILLA_PRES_GEN.Value = indexSeleccionado.ToString();

                HiddenField_ID_PRES_GEN.Value = GridView_PresupuestosAnio.DataKeys[indexSeleccionado].Values["ID_PRES_GEN"].ToString();

                TextBox textoMonto = GridView_PresupuestosAnio.Rows[indexSeleccionado].FindControl("TextBox_Monto") as TextBox;
                HiddenField_MONTO_PRES_GEN.Value = Convert.ToDecimal(textoMonto.Text).ToString();

                TextBox textoDescripcion = GridView_PresupuestosAnio.Rows[indexSeleccionado].FindControl("TextBox_Descripcion") as TextBox;
                HiddenField_DESCRIPCION_PRES_GEN.Value = textoDescripcion.Text.Trim();

                ConfigurarBotonesEdicion_GridView_PresupuestosAnio(GridView_PresupuestosAnio, false, false, true, true);
                PermitirOprimirBotonesDeGuardadoYCancelarEnFilaSeleccionada_GridView_PresupuestosAnio(GridView_PresupuestosAnio, indexSeleccionado);

                Ocultar(Acciones.ModificarPreGen);
            }
            else
            {
                if (e.CommandName == "cancelar")
                {
                    TextBox textoMonto = GridView_PresupuestosAnio.Rows[indexSeleccionado].FindControl("TextBox_Monto") as TextBox;
                    textoMonto.Text = Convert.ToDecimal(HiddenField_MONTO_PRES_GEN.Value).ToString();

                    TextBox textoDescripcion = GridView_PresupuestosAnio.Rows[indexSeleccionado].FindControl("TextBox_Descripcion") as TextBox;
                    textoDescripcion.Text = HiddenField_DESCRIPCION_PRES_GEN.Value.Trim();

                    HiddenField_ACCION_GRILLA_PRES_GEN.Value = AccionesGrilla.Ninguna.ToString();

                    InhabilitarFilasGrilla(GridView_PresupuestosAnio, 4);

                    ConfigurarBotonesEdicion_GridView_PresupuestosAnio(GridView_PresupuestosAnio, true, true, false, false);
                }
                else
                {
                    if (e.CommandName == "guardar")
                    {
                        TextBox textoMonto = GridView_PresupuestosAnio.Rows[indexSeleccionado].FindControl("TextBox_Monto") as TextBox;

                        TextBox textoAsignado = GridView_PresupuestosAnio.Rows[indexSeleccionado].FindControl("TextBox_Asignado") as TextBox;

                        TextBox textoEjecutado = GridView_PresupuestosAnio.Rows[indexSeleccionado].FindControl("TextBox_Ejecutado") as TextBox;

                        TextBox textoDescripcion = GridView_PresupuestosAnio.Rows[indexSeleccionado].FindControl("TextBox_Descripcion") as TextBox;

                        Decimal monto = 0;
                        Decimal asignado = Convert.ToDecimal(textoAsignado.Text);
                        Decimal ejecutado = Convert.ToDecimal(textoEjecutado.Text);
                        Boolean correcto = true;
                        
                        try
                        {
                            monto = Convert.ToDecimal(textoMonto.Text);
                        }
                        catch
                        {
                            correcto = false;
                            monto = 0;
                        }

                        if(correcto == false)
                        {
                            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El monto digitado no es un valor correcto.", Proceso.Advertencia);
                        }
                        else
                        {
                            if(monto <= 0)
                            {
                                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El monto digitado debe ser un valor mayor que cero (0).", Proceso.Advertencia);
                                correcto = false;
                            }
                            else
                            {
                                if((asignado + ejecutado) > monto)
                                {
                                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El monto digitado no puede ser menor que el valor ya ejecutado y asignado a actividades.", Proceso.Advertencia);
                                    correcto = false;
                                }
                                else
                                {
                                    if(textoDescripcion.Text.Trim().Length <= 0)
                                    {
                                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Las observaciones o descripción es necesaria ára poder continuar.", Proceso.Advertencia);
                                        correcto = false;
                                    }
                                }
                            }
                        }

                        if (correcto == true)
                        { 
                            if (ID_PRES_GEN <= 0)
                            {
                                presupuesto _pres = new presupuesto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

                                Decimal ID_PRES_GEN_NUEVO = _pres.AdicionarPresupuestoGeneral(ANIO, GLO_AREA_RSE_GLOBAL, monto, textoDescripcion.Text.Trim());

                                if (ID_PRES_GEN_NUEVO <= 0)
                                {
                                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _pres.MensajeError, Proceso.Error);
                                }
                                else
                                {
                                    Ocultar(Acciones.Inicio);
                                    Mostrar(Acciones.Inicio);
                                    Cargar(Acciones.Inicio);

                                    HiddenField_ACCION_GRILLA_PRES_GEN.Value = AccionesGrilla.Ninguna.ToString();

                                    HiddenField_INDEX_GRIDVIEW_PRES_GEN.Value = "-1";
                                    HiddenField_PAGE_GRIDVIEW_PRES_GEN.Value = "0";

                                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El Presupuesto para el año: " + ANIO.ToString() + " fue procesado correctamente.", Proceso.Correcto);
                                }
                            }
                            else
                            { 
                                presupuesto _pres = new presupuesto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

                                Boolean resultado = _pres.ActualizarPresupuestoGeneral(ID_PRES_GEN, monto, textoDescripcion.Text.Trim());

                                if (resultado == false)
                                {
                                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _pres.MensajeError, Proceso.Error);
                                }
                                else
                                {
                                    Ocultar(Acciones.Inicio);
                                    Mostrar(Acciones.Inicio);
                                    Cargar(Acciones.Inicio);

                                    HiddenField_ACCION_GRILLA_PRES_GEN.Value = AccionesGrilla.Ninguna.ToString();

                                    HiddenField_INDEX_GRIDVIEW_PRES_GEN.Value = "-1";
                                    HiddenField_PAGE_GRIDVIEW_PRES_GEN.Value = "0";

                                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El Presupuesto para el año: " + ANIO.ToString() + " fue procesado correctamente.", Proceso.Correcto);
                                }
                            }
                        }
                    }
                }
            }
        }
    }













    private Boolean CargarPresupuestoAsignadoEjecutadoMes(Int32 MES)
    {
        ConfigurarAreaRseGlobal();

        Int32 ANIO = Convert.ToInt32(HiddenField_ANNO_SELECCIONADO.Value);
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA_SELECCIONADA.Value);

        presupuesto _pres = new presupuesto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaAsignadoEjecutadoMesAnio = _pres.ObtenerPresupuestoAsiganadoEjecutadoParaEmpresaAñoMesArea(ANIO, ID_EMPRESA, GLO_AREA_RSE_GLOBAL, MES);

        DataRow filaAsignadoEjecutadoMesAnio;
        Boolean correcto = true;

        Decimal asignado = 0;
        Decimal ejecutado = 0;

        if (tablaAsignadoEjecutadoMesAnio.Rows.Count <= 0)
        {
            if (_pres.MensajeError != null)
            {
                correcto = false;
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _pres.MensajeError, Proceso.Error);
            }

            asignado = 0;
            ejecutado = 0;
        }
        else
        {
            filaAsignadoEjecutadoMesAnio = tablaAsignadoEjecutadoMesAnio.Rows[0];

            asignado = Convert.ToDecimal (filaAsignadoEjecutadoMesAnio["PRESUPUESTO_ASIGNADO"]);
            ejecutado = Convert.ToDecimal(filaAsignadoEjecutadoMesAnio["PRESUPUESTO_EJECUTADO"]);
        }

        switch (MES)
        { 
            case 1:
                TextBox_PresupuestoEneroAsignado.Text = asignado.ToString();
                TextBox_PresupuestoEneroEjecutado.Text = ejecutado.ToString();
                break;
            case 2:
                TextBox_PresupuestoFebreroAsignado.Text = asignado.ToString();
                TextBox_PresupuestoFebreroEjecutado.Text = ejecutado.ToString();
                break;
            case 3:
                TextBox_PresupuestoMarzoAsignado.Text = asignado.ToString();
                TextBox_PresupuestoMarzoEjecutado.Text = ejecutado.ToString();
                break;
            case 4:
                TextBox_PresupuestoAbrilAsignado.Text = asignado.ToString();
                TextBox_PresupuestoAbrilEjecutado.Text = ejecutado.ToString();
                break;
            case 5:
                TextBox_PresupuestoMayoAsignado.Text = asignado.ToString();
                TextBox_PresupuestoMayoEjecutado.Text = ejecutado.ToString();
                break;
            case 6:
                TextBox_PresupuestoJunioAsignado.Text = asignado.ToString();
                TextBox_PresupuestoJunioEjecutado.Text = ejecutado.ToString();
                break;
            case 7:
                TextBox_PresupuestoJulioAsignado.Text = asignado.ToString();
                TextBox_PresupuestoJulioEjecutado.Text = ejecutado.ToString();
                break;
            case 8:
                TextBox_PresupuestoAgostoAsignado.Text = asignado.ToString();
                TextBox_PresupuestoAgostoEjecutado.Text = ejecutado.ToString();
                break;
            case 9:
                TextBox_PresupuestoSeptiembreAsignado.Text = asignado.ToString();
                TextBox_PresupuestoSeptiembreEjecutado.Text = ejecutado.ToString();
                break;
            case 10:
                TextBox_PresupuestoOctubreAsignado.Text = asignado.ToString();
                TextBox_PresupuestoOctubreEjecutado.Text = ejecutado.ToString();
                break;
            case 11:
                TextBox_PresupuestoNoviembreAsignado.Text = asignado.ToString();
                TextBox_PresupuestoNoviembreEjecutado.Text = ejecutado.ToString();
                break;
            case 12:
                TextBox_PresupuestoDicimebreAsignado.Text = asignado.ToString();
                TextBox_PresupuestoDiciembreEjecutado.Text = ejecutado.ToString();
                break;
        }
        return correcto;
    }

    private void CargarPresupuestoEmpresaSeleccionada(Decimal ID_PRESUPUESTO, Decimal ID_EMPRESA)
    { 
        HiddenField_ID_EMPRESA_SELECCIONADA.Value = ID_EMPRESA.ToString();
        HiddenField_ID_PRESUPUESTO_SELECCIONADO.Value = ID_PRESUPUESTO.ToString();

        ConfigurarAreaRseGlobal();

        Int32 ANIO = Convert.ToInt32(HiddenField_ANNO_SELECCIONADO.Value);

        presupuesto _pres = new presupuesto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaHistorial = _pres.ObtenerHistPresupuestoPorIdPresupuesto(ID_PRESUPUESTO);

        if (tablaHistorial.Rows.Count <= 0)
        {
            if (_pres.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _pres.MensajeError, Proceso.Error);
            }

            Panel_HistorialDetallePresupuestoGeneral.Visible = false;

            GridView_HistorialEmpresaSeleccionada.DataSource = null;
            GridView_HistorialEmpresaSeleccionada.DataBind();
        }
        else
        {
            Panel_HistorialDetallePresupuestoGeneral.Visible = true;

            GridView_HistorialEmpresaSeleccionada.DataSource = tablaHistorial;
            GridView_HistorialEmpresaSeleccionada.DataBind();
        }

        Panel_DetallePorMes.Visible = true;

        CargarPresupuestoAsignadoEjecutadoMes(1);

        CargarPresupuestoAsignadoEjecutadoMes(2);

        CargarPresupuestoAsignadoEjecutadoMes(3);

        CargarPresupuestoAsignadoEjecutadoMes(4);

        CargarPresupuestoAsignadoEjecutadoMes(5);

        CargarPresupuestoAsignadoEjecutadoMes(6);

        CargarPresupuestoAsignadoEjecutadoMes(7);

        CargarPresupuestoAsignadoEjecutadoMes(8);

        CargarPresupuestoAsignadoEjecutadoMes(9);

        CargarPresupuestoAsignadoEjecutadoMes(10);

        CargarPresupuestoAsignadoEjecutadoMes(11);

        CargarPresupuestoAsignadoEjecutadoMes(12);
    }

    protected void GridView_DetallesPresupuestoGeneral_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        ConfigurarAreaRseGlobal();

        Decimal ID_PRESUPUESTO = Convert.ToDecimal(GridView_DetallesPresupuestoGeneral.DataKeys[indexSeleccionado].Values["ID_PRESUPUESTO"]);
        Int32 ANIO = Convert.ToInt32(HiddenField_ANNO_SELECCIONADO.Value);
        Decimal ID_EMPRESA = Convert.ToDecimal(GridView_DetallesPresupuestoGeneral.DataKeys[indexSeleccionado].Values["ID_EMPRESA"]);
        Decimal ID_PRES_GENERAL = Convert.ToDecimal(HiddenField_ID_PRES_GEN_SELECCIONADO.Value);
        Decimal MONTO_TOTAL_PRES_GEN = Convert.ToDecimal(HiddenField_MONTO_PRES_GEN_SELECCIONADO.Value);
        Decimal ASIGNADO_TOTAL_PRES_GEN = Convert.ToDecimal(HiddenField_ASIGNADO_PRES_GEN_SELECCIONADO.Value);
        Decimal EJECUTADO_TOTAL_PRES_GEN = Convert.ToDecimal(HiddenField_EJECUTADO_PRES_GEN_SELECCIONADO.Value);

        if (e.CommandName == "seleccionar")
        {
            if (ID_PRESUPUESTO <= 0)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La Empresa seleccionada aún no tiene asignado un presupuesto para el año: " + ANIO.ToString(), Proceso.Advertencia);
            }
            else
            {
                HiddenField_INDEX_GRID_DET_PRES_GEN.Value = indexSeleccionado.ToString();
                HiddenField_PAGE_GRID_DET_PRES_GEN.Value = GridView_DetallesPresupuestoGeneral.PageIndex.ToString();

                Ocultar(Acciones.PresupuestoEmpresaSeleccionado);
                Mostrar(Acciones.PresupuestoEmpresaSeleccionado);

                CargarPresupuestoEmpresaSeleccionada(ID_PRESUPUESTO, ID_EMPRESA);
            }
        }
        else
        {
            if (e.CommandName == "modificar")
            {
                HiddenField_FILA_SELECCIONADA_GRILLA_DET_PRES_GEN.Value = indexSeleccionado.ToString();

                HabilitarFila_GridView_DetallesPresupuestoGeneral(GridView_DetallesPresupuestoGeneral, indexSeleccionado);

                HiddenField_ACCION_GRILLA_DET_PRES_GEN.Value = AccionesGrilla.Modificar.ToString();
                HiddenField_FILA_SELECCIONADA_GRILLA_DET_PRES_GEN.Value = indexSeleccionado.ToString();

                HiddenField_ID_PRESUPUESTO_DET_PRES_GEN.Value = GridView_DetallesPresupuestoGeneral.DataKeys[indexSeleccionado].Values["ID_PRESUPUESTO"].ToString();

                TextBox textoPresupuesto = GridView_DetallesPresupuestoGeneral.Rows[indexSeleccionado].FindControl("TextBox_Presupuesto") as TextBox;
                HiddenField_PRESUPUESTO_DET_PRES_GEN.Value = Convert.ToDecimal(textoPresupuesto.Text).ToString();

                TextBox textoObservaciones = GridView_DetallesPresupuestoGeneral.Rows[indexSeleccionado].FindControl("TextBox_ObservacionesEmpresa") as TextBox;
                HiddenField_OBSERVACIONES_DET_PRES_GEN.Value = textoObservaciones.Text.Trim();

                ConfigurarBotonesEdicion_GridView_DetallesPresupuestoGeneral(GridView_DetallesPresupuestoGeneral, false, false, true, true);
                PermitirOprimirBotonesDeGuardadoYCancelarEnFilaSeleccionada_GridView_DetallesPresupuestoGeneral(GridView_DetallesPresupuestoGeneral, indexSeleccionado);

                Ocultar(Acciones.ModificarDetallePresGen);
            }
            else
            {
                if (e.CommandName == "cancelar")
                {
                    TextBox textoPresupuesto = GridView_DetallesPresupuestoGeneral.Rows[indexSeleccionado].FindControl("TextBox_Presupuesto") as TextBox;
                    textoPresupuesto.Text = Convert.ToDecimal(HiddenField_PRESUPUESTO_DET_PRES_GEN.Value).ToString();

                    TextBox textoObservaciones = GridView_DetallesPresupuestoGeneral.Rows[indexSeleccionado].FindControl("TextBox_ObservacionesEmpresa") as TextBox;
                    textoObservaciones.Text = HiddenField_OBSERVACIONES_DET_PRES_GEN.Value.Trim();

                    HiddenField_ACCION_GRILLA_DET_PRES_GEN.Value = AccionesGrilla.Ninguna.ToString();

                    InhabilitarFilasGrilla(GridView_DetallesPresupuestoGeneral, 4);

                    ConfigurarBotonesEdicion_GridView_DetallesPresupuestoGeneral(GridView_DetallesPresupuestoGeneral, true, true, false, false);
                }
                else
                {
                    if (e.CommandName == "guardar")
                    {
                        TextBox textoPresupuesto = GridView_DetallesPresupuestoGeneral.Rows[indexSeleccionado].FindControl("TextBox_Presupuesto") as TextBox;
                        TextBox textoAsignado = GridView_DetallesPresupuestoGeneral.Rows[indexSeleccionado].FindControl("TextBox_AsignadoEmpresa") as TextBox;
                        TextBox textoEjecutado = GridView_DetallesPresupuestoGeneral.Rows[indexSeleccionado].FindControl("TextBox_EjecutadoEmpresa") as TextBox;

                        TextBox textoObservaciones = GridView_DetallesPresupuestoGeneral.Rows[indexSeleccionado].FindControl("TextBox_ObservacionesEmpresa") as TextBox;

                        Decimal presupuesto = 0;
                        Decimal asignado = Convert.ToDecimal(textoAsignado.Text);
                        Decimal ejecutado = Convert.ToDecimal(textoEjecutado.Text);
                        Boolean correcto = true;

                        try
                        {
                            presupuesto = Convert.ToDecimal(textoPresupuesto.Text);
                        }
                        catch
                        {
                            correcto = false;
                            presupuesto = 0;
                        }

                        if (correcto == false)
                        {
                            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El presupuesto digitado no es un valor correcto.", Proceso.Advertencia);
                        }
                        else
                        {
                            if (presupuesto <= 0)
                            {
                                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El presupuesto digitado debe ser un valor mayor que cero (0).", Proceso.Advertencia);
                                correcto = false;
                            }
                            else
                            {
                                if ((asignado + ejecutado) > presupuesto)
                                {
                                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El presupuesto digitado no puede ser menor que el valor ya asignado actividades.", Proceso.Advertencia);
                                    correcto = false;
                                }
                                else
                                {
                                    if (textoObservaciones.Text.Trim().Length <= 0)
                                    {
                                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Las observaciones o descripción es necesaria para poder continuar.", Proceso.Advertencia);
                                        correcto = false;
                                    }
                                    else
                                    {
                                        if (ID_PRESUPUESTO <= 0)
                                        {

                                            ASIGNADO_TOTAL_PRES_GEN = ASIGNADO_TOTAL_PRES_GEN + presupuesto;
 
                                            if (MONTO_TOTAL_PRES_GEN < (ASIGNADO_TOTAL_PRES_GEN + EJECUTADO_TOTAL_PRES_GEN))
                                            {
                                                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El presupuesto asignado a la empresa, sobrepasa el presupuesto general disponible para asignar.", Proceso.Advertencia);
                                                correcto = false;
                                            }
                                        }
                                        else
                                        { 
                                            ASIGNADO_TOTAL_PRES_GEN = ASIGNADO_TOTAL_PRES_GEN - Convert.ToDecimal(HiddenField_PRESUPUESTO_DET_PRES_GEN.Value);

                                            ASIGNADO_TOTAL_PRES_GEN = ASIGNADO_TOTAL_PRES_GEN + presupuesto;

                                            if (MONTO_TOTAL_PRES_GEN < (ASIGNADO_TOTAL_PRES_GEN + EJECUTADO_TOTAL_PRES_GEN))
                                            {
                                                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El presupuesto asignado a la empresa, sobrepasa el presupuesto general disponible para asiganar.", Proceso.Advertencia);
                                                correcto = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (correcto == true)
                        {
                            if (ID_PRESUPUESTO <= 0)
                            {
                                presupuesto _pres = new presupuesto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

                                Decimal ID_PRESUPUESTO_NUEVO = _pres.AdicionarPresupuestoEmpresa(ID_EMPRESA, ANIO, presupuesto, textoObservaciones.Text.Trim(), GLO_AREA_RSE_GLOBAL, ID_PRES_GENERAL);

                                if (ID_PRESUPUESTO_NUEVO <= 0)
                                {
                                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _pres.MensajeError, Proceso.Error);
                                }
                                else
                                {
                                    Cargar_GridView_PresupuestosAnio();
                                    InhabilitarFilasGrilla(GridView_PresupuestosAnio, 4);
                                    ConfigurarBotonesEdicion_GridView_PresupuestosAnio(GridView_PresupuestosAnio, true, true, false, false);

                                    CargarPresupuestoGeneralSeleccionado(ID_PRES_GENERAL, ANIO, MONTO_TOTAL_PRES_GEN, ASIGNADO_TOTAL_PRES_GEN, EJECUTADO_TOTAL_PRES_GEN);

                                    HiddenField_ACCION_GRILLA_DET_PRES_GEN.Value = AccionesGrilla.Ninguna.ToString();

                                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El Presupuesto para el año: " + ANIO.ToString() + " fue procesado correctamente.", Proceso.Correcto);
                                }
                            }
                            else
                            {
                                presupuesto _pres = new presupuesto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

                                Boolean resultado = _pres.ActualizarPresupuesto(ID_PRESUPUESTO, presupuesto, textoObservaciones.Text.Trim());

                                if (resultado == false)
                                {
                                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _pres.MensajeError, Proceso.Error);
                                }
                                else
                                {
                                    Cargar_GridView_PresupuestosAnio();
                                    InhabilitarFilasGrilla(GridView_PresupuestosAnio, 4);
                                    ConfigurarBotonesEdicion_GridView_PresupuestosAnio(GridView_PresupuestosAnio, true, true, false, false);

                                    CargarPresupuestoGeneralSeleccionado(ID_PRES_GENERAL, ANIO, MONTO_TOTAL_PRES_GEN, ASIGNADO_TOTAL_PRES_GEN, EJECUTADO_TOTAL_PRES_GEN);

                                    HiddenField_ACCION_GRILLA_DET_PRES_GEN.Value = AccionesGrilla.Ninguna.ToString();

                                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El Presupuesto para el año: " + ANIO.ToString() + " fue procesado correctamente.", Proceso.Correcto);
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    protected void GridView_PresupuestosAnio_PreRender(object sender, EventArgs e)
    {
        Int32 indexSeleccionado = Convert.ToInt32(HiddenField_INDEX_GRIDVIEW_PRES_GEN.Value);
        GridView_PresupuestosAnio.SelectedIndex = indexSeleccionado;

        for (int i = 0; i < GridView_PresupuestosAnio.Rows.Count; i++)
        {
            if (i == indexSeleccionado)
            {
                GridView_PresupuestosAnio.Rows[i].BackColor = colorSeleccionado;
            }
            else
            {
                GridView_PresupuestosAnio.Rows[i].BackColor = System.Drawing.Color.Transparent;
            }
        }
    }

    protected void GridView_DetallesPresupuestoGeneral_PreRender(object sender, EventArgs e)
    {
        Int32 indexSeleccionado = Convert.ToInt32(HiddenField_INDEX_GRID_DET_PRES_GEN.Value);
        GridView_DetallesPresupuestoGeneral.SelectedIndex = indexSeleccionado;

        for (int i = 0; i < GridView_DetallesPresupuestoGeneral.Rows.Count; i++)
        {
            if (i == indexSeleccionado)
            {
                GridView_DetallesPresupuestoGeneral.Rows[i].BackColor = colorSeleccionado;
            }
            else
            {
                GridView_DetallesPresupuestoGeneral.Rows[i].BackColor = System.Drawing.Color.Transparent;
            }
        }
    }

    protected void GridView_HistorialPresupuestoGeneral_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_HistorialPresupuestoGeneral.PageIndex = e.NewPageIndex;
        
        Decimal ID_PRES_GEN = Convert.ToDecimal(HiddenField_ID_PRES_GEN_SELECCIONADO.Value);
   
        presupuesto _pres = new presupuesto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaHistorial = _pres.ObtenerHistPresupuestoGeneral(ID_PRES_GEN);
        
        if (tablaHistorial.Rows.Count <= 0)
        {
            if (_pres.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _pres.MensajeError, Proceso.Error);
            }

            Panel_DatosPresupuestoAnioSeleccionado.Visible = false;
        }
        else
        {
            Panel_DatosPresupuestoAnioSeleccionado.Visible = true;

            GridView_HistorialPresupuestoGeneral.DataSource = tablaHistorial;
            GridView_HistorialPresupuestoGeneral.DataBind();
        }
    }

    private void CargarDetalleActividadesPeriodoDeterminado(Int32 MES)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA_SELECCIONADA.Value);
        Int32 ANIO = Convert.ToInt32(HiddenField_ANNO_SELECCIONADO.Value);

        ConfigurarAreaRseGlobal();

        Programa _prog = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaActividades = _prog.ObtenerActividadesProgramadasParaUnMesAñoEmpresaYAreaEspecificos(ID_EMPRESA, ANIO, MES, GLO_AREA_RSE_GLOBAL);

        if (tablaActividades.Rows.Count <= 0)
        {
            if (_prog.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _prog.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros de Atividades programadas para el mes seleccionado.", Proceso.Advertencia);
            }

            Panel_GrillaDetallesActividadesDelMes.Visible = false;
            GridView_DetallesActividadesDelMes.DataSource = null;
            GridView_DetallesActividadesDelMes.DataBind();
        }
        else
        {
            Panel_GrillaDetallesActividadesDelMes.Visible = true;
            GridView_DetallesActividadesDelMes.DataSource = tablaActividades;
            GridView_DetallesActividadesDelMes.DataBind();
        }
    }

    protected void ImageButton_DetallesEnero_Click(object sender, ImageClickEventArgs e)
    {
        CargarDetalleActividadesPeriodoDeterminado(1);
    }

    protected void ImageButton_DetallesFebrero_Click(object sender, ImageClickEventArgs e)
    {
        CargarDetalleActividadesPeriodoDeterminado(2);
    }

    protected void ImageButton_DetallesMarzo_Click(object sender, ImageClickEventArgs e)
    {
        CargarDetalleActividadesPeriodoDeterminado(3);
    }

    protected void ImageButton_DetallesAbril_Click(object sender, ImageClickEventArgs e)
    {
        CargarDetalleActividadesPeriodoDeterminado(4);
    }

    protected void ImageButton_DetallesMayo_Click(object sender, ImageClickEventArgs e)
    {
        CargarDetalleActividadesPeriodoDeterminado(5);
    }

    protected void ImageButton_DetallesJunio_Click(object sender, ImageClickEventArgs e)
    {
        CargarDetalleActividadesPeriodoDeterminado(6);
    }

    protected void ImageButton_DetallesJulio_Click(object sender, ImageClickEventArgs e)
    {
        CargarDetalleActividadesPeriodoDeterminado(7);
    }

    protected void ImageButton_DetallesAgosto_Click(object sender, ImageClickEventArgs e)
    {
        CargarDetalleActividadesPeriodoDeterminado(8);
    }

    protected void ImageButton_DetallesSeptiembre_Click(object sender, ImageClickEventArgs e)
    {
        CargarDetalleActividadesPeriodoDeterminado(9);
    }

    protected void ImageButton_DetalleOctubre_Click(object sender, ImageClickEventArgs e)
    {
        CargarDetalleActividadesPeriodoDeterminado(10);
    }

    protected void ImageButton_DetallesNoviembre_Click(object sender, ImageClickEventArgs e)
    {
        CargarDetalleActividadesPeriodoDeterminado(11);
    }

    protected void ImageButton_DetallesDiciembre_Click(object sender, ImageClickEventArgs e)
    {
        CargarDetalleActividadesPeriodoDeterminado(12);
    }

    private void CargarGrillaConLetraSeleccionada()
    {
        {
            GridView_DetallesPresupuestoGeneral.SelectedIndex = -1;
            String letraSeleciconada = "A";
            if (String.IsNullOrEmpty(HiddenField_LETRA_PAGINACION_EMPRESA.Value) == false)
            {
                letraSeleciconada = HiddenField_LETRA_PAGINACION_EMPRESA.Value;
            }

            String inicialRazSocial = String.Empty;

            foreach (GridViewRow filaGrilla in GridView_DetallesPresupuestoGeneral.Rows)
            {
                filaGrilla.Visible = filaGrilla.Cells[5].Text.Trim().ToUpper().StartsWith(letraSeleciconada.ToUpper());
            }

            Panel_HistorialDetallePresupuestoGeneral.Visible = false;
            Panel_DetallePorMes.Visible = false;
            Panel_GrillaDetallesActividadesDelMes.Visible = false;
        }
    }

    protected void LinkButton_a_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_EMPRESA.Value = "A";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_z_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_EMPRESA.Value = "Z";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_y_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_EMPRESA.Value = "Y";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_x_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_EMPRESA.Value = "X";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_w_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_EMPRESA.Value = "W";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_v_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_EMPRESA.Value = "V";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_u_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_EMPRESA.Value = "U";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_t_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_EMPRESA.Value = "T";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_s_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_EMPRESA.Value = "S";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_r_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_EMPRESA.Value = "R";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_q_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_EMPRESA.Value = "Q";
        CargarGrillaConLetraSeleccionada();
    }

    protected void LinkButton_p_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_EMPRESA.Value = "P";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_o_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_EMPRESA.Value = "O";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_n_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_EMPRESA.Value = "N";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_m_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_EMPRESA.Value = "M";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_l_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_EMPRESA.Value = "L";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_k_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_EMPRESA.Value = "K";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_j_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_EMPRESA.Value = "J";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_i_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_EMPRESA.Value = "I";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_h_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_EMPRESA.Value = "H";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_g_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_EMPRESA.Value = "G";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_f_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_EMPRESA.Value = "F";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_e_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_EMPRESA.Value = "E";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton3_d_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_EMPRESA.Value = "D";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_c_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_EMPRESA.Value = "C";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_b_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_EMPRESA.Value = "B";
        CargarGrillaConLetraSeleccionada();
    }
}