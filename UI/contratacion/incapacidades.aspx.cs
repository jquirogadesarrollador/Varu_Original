using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.IO;
using TSHAK.Components;

using Brainsbits.LLB;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.nomina;
using Brainsbits.LLB.seguridad;

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

    #region varialbles
    private enum Acciones
    {
        Inicio = 0,
        BusquedaEncontro = 1,
        BusquedaNoEncontro = 2,
        Adiciona = 3,
        Guarda = 4,
        Modifica = 5,
        Visualiza = 6,
        BusquedaEncontroIncapacidades = 7,
        BusquedaNoEncontroIncapacidades = 8,
        Carencia = 9
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }

    private enum Datos
    {
        Contrato = 0,
        Incapacidad = 1
    }
    #endregion varialbles

    #region constructor
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Iniciar();
        }
    }
    #endregion constructor

    #region metodos
    private void Iniciar()
    {
        Ocultar();
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
        Bloquear(Acciones.Inicio);
        Configurar();
    }

    private void Ocultar()
    {
        this.Panel_INFO_ADICIONAL_MODULO.Visible = false;
        this.Panel_FORM_BOTONES_ENCABEZADO.Visible = false;
        this.Panel_MENSAJES.Visible = false;
        this.Panel_RESULTADOS_GRID.Visible = false;
        this.Panel_CONTROL_REGISTRO.Visible = false;
        this.Panel_FORMULARIO.Visible = false;
        this.Panel_DATOS.Visible = false;
        this.Panel_FORM_BOTONES_PIE.Visible = false;
        this.Panel_DATOS_CONTRATO.Visible = false;
        this.Panel_RESULTADOS_GRID_INCAPACIDADES.Visible = false;

        Button_NUEVO.Visible = false;
        Button_GUARDAR.Visible = false;
        Button_MODIFICAR.Visible = false;
        Button_CANCELAR.Visible = false;

        Button_GUARDAR_1.Visible = false;
        Button_MODIFICAR_1.Visible = false;
        Button_CANCELAR_1.Visible = false;

        Label_ID.Visible = false;
        TextBox_ID.Visible = false;
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                break;
            case Acciones.Adiciona:
                Panel_INFO_ADICIONAL_MODULO.Visible = true;
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Panel_FORMULARIO.Visible = true;
                Panel_DATOS.Visible = true;
                Panel_FORM_BOTONES_PIE.Visible = true;
                Panel_DATOS_CONTRATO.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
                break;
            case Acciones.BusquedaEncontro:
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                this.Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.BusquedaNoEncontro:
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                this.Panel_MENSAJES.Visible = true;
                break;
            case Acciones.Guarda:
                this.Panel_INFO_ADICIONAL_MODULO.Visible = true;
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                this.Panel_MENSAJES.Visible = true;
                this.Panel_FORMULARIO.Visible = true;
                this.Panel_DATOS_CONTRATO.Visible = true;
                this.Panel_DATOS.Visible = true;
                this.Panel_FORM_BOTONES_PIE.Visible = true;
                this.Button_MODIFICAR.Visible = true;
                this.Button_MODIFICAR_1.Visible = true;
                this.Button_CANCELAR.Visible = true;
                this.Button_CANCELAR_1.Visible = true;
                this.Button_NUEVO.Visible = true;
                break;
            case Acciones.Modifica:
                this.Panel_INFO_ADICIONAL_MODULO.Visible = true;
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                this.Panel_CONTROL_REGISTRO.Visible = true;
                this.Panel_FORMULARIO.Visible = true;
                this.Panel_DATOS_CONTRATO.Visible = true;
                this.Panel_DATOS.Visible = true;
                this.Panel_FORM_BOTONES_PIE.Visible = true;
                this.Button_GUARDAR.Visible = true;
                this.Button_GUARDAR_1.Visible = true;
                this.Button_CANCELAR.Visible = true;
                this.Button_CANCELAR_1.Visible = true;
                break;
            case Acciones.Visualiza:
                this.Panel_INFO_ADICIONAL_MODULO.Visible = true;
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                this.Panel_CONTROL_REGISTRO.Visible = true;
                this.Panel_DATOS_CONTRATO.Visible = true;
                this.Panel_FORMULARIO.Visible = true;
                this.Panel_DATOS.Visible = true;
                this.Panel_FORM_BOTONES_PIE.Visible = true;
                this.Button_MODIFICAR.Visible = true;
                this.Button_CANCELAR.Visible = true;
                this.Button_CANCELAR_1.Visible = true;
                break;
            case Acciones.BusquedaEncontroIncapacidades:
                this.Panel_INFO_ADICIONAL_MODULO.Visible = true;
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                this.Panel_DATOS_CONTRATO.Visible = true;
                this.Panel_RESULTADOS_GRID_INCAPACIDADES.Visible = true;
                this.Panel_FORM_BOTONES_PIE.Visible = true;
                this.Button_NUEVO.Visible = true;
                this.Button_CANCELAR.Visible = true;
                this.Button_CANCELAR_1.Visible = true;
                break;
            case Acciones.BusquedaNoEncontroIncapacidades:
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                this.Panel_FORM_BOTONES_PIE.Visible = true;
                this.Panel_DATOS_CONTRATO.Visible = true;
                this.Button_NUEVO.Visible = true;
                this.Panel_MENSAJES.Visible = true;
                break;
            case Acciones.Carencia:
                this.Panel_INFO_ADICIONAL_MODULO.Visible = true;
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                this.Panel_DATOS_CONTRATO.Visible = true;
                this.Panel_MENSAJES.Visible = true;
                this.Panel_FORM_BOTONES_PIE.Visible = true;
                this.Button_CANCELAR.Visible = true;
                this.Button_CANCELAR_1.Visible = true;
                break;
        }
    }

    private void Cargar(Acciones accion)
    {
        ListItem item = new ListItem("Ninguno", "0");
        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        switch (accion)
        {
            case Acciones.Inicio:
                DropDownList_BUSCAR.Items.Add(item);
                item = new ListItem("Número de documento", "NUMERO_DOCUMENTO");
                DropDownList_BUSCAR.Items.Add(item);
                item = new ListItem("Nombre", "NOMBRE");
                DropDownList_BUSCAR.Items.Add(item);
                DropDownList_BUSCAR.DataBind();

                Cargar(DropDownList_INC_CARENCIA, tabla.PARAMETROS_PERIODO_CARENCIA);
                Cargar(DropDownList_TIPO_INCA, tabla.PARAMETROS_TIPO_INCAPACIDAD);
                Cargar(DropDownList_SEVERO, tabla.PARAMETROS_CASO_SEVERO);
                Cargar(DropDownList_CLASE_INCA, tabla.PARAMETROS_CLASE_INCAPACIDAD);
                Cargar(DropDownList_PRORROGA, tabla.PARAMETROS_PRORROGA);
                Cargar(DropDownList_estado, tabla.PARAMETROS_ESTADO_INCAPACIDAD);
                Cargar(DropDownList_estado_tramite, tabla.PARAMETROS_ESTADO_INCAPACIDAD_TRAMITE);
                Cargar(DropDownList_tramitada_por, tabla.PARAMETROS_INCAPACIDAD_TRAMITADA_POR);
                break;
        }
    }

    private void Cargar(DropDownList dropDownList, String tabla)
    {
        ListItem item = new ListItem("Seleccione", "");
        parametro _parametro = new parametro(Session["idEmpresa"].ToString());
        DataTable _dataTable = _parametro.ObtenerParametrosPorTabla(tabla);
        dropDownList.Items.Add(item);
        foreach (DataRow _dataRow in _dataTable.Rows)
        {
            item = new ListItem(_dataRow["DESCRIPCION"].ToString(), _dataRow["CODIGO"].ToString());
            dropDownList.Items.Add(item);
        }
        dropDownList.DataBind();
        _dataTable.Dispose();
    }

    private void Cargar(DropDownList dropDownList, DataTable _dataTable, String id)
    {
        dropDownList.Items.Clear();
        ListItem item = new ListItem("Seleccione", "");
        dropDownList.Items.Add(item);

        foreach (DataRow _dataRow in _dataTable.Rows)
        {
            item = new ListItem(_dataRow["NOMBRE"].ToString(), _dataRow[id].ToString());
            dropDownList.Items.Add(item);
        }

        dropDownList.DataBind();
    }

    private void LimpiarDatosINcapacidad()
    {
        GridView_RESULTADOS_BUSQUEDA_INCAPACIDADES.DataSource = null;
        GridView_RESULTADOS_BUSQUEDA_INCAPACIDADES.DataBind();

        TextBox_total_dias_incapacidad.Text = "";

        TextBox_FCH_CRE.Text = "";
        TextBox_HOR_CRE.Text = "";
        TextBox_USU_CRE.Text = "";
        TextBox_FCH_MOD.Text = "";
        TextBox_HOR_MOD.Text = "";
        TextBox_USU_MOD.Text = "";

        TextBox_ID.Text = "";
        DropDownList_estado.ClearSelection();
        TextBox_FECHA.Text = "";
        DropDownList_tramitada_por.ClearSelection();
        TextBox_VAL_RECONOCIDO.Text = "";
        TextBox_NUM_AUTORIZA.Text = "";
        TextBox_fecha_paga_nomina_desde.Text = "";
        TextBox_fecha_paga_nomina_hasta.Text = "";
        TextBox_FCH_INI_REAL.Text = "";
        TextBox_FCH_TER_REAL.Text = "";
        TextBox_DIAS_INCAP.Text = "";
        DropDownList_INC_CARENCIA.ClearSelection();
        DropDownList_TIPO_INCA.ClearSelection();
        DropDownList_SEVERO.ClearSelection();
        DropDownList_CLASE_INCA.ClearSelection();
        DropDownList_ID_CONCEPTO.ClearSelection();
        DropDownList_PRORROGA.ClearSelection();
        TextBox_VALOR_LIQUIDADO_NOMINA.Text = "";
        TextBox_OBS_REG.Text = "";

        TextBox_BUSCADOR_DIAGNOSTICO.Text = "";
        DropDownList_DSC_DIAG.ClearSelection();

        TextBox_dias_pagados.Text = "";
        TextBox_dias_pendientes.Text = "";

        GridView_pagos_nomina.DataSource = null;
        GridView_pagos_nomina.DataBind();

        DropDownList_estado_tramite.ClearSelection();

        TextBox_transcripcion_fecha_radicacion.Text = "";
        TextBox_transcripcion_fecha_seguimiento.Text = "";
        CheckBox_transcripcion_VoBo.Checked = false;
        TextBox_transcripcion_numero.Text = "";
        TextBox_transcripcion_valor.Text = "";
        TextBox_transcripcion_notas.Text = "";

        TextBox_liquidacion_fecha_radicacion.Text = "";
        TextBox_liquidacion_fecha_seguimiento.Text = "";
        CheckBox_liquidacion_VoBo.Checked = false;
        TextBox_liquidacion_numero.Text = "";
        TextBox_liquidacion_valor.Text = "";
        TextBox_liquidacion_notas.Text = "";

        TextBox_reliquidacion_fecha_radicacion.Text = "";
        TextBox_reliquidacion_fecha_seguimiento.Text = "";
        CheckBox_reliquidacion_VoBo.Checked = false;
        TextBox_reliquidacion_numero.Text = "";
        TextBox_reliquidacion_valor.Text = "";
        TextBox_reliquidacion_notas.Text = "";

        TextBox_cobro_fecha_radicacion.Text = "";
        TextBox_cobro_fecha_seguimiento.Text = "";
        CheckBox_cobro_VoBo.Checked = false;
        TextBox_cobro_numero.Text = "";
        TextBox_cobro_valor.Text = "";
        TextBox_cobro_notas.Text = "";

        TextBox_pago_fecha_radicacion.Text = "";
        TextBox_pago_fecha_seguimiento.Text = "";
        CheckBox_pago_VoBo.Checked = false;
        TextBox_pago_numero.Text = "";
        TextBox_pago_valor.Text = "";
        TextBox_pago_notas.Text = "";

        TextBox_objetada_fecha_radicacion.Text = "";
        TextBox_objetada_fecha_seguimiento.Text = "";
        CheckBox_objetada_VoBo.Checked = false;
        TextBox_objetada_numero.Text = "";
        TextBox_objetada_valor.Text = "";
        TextBox_objetada_notas.Text = "";

        TextBox_negada_fecha_radicacion.Text = "";
        TextBox_negada_fecha_seguimiento.Text = "";
        CheckBox_negada_VoBo.Checked = false;
        TextBox_negada_numero.Text = "";
        TextBox_negada_valor.Text = "";
        TextBox_negada_notas.Text = "";

   }


    private void Cargar(DataTable dataTable)
    {
        LimpiarDatosINcapacidad();

        if (dataTable.Rows.Count > 0)
        {
            foreach (DataRow _dataRow in dataTable.Rows)
            {
                if (!String.IsNullOrEmpty(_dataRow["FCH_CRE"].ToString())) this.TextBox_FCH_CRE.Text = _dataRow["FCH_CRE"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["USU_CRE"].ToString())) this.TextBox_USU_CRE.Text = _dataRow["USU_CRE"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["FCH_MOD"].ToString())) this.TextBox_FCH_MOD.Text = _dataRow["FCH_MOD"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["USU_MOD"].ToString())) this.TextBox_USU_MOD.Text = _dataRow["USU_MOD"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["REGISTRO"].ToString())) this.TextBox_ID.Text = _dataRow["REGISTRO"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["FCH_INI_REAL"].ToString())) this.TextBox_FCH_INI_REAL.Text = _dataRow["FCH_INI_REAL"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["FCH_TER_REAL"].ToString())) this.TextBox_FCH_TER_REAL.Text = _dataRow["FCH_TER_REAL"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["DIAS_INCAP"].ToString())) this.TextBox_DIAS_INCAP.Text = _dataRow["DIAS_INCAP"].ToString();

                try
                {
                    if (!String.IsNullOrEmpty(_dataRow["VAL_RECONOCIDO"].ToString()))
                    {
                        this.TextBox_VAL_RECONOCIDO.Text = Decimal.Round(Convert.ToDecimal(_dataRow["VAL_RECONOCIDO"]),0).ToString();
                    }
                    else
                    {
                        this.TextBox_VAL_RECONOCIDO.Text = "0";
                    }
                }
                catch
                {
                    this.TextBox_VAL_RECONOCIDO.Text = "0";
                }

                if (!String.IsNullOrEmpty(_dataRow["NUM_AUTORIZA"].ToString())) this.TextBox_NUM_AUTORIZA.Text = _dataRow["NUM_AUTORIZA"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["FECHA"].ToString())) this.TextBox_FECHA.Text = _dataRow["FECHA"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["OBS_REG"].ToString())) this.TextBox_OBS_REG.Text = HttpUtility.HtmlDecode(_dataRow["OBS_REG"].ToString());
                if (!String.IsNullOrEmpty(_dataRow["INC_CARENCIA"].ToString())) this.DropDownList_INC_CARENCIA.SelectedValue = _dataRow["INC_CARENCIA"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["TIPO_INCA"].ToString())) this.DropDownList_TIPO_INCA.SelectedValue = _dataRow["TIPO_INCA"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["SEVERO"].ToString())) this.DropDownList_SEVERO.SelectedValue = _dataRow["SEVERO"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["CLASE_INCA"].ToString()))
                {
                    DropDownList_CLASE_INCA.SelectedValue = _dataRow["CLASE_INCA"].ToString();
                    parametroIncapacidad _parametroIncapacidad = new parametroIncapacidad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                    DataTable d = _parametroIncapacidad.ObtenerPorClaseIncapacidad(_dataRow["CLASE_INCA"].ToString());

                    Cargar(DropDownList_ID_CONCEPTO, d, "ID_CONCEPTO");
                    try
                    {
                        DropDownList_ID_CONCEPTO.SelectedValue = _dataRow["ID_CONCEPTO"].ToString();
                    }
                    catch
                    {
                        DropDownList_ID_CONCEPTO.ClearSelection();
                    }
                }


                if (!String.IsNullOrEmpty(_dataRow["PRORROGA"].ToString())) this.DropDownList_PRORROGA.SelectedValue = _dataRow["PRORROGA"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["VALOR_LIQUIDADO_NOMINA"].ToString())) this.TextBox_VALOR_LIQUIDADO_NOMINA.Text = _dataRow["VALOR_LIQUIDADO_NOMINA"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["COD_DIAG"].ToString())) Cargar(_dataRow["COD_DIAG"].ToString());


                if (!String.IsNullOrEmpty(_dataRow["DIAS_PEND_POR_PAGAR"].ToString())) this.TextBox_dias_pendientes.Text = _dataRow["DIAS_PEND_POR_PAGAR"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["DIAS_PEND_POR_DESCONTAR"].ToString())) this.TextBox_dias_pagados.Text = _dataRow["DIAS_PEND_POR_DESCONTAR"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["ESTADO"].ToString())) this.DropDownList_estado.SelectedValue = _dataRow["ESTADO"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["TRAMITADA_POR"].ToString())) this.DropDownList_tramitada_por.SelectedValue = _dataRow["TRAMITADA_POR"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["ESTADO_TRAMITE"].ToString())) this.DropDownList_estado_tramite.SelectedValue = _dataRow["ESTADO_TRAMITE"].ToString();

                if (!String.IsNullOrEmpty(_dataRow["transcripcion_fecha_radicacion"].ToString())) this.TextBox_transcripcion_fecha_radicacion.Text = _dataRow["transcripcion_fecha_radicacion"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["transcripcion_fecha_seguimiento"].ToString())) this.TextBox_transcripcion_fecha_seguimiento.Text = _dataRow["transcripcion_fecha_seguimiento"].ToString();
                
                if (!String.IsNullOrEmpty(_dataRow["transcripcion_vobo"].ToString())) 
                {
                    if(_dataRow["transcripcion_vobo"].ToString().Equals("S")) CheckBox_transcripcion_VoBo.Checked = true;
                    else CheckBox_transcripcion_VoBo.Checked = false;
                }
                if (!String.IsNullOrEmpty(_dataRow["transcripcion_numero"].ToString())) this.TextBox_transcripcion_numero.Text = _dataRow["transcripcion_numero"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["transcripcion_valor"].ToString())) this.TextBox_transcripcion_valor.Text = _dataRow["transcripcion_valor"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["transcripcion_notas"].ToString())) this.TextBox_transcripcion_notas.Text = _dataRow["transcripcion_notas"].ToString();


                if (!String.IsNullOrEmpty(_dataRow["liquidacion_fecha_radicacion"].ToString())) this.TextBox_liquidacion_fecha_radicacion.Text = _dataRow["liquidacion_fecha_radicacion"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["liquidacion_fecha_seguimiento"].ToString())) this.TextBox_liquidacion_fecha_seguimiento.Text = _dataRow["liquidacion_fecha_seguimiento"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["liquidacion_vobo"].ToString())) 
                {
                    if(_dataRow["liquidacion_vobo"].ToString().Equals("S")) CheckBox_liquidacion_VoBo.Checked = true;
                    else CheckBox_liquidacion_VoBo.Checked = false;
                }
                if (!String.IsNullOrEmpty(_dataRow["liquidacion_numero"].ToString())) this.TextBox_liquidacion_numero.Text = _dataRow["liquidacion_numero"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["liquidacion_valor"].ToString())) this.TextBox_liquidacion_valor.Text = _dataRow["liquidacion_valor"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["liquidacion_notas"].ToString())) this.TextBox_liquidacion_notas.Text = _dataRow["liquidacion_notas"].ToString();


                if (!String.IsNullOrEmpty(_dataRow["reliquidacion_fecha_radicacion"].ToString())) this.TextBox_reliquidacion_fecha_radicacion.Text = _dataRow["reliquidacion_fecha_radicacion"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["reliquidacion_fecha_seguimiento"].ToString())) this.TextBox_reliquidacion_fecha_seguimiento.Text = _dataRow["reliquidacion_fecha_seguimiento"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["reliquidacion_vobo"].ToString())) 
                {
                    if(_dataRow["reliquidacion_vobo"].ToString().Equals("S")) CheckBox_reliquidacion_VoBo.Checked = true;
                    else CheckBox_reliquidacion_VoBo.Checked = false;
                }
                if (!String.IsNullOrEmpty(_dataRow["reliquidacion_numero"].ToString())) this.TextBox_reliquidacion_numero.Text = _dataRow["reliquidacion_numero"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["reliquidacion_valor"].ToString())) this.TextBox_reliquidacion_valor.Text = _dataRow["reliquidacion_valor"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["reliquidacion_notas"].ToString())) this.TextBox_reliquidacion_notas.Text = _dataRow["reliquidacion_notas"].ToString();

                
                if (!String.IsNullOrEmpty(_dataRow["cobro_fecha_radicacion"].ToString())) this.TextBox_cobro_fecha_radicacion.Text = _dataRow["cobro_fecha_radicacion"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["cobro_fecha_seguimiento"].ToString())) this.TextBox_cobro_fecha_seguimiento.Text = _dataRow["cobro_fecha_seguimiento"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["cobro_vobo"].ToString())) 
                {
                    if(_dataRow["cobro_vobo"].ToString().Equals("S")) CheckBox_cobro_VoBo.Checked = true;
                    else CheckBox_cobro_VoBo.Checked = false;
                }
                if (!String.IsNullOrEmpty(_dataRow["cobro_numero"].ToString())) this.TextBox_cobro_numero.Text = _dataRow["cobro_numero"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["cobro_valor"].ToString())) this.TextBox_cobro_valor.Text = _dataRow["cobro_valor"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["cobro_notas"].ToString())) this.TextBox_cobro_notas.Text = _dataRow["cobro_notas"].ToString();


                if (!String.IsNullOrEmpty(_dataRow["pago_fecha_radicacion"].ToString())) this.TextBox_pago_fecha_radicacion.Text = _dataRow["pago_fecha_radicacion"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["pago_fecha_seguimiento"].ToString())) this.TextBox_pago_fecha_seguimiento.Text = _dataRow["pago_fecha_seguimiento"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["pago_vobo"].ToString())) 
                {
                    if(_dataRow["pago_vobo"].ToString().Equals("S")) CheckBox_pago_VoBo.Checked = true;
                    else CheckBox_pago_VoBo.Checked = false;
                }
                if (!String.IsNullOrEmpty(_dataRow["pago_numero"].ToString())) this.TextBox_pago_numero.Text = _dataRow["pago_numero"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["pago_valor"].ToString())) this.TextBox_pago_valor.Text = _dataRow["pago_valor"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["pago_notas"].ToString())) this.TextBox_pago_notas.Text = _dataRow["pago_notas"].ToString();


                if (!String.IsNullOrEmpty(_dataRow["objetada_fecha_radicacion"].ToString())) this.TextBox_objetada_fecha_radicacion.Text = _dataRow["objetada_fecha_radicacion"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["objetada_fecha_seguimiento"].ToString())) this.TextBox_objetada_fecha_seguimiento.Text = _dataRow["objetada_fecha_seguimiento"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["objetada_vobo"].ToString())) 
                {
                    if (_dataRow["objetada_vobo"].ToString().Equals("S")) CheckBox_objetada_VoBo.Checked = true;
                    else CheckBox_objetada_VoBo.Checked = false;
                }
                if (!String.IsNullOrEmpty(_dataRow["objetada_numero"].ToString())) this.TextBox_objetada_numero.Text = _dataRow["objetada_numero"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["objetada_valor"].ToString())) this.TextBox_objetada_valor.Text = _dataRow["objetada_valor"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["objetada_notas"].ToString())) this.TextBox_objetada_notas.Text = _dataRow["objetada_notas"].ToString();

                if (!String.IsNullOrEmpty(_dataRow["negada_fecha_radicacion"].ToString())) this.TextBox_negada_fecha_radicacion.Text = _dataRow["negada_fecha_radicacion"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["negada_fecha_seguimiento"].ToString())) this.TextBox_negada_fecha_seguimiento.Text = _dataRow["negada_fecha_seguimiento"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["negada_vobo"].ToString())) 
                {
                    if(_dataRow["negada_vobo"].ToString().Equals("S")) CheckBox_negada_VoBo.Checked = true;
                    else CheckBox_negada_VoBo.Checked = false;
                }
                if (!String.IsNullOrEmpty(_dataRow["negada_numero"].ToString())) this.TextBox_negada_numero.Text = _dataRow["negada_numero"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["negada_valor"].ToString())) this.TextBox_negada_valor.Text = _dataRow["negada_valor"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["negada_notas"].ToString())) this.TextBox_negada_notas.Text = _dataRow["negada_notas"].ToString();

                if (!String.IsNullOrEmpty(_dataRow["fch_ini_nom"].ToString())) this.TextBox_fecha_paga_nomina_desde.Text = _dataRow["fch_ini_nom"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["fch_ter_nom"].ToString())) this.TextBox_fecha_paga_nomina_hasta.Text = _dataRow["fch_ter_nom"].ToString();

                if (DBNull.Value.Equals(_dataRow["ARCHIVO"]) == true)
                {
                    HyperLink_ARCHIVO.Text = "Sin Archivo";
                    HyperLink_ARCHIVO.Enabled = false;
                }
                else
                {
                    tools _tools = new tools();
                    SecureQueryString QueryStringSeguro;
                    QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
                    QueryStringSeguro["id_incapacidad"] = _dataRow["REGISTRO"].ToString();
                    HyperLink_ARCHIVO.NavigateUrl = "~/contratacion/VisorDocumentosIncapacidad.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
                    HyperLink_ARCHIVO.Enabled = true;
                }
            }
        }
        dataTable.Dispose();
    }

    private void Cargar(String codigo)
    {
        diagnostico _diagnostico = new diagnostico(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable _dataTable = _diagnostico.ObtenerPorCodigo(codigo);

        if (_dataTable.Rows.Count > 0)
        {
            Cargar(this.DropDownList_DSC_DIAG, _dataTable, "ID_DIAGNOSTICO");
            try
            {
                DropDownList_DSC_DIAG.SelectedValue = codigo.ToUpper();
            }
            catch
            {
                DropDownList_DSC_DIAG.ClearSelection();
            }
        }
        else
        {
            DropDownList_DSC_DIAG.ClearSelection();
        }

        _dataTable.Dispose();
    }

    private void Cargar(GridView gridView)
    {
        if (gridView.Rows.Count > 0)
        {
            DataTable dataTable;
            DataRow dataRow;
            if (!String.IsNullOrEmpty(this.GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["NUM_CONTRATO"].ToString())) this.TextBox_NUM_CONTRATO.Text = this.GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["NUM_CONTRATO"].ToString();
            if (!String.IsNullOrEmpty(this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[2].Text))
            {
                this.TextBox_ID_EMPLEADO.Text = this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[2].Text;
                eps Eps = new eps(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                dataTable = Eps.ObtenerPorEPSPorIdEmpleado(Convert.ToDecimal(TextBox_ID_EMPLEADO.Text));
                if (!dataTable.Rows.Count.Equals(0))
                {
                    dataRow = dataTable.Rows[0];
                    if (!string.IsNullOrEmpty(dataRow["NOM_ENTIDAD"].ToString())) TextBox_EPS.Text = dataRow["NOM_ENTIDAD"].ToString();
                }

                arp Arp = new arp(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                dataTable = Arp.ObtenerPorARLPorIdEmpleado(Convert.ToDecimal(TextBox_ID_EMPLEADO.Text));
                if (!dataTable.Rows.Count.Equals(0))
                {
                    dataRow = dataTable.Rows[0];
                    if (!string.IsNullOrEmpty(dataRow["NOM_ENTIDAD"].ToString())) TextBox_ARL.Text = dataRow["NOM_ENTIDAD"].ToString();
                }
            }
            if (!String.IsNullOrEmpty(this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[3].Text)) this.TextBox_fecha_ingreso.Text = this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[3].Text;
            if (!String.IsNullOrEmpty(this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[4].Text)) this.TextBox_fecha_retiro.Text = HttpUtility.HtmlDecode(this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[4].Text);
            if (!String.IsNullOrEmpty(this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[6].Text)) this.TextBox_ACTIVO.Text = this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[7].Text;
            if (!String.IsNullOrEmpty(this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[7].Text)) this.TextBox_APELLIDOS.Text = HttpUtility.HtmlDecode(this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[8].Text);
            if (!String.IsNullOrEmpty(this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[8].Text)) this.TextBox_NOMBRES.Text = HttpUtility.HtmlDecode(this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[9].Text);
            if (!String.IsNullOrEmpty(this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[9].Text)) this.TextBox_NUM_DOC_IDENTIDAD.Text = this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[10].Text;
            if (!String.IsNullOrEmpty(this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[10].Text)) this.TextBox_RAZ_SOCIAL.Text = HttpUtility.HtmlDecode(this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[11].Text);

        }
    }

    private void Bloquear(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                this.TextBox_DIAS_INCAP.Enabled = false;
                this.TextBox_VALOR_LIQUIDADO_NOMINA.Enabled = false;
                break;
            case Acciones.Guarda:
                this.Panel_FORMULARIO.Enabled = false;
                break;
            case Acciones.Visualiza:
                this.Panel_FORMULARIO.Enabled = false;
                break;
        }
    }

    private void Desbloquear(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Adiciona:
                this.Panel_FORMULARIO.Enabled = true;
                break;
            case Acciones.Modifica:
                this.Panel_FORMULARIO.Enabled = true;
                break;
        }
    }

    protected void Buscar()
    {
        Ocultar();
        registroContrato _contrato = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable _dataTable = new DataTable();
        switch (this.DropDownList_BUSCAR.SelectedValue)
        {
            case "NUMERO_DOCUMENTO":
                _dataTable = _contrato.ObtenerPorNumeroIdentificacion(this.TextBox_BUSCAR.Text);
                break;

            case "NOMBRE":
                _dataTable = _contrato.ObtenerPorNombre(this.TextBox_BUSCAR.Text);
                break;
        }

        if (_dataTable.Rows.Count > 0)
        {
            GridView_RESULTADOS_BUSQUEDA.DataSource = _dataTable;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
            Mostrar(Acciones.BusquedaEncontro);
        }
        else
        {
            if (!String.IsNullOrEmpty(_contrato.MensajeError)) Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Error: Consulte con el Administrador: " + _contrato.MensajeError, Proceso.Error);
            else Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "ADVERTENCIA: No se encontró información para " + this.DropDownList_BUSCAR.SelectedItem + " : " + this.TextBox_BUSCAR.Text + "<br />" 
                + "Causa: 1. La información ingresada no es correcta." + "<br />" 
                + "Causa: 2. No tiene contrato.", Proceso.Correcto);
            Mostrar(Acciones.BusquedaNoEncontro);
        }
        _dataTable.Dispose();
    }

    private void Limpiar()
    {
        TextBox_FCH_CRE.Text = String.Empty;
        TextBox_HOR_CRE.Text = String.Empty;
        TextBox_USU_CRE.Text = String.Empty;
        TextBox_FCH_MOD.Text = String.Empty;
        TextBox_HOR_MOD.Text = String.Empty;
        TextBox_USU_MOD.Text = String.Empty;
        TextBox_ID.Text = String.Empty;
        TextBox_FCH_INI_REAL.Text = String.Empty;
        TextBox_FCH_TER_REAL.Text = String.Empty;
        TextBox_DIAS_INCAP.Text = String.Empty;
        TextBox_VAL_RECONOCIDO.Text = String.Empty;
        TextBox_NUM_AUTORIZA.Text = String.Empty;
        TextBox_FECHA.Text = String.Empty;
        TextBox_OBS_REG.Text = String.Empty;
        TextBox_BUSCAR.Text = String.Empty;
        TextBox_VALOR_LIQUIDADO_NOMINA.Text = String.Empty;
        TextBox_dias_pagados.Text = String.Empty;
        TextBox_dias_pendientes.Text = String.Empty;

        TextBox_transcripcion_fecha_radicacion.Text = String.Empty;
        TextBox_transcripcion_fecha_seguimiento.Text = String.Empty;
        TextBox_transcripcion_numero.Text = String.Empty;
        TextBox_transcripcion_valor.Text = String.Empty;
        TextBox_transcripcion_notas.Text = String.Empty;
        TextBox_liquidacion_fecha_radicacion.Text = String.Empty;
        TextBox_liquidacion_fecha_seguimiento.Text = String.Empty;
        TextBox_liquidacion_numero.Text = String.Empty;
        TextBox_liquidacion_valor.Text = String.Empty;
        TextBox_liquidacion_notas.Text = String.Empty;
        TextBox_reliquidacion_fecha_radicacion.Text = String.Empty;
        TextBox_reliquidacion_fecha_seguimiento.Text = String.Empty;
        TextBox_reliquidacion_numero.Text = String.Empty;
        TextBox_reliquidacion_valor.Text = String.Empty;
        TextBox_reliquidacion_notas.Text = String.Empty;
        TextBox_cobro_fecha_radicacion.Text = String.Empty;
        TextBox_cobro_fecha_seguimiento.Text = String.Empty;
        TextBox_cobro_numero.Text = String.Empty;
        TextBox_cobro_valor.Text = String.Empty;
        TextBox_cobro_notas.Text = String.Empty;
        TextBox_pago_fecha_radicacion.Text = String.Empty;
        TextBox_pago_fecha_seguimiento.Text = String.Empty;
        TextBox_pago_numero.Text = String.Empty;
        TextBox_pago_valor.Text = String.Empty;
        TextBox_pago_notas.Text = String.Empty;
        TextBox_objetada_fecha_radicacion.Text = String.Empty;
        TextBox_objetada_fecha_seguimiento.Text = String.Empty;
        TextBox_objetada_numero.Text = String.Empty;
        TextBox_objetada_valor.Text = String.Empty;
        TextBox_objetada_notas.Text = String.Empty;
        TextBox_negada_fecha_radicacion.Text = String.Empty;
        TextBox_negada_fecha_seguimiento.Text = String.Empty;
        TextBox_negada_numero.Text = String.Empty;
        TextBox_negada_valor.Text = String.Empty;
        TextBox_negada_notas.Text = String.Empty;

        CheckBox_transcripcion_VoBo.Text = String.Empty;
        CheckBox_liquidacion_VoBo.Text = String.Empty;
        CheckBox_reliquidacion_VoBo.Text = String.Empty;
        CheckBox_cobro_VoBo.Text = String.Empty;
        CheckBox_pago_VoBo.Text = String.Empty;
        CheckBox_objetada_VoBo.Text = String.Empty;
        CheckBox_negada_VoBo.Text = String.Empty;

        DropDownList_INC_CARENCIA.Items.Clear();
        DropDownList_TIPO_INCA.Items.Clear();
        DropDownList_SEVERO.Items.Clear();
        DropDownList_CLASE_INCA.Items.Clear();
        DropDownList_PRORROGA.Items.Clear();
        DropDownList_DSC_DIAG.Items.Clear();
        DropDownList_BUSCAR.Items.Clear();
        DropDownList_estado.Items.Clear();
        DropDownList_estado_tramite.Items.Clear();
        DropDownList_tramitada_por.Items.Clear();
    }

    private void Guardar()
    {
        tools _tools = new tools();
        Byte[] archivo = null;
        Int32 archivo_tamaño = 0;
        String archivo_extension = null;
        String archivo_tipo = null;
        DateTime fecha = new DateTime();

        incapacidad _incapacidad = new incapacidad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        Decimal VAL_RECONOCIDO = !String.IsNullOrEmpty(TextBox_VAL_RECONOCIDO.Text) ? Convert.ToDecimal(TextBox_VAL_RECONOCIDO.Text) : 0;
        try
        {
            if (FileUpload_archivo.HasFile == true)
            {
                using (BinaryReader reader = new BinaryReader(FileUpload_archivo.PostedFile.InputStream))
                {
                    archivo = reader.ReadBytes(FileUpload_archivo.PostedFile.ContentLength);
                    archivo_tamaño = FileUpload_archivo.PostedFile.ContentLength;
                    archivo_tipo = FileUpload_archivo.PostedFile.ContentType;
                    archivo_extension = _tools.obtenerExtensionArchivo(FileUpload_archivo.PostedFile.FileName);
                }
            }

            Decimal ID = _incapacidad.Adicionar(Convert.ToDecimal(TextBox_ID_EMPLEADO.Text), Convert.ToDateTime(TextBox_FECHA.Text), DropDownList_TIPO_INCA.SelectedValue,
                DropDownList_CLASE_INCA.SelectedValue, DropDownList_PRORROGA.SelectedValue, DropDownList_SEVERO.SelectedValue,
                TextBox_OBS_REG.Text, VAL_RECONOCIDO, TextBox_NUM_AUTORIZA.Text, Convert.ToDateTime(TextBox_FCH_INI_REAL.Text), Convert.ToDateTime(TextBox_FCH_TER_REAL.Text),
                DropDownList_DSC_DIAG.SelectedValue, Convert.ToInt32(TextBox_DIAS_INCAP.Text), DropDownList_INC_CARENCIA.SelectedValue, Convert.ToDecimal(DropDownList_ID_CONCEPTO.SelectedValue),
                TextBox_transcripcion_fecha_radicacion.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_transcripcion_fecha_radicacion.Text),
                TextBox_transcripcion_fecha_seguimiento.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_transcripcion_fecha_seguimiento.Text),
                TextBox_transcripcion_numero.Text,
                TextBox_transcripcion_valor.Text == string.Empty ? 0 : Convert.ToDecimal(TextBox_transcripcion_valor.Text),
                TextBox_transcripcion_notas.Text,
                CheckBox_transcripcion_VoBo.Checked,
                TextBox_liquidacion_fecha_radicacion.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_liquidacion_fecha_radicacion.Text),
                TextBox_liquidacion_fecha_seguimiento.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_liquidacion_fecha_seguimiento.Text),
                TextBox_liquidacion_numero.Text,
                TextBox_liquidacion_valor.Text == string.Empty ? 0 : Convert.ToDecimal(TextBox_liquidacion_valor.Text),
                TextBox_liquidacion_notas.Text,
                CheckBox_liquidacion_VoBo.Checked,
                TextBox_reliquidacion_fecha_radicacion.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_reliquidacion_fecha_radicacion.Text),
                TextBox_reliquidacion_fecha_seguimiento.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_reliquidacion_fecha_seguimiento.Text),
                TextBox_reliquidacion_numero.Text,
                TextBox_reliquidacion_valor.Text == string.Empty ? 0 : Convert.ToDecimal(TextBox_reliquidacion_valor.Text),
                TextBox_reliquidacion_notas.Text,
                CheckBox_reliquidacion_VoBo.Checked,
                TextBox_cobro_fecha_radicacion.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_cobro_fecha_radicacion.Text),
                TextBox_cobro_fecha_seguimiento.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_cobro_fecha_seguimiento.Text),
                TextBox_cobro_numero.Text,
                TextBox_cobro_valor.Text == string.Empty ? 0 : Convert.ToDecimal(TextBox_cobro_valor.Text),
                TextBox_cobro_notas.Text,
                CheckBox_cobro_VoBo.Checked,
                TextBox_pago_fecha_radicacion.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_pago_fecha_radicacion.Text),
                TextBox_pago_fecha_seguimiento.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_pago_fecha_seguimiento.Text),
                TextBox_pago_numero.Text,
                TextBox_pago_valor.Text == string.Empty ? 0 : Convert.ToDecimal(TextBox_pago_valor.Text),
                TextBox_pago_notas.Text,
                CheckBox_pago_VoBo.Checked,
                TextBox_objetada_fecha_radicacion.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_objetada_fecha_radicacion.Text),
                TextBox_objetada_fecha_seguimiento.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_objetada_fecha_seguimiento.Text),
                TextBox_objetada_numero.Text,
                TextBox_objetada_valor.Text == string.Empty ? 0 : Convert.ToDecimal(TextBox_objetada_valor.Text),
                TextBox_objetada_notas.Text,
                CheckBox_objetada_VoBo.Checked,
                TextBox_negada_fecha_radicacion.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_negada_fecha_radicacion.Text),
                TextBox_negada_fecha_seguimiento.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_negada_fecha_seguimiento.Text),
                TextBox_negada_numero.Text,
                TextBox_negada_valor.Text == string.Empty ? 0 : Convert.ToDecimal(TextBox_negada_valor.Text),
                TextBox_negada_notas.Text,
                CheckBox_negada_VoBo.Checked,
                DropDownList_estado.SelectedValue,
                DropDownList_estado_tramite.SelectedValue,
                DropDownList_tramitada_por.SelectedValue,
                archivo,
                archivo_tamaño,
                archivo_extension,
                archivo_tipo,
                TextBox_fecha_paga_nomina_desde.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_fecha_paga_nomina_desde.Text),
                TextBox_fecha_paga_nomina_hasta.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_fecha_paga_nomina_hasta.Text)
                );

            if (ID != 0)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La incapacidad fue registrada correctamente y se le asignó el ID: " + ID.ToString() + ".", Proceso.Correcto);
                TextBox_ID.Text = ID.ToString();
                Ocultar();
                Mostrar(Acciones.Guarda);
                Bloquear(Acciones.Guarda);
            }
            else Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Error al registrar la incapacidad. ", Proceso.Error); 
        }
        catch(Exception e)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, e.Message, Proceso.Error);
        }
    }

    private void Modificar()
    {
        tools _tools = new tools();
        Byte[] archivo = null;
        Int32 archivo_tamaño = 0;
        String archivo_extension = null;
        String archivo_tipo = null;
        DateTime fecha = new DateTime();
        bool actualizado = false;

        incapacidad _incapacidad = new incapacidad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        Decimal VAL_RECONOCIDO = !String.IsNullOrEmpty(TextBox_VAL_RECONOCIDO.Text) ? Convert.ToDecimal(TextBox_VAL_RECONOCIDO.Text) : 0;

        try
        {
            if (FileUpload_archivo.HasFile == true)
            {
                using (BinaryReader reader = new BinaryReader(FileUpload_archivo.PostedFile.InputStream))
                {
                    archivo = reader.ReadBytes(FileUpload_archivo.PostedFile.ContentLength);
                    archivo_tamaño = FileUpload_archivo.PostedFile.ContentLength;
                    archivo_tipo = FileUpload_archivo.PostedFile.ContentType;
                    archivo_extension = _tools.obtenerExtensionArchivo(FileUpload_archivo.PostedFile.FileName);
                }
            }

            actualizado = _incapacidad.Actualizar(Convert.ToDecimal(TextBox_ID.Text), 
                Convert.ToDateTime(TextBox_FECHA.Text), 
                DropDownList_TIPO_INCA.SelectedValue,
                DropDownList_CLASE_INCA.SelectedValue, 
                DropDownList_PRORROGA.SelectedValue, 
                DropDownList_SEVERO.SelectedValue,
                TextBox_OBS_REG.Text, VAL_RECONOCIDO, 
                TextBox_NUM_AUTORIZA.Text, 
                Convert.ToDateTime(TextBox_FCH_INI_REAL.Text), 
                Convert.ToDateTime(TextBox_FCH_TER_REAL.Text),
                DropDownList_DSC_DIAG.SelectedValue, 
                Convert.ToInt32(TextBox_DIAS_INCAP.Text), 
                DropDownList_INC_CARENCIA.SelectedValue, 
                Convert.ToDecimal(DropDownList_ID_CONCEPTO.SelectedValue),
                TextBox_transcripcion_fecha_radicacion.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_transcripcion_fecha_radicacion.Text),
                TextBox_transcripcion_fecha_seguimiento.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_transcripcion_fecha_seguimiento.Text),
                TextBox_transcripcion_numero.Text,
                TextBox_transcripcion_valor.Text == string.Empty ? 0 : Convert.ToDecimal(TextBox_transcripcion_valor.Text),
                TextBox_transcripcion_notas.Text,
                CheckBox_transcripcion_VoBo.Checked,
                TextBox_liquidacion_fecha_radicacion.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_liquidacion_fecha_radicacion.Text),
                TextBox_liquidacion_fecha_seguimiento.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_liquidacion_fecha_seguimiento.Text),
                TextBox_liquidacion_numero.Text,
                TextBox_liquidacion_valor.Text == string.Empty ? 0 : Convert.ToDecimal(TextBox_liquidacion_valor.Text),
                TextBox_liquidacion_notas.Text,
                CheckBox_liquidacion_VoBo.Checked,
                TextBox_reliquidacion_fecha_radicacion.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_reliquidacion_fecha_radicacion.Text),
                TextBox_reliquidacion_fecha_seguimiento.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_reliquidacion_fecha_seguimiento.Text),
                TextBox_reliquidacion_numero.Text,
                TextBox_reliquidacion_valor.Text == string.Empty ? 0 : Convert.ToDecimal(TextBox_reliquidacion_valor.Text),
                TextBox_reliquidacion_notas.Text,
                CheckBox_reliquidacion_VoBo.Checked,
                TextBox_cobro_fecha_radicacion.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_cobro_fecha_radicacion.Text),
                TextBox_cobro_fecha_seguimiento.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_cobro_fecha_seguimiento.Text),
                TextBox_cobro_numero.Text,
                TextBox_cobro_valor.Text == string.Empty ? 0 : Convert.ToDecimal(TextBox_cobro_valor.Text),
                TextBox_cobro_notas.Text,
                CheckBox_cobro_VoBo.Checked,
                TextBox_pago_fecha_radicacion.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_pago_fecha_radicacion.Text),
                TextBox_pago_fecha_seguimiento.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_pago_fecha_seguimiento.Text),
                TextBox_pago_numero.Text,
                TextBox_pago_valor.Text == string.Empty ? 0 : Convert.ToDecimal(TextBox_pago_valor.Text),
                TextBox_pago_notas.Text,
                CheckBox_pago_VoBo.Checked,
                TextBox_objetada_fecha_radicacion.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_objetada_fecha_radicacion.Text),
                TextBox_objetada_fecha_seguimiento.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_objetada_fecha_seguimiento.Text),
                TextBox_objetada_numero.Text,
                TextBox_objetada_valor.Text == string.Empty ? 0 : Convert.ToDecimal(TextBox_objetada_valor.Text),
                TextBox_objetada_notas.Text,
                CheckBox_objetada_VoBo.Checked,
                TextBox_negada_fecha_radicacion.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_negada_fecha_radicacion.Text),
                TextBox_negada_fecha_seguimiento.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_negada_fecha_seguimiento.Text),
                TextBox_negada_numero.Text,
                TextBox_negada_valor.Text == string.Empty ? 0 : Convert.ToDecimal(TextBox_negada_valor.Text),
                TextBox_negada_notas.Text,
                CheckBox_negada_VoBo.Checked,
                DropDownList_estado.SelectedValue,
                DropDownList_estado_tramite.SelectedValue,
                DropDownList_tramitada_por.SelectedValue,
                archivo,
                archivo_tamaño,
                archivo_extension,
                archivo_tipo,
                TextBox_fecha_paga_nomina_desde.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_fecha_paga_nomina_desde.Text),
                TextBox_fecha_paga_nomina_hasta.Text == string.Empty ? fecha : Convert.ToDateTime(TextBox_fecha_paga_nomina_hasta.Text)
                );

            if (actualizado)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La incapacidad fue actualizada correctamente. " , Proceso.Correcto);
                TextBox_ID.Text = ID.ToString();
                Ocultar();
                Mostrar(Acciones.Guarda);
                Bloquear(Acciones.Guarda);
                
            }
            else Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La incapacidad no fue actualizada. ", Proceso.Error);
        }
        catch (Exception e)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, e.Message, Proceso.Error);
        }
    }

    private void configurarCaracteresAceptadosBusqueda(Boolean letras, Boolean numeros)
    {
        FilteredTextBoxExtender_TextBox_BUSCAR_Numbers.Enabled = numeros;
        FilteredTextBoxExtender_TextBox_BUSCAR_Letras.Enabled = letras;
    }

    private void Configurar()
    {
        Page.Header.Title = "INCAPACIDADES";
        Configurar(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    private String ObtenerDiasIncapacidad(String fechaInicial, String fechaFinal)
    {
        DateTime fecha;
        TimeSpan timeSpan;

        if ((fechaInicial == String.Empty)
            && (fechaFinal == String.Empty)) return String.Empty;

        if (!DateTime.TryParse(fechaInicial, out fecha)) return String.Empty;
        if (!DateTime.TryParse(fechaFinal, out fecha)) return String.Empty;

        if (Convert.ToDateTime(fechaInicial) > Convert.ToDateTime(fechaFinal)) return String.Empty;

        timeSpan = Convert.ToDateTime(fechaFinal) - Convert.ToDateTime(fechaInicial);
        if (Convert.ToDateTime(fechaFinal) == Convert.ToDateTime(fechaInicial)) return (timeSpan.Days - 1).ToString();
        else return (timeSpan.Days + 1).ToString();
    }

    private void Ocultar(Panel panel_fondo, Panel panel_mensaj)
    {
        panel_fondo.Style.Add("display", "none");
        panel_mensaj.Style.Add("display", "none");

        panel_fondo.Visible = false;
        panel_mensaj.Visible = false;

    }

    private void Configurar(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_mensaje.CssClass = "panel_popup";
    }

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

    private int Totalizar(DataTable dataTable)
    {
        int total_dias_incapacidad=0;
        foreach (DataRow dataRow in dataTable.Rows)
        {
            if (!string.IsNullOrEmpty(dataRow["DIAS_INCAP"].ToString())) total_dias_incapacidad+= Convert.ToInt16(dataRow["DIAS_INCAP"].ToString());
        }
        return total_dias_incapacidad;
    }

    private void Alertar(TextBox textBox)
    {
        if (Convert.ToInt16(textBox.Text) < 180) textBox.BackColor = System.Drawing.Color.Yellow;
        if (Convert.ToInt16(textBox.Text) < 180) textBox.BackColor = System.Drawing.Color.Yellow;
        if ((Convert.ToInt16(textBox.Text) > 180) && (Convert.ToInt16(textBox.Text) < 560)) textBox.BackColor = System.Drawing.Color.Blue;
        if (Convert.ToInt16(textBox.Text) > 560)  textBox.BackColor = System.Drawing.Color.Red;

    }

    private bool Validar()
    {
        bool validado = true;

        if ((Convert.ToInt16(TextBox_DIAS_INCAP.Text) > 30) && (!DropDownList_CLASE_INCA.SelectedValue.Equals("MT")))
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se permite registrar incapaciades con mas de 30 días: " , Proceso.Error);
            validado = false;
        }

        if((DropDownList_CLASE_INCA.SelectedValue.Equals("EG") ||
            DropDownList_CLASE_INCA.SelectedValue.Equals("MT") ||
                DropDownList_CLASE_INCA.SelectedValue.Equals("PT")) && !DropDownList_tramitada_por.SelectedValue.Equals("1"))
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La clase de incapacidad seleccionada solo puede ser tramitada por EPS: " , Proceso.Error);
            validado = false;
        }

        if((DropDownList_CLASE_INCA.SelectedValue.Equals("AL") ||
            DropDownList_CLASE_INCA.SelectedValue.Equals("EP") ) && !DropDownList_tramitada_por.SelectedValue.Equals("2"))
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La clase de incapacidad seleccionada solo puede ser tramitada por ARL: " , Proceso.Error);
            validado = false;
        }

        return validado;
    }

    private void Inactivar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Modifica:
                RequiredFieldValidator_FileUpload_archivo.Enabled = false;
                break;
        }
    }

    private void Activar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Adiciona:
                RequiredFieldValidator_FileUpload_archivo.Enabled = true;
                break;
        }
    }

    #endregion metodos

    #region eventos
    protected void Button_NUEVO_Click(object sender, EventArgs e)
    {
        Limpiar();
        Ocultar();
        Cargar(Acciones.Inicio);
        Mostrar(Acciones.Adiciona);
        Desbloquear(Acciones.Adiciona);
        Activar(Acciones.Adiciona);
    }

    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar();
        Mostrar(Acciones.Modifica);
        Desbloquear(Acciones.Modifica);
        Inactivar(Acciones.Modifica);
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        if (Validar())
        {
            if (String.IsNullOrEmpty(this.TextBox_ID.Text)) Guardar();
            else Modificar();
        }
    }

    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        Limpiar();
        Ocultar();
        Cargar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
    }

    protected void DropDownList_BUSCAR_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        Buscar();
    }

    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;
        Buscar();
    }

    protected void GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged(object sender, EventArgs e)
    {
        Ocultar();
        incapacidad _incapacidad = new incapacidad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable _dataTable = new DataTable();
        _dataTable = _incapacidad.ObtenerPorIdContrato(Convert.ToDecimal(this.GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["NUM_CONTRATO"].ToString()));

        if (_dataTable.Rows.Count > 0)
        {
            TextBox_total_dias_incapacidad.Text = Totalizar(_dataTable).ToString();
            Alertar(TextBox_total_dias_incapacidad);
            GridView_RESULTADOS_BUSQUEDA_INCAPACIDADES.DataSource = _dataTable;
            GridView_RESULTADOS_BUSQUEDA_INCAPACIDADES.DataBind();
            Cargar(this.GridView_RESULTADOS_BUSQUEDA);
            Mostrar(Acciones.BusquedaEncontroIncapacidades);
        }
        else
        {
            if (!String.IsNullOrEmpty(_incapacidad.MensajeError)) Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Error: Consulte con el Administrador: " + _incapacidad.MensajeError, Proceso.Error);
            else
            {
                LimpiarDatosINcapacidad();

                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El contrato No. " + this.GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["NUM_CONTRATO"].ToString() + ", no cuenta con incapacidades registradas", Proceso.Correcto);
                Cargar(this.GridView_RESULTADOS_BUSQUEDA);
                Mostrar(Acciones.BusquedaNoEncontroIncapacidades);
            }
        }
        _dataTable.Dispose();
    }

    protected void GridView_RESULTADOS_BUSQUEDA_INCAPACIDADES_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(this.GridView_RESULTADOS_BUSQUEDA_INCAPACIDADES.SelectedDataKey["REGISTRO"].ToString()))
        {
            incapacidad _incapacidad = new incapacidad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            Cargar(_incapacidad.ObtenerPorIdIncapacidad(Convert.ToDecimal(this.GridView_RESULTADOS_BUSQUEDA_INCAPACIDADES.SelectedDataKey["REGISTRO"].ToString())));
            incapacidad Incapacidad = new incapacidad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            GridView_pagos_nomina.DataSource = Incapacidad.ObtenerPagosDeNominaPorIdEmpleado(Convert.ToDecimal(TextBox_ID_EMPLEADO.Text));
            GridView_pagos_nomina.DataBind();
        }

        Ocultar();
        Mostrar(Acciones.Visualiza);
        Bloquear(Acciones.Visualiza);
    }

    protected void TextBox_FCH_INI_REAL_TextChanged(object sender, EventArgs e)
    {
        this.TextBox_DIAS_INCAP.Text = ObtenerDiasIncapacidad(this.TextBox_FCH_INI_REAL.Text,
            this.TextBox_FCH_TER_REAL.Text);
    }

    protected void TextBox_FCH_TER_REAL_TextChanged(object sender, EventArgs e)
    {
        this.TextBox_DIAS_INCAP.Text = ObtenerDiasIncapacidad(this.TextBox_FCH_INI_REAL.Text,
            this.TextBox_FCH_TER_REAL.Text);
    }

    protected void DropDownList_CLASE_INCA_SelectedIndexChanged(object sender, EventArgs e)
    {
        parametroIncapacidad _parametroIncapacidad = new parametroIncapacidad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable dataTable = _parametroIncapacidad.ObtenerPorClaseIncapacidad(DropDownList_CLASE_INCA.SelectedValue);
        Cargar(DropDownList_ID_CONCEPTO, dataTable, "ID_CONCEPTO");
    }

    protected void DropDownList_BUSCAR_SelectedIndexChanged1(object sender, EventArgs e)
    {
        switch (DropDownList_BUSCAR.SelectedValue)
        {
            case "":
                configurarCaracteresAceptadosBusqueda(false, false);
                break;
            case "NUMERO_DOCUMENTO":
                configurarCaracteresAceptadosBusqueda(false, true);
                this.Label_INFO_ADICIONAL_MODULO.Text = "seleccion documento";
                break;
            case "NOMBRE":
                configurarCaracteresAceptadosBusqueda(true, false);
                this.Label_INFO_ADICIONAL_MODULO.Text = "seleccion nombre";
                break;
        }
    }

    protected void Button_BUSCADOR_DIAGNOSTICO_Click(object sender, EventArgs e)
    {
        diagnostico _diagnostico = new diagnostico(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable _dataTable = _diagnostico.ObtenerPorNombre(this.TextBox_BUSCADOR_DIAGNOSTICO.Text);
        Cargar(this.DropDownList_DSC_DIAG, _dataTable, "ID_DIAGNOSTICO");
        _dataTable.Dispose();
    }

    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {
        Ocultar(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }
    #endregion eventos
}