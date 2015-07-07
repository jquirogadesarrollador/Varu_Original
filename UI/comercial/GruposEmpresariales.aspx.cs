using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brainsbits.LLB.comercial;
using System.Data;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;
using TSHAK.Components;
using Brainsbits.LLB;

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
        NuevoGrupo = 1, 
        CargarGrupo = 2,
        ModificarGrupo = 3
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
        Modificar = 1,
        Ninguna = 2
    }

    private void configurar_paneles_popup(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_mensaje.CssClass = "panel_popup";
    }

    private void Configurar()
    {
        configurar_paneles_popup(Panel_FONDO_MENSAJE, Panel_MENSAJES);

        configurar_paneles_popup(Panel_FONDO_MENSAJE_EMPRESAS, Panel_MENSAJE_EMRESAS);
    }

    private void Ocultar(Acciones accion)
    {
        switch (accion)
        { 
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = false;
                
                Panel_FORM_BOTONES_ENCABEZADO.Visible = false;
                Button_NUEVO.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_CANCELAR.Visible = false;

                Panel_RESULTADOS_GRID.Visible = false;

                Panel_FORMULARIO.Visible = false;
                Panel_CONTROL_REGISTRO.Visible = false;
                Panel_DATOS.Visible = false;
                Panel_EMPRESAS_ASOCIADAS_AL_GRUPO.Visible = false;
                Button_NUEVO_EMPRESA.Visible = false;
                Button_GUARDAR_EMPRESA.Visible = false;
                Button_CANCELAR_EMPRESA.Visible = false;

                Panel_FORM_BOTONES_PIE.Visible = false;
                Button_NUEVO_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;

                GridView_EMPRESAS_GRUPO.Columns[0].Visible = false;
                break;
            case Acciones.ModificarGrupo:
                Button_NUEVO.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_CANCELAR.Visible = false;

                Panel_CONTROL_REGISTRO.Visible = false;

                Button_NUEVO_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;

                Button_NUEVO_EMPRESA.Visible = false;
                Button_GUARDAR_EMPRESA.Visible = false;
                Button_CANCELAR_EMPRESA.Visible = false;

                GridView_EMPRESAS_GRUPO.Columns[0].Visible = false;
                break;
        }
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Button_NUEVO.Visible = true;
                Button_NUEVO_1.Visible = true;

                Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.NuevoGrupo:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_FORMULARIO.Visible = true;
                Panel_DATOS.Visible = true;
                Panel_EMPRESAS_ASOCIADAS_AL_GRUPO.Visible = true;

                Button_NUEVO_EMPRESA.Visible = true;

                Panel_FORM_BOTONES_PIE.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;

                GridView_EMPRESAS_GRUPO.Columns[0].Visible = true;
                break;
            case Acciones.CargarGrupo:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Button_NUEVO.Visible = true;
                Button_MODIFICAR.Visible = true;
                
                Panel_FORMULARIO.Visible = true;
                Panel_CONTROL_REGISTRO.Visible = true;
                Panel_DATOS.Visible = true;

                Panel_EMPRESAS_ASOCIADAS_AL_GRUPO.Visible = true;

                Panel_FORM_BOTONES_PIE.Visible = true;
                Button_NUEVO_1.Visible = true;
                Button_MODIFICAR_1.Visible = true;
                break;
            case Acciones.ModificarGrupo:
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;

                Panel_EMPRESAS_ASOCIADAS_AL_GRUPO.Visible = true;
                Button_NUEVO_EMPRESA.Visible = true;
                GridView_EMPRESAS_GRUPO.Columns[0].Visible = true;                
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

                TextBox_NOMBRE_GRUPO.Enabled = false;

                break;
        }
    }

    private void cargar_DropDownList_BUSCAR()
    {
        DropDownList_BUSCAR.Items.Clear();

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_BUSCAR.Items.Add(item);

        item = new ListItem("Nombre", "NOMBRE");
        DropDownList_BUSCAR.Items.Add(item);

        DropDownList_BUSCAR.DataBind();
    }

    private void configurarCaracteresAceptadosBusqueda(Boolean letras, Boolean numeros)
    {
        FilteredTextBoxExtender_TextBox_BUSCAR_Numbers.Enabled = numeros;
        FilteredTextBoxExtender_TextBox_BUSCAR_Letras.Enabled = letras;
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

    private void cargar_GridView_RESULTADOS_BUSQUEDA_con_todos_lod_grupos_actuales()
    {
        grupoEmpresarial _grupoEmpresarial = new grupoEmpresarial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaDatos = _grupoEmpresarial.ObtenerTodosLosGruposEmpresariales();

        if (tablaDatos.Rows.Count <= 0)
        {
            if (_grupoEmpresarial.MensajeError == null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros en la busqueda de Grupos Empresariales.", Proceso.Advertencia);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _grupoEmpresarial.MensajeError, Proceso.Error);
            }

            Panel_RESULTADOS_GRID.Visible = false;
        }
        else
        {
            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaDatos;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
        }
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                this.Title = "GRUPOS EMPRESARIALES";

                cargar_DropDownList_BUSCAR();
                configurarCaracteresAceptadosBusqueda(true, true);

                HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "SIN_FILTRO";
                HiddenField_FILTRO_DROP.Value = String.Empty;
                HiddenField_FILTRO_DATO.Value = String.Empty;

                cargar_GridView_RESULTADOS_BUSQUEDA_con_todos_lod_grupos_actuales();
                break;
            case Acciones.NuevoGrupo:
                this.Title = "NUEVO GRUPO EMPRESARIAL";
                HiddenField_ID_GRUPOEMPRESARIAL.Value = "";
                TextBox_NOMBRE_GRUPO.Text = "";

                GridView_EMPRESAS_GRUPO.DataSource = null;
                GridView_EMPRESAS_GRUPO.DataBind();

                HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_FILA_SELECCIONADA_GRILLA.Value = "";
                HiddenField_ID_EMPRESA.Value = "";
                break;
            case Acciones.ModificarGrupo:
                HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_FILA_SELECCIONADA_GRILLA.Value = "";
                HiddenField_ID_EMPRESA.Value = "";
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
    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    private void Activar(Acciones accion)
    {
        switch (accion)
        { 
            case Acciones.NuevoGrupo:
                TextBox_NOMBRE_GRUPO.Enabled = true;
                break;
            case Acciones.ModificarGrupo:

                break;
        }
    }

    protected void Button_NUEVO_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.NuevoGrupo);
        Activar(Acciones.NuevoGrupo);
        Cargar(Acciones.NuevoGrupo);
    }
    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.ModificarGrupo);
        Mostrar(Acciones.ModificarGrupo);
        Activar(Acciones.ModificarGrupo);
        Cargar(Acciones.ModificarGrupo);
    }

    private void cargar_registro_control_basico(DataRow filaInfoGrupo)
    {
        TextBox_USU_CRE.Text = filaInfoGrupo["USU_CRE"].ToString();
        try
        {
            TextBox_FCH_CRE.Text = DateTime.Parse(filaInfoGrupo["FCH_CRE"].ToString()).ToShortDateString();
            TextBox_HOR_CRE.Text = DateTime.Parse(filaInfoGrupo["FCH_CRE"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_CRE.Text = "";
            TextBox_HOR_CRE.Text = "";
        }
        TextBox_USU_MOD.Text = filaInfoGrupo["USU_MOD"].ToString();
        try
        {
            TextBox_FCH_MOD.Text = DateTime.Parse(filaInfoGrupo["FCH_MOD"].ToString()).ToShortDateString();
            TextBox_HOR_MOD.Text = DateTime.Parse(filaInfoGrupo["FCH_MOD"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_MOD.Text = "";
            TextBox_HOR_MOD.Text = "";
        }
    }

    private void cargar_empresas_asociadas_a_grupo(Decimal ID_GRUPOEMPRESARIAL)
    {
        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaEmpresas = _cliente.ObtenerEmpresasAsociadasAGrupo(ID_GRUPOEMPRESARIAL);

        if (tablaEmpresas.Rows.Count <= 0)
        {
            if (_cliente.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _cliente.MensajeError, Proceso.Error);
            }

            Panel_EMPRESAS_ASOCIADAS_AL_GRUPO.Visible = false;
        }
        else
        {
            cargar_GridView_EMPRESAS_GRUPO_desde_tabal(tablaEmpresas);
        }
    }

    private void Cargar(Decimal ID_GRUPOEMPRESARIL)
    {
        Ocultar(Acciones.Inicio);

        grupoEmpresarial _grupoEmpresarial = new grupoEmpresarial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaInfoGrupo = _grupoEmpresarial.ObtenerGruposEmpresarialesPorId(ID_GRUPOEMPRESARIL);

        if (tablaInfoGrupo.Rows.Count <= 0)
        {
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);

            if (_grupoEmpresarial.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _grupoEmpresarial.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró un  grupo empresarial con el id especificado.", Proceso.Error);
            }
        }
        else
        {
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.CargarGrupo);

            DataRow filaInfoGrupo = tablaInfoGrupo.Rows[0];

            cargar_registro_control_basico(filaInfoGrupo);

            HiddenField_ID_GRUPOEMPRESARIAL.Value = filaInfoGrupo["ID_GRUPOEMPRESARIAL"].ToString();
            TextBox_NOMBRE_GRUPO.Text = filaInfoGrupo["NOMBRE"].ToString().Trim();

            this.Title = "GRUPO: " + filaInfoGrupo["NOMBRE"].ToString().Trim();
            HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
            HiddenField_FILA_SELECCIONADA_GRILLA.Value = "";
            HiddenField_ID_EMPRESA.Value = "";

            cargar_empresas_asociadas_a_grupo(Convert.ToDecimal(filaInfoGrupo["ID_GRUPOEMPRESARIAL"]));
        }
    }

    private void Guardar()
    {
        String NOMBRE = TextBox_NOMBRE_GRUPO.Text.Trim();

        List<grupoEmpresarial> listaEmpresas = new List<grupoEmpresarial>();
        grupoEmpresarial _grupoEmpresarialParaLista;
        DropDownList datoDrop;

        for (int i = 0; i < GridView_EMPRESAS_GRUPO.Rows.Count; i++)
        {
            _grupoEmpresarialParaLista = new grupoEmpresarial();

            datoDrop = GridView_EMPRESAS_GRUPO.Rows[i].FindControl("DropDownList_ID_EMPRESA") as DropDownList;
            _grupoEmpresarialParaLista.ID_EMPRESA = Convert.ToDecimal(datoDrop.SelectedValue);

            listaEmpresas.Add(_grupoEmpresarialParaLista);
        }

        grupoEmpresarial _grupoEmpresarial = new grupoEmpresarial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Decimal ID_GRUPOEMPRESARIAL = _grupoEmpresarial.AdicionarGrupoEmpresarialConEmpresas(NOMBRE, listaEmpresas);


        if (ID_GRUPOEMPRESARIAL <= 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _grupoEmpresarial.MensajeError, Proceso.Error);
        }
        else
        {
            Cargar(ID_GRUPOEMPRESARIAL);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Proceso correcto, El grupo empresarial: " + NOMBRE + " se creó satisfactoriamente, y se le asignó el ID: " + ID_GRUPOEMPRESARIAL.ToString(), Proceso.Correcto);
        }
    }

    private void Modificar()
    {
        Decimal ID_GRUPOEMPRESARIAL = Convert.ToDecimal(HiddenField_ID_GRUPOEMPRESARIAL.Value);
        String NOMBRE = TextBox_NOMBRE_GRUPO.Text.Trim();

        List<grupoEmpresarial> listaEmpresas = new List<grupoEmpresarial>();
        grupoEmpresarial _grupoEmpresarialParaLista;
        DropDownList datoDrop;

        for (int i = 0; i < GridView_EMPRESAS_GRUPO.Rows.Count; i++)
        {
            _grupoEmpresarialParaLista = new grupoEmpresarial();

            datoDrop = GridView_EMPRESAS_GRUPO.Rows[i].FindControl("DropDownList_ID_EMPRESA") as DropDownList;
            _grupoEmpresarialParaLista.ID_EMPRESA = Convert.ToDecimal(datoDrop.SelectedValue);

            listaEmpresas.Add(_grupoEmpresarialParaLista);
        }

        grupoEmpresarial _grupoEmpresarial = new grupoEmpresarial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean correcto = _grupoEmpresarial.ActualizarGrupoEmpresarialConEMpresas(ID_GRUPOEMPRESARIAL, NOMBRE, listaEmpresas);

        if (correcto == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _grupoEmpresarial.MensajeError, Proceso.Error);
        }
        else
        {
            Cargar(ID_GRUPOEMPRESARIAL);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Proceso correcto, El grupo empresarial: " + NOMBRE + " se Modificó satisfactoriamente..", Proceso.Correcto);
        }
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        Boolean correcto = true;

        if (HiddenField_ACCION_GRILLA.Value != AccionesGrilla.Ninguna.ToString())
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Para poder continuar primero debe terminar las acciones sobre la grilla de EMPRESAS INCLUIDAS EN EL GRUPO EMPRESARIAL.", Proceso.Advertencia);
            correcto = false;
        }

        if (correcto == true)
        {
            if (HiddenField_ID_GRUPOEMPRESARIAL.Value == "")
            {
                Guardar();
            }
            else
            { 
                Modificar();
            }
        }
    }
    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }
    protected void DropDownList_BUSCAR_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_BUSCAR.SelectedValue == "")
        {
            configurarCaracteresAceptadosBusqueda(true, true);
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "NOMBRE")
            {
                configurarCaracteresAceptadosBusqueda(true, false);
            }
        }
        TextBox_BUSCAR.Text = "";
    }

    private void Buscar()
    {
        grupoEmpresarial _grupoEmpresarial = new grupoEmpresarial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable _dataTable = new DataTable();

        switch (HiddenField_FILTRO_DROP.Value)
        {
            case "NOMBRE":
                _dataTable = _grupoEmpresarial.ObtenerTodosLosGruposEmpresarialesPorNombre(HiddenField_FILTRO_DATO.Value);
                break;
        }

        if (_dataTable.Rows.Count > 0)
        {
            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);

            GridView_RESULTADOS_BUSQUEDA.DataSource = _dataTable;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
        }
        else
        {
            if (!String.IsNullOrEmpty(_grupoEmpresarial.MensajeError))
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Consulte con el Administrador: " + _grupoEmpresarial.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información para mostrar.", Proceso.Advertencia);
            }

            Panel_RESULTADOS_GRID.Visible = false;
        }
        if (_dataTable.Rows.Count > 0)
        {
            _dataTable.Dispose();
        }
    }

    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "CON_FILTRO";
        HiddenField_FILTRO_DROP.Value = DropDownList_BUSCAR.SelectedValue;
        HiddenField_FILTRO_DATO.Value = TextBox_BUSCAR.Text;

        Buscar();

        this.Title = "BUSQUEDA DE GRUPOS EMPRESARIALES";
    }
    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;

        if (HiddenField_TIPO_BUSQUEDA_ACTUAL.Value == "SIN_FILTRO")
        {
            cargar_GridView_RESULTADOS_BUSQUEDA_con_todos_lod_grupos_actuales();
        }
        else
        {
            Buscar();
        }
    }
    protected void GridView_RESULTADOS_BUSQUEDA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal ID_GRUPO_EMPRESARIAL = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_GRUPOEMPRESARIAL"]);

            Cargar(ID_GRUPO_EMPRESARIAL);
        }
    }

    private void ocultar_mensaje(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.Style.Add("display", "none");
        panel_mensaje.Style.Add("display", "none");

        panel_fondo.Visible = false;
        panel_mensaje.Visible = false;
    }

    protected void Button_MENSAJE_EMPRESAS_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE_EMPRESAS, Panel_MENSAJE_EMRESAS);
    }
    protected void GridView_EMPRESAS_GRUPO_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "eliminar")
        {
            DataTable tablaDesdeGrilla = obtenerDataTable_De_GridView_EMPRESAS_GRUPO();

            tablaDesdeGrilla.Rows.RemoveAt(indexSeleccionado);

            GridViewRow filaGrilla;

            cargar_GridView_EMPRESAS_GRUPO_desde_tabal(tablaDesdeGrilla);

            for (int i = 0; i < GridView_EMPRESAS_GRUPO.Rows.Count; i++)
            {
                filaGrilla = GridView_EMPRESAS_GRUPO.Rows[i];

                for (int j = 1; j < GridView_EMPRESAS_GRUPO.Columns.Count; j++)
                {
                    filaGrilla.Cells[j].Enabled = false;
                }
            }

            GridView_EMPRESAS_GRUPO.Columns[0].Visible = true;

            HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
            HiddenField_FILA_SELECCIONADA_GRILLA.Value = null;
            HiddenField_ID_EMPRESA.Value = "";

            Button_NUEVO_EMPRESA.Visible = true;
            Button_GUARDAR_EMPRESA.Visible = false;
            Button_CANCELAR_EMPRESA.Visible = false;
        }
    }

    private DataTable obtenerDataTable_De_GridView_EMPRESAS_GRUPO()
    {
        DataTable tablaResultado = new DataTable();
        tablaResultado.Columns.Add("ID_EMPRESA");

        DataRow filaTablaResultado;

        Decimal ID_EMPRESA = 0;

        GridViewRow filaGrilla;

        DropDownList datoDrop;

        for (int i = 0; i < GridView_EMPRESAS_GRUPO.Rows.Count; i++)
        {
            filaGrilla = GridView_EMPRESAS_GRUPO.Rows[i];

            datoDrop = filaGrilla.FindControl("DropDownList_ID_EMPRESA") as DropDownList;

            try
            {
                ID_EMPRESA = Convert.ToDecimal(datoDrop.SelectedValue);
            }
            catch
            {
                ID_EMPRESA = 0;
            }

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_EMPRESA"] = ID_EMPRESA;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }

    private void cargar_ListaEmpresasEnDrop(DropDownList drop)
    {
        drop.Items.Clear();

        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        ListItem item = new ListItem("Seleccione...", "");
        drop.Items.Add(item);

        DataTable tablaClientes = _cliente.ObtenerTodasLasEmpresasActivas();

        foreach (DataRow fila in tablaClientes.Rows)
        {
            item = new ListItem(fila["RAZ_SOCIAL"].ToString(), fila["ID_EMPRESA"].ToString());
            drop.Items.Add(item);
        }

        drop.DataBind();
    }

    private void cargar_GridView_EMPRESAS_GRUPO_desde_tabal(DataTable tablaInfoEmpresas)
    {
        GridView_EMPRESAS_GRUPO.DataSource = tablaInfoEmpresas;
        GridView_EMPRESAS_GRUPO.DataBind();

        GridViewRow filaGrilla;
        Decimal ID_EMPRESA;
        DropDownList datoDrop;

        for (int i = 0; i < GridView_EMPRESAS_GRUPO.Rows.Count; i++)
        {
            filaGrilla = GridView_EMPRESAS_GRUPO.Rows[i];

            ID_EMPRESA = Convert.ToDecimal(GridView_EMPRESAS_GRUPO.DataKeys[i].Values["ID_EMPRESA"]);

            datoDrop = filaGrilla.FindControl("DropDownList_ID_EMPRESA") as DropDownList;

            cargar_ListaEmpresasEnDrop(datoDrop);

            try
            {
                datoDrop.SelectedValue = ID_EMPRESA.ToString();
            }
            catch
            {
                datoDrop.SelectedIndex = 0;
            }

            for (int j = 1; j < GridView_EMPRESAS_GRUPO.Columns.Count; j++)
            {
                filaGrilla.Cells[j].Enabled = false;
            }
        }
    }

    protected void Button_NUEVO_EMPRESA_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerDataTable_De_GridView_EMPRESAS_GRUPO();

        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_EMPRESA"] = 0;

        tablaDesdeGrilla.Rows.Add(filaNueva);

        GridViewRow filaGrilla;

        cargar_GridView_EMPRESAS_GRUPO_desde_tabal(tablaDesdeGrilla);

        for (int i = 0; i < GridView_EMPRESAS_GRUPO.Rows.Count; i++)
        {
            filaGrilla = GridView_EMPRESAS_GRUPO.Rows[i];

            if (i == (GridView_EMPRESAS_GRUPO.Rows.Count - 1))
            {
                for (int j = 1; j < GridView_EMPRESAS_GRUPO.Columns.Count; j++)
                {
                    filaGrilla.Cells[j].Enabled = true;
                }
            }
            else
            {
                for (int j = 1; j < GridView_EMPRESAS_GRUPO.Columns.Count; j++)
                {
                    filaGrilla.Cells[j].Enabled = false;
                }
            }
        }

        HiddenField_ID_EMPRESA.Value = "";

        GridView_EMPRESAS_GRUPO.Columns[0].Visible = false;

        Button_NUEVO_EMPRESA.Visible = false;
        Button_GUARDAR_EMPRESA.Visible = true;
        Button_CANCELAR_EMPRESA.Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_SELECCIONADA_GRILLA.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();
    }
    protected void Button_GUARDAR_EMPRESA_Click(object sender, EventArgs e)
    {
        int FILA_SELECCIONADA = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA.Value);

        for (int j = 1; j < GridView_EMPRESAS_GRUPO.Columns.Count; j++)
        {
            GridView_EMPRESAS_GRUPO.Rows[FILA_SELECCIONADA].Cells[j].Enabled = false;
        }

        GridView_EMPRESAS_GRUPO.Columns[0].Visible = true;

        Button_GUARDAR_EMPRESA.Visible = false;
        Button_CANCELAR_EMPRESA.Visible = false;
        Button_NUEVO_EMPRESA.Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
    }
    protected void Button_CANCELAR_EMPRESA_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = obtenerDataTable_De_GridView_EMPRESAS_GRUPO();

        if (HiddenField_ACCION_GRILLA.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }

        cargar_GridView_EMPRESAS_GRUPO_desde_tabal(tablaGrilla);

        foreach (GridViewRow fila in GridView_EMPRESAS_GRUPO.Rows)
        {
            for (int j = 1; j < GridView_EMPRESAS_GRUPO.Columns.Count; j++)
            {
                fila.Cells[j].Enabled = false;
            }
        }

        GridView_EMPRESAS_GRUPO.Columns[0].Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();

        Button_NUEVO_EMPRESA.Visible = true;
        Button_GUARDAR_EMPRESA.Visible = false;
        Button_CANCELAR_EMPRESA.Visible = false;
    }

    protected void DropDownList_ID_EMPRESA_SelectedIndexChanged(object sender, EventArgs e)
    {
        Decimal ID_EMPRESA_SELECCIONADA = 0;
        Decimal ID_EMPRESA_INSPECCIONADA = 0;
        Boolean empresaYaIncluida = false;

        GridViewRow filaGrillaSeleccionada;

        if (GridView_EMPRESAS_GRUPO.Rows.Count > 1)
        {
            filaGrillaSeleccionada = GridView_EMPRESAS_GRUPO.Rows[GridView_EMPRESAS_GRUPO.Rows.Count - 1];

            DropDownList datoDropSeleccionado = filaGrillaSeleccionada.FindControl("DropDownList_ID_EMPRESA") as DropDownList;
            ID_EMPRESA_SELECCIONADA = Convert.ToDecimal(datoDropSeleccionado.SelectedValue);

            GridViewRow filaGrilla;

            for (int i = 0; i < (GridView_EMPRESAS_GRUPO.Rows.Count - 1); i++)
            {
                filaGrilla = GridView_EMPRESAS_GRUPO.Rows[i];

                ID_EMPRESA_INSPECCIONADA = Convert.ToDecimal(GridView_EMPRESAS_GRUPO.DataKeys[i].Values["ID_EMPRESA"]);

                if (ID_EMPRESA_INSPECCIONADA == ID_EMPRESA_SELECCIONADA)
                {
                    Informar(Panel_FONDO_MENSAJE_EMPRESAS, Image_MENSAJE_EMPRESAS_POPUP, Panel_MENSAJE_EMRESAS, Label_MENSAJE_EMPRESAS, "La Empresa Seleciconada ya se encuentra en la lista.", Proceso.Advertencia);

                    empresaYaIncluida = true;

                    break;
                }
            }

            if (empresaYaIncluida == true)
            {
                datoDropSeleccionado.SelectedIndex = 0;
            }
        }
    }
}