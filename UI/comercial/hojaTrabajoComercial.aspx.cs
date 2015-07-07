using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Text;
using System.IO;

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

    #region varialbles
    private System.Drawing.Color colorRojo = System.Drawing.ColorTranslator.FromHtml("#F8523A");
    private System.Drawing.Color colorAmarillo = System.Drawing.ColorTranslator.FromHtml("#FFF200");
    private System.Drawing.Color colorVerde = System.Drawing.ColorTranslator.FromHtml("#50CE04");

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }
    #endregion varialbles

    #region constructores
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            Iniciar();
        }
    }
    #endregion constructores

    #region metodos
    private void Iniciar()
    {
        Configurar();
        Cargar();
        cargar_menu_botones_modulos_internos();
    }

    private void Configurar()
    {
        Page.Header.Title = "HOJA DE TRABAJO";
        this.Button_EXPORTAR.Visible = false; 
    }

    private void Cargar()
    {

        contratosServicio _contratosServicio = new contratosServicio(Session["idEmpresa"].ToString());
        Cargar(GridView_HOJA_DE_TRABAJO, _contratosServicio.ObtenerSemaforoContratosDeServico());
    }

    private void Cargar(GridView gridView, DataTable dataTable)
    {
        int cantidad_alto = 0;
        int cantidad_medio = 0;
        int cantidad_bajo = 0;

        gridView.DataSource = dataTable;
        gridView.DataBind();

        for (int i = 0; i < gridView.Rows.Count; i++)
        {
            DataRow filaParaColocarColor = dataTable.Rows[(gridView.PageIndex * gridView.PageSize) + i];

            if (filaParaColocarColor["ALERTA"].ToString().Trim() == "ALTO")
            {
                gridView.Rows[i].BackColor = colorRojo;
                cantidad_alto++;
            }
            else
            {
                if (filaParaColocarColor["ALERTA"].ToString().Trim() == "MEDIO")
                {
                    gridView.Rows[i].BackColor = colorAmarillo;
                    cantidad_medio++;
                }
                else
                {
                    if (filaParaColocarColor["ALERTA"].ToString().Trim() == "BAJO")
                    {
                        gridView.Rows[i].BackColor = colorVerde;
                        cantidad_bajo++;
                    }
                    else
                    {
                        gridView.Rows[i].BackColor = System.Drawing.Color.Transparent;
                    }
                }
            }
        }

        Label_ALERTA_BAJA.Text = "Falta más de un mes para Vencimiento " + cantidad_bajo.ToString();
        Label_ALERTA_MEDIA.Text = "Falta un mes o menos para Vencimiento " + cantidad_medio.ToString();
        Label_ALERTA_ALTA.Text = "Vencido " + cantidad_alto.ToString();
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
        celdaTabla.ID = "cell_4_row_" + contadorFilas.ToString();
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


        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_4_row_" + contadorFilas.ToString();
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
        celdaTabla.ID = "cell_5_row_" + contadorFilas.ToString();
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



        Table_MENU.Rows.Add(filaTabla);




        contadorFilas = 0;













        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_3_T1_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_reportes";
        QueryStringSeguro["nombre_modulo"] = "REPORTES";
        link.NavigateUrl = "~/Reportes/comercial.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
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
    #endregion metodos

    #region eventos
    
    protected void Button_EXPORTAR_Click(object sender, EventArgs e)
    {
        StringBuilder sb;
        StringWriter sw;
        HtmlTextWriter htw;
        sb = new StringBuilder();
        sw = new StringWriter(sb);
        htw = new HtmlTextWriter(sw);
        Response.Clear(); 
        Response.ContentType = "application/vnd.ms-excel"; 
        Response.AddHeader("Content-Disposition", "attachment;filename=ComparativoMensualVentas_" + DateTime.Now.ToString("ddMMyyyy") + ".xls"); 
        GridView_HOJA_DE_TRABAJO.RenderControl(htw);
        Response.Output.Write(sw.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    #endregion eventos
}