using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using Brainsbits.LLB;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.comercial;
using Brainsbits.LLB.seleccion;

using TSHAK.Components;
using Brainsbits.LLB.seguridad;

public partial class _Default : System.Web.UI.Page
{
    private String NOMBRE_AREA = tabla.NOMBRE_AREA_SELECCION;
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

    #region CARGAR DROPS Y GRIDS
    private void cargar_GridView_DATOS()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

        envioCandidato _envioCandidato = new envioCandidato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaCondicionesEnvio = _envioCandidato.ObtenerTodosLosContactosParaEnvioDeCandidatosPorIdEmpresa(ID_EMPRESA);

        if (tablaCondicionesEnvio.Rows.Count > 0)
        {
            GridView_DATOS.DataSource = tablaCondicionesEnvio;
            GridView_DATOS.DataBind();
        }
        else
        {
            configurarMensajes(true, System.Drawing.Color.Red);
            Label_MENSAJE.Text = "ADVERTENCIA: La empresa no posee condiciones de envio.";

            configurarBotonesDeAccion(true, false, false, false, false);

            Panel_RESULTADOS_GRID.Visible = false;

            Panel_FORMULARIO.Visible = false;
        }
    }
    private void cargar_DropDownList_CONT_NOMBRE()
    {
        DropDownList_CONT_NOMBRE.Items.Clear();

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

        contactos _contactos = new contactos(Session["idEmpresa"].ToString());
        DataTable tablaContactos = _contactos.ObtenerContactosPorIdEmpresa(ID_EMPRESA, tabla.proceso.ContactoSeleccion);

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_CONT_NOMBRE.Items.Add(item);

        foreach (DataRow fila in tablaContactos.Rows)
        {
            item = new ListItem(fila["CONT_NOM"].ToString(), fila["REGISTRO"].ToString());
            DropDownList_CONT_NOMBRE.Items.Add(item);
        }

        DropDownList_CONT_NOMBRE.DataBind();
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
    private void cargar_DropDownList_DEPARTAMENTO(String ID_REGIONAL)
    {
        DropDownList_DEPARTAMENTO.Items.Clear();

        departamento _departamento = new departamento(Session["idEmpresa"].ToString());
        DataTable tablaDepartamentos = _departamento.ObtenerDepartamentosPorIdRegional(ID_REGIONAL);

        ListItem item = new ListItem("Seleccione Departamento", "");
        DropDownList_DEPARTAMENTO.Items.Add(item);

        foreach (DataRow fila in tablaDepartamentos.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_DEPARTAMENTO"].ToString());
            DropDownList_DEPARTAMENTO.Items.Add(item);
        }

        DropDownList_DEPARTAMENTO.DataBind();
    }
    private void cargar_DropDownList_CIUDAD(String idDepartamento, String ID_REGIONAL)
    {
        DropDownList_CIUDAD.Items.Clear();

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
        DataTable tablaCiudades = _ciudad.ObtenerCiudadesPorIdDepartamentoEIdRegional(ID_REGIONAL, idDepartamento);

        ListItem item = new ListItem("Seleccione Ciudad", "");
        DropDownList_CIUDAD.Items.Add(item);

        foreach (DataRow fila in tablaCiudades.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_CIUDAD"].ToString());
            DropDownList_CIUDAD.Items.Add(item);
        }

        DropDownList_CIUDAD.DataBind();
    }
    #endregion

    #region METODOS PARA CONFIGURAR CONTROLES
    private void cargar_controles_nuevo_registro()
    {
        cargar_DropDownList_CONT_NOMBRE();

        cargar_DropDownList_REGIONAL();
        inhabilitar_DropDownList_DEPARTAMENTO();
        inhabilitar_DropDownList_CIUDAD();  
    }
    private void inhabilitar_DropDownList_CIUDAD()
    {
        DropDownList_CIUDAD.Enabled = false;
        DropDownList_CIUDAD.Items.Clear();
        ListItem item = new ListItem("Seleccione Ciudad", "");
        DropDownList_CIUDAD.Items.Add(item);
        DropDownList_CIUDAD.DataBind();
    }
    private void inhabilitar_DropDownList_DEPARTAMENTO()
    {
        DropDownList_DEPARTAMENTO.Enabled = false;
        DropDownList_DEPARTAMENTO.Items.Clear();
        ListItem item = new ListItem("Seleccione Departamento", "");
        DropDownList_DEPARTAMENTO.Items.Add(item);
        DropDownList_DEPARTAMENTO.DataBind();
    }
    #endregion

    #region METODOS QUE SE EJECUTAN AL PARGAR LA PAGINA
    private void iniciarControlesNuevo()
    {
        configurarBotonesDeAccion(false, false, true, true, true);

        configurarMensajes(false, System.Drawing.Color.Green);

        Panel_RESULTADOS_GRID.Visible = false;

        Panel_FORMULARIO.Visible = true;
        Panel_FORMULARIO.Enabled = true;

        Panel_CONTROL_REGISTRO.Visible = false;

        Panel_IDENTIFICADOR.Visible = false;

        cargar_controles_nuevo_registro();

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

        Button_VOLVER.Visible = bVolver;
        Button_VOLVER.Enabled = bVolver;
    }
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
    #endregion

    #region METODOS QUE SE EJECUTAN AL PARGAR PAGINA
    private void iniciarControlesInicial()
    {
        configurarBotonesDeAccion(true, false, false, false, false);

        configurarMensajes(false, System.Drawing.Color.Green);

        Panel_FORMULARIO.Visible = false;

        cargar_GridView_DATOS();
    }

    private void iniciarControlesCargar(Boolean modificar)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
        Decimal REGISTRO_ENVIO = Convert.ToDecimal(QueryStringSeguro["envio"]);

        envioCandidato _envioCandidato = new envioCandidato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaEnvio = _envioCandidato.ObtenerEnvioDeCandidatoPorRegistro(REGISTRO_ENVIO);

        if (_envioCandidato.MensajeError == null)
        {
            if (tablaEnvio.Rows.Count <= 0)
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = "ADVERTENCIA: No se encontró información de la condición de envio número: " + REGISTRO_ENVIO.ToString();

                configurarBotonesDeAccion(true, false, false, false, true);

                Panel_RESULTADOS_GRID.Visible = false;

                Panel_FORMULARIO.Visible = false;
            }
            else
            {
                if (modificar == true)
                {
                    configurarBotonesDeAccion(false, false, true, true, true);
                }
                else
                {
                    configurarBotonesDeAccion(true, true, false, false, true);
                }

                Panel_RESULTADOS_GRID.Visible = false;

                configurarMensajes(false, System.Drawing.Color.Green);

                Panel_FORMULARIO.Visible = true;
                if (modificar == true)
                {
                    Panel_FORMULARIO.Enabled = true;
                }
                else
                {
                    Panel_FORMULARIO.Enabled = false;
                }

                DataRow filaInfoEnvio = tablaEnvio.Rows[0];

                Page.Header.Title += ": " + filaInfoEnvio["REGISTRO"].ToString();

                if (modificar == true)
                {
                    Panel_CONTROL_REGISTRO.Visible = false;
                }
                else
                {
                    Panel_CONTROL_REGISTRO.Visible = true;
                    Panel_CONTROL_REGISTRO.Enabled = false;
                    TextBox_USU_CRE.Text = filaInfoEnvio["USU_CRE"].ToString();
                    try
                    {
                        TextBox_FCH_CRE.Text = DateTime.Parse(filaInfoEnvio["FCH_CRE"].ToString()).ToShortDateString();
                        TextBox_HOR_CRE.Text = DateTime.Parse(filaInfoEnvio["FCH_CRE"].ToString()).ToShortTimeString();
                    }
                    catch
                    {
                        TextBox_FCH_CRE.Text = "";
                        TextBox_HOR_CRE.Text = "";
                    }
                    TextBox_USU_MOD.Text = filaInfoEnvio["USU_MOD"].ToString();
                    try
                    {
                        TextBox_FCH_MOD.Text = DateTime.Parse(filaInfoEnvio["FCH_MOD"].ToString()).ToShortDateString();
                        TextBox_HOR_MOD.Text = DateTime.Parse(filaInfoEnvio["FCH_MOD"].ToString()).ToShortTimeString();
                    }
                    catch
                    {
                        TextBox_FCH_MOD.Text = "";
                        TextBox_HOR_MOD.Text = "";
                    }
                }
                
                if (modificar == true)
                {
                    Panel_IDENTIFICADOR.Visible = false;
                }
                else
                {
                    Panel_IDENTIFICADOR.Visible = true;
                    Panel_IDENTIFICADOR.Enabled = false;
                    TextBox_REGISTRO.Text = filaInfoEnvio["REGISTRO"].ToString().Trim();
                }

                cargar_DropDownList_CONT_NOMBRE();
                DropDownList_CONT_NOMBRE.SelectedValue = filaInfoEnvio["REGISTRO_CONTACTO"].ToString().Trim();
                TextBox_CONT_MAIL.Text = filaInfoEnvio["CONT_MAIL"].ToString().Trim();

                TextBox_DIR_ENVIO.Text = filaInfoEnvio["DIR_ENVIO"].ToString().Trim();

                if (filaInfoEnvio["ID_REGIONAL"].ToString() != "")
                {
                    cargar_DropDownList_REGIONAL();
                    DropDownList_REGIONAL.SelectedValue = filaInfoEnvio["ID_REGIONAL"].ToString();
                    cargar_DropDownList_DEPARTAMENTO(filaInfoEnvio["ID_REGIONAL"].ToString());
                    DropDownList_DEPARTAMENTO.SelectedValue = filaInfoEnvio["ID_DEPARTAMENTO"].ToString();
                    cargar_DropDownList_CIUDAD(filaInfoEnvio["ID_DEPARTAMENTO"].ToString(), filaInfoEnvio["ID_REGIONAL"].ToString());
                    DropDownList_CIUDAD.SelectedValue = filaInfoEnvio["CIU_ENVIO"].ToString().Trim();
                }
                else
                {
                    cargar_DropDownList_REGIONAL();
                    inhabilitar_DropDownList_DEPARTAMENTO();
                    inhabilitar_DropDownList_CIUDAD();
                }

                TextBox_TEL_ENVIO.Text = filaInfoEnvio["TEL_ENVIO"].ToString().Trim();

                TextBox_COND_ENVIO.Text = filaInfoEnvio["COND_ENVIO"].ToString().Trim();
            }
        }
        else
        {
            configurarMensajes(true, System.Drawing.Color.Red);
            Label_MENSAJE.Text = _envioCandidato.MensajeError;

            configurarBotonesDeAccion(true, false, false, false, true);

            Panel_RESULTADOS_GRID.Visible = false;

            Panel_FORMULARIO.Visible = false;
        }
    }

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Page.Header.Title = "CONDICIONES DE SELECCIÓN (ENVIO)";

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoEmpresa = _cliente.ObtenerEmpresaConIdEmpresa(ID_EMPRESA);
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
                if (accion == "nuevo")
                {
                    iniciarControlesNuevo();
                }
                else
                {
                    if (accion == "cargarNuevo")
                    {
                        iniciarControlesCargar(false);

                        configurarMensajes(true, System.Drawing.Color.Green);
                        String REGISTRO = QueryStringSeguro["envio"].ToString();
                        Label_MENSAJE.Text = "La condición de envio fue creada y se le asignó el ID " + REGISTRO + ".";
                    }
                    else
                    {
                        if (accion == "cargarModificado")
                        {
                            iniciarControlesCargar(false);

                            configurarMensajes(true, System.Drawing.Color.Green);
                            String REGISTRO = QueryStringSeguro["envio"].ToString();
                            Label_MENSAJE.Text = "La condición de envio  " + REGISTRO + " fue modificada correctamente.";
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
                            }
                        }
                    }
                }
            }
        }
    }
    protected void Button_NUEVO_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String ID_EMPRESA = QueryStringSeguro["reg"].ToString();

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
        QueryStringSeguro["img_area"] = "seleccion";
        QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
        QueryStringSeguro["nombre_modulo"] = "CONDICIONES DE ENVIO";
        QueryStringSeguro["accion"] = "nuevo";
        QueryStringSeguro["reg"] = ID_EMPRESA;

        Response.Redirect("~/seleccion/condicionesSeleccion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }
    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String accion = QueryStringSeguro["accion"].ToString();

        envioCandidato _envioCandidato = new envioCandidato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

        Decimal REGISTRO_CONTACTO = Convert.ToDecimal(DropDownList_CONT_NOMBRE.SelectedValue);
        String DIR_ENVIO = TextBox_DIR_ENVIO.Text.ToUpper().Trim();
        String CIU_ENVIO = DropDownList_CIUDAD.SelectedValue;
        String TEL_ENVIO = TextBox_TEL_ENVIO.Text.ToUpper().Trim();
        string COND_ENVIO = TextBox_COND_ENVIO.Text.ToUpper().Trim();

        Decimal REGISTRO_ENVIO;
        if (accion == "nuevo")
        {
            REGISTRO_ENVIO = _envioCandidato.Adicionar(ID_EMPRESA, DIR_ENVIO, CIU_ENVIO, TEL_ENVIO, REGISTRO_CONTACTO, COND_ENVIO);

            if (REGISTRO_ENVIO == 0)
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = _envioCandidato.MensajeError;
            }
            else
            {
                maestrasInterfaz _maestrasInterfaz = new maestrasInterfaz();
                _maestrasInterfaz.CargarEnBdElManualServicioActual(ID_EMPRESA);


                QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
                QueryStringSeguro["img_area"] = "seleccion";
                QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
                QueryStringSeguro["nombre_modulo"] = "CONDICIONES DE ENVIO";
                QueryStringSeguro["accion"] = "cargarNuevo";
                QueryStringSeguro["reg"] = ID_EMPRESA.ToString();
                QueryStringSeguro["envio"] = REGISTRO_ENVIO.ToString();

                Response.Redirect("~/seleccion/condicionesSeleccion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
            }
        }
        else
        {
            REGISTRO_ENVIO = Convert.ToDecimal(QueryStringSeguro["envio"]);

            Boolean verificador = _envioCandidato.Actualizar(REGISTRO_ENVIO, DIR_ENVIO, CIU_ENVIO, TEL_ENVIO, COND_ENVIO, REGISTRO_CONTACTO, ID_EMPRESA);

            if (verificador == false)
            {
                configurarMensajes(true, System.Drawing.Color.Green);
                Label_MENSAJE.Text = _envioCandidato.MensajeError;
            }
            else
            {
                maestrasInterfaz _maestrasInterfaz = new maestrasInterfaz();
                _maestrasInterfaz.CargarEnBdElManualServicioActual(ID_EMPRESA);


                QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
                QueryStringSeguro["img_area"] = "seleccion";
                QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
                QueryStringSeguro["nombre_modulo"] = "CONDICIONES DE ENVIO";
                QueryStringSeguro["accion"] = "cargarModificado";
                QueryStringSeguro["reg"] = ID_EMPRESA.ToString();
                QueryStringSeguro["envio"] = REGISTRO_ENVIO.ToString();

                Response.Redirect("~/seleccion/condicionesSeleccion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
            }
        }   
    }
    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String ID_EMPRESA = QueryStringSeguro["reg"].ToString();
        String REGISTRO = QueryStringSeguro["envio"].ToString();

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
        QueryStringSeguro["img_area"] = "seleccion";
        QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
        QueryStringSeguro["nombre_modulo"] = "CONDICIONES DE ENVIO";
        QueryStringSeguro["accion"] = "modificar";
        QueryStringSeguro["reg"] = ID_EMPRESA;
        QueryStringSeguro["envio"] = REGISTRO;

        Response.Redirect("~/seleccion/condicionesSeleccion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }
    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String accion = QueryStringSeguro["accion"].ToString();
        String ID_EMPRESA = QueryStringSeguro["reg"].ToString();

        if (accion == "nuevo")
        {
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
            QueryStringSeguro["img_area"] = "seleccion";
            QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
            QueryStringSeguro["nombre_modulo"] = "CONDICIONES DE ENVIO";
            QueryStringSeguro["accion"] = "inicial";
            QueryStringSeguro["reg"] = ID_EMPRESA.ToString();

            Response.Redirect("~/seleccion/condicionesSeleccion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
        }
        else
        {
            if (accion == "modificar")
            {
                String REGISTRO = QueryStringSeguro["envio"].ToString();

                QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
                QueryStringSeguro["img_area"] = "seleccion";
                QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
                QueryStringSeguro["nombre_modulo"] = "CONDICIONES DE ENVIO";
                QueryStringSeguro["accion"] = "cargar";
                QueryStringSeguro["reg"] = ID_EMPRESA;
                QueryStringSeguro["envio"] = REGISTRO;

                Response.Redirect("~/seleccion/condicionesSeleccion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
            }
        }
    }
    protected void Button_LISTA_CONTACTOS_Click(object sender, EventArgs e)
    {

    }
    protected void GridView_DATOS_SelectedIndexChanged(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String codigoCondicionSeleccionado = GridView_DATOS.SelectedDataKey["REGISTRO"].ToString();
        String reg = QueryStringSeguro["reg"].ToString();

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
        QueryStringSeguro["img_area"] = "seleccion";

        QueryStringSeguro["img_area"] = "seleccion";
        QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
        QueryStringSeguro["nombre_modulo"] = "CONDICIONES DE ENVIO";
        QueryStringSeguro["accion"] = "cargar";
        QueryStringSeguro["reg"] = reg;
        QueryStringSeguro["envio"] = codigoCondicionSeleccionado;

        Response.Redirect("~/seleccion/condicionesSeleccion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }
    protected void DropDownList_CONT_NOMBRE_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_CONT_NOMBRE.SelectedValue == "")
        {
            TextBox_CONT_MAIL.Text = "";
        }
        else
        { 
            Decimal REGISTRO = Convert.ToDecimal(DropDownList_CONT_NOMBRE.SelectedValue);
            contactos _contactos = new contactos(Session["idEmpresa"].ToString());
            DataTable tablaContacto = _contactos.ObtenerContactosPorRegistro(REGISTRO);

            if (tablaContacto.Rows.Count > 0)
            {
                DataRow filaTablaContacto = tablaContacto.Rows[0];
                if (!(String.IsNullOrEmpty(filaTablaContacto["CONT_MAIL"].ToString().Trim())))
                {
                    TextBox_CONT_MAIL.Text = filaTablaContacto["CONT_MAIL"].ToString().Trim();
                }
            }
            else
            {
                TextBox_CONT_MAIL.Text = String.Empty;
            }
        }
    }

    protected void DropDownList_REGIONAL_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_REGIONAL.SelectedValue == "")
        {
            inhabilitar_DropDownList_DEPARTAMENTO();
            inhabilitar_DropDownList_CIUDAD();
        }
        else
        {
            String ID_REGIONAL = DropDownList_REGIONAL.SelectedValue.ToString();
            cargar_DropDownList_DEPARTAMENTO(ID_REGIONAL);
            DropDownList_DEPARTAMENTO.Enabled = true;
            inhabilitar_DropDownList_CIUDAD();
        }
    }
    protected void DropDownList_DEPARTAMENTO_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_DEPARTAMENTO.SelectedValue == "")
        {
            inhabilitar_DropDownList_CIUDAD();
        }
        else
        {
            String ID_DEPARTAMENTO = DropDownList_DEPARTAMENTO.SelectedValue.ToString();
            String ID_REGIONAL = DropDownList_REGIONAL.SelectedValue.ToString();
            cargar_DropDownList_CIUDAD(ID_DEPARTAMENTO, ID_REGIONAL);
            DropDownList_CIUDAD.Enabled = true;
        }
    }
    protected void Button_VOLVER_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String ID_EMPRESA = QueryStringSeguro["reg"].ToString();

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
        QueryStringSeguro["img_area"] = "seleccion";
        QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
        QueryStringSeguro["nombre_modulo"] = "CONDICIONES DE ENVIO";
        QueryStringSeguro["accion"] = "inicial";
        QueryStringSeguro["reg"] = ID_EMPRESA.ToString();

        Response.Redirect("~/seleccion/condicionesSeleccion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }
}