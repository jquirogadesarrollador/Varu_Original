using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Brainsbits.LLB.maestras;

using TSHAK.Components;

public partial class _Default : System.Web.UI.Page
{
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
        link.ID = "link_incapacidades";
        QueryStringSeguro["nombre_modulo"] = "INCAPACIDADES";
        link.NavigateUrl = "~/contratacion/incapacidades.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bIncapacidadesEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bIncapacidadesAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bIncapacidadesEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);


        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_2_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_salir";
        link.NavigateUrl = "javascript:window.close();";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bMenuSalirEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bMenuSalirAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bMenuSalirEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);
        Table_MENU.Rows.Add(filaTabla);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            cargar_menu_botones_internos();
        }
    }
}