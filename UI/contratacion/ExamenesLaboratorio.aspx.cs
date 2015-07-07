using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brainsbits.LLB.almacen;
using System.Data;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;
using TSHAK.Components;
using Brainsbits.LLB;

public partial class _Default : System.Web.UI.Page
{

    private String NOMBRE_AREA = tabla.NOMBRE_AREA_CONTRATACION;


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
        CargarProv = 1, 
        NuevoExamen = 2
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }

    private enum tiposBusqueda
    { 
        SIN_FILTRO = 0,
        CON_FILTRO
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
                Button_CANCELAR.Visible = false;

                Panel_MENSAJES.Visible = false;

                Panel_RESULTADOS_GRID.Visible = false;

                Panel_DATOS_PROVEEDOR_SELECCIONADO.Visible = false;

                Panel_FORMULARIO.Visible = false;

                Panel_EXAMENES_ASOCIADOS_ACTUALMENTE.Visible = false;

                Panel_NUEVO_EXAMEN_PARA_PROVEEDOR.Visible = false;

                Panel_FORM_BOTONES_PIE.Visible = false;
                Button_NUEVO_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                break;
            case Acciones.NuevoExamen:
                Panel_NUEVO_EXAMEN_PARA_PROVEEDOR.Visible = false;
                break;
        }
    }
    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;

                Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.CargarProv:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Button_NUEVO.Visible = true;
                Button_CANCELAR.Visible = false;
                Button_GUARDAR.Visible = true;

                Panel_DATOS_PROVEEDOR_SELECCIONADO.Visible = true;

                Panel_FORMULARIO.Visible = true;
                Panel_EXAMENES_ASOCIADOS_ACTUALMENTE.Visible = true;

                Panel_FORM_BOTONES_PIE.Visible = true;
                Button_NUEVO_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
                Button_GUARDAR_1.Visible = true;
                break;
            case Acciones.NuevoExamen:
                Panel_NUEVO_EXAMEN_PARA_PROVEEDOR.Visible = true;
                break;
        }
    }
    private void cargar_DropDownList_BUSCAR()
    {
        DropDownList_BUSCAR.Items.Clear();

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_BUSCAR.Items.Add(item);

        item = new ListItem("Núm identificación", "NUMERO_IDENTIFICACION");
        DropDownList_BUSCAR.Items.Add(item);

        item = new ListItem("Nombre", "RAZON_SOCIAL");
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

    private void cargar_GridView_RESULTADOS_BUSQUEDA_laboratorios_medicos_activos()
    {
        proveedor _proveedor = new proveedor(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaProv = _proveedor.ObtenerProveedoresLaboratoriosActivos(); 

        if (tablaProv.Rows.Count <= 0)
        {
            if (_proveedor.MensajeError == null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES,Label_MENSAJE, "No se encontraron registros en la busqueda de Laboratorios clínicos activos.", Proceso.Advertencia);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _proveedor.MensajeError, Proceso.Error);
            }

            Panel_RESULTADOS_GRID.Visible = false;
        }
        else
        {
            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaProv;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
        }
    }

    private void cargar_DropDownList_EXAMENES()
    {
        DropDownList_EXAMENES.Items.Clear();

        producto _producto = new producto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaExamenes;

        if (Session["idEmpresa"].ToString() == "1")
        {
            tablaExamenes = _producto.ObtenerTodosExamenesMedicosPorTipoServicioComplemetario("TEMPORAL");
        }
        else
        {
            tablaExamenes = _producto.ObtenerTodosExamenesMedicosPorTipoServicioComplemetario("OUTSOURCING");
        }

        ListItem item = new ListItem("Seleccione", "");
        DropDownList_EXAMENES.Items.Add(item);

        foreach (DataRow fila in tablaExamenes.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_PRODUCTO"].ToString());
            DropDownList_EXAMENES.Items.Add(item);
        }

        DropDownList_EXAMENES.DataBind();
    }
    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:

                cargar_DropDownList_BUSCAR();
                configurarCaracteresAceptadosBusqueda(true, true);

                HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "SIN_FILTRO";
                HiddenField_FILTRO_DROP.Value = "";
                HiddenField_FILTRO_DATO.Value = "";

                cargar_GridView_RESULTADOS_BUSQUEDA_laboratorios_medicos_activos();
                break;
            case Acciones.NuevoExamen:
                cargar_DropDownList_EXAMENES();
                break;
        }
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
    protected void Button_NUEVO_Click(object sender, EventArgs e)
    {
        Mostrar(Acciones.NuevoExamen);
        Cargar(Acciones.NuevoExamen);
    }
    private void Guardar()
    {
        Decimal ID_PROVEEDOR = Convert.ToDecimal(Label_ID_PROVEEDOR.Text);

        List<producto> listaProductosParaProveedor = new List<producto>();
        producto _productoParaLista;

        for (int i = 0; i < GridView_EXAMENES_POR_PROVEEDOR.Rows.Count; i++)
        { 
            _productoParaLista = new producto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            _productoParaLista.ID_PRODUCTO = Convert.ToDecimal(GridView_EXAMENES_POR_PROVEEDOR.DataKeys[i].Values["ID_PRODUCTO"]);
            _productoParaLista.REGISTRO_P_P = Convert.ToDecimal(GridView_EXAMENES_POR_PROVEEDOR.DataKeys[i].Values["REGISTRO_P_P"]);

            listaProductosParaProveedor.Add(_productoParaLista);
        }

        examenesEmpleado _examenesEmpleado = new examenesEmpleado(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean verificador = _examenesEmpleado.AdicionarAlmRegProductosProveedor(ID_PROVEEDOR, listaProductosParaProveedor);

        if (verificador == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _examenesEmpleado.MensajeError, Proceso.Error);
        }
        else
        { 
            proveedor _proveedor = new proveedor(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaInfoProv = _proveedor.ObtenerAlmRegProveedorPorRegistro(ID_PROVEEDOR);

            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.CargarProv);
            Cargar(tablaInfoProv.Rows[0]);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El laboratorio " + Label_NOMBRE_PROVEEDOR.Text + " fue actualizado correctamente.", Proceso.Correcto);
        }

    }
    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        Guardar();    
    }
    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
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
            if (DropDownList_BUSCAR.SelectedValue == "NUMERO_IDENTIFICACION")
            {
                configurarCaracteresAceptadosBusqueda(false, true);
            }
            else
            {
                if (DropDownList_BUSCAR.SelectedValue == "RAZON_SOCIAL")
                {
                    configurarCaracteresAceptadosBusqueda(true, false);
                }
            }
        }
        TextBox_BUSCAR.Text = "";
    }
    protected void Buscar()
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);

        String dato = HiddenField_FILTRO_DATO.Value;
        String drop = HiddenField_FILTRO_DROP.Value;

        proveedor _proveedor = new proveedor(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable _dataTable = new DataTable();

        switch (drop)
        {
            case "NUMERO_IDENTIFICACION":
                _dataTable = _proveedor.ObtenerAlmRegProveedorLaboratoriosPorNit(dato); 
                break;

            case "RAZON_SOCIAL":
                _dataTable = _proveedor.ObtenerAlmRegProveedorLaboratoriosPorNombre(dato); 
                break;
        }

        if (_dataTable.Rows.Count > 0)
        {
            GridView_RESULTADOS_BUSQUEDA.DataSource = _dataTable;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
        }
        else
        {
            if (!String.IsNullOrEmpty(_proveedor.MensajeError))
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES,Label_MENSAJE, "Consulte con el Administrador: " + _proveedor.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información para " + this.DropDownList_BUSCAR.SelectedItem + " : " + this.TextBox_BUSCAR.Text, Proceso.Advertencia);
            }

            Panel_RESULTADOS_GRID.Visible = false;
        }
        _dataTable.Dispose();
    }
    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = tiposBusqueda.CON_FILTRO.ToString();
        HiddenField_FILTRO_DROP.Value = DropDownList_BUSCAR.SelectedValue;
        HiddenField_FILTRO_DATO.Value = TextBox_BUSCAR.Text;

        Buscar();
    }
    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;

        if (HiddenField_TIPO_BUSQUEDA_ACTUAL.Value == tiposBusqueda.SIN_FILTRO.ToString())
        {
            cargar_GridView_RESULTADOS_BUSQUEDA_laboratorios_medicos_activos();
        }
        else
        {
            Buscar();
        }
    }
    private void cargar_datos_proveedor_seleciconado(Decimal ID_PROVEEDOR)
    {
        proveedor _proveedor = new proveedor(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoProveedor = _proveedor.ObtenerAlmRegProveedorPorRegistro(ID_PROVEEDOR);
        DataRow filaInfoProveedor = tablaInfoProveedor.Rows[0];

        Label_ID_PROVEEDOR.Text = ID_PROVEEDOR.ToString();
        Label_NOMBRE_PROVEEDOR.Text = filaInfoProveedor["RAZON_SOCIAL"].ToString();
        Label_ID_CATEGORIA.Text = filaInfoProveedor["ID_CATEGORIA"].ToString();
        Label_NOMBRE_CATEGORIA.Text = filaInfoProveedor["NOMBRE_CATEGORIA"].ToString();
        Label_REGIONAL.Text = filaInfoProveedor["REGIONAL"].ToString();
        Label_CIUDAD.Text = filaInfoProveedor["CIUDAD_SECTOR"].ToString();

    }
    private void cargar_examenes_asociados(Decimal ID_PROVEEDOR)
    { 
        examenesEmpleado _examenesEmpleado = new examenesEmpleado(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaExamenes = _examenesEmpleado.ObtenerAlmRegProductosProveedorPorIdProveedor(ID_PROVEEDOR);

        if (tablaExamenes.Rows.Count < 0)
        {
            if (_examenesEmpleado.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _examenesEmpleado.MensajeError, Proceso.Error);
            }

            Panel_EXAMENES_ASOCIADOS_ACTUALMENTE.Visible = false;
        }
        else
        {
            GridView_EXAMENES_POR_PROVEEDOR.DataSource = tablaExamenes;
            GridView_EXAMENES_POR_PROVEEDOR.DataBind();
        }
    }
    private void Cargar(DataRow filaInfoProv)
    {
        cargar_datos_proveedor_seleciconado(Convert.ToDecimal(filaInfoProv["ID_PROVEEDOR"]));

        cargar_examenes_asociados(Convert.ToDecimal(filaInfoProv["ID_PROVEEDOR"]));
    }   
    protected void GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged(object sender, EventArgs e)
    {
        Decimal ID_PROVEEDOR = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["ID_PROVEEDOR"]);

        proveedor _proveedor = new proveedor(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoProv = _proveedor.ObtenerAlmRegProveedorPorRegistro(ID_PROVEEDOR);

        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.CargarProv);
        Cargar(tablaInfoProv.Rows[0]);
    }
    protected void GridView_EXAMENES_POR_PROVEEDOR_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        DataTable tablaGrilla = obtenerDataTableDe_GridView_EXAMENES_POR_PROVEEDOR();

        int filaSeleccionada = Convert.ToInt32(e.CommandArgument);

        tablaGrilla.Rows.RemoveAt(filaSeleccionada);

        GridView_EXAMENES_POR_PROVEEDOR.DataSource = tablaGrilla;
        GridView_EXAMENES_POR_PROVEEDOR.DataBind();

        if (GridView_EXAMENES_POR_PROVEEDOR.Rows.Count > 0)
        {
            Panel_EXAMENES_ASOCIADOS_ACTUALMENTE.Visible = true;
        }
        else
        {
            Panel_EXAMENES_ASOCIADOS_ACTUALMENTE.Visible = false;
        }
    }
    private DataTable obtenerDataTableDe_GridView_EXAMENES_POR_PROVEEDOR()
    {
        DataTable tablaResultado = new DataTable();
        tablaResultado.Columns.Add("ID_PRODUCTO");
        tablaResultado.Columns.Add("REGISTRO_P_P");
        tablaResultado.Columns.Add("NOMBRE_PRODUCTO");
        tablaResultado.Columns.Add("DESCRIPCION_PRODUCTO");
        tablaResultado.Columns.Add("BASICO");
        tablaResultado.Columns.Add("APLICA_A");

        DataRow filaTablaResultado;

        Decimal ID_PRODUCTO = 0;
        Decimal REGISTRO_P_P = 0;
        String NOMBRE_PRODUCTO = null;
        String DESCRIPCION_PRODUCTO = null;
        String BASICO = null;
        String APLICA_A = null;

        GridViewRow filaGrilla;
        for (int i = 0; i < GridView_EXAMENES_POR_PROVEEDOR.Rows.Count; i++)
        {
            filaGrilla = GridView_EXAMENES_POR_PROVEEDOR.Rows[i];

            ID_PRODUCTO = Convert.ToDecimal(GridView_EXAMENES_POR_PROVEEDOR.DataKeys[i].Values["ID_PRODUCTO"]);

            REGISTRO_P_P = Convert.ToDecimal(GridView_EXAMENES_POR_PROVEEDOR.DataKeys[i].Values["REGISTRO_P_P"]);

            NOMBRE_PRODUCTO = HttpUtility.HtmlDecode(filaGrilla.Cells[3].Text);

            DESCRIPCION_PRODUCTO = HttpUtility.HtmlDecode(filaGrilla.Cells[4].Text);

            BASICO = HttpUtility.HtmlDecode(filaGrilla.Cells[5].Text);

            APLICA_A = HttpUtility.HtmlDecode(filaGrilla.Cells[6].Text);


            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_PRODUCTO"] = ID_PRODUCTO;
            filaTablaResultado["REGISTRO_P_P"] = REGISTRO_P_P;
            filaTablaResultado["NOMBRE_PRODUCTO"] = NOMBRE_PRODUCTO;
            filaTablaResultado["DESCRIPCION_PRODUCTO"] = DESCRIPCION_PRODUCTO;
            filaTablaResultado["BASICO"] = BASICO;
            filaTablaResultado["APLICA_A"] = APLICA_A;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }
    private Boolean determinar_si_examen_ya_esta_en_grilla(Decimal ID_PRODUCTO)
    {
        Decimal ID_PRODUCTO_C = 0;
        for (int i = 0; i < GridView_EXAMENES_POR_PROVEEDOR.Rows.Count; i++)
        {
            ID_PRODUCTO_C = Convert.ToDecimal(GridView_EXAMENES_POR_PROVEEDOR.DataKeys[i].Values["ID_PRODUCTO"]);

            if (ID_PRODUCTO_C == ID_PRODUCTO)
            {
                return true;
            }
        }

        return false;
    }
    protected void Button_INGRESAR_EXAMEN_A_GRILLA_Click(object sender, EventArgs e)
    {
        Decimal ID_PRODUCTO = Convert.ToDecimal(DropDownList_EXAMENES.SelectedValue);
        if (determinar_si_examen_ya_esta_en_grilla(ID_PRODUCTO) == false)
        {
            DataTable tablaExamenesGrilla = obtenerDataTableDe_GridView_EXAMENES_POR_PROVEEDOR();

            DataRow filaNueva = tablaExamenesGrilla.NewRow();

            producto _producto = new producto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaProducto = _producto.ObtenerAlmRegProductoPorId(Convert.ToInt32(ID_PRODUCTO));
            DataRow filaProd = tablaProducto.Rows[0];

            filaNueva["ID_PRODUCTO"] = ID_PRODUCTO;
            filaNueva["REGISTRO_P_P"] = 0;
            filaNueva["NOMBRE_PRODUCTO"] = DropDownList_EXAMENES.SelectedItem.Text;
            filaNueva["DESCRIPCION_PRODUCTO"] = filaProd["DESCRIPCION"].ToString().Trim();
            if (filaProd["BASICO"].ToString() == "S")
            {
                filaNueva["BASICO"] = "Si";
            }
            else
            {
                filaNueva["BASICO"] = "No";
            }
            filaNueva["APLICA_A"] = filaProd["APLICA_A"].ToString().Trim();

            tablaExamenesGrilla.Rows.Add(filaNueva);

            GridView_EXAMENES_POR_PROVEEDOR.DataSource = tablaExamenesGrilla;
            GridView_EXAMENES_POR_PROVEEDOR.DataBind();

            Panel_EXAMENES_ASOCIADOS_ACTUALMENTE.Visible = true;
        }
        else
        { 
        }

        Ocultar(Acciones.NuevoExamen);
    }
    protected void Button_CANCELAR_INGRESO_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.NuevoExamen);
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
}