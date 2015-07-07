using System;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using Brainsbits.LLB.maestras;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB;

using TSHAK.Components;

public partial class _Default : System.Web.UI.Page
{
    private System.Drawing.Color colorNo = System.Drawing.ColorTranslator.FromHtml("#F8523A");
    private System.Drawing.Color colorMedio = System.Drawing.ColorTranslator.FromHtml("#FFF200");
    private System.Drawing.Color colorSi = System.Drawing.ColorTranslator.FromHtml("#50CE04");

    #region carga de grids drops
    private void cargar_GridView_HOJA_DE_TRABAJO(Int32 VALOR)
    { 
        registroContrato _registroContrato = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaContratos = _registroContrato.ObtenerInformacionContratoPorVencer(VALOR);

        if (tablaContratos.Rows.Count <= 0)
        {
            configurarMensajes(true, System.Drawing.Color.Red);
            Label_MENSAJE.Text = "ADVERTENCIA: No se encontraron contratos en rango de vencimiento.";

            Panel_HOJA_DE_TRABAJO.Visible = false;
        }
        else
        {
            GridView_HOJA_DE_TRABAJO.DataSource = tablaContratos;
            GridView_HOJA_DE_TRABAJO.DataBind();

            Int32 contadorALTAS = 0;
            Int32 contadorMEDIAS = 0;
            Int32 contadorBAJAS = 0;
            DataRow filaInfoContrato;
            for (int i = 0; i < tablaContratos.Rows.Count; i++)
            { 
                filaInfoContrato = tablaContratos.Rows[i];
                if (filaInfoContrato["ALERTA"].ToString() == "ALTA")
                {
                    GridView_HOJA_DE_TRABAJO.Rows[i].BackColor = colorNo;
                    contadorALTAS += 1;
                }
                else
                {
                    if (filaInfoContrato["ALERTA"].ToString() == "MEDIA")
                    {
                        GridView_HOJA_DE_TRABAJO.Rows[i].BackColor = colorMedio;
                        contadorMEDIAS += 1;
                    }
                    else
                    {
                        GridView_HOJA_DE_TRABAJO.Rows[i].BackColor = colorSi;
                        contadorBAJAS += 1;
                    }
                }
            }

            Label_ALERTA_BAJA.Text = contadorBAJAS.ToString();
            Label_ALERTA_MEDIA.Text = contadorMEDIAS.ToString();
            Label_ALERTA_ALTA.Text = contadorALTAS.ToString();
        }
    }
    #endregion

    #region metodos para configurar controles
    private void configurarMensajes(Boolean mostrarMensaje, System.Drawing.Color color)
    {
        Panel_MENSAJES.Visible = mostrarMensaje;
        Label_MENSAJE.Visible = mostrarMensaje;
        Label_MENSAJE.ForeColor = color;
    }
    private void configurarInfoAdicionalModulo(Boolean ver, String texto)
    {
        Panel_INFO_ADICIONAL_MODULO.Visible = ver;
        Label_INFO_ADICIONAL_MODULO.Text = texto;
    }
    private void cargar_botones_internos()
    {
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
        link.ID = "link_salir";
        link.NavigateUrl = "javascript:window.close();";
        link.CssClass = "botones_menu_principal";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bMenuSalirEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bMenuSalirAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bMenuSalirEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);

        Table_MENU.Rows.Add(filaTabla);
    }
    #endregion

    #region metodos para obtener datos de la bd
    private Int32 obtenerValorRango()
    {
        Int32 resultado = 0;

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());
        DataTable tablaParametros;

        tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_AVISO_OBJETO_CONTRATO);

        if(tablaParametros.Rows.Count > 0)
        {
            DataRow filaInfoValor = tablaParametros.Rows[0];
            resultado = Convert.ToInt32(filaInfoValor["CODIGO"]);
        }

        return resultado;
    }
    #endregion

    #region metodos que se ejecutan ala cargar la web
    private void iniciar_interfaz_inicial()
    {
        Int32 VALOR = obtenerValorRango();

        if(VALOR <= 0)
        {
            configurarInfoAdicionalModulo(false,"");

            configurarMensajes(true, System.Drawing.Color.Red);
            Label_MENSAJE.Text = "ADVERTENCIA: No se encontró información de rango de aviso.";

            Panel_HOJA_DE_TRABAJO.Visible = false;
        }
        else
        {
            configurarInfoAdicionalModulo(true,"Rango de aviso: " + VALOR.ToString() + " dias.");

            configurarMensajes(false, System.Drawing.Color.Red);

            cargar_GridView_HOJA_DE_TRABAJO(VALOR);
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = "OBJETOS DE CONTRATO";

        if (IsPostBack == false)
        {
            tools _tools = new tools();
            SecureQueryString QueryStringSeguro;
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

            cargar_botones_internos();

            String accion = QueryStringSeguro["accion"].ToString();

            if (accion == "inicial")
            {
                iniciar_interfaz_inicial();
            }
        }
    }
}