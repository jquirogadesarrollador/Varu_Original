using System;
using System.Web.UI.WebControls;
using Brainsbits.LLB.maestras;
using System.Data;
using Brainsbits.LLB.comercial;
using Brainsbits.LLB;
using Brainsbits.LLB.seguridad;
using TSHAK.Components;
using System.Web;

public partial class _Default : System.Web.UI.Page
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

    private enum Acciones
    {
        Inicio = 0,
        CargarCargo,
        Nuevo,
        Modificar
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }

    private void configurar_paneles_popup(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_mensaje.CssClass = "panel_popup";
    }

    private void Configurar()
    {
        Page.Header.Title = "CARGOS DE TRABAJO";

        configurar_paneles_popup(Panel_FONDO_MENSAJE, Panel_MENSAJES);
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
                Panel_FORMULARIO.Visible = false;
                Panel_CONTROL_REGISTRO.Visible = false;
                Panel_TIPO_CARGO.Visible = false;
                Panel_EMPRESA.Visible = false;
                Panel_CARGOS_DANE.Visible = false;
                Panel_BuscadorCargo.Visible = false;
                Panel_DATOS_BASICOS_CARGO.Visible = false;

                Panel_BOTONES_ACCION_1.Visible = false;
                Button_NUEVO_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;

                Panel_ESTADO_CARGO.Visible = false;
                break;
            case Acciones.Modificar:
                Button_NUEVO.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_CANCELAR.Visible = false;

                Panel_CONTROL_REGISTRO.Visible = false;

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

                RadioButtonList_TIPO_GARGO.Enabled = false;
                DropDownList_EMPRESA_USUARIA.Enabled = false;
                DropDownList_GRUPOS_PRIMARIOS.Enabled = false;

                TextBox_NOMBRE_CARGO_NUEVO.Enabled = false;
                TextBox_FUNCIONES_CARGO.Enabled = false;

                TextBox_RESPONSABILIDADES.Enabled = false;
                TextBox_OBLIGACIONES.Enabled = false;

                CheckBox_COMISIONA.Enabled = false;

                DropDownList_ESTADO.Enabled = false;
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
                break;
            case Acciones.CargarCargo:
                Panel_FORM_BOTONES.Visible = true;
                Button_MODIFICAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_FORMULARIO.Visible = true;
                Panel_CONTROL_REGISTRO.Visible = true;
                Panel_TIPO_CARGO.Visible = true;
                Panel_EMPRESA.Visible = true;
                Panel_CARGOS_DANE.Visible = true;
                Panel_DATOS_BASICOS_CARGO.Visible = true;

                Panel_ESTADO_CARGO.Visible = true;

                Panel_BOTONES_ACCION_1.Visible = true;
                Button_MODIFICAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;

                break;
            case Acciones.Nuevo:
                Panel_FORM_BOTONES.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_FORMULARIO.Visible = true;
                Panel_TIPO_CARGO.Visible = true;
                Panel_CARGOS_DANE.Visible = true;
                Panel_DATOS_BASICOS_CARGO.Visible = true;

                Panel_BuscadorCargo.Visible = true;

                Panel_BOTONES_ACCION_1.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
                break;
            case Acciones.Modificar:
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_BuscadorCargo.Visible = true;

                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
                break;

        }
    }

    private void configurarCaracteresAceptadosBusqueda(Boolean letras, Boolean numeros)
    {
        FilteredTextBoxExtender_TextBox_BUSCAR_Numbers.Enabled = numeros;
        FilteredTextBoxExtender_TextBox_BUSCAR_Letras.Enabled = letras;
    }

    private void cargar_DropDownList_BUSCAR()
    {
        DropDownList_BUSCAR.Items.Clear();

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_BUSCAR.Items.Add(item);

        item = new ListItem("Empresa Cliente", "RAZ_SOCIAL");
        DropDownList_BUSCAR.Items.Add(item);

        item = new ListItem("Cargo de Trabajo", "NOM_OCUPACION");
        DropDownList_BUSCAR.Items.Add(item);

        DropDownList_BUSCAR.DataBind();
    }

    private void iniciar_seccion_de_busqueda()
    {
        TextBox_BUSCAR.Text = "";
        cargar_DropDownList_BUSCAR();
    }

    private void Cargar_DropDownList_GRUPOS_PRIMARIOS_EnBlanco()
    {
        DropDownList_GRUPOS_PRIMARIOS.Items.Clear();
        DropDownList_GRUPOS_PRIMARIOS.Items.Add(new ListItem("DEBE REALIZAR BUSQUEDA...", ""));

        DropDownList_GRUPOS_PRIMARIOS.DataBind();
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                configurarCaracteresAceptadosBusqueda(true, true);

                HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "SIN_FILTRO";
                HiddenField_FILTRO_DROP.Value = String.Empty;
                HiddenField_FILTRO_DATO.Value = String.Empty;

                iniciar_seccion_de_busqueda();
                break;
            case Acciones.Nuevo:
                HiddenField_ID_OCUPACION.Value = String.Empty;

                Cargar_DropDownList_GRUPOS_PRIMARIOS_EnBlanco();

                RadioButtonList_TIPO_GARGO.SelectedIndex = -1;

                TextBox_NOMBRE_CARGO_NUEVO.Text = "";
                TextBox_FUNCIONES_CARGO.Text = "";
                
                TextBox_OBLIGACIONES.Text = "";
                TextBox_RESPONSABILIDADES.Text = "";

                CheckBox_COMISIONA.Checked = false;
                break;
            case Acciones.Modificar:
                TextBox_BUSCADOR_CARGO.Text = "";
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

    private void Activar(Acciones accion)
    { 
        switch(accion)
        {
            case Acciones.Nuevo:
                RadioButtonList_TIPO_GARGO.Enabled = true;
                DropDownList_EMPRESA_USUARIA.Enabled = true;
                DropDownList_GRUPOS_PRIMARIOS.Enabled = true;
                TextBox_NOMBRE_CARGO_NUEVO.Enabled = true;
                TextBox_FUNCIONES_CARGO.Enabled = true;
                TextBox_RESPONSABILIDADES.Enabled = true;
                TextBox_OBLIGACIONES.Enabled = true;
                CheckBox_COMISIONA.Enabled = true;
                break;
            case Acciones.Modificar:
                RadioButtonList_TIPO_GARGO.Enabled = true;
                DropDownList_EMPRESA_USUARIA.Enabled = true;

                DropDownList_GRUPOS_PRIMARIOS.Enabled = true;

                TextBox_NOMBRE_CARGO_NUEVO.Enabled = true;
                TextBox_FUNCIONES_CARGO.Enabled = true;
                TextBox_RESPONSABILIDADES.Enabled = true;
                TextBox_OBLIGACIONES.Enabled = true;
                CheckBox_COMISIONA.Enabled = true;

                DropDownList_ESTADO.Enabled = true;
                break;
        }
    }

    protected void Button_NUEVO_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Nuevo);
        Activar(Acciones.Nuevo);
        Cargar(Acciones.Nuevo);
    }
    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Modificar);
        Mostrar(Acciones.Modificar);
        Activar(Acciones.Modificar);
        Cargar(Acciones.Modificar);
    }

    private void Guardar()
    {
        String TIPO_CARGO = RadioButtonList_TIPO_GARGO.SelectedValue;
        
        Decimal ID_EMP = 0;
        if (TIPO_CARGO.ToUpper() == "CON EMPRESA")
        {
            ID_EMP = Convert.ToDecimal(DropDownList_EMPRESA_USUARIA.SelectedValue);
        }

        String ID_GRUPOS_PRIMARIOS = DropDownList_GRUPOS_PRIMARIOS.SelectedValue;
        String NOMBRE_CARGO = TextBox_NOMBRE_CARGO_NUEVO.Text.Trim().ToUpper();
        String FUNCIONES_CARGO = TextBox_FUNCIONES_CARGO.Text.Trim().ToUpper();
        String OBLIGACIONES = null;
        String RESPONSABILIDADES = null;
        if (String.IsNullOrEmpty(TextBox_OBLIGACIONES.Text.Trim()) == false)
        {
            OBLIGACIONES = TextBox_OBLIGACIONES.Text.Trim();        
        }
        if (String.IsNullOrEmpty(TextBox_RESPONSABILIDADES.Text.Trim()) == false)
        {
            RESPONSABILIDADES = TextBox_RESPONSABILIDADES.Text.Trim();
        }

        String COMISIONA = "N";
        if (CheckBox_COMISIONA.Checked == true)
        {
            COMISIONA = "S";
        }

        cargo _cargo = new cargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Decimal ID_OCUPACION = _cargo.AdicionarRecOcupaciones(ID_EMP, ID_GRUPOS_PRIMARIOS, NOMBRE_CARGO, FUNCIONES_CARGO, COMISIONA, OBLIGACIONES, RESPONSABILIDADES);

        if (ID_OCUPACION <= 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _cargo.MensajeError, Proceso.Error);
        }
        else
        {
            Cargar(ID_OCUPACION);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El cargo " + NOMBRE_CARGO + " fue creado correctamente, y se le asignó el ID: " + ID_OCUPACION.ToString() + ".", Proceso.Correcto);
        }
    }

    private void Modificar()
    {

        Decimal ID_OCUPACION = Convert.ToDecimal(HiddenField_ID_OCUPACION.Value);

        String TIPO_CARGO = RadioButtonList_TIPO_GARGO.SelectedValue;

        Decimal ID_EMP = 0;
        if (TIPO_CARGO.ToUpper() == "CON EMPRESA")
        {
            ID_EMP = Convert.ToDecimal(DropDownList_EMPRESA_USUARIA.SelectedValue);
        }

        String ID_GRUPOS_PRIMARIOS = DropDownList_GRUPOS_PRIMARIOS.SelectedValue;
        String NOMBRE_CARGO = TextBox_NOMBRE_CARGO_NUEVO.Text.Trim().ToUpper();
        String FUNCIONES_CARGO = TextBox_FUNCIONES_CARGO.Text.Trim().ToUpper();
        String RESPONSABILIDADES = null;
        String OBLIGACIONES = null;
        if (String.IsNullOrEmpty(TextBox_RESPONSABILIDADES.Text.Trim()) == false)
        {
            RESPONSABILIDADES = TextBox_RESPONSABILIDADES.Text.Trim();
        }  
        if(String.IsNullOrEmpty(TextBox_OBLIGACIONES.Text.Trim()) == false)
        {
            OBLIGACIONES = TextBox_OBLIGACIONES.Text.Trim();
        }

        String COMISIONA = "N";
        if (CheckBox_COMISIONA.Checked == true)
        {
            COMISIONA = "S";
        }

        String ACTIVO = DropDownList_ESTADO.SelectedValue;

        cargo _cargo = new cargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean verificador = _cargo.ActualizarRecOcupaciones(ID_OCUPACION, ID_EMP, ID_GRUPOS_PRIMARIOS, NOMBRE_CARGO, FUNCIONES_CARGO, ACTIVO, COMISIONA, OBLIGACIONES, RESPONSABILIDADES);

        if (verificador == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _cargo.MensajeError, Proceso.Error);
        }
        else
        {
            Cargar(ID_OCUPACION);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El cargo " + NOMBRE_CARGO + " fue modificado correctamente.", Proceso.Correcto);
        }
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(HiddenField_ID_OCUPACION.Value) == true)
        {
            Guardar();
        }
        else
        {
            Modificar();
        }
    }
    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(HiddenField_ID_OCUPACION.Value) == false)
        {
            Decimal ID_OCUPACION = Convert.ToDecimal(HiddenField_ID_OCUPACION.Value);

            Cargar(ID_OCUPACION);
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
        }
    }
    protected void DropDownList_BUSCAR_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_BUSCAR.SelectedValue == "")
        {
            configurarCaracteresAceptadosBusqueda(true, true);
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "RAZ_SOCIAL")
            {
                configurarCaracteresAceptadosBusqueda(true, false);
            }
            else
            {
                if (DropDownList_BUSCAR.SelectedValue == "NOM_OCUPACION")
                {
                    configurarCaracteresAceptadosBusqueda(false, false);
                }
            }
        }
        TextBox_BUSCAR.Text = "";
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
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);

        String datosCapturados = HiddenField_FILTRO_DATO.Value;
        String campo = HiddenField_FILTRO_DROP.Value;

        cargo _cargo = new cargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaResultadosBusqueda = new DataTable();

        if (campo == "RAZ_SOCIAL")
        {
            tablaResultadosBusqueda = _cargo.ObtenerRecOcupacionesPorRazSocial(datosCapturados);
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "NOM_OCUPACION")
            {
                tablaResultadosBusqueda = _cargo.ObtenerRecOcupacionesPorNomOcupacion(datosCapturados);
            }
        }

        if (tablaResultadosBusqueda.Rows.Count <= 0)
        {
            if (_cargo.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _cargo.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros que cumplieran los datos de busqueda.", Proceso.Advertencia);
            }

            Panel_RESULTADOS_GRID.Visible = false;
        }
        else
        {
            Panel_RESULTADOS_GRID.Visible = true;
            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaResultadosBusqueda;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
        }
    }

    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "CON_FILTRO";
        HiddenField_FILTRO_DROP.Value = DropDownList_BUSCAR.SelectedValue;
        HiddenField_FILTRO_DATO.Value = TextBox_BUSCAR.Text;

        Buscar();
    }

    private void cargar_informacion_control_registro(DataRow filaCargo)
    {
        TextBox_USU_CRE.Text = filaCargo["USU_CRE"].ToString();
        try
        {
            TextBox_FCH_CRE.Text = DateTime.Parse(filaCargo["FCH_CRE"].ToString()).ToShortDateString();
            TextBox_HOR_CRE.Text = DateTime.Parse(filaCargo["FCH_CRE"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_CRE.Text = "";
            TextBox_HOR_CRE.Text = "";
        }
        TextBox_USU_MOD.Text = filaCargo["USU_MOD"].ToString();
        try
        {
            TextBox_FCH_MOD.Text = DateTime.Parse(filaCargo["FCH_MOD"].ToString()).ToShortDateString();
            TextBox_HOR_MOD.Text = DateTime.Parse(filaCargo["FCH_MOD"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_MOD.Text = "";
            TextBox_HOR_MOD.Text = "";
        }
    }

    private void cargar_DropDownList_EMPRESA_USUARIA()
    {
        DropDownList_EMPRESA_USUARIA.Items.Clear();

        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaEmpresasActivas = _cliente.ObtenerTodasLasEmpresasActivas();

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_EMPRESA_USUARIA.Items.Add(item);

        foreach (DataRow fila in tablaEmpresasActivas.Rows)
        {
            item = new ListItem(fila["RAZ_SOCIAL"].ToString(), fila["ID_EMPRESA"].ToString());
            DropDownList_EMPRESA_USUARIA.Items.Add(item);
        }

        DropDownList_EMPRESA_USUARIA.DataBind();
    }


    private void cargar_DropDownList_GRUPOS_PRIMARIOS(DataRow fila)
    {
        DropDownList_GRUPOS_PRIMARIOS.Items.Clear();

        ListItem item = new ListItem("Seleccione Cargo...", "");
        DropDownList_GRUPOS_PRIMARIOS.Items.Add(item);

        DropDownList_GRUPOS_PRIMARIOS.Items.Add(new ListItem(fila["DESCRIPCION"].ToString().Trim(), fila["ID_GRUPOS_PRIMARIOS"].ToString().Trim()));        

        DropDownList_GRUPOS_PRIMARIOS.DataBind();
    }

    private void cargar_DropDownList_ESTADO()
    {
        DropDownList_ESTADO.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_ESTADO_CARGO);

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_ESTADO.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_ESTADO.Items.Add(item);
        }
        DropDownList_ESTADO.DataBind();
    }

    private void cargar_informacion_cargo(DataRow filaCargo)
    { 
        if (filaCargo["ID_EMP"] == DBNull.Value)
        {
            RadioButtonList_TIPO_GARGO.SelectedValue = "GENERICO";
            Panel_EMPRESA.Visible = false;
        }
        else
        {
            if (Convert.ToDecimal(filaCargo["ID_EMP"]) <= 0)
            {
                RadioButtonList_TIPO_GARGO.SelectedValue = "GENERICO";
                Panel_EMPRESA.Visible = false;
            }
            else
            {
                RadioButtonList_TIPO_GARGO.SelectedValue = "CON EMPRESA";

                Panel_EMPRESA.Visible = true;
                cargar_DropDownList_EMPRESA_USUARIA();
                try
                {
                    DropDownList_EMPRESA_USUARIA.SelectedValue = filaCargo["ID_EMP"].ToString();
                }
                catch 
                {
                    DropDownList_EMPRESA_USUARIA.SelectedIndex = 0;
                }
            }
        }

        cargo _cargo = new cargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoGrupoPrimario = _cargo.ObtenerTodoPorIdGrupoPrimario(filaCargo["COD_OCUPACION"].ToString());

        if (tablaInfoGrupoPrimario.Rows.Count > 0)
        {
            cargar_DropDownList_GRUPOS_PRIMARIOS(tablaInfoGrupoPrimario.Rows[0]);
            DropDownList_GRUPOS_PRIMARIOS.SelectedValue = tablaInfoGrupoPrimario.Rows[0]["ID_GRUPOS_PRIMARIOS"].ToString().Trim();
        }
        else
        {
            Cargar_DropDownList_GRUPOS_PRIMARIOS_EnBlanco();
        }

        TextBox_NOMBRE_CARGO_NUEVO.Text = filaCargo["NOM_OCUPACION"].ToString().Trim();
        
        TextBox_FUNCIONES_CARGO.Text = filaCargo["DSC_FUNCIONES"].ToString().Trim();

        TextBox_OBLIGACIONES.Text = filaCargo["OBLIGACIONES"].ToString().Trim();
        TextBox_RESPONSABILIDADES.Text = filaCargo["RESPONSABILIDADES"].ToString().Trim();

        if ((filaCargo["COMISIONA"] == DBNull.Value) || (filaCargo["COMISIONA"].ToString().ToUpper() == "N"))
        {
            CheckBox_COMISIONA.Checked = false;
        }
        else 
        {
            CheckBox_COMISIONA.Checked = true;
        }

        cargar_DropDownList_ESTADO();
        try
        {
            DropDownList_ESTADO.SelectedValue = filaCargo["ACTIVO"].ToString().Trim();
        }
        catch
        {
            DropDownList_ESTADO.SelectedIndex = -1;
        }
    }

    private void Cargar(Decimal ID_OCUPACION)
    {
        HiddenField_ID_OCUPACION.Value = ID_OCUPACION.ToString();

        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);

        cargo _cargo = new cargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaCargo = _cargo.ObtenerOcupacionPorIdOcupacion(ID_OCUPACION);

        if (tablaCargo.Rows.Count <= 0)
        {
            Mostrar(Acciones.Inicio);
            HiddenField_ID_OCUPACION.Value = "";

            if (_cargo.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _cargo.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información para  la busqueda especifica.", Proceso.Advertencia);
            }
        }
        else
        {
            Mostrar(Acciones.CargarCargo);

            DataRow filaCargo = tablaCargo.Rows[0];

            cargar_informacion_control_registro(filaCargo);

            cargar_informacion_cargo(filaCargo);
        }

    }

    protected void GridView_RESULTADOS_BUSQUEDA_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal ID_OCUPACION = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_OCUPACION"]);

            Cargar(ID_OCUPACION);
        }
    }
    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;

        if (HiddenField_TIPO_BUSQUEDA_ACTUAL.Value == "CON_FILTRO")
        {
            Buscar();
        }
    }
    protected void RadioButtonList_TIPO_GARGO_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList_TIPO_GARGO.SelectedValue == "GENERICO")
        {
            Panel_EMPRESA.Visible = false;
            DropDownList_EMPRESA_USUARIA.Items.Clear();
        }
        else 
        {
            Panel_EMPRESA.Visible = true;
            cargar_DropDownList_EMPRESA_USUARIA();
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

    private void cargar_DropDownList_GRUPOS_PRIMARIOS_despues_de_busquda(DataTable tablaInformacion)
    {
        DropDownList_GRUPOS_PRIMARIOS.Items.Clear();

        DropDownList_GRUPOS_PRIMARIOS.Items.Add(new ListItem("Seleccione Cargo...", ""));

        foreach (DataRow filaInformacion in tablaInformacion.Rows)
        {
            DropDownList_GRUPOS_PRIMARIOS.Items.Add(new ListItem(filaInformacion["DESCRIPCION"].ToString().Trim(), filaInformacion["ID_GRUPOS_PRIMARIOS"].ToString().Trim()));
        }

        DropDownList_GRUPOS_PRIMARIOS.DataBind();
    }

    protected void Button_BUSCADOR_CARGO_Click(object sender, EventArgs e)
    {
        cargo _cargo = new cargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCargosDaneEncontrados = _cargo.ObtenerGruposPrimariosPorDescripcion(TextBox_BUSCADOR_CARGO.Text.Trim());

        if (tablaCargosDaneEncontrados.Rows.Count <= 0)
        {
            if (_cargo.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _cargo.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron datos para la busqueda realizada.", Proceso.Advertencia);
            }

            Cargar_DropDownList_GRUPOS_PRIMARIOS_EnBlanco();
        }
        else
        {
            cargar_DropDownList_GRUPOS_PRIMARIOS_despues_de_busquda(tablaCargosDaneEncontrados);        
        }
    }
}
