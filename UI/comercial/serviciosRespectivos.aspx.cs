using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brainsbits.LLB.comercial;
using System.Data;

using System.Text;
using System.IO;
using Brainsbits.LLB;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;
using TSHAK.Components;
using System.Web;

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

    private void Cargar(GridView gridView, DataTable dataTable )
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

    private void Cargar()
    {
        contratosServicio _contratosServicio = new contratosServicio(Session["idEmpresa"].ToString());

        Cargar(GridView_HOJA_DE_TRABAJO, _contratosServicio.ObtenerSemaforoContratosDeServico());
        Cargar(DropDownList_BUSCAR);
    }

    private void Cargar(DropDownList dropDownList)
    {
        dropDownList.Items.Clear();

        ListItem item = new ListItem("Seleccione...", "");
        dropDownList.Items.Add(item);

        item = new ListItem("Rojo", "Rojo");
        dropDownList.Items.Add(item);

        item = new ListItem("Amarillo", "Amarillo");
        dropDownList.Items.Add(item);

        item = new ListItem("Verde", "Verde");
        dropDownList.Items.Add(item);
    }

    private void Iniciar()
    {
        Page.Header.Title = "SERVICIOS RESPECTIVOS";

        Configurar();
        Cargar();
    }

    private void ocultar_mensaje(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.Style.Add("display", "none");
        panel_mensaje.Style.Add("display", "none");

        panel_fondo.Visible = false;
        panel_mensaje.Visible = false;
    }

    private void Buscar()
    {
        DataTable dataTableColor = new DataTable();
        DataRow[] dataRows;
        string alerta = null;
        Configurar(dataTableColor);
        if (DropDownList_BUSCAR.SelectedValue.Equals("Rojo")) alerta = "ALTO";
        if (DropDownList_BUSCAR.SelectedValue.Equals("Amarillo")) alerta = "MEDIO";
        if (DropDownList_BUSCAR.SelectedValue.Equals("Verde")) alerta = "BAJO";

        contratosServicio _contratosServicio = new contratosServicio(Session["idEmpresa"].ToString());
        if (DropDownList_BUSCAR.SelectedValue.ToString() == "")
        {
            dataTableColor = _contratosServicio.ObtenerSemaforoContratosDeServico();
        }
        else
        {
            dataRows = _contratosServicio.ObtenerSemaforoContratosDeServico().Select("ALERTA = '" + alerta + "'");
            foreach (DataRow dataRow in dataRows)
            {
                dataTableColor.ImportRow(dataRow);
            }
        }
        Cargar(GridView_HOJA_DE_TRABAJO, dataTableColor);
    }


    public void Configurar(DataTable dataTable)
    {
        dataTable.Columns.Add("RAZ_SOCIAL");
        dataTable.Columns.Add("ACTIVO");
        dataTable.Columns.Add("REGISTRO");
        dataTable.Columns.Add("FECHA");
        dataTable.Columns.Add("FCH_VENCE");
        dataTable.Columns.Add("FIRMADO");
        dataTable.Columns.Add("ENVIO_CTE");
        dataTable.Columns.Add("OBJ_CONTRATO");
        dataTable.Columns.Add("ALERTA");
        dataTable.Columns.Add("FIRMADO_TEXTO");
        dataTable.Columns.Add("ENVIO_TEXTO");

    }
    #endregion metodos

    #region eventos
    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    protected void GridView_HOJA_DE_TRABAJO_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_HOJA_DE_TRABAJO.PageIndex = e.NewPageIndex;

        contratosServicio _contratosServicio = new contratosServicio(Session["idEmpresa"].ToString());
        Cargar(GridView_HOJA_DE_TRABAJO, _contratosServicio.ObtenerSemaforoContratosDeServico());
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
        Response.AddHeader("Content-Disposition", "attachment;filename=serviciosComplementarios_" + DateTime.Now.ToString("ddMMyyyy") + ".xls"); 
        GridView_HOJA_DE_TRABAJO.RenderControl(htw);
        Response.Output.Write(sw.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {

    }


    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        Buscar();
    }
    #endregion eventos
}