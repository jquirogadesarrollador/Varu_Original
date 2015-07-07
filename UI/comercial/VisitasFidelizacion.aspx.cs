using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using Brainsbits.LLB.seleccion;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.comercial;
using Brainsbits.LLB;

using TSHAK.Components;
using Brainsbits.LLB.seguridad;

public partial class _Default : System.Web.UI.Page
{

    private String NOMBRE_AREA = tabla.NOMBRE_AREA_GESTION_COMERCIAL;

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
    private System.Drawing.Color colorRojo = System.Drawing.ColorTranslator.FromHtml("#F8523A");
    private System.Drawing.Color colorAmarillo = System.Drawing.ColorTranslator.FromHtml("#FFF200");
    private System.Drawing.Color colorVerde = System.Drawing.ColorTranslator.FromHtml("#50CE04");
    #endregion variables

    #region constructor
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            Iniciar();
        }
    }
    #endregion constructor

    #region metodos
    private void Iniciar()
    {
        Configurar();
        Cargar();
    }

    private void Configurar()
    {
        Page.Header.Title = "HOJA DE TRABAJO DE COMERCIAL";
    }

    private void Configurar(DataTable dataTable)
    {
        int cantidadRojos = 0;
        int cantidadVerde = 0;
        int cantidadAmarillo = 0;

        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            switch (dataTable.Rows[i]["color"].ToString())
            {
                case "Rojo":
                    GridView_HOJA_DE_TRABAJO.Rows[i].BackColor = colorRojo;
                    cantidadRojos++;
                    break;
                case "Verde":
                    GridView_HOJA_DE_TRABAJO.Rows[i].BackColor = colorVerde;
                    cantidadVerde++;
                    break;
                case "Amarillo":
                    GridView_HOJA_DE_TRABAJO.Rows[i].BackColor = colorAmarillo;
                    cantidadAmarillo++;
                    break;
            }
        }
        Label_ALERTA_AMARILLA.Text = "Empresas que mantienen sus ventas " + cantidadAmarillo.ToString();
        Label_ALERTA_ROJA.Text = "Empresas que disminuyen en un 20% sus ventas " + cantidadRojos.ToString();
        Label_ALERTA_VERDE.Text = "Empresas que aumentan en un 20% sus ventas " + cantidadVerde.ToString();
    }

    private void Cargar()
    {
        string periodoContable = null;
        if (System.DateTime.Now.Year.ToString().Length.Equals(4)) periodoContable = System.DateTime.Now.Year.ToString("00").Substring(2,2) + System.DateTime.Now.Month.ToString("00").ToString();
        else periodoContable = System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString("00").ToString();
        cargar_menu_botones_modulos_internos();
        Cargar(periodoContable);
    }

    private void Cargar(string periodoContable)
    {
        DataTable dataTable;
        Venta venta = new Venta(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        dataTable = venta.ObtenerComparativoMensualVentas(periodoContable);
        GridView_HOJA_DE_TRABAJO.DataSource = dataTable;
        GridView_HOJA_DE_TRABAJO.DataBind();
        Configurar(dataTable);
    }

    private void cargar_menu_botones_modulos_internos()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = "comercial";
        QueryStringSeguro["nombre_area"] = "COMERCIAL";
        QueryStringSeguro["accion"] = "inicial";

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
        link.ID = "link_clientes";
        QueryStringSeguro["nombre_modulo"] = "ADMINISTRACIÓN DE CLIENTES";
        link.NavigateUrl = "~/comercial/clientes.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bClientesEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bClientesAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bClientesEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);





        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_2_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_servicios_respectivos";
        QueryStringSeguro["nombre_modulo"] = "SERVICIOS RESPECTIVOS";
        link.NavigateUrl = "~/comercial/serviciosRespectivos.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bServiciosRespectivosEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bServiciosRespectivosAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bServiciosRespectivosEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);





        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_3_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_manual_cliente";
        QueryStringSeguro["nombre_modulo"] = "MANUAL DEL CLIENTE";
        link.NavigateUrl = "~/comercial/manualCliente.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bManualServicioEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bManualServicioAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bManualServicioEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);






        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_4_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_visitas_fidelizacion";
        QueryStringSeguro["nombre_modulo"] = "VISITAS DE FIDELIZACIÓN";
        link.NavigateUrl = "~/comercial/visitasFidelizacion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bVisitasFidelizacionEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bVisitasFidelizacionAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bVisitasFidelizacionEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);


        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_5_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_estadisticas_ventas";
        QueryStringSeguro["nombre_modulo"] = "ESTADÍSTICAS DE VENTAS";
        link.NavigateUrl = "~/comercial/EstadisticasVentas.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bEstadisticasVentasEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bEstadisticasVentasAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bEstadisticasVentasEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);



        Table_MENU.Rows.Add(filaTabla);




        contadorFilas = 0;

        filaTabla = new TableRow();
        filaTabla.ID = "row_T1_" + contadorFilas.ToString();

        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_1_T1_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_cartera";
        QueryStringSeguro["nombre_modulo"] = "CARTERA";
        link.NavigateUrl = "~/comercial/cartera.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bCarteraComercialEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bCarteraComercialAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bCarteraComercialEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);


        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_2_T1_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_administracion";
        QueryStringSeguro["nombre_modulo"] = "ADMINISTRACIÓN";
        link.NavigateUrl = "~/comercial/administracion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bMenuAdministracionEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bMenuAdministracionAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bMenuAdministracionEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);

        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_3_T1_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_reportes";
        QueryStringSeguro["nombre_modulo"] = "REPORTES";
        link.NavigateUrl = "~/comercial/reportes.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bReportesEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bReportesAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bReportesEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);

        Table_MENU_1.Rows.Add(filaTabla);
    }

    private void configurar_paneles_popup(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_mensaje.CssClass = "panel_popup";
    }

    #endregion metodos

    #region eventos
    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        Cargar(TextBox_buscar.Text);
    }

    #endregion eventos
}