
using System.Web.UI.WebControls;
using System;
using Brainsbits.LLB.seleccion;
using System.Data;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.comercial;
using TSHAK.Components;
using System.Web;
using System.Collections.Generic;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using Brainsbits.LLB.seguridad;
using AjaxControlToolkit;
using System.Web.UI;
using Brainsbits.LLB;
public partial class _Entrevista : System.Web.UI.Page
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

    private System.Drawing.Color colorAmarillo = System.Drawing.ColorTranslator.FromHtml("#FFF200");
    private System.Drawing.Color colorVerde = System.Drawing.ColorTranslator.FromHtml("#50CE04");
    private System.Drawing.Color colorGris = System.Drawing.ColorTranslator.FromHtml("#cccccc");

    private enum Acciones
    {
        Inicio = 0,
        SolicitudIngreso,
        TipoEntrevista,
        Entrevista,
        ConEntrevista,
        ConArchivoWord,
        PruebasAAplicar,
        CompetenciasAAplicar,
        NuevaEntrvita,
        Modificar
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
        configurar_paneles_popup(Panel_FONDO_MENSAJE_TIPO_ENTREVISTA, Panel_MENSAJES_TIPO_ENTREVISTA);
        configurar_paneles_popup(Panel_FONDO_MENSAJE_PRUEBAS, Panel_MENSAJES_PRUEBAS);
        configurar_paneles_popup(Panel_FONDO_MENSAJE_TIPO, Panel_MENSAJES_TIPO);
    }

    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = false;

                Panel_FORM_BOTONES.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_IMPRIMIR.Visible = false;
                Button_CANCELAR.Visible = false;

                Panel_RESULTADOS_GRID.Visible = false;
                Panel_DATOS_CANDIDATO.Visible = false;
                Panel_TIPO_ENTREVISTA.Visible = false;

                Panel_TIPO_SIN_REQUISICION.Visible = false;
                Panel_TIPO_CON_REQUISICION.Visible = false;

                Panel_ENTREVISTA_BASICA.Visible = false;
                Panel_CONTROL_REGISTRO.Visible = false;
                Panel_VER_ARCHIVO_DE_WORD.Visible = false;

                Panel_PRUEBAS.Visible = false;
                Panel_GRILLA_SELECCION_PRUEBAS.Visible = false;
                Panel_PRUEBAS_SELECCIONADAS.Visible = false;

                Panel_Botones_ComposicionFamiliar.Visible = false;
                Button_NUEVA_COMPOSICIONFAMILIAR.Visible = false;
                Button_GUARDAR_COMPOSICIONFAMILIAR.Visible = false;
                Button_CANCELAR_COMPOSICIONFAMILIAR.Visible = false;
                GridView_ComposicionFamiliar.Columns[0].Visible = false;
                GridView_ComposicionFamiliar.Columns[1].Visible = false;

                Panel_EducacionFormal.Visible = false;
                GridView_EducacionFormal.Columns[0].Visible = false;
                GridView_EducacionFormal.Columns[1].Visible = false;
                Button_NuevoEF.Visible = false;
                Button_GuardarEF.Visible = false;
                Button_CancelarEF.Visible = false;

                Panel_EducacionNoFormal.Visible = false;
                GridView_EducacionNoFormal.Columns[0].Visible = false;
                GridView_EducacionNoFormal.Columns[1].Visible = false;
                Button_NuevoENF.Visible = false;
                Button_GuardarENF.Visible = false;
                Button_CancelarENF.Visible = false;

                Panel_ExperienciaLaboral.Visible = false;
                GridView_ExperienciaLaboral.Columns[0].Visible = false;
                GridView_ExperienciaLaboral.Columns[1].Visible = false;
                Button_NuevoEmpleo.Visible = false;
                Button_GuardarEmpleo.Visible = false;
                Button_CancelarEmpleo.Visible = false;

                Panel_ContenedorTipoEntrevista.Visible = false;
                Panel_SELECCION_TIPO_ENTREVISTA.Visible = false;
                Panel_AvisoDeSeleccionDeTipoEntrevista.Visible = false;
                Panel_SeleccionCompetencias.Visible = false;
                Panel_InformacionAssesmentSeleccionado.Visible = false;
                Panel_BotonesDeSeleciconTipoEntrevista.Visible = false;

                Panel_CALIFICACIONES_COMPETENCIAS.Visible = false;

                Panel_CONCEPTO_GENERAL.Visible = false;

                Panel_BOTONES_ACCION_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_IMPRIMIR_1.Visible = false; 
                Button_CANCELAR_1.Visible = false;
                Button_DOCUMENTOS_1.Visible = false;
                break;
            case Acciones.SolicitudIngreso:
                Panel_RESULTADOS_GRID.Visible = false;
                break;
            case Acciones.TipoEntrevista:
                Panel_TIPO_ENTREVISTA.Visible = false;
                Panel_TIPO_SIN_REQUISICION.Visible = false;
                Panel_TIPO_CON_REQUISICION.Visible = false;
                break;
            case Acciones.Modificar:
                Button_MODIFICAR.Visible = false;
                Button_IMPRIMIR.Visible = false;
                Button_CANCELAR.Visible = false;

                Panel_CONTROL_REGISTRO.Visible = false;
                Panel_VER_ARCHIVO_DE_WORD.Visible = false;

                if (String.IsNullOrEmpty(HiddenField_ID_PERFIL.Value) == true)
                {
                    Panel_PRUEBAS_SELECCIONADAS.Visible = false;
                    Panel_CALIFICACIONES_COMPETENCIAS.Visible = false;
                }

                Button_MODIFICAR_1.Visible = false;
                Button_IMPRIMIR_1.Visible = false; 
                Button_CANCELAR_1.Visible = false;
                Button_DOCUMENTOS_1.Visible = false;
                break;


        }
    }

    private void Desactivar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                RadioButtonList_TIPO_ENTREVISTA.Enabled = false;
                DropDownList_EMPRESA_ASPIRANTE.Enabled = false;

                TextBox_FCH_CRE.Enabled = false;
                TextBox_FCH_MOD.Enabled = false;
                TextBox_HOR_CRE.Enabled = false;
                TextBox_HOR_MOD.Enabled = false;
                TextBox_USU_CRE.Enabled = false;
                TextBox_USU_MOD.Enabled = false;

                TextBox_FCH_ENTREVISTA.Enabled = false;
                TextBox_COM_C_FAM.Enabled = false;
                TextBox_COM_C_ACA.Enabled = false;
                TextBox_COM_F_LAB.Enabled = false;

                CheckBox_TipoBasica.Enabled = false;
                CheckBox_TipoCompetencias.Enabled = false;

                TextBox_CONCEPTO_GENERAL.Enabled = false;
                break;
            case Acciones.TipoEntrevista:
                RadioButtonList_TIPO_ENTREVISTA.Enabled = false;
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
            case Acciones.SolicitudIngreso:
                Panel_DATOS_CANDIDATO.Visible = true;
                break;
            case Acciones.TipoEntrevista:
                Panel_TIPO_ENTREVISTA.Visible = true;
                break;
            case Acciones.Entrevista:
                break;
            case Acciones.ConEntrevista:
                Panel_FORM_BOTONES.Visible = true;
                Button_IMPRIMIR.Visible = true;

                Panel_DATOS_CANDIDATO.Visible = true;
                Panel_ENTREVISTA_BASICA.Visible = true;
                Panel_CONTROL_REGISTRO.Visible = true;

                Panel_CONCEPTO_GENERAL.Visible = true;

                Panel_BOTONES_ACCION_1.Visible = true;
                
                Button_IMPRIMIR_1.Visible = true; 

                Panel_ContenedorTipoEntrevista.Visible = true;
                Panel_SELECCION_TIPO_ENTREVISTA.Visible = true;

                if (Label_ARCHIVO_SOLICITUD.Text != "CONTRATADO" && Label_ARCHIVO_SOLICITUD.Text != "POR CONTRATAR")
                {
                    Button_MODIFICAR.Visible = true;
                    Button_MODIFICAR_1.Visible = true;
                }
                break;
            case Acciones.ConArchivoWord:
                Panel_VER_ARCHIVO_DE_WORD.Visible = true;
                break;
            case Acciones.PruebasAAplicar:
                Panel_PRUEBAS.Visible = true;
                Panel_PRUEBAS_SELECCIONADAS.Visible = true;
                break;
            case Acciones.CompetenciasAAplicar:
                Panel_CALIFICACIONES_COMPETENCIAS.Visible = true;
                break;
            case Acciones.NuevaEntrvita:
                Panel_FORM_BOTONES.Visible = true;
                Button_CANCELAR.Visible = true;
                Button_GUARDAR.Visible = true;
                Panel_DATOS_CANDIDATO.Visible = true;

                Panel_ENTREVISTA_BASICA.Visible = true;

                GridView_ComposicionFamiliar.Columns[0].Visible = true;
                GridView_ComposicionFamiliar.Columns[1].Visible = true;
                Panel_Botones_ComposicionFamiliar.Visible = true;
                Button_NUEVA_COMPOSICIONFAMILIAR.Visible = true;

                GridView_EducacionFormal.Columns[0].Visible = true;
                GridView_EducacionFormal.Columns[1].Visible = true;
                Panel_EducacionFormal.Visible = true;
                Button_NuevoEF.Visible = true;

                GridView_EducacionNoFormal.Columns[0].Visible = true;
                GridView_EducacionNoFormal.Columns[1].Visible = true;
                Panel_EducacionNoFormal.Visible = true;
                Button_NuevoENF.Visible = true;

                GridView_ExperienciaLaboral.Columns[0].Visible = true;
                GridView_ExperienciaLaboral.Columns[1].Visible = true;
                Panel_ExperienciaLaboral.Visible = true;
                Button_NuevoEmpleo.Visible = true;

                Panel_CONCEPTO_GENERAL.Visible = true;
                
                Panel_ContenedorTipoEntrevista.Visible = true;

                if (String.IsNullOrEmpty(HiddenField_ID_PERFIL.Value) == true)
                {
                    Panel_PRUEBAS.Visible = true;
                    Panel_GRILLA_SELECCION_PRUEBAS.Visible = true;

                    Panel_AvisoDeSeleccionDeTipoEntrevista.Visible = true;
                    Panel_BotonesDeSeleciconTipoEntrevista.Visible = true;
                }
                else
                {
                    Button_GUARDAR.Visible = true;
                    Button_GUARDAR_1.Visible = true;
                }

                Panel_BOTONES_ACCION_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
                Button_GUARDAR_1.Visible = true;
                break;
            case Acciones.Modificar:
                Button_GUARDAR_1.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;
                Button_CANCELAR_1.Visible = true;

                if (String.IsNullOrEmpty(HiddenField_ID_PERFIL.Value) == true)
                {
                    Panel_PRUEBAS.Visible = true;
                    Panel_GRILLA_SELECCION_PRUEBAS.Visible = true;

                    Panel_AvisoDeSeleccionDeTipoEntrevista.Visible = true;
                    Panel_BotonesDeSeleciconTipoEntrevista.Visible = true;
                }
                else
                {
                    Button_GUARDAR.Visible = true;
                    Button_GUARDAR_1.Visible = true;
                }

                GridView_ComposicionFamiliar.Columns[0].Visible = true;
                GridView_ComposicionFamiliar.Columns[1].Visible = true;
                Panel_Botones_ComposicionFamiliar.Visible = true;
                Button_NUEVA_COMPOSICIONFAMILIAR.Visible = true;

                GridView_EducacionFormal.Columns[0].Visible = true;
                GridView_EducacionFormal.Columns[1].Visible = true;
                Panel_EducacionFormal.Visible = true;
                Button_NuevoEF.Visible = true;

                GridView_EducacionNoFormal.Columns[0].Visible = true;
                GridView_EducacionNoFormal.Columns[1].Visible = true;
                Panel_EducacionNoFormal.Visible = true;
                Button_NuevoENF.Visible = true;

                GridView_ExperienciaLaboral.Columns[0].Visible = true;
                GridView_ExperienciaLaboral.Columns[1].Visible = true;
                Panel_ExperienciaLaboral.Visible = true;
                Button_NuevoEmpleo.Visible = true;
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

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Ninguno", "");
        DropDownList_BUSCAR.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("Nombres", "NOMBRES");
        DropDownList_BUSCAR.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("Apellidos", "APELLIDOS");
        DropDownList_BUSCAR.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("Número de Identificación", "NUM_DOC_IDENTIDAD");
        DropDownList_BUSCAR.Items.Add(item);
        DropDownList_BUSCAR.DataBind();
    }


    private void Cargar_DropDownList_TipoFamiliar(DropDownList drop)
    {
        drop.Items.Clear();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccionae...", "");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("ABUELA", "ABUELA");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("ABUELO", "ABUELO");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("MAMA", "MAMA");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("PAPA", "PAPA");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("ESPOSA", "ESPOSA");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("ESPOSO", "ESPOSO");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("HERMANA", "HERMANA");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("HERMANO", "HERMANO");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("HIJA", "HIJA");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("HIJO", "HIJO");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("HIJASTRA", "HIJASTRA");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("HIJASTRO", "HIJASTRO");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("TIA", "TIA");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("TIO", "TIO");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("PRIMA", "PRIMA");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("PRIMO", "PRIMO");
        drop.Items.Add(item);

        drop.DataBind();
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

                HiddenField_ID_EMPRESA.Value = "";
                HiddenField_ID_ENTREVISTA.Value = "";
                HiddenField_ID_PERFIL.Value = "";
                HiddenField_ID_REQUISICION.Value = "";
                HiddenField_ID_SOLICITUD.Value = "";
                HiddenField_TIPO_ENTREVISTA.Value = "";
                HiddenField_ID_ASSESMENT_CENTER.Value = "";
            
                iniciar_seccion_de_busqueda();
                break;
            case Acciones.NuevaEntrvita:
                TextBox_FCH_ENTREVISTA.Text = DateTime.Now.ToShortDateString();

                if (String.IsNullOrEmpty(HiddenField_ID_PERFIL.Value) == true)
                {
                    CheckBox_TipoBasica.Checked = false;
                    CheckBox_TipoCompetencias.Checked = false;
                }
                break;
            case Acciones.Modificar:
                cargar_GridView_SELECCION_PRUEBAS();

                if (String.IsNullOrEmpty(HiddenField_ID_PERFIL.Value) == true)
                {
                    CheckBox_TipoBasica.Checked = false;
                    CheckBox_TipoCompetencias.Checked = false;
                }
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
        Page.Header.Title = "ENTREVISTAS DE SELECCIÓN";

        if (IsPostBack == false)
        {
            Iniciar();
        }
    }
    
    private void Guardar()
    {
        tools _tools = new tools();

        Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);

        Decimal ID_REQUERIMIENTO = 0;
        if (String.IsNullOrEmpty(HiddenField_ID_REQUISICION.Value) == false)
        {
            ID_REQUERIMIENTO = Convert.ToDecimal(HiddenField_ID_REQUISICION.Value);
        }

        Decimal ID_EMPRESA = 0;
        if (String.IsNullOrEmpty(HiddenField_ID_EMPRESA.Value) == false)
        {
            ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        }

        Decimal ID_PERFIL = 0;
        if (String.IsNullOrEmpty(HiddenField_ID_PERFIL.Value) == false)
        {
            ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);
        }

        DateTime FCH_ENTREVISTA = Convert.ToDateTime(TextBox_FCH_ENTREVISTA.Text);

        List<ComposicionFamiliar> listaComposicionFamiliar = new List<ComposicionFamiliar>();
        for (int i = 0; i < GridView_ComposicionFamiliar.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ComposicionFamiliar.Rows[i];

            Decimal ID_COMPOSICION = Convert.ToDecimal(GridView_ComposicionFamiliar.DataKeys[i].Values["ID_COMPOSICION"]);

            DropDownList dropTipoFamiliar = filaGrilla.FindControl("DropDownList_TipoFamiliar") as DropDownList;
            String ID_TIPO_FAMILIAR = dropTipoFamiliar.SelectedValue;

            TextBox textoNombres = filaGrilla.FindControl("TextBox_NombresFamiliar") as TextBox;
            String NOMBRES = textoNombres.Text;

            TextBox textoApellidos = filaGrilla.FindControl("TextBox_ApellidosFamiliar") as TextBox;
            String APELLIDOS = textoApellidos.Text;

            DateTime FECHA_NACIMIENTO = new DateTime();
            TextBox textoFechaNacimiento = filaGrilla.FindControl("TextBox_FechaNacimientoFamiliar") as TextBox;
            try
            {
                FECHA_NACIMIENTO = Convert.ToDateTime(textoFechaNacimiento.Text);
            }
            catch
            {
                FECHA_NACIMIENTO = new DateTime();
            }

            TextBox textoProfesion = filaGrilla.FindControl("TextBox_ProfesionFamiliar") as TextBox;
            String PROFESION = textoProfesion.Text;

            CheckBox checkExtrajero = filaGrilla.FindControl("") as CheckBox;
            DropDownList dropCiudad = filaGrilla.FindControl("DropDownList_CiudadFamiliar") as DropDownList;
            String ID_CIUDAD = dropCiudad.SelectedValue;

            Boolean VIVE_CON_EL = false;
            CheckBox checkViveConEl = filaGrilla.FindControl("CheckBox_ViveConElFamiliar") as CheckBox;
            if (checkViveConEl.Checked == true)
            {
                VIVE_CON_EL = true;
            }

            Boolean ACTIVO = true;

            ComposicionFamiliar _composicionParaLista = new ComposicionFamiliar();

            _composicionParaLista.ACTIVO = ACTIVO;
            _composicionParaLista.APELLIDOS = APELLIDOS;
            _composicionParaLista.FECHA_NACIMIENTO = FECHA_NACIMIENTO;
            _composicionParaLista.ID_CIUDAD = ID_CIUDAD;
            _composicionParaLista.ID_COMPOSICION = ID_COMPOSICION;
            _composicionParaLista.ID_TIPO_FAMILIAR = ID_TIPO_FAMILIAR;
            _composicionParaLista.NOMBRES = NOMBRES;
            _composicionParaLista.PROFESION = PROFESION;
            _composicionParaLista.REGISTRO_ENTREVISTA = 0;
            _composicionParaLista.VIVE_CON_EL = VIVE_CON_EL;

            listaComposicionFamiliar.Add(_composicionParaLista);
        }
        String COM_C_FAM = TextBox_COM_C_FAM.Text.Trim();


        List<FormacionAcademica> listaFormacionAcademica = new List<FormacionAcademica>();
        for (int i = 0; i < GridView_EducacionFormal.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_EducacionFormal.Rows[i];

            String TIPO_EDUCACION = "FORMAL";

            Decimal ID_INFO_ACADEMICA = Convert.ToDecimal(GridView_EducacionFormal.DataKeys[i].Values["ID_INFO_ACADEMICA"]);

            DropDownList dropnivAcademico = filaGrilla.FindControl("DropDownList_NivAcademico") as DropDownList;
            String NIVEL_ACADEMICO = dropnivAcademico.SelectedValue;

            TextBox textoInstitucion = filaGrilla.FindControl("TextBox_Institucion") as TextBox;
            String INSTITUCION = textoInstitucion.Text;

            Int32 ANNO = 0;
            TextBox textoAnno = filaGrilla.FindControl("TextBox_Anno") as TextBox;
            if (String.IsNullOrEmpty(textoAnno.Text) == false)
            {
                ANNO = Convert.ToInt32(textoAnno.Text);
            }
            else
            {
                ANNO = 0;
            }

            TextBox textoObservaciones = filaGrilla.FindControl("TextBox_Observaciones") as TextBox;
            String OBSERVACIONES = textoObservaciones.Text;

            FormacionAcademica _formacionParaLista = new FormacionAcademica();

            _formacionParaLista.ACTIVO = true;
            _formacionParaLista.ANNO = ANNO;
            _formacionParaLista.CURSO = null;
            _formacionParaLista.DURACION = 0;
            _formacionParaLista.ID_INFO_ACADEMICA = ID_INFO_ACADEMICA;
            _formacionParaLista.INSTITUCION = INSTITUCION;
            _formacionParaLista.NIVEL_ACADEMICO = NIVEL_ACADEMICO;
            _formacionParaLista.OBSERVACIONES = OBSERVACIONES;
            _formacionParaLista.REGISTRO_ENTREVISTA = 0;
            _formacionParaLista.TIPO_EDUCACION = TIPO_EDUCACION;
            _formacionParaLista.UNIDAD_DURACION = null;

            listaFormacionAcademica.Add(_formacionParaLista);

        }

        for (int i = 0; i < GridView_EducacionNoFormal.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_EducacionNoFormal.Rows[i];

            String TIPO_EDUCACION = "NO FORMAL";

            Decimal ID_INFO_ACADEMICA = Convert.ToDecimal(GridView_EducacionNoFormal.DataKeys[i].Values["ID_INFO_ACADEMICA"]);

            TextBox textoCurso = filaGrilla.FindControl("TextBox_Curso") as TextBox;
            String CURSO = textoCurso.Text;

            TextBox textoInstitucion = filaGrilla.FindControl("TextBox_Institucion") as TextBox;
            String INSTITUCION = textoInstitucion.Text;

            Decimal DURACION = 0;
            TextBox textoDuracion = filaGrilla.FindControl("TextBox_Duracion") as TextBox;
            if (String.IsNullOrEmpty(textoDuracion.Text) == false)
            {
                DURACION = Convert.ToDecimal(textoDuracion.Text);
            }
            else
            {
                DURACION = 0;
            }

            DropDownList dropUnidadDuracion = filaGrilla.FindControl("DropDownList_UnidadDuracion") as DropDownList;
            String UNIDAD_DURACION = dropUnidadDuracion.SelectedValue;

            TextBox textoobservaciones = filaGrilla.FindControl("TextBox_Observaciones") as TextBox;
            String OBSERVACIONES = textoobservaciones.Text;

            FormacionAcademica _formacionParaLista = new FormacionAcademica();

            _formacionParaLista.ACTIVO = true;
            _formacionParaLista.ANNO = 0;
            _formacionParaLista.CURSO = CURSO;
            _formacionParaLista.DURACION = DURACION;
            _formacionParaLista.ID_INFO_ACADEMICA = ID_INFO_ACADEMICA;
            _formacionParaLista.INSTITUCION = INSTITUCION;
            _formacionParaLista.NIVEL_ACADEMICO = null;
            _formacionParaLista.OBSERVACIONES = OBSERVACIONES;
            _formacionParaLista.REGISTRO_ENTREVISTA = 0;
            _formacionParaLista.TIPO_EDUCACION = TIPO_EDUCACION;
            _formacionParaLista.UNIDAD_DURACION = UNIDAD_DURACION;

            listaFormacionAcademica.Add(_formacionParaLista);

        }
        String COM_C_ACA = TextBox_COM_C_ACA.Text.Trim();

        List<ExperienciaLaboral> listaExperiencia = new List<ExperienciaLaboral>();
        for (int i = 0; i < GridView_ExperienciaLaboral.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ExperienciaLaboral.Rows[i];

            Decimal ID_EXPERIENCIA = Convert.ToDecimal(GridView_ExperienciaLaboral.DataKeys[i].Values["ID_EXPERIENCIA"]);

            TextBox textoEmpresa = filaGrilla.FindControl("TextBox_Empresa") as TextBox;
            String EMPRESA = textoEmpresa.Text;

            TextBox textoCargo = filaGrilla.FindControl("TextBox_Cargo") as TextBox;
            String CARGO = textoCargo.Text;

            TextBox textoFunciones = filaGrilla.FindControl("TextBox_FuncionesRealizadas") as TextBox;
            String FUNCIONES = textoFunciones.Text;

            DateTime FECHA_INGRESO;
            TextBox textoFechaIngreso = filaGrilla.FindControl("TextBox_FechaIngreso") as TextBox;
            try
            {
                FECHA_INGRESO = Convert.ToDateTime(textoFechaIngreso.Text);
            }
            catch
            { 
                FECHA_INGRESO = new DateTime();
            }

            DateTime FECHA_RETIRO;
            TextBox textoFechaRetiro = filaGrilla.FindControl("TextBox_FechaRetiro") as TextBox;
            try
            {
                FECHA_RETIRO = Convert.ToDateTime(textoFechaRetiro.Text);
            }
            catch
            {
                FECHA_RETIRO = new DateTime();
            }

            DropDownList dropMotivoRetiro = filaGrilla.FindControl("DropDownList_MotivoRetiro") as DropDownList;
            String MOTIVO_RETIRO = null;
            if (String.IsNullOrEmpty(dropMotivoRetiro.SelectedValue) == false)
            {
                MOTIVO_RETIRO = dropMotivoRetiro.SelectedValue;
            }
            
            Decimal ULTIMO_SALARIO = 0;
            TextBox textoUltimoSalario = filaGrilla.FindControl("TextBox_Ultimosalario") as TextBox;
            try
            {
                ULTIMO_SALARIO = Convert.ToDecimal(textoUltimoSalario.Text);
            }
            catch
            {
                ULTIMO_SALARIO = 0;
            }

            ExperienciaLaboral _experienciaParaLista = new ExperienciaLaboral();

            _experienciaParaLista.ACTIVO = true;
            _experienciaParaLista.CARGO = CARGO;
            _experienciaParaLista.EMPRESA_CLIENTE = EMPRESA;
            _experienciaParaLista.FECHA_INGRESO = FECHA_INGRESO;
            _experienciaParaLista.FECHA_RETIRO = FECHA_RETIRO;
            _experienciaParaLista.FUNCIONES = FUNCIONES;
            _experienciaParaLista.ID_EXPERIENCIA = ID_EXPERIENCIA;
            _experienciaParaLista.MOTIVO_RETIRO = MOTIVO_RETIRO;
            _experienciaParaLista.REGISTRO_ENTREVISTA = 0;
            _experienciaParaLista.ULTIMO_SALARIO = ULTIMO_SALARIO;

            listaExperiencia.Add(_experienciaParaLista);
        }
        String COM_F_LAB = TextBox_COM_F_LAB.Text.Trim();


        String COM_C_GEN = TextBox_CONCEPTO_GENERAL.Text.Trim();
  

        List<listaPruebasAplicados> listaPruebas = new List<listaPruebasAplicados>();

        for (int i = 0; i < GridView_PRUEBAS_SELECCIONADAS.Rows.Count; i++)
        {
            Decimal ID_APLICACION_PRUEBA = Convert.ToDecimal(GridView_PRUEBAS_SELECCIONADAS.DataKeys[i].Values["ID_APLICACION_PRUEBA"]);
            Decimal ID_PRUEBA = Convert.ToDecimal(GridView_PRUEBAS_SELECCIONADAS.DataKeys[i].Values["ID_PRUEBA"]);
            Decimal ID_CATEGORIA = Convert.ToDecimal(GridView_PRUEBAS_SELECCIONADAS.DataKeys[i].Values["ID_CATEGORIA"]);
            String NOM_PRUEBA = GridView_PRUEBAS_SELECCIONADAS.DataKeys[i].Values["NOM_PRUEBA"].ToString();

            TextBox textoFechaPrueba = GridView_PRUEBAS_SELECCIONADAS.Rows[i].FindControl("TextBox_FECHA_PRUEBA") as TextBox;
            DateTime FECHA_R = Convert.ToDateTime(textoFechaPrueba.Text);

            TextBox textoResultados = GridView_PRUEBAS_SELECCIONADAS.Rows[i].FindControl("TextBox_RESULTADO_PRUEBAS") as TextBox;
            String RESULTADOS = textoResultados.Text;

            FileUpload archivoCargado = GridView_PRUEBAS_SELECCIONADAS.Rows[i].FindControl("FileUpload_ARCHIVO") as FileUpload;

            Byte[] ARCHIVO = null;
            Int32 ARCHIVO_TAMANO = 0;
            String ARCHIVO_EXTENSION = null;
            String ARCHIVO_TYPE = null;
            if (archivoCargado.HasFile == true)
            {
                using (BinaryReader reader = new BinaryReader(archivoCargado.PostedFile.InputStream))
                {
                    ARCHIVO = reader.ReadBytes(archivoCargado.PostedFile.ContentLength);
                    ARCHIVO_TAMANO = archivoCargado.PostedFile.ContentLength;
                    ARCHIVO_TYPE = archivoCargado.PostedFile.ContentType;
                    ARCHIVO_EXTENSION = _tools.obtenerExtensionArchivo(archivoCargado.PostedFile.FileName);
                }
            }

            listaPruebasAplicados _pruebaParaLista = new listaPruebasAplicados();

            _pruebaParaLista.ID_SOLICITUD = ID_SOLICITUD;
            _pruebaParaLista.FECHA_R = FECHA_R;
            _pruebaParaLista.ID_CATEGORIA = ID_CATEGORIA;
            _pruebaParaLista.ID_PRUEBA = ID_PRUEBA;
            _pruebaParaLista.NOM_PRUEBA = NOM_PRUEBA;
            _pruebaParaLista.REGISTRO = ID_APLICACION_PRUEBA;
            _pruebaParaLista.RESULTADOS = RESULTADOS;
            _pruebaParaLista.ARCHIVO_PRUEBA = ARCHIVO;
            _pruebaParaLista.ARCHIVO_TAMANO = ARCHIVO_TAMANO;
            _pruebaParaLista.ARCHIVO_EXTENSION = ARCHIVO_EXTENSION;
            _pruebaParaLista.ARCHIVO_TYPE = ARCHIVO_TYPE;

            listaPruebas.Add(_pruebaParaLista);
        }

        List<AplicacionCompetencia> listaCompetencias = new List<AplicacionCompetencia>();
        if (ID_PERFIL > 0)
        {
            if (HiddenField_TIPO_ENTREVISTA.Value.Contains("A") == true)
            {
                for (int i = 0; i < GridView_CompetenciasAssesment.Rows.Count; i++)
                {
                    GridViewRow filaGrilla = GridView_CompetenciasAssesment.Rows[i];
                    AplicacionCompetencia _aplicacionParaLista = new AplicacionCompetencia();

                    CheckBox checkCumple = filaGrilla.FindControl("CheckBox_CumpleCompetencia") as CheckBox;
                    DropDownList dropNivelCalificaicon = filaGrilla.FindControl("DropDownList_NivelCumplimiento") as DropDownList;
                    CheckBox checkNoCumple = filaGrilla.FindControl("CheckBox_NoCumpeCompetencia_CheckedChanged") as CheckBox;
                    TextBox textoObservaciones = filaGrilla.FindControl("TextBox_ObservacionesCalificacion") as TextBox;

                    _aplicacionParaLista.CALIFICACION = "NO CUMPLE";
                    _aplicacionParaLista.NIVEL_CALIFICACION = 0;
                    if (checkCumple.Checked == true)
                    {
                        _aplicacionParaLista.CALIFICACION = "CUMPLE";
                        _aplicacionParaLista.NIVEL_CALIFICACION = Convert.ToInt32(dropNivelCalificaicon.SelectedValue);
                    }

                    _aplicacionParaLista.ID_APLICACION_COMPETENCIA = Convert.ToDecimal(GridView_CompetenciasAssesment.DataKeys[i].Values["ID_APLICACION_COMPETENCIA"]);
                    _aplicacionParaLista.ID_COMPETENCIA_ASSESMENT = Convert.ToDecimal(GridView_CompetenciasAssesment.DataKeys[i].Values["ID_COMPETENCIA_ASSESMENT"]);
                    _aplicacionParaLista.ID_SOLICITUD = ID_SOLICITUD;
                    _aplicacionParaLista.OBSERVACIONES = textoObservaciones.Text.Trim();

                    listaCompetencias.Add(_aplicacionParaLista);
                }
            }
        }
        else
        {
            if (CheckBox_TipoCompetencias.Checked == true)
            {
                for (int i = 0; i < GridView_CompetenciasAssesment.Rows.Count; i++)
                {
                    GridViewRow filaGrilla = GridView_CompetenciasAssesment.Rows[i];
                    AplicacionCompetencia _aplicacionParaLista = new AplicacionCompetencia();

                    CheckBox checkCumple = filaGrilla.FindControl("CheckBox_CumpleCompetencia") as CheckBox;
                    DropDownList dropNivelCalificaicon = filaGrilla.FindControl("DropDownList_NivelCumplimiento") as DropDownList;
                    CheckBox checkNoCumple = filaGrilla.FindControl("CheckBox_NoCumpeCompetencia_CheckedChanged") as CheckBox;
                    TextBox textoObservaciones = filaGrilla.FindControl("TextBox_ObservacionesCalificacion") as TextBox;

                    _aplicacionParaLista.CALIFICACION = "NO CUMPLE";
                    _aplicacionParaLista.NIVEL_CALIFICACION = 0;
                    if (checkCumple.Checked == true)
                    {
                        _aplicacionParaLista.CALIFICACION = "CUMPLE";
                        _aplicacionParaLista.NIVEL_CALIFICACION = Convert.ToInt32(dropNivelCalificaicon.SelectedValue);
                    }

                    _aplicacionParaLista.ID_APLICACION_COMPETENCIA = Convert.ToDecimal(GridView_CompetenciasAssesment.DataKeys[i].Values["ID_APLICACION_COMPETENCIA"]);
                    _aplicacionParaLista.ID_COMPETENCIA_ASSESMENT = Convert.ToDecimal(GridView_CompetenciasAssesment.DataKeys[i].Values["ID_COMPETENCIA_ASSESMENT"]);
                    _aplicacionParaLista.ID_SOLICITUD = ID_SOLICITUD;
                    _aplicacionParaLista.OBSERVACIONES = textoObservaciones.Text.Trim();

                    listaCompetencias.Add(_aplicacionParaLista);
                }
            }
        }

        hojasVida _hojasVida = new hojasVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        Decimal ID_ENTREVISTA = _hojasVida.AdicionarEntrevista(ID_SOLICITUD, FCH_ENTREVISTA, COM_C_FAM, COM_F_LAB, COM_C_ACA, COM_C_GEN, listaPruebas, ID_REQUERIMIENTO, listaComposicionFamiliar, listaFormacionAcademica, listaExperiencia, ID_PERFIL, listaCompetencias);

        if (ID_ENTREVISTA <= 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _hojasVida.MensajeError, Proceso.Error);
        }
        else
        {

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La entrevista fue actualizada correctamente.", Proceso.Correcto);
        }
    }

    private void Actualizar()
    {
        tools _tools = new tools();

        Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);

        Decimal ID_REQUERIMIENTO = 0;
        if (String.IsNullOrEmpty(HiddenField_ID_REQUISICION.Value) == false)
        {
            ID_REQUERIMIENTO = Convert.ToDecimal(HiddenField_ID_REQUISICION.Value);
        }

        Decimal ID_EMPRESA = 0;
        if (String.IsNullOrEmpty(HiddenField_ID_EMPRESA.Value) == false)
        {
            ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        }

        Decimal ID_PERFIL = 0;
        if (String.IsNullOrEmpty(HiddenField_ID_PERFIL.Value) == false)
        {
            ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);
        }

        Decimal ID_ENTREVISTA = Convert.ToDecimal(HiddenField_ID_ENTREVISTA.Value);

        DateTime FCH_ENTREVISTA = Convert.ToDateTime(TextBox_FCH_ENTREVISTA.Text);


        List<ComposicionFamiliar> listaComposicionFamiliar = new List<ComposicionFamiliar>();
        for (int i = 0; i < GridView_ComposicionFamiliar.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ComposicionFamiliar.Rows[i];

            Decimal ID_COMPOSICION = Convert.ToDecimal(GridView_ComposicionFamiliar.DataKeys[i].Values["ID_COMPOSICION"]);

            DropDownList dropTipoFamiliar = filaGrilla.FindControl("DropDownList_TipoFamiliar") as DropDownList;
            String ID_TIPO_FAMILIAR = dropTipoFamiliar.SelectedValue;

            TextBox textoNombres = filaGrilla.FindControl("TextBox_NombresFamiliar") as TextBox;
            String NOMBRES = textoNombres.Text;

            TextBox textoApellidos = filaGrilla.FindControl("TextBox_ApellidosFamiliar") as TextBox;
            String APELLIDOS = textoApellidos.Text;

            DateTime FECHA_NACIMIENTO = new DateTime();
            TextBox textoFechaNacimiento = filaGrilla.FindControl("TextBox_FechaNacimientoFamiliar") as TextBox;
            try
            {
                FECHA_NACIMIENTO = Convert.ToDateTime(textoFechaNacimiento.Text);
            }
            catch
            {
                FECHA_NACIMIENTO = new DateTime();
            }

            TextBox textoProfesion = filaGrilla.FindControl("TextBox_ProfesionFamiliar") as TextBox;
            String PROFESION = textoProfesion.Text;

            DropDownList dropCiudad = filaGrilla.FindControl("DropDownList_CiudadFamiliar") as DropDownList;
            String ID_CIUDAD = dropCiudad.SelectedValue;

            Boolean VIVE_CON_EL = false;
            CheckBox checkViveConEl = filaGrilla.FindControl("CheckBox_ViveConElFamiliar") as CheckBox;
            if (checkViveConEl.Checked == true)
            {
                VIVE_CON_EL = true;
            }

            Boolean ACTIVO = true;

            ComposicionFamiliar _composicionParaLista = new ComposicionFamiliar();

            _composicionParaLista.ACTIVO = ACTIVO;
            _composicionParaLista.APELLIDOS = APELLIDOS;
            _composicionParaLista.FECHA_NACIMIENTO = FECHA_NACIMIENTO;
            _composicionParaLista.ID_CIUDAD = ID_CIUDAD;
            _composicionParaLista.ID_COMPOSICION = ID_COMPOSICION;
            _composicionParaLista.ID_TIPO_FAMILIAR = ID_TIPO_FAMILIAR;
            _composicionParaLista.NOMBRES = NOMBRES;
            _composicionParaLista.PROFESION = PROFESION;
            _composicionParaLista.REGISTRO_ENTREVISTA = 0;
            _composicionParaLista.VIVE_CON_EL = VIVE_CON_EL;

            listaComposicionFamiliar.Add(_composicionParaLista);
        }
        String COM_C_FAM = TextBox_COM_C_FAM.Text.Trim();


        List<FormacionAcademica> listaFormacionAcademica = new List<FormacionAcademica>();
        for (int i = 0; i < GridView_EducacionFormal.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_EducacionFormal.Rows[i];

            String TIPO_EDUCACION = "FORMAL";

            Decimal ID_INFO_ACADEMICA = Convert.ToDecimal(GridView_EducacionFormal.DataKeys[i].Values["ID_INFO_ACADEMICA"]);

            DropDownList dropnivAcademico = filaGrilla.FindControl("DropDownList_NivAcademico") as DropDownList;
            String NIVEL_ACADEMICO = dropnivAcademico.SelectedValue;

            TextBox textoInstitucion = filaGrilla.FindControl("TextBox_Institucion") as TextBox;
            String INSTITUCION = textoInstitucion.Text;

            Int32 ANNO = 0;
            TextBox textoAnno = filaGrilla.FindControl("TextBox_Anno") as TextBox;
            if (String.IsNullOrEmpty(textoAnno.Text) == false)
            {
                ANNO = Convert.ToInt32(textoAnno.Text);
            }
            else
            {
                ANNO = 0;
            }

            TextBox textoObservaciones = filaGrilla.FindControl("TextBox_Observaciones") as TextBox;
            String OBSERVACIONES = textoObservaciones.Text;

            FormacionAcademica _formacionParaLista = new FormacionAcademica();

            _formacionParaLista.ACTIVO = true;
            _formacionParaLista.ANNO = ANNO;
            _formacionParaLista.CURSO = null;
            _formacionParaLista.DURACION = 0;
            _formacionParaLista.ID_INFO_ACADEMICA = ID_INFO_ACADEMICA;
            _formacionParaLista.INSTITUCION = INSTITUCION;
            _formacionParaLista.NIVEL_ACADEMICO = NIVEL_ACADEMICO;
            _formacionParaLista.OBSERVACIONES = OBSERVACIONES;
            _formacionParaLista.REGISTRO_ENTREVISTA = 0;
            _formacionParaLista.TIPO_EDUCACION = TIPO_EDUCACION;
            _formacionParaLista.UNIDAD_DURACION = null;

            listaFormacionAcademica.Add(_formacionParaLista);

        }

        for (int i = 0; i < GridView_EducacionNoFormal.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_EducacionNoFormal.Rows[i];

            String TIPO_EDUCACION = "NO FORMAL";

            Decimal ID_INFO_ACADEMICA = Convert.ToDecimal(GridView_EducacionNoFormal.DataKeys[i].Values["ID_INFO_ACADEMICA"]);

            TextBox textoCurso = filaGrilla.FindControl("TextBox_Curso") as TextBox;
            String CURSO = textoCurso.Text;

            TextBox textoInstitucion = filaGrilla.FindControl("TextBox_Institucion") as TextBox;
            String INSTITUCION = textoInstitucion.Text;

            Decimal DURACION = 0;
            TextBox textoDuracion = filaGrilla.FindControl("TextBox_Duracion") as TextBox;
            if (String.IsNullOrEmpty(textoDuracion.Text) == false)
            {
                DURACION = Convert.ToDecimal(textoDuracion.Text);
            }
            else
            {
                DURACION = 0;
            }

            DropDownList dropUnidadDuracion = filaGrilla.FindControl("DropDownList_UnidadDuracion") as DropDownList;
            String UNIDAD_DURACION = dropUnidadDuracion.SelectedValue;

            TextBox textoobservaciones = filaGrilla.FindControl("TextBox_Observaciones") as TextBox;
            String OBSERVACIONES = textoobservaciones.Text;

            FormacionAcademica _formacionParaLista = new FormacionAcademica();

            _formacionParaLista.ACTIVO = true;
            _formacionParaLista.ANNO = 0;
            _formacionParaLista.CURSO = CURSO;
            _formacionParaLista.DURACION = DURACION;
            _formacionParaLista.ID_INFO_ACADEMICA = ID_INFO_ACADEMICA;
            _formacionParaLista.INSTITUCION = INSTITUCION;
            _formacionParaLista.NIVEL_ACADEMICO = null;
            _formacionParaLista.OBSERVACIONES = OBSERVACIONES;
            _formacionParaLista.REGISTRO_ENTREVISTA = 0;
            _formacionParaLista.TIPO_EDUCACION = TIPO_EDUCACION;
            _formacionParaLista.UNIDAD_DURACION = UNIDAD_DURACION;

            listaFormacionAcademica.Add(_formacionParaLista);

        }
        String COM_C_ACA = TextBox_COM_C_ACA.Text.Trim();

        List<ExperienciaLaboral> listaExperiencia = new List<ExperienciaLaboral>();
        for (int i = 0; i < GridView_ExperienciaLaboral.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ExperienciaLaboral.Rows[i];

            Decimal ID_EXPERIENCIA = Convert.ToDecimal(GridView_ExperienciaLaboral.DataKeys[i].Values["ID_EXPERIENCIA"]);

            TextBox textoEmpresa = filaGrilla.FindControl("TextBox_Empresa") as TextBox;
            String EMPRESA = textoEmpresa.Text;

            TextBox textoCargo = filaGrilla.FindControl("TextBox_Cargo") as TextBox;
            String CARGO = textoCargo.Text;

            TextBox textoFunciones = filaGrilla.FindControl("TextBox_FuncionesRealizadas") as TextBox;
            String FUNCIONES = textoFunciones.Text;

            DateTime FECHA_INGRESO;
            TextBox textoFechaIngreso = filaGrilla.FindControl("TextBox_FechaIngreso") as TextBox;
            try
            {
                FECHA_INGRESO = Convert.ToDateTime(textoFechaIngreso.Text);
            }
            catch
            {
                FECHA_INGRESO = new DateTime();
            }

            DateTime FECHA_RETIRO;
            TextBox textoFechaRetiro = filaGrilla.FindControl("TextBox_FechaRetiro") as TextBox;
            try
            {
                FECHA_RETIRO = Convert.ToDateTime(textoFechaRetiro.Text);
            }
            catch
            {
                FECHA_RETIRO = new DateTime();
            }

            DropDownList dropMotivoRetiro = filaGrilla.FindControl("DropDownList_MotivoRetiro") as DropDownList;
            String MOTIVO_RETIRO = null;
            if (String.IsNullOrEmpty(dropMotivoRetiro.SelectedValue) == false)
            {
                MOTIVO_RETIRO = dropMotivoRetiro.Text;
            }

            Decimal ULTIMO_SALARIO = 0;
            TextBox textoUltimoSalario = filaGrilla.FindControl("TextBox_Ultimosalario") as TextBox;
            try
            {
                ULTIMO_SALARIO = Convert.ToDecimal(textoUltimoSalario.Text);
            }
            catch
            {
                ULTIMO_SALARIO = 0;
            }

            ExperienciaLaboral _experienciaParaLista = new ExperienciaLaboral();

            _experienciaParaLista.ACTIVO = true;
            _experienciaParaLista.CARGO = CARGO;
            _experienciaParaLista.EMPRESA_CLIENTE = EMPRESA;
            _experienciaParaLista.FECHA_INGRESO = FECHA_INGRESO;
            _experienciaParaLista.FECHA_RETIRO = FECHA_RETIRO;
            _experienciaParaLista.FUNCIONES = FUNCIONES;
            _experienciaParaLista.ID_EXPERIENCIA = ID_EXPERIENCIA;
            _experienciaParaLista.MOTIVO_RETIRO = MOTIVO_RETIRO;
            _experienciaParaLista.REGISTRO_ENTREVISTA = 0;
            _experienciaParaLista.ULTIMO_SALARIO = ULTIMO_SALARIO;

            listaExperiencia.Add(_experienciaParaLista);
        }
        String COM_F_LAB = TextBox_COM_F_LAB.Text.Trim();


        String COM_C_GEN = TextBox_CONCEPTO_GENERAL.Text.Trim();

        List<listaPruebasAplicados> listaPruebas = new List<listaPruebasAplicados>();

        for (int i = 0; i < GridView_PRUEBAS_SELECCIONADAS.Rows.Count; i++)
        {
            Decimal ID_APLICACION_PRUEBA = Convert.ToDecimal(GridView_PRUEBAS_SELECCIONADAS.DataKeys[i].Values["ID_APLICACION_PRUEBA"]);
            Decimal ID_PRUEBA = Convert.ToDecimal(GridView_PRUEBAS_SELECCIONADAS.DataKeys[i].Values["ID_PRUEBA"]);
            Decimal ID_CATEGORIA = Convert.ToDecimal(GridView_PRUEBAS_SELECCIONADAS.DataKeys[i].Values["ID_CATEGORIA"]);
            String NOM_PRUEBA = GridView_PRUEBAS_SELECCIONADAS.DataKeys[i].Values["NOM_PRUEBA"].ToString();

            TextBox textoFechaPrueba = GridView_PRUEBAS_SELECCIONADAS.Rows[i].FindControl("TextBox_FECHA_PRUEBA") as TextBox;
            DateTime FECHA_R = Convert.ToDateTime(textoFechaPrueba.Text);

            TextBox textoResultados = GridView_PRUEBAS_SELECCIONADAS.Rows[i].FindControl("TextBox_RESULTADO_PRUEBAS") as TextBox;
            String RESULTADOS = textoResultados.Text;

            FileUpload archivoCargado = GridView_PRUEBAS_SELECCIONADAS.Rows[i].FindControl("FileUpload_ARCHIVO") as FileUpload;

            Byte[] ARCHIVO = null;
            Int32 ARCHIVO_TAMANO = 0;
            String ARCHIVO_EXTENSION = null;
            String ARCHIVO_TYPE = null;
            if (archivoCargado.HasFile == true)
            {
                using (BinaryReader reader = new BinaryReader(archivoCargado.PostedFile.InputStream))
                {
                    ARCHIVO = reader.ReadBytes(archivoCargado.PostedFile.ContentLength);
                    ARCHIVO_TAMANO = archivoCargado.PostedFile.ContentLength;
                    ARCHIVO_TYPE = archivoCargado.PostedFile.ContentType;
                    ARCHIVO_EXTENSION = _tools.obtenerExtensionArchivo(archivoCargado.PostedFile.FileName);
                }
            }

            listaPruebasAplicados _pruebaParaLista = new listaPruebasAplicados();

            _pruebaParaLista.ID_SOLICITUD = ID_SOLICITUD;
            _pruebaParaLista.FECHA_R = FECHA_R;
            _pruebaParaLista.ID_CATEGORIA = ID_CATEGORIA;
            _pruebaParaLista.ID_PRUEBA = ID_PRUEBA;
            _pruebaParaLista.NOM_PRUEBA = NOM_PRUEBA;
            _pruebaParaLista.REGISTRO = ID_APLICACION_PRUEBA;
            _pruebaParaLista.RESULTADOS = RESULTADOS;
            _pruebaParaLista.ARCHIVO_PRUEBA = ARCHIVO;
            _pruebaParaLista.ARCHIVO_TAMANO = ARCHIVO_TAMANO;
            _pruebaParaLista.ARCHIVO_EXTENSION = ARCHIVO_EXTENSION;
            _pruebaParaLista.ARCHIVO_TYPE = ARCHIVO_TYPE;

            listaPruebas.Add(_pruebaParaLista);
        }


        List<AplicacionCompetencia> listaCompetencias = new List<AplicacionCompetencia>();
        if (ID_PERFIL > 0)
        {
            if (HiddenField_TIPO_ENTREVISTA.Value.Contains("A") == true)
            {
                for (int i = 0; i < GridView_CompetenciasAssesment.Rows.Count; i++)
                {
                    GridViewRow filaGrilla = GridView_CompetenciasAssesment.Rows[i];
                    AplicacionCompetencia _aplicacionParaLista = new AplicacionCompetencia();

                    CheckBox checkCumple = filaGrilla.FindControl("CheckBox_CumpleCompetencia") as CheckBox;
                    DropDownList dropNivelCalificaicon = filaGrilla.FindControl("DropDownList_NivelCumplimiento") as DropDownList;
                    CheckBox checkNoCumple = filaGrilla.FindControl("CheckBox_NoCumpeCompetencia_CheckedChanged") as CheckBox;
                    TextBox textoObservaciones = filaGrilla.FindControl("TextBox_ObservacionesCalificacion") as TextBox;

                    _aplicacionParaLista.CALIFICACION = "NO CUMPLE";
                    _aplicacionParaLista.NIVEL_CALIFICACION = 0;
                    if (checkCumple.Checked == true)
                    {
                        _aplicacionParaLista.CALIFICACION = "CUMPLE";
                        _aplicacionParaLista.NIVEL_CALIFICACION = Convert.ToInt32(dropNivelCalificaicon.SelectedValue);
                    }

                    _aplicacionParaLista.ID_APLICACION_COMPETENCIA = Convert.ToDecimal(GridView_CompetenciasAssesment.DataKeys[i].Values["ID_APLICACION_COMPETENCIA"]);
                    _aplicacionParaLista.ID_COMPETENCIA_ASSESMENT = Convert.ToDecimal(GridView_CompetenciasAssesment.DataKeys[i].Values["ID_COMPETENCIA_ASSESMENT"]);
                    _aplicacionParaLista.ID_SOLICITUD = ID_SOLICITUD;
                    _aplicacionParaLista.OBSERVACIONES = textoObservaciones.Text.Trim();

                    listaCompetencias.Add(_aplicacionParaLista);
                }
            }
        }
        else
        {
            if (CheckBox_TipoCompetencias.Checked == true)
            {
                for (int i = 0; i < GridView_CompetenciasAssesment.Rows.Count; i++)
                {
                    GridViewRow filaGrilla = GridView_CompetenciasAssesment.Rows[i];
                    AplicacionCompetencia _aplicacionParaLista = new AplicacionCompetencia();

                    CheckBox checkCumple = filaGrilla.FindControl("CheckBox_CumpleCompetencia") as CheckBox;
                    DropDownList dropNivelCalificaicon = filaGrilla.FindControl("DropDownList_NivelCumplimiento") as DropDownList;
                    CheckBox checkNoCumple = filaGrilla.FindControl("CheckBox_NoCumpeCompetencia_CheckedChanged") as CheckBox;
                    TextBox textoObservaciones = filaGrilla.FindControl("TextBox_ObservacionesCalificacion") as TextBox;

                    _aplicacionParaLista.CALIFICACION = "NO CUMPLE";
                    _aplicacionParaLista.NIVEL_CALIFICACION = 0;
                    if (checkCumple.Checked == true)
                    {
                        _aplicacionParaLista.CALIFICACION = "CUMPLE";
                        _aplicacionParaLista.NIVEL_CALIFICACION = Convert.ToInt32(dropNivelCalificaicon.SelectedValue);
                    }

                    _aplicacionParaLista.ID_APLICACION_COMPETENCIA = Convert.ToDecimal(GridView_CompetenciasAssesment.DataKeys[i].Values["ID_APLICACION_COMPETENCIA"]);
                    _aplicacionParaLista.ID_COMPETENCIA_ASSESMENT = Convert.ToDecimal(GridView_CompetenciasAssesment.DataKeys[i].Values["ID_COMPETENCIA_ASSESMENT"]);
                    _aplicacionParaLista.ID_SOLICITUD = ID_SOLICITUD;
                    _aplicacionParaLista.OBSERVACIONES = textoObservaciones.Text.Trim();

                    listaCompetencias.Add(_aplicacionParaLista);
                }
            }
        }

        hojasVida _hojasVida = new hojasVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        Boolean correcto = _hojasVida.ActualizarEntrevista(ID_SOLICITUD, FCH_ENTREVISTA, COM_C_FAM, COM_F_LAB, COM_C_ACA, COM_C_GEN, listaPruebas, ID_REQUERIMIENTO, listaComposicionFamiliar, listaFormacionAcademica, listaExperiencia, ID_ENTREVISTA, ID_PERFIL, listaCompetencias);

        if (correcto == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _hojasVida.MensajeError, Proceso.Error);
        }
        else
        {
            CargarEntrevista(ID_REQUERIMIENTO, ID_EMPRESA, ID_PERFIL);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La entrevista fue actualizada correctamente.", Proceso.Correcto);
        }
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(HiddenField_ID_ENTREVISTA.Value) == true)
        {
        }
        else
        {
            Actualizar();
        }
    }
    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Modificar);
        Mostrar(Acciones.Modificar);
        Activar(Acciones.Modificar);
        Cargar(Acciones.Modificar);
    }
    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }
    protected void DropDownList_BUSCAR_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_BUSCAR.SelectedValue == "")
        {
            configurarCaracteresAceptadosBusqueda(false, false);
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "NOMBRES")
            {
                configurarCaracteresAceptadosBusqueda(true, false);
            }
            else
            {
                if (DropDownList_BUSCAR.SelectedValue == "APELLIDOS")
                {
                    configurarCaracteresAceptadosBusqueda(true, false);
                }
                else
                {
                    if (DropDownList_BUSCAR.SelectedValue == "NUM_DOC_IDENTIDAD")
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

        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaResultadosBusqueda = new DataTable();

        if (DropDownList_BUSCAR.SelectedValue == "NOMBRES")
        {
            tablaResultadosBusqueda = _radicacionHojasDeVida.ObtenerRegSolicitudesingresoPorNombres(datosCapturados);
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "APELLIDOS")
            {
                tablaResultadosBusqueda = _radicacionHojasDeVida.ObtenerRegSolicitudesingresoPorApellidos(datosCapturados);
            }
            else
            {
                if (DropDownList_BUSCAR.SelectedValue == "NUM_DOC_IDENTIDAD")
                {
                    tablaResultadosBusqueda = _radicacionHojasDeVida.ObtenerRegSolicitudesingresoPorNumDocIdentidad(datosCapturados);
                }
            }
        }

        if (tablaResultadosBusqueda.Rows.Count <= 0)
        {
            if (_radicacionHojasDeVida.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _radicacionHojasDeVida.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros que cumplieran los datos de busqueda.", Proceso.Error);
            }
        }
        else
        {
            Panel_RESULTADOS_GRID.Visible = true;

            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaResultadosBusqueda;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
        }    
    }

    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "CON_FILTRO";

        HiddenField_FILTRO_DROP.Value = DropDownList_BUSCAR.SelectedValue;
        HiddenField_FILTRO_DATO.Value = TextBox_BUSCAR.Text;

        Buscar();
    }
    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;

        Buscar();
    }

    private void configurarInfoAdicionalModulo(Boolean ver, String texto)
    {
        Panel_INFO_ADICIONAL_MODULO.Visible = ver;
        Label_INFO_ADICIONAL_MODULO.Text = texto;
    }

    private void cargar_informacion_solicitud(DataRow informacionSolicitud)
    {
        cargo _cargo = new cargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        String ID_GRUPOS_PRIMARIOS = informacionSolicitud["ID_GRUPOS_PRIMARIOS"].ToString();
        DataTable tablaCargoSolicitud = _cargo.ObtenerGruposPrimariosPorIdGrupoPrimario(ID_GRUPOS_PRIMARIOS);

        if (tablaCargoSolicitud.Rows.Count > 0)
        {
            DataRow filatablaCargoSolicitud = tablaCargoSolicitud.Rows[0];
            Label_OCUPACION_ASPIRANTE.Text = filatablaCargoSolicitud["DESCRIPCION"].ToString().Trim();
        }
        else
        {
            Label_OCUPACION_ASPIRANTE.Text = "Desconocido";
        }

        DataTable tablaOcupacionAspira = new DataTable();
        tablaOcupacionAspira = _cargo.ObtenerOcupacionPorIdOcupacion(Convert.ToDecimal(informacionSolicitud["ID_OCUPACION"].ToString().Trim()));
        if (tablaOcupacionAspira.Rows.Count <= 0)
        {
            Label_ID_OCUPACION.Text = "Desconocido";
        }
        else
        {
            DataRow filaOcupacionAspira = tablaOcupacionAspira.Rows[0];
            Label_ID_OCUPACION.Text = filaOcupacionAspira["NOM_OCUPACION"].ToString().Trim();
        }

        Label_ARCHIVO_SOLICITUD.Text = informacionSolicitud["ARCHIVO"].ToString().Trim();

        Label_ID_SOLICITUD.Text = informacionSolicitud["ID_SOLICITUD"].ToString().Trim();
        Label_APPELIDOS.Text = informacionSolicitud["APELLIDOS"].ToString().Trim();
        Label_NOMBRES.Text = informacionSolicitud["NOMBRES"].ToString().Trim();
        Label_DOC_IDENTIDAD.Text = informacionSolicitud["TIP_DOC_IDENTIDAD"].ToString().Trim() + " " + informacionSolicitud["NUM_DOC_IDENTIDAD"].ToString().Trim();
        
        if (DBNull.Value.Equals(informacionSolicitud["NOMBRE_CIUDAD"])) Label_CIUDAD.Text = "Desconocida";
        else Label_CIUDAD.Text = informacionSolicitud["NOMBRE_CIUDAD"].ToString().Trim();

        Label_DIRECCION.Text = informacionSolicitud["DIR_ASPIRANTE"].ToString();
        Label_SECTOR.Text = informacionSolicitud["SECTOR"].ToString().Trim();
        Label_TELEFONO.Text = informacionSolicitud["TEL_ASPIRANTE"].ToString().Trim();

        try { Label_ASPIRACION_SALARIAL.Text = Convert.ToInt32(informacionSolicitud["ASPIRACION_SALARIAL"]).ToString(); }
        catch { Label_ASPIRACION_SALARIAL.Text = "Desconocido"; }
    }

    private void Activar(Acciones accion)
    {
        switch (accion)
        { 
            case Acciones.TipoEntrevista:
                RadioButtonList_TIPO_ENTREVISTA.Enabled = true;
                break;
            case Acciones.NuevaEntrvita:
                TextBox_COM_C_FAM.Enabled = true;
                TextBox_COM_C_ACA.Enabled = true;
                TextBox_COM_F_LAB.Enabled = true;
                TextBox_CONCEPTO_GENERAL.Enabled = true;

                if (String.IsNullOrEmpty(HiddenField_ID_PERFIL.Value) == false)
                {
                    
                    if (GridView_PRUEBAS_SELECCIONADAS.Rows.Count > 0)
                    {
                        HabilitarFilasGrilla(GridView_PRUEBAS_SELECCIONADAS, 0);
                        PermitirVerArchivoPrueba(GridView_PRUEBAS_SELECCIONADAS, true);
                    }

                    HabilitarFilasGrilla(GridView_CompetenciasAssesment, 0);
                }
                else
                {
                    if (GridView_SELECCION_PRUEBAS.Rows.Count > 0)
                    {
                        HabilitarFilasGrilla(GridView_SELECCION_PRUEBAS, 0);
                    }

                    CheckBox_TipoBasica.Enabled = true;
                    CheckBox_TipoCompetencias.Enabled = true;
                }
                break;
            case Acciones.Modificar:
                TextBox_COM_C_FAM.Enabled = true;
                TextBox_COM_C_ACA.Enabled = true;
                TextBox_COM_F_LAB.Enabled = true;
                TextBox_CONCEPTO_GENERAL.Enabled = true;

                if (String.IsNullOrEmpty(HiddenField_ID_PERFIL.Value) == false)
                {
                    if (GridView_PRUEBAS_SELECCIONADAS.Rows.Count > 0)
                    {
                        HabilitarFilasGrilla(GridView_PRUEBAS_SELECCIONADAS, 0);
                        PermitirVerArchivoPrueba(GridView_PRUEBAS_SELECCIONADAS, true);
                    }

                    HabilitarFilasGrilla(GridView_CompetenciasAssesment, 0);
                }
                else
                {
                    if (GridView_SELECCION_PRUEBAS.Rows.Count > 0)
                    {
                        HabilitarFilasGrilla(GridView_SELECCION_PRUEBAS, 0);
                    }

                    CheckBox_TipoBasica.Enabled = true;
                    CheckBox_TipoCompetencias.Enabled = true;
                }
                break;
        }
    }

    private Boolean VerificarSiSolicitudTieneEntrevista(Decimal ID_SOLICITUD)
    {
        hojasVida _hojasVida = new hojasVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaEntrevista = _hojasVida.ObtenerSelRegEntrevistasPorIdSolicitud(ID_SOLICITUD);

        if (tablaEntrevista.Rows.Count <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void CargarSolicitud(Decimal ID_SOLICITUD)
    {
        HiddenField_ID_SOLICITUD.Value = ID_SOLICITUD.ToString();
        HiddenField_ID_ENTREVISTA.Value = "";

        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaSolicitud = _radicacionHojasDeVida.ObtenerRegSolicitudesingresoPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));

        if (_radicacionHojasDeVida.MensajeError != null)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _radicacionHojasDeVida.MensajeError, Proceso.Error);
        }
        else
        {
            if (tablaSolicitud.Rows.Count <= 0)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información del candidato seleccionado.", Proceso.Error);
            }
            else
            {

                Ocultar(Acciones.SolicitudIngreso);
                Mostrar(Acciones.SolicitudIngreso);

                DataRow informacionSolicitud = tablaSolicitud.Rows[0];

                Page.Header.Title = informacionSolicitud["APELLIDOS"].ToString() + " " + informacionSolicitud["NOMBRES"].ToString();

                configurarInfoAdicionalModulo(true, informacionSolicitud["APELLIDOS"].ToString() + " " + informacionSolicitud["NOMBRES"].ToString());

                cargar_informacion_solicitud(informacionSolicitud);

                if ((informacionSolicitud["ARCHIVO"].ToString().ToUpper().Trim() == "DISPONIBLE") || (informacionSolicitud["ARCHIVO"].ToString().ToUpper().Trim() == "ASPIRANTE") || (informacionSolicitud["ARCHIVO"].ToString().ToUpper().Trim() == "EN CLIENTE"))
                {
                    if (informacionSolicitud["ARCHIVO"].ToString().ToUpper().Trim() == "EN CLIENTE")
                    {
                        Decimal ID_REQUISICION = Convert.ToDecimal(informacionSolicitud["ID_REQUERIMIENTO"]);

                        requisicion _requisicion = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                        DataTable tablaReq = _requisicion.ObtenerComRequerimientoPorIdRequerimiento(ID_REQUISICION);
                        DataRow filaReq = tablaReq.Rows[0];

                        Decimal ID_EMPRESA = Convert.ToDecimal(filaReq["ID_EMPRESA"]);
                        Decimal ID_PERFIL = Convert.ToDecimal(filaReq["REGISTRO_PERFIL"]);

                        CargarEntrevista(ID_REQUISICION, ID_EMPRESA, ID_PERFIL);
                    }
                    else
                    { 
                        Ocultar(Acciones.TipoEntrevista);
                        Mostrar(Acciones.TipoEntrevista);
                        Activar(Acciones.TipoEntrevista);

                        RadioButtonList_TIPO_ENTREVISTA.ClearSelection();

                        if (informacionSolicitud["ARCHIVO"].ToString().ToUpper().Trim() == "DISPONIBLE")
                        {
                            if (VerificarSiSolicitudTieneEntrevista(ID_SOLICITUD) == true)
                            {
                                Button_IMPRIMIR.Visible = true;
                            }
                        }
                    }
                }
                else
                {
                    if ((informacionSolicitud["ARCHIVO"].ToString().ToUpper().Trim() == "CONTRATADO") || (informacionSolicitud["ARCHIVO"].ToString().ToUpper().Trim() == "POR CONTRATAR"))
                    {
                        Decimal ID_REQUISICION = Convert.ToDecimal(informacionSolicitud["ID_REQUERIMIENTO"]);

                        requisicion _requisicion = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                        DataTable tablaReq = _requisicion.ObtenerComRequerimientoPorIdRequerimiento(ID_REQUISICION);
                        DataRow filaReq = tablaReq.Rows[0];

                        Decimal ID_EMPRESA = Convert.ToDecimal(filaReq["ID_EMPRESA"]);
                        Decimal ID_PERFIL = Convert.ToDecimal(filaReq["REGISTRO_PERFIL"]);

                        CargarEntrevista(ID_REQUISICION, ID_EMPRESA, ID_PERFIL);
                    }
                    else
                    {
                        Ocultar(Acciones.TipoEntrevista);
                        Desactivar(Acciones.TipoEntrevista);
                    }
                }
            }
        }
    }

    protected void GridView_RESULTADOS_BUSQUEDA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal ID_SOLICITUD = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_SOLICITUD"]);

            CargarSolicitud(ID_SOLICITUD);
        }
    }

    private void cargar_DropDownList_EMPRESA_ASPIRANTE()
    {
        DropDownList_EMPRESA_ASPIRANTE.Items.Clear();

        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaEmpresasActivas = _cliente.ObtenerTodasLasEmpresasActivas();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_EMPRESA_ASPIRANTE.Items.Add(item);

        foreach (DataRow fila in tablaEmpresasActivas.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["RAZ_SOCIAL"].ToString(), fila["ID_EMPRESA"].ToString());
            DropDownList_EMPRESA_ASPIRANTE.Items.Add(item);
        }

        DropDownList_EMPRESA_ASPIRANTE.DataBind();
    }

    protected void RadioButtonList_TIPO_ENTREVISTA_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(RadioButtonList_TIPO_ENTREVISTA.SelectedValue == "SIN")
        {
            Panel_TIPO_SIN_REQUISICION.Visible = true;
            Panel_TIPO_CON_REQUISICION.Visible = false;
        }
        else
        {
            Panel_TIPO_SIN_REQUISICION.Visible = false;
            Panel_TIPO_CON_REQUISICION.Visible = true;
            
            DropDownList_EMPRESA_ASPIRANTE.Enabled = true;
            cargar_DropDownList_EMPRESA_ASPIRANTE();

            GridView_REQ.DataSource = null;
            GridView_REQ.DataBind();
        }
    }
    protected void Button_SELECCIONAR_SIN_REQUISISCION_Click(object sender, EventArgs e)
    {
        HiddenField_ID_EMPRESA.Value = "";
        HiddenField_ID_ENTREVISTA.Value = "";
        HiddenField_ID_PERFIL.Value = "";
        HiddenField_ID_REQUISICION.Value = "";

        CargarEntrevista(0, 0, 0);
    }
    protected void DropDownList_EMPRESA_ASPIRANTE_SelectedIndexChanged(object sender, EventArgs e)
    {
        requisicion _requisicion = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        Decimal ID_EMPRESA = Convert.ToDecimal(DropDownList_EMPRESA_ASPIRANTE.SelectedValue);

        DataTable tablaResultadosBusqueda = new DataTable();

        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoEmpresaSeleccionada = _cliente.ObtenerEmpresaConIdEmpresa(ID_EMPRESA);

        if (tablaInfoEmpresaSeleccionada.Rows.Count <= 0)
        {
            Informar(Panel_FONDO_MENSAJE_TIPO_ENTREVISTA, Image_MENSAJE_TIPO_ENTREVISTA_POPUP, Panel_MENSAJES_TIPO_ENTREVISTA, Label_MENSAJE, "No se encontraron datos para la empresa seleccionada.", Proceso.Advertencia);

            GridView_REQ.DataSource = null;
            GridView_REQ.DataBind();
        }
        else
        {
            DataRow filaInfoEmpresaSeleccionada = tablaInfoEmpresaSeleccionada.Rows[0];
            String COD_EMPRESA = filaInfoEmpresaSeleccionada["COD_EMPRESA"].ToString().Trim();

            tablaResultadosBusqueda = _requisicion.ObtenerComRequerimientoPorIdEmpresaEstadoReq(ID_EMPRESA, "N", "N");

            GridView_REQ.DataSource = tablaResultadosBusqueda;
            GridView_REQ.DataBind();
        }
    }
    protected void Button_GENERAR_RESULTADOS_PRUEBAS_Click(object sender, EventArgs e)
    {
        int contadorSeleciconados = 0;
        for (int i = 0; i < GridView_SELECCION_PRUEBAS.Rows.Count; i++)
        {
            GridViewRow filaGrid = GridView_SELECCION_PRUEBAS.Rows[i];
            CheckBox check = filaGrid.FindControl("CheckBox_PRUEBA") as CheckBox;

            if (check.Checked == true)
            {
                contadorSeleciconados += 1;
            }
        }

        if (contadorSeleciconados <= 0)
        {
            Informar(Panel_FONDO_MENSAJE_PRUEBAS, Image_MENSAJE_PRUEBAS_POPUP, Panel_MENSAJES_PRUEBAS, Label_MENSAJE_PRUEBAS, "No se seleccionó ninguna prueba de la lista. Debe seleccionar por lo menos una prueba.", Proceso.Advertencia);
        }
        else
        {
            Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);

            DataTable tablaParaGrilla = ObtenerEstructuraTablaGrillaResultados();

            DataTable tablaPruebas = new DataTable();
            tablaPruebas.Columns.Add("ID_PRUEBA");
            tablaPruebas.Columns.Add("NOM_PRUEBA");
            tablaPruebas.Columns.Add("ID_CATEGORIA");

            for (int i = 0; i < GridView_SELECCION_PRUEBAS.Rows.Count; i++)
            {
                GridViewRow filaGrid = GridView_SELECCION_PRUEBAS.Rows[i];
                CheckBox check = filaGrid.FindControl("CheckBox_PRUEBA") as CheckBox;

                if (check.Checked == true)
                {
                    DataRow filaNueva = tablaPruebas.NewRow();
                    filaNueva["ID_PRUEBA"] = Convert.ToDecimal(GridView_SELECCION_PRUEBAS.DataKeys[i].Values["ID_PRUEBA"]);
                    filaNueva["NOM_PRUEBA"] = GridView_SELECCION_PRUEBAS.DataKeys[i].Values["NOM_PRUEBA"].ToString();
                    filaNueva["ID_CATEGORIA"] = Convert.ToDecimal(GridView_SELECCION_PRUEBAS.DataKeys[i].Values["ID_CATEGORIA"]);
                    tablaPruebas.Rows.Add(filaNueva);
                }
            }

            tablaPruebas.AcceptChanges();

            for (int i = 0; i < tablaPruebas.Rows.Count; i++)
            {
                Decimal ID_APLICACION_PRUEBA = 0;

                DataRow filaPrueba = tablaPruebas.Rows[i];
                Decimal ID_PRUEBA = Convert.ToDecimal(filaPrueba["ID_PRUEBA"]);
                String NOM_PRUEBA = filaPrueba["NOM_PRUEBA"].ToString().Trim();
                Decimal ID_CATEGORIA = Convert.ToDecimal(filaPrueba["ID_CATEGORIA"]);

                String FECHA_R = String.Empty;
                String RESULTADOS = String.Empty;
                String TIENE_ARCHIVO = "N";

                hojasVida _hojasVida = new hojasVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                DataTable tablaPruebaAplicadaYa = _hojasVida.ObtenerSelRegAplicacionPrueebasObtenerPorIdSolicitudIdPrueba(ID_SOLICITUD, ID_PRUEBA);

                if (tablaPruebaAplicadaYa.Rows.Count > 0)
                {
                    DataRow filaPruebaAplicadaYa = tablaPruebaAplicadaYa.Rows[0];
                    ID_APLICACION_PRUEBA = Convert.ToDecimal(filaPruebaAplicadaYa["REGISTRO"]);
                    try
                    {
                        FECHA_R = Convert.ToDateTime(filaPruebaAplicadaYa["FECHA_R"]).ToShortDateString();
                    }
                    catch
                    {
                        FECHA_R = String.Empty;
                    }

                    RESULTADOS = filaPruebaAplicadaYa["RESULTADOS"].ToString().Trim();

                    if (filaPruebaAplicadaYa["ARCHIVO_PRUEBA"] != DBNull.Value)
                    {
                        TIENE_ARCHIVO = "S";
                    }
                }

                DataRow filaParaGrilla = tablaParaGrilla.NewRow();

                filaParaGrilla["ID_APLICACION_PRUEBA"] = ID_APLICACION_PRUEBA;
                filaParaGrilla["ID_PRUEBA"] = ID_PRUEBA;
                filaParaGrilla["NOM_PRUEBA"] = NOM_PRUEBA;
                filaParaGrilla["ID_CATEGORIA"] = ID_CATEGORIA;
                filaParaGrilla["FECHA_R"] = FECHA_R;
                filaParaGrilla["RESULTADOS"] = RESULTADOS;
                filaParaGrilla["TIENE_IMAGEN"] = TIENE_ARCHIVO;

                tablaParaGrilla.Rows.Add(filaParaGrilla);
            }

            CargarGrillaPruebasAplicarDesdeTabla(tablaParaGrilla);

            Panel_GRILLA_SELECCION_PRUEBAS.Visible = false;
            Mostrar(Acciones.PruebasAAplicar);

            Panel_PRUEBAS_SELECCIONADAS.Focus();

        }
    }

    private void GenerarPDFEntrevista()
    {
        Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);
        Decimal ID_PERFIL = 0;

        if(String.IsNullOrEmpty(HiddenField_ID_PERFIL.Value) == false)
        {
            ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);
        }

        radicacionHojasDeVida _radicacionHojasVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaSolicitud = _radicacionHojasVida.ObtenerRegSolicitudesingresoPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));
        DataRow filaSolicitud = tablaSolicitud.Rows[0];

        maestrasInterfaz _maestras = new maestrasInterfaz();

        byte[] archivoEntrevista = _maestras.GenerarPDFEntrevista(ID_SOLICITUD, ID_PERFIL);

        String filename = filaSolicitud["NUM_DOC_IDENTIDAD"].ToString().Trim() + "INFORME_SELECCION";
        filename = filename.Replace(' ', '_');

        Response.Clear(); 
        Response.BufferOutput = false; 
        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", "attachment;FileName=" + filename + ".pdf");
        Response.BinaryWrite(archivoEntrevista);
        Response.End();
    }

    protected void Button_IMPRIMIR_Click(object sender, EventArgs e)
    {
        GenerarPDFEntrevista();
    }
    protected void Button_VOLVER_Click(object sender, EventArgs e)
    {

    }
    protected void Button_documentos_Click(object sender, EventArgs e)
    {
        Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);

        Decimal ID_REQUERIMIENTO = 0;
        if (String.IsNullOrEmpty(HiddenField_ID_REQUISICION.Value) == false)
        {
            ID_REQUERIMIENTO = Convert.ToDecimal(HiddenField_ID_REQUISICION.Value);
        }

        tools tool = new tools();
        SecureQueryString QueryStringSeguros;
        QueryStringSeguros = new SecureQueryString(tool.byteParaQueryStringSeguro(), Request["data"]);

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = "seleccion";
        QueryStringSeguro["nombre_area"] = "SELECCIÓN Y REQUISICIONES";
        QueryStringSeguro["nombre_modulo"] = "VALIDACIÓN DE DOCUMENTOS";
        QueryStringSeguro["accion"] = "cargar";
        QueryStringSeguro["solicitud"] = ID_SOLICITUD.ToString();
        if (ID_REQUERIMIENTO > 0)
        {
            QueryStringSeguro["requerimiento"] = ID_REQUERIMIENTO.ToString();
        }
        else
        {
            QueryStringSeguro["requerimiento"] = null;
        }

        Response.Redirect("~/seleccion/requisitos.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
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

    private void CargarRegistroControlEntrevista(DataRow filaEntrevista)
    {
        TextBox_USU_CRE.Text = filaEntrevista["USU_CRE"].ToString();
        try
        {
            TextBox_FCH_CRE.Text = DateTime.Parse(filaEntrevista["FCH_CRE"].ToString()).ToShortDateString();
            TextBox_HOR_CRE.Text = DateTime.Parse(filaEntrevista["FCH_CRE"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_CRE.Text = "";
            TextBox_HOR_CRE.Text = "";
        }
        TextBox_USU_MOD.Text = filaEntrevista["USU_MOD"].ToString();
        try
        {
            TextBox_FCH_MOD.Text = DateTime.Parse(filaEntrevista["FCH_MOD"].ToString()).ToShortDateString();
            TextBox_HOR_MOD.Text = DateTime.Parse(filaEntrevista["FCH_MOD"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_MOD.Text = "";
            TextBox_HOR_MOD.Text = "";
        }
    }

    private DataRow obtenerDatosCiudadViveFamiliar(String idCiudad)
    {
        DataRow resultado = null;

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());

        DataTable tablaIdDepartamento = _ciudad.ObtenerIdDepartamentoConIdCiudad(idCiudad);

        if (tablaIdDepartamento.Rows.Count > 0)
        {
            resultado = tablaIdDepartamento.Rows[0];
        }

        return resultado;
    }

    private void cargar_DropDownList_DEPARTAMENTO(DropDownList drop)
    {
        drop.Items.Clear();

        departamento _departamento = new departamento(Session["idEmpresa"].ToString());
        DataTable tablaDepartamentos = _departamento.ObtenerTodasLosDepartamentos();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        drop.Items.Add(item);

        foreach (DataRow fila in tablaDepartamentos.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOMBRE"].ToString(), fila["ID_DEPARTAMENTO"].ToString());
            drop.Items.Add(item);
        }

        drop.DataBind();
    }

    private void cargar_DropDownList_CIUDAD(String idDepartamento, DropDownList drop)
    {
        drop.Items.Clear();

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
        DataTable tablaCiudades = _ciudad.ObtenerCiudadesPorIdDepartamento(idDepartamento);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione Ciudad", "");
        drop.Items.Add(item);

        foreach (DataRow fila in tablaCiudades.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOMBRE"].ToString(), fila["ID_CIUDAD"].ToString());
            drop.Items.Add(item);
        }

        drop.DataBind();
    }

    private void Cargar_Grilla_Composicionfamiliar_Desdetabla(DataTable tablaComposicion)
    {
        tools _tools = new tools();

        GridView_ComposicionFamiliar.DataSource = tablaComposicion;
        GridView_ComposicionFamiliar.DataBind();

        for (int i = 0; i < GridView_ComposicionFamiliar.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ComposicionFamiliar.Rows[i];
            DataRow filaTabla = tablaComposicion.Rows[i];

            DropDownList droptipoFamiliar = filaGrilla.FindControl("DropDownList_TipoFamiliar") as DropDownList;
            Cargar_DropDownList_TipoFamiliar(droptipoFamiliar);
            droptipoFamiliar.SelectedValue = filaTabla["ID_TIPO_FAMILIAR"].ToString().Trim();

            TextBox textoNombres = filaGrilla.FindControl("TextBox_NombresFamiliar") as TextBox;
            textoNombres.Text = filaTabla["NOMBRES"].ToString().Trim();

            TextBox textoApellidos = filaGrilla.FindControl("TextBox_ApellidosFamiliar") as TextBox;
            textoApellidos.Text = filaTabla["APELLIDOS"].ToString().Trim();

            TextBox textoFechaFamiliar = filaGrilla.FindControl("TextBox_FechaNacimientoFamiliar") as TextBox;
            Label labelEdad = filaGrilla.FindControl("Label_Edad") as Label;
            if (String.IsNullOrEmpty(filaTabla["FECHA_NACIMIENTO"].ToString().Trim()) == false)
            {
                textoFechaFamiliar.Text = Convert.ToDateTime(filaTabla["FECHA_NACIMIENTO"]).ToShortDateString();

                try
                {
                    labelEdad.Text = "Edad: " + _tools.ObtenerEdadDesdeFechaNacimiento(Convert.ToDateTime(filaTabla["FECHA_NACIMIENTO"])).ToString();
                }
                catch
                {
                    labelEdad.Text = "Edad: Desconocida.";
                }
            }
            else
            {
                textoFechaFamiliar.Text = "";
                labelEdad.Text = "Edad: Desconocida.";
            }

            TextBox textoProfesion = filaGrilla.FindControl("TextBox_ProfesionFamiliar") as TextBox;
            textoProfesion.Text = filaTabla["PROFESION"].ToString().Trim();

            CheckBox checkExtranjero = filaGrilla.FindControl("CheckBox_Extranjero") as CheckBox;
            Panel panelViveEn = filaGrilla.FindControl("Panel_ViveEn") as Panel;
            DropDownList dropDepartamento = filaGrilla.FindControl("DropDownList_DepartamentoFamiliar") as DropDownList;
            DropDownList dropCiudad = filaGrilla.FindControl("DropDownList_CiudadFamiliar") as DropDownList;
            if (String.IsNullOrEmpty(filaTabla["ID_CIUDAD"].ToString().Trim()) == false)
            {
                if (filaTabla["ID_CIUDAD"].ToString().Trim() == "EXTRA")
                {
                    checkExtranjero.Checked = true;
                    panelViveEn.Visible = false;
                    cargar_DropDownList_DEPARTAMENTO(dropDepartamento);
                    dropCiudad.Items.Clear();
                }
                else
                {
                    DataRow filaInfoCiudadYDepartamento = obtenerDatosCiudadViveFamiliar(filaTabla["ID_CIUDAD"].ToString().Trim());
                    cargar_DropDownList_DEPARTAMENTO(dropDepartamento);
                    dropDepartamento.SelectedValue = filaInfoCiudadYDepartamento["ID_DEPARTAMENTO"].ToString().Trim();
                    cargar_DropDownList_CIUDAD(filaInfoCiudadYDepartamento["ID_DEPARTAMENTO"].ToString().Trim(), dropCiudad);
                    dropCiudad.SelectedValue = filaInfoCiudadYDepartamento["ID_CIUDAD"].ToString().Trim();
                }
            }
            else
            {
                checkExtranjero.Checked = false;
                panelViveEn.Visible = true;
                cargar_DropDownList_DEPARTAMENTO(dropDepartamento);
                dropCiudad.Items.Clear();
            }

            CheckBox check = filaGrilla.FindControl("CheckBox_ViveConElFamiliar") as CheckBox;
            if (filaTabla["VIVE_CON_EL"].ToString().Trim().ToUpper() == "TRUE")
            {
                check.Checked = true;
            }
            else
            {
                check.Checked = false;
            }
        }
    }

    private void CargarComposicionFamiliar(Decimal ID_ENTREVISTA)
    {
        hojasVida _hojasVida = new hojasVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablainfofamiliar = _hojasVida.ObtenerSelRegComposicionFamiliar(ID_ENTREVISTA);

        if (tablainfofamiliar.Rows.Count <= 0)
        {
            if (_hojasVida.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _hojasVida.MensajeError, Proceso.Error);
            }

            GridView_ComposicionFamiliar.DataSource = null;
            GridView_ComposicionFamiliar.DataBind();
        }
        else
        {
            Cargar_Grilla_Composicionfamiliar_Desdetabla(tablainfofamiliar);
        }
    }

    private void cargar_DropDownList_NIV_EDUCACION(DropDownList drop)
    {
        drop.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerNivEstudiosParametros();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        drop.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            drop.Items.Add(item);
        }

        drop.DataBind();
    }

    private void cargar_DropDownList_UnidadDuracion(DropDownList drop)
    {
        drop.Items.Clear();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        drop.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("HORAS", "HORAS");
        drop.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("DIAS", "DIAS");
        drop.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("SEMANAS", "SEMANAS");
        drop.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("MESES", "MESES");
        drop.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("AÑOS", "AÑOS");
        drop.Items.Add(item);

        drop.DataBind();
    }

    private void Cargar_Grilla_informacionEducativaFormal_Desdetabla(DataTable tablainformacion)
    {
        GridView_EducacionFormal.DataSource = tablainformacion;
        GridView_EducacionFormal.DataBind();

        for (int i = 0; i < GridView_EducacionFormal.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_EducacionFormal.Rows[i];
            DataRow filaTabla = tablainformacion.Rows[i];

            DropDownList droptipoNivEducacion = filaGrilla.FindControl("DropDownList_NivAcademico") as DropDownList;
            cargar_DropDownList_NIV_EDUCACION(droptipoNivEducacion);
            droptipoNivEducacion.SelectedValue = filaTabla["NIVEL_ACADEMICO"].ToString().Trim();

            TextBox textoInstitucion = filaGrilla.FindControl("TextBox_Institucion") as TextBox;
            textoInstitucion.Text = filaTabla["INSTITUCION"].ToString().Trim();

            TextBox textoAnno = filaGrilla.FindControl("TextBox_Anno") as TextBox;
            textoAnno.Text = filaTabla["ANNO"].ToString().Trim();

            TextBox textoobservaciones = filaGrilla.FindControl("TextBox_Observaciones") as TextBox;
            textoobservaciones.Text = filaTabla["OBSERVACIONES"].ToString().Trim();
        }
    }

    private void Cargar_Grilla_informacionEducativaNoFormal_Desdetabla(DataTable tablainformacion)
    {
        GridView_EducacionNoFormal.DataSource = tablainformacion;
        GridView_EducacionNoFormal.DataBind();

        for (int i = 0; i < GridView_EducacionNoFormal.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_EducacionNoFormal.Rows[i];
            DataRow filaTabla = tablainformacion.Rows[i];

            TextBox textoCurso = filaGrilla.FindControl("TextBox_Curso") as TextBox;
            textoCurso.Text = filaTabla["CURSO"].ToString().Trim();
            
            TextBox textoInstitucion = filaGrilla.FindControl("TextBox_Institucion") as TextBox;
            textoInstitucion.Text = filaTabla["INSTITUCION"].ToString().Trim();

            TextBox textoDuracion = filaGrilla.FindControl("TextBox_Duracion") as TextBox;
            if (String.IsNullOrEmpty(filaTabla["DURACION"].ToString().Trim()) == false)
            {
                textoDuracion.Text = Convert.ToDecimal(filaTabla["DURACION"]).ToString();
            }
            else
            {
                textoDuracion.Text = "";
            }
            
            DropDownList droptipoUnidadDuracion = filaGrilla.FindControl("DropDownList_UnidadDuracion") as DropDownList;
            cargar_DropDownList_UnidadDuracion(droptipoUnidadDuracion);
            droptipoUnidadDuracion.SelectedValue = filaTabla["UNIDAD_DURACION"].ToString().Trim();

            TextBox textoobservaciones = filaGrilla.FindControl("TextBox_Observaciones") as TextBox;
            textoobservaciones.Text = filaTabla["OBSERVACIONES"].ToString().Trim();
        }
    }

    private void Cargar_Grilla_ExperienciaLaboral_Desdetabla(DataTable tablainformacion)
    {
        GridView_ExperienciaLaboral.DataSource = tablainformacion;
        GridView_ExperienciaLaboral.DataBind();

        for (int i = 0; i < GridView_ExperienciaLaboral.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ExperienciaLaboral.Rows[i];
            DataRow filaTabla = tablainformacion.Rows[i];

            TextBox textoEmpresa = filaGrilla.FindControl("TextBox_Empresa") as TextBox;
            textoEmpresa.Text = filaTabla["EMPRESA"].ToString().Trim();

            TextBox textoInstitucion = filaGrilla.FindControl("TextBox_Cargo") as TextBox;
            textoInstitucion.Text = filaTabla["CARGO"].ToString().Trim();

            TextBox textoFunciones = filaGrilla.FindControl("TextBox_FuncionesRealizadas") as TextBox;
            textoFunciones.Text = filaTabla["FUNCIONES"].ToString().Trim();

            TextBox textoFechaInicio = filaGrilla.FindControl("TextBox_FechaIngreso") as TextBox;
            if (String.IsNullOrEmpty(filaTabla["FECHA_INGRESO"].ToString().Trim()) == false)
            {
                textoFechaInicio.Text = Convert.ToDateTime(filaTabla["FECHA_INGRESO"]).ToShortDateString();
            }
            else
            {
                textoFechaInicio.Text = "";
            }

            TextBox textoFechaRetiro = filaGrilla.FindControl("TextBox_FechaRetiro") as TextBox;
            if (String.IsNullOrEmpty(filaTabla["FECHA_RETIRO"].ToString().Trim()) == false)
            {
                textoFechaRetiro.Text = Convert.ToDateTime(filaTabla["FECHA_RETIRO"]).ToShortDateString();
            }
            else
            {
                textoFechaRetiro.Text = "";
            }

            Label labelTiempoTrabajado = filaGrilla.FindControl("Label_TiempoTrabajado") as Label;

            Boolean correcto = true;
            DateTime fechaIngreso;
            DateTime fechaRetiro;
            try
            {
                fechaIngreso = Convert.ToDateTime(textoFechaInicio.Text);
            }
            catch
            {
                correcto = false;
                fechaIngreso = new DateTime();
            }

            if (correcto == true)
            {
                Boolean conContratoVigente = true;
                try
                {
                    fechaRetiro = Convert.ToDateTime(textoFechaRetiro.Text);
                    conContratoVigente = false;
                }
                catch
                {
                    conContratoVigente = true;
                    fechaRetiro = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                }

                if (fechaRetiro < fechaIngreso)
                {
                    labelTiempoTrabajado.Text = "Error en fechas.";
                }
                else
                {
                    tools _tools = new tools();

                    if (conContratoVigente == true)
                    {
                        labelTiempoTrabajado.Text = "Lleva trabajando: " + _tools.DiferenciaFechas(fechaRetiro, fechaIngreso);
                    }
                    else
                    {
                        labelTiempoTrabajado.Text = "Trabajó: " + _tools.DiferenciaFechas(fechaRetiro, fechaIngreso);
                    }
                }
            }
            else
            {
                labelTiempoTrabajado.Text = "Tiempo Desconocido.";
            }



            DropDownList dropMotivoRetiro = filaGrilla.FindControl("DropDownList_MotivoRetiro") as DropDownList;
            try
            {
                dropMotivoRetiro.SelectedValue = filaTabla["MOTIVO_RETIRO"].ToString().Trim();
            }
            catch
            {
                dropMotivoRetiro.SelectedIndex = 0;
            }
            

            TextBox textoUltimoSalario = filaGrilla.FindControl("TextBox_Ultimosalario") as TextBox;
            textoUltimoSalario.Text = filaTabla["ULTIMO_SALARIO"].ToString();
        }
    }

    private void CargarEducacionFormal(Decimal ID_ENTREVISTA)
    {
        hojasVida _hojasVida = new hojasVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaEducacion = _hojasVida.ObtenerSelRegInformacionAcademica(ID_ENTREVISTA, "FORMAL");

        if (tablaEducacion.Rows.Count <= 0)
        {
            if (_hojasVida.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _hojasVida.MensajeError, Proceso.Error);
            }

            GridView_EducacionFormal.DataSource = null;
            GridView_EducacionFormal.DataBind();
        }
        else
        {
            Cargar_Grilla_informacionEducativaFormal_Desdetabla(tablaEducacion);
        }
    }

    private void CargarEducacionNoFormal(Decimal ID_ENTREVISTA)
    {
        hojasVida _hojasVida = new hojasVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaEducacion = _hojasVida.ObtenerSelRegInformacionAcademica(ID_ENTREVISTA, "NO FORMAL");

        if (tablaEducacion.Rows.Count <= 0)
        {
            if (_hojasVida.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _hojasVida.MensajeError, Proceso.Error);
            }

            GridView_EducacionNoFormal.DataSource = null;
            GridView_EducacionNoFormal.DataBind();
        }
        else
        {
            Cargar_Grilla_informacionEducativaNoFormal_Desdetabla(tablaEducacion);
        }
    }

    private void CargarExperienciaLaboral(Decimal ID_ENTREVISTA)
    {
        hojasVida _hojasVida = new hojasVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaExperiencia = _hojasVida.ObtenerSelRegExperienciaLaboral(ID_ENTREVISTA);

        if (tablaExperiencia.Rows.Count <= 0)
        {
            if (_hojasVida.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _hojasVida.MensajeError, Proceso.Error);
            }

            GridView_ExperienciaLaboral.DataSource = null;
            GridView_ExperienciaLaboral.DataBind();
        }
        else
        {
            Cargar_Grilla_ExperienciaLaboral_Desdetabla(tablaExperiencia);
        }
    }

    private void CargarDatosBasicosEntrevista(DataRow filaEntrevista)
    {
        HiddenField_ID_ENTREVISTA.Value = filaEntrevista["REGISTRO"].ToString();

        Decimal ID_ENTREVISTA = Convert.ToDecimal(HiddenField_ID_ENTREVISTA.Value);

        //String archivoEntrevista = TieneEntrevistaArchivadaElCandidato(ID_ENTREVISTA);
        //if (String.IsNullOrEmpty(archivoEntrevista) == false)
        //{
        //    Mostrar(Acciones.ConArchivoWord);
        //    HyperLink_ARCHIVO_WORD_ENTREVISTA.NavigateUrl = archivoEntrevista;
        //}

        CargarRegistroControlEntrevista(filaEntrevista);

        try
        {
            TextBox_FCH_ENTREVISTA.Text = Convert.ToDateTime(filaEntrevista["FCH_ENTREVISTA"]).ToShortDateString();
        }
        catch
        {
            TextBox_FCH_ENTREVISTA.Text = DateTime.Now.ToShortDateString();
        }

        CargarComposicionFamiliar(ID_ENTREVISTA);
        inhabilitarFilasGrilla(GridView_ComposicionFamiliar, 2);
        TextBox_COM_C_FAM.Text = filaEntrevista["COM_C_FAM"].ToString().Trim();

        CargarEducacionFormal(ID_ENTREVISTA);
        inhabilitarFilasGrilla(GridView_EducacionFormal, 2);
        CargarEducacionNoFormal(ID_ENTREVISTA);
        inhabilitarFilasGrilla(GridView_EducacionNoFormal, 2);
        TextBox_COM_C_ACA.Text = filaEntrevista["COM_C_ACA"].ToString().Trim();
        
        CargarExperienciaLaboral(ID_ENTREVISTA);
        inhabilitarFilasGrilla(GridView_ExperienciaLaboral, 2);
        TextBox_COM_F_LAB.Text = filaEntrevista["COM_F_LAB"].ToString().Trim();

        TextBox_CONCEPTO_GENERAL.Text = filaEntrevista["COM_C_GEN"].ToString().Trim();
    }

    ////private String TieneEntrevistaArchivadaElCandidato(Decimal ID_ENTREVISTA)
    ////{
    ////    String resultado = null;

    ////    Boolean archivoEncontrado = false;

    ////    String[] listaArchivos;
    ////    if (Session["idEmpresa"].ToString() == "1")
    ////    {
    ////        listaArchivos = System.IO.Directory.GetFiles(Server.MapPath("~/entrevistas/varu"), "*.doc");
    ////        resultado = "~/entrevistas/varu/";
    ////    }

    ////    foreach (String archivo in listaArchivos)
    ////    {
    ////        if (archivo.IndexOf("gv_" + ID_ENTREVISTA.ToString()) > 0)
    ////        {
    ////            resultado += "gv_" + ID_ENTREVISTA.ToString() + ".DOC";
    ////            archivoEncontrado = true;
    ////            break;
    ////        }
    ////    }

    ////    if (archivoEncontrado == false)
    ////    {
    ////        resultado = null;
    ////    }

    ////    return resultado;
    ////}

    private void cargar_GridView_SELECCION_PRUEBAS()
    {
        categoriaPruebas _categoriaPruebas = new categoriaPruebas(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaPruebas = _categoriaPruebas.ObtenerTodosLasPruebasMasNomcategoria();

        GridView_SELECCION_PRUEBAS.DataSource = tablaPruebas;
        GridView_SELECCION_PRUEBAS.DataBind();
    }

    private DataTable ObtenerEstructuraTablaGrillaResultados()
    {
        DataTable tablaResultado = new DataTable();

        tablaResultado.Columns.Add("ID_APLICACION_PRUEBA");
        tablaResultado.Columns.Add("ID_PRUEBA");
        tablaResultado.Columns.Add("NOM_PRUEBA");
        tablaResultado.Columns.Add("ID_CATEGORIA");
        tablaResultado.Columns.Add("FECHA_R");
        tablaResultado.Columns.Add("RESULTADOS");
        tablaResultado.Columns.Add("TIENE_IMAGEN");

        tablaResultado.AcceptChanges();

        return tablaResultado;
    }

    private void CargarGrillaPruebasAplicarDesdeTabla(DataTable tablaPruebas)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        GridView_PRUEBAS_SELECCIONADAS.DataSource = tablaPruebas;
        GridView_PRUEBAS_SELECCIONADAS.DataBind();

        for (int i = 0; i < GridView_PRUEBAS_SELECCIONADAS.Rows.Count; i++)
        {
            DataRow filaTabla = tablaPruebas.Rows[i];

            TextBox textoFecha = GridView_PRUEBAS_SELECCIONADAS.Rows[i].FindControl("TextBox_FECHA_PRUEBA") as TextBox;

            try
            {
                textoFecha.Text = Convert.ToDateTime(filaTabla["FECHA_R"]).ToShortDateString();
            }
            catch
            {
                textoFecha.Text = "";
            }

            TextBox textoResultados = GridView_PRUEBAS_SELECCIONADAS.Rows[i].FindControl("TextBox_RESULTADO_PRUEBAS") as TextBox;

            textoResultados.Text = filaTabla["RESULTADOS"].ToString().Trim();

            HyperLink link = GridView_PRUEBAS_SELECCIONADAS.Rows[i].FindControl("HyperLink_ARCHIVO") as HyperLink;

            if (filaTabla["TIENE_IMAGEN"].ToString() == "S")
            {
                link.Target = "_blank";

                QueryStringSeguro["registro"] = filaTabla["ID_APLICACION_PRUEBA"].ToString();

                link.NavigateUrl = "~/seleccion/visorArchivoPrueba.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());

                link.Visible = true;
                link.Enabled = true;
            }
            else
            {
                link.Visible = false;
                link.Enabled = false;
                link.NavigateUrl = null;
            }
        }
    }

    private void CargarPruebasAsociadasAPerfil(Decimal ID_PERFIL)
    {
        Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);

        DataTable tablaParaGrilla = ObtenerEstructuraTablaGrillaResultados();
        
        pruebaPerfil _pruebaPerfil = new pruebaPerfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaPruebasPerfil = _pruebaPerfil.ObtenerPorIdPerfil(ID_PERFIL);

        for (int i = 0; i < tablaPruebasPerfil.Rows.Count; i++)
        {
            Decimal ID_APLICACION_PRUEBA = 0;

            DataRow filaPrueba = tablaPruebasPerfil.Rows[i];
            Decimal ID_PRUEBA = Convert.ToDecimal(filaPrueba["ID_PRUEBA"]);
            String NOM_PRUEBA = filaPrueba["NOM_PRUEBA"].ToString().Trim();
            Decimal ID_CATEGORIA = Convert.ToDecimal(filaPrueba["ID_CATEGORIA"]);

            String FECHA_R = String.Empty;
            String RESULTADOS = String.Empty;
            String TIENE_ARCHIVO = "N";

            hojasVida _hojasVida = new hojasVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaPruebaAplicadaYa = _hojasVida.ObtenerSelRegAplicacionPrueebasObtenerPorIdSolicitudIdPrueba(ID_SOLICITUD, ID_PRUEBA);

            if (tablaPruebaAplicadaYa.Rows.Count > 0)
            {
                DataRow filaPruebaAplicadaYa = tablaPruebaAplicadaYa.Rows[0];
                ID_APLICACION_PRUEBA = Convert.ToDecimal(filaPruebaAplicadaYa["REGISTRO"]);
                try
                {
                    FECHA_R = Convert.ToDateTime(filaPruebaAplicadaYa["FECHA_R"]).ToShortDateString();
                }
                catch
                {
                    FECHA_R = String.Empty;
                }

                RESULTADOS = filaPruebaAplicadaYa["RESULTADOS"].ToString().Trim();

                if (filaPruebaAplicadaYa["ARCHIVO_PRUEBA"] != DBNull.Value)
                {
                    TIENE_ARCHIVO = "S";
                }
            }

            DataRow filaParaGrilla = tablaParaGrilla.NewRow();

            filaParaGrilla["ID_APLICACION_PRUEBA"] = ID_APLICACION_PRUEBA;
            filaParaGrilla["ID_PRUEBA"] = ID_PRUEBA;
            filaParaGrilla["NOM_PRUEBA"] = NOM_PRUEBA;
            filaParaGrilla["ID_CATEGORIA"] = ID_CATEGORIA;
            filaParaGrilla["FECHA_R"] = FECHA_R;
            filaParaGrilla["RESULTADOS"] = RESULTADOS;
            filaParaGrilla["TIENE_IMAGEN"] = TIENE_ARCHIVO;

            tablaParaGrilla.Rows.Add(filaParaGrilla);
        }

        CargarGrillaPruebasAplicarDesdeTabla(tablaParaGrilla);
    }

    private void CargarPruebasSinAsociacionAPerfil(DataTable tablaPruebasOriginal)
    {
        Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);

        DataTable tablaParaGrilla = ObtenerEstructuraTablaGrillaResultados();

        for (int i = 0; i < tablaPruebasOriginal.Rows.Count; i++)
        {
            DataRow filaPruebaOriginal = tablaPruebasOriginal.Rows[i];


            Decimal ID_APLICACION_PRUEBA = Convert.ToDecimal(filaPruebaOriginal["REGISTRO"]);
            Decimal ID_PRUEBA = Convert.ToDecimal(filaPruebaOriginal["ID_PRUEBA"]);
            String NOM_PRUEBA = filaPruebaOriginal["NOM_PRUEBA"].ToString().Trim();
            Decimal ID_CATEGORIA = Convert.ToDecimal(filaPruebaOriginal["ID_CATEGORIA"]);

            String FECHA_R = String.Empty;
            try
            {
                FECHA_R = Convert.ToDateTime(filaPruebaOriginal["FECHA_R"]).ToShortDateString();
            }
            catch
            {
                FECHA_R = string.Empty;
            }

            String RESULTADOS = filaPruebaOriginal["RESULTADOS"].ToString().Trim();
            String TIENE_ARCHIVO = filaPruebaOriginal["TIENE_ARCHIVO"].ToString().Trim();

            DataRow filaParaGrilla = tablaParaGrilla.NewRow();

            filaParaGrilla["ID_APLICACION_PRUEBA"] = ID_APLICACION_PRUEBA;
            filaParaGrilla["ID_PRUEBA"] = ID_PRUEBA;
            filaParaGrilla["NOM_PRUEBA"] = NOM_PRUEBA;
            filaParaGrilla["ID_CATEGORIA"] = ID_CATEGORIA;
            filaParaGrilla["FECHA_R"] = FECHA_R;
            filaParaGrilla["RESULTADOS"] = RESULTADOS;
            filaParaGrilla["TIENE_IMAGEN"] = TIENE_ARCHIVO;

            tablaParaGrilla.Rows.Add(filaParaGrilla);
        }

        CargarGrillaPruebasAplicarDesdeTabla(tablaParaGrilla);
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

    private void HabilitarFilasGrilla(GridView grilla, int colInicio)
    {
        for (int i = 0; i < grilla.Rows.Count; i++)
        {
            for (int j = colInicio; j < grilla.Columns.Count; j++)
            {
                grilla.Rows[i].Cells[j].Enabled = true;
            }
        }
    }

    private void limpiar(Acciones accion)
    {
        switch (accion)
        { 
            case Acciones.NuevaEntrvita:
                TextBox_COM_C_FAM.Text = "";
                TextBox_COM_C_ACA.Text = "";
                TextBox_COM_F_LAB.Text = "";
                TextBox_CONCEPTO_GENERAL.Text = "";

                GridView_ComposicionFamiliar.DataSource = null;
                GridView_ComposicionFamiliar.DataBind();
                HiddenField_ACCION_GRILLA_COMPOSICIONFAMILIAR.Value = AccionesGrilla.Ninguna.ToString();

                HiddenField_ACCION_GRILLA_INFOACADEMMICA_EF.Value = AccionesGrilla.Ninguna.ToString();
                GridView_EducacionFormal.DataSource = null;
                GridView_EducacionFormal.DataBind();

                HiddenField_ACCION_GRILLA_INFOACADEMMIC_ENF.Value = AccionesGrilla.Ninguna.ToString();
                GridView_EducacionNoFormal.DataSource = null;
                GridView_EducacionNoFormal.DataBind();

                HiddenField_ACCION_GRILLA_EXPLABORAL.Value = AccionesGrilla.Ninguna.ToString();
                GridView_ExperienciaLaboral.DataSource = null;
                GridView_ExperienciaLaboral.DataBind();
                break;
        }
    }

    private void PermitirVerArchivoPrueba(GridView grilla, Boolean verFileUpload)
    { 
        for (int i = 0; i < grilla.Rows.Count; i++)
        {
            grilla.Rows[i].Cells[4].Enabled = true;

            FileUpload fUpload = grilla.Rows[i].FindControl("FileUpload_ARCHIVO") as FileUpload;
            fUpload.Visible = verFileUpload;

            HyperLink link = grilla.Rows[i].FindControl("HyperLink_ARCHIVO") as HyperLink;
            if (String.IsNullOrEmpty(link.NavigateUrl) == false)
            {
                link.Visible = true;
                link.Enabled = true;
            }
            else
            {
                link.Visible = false;
            }
        }
    }

    private void CargarDatosDefaultEnCamposEntreista()
    {
        Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);

        radicacionHojasDeVida _rad = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaSolicitud = _rad.ObtenerRegSolicitudesingresoPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));

    }

    private void HabilitarTodasFilasGrilla(GridView grilla, int colInicio)
    {
        for (int i = 0; i < grilla.Rows.Count; i++)
        {
            for (int j = colInicio; j < grilla.Columns.Count; j++)
            {
                grilla.Rows[i].Cells[j].Enabled = true;
            }
        }
    }

    private void cargarDropNivelesCalificacion(DropDownList drop)
    {
        drop.Items.Clear();

        drop.Items.Add(new System.Web.UI.WebControls.ListItem("Seleccione...", ""));
        drop.Items.Add(new System.Web.UI.WebControls.ListItem("1", "1"));
        drop.Items.Add(new System.Web.UI.WebControls.ListItem("2", "2"));
        drop.Items.Add(new System.Web.UI.WebControls.ListItem("3", "3"));
        drop.Items.Add(new System.Web.UI.WebControls.ListItem("4", "4"));
        drop.Items.Add(new System.Web.UI.WebControls.ListItem("5", "5"));

        drop.DataBind();
    }

    private void CargarGrillaCompetenciasAssesmentCenter_desdeTabla(DataTable tablaCompetencias)
    {
        GridView_CompetenciasAssesment.DataSource = tablaCompetencias;
        GridView_CompetenciasAssesment.DataBind();

        for (int i = 0; i < GridView_CompetenciasAssesment.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_CompetenciasAssesment.Rows[i];
            DataRow filaTabla = tablaCompetencias.Rows[i];

            Label labelCompetencia = filaGrilla.FindControl("Label_Competencia") as Label;
            Label labeldefinicionCompetencia = filaGrilla.FindControl("Label_DefinicionCompetencia") as Label;
            CheckBox checkCumple = filaGrilla.FindControl("CheckBox_CumpleCompetencia") as CheckBox;
            DropDownList dropNivelCumplimiento = filaGrilla.FindControl("DropDownList_NivelCumplimiento") as DropDownList;
            CheckBox checkNoCumple = filaGrilla.FindControl("CheckBox_NoCumpeCompetencia") as CheckBox;
            TextBox textoObservaciones = filaGrilla.FindControl("TextBox_ObservacionesCalificacion") as TextBox;

            labelCompetencia.Text = filaTabla["COMPETENCIA"].ToString().Trim();
            labeldefinicionCompetencia.Text = filaTabla["DEFINICION"].ToString().Trim();

            cargarDropNivelesCalificacion(dropNivelCumplimiento);

            if (filaTabla["ID_APLICACION_COMPETENCIA"].ToString().Trim() == "0")
            {
                checkCumple.Checked = false;
                dropNivelCumplimiento.SelectedIndex = 0;
                checkNoCumple.Checked = false;
                textoObservaciones.Text = "";
            }
            else
            {
                if (filaTabla["CALIFICACION"].ToString().Trim().ToUpper() == "CUMPLE")
                {
                    checkCumple.Checked = true;
                    checkNoCumple.Checked = false;
                    try
                    {
                        dropNivelCumplimiento.SelectedValue = filaTabla["NIVEL_CALIFICACION"].ToString().Trim();
                    }
                    catch
                    {
                        dropNivelCumplimiento.SelectedIndex = 0;
                    }
                }
                else
                {
                    if (filaTabla["CALIFICACION"].ToString().Trim().ToUpper() == "NO CUMPLE")
                    {
                        checkCumple.Checked = false;
                        checkNoCumple.Checked = true;
                        dropNivelCumplimiento.SelectedIndex = 0;
                    }
                }
            }

            textoObservaciones.Text = filaTabla["OBSERVACIONES"].ToString().Trim();
        }
    }

    private void CargarCompetenciasAssesmentConCalificacionAPerfil(Decimal ID_ASSESMENT_CENTER, Decimal ID_SOLICITUD)
    {
        String NOMBRE_ASSESMENT = "ASSESMENT CENTER NO ENCONTRADO";
        String DESCRIPCION_ASSESMENT = "La información relacionada con el Assesment Center que se debe aplicar no fue encontrada.";

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
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información del Assesment Center relacionado con el Perfil actual.", Proceso.Error);
            }
        }
        else
        {
            DataRow filaAssesment = tablaAssesment.Rows[0];

            NOMBRE_ASSESMENT = filaAssesment["NOMBRE_ASSESMENT"].ToString().Trim().ToUpper();
            DESCRIPCION_ASSESMENT = filaAssesment["DESCRIPCION_ASSESMENT"].ToString().Trim();

            DataTable tablaCompetenciasAssesment = _fab.ObtenerCompetenciasAssesmentCenteActivos(ID_ASSESMENT_CENTER, ID_SOLICITUD);

            if (tablaCompetenciasAssesment.Rows.Count <= 0)
            {
                if (_fab.MensajeError != null)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _fab.MensajeError, Proceso.Error);
                }
                else
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron Competencias activas relacionadas con el Assesment Center que se evalua.", Proceso.Error);
                }

                GridView_CompetenciasAssesment.DataSource = null;
                GridView_CompetenciasAssesment.DataBind();
            }
            else
            {
                CargarGrillaCompetenciasAssesmentCenter_desdeTabla(tablaCompetenciasAssesment);
            }
        }

        Label_NombreAssesmentCenter.Text = NOMBRE_ASSESMENT;
        Label_Descripcion2Assesment.Text = DESCRIPCION_ASSESMENT;
    }

    private void CargarEntrevista(Decimal ID_REQUISICION, Decimal ID_EMPRESA, Decimal ID_PERFIL)
    {
        Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);

        if (ID_REQUISICION == 0) HiddenField_ID_REQUISICION.Value = "";
        else HiddenField_ID_REQUISICION.Value = ID_REQUISICION.ToString();

        if (ID_EMPRESA == 0) HiddenField_ID_EMPRESA.Value = "";
        else HiddenField_ID_EMPRESA.Value = ID_EMPRESA.ToString();

        if (ID_PERFIL == 0) HiddenField_ID_PERFIL.Value = "";
        else HiddenField_ID_PERFIL.Value = ID_PERFIL.ToString();

        HiddenField_ID_ENTREVISTA.Value = "";

        HyperLink_ARCHIVO_WORD_ENTREVISTA.NavigateUrl = null;

        Ocultar(Acciones.Inicio);

        hojasVida _hojasVida = new hojasVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaEntrevista = _hojasVida.ObtenerSelRegEntrevistasPorIdSolicitud(ID_SOLICITUD);

        if (tablaEntrevista.Rows.Count > 0)
        {
            DataRow filaEntrevista = tablaEntrevista.Rows[0];

            Decimal REGISTRO_ENTREVISTA = Convert.ToDecimal(filaEntrevista["REGISTRO"]);

            HiddenField_ID_ENTREVISTA.Value = REGISTRO_ENTREVISTA.ToString();

            CargarDatosBasicosEntrevista(filaEntrevista);
            
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.ConEntrevista);

            if (ID_PERFIL != 0)
            {
                CargarPruebasAsociadasAPerfil(ID_PERFIL);
                inhabilitarFilasGrilla(GridView_PRUEBAS_SELECCIONADAS, 0);
                PermitirVerArchivoPrueba(GridView_PRUEBAS_SELECCIONADAS, false);

                if (GridView_PRUEBAS_SELECCIONADAS.Rows.Count > 0)
                {
                    Mostrar(Acciones.PruebasAAplicar);
                }

                perfil _perfiles = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                DataTable tablaPerfil = _perfiles.ObtenerPorRegistro(ID_PERFIL);
                DataRow filaInfoPerfil = tablaPerfil.Rows[0];

                HiddenField_TIPO_ENTREVISTA.Value = filaInfoPerfil["TIPO_ENTREVISTA"].ToString().Trim();

                CheckBox_TipoBasica.Checked = false;
                CheckBox_TipoCompetencias.Checked = false;


                if (filaInfoPerfil["TIPO_ENTREVISTA"].ToString().Trim().Contains("B") == true)
                {
                    CheckBox_TipoBasica.Checked = true;
                    Panel_CALIFICACIONES_COMPETENCIAS.Visible = false;
                    HiddenField_ID_ASSESMENT_CENTER.Value = "";
                }
                else
                {
                    if (filaInfoPerfil["TIPO_ENTREVISTA"].ToString().Trim().Contains("A") == true)
                    { 
                        Decimal ID_ASSESMENT_CENTER = Convert.ToDecimal(filaInfoPerfil["ID_ASSESMENT_CENTAR"]);

                        HiddenField_ID_ASSESMENT_CENTER.Value = ID_ASSESMENT_CENTER.ToString();
                        CargarCompetenciasAssesmentConCalificacionAPerfil(ID_ASSESMENT_CENTER, ID_SOLICITUD);
                        inhabilitarFilasGrilla(GridView_CompetenciasAssesment, 0);

                        CheckBox_TipoCompetencias.Checked = true;

                        Mostrar(Acciones.CompetenciasAAplicar);  
                    }
                }
            }
            else
            {
         
                perfil _perfil = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                pruebaPerfil _prueba = new pruebaPerfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                FabricaAssesment _fabrica = new FabricaAssesment(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                
                DataTable tablaCompetencias = _fabrica.ObtenerAplicacionCompetenciasPorSolicitudIngreso(ID_SOLICITUD);
                DataTable tablaPruebas = _prueba.ObtenerAplicadasAIdSolicitudConResultados(ID_SOLICITUD);

                if (tablaPruebas.Rows.Count > 0)
                {
                    Panel_PRUEBAS.Visible = true;
                    Panel_GRILLA_SELECCION_PRUEBAS.Visible = false;
                    GridView_SELECCION_PRUEBAS.DataSource = null;
                    GridView_SELECCION_PRUEBAS.DataBind();
                    Panel_PRUEBAS_SELECCIONADAS.Visible = true;

                    CargarPruebasSinAsociacionAPerfil(tablaPruebas);
                    inhabilitarFilasGrilla(GridView_PRUEBAS_SELECCIONADAS, 0);

                    PermitirVerArchivoPrueba(GridView_PRUEBAS_SELECCIONADAS, false);
                }
                else
                {
                    Panel_PRUEBAS.Visible = false;
                }

                HiddenField_TIPO_ENTREVISTA.Value = String.Empty;
               
                if (tablaCompetencias.Rows.Count > 0)
                {
                    HiddenField_TIPO_ENTREVISTA.Value = "A";
                    
                    CheckBox_TipoCompetencias.Checked = true;

                    Panel_CALIFICACIONES_COMPETENCIAS.Visible = true;
                    Label_NombreAssesmentCenter.Text = String.Empty;
                    Label_Descripcion2Assesment.Text = String.Empty;

                    CargarGrillaCompetenciasAssesmentCenter_desdeTabla(tablaCompetencias);
                    inhabilitarFilasGrilla(GridView_CompetenciasAssesment, 0);
                }

                if (String.IsNullOrEmpty(HiddenField_TIPO_ENTREVISTA.Value) == true)
                {
                    HiddenField_TIPO_ENTREVISTA.Value = "B";
                    CheckBox_TipoBasica.Checked = true;
                }
            }
        }
        else
        {
            Activar(Acciones.NuevaEntrvita);
            limpiar(Acciones.NuevaEntrvita); 
            Mostrar(Acciones.NuevaEntrvita);
            Cargar(Acciones.NuevaEntrvita);
           
            if ((ID_REQUISICION == 0) && (ID_EMPRESA == 0) && (ID_PERFIL == 0))
            {
                cargar_GridView_SELECCION_PRUEBAS();
                Panel_PRUEBAS.Visible = true;
                Panel_GRILLA_SELECCION_PRUEBAS.Visible = true;
                Button_GENERAR_RESULTADOS_PRUEBAS.Visible = true;

                HiddenField_TIPO_ENTREVISTA.Value = "B";
                
                Panel_ContenedorTipoEntrevista.Visible = true;
                Panel_BotonesDeSeleciconTipoEntrevista.Visible = true;

                Panel_SELECCION_TIPO_ENTREVISTA.Visible = true;

                CheckBox_TipoBasica.Enabled = true;
                CheckBox_TipoCompetencias.Enabled = true;

                CheckBox_TipoBasica.Checked = false;
                CheckBox_TipoCompetencias.Checked = false;

                HiddenField_ID_ASSESMENT_CENTER.Value = "";
            }
            else
            {
                perfil _perfiles = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                DataTable tablaPerfil = _perfiles.ObtenerPorRegistro(ID_PERFIL);
                DataRow filaInfoPerfil = tablaPerfil.Rows[0];

                HiddenField_TIPO_ENTREVISTA.Value = filaInfoPerfil["TIPO_ENTREVISTA"].ToString().Trim();

                Panel_SELECCION_TIPO_ENTREVISTA.Visible = true;


                if (filaInfoPerfil["TIPO_ENTREVISTA"].ToString().Trim().Contains("B") == true)
                {
                    Panel_CALIFICACIONES_COMPETENCIAS.Visible = false;

                    CheckBox_TipoBasica.Checked = true;
                    CheckBox_TipoCompetencias.Checked = false;

                    HiddenField_ID_ASSESMENT_CENTER.Value = "";
                }
                else
                {
                    if (filaInfoPerfil["TIPO_ENTREVISTA"].ToString().Trim().Contains("A") == true)
                    {
                        Decimal ID_ASSESMENT_CENTER = Convert.ToDecimal(filaInfoPerfil["ID_ASSESMENT_CENTAR"]);
                        CargarCompetenciasAssesmentConCalificacionAPerfil(ID_ASSESMENT_CENTER, ID_SOLICITUD);
                        HabilitarTodasFilasGrilla(GridView_CompetenciasAssesment, 0);

                        CheckBox_TipoBasica.Checked = false;
                        CheckBox_TipoCompetencias.Checked = true;

                    }
                }
               
                CargarPruebasAsociadasAPerfil(ID_PERFIL);
                HabilitarFilasGrilla(GridView_PRUEBAS_SELECCIONADAS, 0);
                PermitirVerArchivoPrueba(GridView_PRUEBAS_SELECCIONADAS, true);
                if (GridView_PRUEBAS_SELECCIONADAS.Rows.Count > 0)
                {
                    Mostrar(Acciones.PruebasAAplicar);
                }
            }

            CargarDatosDefaultEnCamposEntreista();
        }
    }

    protected void GridView_REQ_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal ID_REQUISICION = Convert.ToDecimal(GridView_REQ.DataKeys[indexSeleccionado].Values["ID_REQUERIMIENTO"]);
            Decimal ID_EMPRESA = Convert.ToDecimal(GridView_REQ.DataKeys[indexSeleccionado].Values["ID_EMPRESA"]);
            Decimal ID_PERFIL = Convert.ToDecimal(GridView_REQ.DataKeys[indexSeleccionado].Values["REGISTRO_PERFIL"]);

            CargarEntrevista(ID_REQUISICION, ID_EMPRESA, ID_PERFIL);
        }
    }

    protected void Button_SIN_PRUEBAS_Click(object sender, EventArgs e)
    {
        Panel_PRUEBAS.Visible = false;
        Panel_GRILLA_SELECCION_PRUEBAS.Visible = false;

        Panel_ContenedorTipoEntrevista.Focus();

    }

    protected void Button_CERRAR_MENSAJE_TIPO_ENTREVISTA_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE_TIPO_ENTREVISTA, Panel_MENSAJES_TIPO_ENTREVISTA);
    }

    protected void Button_CERRAR_MENSAJE_PRUEBAS_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE_PRUEBAS, Panel_MENSAJES_PRUEBAS);
    }

    private DataTable ConfigurarTablaFamiliares()
    {
        DataTable tablaResultado = new DataTable();

        tablaResultado.Columns.Add("ID_COMPOSICION");
        tablaResultado.Columns.Add("ID_TIPO_FAMILIAR");
        tablaResultado.Columns.Add("NOMBRES");
        tablaResultado.Columns.Add("APELLIDOS");
        tablaResultado.Columns.Add("FECHA_NACIMIENTO");
        tablaResultado.Columns.Add("PROFESION");
        tablaResultado.Columns.Add("ID_CIUDAD");
        tablaResultado.Columns.Add("VIVE_CON_EL");
        tablaResultado.Columns.Add("ACTIVO");

        return tablaResultado;
    }

    private DataTable obtenerTablaDeGrillaFamiliares()
    {
        DataTable tablaResultado = ConfigurarTablaFamiliares();

        DataRow filaTablaResultado;

        Decimal ID_COMPOSICION;
        String ID_TIPO_FAMILIAR;
        String NOMBRES;
        String APELLIDOS;
        String FECHA_NACIMIENTO;
        String PROFESION;
        String ID_CIUDAD;
        String VIVE_CON_EL;
        Boolean ACTIVO;

        for (int i = 0; i < GridView_ComposicionFamiliar.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ComposicionFamiliar.Rows[i];

            ID_COMPOSICION = Convert.ToDecimal(GridView_ComposicionFamiliar.DataKeys[i].Values["ID_COMPOSICION"]);

            DropDownList dropTipoFamiliar = filaGrilla.FindControl("DropDownList_TipoFamiliar") as DropDownList;
            ID_TIPO_FAMILIAR = dropTipoFamiliar.SelectedValue;

            TextBox textoNombres = filaGrilla.FindControl("TextBox_NombresFamiliar") as TextBox;
            NOMBRES = textoNombres.Text;

            TextBox textoApellidos = filaGrilla.FindControl("TextBox_ApellidosFamiliar") as TextBox;
            APELLIDOS = textoApellidos.Text;

            TextBox textoFechaNacimiento = filaGrilla.FindControl("TextBox_FechaNacimientoFamiliar") as TextBox;
            try
            {
                FECHA_NACIMIENTO = Convert.ToDateTime(textoFechaNacimiento.Text).ToShortDateString();
            }
            catch
            {
                FECHA_NACIMIENTO = null;
            }

            TextBox textoProfesion = filaGrilla.FindControl("TextBox_ProfesionFamiliar") as TextBox;
            PROFESION = textoProfesion.Text;

            CheckBox checkExtranjero = filaGrilla.FindControl("CheckBox_Extranjero") as CheckBox;
            DropDownList dropCiudad = filaGrilla.FindControl("DropDownList_CiudadFamiliar") as DropDownList;
            if (checkExtranjero.Checked == true)
            {
                ID_CIUDAD = "EXTRA";
            }
            else
            {
                ID_CIUDAD = dropCiudad.SelectedValue;
            }

            CheckBox checkViveConEl = filaGrilla.FindControl("CheckBox_ViveConElFamiliar") as CheckBox;
            if (checkViveConEl.Checked == true)
            {
                VIVE_CON_EL = "True";
            }
            else
            {
                VIVE_CON_EL = "False";
            }

            ACTIVO = true;

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_COMPOSICION"] = ID_COMPOSICION;
            filaTablaResultado["ID_TIPO_FAMILIAR"] = ID_TIPO_FAMILIAR;
            filaTablaResultado["NOMBRES"] = NOMBRES;
            filaTablaResultado["APELLIDOS"] = APELLIDOS;
            filaTablaResultado["FECHA_NACIMIENTO"] = FECHA_NACIMIENTO;
            filaTablaResultado["PROFESION"] = PROFESION;
            filaTablaResultado["ID_CIUDAD"] = ID_CIUDAD;
            filaTablaResultado["VIVE_CON_EL"] = VIVE_CON_EL;
            filaTablaResultado["ACTIVO"] = ACTIVO;

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

    protected void Button_NUEVA_COMPOSICIONFAMILIAR_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerTablaDeGrillaFamiliares();
        
        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_COMPOSICION"] = 0;
        filaNueva["ID_TIPO_FAMILIAR"] = "";
        filaNueva["NOMBRES"] = "";
        filaNueva["APELLIDOS"] = "";
        filaNueva["FECHA_NACIMIENTO"] = "";
        filaNueva["PROFESION"] = "";
        filaNueva["ID_CIUDAD"] = "";
        filaNueva["VIVE_CON_EL"] = "";
        filaNueva["ACTIVO"] = "True";

        tablaDesdeGrilla.Rows.Add(filaNueva);

        Cargar_Grilla_Composicionfamiliar_Desdetabla(tablaDesdeGrilla);

        inhabilitarFilasGrilla(GridView_ComposicionFamiliar, 2);
        ActivarFilaGrilla(GridView_ComposicionFamiliar, GridView_ComposicionFamiliar.Rows.Count - 1, 2);

        HiddenField_ACCION_GRILLA_COMPOSICIONFAMILIAR.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_SELECCIONADA_GRILLA_COMPOSIIONFAMILIAR.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();

        GridView_ComposicionFamiliar.Columns[0].Visible = false;
        GridView_ComposicionFamiliar.Columns[1].Visible = false;

        Button_NUEVA_COMPOSICIONFAMILIAR.Visible = false;
        Button_GUARDAR_COMPOSICIONFAMILIAR.Visible = true;
        Button_CANCELAR_COMPOSICIONFAMILIAR.Visible = true;
    }

    protected void DropDownList_DepartamentoFamiliar_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList drop = (DropDownList)sender;

        Int32 indexSeleccionadoEnGrilla = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_COMPOSIIONFAMILIAR.Value);
        GridViewRow filaGrilla = GridView_ComposicionFamiliar.Rows[indexSeleccionadoEnGrilla];

        DropDownList dropCiudad = filaGrilla.FindControl("DropDownList_CiudadFamiliar") as DropDownList;

        if (drop.SelectedValue == "")
        {
            dropCiudad.Items.Clear();
        }
        else
        {
            String ID_DEPARTAMENTO = drop.SelectedValue.ToString();
            cargar_DropDownList_CIUDAD(ID_DEPARTAMENTO,dropCiudad);
        }
    }

    private void inhabilitarFilaGrilla(GridView grilla, int fila, int colInicio)
    {
        for (int j = colInicio; j < grilla.Columns.Count; j++)
        {
            grilla.Rows[fila].Cells[j].Enabled = false;
        }
    }

    protected void Button_GUARDAR_COMPOSICIONFAMILIAR_Click(object sender, EventArgs e)
    {
        int FILA_SELECCIONADA = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_COMPOSIIONFAMILIAR.Value);

        inhabilitarFilaGrilla(GridView_ComposicionFamiliar, FILA_SELECCIONADA, 2);

        GridView_ComposicionFamiliar.Columns[0].Visible = true;
        GridView_ComposicionFamiliar.Columns[1].Visible = true;

        Button_GUARDAR_COMPOSICIONFAMILIAR.Visible = false;
        Button_CANCELAR_COMPOSICIONFAMILIAR.Visible = false;
        Button_NUEVA_COMPOSICIONFAMILIAR.Visible = true;

        HiddenField_ACCION_GRILLA_COMPOSICIONFAMILIAR.Value = AccionesGrilla.Ninguna.ToString();
    }

    protected void Button_CANCELAR_COMPOSICIONFAMILIAR_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = obtenerTablaDeGrillaFamiliares();

        if (HiddenField_ACCION_GRILLA_COMPOSICIONFAMILIAR.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }
        else
        {
            if (HiddenField_ACCION_GRILLA_COMPOSICIONFAMILIAR.Value == AccionesGrilla.Modificar.ToString())
            {
                DataRow filaGrilla = tablaGrilla.Rows[Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_COMPOSIIONFAMILIAR.Value)];

                filaGrilla["ID_COMPOSICION"] = HiddenField_ID_COMPOSICION.Value;
                filaGrilla["ID_TIPO_FAMILIAR"] = HiddenField_ID_TIPO_FAMILIAR.Value;
                filaGrilla["NOMBRES"] = HiddenField_NOMBRES.Value;
                filaGrilla["APELLIDOS"] = HiddenField_APELLIDOS.Value;
                filaGrilla["FECHA_NACIMIENTO"] = HiddenField_FECHA_NACIMIENTO.Value;
                filaGrilla["PROFESION"] = HiddenField_PROFESION.Value;
                filaGrilla["ID_CIUDAD"] = HiddenField_ID_CIUDAD.Value;
                filaGrilla["VIVE_CON_EL"] = HiddenField_VIVE_CON_EL.Value;
                filaGrilla["ACTIVO"] = "True";
            }
        }

        Cargar_Grilla_Composicionfamiliar_Desdetabla(tablaGrilla);

        inhabilitarFilasGrilla(GridView_ComposicionFamiliar, 2);

        GridView_ComposicionFamiliar.Columns[0].Visible = true;
        GridView_ComposicionFamiliar.Columns[1].Visible = true;

        HiddenField_ACCION_GRILLA_COMPOSICIONFAMILIAR.Value = AccionesGrilla.Ninguna.ToString();

        Button_NUEVA_COMPOSICIONFAMILIAR.Visible = true;
        Button_GUARDAR_COMPOSICIONFAMILIAR.Visible = false;
        Button_CANCELAR_COMPOSICIONFAMILIAR.Visible = false;
    }

    protected void GridView_ComposicionFamiliar_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "modificar")
        {
            HiddenField_FILA_SELECCIONADA_GRILLA_COMPOSIIONFAMILIAR.Value = indexSeleccionado.ToString();

            GridViewRow filaGrilla = GridView_ComposicionFamiliar.Rows[indexSeleccionado];

            ActivarFilaGrilla(GridView_ComposicionFamiliar, indexSeleccionado, 2);

            HiddenField_ACCION_GRILLA_COMPOSICIONFAMILIAR.Value = AccionesGrilla.Modificar.ToString();
            HiddenField_FILA_SELECCIONADA_GRILLA_COMPOSIIONFAMILIAR.Value = indexSeleccionado.ToString();

            HiddenField_ID_COMPOSICION.Value = GridView_ComposicionFamiliar.DataKeys[indexSeleccionado].Values["ID_COMPOSICION"].ToString();

            DropDownList dropTipoFamiliar = filaGrilla.FindControl("DropDownList_TipoFamiliar") as DropDownList;
            HiddenField_ID_TIPO_FAMILIAR.Value = dropTipoFamiliar.SelectedValue;

            TextBox textoNombres = filaGrilla.FindControl("TextBox_NombresFamiliar") as TextBox;
            HiddenField_NOMBRES.Value = textoNombres.Text;

            TextBox textoApellidos = filaGrilla.FindControl("TextBox_ApellidosFamiliar") as TextBox;
            HiddenField_APELLIDOS.Value = textoApellidos.Text;

            TextBox textoFechaNacimiento = filaGrilla.FindControl("TextBox_FechaNacimientoFamiliar") as TextBox;
            try
            {
                HiddenField_FECHA_NACIMIENTO.Value = Convert.ToDateTime(textoFechaNacimiento.Text).ToShortDateString();
            }
            catch
            {
                HiddenField_FECHA_NACIMIENTO.Value = "";
            }

            TextBox textoProfesion = filaGrilla.FindControl("TextBox_ProfesionFamiliar") as TextBox;
            HiddenField_PROFESION.Value = textoProfesion.Text;

            CheckBox checkExtranjero = filaGrilla.FindControl("CheckBox_Extranjero") as CheckBox;
            DropDownList dropCiudad = filaGrilla.FindControl("DropDownList_CiudadFamiliar") as DropDownList;
            if (checkExtranjero.Checked == true)
            {
                HiddenField_ID_CIUDAD.Value = "EXTRA";
            }
            else
            {
                HiddenField_ID_CIUDAD.Value = dropCiudad.SelectedValue;
            }

            CheckBox checkViveConEl = filaGrilla.FindControl("CheckBox_ViveConElFamiliar") as CheckBox;
            if (checkViveConEl.Checked == true)
            {
                HiddenField_VIVE_CON_EL.Value = "True";
            }
            else
            {
                HiddenField_VIVE_CON_EL.Value = "False";
            }

            HiddenField_ACTIVO.Value = "True";

            GridView_ComposicionFamiliar.Columns[0].Visible = false;
            GridView_ComposicionFamiliar.Columns[1].Visible = false;

            Button_NUEVA_COMPOSICIONFAMILIAR.Visible = false;
            Button_GUARDAR_COMPOSICIONFAMILIAR.Visible = true;
            Button_CANCELAR_COMPOSICIONFAMILIAR.Visible = true;
        }
        else
        {
            if (e.CommandName == "eliminar")
            {
                DataTable tablaDesdeGrilla = obtenerTablaDeGrillaFamiliares();

                tablaDesdeGrilla.Rows.RemoveAt(indexSeleccionado);

                Cargar_Grilla_Composicionfamiliar_Desdetabla(tablaDesdeGrilla);

                inhabilitarFilasGrilla(GridView_ComposicionFamiliar, 2);

                GridView_ComposicionFamiliar.Columns[0].Visible = true;
                GridView_ComposicionFamiliar.Columns[1].Visible = true;

                HiddenField_ACCION_GRILLA_COMPOSICIONFAMILIAR.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_FILA_SELECCIONADA_GRILLA_COMPOSIIONFAMILIAR.Value = null;

                Button_NUEVA_COMPOSICIONFAMILIAR.Visible = true;
                Button_GUARDAR_COMPOSICIONFAMILIAR.Visible = false;
                Button_CANCELAR_COMPOSICIONFAMILIAR.Visible = false;
            }
        }
    }

    private DataTable ConfigurarTablaParaGrillaEducacionFormal()
    {
        DataTable tablaResultado = new DataTable();

        tablaResultado.Columns.Add("ID_INFO_ACADEMICA");
        tablaResultado.Columns.Add("NIVEL_ACADEMICO");
        tablaResultado.Columns.Add("INSTITUCION");
        tablaResultado.Columns.Add("ANNO");
        tablaResultado.Columns.Add("OBSERVACIONES");
        tablaResultado.Columns.Add("ACTIVO");

        return tablaResultado;
    }

    private DataTable obtenerTablaDeGrillaEducacionFormal()
    {
        DataTable tablaResultado = ConfigurarTablaParaGrillaEducacionFormal();

        DataRow filaTablaResultado;

        Decimal ID_INFO_ACADEMICA;
        String NIVEL_ACADEMICO;
        String INSTITUCION;
        String ANNO;
        String OBSERVACIONES;
        Boolean ACTIVO = true;

        for (int i = 0; i < GridView_EducacionFormal.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_EducacionFormal.Rows[i];

            ID_INFO_ACADEMICA = Convert.ToDecimal(GridView_EducacionFormal.DataKeys[i].Values["ID_INFO_ACADEMICA"]);

            DropDownList dropnivAcademico = filaGrilla.FindControl("DropDownList_NivAcademico") as DropDownList;
            NIVEL_ACADEMICO = dropnivAcademico.SelectedValue;

            TextBox textoInstitucion = filaGrilla.FindControl("TextBox_Institucion") as TextBox;
            INSTITUCION = textoInstitucion.Text;


            TextBox textoAnno = filaGrilla.FindControl("TextBox_Anno") as TextBox;
            if (String.IsNullOrEmpty(textoAnno.Text) == false)
            {
                ANNO = Convert.ToInt32(textoAnno.Text).ToString();
            }
            else
            {
                ANNO = "";
            }

            TextBox textoObservaciones = filaGrilla.FindControl("TextBox_Observaciones") as TextBox;
            OBSERVACIONES = textoObservaciones.Text;

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_INFO_ACADEMICA"] = ID_INFO_ACADEMICA;
            filaTablaResultado["NIVEL_ACADEMICO"] = NIVEL_ACADEMICO;
            filaTablaResultado["INSTITUCION"] = INSTITUCION;
            filaTablaResultado["ANNO"] = ANNO;
            filaTablaResultado["OBSERVACIONES"] = OBSERVACIONES;
            filaTablaResultado["ACTIVO"] = ACTIVO;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }

    protected void Button_NuevoEF_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerTablaDeGrillaEducacionFormal();
        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_INFO_ACADEMICA"] = 0;
        filaNueva["NIVEL_ACADEMICO"] = "";
        filaNueva["INSTITUCION"] = "";
        filaNueva["ANNO"] = "";
        filaNueva["OBSERVACIONES"] = "";
        filaNueva["ACTIVO"] = "True";

        tablaDesdeGrilla.Rows.Add(filaNueva);

        Cargar_Grilla_informacionEducativaFormal_Desdetabla(tablaDesdeGrilla);

        inhabilitarFilasGrilla(GridView_EducacionFormal, 2);
        ActivarFilaGrilla(GridView_EducacionFormal, GridView_EducacionFormal.Rows.Count - 1, 2);

        HiddenField_ACCION_GRILLA_INFOACADEMMICA_EF.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_SELECCIONADA_GRILLA_INFOACADEMICA_EF.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();
        HiddenField_ID_INFO_ACADEMICA_EF.Value = null;
        HiddenField_INSTITUCION_EF.Value = null;
        HiddenField_ANNO_EF.Value = null;
        HiddenField_OBSERVACIONES_EF.Value = null;

        GridView_EducacionFormal.Columns[0].Visible = false;
        GridView_EducacionFormal.Columns[1].Visible = false;

        Button_NuevoEF.Visible = false;
        Button_GuardarEF.Visible = true;
        Button_CancelarEF.Visible = true;
    }

    protected void Button_GuardarEF_Click(object sender, EventArgs e)
    {
        int FILA_SELECCIONADA = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_INFOACADEMICA_EF.Value);

        inhabilitarFilaGrilla(GridView_EducacionFormal, FILA_SELECCIONADA, 2);

        GridView_EducacionFormal.Columns[0].Visible = true;
        GridView_EducacionFormal.Columns[1].Visible = true;

        Button_GuardarEF.Visible = false;
        Button_CancelarEF.Visible = false;
        Button_NuevoEF.Visible = true;

        HiddenField_ACCION_GRILLA_INFOACADEMMICA_EF.Value = AccionesGrilla.Ninguna.ToString();
    }

    protected void Button_CancelarEF_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = obtenerTablaDeGrillaEducacionFormal();

        if (HiddenField_ACCION_GRILLA_INFOACADEMMICA_EF.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }
        else
        {
            if (HiddenField_ACCION_GRILLA_INFOACADEMMICA_EF.Value == AccionesGrilla.Modificar.ToString())
            {
                DataRow filaGrilla = tablaGrilla.Rows[Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_INFOACADEMICA_EF.Value)];

                filaGrilla["ID_INFO_ACADEMICA"] = HiddenField_ID_INFO_ACADEMICA_EF.Value;
                filaGrilla["NIVEL_ACADEMICO"] = HiddenField_NIV_ACADEMICO_EF.Value;
                filaGrilla["INSTITUCION"] = HiddenField_INSTITUCION_EF.Value;
                filaGrilla["ANNO"] = HiddenField_ANNO_EF.Value;
                filaGrilla["OBSERVACIONES"] = HiddenField_OBSERVACIONES_EF.Value;
                filaGrilla["ACTIVO"] = "True";
            }
        }

        Cargar_Grilla_informacionEducativaFormal_Desdetabla(tablaGrilla);

        inhabilitarFilasGrilla(GridView_EducacionFormal, 2);

        GridView_EducacionFormal.Columns[0].Visible = true;
        GridView_EducacionFormal.Columns[1].Visible = true;

        HiddenField_ACCION_GRILLA_INFOACADEMMICA_EF.Value = AccionesGrilla.Ninguna.ToString();

        Button_NuevoEF.Visible = true;
        Button_GuardarEF.Visible = false;
        Button_CancelarEF.Visible = false;
    }

    protected void GridView_EducacionFormal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "modificar")
        {
            HiddenField_FILA_SELECCIONADA_GRILLA_INFOACADEMICA_EF.Value = indexSeleccionado.ToString();

            ActivarFilaGrilla(GridView_EducacionFormal, indexSeleccionado, 2);

            HiddenField_ACCION_GRILLA_INFOACADEMMICA_EF.Value = AccionesGrilla.Modificar.ToString();
            HiddenField_FILA_SELECCIONADA_GRILLA_INFOACADEMICA_EF.Value = indexSeleccionado.ToString();

            GridViewRow filaGrilla = GridView_EducacionFormal.Rows[indexSeleccionado];

            HiddenField_ID_INFO_ACADEMICA_EF.Value = GridView_EducacionFormal.DataKeys[indexSeleccionado].Values["ID_INFO_ACADEMICA"].ToString();

            DropDownList dropnivAcademico = filaGrilla.FindControl("DropDownList_NivAcademico") as DropDownList;
            HiddenField_NIV_ACADEMICO_EF.Value = dropnivAcademico.SelectedValue;

            TextBox textoInstitucion = filaGrilla.FindControl("TextBox_Institucion") as TextBox;
            HiddenField_INSTITUCION_EF.Value = textoInstitucion.Text;


            TextBox textoAnno = filaGrilla.FindControl("TextBox_Anno") as TextBox;
            if (String.IsNullOrEmpty(textoAnno.Text) == false)
            {
                HiddenField_ANNO_EF.Value = Convert.ToInt32(textoAnno.Text).ToString();
            }
            else
            {
                HiddenField_ANNO_EF.Value = "";
            }

            TextBox textoObservaciones = filaGrilla.FindControl("TextBox_Observaciones") as TextBox;
            HiddenField_OBSERVACIONES_EF.Value = textoObservaciones.Text;

            HiddenField_ACTIVO_EF.Value = "True";

            GridView_EducacionFormal.Columns[0].Visible = false;
            GridView_EducacionFormal.Columns[1].Visible = false;

            Button_NuevoEF.Visible = false;
            Button_GuardarEF.Visible = true;
            Button_CancelarEF.Visible = true;
        }
        else
        {
            if (e.CommandName == "eliminar")
            {
                DataTable tablaDesdeGrilla = obtenerTablaDeGrillaEducacionFormal();

                tablaDesdeGrilla.Rows.RemoveAt(indexSeleccionado);

                Cargar_Grilla_informacionEducativaFormal_Desdetabla(tablaDesdeGrilla);

                inhabilitarFilasGrilla(GridView_EducacionFormal, 2);

                GridView_EducacionFormal.Columns[0].Visible = true;
                GridView_EducacionFormal.Columns[1].Visible = true;

                HiddenField_ACCION_GRILLA_INFOACADEMMICA_EF.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_FILA_SELECCIONADA_GRILLA_INFOACADEMICA_EF.Value = null;

                Button_NuevoEF.Visible = true;
                Button_GuardarEF.Visible = false;
                Button_CancelarEF.Visible = false;
            }
        }
    }

    private DataTable ConfigurarTablaParaGrillaEducacionNoFormal()
    {
        DataTable tablaResultado = new DataTable();

        tablaResultado.Columns.Add("ID_INFO_ACADEMICA");
        tablaResultado.Columns.Add("CURSO");
        tablaResultado.Columns.Add("INSTITUCION");
        tablaResultado.Columns.Add("DURACION");
        tablaResultado.Columns.Add("UNIDAD_DURACION");
        tablaResultado.Columns.Add("OBSERVACIONES");
        tablaResultado.Columns.Add("ACTIVO");

        return tablaResultado;
    }

    private DataTable obtenerTablaDeGrillaEducacionNoFormal()
    {
        DataTable tablaResultado = ConfigurarTablaParaGrillaEducacionNoFormal();

        DataRow filaTablaResultado;

        Decimal ID_INFO_ACADEMICA;
        String CURSO;
        String INSTITUCION;
        String DURACION;
        String UNIDAD_DURACION;
        String OBSERVACIONES;
        Boolean ACTIVO;

        for (int i = 0; i < GridView_EducacionNoFormal.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_EducacionNoFormal.Rows[i];

            ID_INFO_ACADEMICA = Convert.ToDecimal(GridView_EducacionNoFormal.DataKeys[i].Values["ID_INFO_ACADEMICA"]);

            TextBox textoCurso = filaGrilla.FindControl("TextBox_Curso") as TextBox;
            CURSO = textoCurso.Text;

            TextBox textoInstitucion = filaGrilla.FindControl("TextBox_Institucion") as TextBox;
            INSTITUCION = textoInstitucion.Text;

            TextBox textoDuracion = filaGrilla.FindControl("TextBox_Duracion") as TextBox;
            if (String.IsNullOrEmpty(textoDuracion.Text) == false)
            {
                DURACION = Convert.ToDecimal(textoDuracion.Text).ToString();
            }
            else
            {
                DURACION = "";
            }

            DropDownList dropUnidadDuracion = filaGrilla.FindControl("DropDownList_UnidadDuracion") as DropDownList;
            UNIDAD_DURACION = dropUnidadDuracion.SelectedValue;

            TextBox textoobservaciones = filaGrilla.FindControl("TextBox_Observaciones") as TextBox;
            OBSERVACIONES = textoobservaciones.Text;

            ACTIVO = true;

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_INFO_ACADEMICA"] = ID_INFO_ACADEMICA;
            filaTablaResultado["CURSO"] = CURSO;
            filaTablaResultado["INSTITUCION"] = INSTITUCION;
            filaTablaResultado["DURACION"] = DURACION;
            filaTablaResultado["UNIDAD_DURACION"] = UNIDAD_DURACION;
            filaTablaResultado["OBSERVACIONES"] = OBSERVACIONES;
            filaTablaResultado["ACTIVO"] = "True";

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }

    protected void Button_NuevoENF_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerTablaDeGrillaEducacionNoFormal();
        
        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_INFO_ACADEMICA"] = 0;
        filaNueva["CURSO"] = "";
        filaNueva["INSTITUCION"] = "";
        filaNueva["DURACION"] = "";
        filaNueva["UNIDAD_DURACION"] = "";
        filaNueva["OBSERVACIONES"] = "";
        filaNueva["ACTIVO"] = "True";

        tablaDesdeGrilla.Rows.Add(filaNueva);

        Cargar_Grilla_informacionEducativaNoFormal_Desdetabla(tablaDesdeGrilla);

        inhabilitarFilasGrilla(GridView_EducacionNoFormal, 2);
        ActivarFilaGrilla(GridView_EducacionNoFormal, GridView_EducacionNoFormal.Rows.Count - 1, 2);

        HiddenField_ACCION_GRILLA_INFOACADEMMIC_ENF.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_SELECCIONADA_GRILLA_INFOACADEMICA_ENF.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();
        HiddenField_ID_INFO_ACADEMICA_ENF.Value = null;
        HiddenField_CURSO_ENF.Value = null;
        HiddenField_DURACION_ENF.Value = null;
        HiddenField_UNIDAD_DURACION_ENF.Value = null;
        HiddenField_ACTIVO_ENF.Value = null;

        GridView_EducacionNoFormal.Columns[0].Visible = false;
        GridView_EducacionNoFormal.Columns[1].Visible = false;

        Button_NuevoENF.Visible = false;
        Button_GuardarENF.Visible = true;
        Button_CancelarENF.Visible = true;
    }

    protected void Button_GuardarENF_Click(object sender, EventArgs e)
    {
        int FILA_SELECCIONADA = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_INFOACADEMICA_ENF.Value);

        inhabilitarFilaGrilla(GridView_EducacionNoFormal, FILA_SELECCIONADA, 2);

        GridView_EducacionNoFormal.Columns[0].Visible = true;
        GridView_EducacionNoFormal.Columns[1].Visible = true;

        Button_GuardarENF.Visible = false;
        Button_CancelarENF.Visible = false;
        Button_NuevoENF.Visible = true;

        HiddenField_ACCION_GRILLA_INFOACADEMMIC_ENF.Value = AccionesGrilla.Ninguna.ToString();
    }

    protected void Button_CancelarENF_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = obtenerTablaDeGrillaEducacionNoFormal();

        if (HiddenField_ACCION_GRILLA_INFOACADEMMIC_ENF.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }
        else
        {
            if (HiddenField_ACCION_GRILLA_INFOACADEMMIC_ENF.Value == AccionesGrilla.Modificar.ToString())
            {
                DataRow filaGrilla = tablaGrilla.Rows[Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_INFOACADEMICA_ENF.Value)];

                filaGrilla["ID_INFO_ACADEMICA"] = HiddenField_ID_INFO_ACADEMICA_ENF.Value;
                filaGrilla["CURSO"] = HiddenField_CURSO_ENF.Value;
                filaGrilla["INSTITUCION"] = HiddenField_INSTITUCION_ENF.Value;
                filaGrilla["DURACION"] = HiddenField_DURACION_ENF.Value;
                filaGrilla["UNIDAD_DURACION"] = HiddenField_UNIDAD_DURACION_ENF.Value;
                filaGrilla["OBSERVACIONES"] = HiddenField_OBSERVACIONES_ENF.Value;
                filaGrilla["ACTIVO"] = "True";
            }
        }

        Cargar_Grilla_informacionEducativaNoFormal_Desdetabla(tablaGrilla);

        inhabilitarFilasGrilla(GridView_EducacionNoFormal, 2);

        GridView_EducacionNoFormal.Columns[0].Visible = true;
        GridView_EducacionNoFormal.Columns[1].Visible = true;

        HiddenField_ACCION_GRILLA_INFOACADEMMIC_ENF.Value = AccionesGrilla.Ninguna.ToString();

        Button_NuevoENF.Visible = true;
        Button_GuardarENF.Visible = false;
        Button_CancelarENF.Visible = false;
    }

    protected void GridView_EducacionNoFormal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "modificar")
        {
            HiddenField_FILA_SELECCIONADA_GRILLA_INFOACADEMICA_ENF.Value = indexSeleccionado.ToString();

            GridViewRow filaGrilla = GridView_EducacionNoFormal.Rows[indexSeleccionado];

            ActivarFilaGrilla(GridView_EducacionNoFormal, indexSeleccionado, 2);

            HiddenField_ACCION_GRILLA_INFOACADEMMIC_ENF.Value = AccionesGrilla.Modificar.ToString();
            HiddenField_FILA_SELECCIONADA_GRILLA_INFOACADEMICA_ENF.Value = indexSeleccionado.ToString();


            HiddenField_ID_INFO_ACADEMICA_ENF.Value = GridView_EducacionNoFormal.DataKeys[indexSeleccionado].Values["ID_INFO_ACADEMICA"].ToString();

            TextBox textoCurso = filaGrilla.FindControl("TextBox_Curso") as TextBox;
            HiddenField_CURSO_ENF.Value = textoCurso.Text;

            TextBox textoInstitucion = filaGrilla.FindControl("TextBox_Institucion") as TextBox;
            HiddenField_INSTITUCION_ENF.Value = textoInstitucion.Text;

            TextBox textoDuracion = filaGrilla.FindControl("TextBox_Duracion") as TextBox;
            if (String.IsNullOrEmpty(textoDuracion.Text) == false)
            {
                HiddenField_DURACION_ENF.Value = Convert.ToDecimal(textoDuracion.Text).ToString();
            }
            else
            {
                HiddenField_DURACION_ENF.Value = "";
            }

            DropDownList dropUnidadDuracion = filaGrilla.FindControl("DropDownList_UnidadDuracion") as DropDownList;
            HiddenField_UNIDAD_DURACION_ENF.Value = dropUnidadDuracion.SelectedValue;

            TextBox textoobservaciones = filaGrilla.FindControl("TextBox_Observaciones") as TextBox;
            HiddenField_OBSERVACIONES_ENF.Value = textoobservaciones.Text;

            GridView_EducacionNoFormal.Columns[0].Visible = false;
            GridView_EducacionNoFormal.Columns[1].Visible = false;

            Button_NuevoENF.Visible = false;
            Button_GuardarENF.Visible = true;
            Button_CancelarENF.Visible = true;
        }
        else
        {
            if (e.CommandName == "eliminar")
            {
                DataTable tablaDesdeGrilla = obtenerTablaDeGrillaEducacionNoFormal();

                tablaDesdeGrilla.Rows.RemoveAt(indexSeleccionado);

                Cargar_Grilla_informacionEducativaNoFormal_Desdetabla(tablaDesdeGrilla);

                inhabilitarFilasGrilla(GridView_EducacionNoFormal, 2);

                GridView_EducacionNoFormal.Columns[0].Visible = true;
                GridView_EducacionNoFormal.Columns[1].Visible = true;

                HiddenField_ACCION_GRILLA_INFOACADEMMIC_ENF.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_FILA_SELECCIONADA_GRILLA_INFOACADEMICA_ENF.Value = null;

                Button_NuevoENF.Visible = true;
                Button_GuardarENF.Visible = false;
                Button_CancelarENF.Visible = false;
            }
        }
    }

    private DataTable ConfigurarTablaParaGrillaExperienciaLaboral()
    {
        DataTable tablaResultado = new DataTable();

        tablaResultado.Columns.Add("ID_EXPERIENCIA");
        tablaResultado.Columns.Add("EMPRESA");
        tablaResultado.Columns.Add("CARGO");
        tablaResultado.Columns.Add("FUNCIONES");
        tablaResultado.Columns.Add("FECHA_INGRESO");
        tablaResultado.Columns.Add("FECHA_RETIRO");
        tablaResultado.Columns.Add("MOTIVO_RETIRO");
        tablaResultado.Columns.Add("ULTIMO_SALARIO");
        tablaResultado.Columns.Add("ACTIVO");

        return tablaResultado;
    }

    private DataTable obtenerTablaDeGrillaExperienciaLaboral()
    {
        DataTable tablaResultado = ConfigurarTablaParaGrillaExperienciaLaboral();

        DataRow filaTablaResultado;

        Decimal ID_EXPERIENCIA;
        String EMPRESA;
        String CARGO;
        String FUNCIONES;
        String FECHA_INGRESO;
        String FECHA_RETIRO;
        String MOTIVO_RETIRO;
        String ULTIMO_SALARIO;
        Boolean ACTIVO;

        for (int i = 0; i < GridView_ExperienciaLaboral.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ExperienciaLaboral.Rows[i];

            ID_EXPERIENCIA = Convert.ToDecimal(GridView_ExperienciaLaboral.DataKeys[i].Values["ID_EXPERIENCIA"]);

            TextBox textoEmpresa = filaGrilla.FindControl("TextBox_Empresa") as TextBox;
            EMPRESA = textoEmpresa.Text;

            TextBox textoCargo = filaGrilla.FindControl("TextBox_Cargo") as TextBox;
            CARGO = textoCargo.Text;

            TextBox textoFunciones = filaGrilla.FindControl("TextBox_FuncionesRealizadas") as TextBox;
            FUNCIONES = textoFunciones.Text;

            TextBox textoFechaIngreso = filaGrilla.FindControl("TextBox_FechaIngreso") as TextBox;
            FECHA_INGRESO = textoFechaIngreso.Text;

            TextBox textoFechaRetiro = filaGrilla.FindControl("TextBox_FechaRetiro") as TextBox;
            FECHA_RETIRO = textoFechaRetiro.Text;

            DropDownList dropMotivoRetiro = filaGrilla.FindControl("DropDownList_MotivoRetiro") as DropDownList;
            MOTIVO_RETIRO = dropMotivoRetiro.Text;

            TextBox textoUltimoSalario = filaGrilla.FindControl("TextBox_Ultimosalario") as TextBox;
            ULTIMO_SALARIO = textoUltimoSalario.Text;

            ACTIVO = true;

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_EXPERIENCIA"] = ID_EXPERIENCIA;
            filaTablaResultado["EMPRESA"] = EMPRESA;
            filaTablaResultado["CARGO"] = CARGO;
            filaTablaResultado["FUNCIONES"] = FUNCIONES;
            filaTablaResultado["FECHA_INGRESO"] = FECHA_INGRESO;
            filaTablaResultado["FECHA_RETIRO"] = FECHA_RETIRO;
            filaTablaResultado["MOTIVO_RETIRO"] = MOTIVO_RETIRO;
            filaTablaResultado["ULTIMO_SALARIO"] = ULTIMO_SALARIO;
            filaTablaResultado["ACTIVO"] = "True";

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }

    protected void Button_NuevoEmpleo_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerTablaDeGrillaExperienciaLaboral();
        
        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_EXPERIENCIA"] = 0;
        filaNueva["EMPRESA"] = "";
        filaNueva["CARGO"] = "";
        filaNueva["FUNCIONES"] = "";
        filaNueva["FECHA_INGRESO"] = "";
        filaNueva["FECHA_RETIRO"] = "";
        filaNueva["MOTIVO_RETIRO"] = "";
        filaNueva["ULTIMO_SALARIO"] = "";
        filaNueva["ACTIVO"] = "True";

        tablaDesdeGrilla.Rows.Add(filaNueva);

        Cargar_Grilla_ExperienciaLaboral_Desdetabla(tablaDesdeGrilla);

        inhabilitarFilasGrilla(GridView_ExperienciaLaboral, 2);
        ActivarFilaGrilla(GridView_ExperienciaLaboral, GridView_ExperienciaLaboral.Rows.Count - 1, 2);

        HiddenField_ACCION_GRILLA_EXPLABORAL.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_SELECCIONADA_GRILLA_EXPLABORAL.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();

        GridView_ExperienciaLaboral.Columns[0].Visible = false;
        GridView_ExperienciaLaboral.Columns[1].Visible = false;

        Button_NuevoEmpleo.Visible = false;
        Button_GuardarEmpleo.Visible = true;
        Button_CancelarEmpleo.Visible = true;
    }

    protected void Button_GuardarEmpleo_Click(object sender, EventArgs e)
    {
        int FILA_SELECCIONADA = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_EXPLABORAL.Value);

        inhabilitarFilaGrilla(GridView_ExperienciaLaboral, FILA_SELECCIONADA, 2);

        GridView_ExperienciaLaboral.Columns[0].Visible = true;
        GridView_ExperienciaLaboral.Columns[1].Visible = true;

        Button_GuardarEmpleo.Visible = false;
        Button_CancelarEmpleo.Visible = false;
        Button_NuevoEmpleo.Visible = true;

        HiddenField_ACCION_GRILLA_EXPLABORAL.Value = AccionesGrilla.Ninguna.ToString();
    }

    protected void Button_CancelarEmpleo_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = obtenerTablaDeGrillaExperienciaLaboral();

        if (HiddenField_ACCION_GRILLA_EXPLABORAL.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }
        else
        {
            if (HiddenField_ACCION_GRILLA_EXPLABORAL.Value == AccionesGrilla.Modificar.ToString())
            {
                DataRow filaGrilla = tablaGrilla.Rows[Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_EXPLABORAL.Value)];

                filaGrilla["ID_EXPERIENCIA"] = HiddenField_ID_EXPERIENCIA.Value;
                filaGrilla["EMPRESA"] = HiddenField_EMPRESA_EL.Value;
                filaGrilla["CARGO"] = HiddenField_CARGO_EL.Value;
                filaGrilla["FUNCIONES"] = HiddenField_FUNCIONES_EL.Value;
                filaGrilla["FECHA_INGRESO"] = HiddenField_FECHA_INGRESO_EL.Value;
                filaGrilla["FECHA_RETIRO"] = HiddenField_FECHA_RETIRO_EL.Value;
                filaGrilla["MOTIVO_RETIRO"] = HiddenField_MOTIVO_RETIRO_EL.Value;
                filaGrilla["ULTIMO_SALARIO"] = HiddenField_ULTIMO_SALARIO_EL.Value;
                filaGrilla["ACTIVO"] = "True";
            }
        }

        Cargar_Grilla_ExperienciaLaboral_Desdetabla(tablaGrilla);

        inhabilitarFilasGrilla(GridView_ExperienciaLaboral, 2);

        GridView_ExperienciaLaboral.Columns[0].Visible = true;
        GridView_ExperienciaLaboral.Columns[1].Visible = true;

        HiddenField_ACCION_GRILLA_EXPLABORAL.Value = AccionesGrilla.Ninguna.ToString();

        Button_NuevoEmpleo.Visible = true;
        Button_GuardarEmpleo.Visible = false;
        Button_CancelarEmpleo.Visible = false;
    }

    protected void GridView_ExperienciaLaboral_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "modificar")
        {
            HiddenField_FILA_SELECCIONADA_GRILLA_EXPLABORAL.Value = indexSeleccionado.ToString();

            ActivarFilaGrilla(GridView_ExperienciaLaboral, indexSeleccionado, 2);

            GridViewRow filaGrilla = GridView_ExperienciaLaboral.Rows[indexSeleccionado];

            HiddenField_ACCION_GRILLA_EXPLABORAL.Value = AccionesGrilla.Modificar.ToString();
            HiddenField_FILA_SELECCIONADA_GRILLA_EXPLABORAL.Value = indexSeleccionado.ToString();


            HiddenField_ID_EXPERIENCIA.Value = GridView_ExperienciaLaboral.DataKeys[indexSeleccionado].Values["ID_EXPERIENCIA"].ToString();

            TextBox textoEmpresa = filaGrilla.FindControl("TextBox_Empresa") as TextBox;
            HiddenField_EMPRESA_EL.Value = textoEmpresa.Text;

            TextBox textoCargo = filaGrilla.FindControl("TextBox_Cargo") as TextBox;
            HiddenField_CARGO_EL.Value = textoCargo.Text;

            TextBox textoFunciones = filaGrilla.FindControl("TextBox_FuncionesRealizadas") as TextBox;
            HiddenField_FUNCIONES_EL.Value = textoFunciones.Text;

            TextBox textoFechaIngreso = filaGrilla.FindControl("TextBox_FechaIngreso") as TextBox;
            HiddenField_FECHA_INGRESO_EL.Value = textoFechaIngreso.Text;

            TextBox textoFechaRetiro = filaGrilla.FindControl("TextBox_FechaRetiro") as TextBox;
            HiddenField_FECHA_RETIRO_EL.Value = textoFechaRetiro.Text;

            DropDownList dropMotivoRetiro = filaGrilla.FindControl("DropDownList_MotivoRetiro") as DropDownList;
            HiddenField_MOTIVO_RETIRO_EL.Value = dropMotivoRetiro.SelectedValue;

            TextBox textoUltimoSalario = filaGrilla.FindControl("TextBox_Ultimosalario") as TextBox;
            HiddenField_ULTIMO_SALARIO_EL.Value = textoUltimoSalario.Text;

            HiddenField_ACTIVO_EL.Value = "True";

            GridView_ExperienciaLaboral.Columns[0].Visible = false;
            GridView_ExperienciaLaboral.Columns[1].Visible = false;

            Button_NuevoEmpleo.Visible = false;
            Button_GuardarEmpleo.Visible = true;
            Button_CancelarEmpleo.Visible = true;
        }
        else
        {
            if (e.CommandName == "eliminar")
            {
                DataTable tablaDesdeGrilla = obtenerTablaDeGrillaExperienciaLaboral();

                tablaDesdeGrilla.Rows.RemoveAt(indexSeleccionado);

                Cargar_Grilla_ExperienciaLaboral_Desdetabla(tablaDesdeGrilla);

                inhabilitarFilasGrilla(GridView_ExperienciaLaboral, 2);

                GridView_ExperienciaLaboral.Columns[0].Visible = true;
                GridView_ExperienciaLaboral.Columns[1].Visible = true;

                HiddenField_ACCION_GRILLA_EXPLABORAL.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_FILA_SELECCIONADA_GRILLA_EXPLABORAL.Value = null;

                Button_NuevoEmpleo.Visible = true;
                Button_GuardarEmpleo.Visible = false;
                Button_CancelarEmpleo.Visible = false;
            }
        }
    }

    protected void TextBox_FechaNacimientoFamiliar_TextChanged(object sender, EventArgs e)
    {
        tools _tools = new tools();

        Int32 indexSeleccionado = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_COMPOSIIONFAMILIAR.Value);

        TextBox textoFechaNacimiento = GridView_ComposicionFamiliar.Rows[indexSeleccionado].FindControl("TextBox_FechaNacimientoFamiliar") as TextBox;
        Label LabelEdad = GridView_ComposicionFamiliar.Rows[indexSeleccionado].FindControl("Label_Edad") as Label;

        if (textoFechaNacimiento.Text.Length > 0)
        {
            try
            {
                LabelEdad.Text = "Edad: " + _tools.ObtenerEdadDesdeFechaNacimiento(Convert.ToDateTime(textoFechaNacimiento.Text)).ToString();
            }
            catch
            {
                LabelEdad.Text = "Edad: Desconocida.";
            }
        }
        else
        {
            LabelEdad.Text = "Edad: Desconocida.";
        }
    }

    protected void CheckBox_Extranjero_CheckedChanged(object sender, EventArgs e)
    {
        Int32 indexSeleccionado = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_COMPOSIIONFAMILIAR.Value);

        CheckBox checkExtranjero = GridView_ComposicionFamiliar.Rows[indexSeleccionado].FindControl("CheckBox_Extranjero") as CheckBox;
        Panel panelViveEn = GridView_ComposicionFamiliar.Rows[indexSeleccionado].FindControl("Panel_ViveEn") as Panel;

        if (checkExtranjero.Checked == true)
        {
            panelViveEn.Visible = false;
        }
        else
        {
            panelViveEn.Visible = true;
        }
    }

    protected void TextBox_FechaIngreso_TextChanged(object sender, EventArgs e)
    {
        Int32 indexSeleccionado = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_EXPLABORAL.Value);

        TextBox textoFechaIngreso = GridView_ExperienciaLaboral.Rows[indexSeleccionado].FindControl("TextBox_FechaIngreso") as TextBox;
        TextBox textoFechaRetiro = GridView_ExperienciaLaboral.Rows[indexSeleccionado].FindControl("TextBox_FechaRetiro") as TextBox;
        Label labelTiempoTrabajado = GridView_ExperienciaLaboral.Rows[indexSeleccionado].FindControl("Label_TiempoTrabajado") as Label;

        Boolean correcto = true;
        DateTime fechaIngreso;
        DateTime fechaRetiro;
        try
        {
            fechaIngreso = Convert.ToDateTime(textoFechaIngreso.Text);
        }
        catch
        {
            correcto = false;
            fechaIngreso = new DateTime();
        }

        if (correcto == true)
        {
            Boolean conContratoVigente = true;
            try
            {
                fechaRetiro = Convert.ToDateTime(textoFechaRetiro.Text);
                conContratoVigente = false;
            }
            catch
            {
                conContratoVigente = true;
                fechaRetiro = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            }

            if (fechaRetiro < fechaIngreso)
            {
                labelTiempoTrabajado.Text = "Error en fechas.";
            }
            else
            {
                tools _tools = new tools();

                if (conContratoVigente == true)
                {
                    labelTiempoTrabajado.Text = "Lleva trabajando: " + _tools.DiferenciaFechas(fechaRetiro, fechaIngreso);
                }
                else
                {
                    labelTiempoTrabajado.Text = "Trabajó: " + _tools.DiferenciaFechas(fechaRetiro, fechaIngreso);
                }
            }
        }
        else
        {
            labelTiempoTrabajado.Text = "Desconocido.";
        }
    }

    protected void CheckBox_TipoBasica_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_TipoBasica.Checked == true)
        {
            Panel_SeleccionCompetencias.Visible = false;
            Panel_InformacionAssesmentSeleccionado.Visible = false;

            CheckBox_TipoCompetencias.Checked = false;
        }
    }

    private void CargarAssesmentCenter()
    {
        FabricaAssesment _fab = new FabricaAssesment(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaAssesment = _fab.ObtenerAssesmentCenteActivos();

        DropDownList_AssesmentCenter.Items.Clear();

        DropDownList_AssesmentCenter.Items.Add(new System.Web.UI.WebControls.ListItem("Seleccione...", ""));

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
                DropDownList_AssesmentCenter.Items.Add(new System.Web.UI.WebControls.ListItem(filaTabla["NOMBRE_ASSESMENT"].ToString(), filaTabla["ID_ASSESMENT_CENTER"].ToString()));
            }
        }

        DropDownList_AssesmentCenter.DataBind();
    }

    protected void CheckBox_TipoCompetencias_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_TipoCompetencias.Checked == true)
        {
            CheckBox_TipoBasica.Checked = false;

            Panel_SeleccionCompetencias.Visible = true;
            Panel_InformacionAssesmentSeleccionado.Visible = false;
            
            CargarAssesmentCenter();
        }
        else
        {
            Panel_SeleccionCompetencias.Visible = false;
            Panel_InformacionAssesmentSeleccionado.Visible = false;

            GridView_CompetenciasAssesment.DataSource = null;
            GridView_CompetenciasAssesment.DataBind();
        }
    }

    protected void Button_AceptarSeleccionDeTipoEntrevista_Click(object sender, EventArgs e)
    {
        Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);

        if ((CheckBox_TipoBasica.Checked == false) && (CheckBox_TipoCompetencias.Checked == false))
        {
            Informar(Panel_FONDO_MENSAJE_TIPO, Image_MENSAJE_TIPO_POPUP, Panel_MENSAJES_TIPO, Label_MENSAJE_TIPO, "Debe seleciconar el tipo de entrevista para poder continuar.", Proceso.Advertencia);
        }
        else
        {
            Boolean correcto = true;

            if (CheckBox_TipoCompetencias.Checked == true)
            {
                if (DropDownList_AssesmentCenter.SelectedIndex <= 0)
                {
                    correcto = false;
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar el Assesment Center para poder contunuar.", Proceso.Advertencia);
                }
                else
                {
                    Decimal ID_ASSESMENT_CENTER = Convert.ToDecimal(DropDownList_AssesmentCenter.SelectedValue);

                    HiddenField_ID_ASSESMENT_CENTER.Value = ID_ASSESMENT_CENTER.ToString();
                    CargarCompetenciasAssesmentConCalificacionAPerfil(ID_ASSESMENT_CENTER, ID_SOLICITUD);
                    HabilitarFilasGrilla(GridView_CompetenciasAssesment, 0);
                }

                if (correcto == true)
                {
                    Panel_CALIFICACIONES_COMPETENCIAS.Visible = true;
                }
            }


            if (correcto == true)
            {
                CheckBox_TipoBasica.Enabled = false;
                CheckBox_TipoCompetencias.Enabled = false;

                Panel_AvisoDeSeleccionDeTipoEntrevista.Visible = false;
                Panel_SeleccionCompetencias.Visible = false;

                Panel_BotonesDeSeleciconTipoEntrevista.Visible = false;
            }
        }
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
    protected void CheckBox_CumpleCompetencia_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow filaGrilla = ((GridViewRow)((Control)sender).Parent.Parent); 

        CheckBox checkCumple = filaGrilla.FindControl("CheckBox_CumpleCompetencia") as CheckBox;
        DropDownList dropNivelCumplimiento = filaGrilla.FindControl("DropDownList_NivelCumplimiento") as DropDownList;
        RequiredFieldValidator required = filaGrilla.FindControl("RequiredFieldValidator_DropDownList_NivelCumplimiento") as RequiredFieldValidator;
        ValidatorCalloutExtender validator = filaGrilla.FindControl("ValidatorCalloutExtender_DropDownList_NivelCumplimiento") as ValidatorCalloutExtender;

        CheckBox checkNoCumple = filaGrilla.FindControl("CheckBox_NoCumpeCompetencia") as CheckBox;

        if (checkCumple.Checked == true)
        {
            checkNoCumple.Checked = false;
            dropNivelCumplimiento.SelectedIndex = 0;
            required.Enabled = true;
            validator.Enabled = true;
        }
        else
        {
            dropNivelCumplimiento.SelectedIndex = 0;
            required.Enabled = false;
            validator.Enabled = false;
        }
    }
    protected void CheckBox_NoCumpeCompetencia_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow filaGrilla = ((GridViewRow)((Control)sender).Parent.Parent); 

        CheckBox checkCumple = filaGrilla.FindControl("CheckBox_CumpleCompetencia") as CheckBox;
        DropDownList dropNivelCumplimiento = filaGrilla.FindControl("DropDownList_NivelCumplimiento") as DropDownList;
        RequiredFieldValidator required = filaGrilla.FindControl("RequiredFieldValidator_DropDownList_NivelCumplimiento") as RequiredFieldValidator;
        ValidatorCalloutExtender validator = filaGrilla.FindControl("ValidatorCalloutExtender_DropDownList_NivelCumplimiento") as ValidatorCalloutExtender;

        CheckBox checkNoCumple = filaGrilla.FindControl("CheckBox_NoCumpeCompetencia") as CheckBox;

        if (checkNoCumple.Checked == true)
        {
            checkCumple.Checked = false;
            dropNivelCumplimiento.SelectedIndex = 0;
            required.Enabled = false;
            validator.Enabled = false;
        }
    }
}