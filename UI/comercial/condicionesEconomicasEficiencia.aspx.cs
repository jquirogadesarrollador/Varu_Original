using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Drawing;

using Brainsbits.LLB.comercial;
using Brainsbits.LLB.maestras;
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

        DataTable tablaInformacionPermisos = _seguridad.ObtenerPermisosBotonesConSoloNombreDeModulo(NOMBRE_AREA, "CONDICIONES ECONÓMICAS");

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

    #region VARIABLES GLOBALES
    private Decimal _ID_EMPRESA = 0;
    private String _ID_CIUDAD = null;
    private Decimal _ID_CENTRO_C = 0;
    private Decimal _ID_SUB_C = 0;

    private System.Drawing.Color colorNo = System.Drawing.ColorTranslator.FromHtml("#F8523A");
    private System.Drawing.Color colorMedio = System.Drawing.ColorTranslator.FromHtml("#FFF200");
    private System.Drawing.Color colorSi = System.Drawing.ColorTranslator.FromHtml("#50CE04");
    #endregion

    #region PROPIEDADES
    private Decimal ID_EMPRESA
    {
        get { return _ID_EMPRESA; }
        set { _ID_EMPRESA = value; }
    }
    private String ID_CIUDAD
    {
        get { return _ID_CIUDAD; }
        set { _ID_CIUDAD = value; }
    }
    private Decimal ID_CENTRO_C
    {
        get { return _ID_CENTRO_C; }
        set { _ID_CENTRO_C = value; }
    }
    private Decimal ID_SUB_C
    {
        get { return _ID_SUB_C; }
        set { _ID_SUB_C = value; }
    }
    #endregion

    #region cargar drops y grids
    private void cargar_DropDownList_CONFIGURACION()
    {
        DropDownList_CONFIGURACION.Items.Clear();

        ListItem item = new ListItem("Seleccione", "");
        DropDownList_CONFIGURACION.Items.Add(item);

        item = new ListItem("AIU", "AIU");
        DropDownList_CONFIGURACION.Items.Add(item);

        item = new ListItem("AIU PREFERENCIAL", "AIU_PREFERENCIAL");
        DropDownList_CONFIGURACION.Items.Add(item);

        item = new ListItem("TARIFA", "TARIFA");
        DropDownList_CONFIGURACION.Items.Add(item);

        item = new ListItem("IVA", "IVA");
        DropDownList_CONFIGURACION.Items.Add(item);

        item = new ListItem("REEMBOLSO DE GASTOS", "REEMBOLSO_DE_GASTOS");
        DropDownList_CONFIGURACION.Items.Add(item);

        item = new ListItem("NINGUNA", "NINGUNA");
        DropDownList_CONFIGURACION.Items.Add(item);

        DropDownList_CONFIGURACION.DataBind();
    }
    private void cargar_DropDownList_REGIMEN()
    {
        DropDownList_REGIMEN.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_REGIMEN_EMPRESA);

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_REGIMEN.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_REGIMEN.Items.Add(item);
        }
        DropDownList_REGIMEN.DataBind();
    }
    private void cargar_DropDownList_MOD_SOPORTE()
    {
        DropDownList_MOD_SOPORTE.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_MODELO_SOPORTE);

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_MOD_SOPORTE.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_MOD_SOPORTE.Items.Add(item);
        }
        DropDownList_MOD_SOPORTE.DataBind();
    }

    private void cargar_DropDownList_MOD_FACTURA()
    {
        DropDownList_MOD_FACTURA.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_MODELO_FACTURA);

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_MOD_FACTURA.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_MOD_FACTURA.Items.Add(item);
        }
        DropDownList_MOD_FACTURA.DataBind();
    }
    private void cargar_DropDownList_DETALLES_SERVICIO()
    {
        DropDownList_DETALLES_SERVICIO.Items.Clear();

        condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaServiciosComplementarios = _condicionComercial.ObtenerServiciosComplementariosPorTipo(tabla.SERVICIO_EMPRESA_OUTSORUCING);

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_DETALLES_SERVICIO.Items.Add(item);

        foreach (DataRow fila in tablaServiciosComplementarios.Rows)
        {
            item = new ListItem(fila["NOMBRE_SERVICIO_COMPLEMENTARIO"].ToString(), fila["ID_SERVICIO_COMPLEMENTARIO"].ToString());
            DropDownList_DETALLES_SERVICIO.Items.Add(item);
        }
        DropDownList_DETALLES_SERVICIO.DataBind();
    }
    private void cargar_GridView_COBERTURA()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

        cobertura _cobertura = new cobertura(Session["idEmpresa"].ToString());

        DataTable tablaCobertura = _cobertura.obtenerCoberturaDeUnCliente(ID_EMPRESA);

        if (tablaCobertura.Rows.Count <= 0)
        {
            configurarMensajesCobertura(true, System.Drawing.Color.Red);
            Label_MENSAJE_COBERTURA.Text = "La empresa no tiene configurada actualmente una cobertura.";
        }
        else
        {
            configurarMensajesCobertura(false, System.Drawing.Color.Red);
            
            GridView_COBERTURA.DataSource = tablaCobertura;
            GridView_COBERTURA.DataBind();

            DataRow filaEnTablaCobertura;
            condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaCondicionesComercialesDeCobertura;

            centroCosto _centroCosto = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaCentrosDeCostoCiudad;
            for (int i = 0; i < tablaCobertura.Rows.Count; i++)
            {
                filaEnTablaCobertura = tablaCobertura.Rows[i];

                tablaCondicionesComercialesDeCobertura = _condicionComercial.ObtenerCondicionesEconomicasPorId(ID_EMPRESA, filaEnTablaCobertura["Código Ciudad"].ToString(), 0, 0);
                if (tablaCondicionesComercialesDeCobertura.Rows.Count <= 0)
                {
                    GridView_COBERTURA.Rows[i].BackColor = colorNo;
                }
                else
                {
                    GridView_COBERTURA.Rows[i].BackColor = colorSi;
                }

                tablaCentrosDeCostoCiudad = _centroCosto.ObtenerCentrosDeCostoPorIdEmpresaIdCiudad(ID_EMPRESA, filaEnTablaCobertura["Código Ciudad"].ToString());
                if (tablaCentrosDeCostoCiudad.Rows.Count <= 0)
                {
                    GridView_COBERTURA.Rows[i].Cells[1].Enabled = false;
                }
                else
                {
                    GridView_COBERTURA.Rows[i].Cells[1].Enabled = true;
                }

            }
        }

        configurarMensajesCC(true, System.Drawing.Color.Red);
        Label_MENSAJE_CC.Text = "Por favor seleccionar una ciudad de la lista de cobertura.";
        GridView_CENTROS_DE_COSTO.DataSource = null;
        GridView_CENTROS_DE_COSTO.DataBind();

        configurarMensajesSUBCC(true, System.Drawing.Color.Red);
        Label_MENSAJE_SUB_CC.Text = "Por favor seleccionar un centro de costo de la lista de centros de costo.";
        GridView_SUB_CENTROS_DE_COSTO.DataSource = null;
        GridView_SUB_CENTROS_DE_COSTO.DataBind();
        
    }
    private void cargar_GridView_CENTROS_DE_COSTO(Decimal ID_EMPRESA, String ID_CIUDAD)
    {
        centroCosto _centroCosto = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCCDeCiudad = _centroCosto.ObtenerCentrosDeCostoPorIdEmpresaIdCiudad(ID_EMPRESA, ID_CIUDAD);

        if (tablaCCDeCiudad.Rows.Count <= 0)
        {
            configurarMensajesCC(true, System.Drawing.Color.Red);
            Label_MENSAJE_COBERTURA.Text = "La Ciudad no tiene centros de costo actualmente.";
        }
        else
        {
            configurarMensajesCC(false, System.Drawing.Color.Red);

            GridView_CENTROS_DE_COSTO.DataSource = tablaCCDeCiudad;
            GridView_CENTROS_DE_COSTO.DataBind();

            DataRow filaEnTablaCC;
            condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaCondicionesComercialesDeCC;

            subCentroCosto _subCentroCosto = new subCentroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaSUBCCDeCC;
            for (int i = 0; i < tablaCCDeCiudad.Rows.Count; i++)
            {
                filaEnTablaCC = tablaCCDeCiudad.Rows[i];

                tablaCondicionesComercialesDeCC = _condicionComercial.ObtenerCondicionesEconomicasPorId(ID_EMPRESA, null, Convert.ToDecimal(filaEnTablaCC["ID_CENTRO_C"]), 0);
                if (tablaCondicionesComercialesDeCC.Rows.Count <= 0)
                {
                    GridView_CENTROS_DE_COSTO.Rows[i].BackColor = colorNo;
                }
                else
                {
                    GridView_CENTROS_DE_COSTO.Rows[i].BackColor = colorSi;
                }

                tablaSUBCCDeCC = _subCentroCosto.ObtenerSubCentrosDeCostoPorIdEmpresaIdCentroCosto(ID_EMPRESA, Convert.ToDecimal(filaEnTablaCC["ID_CENTRO_C"]));
                if (tablaSUBCCDeCC.Rows.Count <= 0)
                {
                    GridView_CENTROS_DE_COSTO.Rows[i].Cells[2].Enabled = false;
                }
                else
                {
                    GridView_CENTROS_DE_COSTO.Rows[i].Cells[2].Enabled = true;
                }
            }
        }

        configurarMensajesSUBCC(true, System.Drawing.Color.Red);
        Label_MENSAJE_SUB_CC.Text = "Por favor seleccionar un centro de costo de la lista de centros de costo.";
        GridView_SUB_CENTROS_DE_COSTO.DataSource = null;
        GridView_SUB_CENTROS_DE_COSTO.DataBind();
    }
    private void cargar_GridView_SUB_CENTROS_DE_COSTO(Decimal ID_EMPRESA, Decimal ID_CENTRO_C)
    {
        subCentroCosto _subCentroCosto = new subCentroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaSUBCCDeCC = _subCentroCosto.ObtenerSubCentrosDeCostoPorIdEmpresaIdCentroCosto(ID_EMPRESA, ID_CENTRO_C);

        if (tablaSUBCCDeCC.Rows.Count <= 0)
        {
            configurarMensajesSUBCC(true, System.Drawing.Color.Red);
            Label_MENSAJE_COBERTURA.Text = "El Centro de costos seleccionado no tiene sub centros.";
        }
        else
        {
            configurarMensajesSUBCC(false, System.Drawing.Color.Red);

            GridView_SUB_CENTROS_DE_COSTO.DataSource = tablaSUBCCDeCC;
            GridView_SUB_CENTROS_DE_COSTO.DataBind();

            DataRow filaEnTablaSUBCC;
            condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaCondicionesComercialesDeSUBCC;

            for (int i = 0; i < tablaSUBCCDeCC.Rows.Count; i++)
            {
                filaEnTablaSUBCC = tablaSUBCCDeCC.Rows[i];

                tablaCondicionesComercialesDeSUBCC = _condicionComercial.ObtenerCondicionesEconomicasPorId(ID_EMPRESA, null,0, Convert.ToDecimal(filaEnTablaSUBCC["ID_SUB_C"]));
                if (tablaCondicionesComercialesDeSUBCC.Rows.Count <= 0)
                {
                    GridView_SUB_CENTROS_DE_COSTO.Rows[i].BackColor = colorNo;
                }
                else
                {
                    GridView_SUB_CENTROS_DE_COSTO.Rows[i].BackColor = colorSi;
                }

                GridView_SUB_CENTROS_DE_COSTO.Rows[i].Cells[2].Enabled = false;
            }
        }
    }
    private void cargar_GridView_DETALLES_SERVICIO_SELECCIONADO(Decimal ID_SERVICIO, String NOMBRE_SERVICIO, Decimal ID_SERVICIO_POR_EMPRESA)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String accion = QueryStringSeguro["accion"].ToString();

        DataTable tablaDetallesServicio = new DataTable();

        tablaDetallesServicio.Columns.Add("ID_SERVICIO");
        tablaDetallesServicio.Columns.Add("NOMBRE_SERVICIO");
        tablaDetallesServicio.Columns.Add("ID_SERVICIO_COMPLEMENTARIO");
        tablaDetallesServicio.Columns.Add("NOMBRE_SERVICIO_COMPLEMENTARIO");

        DataRow filaTablaDetallesServicio;

        condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoDetalle;
        DataRow filaTablaInfoDetalle;

        List<detalleServicio> listaDetallesServicio = capturarListaDetallesServicioDesdeSession();
        foreach (detalleServicio detalle in listaDetallesServicio)
        {
            if((ID_SERVICIO_POR_EMPRESA == detalle.ID_SERVICIO_POR_EMPRESA) && (ID_SERVICIO == detalle.ID_SERVICIO) && (NOMBRE_SERVICIO == detalle.NOMBRE_SERVICIO))
            {
                filaTablaDetallesServicio = tablaDetallesServicio.NewRow();

                filaTablaDetallesServicio["ID_SERVICIO"] = ID_SERVICIO.ToString();
                filaTablaDetallesServicio["NOMBRE_SERVICIO"] = NOMBRE_SERVICIO;
                filaTablaDetallesServicio["ID_SERVICIO_COMPLEMENTARIO"] = detalle.ID_SERVICIO_COMPLEMENTARIO.ToString();

                tablaInfoDetalle = _condicionComercial.ObtenerServiciosComplementariosPorId(detalle.ID_SERVICIO_COMPLEMENTARIO);
                filaTablaInfoDetalle = tablaInfoDetalle.Rows[0];

                filaTablaDetallesServicio["NOMBRE_SERVICIO_COMPLEMENTARIO"] = filaTablaInfoDetalle["NOMBRE_SERVICIO_COMPLEMENTARIO"];

                tablaDetallesServicio.Rows.Add(filaTablaDetallesServicio);
            }           
        }

        if ((accion == "inicial") || (accion == "cargarModificado") || (accion == "cargarNuevo"))
        {
            GridView_DETALLES_SERVICIO_SELECCIONADO.Columns[0].Visible = false;
        }

        GridView_DETALLES_SERVICIO_SELECCIONADO.DataSource = tablaDetallesServicio;
        GridView_DETALLES_SERVICIO_SELECCIONADO.DataBind();

        Panel_GRID_DETALLES_SERVICIO_SELECCIONADO.Visible = true;
    }
    #endregion

    #region cargar datos en controles
    private void cargarEstructuraDeCC()
    { 
        cargar_GridView_COBERTURA();
    }

    private void cargar_campos_condiciones_comerciales(DataRow infoCondicionesComerciales)
    {
        TextBox_USU_CRE.Text = infoCondicionesComerciales["USU_CRE"].ToString();
        try
        {
            TextBox_FCH_CRE.Text = DateTime.Parse(infoCondicionesComerciales["FCH_CRE"].ToString()).ToShortDateString();
            TextBox_HOR_CRE.Text = DateTime.Parse(infoCondicionesComerciales["FCH_CRE"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_CRE.Text = "";
            TextBox_HOR_CRE.Text = "";
        }
        TextBox_USU_MOD.Text = infoCondicionesComerciales["USU_MOD"].ToString();
        try
        {
            TextBox_FCH_MOD.Text = DateTime.Parse(infoCondicionesComerciales["FCH_MOD"].ToString()).ToShortDateString();
            TextBox_HOR_MOD.Text = DateTime.Parse(infoCondicionesComerciales["FCH_MOD"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_MOD.Text = "";
            TextBox_HOR_MOD.Text = "";
        }

        TextBox_COD_CONDICION.Text = infoCondicionesComerciales["REGISTRO"].ToString().Trim();

        if (infoCondicionesComerciales["FACTURA"].ToString().Trim() == "N")
        {
            CheckBox_FACTURA_NOMINA.Checked = false;
        }
        else
        {
            if (infoCondicionesComerciales["FACTURA"].ToString().Trim() == "S")
            {
                CheckBox_FACTURA_NOMINA.Checked = true;
            }
        }

        cargar_DropDownList_MOD_SOPORTE();
        DropDownList_MOD_SOPORTE.SelectedValue = infoCondicionesComerciales["MOD_SOPORTE"].ToString().Trim();
        cargar_DropDownList_MOD_FACTURA();
        DropDownList_MOD_FACTURA.SelectedValue = infoCondicionesComerciales["MOD_FACTURA"].ToString().Trim();

        TextBox_AD_NOM.Text = String.Format("{0:N2}", Convert.ToDecimal(infoCondicionesComerciales["AD_NOM"]));

        if (infoCondicionesComerciales["SOLO_DEV"].ToString().Trim() == "N")
        {
            CheckBox_SOLO_DEV.Checked = false;
        }
        else
        {
            if (infoCondicionesComerciales["SOLO_DEV"].ToString().Trim() == "S")
            {
                CheckBox_SOLO_DEV.Checked = true;
            }
        }

        if (infoCondicionesComerciales["APL_MTZ"].ToString().Trim() == "N")
        {
            CheckBox_APL_MTZ.Checked = false;
        }
        else
        {
            if (infoCondicionesComerciales["APL_MTZ"].ToString().Trim() == "S")
            {
                CheckBox_APL_MTZ.Checked = true;
            }
        }

        TextBox_AD_PENSION.Text = String.Format("{0:N2}", Convert.ToDecimal(infoCondicionesComerciales["AD_PENSION"]));
        TextBox_AD_SALUD.Text = String.Format("{0:N2}", Convert.ToDecimal(infoCondicionesComerciales["AD_SALUD"]));
        TextBox_AD_RIESGOS.Text = String.Format("{0:N2}", Convert.ToDecimal(infoCondicionesComerciales["AD_RIESGOS"]));
        TextBox_AD_APO_SENA.Text = String.Format("{0:N2}", Convert.ToDecimal(infoCondicionesComerciales["AD_APO_SENA"]));
        TextBox_AD_APO_ICBF.Text = String.Format("{0:N2}", Convert.ToDecimal(infoCondicionesComerciales["AD_APO_ICBF"]));
        TextBox_AD_APO_CAJA.Text = String.Format("{0:N2}", Convert.ToDecimal(infoCondicionesComerciales["AD_APO_CAJA"]));
        TextBox_AD_VACACIONES.Text = String.Format("{0:N2}", Convert.ToDecimal(infoCondicionesComerciales["AD_VACACIONES"]));
        TextBox_AD_CESANTIA.Text = String.Format("{0:N2}", Convert.ToDecimal(infoCondicionesComerciales["AD_CESANTIA"]));
        TextBox_AD_INT_CES.Text = String.Format("{0:N2}", Convert.ToDecimal(infoCondicionesComerciales["AD_INT_CES"]));
        TextBox_AD_PRIMA.Text = String.Format("{0:N2}", Convert.ToDecimal(infoCondicionesComerciales["AD_PRIMA"]));
        TextBox_AD_SEG_VID.Text = String.Format("{0:N2}", Convert.ToDecimal(infoCondicionesComerciales["AD_SEG_VID"]));

        cargarCheckBox(CheckBox_SUB_PENSION, infoCondicionesComerciales["SUB_PENSION"].ToString().Trim());
        cargarCheckBox(CheckBox_SUB_SALUD, infoCondicionesComerciales["SUB_SALUD"].ToString().Trim());
        cargarCheckBox(CheckBox_SUB_RIESGO, infoCondicionesComerciales["SUB_RIESGOS"].ToString().Trim());
        cargarCheckBox(CheckBox_SUB_SENA, infoCondicionesComerciales["SUB_SENA"].ToString().Trim());
        cargarCheckBox(CheckBox_SUB_ICBF, infoCondicionesComerciales["SUB_ICBF"].ToString().Trim());
        cargarCheckBox(CheckBox_SUB_CAJA, infoCondicionesComerciales["SUB_CAJA"].ToString().Trim());
        cargarCheckBox(CheckBox_SUB_VACACIONES, infoCondicionesComerciales["SUB_VACACIONES"].ToString().Trim());
        cargarCheckBox(CheckBox_SUB_CESANTIAS, infoCondicionesComerciales["SUB_CESANTIAS"].ToString().Trim());
        cargarCheckBox(CheckBox_SUB_INT_CES, infoCondicionesComerciales["SUB_INT_CES"].ToString().Trim());
        cargarCheckBox(CheckBox_SUB_PRIMA, infoCondicionesComerciales["SUB_PRIMA"].ToString().Trim());
        cargarCheckBox(CheckBox_SUB_SEG_VID, infoCondicionesComerciales["SUB_SEG_VID"].ToString().Trim());

        cargarCheckBox(CheckBox_RET_VAC, infoCondicionesComerciales["RET_VAC"].ToString().Trim());
        cargarCheckBox(CheckBox_RET_CES, infoCondicionesComerciales["RET_CES"].ToString().Trim());
        cargarCheckBox(CheckBox_RET_INT_CES, infoCondicionesComerciales["RET_INT_CES"].ToString().Trim());
        cargarCheckBox(CheckBox_RET_PRIM, infoCondicionesComerciales["RET_PRIM"].ToString().Trim());

        TextBox_DIAS_VNC.Text = infoCondicionesComerciales["DIAS_VNC"].ToString().Trim();

        cargar_DropDownList_REGIMEN();
        DropDownList_REGIMEN.SelectedValue = infoCondicionesComerciales["REGIMEN"].ToString().Trim();

        TextBox_OBS_FACT.Text = infoCondicionesComerciales["OBS_FACT"].ToString().Trim();
    }

    private void cargarInfoCondiciones(Boolean bModificar)
    {
        capturarVariablesGlogales();

        if (ID_SUB_C != 0)
        {
            subCentroCosto _subCentroCostoMODULO = new subCentroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaInfoSubCentro = _subCentroCostoMODULO.ObtenerSubCentrosDeCostoPorIdSubCosto(ID_SUB_C);
            DataRow filaTablaInfoSubCentro = tablaInfoSubCentro.Rows[0];

            configurarInfoAdicionalModulo(true, filaTablaInfoSubCentro["NOM_SUB_C"].ToString());

            Panel_COPIA_CONDICIONES_ECONOMICAS_GRUPO_EMPRESARIAL.Visible = false;
        }
        else
        {
            if (ID_CENTRO_C != 0)
            {
                centroCosto _centroCostoMODULO = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                DataTable tablaInfoCC = _centroCostoMODULO.ObtenerCentrosDeCostoPorIdCentroCosto(ID_CENTRO_C);
                DataRow filaTablaInfoCC = tablaInfoCC.Rows[0];

                configurarInfoAdicionalModulo(true, filaTablaInfoCC["NOM_CC"].ToString());

                Panel_COPIA_CONDICIONES_ECONOMICAS_GRUPO_EMPRESARIAL.Visible = false;
            }
            else
            {
                if ((String.IsNullOrEmpty(ID_CIUDAD) == false) && (ID_EMPRESA != 0))
                {
                    cobertura _coberturaMODULO = new cobertura(Session["idEmpresa"].ToString());
                    DataTable tablaInfoCiudad = _coberturaMODULO.obtenerNombreCiudadPorIdCiudad(ID_CIUDAD);
                    DataRow filaTablaInfoCiudad = tablaInfoCiudad.Rows[0];

                    configurarInfoAdicionalModulo(true, filaTablaInfoCiudad["NOMBRE"].ToString());

                    Panel_COPIA_CONDICIONES_ECONOMICAS_GRUPO_EMPRESARIAL.Visible = false;
                }
                else
                {
                    cliente _clienteMODULO = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                    DataTable tablaInfoCliente = _clienteMODULO.ObtenerEmpresaConIdEmpresa(ID_EMPRESA);
                    DataRow filaTablaInfoCliente = tablaInfoCliente.Rows[0];

                    configurarInfoAdicionalModulo(true, filaTablaInfoCliente["RAZ_SOCIAL"].ToString());
                    Panel_COPIA_CONDICIONES_ECONOMICAS_GRUPO_EMPRESARIAL.Visible = true;
                }
            }
        }

        condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCondiciones = _condicionComercial.ObtenerCondicionesEconomicasPorId(ID_EMPRESA, ID_CIUDAD, ID_CENTRO_C, ID_SUB_C);
        if (tablaCondiciones.Rows.Count <= 0)
        {
            if (_condicionComercial.MensajeError != null)
            {
                if ((String.IsNullOrEmpty(ID_CIUDAD) == true) && (ID_CENTRO_C == 0) && (ID_SUB_C == 0))
                {
                    configurarBotonesDeAccion(false, false, false, false);
                }
                else
                {
                    configurarBotonesDeAccion(false, false, false, true);
                }

                Panel_FORMULARIO.Visible = false;

                Panel_SECCION_SERVICIOS.Visible = false;

                Panel_ESTRUCTURA_CENTRO_COSTOS.Visible = false;

                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = _condicionComercial.MensajeError;

                Panel_COPIA_CONDICIONES_ECONOMICAS_GRUPO_EMPRESARIAL.Visible = false;
            }
            else
            {
                if ((String.IsNullOrEmpty(ID_CIUDAD) == true) && (ID_CENTRO_C == 0) && (ID_SUB_C == 0))
                {
                    configurarBotonesDeAccion(false, true, true, false);
                }
                else
                {
                    configurarBotonesDeAccion(false, true, true, true);
                }

                Panel_FORMULARIO.Visible = true;

                Panel_SECCION_SERVICIOS.Visible = false;

                Panel_ESTRUCTURA_CENTRO_COSTOS.Visible = true;

                controlesParaNuevaCondicionEconomica();

                if ((String.IsNullOrEmpty(ID_CIUDAD) == true) && (ID_CENTRO_C == 0) && (ID_SUB_C == 0))
                {
                    Panel_ESTRUCTURA_CENTRO_COSTOS.Visible = true;
                    cargarEstructuraDeCC();

                    configurarMensajesCC(true, System.Drawing.Color.Red);
                    Label_MENSAJE_CC.Text = "Por favor seleccionar una ciudad de la lista de cobertura.";

                    configurarMensajesSUBCC(true, System.Drawing.Color.Red);
                    Label_MENSAJE_SUB_CC.Text = "Por favor seleccionar un centro de costo de la lista de centros de costo.";
                }
                else
                {
                    Panel_ESTRUCTURA_CENTRO_COSTOS.Visible = false;
                }
            }
        }
        else
        {

            if (bModificar == true)
            {
                if ((String.IsNullOrEmpty(ID_CIUDAD) == true) && (ID_CENTRO_C == 0) && (ID_SUB_C == 0))
                {
                    configurarBotonesDeAccion(false, true, true, false);
                }
                else
                {
                    configurarBotonesDeAccion(false, true, true, true);
                }

                Panel_FORMULARIO.Enabled = true;

                Panel_CONTROL_REGISTRO.Visible = false;

                Panel_COD_CONDICIONES.Visible = false;

                Panel_COPIA_CONDICIONES_ECONOMICAS_GRUPO_EMPRESARIAL.Visible = true;
                mostrar_modulo_copia_condiciones_grupoempresarial();

            }
            else 
            {
                if ((String.IsNullOrEmpty(ID_CIUDAD) == true) && (ID_CENTRO_C == 0) && (ID_SUB_C == 0))
                {
                    configurarBotonesDeAccion(true, false, false, false);
                }
                else
                {
                    configurarBotonesDeAccion(true, false, false, true);
                }

                Panel_FORMULARIO.Enabled = false;

                Panel_CONTROL_REGISTRO.Visible = true;
                Panel_CONTROL_REGISTRO.Enabled = false;

                Panel_COD_CONDICIONES.Visible = true;
                Panel_COD_CONDICIONES.Enabled = false;

                Panel_COPIA_CONDICIONES_ECONOMICAS_GRUPO_EMPRESARIAL.Visible = false;
            }

            Panel_FORMULARIO.Visible = true;

            configurarMensajes(false, System.Drawing.Color.Green);

            DataRow infoCondicionesComerciales = tablaCondiciones.Rows[0];

            cargar_campos_condiciones_comerciales(infoCondicionesComerciales);
            
            DataTable tablaServiciosDeLaEntidad;
            if (ID_SUB_C != 0)
            {
                tablaServiciosDeLaEntidad = _condicionComercial.ObtenerServiciosPorEmpresaPorIdSubC(ID_SUB_C);

                Panel_COPIA_CONDICIONES_ECONOMICAS_GRUPO_EMPRESARIAL.Visible = false;
            }
            else
            {
                if (ID_CENTRO_C != 0)
                {
                    tablaServiciosDeLaEntidad = _condicionComercial.ObtenerServiciosPorEmpresaPorIdCentroC(ID_CENTRO_C);

                    Panel_COPIA_CONDICIONES_ECONOMICAS_GRUPO_EMPRESARIAL.Visible = false;
                }
                else
                {
                    if ((ID_EMPRESA != 0) && (String.IsNullOrEmpty(ID_CIUDAD) == false))
                    {
                        tablaServiciosDeLaEntidad = _condicionComercial.ObtenerServiciosPorEmpresaPorIdCiudad(ID_CIUDAD, ID_EMPRESA);

                        Panel_COPIA_CONDICIONES_ECONOMICAS_GRUPO_EMPRESARIAL.Visible = false;
                    }
                    else
                    {
                        tablaServiciosDeLaEntidad = _condicionComercial.ObtenerServiciosPorEmpresaPorIdEmpresa(ID_EMPRESA);

                    }
                }
            }
             
            if (tablaServiciosDeLaEntidad.Rows.Count <= 0)
            {
                habilitarSeccionServiciosParaDatosNuevos(bModificar); 
            }
            else
            {

                List<servicio> listaServicios = new List<servicio>();
                Session.Remove("listaServicios_" + ID_EMPRESA.ToString());
                Session.Add("listaServicios_" + ID_EMPRESA.ToString(), listaServicios);
                List<detalleServicio> listaDetallesServicio = new List<detalleServicio>();
                Session.Remove("listaDetallesServicio_" + ID_EMPRESA.ToString());
                Session.Add("listaDetallesServicio_" + ID_EMPRESA.ToString(), listaDetallesServicio);

                cargarInformacionServiciosComplementariosDeUnaEntidad(tablaServiciosDeLaEntidad, bModificar, false);
            }

            if ((String.IsNullOrEmpty(ID_CIUDAD) == true) && (ID_CENTRO_C == 0) && (ID_SUB_C == 0))
            {
                Panel_ESTRUCTURA_CENTRO_COSTOS.Visible = true;
                cargarEstructuraDeCC();

                configurarMensajesCC(true, System.Drawing.Color.Red);
                Label_MENSAJE_CC.Text = "Por favor seleccionar una ciudad de la lista de cobertura.";

                configurarMensajesSUBCC(true, System.Drawing.Color.Red);
                Label_MENSAJE_SUB_CC.Text = "Por favor seleccionar un centro de costo de la lista de centros de costo.";
            }
            else
            {
                Panel_ESTRUCTURA_CENTRO_COSTOS.Visible = false;
            }
        }
    }
    private void cargarInformacionServiciosComplementariosDeUnaEntidad(DataTable tablaServicios, Boolean modificar, Boolean datosHeredados)
    {
        capturarVariablesGlogales();

        DataTable tablaServiciosCreada = new DataTable();

        tablaServiciosCreada.Columns.Add("ID_EMPRESA");
        tablaServiciosCreada.Columns.Add("ID_SERVICIO");
        tablaServiciosCreada.Columns.Add("ID_SERVICIO_POR_EMPRESA");
        tablaServiciosCreada.Columns.Add("NOMBRE_SERVICIO");
        tablaServiciosCreada.Columns.Add("AIU");
        tablaServiciosCreada.Columns.Add("IVA");
        tablaServiciosCreada.Columns.Add("VALOR");
        tablaServiciosCreada.Columns.Add("observaciones");

        List<servicio> listaServicios = capturarListaServiciosDesdeSession();
        List<detalleServicio> listaDetallesServicio = capturarListaDetallesServicioDesdeSession();
        servicio _servicio;
        detalleServicio _detalleServicio;

        detalleServicio _detalleServicioDelServicioSeleccionado = new detalleServicio(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaDetallesServicioDelServicioSeleccionado;

        DataRow filaTablaServiciosCreada;
        foreach (DataRow servicio in tablaServicios.Rows)
        {
            filaTablaServiciosCreada = tablaServiciosCreada.NewRow();

            filaTablaServiciosCreada["ID_EMPRESA"] = servicio["ID_EMPRESA"];

            if (datosHeredados == true)
            {
                filaTablaServiciosCreada["ID_SERVICIO"] = 0;
                filaTablaServiciosCreada["ID_SERVICIO_POR_EMPRESA"] = 0;
            }
            else
            {
                filaTablaServiciosCreada["ID_SERVICIO"] = servicio["ID_SERVICIO"];
                filaTablaServiciosCreada["ID_SERVICIO_POR_EMPRESA"] = servicio["ID_SERVICIO_POR_EMPRESA"];
            }
            
            
            filaTablaServiciosCreada["NOMBRE_SERVICIO"] = servicio["NOMBRE_SERVICIO"];
            filaTablaServiciosCreada["AIU"] = servicio["AIU"];
            filaTablaServiciosCreada["IVA"] = servicio["IVA"];
            filaTablaServiciosCreada["VALOR"] = servicio["VALOR"];
            filaTablaServiciosCreada["observaciones"] = servicio["observaciones"];

            tablaServiciosCreada.Rows.Add(filaTablaServiciosCreada);

            _servicio = new servicio(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            if (datosHeredados == true)
            {
                _servicio.ACCION = "INSERTAR";
                _servicio.ID_SERVICIO_POR_EMPRESA = 0;
                _servicio.ID_SERVICIO = 0;
            }
            else
            {
                _servicio.ACCION = "NINGUNO";
                _servicio.ID_SERVICIO_POR_EMPRESA = Convert.ToDecimal(servicio["ID_SERVICIO_POR_EMPRESA"]);
                _servicio.ID_SERVICIO = Convert.ToDecimal(servicio["ID_SERVICIO"]);
            }
            
            _servicio.AIU = Convert.ToDecimal(servicio["AIU"]);
            _servicio.IVA = Convert.ToDecimal(servicio["IVA"]);
            _servicio.NOMBRE_SERVICIO = servicio["NOMBRE_SERVICIO"].ToString();
            _servicio.VALOR = Convert.ToDecimal(servicio["VALOR"]);
            _servicio.DESCRIPCION = servicio["observaciones"].ToString();

            listaServicios.Add(_servicio);

            tablaDetallesServicioDelServicioSeleccionado = _detalleServicioDelServicioSeleccionado.ObtenerDetalleServicioPorIdServicioActivos(Convert.ToDecimal(servicio["ID_SERVICIO"]));

            foreach (DataRow filaTablaDetallesServicio in tablaDetallesServicioDelServicioSeleccionado.Rows)
            {
                _detalleServicio = new detalleServicio(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

                if (datosHeredados == true)
                {
                    _detalleServicio.ACCION = "INSERTAR";
                    _detalleServicio.ID_DETALLE_SERVICIO = 0;
                    _detalleServicio.ID_SERVICIO = 0;
                }
                else
                {
                    _detalleServicio.ACCION = "NINGUNO";
                    _detalleServicio.ID_DETALLE_SERVICIO = Convert.ToDecimal(filaTablaDetallesServicio["ID_DETALLE_SERVICIO"]);
                    _detalleServicio.ID_SERVICIO = Convert.ToDecimal(filaTablaDetallesServicio["ID_SERVICIO"]);
                }

                _detalleServicio.ID_SERVICIO_POR_EMPRESA = _servicio.ID_SERVICIO_POR_EMPRESA;
                _detalleServicio.AIU = Convert.ToDecimal(filaTablaDetallesServicio["AIU"]);
                _detalleServicio.ID_SERVICIO_COMPLEMENTARIO = Convert.ToDecimal(filaTablaDetallesServicio["ID_SERVICIO_COMPLEMENTARIO"]);
                _detalleServicio.IVA = Convert.ToDecimal(filaTablaDetallesServicio["IVA"]);
                _detalleServicio.NOMBRE_SERVICIO = filaTablaDetallesServicio["NOMBRE_SERVICIO"].ToString();
                _detalleServicio.VALOR = Convert.ToDecimal(filaTablaDetallesServicio["VALOR"]);

                listaDetallesServicio.Add(_detalleServicio);
            }
        }

        Session.Remove("listaServicios_" + ID_EMPRESA.ToString());
        Session.Add("listaServicios_" + ID_EMPRESA.ToString(),listaServicios);
        Session.Remove("listaDetallesServicio_" + ID_EMPRESA);
        Session.Add("listaDetallesServicio_" + ID_EMPRESA.ToString(), listaDetallesServicio);

        if (modificar == false)
        {
            GridView_SERVICIOS_INCLUIDOS.Columns[0].Visible = false;
        }

        GridView_SERVICIOS_INCLUIDOS.DataSource = tablaServiciosCreada;
        GridView_SERVICIOS_INCLUIDOS.DataBind();

        Panel_SECCION_SERVICIOS.Visible = true;
        Panel_SERVICIOS_ACTUALES_GENERAL.Visible = true;
        configurarMensajesServiciosActuales(false, System.Drawing.Color.Red);
        Panel_GRID_SERVICIOS_ACTUALES.Visible = true;
        Panel_GRID_DETALLES_SERVICIO_SELECCIONADO.Visible = false;

        if (modificar == false)
        {
            Panel_BOTON_NUEVO_SERVICIO.Visible = false;
        }
        else
        {
            Panel_BOTON_NUEVO_SERVICIO.Visible = true;
        }

        configurarSeccionDeNuevoServicio(false, false, false, false, false, false);
    }
    #endregion

    #region metodos para configurar controles
    private void configurarBotonesDeAccion(Boolean bModificar, Boolean bGuardar, Boolean bCancelar, Boolean bVolver)
    {
        Button_MODIFICAR.Visible = bModificar;
        Button_MODIFICAR.Enabled = bModificar;
        Button_MODIFICAR_1.Visible = bModificar;
        Button_MODIFICAR_1.Enabled = bModificar;
        
        Button_GUARDAR.Visible = bGuardar;
        Button_GUARDAR.Enabled = bGuardar;
        Button_GUARDAR_1.Visible = bGuardar;
        Button_GUARDAR_1.Enabled = bGuardar;

        Button_CANCELAR.Visible = bCancelar;
        Button_CANCELAR.Enabled = bCancelar;
        Button_CANCELAR_1.Visible = bCancelar;
        Button_CANCELAR_1.Enabled = bCancelar;

        Button_VOLVER.Visible = bVolver;
        Button_VOLVER.Enabled = bVolver;
        Button_VOLVER_1.Visible = bVolver;
        Button_VOLVER_1.Enabled = bVolver;
    }
    private void configurarInfoAdicionalModulo(Boolean ver, String texto)
    {
        Panel_INFO_ADICIONAL_MODULO.Visible = ver;
        Label_INFO_ADICIONAL_MODULO.Text = texto;
    }

    private void configurarMensajes(Boolean mostrarMensaje, System.Drawing.Color color)
    {
        if (mostrarMensaje == false)
        {
            Panel_FONDO_MENSAJE.Style.Add("display", "none");
            Panel_MENSAJES.Style.Add("display", "none");
        }
        else
        {
            Panel_FONDO_MENSAJE.Style.Add("display", "block");
            Panel_MENSAJES.Style.Add("display", "block");
            Label_MENSAJE.ForeColor = color;

            if (color == System.Drawing.Color.Green)
            {
                Image_MENSAJE_POPUP.ImageUrl = "~/imagenes/plantilla/ok_popup.png";
            }
            else
            {
                Image_MENSAJE_POPUP.ImageUrl = "~/imagenes/plantilla/advertencia_popup.png";
            }
        }
        Panel_FONDO_MENSAJE.Visible = mostrarMensaje;
        Panel_MENSAJES.Visible = mostrarMensaje;
    }
    private void configurarMensajesServiciosActuales(Boolean mostrarMensaje, System.Drawing.Color color)
    {
        if (mostrarMensaje == false)
        {
            Panel_FONDO_MENSAJES_SERVICIOS_ACTUALES.Style.Add("display", "none");
            Panel_MENSAJES_SERVICIOS_ACTUALES.Style.Add("display", "none");
        }
        else
        {
            Panel_FONDO_MENSAJES_SERVICIOS_ACTUALES.Style.Add("display", "block");
            Panel_MENSAJES_SERVICIOS_ACTUALES.Style.Add("display", "block");
            Panel_MENSAJES_SERVICIOS_ACTUALES.ForeColor = color;

            if (color == System.Drawing.Color.Green)
            {
                Image_MENSAJES_SERVICIOS_ACTUALES_POPUP.ImageUrl = "~/imagenes/plantilla/ok_popup.png";
            }
            else
            {
                Image_MENSAJES_SERVICIOS_ACTUALES_POPUP.ImageUrl = "~/imagenes/plantilla/advertencia_popup.png";
            }
        }
        Panel_FONDO_MENSAJES_SERVICIOS_ACTUALES.Visible = mostrarMensaje;
        Panel_MENSAJES_SERVICIOS_ACTUALES.Visible = mostrarMensaje;
    }
    private void configurarMensajesNuevoServicio(Boolean mostrarMensaje, System.Drawing.Color color)
    {
        if (mostrarMensaje == false)
        {
            Panel_FONDO_MENSAJES_NUEVO_SERVICIO.Style.Add("display", "none");
            Panel_MENSAJES_NUEVO_SERVICIO.Style.Add("display", "none");
        }
        else
        {
            Panel_FONDO_MENSAJES_NUEVO_SERVICIO.Style.Add("display", "block");
            Panel_MENSAJES_NUEVO_SERVICIO.Style.Add("display", "block");
            Label_MENSAJES_NUEVO_SERVICIO.ForeColor = color;

            if (color == System.Drawing.Color.Green)
            {
                Image_MENSAJES_NUEVO_SERVICIO_POPUP.ImageUrl = "~/imagenes/plantilla/ok_popup.png";
            }
            else
            {
                Image_MENSAJES_NUEVO_SERVICIO_POPUP.ImageUrl = "~/imagenes/plantilla/advertencia_popup.png";
            }
        }
        Panel_FONDO_MENSAJES_NUEVO_SERVICIO.Visible = mostrarMensaje;
        Panel_MENSAJES_NUEVO_SERVICIO.Visible = mostrarMensaje;
    }
    private void configurarMensajesCobertura(Boolean mostrarMensaje, System.Drawing.Color color)
    {
        Panel_MENSAJE_COBERTURA.Visible = mostrarMensaje;
        Label_MENSAJE_COBERTURA.Visible = mostrarMensaje;
        Label_MENSAJE_COBERTURA.ForeColor = color;
    }
    private void configurarMensajesCC(Boolean mostrarMensaje, System.Drawing.Color color)
    {
        Panel_MENSAJE_CC.Visible = mostrarMensaje;
        Label_MENSAJE_CC.Visible = mostrarMensaje;
        Label_MENSAJE_CC.ForeColor = color;
    }
    private void configurarMensajesSUBCC(Boolean mostrarMensaje, System.Drawing.Color color)
    {
        Panel_MENSAJE_SUB_CC.Visible = mostrarMensaje;
        Label_MENSAJE_SUB_CC.Visible = mostrarMensaje;
        Label_MENSAJE_SUB_CC.ForeColor = color;
    }

    private void habilitarSeccionServiciosParaDatosNuevos(Boolean modificar)
    {
        capturarVariablesGlogales();

        Panel_SECCION_SERVICIOS.Visible = true;

        Panel_SERVICIOS_ACTUALES_GENERAL.Visible = true;

        configurarMensajesServiciosActuales(true, System.Drawing.Color.Red);

        if (ID_SUB_C != 0)
        {
            Label_MENSAJES_SERVICIOS_ACTUALES.Text = "ADVERTENCIA: El sub centro de costo no tiene servicios configurados actualmente, es necesario configurarlos en este momento.";
        }
        else
        {
            if (ID_CENTRO_C != 0)
            {
                Label_MENSAJES_SERVICIOS_ACTUALES.Text = "ADVERTENCIA: El centro de costo no tiene servicios configurados actualmente, es necesario configurarlos en este momento.";
            }
            else
            {
                if ((ID_EMPRESA != 0) && (String.IsNullOrEmpty(ID_CIUDAD) == false))
                {
                    Label_MENSAJES_SERVICIOS_ACTUALES.Text = "ADVERTENCIA: La cobertura no tiene servicios configurados actualmente, es necesario configurarlos en este momento.";
                }
                else
                {
                    Label_MENSAJES_SERVICIOS_ACTUALES.Text = "ADVERTENCIA: La empresa no tiene servicios configurados actualmente, es necesario configurarlos en este momento.";
                }
            }
        }

        Panel_GRID_SERVICIOS_ACTUALES.Visible = false;

        Panel_GRID_DETALLES_SERVICIO_SELECCIONADO.Visible = false;

        if (modificar == true)
        {
            configurarSeccionDeNuevoServicio(true, true, false, false, false, false);

            Panel_MENSAJES_NUEVO_SERVICIO.Visible = false;

            limpiarTextBoxServicioAdicionar();
        }
        else
        {
            configurarSeccionDeNuevoServicio(false, false, false, false, false, false);
        }

        List<servicio> listaServicios = new List<servicio>();
        Session.Remove("listaServicios_" + ID_EMPRESA.ToString());
        Session.Add("listaServicios_" + ID_EMPRESA.ToString(), listaServicios);
        List<detalleServicio> listaDetallesServicio = new List<detalleServicio>();
        Session.Remove("listaDetallesServicio_" + ID_EMPRESA.ToString());
        Session.Add("listaDetallesServicio_" + ID_EMPRESA.ToString(), listaDetallesServicio);
    }
    private void cargar_DropDownList_EMPRESAS_DEL_GRUPO(DataTable tablaEmpresas)
    {
        DropDownList_EMPRESAS_DEL_GRUPO.Items.Clear();

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_EMPRESAS_DEL_GRUPO.Items.Add(item);

        foreach (DataRow fila in tablaEmpresas.Rows)
        {
            item = new ListItem(fila["RAZ_SOCIAL"].ToString(), fila["ID_EMPRESA"].ToString());
            DropDownList_EMPRESAS_DEL_GRUPO.Items.Add(item);
        }
        DropDownList_EMPRESAS_DEL_GRUPO.DataBind();
    }
    private void mostrar_modulo_copia_condiciones_grupoempresarial()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"].ToString());

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfo = _cliente.ObtenerEmpresaConIdEmpresa(ID_EMPRESA);
        DataRow filaInfo = tablaInfo.Rows[0];

        Decimal ID_GRUPOEMPRESARIAL = 0;
        if (filaInfo["ID_GRUPO_EMPRESARIAL"] != DBNull.Value)
        {
            ID_GRUPOEMPRESARIAL = Convert.ToDecimal(filaInfo["ID_GRUPO_EMPRESARIAL"]);
        }

        if (ID_GRUPOEMPRESARIAL == 0)
        {
            Panel_COPIA_CONDICIONES_ECONOMICAS_GRUPO_EMPRESARIAL.Visible = false;
        }
        else
        {
            DataTable tablaEmpresas = _cliente.ObtenerEmpresasDelMismoGrupoEmpresarial(ID_GRUPOEMPRESARIAL, ID_EMPRESA);

            if (tablaEmpresas.Rows.Count <= 0)
            {
                Panel_COPIA_CONDICIONES_ECONOMICAS_GRUPO_EMPRESARIAL.Visible = false;
            }
            else
            {
                Panel_COPIA_CONDICIONES_ECONOMICAS_GRUPO_EMPRESARIAL.Visible = true;
                cargar_DropDownList_EMPRESAS_DEL_GRUPO(tablaEmpresas);
            }
        }
    }

    private void controlesParaNuevaCondicionEconomica()
    {
        capturarVariablesGlogales();

        if ((String.IsNullOrEmpty(ID_CIUDAD) == true) && (ID_CENTRO_C == 0) && (ID_SUB_C == 0))
        {
            configurarBotonesDeAccion(false, true, true, false);

            configurarMensajes(true, System.Drawing.Color.Red);
            Label_MENSAJE.Text = "ADVERTENCIA: No se encontraron registros de condiciones económicas para mostrar, por favor digitelas y guarde.";

            Panel_CONTROL_REGISTRO.Visible = false;

            Panel_COD_CONDICIONES.Visible = false;

            CheckBox_FACTURA_NOMINA.Checked = false;

            cargar_DropDownList_MOD_SOPORTE();
            cargar_DropDownList_MOD_FACTURA();

            TextBox_AD_NOM.Text = "0";
            CheckBox_SOLO_DEV.Checked = false;
            CheckBox_APL_MTZ.Checked = false;

            TextBox_AD_PENSION.Text = "0";
            TextBox_AD_SALUD.Text = "0";
            TextBox_AD_RIESGOS.Text = "0";
            TextBox_AD_APO_SENA.Text = "0";
            TextBox_AD_APO_ICBF.Text = "0";
            TextBox_AD_APO_CAJA.Text = "0";
            TextBox_AD_VACACIONES.Text = "0";
            TextBox_AD_CESANTIA.Text = "0";
            TextBox_AD_INT_CES.Text = "0";
            TextBox_AD_PRIMA.Text = "0";
            TextBox_AD_SEG_VID.Text = "0";

            CheckBox_SUB_PENSION.Checked = false;
            CheckBox_SUB_SALUD.Checked = false;
            CheckBox_SUB_RIESGO.Checked = false;
            CheckBox_SUB_SENA.Checked = false;
            CheckBox_SUB_ICBF.Checked = false;
            CheckBox_SUB_CAJA.Checked = false;
            CheckBox_SUB_VACACIONES.Checked = false;
            CheckBox_SUB_CESANTIAS.Checked = false;
            CheckBox_SUB_INT_CES.Checked = false;
            CheckBox_SUB_PRIMA.Checked = false;
            CheckBox_SUB_SEG_VID.Checked = false;

            CheckBox_RET_VAC.Checked = false;
            CheckBox_RET_CES.Checked = false;
            CheckBox_RET_INT_CES.Checked = false;
            CheckBox_RET_PRIM.Checked = false;

            TextBox_DIAS_VNC.Text = "";

            cargar_DropDownList_REGIMEN();

            TextBox_OBS_FACT.Text = "";

            habilitarSeccionServiciosParaDatosNuevos(true);

            mostrar_modulo_copia_condiciones_grupoempresarial();
        }
        else
        {
            Decimal ID_EMPRESA_1 = ID_EMPRESA;
            String ID_CIUDAD_1 = null;
            Decimal ID_CENTRO_C_1 = 0;
            Decimal ID_SUB_C_1 = 0;


            Panel_FORMULARIO.Visible = true;
            Panel_FORMULARIO.Enabled = true;

            Panel_COPIA_CONDICIONES_ECONOMICAS_GRUPO_EMPRESARIAL.Visible = false;

            configurarMensajes(false, System.Drawing.Color.Green);

            condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(),Session["USU_LOG"].ToString());

            if (ID_SUB_C != 0)
            {
                subCentroCosto _subCentroCosto = new subCentroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                DataTable tablainfoSubCentro = _subCentroCosto.ObtenerSubCentrosDeCostoPorIdSubCosto(ID_SUB_C);
                DataRow infoSubCentro = tablainfoSubCentro.Rows[0];

                ID_EMPRESA_1 = ID_EMPRESA;
                ID_CIUDAD_1 = null;
                ID_CENTRO_C_1 = Convert.ToDecimal(infoSubCentro["ID_CENTRO_C"]);
                ID_SUB_C_1 = 0;
            }
            else
            {
                if (ID_CENTRO_C != 0)
                {
                    centroCosto _centroCosto = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                    DataTable tablaCentroCosto = _centroCosto.ObtenerCentrosDeCostoPorIdCentroCosto(ID_CENTRO_C);
                    DataRow infoCentroCosto = tablaCentroCosto.Rows[0];

                    ID_EMPRESA_1 = ID_EMPRESA;
                    ID_CIUDAD_1 = infoCentroCosto["ID_CIUDAD"].ToString().Trim();
                    ID_CENTRO_C_1 = 0;
                    ID_SUB_C_1 = 0;
                }
            }

            DataTable tablaCondicionesHeredadas = _condicionComercial.ObtenerCondicionesEconomicasPorId(ID_EMPRESA_1, ID_CIUDAD_1, ID_CENTRO_C_1, ID_SUB_C_1); 

            if (tablaCondicionesHeredadas.Rows.Count <= 0)
            {
                configurarBotonesDeAccion(false, true, true, true);

                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = "ADVERTENCIA: No se encontraron registros de condiciones económicas para mostrar y no se pudieron heredar condiciones económicas de la entidad anterior a la seleccionada,  por favor digite las nuevas condiciones y guarde.";

                Panel_CONTROL_REGISTRO.Visible = false;

                Panel_COD_CONDICIONES.Visible = false;

                CheckBox_FACTURA_NOMINA.Checked = false;

                cargar_DropDownList_MOD_SOPORTE();
                cargar_DropDownList_MOD_FACTURA();

                TextBox_AD_NOM.Text = "0,00";
                CheckBox_SOLO_DEV.Checked = false;
                CheckBox_APL_MTZ.Checked = false;

                TextBox_AD_PENSION.Text = "0,00";
                TextBox_AD_SALUD.Text = "0,00";
                TextBox_AD_RIESGOS.Text = "0,00";
                TextBox_AD_APO_SENA.Text = "0,00";
                TextBox_AD_APO_ICBF.Text = "0,00";
                TextBox_AD_APO_CAJA.Text = "0,00";
                TextBox_AD_VACACIONES.Text = "0,00";
                TextBox_AD_CESANTIA.Text = "0,00";
                TextBox_AD_INT_CES.Text = "0,00";
                TextBox_AD_PRIMA.Text = "0,00";
                TextBox_AD_SEG_VID.Text = "0,00";

                CheckBox_SUB_PENSION.Checked = false;
                CheckBox_SUB_SALUD.Checked = false;
                CheckBox_SUB_RIESGO.Checked = false;
                CheckBox_SUB_SENA.Checked = false;
                CheckBox_SUB_ICBF.Checked = false;
                CheckBox_SUB_CAJA.Checked = false;
                CheckBox_SUB_VACACIONES.Checked = false;
                CheckBox_SUB_CESANTIAS.Checked = false;
                CheckBox_SUB_INT_CES.Checked = false;
                CheckBox_SUB_PRIMA.Checked = false;
                CheckBox_SUB_SEG_VID.Checked = false;

                CheckBox_RET_VAC.Checked = false;
                CheckBox_RET_CES.Checked = false;
                CheckBox_RET_INT_CES.Checked = false;
                CheckBox_RET_PRIM.Checked = false;

                TextBox_DIAS_VNC.Text = "";

                cargar_DropDownList_REGIMEN();

                TextBox_OBS_FACT.Text = "";

                habilitarSeccionServiciosParaDatosNuevos(true);
            }
            else
            { 
                DataRow infoCondicionesComerciales = tablaCondicionesHeredadas.Rows[0];

                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = "ADVERTENCIA: No se encontraron registros de condiciones económicas para mostrar, se heredaron las condiciones económicas de la entidad anterior a la seleccionada, por favor realice los cambios necesarios y guarde la información.";

                configurarBotonesDeAccion(false, true, true, true);

                Panel_CONTROL_REGISTRO.Visible = false;

                Panel_COD_CONDICIONES.Visible = false;

                cargar_campos_condiciones_comerciales(infoCondicionesComerciales);

                TextBox_USU_CRE.Text = "";
                TextBox_FCH_CRE.Text = "";
                TextBox_HOR_CRE.Text = "";
                TextBox_USU_MOD.Text = "";
                TextBox_FCH_MOD.Text = "";
                TextBox_HOR_MOD.Text = "";

                TextBox_COD_CONDICION.Text = "";


                DataTable tablaServiciosHeredados;

                if (ID_SUB_C != 0)
                {
                    tablaServiciosHeredados = _condicionComercial.ObtenerServiciosPorEmpresaPorIdCentroC(ID_CENTRO_C_1);
                }
                else
                {
                    if (ID_CENTRO_C != 0)
                    {
                        tablaServiciosHeredados = _condicionComercial.ObtenerServiciosPorEmpresaPorIdCiudad(ID_CIUDAD_1, ID_EMPRESA_1);
                    }
                    else
                    { 
                        tablaServiciosHeredados = _condicionComercial.ObtenerServiciosPorEmpresaPorIdEmpresa(ID_EMPRESA_1);
                    }
                }

                if (tablaServiciosHeredados.Rows.Count <= 0)
                {
                    habilitarSeccionServiciosParaDatosNuevos(true);
                }
                else
                {

                    List<servicio> listaServicios = new List<servicio>();
                    Session.Remove("listaServicios_" + ID_EMPRESA.ToString());
                    Session.Add("listaServicios_" + ID_EMPRESA.ToString(), listaServicios);
                    List<detalleServicio> listaDetallesServicio = new List<detalleServicio>();
                    Session.Remove("listaDetallesServicio_" + ID_EMPRESA.ToString());
                    Session.Add("listaDetallesServicio_" + ID_EMPRESA.ToString(), listaDetallesServicio);

                    cargarInformacionServiciosComplementariosDeUnaEntidad(tablaServiciosHeredados, true, true);
                }

                if ((String.IsNullOrEmpty(ID_CIUDAD) == true) && (ID_CENTRO_C == 0) && (ID_SUB_C == 0))
                {
                    Panel_ESTRUCTURA_CENTRO_COSTOS.Visible = true;
                    cargarEstructuraDeCC();

                    configurarMensajesCC(true, System.Drawing.Color.Red);
                    Label_MENSAJE_CC.Text = "Por favor seleccionar una ciudad de la lista de cobertura.";

                    configurarMensajesSUBCC(true, System.Drawing.Color.Red);
                    Label_MENSAJE_SUB_CC.Text = "Por favor seleccionar un centro de costo de la lista de centros de costo.";
                }
                else
                {
                    Panel_ESTRUCTURA_CENTRO_COSTOS.Visible = false;
                }
            }
        }
    }

    private void cargarCheckBox(CheckBox control, String valor)
    {
        if (valor == "N")
        {
            control.Checked = false;
        }
        else
        {
            if (valor == "S")
            {
                control.Checked = true;
            }
        }
    }
    
    private void limpiarTextBoxServicioAdicionar()
    {
        TextBox_NOMBRE_NUEVO_SERVICIO.Text = "";
        TextBox_NUEVO_AIU.Text = "0";
        TextBox_NUEVO_IVA.Text = "0,00";
        TextBox_NUEVA_TARIFA.Text = "0";
        TextBox_OBSERVACIONES.Text = "";
    }

    private void limpiarTextBoxServicioAdicionarSinNombreNuevoServicio()
    {
        TextBox_NUEVO_AIU.Text = "0";
        TextBox_NUEVO_IVA.Text = "0,00";
        TextBox_NUEVA_TARIFA.Text = "0";
        TextBox_OBSERVACIONES.Text = "";
    }

    private void configurarSeccionDeNuevoServicio(Boolean mostrarTotal, Boolean bPanel_NUEVO_SERVICIO_ADICIONAR_SERVICIO, Boolean bPanel_NUEVO_SERVICIO_SERVICIO_ADICINADO, Boolean bPanel_NUEVO_SERVICIO_INGRESAR_DETALLES, Boolean bPanel_NUEVO_SERVICIO_GRID_DETALLES, Boolean bPanel_NUEVO_SERVICIO_BOTON_INCLUIR_SERVICIO)
    {
        Panel_NUEVO_SERVICIO.Visible = mostrarTotal;

        Panel_NUEVO_SERVICIO_ADICIONAR_SERVICIO.Visible = bPanel_NUEVO_SERVICIO_ADICIONAR_SERVICIO;

        Panel_NUEVO_SERVICIO_SERVICIO_ADICINADO.Visible = bPanel_NUEVO_SERVICIO_SERVICIO_ADICINADO;

        Panel_NUEVO_SERVICIO_INGRESAR_DETALLES.Visible = bPanel_NUEVO_SERVICIO_INGRESAR_DETALLES;

        Panel_NUEVO_SERVICIO_GRID_DETALLES.Visible = bPanel_NUEVO_SERVICIO_GRID_DETALLES;

        Panel_NUEVO_SERVICIO_BOTON_INCLUIR_SERVICIO.Visible = bPanel_NUEVO_SERVICIO_BOTON_INCLUIR_SERVICIO;

    }
    #endregion

    #region metodos para capturar datos de los controles
    private String obtenerDeUnCheckBox(CheckBox check)
    {
        String resultado = "N";

        if (check.Checked == true)
        {
            resultado = "S";
        }

        return resultado;
    }
    private DataTable obtenerDataTableDeGridViewDetallesServicioNuevo()
    {
        DataTable tablaResultado = new DataTable();

        tablaResultado.Columns.Add("ID_SERVICIO");
        tablaResultado.Columns.Add("NOMBRE_SERVICIO");
        tablaResultado.Columns.Add("ID_SERVICIO_COMPLEMENTARIO");
        tablaResultado.Columns.Add("NOMBRE_SERVICIO_COMPLEMENTARIO");

        DataRow filaTablaResultado;

        String ID_SERVICIO;
        String NOMBRE_SERVICIO;
        String ID_SERVICIO_COMPLEMENTARIO;
        String NOMBRE_SERVICIO_COMPLEMENTARIO;

        for (int i = 0; i < GridView_DETALLES_NUEVO_SERVICIO.Rows.Count; i++)
        {
            ID_SERVICIO = GridView_DETALLES_NUEVO_SERVICIO.DataKeys[i].Values["ID_SERVICIO"].ToString();
            NOMBRE_SERVICIO = GridView_DETALLES_NUEVO_SERVICIO.DataKeys[i].Values["NOMBRE_SERVICIO"].ToString();
            ID_SERVICIO_COMPLEMENTARIO = GridView_DETALLES_NUEVO_SERVICIO.DataKeys[i].Values["ID_SERVICIO_COMPLEMENTARIO"].ToString();
            NOMBRE_SERVICIO_COMPLEMENTARIO = GridView_DETALLES_NUEVO_SERVICIO.Rows[i].Cells[4].Text;

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_SERVICIO"] = ID_SERVICIO;
            filaTablaResultado["NOMBRE_SERVICIO"] = NOMBRE_SERVICIO;
            filaTablaResultado["ID_SERVICIO_COMPLEMENTARIO"] = ID_SERVICIO_COMPLEMENTARIO;
            filaTablaResultado["NOMBRE_SERVICIO_COMPLEMENTARIO"] = NOMBRE_SERVICIO_COMPLEMENTARIO;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }
    private DataTable obtenerDataTableDeGridViewDetallesServicioActualSeleccionado()
    {
        DataTable tablaResultado = new DataTable();

        tablaResultado.Columns.Add("ID_SERVICIO");
        tablaResultado.Columns.Add("NOMBRE_SERVICIO");
        tablaResultado.Columns.Add("ID_SERVICIO_COMPLEMENTARIO");
        tablaResultado.Columns.Add("NOMBRE_SERVICIO_COMPLEMENTARIO");

        DataRow filaTablaResultado;

        String ID_SERVICIO;
        String NOMBRE_SERVICIO;
        String ID_SERVICIO_COMPLEMENTARIO;
        String NOMBRE_SERVICIO_COMPLEMENTARIO;

        for (int i = 0; i < GridView_DETALLES_SERVICIO_SELECCIONADO.Rows.Count; i++)
        {
            ID_SERVICIO = GridView_DETALLES_SERVICIO_SELECCIONADO.DataKeys[i].Values["ID_SERVICIO"].ToString();
            NOMBRE_SERVICIO = GridView_DETALLES_SERVICIO_SELECCIONADO.DataKeys[i].Values["NOMBRE_SERVICIO"].ToString();
            ID_SERVICIO_COMPLEMENTARIO = GridView_DETALLES_SERVICIO_SELECCIONADO.DataKeys[i].Values["ID_SERVICIO_COMPLEMENTARIO"].ToString();
            NOMBRE_SERVICIO_COMPLEMENTARIO = GridView_DETALLES_SERVICIO_SELECCIONADO.Rows[i].Cells[4].Text;

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_SERVICIO"] = ID_SERVICIO;
            filaTablaResultado["NOMBRE_SERVICIO"] = NOMBRE_SERVICIO;
            filaTablaResultado["ID_SERVICIO_COMPLEMENTARIO"] = ID_SERVICIO_COMPLEMENTARIO;
            filaTablaResultado["NOMBRE_SERVICIO_COMPLEMENTARIO"] = NOMBRE_SERVICIO_COMPLEMENTARIO;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }
    private DataTable obtenerDataTableDeGridViewServiciosActules()
    {
        DataTable tablaResultado = new DataTable();

        tablaResultado.Columns.Add("ID_EMPRESA");
        tablaResultado.Columns.Add("ID_SERVICIO");
        tablaResultado.Columns.Add("ID_SERVICIO_POR_EMPRESA");
        tablaResultado.Columns.Add("NOMBRE_SERVICIO");
        tablaResultado.Columns.Add("AIU");
        tablaResultado.Columns.Add("IVA");
        tablaResultado.Columns.Add("VALOR");
        tablaResultado.Columns.Add("observaciones");

        DataRow filaTablaResultado;

        String ID_EMPRESA;
        String ID_SERVICIO;
        String ID_SERVICIO_POR_EMPRESA;
        String NOMBRE_SERVICIO;
        String AIU;
        String IVA;
        String VALOR;
        String OBSERVACIONES;

        for (int i = 0; i < GridView_SERVICIOS_INCLUIDOS.Rows.Count; i++)
        {
            ID_EMPRESA = GridView_SERVICIOS_INCLUIDOS.DataKeys[i].Values["ID_EMPRESA"].ToString();
            ID_SERVICIO = GridView_SERVICIOS_INCLUIDOS.DataKeys[i].Values["ID_SERVICIO"].ToString();
            ID_SERVICIO_POR_EMPRESA = GridView_SERVICIOS_INCLUIDOS.DataKeys[i].Values["ID_SERVICIO_POR_EMPRESA"].ToString();
            NOMBRE_SERVICIO = GridView_SERVICIOS_INCLUIDOS.Rows[i].Cells[5].Text;
            AIU = GridView_SERVICIOS_INCLUIDOS.Rows[i].Cells[6].Text;
            IVA = GridView_SERVICIOS_INCLUIDOS.Rows[i].Cells[7].Text;
            VALOR = GridView_SERVICIOS_INCLUIDOS.Rows[i].Cells[8].Text;
            OBSERVACIONES = GridView_SERVICIOS_INCLUIDOS.Rows[i].Cells[9].Text;

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_EMPRESA"] = ID_SERVICIO;
            filaTablaResultado["ID_SERVICIO"] = ID_SERVICIO;
            filaTablaResultado["ID_SERVICIO_POR_EMPRESA"] = ID_SERVICIO_POR_EMPRESA;
            filaTablaResultado["NOMBRE_SERVICIO"] = NOMBRE_SERVICIO;
            filaTablaResultado["AIU"] = AIU;
            filaTablaResultado["IVA"] = IVA;
            filaTablaResultado["VALOR"] = VALOR;
            filaTablaResultado["observaciones"] = OBSERVACIONES;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }
    #endregion

    #region metodos que se ejecutan al cargar la pagina
    private void iniciar_interfaz_inicial()
    {
        capturarVariablesGlogales();
        if ((String.IsNullOrEmpty(ID_CIUDAD) == true) && (ID_CENTRO_C == 0) && (ID_SUB_C == 0))
        {
            configurarBotonesDeAccion(false, false, false, false);
        }
        else
        {
            configurarBotonesDeAccion(false, false, false, true);
        }
        
        configurarMensajes(false, System.Drawing.Color.Green);

        Panel_FORMULARIO.Visible = true;

        Panel_SERVICIOS_ACTUALES_GENERAL.Visible = false;

        Panel_BOTON_NUEVO_SERVICIO.Visible = false;

        Panel_NUEVO_SERVICIO.Visible = false;

        if ((ID_SUB_C == 0) && (ID_CENTRO_C == 0) && (String.IsNullOrEmpty(ID_CIUDAD) == true))
        {
            Panel_ESTRUCTURA_CENTRO_COSTOS.Visible = true;
        }
        else
        { 
            Panel_ESTRUCTURA_CENTRO_COSTOS.Visible = false;
        }

        cargarInfoCondiciones(false);
    }
    private void iniciarControlesNuevo()
    {
        if((String.IsNullOrEmpty(ID_CIUDAD) == true)&&(ID_CENTRO_C == 0)&&(ID_SUB_C == 0))
        {
            configurarBotonesDeAccion(false, false, true, false);
        }
        else
        {
            configurarBotonesDeAccion(false, false, true, true);
        }
        

        configurarMensajes(false, System.Drawing.Color.Green);

        Panel_FORMULARIO.Visible = true;

        Panel_CONTROL_REGISTRO.Visible = false;

        Panel_COD_CONDICIONES.Visible = false;
        

    }
    private void iniciar_interfaz_para_modificar_cliente()
    {
        if((String.IsNullOrEmpty(ID_CIUDAD) == true)&&(ID_CENTRO_C == 0)&&(ID_SUB_C == 0))
        {
            configurarBotonesDeAccion(false, true, true, false);
        }
        else
        {
            configurarBotonesDeAccion(false, true, true, true);
        }
        
        configurarMensajes(false, System.Drawing.Color.Green);

        Panel_FORMULARIO.Visible = true;

        cargarInfoCondiciones(true);
    }
    #endregion

    #region metodos generales
    private String determinarFuenteCondiciones()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String resultado = null;
        try
        {
            String dato = QueryStringSeguro["regCobertura"].ToString();
            resultado = "cobertura";
            return resultado;
        }
        catch
        {
            try
            {
                String dato = QueryStringSeguro["regCC"].ToString();
                resultado = "cc";
                return resultado;
            }
            catch
            {
                try
                {
                    String dato = QueryStringSeguro["regSubCC"].ToString();
                    resultado = "subcc";
                    return resultado;
                }
                catch
                {
                    resultado = "empresa";
                    return resultado;
                }
            }   
        }
    }
    private List<servicio> capturarListaServiciosDesdeSession()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String ID_EMPRESA = QueryStringSeguro["reg"].ToString();
        List<servicio> listaResultado;
        try
        {
            listaResultado = (List<servicio>)Session["listaServicios_" + ID_EMPRESA];
        }
        catch
        {
            listaResultado = new List<servicio>();
        }

        return listaResultado;

    }
    private List<detalleServicio> capturarListaDetallesServicioDesdeSession()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String ID_EMPRESA = QueryStringSeguro["reg"].ToString();
        List<detalleServicio> listaResultado;
        try
        {
            listaResultado = (List<detalleServicio>)Session["listaDetallesServicio_" + ID_EMPRESA];
        }
        catch
        {
            listaResultado = new List<detalleServicio>();
        }

        return listaResultado;

    }
    private void capturarVariablesGlogales()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

        try
        {
            ID_CIUDAD = QueryStringSeguro["codCiudad"].ToString();
        }
        catch
        {
            ID_CIUDAD = null;
        }

        try
        {
            ID_CENTRO_C = Convert.ToDecimal(QueryStringSeguro["codCC"]);
        }
        catch
        {
            ID_CENTRO_C = 0;
        }

        try
        {
            ID_SUB_C = Convert.ToDecimal(QueryStringSeguro["codSUBCC"]);
        }
        catch
        {
            ID_SUB_C = 0;
        }
    }
    #endregion

    private void configurar_paneles_popup(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_mensaje.CssClass = "panel_popup";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = "CONDICIONES ECONÓMICAS";

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        if (IsPostBack == false)
        {
            configurar_paneles_popup(Panel_FONDO_MENSAJE, Panel_MENSAJES);
            configurar_paneles_popup(Panel_FONDO_MENSAJES_SERVICIOS_ACTUALES, Panel_MENSAJES_SERVICIOS_ACTUALES);
            configurar_paneles_popup(Panel_FONDO_MENSAJES_NUEVO_SERVICIO, Panel_MENSAJES_NUEVO_SERVICIO);

            capturarVariablesGlogales();

            if (Session["idEmpresa"].ToString() == "3")
            {
                String accion = QueryStringSeguro["accion"].ToString();

                if (accion == "inicial")
                {
                    iniciar_interfaz_inicial();
                }
                else
                {
                    if (accion == "modificar")
                    {
                        iniciar_interfaz_para_modificar_cliente();
                    }
                    else
                    {
                        if (accion == "cargarNuevo")
                        {
                            if((String.IsNullOrEmpty(ID_CIUDAD) == true)&&(ID_CENTRO_C == 0)&&(ID_SUB_C == 0))
                            {
                                configurarBotonesDeAccion(false, false, false, false);
                            }
                            else
                            {
                                configurarBotonesDeAccion(false, false, false, false);
                            }
                            

                            configurarMensajes(false, System.Drawing.Color.Green);

                            Panel_FORMULARIO.Visible = true;

                            cargarInfoCondiciones(false);

                            configurarMensajes(true, System.Drawing.Color.Green);
                            if (ID_SUB_C != 0)
                            {
                                Label_MENSAJE.Text = "Las condiciones Económicas para el Sub centro de costo:  " + ID_SUB_C + " fueron creadas correctamente.";
                            }
                            else
                            {
                                if (ID_CENTRO_C != 0)
                                {
                                    Label_MENSAJE.Text = "Las condiciones Económicas para el Centro de costo:  " + ID_CENTRO_C + " fueron creadas correctamente.";
                                }
                                else
                                {
                                    if ((ID_EMPRESA != 0) && (String.IsNullOrEmpty(ID_CIUDAD) == false))
                                    {
                                        Label_MENSAJE.Text = "Las condiciones Económicas para la Cobertura:  " + ID_CIUDAD + " fueron creadas correctamente.";
                                    }
                                    else
                                    {
                                        Label_MENSAJE.Text = "Las condiciones Económicas para la Empresa:  " + ID_EMPRESA + " fueron creadas correctamente.";
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (accion == "cargarModificado")
                            {
                                if((String.IsNullOrEmpty(ID_CIUDAD) == true)&&(ID_CENTRO_C == 0)&&(ID_SUB_C == 0))
                                {
                                    configurarBotonesDeAccion(false, false, false, false);
                                }
                                else
                                {
                                    configurarBotonesDeAccion(false, false, false, true);
                                }
                                

                                configurarMensajes(false, System.Drawing.Color.Green);

                                Panel_FORMULARIO.Visible = true;

                                cargarInfoCondiciones(false);

                                configurarMensajes(true, System.Drawing.Color.Green);
                                String REGISTRO = QueryStringSeguro["codCondicion"].ToString();
                                if (ID_SUB_C != 0)
                                {
                                    Label_MENSAJE.Text = "Las condiciones Económicas para el Sub centro de costo:  " + ID_SUB_C + " fueron modificadas correctamente.";
                                }
                                else
                                {
                                    if (ID_CENTRO_C != 0)
                                    {
                                        Label_MENSAJE.Text = "Las condiciones Económicas para el Centro de costo:  " + ID_CENTRO_C + " fueron modificadas correctamente.";
                                    }
                                    else
                                    {
                                        if ((ID_EMPRESA != 0) && (String.IsNullOrEmpty(ID_CIUDAD) == false))
                                        {
                                            Label_MENSAJE.Text = "Las condiciones Económicas para la Cobertura:  " + ID_CIUDAD + " fueron modificadas correctamente.";
                                        }
                                        else
                                        {
                                            Label_MENSAJE.Text = "Las condiciones Económicas para la Empresa:  " + ID_EMPRESA + " fueron modificadas correctamente.";
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                Panel_FORM_BOTONES.Visible = false;
                Panel_FORM_BOTONES_1.Visible = false;
                Panel_FORMULARIO.Visible = false;

                Panel_SECCION_SERVICIOS.Visible = false;

                Panel_ESTRUCTURA_CENTRO_COSTOS.Visible = false;

                configurarMensajes(true, System.Drawing.Color.Red);

                Label_MENSAJE.Text = "ADVERTENCIA: Sesión de EMPRESA incorrecta, formulario destinado a configurar condiciones esonómicas de clientes de Eficiencia & Servicios.";
            }
        }
    }
    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        String REGISTRO = TextBox_COD_CONDICION.Text.Trim();

        capturarVariablesGlogales();

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = "comercial";
        QueryStringSeguro["nombre_area"] = "GESTIÓN COMERCIAL";
        QueryStringSeguro["nombre_modulo"] = "CONDICIONES ECONÓMICAS";
        QueryStringSeguro["reg"] = ID_EMPRESA.ToString();
        QueryStringSeguro["accion"] = "modificar";
        QueryStringSeguro["codCondicion"] = REGISTRO;

        if (ID_SUB_C != 0)
        {
            QueryStringSeguro["codSUBCC"] = ID_SUB_C.ToString();
        }
        else
        {
            if (ID_CENTRO_C != 0)
            {
                QueryStringSeguro["codCC"] = ID_CENTRO_C.ToString();
            }
            else
            {
                if ((ID_EMPRESA != 0) && (String.IsNullOrEmpty(ID_CIUDAD) == false))
                {
                    QueryStringSeguro["codCiudad"] = ID_CIUDAD.ToString();
                }
            }
        }

        Response.Redirect("~/comercial/condicionesEconomicasEficiencia.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }
    private void seEjecutaAlPresionarBotonGuardar()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String accion = QueryStringSeguro["accion"].ToString();

        capturarVariablesGlogales();
        condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        String FACTURA = obtenerDeUnCheckBox(CheckBox_FACTURA_NOMINA);

        String MOD_SOPORTE = DropDownList_MOD_SOPORTE.SelectedValue;
        String MOD_FACTURA = DropDownList_MOD_FACTURA.SelectedValue;

        Decimal AD_NOM = Convert.ToDecimal(TextBox_AD_NOM.Text);

        String SOLO_DEV = obtenerDeUnCheckBox(CheckBox_SOLO_DEV);

        Decimal AD_PENSION = Convert.ToDecimal(TextBox_AD_PENSION.Text);
        Decimal AD_SALUD = Convert.ToDecimal(TextBox_AD_SALUD.Text);
        Decimal AD_RIESGOS = Convert.ToDecimal(TextBox_AD_RIESGOS.Text);
        Decimal AD_APO_SENA = Convert.ToDecimal(TextBox_AD_APO_SENA.Text);
        Decimal AD_APO_ICBF = Convert.ToDecimal(TextBox_AD_APO_ICBF.Text);
        Decimal AD_APO_CAJA = Convert.ToDecimal(TextBox_AD_APO_CAJA.Text);
        
        
        Decimal AD_VACACIONES = Convert.ToDecimal(TextBox_AD_VACACIONES.Text);
        Decimal AD_CESANTIA = Convert.ToDecimal(TextBox_AD_CESANTIA.Text);
        Decimal AD_INT_CES = Convert.ToDecimal(TextBox_AD_INT_CES.Text);
        Decimal AD_PRIMA = Convert.ToDecimal(TextBox_AD_PRIMA.Text);
        Decimal AD_SEG_VID = Convert.ToDecimal(TextBox_AD_SEG_VID.Text);
        Boolean verificadorValoresPorcentajes = true;

        if (CheckBox_RET_VAC.Checked == false)
        {
            if (AD_VACACIONES == 0)
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = "ADVERTENCIA: El % para VACACIONES solo puede ir en CERO si esta seleccionado SE COBRA AL RETIRO.";
                verificadorValoresPorcentajes = false;
            }
        }

        if (CheckBox_RET_CES.Checked == false)
        {
            if (AD_CESANTIA == 0)
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = "ADVERTENCIA: El % para CESANTÍA solo puede ir en CERO si esta seleccionado SE COBRA AL RETIRO.";
                verificadorValoresPorcentajes = false;
            }
        }

        if (CheckBox_RET_INT_CES.Checked == false)
        {
            if (AD_INT_CES == 0)
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = "ADVERTENCIA: El % para INT CESANTÍA solo puede ir en CERO si esta seleccionado SE COBRA AL RETIRO.";
                verificadorValoresPorcentajes = false;
            }
        }

        if (CheckBox_RET_PRIM.Checked == false)
        {
            if (AD_PRIMA == 0)
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = "ADVERTENCIA: El % para PRIMA solo puede ir en CERO si esta seleccionado SE COBRA AL RETIRO.";
                verificadorValoresPorcentajes = false;
            }
        }

        if (verificadorValoresPorcentajes == true)
        {
            String SUB_PENSION = obtenerDeUnCheckBox(CheckBox_SUB_PENSION);
            String SUB_SALUD = obtenerDeUnCheckBox(CheckBox_SUB_SALUD);
            String SUB_RIESGO = obtenerDeUnCheckBox(CheckBox_SUB_RIESGO);
            String SUB_SENA = obtenerDeUnCheckBox(CheckBox_SUB_SENA);
            String SUB_ICBF = obtenerDeUnCheckBox(CheckBox_SUB_ICBF);
            String SUB_CAJA = obtenerDeUnCheckBox(CheckBox_SUB_CAJA);
            String SUB_VACACIONES = obtenerDeUnCheckBox(CheckBox_SUB_VACACIONES);
            String SUB_CESANTIAS = obtenerDeUnCheckBox(CheckBox_SUB_CESANTIAS);
            String SUB_INT_CES = obtenerDeUnCheckBox(CheckBox_SUB_INT_CES);
            String SUB_PRIMA = obtenerDeUnCheckBox(CheckBox_SUB_PRIMA);
            String SUB_SEG_VID = obtenerDeUnCheckBox(CheckBox_SUB_SEG_VID);

            String RET_VAC = obtenerDeUnCheckBox(CheckBox_RET_VAC);
            String RET_CES = obtenerDeUnCheckBox(CheckBox_RET_CES);
            String RET_INT_CES = obtenerDeUnCheckBox(CheckBox_RET_INT_CES);
            String RET_PRIM = obtenerDeUnCheckBox(CheckBox_RET_PRIM);

            int DIAS_VNC = Convert.ToInt32(TextBox_DIAS_VNC.Text);

            String REGIMEN = DropDownList_REGIMEN.SelectedValue;

            String OBS_FACT = _tools.RemplazarCaracteresEnString(TextBox_OBS_FACT.Text.Trim());

            String USU_CRE = Session["USU_LOG"].ToString();
            String USU_MOD = Session["USU_LOG"].ToString();

            String VAC_PARAF = "N"; 

            String APL_MTZ = obtenerDeUnCheckBox(CheckBox_APL_MTZ);


            List<servicio> listaServicios = capturarListaServiciosDesdeSession();
            List<detalleServicio> listaDetallesServicio = capturarListaDetallesServicioDesdeSession();

            Decimal REGISTRO = 0;
            if (accion == "inicial")
            {
                REGISTRO = _condicionComercial.Adicionar(ID_EMPRESA, FACTURA, REGIMEN, SOLO_DEV, VAC_PARAF, DIAS_VNC, AD_NOM, AD_PENSION, AD_SALUD, AD_RIESGOS, AD_APO_SENA, AD_APO_ICBF, AD_APO_CAJA, AD_VACACIONES, AD_CESANTIA, AD_INT_CES, AD_PRIMA, AD_SEG_VID, SUB_PENSION, SUB_SALUD, SUB_RIESGO, SUB_SENA, SUB_ICBF, SUB_CAJA, SUB_VACACIONES, SUB_CESANTIAS, SUB_INT_CES, SUB_PRIMA, SUB_SEG_VID, MOD_SOPORTE, MOD_FACTURA, OBS_FACT, RET_VAC, RET_CES, RET_INT_CES, RET_PRIM, USU_CRE, APL_MTZ, ID_CIUDAD, ID_CENTRO_C, ID_SUB_C, listaServicios, listaDetallesServicio);

                if (REGISTRO == 0)
                {
                    configurarMensajes(true, System.Drawing.Color.Red);
                    Label_MENSAJE.Text = _condicionComercial.MensajeError;
                }
                else
                {
                    QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

                    QueryStringSeguro["img_area"] = "comercial";
                    QueryStringSeguro["nombre_area"] = "GESTIÓN COMERCIAL";
                    QueryStringSeguro["nombre_modulo"] = "CONDICIONES ECONÓMICAS";
                    QueryStringSeguro["accion"] = "cargarNuevo";
                    QueryStringSeguro["reg"] = ID_EMPRESA.ToString();
                    QueryStringSeguro["codCondicion"] = REGISTRO.ToString();

                    if (ID_SUB_C != 0)
                    {
                        QueryStringSeguro["codSUBCC"] = ID_SUB_C.ToString();
                    }
                    else
                    {
                        if (ID_CENTRO_C != 0)
                        {
                            QueryStringSeguro["codCC"] = ID_CENTRO_C.ToString();
                        }
                        else
                        {
                            if ((ID_EMPRESA != 0) && (String.IsNullOrEmpty(ID_CIUDAD) == false))
                            {
                                QueryStringSeguro["codCiudad"] = ID_CIUDAD;
                            }
                        }
                    }

                    Response.Redirect("~/comercial/condicionesEconomicasEficiencia.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
                }
            }
            else
            {
                if (accion == "modificar")
                {
                    Boolean verificador = true;

                    REGISTRO = Convert.ToDecimal(QueryStringSeguro["codCondicion"]);

                    verificador = _condicionComercial.Actualizar(REGISTRO, ID_EMPRESA, FACTURA, REGIMEN, SOLO_DEV, VAC_PARAF, DIAS_VNC, AD_NOM, AD_PENSION, AD_SALUD, AD_RIESGOS, AD_APO_SENA, AD_APO_ICBF, AD_APO_CAJA, AD_VACACIONES, AD_CESANTIA, AD_INT_CES, AD_PRIMA, AD_SEG_VID, SUB_PENSION, SUB_SALUD, SUB_RIESGO, SUB_SENA, SUB_ICBF, SUB_CAJA, SUB_VACACIONES, SUB_CESANTIAS, SUB_INT_CES, SUB_PRIMA, SUB_SEG_VID, MOD_SOPORTE, MOD_FACTURA, OBS_FACT, RET_VAC, RET_CES, RET_INT_CES, RET_PRIM, USU_MOD, APL_MTZ, ID_CIUDAD, ID_CENTRO_C, ID_SUB_C, listaServicios, listaDetallesServicio);

                    if (verificador == false)
                    {
                        configurarMensajes(true, System.Drawing.Color.Red);
                        Label_MENSAJE.Text = _condicionComercial.MensajeError;
                    }
                    else
                    {
                        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

                        QueryStringSeguro["img_area"] = "comercial";
                        QueryStringSeguro["nombre_area"] = "GESTIÓN COMERCIAL";
                        QueryStringSeguro["nombre_modulo"] = "CONDICIONES ECONÓMICAS";
                        QueryStringSeguro["accion"] = "cargarModificado";

                        QueryStringSeguro["reg"] = ID_EMPRESA.ToString();

                        QueryStringSeguro["codCondicion"] = REGISTRO.ToString();

                        if (ID_SUB_C != 0)
                        {
                            QueryStringSeguro["codSUBCC"] = ID_SUB_C.ToString();
                        }
                        else
                        {
                            if (ID_CENTRO_C != 0)
                            {
                                QueryStringSeguro["codCC"] = ID_CENTRO_C.ToString();
                            }
                            else
                            {
                                if ((ID_EMPRESA != 0) && (String.IsNullOrEmpty(ID_CIUDAD) == false))
                                {
                                    QueryStringSeguro["codCiudad"] = ID_CIUDAD;
                                }
                            }
                        }

                        Response.Redirect("~/comercial/condicionesEconomicasEficiencia.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));

                    }
                }
            }
        }
    }
    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        if (GridView_SERVICIOS_INCLUIDOS.Rows.Count <= 0)
        {
            configurarMensajes(true, System.Drawing.Color.Red);
            Label_MENSAJE.Text = "ADVERTENCIA: Para poder guardar las condiciones económicas debe incluir por lo menos un servicio a la lista.";
        }
        else
        {
            seEjecutaAlPresionarBotonGuardar();
        }
        
    }
    protected void GridView_DETALLES_NUEVO_SERVICIO_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable tablaDetallesEnGrid = obtenerDataTableDeGridViewDetallesServicioNuevo();

        DataRow filaSeleccionada = tablaDetallesEnGrid.Rows[GridView_DETALLES_NUEVO_SERVICIO.SelectedIndex];

        tablaDetallesEnGrid.Rows[GridView_DETALLES_NUEVO_SERVICIO.SelectedIndex].Delete();

        GridView_DETALLES_NUEVO_SERVICIO.DataSource = tablaDetallesEnGrid;
        GridView_DETALLES_NUEVO_SERVICIO.DataBind();
    }
    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        capturarVariablesGlogales();

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = "comercial";
        QueryStringSeguro["nombre_area"] = "GESTIÓN COMERCIAL";
        QueryStringSeguro["nombre_modulo"] = "CONDICIONES ECONÓMICAS";
        QueryStringSeguro["accion"] = "inicial";
        QueryStringSeguro["reg"] = ID_EMPRESA.ToString();

        if (ID_SUB_C != 0)
        {
            QueryStringSeguro["codSUBCC"] = ID_SUB_C.ToString();
        }
        else
        {
            if (ID_CENTRO_C != 0)
            {
                QueryStringSeguro["codCC"] = ID_CENTRO_C.ToString();
            }
            else
            {
                if ((ID_EMPRESA != 0) && (String.IsNullOrEmpty(ID_CIUDAD) == false))
                {
                    QueryStringSeguro["codCiudad"] = ID_CIUDAD;
                }
            }
        }

        Response.Redirect("~/comercial/condicionesEconomicasEficiencia.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));

    }
    protected void GridView_COBERTURA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        if (e.CommandName == "Ciudad")
        {
            int filaSeleccionada = Convert.ToInt32(e.CommandArgument);

            Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
            String ID_CIUDAD = GridView_COBERTURA.DataKeys[filaSeleccionada].Value.ToString();

            cargar_GridView_CENTROS_DE_COSTO(ID_EMPRESA, ID_CIUDAD);
        }
    }
    protected void GridView_CENTROS_DE_COSTO_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        if (e.CommandName == "centrocosto")
        {
            int filaSeleccionada = Convert.ToInt32(e.CommandArgument);

            Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
            Decimal ID_CENTRO_C = Convert.ToDecimal(GridView_CENTROS_DE_COSTO.DataKeys[filaSeleccionada].Value);

            cargar_GridView_SUB_CENTROS_DE_COSTO(ID_EMPRESA, ID_CENTRO_C);
        }
    }
    protected void GridView_COBERTURA_SelectedIndexChanged(object sender, EventArgs e)
    {
        capturarVariablesGlogales();

        ID_CIUDAD = GridView_COBERTURA.SelectedDataKey["Código Ciudad"].ToString();

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = "comercial";
        QueryStringSeguro["nombre_area"] = "GESTIÓN COMERCIAL";
        QueryStringSeguro["nombre_modulo"] = "CONDICIONES ECONÓMICAS";
        QueryStringSeguro["accion"] = "inicial";
        QueryStringSeguro["reg"] = ID_EMPRESA.ToString();
        QueryStringSeguro["codCiudad"] = ID_CIUDAD;

        Response.Redirect("~/comercial/condicionesEconomicasEficiencia.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }

    protected void GridView_CENTROS_DE_COSTO_SelectedIndexChanged(object sender, EventArgs e)
    {
        capturarVariablesGlogales();

        ID_CENTRO_C = Convert.ToDecimal(GridView_CENTROS_DE_COSTO.SelectedDataKey["ID_CENTRO_C"]);

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = "comercial";
        QueryStringSeguro["nombre_area"] = "GESTIÓN COMERCIAL";
        QueryStringSeguro["nombre_modulo"] = "CONDICIONES ECONÓMICAS";
        QueryStringSeguro["accion"] = "inicial";
        QueryStringSeguro["reg"] = ID_EMPRESA.ToString();
        QueryStringSeguro["codCC"] = ID_CENTRO_C.ToString();

        Response.Redirect("~/comercial/condicionesEconomicasEficiencia.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }

    protected void GridView_SUB_CENTROS_DE_COSTO_SelectedIndexChanged(object sender, EventArgs e)
    {
        ID_SUB_C = Convert.ToDecimal(GridView_SUB_CENTROS_DE_COSTO.SelectedDataKey["ID_SUB_C"]);

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = "comercial";
        QueryStringSeguro["nombre_area"] = "GESTIÓN COMERCIAL";
        QueryStringSeguro["nombre_modulo"] = "CONDICIONES ECONÓMICAS";
        QueryStringSeguro["accion"] = "inicial";
        QueryStringSeguro["reg"] = ID_EMPRESA.ToString();
        QueryStringSeguro["codSUBCC"] = ID_SUB_C.ToString();

        Response.Redirect("~/comercial/condicionesEconomicasEficiencia.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }
    protected void Button_INGRESAR_DETALLE_AL_SERVICIO_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        DataTable tablaDetallesEnGrid = obtenerDataTableDeGridViewDetallesServicioNuevo();

        Boolean verificador = true;

        foreach (DataRow fila in tablaDetallesEnGrid.Rows)
        {
            if (fila["ID_SERVICIO_COMPLEMENTARIO"].ToString() == DropDownList_DETALLES_SERVICIO.SelectedValue)
            {
                verificador = false;
            }
        }

        if (verificador == true)
        {
            configurarMensajesNuevoServicio(false, System.Drawing.Color.Red);

            DataRow filaDetalleAAdicionar = tablaDetallesEnGrid.NewRow();

            condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

            filaDetalleAAdicionar["ID_SERVICIO"] = 0;
            filaDetalleAAdicionar["NOMBRE_SERVICIO"] = Label_NOMBRE_NUEVO_SERVICIO.Text.ToUpper();
            filaDetalleAAdicionar["ID_SERVICIO_COMPLEMENTARIO"] = DropDownList_DETALLES_SERVICIO.SelectedValue;
            filaDetalleAAdicionar["NOMBRE_SERVICIO_COMPLEMENTARIO"] = DropDownList_DETALLES_SERVICIO.SelectedItem.Text;

            tablaDetallesEnGrid.Rows.Add(filaDetalleAAdicionar);

            DataView DV = new DataView(tablaDetallesEnGrid, "", "NOMBRE_SERVICIO_COMPLEMENTARIO", DataViewRowState.CurrentRows);
            GridView_DETALLES_NUEVO_SERVICIO.DataSource = DV;
            GridView_DETALLES_NUEVO_SERVICIO.DataBind();

            cargar_DropDownList_DETALLES_SERVICIO();

        }
        else
        {
            configurarMensajesNuevoServicio(true, System.Drawing.Color.Red);
            Label_MENSAJES_NUEVO_SERVICIO.Text = "ADVERTENCIA: El detalle que desea incluir ya se encuentra en la lista por lo tanto no fue ingresado.";
        }

        configurarSeccionDeNuevoServicio(true, false, true, true, true, true);
    }
    private Boolean comprobar_no_null_o_no_cero(String texto)
    {
        if (String.IsNullOrEmpty(texto) == true)
        {
            return false;
        }
        else
        {
            if (Convert.ToDecimal(texto) > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
    protected void Button_COMPROBAR_NUEVO_SERVICIO_Click(object sender, EventArgs e)
    {
        Boolean verificador = true;


        if (DropDownList_CONFIGURACION.SelectedValue == "AIU")
        {
            if (Convert.ToDecimal(TextBox_NUEVO_AIU.Text) <= 0)
            {
                verificador = false;
                configurarMensajesNuevoServicio(true, System.Drawing.Color.Red);
                Label_MENSAJES_NUEVO_SERVICIO.Text = "(Configuración: AIU), Debe digitar el PORCENTAJE DE ADMINISTRACIÓN para poder continuar.";
            }
        }
        else
        {
            if (DropDownList_CONFIGURACION.SelectedValue == "AIU_PREFERENCIAL")
            {
                if (Convert.ToDecimal(TextBox_NUEVO_AIU.Text) <= 0)
                {
                    verificador = false;
                    configurarMensajesNuevoServicio(true, System.Drawing.Color.Red);
                    Label_MENSAJES_NUEVO_SERVICIO.Text = "(Configuración: AIU PREFERENCIAL), Debe digitar el PORCENTAJE DE ADMINISTRACIÓN para poder continuar.";
                }
            }
            else
            {
                if (DropDownList_CONFIGURACION.SelectedValue == "TARIFA")
                {
                    if (Convert.ToDecimal(TextBox_NUEVA_TARIFA.Text) <= 0)
                    {
                        verificador = false;
                        configurarMensajesNuevoServicio(true, System.Drawing.Color.Red);
                        Label_MENSAJES_NUEVO_SERVICIO.Text = "(Configuración: TARIFA), Debe digitar el valorde la TARIFA para poder continuar.";
                    }
                }
                else
                {
                    if (DropDownList_CONFIGURACION.SelectedValue == "IVA")
                    {
                        if (Convert.ToDecimal(TextBox_NUEVO_IVA.Text) <= 0)
                        {
                            verificador = false;
                            configurarMensajesNuevoServicio(true, System.Drawing.Color.Red);
                            Label_MENSAJES_NUEVO_SERVICIO.Text = "(Configuración: IVA), Debe digitar el valorde del IVA para poder continuar.";
                        }
                    }
                }
            }
        }

        if (verificador == true)
        {
            configurarMensajesNuevoServicio(false, System.Drawing.Color.Red);

            DataTable tablaServiciosActualesEnGrid = obtenerDataTableDeGridViewServiciosActules();

            foreach (DataRow fila in tablaServiciosActualesEnGrid.Rows)
            {
                if (fila["NOMBRE_SERVICIO"].ToString().Trim() == TextBox_NOMBRE_NUEVO_SERVICIO.Text.ToUpper())
                {
                    verificador = false;
                }
            }

            if (verificador == true)
            {
                configurarSeccionDeNuevoServicio(true, false, true, true, false, true);

                Label_NOMBRE_NUEVO_SERVICIO.Text = TextBox_NOMBRE_NUEVO_SERVICIO.Text.ToUpper();

                if (String.IsNullOrEmpty(TextBox_NUEVO_AIU.Text) == false)
                {
                    Label_AIU_NUEVO_SERVICIO.Text = TextBox_NUEVO_AIU.Text.ToUpper();
                }
                else
                {
                    Label_AIU_NUEVO_SERVICIO.Text = "0";
                }

                if (String.IsNullOrEmpty(TextBox_NUEVO_IVA.Text) == false)
                {
                    Label_IVA_NUEVO_SERVICIO.Text = TextBox_NUEVO_IVA.Text;
                }
                else
                {
                    Label_IVA_NUEVO_SERVICIO.Text = "0";
                }

                if (String.IsNullOrEmpty(TextBox_NUEVA_TARIFA.Text) == false)
                {
                    Label_TARIFA_NUEVO_SERVICIO.Text = TextBox_NUEVA_TARIFA.Text;
                }
                else
                {
                    Label_TARIFA_NUEVO_SERVICIO.Text = "0";
                }

                Label_OBSERVACIONES.Text = TextBox_OBSERVACIONES.Text.ToUpper();

                cargar_DropDownList_DETALLES_SERVICIO();
            }
            else
            {
                configurarMensajesNuevoServicio(true, System.Drawing.Color.Red);
                Label_MENSAJES_NUEVO_SERVICIO.Text = "ADVERTENCIA: El Servicio que desea ingresar ya se encuentra en la lista de servicios incluidos actualmente, no se puede configurar este servicio";
            }
        }
    }
    protected void Button_CANCELAR_NUEVO_SERVICIO_Click(object sender, EventArgs e)
    {
        Panel_BOTON_NUEVO_SERVICIO.Visible = true;

        configurarSeccionDeNuevoServicio(false, false, false, false, false, false);
    }
    protected void Button_NUEVO_SERVICIO_Click(object sender, EventArgs e)
    {
        Panel_BOTON_NUEVO_SERVICIO.Visible = false;

        configurarSeccionDeNuevoServicio(true, true, false, false, false, false);

        cargar_DropDownList_CONFIGURACION();

        limpiarTextBoxServicioAdicionar();

        configurarMensajesNuevoServicio(false, System.Drawing.Color.Red);

        GridView_DETALLES_NUEVO_SERVICIO.DataSource = null;
        GridView_DETALLES_NUEVO_SERVICIO.DataBind();
    }
    protected void Button_INCLUIR_NUEVO_SERVICIO_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String ID_EMPRESA = QueryStringSeguro["reg"].ToString();

        if (GridView_DETALLES_NUEVO_SERVICIO.Rows.Count <= 0)
        {
            configurarMensajesNuevoServicio(true, System.Drawing.Color.Red);
            Label_MENSAJES_NUEVO_SERVICIO.Text = "ADVERTENCIA: Para poder incluir un servicio a la lista debe primero asignarle uno o más detalles.";
        }
        else
        {
            List<servicio> listaServicios = capturarListaServiciosDesdeSession();
            List<detalleServicio> listaDetallesServicio= capturarListaDetallesServicioDesdeSession();

            servicio _servicio = new servicio(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            Decimal ID_SERVICIO_POR_EMPRESA = 0;
            Decimal ID_SERVICIO = 0;
            String NOMBRE_SERVICIO = Label_NOMBRE_NUEVO_SERVICIO.Text.ToUpper();
            Decimal AIU = Convert.ToDecimal(Label_AIU_NUEVO_SERVICIO.Text);
            Decimal IVA = Convert.ToDecimal(Label_IVA_NUEVO_SERVICIO.Text);
            Decimal VALOR = Convert.ToDecimal(Label_TARIFA_NUEVO_SERVICIO.Text);
            String OBSERVACIONES = Label_OBSERVACIONES.Text.ToUpper();

            _servicio.ACCION = "INSERTAR";
            _servicio.AIU = AIU;
            _servicio.ID_SERVICIO_POR_EMPRESA = ID_SERVICIO_POR_EMPRESA;
            _servicio.ID_SERVICIO = 0;
            _servicio.IVA = IVA;
            _servicio.NOMBRE_SERVICIO = NOMBRE_SERVICIO;
            _servicio.VALOR = VALOR;
            _servicio.DESCRIPCION = OBSERVACIONES;
            listaServicios.Add(_servicio);

            Decimal ID_DETALLE;
            detalleServicio _detalleServicio;

            for (int i = 0; i < GridView_DETALLES_NUEVO_SERVICIO.Rows.Count; i++ )
            {
                ID_DETALLE = Convert.ToDecimal(GridView_DETALLES_NUEVO_SERVICIO.DataKeys[i].Values["ID_SERVICIO_COMPLEMENTARIO"]);

                _detalleServicio = new detalleServicio(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

                _detalleServicio.ACCION = "INSERTAR";
                _detalleServicio.AIU = 0;
                _detalleServicio.ID_DETALLE_SERVICIO = 0;
                _detalleServicio.ID_SERVICIO = 0;
                _detalleServicio.ID_SERVICIO_COMPLEMENTARIO = ID_DETALLE;
                _detalleServicio.IVA = 0;
                _detalleServicio.NOMBRE_SERVICIO = NOMBRE_SERVICIO;
                _detalleServicio.VALOR = 0;
                listaDetallesServicio.Add(_detalleServicio);
            }

            DataTable tablaServiciosActuales = obtenerDataTableDeGridViewServiciosActules();

            DataRow filaTablaServiciosActuales = tablaServiciosActuales.NewRow();

            filaTablaServiciosActuales["ID_EMPRESA"] = ID_EMPRESA;
            filaTablaServiciosActuales["ID_SERVICIO"] = ID_SERVICIO;
            filaTablaServiciosActuales["ID_SERVICIO_POR_EMPRESA"] = ID_SERVICIO_POR_EMPRESA;
            filaTablaServiciosActuales["NOMBRE_SERVICIO"] = NOMBRE_SERVICIO;
            filaTablaServiciosActuales["AIU"] = AIU;
            filaTablaServiciosActuales["IVA"] = IVA;
            filaTablaServiciosActuales["VALOR"] = VALOR;
            filaTablaServiciosActuales["observaciones"] = OBSERVACIONES;

            tablaServiciosActuales.Rows.Add(filaTablaServiciosActuales);

            DataView DV = new DataView(tablaServiciosActuales, "", "NOMBRE_SERVICIO", DataViewRowState.CurrentRows);
            GridView_SERVICIOS_INCLUIDOS.DataSource = DV;
            GridView_SERVICIOS_INCLUIDOS.DataBind();

            configurarMensajesServiciosActuales(false, System.Drawing.Color.Green);

            Panel_GRID_SERVICIOS_ACTUALES.Visible = true;

            Panel_GRID_DETALLES_SERVICIO_SELECCIONADO.Visible = false;

            configurarSeccionDeNuevoServicio(false, false, false, false, false, false);

            Panel_BOTON_NUEVO_SERVICIO.Visible = true;
        }       
    }
    protected void GridView_SERVICIOS_INCLUIDOS_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "seleccionar")
        {
            int filaSeleccionada = Convert.ToInt32(e.CommandArgument);

            String NOMBRE_SERVICIO = GridView_SERVICIOS_INCLUIDOS.Rows[filaSeleccionada].Cells[5].Text;
            Decimal ID_SERVICIO_POR_EMPRESA = Convert.ToDecimal(GridView_SERVICIOS_INCLUIDOS.DataKeys[filaSeleccionada].Values["ID_SERVICIO_POR_EMPRESA"]);
            Decimal ID_SERVICIO = Convert.ToDecimal(GridView_SERVICIOS_INCLUIDOS.DataKeys[filaSeleccionada].Values["ID_SERVICIO"]);

            for (int i = 0; i < GridView_SERVICIOS_INCLUIDOS.Rows.Count; i++)
            {
                if (i == filaSeleccionada)
                {
                    GridView_SERVICIOS_INCLUIDOS.Rows[i].BackColor = System.Drawing.Color.Yellow;
                }
                else
                {
                    GridView_SERVICIOS_INCLUIDOS.Rows[i].BackColor = System.Drawing.Color.Transparent;
                }
            }

            cargar_GridView_DETALLES_SERVICIO_SELECCIONADO(ID_SERVICIO, NOMBRE_SERVICIO, ID_SERVICIO_POR_EMPRESA);
        }
    }

    private void eliminar_servicio_de_grilla_y_lista_session(DataTable tablaServiciosEnGrid, int indexSeleciconado)
    {
        DataRow filaSeleccionada = tablaServiciosEnGrid.Rows[indexSeleciconado];

        Decimal ID_SERVICIO = Convert.ToDecimal(filaSeleccionada["ID_SERVICIO"]);
        Decimal ID_SERVICIO_POR_EMPRESA = Convert.ToDecimal(filaSeleccionada["ID_SERVICIO_POR_EMPRESA"]);
        String NOMBRE_SERVICIO = filaSeleccionada["NOMBRE_SERVICIO"].ToString();
        Decimal AIU = Convert.ToDecimal(filaSeleccionada["AIU"]);
        Decimal IVA = Convert.ToDecimal(filaSeleccionada["IVA"]);
        Decimal VALOR = Convert.ToDecimal(filaSeleccionada["VALOR"]);
        String OBSERVACIONES = filaSeleccionada["observaciones"].ToString();

        List<servicio> listaServicios = capturarListaServiciosDesdeSession();
        List<detalleServicio> listaDetallesServicio = capturarListaDetallesServicioDesdeSession();

        servicio _servicio;

        int indexAEliminar = -1;
        for (int i = 0; i < listaServicios.Count; i++)
        {
            _servicio = listaServicios[i];
            if ((_servicio.ID_SERVICIO_POR_EMPRESA == ID_SERVICIO_POR_EMPRESA) &&(_servicio.ID_SERVICIO == ID_SERVICIO) && (_servicio.NOMBRE_SERVICIO == NOMBRE_SERVICIO))
            {
                indexAEliminar = i;
                break;
            }
        }

        if (indexAEliminar != -1)
        {
            if((ID_SERVICIO == 0) && (ID_SERVICIO_POR_EMPRESA == 0))
            {
                listaServicios.RemoveAt(indexAEliminar);
            }
            else
            {
                listaServicios[indexAEliminar].ACCION = "DESACTIVAR";
            }
        }

        List<int> listaIndicesAEliminar = new List<int>();
        detalleServicio _detalleServicio;

        for (int i = 0; i < listaDetallesServicio.Count; i++)
        {
            _detalleServicio = listaDetallesServicio[i];
            if ((_detalleServicio.ID_SERVICIO_POR_EMPRESA == ID_SERVICIO_POR_EMPRESA) && (_detalleServicio.ID_SERVICIO == ID_SERVICIO) && (_detalleServicio.NOMBRE_SERVICIO == NOMBRE_SERVICIO))
            {
                listaIndicesAEliminar.Add(i);
            }
        }

        for (int i = (listaIndicesAEliminar.Count - 1); i >= 0; i--)
        {
            if ((ID_SERVICIO == 0) && (ID_SERVICIO_POR_EMPRESA == 0))
            {
                listaDetallesServicio.RemoveAt(listaIndicesAEliminar[i]);
            }
            else
            {
                listaDetallesServicio[listaIndicesAEliminar[i]].ACCION = "DESACTIVAR";
            }
            
        }

        tablaServiciosEnGrid.Rows[indexSeleciconado].Delete();

        GridView_SERVICIOS_INCLUIDOS.DataSource = tablaServiciosEnGrid;
        GridView_SERVICIOS_INCLUIDOS.DataBind();
    }

    protected void GridView_SERVICIOS_INCLUIDOS_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable tablaServiciosEnGrid = obtenerDataTableDeGridViewServiciosActules();

        eliminar_servicio_de_grilla_y_lista_session(tablaServiciosEnGrid, GridView_SERVICIOS_INCLUIDOS.SelectedIndex);

        Panel_GRID_DETALLES_SERVICIO_SELECCIONADO.Visible = false;
    }

    protected void GridView_DETALLES_SERVICIO_SELECCIONADO_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (GridView_DETALLES_SERVICIO_SELECCIONADO.Rows.Count > 1)
        {
            DataTable tablaDetallesEnGrid = obtenerDataTableDeGridViewDetallesServicioActualSeleccionado();


            DataRow filaSeleccionada = tablaDetallesEnGrid.Rows[GridView_DETALLES_SERVICIO_SELECCIONADO.SelectedIndex];

            Decimal ID_SERVICIO = Convert.ToDecimal(filaSeleccionada["ID_SERVICIO"]);
            String NOMBRE_SERVICIO = filaSeleccionada["NOMBRE_SERVICIO"].ToString();
            Decimal ID_SERVICIO_COMPLEMENTARIO = Convert.ToDecimal(filaSeleccionada["ID_SERVICIO_COMPLEMENTARIO"]);

            List<detalleServicio> listaDetallesServicio = capturarListaDetallesServicioDesdeSession();

            detalleServicio _detalleServicio;

            int indexAEliminar = -1;
            for (int i = 0; i < listaDetallesServicio.Count; i++)
            {
                _detalleServicio = listaDetallesServicio[i];
                if ((_detalleServicio.ID_SERVICIO == ID_SERVICIO) && (_detalleServicio.NOMBRE_SERVICIO == NOMBRE_SERVICIO) && (_detalleServicio.ID_SERVICIO_COMPLEMENTARIO == ID_SERVICIO_COMPLEMENTARIO))
                {
                    indexAEliminar = i;
                    break;
                }
            }

            if (indexAEliminar != -1)
            {
                listaDetallesServicio.RemoveAt(indexAEliminar);
            }

            tablaDetallesEnGrid.Rows[GridView_DETALLES_SERVICIO_SELECCIONADO.SelectedIndex].Delete();

            GridView_DETALLES_SERVICIO_SELECCIONADO.DataSource = tablaDetallesEnGrid;
            GridView_DETALLES_SERVICIO_SELECCIONADO.DataBind();
        }
        else
        {
            configurarMensajesServiciosActuales(true, System.Drawing.Color.Red);
            Label_MENSAJES_SERVICIOS_ACTUALES.Text = "ADVERTENCIA: No puede eliminar todos los detalles de un servicio seleccionado, si desea eliminar el servicio complemente por favor utilice el link de Eliminar del grid de servicios.";
        }
    }
    protected void Button_VOLVER_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        capturarVariablesGlogales();

        QueryStringSeguro["img_area"] = "comercial";
        QueryStringSeguro["nombre_area"] = "GESTIÓN COMERCIAL";
        QueryStringSeguro["nombre_modulo"] = "CONDICIONES ECONÓMICAS";
        QueryStringSeguro["accion"] = "inicial";
        QueryStringSeguro["reg"] = ID_EMPRESA.ToString();

        Response.Redirect("~/comercial/condicionesEconomicasEficiencia.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }
    protected void Button_COPIAR_CONDICIONES_GRUPO_Click(object sender, EventArgs e)
    {
        Decimal ID_EMPRESA_SELECICONADA = Convert.ToDecimal(DropDownList_EMPRESAS_DEL_GRUPO.SelectedValue);

        condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaCondiciones = _condicionComercial.ObtenerCondicionesEconomicasPorId(ID_EMPRESA_SELECICONADA, null, 0, 0);
        DataRow infoCondiciones = tablaCondiciones.Rows[0];

        cargar_campos_condiciones_comerciales(infoCondiciones);

        DataTable tablaServiciosAnteriores;
        while(GridView_SERVICIOS_INCLUIDOS.Rows.Count > 0)
        {
            tablaServiciosAnteriores = obtenerDataTableDeGridViewServiciosActules();
            eliminar_servicio_de_grilla_y_lista_session(tablaServiciosAnteriores, GridView_SERVICIOS_INCLUIDOS.Rows.Count -1);
        }

        List<servicio> listaServicios = capturarListaServiciosDesdeSession();
        List<detalleServicio> listaDetalles = capturarListaDetallesServicioDesdeSession();

        DataTable tablaServiciosDeLaEntidad;

        tablaServiciosDeLaEntidad = _condicionComercial.ObtenerServiciosPorEmpresaPorIdEmpresa(ID_EMPRESA_SELECICONADA);

        if (tablaServiciosDeLaEntidad.Rows.Count <= 0)
        {
            habilitarSeccionServiciosParaDatosNuevos(true);

            Session.Remove("listaServicios_" + ID_EMPRESA.ToString());
            Session.Add("listaServicios_" + ID_EMPRESA.ToString(), listaServicios);
            Session.Remove("listaDetallesServicio_" + ID_EMPRESA.ToString());
            Session.Add("listaDetallesServicio_" + ID_EMPRESA.ToString(), listaDetalles);
        }
        else
        {
            cargarInformacionServiciosComplementariosDeUnaEntidad(tablaServiciosDeLaEntidad, true, true);
        }
    }
    protected void DropDownList_CONFIGURACION_SelectedIndexChanged(object sender, EventArgs e)
    {

        limpiarTextBoxServicioAdicionarSinNombreNuevoServicio();

        if (DropDownList_CONFIGURACION.SelectedIndex <= 0)
        {
            RequiredFieldValidator_TextBox_NUEVO_AIU.Enabled = true;
            ValidatorCalloutExtender_TextBox_NUEVO_AIU.Enabled = true;

            RequiredFieldValidator_TextBox_NUEVO_IVA.Enabled = true;
            ValidatorCalloutExtender_TextBox_NUEVO_IVA.Enabled = true;
            RangeValidator_TextBox_NUEVO_IVA.Enabled = true;
            ValidatorCalloutExtender_TextBox_NUEVO_IVA_1.Enabled = true;

            RequiredFieldValidator_TextBox_NUEVA_TARIFA.Enabled = true;
            ValidatorCalloutExtender_TextBox_NUEVA_TARIFA.Enabled = true;
        }
        else
        {
            if (DropDownList_CONFIGURACION.SelectedValue == "AIU")
            {
                if (String.IsNullOrEmpty(TextBox_AD_NOM.Text) == false)
                {
                    RequiredFieldValidator_TextBox_NUEVO_AIU.Enabled = true;
                    ValidatorCalloutExtender_TextBox_NUEVO_AIU.Enabled = true;

                    RequiredFieldValidator_TextBox_NUEVO_IVA.Enabled = true;
                    ValidatorCalloutExtender_TextBox_NUEVO_IVA.Enabled = true;
                    RangeValidator_TextBox_NUEVO_IVA.Enabled = false;
                    ValidatorCalloutExtender_TextBox_NUEVO_IVA_1.Enabled = false;

                    RequiredFieldValidator_TextBox_NUEVA_TARIFA.Enabled = false;
                    ValidatorCalloutExtender_TextBox_NUEVA_TARIFA.Enabled = false;

                    TextBox_NUEVO_AIU.Text = TextBox_AD_NOM.Text;

                    TextBox_NUEVO_AIU.Enabled = false;
                    TextBox_NUEVO_IVA.Enabled = true;
                    TextBox_NUEVA_TARIFA.Enabled = false;
                }
                else
                {

                    configurarMensajesNuevoServicio(true, System.Drawing.Color.Red);
                    Label_MENSAJES_NUEVO_SERVICIO.Text = "Antes de configurar Servicios complementarios para esta empresa, se debe diligenciar las condiciones económicas.";

                    DropDownList_CONFIGURACION.SelectedIndex = 0;

                    RequiredFieldValidator_TextBox_NUEVO_AIU.Enabled = true;
                    ValidatorCalloutExtender_TextBox_NUEVO_AIU.Enabled = true;

                    RequiredFieldValidator_TextBox_NUEVO_IVA.Enabled = true;
                    ValidatorCalloutExtender_TextBox_NUEVO_IVA.Enabled = true;
                    RangeValidator_TextBox_NUEVO_IVA.Enabled = true;
                    ValidatorCalloutExtender_TextBox_NUEVO_IVA_1.Enabled = true;

                    RequiredFieldValidator_TextBox_NUEVA_TARIFA.Enabled = true;
                    ValidatorCalloutExtender_TextBox_NUEVA_TARIFA.Enabled = true;

                    TextBox_NUEVO_AIU.Enabled = true;
                    TextBox_NUEVO_IVA.Enabled = true;
                    TextBox_NUEVA_TARIFA.Enabled = true;
                }
            }
            else
            {
                if (DropDownList_CONFIGURACION.SelectedValue == "AIU_PREFERENCIAL")
                {
                    RequiredFieldValidator_TextBox_NUEVO_AIU.Enabled = true;
                    ValidatorCalloutExtender_TextBox_NUEVO_AIU.Enabled = true;

                    RequiredFieldValidator_TextBox_NUEVO_IVA.Enabled = true;
                    ValidatorCalloutExtender_TextBox_NUEVO_IVA.Enabled = true;
                    RangeValidator_TextBox_NUEVO_IVA.Enabled = false;
                    ValidatorCalloutExtender_TextBox_NUEVO_IVA_1.Enabled = false;

                    RequiredFieldValidator_TextBox_NUEVA_TARIFA.Enabled = false;
                    ValidatorCalloutExtender_TextBox_NUEVA_TARIFA.Enabled = false;

                    TextBox_NUEVO_AIU.Enabled = true;
                    TextBox_NUEVO_IVA.Enabled = true;
                    TextBox_NUEVA_TARIFA.Enabled = false;

                    TextBox_NUEVO_AIU.Text = "";
                }
                else
                {
                    if (DropDownList_CONFIGURACION.SelectedValue == "TARIFA")
                    {
                        RequiredFieldValidator_TextBox_NUEVO_AIU.Enabled = false;
                        ValidatorCalloutExtender_TextBox_NUEVO_AIU.Enabled = false;

                        RequiredFieldValidator_TextBox_NUEVO_IVA.Enabled = true;
                        ValidatorCalloutExtender_TextBox_NUEVO_IVA.Enabled = true;
                        RangeValidator_TextBox_NUEVO_IVA.Enabled = false;
                        ValidatorCalloutExtender_TextBox_NUEVO_IVA_1.Enabled = false;

                        RequiredFieldValidator_TextBox_NUEVA_TARIFA.Enabled = true;
                        ValidatorCalloutExtender_TextBox_NUEVA_TARIFA.Enabled = true;

                        TextBox_NUEVO_AIU.Enabled = false;
                        TextBox_NUEVO_IVA.Enabled = true;
                        TextBox_NUEVA_TARIFA.Enabled = true;

                        TextBox_NUEVA_TARIFA.Text = "";
                    }
                    else
                    {
                        if (DropDownList_CONFIGURACION.SelectedValue == "IVA")
                        {
                            RequiredFieldValidator_TextBox_NUEVO_AIU.Enabled = false;
                            ValidatorCalloutExtender_TextBox_NUEVO_AIU.Enabled = false;

                            RequiredFieldValidator_TextBox_NUEVO_IVA.Enabled = true;
                            ValidatorCalloutExtender_TextBox_NUEVO_IVA.Enabled = true;
                            RangeValidator_TextBox_NUEVO_IVA.Enabled = true;
                            ValidatorCalloutExtender_TextBox_NUEVO_IVA_1.Enabled = true;

                            RequiredFieldValidator_TextBox_NUEVA_TARIFA.Enabled = false;
                            ValidatorCalloutExtender_TextBox_NUEVA_TARIFA.Enabled = false;

                            TextBox_NUEVO_AIU.Enabled = false;
                            TextBox_NUEVO_IVA.Enabled = true;
                            TextBox_NUEVA_TARIFA.Enabled = false;

                            TextBox_NUEVO_IVA.Text = "";
                        }
                        else
                        {
                            if (DropDownList_CONFIGURACION.SelectedValue == "REEMBOLSO_DE_GASTOS")
                            {
                                RequiredFieldValidator_TextBox_NUEVO_AIU.Enabled = false;
                                ValidatorCalloutExtender_TextBox_NUEVO_AIU.Enabled = false;

                                RequiredFieldValidator_TextBox_NUEVO_IVA.Enabled = false;
                                ValidatorCalloutExtender_TextBox_NUEVO_IVA.Enabled = false;
                                RangeValidator_TextBox_NUEVO_IVA.Enabled = false;
                                ValidatorCalloutExtender_TextBox_NUEVO_IVA_1.Enabled = false;

                                RequiredFieldValidator_TextBox_NUEVA_TARIFA.Enabled = false;
                                ValidatorCalloutExtender_TextBox_NUEVA_TARIFA.Enabled = false;

                                TextBox_NUEVO_AIU.Enabled = false;
                                TextBox_NUEVO_IVA.Enabled = false;
                                TextBox_NUEVA_TARIFA.Enabled = false;
                            }
                            else
                            {
                                RequiredFieldValidator_TextBox_NUEVO_AIU.Enabled = false;
                                ValidatorCalloutExtender_TextBox_NUEVO_AIU.Enabled = false;

                                RequiredFieldValidator_TextBox_NUEVO_IVA.Enabled = false;
                                ValidatorCalloutExtender_TextBox_NUEVO_IVA.Enabled = false;
                                RangeValidator_TextBox_NUEVO_IVA.Enabled = false;
                                ValidatorCalloutExtender_TextBox_NUEVO_IVA_1.Enabled = false;

                                RequiredFieldValidator_TextBox_NUEVA_TARIFA.Enabled = false;
                                ValidatorCalloutExtender_TextBox_NUEVA_TARIFA.Enabled = false;

                                TextBox_NUEVO_AIU.Enabled = false;
                                TextBox_NUEVO_IVA.Enabled = false;
                                TextBox_NUEVA_TARIFA.Enabled = false;
                            }
                        }
                    }
                }
            }
        }
    }
    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {
        configurarMensajes(false, System.Drawing.Color.Green);
    }
    protected void Button_CERRAR_MENSAJES_SERVICIOS_ACTUALES_Click(object sender, EventArgs e)
    {
        configurarMensajesServiciosActuales(false, System.Drawing.Color.Green);
    }
    protected void Button_MENSAJES_NUEVO_SERVICIO_Click(object sender, EventArgs e)
    {
        configurarMensajesNuevoServicio(false, System.Drawing.Color.Green);
    }
}