using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brainsbits.LLB.comercial;
using System.Data;
using Brainsbits.LLB.operaciones;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.seleccion;
using Brainsbits.LLB.contratacion;
using TSHAK.Components;

public partial class _ManualServicio : System.Web.UI.Page
{
    private String NOMBRE_AREA = tabla.NOMBRE_AREA_OPERACIONES;
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

    private enum Acciones
    {
        Inicio = 0, 
        NuevoManual,
        CargarManual,
        ModificarManual,
        Panel_RESULTADOS_GRID,
        Informacion_basica_comercial
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2

    }

    private enum AccionesGrilla
    {
        Ninguna,
        Nuevo,
        Modificar,
        Eliminar
    }

    private Decimal GLO_ID_SUB_C = 0;
    private Decimal GLO_ID_CENTRO_C = 0;
    private String GLO_ID_CIUDAD = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (IsPostBack == false)
        {
            Iniciar();
        }
    }

    private void ocultar_mensaje(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.Style.Add("display", "none");
        panel_mensaje.Style.Add("display", "none");

        panel_fondo.Visible = false;
        panel_mensaje.Visible = false;
    }

    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.ModificarManual);
        Mostrar(Acciones.ModificarManual);
    }

    private void Guardar()
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        List<ManualServicio> listaAdicionales = new List<ManualServicio>();

        for (int i = 0; i < GridView_AdicionalesComercial.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_AdicionalesComercial.Rows[i];
            ManualServicio _manualParaLista = new ManualServicio();

            TextBox texto_Titulo = filaGrilla.FindControl("TextBox_TituloAdicional") as TextBox;
            TextBox texto_Descripcion = filaGrilla.FindControl("TextBox_DescripcionAdicional") as TextBox;

            Decimal ID_ADICIONAL = Convert.ToDecimal(GridView_AdicionalesComercial.DataKeys[i].Values["ID_ADICIONAL"]);
            String TITULO = texto_Titulo.Text;
            String DESCRIPCION = texto_Descripcion.Text;

            _manualParaLista.AREA = ManualServicio.ListaSecciones.Comercial;
            _manualParaLista.DESCRIPCION = DESCRIPCION;
            _manualParaLista.ID_ADICIONAL = ID_ADICIONAL;
            _manualParaLista.ID_EMPRESA = ID_EMPRESA;
            _manualParaLista.TITULO = TITULO;

            listaAdicionales.Add(_manualParaLista);
        }

        for (int i = 0; i < GridView_AdicionalesSeleccion.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_AdicionalesSeleccion.Rows[i];
            ManualServicio _manualParaLista = new ManualServicio();

            TextBox texto_Titulo = filaGrilla.FindControl("TextBox_TituloAdicional") as TextBox;
            TextBox texto_Descripcion = filaGrilla.FindControl("TextBox_DescripcionAdicional") as TextBox;

            Decimal ID_ADICIONAL = Convert.ToDecimal(GridView_AdicionalesSeleccion.DataKeys[i].Values["ID_ADICIONAL"]);
            String TITULO = texto_Titulo.Text;
            String DESCRIPCION = texto_Descripcion.Text;

            _manualParaLista.AREA = ManualServicio.ListaSecciones.Seleccion;
            _manualParaLista.DESCRIPCION = DESCRIPCION;
            _manualParaLista.ID_ADICIONAL = ID_ADICIONAL;
            _manualParaLista.ID_EMPRESA = ID_EMPRESA;
            _manualParaLista.TITULO = TITULO;

            listaAdicionales.Add(_manualParaLista);
        }

        for (int i = 0; i < GridView_AdicionalesContratacion.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_AdicionalesContratacion.Rows[i];
            ManualServicio _manualParaLista = new ManualServicio();

            TextBox texto_Titulo = filaGrilla.FindControl("TextBox_TituloAdicional") as TextBox;
            TextBox texto_Descripcion = filaGrilla.FindControl("TextBox_DescripcionAdicional") as TextBox;

            Decimal ID_ADICIONAL = Convert.ToDecimal(GridView_AdicionalesContratacion.DataKeys[i].Values["ID_ADICIONAL"]);
            String TITULO = texto_Titulo.Text;
            String DESCRIPCION = texto_Descripcion.Text;

            _manualParaLista.AREA = ManualServicio.ListaSecciones.Contratacion;
            _manualParaLista.DESCRIPCION = DESCRIPCION;
            _manualParaLista.ID_ADICIONAL = ID_ADICIONAL;
            _manualParaLista.ID_EMPRESA = ID_EMPRESA;
            _manualParaLista.TITULO = TITULO;

            listaAdicionales.Add(_manualParaLista);
        }






        ManualServicio _manual = new ManualServicio(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        Decimal ID_VERSIONAMIENTO = _manual.CrearManualParaEmpresa(ID_EMPRESA, listaAdicionales);

        if (ID_VERSIONAMIENTO <= 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _manual.MensajeError, Proceso.Error);
        }
        else
        {
            maestrasInterfaz _maestrasInterfaz = new maestrasInterfaz();
            _maestrasInterfaz.CargarEnBdElManualServicioActual(ID_EMPRESA);

            Cargar(ID_EMPRESA);
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El Manual fue creado correctamente.", Proceso.Correcto);
        }
    }

    private void Actualizar()
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        List<ManualServicio> listaAdicionales = new List<ManualServicio>();

        for (int i = 0; i < GridView_AdicionalesComercial.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_AdicionalesComercial.Rows[i];

            ManualServicio _manualParaLista = new ManualServicio();

            TextBox texto_Titulo = filaGrilla.FindControl("TextBox_TituloAdicional") as TextBox;
            TextBox texto_Descripcion = filaGrilla.FindControl("TextBox_DescripcionAdicional") as TextBox;

            Decimal ID_ADICIONAL = Convert.ToDecimal(GridView_AdicionalesComercial.DataKeys[i].Values["ID_ADICIONAL"]);
            String TITULO = texto_Titulo.Text;
            String DESCRIPCION = texto_Descripcion.Text;

            _manualParaLista.AREA = ManualServicio.ListaSecciones.Comercial;
            _manualParaLista.DESCRIPCION = DESCRIPCION;
            _manualParaLista.ID_ADICIONAL = ID_ADICIONAL;
            _manualParaLista.ID_EMPRESA = ID_EMPRESA;
            _manualParaLista.TITULO = TITULO;

            listaAdicionales.Add(_manualParaLista);
        }

        for (int i = 0; i < GridView_AdicionalesSeleccion.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_AdicionalesSeleccion.Rows[i];
            ManualServicio _manualParaLista = new ManualServicio();

            TextBox texto_Titulo = filaGrilla.FindControl("TextBox_TituloAdicional") as TextBox;
            TextBox texto_Descripcion = filaGrilla.FindControl("TextBox_DescripcionAdicional") as TextBox;

            Decimal ID_ADICIONAL = Convert.ToDecimal(GridView_AdicionalesSeleccion.DataKeys[i].Values["ID_ADICIONAL"]);
            String TITULO = texto_Titulo.Text;
            String DESCRIPCION = texto_Descripcion.Text;

            _manualParaLista.AREA = ManualServicio.ListaSecciones.Seleccion;
            _manualParaLista.DESCRIPCION = DESCRIPCION;
            _manualParaLista.ID_ADICIONAL = ID_ADICIONAL;
            _manualParaLista.ID_EMPRESA = ID_EMPRESA;
            _manualParaLista.TITULO = TITULO;

            listaAdicionales.Add(_manualParaLista);
        }


        for (int i = 0; i < GridView_AdicionalesContratacion.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_AdicionalesContratacion.Rows[i];
            ManualServicio _manualParaLista = new ManualServicio();

            TextBox texto_Titulo = filaGrilla.FindControl("TextBox_TituloAdicional") as TextBox;
            TextBox texto_Descripcion = filaGrilla.FindControl("TextBox_DescripcionAdicional") as TextBox;

            Decimal ID_ADICIONAL = Convert.ToDecimal(GridView_AdicionalesContratacion.DataKeys[i].Values["ID_ADICIONAL"]);
            String TITULO = texto_Titulo.Text;
            String DESCRIPCION = texto_Descripcion.Text;

            _manualParaLista.AREA = ManualServicio.ListaSecciones.Contratacion;
            _manualParaLista.DESCRIPCION = DESCRIPCION;
            _manualParaLista.ID_ADICIONAL = ID_ADICIONAL;
            _manualParaLista.ID_EMPRESA = ID_EMPRESA;
            _manualParaLista.TITULO = TITULO;

            listaAdicionales.Add(_manualParaLista);
        }




        ManualServicio _manual = new ManualServicio(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        Boolean verificado = _manual.ActualizarManualParaEmpresa(ID_EMPRESA, listaAdicionales);

        if (verificado == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _manual.MensajeError, Proceso.Error);
        }
        else
        {
            maestrasInterfaz _maestrasInterfaz = new maestrasInterfaz();
            _maestrasInterfaz.CargarEnBdElManualServicioActual(ID_EMPRESA);

            Cargar(ID_EMPRESA);
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El Manual fue modificado correctamente.", Proceso.Correcto);
        }
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(HiddenField_ID_VERSIONAMIENTO.Value) == true)
        {
            Guardar();
        }
        else
        {
            Actualizar();
        }
    }

    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {

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
                    if (DropDownList_BUSCAR.SelectedValue == "NIT_EMPRESA")
                    {
                        configurarCaracteresAceptadosBusqueda(false, true);
                    }
                }
            }
        }
        TextBox_BUSCAR.Text = "";
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

    private void Buscar()
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);

        String datosCapturados = HiddenField_FILTRO_DATO.Value;
        String campo = HiddenField_FILTRO_DROP.Value;

        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaResultadosBusqueda = new DataTable();


        if (DropDownList_BUSCAR.SelectedValue == "RAZ_SOCIAL")
        {
            tablaResultadosBusqueda = _cliente.ObtenerEmpresaConRazSocial(datosCapturados);
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "COD_EMPRESA")
            {
                tablaResultadosBusqueda = _cliente.ObtenerEmpresaConCodEmpresa(datosCapturados);
            }
            if (DropDownList_BUSCAR.SelectedValue == "NIT_EMPRESA")
            {
                tablaResultadosBusqueda = _cliente.ObtenerEmpresaConNitEmpresa(datosCapturados);
            }
        }

        if (tablaResultadosBusqueda.Rows.Count <= 0)
        {
            if (_cliente.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _cliente.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros que cumplieran los datos de busqueda.", Proceso.Advertencia);
            }
        }
        else
        {
            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaResultadosBusqueda;
            GridView_RESULTADOS_BUSQUEDA.DataBind();

            Panel_RESULTADOS_GRID.Visible = true;
        }
    }

    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "CON_FILTRO";

        HiddenField_FILTRO_DROP.Value = DropDownList_BUSCAR.SelectedValue;
        HiddenField_FILTRO_DATO.Value = TextBox_BUSCAR.Text;

        Buscar();

    }

    private void CargarLogoEmpresaCabecera()
    {
        if (Session["idEmpresa"].ToString() == "1")
        {
            Image_LOGO_EMPRESA_MANUAL.ImageUrl = "~/imagenes/reportes/logo_sertempo.png";
        }
        else
        {
            Image_LOGO_EMPRESA_MANUAL.ImageUrl = "~/imagenes/reportes/logo_eficiencia.png";
        }
    }

    private void CargarCabeceraManualNuevo()
    {
        CargarLogoEmpresaCabecera();

        Label_FECHA_EMISION.Text = DateTime.Now.ToShortDateString();
        Label_VERSION.Text = "1.0";
        Label_APLICAR_A_APARTIR.Text = DateTime.Now.ToShortDateString();
        Label_PAGINA.Text = "NA";
    }

    private void CargarIdentificacionCliente(Decimal ID_EMPRESA)
    { 
        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaCliente = _cliente.ObtenerEmpresaConIdEmpresa(ID_EMPRESA);
        DataRow filaCliente = tablaCliente.Rows[0];

        Label_RAZ_SOCIAL.Text = filaCliente["RAZ_SOCIAL"].ToString().Trim();
        Label_ActividadEconomica.Text = filaCliente["ACT_ECO"].ToString().Trim();
        Label_NIT.Text = filaCliente["NIT_EMPRESA"].ToString().Trim() + "-" + filaCliente["DIG_VER"].ToString().Trim();
        Label_REPRESENTANTE_LEGAL.Text = filaCliente["NOM_REP_LEGAL"].ToString().Trim();
        Label_CEDULA_REPRESENTANTE.Text = filaCliente["TIP_DOC_REP_LEGAL"].ToString().Trim() + " " + filaCliente["CC_REP_LEGAL"].ToString().Trim();
        Label_DIRECCION_CLIENTE.Text = filaCliente["DIR_EMP"].ToString().Trim() + " (" + filaCliente["ID_CIUDAD_EMPRESA"].ToString().Trim() + ").";
        Label_Telefono.Text = filaCliente["TEL_EMP"].ToString().Trim() + " / " + filaCliente["TEL_EMP1"].ToString().Trim() + " CEL: " + filaCliente["NUM_CELULAR"].ToString();
    }


    private void CargarContactosComerciales(Decimal ID_EMPRESA)
    {
        contactos _contacto = new contactos(Session["idEmpresa"].ToString());
        DataTable tablaContactos = _contacto.ObtenerContactosPorIdEmpresa(ID_EMPRESA, tabla.proceso.ContactoComercial);

        GridView_ContactosComerciales.DataSource = tablaContactos;
        GridView_ContactosComerciales.DataBind();
    }

    private void CargarUnidadNegocio(Decimal ID_EMPRESA)
    {
        seguridad _seguridad = new seguridad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaContratosOriginal = _seguridad.ObtenerUnidadesNegocioDeUnaEmpresa(ID_EMPRESA);
 
        GridView_UNIDAD_NEGOCIO.DataSource = tablaContratosOriginal;
        GridView_UNIDAD_NEGOCIO.DataBind();

        Informacion_Basica_comercial _seguridadd = new Informacion_Basica_comercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaInformacionBasica = _seguridadd.ObtenerInformacionBasicaporId_Empresa(ID_EMPRESA);

        GVInfoBasica.DataSource = tablaInformacionBasica;
        GVInfoBasica.DataBind();
    }

        private void CargarInformacionBasicaDelCliente(Decimal ID_EMPRESA)
    {
        Informacion_Basica_comercial _seguridadd = new Informacion_Basica_comercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaInformacionBasica = _seguridadd.ObtenerInformacionBasicaporId_Empresa(ID_EMPRESA);

        GVInfoBasica.DataSource = tablaInformacionBasica;
        GVInfoBasica.DataBind();
        GVInfoBasica.Visible = true;
    }



    private void CargarGrillaAdicionalesDesdeTabla(DataTable tablaAdicionales, GridView grilla)
    {
        grilla.DataSource = tablaAdicionales;
        grilla.DataBind();

        for (int i = 0; i < grilla.Rows.Count; i++)
        {
            DataRow filaTabla = tablaAdicionales.Rows[i];

            TextBox texto_TituloAdicional = grilla.Rows[i].FindControl("TextBox_TituloAdicional") as TextBox;
            TextBox texto_DescripcionAdicional = grilla.Rows[i].FindControl("TextBox_DescripcionAdicional") as TextBox;

            texto_TituloAdicional.Text = filaTabla["TITULO"].ToString().Trim();
            texto_DescripcionAdicional.Text = filaTabla["DESCRIPCION"].ToString().Trim();
        }
    }

    private void inhabilitarFilasGrilla(GridView grilla, int colInicio)
    {
        for (int i = 0; i < grilla.Rows.Count; i++)
        {
            for (int j = colInicio; j < grilla.Columns.Count; j++)
            {
                grilla.Rows[i].Cells[j].Enabled = false;
            }
        }
    }

    private void CargarAdicionales(Decimal ID_EMPRESA, ManualServicio.ListaSecciones seccion, GridView grilla)
    { 
        ManualServicio _manual = new ManualServicio(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaAdicionales = _manual.ObtenerAdicionalesPorEmpresaYArea(ID_EMPRESA, seccion.ToString());

        CargarGrillaAdicionalesDesdeTabla(tablaAdicionales, grilla);
        inhabilitarFilasGrilla(grilla, 2);
    }

    private void CargarCabeceraManual(DataRow filaTabla)
    {
        HiddenField_ID_VERSIONAMIENTO.Value = filaTabla["ID_VERSIONAMIENTO"].ToString().Trim();

        CargarLogoEmpresaCabecera();

        Label_FECHA_EMISION.Text = Convert.ToDateTime(filaTabla["FECHA_EMISION"]).ToShortDateString();
        Label_VERSION.Text = filaTabla["VERSION_MAYOR"].ToString().Trim() + "." + filaTabla["VERSION_MENOR"].ToString().Trim();
        Label_APLICAR_A_APARTIR.Text = Convert.ToDateTime(filaTabla["APLICAR_A_PARTIR"]).ToShortDateString();
        Label_PAGINA.Text = "NA";
    }

    private void CargarGrillaModificacionesDesdeTabla(DataTable tablaModificaciones, GridView grilla)
    {
        grilla.DataSource = tablaModificaciones;
        grilla.DataBind();

        for (int i = 0; i < grilla.Rows.Count; i++)
        {
            DataRow filaTabla = tablaModificaciones.Rows[i];

            tools _tools = new tools();
            SecureQueryString QueryStringSeguro;
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

            QueryStringSeguro["version"] = filaTabla["ID_VERSIONAMIENTO"].ToString().Trim();

            HyperLink linkManual = grilla.Rows[i].FindControl("HyperLink_archivo") as HyperLink;

            linkManual.Target = "_blank";
            linkManual.NavigateUrl = "~/Operaciones/visorManualServicio.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        }
    }

    private void CargarModificacionesManual(Decimal ID_EMPRESA)
    { 
        ManualServicio _manual = new ManualServicio(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaModificaciones = _manual.ObtenerModificacionesManualPorEmpresa(ID_EMPRESA);

        CargarGrillaModificacionesDesdeTabla(tablaModificaciones, GridView_ControlModificaciones);
    }

    private void llenarGridCobertura(Decimal idEmpresa)
    {
        cobertura _cobertura = new cobertura(Session["idEmpresa"].ToString());
        DataTable tablaCobertura = _cobertura.obtenerCoberturaDeUnCliente(idEmpresa);

        if (tablaCobertura.Rows.Count > 0)
        {
            GridView_COBERTURA.DataSource = tablaCobertura;
            GridView_COBERTURA.DataBind();
        }
        else
        {
            GridView_COBERTURA.DataSource = null;
            GridView_COBERTURA.DataBind();
        }
    }

    private void CargarDatosPerfilesUnificado(Decimal ID_EMPRESA)
    {
        perfil _perfil = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaPerfiles = _perfil.ObtenerVenDPerfilesConNomOcupacionDocumentosYPruebasPorIdEmpresa(ID_EMPRESA);

        if (tablaPerfiles.Rows.Count > 0)
        {
            GridView_PERFILES.DataSource = tablaPerfiles;
            GridView_PERFILES.DataBind();

            GridView_DocumentosPruebasPerfiles.DataSource = tablaPerfiles;
            GridView_DocumentosPruebasPerfiles.DataBind();
        }
        else
        {
            GridView_PERFILES.DataSource = null;
            GridView_PERFILES.DataBind();

            GridView_DocumentosPruebasPerfiles.DataSource = null;
            GridView_DocumentosPruebasPerfiles.DataBind();
        }
    }

    private DataTable ConfigurarTablaPerfilesDocumentosPruebas()
    { 
        DataTable tablaTemp = new DataTable();

        tablaTemp.Columns.Add("REGISTRO");
        tablaTemp.Columns.Add("NOM_OCUPACION");
        tablaTemp.Columns.Add("DOCUMENTOS_REQUERIDOS");
        tablaTemp.Columns.Add("PRUEBAS_APLICADAS");

        return tablaTemp;
    }

    private void CargarCondicionesEnvioSeleccion(Decimal ID_EMPRESA)
    {
        envioCandidato _envioCandidato = new envioCandidato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaCondicionesEnvio = _envioCandidato.ObtenerTodosLosContactosParaEnvioDeCandidatosPorIdEmpresa(ID_EMPRESA);

        if (tablaCondicionesEnvio.Rows.Count > 0)
        {
            GridView_CondicionesEnvioSeleccion.DataSource = tablaCondicionesEnvio;
        }
        else
        {
            GridView_CondicionesEnvioSeleccion.DataSource = null;
        }

        GridView_CondicionesEnvioSeleccion.DataBind();
    }

    private DataTable ConfigurarTablaPerfilesCondicionesContratacion()
    {
        DataTable tablaTemp = new DataTable();

        tablaTemp.Columns.Add("REGISTRO");
        tablaTemp.Columns.Add("PERFIL");
        tablaTemp.Columns.Add("NOM_SUB_C");
        tablaTemp.Columns.Add("NOM_CC");
        tablaTemp.Columns.Add("NOMBRE_CIUDAD");
        tablaTemp.Columns.Add("PORCENTAJE_RIESGO");
        tablaTemp.Columns.Add("DOC_TRAB");
        tablaTemp.Columns.Add("OBS_CTE");

        return tablaTemp;
    }



    private void CargarInformacionCondicionesContratacionUnificada(Decimal ID_EMPRESA)
    {
        perfil _perfil = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaPerfiles = _perfil.ObtenerVenDPerfilesConNomOcupacionExamenesDocumentosYRequermientosPorIdEmpresa(ID_EMPRESA);

        
        if (tablaPerfiles.Rows.Count <= 0)
        {
            GridView_CondicionesContratacion.DataSource = null;
            GridView_CondicionesContratacion.DataBind();

            GridView_ExamenesMedicosPerfil.DataSource = null;
            GridView_ExamenesMedicosPerfil.DataBind();
        }
        else
        {
            GridView_CondicionesContratacion.DataSource = tablaPerfiles;
            GridView_CondicionesContratacion.DataBind();

            GridView_ExamenesMedicosPerfil.DataSource = tablaPerfiles;
            GridView_ExamenesMedicosPerfil.DataBind();
        } 
    }

    private void CargarVariablesCiudadCentroYSubCentro(DataRow filaCondicion)
    {
        if ((DBNull.Value.Equals(filaCondicion["ID_SUB_C"]) == false) && (filaCondicion["ID_SUB_C"].ToString().Trim() != "0"))
        {
            GLO_ID_SUB_C = Convert.ToDecimal(filaCondicion["ID_SUB_C"]);
            GLO_ID_CENTRO_C = 0;
            GLO_ID_CIUDAD = null;
        }
        else
        {
            if ((DBNull.Value.Equals(filaCondicion["ID_CENTRO_C"]) == false) && (filaCondicion["ID_CENTRO_C"].ToString().Trim() != "0"))
            {
                GLO_ID_SUB_C = 0;
                GLO_ID_CENTRO_C = Convert.ToDecimal(filaCondicion["ID_CENTRO_C"]);
                GLO_ID_CIUDAD = null;
            }
            else
            {
                GLO_ID_SUB_C = 0;
                GLO_ID_CENTRO_C = 0;
                GLO_ID_CIUDAD = filaCondicion["ID_CIUDAD"].ToString().Trim();
            }
        }
    }

    private DataTable ConfigurarTablaPerfilesExamenes()
    {
        DataTable tablaTemp = new DataTable();

        tablaTemp.Columns.Add("REGISTRO");
        tablaTemp.Columns.Add("PERFIL");
        tablaTemp.Columns.Add("NOM_SUB_C");
        tablaTemp.Columns.Add("NOM_CC");
        tablaTemp.Columns.Add("NOMBRE_CIUDAD");
        tablaTemp.Columns.Add("EXAMENES_MEDICOS_REQUERIDOS");

        return tablaTemp;
    }

    private DataTable ConfigurarTablaPerfilesClausulas()
    {
        DataTable tablaTemp = new DataTable();

        tablaTemp.Columns.Add("REGISTRO");
        tablaTemp.Columns.Add("PERFIL");
        tablaTemp.Columns.Add("CLAUSULAS_ADICIONALES");

        return tablaTemp;
    }

    private void CargarClausulasAdicionales(Decimal ID_EMPRESA)
    {
        DataTable tablaPerfilesClausulas = ConfigurarTablaPerfilesClausulas();

        perfil _perfil = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaPerfiles = _perfil.ObtenerVenDPerfilesConOcupacionPorIdEmpresa(ID_EMPRESA);

        foreach (DataRow filaPerfil in tablaPerfiles.Rows)
        {
            Decimal ID_PERFIL = Convert.ToDecimal(filaPerfil["REGISTRO"]);
            String PERFIL = filaPerfil["NOM_OCUPACION"].ToString().Trim() + " (Entre " + filaPerfil["EDAD_MIN"].ToString().Trim() + " y " + filaPerfil["EDAD_MAX"].ToString().Trim() + ")."; 

            condicionesContratacion _condicionesContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaClausulas = _condicionesContratacion.obtenerClausulasPorPerfil(ID_PERFIL);

            String CLAUSULAS = String.Empty;
            foreach(DataRow filaClausula in tablaClausulas.Rows)
            {
                if (String.IsNullOrEmpty(CLAUSULAS) == true)
                {
                    CLAUSULAS = filaClausula["NOMBRE"].ToString().Trim() + ".";
                }
                else
                {
                    CLAUSULAS += "<br>" + filaClausula["NOMBRE"].ToString().Trim();
                }
            }

            DataRow filaPerfilClausula = tablaPerfilesClausulas.NewRow();

            filaPerfilClausula["REGISTRO"] = ID_PERFIL;
            filaPerfilClausula["PERFIL"] = PERFIL;
            if(String.IsNullOrEmpty(CLAUSULAS) == true)
            {
                filaPerfilClausula["CLAUSULAS_ADICIONALES"] = "Ninguna."; 
            }
            else
            {
                filaPerfilClausula["CLAUSULAS_ADICIONALES"] = CLAUSULAS;
            }

            tablaPerfilesClausulas.Rows.Add(filaPerfilClausula);
            tablaPerfilesClausulas.AcceptChanges();
        }

        if (tablaPerfilesClausulas.Rows.Count <= 0)
        {
            GridView_ClausulasAdicionalesContrato.DataSource = null;
        }
        else
        {
            GridView_ClausulasAdicionalesContrato.DataSource = tablaPerfilesClausulas;
        }

        GridView_ClausulasAdicionalesContrato.DataBind();
    }

    private String ObtenerDocumentosSeleccion(String documentosSeleccion, DataRow filaInfoEnvioDocumentos)
    {
        String mensaje_seleccion = "<div style=\"text-align:justify;\">";
        mensaje_seleccion += "Enviar al E-Mail <b>[CORREO_SELECCION]</b> ([NOMBRE_CONTATCTO_SELECCION] - [CARGO_CONTATCTO_SELECCION]):<br />";
        mensaje_seleccion += "[LISTA_DOCUMENTOS_SELECCION]";
        mensaje_seleccion += "</div>";

        mensaje_seleccion = mensaje_seleccion.Replace("[CORREO_SELECCION]",filaInfoEnvioDocumentos["CONT_MAIL_SELECCION"].ToString().Trim());
        mensaje_seleccion = mensaje_seleccion.Replace("[NOMBRE_CONTATCTO_SELECCION]", filaInfoEnvioDocumentos["CONT_NOM_SELECCION"].ToString().Trim());
        mensaje_seleccion = mensaje_seleccion.Replace("[CARGO_CONTATCTO_SELECCION]", filaInfoEnvioDocumentos["CONT_CARGO_SELECCION"].ToString().Trim());

        documentosSeleccion = documentosSeleccion.Replace("INFORME_SELECCION", "Informe Selección");
        documentosSeleccion = documentosSeleccion.Replace("ARCHIVOS_PRUEBAS", "Archivos Pruebas");
        documentosSeleccion = documentosSeleccion.Replace("REFERENCIA_LABORAL", "Confirmación Referencia Laboral");

        String listaDocumentosSeleccion = "<ul>";

        foreach (String documento in documentosSeleccion.Split(';'))
        {
            listaDocumentosSeleccion += "<li>" + documento + "</li>";
        }
        listaDocumentosSeleccion += "</ul>";

        mensaje_seleccion = mensaje_seleccion.Replace("[LISTA_DOCUMENTOS_SELECCION]",listaDocumentosSeleccion);

        return mensaje_seleccion;
    }

    private String ObtenerDocumentosContratacion(String documentosContratacion, DataRow filaInfoEnvioDocumentos)
    {
        String mensaje_contratacion = "<div style=\"text-align:justify;\">";
        mensaje_contratacion += "Enviar al E-Mail <b>[CORREO_CONTRATACION]</b> ([NOMBRE_CONTATCTO_CONTRATACION] - [CARGO_CONTATCTO_CONTRATACION]):<br />";
        mensaje_contratacion += "[LISTA_DOCUMENTOS_CONTRATACION]";
        mensaje_contratacion += "</div>";

        mensaje_contratacion = mensaje_contratacion.Replace("[CORREO_CONTRATACION]", filaInfoEnvioDocumentos["CONT_MAIL_CONTRATACION"].ToString().Trim());
        mensaje_contratacion = mensaje_contratacion.Replace("[NOMBRE_CONTATCTO_CONTRATACION]", filaInfoEnvioDocumentos["CONT_NOM_CONTRATACION"].ToString().Trim());
        mensaje_contratacion = mensaje_contratacion.Replace("[CARGO_CONTATCTO_CONTRATACION]", filaInfoEnvioDocumentos["CONT_CARGO_CONTRATACION"].ToString().Trim());

        documentosContratacion = documentosContratacion.Replace("ARCHIVOS_EXAMENES", "Examenes Medicos -Resultados-");
        documentosContratacion = documentosContratacion.Replace("EXAMENES", "Examenes Medicos -Autos Recomendación-");
        documentosContratacion = documentosContratacion.Replace("CONTRATO", "Contrato Laboral");
        documentosContratacion = documentosContratacion.Replace("CLAUSULAS", "Clausulas Adicionales");
        documentosContratacion = documentosContratacion.Replace("ARCHIVOS_AFILIACIONES", "Afiliaciones");

        String listaDocumentosContratacion = "<ul>";

        foreach (String documento in documentosContratacion.Split(';'))
        {
            listaDocumentosContratacion += "<li>" + documento + "</li>";
        }
        listaDocumentosContratacion += "</ul>";

        mensaje_contratacion = mensaje_contratacion.Replace("[LISTA_DOCUMENTOS_CONTRATACION]", listaDocumentosContratacion);

        return mensaje_contratacion;
    }

    private void CargarEnvioDocumentosContratacion(Decimal ID_EMPRESA)
    {
        ConfDocEntregable _confDocEntregable = new ConfDocEntregable(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaConf = _confDocEntregable.ObtenerPorEmpresa(ID_EMPRESA);

        if (tablaConf.Rows.Count <= 0)
        {
            Label_TextoEnvíoDocuemntosCliente.Text = "<b>No se ha realizado la configuración de envío de documentos al cliente.</b>";
        }
        else
        {
            DataRow filaConf = tablaConf.Rows[0];
            if (filaConf["ENTREGA_DOCUMENTOS"].ToString().Trim().ToUpper() == "FALSE")
            {
                Label_TextoEnvíoDocuemntosCliente.Text = "<b>El cliente no requiere envío de documentación de los trabajadores.</b>";
            }
            else
            {
                String mensaje = "<div style=\"text-align:justify;\">";
                mensaje += "El cliente requiere el envío de la siguiente documentación vía E-Mail:";
                mensaje += "</div>";
                mensaje += "<br />";
                mensaje += "[DOCUMENTOS_SELECCION]";
                mensaje += "[DOCUMENTOS_CONTRATACION]";

                if(DBNull.Value.Equals(filaConf["DOCUMENTOS_SELECCION"]) == true)
                {
                    mensaje = mensaje.Replace("[DOCUMENTOS_SELECCION]", "");
                }
                else
                {
                    mensaje = mensaje.Replace("[DOCUMENTOS_SELECCION]", ObtenerDocumentosSeleccion(filaConf["DOCUMENTOS_SELECCION"].ToString().Trim(), filaConf));
                }


                if(DBNull.Value.Equals(filaConf["DOCUMENTOS_CONTRATACION"]) == true)
                {
                    mensaje = mensaje.Replace("[DOCUMENTOS_CONTRATACION]", "");
                }
                else
                {
                    mensaje = mensaje.Replace("[DOCUMENTOS_CONTRATACION]", ObtenerDocumentosContratacion(filaConf["DOCUMENTOS_CONTRATACION"].ToString().Trim(), filaConf));            
                }

                Label_TextoEnvíoDocuemntosCliente.Text = mensaje;
            }
        }
    }

    private void Cargar(Decimal ID_EMPRESA)
    {
        HiddenField_ID_EMPRESA.Value = ID_EMPRESA.ToString();

        ManualServicio _manualServicio = new ManualServicio(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaVersionamiento = _manualServicio.ObtenerVersionamientoManualPorEmpresa(ID_EMPRESA);

        if (tablaVersionamiento.Rows.Count <= 0)
        {
            HiddenField_ID_VERSIONAMIENTO.Value = "";
            if (_manualServicio.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _manualServicio.MensajeError, Proceso.Error);
            }
            else
            {
                Ocultar(Acciones.Inicio);
                Mostrar(Acciones.NuevoManual);

                CargarCabeceraManualNuevo();
                
                CargarIdentificacionCliente(ID_EMPRESA);

                


                CargarContactosComerciales(ID_EMPRESA); 
                CargarUnidadNegocio(ID_EMPRESA); 
                llenarGridCobertura(ID_EMPRESA); 
                CargarAdicionales(ID_EMPRESA, ManualServicio.ListaSecciones.Comercial, GridView_AdicionalesComercial); 
                 
                CargarDatosPerfilesUnificado(ID_EMPRESA); 
                CargarCondicionesEnvioSeleccion(ID_EMPRESA); 
                CargarAdicionales(ID_EMPRESA, ManualServicio.ListaSecciones.Seleccion, GridView_AdicionalesSeleccion); 

                CargarInformacionCondicionesContratacionUnificada(ID_EMPRESA);
                CargarClausulasAdicionales(ID_EMPRESA);
                CargarEnvioDocumentosContratacion(ID_EMPRESA);
                CargarAdicionales(ID_EMPRESA, ManualServicio.ListaSecciones.Contratacion, GridView_AdicionalesContratacion);



                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "A la empresa seleccionada aún no se le ha creado el Manual, realice los cambios o actualizaciones necesarias y guarde el manual.", Proceso.Advertencia);
            }
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.CargarManual);

            CargarCabeceraManual(tablaVersionamiento.Rows[tablaVersionamiento.Rows.Count - 1]);

            CargarIdentificacionCliente(ID_EMPRESA);

            CargarModificacionesManual(ID_EMPRESA);


            CargarContactosComerciales(ID_EMPRESA);
            CargarUnidadNegocio(ID_EMPRESA);
            
            llenarGridCobertura(ID_EMPRESA);
            CargarAdicionales(ID_EMPRESA, ManualServicio.ListaSecciones.Comercial, GridView_AdicionalesComercial);

            CargarDatosPerfilesUnificado(ID_EMPRESA); 
            CargarCondicionesEnvioSeleccion(ID_EMPRESA);
            CargarAdicionales(ID_EMPRESA, ManualServicio.ListaSecciones.Seleccion, GridView_AdicionalesSeleccion);

            CargarInformacionCondicionesContratacionUnificada(ID_EMPRESA);
            CargarClausulasAdicionales(ID_EMPRESA);
            CargarEnvioDocumentosContratacion(ID_EMPRESA);
            CargarAdicionales(ID_EMPRESA, ManualServicio.ListaSecciones.Contratacion, GridView_AdicionalesContratacion);
        }
    }

    protected void GridView_RESULTADOS_BUSQUEDA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal ID_EMPRESA = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID"]);

            Cargar(ID_EMPRESA);
        }
    }
    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;

        Buscar();
    }
    protected void GridView_AdicionalesComercial_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "modificar")
        {
            ActivarFilaGrilla(GridView_AdicionalesComercial, indexSeleccionado, 2);

            HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Modificar.ToString();
            HiddenField_FILA_SELECCIONADA_GRILLA.Value = indexSeleccionado.ToString();

            HiddenField_ID_ADICIONAL.Value = GridView_AdicionalesComercial.DataKeys[indexSeleccionado].Values["ID_ADICIONAL"].ToString();


            TextBox texto_Titulo = GridView_AdicionalesComercial.Rows[indexSeleccionado].FindControl("TextBox_TituloAdicional") as TextBox;
            HiddenField_TITULO.Value = texto_Titulo.Text;

            TextBox texto_Descripcion = GridView_AdicionalesComercial.Rows[indexSeleccionado].FindControl("TextBox_DescripcionAdicional") as TextBox;
            HiddenField_DESCRIPCION.Value = texto_Descripcion.Text;

            GridView_AdicionalesComercial.Columns[0].Visible = false;
            GridView_AdicionalesComercial.Columns[1].Visible = false;

            Button_NUEVOADICIONALCOMERCIAL.Visible = false;
            Button_GUARDARADICIONALCOMERCIAL.Visible = true;
            Button_CANCELARADICIONALCOMERCIAL.Visible = true;
        }
        else
        {
            if (e.CommandName == "eliminar")
            {
                DataTable tablaDesdeGrilla = obtenerTablaDeGrillaAdicionales(GridView_AdicionalesComercial);

                tablaDesdeGrilla.Rows.RemoveAt(indexSeleccionado);

                CargarGrillaAdicionalesDesdeTabla(tablaDesdeGrilla, GridView_AdicionalesComercial);

                inhabilitarFilasGrilla(GridView_AdicionalesComercial, 2);

                GridView_AdicionalesComercial.Columns[0].Visible = true;
                GridView_AdicionalesComercial.Columns[1].Visible = true;

                HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_FILA_SELECCIONADA_GRILLA.Value = null;
                HiddenField_ID_ADICIONAL.Value = "";
                HiddenField_TITULO.Value = "";
                HiddenField_DESCRIPCION.Value = "";

                Button_NUEVOADICIONALCOMERCIAL.Visible = true;
                Button_GUARDARADICIONALCOMERCIAL.Visible = false;
                Button_CANCELARADICIONALCOMERCIAL.Visible = false;
            }
        }
    }

    private DataTable ConfigurarTablaAdicionales()
    {
        DataTable tablaResultado = new DataTable();
        tablaResultado.Columns.Add("ID_ADICIONAL");
        tablaResultado.Columns.Add("ID_EMPRESA");
        tablaResultado.Columns.Add("TITULO");
        tablaResultado.Columns.Add("DESCRIPCION");
        return tablaResultado;
    }

    private DataTable obtenerTablaDeGrillaAdicionales(GridView grilla)
    {
        DataTable tablaResultado = ConfigurarTablaAdicionales();
        DataRow filaTablaResultado;

        for (int i = 0; i < grilla.Rows.Count; i++)
        {
            GridViewRow filaGrilla = grilla.Rows[i];

            Decimal ID_ADICIONAL = Convert.ToDecimal(grilla.DataKeys[i].Values["ID_ADICIONAL"]);

            Decimal ID_EMPRESA = Convert.ToDecimal(grilla.DataKeys[i].Values["ID_EMPRESA"]);

            TextBox texto_Titulo = filaGrilla.FindControl("TextBox_TituloAdicional") as TextBox;
            String TITULO = texto_Titulo.Text;

            TextBox texto_Descripcion = filaGrilla.FindControl("TextBox_DescripcionAdicional") as TextBox;
            String DESCRIPCION = texto_Descripcion.Text;

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_ADICIONAL"] = ID_ADICIONAL;
            filaTablaResultado["ID_EMPRESA"] = ID_EMPRESA;
            filaTablaResultado["TITULO"] = TITULO;
            filaTablaResultado["DESCRIPCION"] = DESCRIPCION;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }

    private void ActivarFilaGrilla(GridView grilla, int fila, int colInicio)
    {
        for (int i = 0; i < grilla.Columns.Count; i++)
        {
            grilla.Rows[fila].Cells[i].Enabled = true;
        }
    }

    protected void Button_NUEVOADICIONALCOMERCIAL_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerTablaDeGrillaAdicionales(GridView_AdicionalesComercial);
        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_ADICIONAL"] = 0;
        filaNueva["ID_EMPRESA"] = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        filaNueva["TITULO"] = "";
        filaNueva["DESCRIPCION"] = "";

        tablaDesdeGrilla.Rows.Add(filaNueva);

        CargarGrillaAdicionalesDesdeTabla(tablaDesdeGrilla, GridView_AdicionalesComercial);

        inhabilitarFilasGrilla(GridView_AdicionalesComercial, 2);
        ActivarFilaGrilla(GridView_AdicionalesComercial, GridView_AdicionalesComercial.Rows.Count - 1, 2);

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_SELECCIONADA_GRILLA.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();
        HiddenField_ID_ADICIONAL.Value = null;
        HiddenField_TITULO.Value = null;
        HiddenField_DESCRIPCION.Value = null;

        GridView_AdicionalesComercial.Columns[0].Visible = false;
        GridView_AdicionalesComercial.Columns[1].Visible = false;

        Button_NUEVOADICIONALCOMERCIAL.Visible = false;
        Button_GUARDARADICIONALCOMERCIAL.Visible = true;
        Button_CANCELARADICIONALCOMERCIAL.Visible = true;
    }

    private void inhabilitarFilaGrilla(GridView grilla, int fila, int colInicio)
    {
        for (int j = colInicio; j < grilla.Columns.Count; j++)
        {
            grilla.Rows[fila].Cells[j].Enabled = false;
        }
    }

    protected void Button_GUARDARADICIONALCOMERCIAL_Click(object sender, EventArgs e)
    {
        int FILA_SELECCIONADA = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA.Value);

        inhabilitarFilaGrilla(GridView_AdicionalesComercial, FILA_SELECCIONADA, 2);

        GridView_AdicionalesComercial.Columns[0].Visible = true;
        GridView_AdicionalesComercial.Columns[1].Visible = true;

        Button_GUARDARADICIONALCOMERCIAL.Visible = false;
        Button_CANCELARADICIONALCOMERCIAL.Visible = false;
        Button_NUEVOADICIONALCOMERCIAL.Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
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

    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_FORM_BOTONES.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_Imprimir.Visible = false;

                Panel_RESULTADOS_GRID.Visible = false;

                Panel_ContenedorManual.Visible = false;
                Panel_Cabacera.Visible = false;
                Panel_IdentificacionCliente.Visible = false;
                Panel_ControlModificaciones.Visible = false;

                Panel_GestionComercial.Visible = false;
                Panel_AdicionalesComercial.Visible = false;
                Panel_BotonesAdicionalesComercial.Visible = false;
                Button_NUEVOADICIONALCOMERCIAL.Visible = false;
                Button_GUARDARADICIONALCOMERCIAL.Visible = false;
                Button_CANCELARADICIONALCOMERCIAL.Visible = false;
                GridView_AdicionalesComercial.Columns[0].Visible = false;
                GridView_AdicionalesComercial.Columns[1].Visible = false;

                Panel_Seleccion.Visible = false;
                Panel_AdicionalesSeleccion.Visible = false;
                Panel_BotonesAdicionalesSeleccion.Visible = false;
                Button_NUEVOADICIONALSELECCION.Visible = false;
                Button_GUARDARADICIONALSELECCION.Visible = false;
                Button_CANCELARADICIONALSELECCION.Visible = false;
                GridView_AdicionalesSeleccion.Columns[0].Visible = false;
                GridView_AdicionalesSeleccion.Columns[1].Visible = false;

                Panel_Contratacion.Visible = false;
                Panel_AdicionalesContratacion.Visible = false;
                Panel_BotonesAdicionalesContratacion.Visible = false;
                Button_NUEVOADICIONALCONTRATACION.Visible = false;
                Button_GUARDARADICIONALCONTRATACION.Visible = false;
                Button_CANCELARADICIONALCONTRATACION.Visible = false;
                GridView_AdicionalesContratacion.Columns[0].Visible = false;
                GridView_AdicionalesContratacion.Columns[1].Visible = false;



                Panel_BOTONES_ACCION_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                Button_IMPRIMIR_1.Visible = false;

                break;
            case Acciones.ModificarManual:
                Button_MODIFICAR.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_Imprimir.Visible = false;

                Button_NUEVOADICIONALCOMERCIAL.Visible = false;
                Button_GUARDARADICIONALCOMERCIAL.Visible = false;
                Button_CANCELARADICIONALCOMERCIAL.Visible = false;

                
                Button_NUEVOADICIONALSELECCION.Visible = false;
                Button_GUARDARADICIONALSELECCION.Visible = false;
                Button_CANCELARADICIONALSELECCION.Visible = false;

                Button_NUEVOADICIONALCONTRATACION.Visible = false;
                Button_GUARDARADICIONALCONTRATACION.Visible = false;
                Button_CANCELARADICIONALCONTRATACION.Visible = false;



                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                Button_IMPRIMIR_1.Visible = false;
                break;
        }
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_FORM_BOTONES.Visible = true;
                break;
            case Acciones.NuevoManual:
                Panel_FORM_BOTONES.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_ContenedorManual.Visible = true;
                Panel_Cabacera.Visible = true;
                Panel_IdentificacionCliente.Visible = true;

                Panel_GestionComercial.Visible = true;
                Panel_AdicionalesComercial.Visible = true;
                Panel_BotonesAdicionalesComercial.Visible = true;
                Button_NUEVOADICIONALCOMERCIAL.Visible = true;
                GridView_AdicionalesComercial.Columns[0].Visible = true;
                GridView_AdicionalesComercial.Columns[1].Visible = true;

                GVInfoBasica.Visible = false;

                Panel_Seleccion.Visible = true;
                Panel_AdicionalesSeleccion.Visible = true;
                Panel_BotonesAdicionalesSeleccion.Visible = true;
                Button_NUEVOADICIONALSELECCION.Visible = true;
                GridView_AdicionalesSeleccion.Columns[0].Visible = true;
                GridView_AdicionalesSeleccion.Columns[1].Visible = true;

                Panel_Contratacion.Visible = true;
                Panel_AdicionalesContratacion.Visible = true;
                Panel_BotonesAdicionalesContratacion.Visible = true;
                Button_NUEVOADICIONALCONTRATACION.Visible = true;
                GridView_AdicionalesContratacion.Columns[0].Visible = true;
                GridView_AdicionalesContratacion.Columns[1].Visible = true;



                Panel_BOTONES_ACCION_1.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
                break;
            case Acciones.CargarManual:
                Panel_FORM_BOTONES.Visible = true;
                Button_MODIFICAR.Visible = true;
                Button_Imprimir.Visible = true;

                Panel_ContenedorManual.Visible = true;
                Panel_Cabacera.Visible = true;
                Panel_IdentificacionCliente.Visible = true;

                Panel_ControlModificaciones.Visible = true;

                Panel_GestionComercial.Visible = true;
                Panel_AdicionalesComercial.Visible = true;

                Panel_Seleccion.Visible = true;
                Panel_AdicionalesSeleccion.Visible = true;

                Panel_Contratacion.Visible = true;
                Panel_AdicionalesContratacion.Visible = true;



                Panel_BOTONES_ACCION_1.Visible = true;
                Button_MODIFICAR_1.Visible = true;
                Button_IMPRIMIR_1.Visible = true;
                break;
            case Acciones.ModificarManual:
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_AdicionalesComercial.Visible = true;
                Panel_BotonesAdicionalesComercial.Visible = true;
                Button_NUEVOADICIONALCOMERCIAL.Visible = true;
                GridView_AdicionalesComercial.Columns[0].Visible = true;
                GridView_AdicionalesComercial.Columns[1].Visible = true;

                Panel_AdicionalesSeleccion.Visible = true;
                Panel_BotonesAdicionalesSeleccion.Visible = true;
                Button_NUEVOADICIONALSELECCION.Visible = true;
                GridView_AdicionalesSeleccion.Columns[0].Visible = true;
                GridView_AdicionalesSeleccion.Columns[1].Visible = true;

                Panel_AdicionalesContratacion.Visible = true;
                Panel_BotonesAdicionalesContratacion.Visible = true;
                Button_NUEVOADICIONALCONTRATACION.Visible = true;
                GridView_AdicionalesContratacion.Columns[0].Visible = true;
                GridView_AdicionalesContratacion.Columns[1].Visible = true;



                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
                break;
        }
    }

    private void configurarCaracteresAceptadosBusqueda(Boolean letras, Boolean numeros)
    {
        FilteredTextBoxExtender_TextBox_BUSCAR_Numbers.Enabled = numeros;
        FilteredTextBoxExtender_TextBox_BUSCAR_Letras.Enabled = letras;
    }

    private void cargar_DropDownList_BUSCAR()
    {
        DropDownList_BUSCAR.Items.Clear();

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_BUSCAR.Items.Add(item);
        item = new ListItem("Código de Cliente", "COD_EMPRESA");
        DropDownList_BUSCAR.Items.Add(item);
        item = new ListItem("Razón social", "RAZ_SOCIAL");
        DropDownList_BUSCAR.Items.Add(item);
        item = new ListItem("NIT", "NIT_EMPRESA");
        DropDownList_BUSCAR.Items.Add(item);
        DropDownList_BUSCAR.DataBind();
    }

    private void iniciar_seccion_de_busqueda()
    {
        TextBox_BUSCAR.Text = "";
        cargar_DropDownList_BUSCAR();
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                configurarCaracteresAceptadosBusqueda(true, true);

                HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "NINGUNA";
                HiddenField_FILTRO_DROP.Value = String.Empty;
                HiddenField_FILTRO_DATO.Value = String.Empty;

                iniciar_seccion_de_busqueda();
                break;
        }
    }

    private void Iniciar()
    {
        Page.Header.Title = "MANUAL DE SERVICIO";
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);
        try
        {
            Decimal EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"].ToString());
            String _Empresa = Convert.ToString(EMPRESA);
            if (_Empresa != "0")
            {
                HiddenField_FILTRO_DATO.Value = _Empresa;
                Panel_MENSAJES.Visible = false;
                Cargar(EMPRESA);
            }
            else
            {
                Configurar();
                Ocultar(Acciones.Inicio);
                Mostrar(Acciones.Inicio);
                Cargar(Acciones.Inicio);
            }
        }
        catch
        {
            Configurar();
            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);
        }     
    }

    protected void Button_CANCELARADICIONALCOMERCIAL_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = obtenerTablaDeGrillaAdicionales(GridView_AdicionalesComercial);

        if (HiddenField_ACCION_GRILLA.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }
        else
        {
            if (HiddenField_ACCION_GRILLA.Value == AccionesGrilla.Modificar.ToString())
            {
                DataRow filaGrilla = tablaGrilla.Rows[Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA.Value)];

                filaGrilla["ID_ADICIONAL"] = Convert.ToDecimal(HiddenField_ID_ADICIONAL.Value);
                filaGrilla["ID_EMPRESA"] = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
                filaGrilla["TITULO"] = HiddenField_TITULO.Value;
                filaGrilla["DESCRIPCION"] = HiddenField_DESCRIPCION.Value;
            }
        }

        CargarGrillaAdicionalesDesdeTabla(tablaGrilla, GridView_AdicionalesComercial);

        inhabilitarFilasGrilla(GridView_AdicionalesComercial, 2);

        GridView_AdicionalesComercial.Columns[0].Visible = true;
        GridView_AdicionalesComercial.Columns[1].Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();

        Button_NUEVOADICIONALCOMERCIAL.Visible = true;
        Button_GUARDARADICIONALCOMERCIAL.Visible = false;
        Button_CANCELARADICIONALCOMERCIAL.Visible = false;
    }

    protected void Button_NUEVOADICIONALSELECCION_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerTablaDeGrillaAdicionales(GridView_AdicionalesSeleccion);
        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_ADICIONAL"] = 0;
        filaNueva["ID_EMPRESA"] = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        filaNueva["TITULO"] = "";
        filaNueva["DESCRIPCION"] = "";

        tablaDesdeGrilla.Rows.Add(filaNueva);

        CargarGrillaAdicionalesDesdeTabla(tablaDesdeGrilla, GridView_AdicionalesSeleccion);

        inhabilitarFilasGrilla(GridView_AdicionalesSeleccion, 2);
        ActivarFilaGrilla(GridView_AdicionalesSeleccion, GridView_AdicionalesSeleccion.Rows.Count - 1, 2);

        HiddenField_ACCION_GRILLA_SELECCION.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_SELECCIONADA_GRILLA_SELECCION.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();
        HiddenField_ID_ADICIONAL_SELECCION.Value = null;
        HiddenField_TITULO_SELECCION.Value = null;
        HiddenField_DESCRIPCION_SELECCION.Value = null;

        GridView_AdicionalesSeleccion.Columns[0].Visible = false;
        GridView_AdicionalesSeleccion.Columns[1].Visible = false;

        Button_NUEVOADICIONALSELECCION.Visible = false;
        Button_GUARDARADICIONALSELECCION.Visible = true;
        Button_CANCELARADICIONALSELECCION.Visible = true;
    }
    protected void Button_GUARDARADICIONALSELECCION_Click(object sender, EventArgs e)
    {
        int FILA_SELECCIONADA = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_SELECCION.Value);

        inhabilitarFilaGrilla(GridView_AdicionalesSeleccion, FILA_SELECCIONADA, 2);

        GridView_AdicionalesSeleccion.Columns[0].Visible = true;
        GridView_AdicionalesSeleccion.Columns[1].Visible = true;

        Button_GUARDARADICIONALSELECCION.Visible = false;
        Button_CANCELARADICIONALSELECCION.Visible = false;
        Button_NUEVOADICIONALSELECCION.Visible = true;

        HiddenField_ACCION_GRILLA_SELECCION.Value = AccionesGrilla.Ninguna.ToString();
    }
    protected void Button_CANCELARADICIONALSELECCION_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = obtenerTablaDeGrillaAdicionales(GridView_AdicionalesSeleccion);

        if (HiddenField_ACCION_GRILLA_SELECCION.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }
        else
        {
            if (HiddenField_ACCION_GRILLA_SELECCION.Value == AccionesGrilla.Modificar.ToString())
            {
                DataRow filaGrilla = tablaGrilla.Rows[Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_SELECCION.Value)];

                filaGrilla["ID_ADICIONAL"] = Convert.ToDecimal(HiddenField_ID_ADICIONAL_SELECCION.Value);
                filaGrilla["ID_EMPRESA"] = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
                filaGrilla["TITULO"] = HiddenField_TITULO_SELECCION.Value;
                filaGrilla["DESCRIPCION"] = HiddenField_DESCRIPCION_SELECCION.Value;
            }
        }

        CargarGrillaAdicionalesDesdeTabla(tablaGrilla, GridView_AdicionalesSeleccion);

        inhabilitarFilasGrilla(GridView_AdicionalesSeleccion, 2);

        GridView_AdicionalesSeleccion.Columns[0].Visible = true;
        GridView_AdicionalesSeleccion.Columns[1].Visible = true;

        HiddenField_ACCION_GRILLA_SELECCION.Value = AccionesGrilla.Ninguna.ToString();

        Button_NUEVOADICIONALSELECCION.Visible = true;
        Button_GUARDARADICIONALSELECCION.Visible = false;
        Button_CANCELARADICIONALSELECCION.Visible = false;
    }
    protected void GridView_AdicionalesSeleccion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "modificar")
        {
            ActivarFilaGrilla(GridView_AdicionalesSeleccion, indexSeleccionado, 2);

            HiddenField_ACCION_GRILLA_SELECCION.Value = AccionesGrilla.Modificar.ToString();
            HiddenField_FILA_SELECCIONADA_GRILLA_SELECCION.Value = indexSeleccionado.ToString();

            HiddenField_ID_ADICIONAL_SELECCION.Value = GridView_AdicionalesSeleccion.DataKeys[indexSeleccionado].Values["ID_ADICIONAL"].ToString();


            TextBox texto_Titulo = GridView_AdicionalesSeleccion.Rows[indexSeleccionado].FindControl("TextBox_TituloAdicional") as TextBox;
            HiddenField_TITULO_SELECCION.Value = texto_Titulo.Text;

            TextBox texto_Descripcion = GridView_AdicionalesSeleccion.Rows[indexSeleccionado].FindControl("TextBox_DescripcionAdicional") as TextBox;
            HiddenField_DESCRIPCION_SELECCION.Value = texto_Descripcion.Text;

            GridView_AdicionalesSeleccion.Columns[0].Visible = false;
            GridView_AdicionalesSeleccion.Columns[1].Visible = false;

            Button_NUEVOADICIONALSELECCION.Visible = false;
            Button_GUARDARADICIONALSELECCION.Visible = true;
            Button_CANCELARADICIONALSELECCION.Visible = true;
        }
        else
        {
            if (e.CommandName == "eliminar")
            {
                DataTable tablaDesdeGrilla = obtenerTablaDeGrillaAdicionales(GridView_AdicionalesSeleccion);

                tablaDesdeGrilla.Rows.RemoveAt(indexSeleccionado);

                CargarGrillaAdicionalesDesdeTabla(tablaDesdeGrilla, GridView_AdicionalesSeleccion);

                inhabilitarFilasGrilla(GridView_AdicionalesSeleccion, 2);

                GridView_AdicionalesSeleccion.Columns[0].Visible = true;
                GridView_AdicionalesSeleccion.Columns[1].Visible = true;

                HiddenField_ACCION_GRILLA_SELECCION.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_FILA_SELECCIONADA_GRILLA_SELECCION.Value = null;
                HiddenField_ID_ADICIONAL_SELECCION.Value = "";
                HiddenField_TITULO_SELECCION.Value = "";
                HiddenField_DESCRIPCION_SELECCION.Value = "";

                Button_NUEVOADICIONALSELECCION.Visible = true;
                Button_GUARDARADICIONALSELECCION.Visible = false;
                Button_CANCELARADICIONALSELECCION.Visible = false;
            }
        }
    }
    protected void Button_NUEVOADICIONALCONTRATACION_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerTablaDeGrillaAdicionales(GridView_AdicionalesContratacion);
        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_ADICIONAL"] = 0;
        filaNueva["ID_EMPRESA"] = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        filaNueva["TITULO"] = "";
        filaNueva["DESCRIPCION"] = "";

        tablaDesdeGrilla.Rows.Add(filaNueva);

        CargarGrillaAdicionalesDesdeTabla(tablaDesdeGrilla, GridView_AdicionalesContratacion);

        inhabilitarFilasGrilla(GridView_AdicionalesContratacion, 2);
        ActivarFilaGrilla(GridView_AdicionalesContratacion, GridView_AdicionalesContratacion.Rows.Count - 1, 2);

        HiddenField_ACCION_GRILLA_CONTRATACION.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_SELECCIONADA_GRILLA_CONTRATACION.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();
        HiddenField_ID_ADICIONAL_CONTRATACION.Value = null;
        HiddenField_TITULO_CONTRATACION.Value = null;
        HiddenField_DESCRIPCION_CONTRATACION.Value = null;

        GridView_AdicionalesContratacion.Columns[0].Visible = false;
        GridView_AdicionalesContratacion.Columns[1].Visible = false;

        Button_NUEVOADICIONALCONTRATACION.Visible = false;
        Button_GUARDARADICIONALCONTRATACION.Visible = true;
        Button_CANCELARADICIONALCONTRATACION.Visible = true;
    }
    protected void Button_GUARDARADICIONALCONTRATACION_Click(object sender, EventArgs e)
    {
        int FILA_SELECCIONADA = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_CONTRATACION.Value);

        inhabilitarFilaGrilla(GridView_AdicionalesContratacion, FILA_SELECCIONADA, 2);

        GridView_AdicionalesContratacion.Columns[0].Visible = true;
        GridView_AdicionalesContratacion.Columns[1].Visible = true;

        Button_GUARDARADICIONALCONTRATACION.Visible = false;
        Button_CANCELARADICIONALCONTRATACION.Visible = false;
        Button_NUEVOADICIONALCONTRATACION.Visible = true;

        HiddenField_ACCION_GRILLA_CONTRATACION.Value = AccionesGrilla.Ninguna.ToString();
    }
    protected void Button_CANCELARADICIONALCONTRATACION_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = obtenerTablaDeGrillaAdicionales(GridView_AdicionalesContratacion);

        if (HiddenField_ACCION_GRILLA_CONTRATACION.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }
        else
        {
            if (HiddenField_ACCION_GRILLA_CONTRATACION.Value == AccionesGrilla.Modificar.ToString())
            {
                DataRow filaGrilla = tablaGrilla.Rows[Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_CONTRATACION.Value)];

                filaGrilla["ID_ADICIONAL"] = Convert.ToDecimal(HiddenField_ID_ADICIONAL_CONTRATACION.Value);
                filaGrilla["ID_EMPRESA"] = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
                filaGrilla["TITULO"] = HiddenField_TITULO_CONTRATACION.Value;
                filaGrilla["DESCRIPCION"] = HiddenField_DESCRIPCION_CONTRATACION.Value;
            }
        }

        CargarGrillaAdicionalesDesdeTabla(tablaGrilla, GridView_AdicionalesContratacion);

        inhabilitarFilasGrilla(GridView_AdicionalesContratacion, 2);

        GridView_AdicionalesContratacion.Columns[0].Visible = true;
        GridView_AdicionalesContratacion.Columns[1].Visible = true;

        HiddenField_ACCION_GRILLA_CONTRATACION.Value = AccionesGrilla.Ninguna.ToString();

        Button_NUEVOADICIONALCONTRATACION.Visible = true;
        Button_GUARDARADICIONALCONTRATACION.Visible = false;
        Button_CANCELARADICIONALCONTRATACION.Visible = false;
    }
    protected void GridView_AdicionalesContratacion_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "modificar")
        {
            ActivarFilaGrilla(GridView_AdicionalesContratacion, indexSeleccionado, 2);

            HiddenField_ACCION_GRILLA_CONTRATACION.Value = AccionesGrilla.Modificar.ToString();
            HiddenField_FILA_SELECCIONADA_GRILLA_CONTRATACION.Value = indexSeleccionado.ToString();

            HiddenField_ID_ADICIONAL_CONTRATACION.Value = GridView_AdicionalesContratacion.DataKeys[indexSeleccionado].Values["ID_ADICIONAL"].ToString();


            TextBox texto_Titulo = GridView_AdicionalesContratacion.Rows[indexSeleccionado].FindControl("TextBox_TituloAdicional") as TextBox;
            HiddenField_TITULO_CONTRATACION.Value = texto_Titulo.Text;

            TextBox texto_Descripcion = GridView_AdicionalesContratacion.Rows[indexSeleccionado].FindControl("TextBox_DescripcionAdicional") as TextBox;
            HiddenField_DESCRIPCION_CONTRATACION.Value = texto_Descripcion.Text;

            GridView_AdicionalesContratacion.Columns[0].Visible = false;
            GridView_AdicionalesContratacion.Columns[1].Visible = false;

            Button_NUEVOADICIONALCONTRATACION.Visible = false;
            Button_GUARDARADICIONALCONTRATACION.Visible = true;
            Button_CANCELARADICIONALCONTRATACION.Visible = true;
        }
        else
        {
            if (e.CommandName == "eliminar")
            {
                DataTable tablaDesdeGrilla = obtenerTablaDeGrillaAdicionales(GridView_AdicionalesContratacion);

                tablaDesdeGrilla.Rows.RemoveAt(indexSeleccionado);

                CargarGrillaAdicionalesDesdeTabla(tablaDesdeGrilla, GridView_AdicionalesContratacion);

                inhabilitarFilasGrilla(GridView_AdicionalesContratacion, 2);

                GridView_AdicionalesContratacion.Columns[0].Visible = true;
                GridView_AdicionalesContratacion.Columns[1].Visible = true;

                HiddenField_ACCION_GRILLA_CONTRATACION.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_FILA_SELECCIONADA_GRILLA_CONTRATACION.Value = null;
                HiddenField_ID_ADICIONAL_CONTRATACION.Value = "";
                HiddenField_TITULO_CONTRATACION.Value = "";
                HiddenField_DESCRIPCION_CONTRATACION.Value = "";

                Button_NUEVOADICIONALCONTRATACION.Visible = true;
                Button_GUARDARADICIONALCONTRATACION.Visible = false;
                Button_CANCELARADICIONALCONTRATACION.Visible = false;
            }
        }
        if (e.CommandName == "Salvar")
        {
        }
    }
    protected void Button_Imprimir_Click(object sender, EventArgs e)
    {
        Decimal ID_VERSIONAMIENTO = Convert.ToDecimal(HiddenField_ID_VERSIONAMIENTO.Value);

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["version"] = ID_VERSIONAMIENTO.ToString();

        Response.Redirect("~/Operaciones/visorManualServicio.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }
    protected void btnEditar_Click(object sender, EventArgs e)
    {
    }
}