using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Text;
using System.IO;

using Brainsbits.LLB.comercial;

using TSHAK.Components;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB;


public partial class comercial_Cumplimiento : System.Web.UI.Page
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
        Page.Header.Title = "CUMPLIMIENTO DE VENTAS";
    }

    private void Cargar()
    {
        string año = System.DateTime.Now.Year.ToString();
        this.TextBox_buscar.Text = año;
        Cargar(año);
    }

    private void Cargar(string año)
    {
        DataTable dataTable;
        Venta venta = new Venta(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        dataTable = venta.ObtenerCumplimentoVentas(año);
        Configurar(dataTable);
        GridView_HOJA_DE_TRABAJO.DataSource = dataTable;
        GridView_HOJA_DE_TRABAJO.DataBind();
        Configurar(GridView_HOJA_DE_TRABAJO);
    }

    private void Configurar(DataTable dataTable)
    {
        foreach (DataRow dataRow in dataTable.Rows)
        {
            if ((dataRow["Descripcion"].ToString().Equals("Cumplimiento")) || (dataRow["Descripcion"].ToString().Equals("Presupuesto")))
            {

            }
        }
    }

    private void Configurar(GridView gridView)
    {
        foreach (GridViewRow gridViewRow in gridView.Rows)
        {
            gridViewRow.Cells[0].BorderWidth = 0;
        }
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
        Response.AddHeader("Content-Disposition", "attachment;filename=CumplimientoVentas_" + DateTime.Now.ToString("ddMMyyyy") + ".xls"); 
        GridView_HOJA_DE_TRABAJO.RenderControl(htw);
        Response.Output.Write(sw.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    #endregion eventos
}