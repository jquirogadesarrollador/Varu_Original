using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.IO;
using System.Data;
using TSHAK.Components;


using Brainsbits.LLB.contratacion;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.comercial;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB;


public partial class contratacion_Clausulas : System.Web.UI.Page
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

    #region variables
    private enum Acciones
    {
        Iniciar=0,
        Adicionar,
        Cancelar,
        Editar,
        Guardar
    }
    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }

    #endregion variables

    #region constructor
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false) Iniciar();
    }
    #endregion constructor

    #region metodos
    private void Iniciar()
    {
        Page.Header.Title = "CLAUSULAS";
        Configurar();
        Ocultar();
        Bloquear();
        
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);
        HiddenField_ID_EMPRESA.Value = QueryStringSeguro["reg"].ToString();
        Cargar(Convert.ToDecimal(HiddenField_ID_EMPRESA.Value));
        
        Mostrar(Acciones.Iniciar);
    }

    private void Bloquear()
    {
        DropDownList_ID_TIPO_CLAUSULA.Enabled = false;
        DropDownList_ID_OCUPACION.Enabled = false;
        DropDownList_ID_ESTADO.Enabled = false;

        TextBox_DESCRIPCION.Enabled = false;
        FileUpload_archivo.Enabled = false;
    }

    private void Desbloquear(Acciones accion)
    {
        switch(accion)
        {
            case Acciones.Adicionar:
                DropDownList_ID_TIPO_CLAUSULA.Enabled = true;
                TextBox_DESCRIPCION.Enabled = true;
                DropDownList_ID_OCUPACION.Enabled = true;
                FileUpload_archivo.Enabled = true;
                break;
            case Acciones.Editar:
                DropDownList_ID_TIPO_CLAUSULA.Enabled = true;
                DropDownList_ID_ESTADO.Enabled = true;
                DropDownList_ID_OCUPACION.Enabled = true;

                TextBox_DESCRIPCION.Enabled = true;
                FileUpload_archivo.Enabled = true;
                break;
        }
    }

    private void Ocultar()
    {
        Button_NUEVO.Visible = false;
        Button_GUARDAR.Visible = false;
        Button_ACTUALIZAR.Visible = false;
        Button_CANCELAR.Visible = false;

        Panel_Clausulas.Visible = false;
        Panel_RESULTADOS_GRID.Visible = false;
        GridView_RESULTADOS_BUSQUEDA.Visible = false;
    }

    private void Ocultar(Panel panel_fondo, Panel panel_mensaj)
    {
        panel_fondo.Style.Add("display", "none");
        panel_mensaj.Style.Add("display", "none");

        panel_fondo.Visible = false;
        panel_mensaj.Visible = false;

    }

    private void Mostrar(Acciones acccion)
    {
        switch (acccion)
        {
            case Acciones.Iniciar:
                Button_NUEVO.Visible = true;
                GridView_RESULTADOS_BUSQUEDA.Visible = true;
                Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.Adicionar:
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_Clausulas.Visible = true;
                break;
            case Acciones.Editar:
                Button_NUEVO.Visible = true;
                Button_ACTUALIZAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_Clausulas.Visible = true;
                break;
            case Acciones.Guardar:
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_Clausulas.Visible = true;
                break;
        }
    }

    private void Configurar()
    {
        Configurar(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    private void Configurar(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_mensaje.CssClass = "panel_popup";
    }

    private void Limpiar()
    {
        Label_ID_EMPRESA.Text = string.Empty;

        TextBox_DESCRIPCION.Text = string.Empty;

    }

    private void Adicionar()
    {
        Limpiar();
        Ocultar();
        Mostrar(Acciones.Adicionar);
        Desbloquear(Acciones.Adicionar);
        Cargar();
    }

    private void Guardar()
    {
        tools _tools = new tools();
        Byte[] archivo = null;
        Int32 archivo_tamaño = 0;
        decimal id_clausula = 0;
        String archivo_extension = null;
        String archivo_tipo = null;
        bool aplicarTodosLosCargos = false;
        decimal id_ocupacion=0;

        try
        {
            if (FileUpload_archivo.HasFile == true)
            {
                using (BinaryReader reader = new BinaryReader(FileUpload_archivo.PostedFile.InputStream))
                {
                    archivo = reader.ReadBytes(FileUpload_archivo.PostedFile.ContentLength);
                    archivo_tamaño = FileUpload_archivo.PostedFile.ContentLength;
                    archivo_tipo = FileUpload_archivo.PostedFile.ContentType;
                    archivo_extension = _tools.obtenerExtensionArchivo(FileUpload_archivo.PostedFile.FileName);
                }

                if (DropDownList_ID_OCUPACION.Text.Equals(string.Empty)) id_ocupacion = 0;
                else if (DropDownList_ID_OCUPACION.Text.Equals("0")) aplicarTodosLosCargos = true;
                else id_ocupacion = Convert.ToDecimal(DropDownList_ID_OCUPACION.SelectedValue);

                Clausula clausula = new Clausula(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                id_clausula = clausula.Adicionar(DropDownList_ID_TIPO_CLAUSULA.SelectedValue, DropDownList_ID_ESTADO.SelectedValue, TextBox_DESCRIPCION.Text,
                    Convert.ToDecimal(HiddenField_ID_EMPRESA.Value), id_ocupacion, archivo, archivo_tamaño, archivo_extension, archivo_tipo, aplicarTodosLosCargos);

                if (id_clausula > 0)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La Clausula fue registrada correctamente.", Proceso.Correcto);
                    this.HiddenField_id_clausula.Value = id_clausula.ToString();
                    Bloquear();
                    Ocultar();
                    Mostrar(Acciones.Editar);
                    DataRow dataRow = clausula.ObtenerPorId(id_clausula);
                    Cargar(dataRow);
                }
                else
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Error al registrar la Clausula", Proceso.Error);
                }
            }
        }
        catch (Exception e)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, e.Message, Proceso.Error);
        }
    }

    private void Actualizar()
    {
        tools _tools = new tools();
        Byte[] archivo = null;
        Int32 archivo_tamaño = 0;
        String archivo_extension = null;
        String archivo_tipo = null;
        bool actualizado = false;
        decimal id_ocupacion=0;
        try
        {
            if (FileUpload_archivo.HasFile == true)
            {
                using (BinaryReader reader = new BinaryReader(FileUpload_archivo.PostedFile.InputStream))
                {
                    archivo = reader.ReadBytes(FileUpload_archivo.PostedFile.ContentLength);
                    archivo_tamaño = FileUpload_archivo.PostedFile.ContentLength;
                    archivo_tipo = FileUpload_archivo.PostedFile.ContentType;
                    archivo_extension = _tools.obtenerExtensionArchivo(FileUpload_archivo.PostedFile.FileName);
                }
            }

            if (DropDownList_ID_OCUPACION.Text.Equals(string.Empty)) id_ocupacion = 0;
            else id_ocupacion = Convert.ToDecimal(DropDownList_ID_OCUPACION.SelectedValue);

            Clausula clausula = new Clausula(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            actualizado = clausula.Actualizar(Convert.ToDecimal(HiddenField_id_clausula.Value), DropDownList_ID_TIPO_CLAUSULA.SelectedValue,
                DropDownList_ID_ESTADO.SelectedValue, TextBox_DESCRIPCION.Text, Convert.ToDecimal(HiddenField_ID_EMPRESA.Value), id_ocupacion, archivo, archivo_tamaño, archivo_extension, archivo_tipo);
            if (actualizado.Equals(true))
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La Clausula fue actualizada correctamente.", Proceso.Correcto);
                Bloquear();
                Ocultar();
                Mostrar(Acciones.Editar);
                DataRow dataRow = clausula.ObtenerPorId(Convert.ToDecimal(HiddenField_id_clausula.Value));
                Cargar(dataRow);
            }
            else Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Error al actualizar la Clausula ", Proceso.Error);
        }
        catch (Exception e)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, e.Message, Proceso.Error);
        }

    }

    private void Editar()
    {
        Ocultar();
        Mostrar(Acciones.Guardar);
        Bloquear();
        Desbloquear(Acciones.Editar);
        Inactivar(Acciones.Editar);
    }

    private void Inactivar(Acciones accion)
    {
        switch(accion)
        {
            case Acciones.Editar:
                    RequiredFieldValidator_FileUpload_archivo.Enabled = false;
                break;
        }
    }

    private void Activar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Adicionar:
                RequiredFieldValidator_FileUpload_archivo.Enabled = true;
                break;
        }
    }

    private void Cancelar()
    {
        Limpiar();
        Ocultar();
        Mostrar(Acciones.Iniciar);
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

    private void Cargar()
    {
        Cargar(DropDownList_ID_TIPO_CLAUSULA, Brainsbits.LLB.tabla.PARAMETROS_TIPOS_CLAUSULA);

        Cargar(DropDownList_ID_ESTADO, Brainsbits.LLB.tabla.PARAMETROS_TIPOS_CLAUSULA_ESTADOS);
        DropDownList_ID_ESTADO.SelectedValue = "1";

        cargo _cargo = new cargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable dataTable = _cargo.ObtenerRecOcupacionesPorIdEmp(Convert.ToDecimal(HiddenField_ID_EMPRESA.Value));

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_ID_OCUPACION.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("Aplicar todos los cargos", "0");
        DropDownList_ID_OCUPACION.Items.Add(item);

        foreach (DataRow dataRow in dataTable.Rows)
        {
            if (!string.IsNullOrEmpty(dataRow["ID_EMP"].ToString()))
            {
                item = new System.Web.UI.WebControls.ListItem(dataRow["ID_OCUPACION"].ToString() + "-" + dataRow["NOM_OCUPACION"].ToString(), dataRow["ID_OCUPACION"].ToString());
                DropDownList_ID_OCUPACION.Items.Add(item);
            }
        }

        DropDownList_ID_OCUPACION.DataBind();

        cliente _cliete = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        dataTable = _cliete.ObtenerEmpresaConIdEmpresa(Convert.ToDecimal(HiddenField_ID_EMPRESA.Value));
        if (dataTable.Rows.Count.Equals(0)) return;
        if (!string.IsNullOrEmpty(dataTable.Rows[0]["RAZ_SOCIAL"].ToString()))this.Label_ID_EMPRESA.Text = dataTable.Rows[0]["RAZ_SOCIAL"].ToString();
    }

    private void Cargar(DataRow dataRow)
    {
        Cargar();
        if (DBNull.Value.Equals(dataRow["ARCHIVO"]) == true)
        {
            HyperLink_ARCHIVO.Text = "Sin Archivo";
            HyperLink_ARCHIVO.Enabled = false;
        }
        else
        {
            tools _tools = new tools();
            SecureQueryString QueryStringSeguro;
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
            QueryStringSeguro["id_clausula"] = dataRow["ID_CLAUSULA"].ToString();
            HyperLink_ARCHIVO.NavigateUrl = "~/contratacion/VisorDocumentosClausulas.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            HyperLink_ARCHIVO.Enabled = true;

            HiddenField_id_clausula.Value = dataRow["ID_CLAUSULA"].ToString();
            HiddenField_ID_EMPRESA.Value = dataRow["ID_EMPRESA"].ToString();

            TextBox_DESCRIPCION.Text = dataRow["DESCRIPCION"].ToString();

            DropDownList_ID_OCUPACION.SelectedValue = dataRow["ID_OCUPACION"].ToString();
            DropDownList_ID_TIPO_CLAUSULA.SelectedValue = dataRow["ID_TIPO_CLAUSULA"].ToString();
        }
    }

    private void Cargar(decimal idEmpresa)
    {
        Clausula clausula = new Clausula(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        GridView_RESULTADOS_BUSQUEDA.DataSource = clausula.ObtenerPorIdEmpresa(idEmpresa);
        GridView_RESULTADOS_BUSQUEDA.DataBind();

        cliente _cliete = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable dataTable = _cliete.ObtenerEmpresaConIdEmpresa(idEmpresa);
        if (dataTable.Rows.Count.Equals(0)) return;
        if (!string.IsNullOrEmpty(dataTable.Rows[0]["RAZ_SOCIAL"].ToString())) Label_INFO_ADICIONAL_MODULO.Text = dataTable.Rows[0]["RAZ_SOCIAL"].ToString();

    }

    private void Cargar(DropDownList dropDownList, string parametro)
    {
        dropDownList.Items.Clear();
        parametro _parametro = new parametro(Session["idEmpresa"].ToString());
        DataTable dataTable = _parametro.ObtenerParametrosPorTabla(parametro);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione", "");
        dropDownList.Items.Add(item);
        foreach (DataRow dataRow in dataTable.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(dataRow["DESCRIPCION"].ToString(), dataRow["CODIGO"].ToString());
            dropDownList.Items.Add(item);
        }
        dropDownList.DataBind();
    }

    private void Buscar()
    {
        DataTable dataTable = new DataTable();
        try
        {
            Clausula clausula = new Clausula(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            dataTable = clausula.Buscar(Convert.ToDecimal(HiddenField_ID_EMPRESA.Value), TextBox_BUSCAR.Text);
            if (dataTable.Rows.Equals(0))  Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros que cumplan el criterio ingresado.", Proceso.Advertencia);
            GridView_RESULTADOS_BUSQUEDA.DataSource = dataTable;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
        }
        catch (Exception e)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, e.Message, Proceso.Error);
        }
        dataTable.Dispose();
    }

    #endregion metodos

    #region eventos
    protected void Button_NUEVO_Click(object sender, EventArgs e)
    {
        Activar(Acciones.Adicionar);
        Adicionar();
    }
    
    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Actualizar();
    }
    
    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(HiddenField_id_clausula.Value)) Guardar();
        else Actualizar();
    }

    protected void Button_ACTUALIZAR_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(this.HiddenField_id_clausula.Value)) Editar();
        else Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No existe una clausual para ser editada", Proceso.Error);
    }

    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        Cancelar();
    }

    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {
        Ocultar(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    protected void GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged(object sender, EventArgs e)
    {
        Ocultar();
        Clausula clausula = new Clausula(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataRow dataRow = clausula.ObtenerPorId(Convert.ToDecimal(this.GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["ID_CLAUSULA"].ToString()));
        Cargar(dataRow);
        Mostrar(Acciones.Editar);
    }

    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;
        Cargar(Convert.ToDecimal(HiddenField_ID_EMPRESA.Value));
    }

    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        Buscar();
    }
    #endregion eventos
}