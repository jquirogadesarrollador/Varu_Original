﻿using System;
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

public partial class comercial_VentasAUI : System.Web.UI.Page
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
        Page.Header.Title = "VENTAS POR AIU";
        this.Button_EXPORTAR.Visible = false;
    }

    private void Cargar()
    {
        string periodoContable = null;
        if (System.DateTime.Now.Year.ToString().Length.Equals(4)) periodoContable = System.DateTime.Now.Year.ToString("00").Substring(2, 2) + System.DateTime.Now.Month.ToString("00").ToString();
        else periodoContable = System.DateTime.Now.Year.ToString() + System.DateTime.Now.Month.ToString("00").ToString();
        this.TextBox_buscar.Text = periodoContable;
        Cargar(periodoContable);
    }

    private void Cargar(string periodoContable)
    {
        DataTable dataTable;
        Venta venta = new Venta(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        dataTable = venta.ObtenerVentasAIU(periodoContable);
        GridView_HOJA_DE_TRABAJO.DataSource = dataTable;
        GridView_HOJA_DE_TRABAJO.DataBind();
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
        Response.AddHeader("Content-Disposition", "attachment;filename=VentasAIU_" + DateTime.Now.ToString("ddMMyyyy") + ".xls"); 
        GridView_HOJA_DE_TRABAJO.RenderControl(htw);
        Response.Output.Write(sw.ToString());
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    #endregion eventos

}