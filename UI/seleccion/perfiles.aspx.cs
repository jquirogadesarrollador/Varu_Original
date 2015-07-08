using System.Web.UI.WebControls;
using Brainsbits.LLB.maestras;
using TSHAK.Components;
using System;
using Brainsbits.LLB.seleccion;
using System.Data;
using System.Collections.Generic;
using Brainsbits.LLB;
using System.Web;
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

    private System.Drawing.Color colorAmarillo = System.Drawing.ColorTranslator.FromHtml("#FFF200");
    private System.Drawing.Color colorVerde = System.Drawing.ColorTranslator.FromHtml("#50CE04");
    private System.Drawing.Color colorGris = System.Drawing.ColorTranslator.FromHtml("#cccccc");

    private enum Acciones
    {
        Inicio = 0,
        CargarPerfil,
        NuevoPerfil,
        Modificar, 
        SeleccionDeCargoSinPerfil
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

    private void configurar_paneles_popup(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_mensaje.CssClass = "panel_popup";
    }

    private void Configurar()
    {
        configurar_paneles_popup(Panel_FONDO_MENSAJE, Panel_MENSAJES);
        configurar_paneles_popup(Panel_FONDO_MENSAJE_DOCUMENTOS, Panel_MENSAJES_DOCUMENTOS);
        configurar_paneles_popup(Panel_FONDO_MENSAJE_PRUEBAS, Panel_MENSAJES_PRUEBAS);
    }

    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = false;

                Panel_FORM_BOTONES.Visible = false;
                Button_NUEVO.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_VOLVER.Visible = false;

                Panel_RESULTADOS_GRID.Visible = false;

                Panel_FORMULARIO.Visible = false;
                Panel_CONTROL_REGISTRO.Visible = false;
                Panel_IDENTIFICADOR.Visible = false;
                Panel_OBSERVACIONES_ESPECIALES.Visible = false;

                Panel_documentos.Visible = false;
                Panel_ADICIONAR_DOCUMENTO.Visible = false;
                Panel_pruebas.Visible = false;
                Panel_ADICIONAR_PRUEBAS.Visible = false;

                Panel_NivelDificultadRequerimientos.Visible = false;

                Panel_CONFIGURACION_REFERENCIA.Visible = false;

                Panel_CONFIGURACION_ENTRVISTA.Visible = false;

                Panel_TIPO_ENTREVISTA.Visible = false;
                Panel_COMPETENCIAS.Visible = false;
                Panel_InformacionAssesmentSeleccionado.Visible = false;

                Panel_FORM_BOTONES_ABAJO.Visible = false;
                Button_NUEVO_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                Button_VOLVER_1.Visible = false;

                GridView_NOMBRE_DOCUMENTO.Columns[0].Visible = false;
                GridView_NOMBRE_PRUEBA.Columns[0].Visible = false;
                break;
            case Acciones.Modificar:
                Panel_CONTROL_REGISTRO.Visible = false;
                Button_NUEVO.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_VOLVER.Visible = false;

                Button_NUEVO_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                Button_VOLVER_1.Visible = false;
                break;
            case Acciones.SeleccionDeCargoSinPerfil:
                Panel_COMPETENCIAS.Visible = false;
                Panel_InformacionAssesmentSeleccionado.Visible = false;
                break;
        }
    }

    private void Desactivar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                TextBox_FCH_CRE.Enabled = false;
                TextBox_FCH_MOD.Enabled = false;
                TextBox_HOR_CRE.Enabled = false;
                TextBox_HOR_MOD.Enabled = false;
                TextBox_USU_CRE.Enabled = false;
                TextBox_USU_MOD.Enabled = false;

                DropDownList_ocupacion.Enabled = false;
                TextBox_EDAD_MINIMA.Enabled = false;
                TextBox_EDAD_MAXIMA.Enabled = false;
                DropDownList_Experiencia.Enabled = false;
                DropDownList_SEXO.Enabled = false;
                DropDownList_NIV_ACADEMICO.Enabled = false;
                TextBox_OBSERVACIONES_ESPECIALES.Enabled = false;
                DropDownList_NOMBRE_DOCUMENTO.Enabled = false;
                DropDownList_NOMBRE_PRUEBA.Enabled = false;
                TextBox_FincionesCargoSeleccionado.Enabled = false;

                RadioButtonList_NivelDificultadReq.Enabled = false;

                DropDownList_TipoConfirmacionReferencia.Enabled = false;

                DropDownList_AssesmentCenter.Enabled = false;

                CheckBox_TipoBasica.Enabled = false;
                CheckBox_TipoCompetencias.Enabled = false;
                break;
        }
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

    private void cargar_GridView_RESULTADOS_BUSQUEDA()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

        HiddenField_ID_EMPRESA.Value = ID_EMPRESA.ToString();

        perfil _perfil = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaPerfiles = _perfil.ObtenerVenDPerfilesConOcupacionPorIdEmpresa(ID_EMPRESA);


        if (tablaPerfiles.Rows.Count > 0)
        {
            GridView_PERFILES.PageIndex = Convert.ToInt32(HiddenField_PAGINA_GRID.Value);

            GridView_PERFILES.DataSource = tablaPerfiles;
            GridView_PERFILES.DataBind();
        }
        else
        {
            if (_perfil.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _perfil.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron Registros de perfiles.", Proceso.Advertencia);
            }

            Panel_RESULTADOS_GRID.Visible = false;
            GridView_PERFILES.DataSource = null;
            GridView_PERFILES.DataBind();
        }
    }

    private void cargar_DropDownList_NOMBRE_DOCUMENTO()
    {
        DropDownList_NOMBRE_DOCUMENTO.Items.Clear();

        documento _documento = new documento(Session["idEmpresa"].ToString());
        DataTable tablaDocumento = _documento.ObtenerTodosLosDocumentos();

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_NOMBRE_DOCUMENTO.Items.Add(item);

        foreach (DataRow fila in tablaDocumento.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_SEL_REG_DOCUMENTOS"].ToString());
            DropDownList_NOMBRE_DOCUMENTO.Items.Add(item);
        }
        DropDownList_NOMBRE_DOCUMENTO.DataBind();
    }

    private void cargar_DropDownList_PRUEBA()
    {
        this.DropDownList_NOMBRE_PRUEBA.Items.Clear();

        categoriaPruebas _categoriaPruebas = new categoriaPruebas(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaDocumento = _categoriaPruebas.ObtenerTodosLasPruebas();

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_NOMBRE_PRUEBA.Items.Add(item);

        foreach (DataRow fila in tablaDocumento.Rows)
        {
            item = new ListItem(fila["NOM_PRUEBA"].ToString(), fila["ID_PRUEBA"].ToString());
            DropDownList_NOMBRE_PRUEBA.Items.Add(item);
        }
        DropDownList_NOMBRE_PRUEBA.DataBind();
    }

    private void configurarCaracteresAceptadosBusqueda(Boolean letras, Boolean numeros)
    {
        FilteredTextBoxExtender_TextBox_BUSCAR_Numbers.Enabled = numeros;
        FilteredTextBoxExtender_TextBox_BUSCAR_Letras.Enabled = letras;
    }

    private void cargar_DropDownList_BUSCAR()
    {
        DropDownList_BUSCAR.Items.Clear();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_BUSCAR.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("Nombre Ocupación", "NOM_OCUPACION");
        DropDownList_BUSCAR.Items.Add(item);

        DropDownList_BUSCAR.DataBind();
    }

    private void iniciar_seccion_de_busqueda()
    {
        TextBox_BUSCAR.Text = "";
        cargar_DropDownList_BUSCAR();
    }

    private void cargar_DropDownList_TipoConfirmacionReferencia()
    {
        referencia _ref = new referencia(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCategoriasReferencias = _ref.ObtenerCategoriasActivas();

        DropDownList_TipoConfirmacionReferencia.Items.Clear();

        DropDownList_TipoConfirmacionReferencia.Items.Add(new ListItem("Seleccione...",""));

        if (tablaCategoriasReferencias.Rows.Count <= 0)
        {
            if (_ref.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _ref.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información sobre Confirmación de Referencias, El Usuario encargado debe realizar la Adminitración de Confirmación de Referencias (Categorías y preguntas).", Proceso.Advertencia);
            }
        }
        else
        {
            foreach (DataRow filaTabla in tablaCategoriasReferencias.Rows)
            {
                DropDownList_TipoConfirmacionReferencia.Items.Add(new ListItem(filaTabla["NOMBRE_CAT"].ToString().Trim(), filaTabla["ID_CATEGORIA"].ToString()));        
            }
        }

        DropDownList_TipoConfirmacionReferencia.DataBind();
    }

    private DataTable ObtenerDocumentosBasicosEnGrilla(GridView grilla)
    {
        DataTable tablaDocumentos = obtenerTablaDeGrillaDocumentos();


        Decimal[] idsDocumentos = { 1, 2, 3, 4, 6, 14, 16 };
        String [] nombresDocumentos = {"Documento de Identidad", "Situación militar definida","Antecedentes Disciplinarios - Procuraduria","Certificado judicial - Policia","Referencias Personales","Hoja de vida","Documento protección de datos"};

        Boolean documentoYaSeleccionado = false;
        for (int i = 0; i < idsDocumentos.Length; i++)
        {
            documentoYaSeleccionado = false;

            for (int j = 0; j < grilla.Rows.Count; j++)
            {
                Decimal idDocumentoGrilla = Convert.ToDecimal(grilla.DataKeys[j].Values["Código Documento"]);

                if (idDocumentoGrilla == idsDocumentos[i])
                {
                    documentoYaSeleccionado = true;
                    break;
                }
            }

            if (documentoYaSeleccionado == false)
            { 
                DataRow fila_temp = tablaDocumentos.NewRow();

                fila_temp["Código Documento"] = idsDocumentos[i];
                fila_temp["Documento"] = nombresDocumentos[i];

                tablaDocumentos.Rows.Add(fila_temp);
            }
        }

        return tablaDocumentos;
    }

    private DataTable ObtenerPruebasBasicasEnGrilla(GridView grilla)
    {
        DataTable tablaPruebas = obtenerTablaDeGrillaPruebas();


        Decimal[] idsPruebas = { 29 };
        String[] nombresPruebas = { "WARTEGG" };

        Boolean pruebaYaSeleccionada = false;
        for (int i = 0; i < idsPruebas.Length; i++)
        {
            pruebaYaSeleccionada = false;

            for (int j = 0; j < grilla.Rows.Count; j++)
            {
                Decimal idPruebaGrilla = Convert.ToDecimal(grilla.DataKeys[j].Values["Código Prueba"]);

                if (idPruebaGrilla == idsPruebas[i])
                {
                    pruebaYaSeleccionada = true;
                    break;
                }
            }

            if (pruebaYaSeleccionada == false)
            {
                DataRow fila_temp = tablaPruebas.NewRow();

                fila_temp["Código Prueba"] = idsPruebas[i];
                fila_temp["Prueba"] = nombresPruebas[i];

                tablaPruebas.Rows.Add(fila_temp);
            }
        }

        return tablaPruebas;
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                configurarCaracteresAceptadosBusqueda(true, true);

                iniciar_seccion_de_busqueda();

                HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "SIN_FILTRO";
                HiddenField_FILTRO_DROP.Value = String.Empty;
                HiddenField_FILTRO_DATO.Value = String.Empty;
                HiddenField_PAGINA_GRID.Value = "0";

                cargar_GridView_RESULTADOS_BUSQUEDA();
                break;
            case Acciones.NuevoPerfil:
                cargar_DropDownList_ocupaciones();
                cargar_DropDownList_EXPERIENCIA();
                cargar_DropDownList_Sexo();
                cargar_DropDownList_NIV_ESTUDIOS();
                cargar_DropDownList_NOMBRE_DOCUMENTO();
                cargar_DropDownList_PRUEBA();

                cargar_DropDownList_TipoConfirmacionReferencia();

                CargarAssesmentCenter();
                
                GridView_NOMBRE_DOCUMENTO.DataSource = null;
                GridView_NOMBRE_DOCUMENTO.DataBind();
                
                GridView_NOMBRE_PRUEBA.DataSource = null;
                GridView_NOMBRE_PRUEBA.DataBind();
                
                break;
            case Acciones.Modificar:
                cargar_DropDownList_NOMBRE_DOCUMENTO();
                cargar_DropDownList_PRUEBA();
                break;
            case Acciones.SeleccionDeCargoSinPerfil:
                cargar_DropDownList_EXPERIENCIA();
                cargar_DropDownList_Sexo();
                cargar_DropDownList_NIV_ESTUDIOS();
                cargar_DropDownList_NOMBRE_DOCUMENTO();
                cargar_DropDownList_PRUEBA();

                GridView_NOMBRE_DOCUMENTO.DataSource = null;
                GridView_NOMBRE_DOCUMENTO.DataBind();
                
                GridView_NOMBRE_PRUEBA.DataSource = null;
                GridView_NOMBRE_PRUEBA.DataBind();
                
                cargar_DropDownList_TipoConfirmacionReferencia();

                CheckBox_TipoBasica.Checked = false;
                CheckBox_TipoCompetencias.Checked = false;

                CargarAssesmentCenter();
                break;
        }
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_FORM_BOTONES.Visible = true;
                Button_NUEVO.Visible = true;
                Button_VOLVER.Visible = true;

                Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.CargarPerfil:
                Panel_FORM_BOTONES.Visible = true;
                Button_MODIFICAR.Visible = true;
                Button_VOLVER.Visible = true;

                Panel_FORMULARIO.Visible = true;
                Panel_CONTROL_REGISTRO.Visible = true;
                Panel_IDENTIFICADOR.Visible = true;
                Panel_OBSERVACIONES_ESPECIALES.Visible = true;

                Panel_documentos.Visible = true;
                Panel_pruebas.Visible = true;

                Panel_NivelDificultadRequerimientos.Visible = true;

                Panel_CONFIGURACION_REFERENCIA.Visible = true;

                Panel_CONFIGURACION_ENTRVISTA.Visible = true;
                Panel_TIPO_ENTREVISTA.Visible = true;
                Panel_COMPETENCIAS.Visible = true;
                Panel_InformacionAssesmentSeleccionado.Visible = true;

                Panel_FORM_BOTONES_ABAJO.Visible = true;
                Button_MODIFICAR_1.Visible = true;
                Button_VOLVER_1.Visible = true;
                break;
            case Acciones.NuevoPerfil:
                Panel_FORM_BOTONES.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;
                Button_VOLVER.Visible = true;

                Panel_FORMULARIO.Visible  = true;
                Panel_IDENTIFICADOR.Visible = true;
                Panel_OBSERVACIONES_ESPECIALES.Visible = true;

                Panel_documentos.Visible = true;
                Panel_ADICIONAR_DOCUMENTO.Visible = true;
                Panel_pruebas.Visible = true;
                Panel_ADICIONAR_PRUEBAS.Visible = true;

                Panel_NivelDificultadRequerimientos.Visible = true;

                Panel_CONFIGURACION_REFERENCIA.Visible = true;

                Panel_CONFIGURACION_ENTRVISTA.Visible = true;
                Panel_TIPO_ENTREVISTA.Visible = true;

                Panel_FORM_BOTONES_ABAJO.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
                Button_VOLVER_1.Visible = true;

                GridView_NOMBRE_DOCUMENTO.Columns[0].Visible = true;
                GridView_NOMBRE_PRUEBA.Columns[0].Visible = true;

                break;
            case Acciones.Modificar:
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;
                Button_VOLVER.Visible = true;

                Panel_ADICIONAR_DOCUMENTO.Visible = true;
                Panel_ADICIONAR_PRUEBAS.Visible = true;

                GridView_NOMBRE_DOCUMENTO.Columns[0].Visible = true;
                GridView_NOMBRE_PRUEBA.Columns[0].Visible = true;

                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
                Button_VOLVER_1.Visible = true;

                break;
        }
    }

    private void Iniciar()
    {
        Configurar();
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (IsPostBack == false)
        {
            Iniciar();
        }
    }

    private void Activar(Acciones accion)
    {
        switch (accion)
        { 
            case Acciones.NuevoPerfil:
                DropDownList_ocupacion.Enabled = true;
                TextBox_EDAD_MINIMA.Enabled = true;
                TextBox_EDAD_MAXIMA.Enabled = true;
                DropDownList_Experiencia.Enabled = true;
                DropDownList_SEXO.Enabled = true;
                DropDownList_NIV_ACADEMICO.Enabled = true;
                TextBox_OBSERVACIONES_ESPECIALES.Enabled = true;
                DropDownList_NOMBRE_DOCUMENTO.Enabled = true;
                DropDownList_NOMBRE_PRUEBA.Enabled = true;

                RadioButtonList_NivelDificultadReq.Enabled = true;

                DropDownList_TipoConfirmacionReferencia.Enabled = true;

                CheckBox_TipoBasica.Enabled = true;
                CheckBox_TipoCompetencias.Enabled = true;

                DropDownList_AssesmentCenter.Enabled = true;
                break;
            case Acciones.Modificar:
                TextBox_EDAD_MINIMA.Enabled = true;
                TextBox_EDAD_MAXIMA.Enabled = true;
                DropDownList_Experiencia.Enabled = true;
                DropDownList_SEXO.Enabled = true;
                DropDownList_NIV_ACADEMICO.Enabled = true;
                TextBox_OBSERVACIONES_ESPECIALES.Enabled = true;
                DropDownList_NOMBRE_DOCUMENTO.Enabled = true;
                DropDownList_NOMBRE_PRUEBA.Enabled = true;

                RadioButtonList_NivelDificultadReq.Enabled = true;

                DropDownList_TipoConfirmacionReferencia.Enabled = true;

                DropDownList_AssesmentCenter.Enabled = true;

                CheckBox_TipoBasica.Enabled = true;
                CheckBox_TipoCompetencias.Enabled = true;
                break;
        }
    }

    private void Limpiar(Acciones accion)
    { 
        switch(accion)
        {
            case Acciones.NuevoPerfil:
                DropDownList_ocupacion.Items.Clear();
                TextBox_EDAD_MINIMA.Text = "";
                TextBox_EDAD_MAXIMA.Text = "";
                DropDownList_Experiencia.Items.Clear();
                DropDownList_SEXO.Items.Clear();
                DropDownList_NIV_ACADEMICO.Items.Clear();
                TextBox_OBSERVACIONES_ESPECIALES.Text = "";
                TextBox_FincionesCargoSeleccionado.Text = "";

                GridView_NOMBRE_DOCUMENTO.DataSource = null;
                GridView_NOMBRE_DOCUMENTO.DataBind();

                GridView_NOMBRE_PRUEBA.DataSource = null;
                GridView_NOMBRE_PRUEBA.DataBind();

                RadioButtonList_NivelDificultadReq.SelectedIndex = -1;

                DropDownList_AssesmentCenter.Items.Clear();

                HiddenField_ID_PERFIL.Value = "";
                break;
            case Acciones.SeleccionDeCargoSinPerfil:
                TextBox_EDAD_MINIMA.Text = "";
                TextBox_EDAD_MAXIMA.Text = "";
                DropDownList_Experiencia.Items.Clear();
                DropDownList_SEXO.Items.Clear();
                DropDownList_NIV_ACADEMICO.Items.Clear();
                TextBox_OBSERVACIONES_ESPECIALES.Text = "";

                GridView_NOMBRE_DOCUMENTO.DataSource = null;
                GridView_NOMBRE_DOCUMENTO.DataBind();

                GridView_NOMBRE_PRUEBA.DataSource = null;
                GridView_NOMBRE_PRUEBA.DataBind();

                RadioButtonList_NivelDificultadReq.SelectedIndex = -1;

                DropDownList_TipoConfirmacionReferencia.Items.Clear();
                DropDownList_AssesmentCenter.Items.Clear();

                HiddenField_ID_PERFIL.Value = "";
                break;
        }
    }

    protected void Button_NUEVO_Click(object sender, System.EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.NuevoPerfil);
        Activar(Acciones.NuevoPerfil); 
        Limpiar(Acciones.NuevoPerfil);
        Cargar(Acciones.NuevoPerfil);
    }

    private void Guardar()
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        Decimal ID_OCUPACION = Convert.ToDecimal(DropDownList_ocupacion.SelectedValue);
        String EDADMIN = TextBox_EDAD_MINIMA.Text;
        String EDADMAX = TextBox_EDAD_MAXIMA.Text;

        String EXPERIENCIA = DropDownList_Experiencia.SelectedValue;
        String SEXO = DropDownList_SEXO.SelectedValue;
        String NIV_ACADEMICO = DropDownList_NIV_ACADEMICO.SelectedValue;

        String OBSERVACIONES_ESPECIALES = TextBox_OBSERVACIONES_ESPECIALES.Text;

        Boolean ESTADO = true;

        List<documentoPerfil> listaDocumentos = new List<documentoPerfil>();
        foreach (DataRow fila in obtenerTablaDeGrillaDocumentos().Rows)
        {
            documentoPerfil _documento = new documentoPerfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            _documento.IDDOCUMENTO = fila["Código Documento"].ToString().Trim();
            _documento.IDEMPRESA = ID_EMPRESA;
            listaDocumentos.Add(_documento);
        }

        List<pruebaPerfil> listaPruebas = new List<pruebaPerfil>();
        foreach (DataRow fila in obtenerTablaDeGrillaPruebas().Rows)
        {
            pruebaPerfil _pruebaPerfil = new pruebaPerfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            _pruebaPerfil.IDPRUEBA = fila["Código Prueba"].ToString().Trim();
            listaPruebas.Add(_pruebaPerfil);
        }

        String NIVEL_REQUERIMIENTO = RadioButtonList_NivelDificultadReq.SelectedValue;

        Decimal ID_CATEGORIA_REFERENCIA = Convert.ToDecimal(DropDownList_TipoConfirmacionReferencia.SelectedValue);

        Decimal ID_ASSESMENT_CENTER = 0;

        String TIPO_ENTREVISTA = null;

        if (CheckBox_TipoBasica.Checked == true)
        {
            TIPO_ENTREVISTA = "B";
        }
        else
        {
            ID_ASSESMENT_CENTER = Convert.ToDecimal(DropDownList_AssesmentCenter.SelectedValue);

            if (CheckBox_TipoCompetencias.Checked == true)
            {
                TIPO_ENTREVISTA = "A";
            }
        }

        perfil _perfil = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Decimal ID_PERFIL = _perfil.Adicionar(ID_EMPRESA, ID_OCUPACION, EDADMIN, EDADMAX, SEXO, EXPERIENCIA, NIV_ACADEMICO, listaDocumentos, listaPruebas, OBSERVACIONES_ESPECIALES, TIPO_ENTREVISTA, ID_CATEGORIA_REFERENCIA, ID_ASSESMENT_CENTER, ESTADO, NIVEL_REQUERIMIENTO);

        if (ID_PERFIL == 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _perfil.MensajeError, Proceso.Error);
        }
        else
        {
            maestrasInterfaz _maestrasInterfaz = new maestrasInterfaz();
            _maestrasInterfaz.CargarEnBdElManualServicioActual(ID_EMPRESA);


            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El Perfil fue creado correctamente y se e asignó el ID: " + ID_PERFIL.ToString() + ".", Proceso.Correcto);
        }
    }

    private void Actualizar()
    {
        Decimal ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        String EDADMIN = TextBox_EDAD_MINIMA.Text;
        String EDADMAX = TextBox_EDAD_MAXIMA.Text;

        String EXPERIENCIA = DropDownList_Experiencia.SelectedValue;
        String SEXO = DropDownList_SEXO.SelectedValue;
        String NIV_ACADEMICO = DropDownList_NIV_ACADEMICO.SelectedValue;

        String OBSERVACIONES_ESPECIALES = TextBox_OBSERVACIONES_ESPECIALES.Text;

        Boolean ESTADO = true;

        List<documentoPerfil> listaDocumentos = new List<documentoPerfil>();
        foreach (DataRow fila in obtenerTablaDeGrillaDocumentos().Rows)
        {
            documentoPerfil _documento = new documentoPerfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            _documento.IDDOCUMENTO = fila["Código Documento"].ToString().Trim();
            _documento.IDEMPRESA = ID_EMPRESA;
            listaDocumentos.Add(_documento);
        }

        List<pruebaPerfil> listaPruebas = new List<pruebaPerfil>();
        foreach (DataRow fila in obtenerTablaDeGrillaPruebas().Rows)
        {
            pruebaPerfil _pruebaPerfil = new pruebaPerfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            _pruebaPerfil.IDPRUEBA = fila["Código Prueba"].ToString().Trim();
            listaPruebas.Add(_pruebaPerfil);
        }

        String NIVEL_REQUERIMIENTO = RadioButtonList_NivelDificultadReq.SelectedValue;

        Decimal ID_CATEGORIA_REFERENCIA = Convert.ToDecimal(DropDownList_TipoConfirmacionReferencia.SelectedValue);
        Decimal ID_ASSESMENT_CENTER = 0;

        Boolean actualizarTipoEntrevista = true;
        String TIPO_ENTREVISTA = null;

        actualizarTipoEntrevista = true;

        if (CheckBox_TipoBasica.Checked == true)
        {
            TIPO_ENTREVISTA = "B";
        }
        else
        {
            if (CheckBox_TipoCompetencias.Checked == true)
            {
                TIPO_ENTREVISTA = "A";

                ID_ASSESMENT_CENTER = Convert.ToDecimal(DropDownList_AssesmentCenter.SelectedValue);
            }
        }

        perfil _perfil = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean correcto = _perfil.Actualizar(ID_PERFIL, ID_EMPRESA, EDADMIN, EDADMAX, SEXO, EXPERIENCIA, NIV_ACADEMICO, listaDocumentos, listaPruebas, OBSERVACIONES_ESPECIALES, actualizarTipoEntrevista, TIPO_ENTREVISTA,ID_CATEGORIA_REFERENCIA, ID_ASSESMENT_CENTER, ESTADO, NIVEL_REQUERIMIENTO);

        if (correcto == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _perfil.MensajeError, Proceso.Error);
        }
        else
        {
            maestrasInterfaz _maestrasInterfaz = new maestrasInterfaz();
            _maestrasInterfaz.CargarEnBdElManualServicioActual(ID_EMPRESA);

            Cargar(ID_PERFIL, false);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El Perfil fue modificado correctamente.", Proceso.Correcto);
        }
    }

    protected void Button_GUARDAR_Click(object sender, System.EventArgs e)
    {
        Boolean verificado = true;

        DataTable tablaDocumentos = obtenerTablaDeGrillaDocumentos();
        DataTable tablaPruebas = obtenerTablaDeGrillaPruebas();

        if ((tablaDocumentos.Rows.Count <= 0) || (tablaPruebas.Rows.Count <= 0))
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El perfil debe tener asociado por lo menos un Documento y una Prueba para poder continnuar.", Proceso.Advertencia);
            verificado = false;
        }
        else
        {
            if ((CheckBox_TipoBasica.Checked == false) && (CheckBox_TipoCompetencias.Checked == false))
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El perfil debe tener asociado por lo menos un formato de entrevista.", Proceso.Advertencia);
                verificado = false;
            }
        }

        if (verificado == true)
        {
            if (String.IsNullOrEmpty(HiddenField_ID_PERFIL.Value) == true)
            {
                Guardar();
            }
            else
            {
                Actualizar();
            }
        }
    }
    protected void Button_MODIFICAR_Click(object sender, System.EventArgs e)
    {
        Ocultar(Acciones.Modificar);
        Mostrar(Acciones.Modificar);
        Activar(Acciones.Modificar);
        Cargar(Acciones.Modificar);
    }

    protected void Button_CANCELAR_Click(object sender, System.EventArgs e)
    {
        if (String.IsNullOrEmpty(HiddenField_ID_PERFIL.Value) == true)
        {
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);
        }
        else
        {
            Decimal ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);

            Cargar(ID_PERFIL, false);
        }
    }

    protected void Button_LISTA_CONTRATOS_Click(object sender, System.EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);

        if (HiddenField_TIPO_BUSQUEDA_ACTUAL.Value == "CON_FILTRO")
        {
            Buscar();
        }
        else
        {
            cargar_GridView_RESULTADOS_BUSQUEDA();
        }
    }

    private void ocultar_mensaje(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.Style.Add("display", "none");
        panel_mensaje.Style.Add("display", "none");

        panel_fondo.Visible = false;
        panel_mensaje.Visible = false;
    }

    protected void Button_CERRAR_MENSAJE_Click(object sender, System.EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE, Panel_MENSAJES); 
    }

    private void CargarInformacionRegistroControl(DataRow filaPerfil)
    {
        TextBox_USU_CRE.Text = filaPerfil["USU_CRE"].ToString();
        try
        {
            TextBox_FCH_CRE.Text = DateTime.Parse(filaPerfil["FCH_CRE"].ToString()).ToShortDateString();
            TextBox_HOR_CRE.Text = DateTime.Parse(filaPerfil["FCH_CRE"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_CRE.Text = "";
            TextBox_HOR_CRE.Text = "";
        }
        TextBox_USU_MOD.Text = filaPerfil["USU_MOD"].ToString();
        try
        {
            TextBox_FCH_MOD.Text = DateTime.Parse(filaPerfil["FCH_MOD"].ToString()).ToShortDateString();
            TextBox_HOR_MOD.Text = DateTime.Parse(filaPerfil["FCH_MOD"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_MOD.Text = "";
            TextBox_HOR_MOD.Text = "";
        }
    }

    private DataRow obtenerDatosCargos(Decimal idOcupacion)
    {
        DataRow resultado = null;

        cargo _cargo = new cargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCargo = _cargo.ObtenerOcupacionPorIdOcupacion(idOcupacion);

        if (tablaCargo.Rows.Count > 0)
        {
            resultado = tablaCargo.Rows[0];
        }

        return resultado;
    }

    private void cargar_DropDownList_ocupaciones()
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        cargo _cargo = new cargo(Session["idEmpresa"].ToString(), Session["idEmpresa"].ToString());
        perfil _perfil = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
       
        DataTable _dataTable = _cargo.ObtenerRecOcupacionesPorIdEmp(ID_EMPRESA);

        DropDownList_ocupacion.Items.Clear();

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_ocupacion.Items.Add(item);

        foreach (DataRow fila in _dataTable.Rows)
        {
            item = new ListItem(fila["ID_OCUPACION"].ToString() + "-" + fila["NOM_OCUPACION"].ToString(), fila["ID_OCUPACION"].ToString());
            item.Attributes.Add("title", "ID: " + fila["ID_OCUPACION"].ToString() + " --> FUNCIONES: " + fila["DSC_FUNCIONES"].ToString().Trim());
            DropDownList_ocupacion.Items.Add(item);
        }

        DropDownList_ocupacion.DataBind();
    }

    private void cargar_DropDownList_NIV_ESTUDIOS()
    {
        DropDownList_NIV_ACADEMICO.Items.Clear();

        parametro _nivAcademico = new parametro(Session["idEmpresa"].ToString());
        DataTable tablanivAcademico = _nivAcademico.ObtenerParametrosPorTabla("NIV_ESTUDIOS");

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_NIV_ACADEMICO.Items.Add(item);

        foreach (DataRow fila in tablanivAcademico.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_NIV_ACADEMICO.Items.Add(item);
        }
        DropDownList_NIV_ACADEMICO.DataBind();
    }

    private void cargar_DropDownList_Sexo()
    {
        DropDownList_SEXO.Items.Clear();

        parametro _sexo = new parametro(Session["idEmpresa"].ToString());
        DataTable tablaSexo = _sexo.ObtenerParametrosPorTabla("SEXOREQ");

        ListItem item = new ListItem("Seleccione", "");
        DropDownList_SEXO.Items.Add(item);

        foreach (DataRow fila in tablaSexo.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_SEXO.Items.Add(item);
        }
        DropDownList_SEXO.DataBind();
    }

    private void cargar_DropDownList_EXPERIENCIA()
    {
        DropDownList_Experiencia.Items.Clear();

        parametro _experiencia = new parametro(Session["idEmpresa"].ToString());
        DataTable tablanivAcademico = _experiencia.ObtenerParametrosPorTabla("EXPERIENCIA");

        ListItem item = new ListItem("Seleccione", "");
        DropDownList_Experiencia.Items.Add(item);

        foreach (DataRow fila in tablanivAcademico.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_Experiencia.Items.Add(item);
        }
        DropDownList_Experiencia.DataBind();
    }

    private DataTable IniciarTablaSessionDocumentos()
    {
        DataTable tabla_temp = new DataTable();
        tabla_temp.Columns.Add("Código Documento");
        tabla_temp.Columns.Add("Documento");

        return tabla_temp;
    }

    private DataTable IniciarTablaSessionPruebas()
    {
        DataTable tabla_temp = new DataTable();
        tabla_temp.Columns.Add("Código Prueba");
        tabla_temp.Columns.Add("Prueba");

        return tabla_temp;
    }

    private void llenarGridDocumentos(Decimal idPerfil)
    {
        documentoPerfil _docs = new documentoPerfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable _dataTable = _docs.ObtenerPorIdPerfil(idPerfil);

        if (_dataTable.Rows.Count > 0)
        {
            Cargar_GridView_NOMBRE_DOCUMENTO_desde_tabla(_dataTable);
        }
        else
        {
            GridView_NOMBRE_DOCUMENTO.DataSource = null;
            GridView_NOMBRE_DOCUMENTO.DataBind();
        }
    }

    private void llenarGridPruebas(Decimal idPerfil)
    {
        pruebaPerfil _prueba = new pruebaPerfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable _dataTable = _prueba.ObtenerPorIdPerfil(idPerfil);

        if (_dataTable.Rows.Count > 0)
        {
            Cargar_GridView_NOMBRE_PRUEBA_desde_tabla(_dataTable);
        }
        else
        {
            GridView_NOMBRE_PRUEBA.DataSource = null;
            GridView_NOMBRE_PRUEBA.DataBind();
        }
    }

    private void cargar_DropDownList_ocupaciones_UnicoCargo(DataRow filaInfoCargo)
    {
        DropDownList_ocupacion.Items.Clear();

        DropDownList_ocupacion.Items.Add(new ListItem("Seleccione...",""));

        ListItem item = new ListItem(filaInfoCargo["ID_OCUPACION"].ToString() + "-" + filaInfoCargo["NOM_OCUPACION"].ToString(), filaInfoCargo["ID_OCUPACION"].ToString());
        item.Attributes.Add("title", "ID: " + filaInfoCargo["ID_OCUPACION"].ToString() + " --> FUNCIONES: " + filaInfoCargo["DSC_FUNCIONES"].ToString().Trim());
        DropDownList_ocupacion.Items.Add(item);

        DropDownList_ocupacion.DataBind();
    }

    private void CargarinformacionBasicaPerfil(DataRow filaInfoPerfil, Boolean mostrarTodosLosCargos)
    {
        Decimal ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);

        DataRow filaInfoCargo = obtenerDatosCargos(Convert.ToDecimal(filaInfoPerfil["ID_OCUPACION"]));

        if (filaInfoCargo != null)
        {
            if (mostrarTodosLosCargos == false)
            {
                cargar_DropDownList_ocupaciones_UnicoCargo(filaInfoCargo);
            }
            else
            {
                cargar_DropDownList_ocupaciones();
            }
            
            DropDownList_ocupacion.SelectedValue = filaInfoCargo["ID_OCUPACION"].ToString().Trim();

            TextBox_FincionesCargoSeleccionado.Text = filaInfoCargo["DSC_FUNCIONES"].ToString().Trim();
        }
        else
        {
            TextBox_FincionesCargoSeleccionado.Text = "";
        }

        cargar_DropDownList_NIV_ESTUDIOS();
        DropDownList_NIV_ACADEMICO.SelectedValue = filaInfoPerfil["NIV_ESTUDIOS"].ToString().Trim();
        DropDownList_NIV_ACADEMICO.DataBind();

        cargar_DropDownList_Sexo();
        DropDownList_SEXO.SelectedValue = filaInfoPerfil["SEXO"].ToString().Trim();
        DropDownList_SEXO.DataBind();

        TextBox_EDAD_MAXIMA.Text = filaInfoPerfil["EDAD_MAX"].ToString().Trim();
        TextBox_EDAD_MINIMA.Text = filaInfoPerfil["EDAD_MIN"].ToString().Trim();

        try
        {
            RadioButtonList_NivelDificultadReq.SelectedValue = filaInfoPerfil["NIVEL_REQUERIMIENTO"].ToString();
        }
        catch 
        {
            RadioButtonList_NivelDificultadReq.SelectedIndex = -1;
        }

        cargar_DropDownList_EXPERIENCIA();
        DropDownList_Experiencia.SelectedValue = filaInfoPerfil["EXPERIENCIA"].ToString().Trim();
        DropDownList_Experiencia.DataBind();

        llenarGridDocumentos(ID_PERFIL);

        llenarGridPruebas(ID_PERFIL);

        cargar_DropDownList_TipoConfirmacionReferencia();
        try
        {
            DropDownList_TipoConfirmacionReferencia.SelectedValue = filaInfoPerfil["ID_CATEGORIA_REFERENCIA"].ToString().Trim();
        }
        catch
        {
            DropDownList_TipoConfirmacionReferencia.SelectedIndex = 0;
        }

        TextBox_OBSERVACIONES_ESPECIALES.Text = filaInfoPerfil["OBSERVACIONES_ESPECIALES"].ToString().Trim();
    }

    private void CargarAssesmentCenter()
    {
        FabricaAssesment _fab = new FabricaAssesment(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaAssesment = _fab.ObtenerAssesmentCenteActivos();

        DropDownList_AssesmentCenter.Items.Clear();

        DropDownList_AssesmentCenter.Items.Add(new ListItem("Seleccione...",""));

        if (tablaAssesment.Rows.Count <= 0)
        {
            if (_fab.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _fab.MensajeError, Proceso.Error);
            }   
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información de Assesment Center Activos.", Proceso.Advertencia);
            }
        }
        else
        {
            foreach (DataRow filaTabla in tablaAssesment.Rows)
            {
                DropDownList_AssesmentCenter.Items.Add(new ListItem(filaTabla["NOMBRE_ASSESMENT"].ToString(), filaTabla["ID_ASSESMENT_CENTER"].ToString()));
            }
        }

        DropDownList_AssesmentCenter.DataBind();
    }

    private void CargarTipoEntrevista(DataRow filaInfoPerfil)
    {
        Decimal ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);

        HiddenField_TIPO_ENTREVISTA_INICIAL.Value = String.Empty;

        if (filaInfoPerfil["TIPO_ENTREVISTA"].ToString().Trim() == "B")
        {
            HiddenField_TIPO_ENTREVISTA_INICIAL.Value = "B";

            CheckBox_TipoBasica.Checked = true;
            CheckBox_TipoCompetencias.Checked = false;

            Panel_COMPETENCIAS.Visible = false;
            Panel_InformacionAssesmentSeleccionado.Visible = false;
        }
        else
        {
            CheckBox_TipoBasica.Checked = false;

            if (filaInfoPerfil["TIPO_ENTREVISTA"].ToString().Trim().Contains("A") == true)
            {
                HiddenField_TIPO_ENTREVISTA_INICIAL.Value = "A";
              
                CheckBox_TipoCompetencias.Checked = true;

                Panel_COMPETENCIAS.Visible = true;
                Panel_InformacionAssesmentSeleccionado.Visible = true;
                
                CargarAssesmentCenter();
                try
                {
                    DropDownList_AssesmentCenter.SelectedValue = filaInfoPerfil["ID_ASSESMENT_CENTAR"].ToString().Trim();
                }
                catch
                {
                    DropDownList_AssesmentCenter.SelectedIndex = 0;
                }


                Decimal ID_ASSESMENT_CENTER = Convert.ToDecimal(filaInfoPerfil["ID_ASSESMENT_CENTAR"]);

                FabricaAssesment _fab = new FabricaAssesment(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                DataTable tablaAssesment = _fab.ObtenerAssesmentCentePorId(ID_ASSESMENT_CENTER);

                if (tablaAssesment.Rows.Count <= 0)
                {
                    if (_fab.MensajeError != null)
                    {
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _fab.MensajeError, Proceso.Error);
                    }
                    else
                    {
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información del Assesment Center relacionado con el perfil.", Proceso.Error);
                    }
                    Panel_InformacionAssesmentSeleccionado.Visible = false;
                }
                else
                {
                    DataRow filaAssesment = tablaAssesment.Rows[0];

                    Panel_InformacionAssesmentSeleccionado.Visible = true;

                    Label_DescripcionAssesment.Text = filaAssesment["DESCRIPCION_ASSESMENT"].ToString().Trim();

                    DataTable tablaCompetencias = _fab.ObtenerCompetenciasAssesmentCenteActivos(ID_ASSESMENT_CENTER, 0);

                    if (tablaCompetencias.Rows.Count <= 0)
                    {
                        if (_fab.MensajeError != null)
                        {
                            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _fab.MensajeError, Proceso.Error);
                        }

                        Label_CompetenciasAssesment.Text = "Sin Competencias asignadas.";
                    }
                    else
                    {
                        Label_CompetenciasAssesment.Text = "";

                        for (int i = 0; i < tablaCompetencias.Rows.Count; i++)
                        {
                            DataRow filaCompetencia = tablaCompetencias.Rows[i];

                            if (i == 0)
                            {
                                Label_CompetenciasAssesment.Text = filaCompetencia["COMPETENCIA"].ToString().Trim();
                            }
                            else
                            {
                                Label_CompetenciasAssesment.Text += "<br />" + filaCompetencia["COMPETENCIA"].ToString().Trim();
                            }
                        }
                    }
                }
            }
            else
            {
                CheckBox_TipoCompetencias.Checked = false;
                Panel_COMPETENCIAS.Visible = false;
                Panel_InformacionAssesmentSeleccionado.Visible = false;
            }
        }

        if (String.IsNullOrEmpty(HiddenField_TIPO_ENTREVISTA_INICIAL.Value) == true)
        {
            HiddenField_TIPO_ENTREVISTA_INICIAL.Value = "VACIO";

            CheckBox_TipoBasica.Checked = false;
            CheckBox_TipoCompetencias.Checked = false;
            Panel_COMPETENCIAS.Visible = false;
            Panel_InformacionAssesmentSeleccionado.Visible = false;
        }
    }

    private void Cargar(Decimal ID_PERFIL, Boolean cargarTodosLosCargos)
    {
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Mostrar(Acciones.CargarPerfil);

        HiddenField_ID_PERFIL.Value = ID_PERFIL.ToString();

        perfil _perfiles = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaPerfil = _perfiles.ObtenerPorRegistro(ID_PERFIL);
        DataRow filaInfoPerfil = tablaPerfil.Rows[0];

        CargarInformacionRegistroControl(filaInfoPerfil);

        CargarinformacionBasicaPerfil(filaInfoPerfil, cargarTodosLosCargos);

        CargarTipoEntrevista(filaInfoPerfil);
    }

    protected void GridView_PERFILES_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal REGISTRO = Convert.ToDecimal(GridView_PERFILES.DataKeys[indexSeleccionado].Values["REGISTRO"]);

            Cargar(REGISTRO, false);
        }
    }

    protected void GridView_PERFILES_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        GridView_PERFILES.PageIndex = e.NewPageIndex;
        HiddenField_PAGINA_GRID.Value = e.NewPageIndex.ToString();

        if (HiddenField_TIPO_BUSQUEDA_ACTUAL.Value == "CON_FILTRO")
        {
            Buscar();
        }
        else
        {
            cargar_GridView_RESULTADOS_BUSQUEDA();
        }
    }

    protected void GridView_NOMBRE_DOCUMENTO_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);

        DataTable tabla_temp = obtenerTablaDeGrillaDocumentos();

        tabla_temp.Rows.RemoveAt(index);

        Cargar_GridView_NOMBRE_DOCUMENTO_desde_tabla(tabla_temp);
        GridView_NOMBRE_DOCUMENTO.SelectedIndex = -1;
    }

    private DataTable obtenerTablaDeGrillaDocumentos()
    {
        DataTable tablaResultado = new DataTable();

        tablaResultado.Columns.Add("Código Documento");
        tablaResultado.Columns.Add("Documento");

        for (int i = 0; i < GridView_NOMBRE_DOCUMENTO.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_NOMBRE_DOCUMENTO.Rows[i];

            Decimal ID_DOCUMENTO = Convert.ToDecimal(GridView_NOMBRE_DOCUMENTO.DataKeys[i].Values["Código Documento"]);

            Label dato = filaGrilla.FindControl("Label_Documento") as Label;
            String NOMBRE = dato.Text;

            DataRow filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["Código Documento"] = ID_DOCUMENTO;
            filaTablaResultado["Documento"] = NOMBRE;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }

    private DataTable obtenerTablaDeGrillaPruebas()
    {
        DataTable tablaResultado = new DataTable();

        tablaResultado.Columns.Add("Código Prueba");
        tablaResultado.Columns.Add("Prueba");

        for (int i = 0; i < GridView_NOMBRE_PRUEBA.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_NOMBRE_PRUEBA.Rows[i];

            Decimal ID_PRUEBA = Convert.ToDecimal(GridView_NOMBRE_PRUEBA.DataKeys[i].Values["Código Prueba"]);

            Label dato = filaGrilla.FindControl("Label_Prueba") as Label;
            String NOMBRE = dato.Text;

            DataRow filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["Código Prueba"] = ID_PRUEBA;
            filaTablaResultado["Prueba"] = NOMBRE;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }

    private void Cargar_GridView_NOMBRE_DOCUMENTO_desde_tabla(DataTable tablaDocumentos)
    {
        GridView_NOMBRE_DOCUMENTO.DataSource = tablaDocumentos;
        GridView_NOMBRE_DOCUMENTO.DataBind();

        for (int i = 0; i < GridView_NOMBRE_DOCUMENTO.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_NOMBRE_DOCUMENTO.Rows[i];
            DataRow filaTabla = tablaDocumentos.Rows[i];

            Label texto = filaGrilla.FindControl("Label_Documento") as Label;
            texto.Text = filaTabla["Documento"].ToString().Trim();
        }
    }

    private void Cargar_GridView_NOMBRE_PRUEBA_desde_tabla(DataTable tablaPruebas)
    {
        GridView_NOMBRE_PRUEBA.DataSource = tablaPruebas;
        GridView_NOMBRE_PRUEBA.DataBind();

        for (int i = 0; i < GridView_NOMBRE_PRUEBA.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_NOMBRE_PRUEBA.Rows[i];
            DataRow filaTabla = tablaPruebas.Rows[i];

            Label texto = filaGrilla.FindControl("Label_Prueba") as Label;
            texto.Text = filaTabla["Prueba"].ToString().Trim();
        }
    }

    protected void Button_ADICIONAR_DOCUMENTO_Click(object sender, System.EventArgs e)
    {
        if (DropDownList_NOMBRE_DOCUMENTO.SelectedValue.ToString() != "")
        {
            Boolean verificado = true;

            DataTable tablaDocumentos = obtenerTablaDeGrillaDocumentos();

            foreach (DataRow fila in tablaDocumentos.Rows)
            {
                if (Convert.ToDecimal(fila["Código Documento"]) == Convert.ToDecimal(DropDownList_NOMBRE_DOCUMENTO.SelectedValue))
                {
                    verificado = false;
                    break;
                }
            }

            if (verificado == true)
            {
                DataRow fila_temp = tablaDocumentos.NewRow();

                fila_temp["Código Documento"] = DropDownList_NOMBRE_DOCUMENTO.SelectedValue;
                fila_temp["Documento"] = DropDownList_NOMBRE_DOCUMENTO.SelectedItem.ToString();

                tablaDocumentos.Rows.Add(fila_temp);

                Cargar_GridView_NOMBRE_DOCUMENTO_desde_tabla(tablaDocumentos);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE_DOCUMENTOS, Image_MENSAJE_POPUP_DOCUMENTOS, Panel_MENSAJES_DOCUMENTOS, Label_MENSAJE_DOCUMENTOS, "El documento ya se encuentra en la lista.", Proceso.Advertencia);
            }
        }
    }

    protected void GridView_NOMBRE_PRUEBA_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);

        DataTable tabla_temp = obtenerTablaDeGrillaPruebas();

        tabla_temp.Rows.RemoveAt(index);

        Cargar_GridView_NOMBRE_PRUEBA_desde_tabla(tabla_temp);
        GridView_NOMBRE_PRUEBA.SelectedIndex = -1;
    }

    protected void Button_ADICIONAR_PRUEBA_Click(object sender, System.EventArgs e)
    {
        if (DropDownList_NOMBRE_PRUEBA.SelectedValue.ToString() != "")
        {
            Boolean verificado = true;

            DataTable tablaPruebas = obtenerTablaDeGrillaPruebas();

            foreach (DataRow fila in tablaPruebas.Rows)
            {
                if (Convert.ToDecimal(fila["Código Prueba"]) == Convert.ToDecimal(DropDownList_NOMBRE_PRUEBA.SelectedValue))
                {
                    verificado = false;
                    break;
                }
            }

            if (verificado == true)
            {
                DataRow fila_temp = tablaPruebas.NewRow();

                fila_temp["Código Prueba"] = DropDownList_NOMBRE_PRUEBA.SelectedValue;
                fila_temp["Prueba"] = DropDownList_NOMBRE_PRUEBA.SelectedItem.ToString();

                tablaPruebas.Rows.Add(fila_temp);

                Cargar_GridView_NOMBRE_PRUEBA_desde_tabla(tablaPruebas);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La Prueba ya se encuentra en la lista.", Proceso.Advertencia);
            }
        }
    }

    protected void CheckBox_TipoBasica_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_TipoBasica.Checked == true)
        {
            Panel_COMPETENCIAS.Visible = false;
            Panel_InformacionAssesmentSeleccionado.Visible = false;
            DropDownList_AssesmentCenter.ClearSelection();
            CheckBox_TipoCompetencias.Checked = false;
        }
    }

    protected void CheckBox_TipoCompetencias_CheckedChanged(object sender, EventArgs e)
    {
        Decimal ID_PERFIL = 0;
        if (String.IsNullOrEmpty(HiddenField_ID_PERFIL.Value) == false)
        {
            ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);
        }

        if (CheckBox_TipoCompetencias.Checked == true)
        {
            CheckBox_TipoBasica.Checked = false;

            Panel_COMPETENCIAS.Visible = true;

            CargarAssesmentCenter();

            Panel_InformacionAssesmentSeleccionado.Visible = false;
        }
        else
        {
            Panel_COMPETENCIAS.Visible = false;
            DropDownList_AssesmentCenter.SelectedIndex = 0;

            Panel_InformacionAssesmentSeleccionado.Visible = false;
        }
    }

    protected void DropDownList_ocupacion_SelectedIndexChanged(object sender, EventArgs e)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        if (DropDownList_ocupacion.SelectedIndex <= 0)
        {
            TextBox_FincionesCargoSeleccionado.Text = "";
        }
        else
        {
            Decimal ID_OCUPACION = Convert.ToDecimal(DropDownList_ocupacion.SelectedValue);

            DataRow filaOcupacion = obtenerDatosCargos(ID_OCUPACION);

            if (filaOcupacion != null)
            {
                perfil _perfil = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                DataTable tablaPerfil = _perfil.ObtenerVenDPerfilesConOcupacionPorIdEmpresaYIdOcupacion(ID_EMPRESA, ID_OCUPACION);

                if (tablaPerfil.Rows.Count <= 0)
                {
                    Ocultar(Acciones.SeleccionDeCargoSinPerfil);
                    Limpiar(Acciones.SeleccionDeCargoSinPerfil);
                    Cargar(Acciones.SeleccionDeCargoSinPerfil);

                    TextBox_FincionesCargoSeleccionado.Text = filaOcupacion["DSC_FUNCIONES"].ToString().Trim();

                    DataTable tablaDocumentos = ObtenerDocumentosBasicosEnGrilla(GridView_NOMBRE_DOCUMENTO);
                    Cargar_GridView_NOMBRE_DOCUMENTO_desde_tabla(tablaDocumentos);

                    DataTable tablaPruebas = ObtenerPruebasBasicasEnGrilla(GridView_NOMBRE_PRUEBA);
                    Cargar_GridView_NOMBRE_PRUEBA_desde_tabla(tablaPruebas);
                }
                else
                {
                    DataRow filaPefil = tablaPerfil.Rows[0];
                    Decimal REGISTRO = Convert.ToDecimal(filaPefil["REGISTRO"]);

                    Cargar(REGISTRO,true);

                    Ocultar(Acciones.Modificar);
                    Mostrar(Acciones.Modificar);
                    Activar(Acciones.Modificar);
                    Cargar(Acciones.Modificar);

                    DropDownList_ocupacion.Enabled = true;

                    DataTable tablaDocumentos = ObtenerDocumentosBasicosEnGrilla(GridView_NOMBRE_DOCUMENTO);
                    Cargar_GridView_NOMBRE_DOCUMENTO_desde_tabla(tablaDocumentos);

                    DataTable tablaPruebas = ObtenerPruebasBasicasEnGrilla(GridView_NOMBRE_PRUEBA);
                    Cargar_GridView_NOMBRE_PRUEBA_desde_tabla(tablaPruebas);
                }
            }
            else
            {

                Ocultar(Acciones.SeleccionDeCargoSinPerfil);
                Limpiar(Acciones.SeleccionDeCargoSinPerfil);
                Cargar(Acciones.SeleccionDeCargoSinPerfil);

                TextBox_FincionesCargoSeleccionado.Text = "";
            }
        }
    }


    protected void DropDownList_BUSCAR_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_BUSCAR.SelectedValue == "")
        {
            configurarCaracteresAceptadosBusqueda(true, true);
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "NOM_OCUPACION")
            {
                configurarCaracteresAceptadosBusqueda(true, false);
            }
        }
        TextBox_BUSCAR.Text = "";
    }

    private void Buscar()
    {
        String datosCapturados = HiddenField_FILTRO_DATO.Value;
        String campo = HiddenField_FILTRO_DROP.Value;

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

        perfil _PERFIL = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaResultadosBusqueda = new DataTable();

        if (campo == "NOM_OCUPACION")
        {
            tablaResultadosBusqueda = _PERFIL.ObtenerVenDPerfilesConOcupacionPorIdEmpresaYNomOcupacion(ID_EMPRESA, datosCapturados);
        }

        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio); 
        Mostrar(Acciones.Inicio);

        if (tablaResultadosBusqueda.Rows.Count <= 0)
        {
            if (_PERFIL.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _PERFIL.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros que cumplieran los datos de busqueda.", Proceso.Advertencia);
            }

            Panel_RESULTADOS_GRID.Visible = false;
        }
        else
        {
            GridView_PERFILES.PageIndex = Convert.ToInt32(HiddenField_PAGINA_GRID.Value);

            GridView_PERFILES.DataSource = tablaResultadosBusqueda;
            GridView_PERFILES.DataBind();
        }
    }

    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "CON_FILTRO";
        HiddenField_FILTRO_DROP.Value = DropDownList_BUSCAR.SelectedValue;
        HiddenField_FILTRO_DATO.Value = TextBox_BUSCAR.Text;
        HiddenField_PAGINA_GRID.Value = "0";

        Buscar();
    }
    protected void DropDownList_AssesmentCenter_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_AssesmentCenter.SelectedIndex <= 0)
        {
            Panel_InformacionAssesmentSeleccionado.Visible = false;
        }
        else
        { 
            Decimal ID_ASSESMENT_CENTER = Convert.ToDecimal(DropDownList_AssesmentCenter.SelectedValue);

            FabricaAssesment _fab = new FabricaAssesment(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaAssesment = _fab.ObtenerAssesmentCentePorId(ID_ASSESMENT_CENTER);

            if (tablaAssesment.Rows.Count <= 0)
            {
                if (_fab.MensajeError != null)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _fab.MensajeError, Proceso.Error);
                }
                else
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información del Assesment Center seleccionado.", Proceso.Error);
                }
            }
            else
            {
                DataRow filaAssesment = tablaAssesment.Rows[0];

                Panel_InformacionAssesmentSeleccionado.Visible = true;

                Label_DescripcionAssesment.Text = filaAssesment["DESCRIPCION_ASSESMENT"].ToString().Trim();

                DataTable tablaCompetencias = _fab.ObtenerCompetenciasAssesmentCenteActivos(ID_ASSESMENT_CENTER, 0);

                if (tablaCompetencias.Rows.Count <= 0)
                {
                    if (_fab.MensajeError != null)
                    {
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _fab.MensajeError, Proceso.Error);
                    }

                    Label_CompetenciasAssesment.Text = "Sin Competencias asignadas.";
                }
                else
                {
                    Label_CompetenciasAssesment.Text = "";

                    for (int i = 0; i < tablaCompetencias.Rows.Count; i++)
                    {
                        DataRow filaCompetencia = tablaCompetencias.Rows[i];

                        if (i == 0)
                        {
                            Label_CompetenciasAssesment.Text = "<b>•</b>  " + filaCompetencia["COMPETENCIA"].ToString().Trim();
                        }
                        else
                        {
                            Label_CompetenciasAssesment.Text += "<br /><b>•</b>  " + filaCompetencia["COMPETENCIA"].ToString().Trim();
                        }
                    }
                }
            }
        }
    }
    protected void Button_CERRAR_MENSAJE_DOCUMENTOS_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE_DOCUMENTOS, Panel_MENSAJES_DOCUMENTOS); 
    }
    protected void Button_CERRAR_MENSAJE_PRUEBAS_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE_PRUEBAS, Panel_MENSAJES_PRUEBAS); 
    }
}