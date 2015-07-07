using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using Brainsbits.LLB;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB.seguridad;
using TSHAK.Components;

public partial class contratacion_Retiros : System.Web.UI.Page
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
        Iniciar = 0,
        Adicionar,
        Guardar,
        Actualizar,
        Visualizar,
        Consultar,
        Buscar,
        Habilitar,
        Editar,
        Encontrar,
        Cargar,
        Retirar
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
        if (!IsPostBack)
        {
            Iniciar();
        }
    }
    #endregion constructor

    #region metodos
    private void Iniciar()
    {
        Configurar();
        Limpiar();
        Ocultar();
        Cargar(Acciones.Iniciar);
        Bloquear();
    }

    private void Ocultar()
    {
        Panel_RESULTADOS_GRID.Visible = false;
        Panel_DATOS_RETIROS.Visible = false;
        
        Button_CANCELAR.Visible = false;
        Button_GUARDAR.Visible = false;
        Button_MODIFICAR.Visible = false;
        Button_RevertirRetiro.Visible = false;
    }

    private void Limpiar()
    {
        TextBox_regional.Text = string.Empty;
        TextBox_activo.Text = string.Empty;
        TextBox_ciudad.Text = string.Empty;
        TextBox_empresa.Text = string.Empty;
        TextBox_id_empleado.Text = string.Empty;
        TextBox_numero_documento.Text = string.Empty;
        TextBox_apellidos.Text = string.Empty;
        TextBox_nombre.Text = string.Empty;
        TextBox_fecha_ingreso.Text = string.Empty;
        TextBox_fecha_liquidacion.Text = string.Empty;
        TextBox_cargo.Text = string.Empty;

        TextBox_fecha_retiro.Text = string.Empty;
        TextBox_notas.Text = string.Empty;
        TextBox_carpeta.Text = string.Empty;

        DropDownList_estado.ClearSelection();

        CheckBox_caso_severo.Checked = false;
        CheckBox_embarazada.Checked = false;
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Encontrar:
                Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.Cargar:
                Panel_DATOS_RETIROS.Visible = true;
                Button_CANCELAR.Visible = true;
                Button_MODIFICAR.Visible = true;

                if ((string.IsNullOrEmpty(TextBox_fecha_retiro.Text) == false) && (string.IsNullOrEmpty(TextBox_fecha_liquidacion.Text) == true) && (TextBox_activo.Text.ToUpper() == "S"))
                {
                    Button_RevertirRetiro.Visible = true;
                }
                else
                {
                    Button_RevertirRetiro.Visible = false;
                }
                break;
            case Acciones.Editar:
                Button_CANCELAR.Visible = true;
                Button_MODIFICAR.Visible = true;
                break;
            case Acciones.Actualizar:
                Panel_DATOS_RETIROS.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;
                break;
        }
    }

    private void Cargar(Acciones accion)
    {
        ListItem item = new ListItem("Seleccione...", "0");

        switch (accion)
        {
            case Acciones.Iniciar:
                DropDownList_BUSCAR.Items.Add(item);
                item = new ListItem("Número de documento", "NUMERO_DOCUMENTO");
                DropDownList_BUSCAR.Items.Add(item);
                item = new ListItem("Nombre", "NOMBRE");
                DropDownList_BUSCAR.Items.Add(item);
                DropDownList_BUSCAR.DataBind();

                Cargar(DropDownList_estado, tabla.PARAMETROS_ESTADO_RETIRO);
                break;
        }
    }

    private void Cargar(DropDownList dropDownList, String tabla)
    {
        ListItem item = new ListItem("Seleccione...", "");
        parametro _parametro = new parametro(Session["idEmpresa"].ToString());
        DataTable _dataTable = _parametro.ObtenerParametrosPorTabla(tabla);
        dropDownList.Items.Add(item);
        foreach (DataRow _dataRow in _dataTable.Rows)
        {
            item = new ListItem(_dataRow["DESCRIPCION"].ToString(), _dataRow["CODIGO"].ToString());
            dropDownList.Items.Add(item);
        }
        dropDownList.DataBind();
        _dataTable.Dispose();
    }

    private void Configurar()
    {
        Page.Header.Title = "RETIROS";
        Configurar(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    private void Configurar(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_mensaje.CssClass = "panel_popup";
    }

    private void Configurar(Boolean letras, Boolean numeros)
    {
        FilteredTextBoxExtender_TextBox_BUSCAR_Numbers.Enabled = numeros;
        FilteredTextBoxExtender_TextBox_BUSCAR_Letras.Enabled = letras;
    }

    private void Ocultar(Panel panel_fondo, Panel panel_mensaj)
    {
        panel_fondo.Style.Add("display", "none");
        panel_mensaj.Style.Add("display", "none");

        panel_fondo.Visible = false;
        panel_mensaj.Visible = false;

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

    private void Buscar()
    {
        DataTable dataTable = new DataTable();
        Retiro retiro;

        Ocultar();
        retiro = new Retiro(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        try
        {
            dataTable = retiro.Consultar(TextBox_BUSCAR.Text);

            if (dataTable.Rows.Count > 0)
            {
                GridView_RESULTADOS_BUSQUEDA.DataSource = dataTable;
                GridView_RESULTADOS_BUSQUEDA.DataBind();
                Mostrar(Acciones.Encontrar);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información para " + this.DropDownList_BUSCAR.SelectedItem + " : " + this.TextBox_BUSCAR.Text , Proceso.Advertencia);
            }
        }
        catch (Exception e)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, e.Message, Proceso.Error);
        }
        if (dataTable == null) dataTable.Dispose();

    }

    private void Bloquear()
    {
        TextBox_fecha_retiro.Enabled = false;
        DropDownList_estado.Enabled = false;
        TextBox_notas.Enabled = false;
        TextBox_carpeta.Enabled = false;
    }

    private void Activar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Retirar:
                RequiredFieldValidator_TextBox_fecha_retiro.Enabled = true;
                RequiredFieldValidator_DropDownList_estado.Enabled = true;
                RequiredFieldValidator_TextBox_notas.Enabled = true;
                break;
        }
    }

    private void Inactivar()
    {
        RequiredFieldValidator_TextBox_fecha_retiro.Enabled = false;
        RequiredFieldValidator_DropDownList_estado.Enabled = false;
        RequiredFieldValidator_TextBox_notas.Enabled = false;
    }

    private void Desbloquear(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Retirar:
                TextBox_fecha_retiro.Enabled = true;
                DropDownList_estado.Enabled = true;
                TextBox_notas.Enabled = true;
                TextBox_carpeta.Enabled = true;
                break;
            case Acciones.Actualizar:
                TextBox_carpeta.Enabled = true;
                break;
        }
    }

    private void Editar()
    {
        Ocultar();
        Inactivar();
        if (TextBox_activo.Text.Equals("S") || (string.IsNullOrEmpty(TextBox_fecha_liquidacion.Text)))
        {
            Desbloquear(Acciones.Retirar);
            Activar(Acciones.Retirar);
        }
        else Desbloquear(Acciones.Actualizar);
        Mostrar(Acciones.Actualizar);
    }

    private void Actualizar()
    {
        Retiro retiro;
        DateTime fecha = new DateTime();

        Decimal idEmpleado = Convert.ToDecimal(TextBox_id_empleado.Text);

        retiro = new Retiro(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        try
        {
            if (retiro.Actualizar(idEmpleado, string.IsNullOrEmpty(TextBox_fecha_retiro.Text)? fecha:Convert.ToDateTime(TextBox_fecha_retiro.Text), TextBox_notas.Text, DropDownList_estado.SelectedValue, TextBox_carpeta.Text))
            {
                Limpiar();
                Bloquear();
                Cargar(idEmpleado);
   
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El retiro fue actualizado correctamente.", Proceso.Correcto);
            }
            else Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Se genero un error al intentar actualizar el retiro.", Proceso.Error);
        }

        catch (Exception e)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, e.Message, Proceso.Error);
        }
    }
    #endregion medodos

    #region eventos
    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Editar();
    }
    
    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        Actualizar();
    }
    
    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(TextBox_id_empleado.Text) == false)
        {
            Decimal idEmpleado = Convert.ToDecimal(TextBox_id_empleado.Text);

            Limpiar();
            Bloquear();
            Cargar(idEmpleado);
        }
        else
        { 
            Limpiar();
            Ocultar();
            Cargar(Acciones.Iniciar);
            Bloquear();
        }
    }
    
    protected void DropDownList_BUSCAR_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (DropDownList_BUSCAR.SelectedValue)
        {
            case "":
                Configurar(false, false);
                break;
            case "NUMERO_DOCUMENTO":
                Configurar(false, true);
                break;
            case "NOMBRE":
                Configurar(true, false);
                break;
        }
    }
    
    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        Buscar();
    }
    
    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {
        Ocultar(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }
    
    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;
        Buscar();
    }
    
    protected void GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged(object sender, EventArgs e)
    {
        Limpiar();
        Cargar(Convert.ToDecimal(this.GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["id_empleado"].ToString()));
    }

    private void Cargar(decimal idEmpleado)
    {
        Retiro retiro = new Retiro(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable dataTable = new DataTable();
        DataRow dataRow;

        Ocultar();
        try
        {
            dataTable = retiro.ObtenerPorIdEmpleado(idEmpleado);
            if (dataTable.Rows.Count > 0)
            {
                dataRow = dataTable.Rows[0];
                if (!string.IsNullOrEmpty(dataRow["regional"].ToString())) TextBox_regional.Text = dataRow["regional"].ToString();
                if (!string.IsNullOrEmpty(dataRow["ciudad"].ToString())) TextBox_ciudad.Text = dataRow["ciudad"].ToString();
                if (!string.IsNullOrEmpty(dataRow["empresa"].ToString())) TextBox_empresa.Text = dataRow["empresa"].ToString();
                if (!string.IsNullOrEmpty(dataRow["id_empleado"].ToString())) TextBox_id_empleado.Text = dataRow["id_empleado"].ToString();
                if (!string.IsNullOrEmpty(dataRow["activo"].ToString())) TextBox_activo.Text = dataRow["activo"].ToString();
                if (!string.IsNullOrEmpty(dataRow["numero_documento"].ToString())) TextBox_numero_documento.Text = dataRow["numero_documento"].ToString();
                if (!string.IsNullOrEmpty(dataRow["nombre"].ToString())) TextBox_nombre.Text = dataRow["nombre"].ToString();
                if (!string.IsNullOrEmpty(dataRow["apellidos"].ToString())) TextBox_apellidos.Text = dataRow["apellidos"].ToString();
                if (!string.IsNullOrEmpty(dataRow["cargo"].ToString())) TextBox_cargo.Text = dataRow["cargo"].ToString();
                if (!string.IsNullOrEmpty(dataRow["fecha_ingreso"].ToString())) TextBox_fecha_ingreso.Text = dataRow["fecha_ingreso"].ToString();
                if (!string.IsNullOrEmpty(dataRow["fecha_retiro"].ToString())) TextBox_fecha_retiro.Text = dataRow["fecha_retiro"].ToString();
                if (!string.IsNullOrEmpty(dataRow["fecha_liquidacion"].ToString())) TextBox_fecha_liquidacion.Text = dataRow["fecha_liquidacion"].ToString();

                if (!string.IsNullOrEmpty(dataRow["caso_severo"].ToString()))
                {
                    if (dataRow["caso_severo"].ToString().Equals("S")) CheckBox_caso_severo.Checked = true;
                    else CheckBox_caso_severo.Checked = false;
                }
                if (!string.IsNullOrEmpty(dataRow["embarazada"].ToString())) TextBox_regional.Text = dataRow["embarazada"].ToString();
                {
                    if (dataRow["caso_severo"].ToString().Equals("S")) CheckBox_embarazada.Checked = true;
                    else CheckBox_embarazada.Checked = false;
                }
                if (!string.IsNullOrEmpty(dataRow["notas"].ToString())) TextBox_notas.Text = dataRow["notas"].ToString();
                if (!string.IsNullOrEmpty(dataRow["estado"].ToString())) DropDownList_estado.SelectedValue = dataRow["estado"].ToString();
                if (!string.IsNullOrEmpty(dataRow["carpeta"].ToString())) TextBox_carpeta.Text = dataRow["carpeta"].ToString();

                HiddenField_Archivo.Value = dataRow["ARCHIVO"].ToString().Trim();

                Mostrar(Acciones.Cargar);

            }
        }
        catch (Exception e)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, e.Message, Proceso.Error);
        }

        if (dataTable != null) dataTable.Dispose();
    }

    #endregion eventos
    protected void Button_RevertirRetiro_Click(object sender, EventArgs e)
    {
        decimal idEmpleado = Convert.ToDecimal(TextBox_id_empleado.Text);

        Retiro _r = new Retiro(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable _tablaResultado = _r.ReversarRetiroTemporal(idEmpleado);

        if (String.IsNullOrEmpty(_r.MensajeError) == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _r.MensajeError, Proceso.Error);
        }
        else
        { 
            DataRow filaResultado = _tablaResultado.Rows[0];

            if (filaResultado["TIPO"].ToString().ToUpper() == "ERROR")
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, filaResultado["MENSAJE"].ToString(), Proceso.Advertencia);
            }
            else
            {
                Limpiar();
                Bloquear();
                Cargar(idEmpleado);

                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, filaResultado["MENSAJE"].ToString(), Proceso.Correcto);
            }
        }
    }
}