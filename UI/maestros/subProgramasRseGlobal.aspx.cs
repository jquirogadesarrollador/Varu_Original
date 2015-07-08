using System;
using System.Web.UI.WebControls;
using Brainsbits.LLB.maestras;
using TSHAK.Components;
using Brainsbits.LLB;
using Brainsbits.LLB.programasRseGlobal;
using System.Data;
using Brainsbits.LLB.seguridad;
using System.Web;

public partial class _subProgramas : System.Web.UI.Page
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
        Nuevo,
        Cargar,
        Modificar
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
                Button_NUEVO.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_CANCELAR.Visible = false;

                Panel_RESULTADOS_GRID.Visible = false;

                Panel_InformacionSubPrograma.Visible = false;
                Panel_CONTROL_REGISTRO.Visible = false;
                Panel_SubPrograma.Visible = false;
                Panel_Estado.Visible = false;
                break;
            case Acciones.Modificar:
                Button_NUEVO.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_CANCELAR.Visible = false;

                Panel_CONTROL_REGISTRO.Visible = false;

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

                TextBox_Nombre.Enabled = false;
                DropDownList_EstadoSubPrograma.Enabled = false;
                TextBox_Descripcion.Enabled = false;
                break;
        }
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_FORM_BOTONES.Visible = true;
                Button_NUEVO.Visible = true;

                Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.Nuevo:
                Panel_FORM_BOTONES.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_InformacionSubPrograma.Visible = true;
                Panel_SubPrograma.Visible = true;
                break;
            case Acciones.Cargar:
                Panel_FORM_BOTONES.Visible = true;
                Button_MODIFICAR.Visible = true;
                Button_NUEVO.Visible = true;

                Panel_InformacionSubPrograma.Visible = true;
                Panel_CONTROL_REGISTRO.Visible = true;
                Panel_SubPrograma.Visible = true;
                Panel_Estado.Visible = true;

                break;
            case Acciones.Modificar:
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

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

    private void cargar_GridView_RESULTADOS_BUSQUEDA()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Programa.Areas AREA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);

        SubPrograma _subPrograma = new SubPrograma(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaSubProgramas = _subPrograma.ObtenerSubProgramasPorArea(AREA);

        if (tablaSubProgramas.Rows.Count <= 0)
        {
            if (_subPrograma.MensajeError == null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros de Sub Programas.", Proceso.Advertencia);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _subPrograma.MensajeError, Proceso.Error);
            }

            Panel_RESULTADOS_GRID.Visible = false;
        }
        else
        {
            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaSubProgramas;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
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

        item = new ListItem("Id Sub Programa", "ID_SUB_PROGRAMA");
        DropDownList_BUSCAR.Items.Add(item);

        item = new ListItem("Nombre", "NOMBRE");
        DropDownList_BUSCAR.Items.Add(item);

        DropDownList_BUSCAR.DataBind();
    }

    private void iniciar_seccion_de_busqueda()
    {
        TextBox_BUSCAR.Text = "";
        cargar_DropDownList_BUSCAR();
    }

    private void ConfigurarAreaRseGlobal()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        int proceso = Convert.ToInt32(QueryStringSeguro["proceso"]);

        HiddenField_ID_AREA.Value = _tools.ObtenerIdAreaProceso(proceso);
    }

    private void Cargar_DropDownList_EstadoSubPrograma(DropDownList drop)
    {
        drop.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_ESTADO_SUB_PROGRAMA);

        ListItem item = new ListItem("Seleccione...", "");
        drop.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            drop.Items.Add(item);
        }
        drop.DataBind();
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                configurarCaracteresAceptadosBusqueda(true, true);

                ConfigurarAreaRseGlobal();

                HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "INICIAL";
                HiddenField_FILTRO_DROP.Value = String.Empty;
                HiddenField_FILTRO_DATO.Value = String.Empty;

                iniciar_seccion_de_busqueda();

                cargar_GridView_RESULTADOS_BUSQUEDA();
                break;
            case Acciones.Cargar:
                Cargar_DropDownList_EstadoSubPrograma(DropDownList_EstadoSubPrograma);
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

    protected void DropDownList_BUSCAR_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_BUSCAR.SelectedValue == "")
        {
            configurarCaracteresAceptadosBusqueda(true, true);
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "ID_SUB_PROGRAMA")
            {
                configurarCaracteresAceptadosBusqueda(false, true);
            }
            else
            {
                if (DropDownList_BUSCAR.SelectedValue == "NOMBRE")
                {
                    configurarCaracteresAceptadosBusqueda(true, false);
                }
            }
        }

        TextBox_BUSCAR.Text = "";
    }
    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    private void Activar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Nuevo:
                TextBox_Nombre.Enabled = true;
                TextBox_Descripcion.Enabled = true;
                break;
            case Acciones.Modificar:
                TextBox_Nombre.Enabled = true;
                DropDownList_EstadoSubPrograma.Enabled = true;
                TextBox_Descripcion.Enabled = true;
                break;
        }
    }

    private void Limpiar(Acciones accion)
    {
        HiddenField_ID_SUB_PROGRAMA.Value = "";

        switch (accion)
        {
            case Acciones.Nuevo:
                TextBox_Nombre.Text = "";
                TextBox_Descripcion.Text = "";
                break;
        }
    }

    protected void Button_NUEVO_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Nuevo);
        Desactivar(Acciones.Inicio);
        Activar(Acciones.Nuevo);
        Limpiar(Acciones.Nuevo);
    }
    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Modificar);
        Mostrar(Acciones.Modificar);
        Activar(Acciones.Modificar);
    }

    private void Guardar()
    {
        String NOMBRE = TextBox_Nombre.Text.Trim();
        String DESCRIPCION = TextBox_Descripcion.Text.Trim();

        Programa.Areas AREA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);

        SubPrograma _subprograma = new SubPrograma(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Decimal ID_SUB_PROGRAMA = _subprograma.Adicionar(NOMBRE, DESCRIPCION, AREA);

        if (ID_SUB_PROGRAMA <= 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE,  _subprograma.MensajeError, Proceso.Error);
        }
        else
        {
            Cargar(ID_SUB_PROGRAMA);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El Sub Programa " + NOMBRE + " fue creado correctamente y se le asignó el ID: " + ID_SUB_PROGRAMA.ToString(), Proceso.Correcto);
        }
    }

    private void Actualizar()
    {
        Decimal ID_SUB_PROGRAMA = Convert.ToDecimal(HiddenField_ID_SUB_PROGRAMA.Value);

        String NOMBRE = TextBox_Nombre.Text.Trim();
        String DESCRIPCION = TextBox_Descripcion.Text.Trim();

        Boolean ACTIVO = true;

        if (DropDownList_EstadoSubPrograma.SelectedValue == "False")
        {
            ACTIVO = false;   
        }

        SubPrograma _subprograma = new SubPrograma(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean verificado = _subprograma.Actualizar(ID_SUB_PROGRAMA, NOMBRE, DESCRIPCION, ACTIVO);

        if (verificado == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _subprograma.MensajeError, Proceso.Error);
        }
        else
        {
            Cargar(ID_SUB_PROGRAMA);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El Sub Programa " + NOMBRE + " fue creado Actualizado correctamente.", Proceso.Correcto);
        }
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(HiddenField_ID_SUB_PROGRAMA.Value) == true)
        {
            Guardar();
        }
        else
        {
            Actualizar();
        }
    }
    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(HiddenField_ID_SUB_PROGRAMA.Value) == true)
        {
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);
        }
        else
        {
            Decimal ID_SUB_PROGRAMA = Convert.ToDecimal(HiddenField_ID_SUB_PROGRAMA.Value);

            Cargar(ID_SUB_PROGRAMA);
        }
    }

    private void Buscar()
    {
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);

        String datosCapturados = HiddenField_FILTRO_DATO.Value;
        String campo = HiddenField_FILTRO_DROP.Value;

        Programa.Areas AREA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);

        SubPrograma _SubPrograma = new SubPrograma(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaResultadosBusqueda = new DataTable();

        if (campo == "ID_SUB_PROGRAMA")
        {
            tablaResultadosBusqueda = _SubPrograma.ObtenerSubProgramasPorId(Convert.ToDecimal(datosCapturados), AREA);
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "NOMBRE")
            {
                tablaResultadosBusqueda = _SubPrograma.ObtenerSubProgramasPorNombre(datosCapturados, AREA);
            }
        }

        if (tablaResultadosBusqueda.Rows.Count <= 0)
        {
            if (_SubPrograma.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _SubPrograma.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros que cumplieran los datos de busqueda.", Proceso.Advertencia);
            }

            Panel_RESULTADOS_GRID.Visible = false;
        }
        else
        {
            Panel_RESULTADOS_GRID.Visible = true;

            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaResultadosBusqueda;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
        }
    }

    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "CON_FILTRO";

        HiddenField_FILTRO_DATO.Value = TextBox_BUSCAR.Text.Trim();
        HiddenField_FILTRO_DROP.Value = DropDownList_BUSCAR.SelectedValue;

        Buscar();
    }
    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;

        if (HiddenField_TIPO_BUSQUEDA_ACTUAL.Value == "INICIAL")
        {
            cargar_GridView_RESULTADOS_BUSQUEDA();
        }
        else
        {
            Buscar();
        }
    }

    private void CargarControlRegistro(DataRow filaSubPrograma)
    {
        TextBox_USU_CRE.Text = filaSubPrograma["USU_CRE"].ToString();
        try
        {
            TextBox_FCH_CRE.Text = DateTime.Parse(filaSubPrograma["FCH_CRE"].ToString()).ToShortDateString();
            TextBox_HOR_CRE.Text = DateTime.Parse(filaSubPrograma["FCH_CRE"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_CRE.Text = "";
            TextBox_HOR_CRE.Text = "";
        }
        TextBox_USU_MOD.Text = filaSubPrograma["USU_MOD"].ToString();
        try
        {
            TextBox_FCH_MOD.Text = DateTime.Parse(filaSubPrograma["FCH_MOD"].ToString()).ToShortDateString();
            TextBox_HOR_MOD.Text = DateTime.Parse(filaSubPrograma["FCH_MOD"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_MOD.Text = "";
            TextBox_HOR_MOD.Text = "";
        }
    }

    private void CargarInformacionSubprograma(DataRow filaSubprograma)
    {
        TextBox_Nombre.Text = filaSubprograma["NOMBRE"].ToString().Trim();

        DropDownList_EstadoSubPrograma.SelectedValue = filaSubprograma["ACTIVO"].ToString().Trim();

        TextBox_Descripcion.Text = filaSubprograma["DESCRIPCION"].ToString().Trim();
    }

    private void Cargar(Decimal ID_SUB_PROGRAMA)
    {
        HiddenField_ID_SUB_PROGRAMA.Value = ID_SUB_PROGRAMA.ToString();

        Programa.Areas AREA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);

        SubPrograma _subPrograma = new  SubPrograma(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaSubprograma = _subPrograma.ObtenerSubProgramasPorId(ID_SUB_PROGRAMA, AREA);

        if (tablaSubprograma.Rows.Count <= 0)
        {
            if (String.IsNullOrEmpty(_subPrograma.MensajeError) == false)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _subPrograma.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información del Sub Programa seleccionado", Proceso.Error);
            }
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Cargar);
            Cargar(Acciones.Cargar);

            DataRow filaSubprograma = tablaSubprograma.Rows[0];

            CargarControlRegistro(filaSubprograma);

            CargarInformacionSubprograma(filaSubprograma);
        }
    }

    protected void GridView_RESULTADOS_BUSQUEDA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal ID_SUB_PROGRAMA = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_SUB_PROGRAMA"]);

            Cargar(ID_SUB_PROGRAMA);
        }
    }
}