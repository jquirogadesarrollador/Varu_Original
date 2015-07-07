using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using Brainsbits.LLB.comercial;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB.seleccion;
using Brainsbits.LLB;

using TSHAK.Components;

public partial class _Default : System.Web.UI.Page
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
    private String GLO_ID_CIUDAD = "";
    private Decimal GLO_ID_CENTRO_C = 0;
    private Decimal GLO_ID_SUB_C = 0;

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }

    private enum Acciones
    {
        Inicio = 0,
        Buscar,
        SinUbicacion,
        Cargar
    }

    private enum Entidades
    {
        Arl=0,
        Eps,
        Afp,
        Ccf
    }
    #endregion variables

    #region constructores
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = "AFILIACIONES";

        CargarQueryString();

        cargar_menu_botones_modulos_internos();

        if (IsPostBack == false)
        {
            Iniciar();
        }
    }

    #endregion constructores

    #region metodos
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

    private void configurar_paneles_popup(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_mensaje.CssClass = "panel_popup";
    }

    private void Configurar()
    {
        configurar_paneles_popup(Panel_FONDO_MENSAJE, Panel_MENSAJES);
        configurar_paneles_popup(Panel_FONDO_MENSAJE_ARP, Panel_MENSAJES_ARP);
        configurar_paneles_popup(Panel_FONDO_MENSAJE_EPS, Panel_MENSAJES_EPS);
        configurar_paneles_popup(Panel_FONDO_MENSAJE_CCF, Panel_MENSAJES_CCF);
        configurar_paneles_popup(Panel_FONDO_MENSAJE_AFP, Panel_MENSAJES_AFP);
    }

    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = false;
                Panel_BOTONES_INTERNOS.Visible = false;
                Panel_FORM_BOTONES.Visible = false;
                Panel_RESULTADOS_GRID.Visible = false;

                Panel_INFORMACION_CANDIDATO_SELECCION.Visible = false;

                Panel_UbicacionTrabajador.Visible = false;

                Panel_ARP.Visible = false;

                Panel_EPS.Visible = false;

                Panel_CCF.Visible = false;

                Panel_AFP.Visible = false;
                panel_tipo_pensionado.Visible = false;

                break;
        }
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_FORM_BOTONES.Visible = true;
                break;
            case Acciones.Buscar:
                Panel_FORM_BOTONES.Visible = true;
                Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.Cargar:
                Panel_BOTONES_INTERNOS.Visible = true;
                Panel_FORM_BOTONES.Visible = true;
                Panel_INFORMACION_CANDIDATO_SELECCION.Visible = true;
                Panel_UbicacionTrabajador.Visible = true;

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

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Ninguno", "");
        DropDownList_BUSCAR.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("NÚMERO IDENTIFICACIÓN", "NUM_DOC_IDENTIFICACION");
        DropDownList_BUSCAR.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("NOMBRES", "NOMBRES");
        DropDownList_BUSCAR.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("APELLIDOS", "APELLIDOS");
        DropDownList_BUSCAR.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("EMPRESA", "RAZ_SOCIAL");
        DropDownList_BUSCAR.Items.Add(item);

        DropDownList_BUSCAR.DataBind();
    }

    private void iniciar_seccion_de_busqueda()
    {
        TextBox_BUSCAR.Text = "";
        cargar_DropDownList_BUSCAR();

    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                configurarCaracteresAceptadosBusqueda(true, true);

                HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "NINGUNA";
                HiddenField_FILTRO_DROP.Value = String.Empty;
                HiddenField_FILTRO_DATO.Value = String.Empty;

                iniciar_seccion_de_busqueda();
                break;
        }
    }

    private void cargar_menu_botones_modulos_internos()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = "contratacion";
        QueryStringSeguro["nombre_area"] = "CONTRATOS Y RELACIONES LABORALES";

        QueryStringSeguro["empresa"] = HiddenField_ID_EMPRESA.Value;
        QueryStringSeguro["solicitud"] = HiddenField_ID_SOLICITUD.Value;
        QueryStringSeguro["requerimiento"] = HiddenField_ID_REQUERIMIENTO.Value;
        QueryStringSeguro["cargo"] = HiddenField_ID_OCUPACION.Value;
        QueryStringSeguro["docID"] = HiddenField_NUM_DOC_IDENTIDAD.Value;

        QueryStringSeguro["persona"] = HiddenField_persona.Value;

        TableRow filaTabla;
        TableCell celdaTabla;
        HyperLink link;
        System.Web.UI.WebControls.Image imagen;

        int contadorFilas = 0;

        filaTabla = new TableRow();
        filaTabla.ID = "row_" + contadorFilas.ToString();

        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_1_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_examenes";

        QueryStringSeguro["accion"] = "cargar";
        QueryStringSeguro["nombre_modulo"] = "EXAMENES Y CUENTA BANCARIA";
        link.NavigateUrl = "~/contratacion/examenesEmpleado.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());

        link.CssClass = "botones_menu_principal";
        imagen = new System.Web.UI.WebControls.Image();
        imagen.ImageUrl = "~/imagenes/areas/bExamenCuentaEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bExamenCuentaAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bExamenCuentaEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);


        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_2_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_afiliaciones";
        QueryStringSeguro["accion"] = "cargar";
        QueryStringSeguro["nombre_modulo"] = "AFILIACIONES";
        link.NavigateUrl = "~/contratacion/afiliaciones.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        imagen = new System.Web.UI.WebControls.Image();
        imagen.ImageUrl = "~/imagenes/areas/bAfiliacionesEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bAfiliacionesAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bAfiliacionesEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);



        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_3_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_elaborar_contrato";
        QueryStringSeguro["accion"] = "cargar";
        QueryStringSeguro["nombre_modulo"] = "ELABORAR CONTRATO";
        link.NavigateUrl = "~/contratacion/activarEmpleado.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        imagen = new System.Web.UI.WebControls.Image();
        imagen.ImageUrl = "~/imagenes/areas/bElaborarContratoEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bElaborarContratoAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bElaborarContratoEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);


        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_4_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_servicios";
        QueryStringSeguro["accion"] = "cargar";
        QueryStringSeguro["nombre_modulo"] = "SERVICIOS COMPLEMENTARIOS";
        link.NavigateUrl = "~/maestros/EntregasEmpleado.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        imagen = new System.Web.UI.WebControls.Image();
        imagen.ImageUrl = "~/imagenes/areas/bServiciosComplementariosEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bServiciosComplementariosAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bServiciosComplementariosEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);


        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_5_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_volver";
        QueryStringSeguro["accion"] = "inicial";
        link.NavigateUrl = "~/contratacion/hojaTrabajoContratacion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        imagen = new System.Web.UI.WebControls.Image();
        imagen.ImageUrl = "~/imagenes/areas/bVolverHojaTrabajoEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bVolverHojaTrabajoAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bVolverHojaTrabajoEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);

        Table_MENU.Rows.Add(filaTabla);

    }

    private void cargarDatosTrabajador(String NOMBRE_TRABAJADOR, String NUM_DOC_IDENTIDAD, String RAZ_SOCIAL, String CARGO, String ID_OCUPACION)
    {
        Label_NOMBRE_TRABAJADOR.Text = NOMBRE_TRABAJADOR;
        Label_NUM_DOC_IDENTIDAD.Text = NUM_DOC_IDENTIDAD;
        Label_RAZ_SOCIAL.Text = RAZ_SOCIAL;
        Label_CARGO.Text = ID_OCUPACION + "-" + CARGO;
    }

    private void cargar_DropDownList_ENTIDAD_ARP()
    {
        DropDownList_ENTIDAD_ARP.Items.Clear();

        arp _arp = new arp(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaRegionales = _arp.ObtenerTodasLasARP();

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_ENTIDAD_ARP.Items.Add(item);

        foreach (DataRow fila in tablaRegionales.Rows)
        {
            item = new ListItem(fila["NOM_ENTIDAD"].ToString(), fila["ID_ARP"].ToString());
            DropDownList_ENTIDAD_ARP.Items.Add(item);
        }

        DropDownList_ENTIDAD_ARP.DataBind();
        DropDownList_ENTIDAD_ARP.Enabled = true;
    }

    private void cargar_DropDownList_ENTIDAD_EPS()
    {
        DropDownList_ENTIDAD_EPS.Items.Clear();

        eps _eps = new eps(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaRegionales = _eps.ObtenerTodasLasEPS();

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_ENTIDAD_EPS.Items.Add(item);

        foreach (DataRow fila in tablaRegionales.Rows)
        {
            item = new ListItem(fila["NOM_ENTIDAD"].ToString(), fila["ID_EPS"].ToString());
            DropDownList_ENTIDAD_EPS.Items.Add(item);
        }

        DropDownList_ENTIDAD_EPS.DataBind();
        DropDownList_ENTIDAD_EPS.Enabled = true;
    }

    private void cargar_DropDownList_ENTIDAD_CAJA()
    {
        DropDownList_ENTIDAD_Caja.Items.Clear();

        cajaCompensacionFamiliar _ccf = new cajaCompensacionFamiliar(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaRegionales = _ccf.ObtenerTodasLasCajasCompensacionFamiliar();

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_ENTIDAD_Caja.Items.Add(item);

        foreach (DataRow fila in tablaRegionales.Rows)
        {
            item = new ListItem(fila["NOM_ENTIDAD"].ToString(), fila["ID_CAJA_C"].ToString());
            DropDownList_ENTIDAD_Caja.Items.Add(item);
        }

        DropDownList_ENTIDAD_Caja.DataBind();
        DropDownList_ENTIDAD_Caja.Enabled = true;
    }

    private void cargar_DropDownList_ENTIDAD_AFP()
    {
        DropDownList_AFP.Items.Clear();

        fondoPensiones _AFP = new fondoPensiones(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaRegionales = _AFP.ObtenerTodosLosFondosDePensiones();

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_AFP.Items.Add(item);

        item = new ListItem("Ninguna", "0");
        DropDownList_AFP.Items.Add(item);

        foreach (DataRow fila in tablaRegionales.Rows)
        {
            item = new ListItem(fila["NOM_ENTIDAD"].ToString(), fila["ID_F_PENSIONES"].ToString());
            DropDownList_AFP.Items.Add(item);
        }

        DropDownList_AFP.DataBind();
        DropDownList_AFP.Enabled = true;
    }

    private void inhabilitar_DropDownList_SUB_CENTRO()
    {
        DropDownList_SUB_CENTRO_TRABAJADOR.Enabled = false;
        DropDownList_SUB_CENTRO_TRABAJADOR.Items.Clear();
        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione", "");
        DropDownList_SUB_CENTRO_TRABAJADOR.Items.Add(item);
        DropDownList_SUB_CENTRO_TRABAJADOR.DataBind();
    }

    private void inhabilitar_DropDownList_CENTRO_COSTO()
    {
        DropDownList_CC_TRABAJADOR.Enabled = false;
        DropDownList_CC_TRABAJADOR.Items.Clear();
        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione", "");
        DropDownList_CC_TRABAJADOR.Items.Add(item);
        DropDownList_CC_TRABAJADOR.DataBind();
    }

    private void inhabilitar_DropDownList_CIUDAD_TRABAJADOR()
    {
        DropDownList_CIUDAD_TRABAJADOR.Enabled = false;
        DropDownList_CIUDAD_TRABAJADOR.Items.Clear();
        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione", "");
        DropDownList_CIUDAD_TRABAJADOR.Items.Add(item);
        DropDownList_CIUDAD_TRABAJADOR.DataBind();
    }

    private void limpiar_DropDownList_ENTIDAD_ARP()
    {
        DropDownList_ENTIDAD_ARP.Items.Clear();
        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_ENTIDAD_ARP.Items.Add(item);
        DropDownList_ENTIDAD_ARP.DataBind();
    }

    private void limpiar_DropDownList_ENTIDAD_EPS()
    {
        DropDownList_ENTIDAD_EPS.Items.Clear();
        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_ENTIDAD_EPS.Items.Add(item);
        DropDownList_ENTIDAD_EPS.DataBind();
    }

    private void limpiar_DropDownList_ENTIDAD_Caja()
    {
        DropDownList_ENTIDAD_Caja.Items.Clear();
        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_ENTIDAD_Caja.Items.Add(item);
        DropDownList_ENTIDAD_Caja.DataBind();
    }

    private void limpiar_DropDownList(DropDownList drop)
    {
        drop.Items.Clear();
        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        drop.Items.Add(item);
        drop.DataBind();
    }

    private void limpiar_DropDownList_AFP()
    {
        DropDownList_AFP.Items.Clear();
        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_AFP.Items.Add(item);
        DropDownList_AFP.DataBind();
    }

    private void colorear_indicadores_de_ubicacion(Boolean ciudad, Boolean centro_c, Boolean sub_c, System.Drawing.Color color)
    {
        Label_CIUDAD_SELECCIONADA.BackColor = System.Drawing.Color.Transparent;
        Label_CC_SELECCIONADO.BackColor = System.Drawing.Color.Transparent;
        Label_SUBC_SELECCIONADO.BackColor = System.Drawing.Color.Transparent;

        if (ciudad == true)
        {
            Label_CIUDAD_SELECCIONADA.BackColor = color;
        }

        if (centro_c == true)
        {
            Label_CC_SELECCIONADO.BackColor = color;
        }

        if (sub_c == true)
        {
            Label_SUBC_SELECCIONADO.BackColor = color;
        }
    }

    private void CargarSeccionUbicacionTrabajador()
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        cargar_DropDownList_CIUDAD(ID_EMPRESA);
        inhabilitar_DropDownList_CENTRO_COSTO();
        inhabilitar_DropDownList_SUB_CENTRO();

        colorear_indicadores_de_ubicacion(false, false, false, System.Drawing.Color.Transparent);
        HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";
    }

    private void cargar_DropDownList_SUB_CENTRO(Decimal ID_EMPRESA, Decimal ID_CENTRO_C)
    {
        DropDownList_SUB_CENTRO_TRABAJADOR.Items.Clear();

        subCentroCosto _subCentroCosto = new subCentroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaSUB_C = _subCentroCosto.ObtenerSubCentrosDeCostoPorIdEmpresaIdCentroCosto(ID_EMPRESA, ID_CENTRO_C);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione", "");
        DropDownList_SUB_CENTRO_TRABAJADOR.Items.Add(item);

        foreach (DataRow fila in tablaSUB_C.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOM_SUB_C"].ToString(), fila["ID_SUB_C"].ToString());
            DropDownList_SUB_CENTRO_TRABAJADOR.Items.Add(item);
        }

        DropDownList_SUB_CENTRO_TRABAJADOR.DataBind();
    }

    private void cargar_DropDownList_CENTRO_COSTO(Decimal ID_EMPRESA, String ID_CIUDAD)
    {
        DropDownList_CC_TRABAJADOR.Items.Clear();

        centroCosto _centroCosto = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaCCCiudad = _centroCosto.ObtenerCentrosDeCostoPorIdEmpresaIdCiudad(ID_EMPRESA, ID_CIUDAD);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione", "");
        DropDownList_CC_TRABAJADOR.Items.Add(item);

        foreach (DataRow fila in tablaCCCiudad.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOM_CC"].ToString(), fila["ID_CENTRO_C"].ToString());
            DropDownList_CC_TRABAJADOR.Items.Add(item);
        }

        DropDownList_CC_TRABAJADOR.DataBind();
    }

    private void cargar_DropDownList_CIUDAD(Decimal ID_EMPRESA)
    {
        DropDownList_CIUDAD_TRABAJADOR.Items.Clear();

        cobertura _cobertura = new cobertura(Session["idEmpresa"].ToString());
        DataTable tablaCoberturaEmpresa = _cobertura.obtenerCoberturaDeUnCliente(ID_EMPRESA);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_CIUDAD_TRABAJADOR.Items.Add(item);

        foreach (DataRow fila in tablaCoberturaEmpresa.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["Ciudad"].ToString(), fila["Código Ciudad"].ToString());
            DropDownList_CIUDAD_TRABAJADOR.Items.Add(item);
        }

        DropDownList_CIUDAD_TRABAJADOR.DataBind();
    }

    private void CargarUbicacionTrabajadorSegunTemporal(DataRow filaContratoTemporal)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        Decimal ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);

        condicionesContratacion _condicionesContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        if (DBNull.Value.Equals(filaContratoTemporal["ID_SUB_C"]) == false)
        {
            Decimal ID_SUB_C = Convert.ToDecimal(filaContratoTemporal["ID_SUB_C"]);

            subCentroCosto _sub = new subCentroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaSubCentro = _sub.ObtenerSubCentrosDeCostoPorIdSubCosto(ID_SUB_C);
            DataRow filaSub = tablaSubCentro.Rows[0];

            Decimal ID_CENTRO_C = Convert.ToDecimal(filaSub["ID_CENTRO_C"]);

            centroCosto _centro = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaCentro = _centro.ObtenerCentrosDeCostoPorIdCentroCosto(ID_CENTRO_C);
            DataRow filaCentro = tablaCentro.Rows[0];

            String ID_CIUDAD = filaCentro["ID_CIUDAD"].ToString().Trim();

            cargar_DropDownList_SUB_CENTRO(ID_EMPRESA, ID_CENTRO_C);
            DropDownList_SUB_CENTRO_TRABAJADOR.SelectedValue = ID_SUB_C.ToString();
            DropDownList_SUB_CENTRO_TRABAJADOR.Enabled = true;

            cargar_DropDownList_CENTRO_COSTO(ID_EMPRESA, ID_CIUDAD);
            DropDownList_CC_TRABAJADOR.SelectedValue = ID_CENTRO_C.ToString();
            DropDownList_CC_TRABAJADOR.Enabled = true;

            cargar_DropDownList_CIUDAD(ID_EMPRESA);
            DropDownList_CIUDAD_TRABAJADOR.SelectedValue = ID_CIUDAD;
            DropDownList_CIUDAD_TRABAJADOR.Enabled = true;

            DataTable tablaCondicionContratacion = _condicionesContratacion.ObtenerCondicionComercialPorIdPerfilIdSubC(ID_PERFIL, ID_SUB_C);

            if (tablaCondicionContratacion.Rows.Count <= 0)
            {
                colorear_indicadores_de_ubicacion(false, false, true, System.Drawing.Color.Red);
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";
            }
            else
            {
                colorear_indicadores_de_ubicacion(false, false, true, System.Drawing.Color.Green);
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "S";
            }
        }
        else
        {
            if (DBNull.Value.Equals(filaContratoTemporal["ID_CENTRO_C"]) == false)
            {
                Decimal ID_CENTRO_C = Convert.ToDecimal(filaContratoTemporal["ID_CENTRO_C"]);

                centroCosto _centro = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                DataTable tablaCentro = _centro.ObtenerCentrosDeCostoPorIdCentroCosto(ID_CENTRO_C);
                DataRow filaCentro = tablaCentro.Rows[0];

                String ID_CIUDAD = filaCentro["ID_CIUDAD"].ToString().Trim();

                inhabilitar_DropDownList_SUB_CENTRO();
                DropDownList_SUB_CENTRO_TRABAJADOR.Enabled = true;

                cargar_DropDownList_CENTRO_COSTO(ID_EMPRESA, ID_CIUDAD);
                DropDownList_CC_TRABAJADOR.SelectedValue = ID_CENTRO_C.ToString();
                DropDownList_CC_TRABAJADOR.Enabled = true;

                cargar_DropDownList_CIUDAD(ID_EMPRESA);
                DropDownList_CIUDAD_TRABAJADOR.SelectedValue = ID_CIUDAD;
                DropDownList_CIUDAD_TRABAJADOR.Enabled = true;

                DataTable tablaCondicionContratacion = _condicionesContratacion.ObtenerCondicionComercialPorIdPerfilIdCentroC(ID_PERFIL, ID_CENTRO_C);

                if (tablaCondicionContratacion.Rows.Count <= 0)
                {
                    colorear_indicadores_de_ubicacion(false, true, false, System.Drawing.Color.Red);
                    HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";
                }
                else
                {
                    colorear_indicadores_de_ubicacion(false, true, false, System.Drawing.Color.Green);
                    HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "S";
                }
            }
            else
            {
                String ID_CIUDAD = filaContratoTemporal["ID_CIUDAD"].ToString().Trim();

                inhabilitar_DropDownList_SUB_CENTRO();
                DropDownList_SUB_CENTRO_TRABAJADOR.Enabled = true;

                inhabilitar_DropDownList_CENTRO_COSTO();
                DropDownList_CC_TRABAJADOR.Enabled = true;

                cargar_DropDownList_CIUDAD(ID_EMPRESA);
                DropDownList_CIUDAD_TRABAJADOR.SelectedValue = ID_CIUDAD;
                DropDownList_CIUDAD_TRABAJADOR.Enabled = true;

                DataTable tablaCondicionContratacion = _condicionesContratacion.ObtenerCondicionComercialPorIdPerfilIdCiudad(ID_PERFIL, ID_CIUDAD);

                if (tablaCondicionContratacion.Rows.Count <= 0)
                {
                    colorear_indicadores_de_ubicacion(true, false, false, System.Drawing.Color.Red);
                    HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";
                }
                else
                {
                    colorear_indicadores_de_ubicacion(true, false, false, System.Drawing.Color.Green);
                    HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "S";
                }
            }
        }
    }

    private void cargar_DropDownList_pensionado()
    {
        DropDownList_pensionado.Items.Clear();

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_pensionado.Items.Add(item);
        item = new ListItem("SI", "S");
        DropDownList_pensionado.Items.Add(item);
        item = new ListItem("NO", "N");
        DropDownList_pensionado.Items.Add(item);
        DropDownList_BUSCAR.DataBind();
        DropDownList_pensionado.Enabled = true;
    }

    private void cargar_GridView_ARP(String idSolicitud, String idRequerimiento)
    {
        afiliacion _AfiliacionARP = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCargos = _AfiliacionARP.ObtenerconafiliacionArpPorSolicitudRequerimiento(Convert.ToInt32(idSolicitud), Convert.ToInt32(idRequerimiento));

        if (tablaCargos.Rows.Count > 0)
        {
            GridView_ARP.DataSource = tablaCargos;
            GridView_ARP.DataBind();
        }
        else
        {
            GridView_ARP.DataSource = null;
            GridView_ARP.DataBind();
        }

        for (int i = 0; i < GridView_ARP.Rows.Count; i++)
        {
            if (i == 0)
            {
                GridView_ARP.Rows[i].BackColor = System.Drawing.Color.Green;
            }
            else
            {
                GridView_ARP.Rows[i].BackColor = System.Drawing.Color.Transparent;
            }
        }
    }

    private void cargar_GridView_EPS(String idSolicitud, String idRequerimiento)
    {
        afiliacion _AfiliacionEPS = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCargos = _AfiliacionEPS.ObtenerconafiliacionEpsPorSolicitudRequerimiento(Convert.ToInt32(idSolicitud), Convert.ToInt32(idRequerimiento));

        if (tablaCargos.Rows.Count > 0)
        {
            GridView_EPS.DataSource = tablaCargos;
            GridView_EPS.DataBind();
        }
        else
        {
            GridView_EPS.DataSource = null;
            GridView_EPS.DataBind();
        }

        for (int i = 0; i < GridView_EPS.Rows.Count; i++)
        {
            if (i == 0)
            {
                GridView_EPS.Rows[i].BackColor = System.Drawing.Color.Green;
            }
            else
            {
                GridView_EPS.Rows[i].BackColor = System.Drawing.Color.Transparent;
            }
        }
    }

    private void cargar_GridView_CCF(String idSolicitud, String idRequerimiento)
    {
        afiliacion _AfiliacionCCF = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCargos = _AfiliacionCCF.ObtenerconafiliacionCajasCPorSolicitudRequerimiento(Convert.ToInt32(idSolicitud), Convert.ToInt32(idRequerimiento));

        if (tablaCargos.Rows.Count > 0)
        {
            GridView_CAJA.DataSource = tablaCargos;
            GridView_CAJA.DataBind();
        }
        else
        {
            GridView_CAJA.DataSource = null;
            GridView_CAJA.DataBind();
        }

        for (int i = 0; i < GridView_CAJA.Rows.Count; i++)
        {
            if (i == 0)
            {
                GridView_CAJA.Rows[i].BackColor = System.Drawing.Color.Green;
            }
            else
            {
                GridView_CAJA.Rows[i].BackColor = System.Drawing.Color.Transparent;
            }
        }
    }

    private void cargar_GridView_AFP(String idSolicitud, String idRequerimiento)
    {
        afiliacion _AfiliacionAFP = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCargos = _AfiliacionAFP.ObtenerconafiliacionfpensionesPorSolicitudRequerimiento(Convert.ToInt32(idSolicitud), Convert.ToInt32(idRequerimiento));

        if (tablaCargos.Rows.Count > 0)
        {
            GridView_AFP.DataSource = tablaCargos;
            GridView_AFP.DataBind();
        }
        else
        {
            GridView_AFP.DataSource = null;
            GridView_AFP.DataBind();
        }

        for (int i = 0; i < GridView_AFP.Rows.Count; i++)
        {
            if (i == 0)
            {
                GridView_AFP.Rows[i].BackColor = System.Drawing.Color.Green;
            }
            else
            {
                GridView_AFP.Rows[i].BackColor = System.Drawing.Color.Transparent;
            }
        }
    }

    private void Cargar(Decimal ID_REQUERIMIENTO, Decimal ID_SOLICITUD, Decimal ID_EMPRESA, Decimal ID_OCUPACION)
    {
        HiddenField_ID_REQUERIMIENTO.Value = ID_REQUERIMIENTO.ToString();
        HiddenField_ID_SOLICITUD.Value = ID_SOLICITUD.ToString();
        HiddenField_ID_EMPRESA.Value = ID_EMPRESA.ToString();
        HiddenField_ID_OCUPACION.Value = ID_OCUPACION.ToString();

        radicacionHojasDeVida _solIngreso = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        _solIngreso.ActualizarEstadoProcesoRegSolicitudesIngreso(Convert.ToInt32(ID_REQUERIMIENTO), Convert.ToInt32(ID_SOLICITUD), "EN AFILIACIONES", Session["USU_LOG"].ToString());

        requisicion _requisicion = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable _tablaReq = _requisicion.ObtenerComRequerimientoPorIdRequerimiento(ID_REQUERIMIENTO);
        DataRow _filaReq = _tablaReq.Rows[0];
        String CIUDAD_REQ = _filaReq["CIUDAD_REQ"].ToString().Trim();
        HiddenField_CIUDAD_REQ.Value = CIUDAD_REQ;

        perfil _PERFIL = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaPerfil = _PERFIL.ObtenerPorRegistro(Convert.ToInt32(_filaReq["REGISTRO_PERFIL"]));
        DataRow filaPerfil = tablaPerfil.Rows[0];
        Decimal ID_PERFIL = Convert.ToDecimal(_filaReq["REGISTRO_PERFIL"]);
        ID_OCUPACION = Convert.ToDecimal(filaPerfil["ID_OCUPACION"]);
        if (!string.IsNullOrEmpty(filaPerfil["CODIGO"].ToString())) Label_RIESGO.Text = filaPerfil["CODIGO"].ToString();
        HiddenField_ID_PERFIL.Value = ID_PERFIL.ToString();
        HiddenField_ID_OCUPACION.Value = ID_OCUPACION.ToString();

        DataTable tablasol = _solIngreso.ObtenerRegSolicitudesingresoPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));
        DataRow filaSolIngreso = tablasol.Rows[0];
        String NOMBRE_TRABAJADOR = filaSolIngreso["NOMBRES"].ToString().Trim() + " " + filaSolIngreso["APELLIDOS"].ToString().Trim();
        String NUM_DOC_IDENTIDAD_COMPLETO = filaSolIngreso["TIP_DOC_IDENTIDAD"].ToString().Trim() + " " + filaSolIngreso["NUM_DOC_IDENTIDAD"].ToString().Trim();
        String NUM_DOC_IDENTIDAD = filaSolIngreso["NUM_DOC_IDENTIDAD"].ToString().Trim();
        HiddenField_NUM_DOC_IDENTIDAD.Value = NUM_DOC_IDENTIDAD;

        cliente _empresa = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaEmpresa = _empresa.ObtenerEmpresaConIdEmpresa(ID_EMPRESA);
        DataRow filaEmpresa = tablaEmpresa.Rows[0];
        String RAZ_SOCIAL = filaEmpresa["RAZ_SOCIAL"].ToString().Trim();
        HiddenField_ID_EMPRESA.Value = filaEmpresa["ID_EMPRESA"].ToString().Trim();

        cargo _cargo = new cargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaOcupacion = _cargo.ObtenerOcupacionPorIdOcupacion(ID_OCUPACION);
        DataRow filaOcupacion = tablaOcupacion.Rows[0];
        String CARGO = filaOcupacion["NOM_OCUPACION"].ToString().Trim();

        HiddenField_persona.Value = ID_SOLICITUD.ToString() + "," + ID_REQUERIMIENTO.ToString() + "," + ID_OCUPACION.ToString() + "," + ID_EMPRESA.ToString() + "," + NUM_DOC_IDENTIDAD.Trim();


        cargarDatosTrabajador(NOMBRE_TRABAJADOR, NUM_DOC_IDENTIDAD_COMPLETO, RAZ_SOCIAL, CARGO, ID_OCUPACION.ToString());

        ConRegContratoTemporal _contratoTemporal = new ConRegContratoTemporal(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaCOntratoTemporal = _contratoTemporal.ObtenerConRegContratosTemporalPorIdRequerimientoIdSolicitud(ID_REQUERIMIENTO, ID_SOLICITUD);

        Panel_UbicacionTrabajador.Visible = true;

        if (tablaCOntratoTemporal.Rows.Count <= 0)
        {
            if (_contratoTemporal.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _contratoTemporal.MensajeError, Proceso.Error);
            }

            CargarSeccionUbicacionTrabajador();

            Panel_INFO_ADICIONAL_MODULO.Visible = true;
            Label_INFO_ADICIONAL_MODULO.Text = "SELECCIONE LA UBICACIÓN DONDE LABORARÁ EL TRABAJADOR, PARA PODER CONTINUAR CON LAS AFILIACIONES";
        }
        else
        {
            DataRow filaContratoTemporal = tablaCOntratoTemporal.Rows[0];
            CargarUbicacionTrabajadorSegunTemporal(filaContratoTemporal);
            Panel_INFO_ADICIONAL_MODULO.Visible = true;
            Label_INFO_ADICIONAL_MODULO.Text = "DILIGENCIAR AFILIACIONES DEL TRABAJADOR";
        }

        limpiar_DropDownList_ENTIDAD_ARP();
        limpiar_DropDownList_ENTIDAD_EPS();
        limpiar_DropDownList_ENTIDAD_Caja();
        limpiar_DropDownList(DropDownList_DepartamentoCajaC);
        limpiar_DropDownList(DropDownList_CiudadCajaC);
        limpiar_DropDownList_AFP();

        cargar_DropDownList_pensionado();

        if (HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value == "S")
        {
            Panel_ARP.Visible = true;
            Panel_EPS.Visible = true;
            Panel_CCF.Visible = true;
            Panel_AFP.Visible = true;

            cargar_DropDownList_ENTIDAD_AFP();
            cargar_DropDownList_ENTIDAD_ARP();
            cargar_DropDownList_ENTIDAD_CAJA();
            cargar_DropDownList_ENTIDAD_EPS();
        }

        cargar_GridView_AFP(ID_SOLICITUD.ToString(), ID_REQUERIMIENTO.ToString());
        cargar_GridView_ARP(ID_SOLICITUD.ToString(), ID_REQUERIMIENTO.ToString());
        cargar_GridView_CCF(ID_SOLICITUD.ToString(), ID_REQUERIMIENTO.ToString());
        cargar_GridView_EPS(ID_SOLICITUD.ToString(), ID_REQUERIMIENTO.ToString());


        TextBox_ARP_OBSERVACIONES.Text = "";
        TextBox_COMENTARIOS_AFP.Text = "";
        TextBox_COMENTARIOS_EPS.Text = "";
        TextBox_observaciones_Caja.Text = "";

        Panel_Registro_CCF.Visible = true;
        Panel_registro_EPS.Visible = true;
        Panel_registros_afp.Visible = true;
        Panel_registros_ARP.Visible = true;

        if (GridView_ARP.Rows.Count > 0)
        {
            Panel_registros_ARP.Visible = false;
        }
        if (GridView_AFP.Rows.Count > 0)
        {
            Panel_registros_afp.Visible = false;
        }
        if (GridView_CAJA.Rows.Count > 0)
        {
            Panel_Registro_CCF.Visible = false;
        }
        if (GridView_EPS.Rows.Count > 0)
        {
            Panel_registro_EPS.Visible = false;
        }
    }

    private void direccionado_desde_hoja_de_trabajo(Decimal ID_EMPRESA, Decimal ID_SOLICITUD, Decimal ID_REQUERIMIENTO, Decimal ID_OCUPACION, String NUM_DOC_IDENTIDAD)
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Cargar);
        Cargar(ID_REQUERIMIENTO, ID_SOLICITUD, ID_EMPRESA, ID_OCUPACION);

        Cargar(Acciones.Inicio);
    }

    protected void Iniciar()
    {
        Configurar();

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        if ((String.IsNullOrEmpty(QueryStringSeguro["empresa"]) == false) && (String.IsNullOrEmpty(QueryStringSeguro["solicitud"]) == false) && (String.IsNullOrEmpty(QueryStringSeguro["requerimiento"]) == false) && (String.IsNullOrEmpty(QueryStringSeguro["docID"]) == false))
        {
            Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["empresa"]);
            Decimal ID_SOLICITUD = Convert.ToDecimal(QueryStringSeguro["solicitud"]);
            Decimal ID_REQUERIMIENTO = Convert.ToDecimal(QueryStringSeguro["requerimiento"]);
            Decimal ID_OCUPACION = Convert.ToDecimal(QueryStringSeguro["cargo"]);
            String NUM_DOC_IDENTIDAD = QueryStringSeguro["docID"].ToString().Trim();

            HiddenField_ID_EMPRESA.Value = ID_EMPRESA.ToString();
            HiddenField_ID_SOLICITUD.Value = ID_SOLICITUD.ToString();
            HiddenField_ID_REQUERIMIENTO.Value = ID_REQUERIMIENTO.ToString();
            HiddenField_ID_OCUPACION.Value = ID_OCUPACION.ToString();
            HiddenField_NUM_DOC_IDENTIDAD.Value = NUM_DOC_IDENTIDAD;

            direccionado_desde_hoja_de_trabajo(ID_EMPRESA, ID_SOLICITUD, ID_REQUERIMIENTO, ID_OCUPACION, NUM_DOC_IDENTIDAD);
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);

            Panel_INFO_ADICIONAL_MODULO.Visible = true;
            Label_INFO_ADICIONAL_MODULO.Text = "UTILICE EL BUSCADOR PARA ENCONTRAR A QUIEN SE LE DESEA REALIZAR EL CONTRATO";
        }
    }

    private void CargarQueryString()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        HiddenField_ID_EMPRESA.Value = QueryStringSeguro["empresa"];
        HiddenField_ID_SOLICITUD.Value = QueryStringSeguro["solicitud"];
        HiddenField_ID_REQUERIMIENTO.Value = QueryStringSeguro["requerimiento"];
        HiddenField_ID_OCUPACION.Value = QueryStringSeguro["cargo"];
        HiddenField_NUM_DOC_IDENTIDAD.Value = QueryStringSeguro["docID"];

        HiddenField_persona.Value = QueryStringSeguro["persona"];
    }

    private void Cargar_GridView_RESULTADOS_BUSQUEDA(DataTable tablaResultadosBusqueda)
    {
        GridView_RESULTADOS_BUSQUEDA.DataSource = tablaResultadosBusqueda;
        GridView_RESULTADOS_BUSQUEDA.DataBind();

        DataRow filaParaColocarColor;
        int contadorAlertasBajas = 0;
        int contadorAlertasMedias = 0;
        int contadorAlertasAltas = 0;
        int contadorAlertasNinguna = 0;

        for (int i = 0; i < GridView_RESULTADOS_BUSQUEDA.Rows.Count; i++)
        {
            filaParaColocarColor = tablaResultadosBusqueda.Rows[(GridView_RESULTADOS_BUSQUEDA.PageIndex * GridView_RESULTADOS_BUSQUEDA.PageSize) + i];

            if (filaParaColocarColor["ALERTA"].ToString().Trim() == "ALTA")
            {
                GridView_RESULTADOS_BUSQUEDA.Rows[i].BackColor = System.Drawing.Color.Red;
                contadorAlertasAltas += 1;
            }
            else
            {
                if (filaParaColocarColor["ALERTA"].ToString().Trim() == "MEDIA")
                {
                    GridView_RESULTADOS_BUSQUEDA.Rows[i].BackColor = System.Drawing.Color.Yellow;
                    contadorAlertasMedias += 1;
                }
                else
                {
                    if (filaParaColocarColor["ALERTA"].ToString().Trim() == "BAJA")
                    {
                        GridView_RESULTADOS_BUSQUEDA.Rows[i].BackColor = System.Drawing.Color.Green;
                        contadorAlertasBajas += 1;
                    }
                    else
                    {
                        GridView_RESULTADOS_BUSQUEDA.Rows[i].BackColor = System.Drawing.Color.Gray;
                        contadorAlertasNinguna += 1;
                    }
                }
            }

            if (i == (GridView_RESULTADOS_BUSQUEDA.Rows.Count - 1))
            {
                GridView_RESULTADOS_BUSQUEDA.Rows[i].Cells[1].Text = "";
            }
        }

        Label_ALERTA_ALTA.Text = contadorAlertasAltas.ToString();
        Label_ALERTA_MEDIA.Text = contadorAlertasMedias.ToString();
        Label_ALERTA_BAJA.Text = contadorAlertasBajas.ToString();
    }

    private void Buscar()
    {
        Ocultar(Acciones.Inicio);

        String datosCapturados = HiddenField_FILTRO_DATO.Value;
        String campo = HiddenField_FILTRO_DROP.Value;

        requisicion _requicision = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaResultadosBusqueda = new DataTable();

        if (campo == "NUM_DOC_IDENTIFICACION")
        {
            tablaResultadosBusqueda = _requicision.ObtenerPersonasPorContratarPorNumDocIdentificacion(datosCapturados);
        }
        else if (campo == "NOMBRES")
        {
            tablaResultadosBusqueda = _requicision.ObtenerPersonasPorContratarPorNombres(datosCapturados);
        }
        else if (campo == "APELLIDOS")
        {
            tablaResultadosBusqueda = _requicision.ObtenerPersonasPorContratarPorApellidos(datosCapturados);
        }
        else if (campo == "RAZ_SOCIAL")
        {
            tablaResultadosBusqueda = _requicision.ObtenerPersonasPorContratarPorEmpresa(datosCapturados);
        }

        if (tablaResultadosBusqueda.Rows.Count <= 0)
        {
            if (_requicision.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _requicision.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron resultados para a busqueda realizada.", Proceso.Error);
            }
        }
        else
        {
            Mostrar(Acciones.Buscar);
            Cargar_GridView_RESULTADOS_BUSQUEDA(tablaResultadosBusqueda);
        }
    }

    private void ocultar_mensaje(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.Style.Add("display", "none");
        panel_mensaje.Style.Add("display", "none");

        panel_fondo.Visible = false;
        panel_mensaje.Visible = false;
    }

    private void ObtenerVariablesUbicacionGlobales()
    {
        if (DropDownList_SUB_CENTRO_TRABAJADOR.SelectedIndex > 0)
        {
            GLO_ID_SUB_C = Convert.ToDecimal(DropDownList_SUB_CENTRO_TRABAJADOR.SelectedValue);
            GLO_ID_CENTRO_C = 0;
            GLO_ID_CIUDAD = null;
        }
        else
        {
            if (DropDownList_CC_TRABAJADOR.SelectedIndex > 0)
            {
                GLO_ID_SUB_C = 0;
                GLO_ID_CENTRO_C = Convert.ToDecimal(DropDownList_CC_TRABAJADOR.SelectedValue);
                GLO_ID_CIUDAD = null;
            }
            else
            {
                GLO_ID_SUB_C = 0;
                GLO_ID_CENTRO_C = 0;
                GLO_ID_CIUDAD = DropDownList_CIUDAD_TRABAJADOR.SelectedValue.Trim();
            }
        }
    }

    private Boolean ActualizarContratoTemporal(Decimal ID_REQUERIMIENTO, Decimal ID_SOLICITUD)
    {
        Boolean resultado = true;

        ObtenerVariablesUbicacionGlobales();

        ConRegContratoTemporal _contratoTemporal = new ConRegContratoTemporal(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaTemporal = _contratoTemporal.ObtenerConRegContratosTemporalPorIdRequerimientoIdSolicitud(ID_REQUERIMIENTO, ID_SOLICITUD);

        if (tablaTemporal.Rows.Count <= 0)
        {
            if (_contratoTemporal.MensajeError != null)
            {
                resultado = false;
                Informar(Panel_FONDO_MENSAJE_ARP, Image_MENSAJE_ARP_POPUP, Panel_MENSAJES_ARP, Label_MENSAJE_ARP, _contratoTemporal.MensajeError, Proceso.Error);
            }
            else
            {
                Decimal ID_TEMPORAL = _contratoTemporal.AdicionarConRegContratoTemporal(ID_REQUERIMIENTO, ID_SOLICITUD, GLO_ID_CIUDAD, GLO_ID_CENTRO_C, GLO_ID_SUB_C, 0, Convert.ToDecimal(HiddenField_ID_EMPRESA.Value), false);

                if (ID_TEMPORAL <= 0)
                {
                    resultado = false;
                    Informar(Panel_FONDO_MENSAJE_ARP, Image_MENSAJE_ARP_POPUP, Panel_MENSAJES_ARP, Label_MENSAJE_ARP, _contratoTemporal.MensajeError, Proceso.Error);
                }
            }
        }
        else
        {
            DataRow filaTemporal = tablaTemporal.Rows[0];

            Boolean TIENE_CUENTA = false;
            if (filaTemporal["TIENE_CUENTA"].ToString().Trim() == "True")
            {
                TIENE_CUENTA = true;
            }

            Decimal ID_TEMPORAL = _contratoTemporal.AdicionarConRegContratoTemporal(ID_REQUERIMIENTO, ID_SOLICITUD, GLO_ID_CIUDAD, GLO_ID_CENTRO_C, GLO_ID_SUB_C, 0, Convert.ToDecimal(HiddenField_ID_EMPRESA.Value), TIENE_CUENTA);

            if (ID_TEMPORAL <= 0)
            {
                resultado = false;
                Informar(Panel_FONDO_MENSAJE_ARP, Image_MENSAJE_ARP_POPUP, Panel_MENSAJES_ARP, Label_MENSAJE_ARP, _contratoTemporal.MensajeError, Proceso.Error);
            }
        }

        return resultado;
    }

    private bool Actualizar(afiliacion.Entidades entidad)
    {
        bool actualizado = true;
        afiliacion Afiliacion = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        try
        {
            
            switch (entidad)
            {
                case afiliacion.Entidades.Arl:
                    if (!Afiliacion.ActualizarconafiliacionArp(Convert.ToInt32(HiddenField_id_arl.Value),
                        Convert.ToInt32(HiddenField_ID_SOLICITUD.Value),
                        Convert.ToInt32(DropDownList_ENTIDAD_ARP.SelectedValue),
                        Convert.ToDateTime(TextBox_fecha_r.Text),
                        TextBox_ARP_OBSERVACIONES.Text,
                        Convert.ToInt32(HiddenField_ID_REQUERIMIENTO.Value))) actualizado = false;
                    break;
                case afiliacion.Entidades.Eps:
                    if (!Afiliacion.ActualizarconafiliacionEps(Convert.ToInt32(HiddenField_id_eps.Value),
                        Convert.ToInt32(HiddenField_ID_SOLICITUD.Value),
                        Convert.ToInt32(DropDownList_ENTIDAD_EPS.SelectedValue),
                        Convert.ToDateTime(TextBox_fecha_EPS.Text),
                        TextBox_COMENTARIOS_EPS.Text,
                        Convert.ToInt32(HiddenField_ID_REQUERIMIENTO.Value))) actualizado = false;
                    break;
                case afiliacion.Entidades.Afp:
                    if (!Afiliacion.Actualizarconafiliacionfpensiones(Convert.ToInt32(this.HiddenField_id_afp.Value),
                        Convert.ToInt32(HiddenField_ID_SOLICITUD.Value),
                        Convert.ToInt32(DropDownList_AFP.SelectedValue),
                        Convert.ToDateTime(TextBox_Fecha_AFP.Text),
                        TextBox_COMENTARIOS_AFP.Text,
                        DropDownList_pensionado.SelectedValue.ToString(),
                        Convert.ToInt32(HiddenField_ID_REQUERIMIENTO.Value), 
                        DropDownList_tipo_pensionado.SelectedValue,
                        TextBox_Numero_resolucion_tramite.Text)) actualizado = false;

                    break;
                case afiliacion.Entidades.Ccf:
                    if (!Afiliacion.ActualizarconafiliacionCajasC(Convert.ToInt32(HiddenField_id_ccf.Value),
                        Convert.ToInt32(HiddenField_ID_SOLICITUD.Value),
                        Convert.ToInt32(DropDownList_ENTIDAD_Caja.SelectedValue),
                        Convert.ToDateTime(TextBox_Fecha_Caja.Text),
                        TextBox_observaciones_Caja.Text,
                        Convert.ToInt32(HiddenField_ID_REQUERIMIENTO.Value), 
                        DropDownList_CiudadCajaC.SelectedValue)) actualizado = false;
                    break;
            }
        }
        catch (Exception e)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, e.Message, Proceso.Error);
        }
        return actualizado;
    }

    private void cargar_DropDownList_tipo_Pensionado()
    {
        DropDownList_tipo_pensionado.Items.Clear();
        parametro _parametro = new parametro(Session["idEmpresa"].ToString());
        DataTable tablaParametro = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_TIPO_PENSIONADO);

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_tipo_pensionado.Items.Add(item);

        foreach (DataRow fila in tablaParametro.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_tipo_pensionado.Items.Add(item);
        }
    }

    #endregion metodos

    #region eventos
    protected void DropDownList_BUSCAR_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_BUSCAR.SelectedValue == "")
        {
            configurarCaracteresAceptadosBusqueda(true, true);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "NUM_DOC_IDENTIFICACION")
        {
            configurarCaracteresAceptadosBusqueda(false, true);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "NOMBRES")
        {
            configurarCaracteresAceptadosBusqueda(true, false);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "APELLIDOS")
        {
            configurarCaracteresAceptadosBusqueda(true, false);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "RAZ_SOCIAL")
        {
            configurarCaracteresAceptadosBusqueda(true, false);
        }
        TextBox_BUSCAR.Text = "";
    }

    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "CON_FILTRO";
        HiddenField_FILTRO_DROP.Value = DropDownList_BUSCAR.SelectedValue;
        HiddenField_FILTRO_DATO.Value = TextBox_BUSCAR.Text;

        Buscar();
    }

    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    protected void GridView_RESULTADOS_BUSQUEDA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal ID_REQUERIMIENTO = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_REQUERIMIENTO"]);
            Decimal ID_SOLICITUD = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_SOLICITUD"]);
            Decimal ID_EMPRESA = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_EMPRESA"]);
            Decimal ID_OCUPACION = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_OCUPACION"]);

            Cargar(ID_REQUERIMIENTO, ID_SOLICITUD, ID_EMPRESA, ID_OCUPACION);
        }
    }

    protected void Button_CERRAR_MENSAJE_ARP_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE_ARP, Panel_MENSAJES_ARP);
    }

    protected void DropDownList_CIUDAD_TRABAJADOR_SelectedIndexChanged(object sender, EventArgs e)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        Decimal ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);

        if (DropDownList_CIUDAD_TRABAJADOR.SelectedIndex <= 0)
        {
            colorear_indicadores_de_ubicacion(false, false, false, System.Drawing.Color.Transparent);
            HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";

            inhabilitar_DropDownList_CENTRO_COSTO();
            inhabilitar_DropDownList_SUB_CENTRO();
        }
        else
        {
            condicionesContratacion _condicionesContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            String ID_CIUDAD = DropDownList_CIUDAD_TRABAJADOR.SelectedValue;

            DataTable tablaCondicionContratacion = _condicionesContratacion.ObtenerCondicionComercialPorIdPerfilIdCiudad(ID_PERFIL, ID_CIUDAD);

            if (tablaCondicionContratacion.Rows.Count <= 0)
            {
                colorear_indicadores_de_ubicacion(true, false, false, System.Drawing.Color.Red);
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";
            }
            else
            {
                colorear_indicadores_de_ubicacion(true, false, false, System.Drawing.Color.Green);
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "S";
            }

            cargar_DropDownList_CENTRO_COSTO(ID_EMPRESA, ID_CIUDAD);
            DropDownList_CC_TRABAJADOR.Enabled = true;
            inhabilitar_DropDownList_SUB_CENTRO();
        }

        if (HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value == "N")
        {
            Panel_ARP.Visible = false;
            Panel_EPS.Visible = false;
            Panel_CCF.Visible = false;
            Panel_AFP.Visible = false;

            limpiar_DropDownList_AFP();
            limpiar_DropDownList_ENTIDAD_ARP();
            limpiar_DropDownList_ENTIDAD_Caja();
            limpiar_DropDownList_ENTIDAD_EPS();
        }
        else
        {
            Panel_ARP.Visible = true;
            Panel_EPS.Visible = true;
            Panel_CCF.Visible = true;
            Panel_AFP.Visible = true;

            cargar_DropDownList_ENTIDAD_AFP();
            cargar_DropDownList_ENTIDAD_ARP();
            cargar_DropDownList_ENTIDAD_CAJA();
            cargar_DropDownList_ENTIDAD_EPS();
        }
    }

    protected void DropDownList_CC_TRABAJADOR_SelectedIndexChanged(object sender, EventArgs e)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        Decimal ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);
        String ID_CIUDAD = DropDownList_CIUDAD_TRABAJADOR.SelectedValue;

        condicionesContratacion _condicionesContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        if (DropDownList_CC_TRABAJADOR.SelectedIndex <= 0)
        {
            DataTable tablaCondicionContratacion = _condicionesContratacion.ObtenerCondicionComercialPorIdPerfilIdCiudad(ID_PERFIL, ID_CIUDAD);

            if (tablaCondicionContratacion.Rows.Count <= 0)
            {
                colorear_indicadores_de_ubicacion(true, false, false, System.Drawing.Color.Red);
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";
            }
            else
            {
                colorear_indicadores_de_ubicacion(true, false, false, System.Drawing.Color.Green);
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "S";
            }

            inhabilitar_DropDownList_SUB_CENTRO();
        }
        else
        {
            Decimal ID_CENTRO_C = Convert.ToDecimal(DropDownList_CC_TRABAJADOR.SelectedValue);

            DataTable tablaCondicionContratacion = _condicionesContratacion.ObtenerCondicionComercialPorIdPerfilIdCentroC(ID_PERFIL, ID_CENTRO_C);

            if (tablaCondicionContratacion.Rows.Count <= 0)
            {
                colorear_indicadores_de_ubicacion(false, true, false, System.Drawing.Color.Red);
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";
            }
            else
            {
                colorear_indicadores_de_ubicacion(false, true, false, System.Drawing.Color.Green);
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "S";
            }

            cargar_DropDownList_SUB_CENTRO(ID_EMPRESA, ID_CENTRO_C);
            DropDownList_SUB_CENTRO_TRABAJADOR.Enabled = true;
        }

        if (HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value == "N")
        {
            Panel_ARP.Visible = false;
            Panel_EPS.Visible = false;
            Panel_CCF.Visible = false;
            Panel_AFP.Visible = false;

            limpiar_DropDownList_AFP();
            limpiar_DropDownList_ENTIDAD_ARP();
            limpiar_DropDownList_ENTIDAD_Caja();
            limpiar_DropDownList_ENTIDAD_EPS();
        }
        else
        {
            Panel_ARP.Visible = true;
            Panel_EPS.Visible = true;
            Panel_CCF.Visible = true;
            Panel_AFP.Visible = true;
        }
    }

    protected void DropDownList_SUB_CENTRO_TRABAJADOR_SelectedIndexChanged(object sender, EventArgs e)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        Decimal ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);
        String ID_CIUDAD = DropDownList_CIUDAD_TRABAJADOR.SelectedValue;
        Decimal ID_CENTRO_C = Convert.ToDecimal(DropDownList_CC_TRABAJADOR.SelectedValue);

        condicionesContratacion _condicionesContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        if (DropDownList_SUB_CENTRO_TRABAJADOR.SelectedIndex <= 0)
        {
            DataTable tablaCondicionContratacion = _condicionesContratacion.ObtenerCondicionComercialPorIdPerfilIdCentroC(ID_PERFIL, ID_CENTRO_C);

            if (tablaCondicionContratacion.Rows.Count <= 0)
            {
                colorear_indicadores_de_ubicacion(false, true, false, System.Drawing.Color.Red);
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";
            }
            else
            {
                colorear_indicadores_de_ubicacion(false, true, false, System.Drawing.Color.Green);
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "S";
            }
        }
        else
        {
            Decimal ID_SUB_C = Convert.ToDecimal(DropDownList_SUB_CENTRO_TRABAJADOR.SelectedValue);

            DataTable tablaCondicionContratacion = _condicionesContratacion.ObtenerCondicionComercialPorIdPerfilIdSubC(ID_PERFIL, ID_SUB_C);

            if (tablaCondicionContratacion.Rows.Count <= 0)
            {
                colorear_indicadores_de_ubicacion(false, false, true, System.Drawing.Color.Red);
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";
            }
            else
            {
                colorear_indicadores_de_ubicacion(false, false, true, System.Drawing.Color.Green);
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "S";
            }
        }

        if (HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value == "N")
        {
            Panel_ARP.Visible = false;
            Panel_EPS.Visible = false;
            Panel_CCF.Visible = false;
            Panel_AFP.Visible = false;

            limpiar_DropDownList_AFP();
            limpiar_DropDownList_ENTIDAD_ARP();
            limpiar_DropDownList_ENTIDAD_Caja();
            limpiar_DropDownList_ENTIDAD_EPS();
        }
        else
        {
            Panel_ARP.Visible = true;
            Panel_EPS.Visible = true;
            Panel_CCF.Visible = true;
            Panel_AFP.Visible = true;
        }
    }

    protected void Button_GUARDAR_ARP_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(HiddenField_id_arl.Value))
        {
            if (Actualizar(afiliacion.Entidades.Arl)) Informar(Panel_FONDO_MENSAJE_ARP, Image_MENSAJE_ARP_POPUP, Panel_MENSAJES_ARP, Label_MENSAJE_ARP, "El registro ha sido actualizado correctamente.", Proceso.Correcto);
            else Informar(Panel_FONDO_MENSAJE_ARP, Image_MENSAJE_ARP_POPUP, Panel_MENSAJES_ARP, Label_MENSAJE_ARP, "El registro NO ha sido actualizado.", Proceso.Error); 
            cargar_GridView_ARP(HiddenField_ID_SOLICITUD.Value, HiddenField_ID_REQUERIMIENTO.Value);
            
            TextBox_fecha_r.Text = "";
            TextBox_fecha_r.Enabled = false;
            TextBox_ARP_OBSERVACIONES.Text = "";
            TextBox_ARP_OBSERVACIONES.Enabled = false;
            DropDownList_ENTIDAD_ARP.Enabled = false;
            Panel_registros_ARP.Visible = false;
        }
        else
        {


            int idSolicitud = 0;
            int idrequerimiento = 0;
            int idARP = 0;
            String observaciones = null;
            DateTime fecha_r;

            tools _tools = new tools();
            SecureQueryString QueryStringSeguro;
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

            idSolicitud = Convert.ToInt32(HiddenField_ID_SOLICITUD.Value);
            idrequerimiento = Convert.ToInt32(HiddenField_ID_REQUERIMIENTO.Value);
            idARP = Convert.ToInt32(DropDownList_ENTIDAD_ARP.SelectedValue);
            fecha_r = Convert.ToDateTime(TextBox_fecha_r.Text);
            observaciones = TextBox_ARP_OBSERVACIONES.Text;
            if (String.IsNullOrEmpty(observaciones))
            {
                observaciones = "Ninguna";
            }

            afiliacion _ARP = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            _ARP.AdicionarconafiliacionArp(idSolicitud, idARP, fecha_r, observaciones, idrequerimiento);

            if (_ARP.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE_ARP, Image_MENSAJE_ARP_POPUP, Panel_MENSAJES_ARP, Label_MENSAJE_ARP, _ARP.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE_ARP, Image_MENSAJE_ARP_POPUP, Panel_MENSAJES_ARP, Label_MENSAJE_ARP, "La afiliación fue adicionado correctamente.", Proceso.Correcto);
            }

            TextBox_fecha_r.Text = "";
            TextBox_fecha_r.Enabled = false;
            TextBox_ARP_OBSERVACIONES.Text = "";
            TextBox_ARP_OBSERVACIONES.Enabled = false;
            DropDownList_ENTIDAD_ARP.Enabled = false;

            cargar_GridView_ARP(idSolicitud.ToString(), idrequerimiento.ToString());

            Panel_registros_ARP.Visible = false;

            ActualizarContratoTemporal(Convert.ToDecimal(idrequerimiento), Convert.ToDecimal(idSolicitud));
        }
    }

    protected void GridView_ARP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            string REGISTRO = GridView_ARP.DataKeys[indexSeleccionado].Values["REGISTRO"].ToString();
            HiddenField_id_arl.Value = REGISTRO;
            afiliacion _ARP = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable _TablaAfiliacionARP = _ARP.ObtenerconafiliacionArpPorRegistro(Convert.ToInt32(REGISTRO));
            DataRow _filaSeleccionada = _TablaAfiliacionARP.Rows[0];

            TextBox_fecha_r.Text = Convert.ToDateTime(_filaSeleccionada["FECHA_R"]).ToShortDateString();
            TextBox_fecha_r.Enabled = false;
            cargar_DropDownList_ENTIDAD_ARP();
            DropDownList_ENTIDAD_ARP.SelectedValue = _filaSeleccionada["ID_ARP"].ToString().Trim();
            DropDownList_ENTIDAD_ARP.DataBind();
            TextBox_ARP_OBSERVACIONES.Text = _filaSeleccionada["OBSERVACIONES"].ToString().Trim();
            TextBox_ARP_OBSERVACIONES.Enabled = true;

            Panel_registros_ARP.Visible = true;
        }
    }

    protected void Button_CERRAR_MENSAJE_EPS_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE_EPS, Panel_MENSAJES_EPS);
    }

    protected void Button_GUARDAR_EPS_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(HiddenField_id_eps.Value))
        {
            if (Actualizar(afiliacion.Entidades.Eps)) Informar(Panel_FONDO_MENSAJE_EPS, Image_MENSAJE_EPS_POPUP, Panel_MENSAJES_EPS, Label_MENSAJE_EPS, "El registro ha sido actualizado correctamente.", Proceso.Correcto);
            else Informar(Panel_FONDO_MENSAJE_EPS, Image_MENSAJE_EPS_POPUP, Panel_MENSAJES_EPS, Label_MENSAJE_EPS, "El registro NO ha sido actualizado.", Proceso.Correcto);
            cargar_GridView_EPS(HiddenField_ID_SOLICITUD.Value, HiddenField_ID_REQUERIMIENTO.Value);

            TextBox_fecha_EPS.Text = "";
            TextBox_fecha_EPS.Enabled = false;
            TextBox_COMENTARIOS_EPS.Text = "";
            TextBox_COMENTARIOS_EPS.Enabled = false;
            DropDownList_ENTIDAD_EPS.Enabled = false;
            Panel_registro_EPS.Visible = false;
        }
        else
        {

        int idSolicitud = 0;
        int idRequerimiento = 0;
        int idEPS = 0;
        String observaciones = null;
        DateTime fecha_r;

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        idSolicitud = Convert.ToInt32(HiddenField_ID_SOLICITUD.Value);
        idRequerimiento = Convert.ToInt32(HiddenField_ID_REQUERIMIENTO.Value);
        idEPS = Convert.ToInt32(DropDownList_ENTIDAD_EPS.SelectedValue);
        fecha_r = Convert.ToDateTime(TextBox_fecha_EPS.Text);
        observaciones = TextBox_COMENTARIOS_EPS.Text;
        if (String.IsNullOrEmpty(observaciones))
        {
            observaciones = "Ninguna";
        }
        afiliacion _EPS = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        _EPS.AdicionarconafiliacionEps(idSolicitud, idEPS, fecha_r, observaciones, idRequerimiento);

        if (_EPS.MensajeError != null)
        {
            Informar(Panel_FONDO_MENSAJE_EPS, Image_MENSAJE_EPS_POPUP, Panel_MENSAJES_EPS, Label_MENSAJE_EPS, _EPS.MensajeError, Proceso.Error);
        }
        else
        {
            Informar(Panel_FONDO_MENSAJE_EPS, Image_MENSAJE_EPS_POPUP, Panel_MENSAJES_EPS, Label_MENSAJE_EPS, "La afiliación fue adicionado correctamente.", Proceso.Correcto);
        }

        TextBox_fecha_EPS.Text = "";
        TextBox_fecha_EPS.Enabled = false;
        TextBox_COMENTARIOS_EPS.Text = "";
        TextBox_COMENTARIOS_EPS.Enabled = false;
        DropDownList_ENTIDAD_EPS.Enabled = false;
        cargar_GridView_EPS(idSolicitud.ToString(), idRequerimiento.ToString());
        Panel_registro_EPS.Visible = false;

        ActualizarContratoTemporal(Convert.ToDecimal(idRequerimiento), Convert.ToDecimal(idSolicitud));
        }
    }

    protected void GridView_EPS_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            string REGISTRO = GridView_EPS.DataKeys[indexSeleccionado].Values["REGISTRO"].ToString();
            afiliacion _EPS = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            HiddenField_id_eps.Value = REGISTRO;
            DataTable _TablaAfiliacionEPS = _EPS.ObtenerconafiliacionEpsPorRegistro(Convert.ToInt32(REGISTRO));
            DataRow _filaSeleccionada = _TablaAfiliacionEPS.Rows[0];

            TextBox_fecha_EPS.Text = Convert.ToDateTime(_filaSeleccionada["FECHA_R"]).ToShortDateString();
            TextBox_fecha_EPS.Enabled = false;
            cargar_DropDownList_ENTIDAD_EPS();
            DropDownList_ENTIDAD_EPS.SelectedValue = _filaSeleccionada["ID_EPS"].ToString();
            DropDownList_ENTIDAD_EPS.DataBind();
            TextBox_COMENTARIOS_EPS.Text = _filaSeleccionada["OBSERVACIONES"].ToString();
            TextBox_COMENTARIOS_EPS.Enabled = true;

            Panel_registro_EPS.Visible = true;
        }
    }

    protected void Button_CERRAR_MENSAJE_CCF_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE_CCF, Panel_MENSAJES_CCF);
    }

    protected void Button_GUARDAR_CAJA_Click(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(HiddenField_id_ccf.Value))
        {
            if (Actualizar(afiliacion.Entidades.Ccf))
            {
                Informar(Panel_FONDO_MENSAJE_CCF, Image_MENSAJE_CCF_POPUP, Panel_MENSAJES_CCF, Label_MENSAJE_CCF, "El registro ha sido actualizado correctamente.", Proceso.Correcto);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE_CCF, Image_MENSAJE_CCF_POPUP, Panel_MENSAJES_CCF, Label_MENSAJE_CCF, "El registro NO ha sido actualizado.", Proceso.Error);
            }

            cargar_GridView_CCF(HiddenField_ID_SOLICITUD.Value, HiddenField_ID_REQUERIMIENTO.Value);

            TextBox_Fecha_Caja.Text = "";
            TextBox_Fecha_Caja.Enabled = false;
            TextBox_observaciones_Caja.Text = "";
            TextBox_observaciones_Caja.Enabled = false;
            DropDownList_ENTIDAD_Caja.Enabled = false;
            Panel_Registro_CCF.Visible = false;
        }
        else
        {
            int idSolicitud = 0;
            int idRequerimiento = 0;
            int idCCF = 0;
            String observaciones = null;
            DateTime fecha_r;

            tools _tools = new tools();
            SecureQueryString QueryStringSeguro;
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

            idSolicitud = Convert.ToInt32(HiddenField_ID_SOLICITUD.Value);
            idRequerimiento = Convert.ToInt32(HiddenField_ID_REQUERIMIENTO.Value);
            idCCF = Convert.ToInt32(DropDownList_ENTIDAD_Caja.SelectedValue);
            fecha_r = Convert.ToDateTime(TextBox_Fecha_Caja.Text);
            observaciones = TextBox_observaciones_Caja.Text;
            if (String.IsNullOrEmpty(observaciones))
            {
                observaciones = "Ninguna";
            }
            afiliacion _CCF = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            _CCF.AdicionarconafiliacionCajasC(idSolicitud, idCCF, fecha_r, observaciones, idRequerimiento, DropDownList_CiudadCajaC.SelectedValue);

            if (_CCF.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE_CCF, Image_MENSAJE_CCF_POPUP, Panel_MENSAJES_CCF, Label_MENSAJE_CCF, _CCF.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE_CCF, Image_MENSAJE_CCF_POPUP, Panel_MENSAJES_CCF, Label_MENSAJE_CCF, "La afiliación fue adicionado correctamente.", Proceso.Correcto);
            }

            TextBox_Fecha_Caja.Text = "";
            TextBox_Fecha_Caja.Enabled = false;
            TextBox_observaciones_Caja.Text = "";
            TextBox_observaciones_Caja.Enabled = false;
            DropDownList_ENTIDAD_Caja.Enabled = false;
            cargar_GridView_CCF(idSolicitud.ToString(), idRequerimiento.ToString());
            Panel_Registro_CCF.Visible = false;

            ActualizarContratoTemporal(Convert.ToDecimal(idRequerimiento), Convert.ToDecimal(idSolicitud));
        }
    }

    protected void GridView_CAJA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            string REGISTRO = GridView_CAJA.DataKeys[indexSeleccionado].Values["REGISTRO"].ToString();
            HiddenField_id_ccf.Value = REGISTRO;
            afiliacion _CCF = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable _TablaAfiliacionCCF = _CCF.ObtenerconafiliacionCajasCPorRegistro(Convert.ToInt32(REGISTRO));
            DataRow _filaSeleccionada = _TablaAfiliacionCCF.Rows[0];

            TextBox_Fecha_Caja.Text = Convert.ToDateTime(_filaSeleccionada["FECHA_R"]).ToShortDateString();
            TextBox_Fecha_Caja.Enabled = false;

            cargar_DropDownList_ENTIDAD_CAJA();
            DropDownList_ENTIDAD_Caja.SelectedValue = _filaSeleccionada["ID_CAJA_C"].ToString().Trim();
            DropDownList_ENTIDAD_Caja.DataBind();

            try
            {
                llenarDropDepartamentos(Convert.ToDecimal(GridView_CAJA.DataKeys[indexSeleccionado].Values["ID_CAJA_C"].ToString()));

                DropDownList_DepartamentoCajaC.SelectedValue = GridView_CAJA.DataKeys[indexSeleccionado].Values["ID_DEPARTAMENTO"].ToString();

                llenarDropCiudades(GridView_CAJA.DataKeys[indexSeleccionado].Values["ID_DEPARTAMENTO"].ToString(), Convert.ToDecimal(GridView_CAJA.DataKeys[indexSeleccionado].Values["ID_CAJA_C"].ToString()));

               DropDownList_CiudadCajaC.SelectedValue = GridView_CAJA.DataKeys[indexSeleccionado].Values["ID_CIUDAD"].ToString();
            }
            catch
            {
                llenarDropDepartamentos(Convert.ToDecimal(REGISTRO));

                DropDownList_CiudadCajaC.Items.Clear();
                DropDownList_CiudadCajaC.Items.Add(new ListItem("Seleccione...", ""));
                DropDownList_CiudadCajaC.DataBind();
            }

            TextBox_observaciones_Caja.Text = _filaSeleccionada["OBSERVACIONES"].ToString();
            TextBox_observaciones_Caja.Enabled = true;

            Panel_Registro_CCF.Visible = true;
        }
    }

    protected void Button_CERRAR_MENSAJE_AFP_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE_AFP, Panel_MENSAJES_AFP);
    }

    protected void DropDownList_pensionado_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_pensionado.SelectedValue == "S")
        {
            cargar_DropDownList_tipo_Pensionado();
            panel_tipo_pensionado.Visible = true;
            TextBox_Numero_resolucion_tramite.Text = "";
        }
        else
        {
            DropDownList_tipo_pensionado.Items.Clear();
            this.TextBox_Numero_resolucion_tramite.Text = String.Empty;
            panel_tipo_pensionado.Visible = false;
        }
    }

    protected void Button_GUARDAR_AFPClick(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(HiddenField_id_afp.Value))
        {
            if (Actualizar(afiliacion.Entidades.Afp)) Informar(Panel_FONDO_MENSAJE_AFP, Image_MENSAJE_AFP_POPUP, Panel_MENSAJES_AFP, Label_MENSAJE_AFP, "El registro ha sido actualizado correctamente.", Proceso.Correcto);
            else Informar(Panel_FONDO_MENSAJE_AFP, Image_MENSAJE_AFP_POPUP, Panel_MENSAJES_AFP, Label_MENSAJE_AFP, "El registro NO ha sido actualizado", Proceso.Error);
            cargar_GridView_AFP(HiddenField_ID_SOLICITUD.Value, HiddenField_ID_REQUERIMIENTO.Value);

            TextBox_Fecha_AFP.Text = "";
            TextBox_Fecha_AFP.Enabled = false;
            TextBox_COMENTARIOS_AFP.Text = "";
            TextBox_COMENTARIOS_AFP.Enabled = false;
            DropDownList_AFP.Enabled = false;
            DropDownList_pensionado.Enabled = false;
            Panel_registros_afp.Visible = false;
        }
        else
        {
            int idSolicitud = 0;
            int idRequerimiento = 0;
            int idAFP = 0;
            String pensionado = null;
            String observaciones = null;
            DateTime fecha_r;
            String tipo_pensionado = null;
            String numero = null;

            tools _tools = new tools();
            SecureQueryString QueryStringSeguro;
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

            idSolicitud = Convert.ToInt32(HiddenField_ID_SOLICITUD.Value);
            idRequerimiento = Convert.ToInt32(HiddenField_ID_REQUERIMIENTO.Value);
            if (DropDownList_AFP.SelectedValue.Equals(""))
            {
                idAFP = 0;
            }
            else
            {
                idAFP = Convert.ToInt32(DropDownList_AFP.SelectedValue);
            }
            fecha_r = Convert.ToDateTime(TextBox_Fecha_AFP.Text);
            observaciones = TextBox_COMENTARIOS_AFP.Text;
            pensionado = DropDownList_pensionado.SelectedValue;
            if (pensionado.Equals("S"))
            {
                tipo_pensionado = DropDownList_tipo_pensionado.SelectedValue;
                numero = TextBox_Numero_resolucion_tramite.Text;
            }


            if (String.IsNullOrEmpty(observaciones))
            {
                observaciones = "Ninguna";
            }
            afiliacion _AFP = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            _AFP.Adicionarconafiliacionfpensiones(idSolicitud, idAFP, fecha_r, observaciones, pensionado, idRequerimiento, tipo_pensionado, numero);

            if (_AFP.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE_AFP, Image_MENSAJE_AFP_POPUP, Panel_MENSAJES_AFP, Label_MENSAJE_AFP, _AFP.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE_AFP, Image_MENSAJE_AFP_POPUP, Panel_MENSAJES_AFP, Label_MENSAJE_AFP, "La afiliación fue adicionado correctamente.", Proceso.Correcto);

                TextBox_Fecha_AFP.Text = "";
                TextBox_Fecha_AFP.Enabled = false;
                TextBox_COMENTARIOS_AFP.Text = "";
                TextBox_COMENTARIOS_AFP.Enabled = false;
                DropDownList_AFP.Enabled = false;
                DropDownList_pensionado.Enabled = false;
                cargar_GridView_AFP(idSolicitud.ToString(), idRequerimiento.ToString());
                Panel_registros_afp.Visible = false;

                ActualizarContratoTemporal(Convert.ToDecimal(idRequerimiento), Convert.ToDecimal(idSolicitud));
            }
        }
    }

    protected void GridView_AFP_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            string REGISTRO = GridView_AFP.DataKeys[indexSeleccionado].Values["REGISTRO"].ToString();
            HiddenField_id_afp.Value = REGISTRO;
            afiliacion _AFP = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable _TablaAfiliacionAFP = _AFP.ObtenerconafiliacionfpensionesPorRegistro(Convert.ToInt32(REGISTRO));
            DataRow _filaSeleccionada = _TablaAfiliacionAFP.Rows[0];

            TextBox_Fecha_AFP.Text = Convert.ToDateTime(_filaSeleccionada["FECHA_R"]).ToShortDateString();
            TextBox_Fecha_AFP.Enabled = false;

            cargar_DropDownList_ENTIDAD_AFP();
            DropDownList_AFP.SelectedValue = _filaSeleccionada["ID_F_PENSIONES"].ToString();
            DropDownList_AFP.DataBind();

            TextBox_COMENTARIOS_AFP.Text = _filaSeleccionada["OBSERVACIONES"].ToString().Trim();
            TextBox_COMENTARIOS_AFP.Enabled = true;

            cargar_DropDownList_pensionado();
            try
            {
                DropDownList_pensionado.SelectedValue = _filaSeleccionada["PENSIONADO"].ToString();
            }
            catch
            {
                DropDownList_pensionado.SelectedIndex = 0;
            }
            DropDownList_pensionado.DataBind();
            DropDownList_pensionado.Enabled = true;

            if (DropDownList_pensionado.SelectedValue == "S")
            {
                cargar_DropDownList_tipo_Pensionado();
                try
                {
                    DropDownList_tipo_pensionado.SelectedValue = _filaSeleccionada["TIPO_PENSIONADO"].ToString();
                }
                catch
                {
                    DropDownList_tipo_pensionado.SelectedIndex = 0;
                }
                DropDownList_tipo_pensionado.Enabled = true;

                TextBox_Numero_resolucion_tramite.Text = _filaSeleccionada["NUMERO_RESOLUCION_TRAMITE"].ToString().Trim();
                TextBox_Numero_resolucion_tramite.Enabled = true;

                panel_tipo_pensionado.Visible = true;
            }
            else
            {
                DropDownList_tipo_pensionado.Items.Clear();
                TextBox_Numero_resolucion_tramite.Text = "";

                panel_tipo_pensionado.Visible = false;
            }

            Panel_registros_afp.Visible = true;
        }
    }

    #endregion eventos

    private void llenarDropDepartamentos(decimal registro)
    {
        DropDownList_DepartamentoCajaC.Items.Clear();

        DropDownList_DepartamentoCajaC.Items.Add(new ListItem("Seleccione...", ""));

        afiliacion _afi = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaDepartamentos = _afi.ObtenerDepartamentosCajaC(registro, "CCF");

        foreach (DataRow fila in tablaDepartamentos.Rows)
        {
            DropDownList_DepartamentoCajaC.Items.Add(new ListItem(fila["DEPARTAMENTO"].ToString(), fila["ID_DEPARTAMENTO"].ToString()));
        }

        DropDownList_DepartamentoCajaC.DataBind();
    }



    private void llenarDropCiudades(string id_departamento, Decimal registro)
    {
        DropDownList_CiudadCajaC.Items.Clear();

        DropDownList_CiudadCajaC.Items.Add(new ListItem("Seleccione...", ""));

        afiliacion _afi = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCiudades = _afi.ObtenerCiudadesCajaC(registro, "CCF", id_departamento);

        foreach (DataRow fila in tablaCiudades.Rows)
        {
            DropDownList_CiudadCajaC.Items.Add(new ListItem(fila["CIUDAD"].ToString(), fila["ID_CIUDAD"].ToString()));
        }

        DropDownList_CiudadCajaC.DataBind();
    }  


    protected void DropDownList_ENTIDAD_Caja_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_ENTIDAD_Caja.SelectedIndex > 0)
        {
            if (DropDownList_ENTIDAD_Caja.SelectedValue == "43" || DropDownList_ENTIDAD_Caja.SelectedItem.Text.ToUpper() == "SIN CAJA")
            {
                Panel_CiudadCaja.Visible = false;
                DropDownList_DepartamentoCajaC.ClearSelection();
                DropDownList_CiudadCajaC.ClearSelection();
            }
            else
            {
                Panel_CiudadCaja.Visible = true;
                llenarDropDepartamentos(Convert.ToDecimal(DropDownList_ENTIDAD_Caja.SelectedValue));
            }
        }
        else
        {
            DropDownList_DepartamentoCajaC.Items.Clear();
            DropDownList_DepartamentoCajaC.Items.Add(new ListItem("Seleccione...", ""));
            DropDownList_DepartamentoCajaC.Items.Clear();
        }

        DropDownList_CiudadCajaC.Items.Clear();
        DropDownList_CiudadCajaC.Items.Add(new ListItem("Seleccione...", ""));
        DropDownList_CiudadCajaC.Items.Clear();
    }

    protected void DropDownList_DepartamentoCajaC_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_DepartamentoCajaC.SelectedIndex > 0)
        {
            llenarDropCiudades(DropDownList_DepartamentoCajaC.SelectedValue, Convert.ToDecimal(DropDownList_ENTIDAD_Caja.SelectedValue));
        }
        else
        {
            DropDownList_CiudadCajaC.Items.Clear();
            DropDownList_CiudadCajaC.Items.Add(new ListItem("Seleccione...", ""));
            DropDownList_CiudadCajaC.Items.Clear();
        }
    }
}