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

public partial class contratacion_ccf : System.Web.UI.Page
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
        Error = 1,
        Advertencia = 2
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




    private void configurar_paneles_popup(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_mensaje.CssClass = "panel_popup";
    }

    private void Configurar()
    {
        configurar_paneles_popup(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }



    private void Iniciar()
    {
        Configurar();
        Ocultar();
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }

    private void Ocultar()
    {
        this.Panel_INFO_ADICIONAL_MODULO.Visible = false;
        this.Panel_FORM_BOTONES_ENCABEZADO.Visible = false;
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
                break;
            case Acciones.Guarda:
                this.Panel_INFO_ADICIONAL_MODULO.Visible = true;
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
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

    private void CargarGrillaCiudades()
    {
        ciudad _ciu = new ciudad(Session["idEmpresa"].ToString());

        DataTable tablaCiudades = _ciu.ObtenerTodasLasCiudadesAsociadasARegional();

        GridView_Ciudades.DataSource = tablaCiudades;
        GridView_Ciudades.DataBind();

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


                GridView_Ciudades.DataSource = null;
                GridView_Ciudades.DataBind();

                CargarGrillaCiudades();
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

    private void Cargar(DropDownList dropDownList, DataTable _dataTable, String id)
    {
        ListItem item = new ListItem("Seleccione", "");
        dropDownList.Items.Add(item);

        foreach (DataRow _dataRow in _dataTable.Rows)
        {
            item = new ListItem(_dataRow["NOMBRE"].ToString(), _dataRow[id].ToString());
            dropDownList.Items.Add(item);
        }

        dropDownList.DataBind();
    }

    private void SeleccionarCiudadesEnGrilla(Decimal ID_CAJA_C)
    {
        cajaCompensacionFamiliar _ccf = new cajaCompensacionFamiliar(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCiudades = _ccf.ObtenerCiudadesEntidad(ID_CAJA_C, "CCF");

        for (int i = 0; i < GridView_Ciudades.Rows.Count; i++)
        {
            String ID_CIUDAD = GridView_Ciudades.DataKeys[i].Values["ID_CIUDAD"].ToString();

            DataRow[] filasEncontradas = tablaCiudades.Select("ID_CIUDAD = " + ID_CIUDAD);

            if (filasEncontradas.Length > 0)
            {
                GridViewRow filaGrilla = GridView_Ciudades.Rows[i];
                CheckBox check = filaGrilla.FindControl("CheckBox_Seleccion") as CheckBox;
                check.Checked = true;
            }   
        }
    }

    private void Cargar(DataTable dataTable)
    {
        if (dataTable.Rows.Count > 0)
        {
            DataRow _dataRow = dataTable.Rows[0];

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

            SeleccionarCiudadesEnGrilla(Convert.ToDecimal(_dataRow["ID"]));
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
        cajaCompensacionFamiliar _cajaCompensacionFamiliar = new cajaCompensacionFamiliar(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable _dataTable = new DataTable();
        switch (this.DropDownList_BUSCAR.SelectedValue)
        {
            case "NOMBRE":
                _dataTable = _cajaCompensacionFamiliar.ObtenerPorNombre(this.TextBox_BUSCAR.Text);
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
            if (!String.IsNullOrEmpty(_cajaCompensacionFamiliar.MensajeError))
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES,Label_MENSAJE, "Error: Consulte con el Administrador: " + _cajaCompensacionFamiliar.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "ADVERTENCIA: No se encontró información para " + this.DropDownList_BUSCAR.SelectedItem + " : " + this.TextBox_BUSCAR.Text, Proceso.Correcto);
            }

            Mostrar(Acciones.BusquedaNoEncontro);
        }

        _dataTable.Dispose();
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

    private Int32 ObtenerItemsSeleccionadosDeGrilla(GridView grid)
    {
        Int32 contador = 0;

        foreach (GridViewRow filaGrilla in grid.Rows)
        {
            CheckBox check = filaGrilla.FindControl("CheckBox_Seleccion") as CheckBox;

            if (check.Checked == true)
            {
                contador += 1;
            }
        }

        return contador;
    }

    private void Guardar()
    {
        List<String> listaCiudades = new List<String>();

        for (int i = 0; i < GridView_Ciudades.Rows.Count; i++)
        {
            GridViewRow filaCiudad = GridView_Ciudades.Rows[i];

            CheckBox check = filaCiudad.FindControl("CheckBox_Seleccion") as CheckBox;

            if (check.Checked == true)
            {
                String ID_CIUDAD = GridView_Ciudades.DataKeys[i].Values["ID_CIUDAD"].ToString().Trim();

                listaCiudades.Add(ID_CIUDAD);
            }
        }

        cajaCompensacionFamiliar _cajaCompensacionFamiliar = new cajaCompensacionFamiliar(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Decimal ID = _cajaCompensacionFamiliar.AdicionarConCobertura(TextBox_NIT.Text, 
            TextBox_DV.Text, 
            this.TextBox_COD_ENTIDAD.Text, 
            TextBox_NOM_ENTIDAD.Text, 
            TextBox_DIR_ENTIDAD.Text,
            TextBox_TEL_ENTIDAD.Text, 
            TextBox_CONTACTO.Text, 
            TextBox_CARGO.Text,
            listaCiudades);

        if (ID == 0)
        {
            if (!String.IsNullOrEmpty(_cajaCompensacionFamiliar.MensajeError))
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Error: Consulte con el Administrador: " + _cajaCompensacionFamiliar.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Error al intentar crear la Entidad.", Proceso.Error);
            }
        }
        else
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La entidad fue creada correctamente. Se le asignó el ID = " + ID.ToString(), Proceso.Correcto);

            TextBox_ID.Text = ID.ToString();
        }

        Ocultar();
        Mostrar(Acciones.Guarda);
        Bloquear(Acciones.Guarda);
    }

    private void Modificar()
    {
        List<String> listaCiudades = new List<String>();

        for (int i = 0; i < GridView_Ciudades.Rows.Count; i++)
        {
            GridViewRow filaCiudad = GridView_Ciudades.Rows[i];

            CheckBox check = filaCiudad.FindControl("CheckBox_Seleccion") as CheckBox;

            if (check.Checked == true)
            {
                String ID_CIUDAD = GridView_Ciudades.DataKeys[i].Values["ID_CIUDAD"].ToString().Trim();

                listaCiudades.Add(ID_CIUDAD);
            }
        }

        cajaCompensacionFamiliar _cajaCompensacionFamiliar = new cajaCompensacionFamiliar(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        if (_cajaCompensacionFamiliar.ActualizarConCobertura(Convert.ToDecimal(this.TextBox_ID.Text), 
            TextBox_NIT.Text, 
            TextBox_DV.Text, 
            this.TextBox_COD_ENTIDAD.Text, 
            TextBox_NOM_ENTIDAD.Text, 
            TextBox_DIR_ENTIDAD.Text, 
            TextBox_TEL_ENTIDAD.Text,
            TextBox_CONTACTO.Text, 
            TextBox_CARGO.Text, 
            CheckBox_ESTADO.Checked, 
            listaCiudades) == false)
        {
            if (!String.IsNullOrEmpty(_cajaCompensacionFamiliar.MensajeError))
            {
                Informar(Panel_FONDO_MENSAJE,Image_MENSAJE_POPUP,Panel_MENSAJES,Label_MENSAJE, "Error: " + _cajaCompensacionFamiliar.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Error: al intentar crear la Entidad.", Proceso.Error);
            }
        }
        else
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La entidad fue modificada correctamente.", Proceso.Correcto);
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
        if (ObtenerItemsSeleccionadosDeGrilla(GridView_Ciudades) > 0)
        {
            if (String.IsNullOrEmpty(this.TextBox_ID.Text))
            {
                Guardar();
            }
            else
            {
                Modificar();
            }
        }
        else 
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar la cobertura de la entidad para poder guardar los datos.", Proceso.Advertencia);
        }
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
            cajaCompensacionFamiliar _cajaCompensacionFamiliar = new cajaCompensacionFamiliar(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            Cargar(_cajaCompensacionFamiliar.ObtenerPorIdCCF(Convert.ToDecimal(this.GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["id"].ToString())));
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


    private void FiltrarEnGrilla(GridView grilla, String valor)
    {
        foreach (GridViewRow filaGrilla in grilla.Rows)
        {
            filaGrilla.Visible = false;

            for (int i = 0; i < filaGrilla.Cells.Count; i++)
            {
                if (HttpUtility.HtmlDecode(filaGrilla.Cells[i].Text).ToUpper().Contains(valor.ToUpper()) == true)
                {
                    filaGrilla.Visible = true;
                    break;
                }
            }
        }
    }


    protected void Button_BuscarEnGrillaCiudades_Click(object sender, EventArgs e)
    {
        FiltrarEnGrilla(GridView_Ciudades, TextBox_BUscarEnGrillaCiudades.Text.Trim());
    }

    private void SeleccionarTodosEnGrilla(GridView grilla)
    {
        foreach (GridViewRow filaGrilla in grilla.Rows)
        {
            CheckBox check = filaGrilla.FindControl("CheckBox_Seleccion") as CheckBox;

            check.Checked = true;

            filaGrilla.Visible = true;
        }
    }

    protected void Button_SeleccionarTodasLasCiudades_Click(object sender, EventArgs e)
    {
        SeleccionarTodosEnGrilla(GridView_Ciudades);
    }

    private void LimpiarSeleccionadosEnGrilla(GridView grilla)
    {
        foreach (GridViewRow filaGrilla in grilla.Rows)
        {
            CheckBox check = filaGrilla.FindControl("CheckBox_Seleccion") as CheckBox;

            check.Checked = false;

            filaGrilla.Visible = true;
        }

    }
    protected void Button_LimpiarSeleccionCiudades_Click(object sender, EventArgs e)
    {
        LimpiarSeleccionadosEnGrilla(GridView_Ciudades);
    }

    private void MostrarSoloSeleccionadoEnGrilla(GridView grilla)
    {
        foreach (GridViewRow filaGrilla in grilla.Rows)
        {
            CheckBox check = filaGrilla.FindControl("CheckBox_Seleccion") as CheckBox;

            filaGrilla.Visible = check.Checked;
        }
    }

    protected void Button_MostrarSoloCiudadesSeleccionadas_Click(object sender, EventArgs e)
    {
        MostrarSoloSeleccionadoEnGrilla(GridView_Ciudades);
    }


    private void ocultar_mensaje(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.Style.Add("display", "none");
        panel_mensaje.Style.Add("display", "none");

        panel_fondo.Visible = false;
        panel_mensaje.Visible = false;
    }

    protected void Button_CERRAR_MENSAJE_Click(object sender, System.EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }


}