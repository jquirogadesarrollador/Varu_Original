using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brainsbits.LLB.programasRseGlobal;
using System.Data;
using Brainsbits.LLB.maestras;
using TSHAK.Components;
using Brainsbits.LLB;
using Brainsbits.LLB.seguridad;

public partial class _MotivoActProgComp : System.Web.UI.Page
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

                Panel_MOTIVOS.Visible = false;
                Button_NUEVO_MOTIVO.Visible = false;
                Button_GUARDAR_MOTIVO.Visible = false;
                Button_CANCELAR_MOTIVO.Visible = false;

                Panel_FORM_BOTONES_ABAJO.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;

                GridView_MOTIVOS.Columns[0].Visible = false;
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

                Panel_MOTIVOS.Visible = true;

                Button_NUEVO_MOTIVO.Visible = true;

                Panel_FORM_BOTONES_ABAJO.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;

                GridView_MOTIVOS.Columns[0].Visible = true;
                break;
            case Acciones.Cargar:
                Panel_FORM_BOTONES.Visible = true;
                Button_MODIFICAR.Visible = true;

                Panel_MOTIVOS.Visible = true;

                Panel_FORM_BOTONES_ABAJO.Visible = true;
                Button_MODIFICAR_1.Visible = true;
                break;
            case Acciones.Modificar:
                Panel_FORM_BOTONES.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;
                Panel_MOTIVOS.Visible = true;
                Button_NUEVO_MOTIVO.Visible = true;
                Panel_FORM_BOTONES_ABAJO.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;

                GridView_MOTIVOS.Columns[0].Visible = true;
                break;
        }
    }

    private void CargarGrillaMotivosDesdeTabla(DataTable tablaMotivos)
    {
        GridView_MOTIVOS.DataSource = tablaMotivos;
        GridView_MOTIVOS.DataBind();

        for (int i = 0; i < GridView_MOTIVOS.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_MOTIVOS.Rows[i];
            DataRow filaTabla = tablaMotivos.Rows[i];

            TextBox textoMotivo = filaGrilla.FindControl("TextBox_Motivo") as TextBox;
            textoMotivo.Text = filaTabla["MOTIVO"].ToString().Trim();

            DropDownList dropEstado = filaGrilla.FindControl("DropDownList_Estado") as DropDownList;
            dropEstado.SelectedValue = filaTabla["ACTIVO"].ToString();
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

    private void cargarGrillaMotivos()
    {
        Programa.Areas AREA_PROGRAMA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);
        String TIPO = HiddenField_TIPO.Value;

        MotivoProgComp _motivo = new MotivoProgComp(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaMotivos = _motivo.ObtenerMotivosActProgCompPorAreaYTipo(AREA_PROGRAMA, TIPO);

        if (tablaMotivos.Rows.Count <= 0)
        {
            if (_motivo.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _motivo.MensajeError, Proceso.Error);
            }
            else
            {
                Mostrar(Acciones.Nuevo);
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron motivos configurados.", Proceso.Advertencia);
            }

            GridView_MOTIVOS.DataSource = null;
            GridView_MOTIVOS.DataBind();
        }
        else
        {
            Mostrar(Acciones.Cargar);

            CargarGrillaMotivosDesdeTabla(tablaMotivos);

            inhabilitarFilasGrilla(GridView_MOTIVOS, 1);
        }
    }

    private void ConfigurarAreaRseGlobalYTipo()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String tipo = QueryStringSeguro["tipo"].ToString();
        int proceso = Convert.ToInt32(QueryStringSeguro["proceso"]);

        HiddenField_TIPO.Value = tipo;

        HiddenField_ID_AREA.Value = _tools.ObtenerIdAreaProceso(proceso);
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                ConfigurarAreaRseGlobalYTipo();

                cargarGrillaMotivos();
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
        Page.Header.Title = "MOTIVOS";

        if (IsPostBack == false)
        {
            Iniciar();
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

    private DataTable obtenerTablaDeGrillaMotivos()
    {
        DataTable tablaResultado = new DataTable();
        tablaResultado.Columns.Add("ID_MOTIVO");
        tablaResultado.Columns.Add("MOTIVO");
        tablaResultado.Columns.Add("ACTIVO");


        for (int i = 0; i < GridView_MOTIVOS.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_MOTIVOS.Rows[i];

            Decimal ID_MOTIVO = Convert.ToDecimal(GridView_MOTIVOS.DataKeys[i].Values["ID_MOTIVO"]);

            TextBox textoMotivo = filaGrilla.FindControl("TextBox_Motivo") as TextBox;
            String MOTIVO = textoMotivo.Text.Trim();

            DropDownList dropActivo = filaGrilla.FindControl("DropDownList_Estado") as DropDownList;
            String ACTIVO = dropActivo.SelectedValue;

            DataRow filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_MOTIVO"] = ID_MOTIVO;
            filaTablaResultado["MOTIVO"] = MOTIVO;
            filaTablaResultado["ACTIVO"] = ACTIVO;

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

    protected void Button_NUEVO_MOTIVO_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerTablaDeGrillaMotivos();
        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_MOTIVO"] = 0;
        filaNueva["MOTIVO"] = "";
        filaNueva["ACTIVO"] = "";

        tablaDesdeGrilla.Rows.Add(filaNueva);

        CargarGrillaMotivosDesdeTabla(tablaDesdeGrilla);

        inhabilitarFilasGrilla(GridView_MOTIVOS, 1);
        ActivarFilaGrilla(GridView_MOTIVOS, GridView_MOTIVOS.Rows.Count - 1, 1);

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_SELECCIONADA_GRILLA.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();

        GridView_MOTIVOS.Columns[0].Visible = false;

        Button_NUEVO_MOTIVO.Visible = false;
        Button_GUARDAR_MOTIVO.Visible = true;
        Button_CANCELAR_MOTIVO.Visible = true;
    }

    private void inhabilitarFilaGrilla(GridView grilla, int fila, int colInicio)
    {
        for (int j = colInicio; j < grilla.Columns.Count; j++)
        {
            grilla.Rows[fila].Cells[j].Enabled = false;
        }
    }

    protected void Button_GUARDAR_MOTIVO_Click(object sender, EventArgs e)
    {
        int FILA_SELECCIONADA = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA.Value);

        inhabilitarFilaGrilla(GridView_MOTIVOS, FILA_SELECCIONADA, 1);

        GridView_MOTIVOS.Columns[0].Visible = true;

        Button_GUARDAR_MOTIVO.Visible = false;
        Button_CANCELAR_MOTIVO.Visible = false;
        Button_NUEVO_MOTIVO.Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
    }
    protected void Button_CANCELAR_MOTIVO_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = obtenerTablaDeGrillaMotivos();

        if (HiddenField_ACCION_GRILLA.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }
        else
        {
            if (HiddenField_ACCION_GRILLA.Value == AccionesGrilla.Modificar.ToString())
            {
                DataRow filaGrilla = tablaGrilla.Rows[Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA.Value)];

                filaGrilla["ID_MOTIVO"] = HiddenField_ID_MOTIVO.Value;
                filaGrilla["MOTIVOS"] = HiddenField_MOTIVO.Value;
                filaGrilla["ACTIVO"] = HiddenField_ACTIVO.Value;
            }
        }

        CargarGrillaMotivosDesdeTabla(tablaGrilla);

        inhabilitarFilasGrilla(GridView_MOTIVOS, 1);

        GridView_MOTIVOS.Columns[0].Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();

        Button_NUEVO_MOTIVO.Visible = true;
        Button_GUARDAR_MOTIVO.Visible = false;
        Button_CANCELAR_MOTIVO.Visible = false;
    }
    protected void GridView_MOTIVOS_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "modificar")
        {
            HiddenField_FILA_SELECCIONADA_GRILLA.Value = indexSeleccionado.ToString();

            ActivarFilaGrilla(GridView_MOTIVOS, indexSeleccionado, 1);

            HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Modificar.ToString();
            HiddenField_FILA_SELECCIONADA_GRILLA.Value = indexSeleccionado.ToString();

            HiddenField_ID_MOTIVO.Value = GridView_MOTIVOS.DataKeys[indexSeleccionado].Values["ID_MOTIVO"].ToString();

            TextBox textoMotivo = GridView_MOTIVOS.Rows[indexSeleccionado].FindControl("TextBox_Motivo") as TextBox;
            HiddenField_MOTIVO.Value = textoMotivo.Text;

            DropDownList dropEstado = GridView_MOTIVOS.Rows[indexSeleccionado].FindControl("DropDownList_Estado") as DropDownList;
            HiddenField_ACTIVO.Value = dropEstado.SelectedValue;

            GridView_MOTIVOS.Columns[0].Visible = false;

            Button_NUEVO_MOTIVO.Visible = false;
            Button_GUARDAR_MOTIVO.Visible = true;
            Button_CANCELAR_MOTIVO.Visible = true;
        }
    }

    private void Actualizar()
    {
        Programa.Areas AREA_PROGRAMA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);
        String TIPO = HiddenField_TIPO.Value;

        List<MotivoProgComp> listaMotivos = new List<MotivoProgComp>();

        for (int i = 0; i < GridView_MOTIVOS.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_MOTIVOS.Rows[i];

            MotivoProgComp _motivoParaLista = new MotivoProgComp();

            Decimal ID_MOTIVO = Convert.ToDecimal(GridView_MOTIVOS.DataKeys[i].Values["ID_MOTIVO"]);

            TextBox textoMotivo = filaGrilla.FindControl("TextBox_Motivo") as TextBox;
            String MOTIVO = textoMotivo.Text.Trim();

            DropDownList dropActivo = filaGrilla.FindControl("DropDownList_Estado") as DropDownList;
            Boolean ACTIVO = true;
            if (dropActivo.SelectedValue == "False")
            {
                ACTIVO = false;
            }

            _motivoParaLista.ACTIVO = ACTIVO;
            _motivoParaLista.ID_MOTIVO = ID_MOTIVO;
            _motivoParaLista.MOTIVO = MOTIVO;

            listaMotivos.Add(_motivoParaLista);
        }

        MotivoProgComp _motivo = new MotivoProgComp(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean verificador = _motivo.ActualizarMotivos(AREA_PROGRAMA, TIPO, listaMotivos);

        if (verificador == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _motivo.MensajeError, Proceso.Error);
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Cargar(Acciones.Inicio);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Los motivos, fueron actualizados correctamente.", Proceso.Correcto);
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
}