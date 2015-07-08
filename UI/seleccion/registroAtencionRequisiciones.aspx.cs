using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using Brainsbits.LLB.seleccion;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB;
using Brainsbits.LLB.comercial;

using TSHAK.Components;
using System.Configuration;

public partial class _Default : System.Web.UI.Page
{
    private String NOMBRE_AREA = null;
    private void RolPermisos()
    {
        #region variables
        int contadorPermisos = 0;
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        #endregion variables

        seguridad _seguridad = new seguridad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        String rutaScript = _tools.obtenerRutaVerdaderaScript(Request.ServerVariables["SCRIPT_NAME"]);

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        if (String.IsNullOrEmpty(QueryStringSeguro["proceso"]) == true)
        {
            NOMBRE_AREA = tabla.NOMBRE_AREA_SELECCION;
        }
        else
        {
            int proceso = Convert.ToInt32(QueryStringSeguro["proceso"]);
            NOMBRE_AREA = _tools.ObtenerTablaNombreArea(proceso);
        }

        DataTable tablaInformacionPermisos = _seguridad.ObtenerPermisosBotones(NOMBRE_AREA, rutaScript);

        maestrasInterfaz _maestrasInterfaz = new maestrasInterfaz();

        contadorPermisos = _maestrasInterfaz.RolPermisos(this, tablaInformacionPermisos);

        if (contadorPermisos <= 0)
        {
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

    private String _mensaje;
    private String _mensaje_envioEmailIndeterminado = "La información de envío de correos electrónicos no se puede determinar por falta de datos. Por favor seleccione la EMPRESA y CIUDAD del requerimiento.";
    private String _mensaje_SinEnvioEmail = "Para la EMPRESA y CIUDAD no se encuentrá información de envío de correos electrónicos.";

    private System.Drawing.Color colorSeleccionado = System.Drawing.ColorTranslator.FromHtml("#699DF0");

    private enum NivelesRequerimiento
    { 
        Baja = 0,
        Media,
        Compleja
    }

    #region carga de drop y grids
    private void cargar_DropDownList_BUSCAR()
    {
        DropDownList_BUSCAR.Items.Clear();
        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_BUSCAR.Items.Add(item);
        item = new ListItem("Código de Empresa", "COD_EMPRESA");
        DropDownList_BUSCAR.Items.Add(item);
        item = new ListItem("Razón social", "RAZ_SOCIAL");
        DropDownList_BUSCAR.Items.Add(item);
        item = new ListItem("Número requisición", "ID_REQUERIMIENTO");
        DropDownList_BUSCAR.Items.Add(item);
        DropDownList_BUSCAR.DataBind();
    }

    private void cargar_DropDownList_TIP_REQ()
    {
        DropDownList_TIP_REQ.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_TIPO_REQ);

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_TIP_REQ.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_TIP_REQ.Items.Add(item);
        }
        DropDownList_TIP_REQ.DataBind();
    }

    private void cargar_DropDownList_TIP_REQ_con_filtro_usaurio()
    {
        DropDownList_TIP_REQ.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTablaRestriccionUsuario(tabla.PARAMETROS_TIPO_REQ, Session["USU_LOG"].ToString());

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_TIP_REQ.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_TIP_REQ.Items.Add(item);
        }
        DropDownList_TIP_REQ.DataBind();
    }

    private void cargar_DropDownList_NIV_ACADEMICO()
    {
        DropDownList_NIV_ACADEMICO.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_NIV_ESTUDIOS);

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_NIV_ACADEMICO.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_NIV_ACADEMICO.Items.Add(item);
        }
        DropDownList_NIV_ACADEMICO.DataBind();
    }
    private void cargar_DropDownList_EXPERIENCIA()
    {
        DropDownList_EXPERIENCIA.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_EXPERIENCIA);

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_EXPERIENCIA.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_EXPERIENCIA.Items.Add(item);
        }
        DropDownList_EXPERIENCIA.DataBind();
    }

    private void cargar_DropDownList_ID_EMPRESA()
    {
        DropDownList_ID_EMPRESA.Items.Clear();

        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaEmpresasActivas = _cliente.ObtenerTodasLasEmpresasActivas();

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_ID_EMPRESA.Items.Add(item);

        foreach (DataRow fila in tablaEmpresasActivas.Rows)
        {
            item = new ListItem(fila["RAZ_SOCIAL"].ToString(), fila["ID_EMPRESA"].ToString());
            DropDownList_ID_EMPRESA.Items.Add(item);
        }

        DropDownList_ID_EMPRESA.DataBind();
    }

    private void cargar_DropDownList_REGIONAL()
    {
        DropDownList_REGIONAL.Items.Clear();

        regional _regional = new regional(Session["idEmpresa"].ToString());
        DataTable tablaRegionales = _regional.ObtenerTodasLasRegionales();

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_REGIONAL.Items.Add(item);

        foreach (DataRow fila in tablaRegionales.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_REGIONAL"].ToString());
            DropDownList_REGIONAL.Items.Add(item);
        }

        DropDownList_REGIONAL.DataBind();
    }

    private void cargar_DropDownList_REGIONAL_ConRestriccion(Decimal idEmpresa, String usuLog)
    {
        DropDownList_REGIONAL.Items.Clear();

        regional _regional = new regional(Session["idEmpresa"].ToString());
        DataTable tablaRegionales = _regional.ObtenerTodasLasRegionalesConRestriccion(idEmpresa, usuLog);

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_REGIONAL.Items.Add(item);

        foreach (DataRow fila in tablaRegionales.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_REGIONAL"].ToString());
            DropDownList_REGIONAL.Items.Add(item);
        }

        DropDownList_REGIONAL.DataBind();
    }

    private void cargar_DropDownList_DEPARTAMENTO(String id)
    {
        DropDownList_DEPARTAMENTO.Items.Clear();

        departamento _departamento = new departamento(Session["idEmpresa"].ToString());
        DataTable tablaDepartamentos = _departamento.ObtenerDepartamentosPorIdRegional(id);

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_DEPARTAMENTO.Items.Add(item);

        foreach (DataRow fila in tablaDepartamentos.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_DEPARTAMENTO"].ToString());
            DropDownList_DEPARTAMENTO.Items.Add(item);
        }

        DropDownList_DEPARTAMENTO.DataBind();
    }


    private void cargar_DropDownList_DEPARTAMENTO_ConRestriccion(String id, Decimal ID_EMPRESA)
    {
        DropDownList_DEPARTAMENTO.Items.Clear();

        departamento _departamento = new departamento(Session["idEmpresa"].ToString());
        DataTable tablaDepartamentos = _departamento.ObtenerDepartamentosPorIdRegional_ConRestriccion(id, ID_EMPRESA, Session["USU_LOG"].ToString());

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_DEPARTAMENTO.Items.Add(item);

        foreach (DataRow fila in tablaDepartamentos.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_DEPARTAMENTO"].ToString());
            DropDownList_DEPARTAMENTO.Items.Add(item);
        }

        DropDownList_DEPARTAMENTO.DataBind();
    }


    private void cargar_DropDownList_CIUDAD_REQ(String idRegional, String idDepartamento)
    {
        DropDownList_CIUDAD_REQ.Items.Clear();

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
        DataTable tablaCiudades = _ciudad.ObtenerCiudadesPorIdDepartamentoEIdRegional(idRegional, idDepartamento);

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_CIUDAD_REQ.Items.Add(item);

        foreach (DataRow fila in tablaCiudades.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_CIUDAD"].ToString());
            DropDownList_CIUDAD_REQ.Items.Add(item);
        }

        DropDownList_CIUDAD_REQ.DataBind();
    }


    private void cargar_DropDownList_CIUDAD_REQ_ConRestriccion(String idRegional, String idDepartamento, Decimal ID_EMPRESA)
    {
        DropDownList_CIUDAD_REQ.Items.Clear();

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
        DataTable tablaCiudades = _ciudad.ObtenerCiudadesPorIdDepartamentoEIdRegional_ConRestricciones(idRegional, idDepartamento, ID_EMPRESA, Session["USU_LOG"].ToString());

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_CIUDAD_REQ.Items.Add(item);

        foreach (DataRow fila in tablaCiudades.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_CIUDAD"].ToString());
            DropDownList_CIUDAD_REQ.Items.Add(item);
        }

        DropDownList_CIUDAD_REQ.DataBind();
    }


    private void cargar_DropDownList_PERFILES(int ID_EMPRESA)
    {
        DropDownList_PERFILES.Items.Clear();

        perfil _perfil = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaOcupaciones = _perfil.ObtenerTodasLasOcupacionesPorIdEmpresa(ID_EMPRESA);

        ListItem item = new ListItem("Seleccione Cargo...", "");
        DropDownList_PERFILES.Items.Add(item);

        foreach (DataRow fila in tablaOcupaciones.Rows)
        {
            item = new ListItem(fila["ID_OCUPACION"].ToString() + "-" + fila["NOM_OCUPACION"].ToString(), fila["REGISTRO"].ToString());
            item.Attributes.Add("title", "ID: " + fila["ID_OCUPACION"].ToString() + " --> FUNCIONES: " + fila["DSC_FUNCIONES"].ToString().Trim());

            DropDownList_PERFILES.Items.Add(item);
        }

        DropDownList_PERFILES.DataBind();
    }



    private void cargar_DropDownList_PERFILES_InfoDePerfilEspecifico(Decimal REGISTRO_PERFIL)
    {
        DropDownList_PERFILES.Items.Clear();

        perfil _perfil = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaInfoPerfil = _perfil.ObtenerPorRegistro(REGISTRO_PERFIL);

        ListItem item = new ListItem("Seleccione Cargo...", "");
        DropDownList_PERFILES.Items.Add(item);

        foreach (DataRow fila in tablaInfoPerfil.Rows)
        {
            item = new ListItem(fila["ID_OCUPACION"].ToString() + "-" + fila["NOM_OCUPACION"].ToString(), fila["REGISTRO"].ToString());
            item.Attributes.Add("title", "ID: " + fila["ID_OCUPACION"].ToString() + " --> FUNCIONES: " + fila["DSC_FUNCIONES"].ToString().Trim());

            DropDownList_PERFILES.Items.Add(item);
        }

        DropDownList_PERFILES.DataBind();
    }



    private void cargar_DropDownList_ENVIO_CANDIDATOS(Decimal ID_EMPRESA)
    {
        DropDownList_ENVIO_CANDIDATOS.Items.Clear();

        envioCandidato _envioCandidato = new envioCandidato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaEnvioCandidatos = _envioCandidato.ObtenerTodosLosContactosParaEnvioDeCandidatosPorIdEmpresa(ID_EMPRESA);

        ListItem item = new ListItem("Seleccione solicitante", "");
        DropDownList_ENVIO_CANDIDATOS.Items.Add(item);

        foreach (DataRow fila in tablaEnvioCandidatos.Rows)
        {
            item = new ListItem(fila["NOMBRE_CONTACTO"].ToString(), fila["REGISTRO"].ToString());
            DropDownList_ENVIO_CANDIDATOS.Items.Add(item);
        }

        DropDownList_REGIONAL.DataBind();
    }
    private void cargar_DropDownList_NIV_EDUCACION()
    {
        DropDownList_NIV_EDUCACION.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_NIV_ESTUDIOS);

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_NIV_EDUCACION.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_NIV_EDUCACION.Items.Add(item);
        }
        DropDownList_NIV_EDUCACION.DataBind();
    }
    private void cargar_DropDownList_SEXO()
    {
        DropDownList_SEXO.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_SEXOREQ);

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_SEXO.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_SEXO.Items.Add(item);
        }

        DropDownList_SEXO.DataBind();
    }

    private void cargar_GridView_CONTRATOS_ACTIVOS(Decimal ID_EMPRESA)
    { 

        ServicioRespectivo _sr = new ServicioRespectivo(Session["idEmpresa"].ToString());

        DataTable tablaServiciosRespectivosVigentesPorEmpresa = _sr.ObtenerServicioRespectivosVigentesPorIdEmpresa(ID_EMPRESA);

        if (tablaServiciosRespectivosVigentesPorEmpresa.Rows.Count <= 0)
        {
            configurarMensajes(true, System.Drawing.Color.Orange);
            Label_MENSAJE.Text = "No se encontraron Servicios Respectivos Activos para la empresa seleccionada.";

            Panel_CONTRATOS_Y_SERVICIOS.Visible = false;
        }
        else
        {
            Panel_CONTRATOS_Y_SERVICIOS.Visible = true;
            Panel_GRILLA_CONTRATOS_ACTIVOS.Visible = true;
            GridView_CONTRATOS_ACTIVOS.DataSource = tablaServiciosRespectivosVigentesPorEmpresa;
            GridView_CONTRATOS_ACTIVOS.DataBind();

            Panel_CONTRATOS_Y_SERVICIOS.Visible = true;
        }

        HiddenField_ID_SERVICIO_RESPECTIVO.Value = string.Empty;
    }

    private void cargar_DropDownList_DropDownList_DURACION()
    {
        DropDownList_DURACION.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_CLASE_CONTRATO);

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_DURACION.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_DURACION.Items.Add(item);
        }
        DropDownList_DURACION.DataBind();
    }
    private void cargar_DropDownList_HORARIOO()
    {
        DropDownList_HORARIO.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_TIPO_CONTRATO);

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_HORARIO.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_HORARIO.Items.Add(item);
        }
        DropDownList_HORARIO.DataBind();
    }
    #endregion

    #region metodos para configurar controles
    private void configurarCaracteresAceptadosBusqueda(Boolean letras, Boolean numeros)
    {
        FilteredTextBoxExtender_TextBox_BUSCAR_Numbers.Enabled = numeros;
        FilteredTextBoxExtender_TextBox_BUSCAR_Letras.Enabled = letras;
    }
    private void iniciar_seccion_de_busqueda()
    {
        TextBox_BUSCAR.Text = "";
        cargar_DropDownList_BUSCAR();
    }

    private void configurarBotonesDeAccion(Boolean bNuevo, Boolean bModificar, Boolean bGuardar, Boolean bCancelar, Boolean bVolver)
    {
        Button_NUEVO.Visible = bNuevo;
        Button_NUEVO.Enabled = bNuevo;
        Button_NUEVO_1.Visible = bNuevo;
        Button_NUEVO_1.Enabled = bNuevo;

        Boolean puedeModificar = true;

        if (bModificar == true)
        {
            seguridad _s = new seguridad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            DataTable tablaRestriccion = _s.UsuarioConRestriccionTopoReq(Session["USU_LOG"].ToString());

            if (tablaRestriccion.Rows.Count > 0)
            {
                DataRow fila = tablaRestriccion.Rows[0];

                if (fila["RESTRICCION"].ToString().Trim().ToUpper() == "TRUE")
                {
                    if (DropDownList_TIP_REQ.SelectedValue != "USUARIO")
                    {
                        puedeModificar = false;
                    }
                 }
            }

            Button_MODIFICAR.Visible = puedeModificar;
            Button_MODIFICAR.Enabled = puedeModificar;
            Button_MODIFICAR_1.Visible = puedeModificar;
            Button_MODIFICAR_1.Enabled = puedeModificar;

        }
        else
        {
            Button_MODIFICAR.Visible = bModificar;
            Button_MODIFICAR.Enabled = bModificar;
            Button_MODIFICAR_1.Visible = bModificar;
            Button_MODIFICAR_1.Enabled = bModificar;
        }
        
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
            Label_MENSAJE.Font.Bold = true;

            if (color == System.Drawing.Color.Green)
            {
                Image_MENSAJE_POPUP.ImageUrl = "~/imagenes/plantilla/ok_popup.png";
            }
            else
            {
                if (color == System.Drawing.Color.Orange)
                {
                    Image_MENSAJE_POPUP.ImageUrl = "~/imagenes/plantilla/advertencia_popup.png";
                }
                else
                {
                    Image_MENSAJE_POPUP.ImageUrl = "~/imagenes/plantilla/error_popup.png";
                }
                
            }
        }
        Panel_FONDO_MENSAJE.Visible = mostrarMensaje;
        Panel_MENSAJES.Visible = mostrarMensaje;
    }

    
    
    private void configurarInfoAdicionalModulo(Boolean ver, String texto)
    {
        Panel_INFO_ADICIONAL_MODULO.Visible = ver;
        Label_INFO_ADICIONAL_MODULO.Text = texto;
    }
    private void configurarPaneles(Boolean bPanelTrazavilidad, Boolean bPanel_GRID_ENVIAR_A_CLIENTE, Boolean bPanel_GRID_CANDIDATOS_EN_CLIENTE, Boolean bPanel_GRID_HISTORIAL, Boolean bPanel_GRID_SEGUIMIENTO, Boolean bPanel_BOTONES_ACCION_1)
    {
        PanelSeguimientoDesdeReclutamiento.Visible = bPanelTrazavilidad; 
        Panel_GRID_ENVIAR_A_CLIENTE.Visible = bPanel_GRID_ENVIAR_A_CLIENTE;
        Panel_GRID_CANDIDATOS_EN_CLIENTE.Visible = bPanel_GRID_CANDIDATOS_EN_CLIENTE;
        Panel_GRID_HISTORIAL.Visible = bPanel_GRID_HISTORIAL;
        Panel_GRID_SEGUIMIENTO.Visible = bPanel_GRID_SEGUIMIENTO;
        Panel_BOTONES_ACCION_1.Visible = bPanel_BOTONES_ACCION_1;
    }
    private void configurarPanelesInternosEnviarACliente(Boolean bPanelTrazavilidad, Boolean bPanel_GRILLA_ENVIAR_A_CLIENTE, Boolean bPanel_INTERNO_FILTRO_VARIACION_PERFIL, Boolean bPanel_INTERNO_FILTRO_CEDULA)
    {
        Panel_GRILLA_ENVIAR_A_CLIENTE.Visible = bPanel_GRILLA_ENVIAR_A_CLIENTE;
        Panel_INTERNO_FILTRO_VARIACION_PERFIL.Visible = bPanel_INTERNO_FILTRO_VARIACION_PERFIL;
        Panel_INTERNO_FILTRO_CEDULA.Visible = bPanel_INTERNO_FILTRO_CEDULA;
    }
    private void configurarChecksEnviarACliente(Boolean bPanelTrazavilidad, Boolean bCheckBox_ENVIAR_CANDIDATOS_FILTRO_ACIDO, Boolean bCheckBox_ENVIAR_CANDIDATOS_FILTRO_VARIACION_PERFIL, Boolean bCheckBox_ENVIAR_CANDIDATOS_FILTRO_CEDULA)
    {
        CheckBox_ENVIAR_CANDIDATOS_FILTRO_ACIDO.Checked = bCheckBox_ENVIAR_CANDIDATOS_FILTRO_ACIDO;
        CheckBox_ENVIAR_CANDIDATOS_FILTRO_VARIACION_PERFIL.Checked = bCheckBox_ENVIAR_CANDIDATOS_FILTRO_VARIACION_PERFIL;
        CheckBox_ENVIAR_CANDIDATOS_FILTRO_CEDULA.Checked = bCheckBox_ENVIAR_CANDIDATOS_FILTRO_CEDULA;
    }
    private void inhabilitar_DropDownList_ENVIO_CANDIDATOS()
    {
        DropDownList_ENVIO_CANDIDATOS.Enabled = false;
        DropDownList_ENVIO_CANDIDATOS.Items.Clear();
        ListItem item = new ListItem("Seleccione solicitante", "");
        DropDownList_ENVIO_CANDIDATOS.Items.Add(item);
        DropDownList_ENVIO_CANDIDATOS.DataBind();
    }
    private void inhabilitar_DropDownList_DEPARTAMENTO()
    {
        DropDownList_DEPARTAMENTO.Enabled = false;
        DropDownList_DEPARTAMENTO.Items.Clear();
        ListItem item = new ListItem("Seleccione Departamento", "");
        DropDownList_DEPARTAMENTO.Items.Add(item);
        DropDownList_DEPARTAMENTO.DataBind();
    }
    private void inhabilitar_DropDownList_CIUDAD_REQ()
    {
        DropDownList_CIUDAD_REQ.Enabled = false;
        DropDownList_CIUDAD_REQ.Items.Clear();
        ListItem item = new ListItem("Seleccione Ciudad", "");
        DropDownList_CIUDAD_REQ.Items.Add(item);
        DropDownList_CIUDAD_REQ.DataBind();
    }
    private void inhabilitar_DropDownList_PERFILES()
    {
        DropDownList_PERFILES.Enabled = false;
        DropDownList_PERFILES.Items.Clear();
        ListItem item = new ListItem("Seleccione Cargo...", "");
        DropDownList_PERFILES.Items.Add(item);
        DropDownList_PERFILES.DataBind();
    }

    private void iniciar_controles_formulario_nuevo_req()
    {

        cargar_DropDownList_TIP_REQ_con_filtro_usaurio();

        cargar_DropDownList_ID_EMPRESA();

        inhabilitar_DropDownList_ENVIO_CANDIDATOS();
        TextBox_DIR_ENVIO.Text = "";
        TextBox_DIR_ENVIO.Enabled = false;
        TextBox_CIUDAD_ENVIO.Text = "";
        TextBox_CIUDAD_ENVIO.Enabled = false;
        TextBox_TEL_ENVIO.Text = "";
        TextBox_TEL_ENVIO.Enabled = false;
        TextBox_EMAIL_ENVIO.Text = "";
        TextBox_EMAIL_ENVIO.Enabled = false;
        TextBox_COND_ENVIO.Text = "";
        TextBox_COND_ENVIO.Enabled = false;

        Panel_GRILLA_CONTRATOS_ACTIVOS.Visible = false;
        Panel_CONTRATOS_Y_SERVICIOS.Visible = false;

        TextBox_FECHA_R.Enabled = false;
        TextBox_FECHA_R.Text = DateTime.Now.ToShortDateString();

        TextBox_FECHA_REQUERIDA.Enabled = true;
        TextBox_FECHA_REQUERIDA.Text = "";
        RangeValidator_TextBox_FECHA_REQUERIDA.MinimumValue = DateTime.Today.ToShortDateString();
        RangeValidator_TextBox_FECHA_REQUERIDA.MaximumValue = DateTime.Now.AddYears(1).ToShortDateString();

        TextBox_FechaReferenciaSistema.Enabled = false;
        TextBox_FechaReferenciaSistema.Text = "";

        DropDownList_REGIONAL.Enabled = false;
        DropDownList_REGIONAL.ClearSelection();

        inhabilitar_DropDownList_DEPARTAMENTO();

        inhabilitar_DropDownList_CIUDAD_REQ();

        HiddenField_NivelRequerimiento.Value = NivelesRequerimiento.Baja.ToString();
        inhabilitar_DropDownList_PERFILES();
        TextBox_EDAD_MIN.Text = "";
        TextBox_EDAD_MIN.Enabled = false;
        TextBox_EDAD_MAX.Text = "";
        TextBox_EDAD_MAX.Enabled = false;
        TextBox_CANTIDAD.Text = "";
        TextBox_CANTIDAD.Enabled = false;
        TextBox_SEXO.Text = "";
        TextBox_SEXO.Enabled = false;
        TextBox_EXPERIENCIA.Text = "";
        TextBox_EXPERIENCIA.Enabled = false;

        cargar_DropDownList_NIV_ACADEMICO();
        DropDownList_NIV_ACADEMICO.Enabled = false;

        TextBox_FUNCIONES.Text = "";
        TextBox_FUNCIONES.Enabled = false;

        cargar_DropDownList_DropDownList_DURACION();
        cargar_DropDownList_HORARIOO();
        TextBox_SALARIO.Text = "";
    }
    #endregion

    #region METODOS PARA OBTENERDATOS DE LA BD
    private DataRow obtenerDatosCiudadCliente(String idCiudad)
    {
        DataRow resultado = null;

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());

        DataTable tablaEmpresa = _ciudad.ObtenerCiudadPorIdCiudad(idCiudad);

        if (tablaEmpresa.Rows.Count > 0)
        {
            resultado = tablaEmpresa.Rows[0];
        }

        return resultado;
    }
    private DataRow ObtenerEnvioContactoVigentePorIdEmpresa(Decimal ID_EMPRESA)
    {
        DataRow resultado = null;

        envioCandidato _envioCandidato = new envioCandidato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaEnvioInmediato = _envioCandidato.ObtenerEnvioContactoVigentePorIdEmpresa(ID_EMPRESA);

        if (tablaEnvioInmediato.Rows.Count > 0)
        {
            resultado = tablaEnvioInmediato.Rows[0];
        }

        return resultado;
    }
    private DataRow ObtenerContratoDeServicioVigentePorIdEmpresa(Decimal ID_EMPRESA)
    {
        DataRow resultado = null;

        contratosServicio _contratosServicio = new contratosServicio(Session["idEmpresa"].ToString());

        DataTable tablaContratoVigente = _contratosServicio.ObtenerContratoDeServicioVigentePorIdEmpresa(ID_EMPRESA);

        if (tablaContratoVigente.Rows.Count > 0)
        {
            resultado = tablaContratoVigente.Rows[0];
        }

        return resultado;
    }
    private DataRow ObtenerEnvioDeCandidatoPorRegistro(Decimal REGISTRO)
    {
        DataRow resultado = null;

        envioCandidato _envioCandidato = new envioCandidato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoSolicitante = _envioCandidato.ObtenerEnvioDeCandidatoPorRegistro(REGISTRO);

        if (tablaInfoSolicitante.Rows.Count > 0)
        {
            resultado = tablaInfoSolicitante.Rows[0];
        }

        return resultado;
    }
    private DataRow ObtenerPerfilPorRegistro(int REGISTRO)
    {
        DataRow resultado = null;

        perfil _perfil = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoPerfil = _perfil.ObtenerPorRegistro(REGISTRO);

        if (tablaInfoPerfil.Rows.Count > 0)
        {
            resultado = tablaInfoPerfil.Rows[0];
        }

        return resultado;
    }
    private String obtenerNivelEstudiosPerfil(String ID_NIVEL)
    {
        String resultado = "Problema de migración";
        parametro _parametro = new parametro(Session["idEmpresa"].ToString());
        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_NIV_ESTUDIOS);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            if (fila["CODIGO"].ToString().Trim() == ID_NIVEL.ToString().Trim())
            {
                resultado = fila["DESCRIPCION"].ToString().Trim();
                break;
            }
        }

        return resultado;
    }
    private Int32 obtenerParametroAvisoDiasVencimientoContrato()
    {
        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametro;

        tablaParametro = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_AVISO_OBJETO_CONTRATO);

        if (tablaParametro.Rows.Count <= 0)
        {
            return 1;
        }
        else
        {
            DataRow filaTabla = tablaParametro.Rows[0];

            return Convert.ToInt32(filaTabla["CODIGO"]);
        }
    }
    #endregion

    #region metodos que se cargan al iniciar la web
    private void iniciar_interfaz_inicial()
    {
        Panel_FORM_BOTONES.Visible = true;

        configurarBotonesDeAccion(true, false, false, false, true);
        Panel_BOTONES_ACCION_1.Visible = false;

        Panel_GRID_RESULTADOS.Visible = false;

        Panel_FORMULARIO.Visible = false;

        Panel_BOTONES_ACCION_1.Visible = false;
        Panel_BOTONES_ACCIONES_REQUISICION.Visible = false;
        Panel_GRID_ENVIAR_A_CLIENTE.Visible = false;
        Panel_GRID_CANDIDATOS_EN_CLIENTE.Visible = false;
        Panel_GRID_HISTORIAL.Visible = false;
        Panel_GRID_SEGUIMIENTO.Visible = false;
    }
    private void iniciar_interfaz_nuevo()
    {
        configurarInfoAdicionalModulo(false, "");

        Panel_FORM_BOTONES.Visible = true;

        configurarBotonesDeAccion(false, false, true, true, true);
        Panel_BOTONES_ACCION_1.Visible = true;

        Panel_GRID_RESULTADOS.Visible = false;

        Panel_FORMULARIO.Visible = true;
        Panel_FORMULARIO.Enabled = true;

        Panel_CONTROL_REGISTRO.Visible = false;

        Panel_ID_REQUERIMIENTO.Visible = false;

        Panel_InformacionEnvioEmail.Visible = true;
        Panel_EnvioEmailIndeterminado.Visible = true;
        Label_EnvioEmailIndeterminado.Text = _mensaje_envioEmailIndeterminado;

        iniciar_controles_formulario_nuevo_req();

        Panel_BOTONES_ACCIONES_REQUISICION.Visible = false;
        Panel_GRID_ENVIAR_A_CLIENTE.Visible = false;
        Panel_GRID_CANDIDATOS_EN_CLIENTE.Visible = false;
        Panel_GRID_HISTORIAL.Visible = false;
        Panel_GRID_SEGUIMIENTO.Visible = false;
    }

    private Boolean seleccionar_fila_grilla_contratos_por_registro(Decimal REGISTRO, GridView grilla)
    {
        Boolean resultado = false;

        for (int i = 0; i < grilla.Rows.Count; i++)
        {
            if (Convert.ToDecimal(grilla.DataKeys[i].Values["ID_SERVICIO_RESPECTIVO"]) == REGISTRO)
            {
                grilla.Rows[i].BackColor = colorSeleccionado;
                resultado = true;

                HiddenField_ID_SERVICIO_RESPECTIVO.Value = REGISTRO.ToString();

                grilla.SelectedIndex = i;
            }
            else
            {
                grilla.Rows[i].BackColor = System.Drawing.Color.Transparent;
            }
        }

        return resultado;
    }

    private void CargarControlRegistro(DataRow filaRequisicion)
    {
        TextBox_USU_CRE.Text = filaRequisicion["USU_CRE"].ToString();
        try
        {
            TextBox_FCH_CRE.Text = DateTime.Parse(filaRequisicion["FCH_CRE"].ToString()).ToShortDateString();
            TextBox_HOR_CRE.Text = DateTime.Parse(filaRequisicion["FCH_CRE"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_CRE.Text = "";
            TextBox_HOR_CRE.Text = "";
        }
        TextBox_USU_MOD.Text = filaRequisicion["USU_MOD"].ToString();
        try
        {
            TextBox_FCH_MOD.Text = DateTime.Parse(filaRequisicion["FCH_MOD"].ToString()).ToShortDateString();
            TextBox_HOR_MOD.Text = DateTime.Parse(filaRequisicion["FCH_MOD"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_MOD.Text = "";
            TextBox_HOR_MOD.Text = "";
        }
    }

    private void CargarDatosEstadoRequisicion(Boolean modificar, DataRow filaRequisicion)
    {
        DateTime fechaVencimiento;

        DateTime fechaHoy = Convert.ToDateTime(DateTime.Now.ToShortDateString());

        Int32 diasAviso = obtenerParametroAvisoDiasVencimientoContrato();

    
        if (modificar == false)
        {
            configurarPaneles(false, false, false, false, false, true);

            if ((filaRequisicion["CUMPLIDO"].ToString() == "N") && (filaRequisicion["CANCELADO"].ToString() == "N"))
            {

                if (HiddenField_ID_SERVICIO_RESPECTIVO.Value == "")
                {
                    fechaVencimiento = new DateTime();
                }
                else
                {
                    fechaVencimiento = Convert.ToDateTime(GridView_CONTRATOS_ACTIVOS.SelectedRow.Cells[3].Text);
                }

                if (fechaVencimiento < fechaHoy)
                {
                    configurarMensajes(true, System.Drawing.Color.Red);
                    Label_MENSAJE.Text = "El Servicio Respectivo asociado a la requisición está vencido ó no se pudo determinar.";
                    Panel_BOTONES_ACCIONES_REQUISICION.Visible = false;
                }
                else
                {
                    if (fechaVencimiento.AddDays(-diasAviso) <= fechaHoy)
                    {
                        configurarMensajes(true, System.Drawing.Color.Orange);
                        Label_MENSAJE.Text = "El Servicio Respectivo asociado a la requisición está por vencer.";
                        Panel_BOTONES_ACCIONES_REQUISICION.Visible = true;
                    }
                    else
                    {
                        Panel_BOTONES_ACCIONES_REQUISICION.Visible = true;
                    }
                }

                seguridad _s = new seguridad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                DataTable tablaRestriccion = _s.UsuarioConRestriccionTopoReq(Session["USU_LOG"].ToString());

                if (tablaRestriccion.Rows.Count <= 0)
                {
                    Button_COPIAR_REQ.Visible = true;
                }
                else
                {
                    DataRow fila = tablaRestriccion.Rows[0];

                    if (fila["RESTRICCION"].ToString().Trim().ToUpper() == "TRUE")
                    {
                        if (DropDownList_TIP_REQ.SelectedValue != "USUARIO")
                        {
                            Button_COPIAR_REQ.Visible = false;
                        }
                        else
                        {
                            Button_COPIAR_REQ.Visible = true;
                        }
                    }
                    else
                    {
                        Button_COPIAR_REQ.Visible = true;
                    }
                }
            }
            else
            {
                Panel_BOTONES_ACCIONES_REQUISICION.Visible = false;

                if (filaRequisicion["CUMPLIDO"].ToString().ToUpper() == "S")
                {
                    configurarMensajes(true, System.Drawing.Color.Orange);
                    Label_MENSAJE.Text = "La requisición NO puede ser editada porque su estado es CUMPLIDA por: " + filaRequisicion["MOTIVO_CUMPLIDO"].ToString().Trim() + ".";
                }
                else
                {
                    if (filaRequisicion["CANCELADO"].ToString().ToUpper() == "S")
                    {
                        configurarMensajes(true, System.Drawing.Color.Orange);
                        Label_MENSAJE.Text = "La requisición NO puede ser editada porque su estado es CANCELADA por: " + filaRequisicion["MOTIVO_CANCELA"].ToString().Trim() + ".";
                    }
                    else
                    {
                        configurarMensajes(true, System.Drawing.Color.Red);
                        Label_MENSAJE.Text = "La requisición NO puede ser editada porque su estado es DESCONOCIDO.";
                    }
                }
            }
        }
        else
        {
            configurarPaneles(false, false, false, false, false, true);
            Panel_BOTONES_ACCIONES_REQUISICION.Visible = false;
        }


        if (modificar == true)
        {
            configurarBotonesDeAccion(false, false, true, true, true); 
            Panel_BOTONES_ACCION_1.Visible = true;
        }
        else
        {
            Panel_BOTONES_ACCION_1.Visible = false;
            if ((filaRequisicion["CUMPLIDO"].ToString().ToUpper() == "N") && (filaRequisicion["CANCELADO"].ToString().ToUpper() == "N"))
            {
                if (HiddenField_ID_SERVICIO_RESPECTIVO.Value == "")
                {
                    fechaVencimiento = new DateTime();
                }
                else
                {
                    fechaVencimiento = Convert.ToDateTime(GridView_CONTRATOS_ACTIVOS.SelectedRow.Cells[3].Text);
                }

                if (fechaVencimiento < fechaHoy)
                {
                    configurarBotonesDeAccion(true, true, false, false, true);
                }
                else
                {
                    if (fechaVencimiento.AddDays(-diasAviso) <= fechaHoy)
                    {
                        configurarBotonesDeAccion(true, true, false, false, true);
                    }
                    else
                    {
                        configurarBotonesDeAccion(true, true, false, false, true);
                    }
                }
            }
            else
            {
                configurarBotonesDeAccion(true, false, false, false, true);
            }
        }

        Button_ENVIAR_CANDIDATOS.Visible = true;
    }

    private void CargarDatosRequisicion(DataRow filaRequisicion, String SI_COPIA_REQ, Boolean modificar)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        TextBox_ID_REQUERIMIENTO.Text = filaRequisicion["ID_REQUERIMIENTO"].ToString().Trim();

        CargarControlRegistro(filaRequisicion);

        if (modificar == true)
        {
            cargar_DropDownList_TIP_REQ_con_filtro_usaurio();
        }
        else
        {
            cargar_DropDownList_TIP_REQ();
        }
        
        try 
        { 
            DropDownList_TIP_REQ.SelectedValue = filaRequisicion["TIPO_REQ"].ToString().Trim(); 
        }
        catch 
        {
            DropDownList_TIP_REQ.ClearSelection();
        }

        cargar_DropDownList_ID_EMPRESA();
        try { DropDownList_ID_EMPRESA.SelectedValue = filaRequisicion["ID_EMPRESA"].ToString().Trim(); }
        catch { DropDownList_ID_EMPRESA.SelectedIndex = 0; }

        Decimal ID_EMPRESA = 0;

        if (DropDownList_ID_EMPRESA.SelectedValue == "")
        {
            TextBox_DIR_ENVIO.Text = "";
            TextBox_CIUDAD_ENVIO.Text = "";
            TextBox_TEL_ENVIO.Text = "";
            TextBox_EMAIL_ENVIO.Text = "";
            TextBox_COND_ENVIO.Text = "";

            HiddenField_ID_SERVICIO_RESPECTIVO.Value = "";
            
            TextBox_EDAD_MIN.Text = "";
            TextBox_EDAD_MAX.Text = "";
            TextBox_CANTIDAD.Text = "";
            TextBox_SEXO.Text = "";
            TextBox_EXPERIENCIA.Text = "";
            
            cargar_DropDownList_NIV_ACADEMICO();
            
            TextBox_FUNCIONES.Text = "";

            HiddenField_NivelRequerimiento.Value = NivelesRequerimiento.Baja.ToString();
        }
        else
        {
            ID_EMPRESA = Convert.ToDecimal(DropDownList_ID_EMPRESA.SelectedValue);

            cargar_DropDownList_ENVIO_CANDIDATOS(ID_EMPRESA);
            
            Decimal REGISTRO_ENVIO = 0;
            try
            {
                REGISTRO_ENVIO = Convert.ToDecimal(filaRequisicion["REGISTRO_ENVIO_CANDIDATOS"]);
            }
            catch
            {
                REGISTRO_ENVIO = 0;
            }

            DataRow filaInfoCandidatoReq = null;
            if (REGISTRO_ENVIO != 0)
            {
                filaInfoCandidatoReq = ObtenerEnvioDeCandidatoPorRegistro(REGISTRO_ENVIO);
            }

            if (filaInfoCandidatoReq == null)
            {
                DropDownList_ENVIO_CANDIDATOS.SelectedValue = "";
                TextBox_DIR_ENVIO.Text = "";
                TextBox_CIUDAD_ENVIO.Text = "";
                TextBox_TEL_ENVIO.Text = "";
                TextBox_EMAIL_ENVIO.Text = "";
                TextBox_COND_ENVIO.Text = "";
            }
            else
            {
                DropDownList_ENVIO_CANDIDATOS.SelectedValue = REGISTRO_ENVIO.ToString();
                TextBox_DIR_ENVIO.Text = filaInfoCandidatoReq["DIR_ENVIO"].ToString().Trim();
                TextBox_CIUDAD_ENVIO.Text = filaInfoCandidatoReq["NOMBRE_CIUDAD"].ToString().Trim();
                TextBox_TEL_ENVIO.Text = filaInfoCandidatoReq["TEL_ENVIO"].ToString().Trim();
                TextBox_EMAIL_ENVIO.Text = filaInfoCandidatoReq["CONT_MAIL"].ToString().Trim();
                TextBox_COND_ENVIO.Text = filaInfoCandidatoReq["COND_ENVIO"].ToString().Trim();
            }

            Decimal ID_SERVICIO_RESPECTIVO = 0;

            try
            {
                ID_SERVICIO_RESPECTIVO = Convert.ToDecimal(filaRequisicion["ID_SERVICIO_RESPECTIVO"]);
            }
            catch
            {
                ID_SERVICIO_RESPECTIVO = 0;
            }

            if (ID_SERVICIO_RESPECTIVO == 0)
            {
                if (QueryStringSeguro["accion"].ToString() == "copiarReq")
                {
                    cargar_GridView_CONTRATOS_ACTIVOS(ID_EMPRESA);
                    HiddenField_ID_SERVICIO_RESPECTIVO.Value = null;
                }
                else
                {
                    configurarMensajes(true, System.Drawing.Color.Orange);
                    Label_MENSAJE.Text = "No se encontró información del Servicio Respectivo asociado.";

                    cargar_GridView_CONTRATOS_ACTIVOS(ID_EMPRESA);
                    HiddenField_ID_SERVICIO_RESPECTIVO.Value = null;
                }
            }
            else
            {
                cargar_GridView_CONTRATOS_ACTIVOS(ID_EMPRESA);

                if (seleccionar_fila_grilla_contratos_por_registro(ID_SERVICIO_RESPECTIVO, GridView_CONTRATOS_ACTIVOS) == false)
                {
                    HiddenField_ID_SERVICIO_RESPECTIVO.Value = string.Empty;
                }
            }

            Decimal REGISTRO_PERFIL = 0;
            try
            {
                REGISTRO_PERFIL = Convert.ToDecimal(filaRequisicion["REGISTRO_PERFIL"]);
            }
            catch
            {
                REGISTRO_PERFIL = 0;
            }

            DataRow filaInfoPerfil = null;
            if (REGISTRO_PERFIL != 0)
            {
                cargar_DropDownList_PERFILES_InfoDePerfilEspecifico(REGISTRO_PERFIL);

                filaInfoPerfil = ObtenerPerfilPorRegistro(Convert.ToInt32(REGISTRO_PERFIL));
                DropDownList_PERFILES.SelectedValue = filaInfoPerfil["REGISTRO"].ToString();

                TextBox_EDAD_MIN.Text = filaInfoPerfil["EDAD_MIN"].ToString().Trim();
                TextBox_EDAD_MAX.Text = filaInfoPerfil["EDAD_MAX"].ToString().Trim();
                TextBox_CANTIDAD.Text = filaRequisicion["CANTIDAD"].ToString().Trim();

                TextBox_SEXO.Text = filaInfoPerfil["SEXO"].ToString().Trim();
                TextBox_EXPERIENCIA.Text = filaInfoPerfil["EXPERIENCIA"].ToString().Trim();

                cargar_DropDownList_NIV_ACADEMICO();
                DropDownList_NIV_ACADEMICO.SelectedValue = filaInfoPerfil["NIV_ESTUDIOS"].ToString().Trim();

                TextBox_FUNCIONES.Text = filaInfoPerfil["DSC_FUNCIONES"].ToString().Trim();

                HiddenField_NivelRequerimiento.Value = NivelesRequerimiento.Baja.ToString();
                if (filaInfoPerfil["NIVEL_REQUERIMIENTO"].ToString().ToUpper() == "COMPLEJA")
                {
                    HiddenField_NivelRequerimiento.Value = NivelesRequerimiento.Compleja.ToString();
                }
                else
                {
                    if (filaInfoPerfil["NIVEL_REQUERIMIENTO"].ToString().ToUpper() == "MEDIA")
                    {
                        HiddenField_NivelRequerimiento.Value = NivelesRequerimiento.Media.ToString();
                    }
                    else
                    {
                        if (filaInfoPerfil["NIVEL_REQUERIMIENTO"].ToString().ToUpper() == "BAJA")
                        {
                            HiddenField_NivelRequerimiento.Value = NivelesRequerimiento.Baja.ToString();
                        }
                    }
                }
            }
            else
            {
                DropDownList_PERFILES.Items.Clear();
                DropDownList_PERFILES.Items.Add(new ListItem("Requisición sin Perfil",""));

                TextBox_EDAD_MIN.Text = "";
                TextBox_EDAD_MAX.Text = "";
                TextBox_CANTIDAD.Text = "";

                TextBox_SEXO.Text = "";
                TextBox_EXPERIENCIA.Text = "";

                cargar_DropDownList_NIV_ACADEMICO();

                TextBox_FUNCIONES.Text = "";
            }
        }

        try
        {
            if (SI_COPIA_REQ != null)
            {
                TextBox_FECHA_R.Text = DateTime.Now.ToShortDateString();
            }
            else
            {
                TextBox_FECHA_R.Text = Convert.ToDateTime(filaRequisicion["FECHA_R"]).ToShortDateString();
            }
        }
        catch
        {
            if (SI_COPIA_REQ != null)
            {
                TextBox_FECHA_R.Text = DateTime.Now.ToShortDateString();
            }
            else
            {
                TextBox_FECHA_R.Text = "";
            }
        }

        if (SI_COPIA_REQ != null)
        {
            TextBox_FECHA_REQUERIDA.Text = "";
        }
        else 
        {
            try
            {
                TextBox_FECHA_REQUERIDA.Text = Convert.ToDateTime(filaRequisicion["FECHA_REQUERIDA"]).ToShortDateString();
            }
            catch
            {
                TextBox_FECHA_REQUERIDA.Text = "";
            }
        }
        
        if (SI_COPIA_REQ != null)
        {
            if ((DropDownList_PERFILES.SelectedValue == "") || (TextBox_CANTIDAD.Text == ""))
            {
                TextBox_FechaReferenciaSistema.Text = "";
            }
            else
            {
                Int32 cantidad = Convert.ToInt32(TextBox_CANTIDAD.Text);
                String nivelComplejidad = HiddenField_NivelRequerimiento.Value;

                DateTime fechaRequiereCliente = GetFechaRequiereCliente(cantidad, nivelComplejidad);

                if (fechaRequiereCliente == new DateTime())
                {
                    TextBox_FechaReferenciaSistema.Text = "";
                }
                else
                {
                    TextBox_FechaReferenciaSistema.Text = fechaRequiereCliente.ToShortDateString();
                }
            }
        }
        else
        {
            try
            {
                TextBox_FechaReferenciaSistema.Text = Convert.ToDateTime(filaRequisicion["FECHA_REFERENCIA_SISTEMA"]).ToShortDateString();
            }
            catch
            {
                if ((DropDownList_PERFILES.SelectedValue == "") || (TextBox_CANTIDAD.Text == ""))
                {
                    TextBox_FechaReferenciaSistema.Text = "";
                }
                else
                {
                    Int32 cantidad = Convert.ToInt32(TextBox_CANTIDAD.Text);
                    String nivelComplejidad = HiddenField_NivelRequerimiento.Value;

                    DateTime fechaRequiereCliente = GetFechaRequiereCliente(cantidad, nivelComplejidad);

                    if (fechaRequiereCliente == new DateTime())
                    {
                        TextBox_FechaReferenciaSistema.Text = "";
                    }
                    else
                    {
                        TextBox_FechaReferenciaSistema.Text = fechaRequiereCliente.ToShortDateString();
                    }
                }
            }
        }

        DataRow filaInfoCiudadEmpresa = obtenerDatosCiudadCliente(filaRequisicion["CIUDAD_REQ"].ToString().Trim());
        if (filaInfoCiudadEmpresa != null)
        {
            cargar_DropDownList_REGIONAL();
            DropDownList_REGIONAL.SelectedValue = filaInfoCiudadEmpresa["ID_REGIONAL"].ToString().Trim();
            DropDownList_REGIONAL.DataBind();
            cargar_DropDownList_DEPARTAMENTO(filaInfoCiudadEmpresa["ID_REGIONAL"].ToString().Trim());
            DropDownList_DEPARTAMENTO.SelectedValue = filaInfoCiudadEmpresa["ID_DEPARTAMENTO"].ToString().Trim();
            DropDownList_DEPARTAMENTO.DataBind();
            cargar_DropDownList_CIUDAD_REQ(filaInfoCiudadEmpresa["ID_REGIONAL"].ToString().Trim(), filaInfoCiudadEmpresa["ID_DEPARTAMENTO"].ToString().Trim());
            DropDownList_CIUDAD_REQ.SelectedValue = filaInfoCiudadEmpresa["ID_CIUDAD"].ToString().Trim();
            DropDownList_CIUDAD_REQ.DataBind();
        }
        else
        {
            cargar_DropDownList_REGIONAL_ConRestriccion(ID_EMPRESA, Session["USU_LOG"].ToString());
            inhabilitar_DropDownList_DEPARTAMENTO();
            inhabilitar_DropDownList_CIUDAD_REQ();
        }

        cargar_DropDownList_DropDownList_DURACION();
        DropDownList_DURACION.SelectedValue = filaRequisicion["DURACION"].ToString().Trim();

        cargar_DropDownList_HORARIOO();
        DropDownList_HORARIO.SelectedValue = filaRequisicion["HORARIO"].ToString().Trim();

        Decimal SALARIO = Convert.ToDecimal(filaRequisicion["SALARIO"]);
        TextBox_SALARIO.Text = SALARIO.ToString();

        TextBox_OBS_REQUERIMIENTO.Text = filaRequisicion["OBS_REQUERIMIENTO"].ToString().Trim();

        LlenarGridViewEnvioCorreos(ID_EMPRESA, filaRequisicion["CIUDAD_REQ"].ToString().Trim());

        CargarDatosEstadoRequisicion(modificar, filaRequisicion);
    }

    private void iniciar_interfaz_para_cargar(Boolean modificar)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_REQUERIMIENTO = Convert.ToDecimal(QueryStringSeguro["requerimiento"]);
        
        String SI_COPIA_REQ = QueryStringSeguro["copia"];
        if (String.IsNullOrEmpty(SI_COPIA_REQ) == true)
        {
            SI_COPIA_REQ = null;
        }
        else
        {
            SI_COPIA_REQ = "si";
        }

        requisicion _requisicion = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable TablaInfoRequisicion = _requisicion.ObtenerComRequerimientoPorIdRequerimiento(ID_REQUERIMIENTO);

        if (TablaInfoRequisicion.Rows.Count <= 0)
        {
            if (String.IsNullOrEmpty(_requisicion.MensajeError) == false)
            {
                configurarBotonesDeAccion(true, false, false, false, true);
                Panel_BOTONES_ACCION_1.Visible = false;

                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = _requisicion.MensajeError;


                Panel_GRID_RESULTADOS.Visible = false;

                Panel_BOTONES_ACCIONES_REQUISICION.Visible = false;

                Panel_FORMULARIO.Visible = false;

                configurarPaneles(false, false, false, false, false, false);
            }
            else
            {
                configurarBotonesDeAccion(true, false, false, false, true);
                Panel_BOTONES_ACCION_1.Visible = false;

                configurarMensajes(true, System.Drawing.Color.Orange);
                Label_MENSAJE.Text = "No se encontró una requisición con el ID = " + ID_REQUERIMIENTO;

                Panel_GRID_RESULTADOS.Visible = false;

                Panel_BOTONES_ACCIONES_REQUISICION.Visible = false;

                Panel_FORMULARIO.Visible = false;

                configurarPaneles(false, false, false, false, false, false);
            }
        }
        else
        {
            DataRow filaTablaInfoRequisicion = TablaInfoRequisicion.Rows[0];

            CargarDatosRequisicion(filaTablaInfoRequisicion, SI_COPIA_REQ, modificar);

            if (DropDownList_ID_EMPRESA.SelectedValue == "")
            {
                inhabilitar_DropDownList_ENVIO_CANDIDATOS();
                TextBox_DIR_ENVIO.Enabled = false;
                TextBox_CIUDAD_ENVIO.Enabled = false;
                TextBox_TEL_ENVIO.Enabled = false;
                TextBox_EMAIL_ENVIO.Enabled = false;
                TextBox_COND_ENVIO.Enabled = false;

                Panel_GRILLA_CONTRATOS_ACTIVOS.Visible = false;

                Panel_CONTRATOS_Y_SERVICIOS.Visible = false;

                inhabilitar_DropDownList_PERFILES();
                TextBox_EDAD_MIN.Enabled = false;
                TextBox_EDAD_MAX.Enabled = false;
                TextBox_CANTIDAD.Enabled = false;
                TextBox_SEXO.Enabled = false;
                TextBox_EXPERIENCIA.Enabled = false;

                DropDownList_NIV_ACADEMICO.Enabled = false;

                TextBox_FUNCIONES.Enabled = false;
            }
            else
            {
                DropDownList_ENVIO_CANDIDATOS.Enabled = true;

                if (DropDownList_ENVIO_CANDIDATOS.SelectedValue == "")
                {
                    TextBox_DIR_ENVIO.Enabled = false;
                    TextBox_CIUDAD_ENVIO.Enabled = false;
                    TextBox_TEL_ENVIO.Enabled = false;
                    TextBox_EMAIL_ENVIO.Enabled = false;
                    TextBox_COND_ENVIO.Enabled = false;
                }
                else
                {
                    TextBox_DIR_ENVIO.Enabled = false;
                    TextBox_CIUDAD_ENVIO.Enabled = false;
                    TextBox_TEL_ENVIO.Enabled = false;
                    TextBox_EMAIL_ENVIO.Enabled = false;
                    TextBox_COND_ENVIO.Enabled = false;
                }

                DropDownList_ENVIO_CANDIDATOS.Enabled = true;

                if (HiddenField_ID_SERVICIO_RESPECTIVO.Value == "")
                {
                    if (QueryStringSeguro["accion"].ToString() != "copiarReq")
                    {
                        if (QueryStringSeguro["accion"].ToString() == "modificar")
                        {
                            Panel_CONTRATOS_Y_SERVICIOS.Visible = true;
                        }
                        else
                        {
                            Panel_CONTRATOS_Y_SERVICIOS.Visible = false;
                        }
                    }
                }
                else
                {
                    Panel_CONTRATOS_Y_SERVICIOS.Visible = true;

                    if (HiddenField_ID_SERVICIO_RESPECTIVO.Value == "")
                    {
                        Panel_GRILLA_CONTRATOS_ACTIVOS.Visible = true;
                    }
                    else
                    {
                        Panel_GRILLA_CONTRATOS_ACTIVOS.Visible = true;
                    }
                }

                if (modificar == false)
                {
                    GridView_CONTRATOS_ACTIVOS.Columns[0].Visible = false;
                }
                else
                {
                    if (QueryStringSeguro["accion"].ToString() == "copiarReq")
                    {
                        GridView_CONTRATOS_ACTIVOS.Columns[0].Visible = true;
                    }
                    else
                    {
                        if (HiddenField_ID_SERVICIO_RESPECTIVO.Value == "")
                        {
                            GridView_CONTRATOS_ACTIVOS.Columns[0].Visible = true;
                        }
                        else
                        {
                            GridView_CONTRATOS_ACTIVOS.Columns[0].Visible = true; 
                        }
                    }
                }

                DropDownList_PERFILES.Enabled = true;
                if (DropDownList_PERFILES.SelectedValue == "")
                {
                    DropDownList_PERFILES.Enabled = true;
                    TextBox_EDAD_MIN.Enabled = false;
                    TextBox_EDAD_MAX.Enabled = false;


                    if ((modificar == true) && (SI_COPIA_REQ != null))
                    {
                        TextBox_CANTIDAD.Enabled = true;
                    }
                    else
                    {
                        TextBox_CANTIDAD.Enabled = false;
                    }

                    TextBox_SEXO.Enabled = false;
                    TextBox_EXPERIENCIA.Enabled = false;

                    DropDownList_NIV_ACADEMICO.Enabled = false;

                    TextBox_FUNCIONES.Enabled = false;
                }
                else
                {
                    DropDownList_PERFILES.Enabled = true;
                    TextBox_EDAD_MIN.Enabled = false;
                    TextBox_EDAD_MAX.Enabled = false;

                    if ((modificar == true) && (SI_COPIA_REQ != null))
                    {
                        TextBox_CANTIDAD.Enabled = true;
                    }
                    else
                    {
                        TextBox_CANTIDAD.Enabled = false;
                    }

                    TextBox_SEXO.Enabled = false;
                    TextBox_EXPERIENCIA.Enabled = false;

                    DropDownList_NIV_ACADEMICO.Enabled = false;

                    TextBox_FUNCIONES.Enabled = false;
                }

                if ((modificar == true) && (SI_COPIA_REQ == null))
                {
                    DropDownList_PERFILES.Enabled = false;
                }
                else
                {
                    DropDownList_PERFILES.Enabled = true;
                }
            }

            Panel_GRID_RESULTADOS.Visible = false;

            if (modificar == true)
            {
                Panel_CONTROL_REGISTRO.Visible = false;

                Panel_ID_REQUERIMIENTO.Visible = false;
            }
            else
            {
                Panel_CONTROL_REGISTRO.Visible = true;
                Panel_CONTROL_REGISTRO.Enabled = false;
                

                Panel_ID_REQUERIMIENTO.Visible = true;
                TextBox_ID_REQUERIMIENTO.Enabled = false;
                
            }

            if (modificar == true)
            {
                Panel_FORMULARIO.Visible = true;
                Panel_FORMULARIO.Enabled = true;
            }
            else
            {
                Panel_FORMULARIO.Visible = true;
                Panel_FORMULARIO.Enabled = false;
            }

            
            if (modificar == true)
            {
                DropDownList_ID_EMPRESA.Enabled = false;
                DropDownList_TIP_REQ.Enabled = false;
            }
            else
            {
                DropDownList_ID_EMPRESA.Enabled = true;
                DropDownList_TIP_REQ.Enabled = true;
            }


            if (SI_COPIA_REQ != null)
            {
                TextBox_FECHA_REQUERIDA.Enabled = true;

                RangeValidator_TextBox_FECHA_REQUERIDA.MinimumValue = DateTime.Now.ToShortDateString();
                RangeValidator_TextBox_FECHA_REQUERIDA.MaximumValue = DateTime.Now.AddMonths(1).ToShortDateString();

                RangeValidator_TextBox_FECHA_REQUERIDA.Enabled = true;
                ValidatorCalloutExtender_TextBox_FECHA_REQUERIDA_1.Enabled = true;
            }
            else
            {
                if (modificar == true)
                {
                    RangeValidator_TextBox_FECHA_REQUERIDA.MinimumValue = TextBox_FECHA_R.Text;
                    RangeValidator_TextBox_FECHA_REQUERIDA.MaximumValue = Convert.ToDateTime(TextBox_FECHA_R.Text).AddMonths(1).ToShortDateString();

                    RangeValidator_TextBox_FECHA_REQUERIDA.Enabled = true;
                    ValidatorCalloutExtender_TextBox_FECHA_REQUERIDA_1.Enabled = true;

                    TextBox_FECHA_REQUERIDA.Enabled = true;
                }
                else
                {
                    RangeValidator_TextBox_FECHA_REQUERIDA.Enabled = false;
                    ValidatorCalloutExtender_TextBox_FECHA_REQUERIDA_1.Enabled = false;

                    TextBox_FECHA_REQUERIDA.Enabled = false;
                }     
            }

            TextBox_FechaReferenciaSistema.Enabled = false;

            Button_ENVIAR_CANDIDATOS.Visible = true;
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
        Page.Header.Title = "REGISTRO, ATENCIÓN DE REQUISICIONES";

        Panel_INFO_ADICIONAL_MODULO.Visible = false;

        Panel_PARAMETROS.Visible = false;

        if (IsPostBack == false)
        {
            configurar_paneles_popup(Panel_FONDO_MENSAJE, Panel_MENSAJES);

            tools _tools = new tools();
            SecureQueryString QueryStringSeguro;
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

            String accion = QueryStringSeguro["accion"].ToString();

            configurarCaracteresAceptadosBusqueda(false, false);

            iniciar_seccion_de_busqueda();

            if (accion == "inicial")
            {
                iniciar_interfaz_inicial();
            }
            else
            {
                if (accion == "nuevo")
                {
                    iniciar_interfaz_nuevo();
                }
                else
                {
                    if (accion == "cargar")
                    {
                        iniciar_interfaz_para_cargar(false);
                    }
                    else
                    {
                        if (accion == "modificar")
                        {
                            iniciar_interfaz_para_cargar(true);
                        }
                        else
                        {
                            if (accion == "cargarNuevo")
                            {
                                iniciar_interfaz_para_cargar(false);

                                String ID_REQUERIMIENTO = QueryStringSeguro["requerimiento"].ToString();
                                configurarMensajes(true, System.Drawing.Color.Green);
                                Label_MENSAJE.Text = "La Requisición fue creada correctamente y se le asignó el ID: " + ID_REQUERIMIENTO + ".";
                            }
                            else
                            {
                                if (accion == "cargarModificado")
                                {
                                    iniciar_interfaz_para_cargar(false);

                                    String ID_REQUERIMIENTO = QueryStringSeguro["requerimiento"].ToString();
                                    configurarMensajes(true, System.Drawing.Color.Green);
                                    Label_MENSAJE.Text = "La requisición # " + ID_REQUERIMIENTO.ToString() + " fue modificada correctamente.";
                                }
                                else 
                                {
                                    if (accion == "copiarReq")
                                    {
                                        iniciar_interfaz_para_cargar(true);

                                        String ID_REQUERIMIENTO = QueryStringSeguro["requerimiento"].ToString();
                                        configurarMensajes(true, System.Drawing.Color.Orange);
                                        Label_MENSAJE.Text = "la información actual es una copia de la requisición " + ID_REQUERIMIENTO + ", puede modificar los datos que necesite y guardar, esto creará una nueva requisición.";
                                    }
                                }
                            }   
                        }
                    }
                }
            }
        }
    }

    protected void DropDownList_BUSCAR_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_BUSCAR.SelectedValue == "")
        {
            configurarCaracteresAceptadosBusqueda(false, false);
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "COD_EMPRESA")
            {
                configurarCaracteresAceptadosBusqueda(false, true);
            }
            else
            {
                if (DropDownList_BUSCAR.SelectedValue == "RAZ_SOCIAL")
                {
                    configurarCaracteresAceptadosBusqueda(true, false);
                }
                else
                {
                    if (DropDownList_BUSCAR.SelectedValue == "ID_REQUERIMIENTO")
                    {
                        configurarCaracteresAceptadosBusqueda(false, true);
                    }
                }
            }
        }
        TextBox_BUSCAR.Text = "";
    }

    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        configurarInfoAdicionalModulo(false, "");

        String datosCapturados = TextBox_BUSCAR.Text.ToUpper();
        String campo = DropDownList_BUSCAR.SelectedValue.ToString();

        requisicion _requisicion = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaResultadosBusqueda = new DataTable();

        if (DropDownList_BUSCAR.SelectedValue == "RAZ_SOCIAL")
        {
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "COD_EMPRESA")
            {
                tablaResultadosBusqueda = _requisicion.ObtenerComRequerimientoPorCodEmpresa(datosCapturados, DropDownList_Cancelado.SelectedValue.ToString(), DropDownList_Cumplido.SelectedValue.ToString());
            }
            else
            {
                if (DropDownList_BUSCAR.SelectedValue == "ID_REQUERIMIENTO")
                {
                    tablaResultadosBusqueda = _requisicion.ObtenerComRequerimientoPorIdRequerimiento(Convert.ToDecimal(datosCapturados));
                }
            }
        }

        if (tablaResultadosBusqueda.Rows.Count <= 0)
        {
            if (_requisicion.MensajeError != null)
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = _requisicion.MensajeError;
            }
            else
            {
                configurarMensajes(true, System.Drawing.Color.Orange);
                Label_MENSAJE.Text = "No se encontraron registros que cumplieran los datos de busqueda.";
            }

            configurarBotonesDeAccion(true, false, false, false, true);
            Panel_BOTONES_ACCION_1.Visible = false;

            Panel_GRID_RESULTADOS.Visible = false;

            Panel_FORMULARIO.Visible = false;

            configurarPaneles(false, false, false, false, false, false);
        }
        else
        {
            configurarBotonesDeAccion(true, false, false, false, true);
            Panel_BOTONES_ACCION_1.Visible = false;

            Panel_GRID_RESULTADOS.Visible = true;
            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaResultadosBusqueda;
            GridView_RESULTADOS_BUSQUEDA.DataBind();

            Panel_FORMULARIO.Visible = false;

            configurarPaneles(false, false, false, false, false, false);
        }
    }

    protected void GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged(object sender, EventArgs e)
    {
        String ID_REQUERIMIENTO = GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["ID_REQUERIMIENTO"].ToString();

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = "seleccion";
        QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
        QueryStringSeguro["nombre_modulo"] = "REGISTRO, ATENCIÓN DE REQUISICIONES";
        QueryStringSeguro["accion"] = "cargar";
        QueryStringSeguro["requerimiento"] = ID_REQUERIMIENTO;

        Response.Redirect("~/seleccion/registroAtencionRequisiciones.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }
    protected void Button_NUEVO_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = "seleccion";
        QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
        QueryStringSeguro["nombre_modulo"] = "REGISTRO, ATENCIÓN DE REQUISICIONES";
        QueryStringSeguro["accion"] = "nuevo";

        Response.Redirect("~/seleccion/registroAtencionRequisiciones.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }

    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"].ToString());

        String ID_REQUERIMIENTO = QueryStringSeguro["requerimiento"].ToString();

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
        QueryStringSeguro["img_area"] = "seleccion";
        QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
        QueryStringSeguro["nombre_modulo"] = "REGISTRO, ATENCIÓN DE REQUISICIONES";
        QueryStringSeguro["accion"] = "modificar";
        QueryStringSeguro["requerimiento"] = ID_REQUERIMIENTO;

        Response.Redirect("~/seleccion/registroAtencionRequisiciones.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }

    private Boolean EnviarCorreoAColaboradoresInteresados(Decimal idRequerimiento)
    { 
        String mensaje;
        String mensaje_interno;
        tools _tools = new tools();
        String destinatarios = "";
        String destinatarioCliente = "";
        Boolean resultado = true;
        Boolean resultadoCliente = true;

        if (GridView_EnvioCorreos.Rows.Count > 0)
        {
            for (int i = 0; i < GridView_EnvioCorreos.Rows.Count; i++)
            {
                String email = GridView_EnvioCorreos.DataKeys[i].Values["MAIL_COLABORADOR"].ToString().Trim();
                if (String.IsNullOrEmpty(email) == false)
                {
                    if (string.IsNullOrEmpty(destinatarios) == true)
                    {
                        destinatarios = email;
                    }
                    else
                    {
                        destinatarios += ";" + email;
                    }
                }
            }

            if (String.IsNullOrEmpty(destinatarios) == true)
            {
                resultado = false;
            }
        }
        else
        {
            resultado = false;
        }

        if (TextBox_EMAIL_ENVIO.Text.Trim().Length > 0)
        {
            if (TextBox_EMAIL_ENVIO.Text.Contains("@") == true)
            {
                destinatarioCliente = TextBox_EMAIL_ENVIO.Text.Trim();
                resultadoCliente = true;
            }
            else
            {
                resultadoCliente = false;
            }
        }


        {
            mensaje_interno = "<p style=\"text-align:center; font-weight:bold\">REQUERIMIENTO NUEVO!</p>";
            mensaje_interno += "<p>Se creó un nuevo requerimiento, los datos más importantes son los siguientes:</p>";
            mensaje_interno += "<ul><li><strong>NUM REQUERIMIENTO:</strong>[ID_REQUERIMIENTO]</li>";
            mensaje_interno += "<li><strong>CLIENTE:</strong>[CLIENTE]</li>";
            mensaje_interno += "<li><strong>TIPO REQ:</strong>[TIPO_REQ]</li></ul>";
            mensaje_interno += "<p>Para realizar el respectivo seguimiento según sus funciones, dirijase al sistema SISER WEB - Hoja de trabajado de selección, recuerde el id de este requerimeinto es el <strong>[ID_REQUERIMIENTO]</strong>.</p>";
            mensaje_interno += "<p>Creado Automáticamente por el Sistema SISER WEB, por favor no responder a este mensaje.</p>";
            mensaje_interno += "<p>Fecha: [FECHA_REPORTE].</p>";

            mensaje_interno = mensaje_interno.Replace("[ID_REQUERIMIENTO]", idRequerimiento.ToString());
            mensaje_interno = mensaje_interno.Replace("[CLIENTE]", DropDownList_ID_EMPRESA.SelectedItem.Text.Trim());
            mensaje_interno = mensaje_interno.Replace("[TIPO_REQ]", DropDownList_TIP_REQ.SelectedItem.Text.Trim());
            mensaje_interno = mensaje_interno.Replace("[FECHA_REPORTE]", DateTime.Now.ToLongDateString());

            resultado = _tools.enviarCorreoConCuerpoHtml(destinatarios, "NUEVO REQUERIMIENTO -SISER WEB-", mensaje_interno);

        }

        if (resultadoCliente == true)
        {

            if (ConfigurationManager.AppSettings["envioCorreosAClienteRequerimientosSeleccion"].ToLower() == "true")
            {
                mensaje = "<p style=\"text-align:center; font-weight:bold\">REQUERIMIENTO NUEVO -[EMPRESA_TEMPORAL]-!</p>";
                mensaje += "<p>Se creó un nuevo requerimiento, los datos más importantes son los siguientes:</p>";
                mensaje += "<ul><li><strong>NUM REQUERIMIENTO:</strong>[ID_REQUERIMIENTO]</li>";
                mensaje += "<li><strong>CLIENTE:</strong>[CLIENTE]</li>";
                mensaje += "<li><strong>TIPO REQ:</strong>[TIPO_REQ]</li>";
                mensaje += "<li><strong>CARGO:</strong>[CARGO]</li>";
                mensaje += "<li><strong>PERSONAS SOLICITADAS:</strong>[CANTIDAD]</li>";
                mensaje += "<li><strong>FECHA VENCIMIENTO:</strong>[FECHA_VENCE]</li>";
                mensaje += "<li><strong>CIUDAD:</strong>[CIUDAD]</li></ul>";
                mensaje += "<p>E-mail creado Automáticamente por el Sistema SISER WEB, por favor no responder a este mensaje.</p>";
                mensaje += "<p>Fecha: [FECHA_REPORTE].</p>";

                mensaje = mensaje.Replace("[ID_REQUERIMIENTO]", idRequerimiento.ToString());
                mensaje = mensaje.Replace("[CLIENTE]", DropDownList_ID_EMPRESA.SelectedItem.Text.Trim());
                mensaje = mensaje.Replace("[TIPO_REQ]", DropDownList_TIP_REQ.SelectedItem.Text.Trim());
                mensaje = mensaje.Replace("[CARGO]", DropDownList_PERFILES.SelectedItem.Text.Trim());
                mensaje = mensaje.Replace("[CANTIDAD]", TextBox_CANTIDAD.Text.Trim());
                mensaje = mensaje.Replace("[FECHA_VENCE]", Convert.ToDateTime(TextBox_FECHA_REQUERIDA.Text).ToLongDateString());
                mensaje = mensaje.Replace("[CIUDAD]", DropDownList_REGIONAL.SelectedItem.Text.Trim() + " - " + DropDownList_DEPARTAMENTO.SelectedItem.Text.Trim() + " - " + DropDownList_CIUDAD_REQ.SelectedItem.Text.Trim());
                mensaje = mensaje.Replace("[FECHA_REPORTE]", DateTime.Now.ToLongDateString());
                
                if (Session["idEmpresa"].ToString() == "1")
                {
                    mensaje = mensaje.Replace("[EMPRESA_TEMPORAL]", tabla.VAR_NOMBRE_SERTEMPO);
                    resultadoCliente = _tools.enviarCorreoConCuerpoHtml(destinatarios, "NUEVO REQUERIMIENTO -" + tabla.VAR_NOMBRE_SERTEMPO + "-", mensaje);
                }
                else
                {
                    mensaje = mensaje.Replace("[EMPRESA_TEMPORAL]", tabla.VAR_NOMBRE_EYS);
                    resultadoCliente = _tools.enviarCorreoConCuerpoHtml(destinatarios, "NUEVO REQUERIMIENTO -" + tabla.VAR_NOMBRE_EYS + "-", mensaje);
                }
            }
        }

        return resultado;
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"].ToString());

        Decimal ID_REQUERIMIENTO = Convert.ToDecimal(QueryStringSeguro["requerimiento"]);

        String SI_COPPIAR_REQ = QueryStringSeguro["copia"];
        if (String.IsNullOrEmpty(SI_COPPIAR_REQ) == true)
        {
            SI_COPPIAR_REQ = null;
        }
        else
        {
            SI_COPPIAR_REQ = "si";
        }

        String accion = QueryStringSeguro["accion"].ToString();

        requisicion _requisicion = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        String TIPO_REQ = DropDownList_TIP_REQ.SelectedValue;
        DateTime FECHA_REQUERIDA = Convert.ToDateTime(TextBox_FECHA_REQUERIDA.Text);
        
        DateTime FECHA_REFERENCIA_SISTEMA = new DateTime();
        if (String.IsNullOrEmpty(TextBox_FechaReferenciaSistema.Text) == false)
        {
            FECHA_REFERENCIA_SISTEMA = Convert.ToDateTime(TextBox_FechaReferenciaSistema.Text);
        }

        int ID_EMPRESA = Convert.ToInt32(DropDownList_ID_EMPRESA.SelectedValue);
        int CANTIDAD = Convert.ToInt32(TextBox_CANTIDAD.Text);
        Decimal SALARIO = Convert.ToDecimal(TextBox_SALARIO.Text);

        String HORARIO = DropDownList_HORARIO.SelectedValue;

        String CIUDAD_CONTRATO;

        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoEmpresaSeleccionada = _cliente.ObtenerEmpresaConIdEmpresa(Convert.ToDecimal(ID_EMPRESA));
        if (tablaInfoEmpresaSeleccionada.Rows.Count <= 0)
        {
            CIUDAD_CONTRATO = "";
        }
        else
        {
            DataRow filaTablaInfoEmpresaSeleccionada = tablaInfoEmpresaSeleccionada.Rows[0];
            CIUDAD_CONTRATO = filaTablaInfoEmpresaSeleccionada["CIU_EMP"].ToString();
        }

        String DURACION = DropDownList_DURACION.SelectedValue;
        
        String OBS_REQUERIMIENTO = TextBox_OBS_REQUERIMIENTO.Text;
        String CIUDAD_REQ = DropDownList_CIUDAD_REQ.SelectedValue;
        Decimal REGISTRO_PERFIL = Convert.ToDecimal(DropDownList_PERFILES.SelectedValue);

        if(HiddenField_ID_SERVICIO_RESPECTIVO.Value == "")
        {
            configurarMensajes(true, System.Drawing.Color.Orange);
            Label_MENSAJE.Text = "Para poder guardar la requisición, primero debe seleccionar el Servicio Respectivo. (En la sección DATOS DE CONTRATO DE SERVICIO / SERVICIO RESPECTIVO).";
        }
        else
        {
            Decimal ID_SERVICIO_RESPECTIVO = Convert.ToDecimal(HiddenField_ID_SERVICIO_RESPECTIVO.Value);

            Decimal REGISTRO_ENVIO_CANDIDATOS = Convert.ToDecimal(DropDownList_ENVIO_CANDIDATOS.SelectedValue);

            Decimal REGISTRO_REQUISICION;
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
            QueryStringSeguro["img_area"] = "seleccion";
            QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
            QueryStringSeguro["nombre_modulo"] = "REGISTRO, ATENCIÓN DE REQUISICIONES";


            if (accion == "nuevo")
            {
                REGISTRO_REQUISICION = _requisicion.AdicionarConRequerimientos(TIPO_REQ, FECHA_REQUERIDA, ID_EMPRESA, CANTIDAD, SALARIO, HORARIO, CIUDAD_CONTRATO, DURACION, OBS_REQUERIMIENTO, CIUDAD_REQ, REGISTRO_PERFIL, ID_SERVICIO_RESPECTIVO, REGISTRO_ENVIO_CANDIDATOS, FECHA_REFERENCIA_SISTEMA);


                if (_requisicion.MensajeError == null)
                {
                    EnviarCorreoAColaboradoresInteresados(REGISTRO_REQUISICION);

                    QueryStringSeguro["accion"] = "cargarNuevo";
                    QueryStringSeguro["requerimiento"] = REGISTRO_REQUISICION.ToString();

                    Response.Redirect("~/seleccion/registroAtencionRequisiciones.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
                }
                else
                {
                    configurarMensajes(true, System.Drawing.Color.Red);
                    Label_MENSAJE.Text = _cliente.MensajeError;
                }
            }
            else
            {
                if (accion == "copiarReq")
                {
                    REGISTRO_REQUISICION = _requisicion.AdicionarConRequerimientos(TIPO_REQ, FECHA_REQUERIDA, ID_EMPRESA, CANTIDAD, SALARIO, HORARIO, CIUDAD_CONTRATO, DURACION, OBS_REQUERIMIENTO, CIUDAD_REQ, REGISTRO_PERFIL, ID_SERVICIO_RESPECTIVO, REGISTRO_ENVIO_CANDIDATOS, FECHA_REFERENCIA_SISTEMA);

                    if (_requisicion.MensajeError == null)
                    {
                        QueryStringSeguro["accion"] = "cargarNuevo";
                        QueryStringSeguro["requerimiento"] = REGISTRO_REQUISICION.ToString();

                        Response.Redirect("~/seleccion/registroAtencionRequisiciones.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
                    }
                    else
                    {
                        configurarMensajes(true, System.Drawing.Color.Red);
                        Label_MENSAJE.Text = _cliente.MensajeError;
                    }
                }
                else
                {
                    if (accion == "modificar")
                    {
                        DataTable tablaRequerimientoActual = _requisicion.ObtenerComRequerimientoPorIdRequerimiento(ID_REQUERIMIENTO);


                        Boolean verificador_fecha = true;

                        if (verificador_fecha == true)
                        {
                            if (tablaRequerimientoActual.Rows.Count <= 0)
                            {
                                configurarMensajes(true, System.Drawing.Color.Orange);
                                Label_MENSAJE.Text = "No se encontró la información previa del requerimineto que se desea actualizar.";
                            }
                            else
                            {
                                DataRow filaTablaRequerimientoActual = tablaRequerimientoActual.Rows[0];
                                
                                Boolean verificador = true;
                                verificador = _requisicion.ActualizarConRequerimeintos(Convert.ToInt32(ID_REQUERIMIENTO), TIPO_REQ, FECHA_REQUERIDA, CANTIDAD, SALARIO, HORARIO, DURACION, OBS_REQUERIMIENTO, CIUDAD_REQ, REGISTRO_PERFIL, ID_SERVICIO_RESPECTIVO, REGISTRO_ENVIO_CANDIDATOS, FECHA_REFERENCIA_SISTEMA);

                                if (verificador == false)
                                {
                                    configurarMensajes(true, System.Drawing.Color.Red);
                                    Label_MENSAJE.Text = _requisicion.MensajeError;
                                }
                                else
                                {
                                    QueryStringSeguro["accion"] = "cargarModificado";
                                    QueryStringSeguro["requerimiento"] = ID_REQUERIMIENTO.ToString();

                                    Response.Redirect("~/seleccion/registroAtencionRequisiciones.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"].ToString());

        String accion = QueryStringSeguro["accion"].ToString();

        if (accion == "modificar")
        {
            String requisicion = QueryStringSeguro["requerimiento"].ToString();

            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
            QueryStringSeguro["img_area"] = "seleccion";
            QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
            QueryStringSeguro["nombre_modulo"] = "REGISTRO, ATENCIÓN DE REQUISICIONES";
            QueryStringSeguro["accion"] = "cargar";
            QueryStringSeguro["requerimiento"] = requisicion;
        }
        else
        {
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
            QueryStringSeguro["img_area"] = "seleccion";
            QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
            QueryStringSeguro["nombre_modulo"] = "REGISTRO, ATENCIÓN DE REQUISICIONES";
            QueryStringSeguro["accion"] = "inicial";
        }

        Response.Redirect("~/seleccion/registroAtencionRequisiciones.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }

    protected void DropDownList_ID_EMPRESA_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_ID_EMPRESA.SelectedValue == "")
        {
            inhabilitar_DropDownList_ENVIO_CANDIDATOS();
            TextBox_DIR_ENVIO.Text = "";
            TextBox_DIR_ENVIO.Enabled = false;
            TextBox_CIUDAD_ENVIO.Text = "";
            TextBox_CIUDAD_ENVIO.Enabled = false;
            TextBox_TEL_ENVIO.Text = "";
            TextBox_TEL_ENVIO.Enabled = false;
            TextBox_EMAIL_ENVIO.Text = "";
            TextBox_EMAIL_ENVIO.Enabled = false;
            TextBox_COND_ENVIO.Text = "";
            TextBox_COND_ENVIO.Enabled = false;

            Panel_GRILLA_CONTRATOS_ACTIVOS.Visible = false;
            GridView_CONTRATOS_ACTIVOS.DataSource = null;
            GridView_CONTRATOS_ACTIVOS.DataBind();
            HiddenField_ID_SERVICIO_RESPECTIVO.Value = null;
            Panel_CONTRATOS_Y_SERVICIOS.Visible = false;

            inhabilitar_DropDownList_PERFILES();
            TextBox_EDAD_MIN.Text = "";
            TextBox_EDAD_MIN.Enabled = false;
            TextBox_EDAD_MAX.Text = "";
            TextBox_EDAD_MAX.Enabled = false;
            TextBox_CANTIDAD.Text = "";
            TextBox_CANTIDAD.Enabled = false;
            TextBox_SEXO.Text = "";
            TextBox_SEXO.Enabled = false;
            TextBox_EXPERIENCIA.Text = "";
            TextBox_EXPERIENCIA.Enabled = false;

            cargar_DropDownList_NIV_ACADEMICO();
            DropDownList_NIV_ACADEMICO.Enabled = false;

            TextBox_FUNCIONES.Text = "";
            TextBox_FUNCIONES.Enabled = false;

            DropDownList_REGIONAL.Enabled = false;
            DropDownList_REGIONAL.ClearSelection();
            DropDownList_DEPARTAMENTO.Enabled = false;
            DropDownList_DEPARTAMENTO.ClearSelection();
            DropDownList_CIUDAD_REQ.Enabled = false;
            DropDownList_CIUDAD_REQ.ClearSelection();

            Panel_InformacionEnvioEmail.Visible = true;
            Panel_EnvioEmailIndeterminado.Visible = true;
            PanelSeguimientoDesdeReclutamiento.Visible = true;
            GridViewTrazavilidad.DataSource = null;
            GridView_EnvioCorreos.DataSource = null;
        }
        else
        {
            Decimal ID_EMPRESA = Convert.ToDecimal(DropDownList_ID_EMPRESA.SelectedValue);

            cargar_DropDownList_ENVIO_CANDIDATOS(ID_EMPRESA);
            DropDownList_ENVIO_CANDIDATOS.Enabled = true;

            DataRow filaInfoCandidatoInmediato = ObtenerEnvioContactoVigentePorIdEmpresa(ID_EMPRESA);

            if (filaInfoCandidatoInmediato == null)
            {
                DropDownList_ENVIO_CANDIDATOS.SelectedValue = "";
                TextBox_DIR_ENVIO.Text = "";
                TextBox_DIR_ENVIO.Enabled = false;
                TextBox_CIUDAD_ENVIO.Text = "";
                TextBox_CIUDAD_ENVIO.Enabled = false;
                TextBox_TEL_ENVIO.Text = "";
                TextBox_TEL_ENVIO.Enabled = false;
                TextBox_EMAIL_ENVIO.Text = "";
                TextBox_EMAIL_ENVIO.Enabled = false;
                TextBox_COND_ENVIO.Text = "";
                TextBox_COND_ENVIO.Enabled = false;
            }
            else
            {
                DropDownList_ENVIO_CANDIDATOS.SelectedValue = filaInfoCandidatoInmediato["REGISTRO"].ToString();
                TextBox_DIR_ENVIO.Text = filaInfoCandidatoInmediato["DIR_ENVIO"].ToString();
                TextBox_DIR_ENVIO.Enabled = false;
                TextBox_CIUDAD_ENVIO.Text = filaInfoCandidatoInmediato["NOMBRE_CIUDAD"].ToString();
                TextBox_CIUDAD_ENVIO.Enabled = false;
                TextBox_TEL_ENVIO.Text = filaInfoCandidatoInmediato["TEL_ENVIO"].ToString();
                TextBox_TEL_ENVIO.Enabled = false;
                TextBox_EMAIL_ENVIO.Text = filaInfoCandidatoInmediato["CONT_MAIL"].ToString();
                TextBox_EMAIL_ENVIO.Enabled = false;
                TextBox_COND_ENVIO.Text = filaInfoCandidatoInmediato["COND_ENVIO"].ToString();
                TextBox_COND_ENVIO.Enabled = false;
            }

            cargar_GridView_CONTRATOS_ACTIVOS(ID_EMPRESA);
            HiddenField_ID_SERVICIO_RESPECTIVO.Value = null;
            GridView_CONTRATOS_ACTIVOS.SelectedIndex = -1;

            cargar_DropDownList_PERFILES(Convert.ToInt32(ID_EMPRESA));
            DropDownList_PERFILES.Enabled = true;
            TextBox_EDAD_MIN.Text = "";
            TextBox_EDAD_MIN.Enabled = false;
            TextBox_EDAD_MAX.Text = "";
            TextBox_EDAD_MAX.Enabled = false;
            TextBox_CANTIDAD.Text = "";
            TextBox_CANTIDAD.Enabled = false;
            TextBox_SEXO.Text = "";
            TextBox_SEXO.Enabled = false;
            TextBox_EXPERIENCIA.Text = "";
            TextBox_EXPERIENCIA.Enabled = false;

            cargar_DropDownList_NIV_ACADEMICO();
            DropDownList_NIV_ACADEMICO.Enabled = false;

            TextBox_FUNCIONES.Text = "";
            TextBox_FUNCIONES.Enabled = false;

            DropDownList_REGIONAL.Enabled = true;
            cargar_DropDownList_REGIONAL_ConRestriccion(ID_EMPRESA, Session["USU_LOG"].ToString());
            DropDownList_DEPARTAMENTO.Enabled = true;
            inhabilitar_DropDownList_DEPARTAMENTO();
            DropDownList_CIUDAD_REQ.Enabled = true;
            inhabilitar_DropDownList_CIUDAD_REQ();
        }

        Panel_InformacionEnvioEmail.Visible = true;
        Panel_EnvioEmailIndeterminado.Visible = true;
        Label_EnvioEmailIndeterminado.Text = _mensaje_envioEmailIndeterminado;
        GridView_EnvioCorreos.DataSource = null;
    }

    protected void DropDownList_ENVIO_CANDIDATOS_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_ENVIO_CANDIDATOS.SelectedValue == "")
        {
            TextBox_DIR_ENVIO.Text = "";
            TextBox_DIR_ENVIO.Enabled = false;
            TextBox_CIUDAD_ENVIO.Text = "";
            TextBox_CIUDAD_ENVIO.Enabled = false;
            TextBox_TEL_ENVIO.Text = "";
            TextBox_TEL_ENVIO.Enabled = false;
            TextBox_EMAIL_ENVIO.Text = "";
            TextBox_EMAIL_ENVIO.Enabled = false;
            TextBox_COND_ENVIO.Text = "";
            TextBox_COND_ENVIO.Enabled = false;
        }
        else
        {
            DataRow filaInfoSolicitanteSeleccionado = ObtenerEnvioDeCandidatoPorRegistro(Convert.ToDecimal(DropDownList_ENVIO_CANDIDATOS.SelectedValue));
            if (filaInfoSolicitanteSeleccionado == null)
            {
                TextBox_DIR_ENVIO.Text = "";
                TextBox_DIR_ENVIO.Enabled = false;
                TextBox_CIUDAD_ENVIO.Text = "";
                TextBox_CIUDAD_ENVIO.Enabled = false;
                TextBox_TEL_ENVIO.Text = "";
                TextBox_TEL_ENVIO.Enabled = false;
                TextBox_EMAIL_ENVIO.Text = "";
                TextBox_EMAIL_ENVIO.Enabled = false;
                TextBox_COND_ENVIO.Text = "";
                TextBox_COND_ENVIO.Enabled = false;

                DropDownList_ENVIO_CANDIDATOS.SelectedValue = "";
            }
            else
            {
                TextBox_DIR_ENVIO.Text = filaInfoSolicitanteSeleccionado["DIR_ENVIO"].ToString();
                TextBox_DIR_ENVIO.Enabled = false;
                TextBox_CIUDAD_ENVIO.Text = filaInfoSolicitanteSeleccionado["NOMBRE_CIUDAD"].ToString();
                TextBox_CIUDAD_ENVIO.Enabled = false;
                TextBox_TEL_ENVIO.Text = filaInfoSolicitanteSeleccionado["TEL_ENVIO"].ToString();
                TextBox_TEL_ENVIO.Enabled = false;
                TextBox_EMAIL_ENVIO.Text = filaInfoSolicitanteSeleccionado["CONT_MAIL"].ToString();
                TextBox_EMAIL_ENVIO.Enabled = false;
                TextBox_COND_ENVIO.Text = filaInfoSolicitanteSeleccionado["COND_ENVIO"].ToString();
                TextBox_COND_ENVIO.Enabled = false;
            }
        }
    }

    protected void DropDownList_REGIONAL_SelectedIndexChanged(object sender, EventArgs e)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(DropDownList_ID_EMPRESA.SelectedValue);

        if (DropDownList_REGIONAL.SelectedValue == "")
        {
            inhabilitar_DropDownList_DEPARTAMENTO();
            inhabilitar_DropDownList_CIUDAD_REQ();
        }
        else
        {
            String ID_REGIONAL = DropDownList_REGIONAL.SelectedValue.ToString();
            cargar_DropDownList_DEPARTAMENTO_ConRestriccion(ID_REGIONAL, ID_EMPRESA);
            DropDownList_DEPARTAMENTO.Enabled = true;
            inhabilitar_DropDownList_CIUDAD_REQ();
        }

        Panel_InformacionEnvioEmail.Visible = true;
        Panel_EnvioEmailIndeterminado.Visible = true;
        Label_EnvioEmailIndeterminado.Text = _mensaje_envioEmailIndeterminado;
        GridView_EnvioCorreos.DataSource = null;
    }
    protected void DropDownList_DEPARTAMENTO_SelectedIndexChanged(object sender, EventArgs e)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(DropDownList_ID_EMPRESA.SelectedValue);

        if (DropDownList_DEPARTAMENTO.SelectedValue == "")
        {
            inhabilitar_DropDownList_CIUDAD_REQ();
        }
        else
        {
            String ID_DEPARTAMENTO = DropDownList_DEPARTAMENTO.SelectedValue.ToString();
            String ID_REGIONAL = DropDownList_REGIONAL.SelectedValue.ToString();
            cargar_DropDownList_CIUDAD_REQ_ConRestriccion(ID_REGIONAL, ID_DEPARTAMENTO, ID_EMPRESA);
            DropDownList_CIUDAD_REQ.Enabled = true;
        }

        Panel_InformacionEnvioEmail.Visible = true;
        Panel_EnvioEmailIndeterminado.Visible = true;
        Label_EnvioEmailIndeterminado.Text = _mensaje_envioEmailIndeterminado;
        GridView_EnvioCorreos.DataSource = null;
    }
    protected void DropDownList_PERFILES_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_PERFILES.SelectedValue == "")
        {
            TextBox_EDAD_MIN.Text = "";
            TextBox_EDAD_MIN.Enabled = false;
            TextBox_EDAD_MAX.Text = "";
            TextBox_EDAD_MAX.Enabled = false;
            TextBox_CANTIDAD.Text = "";
            TextBox_CANTIDAD.Enabled = false;
            TextBox_SEXO.Text = "";
            TextBox_SEXO.Enabled = false;
            TextBox_EXPERIENCIA.Text = "";
            TextBox_EXPERIENCIA.Enabled = false;

            cargar_DropDownList_NIV_ACADEMICO();
            DropDownList_NIV_ACADEMICO.Enabled = false;

            TextBox_FUNCIONES.Text = "";
            TextBox_FUNCIONES.Enabled = false;

            HiddenField_NivelRequerimiento.Value = NivelesRequerimiento.Baja.ToString();
            TextBox_FechaReferenciaSistema.Text = "";
            TextBox_FechaReferenciaSistema.Enabled = false;
        }
        else
        {
            DataRow filaInfoCargo = ObtenerPerfilPorRegistro(Convert.ToInt32(DropDownList_PERFILES.SelectedValue));

            TextBox_EDAD_MIN.Text = filaInfoCargo["EDAD_MIN"].ToString();
            TextBox_EDAD_MIN.Enabled = false;
            TextBox_EDAD_MAX.Text = filaInfoCargo["EDAD_MAX"].ToString();
            TextBox_EDAD_MAX.Enabled = false;
            TextBox_CANTIDAD.Text = "";
            TextBox_CANTIDAD.Enabled = true;
            TextBox_SEXO.Text = filaInfoCargo["SEXO"].ToString();
            TextBox_SEXO.Enabled = false;
            TextBox_EXPERIENCIA.Text = filaInfoCargo["EXPERIENCIA"].ToString();
            TextBox_EXPERIENCIA.Enabled = false;

            cargar_DropDownList_NIV_ACADEMICO();
            DropDownList_NIV_ACADEMICO.SelectedValue = filaInfoCargo["NIV_ESTUDIOS"].ToString().Trim();
            DropDownList_NIV_ACADEMICO.Enabled = false;

            TextBox_FUNCIONES.Text = filaInfoCargo["DSC_FUNCIONES"].ToString();
            TextBox_FUNCIONES.Enabled = false;

            HiddenField_NivelRequerimiento.Value = NivelesRequerimiento.Baja.ToString();
            if (filaInfoCargo["NIVEL_REQUERIMIENTO"].ToString().ToUpper() == "COMPLEJA")
            {
                HiddenField_NivelRequerimiento.Value = NivelesRequerimiento.Compleja.ToString();
            }
            else
            {
                if (filaInfoCargo["NIVEL_REQUERIMIENTO"].ToString().ToUpper() == "MEDIA")
                {
                    HiddenField_NivelRequerimiento.Value = NivelesRequerimiento.Media.ToString();
                }
                else
                {
                    if (filaInfoCargo["NIVEL_REQUERIMIENTO"].ToString().ToUpper() == "BAJA")
                    {
                        HiddenField_NivelRequerimiento.Value = NivelesRequerimiento.Baja.ToString();
                    }
                }
            }

            TextBox_FechaReferenciaSistema.Text = "";
            TextBox_FechaReferenciaSistema.Enabled = false;
        }
    }
    protected void Button_ENVIAR_CANDIDATOS_Click(object sender, EventArgs e)
    {
        if (DropDownList_PERFILES.SelectedValue == "")
        {
            configurarMensajes(true, System.Drawing.Color.Orange);
            Label_MENSAJE.Text = "NO SE HA CARGADO PERFIL, Requisición invalida.";
            configurarPaneles(false, false, false, false, false, false);
        }
        else
        {
            if (DropDownList_CIUDAD_REQ.SelectedValue == "")
            {
                configurarMensajes(true, System.Drawing.Color.Orange);
                Label_MENSAJE.Text = "ADVERTENCIA: NO SE HA CARGADO CIUDAD DE REQUISICIÓN, Requisición invalida.";
                configurarPaneles(false, false, false, false, false, false);
            }
            else
            {
                configurarPaneles(false, true, false, false, false, false);

                configurarChecksEnviarACliente(false, false, false, false);
                configurarPanelesInternosEnviarACliente(false, false, false, false);
            }
        }
    }

    protected void Button_OCULTAR_ENVIAR_A_CLIENTES_Click(object sender, EventArgs e)
    {
        configurarPaneles(false, false, false, false, false, true);
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"].ToString());

        Decimal ID_REQUERIMIENTO = Convert.ToDecimal(QueryStringSeguro["requerimiento"]);

        Button_ENVIAR_CANDIDATOS.Visible = true;
    }


    

    protected void Button_ENVIAR_CANDIDATOS_SELECCIONADOS_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"].ToString());
        int ID_REQUERIMIENTO = Convert.ToInt32(QueryStringSeguro["requerimiento"]);

        requisicion _requisicion = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Int32 candidatosAEnviar = 0;
        for (int i = 0; i < GridView_ENVIAR_A_CLIENTE.Rows.Count; i++)
        {
            GridViewRow filaGrid = GridView_ENVIAR_A_CLIENTE.Rows[0];
            CheckBox check = filaGrid.FindControl("CheckBox_CANDIDATOS_SELECCIONADOS_ENVIAR") as CheckBox;

            if (check.Checked == true)
            {
                candidatosAEnviar += 1;
            }
        }

        radicacionHojasDeVida _solicitud = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        Decimal REGISTRO_RESULTADO = 0;
            
        int ID_SOLICITUD;
        int ID_EMPRESA;

        int contador_enviados = 0;
        int contador_errores = 0;

        DateTime FECHA_R = DateTime.Now;

        for (int i = 0; i < GridView_ENVIAR_A_CLIENTE.Rows.Count; i++)
        {
            GridViewRow filaGrid = GridView_ENVIAR_A_CLIENTE.Rows[i];
            CheckBox check = filaGrid.FindControl("CheckBox_CANDIDATOS_SELECCIONADOS_ENVIAR") as CheckBox;

            ID_SOLICITUD = Convert.ToInt32(GridView_ENVIAR_A_CLIENTE.DataKeys[i].Values["ID_SOLICITUD"]);

            if (check.Checked == true)
            {
                ID_EMPRESA = Convert.ToInt32(DropDownList_ID_EMPRESA.SelectedValue);

                DataTable temporalidad = _solicitud.ObtenerTemporalidad(ID_EMPRESA, ID_SOLICITUD);
                if (temporalidad.Rows.Count <= 0)
                {
                    REGISTRO_RESULTADO = _requisicion.AdicionarConAspEnviadosCliente(ID_SOLICITUD, ID_EMPRESA, ID_REQUERIMIENTO, FECHA_R);
                    if (REGISTRO_RESULTADO != 0)
                    {
                        contador_enviados += 1;
                    }
                    else
                    {
                        contador_errores += 1;
                    }
                }
                else
                {
                    DataRow filaTemporalidad = temporalidad.Rows[0];
                    if (Convert.ToInt32(filaTemporalidad["tiempo"].ToString()) < 96718)
                    {
                        REGISTRO_RESULTADO = _requisicion.AdicionarConAspEnviadosCliente(ID_SOLICITUD, ID_EMPRESA, ID_REQUERIMIENTO, FECHA_R);
                        if (REGISTRO_RESULTADO != 0)
                        {
                            contador_enviados += 1;
                        }
                        else
                        {
                            contador_errores += 1;
                        }
                    }
                    else
                    {
                        Label_MENSAJE.Text += "ADVERTENCIA: El candidato" + ID_SOLICITUD.ToString() + " no puede ser enviado a contratar por que ya cumplio el tiempo maximo de contratación con esta empresa" + ID_EMPRESA;
                    }
                }
            }
        }

        if (contador_enviados <= 0)
        {
            if (contador_errores > 0)
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text += "Ocurrieron " + contador_errores.ToString() + " errores al enviar candidatos.";
            }
            else
            {
                configurarMensajes(true, System.Drawing.Color.Orange);
                Label_MENSAJE.Text += "No se seleccionaron candidatos para enviar.";
            }
        }
        else
        {
            configurarMensajes(true, System.Drawing.Color.Green);
            Label_MENSAJE.Text = "Se enviaron a cliente los candidatos seleccionados correctamente.";

            if (contador_errores > 0)
            {
                Label_MENSAJE.Text += " Pero ocurrieron " + contador_errores.ToString() + " errores.";
            }
            configurarPaneles(false, false, false, false, false, true);
        }

        Button_ENVIAR_CANDIDATOS.Visible = true;

    }

    protected void Button_CANDIDATOS_EN_CLIENTE_Click(object sender, EventArgs e)
    {
        requisicion _requisicion = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"].ToString());
        Decimal ID_REQUERIMIENTO = Convert.ToDecimal(QueryStringSeguro["requerimiento"]);

        DataTable tablaEnviadosCliente = _requisicion.ObtenerConAspEnviadosClientePorIdRequerimientoEnCliente(ID_REQUERIMIENTO);

        if (tablaEnviadosCliente.Rows.Count <= 0)
        {
            configurarMensajes(true, System.Drawing.Color.Orange);
            Label_MENSAJE.Text = "No se encontraron candidatos en cliente.";
            configurarPaneles(false, false, false, false, false, true);
        }
        else
        {
            GridView_CANDIDATOS_EN_CLIENTE.DataSource = tablaEnviadosCliente;
            GridView_CANDIDATOS_EN_CLIENTE.DataBind();
            configurarPaneles(false, false, true, false, false, true);
            Panel_BOTONES_INTERNOS_ENVIAR_A_CONTRATAR.Visible = true;
            Panel_CONFIRMAR_SULEDO_ENVIAR_A_CONTRATAR.Visible = false;
            Panel_REQUISITOS_CUMPLIDOS.Visible = false;
            Panel_REQUISITOS_FALTANTES.Visible = false;

            SetVisibleEnvioAContratar();
        }
    
        Button_ENVIAR_CANDIDATOS.Visible = true;
    }

    private void SetVisibleEnvioAContratar()
    {
        Decimal ID_REQUERIMIENTO = Convert.ToDecimal(TextBox_ID_REQUERIMIENTO.Text);

        Int32 numGestion = ObtenerNumGestionReqPorContratarContratados(ID_REQUERIMIENTO);

        if (numGestion <= 0)
        {
            GridView_CANDIDATOS_EN_CLIENTE.Columns[1].Visible = false;
            configurarMensajes(true, System.Drawing.Color.Orange);
            Label_MENSAJE.Text = "No se pueden enviar a contratar más candidatos, debe esperar los resultados de los candidatos ya enviados a contratar.";
        }
        else
        {
            GridView_CANDIDATOS_EN_CLIENTE.Columns[1].Visible = true;
        }
    }


    protected void Button_OCULTAR_CANDIDATOS_EN_CLIENTE_Click(object sender, EventArgs e)
    {
        configurarPaneles(false, false, false, false, false, true);




        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"].ToString());

        Decimal ID_REQUERIMIENTO = Convert.ToDecimal(QueryStringSeguro["requerimiento"]);

        Button_ENVIAR_CANDIDATOS.Visible = true;
    }

    protected void Button_CANCELAR_ENVIO_Click(object sender, EventArgs e)
    {
        Panel_BOTONES_INTERNOS_ENVIAR_A_CONTRATAR.Visible = true;
        Panel_CONFIRMAR_SULEDO_ENVIAR_A_CONTRATAR.Visible = false;
    }

    protected void Button_CONFIRMAR_SUELDO_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"].ToString());
        int ID_REQUERIMIENTO = Convert.ToInt32(QueryStringSeguro["requerimiento"]);

        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean resultado = true;

        int ID_SOLICITUD = Convert.ToInt32(Label_ID_SOLICITUD.Text);

        DateTime F_ING_C = Convert.ToDateTime(TextBox_FECHA_INICIACION.Text);
        Decimal SUELDO_C = Convert.ToDecimal(TextBox_CONFIRMAR_SUELDO.Text);

        resultado = _radicacionHojasDeVida.ActualizarEstadoFechaIngresoYSueldoIngresoContratoRegSolicitudesIngreso(ID_REQUERIMIENTO, ID_SOLICITUD, "POR CONTRATAR", F_ING_C, SUELDO_C);
        if (DropDownList_TIP_REQ.SelectedItem.ToString() == "TRASLADO")
        {
            requisicion _requisicion = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            Decimal RESULTADO = _requisicion.AdicionarConRegSeg(ID_REQUERIMIENTO, System.DateTime.Today, "TRASLADO", "Se enviaron masivamente a contratar, sin validación de documentos");
        }

        if (resultado == true)
        {
            configurarMensajes(true, System.Drawing.Color.Green);
            Label_MENSAJE.Text = "El candidato seleccionado fue enviado a contratar correctamente.";
            configurarPaneles(false, false, false, false, false, true);

            Button_ENVIAR_CANDIDATOS.Visible = true;

        }
        else
        {
            configurarMensajes(true, System.Drawing.Color.Red);
            Label_MENSAJE.Text = _radicacionHojasDeVida.MensajeError;
        }
    }

    protected void Button_SEGUIMIENTO_Click(object sender, EventArgs e)
    {
        TextBox_FECHA_SEGUIMIENTO.Text = DateTime.Now.ToShortDateString();
        TextBox_FECHA_SEGUIMIENTO.Enabled = false;
        DropDownList_ACCION_SEGUIMIENTO.SelectedIndex = 0;
        TextBox_OBS_SEGUIMIENTO.Text = "";
        configurarPaneles(false, false, false, false, true, true);

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"].ToString());
        int ID_REQUERIMIENTO = Convert.ToInt32(QueryStringSeguro["requerimiento"]);

        requisicion _requisicion = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaConRegSeg = _requisicion.ObtenerConRegSegPorIdRequerimiento(ID_REQUERIMIENTO);
        GridView_SEGUIMIENTO.DataSource = tablaConRegSeg;
        GridView_SEGUIMIENTO.DataBind();

        TextBox_FECHA_SEGUIMIENTO.Text = DateTime.Now.ToShortDateString();
        DropDownList_ACCION_SEGUIMIENTO.SelectedIndex = 0;
        TextBox_OBS_SEGUIMIENTO.Text = "";

        Button_ENVIAR_CANDIDATOS.Visible = true;
    }

    protected void Button11_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"].ToString());
        int ID_REQUERIMIENTO = Convert.ToInt32(QueryStringSeguro["requerimiento"]);

        DateTime FECHA_R_SEGUIMIENTO = Convert.ToDateTime(TextBox_FECHA_SEGUIMIENTO.Text);
        String ACCION = DropDownList_ACCION_SEGUIMIENTO.SelectedValue;
        String COMENTARIOS = TextBox_OBS_SEGUIMIENTO.Text.ToUpper();

        requisicion _requisicion = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        Decimal RESULTADO = _requisicion.AdicionarConRegSeg(ID_REQUERIMIENTO, FECHA_R_SEGUIMIENTO, ACCION, COMENTARIOS);

        if (RESULTADO == 0)
        {
            configurarMensajes(true, System.Drawing.Color.Red);
            Label_MENSAJE.Text = _requisicion.MensajeError;
            configurarPaneles(false, false, false, false, false, true);

            Button_ENVIAR_CANDIDATOS.Visible = true;
        }
        else
        {
            DataTable tablaConRegSeg = _requisicion.ObtenerConRegSegPorIdRequerimiento(ID_REQUERIMIENTO);
            GridView_SEGUIMIENTO.DataSource = tablaConRegSeg;
            GridView_SEGUIMIENTO.DataBind();

            TextBox_FECHA_SEGUIMIENTO.Text = DateTime.Now.ToShortDateString();
            DropDownList_ACCION_SEGUIMIENTO.SelectedIndex = 0;
            TextBox_OBS_SEGUIMIENTO.Text = "";
        }
    }

    protected void Button_HISTORIAL_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"].ToString());
        Decimal ID_REQUERIMIENTO = Convert.ToDecimal(QueryStringSeguro["requerimiento"]);

        requisicion _requisicion = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaHistorial = _requisicion.ObtenerConAspEnviadosClientePorIdRequerimiento(ID_REQUERIMIENTO);

        if (tablaHistorial.Rows.Count <= 0)
        {
            configurarMensajes(true, System.Drawing.Color.Orange);
            Label_MENSAJE.Text = "No se encontró historial para esta requisición.";
            configurarPaneles(false, false, false, false, false, true);
        }
        else
        {
            GridView_HISTORIAL.DataSource = tablaHistorial;
            GridView_HISTORIAL.DataBind();
            configurarPaneles(false, false, false, true, false, true);
        }

        Button_ENVIAR_CANDIDATOS.Visible = true;
    }

    protected void Button10_Click(object sender, EventArgs e)
    {
        configurarPaneles(false, false, false, false, false, true);
        Button_ENVIAR_CANDIDATOS.Visible = true;
    }

    protected void Button_VOLVER_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = "seleccion";
        QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
        QueryStringSeguro["nombre_modulo"] = "HOJA DE TRABAJO";
        QueryStringSeguro["accion"] = "inicial";

        Response.Redirect("~/seleccion/hojaTrabajoSeleccion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }
    protected void CheckBox_ENVIAR_CANDIDATOS_FILTRO_ACIDO_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_ENVIAR_CANDIDATOS_FILTRO_ACIDO.Checked == true)
        {
            radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            perfil _perfil = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            int REGISTRO_PERFIL = Convert.ToInt32(DropDownList_PERFILES.SelectedValue);

            DataRow filaInfoPerfilSeleccionado = ObtenerPerfilPorRegistro(REGISTRO_PERFIL);

            String CIU_ASPIRANTE = DropDownList_CIUDAD_REQ.SelectedValue;
            DataTable tablaDisponiblesRequisicion = _radicacionHojasDeVida.ObtenerRegSolicitudesingresoPorPerfil(REGISTRO_PERFIL, CIU_ASPIRANTE);

            if (tablaDisponiblesRequisicion.Rows.Count <= 0)
            {
                configurarMensajes(true, System.Drawing.Color.Orange);
                Label_MENSAJE.Text = "No se encontraron candidatos disponibles para esta requisición que cumplieran con el perfil solicitado.";
                configurarPanelesInternosEnviarACliente(false, false, false, false);
                configurarChecksEnviarACliente(false, false, false, false);
            }
            else
            {
                GridView_ENVIAR_A_CLIENTE.DataSource = tablaDisponiblesRequisicion;
                GridView_ENVIAR_A_CLIENTE.DataBind();
                configurarPanelesInternosEnviarACliente(false, true, false, false);
                configurarChecksEnviarACliente(false, true, false, false);
            }
            configurarChecksEnviarACliente(false, true, false, false);
        }
        else
        {
            CheckBox_ENVIAR_CANDIDATOS_FILTRO_ACIDO.Checked = false;
        }
    }
    protected void CheckBox_ENVIAR_CANDIDATOS_FILTRO_VARIACION_PERFIL_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_ENVIAR_CANDIDATOS_FILTRO_VARIACION_PERFIL.Checked == true)
        {
            DataRow filaInfoPerfil = ObtenerPerfilPorRegistro(Convert.ToInt32(DropDownList_PERFILES.SelectedValue));

            if (filaInfoPerfil == null)
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = "No se encontró información del perfil. no se pudo continuar.";
            }
            else
            {
                configurarPanelesInternosEnviarACliente(false, false, true, false);

                RadioButtonList_FiltrarCargo.SelectedValue = "CARGO_EXACTO";

                TextBox_EDAD_MINIMA.Text = TextBox_EDAD_MIN.Text;
                TextBox_EDAD_MAXIMA.Text = TextBox_EDAD_MAX.Text;
                cargar_DropDownList_NIV_EDUCACION();
                DropDownList_NIV_EDUCACION.SelectedValue = filaInfoPerfil["NIV_ESTUDIOS"].ToString().Trim();
                cargar_DropDownList_SEXO();
                DropDownList_SEXO.SelectedValue = filaInfoPerfil["SEXO"].ToString().Trim();
                cargar_DropDownList_EXPERIENCIA();
                DropDownList_EXPERIENCIA.SelectedValue = filaInfoPerfil["EXPERIENCIA"].ToString().Trim();

                RadioButtonList_FiltroCiudad.SelectedValue = "CON_CIUDAD";
            }
            configurarChecksEnviarACliente(false, false, true, false);
        }
        else
        {
            CheckBox_ENVIAR_CANDIDATOS_FILTRO_VARIACION_PERFIL.Checked = false;
        }
    }
    protected void Button_TRAER_DATOS_FILTRO_VARIACION_Click(object sender, EventArgs e)
    {
        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        perfil _perfil = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        String FILTRO_CARGO = RadioButtonList_FiltrarCargo.SelectedValue;

        Decimal REGISTRO_PERFIL = Convert.ToDecimal(DropDownList_PERFILES.SelectedValue);
        Int32 EDAD_MAX = Convert.ToInt32(TextBox_EDAD_MAXIMA.Text);
        Int32 EDAD_MIN = Convert.ToInt32(TextBox_EDAD_MINIMA.Text);
        String NIV_EDUCACION = DropDownList_NIV_EDUCACION.SelectedValue;
        String EXPERIENCIA = DropDownList_EXPERIENCIA.SelectedValue;
        String SEXO = DropDownList_SEXO.SelectedValue;
        String CIU_ASPIRANTE = DropDownList_CIUDAD_REQ.SelectedValue;

        String FILTRO_CIUDAD = RadioButtonList_FiltroCiudad.SelectedValue;

        DataTable tablaDisponiblesRequisicion = _radicacionHojasDeVida.ObtenerRegSolicitudesingresoPorFiltroRequisicion(REGISTRO_PERFIL, EDAD_MAX, EDAD_MIN, NIV_EDUCACION, CIU_ASPIRANTE, SEXO, EXPERIENCIA, FILTRO_CARGO, FILTRO_CIUDAD);

        if (tablaDisponiblesRequisicion.Rows.Count <= 0)
        {
            configurarMensajes(true, System.Drawing.Color.Orange);
            Label_MENSAJE.Text = "No se encontraron candidatos disponibles para esta requisición que cumplieran con los datos seleccionados solicitados.";
            configurarPanelesInternosEnviarACliente(false, false, false, false);
            configurarChecksEnviarACliente(false, false, false, false);
        }
        else
        {
            GridView_ENVIAR_A_CLIENTE.DataSource = tablaDisponiblesRequisicion;
            GridView_ENVIAR_A_CLIENTE.DataBind();
            configurarPanelesInternosEnviarACliente(false, true, true, false);
            configurarChecksEnviarACliente(false, false, true, false);
        }
    }
    protected void Button_TRAER_DATOS_FILTRO_CEDULA_Click(object sender, EventArgs e)
    {
        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        string NUM_DOC_IDENTIDAD = TextBox_CEDULA.Text.Trim();

        DataTable tablaDisponiblesRequisicion = _radicacionHojasDeVida.ObtenerRegSolicitudesingresoPorNumDocIdentidadSoloSiDisponible(NUM_DOC_IDENTIDAD);

        if (tablaDisponiblesRequisicion.Rows.Count <= 0)
        {
            configurarMensajes(true, System.Drawing.Color.Orange);
            Label_MENSAJE.Text = "No se encontró un candidato disponible con ese numero de identificaión.";
            configurarPanelesInternosEnviarACliente(false, false, false, false);
            configurarChecksEnviarACliente(false, false, false, false);
        }
        else
        {
            GridView_ENVIAR_A_CLIENTE.DataSource = tablaDisponiblesRequisicion;
            GridView_ENVIAR_A_CLIENTE.DataBind();
            configurarPanelesInternosEnviarACliente(false, true, false, true);
            configurarChecksEnviarACliente(false, false, false, true);
        }
    }
    protected void CheckBox_ENVIAR_CANDIDATOS_FILTRO_CEDULA_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_ENVIAR_CANDIDATOS_FILTRO_CEDULA.Checked == true)
        {
            configurarPanelesInternosEnviarACliente(false, false, false, true);

            TextBox_CEDULA.Text = "";
            configurarChecksEnviarACliente(false, false, false, true);
        }
        else
        {
            CheckBox_ENVIAR_CANDIDATOS_FILTRO_CEDULA.Checked = false;
        }
    }
    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;

        String datosCapturados = TextBox_BUSCAR.Text.ToUpper();
        String campo = DropDownList_BUSCAR.SelectedValue.ToString();

        requisicion _requisicion = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaResultadosBusqueda = new DataTable();

        if (DropDownList_BUSCAR.SelectedValue == "RAZ_SOCIAL")
        {
            tablaResultadosBusqueda = _requisicion.ObtenerComRequerimientoPorRazSocial(datosCapturados, DropDownList_Cancelado.SelectedValue.ToString(), DropDownList_Cumplido.SelectedValue.ToString());
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "COD_EMPRESA")
            {
                tablaResultadosBusqueda = _requisicion.ObtenerComRequerimientoPorCodEmpresa(datosCapturados, DropDownList_Cancelado.SelectedValue.ToString(), DropDownList_Cumplido.SelectedValue.ToString());
            }
            else
            {
                if (DropDownList_BUSCAR.SelectedValue == "ID_REQUERIMIENTO")
                {
                    tablaResultadosBusqueda = _requisicion.ObtenerComRequerimientoPorIdRequerimiento(Convert.ToDecimal(datosCapturados));
                }
            }
        }

        if (tablaResultadosBusqueda.Rows.Count <= 0)
        {
            if (_requisicion.MensajeError != null)
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = _requisicion.MensajeError;
            }
            else
            {
                configurarMensajes(true, System.Drawing.Color.Orange);
                Label_MENSAJE.Text = "No se encontraron registros que cumplieran los datos de busqueda.";
            }

            configurarBotonesDeAccion(true, false, false, false, true);
            Panel_BOTONES_ACCION_1.Visible = false;

            Panel_GRID_RESULTADOS.Visible = false;

            Panel_FORMULARIO.Visible = false;

            configurarPaneles(false, false, false, false, false, false);
        }
        else
        {
            configurarBotonesDeAccion(true, false, false, false, true);
            Panel_BOTONES_ACCION_1.Visible = false;

            Panel_GRID_RESULTADOS.Visible = true;
            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaResultadosBusqueda;
            GridView_RESULTADOS_BUSQUEDA.DataBind();

            Panel_FORMULARIO.Visible = false;

            configurarPaneles(false, false, false, false, false, false);
        }
    }
    protected void CheckBox_RAZ_SOCIAL_CheckedChanged(object sender, EventArgs e)
    {
        if(CheckBox_RAZ_SOCIAL.Checked)
        {
            Panel_PARAMETROS.Visible = true;
            Panel_RAZ_SOCIAL.Visible = true;
        }
        else
        {
            Panel_PARAMETROS.Visible = true;
            Panel_RAZ_SOCIAL.Visible = false;
        }
    }
    protected void CheckBox_CUMPLIDO_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_CUMPLIDO.Checked)
        {
            Panel_PARAMETROS.Visible = true;
            Panel_CUMPLIDO.Visible = true;
        }
        else
        {
            Panel_PARAMETROS.Visible = true;
            Panel_CUMPLIDO.Visible = false;
        }
    }
    protected void CheckBox_CANCELADO_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_CANCELADO.Checked)
        {
            Panel_PARAMETROS.Visible = true;
            Panel_CANCELADO.Visible = true;
        }
        else
        {
            Panel_PARAMETROS.Visible = true;
            Panel_CANCELADO.Visible = false;
        }
    }
    protected void CheckBox_REGIONAL_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_REGIONAL.Checked)
        {
            Panel_PARAMETROS.Visible = true;
            Panel_REGIONAL.Visible = true;
            
            DropDownList_REGIONALES.Items.Clear();

            regional _regional = new regional(Session["idEmpresa"].ToString());
            DataTable tablaRegionales = _regional.ObtenerTodasLasRegionales();

            ListItem item = new ListItem("Seleccione Regional", "");
            DropDownList_REGIONALES.Items.Add(item);

            foreach (DataRow fila in tablaRegionales.Rows)
            {
                item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_REGIONAL"].ToString());
                DropDownList_REGIONALES.Items.Add(item);
            }

            DropDownList_REGIONALES.DataBind();
        }
        else
        {
            Panel_PARAMETROS.Visible = true;
            Panel_REGIONAL.Visible = false;
        }
    }
    protected void CheckBox_CIUDAD_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_CIUDAD.Checked)
        {
            Panel_PARAMETROS.Visible = true;
            Panel_CIUDAD.Visible = true;

            ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
            DataTable tablaCiudad = _ciudad.ObtenerTodasLasCiudades();

            ListItem item = new ListItem("Seleccione Ciudad", "");
            DropDownList_CIUDAD.Items.Add(item);

            foreach (DataRow fila in tablaCiudad.Rows)
            {
                item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_CIUDAD"].ToString());
                DropDownList_CIUDAD.Items.Add(item);
            }

            DropDownList_CIUDAD.DataBind();

        }
        else
        {
            Panel_PARAMETROS.Visible = true;
            Panel_CIUDAD.Visible = false;
        }
    }
    protected void CheckBox_PSICOLOGO_CheckedChanged(object sender, EventArgs e)
    {
        if(CheckBox_PSICOLOGO.Checked)
        {
            Panel_PARAMETROS.Visible = true;
            Panel_psicologo.Visible = true;
            
            DropDownList_PSICOLOGO.Items.Clear();

            usuario _usuario = new usuario(Session["idEmpresa"].ToString());
            DataTable tablaPsicologos = _usuario.ObtenerEmpleadosPorIdRol(usuario.Rol.Psicologo);

            ListItem item = new ListItem("Seleccione Psicologo", "");
            DropDownList_PSICOLOGO.Items.Add(item);

            foreach (DataRow fila in tablaPsicologos.Rows)
            {
                item = new ListItem(fila["NOMBRE_EMPLEADO"].ToString(), fila["ID_EMPLEADO"].ToString());
                DropDownList_PSICOLOGO.Items.Add(item);
            }

            DropDownList_PSICOLOGO.DataBind();
        }
        else
        {
            Panel_PARAMETROS.Visible = true;
            Panel_psicologo.Visible = false;
        }
    }
    protected void CheckBox_TIPO_CheckedChanged(object sender, EventArgs e)
    {
        if(CheckBox_TIPO.Checked)
        {
            Panel_PARAMETROS.Visible = true;
            Panel_TIPO.Visible = true;

            DropDownList_Tipo.Items.Clear();

            parametro _parametro = new parametro(Session["idEmpresa"].ToString());

            DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_TIPO_REQ);

            ListItem item = new ListItem("Ninguno", "");
            DropDownList_Tipo.Items.Add(item);

            foreach (DataRow fila in tablaParametros.Rows)
            {
                item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
                DropDownList_Tipo.Items.Add(item);
            }
            DropDownList_Tipo.DataBind();
        }
        else
        {
            Panel_PARAMETROS.Visible = true;
            Panel_TIPO.Visible = false;
        }
    }
    protected void CheckBox_FECHA_REQ_CheckedChanged(object sender, EventArgs e)
    {
        if(CheckBox_FECHA_REQ.Checked)
        {
            Panel_PARAMETROS.Visible = true;
            Panel_FECHA_REQ.Visible = true;
        }
        else
        {
            Panel_PARAMETROS.Visible = true;
            Panel_FECHA_REQ.Visible = false;
        }

    }
    protected void Button_BUSCAR_AVANZADA_Click(object sender, EventArgs e)
    {
        Panel_FORM_BOTONES.Visible = false;
        
        CheckBox_TIPO.Checked = false;
        CheckBox_REGIONAL.Checked = false;
        CheckBox_RAZ_SOCIAL.Checked = false;
        CheckBox_PSICOLOGO.Checked = false;
        CheckBox_FECHA_REQ.Checked = false;
        CheckBox_CUMPLIDO.Checked = false;
        CheckBox_CANCELADO.Checked = false;
        CheckBox_CIUDAD.Checked = false;

        Panel_PARAMETROS.Visible = true;
        Panel_RAZ_SOCIAL.Visible = false;
        Panel_CUMPLIDO.Visible = false;
        Panel_CANCELADO.Visible = false;
        Panel_REGIONAL.Visible = false;
        Panel_CIUDAD.Visible = false;
        Panel_psicologo.Visible = false;
        Panel_TIPO.Visible = false;
        Panel_FECHA_REQ.Visible = false;
        Panel_FORMULARIO.Visible = false;
    }
    protected void Button_BUSQUEDA_AVANZADA_Click(object sender, EventArgs e)
    {
        if (CheckBox_RAZ_SOCIAL.Checked == true || CheckBox_CUMPLIDO.Checked == true || CheckBox_CANCELADO.Checked == true ||
            CheckBox_REGIONAL.Checked == true || CheckBox_CIUDAD.Checked == true || CheckBox_PSICOLOGO.Checked == true ||
            CheckBox_TIPO.Checked == true || CheckBox_FECHA_REQ.Checked == true)
        {
            String RAZ_SOCIAL = null;
            String CUMPLIDO = null;
            String CANCELADO = null;
            String REGIONAL = null;
            String CIUDAD = null;
            String PSICOLOGO = null;
            String TIPO = null;
            DateTime FECHA_INICIAL = new DateTime();
            DateTime FECHA_FINAL = new DateTime();

            if (CheckBox_RAZ_SOCIAL.Checked)
            {
                RAZ_SOCIAL = TextBox_RAZON_SOCIAL.Text;
            }
            if(CheckBox_CUMPLIDO.Checked)
            {
                CUMPLIDO = DropDownList_CUMPLIDOS.SelectedItem.ToString();
            }
            if (CheckBox_CANCELADO.Checked)
            {
                CANCELADO = DropDownList_CANCELADOS.SelectedItem.ToString();
            }
            if (CheckBox_REGIONAL.Checked)
            {
                REGIONAL = DropDownList_REGIONALES.SelectedItem.ToString();
            }
            if (CheckBox_CIUDAD.Checked)
            {
                CIUDAD = DropDownList_CIUDAD.SelectedItem.ToString();
            }
            if (CheckBox_PSICOLOGO.Checked)
            {
                PSICOLOGO = DropDownList_PSICOLOGO.SelectedValue.ToString();
            }
            if (CheckBox_TIPO.Checked)
            {
                TIPO = DropDownList_Tipo.SelectedValue.ToString();
            }
            if (CheckBox_FECHA_REQ.Checked)
            {
                tools _tool = new tools();
                if((String.IsNullOrEmpty(TextBox_FECHA_FINAL.Text))||(String.IsNullOrEmpty(TextBox_FECHA_INICIAL.Text)))
                {
                    configurarMensajes(true, System.Drawing.Color.Orange);
                    Label_MENSAJE.Text = "Los Campos de Fecha inicial y Fecha final son requeridos";   
                }
                else
                {
                    FECHA_FINAL = Convert.ToDateTime(TextBox_FECHA_FINAL.Text.ToString());
                    FECHA_INICIAL = Convert.ToDateTime(TextBox_FECHA_INICIAL.Text.ToString());
                }
            }
            requisicion _req = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaReq = _req.ObtenerRequisicionesPorFiltro(RAZ_SOCIAL, CUMPLIDO, CANCELADO, REGIONAL, CIUDAD, PSICOLOGO, TIPO, FECHA_INICIAL, FECHA_FINAL);
            if (!(String.IsNullOrEmpty(_req.MensajeError)))
            {
                configurarMensajes(true, System.Drawing.Color.Orange);
                Label_MENSAJE.Text = "Para poder generar el reporte debe seleccionar por lo menos un parámetro de busqueda.";
            }
            if (tablaReq.Rows.Count <= 0)
            {
                configurarMensajes(true, System.Drawing.Color.Orange);
                Label_MENSAJE.Text = "No se encontraron resulados para la busqueda, con los parámetros seleccionados.";
                Panel_GRID_RESULTADOS.Visible = false;
                Panel_FORM_BOTONES.Visible = true;
            }
            else
            {
                GridView_RESULTADOS_BUSQUEDA.DataSource = tablaReq;
                GridView_RESULTADOS_BUSQUEDA.DataBind();
                Panel_GRID_RESULTADOS.Visible = true;
                Panel_FORM_BOTONES.Visible = true;
            }
        }
        else
        {
            configurarMensajes(true, System.Drawing.Color.Orange);
            Label_MENSAJE.Text = "Para poder generar el reporte debe seleccionar por lo menos un parámetro de busqueda.";
        }
    }
   
    private DataTable devolverRequisitosFaltantes(Decimal ID_REQUERIMIENTO, Decimal ID_SOLICITUD)
    {
        DataTable tablaResultado = new DataTable();
        tablaResultado.Columns.Add("TIPO_REQUISITO");
        tablaResultado.Columns.Add("DOCUMENTO");
        DataRow filaResultado;

        requisito _requisito = new requisito(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaTodosRequisitos = _requisito.VerificarRequisitos(ID_REQUERIMIENTO, ID_SOLICITUD);

        foreach (DataRow filaOriginal in tablaTodosRequisitos.Rows)
        {
            if ((filaOriginal["CUMPLIDO"].ToString() == "") && (filaOriginal["DEROGADO"].ToString() == ""))
            {
                filaResultado = tablaResultado.NewRow();
                filaResultado["TIPO_REQUISITO"] = filaOriginal["TIPO_REQUISITO"];
                filaResultado["DOCUMENTO"] = filaOriginal["DOCUMENTO"];

                tablaResultado.Rows.Add(filaResultado);
            }
        }

        return tablaResultado;
    }

    protected void GridView_CANDIDATOS_EN_CLIENTE_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"].ToString());
        int ID_REQUERIMIENTO = Convert.ToInt32(QueryStringSeguro["requerimiento"]);

        int filaSeleccionada = Convert.ToInt32(e.CommandArgument);
        int ID_SOLICITUD = Convert.ToInt32(GridView_CANDIDATOS_EN_CLIENTE.DataKeys[filaSeleccionada].Values["ID_SOLICITUD"]);

        Boolean resultado = true;
        if (e.CommandName == "disponible")
        {
            radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            resultado = _radicacionHojasDeVida.ActualizarEstadoRegSolicitudesIngreso(ID_REQUERIMIENTO, ID_SOLICITUD, "DISPONIBLE");

            if (resultado == true)
            {
                configurarMensajes(true, System.Drawing.Color.Green);
                Label_MENSAJE.Text = "Se actualizó correctamente el estado del candidato.";
            }
            else
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = _radicacionHojasDeVida.MensajeError;
            }

            configurarPaneles(false, false, false, false, false, true);

            Button_ENVIAR_CANDIDATOS.Visible = true;
        }
        else
        {
            if (e.CommandName == "contratar")
            {
                Label_ID_SOLICITUD.Text = ID_SOLICITUD.ToString();
                Panel_BOTONES_INTERNOS_ENVIAR_A_CONTRATAR.Visible = false;
                Panel_CONFIRMAR_SULEDO_ENVIAR_A_CONTRATAR.Visible = true;
                TextBox_CONFIRMAR_SUELDO.Text = TextBox_SALARIO.Text;
                TextBox_FECHA_INICIACION.Text = DateTime.Now.ToShortDateString();
                Label_NOMBRE_CANDIDATO.Text = GridView_CANDIDATOS_EN_CLIENTE.Rows[filaSeleccionada].Cells[5].Text + " " + GridView_CANDIDATOS_EN_CLIENTE.Rows[filaSeleccionada].Cells[6].Text;

                DataTable tablaRequisitosFaltantes = devolverRequisitosFaltantes(ID_REQUERIMIENTO, ID_SOLICITUD);

                if (DropDownList_TIP_REQ.SelectedItem.ToString() != "TRASLADO")
                {
                    if (tablaRequisitosFaltantes.Rows.Count <= 0)
                    {
                        Panel_REQUISITOS_FALTANTES.Visible = false;
                        Panel_REQUISITOS_CUMPLIDOS.Visible = true;
                        Button_CONFIRMAR_SUELDO.Visible = true;
                    }
                    else
                    {
                        Panel_REQUISITOS_FALTANTES.Visible = true;
                        Panel_REQUISITOS_CUMPLIDOS.Visible = false;
                        Button_CONFIRMAR_SUELDO.Visible = false;

                        GridView_REQUISITOS_FALTANTES.DataSource = tablaRequisitosFaltantes;
                        GridView_REQUISITOS_FALTANTES.DataBind();

                        configurarMensajes(true, System.Drawing.Color.Orange);
                        Label_MENSAJE.Text = "La persona que intenta enviar a contratar no cumple con los requerimientos.";
                    }
                }
                else
                {
                    Panel_REQUISITOS_FALTANTES.Visible = false;
                    Panel_REQUISITOS_CUMPLIDOS.Visible = true;
                    Button_CONFIRMAR_SUELDO.Visible = true;
                }
            }
        }
    }

    protected void Button_COPIAR_REQ_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"].ToString());

        String ID_REQUERIMIENTO = QueryStringSeguro["requerimiento"].ToString();

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
        QueryStringSeguro["img_area"] = "seleccion";
        QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
        QueryStringSeguro["nombre_modulo"] = "REGISTRO, ATENCIÓN DE REQUISICIONES";
        QueryStringSeguro["accion"] = "copiarReq";
        QueryStringSeguro["requerimiento"] = ID_REQUERIMIENTO;

        QueryStringSeguro["copia"] = "si";

        Response.Redirect("~/seleccion/registroAtencionRequisiciones.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }

    private void seleccionar_index_grilla(int index, GridView grilla)
    { 
        for(int i = 0; i < grilla.Rows.Count; i++)
        {
            grilla.Rows[i].BackColor = System.Drawing.Color.Transparent;
        }

        grilla.Rows[index].BackColor = colorSeleccionado;
    }

    protected void GridView_CONTRATOS_ACTIVOS_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            seleccionar_index_grilla(indexSeleccionado, GridView_CONTRATOS_ACTIVOS);

            Decimal REGISTRO_CONTRATO = Convert.ToDecimal(GridView_CONTRATOS_ACTIVOS.DataKeys[indexSeleccionado].Values["ID_SERVICIO_RESPECTIVO"]);

            HiddenField_ID_SERVICIO_RESPECTIVO.Value = REGISTRO_CONTRATO.ToString();

            GridView_CONTRATOS_ACTIVOS.SelectedIndex = indexSeleccionado;
        }
    }

    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {
        configurarMensajes(false, System.Drawing.Color.Green);
    }
    protected void Button_CERRAR_MENSAJES_CONTRATO_Click(object sender, EventArgs e)
    {
        configurarMensajes(false, System.Drawing.Color.Green);
    }
    protected void Button_CERRAR_MENSAJES_ABAJO_Click(object sender, EventArgs e)
    {
        configurarMensajes(false, System.Drawing.Color.Green);
    }

    private Int32 GEtPorcentajeDefecto(Int32 cantidadPersonal)
    {
        Int32 porcentaje = -1;

        if ((cantidadPersonal >= 1) && (cantidadPersonal <= 5))
        {
            porcentaje = 0;
        }
        else
        {
            if ((cantidadPersonal >= 6) && (cantidadPersonal <= 10))
            {
                porcentaje = 50;
            }
            else
            {
                if ((cantidadPersonal >= 11) && (cantidadPersonal <= 15))
                {
                    porcentaje = 100;
                }
                else
                {
                    if ((cantidadPersonal >= 16) && (cantidadPersonal <= 20))
                    {
                        porcentaje = 150;
                    }
                }
            }
        }

        return porcentaje;
    }

    private Int32 GetPorcentajeSegunCantidadPersonal(Int32 cantidadPersonal)
    {
        Int32 porcentaje = GEtPorcentajeDefecto(cantidadPersonal);
        
        parametro _par = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaDiasNiveles = _par.ObtenerParametrosPorTabla(tabla.PARAMETROS_PORCENTAJES_NIVELES_REQUERIMIENTO);

        if (tablaDiasNiveles.Rows.Count > 0)
        {
            String[] valoresArray = tablaDiasNiveles.Rows[0]["CODIGO"].ToString().Split(';');

            try
            {
                foreach (String valorCompletoNivel in valoresArray)
                {
                    Int32 numInicial = Convert.ToInt32(valorCompletoNivel.Split(':')[0].Split('-')[0]);
                    Int32 numFinal = Convert.ToInt32(valorCompletoNivel.Split(':')[0].Split('-')[1]);
                    Int32 numPorcentaje = Convert.ToInt32(valorCompletoNivel.Split(':')[1]);

                    if ((cantidadPersonal >= numInicial) && (cantidadPersonal <= numFinal))
                    {
                        porcentaje = numPorcentaje;
                        break;
                    }
                }
            }
            catch
            {
                porcentaje = GEtPorcentajeDefecto(cantidadPersonal);
            }
        }

        return porcentaje;
    }

    private Int32 GetNumeroDiazNivel(String nivelComplejidad)
    {
        Int32 numeroDiasNivelBaja = 3;
        Int32 numeroDiasNivelMedia = 5;
        Int32 numeroDiasNivelCompleja = 10;

        parametro _par = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaDiasNiveles = _par.ObtenerParametrosPorTabla(tabla.PARAMETROS_DIAS_NIVELES_REQUERIMIENTO);

        if (tablaDiasNiveles.Rows.Count > 0)
        {
            String[] diasNivelesArray = tablaDiasNiveles.Rows[0]["CODIGO"].ToString().Split(';');
            try
            {
                numeroDiasNivelBaja = Convert.ToInt32(diasNivelesArray[0]);
                numeroDiasNivelMedia = Convert.ToInt32(diasNivelesArray[1]);
                numeroDiasNivelCompleja = Convert.ToInt32(diasNivelesArray[2]);
            }
            catch
            {
                numeroDiasNivelBaja = 3;
                numeroDiasNivelMedia = 5;
                numeroDiasNivelCompleja = 10;
            }
        }

        if (nivelComplejidad.ToUpper() == "COMPLEJA")
        {
            return numeroDiasNivelCompleja;
        }
        else
        {
            if (nivelComplejidad.ToUpper() == "MEDIA")
            {
                return numeroDiasNivelMedia;
            }
            else
            {
                return numeroDiasNivelBaja;
            }
        }
    }

    private DateTime GetFechaRequiereCliente(Int32 cantidadPersonal, String nivelComplejidad)
    {
        
        Int32 numeroDias = GetNumeroDiazNivel(nivelComplejidad);
        Int32 porcentaje = GetPorcentajeSegunCantidadPersonal(cantidadPersonal);

        if (porcentaje == -1)
        {
            return new DateTime();
        }
        else
        { 
            Int32 numDiasASumar = numeroDias + ((porcentaje * numeroDias) / 100);

            CrtCalendario _calendario = new CrtCalendario(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DateTime fechaRequiereCliente = _calendario.ObtenerFechaRequiereCliente(DateTime.Now, numDiasASumar, (Int32)tabla.proceso.ContactoSeleccion);

            return fechaRequiereCliente;
        }   
    }

    protected void TextBox_CANTIDAD_TextChanged(object sender, EventArgs e)
    {
        Int32 cantidad = 0;
        try
        {
            cantidad = Convert.ToInt32(TextBox_CANTIDAD.Text);
        }
        catch
        {
            cantidad = 0;
        }

        String nivelComplejidad = HiddenField_NivelRequerimiento.Value;

        DateTime fechaRequiereCliente = GetFechaRequiereCliente(cantidad, nivelComplejidad);

        if (fechaRequiereCliente == new DateTime())
        {
            TextBox_FechaReferenciaSistema.Text = "";
            TextBox_FechaReferenciaSistema.Enabled = false;
        }
        else
        {
            TextBox_FechaReferenciaSistema.Enabled = false;
            TextBox_FechaReferenciaSistema.Text = fechaRequiereCliente.ToShortDateString();
        }
    }

    private void LlenarGridViewEnvioCorreos(Decimal ID_EMPRESA, String ID_CIUDAD)
    {
        requisicion _req = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaEnvios = _req.ObtenerEnvioEmailPorEmpresaCiudad(ID_EMPRESA, ID_CIUDAD);

        if (tablaEnvios.Rows.Count <= 0)
        {
            Panel_InformacionEnvioEmail.Visible = true;
            Panel_EnvioEmailIndeterminado.Visible = true;
            Label_EnvioEmailIndeterminado.Text = _mensaje_SinEnvioEmail;
            GridView_EnvioCorreos.DataSource = null;
        }
        else
        {
            Panel_InformacionEnvioEmail.Visible = true;
            Panel_EnvioEmailIndeterminado.Visible = false;
            GridView_EnvioCorreos.DataSource = tablaEnvios;
            GridView_EnvioCorreos.DataBind();
        }
    }

    protected void DropDownList_CIUDAD_REQ_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_CIUDAD_REQ.SelectedIndex <= 0)
        {
            Panel_InformacionEnvioEmail.Visible = true;
            Panel_EnvioEmailIndeterminado.Visible = true;
            Label_EnvioEmailIndeterminado.Text = _mensaje_envioEmailIndeterminado;
            GridView_EnvioCorreos.DataSource = null;
        }
        else
        {
            Decimal ID_EMPRESA = Convert.ToDecimal(DropDownList_ID_EMPRESA.SelectedValue);
            String ID_CIUDAD = DropDownList_CIUDAD_REQ.SelectedValue;

            LlenarGridViewEnvioCorreos(ID_EMPRESA, ID_CIUDAD);
        }        
    }

    private Int32 ObtenerNumGestionReq(Decimal ID_REQUERIMIENTO)
    {
        requisicion _requisicion = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Int32 numero = 0;
        String resultado = null;

        DataTable tablaEstadoAtencionReq = _requisicion.ObtenerEstadoGestionReq(Convert.ToDecimal(ID_REQUERIMIENTO));

        if (tablaEstadoAtencionReq.Rows.Count <= 0)
        {
            if (_requisicion.MensajeError != null)
            {
                resultado = "ERROR: " + _requisicion.MensajeError;
            }
            else
            {
                resultado = "ADVERTENCIA: " + "no se encontró información de genstión sobre la requisición seleccionada.";
            }
            numero = 0;
        }
        else
        {
            DataRow filaGestion = tablaEstadoAtencionReq.Rows[0];

            numero = Convert.ToInt32(filaGestion["numGestion"]);
            resultado = null;
        }

        return numero;
    }

    private Int32 ObtenerNumGestionReqPorContratarContratados(Decimal ID_REQUERIMIENTO)
    {
        requisicion _requisicion = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Int32 numero = 0;
        String resultado = null;

        DataTable tablaEstadoAtencionReq = _requisicion.ObtenerEstadoGestionReqPorContratarContratados(Convert.ToDecimal(ID_REQUERIMIENTO));

        if (tablaEstadoAtencionReq.Rows.Count <= 0)
        {
            if (_requisicion.MensajeError != null)
            {
                resultado = "ERROR: " + _requisicion.MensajeError;
            }
            else
            {
                resultado = "ADVERTENCIA: " + "no se encontró información de genstión sobre la requisición seleccionada.";
            }
            numero = 0;
        }
        else
        {
            DataRow filaGestion = tablaEstadoAtencionReq.Rows[0];

            numero = Convert.ToInt32(filaGestion["numGestion"]);
            resultado = null;
        }

        return numero;
    }
    protected void Button_Trazabilidad_Click(object sender, EventArgs e)
    {
            configurarPaneles(true, false, false, false, false, false);    
            tools _tools = new tools();
            SecureQueryString QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);
            Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable ConceptoGVT = ConstructorReclutamiento.Trazabilidad(Session["idEmpresa"].ToString(), QueryStringSeguro["requerimiento"].ToString());
            GridViewTrazavilidad.DataSource = ConceptoGVT;
            GridViewTrazavilidad.DataBind();
    }
}