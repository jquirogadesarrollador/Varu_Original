using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using Brainsbits.LLB.maestras;
using Brainsbits.LLB;
using Brainsbits.LLB.comercial;

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

    #region CONFIGURAR CONTROLES
    private void configurarBotonesDeAccion(Boolean bGuardar, Boolean bVolver, Boolean bCancelar)
    {
        Button_GUARDAR.Visible = bGuardar;
        Button_GUARDAR.Enabled = bGuardar;

        Button_VOLVER.Visible = bVolver;
        Button_VOLVER.Enabled = bVolver;

        Button_CANCELAR.Visible = bCancelar;
        Button_CANCELAR.Enabled = bCancelar;
    }
    private void configurarInfoAdicionalModulo(Boolean ver, String texto)
    {
        Panel_INFO_ADICIONAL_MODULO.Visible = ver;
        Label_INFO_ADICIONAL_MODULO.Text = texto;
    }
    #endregion

    #region METODOS QUE SE INICIAN AL CARGAR LA PAGINA
    private void iniciarControlesNuevo()
    {
        configurarBotonesDeAccion(true, true, true);

        Panel_INFORMACION_CC.Visible = true;
        cargar_datos_centro_costo();

        Panel_FORMULARIO.Visible = true;

        Panel_CONTROL_REGISTRO.Visible = false;

        Panel_ID_SUB_C.Visible = false;

        Panel_OcultarSubC.Visible = false;
    }
    private void cargar_datos_sub_centro_costo()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_SUB_C = Convert.ToDecimal(QueryStringSeguro["subcc"]);

        Panel_FORMULARIO.Visible = true;

        Panel_CONTROL_REGISTRO.Visible = false;

        subCentroCosto _subCentroCosto = new subCentroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaSubCentro = _subCentroCosto.ObtenerSubCentrosDeCostoPorIdSubCosto(ID_SUB_C);
        DataRow filaSubCentro = tablaSubCentro.Rows[0];

        Panel_ID_SUB_C.Visible = true;
        Panel_ID_SUB_C.Enabled = false;
        TextBox_ID_SUB_C.Text = filaSubCentro["ID_SUB_C"].ToString();

        TextBox_NOM_SUB_C.Text = filaSubCentro["NOM_SUB_C"].ToString().Trim();

        Panel_OcultarSubC.Visible = true;
        CheckBox_OcultarSubC.Checked = false;
    }
    private void cargar_datos_centro_costo()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_CENTRO_C = Convert.ToDecimal(QueryStringSeguro["cc"]);

        centroCosto _centroCosto = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoCC = _centroCosto.ObtenerCentrosDeCostoPorIdCentroCosto(ID_CENTRO_C);
        DataRow filaInfoCC = tablaInfoCC.Rows[0];

        Label_NOM_CC.Text = filaInfoCC["NOM_CC"].ToString();

        configurarInfoAdicionalModulo(true, "Centro de costo: " + filaInfoCC["NOM_CC"].ToString());
    }
    private void iniciar_interfaz_para_modificar_cliente()
    {
        configurarBotonesDeAccion(true, true, true);
        
        Panel_INFORMACION_CC.Visible = true;
        cargar_datos_centro_costo();

        Panel_FORMULARIO.Visible = true;

        Panel_CONTROL_REGISTRO.Visible = true;

        Panel_ID_SUB_C.Visible = true;

        cargar_datos_sub_centro_costo();
    }
    #endregion

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
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
        Page.Header.Title = "SUB CENTROS DE COSTO";

        if (IsPostBack == false)
        {
            Configurar();

            tools _tools = new tools();
            SecureQueryString QueryStringSeguro;
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"].ToString());

            String accion = QueryStringSeguro["accion"].ToString();

            if (accion == "nuevo")
            {
                iniciarControlesNuevo();   
            }
            else
            {
                if (accion == "modificar")
                {
                    iniciar_interfaz_para_modificar_cliente();
                }
            }
        }
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String accion = QueryStringSeguro["accion"].ToString();

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
        Decimal ID_CENTRO_C = Convert.ToDecimal(QueryStringSeguro["cc"]);

        String NOM_SUB_C = TextBox_NOM_SUB_C.Text.Trim().ToUpper();

        Boolean OCULTAR_SUB_CENTRO = false;
        if (CheckBox_OcultarSubC.Checked == true)
        {
            OCULTAR_SUB_CENTRO = true;
        }

        subCentroCosto _subCentroCosto = new subCentroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataSet dataSetResultados;
        Decimal ID_SUB_C = 0;
        if (accion == "nuevo")
        {
            subCentroCosto _sub = new subCentroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaSubCentro = _sub.ObtenerSubCentroDeCostoPorNombreIdCentroC(NOM_SUB_C, ID_CENTRO_C);

            if (tablaSubCentro.Rows.Count > 0)
            {
                DataRow filaSUBC = tablaSubCentro.Rows[0];
                ID_SUB_C = Convert.ToDecimal(filaSUBC["ID_SUB_C"]);
                Boolean OCULTAR_SUBC = false;

                dataSetResultados = _sub.ActualizarSubCC(ID_SUB_C, ID_EMPRESA, ID_CENTRO_C, NOM_SUB_C, OCULTAR_SUBC);

                if (dataSetResultados.Tables.Count <= 0)
                {
                    if (String.IsNullOrEmpty(_sub.MensajeError) == true)
                    {
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se pudo procesar la petición. " + _sub.MensajeError, Proceso.Error);
                    }
                    else
                    {
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Ocurrio un error inesperado procesando la solicitud. No se actualizó el sub centro seleccionado.", Proceso.Error);
                    }
                }
                else
                {
                    DataView vistaResultados = dataSetResultados.Tables[1].DefaultView;
                    DataTable tablaResultados = vistaResultados.Table;
                    DataRow filaResultado = tablaResultados.Rows[0];

                    QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

                    QueryStringSeguro["img_area"] = "contratacion";
                    QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
                    QueryStringSeguro["nombre_modulo"] = "CENTROS DE COSTO";
                    QueryStringSeguro["accion"] = "cargarSubccActivado";
                    QueryStringSeguro["reg"] = ID_EMPRESA.ToString();
                    QueryStringSeguro["cc"] = ID_CENTRO_C.ToString();
                    QueryStringSeguro["subcc"] = ID_SUB_C.ToString();
                    QueryStringSeguro["tipo"] = filaResultado["tipo"].ToString().Trim().ToUpper();
                    QueryStringSeguro["mensaje"] = filaResultado["mensaje"].ToString().Trim();

                    Response.Redirect("~/contratacion/centrosCosto.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
                }
            }
            else
            {
                dataSetResultados = _subCentroCosto.AdicionarSUBCC(ID_EMPRESA, ID_CENTRO_C, NOM_SUB_C);

                if (dataSetResultados.Tables.Count <= 0)
                {
                    if (String.IsNullOrEmpty(_subCentroCosto.MensajeError) == true)
                    {
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se pudo procesar la petición. " + _subCentroCosto.MensajeError, Proceso.Error);
                    }
                    else
                    {
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Ocurrio un error inesperado procesando la solicitud. No se actualizó el sub centro seleccionado.", Proceso.Error);
                    }
                }
                else
                {
                    DataView vistaResultados = dataSetResultados.Tables[1].DefaultView;
                    DataTable tablaResultados = vistaResultados.Table;
                    DataRow filaResultado = tablaResultados.Rows[0];

                    QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
                    QueryStringSeguro["img_area"] = "contratacion";
                    QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
                    QueryStringSeguro["nombre_modulo"] = "CENTROS DE COSTO";
                    QueryStringSeguro["accion"] = "cargarSubccNuevo";
                    QueryStringSeguro["reg"] = ID_EMPRESA.ToString();
                    QueryStringSeguro["cc"] = ID_CENTRO_C.ToString();
                    QueryStringSeguro["subcc"] = filaResultado["id"].ToString();
                    QueryStringSeguro["tipo"] = filaResultado["tipo"].ToString().Trim().ToUpper();
                    QueryStringSeguro["mensaje"] = filaResultado["mensaje"].ToString();

                    Response.Redirect("~/contratacion/centrosCosto.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
                }
            }
        }
        else
        {
            if (accion == "modificar")
            {
                ID_SUB_C = Convert.ToDecimal(QueryStringSeguro["subcc"].ToString());

                dataSetResultados = _subCentroCosto.ActualizarSubCC(ID_SUB_C, ID_EMPRESA, ID_CENTRO_C, NOM_SUB_C, OCULTAR_SUB_CENTRO);


                if (dataSetResultados.Tables.Count <= 0)
                {
                    if (String.IsNullOrEmpty(_subCentroCosto.MensajeError) == true)
                    {
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se pudo procesar la petición. " + _subCentroCosto.MensajeError, Proceso.Error);
                    }
                    else
                    {
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Ocurrio un error inesperado procesando la solicitud. No se actualizó el sub centro seleccionado.", Proceso.Error);
                    }
                }
                else
                {
                    DataView vistaResultados = dataSetResultados.Tables[1].DefaultView;
                    DataTable tablaResultados = vistaResultados.Table;
                    DataRow filaResultado = tablaResultados.Rows[0];

                    QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

                    QueryStringSeguro["img_area"] = "contratacion";
                    QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
                    QueryStringSeguro["nombre_modulo"] = "CENTROS DE COSTO";
                    QueryStringSeguro["accion"] = "cargarSubccModificado";
                    QueryStringSeguro["reg"] = ID_EMPRESA.ToString();
                    QueryStringSeguro["cc"] = ID_CENTRO_C.ToString();
                    QueryStringSeguro["subcc"] = ID_SUB_C.ToString();
                    QueryStringSeguro["tipo"] = filaResultado["tipo"].ToString().Trim().ToUpper();
                    QueryStringSeguro["mensaje"] = filaResultado["mensaje"].ToString().Trim();

                    Response.Redirect("~/contratacion/centrosCosto.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
                }
            }
        }
    }

    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String accion = QueryStringSeguro["accion"].ToString();

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
        Decimal ID_CENTRO_C = Convert.ToDecimal(QueryStringSeguro["cc"]);

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
        QueryStringSeguro["img_area"] = "contratacion";
        QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
        QueryStringSeguro["nombre_modulo"] = "CENTROS DE COSTO";
        QueryStringSeguro["accion"] = "cargar";
        QueryStringSeguro["reg"] = ID_EMPRESA.ToString();
        QueryStringSeguro["cc"] = ID_CENTRO_C.ToString();

        Response.Redirect("~/contratacion/centrosCosto.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }

    protected void Button_VOLVER_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String accion = QueryStringSeguro["accion"].ToString();

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
        Decimal ID_CENTRO_C = Convert.ToDecimal(QueryStringSeguro["cc"]);

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
        QueryStringSeguro["img_area"] = "contratacion";
        QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
        QueryStringSeguro["nombre_modulo"] = "CENTROS DE COSTO";
        QueryStringSeguro["accion"] = "cargar";
        QueryStringSeguro["reg"] = ID_EMPRESA.ToString();
        QueryStringSeguro["cc"] = ID_CENTRO_C.ToString();

        Response.Redirect("~/contratacion/centrosCosto.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
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