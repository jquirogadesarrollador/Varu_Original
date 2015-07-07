using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using TSHAK.Components;

using Brainsbits.LLB.comercial;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB;

public partial class comercial_UnidadesNegocio : System.Web.UI.Page
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

    #region variables
    private enum Acciones
    {
        Inicio = 0,
        Adicionar,
        Guardar,
        Modificar,
        Cancelar,
        BusquedaEncontrada
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }
    #endregion variables

    #region constructores
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            Iniciar();
        }
    }

    #endregion constructores

    #region metodos
    private void Iniciar()
    {

        Configurar();
        Ocultar();
        
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }

    private void Configurar()
    {
        this.Title = "Unidad de negocio";
        this.Label_INFO_ADICIONAL_MODULO.Text = "Unidades de negocio";
        Panel_FONDO_MENSAJE.CssClass = "panel_fondo_popup";
        Panel_MENSAJES.CssClass = "panel_popup";

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);
        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["ID_EMPRESA"]);
        HiddenField_ID_EMPRESA.Value = ID_EMPRESA.ToString();
    }

    private void Ocultar()
    {
        Button_NUEVO.Visible = false;
        Button_MODIFICAR.Visible = false;
        Button_GUARDAR.Visible = false;
        Button_CANCELAR.Visible = false;
        Panel_empresas_unidad_negocio.Visible = false;
        Panel_unidad_negocio.Visible = false;
        Panel_FORMULARIO.Visible = false;
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Button_NUEVO.Visible = true;
                break;
            case Acciones.Adicionar:
                Panel_FORMULARIO.Visible = true;
                Panel_unidad_negocio.Visible = true;

                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;
                break;
            case Acciones.Guardar:
                Panel_FORMULARIO.Visible = true;
                Button_NUEVO.Visible = true;
                Panel_empresas_unidad_negocio.Visible = true;
                break;
            case Acciones.Cancelar:
                Panel_FORMULARIO.Visible = true;
                Button_NUEVO.Visible = true;
                Panel_empresas_unidad_negocio.Visible = true;
                break;
        }
    }

    private void Cargar(Acciones accion)
    {
        DataTable dataTable = new DataTable();
        switch(accion)
        {
            case Acciones.Inicio:
                cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                dataTable = _cliente.ObtenerTodasLasEmpresasActivas();
                Cargar(dataTable, this.DropDownList_empresas, "ID_EMPRESA", "RAZ_SOCIAL");
                

                dataTable = _cliente.ObtenerEmpresaConIdEmpresa(Convert.ToDecimal(this.HiddenField_ID_EMPRESA.Value));
                Cargar(dataTable);

                usuario _usuario = new usuario(Session["idEmpresa"].ToString());
                dataTable = _usuario.ObtenerEmpleadosRestriccionEmpresas();
                Cargar(dataTable, this.DropDownList_usuario, "Id_Usuario", "NOMBRE_EMPLEADO");

                parametro _parametro = new parametro(Session["idEmpresa"].ToString());
                dataTable = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_UNIDAD_NEGOCIO);
                Cargar(dataTable, this.DropDownList_unidad_negocio, "codigo", "descripcion");

                Cargar(GridView_unidades_negocio);
                break;

        }
        if (dataTable == null) dataTable.Dispose();
    }

    private void Cargar(GridView gridView)
    {

        seguridad _seguridad = new seguridad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable dataTable = _seguridad.ObtenerUsuariosPorEmpresa(Convert.ToDecimal(HiddenField_ID_EMPRESA.Value));

        if (dataTable.Rows.Count <= 0)
        {
            if (_seguridad.MensajeError == null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros de usuarios asociados a unidad de negocio para esta empresa.", Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _seguridad.MensajeError, Proceso.Error);
            }

            Panel_empresas_unidad_negocio.Visible = false;
        }
        else
        {
            gridView.DataSource = dataTable;
            gridView.DataBind();
            Panel_empresas_unidad_negocio.Visible = true;
        }
    }

    private void Cargar(DataTable dataTable)
    {
        if (dataTable.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(dataTable.Rows[0]["RAZ_SOCIAL"].ToString())) this.Label_INFO_ADICIONAL_MODULO.Text = "Unidad de negocio para la empresa " + dataTable.Rows[0]["RAZ_SOCIAL"].ToString();
        }
    }

    private void Cargar(DataTable dataTable, DropDownList dropDownList, string id, string descripcion)
    {
        ListItem listItem = new ListItem("Seleccione...", "0");
        dropDownList.Items.Add(listItem);
        if (dataTable.Rows.Count > 0)
        {
            foreach (DataRow dataRow in dataTable.Rows)
            {
                listItem = new ListItem(dataRow[descripcion].ToString(), dataRow[id].ToString());
                dropDownList.Items.Add(listItem);
            }
        }
    }

    private void Guardar()
    {
        seguridad _seguridad = new seguridad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        try
        {
            _seguridad.Adicionar(DropDownList_usuario.SelectedValue, DropDownList_empresas.SelectedValue, DropDownList_unidad_negocio.Text);
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La unidad de negocio fue adicionada correctamente." , Proceso.Correcto);
            Ocultar();
            Mostrar(Acciones.Guardar);
            Cargar(GridView_unidades_negocio);
        }
        catch (Exception e)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Error al adicionar unidad de negocio." + e.Message, Proceso.Error);
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

    private void ocultar_mensaje(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.Style.Add("display", "none");
        panel_mensaje.Style.Add("display", "none");

        panel_fondo.Visible = false;
        panel_mensaje.Visible = false;
    }
    
    private void Limpiar()
    {
        DropDownList_empresas.ClearSelection();
        DropDownList_unidad_negocio.ClearSelection();
        DropDownList_usuario.ClearSelection();

    }
    
    private void Adicionar()
    {
        Limpiar();
        Ocultar();
        Mostrar(Acciones.Adicionar);
        DropDownList_empresas.SelectedValue = this.HiddenField_ID_EMPRESA.Value;
        DropDownList_empresas.Enabled = false;
    }

    #endregion metodos

    #region eventos
    protected void  Button_NUEVO_Click(object sender, EventArgs e)
    {
        Adicionar();
    }
    protected void  Button_MODIFICAR_Click(object sender, EventArgs e)
    {

    }
    protected void  Button_GUARDAR_Click(object sender, EventArgs e)
    {
        Guardar();
    }
    protected void  Button_CANCELAR_Click(object sender, EventArgs e)
    {
        Ocultar();
        Mostrar(Acciones.Cancelar);
    }
    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }
    #endregion eventos
}