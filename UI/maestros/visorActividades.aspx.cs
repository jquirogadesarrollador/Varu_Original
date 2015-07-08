using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brainsbits.LLB.maestras;
using TSHAK.Components;
using Brainsbits.LLB;
using Brainsbits.LLB.programasRseGlobal;
using System.Data;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB.seleccion;
using System.IO;
using System.Net;
using Brainsbits.LLB.comercial;
using AjaxControlToolkit;
using Brainsbits.LLB.seguridad;
using System.Configuration;

public partial class _VisorActividades : System.Web.UI.Page
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

        int proceso = Convert.ToInt32(QueryStringSeguro["proceso"]);
        NOMBRE_AREA = _tools.ObtenerTablaNombreArea(proceso);

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
    private System.Drawing.Color colorRojo = System.Drawing.ColorTranslator.FromHtml("#F8523A");
    private System.Drawing.Color colorGris = System.Drawing.ColorTranslator.FromHtml("#cccccc");

    private enum Acciones
    {
        Inicio = 0,
        Cargar, 
        Modificar,
        Reprogramar,
        Cancelar,
        Terminar,
        ResultadosActividad,
        NuevoAdjunto,
        CargarAdjuntos, 
        AjustarPresupuesto,
        ModificarFinal
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }

    private enum Listas
    {
        Actividades,
        EstadosActividades,
        TiposActividad,
        SectoresActividad,
        Regionales,
        SubProgramas, 
        EntidadesCOlaboradoras, 
        Encargados, 
        MotivosCancelacion, 
        MotivosReprogramacion,
        ResponsablesCompromisos
    }

    private enum AccionesSobreBotones
    { 
        Modificar = 1,
        Reprogramar,
        AjustarPresupuesto,
        CancelarActividad, 
        ResultadosActividad, 
        ModificarFinal
    }

    private enum AccionesGrilla
    {
        Nuevo = 0,
        Modificar = 1,
        Ninguna = 2
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

    private void configurar_paneles_popup(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_mensaje.CssClass = "panel_popup";
    }

    private void Configurar()
    {
        configurar_paneles_popup(Panel_FONDO_MENSAJE, Panel_MENSAJES);
        configurar_paneles_popup(Panel_FONDO_MENSAJE_ENCUESTA, Panel_MENSAJES_ENCUESTA);
    }

    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = false;

                Panel_FORM_BOTONES_ENCABEZADO.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_REPROGRAMAR.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_CANCELAR_ACTIVIDAD.Visible = false;
                Button_RESULTADOS.Visible = false;
                Button_MODIFICARFINAL.Visible = false;

                Panel_InfoActividad.Visible = false;
                Panel_CONTROL_REGISTRO.Visible = false;
                Panel_Reprogramacion.Visible = false;

                Panel_MOTIVO_CANCELACION.Visible = false;
                Panel_HistorialReprogramaciones.Visible = false;

                Panel_BOTONES_ACCION_PIE.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_REPROGRAMAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                Button_CANCELAR_ACTIVIDAD_1.Visible = false;
                Button_RESULTADOS_1.Visible = false;
                Button_MODIFICARFINAL_1.Visible = false;

                Panel_ResultadosActividad.Attributes.Add("style","Display:none"); 

                Panel_ResultadosPresupuestoPersonal.Visible = false;

                Panel_ResultadosEncuesta.Visible = false;
                Panel_ResultadosEncuestaLinkArchivo.Visible = false;
                Panel_ResultadoEncuestaUpload.Visible = false;

                Panel_ControlAsistencia.Visible = false;
                Panel_linkArchivoAsistencia.Visible = false;
                Panel_UploadArchivoAsistencia.Visible = false;


                Panel_EntidadesColaboradoras.Visible = false;
                Panel_BotonesAccionEntidades.Visible = false;
                Button_NuevaEntidadColaboradora.Visible = false;
                Button_GuardarEntidad.Visible = false;
                Button_CancelarEntidad.Visible = false;

                Panel_Compromisos.Visible = false;
                Panel_BotonesAccionCompromisos.Visible = false;
                Button_NuevoCompromiso.Visible = false;
                Button_GuardarCompromiso.Visible = false;
                Button_CancelarCompromiso.Visible = false;

                Panel_ImagenYConclusiones.Visible = false;
                Panel_SubirArchivoImagenRepresentativa.Visible = false;


                Button_AJUSTARPRESUPUESTO.Visible = false;
                Panel_AjustarPresupuesto.Visible = false;
                Button_AJUSTARPRESUPUESTO_1.Visible = false;

                Panel_HistorialAjustesPresupuesto.Visible = false;

                Panel_AdjuntarArchivos.Visible = false;
                Panel_NuevoAdjunto.Visible = false;
                Button_NuevoAdjunto.Visible = false;
                Button_GuardarAdjunto.Visible = false;
                Button_CancelarAdjunto.Visible = false;

                Label_PersonalAsistioMaximo.Visible = false;
                break;
            case Acciones.Modificar:
                Button_GUARDAR.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_REPROGRAMAR.Visible = false;
                Button_CANCELAR_ACTIVIDAD.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_AJUSTARPRESUPUESTO.Visible = false;
                Button_RESULTADOS.Visible = false;
                Button_MODIFICARFINAL.Visible = false;

                Panel_CONTROL_REGISTRO.Visible = false;
                Panel_Reprogramacion.Visible = false;

                Panel_MOTIVO_CANCELACION.Visible = false;

                Panel_HistorialReprogramaciones.Visible = false;

                Panel_AjustarPresupuesto.Visible = false;

                Button_GUARDAR_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_REPROGRAMAR_1.Visible = false;
                Button_CANCELAR_ACTIVIDAD_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                Button_AJUSTARPRESUPUESTO_1.Visible = false;
                Button_RESULTADOS_1.Visible = false;
                Button_MODIFICARFINAL_1.Visible = false;

                Panel_HistorialAjustesPresupuesto.Visible = false;


                Panel_AdjuntarArchivos.Visible = false;
                Panel_NuevoAdjunto.Visible = false;
                Button_NuevoAdjunto.Visible = false;
                Button_GuardarAdjunto.Visible = false;
                Button_CancelarAdjunto.Visible = false;
                break;
            case Acciones.ModificarFinal:
                Button_GUARDAR.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_REPROGRAMAR.Visible = false;
                Button_CANCELAR_ACTIVIDAD.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_AJUSTARPRESUPUESTO.Visible = false;
                Button_RESULTADOS.Visible = false;
                Button_MODIFICARFINAL.Visible = false;

                Panel_CONTROL_REGISTRO.Visible = false;
                Panel_Reprogramacion.Visible = false;

                Panel_MOTIVO_CANCELACION.Visible = false;

                Panel_HistorialReprogramaciones.Visible = false;

                Panel_AjustarPresupuesto.Visible = false;

                Button_GUARDAR_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_REPROGRAMAR_1.Visible = false;
                Button_CANCELAR_ACTIVIDAD_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                Button_AJUSTARPRESUPUESTO_1.Visible = false;
                Button_RESULTADOS_1.Visible = false;
                Button_MODIFICARFINAL_1.Visible = false;

                Panel_HistorialAjustesPresupuesto.Visible = false;

                Panel_AdjuntarArchivos.Visible = false;
                Panel_NuevoAdjunto.Visible = false;
                Button_NuevoAdjunto.Visible = false;
                Button_GuardarAdjunto.Visible = false;
                Button_CancelarAdjunto.Visible = false;
                break;
            case Acciones.Reprogramar:
                Button_GUARDAR.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_REPROGRAMAR.Visible = false;
                Button_CANCELAR_ACTIVIDAD.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_AJUSTARPRESUPUESTO.Visible = false;
                Button_RESULTADOS.Visible = false;
                Button_MODIFICARFINAL.Visible = false;

                Panel_CONTROL_REGISTRO.Visible = false;

                Panel_MOTIVO_CANCELACION.Visible = false;

                Panel_HistorialReprogramaciones.Visible = false;
                Panel_HistorialAjustesPresupuesto.Visible = false;

                Panel_AjustarPresupuesto.Visible = false;

                Button_GUARDAR_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_REPROGRAMAR_1.Visible = false;
                Button_CANCELAR_ACTIVIDAD_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                Button_AJUSTARPRESUPUESTO_1.Visible = false;
                Button_RESULTADOS_1.Visible = false;
                Button_MODIFICARFINAL_1.Visible = false;

                Panel_AdjuntarArchivos.Visible = false;
                Panel_NuevoAdjunto.Visible = false;
                Button_NuevoAdjunto.Visible = false;
                Button_GuardarAdjunto.Visible = false;
                Button_CancelarAdjunto.Visible = false;

                break;
            case Acciones.AjustarPresupuesto:
                Button_GUARDAR.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_REPROGRAMAR.Visible = false;
                Button_CANCELAR_ACTIVIDAD.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_AJUSTARPRESUPUESTO.Visible = false;
                Button_RESULTADOS.Visible = false;
                Button_MODIFICARFINAL.Visible = false;

                Panel_CONTROL_REGISTRO.Visible = false;

                Panel_Reprogramacion.Visible = false;
                Panel_MOTIVO_CANCELACION.Visible = false;

                Panel_HistorialReprogramaciones.Visible = false;
                Panel_HistorialAjustesPresupuesto.Visible = false;

                Button_GUARDAR_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_REPROGRAMAR_1.Visible = false;
                Button_CANCELAR_ACTIVIDAD_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                Button_AJUSTARPRESUPUESTO_1.Visible = false;
                Button_RESULTADOS_1.Visible = false;
                Button_MODIFICARFINAL_1.Visible = false;

                Panel_AdjuntarArchivos.Visible = false;
                Panel_NuevoAdjunto.Visible = false;
                Button_NuevoAdjunto.Visible = false;
                Button_GuardarAdjunto.Visible = false;
                Button_CancelarAdjunto.Visible = false;
                break;
            case Acciones.Cancelar:
                Button_GUARDAR.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_REPROGRAMAR.Visible = false;
                Button_CANCELAR_ACTIVIDAD.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_AJUSTARPRESUPUESTO.Visible = false;
                Button_RESULTADOS.Visible = false;
                Button_MODIFICARFINAL.Visible = false;

                Panel_CONTROL_REGISTRO.Visible = false;

                Panel_Reprogramacion.Visible = false;

                Panel_HistorialReprogramaciones.Visible = false;
                Panel_HistorialAjustesPresupuesto.Visible = false;

                Panel_AjustarPresupuesto.Visible = false;

                Button_GUARDAR_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_REPROGRAMAR_1.Visible = false;
                Button_CANCELAR_ACTIVIDAD_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                Button_AJUSTARPRESUPUESTO_1.Visible = false;
                Button_RESULTADOS_1.Visible = false;
                Button_MODIFICARFINAL_1.Visible = false;

                Panel_AdjuntarArchivos.Visible = false;
                Panel_NuevoAdjunto.Visible = false;
                Button_NuevoAdjunto.Visible = false;
                Button_GuardarAdjunto.Visible = false;
                Button_CancelarAdjunto.Visible = false;

                Panel_LinkArchivoCancelacion.Visible = false;
                break;
            case Acciones.Terminar:
                Button_GUARDAR.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_REPROGRAMAR.Visible = false;
                Button_CANCELAR_ACTIVIDAD.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_RESULTADOS.Visible = false;
                Button_AJUSTARPRESUPUESTO.Visible = false;
                Button_MODIFICARFINAL.Visible = false;

                Panel_CONTROL_REGISTRO.Visible = false;

                Panel_Reprogramacion.Visible = false;

                Panel_HistorialReprogramaciones.Visible = false;
                Panel_HistorialAjustesPresupuesto.Visible = false;

                Panel_AjustarPresupuesto.Visible = false;

                Button_GUARDAR_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_REPROGRAMAR_1.Visible = false;
                Button_CANCELAR_ACTIVIDAD_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                Button_RESULTADOS_1.Visible = false;
                Button_MODIFICARFINAL_1.Visible = false;

                Button_AJUSTARPRESUPUESTO_1.Visible = false;

                Panel_AdjuntarArchivos.Visible = false;
                Panel_NuevoAdjunto.Visible = false;
                Button_NuevoAdjunto.Visible = false;
                Button_GuardarAdjunto.Visible = false;
                Button_CancelarAdjunto.Visible = false;
                break;
            case Acciones.CargarAdjuntos:
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

                DropDownList_SubPrograma.Enabled = false;

                DropDownList_IdActividad.Enabled = false;
                DropDownList_Tipo.Enabled = false;
                DropDownList_Sector.Enabled = false;
                DropDownList_EstadoActividad.Enabled = false;

                TextBox_Resumen.Enabled = false;
                TextBox_FechaActividad.Enabled = false;
                TimePicker_HoraInicioActividad.Enabled = false;
                TimePicker_HoraFinActividad.Enabled = false;
                TextBox_MotivoReprogramacion.Enabled = false;
                TextBox_PresupuestoAsignado.Enabled = false;
                TextBox_PersonalCitado.Enabled = false;
                DropDownList_Encargado.Enabled = false;
                DropDownList_REGIONAL.Enabled = false;
                DropDownList_DEPARTAMENTO.Enabled = false;
                DropDownList_CIUDAD.Enabled = false;

                TextBox_Motivocancelacion.Enabled = false;


                TextBox_PresupuestoFinal.Enabled = false;
                TextBox_PersonalFinal.Enabled = false;

                TextBox_LogisticaBuena.Enabled = false;
                TextBox_LogisticaRegular.Enabled = false;
                TextBox_LogisticaMala.Enabled = false;
                TextBox_InstructorBuena.Enabled = false;
                TextBox_InstructorRegular.Enabled = false;
                TextBox_InstructorMala.Enabled = false;
                TextBox_InstalacionesBuena.Enabled = false;
                TextBox_InstalacionesRegular.Enabled = false;
                TextBox_InstalacionesMala.Enabled = false;
                TextBox_TotalBuena.Enabled = false;
                TextBox_TotalRegular.Enabled = false;
                TextBox_TotalMala.Enabled = false;
                TextBox_LogisticaPorcentajeBuena.Enabled = false;
                TextBox_InstructorPorcentajeBuena.Enabled = false;
                TextBox_InstalacionesPorcentajeBuena.Enabled = false;
                TextBox_TotalBenaPorcentaje.Enabled = false;
                TextBox_TotalRegularPorcentaje.Enabled = false;
                TextBox_TotalMalaPorcentaje.Enabled = false;

                GridView_ControlAsistencia.Enabled = false;

                TextBox_ConclusionesActividad.Enabled = false;

                DropDownList_MotivoCancelacion.Enabled = false;

                DropDownList_TipoReprogramacion.Enabled = false;
                break;
        }
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Button_MODIFICAR.Visible = true;
                Button_REPROGRAMAR.Visible = true;
                Button_RESULTADOS.Visible = true;
                Button_CANCELAR_ACTIVIDAD.Visible = true;
                
                Button_AJUSTARPRESUPUESTO.Visible = true;

                Panel_InfoActividad.Visible = true;
                Panel_CONTROL_REGISTRO.Visible = true;

                Panel_BOTONES_ACCION_PIE.Visible = true;
                Button_MODIFICAR_1.Visible = true;
                Button_REPROGRAMAR_1.Visible = true;
                Button_CANCELAR_ACTIVIDAD_1.Visible = true;
                Button_RESULTADOS_1.Visible = true;
                
                Button_AJUSTARPRESUPUESTO_1.Visible = true;

                Panel_HistorialAjustesPresupuesto.Visible = true;

                Panel_INFO_ADICIONAL_MODULO.Visible = true;
                break;
            case Acciones.Modificar:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Panel_BOTONES_ACCION_PIE.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
                break;
            case Acciones.ModificarFinal:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Panel_BOTONES_ACCION_PIE.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;

                Panel_ImagenYConclusiones.Visible = true;
                Panel_SubirArchivoImagenRepresentativa.Visible = true;
                break;
            case Acciones.Reprogramar:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Panel_BOTONES_ACCION_PIE.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_Reprogramacion.Visible = true;

                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
                break;
            case Acciones.AjustarPresupuesto:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Panel_BOTONES_ACCION_PIE.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_AjustarPresupuesto.Visible = true;

                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
                break;
            case Acciones.Cancelar:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Panel_BOTONES_ACCION_PIE.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_MOTIVO_CANCELACION.Visible = true;

                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;

                Panel_FileUploadArchivoCancelacion.Visible = true;
                break;
            case Acciones.Terminar:

                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Panel_BOTONES_ACCION_PIE.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_ResultadosActividad.Attributes.Add("style","Display:block"); 
                Panel_ResultadosPresupuestoPersonal.Visible = true;

                if (HiddenField_SECCIONES_HABILITADAS.Value.Contains("Resultados Encuesta") == true)
                {
                    Panel_ResultadosEncuesta.Visible = true;
                    Panel_ResultadoEncuestaUpload.Visible = true;
                }
                
                if (HiddenField_SECCIONES_HABILITADAS.Value.Contains("Control Asistencia") == true)
                {
                    Panel_ControlAsistencia.Visible = true;
                    Panel_UploadArchivoAsistencia.Visible = true;
                }
                
                if (HiddenField_SECCIONES_HABILITADAS.Value.Contains("Entidades Colaboradoras") == true)
                {
                    Panel_EntidadesColaboradoras.Visible = true;
                    Panel_BotonesAccionEntidades.Visible = true;
                    Button_NuevaEntidadColaboradora.Visible = true;
                    GridView_EntidadesColaboradoras.Columns[0].Visible = true;
                }

                if (HiddenField_SECCIONES_HABILITADAS.Value.Contains("Compromisos"))
                {
                    Panel_Compromisos.Visible = true;
                    Panel_BotonesAccionCompromisos.Visible = true;
                    Button_NuevoCompromiso.Visible = true;
                    GridView_Compromisos.Columns[0].Visible = true;
                }
                
                Panel_ImagenYConclusiones.Visible = true;
                Panel_SubirArchivoImagenRepresentativa.Visible = true;


                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
                break;
            case Acciones.ResultadosActividad:
                Panel_ResultadosActividad.Attributes.Add("style","Display:block"); 
                Panel_ResultadosPresupuestoPersonal.Visible = true;

                if (HiddenField_SECCIONES_HABILITADAS.Value.Contains("Resultados Encuesta") == true)
                {
                    Panel_ResultadosEncuesta.Visible = true;
                    Panel_ResultadosEncuestaLinkArchivo.Visible = true;
                }

                if (HiddenField_SECCIONES_HABILITADAS.Value.Contains("Control Asistencia") == true)
                {
                    Panel_ControlAsistencia.Visible = true;
                    Panel_linkArchivoAsistencia.Visible = true;
                }

                if (HiddenField_SECCIONES_HABILITADAS.Value.Contains("Entidades Colaboradoras") == true)
                {
                    Panel_EntidadesColaboradoras.Visible = true;
                }

                if(HiddenField_SECCIONES_HABILITADAS.Value.Contains("Compromisos") == true)
                {
                    Panel_Compromisos.Visible = true;
                }

                Panel_ImagenYConclusiones.Visible = true;

                break;
            case Acciones.NuevoAdjunto:
                break;
            case Acciones.CargarAdjuntos:
                break;
        }
    }

    private void ConfigurarAreaRseGlobal()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        int proceso = Convert.ToInt32(QueryStringSeguro["proceso"]);

        HiddenField_ID_AREA.Value = _tools.ObtenerIdAreaProceso(proceso);
    }

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

    private void cargar_DropDownList_CIUDAD(String idRegional, String idDepartamento)
    {
        DropDownList_CIUDAD.Items.Clear();

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
        DataTable tablaCiudades = _ciudad.ObtenerCiudadesPorIdDepartamentoEIdRegional(idRegional, idDepartamento);

        ListItem item = new ListItem("Seleccione...", "");
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

    private void CargarBarraDeEstadoDeActividad(String EstadoDetalleActividad)
    {
        if (Programa.EstadosDetalleActividad.CREADA.ToString() == EstadoDetalleActividad)
        {
            Label_color_estado_detalle.BackColor = colorAmarillo;
        }
        else
        {
            if (Programa.EstadosDetalleActividad.APROBADA.ToString() == EstadoDetalleActividad)
            {
                Label_color_estado_detalle.BackColor = colorVerde;
            }
            else
            {
                if (Programa.EstadosDetalleActividad.CANCELADA.ToString() == EstadoDetalleActividad)
                {
                    Label_color_estado_detalle.BackColor = colorRojo;
                }
                else
                {
                    Label_color_estado_detalle.BackColor = colorGris;
                }
            }
        }
    }

    private void Cargar_GridView_HistorialReprogramaciones_DesdeTabla(DataTable tablaHistorial)
    {
        GridView_HistorialReprogramaciones.DataSource = tablaHistorial;
        GridView_HistorialReprogramaciones.DataBind();

        for (int i = 0; i < GridView_HistorialReprogramaciones.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_HistorialReprogramaciones.Rows[i];
            DataRow filaTabla = tablaHistorial.Rows[i];

            HyperLink link = filaGrilla.FindControl("HyperLink_DocumentoAdjunto") as HyperLink;

            if (DBNull.Value.Equals(filaTabla["ARCHIVO"]) == true)
            {
                link.Enabled = false;
                link.Text = "Sin Archivo";
            }
            else
            {
                tools _tools = new tools();
                SecureQueryString QueryStringSeguro;
                QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

                QueryStringSeguro["id_historial"] = filaTabla["ID_HISTORIAL"].ToString().Trim();

                link.Enabled = true;
                link.Text = "Ver Archivo";
                link.NavigateUrl = "~/maestros/visorArchivoReprogramacion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            }
        }
    }

    private void CargarHistorialReprogramaciones(Decimal ID_DETALLE)
    { 
        Programa _programa =new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaHistorial = _programa.ObtenerHistorialReprogramaciones(ID_DETALLE);

        if (tablaHistorial.Rows.Count <= 0)
        {
            Panel_HistorialReprogramaciones.Visible = false;
            GridView_HistorialReprogramaciones.DataSource = null;
            GridView_HistorialReprogramaciones.DataBind();
        }
        else
        {
            Panel_HistorialReprogramaciones.Visible = true;

            Cargar_GridView_HistorialReprogramaciones_DesdeTabla(tablaHistorial);
        }
    }

    private void ComprobraEstadoActividad(String estadoActividad)
    {
        if ((estadoActividad == Programa.EstadosDetalleActividad.CANCELADA.ToString()) || (estadoActividad == Programa.EstadosDetalleActividad.TERMINADA.ToString()))
        {
            Button_GUARDAR.Visible = false;
            Button_MODIFICAR.Visible = false;
            Button_REPROGRAMAR.Visible = false;
            Button_CANCELAR_ACTIVIDAD.Visible = false;
            Button_CANCELAR.Visible = false;
            Button_RESULTADOS.Visible = false;
            Button_AJUSTARPRESUPUESTO.Visible = false;

            Button_GUARDAR_1.Visible = false;
            Button_MODIFICAR_1.Visible = false;
            Button_REPROGRAMAR_1.Visible = false;
            Button_CANCELAR_ACTIVIDAD_1.Visible = false;
            Button_CANCELAR_1.Visible = false;
            Button_RESULTADOS_1.Visible = false;
            Button_AJUSTARPRESUPUESTO_1.Visible = false;

            Panel_BOTONES_ACCION_PIE.Visible = false;

            if (estadoActividad == Programa.EstadosDetalleActividad.TERMINADA.ToString())
            { 
                Button_MODIFICARFINAL.Visible = true;
                Button_MODIFICARFINAL_1.Visible = true;
            }
        }
        else
        {
            if (estadoActividad == Programa.EstadosDetalleActividad.CREADA.ToString())
            {
                Button_RESULTADOS.Visible = false;

                Button_RESULTADOS_1.Visible = false;
            }
            if (estadoActividad == Programa.EstadosDetalleActividad.APROBADA.ToString())
            {
            }
        }
    }

    private void LlenarcamposDeEncuesta(String resultadosEncuesta)
    {
        String[] resultadosArray = resultadosEncuesta.Split('*');

        Int32 PUNTUACION_LOG_BUENA = 0;
        Int32 PUNTUACION_LOG_REGULAR = 0;
        Int32 PUNTUACION_LOG_MALA = 0;

        Int32 PUNTUACION_INSTRUCTOR_BUENA = 0;
        Int32 PUNTUACION_INSTRUCTOR_REGULAR = 0;
        Int32 PUNTUACION_INSTRUCTOR_MALA = 0;

        Int32 PUNTUACION_INSTALACIONES_BUENA = 0;
        Int32 PUNTUACION_INSTALACIONES_REGULAR = 0;
        Int32 PUNTUACION_INSTALACIONES_MALA = 0;

        foreach (String p in resultadosArray)
        {
            if (p.Contains(Programa.TemasEncuesta.LOGISTICA.ToString() + ":" + Programa.CalificacionesTemasEncuesta.BUENA.ToString()) == true)
            {
                PUNTUACION_LOG_BUENA = Convert.ToInt32(p.Split('=')[1]);
            }

            if (p.Contains(Programa.TemasEncuesta.LOGISTICA.ToString() + ":" + Programa.CalificacionesTemasEncuesta.REGULAR.ToString()) == true)
            {
                PUNTUACION_LOG_REGULAR = Convert.ToInt32(p.Split('=')[1]);
            }

            if (p.Contains(Programa.TemasEncuesta.LOGISTICA.ToString() + ":" + Programa.CalificacionesTemasEncuesta.MALA.ToString()) == true)
            {
                PUNTUACION_LOG_MALA = Convert.ToInt32(p.Split('=')[1]);
            }


            if (p.Contains(Programa.TemasEncuesta.INSTRUCTOR.ToString() + ":" + Programa.CalificacionesTemasEncuesta.BUENA.ToString()) == true)
            {
                PUNTUACION_INSTRUCTOR_BUENA = Convert.ToInt32(p.Split('=')[1]);
            }

            if (p.Contains(Programa.TemasEncuesta.INSTRUCTOR.ToString() + ":" + Programa.CalificacionesTemasEncuesta.REGULAR.ToString()) == true)
            {
                PUNTUACION_INSTRUCTOR_REGULAR = Convert.ToInt32(p.Split('=')[1]);
            }

            if (p.Contains(Programa.TemasEncuesta.INSTRUCTOR.ToString() + ":" + Programa.CalificacionesTemasEncuesta.MALA.ToString()) == true)
            {
                PUNTUACION_INSTRUCTOR_MALA = Convert.ToInt32(p.Split('=')[1]);
            }


            if (p.Contains(Programa.TemasEncuesta.INSTALACIONES.ToString() + ":" + Programa.CalificacionesTemasEncuesta.BUENA.ToString()) == true)
            {
                PUNTUACION_INSTALACIONES_BUENA = Convert.ToInt32(p.Split('=')[1]);
            }

            if (p.Contains(Programa.TemasEncuesta.INSTALACIONES.ToString() + ":" + Programa.CalificacionesTemasEncuesta.REGULAR.ToString()) == true)
            {
                PUNTUACION_INSTALACIONES_REGULAR = Convert.ToInt32(p.Split('=')[1]);
            }

            if (p.Contains(Programa.TemasEncuesta.INSTALACIONES.ToString() + ":" + Programa.CalificacionesTemasEncuesta.MALA.ToString()) == true)
            {
                PUNTUACION_INSTALACIONES_MALA = Convert.ToInt32(p.Split('=')[1]);
            }
        }

        TextBox_LogisticaBuena.Text = PUNTUACION_LOG_BUENA.ToString();
        TextBox_LogisticaRegular.Text = PUNTUACION_LOG_REGULAR.ToString();
        TextBox_LogisticaMala.Text = PUNTUACION_LOG_MALA.ToString();

        TextBox_InstructorBuena.Text = PUNTUACION_INSTRUCTOR_BUENA.ToString();
        TextBox_InstructorRegular.Text = PUNTUACION_INSTRUCTOR_REGULAR.ToString();
        TextBox_InstructorMala.Text = PUNTUACION_INSTRUCTOR_MALA.ToString();

        TextBox_InstalacionesBuena.Text = PUNTUACION_INSTALACIONES_BUENA.ToString();
        TextBox_InstalacionesRegular.Text = PUNTUACION_INSTALACIONES_REGULAR.ToString();
        TextBox_InstalacionesMala.Text = PUNTUACION_INSTALACIONES_MALA.ToString();


        Int32 totalLogistica = PUNTUACION_LOG_BUENA + PUNTUACION_LOG_REGULAR + PUNTUACION_LOG_MALA;
        Int32 totalInstructor = PUNTUACION_INSTRUCTOR_BUENA + PUNTUACION_INSTRUCTOR_REGULAR + PUNTUACION_INSTRUCTOR_MALA;
        Int32 totalInstalaciones = PUNTUACION_INSTALACIONES_BUENA + PUNTUACION_INSTALACIONES_REGULAR + PUNTUACION_INSTALACIONES_MALA;

        Int32 totalTotal = totalLogistica + totalInstructor + totalInstalaciones;

        Int32 totalBuena = PUNTUACION_LOG_BUENA + PUNTUACION_INSTRUCTOR_BUENA + PUNTUACION_INSTALACIONES_BUENA;
        Int32 totalRegular = PUNTUACION_LOG_REGULAR + PUNTUACION_INSTRUCTOR_REGULAR + PUNTUACION_INSTALACIONES_REGULAR;
        Int32 totalMala = PUNTUACION_LOG_MALA + PUNTUACION_INSTRUCTOR_MALA + PUNTUACION_INSTALACIONES_MALA;

        if (totalLogistica <= 0)
        {
            TextBox_LogisticaPorcentajeBuena.Text = "0 %";
        }
        else
        {
            TextBox_LogisticaPorcentajeBuena.Text = (((Decimal)PUNTUACION_LOG_BUENA * 100) / (Decimal)totalLogistica).ToString("#.#") + " %";
        }

        if (totalInstructor <= 0)
        {
            TextBox_InstructorPorcentajeBuena.Text = "0 %";
        }
        else
        {
            TextBox_InstructorPorcentajeBuena.Text = (((Decimal)PUNTUACION_INSTRUCTOR_BUENA * 100) / (Decimal)totalInstructor).ToString("#.#") + " %";
        }

        if (totalInstalaciones <= 0)
        {
            TextBox_InstalacionesPorcentajeBuena.Text = "0 %";
        }
        else
        {
            TextBox_InstalacionesPorcentajeBuena.Text = (((Decimal)PUNTUACION_INSTALACIONES_BUENA * 100) / (Decimal)totalInstalaciones).ToString("#.#") + " %";
        }

        if (totalTotal <= 0)
        {
            TextBox_TotalBenaPorcentaje.Text = "0 %";
            TextBox_TotalRegularPorcentaje.Text = "0 %";
            TextBox_TotalMalaPorcentaje.Text = "0 %";
        }
        else
        {
            TextBox_TotalBenaPorcentaje.Text = (((Decimal)totalBuena * 100) / (Decimal)totalTotal).ToString("#.#") + " %";
            TextBox_TotalRegularPorcentaje.Text = (((Decimal)totalRegular * 100) / (Decimal)totalTotal).ToString("#.#") + " %";
            TextBox_TotalMalaPorcentaje.Text = (((Decimal)totalMala * 100) / (Decimal)totalTotal).ToString("#.#") + " %";
        }


    }

    private void CargarLinkArchivo(Programa.TiposArchivo tipoArchivo, HyperLink link)
    {
        Decimal ID_DETALLE = Convert.ToDecimal(HiddenField_ID_DETALLE.Value);

        Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaArchivo = _programa.ObtenerArchivoAdjuntoPorTipoYDetalle(ID_DETALLE, tipoArchivo);

        if (tablaArchivo.Rows.Count <= 0)
        {
            if (_programa.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _programa.MensajeError, Proceso.Error);
            }

            Panel_ResultadosEncuestaLinkArchivo.Visible = false;
        }
        else
        {
            DataRow filaArchivo = tablaArchivo.Rows[0];

            tools _tools = new tools();
            SecureQueryString QueryStringSeguro;
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

            QueryStringSeguro["id_adjunto"] = filaArchivo["ID_ADJUNTO"].ToString().Trim();

            link.NavigateUrl = "~/maestros/visorAdjuntosActividades.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.Enabled = true;

            Panel_ResultadosEncuestaLinkArchivo.Visible = true;
        }

    }

    private void CargarActividadesSegunSubPrograma(DataTable tablaActividades, DropDownList drop)
    {
        drop.Items.Clear();
        drop.Items.Add(new ListItem("Seleccione...", ""));

        foreach (DataRow f in tablaActividades.Rows)
        {
            drop.Items.Add(new ListItem(f["NOMBRE_ACTIVIDAD"].ToString().Trim(), f["ID_ACTIVIDAD"].ToString().Trim()));
        }

        drop.DataBind();
    }

    private void CargarChulosDeAsistencia()
    {
        Decimal ID_DETALLE = Convert.ToDecimal(HiddenField_ID_DETALLE.Value);

        Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaEmpleadosAsistieron = _programa.ObtenerEmpleadosQueAsistieronAActividad(ID_DETALLE);

        Int32 contador = 0;

        if(tablaEmpleadosAsistieron.Rows.Count <= 0)
        {
            if (_programa.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _programa.MensajeError, Proceso.Advertencia);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "no se encontraron registros de asistencia para esta actividad.", Proceso.Advertencia);
            }
        }
        else
        {
            for(int i = 0; i < GridView_ControlAsistencia.Rows.Count; i++)
            {
                Decimal ID_EMPLEADO = Convert.ToDecimal(GridView_ControlAsistencia.DataKeys[i].Values["ID_EMPLEADO"]);
                String expresion = "ID_EMPLEADO = " + ID_EMPLEADO.ToString();
                DataRow[] rowsEncontradas;

                rowsEncontradas = tablaEmpleadosAsistieron.Select(expresion);

                if (rowsEncontradas.Length > 0)
                {
                    CheckBox check = GridView_ControlAsistencia.Rows[i].FindControl("CheckBox_Asistencia") as CheckBox;
                    check.Checked = true;

                    contador += 1;
                }
            }
        }

        Label_trabajadoresSeleciconados.Text = contador.ToString();
    }


    private void CargarEntidadesQueColaboraron()
    {
        Decimal ID_DETALLE = Convert.ToDecimal(HiddenField_ID_DETALLE.Value);

        Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaEntidadesColaboraron = _programa.ObtenerEntidadesQueCOlaboraronEnActividad(ID_DETALLE);

        if (tablaEntidadesColaboraron.Rows.Count <= 0)
        {
            if (_programa.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _programa.MensajeError, Proceso.Error);
            }

            Panel_EntidadesColaboradoras.Visible = false;
        }
        else
        {
            llenar_GridView_EntidadesColaboradoras_desde_tabla(tablaEntidadesColaboraron);

            inhabilitarFilasGrilla(GridView_EntidadesColaboradoras, 1);
            GridView_EntidadesColaboradoras.Columns[0].Visible = false;
        }
    }


    private void CargarCompromisosActividad()
    {
        Decimal ID_DETALLE = Convert.ToDecimal(HiddenField_ID_DETALLE.Value);
        String NOMBRE_DETALLE = DropDownList_IdActividad.SelectedValue;

        Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaCompromisos = _programa.ObtenerCompromisosDeActividad(ID_DETALLE, Programa.TiposGeneraCompromiso.ACTIVIDAD);

        if (tablaCompromisos.Rows.Count <= 0)
        {
            if (_programa.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _programa.MensajeError, Proceso.Error);
            }

            Panel_Compromisos.Visible = false;
        }
        else
        {
            llenar_GridView_Compromisos_desde_tabla(tablaCompromisos);

            inhabilitarFilasGrilla(GridView_Compromisos, 1);
            GridView_Compromisos.Columns[0].Visible = false;
        }
    }


    private void CargarImagenYConclusiones(DataRow filaActividad)
    {
        if (DBNull.Value.Equals(filaActividad["DIR_IMAGEN_REPRESENTATIVA"]) == false)
        {
            Image_Representativa.Visible = true;

            System.Drawing.Image imgPhoto = System.Drawing.Image.FromFile(MapPath(filaActividad["DIR_IMAGEN_REPRESENTATIVA"].ToString().Trim()));

            decimal ancho = imgPhoto.Size.Width;
            decimal alto = imgPhoto.Size.Height;

            if (ancho > 550)
            {

                alto = (alto * (((550 * 100) / ancho) / 100));
                ancho = 550;
            }

            if (alto > 550)
            {
                ancho = (ancho * (((550 * 100) / alto) / 100));
                alto = 550;
            }

            Image_Representativa.ImageUrl = filaActividad["DIR_IMAGEN_REPRESENTATIVA"].ToString().Trim();
            Image_Representativa.Width = new Unit(Convert.ToInt32(ancho));
            Image_Representativa.Height = new Unit(Convert.ToInt32(alto));
        }

        TextBox_ConclusionesActividad.Text = filaActividad["CONCLUSIONES"].ToString().Trim();
    }

    private void CargarHistorialAjustesAPresupuesto(Decimal ID_DETALLE)
    {
        Programa _prog = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaHistorialAjustes = _prog.ObtenerHistorialAjustesPresupuesto(ID_DETALLE);

        if (tablaHistorialAjustes.Rows.Count <= 0)
        {
            if (_prog.MensajeError != null)
            {
                Informar(Panel_MENSAJES, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _prog.MensajeError, Proceso.Error);
            }

            Panel_HistorialAjustesPresupuesto.Visible = false;
        }
        else
        {
            Panel_HistorialAjustesPresupuesto.Visible = true;

            Cargar_GridView_HistorialAjustesPresupuesto_DesdeDataTable(tablaHistorialAjustes);

            
        }
    }

    private void Cargar_GridView_HistorialAjustesPresupuesto_DesdeDataTable(DataTable tablaHistorialAjustes)
    {
        GridView_HistorialAjustesPresupuesto.DataSource = tablaHistorialAjustes;
        GridView_HistorialAjustesPresupuesto.DataBind();

        for (int i = 0; i < GridView_HistorialAjustesPresupuesto.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_HistorialAjustesPresupuesto.Rows[i];
            DataRow filaTabla = tablaHistorialAjustes.Rows[i];

            HyperLink link = filaGrilla.FindControl("HyperLink_DocumentoAdjunto") as HyperLink;

            link.Enabled = false;
            link.Visible = false;

            if (DBNull.Value.Equals(filaTabla["ARCHIVO"]) == false)
            {
                tools _tools = new tools();
                SecureQueryString QueryStringSeguro;
                QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

                QueryStringSeguro["ID_HIST_AJUSTE"] = filaTabla["ID_HIST_AJUSTE"].ToString();

                link.NavigateUrl = "~/maestros/visorDocumentoAjustePresupuesto.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
                link.Enabled = true;
                link.Visible = true;
            }
        }
    }


    private void llenar_GridView_AdjuntosInforme_desde_tabla(DataTable tablaDatos)
    {
        GridView_AdjuntosInforme.DataSource = tablaDatos;
        GridView_AdjuntosInforme.DataBind();

        GridViewRow filaGrilla;
        DataRow filaTabla;
        HyperLink link;

        for (int i = 0; i < tablaDatos.Rows.Count; i++)
        {
            filaGrilla = GridView_AdjuntosInforme.Rows[i];
            filaTabla = tablaDatos.Rows[i];

            link = filaGrilla.FindControl("HyperLink_ARCHIVO_ADJUNTO") as HyperLink;

            tools _tools = new tools();
            SecureQueryString QueryStringSeguro;
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
            QueryStringSeguro["id_adjunto"] = filaTabla["ID_ADJUNTO"].ToString().Trim();

            link.NavigateUrl = "~/maestros/visorAdjuntosActividades.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.Enabled = true;
            link.Target = "_blank";
            link.Text = "Ver Archivo";
        }
    }

    private void CargarAdjuntos()
    {
        Decimal ID_DETALLE = Convert.ToDecimal(HiddenField_ID_DETALLE.Value);

        Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaAdjuntos = _programa.ObtenerArchivoAdjuntoPorTipoYDetalle(ID_DETALLE, Programa.TiposArchivo.ADJUNTO);

        if (tablaAdjuntos.Rows.Count <= 0)
        {
            if (_programa.MensajeError != null)
            {
                Informar(Panel_MENSAJES, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _programa.MensajeError, Proceso.Error);
            }

            GridView_AdjuntosInforme.DataSource = null;
            GridView_AdjuntosInforme.DataBind();
        }
        else
        {
            llenar_GridView_AdjuntosInforme_desde_tabla(tablaAdjuntos);
        }
    }

    private void VisiblesSoloLosEmpleadosChueados()
    {
        foreach (GridViewRow filaGrilla in GridView_ControlAsistencia.Rows)
        {
            CheckBox checkSeleccionado = filaGrilla.FindControl("CheckBox_Asistencia") as CheckBox;
            filaGrilla.Visible = checkSeleccionado.Checked;
        }
    }

    private void CargarDatosActividad(DataTable tablaActividad)
    {
        Image_Representativa.Visible = false;

        DataRow filaActividad = tablaActividad.Rows[0];

        Label_INFO_ADICIONAL_MODULO.Text = filaActividad["RAZ_SOCIAL"].ToString() + "<br>NIT: " +  filaActividad["NIT_EMPRESA"].ToString();

        HiddenField_ID_ACTIVIDAD.Value = filaActividad["ID_ACTIVIDAD"].ToString().Trim();
        HiddenField_ID_DETALLE.Value = filaActividad["ID_DETALLE"].ToString();
        HiddenField_ID_DETALLE_GENERAL.Value = filaActividad["ID_DETALLE_GENERAL"].ToString();
        HiddenField_ID_PROGRAMA_GENERAL.Value = filaActividad["ID_PROGRAMA_GENERAL"].ToString();
        HiddenField_ID_EMPRESA.Value = filaActividad["ID_EMPRESA"].ToString();
        HiddenField_ID_DETALLE_GENERAL_PADRE.Value = filaActividad["ID_DETALLE_GENERAL_PADRE"].ToString();
        HiddenField_ID_PRESUPUESTO.Value = filaActividad["ID_PRESUPUESTO"].ToString();
        
        HiddenField_PRESUPUESTO.Value = Convert.ToDecimal(filaActividad["PRESUPUESTO"]).ToString();
        HiddenField_PRESUPUESTO_ASIGNADO_EMPRESA.Value = Convert.ToDecimal(filaActividad["ASIGNADO"]).ToString();
        HiddenField_PRESUPUESTO_EJECUTADO_EMPRESA.Value = Convert.ToDecimal(filaActividad["EJECUTADO"]).ToString();

        HiddenField_SECCIONES_HABILITADAS.Value = filaActividad["SECCIONES_HABILITADAS"].ToString().Trim();

        Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaSubPrograma = _programa.ObtenerProgramaAlQuePerteneceUnaActividadProgramada(Convert.ToDecimal(HiddenField_ID_DETALLE_GENERAL.Value));
        DataRow filaSubprograma = tablaSubPrograma.Rows[0];

        HiddenField_ID_SUB_PROGRAMA_PADRE.Value = filaSubprograma["ID_SUB_PROGRAMA"].ToString();

        Cargar(Listas.SubProgramas, DropDownList_SubPrograma);
        DropDownList_SubPrograma.SelectedValue = HiddenField_ID_SUB_PROGRAMA_PADRE.Value;

        DataTable tablaActividades = _programa.ObtenerActividadesPorDetalleGeneralPadre(Convert.ToDecimal(HiddenField_ID_DETALLE_GENERAL_PADRE.Value));
        CargarActividadesSegunSubPrograma(tablaActividades, DropDownList_IdActividad);

        DropDownList_IdActividad.SelectedValue = filaActividad["ID_ACTIVIDAD"].ToString().Trim();
        DropDownList_Tipo.SelectedValue = filaActividad["TIPO"].ToString().Trim();
        DropDownList_Sector.SelectedValue = filaActividad["SECTOR"].ToString().Trim();
        DropDownList_EstadoActividad.SelectedValue = filaActividad["ACTIVO"].ToString().Trim();

        TextBox_Resumen.Text = filaActividad["RESUMEN_ACTIVIDAD"].ToString().Trim();

        TextBox_FechaActividad.Text = Convert.ToDateTime(filaActividad["FECHA_ACTIVIDAD"]).ToShortDateString();

        TimePicker_HoraInicioActividad.SelectedTime = Convert.ToDateTime(filaActividad["HORA_INICIO"]);
        TimePicker_HoraFinActividad.SelectedTime = Convert.ToDateTime(filaActividad["HORA_FIN"]);

        TextBox_PresupuestoAsignado.Text = Convert.ToDecimal(filaActividad["PRESUPUESTO_APROBADO"]).ToString();

        RangeValidator_TextBox_PresupuestoAsignado.MinimumValue = "0";
        RangeValidator_TextBox_PresupuestoAsignado.MaximumValue = HiddenField_PRESUPUESTO.Value;

        TextBox_PersonalCitado.Text = Convert.ToInt32(filaActividad["PERSONAL_CITADO"]).ToString();

        RangeValidator_TextBox_PersonalCitado.Enabled = false;
        ValidatorCalloutExtender_TextBox_PersonalCitado_1.Enabled = false;
        Label_PersonalCitadoMaximo.Visible = false;

        Cargar(Listas.Encargados, DropDownList_Encargado);
        
        try
        {
            DropDownList_Encargado.SelectedValue = filaActividad["ENCARGADO"].ToString().Trim();
        }
        catch
        {
            DropDownList_Encargado.SelectedIndex = 0;
        }
        
        DataRow filaInfoCiudadActividad = obtenerDatosCiudadCliente(filaActividad["ID_CIUDAD"].ToString().Trim());
        if (filaInfoCiudadActividad != null)
        {
            DropDownList_REGIONAL.SelectedValue = filaInfoCiudadActividad["ID_REGIONAL"].ToString().Trim();
            cargar_DropDownList_DEPARTAMENTO(filaInfoCiudadActividad["ID_REGIONAL"].ToString().Trim());
            DropDownList_DEPARTAMENTO.SelectedValue = filaInfoCiudadActividad["ID_DEPARTAMENTO"].ToString().Trim();
            cargar_DropDownList_CIUDAD(filaInfoCiudadActividad["ID_REGIONAL"].ToString().Trim(), filaInfoCiudadActividad["ID_DEPARTAMENTO"].ToString().Trim());
            DropDownList_CIUDAD.SelectedValue = filaInfoCiudadActividad["ID_CIUDAD"].ToString().Trim();
        }
        else
        {
            inhabilitar_DropDownList_DEPARTAMENTO();
            inhabilitar_DropDownList_CIUDAD();
        }

        CargarBarraDeEstadoDeActividad(filaActividad["ID_ESTADO"].ToString().Trim());

        if (filaActividad["ID_ESTADO"].ToString().Trim() == Programa.EstadosDetalleActividad.CANCELADA.ToString())
        {
            Panel_MOTIVO_CANCELACION.Visible = true;
            TextBox_Motivocancelacion.Text = filaActividad["MOTIVO_CANCELACION"].ToString().Trim();
            try
            {
                DropDownList_MotivoCancelacion.SelectedValue = filaActividad["TIPO_CANCELACION"].ToString().Trim();
            }
            catch
            {
                DropDownList_MotivoCancelacion.SelectedIndex = 0;
            }

            Panel_LinkArchivoCancelacion.Visible = false;
            Panel_FileUploadArchivoCancelacion.Visible = false;
            if (DBNull.Value.Equals(filaActividad["ARCHIVO_CANCELACION"]) == true)
            {
            }
            else
            {
                Panel_LinkArchivoCancelacion.Visible = true;

                tools _tools = new tools();
                SecureQueryString QueryStringSeguro;
                QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

                QueryStringSeguro["id_detalle"] = HiddenField_ID_DETALLE.Value;

                HyperLink_ArchivoCancelacion.NavigateUrl = "~/maestros/visorArchivoCancelacion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
                HyperLink_ArchivoCancelacion.Enabled = true;
                HyperLink_ArchivoCancelacion.Text = "Ver Archivo.";
            }
        }
        else
        {
            if (filaActividad["ID_ESTADO"].ToString().Trim() == Programa.EstadosDetalleActividad.TERMINADA.ToString())
            {
                Mostrar(Acciones.ResultadosActividad);

                TextBox_PresupuestoFinal.Text = Convert.ToDecimal(filaActividad["PRESUPUESTO_FINAL"]).ToString();
                TextBox_PersonalFinal.Text = Convert.ToDecimal(filaActividad["PERSONAL_FINAL"]).ToString();

                if (HiddenField_SECCIONES_HABILITADAS.Value.Contains("Resultados Encuesta") == true)
                {
                    LlenarcamposDeEncuesta(filaActividad["RESULTADOS_ENCUESTA"].ToString().Trim());
                    CargarLinkArchivo(Programa.TiposArchivo.ENCUESTA, HyperLink_ArchivoEncuesta);
                }

                if (HiddenField_SECCIONES_HABILITADAS.Value.Contains("Control Asistencia") == true)
                {
                    CargarTrabajadoresEmpresa();

                    CargarChulosDeAsistencia();

                    VisiblesSoloLosEmpleadosChueados();

                    CheckBox_TodosEmpleados.Checked = false;
                    TextBox_FiltroEmpleados.Text = "";

                    CargarLinkArchivo(Programa.TiposArchivo.ASISTENCIA, HyperLink_ArchivoAsistencia);
                }

                if (HiddenField_SECCIONES_HABILITADAS.Value.Contains("Entidades Colaboradoras") == true)
                {
                    CargarEntidadesQueColaboraron();
                }

                if (HiddenField_SECCIONES_HABILITADAS.Value.Contains("Compromisos") == true)
                {
                    CargarCompromisosActividad();
                    CheckBox_TodosEmpleados.Enabled = false;
                }

                CargarImagenYConclusiones(filaActividad);

                Panel_AdjuntarArchivos.Visible = true;
                Panel_NuevoAdjunto.Visible = false;
                Button_NuevoAdjunto.Visible = true;
                Button_GuardarAdjunto.Visible = false;
                Button_CancelarAdjunto.Visible = false;

                CargarAdjuntos();
            }
        }

        CargarHistorialAjustesAPresupuesto(Convert.ToDecimal(filaActividad["ID_DETALLE"]));

        CargarHistorialReprogramaciones(Convert.ToDecimal(filaActividad["ID_DETALLE"]));


        ComprobraEstadoActividad(filaActividad["ID_ESTADO"].ToString().Trim());
    }

    private void CargarControlRegistro(DataRow filaActividad)
    {
        TextBox_USU_CRE.Text = filaActividad["USU_CRE"].ToString();
        try
        {
            TextBox_FCH_CRE.Text = DateTime.Parse(filaActividad["FCH_CRE"].ToString()).ToShortDateString();
            TextBox_HOR_CRE.Text = DateTime.Parse(filaActividad["FCH_CRE"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_CRE.Text = "";
            TextBox_HOR_CRE.Text = "";
        }
        TextBox_USU_MOD.Text = filaActividad["USU_MOD"].ToString();
        try
        {
            TextBox_FCH_MOD.Text = DateTime.Parse(filaActividad["FCH_MOD"].ToString()).ToShortDateString();
            TextBox_HOR_MOD.Text = DateTime.Parse(filaActividad["FCH_MOD"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_MOD.Text = "";
            TextBox_HOR_MOD.Text = "";
        }
    }

    private void CargarDatosActividad()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_DETALLE = Convert.ToDecimal(QueryStringSeguro["id_detalle"]);

        Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaActividad = _programa.ObtenerDetalleActividadesPorIdDetalle(ID_DETALLE);

        if (tablaActividad.Rows.Count <= 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información de la Actividad seleccionada.", Proceso.Advertencia);
        }
        else
        {
            CargarControlRegistro(tablaActividad.Rows[0]);
            CargarDatosActividad(tablaActividad);
        }
    }

    private void Cargar(Listas lista, DropDownList drop)
    {
        Programa.Areas AREA_PROGRAMA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);

        Programa _prog = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        ListItem item;

        drop.Items.Clear();

        switch (lista)
        {
            case Listas.SubProgramas:

                Decimal ID_PROGRAMA_GENERAL = Convert.ToDecimal(HiddenField_ID_PROGRAMA_GENERAL.Value);

                DataTable tablaProgramas = _prog.ObtenerSubProgramasDeUnProgramaGeneral(ID_PROGRAMA_GENERAL);

                drop.Items.Clear();
                drop.Items.Add(new ListItem("Seleccione...", ""));

                foreach (DataRow fila in tablaProgramas.Rows)
                {
                    Decimal ID_DETALLE_GENERAL = Convert.ToDecimal(fila["ID_DETALLE_GENERAL"]);
                    Decimal ID_SUB_PROGRAMA = Convert.ToDecimal(fila["ID_SUBPROGRAMA"]);

                    DataTable tablaActividadesPrograma = _prog.ObtenerActividadesPorDetalleGeneralPadre(ID_DETALLE_GENERAL);

                    if (tablaActividadesPrograma.Rows.Count > 0)
                    {
                        drop.Items.Add(new ListItem(fila["NOMBRE_SUB_PROGRAMA"].ToString().Trim(), ID_SUB_PROGRAMA.ToString()));
                    }
                }

                drop.DataBind();
                break;
            case Listas.EstadosActividades:
                drop.Items.Clear();
                parametro _parametroAC = new parametro(Session["idEmpresa"].ToString());
                DataTable tablaParametrosAC = _parametroAC.ObtenerParametrosPorTabla(tabla.PARAMETROS_ESTADO_ACTIVIDAD_RSE_GLOBAL);
                ListItem itemAC = new ListItem("Seleccione...", "");
                drop.Items.Add(itemAC);
                foreach (DataRow fila in tablaParametrosAC.Rows)
                {
                    itemAC = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
                    drop.Items.Add(itemAC);
                }
                drop.DataBind();
                break;
            case Listas.TiposActividad:
                drop.Items.Clear();
                TipoActividad _tipoActividad = new TipoActividad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                DataTable tablaParametrosTA = _tipoActividad.ObtenerTiposActividadPorAreayEstado(AREA_PROGRAMA, true);
                drop.Items.Add(new ListItem("Seleccione...", ""));

                foreach (DataRow fila in tablaParametrosTA.Rows)
                {
                    drop.Items.Add(new ListItem(fila["NOMBRE"].ToString(), fila["NOMBRE"].ToString()));
                }
                drop.DataBind();
                break;
            case Listas.SectoresActividad:
                drop.Items.Clear();
                parametro _parametroSA = new parametro(Session["idEmpresa"].ToString());
                DataTable tablaParametrosSA = _parametroSA.ObtenerParametrosPorTabla(tabla.PARAMETROS_SECTORES_ACTIVIDAD);
                drop.Items.Add(new ListItem("Seleccione...", ""));
                foreach (DataRow fila in tablaParametrosSA.Rows)
                {
                    drop.Items.Add(new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString()));
                }
                drop.DataBind();
                break;
            case Listas.Regionales:
                drop.Items.Clear();
                regional _regional = new regional(Session["idEmpresa"].ToString());
                DataTable tablaRegionales = _regional.ObtenerTodasLasRegionales();

                item = new ListItem("Seleccione...", "");
                drop.Items.Add(item);

                foreach (DataRow fila in tablaRegionales.Rows)
                {
                    item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_REGIONAL"].ToString());
                    drop.Items.Add(item);
                }

                drop.DataBind();
                break;
            case Listas.EntidadesCOlaboradoras:
                EntidadColaboradora _entidad = new EntidadColaboradora(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

                Programa.Areas AREA_ENTIDAD = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);

                DataTable tablaEntidades = _entidad.ObtenerTodasEntidadesPorAreaYEstado(AREA_ENTIDAD, true);

                drop.Items.Clear();
                drop.Items.Add(new ListItem("Seleccione...",""));

                foreach (DataRow fila in tablaEntidades.Rows)
                {
                    item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_ENTIDAD"].ToString());
                    drop.Items.Add(item);
                }

                drop.DataBind();

                break;
            case Listas.Encargados:
                DataTable tablaEncargados = _prog.ObtenerUsuariosSistemaActivos();

                drop.Items.Clear();

                drop.Items.Add(new ListItem("Seleccione...",""));

                foreach (DataRow fila in tablaEncargados.Rows)
                {
                    item = new ListItem(fila["NOMBRE_USUARIO"].ToString(), fila["USU_LOG"].ToString());
                    drop.Items.Add(item);
                }

                drop.DataBind();

                break;
            case Listas.MotivosCancelacion:
                MotivoProgComp _motivoCancelacion = new MotivoProgComp(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

                DataTable tablaMotivosCancelacion = _motivoCancelacion.ObtenerMotivosActProgCompPorAreaYTipo(AREA_PROGRAMA, "CANCELACION");

                drop.Items.Add(new ListItem("Seleccione...", ""));

                foreach (DataRow fila in tablaMotivosCancelacion.Rows)
                {
                    if (fila["ACTIVO"].ToString() == "True")
                    {
                        drop.Items.Add(new ListItem(fila["MOTIVO"].ToString(), fila["MOTIVO"].ToString()));
                    }
                }

                drop.DataBind();

                break;
            case Listas.MotivosReprogramacion:
                MotivoProgComp _motivoRepro = new MotivoProgComp(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

                DataTable tablaMotivosRepro = _motivoRepro.ObtenerMotivosActProgCompPorAreaYTipo(AREA_PROGRAMA, "REPROGRAMACION");

                drop.Items.Add(new ListItem("Seleccione...", ""));

                foreach (DataRow fila in tablaMotivosRepro.Rows)
                {
                    if (fila["ACTIVO"].ToString() == "True")
                    {
                        drop.Items.Add(new ListItem(fila["MOTIVO"].ToString(), fila["MOTIVO"].ToString()));
                    }
                }

                drop.DataBind();

                break;
            case Listas.ResponsablesCompromisos:
                DataTable tablaResponsables = _prog.ObtenerUsuariosSistemaActivos();
                
                drop.Items.Clear();

                drop.Items.Add(new ListItem("Seleccione...",""));
                drop.Items.Add(new ListItem("CLIENTE", "CLIENTE"));

                foreach (DataRow fila in tablaResponsables.Rows)
                {
                    item = new ListItem(fila["NOMBRE_USUARIO"].ToString(), fila["USU_LOG"].ToString());
                    drop.Items.Add(item);
                }

                drop.DataBind();
                break;
        }
    }

    private DataTable ObtenerTablaParaGrillaEmpleados()
    {
        DataTable tablaTemp = new DataTable();

        tablaTemp.Columns.Add("ID_EMPLEADO");
        tablaTemp.Columns.Add("ID_SOLICITUD");
        tablaTemp.Columns.Add("NOMBRES_EMPLEADO");
        tablaTemp.Columns.Add("NUMERO_IDENTIFICACION");
        tablaTemp.Columns.Add("CARGO");

        tablaTemp.AcceptChanges();

        return tablaTemp;
    }

    private void CargarTrabajadoresEmpresa()
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value); 
        
        registroContrato _registroContrato = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaEmpleados = _registroContrato.ObtenerEmpleadosActivosPorEmpresa(ID_EMPRESA);

        if (tablaEmpleados.Rows.Count <= 0)
        {
            if (_registroContrato.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _registroContrato.MensajeError, Proceso.Error);
            }

            GridView_ControlAsistencia.DataSource = null;
            GridView_ControlAsistencia.DataBind();
        }
        else
        { 
            DataTable tablaEmpleadosFinal = ObtenerTablaParaGrillaEmpleados();

            perfil _perfil = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            for (int i = 0; i < tablaEmpleados.Rows.Count; i++)
            {
                DataRow filaTablaFinal = tablaEmpleadosFinal.NewRow();
                DataRow filaTabla = tablaEmpleados.Rows[i];

                filaTablaFinal["ID_EMPLEADO"] = filaTabla["ID_EMPLEADO"];
                filaTablaFinal["ID_SOLICITUD"] = filaTabla["ID_SOLICITUD"];
                filaTablaFinal["NOMBRES_EMPLEADO"] = filaTabla["NOMBRES"].ToString().Trim() + " " + filaTabla["APELLIDOS"].ToString().Trim();
                filaTablaFinal["NUMERO_IDENTIFICACION"] = filaTabla["TIP_DOC_IDENTIDAD"].ToString().Trim() + " " + filaTabla["NUM_DOC_IDENTIDAD"].ToString().Trim();

                Decimal ID_PERFIL = 0;
                if (filaTabla["ID_PERFIL"].Equals(DBNull.Value) == false)
                {
                    ID_PERFIL = Convert.ToDecimal(filaTabla["ID_PERFIL"]);

                    DataTable tablaPerfil = _perfil.ObtenerPorRegistroConinfoOcupacion(ID_PERFIL);

                    if (tablaPerfil.Rows.Count <= 0)
                    {
                        if (_perfil.MensajeError != null)
                        {
                            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _perfil.MensajeError, Proceso.Error);
                        }

                        filaTablaFinal["CARGO"] = "Desconocido";
                    }
                    else
                    {
                        DataRow filaPerfil = tablaPerfil.Rows[0];

                        filaTablaFinal["CARGO"] = filaPerfil["NOM_OCUPACION"].ToString().Trim();
                    }
                }
                else
                {
                    filaTablaFinal["CARGO"] = "Desconocido";
                }

                tablaEmpleadosFinal.Rows.Add(filaTablaFinal);
                tablaEmpleadosFinal.AcceptChanges();
            }

            GridView_ControlAsistencia.DataSource = tablaEmpleadosFinal;
            GridView_ControlAsistencia.DataBind();
        }

        Label_trabajadoresSeleciconados.Text = "0";
    }

    private void ConfigurarDatoasParaAjusteDePresupuesto()
    {
        Decimal presupuesto = Convert.ToDecimal(HiddenField_PRESUPUESTO.Value);
        Decimal asignado = Convert.ToDecimal(HiddenField_PRESUPUESTO_ASIGNADO_EMPRESA.Value);
        Decimal ejecutado = Convert.ToDecimal(HiddenField_PRESUPUESTO_EJECUTADO_EMPRESA.Value);

        Decimal presupuestoActividad = Convert.ToDecimal(TextBox_PresupuestoAsignado.Text);

        asignado = asignado - presupuestoActividad;
        Label_MaxPresupuestoAjsutar.Text = String.Format("{0:C}",(presupuesto - (asignado + ejecutado)));

        TextBox_PresupuestoAjustado.Text = TextBox_PresupuestoAsignado.Text;
    }

    private void CargarValidadorPersonalCitado(Label labelControl, RangeValidator validadorRango, ValidatorCalloutExtender validadorCallout)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaDatos = _cliente.ObtenerNumEmpleadosActivosPorIdEmpresa(ID_EMPRESA, "S", "S");

        if (tablaDatos.Rows.Count <= 0)
        {
            if (_cliente.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se pudo determinar el personal activo actual de la empresa, no se realizará el control de personal.", Proceso.Advertencia);
            }

            labelControl.Visible = false;
            validadorRango.Enabled = false;
            validadorCallout.Enabled = false;

        }
        else
        {
            Int32 contadorPersonalActivo = Convert.ToInt32(tablaDatos.Rows[0]["NUM_EMPLEADOS"]);

            if (contadorPersonalActivo <= 0)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La empresa no tiene personal activo, no se realizará el control de personal.", Proceso.Advertencia);

                labelControl.Visible = false;
                validadorRango.Enabled = false;
                validadorCallout.Enabled = false;
            }
            else
            {
                labelControl.Visible = true;
                labelControl.Text = contadorPersonalActivo.ToString();
                validadorRango.Enabled = true;
                validadorCallout.Enabled = true;

                validadorRango.MinimumValue = "1";
                validadorRango.MaximumValue = contadorPersonalActivo.ToString();
            }
        }
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                ConfigurarAreaRseGlobal();

                Cargar(Listas.SectoresActividad, DropDownList_Sector);
                Cargar(Listas.TiposActividad, DropDownList_Tipo);
                Cargar(Listas.EstadosActividades, DropDownList_EstadoActividad);
                Cargar(Listas.Regionales, DropDownList_REGIONAL);
                Cargar(Listas.MotivosCancelacion, DropDownList_MotivoCancelacion);
                CargarDatosActividad();
                break;
            case Acciones.Terminar:
                RangeValidator_TextBox_PresupuestoFinal.MinimumValue = "0";
                RangeValidator_TextBox_PresupuestoFinal.MaximumValue = HiddenField_PRESUPUESTO.Value;

                if (HiddenField_SECCIONES_HABILITADAS.Value.Contains("Control Asistencia") == true)
                {
                    CargarTrabajadoresEmpresa();
                    HiddenField_LETRA_PAGINACION_LISTA.Value = "A";
                    CargarGrillaConLetraSeleccionada();
                }

                CargarValidadorPersonalCitado(Label_PersonalAsistioMaximo, RangeValidator_TextBox_PersonalFinal, ValidatorCalloutExtender_TextBox_PersonalFinal_1);
                break;
            case Acciones.AjustarPresupuesto:
                ConfigurarDatoasParaAjusteDePresupuesto();
                break;
            case Acciones.Cancelar:
                Cargar(Listas.MotivosCancelacion, DropDownList_MotivoCancelacion);
                TextBox_Motivocancelacion.Text = "";
                break;
            case Acciones.Reprogramar:
                Cargar(Listas.MotivosReprogramacion, DropDownList_TipoReprogramacion);
                TextBox_MotivoReprogramacion.Text = "";
                break;
            case Acciones.Modificar:
                CargarValidadorPersonalCitado(Label_PersonalCitadoMaximo, RangeValidator_TextBox_PersonalCitado, ValidatorCalloutExtender_TextBox_PersonalCitado_1);
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

        if (IsPostBack == false)
        {
            Iniciar();
        }
    }

    private void Actualizar()
    {
        Decimal ID_ACTIVIDAD = Convert.ToDecimal(HiddenField_ID_ACTIVIDAD.Value);
        Decimal ID_DETALLE = Convert.ToDecimal(HiddenField_ID_DETALLE.Value);

        String RESUMEN_ACTIVIDAD = TextBox_Resumen.Text.Trim();

        Decimal PRESUPUESTO_ASIGNADO = Convert.ToDecimal(TextBox_PresupuestoAsignado.Text);
        Int32 PERSONAL_CITADO = Convert.ToInt32(TextBox_PersonalCitado.Text);
        String ENCARGADO = DropDownList_Encargado.SelectedValue;

        String ID_CIUDAD = DropDownList_CIUDAD.SelectedValue;

        Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean verificado = _programa.ActualizarDetalleActividad(ID_DETALLE, RESUMEN_ACTIVIDAD, PRESUPUESTO_ASIGNADO, PERSONAL_CITADO, ENCARGADO, ID_CIUDAD);

        if (verificado == true)
        {
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La Actividad fue correctamente actualizada.", Proceso.Correcto);
        }
        else
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _programa.MensajeError, Proceso.Error);
        }
    }


    private void ActualizarFinal()
    {
        tools _tools = new tools();

        Decimal ID_ACTIVIDAD = Convert.ToDecimal(HiddenField_ID_ACTIVIDAD.Value);
        Decimal ID_DETALLE = Convert.ToDecimal(HiddenField_ID_DETALLE.Value);

        String CONCLUSIONES = TextBox_ConclusionesActividad.Text.Trim();

        String DIRECCION_IMAGEN_REPRESENTATIVA = null;

        if (FileUpload_ImagenRepresentativa.HasFile == true)
        {
            String[] extensionesImagenPermitidas = ConfigurationManager.AppSettings["extensionesImagenesPermitidas"].ToLower().Split(',');
            String ext = System.IO.Path.GetExtension(FileUpload_ImagenRepresentativa.PostedFile.FileName).ToLower();
            Boolean isValidFile = false;
            for (int i = 0; i < extensionesImagenPermitidas.Length; i++)
            {
                if (ext == "." + extensionesImagenPermitidas[i])
                {
                    isValidFile = true;
                    break;
                }
            }

            if (isValidFile == true)
            {
                Guid id = Guid.NewGuid();

                String nombreUnicoParaImagen = id.ToString();

                String filePath = "~/imagenes/imgRepresentativasActividad/" + nombreUnicoParaImagen + ext;

                FileUpload_ImagenRepresentativa.SaveAs(MapPath(filePath));

                DIRECCION_IMAGEN_REPRESENTATIVA = filePath;

                FileUpload_ImagenRepresentativa.Dispose();
            }
        }

        Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean verificado = _programa.ActualizacionFinalDetalleActividad(ID_DETALLE, CONCLUSIONES, DIRECCION_IMAGEN_REPRESENTATIVA);

        if (verificado == true)
        {
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La Actividad fue correctamente actualizada.", Proceso.Correcto);
        }
        else
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _programa.MensajeError, Proceso.Error);
        }
    }

    private void Reprogramar()
    {
        tools _tools = new tools();
        Boolean correcto = true;

        Decimal ID_ACTIVIDAD = Convert.ToDecimal(HiddenField_ID_ACTIVIDAD.Value);
        Decimal ID_DETALLE = Convert.ToDecimal(HiddenField_ID_DETALLE.Value);

        DateTime FECHA_ACTIVIDAD = Convert.ToDateTime(TextBox_FechaActividad.Text);

        String HORA_INICIO = TimePicker_HoraInicioActividad.SelectedTime.ToShortTimeString();
        String HORA_FIN = TimePicker_HoraFinActividad.SelectedTime.ToShortTimeString();

        String TIPO_REPROGRAMACION = DropDownList_TipoReprogramacion.SelectedValue;
        String MOTIVO_REPROGRAMACION = TextBox_MotivoReprogramacion.Text.Trim();

        Byte[] ARCHIVO = null;
        Int32 ARCHIVO_TAMANO = 0;
        String ARCHIVO_EXTENSION = null;
        String ARCHIVO_TYPE = null;
        if (FileUpload_ArchivoReprogramacion.HasFile == true)
        {
            using (BinaryReader reader = new BinaryReader(FileUpload_ArchivoReprogramacion.PostedFile.InputStream))
            {
                ARCHIVO = reader.ReadBytes(FileUpload_ArchivoReprogramacion.PostedFile.ContentLength);
                ARCHIVO_TAMANO = FileUpload_ArchivoReprogramacion.PostedFile.ContentLength;
                ARCHIVO_TYPE = FileUpload_ArchivoReprogramacion.PostedFile.ContentType;
                ARCHIVO_EXTENSION = _tools.obtenerExtensionArchivo(FileUpload_ArchivoReprogramacion.PostedFile.FileName);
            }
        }
        else
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Es necesario adjuntar una archivo que soporte la reprogramación.", Proceso.Advertencia);
            correcto = false;
        }

        if (correcto == true)
        {
            Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            Boolean verificado = _programa.ReprogramarDetalleActividad(ID_DETALLE, FECHA_ACTIVIDAD, HORA_INICIO, HORA_FIN, MOTIVO_REPROGRAMACION, TIPO_REPROGRAMACION, ARCHIVO, ARCHIVO_EXTENSION, ARCHIVO_TAMANO, ARCHIVO_TYPE);

            if (verificado == true)
            {
                Ocultar(Acciones.Inicio);
                Desactivar(Acciones.Inicio);
                Mostrar(Acciones.Inicio);
                Cargar(Acciones.Inicio);

                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La Actividad fue Reprogramada Correctamente.", Proceso.Correcto);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _programa.MensajeError, Proceso.Error);
            }   
        }
    }

    private void Aprobar()
    {
        Decimal ID_ACTIVIDAD = Convert.ToDecimal(HiddenField_ID_ACTIVIDAD.Value);
        Decimal ID_DETALLE = Convert.ToDecimal(HiddenField_ID_DETALLE.Value);

        Decimal PRESUPUESTO_APROBADO = Convert.ToDecimal(TextBox_PresupuestoAsignado.Text);

        Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean verificado = _programa.AprobarDetalleActividad(ID_DETALLE, PRESUPUESTO_APROBADO, Programa.EstadosDetalleActividad.APROBADA);

        if (verificado == true)
        {
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La Actividad fue Aprobada Correctamente.", Proceso.Correcto);
        }
        else
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _programa.MensajeError, Proceso.Error);
        }
    }


    private void CancelarActividad()
    {
        tools _tools = new tools();
        Boolean correcto = true;

 
        Decimal ID_ACTIVIDAD = Convert.ToDecimal(HiddenField_ID_ACTIVIDAD.Value);
        Decimal ID_DETALLE = Convert.ToDecimal(HiddenField_ID_DETALLE.Value);

        String TIPO_CANCELACION = DropDownList_MotivoCancelacion.SelectedValue;
        String MOTIVO_CANCELACION = TextBox_Motivocancelacion.Text.Trim();

        Byte[] ARCHIVO = null;
        Int32 ARCHIVO_TAMANO = 0;
        String ARCHIVO_EXTENSION = null;
        String ARCHIVO_TYPE = null;
        if (FileUpload_ArchivoCancelacion.HasFile == true)
        {
            using (BinaryReader reader = new BinaryReader(FileUpload_ArchivoCancelacion.PostedFile.InputStream))
            {
                ARCHIVO = reader.ReadBytes(FileUpload_ArchivoCancelacion.PostedFile.ContentLength);
                ARCHIVO_TAMANO = FileUpload_ArchivoCancelacion.PostedFile.ContentLength;
                ARCHIVO_TYPE = FileUpload_ArchivoCancelacion.PostedFile.ContentType;
                ARCHIVO_EXTENSION = _tools.obtenerExtensionArchivo(FileUpload_ArchivoCancelacion.PostedFile.FileName);
            }
        }
        else
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Es necesario adjuntar una archivo que soporte la cancelación de la actividad.", Proceso.Advertencia);
            correcto = false;
        }

        if (correcto == true)
        {
            Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            Boolean verificado = _programa.CancelarDetalleActividad(ID_DETALLE, Programa.EstadosDetalleActividad.CANCELADA, MOTIVO_CANCELACION, TIPO_CANCELACION, ARCHIVO, ARCHIVO_EXTENSION, ARCHIVO_TAMANO, ARCHIVO_TYPE);

            if (verificado == true)
            {
                Ocultar(Acciones.Inicio);
                Desactivar(Acciones.Inicio);
                Mostrar(Acciones.Inicio);
                Cargar(Acciones.Inicio);

                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La Actividad fue Cancelada Correctamente.", Proceso.Correcto);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _programa.MensajeError, Proceso.Error);
            }
        }
    }

    private Boolean VerificarEstandarEncuesta(Int32 p_log_buena, Int32 p_log_regular, Int32 p_log_mala, Int32 p_instructor_buena, Int32 p_instructor_regular, Int32 p_instructor_mala, Int32 p_instalaciones_buena, Int32 p_instalaciones_regular, Int32 p_instalaciones_mala)
    {
        if ((p_log_buena + p_log_regular + p_log_mala) <= 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe incluir datos en la PUNTUACIÓN DE (LOGISTICA).", Proceso.Advertencia);
            return false;
        }
        else
        {
            if ((p_instructor_buena + p_instructor_regular + p_instructor_mala) <= 0)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe incluir datos en la PUNTUACIÓN DE (INSTRUCTOR).", Proceso.Advertencia);
                return false;
            }
            else
            {
                if ((p_instalaciones_buena + p_instalaciones_regular + p_instalaciones_mala) <= 0)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe incluir datos en la PUNTUACIÓN DE (INSTALACIONES).", Proceso.Advertencia);
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }

    private void ResultadosActividad()
    {
        Programa.Areas AREA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);

        tools _tools = new tools();

        Decimal ID_ACTIVIDAD = Convert.ToDecimal(HiddenField_ID_ACTIVIDAD.Value);
        Decimal ID_DETALLE = Convert.ToDecimal(HiddenField_ID_DETALLE.Value);

        Decimal PRESUPUESTO_FINAL = Convert.ToDecimal(TextBox_PresupuestoFinal.Text);
        Int32 PERSONAL_FINAL = Convert.ToInt32(TextBox_PersonalFinal.Text);

        Int32 PUNTUACION_LOG_BUENA = 0;
        Int32 PUNTUACION_LOG_REGULAR = 0;
        Int32 PUNTUACION_LOG_MALA = 0;

        Int32 PUNTUACION_INSTRUCTOR_BUENA = 0;
        Int32 PUNTUACION_INSTRUCTOR_REGULAR = 0;
        Int32 PUNTUACION_INSTRUCTOR_MALA = 0;

        Int32 PUNTUACION_INSTALACIONES_BUENA = 0;
        Int32 PUNTUACION_INSTALACIONES_REGULAR = 0;
        Int32 PUNTUACION_INSTALACIONES_MALA = 0;

        try { PUNTUACION_LOG_BUENA = Convert.ToInt32(TextBox_LogisticaBuena.Text); }
        catch { PUNTUACION_LOG_BUENA = 0; }

        try { PUNTUACION_LOG_REGULAR = Convert.ToInt32(TextBox_LogisticaRegular.Text); }
        catch { PUNTUACION_LOG_REGULAR = 0; }

        try { PUNTUACION_LOG_MALA = Convert.ToInt32(TextBox_LogisticaMala.Text); }
        catch { PUNTUACION_LOG_MALA = 0; }

        try { PUNTUACION_INSTRUCTOR_BUENA = Convert.ToInt32(TextBox_InstructorBuena.Text); }
        catch { PUNTUACION_INSTRUCTOR_BUENA = 0; }

        try { PUNTUACION_INSTRUCTOR_REGULAR = Convert.ToInt32(TextBox_InstructorRegular.Text); }
        catch { PUNTUACION_INSTRUCTOR_REGULAR = 0; }

        try { PUNTUACION_INSTRUCTOR_MALA = Convert.ToInt32(TextBox_InstructorMala.Text); }
        catch { PUNTUACION_INSTRUCTOR_MALA = 0; }

        try { PUNTUACION_INSTALACIONES_BUENA = Convert.ToInt32(TextBox_InstalacionesBuena.Text); }
        catch { PUNTUACION_INSTALACIONES_BUENA = 0; }

        try { PUNTUACION_INSTALACIONES_REGULAR = Convert.ToInt32(TextBox_InstalacionesRegular.Text); }
        catch { PUNTUACION_INSTALACIONES_REGULAR = 0; }

        try { PUNTUACION_INSTALACIONES_MALA = Convert.ToInt32(TextBox_InstalacionesMala.Text); }
        catch { PUNTUACION_INSTALACIONES_MALA = 0; }

        Int32 TotalLogistica = PUNTUACION_LOG_BUENA + PUNTUACION_LOG_MALA + PUNTUACION_LOG_REGULAR;
        Int32 TotalInstructor = PUNTUACION_INSTRUCTOR_BUENA + PUNTUACION_INSTRUCTOR_MALA + PUNTUACION_INSTRUCTOR_REGULAR;
        Int32 TotalInstalaciones = PUNTUACION_INSTALACIONES_BUENA + PUNTUACION_INSTALACIONES_MALA + PUNTUACION_INSTALACIONES_REGULAR;

        if (TotalLogistica > PERSONAL_FINAL)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El número de encuestas totales de LOGISTICA (" + TotalLogistica.ToString() + ") no puede superar el número de participantes (" + PERSONAL_FINAL.ToString() + ") de la encuesta", Proceso.Advertencia);
        }
        else
        {
            if (TotalInstructor > PERSONAL_FINAL)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El número de encuestas totales de INSTRUCTOR (" + TotalInstructor.ToString() + ") no puede superar el número de participantes (" + PERSONAL_FINAL.ToString() + ") de la encuesta", Proceso.Advertencia);
            }
            else
            {
                if (TotalInstalaciones > PERSONAL_FINAL)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El número de encuestas totales de INSTALACIONES (" + TotalInstalaciones.ToString() + ") no puede superar el número de participantes (" + PERSONAL_FINAL.ToString() + ") de la encuesta", Proceso.Advertencia);
                }
                else
                {
                    Byte[] ARCHIVO_ENCUESTA = null;
                    Int32 ARCHIVO_ENCUESTA_TAMANO = 0;
                    String ARCHIVO_ENCUESTA_EXTENSION = null;
                    String ARCHIVO_ENCUESTA_TYPE = null;
                    if (FileUpload_ArchivoEncuesta.HasFile == true)
                    {
                        using (BinaryReader reader = new BinaryReader(FileUpload_ArchivoEncuesta.PostedFile.InputStream))
                        {
                            ARCHIVO_ENCUESTA = reader.ReadBytes(FileUpload_ArchivoEncuesta.PostedFile.ContentLength);
                            ARCHIVO_ENCUESTA_TAMANO = FileUpload_ArchivoEncuesta.PostedFile.ContentLength;
                            ARCHIVO_ENCUESTA_TYPE = FileUpload_ArchivoEncuesta.PostedFile.ContentType;
                            ARCHIVO_ENCUESTA_EXTENSION = _tools.obtenerExtensionArchivo(FileUpload_ArchivoEncuesta.PostedFile.FileName);
                        }
                    }

                    List<Asistencia> listaAsistencia = new List<Asistencia>();

                    for (int i = 0; i < GridView_ControlAsistencia.Rows.Count; i++)
                    {
                        CheckBox check = GridView_ControlAsistencia.Rows[i].FindControl("CheckBox_Asistencia") as CheckBox;


                        if (check.Checked == true)
                        {
                            Decimal ID_EMPLEADO = Convert.ToDecimal(GridView_ControlAsistencia.DataKeys[i].Values["ID_EMPLEADO"]);
                            Decimal ID_SOLICITUD = Convert.ToDecimal(GridView_ControlAsistencia.DataKeys[i].Values["ID_SOLICITUD"]);

                            Asistencia _asistenciaParaLista = new Asistencia();

                            _asistenciaParaLista.ID_EMPLEADO = ID_EMPLEADO;
                            _asistenciaParaLista.ID_SOLICITUD = ID_SOLICITUD;
                            _asistenciaParaLista.ID_DETALLE = ID_DETALLE;

                            listaAsistencia.Add(_asistenciaParaLista);
                        }
                    }

                    if ((listaAsistencia.Count <= 0) && (HiddenField_SECCIONES_HABILITADAS.Value.Contains("Control Asistencia") == true))
                    {
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Para porder continuar debe diligenciar la asistencia de los colaboradores.", Proceso.Advertencia);
                    }
                    else
                    {
                        Byte[] ARCHIVO_ASISTENCIA = null;
                        Int32 ARCHIVO_ASISTENCIA_TAMANO = 0;
                        String ARCHIVO_ASISTENCIA_EXTENSION = null;
                        String ARCHIVO_ASISTENCIA_TYPE = null;
                        if (FileUpload_ArchivoAsistencia.HasFile == true)
                        {
                            using (BinaryReader reader = new BinaryReader(FileUpload_ArchivoAsistencia.PostedFile.InputStream))
                            {
                                ARCHIVO_ASISTENCIA = reader.ReadBytes(FileUpload_ArchivoAsistencia.PostedFile.ContentLength);
                                ARCHIVO_ASISTENCIA_TAMANO = FileUpload_ArchivoAsistencia.PostedFile.ContentLength;
                                ARCHIVO_ASISTENCIA_TYPE = FileUpload_ArchivoAsistencia.PostedFile.ContentType;
                                ARCHIVO_ASISTENCIA_EXTENSION = _tools.obtenerExtensionArchivo(FileUpload_ArchivoAsistencia.PostedFile.FileName);
                            }
                        }

                        String RESULTADOS_ENCUESTA = "";

                        RESULTADOS_ENCUESTA = Programa.TemasEncuesta.LOGISTICA.ToString() + ":" + Programa.CalificacionesTemasEncuesta.BUENA.ToString() + "=" + PUNTUACION_LOG_BUENA.ToString();
                        RESULTADOS_ENCUESTA += "*" + Programa.TemasEncuesta.LOGISTICA.ToString() + ":" + Programa.CalificacionesTemasEncuesta.REGULAR.ToString() + "=" + PUNTUACION_LOG_REGULAR.ToString();
                        RESULTADOS_ENCUESTA += "*" + Programa.TemasEncuesta.LOGISTICA.ToString() + ":" + Programa.CalificacionesTemasEncuesta.MALA.ToString() + "=" + PUNTUACION_LOG_MALA.ToString();

                        RESULTADOS_ENCUESTA += "*" + Programa.TemasEncuesta.INSTRUCTOR.ToString() + ":" + Programa.CalificacionesTemasEncuesta.BUENA.ToString() + "=" + PUNTUACION_INSTRUCTOR_BUENA.ToString();
                        RESULTADOS_ENCUESTA += "*" + Programa.TemasEncuesta.INSTRUCTOR.ToString() + ":" + Programa.CalificacionesTemasEncuesta.REGULAR.ToString() + "=" + PUNTUACION_INSTRUCTOR_REGULAR.ToString();
                        RESULTADOS_ENCUESTA += "*" + Programa.TemasEncuesta.INSTRUCTOR.ToString() + ":" + Programa.CalificacionesTemasEncuesta.MALA.ToString() + "=" + PUNTUACION_INSTRUCTOR_MALA.ToString();

                        RESULTADOS_ENCUESTA += "*" + Programa.TemasEncuesta.INSTALACIONES.ToString() + ":" + Programa.CalificacionesTemasEncuesta.BUENA.ToString() + "=" + PUNTUACION_INSTALACIONES_BUENA.ToString();
                        RESULTADOS_ENCUESTA += "*" + Programa.TemasEncuesta.INSTALACIONES.ToString() + ":" + Programa.CalificacionesTemasEncuesta.REGULAR.ToString() + "=" + PUNTUACION_INSTALACIONES_REGULAR.ToString();
                        RESULTADOS_ENCUESTA += "*" + Programa.TemasEncuesta.INSTALACIONES.ToString() + ":" + Programa.CalificacionesTemasEncuesta.MALA.ToString() + "=" + PUNTUACION_INSTALACIONES_MALA.ToString();

                        Boolean correcto = true;

                        if (HiddenField_SECCIONES_HABILITADAS.Value.Contains("Resultados Encuesta") == true)
                        {
                            correcto = VerificarEstandarEncuesta(PUNTUACION_LOG_BUENA, PUNTUACION_LOG_REGULAR, PUNTUACION_LOG_MALA, PUNTUACION_INSTRUCTOR_BUENA, PUNTUACION_INSTRUCTOR_REGULAR, PUNTUACION_INSTRUCTOR_MALA, PUNTUACION_INSTALACIONES_BUENA, PUNTUACION_INSTALACIONES_REGULAR, PUNTUACION_INSTALACIONES_MALA);
                        }

                        if (correcto == true)
                        {
                            List<EntidadColaboradora> listaEntidadesColaboradoras = new List<EntidadColaboradora>();
                            for (int i = 0; i < GridView_EntidadesColaboradoras.Rows.Count; i++)
                            {
                                EntidadColaboradora entidadParaLista = new EntidadColaboradora();
                                
                                DropDownList dropGrilla = GridView_EntidadesColaboradoras.Rows[i].FindControl("DropDownList_EntidadesColoboradoras") as DropDownList;
                                
                                TextBox texto = GridView_EntidadesColaboradoras.Rows[i].FindControl("TextBox_DescripcionEntidad") as TextBox;

                                entidadParaLista.DESCRIPCION = texto.Text.Trim();
                                entidadParaLista.ID_ENTIDAD = Convert.ToDecimal(dropGrilla.SelectedValue);
                                entidadParaLista.ID_ENTIDAD_COLABORA = Convert.ToDecimal(GridView_EntidadesColaboradoras.DataKeys[i].Values["ID_ENTIDAD_COLABORA"]);

                                listaEntidadesColaboradoras.Add(entidadParaLista);
                            }

                            List<MaestraCompromiso> listaCompromisos = new List<MaestraCompromiso>();
                            for (int i = 0; i < GridView_Compromisos.Rows.Count; i++)
                            {
                                GridViewRow filaGrilla = GridView_Compromisos.Rows[i];

                                MaestraCompromiso compromisoParaLista = new MaestraCompromiso();

                                Decimal ID_MAESTRA_COMPROMISO = Convert.ToDecimal(GridView_Compromisos.DataKeys[i].Values["ID_MAESTRA_COMPROMISO"]);

                                Decimal ID_ACTIVIDAD_GENERA = Convert.ToDecimal(HiddenField_ID_DETALLE.Value);

                                String NOMBRE_ACTIVIDAD_GENERA = DropDownList_IdActividad.SelectedItem.Text;

                                String TIPO_GENERA = Programa.TiposGeneraCompromiso.ACTIVIDAD.ToString();

                                TextBox textoCompromiso = filaGrilla.FindControl("TextBox_Compromiso") as TextBox;
                                String COMPROMISO = textoCompromiso.Text.Trim();

                                DropDownList dropGrilla = filaGrilla.FindControl("DropDownList_ResponsableCompromiso") as DropDownList;
                                String USU_LOG_RESPONSABLE = dropGrilla.SelectedValue;

                                TextBox textoFechaP = filaGrilla.FindControl("TextBox_FechaCompromiso") as TextBox;
                                DateTime FECHA_P = new DateTime();
                                try
                                {
                                    FECHA_P = Convert.ToDateTime(textoFechaP.Text);
                                }
                                catch
                                {
                                    FECHA_P = new DateTime();
                                }

                                DateTime FECHA_EJECUCION = new DateTime();

                                TextBox textoObservaciones = filaGrilla.FindControl("TextBox_ObservacionesCompromiso") as TextBox;
                                String OBSERVACIONES = textoObservaciones.Text.Trim();

                                Label textoEstado = filaGrilla.FindControl("Label_EstadoCompromiso") as Label;
                                String ESTADO = "CERRADO";
                                if (USU_LOG_RESPONSABLE.ToUpper() != "CLIENTE")
                                {
                                    ESTADO = textoEstado.Text;
                                }

                                compromisoParaLista.COMPROMISO = COMPROMISO;
                                compromisoParaLista.ESTADO = ESTADO;
                                compromisoParaLista.FECHA_EJECUCION = FECHA_EJECUCION;
                                compromisoParaLista.FECHA_P = FECHA_P;
                                compromisoParaLista.ID_ACTIVIDAD_GENERA = ID_ACTIVIDAD_GENERA;
                                compromisoParaLista.ID_MAESTRA_COMPROMISO = ID_MAESTRA_COMPROMISO;
                                compromisoParaLista.NOMBRE_ACTIVIDAD_GENERA = NOMBRE_ACTIVIDAD_GENERA;
                                compromisoParaLista.OBSERVACIONES = OBSERVACIONES;
                                compromisoParaLista.USU_LOG_RESPONSABLE = USU_LOG_RESPONSABLE;
                                compromisoParaLista.TIPO_GENERA = TIPO_GENERA;

                                listaCompromisos.Add(compromisoParaLista);
                            }

                            String CONCLUSION_ACTIVIDAD = TextBox_ConclusionesActividad.Text.Trim();
                            String DIRECCION_IMAGEN_REPRESENTATIVA = null;
                            if (FileUpload_ImagenRepresentativa.HasFile == false)
                            {
                                DIRECCION_IMAGEN_REPRESENTATIVA = null;
                            }
                            else
                            {
                                String[] extensionesImagenPermitidas = ConfigurationManager.AppSettings["extensionesImagenesPermitidas"].ToLower().Split(',');
                                String ext = System.IO.Path.GetExtension(FileUpload_ImagenRepresentativa.PostedFile.FileName).ToLower();
                                Boolean isValidFile = false;
                                for (int i = 0; i < extensionesImagenPermitidas.Length; i++)
                                {
                                    if (ext == "." + extensionesImagenPermitidas[i])
                                    {
                                        isValidFile = true;
                                        break;
                                    }
                                }

                                if (isValidFile == true)
                                {
                                    Guid id = Guid.NewGuid();

                                    String nombreUnicoParaImagen = id.ToString();
                                  
                                    String filePath = "~/imagenes/imgRepresentativasActividad/" + nombreUnicoParaImagen + ext;

                                    FileUpload_ImagenRepresentativa.SaveAs(MapPath(filePath));

                                    DIRECCION_IMAGEN_REPRESENTATIVA = filePath;

                                    FileUpload_ImagenRepresentativa.Dispose();
                                }
                            }
                          
                            Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

                            Boolean verificado = _programa.TerminarProgDetallesActividad(ID_DETALLE, PRESUPUESTO_FINAL, PERSONAL_FINAL, Programa.EstadosDetalleActividad.TERMINADA, RESULTADOS_ENCUESTA, listaAsistencia, ARCHIVO_ENCUESTA, ARCHIVO_ENCUESTA_TAMANO, ARCHIVO_ENCUESTA_EXTENSION, ARCHIVO_ENCUESTA_TYPE, ARCHIVO_ASISTENCIA, ARCHIVO_ASISTENCIA_TAMANO, ARCHIVO_ASISTENCIA_EXTENSION, ARCHIVO_ASISTENCIA_TYPE, listaEntidadesColaboradoras, CONCLUSION_ACTIVIDAD, DIRECCION_IMAGEN_REPRESENTATIVA, listaCompromisos, AREA);

                            if (verificado == false)
                            {
                                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _programa.MensajeError, Proceso.Advertencia);
                            }
                            else
                            {
                                Ocultar(Acciones.Inicio);
                                Desactivar(Acciones.Inicio);
                                Mostrar(Acciones.Inicio);
                                Cargar(Acciones.Inicio);

                                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Los Resultados de la Actividad fueron registrados correctamente.", Proceso.Correcto);
                            }
                        }
                    }
                }
            }
        }
    }

    private void AjustarPresupuesto()
    {
        Decimal ID_ACTIVIDAD = Convert.ToDecimal(HiddenField_ID_ACTIVIDAD.Value);
        Decimal ID_DETALLE = Convert.ToDecimal(HiddenField_ID_DETALLE.Value);

        Decimal presupuesto = Convert.ToDecimal(HiddenField_PRESUPUESTO.Value);
        Decimal asignado = Convert.ToDecimal(HiddenField_PRESUPUESTO_ASIGNADO_EMPRESA.Value);
        Decimal ejecutado = Convert.ToDecimal(HiddenField_PRESUPUESTO_EJECUTADO_EMPRESA.Value);

        Decimal presupuestoInicialActividad = Convert.ToDecimal(TextBox_PresupuestoAsignado.Text);
        Decimal presupuestoAjustadoActividad = 0;

        Boolean correcto = true;

        try
        {
            presupuestoAjustadoActividad = Convert.ToDecimal(TextBox_PresupuestoAjustado.Text);
        }
        catch
        {
            presupuestoAjustadoActividad = 0;
            correcto = false;
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe Digitar un valor correcto para el nuevo presupuesto para la actividad.", Proceso.Advertencia);
        }

        if (correcto == true)
        { 
            asignado = asignado - presupuestoInicialActividad;
            if ((asignado + presupuestoAjustadoActividad + ejecutado) > presupuesto)
            {
                correcto = false;
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El valor del presupeusto que desea ajustar sobrepasa el valor máximo que puede ser asigando.", Proceso.Advertencia);
            }
        }

        if (correcto == true)
        {

            tools _tools = new tools();

            Byte[] ARCHIVO = null;
            Int32 ARCHIVO_TAMANO = 0;
            String ARCHIVO_EXTENSION = null;
            String ARCHIVO_TYPE = null;
            if (FileUpload_CertificacionAjuste.HasFile == true)
            {
                using (BinaryReader reader = new BinaryReader(FileUpload_CertificacionAjuste.PostedFile.InputStream))
                {
                    ARCHIVO = reader.ReadBytes(FileUpload_CertificacionAjuste.PostedFile.ContentLength);
                    ARCHIVO_TAMANO = FileUpload_CertificacionAjuste.PostedFile.ContentLength;
                    ARCHIVO_TYPE = FileUpload_CertificacionAjuste.PostedFile.ContentType;
                    ARCHIVO_EXTENSION = _tools.obtenerExtensionArchivo(FileUpload_CertificacionAjuste.PostedFile.FileName);
                }
            }

            Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            Boolean verificado = _programa.AjustarPresupuestoDetalleActividad(ID_DETALLE, presupuestoAjustadoActividad,  Programa.EstadosDetalleActividad.APROBADA, ARCHIVO, ARCHIVO_TAMANO, ARCHIVO_EXTENSION, ARCHIVO_TYPE);

            if (verificado == true)
            {
                Ocultar(Acciones.Inicio);
                Desactivar(Acciones.Inicio);
                Mostrar(Acciones.Inicio);
                Cargar(Acciones.Inicio);

                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El prsupuesto de la actividad fue ajustado Correctamente.", Proceso.Correcto);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _programa.MensajeError, Proceso.Error);
            }
        }
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        if (HiddenField_ACCION_SOBRE_BOTON.Value == AccionesSobreBotones.Modificar.ToString())
        {
            Actualizar(); 
        }
        else
        {
            if (HiddenField_ACCION_SOBRE_BOTON.Value == AccionesSobreBotones.Reprogramar.ToString())
            {
                Reprogramar(); 
            }
            else
            {
                if (HiddenField_ACCION_SOBRE_BOTON.Value == AccionesSobreBotones.AjustarPresupuesto.ToString())
                {
                    AjustarPresupuesto(); 
                }
                else
                {
                    if (HiddenField_ACCION_SOBRE_BOTON.Value == AccionesSobreBotones.CancelarActividad.ToString())
                    {
                        CancelarActividad(); 
                    }
                    else
                    {
                        if (HiddenField_ACCION_SOBRE_BOTON.Value == AccionesSobreBotones.ResultadosActividad.ToString())
                        {
                            ResultadosActividad(); 
                        }
                        else
                        {
                            if (HiddenField_ACCION_SOBRE_BOTON.Value == AccionesSobreBotones.ModificarFinal.ToString())
                            {
                                ActualizarFinal(); 
                            }
                        }
                    }
                }
            }
        }
    }

    private void Activar(Acciones accion)
    { 
        switch(accion)
        {
            case Acciones.Modificar:
                TextBox_Resumen.Enabled = true;
                TextBox_PersonalCitado.Enabled = true;
                DropDownList_Encargado.Enabled = true;

                DropDownList_REGIONAL.Enabled = true;
                DropDownList_DEPARTAMENTO.Enabled = true;
                DropDownList_CIUDAD.Enabled = true;
                break;
            case Acciones.ModificarFinal:
                TextBox_ConclusionesActividad.Enabled = true;
                break;
            case Acciones.Reprogramar:
                TextBox_FechaActividad.Enabled = true;

                TimePicker_HoraInicioActividad.Enabled = true;
                TimePicker_HoraFinActividad.Enabled = true;

                DropDownList_TipoReprogramacion.Enabled = true;
                TextBox_MotivoReprogramacion.Enabled = true;
                break;
            case Acciones.AjustarPresupuesto:
                TextBox_PresupuestoAjustado.Enabled = true;
                break;
            case Acciones.Cancelar:
                TextBox_Motivocancelacion.Enabled = true;
                DropDownList_MotivoCancelacion.Enabled = true;
                break;
            case Acciones.Terminar:
                TextBox_PresupuestoFinal.Enabled = true;
                TextBox_PersonalFinal.Enabled = true;

                if (HiddenField_SECCIONES_HABILITADAS.Value.Contains("Resultados Encuesta") == true)
                {
                    TextBox_LogisticaBuena.Enabled = true;
                    TextBox_LogisticaRegular.Enabled = true;
                    TextBox_LogisticaMala.Enabled = true;
                    TextBox_InstructorBuena.Enabled = true;
                    TextBox_InstructorRegular.Enabled = true;
                    TextBox_InstructorMala.Enabled = true;
                    TextBox_InstalacionesBuena.Enabled = true;
                    TextBox_InstalacionesRegular.Enabled = true;
                    TextBox_InstalacionesMala.Enabled = true;
                }

                if (HiddenField_SECCIONES_HABILITADAS.Value.Contains("Control Asistencia") == true)
                {
                    GridView_ControlAsistencia.Enabled = true;
                }



                TextBox_ConclusionesActividad.Enabled = true;
                break;
        }
    }

    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        HiddenField_ACCION_SOBRE_BOTON.Value = AccionesSobreBotones.Modificar.ToString();

        Ocultar(Acciones.Modificar);
        Activar(Acciones.Modificar);
        Mostrar(Acciones.Modificar);

        Cargar(Acciones.Modificar);
    }

    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
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

    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }
    protected void Button_REPROGRAMAR_Click(object sender, EventArgs e)
    {
        HiddenField_ACCION_SOBRE_BOTON.Value = AccionesSobreBotones.Reprogramar.ToString();

        Ocultar(Acciones.Reprogramar);
        Activar(Acciones.Reprogramar);
        Mostrar(Acciones.Reprogramar);
        Cargar(Acciones.Reprogramar);
    }
    
    protected void Button_CANCELAR_ACTIVIDAD_Click(object sender, EventArgs e)
    {
        HiddenField_ACCION_SOBRE_BOTON.Value = AccionesSobreBotones.CancelarActividad.ToString();

        Ocultar(Acciones.Cancelar);
        Activar(Acciones.Cancelar);
        Mostrar(Acciones.Cancelar);
        Cargar(Acciones.Cancelar);
    }

    private void Limpiar(Acciones accion)
    {
        switch (accion)
        { 
            case Acciones.Terminar:
                TextBox_PresupuestoFinal.Text = "";
                TextBox_PersonalFinal.Text = "";

                TextBox_LogisticaBuena.Text = "";
                TextBox_LogisticaRegular.Text = "";
                TextBox_LogisticaMala.Text = "";
                TextBox_InstructorBuena.Text = "";
                TextBox_InstructorRegular.Text = "";
                TextBox_InstructorMala.Text = "";
                TextBox_InstalacionesBuena.Text = "";
                TextBox_InstalacionesRegular.Text = "";
                TextBox_InstalacionesMala.Text = "";


                GridView_EntidadesColaboradoras.DataSource = null;
                GridView_EntidadesColaboradoras.DataBind();

                GridView_Compromisos.DataSource = null;
                GridView_Compromisos.DataBind();

                TextBox_ConclusionesActividad.Text = "";
                break;
        }

    }

    protected void Button_ResultadosActividad_Click(object sender, EventArgs e)
    {
        HiddenField_ACCION_SOBRE_BOTON.Value = AccionesSobreBotones.ResultadosActividad.ToString();

        Ocultar(Acciones.Terminar);
        Mostrar(Acciones.Terminar);
        Activar(Acciones.Terminar);
        Limpiar(Acciones.Terminar);
        Cargar(Acciones.Terminar);
    }
    protected void CheckBox_Asistencia_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox check = (CheckBox)sender;

        if (check.Checked == true)
        {
            Label_trabajadoresSeleciconados.Text = (Convert.ToInt32(Label_trabajadoresSeleciconados.Text) + 1).ToString();
        }
        else
        {
            Label_trabajadoresSeleciconados.Text = (Convert.ToInt32(Label_trabajadoresSeleciconados.Text) - 1).ToString();
        }

        if (Convert.ToInt32(Label_trabajadoresSeleciconados.Text) == GridView_ControlAsistencia.Rows.Count)
        {
            CheckBox_TodosEmpleados.Checked = true;
        }
        else
        {
            CheckBox_TodosEmpleados.Checked = false;
        }

        CheckBox_MostrarSoloSeleccionados.Checked = false;
    }

    private void CalculoEncuesta()
    {
        if (String.IsNullOrEmpty(TextBox_PersonalFinal.Text) == true)
        {
            Informar(Panel_FONDO_MENSAJE_ENCUESTA, Image_MENSAJE_ENCUESTA_POPUP, Panel_MENSAJES_ENCUESTA, Label_MENSAJE_ENCUESTA, "Para Poder digitar los resultados de la Tabulación de la encuesta es necesario digitar primero EL PERSONAL QUE ASISTIÓ.", Proceso.Advertencia);
        }
        else
        {
            Int32 PERSONAL_FINAL = Convert.ToInt32(TextBox_PersonalFinal.Text);

            Int32 logicaBuena = 0;
            try { logicaBuena = Convert.ToInt32(TextBox_LogisticaBuena.Text); }
            catch { logicaBuena = 0; }

            Int32 logicaRegular = 0;
            try { logicaRegular = Convert.ToInt32(TextBox_LogisticaRegular.Text); }
            catch { logicaRegular = 0; }

            Int32 logicaMala = 0;
            try { logicaMala = Convert.ToInt32(TextBox_LogisticaMala.Text); }
            catch { logicaMala = 0; }

            Int32 totalLogistica = logicaBuena + logicaRegular + logicaMala;


            Int32 instructorBuena = 0;
            try { instructorBuena = Convert.ToInt32(TextBox_InstructorBuena.Text); }
            catch { instructorBuena = 0; }

            Int32 instructorRegular = 0;
            try { instructorRegular = Convert.ToInt32(TextBox_InstructorRegular.Text); }
            catch { instructorRegular = 0; }

            Int32 instructorMala = 0;
            try { instructorMala = Convert.ToInt32(TextBox_InstructorMala.Text); }
            catch { instructorMala = 0; }

            Int32 totalInstructor = instructorBuena + instructorRegular + instructorMala;


            Int32 instalacionesBuena = 0;
            try { instalacionesBuena = Convert.ToInt32(TextBox_InstalacionesBuena.Text); }
            catch { instalacionesBuena = 0; }

            Int32 instalacionesRegular = 0;
            try { instalacionesRegular = Convert.ToInt32(TextBox_InstalacionesRegular.Text); }
            catch { instalacionesRegular = 0; }

            Int32 instalacionesMala = 0;
            try { instalacionesMala = Convert.ToInt32(TextBox_InstalacionesMala.Text); }
            catch { instalacionesMala = 0; }

            Int32 totalInstalaciones = instalacionesBuena + instalacionesRegular + instalacionesMala;


            Int32 totalBuena = logicaBuena + instructorBuena + instalacionesBuena;
            Int32 totalRregular = logicaRegular + instructorRegular + instalacionesRegular;
            Int32 totalMala = logicaMala + instructorMala + instalacionesMala;
            Int32 totalTotal = totalLogistica + totalInstructor + totalInstalaciones;

            TextBox_TotalBuena.Text = totalBuena.ToString();
            TextBox_TotalRegular.Text = totalRregular.ToString();
            TextBox_TotalMala.Text = totalMala.ToString();


            if (totalLogistica <= 0)
            {
                TextBox_LogisticaPorcentajeBuena.Text = "0 %";
            }
            else
            {
                TextBox_LogisticaPorcentajeBuena.Text = (((Decimal)logicaBuena * 100) / (Decimal)totalLogistica).ToString("#.#") + " %";
            }

            if (totalInstructor <= 0)
            {
                TextBox_InstructorPorcentajeBuena.Text = "0 %";
            }
            else
            {
                TextBox_InstructorPorcentajeBuena.Text = (((Decimal)instructorBuena * 100) / (Decimal)totalInstructor).ToString("#.#") + " %";
            }

            if (totalInstalaciones <= 0)
            {
                TextBox_InstalacionesPorcentajeBuena.Text = "0 %";
            }
            else
            {
                TextBox_InstalacionesPorcentajeBuena.Text = (((Decimal)instalacionesBuena * 100) / (Decimal)totalInstalaciones).ToString("#.#") + " %";
            }

            if (totalTotal <= 0)
            {
                TextBox_TotalBenaPorcentaje.Text = "0 %";
                TextBox_TotalRegularPorcentaje.Text = "0 %";
                TextBox_TotalMalaPorcentaje.Text = "0 %";
            }
            else
            {
                TextBox_TotalBenaPorcentaje.Text = (((Decimal)totalBuena * 100) / (Decimal)totalTotal).ToString("#.#") + " %";
                TextBox_TotalRegularPorcentaje.Text = (((Decimal)totalRregular * 100) / (Decimal)totalTotal).ToString("#.#") + " %";
                TextBox_TotalMalaPorcentaje.Text = (((Decimal)totalMala * 100) / (Decimal)totalTotal).ToString("#.#") + " %";
            }

            if (totalLogistica > PERSONAL_FINAL)
            {
                Informar(Panel_FONDO_MENSAJE_ENCUESTA, Image_MENSAJE_ENCUESTA_POPUP, Panel_MENSAJES_ENCUESTA, Label_MENSAJE_ENCUESTA, "El número de encuestas totales de LOGISTICA (" + totalLogistica.ToString() + ") no puede superar el número de participantes (" + PERSONAL_FINAL.ToString() + ") de la encuesta", Proceso.Advertencia);
            }
            else
            {
                if (totalInstructor > PERSONAL_FINAL)
                {
                    Informar(Panel_FONDO_MENSAJE_ENCUESTA, Image_MENSAJE_ENCUESTA_POPUP, Panel_MENSAJES_ENCUESTA, Label_MENSAJE_ENCUESTA, "El número de encuestas totales de INSTRUCTOR (" + totalInstructor.ToString() + ") no puede superar el número de participantes (" + PERSONAL_FINAL.ToString() + ") de la encuesta", Proceso.Advertencia);
                }
                else
                {
                    if (totalInstalaciones > PERSONAL_FINAL)
                    {
                        Informar(Panel_FONDO_MENSAJE_ENCUESTA, Image_MENSAJE_ENCUESTA_POPUP, Panel_MENSAJES_ENCUESTA, Label_MENSAJE_ENCUESTA, "El número de encuestas totales de INSTALACIONES (" + totalInstalaciones.ToString() + ") no puede superar el número de participantes (" + PERSONAL_FINAL.ToString() + ") de la encuesta", Proceso.Advertencia);
                    }
                }
            }
        }
    }

























    protected void GridView_EntidadesColaboradoras_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "eliminar")
        {
            DataTable tablaDesdeGrilla = obtenerDataTableDe_GridView_EntidadesColaboradoras();

            tablaDesdeGrilla.Rows.RemoveAt(indexSeleccionado);

            llenar_GridView_EntidadesColaboradoras_desde_tabla(tablaDesdeGrilla);

            inhabilitarFilasGrilla(GridView_EntidadesColaboradoras, 1);

            GridView_EntidadesColaboradoras.Columns[0].Visible = true;

            HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
            HiddenField_FILA_GRILLA_SELECCIONADA.Value = null;

            Button_NuevaEntidadColaboradora.Visible = true;
            Button_GuardarEntidad.Visible = false;
            Button_CancelarEntidad.Visible = false;
        }
    }

    private DataTable obtenerDataTableDe_GridView_EntidadesColaboradoras()
    {
        DataTable tablaResultado = new DataTable();
        tablaResultado.Columns.Add("ID_ENTIDAD_COLABORA");
        tablaResultado.Columns.Add("ID_ENTIDAD");
        tablaResultado.Columns.Add("DESCRIPCION");

        DataRow filaTablaResultado;

        Decimal ID_ENTIDAD_COLABORA;
        Decimal ID_ENTIDAD;
        String DESCRIPCION;

        GridViewRow filaGrilla;
        TextBox dato;
        DropDownList datoDrop;
        for (int i = 0; i < GridView_EntidadesColaboradoras.Rows.Count; i++)
        {
            filaGrilla = GridView_EntidadesColaboradoras.Rows[i];

            ID_ENTIDAD_COLABORA = Convert.ToDecimal(GridView_EntidadesColaboradoras.DataKeys[i].Values["ID_ENTIDAD_COLABORA"]);

            datoDrop = filaGrilla.FindControl("DropDownList_EntidadesColoboradoras") as DropDownList;
            try
            {
                ID_ENTIDAD = Convert.ToDecimal(datoDrop.SelectedValue);
            }
            catch
            {
                ID_ENTIDAD = 0;
            }

            dato = filaGrilla.FindControl("TextBox_DescripcionEntidad") as TextBox;
            DESCRIPCION = dato.Text.Trim();

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_ENTIDAD_COLABORA"] = ID_ENTIDAD_COLABORA;
            filaTablaResultado["ID_ENTIDAD"] = ID_ENTIDAD;
            filaTablaResultado["DESCRIPCION"] = DESCRIPCION;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }

    private DataTable obtenerDataTableDe_GridView_Compromisos()
    {
        DataTable tablaResultado = new DataTable();
        tablaResultado.Columns.Add("ID_MAESTRA_COMPROMISO");
        tablaResultado.Columns.Add("COMPROMISO");
        tablaResultado.Columns.Add("USU_LOG_RESPONSABLE");
        tablaResultado.Columns.Add("FECHA_P");
        tablaResultado.Columns.Add("OBSERVACIONES");
        tablaResultado.Columns.Add("ESTADO");

        for (int i = 0; i < GridView_Compromisos.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_Compromisos.Rows[i];
                        
            Decimal ID_MAESTRA_COMPROMISO = Convert.ToDecimal(GridView_Compromisos.DataKeys[i].Values["ID_MAESTRA_COMPROMISO"]);

            TextBox textoCompromiso = filaGrilla.FindControl("TextBox_Compromiso") as TextBox;
            String COMPROMISO = textoCompromiso.Text;

            DropDownList dropResponsable = filaGrilla.FindControl("DropDownList_ResponsableCompromiso") as DropDownList;
            String USU_LOG_RESPONSABLE = dropResponsable.SelectedValue;

            TextBox textoFechaP = filaGrilla.FindControl("TextBox_FechaCompromiso") as TextBox;
            String FECHA_P = textoFechaP.Text;

            TextBox textoObservaciones = filaGrilla.FindControl("TextBox_ObservacionesCompromiso") as TextBox;
            String OBSERVACIONES = textoObservaciones.Text;

            Label textoEstado = filaGrilla.FindControl("Label_EstadoCompromiso") as Label;
            String ESTADO = textoEstado.Text;


            DataRow filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_MAESTRA_COMPROMISO"] = ID_MAESTRA_COMPROMISO;
            filaTablaResultado["COMPROMISO"] = COMPROMISO;
            filaTablaResultado["USU_LOG_RESPONSABLE"] = USU_LOG_RESPONSABLE;
            filaTablaResultado["FECHA_P"] = FECHA_P;
            filaTablaResultado["OBSERVACIONES"] = OBSERVACIONES;
            filaTablaResultado["ESTADO"] = ESTADO;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }

    private void llenar_GridView_EntidadesColaboradoras_desde_tabla(DataTable tablaDatos)
    {
        GridView_EntidadesColaboradoras.DataSource = tablaDatos;
        GridView_EntidadesColaboradoras.DataBind();

        GridViewRow filaGrilla;
        DataRow filaTabla;
        TextBox dato;
        DropDownList datoDrop;

        for (int i = 0; i < tablaDatos.Rows.Count; i++)
        {
            filaGrilla = GridView_EntidadesColaboradoras.Rows[i];
            filaTabla = tablaDatos.Rows[i];

            datoDrop = filaGrilla.FindControl("DropDownList_EntidadesColoboradoras") as DropDownList;
            Cargar(Listas.EntidadesCOlaboradoras, datoDrop);
            if (filaTabla["ID_ENTIDAD"].ToString() == "0")
            {
                datoDrop.SelectedIndex = 0;
            }
            else
            {
                datoDrop.SelectedValue = filaTabla["ID_ENTIDAD"].ToString();
            }

            dato = filaGrilla.FindControl("TextBox_DescripcionEntidad") as TextBox;
            dato.Text = filaTabla["DESCRIPCION"].ToString();
        }
    }





    private void llenar_GridView_Compromisos_desde_tabla(DataTable tablaDatos)
    {
        GridView_Compromisos.DataSource = tablaDatos;
        GridView_Compromisos.DataBind();

        for (int i = 0; i < tablaDatos.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_Compromisos.Rows[i];
            DataRow filaTabla = tablaDatos.Rows[i];

            TextBox textoCompromiso = filaGrilla.FindControl("TextBox_Compromiso") as TextBox;
            textoCompromiso.Text = filaTabla["COMPROMISO"].ToString().Trim();

            DropDownList dropResponsable = filaGrilla.FindControl("DropDownList_ResponsableCompromiso") as DropDownList;
            Cargar(Listas.ResponsablesCompromisos, dropResponsable);
            if (String.IsNullOrEmpty(filaTabla["USU_LOG_RESPONSABLE"].ToString().Trim()) == true)
            {
                dropResponsable.SelectedIndex = 0;
            }
            else
            {
                dropResponsable.SelectedValue = filaTabla["USU_LOG_RESPONSABLE"].ToString();
            }

            TextBox textoFechaP = filaGrilla.FindControl("TextBox_FechaCompromiso") as TextBox;
            try
            {
                textoFechaP.Text = Convert.ToDateTime(filaTabla["FECHA_P"]).ToShortDateString();
            }
            catch
            { 
                textoFechaP.Text  = "";
            }

            TextBox textoObservaciones = filaGrilla.FindControl("TextBox_ObservacionesCompromiso") as TextBox;
            textoObservaciones.Text = filaTabla["OBSERVACIONES"].ToString().Trim();

            Label textoEstado = filaGrilla.FindControl("Label_EstadoCompromiso") as Label;
            textoEstado.Text = filaTabla["ESTADO"].ToString().Trim();
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

    private void inhabilitarFilaGrilla(GridView grilla, Int32 Numfila, Int32 colInicio)
    {
        for (int j = colInicio; j < grilla.Columns.Count; j++)
        {
            grilla.Rows[Numfila].Cells[j].Enabled = false;
        }
    }

    private void habilitarFilaGrilla(GridView grilla, Int32 NumFila, Int32 colInicio)
    {
        for (int j = colInicio; j < grilla.Columns.Count; j++)
        {
            grilla.Rows[NumFila].Cells[j].Enabled = true;
        }
    }

    protected void Button_NuevaEntidadColaboradora_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerDataTableDe_GridView_EntidadesColaboradoras();
        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_ENTIDAD_COLABORA"] = 0;
        filaNueva["ID_ENTIDAD"] = 0;
        filaNueva["DESCRIPCION"] = "";

        tablaDesdeGrilla.Rows.Add(filaNueva);

        llenar_GridView_EntidadesColaboradoras_desde_tabla(tablaDesdeGrilla);

        inhabilitarFilasGrilla(GridView_EntidadesColaboradoras, 1);
        habilitarFilaGrilla(GridView_EntidadesColaboradoras, GridView_EntidadesColaboradoras.Rows.Count - 1 , 1);
        
        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_GRILLA_SELECCIONADA.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();

        GridView_EntidadesColaboradoras.Columns[0].Visible = false;

        Button_NuevaEntidadColaboradora.Visible = false;
        Button_GuardarEntidad.Visible = true;
        Button_CancelarEntidad.Visible = true;
    }
    protected void Button_GuardarEntidad_Click(object sender, EventArgs e)
    {
        int FILA_SELECCIONADA = Convert.ToInt32(HiddenField_FILA_GRILLA_SELECCIONADA.Value);

        inhabilitarFilaGrilla(GridView_EntidadesColaboradoras, FILA_SELECCIONADA, 1);

        GridView_EntidadesColaboradoras.Columns[0].Visible = true;

        Button_GuardarEntidad.Visible = false;
        Button_CancelarEntidad.Visible = false;
        Button_NuevaEntidadColaboradora.Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
    }
    protected void Button_CancelarEntidad_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = obtenerDataTableDe_GridView_EntidadesColaboradoras();

        if (HiddenField_ACCION_GRILLA.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }

        llenar_GridView_EntidadesColaboradoras_desde_tabla(tablaGrilla);

        inhabilitarFilasGrilla(GridView_EntidadesColaboradoras, 1);

        GridView_EntidadesColaboradoras.Columns[0].Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();

        Button_NuevaEntidadColaboradora.Visible = true;
        Button_GuardarEntidad.Visible = false;
        Button_CancelarEntidad.Visible = false;
    }
    protected void Button_NUevoAdjunto_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.NuevoAdjunto);
        Mostrar(Acciones.NuevoAdjunto);
        Limpiar(Acciones.NuevoAdjunto);
    }

    
    protected void Button_CERRAR_MENSAJE_ENCUESTA_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE_ENCUESTA, Panel_MENSAJES_ENCUESTA);
    }
    protected void Button_AJUSTARPRESUPUESTO_Click(object sender, EventArgs e)
    {
        HiddenField_ACCION_SOBRE_BOTON.Value = AccionesSobreBotones.AjustarPresupuesto.ToString();

        Ocultar(Acciones.AjustarPresupuesto);
        Activar(Acciones.AjustarPresupuesto);
        Mostrar(Acciones.AjustarPresupuesto);
        Cargar(Acciones.AjustarPresupuesto);
    }

    private void CargarGrillaConLetraSeleccionada()
    {
        String letraSeleciconada = string.Empty;
        if (String.IsNullOrEmpty(HiddenField_LETRA_PAGINACION_LISTA.Value) == false)
        {
            letraSeleciconada = HiddenField_LETRA_PAGINACION_LISTA.Value;
        }

        String inicialRazSocial = String.Empty;

        if (String.IsNullOrEmpty(letraSeleciconada) == true)
        {
            foreach (GridViewRow filaGrilla in GridView_ControlAsistencia.Rows)
            {
                filaGrilla.Visible = true;
            }
        }
        else
        {
            foreach (GridViewRow filaGrilla in GridView_ControlAsistencia.Rows)
            {
                filaGrilla.Visible = filaGrilla.Cells[0].Text.Trim().ToUpper().StartsWith(letraSeleciconada.ToUpper());
            }
        }

        CheckBox_MostrarSoloSeleccionados.Checked = false;
    }

    protected void CheckBox_TodosEmpleados_CheckedChanged(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "";

        if (CheckBox_TodosEmpleados.Checked == true)
        {
            foreach (GridViewRow filaGrilla in GridView_ControlAsistencia.Rows)
            {
                CheckBox checkSeleccionar = filaGrilla.FindControl("CheckBox_Asistencia") as CheckBox;

                filaGrilla.Visible = true;

                checkSeleccionar.Checked = true;
            }

            Label_trabajadoresSeleciconados.Text = GridView_ControlAsistencia.Rows.Count.ToString();
        }
        else
        {
            foreach (GridViewRow filaGrilla in GridView_ControlAsistencia.Rows)
            {
                CheckBox checkSeleccionar = filaGrilla.FindControl("CheckBox_Asistencia") as CheckBox;

                checkSeleccionar.Checked = false;

                HiddenField_LETRA_PAGINACION_LISTA.Value = "A";
                CargarGrillaConLetraSeleccionada();
            }

            Label_trabajadoresSeleciconados.Text = "0";
        }

        CheckBox_MostrarSoloSeleccionados.Checked = false;
    }

    protected void Button_FiltrarEmpleados_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "";

        CheckBox_TodosEmpleados.Checked = false;

        String valorABuscar = TextBox_FiltroEmpleados.Text.Trim();

        foreach (GridViewRow filaGrilla in GridView_ControlAsistencia.Rows)
        {
            Boolean encontrado = false;
            
            String valorGrilla = filaGrilla.Cells[0].Text + " " + filaGrilla.Cells[1].Text + " " + filaGrilla.Cells[2].Text;

            valorGrilla = WebUtility.HtmlDecode(valorGrilla);

            encontrado = valorGrilla.ToUpper().Contains(valorABuscar.ToUpper());

            filaGrilla.Visible = encontrado;
        }

        CheckBox_MostrarSoloSeleccionados.Checked = false;
    }
    protected void TextBox_LogisticaBuena_TextChanged(object sender, EventArgs e)
    {
        CalculoEncuesta();
        TextBox_LogisticaRegular.Focus();
    }
    protected void TextBox_LogisticaRegular_TextChanged(object sender, EventArgs e)
    {
        CalculoEncuesta();
        TextBox_LogisticaMala.Focus();
    }
    protected void TextBox_LogisticaMala_TextChanged(object sender, EventArgs e)
    {
        CalculoEncuesta();
        TextBox_InstructorBuena.Focus();
    }
    protected void TextBox_InstructorBuena_TextChanged(object sender, EventArgs e)
    {
        CalculoEncuesta();
        TextBox_InstructorRegular.Focus();
    }
    protected void TextBox_InstructorRegular_TextChanged(object sender, EventArgs e)
    {
        CalculoEncuesta();
        TextBox_InstructorMala.Focus();
    }
    protected void TextBox_InstructorMala_TextChanged(object sender, EventArgs e)
    {
        CalculoEncuesta();
        TextBox_InstalacionesBuena.Focus();
    }
    protected void TextBox_InstalacionesBuena_TextChanged(object sender, EventArgs e)
    {
        CalculoEncuesta();
        TextBox_InstalacionesRegular.Focus();
    }
    protected void TextBox_InstalacionesRegular_TextChanged(object sender, EventArgs e)
    {
        CalculoEncuesta();
        TextBox_InstalacionesMala.Focus();
    }
    protected void TextBox_InstalacionesMala_TextChanged(object sender, EventArgs e)
    {
        CalculoEncuesta();
        TextBox_InstalacionesMala.Focus();
    }
    protected void Button_MODIFICARFINAL_Click(object sender, EventArgs e)
    {
        HiddenField_ACCION_SOBRE_BOTON.Value = AccionesSobreBotones.ModificarFinal.ToString();

        Ocultar(Acciones.ModificarFinal);
        Activar(Acciones.ModificarFinal);
        Mostrar(Acciones.ModificarFinal);
    }
    protected void Button_NuevoAdjunto_Click(object sender, EventArgs e)
    {
        Panel_NuevoAdjunto.Visible = true;

        TextBox_TituloAdjunto.Text = "";
        TextBox_DescripcionAdjunto.Text = "";

        Button_NuevoAdjunto.Visible = false;
        Button_GuardarAdjunto.Visible = true;
        Button_CancelarAdjunto.Visible = false;
    }
    protected void Button_GuardarAdjunto_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();

        Decimal ID_DETALLE = Convert.ToDecimal(HiddenField_ID_DETALLE.Value);

        Byte[] ARCHIVO = null;
        Int32 ARCHIVO_TAMANO = 0;
        String ARCHIVO_EXTENSION = null;
        String ARCHIVO_TYPE = null;
        if (FileUpload_AdjuntosInforme.HasFile == true)
        {
            using (BinaryReader reader = new BinaryReader(FileUpload_AdjuntosInforme.PostedFile.InputStream))
            {
                ARCHIVO = reader.ReadBytes(FileUpload_AdjuntosInforme.PostedFile.ContentLength);
                ARCHIVO_TAMANO = FileUpload_AdjuntosInforme.PostedFile.ContentLength;
                ARCHIVO_TYPE = FileUpload_AdjuntosInforme.PostedFile.ContentType;
                ARCHIVO_EXTENSION = _tools.obtenerExtensionArchivo(FileUpload_AdjuntosInforme.PostedFile.FileName);
            }
        }

        String TITULO_ADJUNTO = TextBox_TituloAdjunto.Text.Trim();
        String DESCRIPCION_ADJUNTO = TextBox_DescripcionAdjunto.Text.Trim();

        Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Decimal ID_ADJUNTO = _programa.AdicionarAdjuntoResultadoActividad(ID_DETALLE, TITULO_ADJUNTO, DESCRIPCION_ADJUNTO, ARCHIVO, ARCHIVO_TAMANO, ARCHIVO_EXTENSION, ARCHIVO_TYPE);

        CargarAdjuntos();

        Panel_NuevoAdjunto.Visible = false;
        Button_NuevoAdjunto.Visible = true;
        Button_GuardarAdjunto.Visible = false;
        Button_CancelarAdjunto.Visible = false;
    }
    protected void Button_CancelarAdjunto_Click(object sender, EventArgs e)
    {
        Panel_NuevoAdjunto.Visible = false;
        Button_NuevoAdjunto.Visible = true;
        Button_GuardarAdjunto.Visible = false;
        Button_CancelarAdjunto.Visible = false;
    }
    protected void Button_NuevoCompromiso_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerDataTableDe_GridView_Compromisos();
        DataRow filaNueva = tablaDesdeGrilla.NewRow();


        filaNueva["ID_MAESTRA_COMPROMISO"] = 0;
        filaNueva["COMPROMISO"] = "";
        filaNueva["USU_LOG_RESPONSABLE"] = "";
        filaNueva["FECHA_P"] = "";
        filaNueva["OBSERVACIONES"] = "";
        filaNueva["ESTADO"] = MaestraCompromiso.EstadosCompromiso.ABIERTO.ToString();

        tablaDesdeGrilla.Rows.Add(filaNueva);

        llenar_GridView_Compromisos_desde_tabla(tablaDesdeGrilla);

        inhabilitarFilasGrilla(GridView_Compromisos, 1);
        habilitarFilaGrilla(GridView_Compromisos, GridView_Compromisos.Rows.Count - 1, 1);

        HiddenField_ACCION_GRILLA_COMPROMISO.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_GRILLA_SELECCIONADA_COMPROMISO.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();

        GridView_Compromisos.Columns[0].Visible = false;

        Button_NuevoCompromiso.Visible = false;
        Button_GuardarCompromiso.Visible = true;
        Button_CancelarCompromiso.Visible = true;
    }

    protected void Button_GuardarCompromiso_Click(object sender, EventArgs e)
    {
        int FILA_SELECCIONADA = Convert.ToInt32(HiddenField_FILA_GRILLA_SELECCIONADA_COMPROMISO.Value);

        inhabilitarFilaGrilla(GridView_Compromisos, FILA_SELECCIONADA, 1);

        GridView_Compromisos.Columns[0].Visible = true;

        Button_GuardarCompromiso.Visible = false;
        Button_CancelarCompromiso.Visible = false;
        Button_NuevoCompromiso.Visible = true;

        HiddenField_ACCION_GRILLA_COMPROMISO.Value = AccionesGrilla.Ninguna.ToString();
    }

    protected void Button_CancelarCompromiso_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = obtenerDataTableDe_GridView_Compromisos();

        if (HiddenField_ACCION_GRILLA_COMPROMISO.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }

        llenar_GridView_Compromisos_desde_tabla(tablaGrilla);

        inhabilitarFilasGrilla(GridView_Compromisos, 1);

        GridView_Compromisos.Columns[0].Visible = true;

        HiddenField_ACCION_GRILLA_COMPROMISO.Value = AccionesGrilla.Ninguna.ToString();

        Button_NuevoCompromiso.Visible = true;
        Button_GuardarCompromiso.Visible = false;
        Button_CancelarCompromiso.Visible = false;
    }
    protected void GridView_Compromisos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "eliminar")
        {
            DataTable tablaDesdeGrilla = obtenerDataTableDe_GridView_Compromisos();

            tablaDesdeGrilla.Rows.RemoveAt(indexSeleccionado);

            llenar_GridView_Compromisos_desde_tabla(tablaDesdeGrilla);

            inhabilitarFilasGrilla(GridView_Compromisos, 1);

            GridView_Compromisos.Columns[0].Visible = true;

            HiddenField_ACCION_GRILLA_COMPROMISO.Value = AccionesGrilla.Ninguna.ToString();
            HiddenField_FILA_GRILLA_SELECCIONADA_COMPROMISO.Value = null;

            Button_NuevoCompromiso.Visible = true;
            Button_GuardarCompromiso.Visible = false;
            Button_CancelarCompromiso.Visible = false;
        }
    }

    protected void LinkButton_a_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "A";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_z_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "Z";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_y_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "Y";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_x_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "X";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_w_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "W";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_v_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "V";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_u_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "U";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_t_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "T";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_s_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "S";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_r_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "R";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_q_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "Q";
        CargarGrillaConLetraSeleccionada();
    }

    protected void LinkButton_p_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "P";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_o_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "O";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_n_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "N";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_m_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "M";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_l_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "L";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_k_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "K";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_j_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "J";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_i_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "I";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_h_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "H";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_g_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "G";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_f_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "F";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_e_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "E";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton3_d_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "D";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_c_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "C";
        CargarGrillaConLetraSeleccionada();
    }
    protected void LinkButton_b_Click(object sender, EventArgs e)
    {
        HiddenField_LETRA_PAGINACION_LISTA.Value = "B";
        CargarGrillaConLetraSeleccionada();
    }

    protected void CheckBox_MostrarSoloSeleccionados_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox_MostrarSoloSeleccionados.Checked == true)
        {
            foreach (GridViewRow filaGrilla in GridView_ControlAsistencia.Rows)
            {
                CheckBox checkAsistencia = filaGrilla.FindControl("CheckBox_Asistencia") as CheckBox;

                filaGrilla.Visible = checkAsistencia.Checked;
            }

            HiddenField_LETRA_PAGINACION_LISTA.Value = "";
        }
        else
        {
            HiddenField_LETRA_PAGINACION_LISTA.Value = "A";
            CargarGrillaConLetraSeleccionada();
        }
    }
}