using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Text;
using System.IO;
using Brainsbits.LLB;

using Brainsbits.LLB.comercial;

using TSHAK.Components;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;

public partial class comercial_ComportamientoVenta : System.Web.UI.Page
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
        Page.Header.Title = "COMPORTAMIENTO DE VENTAS";
    }

    private void Cargar()
    {
        string año = System.DateTime.Now.Year.ToString();
        this.TextBox_buscar.Text = año;
        Reporte reporte = new Reporte(Session["idEmpresa"].ToString());
        Cargar(this.DropDownList_regionales, reporte.ListarRegionales());
        Cargar(this.DropDownList_ciudades);
        Cargar(año, DropDownList_regionales.SelectedValue, DropDownList_ciudades.SelectedValue);
    }

    private void Cargar(string año, string idRegional, string idCiudad)
    {

        DataTable dataTable;
        Venta venta = new Venta(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        dataTable = venta.ObtenerEstadisticaComportamiento(año, idRegional, idCiudad);
        GridView_HOJA_DE_TRABAJO.DataSource = dataTable;
        GridView_HOJA_DE_TRABAJO.DataBind();
    }

    private void Cargar(DropDownList dropDownList, DataTable dataTable)
    {
        ListItem listItem;
        if (dropDownList.ID.Equals("DropDownList_reportes")) listItem = new ListItem("Seleccione reporte", "0");
        else listItem = new ListItem("Todos", "*");
        dropDownList.Items.Add(listItem);
        foreach (DataRow dataRow in dataTable.Rows)
        {
            listItem = new ListItem(dataRow["nombre"].ToString(), dataRow["id"].ToString());
            dropDownList.Items.Add(listItem);
        }
        dropDownList.DataBind();
    }

    private void Cargar(DropDownList dropDownList)
    {
        ListItem listItem = new ListItem("Todas", "*");
        dropDownList.Items.Add(listItem);
        dropDownList.DataBind();
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
        Cargar(TextBox_buscar.Text, DropDownList_regionales.SelectedValue, DropDownList_ciudades.SelectedValue);
    }

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
        Response.AddHeader("Content-Disposition", "attachment;filename=ComportamientolVentas_" + DateTime.Now.ToString("ddMMyyyy") + ".xls"); 
        GridView_HOJA_DE_TRABAJO.RenderControl(htw);
        Response.Output.Write(sw.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    #endregion eventos
    protected void DropDownList_regionales_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.DropDownList_ciudades.Items.Clear();
        Reporte reporte = new Reporte(Session["idEmpresa"].ToString());
        Cargar(this.DropDownList_ciudades, reporte.ListarCiudadesPorRegional(this.DropDownList_regionales.SelectedValue));
     }
}
