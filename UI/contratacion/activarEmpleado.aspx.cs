using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;

using Brainsbits.LLB.comercial;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB.seleccion;
using Brainsbits.LLB.nomina;
using Brainsbits.LLB;

using TSHAK.Components;

using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html;
using iTextSharp.text.html.simpleparser;
using System.Configuration;
using System.Data.SqlClient;

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

    #region variables
    private enum Contratos
    {
        Ninguno=0,
        Indefinido,
        Integral,
        ObraLabor,
        Universitario,
        Sena
    }
    private enum Acciones
    {
        Inicio = 0,
        ConsultaNDatos = 1,
        ConsultaDatos = 2,
        Adiciona = 3,
        Guarda = 4,
        Visualiza = 6,
        Imprime = 7,
        Envia = 8,
        mensaje = 9,
        mensajes=10,
        Contratar,
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }

    private enum ClaseContrato
    {
        O_L = 0,
        IN,
        I,
        T_F,
        L_C_C_D_A,
        L_S_C_D_A
    }


    String tieneCuenta, numDocIdentidad;
    int idEmpresa, idSolicitud, idRequerimiento, idOcupacion;

    private enum Alineacion
    {
        normal = 0,
        centrada = 1,
        justificada = 2
    }

    private float ancho = 612;
    private float alto = 936;

    ReportDocument reporte;

    #endregion variables

    #region constructor
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = "ACTIVAR CONTRATO";

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Panel_MENSAJES.Visible = false;

        CargarQueryString();

        if (IsPostBack == false)
        {
            Configurar();

            String accion = QueryStringSeguro["accion"].ToString();
            if (accion == "inicial")
            {
                iniciar_interfaz_inicial();

                Panel_BOTONES_INTERNOS.Visible = true;
                cargar_menu_botones_modulos_internos(false);
            }
            else if (accion == "cargar")
            {
                Panel_Cargos_Por_Fuente.Visible = true;
                Panel_Informacion_Contrato.Visible = true;
                Panel_MENSAJES.Visible = false;

                registroContrato _contrato = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                DataTable tablaContratos = _contrato.ObtenerContratoPorNumeroIdentificacion(HiddenField_NUM_DOC_IDENTIDAD.Value);
                if (tablaContratos.Rows.Count == 0)
                {
                    Ocultar(Acciones.Contratar);
                    Decimal ID_REQUERIMIENTO = Convert.ToDecimal(HiddenField_ID_REQUISICION.Value);
                    Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);

                    radicacionHojasDeVida _solIngreso = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                    _solIngreso.ActualizarEstadoProcesoRegSolicitudesIngreso(Convert.ToInt32(ID_REQUERIMIENTO), Convert.ToInt32(ID_SOLICITUD), "ELABORAR CONTRATO", Session["USU_LOG"].ToString());

                    tablaContratos = _contrato.ObtenerDatosNuevoContrato(Convert.ToInt32(ID_REQUERIMIENTO), Convert.ToInt32(ID_SOLICITUD));
                    DataRow filaContrato = tablaContratos.Rows[0];
                    String nombre = filaContrato["NOMBRES"].ToString() + " " + filaContrato["APELLIDOS"].ToString();
                    String NUM_DOC_IDENTIDAD = filaContrato["TIP_DOC_IDENTIDAD"].ToString() + " " + filaContrato["NUM_DOC_IDENTIDAD"].ToString();

                    String Datos_persona = "Nombre: " + nombre + "<br> Numero Identificación: " + NUM_DOC_IDENTIDAD + "<br> Empresa: " + filaContrato["RAZ_SOCIAL"].ToString() + "<br>Cargo: " + filaContrato["NOM_OCUPACION"].ToString();
                    configurarInfoAdicionalModulo(true, Datos_persona);

                    cargar_menu_botones_modulos_internos(false);

                    requisicion _req = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                    DataTable tablaReq = _req.ObtenerComRequerimientoPorIdRequerimiento(ID_REQUERIMIENTO);
                    DataRow filaReq = tablaReq.Rows[0];

                    HiddenField_ID_PERFIL.Value = filaReq["REGISTRO_PERFIL"].ToString().Trim();

                    TextBox_Apellidos.Text = filaContrato["APELLIDOS"].ToString();
                    TextBox_Nombres.Text = filaContrato["NOMBRES"].ToString();
                    TextBox_doc_identidad.Text = filaContrato["TIP_DOC_IDENTIDAD"].ToString() + " " + filaContrato["NUM_DOC_IDENTIDAD"].ToString();

                    DateTime fechaInicio;

                    try
                    {
                        fechaInicio = Convert.ToDateTime(filaContrato["F_ING_C"]);
                    }
                    catch 
                    {
                        fechaInicio = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                    }

                    TextBox_fecha_inicio.Text = fechaInicio.ToShortDateString();
                    RangeValidator_TextBox_fecha_inicio.MinimumValue = DateTime.Now.ToShortDateString();
                    RangeValidator_TextBox_fecha_inicio.MaximumValue = DateTime.Now.AddDays(30).ToShortDateString();

                    TextBox_fecha_inicio.Enabled = true;

                    TextBox_Salario.Text = Convert.ToDecimal(filaContrato["SUELDO_C"]).ToString();
                    HiddenField_SUELDO.Value = filaContrato["SUELDO_C"].ToString();
                    TextBox_empresa.Text = filaContrato["RAZ_SOCIAL"].ToString();
                    TextBox_Cargo.Text = filaContrato["ID_Y_NOMBRE_CARGO"].ToString().Trim();

                    cargar_DropDownList_Clase_contrato();
                    cargar_DropDownList_tipo_Contrato();

                    cargar_DropDownList_FORMA_IMPRESION_CONTRATO(String.Empty);
                    DropDownList_FORMA_IMPRESION_CONTRATO.Enabled = false;
                    CheckBox_CON_CARNET_APARTE.Enabled = false;
                    CheckBox_CON_CARNET_APARTE.Checked = false;

                    cargar_DropDownList_Salario_integral();
                    cargar_DropDownList_SALARIO();

                    cargar_DropDownList_PERIODO_PAGO();

                    IniciarSeleccionDeUbicacion();

                    configurarBotonesDeAccion(false, true, true, false);
                }
                else
                {
                    DataRow filaContrato = tablaContratos.Rows[0];
                    HiddenField_ID_CONTRATO.Value += filaContrato["REGISTRO"].ToString();
                    HiddenField_persona.Value += "," + filaContrato["REGISTRO"].ToString() + ", " + filaContrato["ID_EMPLEADO"].ToString();
                    cargar_menu_botones_modulos_internos(false);

                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El empleado ya tiene contrato activo. Para realizar modificaciones por favor utlice el modulo de auditoría de contratos.", Proceso.Advertencia);

                    Panel_Informacion_Contrato.Visible = false;
                }
            }
        }
    }

    protected override void OnUnload(EventArgs e)
    {
        base.OnUnload(e);

        try
        {
            reporte.Dispose();
            reporte = null;
            reporte.Close();
        }
        catch
        {
        }

        try
        {
            GC.Collect();
        }
        catch
        {
        }
    }

    #endregion constructor

    #region metodos
    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Contratar:
                Panel_informacion_adicinal_contrato_indefinido.Visible = false;
                Panel_informacion_adicional_contrato_integral.Visible = false;
                Panel_informacion_adicional_aprendiz_universitario.Visible = false;
                Panel_informacion_adicinal_contrato_aprendiz_sena.Visible = false;
                break;
        }
    }

    private void Mostrar(Contratos contrato)
    {
        switch (contrato)
        {
            case Contratos.Ninguno:
                break;
            case Contratos.Indefinido:
                Panel_informacion_adicinal_contrato_indefinido.Visible = true;
                break;
            case Contratos.Integral:
                Panel_informacion_adicional_contrato_integral.Visible = true;
                break;
            case Contratos.ObraLabor:
                break;
            case Contratos.Universitario:
                Panel_informacion_adicional_aprendiz_universitario.Visible = true;
                break;
            case Contratos.Sena:
                Panel_informacion_adicinal_contrato_aprendiz_sena.Visible = true;
                break;
        }
    }
    #region cargar drops y grids
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

    private void cargar_DropDownList_Servicio(String id_perfil, String ciudad, int CC, int subcc)
    {
        Label_Servicio.Visible = true;

        DropDownList_servicio.Items.Clear();

        configuracionElementosPerfil _servicio = new configuracionElementosPerfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaServicio = _servicio.ObtenerConRegServicioPerfilServicio(Convert.ToInt32(id_perfil), ciudad, CC, subcc);
        System.Web.UI.WebControls.ListItem item;
        if (tablaServicio.Rows.Count <= 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encuentra configurado el servicio para este perfil.Verifique por favor.", Proceso.Advertencia);
        }
        else
        {
            foreach (DataRow fila in tablaServicio.Rows)
            {
                item = new System.Web.UI.WebControls.ListItem(fila["NOMBRE_SERVICIO"].ToString(), fila["ID_SERVICIO"].ToString());
                DropDownList_servicio.Items.Add(item);
            }
            DropDownList_servicio.DataBind();
        }
    }

    private void cargar_DropDownList_Salario_integral()
    {
        DropDownList_Salario_integral.Items.Clear();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Ninguno", "");
        DropDownList_Salario_integral.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("SI", "S");
        DropDownList_Salario_integral.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("No", "N");
        DropDownList_Salario_integral.Items.Add(item);
        DropDownList_Salario_integral.DataBind();
    }

    private void cargar_DropDownList_SALARIO()
    {
        DropDownList_DESCRIPCION_SALARIO.Items.Clear();
        parametro _parametro = new parametro(Session["idEmpresa"].ToString());
        DataTable tablaParametro = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_DESCRIPCION_SALARIO);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione Descripcion de salario", "");
        DropDownList_DESCRIPCION_SALARIO.Items.Add(item);

        foreach (DataRow fila in tablaParametro.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_DESCRIPCION_SALARIO.Items.Add(item);
        }

        DropDownList_DESCRIPCION_SALARIO.DataBind();
    }

    private void cargar_DropDownList_tipo_Contrato()
    {
        DropDownList_tipo_Contrato.Items.Clear();
        parametro _parametro = new parametro(Session["idEmpresa"].ToString());
        DataTable tablaParametro = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_TIPO_CONTRATO);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione Tipo Contrato", "");
        DropDownList_tipo_Contrato.Items.Add(item);

        foreach (DataRow fila in tablaParametro.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_tipo_Contrato.Items.Add(item);
        }

        DropDownList_tipo_Contrato.DataBind();
    }
    private void cargar_DropDownList_Clase_contrato()
    {
        DropDownList_Clase_contrato.Items.Clear();
            
        parametro _parametro = new parametro(Session["idEmpresa"].ToString());
        DataTable tablaParametro = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_CLASE_CONTRATO);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_Clase_contrato.Items.Add(item);

        foreach (DataRow fila in tablaParametro.Rows)
        {
            if (Session["idEmpresa"].ToString().Equals("1") & fila["VARIABLE"].ToString().Equals("TEMPORAL"))
            {
                item = new System.Web.UI.WebControls.ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
                DropDownList_Clase_contrato.Items.Add(item);
            }
            else if (Session["idEmpresa"].ToString().Equals("3") & fila["VARIABLE"].ToString().Equals("OUTSOURSING"))
            {
                item = new System.Web.UI.WebControls.ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
                DropDownList_Clase_contrato.Items.Add(item);
            }
        }

        DropDownList_Clase_contrato.DataBind();
    }
    private void cargar_DropDownList_PERIODO_PAGO()
    {
        DropDownList_PERIODO_PAGO.Items.Clear();
        parametro _parametro = new parametro(Session["idEmpresa"].ToString());
        DataTable tablaParametro = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_PERIODO_PAGO);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione Periodo Pago", "");
        DropDownList_PERIODO_PAGO.Items.Add(item);

        foreach (DataRow fila in tablaParametro.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_PERIODO_PAGO.Items.Add(item);
        }

        DropDownList_PERIODO_PAGO.DataBind();
    }

    #endregion

    #region metodos para configurar controles

    private void configurarBotonesDeAccion(Boolean bImprimir, Boolean bGuardar, Boolean bActivar, Boolean bClausulas)
    {
        Button_Imprimir.Visible = bImprimir;
        Button_Imprimir.Enabled = bImprimir;

        Button_Guardar.Visible = bGuardar;
        Button_Guardar.Enabled = bGuardar;
    }
    
    private void configurarInfoAdicionalModulo(Boolean ver, String texto)
    {
        Panel_INFO_ADICIONAL_MODULO.Visible = ver;
        Label_INFO_ADICIONAL_MODULO.Text = texto;
    }

    private void cargar_menu_botones_modulos_internos(Boolean cargar_otro)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = "contratacion";
        QueryStringSeguro["nombre_area"] = "CONTRATOS Y RELACIONES LABORALES";

        if (String.IsNullOrEmpty(HiddenField_persona.Value) == false)
        {
            String[] arrayDatosVariables = HiddenField_persona.Value.Split(',');
            QueryStringSeguro["empresa"] = arrayDatosVariables[3];
            QueryStringSeguro["solicitud"] = arrayDatosVariables[0];
            QueryStringSeguro["requerimiento"] = arrayDatosVariables[1];
            QueryStringSeguro["cargo"] = arrayDatosVariables[2];
            QueryStringSeguro["docID"] = arrayDatosVariables[4];
            if (arrayDatosVariables.Length > 5) QueryStringSeguro["ID_EMPLEADO"] = arrayDatosVariables[6];
        }

        TableRow filaTabla;
        TableCell celdaTabla;
        HyperLink link;
        System.Web.UI.WebControls.Image imagen;

        int contadorFilas = 0;

        filaTabla = new TableRow();
        filaTabla.ID = "row_" + contadorFilas.ToString();

        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_1_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_examenes";
        if (String.IsNullOrEmpty(HiddenField_persona.Value.ToString()))
        {
            QueryStringSeguro["accion"] = "inicial";
            QueryStringSeguro["nombre_modulo"] = "EXAMENES Y CUENTA BANCARIA";
            QueryStringSeguro["proceso"] = ((int)tabla.proceso.ContactoContratacion).ToString();
            link.NavigateUrl = "~/contratacion/examenesEmpleado.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        }
        else
        {
            if (cargar_otro == false)
            {
                QueryStringSeguro["accion"] = "cargar";
            }
            else
            {
                QueryStringSeguro["accion"] = "cargar_otro";
            }
            QueryStringSeguro["nombre_modulo"] = "EXAMENES Y CUENTA BANCARIA";
            QueryStringSeguro["persona"] = HiddenField_persona.Value;
            link.NavigateUrl = "~/contratacion/examenesEmpleado.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        }

        link.CssClass = "botones_menu_principal";
        imagen = new System.Web.UI.WebControls.Image();
        imagen.ImageUrl = "~/imagenes/areas/bExamenCuentaEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bExamenCuentaAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bExamenCuentaEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);


        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_2_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_afiliaciones";
        if (String.IsNullOrEmpty(HiddenField_persona.Value.ToString()))
        {
            QueryStringSeguro["accion"] = "inicial";
            QueryStringSeguro["nombre_modulo"] = "AFILIACIONES";
            link.NavigateUrl = "~/contratacion/afiliaciones.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        }
        else
        {
            QueryStringSeguro["accion"] = "cargar";
            QueryStringSeguro["nombre_modulo"] = "AFILIACIONES";
            QueryStringSeguro["persona"] = HiddenField_persona.Value;
            link.NavigateUrl = "~/contratacion/afiliaciones.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        }

        link.CssClass = "botones_menu_principal";
        imagen = new System.Web.UI.WebControls.Image();
        imagen.ImageUrl = "~/imagenes/areas/bAfiliacionesEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bAfiliacionesAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bAfiliacionesEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);

        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_3_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_elaborar_contrato";
        if (String.IsNullOrEmpty(HiddenField_persona.Value.ToString()))
        {
            QueryStringSeguro["accion"] = "inicial";
            QueryStringSeguro["nombre_modulo"] = "ELABORAR CONTRATO";
            link.NavigateUrl = "~/contratacion/activarEmpleado.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        }
        else
        {
            QueryStringSeguro["accion"] = "cargar";
            QueryStringSeguro["nombre_modulo"] = "ELABORAR CONTRATO";
            QueryStringSeguro["persona"] = HiddenField_persona.Value;
            link.NavigateUrl = "~/contratacion/activarEmpleado.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        }

        link.CssClass = "botones_menu_principal";
        imagen = new System.Web.UI.WebControls.Image();
        imagen.ImageUrl = "~/imagenes/areas/bElaborarContratoEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bElaborarContratoAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bElaborarContratoEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);


        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_4_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_servicios";
        if (!string.IsNullOrEmpty(HiddenField_ID_EMPLEADO.Value)) QueryStringSeguro["ID_EMPLEADO"] = HiddenField_ID_EMPLEADO.Value;
        if (String.IsNullOrEmpty(HiddenField_persona.Value.ToString()))
        {
            QueryStringSeguro["accion"] = "inicial";
            QueryStringSeguro["nombre_modulo"] = "SERVICIOS COMPLEMENTARIOS";
            link.NavigateUrl = "~/maestros/EntregasEmpleado.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        }
        else
        {
            if (cargar_otro == false)
            {
                QueryStringSeguro["accion"] = "cargar";
            }
            else
            {
                QueryStringSeguro["accion"] = "cargar_otro";
            }
            QueryStringSeguro["nombre_modulo"] = "SERVICIOS COMPLEMENTARIOS";
            QueryStringSeguro["persona"] = HiddenField_persona.Value;
            link.NavigateUrl = "~/maestros/EntregasEmpleado.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        }

        link.CssClass = "botones_menu_principal";
        imagen = new System.Web.UI.WebControls.Image();
        imagen.ImageUrl = "~/imagenes/areas/bServiciosComplementariosEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bServiciosComplementariosAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bServiciosComplementariosEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);


        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_5_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_volver";
        QueryStringSeguro["accion"] = "inicial";
        link.NavigateUrl = "~/contratacion/hojaTrabajoContratacion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        imagen = new System.Web.UI.WebControls.Image();
        imagen.ImageUrl = "~/imagenes/areas/bVolverHojaVidaEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bVolverHojaVidaAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bVolverHojaVidaEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);

        Table_MENU.Rows.Add(filaTabla);
    }

    #endregion

    #region metodos que se ejecutan al cargar la pagina

    private void iniciar_interfaz_inicial()
    {
        configurarInfoAdicionalModulo(false, "");

        Panel_BOTONES_MENU.Visible = false;

        Panel_Informacion_Contrato.Visible = false;
    }

    private void iniciar_interfaz_para_registro_existente()
    {
        Panel_Informacion_Contrato.Enabled = true;

        configurarBotonesDeAccion(true, true, true, true);

        Panel_Informacion_Contrato.Visible = true;

        Panel_MENSAJES.Visible = false;
    }
    #endregion

    private void cargar_DropDownList_FORMA_IMPRESION_CONTRATO(String CLASE_CONTRATO)
    {
        DropDownList_FORMA_IMPRESION_CONTRATO.Items.Clear();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione", "");
        DropDownList_FORMA_IMPRESION_CONTRATO.Items.Add(item);


        switch (CLASE_CONTRATO)
        {
            case "O_L":
                item = new System.Web.UI.WebControls.ListItem("PREIMPRESO", "PREIMPRESO");
                DropDownList_FORMA_IMPRESION_CONTRATO.Items.Add(item);
                item = new System.Web.UI.WebControls.ListItem("COMPLETO", "COMPLETO");
                DropDownList_FORMA_IMPRESION_CONTRATO.Items.Add(item);
                break;
            default:
                item = new System.Web.UI.WebControls.ListItem("COMPLETO", "COMPLETO");
                DropDownList_FORMA_IMPRESION_CONTRATO.Items.Add(item);
                break;
        }

        DropDownList_FORMA_IMPRESION_CONTRATO.DataBind();
    }

    private void cargar_DropDownList_CIUDAD(Decimal ID_EMPRESA)
    {
        DropDownList_Ciudad.Items.Clear();

        cobertura _cobertura = new cobertura(Session["idEmpresa"].ToString());
        DataTable tablaCoberturaEmpresa = _cobertura.obtenerCoberturaDeUnCliente(ID_EMPRESA);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_Ciudad.Items.Add(item);

        foreach (DataRow fila in tablaCoberturaEmpresa.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["Ciudad"].ToString(), fila["Código Ciudad"].ToString());
            DropDownList_Ciudad.Items.Add(item);
        }

        DropDownList_Ciudad.DataBind();
    }

    private void cargar_DropDownList_CENTRO_COSTO(Decimal ID_EMPRESA, String ID_CIUDAD)
    {
        DropDownList_CentroCosto.Items.Clear();

        centroCosto _centroCosto = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaCCCiudad = _centroCosto.ObtenerCentrosDeCostoPorIdEmpresaIdCiudad(ID_EMPRESA, ID_CIUDAD);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione", "");
        DropDownList_CentroCosto.Items.Add(item);

        foreach (DataRow fila in tablaCCCiudad.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOM_CC"].ToString(), fila["ID_CENTRO_C"].ToString());
            DropDownList_CentroCosto.Items.Add(item);
        }

        DropDownList_CentroCosto.DataBind();
    }

    private void cargar_DropDownList_SUB_CENTRO(Decimal ID_EMPRESA, Decimal ID_CENTRO_C)
    {
        DropDownList_sub_cc.Items.Clear();

        subCentroCosto _subCentroCosto = new subCentroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaSUB_C = _subCentroCosto.ObtenerSubCentrosDeCostoPorIdEmpresaIdCentroCosto(ID_EMPRESA, ID_CENTRO_C);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione", "");
        DropDownList_sub_cc.Items.Add(item);

        foreach (DataRow fila in tablaSUB_C.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOM_SUB_C"].ToString(), fila["ID_SUB_C"].ToString());
            DropDownList_sub_cc.Items.Add(item);
        }

        DropDownList_sub_cc.DataBind();
    }

    private void inhabilitar_DropDownList_SUB_CENTRO()
    {
        DropDownList_sub_cc.Enabled = false;
        DropDownList_sub_cc.Items.Clear();
        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_sub_cc.Items.Add(item);
        DropDownList_sub_cc.DataBind();
    }

    private void inhabilitar_DropDownList_CENTRO_COSTO()
    {
        DropDownList_CentroCosto.Enabled = false;
        DropDownList_CentroCosto.Items.Clear();
        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_CentroCosto.Items.Add(item);
        DropDownList_CentroCosto.DataBind();
    }

    private void inhabilitar_DropDownList_CIUDAD_TRABAJADOR()
    {
        DropDownList_Ciudad.Enabled = false;
        DropDownList_Ciudad.Items.Clear();
        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_Ciudad.Items.Add(item);
        DropDownList_Ciudad.DataBind();
    }

    private void limpiarDropDownList_Servicio()
    {
        DropDownList_servicio.Items.Clear();
        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_servicio.Items.Add(item);
        DropDownList_servicio.DataBind();
    }

    private void colorear_indicadores_de_ubicacion(Boolean ciudad, Boolean centro_c, Boolean sub_c, Boolean servicio, System.Drawing.Color color)
    {
        Label_CIUDAD_SELECCIONADA.BackColor = System.Drawing.Color.Transparent;
        Label_CC_SELECCIONADO.BackColor = System.Drawing.Color.Transparent;
        Label_SUBC_SELECCIONADO.BackColor = System.Drawing.Color.Transparent;
        Label_SERVICIO_SELECCIONADO.BackColor = System.Drawing.Color.Transparent;

        if (ciudad == true)
        {
            Label_CIUDAD_SELECCIONADA.BackColor = color;
        }

        if (centro_c == true)
        {
            Label_CC_SELECCIONADO.BackColor = color;
        }

        if (sub_c == true)
        {
            Label_SUBC_SELECCIONADO.BackColor = color;
        }

        if (servicio == true)
        {
            Label_SERVICIO_SELECCIONADO.BackColor = color;
        }
    }

    private void CargarUbicacionTrabajadorSegunTemporal(DataRow filaContratoTemporal)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        Decimal ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);

        condicionesContratacion _condicionesContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        if (DBNull.Value.Equals(filaContratoTemporal["ID_SUB_C"]) == false)
        {
            Decimal ID_SUB_C = Convert.ToDecimal(filaContratoTemporal["ID_SUB_C"]);

            subCentroCosto _sub = new subCentroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaSubCentro = _sub.ObtenerSubCentrosDeCostoPorIdSubCosto(ID_SUB_C);
            DataRow filaSub = tablaSubCentro.Rows[0];

            Decimal ID_CENTRO_C = Convert.ToDecimal(filaSub["ID_CENTRO_C"]);

            centroCosto _centro = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaCentro = _centro.ObtenerCentrosDeCostoPorIdCentroCosto(ID_CENTRO_C);
            DataRow filaCentro = tablaCentro.Rows[0];

            String ID_CIUDAD = filaCentro["ID_CIUDAD"].ToString().Trim();

            cargar_DropDownList_SUB_CENTRO(ID_EMPRESA, ID_CENTRO_C);
            DropDownList_sub_cc.SelectedValue = ID_SUB_C.ToString();
            DropDownList_sub_cc.Enabled = true;

            cargar_DropDownList_CENTRO_COSTO(ID_EMPRESA, ID_CIUDAD);
            DropDownList_CentroCosto.SelectedValue = ID_CENTRO_C.ToString();
            DropDownList_CentroCosto.Enabled = true;

            cargar_DropDownList_CIUDAD(ID_EMPRESA);
            DropDownList_Ciudad.SelectedValue = ID_CIUDAD;
            DropDownList_Ciudad.Enabled = true;

            if (Session["idEmpresa"].ToString() == "1")
            {
                DataTable tablaCondicionContratacion = _condicionesContratacion.ObtenerCondicionComercialPorIdPerfilIdSubC(ID_PERFIL, ID_SUB_C);

                if (tablaCondicionContratacion.Rows.Count <= 0)
                {
                    colorear_indicadores_de_ubicacion(false, false, true, false, System.Drawing.Color.Red);
                    HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";

                    Label_Riesgo.Text = "Riesgo: Desconocido.";
                    TextBox_Doc_Entregar.Text = "";
                    TextBox_Req_usuario.Text = "";
                }
                else
                {
                    colorear_indicadores_de_ubicacion(false, false, true, false, System.Drawing.Color.Green);
                    HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "S";

                    DataRow filaCondicion = tablaCondicionContratacion.Rows[0];

                    Label_Riesgo.Text = "Riesgo: " + filaCondicion["VALOR_RIESGO"].ToString().Trim(); ;
                    TextBox_Doc_Entregar.Text = filaCondicion["DOC_TRAB"].ToString().Trim();
                    TextBox_Req_usuario.Text = filaCondicion["OBS_CTE"].ToString().Trim();
                }

                Label_Servicio.Visible = false;
                Label_SERVICIO_SELECCIONADO.Visible = false;
            }
            else
            {
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";

                cargar_DropDownList_SERVICIO_sub_c(ID_SUB_C);

                colorear_indicadores_de_ubicacion(false, false, false, true, System.Drawing.Color.Red);

                Label_Servicio.Visible = true;
                Label_SERVICIO_SELECCIONADO.Visible = true;
            }
        }
        else
        {
            if (DBNull.Value.Equals(filaContratoTemporal["ID_CENTRO_C"]) == false)
            {
                Decimal ID_CENTRO_C = Convert.ToDecimal(filaContratoTemporal["ID_CENTRO_C"]);

                centroCosto _centro = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                DataTable tablaCentro = _centro.ObtenerCentrosDeCostoPorIdCentroCosto(ID_CENTRO_C);
                DataRow filaCentro = tablaCentro.Rows[0];

                String ID_CIUDAD = filaCentro["ID_CIUDAD"].ToString().Trim();

                inhabilitar_DropDownList_SUB_CENTRO();
                DropDownList_sub_cc.Enabled = true;

                cargar_DropDownList_CENTRO_COSTO(ID_EMPRESA, ID_CIUDAD);
                DropDownList_CentroCosto.SelectedValue = ID_CENTRO_C.ToString();
                DropDownList_CentroCosto.Enabled = true;

                cargar_DropDownList_CIUDAD(ID_EMPRESA);
                DropDownList_Ciudad.SelectedValue = ID_CIUDAD;
                DropDownList_Ciudad.Enabled = true;

                if (Session["idEmpresa"].ToString() == "1")
                {
                    DataTable tablaCondicionContratacion = _condicionesContratacion.ObtenerCondicionComercialPorIdPerfilIdCentroC(ID_PERFIL, ID_CENTRO_C);

                    if (tablaCondicionContratacion.Rows.Count <= 0)
                    {
                        colorear_indicadores_de_ubicacion(false, true, false, false, System.Drawing.Color.Red);
                        HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";

                        Label_Riesgo.Text = "Riesgo: Desconocido.";
                        TextBox_Doc_Entregar.Text = "";
                        TextBox_Req_usuario.Text = "";
                    }
                    else
                    {
                        colorear_indicadores_de_ubicacion(false, true, false, false, System.Drawing.Color.Green);
                        HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "S";

                        DataRow filaCondicion = tablaCondicionContratacion.Rows[0];

                        Label_Riesgo.Text = "Riesgo: " + filaCondicion["VALOR_RIESGO"].ToString().Trim(); ;
                        TextBox_Doc_Entregar.Text = filaCondicion["DOC_TRAB"].ToString().Trim();
                        TextBox_Req_usuario.Text = filaCondicion["OBS_CTE"].ToString().Trim();
                    }

                    Label_Servicio.Visible = false;
                    Label_SERVICIO_SELECCIONADO.Visible = false;
                }
                else
                {
                    HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";

                    cargar_DropDownList_SERVICIO_centro_c(ID_CENTRO_C);

                    colorear_indicadores_de_ubicacion(false, false, false, true, System.Drawing.Color.Red);

                    Label_Servicio.Visible = true;
                    Label_SERVICIO_SELECCIONADO.Visible = true;
                }
            }
            else
            {
                String ID_CIUDAD = filaContratoTemporal["ID_CIUDAD"].ToString().Trim();

                inhabilitar_DropDownList_SUB_CENTRO();
                DropDownList_sub_cc.Enabled = true;

                inhabilitar_DropDownList_CENTRO_COSTO();
                DropDownList_CentroCosto.Enabled = true;

                cargar_DropDownList_CIUDAD(ID_EMPRESA);
                DropDownList_Ciudad.SelectedValue = ID_CIUDAD;
                DropDownList_Ciudad.Enabled = true;

                if (Session["idEmpresa"].ToString() == "1")
                {
                    DataTable tablaCondicionContratacion = _condicionesContratacion.ObtenerCondicionComercialPorIdPerfilIdCiudad(ID_PERFIL, ID_CIUDAD);

                    if (tablaCondicionContratacion.Rows.Count <= 0)
                    {
                        colorear_indicadores_de_ubicacion(true, false, false, false, System.Drawing.Color.Red);
                        HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";

                        Label_Riesgo.Text = "Riesgo: Desconocido.";
                        TextBox_Doc_Entregar.Text = "";
                        TextBox_Req_usuario.Text = "";
                    }
                    else
                    {
                        colorear_indicadores_de_ubicacion(true, false, false, false, System.Drawing.Color.Green);
                        HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "S";

                        DataRow filaCondicion = tablaCondicionContratacion.Rows[0];

                        Label_Riesgo.Text = "Riesgo: " + filaCondicion["VALOR_RIESGO"].ToString().Trim(); ;
                        TextBox_Doc_Entregar.Text = filaCondicion["DOC_TRAB"].ToString().Trim();
                        TextBox_Req_usuario.Text = filaCondicion["OBS_CTE"].ToString().Trim();
                    }

                    Label_Servicio.Visible = false;
                    Label_SERVICIO_SELECCIONADO.Visible = false;
                }
                else
                {
                    HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";

                    cargar_DropDownList_SERVICIO_ciudad(ID_CIUDAD, ID_EMPRESA);

                    colorear_indicadores_de_ubicacion(false, false, false, true, System.Drawing.Color.Red);

                    Label_Servicio.Visible = true;
                    Label_SERVICIO_SELECCIONADO.Visible = true;
                }
            }
        }
    }

    private void IniciarSeleccionDeUbicacion()
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        Decimal ID_REQUERIMIENTO = Convert.ToDecimal(HiddenField_ID_REQUISICION.Value);
        Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);

        ConRegContratoTemporal _contratoTemporal = new ConRegContratoTemporal(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaCOntratoTemporal = _contratoTemporal.ObtenerConRegContratosTemporalPorIdRequerimientoIdSolicitud(ID_REQUERIMIENTO, ID_SOLICITUD);

        if (tablaCOntratoTemporal.Rows.Count <= 0)
        {
            cargar_DropDownList_CIUDAD(ID_EMPRESA);
            inhabilitar_DropDownList_CENTRO_COSTO();
            inhabilitar_DropDownList_SUB_CENTRO();
            limpiarDropDownList_Servicio();

            HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";

            if (Session["idEmpresa"].ToString().Trim() == "1")
            {
                colorear_indicadores_de_ubicacion(false, false, false, false, System.Drawing.Color.Transparent);

                Label_Servicio.Visible = false;
                Label_SERVICIO_SELECCIONADO.Visible = false;
            }
            else
            {
                colorear_indicadores_de_ubicacion(false, false, false, true, System.Drawing.Color.Red);

                Label_Servicio.Visible = true;
                Label_SERVICIO_SELECCIONADO.Visible = true;
            }

            Label_Riesgo.Text = "Riesgo: Desconocido.";

            TextBox_Req_usuario.Text = "";
            TextBox_Doc_Entregar.Text = "";
        }
        else
        {
            DataRow filaContratoTemporal = tablaCOntratoTemporal.Rows[0];
            CargarUbicacionTrabajadorSegunTemporal(filaContratoTemporal);
        }
    }

    private void CargarQueryString()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        HiddenField_ID_EMPRESA.Value = QueryStringSeguro["empresa"];
        HiddenField_ID_SOLICITUD.Value = QueryStringSeguro["solicitud"];
        HiddenField_ID_REQUISICION.Value = QueryStringSeguro["requerimiento"];
        HiddenField_ID_OCUPACION.Value = QueryStringSeguro["cargo"];
        HiddenField_NUM_DOC_IDENTIDAD.Value = QueryStringSeguro["docID"];

        HiddenField_persona.Value = QueryStringSeguro["persona"];
    }

    private void configurar_paneles_popup(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_mensaje.CssClass = "panel_popup";
    }

    private void Configurar()
    {
        configurar_paneles_popup(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    private void ocultar_mensaje(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.Style.Add("display", "none");
        panel_mensaje.Style.Add("display", "none");

        panel_fondo.Visible = false;
        panel_mensaje.Visible = false;
    }

    private void Cargar(DataTable dataTable, GridView gridView)
    {
        tools _tools = new tools();
        SecureQueryString secureQueryString;
        secureQueryString = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        gridView.DataSource = dataTable;
        gridView.DataBind();

        for (int i = 0; i < dataTable.Rows.Count; i++)
        {
            DataRow dataRow = dataTable.Rows[i];
            if (!DBNull.Value.Equals(dataRow["ID_CLAUSULA"])) 
            {
                gridView.Rows[i].Cells[0] .Text = dataRow["ID_CLAUSULA"].ToString();
                secureQueryString["ID_CLAUSULA"] = dataRow["ID_CLAUSULA"].ToString();
            }

            if (!DBNull.Value.Equals(dataRow["ID_EMPLEADO"]))
            {
                gridView.Rows[i].Cells[1].Text = dataRow["ID_EMPLEADO"].ToString();
                secureQueryString["ID_EMPLEADO"] = dataRow["ID_EMPLEADO"].ToString();
            }
            if (!string.IsNullOrEmpty(dataRow["TIPO_CLAUSULA"].ToString())) gridView.Rows[i].Cells[2].Text = dataRow["TIPO_CLAUSULA"].ToString();
            if (!string.IsNullOrEmpty(dataRow["DESCRIPCION"].ToString())) gridView.Rows[i].Cells[3].Text = dataRow["DESCRIPCION"].ToString();
            
            HyperLink hyperLink = gridView.Rows[i].FindControl("HyperLink_ARCHIVO") as HyperLink;

            if (!DBNull.Value.Equals(dataRow["ARCHIVO"]))
            {
                hyperLink.NavigateUrl = "~/contratacion/VisorDocumentosClausulasContratacion.aspx?data=" + HttpUtility.UrlEncode(secureQueryString.ToString());
            }
            else hyperLink.Enabled = false;
        }
    }
    #endregion metodos

    #region eventos

    private void cargar_DropDownList_SERVICIO_ciudad(String ID_CIUDAD, Decimal ID_EMPRESA)
    {
        DropDownList_servicio.Items.Clear();

        condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaServicios = _condicionComercial.ObtenerServiciosPorEmpresaPorIdCiudad(ID_CIUDAD, ID_EMPRESA);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_servicio.Items.Add(item);

        foreach (DataRow fila in tablaServicios.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOMBRE_SERVICIO"].ToString(), fila["ID_SERVICIO"].ToString());
            DropDownList_servicio.Items.Add(item);
        }

        DropDownList_servicio.DataBind();
    }

    private void cargar_DropDownList_SERVICIO_centro_c(Decimal ID_CENTRO_C)
    {
        DropDownList_servicio.Items.Clear();

        condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaServicios = _condicionComercial.ObtenerServiciosPorEmpresaPorIdCentroC(ID_CENTRO_C);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_servicio.Items.Add(item);

        foreach (DataRow fila in tablaServicios.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOMBRE_SERVICIO"].ToString(), fila["ID_SERVICIO"].ToString());
            DropDownList_servicio.Items.Add(item);
        }

        DropDownList_servicio.DataBind();
    }

    private void cargar_DropDownList_SERVICIO_sub_c(Decimal ID_SUB_C)
    {
        DropDownList_servicio.Items.Clear();

        condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaServicios = _condicionComercial.ObtenerServiciosPorEmpresaPorIdSubC(ID_SUB_C);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_servicio.Items.Add(item);

        foreach (DataRow fila in tablaServicios.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOMBRE_SERVICIO"].ToString(), fila["ID_SERVICIO"].ToString());
            DropDownList_servicio.Items.Add(item);
        }

        DropDownList_servicio.DataBind();
    }

    protected void DropDownList_Ciudad_SelectedIndexChanged(object sender, EventArgs e)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        Decimal ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);

        if (DropDownList_Ciudad.SelectedIndex <= 0)
        {
            HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";
            inhabilitar_DropDownList_CENTRO_COSTO();
            inhabilitar_DropDownList_SUB_CENTRO();
            limpiarDropDownList_Servicio();

            Label_Riesgo.Text = "Riesgo: Desconocido.";
            TextBox_Doc_Entregar.Text = "";
            TextBox_Req_usuario.Text = "";

            if (Session["idEmpresa"].ToString() == "1")
            {
                colorear_indicadores_de_ubicacion(false, false, false, false, System.Drawing.Color.Transparent);
                Label_Servicio.Visible = false;
                Label_SERVICIO_SELECCIONADO.Visible = false;
            }
            else
            {
                colorear_indicadores_de_ubicacion(false, false, false, true, System.Drawing.Color.Red);
                Label_Servicio.Visible = true;
                Label_SERVICIO_SELECCIONADO.Visible = true;
            }
        }
        else
        {
            condicionesContratacion _condicionesContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            
            String ID_CIUDAD = DropDownList_Ciudad.SelectedValue;
            
            cargar_DropDownList_CENTRO_COSTO(ID_EMPRESA, ID_CIUDAD);
            DropDownList_CentroCosto.Enabled = true;
            inhabilitar_DropDownList_SUB_CENTRO();

            if (Session["idEmpresa"].ToString() == "1")
            {
                DataTable tablaCondicionContratacion = _condicionesContratacion.ObtenerCondicionComercialPorIdPerfilIdCiudad(ID_PERFIL, ID_CIUDAD);

                if (tablaCondicionContratacion.Rows.Count <= 0)
                {
                    colorear_indicadores_de_ubicacion(true, false, false, false, System.Drawing.Color.Red);
                    HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";

                    Label_Riesgo.Text = "Riesgo: Desconocido.";
                    TextBox_Doc_Entregar.Text = "";
                    TextBox_Req_usuario.Text = "";
                }
                else
                {
                    colorear_indicadores_de_ubicacion(true, false, false, false, System.Drawing.Color.Green);
                    HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "S";

                    DataRow filaCondicion = tablaCondicionContratacion.Rows[0];

                    Label_Riesgo.Text = "Riesgo: " + filaCondicion["VALOR_RIESGO"].ToString().Trim();
                    TextBox_Doc_Entregar.Text = filaCondicion["DOC_TRAB"].ToString().Trim();
                    TextBox_Req_usuario.Text = filaCondicion["OBS_CTE"].ToString().Trim();
                }

                Label_Servicio.Visible = false;
                Label_SERVICIO_SELECCIONADO.Visible = false;
            }
            else
            {
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";

                cargar_DropDownList_SERVICIO_ciudad(ID_CIUDAD, ID_EMPRESA);

                colorear_indicadores_de_ubicacion(false, false, false, true, System.Drawing.Color.Red);

                Label_Servicio.Visible = true;
                Label_SERVICIO_SELECCIONADO.Visible = true;
            }
        }
    }
    
    protected void DropDownList_CentroCosto_SelectedIndexChanged(object sender, EventArgs e)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        Decimal ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);
        String ID_CIUDAD = DropDownList_Ciudad.SelectedValue;

        condicionesContratacion _condicionesContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        if (DropDownList_CentroCosto.SelectedIndex <= 0)
        {
            inhabilitar_DropDownList_SUB_CENTRO();

            if (Session["idEmpresa"].ToString() == "1")
            {
                DataTable tablaCondicionContratacion = _condicionesContratacion.ObtenerCondicionComercialPorIdPerfilIdCiudad(ID_PERFIL, ID_CIUDAD);

                if (tablaCondicionContratacion.Rows.Count <= 0)
                {
                    colorear_indicadores_de_ubicacion(true, false, false, false, System.Drawing.Color.Red);
                    HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";

                    Label_Riesgo.Text = "Riesgo: Desconocido.";
                    TextBox_Doc_Entregar.Text = "";
                    TextBox_Req_usuario.Text = "";
                }
                else
                {
                    colorear_indicadores_de_ubicacion(true, false, false, false, System.Drawing.Color.Green);
                    HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "S";

                    DataRow filaCondicion = tablaCondicionContratacion.Rows[0];

                    Label_Riesgo.Text = "Riesgo: " + filaCondicion["VALOR_RIESGO"].ToString().Trim();
                    TextBox_Doc_Entregar.Text = filaCondicion["DOC_TRAB"].ToString().Trim();
                    TextBox_Req_usuario.Text = filaCondicion["OBS_CTE"].ToString().Trim();
                }

                Label_Servicio.Visible = false;
                Label_SERVICIO_SELECCIONADO.Visible = false;
            }
            else
            {
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";

                cargar_DropDownList_SERVICIO_ciudad(ID_CIUDAD, ID_EMPRESA);

                colorear_indicadores_de_ubicacion(false, false, false, true, System.Drawing.Color.Red);

                Label_Servicio.Visible = true;
                Label_SERVICIO_SELECCIONADO.Visible = true;
            }
        }
        else
        {
            Decimal ID_CENTRO_C = Convert.ToDecimal(DropDownList_CentroCosto.SelectedValue);

            cargar_DropDownList_SUB_CENTRO(ID_EMPRESA, ID_CENTRO_C);
            DropDownList_sub_cc.Enabled = true;

            if (Session["idEmpresa"].ToString() == "1")
            {
                DataTable tablaCondicionContratacion = _condicionesContratacion.ObtenerCondicionComercialPorIdPerfilIdCentroC(ID_PERFIL, ID_CENTRO_C);

                if (tablaCondicionContratacion.Rows.Count <= 0)
                {
                    colorear_indicadores_de_ubicacion(false, true, false, false, System.Drawing.Color.Red);
                    HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";

                    Label_Riesgo.Text = "Riesgo: Desconocido.";
                    TextBox_Doc_Entregar.Text = "";
                    TextBox_Req_usuario.Text = "";
                }
                else
                {
                    colorear_indicadores_de_ubicacion(false, true, false, false, System.Drawing.Color.Green);
                    HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "S";

                    DataRow filaCondicion = tablaCondicionContratacion.Rows[0];

                    Label_Riesgo.Text = "Riesgo: " + filaCondicion["VALOR_RIESGO"].ToString().Trim();
                    TextBox_Doc_Entregar.Text = filaCondicion["DOC_TRAB"].ToString().Trim();
                    TextBox_Req_usuario.Text = filaCondicion["OBS_CTE"].ToString().Trim();
                }

                Label_Servicio.Visible = false;
                Label_SERVICIO_SELECCIONADO.Visible = false;
            }
            else
            {
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";

                cargar_DropDownList_SERVICIO_centro_c(ID_CENTRO_C);

                colorear_indicadores_de_ubicacion(false, false, false, true, System.Drawing.Color.Red);

                Label_Servicio.Visible = true;
                Label_SERVICIO_SELECCIONADO.Visible = true;
            }
        }
    }

    protected void DropDownList_Sub_CC_SelectedIndexChanged(object sender, EventArgs e)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        Decimal ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);
        String ID_CIUDAD = DropDownList_sub_cc.SelectedValue;
        Decimal ID_CENTRO_C = Convert.ToDecimal(DropDownList_CentroCosto.SelectedValue);

        condicionesContratacion _condicionesContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        if (DropDownList_sub_cc.SelectedIndex <= 0)
        {
            if (Session["idEmpresa"].ToString() == "1")
            {
                DataTable tablaCondicionContratacion = _condicionesContratacion.ObtenerCondicionComercialPorIdPerfilIdCentroC(ID_PERFIL, ID_CENTRO_C);

                if (tablaCondicionContratacion.Rows.Count <= 0)
                {
                    colorear_indicadores_de_ubicacion(false, true, false, false, System.Drawing.Color.Red);
                    HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";
                    
                    Label_Riesgo.Text = "Riesgo: Desconocido.";
                    TextBox_Doc_Entregar.Text = "";
                    TextBox_Req_usuario.Text = "";
                }
                else
                {
                    colorear_indicadores_de_ubicacion(false, true, false, false, System.Drawing.Color.Green);
                    HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "S";

                    DataRow filaCondicion = tablaCondicionContratacion.Rows[0];

                    Label_Riesgo.Text = "Riesgo: " + filaCondicion["VALOR_RIESGO"].ToString().Trim();
                    TextBox_Doc_Entregar.Text = filaCondicion["DOC_TRAB"].ToString().Trim();
                    TextBox_Req_usuario.Text = filaCondicion["OBS_CTE"].ToString().Trim();
                }

                Label_Servicio.Visible = false;
                Label_SERVICIO_SELECCIONADO.Visible = false;
            }
            else
            {
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";

                cargar_DropDownList_SERVICIO_centro_c(ID_CENTRO_C);

                colorear_indicadores_de_ubicacion(false, false, false, true, System.Drawing.Color.Red);

                Label_Servicio.Visible = true;
                Label_SERVICIO_SELECCIONADO.Visible = true;
            }
        }
        else
        {
            Decimal ID_SUB_C = Convert.ToDecimal(DropDownList_sub_cc.SelectedValue);

            if (Session["idEmpresa"].ToString() == "1")
            {
                DataTable tablaCondicionContratacion = _condicionesContratacion.ObtenerCondicionComercialPorIdPerfilIdSubC(ID_PERFIL, ID_SUB_C);

                if (tablaCondicionContratacion.Rows.Count <= 0)
                {
                    colorear_indicadores_de_ubicacion(false, false, true, false, System.Drawing.Color.Red);
                    HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";

                    Label_Riesgo.Text = "Riesgo: Desconocido.";
                    TextBox_Doc_Entregar.Text = "";
                    TextBox_Req_usuario.Text = "";
                }
                else
                {
                    colorear_indicadores_de_ubicacion(false, false, true, false, System.Drawing.Color.Green);
                    HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "S";

                    DataRow filaCondicion = tablaCondicionContratacion.Rows[0];

                    Label_Riesgo.Text = "Riesgo: " + filaCondicion["VALOR_RIESGO"].ToString().Trim();
                    TextBox_Doc_Entregar.Text = filaCondicion["DOC_TRAB"].ToString().Trim();
                    TextBox_Req_usuario.Text = filaCondicion["OBS_CTE"].ToString().Trim();
                }

                Label_Servicio.Visible = false;
                Label_SERVICIO_SELECCIONADO.Visible = false;
            }
            else
            {
                HiddenField_SELECCION_ITEM_CON_CONDICIONES_CONTRATACION.Value = "N";

                cargar_DropDownList_SERVICIO_sub_c(ID_SUB_C);

                colorear_indicadores_de_ubicacion(false, false, false, true, System.Drawing.Color.Red);

                Label_Servicio.Visible = true;
                Label_SERVICIO_SELECCIONADO.Visible = true;
            }
        }
    }

    protected void DropDownList_Clase_contrato_SelectedIndexChanged(object sender, EventArgs e)
    {
        TextBox_Salario.Text = Convert.ToDecimal(HiddenField_SUELDO.Value).ToString();
        TextBox_Salario.Enabled = true;
        RadioButton_ninguno.Checked = true;
        RadioButton_SENA_PRODUCTIVO.Checked = false;
        RadioButton_SENA_ELECTIVO.Checked = false;
        RadioButton_PRACTICANTE_UNI.Checked = false;
        DateTime fecha_inicio;

        if (DropDownList_Clase_contrato.SelectedItem.Text.Equals("INDEFINIDO") ||
            DropDownList_Clase_contrato.SelectedItem.Text.Equals("LABOR SIN CORTE DE AÑO CON VACACIONES"))
        {

            Ocultar(Acciones.Contratar);
            Mostrar(Contratos.Indefinido);

            TextBox_fecha_terminacion.Text = null;

        }
        else if (DropDownList_Clase_contrato.SelectedItem.Text.Equals("OBRA O LABOR") ||
            DropDownList_Clase_contrato.SelectedItem.Text.Equals("LABOR CON CORTE DE AÑO"))
        {
            Ocultar(Acciones.Contratar);
            Mostrar(Contratos.ObraLabor);

            fecha_inicio = Convert.ToDateTime(TextBox_fecha_inicio.Text);
            TextBox_fecha_terminacion.Text = fecha_inicio.AddYears(1).AddDays(-1).ToShortDateString();
        }
        else if (DropDownList_Clase_contrato.SelectedItem.Text.Equals("INTEGRAL"))
        {
            Ocultar(Acciones.Contratar);
            Mostrar(Contratos.Integral);
            TextBox_fecha_terminacion.Text = "";
            fecha_inicio = Convert.ToDateTime(TextBox_fecha_inicio.Text);
            TextBox_fecha_terminacion.Text = fecha_inicio.AddYears(1).AddDays(-1).ToShortDateString();
        }
        else
        {
            TextBox_fecha_terminacion.Text = "";
        }

        if (DropDownList_Clase_contrato.SelectedIndex <= 0)
        {
            cargar_DropDownList_FORMA_IMPRESION_CONTRATO(String.Empty);
            DropDownList_FORMA_IMPRESION_CONTRATO.Enabled = false;
            CheckBox_CON_CARNET_APARTE.Enabled = false;
            CheckBox_CON_CARNET_APARTE.Checked = false;
        }
        else
        {
            cargar_DropDownList_FORMA_IMPRESION_CONTRATO(DropDownList_Clase_contrato.SelectedValue);
            DropDownList_FORMA_IMPRESION_CONTRATO.Enabled = true;
            CheckBox_CON_CARNET_APARTE.Enabled = true;
            CheckBox_CON_CARNET_APARTE.Checked = false;
        }
    }

    private void ImprimirContratoO_L_COMPLETO(DataRow filaInfoContrato)
    {
        tools _tools = new tools();

        Boolean CarnetIncluido = CheckBox_CON_CARNET_APARTE.Checked;

        StreamReader archivo;

        if (Session["idEmpresa"].ToString() == "1")
        {
            if (CarnetIncluido == true)
            {
                archivo = new StreamReader(Server.MapPath(@"~\plantillas_reportes\contrato_sertempo_obra_labor.htm"));
            }
            else
            {
                archivo = new StreamReader(Server.MapPath(@"~\plantillas_reportes\contrato_sertempo_obra_labor_carnet_aparte.htm"));
            }
        }
        else
        {
            if (CarnetIncluido == true)
            {
                archivo = new StreamReader(Server.MapPath(@"~\plantillas_reportes\contrato_eys_labor_contratada.htm"));
            }
            else
            {
                archivo = new StreamReader(Server.MapPath(@"~\plantillas_reportes\contrato_eys_labor_contratada_carnet_aparte.htm"));
            }
        }


        String html = archivo.ReadToEnd();

        archivo.Dispose();
        archivo.Close();

        String filename;

        if (Session["idEmpresa"].ToString() == "1")
        {
            html = html.Replace("[DIR_LOGO_EMPLEADOR]", tabla.DIR_IMAGENES_PARA_PDF + "/logo_sertempo.png");
            html = html.Replace("[MENSAJE_LOGO]", "SERVICIOS TEMPORALES PROFESIONALES");
            html = html.Replace("[NOMBRE_EMPLEADOR]", tabla.VAR_NOMBRE_SERTEMPO);
            html = html.Replace("[DOMICILO_EMPLEADOR]", tabla.VAR_DOMICILIO_SERTEMPO);
            html = html.Replace("[DESCRIPCION_CARGO]", filaInfoContrato["DSC_FUNCIONES"].ToString().Trim().ToUpper());
            html = html.Replace("[SERVICIO_RESPECTIVO]", filaInfoContrato["DESCRIPCION"].ToString().Trim().ToUpper());
            html = html.Replace("[EMPRESA_USUARIA]", filaInfoContrato["RAZ_SOCIAL"].ToString().Trim().ToUpper());
            html = html.Replace("[DESCRIPCION_SALARIO]", filaInfoContrato["DESCRIPCION_SALARIO"].ToString().Trim().ToUpper());
            html = html.Replace("[DIR_FIRMA_EMPLEADOR]", tabla.DIR_IMAGENES_PARA_PDF + "/firma_autos_recomendacion.jpg");
            filename = "contrato_sertempo";
        }
        else
        {
            html = html.Replace("[DIR_LOGO_EMPLEADOR]", tabla.DIR_IMAGENES_PARA_PDF + "/logo_eficiencia.jpg");
            html = html.Replace("[NOMBRE_EMPLEADOR]", tabla.VAR_NOMBRE_EYS);
            html = html.Replace("[DOMICILO_EMPLEADOR]", tabla.VAR_DOMICILIO_EYS);
            html = html.Replace("[FUNCION_CARGO]", filaInfoContrato["DSC_FUNCIONES"].ToString().Trim().ToUpper());
            html = html.Replace("[ACTIVIDAD_CONTRATADA]", filaInfoContrato["DSC_FUNCIONES"].ToString().Trim().ToUpper());
            html = html.Replace("[EMPRESA_DESTACA]", filaInfoContrato["RAZ_SOCIAL"].ToString().Trim().ToUpper());
            html = html.Replace("[DESCRIPCION_SALARIO]", filaInfoContrato["DESCRIPCION_SALARIO"].ToString().Trim().ToUpper());
            html = html.Replace("[DIR_FIRMA_EMPLEADOR]", tabla.DIR_IMAGENES_PARA_PDF + "/firma_autos_recomendacion.jpg");
            filename = "contrato_eys";
        }


        html = html.Replace("[CARGO_TRABAJADOR]", filaInfoContrato["NOM_OCUPACION"].ToString().Trim().ToUpper());
        html = html.Replace("[NOMBRE_TRABAJADOR]", filaInfoContrato["APELLIDOS"].ToString().Trim().ToUpper() + " " + filaInfoContrato["NOMBRES"].ToString().Trim().ToUpper());
        html = html.Replace("[TIPO_DOCUMENTO_IDENTIDAD]", filaInfoContrato["TIP_DOC_IDENTIDAD"].ToString().Trim().ToUpper());
        html = html.Replace("[DOC_IDENTIFICACION]", filaInfoContrato["NUM_DOC_IDENTIDAD"].ToString().Trim().ToUpper());
        html = html.Replace("[SALARIO]", String.Format("$ {0:N2}", Convert.ToDecimal(filaInfoContrato["SALARIO"]).ToString()));
        html = html.Replace("[PERIODO_PAGO]", DropDownList_PERIODO_PAGO.SelectedItem.Text);
        html = html.Replace("[FECHA_INICIACION]", Convert.ToDateTime(filaInfoContrato["FECHA_INICIA"]).ToLongDateString());
        html = html.Replace("[CARNE_VALIDO_HASTA]", Convert.ToDateTime(filaInfoContrato["FECHA_TERMINA"]).ToLongDateString());

        usuario _usuario = new usuario(Session["idEmpresa"].ToString());
        DataTable tablaInfoUsuario = _usuario.ObtenerInicioSesionPorUsuLog(Session["USU_LOG"].ToString());
        if (tablaInfoUsuario.Rows.Count <= 0)
        {
            html = html.Replace("[CIUDAD_FIRMA]", "Desconocida");
        }
        else
        {
            DataRow filaInfoUsuario = tablaInfoUsuario.Rows[0];
            html = html.Replace("[CIUDAD_FIRMA]", filaInfoUsuario["NOMBRE_CIUDAD"].ToString());
        }

        DateTime fechaHoy = DateTime.Now;
        html = html.Replace("[DIAS_FIRMA]", fechaHoy.Day.ToString());
        html = html.Replace("[MES_FIRMA]", _tools.obtenerNombreMes(fechaHoy.Month));
        html = html.Replace("[ANNO_FIRMA]", fechaHoy.Year.ToString());

        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;FileName=" + filename + ".pdf");

        Response.Clear();
        Response.ContentType = "application/pdf";


        iTextSharp.text.Document document = new iTextSharp.text.Document(new Rectangle(612, 936), 15, 15, 10, 10);

        iTextSharp.text.pdf.PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);

        pdfEvents PageEventHandler = new pdfEvents();
        writer.PageEvent = PageEventHandler;

        PageEventHandler.tipoDocumento = "contrato";

        document.Open();

        String tempFile = Path.GetTempFileName();

        using (StreamWriter tempwriter = new StreamWriter(tempFile, false))
        {
            tempwriter.Write(html);
        }

        List<IElement> htmlarraylist = HTMLWorker.ParseToList(new StreamReader(tempFile), new StyleSheet());
        foreach (IElement element in htmlarraylist)
        {
            document.Add(element);
        }

        document.Close();
        writer.Close();

        Response.End();

        File.Delete(tempFile);
    }

    private float v_pixeles(int num)
    {
        return (float)(num * 2.83);
    }

    private void ImprimirContratoO_L_PREIMPRESO(DataRow filaInfoContrato)
    {
        tools _tools = new tools();

        Boolean CarnetIncluido = CheckBox_CON_CARNET_APARTE.Checked;


        String filename = "plantilla_contrato_preimpreso_obra_labor_sertempo_eys";

        HttpContext.Current.Response.AddHeader("Content-Disposition", "attachment;FileName=" + filename + ".pdf");

        Response.Clear();
        Response.ContentType = "application/pdf";

        iTextSharp.text.Document document = new iTextSharp.text.Document(new Rectangle(ancho, alto), v_pixeles(15), v_pixeles(15), v_pixeles(15), v_pixeles(15));

        iTextSharp.text.pdf.PdfWriter writer = PdfWriter.GetInstance(document, Response.OutputStream);

        pdfEvents PageEventHandler = new pdfEvents();
        writer.PageEvent = PageEventHandler;

        document.Open();



        String LABORA_EN = filaInfoContrato["RAZ_SOCIAL"].ToString().Trim().ToUpper();
        String CARGO = filaInfoContrato["NOM_OCUPACION"].ToString().Trim().ToUpper();
        String CC = filaInfoContrato["NUM_DOC_IDENTIDAD"].ToString().Trim().ToUpper();
        String NOMBRE = filaInfoContrato["APELLIDOS"].ToString().Trim().ToUpper() + " " + filaInfoContrato["NOMBRES"].ToString().Trim().ToUpper();
        String FIRMA_EMPLEADO = String.Empty;
        String VALIDO_HASTA = Convert.ToDateTime(filaInfoContrato["FECHA_TERMINA"]).ToLongDateString();

        String EMPRESA_USUARIA = filaInfoContrato["RAZ_SOCIAL"].ToString().Trim().ToUpper();
        String FECHA_INICIACION = Convert.ToDateTime(filaInfoContrato["FECHA_INICIA"]).ToLongDateString();
        String SALARIO = "$ " + String.Format("{0:N2}",Convert.ToDecimal(filaInfoContrato["SALARIO"]).ToString());


        String PERIODO_PAGO = DropDownList_PERIODO_PAGO.SelectedItem.Text;


        String SERVICIO_RESPECTIVO = filaInfoContrato["DESCRIPCION"].ToString().Trim().ToUpper();
        String DESCRIPCION_CARGO = filaInfoContrato["DSC_FUNCIONES"].ToString().Trim().ToUpper();
        String NOMBRE_TRABAJADOR = filaInfoContrato["APELLIDOS"].ToString().Trim().ToUpper() + " " + filaInfoContrato["NOMBRES"].ToString().Trim().ToUpper();
        String CC_TRABAJADOR = filaInfoContrato["NUM_DOC_IDENTIDAD"].ToString().Trim().ToUpper();
        String CARGO_TRABAJADOR = filaInfoContrato["NOM_OCUPACION"].ToString().Trim().ToUpper();

        String DOMICILIO_EMPLEADOR;
        if (Session["idEmpresa"].ToString() == "1")
        {
            DOMICILIO_EMPLEADOR = tabla.VAR_DOMICILIO_SERTEMPO;
        }
        else
        {
            DOMICILIO_EMPLEADOR = tabla.VAR_DOMICILIO_EYS;
        }
        
        String DIAS = DateTime.Now.Day.ToString();
        String MES = _tools.obtenerNombreMes(DateTime.Now.Month);
        String ANIO = DateTime.Now.Year.ToString().Substring(2, 2);

        Font letra;
        Phrase texto;
        PdfPTable tablaPdf;
        PdfPCell celda;

        float escalaFirma = 80; 
        String dirImagenFirma;
        if (Session["idEmpresa"].ToString() == "1")
        {
            dirImagenFirma = Server.MapPath("~/imagenes/reportes/firma_contrato_empleador_sertempo.jpg");
        }
        else
        {
            dirImagenFirma = Server.MapPath("~/imagenes/reportes/firma_contrato_empleador_eys.jpg");
        }
        iTextSharp.text.Image imagenFirma = iTextSharp.text.Image.GetInstance(dirImagenFirma);
        imagenFirma.ScalePercent(escalaFirma);


        PdfContentByte cb = writer.DirectContent;

        if (CarnetIncluido == true)
        {
            cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 7);
            cb.BeginText();
            cb.SetTextMatrix(v_pixeles(17), v_pixeles(10));
            cb.ShowText(LABORA_EN);
            cb.EndText();
            cb.BeginText();
            cb.SetTextMatrix(v_pixeles(17), v_pixeles(15));
            cb.ShowText(CARGO);
            cb.EndText();
            cb.BeginText();
            cb.SetTextMatrix(v_pixeles(17), v_pixeles(19));
            cb.ShowText(CC);
            cb.EndText();
            cb.BeginText();
            cb.SetTextMatrix(v_pixeles(17), v_pixeles(24));
            cb.ShowText(NOMBRE);
            cb.EndText();
            cb.BeginText();
            cb.SetTextMatrix(v_pixeles(133), v_pixeles(53));
            cb.ShowText(FIRMA_EMPLEADO);
            cb.EndText();
            cb.BeginText();
            cb.SetTextMatrix(v_pixeles(133), v_pixeles(57));
            cb.ShowText(VALIDO_HASTA);
            cb.EndText();
        }


        cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 8);
        cb.BeginText();
        cb.SetTextMatrix(v_pixeles(10), v_pixeles(234));
        cb.ShowText(EMPRESA_USUARIA);
        cb.EndText();
        cb.BeginText();
        cb.SetTextMatrix(v_pixeles(140), v_pixeles(234));
        cb.ShowText(FECHA_INICIACION);
        cb.EndText();
        cb.BeginText();
        cb.SetTextMatrix(v_pixeles(10), v_pixeles(242));
        cb.ShowText(SALARIO);
        cb.EndText();
        cb.BeginText();
        cb.SetTextMatrix(v_pixeles(140), v_pixeles(242));
        cb.ShowText(PERIODO_PAGO);
        cb.EndText();
        tablaPdf = new PdfPTable(1);
        tablaPdf.TotalWidth = document.PageSize.Width - v_pixeles(20);
        letra = new Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 8);
        texto = new Phrase(SERVICIO_RESPECTIVO, letra);
        celda = new PdfPCell(texto);
        celda.Border = 0;
        celda.BorderWidth = 0;
        celda.HorizontalAlignment = 3;
        tablaPdf.AddCell(celda);
        tablaPdf.WriteSelectedRows(0, -1, v_pixeles(9), v_pixeles(259), cb);
        tablaPdf = new PdfPTable(1);
        tablaPdf.TotalWidth = document.PageSize.Width - v_pixeles(20);
        letra = new Font(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 8);
        texto = new Phrase(DESCRIPCION_CARGO, letra);
        celda = new PdfPCell(texto);
        celda.Border = 0;
        celda.BorderWidth = 0;
        celda.HorizontalAlignment = 3;
        tablaPdf.AddCell(celda);
        tablaPdf.WriteSelectedRows(0, -1, v_pixeles(9), v_pixeles(277), cb);
        cb.BeginText();
        cb.SetTextMatrix(v_pixeles(10), v_pixeles(282));
        cb.ShowText(NOMBRE_TRABAJADOR);
        cb.EndText();
        cb.BeginText();
        cb.SetTextMatrix(v_pixeles(140), v_pixeles(282));
        cb.ShowText(CC_TRABAJADOR);
        cb.EndText();
        cb.BeginText();
        cb.SetTextMatrix(v_pixeles(10), v_pixeles(290));
        cb.ShowText(CARGO_TRABAJADOR);
        cb.EndText();
        cb.BeginText();
        cb.SetTextMatrix(v_pixeles(98), v_pixeles(299));
        cb.ShowText(DOMICILIO_EMPLEADOR);
        cb.EndText();

        document.NewPage();

        cb.SetFontAndSize(BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false), 6);
        cb.BeginText();
        cb.SetTextMatrix(v_pixeles(45), v_pixeles(198));
        cb.ShowText(DIAS);
        cb.EndText();
        cb.BeginText();
        cb.SetTextMatrix(v_pixeles(83), v_pixeles(198));
        cb.ShowText(MES);
        cb.EndText();
        cb.BeginText();
        cb.SetTextMatrix(v_pixeles(122), v_pixeles(198));
        cb.ShowText(ANIO);
        cb.EndText();

        tablaPdf = new PdfPTable(1);
        tablaPdf.TotalWidth = document.PageSize.Width - document.LeftMargin - document.RightMargin;
        celda = new PdfPCell(imagenFirma);
        celda.Border = 0;
        celda.BorderWidth = 0;
        tablaPdf.AddCell(celda);
        tablaPdf.WriteSelectedRows(0, -1, v_pixeles(30), v_pixeles(111) + imagenFirma.Height, cb);

        document.Close();
        writer.Close();

        Response.End();
    }

    private void ImprimirContratoO_L(DataRow filaInfoContrato)
    {
        if (DropDownList_FORMA_IMPRESION_CONTRATO.SelectedValue == "COMPLETO")
        {
            ImprimirContratoO_L_COMPLETO(filaInfoContrato);
        }
        else
        {
            if (DropDownList_FORMA_IMPRESION_CONTRATO.SelectedValue == "PREIMPRESO")
            {
            }
        }
    }

    protected void Button_Imprimir_Click(object sender, EventArgs e)
    {
        if (!(String.IsNullOrEmpty(HiddenField_ID_CONTRATO.Value)))
        {
            registroContrato _registroContrato = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaInfoContrato = _registroContrato.ObtenerInfoParaImprimirContrato(Convert.ToDecimal(HiddenField_ID_CONTRATO.Value));
            NumToLetra numToLetra = new NumToLetra();
            string salario_en_letras = numToLetra.Convertir(Convert.ToDecimal(TextBox_Salario.Text).ToString(), true);

            if(tablaInfoContrato.Rows.Count <= 0)
            {
                if(_registroContrato.MensajeError != null)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información para generar el contrato.", Proceso.Error);
                }
                else
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _registroContrato.MensajeError, Proceso.Error);
                }
            }
            else
            {
                String uspReporte = "";
                String nombreReporte = "";
                string nombre_empresa = null;
                string nit_empresa = null;
                string telefono_empresa = null;
                string direccion_empresa = null;
                
                if (Session["idEmpresa"].ToString() == "1")
                {
                    nombre_empresa = tabla.VAR_NOMBRE_SERTEMPO;
                    nit_empresa = tabla.VAR_NIT_SERTEMPO;
                    telefono_empresa = tabla.VAR_TELEFONO_SERTEMPO;
                    direccion_empresa = tabla.VAR_DOMICILIO_SERTEMPO;
                }
                else
                {
                    if (Session["idEmpresa"].ToString() == "3")
                    {
                        nombre_empresa = tabla.VAR_NOMBRE_EYS;
                        nit_empresa = tabla.VAR_NIT_EYS;
                        telefono_empresa = tabla.VAR_TELEFONO_EYS;
                        direccion_empresa = tabla.VAR_DOMICILIO_EYS;
                    }
                }
 
                DataRow filaInfoContrato = tablaInfoContrato.Rows[0];


                if (RadioButton_PRACTICANTE_UNI.Checked)
                {
                    uspReporte = "RPT_CONTRATACION_CONTRATO_APRENDIZ_UNIVERSITARIO";
                    nombreReporte = "RPT_CONTRATACION_CONTRATO_APRENDIZ_UNIVERSITARIO.rpt";
                    uspReporte += " '" + Session["idEmpresa"].ToString() + "'"
                        + ", '" + nombre_empresa + "'"
                        + ", '" + nit_empresa + "'"
                        + ", '" + telefono_empresa + "'"
                        + ", '" + direccion_empresa + "'"
                        + ", '" + TextBox_contrato_aprendiz_universitario_representante_legal.Text + "'"
                        + ", '" + TextBox_contrato_aprendiz_universitario_numero_identificacion.Text + "'"
                        + ", '" + TextBox_contrato_aprendiz_universitario_nombre_institucion.Text + "'"
                        + ", '" + TextBox_contrato_aprendiz_universitario_nit_institucion.Text + "'"
                        + ", '" + TextBox_contrato_aprendiz_universitario_especialidad_aprendiz.Text + "'"
                        + ", '" + filaInfoContrato["ID_EMPLEADO"].ToString() + "'"
                        ;
                }
                else if ((RadioButton_SENA_ELECTIVO.Checked) || (RadioButton_SENA_PRODUCTIVO.Checked))
                {
                    uspReporte = "RPT_CONTRATACION_CONTRATO_APRENDIZ_SENA";
                    nombreReporte = "RPT_CONTRATACION_CONTRATO_APRENDIZ_SENA.rpt";
                    uspReporte += " '" + Session["idEmpresa"].ToString() + "'"
                        + ", '" + nombre_empresa + "'"
                        + ", '" + nit_empresa + "'"
                        + ", '" + telefono_empresa + "'"
                        + ", '" + direccion_empresa + "'"
                        + ", '" + TextBox_contrato_aprendiz_sena_representante_legal.Text + "'"
                        + ", '" + TextBox_contrato_aprendiz_sena_cargo_r_l.Text + "'"
                        + ", '" + TextBox_contrato_aprendiz_sena_numero_documento_r_l.Text + "'"
                        + ", '" + TextBox_contrato_aprendiz_sena_especialidad_aprendiz.Text + "'"
                        + ", '" + TextBox_contrato_aprendiz_sena_curso_aprendiz.Text + "'"
                        + ", '" + TextBox_contrato_aprendiz_nombre_institucion.Text + "'"
                        + ", '" + TextBox_contrato_aprendiz_nit_institucion.Text + "'"
                        + ", '" + TextBox_contrato_aprendiz_centro_formacion.Text + "'"
                        + ", '" + TextBox_contrato_aprendiz_meses_duracion.Text + "'"
                        + ", '" + TextBox_contrato_aprendiz_fecha_inicio.Text + "'"
                        + ", '" + TextBox_contrato_aprendiz_fecha_final.Text + "'"
                        + ", '" + filaInfoContrato["ID_EMPLEADO"].ToString() + "'"
                        ;
                    
                }
                else if (DropDownList_Clase_contrato.SelectedValue == ClaseContrato.I.ToString())
                {
                    uspReporte = "RPT_CONTRATACION_CONTRATO_INDEFINIDO";
                    nombreReporte = "RPT_CONTRATACION_CONTRATO_INDEFINIDO.rpt";
                    uspReporte += " '" + Session["idEmpresa"].ToString() + "'" 
                        + ", '" + nombre_empresa + "'"
                        + ", '" + nit_empresa + "'"
                        + ", '" + TextBox_contrato_indefinido_identificacion_empleador.Text + "'"
                        + ", '" + TextBox_contrato_indefinido_nombre_empleador.Text + "'"
                        + ", '" + TextBox_contrato_indefinido_ciudad_domicilio_empleador.Text + "'"
                        + ", '" + telefono_empresa + "'"
                        + ", '" + direccion_empresa + "'"
                        + ", '" + salario_en_letras + "'"
                        + ", '" + filaInfoContrato["ID_EMPLEADO"].ToString() + "'"
                        ;
                }
                else
                {
                    if (DropDownList_Clase_contrato.SelectedValue == ClaseContrato.L_C_C_D_A.ToString())
                    {
                    }
                    else
                    {
                        if (DropDownList_Clase_contrato.SelectedValue == ClaseContrato.L_S_C_D_A.ToString())
                        {
                        }
                        else
                        {
                            if (DropDownList_Clase_contrato.SelectedValue == ClaseContrato.O_L.ToString())
                            {

                                if (DropDownList_FORMA_IMPRESION_CONTRATO.SelectedValue == "COMPLETO")
                                {
                                    ImprimirContratoO_L_COMPLETO(filaInfoContrato);
                                }
                                else
                                {
                                    if (DropDownList_FORMA_IMPRESION_CONTRATO.SelectedValue == "PREIMPRESO")
                                    {
                                        uspReporte = "usp_con_reg_contratos_ObtenerInfo_para_imprimir_contrato " + filaInfoContrato["REGISTRO_CONTRATO"].ToString();
                                        nombreReporte = "RPT_CONTRATACION_OBRA_LABOR_PREIMPRESO.rpt "; 
                                    }
                                }
                            }
                            else
                            {
                                if (DropDownList_Clase_contrato.SelectedValue == ClaseContrato.T_F.ToString())
                                {
                                }
                                else
                                {
                                    if (DropDownList_Clase_contrato.SelectedValue == ClaseContrato.IN.ToString())
                                    {
                                        uspReporte = "RPT_CONTRATACION_CONTRATO_INTEGRAL";
                                        nombreReporte = "RPT_CONTRATACION_CONTRATO_INTEGRAL.rpt";
                                        uspReporte += " '" + Session["idEmpresa"].ToString() + "'"
                                            + ", '" + nombre_empresa + "'"
                                            + ", '" + direccion_empresa + "'"
                                            + ", '" + TextBox_contrato_integral_porcentaje_parafiscales.Text + "'"
                                            + ", '" + TextBox_contrato_integral_porcentaje_prestacional.Text + "'"
                                            + ", '" + filaInfoContrato["ID_EMPLEADO"].ToString() + "'";
                                    }
                                }
                            }
                        }
                    }   
                }

                String cadenaDeConeccion = "";
                if (Session["idEmpresa"].ToString() == "1")
                {
                    cadenaDeConeccion = ConfigurationManager.ConnectionStrings["siser"].ConnectionString;
                }
                else
                {
                    cadenaDeConeccion = ConfigurationManager.ConnectionStrings["sister"].ConnectionString;
                }

                SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(cadenaDeConeccion);
                String user = builder.UserID;
                string pass = builder.Password;
                String server = builder.DataSource;
                String db = builder.InitialCatalog;

                SqlConnection conn = new SqlConnection(cadenaDeConeccion);

                try
                {
                    using (SqlCommand comando = new SqlCommand(uspReporte, conn))
                    {
                        using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                        {
                            DataSet ds = new DataSet();
                            adaptador.Fill(ds);

                            reporte = new ReportDocument();

                            reporte.Load(Server.MapPath("~/Reportes/Contratacion/" + nombreReporte));
                            reporte.SetDataSource(ds.Tables[0]);
                            reporte.DataSourceConnections[0].SetConnection(server, db, user, pass);

                            using (var mStream = (MemoryStream)reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat))
                            {
                                Response.AddHeader("Content-Disposition", "attachment;FileName=RPT_PDF.pdf");
                                Response.Clear();
                                Response.Buffer = true;
                                Response.ContentType = "application/pdf";
                                Response.BinaryWrite(mStream.ToArray());
                            }
                            Response.End();
                        } 
                    } 
                } 
                catch (Exception ex)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, ex.Message, Proceso.Error);
                }
                finally
                {
                    if (conn.State != ConnectionState.Closed)
                        conn.Close();
                    conn.Dispose();
                }

            }
        }
        else
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El contrato no ha sido guardado. Debe diligenciar todos los datos de contrato y luego Guardarlos, para poder imprimir el correspondiente documento.", Proceso.Advertencia);
        }
    }

    protected void Button_Guardar_Click(object sender, EventArgs e)
    {

        try
        {
            if (DropDownList_Clase_contrato.SelectedValue == ClaseContrato.IN.ToString())
            {
                parametroSalarial ParametroSalarial = new parametroSalarial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                DataRow dataRow = ParametroSalarial.ObtenerPorAño(System.DateTime.Now.Year);
                if (dataRow != null)
                {
                    if (string.IsNullOrEmpty(dataRow["SMMLV"].ToString()))
                    {
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se ha definido el SMMLV para el año " + System.DateTime.Now.Year.ToString(), Proceso.Error);
                        return;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(dataRow["SMMLV_SALARIO_INTEGRAL"].ToString()))
                        {
                            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se ha definido la cantidad de SMMLV del salario integral, para el año" + System.DateTime.Now.Year.ToString(), Proceso.Error);
                            return;
                        }
                        else
                        {

                            if ((Convert.ToDecimal(this.TextBox_Salario.Text)) < (Convert.ToDecimal(dataRow["SMMLV_SALARIO_INTEGRAL"].ToString()) * Convert.ToDecimal(dataRow["SMMLV"].ToString())))
                            {
                                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El salario integral es menor al permitido " + (Convert.ToDecimal(dataRow["SMMLV_SALARIO_INTEGRAL"].ToString()) * Convert.ToDecimal(dataRow["SMMLV"].ToString())), Proceso.Error);
                                return;
                            }
                            else
                            {
                                if (!(Convert.ToDecimal(TextBox_contrato_integral_porcentaje_parafiscales.Text).Equals(Convert.ToDecimal(dataRow["PORC_BASE_SEGSOC_SALINTEGRAL"].ToString()))))
                                {
                                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El porcentaje parafiscal debe ser " + dataRow["PORC_BASE_SEGSOC_SALINTEGRAL"].ToString(), Proceso.Error);
                                    return;
                                }
                                else
                                {
                                    if (!(Convert.ToDecimal(TextBox_contrato_integral_porcentaje_prestacional.Text).Equals(Convert.ToDecimal(dataRow["PORC_BASE_VACACIONES_SALINTEGRAL"].ToString()))))
                                    {
                                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El porcentaje prestacional debe ser " + dataRow["PORC_BASE_VACACIONES_SALINTEGRAL"].ToString(), Proceso.Error);
                                        return;
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se han configurado los parametros salariales para el año " + System.DateTime.Now.Year.ToString(), Proceso.Error);
                    return;
                }
            }

            tools _tools = new tools();
            SecureQueryString QueryStringSeguro;
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);
            String persona = QueryStringSeguro["persona"].ToString();
            String[] datos = persona.Split(','); 

            requisicion _req = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaReq = _req.ObtenerComRequerimientoPorIdRequerimiento(Convert.ToDecimal(datos[1]));
            DataRow filaReq = tablaReq.Rows[0];
            String idPerfil = filaReq["REGISTRO_PERFIL"].ToString();
            Decimal riesgo = 0;
            String centroCosto = "0";
            String SubCentroCosto = "0";
            String Ciudad = null;
            String servicio = "0";
            Decimal salario = 0;
            Decimal smmlv = 0;
            Decimal smmlv_integral = 0;
            String AFP = null;
            String ARP = null;
            String CCF = null;
            String EPS = null;
            String pensionado = "N";
            int id_requerimiento = Convert.ToInt32(datos[1].ToString());
            int id_solicitud = Convert.ToInt32(datos[0].ToString());
            int id_empresa = Convert.ToInt32(datos[3].ToString());
            int ID_SERVICIO_RESPECTIVO = Convert.ToInt32(filaReq["ID_SERVICIO_RESPECTIVO"].ToString());
            String clase_Contrato = null;
            String tipo_Contrato = null;
            DateTime fechaInicio;
            DateTime fechaFinal;

            int id_entidad_Bancaria = 0;
            String formaPago = null;
            String tipoCuenta = String.Empty;
            String num_Cuenta = null;

            String salInt = null;
            String pago_Dias_Productividad = "N";
            String sena_productivo = "N";
            String sena_electivo = "N";
            String practicante_Universitario = "N";
            Decimal valor_nomina = 0;
            Decimal valor_contrato = 0;
            DateTime fecha_inicio_periodo;
            DateTime fecha_fin_periodo;
            String periodo_pago = null;
            
            radicacionHojasDeVida _sol = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaSolicitud = _sol.ObtenerRegSolicitudesingresoPorIdSolicitud(Convert.ToInt32(datos[0]));
            DataRow filaSolicitud = tablaSolicitud.Rows[0];

            formaPago = filaSolicitud["FORMA_PAGO"].ToString();

            parametroSalarial par = new parametroSalarial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable par_ta = par.ObtenerSalarioMinimo(Convert.ToInt32(System.DateTime.Today.Year.ToString()));
            DataRow fila = par_ta.Rows[0];
            smmlv = Convert.ToDecimal(fila["SMMLV"].ToString());
            par_ta.Clear();
            fila.Delete();
            par_ta = par.ObtenerSalarioIntegral(Convert.ToInt32(System.DateTime.Today.Year.ToString()));
            fila = par_ta.Rows[0];
            smmlv_integral = Convert.ToDecimal(fila["SMMLV_SALARIO_INTEGRAL"].ToString());

            Ciudad = this.DropDownList_Ciudad.SelectedValue;
            centroCosto = this.DropDownList_CentroCosto.SelectedValue;
            SubCentroCosto = this.DropDownList_sub_cc.SelectedValue;

            if (DropDownList_Salario_integral.SelectedValue.Equals("S"))
            {
                salario = Convert.ToDecimal(TextBox_Salario.Text);
                Decimal salarioInt = smmlv * smmlv_integral;
                if (salario < salarioInt)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El salario ingresado debe ser igual o mayor a " + salarioInt + " dado que indico que es salario integral", Proceso.Advertencia);
                }
            }

            #region afiliaciones
            afiliacion _afiliacion = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaAfiliacion;
            DataRow filaAfiliacion;
            if (RadioButton_PRACTICANTE_UNI.Checked)
            {
                practicante_Universitario = "S";
                pensionado = "N";
                tablaAfiliacion = _afiliacion.ObtenerconafiliacionArpPorSolicitudRequerimiento(Convert.ToInt32(datos[0]), Convert.ToInt32(datos[1]));
                if (tablaAfiliacion.Rows.Count <= 0)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La persona no tiene afiliaciones a ARL", Proceso.Advertencia);
                }
                else
                {
                    filaAfiliacion = tablaAfiliacion.Rows[0];
                    ARP = filaAfiliacion["REGISTRO"].ToString();

                    tablaAfiliacion.Clear();
                    filaAfiliacion.Delete();
                }

                tablaAfiliacion = _afiliacion.ObtenerconafiliacionEpsPorSolicitudRequerimiento(Convert.ToInt32(datos[0]), Convert.ToInt32(datos[1]));
                if (tablaAfiliacion.Rows.Count <= 0)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La persona no tiene afiliaciones a EPS.", Proceso.Advertencia);
                }
                else
                {
                    filaAfiliacion = tablaAfiliacion.Rows[0];

                    EPS = filaAfiliacion["REGISTRO"].ToString();

                    tablaAfiliacion.Clear();
                    filaAfiliacion.Delete();
                }
            }
            else if (RadioButton_SENA_ELECTIVO.Checked)
            {
                sena_electivo = "S";
                pensionado = "N";
                tablaAfiliacion = _afiliacion.ObtenerconafiliacionEpsPorSolicitudRequerimiento(Convert.ToInt32(datos[0]), Convert.ToInt32(datos[1]));
                if (tablaAfiliacion.Rows.Count <= 0)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La persona no tiene afiliaciones a EPS.", Proceso.Advertencia);
                }
                else
                {
                    filaAfiliacion = tablaAfiliacion.Rows[0];

                    EPS = filaAfiliacion["REGISTRO"].ToString();

                    tablaAfiliacion.Clear();
                    filaAfiliacion.Delete();
                }

            }
            else if (RadioButton_SENA_PRODUCTIVO.Checked)
            {
                sena_productivo = "S";
                pensionado = "N";
                tablaAfiliacion = _afiliacion.ObtenerconafiliacionEpsPorSolicitudRequerimiento(Convert.ToInt32(datos[0]), Convert.ToInt32(datos[1]));
                if (tablaAfiliacion.Rows.Count <= 0)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La persona no tiene afiliaciones a EPS.", Proceso.Advertencia);
                }
                else
                {
                    filaAfiliacion = tablaAfiliacion.Rows[0];

                    EPS = filaAfiliacion["REGISTRO"].ToString();

                    tablaAfiliacion.Clear();
                    filaAfiliacion.Delete();
                }
                tablaAfiliacion = _afiliacion.ObtenerconafiliacionArpPorSolicitudRequerimiento(Convert.ToInt32(datos[0]), Convert.ToInt32(datos[1]));
                if (tablaAfiliacion.Rows.Count <= 0)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La persona no tiene afiliaciones a ARL.", Proceso.Advertencia);
                }
                else
                {
                    filaAfiliacion = tablaAfiliacion.Rows[0];
                    ARP = filaAfiliacion["REGISTRO"].ToString();

                    tablaAfiliacion.Clear();
                    filaAfiliacion.Delete();
                }
            }
            else
            {
                tablaAfiliacion = _afiliacion.ObtenerconafiliacionEpsPorSolicitudRequerimiento(Convert.ToInt32(datos[0]), Convert.ToInt32(datos[1]));
                if (tablaAfiliacion.Rows.Count <= 0)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La persona no tiene afiliaciones a EPS.", Proceso.Advertencia);
                }
                else
                {
                    filaAfiliacion = tablaAfiliacion.Rows[0];

                    EPS = filaAfiliacion["REGISTRO"].ToString();

                    tablaAfiliacion.Clear();
                    filaAfiliacion.Delete();
                }

                tablaAfiliacion = _afiliacion.ObtenerconafiliacionArpPorSolicitudRequerimiento(Convert.ToInt32(datos[0]), Convert.ToInt32(datos[1]));
                if (tablaAfiliacion.Rows.Count <= 0)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La persona no tiene afiliaciones a ARL.", Proceso.Advertencia);
                }
                else
                {
                    filaAfiliacion = tablaAfiliacion.Rows[0];
                    ARP = filaAfiliacion["REGISTRO"].ToString();

                    tablaAfiliacion.Clear();
                    filaAfiliacion.Delete();
                }

                tablaAfiliacion = _afiliacion.ObtenerconafiliacionCajasCPorSolicitudRequerimiento(Convert.ToInt32(datos[0]), Convert.ToInt32(datos[1]));
                if (tablaAfiliacion.Rows.Count <= 0)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La persona no tiene afiliaciones a CCF.", Proceso.Advertencia);
                }
                else
                {
                    filaAfiliacion = tablaAfiliacion.Rows[0];
                    CCF = filaAfiliacion["REGISTRO"].ToString();

                    tablaAfiliacion.Clear();
                    filaAfiliacion.Delete();
                }

                tablaAfiliacion = _afiliacion.ObtenerconafiliacionfpensionesPorSolicitudRequerimiento(Convert.ToInt32(datos[0]), Convert.ToInt32(datos[1]));
                if (tablaAfiliacion.Rows.Count <= 0)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La persona no tiene afiliaciones a AFP.", Proceso.Advertencia);
                }
                else
                {
                    filaAfiliacion = tablaAfiliacion.Rows[0];
                    pensionado = filaAfiliacion["PENSIONADO"].ToString();
                    if (pensionado.Equals("S"))
                    {
                        AFP = "0";
                    }
                    else
                    {
                        AFP = filaAfiliacion["REGISTRO"].ToString();
                    }
                }
            }
            #endregion afiliaciones
            #region riesgo
            condicionesContratacion _riesgo = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            if (Session["idEmpresa"].ToString().Equals("3"))
            {
                if (String.IsNullOrEmpty(DropDownList_servicio.SelectedValue.ToString()))
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar un servicio.", Proceso.Advertencia);
                }
                else
                {
                    if (!(String.IsNullOrEmpty(DropDownList_sub_cc.SelectedValue.ToString())))
                    {
                        DataTable tablaCondContr = _riesgo.ObtenerCondicionContratacionPorIdPerfilIdSubCIdServicio(Convert.ToDecimal(idPerfil), Convert.ToDecimal(DropDownList_sub_cc.SelectedItem.Value.ToString()), Convert.ToDecimal(DropDownList_servicio.SelectedItem.Value.ToString()));
                        DataRow filaCondContrata = tablaCondContr.Rows[0];
                        riesgo = Convert.ToDecimal(filaCondContrata["RIESGO"].ToString());
                        SubCentroCosto = DropDownList_sub_cc.SelectedValue;
                        servicio = DropDownList_servicio.SelectedValue;
                    }
                    else if (!(String.IsNullOrEmpty(DropDownList_CentroCosto.SelectedValue.ToString())))
                    {
                        DataTable tablaCondContr = _riesgo.ObtenerCondicionContratacionPorIdPerfilIdCentroCIdServicio(Convert.ToDecimal(idPerfil), Convert.ToDecimal(DropDownList_CentroCosto.SelectedItem.Value.ToString()), Convert.ToDecimal(DropDownList_servicio.SelectedItem.Value.ToString()));
                        DataRow filaCondContrata = tablaCondContr.Rows[0];
                        riesgo = Convert.ToDecimal(filaCondContrata["RIESGO"].ToString());
                        centroCosto = DropDownList_CentroCosto.SelectedValue;
                        servicio = DropDownList_servicio.SelectedValue;
                    }
                    else if (!(String.IsNullOrEmpty(DropDownList_Ciudad.SelectedValue.ToString())))
                    {
                        DataTable tablaCondContr = _riesgo.ObtenerCondicionContratacionPorIdPerfilIdCiudadIdServicio(Convert.ToDecimal(idPerfil), DropDownList_Ciudad.SelectedItem.Value.ToString(), Convert.ToDecimal(DropDownList_servicio.SelectedItem.Value.ToString()));
                        DataRow filaCondContrata = tablaCondContr.Rows[0];
                        riesgo = Convert.ToDecimal(filaCondContrata["RIESGO"].ToString());
                        Ciudad = DropDownList_Ciudad.SelectedValue;
                        servicio = DropDownList_servicio.SelectedValue;
                    }
                }
            }
            else
            {
                if (!(String.IsNullOrEmpty(DropDownList_sub_cc.SelectedValue.ToString())))
                {
                    DataTable tablaCondContr = _riesgo.ObtenerCondicionComercialPorIdPerfilIdSubC(Convert.ToDecimal(idPerfil), Convert.ToDecimal(DropDownList_sub_cc.SelectedItem.Value.ToString()));
                    DataRow filaCondContrata = tablaCondContr.Rows[0];
                    riesgo = Convert.ToDecimal(filaCondContrata["RIESGO"].ToString());
                    SubCentroCosto = DropDownList_sub_cc.SelectedValue;
                }
                else if (!(String.IsNullOrEmpty(DropDownList_CentroCosto.SelectedValue.ToString())))
                {
                    DataTable tablaCondContr = _riesgo.ObtenerCondicionComercialPorIdPerfilIdCentroC(Convert.ToDecimal(idPerfil), Convert.ToDecimal(DropDownList_CentroCosto.SelectedItem.Value.ToString()));
                    DataRow filaCondContrata = tablaCondContr.Rows[0];
                    riesgo = Convert.ToDecimal(filaCondContrata["RIESGO"].ToString());
                    centroCosto = DropDownList_CentroCosto.SelectedValue;
                }
                else if (!(String.IsNullOrEmpty(DropDownList_Ciudad.SelectedValue.ToString())))
                {
                    DataTable tablaCondContr = _riesgo.ObtenerCondicionComercialPorIdPerfilIdCiudad(Convert.ToDecimal(idPerfil), DropDownList_Ciudad.SelectedItem.Value.ToString());
                    DataRow filaCondContrata = tablaCondContr.Rows[0];
                    riesgo = Convert.ToDecimal(filaCondContrata["RIESGO"].ToString());
                    Ciudad = DropDownList_Ciudad.SelectedValue;
                }
            }
            #endregion riesgo

            clase_Contrato = DropDownList_Clase_contrato.SelectedItem.Value.ToString();
            tipo_Contrato = DropDownList_tipo_Contrato.SelectedItem.Value.ToString();

            if (clase_Contrato.Equals("C_A") == true)
            {
                fechaInicio = Convert.ToDateTime(TextBox_contrato_aprendiz_fecha_inicio.Text);
            }
            else
            {
                fechaInicio = Convert.ToDateTime(TextBox_fecha_inicio.Text);
            }

            if (clase_Contrato.Equals("I") | clase_Contrato.Equals("L_S_C_D_A") )
            {
                fechaFinal = Convert.ToDateTime("01/01/1900");
            }
            else
            {
                if (clase_Contrato.Equals("C_A") == true)
                {
                    fechaFinal = Convert.ToDateTime(TextBox_contrato_aprendiz_fecha_final.Text);
                }
                else
                {
                    fechaFinal = Convert.ToDateTime(TextBox_fecha_terminacion.Text);
                }
            }

            if (formaPago.Equals("CHEQUE") | formaPago.Equals("EFECTIVO"))
            {
                num_Cuenta = null;
                id_entidad_Bancaria = 0;
                tipoCuenta = String.Empty;
            }
            else
            {
                if (String.IsNullOrEmpty(filaSolicitud["ID_ENTIDAD"].ToString()))
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se ha seleccionado la entidad bancaria, ni el número de cuenta, vuelva a la opción de exámenes y complete el proceso. Verifique por favor.", Proceso.Advertencia);
                    num_Cuenta = null;
                    id_entidad_Bancaria = 0;
                }
                else
                {
                    id_entidad_Bancaria = Convert.ToInt32(filaSolicitud["ID_ENTIDAD"].ToString());
                    num_Cuenta = filaSolicitud["NUM_CUENTA"].ToString();
                    tipoCuenta = filaSolicitud["TIPO_CUENTA"].ToString();
                }
            }

            salario = Convert.ToDecimal(TextBox_Salario.Text);
            salInt = DropDownList_Salario_integral.SelectedValue.ToString();
            if (DropDownList_tipo_Contrato.SelectedValue.Equals("PR"))
            {
                pago_Dias_Productividad = "S";
                valor_contrato = Convert.ToDecimal(TextBox_Salario.Text);
                valor_nomina = Convert.ToDecimal(TextBox_Salario.Text);
            }

            periodo_pago = DropDownList_PERIODO_PAGO.SelectedValue.ToString();
            
            fecha_inicio_periodo = fechaInicio;
            fecha_fin_periodo = fechaFinal;

            registroContrato contrato = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            DataTable tablaContratosExistentes = contrato.ObtenerNomEmpleadoPorIDSolicitudYFechaIngreso(id_solicitud, fechaInicio);

            if (tablaContratosExistentes.Rows.Count <= 0)
            {
                String datosG = contrato.ElaborarContrato(id_requerimiento, 
                        id_solicitud, 
                        id_empresa, 
                        Convert.ToInt32(centroCosto),
                        Convert.ToInt32(SubCentroCosto), 
                        Ciudad, 
                        ID_SERVICIO_RESPECTIVO, 
                        Convert.ToInt32(servicio),
                        Convert.ToInt32(ARP), 
                        Convert.ToInt32(CCF), 
                        Convert.ToInt32(EPS), 
                        Convert.ToInt32(AFP), 
                        Convert.ToDecimal(riesgo),
                        pensionado, 
                        clase_Contrato, 
                        tipo_Contrato, 
                        "C", 
                        fechaInicio, 
                        fechaFinal, 
                        salInt, 
                        salario, 
                        "S", 
                        "S", 
                        "N", 
                        "N",
                        id_entidad_Bancaria, 
                        num_Cuenta, 
                        formaPago, 
                        pago_Dias_Productividad, 
                        sena_productivo, 
                        sena_electivo, 
                        practicante_Universitario,
                        valor_contrato, 
                        valor_nomina, 
                        fecha_inicio_periodo, 
                        fecha_fin_periodo, 
                        periodo_pago, 
                        tipoCuenta, 
                        DropDownList_DESCRIPCION_SALARIO.SelectedValue.ToString(), 
                        Convert.ToDecimal(HiddenField_ID_PERFIL.Value));

                if (!(String.IsNullOrEmpty(contrato.MensajeError)))
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El empleado no fue creado, " + contrato.MensajeError, Proceso.Error);
                }
                else
                {
                    String[] d = datosG.Split(',');
                    HiddenField_ID_CONTRATO.Value = d[0];
                    HiddenField_persona.Value = id_solicitud + "," + id_requerimiento + "," + datos[2] + "," + datos[3] + "," + datos[4] + "," + datosG;

                    Clausula clausula = new Clausula(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                    Cargar(clausula.ObtenerContratacionPorIdEmpleado(Convert.ToDecimal(d[1])), GridView_clausulas);
                    HiddenField_ID_EMPLEADO.Value = d[1].ToString();
                    cargar_menu_botones_modulos_internos(false);

                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El empleado y el contrato fueron creados con exito: " + datosG, Proceso.Correcto);
                }

                configurarBotonesDeAccion(true, true, true, true);

                Panel_Informacion_Contrato.Enabled = false;
            }
            else
            {
                DataRow filaContratos = tablaContratosExistentes.Rows[0];

                String datosG = filaContratos["ID_CONTRATO"].ToString() + "," + filaContratos["ID_EMPLEADO"].ToString();
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El Trabajador ya tiene un contrato activo con la misma fecha de ingreso. Los datos del contrato son: " + datosG, Proceso.Correcto);
            }
        }
        catch (Exception err)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, err.Message, Proceso.Error);
        }
    }
    
    protected void RadioButton_PRACTICANTE_UNI_CheckedChanged(object sender, EventArgs e)
    {
        Ocultar(Acciones.Contratar);
        Mostrar(Contratos.Universitario);
        if (RadioButton_PRACTICANTE_UNI.Checked)
        {
            parametroSalarial par = new parametroSalarial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable part = par.ObtenerSalarioMinimo(Convert.ToInt32(System.DateTime.Today.Year.ToString()));
            DataRow fila = part.Rows[0];

            TextBox_Salario.Text = fila["SMMLV"].ToString();
            TextBox_Salario.Enabled = false;

            try
            {
                DropDownList_Clase_contrato.SelectedValue = "C_A";
            }
            catch
            {
                DropDownList_Clase_contrato.ClearSelection();
            }

            try
            {
                DropDownList_FORMA_IMPRESION_CONTRATO.SelectedValue = "COMPLETO";
            }
            catch
            {
                DropDownList_FORMA_IMPRESION_CONTRATO.ClearSelection();
            }
        }
        else
        {
            TextBox_Salario.Text = "";
            TextBox_Salario.Enabled = true;
        }
    }
    
    protected void RadioButton_SENA_ELECTIVO_CheckedChanged(object sender, EventArgs e)
    {
        Ocultar(Acciones.Contratar);
        Mostrar(Contratos.Sena);

        if (RadioButton_SENA_ELECTIVO.Checked)
        {
            parametroSalarial par = new parametroSalarial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable part = par.ObtenerSalarioMinimo(Convert.ToInt32(System.DateTime.Today.Year.ToString()));
            DataRow fila = part.Rows[0];
            double salario = 0;

            salario = Convert.ToDouble((Convert.ToDouble(fila["SMMLV"]) / 2));
            TextBox_Salario.Text = string.Format("{0:N}", salario);
            TextBox_Salario.Enabled = false;

            try
            {
                DropDownList_Clase_contrato.SelectedValue = "C_A";
            }
            catch
            {
                DropDownList_Clase_contrato.ClearSelection();
            }

            try
            {
                DropDownList_FORMA_IMPRESION_CONTRATO.SelectedValue = "COMPLETO";
            }
            catch
            {
                DropDownList_FORMA_IMPRESION_CONTRATO.ClearSelection();
            }
        }
        else
        {
            TextBox_Salario.Text = "";
            TextBox_Salario.Enabled = true;
        }
    }
    
    protected void RadioButton_SENA_PRODUCTIVO_CheckedChanged(object sender, EventArgs e)
    {
        Ocultar(Acciones.Contratar);
        Mostrar(Contratos.Sena);

        if (RadioButton_SENA_PRODUCTIVO.Checked)
        {
            parametroSalarial par = new parametroSalarial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable part = par.ObtenerSalarioMinimo(Convert.ToInt32(System.DateTime.Today.Year.ToString()));
            DataRow fila = part.Rows[0];
            double salario = 0;

            salario = Convert.ToDouble((Convert.ToDouble(fila["SMMLV"]) *0.75));
            TextBox_Salario.Text = string.Format("{0:N}", salario);
            TextBox_Salario.Enabled = false;

            try
            {
                DropDownList_Clase_contrato.SelectedValue = "C_A";
            }
            catch
            {
                DropDownList_Clase_contrato.ClearSelection();
            }

            try
            {
                DropDownList_FORMA_IMPRESION_CONTRATO.SelectedValue = "COMPLETO";
            }
            catch
            {
                DropDownList_FORMA_IMPRESION_CONTRATO.ClearSelection();
            }
        }
        else
        {
            TextBox_Salario.Text = "";
            TextBox_Salario.Enabled = true;
        }
    }

    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    protected void RadioButton_ninguno_CheckedChanged(object sender, EventArgs e)
    {
        Ocultar(Acciones.Contratar);
        TextBox_Salario.Text = Convert.ToDecimal(HiddenField_SUELDO.Value).ToString();
    }

    protected void Button_Guardar_CLAUSULAS_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        Byte[] archivo = null;
        Int32 archivo_tamaño = 0;
        String archivo_extension = null;
        String archivo_tipo = null;

        List<Clausula> clausulas = new List<Clausula>();
        Clausula clausula = new Clausula(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        if (!GridView_clausulas.Rows.Count.Equals(0))
        {

            for (int i = 0; i < GridView_clausulas.Rows.Count; i++)
            {
                Clausula c = new Clausula(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                FileUpload fileUpload = GridView_clausulas.Rows[i].FindControl("FileUpload_ARCHIVO") as FileUpload;

                using (BinaryReader reader = new BinaryReader(fileUpload.PostedFile.InputStream))
                {
                    archivo = reader.ReadBytes(fileUpload.PostedFile.ContentLength);
                    archivo_tamaño = fileUpload.PostedFile.ContentLength;
                    archivo_tipo = fileUpload.PostedFile.ContentType;
                    archivo_extension = _tools.obtenerExtensionArchivo(fileUpload.PostedFile.FileName);
                }
                c.IdEmpleado = Convert.ToDecimal(GridView_clausulas.Rows[i].Cells[1].Text);
                c.IdClausua = Convert.ToDecimal(GridView_clausulas.Rows[i].Cells[0].Text);
                c.Archivo = archivo;
                c.ArchivoExtension = archivo_extension;
                c.ArchivoTamaño = archivo_tamaño;
                c.ArchivoTipo = archivo_tipo;
                clausulas.Add(c);
            }
            if (clausula.Actualizar(clausulas))
            {
                Cargar(clausula.ObtenerContratacionPorIdEmpleado(Convert.ToDecimal(HiddenField_ID_EMPLEADO.Value)), GridView_clausulas);
                cargar_menu_botones_modulos_internos(false);
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Las clausulas para el trabajador han sido actualizadas correctamente ", Proceso.Correcto);
            }
            else Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Las clausulas NO han sido actualizadas  ", Proceso.Error);
        }
    }
    #endregion
}