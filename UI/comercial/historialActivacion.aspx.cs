using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using Brainsbits.LLB.comercial;
using Brainsbits.LLB.maestras;

using TSHAK.Components;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB;

public partial class _Default : System.Web.UI.Page
{
    private String NOMBRE_AREA = tabla.NOMBRE_AREA_GESTION_COMERCIAL;

    #region CARGAR DROPS Y GRIDS
    private void cargarGridInfoHistorial()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String reg = QueryStringSeguro["reg"].ToString();

        historialActivacion _historialActivacion = new historialActivacion(Session["idEmpresa"].ToString());

        DataTable tablaHistorialOriginal = _historialActivacion.ObtenerHistorialPorIdEmpresa(Convert.ToDecimal(reg));

        if (_historialActivacion.MensajeError != null)
        {
            configurarMensajes(true, System.Drawing.Color.Red);
            Label_MENSAJE.Text = _historialActivacion.MensajeError;

            Panel_RESULTADOS_GRID.Visible = false;
        }
        else
        {
            if (tablaHistorialOriginal.Rows.Count > 0)
            {
                DataTable tablaHistorial = new DataTable();

                tablaHistorial.Columns.Add("Registro");
                tablaHistorial.Columns.Add("Fecha");
                tablaHistorial.Columns.Add("Clase");
                tablaHistorial.Columns.Add("Usuario");
                tablaHistorial.Columns.Add("Empleado");
                tablaHistorial.Columns.Add("Comentario");

                DataRow filaInfoHistorial;

                foreach (DataRow filaOriginal in tablaHistorialOriginal.Rows)
                {
                    filaInfoHistorial = tablaHistorial.NewRow();
                    filaInfoHistorial["Registro"] = filaOriginal["REGISTRO"].ToString();
                    filaInfoHistorial["Fecha"] = Convert.ToDateTime(filaOriginal["FECHA_R"]).ToShortDateString();
                    filaInfoHistorial["Clase"] = filaOriginal["CLASE_REGISTRO"].ToString();
                    filaInfoHistorial["Usuario"] = filaOriginal["USU_CRE"].ToString();
                    filaInfoHistorial["Empleado"] = filaOriginal["NOMBRE_EMPLEADO"].ToString();
                    filaInfoHistorial["Comentario"] = filaOriginal["COMENTARIOS"].ToString();

                    tablaHistorial.Rows.Add(filaInfoHistorial);
                }

                GridView_HISTORIAL.DataSource = tablaHistorial;
                GridView_HISTORIAL.DataBind();
            }
            else
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = "ADVERTENCIA: No existe historial de activaciones para la empresa.";

                Panel_RESULTADOS_GRID.Visible = false;
            }
        }
    }
    #endregion

    #region CONFIGURACION DE CONTROLES
    private void configurarMensajes(Boolean mostrarMensaje, System.Drawing.Color color)
    {
        if (mostrarMensaje == false)
        {
            Panel_FONDO_MENSAJE.Style.Add("display", "none");
            Panel_MENSAJES.Style.Add("display", "none");
        }
        else
        {
            Panel_FONDO_MENSAJE.Style.Add("display", "block");
            Panel_MENSAJES.Style.Add("display", "block");
            Label_MENSAJE.ForeColor = color;

            if (color == System.Drawing.Color.Green)
            {
                Image_MENSAJE_POPUP.ImageUrl = "~/imagenes/plantilla/ok_popup.png";
            }
            else
            {
                Image_MENSAJE_POPUP.ImageUrl = "~/imagenes/plantilla/advertencia_popup.png";
            }
        }
        Panel_FONDO_MENSAJE.Visible = mostrarMensaje;
        Panel_MENSAJES.Visible = mostrarMensaje;
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
        configurarMensajes(false, System.Drawing.Color.Green);

        cargarGridInfoHistorial();
    }
    #endregion

    private void configurar_paneles_popup(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_mensaje.CssClass = "panel_popup";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Page.Header.Title = "HISTORIAL DE ACTIVACIONES";

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

        cliente _clienteMODULO = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoEmpresa = _clienteMODULO.ObtenerEmpresaConIdEmpresa(ID_EMPRESA);
        DataRow filaTablaInfoEmpresa = tablaInfoEmpresa.Rows[0];
        configurarInfoAdicionalModulo(true, filaTablaInfoEmpresa["RAZ_SOCIAL"].ToString());

        if (IsPostBack == false)
        {
            configurar_paneles_popup(Panel_FONDO_MENSAJE, Panel_MENSAJES);

            String accion = QueryStringSeguro["accion"].ToString();

            if (accion == "inicial")
            {
                iniciarControlesInicial();
            }
        }
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

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        RolPermisos();
    }

    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {
        configurarMensajes(false, System.Drawing.Color.Green);
    }
}