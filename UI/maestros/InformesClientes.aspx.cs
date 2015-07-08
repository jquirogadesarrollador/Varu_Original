using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brainsbits.LLB.maestras;
using TSHAK.Components;
using Brainsbits.LLB;
using Brainsbits.LLB.programasRseGlobal;
using Brainsbits.LLB.comercial;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using Brainsbits.LLB.seguridad;

public partial class _InformeClientes : System.Web.UI.Page
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
        EmpresaSeleccionada
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }

    ReportDocument reporte;

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
                Panel_FORM_BOTONES.Visible = false;
                Panel_ResultadosBusquedaEmpresas.Visible = false;
                Panel_EmpresaSeleccionada.Visible = false;
                Panel_RESULTADOS_GRID.Visible = false;

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
            case Acciones.EmpresaSeleccionada:
                Panel_FORM_BOTONES.Visible = true;
                Panel_EmpresaSeleccionada.Visible = true;
                Panel_RESULTADOS_GRID.Visible = true;
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
        item = new ListItem("Código de Cliente", "COD_EMPRESA");
        DropDownList_BUSCAR.Items.Add(item);
        item = new ListItem("Razón social", "RAZ_SOCIAL");
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
                ConfigurarAreaRseGlobal();

                configurarCaracteresAceptadosBusqueda(false, false);

                iniciar_seccion_de_busqueda();
                break;
        }
    }

    private void Iniciar()
    {
        Configurar();
        Ocultar(Acciones.Inicio);
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

    protected void DropDownList_BUSCAR_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_BUSCAR.SelectedValue == "")
        {
            configurarCaracteresAceptadosBusqueda(false, false);
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "COD_EMPRESA")
            {
                configurarCaracteresAceptadosBusqueda(false, true);
            }
            else
            {
                if (DropDownList_BUSCAR.SelectedValue == "RAZ_SOCIAL")
                {
                    configurarCaracteresAceptadosBusqueda(true, false);
                }
            }

            TextBox_BUSCAR.Focus();

        }

        TextBox_BUSCAR.Text = "";
    }

    private void Buscar()
    {
        String datosCapturados = HiddenField_DATO_BUSQUEDA.Value;
        String campo = HiddenField_DROP_BUSQUEDA.Value;

        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaResultadosBusqueda = new DataTable();

        if (DropDownList_BUSCAR.SelectedValue == "RAZ_SOCIAL")
        {
            tablaResultadosBusqueda = _cliente.ObtenerEmpresaConRazSocial(datosCapturados);
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "COD_EMPRESA")
            {
                tablaResultadosBusqueda = _cliente.ObtenerEmpresaConCodEmpresa(datosCapturados);
            }
        }

        if (tablaResultadosBusqueda.Rows.Count <= 0)
        {
            if (_cliente.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _cliente.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros que cumplieran los datos de busqueda.", Proceso.Advertencia);
            }

            Panel_ResultadosBusquedaEmpresas.Visible = false;
        }
        else
        {
            Panel_ResultadosBusquedaEmpresas.Visible = true;
            GridView_EmpresasEncontradas.DataSource = tablaResultadosBusqueda;
            GridView_EmpresasEncontradas.DataBind();
        }
    }

    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);

        String datosCapturados = TextBox_BUSCAR.Text.ToUpper();
        String campo = DropDownList_BUSCAR.SelectedValue.ToString();

        HiddenField_DATO_BUSQUEDA.Value = datosCapturados;
        HiddenField_DROP_BUSQUEDA.Value = campo;

        Buscar();
    }
    protected void GridView_EmpresasEncontradas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_EmpresasEncontradas.PageIndex = e.NewPageIndex;

        Buscar();
    }

    private void CargarDatosEmpresaSeleccionada(Decimal ID_EMPRESA)
    {
        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCliente = _cliente.ObtenerEmpresaConIdEmpresa(ID_EMPRESA);

        DataRow filaCliente = tablaCliente.Rows[0];

        Label_RazonSocial.Text = filaCliente["RAZ_SOCIAL"].ToString().Trim();
        Label_NitEmpresa.Text = filaCliente["NIT_EMPRESA"].ToString().Trim() + "-" + filaCliente["DIG_VER"].ToString().Trim();
    }

    private void CargarEmpresaSeleccionada(Decimal ID_EMPRESA, Programa.Areas AREA)
    {
        HiddenField_ID_EMPRESA.Value = ID_EMPRESA.ToString();

        CargarDatosEmpresaSeleccionada(ID_EMPRESA);

    }

    private void Cargar_GridView_RESULTADOS_BUSQUEDA_DesdeTabla(DataTable tablaProgramas)
    {
        GridView_RESULTADOS_BUSQUEDA.DataSource = tablaProgramas;
        GridView_RESULTADOS_BUSQUEDA.DataBind();

        for (int i = 0; i < GridView_RESULTADOS_BUSQUEDA.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_RESULTADOS_BUSQUEDA.Rows[i];

            TextBox textoFechaInicial = filaGrilla.FindControl("TextBox_FechaInicial") as TextBox;
            textoFechaInicial.Text = "";

            TextBox textoFechaFinal = filaGrilla.FindControl("TextBox_FechaFinal") as TextBox;
            textoFechaFinal.Text = "";

            TextBox textoConclusiones = filaGrilla.FindControl("TextBox_Conclusiones") as TextBox;
            textoConclusiones.Text = "";
            textoConclusiones.Enabled = false;
        }

    }

    private void CargarGrillaDeProgramasParaEmpresa(Decimal ID_EMPRESA, Programa.Areas AREA)
    {
        Programa _prog = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaProgramas = _prog.ObtenerHistorialProgramasEmpresaYArea(AREA, ID_EMPRESA);

        if (tablaProgramas.Rows.Count <= 0)
        {
            if (_prog.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _prog.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La empresa seleciconada no tiene Programas asignados, no se puede mostrar información.", Proceso.Advertencia);
            }

            Panel_RESULTADOS_GRID.Visible = false;

            GridView_RESULTADOS_BUSQUEDA.DataSource = null;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
        }
        else
        { 
            Panel_RESULTADOS_GRID.Visible = true;

            Cargar_GridView_RESULTADOS_BUSQUEDA_DesdeTabla(tablaProgramas);
        }
    }

    protected void GridView_EmpresasEncontradas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal ID_EMPRESA = Convert.ToDecimal(GridView_EmpresasEncontradas.DataKeys[indexSeleccionado].Values["ID"]);
            Programa.Areas AREA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);

            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.EmpresaSeleccionada);

            CargarEmpresaSeleccionada(ID_EMPRESA, AREA);

            CargarGrillaDeProgramasParaEmpresa(ID_EMPRESA, AREA);
        }
    }

    public byte[] ConversionImagen(string nombrearchivo)
    {
        FileStream fs = new FileStream(nombrearchivo, FileMode.Open);

        BinaryReader br = new BinaryReader(fs);
        byte[] imagen = new byte[(int)fs.Length];
        br.Read(imagen, 0, (int)fs.Length);
        br.Close();
        fs.Close();
        return imagen;
    }

    private byte[] ImprimirPdfInforme(DataTable tablaInformeOriginal)
    {
        foreach (DataRow filaInforme in tablaInformeOriginal.Rows)
        {
            if(DBNull.Value.Equals(filaInforme["DIR_IMAGEN_PROG_GEN"]) == false)
            {
                if (File.Exists(Server.MapPath(filaInforme["DIR_IMAGEN_PROG_GEN"].ToString().Trim())) == true)
                {
                    filaInforme["IMAGEN_PROG_GEN"] = ConversionImagen(Server.MapPath(filaInforme["DIR_IMAGEN_PROG_GEN"].ToString().Trim()));
                }
                else
                { 
                    filaInforme["IMAGEN_PROG_GEN"] = ConversionImagen(Server.MapPath("~/imagenes/imgRepresentativasActividad/img_no_disponible.png"));
                }
            }
            else
            {

            }
            
            if (DBNull.Value.Equals(filaInforme["DIR_IMAGEN_REPRESENTATIVA"]) == false)
            {
                if (File.Exists(Server.MapPath(filaInforme["DIR_IMAGEN_REPRESENTATIVA"].ToString().Trim())) == true)
                {
                    filaInforme["IMAGEN_ACTIVIDAD"] = ConversionImagen(Server.MapPath(filaInforme["DIR_IMAGEN_REPRESENTATIVA"].ToString().Trim()));
                }
                else
                {
                    filaInforme["IMAGEN_ACTIVIDAD"] = ConversionImagen(Server.MapPath("~/imagenes/imgRepresentativasActividad/img_no_disponible.png"));
                }
            }
            else
            {
                filaInforme["IMAGEN_ACTIVIDAD"] = ConversionImagen(Server.MapPath("~/imagenes/imgRepresentativasActividad/img_no_disponible.png"));
            }

            tablaInformeOriginal.AcceptChanges();
        }

        try
        {
            reporte = new ReportDocument();

            reporte.Load(Server.MapPath("~/Reportes/ProgramasComplementarios/RPT_PROGRAMAS_PDF_INFORME_CLIENTES.rpt"));
            reporte.SetDataSource(tablaInformeOriginal);

            using (var mStream = (MemoryStream)reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat))
            {
                return mStream.ToArray();
            }
        }
        catch (Exception ex)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, ex.Message, Proceso.Error);
            return null;
        }
    }

    protected void GridView_RESULTADOS_BUSQUEDA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "imprimir")
        {
            Boolean correcto = true;
            DateTime fechaInicial = new DateTime();
            DateTime fechaFinal = new DateTime();

            GridViewRow filaGrilla = GridView_RESULTADOS_BUSQUEDA.Rows[indexSeleccionado];

            TextBox textoFechaInicial = filaGrilla.FindControl("TextBox_FechaInicial") as TextBox;
            try
            {
                fechaInicial = Convert.ToDateTime(textoFechaInicial.Text);
            }
            catch
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La fecha inicial esta vacio o no tiene el formato correcto (dd/mm/yyyy).", Proceso.Advertencia);
                fechaInicial = new DateTime();
                correcto = false;
            }

            TextBox textoFechaFinal = filaGrilla.FindControl("TextBox_FechaFinal") as TextBox;
            try
            {
                fechaFinal = Convert.ToDateTime(textoFechaFinal.Text);
            }
            catch
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La fecha final esta vacio o no tiene el formato correcto (dd/mm/yyyy).", Proceso.Advertencia);
                fechaFinal = new DateTime();
                correcto = false;
            }

            TextBox textoConclusiones = filaGrilla.FindControl("TextBox_Conclusiones") as TextBox;
            if (String.IsNullOrEmpty(textoConclusiones.Text.Trim()) == true)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "para poder continuar debe digitar las conclusiones.", Proceso.Advertencia);
                correcto = false;
            }

            if(correcto == true)
            {
                Programa _prog = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

                Decimal ID_PROGRAMA = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_PROGRAMA"]);

                Decimal ID_CORTE = _prog.AdicionarCorteInformeCliente(ID_PROGRAMA, fechaInicial, fechaFinal, textoConclusiones.Text.Trim());
                if (ID_CORTE <= 0)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _prog.MensajeError, Proceso.Error);
                }
                else
                { 
                    DataTable tablaPrograma = _prog.ObtenerListaActividadesParaPDFInformeClientes(ID_PROGRAMA, ID_CORTE, fechaInicial, fechaFinal);
                    if (tablaPrograma.Rows.Count <= 0)
                    {
                        if (_prog.MensajeError != null)
                        {
                            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _prog.MensajeError, Proceso.Error);
                        }
                        else
                        {
                            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "En este momento no se encontraron actividades ejecutadas para el año, cliente Y periodo seleccionado.", Proceso.Advertencia);
                        }
                    }
                    else
                    {
                        byte[] archivo = ImprimirPdfInforme(tablaPrograma);

                        Response.AddHeader("Content-Disposition", "attachment;FileName=informe_programa_clinete_" + ID_PROGRAMA.ToString() + ".pdf");
                        Response.Clear();
                        Response.Buffer = true;
                        Response.ContentType = "application/pdf";
                        Response.BinaryWrite(archivo);
                        Response.End();
                    }
                }
            }
        }
    }
    protected void TextBox_Fechas_TextChanged(object sender, EventArgs e)
    {
        DateTime fechaInicial = new DateTime();
        DateTime fechaFinal = new DateTime();
        Boolean correcto = true;

        GridViewRow filaGrilla = ((GridViewRow)((Control)sender).Parent.Parent); 
        Int32 indexSeleccionado = filaGrilla.RowIndex;

        Decimal ID_PROGRAMA = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_PROGRAMA"]);

        TextBox textoFechaInicial = filaGrilla.FindControl("TextBox_FechaInicial") as TextBox;
        try
        {
            fechaInicial = Convert.ToDateTime(textoFechaInicial.Text);
        }
        catch
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La fecha inicial esta vacio o no tiene el formato correcto (dd/mm/yyyy).", Proceso.Advertencia);
            fechaInicial = new DateTime();
            correcto = false;
        }

        TextBox textoFechaFinal = filaGrilla.FindControl("TextBox_FechaFinal") as TextBox;
        try
        {
            fechaFinal = Convert.ToDateTime(textoFechaFinal.Text);
        }
        catch
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La fecha final esta vacio o no tiene el formato correcto (dd/mm/yyyy).", Proceso.Advertencia);
            fechaFinal = new DateTime();
            correcto = false;
        }

        TextBox textoConclusiones = filaGrilla.FindControl("TextBox_Conclusiones") as TextBox;

        if (correcto == true)
        {
            if (fechaInicial <= fechaFinal)
            {
                Programa _prog = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

                DataTable tablaPeriodo = _prog.ObtenerCorteinformeClientePorIdProgramaYPeriodo(ID_PROGRAMA, fechaInicial, fechaFinal);

                if (tablaPeriodo.Rows.Count <= 0)
                {
                    if (_prog.MensajeError != null)
                    {
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _prog.MensajeError, Proceso.Error);
                    }

                    textoConclusiones.Text = "";
                    textoConclusiones.Enabled = true;
                }
                else
                { 
                    DataRow filaPeriodo = tablaPeriodo.Rows[0];

                    textoConclusiones.Text = filaPeriodo["CONCLUSIONES"].ToString().Trim();
                    textoConclusiones.Enabled = true;
                }
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La fecha inical debe ser menor o igual que la fecha final.", Proceso.Advertencia);
                textoConclusiones.Text = "";
                textoConclusiones.Enabled = false;
            }
        }
        else
        { 
            textoConclusiones.Text = "";
            textoConclusiones.Enabled = false;
        }
    }
}