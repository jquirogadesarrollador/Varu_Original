using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using Brainsbits.LLB.seleccion;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB;

using TSHAK.Components;

public partial class _Default : System.Web.UI.Page
{
    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2

    }
    #region carga de drop y grids

    private void cargar_DropDownList_SEXO()
    { 
        DropDownList_SEXO.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_SEXO);

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_SEXO.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_SEXO.Items.Add(item);
        }

        DropDownList_SEXO.DataBind();
    }
    private void cargar_DropDownList_TIP_DOC_IDENTIDAD()
    {
        DropDownList_TIP_DOC_IDENTIDAD.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_TIP_DOC_ID);

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_TIP_DOC_IDENTIDAD.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_TIP_DOC_IDENTIDAD.Items.Add(item);
        }
        DropDownList_TIP_DOC_IDENTIDAD.DataBind();
    }
    private void cargar_DropDownList_DEPARTAMENTO_CEDULA()
    {
        DropDownList_DEPARTAMENTO_CEDULA.Items.Clear();

        departamento _departamento = new departamento(Session["idEmpresa"].ToString());
        DataTable tablaDepartamentos = _departamento.ObtenerTodasLosDepartamentos();

        ListItem item = new ListItem("Seleccione Departamento", "");
        DropDownList_DEPARTAMENTO_CEDULA.Items.Add(item);

        foreach (DataRow fila in tablaDepartamentos.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_DEPARTAMENTO"].ToString());
            DropDownList_DEPARTAMENTO_CEDULA.Items.Add(item);
        }

        DropDownList_DEPARTAMENTO_CEDULA.DataBind();
    }
    private void cargar_DropDownList_CIU_CEDULA(String idDepartamento)
    {
        DropDownList_CIU_CEDULA.Items.Clear();

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
        DataTable tablaCiudades = _ciudad.ObtenerCiudadesPorIdDepartamento(idDepartamento);

        ListItem item = new ListItem("Seleccione Ciudad", "");
        DropDownList_CIU_CEDULA.Items.Add(item);

        foreach (DataRow fila in tablaCiudades.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_CIUDAD"].ToString());
            DropDownList_CIU_CEDULA.Items.Add(item);
        }

        DropDownList_CIU_CEDULA.DataBind();
    }
    private void cargar_DropDownList_DEPARTAMENTO_ASPIRANTE()
    {
        DropDownList_DEPARTAMENTO_ASPIRANTE.Items.Clear();

        departamento _departamento = new departamento(Session["idEmpresa"].ToString());
        DataTable tablaDepartamentos = _departamento.ObtenerTodasLosDepartamentos();

        ListItem item = new ListItem("Seleccione Departamento", "");
        DropDownList_DEPARTAMENTO_ASPIRANTE.Items.Add(item);

        foreach (DataRow fila in tablaDepartamentos.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_DEPARTAMENTO"].ToString());
            DropDownList_DEPARTAMENTO_ASPIRANTE.Items.Add(item);
        }

        DropDownList_DEPARTAMENTO_ASPIRANTE.DataBind();
    }
    private void cargar_DropDownList_CIU_ASPIRANTE(String idDepartamento)
    {
        DropDownList_CIU_ASPIRANTE.Items.Clear();

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
        DataTable tablaCiudades = _ciudad.ObtenerCiudadesPorIdDepartamento(idDepartamento);

        ListItem item = new ListItem("Seleccione Ciudad", "");
        DropDownList_CIU_ASPIRANTE.Items.Add(item);

        foreach (DataRow fila in tablaCiudades.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_CIUDAD"].ToString());
            DropDownList_CIU_ASPIRANTE.Items.Add(item);
        }

        DropDownList_CIU_ASPIRANTE.DataBind();
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
    private void cargar_DropDownList_ID_OCUPACION_TODOS()
    {
        DropDownList_ID_OCUPACION.Items.Clear();

        ListItem item = new ListItem("Ninguno Seleccionado", "");
        DropDownList_ID_OCUPACION.Items.Add(item);

        cargo _cargo = new cargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCargosEncontrados = _cargo.ObtenerRecOcupacionesPorTodo();

        foreach (DataRow fila in tablaCargosEncontrados.Rows)
        {
            item = new ListItem(fila["NOM_OCUPACION"].ToString(), fila["ID_OCUPACION"].ToString());
            DropDownList_ID_OCUPACION.Items.Add(item);
        }
        DropDownList_ID_OCUPACION.DataBind();
    }
    private void cargar_DropDownList_ID_OCUPACION_FILTRADO()
    {
        DropDownList_ID_OCUPACION.Items.Clear();

        ListItem item = new ListItem("Ninguno Seleccionado", "");
        DropDownList_ID_OCUPACION.Items.Add(item);

        cargo _cargo = new cargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        String NOMBRE_OCUPACION_A_BUSCAR = TextBox_BUSCADOR_CARGO.Text.ToUpper().Trim();

        DataTable tablaCargosEncontrados = _cargo.ObtenerRecOcupacionesPorNomOcupacion(NOMBRE_OCUPACION_A_BUSCAR);

        foreach (DataRow fila in tablaCargosEncontrados.Rows)
        {
            item = new ListItem(fila["NOM_OCUPACION"].ToString(), fila["ID_OCUPACION"].ToString());
            DropDownList_ID_OCUPACION.Items.Add(item);
        }
        DropDownList_ID_OCUPACION.DataBind();
    }
    private void cargar_DropDownList_ID_OCUPACION_EN_BLANCO()
    {
        DropDownList_ID_OCUPACION.Items.Clear();

        ListItem item = new ListItem("Ninguno Seleccionado", "");
        DropDownList_ID_OCUPACION.Items.Add(item);

        DropDownList_ID_OCUPACION.DataBind();
    }
    private void cargar_DropDownList_ID_FUENTE()
    {
        DropDownList_ID_FUENTE.Items.Clear();

        fuentesReclutamiento _fuentesReclutamiento = new fuentesReclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaFuentes = _fuentesReclutamiento.ObtenerRecFuentesTodos();

        ListItem item = new ListItem("Seleccione Fuente", "");
        DropDownList_ID_FUENTE.Items.Add(item);

        foreach (DataRow fila in tablaFuentes.Rows)
        {
            item = new ListItem(fila["NOM_FUENTE"].ToString(), fila["ID_FUENTE"].ToString());
            DropDownList_ID_FUENTE.Items.Add(item);
        }

        DropDownList_ID_FUENTE.DataBind();
    }
    private void cargar_DropDownList_CAT_LIC_COND()
    {
        DropDownList_CAT_LIC_COND.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_CAT_LICENCIA_CONDUCCION);

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_CAT_LIC_COND.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_CAT_LIC_COND.Items.Add(item);
        }
        DropDownList_CAT_LIC_COND.DataBind();
    }
    private void cargar_DropDownList_AREAS_ESPECIALIZACION()
    {
        DropDownList_AREAS_ESPECIALIZACION.Items.Clear();

        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaAreasEspecializacion = _radicacionHojasDeVida.ObtenerAreasInteresLaboral();

        ListItem item = new ListItem("Seleccione Área", "");
        DropDownList_AREAS_ESPECIALIZACION.Items.Add(item);

        foreach (DataRow fila in tablaAreasEspecializacion.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["ID_AREAINTERES"].ToString());
            DropDownList_AREAS_ESPECIALIZACION.Items.Add(item);
        }

        DropDownList_AREAS_ESPECIALIZACION.DataBind();
    }
    private void cargar_GridView_BITACORA()
    {
     
        int ID_SOLICITUD = Convert.ToInt32(TextBox_ID_SOLICITUD.Text);

        regRegsitrosHojaVida _regRegsitrosHojaVida = new regRegsitrosHojaVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaRegRegistro = _regRegsitrosHojaVida.ObtenerPorIdSolicitud(ID_SOLICITUD);

        GridView_BITACORA.DataSource = tablaRegRegistro;
        GridView_BITACORA.DataBind();
        Panel_BITACORA_HOJA.Visible = true;
    }
    private void cargar_DropDownList_BUSCAR()
    {
        DropDownList_BUSCAR.Items.Clear();
        ListItem item = new ListItem("Ninguno", "");
        DropDownList_BUSCAR.Items.Add(item);
        item = new ListItem("Numero de Identidad", "NUM_DOC_IDENTIDAD");
        DropDownList_BUSCAR.Items.Add(item);
        item = new ListItem("Nombres", "NOMBRES");
        DropDownList_BUSCAR.Items.Add(item);
        item = new ListItem("Apellidos", "APELLIDOS");
        DropDownList_BUSCAR.Items.Add(item);
        DropDownList_BUSCAR.DataBind();
    }
    
    #endregion
    
    #region configuracion de controles

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

    private void configurarBotonesDeAccion(Boolean bModificar, Boolean bGuardar, Boolean bCancelar)
    {


    }

    private void configurarInfoAdicionalModulo(Boolean ver, String texto)
    {
        Panel_INFO_ADICIONAL_MODULO.Visible = ver;
        Label_INFO_ADICIONAL_MODULO.Text = texto;
    }

    private void configurarCaracteresAceptadosBusqueda(Boolean letras, Boolean numeros)
    {
        FilteredTextBoxExtender_TextBox_BUSCAR_Numbers.Enabled = numeros;
        FilteredTextBoxExtender_TextBox_BUSCAR_Letras.Enabled = letras;
    }
    private void cargarInterfazEnBlanco()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String NUM_DOC_IDENTIDAD = QueryStringSeguro["cedula"].ToString();

        Panel_CONTROL_REGISTRO.Visible = false;

        Panel_ID_SOLICITUD.Visible = false;

        Panel_DATOS_SOLICITUD.Visible = true;

        TextBox_FECHA_R.Text = DateTime.Now.ToShortDateString();
        TextBox_FECHA_R.Enabled = false;

        Panel_ESTADO_ASPIRANTE.Visible = false;

        TextBox_APELLIDOS.Text = "";
        TextBox_NOMBRES.Text = "";
        cargar_DropDownList_SEXO();

        cargar_DropDownList_TIP_DOC_IDENTIDAD();
        TextBox_NUM_DOC_IDENTIDAD.Text = NUM_DOC_IDENTIDAD;
        TextBox_NUM_DOC_IDENTIDAD.Enabled = false;

        cargar_DropDownList_DEPARTAMENTO_CEDULA();
        DropDownList_DEPARTAMENTO_CEDULA.Enabled = true;
        inhabilitar_DropDownList_CIU_CEDULA();

        TextBox_LIB_MILITAR.Text = "";

        TextBox_DIR_ASPIRANTE.Text = "";
        cargar_DropDownList_DEPARTAMENTO_ASPIRANTE();
        inhabilitar_DropDownList_CIU_ASPIRANTE();
        TextBox_SECTOR.Text = "";

        cargar_DropDownList_NIV_EDUCACION();
        cargar_DropDownList_ID_OCUPACION_EN_BLANCO();
        cargar_DropDownList_ID_FUENTE();
        cargar_DropDownList_AREAS_ESPECIALIZACION();
        
        TextBox_TEL_ASPIRANTE.Text = "";
        TextBox_E_MAIL.Text = "";
        cargar_DropDownList_CAT_LIC_COND();
    }
    private void cargarInterfazConDatos(DataTable tablaInfoSolicitud)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String accion = QueryStringSeguro["accion"].ToString();

        Panel_FORM_BOTONES_1.Visible = true;
        if (accion == "modificar")
        {
            Panel_FORMULARIO.Enabled = true;
            configurarBotonesDeAccion(false, true, true);
        }
        else
        {
            Panel_FORMULARIO.Enabled = false;
            configurarBotonesDeAccion(true, false, false);
        }

        String NUM_DOC_IDENTIDAD = QueryStringSeguro["NUM_DOC_IDENTIDAD"].ToString();

        DataRow filaTablaInfoSolicitud = tablaInfoSolicitud.Rows[0];

        if (accion != "modificar")
        {
            Panel_CONTROL_REGISTRO.Visible = true;
            Panel_CONTROL_REGISTRO.Enabled = false;

            TextBox_USU_CRE.Text = filaTablaInfoSolicitud["USU_CRE"].ToString();
            try
            {
                TextBox_FCH_CRE.Text = DateTime.Parse(filaTablaInfoSolicitud["FCH_CRE"].ToString()).ToShortDateString();
                TextBox_HOR_CRE.Text = DateTime.Parse(filaTablaInfoSolicitud["FCH_CRE"].ToString()).ToShortTimeString();
            }
            catch
            {
                TextBox_FCH_CRE.Text = "";
                TextBox_HOR_CRE.Text = "";
            }
            TextBox_USU_MOD.Text = filaTablaInfoSolicitud["USU_MOD"].ToString();
            try
            {
                TextBox_FCH_MOD.Text = DateTime.Parse(filaTablaInfoSolicitud["FCH_MOD"].ToString()).ToShortDateString();
                TextBox_HOR_MOD.Text = DateTime.Parse(filaTablaInfoSolicitud["FCH_MOD"].ToString()).ToShortTimeString();
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
        
        Panel_ID_SOLICITUD.Visible = true;
        Panel_ID_SOLICITUD.Enabled = false;
        TextBox_ID_SOLICITUD.Text = filaTablaInfoSolicitud["ID_SOLICITUD"].ToString().Trim();

        if (accion == "modificar")
        {
            Panel_DATOS_SOLICITUD.Visible = true;
            Panel_DATOS_SOLICITUD.Enabled = true;
        }
        else
        {
            Panel_DATOS_SOLICITUD.Visible = true;
            Panel_DATOS_SOLICITUD.Enabled = false;
        }
        
        TextBox_FECHA_R.Text = Convert.ToDateTime(filaTablaInfoSolicitud["FECHA_R"].ToString().Trim()).ToShortDateString();
        TextBox_FECHA_R.Enabled = false;

        Panel_ESTADO_ASPIRANTE.Visible = true;
        Panel_ESTADO_ASPIRANTE.Enabled = false;

        TextBox_ESTADO_ASPIRANTE.Text = filaTablaInfoSolicitud["ARCHIVO"].ToString().Trim();

        TextBox_APELLIDOS.Text = filaTablaInfoSolicitud["APELLIDOS"].ToString().Trim();
        TextBox_NOMBRES.Text = filaTablaInfoSolicitud["NOMBRES"].ToString().Trim();
        cargar_DropDownList_SEXO();
        DropDownList_SEXO.SelectedValue = filaTablaInfoSolicitud["SEXO"].ToString();
        TextBox_FCH_NACIMIENTO.Text = Convert.ToDateTime(filaTablaInfoSolicitud["FCH_NACIMIENTO"]).ToShortDateString();

        cargar_DropDownList_TIP_DOC_IDENTIDAD();
        DropDownList_TIP_DOC_IDENTIDAD.SelectedValue = filaTablaInfoSolicitud["TIP_DOC_IDENTIDAD"].ToString().Trim();
        TextBox_NUM_DOC_IDENTIDAD.Text = NUM_DOC_IDENTIDAD;

        DataRow filaInfoCiudadYDepartamento = obtenerIdDepartamentoIdCiudad(filaTablaInfoSolicitud["CIU_CEDULA"].ToString().Trim());
        if (filaInfoCiudadYDepartamento != null)
        {
            cargar_DropDownList_DEPARTAMENTO_CEDULA();
            DropDownList_DEPARTAMENTO_CEDULA.SelectedValue = filaInfoCiudadYDepartamento["ID_DEPARTAMENTO"].ToString().Trim();

            cargar_DropDownList_CIU_CEDULA(filaInfoCiudadYDepartamento["ID_DEPARTAMENTO"].ToString().Trim());
            DropDownList_CIU_CEDULA.SelectedValue = filaInfoCiudadYDepartamento["ID_CIUDAD"].ToString().Trim();
        }
        else
        {
            if (accion == "modificar")
            {
                cargar_DropDownList_DEPARTAMENTO_CEDULA();
                DropDownList_DEPARTAMENTO_CEDULA.Enabled = true;
                inhabilitar_DropDownList_CIU_CEDULA();
            }
        }

        TextBox_LIB_MILITAR.Text = filaTablaInfoSolicitud["LIB_MILITAR"].ToString().Trim();

        TextBox_DIR_ASPIRANTE.Text = filaTablaInfoSolicitud["DIR_ASPIRANTE"].ToString().Trim();

        filaInfoCiudadYDepartamento = obtenerIdDepartamentoIdCiudad(filaTablaInfoSolicitud["CIU_ASPIRANTE"].ToString().Trim());
        if (filaInfoCiudadYDepartamento != null)
        {
            cargar_DropDownList_DEPARTAMENTO_ASPIRANTE();
            DropDownList_DEPARTAMENTO_ASPIRANTE.SelectedValue = filaInfoCiudadYDepartamento["ID_DEPARTAMENTO"].ToString().Trim();

            cargar_DropDownList_CIU_ASPIRANTE(filaInfoCiudadYDepartamento["ID_DEPARTAMENTO"].ToString().Trim());
            DropDownList_CIU_ASPIRANTE.SelectedValue = filaInfoCiudadYDepartamento["ID_CIUDAD"].ToString().Trim();
        }
        else
        {
            if (accion == "modificar")
            {
                cargar_DropDownList_DEPARTAMENTO_ASPIRANTE();
                DropDownList_DEPARTAMENTO_CEDULA.Enabled = true;
                inhabilitar_DropDownList_CIU_ASPIRANTE();
            }
        }
        TextBox_SECTOR.Text = filaTablaInfoSolicitud["SECTOR"].ToString();

        cargar_DropDownList_NIV_EDUCACION();
        DropDownList_NIV_EDUCACION.SelectedValue = filaTablaInfoSolicitud["NIV_EDUCACION"].ToString().Trim();
        cargar_DropDownList_ID_OCUPACION_TODOS();
        DropDownList_ID_OCUPACION.SelectedValue = filaTablaInfoSolicitud["ID_OCUPACION"].ToString().Trim();
        cargar_DropDownList_ID_FUENTE();
        DropDownList_ID_FUENTE.SelectedValue = filaTablaInfoSolicitud["ID_FUENTE"].ToString().Trim();
        cargar_DropDownList_AREAS_ESPECIALIZACION();
        DropDownList_AREAS_ESPECIALIZACION.SelectedValue = filaTablaInfoSolicitud["ID_AREAINTERES"].ToString();

        try
        {
            TextBox_ASPIRACION_SALARIAL.Text = Convert.ToInt32(filaTablaInfoSolicitud["ASPIRACION_SALARIAL"]).ToString();
        }
        catch
        {
            TextBox_ASPIRACION_SALARIAL.Text = "";
        }

        TextBox_TEL_ASPIRANTE.Text = filaTablaInfoSolicitud["TEL_ASPIRANTE"].ToString().Trim();
        TextBox_E_MAIL.Text = filaTablaInfoSolicitud["E_MAIL"].ToString().Trim();
        cargar_DropDownList_CAT_LIC_COND();
        DropDownList_CAT_LIC_COND.SelectedValue = filaTablaInfoSolicitud["CAT_LIC_COND"].ToString().Trim();
    }
    private void iniciar_seccion_de_busqueda()
    {
        TextBox_BUSCAR.Text = "";
        cargar_DropDownList_BUSCAR();
    }
    private void inhabilitar_DropDownList_CIU_CEDULA()
    {
        DropDownList_CIU_CEDULA.Enabled = false;
        DropDownList_CIU_CEDULA.Items.Clear();
        ListItem item = new ListItem("Seleccione Ciudad", "");
        DropDownList_CIU_CEDULA.Items.Add(item);
        DropDownList_CIU_CEDULA.DataBind();
    }
    private void inhabilitar_DropDownList_CIU_ASPIRANTE()
    {
        DropDownList_CIU_ASPIRANTE.Enabled = false;
        DropDownList_CIU_ASPIRANTE.Items.Clear();
        ListItem item = new ListItem("Seleccione Ciudad", "");
        DropDownList_CIU_ASPIRANTE.Items.Add(item);
        DropDownList_CIU_ASPIRANTE.DataBind();
    }
    #endregion

    #region metodos que se ejecutan al iniciar 
    private void iniciar_interfaz_inicial()
    {
        iniciar_seccion_de_busqueda();

        Panel_FORMULARIO.Visible = false;
        Panel_RESULTADOS_GRID.Visible = false;
        Panel_FORM_BOTONES_1.Visible = false;

        Panel_BITACORA_HOJA.Visible = false;
        Panel_Descarte_Entrevista.Visible = false;
    }
    private void iniciar_interfaz_para_validacion()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String NUM_DOC_IDENTIDAD = QueryStringSeguro["num_doc_identidad"].ToString();

        DataTable tablaInfoSolicitud;

        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        tablaInfoSolicitud = _radicacionHojasDeVida.ObtenerRegSolicitudesingresoPorNumDocIdentidadValAcoset(NUM_DOC_IDENTIDAD);
        
        if (String.IsNullOrEmpty(_radicacionHojasDeVida.MensajeError) == false)
        {
           
            Panel_FORM_BOTONES_1.Visible = false;

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _radicacionHojasDeVida.MensajeError, Proceso.Error);
            
            Panel_FORMULARIO.Visible = false;
            Panel_RESULTADOS_GRID.Visible = false;

            Panel_BITACORA_HOJA.Visible = false;
            Panel_Descarte_Entrevista.Visible = false;
        }
        else
        {
            if (tablaInfoSolicitud.Rows.Count <= 0)
            {
                
                Panel_FORM_BOTONES_1.Visible = true;
                configurarBotonesDeAccion(false, true, true);

                Panel_FORMULARIO.Visible = true;
                Panel_RESULTADOS_GRID.Visible = false;
                cargarInterfazEnBlanco();

                Panel_BITACORA_HOJA.Visible = false;
                Panel_Descarte_Entrevista.Visible = false;
            }
            else
            {
                
                Panel_FORMULARIO.Visible = true;
                Panel_RESULTADOS_GRID.Visible = false;

                cargarInterfazConDatos(tablaInfoSolicitud);
                configurarBotonesDeAccion(true, true, true);

                Panel_BITACORA_HOJA.Visible = true;
                cargar_GridView_BITACORA();
                
                Panel_Descarte_Entrevista.Visible = false;
            }
        }
    }

    #endregion

    #region metodos que consultan a la bd
    private DataRow obtenerIdDepartamentoIdCiudad(String idCiudad)
    {
        DataRow resultado = null;

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());

        DataTable tablaIdDepartamentoIdCiudad = _ciudad.ObtenerIdDepartamentoConIdCiudad(idCiudad);

        if (tablaIdDepartamentoIdCiudad.Rows.Count > 0)
        {
            resultado = tablaIdDepartamentoIdCiudad.Rows[0];
        }

        return resultado;
    }
    #endregion

    private void Configurar()
    {
        configurar_paneles_popup(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    private void configurar_paneles_popup(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_mensaje.CssClass = "panel_popup";
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = "DESCARTE DE PERSONAL";

        Panel_INFO_ADICIONAL_MODULO.Visible = false;

        if (IsPostBack == false)
        {
            Configurar();

            tools _tools = new tools();
            SecureQueryString QueryStringSeguro;
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

            String accion = QueryStringSeguro["accion"].ToString();

            if (accion == "inicial")
            {
                iniciar_interfaz_inicial();
            }
            else
            {
                if (accion == "cargar")
                {
                    iniciar_interfaz_para_validacion();
                }
            }
         }
    }

    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        configurarInfoAdicionalModulo(false, "");

        String datosCapturados = TextBox_BUSCAR.Text.ToUpper();
        String campo = DropDownList_BUSCAR.SelectedValue.ToString();

        radicacionHojasDeVida _SolIngreso = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaResultadosBusqueda = new DataTable();


        if (DropDownList_BUSCAR.SelectedValue == "NOMBRES")
        {
            tablaResultadosBusqueda = _SolIngreso.ObtenerRegSolicitudesingresoPorNombres(datosCapturados);
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "APELLIDOS")
            {
                tablaResultadosBusqueda = _SolIngreso.ObtenerRegSolicitudesingresoPorApellidos(datosCapturados);
            }
            else
            {
                if (DropDownList_BUSCAR.SelectedValue == "NUM_DOC_IDENTIDAD")
                {
                    tablaResultadosBusqueda = _SolIngreso.ObtenerRegSolicitudesingresoPorNumDocIdentidad(datosCapturados);
                }
            }
        }

        if (tablaResultadosBusqueda.Rows.Count <= 0)
        {
            
            if (_SolIngreso.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _SolIngreso.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros que cumplieran los datos de busqueda.", Proceso.Advertencia);
            }

            Panel_RESULTADOS_GRID.Visible = false;
            
            Panel_FORMULARIO.Visible = false;
            Panel_BITACORA_HOJA.Visible = false;
            Panel_Descarte_Entrevista.Visible = false;
        }
        else
        {
            configurarBotonesDeAccion(true, false, false);
            Panel_botones_accion.Visible = true;

            Panel_RESULTADOS_GRID.Visible = true;
            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaResultadosBusqueda;
            GridView_RESULTADOS_BUSQUEDA.DataBind();

            DataRow filaSolicitud;
            for (int i = 0; i < GridView_RESULTADOS_BUSQUEDA.Rows.Count; i++)
            {
                filaSolicitud = tablaResultadosBusqueda.Rows[(GridView_RESULTADOS_BUSQUEDA.PageIndex * GridView_RESULTADOS_BUSQUEDA.PageSize) + i];

                if ((filaSolicitud["ARCHIVO"].ToString().ToUpper().Trim() == "DISPONIBLE") || (filaSolicitud["ARCHIVO"].ToString().ToUpper().Trim() == "ASPIRANTE") || (filaSolicitud["ARCHIVO"].ToString().ToUpper().Trim() == "CONTRATADO") || (filaSolicitud["ARCHIVO"].ToString().ToUpper().Trim() == "DESCARTADO SELECCION"))
                {
                    GridView_RESULTADOS_BUSQUEDA.Rows[i].Cells[0].Enabled = false;
                    GridView_RESULTADOS_BUSQUEDA.Rows[i].Cells[0].Text = "";
                }
            }

            Panel_FORMULARIO.Visible = false;
            Panel_BITACORA_HOJA.Visible = false;
            Panel_Descarte_Entrevista.Visible = false;
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
    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = "Contratacion";
        QueryStringSeguro["nombre_area"] = "CONTRATACIÓN";
        QueryStringSeguro["nombre_modulo"] = "HOJA DE TRABAJO";
        QueryStringSeguro["accion"] = "inicial";

        Response.Redirect("~/contratcion/hojaTrabajoContratacion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }
    protected void DropDownList_SEXO_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void Button_BUSCADOR_CARGO_Click(object sender, EventArgs e)
    {
        cargar_DropDownList_ID_OCUPACION_FILTRADO();
    }
    protected void Button_Entrevista_Click(object sender, EventArgs e)
    {
        Panel_RESULTADOS_GRID.Visible = false;
        Panel_Descarte_Entrevista.Visible = true;
        Panel_BITACORA_HOJA.Visible = false;

        Panel_Descarte_Entrevista.Enabled = true;
        Cliente.Checked = false;
        Cuenta.Checked = false;
        Examenes.Checked = false;
        firma.Checked = false;
        Otros.Checked = false;

        RadioButtonList_TipoDescarte.ClearSelection();
        TextBox_comentarios_Entrevista.Text = "";

        Button5.Visible = true;
        Button3.Visible = true;
    }

    protected void Button_ADICIONAR_ENTREVISTA_Click(object sender, EventArgs e)
    {
        radicacionHojasDeVida _SolIngreso = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        regRegsitrosHojaVida _Reg_HV = new regRegsitrosHojaVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());


        Decimal NUM_DOC_IDENTIDAD = Convert.ToDecimal(TextBox_NUM_DOC_IDENTIDAD.Text);
        int ID_REQUERIMIENTO = 0;
        int ID_SOLICITUD = 0;

        String ARCHIVO = "DISPONIBLE";

        DataTable tablaSolIngreso = _SolIngreso.ObtenerRegSolicitudesingresoPorNumDocIdentidad(NUM_DOC_IDENTIDAD.ToString());
        DataRow filaSolIngreso = tablaSolIngreso.Rows[0];

        if (filaSolIngreso["ARCHIVO"].Equals("CONTRATADO"))
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La persona seleccionada no puede ser descartada, porque ya se encuentra en estado CONTRATADA.", Proceso.Advertencia);
        }
        else
        {
            if (RadioButtonList_TipoDescarte.SelectedValue == "-")
            {
                ARCHIVO = "DESCARTADO SELECCION";
            }

            ID_SOLICITUD = Convert.ToInt32(filaSolIngreso["ID_SOLICITUD"].ToString());

            if (!(String.IsNullOrEmpty(filaSolIngreso["ID_REQUERIMIENTO"].ToString())))
            {
                ID_REQUERIMIENTO = Convert.ToInt32(filaSolIngreso["ID_REQUERIMIENTO"].ToString());
            }

            if (Cliente.Checked)
            {
                _Reg_HV.AdicionarRegRegistrosHojaVida(ID_SOLICITUD, "DESC. CONTRATACION", TextBox_comentarios_Entrevista.Text.ToString(), "CLIENTE", ID_REQUERIMIENTO);
                _SolIngreso.ActualizarEstadoRegSolicitudesIngreso(ID_REQUERIMIENTO, ID_SOLICITUD, ARCHIVO);
                

                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La persona seleccionada fue descartada exitosamente.", Proceso.Correcto);

            }
            else if (Cuenta.Checked)
            {
                _Reg_HV.AdicionarRegRegistrosHojaVida(ID_SOLICITUD, "DESC. CONTRATACION", TextBox_comentarios_Entrevista.Text.ToString(), "CUENTA", ID_REQUERIMIENTO);
                
                _SolIngreso.ActualizarEstadoRegSolicitudesIngreso(ID_REQUERIMIENTO, ID_SOLICITUD, ARCHIVO);
                

                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La persona seleccionada fue descartada exitosamente.", Proceso.Correcto);

            }
            else if (Examenes.Checked)
            {
                _Reg_HV.AdicionarRegRegistrosHojaVida(ID_SOLICITUD, "DESC. CONTRATACION", TextBox_comentarios_Entrevista.Text.ToString(), "EXAMENES", ID_REQUERIMIENTO);
                
                _SolIngreso.ActualizarEstadoRegSolicitudesIngreso(ID_REQUERIMIENTO, ID_SOLICITUD, ARCHIVO);
                

                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_FONDO_MENSAJE, Label_MENSAJE, "La persona seleccionada fue descartada exitosamente.", Proceso.Correcto);

            }
            else if (firma.Checked)
            {
                _Reg_HV.AdicionarRegRegistrosHojaVida(ID_SOLICITUD, "DESC. CONTRATACION", TextBox_comentarios_Entrevista.Text.ToString(), "FIRMA", ID_REQUERIMIENTO);
                
                _SolIngreso.ActualizarEstadoRegSolicitudesIngreso(ID_REQUERIMIENTO, ID_SOLICITUD, ARCHIVO);
                

                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La persona seleccionada fue descartada exitosamente.", Proceso.Correcto);
                
            }
            else if (Otros.Checked)
            {
                _Reg_HV.AdicionarRegRegistrosHojaVida(ID_SOLICITUD, "DESC. CONTRATACION", TextBox_comentarios_Entrevista.Text.ToString(), "OTROS", ID_REQUERIMIENTO);
                
                _SolIngreso.ActualizarEstadoRegSolicitudesIngreso(ID_REQUERIMIENTO, ID_SOLICITUD, ARCHIVO);
                

                Informar(Panel_MENSAJES, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La persona seleccionada fue descartada exitosamente.", Proceso.Correcto);
                
            }

            Panel_Descarte_Entrevista.Enabled = false;

            Button3.Visible = false;
            Button5.Visible = false;
            Button_ENTREVISTA.Visible = false;

            TextBox_ESTADO_ASPIRANTE.Text = ARCHIVO;
        }
    }

    protected void GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged(object sender, EventArgs e)
    {
        String ID_SOLICITUD = GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["ID_SOLICITUD"].ToString();
        String ID_REQUERIMIENTO = GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["ID_REQUERIMIENTO"].ToString();
        String NUM_DOC_IDENTIDAD = GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["NUM_DOC_IDENTIDAD"].ToString();
        radicacionHojasDeVida _SolIngreso = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaSolIngreso = _SolIngreso.ObtenerRegSolicitudesingresoPorNumDocIdentidad(NUM_DOC_IDENTIDAD.ToString());
        DataRow filaSolIngreso = tablaSolIngreso.Rows[0];

        if (filaSolIngreso["ARCHIVO"].ToString().Trim().Equals("CONTRATADO"))
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La persona seleccionada no puede ser descartada, porque se encuentra CONTRATADA.", Proceso.Advertencia);
        }
        else
        {
            tools _tools = new tools();
            SecureQueryString QueryStringSeguros;
            QueryStringSeguros = new SecureQueryString(_tools.byteParaQueryStringSeguro());

            QueryStringSeguros["img_area"] = "contratacion";
            QueryStringSeguros["nombre_area"] = "CONTRATACIÓN";
            QueryStringSeguros["nombre_modulo"] = "DESCARTE DE PERSONAL";
            QueryStringSeguros["accion"] = "cargar";
            QueryStringSeguros["reg"] = ID_SOLICITUD;
            QueryStringSeguros["id_Requerimiento"] = ID_REQUERIMIENTO;
            QueryStringSeguros["num_doc_identidad"] = NUM_DOC_IDENTIDAD;

            Response.Redirect("~/contratacion/descartePersonal.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguros.ToString()));
        }
    }

    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;

        String datosCapturados = TextBox_BUSCAR.Text.ToUpper();

        radicacionHojasDeVida _SolIngreso = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaResultadosBusqueda = new DataTable();


        if (DropDownList_BUSCAR.SelectedValue == "NOMBRES")
        {
            tablaResultadosBusqueda = _SolIngreso.ObtenerRegSolicitudesingresoPorNombres(datosCapturados);
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "APELLIDOS")
            {
                tablaResultadosBusqueda = _SolIngreso.ObtenerRegSolicitudesingresoPorApellidos(datosCapturados);
            }
            else
            {
                if (DropDownList_BUSCAR.SelectedValue == "NUM_DOC_IDENTIDAD")
                {
                    tablaResultadosBusqueda = _SolIngreso.ObtenerRegSolicitudesingresoPorNumDocIdentidad(datosCapturados);
                }
            }
        }

        if (tablaResultadosBusqueda.Rows.Count <= 0)
        {
            if (_SolIngreso.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _SolIngreso.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros que cumplierna los datos de busqueda.", Proceso.Advertencia);
            }
        }
        else
        {
            Panel_RESULTADOS_GRID.Enabled = true;
            Panel_RESULTADOS_GRID.Visible = true;
            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaResultadosBusqueda;
            GridView_RESULTADOS_BUSQUEDA.DataBind();


            DataRow filaSolicitud;
            for (int i = 0; i < GridView_RESULTADOS_BUSQUEDA.Rows.Count; i++)
            {
                filaSolicitud = tablaResultadosBusqueda.Rows[(GridView_RESULTADOS_BUSQUEDA.PageIndex * GridView_RESULTADOS_BUSQUEDA.PageSize) + i];

                if ((filaSolicitud["ARCHIVO"].ToString().ToUpper().Trim() == "DISPONIBLE") || (filaSolicitud["ARCHIVO"].ToString().ToUpper().Trim() == "ASPIRANTE") || (filaSolicitud["ARCHIVO"].ToString().ToUpper().Trim() == "CONTRATADO") || (filaSolicitud["ARCHIVO"].ToString().ToUpper().Trim() == "DESCARTADO SELECCION"))
                {
                    GridView_RESULTADOS_BUSQUEDA.Rows[i].Cells[0].Enabled = false;
                    GridView_RESULTADOS_BUSQUEDA.Rows[i].Cells[0].Text = "";
                }
            }
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
}