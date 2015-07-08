using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using Brainsbits.LLB;
using Brainsbits.LLB.seleccion;
using Brainsbits.LLB.maestras;
using AjaxControlToolkit;
using Brainsbits.LLB.seguridad;
using TSHAK.Components;

public partial class seleccion_derogaciones : System.Web.UI.Page
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

    #region varialbles

    private enum Acciones
    {
        Inicio = 0,
        BusquedaEncontro = 1,
        BusquedaNoEncontro = 2,
        Carga = 3,
        Guarda = 4,
        Modifica = 5
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }

    #endregion varialbles

    #region constructor
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = "DEROGACIONES DE DCUMENTOS PARA PROCESO DE CONTRATACIÓN";

        if (!IsPostBack)
        {
            Iniciar();
        }
    }
    #endregion constructor

    #region metodos
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

    protected void Iniciar()
    {
        Configurar();
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }

    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                this.Panel_INFO_ADICIONAL_MODULO.Visible = false;
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = false;
                this.Panel_RESULTADOS_GRID.Visible = false;
                this.Panel_CONTROL_REGISTRO.Visible = false;
                this.Panel_FORMULARIO.Visible = false;
                this.Panel_requisitos.Visible = false;
                this.Panel_FORM_BOTONES_PIE.Visible = false;

                Button_GUARDAR.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                Button_MODIFICAR.Visible = true;
                Button_MODIFICAR_1.Visible = false;
                break;
            case Acciones.Modifica:
                Panel_CONTROL_REGISTRO.Visible = false;

                Button_GUARDAR.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_CANCELAR.Visible = false;

                Button_GUARDAR_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                break;
        }
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                this.Panel_INFO_ADICIONAL_MODULO.Visible = false;
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                break;
            case Acciones.Carga:
                this.Panel_INFO_ADICIONAL_MODULO.Visible = false;
                
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                
                this.Panel_FORMULARIO.Visible = true;
                this.Panel_requisitos.Visible = true;
                
                this.Panel_FORM_BOTONES_PIE.Visible = true;
                Button_MODIFICAR.Visible = true;
                Button_MODIFICAR_1.Visible = true;
                break;
            case Acciones.BusquedaEncontro:
                this.Panel_INFO_ADICIONAL_MODULO.Visible = false;
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                this.Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.BusquedaNoEncontro:
                this.Panel_INFO_ADICIONAL_MODULO.Visible = false;
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                break;
            case Acciones.Modifica:
                Button_GUARDAR.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR.Visible = true;
                Button_CANCELAR_1.Visible = true;
                break;
        }
    }

    private void Cargar(Acciones accion)
    {
        ListItem item = new ListItem("Seleccione...", "");
        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        switch (accion)
        {
            case Acciones.Inicio:
                DropDownList_BUSCAR.Items.Add(item);
                item = new ListItem("Número de documento", "NUMERO_DOCUMENTO");
                DropDownList_BUSCAR.Items.Add(item);
                item = new ListItem("Nombre", "NOMBRE");
                DropDownList_BUSCAR.Items.Add(item);
                DropDownList_BUSCAR.DataBind();

                HiddenField_ID_REQUERIMIENTO.Value = String.Empty;
                HiddenField_ID_SOLICITUD.Value = String.Empty;
                break;
        }
    }

    private void Cargar(GridView gridView, DataTable tablaDocumentos)
    {
        gridView.DataSource = tablaDocumentos;
        gridView.DataBind();

        for (int i = 0; i < tablaDocumentos.Rows.Count; i++)
        {
            DataRow filaTabla = tablaDocumentos.Rows[i];
            GridViewRow filaGrilla = gridView.Rows[i];

            CheckBox checkCumplido = filaGrilla.FindControl("CheckBox_Cumplido") as CheckBox;
            CheckBox checkDerogado = filaGrilla.FindControl("CheckBox_Derogado") as CheckBox;
            TextBox textoObservaciones = filaGrilla.FindControl("TextBox_Observaciones") as TextBox;

            if ((filaTabla["CUMPLIDO"] != DBNull.Value) && ((filaTabla["CUMPLIDO"].ToString().ToUpper() == "TRUE") || (filaTabla["CUMPLIDO"].ToString().ToUpper() == "1")))
            {
                checkCumplido.Checked = true;
            }

            if ((filaTabla["DEROGADO"] != DBNull.Value) && ((filaTabla["DEROGADO"].ToString().ToUpper() == "TRUE") || (filaTabla["DEROGADO"].ToString().ToUpper() == "1")))
            {
                checkDerogado.Checked = true;
            }

            checkCumplido.Enabled = false;

            textoObservaciones.Text = filaTabla["OBSERVACIONES"].ToString().Trim();
        }
    }

    protected void Consultar(Decimal id_requerimiento, Decimal id_solicitud)
    {
        requisito _requisito = new requisito(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable _dataTable = _requisito.ObtenerPorIdentificador(id_requerimiento, id_solicitud);

        if (_dataTable.Rows.Count > 0)
        {
            Session["requisitos"] = _dataTable;
            Cargar(GridView_Documentos, _dataTable);
        }
        else
        {
            if (!String.IsNullOrEmpty(_requisito.MensajeError))
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Error: Consulte con el Administrador: " + _requisito.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información para " + this.DropDownList_BUSCAR.SelectedItem + " : " + this.TextBox_BUSCAR.Text, Proceso.Advertencia);
            }

            Mostrar(Acciones.BusquedaNoEncontro);
        }

        _dataTable.Dispose();
    }

    protected void Buscar()
    {
        Ocultar(Acciones.Inicio);
        requisicion _requisicion = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable _dataTable = new DataTable();

        switch (this.DropDownList_BUSCAR.SelectedValue)
        {
            case "NUMERO_DOCUMENTO":
                _dataTable = _requisicion.ObtenerRequerimientosPorNumeroDocumento(this.TextBox_BUSCAR.Text);
                break;
            case "NOMBRE":
                _dataTable = _requisicion.ObtenerRequerimientosPorNombre(this.TextBox_BUSCAR.Text);
                break;
        }

        if (_dataTable.Rows.Count > 0)
        {
            GridView_RESULTADOS_BUSQUEDA.DataSource = _dataTable;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
            Mostrar(Acciones.BusquedaEncontro);
        }
        else
        {
            if (!String.IsNullOrEmpty(_requisicion.MensajeError))
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _requisicion.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "ADVERTENCIA: No se encontró información para " + this.DropDownList_BUSCAR.SelectedItem + " : " + this.TextBox_BUSCAR.Text, Proceso.Advertencia);
            }

            Mostrar(Acciones.BusquedaNoEncontro);
        }

        _dataTable.Dispose();

        HiddenField_ID_REQUERIMIENTO.Value = "";
        HiddenField_ID_SOLICITUD.Value = "";
    }

    private void configurarCaracteresAceptadosBusqueda(Boolean letras, Boolean numeros)
    {
        FilteredTextBoxExtender_TextBox_BUSCAR_Numbers.Enabled = numeros;
        FilteredTextBoxExtender_TextBox_BUSCAR_Letras.Enabled = letras;
    }

    #endregion metodos

    #region eventos
    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        Boolean verificador = true;

        requisito _requisito = new requisito(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaDocumentos = new DataTable();
        tablaDocumentos.Columns.Add("ID_CONTROL_REQUISITOS");
        tablaDocumentos.Columns.Add("ID_PRUEBA");
        tablaDocumentos.Columns.Add("ID_DOCUMENTO");
        tablaDocumentos.Columns.Add("CUMPLIDO");
        tablaDocumentos.Columns.Add("DEROGADO");
        tablaDocumentos.Columns.Add("OBSERVACIONES");

        for (int i = 0; i < GridView_Documentos.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_Documentos.Rows[i];

            CheckBox checkCumplido = filaGrilla.FindControl("CheckBox_Cumplido") as CheckBox;
            CheckBox checkDerogado = filaGrilla.FindControl("CheckBox_Derogado") as CheckBox;
            TextBox textoObservaciones = filaGrilla.FindControl("TextBox_Observaciones") as TextBox;

            DataRow filaTabla = tablaDocumentos.NewRow();

            filaTabla["ID_CONTROL_REQUISITOS"] = GridView_Documentos.DataKeys[i].Values["ID_CONTROL_REQUISITOS"].ToString();
            filaTabla["ID_PRUEBA"] = GridView_Documentos.DataKeys[i].Values["ID_PRUEBA"].ToString();
            filaTabla["ID_DOCUMENTO"] = GridView_Documentos.DataKeys[i].Values["ID_DOCUMENTO"].ToString();

            filaTabla["CUMPLIDO"] = "";
            if (checkCumplido.Checked == true)
            {
                filaTabla["CUMPLIDO"] = "1";
            }

            filaTabla["DEROGADO"] = "";
            filaTabla["OBSERVACIONES"] = "";
            if (checkDerogado.Checked == true)
            {
                filaTabla["DEROGADO"] = "1";

                if (String.IsNullOrEmpty(textoObservaciones.Text.Trim()) == true)
                {
                    verificador = false;
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Las OBSERVACIONES no pueden estar vacios cuando se DEROGA algún documento!.", Proceso.Advertencia);
                    break;
                }
                else
                {
                    filaTabla["OBSERVACIONES"] = textoObservaciones.Text.Trim();
                }
            }

            tablaDocumentos.Rows.Add(filaTabla);
        }

        if (verificador == true)
        {
            if (tablaDocumentos.Rows.Count <= 0)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron documentos en la Grilla de derogaciones!.", Proceso.Error);
            }
            else
            {
                Boolean cumplido = _requisito.Cumplir(tablaDocumentos);

                if (cumplido)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La información para derogaciones fue almacenada correctamente.", Proceso.Correcto);

                    Ocultar(Acciones.Inicio);
                    Mostrar(Acciones.Carga);

                    Consultar(Convert.ToDecimal(HiddenField_ID_REQUERIMIENTO.Value), Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value));
                    InhabilitarTodasFilasGrilla(GridView_Documentos, 0);
                }
                else
                {
                    if (!String.IsNullOrEmpty(_requisito.MensajeError))
                    {
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Error: Consulte con el Administrador: " + _requisito.MensajeError, Proceso.Error);
                    }
                }
            }   
        }   
    }
    
    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        Buscar();
    }
    
    protected void DropDownList_BUSCAR_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (DropDownList_BUSCAR.SelectedValue)
        {
            case "":
                configurarCaracteresAceptadosBusqueda(false, false);
                break;
            case "NUMERO_DOCUMENTO":
                configurarCaracteresAceptadosBusqueda(false, true);
                break;
            case "NOMBRE":
                configurarCaracteresAceptadosBusqueda(true, false);
                break;
        }
    }

    private void InhabilitarTodasFilasGrilla(GridView grilla, Int32 colInicio)
    {
        for (int i = 0; i < grilla.Rows.Count; i++)
        {
            for (int j = colInicio; j < grilla.Columns.Count; j++)
            {
                grilla.Rows[i].Cells[j].Enabled = false;
            }
        }
    }

    private void InhabilitarFilaGrilla(GridView grilla, Int32 numFila, Int32 colInicio)
    {

        for (int j = colInicio; j < grilla.Columns.Count; j++)
        {
            grilla.Rows[numFila].Cells[j].Enabled = false;
        }
    }

    private void HabilitarFilaGrilla(GridView grilla, Int32 numFila, Int32 colInicio)
    {

        for (int j = colInicio; j < grilla.Columns.Count; j++)
        {
            grilla.Rows[numFila].Cells[j].Enabled = true;
        }
    }

    protected void GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged(object sender, EventArgs e)
    {
        LabelIdRequerimiento.Text = this.GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["ID_REQUERIMIENTO"].ToString();
        Label_NumDocIdentidad.Text = this.GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["NUM_DOC_IDENTIDAD"].ToString();
        Label_Nombre.Text = this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[4].Text.Trim() + " " + this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[5].Text.Trim();

        HiddenField_ID_REQUERIMIENTO.Value = this.GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["ID_REQUERIMIENTO"].ToString();
        HiddenField_ID_SOLICITUD.Value = this.GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["ID_SOLICITUD"].ToString();

        if (this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[11].Text != "&nbsp;")
        {
            Label_Cargo.Text = this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[11].Text;
        }
        else
        {
            Label_Cargo.Text = "";
        }

        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Carga);
        
        Consultar(Convert.ToDecimal(this.GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["ID_REQUERIMIENTO"]), Convert.ToDecimal(this.GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["ID_SOLICITUD"]));
        InhabilitarTodasFilasGrilla(GridView_Documentos, 0);
    }

    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;
        Buscar();
    }
    
    #endregion eventos

    private void HabilitarFilasAptasEnGrillaDocumentos(GridView grilla)
    {
        for (int i = 0; i < grilla.Rows.Count; i++)
        {
            GridViewRow filaGrilla = grilla.Rows[i];

            CheckBox checkCumplido = filaGrilla.FindControl("CheckBox_Cumplido") as CheckBox;
            CheckBox checkDerogado = filaGrilla.FindControl("CheckBox_Derogado") as CheckBox;

            if (checkCumplido.Checked == true)
            {
                InhabilitarFilaGrilla(grilla, i, 0);
            }
            else
            {
                HabilitarFilaGrilla(grilla, i, 0);
            }  
        }
    }

    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Modifica);
        Mostrar(Acciones.Modifica);

        HabilitarFilasAptasEnGrillaDocumentos(GridView_Documentos);
    }

    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        if ((String.IsNullOrEmpty(HiddenField_ID_REQUERIMIENTO.Value) == true) && (String.IsNullOrEmpty(HiddenField_ID_SOLICITUD.Value) == true))
        {
            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);   
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.Carga);

            Consultar(Convert.ToDecimal(HiddenField_ID_REQUERIMIENTO.Value), Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value));
            InhabilitarTodasFilasGrilla(GridView_Documentos, 0);
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
}