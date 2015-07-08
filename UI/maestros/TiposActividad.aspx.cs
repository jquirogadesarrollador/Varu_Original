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
using System.Data;
using Brainsbits.LLB.seguridad;

public partial class _TiposActividad : System.Web.UI.Page
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

                Panel_TIPOS.Visible = false;
                Button_NUEVO_TIPO.Visible = false;
                Button_GUARDAR_TIPO.Visible = false;
                Button_CANCELAR_TIPO.Visible = false;

                Panel_FORM_BOTONES_ABAJO.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;

                GridView_Tipos.Columns[0].Visible = false;
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

    private void ConfigurarAreaRseGlobal()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        int proceso = Convert.ToInt32(QueryStringSeguro["proceso"]);

        HiddenField_ID_AREA.Value = _tools.ObtenerIdAreaProceso(proceso);
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

                Panel_TIPOS.Visible = true;

                Button_NUEVO_TIPO.Visible = true;

                Panel_FORM_BOTONES_ABAJO.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;

                GridView_Tipos.Columns[0].Visible = true;
                break;
            case Acciones.Cargar:
                Panel_FORM_BOTONES.Visible = true;
                Button_MODIFICAR.Visible = true;

                Panel_TIPOS.Visible = true;

                Panel_FORM_BOTONES_ABAJO.Visible = true;
                Button_MODIFICAR_1.Visible = true;
                break;
            case Acciones.Modificar:
                Panel_FORM_BOTONES.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;
                Panel_TIPOS.Visible = true;
                Button_NUEVO_TIPO.Visible = true;
                Panel_FORM_BOTONES_ABAJO.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;

                GridView_Tipos.Columns[0].Visible = true;
                break;
        }
    }

    private void CargarGrillaTiposDesdeTabla(DataTable tablaTipos)
    {
        GridView_Tipos.DataSource = tablaTipos;
        GridView_Tipos.DataBind();

        for (int i = 0; i < GridView_Tipos.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_Tipos.Rows[i];
            DataRow filaTabla = tablaTipos.Rows[i];

            TextBox textoNombre = filaGrilla.FindControl("TextBox_Nombre") as TextBox;
            textoNombre.Text = filaTabla["NOMBRE"].ToString().Trim();

            DropDownList dropEstado = filaGrilla.FindControl("DropDownList_Estado") as DropDownList;
            dropEstado.SelectedValue = filaTabla["ACTIVA"].ToString();

            CheckBoxList checkSecciones = filaGrilla.FindControl("CheckBoxList_Secciones") as CheckBoxList;
            String seccionesHabilitadas = filaTabla["SECCIONES_HABILITADAS"].ToString().Trim();
            checkSecciones.ClearSelection();
            if(seccionesHabilitadas.Contains("Resultados Encuesta") == true)
            {
                checkSecciones.Items[0].Selected = true;
            }

            if (seccionesHabilitadas.Contains("Control Asistencia") == true)
            {
                checkSecciones.Items[1].Selected = true;
            }
            
            if (seccionesHabilitadas.Contains("Entidades Colaboradoras") == true)
            {
                checkSecciones.Items[2].Selected = true;
            }
            
            if (seccionesHabilitadas.Contains("Compromisos") == true)
            {
                checkSecciones.Items[3].Selected = true;
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

    private void cargarGrillaTipos()
    {
        Programa.Areas AREA_PROGRAMA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);
        
        TipoActividad _tipoActividad = new TipoActividad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaTipos = _tipoActividad.ObtenerTiposActividadPorArea(AREA_PROGRAMA);

        if (tablaTipos.Rows.Count <= 0)
        {
            if (_tipoActividad.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _tipoActividad.MensajeError, Proceso.Error);
            }
            else
            {
                Mostrar(Acciones.Nuevo);
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron Tipos de Actividad configurados.", Proceso.Advertencia);
            }

            GridView_Tipos.DataSource = null;
            GridView_Tipos.DataBind();
        }
        else
        {
            Mostrar(Acciones.Cargar);

            CargarGrillaTiposDesdeTabla(tablaTipos);

            inhabilitarFilasGrilla(GridView_Tipos, 1);
        }
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                ConfigurarAreaRseGlobal();

                cargarGrillaTipos();
                break;
            case Acciones.Modificar:
                HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
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
        Page.Header.Title = "TIPOS ACTIVIDAD";

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

    private void Actualizar()
    {
        Programa.Areas AREA_PROGRAMA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);

        List<TipoActividad> listaTipos = new List<TipoActividad>();

        for (int i = 0; i < GridView_Tipos.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_Tipos.Rows[i];

            TipoActividad _tipoParaLista = new TipoActividad();

            Decimal ID_TIPO_ACTIVIDAD = Convert.ToDecimal(GridView_Tipos.DataKeys[i].Values["ID_TIPO_ACTIVIDAD"]);

            TextBox textoNombre = filaGrilla.FindControl("TextBox_Nombre") as TextBox;
            String NOMBRE = textoNombre.Text.Trim();

            DropDownList dropActiva = filaGrilla.FindControl("DropDownList_Estado") as DropDownList;
            Boolean ACTIVA = true;
            if (dropActiva.SelectedValue == "False")
            {
                ACTIVA = false;
            }

            CheckBoxList checkSecciones = filaGrilla.FindControl("CheckBoxList_Secciones") as CheckBoxList;
            String SECCIONES_HABILITADAS = ObtenerValuesSeleccionados(checkSecciones);


            _tipoParaLista.ACTIVA = ACTIVA;
            _tipoParaLista.ID_TIPO_ACTIVIDAD = ID_TIPO_ACTIVIDAD;
            _tipoParaLista.NOMBRE = NOMBRE;
            _tipoParaLista.SECCIONES_HABILITADAS = SECCIONES_HABILITADAS;

            listaTipos.Add(_tipoParaLista);
        }

        TipoActividad _tipo = new TipoActividad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean verificador = _tipo.ActualizarTipos(AREA_PROGRAMA, listaTipos);

        if (verificador == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _tipo.MensajeError, Proceso.Error);
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Cargar(Acciones.Inicio);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Los tipos de Actividad, fueron actualizados correctamente.", Proceso.Correcto);
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
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Para poder guardar los cambios primero debe terminar las acciones sobre la grilla de PREGUNTAS.", Proceso.Advertencia);
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

    protected void GridView_Tipos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "modificar")
        {
            HiddenField_FILA_SELECCIONADA_GRILLA.Value = indexSeleccionado.ToString();

            ActivarFilaGrilla(GridView_Tipos, indexSeleccionado, 1);

            HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Modificar.ToString();
            HiddenField_FILA_SELECCIONADA_GRILLA.Value = indexSeleccionado.ToString();

            HiddenField_ID_TIPO_ACTIVIDAD.Value = GridView_Tipos.DataKeys[indexSeleccionado].Values["ID_TIPO_ACTIVIDAD"].ToString();

            TextBox textoNombre = GridView_Tipos.Rows[indexSeleccionado].FindControl("TextBox_Nombre") as TextBox;
            HiddenField_NOMBRE.Value = textoNombre.Text;

            DropDownList dropEstado = GridView_Tipos.Rows[indexSeleccionado].FindControl("DropDownList_Estado") as DropDownList;
            HiddenField_ACTIVA.Value = dropEstado.SelectedValue;

            CheckBoxList checkSecciones = GridView_Tipos.Rows[indexSeleccionado].FindControl("CheckBoxList_Secciones") as CheckBoxList;
            HiddenField_SECCIONES_HABILITADAS.Value = ObtenerValuesSeleccionados(checkSecciones);

            GridView_Tipos.Columns[0].Visible = false;

            Button_NUEVO_TIPO.Visible = false;
            Button_GUARDAR_TIPO.Visible = true;
            Button_CANCELAR_TIPO.Visible = true;
        }
    }

    private String ObtenerValuesSeleccionados(CheckBoxList checkSecciones)
    {
        String resultado = "";

        foreach(ListItem item in checkSecciones.Items)
        {
            if (item.Selected == true)
            {
                if (String.IsNullOrEmpty(resultado) == true)
                {
                    resultado = item.Value;
                }
                else
                {
                    resultado += "*" + item.Value;
                }
            }
        }

        return resultado;
    }

    private DataTable obtenerTablaDeGrillaTipos()
    {
        DataTable tablaResultado = new DataTable();
        tablaResultado.Columns.Add("ID_TIPO_ACTIVIDAD");
        tablaResultado.Columns.Add("NOMBRE");
        tablaResultado.Columns.Add("ACTIVA");
        tablaResultado.Columns.Add("SECCIONES_HABILITADAS");

        for (int i = 0; i < GridView_Tipos.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_Tipos.Rows[i];

            Decimal ID_TIPO_ACTIVIDAD = Convert.ToDecimal(GridView_Tipos.DataKeys[i].Values["ID_TIPO_ACTIVIDAD"]);

            TextBox textoNombre = filaGrilla.FindControl("TextBox_Nombre") as TextBox;
            String NOMBRE = textoNombre.Text;

            DropDownList dropActiva = filaGrilla.FindControl("DropDownList_Estado") as DropDownList;
            String ACTIVA = dropActiva.SelectedValue;

            CheckBoxList checkSecciones = filaGrilla.FindControl("CheckBoxList_Secciones") as CheckBoxList;
            String SECCIONES_HABILITADAS = ObtenerValuesSeleccionados(checkSecciones);

            DataRow filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_TIPO_ACTIVIDAD"] = ID_TIPO_ACTIVIDAD;
            filaTablaResultado["NOMBRE"] = NOMBRE;
            filaTablaResultado["ACTIVA"] = ACTIVA;
            filaTablaResultado["SECCIONES_HABILITADAS"] = SECCIONES_HABILITADAS;

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

    protected void Button_NUEVO_TIPO_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerTablaDeGrillaTipos();
        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_TIPO_ACTIVIDAD"] = 0;
        filaNueva["NOMBRE"] = "";
        filaNueva["ACTIVA"] = "";
        filaNueva["SECCIONES_HABILITADAS"] = "";

        tablaDesdeGrilla.Rows.Add(filaNueva);

        CargarGrillaTiposDesdeTabla(tablaDesdeGrilla);

        inhabilitarFilasGrilla(GridView_Tipos, 1);
        ActivarFilaGrilla(GridView_Tipos, GridView_Tipos.Rows.Count - 1, 1);

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_SELECCIONADA_GRILLA.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();

        GridView_Tipos.Columns[0].Visible = false;

        Button_NUEVO_TIPO.Visible = false;
        Button_GUARDAR_TIPO.Visible = true;
        Button_CANCELAR_TIPO.Visible = true;
    }

    private void inhabilitarFilaGrilla(GridView grilla, int fila, int colInicio)
    {
        for (int j = colInicio; j < grilla.Columns.Count; j++)
        {
            grilla.Rows[fila].Cells[j].Enabled = false;
        }
    }

    protected void Button_GUARDAR_TIPO_Click(object sender, EventArgs e)
    {
        int FILA_SELECCIONADA = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA.Value);

        inhabilitarFilaGrilla(GridView_Tipos, FILA_SELECCIONADA, 1);

        GridView_Tipos.Columns[0].Visible = true;

        Button_GUARDAR_TIPO.Visible = false;
        Button_CANCELAR_TIPO.Visible = false;
        Button_NUEVO_TIPO.Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
    }

    protected void Button_CANCELAR_TIPO_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = obtenerTablaDeGrillaTipos();

        if (HiddenField_ACCION_GRILLA.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }
        else
        {
            if (HiddenField_ACCION_GRILLA.Value == AccionesGrilla.Modificar.ToString())
            {
                DataRow filaGrilla = tablaGrilla.Rows[Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA.Value)];

                filaGrilla["ID_TIPO_ACTIVIDAD"] = HiddenField_ID_TIPO_ACTIVIDAD.Value;
                filaGrilla["NOMBRE"] = HiddenField_NOMBRE.Value;
                filaGrilla["ACTIVA"] = HiddenField_ACTIVA.Value;
                filaGrilla["SECCIONES_HABILITADAS"] = HiddenField_SECCIONES_HABILITADAS.Value;
            }
        }

        CargarGrillaTiposDesdeTabla(tablaGrilla);

        inhabilitarFilasGrilla(GridView_Tipos, 1);

        GridView_Tipos.Columns[0].Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();

        Button_NUEVO_TIPO.Visible = true;
        Button_GUARDAR_TIPO.Visible = false;
        Button_CANCELAR_TIPO.Visible = false;
    }
}