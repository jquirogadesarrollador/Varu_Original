using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brainsbits.LLB.seleccion;
using System.Data;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;
using TSHAK.Components;
using Brainsbits.LLB;

public partial class _RotacionRetiro : System.Web.UI.Page
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

    private enum AccionesGrilla
    {
        Nuevo = 0,
        Modificar = 1,
        Ninguna = 2
    }

    private enum Listas
    {
        EstadosCategoria = 1
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

                Panel_InformacionCategoriaSeleccionada.Visible = false;
                Panel_CONTROL_REGISTRO.Visible = false;

                Panel_DatosCategoria.Visible = false;
                Panel_EstadoCategoria.Visible = false;

                Panel_Motivos.Visible = false;
                GridView_MotivosRetiro.Columns[0].Visible = false;
                GridView_MotivosRetiro.Columns[1].Visible = false;
                Panel_BotonesMotivos.Visible = false;
                Button_NUEVOMOTIVO.Visible = false;
                Button_GUARDARMOTIVO.Visible = false;
                Button_CANCELARMOTIVO.Visible = false;

                Panel_BOTONES_PIE.Visible = false;
                Button_NUEVO_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;

                break;
            case Acciones.Modificar:
                Button_NUEVO.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_CANCELAR.Visible = false;

                Panel_CONTROL_REGISTRO.Visible = false;

                Button_NUEVOMOTIVO.Visible = false;
                Button_GUARDARMOTIVO.Visible = false;
                Button_CANCELARMOTIVO.Visible = false;

                Button_NUEVO_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
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

                TextBox_TituloCategoria.Enabled = false;
                DropDownList_EstadoCategoria.Enabled = false;
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

                Panel_InformacionCategoriaSeleccionada.Visible = true;
                Panel_DatosCategoria.Visible = true;

                Panel_Motivos.Visible = true;
                Panel_BotonesMotivos.Visible = true;
                Button_NUEVOMOTIVO.Visible = true;

                Panel_BOTONES_PIE.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
                break;
            case Acciones.Cargar:
                Panel_FORM_BOTONES.Visible = true;
                Button_NUEVO.Visible = true;
                Button_MODIFICAR.Visible = true;

                Panel_InformacionCategoriaSeleccionada.Visible = true;
                Panel_CONTROL_REGISTRO.Visible = true;
                Panel_DatosCategoria.Visible = true;
                Panel_EstadoCategoria.Visible = true;
                Panel_Motivos.Visible = true;

                Panel_BOTONES_PIE.Visible = true;
                Button_NUEVO_1.Visible = true;
                Button_MODIFICAR_1.Visible = true;
                break;
            case Acciones.Modificar:
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                GridView_MotivosRetiro.Columns[0].Visible = true;
                GridView_MotivosRetiro.Columns[1].Visible = true;

                Panel_BotonesMotivos.Visible = true;
                Button_NUEVOMOTIVO.Visible = true;

                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
                break;
        }
    }

    private void cargar_GridView_RESULTADOS_BUSQUEDA()
    {
        MotivoRotacionRetiro _motivo = new MotivoRotacionRetiro(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaCat = _motivo.ObtenerCategoriasRotacionRetiroTodas();

        if (tablaCat.Rows.Count <= 0)
        {
            if (_motivo.MensajeError == null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros de Rotación y Retiro.", Proceso.Advertencia);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _motivo.MensajeError, Proceso.Error);
            }

            Panel_RESULTADOS_GRID.Visible = false;
        }
        else
        {
            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaCat;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
        }
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                cargar_GridView_RESULTADOS_BUSQUEDA();
                break;
            case Acciones.Nuevo:
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

    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    private void Activar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Nuevo:
                TextBox_TituloCategoria.Enabled = true;
                DropDownList_EstadoCategoria.Enabled = true;
                break;
            case Acciones.Modificar:
                TextBox_TituloCategoria.Enabled = true;
                DropDownList_EstadoCategoria.Enabled = true;
                break;
        }
    }

    private void Limpiar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Nuevo:
                HiddenField_ID_MAESTRA_ROTACION.Value = "";

                TextBox_TituloCategoria.Text = "";

                GridView_MotivosRetiro.DataSource = null;
                GridView_MotivosRetiro.DataBind();
                break;
            case Acciones.Modificar:
                HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
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
        Cargar(Acciones.Nuevo);
    }
    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Modificar);
        Mostrar(Acciones.Modificar);
        Activar(Acciones.Modificar);
        Limpiar(Acciones.Modificar);
    }

    private void CargarControlRegistro(DataRow filaCategoria)
    {
        TextBox_USU_CRE.Text = filaCategoria["USU_CRE"].ToString();
        try
        {
            TextBox_FCH_CRE.Text = DateTime.Parse(filaCategoria["FCH_CRE"].ToString()).ToShortDateString();
            TextBox_HOR_CRE.Text = DateTime.Parse(filaCategoria["FCH_CRE"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_CRE.Text = "";
            TextBox_HOR_CRE.Text = "";
        }
        TextBox_USU_MOD.Text = filaCategoria["USU_MOD"].ToString();
        try
        {
            TextBox_FCH_MOD.Text = DateTime.Parse(filaCategoria["FCH_MOD"].ToString()).ToShortDateString();
            TextBox_HOR_MOD.Text = DateTime.Parse(filaCategoria["FCH_MOD"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_MOD.Text = "";
            TextBox_HOR_MOD.Text = "";
        }
    }

    private void Cargar(Listas lista, DropDownList drop)
    {
        switch (lista)
        {
            case Listas.EstadosCategoria:
                drop.Items.Clear();

                drop.Items.Add(new ListItem("Seleccionae...", ""));

                drop.Items.Add(new ListItem("ACTIVO", "True"));
                drop.Items.Add(new ListItem("INACTIVO", "False"));

                drop.DataBind();
                break;
        }
    }

    private void Cargar(Decimal ID_MAESTRA_ROTACION)
    {
        HiddenField_ID_MAESTRA_ROTACION.Value = ID_MAESTRA_ROTACION.ToString();

        MotivoRotacionRetiro _motivo = new MotivoRotacionRetiro(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCategoria = _motivo.ObtenerCategoriaPorId(ID_MAESTRA_ROTACION);

        if (tablaCategoria.Rows.Count <= 0)
        {
            if (_motivo.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _motivo.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información de la catagoría seleccionada.", Proceso.Advertencia);
            }
        }
        else
        {
            DataRow filaCategoria = tablaCategoria.Rows[0];

            CargarControlRegistro(filaCategoria);

            TextBox_TituloCategoria.Text = filaCategoria["TITULO"].ToString().Trim();
            Cargar(Listas.EstadosCategoria, DropDownList_EstadoCategoria);
            DropDownList_EstadoCategoria.SelectedValue = filaCategoria["ACTIVO"].ToString().Trim();

            _motivo.MensajeError = null;

            DataTable tablaMotivos = _motivo.ObtenerMotivosActivosDeCategoria(ID_MAESTRA_ROTACION);

            if (tablaMotivos.Rows.Count <= 0)
            {
                if (_motivo.MensajeError != null)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _motivo.MensajeError, Proceso.Error);
                }
                else
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron Motivos de Rotacion y Retiro asociados a la categoría actual.", Proceso.Advertencia);
                }
                GridView_MotivosRetiro.DataSource = null;
                GridView_MotivosRetiro.DataBind();
            }
            else
            {
                cargarGridView_MotivosRetiroDesdeTabla(tablaMotivos);

                inhabilitarFilasGrilla(GridView_MotivosRetiro, 2);
            }
        }
    }

    private void Guardar()
    {
        String TITULO = TextBox_TituloCategoria.Text.Trim();

        List<MotivoRotacionRetiro> listaMotivos = new List<MotivoRotacionRetiro>();

        for (int i = 0; i < GridView_MotivosRetiro.Rows.Count; i++)
        {
            MotivoRotacionRetiro _motivoParaLista = new MotivoRotacionRetiro();

            _motivoParaLista.ACTIVO = true;
            _motivoParaLista.ID_DETALLE_ROTACION = Convert.ToDecimal(GridView_MotivosRetiro.DataKeys[i].Values["ID_DETALLE_ROTACION"]);

            TextBox textoTitutlo = GridView_MotivosRetiro.Rows[i].FindControl("TextBox_TituloMotivo") as TextBox;
            _motivoParaLista.TITULO = textoTitutlo.Text;

            _motivoParaLista.ID_MAESTRA_ROTACION = 0;

            listaMotivos.Add(_motivoParaLista);
        }

        MotivoRotacionRetiro _motivo = new MotivoRotacionRetiro(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        Decimal ID_MAESTRA_ROTACION = _motivo.GuardarCategoriaYSusMotivos(TITULO, listaMotivos);

        if (ID_MAESTRA_ROTACION <= 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _motivo.MensajeError, Proceso.Error);
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Cargar);

            Cargar(ID_MAESTRA_ROTACION);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La Categoría de Motivos de Rotación y Retiros fue creada correctamente.", Proceso.Correcto);
        }
    }

    private void Actualizar()
    {
        Decimal ID_MAESTRA_ROTACION = Convert.ToDecimal(HiddenField_ID_MAESTRA_ROTACION.Value);

        String TITULO_CATEGORIA = TextBox_TituloCategoria.Text;
        Boolean ACTIVO_CATEGORIA = false;
        if (DropDownList_EstadoCategoria.SelectedValue.ToUpper() == "TRUE")
        {
            ACTIVO_CATEGORIA = true;
        }

        List<MotivoRotacionRetiro> listaMotivos = new List<MotivoRotacionRetiro>();

        for (int i = 0; i < GridView_MotivosRetiro.Rows.Count; i++)
        {
            MotivoRotacionRetiro _motivoParaLista = new MotivoRotacionRetiro();

            _motivoParaLista.ACTIVO = true;
            _motivoParaLista.ID_DETALLE_ROTACION = Convert.ToDecimal(GridView_MotivosRetiro.DataKeys[i].Values["ID_DETALLE_ROTACION"]);

            TextBox textoTitutlo = GridView_MotivosRetiro.Rows[i].FindControl("TextBox_TituloMotivo") as TextBox;
            _motivoParaLista.TITULO = textoTitutlo.Text;

            _motivoParaLista.ID_MAESTRA_ROTACION = 0;

            listaMotivos.Add(_motivoParaLista);
        }

        MotivoRotacionRetiro _motivo = new MotivoRotacionRetiro(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean verificado = _motivo.ActualizarCategoriaYSusMotivos(ID_MAESTRA_ROTACION, TITULO_CATEGORIA, ACTIVO_CATEGORIA, listaMotivos);

        if (verificado == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _motivo.MensajeError, Proceso.Error);
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Cargar);

            Cargar(ID_MAESTRA_ROTACION);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La Categoría de Motivos de Rotación y Retiros fue modificada correctamente.", Proceso.Correcto);
        }
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        if (HiddenField_ACCION_GRILLA.Value == AccionesGrilla.Ninguna.ToString())
        {
            if (GridView_MotivosRetiro.Rows.Count <= 0)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La categoría debe tener Motivos de rotación y retiro asociados.", Proceso.Advertencia);
            }
            else
            {
                if (String.IsNullOrEmpty(HiddenField_ID_MAESTRA_ROTACION.Value) == true)
                {
                    Guardar();
                }
                else
                {
                    Actualizar();
                }
            }
        }
        else
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe terminar las acciones sobre la grilla de Motivos.", Proceso.Advertencia);
        }
    }

    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        if (HiddenField_ID_MAESTRA_ROTACION.Value == "")
        {
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);
        }
        else
        {
            Decimal ID_MAESTRA_ROTACION = 0;

            ID_MAESTRA_ROTACION = Convert.ToDecimal(HiddenField_ID_MAESTRA_ROTACION.Value);

            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Cargar);

            Cargar(ID_MAESTRA_ROTACION);
        }

    }

    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;

        cargar_GridView_RESULTADOS_BUSQUEDA();
    }

    protected void GridView_RESULTADOS_BUSQUEDA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal ID_MAESTRA_ROTACION = 0;


            ID_MAESTRA_ROTACION = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_MAESTRA_ROTACION"]);

            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Cargar);

            Cargar(ID_MAESTRA_ROTACION);
        }
    }

    protected void GridView_MotivosRetiro_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "modificar")
        {
            HiddenField_FILA_SELECCIONADA_GRILLA.Value = indexSeleccionado.ToString();

            habilitarFilaGrilla(GridView_MotivosRetiro, indexSeleccionado, 2);

            HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Modificar.ToString();
            HiddenField_FILA_SELECCIONADA_GRILLA.Value = indexSeleccionado.ToString();

            HiddenField_ID_DETALLE_ROTACION.Value = GridView_MotivosRetiro.DataKeys[indexSeleccionado].Values["ID_DETALLE_ROTACION"].ToString();
            
            TextBox textoTitulo = GridView_MotivosRetiro.Rows[indexSeleccionado].FindControl("TextBox_TituloMotivo") as TextBox;
            HiddenField_TITULO.Value = textoTitulo.Text.Trim();

            GridView_MotivosRetiro.Columns[0].Visible = false;
            GridView_MotivosRetiro.Columns[1].Visible = false;

            Button_NUEVOMOTIVO.Visible = false;
            Button_GUARDARMOTIVO.Visible = true;
            Button_CANCELARMOTIVO.Visible = true;
        }
        else
        {
            if (e.CommandName == "eliminar")
            {
                DataTable tablaDesdeGrilla = obtenerDataTable_De_GridView_MotivosRetiro();

                tablaDesdeGrilla.Rows.RemoveAt(indexSeleccionado);

                cargarGridView_MotivosRetiroDesdeTabla(tablaDesdeGrilla);

                inhabilitarFilasGrilla(GridView_MotivosRetiro, 2);

                GridView_MotivosRetiro.Columns[0].Visible = true;
                GridView_MotivosRetiro.Columns[1].Visible = true;

                HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_FILA_SELECCIONADA_GRILLA.Value = null;
                HiddenField_ID_DETALLE_ROTACION.Value = null;
                HiddenField_TITULO.Value = null;

                Button_NUEVOMOTIVO.Visible = true;
                Button_GUARDARMOTIVO.Visible = false;
                Button_CANCELARMOTIVO.Visible = false;
            }
        }
    }

    private DataTable configurarTablaParaMotivos()
    {
        DataTable tablaResultado = new DataTable();

        tablaResultado.Columns.Add("ID_DETALLE_ROTACION");
        tablaResultado.Columns.Add("TITULO");

        return tablaResultado;
    }

    private DataTable obtenerDataTable_De_GridView_MotivosRetiro()
    {
        DataTable tablaResultado = configurarTablaParaMotivos();

        DataRow filaTablaResultado;

        Decimal ID_DETALLE_ROTACION = 0;
        String TITULO = null;

        GridViewRow filaGrilla;

        for (int i = 0; i < GridView_MotivosRetiro.Rows.Count; i++)
        {
            filaGrilla = GridView_MotivosRetiro.Rows[i];

            ID_DETALLE_ROTACION = Convert.ToDecimal(GridView_MotivosRetiro.DataKeys[i].Values["ID_DETALLE_ROTACION"]);

            TextBox textoTitulo = filaGrilla.FindControl("TextBox_TituloMotivo") as TextBox;
            TITULO = textoTitulo.Text.Trim();

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_DETALLE_ROTACION"] = ID_DETALLE_ROTACION;
            filaTablaResultado["TITULO"] = TITULO;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }

    private void cargarGridView_MotivosRetiroDesdeTabla(DataTable tablaMotivos)
    {
        GridView_MotivosRetiro.DataSource = tablaMotivos;
        GridView_MotivosRetiro.DataBind();

        for (int i = 0; i < GridView_MotivosRetiro.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_MotivosRetiro.Rows[i];
            DataRow filaTabla = tablaMotivos.Rows[i];
          
            TextBox textoTitulo = filaGrilla.FindControl("TextBox_TituloMotivo") as TextBox;
            textoTitulo.Text = filaTabla["TITULO"].ToString().Trim();
        }
    }

    private void inhabilitarFilasGrilla(GridView grilla, Int32 colInicio)
    {
        for (int i = 0; i < grilla.Rows.Count; i++)
        {
            for (int j = colInicio; j < grilla.Columns.Count; j++)
            {
                grilla.Rows[i].Cells[j].Enabled = false;
            }

        }
    }

    private void habilitarFilaGrilla(GridView grilla, Int32 numFila, Int32 colInicio)
    {
        for(int i = colInicio; i < grilla.Columns.Count; i++)
        {
            grilla.Rows[numFila].Cells[i].Enabled = true;
        }
    }

    protected void Button_NUEVOMOTIVO_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerDataTable_De_GridView_MotivosRetiro();

        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_DETALLE_ROTACION"] = 0;
        filaNueva["TITULO"] = "";

        tablaDesdeGrilla.Rows.Add(filaNueva);

        cargarGridView_MotivosRetiroDesdeTabla(tablaDesdeGrilla);

        inhabilitarFilasGrilla(GridView_MotivosRetiro, 2);
        habilitarFilaGrilla(GridView_MotivosRetiro, GridView_MotivosRetiro.Rows.Count - 1, 2);

        HiddenField_ID_DETALLE_ROTACION.Value = "";
        HiddenField_TITULO.Value = "";

        GridView_MotivosRetiro.Columns[0].Visible = false;
        GridView_MotivosRetiro.Columns[1].Visible = false;

        Button_NUEVOMOTIVO.Visible = false;
        Button_GUARDARMOTIVO.Visible = true;
        Button_CANCELARMOTIVO.Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_SELECCIONADA_GRILLA.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();
    }

    private void inhabilitarFilaGrilla(GridView grilla, Int32 numFila, Int32 colInicio)
    {
        for (int i = colInicio; i < grilla.Columns.Count; i++)
        {
            grilla.Rows[numFila].Cells[i].Enabled = false;
        }
    }

    protected void Button_GUARDARMOTIVO_Click(object sender, EventArgs e)
    {
        int FILA_SELECCIONADA = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA.Value);

        inhabilitarFilaGrilla(GridView_MotivosRetiro, FILA_SELECCIONADA, 2);

        GridView_MotivosRetiro.Columns[0].Visible = true;
        GridView_MotivosRetiro.Columns[1].Visible = true;

        Button_GUARDARMOTIVO.Visible = false;
        Button_CANCELARMOTIVO.Visible = false;
        Button_NUEVOMOTIVO.Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
    }
    protected void Button_CANCELARMOTIVO_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = obtenerDataTable_De_GridView_MotivosRetiro();

        if (HiddenField_ACCION_GRILLA.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }
        else
        {
            if (HiddenField_ACCION_GRILLA.Value == AccionesGrilla.Modificar.ToString())
            {
                DataRow filaGrilla = tablaGrilla.Rows[Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA.Value)];

                filaGrilla["ID_DETALLE_ROTACION"] = Convert.ToDecimal(HiddenField_ID_DETALLE_ROTACION.Value);
                filaGrilla["TITULO"] = HiddenField_TITULO.Value;
            }
        }

        cargarGridView_MotivosRetiroDesdeTabla(tablaGrilla);

        inhabilitarFilasGrilla(GridView_MotivosRetiro, 2);


        GridView_MotivosRetiro.Columns[0].Visible = true;
        GridView_MotivosRetiro.Columns[1].Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();

        Button_NUEVOMOTIVO.Visible = true;
        Button_GUARDARMOTIVO.Visible = false;
        Button_CANCELARMOTIVO.Visible = false;
    }
}