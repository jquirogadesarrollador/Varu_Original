using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brainsbits.LLB.seleccion;
using System.Data;
using Brainsbits.LLB.maestras;
using TSHAK.Components;
using System.IO;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB;

public partial class _FabricaAssesment : System.Web.UI.Page
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
                Button_NUEVO.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_VOLVER.Visible = false;

                Panel_RESULTADOS_GRID.Visible = false;

                Panel_DatosAssesmentSeleccionado.Visible = false;

                Panel_CONTROL_REGISTRO.Visible = false;

                Panel_HabilidadesAssesment.Visible = false;
                Button_NUEVA_COMPETENCIA.Visible = false;
                Button_GUARDAR_COMPETENCIA.Visible = false;
                Button_CANCELAR_COMPETENCIA.Visible = false;

                Panel_LinkAssesment.Visible = false;
                Panel_MostrarLink.Visible = false;
                Panel_AdjuntarLink.Visible = false;

                Panel_EstadoAssesment.Visible = false;

                Panel_FORM_PIE.Visible = false;
                Button_NUEVO_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                Button_VOLVER_1.Visible = false;

                GridView_CompetenciasAssesment.Columns[0].Visible = false;
                break;
            case Acciones.Modificar:
                Button_NUEVO.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_VOLVER.Visible = false;

                Button_NUEVO_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                Button_VOLVER_1.Visible = false;

                Panel_CONTROL_REGISTRO.Visible = false;

                Button_NUEVA_COMPETENCIA.Visible = false;
                Button_GUARDAR_COMPETENCIA.Visible = false;
                Button_CANCELAR_COMPETENCIA.Visible = false;

                break;
        }
    }

    private void Desactivar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:

                TextBox_FCH_CRE.Enabled = false;
                TextBox_HOR_CRE.Enabled = false;
                TextBox_USU_CRE.Enabled = false;
                TextBox_FCH_MOD.Enabled = false;
                TextBox_HOR_MOD.Enabled = false;
                TextBox_USU_MOD.Enabled = false;

                TextBox_NombreAssesment.Enabled = false;
                TextBox_DescripcionAssesment.Enabled = false;
                DropDownList_EstadoAssesment.Enabled = false;
                break;
        }
    }

    private void Cargar_GridView_ASSESMENT_desde_tabla(DataTable tablaAssesment)
    {
        GridView_ASSESMENT.DataSource = tablaAssesment;
        GridView_ASSESMENT.DataBind();

        for (int i = 0; i < tablaAssesment.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ASSESMENT.Rows[i];
            DataRow filaTabla = tablaAssesment.Rows[i];

            HyperLink link = filaGrilla.FindControl("HyperLink_ARCHIVO_ASSESMENT") as HyperLink;

            if (DBNull.Value.Equals(filaTabla["ARCHIVO_DOCUMENTO"]) == false)
            {
                tools _tools = new tools();
                SecureQueryString QueryStringSeguro;
                QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

                QueryStringSeguro["id_documento"] = filaTabla["ID_DOCUMENTO"].ToString();

                link.NavigateUrl = "~/seleccion/VisorDocumentosFabricaAssesment.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
                link.Enabled = true;
            }
            else
            {
                link.Enabled = false;
                link.Text = "Sin Archivo";
            }
        }
    }

    private void CargarGrillaAssesmentCenter()
    {
        FabricaAssesment _fab = new FabricaAssesment(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaAssesment = _fab.ObtenerAssesmentCenteActivos();

        if (tablaAssesment.Rows.Count <= 0)
        {
            if (_fab.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _fab.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros de Assesment Center.", Proceso.Advertencia);
            }

            Panel_RESULTADOS_GRID.Visible = false;

            GridView_ASSESMENT.DataSource = null;
            GridView_ASSESMENT.DataBind();
        }
        else
        {
            Boolean correcto = true;

            tablaAssesment.Columns.Add("COMPETENCIAS");

            tablaAssesment.AcceptChanges();

            for(int i = 0; i < tablaAssesment.Rows.Count; i++)
            {
                DataRow filaAssesment = tablaAssesment.Rows[i];
                DataTable tablaCompetenciasAssesment = _fab.ObtenerCompetenciasAssesmentCenteActivos(Convert.ToDecimal(filaAssesment["ID_ASSESMENT_CENTER"]), 0);

                if (tablaCompetenciasAssesment.Rows.Count <= 0)
                {
                    if (_fab.MensajeError != null)
                    {
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _fab.MensajeError, Proceso.Error);
                        correcto = false;
                        break;
                    }
                    else
                    {
                        filaAssesment["COMPETENCIAS"] = "NINGUNA ASIGNADA";
                    }
                }
                else
                { 
                    for (int j = 0; j < tablaCompetenciasAssesment.Rows.Count; j++)
                    {
                        DataRow filaCompetencia = tablaCompetenciasAssesment.Rows[j];
                        if (j == 0)
                        {
                            filaAssesment["COMPETENCIAS"] = filaCompetencia["COMPETENCIA"].ToString();
                        }
                        else
                        {
                            filaAssesment["COMPETENCIAS"] += "<br />" + filaCompetencia["COMPETENCIA"].ToString();
                        }
                    }
                }

                tablaAssesment.AcceptChanges();
            }

            if (correcto == true)
            {
                Cargar_GridView_ASSESMENT_desde_tabla(tablaAssesment);
            }
            else
            {
                Panel_RESULTADOS_GRID.Visible = false;
                GridView_ASSESMENT.DataSource = null;
                GridView_ASSESMENT.DataBind();
            }
        }
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                CargarGrillaAssesmentCenter();

                HiddenField_ID_ASSESMENT_CENTER.Value = "";
                break;
            case Acciones.Nuevo:
                HiddenField_ID_ASSESMENT_CENTER.Value = "";
                TextBox_NombreAssesment.Text = "";
                TextBox_DescripcionAssesment.Text = "";
                HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();

                GridView_CompetenciasAssesment.DataSource = null;
                GridView_CompetenciasAssesment.DataBind();
                break;
            case Acciones.Modificar:
                HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
                break;
        }
    }

    private void Mostrar(Acciones accion)
    { 
        switch(accion)
        {
            case Acciones.Inicio:
                Panel_FORM_BOTONES.Visible = true;
                Button_NUEVO.Visible = true;
                Button_VOLVER.Visible = true;

                Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.Nuevo:
                Panel_FORM_BOTONES.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_DatosAssesmentSeleccionado.Visible = true;
                Panel_HabilidadesAssesment.Visible = true;
                GridView_CompetenciasAssesment.Columns[0].Visible = true;
                Button_NUEVA_COMPETENCIA.Visible = true;

                Panel_LinkAssesment.Visible = true;
                Panel_AdjuntarLink.Visible = true;

                Panel_FORM_PIE.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
                break;
            case Acciones.Cargar:
                Panel_FORM_BOTONES.Visible = true;
                Button_NUEVO.Visible = true;
                Button_VOLVER.Visible = true;
                Button_MODIFICAR.Visible = true;

                Panel_DatosAssesmentSeleccionado.Visible = true;
                Panel_CONTROL_REGISTRO.Visible = true;

                Panel_HabilidadesAssesment.Visible = true;

                Panel_LinkAssesment.Visible = true;
                Panel_MostrarLink.Visible = true;

                Panel_FORM_PIE.Visible = true;
                Button_NUEVO_1.Visible = true;
                Button_VOLVER_1.Visible = true;
                Button_MODIFICAR_1.Visible = true;
                break;
            case Acciones.Modificar:
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_HabilidadesAssesment.Visible = true;
                GridView_CompetenciasAssesment.Columns[0].Visible = true;
                Button_NUEVA_COMPETENCIA.Visible = true;
                Panel_AdjuntarLink.Visible = true;

                Panel_EstadoAssesment.Visible = true;

                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
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
        Page.Header.Title = "FABRIACA DE ASSESMENT CENTER";

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
        switch(accion)
        {   
            case Acciones.Nuevo:
                TextBox_NombreAssesment.Enabled = true;
                TextBox_DescripcionAssesment.Enabled = true;
                break;
            case Acciones.Modificar:
                TextBox_NombreAssesment.Enabled = true;
                TextBox_DescripcionAssesment.Enabled = true;
                DropDownList_EstadoAssesment.Enabled = true;
                break;
        }
    }

    protected void Button_NUEVO_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Mostrar(Acciones.Nuevo);
        Activar(Acciones.Nuevo);
        Cargar(Acciones.Nuevo);
    }

    private void Actualizar()
    {
        tools _tools = new tools();

        Decimal ID_ASSESMENT_CENTER = 0;
        if(String.IsNullOrEmpty(HiddenField_ID_ASSESMENT_CENTER.Value) == false)
        {
            ID_ASSESMENT_CENTER = Convert.ToDecimal(HiddenField_ID_ASSESMENT_CENTER.Value);
        }

        String NOMBRE_ASSESMENT = TextBox_NombreAssesment.Text.Trim();
        String DESCRIPCION_ASSESMENT = TextBox_DescripcionAssesment.Text.Trim();

        Boolean ACTIVO = true;
        if (ID_ASSESMENT_CENTER != 0)
        {
            if (DropDownList_EstadoAssesment.SelectedValue == "True")
            {
                ACTIVO = true;
            }
            else
            {
                ACTIVO = false;
            }
        }

        List<CompetenciaAssesment> listaCompetencias = new List<CompetenciaAssesment>();

        for (int i = 0; i < GridView_CompetenciasAssesment.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_CompetenciasAssesment.Rows[i];

            Decimal ID_COMPETENCIA_ASSESMENT = Convert.ToDecimal(GridView_CompetenciasAssesment.DataKeys[i].Values["ID_COMPETENCIA_ASSESMENT"]);
            Decimal ID_ASSESMENT = Convert.ToDecimal(GridView_CompetenciasAssesment.DataKeys[i].Values["ID_ASSESMENT"]);

            DropDownList dropCompetencia = filaGrilla.FindControl("DropDownList_CompetenciaAssesment") as DropDownList;
            Decimal ID_COMPETENCIA = Convert.ToDecimal(dropCompetencia.SelectedValue);

            CompetenciaAssesment competenciaParaLista = new CompetenciaAssesment();

            competenciaParaLista.ACTIVO = ACTIVO;
            competenciaParaLista.ID_ASSESMENT = ID_ASSESMENT;
            competenciaParaLista.ID_COMPETENCIA = ID_COMPETENCIA;
            competenciaParaLista.ID_COMPETENCIA_ASSESMENT = ID_COMPETENCIA_ASSESMENT;

            listaCompetencias.Add(competenciaParaLista);
        }


        Byte[] ARCHIVO_DOCUMENTO = null;
        Int32 ARCHIVO_TAMANO = 0;
        String ARCHIVO_EXTENSION = null;
        String ARCHIVO_TYPE = null;
        if (FileUpload_Archivo.HasFile == true)
        {
            using (BinaryReader reader = new BinaryReader(FileUpload_Archivo.PostedFile.InputStream))
            {
                ARCHIVO_DOCUMENTO = reader.ReadBytes(FileUpload_Archivo.PostedFile.ContentLength);
                ARCHIVO_TAMANO = FileUpload_Archivo.PostedFile.ContentLength;
                ARCHIVO_TYPE = FileUpload_Archivo.PostedFile.ContentType;
                ARCHIVO_EXTENSION = _tools.obtenerExtensionArchivo(FileUpload_Archivo.PostedFile.FileName);
            }
        }

        hojasVida _hojas = new hojasVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        ID_ASSESMENT_CENTER = _hojas.ActualizarAssesmentCenter(ID_ASSESMENT_CENTER, NOMBRE_ASSESMENT, DESCRIPCION_ASSESMENT, ACTIVO, listaCompetencias, ARCHIVO_DOCUMENTO, ARCHIVO_TAMANO, ARCHIVO_EXTENSION, ARCHIVO_TYPE);

        if (ID_ASSESMENT_CENTER > 0)
        { 
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El Assesment Center " + NOMBRE_ASSESMENT + " fue procesado correctamente.", Proceso.Correcto);

            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Cargar);

            Cargar(ID_ASSESMENT_CENTER);
        }
        else
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _hojas.MensajeError, Proceso.Error);
        }
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        if (HiddenField_ACCION_GRILLA.Value != AccionesGrilla.Ninguna.ToString())
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Para poder continuar primero se deben terminar todas las acciones sobre la grilla de competencias.", Proceso.Advertencia);
        }
        else
        {
            Actualizar();
        }
    }

    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Modificar);
        Mostrar(Acciones.Modificar);
        Activar(Acciones.Modificar);
        Cargar(Acciones.Modificar);
    }

    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(HiddenField_ID_ASSESMENT_CENTER.Value) == true)
        { 
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Cargar);

            Cargar(Convert.ToDecimal(HiddenField_ID_ASSESMENT_CENTER.Value));
        }
    }
    protected void Button_LISTA_CONTRATOS_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }

    private void Cargar_DropDownList_EstadoAssesment()
    {
        DropDownList_EstadoAssesment.Items.Clear();

        DropDownList_EstadoAssesment.Items.Add(new ListItem("Seleccione...", ""));

        DropDownList_EstadoAssesment.Items.Add(new ListItem("ACTIVO", "True"));
        DropDownList_EstadoAssesment.Items.Add(new ListItem("INACTIVO", "False"));

        DropDownList_EstadoAssesment.DataBind();
    }

    private void CargarInformacionRegistroControl(DataRow filaAssesment)
    {
        TextBox_USU_CRE.Text = filaAssesment["USU_CRE"].ToString();
        try
        {
            TextBox_FCH_CRE.Text = DateTime.Parse(filaAssesment["FCH_CRE"].ToString()).ToShortDateString();
            TextBox_HOR_CRE.Text = DateTime.Parse(filaAssesment["FCH_CRE"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_CRE.Text = "";
            TextBox_HOR_CRE.Text = "";
        }
        TextBox_USU_MOD.Text = filaAssesment["USU_MOD"].ToString();
        try
        {
            TextBox_FCH_MOD.Text = DateTime.Parse(filaAssesment["FCH_MOD"].ToString()).ToShortDateString();
            TextBox_HOR_MOD.Text = DateTime.Parse(filaAssesment["FCH_MOD"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_MOD.Text = "";
            TextBox_HOR_MOD.Text = "";
        }
    }

    private void CargarInformacionBasica(DataRow filaAssesment)
    {
        TextBox_NombreAssesment.Text = filaAssesment["NOMBRE_ASSESMENT"].ToString().Trim();
        TextBox_DescripcionAssesment.Text = filaAssesment["DESCRIPCION_ASSESMENT"].ToString().Trim();

        Cargar_DropDownList_EstadoAssesment();
        DropDownList_EstadoAssesment.SelectedValue = filaAssesment["ACTIVO"].ToString().Trim();
    }

    private void CargarArchivoAssesment(DataRow filaAssesment)
    {
        if (DBNull.Value.Equals(filaAssesment["ARCHIVO_DOCUMENTO"]) == true)
        {
            HyperLink_ArchivoAssesment.Text = "Sin Archivo";
            HyperLink_ArchivoAssesment.Enabled = false;
        }
        else
        {
            tools _tools = new tools();
            SecureQueryString QueryStringSeguro;
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

            QueryStringSeguro["id_documento"] = filaAssesment["ID_DOCUMENTO"].ToString();

            HyperLink_ArchivoAssesment.NavigateUrl = "~/seleccion/VisorDocumentosFabricaAssesment.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            HyperLink_ArchivoAssesment.Enabled = true;
        }
    }

    private void CargarCompetenciasAssesmentCenter(Decimal ID_ASSESMENT)
    {
        FabricaAssesment _fabrica = new FabricaAssesment(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCompetencias = _fabrica.ObtenerCompetenciasAssesmentCenteActivos(ID_ASSESMENT, 0);

        if (tablaCompetencias.Rows.Count <= 0)
        {
            if (_fabrica.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _fabrica.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El Assesment Center seleccionado no posee Competencias relacionadas.", Proceso.Advertencia);
            }

            Panel_HabilidadesAssesment.Visible = false;

            GridView_CompetenciasAssesment.DataSource = null;
            GridView_CompetenciasAssesment.DataBind();
        }
        else
        {
            CargarGrillaCompetenciasDesdeTabla(tablaCompetencias);
        }
    }

    private void Cargar(Decimal ID_ASSESMENT_CENTER)
    {
        HiddenField_ID_ASSESMENT_CENTER.Value = ID_ASSESMENT_CENTER.ToString();

        FabricaAssesment _fabrica = new FabricaAssesment(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaAssesment = _fabrica.ObtenerAssesmentCentePorId(ID_ASSESMENT_CENTER);

        if (tablaAssesment.Rows.Count <= 0)
        {
            if (_fabrica.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _fabrica.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró informacion del Assesment center seleccionado.", Proceso.Error);
            }
        }
        else
        {
            DataRow filaAssesment = tablaAssesment.Rows[0];

            CargarInformacionRegistroControl(filaAssesment);

            CargarInformacionBasica(filaAssesment);

            CargarArchivoAssesment(filaAssesment);

            CargarCompetenciasAssesmentCenter(ID_ASSESMENT_CENTER);
            inhabilitarFilasGrilla(GridView_CompetenciasAssesment, 1);
        }
    }

    protected void GridView_ASSESMENT_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal ID_ASSESMENT_CENTER = Convert.ToDecimal(GridView_ASSESMENT.DataKeys[indexSeleccionado].Values["ID_ASSESMENT_CENTER"]);
            HiddenField_ID_ASSESMENT_CENTER.Value = ID_ASSESMENT_CENTER.ToString();

            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Cargar);

            Cargar(ID_ASSESMENT_CENTER);
        }
    }
    protected void GridView_CompetenciasAssesment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "eliminar")
        {
            DataTable tablaDesdeGrilla = obtenerTablaDeGrillaCompetencias();

            tablaDesdeGrilla.Rows.RemoveAt(indexSeleccionado);

            CargarGrillaCompetenciasDesdeTabla(tablaDesdeGrilla);

            inhabilitarFilasGrilla(GridView_CompetenciasAssesment, 1);

            GridView_CompetenciasAssesment.Columns[0].Visible = true;
            GridView_CompetenciasAssesment.Columns[1].Visible = true;

            HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
            HiddenField_FILA_SELECCIONADA_GRILLA.Value = null;

            Button_NUEVA_COMPETENCIA.Visible = true;
            Button_GUARDAR_COMPETENCIA.Visible = false;
            Button_CANCELAR_COMPETENCIA.Visible = false;
        }
    }

    private DataTable obtenerTablaDeGrillaCompetencias()
    {
        DataTable tablaResultado = new DataTable();
        tablaResultado.Columns.Add("ID_COMPETENCIA_ASSESMENT");
        tablaResultado.Columns.Add("ID_ASSESMENT");
        tablaResultado.Columns.Add("ID_COMPETENCIA");

        DataRow filaTablaResultado;

        for (int i = 0; i < GridView_CompetenciasAssesment.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_CompetenciasAssesment.Rows[i];


            Decimal ID_COMPETENCIA_ASSESMENT = Convert.ToDecimal(GridView_CompetenciasAssesment.DataKeys[i].Values["ID_COMPETENCIA_ASSESMENT"]);

            Decimal ID_ASSESMENT = Convert.ToDecimal(GridView_CompetenciasAssesment.DataKeys[i].Values["ID_ASSESMENT"]);

            DropDownList dropCompetencia = filaGrilla.FindControl("DropDownList_CompetenciaAssesment") as DropDownList;
            Decimal ID_COMPETENCIA = 0;
            if(dropCompetencia.SelectedIndex > 0)
            {
                ID_COMPETENCIA = Convert.ToDecimal(dropCompetencia.SelectedValue);
            }

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_COMPETENCIA_ASSESMENT"] = ID_COMPETENCIA_ASSESMENT;
            filaTablaResultado["ID_ASSESMENT"] = ID_ASSESMENT;
            filaTablaResultado["ID_COMPETENCIA"] = ID_COMPETENCIA;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }

    private void Cargar_DropCompetenciasActivas(DropDownList drop)
    {
        hojasVida _hoja = new hojasVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCompetencias = _hoja.ObtenerCompetenciasActivas();

        if (tablaCompetencias.Rows.Count <= 0)
        {
            if (_hoja.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _hoja.MensajeError, Proceso.Error);
            }
        }

        drop.Items.Clear();

        drop.Items.Add(new ListItem("Seleccione...", ""));

        foreach (DataRow filaTable in tablaCompetencias.Rows)
        {
            drop.Items.Add(new ListItem(filaTable["COMPETENCIA"].ToString(), filaTable["ID_COMPETENCIA"].ToString()));
        }

        drop.DataBind();
    }

    private void CargarGrillaCompetenciasDesdeTabla(DataTable tablaCompetencias)
    {
        GridView_CompetenciasAssesment.DataSource = tablaCompetencias;
        GridView_CompetenciasAssesment.DataBind();

        hojasVida _hoja = new hojasVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        for (int i = 0; i < GridView_CompetenciasAssesment.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_CompetenciasAssesment.Rows[i];
            DataRow filaTabla = tablaCompetencias.Rows[i];

            DropDownList dropComptetencias = filaGrilla.FindControl("DropDownList_CompetenciaAssesment") as DropDownList;
            Cargar_DropCompetenciasActivas(dropComptetencias);

            Label labelDefinicion = filaGrilla.FindControl("Label_DefinicionCompetencia") as Label;
            Label labelArea = filaGrilla.FindControl("Label_AreaCompetencia") as Label;

            if (filaTabla["ID_COMPETENCIA"].ToString() == "0")
            {
                dropComptetencias.SelectedIndex = 0;
                labelArea.Text = "No seleccionada.";
                labelDefinicion.Text = "No seleccionada.";
            }
            else
            {
                DataTable tablaCompetencia = _hoja.ObtenerCompetenciaPorId(Convert.ToDecimal(filaTabla["ID_COMPETENCIA"]));

                DataRow filaCompetencia = tablaCompetencia.Rows[0];

                try
                {
                    dropComptetencias.SelectedValue = filaTabla["ID_COMPETENCIA"].ToString();
                }
                catch
                {
                    dropComptetencias.SelectedIndex = 0;
                }

                labelDefinicion.Text = filaCompetencia["DEFINICION"].ToString();
                labelArea.Text = filaCompetencia["AREA"].ToString();
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

    private void ActivarFilaGrilla(GridView grilla, int fila, int colInicio)
    {
        for (int i = 0; i < grilla.Columns.Count; i++)
        {
            grilla.Rows[fila].Cells[i].Enabled = true;
        }
    }

    protected void Button_NUEVA_COMPETENCIA_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerTablaDeGrillaCompetencias();
        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_COMPETENCIA_ASSESMENT"] = 0;
        filaNueva["ID_ASSESMENT"] = 0;
        if (String.IsNullOrEmpty(HiddenField_ID_ASSESMENT_CENTER.Value) == false)
        {
            filaNueva["ID_ASSESMENT"] = Convert.ToDecimal(HiddenField_ID_ASSESMENT_CENTER.Value);
        }
        filaNueva["ID_COMPETENCIA"] = 0;

        tablaDesdeGrilla.Rows.Add(filaNueva);

        CargarGrillaCompetenciasDesdeTabla(tablaDesdeGrilla);

        inhabilitarFilasGrilla(GridView_CompetenciasAssesment, 1);
        ActivarFilaGrilla(GridView_CompetenciasAssesment, GridView_CompetenciasAssesment.Rows.Count - 1, 1);

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_SELECCIONADA_GRILLA.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();

        GridView_CompetenciasAssesment.Columns[0].Visible = false;

        Button_NUEVA_COMPETENCIA.Visible = false;
        Button_GUARDAR_COMPETENCIA.Visible = true;
        Button_CANCELAR_COMPETENCIA.Visible = true;
    }

    private void inhabilitarFilaGrilla(GridView grilla, int fila, int colInicio)
    {
        for (int j = colInicio; j < grilla.Columns.Count; j++)
        {
            grilla.Rows[fila].Cells[j].Enabled = false;
        }
    }

    protected void Button_GUARDAR_COMPETENCIA_Click(object sender, EventArgs e)
    {
        int FILA_SELECCIONADA = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA.Value);

        inhabilitarFilaGrilla(GridView_CompetenciasAssesment, FILA_SELECCIONADA, 1);

        GridView_CompetenciasAssesment.Columns[0].Visible = true;
        GridView_CompetenciasAssesment.Columns[1].Visible = true;

        Button_GUARDAR_COMPETENCIA.Visible = false;
        Button_CANCELAR_COMPETENCIA.Visible = false;
        Button_NUEVA_COMPETENCIA.Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
    }

    protected void Button_CANCELAR_COMPETENCIA_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = obtenerTablaDeGrillaCompetencias();

        if (HiddenField_ACCION_GRILLA.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }

        CargarGrillaCompetenciasDesdeTabla(tablaGrilla);

        inhabilitarFilasGrilla(GridView_CompetenciasAssesment, 1);

        GridView_CompetenciasAssesment.Columns[0].Visible = true;
        GridView_CompetenciasAssesment.Columns[1].Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();

        Button_NUEVA_COMPETENCIA.Visible = true;
        Button_GUARDAR_COMPETENCIA.Visible = false;
        Button_CANCELAR_COMPETENCIA.Visible = false;
    }
    protected void DropDownList_CompetenciaAssesment_SelectedIndexChanged(object sender, EventArgs e)
    {
        Int32 index = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA.Value);

        DropDownList drop = GridView_CompetenciasAssesment.Rows[index].FindControl("DropDownList_CompetenciaAssesment") as DropDownList;
        Label labelDefinicion = GridView_CompetenciasAssesment.Rows[index].FindControl("Label_DefinicionCompetencia") as Label ;
        Label labelArea = GridView_CompetenciasAssesment.Rows[index].FindControl("Label_AreaCompetencia") as Label;

        if (drop.SelectedIndex <= 0)
        {
            labelDefinicion.Text = "No seleccionado.";
            labelArea.Text = "No seleccionado.";
        }
        else
        { 
            hojasVida _hoja = new hojasVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            Decimal ID_COMPETENCIA = Convert.ToDecimal(drop.SelectedValue);

            DataTable tablaCompetencia = _hoja.ObtenerCompetenciaPorId(ID_COMPETENCIA);
            DataRow filaCompetencia = tablaCompetencia.Rows[0];

            labelDefinicion.Text = filaCompetencia["DEFINICION"].ToString();
            labelArea.Text = filaCompetencia["AREA"].ToString();
        }
    }
}