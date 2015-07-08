using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using Brainsbits.LLB.comercial;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.financiera;
using Brainsbits.LLB;

using TSHAK.Components;
using Brainsbits.LLB.seguridad;

public partial class _Default : System.Web.UI.Page
{
    private Int32 _proceso = 0;

    #region CARGAR DROPS Y GRIDS

    private void cargarGridInfoContactos()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        int PROCESO = Convert.ToInt32(QueryStringSeguro["proceso"]);
        String reg = QueryStringSeguro["reg"].ToString();
        contactos _contactos = new contactos(Session["idEmpresa"].ToString());

        tabla.proceso pr = (tabla.proceso)PROCESO;
        DataTable tablaContactosOriginal = _contactos.ObtenerContactosPorIdEmpresa(Convert.ToDecimal(reg), pr);
        
        if (_contactos.MensajeError != null)
        {
            configurarMensajes(true, System.Drawing.Color.Red);
            Label_MENSAJE.Text = _contactos.MensajeError;

            configurarBotonesDeAccion(true, false, false, false, false);

            Panel_RESULTADOS_GRID.Visible = false;

            Panel_FORMULARIO.Visible = false;
        }
        else
        {
            if (tablaContactosOriginal.Rows.Count > 0)
            {
                DataTable tablaContactos = new DataTable();

                tablaContactos.Columns.Add("Registro");
                tablaContactos.Columns.Add("Regional");
                tablaContactos.Columns.Add("Ciudad");
                tablaContactos.Columns.Add("Estado");
                tablaContactos.Columns.Add("Cargo");
                tablaContactos.Columns.Add("Nombre");
                tablaContactos.Columns.Add("Mail");
                tablaContactos.Columns.Add("Teléfono");
                tablaContactos.Columns.Add("Teléfono 2");
                tablaContactos.Columns.Add("Celular");
                
                DataRow filaInfoContrato;

                foreach (DataRow filaOriginal in tablaContactosOriginal.Rows)
                {
                    filaInfoContrato = tablaContactos.NewRow();
                    filaInfoContrato["Registro"] = filaOriginal["REGISTRO"].ToString();
                    filaInfoContrato["Cargo"] = filaOriginal["CONT_CARGO"].ToString();
                    filaInfoContrato["Nombre"] = filaOriginal["CONT_NOM"].ToString();
                    filaInfoContrato["Mail"] = filaOriginal["CONT_MAIL"].ToString();
                    filaInfoContrato["Teléfono"] = filaOriginal["CONT_TEL"].ToString();
                    filaInfoContrato["Teléfono 2"] = filaOriginal["CONT_TEL1"].ToString();
                    filaInfoContrato["Celular"] = filaOriginal["CONT_CELULAR"].ToString();
                    filaInfoContrato["Regional"] = filaOriginal["NOMBRE_REGIONAL"].ToString();
                    filaInfoContrato["Ciudad"] = filaOriginal["NOMBRE_CIUDAD"].ToString();
                    filaInfoContrato["Estado"] = filaOriginal["CONT_ESTADO"].ToString();

                    tablaContactos.Rows.Add(filaInfoContrato);
                }

                GridView_CONTACTOS.DataSource = tablaContactos;
                GridView_CONTACTOS.DataBind();
            }
            else
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = "ADVERTENCIA: El cliente no posee contáctos para este proceso.";

                configurarBotonesDeAccion(true, false, false, false, false);

                Panel_RESULTADOS_GRID.Visible = false;

                Panel_FORMULARIO.Visible = false;
            }
        }
    }

    private void cargar_DropDownList_REGIONAL()
    {
        DropDownList_REGIONAL.Items.Clear();

        regional _regional = new regional(Session["idEmpresa"].ToString());
        DataTable tablaRegionales = _regional.ObtenerTodasLasRegionales();

        ListItem item = new ListItem("Seleccione Regional", "");
        DropDownList_REGIONAL.Items.Add(item);

        foreach (DataRow fila in tablaRegionales.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_REGIONAL"].ToString());
            DropDownList_REGIONAL.Items.Add(item);
        }

        DropDownList_REGIONAL.DataBind();
    }

    private void cargar_DropDownList_CONT_DEPARTAMENTO(String ID_REGIONAL)
    {
        DropDownList_CONT_DEPARTAMENTO.Items.Clear();

        departamento _departamento = new departamento(Session["idEmpresa"].ToString());
        DataTable tablaDepartamentos = _departamento.ObtenerDepartamentosPorIdRegional(ID_REGIONAL);

        ListItem item = new ListItem("Seleccione Departamento", "");
        DropDownList_CONT_DEPARTAMENTO.Items.Add(item);

        foreach (DataRow fila in tablaDepartamentos.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_DEPARTAMENTO"].ToString());
            DropDownList_CONT_DEPARTAMENTO.Items.Add(item);
        }

        DropDownList_CONT_DEPARTAMENTO.DataBind();
    }

    private void cargar_DropDownList_CONT_CIUDAD(String idDepartamento, String ID_REGIONAL)
    {
        DropDownList_CONT_CIUDAD.Items.Clear();

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
        DataTable tablaCiudades = _ciudad.ObtenerCiudadesPorIdDepartamentoEIdRegional(ID_REGIONAL, idDepartamento);

        ListItem item = new ListItem("Seleccione Ciudad", "");
        DropDownList_CONT_CIUDAD.Items.Add(item);

        foreach (DataRow fila in tablaCiudades.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_CIUDAD"].ToString());
            DropDownList_CONT_CIUDAD.Items.Add(item);
        }

        DropDownList_CONT_CIUDAD.DataBind();
    }

    private void cargar_DropDownList_ESTADO()
    {
        DropDownList_ESTADO.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());
        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_ESTADO_CONTACTO);

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_ESTADO.Items.Add(item);
        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["DESCRIPCION"].ToString());
            DropDownList_ESTADO.Items.Add(item);
        }

        DropDownList_ESTADO.DataBind();
    }

    private void cargar_DropDownList_CONTACTOS_EXISTENTES()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_EMPRESA = Convert.ToInt32(QueryStringSeguro["reg"]);

        contactos _contactos = new contactos(Session["idEmpresa"].ToString());
        DataTable tablaContactosEmpresa = _contactos.ObtenerTodosContactosDeUnaEmpresa(ID_EMPRESA);

        ListItem item = new ListItem("Seleccione Contácto", "");
        DropDownList_CONTACTOS_EXISTENTES.Items.Add(item);

        foreach (DataRow fila in tablaContactosEmpresa.Rows)
        {
            item = new ListItem(fila["CONT_NOM"].ToString(), fila["REGISTRO"].ToString());
            DropDownList_CONTACTOS_EXISTENTES.Items.Add(item);
        }

        DropDownList_CONTACTOS_EXISTENTES.DataBind();
    }

    private void cargar_DropDownList_BANCOS()
    {
        DropDownList_BANCOS.Items.Clear();

        financiera _bancos = new financiera(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());  
        DataTable tablaBancos = _bancos.ObtenerBancos();

        ListItem item = new ListItem("Seleccione un Banco...", "");
        DropDownList_BANCOS.Items.Add(item);

        foreach (DataRow fila in tablaBancos.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID"].ToString());
            DropDownList_BANCOS.Items.Add(item);
        }

        DropDownList_BANCOS.DataBind();
    }

    private void cargar_DropDownList_CUENTAS(Int32 _banco)
    {
        DropDownList_CUENTAS.Items.Clear();

        financiera _cuentas = new financiera(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaCuentas = _cuentas.ObtenerCuentasPorBanco(_banco);

        ListItem item = new ListItem("Seleccione una Cuenta...", "");
        DropDownList_CUENTAS.Items.Add(item);

        foreach (DataRow fila in tablaCuentas.Rows)
        {
            item = new ListItem(fila["ALIAS"].ToString(), fila["ID_CUENTA"].ToString());
            DropDownList_CUENTAS.Items.Add(item);
        }

        DropDownList_CUENTAS.DataBind();
    }
    #endregion

    #region CONFIGURACION DE CONTROLES
    private void configurarBotonesDeAccion(Boolean bNuevo, Boolean bModificar, Boolean bGuardar, Boolean bCancelar, Boolean bVolver)
    {
        Button_NUEVO.Visible = bNuevo;
        Button_NUEVO.Enabled = bNuevo;

        Button_MODIFICAR.Visible = bModificar;
        Button_MODIFICAR.Enabled = bModificar;

        Button_GUARDAR.Visible = bGuardar;
        Button_GUARDAR.Enabled = bGuardar;

        Button_CANCELAR.Visible = bCancelar;
        Button_CANCELAR.Enabled = bCancelar;

        Button_LISTA_CONTACTOS.Visible = bVolver;
        Button_LISTA_CONTACTOS.Enabled = bVolver;
    }
    private void configurarInfoAdicionalModulo(Boolean ver, String texto)
    {
        Panel_INFO_ADICIONAL_MODULO.Visible = ver;
        Label_INFO_ADICIONAL_MODULO.Text = texto;
    }
    private void inhabilitar_DropDownList_CONT_CIUDAD()
    {
        DropDownList_CONT_CIUDAD.Enabled = false;
        DropDownList_CONT_CIUDAD.Items.Clear();
        ListItem item = new ListItem("Seleccione Ciudad", "");
        DropDownList_CONT_CIUDAD.Items.Add(item);
        DropDownList_CONT_CIUDAD.DataBind();
    }
    private void inhabilitar_DropDownList_CONT_DEPARTAMENTO()
    {
        DropDownList_CONT_DEPARTAMENTO.Enabled = false;
        DropDownList_CONT_DEPARTAMENTO.Items.Clear();
        ListItem item = new ListItem("Seleccione Departamento", "");
        DropDownList_CONT_DEPARTAMENTO.Items.Add(item);
        DropDownList_CONT_DEPARTAMENTO.DataBind();
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
    #endregion

    #region METODOS PARA OBTENER DATOS DE LA BD
    private DataRow obtenerDatosCiudadDepartamentoRegionalDelContacto(String idCiudad)
    {
        DataRow resultado = null;

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());

        DataTable tablaIdRegionalIdDepartamento = _ciudad.ObtenerIdRegionalIdDepartamentoConIdCiudad(idCiudad);

        if (tablaIdRegionalIdDepartamento.Rows.Count > 0)
        {
            resultado = tablaIdRegionalIdDepartamento.Rows[0];
        }

        return resultado;
    }
    #endregion

    #region METODOS QUE SE EJECUTAN AL PARGAR PAGINA
    private void iniciarControlesInicial()
    {
        configurarBotonesDeAccion(true, false, false, false, false);

        configurarMensajes(false, System.Drawing.Color.Green);

        Panel_FORMULARIO.Visible = false;

        cargarGridInfoContactos();


    }

    private void iniciarControlesNuevo()
    {
        configurarBotonesDeAccion(false, false, true, true, true);

        configurarMensajes(false, System.Drawing.Color.Green);

        Panel_RESULTADOS_GRID.Visible = false;

        Panel_FORMULARIO.Visible = true;
        Panel_FORMULARIO.Enabled = true;

        Panel_CONTACTOS_ACTUALES.Visible = true;
        cargar_DropDownList_CONTACTOS_EXISTENTES();

        Panel_CONTROL_REGISTRO.Visible = false;

        Panel_IDENTIFICADOR.Visible = false;

        cargar_DropDownList_REGIONAL();
        inhabilitar_DropDownList_CONT_DEPARTAMENTO();
        inhabilitar_DropDownList_CONT_CIUDAD();

        Panel_INMEDIATO.Visible = true;

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        if(_proceso == Convert.ToInt32(QueryStringSeguro["proceso"])) this.Panel_CARTERA.Visible = true;
        else this.Panel_CARTERA.Visible = false;
    }

    private void iniciarControlesCargar()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        contactos _contactos = new contactos(Session["idEmpresa"].ToString());

        Decimal REGISTRO = Convert.ToDecimal(QueryStringSeguro["contacto"]);
        DataTable tablainfoContacto = _contactos.ObtenerContactosPorRegistro(REGISTRO);

        if (_contactos.MensajeError == null)
        {
            if (tablainfoContacto.Rows.Count <= 0)
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = "ADVERTENCIA: No se encontró información del Contácto identificado con el número: " + REGISTRO.ToString();

                configurarBotonesDeAccion(true, false, false, false, true);

                Panel_RESULTADOS_GRID.Visible = false;

                Panel_FORMULARIO.Visible = false;
            }
            else
            {
                configurarBotonesDeAccion(true, true, false, false, true);

                Panel_RESULTADOS_GRID.Visible = false;

                configurarMensajes(false, System.Drawing.Color.Green);

                Panel_FORMULARIO.Visible = true;
                Panel_FORMULARIO.Enabled = false;

                DataRow filaInfoContacto = tablainfoContacto.Rows[0];

                Page.Header.Title += ": " + filaInfoContacto["CONT_NOM"].ToString();

                Panel_CONTROL_REGISTRO.Visible = true;
                Panel_CONTROL_REGISTRO.Enabled = false;
                TextBox_USU_CRE.Text = filaInfoContacto["USU_CRE"].ToString();
                try
                {
                    TextBox_FCH_CRE.Text = DateTime.Parse(filaInfoContacto["FCH_CRE"].ToString()).ToShortDateString();
                    TextBox_HOR_CRE.Text = DateTime.Parse(filaInfoContacto["FCH_CRE"].ToString()).ToShortTimeString();
                }
                catch
                {
                    TextBox_FCH_CRE.Text = "";
                    TextBox_HOR_CRE.Text = "";
                }
                TextBox_USU_MOD.Text = filaInfoContacto["USU_MOD"].ToString();
                try
                {
                    TextBox_FCH_MOD.Text = DateTime.Parse(filaInfoContacto["FCH_MOD"].ToString()).ToShortDateString();
                    TextBox_HOR_MOD.Text = DateTime.Parse(filaInfoContacto["FCH_MOD"].ToString()).ToShortTimeString();
                }
                catch
                {
                    TextBox_FCH_MOD.Text = "";
                    TextBox_HOR_MOD.Text = "";
                }

                TextBox_REGISTRO.Text = filaInfoContacto["REGISTRO"].ToString().Trim();

                Panel_IDENTIFICADOR.Visible = true;

                Panel_CONTACTOS_ACTUALES.Visible = false;

                cargar_DropDownList_ESTADO();
                DropDownList_ESTADO.SelectedValue = filaInfoContacto["CONT_ESTADO"].ToString().ToUpper().Trim();

                TextBox_CONT_NOM.Text = filaInfoContacto["CONT_NOM"].ToString().Trim();
                TextBox_CONT_CARGO.Text = filaInfoContacto["CONT_CARGO"].ToString().Trim();
                TextBox_CONT_MAIL.Text = filaInfoContacto["CONT_MAIL"].ToString().Trim();

                TextBox_CONT_TEL.Text = filaInfoContacto["CONT_TEL"].ToString().Trim();
                TextBox_CONT_TEL1.Text = filaInfoContacto["CONT_TEL1"].ToString().Trim();
                TextBox_CONT_CELULAR.Text = filaInfoContacto["CONT_CELULAR"].ToString().Trim();

                _contactos.MensajeError = null;
                DataRow filaInfoCiudadDepartamentoYRegional = obtenerDatosCiudadDepartamentoRegionalDelContacto(filaInfoContacto["CONT_CIUDAD"].ToString().Trim());
                if (filaInfoCiudadDepartamentoYRegional != null)
                {
                    cargar_DropDownList_REGIONAL();
                    DropDownList_REGIONAL.SelectedValue = filaInfoCiudadDepartamentoYRegional["ID_REGIONAL"].ToString().Trim();

                    cargar_DropDownList_CONT_DEPARTAMENTO(filaInfoCiudadDepartamentoYRegional["ID_REGIONAL"].ToString().Trim());
                    DropDownList_CONT_DEPARTAMENTO.SelectedValue = filaInfoCiudadDepartamentoYRegional["ID_DEPARTAMENTO"].ToString().Trim();

                    cargar_DropDownList_CONT_CIUDAD(filaInfoCiudadDepartamentoYRegional["ID_DEPARTAMENTO"].ToString().Trim(), filaInfoCiudadDepartamentoYRegional["ID_REGIONAL"].ToString().Trim());
                    DropDownList_CONT_CIUDAD.SelectedValue = filaInfoCiudadDepartamentoYRegional["ID_CIUDAD"].ToString().Trim();
                }

                Panel_INMEDIATO.Visible = false;


                if (_proceso == Convert.ToInt32(QueryStringSeguro["proceso"]))
                {
                    this.Panel_CARTERA.Visible = true;
                    this.TextBox_DIAS_PAGO.Text = filaInfoContacto["DIAS"].ToString().Trim();
                    if (!String.IsNullOrEmpty(filaInfoContacto["FORMA_PAGO"].ToString()))
                    {
                        this.DropDownList_FORMA_PAGO.SelectedIndex = DropDownList_FORMA_PAGO.Items.IndexOf(DropDownList_FORMA_PAGO.Items.FindByValue(filaInfoContacto["FORMA_PAGO"].ToString().Trim()));
                        if (DropDownList_FORMA_PAGO.SelectedItem.Value == "Consignacion" || DropDownList_FORMA_PAGO.SelectedItem.Value == "Transferencia")
                        {
                            cargar_DropDownList_BANCOS();
                            if (!String.IsNullOrEmpty(filaInfoContacto["BANCO"].ToString()))
                            {
                                this.DropDownList_BANCOS.SelectedIndex = DropDownList_BANCOS.Items.IndexOf(DropDownList_BANCOS.Items.FindByValue(filaInfoContacto["BANCO"].ToString().Trim()));
                                cargar_DropDownList_CUENTAS(Convert.ToInt32(DropDownList_BANCOS.SelectedItem.Value.ToString()));
                                if (!String.IsNullOrEmpty(filaInfoContacto["CUENTA"].ToString()))
                                {
                                    this.DropDownList_CUENTAS.SelectedIndex = DropDownList_CUENTAS.Items.IndexOf(DropDownList_CUENTAS.Items.FindByValue(filaInfoContacto["CUENTA"].ToString().Trim()));
                                }
                            }
                        }
                        else this.DropDownList_BANCOS.Items.Clear();
                    }
                }
                else this.Panel_CARTERA.Visible = false;
            }
        }
        else
        {
            configurarMensajes(true, System.Drawing.Color.Red);
            Label_MENSAJE.Text = _contactos.MensajeError;

            configurarBotonesDeAccion(true, false, false, false, true);

            Panel_RESULTADOS_GRID.Visible = false;

            Panel_FORMULARIO.Visible = false;
        }
    }

    private void iniciarControlesModificar()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        contactos _contactos = new contactos(Session["idEmpresa"].ToString());

        Decimal REGISTRO = Convert.ToDecimal(QueryStringSeguro["contacto"]);
        DataTable tablainfoContacto = _contactos.ObtenerContactosPorRegistro(REGISTRO);

        if (_contactos.MensajeError == null)
        {
            if (tablainfoContacto.Rows.Count <= 0)
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = "ADVERTENCIA: No se encontró información del Contcato # " + REGISTRO.ToString();

                configurarBotonesDeAccion(true, false, false, false, true);

                Panel_RESULTADOS_GRID.Visible = false;

                Panel_FORMULARIO.Visible = false;
            }
            else
            {
                configurarBotonesDeAccion(false, false, true, true, true);

                Panel_RESULTADOS_GRID.Visible = false;

                configurarMensajes(false, System.Drawing.Color.Green);

                Panel_FORMULARIO.Visible = true;
                Panel_FORMULARIO.Enabled = true;

                Panel_CONTROL_REGISTRO.Visible = false;

                DataRow filaInfoContacto = tablainfoContacto.Rows[0];

                Page.Header.Title += ": " + filaInfoContacto["CONT_NOM"].ToString();

                TextBox_REGISTRO.Text = filaInfoContacto["REGISTRO"].ToString().Trim();
                Panel_IDENTIFICADOR.Visible = true;
                Panel_IDENTIFICADOR.Enabled = true;
                TextBox_REGISTRO.Enabled = false;

                Panel_CONTACTOS_ACTUALES.Visible = false;

                cargar_DropDownList_ESTADO();
                DropDownList_ESTADO.SelectedValue = filaInfoContacto["CONT_ESTADO"].ToString().ToUpper().Trim();

                TextBox_CONT_NOM.Text = filaInfoContacto["CONT_NOM"].ToString().Trim();
                TextBox_CONT_CARGO.Text = filaInfoContacto["CONT_CARGO"].ToString().Trim();
                TextBox_CONT_MAIL.Text = filaInfoContacto["CONT_MAIL"].ToString().Trim();

                TextBox_CONT_TEL.Text = filaInfoContacto["CONT_TEL"].ToString().Trim();
                TextBox_CONT_TEL1.Text = filaInfoContacto["CONT_TEL1"].ToString().Trim();
                TextBox_CONT_CELULAR.Text = filaInfoContacto["CONT_CELULAR"].ToString().Trim();

                _contactos.MensajeError = null;
                DataRow filaInfoCiudadDepartamentoYRegional = obtenerDatosCiudadDepartamentoRegionalDelContacto(filaInfoContacto["CONT_CIUDAD"].ToString().Trim());
                if (filaInfoCiudadDepartamentoYRegional != null)
                {
                    cargar_DropDownList_REGIONAL();
                    DropDownList_REGIONAL.SelectedValue = filaInfoCiudadDepartamentoYRegional["ID_REGIONAL"].ToString().Trim();

                    cargar_DropDownList_CONT_DEPARTAMENTO(filaInfoCiudadDepartamentoYRegional["ID_REGIONAL"].ToString().Trim());
                    DropDownList_CONT_DEPARTAMENTO.SelectedValue = filaInfoCiudadDepartamentoYRegional["ID_DEPARTAMENTO"].ToString().Trim();

                    cargar_DropDownList_CONT_CIUDAD(filaInfoCiudadDepartamentoYRegional["ID_DEPARTAMENTO"].ToString().Trim(), filaInfoCiudadDepartamentoYRegional["ID_REGIONAL"].ToString().Trim());
                    DropDownList_CONT_CIUDAD.SelectedValue = filaInfoCiudadDepartamentoYRegional["ID_CIUDAD"].ToString().Trim();
                }
                else
                {
                    cargar_DropDownList_REGIONAL();
                    inhabilitar_DropDownList_CONT_DEPARTAMENTO();
                    inhabilitar_DropDownList_CONT_CIUDAD();
                }

                if (_proceso == Convert.ToInt32(QueryStringSeguro["proceso"]))
                {
                    this.Panel_CARTERA.Visible = true;
                    this.TextBox_DIAS_PAGO.Text = filaInfoContacto["DIAS"].ToString().Trim();
                    if (!String.IsNullOrEmpty(filaInfoContacto["FORMA_PAGO"].ToString()))
                    {
                        this.DropDownList_FORMA_PAGO.SelectedIndex = DropDownList_FORMA_PAGO.Items.IndexOf(DropDownList_FORMA_PAGO.Items.FindByValue(filaInfoContacto["FORMA_PAGO"].ToString().Trim()));
                        if (DropDownList_FORMA_PAGO.SelectedItem.Value == "Consignacion" || DropDownList_FORMA_PAGO.SelectedItem.Value == "Transferencia")
                        {
                            cargar_DropDownList_BANCOS();
                            if (!String.IsNullOrEmpty(filaInfoContacto["BANCO"].ToString()))
                            {
                                this.DropDownList_BANCOS.SelectedIndex = DropDownList_BANCOS.Items.IndexOf(DropDownList_BANCOS.Items.FindByValue(filaInfoContacto["BANCO"].ToString().Trim()));
                                cargar_DropDownList_CUENTAS(Convert.ToInt32(DropDownList_BANCOS.SelectedItem.Value.ToString()));
                                if (!String.IsNullOrEmpty(filaInfoContacto["CUENTA"].ToString()))
                                {
                                    this.DropDownList_CUENTAS.SelectedIndex = DropDownList_CUENTAS.Items.IndexOf(DropDownList_CUENTAS.Items.FindByValue(filaInfoContacto["CUENTA"].ToString().Trim()));
                                }
                            }
                        }
                        else this.DropDownList_BANCOS.Items.Clear();
                    }
                }
                else this.Panel_CARTERA.Visible = false;


                Panel_INMEDIATO.Visible = false;
            }
        }
        else
        {
            configurarMensajes(true, System.Drawing.Color.Red);
            Label_MENSAJE.Text = _contactos.MensajeError;

            configurarBotonesDeAccion(true, false, false, false, true);

            Panel_RESULTADOS_GRID.Visible = false;

            Panel_FORMULARIO.Visible = false;
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
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        _proceso = Convert.ToInt32(tabla.proceso.Financiera);

        Page.Header.Title = "CONTÁCTOS";

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

        cliente _clienteMODULO = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoEmpresa = _clienteMODULO.ObtenerEmpresaConIdEmpresa(ID_EMPRESA);
        DataRow filaTablaInfoEmpresa = tablaInfoEmpresa.Rows[0];
        configurarInfoAdicionalModulo(true, filaTablaInfoEmpresa["RAZ_SOCIAL"].ToString());

        if (IsPostBack == false)
        {
            configurar_paneles_popup(Panel_FONDO_MENSAJE, Panel_MENSAJES);

            String accion = QueryStringSeguro["accion"].ToString();

            if (accion == "inicial")
            {
                iniciarControlesInicial();
            }
            else
            {
                if (accion == "nuevo")
                {
                    iniciarControlesNuevo();
                }
                else
                {
                    if (accion == "cargarNuevo")
                    {
                        iniciarControlesCargar();

                        configurarMensajes(true, System.Drawing.Color.Green);
                        String REGISTRO = QueryStringSeguro["contacto"].ToString();
                        Label_MENSAJE.Text = "El contácto " + REGISTRO + " fue creado correctamente.";
                    }
                    else
                    {
                        if (accion == "cargarModificado")
                        {
                            iniciarControlesCargar();

                            configurarMensajes(true, System.Drawing.Color.Green);
                            String REGISTRO = QueryStringSeguro["contacto"].ToString();
                            Label_MENSAJE.Text = "El contácto " + REGISTRO + " fue modificado correctamente.";
                        }
                        else
                        {
                            if (accion == "cargar")
                            {
                                iniciarControlesCargar();
                            }
                            else
                            {
                                if (accion == "modificar")
                                {
                                    iniciarControlesModificar();
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    protected void GridView_CONTACTOS_SelectedIndexChanged(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String codigoContactoSeleccionado = GridView_CONTACTOS.Rows[GridView_CONTACTOS.SelectedIndex].Cells[1].Text;
        String reg = QueryStringSeguro["reg"].ToString();
        String PROCESO = QueryStringSeguro["proceso"].ToString();

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        if (Convert.ToInt32(tabla.proceso.ContactoComercial) == Convert.ToInt32(PROCESO))
        {
            QueryStringSeguro["img_area"] = "comercial";
            QueryStringSeguro["nombre_area"] = "GESTIÓN COMERCIAL";
        }
        else
        {
            if (Convert.ToInt32(tabla.proceso.ContactoContratacion) == Convert.ToInt32(PROCESO))
            {
                QueryStringSeguro["img_area"] = "contratacion";
                QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
            }
            else
            {
                if (Convert.ToInt32(tabla.proceso.ContactoSeleccion) == Convert.ToInt32(PROCESO))
                {
                    QueryStringSeguro["img_area"] = "seleccion";
                    QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
                }
                else
                {
                    if (Convert.ToInt32(tabla.proceso.ContactoNominaFacturacion) == Convert.ToInt32(PROCESO))
                    {
                        QueryStringSeguro["img_area"] = "nomina";
                        QueryStringSeguro["nombre_area"] = "NÓMINA Y FACTURACIÓN";
                    }
                    else
                    {
                        if (Convert.ToInt32(tabla.proceso.Nomina) == Convert.ToInt32(PROCESO))
                        {
                            QueryStringSeguro["img_area"] = "nomina";
                            QueryStringSeguro["nombre_area"] = "NÓMINA Y FACTURACIÓN";
                        }
                        else
                        {
                            if (Convert.ToInt32(tabla.proceso.Financiera) == Convert.ToInt32(PROCESO))
                            {
                                QueryStringSeguro["img_area"] = "financiera";
                                QueryStringSeguro["nombre_area"] = "FINANCIERA";
                                _proceso = Convert.ToInt32(tabla.proceso.Financiera);
                            }
                        }
                    }
                }
            }
        }

        QueryStringSeguro["nombre_modulo"] = "CONTÁCTOS";
        QueryStringSeguro["accion"] = "cargar";
        QueryStringSeguro["reg"] = reg;
        QueryStringSeguro["contacto"] = codigoContactoSeleccionado;
        QueryStringSeguro["proceso"] = PROCESO;

        Response.Redirect("~/maestros/contactos.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }

    protected void Button_NUEVO_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String ID_EMPRESA = QueryStringSeguro["reg"].ToString();
        String PROCESO = QueryStringSeguro["proceso"].ToString();

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        if (Convert.ToInt32(tabla.proceso.ContactoComercial) == Convert.ToInt32(PROCESO))
        {
            QueryStringSeguro["img_area"] = "comercial";
            QueryStringSeguro["nombre_area"] = "GESTIÓN COMERCIAL";
        }
        else
        {
            if (Convert.ToInt32(tabla.proceso.ContactoContratacion) == Convert.ToInt32(PROCESO))
            {
                QueryStringSeguro["img_area"] = "contratacion";
                QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
            }
            else
            {
                if (Convert.ToInt32(tabla.proceso.ContactoSeleccion) == Convert.ToInt32(PROCESO))
                {
                    QueryStringSeguro["img_area"] = "seleccion";
                    QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
                }
                else
                {
                    if (Convert.ToInt32(tabla.proceso.ContactoNominaFacturacion) == Convert.ToInt32(PROCESO))
                    {
                        QueryStringSeguro["img_area"] = "nomina";
                        QueryStringSeguro["nombre_area"] = "NÓMINA Y FACTURACIÓN";
                    }
                    else
                    {
                        if (Convert.ToInt32(tabla.proceso.Nomina) == Convert.ToInt32(PROCESO))
                        {
                            QueryStringSeguro["img_area"] = "nomina";
                            QueryStringSeguro["nombre_area"] = "NÓMINA Y FACTURACIÓN";
                        }
                        else
                        {
                            if (Convert.ToInt32(tabla.proceso.Financiera) == Convert.ToInt32(PROCESO))
                            {
                                QueryStringSeguro["img_area"] = "financiera";
                                QueryStringSeguro["nombre_area"] = "FINANCIERA";
                                _proceso = Convert.ToInt32(tabla.proceso.Financiera);
                            }
                        }
                    }
                }
            }
        }

        QueryStringSeguro["nombre_modulo"] = "CONTÁCTOS";
        QueryStringSeguro["accion"] = "nuevo";
        QueryStringSeguro["reg"] = ID_EMPRESA;
        QueryStringSeguro["proceso"] = PROCESO;

        Response.Redirect("~/maestros/contactos.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }

    protected void DropDownList_CONT_DEPARTAMENTO_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_CONT_DEPARTAMENTO.SelectedValue == "")
        {
            inhabilitar_DropDownList_CONT_CIUDAD();
        }
        else
        {
            String ID_DEPARTAMENTO = DropDownList_CONT_DEPARTAMENTO.SelectedValue.ToString();
            String ID_REGIONAL = DropDownList_REGIONAL.SelectedValue.ToString();
            cargar_DropDownList_CONT_CIUDAD(ID_DEPARTAMENTO, ID_REGIONAL);
            DropDownList_CONT_CIUDAD.Enabled = true;
        }
    }

    protected void Button_LISTA_CONTACTOS_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String ID_EMPRESA = QueryStringSeguro["reg"].ToString();
        String PROCESO = QueryStringSeguro["proceso"].ToString();

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        if (Convert.ToInt32(tabla.proceso.ContactoComercial) == Convert.ToInt32(PROCESO))
        {
            QueryStringSeguro["img_area"] = "comercial";
            QueryStringSeguro["nombre_area"] = "GESTIÓN COMERCIAL";
        }
        else
        {
            if (Convert.ToInt32(tabla.proceso.ContactoContratacion) == Convert.ToInt32(PROCESO))
            {
                QueryStringSeguro["img_area"] = "contratacion";
                QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
            }
            else
            {
                if (Convert.ToInt32(tabla.proceso.ContactoSeleccion) == Convert.ToInt32(PROCESO))
                {
                    QueryStringSeguro["img_area"] = "seleccion";
                    QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
                }
                else
                {
                    if (Convert.ToInt32(tabla.proceso.ContactoNominaFacturacion) == Convert.ToInt32(PROCESO))
                    {
                        QueryStringSeguro["img_area"] = "nomina";
                        QueryStringSeguro["nombre_area"] = "NÓMINA Y FACTURACIÓN";
                    }
                    else
                    {
                        if (Convert.ToInt32(tabla.proceso.Nomina) == Convert.ToInt32(PROCESO))
                        {
                            QueryStringSeguro["img_area"] = "nomina";
                            QueryStringSeguro["nombre_area"] = "NÓMINA Y FACTURACIÓN";
                        }
                        else
                        {
                            if (Convert.ToInt32(tabla.proceso.Financiera) == Convert.ToInt32(PROCESO))
                            {
                                QueryStringSeguro["img_area"] = "financiera";
                                QueryStringSeguro["nombre_area"] = "FINANCIERA";
                                _proceso = Convert.ToInt32(tabla.proceso.Financiera);
                            }
                        }
                    }
                }
            }
        }

        QueryStringSeguro["nombre_modulo"] = "CONTÁCTOS";
        QueryStringSeguro["accion"] = "inicial";
        QueryStringSeguro["reg"] = ID_EMPRESA;
        QueryStringSeguro["proceso"] = PROCESO.ToString();

        Response.Redirect("~/maestros/contactos.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }

    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String accion = QueryStringSeguro["accion"].ToString();
        String ID_EMPRESA = QueryStringSeguro["reg"].ToString();
        String PROCESO = QueryStringSeguro["proceso"].ToString();

        if (accion == "nuevo")
        {
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

            if (Convert.ToInt32(tabla.proceso.ContactoComercial) == Convert.ToInt32(PROCESO))
            {
                QueryStringSeguro["img_area"] = "comercial";
                QueryStringSeguro["nombre_area"] = "GESTIÓN COMERCIAL";
            }
            else
            {
                if (Convert.ToInt32(tabla.proceso.ContactoContratacion) == Convert.ToInt32(PROCESO))
                {
                    QueryStringSeguro["img_area"] = "contratacion";
                    QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
                }
                else
                {
                    if (Convert.ToInt32(tabla.proceso.ContactoSeleccion) == Convert.ToInt32(PROCESO))
                    {
                        QueryStringSeguro["img_area"] = "seleccion";
                        QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
                    }
                    else
                    {
                        if (Convert.ToInt32(tabla.proceso.ContactoNominaFacturacion) == Convert.ToInt32(PROCESO))
                        {
                            QueryStringSeguro["img_area"] = "nomina";
                            QueryStringSeguro["nombre_area"] = "NÓMINA Y FACTURACIÓN";
                        }
                        else
                        {
                            if (Convert.ToInt32(tabla.proceso.Nomina) == Convert.ToInt32(PROCESO))
                            {
                                QueryStringSeguro["img_area"] = "nomina";
                                QueryStringSeguro["nombre_area"] = "NÓMINA Y FACTURACIÓN";
                            }
                            else
                            {
                                if (Convert.ToInt32(tabla.proceso.Financiera) == Convert.ToInt32(PROCESO))
                                {
                                    QueryStringSeguro["img_area"] = "financiera";
                                    QueryStringSeguro["nombre_area"] = "FINANCIERA";
                                    _proceso = Convert.ToInt32(tabla.proceso.Financiera);
                                }
                            }
                        }
                    }
                }
            }

            QueryStringSeguro["nombre_modulo"] = "CONTÁCTOS";
            QueryStringSeguro["accion"] = "inicial";
            QueryStringSeguro["reg"] = ID_EMPRESA;
            QueryStringSeguro["proceso"] = PROCESO.ToString();

            Response.Redirect("~/maestros/contactos.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
        }
        else
        {
            if (accion == "modificar")
            {
                String REGISTRO = QueryStringSeguro["contaCto"].ToString();

                QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

                if (Convert.ToInt32(tabla.proceso.ContactoComercial) == Convert.ToInt32(PROCESO))
                {
                    QueryStringSeguro["img_area"] = "comercial";
                    QueryStringSeguro["nombre_area"] = "GESTIÓN COMERCIAL";
                }
                else
                {
                    if (Convert.ToInt32(tabla.proceso.ContactoContratacion) == Convert.ToInt32(PROCESO))
                    {
                        QueryStringSeguro["img_area"] = "contratacion";
                        QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
                    }
                    else
                    {
                        if (Convert.ToInt32(tabla.proceso.ContactoSeleccion) == Convert.ToInt32(PROCESO))
                        {
                            QueryStringSeguro["img_area"] = "seleccion";
                            QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
                        }
                        else
                        {
                            if (Convert.ToInt32(tabla.proceso.ContactoNominaFacturacion) == Convert.ToInt32(PROCESO))
                            {
                                QueryStringSeguro["img_area"] = "nomina";
                                QueryStringSeguro["nombre_area"] = "NÓMINA Y FACTURACIÓN";
                            }
                            else
                            {
                                if (Convert.ToInt32(tabla.proceso.Nomina) == Convert.ToInt32(PROCESO))
                                {
                                    QueryStringSeguro["img_area"] = "nomina";
                                    QueryStringSeguro["nombre_area"] = "NÓMINA Y FACTURACIÓN";
                                }
                                else
                                {
                                    if (Convert.ToInt32(tabla.proceso.Financiera) == Convert.ToInt32(PROCESO))
                                    {
                                        QueryStringSeguro["img_area"] = "financiera";
                                        QueryStringSeguro["nombre_area"] = "FINANCIERA";
                                        _proceso = Convert.ToInt32(tabla.proceso.Financiera);
                                    }
                                }
                            }
                        }
                    }
                }

                QueryStringSeguro["nombre_modulo"] = "CONTÁCTOS";
                QueryStringSeguro["accion"] = "cargar";
                QueryStringSeguro["reg"] = ID_EMPRESA;
                QueryStringSeguro["contacto"] = REGISTRO.ToString();
                QueryStringSeguro["proceso"] = PROCESO.ToString();

                Response.Redirect("~/maestros/contactos.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
            }
        }
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String accion = QueryStringSeguro["accion"].ToString();
        int PROCESO = Convert.ToInt32(QueryStringSeguro["proceso"]);
        tabla.proceso pr = (tabla.proceso)PROCESO;

        contactos _contactos = new contactos(Session["idEmpresa"].ToString());
        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

        String CONT_NOM = TextBox_CONT_NOM.Text.ToUpper().Trim();
        String CONT_CARGO = TextBox_CONT_CARGO.Text.ToUpper().Trim();
        
        String CONT_MAIL = null;
        if (String.IsNullOrEmpty(TextBox_CONT_MAIL.Text) == false)
        {
            CONT_MAIL = TextBox_CONT_MAIL.Text.Trim();
        }
     
        String CONT_TEL = TextBox_CONT_TEL.Text.ToUpper().Trim();
        
        String CONT_TEL1 = "Ninguno";
        if (TextBox_CONT_TEL1.Text.ToUpper().Trim() != "")
        {
            CONT_TEL1 = TextBox_CONT_TEL1.Text.ToUpper().Trim();
        }

        String CONT_CELULAR = "Ninguno";
        if (TextBox_CONT_CELULAR.Text.ToUpper().Trim() != "")
        {
            CONT_CELULAR = TextBox_CONT_CELULAR.Text.ToUpper().Trim();
        }

        String CONT_CIUDAD = DropDownList_CONT_CIUDAD.SelectedValue.ToString();

        String USU_CRE = Session["USU_LOG"].ToString();
        String USU_MOD = Session["USU_LOG"].ToString();

        Int32 DIAS = 0;
        String FORMA_PAGO = "";
        Decimal BANCO = 0;
        Decimal CUENTA = 0;

        if (_proceso == PROCESO)
        {
            Int32.TryParse(this.TextBox_DIAS_PAGO.Text, out DIAS);
            if (this.DropDownList_FORMA_PAGO.SelectedIndex != 0) FORMA_PAGO = this.DropDownList_FORMA_PAGO.SelectedItem.Value.ToString();
            if (this.DropDownList_BANCOS.SelectedIndex != 0) BANCO = Convert.ToDecimal(this.DropDownList_BANCOS.SelectedItem.Value.ToString());
            if (this.DropDownList_CUENTAS.SelectedIndex != 0) CUENTA = Convert.ToDecimal(this.DropDownList_CUENTAS.SelectedItem.Value.ToString());
        }
        
        Boolean verificador = true;
        Decimal REGISTRO = 0;
        if (accion == "nuevo")
        {
            String ESTADO;
            if (CheckBox_INMEDIATO.Checked == true)
            {
                ESTADO = "INMEDIATO";
            }
            else
            {
                ESTADO = "ACTIVO";
            }

            REGISTRO = _contactos.Adicionar(ID_EMPRESA, pr, CONT_NOM, CONT_CARGO, CONT_MAIL, CONT_TEL, CONT_TEL1, CONT_CELULAR, CONT_CIUDAD, USU_CRE, ESTADO, DIAS, FORMA_PAGO, BANCO, CUENTA);

            if (REGISTRO == 0)
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = _contactos.MensajeError;
            }
            else 
            {
                maestrasInterfaz _maestrasInterfaz = new maestrasInterfaz();
                _maestrasInterfaz.CargarEnBdElManualServicioActual(ID_EMPRESA);


                QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());


                if (Convert.ToInt32(tabla.proceso.ContactoComercial) == Convert.ToInt32(PROCESO))
                {
                    QueryStringSeguro["img_area"] = "comercial";
                    QueryStringSeguro["nombre_area"] = "GESTIÓN COMERCIAL";
                }
                else
                {
                    if (Convert.ToInt32(tabla.proceso.ContactoContratacion) == Convert.ToInt32(PROCESO))
                    {
                        QueryStringSeguro["img_area"] = "contratacion";
                        QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
                    }
                    else
                    {
                        if (Convert.ToInt32(tabla.proceso.ContactoSeleccion) == Convert.ToInt32(PROCESO))
                        {
                            QueryStringSeguro["img_area"] = "seleccion";
                            QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
                        }
                        else
                        {
                            if (Convert.ToInt32(tabla.proceso.ContactoNominaFacturacion) == Convert.ToInt32(PROCESO))
                            {
                                QueryStringSeguro["img_area"] = "nomina";
                                QueryStringSeguro["nombre_area"] = "NÓMINA Y FACTURACIÓN";
                            }
                            else
                            {
                                if (Convert.ToInt32(tabla.proceso.Nomina) == Convert.ToInt32(PROCESO))
                                {
                                    QueryStringSeguro["img_area"] = "nomina";
                                    QueryStringSeguro["nombre_area"] = "NÓMINA Y FACTURACIÓN";
                                }
                                else
                                {
                                    if (Convert.ToInt32(tabla.proceso.Financiera) == Convert.ToInt32(PROCESO))
                                    {
                                        QueryStringSeguro["img_area"] = "financiera";
                                        QueryStringSeguro["nombre_area"] = "FINANCIERA";
                                    }
                                }
                            }
                        }
                    }
                }

                QueryStringSeguro["nombre_modulo"] = "CONTÁCTOS";
                QueryStringSeguro["accion"] = "cargarNuevo";
                QueryStringSeguro["reg"] = ID_EMPRESA.ToString();
                QueryStringSeguro["contacto"] = REGISTRO.ToString();
                QueryStringSeguro["proceso"] = PROCESO.ToString();

                Response.Redirect("~/maestros/contactos.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
            }
        }
        else
        {
            if (accion == "modificar")
            {
                String CONT_ESTADO = DropDownList_ESTADO.SelectedValue;
                REGISTRO = Convert.ToDecimal(QueryStringSeguro["contacto"].ToString());
                
                verificador = _contactos.Actualizar(REGISTRO, ID_EMPRESA, pr, CONT_NOM, CONT_CARGO, CONT_MAIL, CONT_TEL, CONT_TEL1, CONT_CELULAR, CONT_CIUDAD, USU_MOD, CONT_ESTADO, DIAS, FORMA_PAGO, BANCO, CUENTA);

                if (verificador == false)
                {
                    configurarMensajes(true, System.Drawing.Color.Red);
                    Label_MENSAJE.Text = _contactos.MensajeError;
                }
                else
                {
                    maestrasInterfaz _maestrasInterfaz = new maestrasInterfaz();
                    _maestrasInterfaz.CargarEnBdElManualServicioActual(ID_EMPRESA);


                    QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());


                    if (Convert.ToInt32(tabla.proceso.ContactoComercial) == Convert.ToInt32(PROCESO))
                    {
                        QueryStringSeguro["img_area"] = "comercial";
                        QueryStringSeguro["nombre_area"] = "GESTIÓN COMERCIAL";
                    }
                    else
                    {
                        if (Convert.ToInt32(tabla.proceso.ContactoContratacion) == Convert.ToInt32(PROCESO))
                        {
                            QueryStringSeguro["img_area"] = "contratacion";
                            QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
                        }
                        else
                        {
                            if (Convert.ToInt32(tabla.proceso.ContactoSeleccion) == Convert.ToInt32(PROCESO))
                            {
                                QueryStringSeguro["img_area"] = "seleccion";
                                QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
                            }
                            else
                            {
                                if (Convert.ToInt32(tabla.proceso.ContactoNominaFacturacion) == Convert.ToInt32(PROCESO))
                                {
                                    QueryStringSeguro["img_area"] = "nomina";
                                    QueryStringSeguro["nombre_area"] = "NÓMINA Y FACTURACIÓN";
                                }
                                else
                                {
                                    if (Convert.ToInt32(tabla.proceso.Nomina) == Convert.ToInt32(PROCESO))
                                    {
                                        QueryStringSeguro["img_area"] = "nomina";
                                        QueryStringSeguro["nombre_area"] = "NÓMINA Y FACTURACIÓN";
                                    }
                                    else
                                    {
                                        if (Convert.ToInt32(tabla.proceso.Financiera) == Convert.ToInt32(PROCESO))
                                        {
                                            QueryStringSeguro["img_area"] = "financiera";
                                            QueryStringSeguro["nombre_area"] = "FINANCIERA";
                                        }
                                    }
                                }
                            }
                        }
                    }

                    QueryStringSeguro["nombre_modulo"] = "CONTÁCTOS";
                    QueryStringSeguro["accion"] = "cargarModificado";
                    QueryStringSeguro["reg"] = ID_EMPRESA.ToString();
                    QueryStringSeguro["contacto"] = REGISTRO.ToString();
                    QueryStringSeguro["proceso"] = PROCESO.ToString();

                    Response.Redirect("~/maestros/contactos.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
                }
            }
        }
    }

    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String codigoContactoSeleccionado = QueryStringSeguro["contacto"].ToString();
        String reg = QueryStringSeguro["reg"].ToString();
        String PROCESO = QueryStringSeguro["proceso"].ToString();

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());


        if (Convert.ToInt32(tabla.proceso.ContactoComercial) == Convert.ToInt32(PROCESO))
        {
            QueryStringSeguro["img_area"] = "comercial";
            QueryStringSeguro["nombre_area"] = "GESTIÓN COMERCIAL";
        }
        else
        {
            if (Convert.ToInt32(tabla.proceso.ContactoContratacion) == Convert.ToInt32(PROCESO))
            {
                QueryStringSeguro["img_area"] = "contratacion";
                QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
            }
            else
            {
                if (Convert.ToInt32(tabla.proceso.ContactoSeleccion) == Convert.ToInt32(PROCESO))
                {
                    QueryStringSeguro["img_area"] = "seleccion";
                    QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
                }
                else
                {
                    if (Convert.ToInt32(tabla.proceso.ContactoNominaFacturacion) == Convert.ToInt32(PROCESO))
                    {
                        QueryStringSeguro["img_area"] = "nomina";
                        QueryStringSeguro["nombre_area"] = "NÓMINA Y FACTURACIÓN";
                    }
                    else
                    {
                        if (Convert.ToInt32(tabla.proceso.Nomina) == Convert.ToInt32(PROCESO))
                        {
                            QueryStringSeguro["img_area"] = "nomina";
                            QueryStringSeguro["nombre_area"] = "NÓMINA Y FACTURACIÓN";
                        }
                        else
                        {
                            if (Convert.ToInt32(tabla.proceso.Financiera) == Convert.ToInt32(PROCESO))
                            {
                                QueryStringSeguro["img_area"] = "financiera";
                                QueryStringSeguro["nombre_area"] = "FINANCIERA";
                            }
                        }
                    }
                }
            }
        }


        QueryStringSeguro["nombre_modulo"] = "CONTÁCTOS";
        QueryStringSeguro["accion"] = "modificar";
        QueryStringSeguro["reg"] = reg;
        QueryStringSeguro["contacto"] = codigoContactoSeleccionado;
        QueryStringSeguro["proceso"] = PROCESO.ToString();

        Response.Redirect("~/maestros/contactos.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }

    protected void DropDownList_REGIONAL_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_REGIONAL.SelectedValue == "")
        {
            inhabilitar_DropDownList_CONT_DEPARTAMENTO();
            inhabilitar_DropDownList_CONT_CIUDAD();
        }
        else
        {
            String ID_REGIONAL = DropDownList_REGIONAL.SelectedValue.ToString();
            cargar_DropDownList_CONT_DEPARTAMENTO(ID_REGIONAL);
            DropDownList_CONT_DEPARTAMENTO.Enabled = true;
            inhabilitar_DropDownList_CONT_CIUDAD();
        }
    }

    protected void DropDownList_CONTACTOS_EXISTENTES_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_CONTACTOS_EXISTENTES.SelectedValue != "")
        {
            Decimal REGISTRO = Convert.ToDecimal(DropDownList_CONTACTOS_EXISTENTES.SelectedValue);

            contactos _contactos = new contactos(Session["idEmpresa"].ToString());
            DataTable tablainfoContacto = _contactos.ObtenerContactosPorRegistro(REGISTRO);

            if (_contactos.MensajeError == null)
            {
                if (tablainfoContacto.Rows.Count <= 0)
                {
                    configurarMensajes(true, System.Drawing.Color.Red);
                    Label_MENSAJE.Text = "ADVERTENCIA: No se encontró información del Contácto identificado con el número: " + REGISTRO.ToString();

                    configurarBotonesDeAccion(true, false, false, false, true);

                    Panel_RESULTADOS_GRID.Visible = false;

                    Panel_FORMULARIO.Visible = false;
                }
                else
                {
                    DataRow filaInfoContacto = tablainfoContacto.Rows[0];

                    cargar_DropDownList_ESTADO();
                    DropDownList_ESTADO.SelectedValue = filaInfoContacto["CONT_ESTADO"].ToString().ToUpper().Trim();

                    TextBox_CONT_NOM.Text = filaInfoContacto["CONT_NOM"].ToString().Trim();
                    TextBox_CONT_CARGO.Text = filaInfoContacto["CONT_CARGO"].ToString().Trim();
                    TextBox_CONT_MAIL.Text = filaInfoContacto["CONT_MAIL"].ToString().Trim();

                    TextBox_CONT_TEL.Text = filaInfoContacto["CONT_TEL"].ToString().Trim();
                    TextBox_CONT_TEL1.Text = filaInfoContacto["CONT_TEL1"].ToString().Trim();
                    TextBox_CONT_CELULAR.Text = filaInfoContacto["CONT_CELULAR"].ToString().Trim();

                    _contactos.MensajeError = null;
                    DataRow filaInfoCiudadDepartamentoYRegional = obtenerDatosCiudadDepartamentoRegionalDelContacto(filaInfoContacto["CONT_CIUDAD"].ToString().Trim());
                    if (filaInfoCiudadDepartamentoYRegional != null)
                    {
                        cargar_DropDownList_REGIONAL();
                        DropDownList_REGIONAL.SelectedValue = filaInfoCiudadDepartamentoYRegional["ID_REGIONAL"].ToString().Trim();

                        cargar_DropDownList_CONT_DEPARTAMENTO(filaInfoCiudadDepartamentoYRegional["ID_REGIONAL"].ToString().Trim());
                        DropDownList_CONT_DEPARTAMENTO.SelectedValue = filaInfoCiudadDepartamentoYRegional["ID_DEPARTAMENTO"].ToString().Trim();
                        DropDownList_CONT_DEPARTAMENTO.Enabled = true;

                        cargar_DropDownList_CONT_CIUDAD(filaInfoCiudadDepartamentoYRegional["ID_DEPARTAMENTO"].ToString().Trim(), filaInfoCiudadDepartamentoYRegional["ID_REGIONAL"].ToString().Trim());
                        DropDownList_CONT_CIUDAD.SelectedValue = filaInfoCiudadDepartamentoYRegional["ID_CIUDAD"].ToString().Trim();
                        DropDownList_CONT_CIUDAD.Enabled = true;
                    }
                    else
                    {
                        cargar_DropDownList_REGIONAL();
                        inhabilitar_DropDownList_CONT_DEPARTAMENTO();
                        inhabilitar_DropDownList_CONT_CIUDAD();
                    }
                }
            }
            else
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = _contactos.MensajeError;

                configurarBotonesDeAccion(true, false, false, false, true);

                Panel_RESULTADOS_GRID.Visible = false;

                Panel_FORMULARIO.Visible = false;
            }

        }
    }

    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {
        configurarMensajes(false, System.Drawing.Color.Green);
    }

    protected void DropDownList_BANCOS_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(Convert.ToInt32(DropDownList_BANCOS.SelectedItem.Value) == 0)
        {
              DropDownList_CUENTAS.Items.Clear();
        }
        else
        {
            cargar_DropDownList_CUENTAS(Convert.ToInt32(this.DropDownList_BANCOS.SelectedItem.Value.ToString()));
        }
    }

    protected void DropDownList_FORMA_PAGO_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_FORMA_PAGO.SelectedItem.Value == "Consignacion" || DropDownList_FORMA_PAGO.SelectedItem.Value == "Transferencia") cargar_DropDownList_BANCOS();
        else this.DropDownList_BANCOS.Items.Clear();
    }
}