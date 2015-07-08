using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brainsbits.LLB.seleccion;
using System.Data;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using System.Text;
using TSHAK.Components;
using Brainsbits.LLB.seguridad;

public partial class _BaseDatos : System.Web.UI.Page
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
        Inicio = 0
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
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
                Panel_SeleccionChecks.Visible = false;

                Panel_SeleccionEstadoAspirante.Visible = false;

                Panel_FORM_BOTONES.Visible = false;
                Button_Buscar.Visible = false;
                Button_ExportarExcel.Visible = false;

                Panel_CargoAspira.Visible = false;
                Panel_AreaInteres.Visible = false;
                Panel_Experiencia.Visible = false;
                Panel_AspiracionSalarial.Visible = false;
                Panel_NivelEscolaridad.Visible = false;
                Panel_Profesion.Visible = false;
                Panel_Ciudad.Visible = false;
                Panel_Barrio.Visible = false;
                Panel_FechaActualziacion.Visible = false;
                Panel_NombresApellidos.Visible = false;
                Panel_Edad.Visible = false;
                Panel_PalabraClave.Visible = false;

                Panel_GrillaReporte.Visible = false;

                Panel_FORM_BOTONES_PIE.Visible = false;
                Button_Buscar_1.Visible = false;
                Button_ExportarExcel_1.Visible = false;
                break;
        }
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_SeleccionChecks.Visible = true;

                Panel_SeleccionEstadoAspirante.Visible = true;

                Panel_FORM_BOTONES.Visible = true;
                break;
        }
    }
    private void Limpiar(Acciones accion)
    {
        switch (accion)
        { 
            case Acciones.Inicio:
                RadioButtonList_TipoReporte.SelectedIndex = -1;

                CheckBox_CargoAspira.Checked = false;
                CheckBox_AreaInteres.Checked = false;
                CheckBox_Experiencia.Checked = false;
                CheckBox_AspiracionSalarial.Checked = false;
                CheckBox_NivelEscolaridad.Checked = false;
                CheckBox_Profesión.Checked = false;
                CheckBox_Ciudad.Checked = false;
                CheckBox_Barrio.Checked = false;
                CheckBox_FechaActualizacion.Checked = false;
                CheckBox_NombresApellidos.Checked = false;
                CheckBox_Edad.Checked = false;
                CheckBox_PalabraClave.Checked = false;
                break;
        }
    }

    private void Iniciar()
    {
        Configurar();
        Ocultar(Acciones.Inicio); 
        Mostrar(Acciones.Inicio);
        Limpiar(Acciones.Inicio);
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

    protected void Button_CERRAR_MENSAJE_Click(object sender, System.EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    private Boolean IsValorMoneda(String valorString)
    {
        Decimal valorDecimal = 0;
        Boolean resultado = true;

        try
        {
            valorDecimal = Convert.ToDecimal(valorString);
        }
        catch
        {
            resultado = false;
        }

        return resultado;
    }

    private Boolean IsDateTime(String fechaString)
    {
        DateTime fechaDateTime;
        Boolean resultado = true;

        try
        {
            fechaDateTime = Convert.ToDateTime(fechaString);
        }
        catch
        {
            resultado = false;
        }

        return resultado;
    }

    private Boolean IsInt32(String numeroString)
    {
        Int32 numeroInt32;
        Boolean resultado = true;

        try
        {
            numeroInt32 = Convert.ToInt32(numeroString);
        }
        catch
        {
            resultado = false;
        }

        return resultado;
    }

    private Boolean IsDatosCorrectos()
    {
        Boolean correcto = true;

        if (CheckBox_AspiracionSalarial.Checked == true)
        {
            if ((IsValorMoneda(TextBox_AspiracionSalarialDesde.Text) == false) || (IsValorMoneda(TextBox_AspiracionSalarialHasta.Text) == false))
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Los valores de la ASPIRACIÓN SALARIAL no son vorrectos (PUNTO '.' para separación de miles y COMA ',' para separación de decimales).", Proceso.Advertencia);
                correcto = false;
            }
            else
            {

                Decimal aspiracionDesde = Convert.ToDecimal(TextBox_AspiracionSalarialDesde.Text);
                Decimal aspiracionHasta = Convert.ToDecimal(TextBox_AspiracionSalarialHasta.Text);

                if (aspiracionHasta < aspiracionDesde)
                {
                    correcto = false;
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El valor de la ASPIRACIÓN SALARIAL (HASTA) no puede ser menor que ASPIRACIÓN SALARIAL (DESDE).", Proceso.Advertencia);
                }
            }
        }

        if (correcto == true)
        {
            if (CheckBox_FechaActualizacion.Checked == true)
            {
                if ((IsDateTime(TextBox_FechaActualizacionDesde.Text) == false) || (IsDateTime(TextBox_FechaActualizacionHasta.Text) == false))
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Los valores digitados en FECHA DE ACTUALIZACIÓN no corresponden a fechas correctas (Formato: dd/mm/aaaa).", Proceso.Advertencia);
                    correcto = false;
                }
                else
                {
                    DateTime fechaDesde = Convert.ToDateTime(TextBox_FechaActualizacionDesde.Text);
                    DateTime fechaHasta = Convert.ToDateTime(TextBox_FechaActualizacionHasta.Text);

                    if (fechaHasta < fechaDesde)
                    {
                        correcto = false;
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El valor de la FECHA DE ACTUALIZACIÓN (HASTA) no puede ser menor que FECHA DE ACTUALZIACION (DESDE).", Proceso.Advertencia);
                    }
                }
            }
        }

        if (correcto == true)
        {
            if (CheckBox_Edad.Checked == true)
            {
                if ((IsInt32(TextBox_EdadDesde.Text) == false) || (IsInt32(TextBox_EdadHasta.Text) == false))
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Los valores digitados en EDAD no corresponden a numeros enteros.", Proceso.Advertencia);
                    correcto = false;
                }
                else
                {
                    Int32 edadDesde = Convert.ToInt32(TextBox_EdadDesde.Text);
                    Int32 edadHasta = Convert.ToInt32(TextBox_EdadHasta.Text);

                    if (edadHasta < edadDesde)
                    {
                        correcto = false;
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El valor de la EDAD (HASTA) no puede ser menor que EDAD (DESDE).", Proceso.Advertencia);
                    }
                }
            }
        }

        return correcto;
    }

    private void Cargar_GrillaReporte_DesdeTabla(DataTable tablaReporte)
    {
        GridView_Reporte.DataSource = tablaReporte;
        GridView_Reporte.DataBind();

        for (int i = 0; i < GridView_Reporte.Rows.Count; i++)
        {
            DataRow filaTabla = tablaReporte.Rows[(GridView_Reporte.PageIndex * GridView_Reporte.PageSize) + i];

            GridViewRow filaGrilla = GridView_Reporte.Rows[i];

            HyperLink link = filaGrilla.FindControl("HyperLink_HV") as HyperLink;

            tools _tools = new tools();
            SecureQueryString QueryStringSeguro;
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

            QueryStringSeguro["img_area"] = "contratacion";
            QueryStringSeguro["nombre_area"] = "CONTRATOS Y RELACIONES LABORALES";
            QueryStringSeguro["nombre_modulo"] = "HOJA DE VIDA DEL COLABORADOR";
            QueryStringSeguro["id_solicitud"] = filaTabla["ID_SOLICITUD"].ToString();

            link.NavigateUrl = "~/contratacion/hojaVida.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.Enabled = true;
            link.Target = "_blank";
        }
    }

    private void GenerarReporte()
    {
        tools _tools = new tools();

        String ID_OCUPACION = null;
        if (CheckBox_CargoAspira.Checked == true)
        {
            ID_OCUPACION = DropDownList_CargoAspira.SelectedValue;
        }

        String NIV_EDUCACION = null;
        if (CheckBox_NivelEscolaridad.Checked == true)
        {
            NIV_EDUCACION = DropDownList_NivelEsolaridad.SelectedValue;
        }

        String PROFESION = null;
        if (CheckBox_Profesión.Checked == true)
        {
            PROFESION = DropDownList_Profesion.SelectedValue;
        }

        String AREA_INTERES_LABORAL = null;
        if (CheckBox_AreaInteres.Checked == true)
        {
            AREA_INTERES_LABORAL = DropDownList_AreaInteres.SelectedValue;
        }

        String EXPERIENCIA = null;
        if (CheckBox_Experiencia.Checked == true)
        {
            EXPERIENCIA = DropDownList_Experiencia.SelectedValue;
        }

        String EDAD_DESDE = null;
        String EDAD_HASTA = null;
        if (CheckBox_Edad.Checked == true)
        {
            EDAD_DESDE = TextBox_EdadDesde.Text;
            EDAD_HASTA = TextBox_EdadHasta.Text;
        }

        String ID_CIUDAD = null;
        if (CheckBox_Ciudad.Checked == true)
        {
            ID_CIUDAD = DropDownList_Ciudad.SelectedValue;
        }

        String NOMBRES = null;
        String APELLIDOS = null;
        if (CheckBox_NombresApellidos.Checked == true)
        {
            NOMBRES = TextBox_Nombres.Text;
            APELLIDOS = TextBox_Apellidos.Text;
        }

        String BARRIO = null;
        if (CheckBox_Barrio.Checked == true)
        {
            BARRIO = TextBox_Barrio.Text;
        }

        String ASPIRACION_DESDE = null;
        String ASPIRACION_HASTA = null;
        if (CheckBox_AspiracionSalarial.Checked == true)
        { 
            ASPIRACION_DESDE = TextBox_AspiracionSalarialDesde.Text.Replace(".","").Replace(",",".");
            ASPIRACION_HASTA = TextBox_AspiracionSalarialHasta.Text.Replace(".", "").Replace(",", ".");
        }

        String FECHA_ACTUALIZACION_DESDE = null;
        String FECHA_ACTUALIZACION_HASTA = null;
        if (CheckBox_FechaActualizacion.Checked == true)
        {
            FECHA_ACTUALIZACION_DESDE = _tools.obtenerStringConFormatoFechaSQLServer(Convert.ToDateTime(TextBox_FechaActualizacionDesde.Text));
            FECHA_ACTUALIZACION_HASTA = _tools.obtenerStringConFormatoFechaSQLServer(Convert.ToDateTime(TextBox_FechaActualizacionHasta.Text));
        }

        String PALABRA_CLAVE = null;
        if (CheckBox_PalabraClave.Checked == true)
        {
            PALABRA_CLAVE = TextBox_PalabraClave.Text.Trim();
        }

        String TIPO_REPORTE = RadioButtonList_TipoReporte.SelectedValue;

        BaseDatos _base = new BaseDatos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaReporte = _base.ObtenerReporteBaseDatos(ID_OCUPACION, NIV_EDUCACION, PROFESION, AREA_INTERES_LABORAL, EXPERIENCIA, EDAD_DESDE, EDAD_HASTA, ID_CIUDAD, NOMBRES, APELLIDOS, BARRIO, ASPIRACION_DESDE, ASPIRACION_HASTA, FECHA_ACTUALIZACION_DESDE, FECHA_ACTUALIZACION_HASTA, PALABRA_CLAVE, TIPO_REPORTE);

        if (tablaReporte.Rows.Count <= 0)
        {
            if (_base.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _base.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros que cumplieran las condicioes de filtrado.", Proceso.Advertencia);
            }

            Panel_GrillaReporte.Visible = false;

            GridView_Reporte.DataSource = null;
            GridView_Reporte.DataBind();

            Button_ExportarExcel.Visible = false;
            Button_ExportarExcel_1.Visible = false;
        }
        else
        {
            Panel_GrillaReporte.Visible = true;

            Cargar_GrillaReporte_DesdeTabla(tablaReporte);

            if (TIPO_REPORTE == "NO LABORAN")
            {
                GridView_Reporte.Columns[0].Visible = true;
            }
            else
            {
                GridView_Reporte.Columns[0].Visible = false;
            }

            Button_ExportarExcel.Visible = true;
            Button_ExportarExcel_1.Visible = true;
        }
    }

    protected void Button_Buscar_Click(object sender, EventArgs e)
    {
        if (IsDatosCorrectos() == true)
        {
            GenerarReporte();
        }
    }

    private void ExportarDataTableToExcel1(DataTable dt)
    {
        StringBuilder output = new StringBuilder();

        output.AppendLine("<table border=\"1\" cellspacing=\"1\" cellpadding=\"1\">");
        output.AppendLine("<tr>");

        foreach (DataColumn dc in dt.Columns)
        {
            output.AppendLine("<td style=\"text-align:center; font-weight:bold;\">");
            output.Append(dc.ColumnName);
            output.AppendLine("</td>");
        }

        output.AppendLine("</tr>");
        
        foreach (DataRow item in dt.Rows)
        {
            output.AppendLine("<tr>");

            foreach (object value in item.ItemArray)
            {
                output.AppendLine("<td>");
                output.Append(value.ToString().Trim());
                output.AppendLine("</td>");
            }

            output.AppendLine("</tr>");
        }

        output.AppendLine("</table>");

        StringWriter sw = new StringWriter(output);

        Response.Clear(); 
        Response.ContentType = "application/vnd.ms-excel"; 
        Response.AddHeader("Content-Disposition", "attachment;filename=Reporte_BaseDatos_" + DateTime.Now.ToString("ddMMyyyy") + ".xls"); 
        
        Response.ContentEncoding = Encoding.Default;
        Response.Output.Write(sw.ToString());
        
        Response.End();
    }

    public void ExportarDataTableToExcel3(DataTable dt)
    {
	    if (dt.Rows.Count > 0)
	    {
		    string filename = "ReporteBasedatos.xls";
            System.IO.StringWriter tw = new System.IO.StringWriter();
		    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);

		    DataGrid dgGrid = new DataGrid();
		    dgGrid.DataSource = dt;
		    dgGrid.DataBind();

		    dgGrid.RenderControl(hw);
		    Response.ContentType = "application/vnd.ms-excel";
		    Response.AppendHeader("Content-Disposition", "attachment; filename=" + filename + "");
            Response.Charset = "";
		    Response.Write(tw.ToString());
		    Response.End();
	    }
    }

    private void ExportarReporte()
    {
        tools _tools = new tools();

        String ID_OCUPACION = null;
        if (CheckBox_CargoAspira.Checked == true)
        {
            ID_OCUPACION = DropDownList_CargoAspira.SelectedValue;
        }

        String NIV_EDUCACION = null;
        if (CheckBox_NivelEscolaridad.Checked == true)
        {
            NIV_EDUCACION = DropDownList_NivelEsolaridad.SelectedValue;
        }

        String PROFESION = null;
        if (CheckBox_Profesión.Checked == true)
        {
            PROFESION = DropDownList_Profesion.SelectedValue;
        }

        String AREA_INTERES_LABORAL = null;
        if (CheckBox_AreaInteres.Checked == true)
        {
            AREA_INTERES_LABORAL = DropDownList_AreaInteres.SelectedValue;
        }

        String EXPERIENCIA = null;
        if (CheckBox_Experiencia.Checked == true)
        {
            EXPERIENCIA = DropDownList_Experiencia.SelectedValue;
        }

        String EDAD_DESDE = null;
        String EDAD_HASTA = null;
        if (CheckBox_Edad.Checked == true)
        {
            EDAD_DESDE = TextBox_EdadDesde.Text;
            EDAD_HASTA = TextBox_EdadHasta.Text;
        }

        String ID_CIUDAD = null;
        if (CheckBox_Ciudad.Checked == true)
        {
            ID_CIUDAD = DropDownList_Ciudad.SelectedValue;
        }

        String NOMBRES = null;
        String APELLIDOS = null;
        if (CheckBox_NombresApellidos.Checked == true)
        {
            NOMBRES = TextBox_Nombres.Text;
            APELLIDOS = TextBox_Apellidos.Text;
        }

        String BARRIO = null;
        if (CheckBox_Barrio.Checked == true)
        {
            BARRIO = TextBox_Barrio.Text;
        }

        String ASPIRACION_DESDE = null;
        String ASPIRACION_HASTA = null;
        if (CheckBox_AspiracionSalarial.Checked == true)
        {
            ASPIRACION_DESDE = TextBox_AspiracionSalarialDesde.Text.Replace(".", "").Replace(",", ".");
            ASPIRACION_HASTA = TextBox_AspiracionSalarialHasta.Text.Replace(".", "").Replace(",", ".");
        }

        String FECHA_ACTUALIZACION_DESDE = null;
        String FECHA_ACTUALIZACION_HASTA = null;
        if (CheckBox_FechaActualizacion.Checked == true)
        {
            FECHA_ACTUALIZACION_DESDE = _tools.obtenerStringConFormatoFechaSQLServer(Convert.ToDateTime(TextBox_FechaActualizacionDesde.Text));
            FECHA_ACTUALIZACION_HASTA = _tools.obtenerStringConFormatoFechaSQLServer(Convert.ToDateTime(TextBox_FechaActualizacionHasta.Text));
        }

        String PALABRA_CLAVE = null;
        if (CheckBox_PalabraClave.Checked == true)
        {
            PALABRA_CLAVE = TextBox_PalabraClave.Text.Trim();
        }

        String TIPO_REPORTE = RadioButtonList_TipoReporte.SelectedValue;

        BaseDatos _base = new BaseDatos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaReporte = _base.ObtenerReporteBaseDatos(ID_OCUPACION, NIV_EDUCACION, PROFESION, AREA_INTERES_LABORAL, EXPERIENCIA, EDAD_DESDE, EDAD_HASTA, ID_CIUDAD, NOMBRES, APELLIDOS, BARRIO, ASPIRACION_DESDE, ASPIRACION_HASTA, FECHA_ACTUALIZACION_DESDE, FECHA_ACTUALIZACION_HASTA, PALABRA_CLAVE, TIPO_REPORTE);

        ExportarDataTableToExcel1(tablaReporte);
    }

    protected void Button_ExportarExcel_Click(object sender, EventArgs e)
    {
        if (IsDatosCorrectos() == true)
        {
            ExportarReporte();
        }
    }
    protected void Button_BUSCADOR_CARGO_Click(object sender, EventArgs e)
    {
        cargar_DropDownList_CargoAspira_Filtrado();
    }

    private void cargar_DropDownList_CargoAspira_Filtrado()
    {
        DropDownList_CargoAspira.Items.Clear();

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_CargoAspira.Items.Add(item);

        cargo _cargo = new cargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        String NOMBRE_OCUPACION_A_BUSCAR = TextBox_BUSCADOR_CARGO.Text.ToUpper().Trim();

        DataTable tablaCargosEncontrados = _cargo.ObtenerRecOcupacionesPorNomOcupacion(NOMBRE_OCUPACION_A_BUSCAR);

        foreach (DataRow fila in tablaCargosEncontrados.Rows)
        {
            item = new ListItem(fila["NOM_OCUPACION"].ToString(), fila["ID_OCUPACION"].ToString());
            DropDownList_CargoAspira.Items.Add(item);
        }
        DropDownList_CargoAspira.DataBind();
    }

    private void Cargar_DropDownListVacio(DropDownList drop)
    {
        drop.Items.Clear();
        drop.Items.Add(new ListItem("Seleccione...", ""));
        drop.DataBind();
    }

    private void MostrarBotonesSegunChecks()
    {
        Int32 contador = 0;

        if (CheckBox_CargoAspira.Checked == true)
        {
            contador += 1;
        }

        if (CheckBox_AreaInteres.Checked == true)
        {
            contador += 1;
        }

        if (CheckBox_Experiencia.Checked == true)
        {
            contador += 1;
        }

        if (CheckBox_AspiracionSalarial.Checked == true)
        {
            contador += 1;
        }

        if (CheckBox_NivelEscolaridad.Checked == true)
        {
            contador += 1;
        }

        if (CheckBox_Profesión.Checked == true)
        {
            contador += 1;
        }

        if (CheckBox_Ciudad.Checked == true)
        {
            contador += 1;
        }

        if (CheckBox_Barrio.Checked == true)
        {
            contador += 1;
        }

        if (CheckBox_FechaActualizacion.Checked == true)
        {
            contador += 1;
        }

        if (CheckBox_NombresApellidos.Checked == true)
        {
            contador += 1;
        }

        if (CheckBox_Edad.Checked == true)
        {
            contador += 1;
        }

        if (CheckBox_PalabraClave.Checked == true)
        {
            contador += 1;
        }

        if (contador <= 0)
        {
            Panel_FORM_BOTONES.Visible = true;

            Button_Buscar.Visible = false;
            Button_ExportarExcel.Visible = false;

            Panel_FORM_BOTONES_PIE.Visible = false;
            Button_Buscar_1.Visible = false;
            Button_ExportarExcel_1.Visible = false;
        }
        else
        {
            Panel_FORM_BOTONES.Visible = true;

            Button_Buscar.Visible = true;
            Button_ExportarExcel.Visible = false;

            Panel_FORM_BOTONES_PIE.Visible = true;
            Button_Buscar_1.Visible = true;
            Button_ExportarExcel_1.Visible = false;
        }

        Panel_GrillaReporte.Visible = false;
    }

    protected void CheckBox_CargoAspira_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_CargoAspira.Checked == false)
        {
            Panel_CargoAspira.Visible = false;
        }
        else
        {
            Panel_CargoAspira.Visible = true;

            TextBox_BUSCADOR_CARGO.Text = "";
            Cargar_DropDownListVacio(DropDownList_CargoAspira);
        }

        MostrarBotonesSegunChecks();
    }
    protected void CheckBox_AspiracionSalarial_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_AspiracionSalarial.Checked == false)
        {
            Panel_AspiracionSalarial.Visible = false;
        }
        else
        {
            Panel_AspiracionSalarial.Visible = true;

            TextBox_AspiracionSalarialDesde.Text = "";
            TextBox_AspiracionSalarialHasta.Text = "";
        }

        MostrarBotonesSegunChecks();
    }

    private void Cargar_DropDownList_AreaInteres()
    {
        DropDownList_AreaInteres.Items.Clear();

        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaAreasEspecializacion = _radicacionHojasDeVida.ObtenerAreasInteresLaboral();

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_AreaInteres.Items.Add(item);

        int contador = 0;

        foreach (DataRow fila in tablaAreasEspecializacion.Rows)
        {
            if (contador > 0)
            {
                item = new ListItem(fila["DESCRIPCION"].ToString(), fila["ID_AREAINTERES"].ToString());
                DropDownList_AreaInteres.Items.Add(item);
            }
            contador += 1;
        }

        DropDownList_AreaInteres.DataBind();
    }

    protected void CheckBox_AreaInteres_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_AreaInteres.Checked == false)
        {
            Panel_AreaInteres.Visible = false;
        }
        else
        {
            Panel_AreaInteres.Visible = true;

            Cargar_DropDownList_AreaInteres();
        }

        MostrarBotonesSegunChecks();

    }

    private void Cargar_DropDownList_Experiencia()
    {
        DropDownList_Experiencia.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_EXPERIENCIA);

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_Experiencia.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_Experiencia.Items.Add(item);
        }
        DropDownList_Experiencia.DataBind();
    }

    protected void CheckBox_Experiencia_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_Experiencia.Checked == false)
        {
            Panel_Experiencia.Visible = false;
        }
        else
        {
            Panel_Experiencia.Visible = true;

            Cargar_DropDownList_Experiencia();
        }

        MostrarBotonesSegunChecks();
    }
    private void Cargar_DropDownList_NivelEsolaridad()
    {
        DropDownList_NivelEsolaridad.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerNivEstudiosParametros();

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_NivelEsolaridad.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_NivelEsolaridad.Items.Add(item);
        }

        DropDownList_NivelEsolaridad.DataBind();
    }

    protected void CheckBox_NivelEscolaridad_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_NivelEscolaridad.Checked == false)
        {
            Panel_NivelEscolaridad.Visible = false;
        }
        else
        {
            Panel_NivelEscolaridad.Visible = true;

            Cargar_DropDownList_NivelEsolaridad();
        }

        MostrarBotonesSegunChecks();
    }

    private void Cargar_DropDownList_Profesion()
    {
        DropDownList_Profesion.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla("NUCLEO_FORMACION");

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_Profesion.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_Profesion.Items.Add(item);
        }
        DropDownList_Profesion.DataBind();
    }

    protected void CheckBox_Profesión_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_Profesión.Checked == false)
        {
            Panel_Profesion.Visible = false;
        }
        else
        {
            Panel_Profesion.Visible = true;

            Cargar_DropDownList_Profesion();
        }

        MostrarBotonesSegunChecks();
    }

    private void Cargar_DropDownList_Departamento()
    {
        DropDownList_Departamento.Items.Clear();

        departamento _departamento = new departamento(Session["idEmpresa"].ToString());
        DataTable tablaDepartamentos = _departamento.ObtenerTodasLosDepartamentos();

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_Departamento.Items.Add(item);

        foreach (DataRow fila in tablaDepartamentos.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_DEPARTAMENTO"].ToString());
            DropDownList_Departamento.Items.Add(item);
        }

        DropDownList_Departamento.DataBind();
    }

    protected void CheckBox_Ciudad_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_Ciudad.Checked == false)
        {
            Panel_Ciudad.Visible = false;
        }
        else
        {
            Panel_Ciudad.Visible = true;

            Cargar_DropDownList_Departamento();
            Cargar_DropDownListVacio(DropDownList_Ciudad);
        }

        MostrarBotonesSegunChecks();
    }

    private void cargar_DropDownList_Ciudad(String idDepartamento)
    {
        DropDownList_Ciudad.Items.Clear();

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
        DataTable tablaCiudades = _ciudad.ObtenerCiudadesPorIdDepartamento(idDepartamento);

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_Ciudad.Items.Add(item);

        foreach (DataRow fila in tablaCiudades.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_CIUDAD"].ToString());
            DropDownList_Ciudad.Items.Add(item);
        }

        DropDownList_Ciudad.DataBind();
    }

    protected void DropDownList_Departamento_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_Departamento.SelectedIndex <= 0)
        {
            Cargar_DropDownListVacio(DropDownList_Ciudad);
        }
        else
        {
            String ID_DEPARTAMENTO = DropDownList_Departamento.SelectedValue.ToString();
            cargar_DropDownList_Ciudad(ID_DEPARTAMENTO);
        }
    }

    protected void CheckBox_Barrio_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_Barrio.Checked == false)
        {
            Panel_Barrio.Visible = false;
        }
        else
        {
            Panel_Barrio.Visible = true;

            TextBox_Barrio.Text = "";
        }

        MostrarBotonesSegunChecks();
    }
    protected void CheckBox_FechaActualizacion_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_FechaActualizacion.Checked == false)
        {
            Panel_FechaActualziacion.Visible = false;
        }
        else
        {
            Panel_FechaActualziacion.Visible = true;

            TextBox_FechaActualizacionDesde.Text = "";
            TextBox_FechaActualizacionHasta.Text = "";
        }

        MostrarBotonesSegunChecks();
    }
    protected void CheckBox_NombresApellidos_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_NombresApellidos.Checked == false)
        {
            Panel_NombresApellidos.Visible = false;
        }
        else
        {
            Panel_NombresApellidos.Visible = true;

            TextBox_Nombres.Text = "";
            TextBox_Apellidos.Text = "";
        }

        MostrarBotonesSegunChecks();
    }
    protected void CheckBox_Edad_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_Edad.Checked == false)
        {
            Panel_Edad.Visible = false;
        }
        else
        {
            Panel_Edad.Visible = true;

            TextBox_EdadDesde.Text = "";
            TextBox_EdadHasta.Text = "";
        }

        MostrarBotonesSegunChecks();
    }
    protected void CheckBox_PalabraClave_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_PalabraClave.Checked == false)
        {
            Panel_PalabraClave.Visible = false;
        }
        else
        {
            Panel_PalabraClave.Visible = true;

            TextBox_PalabraClave.Text = "";
        }

        MostrarBotonesSegunChecks();
    }
    protected void GridView_Reporte_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_Reporte.PageIndex = e.NewPageIndex;

        if (IsDatosCorrectos() == true)
        {
            GenerarReporte();
        }
    }
    protected void GridView_Reporte_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
}