using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

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
    private void cargar_DropDownList_SERVICIOS_COMPLEMENTARIOS()
    {
        DropDownList_SERVICIOS_COMPLEMENTARIOS.Items.Clear();

        condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaServiciosComplementarios = _condicionComercial.ObtenerServiciosComplementariosPorTipo(tabla.SERVICIO_EMPRESA_TEMPORAL);

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_SERVICIOS_COMPLEMENTARIOS.Items.Add(item);

        foreach (DataRow fila in tablaServiciosComplementarios.Rows)
        {
            item = new ListItem(fila["NOMBRE_SERVICIO_COMPLEMENTARIO"].ToString(), fila["ID_SERVICIO_COMPLEMENTARIO"].ToString());
            DropDownList_SERVICIOS_COMPLEMENTARIOS.Items.Add(item);
        }
        DropDownList_SERVICIOS_COMPLEMENTARIOS.DataBind();
    }
    #endregion

    #region cargar datos en controles
    private void cargarInfoCondiciones(Boolean bModificar)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
        String ID_CIUDAD = null;
        Decimal ID_CENTRO_C = 0;
        Decimal ID_SUB_C = 0;

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

        if (ID_SUB_C != 0)
        {
            subCentroCosto _subCentroCostoMODULO = new subCentroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaInfoSubCentro = _subCentroCostoMODULO.ObtenerSubCentrosDeCostoPorIdSubCosto(ID_SUB_C);
            DataRow filaTablaInfoSubCentro = tablaInfoSubCentro.Rows[0];

            configurarInfoAdicionalModulo(true, filaTablaInfoSubCentro["NOM_SUB_C"].ToString());
        }
        else
        {
            if (ID_CENTRO_C != 0)
            {
                centroCosto _centroCostoMODULO = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                DataTable tablaInfoCC = _centroCostoMODULO.ObtenerCentrosDeCostoPorIdCentroCosto(ID_CENTRO_C);
                DataRow filaTablaInfoCC = tablaInfoCC.Rows[0];

                configurarInfoAdicionalModulo(true, filaTablaInfoCC["NOM_CC"].ToString());
            }
            else
            {
                if ((String.IsNullOrEmpty(ID_CIUDAD) == false) && (ID_EMPRESA != 0))
                {
                    cobertura _coberturaMODULO = new cobertura(Session["idEmpresa"].ToString());
                    DataTable tablaInfoCiudad = _coberturaMODULO.obtenerNombreCiudadPorIdCiudad(ID_CIUDAD);
                    DataRow filaTablaInfoCiudad = tablaInfoCiudad.Rows[0];

                    configurarInfoAdicionalModulo(true, filaTablaInfoCiudad["NOMBRE"].ToString());
                }
                else
                {
                    cliente _clienteMODULO = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                    DataTable tablaInfoCliente = _clienteMODULO.ObtenerEmpresaConIdEmpresa(ID_EMPRESA);
                    DataRow filaTablaInfoCliente = tablaInfoCliente.Rows[0];

                    configurarInfoAdicionalModulo(true, filaTablaInfoCliente["RAZ_SOCIAL"].ToString());
                }
            }
        }

        condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCondiciones = _condicionComercial.ObtenerCondicionesEconomicasPorId(ID_EMPRESA, ID_CIUDAD, ID_CENTRO_C, ID_SUB_C);
        if (tablaCondiciones.Rows.Count <= 0)
        {
            if (_condicionComercial.MensajeError != null)
            {
                configurarBotonesDeAccion(false, false, false, true);

                Panel_FORMULARIO.Visible = false;

                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = _condicionComercial.MensajeError;
            }
            else
            {
                configurarBotonesDeAccion(false, true, true, true);

                Panel_FORMULARIO.Visible = true;
                
                Panel_SERVICIOS_COMPLEMENTARIOS.Visible = false;

                controlesParaNuevaCondicionEconomica();
            }
        }
        else
        {
            if (bModificar == true)
            {
                configurarBotonesDeAccion(false, true, true, true);
            }
            else 
            {
                configurarBotonesDeAccion(true, false, false, true);
            }

            Panel_FORMULARIO.Visible = true;
            if (bModificar == true)
            {
                Panel_FORMULARIO.Enabled = true;
            }
            else
            {
                Panel_FORMULARIO.Enabled = false;
            }

            configurarMensajes(false, System.Drawing.Color.Green);

            DataRow infoCondicionesComerciales = tablaCondiciones.Rows[0];

            if (bModificar == false)
            {
                Panel_CONTROL_REGISTRO.Visible = true;
                Panel_CONTROL_REGISTRO.Enabled = false;
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
            }
            else
            {
                Panel_CONTROL_REGISTRO.Visible = false;
            }

            if (bModificar == false)
            {
                Panel_COD_CONDICIONES.Visible = true;
                Panel_COD_CONDICIONES.Enabled = false;
                TextBox_COD_CONDICION.Text = infoCondicionesComerciales["REGISTRO"].ToString().Trim();
            }
            else
            {
                Panel_COD_CONDICIONES.Visible = false;
            }

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

            TextBox_AD_NOM.Text = String.Format("{0:N2}",Convert.ToDecimal(infoCondicionesComerciales["AD_NOM"]));

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

            TextBox_AD_PENSION.Text = String.Format("{0:N2}",Convert.ToDecimal(infoCondicionesComerciales["AD_PENSION"]));
            TextBox_AD_SALUD.Text = String.Format("{0:N2}",Convert.ToDecimal(infoCondicionesComerciales["AD_SALUD"]));
            TextBox_AD_RIESGOS.Text = String.Format("{0:N2}",Convert.ToDecimal(infoCondicionesComerciales["AD_RIESGOS"]));
            TextBox_AD_APO_SENA.Text = String.Format("{0:N2}",Convert.ToDecimal(infoCondicionesComerciales["AD_APO_SENA"]));
            TextBox_AD_APO_ICBF.Text = String.Format("{0:N2}",Convert.ToDecimal(infoCondicionesComerciales["AD_APO_ICBF"]));
            TextBox_AD_APO_CAJA.Text = String.Format("{0:N2}",Convert.ToDecimal(infoCondicionesComerciales["AD_APO_CAJA"]));
            TextBox_AD_VACACIONES.Text = String.Format("{0:N2}",Convert.ToDecimal(infoCondicionesComerciales["AD_VACACIONES"]));
            TextBox_AD_CESANTIA.Text = String.Format("{0:N2}",Convert.ToDecimal(infoCondicionesComerciales["AD_CESANTIA"]));
            TextBox_AD_INT_CES.Text = String.Format("{0:N2}",Convert.ToDecimal(infoCondicionesComerciales["AD_INT_CES"]));
            TextBox_AD_PRIMA.Text = String.Format("{0:N2}",Convert.ToDecimal(infoCondicionesComerciales["AD_PRIMA"]));
            TextBox_AD_SEG_VID.Text = String.Format("{0:N2}",Convert.ToDecimal(infoCondicionesComerciales["AD_SEG_VID"]));

            cargarCheckBox(CheckBox_SUB_PENSION,infoCondicionesComerciales["SUB_PENSION"].ToString().Trim());
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

            DataTable tablaServiciosEspecificos;

            if (ID_SUB_C != 0)
            {
                tablaServiciosEspecificos = _condicionComercial.ObtenerServiciosPorEmpresaPorIdSubC(ID_SUB_C);
            }
            else
            {
                if (ID_CENTRO_C != 0)
                {
                    tablaServiciosEspecificos = _condicionComercial.ObtenerServiciosPorEmpresaPorIdCentroC(ID_CENTRO_C);
                }
                else
                {
                    if ((ID_EMPRESA != 0) && (String.IsNullOrEmpty(ID_CIUDAD) == false))
                    {
                        tablaServiciosEspecificos = _condicionComercial.ObtenerServiciosPorEmpresaPorIdCiudad(ID_CIUDAD, ID_EMPRESA);
                    }
                    else
                    {
                        tablaServiciosEspecificos = _condicionComercial.ObtenerServiciosPorEmpresaPorIdEmpresa(ID_EMPRESA);
                    }
                }
            }

            if (tablaServiciosEspecificos.Rows.Count <= 0)
            {
                CheckBox_ACTIVAR_SERVICIO_COMPLEMENTARIO.Checked = false;
                cargarTextCheckActivarServicios(ID_EMPRESA, ID_CIUDAD, ID_CENTRO_C, ID_SUB_C, "sin");
                Panel_SERVICIOS_COMPLEMENTARIOS.Visible = false;

                List<servicio> listaServicios = new List<servicio>();
                Session.Remove("listaServicios_" + ID_EMPRESA.ToString());
                Session.Add("listaServicios_" + ID_EMPRESA.ToString(), listaServicios);
                List<detalleServicio> listaDetallesServicio = new List<detalleServicio>();
                Session.Remove("listaDetallesServicio_" + ID_EMPRESA.ToString());
                Session.Add("listaDetallesServicio_" + ID_EMPRESA.ToString(), listaDetallesServicio);
            }
            else
            { 
                CheckBox_ACTIVAR_SERVICIO_COMPLEMENTARIO.Checked = true;
                cargarTextCheckActivarServicios(ID_EMPRESA, ID_CIUDAD, ID_CENTRO_C, ID_SUB_C, "con");
                Panel_SERVICIOS_COMPLEMENTARIOS.Visible = true;
                configurarMensajesServicioComplementario(false, System.Drawing.Color.Green);
                cargarInformacionServiciosComplementariosEspecificos(tablaServiciosEspecificos, false);
                
                if (bModificar == true)
                {
                    Panel_SERVICIOS_COMPLEMENTARIOS_ADICIONAR.Visible = true;
                }
                else
                {
                    Panel_SERVICIOS_COMPLEMENTARIOS_ADICIONAR.Visible = false;
                }
            }
        }
    }

    private void cargarInformacionServiciosComplementariosEspecificos(DataTable tablaServicios, Boolean datosHeredados)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);


        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
        String ID_CIUDAD = null;
        Decimal ID_CENTRO_C = 0;
        Decimal ID_SUB_C = 0;

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

        cargar_DropDownList_SERVICIOS_COMPLEMENTARIOS();
        cargar_DropDownList_CONFIGURACION();
        limpiarTextBoxServicioAdicionar();

        List<servicio> listaServicios = new List<servicio>();
        List<detalleServicio> listaDetallesServicio = new List<detalleServicio>();
        servicio _servicioParaLista;
        detalleServicio _detalleServicioParaLista;

        Decimal ID_SERVICIO = 0;
        Decimal ID_SERVICIO_POR_EMPRESA = 0;

        String NOMBRE_SERVICIO;

        if (ID_SUB_C != 0)
        {
            NOMBRE_SERVICIO = "EMPRESA_" + ID_EMPRESA.ToString() + "_SUBCC_" + ID_SUB_C.ToString();
        }
        else
        {
            if (ID_CENTRO_C != 0)
            {
                NOMBRE_SERVICIO = "EMPRESA_" + ID_EMPRESA.ToString() + "_CC_" + ID_CENTRO_C.ToString();
            }
            else
            {
                if ((ID_EMPRESA != 0) && (String.IsNullOrEmpty(ID_CIUDAD) == false))
                {
                    NOMBRE_SERVICIO = "EMPRESA_" + ID_EMPRESA.ToString() + "_CIUDAD_" + ID_CIUDAD;
                }
                else
                {
                    NOMBRE_SERVICIO = "EMPRESA_" + ID_EMPRESA.ToString();
                }
            }
        }

        if (tablaServicios.Rows.Count > 0)
        {
            DataRow servicioDefault = tablaServicios.Rows[0];

            _servicioParaLista = new servicio(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            if (datosHeredados == true)
            {
                _servicioParaLista.ACCION = "INSERTAR";
                _servicioParaLista.AIU = 0;
                _servicioParaLista.DESCRIPCION = "Ninguno";
                _servicioParaLista.ID_SERVICIO = 0;
                _servicioParaLista.ID_SERVICIO_POR_EMPRESA = 0;
                _servicioParaLista.IVA = 0;
                _servicioParaLista.NOMBRE_SERVICIO = NOMBRE_SERVICIO;
                _servicioParaLista.VALOR = 0;

                ID_SERVICIO = 0;
                ID_SERVICIO_POR_EMPRESA = 0;
            }
            else
            {
                _servicioParaLista.ACCION = "NINGUNA";
                _servicioParaLista.AIU = Convert.ToDecimal(servicioDefault["AIU"]);
                _servicioParaLista.DESCRIPCION = servicioDefault["observaciones"].ToString().Trim();
                _servicioParaLista.ID_SERVICIO = Convert.ToDecimal(servicioDefault["ID_SERVICIO"]);
                _servicioParaLista.ID_SERVICIO_POR_EMPRESA = Convert.ToDecimal(servicioDefault["ID_SERVICIO_POR_EMPRESA"]);
                _servicioParaLista.IVA = Convert.ToDecimal(servicioDefault["IVA"]);
                _servicioParaLista.NOMBRE_SERVICIO = servicioDefault["NOMBRE_SERVICIO"].ToString().Trim();
                _servicioParaLista.VALOR = Convert.ToDecimal(servicioDefault["VALOR"]);

                ID_SERVICIO = Convert.ToDecimal(servicioDefault["ID_SERVICIO"]);
                ID_SERVICIO_POR_EMPRESA = Convert.ToDecimal(servicioDefault["ID_SERVICIO_POR_EMPRESA"]);
                NOMBRE_SERVICIO = servicioDefault["NOMBRE_SERVICIO"].ToString().Trim();
            }

            listaServicios.Add(_servicioParaLista);

            detalleServicio _detalleServicio = new detalleServicio(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaDetallesServicioOriginal = _detalleServicio.ObtenerDetalleServicioPorIdServicioActivos(Convert.ToDecimal(servicioDefault["ID_SERVICIO"]));

            DataTable tablaDetallesServicioCreada = new DataTable();
            tablaDetallesServicioCreada.Columns.Add("ID_DETALLE_SERVICIO");
            tablaDetallesServicioCreada.Columns.Add("ID_SERVICIO");
            tablaDetallesServicioCreada.Columns.Add("NOMBRE_SERVICIO");
            tablaDetallesServicioCreada.Columns.Add("ID_SERVICIO_COMPLEMENTARIO");
            tablaDetallesServicioCreada.Columns.Add("NOMBRE_SERVICIO_COMPLEMENTARIO");
            tablaDetallesServicioCreada.Columns.Add("AIU");
            tablaDetallesServicioCreada.Columns.Add("IVA");
            tablaDetallesServicioCreada.Columns.Add("VALOR");

            DataRow filaDetalleServicioCreado;
            foreach (DataRow filaDetalleServicioOriginal in tablaDetallesServicioOriginal.Rows)
            {
                filaDetalleServicioCreado = tablaDetallesServicioCreada.NewRow();
                _detalleServicioParaLista = new detalleServicio(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

                if (datosHeredados == true)
                {
                    _detalleServicioParaLista.ACCION = "INSERTAR";
                    _detalleServicioParaLista.ID_DETALLE_SERVICIO = 0;
                    filaDetalleServicioCreado["ID_DETALLE_SERVICIO"] = 0;
                }
                else
                {
                    _detalleServicioParaLista.ACCION = "NINGUNA";
                    _detalleServicioParaLista.ID_DETALLE_SERVICIO = Convert.ToDecimal(filaDetalleServicioOriginal["ID_DETALLE_SERVICIO"]);
                    filaDetalleServicioCreado["ID_DETALLE_SERVICIO"] = filaDetalleServicioOriginal["ID_DETALLE_SERVICIO"];
                }
                _detalleServicioParaLista.AIU = Convert.ToDecimal(filaDetalleServicioOriginal["AIU"]);
                _detalleServicioParaLista.ID_SERVICIO = ID_SERVICIO;
                _detalleServicioParaLista.ID_SERVICIO_COMPLEMENTARIO = Convert.ToDecimal(filaDetalleServicioOriginal["ID_SERVICIO_COMPLEMENTARIO"]);
                _detalleServicioParaLista.ID_SERVICIO_POR_EMPRESA = ID_SERVICIO_POR_EMPRESA;
                _detalleServicioParaLista.IVA = Convert.ToDecimal(filaDetalleServicioOriginal["IVA"]);
                _detalleServicioParaLista.NOMBRE_SERVICIO = NOMBRE_SERVICIO;
                _detalleServicioParaLista.VALOR = Convert.ToDecimal(filaDetalleServicioOriginal["VALOR"]);

                filaDetalleServicioCreado["ID_SERVICIO"] = ID_SERVICIO;
                filaDetalleServicioCreado["NOMBRE_SERVICIO"] = NOMBRE_SERVICIO;
                filaDetalleServicioCreado["ID_SERVICIO_COMPLEMENTARIO"] = filaDetalleServicioOriginal["ID_SERVICIO_COMPLEMENTARIO"];
                filaDetalleServicioCreado["NOMBRE_SERVICIO_COMPLEMENTARIO"] = filaDetalleServicioOriginal["NOMBRE_SERVICIO_COMPLEMENTARIO"];
                filaDetalleServicioCreado["AIU"] = filaDetalleServicioOriginal["AIU"];
                filaDetalleServicioCreado["IVA"] = filaDetalleServicioOriginal["IVA"];
                filaDetalleServicioCreado["VALOR"] = filaDetalleServicioOriginal["VALOR"];

                tablaDetallesServicioCreada.Rows.Add(filaDetalleServicioCreado);
                listaDetallesServicio.Add(_detalleServicioParaLista);
            }

            GridView_SERVICIOS_INCLUIDOS.DataSource = tablaDetallesServicioCreada;
            GridView_SERVICIOS_INCLUIDOS.DataBind();

            Session.Remove("listaServicios_" + ID_EMPRESA.ToString());
            Session.Add("listaServicios_" + ID_EMPRESA.ToString(), listaServicios);
            Session.Remove("listaDetallesServicio_" + ID_EMPRESA.ToString());
            Session.Add("listaDetallesServicio_" + ID_EMPRESA.ToString(), listaDetallesServicio);
        }
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

        Button_VOLVER_MENU_EMPRESA.Visible = bVolver;
        Button_VOLVER_MENU_EMPRESA.Enabled = bVolver;
        Button_VOLVER_MENU_EMPRESA_1.Visible = bVolver;
        Button_VOLVER_MENU_EMPRESA_1.Enabled = bVolver;
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
    private void configurarMensajesServicioComplementario(Boolean mostrarMensaje, System.Drawing.Color color)
    {
        if (mostrarMensaje == false)
        {
            Panel_FONDO_MENSAJE_SERVICIO_COMPLEMENTARIO.Style.Add("display", "none");
            Panel_MENSAJE_SERVICIO_COMPLEMENTARIO.Style.Add("display", "none");
        }
        else
        {
            Panel_FONDO_MENSAJE_SERVICIO_COMPLEMENTARIO.Style.Add("display", "block");
            Panel_MENSAJE_SERVICIO_COMPLEMENTARIO.Style.Add("display", "block");
            Label_MENSAJE_SERVICIO_COMPLEMENTARIO.ForeColor = color;

            if (color == System.Drawing.Color.Green)
            {
                Image_MENSAJE_SERVICIO_COMPLEMENTARIO_POPUP.ImageUrl = "~/imagenes/plantilla/ok_popup.png";
            }
            else
            {
                Image_MENSAJE_SERVICIO_COMPLEMENTARIO_POPUP.ImageUrl = "~/imagenes/plantilla/advertencia_popup.png";
            }
        }
        Panel_FONDO_MENSAJE_SERVICIO_COMPLEMENTARIO.Visible = mostrarMensaje;
        Panel_MENSAJE_SERVICIO_COMPLEMENTARIO.Visible = mostrarMensaje;
    }
    private void controlesParaNuevaCondicionEconomica()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
        String ID_CIUDAD = null;
        Decimal ID_CENTRO_C = 0;
        Decimal ID_SUB_C = 0;

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

        Decimal ID_EMPRESA_1 = ID_EMPRESA;
        String ID_CIUDAD_1 = null;
        Decimal ID_CENTRO_C_1 = 0;
        Decimal ID_SUB_C_1 = 0;

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

        condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCondicionesHeredadas = _condicionComercial.ObtenerCondicionesEconomicasPorId(ID_EMPRESA_1, ID_CIUDAD_1, ID_CENTRO_C_1, ID_SUB_C_1);

        if (tablaCondicionesHeredadas.Rows.Count <= 0)
        {
            if (_condicionComercial.MensajeError != null)
            {
                configurarBotonesDeAccion(false, false, false, true);

                Panel_FORMULARIO.Visible = false;

                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = _condicionComercial.MensajeError;
            }
            else
            {
                configurarBotonesDeAccion(false, true, true, true);

                Panel_FORMULARIO.Visible = true;

                Panel_SERVICIOS_COMPLEMENTARIOS.Visible = false;

                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = "ADVERTENCIA: No se encontraron registros de condiciones económicas para mostrar y la entidad anterior no tiene condiciones económicas para heredar, por favor digitelas y guarde.";

                Panel_CONTROL_REGISTRO.Visible = false;

                Panel_COD_CONDICIONES.Visible = false;

                CheckBox_FACTURA_NOMINA.Checked = false;

                cargar_DropDownList_MOD_SOPORTE();
                cargar_DropDownList_MOD_FACTURA();

                TextBox_AD_NOM.Text = "0";
                CheckBox_SOLO_DEV.Checked = false;

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

                List<servicio> listaServicios = new List<servicio>();
                Session.Remove("listaServicios_" + ID_EMPRESA.ToString());
                Session.Add("listaServicios_" + ID_EMPRESA.ToString(), listaServicios);
                List<detalleServicio> listaDetallesServicio = new List<detalleServicio>();
                Session.Remove("listaDetallesServicio_" + ID_EMPRESA.ToString());
                Session.Add("listaDetallesServicio_" + ID_EMPRESA.ToString(), listaDetallesServicio);

                CheckBox_ACTIVAR_SERVICIO_COMPLEMENTARIO.Checked = false;
                cargarTextCheckActivarServicios(ID_EMPRESA, ID_CIUDAD, ID_CENTRO_C, ID_SUB_C, "sin");
                Panel_SERVICIOS_COMPLEMENTARIOS.Visible = false;
            }
        }
        else
        {
            configurarBotonesDeAccion(false, true, true, true);

            Panel_FORMULARIO.Visible = true;
            Panel_FORMULARIO.Enabled = true;

            configurarMensajes(true, System.Drawing.Color.Red);
            Label_MENSAJE.Text = "ADVERTENCIA: No se encontraron registros de condiciones económicas para mostrar, pero se heredaron las condiciones económicas desde la entidad anterior, REALICE LOS CAMBIOS NECESARIOS Y GUARDE LA INFORMACIÓN.";

            DataRow infoCondicionesComerciales = tablaCondicionesHeredadas.Rows[0];

            Panel_CONTROL_REGISTRO.Visible = false;

            Panel_COD_CONDICIONES.Visible = false;

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

            DataTable tablaServiciosEspecificos;


            if (ID_SUB_C != 0)
            {
                tablaServiciosEspecificos = _condicionComercial.ObtenerServiciosPorEmpresaPorIdCentroC(ID_CENTRO_C_1);
            }
            else
            {
                if (ID_CENTRO_C != 0)
                {
                    tablaServiciosEspecificos = _condicionComercial.ObtenerServiciosPorEmpresaPorIdCiudad(ID_CIUDAD_1, ID_EMPRESA_1);
                }
                else
                {
                    tablaServiciosEspecificos = _condicionComercial.ObtenerServiciosPorEmpresaPorIdEmpresa(ID_EMPRESA_1);
                }
            }


            if (tablaServiciosEspecificos.Rows.Count <= 0)
            {
                CheckBox_ACTIVAR_SERVICIO_COMPLEMENTARIO.Checked = false;
                cargarTextCheckActivarServicios(ID_EMPRESA, ID_CIUDAD, ID_CENTRO_C, ID_SUB_C, "sin");
                Panel_SERVICIOS_COMPLEMENTARIOS.Visible = false;

                List<servicio> listaServicios = new List<servicio>();
                Session.Remove("listaServicios_" + ID_EMPRESA.ToString());
                Session.Add("listaServicios_" + ID_EMPRESA.ToString(), listaServicios);
                List<detalleServicio> listaDetallesServicio = new List<detalleServicio>();
                Session.Remove("listaDetallesServicio_" + ID_EMPRESA.ToString());
                Session.Add("listaDetallesServicio_" + ID_EMPRESA.ToString(), listaDetallesServicio);
            }
            else
            {
                CheckBox_ACTIVAR_SERVICIO_COMPLEMENTARIO.Checked = true;
                cargarTextCheckActivarServicios(ID_EMPRESA, ID_CIUDAD, ID_CENTRO_C, ID_SUB_C, "con");
                Panel_SERVICIOS_COMPLEMENTARIOS.Visible = true;
                configurarMensajesServicioComplementario(false, System.Drawing.Color.Green);
                cargarInformacionServiciosComplementariosEspecificos(tablaServiciosEspecificos, true);

                Panel_SERVICIOS_COMPLEMENTARIOS_ADICIONAR.Visible = true;
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
        TextBox_SER_ADMON.Text = "0";
        TextBox_SER_VALOR.Text = "0";
        TextBox_SER_IVA.Text = "0,00";
    }
    private void cargarTextCheckActivarServicios(Decimal ID_EMPRESA, String ID_CIUDAD, Decimal ID_CENTRO_C, Decimal ID_SUB_C, String texto)
    {
        if (texto == "sin")
        {
            if (ID_SUB_C != 0)
            {
                CheckBox_ACTIVAR_SERVICIO_COMPLEMENTARIO.Text = "Servicios complementarios para sub centro de costo.";
            }
            else
            {
                if (ID_CENTRO_C != 0)
                {
                    CheckBox_ACTIVAR_SERVICIO_COMPLEMENTARIO.Text = "Servicios complementarios para centro de costo.";
                }
                else
                {
                    if ((ID_EMPRESA != 0) && (String.IsNullOrEmpty(ID_CIUDAD) == false))
                    {
                        CheckBox_ACTIVAR_SERVICIO_COMPLEMENTARIO.Text = "Servicios complementarios para cobertura.";
                    }
                    else
                    {
                        CheckBox_ACTIVAR_SERVICIO_COMPLEMENTARIO.Text = "Servicios complementarios para empresa.";
                    }
                }
            }
        }
        else
        {
            if (ID_SUB_C != 0)
            {
                CheckBox_ACTIVAR_SERVICIO_COMPLEMENTARIO.Text = "Sub centro de costo con servicios complementarios.";
            }
            else
            {
                if (ID_CENTRO_C != 0)
                {
                    CheckBox_ACTIVAR_SERVICIO_COMPLEMENTARIO.Text = "Centro de costo con servicios complementarios.";
                }
                else
                {
                    if ((ID_EMPRESA != 0) && (String.IsNullOrEmpty(ID_CIUDAD) == false))
                    {
                        CheckBox_ACTIVAR_SERVICIO_COMPLEMENTARIO.Text = "Cobertura con servicios complementarios.";
                    }
                    else
                    {
                        CheckBox_ACTIVAR_SERVICIO_COMPLEMENTARIO.Text = "Empresa con servicios complementarios.";
                    }
                }
            }
        }
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
    private DataTable obtenerDataTableDeGridViewServiciosComplementarios()
    {
        DataTable tablaResultado = new DataTable();

        tablaResultado.Columns.Add("ID_SERVICIO");
        tablaResultado.Columns.Add("ID_SERVICIO_COMPLEMENTARIO");
        tablaResultado.Columns.Add("NOMBRE_SERVICIO_COMPLEMENTARIO");
        tablaResultado.Columns.Add("AIU");
        tablaResultado.Columns.Add("IVA");
        tablaResultado.Columns.Add("VALOR");

        DataRow filaTablaResultado;

        String ID_SERVICIO;
        String ID_SERVICIO_COMPLEMENTARIO;
        String NOMBRE_SERVICIO_COMPLEMENTARIO;
        String AIU;
        String IVA;
        String VALOR;

        for (int i = 0; i < GridView_SERVICIOS_INCLUIDOS.Rows.Count; i++)
        {
            ID_SERVICIO = GridView_SERVICIOS_INCLUIDOS.DataKeys[i].Values["ID_SERVICIO"].ToString();
            ID_SERVICIO_COMPLEMENTARIO = GridView_SERVICIOS_INCLUIDOS.DataKeys[i].Values["ID_SERVICIO_COMPLEMENTARIO"].ToString();
            NOMBRE_SERVICIO_COMPLEMENTARIO = GridView_SERVICIOS_INCLUIDOS.Rows[i].Cells[3].Text;

            if (GridView_SERVICIOS_INCLUIDOS.Rows[i].Cells[4].Text == "&nbsp;")
            {
                AIU = "";
            }
            else
            {
                AIU = GridView_SERVICIOS_INCLUIDOS.Rows[i].Cells[4].Text;
            }

            IVA = GridView_SERVICIOS_INCLUIDOS.Rows[i].Cells[5].Text;

            if (GridView_SERVICIOS_INCLUIDOS.Rows[i].Cells[6].Text == "&nbsp;")
            {
                VALOR = "";
            }
            else
            {
                VALOR = GridView_SERVICIOS_INCLUIDOS.Rows[i].Cells[6].Text;
            }
            

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_SERVICIO"] = ID_SERVICIO;
            filaTablaResultado["ID_SERVICIO_COMPLEMENTARIO"] = ID_SERVICIO_COMPLEMENTARIO;
            filaTablaResultado["NOMBRE_SERVICIO_COMPLEMENTARIO"] = NOMBRE_SERVICIO_COMPLEMENTARIO;
            filaTablaResultado["AIU"] = AIU;
            filaTablaResultado["IVA"] = IVA;
            filaTablaResultado["VALOR"] = VALOR;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }
    #endregion

    #region metodos que se ejecutan al cargar la pagina
    private void iniciar_interfaz_inicial()
    {
        configurarBotonesDeAccion(false, false, false, true);

        configurarMensajes(false, System.Drawing.Color.Green);

        Panel_FORMULARIO.Visible = true;

        Panel_SERVICIOS_COMPLEMENTARIOS.Visible = false;

        cargarInfoCondiciones(false);
    }
    private void iniciar_interfaz_para_modificar_cliente()
    {
        configurarBotonesDeAccion(false, true, true, true);

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
    #endregion

    private void configurar_paneles_popup(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_mensaje.CssClass = "panel_popup";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = "CONDICIONES ECONÓMICAS";

        if (IsPostBack == false)
        {
            configurar_paneles_popup(Panel_FONDO_MENSAJE, Panel_MENSAJES);
            configurar_paneles_popup(Panel_FONDO_MENSAJE_SERVICIO_COMPLEMENTARIO, Panel_MENSAJE_SERVICIO_COMPLEMENTARIO);

            tools _tools = new tools();
            SecureQueryString QueryStringSeguro;
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

            String accion = QueryStringSeguro["accion"].ToString();

            Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
            String ID_CIUDAD = null;
            Decimal ID_CENTRO_C = 0;
            Decimal ID_SUB_C = 0;

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
                        configurarBotonesDeAccion(false, false, false, true);

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
                            configurarBotonesDeAccion(false, false, false, true);

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
    }

    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String REGISTRO = TextBox_COD_CONDICION.Text.Trim();
        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
        String ID_CIUDAD = null;
        Decimal ID_CENTRO_C = 0;
        Decimal ID_SUB_C = 0;

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

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
        QueryStringSeguro["img_area"] = "comercial";
        QueryStringSeguro["nombre_area"] = "GESTIÓN COMERCIAL";
        QueryStringSeguro["nombre_modulo"] = "CONDICIONES ECONÓMICAS";
        QueryStringSeguro["accion"] = "modificar";
        QueryStringSeguro["reg"] = ID_EMPRESA.ToString();
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
                    QueryStringSeguro["codCiudad"] = ID_CIUDAD;
                }
            }
        }

        Response.Redirect("~/comercial/condicionesEconomicasSertempoPorIdEspecifico.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }

    private void seEjecutaAlPresionarBotonGuardar()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String accion = QueryStringSeguro["accion"].ToString();

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
        String ID_CIUDAD = null;
        Decimal ID_CENTRO_C = 0;
        Decimal ID_SUB_C = 0;

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

        condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        String FACTURA = obtenerDeUnCheckBox(CheckBox_FACTURA_NOMINA);

        String MOD_SOPORTE = DropDownList_MOD_SOPORTE.SelectedValue;
        String MOD_FACTURA = DropDownList_MOD_FACTURA.SelectedValue;

        Decimal AD_NOM = Convert.ToDecimal(TextBox_AD_NOM.Text);

        String SOLO_DEV = obtenerDeUnCheckBox(CheckBox_SOLO_DEV);
        String APL_MTZ = "N";

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
            Decimal AD_SEG_VID = Convert.ToDecimal(TextBox_AD_SEG_VID.Text);

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

            String OBS_FACT = null;
            if (String.IsNullOrEmpty(TextBox_OBS_FACT.Text) == false)
            {
                OBS_FACT = _tools.RemplazarCaracteresEnString(TextBox_OBS_FACT.Text.Trim());
            }

            String USU_CRE = Session["USU_LOG"].ToString();
            String USU_MOD = Session["USU_LOG"].ToString();

            String VAC_PARAF = "N"; 

            List<servicio> listaServicios = capturarListaServiciosDesdeSession();
            List<detalleServicio> listaDetalleServicio = capturarListaDetallesServicioDesdeSession();


            Decimal REGISTRO = 0;
            if (accion == "inicial")
            {
                REGISTRO = _condicionComercial.Adicionar(ID_EMPRESA, FACTURA, REGIMEN, SOLO_DEV, VAC_PARAF, DIAS_VNC, AD_NOM, AD_PENSION, AD_SALUD, AD_RIESGOS, AD_APO_SENA, AD_APO_ICBF, AD_APO_CAJA, AD_VACACIONES, AD_CESANTIA, AD_INT_CES, AD_PRIMA, AD_SEG_VID, SUB_PENSION, SUB_SALUD, SUB_RIESGO, SUB_SENA, SUB_ICBF, SUB_CAJA, SUB_VACACIONES, SUB_CESANTIAS, SUB_INT_CES, SUB_PRIMA, SUB_SEG_VID, MOD_SOPORTE, MOD_FACTURA, OBS_FACT, RET_VAC, RET_CES, RET_INT_CES, RET_PRIM, USU_CRE, APL_MTZ, ID_CIUDAD, ID_CENTRO_C, ID_SUB_C, listaServicios, listaDetalleServicio);

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
                                QueryStringSeguro["codCiudad"] = ID_CIUDAD.ToString();
                            }
                        }
                    }

                    Response.Redirect("~/comercial/condicionesEconomicasSertempoPorIdEspecifico.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));

                }
            }
            else
            {
                if (accion == "modificar")
                {
                    Boolean verificador = true;

                    REGISTRO = Convert.ToDecimal(QueryStringSeguro["codCondicion"]);

                    verificador = _condicionComercial.Actualizar(REGISTRO, ID_EMPRESA, FACTURA, REGIMEN, SOLO_DEV, VAC_PARAF, DIAS_VNC, AD_NOM, AD_PENSION, AD_SALUD, AD_RIESGOS, AD_APO_SENA, AD_APO_ICBF, AD_APO_CAJA, AD_VACACIONES, AD_CESANTIA, AD_INT_CES, AD_PRIMA, AD_SEG_VID, SUB_PENSION, SUB_SALUD, SUB_RIESGO, SUB_SENA, SUB_ICBF, SUB_CAJA, SUB_VACACIONES, SUB_CESANTIAS, SUB_INT_CES, SUB_PRIMA, SUB_SEG_VID, MOD_SOPORTE, MOD_FACTURA, OBS_FACT, RET_VAC, RET_CES, RET_INT_CES, RET_PRIM, USU_MOD, APL_MTZ, ID_CIUDAD, ID_CENTRO_C, ID_SUB_C, listaServicios, listaDetalleServicio);

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

                        Response.Redirect("~/comercial/condicionesEconomicasSertempoPorIdEspecifico.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
                    }
                }
            }
        }     
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        if (CheckBox_ACTIVAR_SERVICIO_COMPLEMENTARIO.Checked == true)
        {
            if (GridView_SERVICIOS_INCLUIDOS.Rows.Count <= 0)
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = "ADVERTENCIA: Por favor incluya en la lista los servicios complemetarios necesarios o inhabilite la opción de 'con servicios complementarios'.";
            }
            else
            {
                seEjecutaAlPresionarBotonGuardar();
            }
        }
        else
        { 
            seEjecutaAlPresionarBotonGuardar();
        }
        
    }

    private List<servicio> capturarListaServiciosDesdeSession()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
       
        List<servicio> listaResultado;
        try
        {
            listaResultado = (List<servicio>)Session["listaServicios_" + ID_EMPRESA.ToString()];
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

    private void activar_o_crear_servicio_en_lista_de_session()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"].ToString());

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
        String ID_CIUDAD = null;
        Decimal ID_CENTRO_C = 0;
        Decimal ID_SUB_C = 0;

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

        String NOMBRE_SERVICIO;

        if (ID_SUB_C != 0)
        {
            NOMBRE_SERVICIO = "EMPRESA_" + ID_EMPRESA.ToString() + "_SUBCC_" + ID_SUB_C.ToString();
        }
        else
        {
            if (ID_CENTRO_C != 0)
            {
                NOMBRE_SERVICIO = "EMPRESA_" + ID_EMPRESA.ToString() + "_CC_" + ID_CENTRO_C.ToString();
            }
            else
            {
                if ((ID_EMPRESA != 0) && (String.IsNullOrEmpty(ID_CIUDAD) == false))
                {
                    NOMBRE_SERVICIO = "EMPRESA_" + ID_EMPRESA.ToString() + "_CIUDAD_" + ID_CIUDAD;
                }
                else
                {
                    NOMBRE_SERVICIO = "EMPRESA_" + ID_EMPRESA.ToString();
                }
            }
        }

        List<servicio> listaServicio = capturarListaServiciosDesdeSession();

        if (listaServicio.Count <= 0)
        {
            servicio _servicio = new servicio(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            _servicio.ACCION = "INSERTAR";
            _servicio.AIU = 0;
            _servicio.DESCRIPCION = "Ninguno";
            _servicio.ID_SERVICIO = 0;
            _servicio.ID_SERVICIO_POR_EMPRESA = 0;
            _servicio.IVA = 0;
            _servicio.NOMBRE_SERVICIO = NOMBRE_SERVICIO;
            _servicio.VALOR = 0;

            listaServicio.Add(_servicio);
        }
        else
        {
            listaServicio[0].ACCION = "NINGUNA";
        }

        Session["listaServicios_" + ID_EMPRESA] = listaServicio;
    }

    private void desactivar_o_eliminar_servicio_en_lista_de_session()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"].ToString());

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

        List<servicio> listaServicio = capturarListaServiciosDesdeSession();
        List<detalleServicio> listaDetalles = capturarListaDetallesServicioDesdeSession();

        if (listaServicio.Count > 0)
        {
            if ((listaServicio[0].ID_SERVICIO == 0) && (listaServicio[0].ID_SERVICIO_POR_EMPRESA == 0))
            {
                listaServicio = new List<servicio>();
            }
            else
            {
                listaServicio[0].ACCION = "DESACTIVAR";
            }
        }

        if (listaDetalles.Count > 0)
        {
            for (int i = (listaDetalles.Count - 1); i >= 0; i--)
            {
                if (listaDetalles[i].ID_DETALLE_SERVICIO == 0)
                {
                    listaDetalles.RemoveAt(i);
                }
                else
                {
                    listaDetalles[i].ACCION = "DESACTIVAR";
                }
            }
        }

        Session["listaServicios_" + ID_EMPRESA.ToString()] = listaServicio;
        Session["listaDetallesServicio_" + ID_EMPRESA.ToString()] = listaDetalles;
    }

    protected void CheckBox_ACTIVAR_SERVICIO_COMPLEMENTARIO_CheckedChanged(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);


        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
        String ID_CIUDAD = null;
        Decimal ID_CENTRO_C = 0;
        Decimal ID_SUB_C = 0;

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

        if (CheckBox_ACTIVAR_SERVICIO_COMPLEMENTARIO.Checked == true)
        {
            cargarTextCheckActivarServicios(ID_EMPRESA, ID_CIUDAD, ID_CENTRO_C, ID_SUB_C, "con");
            Panel_SERVICIOS_COMPLEMENTARIOS.Visible = true;
            configurarMensajesServicioComplementario(false, System.Drawing.Color.Green);

            activar_o_crear_servicio_en_lista_de_session();

            cargar_DropDownList_CONFIGURACION();
            cargar_DropDownList_SERVICIOS_COMPLEMENTARIOS();
            limpiarTextBoxServicioAdicionar();
        }
        else
        {
            cargarTextCheckActivarServicios(ID_EMPRESA, ID_CIUDAD, ID_CENTRO_C, ID_SUB_C, "sin");
            Panel_SERVICIOS_COMPLEMENTARIOS.Visible = false;

            desactivar_o_eliminar_servicio_en_lista_de_session();

            GridView_SERVICIOS_INCLUIDOS.DataSource = null;
            GridView_SERVICIOS_INCLUIDOS.DataBind();
        }
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
                return  false;
            }
        }
    }
    protected void Button_ADICIONAR_SERVICIO_COMPLEMENTARIO_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);


        Boolean verificador = true;

        if (DropDownList_CONFIGURACION.SelectedValue == "AIU")
        {
            if (Convert.ToDecimal(TextBox_SER_ADMON.Text) <= 0)
            {
                verificador = false;
                configurarMensajesServicioComplementario(true, System.Drawing.Color.Red);
                Label_MENSAJE_SERVICIO_COMPLEMENTARIO.Text = "(Configuración: AIU), Debe digitar el PORCENTAJE DE ADMINISTRACIÓN para poder continuar.";
            }
        }
        else
        {
            if (DropDownList_CONFIGURACION.SelectedValue == "AIU_PREFERENCIAL")
            {
                if (Convert.ToDecimal(TextBox_SER_ADMON.Text) <= 0)
                {
                    verificador = false;
                    configurarMensajesServicioComplementario(true, System.Drawing.Color.Red);
                    Label_MENSAJE_SERVICIO_COMPLEMENTARIO.Text = "(Configuración: AIU PREFERENCIAL), Debe digitar el PORCENTAJE DE ADMINISTRACIÓN para poder continuar.";
                }
            }
            else
            {
                if (DropDownList_CONFIGURACION.SelectedValue == "TARIFA")
                {
                    if (Convert.ToDecimal(TextBox_SER_VALOR.Text) <= 0)
                    {
                        verificador = false;
                        configurarMensajesServicioComplementario(true, System.Drawing.Color.Red);
                        Label_MENSAJE_SERVICIO_COMPLEMENTARIO.Text = "(Configuración: TARIFA), Debe digitar el valorde la TARIFA para poder continuar.";
                    }
                }
                else
                {
                    if (DropDownList_CONFIGURACION.SelectedValue == "IVA")
                    {
                        if (Convert.ToDecimal(TextBox_SER_IVA.Text) <= 0)
                        {
                            verificador = false;
                            configurarMensajesServicioComplementario(true, System.Drawing.Color.Red);
                            Label_MENSAJE_SERVICIO_COMPLEMENTARIO.Text = "(Configuración: IVA), Debe digitar el valorde del IVA para poder continuar.";
                        }
                    }
                }
            }
        }


        if (verificador == true)
        {
            Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
            String ID_CIUDAD = null;
            Decimal ID_CENTRO_C = 0;
            Decimal ID_SUB_C = 0;

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


            configurarMensajesServicioComplementario(false, System.Drawing.Color.Green);

            DataTable tablaServiciosEnGrid = obtenerDataTableDeGridViewServiciosComplementarios();

            foreach (DataRow fila in tablaServiciosEnGrid.Rows)
            {
                if (fila["ID_SERVICIO_COMPLEMENTARIO"].ToString() == DropDownList_SERVICIOS_COMPLEMENTARIOS.SelectedValue)
                {
                    verificador = false;
                }
            }

            if (verificador == true)
            {
                List<servicio> listaServicio = capturarListaServiciosDesdeSession();
                List<detalleServicio> listaDetalles = capturarListaDetallesServicioDesdeSession();
                detalleServicio _detalleServicioParaLista;

                DataRow filaServicioAAdicionar = tablaServiciosEnGrid.NewRow();
                _detalleServicioParaLista = new detalleServicio(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

                condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());


                String ID_SERVICIO = listaServicio[0].ID_SERVICIO.ToString();
                String ID_SERVICIO_POR_EMPRESA = listaServicio[0].ID_SERVICIO_POR_EMPRESA.ToString();
                String NOMBRE_SERVICIO = listaServicio[0].NOMBRE_SERVICIO;

                DataTable tablaServiciosEspecificos;
                if (ID_SUB_C != 0)
                {
                    tablaServiciosEspecificos = _condicionComercial.ObtenerServiciosPorEmpresaPorIdSubC(ID_SUB_C);
                }
                else
                {
                    if (ID_CENTRO_C != 0)
                    {
                        tablaServiciosEspecificos = _condicionComercial.ObtenerServiciosPorEmpresaPorIdCentroC(ID_CENTRO_C);
                    }
                    else
                    {
                        if ((ID_EMPRESA != 0) && (String.IsNullOrEmpty(ID_CIUDAD) == false))
                        {
                            tablaServiciosEspecificos = _condicionComercial.ObtenerServiciosPorEmpresaPorIdCiudad(ID_CIUDAD, ID_EMPRESA);
                        }
                        else
                        {
                            tablaServiciosEspecificos = _condicionComercial.ObtenerServiciosPorEmpresaPorIdEmpresa(ID_EMPRESA);
                        }
                    }
                }

               
                filaServicioAAdicionar["ID_SERVICIO"] = ID_SERVICIO;
                filaServicioAAdicionar["ID_SERVICIO_COMPLEMENTARIO"] = DropDownList_SERVICIOS_COMPLEMENTARIOS.SelectedValue;
                filaServicioAAdicionar["NOMBRE_SERVICIO_COMPLEMENTARIO"] = DropDownList_SERVICIOS_COMPLEMENTARIOS.SelectedItem.Text;

                if (String.IsNullOrEmpty(TextBox_SER_ADMON.Text) == false)
                {
                    filaServicioAAdicionar["AIU"] = TextBox_SER_ADMON.Text;
                }
                else
                {
                    filaServicioAAdicionar["AIU"] = 0;
                }

                if (String.IsNullOrEmpty(TextBox_SER_IVA.Text) == false)
                {
                    filaServicioAAdicionar["IVA"] = TextBox_SER_IVA.Text;
                }
                else
                {
                    filaServicioAAdicionar["IVA"] = 0;
                }

                if (String.IsNullOrEmpty(TextBox_SER_VALOR.Text) == false)
                {
                    filaServicioAAdicionar["VALOR"] = TextBox_SER_VALOR.Text;
                }
                else
                {
                    filaServicioAAdicionar["VALOR"] = 0;
                }

                _detalleServicioParaLista.ACCION = "INSERTAR";
                _detalleServicioParaLista.AIU = Convert.ToDecimal(filaServicioAAdicionar["AIU"]);
                _detalleServicioParaLista.ID_DETALLE_SERVICIO = 0;
                _detalleServicioParaLista.ID_SERVICIO = Convert.ToDecimal(ID_SERVICIO);
                _detalleServicioParaLista.ID_SERVICIO_COMPLEMENTARIO = Convert.ToDecimal(filaServicioAAdicionar["ID_SERVICIO_COMPLEMENTARIO"]);
                _detalleServicioParaLista.ID_SERVICIO_POR_EMPRESA = Convert.ToDecimal(ID_SERVICIO_POR_EMPRESA);
                _detalleServicioParaLista.IVA = Convert.ToDecimal(filaServicioAAdicionar["IVA"]);
                _detalleServicioParaLista.NOMBRE_SERVICIO = NOMBRE_SERVICIO;
                _detalleServicioParaLista.VALOR = Convert.ToDecimal(filaServicioAAdicionar["VALOR"]);

                listaDetalles.Add(_detalleServicioParaLista);
                tablaServiciosEnGrid.Rows.Add(filaServicioAAdicionar);

                DataView DV = new DataView(tablaServiciosEnGrid, "", "NOMBRE_SERVICIO_COMPLEMENTARIO", DataViewRowState.CurrentRows);
                GridView_SERVICIOS_INCLUIDOS.DataSource = DV;
                GridView_SERVICIOS_INCLUIDOS.DataBind();

                cargar_DropDownList_SERVICIOS_COMPLEMENTARIOS();
                cargar_DropDownList_CONFIGURACION();
                limpiarTextBoxServicioAdicionar();

                Session["listaDetallesServicio_" + ID_EMPRESA.ToString()] = listaDetalles;
            }
            else
            {
                configurarMensajesServicioComplementario(true, System.Drawing.Color.Red);
                Label_MENSAJE_SERVICIO_COMPLEMENTARIO.Text = "ADVERTENCIA: Servicio complementario que desea agregar ya se encuentra en el lista.<br>Para actualizar la información de un servicio complementario debe primero eliminarlo del la lista.";
            }
        }
    }

    protected void GridView_SERVICIOS_INCLUIDOS_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable tablaServiciosEnGrid = obtenerDataTableDeGridViewServiciosComplementarios();

        List<detalleServicio> listaDetalles = capturarListaDetallesServicioDesdeSession();

        DataRow filaSeleccionada = tablaServiciosEnGrid.Rows[GridView_SERVICIOS_INCLUIDOS.SelectedIndex];

        String ID_SERVICIO_COMPLEMENTARIO = filaSeleccionada["ID_SERVICIO_COMPLEMENTARIO"].ToString();
        String AIU = filaSeleccionada["AIU"].ToString();
        String IVA = filaSeleccionada["IVA"].ToString();
        String VALOR = filaSeleccionada["VALOR"].ToString();

        tablaServiciosEnGrid.Rows[GridView_SERVICIOS_INCLUIDOS.SelectedIndex].Delete();

        GridView_SERVICIOS_INCLUIDOS.DataSource = tablaServiciosEnGrid;
        GridView_SERVICIOS_INCLUIDOS.DataBind();


        for (int i = (listaDetalles.Count - 1); i >= 0; i--)
        {
            if ((listaDetalles[i].ID_SERVICIO_COMPLEMENTARIO == Convert.ToDecimal(ID_SERVICIO_COMPLEMENTARIO)) && (listaDetalles[i].ACCION != "DESACTIVAR"))
            {
                if (listaDetalles[i].ID_DETALLE_SERVICIO == 0)
                {
                    listaDetalles.RemoveAt(i);
                }
                else
                {
                    listaDetalles[i].ACCION = "DESACTIVAR";
                }
            }
        }
    }
    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
        String ID_CIUDAD = null;
        Decimal ID_CENTRO_C = 0;
        Decimal ID_SUB_C = 0;

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

        Response.Redirect("~/comercial/condicionesEconomicasSertempoPorIdEspecifico.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }

    protected void Button_VOLVER_MENU_EMPRESA_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String ID_EMPRESA = QueryStringSeguro["reg"].ToString();

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
        QueryStringSeguro["img_area"] = "comercial";
        QueryStringSeguro["nombre_area"] = "GESTIÓN COMERCIAL";
        QueryStringSeguro["nombre_modulo"] = "CONDICIONES ECONÓMICAS";
        QueryStringSeguro["accion"] = "inicial";
        QueryStringSeguro["reg"] = ID_EMPRESA;

        Response.Redirect("~/comercial/condicionesEconomicasSertempo.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }
    protected void DropDownList_CONFIGURACION_SelectedIndexChanged(object sender, EventArgs e)
    {
        limpiarTextBoxServicioAdicionar();

        if (DropDownList_CONFIGURACION.SelectedIndex <= 0)
        {
            RequiredFieldValidator_TextBox_SER_ADMON.Enabled = true;
            ValidatorCalloutExtender_TextBox_SER_ADMON.Enabled = true;

            RequiredFieldValidator_TextBox_SER_IVA.Enabled = true;
            ValidatorCalloutExtender_TextBox_SER_IVA.Enabled = true;
            RangeValidator_TextBox_SER_IVA.Enabled = true;
            ValidatorCalloutExtender_TextBox_SER_IVA_1.Enabled = true;

            RequiredFieldValidator_TextBox_SER_VALOR.Enabled = true;
            ValidatorCalloutExtender_TextBox_SER_VALOR.Enabled = true;
        }
        else
        {
            if (DropDownList_CONFIGURACION.SelectedValue == "AIU")
            {
                if (String.IsNullOrEmpty(TextBox_AD_NOM.Text) == false)
                {
                    RequiredFieldValidator_TextBox_SER_ADMON.Enabled = true;
                    ValidatorCalloutExtender_TextBox_SER_ADMON.Enabled = true;

                    RequiredFieldValidator_TextBox_SER_IVA.Enabled = true;
                    ValidatorCalloutExtender_TextBox_SER_IVA.Enabled = true;
                    RangeValidator_TextBox_SER_IVA.Enabled = false;
                    ValidatorCalloutExtender_TextBox_SER_IVA_1.Enabled = false;

                    RequiredFieldValidator_TextBox_SER_VALOR.Enabled = false;
                    ValidatorCalloutExtender_TextBox_SER_VALOR.Enabled = false;

                    TextBox_SER_ADMON.Text = TextBox_AD_NOM.Text;

                    TextBox_SER_ADMON.Enabled = false;
                    TextBox_SER_IVA.Enabled = true;
                    TextBox_SER_VALOR.Enabled = false;
                }
                else
                {
                    configurarMensajesServicioComplementario(true, System.Drawing.Color.Red);
                    Label_MENSAJE_SERVICIO_COMPLEMENTARIO.Text = "Antes de configurar Servicios complementarios para esta empresa, se debe diligenciar las condiciones económicas.";

                    DropDownList_CONFIGURACION.SelectedIndex = 0;

                    RequiredFieldValidator_TextBox_SER_ADMON.Enabled = true;
                    ValidatorCalloutExtender_TextBox_SER_ADMON.Enabled = true;

                    RequiredFieldValidator_TextBox_SER_IVA.Enabled = true;
                    ValidatorCalloutExtender_TextBox_SER_IVA.Enabled = true;
                    RangeValidator_TextBox_SER_IVA.Enabled = true;
                    ValidatorCalloutExtender_TextBox_SER_IVA_1.Enabled = true;

                    RequiredFieldValidator_TextBox_SER_VALOR.Enabled = true;
                    ValidatorCalloutExtender_TextBox_SER_VALOR.Enabled = true;

                    TextBox_SER_ADMON.Enabled = true;
                    TextBox_SER_IVA.Enabled = true;
                    TextBox_SER_VALOR.Enabled = true;
                }
            }
            else
            {
                if (DropDownList_CONFIGURACION.SelectedValue == "AIU_PREFERENCIAL")
                {
                    RequiredFieldValidator_TextBox_SER_ADMON.Enabled = true;
                    ValidatorCalloutExtender_TextBox_SER_ADMON.Enabled = true;

                    RequiredFieldValidator_TextBox_SER_IVA.Enabled = true;
                    ValidatorCalloutExtender_TextBox_SER_IVA.Enabled = true;
                    RangeValidator_TextBox_SER_IVA.Enabled = false;
                    ValidatorCalloutExtender_TextBox_SER_IVA_1.Enabled = false;

                    RequiredFieldValidator_TextBox_SER_VALOR.Enabled = false;
                    ValidatorCalloutExtender_TextBox_SER_VALOR.Enabled = false;

                    TextBox_SER_ADMON.Enabled = true;
                    TextBox_SER_IVA.Enabled = true;
                    TextBox_SER_VALOR.Enabled = false;

                    TextBox_SER_ADMON.Text = "";
                }
                else
                {
                    if (DropDownList_CONFIGURACION.SelectedValue == "TARIFA")
                    {
                        RequiredFieldValidator_TextBox_SER_ADMON.Enabled = false;
                        ValidatorCalloutExtender_TextBox_SER_ADMON.Enabled = false;

                        RequiredFieldValidator_TextBox_SER_IVA.Enabled = true;
                        ValidatorCalloutExtender_TextBox_SER_IVA.Enabled = true;
                        RangeValidator_TextBox_SER_IVA.Enabled = false;
                        ValidatorCalloutExtender_TextBox_SER_IVA_1.Enabled = false;

                        RequiredFieldValidator_TextBox_SER_VALOR.Enabled = true;
                        ValidatorCalloutExtender_TextBox_SER_VALOR.Enabled = true;

                        TextBox_SER_ADMON.Enabled = false;
                        TextBox_SER_IVA.Enabled = true;
                        TextBox_SER_VALOR.Enabled = true;

                        TextBox_SER_VALOR.Text = "";
                    }
                    else
                    {
                        if (DropDownList_CONFIGURACION.SelectedValue == "IVA")
                        {
                            RequiredFieldValidator_TextBox_SER_ADMON.Enabled = false;
                            ValidatorCalloutExtender_TextBox_SER_ADMON.Enabled = false;

                            RequiredFieldValidator_TextBox_SER_IVA.Enabled = true;
                            ValidatorCalloutExtender_TextBox_SER_IVA.Enabled = true;
                            RangeValidator_TextBox_SER_IVA.Enabled = true;
                            ValidatorCalloutExtender_TextBox_SER_IVA_1.Enabled = true;

                            RequiredFieldValidator_TextBox_SER_VALOR.Enabled = false;
                            ValidatorCalloutExtender_TextBox_SER_VALOR.Enabled = false;

                            TextBox_SER_ADMON.Enabled = false;
                            TextBox_SER_IVA.Enabled = true;
                            TextBox_SER_VALOR.Enabled = false;

                            TextBox_SER_IVA.Text = "";
                        }
                        else
                        {
                            if (DropDownList_CONFIGURACION.SelectedValue == "REEMBOLSO_DE_GASTOS")
                            {
                                RequiredFieldValidator_TextBox_SER_ADMON.Enabled = false;
                                ValidatorCalloutExtender_TextBox_SER_ADMON.Enabled = false;

                                RequiredFieldValidator_TextBox_SER_IVA.Enabled = false;
                                ValidatorCalloutExtender_TextBox_SER_IVA.Enabled = false;
                                RangeValidator_TextBox_SER_IVA.Enabled = false;
                                ValidatorCalloutExtender_TextBox_SER_IVA_1.Enabled = false;

                                RequiredFieldValidator_TextBox_SER_VALOR.Enabled = false;
                                ValidatorCalloutExtender_TextBox_SER_VALOR.Enabled = false;

                                TextBox_SER_ADMON.Enabled = false;
                                TextBox_SER_IVA.Enabled = false;
                                TextBox_SER_VALOR.Enabled = false;
                            }
                            else
                            {
                                RequiredFieldValidator_TextBox_SER_ADMON.Enabled = false;
                                ValidatorCalloutExtender_TextBox_SER_ADMON.Enabled = false;

                                RequiredFieldValidator_TextBox_SER_IVA.Enabled = false;
                                ValidatorCalloutExtender_TextBox_SER_IVA.Enabled = false;
                                RangeValidator_TextBox_SER_IVA.Enabled = false;
                                ValidatorCalloutExtender_TextBox_SER_IVA_1.Enabled = false;

                                RequiredFieldValidator_TextBox_SER_VALOR.Enabled = false;
                                ValidatorCalloutExtender_TextBox_SER_VALOR.Enabled = false;

                                TextBox_SER_ADMON.Enabled = false;
                                TextBox_SER_IVA.Enabled = false;
                                TextBox_SER_VALOR.Enabled = false;
                            }
                        }
                    }
                }
            }
        }




        if (DropDownList_CONFIGURACION.SelectedIndex <= 0)
        {
            RequiredFieldValidator_TextBox_SER_ADMON.Enabled = true;
            ValidatorCalloutExtender_TextBox_SER_ADMON.Enabled = true;

            RequiredFieldValidator_TextBox_SER_IVA.Enabled = true;
            ValidatorCalloutExtender_TextBox_SER_IVA.Enabled = true;
            RangeValidator_TextBox_SER_IVA.Enabled = true;
            ValidatorCalloutExtender_TextBox_SER_IVA_1.Enabled = true;

            RequiredFieldValidator_TextBox_SER_VALOR.Enabled = true;
            ValidatorCalloutExtender_TextBox_SER_VALOR.Enabled = true;
        }
        else
        {
            if (DropDownList_CONFIGURACION.SelectedValue == "AIU_TARIFA")
            {
                RequiredFieldValidator_TextBox_SER_ADMON.Enabled = true;
                ValidatorCalloutExtender_TextBox_SER_ADMON.Enabled = true;

                RequiredFieldValidator_TextBox_SER_IVA.Enabled = true;
                ValidatorCalloutExtender_TextBox_SER_IVA.Enabled = true;
                RangeValidator_TextBox_SER_IVA.Enabled = false;
                ValidatorCalloutExtender_TextBox_SER_IVA_1.Enabled = false;

                RequiredFieldValidator_TextBox_SER_VALOR.Enabled = true;
                ValidatorCalloutExtender_TextBox_SER_VALOR.Enabled = true;
            }
            else
            {
                if (DropDownList_CONFIGURACION.SelectedValue == "IVA")
                {
                    RequiredFieldValidator_TextBox_SER_ADMON.Enabled = true;
                    ValidatorCalloutExtender_TextBox_SER_ADMON.Enabled = true;

                    RequiredFieldValidator_TextBox_SER_IVA.Enabled = true;
                    ValidatorCalloutExtender_TextBox_SER_IVA.Enabled = true;
                    RangeValidator_TextBox_SER_IVA.Enabled = true;
                    ValidatorCalloutExtender_TextBox_SER_IVA_1.Enabled = true;

                    RequiredFieldValidator_TextBox_SER_VALOR.Enabled = true;
                    ValidatorCalloutExtender_TextBox_SER_VALOR.Enabled = true;
                }
                else
                {
                    RequiredFieldValidator_TextBox_SER_ADMON.Enabled = true;
                    ValidatorCalloutExtender_TextBox_SER_ADMON.Enabled = true;

                    RequiredFieldValidator_TextBox_SER_IVA.Enabled = true;
                    ValidatorCalloutExtender_TextBox_SER_IVA.Enabled = true;
                    RangeValidator_TextBox_SER_IVA.Enabled = false;
                    ValidatorCalloutExtender_TextBox_SER_IVA_1.Enabled = false;

                    RequiredFieldValidator_TextBox_SER_VALOR.Enabled = true;
                    ValidatorCalloutExtender_TextBox_SER_VALOR.Enabled = true;
                }
            }
        }
    }
    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {
        configurarMensajes(false, System.Drawing.Color.Green);
    }
    protected void Button_MENSAJE_SERVICIO_COMPLEMENTARIO_Click(object sender, EventArgs e)
    {
        configurarMensajesServicioComplementario(false, System.Drawing.Color.Green);
    }
}