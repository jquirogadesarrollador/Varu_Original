using System;
using System.Web.UI.WebControls;

using Brainsbits.LLB.comercial;
using Brainsbits.LLB.seleccion;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.almacen;
using Brainsbits.LLB.maestras;

using TSHAK.Components;
using System.IO;
using System.Data;
using AjaxControlToolkit;
using System.Collections.Generic;
using System.Web;

public partial class _CondicionesContratacion : System.Web.UI.Page
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
    Decimal GLO_ID_CENTRO_C = 0;
    Decimal GLO_ID_SUB_C = 0;
    String GLO_ID_CIUDAD = null;
    Decimal GLO_ID_SERVICIO = 0;

    String GLO_SERVICIO_EXAMANES_MEDICOS = "EXAMENES MEDICOS"; 
    String[] listaExclusionImplementos = { "EXAMENES MEDICOS" };

    private enum Acciones
    {
        Inicio = 0,
        NuevaCondicion,
        GrillaCondicionesConfiguradas,
        NuevaCondicionSegundaParte,
        CargarCondicion,
        CargarGrillaReplique,
        ModificarCondicion
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

    private enum TiposServicioComplementarios
    { 
        IMPLEMENTOS = 0,
        EXAMENES_MEDICOS
    }

    #endregion variables

    #region propiedades
    #endregion propiedades

    #region constructores
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            Iniciar();
        }
    }
    #endregion constructores

    #region metodos
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
                Panel_INFO_ADICIONAL_MODULO.Visible = false;

                Panel_FORM_BOTONES.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_MOMDIFICAR.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_VOLVER.Visible = false;

                Panel_RESULTADOS_GRID.Visible = false;

                Panel_DATOS_PERFIL_SELECCIONADO.Visible = false;

                Panel_SELECCION_CIUDAD_CC_SUBC.Visible = false;

                Panel_BOTON_NUEVA_CONDICION.Visible = false;

                Panel_INFO_CONDICION_COTRATACION.Visible = false;
                Panel_DROP_PARA_SELECCIONAR_CIUDAD_CC_SUBC.Visible = false;
                Panel_SeleccionServicio.Visible = false;
                Panel_BOTON_EMPEZAR_CONFIGURACION.Visible = false;

                Panel_RIESGO_DOCUMENTOS_REQUISITOS.Visible = false;
                Panel_CONTROL_REGISTRO.Visible = false;
                Panel_CAMPOS_RIESGO_DOCUMENTOS_REQUISITOS.Visible = false;
                Panel_SERVICIOS_COMPLEMENTARIOS.Visible = false;
                Panel_InfoNoConfImplementos.Visible = false;
                PanelInfoNoConfExamenesMedicos.Visible = false;
                Panel_LISTA_IMPLEMENTOS_SELECCIONADOS.Visible = false;
                Panel_BotonesImplementos.Visible = false;
                GridView_ImplementosParametrizados.Columns[0].Visible = false;
                GridView_ImplementosParametrizados.Columns[1].Visible = false;
                Button_NuevoImplemento.Visible = false;
                Button_GuardarImplemento.Visible = false;
                Button_CancelarImplemento.Visible = false;

                Panel_EXAMENES_SELECCIONADOS.Visible = false;
                GridView_ExamenesParametrizados.Columns[0].Visible = false;
                GridView_ExamenesParametrizados.Columns[1].Visible = false;
                Panel_BotonesExamenesMedicos.Visible = false;
                Button_NuevoExamen.Visible = false;
                Button_GuardarExamen.Visible = false;
                Button_CancelarExamen.Visible = false;

                Panel_BOTONES_INFERIORES.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                Button_VOLVER_1.Visible = false;
                break;
            case Acciones.ModificarCondicion:
                Button_GUARDAR.Visible = false;
                Button_MOMDIFICAR.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_VOLVER.Visible = false;

                Panel_CONTROL_REGISTRO.Visible = false;

                Button_GuardarImplemento.Visible = false;
                Button_CancelarImplemento.Visible = false;

                Button_GuardarExamen.Visible = false;
                Button_CancelarExamen.Visible = false;

                Button_GUARDAR_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                Button_VOLVER_1.Visible = false;

                break;
        }
    }

    private void Desactivar(Acciones accion)
    {
        switch (accion)
        { 
            case Acciones.Inicio:
                DropDownList_CIUDAD.Enabled = false;
                DropDownList_CENTRO_COSTO.Enabled = false;
                DropDownList_SUB_CENTRO.Enabled = false;
                DropDownList_SERVICIO.Enabled = false;

                TextBox_FCH_CRE.Enabled = false;
                TextBox_HOR_CRE.Enabled = false;
                TextBox_USU_CRE.Enabled = false;
                TextBox_FCH_MOD.Enabled = false;
                TextBox_HOR_MOD.Enabled = false;
                TextBox_USU_MOD.Enabled = false;

                DropDownList_RIESGOS.Enabled = false;

                CheckBoxList_Documentos1.Enabled = false;
                CheckBoxList_Documentos2.Enabled = false;
                CheckBoxList_Documentos3.Enabled = false;
                CheckBoxList_Documentos4.Enabled = false;
                CheckBoxList_Documentos5.Enabled = false;

                TextBox_REQUERIMIENTOS_USUARIO.Enabled = false;

                Panel_clausulas.Enabled = false;

                break;
        }
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        { 
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = true;
                Panel_FORM_BOTONES.Visible = true;
                Button_VOLVER.Visible = true;

                Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.NuevaCondicion:
                Panel_INFO_ADICIONAL_MODULO.Visible = true;
                
                Panel_FORM_BOTONES.Visible = true;
                Button_VOLVER.Visible = true;

                Panel_DATOS_PERFIL_SELECCIONADO.Visible = true;

                Panel_INFO_CONDICION_COTRATACION.Visible = true;
                Panel_DROP_PARA_SELECCIONAR_CIUDAD_CC_SUBC.Visible = true;

                if (Session["idEmpresa"].ToString() == "3")
                {
                    Panel_SeleccionServicio.Visible = true;
                }
                Panel_BOTON_EMPEZAR_CONFIGURACION.Visible = true;
                Button_EMPEZAR_CONFIGURACION.Visible = true;
                break;
            case Acciones.GrillaCondicionesConfiguradas:
                Panel_INFO_ADICIONAL_MODULO.Visible = true;

                Panel_FORM_BOTONES.Visible = true;
                Button_VOLVER.Visible = true;

                Panel_DATOS_PERFIL_SELECCIONADO.Visible = true;

                Panel_SELECCION_CIUDAD_CC_SUBC.Visible = true;

                Panel_BOTON_NUEVA_CONDICION.Visible = true;
                break;
            case Acciones.NuevaCondicionSegundaParte:
                Panel_INFO_ADICIONAL_MODULO.Visible = true;

                Panel_FORM_BOTONES.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;
                Button_VOLVER.Visible = true;

                Panel_DATOS_PERFIL_SELECCIONADO.Visible = true;

                Panel_INFO_CONDICION_COTRATACION.Visible = true;
                Panel_DROP_PARA_SELECCIONAR_CIUDAD_CC_SUBC.Visible = true;
                if (Session["idEmpresa"].ToString() == "3")
                {
                    Panel_SeleccionServicio.Visible = true;
                }

                Panel_RIESGO_DOCUMENTOS_REQUISITOS.Visible = true;
                Panel_CAMPOS_RIESGO_DOCUMENTOS_REQUISITOS.Visible = true;

                Panel_SERVICIOS_COMPLEMENTARIOS.Visible = true;
                Panel_InfoNoConfImplementos.Visible = true;
                PanelInfoNoConfExamenesMedicos.Visible = true;
                Panel_LISTA_IMPLEMENTOS_SELECCIONADOS.Visible = true;
                GridView_ImplementosParametrizados.Columns[0].Visible = true;
                GridView_ImplementosParametrizados.Columns[1].Visible = true;
                Panel_BotonesImplementos.Visible = true;
                Button_NuevoImplemento.Visible = true;

                Panel_EXAMENES_SELECCIONADOS.Visible = true;
                GridView_ExamenesParametrizados.Columns[0].Visible = true;
                GridView_ExamenesParametrizados.Columns[1].Visible = true;
                Panel_BotonesExamenesMedicos.Visible = true;
                Button_NuevoExamen.Visible = true;

                Panel_BOTONES_INFERIORES.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
                break;
            case Acciones.CargarCondicion:
                Panel_INFO_ADICIONAL_MODULO.Visible = true;

                Panel_FORM_BOTONES.Visible = true;
                Button_MOMDIFICAR.Visible = true;
                Button_VOLVER.Visible = true;

                Panel_DATOS_PERFIL_SELECCIONADO.Visible = true;

                Panel_INFO_CONDICION_COTRATACION.Visible = true;
                Panel_DROP_PARA_SELECCIONAR_CIUDAD_CC_SUBC.Visible = true;

                if (Session["idEmpresa"].ToString() == "3")
                {
                    Panel_SeleccionServicio.Visible = true;
                }

                Panel_RIESGO_DOCUMENTOS_REQUISITOS.Visible = true;
                Panel_CAMPOS_RIESGO_DOCUMENTOS_REQUISITOS.Visible = true;
                Panel_SERVICIOS_COMPLEMENTARIOS.Visible = true;
                Panel_LISTA_IMPLEMENTOS_SELECCIONADOS.Visible = true;

                Panel_EXAMENES_SELECCIONADOS.Visible = true;
                Panel_clausulas.Visible = true;

                break;
            case Acciones.ModificarCondicion:
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_InfoNoConfImplementos.Visible = true;
                PanelInfoNoConfExamenesMedicos.Visible = true;

                GridView_ExamenesParametrizados.Columns[0].Visible = true;
                GridView_ExamenesParametrizados.Columns[1].Visible = true;
                Button_NuevoExamen.Visible = true;

                GridView_ImplementosParametrizados.Columns[0].Visible = true;
                GridView_ImplementosParametrizados.Columns[1].Visible = true;
                Button_NuevoImplemento.Visible = true;

                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;

                break;
        }
    }

    private void CargarInformacionEmpresa()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoEmpresa = _cliente.ObtenerEmpresaConIdEmpresa(ID_EMPRESA);
        DataRow filaTablaInfoEmpresa = tablaInfoEmpresa.Rows[0];

        Label_INFO_ADICIONAL_MODULO.Text = filaTablaInfoEmpresa["RAZ_SOCIAL"].ToString().Trim();

        HiddenField_ID_EMPRESA.Value = ID_EMPRESA.ToString();
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

    private void Cargar_GridViewPerfiles()
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        String GRID_PAGINA = HiddenField_GridPagina.Value;

        String FILTRO = HiddenField_TIPO_BUSQUEDA_ACTUAL.Value;
        String DROP = HiddenField_FILTRO_DROP.Value;
        String DATO = HiddenField_FILTRO_DATO.Value;

        perfil _PERFIL = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaPerfiles = new DataTable();

        if (FILTRO == "SIN_FILTRO")
        {
            
            tablaPerfiles = _PERFIL.ObtenerVenDPerfilesConOcupacionPorIdEmpresa(Convert.ToInt32(ID_EMPRESA));
        }
        else
        {
            if (DROP == "NOM_OCUPACION")
            {
                tablaPerfiles = _PERFIL.ObtenerVenDPerfilesConOcupacionPorIdEmpresaYNomOcupacion(ID_EMPRESA, DATO);
            }
        }

        GridView_PERFILES.PageIndex = Convert.ToInt32(GRID_PAGINA);

        if (tablaPerfiles.Rows.Count > 0)
        {
            GridView_PERFILES.DataSource = tablaPerfiles;
            GridView_PERFILES.DataBind();
        }
        else
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El cliente no posee pefiles de cargos asociados. Los perfiles deben ser creados en el modulo de SELECCIÓN.", Proceso.Advertencia);
        }
    }

    private void Cargar_DropDownList_CIUDAD(Decimal ID_EMPRESA)
    {
        DropDownList_CIUDAD.Items.Clear();

        cobertura _cobertura = new cobertura(Session["idEmpresa"].ToString());
        DataTable tablaCoberturaEmpresa = _cobertura.obtenerCoberturaDeUnCliente(ID_EMPRESA);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione Ciudad", "");
        DropDownList_CIUDAD.Items.Add(item);

        foreach (DataRow fila in tablaCoberturaEmpresa.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["Ciudad"].ToString(), fila["Código Ciudad"].ToString());
            DropDownList_CIUDAD.Items.Add(item);
        }

        DropDownList_CIUDAD.DataBind();
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        { 
            case Acciones.Inicio:
                Page.Header.Title = "CONDICIONES DE CONTRATACIÓN";

                CargarInformacionEmpresa();

                configurarCaracteresAceptadosBusqueda(true, true);

                iniciar_seccion_de_busqueda();

                HiddenField_ID_PERFIL.Value = "";
                HiddenField_REGISTRO_VEN_P_CONTRATACION.Value = "";
                HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "SIN_FILTRO";
                HiddenField_FILTRO_DROP.Value = String.Empty;
                HiddenField_FILTRO_DATO.Value = String.Empty;
                HiddenField_ACCION_GRILLA_IMPLEMENTOS.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_ACCION_GRILLA_EXAMENES.Value = AccionesGrilla.Ninguna.ToString();

                HiddenField_GridPagina.Value = "0";

                Cargar_GridViewPerfiles();

                break;
            case Acciones.NuevaCondicion:
                Cargar_DropDownList_CIUDAD(Convert.ToDecimal(HiddenField_ID_EMPRESA.Value));
                Cargar_DropDownListVacio(DropDownList_CENTRO_COSTO);
                Cargar_DropDownListVacio(DropDownList_SUB_CENTRO);
                Cargar_DropDownListVacio(DropDownList_SERVICIO);
                break;
            case Acciones.ModificarCondicion:

                DeterminarIDsSubCCentrosCCiudadDesdeDrops();

                servicio _servicio = new servicio(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                DataTable tablaServiciosComplementariosAsociados = _servicio.ObtenerServiciosComplementariosPorUbicacion(GLO_ID_CIUDAD, GLO_ID_CENTRO_C, GLO_ID_SUB_C, GLO_ID_SERVICIO);

                if (ExisteServicioExameneszMedicos(tablaServiciosComplementariosAsociados) > 0)
                {
                    HiddenField_ServicioExamenesMedicos.Value = "SI";
                    HiddenField_ID_PARA_SABER_ID_DE_EXAMENES_MEDICOS.Value = ExisteServicioExameneszMedicos(tablaServiciosComplementariosAsociados).ToString();

                    Panel_SERVICIOS_COMPLEMENTARIOS.Visible = true;

                    PanelInfoNoConfExamenesMedicos.Visible = false;

                    Panel_EXAMENES_SELECCIONADOS.Visible = true;
                    Panel_BotonesExamenesMedicos.Visible = true;
                    Button_NuevoExamen.Visible = true;
                }
                else
                {
                    HiddenField_ServicioExamenesMedicos.Value = "NO";
                    HiddenField_ID_PARA_SABER_ID_DE_EXAMENES_MEDICOS.Value = "";

                    PanelInfoNoConfExamenesMedicos.Visible = true;

                    Panel_EXAMENES_SELECCIONADOS.Visible = false;
                }

                if (TotalServiciosComplementariosAptosParaImplementos(tablaServiciosComplementariosAsociados) <= 0)
                {
                    Panel_InfoNoConfImplementos.Visible = true;

                    Panel_LISTA_IMPLEMENTOS_SELECCIONADOS.Visible = false;
                    Panel_BotonesImplementos.Visible = false;
                    Button_NuevoImplemento.Visible = false;
                    Button_GuardarImplemento.Visible = false;
                    Button_CancelarImplemento.Visible = false;

                    GridView_ImplementosParametrizados.DataSource = null;
                    GridView_ImplementosParametrizados.DataBind();
                }
                else
                {
                    Panel_InfoNoConfImplementos.Visible = false;

                    Panel_LISTA_IMPLEMENTOS_SELECCIONADOS.Visible = true;
                    Panel_BotonesImplementos.Visible = true;
                    Button_NuevoImplemento.Visible = true;
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

    private void ocultar_mensaje(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.Style.Add("display", "none");
        panel_mensaje.Style.Add("display", "none");

        panel_fondo.Visible = false;
        panel_mensaje.Visible = false;
    }

    private String GetListaDocumentosEntregados()
    {
        String resultado = String.Empty;

        foreach (System.Web.UI.WebControls.ListItem item in CheckBoxList_Documentos1.Items)
        {
            if (item.Selected == true)
            {
                if (resultado.Length <= 0)
                {
                    resultado = item.Value;
                }
                else
                {
                    resultado += "•" + item.Value;
                }
            }
        }


        foreach (System.Web.UI.WebControls.ListItem item in CheckBoxList_Documentos2.Items)
        {
            if (item.Selected == true)
            {
                if (resultado.Length <= 0)
                {
                    resultado = item.Value;
                }
                else
                {
                    resultado += "•" + item.Value;
                }
            }
        }

        foreach (System.Web.UI.WebControls.ListItem item in CheckBoxList_Documentos3.Items)
        {
            if (item.Selected == true)
            {
                if (resultado.Length <= 0)
                {
                    resultado = item.Value;
                }
                else
                {
                    resultado += "•" + item.Value;
                }
            }
        }

        foreach (System.Web.UI.WebControls.ListItem item in CheckBoxList_Documentos4.Items)
        {
            if (item.Selected == true)
            {
                if (resultado.Length <= 0)
                {
                    resultado = item.Value;
                }
                else
                {
                    resultado += "•" + item.Value;
                }
            }
        }

        foreach (System.Web.UI.WebControls.ListItem item in CheckBoxList_Documentos5.Items)
        {
            if (item.Selected == true)
            {
                if (resultado.Length <= 0)
                {
                    resultado = item.Value;
                }
                else
                {
                    resultado += "•" + item.Value;
                }
            }
        }

        return resultado;
    }

    private Boolean DeterminarIDsSubCCentrosCCiudadDesdeDataRow(DataRow filaCondicion)
    {
        GLO_ID_SUB_C = 0;
        GLO_ID_CENTRO_C = 0;
        GLO_ID_CIUDAD = null;
        GLO_ID_SERVICIO = 0;

        if ((filaCondicion["ID_SUB_C"].ToString().Trim() != "") && (filaCondicion["ID_SUB_C"].ToString().Trim() != "0"))
        {
            GLO_ID_SUB_C = Convert.ToDecimal(filaCondicion["ID_SUB_C"]);
        }
        else
        {
            if ((filaCondicion["ID_CENTRO_C"].ToString().Trim() != "") && (filaCondicion["ID_CENTRO_C"].ToString().Trim() != "0"))
            {
                GLO_ID_CENTRO_C = Convert.ToDecimal(filaCondicion["ID_CENTRO_C"]);
            }
            else
            {
                if ((filaCondicion["ID_CIUDAD"].ToString().Trim() != "") && (filaCondicion["ID_CIUDAD"].ToString().Trim() != "0"))
                {
                    GLO_ID_CIUDAD = filaCondicion["ID_CIUDAD"].ToString().Trim();
                }
                else
                {
                    return false;
                }
            }
        }

        if ((filaCondicion["ID_SERVICIO"].ToString().Trim() != "") && (filaCondicion["ID_SERVICIO"].ToString().Trim() != "0"))
        {
            GLO_ID_SERVICIO = Convert.ToDecimal(filaCondicion["ID_SERVICIO"]);
        }

        return true;
    }

    private void CargarDropsCiudadCCSubCYServico(DataRow filaInfoCondicionContratacion)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        DeterminarIDsSubCCentrosCCiudadDesdeDataRow(filaInfoCondicionContratacion);

        if (GLO_ID_SUB_C != 0)
        {
            subCentroCosto _subCentroCosto = new subCentroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaInfoSubC = _subCentroCosto.ObtenerSubCentroDeCostoPorIdSubCConInfoDeCCyCiudad(GLO_ID_SUB_C);
            DataRow filaInfoSubC = tablaInfoSubC.Rows[0];

            Cargar_DropDownList_CIUDAD(ID_EMPRESA);
            DropDownList_CIUDAD.SelectedValue = filaInfoSubC["ID_CIUDAD"].ToString().Trim();

            Cargar_DropDownList_CENTRO_COSTO(ID_EMPRESA, filaInfoSubC["ID_CIUDAD"].ToString().Trim());
            DropDownList_CENTRO_COSTO.SelectedValue = filaInfoSubC["ID_CENTRO_C"].ToString().Trim();

            Cargar_DropDownList_SUB_CENTRO(ID_EMPRESA, Convert.ToDecimal(filaInfoSubC["ID_CENTRO_C"]));
            DropDownList_SUB_CENTRO.SelectedValue = GLO_ID_SUB_C.ToString();

            Cargar_DropDownList_SERVICIO_sub_c(GLO_ID_SUB_C);
            try
            {
                DropDownList_SERVICIO.SelectedValue = GLO_ID_SERVICIO.ToString();
            }
            catch
            {
                DropDownList_SERVICIO.SelectedIndex = 0;
            }
        }
        else
        {
            if (GLO_ID_CENTRO_C != 0)
            {
                centroCosto _centroCosto = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                DataTable tablaInfoCentroC = _centroCosto.ObtenerCentrosDeCostoPorIdCentroCosto(GLO_ID_CENTRO_C);
                DataRow filaInfoCentroC = tablaInfoCentroC.Rows[0];

                Cargar_DropDownList_CIUDAD(ID_EMPRESA);
                DropDownList_CIUDAD.SelectedValue = filaInfoCentroC["ID_CIUDAD"].ToString().Trim();

                Cargar_DropDownList_CENTRO_COSTO(ID_EMPRESA, filaInfoCentroC["ID_CIUDAD"].ToString().Trim());
                DropDownList_CENTRO_COSTO.SelectedValue = GLO_ID_CENTRO_C.ToString();

                Cargar_DropDownListVacio(DropDownList_SUB_CENTRO);

                Cargar_DropDownList_SERVICIO_centro_c(GLO_ID_CENTRO_C);
                try
                {
                    DropDownList_SERVICIO.SelectedValue = GLO_ID_SERVICIO.ToString();
                }
                catch
                {
                    DropDownList_SERVICIO.SelectedIndex = 0;
                }
            }
            else
            {
                if (String.IsNullOrEmpty(GLO_ID_CIUDAD) == false)
                {
                    Cargar_DropDownList_CIUDAD(ID_EMPRESA);
                    DropDownList_CIUDAD.SelectedValue = GLO_ID_CIUDAD;

                    Cargar_DropDownListVacio(DropDownList_CENTRO_COSTO);
                    Cargar_DropDownListVacio(DropDownList_SUB_CENTRO);

                    Cargar_DropDownList_SERVICIO_ciudad(GLO_ID_CIUDAD, ID_EMPRESA);
                    try
                    {
                        DropDownList_SERVICIO.SelectedValue = GLO_ID_SERVICIO.ToString();
                    }
                    catch
                    {
                        DropDownList_SERVICIO.SelectedIndex = 0;
                    }
                }
                else
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se pudo realizar el cargue de la ubicación. Consulte al administrador.", Proceso.Error);
                }
            }
        }
    }

    private void CargarDatosControlRegistro(DataRow filaInfoCondicionContratacion)
    {
        TextBox_USU_CRE.Text = filaInfoCondicionContratacion["USU_CRE"].ToString();
        try
        {
            TextBox_FCH_CRE.Text = DateTime.Parse(filaInfoCondicionContratacion["FCH_CRE"].ToString()).ToShortDateString();
            TextBox_HOR_CRE.Text = DateTime.Parse(filaInfoCondicionContratacion["FCH_CRE"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_CRE.Text = "";
            TextBox_HOR_CRE.Text = "";
        }
        TextBox_USU_MOD.Text = filaInfoCondicionContratacion["USU_MOD"].ToString();
        try
        {
            TextBox_FCH_MOD.Text = DateTime.Parse(filaInfoCondicionContratacion["FCH_MOD"].ToString()).ToShortDateString();
            TextBox_HOR_MOD.Text = DateTime.Parse(filaInfoCondicionContratacion["FCH_MOD"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_MOD.Text = "";
            TextBox_HOR_MOD.Text = "";
        }
    }

    private void CargarRiesgoDocumentosyRequerimientos(DataRow filaInfoCondicionContratacion)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        cargar_DropDownList_RIESGOS(ID_EMPRESA);
        try
        {
            DropDownList_RIESGOS.SelectedValue = filaInfoCondicionContratacion["RIESGO"].ToString().Trim();
        }
        catch
        {
            DropDownList_RIESGOS.SelectedIndex = 0;
        }

        Cargar_ChecksDeDocumentosEntregablesAlTrabajador(filaInfoCondicionContratacion["DOC_TRAB"].ToString().Trim());

        TextBox_REQUERIMIENTOS_USUARIO.Text = filaInfoCondicionContratacion["OBS_CTE"].ToString().Trim().ToUpper();
    }

    private DataTable GetTablaParaGrillaImplementosDesdeTabla(DataTable tablaImplementosOriginal)
    {
        DataTable tablaParaGrilla = ConfigurarTablaParaImplementos();

        DataRow[] filasRegistrosIniciales = tablaImplementosOriginal.Select("PRIMERA_ENTREGA = 'True'");

        foreach (DataRow filaInicial in filasRegistrosIniciales)
        {
            DataRow filaParaGrilla = tablaParaGrilla.NewRow();

            DataRow[] filasRegistrosProgramados = tablaImplementosOriginal.Select("PRIMERA_ENTREGA = 'False' AND ID_PRODUCTO = " + filaInicial["ID_PRODUCTO"].ToString().Trim());

            filaParaGrilla["REGISTRO_CON_REG_ELEMENTO_TRABAJO_INICIAL"] = filaInicial["REGISTRO_CON_REG_ELEMENTO_TRABAJO"];
            filaParaGrilla["REGISTRO_VEN_P_CONTRATACION"] = filaInicial["REGISTRO_VEN_P_CONTRATACION"];
            filaParaGrilla["ID_PRODUCTO"] = filaInicial["ID_PRODUCTO"];
            filaParaGrilla["CANTIDAD_INICIAL"] = filaInicial["CANTIDAD"];
            filaParaGrilla["CODIGO_PERIODO_INICIAL"] = filaInicial["PERIODICIDAD"];
            filaParaGrilla["CODIGO_FACTURAR_A_INICIAL"] = filaInicial["FACTURADO_A"];
            filaParaGrilla["VALOR_INICIAL"] = filaInicial["VALOR"];
            filaParaGrilla["PRIMERA_ENTREGA_INICIAL"] = filaInicial["PRIMERA_ENTREGA"];
            filaParaGrilla["AJUSTE_A_INICIAL"] = filaInicial["AJUSTE_A"];
            try
            {
                filaParaGrilla["FECHA_INICIO_INICIAL"] = Convert.ToDateTime(filaInicial["FECHA_AJUSTE"]).ToShortDateString();
            }
            catch
            {
                filaParaGrilla["FECHA_INICIO_INICIAL"] = "";
            }

            if (filasRegistrosProgramados.Length > 0)
            {
                DataRow filaProgramado = filasRegistrosProgramados[0];

                filaParaGrilla["REGISTRO_CON_REG_ELEMENTO_TRABAJO_PROGRAMADO"] = filaProgramado["REGISTRO_CON_REG_ELEMENTO_TRABAJO"];
                filaParaGrilla["CHECK_PROGRAMADO"] = "True";
                filaParaGrilla["CANTIDAD_PROGRAMADO"] = filaProgramado["CANTIDAD"];
                filaParaGrilla["CODIGO_PERIODO_PROGRAMADO"] = filaProgramado["PERIODICIDAD"];
                filaParaGrilla["CODIGO_FACTURAR_A_PROGRAMADO"] = filaProgramado["FACTURADO_A"];
                filaParaGrilla["VALOR_PROGRAMADO"] = filaProgramado["VALOR"];
                filaParaGrilla["PRIMERA_ENTREGA_PROGRAMADO"] = filaProgramado["PRIMERA_ENTREGA"];
                filaParaGrilla["AJUSTE_A_PROGRAMADO"] = filaProgramado["AJUSTE_A"];
                try
                {
                    filaParaGrilla["FECHA_INICIO_PROGRAMADO"] = Convert.ToDateTime(filaProgramado["FECHA_AJUSTE"]).ToShortDateString();
                }
                catch
                {
                    filaParaGrilla["FECHA_INICIO_PROGRAMADO"] = "";
                }
            }
            else
            {

                filaParaGrilla["REGISTRO_CON_REG_ELEMENTO_TRABAJO_PROGRAMADO"] = 0;
                filaParaGrilla["CHECK_PROGRAMADO"] = "False";
                filaParaGrilla["CANTIDAD_PROGRAMADO"] = "";
                filaParaGrilla["CODIGO_PERIODO_PROGRAMADO"] = "";
                filaParaGrilla["CODIGO_FACTURAR_A_PROGRAMADO"] = "";
                filaParaGrilla["VALOR_PROGRAMADO"] = "";
                filaParaGrilla["PRIMERA_ENTREGA_PROGRAMADO"] = "";
                filaParaGrilla["AJUSTE_A_PROGRAMADO"] = "";
                filaParaGrilla["FECHA_INICIO_PROGRAMADO"] = "";
            }

            filaParaGrilla["ID_SERVICIO_COMPLEMENTARIO"] = filaInicial["ID_SERVICIO_COMPLEMENTARIO"];

            tablaParaGrilla.Rows.Add(filaParaGrilla);
        }

        return tablaParaGrilla;
    }

    private DataTable GetTablaParaGrillaExamenesMedicosDesdetabla(DataTable tablaExamenes)
    {
        DataTable tablaParaGrilla = configurarTablaParaExamenesMedicos();

  

        foreach (DataRow filaOriginal in tablaExamenes.Rows)
        {
            DataRow filaParaGrilla = tablaParaGrilla.NewRow();

            filaParaGrilla["REGISTRO_CON_REG_ELEMENTO_TRABAJO"] = filaOriginal["REGISTRO_CON_REG_ELEMENTO_TRABAJO"];
            filaParaGrilla["REGISTRO_VEN_P_CONTRATACION"] = filaOriginal["REGISTRO_VEN_P_CONTRATACION"];
            filaParaGrilla["ID_PRODUCTO"] = filaOriginal["ID_PRODUCTO"];
            filaParaGrilla["CANTIDAD"] = filaOriginal["CANTIDAD"];
            filaParaGrilla["CODIGO_PERIODO"] = filaOriginal["PERIODICIDAD"];
            filaParaGrilla["CODIGO_FACTURAR_A"] = filaOriginal["FACTURADO_A"];
            filaParaGrilla["VALOR"] = filaOriginal["VALOR"];
            filaParaGrilla["PRIMERA_ENTREGA"] = filaOriginal["PRIMERA_ENTREGA"];
            filaParaGrilla["AJUSTE_A"] = filaOriginal["AJUSTE_A"];
            try
            {
                filaParaGrilla["FECHA_INICIO"] = Convert.ToDateTime(filaOriginal["FECHA_AJUSTE"]).ToShortDateString();
            }
            catch
            {
                filaParaGrilla["FECHA_INICIO"] = "";
            }

            filaParaGrilla["ID_SERVICIO_COMPLEMENTARIO"] = filaOriginal["ID_SERVICIO_COMPLEMENTARIO"];

            tablaParaGrilla.Rows.Add(filaParaGrilla);
        }

        return tablaParaGrilla;
    }

    private void Cargar(Decimal REGISTRO_VEN_P_CONTRATACION)
    {
        HiddenField_REGISTRO_VEN_P_CONTRATACION.Value = REGISTRO_VEN_P_CONTRATACION.ToString();

        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        Decimal ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);

        condicionesContratacion _condicionesContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable TablaInfoCondicionContratacion = _condicionesContratacion.ObtenerCondicionContratacionPorRegistro(REGISTRO_VEN_P_CONTRATACION);
        DataRow filaInfoCondicionContratacion = TablaInfoCondicionContratacion.Rows[0];

        DeterminarIDsSubCCentrosCCiudadDesdeDataRow(filaInfoCondicionContratacion);

        CargarInformacionPerfilSeleccionado(ID_PERFIL);

        CargarDropsCiudadCCSubCYServico(filaInfoCondicionContratacion);

        CargarDatosControlRegistro(filaInfoCondicionContratacion);

        CargarRiesgoDocumentosyRequerimientos(filaInfoCondicionContratacion);


        servicio _servicio = new servicio(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaServiciosComplementariosAsociados = _servicio.ObtenerServiciosComplementariosPorUbicacion(GLO_ID_CIUDAD, GLO_ID_CENTRO_C, GLO_ID_SUB_C, GLO_ID_SERVICIO);

        if (ExisteServicioExameneszMedicos(tablaServiciosComplementariosAsociados) > 0)
        {
            HiddenField_ServicioExamenesMedicos.Value = "SI";
            HiddenField_ID_PARA_SABER_ID_DE_EXAMENES_MEDICOS.Value = ExisteServicioExameneszMedicos(tablaServiciosComplementariosAsociados).ToString();
        }
        else
        {
            HiddenField_ServicioExamenesMedicos.Value = "NO";
            HiddenField_ID_PARA_SABER_ID_DE_EXAMENES_MEDICOS.Value = "";
        }

        DataTable tablaInfoImplementosParametrizados = _condicionesContratacion.obtenerImplementosOExamenesActivosPorRegistroVenPContratacionYTipo(REGISTRO_VEN_P_CONTRATACION, TiposServicioComplementarios.IMPLEMENTOS.ToString());
        DataTable tablaInfoExamenesParametrizados = _condicionesContratacion.obtenerImplementosOExamenesActivosPorRegistroVenPContratacionYTipo(REGISTRO_VEN_P_CONTRATACION, TiposServicioComplementarios.EXAMENES_MEDICOS.ToString());

        DataTable tablaParaGrillaImplementos = GetTablaParaGrillaImplementosDesdeTabla(tablaInfoImplementosParametrizados);
        CargarGridView_ImplementosDesdeTabla(tablaParaGrillaImplementos);
        inhabilitarFilasGrilla(GridView_ImplementosParametrizados, 2);
        AjustarEstadoValidadoresGrillaImplementos(GridView_ImplementosParametrizados);
        if (tablaParaGrillaImplementos.Rows.Count <= 0)
        {
            Panel_LISTA_IMPLEMENTOS_SELECCIONADOS.Visible = false;
        }

        DataTable tablaExamenesConfigurada = GetTablaParaGrillaExamenesMedicosDesdetabla(tablaInfoExamenesParametrizados);
        Cargar_GridView_ExamenesMedicosDesdeTabla(tablaExamenesConfigurada);
        inhabilitarFilasGrilla(GridView_ExamenesParametrizados, 2);
        if (tablaExamenesConfigurada.Rows.Count <= 0)
        {
            Panel_EXAMENES_SELECCIONADOS.Visible = false;
        }
    }

    private void Guardar()
    {
        determinarIDsSubCCentrosCCiudad();

        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        Decimal ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);

        
        String RIESGO = DropDownList_RIESGOS.SelectedValue;

        String DOC_TRAB = GetListaDocumentosEntregados();

        String OBS_CTE = TextBox_REQUERIMIENTOS_USUARIO.Text.ToUpper().Trim();

        condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        List<condicionesContratacion> listaImplementosExamenes = new List<condicionesContratacion>();

        DataTable tablaImplementos = ObtenerDataTable_De_GridView_Implementos();
        for (int i = 0; i < tablaImplementos.Rows.Count; i++)
        {
            DataRow filaTabla = tablaImplementos.Rows[i];

       

            condicionesContratacion _implementoParaLista = new condicionesContratacion();
            _implementoParaLista.AJUSTE_A = tabla.EntregaAjusteA.CONTRATO.ToString();

            if (String.IsNullOrEmpty(filaTabla["CANTIDAD_INICIAL"].ToString()) == false) { _implementoParaLista.CANTIDAD = Convert.ToInt32(filaTabla["CANTIDAD_INICIAL"]); }
            else { _implementoParaLista.CANTIDAD = 0; }

            _implementoParaLista.FACTURAR_A = filaTabla["CODIGO_FACTURAR_A_INICIAL"].ToString().Trim();
            _implementoParaLista.FECHA_INICIO = new DateTime();
            _implementoParaLista.ID_PERIODICIDAD = "0";
            _implementoParaLista.ID_PRODUCTO = Convert.ToDecimal(filaTabla["ID_PRODUCTO"]);
            _implementoParaLista.PRIMERA_ENTREGA = true;
            _implementoParaLista.REGISTRO_CON_REG_ELEMENTO_TRABAJO = Convert.ToDecimal(filaTabla["REGISTRO_CON_REG_ELEMENTO_TRABAJO_INICIAL"]);
            _implementoParaLista.REGISTRO_VEN_P_CONTRATACION = 0;
            if (String.IsNullOrEmpty(filaTabla["VALOR_INICIAL"].ToString().Trim()) == false)
            {
                _implementoParaLista.VALOR = Convert.ToDecimal(filaTabla["VALOR_INICIAL"]);
            }
            else
            {
                _implementoParaLista.VALOR = 0;
            }

            listaImplementosExamenes.Add(_implementoParaLista);


            if (filaTabla["CHECK_PROGRAMADO"].ToString() == "True")
            {
                _implementoParaLista = new condicionesContratacion();
                _implementoParaLista.AJUSTE_A = filaTabla["AJUSTE_A_PROGRAMADO"].ToString();

                if (String.IsNullOrEmpty(filaTabla["CANTIDAD_PROGRAMADO"].ToString()) == false) { _implementoParaLista.CANTIDAD = Convert.ToInt32(filaTabla["CANTIDAD_PROGRAMADO"]); }
                else { _implementoParaLista.CANTIDAD = 0; }

                _implementoParaLista.FACTURAR_A = filaTabla["CODIGO_FACTURAR_A_PROGRAMADO"].ToString().Trim();

                if (String.IsNullOrEmpty(filaTabla["FECHA_INICIO_PROGRAMADO"].ToString().Trim()) == false) { _implementoParaLista.FECHA_INICIO = Convert.ToDateTime(filaTabla["FECHA_INICIO_PROGRAMADO"]); }
                else { _implementoParaLista.FECHA_INICIO = new DateTime(); }

                _implementoParaLista.ID_PERIODICIDAD = filaTabla["CODIGO_PERIODO_PROGRAMADO"].ToString();
                _implementoParaLista.ID_PRODUCTO = Convert.ToDecimal(filaTabla["ID_PRODUCTO"]);
                _implementoParaLista.PRIMERA_ENTREGA = false;
                _implementoParaLista.REGISTRO_CON_REG_ELEMENTO_TRABAJO = Convert.ToDecimal(filaTabla["REGISTRO_CON_REG_ELEMENTO_TRABAJO_PROGRAMADO"]);
                _implementoParaLista.REGISTRO_VEN_P_CONTRATACION = 0;
                if (String.IsNullOrEmpty(filaTabla["VALOR_PROGRAMADO"].ToString().Trim()) == false)
                {
                    _implementoParaLista.VALOR = Convert.ToDecimal(filaTabla["VALOR_PROGRAMADO"]);
                }
                else
                {
                    _implementoParaLista.VALOR = 0;
                }

                listaImplementosExamenes.Add(_implementoParaLista);
            }
        }

        DataTable tablaExamenes = ObtenerDataTable_De_GridView_ExamenesMedicos();
        for (int i = 0; i < tablaExamenes.Rows.Count; i++)
        {
            DataRow filaTabla = tablaExamenes.Rows[i];

    

            condicionesContratacion _implementoParaLista = new condicionesContratacion();


            _implementoParaLista.VALOR = 0;
            _implementoParaLista.REGISTRO_VEN_P_CONTRATACION = 0;
            _implementoParaLista.REGISTRO_CON_REG_ELEMENTO_TRABAJO = Convert.ToDecimal(filaTabla["REGISTRO_CON_REG_ELEMENTO_TRABAJO"]);
            _implementoParaLista.PRIMERA_ENTREGA = true;
            _implementoParaLista.ID_PRODUCTO = Convert.ToDecimal(filaTabla["ID_PRODUCTO"]);
            _implementoParaLista.ID_PERIODICIDAD = filaTabla["CODIGO_PERIODO"].ToString().Trim();
            _implementoParaLista.FECHA_INICIO = new DateTime();
            _implementoParaLista.FACTURAR_A = filaTabla["CODIGO_FACTURAR_A"].ToString().Trim();
            _implementoParaLista.CANTIDAD = 1;
            _implementoParaLista.AJUSTE_A = tabla.EntregaAjusteA.CONTRATO.ToString();

            listaImplementosExamenes.Add(_implementoParaLista);
        }

        condicionesContratacion _condicionesContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Decimal REGISTRO_VEN_P_CONTRATACION = _condicionesContratacion.AdicionarCondicionContratacionUnificada(ID_EMPRESA, DOC_TRAB, ID_PERFIL, RIESGO, OBS_CTE, GLO_ID_SUB_C, GLO_ID_CENTRO_C, GLO_ID_CIUDAD, GLO_ID_SERVICIO, listaImplementosExamenes, Recuperar(GridView_clausulas));

        if (REGISTRO_VEN_P_CONTRATACION <= 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _condicionesContratacion.MensajeError, Proceso.Error);
        }
        else
        {
            HiddenField_REGISTRO_VEN_P_CONTRATACION.Value = REGISTRO_VEN_P_CONTRATACION.ToString();

            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.CargarGrillaReplique);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La condición de contratación fue creada correctament.", Proceso.Correcto);
        }
    }

    private void Actualizar()
    {
        Decimal REGISTRO_VEN_P_CONTRATACION = Convert.ToDecimal(HiddenField_REGISTRO_VEN_P_CONTRATACION.Value);

        determinarIDsSubCCentrosCCiudad();

        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        Decimal ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);

        String RIESGO = DropDownList_RIESGOS.SelectedValue;

        String DOC_TRAB = GetListaDocumentosEntregados();

        String OBS_CTE = TextBox_REQUERIMIENTOS_USUARIO.Text.ToUpper().Trim();

        condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        List<condicionesContratacion> listaImplementosExamenes = new List<condicionesContratacion>();

        DataTable tablaImplementos = ObtenerDataTable_De_GridView_Implementos();
        for (int i = 0; i < tablaImplementos.Rows.Count; i++)
        {
            DataRow filaTabla = tablaImplementos.Rows[i];


            condicionesContratacion _implementoParaLista = new condicionesContratacion();
            _implementoParaLista.AJUSTE_A = tabla.EntregaAjusteA.CONTRATO.ToString();

            if (String.IsNullOrEmpty(filaTabla["CANTIDAD_INICIAL"].ToString()) == false) { _implementoParaLista.CANTIDAD = Convert.ToInt32(filaTabla["CANTIDAD_INICIAL"]); }
            else { _implementoParaLista.CANTIDAD = 0; }

            _implementoParaLista.FACTURAR_A = filaTabla["CODIGO_FACTURAR_A_INICIAL"].ToString().Trim();
            _implementoParaLista.FECHA_INICIO = new DateTime();
            _implementoParaLista.ID_PERIODICIDAD = "0";
            _implementoParaLista.ID_PRODUCTO = Convert.ToDecimal(filaTabla["ID_PRODUCTO"]);
            _implementoParaLista.PRIMERA_ENTREGA = true;
            _implementoParaLista.REGISTRO_CON_REG_ELEMENTO_TRABAJO = Convert.ToDecimal(filaTabla["REGISTRO_CON_REG_ELEMENTO_TRABAJO_INICIAL"]);
            _implementoParaLista.REGISTRO_VEN_P_CONTRATACION = 0;
            if (String.IsNullOrEmpty(filaTabla["VALOR_INICIAL"].ToString().Trim()) == false)
            {
                _implementoParaLista.VALOR = Convert.ToDecimal(filaTabla["VALOR_INICIAL"]);
            }
            else
            {
                _implementoParaLista.VALOR = 0;
            }

            listaImplementosExamenes.Add(_implementoParaLista);


            if (filaTabla["CHECK_PROGRAMADO"].ToString() == "True")
            {
                _implementoParaLista = new condicionesContratacion();
                _implementoParaLista.AJUSTE_A = filaTabla["AJUSTE_A_PROGRAMADO"].ToString();

                if (String.IsNullOrEmpty(filaTabla["CANTIDAD_PROGRAMADO"].ToString()) == false) { _implementoParaLista.CANTIDAD = Convert.ToInt32(filaTabla["CANTIDAD_PROGRAMADO"]); }
                else { _implementoParaLista.CANTIDAD = 0; }

                _implementoParaLista.FACTURAR_A = filaTabla["CODIGO_FACTURAR_A_PROGRAMADO"].ToString().Trim();

                if (String.IsNullOrEmpty(filaTabla["FECHA_INICIO_PROGRAMADO"].ToString().Trim()) == false) { _implementoParaLista.FECHA_INICIO = Convert.ToDateTime(filaTabla["FECHA_INICIO_PROGRAMADO"]); }
                else { _implementoParaLista.FECHA_INICIO = new DateTime(); }

                _implementoParaLista.ID_PERIODICIDAD = filaTabla["CODIGO_PERIODO_PROGRAMADO"].ToString();
                _implementoParaLista.ID_PRODUCTO = Convert.ToDecimal(filaTabla["ID_PRODUCTO"]);
                _implementoParaLista.PRIMERA_ENTREGA = false;
                _implementoParaLista.REGISTRO_CON_REG_ELEMENTO_TRABAJO = Convert.ToDecimal(filaTabla["REGISTRO_CON_REG_ELEMENTO_TRABAJO_PROGRAMADO"]);
                _implementoParaLista.REGISTRO_VEN_P_CONTRATACION = 0;
                if (String.IsNullOrEmpty(filaTabla["VALOR_PROGRAMADO"].ToString().Trim()) == false)
                {
                    _implementoParaLista.VALOR = Convert.ToDecimal(filaTabla["VALOR_PROGRAMADO"]);
                }
                else
                {
                    _implementoParaLista.VALOR = 0;
                }

                listaImplementosExamenes.Add(_implementoParaLista);
            }
        }

        DataTable tablaExamenes = ObtenerDataTable_De_GridView_ExamenesMedicos();
        for (int i = 0; i < tablaExamenes.Rows.Count; i++)
        {
            DataRow filaTabla = tablaExamenes.Rows[i];


            condicionesContratacion _implementoParaLista = new condicionesContratacion();


            _implementoParaLista.VALOR = 0;
            _implementoParaLista.REGISTRO_VEN_P_CONTRATACION = 0;
            _implementoParaLista.REGISTRO_CON_REG_ELEMENTO_TRABAJO = Convert.ToDecimal(filaTabla["REGISTRO_CON_REG_ELEMENTO_TRABAJO"]);
            _implementoParaLista.PRIMERA_ENTREGA = true;
            _implementoParaLista.ID_PRODUCTO = Convert.ToDecimal(filaTabla["ID_PRODUCTO"]);
            _implementoParaLista.ID_PERIODICIDAD = filaTabla["CODIGO_PERIODO"].ToString().Trim();
            _implementoParaLista.FECHA_INICIO = new DateTime();
            _implementoParaLista.FACTURAR_A = filaTabla["CODIGO_FACTURAR_A"].ToString().Trim();
            _implementoParaLista.CANTIDAD = 1;
            _implementoParaLista.AJUSTE_A = tabla.EntregaAjusteA.CONTRATO.ToString();

            listaImplementosExamenes.Add(_implementoParaLista);
        }

        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("ID_CLAUSULA");
        dataTable.Columns.Add("ID_PERFIL");

        for (int i = 0; i < GridView_clausulas.Rows.Count; i++)
        {
            if (((CheckBox)GridView_clausulas.Rows[i].FindControl("CheckBox_aplicar")).Checked == true)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["ID_CLAUSULA"] = GridView_clausulas.Rows[i].Cells[1].Text;
                dataRow["ID_PERFIL"] = HiddenField_ID_PERFIL;
                dataTable.Rows.Add(dataRow);
                dataTable.AcceptChanges();
            }
        }

        condicionesContratacion _condicionesContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean correcto = _condicionesContratacion.ActualizarCondicionContratacion(REGISTRO_VEN_P_CONTRATACION, ID_PERFIL, ID_EMPRESA, GLO_ID_SUB_C, GLO_ID_CENTRO_C, GLO_ID_CIUDAD, DOC_TRAB, RIESGO, OBS_CTE, listaImplementosExamenes, Recuperar(GridView_clausulas));

        if (correcto == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _condicionesContratacion.MensajeError, Proceso.Error);
        }
        else
        {
            HiddenField_REGISTRO_VEN_P_CONTRATACION.Value = REGISTRO_VEN_P_CONTRATACION.ToString();

            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.CargarCondicion);
            Desactivar(Acciones.Inicio);

            Cargar(REGISTRO_VEN_P_CONTRATACION);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La condición de contratación fue modificada correctamente.", Proceso.Correcto);
        }
    }

    private DataTable Recuperar(GridView gridView)
    {
        DataTable dataTable = new DataTable();
        dataTable.Columns.Add("ID_CLAUSULA");
        dataTable.Columns.Add("ID_PERFIL");

        for (int i = 0; i < gridView.Rows.Count; i++)
        {
            if (((CheckBox)gridView.Rows[i].FindControl("CheckBox_aplicar")).Checked == true)
            {
                DataRow dataRow = dataTable.NewRow();
                dataRow["ID_CLAUSULA"] = gridView.DataKeys[i].Values["ID_CLAUSULA"];
                dataRow["ID_PERFIL"] = HiddenField_ID_PERFIL.Value;
                dataTable.Rows.Add(dataRow);
                dataTable.AcceptChanges();
            }
        }
        return dataTable;
    }

    private void Activar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.NuevaCondicion:
                DropDownList_CIUDAD.Enabled = true;
                DropDownList_CENTRO_COSTO.Enabled = true;
                DropDownList_SUB_CENTRO.Enabled = true;
                DropDownList_SERVICIO.Enabled = true;
                Panel_clausulas.Enabled = true;
                break;
            case Acciones.NuevaCondicionSegundaParte:
                DropDownList_RIESGOS.Enabled = true;
                CheckBoxList_Documentos1.Enabled = true;
                CheckBoxList_Documentos2.Enabled = true;
                CheckBoxList_Documentos3.Enabled = true;
                CheckBoxList_Documentos4.Enabled = true;
                CheckBoxList_Documentos5.Enabled = true;
                TextBox_REQUERIMIENTOS_USUARIO.Enabled = true;
                break;
            case Acciones.ModificarCondicion:
                DropDownList_RIESGOS.Enabled = true;
                CheckBoxList_Documentos1.Enabled = true;
                CheckBoxList_Documentos2.Enabled = true;
                CheckBoxList_Documentos3.Enabled = true;
                CheckBoxList_Documentos4.Enabled = true;
                CheckBoxList_Documentos5.Enabled = true;

                TextBox_REQUERIMIENTOS_USUARIO.Enabled = true;

                Panel_clausulas.Enabled = true;

                break;
        }
    }

    private void cargarInterfazNuevaCondicion()
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.NuevaCondicion);
        Activar(Acciones.NuevaCondicion);
        Cargar(Acciones.NuevaCondicion);
    }

    private void cargar_GridView_LISTA_CONFIGURACION_ACTUAL(DataTable tablaCondicionesContratacion)
    {
        GridView_LISTA_CONFIGURACION_ACTUAL.DataSource = tablaCondicionesContratacion;
        GridView_LISTA_CONFIGURACION_ACTUAL.DataBind();
    }

    private void CargarCondicionesDeUnPerfil(Decimal ID_PERFIL)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        HiddenField_ID_PERFIL.Value = ID_PERFIL.ToString();

        HiddenField_REGISTRO_VEN_P_CONTRATACION.Value = "";

        condicionesContratacion _condicionesContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaCondicionContratacion = _condicionesContratacion.ObtenerCondicionContratacionPorIdPerfil(ID_PERFIL);

        if (tablaCondicionContratacion.Rows.Count <= 0)
        {
            if (_condicionesContratacion.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _condicionesContratacion.MensajeError, Proceso.Error);
            }
            else
            {
                cargarInterfazNuevaCondicion();
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron condiciones de contratación para el perfil seleccionado</br>Por favor proceda a seleccionar una UBICACIÓN Y/Ó SERVICIO y configure los datos que se presentan.", Proceso.Advertencia);
            }
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.GrillaCondicionesConfiguradas);

            cargar_GridView_LISTA_CONFIGURACION_ACTUAL(tablaCondicionContratacion);

            if (Session["idEmpresa"].ToString() == "1")
            {
                GridView_LISTA_CONFIGURACION_ACTUAL.Columns[7].Visible = false;
            }
            else
            {
                GridView_LISTA_CONFIGURACION_ACTUAL.Columns[7].Visible = true;
            }
        }
    }

    private void CargarInformacionPerfilSeleccionado(Decimal ID_PERFIL)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        perfil _perfil = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoPefil = _perfil.ObtenerPorRegistro(Convert.ToInt32(ID_PERFIL));
        DataRow filaInfoPerfil = tablaInfoPefil.Rows[0];

        Decimal ID_OCUPACION = Convert.ToDecimal(filaInfoPerfil["ID_OCUPACION"]);

        cargo _cargo = new cargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoCargo = _cargo.ObtenerOcupacionPorIdOcupacion(ID_OCUPACION);
        DataRow filaInfoCargo = tablaInfoCargo.Rows[0];

        Label_NOM_OCUPACION.Text = filaInfoCargo["ID_OCUPACION"].ToString() + "-" + filaInfoCargo["NOM_OCUPACION"].ToString();
        Label_EDAD_MIN.Text = filaInfoPerfil["EDAD_MIN"].ToString();
        Label_EDAD_MAX.Text = filaInfoPerfil["EDAD_MAX"].ToString();
        Label_EXPERIENCIA.Text = filaInfoPerfil["EXPERIENCIA"].ToString();
        Label_SEXO.Text = filaInfoPerfil["SEXO"].ToString();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());
        DataTable tablaParametro = _parametro.ObtenerDescripcionParametroPorCodigo(tabla.PARAMETROS_NIV_ESTUDIOS, filaInfoPerfil["NIV_ESTUDIOS"].ToString());
        DataRow filaInfoParametro = tablaParametro.Rows[0];
        Label_NIVEL_ACADEMICO.Text = filaInfoParametro["DESCRIPCION"].ToString();
    }

    private void Cargar_DropDownListVacio(DropDownList drop)
    {
        drop.Items.Clear();

        drop.Items.Add(new ListItem("Seleccione...", ""));

        drop.DataBind();
    }

    private void Cargar_DropDownList_CENTRO_COSTO(Decimal ID_EMPRESA, String ID_CIUDAD)
    {
        DropDownList_CENTRO_COSTO.Items.Clear();

        centroCosto _centroCosto = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaCCCiudad = _centroCosto.ObtenerCentrosDeCostoPorIdEmpresaIdCiudad(ID_EMPRESA, ID_CIUDAD);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_CENTRO_COSTO.Items.Add(item);

        foreach (DataRow fila in tablaCCCiudad.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOM_CC"].ToString(), fila["ID_CENTRO_C"].ToString());
            DropDownList_CENTRO_COSTO.Items.Add(item);
        }

        DropDownList_CENTRO_COSTO.DataBind();
    }

    private void Cargar_DropDownList_SERVICIO_ciudad(String ID_CIUDAD, Decimal ID_EMPRESA)
    {
        DropDownList_SERVICIO.Items.Clear();

        condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaServicios = _condicionComercial.ObtenerServiciosPorEmpresaPorIdCiudad(ID_CIUDAD, ID_EMPRESA);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_SERVICIO.Items.Add(item);

        foreach (DataRow fila in tablaServicios.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOMBRE_SERVICIO"].ToString(), fila["ID_SERVICIO"].ToString());
            DropDownList_SERVICIO.Items.Add(item);
        }

        DropDownList_SERVICIO.DataBind();
    }

    private void Cargar_DropDownList_SUB_CENTRO(Decimal ID_EMPRESA, Decimal ID_CENTRO_C)
    {
        DropDownList_SUB_CENTRO.Items.Clear();

        subCentroCosto _subCentroCosto = new subCentroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaSUB_C = _subCentroCosto.ObtenerSubCentrosDeCostoPorIdEmpresaIdCentroCosto(ID_EMPRESA, ID_CENTRO_C);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_SUB_CENTRO.Items.Add(item);

        foreach (DataRow fila in tablaSUB_C.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOM_SUB_C"].ToString(), fila["ID_SUB_C"].ToString());
            DropDownList_SUB_CENTRO.Items.Add(item);
        }

        DropDownList_SUB_CENTRO.DataBind();
    }

    private void Cargar_DropDownList_SERVICIO_centro_c(Decimal ID_CENTRO_C)
    {
        DropDownList_SERVICIO.Items.Clear();

        condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaServicios = _condicionComercial.ObtenerServiciosPorEmpresaPorIdCentroC(ID_CENTRO_C);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_SERVICIO.Items.Add(item);

        foreach (DataRow fila in tablaServicios.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOMBRE_SERVICIO"].ToString(), fila["ID_SERVICIO"].ToString());
            DropDownList_SERVICIO.Items.Add(item);
        }

        DropDownList_SERVICIO.DataBind();
    }

    private void Cargar_DropDownList_SERVICIO_sub_c(Decimal ID_SUB_C)
    {
        DropDownList_SERVICIO.Items.Clear();

        condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaServicios = _condicionComercial.ObtenerServiciosPorEmpresaPorIdSubC(ID_SUB_C);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_SERVICIO.Items.Add(item);

        foreach (DataRow fila in tablaServicios.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOMBRE_SERVICIO"].ToString(), fila["ID_SERVICIO"].ToString());
            DropDownList_SERVICIO.Items.Add(item);
        }

        DropDownList_SERVICIO.DataBind();
    }

    private Boolean determinarIDsSubCCentrosCCiudad()
    {
        GLO_ID_SUB_C = 0;
        GLO_ID_CENTRO_C = 0;
        GLO_ID_CIUDAD = null;
        GLO_ID_SERVICIO = 0;

        if (DropDownList_SERVICIO.SelectedValue != "")
        {
            GLO_ID_SERVICIO = Convert.ToDecimal(DropDownList_SERVICIO.SelectedValue);
        }
        else
        {
            GLO_ID_SERVICIO = 0;
        }

        if (DropDownList_SUB_CENTRO.SelectedValue != "")
        {
            GLO_ID_SUB_C = Convert.ToDecimal(DropDownList_SUB_CENTRO.SelectedValue);
        }
        else
        {
            if (DropDownList_CENTRO_COSTO.SelectedValue != "")
            {
                GLO_ID_CENTRO_C = Convert.ToDecimal(DropDownList_CENTRO_COSTO.SelectedValue);
            }
            else
            {
                if (DropDownList_CIUDAD.SelectedValue != "")
                {
                    GLO_ID_CIUDAD = DropDownList_CIUDAD.SelectedValue;
                }
                else
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void EnviarCorreoAComercial()
    {
        tools _tools = new tools();
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        seguridad _seguridad = new seguridad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaUsuariosEmpresa = _seguridad.ObtenerUsuariosPorEmpresa(ID_EMPRESA);

        if (tablaUsuariosEmpresa.Rows.Count <= 0)
        {
            if (_seguridad.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _seguridad.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La ubicación seleccionada NO posee CONDICIONES COMERCIALES. Puede continuar con el proceso, pero debe informar personalmente a un Representante Comercial para que ingrese las CONDICIONES COMERCIALES para esta empresa.", Proceso.Advertencia);
            }
        }
        else
        {
            StreamReader archivo_original = new StreamReader(Server.MapPath(@"~\plantillas_reportes\email_conf_condiciones_comerciales.htm"));
            String html_formato_plantilla_email = archivo_original.ReadToEnd();
            archivo_original.Dispose();
            archivo_original.Close();

            String html_tabla_ubicacion_perfil = "<table border=\"0\" cellpadding=\"2\" cellspacing=\"0\" width=\"400px\" style=\"font-size:11px; line-height:13px;\">";
            if (DropDownList_SUB_CENTRO.SelectedIndex > 0)
            {
                html_tabla_ubicacion_perfil += "<tr>";
                html_tabla_ubicacion_perfil += "<td width=\"30%\" valign=\"middle\" style=\"text-align: left; font-weight: bold;\">";
                html_tabla_ubicacion_perfil += "CIUDAD:";
                html_tabla_ubicacion_perfil += "</td>";
                html_tabla_ubicacion_perfil += "<td width=\"70%\" valign=\"middle\" style=\"text-align: justify;\">";
                html_tabla_ubicacion_perfil += DropDownList_CIUDAD.SelectedItem.Text;
                html_tabla_ubicacion_perfil += "</td>";
                html_tabla_ubicacion_perfil += "</tr>";

                html_tabla_ubicacion_perfil += "<tr>";
                html_tabla_ubicacion_perfil += "<td width=\"30%\" valign=\"middle\" style=\"text-align: left; font-weight: bold;\">";
                html_tabla_ubicacion_perfil += "CENTRO DE COSTO:";
                html_tabla_ubicacion_perfil += "</td>";
                html_tabla_ubicacion_perfil += "<td width=\"70%\" valign=\"middle\" style=\"text-align: justify;\">";
                html_tabla_ubicacion_perfil += DropDownList_CENTRO_COSTO.SelectedItem.Text;
                html_tabla_ubicacion_perfil += "</td>";
                html_tabla_ubicacion_perfil += "</tr>";

                html_tabla_ubicacion_perfil += "<tr>";
                html_tabla_ubicacion_perfil += "<td width=\"30%\" valign=\"middle\" style=\"text-align: left; font-weight: bold;\">";
                html_tabla_ubicacion_perfil += "SUB CENTRO:";
                html_tabla_ubicacion_perfil += "</td>";
                html_tabla_ubicacion_perfil += "<td width=\"70%\" valign=\"middle\" style=\"text-align: justify;\">";
                html_tabla_ubicacion_perfil += DropDownList_SUB_CENTRO.SelectedItem.Text;
                html_tabla_ubicacion_perfil += "</td>";
                html_tabla_ubicacion_perfil += "</tr>";
            }
            else
            {
                if (DropDownList_CENTRO_COSTO.SelectedIndex > 0)
                {
                    html_tabla_ubicacion_perfil += "<tr>";
                    html_tabla_ubicacion_perfil += "<td width=\"30%\" valign=\"middle\" style=\"text-align: left; font-weight: bold;\">";
                    html_tabla_ubicacion_perfil += "CIUDAD:";
                    html_tabla_ubicacion_perfil += "</td>";
                    html_tabla_ubicacion_perfil += "<td width=\"70%\" valign=\"middle\" style=\"text-align: justify;\">";
                    html_tabla_ubicacion_perfil += DropDownList_CIUDAD.SelectedItem.Text;
                    html_tabla_ubicacion_perfil += "</td>";
                    html_tabla_ubicacion_perfil += "</tr>";

                    html_tabla_ubicacion_perfil += "<tr>";
                    html_tabla_ubicacion_perfil += "<td width=\"30%\" valign=\"middle\" style=\"text-align: left; font-weight: bold;\">";
                    html_tabla_ubicacion_perfil += "CENTRO DE COSTO:";
                    html_tabla_ubicacion_perfil += "</td>";
                    html_tabla_ubicacion_perfil += "<td width=\"70%\" valign=\"middle\" style=\"text-align: justify;\">";
                    html_tabla_ubicacion_perfil += DropDownList_CENTRO_COSTO.SelectedItem.Text;
                    html_tabla_ubicacion_perfil += "</td>";
                    html_tabla_ubicacion_perfil += "</tr>";
                }
                else
                {
                    html_tabla_ubicacion_perfil += "<tr>";
                    html_tabla_ubicacion_perfil += "<td width=\"30%\" valign=\"middle\" style=\"text-align: left; font-weight: bold;\">";
                    html_tabla_ubicacion_perfil += "CIUDAD:";
                    html_tabla_ubicacion_perfil += "</td>";
                    html_tabla_ubicacion_perfil += "<td width=\"70%\" valign=\"middle\" style=\"text-align: justify;\">";
                    html_tabla_ubicacion_perfil += DropDownList_CIUDAD.SelectedItem.Text;
                    html_tabla_ubicacion_perfil += "</td>";
                    html_tabla_ubicacion_perfil += "</tr>";
                }
            }
            html_tabla_ubicacion_perfil += "</table>";

            html_formato_plantilla_email = html_formato_plantilla_email.Replace("[RAZ_SOCIAL]", Label_INFO_ADICIONAL_MODULO.Text);
            html_formato_plantilla_email = html_formato_plantilla_email.Replace("[TABLA_UBICACION]", html_tabla_ubicacion_perfil);
            html_formato_plantilla_email = html_formato_plantilla_email.Replace("[NOM_OCUPACION]", Label_NOM_OCUPACION.Text);
            html_formato_plantilla_email = html_formato_plantilla_email.Replace("[EDAD_MIN]", Label_EDAD_MIN.Text);
            html_formato_plantilla_email = html_formato_plantilla_email.Replace("[EDAD_MAX]", Label_EDAD_MAX.Text);
            html_formato_plantilla_email = html_formato_plantilla_email.Replace("[NIVEL_ACADEMICO]", Label_NIVEL_ACADEMICO.Text);
            html_formato_plantilla_email = html_formato_plantilla_email.Replace("[EXPERIENCIA]", Label_EXPERIENCIA.Text);
            html_formato_plantilla_email = html_formato_plantilla_email.Replace("[SEXO]", Label_SEXO.Text);

            usuario _usuario = new usuario(Session["idEmpresa"].ToString());
            DataTable tablaUsuario = _usuario.ObtenerUsuarioPorUsuLog(Session["USU_LOG"].ToString());
            DataRow filaUsuario = tablaUsuario.Rows[0];

            if (filaUsuario["USU_TIPO"].ToString().ToUpper() == "PLANTA")
            {
                html_formato_plantilla_email = html_formato_plantilla_email.Replace("[NOMBRE_USUARIO]", filaUsuario["NOMBRES"].ToString().Trim() + " " + filaUsuario["APELLIDOS"].ToString().Trim());
            }
            else
            {
                html_formato_plantilla_email = html_formato_plantilla_email.Replace("[NOMBRE_USUARIO]", filaUsuario["NOMBRES_EXTERNO"].ToString().Trim() + " " + filaUsuario["APELLIDOS_EXTERNO"].ToString().Trim());
            }

            Int32 contadorEnvioEmail = 0;
            Int32 contadorErrores = 0;
            foreach (DataRow fila in tablaUsuariosEmpresa.Rows)
            {
                if (fila["UNIDAD_NEGOCIO"].ToString().Trim().Contains("REP. COMERCIAL") == true)
                {
                    if (DBNull.Value.Equals(fila["USU_MAIL"]) == false)
                    {
                        try
                        {
                            _tools.enviarCorreoConCuerpoHtml(fila["USU_MAIL"].ToString().Trim(), "CONFIGURACIÓN DE CONDICIONES COMERCIALES", html_formato_plantilla_email);
                            contadorEnvioEmail += 1;
                        }
                        catch
                        {
                            contadorErrores += 1;
                        }
                    }
                }
            }

            if (contadorEnvioEmail <= 0)
            {
                if (contadorErrores <= 0)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La ubicación seleccionada NO posee CONDICIONES COMERCIALES. Puede continuar con el proceso, pero debe informar personalmente a un Representante Comercial para que ingrese las CONDICIONES COMERCIALES para esta empresa.", Proceso.Advertencia);
                }
                else
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La ubicación seleccionada NO posee CONDICIONES COMERCIALES. Puede continuar con el proceso, pero debe informar personalmente a un Representante Comercial para que ingrese las CONDICIONES COMERCIALES para esta empresa. (Problema con envío de Emails)", Proceso.Advertencia);
                }
            }
        }
    }

    private void cargar_DropDownList_RIESGOS(Decimal ID_EMPRESA)
    {
        DropDownList_RIESGOS.Items.Clear();

        empresasRiesgos _empresasRiesgos = new empresasRiesgos(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaDatos = _empresasRiesgos.ObtenerRoesgosPorEmpresa(ID_EMPRESA);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_RIESGOS.Items.Add(item);

        foreach (DataRow fila in tablaDatos.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["DESCRIPCION_RIESGO"].ToString() + " - " + fila["CODIGO"].ToString(), fila["DESCRIPCION_RIESGO"].ToString());
            DropDownList_RIESGOS.Items.Add(item);
        }

        DropDownList_RIESGOS.DataBind();
    }

    private Boolean ItemEnLista(String[] lista, System.Web.UI.WebControls.ListItem item)
    {
        foreach (String itemLista in lista)
        {
            if (itemLista == item.Value)
            {
                return true;
            }
        }

        return false;
    }

    private void Cargar_ChecksDeDocumentosEntregablesAlTrabajador(String documentosSeleccionados)
    {
        String[] listaDocumentosSeleccionados = documentosSeleccionados.Split('•');

        CheckBoxList_Documentos1.Items.Clear();
        CheckBoxList_Documentos2.Items.Clear();
        CheckBoxList_Documentos3.Items.Clear();
        CheckBoxList_Documentos4.Items.Clear();
        CheckBoxList_Documentos5.Items.Clear();

        DocumentoEntregable _documento = new DocumentoEntregable(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaDoc = _documento.ObtenerPorEstado(true);

        if (tablaDoc.Rows.Count <= 0)
        {
            if (_documento.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _documento.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se ha creado la lista de Documentos Entregables al Trabajador.", Proceso.Advertencia);
            }
        }
        else
        {
            for (int i = 0; i < tablaDoc.Rows.Count; i++)
            {
                DataRow filaDoc = tablaDoc.Rows[i];

                System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem(filaDoc["NOMBRE"].ToString().Trim(), filaDoc["NOMBRE"].ToString().Trim());

                item.Selected = ItemEnLista(listaDocumentosSeleccionados, item);

                if (i <= 10)
                {
                    CheckBoxList_Documentos1.Items.Add(item);
                }

                if ((i > 10) && (i <= 20))
                {
                    CheckBoxList_Documentos2.Items.Add(item);
                }

                if ((i > 20) && (i <= 30))
                {
                    CheckBoxList_Documentos3.Items.Add(item);
                }

                if ((i > 30) && (i <= 40))
                {
                    CheckBoxList_Documentos4.Items.Add(item);
                }
                if ((i > 40) && (i <= 50))
                {
                    CheckBoxList_Documentos5.Items.Add(item);
                }
            }
        }
    }

    private Decimal ExisteServicioExameneszMedicos(DataTable tablaServiciosComplementarios)
    {
        Decimal resultado = 0;

        foreach (DataRow fila in tablaServiciosComplementarios.Rows)
        {
            if (fila["NOMBRE_SERVICIO_COMPLEMENTARIO"].ToString().Trim() == GLO_SERVICIO_EXAMANES_MEDICOS)
            {
                resultado = Convert.ToDecimal(fila["ID_SERVICIO_COMPLEMENTARIO"]);
                break;
            }
        }

        return resultado;
    }

    private Int32 TotalServiciosComplementariosAptosParaImplementos(DataTable tablaServiciosComplementarios)
    {
        Int32 resultado = 0;
        Boolean excluir = false;

        foreach (DataRow fila in tablaServiciosComplementarios.Rows)
        {
            excluir = false;

            foreach (String nombre in listaExclusionImplementos)
            {
                if (nombre == fila["NOMBRE_SERVICIO_COMPLEMENTARIO"].ToString().Trim())
                {
                    excluir = true;
                    break;
                }
            }

            if (excluir == false)
            {
                resultado += 1;
            }
        }

        return resultado;
    }

    private DataTable configurarTablaParaExamenesMedicos()
    {
        DataTable tablaResultado = new DataTable();

        tablaResultado.Columns.Add("REGISTRO_CON_REG_ELEMENTO_TRABAJO");
        tablaResultado.Columns.Add("REGISTRO_VEN_P_CONTRATACION");
        tablaResultado.Columns.Add("ID_PRODUCTO");
        tablaResultado.Columns.Add("CANTIDAD");
        tablaResultado.Columns.Add("CODIGO_PERIODO");
        tablaResultado.Columns.Add("CODIGO_FACTURAR_A");
        tablaResultado.Columns.Add("VALOR");
        tablaResultado.Columns.Add("PRIMERA_ENTREGA");
        tablaResultado.Columns.Add("AJUSTE_A");
        tablaResultado.Columns.Add("FECHA_INICIO");
        tablaResultado.Columns.Add("ID_SERVICIO_COMPLEMENTARIO");

        return tablaResultado;
    }

    private DataTable ConfigurarTablaParaImplementos()
    {
        DataTable tablaResultado = new DataTable();

        tablaResultado.Columns.Add("REGISTRO_CON_REG_ELEMENTO_TRABAJO_INICIAL");
        tablaResultado.Columns.Add("REGISTRO_CON_REG_ELEMENTO_TRABAJO_PROGRAMADO");
        tablaResultado.Columns.Add("REGISTRO_VEN_P_CONTRATACION");
        tablaResultado.Columns.Add("ID_PRODUCTO");

        tablaResultado.Columns.Add("CHECK_PROGRAMADO");
        tablaResultado.Columns.Add("CANTIDAD_INICIAL");
        tablaResultado.Columns.Add("CODIGO_PERIODO_INICIAL");
        tablaResultado.Columns.Add("CODIGO_FACTURAR_A_INICIAL");
        tablaResultado.Columns.Add("VALOR_INICIAL");
        tablaResultado.Columns.Add("PRIMERA_ENTREGA_INICIAL");
        tablaResultado.Columns.Add("AJUSTE_A_INICIAL");
        tablaResultado.Columns.Add("FECHA_INICIO_INICIAL");

        tablaResultado.Columns.Add("CANTIDAD_PROGRAMADO");
        tablaResultado.Columns.Add("CODIGO_PERIODO_PROGRAMADO");
        tablaResultado.Columns.Add("CODIGO_FACTURAR_A_PROGRAMADO");
        tablaResultado.Columns.Add("VALOR_PROGRAMADO");
        tablaResultado.Columns.Add("PRIMERA_ENTREGA_PROGRAMADO");
        tablaResultado.Columns.Add("AJUSTE_A_PROGRAMADO");
        tablaResultado.Columns.Add("FECHA_INICIO_PROGRAMADO");

        tablaResultado.Columns.Add("ID_SERVICIO_COMPLEMENTARIO");

        return tablaResultado;
    }

    private void Cargar_DropDownList_OBJETOS_SERVICIO(Decimal TIPO, DropDownList drop)
    {
        condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoServicioComplementario = _condicionComercial.ObtenerServiciosComplementariosPorId(TIPO);
        DataRow filaInfoServicioComplementario = tablaInfoServicioComplementario.Rows[0];

        condicionesContratacion _condicionesContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaObjetos = new DataTable();

        if (filaInfoServicioComplementario["NOMBRE_SERVICIO_COMPLEMENTARIO"].ToString() == "EXAMENES MEDICOS")
        {
            tablaObjetos = _condicionesContratacion.obtenerProductoTipoExamenMedico(TIPO);
        }
        else
        {
            tablaObjetos = _condicionesContratacion.obtenerProductosSegunTipoServicioComplementario(TIPO);
        }

        drop.Items.Clear();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        drop.Items.Add(item);

        foreach (DataRow fila in tablaObjetos.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOMBRE"].ToString(), fila["ID_PRODUCTO"].ToString());
            drop.Items.Add(item);
        }

        drop.DataBind();
    }

    private void Cargar_DropDownList_PERIODO_ENTREGA(DropDownList drop)
    {
        drop.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_PERIODO_ENTREGA_OBJETO);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        drop.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            drop.Items.Add(item);
        }
        drop.DataBind();
    }

    private void Cargar_GridView_ExamenesMedicosDesdeTabla(DataTable tablaExamenes)
    {
        GridView_ExamenesParametrizados.DataSource = tablaExamenes;
        GridView_ExamenesParametrizados.DataBind();

        for (int i = 0; i < GridView_ExamenesParametrizados.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ExamenesParametrizados.Rows[i];
            DataRow filaTabla = tablaExamenes.Rows[i];

  

            DropDownList drop_Producto = filaGrilla.FindControl("DropDownList_OBJETOS_SERVICIO") as DropDownList;
            Label label_DescripcionExamen = filaGrilla.FindControl("Label_Descripcion") as Label;
            Label label_AplicaA = filaGrilla.FindControl("Label_AplicaA") as Label;

            Decimal ID_SERVICIO_COMPLEMENTARIO = Convert.ToDecimal(filaTabla["ID_SERVICIO_COMPLEMENTARIO"]);
            Cargar_DropDownList_OBJETOS_SERVICIO(ID_SERVICIO_COMPLEMENTARIO, drop_Producto);
            Decimal ID_PRODUCTO = 0;
            try
            {
                drop_Producto.SelectedValue = filaTabla["ID_PRODUCTO"].ToString().Trim();
                ID_PRODUCTO = Convert.ToDecimal(filaTabla["ID_PRODUCTO"]);
            }
            catch
            {
                drop_Producto.SelectedIndex = 0;
                ID_PRODUCTO = 0;
            }

            if (ID_PRODUCTO == 0)
            {
                label_DescripcionExamen.Text = "Seleccione Exámen Médico.";
                label_AplicaA.Text = "Desconocido.";
            }
            else
            {
                producto _producto = new producto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                DataTable tablaProd = _producto.ObtenerAlmRegProductoPorId(Convert.ToInt32(ID_PRODUCTO));
                DataRow filaProd = tablaProd.Rows[0];

                label_DescripcionExamen.Text = filaProd["DESCRIPCION"].ToString().Trim();

                if (filaProd["APLICA_A"].ToString().Trim() == "M")
                {
                    label_AplicaA.Text = "Hombre";
                }
                else
                {
                    if (filaProd["APLICA_A"].ToString().Trim() == "F")
                    {
                        label_AplicaA.Text = "Mujer";
                    }
                    else
                    {
                        if (filaProd["APLICA_A"].ToString().Trim() == "F/M")
                        {
                            label_AplicaA.Text = "Ambos";
                        }
                        else
                        {
                            label_AplicaA.Text = "Desconocido";
                        }
                    }
                }
            }

            DropDownList drop_Periodo = filaGrilla.FindControl("DropDownList_PERIODO_ENTREGA") as DropDownList;
            Cargar_DropDownList_PERIODO_ENTREGA(drop_Periodo);
            drop_Periodo.SelectedValue = filaTabla["CODIGO_PERIODO"].ToString().Trim();

            DropDownList drop_Facturar = filaGrilla.FindControl("DropDownList_FACTURAR_A") as DropDownList;
            Cargar_DropDownList_FACTURAR_A(drop_Facturar);
            drop_Facturar.SelectedValue = filaTabla["CODIGO_FACTURAR_A"].ToString().Trim();
        }
    }

    private void Cargar_GridView_EXAMENES_SELECICONADOS_con_todos_examenesbasicos_nuevo_registro()
    {
        DataTable tablaParaGrid = configurarTablaParaExamenesMedicos();

        condicionesContratacion _condicionesContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaExamenesBasicos;
        tablaExamenesBasicos = _condicionesContratacion.obtenerExamenesBasicosTodos(tabla.SERVICIO_EMPRESA_TEMPORAL);

        foreach (DataRow filaOriginal in tablaExamenesBasicos.Rows)
        {
            DataRow filaParaGrid = tablaParaGrid.NewRow();


            filaParaGrid["REGISTRO_CON_REG_ELEMENTO_TRABAJO"] = 0;
            filaParaGrid["REGISTRO_VEN_P_CONTRATACION"] = 0;
            filaParaGrid["ID_PRODUCTO"] = Convert.ToDecimal(filaOriginal["ID_PRODUCTO"]);
            filaParaGrid["CANTIDAD"] = 1;
            filaParaGrid["CODIGO_PERIODO"] = 0;
            filaParaGrid["CODIGO_FACTURAR_A"] = "NO FACTURAR";
            filaParaGrid["VALOR"] = 0;
            filaParaGrid["PRIMERA_ENTREGA"] = "True";
            filaParaGrid["AJUSTE_A"] = tabla.EntregaAjusteA.CONTRATO.ToString();
            filaParaGrid["FECHA_INICIO"] = "";
            filaParaGrid["ID_SERVICIO_COMPLEMENTARIO"] = Convert.ToDecimal(filaOriginal["ID_SERVICIO_COMPLEMENTARIO"]);

            tablaParaGrid.Rows.Add(filaParaGrid);
        }

        Cargar_GridView_ExamenesMedicosDesdeTabla(tablaParaGrid);
    }

    private void inhabilitarFilasGrilla(GridView grilla, Int32 colInicio)
    {
        for (int i = 0; i < grilla.Rows.Count; i++)
        {
            for (int j = colInicio; j < grilla.Columns.Count; j++)
            {
                grilla.Rows[i].Cells[j].Enabled = false;
            }

        }
    }

    private void CargarInterfazNuevaCondicionSegundaParte(DataTable tablaServiciosComplementariosAsociados)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        cargar_DropDownList_RIESGOS(ID_EMPRESA);

        Cargar_ChecksDeDocumentosEntregablesAlTrabajador(String.Empty);

        if (tablaServiciosComplementariosAsociados.Rows.Count <= 0)
        {
            Panel_LISTA_IMPLEMENTOS_SELECCIONADOS.Visible = false;
            GridView_ImplementosParametrizados.DataSource = null;
            GridView_ImplementosParametrizados.DataBind();

            HiddenField_ServicioExamenesMedicos.Value = "NO";
            HiddenField_ID_PARA_SABER_ID_DE_EXAMENES_MEDICOS.Value = "";
        }
        else
        {
            if (ExisteServicioExameneszMedicos(tablaServiciosComplementariosAsociados) > 0)
            {
                PanelInfoNoConfExamenesMedicos.Visible = false;
                HiddenField_ServicioExamenesMedicos.Value = "SI";
                HiddenField_ID_PARA_SABER_ID_DE_EXAMENES_MEDICOS.Value = ExisteServicioExameneszMedicos(tablaServiciosComplementariosAsociados).ToString();
            }
            else
            {
                HiddenField_ServicioExamenesMedicos.Value = "NO";
                HiddenField_ID_PARA_SABER_ID_DE_EXAMENES_MEDICOS.Value = "";
            }

            if (TotalServiciosComplementariosAptosParaImplementos(tablaServiciosComplementariosAsociados) <= 0)
            {
                Panel_LISTA_IMPLEMENTOS_SELECCIONADOS.Visible = false;
                GridView_ImplementosParametrizados.DataSource = null;
                GridView_ImplementosParametrizados.DataBind();

                Panel_BotonesImplementos.Visible = false;
                Button_NuevoImplemento.Visible = false;
                Button_GuardarImplemento.Visible = false;
                Button_CancelarImplemento.Visible = false;
            }
            else
            {
                Panel_InfoNoConfImplementos.Visible = false;

                GridView_ImplementosParametrizados.DataSource = null;
                GridView_ImplementosParametrizados.DataBind();
            }
        }

        Panel_EXAMENES_SELECCIONADOS.Visible = true;

        Cargar_GridView_EXAMENES_SELECICONADOS_con_todos_examenesbasicos_nuevo_registro();
        inhabilitarFilasGrilla(GridView_ExamenesParametrizados, 2);

        if (HiddenField_ServicioExamenesMedicos.Value == "NO")
        {
            Button_NuevoExamen.Visible = false;
            Button_GuardarExamen.Visible = false;
            Button_CancelarExamen.Visible = false;
        }
        else
        {
            Button_NuevoExamen.Visible = true;
            Button_GuardarExamen.Visible = false;
            Button_CancelarExamen.Visible = false;
        }
    }

    private DataTable ObtenerDataTable_De_GridView_Implementos()
    {
        Decimal REGISTRO_VEN_P_CONTRATACION = 0;
        if (String.IsNullOrEmpty(HiddenField_REGISTRO_VEN_P_CONTRATACION.Value) == false)
        {
            REGISTRO_VEN_P_CONTRATACION = Convert.ToDecimal(HiddenField_REGISTRO_VEN_P_CONTRATACION.Value);
        }

        DataTable tablaResultado = ConfigurarTablaParaImplementos();

        DataRow filaTablaResultado;


        Decimal REGISTRO_CON_REG_ELEMENTO_TRABAJO_INICIAL = 0;
        Decimal REGISTRO_CON_REG_ELEMENTO_TRABAJO_PROGRAMADO = 0;
        String ID_PRODUCTO = "";

        String CHECK_PROGRAMADO = null;
        String CANTIDAD_INICIAL = "";
        String CODIGO_PERIODO_INICIAL = "";
        String CODIGO_FACTURAR_A_INICIAL = "";
        String VALOR_INICIAL = "";
        String PRIMERA_ENTREGA_INICIAL = "True";
        String AJUSTE_A_INICIAL = tabla.EntregaAjusteA.CONTRATO.ToString();
        String FECHA_INICIO_INICIAL = "";

        String CANTIDAD_PROGRAMADO = "";
        String CODIGO_PERIODO_PROGRAMADO = null;
        String CODIGO_FACTURAR_A_PROGRAMADO = null;
        String VALOR_PROGRAMADO = null;
        String PRIMERA_ENTREGA_PROGRAMADO = "False";
        String AJUSTE_A_PROGRAMADO = null;
        String FECHA_INICIO_PROGRAMADO = null;

        String ID_SERVICIO_COMPLEMENTARIO = "";

        for (int i = 0; i < GridView_ImplementosParametrizados.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ImplementosParametrizados.Rows[i];


            REGISTRO_CON_REG_ELEMENTO_TRABAJO_INICIAL = Convert.ToDecimal(GridView_ImplementosParametrizados.DataKeys[i].Values["REGISTRO_CON_REG_ELEMENTO_TRABAJO_INICIAL"]);

            DropDownList dropServicioComplementario = filaGrilla.FindControl("DropDownList_SERVICIOS_COMPLEMENTARIOS") as DropDownList;
            ID_SERVICIO_COMPLEMENTARIO = dropServicioComplementario.SelectedValue;

            DropDownList dropProducto = filaGrilla.FindControl("DropDownList_OBJETOS_SERVICIO") as DropDownList;
            ID_PRODUCTO = dropProducto.SelectedValue;

            TextBox textoCantidadInicial = filaGrilla.FindControl("TextBox_CANTIDAD_ENTREGA") as TextBox;
            CANTIDAD_INICIAL = textoCantidadInicial.Text;

            CODIGO_PERIODO_INICIAL = "";

            DropDownList dropFactaurarInicial = filaGrilla.FindControl("DropDownList_FACTURAR_A") as DropDownList;
            CODIGO_FACTURAR_A_INICIAL = dropFactaurarInicial.SelectedValue;

            TextBox textoValorInicial = filaGrilla.FindControl("TextBox_VALOR_PRODDUCTO") as TextBox;
            VALOR_INICIAL = textoValorInicial.Text;

            PRIMERA_ENTREGA_INICIAL = "True";

            AJUSTE_A_INICIAL = tabla.EntregaAjusteA.CONTRATO.ToString();

            FECHA_INICIO_INICIAL = "";


            REGISTRO_CON_REG_ELEMENTO_TRABAJO_PROGRAMADO = Convert.ToDecimal(GridView_ImplementosParametrizados.DataKeys[i].Values["REGISTRO_CON_REG_ELEMENTO_TRABAJO_PROGRAMADO"]);

            CheckBox checkProgramado = filaGrilla.FindControl("CheckBox_ProgramarEntregas") as CheckBox;
            if (checkProgramado.Checked == true)
            {
                CHECK_PROGRAMADO = "True";

                TextBox textoCantidadProgramado = filaGrilla.FindControl("TextBox_CantidadPeriodica") as TextBox;
                CANTIDAD_PROGRAMADO = textoCantidadProgramado.Text;

                DropDownList dropPeriodicidadProgramado = filaGrilla.FindControl("DropDownList_PERIODO_ENTREGA") as DropDownList;
                CODIGO_PERIODO_PROGRAMADO = dropPeriodicidadProgramado.SelectedValue;

                DropDownList dropFacturarProgramado = filaGrilla.FindControl("DropDownList_FacturarAPeriodica") as DropDownList;
                CODIGO_FACTURAR_A_PROGRAMADO = dropFacturarProgramado.SelectedValue;

                TextBox textoValorProgramado = filaGrilla.FindControl("TextBox_ValorPeriodica") as TextBox;
                VALOR_PROGRAMADO = textoValorProgramado.Text;

                PRIMERA_ENTREGA_PROGRAMADO = "False";

                DropDownList dropAjusteProgramado = filaGrilla.FindControl("DropDownList_AjusteEntrega") as DropDownList;
                AJUSTE_A_PROGRAMADO = dropAjusteProgramado.SelectedValue;
                FECHA_INICIO_PROGRAMADO = "";
                if (AJUSTE_A_PROGRAMADO == tabla.EntregaAjusteA.FECHA.ToString())
                {
                    TextBox textoFechaInicialProgramado = filaGrilla.FindControl("TextBox_FechaInicialProgramado") as TextBox;
                    FECHA_INICIO_PROGRAMADO = textoFechaInicialProgramado.Text;
                }
            }
            else
            {
                CHECK_PROGRAMADO = "False";

                CANTIDAD_PROGRAMADO = "";

                CODIGO_PERIODO_PROGRAMADO = "";

                CODIGO_FACTURAR_A_PROGRAMADO = "";

                VALOR_PROGRAMADO = "";

                PRIMERA_ENTREGA_PROGRAMADO = "False";

                AJUSTE_A_PROGRAMADO = "";
                FECHA_INICIO_PROGRAMADO = "";
            }

            filaTablaResultado = tablaResultado.NewRow();


            filaTablaResultado["REGISTRO_CON_REG_ELEMENTO_TRABAJO_INICIAL"] = REGISTRO_CON_REG_ELEMENTO_TRABAJO_INICIAL;
            filaTablaResultado["REGISTRO_CON_REG_ELEMENTO_TRABAJO_PROGRAMADO"] = REGISTRO_CON_REG_ELEMENTO_TRABAJO_PROGRAMADO;
            filaTablaResultado["REGISTRO_VEN_P_CONTRATACION"] = REGISTRO_VEN_P_CONTRATACION;
            filaTablaResultado["ID_PRODUCTO"] = ID_PRODUCTO;

            filaTablaResultado["CHECK_PROGRAMADO"] = CHECK_PROGRAMADO;
            filaTablaResultado["CANTIDAD_INICIAL"] = CANTIDAD_INICIAL;
            filaTablaResultado["CODIGO_PERIODO_INICIAL"] = CODIGO_PERIODO_INICIAL;
            filaTablaResultado["CODIGO_FACTURAR_A_INICIAL"] = CODIGO_FACTURAR_A_INICIAL;
            filaTablaResultado["VALOR_INICIAL"] = VALOR_INICIAL;
            filaTablaResultado["PRIMERA_ENTREGA_INICIAL"] = PRIMERA_ENTREGA_INICIAL;
            filaTablaResultado["AJUSTE_A_INICIAL"] = AJUSTE_A_INICIAL;
            filaTablaResultado["FECHA_INICIO_INICIAL"] = FECHA_INICIO_INICIAL;

            filaTablaResultado["CANTIDAD_PROGRAMADO"] = CANTIDAD_PROGRAMADO;
            filaTablaResultado["CODIGO_PERIODO_PROGRAMADO"] = CODIGO_PERIODO_PROGRAMADO;
            filaTablaResultado["CODIGO_FACTURAR_A_PROGRAMADO"] = CODIGO_FACTURAR_A_PROGRAMADO;
            filaTablaResultado["VALOR_PROGRAMADO"] = VALOR_PROGRAMADO;
            filaTablaResultado["PRIMERA_ENTREGA_PROGRAMADO"] = PRIMERA_ENTREGA_PROGRAMADO;
            filaTablaResultado["AJUSTE_A_PROGRAMADO"] = AJUSTE_A_PROGRAMADO;
            filaTablaResultado["FECHA_INICIO_PROGRAMADO"] = FECHA_INICIO_PROGRAMADO;

            filaTablaResultado["ID_SERVICIO_COMPLEMENTARIO"] = ID_SERVICIO_COMPLEMENTARIO;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }

    private Boolean DeterminarIDsSubCCentrosCCiudadDesdeDrops()
    {
        if (DropDownList_SUB_CENTRO.SelectedValue != "")
        {
            GLO_ID_SUB_C = Convert.ToDecimal(DropDownList_SUB_CENTRO.SelectedValue);
            GLO_ID_CENTRO_C = 0;
            GLO_ID_CIUDAD = null;
        }
        else
        {
            if (DropDownList_CENTRO_COSTO.SelectedValue != "")
            {
                GLO_ID_SUB_C = 0;
                GLO_ID_CENTRO_C = Convert.ToDecimal(DropDownList_CENTRO_COSTO.SelectedValue);
                GLO_ID_CIUDAD = null;
            }
            else
            {
                if (DropDownList_CIUDAD.SelectedValue != "")
                {
                    GLO_ID_SUB_C = 0;
                    GLO_ID_CENTRO_C = 0;
                    GLO_ID_CIUDAD = DropDownList_CIUDAD.SelectedValue;
                }
                else
                {
                    GLO_ID_SUB_C = 0;
                    GLO_ID_CENTRO_C = 0;
                    GLO_ID_CIUDAD = null;
                }
            }
        }

        try
        {
            GLO_ID_SERVICIO = Convert.ToDecimal(DropDownList_SERVICIO.SelectedValue);
        }
        catch
        {
            GLO_ID_SERVICIO = 0;
        }
        return true;
    }

    private void Cargar_DropDownList_Implementos(DropDownList drop)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        DeterminarIDsSubCCentrosCCiudadDesdeDrops();

        servicio _servicio = new servicio(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaServiciosComplementariosAsociados;

        tablaServiciosComplementariosAsociados = _servicio.ObtenerServiciosComplementariosPorUbicacion(GLO_ID_CIUDAD, GLO_ID_CENTRO_C, GLO_ID_SUB_C, GLO_ID_SERVICIO);

        drop.Items.Clear();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        drop.Items.Add(item);

        foreach (DataRow fila in tablaServiciosComplementariosAsociados.Rows)
        {
            Boolean excluir = false;

            foreach (String nombre in listaExclusionImplementos)
            {
                excluir = false;

                if (fila["NOMBRE_SERVICIO_COMPLEMENTARIO"].ToString().Trim() == nombre)
                {
                    excluir = true;
                    break;
                }
            }

            if (excluir == false)
            {
                item = new System.Web.UI.WebControls.ListItem(fila["NOMBRE_SERVICIO_COMPLEMENTARIO"].ToString(), fila["ID_SERVICIO_COMPLEMENTARIO"].ToString());
                drop.Items.Add(item);
            }
        }
        drop.DataBind();
    }

    private void Cargar_DropDownList_FACTURAR_A(DropDownList drop)
    {
        drop.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_FACTURACION_EXAMENES);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        drop.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            drop.Items.Add(item);
        }
        drop.DataBind();
    }

    private void Cargar_DropDownList_AjusteA(DropDownList drop)
    {
        drop.Items.Clear();

        drop.Items.Add(new ListItem("Seleccione...", ""));

        drop.Items.Add(new ListItem("Contrato", tabla.EntregaAjusteA.CONTRATO.ToString()));
        drop.Items.Add(new ListItem("Fecha", tabla.EntregaAjusteA.FECHA.ToString()));

        drop.DataBind();
    }

    private void CargarGridView_ImplementosDesdeTabla(DataTable tablaImplementos)
    {
        GridView_ImplementosParametrizados.DataSource = tablaImplementos;
        GridView_ImplementosParametrizados.DataBind();

        for (int i = 0; i < GridView_ImplementosParametrizados.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ImplementosParametrizados.Rows[i];
            DataRow filaTabla = tablaImplementos.Rows[i];

            DropDownList dropServicioComplementario = filaGrilla.FindControl("DropDownList_SERVICIOS_COMPLEMENTARIOS") as DropDownList;
            Cargar_DropDownList_Implementos(dropServicioComplementario);
            dropServicioComplementario.SelectedValue = filaTabla["ID_SERVICIO_COMPLEMENTARIO"].ToString();

            DropDownList dropProducto = filaGrilla.FindControl("DropDownList_OBJETOS_SERVICIO") as DropDownList;
            if (dropServicioComplementario.SelectedValue == "")
            {
                dropProducto.Items.Clear();
                dropProducto.Items.Add(new System.Web.UI.WebControls.ListItem("Seleccione...", ""));
            }
            else
            {
                Decimal ID_SERICIO_COMPLEMENTARIO = Convert.ToDecimal(filaTabla["ID_SERVICIO_COMPLEMENTARIO"]);

                condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                DataTable tablaInfoServicioComplementario = _condicionComercial.ObtenerServiciosComplementariosPorId(ID_SERICIO_COMPLEMENTARIO);

                DataRow filaInfoServicioComplementario = tablaInfoServicioComplementario.Rows[0];

                Cargar_DropDownList_OBJETOS_SERVICIO(ID_SERICIO_COMPLEMENTARIO, dropProducto);
                dropProducto.SelectedValue = filaTabla["ID_PRODUCTO"].ToString();
            }

            TextBox textoCantidadInicial = filaGrilla.FindControl("TextBox_CANTIDAD_ENTREGA") as TextBox;
            textoCantidadInicial.Text = filaTabla["CANTIDAD_INICIAL"].ToString();

            DropDownList dropFactaurarInicial = filaGrilla.FindControl("DropDownList_FACTURAR_A") as DropDownList;
            Cargar_DropDownList_FACTURAR_A(dropFactaurarInicial);
            dropFactaurarInicial.SelectedValue = filaTabla["CODIGO_FACTURAR_A_INICIAL"].ToString();

            TextBox textoValorInicial = filaGrilla.FindControl("TextBox_VALOR_PRODDUCTO") as TextBox;
            textoValorInicial.Text = filaTabla["VALOR_INICIAL"].ToString();

            CheckBox checkProgramado = filaGrilla.FindControl("CheckBox_ProgramarEntregas") as CheckBox;
            TextBox textoCantidadProgramado = filaGrilla.FindControl("TextBox_CantidadPeriodica") as TextBox;
            DropDownList dropPeriodicidadProgramado = filaGrilla.FindControl("DropDownList_PERIODO_ENTREGA") as DropDownList;
            Cargar_DropDownList_PERIODO_ENTREGA(dropPeriodicidadProgramado);
            DropDownList dropFacturarProgramado = filaGrilla.FindControl("DropDownList_FacturarAPeriodica") as DropDownList;
            Cargar_DropDownList_FACTURAR_A(dropFacturarProgramado);
            TextBox textoValorProgramado = filaGrilla.FindControl("TextBox_ValorPeriodica") as TextBox;
            DropDownList dropAjusteProgramado = filaGrilla.FindControl("DropDownList_AjusteEntrega") as DropDownList;
            Cargar_DropDownList_AjusteA(dropAjusteProgramado);
            TextBox textoFechaInicialProgramado = filaGrilla.FindControl("TextBox_FechaInicialProgramado") as TextBox;

            if (filaTabla["CHECK_PROGRAMADO"].ToString() == "True")
            {
                checkProgramado.Checked = true;

                textoCantidadProgramado.Text = filaTabla["CANTIDAD_PROGRAMADO"].ToString();

                dropPeriodicidadProgramado.SelectedValue = filaTabla["CODIGO_PERIODO_PROGRAMADO"].ToString();

                dropFacturarProgramado.SelectedValue = filaTabla["CODIGO_FACTURAR_A_PROGRAMADO"].ToString();

                textoValorProgramado.Text = filaTabla["VALOR_PROGRAMADO"].ToString();

                dropAjusteProgramado.SelectedValue = filaTabla["AJUSTE_A_PROGRAMADO"].ToString();
                textoFechaInicialProgramado.Text = "";
                if (dropAjusteProgramado.SelectedValue == tabla.EntregaAjusteA.FECHA.ToString())
                {
                    textoFechaInicialProgramado.Text = filaTabla["FECHA_INICIO_PROGRAMADO"].ToString();
                }
            }
            else
            {
                checkProgramado.Checked = false;

                textoCantidadProgramado.Text = "";

                dropPeriodicidadProgramado.SelectedValue = "";

                dropFacturarProgramado.SelectedValue = "";

                textoValorProgramado.Text = "";

                dropAjusteProgramado.SelectedValue = "";
                textoFechaInicialProgramado.Text = "";
            }
        }
    }

    private void HabilitarFilaGrillaImplementos(GridView grid, Int32 numFila, Int32 columnaInicio)
    {
        GridViewRow filaGrilla = grid.Rows[numFila];

        for (int i = columnaInicio; i < grid.Columns.Count; i++)
        {
            grid.Rows[numFila].Cells[i].Enabled = true;
        }

        DropDownList dropServicioComplementario = filaGrilla.FindControl("DropDownList_SERVICIOS_COMPLEMENTARIOS") as DropDownList;
        dropServicioComplementario.Enabled = true;

        DropDownList dropProducto = filaGrilla.FindControl("DropDownList_OBJETOS_SERVICIO") as DropDownList;
        dropProducto.Enabled = true;

        TextBox textoCantidadInicial = filaGrilla.FindControl("TextBox_CANTIDAD_ENTREGA") as TextBox;
        textoCantidadInicial.Enabled = true;

        DropDownList dropFactaurarInicial = filaGrilla.FindControl("DropDownList_FACTURAR_A") as DropDownList;
        dropFactaurarInicial.Enabled = true;

        TextBox textoValorInicial = filaGrilla.FindControl("TextBox_VALOR_PRODDUCTO") as TextBox;
        textoValorInicial.Enabled = true;

        CheckBox checkProgramado = filaGrilla.FindControl("CheckBox_ProgramarEntregas") as CheckBox;
        checkProgramado.Enabled = true;
        TextBox textoCantidadProgramado = filaGrilla.FindControl("TextBox_CantidadPeriodica") as TextBox;
        DropDownList dropPeriodicidadProgramado = filaGrilla.FindControl("DropDownList_PERIODO_ENTREGA") as DropDownList;
        DropDownList dropFacturarProgramado = filaGrilla.FindControl("DropDownList_FacturarAPeriodica") as DropDownList;
        TextBox textoValorProgramado = filaGrilla.FindControl("TextBox_ValorPeriodica") as TextBox;
        DropDownList dropAjusteProgramado = filaGrilla.FindControl("DropDownList_AjusteEntrega") as DropDownList;
        TextBox textoFechaInicialProgramado = filaGrilla.FindControl("TextBox_FechaInicialProgramado") as TextBox;

        if (checkProgramado.Checked == true)
        {
            textoCantidadProgramado.Enabled = true;

            dropPeriodicidadProgramado.Enabled = true;

            dropFacturarProgramado.Enabled = true;

            textoValorProgramado.Enabled = true;

            dropAjusteProgramado.Enabled = true;
            if (dropAjusteProgramado.SelectedValue == tabla.EntregaAjusteA.FECHA.ToString())
            {
                textoFechaInicialProgramado.Enabled = true;
            }
            else
            {
                textoFechaInicialProgramado.Enabled = false;
            }
        }
        else
        {
            textoCantidadProgramado.Enabled = false;

            dropPeriodicidadProgramado.Enabled = false;

            dropFacturarProgramado.Enabled = false;

            textoValorProgramado.Enabled = false;

            dropAjusteProgramado.Enabled = false;
            textoFechaInicialProgramado.Enabled = false;
        }
    }

    private void AjustarEstadoValidadoresGrillaImplementos(GridView grid)
    {
        for (int i = 0; i < grid.Rows.Count; i++)
        {
            AjustarEstadoValidadoresGrillaImplementosPorFila(grid, i);
        }
    }

    private void AjustarEstadoValidadoresGrillaImplementosPorFila(GridView grid, Int32 numFila)
    {
        GridViewRow filaGrilla = grid.Rows[numFila];

        CheckBox checkProgramado = filaGrilla.FindControl("CheckBox_ProgramarEntregas") as CheckBox;

        DropDownList dropPeriodicidadProgramado = filaGrilla.FindControl("DropDownList_PERIODO_ENTREGA") as DropDownList;
        RequiredFieldValidator rfv_Periodo = filaGrilla.FindControl("RequiredFieldValidator_DropDownList_PERIODO_ENTREGA") as RequiredFieldValidator;
        ValidatorCalloutExtender vce_Periodo = filaGrilla.FindControl("ValidatorCalloutExtender_DropDownList_PERIODO_ENTREGA") as ValidatorCalloutExtender;

        DropDownList dropAjusteA = filaGrilla.FindControl("DropDownList_AjusteEntrega") as DropDownList;
        RequiredFieldValidator rfv_Ajuste = filaGrilla.FindControl("RequiredFieldValidator_DropDownList_AjusteEntrega") as RequiredFieldValidator;
        ValidatorCalloutExtender vce_Ajuste = filaGrilla.FindControl("ValidatorCalloutExtender_DropDownList_AjusteEntrega") as ValidatorCalloutExtender;

        TextBox textoFechaInicia = filaGrilla.FindControl("TextBox_FechaInicialProgramado") as TextBox;
        RequiredFieldValidator rfv_FechaInicia = filaGrilla.FindControl("RequiredFieldValidator_TextBox_FechaInicialProgramado") as RequiredFieldValidator;
        ValidatorCalloutExtender vce_FechaInicia = filaGrilla.FindControl("ValidatorCalloutExtender_TextBox_FechaInicialProgramado") as ValidatorCalloutExtender;

        TextBox textoCantidadProgramado = filaGrilla.FindControl("TextBox_CantidadPeriodica") as TextBox;
        RequiredFieldValidator rfv_Cantidad = filaGrilla.FindControl("RequiredFieldValidator_TextBox_CantidadPeriodica") as RequiredFieldValidator;
        ValidatorCalloutExtender vce_Cantidad = filaGrilla.FindControl("ValidatorCalloutExtender_TextBox_CantidadPeriodica") as ValidatorCalloutExtender;

        DropDownList dropFacturarA = filaGrilla.FindControl("DropDownList_FacturarAPeriodica") as DropDownList;
        RequiredFieldValidator rfv_Facturar = filaGrilla.FindControl("RequiredFieldValidator_DropDownList_FacturarAPeriodica") as RequiredFieldValidator;
        ValidatorCalloutExtender vce_Facturar = filaGrilla.FindControl("ValidatorCalloutExtender_DropDownList_FacturarAPeriodica") as ValidatorCalloutExtender;

        if (checkProgramado.Checked == false)
        {
            rfv_Periodo.Enabled = false;
            vce_Periodo.Enabled = false;

            rfv_Ajuste.Enabled = false;
            vce_Ajuste.Enabled = false;

            rfv_FechaInicia.Enabled = false;
            vce_FechaInicia.Enabled = false;

            rfv_Cantidad.Enabled = false;
            vce_Cantidad.Enabled = false;

            rfv_Facturar.Enabled = false;
            vce_Facturar.Enabled = false;
        }
        else
        {
            rfv_Periodo.Enabled = true;
            vce_Periodo.Enabled = true;

            rfv_Ajuste.Enabled = true;
            vce_Ajuste.Enabled = true;

            if (dropAjusteA.SelectedValue == tabla.EntregaAjusteA.FECHA.ToString())
            {
                rfv_FechaInicia.Enabled = true;
                vce_FechaInicia.Enabled = true;
            }
            else
            {
                rfv_FechaInicia.Enabled = false;
                vce_FechaInicia.Enabled = false;
            }

            rfv_Cantidad.Enabled = true;
            vce_Cantidad.Enabled = true;

            rfv_Facturar.Enabled = true;
            vce_Facturar.Enabled = true;
        }
    }

    private DataTable ObtenerDataTable_De_GridView_ExamenesMedicos()
    {
        DataTable tablaResultado = configurarTablaParaExamenesMedicos();

        DataRow filaTablaResultado;

        Decimal REGISTRO_CON_REG_ELEMENTO_TRABAJO = 0;
        Decimal REGISTRO_VEN_P_CONTRATACION = 0;
        Decimal ID_PRODUCTO = 0;
        Int32 CANTIDAD = 1;
        String CODIGO_PERIODO = null;
        String CODIGO_FACTURAR_A = null;
        Decimal VALOR = 0;
        String PRIMERA_ENTREGA = "True";
        String AJUSTE_A = tabla.EntregaAjusteA.CONTRATO.ToString();
        String FECHA_INICIO = "";
        Decimal ID_SERVICIO_COMPLEMENTARIO = 0;

        for (int i = 0; i < GridView_ExamenesParametrizados.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ExamenesParametrizados.Rows[i];

            REGISTRO_CON_REG_ELEMENTO_TRABAJO = Convert.ToDecimal(GridView_ExamenesParametrizados.DataKeys[i].Values["REGISTRO_CON_REG_ELEMENTO_TRABAJO"]);

            REGISTRO_VEN_P_CONTRATACION = Convert.ToDecimal(GridView_ExamenesParametrizados.DataKeys[i].Values["REGISTRO_VEN_P_CONTRATACION"]);

            DropDownList drop_Producto = filaGrilla.FindControl("DropDownList_OBJETOS_SERVICIO") as DropDownList;
            if (drop_Producto.SelectedIndex <= 0)
            {
                ID_PRODUCTO = 0;
            }
            else
            {
                ID_PRODUCTO = Convert.ToDecimal(drop_Producto.SelectedValue);
            }

            DropDownList drop_Periodo = filaGrilla.FindControl("DropDownList_PERIODO_ENTREGA") as DropDownList;
            CODIGO_PERIODO = drop_Periodo.SelectedValue;

            DropDownList drop_Facturar = filaGrilla.FindControl("DropDownList_FACTURAR_A") as DropDownList;
            CODIGO_FACTURAR_A = drop_Facturar.SelectedValue;

            ID_SERVICIO_COMPLEMENTARIO = Convert.ToDecimal(GridView_ExamenesParametrizados.DataKeys[i].Values["ID_SERVICIO_COMPLEMENTARIO"]);

            filaTablaResultado = tablaResultado.NewRow();


            filaTablaResultado["REGISTRO_CON_REG_ELEMENTO_TRABAJO"] = REGISTRO_CON_REG_ELEMENTO_TRABAJO;
            filaTablaResultado["REGISTRO_VEN_P_CONTRATACION"] = REGISTRO_VEN_P_CONTRATACION;
            filaTablaResultado["ID_PRODUCTO"] = ID_PRODUCTO;
            filaTablaResultado["CANTIDAD"] = CANTIDAD;
            filaTablaResultado["CODIGO_PERIODO"] = CODIGO_PERIODO;
            filaTablaResultado["CODIGO_FACTURAR_A"] = CODIGO_FACTURAR_A;
            filaTablaResultado["VALOR"] = VALOR;
            filaTablaResultado["PRIMERA_ENTREGA"] = PRIMERA_ENTREGA;
            filaTablaResultado["AJUSTE_A"] = AJUSTE_A;
            filaTablaResultado["FECHA_INICIO"] = FECHA_INICIO;
            filaTablaResultado["ID_SERVICIO_COMPLEMENTARIO"] = ID_SERVICIO_COMPLEMENTARIO;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }

    private void habilitarFilaGrilla(GridView grilla, Int32 numFila, Int32 colInicio)
    {
        for (int i = colInicio; i < grilla.Columns.Count; i++)
        {
            grilla.Rows[numFila].Cells[i].Enabled = true;
        }
    }

    private void inhabilitarFilaGrilla(GridView grilla, Int32 numFila, Int32 colInicio)
    {
        for (int i = colInicio; i < grilla.Columns.Count; i++)
        {
            grilla.Rows[numFila].Cells[i].Enabled = false;
        }
    }

    private bool Validar(GridView gridView)
    {
        bool validado = false;
        foreach (GridViewRow gridViewRow in gridView.Rows)
        {
            if (((CheckBox)gridViewRow.FindControl("CheckBox_aplicar")).Checked == true)
            {
                validado = true;
                break;
            }
        }
        return validado;
    }


    #endregion metodos

    #region eventos
    protected void Button_CERRAR_MENSAJE_Click(object sender, System.EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        if (HiddenField_ACCION_GRILLA_IMPLEMENTOS.Value != AccionesGrilla.Ninguna.ToString())
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Para poder continuar debe terminar las acciones sobre la grilla de parametrización de servicios complementarios.", Proceso.Advertencia);
        }
        else
        {
            if (HiddenField_ACCION_GRILLA_EXAMENES.Value != AccionesGrilla.Ninguna.ToString())
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Para poder continuar debe terminar las acciones sobre la grilla de parametrización de exámenes médicos.", Proceso.Advertencia);
            }
            else
            {
                if (String.IsNullOrEmpty(HiddenField_REGISTRO_VEN_P_CONTRATACION.Value) == true)
                {
                    Guardar();
                }
                else
                {
                    Actualizar();
                }
            }
        }
    }

    protected void Button_MOMDIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.ModificarCondicion);
        Mostrar(Acciones.ModificarCondicion);
        Activar(Acciones.ModificarCondicion);
        Cargar(Acciones.ModificarCondicion);
    }

    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(HiddenField_REGISTRO_VEN_P_CONTRATACION.Value) == false)
        {
            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.CargarCondicion);
            Desactivar(Acciones.Inicio);

            Decimal REGISTRO_VEN_P_CONTRATACION = Convert.ToDecimal(HiddenField_REGISTRO_VEN_P_CONTRATACION.Value);

            Cargar(REGISTRO_VEN_P_CONTRATACION);
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);
        }
    }

    protected void Button_VOLVER_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);

        Cargar_GridViewPerfiles();
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

        TextBox_BUSCAR.Focus();
    }

    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "CON_FILTRO";
        HiddenField_FILTRO_DROP.Value = DropDownList_BUSCAR.SelectedValue;
        HiddenField_FILTRO_DATO.Value = TextBox_BUSCAR.Text.Trim();

        HiddenField_GridPagina.Value = "0";

        Cargar_GridViewPerfiles();
    }

    protected void GridView_PERFILES_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal ID_PERFIL = Convert.ToDecimal(GridView_PERFILES.DataKeys[indexSeleccionado].Values["REGISTRO"]);

            HiddenField_ID_PERFIL.Value = ID_PERFIL.ToString();

            HiddenField_REGISTRO_VEN_P_CONTRATACION.Value = "";

            CargarInformacionPerfilSeleccionado(ID_PERFIL);


            CargarCondicionesDeUnPerfil(ID_PERFIL);

            Clausula clausula = new Clausula(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            tools _tools = new tools();
            SecureQueryString QueryStringSeguro;
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

            Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

            GridView_clausulas.DataSource = clausula.ObtenerPorPerfil(ID_EMPRESA, ID_PERFIL);
            GridView_clausulas.DataBind();

            DataTable dataTable = clausula.ObtenerClausulasAplicadasAlPerfil(ID_PERFIL);
            if (dataTable.Rows.Count.Equals(0)) return;
            foreach (DataRow dataRow in dataTable.Rows)
            {
                for (int i = 0; i < GridView_clausulas.Rows.Count; i++)
                {
                    if (dataRow["ID_CLAUSULA"].Equals(GridView_clausulas.DataKeys[i]["ID_CLAUSULA"]))
                    {
                        ((CheckBox)GridView_clausulas.Rows[i].FindControl("CheckBox_aplicar")).Checked = true;
                    }
                }
            }
        }
    }

    protected void GridView_PERFILES_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        HiddenField_GridPagina.Value = e.NewPageIndex.ToString();

        Cargar_GridViewPerfiles();
    }

    protected void GridView_LISTA_CONFIGURACION_ACTUAL_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal REGISTRO_VEN_P_CONTRATACION = Convert.ToDecimal(GridView_LISTA_CONFIGURACION_ACTUAL.DataKeys[indexSeleccionado].Values["REGISTRO"]);

            HiddenField_REGISTRO_VEN_P_CONTRATACION.Value = REGISTRO_VEN_P_CONTRATACION.ToString();

            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.CargarCondicion);
            Desactivar(Acciones.Inicio);

            Cargar(REGISTRO_VEN_P_CONTRATACION);
        }
    }

    protected void Button_NUEVA_CONDICION_CONTRATACION_Click(object sender, EventArgs e)
    {
        cargarInterfazNuevaCondicion();
    }

    protected void DropDownList_CIUDAD_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_CIUDAD.SelectedValue == "")
        {
            Cargar_DropDownListVacio(DropDownList_CENTRO_COSTO);
            Cargar_DropDownListVacio(DropDownList_SUB_CENTRO);
            Cargar_DropDownListVacio(DropDownList_SERVICIO);
        }
        else
        {
            Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

            String ID_CIUDAD = DropDownList_CIUDAD.SelectedValue;

            Cargar_DropDownList_CENTRO_COSTO(ID_EMPRESA, ID_CIUDAD);
            Cargar_DropDownListVacio(DropDownList_SUB_CENTRO);
            Cargar_DropDownListVacio(DropDownList_SERVICIO);

            Cargar_DropDownList_SERVICIO_ciudad(ID_CIUDAD, ID_EMPRESA);
        }
    }

    protected void DropDownList_CENTRO_COSTO_SelectedIndexChanged(object sender, EventArgs e)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        if (DropDownList_CENTRO_COSTO.SelectedValue == "")
        {
            Cargar_DropDownListVacio(DropDownList_SUB_CENTRO);

            String ID_CIUDAD = DropDownList_CIUDAD.SelectedValue;
            Cargar_DropDownList_SERVICIO_ciudad(ID_CIUDAD, ID_EMPRESA);        }
        else
        {
            Decimal ID_CENTRO_C = Convert.ToDecimal(DropDownList_CENTRO_COSTO.SelectedValue);

            Cargar_DropDownList_SUB_CENTRO(ID_EMPRESA, ID_CENTRO_C);

            Cargar_DropDownList_SERVICIO_centro_c(ID_CENTRO_C);
        }
    }

    protected void DropDownList_SUB_CENTRO_SelectedIndexChanged(object sender, EventArgs e)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        if (DropDownList_SUB_CENTRO.SelectedValue == "")
        {
            Decimal ID_CENTRO_C = Convert.ToDecimal(DropDownList_CENTRO_COSTO.SelectedValue);
            Cargar_DropDownList_SERVICIO_centro_c(ID_CENTRO_C);
        }
        else
        {
            Decimal ID_SUB_C = Convert.ToDecimal(DropDownList_SUB_CENTRO.SelectedValue);
            Cargar_DropDownList_SERVICIO_sub_c(ID_SUB_C);
        }
    }

    protected void Button_EMPEZAR_CONFIGURACION_Click(object sender, EventArgs e)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        Decimal ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);

        Boolean verificador = true;
        verificador = determinarIDsSubCCentrosCCiudad();

        if (verificador == true)
        {
            servicio _servicio = new servicio(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaServiciosComplementariosAsociados;

            condicionesContratacion _condicionesContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaCondicionContratacion;

            condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaCondicionComercial;

            tablaCondicionContratacion = _condicionesContratacion.ObtenerCondicionContratacionPorUbicacion(ID_PERFIL, GLO_ID_CIUDAD, GLO_ID_CENTRO_C, GLO_ID_SUB_C, GLO_ID_SERVICIO);
            tablaServiciosComplementariosAsociados = _servicio.ObtenerServiciosComplementariosPorUbicacion(GLO_ID_CIUDAD, GLO_ID_CENTRO_C, GLO_ID_SUB_C, GLO_ID_SERVICIO);

            if (tablaCondicionContratacion.Rows.Count > 0)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La UBICACIÓN, PERFIL Y/Ó SERVICIO seleccionado YA POSEE CONDICIONES DE CONTRATACIÓN configuradas.", Proceso.Advertencia);
            }
            else
            {
                tablaCondicionComercial = _condicionComercial.ObtenerCondicionesEconomicasPorId(ID_EMPRESA, GLO_ID_CIUDAD, GLO_ID_CENTRO_C, GLO_ID_SUB_C);

                if (tablaCondicionComercial.Rows.Count <= 0)
                {
                    EnviarCorreoAComercial();
                }

                Ocultar(Acciones.Inicio);
                Desactivar(Acciones.Inicio);
                Mostrar(Acciones.NuevaCondicionSegundaParte);
                Activar(Acciones.NuevaCondicionSegundaParte);

                CargarInterfazNuevaCondicionSegundaParte(tablaServiciosComplementariosAsociados);
            }
        }
        else
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar por lo mínimo una ciudad, para aplicar nuevas condiciones.", Proceso.Advertencia);
        }
    }
    
    protected void DropDownList_SERVICIOS_COMPLEMENTARIOS_SelectedIndexChanged(object sender, EventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_IMPLEMENTOS.Value);

        DropDownList drop_ServicioComplementario = GridView_ImplementosParametrizados.Rows[indexSeleccionado].FindControl("DropDownList_SERVICIOS_COMPLEMENTARIOS") as DropDownList;
        DropDownList drop_Objetos = GridView_ImplementosParametrizados.Rows[indexSeleccionado].FindControl("DropDownList_OBJETOS_SERVICIO") as DropDownList;

        if (drop_ServicioComplementario.SelectedValue == "")
        {
            drop_Objetos.Items.Clear();
            drop_Objetos.Items.Add(new System.Web.UI.WebControls.ListItem("Seleccione...", ""));
            drop_Objetos.DataBind();
        }
        else
        {
            Decimal ID_SERICIO_COMPLEMENTARIO = Convert.ToDecimal(drop_ServicioComplementario.SelectedValue);

            condicionComercial _condicionComercial = new condicionComercial(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaInfoServicioComplementario = _condicionComercial.ObtenerServiciosComplementariosPorId(ID_SERICIO_COMPLEMENTARIO);

            DataRow filaInfoServicioComplementario = tablaInfoServicioComplementario.Rows[0];

            Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

            Cargar_DropDownList_OBJETOS_SERVICIO(ID_SERICIO_COMPLEMENTARIO, drop_Objetos);
        }
    }

    protected void DropDownList_AjusteEntrega_SelectedIndexChanged(object sender, EventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_IMPLEMENTOS.Value);

        GridViewRow filaGrilla = GridView_ImplementosParametrizados.Rows[indexSeleccionado];

        DropDownList dropAjusteA = filaGrilla.FindControl("DropDownList_AjusteEntrega") as DropDownList;
        TextBox textoFechaInicio = filaGrilla.FindControl("TextBox_FechaInicialProgramado") as TextBox;
        RequiredFieldValidator rfv_FechaInicia = filaGrilla.FindControl("RequiredFieldValidator_TextBox_FechaInicialProgramado") as RequiredFieldValidator;
        ValidatorCalloutExtender vce_FechaInicia = filaGrilla.FindControl("ValidatorCalloutExtender_TextBox_FechaInicialProgramado") as ValidatorCalloutExtender;


        if (dropAjusteA.SelectedValue == tabla.EntregaAjusteA.FECHA.ToString())
        {
            textoFechaInicio.Text = "";
            textoFechaInicio.Enabled = true;

            rfv_FechaInicia.Enabled = true;
            vce_FechaInicia.Enabled = true;
        }
        else
        {
            textoFechaInicio.Text = "";
            textoFechaInicio.Enabled = false;

            rfv_FechaInicia.Enabled = false;
            vce_FechaInicia.Enabled = false;
        }
    }

    protected void CheckBox_ProgramarEntregas_CheckedChanged(object sender, EventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_IMPLEMENTOS.Value);

        GridViewRow filaGrilla = GridView_ImplementosParametrizados.Rows[indexSeleccionado];

        CheckBox checkProgramado = filaGrilla.FindControl("CheckBox_ProgramarEntregas") as CheckBox;
        
        DropDownList dropPeriodicidadProgramado = filaGrilla.FindControl("DropDownList_PERIODO_ENTREGA") as DropDownList;
        RequiredFieldValidator rfv_Periodo = filaGrilla.FindControl("RequiredFieldValidator_DropDownList_PERIODO_ENTREGA") as RequiredFieldValidator;
        ValidatorCalloutExtender vce_Periodo = filaGrilla.FindControl("ValidatorCalloutExtender_DropDownList_PERIODO_ENTREGA") as ValidatorCalloutExtender;

        DropDownList dropAjusteA = filaGrilla.FindControl("DropDownList_AjusteEntrega") as DropDownList;
        RequiredFieldValidator rfv_Ajuste = filaGrilla.FindControl("RequiredFieldValidator_DropDownList_AjusteEntrega") as RequiredFieldValidator;
        ValidatorCalloutExtender vce_Ajuste = filaGrilla.FindControl("ValidatorCalloutExtender_DropDownList_AjusteEntrega") as ValidatorCalloutExtender;

        TextBox textoFechaInicia = filaGrilla.FindControl("TextBox_FechaInicialProgramado") as TextBox;
        RequiredFieldValidator rfv_FechaInicia = filaGrilla.FindControl("RequiredFieldValidator_TextBox_FechaInicialProgramado") as RequiredFieldValidator;
        ValidatorCalloutExtender vce_FechaInicia = filaGrilla.FindControl("ValidatorCalloutExtender_TextBox_FechaInicialProgramado") as ValidatorCalloutExtender;

        TextBox textoCantidadProgramado = filaGrilla.FindControl("TextBox_CantidadPeriodica") as TextBox;
        RequiredFieldValidator rfv_Cantidad = filaGrilla.FindControl("RequiredFieldValidator_TextBox_CantidadPeriodica") as RequiredFieldValidator;
        ValidatorCalloutExtender vce_Cantidad = filaGrilla.FindControl("ValidatorCalloutExtender_TextBox_CantidadPeriodica") as ValidatorCalloutExtender;

        DropDownList dropFacturarA = filaGrilla.FindControl("DropDownList_FacturarAPeriodica") as DropDownList;
        RequiredFieldValidator rfv_Facturar = filaGrilla.FindControl("RequiredFieldValidator_DropDownList_FacturarAPeriodica") as RequiredFieldValidator;
        ValidatorCalloutExtender vce_Facturar = filaGrilla.FindControl("ValidatorCalloutExtender_DropDownList_FacturarAPeriodica") as ValidatorCalloutExtender;

        TextBox textoValorProgramado = filaGrilla.FindControl("TextBox_ValorPeriodica") as TextBox;

        if (checkProgramado.Checked == false)
        {
            dropPeriodicidadProgramado.Enabled = false;
            dropPeriodicidadProgramado.SelectedIndex = 0;

            dropAjusteA.Enabled = false;
            dropAjusteA.SelectedIndex = 0;

            textoFechaInicia.Text = "";
            textoFechaInicia.Enabled = false;

            textoCantidadProgramado.Enabled = false;
            textoCantidadProgramado.Text = "";

            dropFacturarA.SelectedIndex = 0;
            dropFacturarA.Enabled = false;

            textoValorProgramado.Enabled = false;
            textoValorProgramado.Text = "";

            rfv_Periodo.Enabled = false;
            vce_Periodo.Enabled = false;

            rfv_Ajuste.Enabled = false;
            vce_Ajuste.Enabled = false;

            rfv_FechaInicia.Enabled = false;
            vce_FechaInicia.Enabled = false;

            rfv_Cantidad.Enabled = false;
            vce_Cantidad.Enabled = false;

            rfv_Facturar.Enabled = false;
            vce_Facturar.Enabled = false;

        }
        else
        {
            dropPeriodicidadProgramado.Enabled = true;
            dropPeriodicidadProgramado.SelectedIndex = 0;

            dropAjusteA.Enabled = true;
            dropAjusteA.SelectedIndex = 0;

            textoFechaInicia.Text = "";
            textoFechaInicia.Enabled = false;

            textoCantidadProgramado.Enabled = true;
            textoCantidadProgramado.Text = "";

            dropFacturarA.SelectedIndex = 0;
            dropFacturarA.Enabled = true;

            textoValorProgramado.Enabled = true;
            textoValorProgramado.Text = "";

            rfv_Periodo.Enabled = true;
            vce_Periodo.Enabled = true;

            rfv_Ajuste.Enabled = true;
            vce_Ajuste.Enabled = true;

            if (dropAjusteA.SelectedValue == tabla.EntregaAjusteA.FECHA.ToString())
            {
                rfv_FechaInicia.Enabled = true;
                vce_FechaInicia.Enabled = true;
            }
            else
            {
                rfv_FechaInicia.Enabled = false;
                vce_FechaInicia.Enabled = false;
            }

            rfv_Cantidad.Enabled = true;
            vce_Cantidad.Enabled = true;

            rfv_Facturar.Enabled = true;
            vce_Facturar.Enabled = true;
        }
    }

    protected void Button_NuevoImplemento_Click(object sender, EventArgs e)
    {
        Decimal REGISTRO_VEN_P_CONTRATACION = 0;
        if (String.IsNullOrEmpty(HiddenField_REGISTRO_VEN_P_CONTRATACION.Value) == false)
        {
            REGISTRO_VEN_P_CONTRATACION = Convert.ToDecimal(HiddenField_REGISTRO_VEN_P_CONTRATACION.Value);
        }

        DataTable tablaDesdeGrilla = ObtenerDataTable_De_GridView_Implementos();

        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["REGISTRO_CON_REG_ELEMENTO_TRABAJO_INICIAL"] = 0;
        filaNueva["REGISTRO_CON_REG_ELEMENTO_TRABAJO_PROGRAMADO"] = 0;
        filaNueva["REGISTRO_VEN_P_CONTRATACION"] = REGISTRO_VEN_P_CONTRATACION;
        filaNueva["ID_PRODUCTO"] = "";

        filaNueva["CANTIDAD_INICIAL"] = "";
        filaNueva["CODIGO_PERIODO_INICIAL"] = "";
        filaNueva["CODIGO_FACTURAR_A_INICIAL"] = "";
        filaNueva["VALOR_INICIAL"] = "";
        filaNueva["PRIMERA_ENTREGA_INICIAL"] = "True";
        filaNueva["AJUSTE_A_INICIAL"] = tabla.EntregaAjusteA.CONTRATO.ToString();
        filaNueva["FECHA_INICIO_INICIAL"] = "";

        filaNueva["CHECK_PROGRAMADO"] = "False";
        filaNueva["CANTIDAD_PROGRAMADO"] = "";
        filaNueva["CODIGO_PERIODO_PROGRAMADO"] = "";
        filaNueva["CODIGO_FACTURAR_A_PROGRAMADO"] = "";
        filaNueva["VALOR_PROGRAMADO"] = "";
        filaNueva["PRIMERA_ENTREGA_PROGRAMADO"] = "False";
        filaNueva["AJUSTE_A_PROGRAMADO"] = "";
        filaNueva["FECHA_INICIO_PROGRAMADO"] = "";

        filaNueva["ID_SERVICIO_COMPLEMENTARIO"] = "";

        tablaDesdeGrilla.Rows.Add(filaNueva);

        CargarGridView_ImplementosDesdeTabla(tablaDesdeGrilla);

        inhabilitarFilasGrilla(GridView_ImplementosParametrizados, 2);
        HabilitarFilaGrillaImplementos(GridView_ImplementosParametrizados, GridView_ImplementosParametrizados.Rows.Count - 1, 2);
        AjustarEstadoValidadoresGrillaImplementos(GridView_ImplementosParametrizados);

        HiddenField_ACCION_GRILLA_IMPLEMENTOS.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_SELECCIONADA_GRILLA_IMPLEMENTOS.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();

        GridView_ImplementosParametrizados.Columns[0].Visible = false;
        GridView_ImplementosParametrizados.Columns[1].Visible = false;

        Button_NuevoImplemento.Visible = false;
        Button_GuardarImplemento.Visible = true;
        Button_CancelarImplemento.Visible = true;
    }

    protected void Button_GuardarImplemento_Click(object sender, EventArgs e)
    {
        int FILA_SELECCIONADA = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_IMPLEMENTOS.Value);

        inhabilitarFilaGrilla(GridView_ImplementosParametrizados, FILA_SELECCIONADA, 2);

        GridView_ImplementosParametrizados.Columns[0].Visible = true;
        GridView_ImplementosParametrizados.Columns[1].Visible = true;

        Button_GuardarImplemento.Visible = false;
        Button_CancelarImplemento.Visible = false;
        Button_NuevoImplemento.Visible = true;

        HiddenField_ACCION_GRILLA_IMPLEMENTOS.Value = AccionesGrilla.Ninguna.ToString();
    }

    protected void Button_CancelarImplemento_Click(object sender, EventArgs e)
    {
        Decimal REGISTRO_VEN_P_CONTRATACION = 0;
        if (String.IsNullOrEmpty(HiddenField_REGISTRO_VEN_P_CONTRATACION.Value) == false)
        {
            REGISTRO_VEN_P_CONTRATACION = Convert.ToDecimal(HiddenField_REGISTRO_VEN_P_CONTRATACION.Value);
        }

        DataTable tablaGrilla = ObtenerDataTable_De_GridView_Implementos();

        if (HiddenField_ACCION_GRILLA_IMPLEMENTOS.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }
        else
        {
            if (HiddenField_ACCION_GRILLA_IMPLEMENTOS.Value == AccionesGrilla.Modificar.ToString())
            {
                DataRow filaGrilla = tablaGrilla.Rows[Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_IMPLEMENTOS.Value)];


                filaGrilla["REGISTRO_CON_REG_ELEMENTO_TRABAJO_INICIAL"] = HiddenField_REGISTRO_ELEMENTO_INICIAL.Value;
                filaGrilla["REGISTRO_CON_REG_ELEMENTO_TRABAJO_PROGRAMADO"] = HiddenField_REGISTRO_ELEMENTO_PROGRAMADO.Value;
                filaGrilla["REGISTRO_VEN_P_CONTRATACION"] = REGISTRO_VEN_P_CONTRATACION.ToString();
                filaGrilla["ID_PRODUCTO"] = HiddenField_ID_PRODUCTO.Value;

                filaGrilla["CANTIDAD_INICIAL"] = HiddenField_CANTIDAD_INICIAL.Value;
                filaGrilla["CODIGO_PERIODO_INICIAL"] = "0";
                filaGrilla["CODIGO_FACTURAR_A_INICIAL"] = HiddenField_FACTURAR_A_INICIAL.Value;
                filaGrilla["VALOR_INICIAL"] = HiddenField_VALOR_INICIAL.Value;
                filaGrilla["PRIMERA_ENTREGA_INICIAL"] = "True";
                filaGrilla["AJUSTE_A_INICIAL"] = tabla.EntregaAjusteA.CONTRATO.ToString();
                filaGrilla["FECHA_INICIO_INICIAL"] = "";

                filaGrilla["CHECK_PROGRAMADO"] = HiddenField_PROGRAMADA.Value;
                filaGrilla["CANTIDAD_PROGRAMADO"] = HiddenField_CANTIDAD_PROGRAMADA.Value;
                filaGrilla["CODIGO_PERIODO_PROGRAMADO"] = HiddenField_PERIODICIDAD.Value;
                filaGrilla["CODIGO_FACTURAR_A_PROGRAMADO"] = HiddenField_FACTURAR_A_PROGRAMADA.Value;
                filaGrilla["VALOR_PROGRAMADO"] = HiddenField_VALOR_PROGRAMADA.Value;
                filaGrilla["PRIMERA_ENTREGA_PROGRAMADO"] = "False";
                filaGrilla["AJUSTE_A_PROGRAMADO"] = HiddenField_AJUSTE_A.Value;
                filaGrilla["FECHA_INICIO_PROGRAMADO"] = HiddenField_FECHA_INICIO_PROGRAMADA.Value;

                filaGrilla["ID_SERVICIO_COMPLEMENTARIO"] = HiddenField_ID_SERVICIO_COMPLEMENTARIO.Value;
            }
        }

        CargarGridView_ImplementosDesdeTabla(tablaGrilla);
        inhabilitarFilasGrilla(GridView_ImplementosParametrizados, 2);
        AjustarEstadoValidadoresGrillaImplementos(GridView_ImplementosParametrizados);

        GridView_ImplementosParametrizados.Columns[0].Visible = true;
        GridView_ImplementosParametrizados.Columns[1].Visible = true;

        HiddenField_ACCION_GRILLA_IMPLEMENTOS.Value = AccionesGrilla.Ninguna.ToString();

        Button_NuevoImplemento.Visible = true;
        Button_GuardarImplemento.Visible = false;
        Button_CancelarImplemento.Visible = false;
    }

    protected void GridView_ImplementosParametrizados_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "modificar")
        {
            HiddenField_FILA_SELECCIONADA_GRILLA_IMPLEMENTOS.Value = indexSeleccionado.ToString();
            HabilitarFilaGrillaImplementos(GridView_ImplementosParametrizados, indexSeleccionado, 2);
            AjustarEstadoValidadoresGrillaImplementosPorFila(GridView_ImplementosParametrizados, indexSeleccionado);

            HiddenField_ACCION_GRILLA_IMPLEMENTOS.Value = AccionesGrilla.Modificar.ToString();
            HiddenField_FILA_SELECCIONADA_GRILLA_IMPLEMENTOS.Value = indexSeleccionado.ToString();

            GridViewRow filaGrilla = GridView_ImplementosParametrizados.Rows[indexSeleccionado];

            

            HiddenField_REGISTRO_ELEMENTO_INICIAL.Value = GridView_ImplementosParametrizados.DataKeys[indexSeleccionado].Values["REGISTRO_CON_REG_ELEMENTO_TRABAJO_INICIAL"].ToString();

            DropDownList dropServicioComplementario = filaGrilla.FindControl("DropDownList_SERVICIOS_COMPLEMENTARIOS") as DropDownList;
            HiddenField_ID_SERVICIO_COMPLEMENTARIO.Value = dropServicioComplementario.SelectedValue;

            DropDownList dropProducto = filaGrilla.FindControl("DropDownList_OBJETOS_SERVICIO") as DropDownList;
            HiddenField_ID_PRODUCTO.Value = dropProducto.SelectedValue;

            TextBox textoCantidadInicial = filaGrilla.FindControl("TextBox_CANTIDAD_ENTREGA") as TextBox;
            HiddenField_CANTIDAD_INICIAL.Value = textoCantidadInicial.Text;

            DropDownList dropFactaurarInicial = filaGrilla.FindControl("DropDownList_FACTURAR_A") as DropDownList;
            HiddenField_FACTURAR_A_INICIAL.Value = dropFactaurarInicial.SelectedValue;

            TextBox textoValorInicial = filaGrilla.FindControl("TextBox_VALOR_PRODDUCTO") as TextBox;
            HiddenField_VALOR_INICIAL.Value = textoValorInicial.Text;

            HiddenField_REGISTRO_ELEMENTO_PROGRAMADO.Value = GridView_ImplementosParametrizados.DataKeys[indexSeleccionado].Values["REGISTRO_CON_REG_ELEMENTO_TRABAJO_PROGRAMADO"].ToString();

            CheckBox checkProgramado = filaGrilla.FindControl("CheckBox_ProgramarEntregas") as CheckBox;
            if (checkProgramado.Checked == true)
            {
                HiddenField_PROGRAMADA.Value = "True";
            }
            else
            {
                HiddenField_PROGRAMADA.Value = "False";
            }

            DropDownList dropPeriodicidadProgramado = filaGrilla.FindControl("DropDownList_PERIODO_ENTREGA") as DropDownList;
            HiddenField_PERIODICIDAD.Value = dropPeriodicidadProgramado.SelectedValue;

            DropDownList dropAjusteProgramado = filaGrilla.FindControl("DropDownList_AjusteEntrega") as DropDownList;
            HiddenField_AJUSTE_A.Value = dropAjusteProgramado.SelectedValue;

            TextBox textoFechaInicialProgramado = filaGrilla.FindControl("TextBox_FechaInicialProgramado") as TextBox;
            HiddenField_FECHA_INICIO_PROGRAMADA.Value = textoFechaInicialProgramado.Text;

            TextBox textoCantidadProgramado = filaGrilla.FindControl("TextBox_CantidadPeriodica") as TextBox;
            HiddenField_CANTIDAD_PROGRAMADA.Value = textoCantidadProgramado.Text;

            DropDownList dropFacturarProgramado = filaGrilla.FindControl("DropDownList_FacturarAPeriodica") as DropDownList;
            HiddenField_FACTURAR_A_PROGRAMADA.Value = dropFacturarProgramado.SelectedValue;

            TextBox textoValorProgramado = filaGrilla.FindControl("TextBox_ValorPeriodica") as TextBox;
            HiddenField_VALOR_PROGRAMADA.Value = textoValorProgramado.Text;

            GridView_ImplementosParametrizados.Columns[0].Visible = false;
            GridView_ImplementosParametrizados.Columns[1].Visible = false;

            Button_NuevoImplemento.Visible = false;
            Button_GuardarImplemento.Visible = true;
            Button_CancelarImplemento.Visible = true;
        }
        else
        {
            if (e.CommandName == "eliminar")
            {
                DataTable tablaDesdeGrilla = ObtenerDataTable_De_GridView_Implementos();

                tablaDesdeGrilla.Rows.RemoveAt(indexSeleccionado);

                CargarGridView_ImplementosDesdeTabla(tablaDesdeGrilla);
                AjustarEstadoValidadoresGrillaImplementos(GridView_ImplementosParametrizados);
                inhabilitarFilasGrilla(GridView_ImplementosParametrizados, 2);

                GridView_ImplementosParametrizados.Columns[0].Visible = true;
                GridView_ImplementosParametrizados.Columns[1].Visible = true;

                HiddenField_ACCION_GRILLA_IMPLEMENTOS.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_FILA_SELECCIONADA_GRILLA_IMPLEMENTOS.Value = null;

                Button_NuevoImplemento.Visible = true;
                Button_GuardarImplemento.Visible = false;
                Button_CancelarImplemento.Visible = false;
            }
        }
    }

    protected void GridView_ExamenesParametrizados_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "modificar")
        {
            HiddenField_FILA_SELECCIONADA_GRILLA_EXAMENES.Value = indexSeleccionado.ToString();

            habilitarFilaGrilla(GridView_ExamenesParametrizados, indexSeleccionado, 3);

            HiddenField_ACCION_GRILLA_EXAMENES.Value = AccionesGrilla.Modificar.ToString();
            HiddenField_FILA_SELECCIONADA_GRILLA_EXAMENES.Value = indexSeleccionado.ToString();

            
            HiddenField_REGISTRO_CON_REG_ELEMENTO_TRABAJO_EXAMENES.Value = GridView_ExamenesParametrizados.DataKeys[indexSeleccionado].Values["REGISTRO_CON_REG_ELEMENTO_TRABAJO"].ToString();

            HiddenField_REGISTRO_VEN_P_CONTRATACION_EXAMENES.Value = GridView_ExamenesParametrizados.DataKeys[indexSeleccionado].Values["REGISTRO_VEN_P_CONTRATACION"].ToString();

            DropDownList dropProducto = GridView_ExamenesParametrizados.Rows[indexSeleccionado].FindControl("DropDownList_OBJETOS_SERVICIO") as DropDownList;
            HiddenField_ID_PRODUCTO_EXAMENES.Value = dropProducto.SelectedValue;

            HiddenField_CANTIDAD_EXAMENES.Value = "1";

            DropDownList dropPeriodo = GridView_ExamenesParametrizados.Rows[indexSeleccionado].FindControl("DropDownList_PERIODO_ENTREGA") as DropDownList;
            HiddenField_CODIGO_PERIODO_EXAMENES.Value = dropPeriodo.SelectedValue;

            DropDownList dropFacturar = GridView_ExamenesParametrizados.Rows[indexSeleccionado].FindControl("DropDownList_FACTURAR_A") as DropDownList;
            HiddenField_CODIGO_FACTURAR_A_EXAMENES.Value = dropFacturar.SelectedValue;

            HiddenField_VALOR_EXAMENES.Value = "0";

            HiddenField_PRIMERA_ENTREGA_EXAMENES.Value = "True";

            HiddenField_AJUSTE_A_EXAMENES.Value = tabla.EntregaAjusteA.CONTRATO.ToString();

            HiddenField_FECHA_INICIO_EXAMENES.Value = "";

            HiddenField_ID_SERVICIO_COMPLEMENTARIO_EXAMENES.Value = GridView_ExamenesParametrizados.DataKeys[indexSeleccionado].Values["ID_SERVICIO_COMPLEMENTARIO"].ToString();

            GridView_ExamenesParametrizados.Columns[0].Visible = false;
            GridView_ExamenesParametrizados.Columns[1].Visible = false;

            Button_NuevoExamen.Visible = false;
            Button_GuardarExamen.Visible = true;
            Button_CancelarExamen.Visible = true;
        }
        else
        {
            if (e.CommandName == "eliminar")
            {
                DataTable tablaDesdeGrilla = ObtenerDataTable_De_GridView_ExamenesMedicos();

                tablaDesdeGrilla.Rows.RemoveAt(indexSeleccionado);

                Cargar_GridView_ExamenesMedicosDesdeTabla(tablaDesdeGrilla);

                inhabilitarFilasGrilla(GridView_ExamenesParametrizados, 2);

                GridView_ExamenesParametrizados.Columns[0].Visible = true;
                GridView_ExamenesParametrizados.Columns[1].Visible = true;

                HiddenField_ACCION_GRILLA_EXAMENES.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_FILA_SELECCIONADA_GRILLA_EXAMENES.Value = null;

                if (HiddenField_ServicioExamenesMedicos.Value == "NO")
                {
                    Button_NuevoExamen.Visible = false;
                }
                else
                {
                    Button_NuevoExamen.Visible = true;
                }
                Button_GuardarExamen.Visible = false;
                Button_CancelarExamen.Visible = false;
            }
        }
    }

    protected void Button_NuevoExamen_Click(object sender, EventArgs e)
    {
        Decimal REGISTRO_VEN_P_CONTRATACION = 0;
        if(String.IsNullOrEmpty(HiddenField_REGISTRO_VEN_P_CONTRATACION.Value) == false)
        {
            REGISTRO_VEN_P_CONTRATACION = Convert.ToDecimal(HiddenField_REGISTRO_VEN_P_CONTRATACION.Value);
        }

        DataTable tablaDesdeGrilla = ObtenerDataTable_De_GridView_ExamenesMedicos();

        DataRow filaNueva = tablaDesdeGrilla.NewRow();


        filaNueva["REGISTRO_CON_REG_ELEMENTO_TRABAJO"] = 0;
        filaNueva["REGISTRO_VEN_P_CONTRATACION"] = REGISTRO_VEN_P_CONTRATACION;
        filaNueva["ID_PRODUCTO"] = 0;
        filaNueva["CANTIDAD"] = 1;
        filaNueva["CODIGO_PERIODO"] = "";
        filaNueva["CODIGO_FACTURAR_A"] = "";
        filaNueva["VALOR"] = 0;
        filaNueva["PRIMERA_ENTREGA"] = "True";
        filaNueva["AJUSTE_A"] = tabla.EntregaAjusteA.CONTRATO.ToString();
        filaNueva["FECHA_INICIO"] = "";
        filaNueva["ID_SERVICIO_COMPLEMENTARIO"] = Convert.ToDecimal(HiddenField_ID_PARA_SABER_ID_DE_EXAMENES_MEDICOS.Value);

        tablaDesdeGrilla.Rows.Add(filaNueva);

        Cargar_GridView_ExamenesMedicosDesdeTabla(tablaDesdeGrilla);

        inhabilitarFilasGrilla(GridView_ExamenesParametrizados, 2);
        habilitarFilaGrilla(GridView_ExamenesParametrizados, GridView_ExamenesParametrizados.Rows.Count - 1, 2);

        HiddenField_ACCION_GRILLA_EXAMENES.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_SELECCIONADA_GRILLA_EXAMENES.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();

        GridView_ExamenesParametrizados.Columns[0].Visible = false;
        GridView_ExamenesParametrizados.Columns[1].Visible = false;

        Button_NuevoExamen.Visible = false;
        Button_GuardarExamen.Visible = true;
        Button_CancelarExamen.Visible = true;
    }

    protected void Button_GuardarExamen_Click(object sender, EventArgs e)
    {
        int FILA_SELECCIONADA = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_EXAMENES.Value);

        inhabilitarFilaGrilla(GridView_ExamenesParametrizados, FILA_SELECCIONADA, 2);

        GridView_ExamenesParametrizados.Columns[0].Visible = true;
        GridView_ExamenesParametrizados.Columns[1].Visible = true;

        Button_GuardarExamen.Visible = false;
        Button_CancelarExamen.Visible = false;

        if (HiddenField_ServicioExamenesMedicos.Value == "NO")
        {
            Button_NuevoExamen.Visible = false;
        }
        else
        {
            Button_NuevoExamen.Visible = true;
        }

        HiddenField_ACCION_GRILLA_EXAMENES.Value = AccionesGrilla.Ninguna.ToString();
    }
    
    protected void Button_CancelarExamen_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = ObtenerDataTable_De_GridView_ExamenesMedicos();

        if (HiddenField_ACCION_GRILLA_EXAMENES.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }
        else
        {
            if (HiddenField_ACCION_GRILLA_EXAMENES.Value == AccionesGrilla.Modificar.ToString())
            {
                DataRow filaGrilla = tablaGrilla.Rows[Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_EXAMENES.Value)];


                filaGrilla["REGISTRO_CON_REG_ELEMENTO_TRABAJO"] = HiddenField_REGISTRO_CON_REG_ELEMENTO_TRABAJO_EXAMENES.Value;
                filaGrilla["REGISTRO_VEN_P_CONTRATACION"] = HiddenField_REGISTRO_VEN_P_CONTRATACION_EXAMENES.Value;
                filaGrilla["ID_PRODUCTO"] = HiddenField_ID_PRODUCTO_EXAMENES.Value;
                filaGrilla["CANTIDAD"] = HiddenField_CANTIDAD_EXAMENES.Value;
                filaGrilla["CODIGO_PERIODO"] = HiddenField_CODIGO_PERIODO_EXAMENES.Value;
                filaGrilla["CODIGO_FACTURAR_A"] = HiddenField_CODIGO_FACTURAR_A_EXAMENES.Value;
                filaGrilla["VALOR"] = HiddenField_VALOR_EXAMENES.Value;
                filaGrilla["PRIMERA_ENTREGA"] = HiddenField_PRIMERA_ENTREGA_EXAMENES.Value;
                filaGrilla["AJUSTE_A"] = HiddenField_AJUSTE_A_EXAMENES.Value;
                filaGrilla["FECHA_INICIO"] = HiddenField_FECHA_INICIO_EXAMENES.Value;
                filaGrilla["ID_SERVICIO_COMPLEMENTARIO"] = HiddenField_ID_SERVICIO_COMPLEMENTARIO_EXAMENES.Value;
            }
        }

        Cargar_GridView_ExamenesMedicosDesdeTabla(tablaGrilla);

        inhabilitarFilasGrilla(GridView_ExamenesParametrizados, 2);

        GridView_ExamenesParametrizados.Columns[0].Visible = true;
        GridView_ExamenesParametrizados.Columns[1].Visible = true;

        HiddenField_ACCION_GRILLA_EXAMENES.Value = AccionesGrilla.Ninguna.ToString();

        if (HiddenField_ServicioExamenesMedicos.Value == "NO")
        {
            Button_NuevoExamen.Visible = false;
        }
        else
        {
            Button_NuevoExamen.Visible = true;
        }
        Button_GuardarExamen.Visible = false;
        Button_CancelarExamen.Visible = false;
    }
    
    protected void DropDownList_OBJETOS_SERVICIO_SelectedIndexChanged(object sender, EventArgs e)
    {
        Int32 indexSeleccionado = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_EXAMENES.Value);
        GridViewRow filaGrilla = GridView_ExamenesParametrizados.Rows[indexSeleccionado];

        DropDownList drop_Producto = filaGrilla.FindControl("DropDownList_OBJETOS_SERVICIO") as DropDownList;
        Label label_DescripcionExamen = filaGrilla.FindControl("Label_Descripcion") as Label;
        Label label_AplicaA = filaGrilla.FindControl("Label_AplicaA") as Label;

        if (drop_Producto.SelectedIndex <= 0)
        {
            label_DescripcionExamen.Text = "Seleccione Exámen Médico.";
            label_AplicaA.Text = "Desconocido.";
        }
        else
        {
            Decimal ID_PRODUCTO = Convert.ToDecimal(drop_Producto.SelectedValue);
            producto _producto = new producto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaProd = _producto.ObtenerAlmRegProductoPorId(Convert.ToInt32(ID_PRODUCTO));
            DataRow filaProd = tablaProd.Rows[0];

            label_DescripcionExamen.Text = filaProd["DESCRIPCION"].ToString().Trim();

            if (filaProd["APLICA_A"].ToString().Trim() == "M")
            {
                label_AplicaA.Text = "Hombre";
            }
            else
            {
                if (filaProd["APLICA_A"].ToString().Trim() == "F")
                {
                    label_AplicaA.Text = "Mujer";
                }
                else
                {
                    if (filaProd["APLICA_A"].ToString().Trim() == "F/M")
                    {
                        label_AplicaA.Text = "Ambos";
                    }
                    else
                    {
                        label_AplicaA.Text = "Desconocido";
                    }
                }
            }
        }
    }
    #endregion eventos
}