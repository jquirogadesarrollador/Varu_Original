using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using Brainsbits.LLB.maestras;
using Brainsbits.LLB.comercial;
using Brainsbits.LLB;
using ICSharpCode.SharpZipLib.Zip;
using TSHAK.Components;
using Brainsbits.LLB.seguridad;

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

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }

    #region CARGAR DROPS Y GRIDS
    private void cargar_GridView_DATOS()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String reg = QueryStringSeguro["reg"].ToString();

        centroCosto _centroCosto = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCentrosCosto = _centroCosto.ObtenerCentrosDeCostoPorIdEmpresa(Convert.ToDecimal(reg));

        if (tablaCentrosCosto.Rows.Count > 0)
        {
            GridView_DATOS.DataSource = tablaCentrosCosto;
            GridView_DATOS.DataBind();
        }
        else
        { 
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "ADVERTENCIA: El cliente no posee centros de costo creados.", Proceso.Error);

            configurarBotonesDeAccion(true, false, false, false, false);
            Panel_FORM_BOTONES_1.Visible = false;

            Panel_RESULTADOS_GRID.Visible = false;

            Panel_FORMULARIO.Visible = false;
        }
    }

    private void cargar_DropDownList_CIUDAD(Decimal ID_EMPRESA)
    {
        DropDownList_CIUDAD.Items.Clear();

        cobertura _cobertura = new cobertura(Session["idEmpresa"].ToString());
        DataTable tablaCoberturaEmpresa = _cobertura.obtenerCoberturaDeUnCliente(ID_EMPRESA);

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_CIUDAD.Items.Add(item);

        foreach (DataRow fila in tablaCoberturaEmpresa.Rows)
        {
            item = new ListItem(fila["Ciudad"].ToString(), fila["Código Ciudad"].ToString());
            DropDownList_CIUDAD.Items.Add(item);
        }

        DropDownList_CIUDAD.DataBind();
    }
    private void cargar_DropDownList_PERIODO_PAGO()
    {
        DropDownList_PERIODO_PAGO.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_PERIODO_PAGO);

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_PERIODO_PAGO.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_PERIODO_PAGO.Items.Add(item);
        }
        DropDownList_PERIODO_PAGO.DataBind();
    }
    private void cargar_DropDownList_BANCO()
    {
        DropDownList_BANCO.Items.Clear();

        bancos _bancos = new bancos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaBancos = _bancos.ObtenerTodosLosBancos();

        ListItem item = new ListItem("Seleccionar", "");
        DropDownList_BANCO.Items.Add(item);

        foreach (DataRow fila in tablaBancos.Rows)
        {
            item = new ListItem(fila["NOM_BANCO"].ToString(), fila["REGISTRO"].ToString());
            DropDownList_BANCO.Items.Add(item);
        }

        DropDownList_BANCO.DataBind();
    }
    private void cargar_GridView_SUB_C()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"].ToString());
        Decimal ID_CENTRO_C = Convert.ToDecimal(QueryStringSeguro["cc"].ToString());

        subCentroCosto _subCentroCosto = new subCentroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoSubC = _subCentroCosto.ObtenerSubCentrosDeCostoPorIdEmpresaIdCentroCosto(ID_EMPRESA, ID_CENTRO_C);

        if (tablaInfoSubC.Rows.Count > 0)
        {
            Panel_SUB_CENTROS.Visible = true;

            GridView_SUB_C.DataSource = tablaInfoSubC;
            GridView_SUB_C.DataBind();
        }
        else
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El CENTRO DE COSTO no posee sub centros.", Proceso.Advertencia);
        }
    }
    #endregion

    #region CONFIGURACION DE CONTROLES
    private void configurarBotonesDeAccion(Boolean bNuevo, Boolean bModificar, Boolean bGuardar, Boolean bCancelar, Boolean bVolver)
    {
        Button_NUEVO.Visible = bNuevo;
        Button_NUEVO.Enabled = bNuevo;

        Button_MODIFICAR.Visible = bModificar;
        Button_MODIFICAR.Enabled = bModificar;

        Button_GUARDAR.Visible = bGuardar;
        Button_GUARDAR.Enabled = bGuardar;

        Button_CANCELAR.Visible = bCancelar;
        Button_CANCELAR.Enabled = bCancelar;

        Button_LISTA_CONTRATOS.Visible = bVolver;
        Button_LISTA_CONTRATOS.Enabled = bVolver;

        Button_NUEVO_1.Visible = bNuevo;
        Button_NUEVO_1.Enabled = bNuevo;

        Button_MODIFICAR_1.Visible = bModificar;
        Button_MODIFICAR_1.Enabled = bModificar;

        Button_GUARDAR_1.Visible = bGuardar;
        Button_GUARDAR_1.Enabled = bGuardar;

        Button_CANCELAR_1.Visible = bCancelar;
        Button_CANCELAR_1.Enabled = bCancelar;

        Button_VOLVER_1.Visible = bVolver;
        Button_VOLVER_1.Enabled = bVolver;
    }

    private void configurarInfoAdicionalModulo(Boolean ver, String texto)
    {
        Panel_INFO_ADICIONAL_MODULO.Visible = ver;
        Label_INFO_ADICIONAL_MODULO.Text = texto;
    }
    #endregion

    #region METODOS QUE SE EJECUTAN AL PARGAR PAGINA
    private void iniciarControlesInicial()
    {
        configurarBotonesDeAccion(true, false, false, false, false);
        Panel_FORM_BOTONES_1.Visible = false;

        Panel_FORMULARIO.Visible = false;

        Panel_CONTROL_REGISTRO.Visible = false;

        Panel_FORMULARIO.Visible = false;

        cargar_GridView_DATOS();

        Panel_SUB_CENTROS.Visible = false;
    }
    private void iniciarControlesNuevo()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

        configurarBotonesDeAccion(false, false, true, true, true);

        Panel_RESULTADOS_GRID.Visible = false;

        Panel_FORMULARIO.Visible = true;
        Panel_FORMULARIO.Enabled = true;

        Panel_CONTROL_REGISTRO.Visible = false;

        Panel_IDENTIFICADOR.Visible = false;

        cargar_DropDownList_CIUDAD(ID_EMPRESA);

        Panel_FORM_BOTONES_1.Visible = true;

        cargar_DropDownList_PERIODO_PAGO();

        cargar_DropDownList_BANCO();

        Panel_SUB_CENTROS.Visible = false;

        Panel_OcultarCC.Visible = false;
    }
    private void iniciarSubCentros()
    { 
        Panel_SUB_CENTROS.Visible = true;

        cargar_GridView_SUB_C();
    }
    private void iniciarControlesCargar(Boolean modificar)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_CENTRO_C = Convert.ToDecimal(QueryStringSeguro["cc"]);
        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

        centroCosto _centroCosto = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablainfoCC = _centroCosto.ObtenerCentrosDeCostoPorIdCentroCosto(ID_CENTRO_C);

        if (tablainfoCC.Rows.Count <= 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información del centro de costo seleccionado.",Proceso.Advertencia);

            configurarBotonesDeAccion(true, false, false, false, true);
            Panel_FORM_BOTONES_1.Visible = false;

            Panel_RESULTADOS_GRID.Visible = false;

            Panel_FORMULARIO.Visible = false;

            Panel_SUB_CENTROS.Visible = false;
        }
        else
        {
            DataRow filaInfoCC = tablainfoCC.Rows[0];

            if (modificar == true)
            {
                configurarBotonesDeAccion(false, false, true, true, true);
            }
            else
            {
                configurarBotonesDeAccion(true, true, false, false, true);
            }

            Panel_RESULTADOS_GRID.Visible = false;

            Panel_FORMULARIO.Visible = true;
            if (modificar == true)
            {
                Panel_FORMULARIO.Enabled = true;

                Panel_CONTROL_REGISTRO.Visible = false;
            }
            else
            {
                Panel_FORMULARIO.Enabled = false;

                Panel_CONTROL_REGISTRO.Visible = true;

                TextBox_USU_CRE.Text = filaInfoCC["USU_CRE"].ToString();
                try
                {
                    TextBox_FCH_CRE.Text = DateTime.Parse(filaInfoCC["FCH_CRE"].ToString()).ToShortDateString();
                    TextBox_HOR_CRE.Text = DateTime.Parse(filaInfoCC["FCH_CRE"].ToString()).ToShortTimeString();
                }
                catch
                {
                    TextBox_FCH_CRE.Text = "";
                    TextBox_HOR_CRE.Text = "";
                }
                TextBox_USU_MOD.Text = filaInfoCC["USU_MOD"].ToString();
                try
                {
                    TextBox_FCH_MOD.Text = DateTime.Parse(filaInfoCC["FCH_MOD"].ToString()).ToShortDateString();
                    TextBox_HOR_MOD.Text = DateTime.Parse(filaInfoCC["FCH_MOD"].ToString()).ToShortTimeString();
                }
                catch
                {
                    TextBox_FCH_MOD.Text = "";
                    TextBox_HOR_MOD.Text = "";
                }
            }

            Panel_IDENTIFICADOR.Visible = true;
            Panel_IDENTIFICADOR.Enabled = false;
            TextBox_REGISTRO.Text = filaInfoCC["ID_CENTRO_C"].ToString().Trim();

            Page.Header.Title += ": " + filaInfoCC["NOM_CC"].ToString().Trim();

            cargar_DropDownList_CIUDAD(ID_EMPRESA);
            DropDownList_CIUDAD.SelectedValue = filaInfoCC["ID_CIUDAD"].ToString().Trim();

            TextBox_NOMBRE_CENTRO_COSTO.Text = filaInfoCC["NOM_CC"].ToString().Trim();

            cargar_DropDownList_PERIODO_PAGO();
            DropDownList_PERIODO_PAGO.SelectedValue = filaInfoCC["TIPO_NOM"].ToString();

            cargar_DropDownList_BANCO();
            DropDownList_BANCO.SelectedValue = filaInfoCC["ID_BANCO"].ToString();

            if ((filaInfoCC["CC_EXC_IVA"].ToString() == "") || (filaInfoCC["CC_EXC_IVA"].ToString() == "N"))
            {
                CheckBox_EXCENTO_IVA.Checked = false;
            }
            else
            {
                CheckBox_EXCENTO_IVA.Checked = true;
            }

            if (modificar == true)
            {
                Panel_SUB_CENTROS.Visible = false;
            }
            else
            {
                iniciarSubCentros();
            }

            if (modificar == true)
            {
                Panel_OcultarCC.Visible = true;
            }
            else
            {
                Panel_OcultarCC.Visible = false;
            }
        }
    }
    #endregion

    private void configurar_paneles_popup(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_mensaje.CssClass = "panel_popup";
    }

    private void Configurar()
    {
        configurar_paneles_popup(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Page.Header.Title = "CENTROS DE COSTO";

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

        cliente _clienteMODULO = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoEmpresa = _clienteMODULO.ObtenerEmpresaConIdEmpresa(ID_EMPRESA);
        DataRow filaTablaInfoEmpresa = tablaInfoEmpresa.Rows[0];
        configurarInfoAdicionalModulo(true, filaTablaInfoEmpresa["RAZ_SOCIAL"].ToString());

        Panel_FORM_BOTONES_1.Visible = true;

        if (IsPostBack == false)
        {
            Configurar();
            String accion = QueryStringSeguro["accion"].ToString();

            if (accion == "inicial")
            {
                iniciarControlesInicial();
            }
            else
            {
                if (accion == "nuevo")
                {
                    iniciarControlesNuevo();
                }
                else
                {
                    if (accion == "cargarNuevo")
                    {
                        iniciarControlesCargar(false);

                        String REGISTRO = QueryStringSeguro["cc"].ToString();
                        String TIPO = QueryStringSeguro["tipo"].ToString();
                        String MENSAJE = QueryStringSeguro["mensaje"].ToString();
                        if (TIPO.ToUpper() == "ERROR")
                        {
                            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El centro de costo " + REGISTRO + " fue creado correctamente. Pero ocurrieron errores con la replica de condiciones económicas.<br>" + MENSAJE, Proceso.Advertencia);
                        }
                        else
                        {
                            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El centro de costo " + REGISTRO + " fue creado correctamente.<br>" + MENSAJE, Proceso.Correcto);
                        }
                    }
                    else
                    {
                        if (accion == "cargarModificado")
                        {
                            iniciarControlesCargar(false);

                            String REGISTRO = QueryStringSeguro["cc"].ToString();
                            String TIPO = QueryStringSeguro["tipo"].ToString();
                            String MENSAJE = QueryStringSeguro["mensaje"].ToString();
                            if (TIPO.ToUpper() == "ERROR")
                            {
                                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El centro de costo " + REGISTRO + " fue modificado correctamente. Pero ocurrieron errores con la replica de condiciones económicas.<br>" + MENSAJE, Proceso.Advertencia);
                            }
                            else
                            {
                                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El centro de costo " + REGISTRO + " fue modificado correctamente.<br>" + MENSAJE, Proceso.Correcto);
                            }
                        }
                        else
                        {
                            if (accion == "cargar")
                            {
                                iniciarControlesCargar(false);
                            }
                            else
                            {
                                if (accion == "modificar")
                                {
                                    iniciarControlesCargar(true);
                                }
                                else
                                {
                                    if (accion == "cargarSubccModificado")
                                    {
                                        iniciarControlesCargar(false);

                                        String ID_SUB_C = QueryStringSeguro["subcc"].ToString();
                                        String TIPO = QueryStringSeguro["tipo"].ToString();
                                        String MENSAJE = QueryStringSeguro["mensaje"].ToString();
                                        if (TIPO.ToUpper() == "ERROR")
                                        {
                                            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El sub centro " + ID_SUB_C + " fue modificado correctamente. Pero ocurrieron errores con la replica de condiciones económicas.<br>" + MENSAJE, Proceso.Advertencia);
                                        }
                                        else
                                        {
                                            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El sub centro " + ID_SUB_C + " fue modificado correctamente.<br>" + MENSAJE, Proceso.Correcto);
                                        }
                                    }
                                    else
                                    {
                                        if (accion == "cargarSubccNuevo")
                                        {
                                            iniciarControlesCargar(false);

                                            String ID_SUB_C = QueryStringSeguro["subcc"].ToString();
                                            String TIPO = QueryStringSeguro["tipo"].ToString();
                                            String MENSAJE = QueryStringSeguro["mensaje"].ToString();
                                            if (TIPO.ToUpper() == "ERROR")
                                            {
                                                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El sub centro " + ID_SUB_C + " fue creado correctamente. Pero ocurrieron errores con la replica de condiciones económicas.<br>" + MENSAJE, Proceso.Advertencia);
                                            }
                                            else
                                            {
                                                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El sub centro " + ID_SUB_C + " fue creado correctamente.<br>" + MENSAJE, Proceso.Correcto);
                                            }
                                        }
                                        else
                                        {
                                            if (accion == "cargarOculto")
                                            {
                                                iniciarControlesInicial();

                                                String REGISTRO = QueryStringSeguro["cc"].ToString();
                                                String TIPO = QueryStringSeguro["tipo"].ToString();
                                                String MENSAJE = QueryStringSeguro["mensaje"].ToString();
                                                if (TIPO.ToUpper() == "ERROR")
                                                {
                                                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El centro de costo " + REGISTRO + " fue ocultado correctamente. Pero ocurrieron errores con la replica de condiciones económicas.<br>" + MENSAJE, Proceso.Advertencia);
                                                }
                                                else
                                                {
                                                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El centro de costo " + REGISTRO + " fue ocultado correctamente.<br>" + MENSAJE, Proceso.Correcto);
                                                }
                                            }
                                            else
                                            {
                                                if (accion == "cargarActivado")
                                                {
                                                    iniciarControlesInicial();

                                                    String REGISTRO = QueryStringSeguro["cc"].ToString();
                                                    String TIPO = QueryStringSeguro["tipo"].ToString();
                                                    String MENSAJE = QueryStringSeguro["mensaje"].ToString();
                                                    if (TIPO.ToUpper() == "ERROR")
                                                    {
                                                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El centro de costo " + REGISTRO + " fue activado correctamente. Pero ocurrieron errores con la replica de condiciones económicas.<br>" + MENSAJE, Proceso.Advertencia);
                                                    }
                                                    else
                                                    {
                                                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El centro de costo " + REGISTRO + " fue activado correctamente.<br>" + MENSAJE, Proceso.Correcto);
                                                    }
                                                }
                                                else 
                                                {
                                                    if (accion == "cargarSubccActivado")
                                                    {
                                                        iniciarControlesCargar(false);

                                                        String ID_SUB_C = QueryStringSeguro["subcc"].ToString();
                                                        String TIPO = QueryStringSeguro["tipo"].ToString();
                                                        String MENSAJE = QueryStringSeguro["mensaje"].ToString();

                                                        if (TIPO.ToUpper() == "ERROR")
                                                        {
                                                            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El sub centro " + ID_SUB_C + " fue activado correctamente. Pero ocurrieron errores con la replica de condiciones económicas.<br>" + MENSAJE, Proceso.Advertencia);
                                                        }
                                                        else
                                                        {
                                                            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El sub centro " + ID_SUB_C + " fue activado correctamente.<br>" + MENSAJE, Proceso.Correcto);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    protected void Button_NUEVO_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String ID_EMPRESA = QueryStringSeguro["reg"].ToString();

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
        QueryStringSeguro["img_area"] = "contratacion";
        QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
        QueryStringSeguro["nombre_modulo"] = "CENTROS DE COSTO";
        QueryStringSeguro["accion"] = "nuevo";
        QueryStringSeguro["reg"] = ID_EMPRESA;

        Response.Redirect("~/contratacion/centrosCosto.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }
    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        centroCosto _centroCosto = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        String accion = QueryStringSeguro["accion"].ToString();

        String ID_CIUDAD = DropDownList_CIUDAD.SelectedValue;
        String NOM_CC = TextBox_NOMBRE_CENTRO_COSTO.Text.Trim().ToUpper();
        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

        String TIPO_NOM = null; 
        if (DropDownList_PERIODO_PAGO.SelectedIndex > 0)
        {
            TIPO_NOM = DropDownList_PERIODO_PAGO.SelectedValue;
        }

        String ID_BANCO = null;  
        if (DropDownList_BANCO.SelectedIndex > 0)
        {
            ID_BANCO = DropDownList_BANCO.SelectedValue;
        }

        String CC_EXC_IVA = "N";
        if (CheckBox_EXCENTO_IVA.Checked == true)
        {
            CC_EXC_IVA = "S";
        }

        DataTable tablaResultado;
        Decimal ID_CENTRO_C = 0;
        if (accion == "nuevo")
        {
            DataTable tablaInfoCC = _centroCosto.ObtenerCentrosDeCostoPorIdEmpresaIdCiudadNombreCC(ID_EMPRESA, ID_CIUDAD, NOM_CC);
            if (tablaInfoCC.Rows.Count > 0)
            {
                DataRow filaCC = tablaInfoCC.Rows[0];
                ID_CENTRO_C = Convert.ToDecimal(filaCC["ID_CENTRO_C"]);
                String ESTADO = "ACTIVO";

                tablaResultado = _centroCosto.ActualizarCC(ID_CENTRO_C, ID_EMPRESA, NOM_CC, TIPO_NOM, CC_EXC_IVA, ID_CIUDAD, ID_BANCO, ESTADO);

                if (tablaResultado.Rows.Count <= 0)
                {
                    if (String.IsNullOrEmpty(_centroCosto.MensajeError) == true)
                    {
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se pudo procesar la petición. " + _centroCosto.MensajeError, Proceso.Error);
                    }
                    else
                    {
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Ocurrio un error inesperado procesando la solicitud. No se actualizó el centro de costo seleccionado.", Proceso.Error);
                    }
                }
                else
                {
                    DataRow filaResultado = tablaResultado.Rows[0];

                    QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
                    QueryStringSeguro["img_area"] = "contratacion";
                    QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
                    QueryStringSeguro["nombre_modulo"] = "CENTROS DE COSTO";
                    QueryStringSeguro["accion"] = "cargarActivado";
                    QueryStringSeguro["tipo"] = filaResultado["tipo"].ToString().Trim().ToUpper();
                    QueryStringSeguro["mensaje"] = filaResultado["mensaje"].ToString().Trim();
                    QueryStringSeguro["reg"] = ID_EMPRESA.ToString();
                    QueryStringSeguro["cc"] = ID_CENTRO_C.ToString();

                    Response.Redirect("~/contratacion/centrosCosto.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
                }
            }
            else
            {
                tablaResultado = _centroCosto.AdicionarCC(ID_EMPRESA, NOM_CC, TIPO_NOM, CC_EXC_IVA, ID_CIUDAD, ID_BANCO);

                if (tablaResultado.Rows.Count <= 0)
                {
                    if (String.IsNullOrEmpty(_centroCosto.MensajeError) == true)
                    {
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se pudo procesar la petición. " + _centroCosto.MensajeError, Proceso.Error);
                    }
                    else
                    {
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Ocurrio un error inesperado procesando la solicitud. No se actualizó el centro de costo seleccionado.", Proceso.Error);
                    }
                }
                else
                {
                    DataRow filaResultado = tablaResultado.Rows[0];

                    QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
                    QueryStringSeguro["img_area"] = "contratacion";
                    QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
                    QueryStringSeguro["nombre_modulo"] = "CENTROS DE COSTO";
                    QueryStringSeguro["accion"] = "cargarNuevo";
                    QueryStringSeguro["tipo"] = filaResultado["tipo"].ToString().Trim().ToUpper();
                    QueryStringSeguro["mensaje"] = filaResultado["mensaje"].ToString();
                    QueryStringSeguro["reg"] = ID_EMPRESA.ToString();
                    QueryStringSeguro["cc"] = filaResultado["id"].ToString();

                    Response.Redirect("~/contratacion/centrosCosto.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
                }
            }
        }
        else
        {
            if (accion == "modificar")
            {
                String ESTADO = "ACTIVO";
                if (CheckBox_OcultarCC.Checked == true)
                {
                    ESTADO = "OCULTO";
                }

                ID_CENTRO_C = Convert.ToDecimal(QueryStringSeguro["cc"].ToString());
                tablaResultado = _centroCosto.ActualizarCC(ID_CENTRO_C, ID_EMPRESA, NOM_CC, TIPO_NOM, CC_EXC_IVA, ID_CIUDAD, ID_BANCO, ESTADO);




                if (tablaResultado.Rows.Count <= 0)
                {
                    if (String.IsNullOrEmpty(_centroCosto.MensajeError) == true)
                    {
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se pudo procesar la petición. " + _centroCosto.MensajeError, Proceso.Error);
                    }
                    else
                    {
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Ocurrio un error inesperado procesando la solicitud. No se actualizó el centro de costo seleccionado.", Proceso.Error);
                    }
                }
                else
                {
                    DataRow filaResultado = tablaResultado.Rows[0];

                    if (ESTADO == "ACTIVO")
                    {
                        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
                        QueryStringSeguro["img_area"] = "contratacion";
                        QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
                        QueryStringSeguro["nombre_modulo"] = "CENTROS DE COSTO";
                        QueryStringSeguro["accion"] = "cargarModificado";
                        QueryStringSeguro["tipo"] = filaResultado["tipo"].ToString().Trim().ToUpper();
                        QueryStringSeguro["mensaje"] = filaResultado["mensaje"].ToString();
                        QueryStringSeguro["reg"] = ID_EMPRESA.ToString();
                        QueryStringSeguro["cc"] = ID_CENTRO_C.ToString();

                        Response.Redirect("~/contratacion/centrosCosto.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
                    }
                    else
                    {
                        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
                        QueryStringSeguro["img_area"] = "contratacion";
                        QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
                        QueryStringSeguro["nombre_modulo"] = "CENTROS DE COSTO";
                        QueryStringSeguro["accion"] = "cargarOculto";
                        QueryStringSeguro["tipo"] = filaResultado["tipo"].ToString().Trim().ToUpper();
                        QueryStringSeguro["mensaje"] = filaResultado["mensaje"].ToString();
                        QueryStringSeguro["reg"] = ID_EMPRESA.ToString();
                        QueryStringSeguro["cc"] = ID_CENTRO_C.ToString();

                        Response.Redirect("~/contratacion/centrosCosto.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
                    }

                }
            }
        }
    }

    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String ID_CENTRO_C = QueryStringSeguro["cc"].ToString();
        String reg = QueryStringSeguro["reg"].ToString();

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
        QueryStringSeguro["img_area"] = "contratacion";
        QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
        QueryStringSeguro["nombre_modulo"] = "CENTROS DE COSTO";
        QueryStringSeguro["accion"] = "modificar";
        QueryStringSeguro["reg"] = reg;
        QueryStringSeguro["cc"] = ID_CENTRO_C;

        Response.Redirect("~/contratacion/centrosCosto.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }
    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String accion = QueryStringSeguro["accion"].ToString();
        String ID_EMPRESA = QueryStringSeguro["reg"].ToString();

        if (accion == "nuevo")
        {
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
            QueryStringSeguro["img_area"] = "contratacion";
            QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
            QueryStringSeguro["nombre_modulo"] = "CENTROS DE COSTO";
            QueryStringSeguro["accion"] = "inicial";
            QueryStringSeguro["reg"] = ID_EMPRESA.ToString();

            Response.Redirect("~/contratacion/centrosCosto.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
        }
        else
        {
            if (accion == "modificar")
            {
                String REGISTRO = QueryStringSeguro["cc"].ToString();

                QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
                QueryStringSeguro["img_area"] = "contratacion";
                QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
                QueryStringSeguro["nombre_modulo"] = "CENTROS DE COSTO";
                QueryStringSeguro["accion"] = "cargar";
                QueryStringSeguro["reg"] = ID_EMPRESA.ToString();
                QueryStringSeguro["cc"] = REGISTRO.ToString();

                Response.Redirect("~/contratacion/centrosCosto.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
            }
        }
    }
    protected void Button_LISTA_CONTRATOS_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String ID_EMPRESA = QueryStringSeguro["reg"].ToString();

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
        QueryStringSeguro["img_area"] = "contratacion";
        QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
        QueryStringSeguro["nombre_modulo"] = "CENTROS DE COSTO";
        QueryStringSeguro["accion"] = "inicial";
        QueryStringSeguro["reg"] = ID_EMPRESA.ToString();

        Response.Redirect("~/contratacion/centrosCosto.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }

    protected void GridView_CONTRATOS_SERVICIO_SelectedIndexChanged(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String ID_CENTRO_C = GridView_DATOS.SelectedDataKey["ID_CENTRO_C"].ToString();
        String reg = QueryStringSeguro["reg"].ToString();

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
        QueryStringSeguro["img_area"] = "contratacion";
        QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
        QueryStringSeguro["nombre_modulo"] = "CENTROS DE COSTO";
        QueryStringSeguro["accion"] = "cargar";
        QueryStringSeguro["reg"] = reg;
        QueryStringSeguro["cc"] = ID_CENTRO_C;

        Response.Redirect("~/contratacion/centrosCosto.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }
    protected void GridView_DATOS_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_DATOS.PageIndex = e.NewPageIndex;

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String reg = QueryStringSeguro["reg"].ToString();

        centroCosto _centroCosto = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCentrosCosto = _centroCosto.ObtenerCentrosDeCostoPorIdEmpresa(Convert.ToDecimal(reg));

        if (tablaCentrosCosto.Rows.Count > 0)
        {
            GridView_DATOS.DataSource = tablaCentrosCosto;
            GridView_DATOS.DataBind();
        }
        else
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El cliente no posee centros de costo creados.", Proceso.Advertencia);

            configurarBotonesDeAccion(true, false, false, false, false);
            Panel_FORM_BOTONES_1.Visible = false;

            Panel_RESULTADOS_GRID.Visible = false;

            Panel_FORMULARIO.Visible = false;
        }
    }

    protected void GridView_SUB_C_SelectedIndexChanged(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String ID_CENTRO_C = GridView_SUB_C.SelectedDataKey["ID_CENTRO_C"].ToString();
        String ID_SUB_C = GridView_SUB_C.SelectedDataKey["ID_SUB_C"].ToString();
        String reg = QueryStringSeguro["reg"].ToString();

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
        QueryStringSeguro["img_area"] = "contratacion";
        QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
        QueryStringSeguro["nombre_modulo"] = "SUB CENTROS DE COSTO";
        QueryStringSeguro["accion"] = "modificar";
        QueryStringSeguro["reg"] = reg;
        QueryStringSeguro["cc"] = ID_CENTRO_C;
        QueryStringSeguro["subcc"] = ID_SUB_C;

        Response.Redirect("~/contratacion/subCentrosCosto.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }
    
    protected void Button_NUEVO_SUB_C_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String ID_CENTRO_C = QueryStringSeguro["cc"].ToString();
        String reg = QueryStringSeguro["reg"].ToString();

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
        QueryStringSeguro["img_area"] = "contratacion";
        QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
        QueryStringSeguro["nombre_modulo"] = "SUB CENTROS DE COSTO";
        QueryStringSeguro["accion"] = "nuevo";
        QueryStringSeguro["reg"] = reg;
        QueryStringSeguro["cc"] = ID_CENTRO_C;

        Response.Redirect("~/contratacion/subCentrosCosto.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
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