using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brainsbits.LLB.maestras;
using TSHAK.Components;
using System.Data;
using Brainsbits.LLB.seguridad;
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

    private System.Drawing.Color colorSeleccionado = System.Drawing.ColorTranslator.FromHtml("#699DF0");

    private enum Acciones
    {
        Inicio = 0,
        CargarEmpresaUsuario = 1,
        ModificarEmpresaUsuario = 2
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
        configurar_paneles_popup(Panel_FONDO_MENSAJE_ASIGNACION_UN, Panel_MENSAJE_ASIGNACION_UN);
    }

    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = false;

                Panel_FORM_BOTONES_ENCABEZADO.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_CANCELAR.Visible = false;

                Panel_RESULTADOS_GRID.Visible = false;
                Panel_ASIGNACION_UNIDAD_NEGOCIO.Visible = false;
                Button_NUEVO_ASIGNACION.Visible = false;
                Button_GUARDAR_ASIGNACION.Visible = false;
                Button_CANCELAR_ASIGNACION.Visible = false;

                Panel_FORM_BOTONES_PIE.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;

                GridView_ASIGNACION_UN.Columns[0].Visible = false;
                break;
            case Acciones.ModificarEmpresaUsuario:
                Button_MODIFICAR.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_CANCELAR.Visible = false;

                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;

                Button_NUEVO_ASIGNACION.Visible = false;
                Button_GUARDAR_ASIGNACION.Visible = false;
                Button_CANCELAR_ASIGNACION.Visible = false;
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
                Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.CargarEmpresaUsuario:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Button_MODIFICAR.Visible = true;

                Panel_RESULTADOS_GRID.Visible = true;

                Panel_ASIGNACION_UNIDAD_NEGOCIO.Visible = true;

                Panel_FORM_BOTONES_PIE.Visible = true;
                Button_MODIFICAR_1.Visible = true;
                break;
            case Acciones.ModificarEmpresaUsuario:
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_ASIGNACION_UNIDAD_NEGOCIO.Visible = true;
                Button_NUEVO_ASIGNACION.Visible = true;
                GridView_ASIGNACION_UN.Columns[0].Visible = true;

                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
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

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["ID_EMPRESA"]);

        HiddenField_ID_EMPRESA.Value = ID_EMPRESA.ToString();

        seguridad _seguridad = new seguridad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        
        DataTable tablaContratosOriginal = _seguridad.ObtenerUsuariosPorEmpresa(ID_EMPRESA);

        if (tablaContratosOriginal.Rows.Count <= 0)
        {
            if (_seguridad.MensajeError == null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros de usuarios asociados a unidad de negocio para esta empresa.", Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _seguridad.MensajeError, Proceso.Error);
            }

            Panel_RESULTADOS_GRID.Visible = false;
        }
        else
        {
            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaContratosOriginal;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
        }
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                this.Title = "UNIDAD DE NEGOCIO";
                cargar_GridView_RESULTADOS_BUSQUEDA();
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
    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.ModificarEmpresaUsuario);
        Mostrar(Acciones.ModificarEmpresaUsuario);
    }

    private void Guardar()
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        Decimal ID_EMPRESA_USUARIO = Convert.ToDecimal(HiddenField_ID_EMPRESA_USUARIO.Value);
        
        List<seguridad> listaUnidadNegocio = new List<seguridad>();
        seguridad _unidadNegocioParaLista;
        String UNIDAD_NEGOCIO_ACTUALIZADO = "";

        DropDownList datoDrop;
        for (int i = 0; i < GridView_ASIGNACION_UN.Rows.Count; i++)
        {
            _unidadNegocioParaLista = new seguridad();

            _unidadNegocioParaLista.ID_EMPRESA = ID_EMPRESA;

            _unidadNegocioParaLista.ID_EMPRESA_USUARIO = ID_EMPRESA_USUARIO;

            _unidadNegocioParaLista.ID_UNIDAD_NEGOCIO = Convert.ToDecimal(GridView_ASIGNACION_UN.DataKeys[i].Values["ID_UNIDAD_NEGOCIO"]);

            datoDrop = GridView_ASIGNACION_UN.Rows[i].FindControl("DropDownList_UN") as DropDownList;
            _unidadNegocioParaLista.UNIDAD_NEGOCIO = datoDrop.SelectedValue;

            listaUnidadNegocio.Add(_unidadNegocioParaLista);

            if (i == 0)
            {
                UNIDAD_NEGOCIO_ACTUALIZADO = datoDrop.SelectedItem.Text;
            }
            else
            {
                UNIDAD_NEGOCIO_ACTUALIZADO += ", " + datoDrop.SelectedItem.Text;
            }
        }

        if(UNIDAD_NEGOCIO_ACTUALIZADO == "")
        {
            GridView_RESULTADOS_BUSQUEDA.Rows[GridView_RESULTADOS_BUSQUEDA.SelectedIndex].Cells[6].Text = "Sin Asignación";
        }
        else
        {
            GridView_RESULTADOS_BUSQUEDA.Rows[GridView_RESULTADOS_BUSQUEDA.SelectedIndex].Cells[6].Text = UNIDAD_NEGOCIO_ACTUALIZADO;
        }

        seguridad _seguridad = new seguridad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean resultado = _seguridad.ActualizarUnidadNegocio(ID_EMPRESA, ID_EMPRESA_USUARIO, listaUnidadNegocio);

        if (resultado == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _seguridad.MensajeError, Proceso.Error);
        }
        else
        {
            maestrasInterfaz _maestrasInterfaz = new maestrasInterfaz();
            _maestrasInterfaz.CargarEnBdElManualServicioActual(ID_EMPRESA);



            Cargar(ID_EMPRESA_USUARIO);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La asignación de unidad de negocio para el usuario seleccionado se actualizó correctamente.", Proceso.Correcto);
        }   
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        if (HiddenField_ACCION_GRILLA.Value != AccionesGrilla.Ninguna.ToString())
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Para poder continuar, primero debe terminar las acciones sobre la grilla de asignación de unidad de negocio.", Proceso.Advertencia);
        }
        else
        {
            Guardar();
        }
    }
    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {

    }

    private void cargar_DropDownList_TIPO_SERVICIO_RESPECTIVO(DropDownList drop)
    {
        drop.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());
        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_SERVICIOS_RESPECTIVOS);

        ListItem item = new ListItem("Seleccione...", "");
        drop.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            drop.Items.Add(item);
        }

        drop.DataBind();
    }

    private void cargar_DropDownList_ASIGNACION_UN(DropDownList drop)
    {
        drop.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());
        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_UNIDAD_NEGOCIO);

        ListItem item = new ListItem("Seleccione...", "");
        drop.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            drop.Items.Add(item);
        }

        drop.DataBind();
    }

    private void cargar_GridView_ASIGNACION_UN_desde_tabla(DataTable tablaInfoUnidadNegocio)
    {
        GridView_ASIGNACION_UN.DataSource = tablaInfoUnidadNegocio;
        GridView_ASIGNACION_UN.DataBind();

        GridViewRow filaGrilla;
        DataRow filaTabla;

        DropDownList datoDrop;
        for (int i = 0; i < tablaInfoUnidadNegocio.Rows.Count; i++)
        {
            filaGrilla = GridView_ASIGNACION_UN.Rows[i];
            filaTabla = tablaInfoUnidadNegocio.Rows[i];

            datoDrop = filaGrilla.FindControl("DropDownList_UN") as DropDownList;

            cargar_DropDownList_ASIGNACION_UN(datoDrop);

            try
            {
                datoDrop.SelectedValue = filaTabla["UNIDAD_NEGOCIO"].ToString();
            }
            catch
            {
                datoDrop.SelectedIndex = 0;
            }
        }
    }

    private void Cargar(Decimal ID_EMPRESA_USUARIO)
    {
        HiddenField_ID_EMPRESA_USUARIO.Value = ID_EMPRESA_USUARIO.ToString();

        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Mostrar(Acciones.CargarEmpresaUsuario);

        seguridad _seguridad = new seguridad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoUnidadNegocio = _seguridad.ObtenerCrtUnidadNegocio(ID_EMPRESA_USUARIO);

        if (tablaInfoUnidadNegocio.Rows.Count <= 0)
        {
            if (_seguridad.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _seguridad.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se han asignado unidad de negocio para el usuario seleccionado. por favor realice la nueva asignación y guarde.", Proceso.Advertencia);
            }

            Panel_ASIGNACION_UNIDAD_NEGOCIO.Visible = false;
        }
        else
        {
            cargar_GridView_ASIGNACION_UN_desde_tabla(tablaInfoUnidadNegocio);

            for (int i = 0; i < GridView_ASIGNACION_UN.Rows.Count; i++)
            {
                for (int j = 1; j < GridView_ASIGNACION_UN.Columns.Count; j++)
                {
                    GridView_ASIGNACION_UN.Rows[i].Cells[j].Enabled = false;
                }
            }
        }
    }

    protected void GridView_RESULTADOS_BUSQUEDA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal ID_EMPRESA_USUARIO = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_EMPRESA_USUARIO"]);

            Cargar(ID_EMPRESA_USUARIO);
        }

        for (int i = 0; i < GridView_RESULTADOS_BUSQUEDA.Rows.Count; i++)
        {
            if (i == indexSeleccionado)
            {
                GridView_RESULTADOS_BUSQUEDA.Rows[i].BackColor = colorSeleccionado;
            }
            else
            { 
                GridView_RESULTADOS_BUSQUEDA.Rows[i].BackColor = System.Drawing.Color.Transparent;
            }
        }

        GridView_RESULTADOS_BUSQUEDA.SelectedIndex = indexSeleccionado;
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
    protected void Button_MENSAJE_ASIGNACION_UN_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE_ASIGNACION_UN, Panel_MENSAJE_ASIGNACION_UN);
    }
    protected void GridView_ASIGNACION_UN_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "eliminar")
        {
            DataTable tablaDesdeGrilla = obtenerGridView_ASIGNACION_UN();

            tablaDesdeGrilla.Rows.RemoveAt(indexSeleccionado);

            GridViewRow filaGrilla;

            cargar_GridView_ASIGNACION_UN_desde_tabla(tablaDesdeGrilla);

            for (int i = 0; i < GridView_ASIGNACION_UN.Rows.Count; i++)
            {
                filaGrilla = GridView_ASIGNACION_UN.Rows[i];

                for (int j = 1; j < GridView_ASIGNACION_UN.Columns.Count; j++)
                {
                    filaGrilla.Cells[j].Enabled = false;
                }
            }

            GridView_ASIGNACION_UN.Columns[0].Visible = true;

            HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
            HiddenField_FILA_SELECCIONADA_GRILLA.Value = null;
            HiddenField_ID_UNIDAD_NEGOCIO.Value = "";
            HiddenField_UNIDAD_NEGOCIO.Value = "";

            Button_NUEVO_ASIGNACION.Visible = true;
            Button_GUARDAR_ASIGNACION.Visible = false;
            Button_CANCELAR_ASIGNACION.Visible = false;
        }
    }

    private DataTable obtenerGridView_ASIGNACION_UN()
    {
        DataTable tablaResultado = new DataTable();
        tablaResultado.Columns.Add("ID_UNIDAD_NEGOCIO");
        tablaResultado.Columns.Add("UNIDAD_NEGOCIO");

        DataRow filaTablaResultado;

        Decimal ID_UNIDAD_NEGOCIO = 0;
        String UNIDAD_NEGOCIO = null;

        GridViewRow filaGrilla;
        DropDownList datoDrop;
        for (int i = 0; i < GridView_ASIGNACION_UN.Rows.Count; i++)
        {
            filaGrilla = GridView_ASIGNACION_UN.Rows[i];

            ID_UNIDAD_NEGOCIO = Convert.ToDecimal(GridView_ASIGNACION_UN.DataKeys[i].Values["ID_UNIDAD_NEGOCIO"]);

            datoDrop = filaGrilla.FindControl("DropDownList_UN") as DropDownList;
            UNIDAD_NEGOCIO = datoDrop.SelectedValue;

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_UNIDAD_NEGOCIO"] = ID_UNIDAD_NEGOCIO;

            filaTablaResultado["UNIDAD_NEGOCIO"] = UNIDAD_NEGOCIO;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }

    protected void Button_NUEVO_ASIGNACION_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerGridView_ASIGNACION_UN();
        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_UNIDAD_NEGOCIO"] = 0;
        filaNueva["UNIDAD_NEGOCIO"] = "";

        tablaDesdeGrilla.Rows.Add(filaNueva);

        GridViewRow filaGrilla;

        cargar_GridView_ASIGNACION_UN_desde_tabla(tablaDesdeGrilla);

        for (int i = 0; i < GridView_ASIGNACION_UN.Rows.Count; i++)
        {
            filaGrilla = GridView_ASIGNACION_UN.Rows[i];

            if (i == (GridView_ASIGNACION_UN.Rows.Count - 1))
            {
                for (int j = 1; j < GridView_ASIGNACION_UN.Columns.Count; j++)
                {
                    filaGrilla.Cells[j].Enabled = true;
                }
            }
            else
            {
                for (int j = 1; j < GridView_ASIGNACION_UN.Columns.Count; j++)
                {
                    filaGrilla.Cells[j].Enabled = false;
                }
            }
        }

        HiddenField_ID_UNIDAD_NEGOCIO.Value = "";
        HiddenField_UNIDAD_NEGOCIO.Value = "";

        GridView_ASIGNACION_UN.Columns[0].Visible = false;

        Button_NUEVO_ASIGNACION.Visible = false;
        Button_GUARDAR_ASIGNACION.Visible = true;
        Button_CANCELAR_ASIGNACION.Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_SELECCIONADA_GRILLA.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();
    }
    protected void Button_GUARDAR_ASIGNACION_Click(object sender, EventArgs e)
    {
        int FILA_SELECCIONADA = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA.Value);

        for (int j = 1; j < GridView_ASIGNACION_UN.Columns.Count; j++)
        {
            GridView_ASIGNACION_UN.Rows[FILA_SELECCIONADA].Cells[j].Enabled = false;
        }

        GridView_ASIGNACION_UN.Columns[0].Visible = true;

        Button_GUARDAR_ASIGNACION.Visible = false;
        Button_CANCELAR_ASIGNACION.Visible = false;
        Button_NUEVO_ASIGNACION.Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
    }
    protected void Button_CANCELAR_SERVICIO_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = obtenerGridView_ASIGNACION_UN();

        if (HiddenField_ACCION_GRILLA.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }

        cargar_GridView_ASIGNACION_UN_desde_tabla(tablaGrilla);

        foreach (GridViewRow fila in GridView_ASIGNACION_UN.Rows)
        {
            for (int j = 1; j < GridView_ASIGNACION_UN.Columns.Count; j++)
            {
                fila.Cells[j].Enabled = false;
            }
        }

        GridView_ASIGNACION_UN.Columns[0].Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();

        Button_NUEVO_ASIGNACION.Visible = true;
        Button_GUARDAR_ASIGNACION.Visible = false;
        Button_CANCELAR_ASIGNACION.Visible = false;
    }

    protected void DropDownList_UN_SelectedIndexChanged(object sender, EventArgs e)
    {
        String UNIDAD_NEGOCIO = null;
        String UNIDAD_NEGOCIO_INSPECCIONADA = null;
        Boolean empresaYaIncluida = false;

        GridViewRow filaGrillaSeleccionada;

        if (GridView_ASIGNACION_UN.Rows.Count > 1)
        {
            filaGrillaSeleccionada = GridView_ASIGNACION_UN.Rows[GridView_ASIGNACION_UN.Rows.Count - 1];

            DropDownList datoDropSeleccionado = filaGrillaSeleccionada.FindControl("DropDownList_UN") as DropDownList;
            UNIDAD_NEGOCIO = datoDropSeleccionado.SelectedValue;

            GridViewRow filaGrilla;
            DropDownList datoDropinspeccionado;

            for (int i = 0; i < (GridView_ASIGNACION_UN.Rows.Count - 1); i++)
            {
                filaGrilla = GridView_ASIGNACION_UN.Rows[i];

                datoDropinspeccionado = filaGrilla.FindControl("DropDownList_UN") as DropDownList;
                UNIDAD_NEGOCIO_INSPECCIONADA = datoDropinspeccionado.SelectedValue;

                if (UNIDAD_NEGOCIO == UNIDAD_NEGOCIO_INSPECCIONADA)
                {
                    Informar(Panel_FONDO_MENSAJE_ASIGNACION_UN, Image_MENSAJE_ASIGNACION_UN_POPUP, Panel_MENSAJE_ASIGNACION_UN, Label_MENSAJE_ASIGNACION_UN, "La Unidad de Negocio seleccionada ya se encuentra en la lsita.", Proceso.Advertencia);

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
