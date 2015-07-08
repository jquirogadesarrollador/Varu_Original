
using System.Web.UI.WebControls;
using Brainsbits.LLB.seleccion;
using System.Data;
using System;
using System.Collections.Generic;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;
using TSHAK.Components;
using System.Web;
using Brainsbits.LLB;
public partial class _DiccionarioCompetencias : System.Web.UI.Page
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

    private System.Drawing.Color colorAmarillo = System.Drawing.ColorTranslator.FromHtml("#FFF200");
    private System.Drawing.Color colorVerde = System.Drawing.ColorTranslator.FromHtml("#50CE04");
    private System.Drawing.Color colorGris = System.Drawing.ColorTranslator.FromHtml("#cccccc");

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
        List<competenciaEntrevista> listaCompetencias = new List<competenciaEntrevista>();
        competenciaEntrevista competenciaParaLista;

        for (int i = 0; i < GridView_COMPETENCIAS.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_COMPETENCIAS.Rows[i];
            competenciaParaLista = new competenciaEntrevista();

            TextBox datoCompetencia = filaGrilla.FindControl("TextBox_COMPETENCIA") as TextBox;
            competenciaParaLista.COMPETENCIA = datoCompetencia.Text;

            TextBox datoDefinicion = filaGrilla.FindControl("TextBox_DEFINICION") as TextBox;
            competenciaParaLista.DEFINICION = datoDefinicion.Text;

            competenciaParaLista.ID_COMPETENCIA = Convert.ToDecimal(GridView_COMPETENCIAS.DataKeys[i].Values["ID_COMPETENCIA"]);

            DropDownList dropArea = filaGrilla.FindControl("DropDownList_AreaCompetencia") as DropDownList;
            competenciaParaLista.AREA = dropArea.SelectedValue;

            listaCompetencias.Add(competenciaParaLista);
        }

        hojasVida _hojasVida = new hojasVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean verificador = _hojasVida.ActualizarCompetencias(listaCompetencias);

        if (verificador == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _hojasVida.MensajeError, Proceso.Error);
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Cargar(Acciones.Inicio);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Las competencias utilizadas para relaizar entrevistas, fueron actualizadas correctamente.", Proceso.Correcto);
        }
    }

    protected void Button_GUARDAR_Click(object sender, System.EventArgs e)
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
    protected void Button_MODIFICAR_Click(object sender, System.EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Modificar);
        Cargar(Acciones.Modificar);
    }
    protected void Button_CANCELAR_Click(object sender, System.EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }
    protected void GridView_COMPETENCIAS_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "modificar")
        {
            HiddenField_FILA_SELECCIONADA_GRILLA.Value = indexSeleccionado.ToString();

            ActivarFilaGrilla(GridView_COMPETENCIAS, indexSeleccionado, 2);

            TextBox dato;

            HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Modificar.ToString();
            HiddenField_FILA_SELECCIONADA_GRILLA.Value = indexSeleccionado.ToString();

            HiddenField_ID_COMPETENCIA.Value = GridView_COMPETENCIAS.DataKeys[indexSeleccionado].Values["ID_COMPETENCIA"].ToString();

            dato = GridView_COMPETENCIAS.Rows[indexSeleccionado].FindControl("TextBox_COMPETENCIA") as TextBox;
            HiddenField_COMPETENCIA.Value = dato.Text;

            dato = GridView_COMPETENCIAS.Rows[indexSeleccionado].FindControl("TextBox_DEFINICION") as TextBox;
            HiddenField_DEFINICION.Value = dato.Text;

            DropDownList dropArea = GridView_COMPETENCIAS.Rows[indexSeleccionado].FindControl("DropDownList_AreaCompetencia") as DropDownList;
            HiddenField_AREACOMPETENCIA.Value = dropArea.SelectedValue;

            GridView_COMPETENCIAS.Columns[0].Visible = false;
            GridView_COMPETENCIAS.Columns[1].Visible = false;

            Button_NUEVA_COMPETENCIA.Visible = false;
            Button_GUARDAR_COMPETENCIA.Visible = true;
            Button_CANCELAR_COMPETENCIA.Visible = true;
        }
        else
        {
            if (e.CommandName == "eliminar")
            {
                DataTable tablaDesdeGrilla = obtenerTablaDeGrillaCompetencias();

                tablaDesdeGrilla.Rows.RemoveAt(indexSeleccionado);

                CargarGrillaPreguntasDesdeTabla(tablaDesdeGrilla);

                inhabilitarFilasGrilla(GridView_COMPETENCIAS, 2);

                GridView_COMPETENCIAS.Columns[0].Visible = true;
                GridView_COMPETENCIAS.Columns[1].Visible = true;

                HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_FILA_SELECCIONADA_GRILLA.Value = null;
                HiddenField_ID_COMPETENCIA.Value = "";
                HiddenField_COMPETENCIA.Value = "";
                HiddenField_DEFINICION.Value = "";

                Button_NUEVA_COMPETENCIA.Visible = true;
                Button_GUARDAR_COMPETENCIA.Visible = false;
                Button_CANCELAR_COMPETENCIA.Visible = false;
            }
        }
    }

    private DataTable obtenerTablaDeGrillaCompetencias()
    {
        DataTable tablaResultado = new DataTable();
        tablaResultado.Columns.Add("ID_COMPETENCIA");
        tablaResultado.Columns.Add("COMPETENCIA");
        tablaResultado.Columns.Add("DEFINICION");
        tablaResultado.Columns.Add("ACTIVA");
        tablaResultado.Columns.Add("AREA");

        DataRow filaTablaResultado;

        Decimal ID_COMPETENCIA;
        String COMPETENCIA;
        String DEFINICION;
        Boolean ACTIVA = true;
        String AREA;

        GridViewRow filaGrilla;
        TextBox dato;
        for (int i = 0; i < GridView_COMPETENCIAS.Rows.Count; i++)
        {
            filaGrilla = GridView_COMPETENCIAS.Rows[i];

            ID_COMPETENCIA = Convert.ToDecimal(GridView_COMPETENCIAS.DataKeys[i].Values["ID_COMPETENCIA"]);

            dato = filaGrilla.FindControl("TextBox_COMPETENCIA") as TextBox;
            COMPETENCIA = dato.Text;

            dato = filaGrilla.FindControl("TextBox_DEFINICION") as TextBox;
            DEFINICION = dato.Text;

            DropDownList dropArea = filaGrilla.FindControl("DropDownList_AreaCompetencia") as DropDownList;
            AREA = dropArea.SelectedValue;

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_COMPETENCIA"] = ID_COMPETENCIA;
            filaTablaResultado["COMPETENCIA"] = COMPETENCIA;
            filaTablaResultado["DEFINICION"] = DEFINICION;
            filaTablaResultado["ACTIVA"] = ACTIVA;
            filaTablaResultado["AREA"] = AREA;

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


    protected void Button_NUEVA_COMPETENCIA_Click(object sender, System.EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerTablaDeGrillaCompetencias();
        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_COMPETENCIA"] = 0;
        filaNueva["COMPETENCIA"] = "";
        filaNueva["DEFINICION"] = "";
        filaNueva["ACTIVA"] = "True";
        filaNueva["AREA"] = "";

        tablaDesdeGrilla.Rows.Add(filaNueva);

        CargarGrillaPreguntasDesdeTabla(tablaDesdeGrilla);

        inhabilitarFilasGrilla(GridView_COMPETENCIAS, 2);
        ActivarFilaGrilla(GridView_COMPETENCIAS, GridView_COMPETENCIAS.Rows.Count - 1, 2);

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_SELECCIONADA_GRILLA.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();
        HiddenField_ID_COMPETENCIA.Value = null;
        HiddenField_COMPETENCIA.Value = null;
        HiddenField_DEFINICION.Value = null;
        HiddenField_AREACOMPETENCIA.Value = null;

        GridView_COMPETENCIAS.Columns[0].Visible = false;
        GridView_COMPETENCIAS.Columns[1].Visible = false;

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

    protected void Button_GUARDAR_COMPETENCIA_Click(object sender, System.EventArgs e)
    {
        int FILA_SELECCIONADA = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA.Value);

        inhabilitarFilaGrilla(GridView_COMPETENCIAS, FILA_SELECCIONADA, 2);

        GridView_COMPETENCIAS.Columns[0].Visible = true;
        GridView_COMPETENCIAS.Columns[1].Visible = true;

        Button_GUARDAR_COMPETENCIA.Visible = false;
        Button_CANCELAR_COMPETENCIA.Visible = false;
        Button_NUEVA_COMPETENCIA.Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
    }
    protected void Button_CANCELAR_COMPETENCIA_Click(object sender, System.EventArgs e)
    {
        DataTable tablaGrilla = obtenerTablaDeGrillaCompetencias();

        if (HiddenField_ACCION_GRILLA.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }
        else
        {
            if (HiddenField_ACCION_GRILLA.Value == AccionesGrilla.Modificar.ToString())
            {
                DataRow filaGrilla = tablaGrilla.Rows[Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA.Value)];

                filaGrilla["ID_COMPETENCIA"] = HiddenField_ID_COMPETENCIA.Value;
                filaGrilla["COMPETENCIA"] = HiddenField_COMPETENCIA.Value;
                filaGrilla["DEFINICION"] = HiddenField_DEFINICION.Value;
                filaGrilla["ACTIVA"] = "True";
                filaGrilla["AREA"] = HiddenField_AREACOMPETENCIA.Value;
            }
        }

        CargarGrillaPreguntasDesdeTabla(tablaGrilla);

        inhabilitarFilasGrilla(GridView_COMPETENCIAS, 2);

        GridView_COMPETENCIAS.Columns[0].Visible = true;
        GridView_COMPETENCIAS.Columns[1].Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();

        Button_NUEVA_COMPETENCIA.Visible = true;
        Button_GUARDAR_COMPETENCIA.Visible = false;
        Button_CANCELAR_COMPETENCIA.Visible = false;
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

                Panel_DICCIONARIO.Visible = false;
                Button_NUEVA_COMPETENCIA.Visible = false;
                Button_GUARDAR_COMPETENCIA.Visible = false;
                Button_CANCELAR_COMPETENCIA.Visible = false;

                Panel_FORM_BOTONES_ABAJO.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;

                GridView_COMPETENCIAS.Columns[0].Visible = false;
                GridView_COMPETENCIAS.Columns[1].Visible = false;
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

                Panel_DICCIONARIO.Visible = true;

                Button_NUEVA_COMPETENCIA.Visible = true;

                Panel_FORM_BOTONES_ABAJO.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;

                GridView_COMPETENCIAS.Columns[0].Visible = true;
                GridView_COMPETENCIAS.Columns[1].Visible = true;
                break;
            case Acciones.Cargar:
                Panel_FORM_BOTONES.Visible = true;
                Button_MODIFICAR.Visible = true;

                Panel_DICCIONARIO.Visible = true;

                Panel_FORM_BOTONES_ABAJO.Visible = true;
                Button_MODIFICAR_1.Visible = true;
                break;
            case Acciones.Modificar:
                Panel_FORM_BOTONES.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;
                Panel_DICCIONARIO.Visible = true;
                Button_NUEVA_COMPETENCIA.Visible = true;
                Panel_FORM_BOTONES_ABAJO.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;

                GridView_COMPETENCIAS.Columns[0].Visible = true;
                GridView_COMPETENCIAS.Columns[1].Visible = true;
                break;
        }
    }

    private void CargarGrillaPreguntasDesdeTabla(DataTable tablaCompetencias)
    {
        GridView_COMPETENCIAS.DataSource = tablaCompetencias;
        GridView_COMPETENCIAS.DataBind();

        for (int i = 0; i < GridView_COMPETENCIAS.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_COMPETENCIAS.Rows[i];
            DataRow filaTabla = tablaCompetencias.Rows[i];

            TextBox textoCOMPETENCIA = filaGrilla.FindControl("TextBox_COMPETENCIA") as TextBox;
            textoCOMPETENCIA.Text = filaTabla["COMPETENCIA"].ToString().Trim();

            TextBox textoDEFINICION = filaGrilla.FindControl("TextBox_DEFINICION") as TextBox;
            textoDEFINICION.Text = filaTabla["DEFINICION"].ToString().Trim();

            DropDownList dropArea = filaGrilla.FindControl("DropDownList_AreaCompetencia") as DropDownList;
            dropArea.SelectedValue = filaTabla["AREA"].ToString().Trim();

            if (filaTabla["AREA"].ToString().Trim() == "Gerenciales")
            {
                filaGrilla.BackColor = colorAmarillo;
            }
            else
            {
                if (filaTabla["AREA"].ToString().Trim() == "Comerciales")
                {
                    filaGrilla.BackColor = colorVerde;
                }
                else
                {
                    if (filaTabla["AREA"].ToString().Trim() == "Administrativas")
                    {
                        filaGrilla.BackColor = colorGris;
                    }
                    else
                    {
                        filaGrilla.BackColor = System.Drawing.Color.Transparent;
                    }
                }
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

    private void cargarGrillaCompetencias()
    {
        hojasVida _hojasVida = new hojasVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCompetencias = _hojasVida.ObtenerCompetenciasActivas();

        if (tablaCompetencias.Rows.Count <= 0)
        {
            if (_hojasVida.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _hojasVida.MensajeError, Proceso.Error);
            }
            else
            {
                Mostrar(Acciones.Nuevo);
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron competencias configuradas en el diccionario.", Proceso.Advertencia);
            }

            GridView_COMPETENCIAS.DataSource = null;
            GridView_COMPETENCIAS.DataBind();
        }
        else
        {
            Mostrar(Acciones.Cargar);

            CargarGrillaPreguntasDesdeTabla(tablaCompetencias);

            inhabilitarFilasGrilla(GridView_COMPETENCIAS, 2);
        }
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                cargarGrillaCompetencias();
                break;
            case Acciones.Modificar:
                HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_FILA_SELECCIONADA_GRILLA.Value = "";
                HiddenField_ID_COMPETENCIA.Value = "";
                HiddenField_COMPETENCIA.Value = "";
                HiddenField_DEFINICION.Value = "";
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

    protected void Page_Load(object sender, System.EventArgs e)
    {
        Page.Header.Title = "DICCIONARIO DE COMPETENCIAS";

        if (IsPostBack == false)
        {
            Iniciar();
        }
    }
}