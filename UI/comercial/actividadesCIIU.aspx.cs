using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brainsbits.LLB.maestras;
using System.Data;
using Brainsbits.LLB.seguridad;
using TSHAK.Components;
using Brainsbits.LLB;

public partial class _Default : System.Web.UI.Page
{
    private String NOMBRE_AREA = tabla.NOMBRE_AREA_GESTION_COMERCIAL;

    private System.Drawing.Color colorSeleccionado = System.Drawing.ColorTranslator.FromHtml("#699DF0");

    private enum Acciones
    {
        Inicio = 0, 
        Seccion = 1,
        Division = 2,
        Clase = 3,
        Actividad = 4
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Pregunta = 2
    }

    private enum AccionesGrilla
    { 
        Ninguna = 0,
        Nuevo = 1, 
        modificar = 2, 
        Eliminar = 3, 
        Seleccionar = 4
    }

    private enum GrillasFormulario
    {
        Secciones = 0,
        Divisiones = 1,
        Clases = 2,
        Actividades = 3
    }

    private void Informar(Panel panel_fondo, System.Web.UI.WebControls.Image imagen_mensaje, Panel panel_mensaje, Label label_mensaje, String mensaje, Proceso proceso)
    {
        panel_fondo.Style.Add("display", "block");
        panel_mensaje.Style.Add("display", "block");

        switch (proceso)
        {
            case Proceso.Correcto:
                label_mensaje.ForeColor = System.Drawing.Color.Green;
                imagen_mensaje.ImageUrl = "~/imagenes/plantilla/ok_popup.png";
                break;
            case Proceso.Error:
                label_mensaje.ForeColor = System.Drawing.Color.Red;
                imagen_mensaje.ImageUrl = "~/imagenes/plantilla/advertencia_popup.png";
                break;
            case Proceso.Pregunta:
                label_mensaje.ForeColor = System.Drawing.Color.Black;
                imagen_mensaje.ImageUrl = "~/imagenes/plantilla/pregunta_popup.png";
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

    private void ocultar_mensaje(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.Style.Add("display", "none");
        panel_mensaje.Style.Add("display", "none");

        panel_fondo.Visible = false;
        panel_mensaje.Visible = false;
    }




    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = false;

                Panel_SECCION.Visible = false;
                Button_NUEVO_SECCION.Visible = false;
                Button_GUARDAR_SECCION.Visible = false;
                Button_CANCELAR_SECCION.Visible = false;

                Panel_DIVISION.Visible = false;
                Button_NUEVA_DIVISION.Visible = false;
                Button_GUARDAR_DIVISION.Visible = false;
                Button_CANCELAR_DIVISION.Visible = false;

                Panel_CLASES.Visible = false;
                Button_NUEVO_CLASE.Visible = false;
                Button_GUARDAR_CLASE.Visible = false;
                Button_CANCELAR_CLASE.Visible = false;

                Panel_ACTIVIDADES.Visible = false;
                Button_NUEVO_ACTIVIDAD.Visible = false;
                Button_GUARDAR_ACTIVIDAD.Visible = false;
                Button_CANCELAR_ACTIVIDAD.Visible = false;
                break;
            case Acciones.Seccion:
                Panel_SECCION.Visible = false;
                break;
            case Acciones.Division:
                Panel_DIVISION.Visible = false;
                break;
            case Acciones.Clase:
                Panel_CLASES.Visible = false;
                break;
            case Acciones.Actividad:
                Panel_ACTIVIDADES.Visible = false;
                break;
        }
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_SECCION.Visible = true;
                Button_NUEVO_SECCION.Visible = true;
                break;
            case Acciones.Seccion:
                Panel_SECCION.Visible = true;
                break;
            case Acciones.Division:
                Panel_DIVISION.Visible = true;
                break;
            case Acciones.Clase:
                Panel_CLASES.Visible = true;
                break;
            case Acciones.Actividad:
                Panel_ACTIVIDADES.Visible = true;
                break;
        }
    }

    private void cargar_GridView_SECCIONES_desde_tabla(DataTable tablaInfo)
    {
        GridView_SECCIONES.DataSource = tablaInfo;
        GridView_SECCIONES.DataBind();

        TextBox datoGrilla;

        DataRow filaTabla;
        GridViewRow filaGrilla;

        for (int i = 0; i < tablaInfo.Rows.Count; i++)
        {
            filaTabla = tablaInfo.Rows[i];
            filaGrilla = GridView_SECCIONES.Rows[i];

            datoGrilla = filaGrilla.FindControl("TextBox_ID_SECCION") as TextBox;
            datoGrilla.Text = filaTabla["ID_SECCION"].ToString().Trim();

            datoGrilla = filaGrilla.FindControl("TextBox_DESCRIPCION_SECCION") as TextBox;
            datoGrilla.Text = filaTabla["NOMBRE"].ToString().Trim();

            filaGrilla.BackColor = System.Drawing.Color.Transparent;

            GridView_SECCIONES.SelectedIndex = -1;
        }
    }

    private void cargar_GridView_DIVISIONES_desde_tabla(DataTable tablaInfo)
    {
        GridView_DIVISIONES.DataSource = tablaInfo;
        GridView_DIVISIONES.DataBind();

        TextBox datoGrilla;

        DataRow filaTabla;
        GridViewRow filaGrilla;

        for (int i = 0; i < tablaInfo.Rows.Count; i++)
        {
            filaTabla = tablaInfo.Rows[i];
            filaGrilla = GridView_DIVISIONES.Rows[i];

            datoGrilla = filaGrilla.FindControl("TextBox_ID_DIVISION") as TextBox;
            datoGrilla.Text = filaTabla["ID_DIVISION"].ToString().Trim();

            datoGrilla = filaGrilla.FindControl("TextBox_DESCRIPCION_DIVISION") as TextBox;
            datoGrilla.Text = filaTabla["NOMBRE"].ToString().Trim();

            filaGrilla.BackColor = System.Drawing.Color.Transparent;

            GridView_DIVISIONES.SelectedIndex = -1;
        }
    }


    private void cargar_GridView_CLASES_desde_tabla(DataTable tablaInfo)
    {
        GridView_CLASES.DataSource = tablaInfo;
        GridView_CLASES.DataBind();

        TextBox datoGrilla;

        DataRow filaTabla;
        GridViewRow filaGrilla;

        for (int i = 0; i < tablaInfo.Rows.Count; i++)
        {
            filaTabla = tablaInfo.Rows[i];
            filaGrilla = GridView_CLASES.Rows[i];

            datoGrilla = filaGrilla.FindControl("TextBox_ID_CLASE") as TextBox;
            datoGrilla.Text = filaTabla["ID_CLASE"].ToString().Trim();

            datoGrilla = filaGrilla.FindControl("TextBox_DESCRIPCION_CLASE") as TextBox;
            datoGrilla.Text = filaTabla["NOMBRE"].ToString().Trim();

            filaGrilla.BackColor = System.Drawing.Color.Transparent;

            GridView_CLASES.SelectedIndex = -1;
        }
    }

    private void cargar_GridView_ACTIVIDADES_desde_tabla(DataTable tablaInfo)
    {
        GridView_ACTIVIDADES.DataSource = tablaInfo;
        GridView_ACTIVIDADES.DataBind();

        TextBox datoGrilla;

        DataRow filaTabla;
        GridViewRow filaGrilla;

        for (int i = 0; i < tablaInfo.Rows.Count; i++)
        {
            filaTabla = tablaInfo.Rows[i];
            filaGrilla = GridView_ACTIVIDADES.Rows[i];

            datoGrilla = filaGrilla.FindControl("TextBox_ID_ACTIVIDAD") as TextBox;
            datoGrilla.Text = filaTabla["ID_ACTIVIDAD"].ToString().Trim();

            datoGrilla = filaGrilla.FindControl("TextBox_DESCRIPCION_ACTIVIDAD") as TextBox;
            datoGrilla.Text = filaTabla["NOMBRE"].ToString().Trim();

            filaGrilla.BackColor = System.Drawing.Color.Transparent;

            GridView_ACTIVIDADES.SelectedIndex = -1;
        }
    }

    private void admin_columnas_grilla(GridView grilla, Boolean seleccionar, Boolean editar, Boolean borrar, Boolean resto_columnas)
    {
        GridViewRow filaGrila;

        for (int i = 0; i < grilla.Rows.Count; i++)
        {
            filaGrila = grilla.Rows[i];

            for (int j = 3; j < filaGrila.Cells.Count; j++)
            {
                filaGrila.Cells[j].Enabled = resto_columnas;
            }
        }

        grilla.Columns[0].Visible = seleccionar;
        grilla.Columns[1].Visible = borrar;
        grilla.Columns[2].Visible = editar;
    }
    private void cargar_GridView_SECCIONES()
    {
        seccion _seccion = new seccion(Session["idEmpresa"].ToString());
        DataTable tablaInfo = _seccion.ObtenerTodasLasSecciones();

        if (tablaInfo.Rows.Count <= 0)
        {
            if (_seccion.MensajError != null)
            {
                Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, _seccion.MensajError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, "ADVERTENCIA: No se encontró información de SECCIÓNES.", Proceso.Error);
            }

            GridView_SECCIONES.DataSource = null;
            GridView_SECCIONES.DataBind();
        }
        else
        {
            cargar_GridView_SECCIONES_desde_tabla(tablaInfo);
            admin_columnas_grilla(GridView_SECCIONES,true, true, true, false);
        }
    }

    private void configurar_panel_fondo(Panel panel_fondo)
    {
        panel_fondo.Style.Add("position", "fixed");
        panel_fondo.Style.Add("top", "0");
        panel_fondo.Style.Add("left", "0");
        panel_fondo.Style.Add("right", "0");
        panel_fondo.Style.Add("bottom", "0");
        panel_fondo.Style.Add("width", "100%");
        panel_fondo.Style.Add("height", "100%");
        panel_fondo.Style.Add("margin", "0");
        panel_fondo.Style.Add("background-color", "#aaaaaa");
        panel_fondo.Style.Add("opacity", "0.5");
        panel_fondo.Style.Add("-moz-opacity", "0.5");
        panel_fondo.Style.Add("filter", "alpha(opacity=50)");
        panel_fondo.Style.Add("z-index", "9998");
        panel_fondo.Style.Add("display", "none");
    }

    private void configurar_panel_popup(Panel panel_mensaje, int panel_mensaje_width, int panel_mensaje_height)
    {
        panel_mensaje.Style.Add("position", "fixed");
        panel_mensaje.Style.Add("top", "50%");
        panel_mensaje.Style.Add("left", "50%");
        panel_mensaje.Style.Add("width", panel_mensaje_width + "px");
        panel_mensaje.Style.Add("height", panel_mensaje_height + "px");
        panel_mensaje.Style.Add("margin-left", "-" + (panel_mensaje_width / 2) + "px");
        panel_mensaje.Style.Add("margin-top", "-" + (panel_mensaje_height / 2) + "px");
        panel_mensaje.Style.Add("background-color", "#dddddd");
        panel_mensaje.Style.Add("opacity", "1.0");
        panel_mensaje.Style.Add("-moz-opacity", "1.0");
        panel_mensaje.Style.Add("filter", "alpha(opacity=100)");
        panel_mensaje.Style.Add("z-index", "9999");
        panel_mensaje.Style.Add("border", "2px solid #000000");
        panel_mensaje.Style.Add("padding", "5px");
        panel_mensaje.Style.Add("display", "none");
    }

    private void configurar_panel_mensaje(Panel panel_mensaje, int panel_mensaje_width, int panel_mensaje_height)
    {
        panel_mensaje.Style.Add("position", "fixed");
        panel_mensaje.Style.Add("top", "50%");
        panel_mensaje.Style.Add("left", "50%");
        panel_mensaje.Style.Add("width", panel_mensaje_width + "px");
        panel_mensaje.Style.Add("height", panel_mensaje_height + "px");
        panel_mensaje.Style.Add("margin-left", "-" + (panel_mensaje_width / 2) + "px");
        panel_mensaje.Style.Add("margin-top", "-" + (panel_mensaje_height / 2) + "px");
        panel_mensaje.Style.Add("background-color", "#");
        panel_mensaje.Style.Add("opacity", "1.0");
        panel_mensaje.Style.Add("-moz-opacity", "1.0");
        panel_mensaje.Style.Add("filter", "alpha(opacity=100)");
        panel_mensaje.Style.Add("z-index", "9999");
        panel_mensaje.Style.Add("border", "2px solid #000000");
        panel_mensaje.Style.Add("padding", "5px");
        panel_mensaje.Style.Add("display", "none");
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                this.Title = "ADMININSTRACIÓN DE ACTIVIDADES COMERCIALES";

                cargar_GridView_SECCIONES();

                acciones_sobre_botones_deacicon_grillas(Button_NUEVO_SECCION, Button_GUARDAR_SECCION, Button_CANCELAR_SECCION, true, false, false);

                HiddenField_ACCION_SECCION.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_FILA_SECCION.Value = null;
                HiddenField_ID_SECCION.Value = null;
                HiddenField_DESCRIPCION_SECCION.Value = null;
                break;
        }
    }

    private void Iniciar()
    {
        configurar_paneles_popup(Panel_FONDO_MENSAJE_SECCION, Panel_MENSAJE_ACCION_SECCION);
        configurar_paneles_popup(Panel_FONDO_MENSAJE_SECCION, Panel_MENSAJE_SECCION);

        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }

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


    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            Iniciar();
        }  
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        RolPermisos();
    }

    private void habilitar_fila_grilla(GridView grilla, int indexFila, int indexColumna, Boolean seleccionar, Boolean borrar, Boolean editar)
    {
        for (int j = indexColumna; j < grilla.Columns.Count; j++)
        {
            grilla.Rows[indexFila].Cells[j].Enabled = true;
        }

        grilla.Columns[0].Visible = seleccionar;
        grilla.Columns[1].Visible = borrar;
        grilla.Columns[2].Visible = editar;
    }

    private void acciones_sobre_botones_deacicon_grillas(Button bNuevo, Button bGuardar, Button bCancelar, Boolean nuevo, Boolean guardar, Boolean cancelar)
    {
        bNuevo.Visible = nuevo;
        bGuardar.Visible = guardar;
        bCancelar.Visible = cancelar;
    }

    private void seleccionarFilaDeGrilla(GridView grilla, Int32 index)
    {
        for (int i = 0; i < grilla.Rows.Count; i++)
        {
            grilla.Rows[i].BackColor = System.Drawing.Color.Transparent;
        }

        grilla.Rows[index].BackColor = colorSeleccionado;
    }

    private void cargar_GridView_DIVISIONES(String ID_SECCION)
    {
        division _division = new division(Session["idEmpresa"].ToString());
        DataTable tablaInfo = _division.ObtenerDivisionesPorIdSeccion(ID_SECCION);

        if (tablaInfo.Rows.Count <= 0)
        {
            if (_division.MensajError != null)
            {
                Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, _division.MensajError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, "ADVERTENCIA: No se encontró información de DIVISIONES asociadas a la SECCIÓN seleccionada.", Proceso.Error);
            }

            GridView_DIVISIONES.DataSource = null;
            GridView_DIVISIONES.DataBind();
        }
        else
        {
            cargar_GridView_DIVISIONES_desde_tabla(tablaInfo);
            admin_columnas_grilla(GridView_DIVISIONES, true, true, true, false);
        }
    }

    private void cargar_GridView_CLASES(String ID_DIVISION)
    {
        clase _clase = new clase(Session["idEmpresa"].ToString());
        DataTable tablaInfo = _clase.ObtenerClasesPorIdDivision(ID_DIVISION);

        if (tablaInfo.Rows.Count <= 0)
        {
            if (_clase.MensajError != null)
            {
                Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, _clase.MensajError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, "ADVERTENCIA: No se encontró información de CLASES asociadas a la DIVISIÓN seleccionada.", Proceso.Error);
            }

            GridView_CLASES.DataSource = null;
            GridView_CLASES.DataBind();
        }
        else
        {
            cargar_GridView_CLASES_desde_tabla(tablaInfo);
            admin_columnas_grilla(GridView_CLASES, true, true, true, false);
        }
    }

    private void cargar_GridView_ACTIVIDADES(String ID_CLASE)
    {
        actividad _actividad = new actividad(Session["idEmpresa"].ToString());
        DataTable tablaInfo = _actividad.ObtenerActividadesPorIdClase(ID_CLASE);

        if (tablaInfo.Rows.Count <= 0)
        {
            if (_actividad.MensajError != null)
            {
                Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, _actividad.MensajError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, "ADVERTENCIA: No se encontró información de ACTIVIDADES asociadas a la CLASE seleccionada.", Proceso.Error);
            }

            GridView_ACTIVIDADES.DataSource = null;
            GridView_ACTIVIDADES.DataBind();
        }
        else
        {
            cargar_GridView_ACTIVIDADES_desde_tabla(tablaInfo);
            admin_columnas_grilla(GridView_ACTIVIDADES, false, true, true, false);
        }
    }
    protected void GridView_SECCIONES_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        HiddenField_GRIILA_SELECCIONADA_ACTUALMENTE.Value = GrillasFormulario.Secciones.ToString();

        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        TextBox datoGrilla;

        HiddenField_FILA_SECCION.Value = indexSeleccionado.ToString();
        HiddenField_ID_SECCION.Value = GridView_SECCIONES.DataKeys[indexSeleccionado].Values["ID_SECCION"].ToString();

        datoGrilla = GridView_SECCIONES.Rows[indexSeleccionado].FindControl("TextBox_DESCRIPCION_SECCION") as TextBox;
        HiddenField_DESCRIPCION_SECCION.Value = datoGrilla.Text.Trim();

        if (e.CommandName == "modificar")
        {
            HiddenField_ACCION_SECCION.Value = AccionesGrilla.modificar.ToString();
            
            habilitar_fila_grilla(GridView_SECCIONES, indexSeleccionado, 5, false, false, false);

            ocultar_paneles_generales(true, false, false, false);

            acciones_sobre_botones_deacicon_grillas(Button_NUEVO_SECCION, Button_GUARDAR_SECCION, Button_CANCELAR_SECCION, false, true, true);
        }
        else
        {
            if (e.CommandName == "eliminar")
            {
                HiddenField_ACCION_SECCION.Value = AccionesGrilla.Eliminar.ToString();

                ocultar_paneles_generales(true, false, false, false);

                acciones_sobre_botones_deacicon_grillas(Button_NUEVO_SECCION, Button_GUARDAR_SECCION, Button_CANCELAR_SECCION, false, false, false);

                seccion _seccion = new seccion(Session["idEmpresa"].ToString());
                DataTable tablaInfoSeccion = _seccion.ObtenerDivisionesClasesActidadesEmpresaPorIdSeccion(HiddenField_ID_SECCION.Value);
                DataRow  filaInfoSeccion = tablaInfoSeccion.Rows[0];

                Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_ACCION_SECCION, Panel_MENSAJE_ACCION_SECCION, Label_MENSAJE_ACCION_SECCION, "Al eliminar la SECCIÓN seleccionada se verán afectadas " + filaInfoSeccion["NUM_DIVISIONES"].ToString() + " DIVISIONES, " + filaInfoSeccion["NUM_CLASES"].ToString() + " CLASES, " + filaInfoSeccion["NUM_ACTIVIDADES"].ToString() + " ACTIVIDADES y " + filaInfoSeccion["NUM_EMPRESAS"].ToString() + " EMPRESAS, Desea continuar?", Proceso.Pregunta);
            }
            else
            {
                if (e.CommandName == "seleccionar")
                {
                    HiddenField_ACCION_SECCION.Value = AccionesGrilla.Seleccionar.ToString();

                    seleccionarFilaDeGrilla(GridView_SECCIONES, indexSeleccionado);

                    cargar_GridView_DIVISIONES(GridView_SECCIONES.DataKeys[indexSeleccionado].Values["ID_SECCION"].ToString());

                    ocultar_paneles_generales(true, true, false, false);

                    acciones_sobre_botones_deacicon_grillas(Button_NUEVA_DIVISION, Button_GUARDAR_DIVISION, Button_CANCELAR_DIVISION, true, false, false);

                    HiddenField_ACCION_DIVISION.Value = AccionesGrilla.Ninguna.ToString();
                    HiddenField_FILA_DIVISION.Value = null;
                    HiddenField_ID_DIVISION.Value = null;
                    HiddenField_DESCRIPCION_DIVISION.Value = null;
                }
            }
        }
    }

    private DataTable obtenerDataTableDe_GridView_SECCIONES()
    {
        DataTable tablaResultado = new DataTable();
        tablaResultado.Columns.Add("ID_SECCION");
        tablaResultado.Columns.Add("NOMBRE");

        DataRow filaTablaResultado;

        String ID_SECCION = null;
        String NOMBRE = null;

        GridViewRow filaGrilla;
        TextBox datoGrilla;
        for (int i = 0; i < GridView_SECCIONES.Rows.Count; i++)
        {
            filaGrilla = GridView_SECCIONES.Rows[i];

            ID_SECCION = GridView_SECCIONES.DataKeys[i].Values["ID_SECCION"].ToString();

            datoGrilla = filaGrilla.FindControl("TextBox_DESCRIPCION_SECCION") as TextBox;
            NOMBRE = datoGrilla.Text.Trim();

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_SECCION"] = ID_SECCION;
            filaTablaResultado["NOMBRE"] = NOMBRE;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }

    private DataTable obtenerDataTableDe_GridView_DIVISIONES()
    {
        DataTable tablaResultado = new DataTable();
        tablaResultado.Columns.Add("ID_DIVISION");
        tablaResultado.Columns.Add("NOMBRE");

        DataRow filaTablaResultado;

        String ID_DIVISION = null;
        String NOMBRE = null;

        GridViewRow filaGrilla;
        TextBox datoGrilla;
        for (int i = 0; i < GridView_DIVISIONES.Rows.Count; i++)
        {
            filaGrilla = GridView_DIVISIONES.Rows[i];

            ID_DIVISION = GridView_DIVISIONES.DataKeys[i].Values["ID_DIVISION"].ToString();

            datoGrilla = filaGrilla.FindControl("TextBox_DESCRIPCION_DIVISION") as TextBox;
            NOMBRE = datoGrilla.Text.Trim();

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_DIVISION"] = ID_DIVISION;
            filaTablaResultado["NOMBRE"] = NOMBRE;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }

    private DataTable obtenerDataTableDe_GridView_CLASES()
    {
        DataTable tablaResultado = new DataTable();
        tablaResultado.Columns.Add("ID_CLASE");
        tablaResultado.Columns.Add("NOMBRE");

        DataRow filaTablaResultado;

        String ID_CLASE = null;
        String NOMBRE = null;

        GridViewRow filaGrilla;
        TextBox datoGrilla;
        for (int i = 0; i < GridView_CLASES.Rows.Count; i++)
        {
            filaGrilla = GridView_CLASES.Rows[i];

            ID_CLASE = GridView_CLASES.DataKeys[i].Values["ID_CLASE"].ToString();

            datoGrilla = filaGrilla.FindControl("TextBox_DESCRIPCION_CLASE") as TextBox;
            NOMBRE = datoGrilla.Text.Trim();

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_CLASE"] = ID_CLASE;
            filaTablaResultado["NOMBRE"] = NOMBRE;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }


    private DataTable obtenerDataTableDe_GridView_ACTIVIDADES()
    {
        DataTable tablaResultado = new DataTable();
        tablaResultado.Columns.Add("ID_ACTIVIDAD");
        tablaResultado.Columns.Add("NOMBRE");

        DataRow filaTablaResultado;

        String ID_ACTIVIDAD = null;
        String NOMBRE = null;

        GridViewRow filaGrilla;
        TextBox datoGrilla;
        for (int i = 0; i < GridView_ACTIVIDADES.Rows.Count; i++)
        {
            filaGrilla = GridView_ACTIVIDADES.Rows[i];

            ID_ACTIVIDAD = GridView_ACTIVIDADES.DataKeys[i].Values["ID_ACTIVIDAD"].ToString();

            datoGrilla = filaGrilla.FindControl("TextBox_DESCRIPCION_ACTIVIDAD") as TextBox;
            NOMBRE = datoGrilla.Text.Trim();

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_ACTIVIDAD"] = ID_ACTIVIDAD;
            filaTablaResultado["NOMBRE"] = NOMBRE;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }


    protected void Button_CANCELAR_SECCION_Click(object sender, EventArgs e)
    {
        ocultar_paneles_generales(true, false, false, false);

        DataTable tablaGrilla = obtenerDataTableDe_GridView_SECCIONES();

        if (HiddenField_ACCION_SECCION.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }
        else
        {
            if (HiddenField_ACCION_SECCION.Value == AccionesGrilla.modificar.ToString())
            {
                DataRow filaGrilla = tablaGrilla.Rows[Convert.ToInt32(HiddenField_FILA_SECCION.Value)];

                filaGrilla["ID_SECCION"] = HiddenField_ID_SECCION.Value;
                filaGrilla["NOMBRE"] = HiddenField_DESCRIPCION_SECCION.Value;
            }
        }

        cargar_GridView_SECCIONES_desde_tabla(tablaGrilla);

        admin_columnas_grilla(GridView_SECCIONES, true, true, true, false);

        HiddenField_ACCION_SECCION.Value = AccionesGrilla.Ninguna.ToString();

        acciones_sobre_botones_deacicon_grillas(Button_NUEVO_SECCION, Button_GUARDAR_SECCION, Button_CANCELAR_SECCION, true, false, false);
    }

    private int determinar_secciones_con_id(String ID_SECCION)
    {
        int resultado = 0;

        seccion _seccion = new seccion(Session["idEmpresa"].ToString());
        DataTable tablaInfo = _seccion.ObtenerSeccionPorIdSeccion(ID_SECCION);

        resultado = tablaInfo.Rows.Count;

        return resultado;
    }

    private int determinar_divisiones_con_id(String ID_DIVISION)
    {
        int resultado = 0;

        division _division = new division(Session["idEmpresa"].ToString());
        DataTable tablaInfo = _division.ObtenerDivisionPorIdDivision(ID_DIVISION);

        resultado = tablaInfo.Rows.Count;

        return resultado;
    }

    private int determinar_clases_con_id(String ID_CLASE)
    {
        int resultado = 0;

        clase _clase = new clase(Session["idEmpresa"].ToString());
        DataTable tablaInfo = _clase.ObtenerClasePorIdClase(ID_CLASE);

        resultado = tablaInfo.Rows.Count;

        return resultado;
    }

    private int determinar_actividades_con_id(String ID_ACTIVIDAD)
    {
        int resultado = 0;

        actividad _actividad = new actividad(Session["idEmpresa"].ToString());
        DataTable tablaInfo = _actividad.ObtenerActividPorIdActividad(ID_ACTIVIDAD);

        resultado = tablaInfo.Rows.Count;

        return resultado;
    }

    private void Guardar_SECCION()
    {
        int filaSeleccionada = Convert.ToInt32(HiddenField_FILA_SECCION.Value);

        GridViewRow filaGrilla = GridView_SECCIONES.Rows[filaSeleccionada];

        TextBox datoGrilla;

        datoGrilla = filaGrilla.FindControl("TextBox_ID_SECCION") as TextBox;
        String ID_SECCION = datoGrilla.Text.Trim().ToUpper();

        datoGrilla = filaGrilla.FindControl("TextBox_DESCRIPCION_SECCION") as TextBox;
        String NOMBRE = datoGrilla.Text.Trim().ToUpper();

        if (determinar_secciones_con_id(ID_SECCION) > 0)
        {
            Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, "ADVERTENCIA: Ya existe una Sección con el identificador " + ID_SECCION + ".", Proceso.Error);
        }
        else
        {
            seccion _seccion = new seccion(Session["idEmpresa"].ToString());

            Boolean verificador = _seccion.Adicionar(ID_SECCION, NOMBRE, Session["USU_LOG"].ToString());

            if (verificador == false)
            {
                Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, _seccion.MensajError, Proceso.Error);
            }
            else
            {
                cargar_GridView_SECCIONES();
                Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, "Se creó correctamente la nnueva sección.", Proceso.Correcto);
            }
        }
    }

    private void Guardar_DIVISION()
    {
        String ID_SECCION = HiddenField_ID_SECCION.Value;

        int filaSeleccionada = Convert.ToInt32(HiddenField_FILA_DIVISION.Value);

        GridViewRow filaGrilla = GridView_DIVISIONES.Rows[filaSeleccionada];

        TextBox datoGrilla;

        datoGrilla = filaGrilla.FindControl("TextBox_ID_DIVISION") as TextBox;
        String ID_DIVISION = datoGrilla.Text.Trim().ToUpper();

        datoGrilla = filaGrilla.FindControl("TextBox_DESCRIPCION_DIVISION") as TextBox;
        String NOMBRE = datoGrilla.Text.Trim().ToUpper();

        if (ID_DIVISION.Length != 2)
        {
            Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, "ADVERTENCIA: El Código de una DIVISIÓN esta compuesto por dos números.", Proceso.Error);
        }
        else
        {
            if (determinar_divisiones_con_id(ID_DIVISION) > 0)
            {
                Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, "ADVERTENCIA: Ya existe una DIVISIÓN con el identificador " + ID_DIVISION + ".", Proceso.Error);
            }
            else
            {

                division _division = new division(Session["idEmpresa"].ToString());

                Boolean verificador = _division.Adicionar(ID_DIVISION, ID_SECCION, NOMBRE, Session["USU_LOG"].ToString());

                if (verificador == false)
                {
                    Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, _division.MensajError, Proceso.Error);
                }
                else
                {
                    cargar_GridView_DIVISIONES(ID_SECCION);
                    Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, "Se creó correctamente la nueva División.", Proceso.Correcto);
                }
            }
        }
    }

    private void Guardar_CLASE()
    {
        String ID_DIVISION = HiddenField_ID_DIVISION.Value;

        int filaSeleccionada = Convert.ToInt32(HiddenField_FILA_CLASE.Value);

        GridViewRow filaGrilla = GridView_CLASES.Rows[filaSeleccionada];

        TextBox datoGrilla;

        datoGrilla = filaGrilla.FindControl("TextBox_ID_CLASE") as TextBox;
        String ID_CLASE = datoGrilla.Text.Trim().ToUpper();

        datoGrilla = filaGrilla.FindControl("TextBox_DESCRIPCION_CLASE") as TextBox;
        String NOMBRE = datoGrilla.Text.Trim().ToUpper();

        if (ID_CLASE.Length != 3)
        {
            Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, "ADVERTENCIA: El código de una CLASE está compuesto por tres números.", Proceso.Error);
        }
        else
        {
            if (determinar_clases_con_id(ID_CLASE) > 0)
            {
                Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, "ADVERTENCIA: Ya existe una CLASE con el identificador " + ID_CLASE + ".", Proceso.Error);
            }
            else
            {
                clase _clase = new clase(Session["idEmpresa"].ToString());

                Boolean verificador = _clase.Adicionar(ID_CLASE, ID_DIVISION, NOMBRE, Session["USU_LOG"].ToString());

                if (verificador == false)
                {
                    Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, _clase.MensajError, Proceso.Error);
                }
                else
                {
                    cargar_GridView_CLASES(ID_DIVISION);
                    Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, "Se creó correctamente la nueva CLASE.", Proceso.Correcto);
                }
            }
        }
    }


    private void Guardar_ACTIVIDAD()
    {
        String ID_CLASE = HiddenField_ID_CLASE.Value;

        int filaSeleccionada = Convert.ToInt32(HiddenField_FILA_ACTIVIDAD.Value);

        GridViewRow filaGrilla = GridView_ACTIVIDADES.Rows[filaSeleccionada];

        TextBox datoGrilla;

        datoGrilla = filaGrilla.FindControl("TextBox_ID_ACTIVIDAD") as TextBox;
        String ID_ACTIVIDAD = datoGrilla.Text.Trim().ToUpper();

        datoGrilla = filaGrilla.FindControl("TextBox_DESCRIPCION_ACTIVIDAD") as TextBox;
        String NOMBRE = datoGrilla.Text.Trim().ToUpper();

        if (ID_ACTIVIDAD.Length != 4)
        {
            Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, "ADVERTENCIA: El código de una ACTIVIDAD está compuesto por cuatro números.", Proceso.Error);
        }
        else
        {
            if (determinar_actividades_con_id(ID_ACTIVIDAD) > 0)
            {
                Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, "ADVERTENCIA: Ya existe una ACTIVIDAD con el identificador " + ID_ACTIVIDAD + ".", Proceso.Error);
            }
            else
            {
                actividad _actividad = new actividad(Session["idEmpresa"].ToString());

                Boolean verificador = _actividad.Adicionar(ID_ACTIVIDAD, ID_CLASE, NOMBRE, Session["USU_LOG"].ToString());

                if (verificador == false)
                {
                    Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, _actividad.MensajError, Proceso.Error);
                }
                else
                {
                    cargar_GridView_ACTIVIDADES(ID_CLASE);
                    Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, "Se creó correctamente la nueva ACTIVIDAD.", Proceso.Correcto);
                }
            }
        }
    }

    private void Modificar()
    {
        int filaSeleccionada = 0;
        GridViewRow filaGrilla = null;
        TextBox datoGrilla;
        String ID_SELECCIONADO = null;
        DataTable tablaInfo;
        DataRow filaInfo;

        if (HiddenField_GRIILA_SELECCIONADA_ACTUALMENTE.Value == GrillasFormulario.Secciones.ToString())
        {
            filaSeleccionada = Convert.ToInt32(HiddenField_FILA_SECCION.Value);
            filaGrilla = GridView_SECCIONES.Rows[filaSeleccionada];
            datoGrilla = filaGrilla.FindControl("TextBox_ID_SECCION") as TextBox;

            ID_SELECCIONADO = datoGrilla.Text.Trim().ToUpper();

            seccion _seccion = new seccion(Session["idEmpresa"].ToString());
            tablaInfo = _seccion.ObtenerDivisionesClasesActidadesEmpresaPorIdSeccion(ID_SELECCIONADO);
            filaInfo = tablaInfo.Rows[0];

            Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_ACCION_SECCION, Panel_MENSAJE_ACCION_SECCION, Label_MENSAJE_ACCION_SECCION, "Al modificar la SECCIÓN seleccionada se verán afectadas " + filaInfo["NUM_DIVISIONES"].ToString() + " DIVISIONES, " + filaInfo["NUM_CLASES"].ToString() + " CLASES, " + filaInfo["NUM_ACTIVIDADES"].ToString() + " ACTIVIDADES y " + filaInfo["NUM_EMPRESAS"].ToString() + " EMPRESAS, Desea continuar?", Proceso.Pregunta);
        }
        else
        {
            if (HiddenField_GRIILA_SELECCIONADA_ACTUALMENTE.Value == GrillasFormulario.Divisiones.ToString())
            {
                filaSeleccionada = Convert.ToInt32(HiddenField_FILA_DIVISION.Value);
                filaGrilla = GridView_DIVISIONES.Rows[filaSeleccionada];
                datoGrilla = filaGrilla.FindControl("TextBox_ID_DIVISION") as TextBox;

                ID_SELECCIONADO = datoGrilla.Text.Trim().ToUpper();

                division _division = new division(Session["idEmpresa"].ToString());
                tablaInfo = _division.ObtenerClasesActidadesEmpresaPorIdDivision(ID_SELECCIONADO);
                filaInfo = tablaInfo.Rows[0];

                Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_ACCION_SECCION, Panel_MENSAJE_ACCION_SECCION, Label_MENSAJE_ACCION_SECCION, "Al modificar la DIVISIÓN seleccionada se verán afectadas " + filaInfo["NUM_CLASES"].ToString() + " CLASES, " + filaInfo["NUM_ACTIVIDADES"].ToString() + " ACTIVIDADES y " + filaInfo["NUM_EMPRESAS"].ToString() + " EMPRESAS, Desea continuar?", Proceso.Pregunta);
            }
            else
            {
                if (HiddenField_GRIILA_SELECCIONADA_ACTUALMENTE.Value == GrillasFormulario.Clases.ToString())
                {
                    filaSeleccionada = Convert.ToInt32(HiddenField_FILA_CLASE.Value);
                    filaGrilla = GridView_CLASES.Rows[filaSeleccionada];
                    datoGrilla = filaGrilla.FindControl("TextBox_ID_CLASE") as TextBox;

                    ID_SELECCIONADO = datoGrilla.Text.Trim().ToUpper();

                    clase _clase = new clase(Session["idEmpresa"].ToString());
                    tablaInfo = _clase.ObtenerActidadesEmpresaPorIdClase(ID_SELECCIONADO);
                    filaInfo = tablaInfo.Rows[0];

                    Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_ACCION_SECCION, Panel_MENSAJE_ACCION_SECCION, Label_MENSAJE_ACCION_SECCION, "Al modificar la CLASE seleccionada se verán afectadas " + filaInfo["NUM_ACTIVIDADES"].ToString() + " ACTIVIDADES y " + filaInfo["NUM_EMPRESAS"].ToString() + " EMPRESAS, Desea continuar?", Proceso.Pregunta);
                }
                else
                {
                    if (HiddenField_GRIILA_SELECCIONADA_ACTUALMENTE.Value == GrillasFormulario.Actividades.ToString())
                    {
                        filaSeleccionada = Convert.ToInt32(HiddenField_FILA_ACTIVIDAD.Value);
                        filaGrilla = GridView_ACTIVIDADES.Rows[filaSeleccionada];
                        datoGrilla = filaGrilla.FindControl("TextBox_ID_ACTIVIDAD") as TextBox;

                        ID_SELECCIONADO = datoGrilla.Text.Trim().ToUpper();

                        actividad _actividad = new actividad(Session["idEmpresa"].ToString());
                        tablaInfo = _actividad.ObtenerEmpresaPorIdActividad(ID_SELECCIONADO);
                        filaInfo = tablaInfo.Rows[0];

                        Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_ACCION_SECCION, Panel_MENSAJE_ACCION_SECCION, Label_MENSAJE_ACCION_SECCION, "Al modificar la ACTIVIDAD seleccionada se verán afectadas " + filaInfo["NUM_EMPRESAS"].ToString() + " EMPRESAS, Desea continuar?", Proceso.Pregunta);
                    }
                }
            }
        }
    }

    protected void Button_GUARDAR_SECCION_Click(object sender, EventArgs e)
    {
        ocultar_paneles_generales(true, false, false, false);

        HiddenField_GRIILA_SELECCIONADA_ACTUALMENTE.Value = GrillasFormulario.Secciones.ToString();

        if (HiddenField_ACCION_SECCION.Value == AccionesGrilla.modificar.ToString())
        {
            Modificar();
        }
        else
        {
            if (HiddenField_ACCION_SECCION.Value == AccionesGrilla.Nuevo.ToString())
            {
                Guardar_SECCION();
            }
        }
    }

    private void ocultar_paneles_generales(Boolean bSeccion, Boolean bDivision, Boolean bClase, Boolean bActividad)
    {
        Ocultar(Acciones.Seccion);
        Ocultar(Acciones.Division);
        Ocultar(Acciones.Clase);
        Ocultar(Acciones.Actividad);

        if (bSeccion == true)
        {
            Mostrar(Acciones.Seccion);
        }

        if (bDivision == true)
        {
            Mostrar(Acciones.Division);
        }

        if (bClase == true)
        {
            Mostrar(Acciones.Clase);
        }

        if (bActividad == true)
        {
            Mostrar(Acciones.Actividad);
        }
    }

    protected void Button_NUEVO_SECCION_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerDataTableDe_GridView_SECCIONES();
        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_SECCION"] = "";
        filaNueva["NOMBRE"] = "";

        tablaDesdeGrilla.Rows.Add(filaNueva);

        GridViewRow filaGrilla;

        cargar_GridView_SECCIONES_desde_tabla(tablaDesdeGrilla);

        admin_columnas_grilla(GridView_SECCIONES, false, false, false, false);
        filaGrilla = GridView_SECCIONES.Rows[tablaDesdeGrilla.Rows.Count - 1];
        for (int i = 4; i < GridView_SECCIONES.Columns.Count; i++)
        {
            filaGrilla.Cells[i].Enabled = true;
        }

        HiddenField_ACCION_SECCION.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_SECCION.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();
        HiddenField_ID_SECCION.Value = "";
        HiddenField_DESCRIPCION_SECCION.Value = "";

        ocultar_paneles_generales(true, false, false, false);

        acciones_sobre_botones_deacicon_grillas(Button_NUEVO_SECCION, Button_GUARDAR_SECCION, Button_CANCELAR_SECCION, false, true, true);
    }
    protected void Button_OK_SECCION_Click(object sender, EventArgs e)
    {
        int filaSeleccionada = 0;
        GridViewRow filaGrilla;
        TextBox datoGrilla;
        String ID_SELECCIONADO = null;
        String NOMBRE = null;
        Boolean verificador = true;

        if (HiddenField_GRIILA_SELECCIONADA_ACTUALMENTE.Value == GrillasFormulario.Secciones.ToString())
        {
            filaSeleccionada = Convert.ToInt32(HiddenField_FILA_SECCION.Value);
            filaGrilla = GridView_SECCIONES.Rows[filaSeleccionada];

            datoGrilla = filaGrilla.FindControl("TextBox_ID_SECCION") as TextBox;
            ID_SELECCIONADO = datoGrilla.Text.Trim().ToUpper();

            datoGrilla = filaGrilla.FindControl("TextBox_DESCRIPCION_SECCION") as TextBox;
            NOMBRE = datoGrilla.Text.Trim().ToUpper();

            seccion _seccion = new seccion(Session["idEmpresa"].ToString());

            ocultar_mensaje(Panel_FONDO_MENSAJE_SECCION, Panel_MENSAJE_ACCION_SECCION);

            if (HiddenField_ACCION_SECCION.Value == AccionesGrilla.modificar.ToString())
            {
                verificador = _seccion.ActualizarSeccion(ID_SELECCIONADO, NOMBRE, Session["USU_LOG"].ToString());

                if (verificador == false)
                {
                    Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, _seccion.MensajError, Proceso.Error);
                }
                else
                {
                    Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, "Se modificó correctamente la SECCIÓN.", Proceso.Correcto);
                }
            }
            else
            {
                verificador = _seccion.EliminarSeccion(ID_SELECCIONADO, Session["USU_LOG"].ToString());

                if (verificador == false)
                {
                    Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, _seccion.MensajError, Proceso.Error);
                }
                else
                {
                    Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, "Se eliminó correctamente la SECCIÓN.", Proceso.Correcto);
                }
            }

            cargar_GridView_SECCIONES();

            acciones_sobre_botones_deacicon_grillas(Button_NUEVO_SECCION, Button_GUARDAR_SECCION, Button_CANCELAR_SECCION, true, false, false);
        }
        else
        {
            if (HiddenField_GRIILA_SELECCIONADA_ACTUALMENTE.Value == GrillasFormulario.Divisiones.ToString())
            {
                filaSeleccionada = Convert.ToInt32(HiddenField_FILA_DIVISION.Value);
                filaGrilla = GridView_DIVISIONES.Rows[filaSeleccionada];

                datoGrilla = filaGrilla.FindControl("TextBox_ID_DIVISION") as TextBox;
                ID_SELECCIONADO = datoGrilla.Text.Trim().ToUpper();

                datoGrilla = filaGrilla.FindControl("TextBox_DESCRIPCION_DIVISION") as TextBox;
                NOMBRE = datoGrilla.Text.Trim().ToUpper();

                division _division = new division(Session["idEmpresa"].ToString());

                ocultar_mensaje(Panel_FONDO_MENSAJE_SECCION, Panel_MENSAJE_ACCION_SECCION);

                if (HiddenField_ACCION_DIVISION.Value == AccionesGrilla.modificar.ToString())
                {
                    verificador = _division.ActualizarDivision(ID_SELECCIONADO, NOMBRE, Session["USU_LOG"].ToString());

                    if (verificador == false)
                    {
                        Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, _division.MensajError, Proceso.Error);
                    }
                    else
                    {
                        Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, "Se modificó correctamente la DIVISIÓN.", Proceso.Correcto);
                    }
                }
                else
                {
                    verificador = _division.EliminarDivision(ID_SELECCIONADO, Session["USU_LOG"].ToString());

                    if (verificador == false)
                    {
                        Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, _division.MensajError, Proceso.Error);
                    }
                    else
                    {
                        Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, "Se eliminó correctamente la DIVISIÓN.", Proceso.Correcto);
                    }
                }

                String ID_SECCION = HiddenField_ID_SECCION.Value;
                cargar_GridView_DIVISIONES(ID_SECCION);

                acciones_sobre_botones_deacicon_grillas(Button_NUEVA_DIVISION, Button_GUARDAR_DIVISION, Button_CANCELAR_DIVISION, true, false, false);
            }
            else
            {
                if (HiddenField_GRIILA_SELECCIONADA_ACTUALMENTE.Value == GrillasFormulario.Clases.ToString())
                {
                    filaSeleccionada = Convert.ToInt32(HiddenField_FILA_CLASE.Value);
                    filaGrilla = GridView_CLASES.Rows[filaSeleccionada];

                    datoGrilla = filaGrilla.FindControl("TextBox_ID_CLASE") as TextBox;
                    ID_SELECCIONADO = datoGrilla.Text.Trim().ToUpper();

                    datoGrilla = filaGrilla.FindControl("TextBox_DESCRIPCION_CLASE") as TextBox;
                    NOMBRE = datoGrilla.Text.Trim().ToUpper();

                    clase _clase = new clase(Session["idEmpresa"].ToString());

                    ocultar_mensaje(Panel_FONDO_MENSAJE_SECCION, Panel_MENSAJE_ACCION_SECCION);

                    if (HiddenField_ACCION_CLASE.Value == AccionesGrilla.modificar.ToString())
                    {
                        verificador = _clase.ActualizarClase(ID_SELECCIONADO, NOMBRE, Session["USU_LOG"].ToString());

                        if (verificador == false)
                        {
                            Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, _clase.MensajError, Proceso.Error);
                        }
                        else
                        {
                            Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, "Se modificó correctamente la CLASE.", Proceso.Correcto);
                        }
                    }
                    else
                    {
                        verificador = _clase.EliminarClase(ID_SELECCIONADO, Session["USU_LOG"].ToString());

                        if (verificador == false)
                        {
                            Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, _clase.MensajError, Proceso.Error);
                        }
                        else
                        {
                            Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, "Se eliminó correctamente la CLASE.", Proceso.Correcto);
                        }
                    }

                    String ID_DIVISION = HiddenField_ID_DIVISION.Value;
                    cargar_GridView_CLASES(ID_DIVISION);

                    acciones_sobre_botones_deacicon_grillas(Button_NUEVO_CLASE, Button_GUARDAR_CLASE, Button_CANCELAR_CLASE, true, false, false);
                }
                else
                {
                    if (HiddenField_GRIILA_SELECCIONADA_ACTUALMENTE.Value == GrillasFormulario.Actividades.ToString())
                    {
                        filaSeleccionada = Convert.ToInt32(HiddenField_FILA_ACTIVIDAD.Value);
                        filaGrilla = GridView_ACTIVIDADES.Rows[filaSeleccionada];

                        datoGrilla = filaGrilla.FindControl("TextBox_ID_ACTIVIDAD") as TextBox;
                        ID_SELECCIONADO = datoGrilla.Text.Trim().ToUpper();

                        datoGrilla = filaGrilla.FindControl("TextBox_DESCRIPCION_ACTIVIDAD") as TextBox;
                        NOMBRE = datoGrilla.Text.Trim().ToUpper();

                        actividad _actividad = new actividad(Session["idEmpresa"].ToString());

                        ocultar_mensaje(Panel_FONDO_MENSAJE_SECCION, Panel_MENSAJE_ACCION_SECCION);

                        if (HiddenField_ACCION_ACTIVIDAD.Value == AccionesGrilla.modificar.ToString())
                        {
                            verificador = _actividad.ActualizarActividad(ID_SELECCIONADO, NOMBRE, Session["USU_LOG"].ToString());

                            if (verificador == false)
                            {
                                Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, _actividad.MensajError, Proceso.Error);
                            }
                            else
                            {
                                Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, "Se modificó correctamente la ACTIVIDAD.", Proceso.Correcto);
                            }
                        }
                        else
                        {
                            verificador = _actividad.EliminarActividad(ID_SELECCIONADO, Session["USU_LOG"].ToString());

                            if (verificador == false)
                            {
                                Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, _actividad.MensajError, Proceso.Error);
                            }
                            else
                            {
                                Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_SECCION_POPUP, Panel_MENSAJE_SECCION, Label_MENSAJE_SECCION, "Se eliminó correctamente la ACTIVIDAD.", Proceso.Correcto);
                            }
                        }

                        String ID_CLASE = HiddenField_ID_CLASE.Value;
                        cargar_GridView_ACTIVIDADES(ID_CLASE);

                        acciones_sobre_botones_deacicon_grillas(Button_NUEVO_ACTIVIDAD, Button_GUARDAR_ACTIVIDAD, Button_CANCELAR_ACTIVIDAD, true, false, false);
                    }
                }
            }
        }
    }

    protected void Button_CANCEL_SECCION_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE_SECCION, Panel_MENSAJE_ACCION_SECCION);

        if (HiddenField_GRIILA_SELECCIONADA_ACTUALMENTE.Value == GrillasFormulario.Secciones.ToString())
        {
            cargar_GridView_SECCIONES();
            acciones_sobre_botones_deacicon_grillas(Button_NUEVO_SECCION, Button_GUARDAR_SECCION, Button_CANCELAR_SECCION, true, false, false);
        }
        else
        {
            if (HiddenField_GRIILA_SELECCIONADA_ACTUALMENTE.Value == GrillasFormulario.Divisiones.ToString())
            {
                String ID_SECCION = HiddenField_ID_SECCION.Value;
                cargar_GridView_DIVISIONES(ID_SECCION);

                acciones_sobre_botones_deacicon_grillas(Button_NUEVA_DIVISION, Button_GUARDAR_DIVISION, Button_CANCELAR_DIVISION, true, false, false);
            }
            else
            {
                if (HiddenField_GRIILA_SELECCIONADA_ACTUALMENTE.Value == GrillasFormulario.Clases.ToString())
                {
                    String ID_DIVISION = HiddenField_ID_DIVISION.Value;
                    cargar_GridView_CLASES(ID_DIVISION);

                    acciones_sobre_botones_deacicon_grillas(Button_NUEVO_CLASE, Button_GUARDAR_CLASE, Button_CANCELAR_CLASE, true, false, false);
                }
                else
                {
                    if (HiddenField_GRIILA_SELECCIONADA_ACTUALMENTE.Value == GrillasFormulario.Actividades.ToString())
                    {
                        String ID_CLASE = HiddenField_ID_CLASE.Value;
                        cargar_GridView_ACTIVIDADES(ID_CLASE);

                        acciones_sobre_botones_deacicon_grillas(Button_NUEVO_ACTIVIDAD, Button_GUARDAR_ACTIVIDAD, Button_CANCELAR_ACTIVIDAD, true, false, false);
                    }
                }
            }
        }
    }
    protected void Button_CERRAR_MENSAJE_SECCION_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE_SECCION, Panel_MENSAJE_SECCION);
    }
    protected void Button_NUEVA_DIVISION_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerDataTableDe_GridView_DIVISIONES();
        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_DIVISION"] = "";
        filaNueva["NOMBRE"] = "";

        tablaDesdeGrilla.Rows.Add(filaNueva);

        GridViewRow filaGrilla;

        cargar_GridView_DIVISIONES_desde_tabla(tablaDesdeGrilla);

        admin_columnas_grilla(GridView_DIVISIONES, false, false, false, false);
        filaGrilla = GridView_DIVISIONES.Rows[tablaDesdeGrilla.Rows.Count - 1];
        for (int i = 4; i < GridView_DIVISIONES.Columns.Count; i++)
        {
            filaGrilla.Cells[i].Enabled = true;
        }

        HiddenField_ACCION_DIVISION.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_DIVISION.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();
        HiddenField_ID_DIVISION.Value = "";
        HiddenField_DESCRIPCION_DIVISION.Value = "";

        ocultar_paneles_generales(true, true, false, false);

        acciones_sobre_botones_deacicon_grillas(Button_NUEVA_DIVISION, Button_GUARDAR_DIVISION, Button_CANCELAR_DIVISION, false, true, true);
    }

    protected void Button_GUARDAR_DIVISION_Click(object sender, EventArgs e)
    {
        ocultar_paneles_generales(true, true, false, false);

        HiddenField_GRIILA_SELECCIONADA_ACTUALMENTE.Value = GrillasFormulario.Divisiones.ToString();

        if (HiddenField_ACCION_DIVISION.Value == AccionesGrilla.modificar.ToString())
        {
            Modificar();
        }
        else
        {
            if (HiddenField_ACCION_DIVISION.Value == AccionesGrilla.Nuevo.ToString())
            {
                Guardar_DIVISION();
            }
        }
    }

    protected void Button_CANCELAR_DIVISION_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = obtenerDataTableDe_GridView_DIVISIONES();

        if (HiddenField_ACCION_DIVISION.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }
        else
        {
            if (HiddenField_ACCION_DIVISION.Value == AccionesGrilla.modificar.ToString())
            {
                DataRow filaGrilla = tablaGrilla.Rows[Convert.ToInt32(HiddenField_FILA_DIVISION.Value)];

                filaGrilla["ID_DIVISION"] = HiddenField_ID_DIVISION.Value;
                filaGrilla["NOMBRE"] = HiddenField_DESCRIPCION_DIVISION.Value;
            }
        }

        cargar_GridView_DIVISIONES_desde_tabla(tablaGrilla);

        admin_columnas_grilla(GridView_DIVISIONES, true, true, true, false);

        HiddenField_ACCION_DIVISION.Value = AccionesGrilla.Ninguna.ToString();

        ocultar_paneles_generales(true, true, false, false);

        acciones_sobre_botones_deacicon_grillas(Button_NUEVA_DIVISION, Button_GUARDAR_DIVISION, Button_CANCELAR_DIVISION, true, false, false);
    }
    protected void GridView_DIVISIONES_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        HiddenField_GRIILA_SELECCIONADA_ACTUALMENTE.Value = GrillasFormulario.Divisiones.ToString();

        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        TextBox datoGrilla;

        HiddenField_FILA_DIVISION.Value = indexSeleccionado.ToString();
        HiddenField_ID_DIVISION.Value = GridView_DIVISIONES.DataKeys[indexSeleccionado].Values["ID_DIVISION"].ToString();

        datoGrilla = GridView_DIVISIONES.Rows[indexSeleccionado].FindControl("TextBox_DESCRIPCION_DIVISION") as TextBox;
        HiddenField_DESCRIPCION_DIVISION.Value = datoGrilla.Text.Trim();

        if (e.CommandName == "modificar")
        {
            HiddenField_ACCION_DIVISION.Value = AccionesGrilla.modificar.ToString();

            habilitar_fila_grilla(GridView_DIVISIONES, indexSeleccionado, 5, false, false, false);

            ocultar_paneles_generales(true, true, false, false);

            acciones_sobre_botones_deacicon_grillas(Button_NUEVA_DIVISION, Button_GUARDAR_DIVISION, Button_CANCELAR_DIVISION, false, true, true);
        }
        else
        {
            if (e.CommandName == "eliminar")
            {
                HiddenField_ACCION_DIVISION.Value = AccionesGrilla.Eliminar.ToString();

                ocultar_paneles_generales(true, true, false, false);

                acciones_sobre_botones_deacicon_grillas(Button_NUEVA_DIVISION, Button_GUARDAR_DIVISION, Button_CANCELAR_DIVISION, false, false, false);

                division _division = new division(Session["idEmpresa"].ToString());
                DataTable tablaInfoDivision = _division.ObtenerClasesActidadesEmpresaPorIdDivision(HiddenField_ID_DIVISION.Value);
                DataRow filaInfoDivision = tablaInfoDivision.Rows[0];

                Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_ACCION_SECCION, Panel_MENSAJE_ACCION_SECCION, Label_MENSAJE_ACCION_SECCION, "Al eliminar la DIVISIÓN seleccionada se verán afectadas " + filaInfoDivision["NUM_CLASES"].ToString() + " CLASES, " + filaInfoDivision["NUM_ACTIVIDADES"].ToString() + " ACTIVIDADES y " + filaInfoDivision["NUM_EMPRESAS"].ToString() + " EMPRESAS, Desea continuar?", Proceso.Pregunta);
            }
            else
            {
                if (e.CommandName == "seleccionar")
                {
                    HiddenField_ACCION_DIVISION.Value = AccionesGrilla.Seleccionar.ToString();

                    seleccionarFilaDeGrilla(GridView_DIVISIONES, indexSeleccionado);

                    cargar_GridView_CLASES(GridView_DIVISIONES.DataKeys[indexSeleccionado].Values["ID_DIVISION"].ToString());

                    ocultar_paneles_generales(true, true, true, false);

                    acciones_sobre_botones_deacicon_grillas(Button_NUEVO_CLASE, Button_GUARDAR_CLASE, Button_CANCELAR_CLASE, true, false, false);

                    HiddenField_ACCION_CLASE.Value = AccionesGrilla.Ninguna.ToString();
                    HiddenField_FILA_CLASE.Value = null;
                    HiddenField_ID_CLASE.Value = null;
                    HiddenField_DESCRIPCION_CLASE.Value = null;
                }
            }
        }
    }
    protected void Button_GUARDAR_CLASE_Click(object sender, EventArgs e)
    {
        ocultar_paneles_generales(true, true, true, false);

        HiddenField_GRIILA_SELECCIONADA_ACTUALMENTE.Value = GrillasFormulario.Clases.ToString();

        if (HiddenField_ACCION_CLASE.Value == AccionesGrilla.modificar.ToString())
        {
            Modificar();
        }
        else
        {
            if (HiddenField_ACCION_CLASE.Value == AccionesGrilla.Nuevo.ToString())
            {
                Guardar_CLASE();
            }
        }
    }
    protected void Button_NUEVO_CLASE_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerDataTableDe_GridView_CLASES();
        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_CLASE"] = HiddenField_ID_DIVISION.Value;
        filaNueva["NOMBRE"] = "";

        tablaDesdeGrilla.Rows.Add(filaNueva);

        GridViewRow filaGrilla;

        cargar_GridView_CLASES_desde_tabla(tablaDesdeGrilla);

        admin_columnas_grilla(GridView_CLASES, false, false, false, false);
        filaGrilla = GridView_CLASES.Rows[tablaDesdeGrilla.Rows.Count - 1];
        for (int i = 4; i < GridView_CLASES.Columns.Count; i++)
        {
            filaGrilla.Cells[i].Enabled = true;
        }

        HiddenField_ACCION_CLASE.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_CLASE.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();
        HiddenField_ID_CLASE.Value = "";
        HiddenField_DESCRIPCION_CLASE.Value = "";

        ocultar_paneles_generales(true, true, true, false);

        acciones_sobre_botones_deacicon_grillas(Button_NUEVO_CLASE, Button_GUARDAR_CLASE, Button_CANCELAR_CLASE, false, true, true);
    }
    protected void Button_CANCELAR_CLASE_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = obtenerDataTableDe_GridView_CLASES();

        if (HiddenField_ACCION_CLASE.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }
        else
        {
            if (HiddenField_ACCION_CLASE.Value == AccionesGrilla.modificar.ToString())
            {
                DataRow filaGrilla = tablaGrilla.Rows[Convert.ToInt32(HiddenField_FILA_CLASE.Value)];

                filaGrilla["ID_CLASE"] = HiddenField_ID_CLASE.Value;
                filaGrilla["NOMBRE"] = HiddenField_DESCRIPCION_CLASE.Value;
            }
        }

        cargar_GridView_CLASES_desde_tabla(tablaGrilla);

        admin_columnas_grilla(GridView_CLASES, true, true, true, false);

        HiddenField_ACCION_CLASE.Value = AccionesGrilla.Ninguna.ToString();

        ocultar_paneles_generales(true, true, true, false);

        acciones_sobre_botones_deacicon_grillas(Button_NUEVO_CLASE, Button_GUARDAR_CLASE, Button_CANCELAR_CLASE, true, false, false);
    }
    protected void GridView_CLASES_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        HiddenField_GRIILA_SELECCIONADA_ACTUALMENTE.Value = GrillasFormulario.Clases.ToString();

        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        TextBox datoGrilla;

        HiddenField_FILA_CLASE.Value = indexSeleccionado.ToString();
        HiddenField_ID_CLASE.Value = GridView_CLASES.DataKeys[indexSeleccionado].Values["ID_CLASE"].ToString();

        datoGrilla = GridView_CLASES.Rows[indexSeleccionado].FindControl("TextBox_DESCRIPCION_CLASE") as TextBox;
        HiddenField_DESCRIPCION_CLASE.Value = datoGrilla.Text.Trim();

        if (e.CommandName == "modificar")
        {
            HiddenField_ACCION_CLASE.Value = AccionesGrilla.modificar.ToString();

            habilitar_fila_grilla(GridView_CLASES, indexSeleccionado, 5, false, false, false);

            ocultar_paneles_generales(true, true, true, false);

            acciones_sobre_botones_deacicon_grillas(Button_NUEVO_CLASE, Button_GUARDAR_CLASE, Button_CANCELAR_CLASE, false, true, true);
        }
        else
        {
            if (e.CommandName == "eliminar")
            {
                HiddenField_ACCION_CLASE.Value = AccionesGrilla.Eliminar.ToString();

                ocultar_paneles_generales(true, true, true, false);

                acciones_sobre_botones_deacicon_grillas(Button_NUEVO_CLASE, Button_GUARDAR_CLASE, Button_CANCELAR_CLASE, false, false, false);

                clase _clase = new clase(Session["idEmpresa"].ToString());
                DataTable tablaInfoClase = _clase.ObtenerActidadesEmpresaPorIdClase(HiddenField_ACCION_CLASE.Value);
                DataRow filaInfoClase = tablaInfoClase.Rows[0];

                Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_ACCION_SECCION, Panel_MENSAJE_ACCION_SECCION, Label_MENSAJE_ACCION_SECCION, "Al eliminar la CLASE seleccionada se verán afectadas " + filaInfoClase["NUM_ACTIVIDADES"].ToString() + " ACTIVIDADES y " + filaInfoClase["NUM_EMPRESAS"].ToString() + " EMPRESAS, Desea continuar?", Proceso.Pregunta);
            }
            else
            {
                if (e.CommandName == "seleccionar")
                {
                    HiddenField_ACCION_CLASE.Value = AccionesGrilla.Seleccionar.ToString();

                    seleccionarFilaDeGrilla(GridView_CLASES, indexSeleccionado);

                    cargar_GridView_ACTIVIDADES(GridView_CLASES.DataKeys[indexSeleccionado].Values["ID_CLASE"].ToString());

                    ocultar_paneles_generales(true, true, true, true);

                    acciones_sobre_botones_deacicon_grillas(Button_NUEVO_ACTIVIDAD, Button_GUARDAR_ACTIVIDAD, Button_CANCELAR_ACTIVIDAD, true, false, false);

                    admin_columnas_grilla(GridView_ACTIVIDADES, false, true, true, false);

                    HiddenField_ACCION_ACTIVIDAD.Value = AccionesGrilla.Ninguna.ToString();
                    HiddenField_FILA_ACTIVIDAD.Value = null;
                    HiddenField_ID_ACTIVIDAD.Value = null;
                    HiddenField_DESCRIPCION_ACTIVIDAD.Value = null;
                }
            }
        }
    }
    protected void GridView_ACTIVIDADES_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        HiddenField_GRIILA_SELECCIONADA_ACTUALMENTE.Value = GrillasFormulario.Actividades.ToString();

        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        TextBox datoGrilla;

        HiddenField_FILA_ACTIVIDAD.Value = indexSeleccionado.ToString();
        HiddenField_ID_ACTIVIDAD.Value = GridView_ACTIVIDADES.DataKeys[indexSeleccionado].Values["ID_ACTIVIDAD"].ToString();

        datoGrilla = GridView_ACTIVIDADES.Rows[indexSeleccionado].FindControl("TextBox_DESCRIPCION_ACTIVIDAD") as TextBox;
        HiddenField_DESCRIPCION_ACTIVIDAD.Value = datoGrilla.Text.Trim();

        if (e.CommandName == "modificar")
        {
            HiddenField_ACCION_ACTIVIDAD.Value = AccionesGrilla.modificar.ToString();

            habilitar_fila_grilla(GridView_ACTIVIDADES, indexSeleccionado, 5, false, false, false);

            ocultar_paneles_generales(true, true, true, true);

            acciones_sobre_botones_deacicon_grillas(Button_NUEVO_ACTIVIDAD, Button_GUARDAR_ACTIVIDAD, Button_CANCELAR_ACTIVIDAD, false, true, true);
        }
        else
        {
            if (e.CommandName == "eliminar")
            {
                HiddenField_ACCION_ACTIVIDAD.Value = AccionesGrilla.Eliminar.ToString();

                ocultar_paneles_generales(true, true, true, true);

                acciones_sobre_botones_deacicon_grillas(Button_NUEVO_ACTIVIDAD, Button_GUARDAR_ACTIVIDAD, Button_CANCELAR_ACTIVIDAD, false, false, false);

                actividad _actividad = new actividad(Session["idEmpresa"].ToString());
                DataTable tablaInfoActividad = _actividad.ObtenerEmpresaPorIdActividad(HiddenField_ACCION_ACTIVIDAD.Value);
                DataRow filaInfoActividad = tablaInfoActividad.Rows[0];

                Informar(Panel_FONDO_MENSAJE_SECCION, Image_MENSAJE_ACCION_SECCION, Panel_MENSAJE_ACCION_SECCION, Label_MENSAJE_ACCION_SECCION, "Al eliminar la ACTIVIDAD seleccionada se verán afectadas " + filaInfoActividad["NUM_EMPRESAS"].ToString() + " EMPRESAS, Desea continuar?", Proceso.Pregunta);
            }
        }
    }
    protected void Button_NUEVO_ACTIVIDAD_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerDataTableDe_GridView_ACTIVIDADES();
        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_ACTIVIDAD"] = HiddenField_ID_CLASE.Value;
        filaNueva["NOMBRE"] = "";

        tablaDesdeGrilla.Rows.Add(filaNueva);

        GridViewRow filaGrilla;

        cargar_GridView_ACTIVIDADES_desde_tabla(tablaDesdeGrilla);

        admin_columnas_grilla(GridView_ACTIVIDADES, false, false, false, false);
        filaGrilla = GridView_ACTIVIDADES.Rows[tablaDesdeGrilla.Rows.Count - 1];
        for (int i = 4; i < GridView_ACTIVIDADES.Columns.Count; i++)
        {
            filaGrilla.Cells[i].Enabled = true;
        }

        HiddenField_ACCION_ACTIVIDAD.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_ACTIVIDAD.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();
        HiddenField_ID_ACTIVIDAD.Value = "";
        HiddenField_DESCRIPCION_ACTIVIDAD.Value = "";

        ocultar_paneles_generales(true, true, true, true);

        ocultar_paneles_generales(true, true, true, true);

        acciones_sobre_botones_deacicon_grillas(Button_NUEVO_ACTIVIDAD, Button_GUARDAR_ACTIVIDAD, Button_CANCELAR_ACTIVIDAD, false, true, true);
    }
    protected void Button_CANCELAR_ACTIVIDAD_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = obtenerDataTableDe_GridView_ACTIVIDADES();

        if (HiddenField_ACCION_ACTIVIDAD.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }
        else
        {
            if (HiddenField_ACCION_ACTIVIDAD.Value == AccionesGrilla.modificar.ToString())
            {
                DataRow filaGrilla = tablaGrilla.Rows[Convert.ToInt32(HiddenField_FILA_ACTIVIDAD.Value)];

                filaGrilla["ID_ACTIVIDAD"] = HiddenField_ID_ACTIVIDAD.Value;
                filaGrilla["NOMBRE"] = HiddenField_DESCRIPCION_ACTIVIDAD.Value;
            }
        }

        cargar_GridView_ACTIVIDADES_desde_tabla(tablaGrilla);

        admin_columnas_grilla(GridView_ACTIVIDADES, false, true, true, false);

        HiddenField_ACCION_ACTIVIDAD.Value = AccionesGrilla.Ninguna.ToString();

        ocultar_paneles_generales(true, true, true, true);

        acciones_sobre_botones_deacicon_grillas(Button_NUEVO_ACTIVIDAD, Button_GUARDAR_ACTIVIDAD, Button_CANCELAR_ACTIVIDAD, true, false, false);
    }
    protected void Button_GUARDAR_ACTIVIDAD_Click(object sender, EventArgs e)
    {
        ocultar_paneles_generales(true, true, true, true);

        HiddenField_GRIILA_SELECCIONADA_ACTUALMENTE.Value = GrillasFormulario.Actividades.ToString();

        if (HiddenField_ACCION_ACTIVIDAD.Value == AccionesGrilla.modificar.ToString())
        {
            Modificar();
        }
        else
        {
            if (HiddenField_ACCION_ACTIVIDAD.Value == AccionesGrilla.Nuevo.ToString())
            {
                Guardar_ACTIVIDAD();
            }
        }
    }
}