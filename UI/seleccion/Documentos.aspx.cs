using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brainsbits.LLB.seleccion;
using System.Data;
using Brainsbits.LLB;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;
using TSHAK.Components;

public partial class _Documentos : System.Web.UI.Page
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
        Inicio = 0,
        Nuevo,
        Cargar,
        Modificar
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
                Button_GUARDAR.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_CANCELAR.Visible = false;

                Panel_Documentos.Visible = false;
                Button_NUEVO_DOC.Visible = false;
                Button_GUARDAR_DOC.Visible = false;
                Button_CANCELAR_DOC.Visible = false;

                Panel_FORM_BOTONES_ABAJO.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;

                GridView_Documentos.Columns[0].Visible = false;
                break;
            case Acciones.Modificar:
                break;
        }

    }

    private void Desactivar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                break;
        }
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_FORM_BOTONES.Visible = true;
                Button_MODIFICAR.Visible = true;
                break;
            case Acciones.Nuevo:
                Panel_FORM_BOTONES.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_Documentos.Visible = true;

                Button_NUEVO_DOC.Visible = true;

                Panel_FORM_BOTONES_ABAJO.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;

                GridView_Documentos.Columns[0].Visible = true;
                break;
            case Acciones.Cargar:
                Panel_FORM_BOTONES.Visible = true;
                Button_MODIFICAR.Visible = true;

                Panel_Documentos.Visible = true;

                Panel_FORM_BOTONES_ABAJO.Visible = true;
                Button_MODIFICAR_1.Visible = true;
                break;
            case Acciones.Modificar:
                Panel_FORM_BOTONES.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_Documentos.Visible = true;
                Button_NUEVO_DOC.Visible = true;
                Panel_FORM_BOTONES_ABAJO.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;

                GridView_Documentos.Columns[0].Visible = true;
                break;
        }
    }

    private void CargarGrillaDocumentosDesdeTabla(DataTable tablaDocs)
    {
        GridView_Documentos.DataSource = tablaDocs;
        GridView_Documentos.DataBind();

        for (int i = 0; i < GridView_Documentos.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_Documentos.Rows[i];
            DataRow filaTabla = tablaDocs.Rows[i];

            TextBox texto_Nombre = filaGrilla.FindControl("TextBox_Documento") as TextBox;
            texto_Nombre.Text = filaTabla["NOMBRE"].ToString().Trim();

            DropDownList dropVigencia = filaGrilla.FindControl("DropDownList_Vigencia") as DropDownList;

            dropVigencia.SelectedValue = "False";
            if ((DBNull.Value.Equals(filaTabla["VIGENCIA"]) == true) || (filaTabla["VIGENCIA"].ToString() == "True") || (filaTabla["VIGENCIA"].ToString() == ""))
            {
                dropVigencia.SelectedValue = "True";
            }
        }
    }

    private void inhabilitarFilasGrilla(GridView grilla, int colInicio)
    {
        for (int i = 0; i < grilla.Rows.Count; i++)
        {
            for (int j = colInicio; j < grilla.Columns.Count; j++)
            {
                grilla.Rows[i].Cells[j].Enabled = false;
            }
        }
    }

    private void cargarGrillaDocumentos()
    {
        DocumentoValidacion _doc = new DocumentoValidacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaDocs = _doc.ObtenerDocumentosTodos();

        if (tablaDocs.Rows.Count <= 0)
        {
            if (_doc.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _doc.MensajeError, Proceso.Error);
            }
            else
            {
                Mostrar(Acciones.Nuevo);
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron Documentos para visualizar.", Proceso.Advertencia);
            }

            GridView_Documentos.DataSource = null;
            GridView_Documentos.DataBind();
        }
        else
        {
            Mostrar(Acciones.Cargar);

            CargarGrillaDocumentosDesdeTabla(tablaDocs);

            inhabilitarFilasGrilla(GridView_Documentos, 1);
        }
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                cargarGrillaDocumentos();
                break;
            case Acciones.Modificar:
                HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_FILA_SELECCIONADA_GRILLA.Value = "";
                HiddenField_ID_SEL_REG_DOCUMENTOS.Value = "";
                HiddenField_NOMBRE.Value = "";
                HiddenField_VIGENCIA.Value = "";
                break;
        }
    }

    private void Iniciar()
    {
        Configurar();
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = "DOCUMENTOS VALIDACIÓN";

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

    private void Actualizar()
    {
        List<DocumentoValidacion> listaDocumentos = new List<DocumentoValidacion>();
        DocumentoValidacion documentoParaLista;

        for (int i = 0; i < GridView_Documentos.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_Documentos.Rows[i];
            documentoParaLista = new DocumentoValidacion();

            TextBox datoNombre = filaGrilla.FindControl("TextBox_Documento") as TextBox;
            documentoParaLista.nombre = datoNombre.Text;

            documentoParaLista.id_sel_reg_documentos = Convert.ToDecimal(GridView_Documentos.DataKeys[i].Values["ID_SEL_REG_DOCUMENTOS"]);

            DropDownList dropVigente = filaGrilla.FindControl("DropDownList_Vigencia") as DropDownList;

            documentoParaLista.vigente = false;
            if (dropVigente.SelectedValue == "True")
            {
                documentoParaLista.vigente = true;
            }

            listaDocumentos.Add(documentoParaLista);
        }

        DocumentoValidacion _doc = new DocumentoValidacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean verificador = _doc.ActualizarDocumentos(listaDocumentos);

        if (verificador == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _doc.MensajeError, Proceso.Error);
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Cargar(Acciones.Inicio);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Los Documentos para validación de Contratación, fueron actualizadas correctamente.", Proceso.Correcto);
        }
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        if (HiddenField_ACCION_GRILLA.Value == AccionesGrilla.Ninguna.ToString())
        {
            Actualizar();
        }
        else
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Para poder guardar los cambios primero debe terminar las acciones sobre la grilla de Documentos.", Proceso.Advertencia);
        }
    }
    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Modificar);
        Cargar(Acciones.Modificar);
    }
    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }
    protected void GridView_Documentos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "modificar")
        {
            HiddenField_FILA_SELECCIONADA_GRILLA.Value = indexSeleccionado.ToString();

            ActivarFilaGrilla(GridView_Documentos, indexSeleccionado, 1);

            HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Modificar.ToString();
            HiddenField_FILA_SELECCIONADA_GRILLA.Value = indexSeleccionado.ToString();

            HiddenField_ID_SEL_REG_DOCUMENTOS.Value = GridView_Documentos.DataKeys[indexSeleccionado].Values["ID_SEL_REG_DOCUMENTOS"].ToString();

            TextBox texto_Nombre = GridView_Documentos.Rows[indexSeleccionado].FindControl("TextBox_Documento") as TextBox;
            HiddenField_NOMBRE.Value = texto_Nombre.Text;

            DropDownList dropVigencia = GridView_Documentos.Rows[indexSeleccionado].FindControl("DropDownList_Vigencia") as DropDownList;
            HiddenField_VIGENCIA.Value = dropVigencia.SelectedValue;

            GridView_Documentos.Columns[0].Visible = false;

            Button_NUEVO_DOC.Visible = false;
            Button_GUARDAR_DOC.Visible = true;
            Button_CANCELAR_DOC.Visible = true;
        }
    }

    private DataTable obtenerTablaDeGrillaDocs()
    {
        DataTable tablaResultado = new DataTable();
        tablaResultado.Columns.Add("ID_SEL_REG_DOCUMENTOS");
        tablaResultado.Columns.Add("NOMBRE");
        tablaResultado.Columns.Add("VIGENCIA");

        DataRow filaTablaResultado;

        for (int i = 0; i < GridView_Documentos.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_Documentos.Rows[i];

            Decimal ID_SEL_REG_DOCUMENTOS = Convert.ToDecimal(GridView_Documentos.DataKeys[i].Values["ID_SEL_REG_DOCUMENTOS"]);

            TextBox texto_Nombre = filaGrilla.FindControl("TextBox_Documento") as TextBox;
            String NOMBRE = texto_Nombre.Text;

            DropDownList dropVigencia = filaGrilla.FindControl("DropDownList_Vigencia") as DropDownList;
            String VIGENCIA = dropVigencia.SelectedValue;

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_SEL_REG_DOCUMENTOS"] = ID_SEL_REG_DOCUMENTOS;
            filaTablaResultado["NOMBRE"] = NOMBRE;
            filaTablaResultado["VIGENCIA"] = VIGENCIA;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }

    private void ActivarFilaGrilla(GridView grilla, int fila, int colInicio)
    {
        for (int i = 0; i < grilla.Columns.Count; i++)
        {
            grilla.Rows[fila].Cells[i].Enabled = true;
        }
    }

    protected void Button_NUEVO_DOC_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerTablaDeGrillaDocs();
        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_SEL_REG_DOCUMENTOS"] = 0;
        filaNueva["NOMBRE"] = "";
        filaNueva["VIGENCIA"] = "";

        tablaDesdeGrilla.Rows.Add(filaNueva);

        CargarGrillaDocumentosDesdeTabla(tablaDesdeGrilla);

        inhabilitarFilasGrilla(GridView_Documentos, 1);
        ActivarFilaGrilla(GridView_Documentos, GridView_Documentos.Rows.Count - 1, 1);

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_SELECCIONADA_GRILLA.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();
        HiddenField_ID_SEL_REG_DOCUMENTOS.Value = null;
        HiddenField_NOMBRE.Value = null;
        HiddenField_VIGENCIA.Value = null;

        GridView_Documentos.Columns[0].Visible = false;

        Button_NUEVO_DOC.Visible = false;
        Button_GUARDAR_DOC.Visible = true;
        Button_CANCELAR_DOC.Visible = true;
    }

    private void inhabilitarFilaGrilla(GridView grilla, int fila, int colInicio)
    {
        for (int j = colInicio; j < grilla.Columns.Count; j++)
        {
            grilla.Rows[fila].Cells[j].Enabled = false;
        }
    }

    protected void Button_GUARDAR_DOC_Click(object sender, EventArgs e)
    {
        int FILA_SELECCIONADA = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA.Value);

        inhabilitarFilaGrilla(GridView_Documentos, FILA_SELECCIONADA, 1);

        GridView_Documentos.Columns[0].Visible = true;

        Button_GUARDAR_DOC.Visible = false;
        Button_CANCELAR_DOC.Visible = false;
        Button_NUEVO_DOC.Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
    }

    protected void Button_CANCELAR_DOC_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = obtenerTablaDeGrillaDocs();

        if (HiddenField_ACCION_GRILLA.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }
        else
        {
            if (HiddenField_ACCION_GRILLA.Value == AccionesGrilla.Modificar.ToString())
            {
                DataRow filaGrilla = tablaGrilla.Rows[Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA.Value)];

                filaGrilla["ID_SEL_REG_DOCUMENTOS"] = HiddenField_ID_SEL_REG_DOCUMENTOS.Value;
                filaGrilla["NOMBRE"] = HiddenField_NOMBRE.Value;
                filaGrilla["VIGENCIA"] = HiddenField_VIGENCIA.Value;
            }
        }

        CargarGrillaDocumentosDesdeTabla(tablaGrilla);

        inhabilitarFilasGrilla(GridView_Documentos, 1);

        GridView_Documentos.Columns[0].Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();

        Button_NUEVO_DOC.Visible = true;
        Button_GUARDAR_DOC.Visible = false;
        Button_CANCELAR_DOC.Visible = false;
    }
}