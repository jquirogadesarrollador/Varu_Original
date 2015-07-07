using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Brainsbits.LLB.maestras;
using TSHAK.Components;

public partial class contratacion_Autoliquidaciones : System.Web.UI.Page
{
    #region constructor
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = "AUTOLIQUIDACION";
        if (IsPostBack == false)
        {
            cargar_menu_botones_internos();
        }
    }
    #endregion constructor

    #region metodos
    private void cargar_menu_botones_internos()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = "contratacion";
        QueryStringSeguro["nombre_area"] = "CONTRATOS Y RELACIONES LABORALES";
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
        link.ID = "link_ARP";
        QueryStringSeguro["nombre_modulo"] = "GENERAR AUTOLIQUIDACION";
        link.NavigateUrl = "~/contratacion/GenerarAutoliquidacion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bGenerarAutoliquidacionEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bGenerarAutoliquidacionAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bGenerarAutoliquidacionEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);





        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_3_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_AFP";
        QueryStringSeguro["nombre_modulo"] = "REPORTES";
        link.NavigateUrl = "~/Reportes/autoliquidacion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bReportesEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bReportesAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bReportesEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);



        Table_MENU.Rows.Add(filaTabla);

    }
    #endregion metodos
}