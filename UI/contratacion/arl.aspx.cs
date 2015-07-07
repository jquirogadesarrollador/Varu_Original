using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using Brainsbits.LLB;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;
using TSHAK.Components;

public partial class contratacion_arp : System.Web.UI.Page
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

    #region varialbles
    private enum Acciones
    {
        Inicio = 0,
        BusquedaEncontro = 1,
        BusquedaNoEncontro = 2,
        Adiciona = 3,
        Guarda = 4,
        Modifica = 5,
        Visualiza = 6
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1
    }
    #endregion varialbles
    
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
        Ocultar();
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }

    private void Ocultar()
    {
        this.Panel_INFO_ADICIONAL_MODULO.Visible = false;
        this.Panel_FORM_BOTONES_ENCABEZADO.Visible = false;
        this.Panel_MENSAJES.Visible = false;
        this.Panel_RESULTADOS_GRID.Visible = false;
        this.Panel_CONTROL_REGISTRO.Visible = false;
        this.Panel_FORMULARIO.Visible = false;
        this.Panel_DATOS.Visible = false;
        this.Panel_FORM_BOTONES_PIE.Visible = false;

        Button_NUEVO.Visible = false;
        Button_GUARDAR.Visible = false;
        Button_MODIFICAR.Visible = false;
        Button_CANCELAR.Visible = false;

        Button_GUARDAR_1.Visible = false;
        Button_MODIFICAR_1.Visible = false;
        Button_CANCELAR_1.Visible = false;

        Label_ID.Visible = false;
        TextBox_ID.Visible = false;
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Button_NUEVO.Visible = true;
                break;
            case Acciones.Adiciona:
                Panel_INFO_ADICIONAL_MODULO.Visible = true;
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Panel_FORMULARIO.Visible = true;
                Panel_DATOS.Visible = true;
                Panel_FORM_BOTONES_PIE.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
                break;
            case Acciones.BusquedaEncontro:
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                this.Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.BusquedaNoEncontro:
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                this.Panel_MENSAJES.Visible = true;
                break;
            case Acciones.Guarda:
                this.Panel_INFO_ADICIONAL_MODULO.Visible = true;
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                this.Panel_MENSAJES.Visible = true;
                this.Panel_FORMULARIO.Visible = true;
                this.Panel_DATOS.Visible = true;
                this.Panel_FORM_BOTONES_PIE.Visible = true;
                this.Button_MODIFICAR.Visible = true;
                this.Button_MODIFICAR_1.Visible = true;
                this.Button_NUEVO.Visible = true;
                break;
            case Acciones.Modifica:
                this.Panel_INFO_ADICIONAL_MODULO.Visible = true;
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                this.Panel_CONTROL_REGISTRO.Visible = true;
                this.Panel_FORMULARIO.Visible = true;
                this.Panel_DATOS.Visible = true;
                this.Panel_FORM_BOTONES_PIE.Visible = true;
                this.Button_GUARDAR.Visible = true;
                this.Button_GUARDAR_1.Visible = true;
                this.Button_CANCELAR.Visible = true;
                this.Button_CANCELAR_1.Visible = true;
                break;
            case Acciones.Visualiza:
                this.Panel_INFO_ADICIONAL_MODULO.Visible = true;
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                this.Panel_CONTROL_REGISTRO.Visible = true;
                this.Panel_FORMULARIO.Visible = true;
                this.Panel_DATOS.Visible = true;
                this.Panel_FORM_BOTONES_PIE.Visible = true;
                this.Button_MODIFICAR.Visible = true;
                this.Button_MODIFICAR_1.Visible = true;
                this.Button_CANCELAR.Visible = true;
                this.Button_CANCELAR_1.Visible = true;
                break;
        }
    }

    private void Cargar(Acciones accion)
    {
        ListItem item = new ListItem("Ninguno", "0");
        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        switch (accion)
        {
            case Acciones.Inicio:
                DropDownList_BUSCAR.Items.Add(item);
                item = new ListItem("NOMBRE", "NOMBRE");
                DropDownList_BUSCAR.Items.Add(item);
                DropDownList_BUSCAR.DataBind();

                break;
        }
    }

    private void Cargar(DropDownList dropDownList, String tabla)
    {
        ListItem item = new ListItem("Seleccione", "");
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

    private void Cargar(DataTable dataTable)
    {
        if (dataTable.Rows.Count > 0)
        {
            foreach (DataRow _dataRow in dataTable.Rows)
            {
                if (!String.IsNullOrEmpty(_dataRow["ID"].ToString())) this.TextBox_ID.Text = _dataRow["ID"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["NIT"].ToString())) this.TextBox_NIT.Text = _dataRow["NIT"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["DIG_VER"].ToString())) this.TextBox_DV.Text = _dataRow["DIG_VER"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["COD_ENTIDAD"].ToString())) this.TextBox_COD_ENTIDAD.Text = _dataRow["COD_ENTIDAD"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["NOM_ENTIDAD"].ToString())) this.TextBox_NOM_ENTIDAD.Text = _dataRow["NOM_ENTIDAD"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["DIR_ENTIDAD"].ToString())) this.TextBox_DIR_ENTIDAD.Text = _dataRow["DIR_ENTIDAD"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["TEL_ENTIDAD"].ToString())) this.TextBox_TEL_ENTIDAD.Text = _dataRow["TEL_ENTIDAD"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["CONTACTO"].ToString())) this.TextBox_CONTACTO.Text = _dataRow["CONTACTO"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["CARGO"].ToString())) this.TextBox_CARGO.Text = _dataRow["CARGO"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["FCH_CRE"].ToString())) this.TextBox_FCH_CRE.Text = _dataRow["FCH_CRE"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["USU_CRE"].ToString())) this.TextBox_USU_CRE.Text = _dataRow["USU_CRE"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["FCH_MOD"].ToString())) this.TextBox_FCH_MOD.Text = _dataRow["FCH_MOD"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["USU_MOD"].ToString())) this.TextBox_USU_MOD.Text = _dataRow["USU_MOD"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["ACTIVO"].ToString()))
                {
                    if (_dataRow["ACTIVO"].Equals("S")) CheckBox_ESTADO.Checked = true;
                    else CheckBox_ESTADO.Checked = false;
                }
            }
        }
        dataTable.Dispose();
    }

    private void Bloquear(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Guarda:
                this.Panel_FORMULARIO.Enabled = false;
                break;
            case Acciones.Visualiza:
                this.Panel_FORMULARIO.Enabled = false;
                break;
        }
    }

    private void Desbloquear(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Modifica:
                this.Panel_FORMULARIO.Enabled = true;
                break;
        }
    }

    protected void Buscar()
    {
        Ocultar();
        arp _arp = new arp(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable _dataTable = new DataTable();
        switch (this.DropDownList_BUSCAR.SelectedValue)
        {
            case "NOMBRE":
                _dataTable = _arp.ObtenerPorNombre(this.TextBox_BUSCAR.Text);
                break;
        }

        if (_dataTable.Rows.Count > 0)
        {
            GridView_RESULTADOS_BUSQUEDA.DataSource = _dataTable;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
            Mostrar(Acciones.BusquedaEncontro);
        }
        else
        {
            if (!String.IsNullOrEmpty(_arp.MensajeError)) Informar(Label_MENSAJE, "Error: Consulte con el Administrador: " + _arp.MensajeError, Proceso.Error);
            else Informar(Label_MENSAJE, "ADVERTENCIA: No se encontró información para " + this.DropDownList_BUSCAR.SelectedItem + " : " + this.TextBox_BUSCAR.Text, Proceso.Error);
            Mostrar(Acciones.BusquedaNoEncontro);
        }
        _dataTable.Dispose();
    }

    private void Informar(Label label, String mensaje, Proceso proceso)
    {
        label.Text = mensaje;
        switch (proceso)
        {
            case Proceso.Correcto:
                label.ForeColor = System.Drawing.Color.Green;
                break;
            case Proceso.Error:
                label.ForeColor = System.Drawing.Color.Red;
                break;
        }
    }

    private void Limpiar()
    {
        TextBox_NOM_ENTIDAD.Text = String.Empty;
        TextBox_NIT.Text = String.Empty;
        TextBox_DV.Text = String.Empty;
        TextBox_COD_ENTIDAD.Text = String.Empty;
        TextBox_DIR_ENTIDAD.Text = String.Empty;
        TextBox_TEL_ENTIDAD.Text = String.Empty;
        TextBox_CONTACTO.Text = String.Empty;
        TextBox_CARGO.Text = String.Empty;
        TextBox_BUSCAR.Text = String.Empty;
        DropDownList_BUSCAR.Items.Clear();
    }

    private void Guardar()
    {
        arp _arp = new arp(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        Decimal ID = _arp.Adicionar(TextBox_NIT.Text, TextBox_DV.Text, this.TextBox_COD_ENTIDAD.Text, TextBox_NOM_ENTIDAD.Text, TextBox_DIR_ENTIDAD.Text,
                TextBox_TEL_ENTIDAD.Text, TextBox_CONTACTO.Text, TextBox_CARGO.Text);
        if (ID == 0)
        {
            if (!String.IsNullOrEmpty(_arp.MensajeError)) Informar(Label_MENSAJE, "Error: Consulte con el Administrador: " + _arp.MensajeError, Proceso.Error);
        }
        else
        {
            Informar(Label_MENSAJE, "La entidad fue creada correctamente y se le asignó el ID: " + ID.ToString() + ".", Proceso.Correcto);
            TextBox_ID.Text = ID.ToString();
        }
        Ocultar();
        Mostrar(Acciones.Guarda);
        Bloquear(Acciones.Guarda);
    }

    private void Modificar()
    {
        arp _arp = new arp(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        if (_arp.Actualizar(Convert.ToDecimal(this.TextBox_ID.Text), TextBox_NIT.Text, TextBox_DV.Text, this.TextBox_COD_ENTIDAD.Text, TextBox_NOM_ENTIDAD.Text, TextBox_DIR_ENTIDAD.Text, TextBox_TEL_ENTIDAD.Text, TextBox_CONTACTO.Text, TextBox_CARGO.Text, CheckBox_ESTADO.Checked))
        {
            if (!String.IsNullOrEmpty(_arp.MensajeError)) Informar(Label_MENSAJE, "Error: Consulte con el Administrador: " + _arp.MensajeError, Proceso.Error);
        }
        else
        {
            Informar(Label_MENSAJE, "La entidad fue actualizada correctamente.", Proceso.Correcto);
            TextBox_ID.Text = ID.ToString();
        }
        Ocultar();
        Mostrar(Acciones.Modifica);
        Bloquear(Acciones.Modifica);
    }

    private void configurarCaracteresAceptadosBusqueda(Boolean letras, Boolean numeros)
    {
        FilteredTextBoxExtender_TextBox_BUSCAR_Numbers.Enabled = numeros;
        FilteredTextBoxExtender_TextBox_BUSCAR_Letras.Enabled = letras;
    }
    #endregion metodos
    
    #region eventos
    protected void Button_NUEVO_Click(object sender, EventArgs e)
    {
        Limpiar();
        Ocultar();
        Cargar(Acciones.Inicio);
        Mostrar(Acciones.Adiciona);
        this.TextBox_NOM_ENTIDAD.Focus();
    }

    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar();
        Mostrar(Acciones.Modifica);
        Desbloquear(Acciones.Modifica);
        this.TextBox_NOM_ENTIDAD.Focus();
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(this.TextBox_ID.Text)) Guardar();
        else Modificar();
    }

    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        Limpiar();
        Ocultar();
        Cargar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
    }
    
    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        Buscar();
    }
    
    protected void GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        if (!String.IsNullOrEmpty(this.GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["id"].ToString()))
        {
            arp _arp = new arp(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            Cargar(_arp.ObtenerPorIdARP(Convert.ToDecimal(this.GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["id"].ToString())));
        }
        Ocultar();
        Mostrar(Acciones.Visualiza);
        Bloquear(Acciones.Visualiza);
    }
    
    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;
        Buscar();
    }
    
    protected void DropDownList_BUSCAR_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (DropDownList_BUSCAR.SelectedValue)
        {
            case "":
                configurarCaracteresAceptadosBusqueda(false, false);
                break;
            case "NOMBRE":
                configurarCaracteresAceptadosBusqueda(true, false);
                break;
        }
    }
    #endregion eventos
}