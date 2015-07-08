using System;
using System.Web.UI.WebControls;
using Brainsbits.LLB.maestras;
using System.Data;
using Brainsbits.LLB.seguridad;
using TSHAK.Components;
using System.Web;
using Brainsbits.LLB;


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

    private enum Acciones
    {
        Inicio = 0, 
        Nuevo, 
        CargarFuente,
        CargarCargos,
        NuevoCargo,
        CargarComunicaciones,
        NuevaComunicacion,
        ModificarFuente
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }

    private void configurar_paneles_popup(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_mensaje.CssClass = "panel_popup";
    }

    private void Configurar()
    {
        configurar_paneles_popup(Panel_FONDO_MENSAJE, Panel_MENSAJES);
        configurar_paneles_popup(Panel_FONDO_MENSAJE_ABAJO, Panel_MENSAJES_ABAJO);
    }

    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = false;
                Panel_FORM_BOTONES.Visible = false;
                Button_NUEVO.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_CANCELAR.Visible = false;

                Panel_RESULTADOS_GRID.Visible = false;

                Panel_FORMULARIO.Visible = false;
                Panel_CONTROL_REGISTRO.Visible = false;
                Panel_DATOS_FUENTE.Visible = false;
                Panel_BotonOcultar.Visible = false;

                Panel_CARGOS_FUENTE.Visible = false;
                Panel_GRILLA_OCUPACIONES_FUENTE.Visible = false;
                Panel_NUEVO_CARGO.Visible = false;

                Panel_COMUNICACIONES.Visible = false;
                Panel_GRILLA_COMUNICACIONES.Visible = false;
                Panel_NUEVA_COMUNICACION.Visible = false;
                break;
            case Acciones.NuevoCargo:
                Panel_NUEVO_CARGO.Visible = false;
                break;
            case Acciones.NuevaComunicacion:
                Panel_NUEVA_COMUNICACION.Visible = false;
                break;
            case Acciones.ModificarFuente:
                Button_NUEVO.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_CANCELAR.Visible = false;

                Panel_CONTROL_REGISTRO.Visible = false;

                Panel_CARGOS_FUENTE.Visible = false;
                Panel_COMUNICACIONES.Visible = false;

                Panel_BotonOcultar.Visible = false;
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

                TextBox_NOM_FUENTE.Enabled = false;
                TextBox_DIR_FUENTE.Enabled = false;
                TextBox_ENCARGADO.Enabled = false;
                TextBox_CARGO_ENC.Enabled = false;
                TextBox_TEL_FUENTE.Enabled = false;
                TextBox_EMAIL_CONTACTO.Enabled = false;
                DropDownList_REGIONAL.Enabled = false;
                DropDownList_DEPARTAMENTO.Enabled = false;
                DropDownList_CIUDAD.Enabled = false;
                TextBox_Observaciones.Enabled = false;
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

                Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.Nuevo:
                Panel_FORM_BOTONES.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_FORMULARIO.Visible = true;
                Panel_DATOS_FUENTE.Visible = true;
                break;
            case Acciones.CargarFuente:
                Panel_FORM_BOTONES.Visible = true;
                Button_MODIFICAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_FORMULARIO.Visible = true;
                Panel_CONTROL_REGISTRO.Visible = true;
                Panel_DATOS_FUENTE.Visible = true;

                Panel_BotonOcultar.Visible = true;
                break;
            case Acciones.CargarCargos:
                Panel_CARGOS_FUENTE.Visible = true;
                Button_NUEVO_CARGO.Visible = true;
                break;
            case Acciones.NuevoCargo:
                Panel_NUEVO_CARGO.Visible = true;
                break;
            case Acciones.CargarComunicaciones:
                Panel_COMUNICACIONES.Visible = true;
                Button_NUEVA_COMUNICACION.Visible = true;
                break;
            case Acciones.NuevaComunicacion:
                Panel_NUEVA_COMUNICACION.Visible = true;
                break;
            case Acciones.ModificarFuente:
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;
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

        ListItem item = new ListItem("Selecicones...", "");
        DropDownList_BUSCAR.Items.Add(item);

        item = new ListItem("Nombres", "NOM_FUENTE");
        DropDownList_BUSCAR.Items.Add(item);

        item = new ListItem("Contácto", "ENCARGADO");
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

    private void cargar_GridView_RESULTADOS_BUSQUEDA()
    {
        fuentesReclutamiento _fuentesReclutamiento = new fuentesReclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaFuentes = _fuentesReclutamiento.ObtenerRecFuentesTodos();

        if (tablaFuentes.Rows.Count <= 0)
        {
            if (_fuentesReclutamiento.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _fuentesReclutamiento.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron resultados para mostrar.", Proceso.Advertencia);
            }

            Panel_RESULTADOS_GRID.Visible = false;
        }
        else
        {
            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaFuentes;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
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

    private void cargar_DropDownList_DEPARTAMENTO(String id)
    {
        DropDownList_DEPARTAMENTO.Items.Clear();

        departamento _departamento = new departamento(Session["idEmpresa"].ToString());
        DataTable tablaDepartamentos = _departamento.ObtenerDepartamentosPorIdRegional(id);

        ListItem item = new ListItem("Seleccione Departamento", "");
        DropDownList_DEPARTAMENTO.Items.Add(item);

        foreach (DataRow fila in tablaDepartamentos.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_DEPARTAMENTO"].ToString());
            DropDownList_DEPARTAMENTO.Items.Add(item);
        }

        DropDownList_DEPARTAMENTO.DataBind();
    }

    private void cargar_DropDownList_CIUDAD(String idRegional, String idDepartamento)
    {
        DropDownList_CIUDAD.Items.Clear();

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
        DataTable tablaCiudades = _ciudad.ObtenerCiudadesPorIdDepartamentoEIdRegional(idRegional, idDepartamento);

        ListItem item = new ListItem("Seleccione Ciudad", "");
        DropDownList_CIUDAD.Items.Add(item);

        foreach (DataRow fila in tablaCiudades.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_CIUDAD"].ToString());
            DropDownList_CIUDAD.Items.Add(item);
        }

        DropDownList_CIUDAD.DataBind();
    }

    private void inhabilitar_DropDownList_DEPARTAMENTO()
    {
        DropDownList_DEPARTAMENTO.Enabled = false;
        DropDownList_DEPARTAMENTO.Items.Clear();
        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_DEPARTAMENTO.Items.Add(item);
        DropDownList_DEPARTAMENTO.DataBind();
    }

    private void inhabilitar_DropDownList_CIUDAD()
    {
        DropDownList_CIUDAD.Enabled = false;
        DropDownList_CIUDAD.Items.Clear();
        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_CIUDAD.Items.Add(item);
        DropDownList_CIUDAD.DataBind();
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                configurarCaracteresAceptadosBusqueda(true, true);

                HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "SIN_FILTRO";
                HiddenField_FILTRO_DROP.Value = String.Empty;
                HiddenField_FILTRO_DATO.Value = String.Empty;

                iniciar_seccion_de_busqueda();

                cargar_GridView_RESULTADOS_BUSQUEDA();
                break;
            case Acciones.Nuevo:
                cargar_DropDownList_REGIONAL();
                inhabilitar_DropDownList_DEPARTAMENTO();
                inhabilitar_DropDownList_CIUDAD();
                break;
            case Acciones.NuevoCargo:
                DropDownList_ID_OCUPACION.Items.Add(new ListItem("Seleccione...", ""));
                break;
            case Acciones.NuevaComunicacion:
                TextBox_fecha.Text = DateTime.Now.ToShortDateString();
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

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = "FUENTES DE RECLUTAMIENTO";

        if (IsPostBack == false)
        {
            Iniciar();
        }
    }

    private void Limpiar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Nuevo:
                HiddenField_ID_FUENTE.Value = "";
                TextBox_NOM_FUENTE.Text = "";
                TextBox_DIR_FUENTE.Text = "";
                TextBox_ENCARGADO.Text = "";
                TextBox_CARGO_ENC.Text = "";
                TextBox_TEL_FUENTE.Text = "";
                TextBox_EMAIL_CONTACTO.Text = "";

                DropDownList_REGIONAL.Items.Clear();
                DropDownList_DEPARTAMENTO.Items.Clear();
                DropDownList_CIUDAD.Items.Clear();
                TextBox_Observaciones.Text = "";

                break;
            case Acciones.NuevoCargo:
                TextBox_BUSCADOR_CARGO.Text = "";
                DropDownList_ID_OCUPACION.Items.Clear();
                TextBox_Cargo_Comentario.Text = "";
                break;
            case Acciones.NuevaComunicacion:
                TextBox_fecha.Text = "";
                TextBox_Comentarios_comunicacion.Text = "";
                TextBox_Cargo_Comentario.Text = "";
                break;
        }
    }

    private void Activar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Nuevo:
                TextBox_NOM_FUENTE.Enabled = true;
                TextBox_DIR_FUENTE.Enabled = true;
                TextBox_ENCARGADO.Enabled = true;
                TextBox_CARGO_ENC.Enabled = true;
                TextBox_TEL_FUENTE.Enabled = true;
                TextBox_EMAIL_CONTACTO.Enabled = true;
                DropDownList_REGIONAL.Enabled = true;
                TextBox_Observaciones.Enabled = true;
                break;
            case Acciones.ModificarFuente:
                TextBox_NOM_FUENTE.Enabled = true;
                TextBox_DIR_FUENTE.Enabled = true;
                TextBox_ENCARGADO.Enabled = true;
                TextBox_CARGO_ENC.Enabled = true;
                TextBox_TEL_FUENTE.Enabled = true;
                TextBox_EMAIL_CONTACTO.Enabled = true;

                if (DropDownList_CIUDAD.SelectedIndex <= 0)
                {
                    DropDownList_REGIONAL.Enabled = true;
                }
                else
                {
                    DropDownList_REGIONAL.Enabled = true;
                    DropDownList_DEPARTAMENTO.Enabled = true;
                    DropDownList_CIUDAD.Enabled = true;
                }

                TextBox_Observaciones.Enabled = true;
                break;
        }
    }

    protected void Button_NUEVO_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Nuevo);
        Limpiar(Acciones.Nuevo);
        Activar(Acciones.Nuevo);
        Cargar(Acciones.Nuevo);
    }
    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.ModificarFuente);
        Mostrar(Acciones.ModificarFuente);
        Activar(Acciones.ModificarFuente);
    }

    private void cargar_informacion_control_registro(DataRow filaFuente)
    {
        TextBox_USU_CRE.Text = filaFuente["USU_CRE"].ToString();
        try
        {
            TextBox_FCH_CRE.Text = DateTime.Parse(filaFuente["FCH_CRE"].ToString()).ToShortDateString();
            TextBox_HOR_CRE.Text = DateTime.Parse(filaFuente["FCH_CRE"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_CRE.Text = "";
            TextBox_HOR_CRE.Text = "";
        }
        TextBox_USU_MOD.Text = filaFuente["USU_MOD"].ToString();
        try
        {
            TextBox_FCH_MOD.Text = DateTime.Parse(filaFuente["FCH_MOD"].ToString()).ToShortDateString();
            TextBox_HOR_MOD.Text = DateTime.Parse(filaFuente["FCH_MOD"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_MOD.Text = "";
            TextBox_HOR_MOD.Text = "";
        }
    }

    private DataRow obtenerDatosCiudad(String idCiudad)
    {
        DataRow resultado = null;

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());

        DataTable tablaCiudad = _ciudad.ObtenerCiudadPorIdCiudad(idCiudad);

        if (tablaCiudad.Rows.Count > 0)
        {
            resultado = tablaCiudad.Rows[0];
        }

        return resultado;
    }

    private void cargar_cargos_en_fuente(Decimal ID_FUENTE)
    {
        Mostrar(Acciones.CargarCargos);

        fuentesReclutamiento _fuentesReclutamiento = new fuentesReclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCargos = _fuentesReclutamiento.ObtenerRecOcupacionesFuentesPorFuente(ID_FUENTE);

        if(tablaCargos.Rows.Count <= 0)
        {
            GridView_CARGOS_INCLUIDOS.DataSource = null;
            GridView_CARGOS_INCLUIDOS.DataBind();

            if (_fuentesReclutamiento.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _fuentesReclutamiento.MensajeError, Proceso.Error);
            }
        }
        else
        {
            GridView_CARGOS_INCLUIDOS.DataSource = tablaCargos;
            GridView_CARGOS_INCLUIDOS.DataBind();
            Panel_GRILLA_OCUPACIONES_FUENTE.Visible = true;
        }
    }

    private void cargar_comunicaciones(Decimal ID_FUENTE)
    {
        Mostrar(Acciones.CargarComunicaciones);

        fuentesReclutamiento _fuentesReclutamiento = new fuentesReclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaComunicaciones = _fuentesReclutamiento.ObtenerRecComFuentesPorIdFuente(ID_FUENTE);

        if (tablaComunicaciones.Rows.Count <= 0)
        {
            GridView_COMUNICACION.DataSource = null;
            GridView_COMUNICACION.DataBind();

            if (_fuentesReclutamiento.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _fuentesReclutamiento.MensajeError, Proceso.Error);
            }
        }
        else
        {
            GridView_COMUNICACION.DataSource = tablaComunicaciones;
            GridView_COMUNICACION.DataBind();
            Panel_GRILLA_COMUNICACIONES.Visible = true;
        }
    }

    private void cargar_informacion_fuente(DataRow filaFuente)
    {
        Decimal ID_FUENTE = Convert.ToDecimal(filaFuente["ID_FUENTE"]);

        TextBox_NOM_FUENTE.Text = filaFuente["NOM_FUENTE"].ToString().Trim();
        TextBox_DIR_FUENTE.Text = filaFuente["DIR_FUENTE"].ToString().Trim();
        TextBox_ENCARGADO.Text = filaFuente["ENCARGADO"].ToString().Trim();
        TextBox_CARGO_ENC.Text = filaFuente["CARGO_ENC"].ToString().Trim();
        TextBox_TEL_FUENTE.Text = filaFuente["TEL_FUENTE"].ToString().Trim();
        TextBox_Observaciones.Text = filaFuente["OBS_FUENTE"].ToString().Trim();
        TextBox_EMAIL_CONTACTO.Text = filaFuente["EMAIL_ENCARGADO"].ToString().Trim();

        DataRow filaInfoCiudadEmpresa = obtenerDatosCiudad(filaFuente["CIU_FUENTE"].ToString().Trim());
        if (filaInfoCiudadEmpresa != null)
        {
            cargar_DropDownList_REGIONAL();
            DropDownList_REGIONAL.SelectedValue = filaInfoCiudadEmpresa["ID_REGIONAL"].ToString().Trim();
            DropDownList_REGIONAL.DataBind();

            cargar_DropDownList_DEPARTAMENTO(filaInfoCiudadEmpresa["ID_REGIONAL"].ToString().Trim());
            DropDownList_DEPARTAMENTO.SelectedValue = filaInfoCiudadEmpresa["ID_DEPARTAMENTO"].ToString().Trim();
            DropDownList_DEPARTAMENTO.DataBind();

            cargar_DropDownList_CIUDAD(filaInfoCiudadEmpresa["ID_REGIONAL"].ToString().Trim(), filaInfoCiudadEmpresa["ID_DEPARTAMENTO"].ToString().Trim());
            DropDownList_CIUDAD.SelectedValue = filaInfoCiudadEmpresa["ID_CIUDAD"].ToString().Trim();
            DropDownList_CIUDAD.DataBind();
        }
        else
        {
            cargar_DropDownList_REGIONAL();
            inhabilitar_DropDownList_DEPARTAMENTO();
            inhabilitar_DropDownList_CIUDAD();
        }

        cargar_cargos_en_fuente(ID_FUENTE);

        cargar_comunicaciones(ID_FUENTE);
    }

    private void Cargar(Decimal REGISTRO)
    {
        HiddenField_ID_FUENTE.Value = REGISTRO.ToString();

        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);

        fuentesReclutamiento _fuentesReclutamiento = new fuentesReclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaFuente = _fuentesReclutamiento.ObtenerRecFuentesPorIdFuente(Convert.ToInt32(REGISTRO));

        if (tablaFuente.Rows.Count <= 0)
        {
            Mostrar(Acciones.Inicio);
            Limpiar(Acciones.Nuevo);

            if (_fuentesReclutamiento.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _fuentesReclutamiento.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información de la fuente seleccionad.", Proceso.Advertencia);
            }
        }
        else
        {
            Mostrar(Acciones.CargarFuente);

            DataRow filaFuente = tablaFuente.Rows[0];

            cargar_informacion_control_registro(filaFuente);

            cargar_informacion_fuente(filaFuente);
        }

    }

    private void Guardar()
    {
        fuentesReclutamiento _fuentesReclutamiento = new fuentesReclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        
        String NOM_FUENTE = TextBox_NOM_FUENTE.Text.ToUpper().Trim();
        String DIR_FUENTE = TextBox_DIR_FUENTE.Text.ToUpper().Trim();
        String ENCARGADO = TextBox_ENCARGADO.Text.ToUpper().Trim();
        String CARGO_ENC = TextBox_CARGO_ENC.Text.ToUpper().Trim();
        String TEL_FUENTE = TextBox_TEL_FUENTE.Text.ToUpper().Trim();
        String EMAIL_ENCARGADO = TextBox_EMAIL_CONTACTO.Text.Trim();
        String CIU_FUENTE = DropDownList_CIUDAD.SelectedValue;
        String OBSERVACIONES = TextBox_Observaciones.Text.ToUpper().Trim();

        Decimal REGISTRO = _fuentesReclutamiento.AdicionarRecFuentes(NOM_FUENTE, DIR_FUENTE, DropDownList_CIUDAD.SelectedValue, TEL_FUENTE, ENCARGADO, CARGO_ENC, OBSERVACIONES, EMAIL_ENCARGADO);

        if (REGISTRO == 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _fuentesReclutamiento.MensajeError, Proceso.Error);
        }
        else
        {
            Cargar(REGISTRO);
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La Fuente de Reclutamiento " + NOM_FUENTE + " fue creada correctamente. Se le asignó el ID: " + REGISTRO.ToString() + ".", Proceso.Correcto);
        }
    }

    private void Modificar()
    {
        Decimal ID_FUENTE = Convert.ToDecimal(HiddenField_ID_FUENTE.Value);

        String NOM_FUENTE = TextBox_NOM_FUENTE.Text.ToUpper().Trim();
        String DIR_FUENTE = TextBox_DIR_FUENTE.Text.ToUpper().Trim();
        String ENCARGADO = TextBox_ENCARGADO.Text.ToUpper().Trim();
        String CARGO_ENC = TextBox_CARGO_ENC.Text.ToUpper().Trim();
        String TEL_FUENTE = TextBox_TEL_FUENTE.Text.ToUpper().Trim();
        String EMAIL_ENCARGADO = TextBox_EMAIL_CONTACTO.Text.Trim();
        String CIU_FUENTE = DropDownList_CIUDAD.SelectedValue;
        String OBSERVACIONES = TextBox_Observaciones.Text.ToUpper().Trim();

        fuentesReclutamiento _fuentesReclutamiento = new fuentesReclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean verificador = _fuentesReclutamiento.ActualizarRecFuentes(ID_FUENTE, NOM_FUENTE, DIR_FUENTE, CIU_FUENTE, TEL_FUENTE, ENCARGADO, CARGO_ENC, OBSERVACIONES, EMAIL_ENCARGADO);

        if (verificador == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _fuentesReclutamiento.MensajeError, Proceso.Error);
        }
        else
        {
            Cargar(ID_FUENTE);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La Fuente de Reclutamiento fue modificada correctamente.", Proceso.Correcto);
        }

    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        if (HiddenField_ID_FUENTE.Value == "")
        {
            Guardar();
        }
        else
        {
            Modificar();
        }
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
            configurarCaracteresAceptadosBusqueda(true, true);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "NOM_FUENTE")
        {
            configurarCaracteresAceptadosBusqueda(true, false);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "ENCARGADO")
        {
            configurarCaracteresAceptadosBusqueda(true, false);
        }

        TextBox_BUSCAR.Text = "";
    }

    private void Buscar()
    {
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);

        String datosCapturados = HiddenField_FILTRO_DATO.Value;
        String campo = HiddenField_FILTRO_DROP.Value;

        fuentesReclutamiento _fuentesReclutamiento = new fuentesReclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaResultadosBusqueda = new DataTable();

        if (DropDownList_BUSCAR.SelectedValue == "NOM_FUENTE")
        {
            tablaResultadosBusqueda = _fuentesReclutamiento.ObtenerRecFuentesPorNomFuente(datosCapturados);
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "ENCARGADO")
            {
                tablaResultadosBusqueda = _fuentesReclutamiento.ObtenerRecFuentesPorEncargado(datosCapturados);
            }
        }

        if (tablaResultadosBusqueda.Rows.Count <= 0)
        {
            if (_fuentesReclutamiento.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _fuentesReclutamiento.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros que cumplieran los datos de busqueda.", Proceso.Advertencia);
            }

            Panel_RESULTADOS_GRID.Visible = false;
        }
        else
        {
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
    protected void GridView_RESULTADOS_BUSQUEDA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal ID_FUENTE = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_FUENTE"]);

            Cargar(ID_FUENTE);
        }

    }
    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;

        if (HiddenField_TIPO_BUSQUEDA_ACTUAL.Value == "SIN_FILTRO")
        {
            cargar_GridView_RESULTADOS_BUSQUEDA();
        }
        else
        {
            Buscar();
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
            cargar_DropDownList_DEPARTAMENTO(DropDownList_REGIONAL.SelectedValue.ToString());
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
            cargar_DropDownList_CIUDAD(DropDownList_REGIONAL.SelectedValue.ToString(), DropDownList_DEPARTAMENTO.SelectedValue.ToString());
            DropDownList_CIUDAD.Enabled = true;
        }
    }
    protected void Button_CERRAR_MENSAJE_ABAJO_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE_ABAJO, Panel_MENSAJES_ABAJO);
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
    protected void Button_NUEVO_CARGO_Click(object sender, EventArgs e)
    {
        Button_NUEVO_CARGO.Visible = false;

        Mostrar(Acciones.NuevoCargo);
        Limpiar(Acciones.NuevoCargo);
        Cargar(Acciones.NuevoCargo);

    }
    protected void Button_ADICIONAR_CARGO_Click(object sender, EventArgs e)
    {
        Decimal ID_FUENTE = Convert.ToDecimal(HiddenField_ID_FUENTE.Value);

        Decimal ID_OCUPACION = Convert.ToDecimal(DropDownList_ID_OCUPACION.SelectedValue);
        String OBSERVACIONES = TextBox_Cargo_Comentario.Text.Trim();
        String LLAVE = ID_FUENTE.ToString() + "-" + ID_OCUPACION.ToString();

        fuentesReclutamiento _fuentesReclutamiento = new fuentesReclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Decimal REGISTRO = _fuentesReclutamiento.AdicionarRecOcupacionesFuente(ID_FUENTE, ID_OCUPACION, OBSERVACIONES, LLAVE);

        if (REGISTRO <= 0)
        {
            Informar(Panel_FONDO_MENSAJE_ABAJO, Image_MENSAJE_ABAJO_POPUP, Panel_MENSAJES_ABAJO, Label_MENSAJE_ABAJO, _fuentesReclutamiento.MensajeError, Proceso.Error);
        }
        else
        {
            cargar_cargos_en_fuente(ID_FUENTE);

            Panel_NUEVO_CARGO.Visible = false;

            Informar(Panel_FONDO_MENSAJE_ABAJO, Image_MENSAJE_ABAJO_POPUP, Panel_MENSAJES_ABAJO, Label_MENSAJE_ABAJO, "El Cargo fue adicionado correctamente.", Proceso.Correcto);
        }
    }
    protected void Button_CANCELAR_CARGO_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.NuevoCargo);

        Button_NUEVO_CARGO.Visible = true;
    }
    protected void Button_NUEVA_COMUNICACION_Click(object sender, EventArgs e)
    {
        Button_NUEVA_COMUNICACION.Visible = false;

        Mostrar(Acciones.NuevaComunicacion);
        Limpiar(Acciones.NuevaComunicacion);
        Cargar(Acciones.NuevaComunicacion);
    }
    protected void Button_ADICIONAR_COMUNICACION_Click(object sender, EventArgs e)
    {
        Decimal ID_FUENTE = Convert.ToDecimal(HiddenField_ID_FUENTE.Value);

        DateTime FECHA_R = Convert.ToDateTime(TextBox_fecha.Text);
        String COMUNICAION = TextBox_Comentarios_comunicacion.Text.Trim();

        fuentesReclutamiento _fuentesReclutamiento = new fuentesReclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Decimal REGISTRO = _fuentesReclutamiento.AdicionarRecComFuentes(ID_FUENTE, FECHA_R, COMUNICAION);

        if (REGISTRO <= 0)
        {
            Informar(Panel_FONDO_MENSAJE_ABAJO, Image_MENSAJE_ABAJO_POPUP, Panel_MENSAJES_ABAJO, Label_MENSAJE_ABAJO, _fuentesReclutamiento.MensajeError, Proceso.Error);
        }
        else
        {
            cargar_comunicaciones(ID_FUENTE);

            Panel_NUEVA_COMUNICACION.Visible = false;

            Informar(Panel_FONDO_MENSAJE_ABAJO, Image_MENSAJE_ABAJO_POPUP, Panel_MENSAJES_ABAJO, Label_MENSAJE_ABAJO, "La comunicación fue registrada correctamente", Proceso.Correcto);
        }
    }

    protected void Button_CANCALAR_COMUNICACION_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.NuevaComunicacion);

        Button_NUEVA_COMUNICACION.Visible = true;
    }

    private void cargar_DropDownList_ID_OCUPACION_FILTRADO()
    {
        DropDownList_ID_OCUPACION.Items.Clear();

        ListItem item = new ListItem("Seleccione...", "");
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

    protected void Button_BUSCADOR_CARGO_Click(object sender, EventArgs e)
    {
        cargar_DropDownList_ID_OCUPACION_FILTRADO();
    }
    protected void Button_OcultarFuente_Click(object sender, EventArgs e)
    {
        Decimal ID_FUENTE = Convert.ToDecimal(HiddenField_ID_FUENTE.Value);

        fuentesReclutamiento _fuente = new fuentesReclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean verificador = _fuente.OcultarRecFuentes(ID_FUENTE);

        if (verificador == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _fuente.MensajeError, Proceso.Error);
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);

            if (HiddenField_TIPO_BUSQUEDA_ACTUAL.Value == "SIN_FILTRO")
            {
                cargar_GridView_RESULTADOS_BUSQUEDA();
            }
            else
            {
                Buscar();
            }   
        }
    }
}