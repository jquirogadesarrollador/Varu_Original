using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Brainsbits.LLB.maestras;
using Brainsbits.LLB.comercial;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB;

using TSHAK.Components;

public partial class _Default : System.Web.UI.Page
{
    private String NOMBRE_AREA = tabla.NOMBRE_AREA_GESTION_COMERCIAL;

    private enum Acciones
    {
        Inicio = 0,
        BusquedaEncontrada = 1,
        CargarEmpresa = 2,
        NuevaEmpresa = 3,
        ModificarEmpresa = 4
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

    private void Activar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.NuevaEmpresa:
                DropDownList_ACTIVO.Enabled = true;

                TextBox_FCH_INGRESO.Enabled = true;
                TextBox_NIT_EMPRESA.Enabled = true;
                TextBox_DIG_VER.Enabled = true;
                TextBox_RAZ_SOCIAL.Enabled = true;
                TextBox_DIR_EMP.Enabled = true;

                DropDownList_REGIONAL.Enabled = true;

                TextBox_TEL_EMP.Enabled = true;
                TextBox_TEL_EMP_1.Enabled = true;
                TextBox_CEL_EMP.Enabled = true;

                DropDownList_NOMBRE_SECCION.Enabled = true;
                TextBox_DES_ACTIVIDAD.Enabled = true;

                DropDownList_REGIONAL_COVERTURA.Enabled = true;
                
                DropDownList_GRUPO_EMPRESARIAL.Enabled = true;
                DropDownList_ALIANZA.Enabled = true;

                DropDownList_EMP_ESTADO.Enabled = true;
                DropDownList_EMP_EXC_IVA.Enabled = true;
                DropDownList_FAC_NAL.Enabled = true;
                DropDownList_TIPO_EMPRESA.Enabled = true;
                TextBox_NUM_EMPLEADOS.Enabled = true;

                DropDownList_DEPARTAMENTO_ORIGINO.Enabled = true;

                TextBox_NOM_REP_LEGAL.Enabled = true;
                DropDownList_TIP_CEDULA_REP_LEGAL_CLIENTE.Enabled = true;

                TextBox_CC_REP_LEGAL.Enabled = true;
                break;
            case Acciones.ModificarEmpresa:
                DropDownList_ACTIVO.Enabled = true;

                TextBox_FCH_INGRESO.Enabled = true;
                TextBox_NIT_EMPRESA.Enabled = true;
                TextBox_DIG_VER.Enabled = true;
                TextBox_RAZ_SOCIAL.Enabled = true;
                TextBox_DIR_EMP.Enabled = true;

                DropDownList_REGIONAL.Enabled = true;
                if (DropDownList_REGIONAL.SelectedIndex <= 0)
                {
                    inhabilitar_DropDownList_DEPARTAMENTO();
                    inhabilitar_DropDownList_CIUDAD();
                }
                else
                {
                    DropDownList_DEPARTAMENTO.Enabled = true;
                    DropDownList_CIUDAD.Enabled = true;
                }

                TextBox_TEL_EMP.Enabled = true;
                TextBox_TEL_EMP_1.Enabled = true;
                TextBox_CEL_EMP.Enabled = true;

                DropDownList_NOMBRE_SECCION.Enabled = true;
                if (DropDownList_NOMBRE_SECCION.SelectedIndex <= 0)
                {
                    inhabilitar_DropDownList_DIVISION();
                    inhabilitar_DropDownList_CLASE();
                    inhabilitar_DropDownList_ACTIVIDAD();
                }
                else
                {
                    DropDownList_DIVISION.Enabled = true;
                    DropDownList_CLASE.Enabled = true;
                    DropDownList_ACTIVIDAD.Enabled = true;
                }
                TextBox_DES_ACTIVIDAD.Enabled = true;

                DropDownList_REGIONAL_COVERTURA.Enabled = true;
                if (DropDownList_REGIONAL_COVERTURA.SelectedIndex <= 0)
                {
                    inhabilitar_DropDownList_DEPARTAMNETO_COVERTURA();
                    inhabilitar_DropDownList_CIUDAD_COVERTURA();
                }
                else
                {
                    DropDownList_DEPARTAMNETO_COVERTURA.Enabled = true;
                    DropDownList_CIUDAD_COVERTURA.Enabled = true;
                }

                DropDownList_GRUPO_EMPRESARIAL.Enabled = true;
                DropDownList_ALIANZA.Enabled = true;

                DropDownList_EMP_ESTADO.Enabled = true;
                DropDownList_EMP_EXC_IVA.Enabled = true;
                DropDownList_FAC_NAL.Enabled = true;
                DropDownList_TIPO_EMPRESA.Enabled = true;
                TextBox_NUM_EMPLEADOS.Enabled = true;

                DropDownList_DEPARTAMENTO_ORIGINO.Enabled = true;
                if (DropDownList_DEPARTAMENTO_ORIGINO.SelectedIndex <= 0)
                {
                    inhabilitar_DropDownList_CIUDAD_ORIGINO();
                }
                else
                {
                    DropDownList_CIUDAD_ORIGINO.Enabled = true;
                }

                TextBox_NOM_REP_LEGAL.Enabled = true;
                DropDownList_TIP_CEDULA_REP_LEGAL_CLIENTE.Enabled = true;
                if (DropDownList_TIP_CEDULA_REP_LEGAL_CLIENTE.SelectedValue == "CC")
                {
                    DropDownList_DEP_CC_REP_LEGAL.Enabled = true;
                    DropDownList_CIU_CC_REP_LEGAL.Enabled = true;
                }
                else
                {
                    inhabilitar_DropDownList_DEP_CC_REP_LEGAL();
                    inhabilitar_DropDownList_CIU_CC_REP_LEGAL();
                }
                TextBox_CC_REP_LEGAL.Enabled = true;

                DropDownList_LISTA_RIESGOS.Enabled = true;
                break;
        }
    }

    private void Desactivar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                TextBox_FCH_CRE.Enabled = false;
                TextBox_HOR_CRE.Enabled = false;
                TextBox_USU_CRE.Enabled = false;
                TextBox_FCH_MOD.Enabled = false;
                TextBox_HOR_MOD.Enabled = false;
                TextBox_USU_MOD.Enabled = false;

                TextBox_COD_EMPRESA.Enabled = false;
                DropDownList_ACTIVO.Enabled = false;

                TextBox_FCH_INGRESO.Enabled = false;
                TextBox_NIT_EMPRESA.Enabled = false;
                TextBox_DIG_VER.Enabled = false;
                TextBox_RAZ_SOCIAL.Enabled = false;
                TextBox_DIR_EMP.Enabled = false;

                DropDownList_REGIONAL.Enabled = false;
                DropDownList_DEPARTAMENTO.Enabled = false;
                DropDownList_CIUDAD.Enabled = false;
                TextBox_TEL_EMP.Enabled = false;
                TextBox_TEL_EMP_1.Enabled = false;
                TextBox_CEL_EMP.Enabled = false;

                DropDownList_NOMBRE_SECCION.Enabled = false;
                DropDownList_DIVISION.Enabled = false;
                DropDownList_CLASE.Enabled = false;
                DropDownList_ACTIVIDAD.Enabled = false;
                TextBox_DES_ACTIVIDAD.Enabled = false;

                DropDownList_REGIONAL_COVERTURA.Enabled = false;
                DropDownList_DEPARTAMNETO_COVERTURA.Enabled = false;
                DropDownList_CIUDAD_COVERTURA.Enabled = false;
                DropDownList_GRUPO_EMPRESARIAL.Enabled = false;
                DropDownList_ALIANZA.Enabled = false;

                DropDownList_EMP_ESTADO.Enabled = false;
                DropDownList_EMP_EXC_IVA.Enabled = false;
                DropDownList_FAC_NAL.Enabled = false;
                DropDownList_TIPO_EMPRESA.Enabled = false;
                TextBox_NUM_EMPLEADOS.Enabled = false;

                DropDownList_DEPARTAMENTO_ORIGINO.Enabled = false;
                DropDownList_CIUDAD_ORIGINO.Enabled = false;
                TextBox_NOM_REP_LEGAL.Enabled = false;
                DropDownList_TIP_CEDULA_REP_LEGAL_CLIENTE.Enabled = false;
                TextBox_CC_REP_LEGAL.Enabled = false;
                DropDownList_DEP_CC_REP_LEGAL.Enabled = false;
                DropDownList_CIU_CC_REP_LEGAL.Enabled = false;
                break;
        }
    }

    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = false;
                Panel_FORM_BOTONES.Visible = false;

                Button_NUEVO.Visible = false;
                Button_NUEVO_1.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_CANCELAR_1.Visible = false;

                Panel_BOTONES_INTERNOS.Visible = false;

                Panel_RESULTADOS_GRID.Visible = false;

                Panel_FORMULARIO.Visible = false;

                Panel_CONTROL_REGISTRO.Visible = false;

                Panel_COD_EMPRESA.Visible = false;

                Panel_HISTORIAL_ACT.Visible = false;

                Panel_SELECCIONAR_RIESGO.Visible = false;

                Panel_COBERTURA_DROPS.Visible = false;

                Panel_UNIDAD_NEGOCIO.Visible = false;
                Panel_INFO_SIN_UNIDAD_NEGOCIO.Visible = false;
                Panel_GRILLA_UNIDAD_NEGOCIO.Visible = false;

                Panel_BOTONES_ACCION_1.Visible = false;

                GridView_RIESGOS_CONFIGURADOS.Columns[0].Visible = false;
                GridView_COVERTURA.Columns[0].Visible = false;

                Panel_EMPLEADOS_VIGENTES_EMPRESA.Visible = false;

                break;
            case Acciones.ModificarEmpresa:

                Button_NUEVO.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_NUEVO_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;

                Panel_CONTROL_REGISTRO.Visible = false;
                break;
        }
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:

                Panel_INFO_ADICIONAL_MODULO.Visible = true;

                Panel_FORM_BOTONES.Visible = true;

                Button_NUEVO.Visible = true;
                Button_NUEVO_1.Visible = true;
                break;
            case Acciones.BusquedaEncontrada:

                Panel_INFO_ADICIONAL_MODULO.Visible = true;

                Panel_FORM_BOTONES.Visible = true;

                Button_NUEVO.Visible = true;
                Button_NUEVO_1.Visible = true;

                Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.CargarEmpresa:
                Panel_INFO_ADICIONAL_MODULO.Visible = true;
                
                Panel_FORM_BOTONES.Visible = true;
                Panel_BOTONES_ACCION_1.Visible = true;

                Button_NUEVO.Visible = true;
                Button_NUEVO_1.Visible = true;
                Button_MODIFICAR.Visible = true;
                Button_MODIFICAR_1.Visible = true;

                Panel_BOTONES_INTERNOS.Visible = true;

                Panel_FORMULARIO.Visible = true;
                Panel_CONTROL_REGISTRO.Visible = true;
                Panel_COD_EMPRESA.Visible = true;

                Panel_EMPLEADOS_VIGENTES_EMPRESA.Visible = true;

                Panel_UNIDAD_NEGOCIO.Visible = true;
                Panel_INFO_SIN_UNIDAD_NEGOCIO.Visible = true;
                Panel_GRILLA_UNIDAD_NEGOCIO.Visible = true;
                break;
            case Acciones.NuevaEmpresa:
                Panel_INFO_ADICIONAL_MODULO.Visible = true;

                Panel_FORM_BOTONES.Visible = true;
                Panel_BOTONES_ACCION_1.Visible = true;

                Button_GUARDAR.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR.Visible = true;
                Button_CANCELAR_1.Visible = true;

                Panel_FORMULARIO.Visible = true;

                Panel_SELECCIONAR_RIESGO.Visible = true;
                Panel_COBERTURA_DROPS.Visible = true;

                GridView_RIESGOS_CONFIGURADOS.Columns[0].Visible = true;
                GridView_COVERTURA.Columns[0].Visible = true;
                break;
            case Acciones.ModificarEmpresa:
                
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;

                Panel_SELECCIONAR_RIESGO.Visible = true;
                Panel_COBERTURA_DROPS.Visible = true;

                GridView_RIESGOS_CONFIGURADOS.Columns[0].Visible = true;
                GridView_COVERTURA.Columns[0].Visible = true;
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

        item = new ListItem("Razón social", "RAZ_SOCIAL");
        DropDownList_BUSCAR.Items.Add(item);

        item = new ListItem("Grupo Empresarial", "GRUPO_EMPRESARIAL");
        DropDownList_BUSCAR.Items.Add(item);

        item = new ListItem("NIT", "NIT_EMPRESA");
        DropDownList_BUSCAR.Items.Add(item);

        item = new ListItem("Código de Cliente", "COD_EMPRESA");
        DropDownList_BUSCAR.Items.Add(item);

        item = new ListItem("Regional", "REGIONAL");
        DropDownList_BUSCAR.Items.Add(item);

        item = new ListItem("Ciudad", "CIUDAD");
        DropDownList_BUSCAR.Items.Add(item);

        item = new ListItem("Comercial", "COMERCIAL");
        DropDownList_BUSCAR.Items.Add(item);

        DropDownList_BUSCAR.DataBind();
    }

    private void iniciar_seccion_de_busqueda()
    {
        TextBox_BUSCAR.Text = "";
        cargar_DropDownList_BUSCAR();
    }

    private void cargar_DropDownList_REGIONAL_COVERTURA()
    {
        DropDownList_REGIONAL_COVERTURA.Items.Clear();

        regional _regional = new regional(Session["idEmpresa"].ToString());
        DataTable tablaRegionales = _regional.ObtenerTodasLasRegionales();

        ListItem item = new ListItem("Seleccione Regional", "");
        DropDownList_REGIONAL_COVERTURA.Items.Add(item);

        foreach (DataRow fila in tablaRegionales.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_REGIONAL"].ToString());
            DropDownList_REGIONAL_COVERTURA.Items.Add(item);
        }

        DropDownList_REGIONAL_COVERTURA.DataBind();
    }

    private void cargar_DropDownList_LISTA_RIESGOS()
    {
        DropDownList_LISTA_RIESGOS.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());
        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_TABLA_RIESGOS);

        ListItem item = new ListItem("Seleccione", "");
        DropDownList_LISTA_RIESGOS.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString() + " - " + fila["CODIGO"].ToString() + "%", fila["DESCRIPCION"].ToString());
            DropDownList_LISTA_RIESGOS.Items.Add(item);
        }

        DropDownList_LISTA_RIESGOS.DataBind();
    }

    private void cargar_DropDownList_DEPARTAMENTO_COVERTURA(String id)
    {
        DropDownList_DEPARTAMNETO_COVERTURA.Items.Clear();

        departamento _departamento = new departamento(Session["idEmpresa"].ToString());
        DataTable tablaDepartamentos = _departamento.ObtenerDepartamentosPorIdRegional(id);

        ListItem item = new ListItem("Seleccione Departamento", "");
        DropDownList_DEPARTAMNETO_COVERTURA.Items.Add(item);

        foreach (DataRow fila in tablaDepartamentos.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_DEPARTAMENTO"].ToString());
            DropDownList_DEPARTAMNETO_COVERTURA.Items.Add(item);
        }

        DropDownList_DEPARTAMNETO_COVERTURA.DataBind();
    }

    private void cargar_DropDownList_CIUDAD_COVERTURA(String idRegional, String idDepartamento)
    {
        DropDownList_CIUDAD_COVERTURA.Items.Clear();

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
        DataTable tablaCiudades = _ciudad.ObtenerCiudadesPorIdDepartamentoEIdRegional(idRegional, idDepartamento);

        ListItem item = new ListItem("Seleccione Ciudad", "");
        DropDownList_CIUDAD_COVERTURA.Items.Add(item);

        foreach (DataRow fila in tablaCiudades.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_CIUDAD"].ToString());
            DropDownList_CIUDAD_COVERTURA.Items.Add(item);
        }

        DropDownList_CIUDAD_COVERTURA.DataBind();
    }

    private void inhabilitar_DropDownList_DEPARTAMNETO_COVERTURA()
    {
        DropDownList_DEPARTAMNETO_COVERTURA.Enabled = false;
        DropDownList_DEPARTAMNETO_COVERTURA.Items.Clear();
        ListItem item = new ListItem("Seleccione Departamento", "");
        DropDownList_DEPARTAMNETO_COVERTURA.Items.Add(item);
        DropDownList_DEPARTAMNETO_COVERTURA.DataBind();
    }

    private void inhabilitar_DropDownList_CIUDAD_COVERTURA()
    {
        DropDownList_CIUDAD_COVERTURA.Enabled = false;
        DropDownList_CIUDAD_COVERTURA.Items.Clear();
        ListItem item = new ListItem("Seleccione Ciudad", "");
        DropDownList_CIUDAD_COVERTURA.Items.Add(item);
        DropDownList_CIUDAD_COVERTURA.DataBind();
    }

    private void inhabilitar_DropDownList_CIUDAD_ORIGINO()
    {
        DropDownList_CIUDAD_ORIGINO.Enabled = false;
        DropDownList_CIUDAD_ORIGINO.Items.Clear();
        ListItem item = new ListItem("Seleccione", "");
        DropDownList_CIUDAD_ORIGINO.Items.Add(item);
        DropDownList_CIUDAD_ORIGINO.DataBind();
    }

    private void iniciarControlesParaNuevoCliente()
    {
        TextBox_FCH_INGRESO.Text = "";
        TextBox_NIT_EMPRESA.Text = "";
        TextBox_DIG_VER.Text = "";
        TextBox_RAZ_SOCIAL.Text = "";
        TextBox_DIR_EMP.Text = "";

        cargar_DropDownList_REGIONAL();
        inhabilitar_DropDownList_DEPARTAMENTO();
        inhabilitar_DropDownList_CIUDAD();

        TextBox_TEL_EMP.Text = "";
        TextBox_TEL_EMP_1.Text = "";
        TextBox_CEL_EMP.Text = "";

        TextBox_DES_ACTIVIDAD.Text = "";
        cargar_DropDownList_NOMBRE_SECCION();
        inhabilitar_DropDownList_DIVISION();
        inhabilitar_DropDownList_CLASE();
        inhabilitar_DropDownList_ACTIVIDAD();

        cargar_DropDownList_GRUPO_EMPRESARIAL();

        cargar_DropDownList_ALIANZA();

        Session.Remove("dt_GRID_COVERTURA");
        Session.Remove("dt_GRID_RIESGOS");

        cargar_DropDownList_REGIONAL_COVERTURA();
        inhabilitar_DropDownList_DEPARTAMNETO_COVERTURA();
        inhabilitar_DropDownList_CIUDAD_COVERTURA();

        cargar_DropDownList_LISTA_RIESGOS();

        cargar_DropDownList_DEPARTAMENTO_ORIGINO();
        inhabilitar_DropDownList_CIUDAD_ORIGINO();

        TextBox_NOM_REP_LEGAL.Text = "";
        cargamos_DropDownList_TIP_CEDULA_REP_LEGAL_CLIENTE();
        RequiredFieldValidator_DropDownList_CIU_CC_REP_LEGAL.Enabled = false;
        TextBox_CC_REP_LEGAL.Text = "";
        inhabilitar_DropDownList_DEP_CC_REP_LEGAL();
        inhabilitar_DropDownList_CIU_CC_REP_LEGAL();

        TextBox_NUM_EMPLEADOS.Text = "";
        cargar_DropDownList_EMP_ESTADO();
        cargar_DropDownList_EMP_EXC_IVA();
        cargar_DropDownList_FAC_NAL();
        cargar_DropDownList_TIPO_EMPRESA();

        GridView_COVERTURA.DataSource = null;
        GridView_COVERTURA.DataBind();
        GridView_RIESGOS_CONFIGURADOS.DataSource = null;
        GridView_RIESGOS_CONFIGURADOS.DataBind();
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                
                Label_INFO_ADICIONAL_MODULO.Text = "BUSQUEDA Y CREACIÓN DE CLIENTES";

                configurarCaracteresAceptadosBusqueda(true, true);

                iniciar_seccion_de_busqueda();

                Session.Remove("dt_GRID_RIESGOS");
                Session.Remove("dt_GRID_COVERTURA");

                Session.Add("dt_GRID_RIESGOS", new DataTable());
                Session.Add("dt_GRID_COVERTURA", new DataTable());
                break;
            case Acciones.NuevaEmpresa:
                this.Title = "NUEVO CLIENTE";
                Label_INFO_ADICIONAL_MODULO.Text = "NUEVO CLIENTE";

                HiddenField_ID_EMPRESA.Value = "";
                HiddenField_ESTADO_EMPRESA.Value = "";

                iniciarControlesParaNuevoCliente();
                break;
            case Acciones.ModificarEmpresa:
                cargar_DropDownList_REGIONAL_COVERTURA();
                inhabilitar_DropDownList_DEPARTAMNETO_COVERTURA();
                inhabilitar_DropDownList_CIUDAD_COVERTURA();

                cargar_DropDownList_LISTA_RIESGOS();
                break;
        }
    }

    private void Iniciar()
    {
        this.Title = "Clientes";

        configurar_paneles_popup(Panel_FONDO_MENSAJE, Panel_MENSAJES);

        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
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

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            Iniciar();
        }
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        RolPermisos();
    }

    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    protected void Button_NUEVO_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Activar(Acciones.NuevaEmpresa);
        Mostrar(Acciones.NuevaEmpresa);
        Cargar(Acciones.NuevaEmpresa);
    }

    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.ModificarEmpresa);
        Mostrar(Acciones.ModificarEmpresa);
        Cargar(Acciones.ModificarEmpresa);
        Activar(Acciones.ModificarEmpresa);
    }


    private void Guardar()
    {
        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        tools _tools = new tools();

        decimal ID_EMPRESA = 0;

        String ACTIVO = DropDownList_ACTIVO.SelectedValue.ToString();
        DateTime FCH_INGRESO = DateTime.Parse(TextBox_FCH_INGRESO.Text.Trim());
        String NIT_EMPRESA = TextBox_NIT_EMPRESA.Text.Trim();

        String ACT_ECO = null;
        if (String.IsNullOrEmpty(TextBox_DES_ACTIVIDAD.Text) == false)
        {
            ACT_ECO = _tools.RemplazarCaracteresEnString(TextBox_DES_ACTIVIDAD.Text.ToUpper().Trim());
        }

        String RAZ_SOCIAL = _tools.RemplazarCaracteresEnString(TextBox_RAZ_SOCIAL.Text.ToUpper().Trim());
        String DIR_EMP = _tools.RemplazarCaracteresEnString(TextBox_DIR_EMP.Text.ToUpper().Trim());
        String CIU_EMP = DropDownList_CIUDAD.SelectedValue.ToString();
        String TEL_EMP = _tools.RemplazarCaracteresEnString(TextBox_TEL_EMP.Text.ToUpper().Trim());

        List<cobertura> listaCiudadesSeleccionadas = new List<cobertura>();
        cobertura _coberturaParaLista;
        foreach (GridViewRow fila in GridView_COVERTURA.Rows)
        {
            _coberturaParaLista = new cobertura(Session["idEmpresa"].ToString());
            _coberturaParaLista.IDCIUDAD = fila.Cells[1].Text.Trim();
            listaCiudadesSeleccionadas.Add(_coberturaParaLista);
        }

        List<empresasRiesgos> listaRiesgosEmpresa = new List<empresasRiesgos>();
        empresasRiesgos _empresasRiesgosParaLista;
        foreach (DataRow fila in ((DataTable)Session["dt_GRID_RIESGOS"]).Rows)
        {
            _empresasRiesgosParaLista = new empresasRiesgos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            _empresasRiesgosParaLista.ID_EMPRESA = ID_EMPRESA;
            _empresasRiesgosParaLista.DESCRIPCION_RIESGO = fila["DESCRIPCION_RIESGO"].ToString();
            listaRiesgosEmpresa.Add(_empresasRiesgosParaLista);
        }

        String CUB_CIUDADES = "";
        int contador_filas = 0;
        foreach (cobertura fila in listaCiudadesSeleccionadas)
        {
            if (contador_filas <= 0)
            {
                CUB_CIUDADES = fila.IDCIUDAD;
            }
            else
            {
                CUB_CIUDADES += "," + fila.IDCIUDAD;
            }
            contador_filas += 1;
        }

        String NOM_REP_LEGAL = _tools.RemplazarCaracteresEnString(TextBox_NOM_REP_LEGAL.Text.ToUpper().Trim());
        String TIP_DOC_REP_LEGAL = DropDownList_TIP_CEDULA_REP_LEGAL_CLIENTE.SelectedValue;
        String CC_REP_LEGAL = TextBox_CC_REP_LEGAL.Text.Trim();
        String ID_CIU_CC_REP_LEGAL = null;
        if (DropDownList_TIP_CEDULA_REP_LEGAL_CLIENTE.SelectedValue == "CC")
        {
            ID_CIU_CC_REP_LEGAL = DropDownList_CIU_CC_REP_LEGAL.SelectedValue.ToString();
        }

        String CIU_ORG_NEG = DropDownList_CIUDAD_ORIGINO.SelectedValue.ToString();
        String TIPO_EMPRESA = DropDownList_TIPO_EMPRESA.SelectedValue.ToString();
        int NUM_EMPLEADOS = Convert.ToInt32(TextBox_NUM_EMPLEADOS.Text.Trim());

        String USU_ACTUAL = Session["USU_LOG"].ToString();

        String FAC_NAL = DropDownList_FAC_NAL.SelectedValue.ToString();

        Decimal ID_ALIANZA = 0; 
        if (DropDownList_ALIANZA.SelectedIndex > 0)
        {
            ID_ALIANZA = Convert.ToDecimal(DropDownList_ALIANZA.SelectedValue);
        }

        int DIG_VER = Convert.ToInt32(TextBox_DIG_VER.Text.Trim());
        String EMP_ESTADO = DropDownList_EMP_ESTADO.SelectedValue.ToString();
        String EMP_EXC_IVA = DropDownList_EMP_EXC_IVA.SelectedValue.ToString();

        String TEL_EMP1; 
        if (TextBox_TEL_EMP_1.Text.ToUpper() == "")
        {
            TEL_EMP1 = "Ninguno";
        }
        else
        {
            TEL_EMP1 = _tools.RemplazarCaracteresEnString(TextBox_TEL_EMP_1.Text.ToUpper().Trim());
        }

        String NUM_CELULAR; 
        if (TextBox_CEL_EMP.Text.ToUpper().Trim() == "")
        {
            NUM_CELULAR = "Ninguno";
        }
        else
        {
            NUM_CELULAR = TextBox_CEL_EMP.Text.ToUpper().Trim();
        }

        String ID_ACTIVIDAD = DropDownList_ACTIVIDAD.SelectedValue.ToString();

        Decimal ID_GRUPO_EMPRESARIAL = 0;
        if (DropDownList_GRUPO_EMPRESARIAL.SelectedIndex > 0)
        {
            ID_GRUPO_EMPRESARIAL = Convert.ToDecimal(DropDownList_GRUPO_EMPRESARIAL.SelectedValue);
        }
        
        String ID_CIUDAD_CC_REP_LEGAL = DropDownList_CIU_CC_REP_LEGAL.SelectedValue.ToString();

        String ID_SERVICIO;

        if (Session["idEmpresa"].ToString() == "1")
        {
            ID_SERVICIO = "1";
        }
        else
        {
            ID_SERVICIO = "2";
        }

        ID_EMPRESA = _cliente.Adicionar("S", FCH_INGRESO, NIT_EMPRESA, ACT_ECO, RAZ_SOCIAL, DIR_EMP, CIU_EMP, TEL_EMP, CUB_CIUDADES, NOM_REP_LEGAL, CC_REP_LEGAL, TIPO_EMPRESA, CIU_ORG_NEG, NUM_EMPLEADOS, USU_ACTUAL, FAC_NAL, ID_ALIANZA, DIG_VER, EMP_ESTADO, EMP_EXC_IVA, TEL_EMP1, NUM_CELULAR, ID_ACTIVIDAD, ID_GRUPO_EMPRESARIAL, listaCiudadesSeleccionadas, ID_CIUDAD_CC_REP_LEGAL, ID_SERVICIO, TIP_DOC_REP_LEGAL, listaRiesgosEmpresa);

        if (ID_EMPRESA == 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _cliente.MensajeError, Proceso.Error);
        }
        else
        {
            Cargar(ID_EMPRESA);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El cliente " + RAZ_SOCIAL + " fue creado correctamente y se le asignó el ID " + ID_EMPRESA.ToString(), Proceso.Correcto);
        }
    }

    private void Modificar()
    {


        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        tools _tools = new tools();

        decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        String ACTIVO = DropDownList_ACTIVO.SelectedValue.ToString();
        DateTime FCH_INGRESO = DateTime.Parse(TextBox_FCH_INGRESO.Text.Trim());
        String NIT_EMPRESA = TextBox_NIT_EMPRESA.Text.Trim();

        String ACT_ECO = null;
        if (String.IsNullOrEmpty(TextBox_DES_ACTIVIDAD.Text) == false)
        {
            ACT_ECO = _tools.RemplazarCaracteresEnString(TextBox_DES_ACTIVIDAD.Text.ToUpper().Trim());
        }

        String RAZ_SOCIAL = _tools.RemplazarCaracteresEnString(TextBox_RAZ_SOCIAL.Text.ToUpper().Trim());
        String DIR_EMP = _tools.RemplazarCaracteresEnString(TextBox_DIR_EMP.Text.ToUpper().Trim());
        String CIU_EMP = DropDownList_CIUDAD.SelectedValue.ToString();
        String TEL_EMP = _tools.RemplazarCaracteresEnString(TextBox_TEL_EMP.Text.ToUpper().Trim());

        List<cobertura> listaCiudadesSeleccionadas = new List<cobertura>();
        cobertura _coberturaParaLista;
        foreach (GridViewRow fila in GridView_COVERTURA.Rows)
        {
            _coberturaParaLista = new cobertura(Session["idEmpresa"].ToString());
            _coberturaParaLista.IDCIUDAD = fila.Cells[1].Text.Trim();
            listaCiudadesSeleccionadas.Add(_coberturaParaLista);
        }

        List<empresasRiesgos> listaRiesgosEmpresa = new List<empresasRiesgos>();
        empresasRiesgos _empresasRiesgosParaLista;
        foreach (DataRow fila in ((DataTable)Session["dt_GRID_RIESGOS"]).Rows)
        {
            _empresasRiesgosParaLista = new empresasRiesgos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            _empresasRiesgosParaLista.ID_EMPRESA = ID_EMPRESA;
            _empresasRiesgosParaLista.DESCRIPCION_RIESGO = fila["DESCRIPCION_RIESGO"].ToString();
            listaRiesgosEmpresa.Add(_empresasRiesgosParaLista);
        }

        String CUB_CIUDADES = "";
        int contador_filas = 0;
        foreach (cobertura fila in listaCiudadesSeleccionadas)
        {
            if (contador_filas <= 0)
            {
                CUB_CIUDADES = fila.IDCIUDAD;
            }
            else
            {
                CUB_CIUDADES += "," + fila.IDCIUDAD;
            }
            contador_filas += 1;
        }

        String NOM_REP_LEGAL = _tools.RemplazarCaracteresEnString(TextBox_NOM_REP_LEGAL.Text.ToUpper().Trim());
        String TIP_DOC_REP_LEGAL = DropDownList_TIP_CEDULA_REP_LEGAL_CLIENTE.SelectedValue;
        String CC_REP_LEGAL = TextBox_CC_REP_LEGAL.Text.Trim();
        String ID_CIU_CC_REP_LEGAL = null;
        if (DropDownList_TIP_CEDULA_REP_LEGAL_CLIENTE.SelectedValue == "CC")
        {
            ID_CIU_CC_REP_LEGAL = DropDownList_CIU_CC_REP_LEGAL.SelectedValue.ToString();
        }

        String CIU_ORG_NEG = DropDownList_CIUDAD_ORIGINO.SelectedValue.ToString();
        String TIPO_EMPRESA = DropDownList_TIPO_EMPRESA.SelectedValue.ToString();
        int NUM_EMPLEADOS = Convert.ToInt32(TextBox_NUM_EMPLEADOS.Text.Trim());

        String USU_ACTUAL = Session["USU_LOG"].ToString();

        String FAC_NAL = DropDownList_FAC_NAL.SelectedValue.ToString();


        Decimal ID_ALIANZA = 0; 
        if (DropDownList_ALIANZA.SelectedIndex > 0)
        {
            ID_ALIANZA = Convert.ToDecimal(DropDownList_ALIANZA.SelectedValue);
        }

        int DIG_VER = Convert.ToInt32(TextBox_DIG_VER.Text.Trim());
        String EMP_ESTADO = DropDownList_EMP_ESTADO.SelectedValue.ToString();
        String EMP_EXC_IVA = DropDownList_EMP_EXC_IVA.SelectedValue.ToString();

        String TEL_EMP1; 
        if (TextBox_TEL_EMP_1.Text.ToUpper() == "")
        {
            TEL_EMP1 = "Ninguno";
        }
        else
        {
            TEL_EMP1 = _tools.RemplazarCaracteresEnString(TextBox_TEL_EMP_1.Text.ToUpper().Trim());
        }

        String NUM_CELULAR; 
        if (TextBox_CEL_EMP.Text.ToUpper().Trim() == "")
        {
            NUM_CELULAR = "Ninguno";
        }
        else
        {
            NUM_CELULAR = TextBox_CEL_EMP.Text.ToUpper().Trim();
        }

        String ID_ACTIVIDAD = DropDownList_ACTIVIDAD.SelectedValue.ToString();

        Decimal ID_GRUPO_EMPRESARIAL = 0;
        if(DropDownList_GRUPO_EMPRESARIAL.SelectedIndex > 0)
        {
            ID_GRUPO_EMPRESARIAL = Convert.ToDecimal(DropDownList_GRUPO_EMPRESARIAL.SelectedValue);
        }

        String ID_CIUDAD_CC_REP_LEGAL = DropDownList_CIU_CC_REP_LEGAL.SelectedValue.ToString();

        String ID_SERVICIO = null;

        if (Session["idEmpresa"].ToString() == "1")
        {
            ID_SERVICIO = "1";
        }
        else
        {
            ID_SERVICIO = "2";
        }

        Boolean resultadoActualizacion = true;

        if (HiddenField_ESTADO_EMPRESA.Value != DropDownList_ACTIVO.SelectedValue.ToString())
        {
            historialActivacion _historialActivacion = new historialActivacion(Session["idEmpresa"].ToString());
            _historialActivacion.CLASE_REGISTRO = Label_TIPO_DE_ACTIVACION.Text.ToUpper();
            _historialActivacion.COMENTARIO = TextBox_DESCRIPCION_HISTORIAL_ACT.Text.ToUpper().Trim();

            resultadoActualizacion = _cliente.Actualizar(ID_EMPRESA, ACTIVO, FCH_INGRESO, NIT_EMPRESA, ACT_ECO, RAZ_SOCIAL, DIR_EMP, CIU_EMP, TEL_EMP, CUB_CIUDADES, NOM_REP_LEGAL, CC_REP_LEGAL, TIPO_EMPRESA, CIU_ORG_NEG, NUM_EMPLEADOS, USU_ACTUAL, FAC_NAL, ID_ALIANZA, DIG_VER, EMP_ESTADO, EMP_EXC_IVA, TEL_EMP1, NUM_CELULAR, ID_ACTIVIDAD, ID_GRUPO_EMPRESARIAL, listaCiudadesSeleccionadas, ID_CIUDAD_CC_REP_LEGAL, _historialActivacion, ID_SERVICIO, TIP_DOC_REP_LEGAL, listaRiesgosEmpresa, HiddenField_RAZ_SOCIAL_ANTERIOR.Value);
        }
        else
        {
            resultadoActualizacion = _cliente.Actualizar(ID_EMPRESA, ACTIVO, FCH_INGRESO, NIT_EMPRESA, ACT_ECO, RAZ_SOCIAL, DIR_EMP, CIU_EMP, TEL_EMP, CUB_CIUDADES, NOM_REP_LEGAL, CC_REP_LEGAL, TIPO_EMPRESA, CIU_ORG_NEG, NUM_EMPLEADOS, USU_ACTUAL, FAC_NAL, ID_ALIANZA, DIG_VER, EMP_ESTADO, EMP_EXC_IVA, TEL_EMP1, NUM_CELULAR, ID_ACTIVIDAD, ID_GRUPO_EMPRESARIAL, listaCiudadesSeleccionadas, ID_CIUDAD_CC_REP_LEGAL, ID_SERVICIO, TIP_DOC_REP_LEGAL, listaRiesgosEmpresa, HiddenField_RAZ_SOCIAL_ANTERIOR.Value);
        }

        if (resultadoActualizacion == true)
        {
            maestrasInterfaz _maestrasInterfaz = new maestrasInterfaz();
            _maestrasInterfaz.CargarEnBdElManualServicioActual(ID_EMPRESA);

            Cargar(ID_EMPRESA);
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El cliente " + RAZ_SOCIAL + " fue actualizado correctamente", Proceso.Correcto);
        }
        else
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _cliente.MensajeError, Proceso.Error);
        }
    }   

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        if (GridView_COVERTURA.Rows.Count <= 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Para poder continuar debe especificar por lo menos una ciudad en la sección de COBERTURA DEL CLIENTE.", Proceso.Advertencia);
        }
        else
        {
            if (GridView_RIESGOS_CONFIGURADOS.Rows.Count <= 0)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Para poder continuar debe especificar por lo menos un riesgo en la sección de RIESGOS DEL CLIENTE.", Proceso.Advertencia);
            }
            else
            {
                if (HiddenField_ID_EMPRESA.Value == "")
                {
                    Guardar();
                }
                else
                {
                    Modificar();
                }
            }            
        }
    }
    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
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
                if (DropDownList_BUSCAR.SelectedValue == "GRUPO_EMPRESARIAL")
                {
                    configurarCaracteresAceptadosBusqueda(true, false);
                }
                else
                {
                    if (DropDownList_BUSCAR.SelectedValue == "NIT_EMPRESA")
                    {
                        configurarCaracteresAceptadosBusqueda(false, true);
                    }
                    else
                    {
                        if (DropDownList_BUSCAR.SelectedValue == "COD_EMPRESA")
                        {
                            configurarCaracteresAceptadosBusqueda(false, true);
                        }
                        else
                        {
                            if (DropDownList_BUSCAR.SelectedValue == "REGIONAL")
                            {
                                configurarCaracteresAceptadosBusqueda(true, false);
                            }
                            else
                            {
                                if (DropDownList_BUSCAR.SelectedValue == "CIUDAD")
                                {
                                    configurarCaracteresAceptadosBusqueda(true, false);
                                }
                                else
                                {
                                    if (DropDownList_BUSCAR.SelectedValue == "COMERCIAL")
                                    {
                                        configurarCaracteresAceptadosBusqueda(true, false);
                                    }
                                }   
                            }
                        }
                    }
                }
            } 
        }

        TextBox_BUSCAR.Text = "";
    }

    private void Buscar()
    {
        String datosCapturados = TextBox_BUSCAR.Text.ToUpper();
        String campo = DropDownList_BUSCAR.SelectedValue.ToString();

        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaResultadosBusqueda = new DataTable();

        if (DropDownList_BUSCAR.SelectedValue == "RAZ_SOCIAL")
        {
            tablaResultadosBusqueda = _cliente.ObtenerEmpresaConRazSocialSoloComercial(datosCapturados);
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "GRUPO_EMPRESARIAL")
            {
                tablaResultadosBusqueda = _cliente.ObtenerEmpresaConGrupoEmpresarialSoloComercial(datosCapturados);
            }
            else
            {
                if (DropDownList_BUSCAR.SelectedValue == "NIT_EMPRESA")
                {
                    tablaResultadosBusqueda = _cliente.ObtenerEmpresaConNitEmpresaSoloComercial(datosCapturados);
                }
                else
                {
                    if (DropDownList_BUSCAR.SelectedValue == "COD_EMPRESA")
                    {
                        tablaResultadosBusqueda = _cliente.ObtenerEmpresaConCodEmpresaSoloComercial(datosCapturados);
                    }
                    else
                    {
                        if (DropDownList_BUSCAR.SelectedValue == "REGIONAL")
                        {
                            tablaResultadosBusqueda = _cliente.ObtenerEmpresaConRegionalSoloComercial(datosCapturados);
                        }
                        else
                        {
                            if (DropDownList_BUSCAR.SelectedValue == "CIUDAD")
                            {
                                tablaResultadosBusqueda = _cliente.ObtenerEmpresaConCiudadSoloComercial(datosCapturados);
                            }
                            else
                            {
                                if (DropDownList_BUSCAR.SelectedValue == "COMERCIAL")
                                {
                                    tablaResultadosBusqueda = _cliente.ObtenerEmpresaConComercialSoloComercial(datosCapturados);
                                }
                            }
                        }
                    }
                }
            }
        }

        if (tablaResultadosBusqueda.Rows.Count <= 0)
        {
            if (_cliente.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _cliente.MensajeError, Proceso.Error);
                Label_MENSAJE.Text = _cliente.MensajeError;
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros que cumplieran los datos de busqueda.", Proceso.Advertencia);
            }

            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);

            Label_INFO_ADICIONAL_MODULO.Text = "BUSQUEDA Y CREACIÓON DE CLIENTES";
        }
        else
        {
            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaResultadosBusqueda;
            GridView_RESULTADOS_BUSQUEDA.DataBind();

            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.BusquedaEncontrada);
        }

        this.Title = "BUSQUEDA Y CREACIÓON DE CLIENTES";
    }

    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        Buscar();
    }

    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;

        Buscar();
    }

    private void cargar_menu_interno(Decimal ID_EMPRESA)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
        QueryStringSeguro["img_area"] = "comercial";
        QueryStringSeguro["nombre_area"] = "GESTIÓN COMERCIAL";



        TableRow filaTabla;
        TableCell celdaTabla;
        HyperLink link;
        Image imagen;



        int contadorFilas = 0;



        filaTabla = new TableRow();
        filaTabla.ID = "row_" + contadorFilas.ToString();

        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_1_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_contratos";
        QueryStringSeguro["nombre_modulo"] = "CONTRATOS DE SERVICIO";
        QueryStringSeguro["accion"] = "inicial";
        QueryStringSeguro["reg"] = ID_EMPRESA.ToString();
        link.NavigateUrl = "~/comercial/contratosServicio.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bContratosServicioEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bContratosServicioAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bContratosServicioEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);

        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_2_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_condiciones";
        QueryStringSeguro["nombre_modulo"] = "CONDICIONES ECONÓMICAS";
        if (Session["idEmpresa"].ToString() == "1")
        {
            link.NavigateUrl = "~/comercial/condicionesEconomicasSertempo.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        }
        else
        {
            if (Session["idEmpresa"].ToString() == "3")
            {
                link.NavigateUrl = "~/comercial/condicionesEconomicasEficiencia.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            }
        }
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bCondicionesComercialesEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bCondicionesComercialesAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bCondicionesComercialesEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);




        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_3_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_contactos";
        QueryStringSeguro["nombre_modulo"] = "CONTÁCTOS";
        QueryStringSeguro["proceso"] = "1"; 
        link.NavigateUrl = "~/maestros/contactos.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bContactosComercialesEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bContactosComercialesAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bContactosComercialesEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);

        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_4_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_historial";
        QueryStringSeguro["nombre_modulo"] = "HISTORIAL DE ACTIVACIÓN";
        link.NavigateUrl = "~/comercial/historialActivacion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bHistorialActivacionesEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bHistorialActivacionesAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bHistorialActivacionesEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);






        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_5_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_unidad_negocio";
        QueryStringSeguro["nombre_modulo"] = "UNIDAD DE NEGOCIO";
        QueryStringSeguro["ID_EMPRESA"] = ID_EMPRESA.ToString();
        link.NavigateUrl = "~/comercial/UnidadesNegocio.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bUnidadNegocioEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bUnidadNegocioAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bUnidadNegocioEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);

        Table_MENU.Rows.Add(filaTabla);




        contadorFilas = 1;

        filaTabla = new TableRow();
        filaTabla.ID = "t1_row_" + contadorFilas.ToString();

        celdaTabla = new TableCell();
        celdaTabla.ID = "t1_cell_1_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_manual";
        QueryStringSeguro["nombre_modulo"] = "MANUAL DE SERVICIOS";
        link.NavigateUrl = "~/Operaciones/ManualServicio.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bManualServicioEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bManualServicioAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bManualServicioEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);

        Table_MENU_1.Rows.Add(filaTabla);


        celdaTabla = new TableCell();
        celdaTabla.ID = "t1_cell_2_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_Informacion_basica_comercial";
        QueryStringSeguro["nombre_modulo"] = "Información Basica Comercial";
        link.NavigateUrl = "~/comercial/Informacion_Basica_comercial.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bInformacionBasicaComercial.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bInformacionBasicaComercialAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bInformacionBasicaComercial.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);

        Table_MENU_1.Rows.Add(filaTabla);
    }

    private void cargar_control_registro(DataRow informacionEmpresa)
    {
        TextBox_USU_CRE.Text = informacionEmpresa["USU_CRE"].ToString();
        try
        {
            TextBox_FCH_CRE.Text = DateTime.Parse(informacionEmpresa["FCH_CRE"].ToString()).ToShortDateString();
            TextBox_HOR_CRE.Text = DateTime.Parse(informacionEmpresa["FCH_CRE"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_CRE.Text = "";
            TextBox_HOR_CRE.Text = "";
        }
        TextBox_USU_MOD.Text = informacionEmpresa["USU_MOD"].ToString();
        try
        {
            TextBox_FCH_MOD.Text = DateTime.Parse(informacionEmpresa["FCH_MOD"].ToString()).ToShortDateString();
            TextBox_HOR_MOD.Text = DateTime.Parse(informacionEmpresa["FCH_MOD"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_MOD.Text = "";
            TextBox_HOR_MOD.Text = "";
        }
    }

    private void cargar_DropDownList_ACTIVO()
    {
        DropDownList_ACTIVO.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_ESTADO_EMPRESA);

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_ACTIVO.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_ACTIVO.Items.Add(item);
        }
        DropDownList_ACTIVO.DataBind();
    }

    private DataRow obtenerDatosCiudadCliente(String idCiudad)
    {
        DataRow resultado = null;

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());

        DataTable tablaEmpresa = _ciudad.ObtenerCiudadPorIdCiudad(idCiudad);

        if (tablaEmpresa.Rows.Count > 0)
        {
            resultado = tablaEmpresa.Rows[0];
        }

        return resultado;
    }

    private void cargar_DropDownList_REGIONAL()
    {
        DropDownList_REGIONAL.Items.Clear();

        regional _regional = new regional(Session["idEmpresa"].ToString());
        DataTable tablaRegionales = _regional.ObtenerTodasLasRegionales();

        ListItem item = new ListItem("Seleccione Regional", "");
        DropDownList_REGIONAL.Items.Add(item);

        foreach (DataRow fila in tablaRegionales.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_REGIONAL"].ToString());
            DropDownList_REGIONAL.Items.Add(item);
        }

        DropDownList_REGIONAL.DataBind();
    }

    private void cargar_DropDownList_DEPARTAMENTO(String id)
    {
        DropDownList_DEPARTAMENTO.Items.Clear();

        departamento _departamento = new departamento(Session["idEmpresa"].ToString());
        DataTable tablaDepartamentos = _departamento.ObtenerDepartamentosPorIdRegional(id);

        ListItem item = new ListItem("Seleccione Departamento", "");
        DropDownList_DEPARTAMENTO.Items.Add(item);

        foreach (DataRow fila in tablaDepartamentos.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_DEPARTAMENTO"].ToString());
            DropDownList_DEPARTAMENTO.Items.Add(item);
        }

        DropDownList_DEPARTAMENTO.DataBind();
    }

    private void cargar_DropDownList_CIUDAD(String idRegional, String idDepartamento)
    {
        DropDownList_CIUDAD.Items.Clear();

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
        DataTable tablaCiudades = _ciudad.ObtenerCiudadesPorIdDepartamentoEIdRegional(idRegional, idDepartamento);

        ListItem item = new ListItem("Seleccione Ciudad", "");
        DropDownList_CIUDAD.Items.Add(item);

        foreach (DataRow fila in tablaCiudades.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_CIUDAD"].ToString());
            DropDownList_CIUDAD.Items.Add(item);
        }

        DropDownList_CIUDAD.DataBind();
    }

    private void inhabilitar_DropDownList_DEPARTAMENTO()
    {
        DropDownList_DEPARTAMENTO.Enabled = false;
        DropDownList_DEPARTAMENTO.Items.Clear();
        ListItem item = new ListItem("Seleccione Departamento", "");
        DropDownList_DEPARTAMENTO.Items.Add(item);
        DropDownList_DEPARTAMENTO.DataBind();
    }

    private void inhabilitar_DropDownList_CIUDAD()
    {
        DropDownList_CIUDAD.Enabled = false;
        DropDownList_CIUDAD.Items.Clear();
        ListItem item = new ListItem("Seleccione Ciudad", "");
        DropDownList_CIUDAD.Items.Add(item);
        DropDownList_CIUDAD.DataBind();
    }

    private DataRow obtenerDatosActividadCliente(String idActividad)
    {
        DataRow resultado = null;

        actividad _actividad = new actividad(Session["idEmpresa"].ToString());

        DataTable tablaActividad = _actividad.ObtenerActividPorIdActividad(idActividad);

        if (tablaActividad.Rows.Count > 0)
        {
            resultado = tablaActividad.Rows[0];
        }

        return resultado;
    }

    private void cargar_DropDownList_NOMBRE_SECCION()
    {
        DropDownList_NOMBRE_SECCION.Items.Clear();

        seccion _seccion = new seccion(Session["idEmpresa"].ToString());
        DataTable tablaSecicones = _seccion.ObtenerTodasLasSecciones();

        ListItem item = new ListItem("Seleccione Sección", "");
        DropDownList_NOMBRE_SECCION.Items.Add(item);

        foreach (DataRow fila in tablaSecicones.Rows)
        {
            item = new ListItem(fila["ID_SECCION"].ToString() + " - " + fila["NOMBRE"].ToString(), fila["ID_SECCION"].ToString());
            DropDownList_NOMBRE_SECCION.Items.Add(item);
        }

        DropDownList_NOMBRE_SECCION.DataBind();
    }

    private void cargar_DropDownList_DIVISION(String idSeccion)
    {
        DropDownList_DIVISION.Items.Clear();

        division _division = new division(Session["idEmpresa"].ToString());
        DataTable tablaDivisiones = _division.ObtenerDivisionesPorIdSeccion(idSeccion);

        ListItem item = new ListItem("Seleccione División", "");
        DropDownList_DIVISION.Items.Add(item);

        foreach (DataRow fila in tablaDivisiones.Rows)
        {
            item = new ListItem(fila["ID_DIVISION"].ToString() + " - " + fila["NOMBRE"].ToString(), fila["ID_DIVISION"].ToString());
            DropDownList_DIVISION.Items.Add(item);
        }

        DropDownList_DIVISION.DataBind();
    }

    private void cargar_DropDownList_CLASE(String idDivision)
    {
        DropDownList_CLASE.Items.Clear();

        clase _clase = new clase(Session["idEmpresa"].ToString());
        DataTable tablaClases = _clase.ObtenerClasesPorIdDivision(idDivision);

        ListItem item = new ListItem("Seleccione Clase", "");
        DropDownList_CLASE.Items.Add(item);

        foreach (DataRow fila in tablaClases.Rows)
        {
            item = new ListItem(fila["ID_CLASE"].ToString() + " - " + fila["NOMBRE"].ToString(), fila["ID_CLASE"].ToString());
            DropDownList_CLASE.Items.Add(item);
        }

        DropDownList_CLASE.DataBind();
    }

    private void cargar_DropDownList_ACTIVIDAD(String idClase)
    {
        DropDownList_ACTIVIDAD.Items.Clear();

        actividad _actividad = new actividad(Session["idEmpresa"].ToString());
        DataTable tablaActividades = _actividad.ObtenerActividadesPorIdClase(idClase);

        ListItem item = new ListItem("Seleccione Actividad", "");
        DropDownList_ACTIVIDAD.Items.Add(item);

        foreach (DataRow fila in tablaActividades.Rows)
        {
            item = new ListItem(fila["ID_ACTIVIDAD"].ToString() + " - " + fila["NOMBRE"].ToString(), fila["ID_ACTIVIDAD"].ToString());
            DropDownList_ACTIVIDAD.Items.Add(item);
        }

        DropDownList_ACTIVIDAD.DataBind();
    }

    private void inhabilitar_DropDownList_DIVISION()
    {
        DropDownList_DIVISION.Enabled = false;
        DropDownList_DIVISION.Items.Clear();
        ListItem item = new ListItem("Seleccione División", "");
        DropDownList_DIVISION.Items.Add(item);
        DropDownList_DIVISION.DataBind();
    }
    private void inhabilitar_DropDownList_CLASE()
    {
        DropDownList_CLASE.Enabled = false;
        DropDownList_CLASE.Items.Clear();
        ListItem item = new ListItem("Seleccione Clase", "");
        DropDownList_CLASE.Items.Add(item);
        DropDownList_CLASE.DataBind();
    }
    private void inhabilitar_DropDownList_ACTIVIDAD()
    {
        DropDownList_ACTIVIDAD.Enabled = false;
        DropDownList_ACTIVIDAD.Items.Clear();
        ListItem item = new ListItem("Seleccione Actividad", "");
        DropDownList_ACTIVIDAD.Items.Add(item);
        DropDownList_ACTIVIDAD.DataBind();
    }

    private void cargar_DropDownList_GRUPO_EMPRESARIAL()
    {
        DropDownList_GRUPO_EMPRESARIAL.Items.Clear();

        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaGruposEmpresariales = _cliente.ObtenerTodosLosGruposEmpresariales();

        ListItem item = new ListItem("Seleccione...", "0");
        DropDownList_GRUPO_EMPRESARIAL.Items.Add(item);

        foreach (DataRow fila in tablaGruposEmpresariales.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_GRUPOEMPRESARIAL"].ToString());
            DropDownList_GRUPO_EMPRESARIAL.Items.Add(item);
        }

        DropDownList_GRUPO_EMPRESARIAL.DataBind();
    }

    private void cargar_DropDownList_ALIANZA()
    {
        DropDownList_ALIANZA.Items.Clear();

        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaEmpresasActivas = _cliente.ObtenerEmpresasAlianza();

        ListItem item = new ListItem("Ninguno", "0");
        DropDownList_ALIANZA.Items.Add(item);

        foreach (DataRow fila in tablaEmpresasActivas.Rows)
        {
            item = new ListItem(fila["RAZ_SOCIAL"].ToString(), fila["ID_EMPRESA"].ToString());
            DropDownList_ALIANZA.Items.Add(item);
        }

        DropDownList_ALIANZA.DataBind();
    }

    private void cargar_DropDownList_EMP_ESTADO()
    {
        DropDownList_EMP_ESTADO.Items.Clear();

        ListItem item = new ListItem("?", "0");
        DropDownList_EMP_ESTADO.Items.Add(item);
        item = new ListItem("SI", "S");
        DropDownList_EMP_ESTADO.Items.Add(item);
        item = new ListItem("NO", "N");
        DropDownList_EMP_ESTADO.Items.Add(item);
        DropDownList_EMP_ESTADO.SelectedIndex = 0;
        DropDownList_EMP_ESTADO.DataBind();
    }

    private void cargar_DropDownList_EMP_EXC_IVA()
    {
        DropDownList_EMP_EXC_IVA.Items.Clear();

        ListItem item = new ListItem("?", "0");
        DropDownList_EMP_EXC_IVA.Items.Add(item);
        item = new ListItem("SI", "S");
        DropDownList_EMP_EXC_IVA.Items.Add(item);
        item = new ListItem("NO", "N");
        DropDownList_EMP_EXC_IVA.Items.Add(item);
        DropDownList_EMP_EXC_IVA.SelectedIndex = 0;
        DropDownList_EMP_EXC_IVA.DataBind();
    }

    private void cargar_DropDownList_FAC_NAL()
    {
        DropDownList_FAC_NAL.Items.Clear();

        ListItem item = new ListItem("?", "0");
        DropDownList_FAC_NAL.Items.Add(item);
        item = new ListItem("SI", "S");
        DropDownList_FAC_NAL.Items.Add(item);
        item = new ListItem("NO", "N");
        DropDownList_FAC_NAL.Items.Add(item);
        DropDownList_FAC_NAL.SelectedIndex = 0;
        DropDownList_FAC_NAL.DataBind();
    }

    private void cargar_DropDownList_TIPO_EMPRESA()
    {
        DropDownList_TIPO_EMPRESA.Items.Clear();

        ListItem item = new ListItem("?", "0");
        DropDownList_TIPO_EMPRESA.Items.Add(item);

        if (Session["idEmpresa"].ToString() == "1")
        {
            Label_TIPO_EMPRESA.Text = "Tipo empresa";

            item = new ListItem("BAJO", "BA");
            DropDownList_TIPO_EMPRESA.Items.Add(item);

            item = new ListItem("MEDIO", "ME");
            DropDownList_TIPO_EMPRESA.Items.Add(item);

            item = new ListItem("ALTO", "AL");
            DropDownList_TIPO_EMPRESA.Items.Add(item);
        }
        else
        {
            Label_TIPO_EMPRESA.Text = "Línea empresa";

            item = new ListItem("Aseo", "1");
            DropDownList_TIPO_EMPRESA.Items.Add(item);

            item = new ListItem("Comercial", "2");
            DropDownList_TIPO_EMPRESA.Items.Add(item);

            item = new ListItem("Téc. y admin", "3");
            DropDownList_TIPO_EMPRESA.Items.Add(item);
        }

        DropDownList_TIPO_EMPRESA.DataBind();
    }

    private void llenarGridCobertura(Decimal idEmpresa)
    {
        cobertura _cobertura = new cobertura(Session["idEmpresa"].ToString());

        DataTable tablaCobertura = _cobertura.obtenerCoberturaDeUnCliente(idEmpresa);

        if (tablaCobertura.Rows.Count > 0)
        {
            GridView_COVERTURA.DataSource = tablaCobertura;
            GridView_COVERTURA.DataBind();
        }

        DataTable tabla_temp = new DataTable();
        tabla_temp.Columns.Add("Código Ciudad");
        tabla_temp.Columns.Add("Regional");
        tabla_temp.Columns.Add("Departamento");
        tabla_temp.Columns.Add("Ciudad");

        DataRow fila_temp;
        foreach (DataRow filaOriginal in tablaCobertura.Rows)
        {
            fila_temp = tabla_temp.NewRow();

            fila_temp["Código Ciudad"] = filaOriginal["Código Ciudad"].ToString();
            fila_temp["Regional"] = filaOriginal["Regional"].ToString();
            fila_temp["Departamento"] = filaOriginal["Departamento"].ToString();
            fila_temp["Ciudad"] = filaOriginal["Ciudad"].ToString();
            tabla_temp.Rows.Add(fila_temp);
        }

        Session["dt_GRID_COVERTURA"] = tabla_temp;
    }

    private void llenarGridRiesgos(Decimal ID_EMPRESA)
    {
        empresasRiesgos _empresasRiesgos = new empresasRiesgos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaDatos = _empresasRiesgos.ObtenerRoesgosPorEmpresa(ID_EMPRESA);

        if (tablaDatos.Rows.Count > 0)
        {
            GridView_RIESGOS_CONFIGURADOS.DataSource = tablaDatos;
            GridView_RIESGOS_CONFIGURADOS.DataBind();
        }
        else
        {
            GridView_RIESGOS_CONFIGURADOS.DataSource = null;
            GridView_RIESGOS_CONFIGURADOS.DataBind();
        }

        DataTable tabla_temp = new DataTable();
        tabla_temp.Columns.Add("ACTIVO");
        tabla_temp.Columns.Add("DESCRIPCION_RIESGO");
        tabla_temp.Columns.Add("ID_EMPRESA");
        tabla_temp.Columns.Add("CODIGO");

        DataRow fila_temp;
        foreach (DataRow filaOriginal in tablaDatos.Rows)
        {
            fila_temp = tabla_temp.NewRow();

            fila_temp["ACTIVO"] = filaOriginal["ACTIVO"].ToString();
            fila_temp["DESCRIPCION_RIESGO"] = filaOriginal["DESCRIPCION_RIESGO"].ToString();
            fila_temp["ID_EMPRESA"] = filaOriginal["ID_EMPRESA"].ToString();
            fila_temp["CODIGO"] = filaOriginal["CODIGO"].ToString();
            tabla_temp.Rows.Add(fila_temp);
        }

        Session["dt_GRID_RIESGOS"] = tabla_temp;
    }

    private DataRow obtenerDatosCiudadOriginoNegocio(String idCiudad)
    {
        DataRow resultado = null;

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());

        DataTable tablaIdDepartamento = _ciudad.ObtenerIdDepartamentoConIdCiudad(idCiudad);

        if (tablaIdDepartamento.Rows.Count > 0)
        {
            resultado = tablaIdDepartamento.Rows[0];
        }

        return resultado;
    }

    private void cargar_DropDownList_DEPARTAMENTO_ORIGINO()
    {
        DropDownList_DEPARTAMENTO_ORIGINO.Items.Clear();

        departamento _departamento = new departamento(Session["idEmpresa"].ToString());
        DataTable tablaDepartamentos = _departamento.ObtenerTodasLosDepartamentos();

        ListItem item = new ListItem("Seleccione Departamento", "");
        DropDownList_DEPARTAMENTO_ORIGINO.Items.Add(item);

        foreach (DataRow fila in tablaDepartamentos.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_DEPARTAMENTO"].ToString());
            DropDownList_DEPARTAMENTO_ORIGINO.Items.Add(item);
        }

        DropDownList_DEPARTAMENTO_ORIGINO.DataBind();
    }

    private void cargar_DropDownList_CIUDAD_ORIGINO(String idDepartamento)
    {
        DropDownList_CIUDAD_ORIGINO.Items.Clear();

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
        DataTable tablaCiudades = _ciudad.ObtenerCiudadesPorIdDepartamento(idDepartamento);

        ListItem item = new ListItem("Seleccione Ciudad", "");
        DropDownList_CIUDAD_ORIGINO.Items.Add(item);

        foreach (DataRow fila in tablaCiudades.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_CIUDAD"].ToString());
            DropDownList_CIUDAD_ORIGINO.Items.Add(item);
        }

        DropDownList_CIUDAD_ORIGINO.DataBind();
    }

    private void cargamos_DropDownList_TIP_CEDULA_REP_LEGAL_CLIENTE()
    {
        DropDownList_TIP_CEDULA_REP_LEGAL_CLIENTE.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_TIP_DOC_ID);

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_TIP_CEDULA_REP_LEGAL_CLIENTE.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_TIP_CEDULA_REP_LEGAL_CLIENTE.Items.Add(item);
        }
        DropDownList_TIP_CEDULA_REP_LEGAL_CLIENTE.DataBind();
    }

    private void cargar_DropDownList_DEPARTAMENTO_REP_LEGAL()
    {
        DropDownList_DEP_CC_REP_LEGAL.Items.Clear();

        departamento _departamento = new departamento(Session["idEmpresa"].ToString());
        DataTable tablaDepartamentos = _departamento.ObtenerTodasLosDepartamentos();

        ListItem item = new ListItem("Seleccione Departamento", "");
        DropDownList_DEP_CC_REP_LEGAL.Items.Add(item);

        foreach (DataRow fila in tablaDepartamentos.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_DEPARTAMENTO"].ToString());
            DropDownList_DEP_CC_REP_LEGAL.Items.Add(item);
        }

        DropDownList_DEP_CC_REP_LEGAL.DataBind();
    }

    private void cargar_DropDownList_CIU_CC_REP_LEGAL(String idDepartamento)
    {
        DropDownList_CIU_CC_REP_LEGAL.Items.Clear();

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
        DataTable tablaCiudades = _ciudad.ObtenerCiudadesPorIdDepartamento(idDepartamento);

        ListItem item = new ListItem("Seleccione Ciudad", "");
        DropDownList_CIU_CC_REP_LEGAL.Items.Add(item);

        foreach (DataRow fila in tablaCiudades.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_CIUDAD"].ToString());
            DropDownList_CIU_CC_REP_LEGAL.Items.Add(item);
        }

        DropDownList_CIU_CC_REP_LEGAL.DataBind();
    }

    private void inhabilitar_DropDownList_DEP_CC_REP_LEGAL()
    {
        DropDownList_DEP_CC_REP_LEGAL.Enabled = false;
        DropDownList_DEP_CC_REP_LEGAL.Items.Clear();
        ListItem item = new ListItem("Seleccione", "");
        DropDownList_DEP_CC_REP_LEGAL.Items.Add(item);
        DropDownList_DEP_CC_REP_LEGAL.DataBind();
    }

    private void inhabilitar_DropDownList_CIU_CC_REP_LEGAL()
    {
        DropDownList_CIU_CC_REP_LEGAL.Enabled = false;
        DropDownList_CIU_CC_REP_LEGAL.Items.Clear();
        ListItem item = new ListItem("Seleccione", "");
        DropDownList_CIU_CC_REP_LEGAL.Items.Add(item);
        DropDownList_CIU_CC_REP_LEGAL.DataBind();
    }
    private void cargar_num_empleados_reales(Decimal ID_EMPRESA_PARAM, Decimal num_emplados_proyectados)
    {
        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaDatos = _cliente.ObtenerNumEmpleadosActivosPorIdEmpresa(ID_EMPRESA_PARAM, "S", "S");

        DataRow filaDatos = tablaDatos.Rows[0];

        Decimal num_empleados_reales = Convert.ToDecimal(filaDatos["NUM_EMPLEADOS"]);
        Decimal margen_inf = num_emplados_proyectados - (num_emplados_proyectados * (tabla.VAR_MARGEN_AVISO_EMPLEADOS / 100));
        Decimal margen_sup = num_emplados_proyectados + (num_emplados_proyectados * (tabla.VAR_MARGEN_AVISO_EMPLEADOS / 100));

        if ((num_empleados_reales < margen_inf) || (num_empleados_reales > margen_sup))
        {
            Label_num_empleados_reales.ForeColor = System.Drawing.Color.Red;
        }
        else
        {
            Label_num_empleados_reales.ForeColor = System.Drawing.Color.Black;
        }

        Label_num_empleados_reales.Text = num_empleados_reales.ToString();
    }

    private void cargar_unidad_de_negocio(Decimal ID_EMPRESA)
    {
        seguridad _seguridad = new seguridad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaContratosOriginal = _seguridad.ObtenerUsuariosPorEmpresa(ID_EMPRESA);

        if (tablaContratosOriginal.Rows.Count <= 0)
        {
            if (_seguridad.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _seguridad.MensajeError, Proceso.Error);
            }

            Panel_INFO_SIN_UNIDAD_NEGOCIO.Visible = true;
            Panel_GRILLA_UNIDAD_NEGOCIO.Visible = false;

            GridView_UNIDAD_NEGOCIO.DataSource = null;
            GridView_UNIDAD_NEGOCIO.DataBind();
        }
        else
        {
            Panel_INFO_SIN_UNIDAD_NEGOCIO.Visible = false;
            Panel_GRILLA_UNIDAD_NEGOCIO.Visible = true;

            GridView_UNIDAD_NEGOCIO.DataSource = tablaContratosOriginal;
            GridView_UNIDAD_NEGOCIO.DataBind();
        }
    }


    private void cargar_datos_empresa(DataRow informacionEmpresa)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(informacionEmpresa["ID_EMPRESA"]);

        TextBox_COD_EMPRESA.Text = informacionEmpresa["COD_EMPRESA"].ToString().Trim();
        cargar_DropDownList_ACTIVO();
        DropDownList_ACTIVO.SelectedValue = informacionEmpresa["ACTIVO"].ToString().Trim();

        TextBox_FCH_INGRESO.Text = DateTime.Parse(informacionEmpresa["FCH_INGRESO"].ToString().Trim()).ToShortDateString().Trim();
        TextBox_NIT_EMPRESA.Text = informacionEmpresa["NIT_EMPRESA"].ToString().Trim();
        TextBox_DIG_VER.Text = informacionEmpresa["DIG_VER"].ToString().Trim();
        TextBox_RAZ_SOCIAL.Text = informacionEmpresa["RAZ_SOCIAL"].ToString().Trim();
        TextBox_DIR_EMP.Text = informacionEmpresa["DIR_EMP"].ToString().Trim();

        DataRow filaInfoCiudadEmpresa = obtenerDatosCiudadCliente(informacionEmpresa["CIU_EMP"].ToString().Trim());
        if (filaInfoCiudadEmpresa != null)
        {
            cargar_DropDownList_REGIONAL();
            DropDownList_REGIONAL.SelectedValue = filaInfoCiudadEmpresa["ID_REGIONAL"].ToString().Trim();
            cargar_DropDownList_DEPARTAMENTO(filaInfoCiudadEmpresa["ID_REGIONAL"].ToString().Trim());
            DropDownList_DEPARTAMENTO.SelectedValue = filaInfoCiudadEmpresa["ID_DEPARTAMENTO"].ToString().Trim();
            cargar_DropDownList_CIUDAD(filaInfoCiudadEmpresa["ID_REGIONAL"].ToString().Trim(), filaInfoCiudadEmpresa["ID_DEPARTAMENTO"].ToString().Trim());
            DropDownList_CIUDAD.SelectedValue = filaInfoCiudadEmpresa["ID_CIUDAD"].ToString().Trim();
        }
        else
        {
            cargar_DropDownList_REGIONAL();
            inhabilitar_DropDownList_DEPARTAMENTO();
            inhabilitar_DropDownList_CIUDAD();
        }

        TextBox_TEL_EMP.Text = informacionEmpresa["TEL_EMP"].ToString().Trim();
        TextBox_TEL_EMP_1.Text = informacionEmpresa["TEL_EMP1"].ToString().Trim();
        TextBox_CEL_EMP.Text = informacionEmpresa["NUM_CELULAR"].ToString().Trim();

        DataRow filaInfoActividadEmpresa = obtenerDatosActividadCliente(informacionEmpresa["ID_ACTIVIDAD"].ToString().Trim());
        if (filaInfoActividadEmpresa != null)
        {
            cargar_DropDownList_NOMBRE_SECCION();
            DropDownList_NOMBRE_SECCION.SelectedValue = filaInfoActividadEmpresa["ID_SECCION"].ToString().Trim();

            cargar_DropDownList_DIVISION(filaInfoActividadEmpresa["ID_SECCION"].ToString().Trim());
            DropDownList_DIVISION.SelectedValue = filaInfoActividadEmpresa["ID_DIVISION"].ToString().Trim();

            cargar_DropDownList_CLASE(filaInfoActividadEmpresa["ID_DIVISION"].ToString().Trim());
            DropDownList_CLASE.SelectedValue = filaInfoActividadEmpresa["ID_CLASE"].ToString().Trim();

            cargar_DropDownList_ACTIVIDAD(filaInfoActividadEmpresa["ID_CLASE"].ToString().Trim());
            DropDownList_ACTIVIDAD.SelectedValue = filaInfoActividadEmpresa["ID_ACTIVIDAD"].ToString().Trim();
        }
        else
        {
            cargar_DropDownList_NOMBRE_SECCION();
            inhabilitar_DropDownList_DIVISION();
            inhabilitar_DropDownList_CLASE();
            inhabilitar_DropDownList_ACTIVIDAD();
        }

        TextBox_DES_ACTIVIDAD.Text = informacionEmpresa["ACT_ECO"].ToString().Trim();

        cargar_DropDownList_GRUPO_EMPRESARIAL();
        try
        {
            DropDownList_GRUPO_EMPRESARIAL.SelectedValue = informacionEmpresa["ID_GRUPO_EMPRESARIAL"].ToString().Trim();
        }
        catch
        {
            DropDownList_GRUPO_EMPRESARIAL.SelectedIndex = 0;
        }

        cargar_DropDownList_ALIANZA();
        try
        {
            DropDownList_ALIANZA.SelectedValue = informacionEmpresa["ID_ALIANZA"].ToString().Trim();
        }
        catch
        {
            DropDownList_ALIANZA.SelectedIndex = 0;
        }

        cargar_DropDownList_EMP_ESTADO();
        try
        {
            DropDownList_EMP_ESTADO.SelectedValue = informacionEmpresa["EMP_ESTADO"].ToString().Trim();
        }
        catch
        {
            DropDownList_EMP_ESTADO.SelectedIndex = 0;
        }

        cargar_DropDownList_EMP_EXC_IVA();
        try
        {
            DropDownList_EMP_EXC_IVA.SelectedValue = informacionEmpresa["EMP_EXC_IVA"].ToString().Trim();
        }
        catch
        {
            DropDownList_EMP_EXC_IVA.SelectedIndex = 0;
        }

        cargar_DropDownList_FAC_NAL();
        try
        {
            DropDownList_FAC_NAL.SelectedValue = informacionEmpresa["FAC_NAL"].ToString().Trim();
        }
        catch
        {
            DropDownList_FAC_NAL.SelectedIndex = 0;
        }

        cargar_DropDownList_TIPO_EMPRESA();
        try
        {
            DropDownList_TIPO_EMPRESA.SelectedValue = informacionEmpresa["TIPO_EMPRESA"].ToString().Trim();
        }
        catch
        {
            DropDownList_TIPO_EMPRESA.SelectedIndex = 0;
        }

        TextBox_NUM_EMPLEADOS.Text = informacionEmpresa["NUM_EMPLEADOS"].ToString().Trim();

        DataRow filaInfoCiudadYDepartamento = obtenerDatosCiudadOriginoNegocio(informacionEmpresa["CIU_ORG_NEG"].ToString().Trim());
        if (filaInfoCiudadYDepartamento != null)
        {
            cargar_DropDownList_DEPARTAMENTO_ORIGINO();
            DropDownList_DEPARTAMENTO_ORIGINO.SelectedValue = filaInfoCiudadYDepartamento["ID_DEPARTAMENTO"].ToString().Trim();

            cargar_DropDownList_CIUDAD_ORIGINO(filaInfoCiudadYDepartamento["ID_DEPARTAMENTO"].ToString().Trim());
            DropDownList_CIUDAD_ORIGINO.SelectedValue = filaInfoCiudadYDepartamento["ID_CIUDAD"].ToString().Trim();
        }
        else
        {
            cargar_DropDownList_DEPARTAMENTO_ORIGINO();
            inhabilitar_DropDownList_CIUDAD_ORIGINO();
        }

        TextBox_NOM_REP_LEGAL.Text = informacionEmpresa["NOM_REP_LEGAL"].ToString().Trim();

        cargamos_DropDownList_TIP_CEDULA_REP_LEGAL_CLIENTE();
        try
        {
            DropDownList_TIP_CEDULA_REP_LEGAL_CLIENTE.SelectedValue = informacionEmpresa["TIP_DOC_REP_LEGAL"].ToString().Trim();
        }
        catch
        {
            DropDownList_TIP_CEDULA_REP_LEGAL_CLIENTE.ClearSelection();
        }

        TextBox_CC_REP_LEGAL.Text = informacionEmpresa["CC_REP_LEGAL"].ToString().Trim();

        if (DropDownList_TIP_CEDULA_REP_LEGAL_CLIENTE.SelectedValue == "CC")
        {
            filaInfoCiudadYDepartamento = obtenerDatosCiudadOriginoNegocio(informacionEmpresa["ID_CIUDAD_CC_REP_LEGAL"].ToString().Trim());
            if (filaInfoCiudadYDepartamento != null)
            {
                cargar_DropDownList_DEPARTAMENTO_REP_LEGAL();
                DropDownList_DEP_CC_REP_LEGAL.SelectedValue = filaInfoCiudadYDepartamento["ID_DEPARTAMENTO"].ToString().Trim();

                cargar_DropDownList_CIU_CC_REP_LEGAL(filaInfoCiudadYDepartamento["ID_DEPARTAMENTO"].ToString().Trim());
                DropDownList_CIU_CC_REP_LEGAL.SelectedValue = filaInfoCiudadYDepartamento["ID_CIUDAD"].ToString().Trim();

                RequiredFieldValidator_DropDownList_CIU_CC_REP_LEGAL.Enabled = true;
            }
        }
        else
        {
            inhabilitar_DropDownList_DEP_CC_REP_LEGAL();
            inhabilitar_DropDownList_CIU_CC_REP_LEGAL();

            RequiredFieldValidator_DropDownList_CIU_CC_REP_LEGAL.Enabled = false;
        }


        cargar_unidad_de_negocio(ID_EMPRESA);


        Decimal num_emplados_proyectados = 0;
        try
        {
            num_emplados_proyectados = Convert.ToDecimal(informacionEmpresa["NUM_EMPLEADOS"]);
        }
        catch
        {
            num_emplados_proyectados = 0;
        }
        cargar_num_empleados_reales(ID_EMPRESA, num_emplados_proyectados);

        llenarGridRiesgos(Convert.ToDecimal(informacionEmpresa["ID_EMPRESA"]));

        llenarGridCobertura(Convert.ToDecimal(informacionEmpresa["ID_EMPRESA"]));
    }

    private void Cargar(Decimal ID_EMPRESA)
    {
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);

        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaEmpresa = _cliente.ObtenerEmpresaConIdEmpresa(ID_EMPRESA);

        if (_cliente.MensajeError != null)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _cliente.MensajeError, Proceso.Error);

            Mostrar(Acciones.Inicio);
        }
        else
        {
            if (tablaEmpresa.Rows.Count <= 0)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró una empresa con el ID: " + ID_EMPRESA.ToString(), Proceso.Error);

                Mostrar(Acciones.Inicio);
            }
            else
            {
                DataRow informacionEmpresa = tablaEmpresa.Rows[0];

                HiddenField_ID_EMPRESA.Value = ID_EMPRESA.ToString();
                HiddenField_ESTADO_EMPRESA.Value = informacionEmpresa["ACTIVO"].ToString();
                HiddenField_RAZ_SOCIAL_ANTERIOR.Value = informacionEmpresa["RAZ_SOCIAL"].ToString().Trim();

                this.Title = informacionEmpresa["RAZ_SOCIAL"].ToString().Trim();

                Desactivar(Acciones.CargarEmpresa);

                Mostrar(Acciones.CargarEmpresa);
                
                Page.Header.Title = informacionEmpresa["RAZ_SOCIAL"].ToString();

                Label_INFO_ADICIONAL_MODULO.Text = informacionEmpresa["RAZ_SOCIAL"].ToString().Trim();

                cargar_menu_interno(ID_EMPRESA);

                cargar_control_registro(informacionEmpresa);

                cargar_datos_empresa(informacionEmpresa);
                
            }
        }
    }

    protected void GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged(object sender, EventArgs e)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["ID"]);

        Cargar(ID_EMPRESA);
        Session.Add("ID_EMPRESA", ID_EMPRESA);
    }

    protected void DropDownList_ACTIVO_SelectedIndexChanged(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        if (DropDownList_ACTIVO.SelectedValue != "")
        {
            String ID_EMPRESA = HiddenField_ID_EMPRESA.Value;

            if (HiddenField_ESTADO_EMPRESA.Value  == "")
            {
                Panel_HISTORIAL_ACT.Visible = false;
            }
            else
            {
                if (HiddenField_ESTADO_EMPRESA.Value == DropDownList_ACTIVO.SelectedValue.ToString())
                {
                    Panel_HISTORIAL_ACT.Visible = false;
                }
                else
                {
                    if (HiddenField_ESTADO_EMPRESA.Value == "S")
                    {
                        if (DropDownList_ACTIVO.SelectedValue == "N")
                        {
                            Label_TIPO_DE_ACTIVACION.Text = "DESACTIVACIÓN";
                        }
                        else
                        {
                            Label_TIPO_DE_ACTIVACION.Text = DropDownList_ACTIVO.SelectedItem.Text;
                        }
                    }
                    else
                    {
                        if (HiddenField_ESTADO_EMPRESA.Value == "N")
                        {
                            if (DropDownList_ACTIVO.SelectedValue.ToString() == "S")
                            {
                                Label_TIPO_DE_ACTIVACION.Text = "ACTIVACIÓN";
                            }
                            else
                            {
                                Label_TIPO_DE_ACTIVACION.Text = DropDownList_ACTIVO.SelectedItem.Text;
                            }
                        }
                        else
                        {
                            if (DropDownList_ACTIVO.SelectedValue.ToString() == "S")
                            {
                                Label_TIPO_DE_ACTIVACION.Text = "ACTIVACIÓN";
                            }
                            else
                            {
                                if (DropDownList_ACTIVO.SelectedValue.ToString() == "N")
                                {
                                    Label_TIPO_DE_ACTIVACION.Text = "DESACTIVACIÓN";
                                }
                                else
                                {
                                    Label_TIPO_DE_ACTIVACION.Text = DropDownList_ACTIVO.SelectedItem.Text;
                                }
                            }
                        }
                    }

                    Panel_HISTORIAL_ACT.Visible = true;
                }
            }
        }
        else
        {
            Panel_HISTORIAL_ACT.Visible = false;
        }
    }

    protected void DropDownList_REGIONAL_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_REGIONAL.SelectedValue == "")
        {
            inhabilitar_DropDownList_CIUDAD();
            inhabilitar_DropDownList_DEPARTAMENTO();
        }
        else
        {
            cargar_DropDownList_DEPARTAMENTO(DropDownList_REGIONAL.SelectedValue.ToString());
            DropDownList_DEPARTAMENTO.Enabled = true;
            inhabilitar_DropDownList_CIUDAD();
        }
    }
    protected void DropDownList_DEPARTAMENTO_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_DEPARTAMENTO.SelectedValue == "")
        {
            inhabilitar_DropDownList_CIUDAD();
        }
        else
        {
            cargar_DropDownList_CIUDAD(DropDownList_REGIONAL.SelectedValue.ToString(), DropDownList_DEPARTAMENTO.SelectedValue.ToString());
            DropDownList_CIUDAD.Enabled = true;
        }
    }

    protected void DropDownList_NOMBRE_SECCION_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_NOMBRE_SECCION.SelectedValue == "")
        {
            inhabilitar_DropDownList_DIVISION();
            inhabilitar_DropDownList_CLASE();
            inhabilitar_DropDownList_ACTIVIDAD();
        }
        else
        {
            String ID_SECCION = DropDownList_NOMBRE_SECCION.SelectedValue.ToString();
            cargar_DropDownList_DIVISION(ID_SECCION);
            DropDownList_DIVISION.Enabled = true;
            inhabilitar_DropDownList_CLASE();
            inhabilitar_DropDownList_ACTIVIDAD();
        }
    }

    protected void DropDownList_DIVISION_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_DIVISION.SelectedValue == "")
        {
            inhabilitar_DropDownList_CLASE();
            inhabilitar_DropDownList_ACTIVIDAD();
        }
        else
        {
            String ID_DIVISION = DropDownList_DIVISION.SelectedValue.ToString();
            cargar_DropDownList_CLASE(ID_DIVISION);
            DropDownList_CLASE.Enabled = true;
            inhabilitar_DropDownList_ACTIVIDAD();
        }
    }

    protected void DropDownList_CLASE_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_CLASE.SelectedValue == "")
        {
            inhabilitar_DropDownList_ACTIVIDAD();
        }
        else
        {
            String ID_CLASE = DropDownList_CLASE.SelectedValue.ToString();
            cargar_DropDownList_ACTIVIDAD(ID_CLASE);
            DropDownList_ACTIVIDAD.Enabled = true;
        }
    }

    protected void DropDownList_LISTA_RIESGOS_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_LISTA_RIESGOS.SelectedValue.ToString() != "")
        {
            if (Session["dt_GRID_RIESGOS"] == null)
            {
                DataTable tabla_temp = new DataTable();
                tabla_temp.Columns.Add("ACTIVO");
                tabla_temp.Columns.Add("DESCRIPCION_RIESGO");
                tabla_temp.Columns.Add("ID_EMPRESA");
                tabla_temp.Columns.Add("CODIGO");

                DataRow fila_temp = tabla_temp.NewRow();

                fila_temp["ACTIVO"] = "S";
                fila_temp["DESCRIPCION_RIESGO"] = DropDownList_LISTA_RIESGOS.SelectedValue;
                fila_temp["ID_EMPRESA"] = HiddenField_ID_EMPRESA.Value;

                String[] arrayRiesgo = DropDownList_LISTA_RIESGOS.SelectedItem.Text.Split('-');
                String porcentaje = arrayRiesgo[1];
                porcentaje = porcentaje.Substring(0, porcentaje.Length - 1);
                fila_temp["CODIGO"] = porcentaje;

                tabla_temp.Rows.Add(fila_temp);


                GridView_RIESGOS_CONFIGURADOS.DataSource = tabla_temp;
                GridView_RIESGOS_CONFIGURADOS.DataBind();

                Session["dt_GRID_RIESGOS"] = tabla_temp;
            }
            else
            {
                DataTable tabla_temp = ((DataTable)Session["dt_GRID_RIESGOS"]);

                String DESCRIPCION_RIESGO = DropDownList_LISTA_RIESGOS.SelectedValue;

                Boolean verificador = false;
                foreach (DataRow fila in tabla_temp.Rows)
                {
                    if (fila["DESCRIPCION_RIESGO"].ToString() == DESCRIPCION_RIESGO)
                    {
                        verificador = true;
                        break;
                    }
                }

                if (verificador == false)
                {
                    DataRow fila_temp = tabla_temp.NewRow();

                    fila_temp["ACTIVO"] = "S";
                    fila_temp["DESCRIPCION_RIESGO"] = DESCRIPCION_RIESGO;
                    fila_temp["ID_EMPRESA"] = HiddenField_ID_EMPRESA.Value;

                    String[] arrayRiesgo = DropDownList_LISTA_RIESGOS.SelectedItem.Text.Split('-');
                    String porcentaje = arrayRiesgo[1];
                    porcentaje = porcentaje.Substring(0, porcentaje.Length - 1);
                    fila_temp["CODIGO"] = porcentaje;

                    tabla_temp.Rows.Add(fila_temp);

                    DataView DV = new DataView(tabla_temp, "", "DESCRIPCION_RIESGO", DataViewRowState.CurrentRows);

                    GridView_RIESGOS_CONFIGURADOS.DataSource = DV;
                    GridView_RIESGOS_CONFIGURADOS.DataBind();

                    Session["dt_GRID_RIESGOS"] = DV.ToTable();
                }
            }
        }
    }

    protected void GridView_RIESGOS_CONFIGURADOS_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "eliminar")
        {
            DataTable tabla_temp = ((DataTable)Session["dt_GRID_RIESGOS"]);

            tabla_temp.Rows[indexSeleccionado].Delete();

            GridView_RIESGOS_CONFIGURADOS.DataSource = tabla_temp;

            GridView_RIESGOS_CONFIGURADOS.SelectedIndex = -1;

            GridView_RIESGOS_CONFIGURADOS.DataBind();

            Session["dt_GRID_RIESGOS"] = tabla_temp;
        }
    }

    protected void DropDownList_REGIONAL_COVERTURA_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_REGIONAL_COVERTURA.SelectedValue == "")
        {
            inhabilitar_DropDownList_DEPARTAMNETO_COVERTURA();
            inhabilitar_DropDownList_CIUDAD_COVERTURA();
        }
        else
        {
            String ID_REGIONAL = DropDownList_REGIONAL_COVERTURA.SelectedValue.ToString();
            cargar_DropDownList_DEPARTAMENTO_COVERTURA(ID_REGIONAL);
            DropDownList_DEPARTAMNETO_COVERTURA.Enabled = true;
            inhabilitar_DropDownList_CIUDAD_COVERTURA();
        }
    }

    protected void DropDownList_DEPARTAMNETO_COVERTURA_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_DEPARTAMNETO_COVERTURA.SelectedValue == "")
        {
            inhabilitar_DropDownList_CIUDAD_COVERTURA();
        }
        else
        {
            String ID_REGIONAL = DropDownList_REGIONAL_COVERTURA.SelectedValue.ToString();
            String ID_DEPARTAMENTO = DropDownList_DEPARTAMNETO_COVERTURA.SelectedValue.ToString();
            cargar_DropDownList_CIUDAD_COVERTURA(ID_REGIONAL, ID_DEPARTAMENTO);
            DropDownList_CIUDAD_COVERTURA.Enabled = true;
        }
    }

    protected void GridView_COVERTURA_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable tabla_temp = ((DataTable)Session["dt_GRID_COVERTURA"]);

        tabla_temp.Rows[GridView_COVERTURA.SelectedIndex].Delete();

        GridView_COVERTURA.DataSource = tabla_temp;

        GridView_COVERTURA.SelectedIndex = -1;

        GridView_COVERTURA.DataBind();

        Session["dt_GRID_COVERTURA"] = tabla_temp;
    }

    protected void DropDownList_DEPARTAMENTO_ORIGINO_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_DEPARTAMENTO_ORIGINO.SelectedValue == "")
        {
            inhabilitar_DropDownList_CIUDAD_ORIGINO();
        }
        else
        {
            String ID_DEPARTAMENTO = DropDownList_DEPARTAMENTO_ORIGINO.SelectedValue.ToString();
            cargar_DropDownList_CIUDAD_ORIGINO(ID_DEPARTAMENTO);
            DropDownList_CIUDAD_ORIGINO.Enabled = true;
        }
    }

    protected void DropDownList_TIP_CEDULA_REP_LEGAL_CLIENTE_SelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox_CC_REP_LEGAL.Text = "";
        inhabilitar_DropDownList_CIU_CC_REP_LEGAL();

        if (DropDownList_TIP_CEDULA_REP_LEGAL_CLIENTE.SelectedValue == "CC")
        {
            cargar_DropDownList_DEPARTAMENTO_REP_LEGAL();
            DropDownList_DEP_CC_REP_LEGAL.Enabled = true;
            RequiredFieldValidator_DropDownList_CIU_CC_REP_LEGAL.Enabled = true;
        }
        else
        {
            inhabilitar_DropDownList_DEP_CC_REP_LEGAL();
            RequiredFieldValidator_DropDownList_CIU_CC_REP_LEGAL.Enabled = false;
        }
    }

    protected void DropDownList_DEP_CC_REP_LEGAL_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_DEP_CC_REP_LEGAL.SelectedValue == "")
        {
            inhabilitar_DropDownList_CIU_CC_REP_LEGAL();
        }
        else
        {
            String ID_DEPARTAMENTO = DropDownList_DEP_CC_REP_LEGAL.SelectedValue.ToString();
            cargar_DropDownList_CIU_CC_REP_LEGAL(ID_DEPARTAMENTO);
            DropDownList_CIU_CC_REP_LEGAL.Enabled = true;
        }
    }

    protected void DropDownList7_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_CIUDAD_COVERTURA.SelectedValue.ToString() != "")
        {
            if (Session["dt_GRID_COVERTURA"] == null)
            {
                DataTable tabla_temp = new DataTable();
                tabla_temp.Columns.Add("Código Ciudad");
                tabla_temp.Columns.Add("Regional");
                tabla_temp.Columns.Add("Departamento");
                tabla_temp.Columns.Add("Ciudad");

                DataRow fila_temp = tabla_temp.NewRow();

                fila_temp["Código Ciudad"] = DropDownList_CIUDAD_COVERTURA.SelectedValue;
                fila_temp["Regional"] = DropDownList_REGIONAL_COVERTURA.SelectedItem.ToString();
                fila_temp["Departamento"] = DropDownList_DEPARTAMNETO_COVERTURA.SelectedItem.ToString();
                fila_temp["Ciudad"] = DropDownList_CIUDAD_COVERTURA.SelectedItem.ToString();

                tabla_temp.Rows.Add(fila_temp);

                GridView_COVERTURA.DataSource = tabla_temp;

                GridView_COVERTURA.DataBind();


                Session["dt_GRID_COVERTURA"] = tabla_temp;
            }
            else
            {
                DataTable tabla_temp = ((DataTable)Session["dt_GRID_COVERTURA"]);

                String ID_CIUDAD_SELECCIONADO = DropDownList_CIUDAD_COVERTURA.SelectedValue;

                Boolean verificador = false;
                foreach (DataRow fila in tabla_temp.Rows)
                {
                    if (fila["Código Ciudad"].ToString() == ID_CIUDAD_SELECCIONADO)
                    {
                        verificador = true;
                        break;
                    }
                }

                if (verificador == false)
                {
                    DataRow fila_temp = tabla_temp.NewRow();

                    fila_temp["Código Ciudad"] = DropDownList_CIUDAD_COVERTURA.SelectedValue;
                    fila_temp["Regional"] = DropDownList_REGIONAL_COVERTURA.SelectedItem.ToString();
                    fila_temp["Departamento"] = DropDownList_DEPARTAMNETO_COVERTURA.SelectedItem.ToString();
                    fila_temp["Ciudad"] = DropDownList_CIUDAD_COVERTURA.SelectedItem.ToString();

                    tabla_temp.Rows.Add(fila_temp);

                    DataView DV = new DataView(tabla_temp, "", "Ciudad", DataViewRowState.CurrentRows);

                    GridView_COVERTURA.DataSource = DV;

                    GridView_COVERTURA.DataBind();

                    Session["dt_GRID_COVERTURA"] = DV.ToTable();
                }
            }
        }
    }
    protected void DropDownList_FAC_NAL_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
