using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using Brainsbits.LLB.maestras;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB.comercial;

using TSHAK.Components;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB;

public partial class _Default : System.Web.UI.Page
{

    private String NOMBRE_AREA = tabla.NOMBRE_AREA_CONTRATACION;

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

    #region carga de drops y grids
    private void cargar_DropDownList_BANCO()
    {
        DropDownList_BANCO.Items.Clear();

        bancos _bancos = new bancos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaBancos = _bancos.ObtenerTodosLosBancos();

        ListItem item = new ListItem("Seleccione entidad", "");
        DropDownList_BANCO.Items.Add(item);

        foreach (DataRow fila in tablaBancos.Rows)
        {
            item = new ListItem(fila["NOM_BANCO"].ToString(), fila["REGISTRO"].ToString());
            DropDownList_BANCO.Items.Add(item);
        }

        DropDownList_BANCO.DataBind();
    }
    private void cargar_GridView_LISTA_BANCOS_POR_CIUDAD(Boolean modificar)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
        String ID_CIUDAD = QueryStringSeguro["ciudad"];

        bancosPorEmpresa _bancosPorempresa = new bancosPorEmpresa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaBancos = _bancosPorempresa.ObtenerconBancoEmpresaPorCiudadEmpresa(ID_CIUDAD, Convert.ToInt32(ID_EMPRESA));

        if (tablaBancos.Rows.Count > 0)
        {
            Panel_GRILLA_BANCOS_ASIGNADOS.Visible = true;

            GridView_LISTA_BANCOS_POR_CIUDAD.DataSource = tablaBancos;
            GridView_LISTA_BANCOS_POR_CIUDAD.DataBind();

            if (modificar == false)
            {
                GridView_LISTA_BANCOS_POR_CIUDAD.Columns[0].Visible = false;
            }
            configurarMensajesListaBancos(false, System.Drawing.Color.Aqua);
        }
        else
        {
            Panel_GRILLA_BANCOS_ASIGNADOS.Visible = false;

            configurarMensajesListaBancos(true, System.Drawing.Color.Red);
            Label_MENSAJE_LISTA_BANCOS.Text = "ADVERTENCIA: No existen bancos configurados actualmente.";
        }
    }
    private void cargar_GridView_DATOS()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

        cobertura _cobertura = new cobertura(Session["idEmpresa"].ToString());
        DataTable tablaCoberturaEmpresa = _cobertura.obtenerCoberturaDeUnCliente(ID_EMPRESA);

        if (tablaCoberturaEmpresa.Rows.Count > 0)
        {
            GridView_DATOS.DataSource = tablaCoberturaEmpresa;
            GridView_DATOS.DataBind();
        }
        else
        {
            configurarMensajes(true, System.Drawing.Color.Red);
            Label_MENSAJE.Text = "ADVERTENCIA: El cliente no posee cobertura configurada.";

            configurarPanelesGenerales(false, false, false);
        }
    }
    private Boolean cargar_GridView_LISTA_BANCOS_POR_CIUDAD_con_banco(Decimal REGISTRO_BANCO, String NOM_BANCO, Decimal REGISTRO_CON_BANCO_EMPRESA)
    {
        DataTable tablaParaGrid = obtenerDataTableDeGridView_LISTA_BANCOS_POR_CIUDAD();
        DataRow filaParaGrid;

        Boolean verificador = true;
        foreach (DataRow fila in tablaParaGrid.Rows)
        {
            if (Convert.ToDecimal(fila["REGISTRO"]) == REGISTRO_BANCO)
            {
                verificador = false;
                break;
            }
        }

        if (verificador == true)
        {
            filaParaGrid = tablaParaGrid.NewRow();

            filaParaGrid["REGISTRO"] = REGISTRO_BANCO;
            filaParaGrid["NOM_BANCO"] = NOM_BANCO;
            filaParaGrid["REGISTRO_CON_BANCO_EMPRESA"] = REGISTRO_CON_BANCO_EMPRESA;

            tablaParaGrid.Rows.Add(filaParaGrid);

            GridView_LISTA_BANCOS_POR_CIUDAD.DataSource = tablaParaGrid;
            GridView_LISTA_BANCOS_POR_CIUDAD.DataBind();

            return true;
        }

        return false;
    }
    #endregion

    #region configuracion de controles
    private void configurarInfoAdicionalModulo(Boolean ver, String texto)
    {
        Panel_INFO_ADICIONAL_MODULO.Visible = ver;
        Label_INFO_ADICIONAL_MODULO.Text = texto;
    }
    private void configurarMensajes(Boolean mostrarMensaje, System.Drawing.Color color)
    {
        Panel_MENSAJES.Visible = mostrarMensaje;
        Label_MENSAJE.Visible = mostrarMensaje;
        Label_MENSAJE.ForeColor = color;
    }
    private void configurarMensajesListaBancos(Boolean mostrarMensaje, System.Drawing.Color color)
    {
        Panel_MENSAJE_LISTA_BANCOS.Visible = mostrarMensaje;
        Label_MENSAJE_LISTA_BANCOS.Visible = mostrarMensaje;
        Label_MENSAJE_LISTA_BANCOS.ForeColor = color;
    }
    private void configurarBotonesGenerales(Boolean bButton_GUARDAR, Boolean bButton_VOLVER, Boolean bButton_MODIFICAR)
    {
        Button_GUARDAR.Visible = bButton_GUARDAR;
        Button_GUARDAR_1.Visible = bButton_GUARDAR;

        Button_VOLVER.Visible = bButton_VOLVER;
        Button_VOLVER_1.Visible = bButton_VOLVER;

        Button_MODIFICAR.Visible = bButton_MODIFICAR;
        Button_MODIFICAR_1.Visible = bButton_MODIFICAR;
    }
    private void configurarPanelesGenerales(Boolean bPanel_INFO_ADICIONAL_MODULO, Boolean bPanel_RESULTADOS_GRID, Boolean bPanel_FORMULARIO)
    {
        Panel_RESULTADOS_GRID.Visible = bPanel_RESULTADOS_GRID;
        Panel_INFO_ADICIONAL_MODULO.Visible = bPanel_INFO_ADICIONAL_MODULO;
        Panel_FORMULARIO.Visible = bPanel_FORMULARIO;
    }
    #endregion

    #region metodos para capturar datos de los controles
    private DataTable obtenerDataTableDeGridView_LISTA_BANCOS_POR_CIUDAD()
    {
        DataTable tablaResultado = new DataTable();

        tablaResultado.Columns.Add("REGISTRO");
        tablaResultado.Columns.Add("NOM_BANCO");
        tablaResultado.Columns.Add("REGISTRO_CON_BANCO_EMPRESA");

        DataRow filaTablaResultado;

        Decimal REGISTRO;
        String NOM_BANCO;
        Decimal REGISTRO_CON_BANCO_EMPRESA;

        for (int i = 0; i < GridView_LISTA_BANCOS_POR_CIUDAD.Rows.Count; i++)
        {
            REGISTRO = Convert.ToDecimal(GridView_LISTA_BANCOS_POR_CIUDAD.DataKeys[i].Values["REGISTRO"]);
            NOM_BANCO = GridView_LISTA_BANCOS_POR_CIUDAD.Rows[i].Cells[2].Text;
            REGISTRO_CON_BANCO_EMPRESA = Convert.ToDecimal(GridView_LISTA_BANCOS_POR_CIUDAD.DataKeys[i].Values["REGISTRO_CON_BANCO_EMPRESA"]);

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["REGISTRO"] = REGISTRO;
            filaTablaResultado["NOM_BANCO"] = NOM_BANCO;
            filaTablaResultado["REGISTRO_CON_BANCO_EMPRESA"] = REGISTRO_CON_BANCO_EMPRESA;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }
    #endregion


    #region metodos que se cargan al inciar la web
    private void iniciarControlesInicial()
    {
        configurarMensajes(false, System.Drawing.Color.Red);

        configurarBotonesGenerales(false, true, false);

        configurarPanelesGenerales(true, true, false);

        cargar_GridView_DATOS();
    }
    private void cargar_datos_ciudad()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
        String ID_CIUDAD = QueryStringSeguro["ciudad"];

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
        DataTable tablaInfoCiudad = _ciudad.ObtenerCiudadPorIdCiudad(ID_CIUDAD);
        DataRow filaInfoCudad = tablaInfoCiudad.Rows[0];

        Label_CIUDAD_SELECCIONADA.Text = filaInfoCudad["NOMBRE_CIUDAD"].ToString().Trim();
    }
    private void iniciarControlesCargar(Boolean modificar)
    {
        configurarMensajes(false, System.Drawing.Color.Red);

        if (modificar == true)
        {
            configurarBotonesGenerales(true, true, false);
        }
        else
        {
            configurarBotonesGenerales(false, true, true);
        }

        configurarPanelesGenerales(true, false, true);

        cargar_datos_ciudad();

        cargar_GridView_LISTA_BANCOS_POR_CIUDAD(modificar);

        if (modificar == true)
        {
            cargar_DropDownList_BANCO();
            Panel_LISTA_BANCOS.Visible = true;
        }
        else
        {
            Panel_LISTA_BANCOS.Visible = false;
        }
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Page.Header.Title = "ASIGNACIÓN DE BANCOS";


        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

        cliente _clienteMODULO = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoEmpresa = _clienteMODULO.ObtenerEmpresaConIdEmpresa(ID_EMPRESA);
        DataRow filaTablaInfoEmpresa = tablaInfoEmpresa.Rows[0];
        configurarInfoAdicionalModulo(true, filaTablaInfoEmpresa["RAZ_SOCIAL"].ToString());


        if (IsPostBack == false)
        {
            String accion = QueryStringSeguro["accion"].ToString();

            if (accion == "inicial")
            {
                iniciarControlesInicial();
            }
            else
            {
                if (accion == "cargar")
                {
                    iniciarControlesCargar(false);
                }
                else
                {
                    if (accion == "modificar")
                    {
                        iniciarControlesCargar(true);
                    }
                    else
                    {
                        if (accion == "cargarActualizado")
                        {
                            iniciarControlesCargar(false);

                            configurarMensajes(true, System.Drawing.Color.Green);
                            Label_MENSAJE.Text = "La asignación de bancos para la ciudad fue realizada correctamente";
                        }
                    }
                }
            }
        }
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
        String ID_CIUDAD = QueryStringSeguro["ciudad"];

        List<bancos> listaBancos = new List<bancos>();
        bancos _bancosParaLista;

        for(int i = 0; i < GridView_LISTA_BANCOS_POR_CIUDAD.Rows.Count; i++)
        {
            _bancosParaLista = new bancos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            _bancosParaLista.ID_CIUDAD = ID_CIUDAD;
            _bancosParaLista.ID_EMPRESA = ID_EMPRESA;
            _bancosParaLista.REGISTRO_BANCO = Convert.ToDecimal(GridView_LISTA_BANCOS_POR_CIUDAD.DataKeys[i].Values["REGISTRO"]);
            _bancosParaLista.REGISTRO_CON_REG_BANCOS_EMPRESA = Convert.ToDecimal(GridView_LISTA_BANCOS_POR_CIUDAD.DataKeys[i].Values["REGISTRO_CON_BANCO_EMPRESA"]);

            listaBancos.Add(_bancosParaLista);
        }

        bancosPorEmpresa _bancosPorEmpresa = new bancosPorEmpresa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Decimal RESULTADO = _bancosPorEmpresa.AsignarBancosACiudad(ID_EMPRESA, ID_CIUDAD, listaBancos);
        if (RESULTADO <= 0)
        {
            configurarMensajes(true, System.Drawing.Color.Red);
            Label_MENSAJE.Text = _bancosPorEmpresa.MensajeError;
        }
        else
        {
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
            QueryStringSeguro["img_area"] = "contratacion";
            QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
            QueryStringSeguro["nombre_modulo"] = "ASIGNACIÓN BANCOS";
            QueryStringSeguro["accion"] = "cargarActualizado";
            QueryStringSeguro["reg"] = ID_EMPRESA.ToString();
            QueryStringSeguro["ciudad"] = ID_CIUDAD;

            Response.Redirect("~/contratacion/bancosPorCiudad.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
        }
    }
    protected void Button_VOLVER_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"].ToString());

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
        QueryStringSeguro["img_area"] = "contratacion";
        QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
        QueryStringSeguro["nombre_modulo"] = "ASIGNACIÓN BANCOS";
        QueryStringSeguro["accion"] = "inicial";
        QueryStringSeguro["reg"] = ID_EMPRESA.ToString();

        Response.Redirect("~/contratacion/bancosPorCiudad.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }
    protected void GridView_DATOS_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"].ToString());

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]); 

        int filaSeleccionada = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            String ID_CIUDAD = GridView_DATOS.DataKeys[filaSeleccionada].Values["Código Ciudad"].ToString();

            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
            QueryStringSeguro["img_area"] = "contratacion";
            QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
            QueryStringSeguro["nombre_modulo"] = "ASIGNACIÓN BANCOS";
            QueryStringSeguro["accion"] = "cargar";
            QueryStringSeguro["reg"] = ID_EMPRESA.ToString();
            QueryStringSeguro["ciudad"] = ID_CIUDAD;

            Response.Redirect("~/contratacion/bancosPorCiudad.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
        }
    }
    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"].ToString());

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
        String ID_CIUDAD = QueryStringSeguro["ciudad"];

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
        QueryStringSeguro["img_area"] = "contratacion";
        QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
        QueryStringSeguro["nombre_modulo"] = "ASIGNACIÓN BANCOS";
        QueryStringSeguro["accion"] = "modificar";
        QueryStringSeguro["reg"] = ID_EMPRESA.ToString();
        QueryStringSeguro["ciudad"] = ID_CIUDAD;

        Response.Redirect("~/contratacion/bancosPorCiudad.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }
    protected void GridView_LISTA_BANCOS_POR_CIUDAD_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"].ToString());

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
        Decimal ID_CIUDAD = Convert.ToDecimal(QueryStringSeguro["ciudad"]);

        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "eliminar")
        {
            DataTable tablaBancosEnGrid = obtenerDataTableDeGridView_LISTA_BANCOS_POR_CIUDAD();

            tablaBancosEnGrid.Rows[indexSeleccionado].Delete();

            GridView_LISTA_BANCOS_POR_CIUDAD.DataSource = tablaBancosEnGrid;
            GridView_LISTA_BANCOS_POR_CIUDAD.DataBind();

            if (tablaBancosEnGrid.Rows.Count <= 0)
            {
                configurarMensajesListaBancos(true, System.Drawing.Color.Red);
                Label_MENSAJE_LISTA_BANCOS.Text = "ADVERTENCIA: La ciudad selecconada no tiene bancos asignados.";

                Panel_GRILLA_BANCOS_ASIGNADOS.Visible = false;
            }
        }
    }


    protected void Button_AGREGAR_BANCO_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
        String ID_CIUDAD = QueryStringSeguro["ciudad"];

        Decimal REGISTRO_BANCO = Convert.ToDecimal(DropDownList_BANCO.SelectedValue);
        Decimal REGISTRO_CON_BANCO_EMPRESA = 0;
        String NOM_BANCO = DropDownList_BANCO.SelectedItem.Text;

        if (cargar_GridView_LISTA_BANCOS_POR_CIUDAD_con_banco(REGISTRO_BANCO, NOM_BANCO, REGISTRO_CON_BANCO_EMPRESA) == true)
        {
            Panel_GRILLA_BANCOS_ASIGNADOS.Visible = true;
            configurarMensajesListaBancos(true, System.Drawing.Color.Green);
            Label_MENSAJE_LISTA_BANCOS.Text = "EL banco (" + DropDownList_BANCO.SelectedItem.Text + ") fue configurado correctamente.";
        }
        else
        {
            configurarMensajesListaBancos(true, System.Drawing.Color.Red);
            Label_MENSAJE_LISTA_BANCOS.Text = "El banco (" + DropDownList_BANCO.SelectedItem.Text + ") fue configurado previamente.";
        }
    }
}
