using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brainsbits.LLB.contratacion;
using System.Data;
using Brainsbits.LLB.seleccion;
using Brainsbits.LLB.comercial;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;
using TSHAK.Components;
using Brainsbits.LLB;

public partial class _EntrevistaRotacionRetiro : System.Web.UI.Page
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
    private enum Acciones
    {
        Inicio = 0,
        Nuevo,
        Cargar,
        Modificar, 
        Buscar
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

    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = false;

                Panel_FORM_BOTONES.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_IMPRIMIR.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_CANCELAR.Visible = false;

                Panel_RESULTADOS_GRID.Visible = false;

                Panel_DATOS_TRABAJADOR.Visible = false;

                Panel_GrillaMotivosRotacion.Visible = false;

                Panel_Observaciones.Visible = false;

                Panel_BOTONES_PIE.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_IMPRIMIR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                break;
            case Acciones.Modificar:
                Button_MODIFICAR.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_CANCELAR.Visible = false;


                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
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
            case Acciones.Cargar:

                Panel_FORM_BOTONES.Visible = true;
                Button_MODIFICAR.Visible = true;
                Button_IMPRIMIR.Visible = true;

                Panel_DATOS_TRABAJADOR.Visible = true;
                Panel_GrillaMotivosRotacion.Visible = true;

                Panel_Observaciones.Visible = true;

                Panel_BOTONES_PIE.Visible = true;
                Button_MODIFICAR_1.Visible = true;
                Button_IMPRIMIR_1.Visible = true;
                break;
            case Acciones.Nuevo:
                Panel_FORM_BOTONES.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_DATOS_TRABAJADOR.Visible = true;

                Panel_GrillaMotivosRotacion.Visible = true;

                Panel_Observaciones.Visible = true;

                Panel_BOTONES_PIE.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
                break;
            case Acciones.Modificar:
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

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

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_BUSCAR.Items.Add(item);

        item = new ListItem("Documento identidad", "NUM_DOC_IDENTIDAD");
        DropDownList_BUSCAR.Items.Add(item);

        item = new ListItem("Nombres", "NOMBRES");
        DropDownList_BUSCAR.Items.Add(item);

        item = new ListItem("Apellidos", "APELLIDOS");
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
                HiddenField_FILTRO_DATO.Value = null;
                HiddenField_FILTRO_DROP.Value = null;

                iniciar_seccion_de_busqueda();
                break;
        }
    }

    private void Iniciar()
    {
        Configurar();
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }

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
        Ocultar(Acciones.Modificar);
        Mostrar(Acciones.Modificar);
        Activar(Acciones.Modificar);

        habilitarFilasGrilla(GridView_MotivosRotacion, 0);
    }

    private Int32 ContarChulosEnGrillaMotivos()
    {
        Int32 contador = 0;

        foreach (GridViewRow fila in GridView_MotivosRotacion.Rows)
        {
            CheckBox check = fila.FindControl("CheckBox_Configurado") as CheckBox;

            if (check.Checked == true)
            {
                contador += 1;
            }
        }

        return contador;
    }

    private void Actualizar()
    { 
        Decimal ID_MAESTRA_ROTACION_EMPLEADO = 0;
        Decimal ID_EMPLEADO = Convert.ToDecimal(HiddenField_ID_EMPLEADO.Value);
        Decimal REGISTRO_CONTRATO = Convert.ToDecimal(HiddenField_REGISTRO_CONTRATO.Value);
        Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        String OBSERVACIONES = TextBox_Observaciones.Text.Trim();

        if (String.IsNullOrEmpty(HiddenField_ID_MAESTRA_ENTREVISTA_EMPLEADO.Value) == false)
        {
            ID_MAESTRA_ROTACION_EMPLEADO = Convert.ToDecimal(HiddenField_ID_MAESTRA_ENTREVISTA_EMPLEADO.Value);
        }

        List<EntrevistaRotacionEmpleado> listaMotivosResultados = new List<EntrevistaRotacionEmpleado>();

        for (int i = 0; i < GridView_MotivosRotacion.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_MotivosRotacion.Rows[i];

            CheckBox check = filaGrilla.FindControl("CheckBox_Configurado") as CheckBox;

            if (check.Checked == true)
            {
                EntrevistaRotacionEmpleado _rotacionParaLista = new EntrevistaRotacionEmpleado();

                _rotacionParaLista.ACTIVO = true;
                _rotacionParaLista.ID_DETALLE_ROTACION_EMPLEADO = Convert.ToDecimal(GridView_MotivosRotacion.DataKeys[i].Values["ID_DETALLE_ROTACION_EMPLEADO"]);
                _rotacionParaLista.ID_MAESTRA_ROTACION_EMPLEADO = ID_MAESTRA_ROTACION_EMPLEADO;
                _rotacionParaLista.ID_ROTACION_EMPRESA = Convert.ToDecimal(GridView_MotivosRotacion.DataKeys[i].Values["ID_ROTACION_EMPRESA"]);

                listaMotivosResultados.Add(_rotacionParaLista);
            }
        }

        MotivoRotacionRetiro _motivo = new MotivoRotacionRetiro(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        ID_MAESTRA_ROTACION_EMPLEADO = _motivo.ActualizarResultadosEntrevistaRetiroDeEmpleado(ID_MAESTRA_ROTACION_EMPLEADO, ID_EMPLEADO, OBSERVACIONES,listaMotivosResultados);

        if (ID_MAESTRA_ROTACION_EMPLEADO <= 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _motivo.MensajeError, Proceso.Error);
        }
        else
        {
   
            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.Cargar);

            Cargar(ID_SOLICITUD, ID_EMPLEADO, ID_EMPRESA, REGISTRO_CONTRATO);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La entrevista de Rotación y Retiro fue actualizada correctamente.", Proceso.Correcto);
        }
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        Int32 contadorChulos = ContarChulosEnGrillaMotivos();

        if (contadorChulos <= 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Para poder continuar primero debe seleccionar por lo menos un Motivo de Rotación y Retiro de la lista.", Proceso.Advertencia);
        }
        else
        {
            Actualizar();  
        }
    }
    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        if (HiddenField_ID_MAESTRA_ENTREVISTA_EMPLEADO.Value == "")
        {
            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Cargar(Acciones.Inicio);
        }
        else
        {
            Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);
            Decimal ID_EMPLEADO = Convert.ToDecimal(HiddenField_ID_EMPLEADO.Value);
            Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
            Decimal REGISTRO_CONTRATO = Convert.ToDecimal(HiddenField_REGISTRO_CONTRATO.Value);

            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.Cargar);

            Cargar(ID_SOLICITUD, ID_EMPLEADO, ID_EMPRESA, REGISTRO_CONTRATO);
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
            if (DropDownList_BUSCAR.SelectedValue == "NUM_DOC_IDENTIDAD")
            {
                configurarCaracteresAceptadosBusqueda(false, true);
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
                        configurarCaracteresAceptadosBusqueda(true, true);
                    }
                }
            }
        }
        TextBox_BUSCAR.Text = "";
    }

    private void Buscar()
    {
        String datosCapturados = HiddenField_FILTRO_DATO.Value;
        String campo = HiddenField_FILTRO_DROP.Value;

        registroContrato _registroContrato = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaResultadosBusqueda = new DataTable();

        if (DropDownList_BUSCAR.SelectedValue == "NUM_DOC_IDENTIDAD")
        {
            tablaResultadosBusqueda = _registroContrato.ObtenerPorNumeroIdentificacion(datosCapturados);
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "NOMBRES")
            {
                tablaResultadosBusqueda = _registroContrato.ObtenerPorNombre(datosCapturados);
            }
            else
            {
                if (DropDownList_BUSCAR.SelectedValue == "APELLIDOS")
                {
                    tablaResultadosBusqueda = _registroContrato.ObtenerPorApellido(datosCapturados);
                }
            }
        }

        if (tablaResultadosBusqueda.Rows.Count <= 0)
        {
            if (_registroContrato.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _registroContrato.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros para la busqueda solicitada.", Proceso.Advertencia);
            }

            Panel_RESULTADOS_GRID.Visible = false;
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
        HiddenField_FILTRO_DATO.Value = TextBox_BUSCAR.Text.Trim();
        HiddenField_FILTRO_DROP.Value = DropDownList_BUSCAR.SelectedValue;

        Buscar();
    }
    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;

        Buscar();
    }

    private void cargarInfoTrabajador(DataRow filaInfoTrabajador, DataRow filaInfoCliente)
    {
        Label_APELLIDOS_TRABAJADOR.Text = filaInfoTrabajador["APELLIDOS"].ToString().Trim();
        Label_NOMBRES_TRABAJADOR.Text = filaInfoTrabajador["NOMBRES"].ToString().Trim();
        Label_TIPO_DOCUMENTO.Text = filaInfoTrabajador["TIP_DOC_IDENTIDAD"].ToString().Trim();
        Label_NUM_DOC_IDENTIDAD.Text = filaInfoTrabajador["NUM_DOC_IDENTIDAD"].ToString().Trim();
        Label_ESTADO.Text = filaInfoTrabajador["ARCHIVO"].ToString().Trim();
        Label_RAZ_SOCIAL.Text = filaInfoCliente["RAZ_SOCIAL"].ToString().Trim();
        Label_REGISTRO_CONTRATO.Text = HiddenField_REGISTRO_CONTRATO.Value;
    }

    private void Desactivar(Acciones accion)
    {
        switch (accion)
        { 
            case Acciones.Inicio:
                TextBox_Observaciones.Enabled = false;
                break;
            case Acciones.Cargar:
                TextBox_Observaciones.Enabled = false;
                break;
        }
    }

    private DataTable configurarTablaParaGrillaMotivos()
    {
        DataTable tablaTemp = new DataTable();

        tablaTemp.Columns.Add("ID_MAESTRA_ROTACION");
        tablaTemp.Columns.Add("ID_DETALLE_ROTACION");
        tablaTemp.Columns.Add("ID_ROTACION_EMPRESA");
        tablaTemp.Columns.Add("ID_DETALLE_ROTACION_EMPLEADO");
        tablaTemp.Columns.Add("TITULO");
        tablaTemp.Columns.Add("TITULO_MAESTRA_ROTACION");

        return tablaTemp;
    }

    private void Activar(Acciones accion)
    {
        switch (accion)
        { 
            case Acciones.Nuevo:
                TextBox_Observaciones.Enabled = true;
                break;
            case Acciones.Modificar:
                TextBox_Observaciones.Enabled = true;
                break;
        }
    }

    private void Limpiar(Acciones accion)
    { 
        switch(accion)
        {
            case Acciones.Nuevo:
                TextBox_Observaciones.Text = "";
                HiddenField_ID_MAESTRA_ENTREVISTA_EMPLEADO.Value = "";
            break;
        }
    }

    private void habilitarFilasGrilla(GridView grilla, Int32 colInicio)
    {
        for (int j = 0; j < grilla.Rows.Count; j++)
        {
            for (int i = colInicio; i < grilla.Columns.Count; i++)
            {
                grilla.Rows[j].Cells[i].Enabled = true;
            }
        }
    }

    private void CargarGrillaMotivosRotacionDesdeTabla(DataTable tablaCat)
    {
        GridView_MotivosRotacion.DataSource = tablaCat;
        GridView_MotivosRotacion.DataBind();

        for (int i = 0; i < GridView_MotivosRotacion.Rows.Count; i++)
        {
            DataRow filaTabla = tablaCat.Rows[i];
            GridViewRow filaGrilla = GridView_MotivosRotacion.Rows[i];

            CheckBox check = filaGrilla.FindControl("CheckBox_Configurado") as CheckBox;

            if ((DBNull.Value.Equals(filaTabla["ID_DETALLE_ROTACION_EMPLEADO"]) == true) || (filaTabla["ID_DETALLE_ROTACION_EMPLEADO"].ToString().Trim() == "0"))
            {
                check.Checked = false;
            }
            else
            {
                check.Checked = true;
            }
        }
    }

    private void Cargar(Decimal ID_SOLICITUD, Decimal ID_EMPLEADO, Decimal ID_EMPRESA, Decimal REGISTRO_CONTRATO)
    {
        HiddenField_ID_SOLICITUD.Value = ID_SOLICITUD.ToString();
        HiddenField_ID_EMPLEADO.Value = ID_EMPLEADO.ToString();
        HiddenField_ID_EMPRESA.Value = ID_EMPRESA.ToString();
        HiddenField_REGISTRO_CONTRATO.Value = REGISTRO_CONTRATO.ToString();

        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoTrabajador = _radicacionHojasDeVida.ObtenerRegSolicitudesingresoPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));

        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaCliente = _cliente.ObtenerEmpresaConIdEmpresa(ID_EMPRESA);

        if (tablaInfoTrabajador.Rows.Count <= 0)
        {
            if (_radicacionHojasDeVida.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _radicacionHojasDeVida.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información del Trabajador Seleciconado.", Proceso.Advertencia);
            }
        }
        else
        {
            if (tablaCliente.Rows.Count <= 0)
            {
                if (_cliente.MensajeError != null)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _cliente.MensajeError, Proceso.Error);
                }
                else
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información de la Empresa asociada al trabajador.", Proceso.Advertencia);
                }
            }
            else
            {
                DataRow filainfoCliente = tablaCliente.Rows[0];

                cargarInfoTrabajador(tablaInfoTrabajador.Rows[0], filainfoCliente);

                MotivoRotacionRetiro _motivo = new MotivoRotacionRetiro(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                DataTable tablaMotivosAsociadosAEmpresa = _motivo.ObtenerMotivosActivosEmpresa(ID_EMPRESA);

                if (tablaMotivosAsociadosAEmpresa.Rows.Count <= 0)
                {
                    if (_motivo.MensajeError != null)
                    {
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _motivo.MensajeError, Proceso.Error);
                    }
                    else
                    {
                        Ocultar(Acciones.Inicio);
                        Desactivar(Acciones.Inicio);
                        Mostrar(Acciones.Inicio);
                        Cargar(Acciones.Inicio);

                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La empresa no tiene Motivos de Rotación y Retiros asociados.", Proceso.Advertencia);
                    }
                }
                else
                { 
                    _motivo.MensajeError = null;

                    Boolean correcto = true;

                    DataTable tablaResultadosEntrevistaRetiro = _motivo.ObtenerResultadosEntrevistaDeRetiroParaEmpleado(ID_EMPLEADO);

                    if (tablaResultadosEntrevistaRetiro.Rows.Count <= 0)
                    {
                        if (_motivo.MensajeError != null)
                        {
                            if (_motivo.MensajeError != null)
                            {
                                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _motivo.MensajeError, Proceso.Error);
                                correcto = false;
                            }
                        }
                    }

                    if (correcto == true)
                    { 
                        DataTable tablaparaGrilla = configurarTablaParaGrillaMotivos();

                        Boolean idEncontrado = false;

                        Int32 contadorResultados = 0;

                        for (int i = 0; i < tablaMotivosAsociadosAEmpresa.Rows.Count; i++)
                        {
                            idEncontrado = false;

                            DataRow filaMotivo = tablaMotivosAsociadosAEmpresa.Rows[i];
                            DataRow filaParaGrilla = tablaparaGrilla.NewRow();

                            filaParaGrilla["ID_MAESTRA_ROTACION"] = filaMotivo["ID_MAESTRA_ROTACION"];
                            filaParaGrilla["ID_DETALLE_ROTACION"] = filaMotivo["ID_DETALLE_ROTACION"];
                            filaParaGrilla["ID_ROTACION_EMPRESA"] = filaMotivo["ID_ROTACION_EMPRESA"];

                            filaParaGrilla["TITULO"] = filaMotivo["TITULO"];
                            filaParaGrilla["TITULO_MAESTRA_ROTACION"] = filaMotivo["TITULO_MAESTRA_ROTACION"];

                            Decimal ID_ROTACION_EMPRESA_1 = Convert.ToDecimal(filaMotivo["ID_ROTACION_EMPRESA"]);

                            Decimal ID_DETALLE_ROTACION_EMPLEADO = 0;
                            
                            for(int j = 0; j < tablaResultadosEntrevistaRetiro.Rows.Count; j++)
                            {
                                DataRow filaResultado = tablaResultadosEntrevistaRetiro.Rows[j];

                                Decimal ID_ROTACION_EMPRESA_2 = Convert.ToDecimal(filaResultado["ID_ROTACION_EMPRESA"]);
                                
                                if(ID_ROTACION_EMPRESA_1 == ID_ROTACION_EMPRESA_2)
                                {
                                    contadorResultados += 1;

                                    ID_DETALLE_ROTACION_EMPLEADO = Convert.ToDecimal(filaResultado["ID_DETALLE_ROTACION_EMPLEADO"]);
                                    idEncontrado = true;

                                    if (contadorResultados == 1)
                                    { 
                                        HiddenField_ID_MAESTRA_ENTREVISTA_EMPLEADO.Value = filaResultado["ID_MAESTRA_ROTACION_EMPLEADO"].ToString().Trim();

                                        TextBox_Observaciones.Text = filaResultado["OBSERVACIONES"].ToString().Trim();
                                    }
                                    break;
                                }
                            }

                            if(idEncontrado == true)
                            {
                                filaParaGrilla["ID_DETALLE_ROTACION_EMPLEADO"] = ID_DETALLE_ROTACION_EMPLEADO;
                            }
                            else
                            {
                                filaParaGrilla["ID_DETALLE_ROTACION_EMPLEADO"] = 0;
                            }

                            tablaparaGrilla.Rows.Add(filaParaGrilla);
                        }

                        CargarGrillaMotivosRotacionDesdeTabla(tablaparaGrilla);

                        Ocultar(Acciones.Inicio);

                        if (contadorResultados <= 0)
                        {
                            Mostrar(Acciones.Nuevo);
                            Activar(Acciones.Nuevo);
                            Limpiar(Acciones.Nuevo);

                            habilitarFilasGrilla(GridView_MotivosRotacion, 0);
                        }
                        else
                        {
                            Mostrar(Acciones.Cargar);
                            Desactivar(Acciones.Cargar);

                            inhabilitarFilasGrilla(GridView_MotivosRotacion, 0);
                        }
                    }
                }
            }
        }
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

    protected void GridView_RESULTADOS_BUSQUEDA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {

            Decimal ID_SOLICITUD = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_SOLICITUD"]);
            Decimal ID_EMPLEADO = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_EMPLEADO"]);
            Decimal ID_EMPRESA = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_EMPRESA"]);
            Decimal REGISTRO_CONTRATO = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["NUM_CONTRATO"]);

            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.Cargar);

            Cargar(ID_SOLICITUD, ID_EMPLEADO, ID_EMPRESA, REGISTRO_CONTRATO);
        }
    }
    protected void Button_IMPRIMIR_Click(object sender, EventArgs e)
    {
        Decimal ID_EMPLEADO = Convert.ToDecimal(HiddenField_ID_EMPLEADO.Value);
        Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        radicacionHojasDeVida _rad = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaSolicitud = _rad.ObtenerRegSolicitudesingresoPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));
        DataRow filaSolicitud = tablaSolicitud.Rows[0];

        maestrasInterfaz _maestrasInterfaz = new maestrasInterfaz();

        byte[] datosArchivo = _maestrasInterfaz.GenerarPDFEntrevistaRetiro(ID_EMPLEADO, ID_SOLICITUD, ID_EMPRESA);

        String filename = filaSolicitud["NUM_DOC_IDENTIDAD"].ToString().Trim() + "_INFORME_ENTREVISTA_RETIRO";
        filename = filename.Replace(' ', '_');

        Response.Clear();
        Response.BufferOutput = false; 
        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", "attachment;FileName=" + filename + ".pdf");
        Response.BinaryWrite(datosArchivo);
        Response.End();
    }
}