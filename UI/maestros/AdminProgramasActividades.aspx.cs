using System;
using System.Web.UI.WebControls;
using Brainsbits.LLB.maestras;
using TSHAK.Components;
using Brainsbits.LLB.comercial;
using System.Data;
using Brainsbits.LLB.programasRseGlobal;
using Brainsbits.LLB;
using System.Configuration;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using Brainsbits.LLB.seguridad;
using System.Web;

public partial class _AdminProgramasActividades : System.Web.UI.Page
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

    private String urlImgEmpresa = "~/imagenes/plantilla/imgEmpresa.png";
    private String urlImgPrograma = "~/imagenes/plantilla/imgPrograma.png";
    private String urlImgActividad = "~/imagenes/plantilla/imgActividad.png";

    private System.Drawing.Color colorAmarillo = System.Drawing.ColorTranslator.FromHtml("#FFF200");
    private System.Drawing.Color colorVerde = System.Drawing.ColorTranslator.FromHtml("#50CE04");
    private System.Drawing.Color colorRojo = System.Drawing.ColorTranslator.FromHtml("#F8523A");
    private System.Drawing.Color colorGris = System.Drawing.ColorTranslator.FromHtml("#cccccc");

    private enum Acciones
    {
        Inicio = 0, 
        Cargar,
        InfoEmpresa,
        NuevaActividad,
        CancelarNuevaActividad,
        EmpresaSelecionada
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }

    private enum Listas
    { 
        SubProgramas = 0,
        Actividades, 
        EstadosActividades, 
        TiposActividad, 
        SectoresActividad,
        Regionales,
        RegionalesConRestriccion,
        Encargados
    }

    ReportDocument reporte;

    private void configurar_paneles_popup(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_mensaje.CssClass = "panel_popup";
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

    private void Configurar()
    {
        configurar_paneles_popup(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    private void Activar(Acciones accion)
    { 
        switch(accion)
        {
            case Acciones.NuevaActividad:
                DropDownList_SubPrograma.Enabled = true;

                DropDownList_IdActividad.Enabled = true;

                TextBox_Resumen.Enabled = true;
                TextBox_FechaActividad.Enabled = true;
                TimePicker_HoraInicioActividad.Enabled = true;
                TimePicker_HoraFinActividad.Enabled = true;
                TextBox_PresupuestoAsignado.Enabled = true;
                TextBox_PersonalCitado.Enabled = true;
                DropDownList_Encargado.Enabled = true;
                DropDownList_REGIONAL.Enabled = true;
                break;
        }
    }

    private void Limpiar(Acciones accion)
    { 
        switch(accion)
        {
            case Acciones.NuevaActividad:
                DropDownList_SubPrograma.Items.Clear();
                
                DropDownList_IdActividad.Items.Clear();
                DropDownList_Tipo.Items.Clear();
                DropDownList_Sector.Items.Clear();
                DropDownList_EstadoActividad.Items.Clear();
                TextBox_DescripcionActividad.Text = "";

                TextBox_Resumen.Text = "";
                TextBox_FechaActividad.Text = "";

                TimePicker_HoraInicioActividad.SelectedTime = new DateTime();
                TimePicker_HoraFinActividad.SelectedTime = new DateTime();

                TextBox_PresupuestoAsignado.Text = "";
                TextBox_PersonalCitado.Text = "";

                DropDownList_Encargado.Items.Clear();

                DropDownList_REGIONAL.Items.Clear();
                DropDownList_DEPARTAMENTO.Items.Clear();
                DropDownList_CIUDAD.Items.Clear();
                break;
        }
    }

    private void AutoseleccionarUsuarioEncargado()
    {
        try
        {
            DropDownList_Encargado.SelectedValue = Session["USU_LOG"].ToString();
        }
        catch
        {
            DropDownList_Encargado.SelectedIndex = 0;
        }
    }

    protected void Button_NUEVAACTIVIDAD_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.NuevaActividad); 
        Mostrar(Acciones.NuevaActividad); 
        Desactivar(Acciones.Inicio); 
        Activar(Acciones.NuevaActividad); 
        Limpiar(Acciones.NuevaActividad); 

        Cargar(Acciones.NuevaActividad); 
		
        AutoseleccionarUsuarioEncargado();
    }
    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }

    private DataTable ConfigurarTablaParaProgramaEspecifico()
    {
        DataTable tablaTemp = new DataTable();

        tablaTemp.Columns.Add("ID_DETALLE");
        tablaTemp.Columns.Add("ID_ACTIVIDAD");
        tablaTemp.Columns.Add("REGIONAL");
        tablaTemp.Columns.Add("CIUDAD");
        tablaTemp.Columns.Add("PROGRAMA");
        tablaTemp.Columns.Add("ACTIVIDAD");
        tablaTemp.Columns.Add("FECHA");
        tablaTemp.Columns.Add("PRESUPUESTO", Type.GetType("System.Decimal"));
        tablaTemp.Columns.Add("ID_ESTADO");

        return tablaTemp;
    }

    private void CargarArbolDePrograma(Decimal ID_EMPRESA, Decimal ID_PRESUPUESTO, Decimal ID_PROGRAMA, Int32 ANNO)
    {
        Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaprograma = _programa.ObtenerActividadesProgramaEspecificoPorIdPrograma(ID_PROGRAMA);

        DataTable tablaParaGrilla = ConfigurarTablaParaProgramaEspecifico();

        for (int i = 0; i < tablaprograma.Rows.Count; i++)
        {
            DataRow filaPrograma = tablaprograma.Rows[i];
            DataRow filaParaGrilla = tablaParaGrilla.NewRow();

            Decimal ID_DETALLE_GENERAL = Convert.ToDecimal(filaPrograma["ID_DETALLE_GENERAL"]);

            filaParaGrilla["ID_DETALLE"] = filaParaGrilla["ID_DETALLE"];
            filaParaGrilla["ID_ACTIVIDAD"] = filaParaGrilla["ID_ACTIVIDAD"];

            filaParaGrilla["REGIONAL"] = filaPrograma["NOMBRE_REGIONAL"];
            filaParaGrilla["CIUDAD"] = filaPrograma["NOMBRE_CIUDAD"];

            DataTable tablaProgramaDeActividad = _programa.ObtenerProgramaAlQuePerteneceUnaActividadProgramada(ID_DETALLE_GENERAL);
            filaParaGrilla["PROGRAMA"] = tablaProgramaDeActividad.Rows[0]["NOMBRE"];

            filaParaGrilla["ACTIVIDAD"] = filaPrograma["NOMBRE_ACTIVIDAD"].ToString().Trim() + ": " + filaPrograma["RESUMEN_ACTIVIDAD"].ToString().Trim();

            filaParaGrilla["FECHA"] = filaPrograma["TRIMESTRE"].ToString().Trim() + "<br />" + Convert.ToDateTime(filaPrograma["FECHA_ACTIVIDAD"]).ToLongDateString() + "<br />" + filaPrograma["HORA_INICIO"].ToString().Trim() + " - " + filaPrograma["HORA_FIN"].ToString().Trim();

            filaParaGrilla["PRESUPUESTO"] = filaPrograma["PRESUPUESTO_APROBADO"];

            filaParaGrilla["ID_ESTADO"] = filaPrograma["ID_ESTADO"];

            tablaParaGrilla.Rows.Add(filaParaGrilla);
        }

        if (tablaParaGrilla.Rows.Count <= 0)
        {
            GridView_EsquemaProgramaEspecifico.DataSource = null;
            GridView_EsquemaProgramaEspecifico.DataBind();

            Panel_ArbolPrograma.Visible = false;

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se han configurado actividades para este Programa.", Proceso.Advertencia);
        }
        else
        {
            Panel_ArbolPrograma.Visible = true;

            GridView_EsquemaProgramaEspecifico.DataSource = tablaParaGrilla;
            GridView_EsquemaProgramaEspecifico.DataBind();

            for (int i = 0; i < GridView_EsquemaProgramaEspecifico.Rows.Count; i++)
            {
                String ID_ESTADO = GridView_EsquemaProgramaEspecifico.DataKeys[i].Values["ID_ESTADO"].ToString();

                if (ID_ESTADO == Programa.EstadosDetalleActividad.CREADA.ToString())
                {
                    GridView_EsquemaProgramaEspecifico.Rows[i].BackColor = colorAmarillo;
                }
                else
                {
                    if (ID_ESTADO == Programa.EstadosDetalleActividad.APROBADA.ToString())
                    {
                        GridView_EsquemaProgramaEspecifico.Rows[i].BackColor = colorVerde;
                    }
                    else
                    {
                        if (ID_ESTADO == Programa.EstadosDetalleActividad.CANCELADA.ToString())
                        {
                            GridView_EsquemaProgramaEspecifico.Rows[i].BackColor = colorRojo;
                        }
                        else
                        {
                            GridView_EsquemaProgramaEspecifico.Rows[i].BackColor = colorGris;
                        }
                    }
                }
            }
        }
    }

    private void Cargar(Decimal ID_PRESUPUESTO, Decimal ID_PROGRAMA, Int32 ANNO, Decimal ID_PROGRAMA_GENERAL)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        HiddenField_ID_PROGRAMA.Value = ID_PROGRAMA.ToString();
        HiddenField_ID_PRESUPUESTO.Value = ID_PRESUPUESTO.ToString();
        HiddenField_ANNO.Value = ANNO.ToString();
        HiddenField_ID_PROGRAMA_GENERAL.Value = ID_PROGRAMA_GENERAL.ToString();

        Int32 ANNO_ACTUAL = DateTime.Now.Year;


        HiddenField_FORM_SOLO_LECTURA.Value = "NO";

        if (ID_PROGRAMA <= 0)
        {
            Button_Imprimir.Visible = false;
            Button_Imprimir_1.Visible = false;
        }

        CargarArbolDePrograma(ID_EMPRESA, ID_PRESUPUESTO, ID_PROGRAMA, ANNO);
    }

    protected void GridView_RESULTADOS_BUSQUEDA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal ID_PROGRAMA = 0;

            if (String.IsNullOrEmpty(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_PROGRAMA"].ToString().Trim()) == false)
            {
                ID_PROGRAMA = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_PROGRAMA"]);
            }

            Int32 ANNO = Convert.ToInt32(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ANNO"]);

            decimal ID_PROGRAMA_GENERAL = 0;
            if (String.IsNullOrEmpty(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_PROGRAMA_GENERAL"].ToString().Trim()) == false)
            {
                ID_PROGRAMA_GENERAL = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_PROGRAMA_GENERAL"]);


                HiddenField_FORM_SOLO_LECTURA.Value = "NO";

                HiddenField_ASIGNADO.Value = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ASIGNADO"]).ToString();
                HiddenField_EJECUTADO.Value = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["EJECUTADO"]).ToString();
                HiddenField_PRESUPUESTO_TOTAL.Value = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["PRESUPUESTO"]).ToString();

                Decimal ID_PRESUPUESTO = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_PRESUPUESTO"]);

                Ocultar(Acciones.Inicio);
                Mostrar(Acciones.Cargar);

                Cargar(ID_PRESUPUESTO, ID_PROGRAMA, ANNO, ID_PROGRAMA_GENERAL);

                Label_PresupuestoMax.Text = String.Format("{0:C}",Convert.ToDecimal(HiddenField_PRESUPUESTO_TOTAL.Value) - (Convert.ToDecimal(HiddenField_ASIGNADO.Value) + Convert.ToDecimal(HiddenField_EJECUTADO.Value)));
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se ha creado el Programa General para el año " + ANNO + ". No puede continuar.", Proceso.Advertencia);
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

    private void CargarActividadesDeSubPrograma(DataTable tablaActividades, DropDownList drop)
    {
        drop.Items.Clear();

        drop.Items.Add(new ListItem("Seleccione...", ""));

        foreach (DataRow f in tablaActividades.Rows)
        {
            drop.Items.Add(new ListItem(f["NOMBRE_ACTIVIDAD"].ToString().Trim(), f["ID_DETALLE_GENERAL"].ToString().Trim() + ":" + f["ID_ACTIVIDAD"].ToString().Trim()));
        }

        drop.DataBind();
    }

    protected void DropDownList_SubPrograma_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(DropDownList_SubPrograma.SelectedIndex <= 0)
        {
            DropDownList_IdActividad.SelectedIndex = 0;
            DropDownList_Tipo.SelectedIndex = 0;
            DropDownList_Sector.SelectedIndex = 0;
            DropDownList_EstadoActividad.SelectedIndex = 0;
            TextBox_DescripcionActividad.Text = "";
        }
        else
        {
            Decimal ID_DETALLE_GENERAL_PADRE = Convert.ToDecimal(DropDownList_SubPrograma.SelectedValue.Split(':')[0]);
            
            Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaActividades = _programa.ObtenerActividadesPorDetalleGeneralPadre(ID_DETALLE_GENERAL_PADRE);

            CargarActividadesDeSubPrograma(tablaActividades, DropDownList_IdActividad);

            Cargar(Listas.TiposActividad, DropDownList_Tipo);
            Cargar(Listas.SectoresActividad, DropDownList_Sector);
            Cargar(Listas.EstadosActividades, DropDownList_EstadoActividad);

            DropDownList_IdActividad.SelectedIndex = 0;
            DropDownList_Tipo.SelectedIndex = 0;
            DropDownList_Sector.SelectedIndex = 0;
            DropDownList_EstadoActividad.SelectedIndex = 0;
            TextBox_DescripcionActividad.Text = "";
        }
    }
    protected void DropDownList_IdActividad_SelectedIndexChanged(object sender, EventArgs e)
    {
        Programa.Areas AREA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);
        
        if (DropDownList_IdActividad.SelectedIndex <= 0)
        {
            DropDownList_Tipo.SelectedIndex = 0;
            DropDownList_Sector.SelectedIndex = 0;
            DropDownList_EstadoActividad.SelectedIndex = 0;
            TextBox_DescripcionActividad.Text = "";
        }
        else
        {
            Decimal ID_ACTIVIDAD = Convert.ToDecimal(DropDownList_IdActividad.SelectedValue.Split(':')[1]);

            ActividadRseGlobal _act = new ActividadRseGlobal(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaAct = _act.ObtenerActividadPorId(ID_ACTIVIDAD, AREA);

            DataRow filaAct = tablaAct.Rows[0];

            DropDownList_Tipo.SelectedValue = filaAct["TIPO"].ToString().Trim();
            DropDownList_Sector.SelectedValue = filaAct["SECTOR"].ToString().Trim();
            DropDownList_EstadoActividad.SelectedValue = filaAct["ACTIVO"].ToString().Trim();

            TextBox_DescripcionActividad.Text = filaAct["DESCRIPCION"].ToString().Trim();
        }
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

    private void cargar_DropDownList_DEPARTAMENTO(String id)
    {
        DropDownList_DEPARTAMENTO.Items.Clear();

        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

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

    private void cargar_DropDownList_DEPARTAMENTO_ConRestriccion(String id)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

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

    protected void DropDownList_REGIONAL_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_REGIONAL.SelectedValue == "")
        {
            inhabilitar_DropDownList_CIUDAD();
            inhabilitar_DropDownList_DEPARTAMENTO();
        }
        else
        {
            cargar_DropDownList_DEPARTAMENTO_ConRestriccion(DropDownList_REGIONAL.SelectedValue);
            
            DropDownList_DEPARTAMENTO.Enabled = true;
            inhabilitar_DropDownList_CIUDAD();
        }
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

    private void cargar_DropDownList_CIUDAD_ConRestriccion(String idRegional, String idDepartamento)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        DropDownList_CIUDAD.Items.Clear();

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
        DataTable tablaCiudades = _ciudad.ObtenerCiudadesPorIdDepartamentoEIdRegional_ConRestricciones(idRegional, idDepartamento, ID_EMPRESA, Session["USU_LOG"].ToString());

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_CIUDAD.Items.Add(item);

        foreach (DataRow fila in tablaCiudades.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_CIUDAD"].ToString());
            DropDownList_CIUDAD.Items.Add(item);
        }

        DropDownList_CIUDAD.DataBind();
    }

    protected void DropDownList_DEPARTAMENTO_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_DEPARTAMENTO.SelectedValue == "")
        {
            inhabilitar_DropDownList_CIUDAD();
        }
        else
        {
            cargar_DropDownList_CIUDAD_ConRestriccion(DropDownList_REGIONAL.SelectedValue.ToString(), DropDownList_DEPARTAMENTO.SelectedValue.ToString());
            DropDownList_CIUDAD.Enabled = true;
        }
    }

    protected void Button_GAURDAR_ACTIVIDAD_Click(object sender, EventArgs e)
    {
        Decimal PRESUPUESTO_APROBADO = Convert.ToDecimal(TextBox_PresupuestoAsignado.Text.Trim());
        Decimal PRESUPUESTO_ASIGNADO_ACTUAL = Convert.ToDecimal(HiddenField_ASIGNADO.Value);
        Decimal PRESUPUESTO_EJECUTADO_ACTUAL = Convert.ToDecimal(HiddenField_EJECUTADO.Value);
        Decimal PRESUPUESTO_TOTAL = Convert.ToDecimal(HiddenField_PRESUPUESTO_TOTAL.Value);

        if (PRESUPUESTO_APROBADO > (PRESUPUESTO_TOTAL - (PRESUPUESTO_ASIGNADO_ACTUAL + PRESUPUESTO_EJECUTADO_ACTUAL)))
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El presupuesto asignado a la actividad sobrepasa el presupuesto maximo disponible.", Proceso.Advertencia);
        }
        else
        {
            Decimal ID_PROGRAMA = Convert.ToDecimal(HiddenField_ID_PROGRAMA.Value);
            Decimal ID_PROGRAMA_GENERAL = Convert.ToDecimal(HiddenField_ID_PROGRAMA_GENERAL.Value);

            Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
            Decimal ID_PRESUPUESTO = Convert.ToDecimal(HiddenField_ID_PRESUPUESTO.Value);
            Int32 ANNO = Convert.ToInt32(HiddenField_ANNO.Value);

            Programa.Areas AREA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);

            Decimal ID_DETALLE_GENERAL = Convert.ToDecimal(DropDownList_IdActividad.SelectedValue.Split(':')[0]);
            Decimal ID_ACTIVIDAD = Convert.ToDecimal(DropDownList_IdActividad.SelectedValue.Split(':')[1]);

            String RESUMEN_ACTIVIDAD = TextBox_Resumen.Text.Trim();

            DateTime FECHA_ACTIVIDAD = Convert.ToDateTime(TextBox_FechaActividad.Text.Trim());

            String HORA_INICIO = TimePicker_HoraInicioActividad.SelectedTime.ToShortTimeString();
            String HORA_FIN = TimePicker_HoraFinActividad.SelectedTime.ToShortTimeString();

            Int32 PERSONAL_CITADO = Convert.ToInt32(TextBox_PersonalCitado.Text.Trim());

            String ENCARGADO = DropDownList_Encargado.SelectedValue;

            String ID_CIUDAD = DropDownList_CIUDAD.SelectedValue;

            Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            Boolean verificado = _programa.AdicionarDetalleActividad(ID_PROGRAMA_GENERAL, ID_PROGRAMA, ID_EMPRESA, AREA, ID_PRESUPUESTO, ANNO, ID_ACTIVIDAD, FECHA_ACTIVIDAD, HORA_INICIO, HORA_FIN, PRESUPUESTO_APROBADO, PERSONAL_CITADO, ENCARGADO, ID_CIUDAD, RESUMEN_ACTIVIDAD, ID_DETALLE_GENERAL);

            if (verificado == true)
            {
                ID_PROGRAMA = _programa.IdPrograma;
                Decimal ID_DETALLE_ACTIVIDAD = _programa.IdDetalleActividad;

                Ocultar(Acciones.Inicio);
                Mostrar(Acciones.Cargar);

                Cargar(ID_PRESUPUESTO, ID_PROGRAMA, ANNO, ID_PROGRAMA_GENERAL);

                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El Programa para el año " + ANNO + " fue correctamente actualizado.", Proceso.Correcto);

                PRESUPUESTO_ASIGNADO_ACTUAL = PRESUPUESTO_ASIGNADO_ACTUAL + PRESUPUESTO_APROBADO;
                HiddenField_ASIGNADO.Value = PRESUPUESTO_ASIGNADO_ACTUAL.ToString();

                Label_PresupuestoMax.Text = String.Format("{0:C}", (PRESUPUESTO_TOTAL - (PRESUPUESTO_ASIGNADO_ACTUAL + PRESUPUESTO_EJECUTADO_ACTUAL)));
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El Programa para el año " + ANNO + " no puedo ser actualizado: " + _programa.MensajeError, Proceso.Error);
            }
        }
    }

    protected void Button_CANCELAR_ACTIVIDAD_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.CancelarNuevaActividad);
        Mostrar(Acciones.CancelarNuevaActividad);
    }

    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = false;

                Panel_FORM_BOTONES.Visible = false;
                Button_NUEVAACTIVIDAD.Visible = false;
                Button_Imprimir.Visible = false;

                Panel_ResultadosBusquedaEmpresas.Visible = false;

                Panel_RESULTADOS_GRID.Visible = false;

                Panel_ArbolPrograma.Visible = false;

                Panel_InfoActividad.Visible = false;
                Panel_BotonesActividad.Visible = false;

                Panel_BOTONES_PIE.Visible = false;
                Button_NUEVAACTIVIDAD_1.Visible = false;
                Button_Imprimir_1.Visible = false;
                break;
            case Acciones.NuevaActividad:
                Button_NUEVAACTIVIDAD.Visible = false;
                Button_Imprimir.Visible = false;

                Panel_BOTONES_PIE.Visible = false;
                Button_NUEVAACTIVIDAD_1.Visible = false;
                Button_Imprimir_1.Visible = false;
                break;
            case Acciones.CancelarNuevaActividad:
                Panel_InfoActividad.Visible = false;
                Panel_BotonesActividad.Visible = false;

                Button_Imprimir.Visible = false;
                Button_Imprimir_1.Visible = false;
                break;
        }
    }

    private void Desactivar(Acciones accion)
    {
        ConfigurarAreaRseGlobal();
        switch (accion)
        {
            case Acciones.Inicio:
                DropDownList_SubPrograma.Enabled = false;

                DropDownList_IdActividad.Enabled = false;
                DropDownList_Tipo.Enabled = false;
                DropDownList_Sector.Enabled = false;
                DropDownList_EstadoActividad.Enabled = false;
                TextBox_DescripcionActividad.Enabled = false;

                TextBox_FechaActividad.Enabled = false;

                TimePicker_HoraInicioActividad.Enabled = false;
                TimePicker_HoraFinActividad.Enabled = false;

                TextBox_Resumen.Enabled = false;
                TextBox_PresupuestoAsignado.Enabled = false;
                TextBox_PersonalCitado.Enabled = false;
                DropDownList_Encargado.Enabled = false;

                DropDownList_REGIONAL.Enabled = false;
                DropDownList_DEPARTAMENTO.Enabled = false;
                DropDownList_CIUDAD.Enabled = false;

                if (HiddenField_ID_AREA.Value == Programa.Areas.GESTIONHUMANA.ToString())
                { 
                    DropDownList_BUSCAR.Enabled = false;
                    TextBox_BUSCAR.Enabled = false;
                    Button_BUSCAR.Enabled = false;
                }
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
            case Acciones.InfoEmpresa:
                Panel_INFO_ADICIONAL_MODULO.Visible = true;
                break;
            case Acciones.Cargar:

                Panel_INFO_ADICIONAL_MODULO.Visible = true;

                Panel_FORM_BOTONES.Visible = true;
                Button_Imprimir.Visible = true;

                Button_Imprimir_1.Visible = true;
                if (HiddenField_FORM_SOLO_LECTURA.Value == "NO")
                {
                    Button_NUEVAACTIVIDAD.Visible = true;

                    Panel_BOTONES_PIE.Visible = true;
                    Button_NUEVAACTIVIDAD_1.Visible = true;
                }

                Panel_ArbolPrograma.Visible = true;
                break;
            case Acciones.NuevaActividad:
                Panel_InfoActividad.Visible = true;
                Panel_BotonesActividad.Visible = true;
                break;
            case Acciones.CancelarNuevaActividad:
                Button_Imprimir.Visible = true;
                Button_Imprimir_1.Visible = true;
                if (HiddenField_FORM_SOLO_LECTURA.Value == "NO")
                {
                    Button_NUEVAACTIVIDAD.Visible = true;

                    Panel_BOTONES_PIE.Visible = true;
                    Button_NUEVAACTIVIDAD_1.Visible = true;
                }
                break;
            case Acciones.EmpresaSelecionada:
                Panel_INFO_ADICIONAL_MODULO.Visible = true;
                Panel_RESULTADOS_GRID.Visible = true;
                Panel_FORM_BOTONES.Visible = true;
                Panel_RESULTADOS_GRID.Visible = true;
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

    private void cargar_GridView_RESULTADOS_BUSQUEDA(Decimal ID_EMPRESA)
    {
        HiddenField_ID_EMPRESA.Value = ID_EMPRESA.ToString();

        Programa.Areas AREA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);

        Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaPresupuestosProgramas = _programa.ObtenerPresupuestosYProgramasPorEmpresaYArea(ID_EMPRESA, AREA);

        if (tablaPresupuestosProgramas.Rows.Count <= 0)
        {
            if (_programa.MensajeError == null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros de presupuestos y programas configurados para la empresa seleccionada.", Proceso.Advertencia);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _programa.MensajeError, Proceso.Error);
            }

            Panel_RESULTADOS_GRID.Visible = false;
            GridView_RESULTADOS_BUSQUEDA.DataSource = null;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.EmpresaSelecionada);
            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaPresupuestosProgramas;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
        }
    }

    private void CargarInformacionCliente(Decimal ID_EMPRESA)
    {
        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaCliente = _cliente.ObtenerEmpresaConIdEmpresa(ID_EMPRESA);

        DataRow fila = tablaCliente.Rows[0];

        String RAZ_SOCIAL = fila["RAZ_SOCIAL"].ToString().Trim();
        String NIT = fila["NIT_EMPRESA"].ToString().Trim() + "-" + fila["DIG_VER"].ToString().Trim();

        Mostrar(Acciones.InfoEmpresa);

        Label_INFO_ADICIONAL_MODULO.Text = "CLIENTE: " + RAZ_SOCIAL + "<br>NIT: " + NIT;
    }

    private void Cargar(Listas lista, DropDownList drop)
    {
        Decimal ID_PROGRAMA_GENERAL = Convert.ToDecimal(HiddenField_ID_PROGRAMA_GENERAL.Value);
        Programa.Areas AREA_PROGRAMA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);

        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        Programa _prog = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        regional _regional;
        DataTable tablaRegionales;
        ListItem item;

        switch (lista)
        {
            case Listas.SubProgramas:
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
                        drop.Items.Add(new ListItem(fila["NOMBRE_SUB_PROGRAMA"].ToString().Trim(), ID_DETALLE_GENERAL.ToString() + ":" + ID_SUB_PROGRAMA.ToString()));
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
            case Listas.RegionalesConRestriccion:
                drop.Items.Clear();
                _regional = new regional(Session["idEmpresa"].ToString());
                tablaRegionales = _regional.ObtenerTodasLasRegionalesConRestriccion(ID_EMPRESA, Session["USU_LOG"].ToString());

                item = new ListItem("Seleccione...", "");
                drop.Items.Add(item);

                foreach (DataRow fila in tablaRegionales.Rows)
                {
                    item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_REGIONAL"].ToString());
                    drop.Items.Add(item);
                }

                drop.DataBind();
                break;
            case Listas.Regionales:
                drop.Items.Clear();
                _regional = new regional(Session["idEmpresa"].ToString());
                tablaRegionales = _regional.ObtenerTodasLasRegionales();

                item = new ListItem("Seleccione...", "");
                drop.Items.Add(item);

                foreach (DataRow fila in tablaRegionales.Rows)
                {
                    item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_REGIONAL"].ToString());
                    drop.Items.Add(item);
                }

                drop.DataBind();
                break;
            case Listas.Encargados:

                DataTable tablaEncargados = _prog.ObtenerUsuariosSistemaActivos();

                drop.Items.Add(new ListItem("Seleccione...",""));

                foreach (DataRow fila in tablaEncargados.Rows)
                {
                    item = new ListItem(fila["NOMBRE_USUARIO"].ToString(), fila["USU_LOG"].ToString());
                    drop.Items.Add(item);
                }

                drop.DataBind();
                break;
        }
    }

    private void CargarValidadorPresupuesto()
    {
        Decimal ID_PRESUPUESTO = Convert.ToDecimal(HiddenField_ID_PRESUPUESTO.Value);

        presupuesto _presupuesto = new presupuesto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaPresupuesto = _presupuesto.ObtenerPresupuestoPorId(ID_PRESUPUESTO);

        DataRow fila = tablaPresupuesto.Rows[0];

        RangeValidator_TextBox_PresupuestoAsignado.MinimumValue = "0";
        RangeValidator_TextBox_PresupuestoAsignado.MaximumValue = Convert.ToDecimal(fila["PRESUPUESTO"]).ToString();
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
        item = new ListItem("Código de Cliente", "COD_EMPRESA");
        DropDownList_BUSCAR.Items.Add(item);
        item = new ListItem("Razón social", "RAZ_SOCIAL");
        DropDownList_BUSCAR.Items.Add(item);
        DropDownList_BUSCAR.DataBind();
    }

    private void iniciar_seccion_de_busqueda()
    {
        TextBox_BUSCAR.Text = "";
        cargar_DropDownList_BUSCAR();
    }

    private void CargarValidadorPersonalCitado()
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaDatos = _cliente.ObtenerNumEmpleadosActivosPorIdEmpresa(ID_EMPRESA, "S", "S");

        if (tablaDatos.Rows.Count <= 0)
        {
            if (_cliente.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se pudo determinar el personal activo actual de la empresa, no se realizará el control de personal citado.", Proceso.Advertencia);
            }

            Label_PersonalCitadoMaximo.Visible = false;
            RangeValidator_TextBox_PersonalCitado.Enabled = false;
            ValidatorCalloutExtender_TextBox_PersonalCitado_1.Enabled = false;

        }
        else
        {
            Int32 contadorPersonalActivo = Convert.ToInt32(tablaDatos.Rows[0]["NUM_EMPLEADOS"]);

            if (contadorPersonalActivo <= 0)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La empresa no tiene personal activo, no se realizará el control de personal citado.", Proceso.Advertencia);
                
                Label_PersonalCitadoMaximo.Visible = false;
                RangeValidator_TextBox_PersonalCitado.Enabled = false;
                ValidatorCalloutExtender_TextBox_PersonalCitado_1.Enabled = false;
            }
            else
            {
                Label_PersonalCitadoMaximo.Visible = true;
                Label_PersonalCitadoMaximo.Text = contadorPersonalActivo.ToString();
                RangeValidator_TextBox_PersonalCitado.Enabled = true;
                ValidatorCalloutExtender_TextBox_PersonalCitado_1.Enabled = true;

                RangeValidator_TextBox_PersonalCitado.MinimumValue = "1";
                RangeValidator_TextBox_PersonalCitado.MaximumValue = contadorPersonalActivo.ToString();
            }
        }
    }

    private void Cargar(Acciones accion)
    {
        tools _tools = new tools();

        switch (accion)
        {
            case Acciones.Inicio:
                ConfigurarAreaRseGlobal();

                configurarCaracteresAceptadosBusqueda(false, false);

                iniciar_seccion_de_busqueda();

                if (HiddenField_ID_AREA.Value == Programa.Areas.GESTIONHUMANA.ToString())
                {
                    Decimal ID_EMPRESA = _tools.ObtenerIdEmpleadorPorSession(Convert.ToInt32(Session["idEmpresa"]));

                    cargar_GridView_RESULTADOS_BUSQUEDA(ID_EMPRESA);

                    CargarInformacionCliente(ID_EMPRESA);
                }
                break;
            case Acciones.NuevaActividad:
                Cargar(Listas.SubProgramas, DropDownList_SubPrograma);
                Cargar(Listas.RegionalesConRestriccion, DropDownList_REGIONAL);
                Cargar(Listas.Encargados, DropDownList_Encargado);

                CargarValidadorPresupuesto();

                CargarValidadorPersonalCitado();
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
        Title = "ADMINIASTRACIÓN PROGRAMA ESPECÍFICO";
        if (IsPostBack == false)
        {
            Iniciar();
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
            }

            TextBox_BUSCAR.Focus();

        }

        TextBox_BUSCAR.Text = "";
    }
    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        Panel_RESULTADOS_GRID.Visible = false;
        Panel_ArbolPrograma.Visible = false;
        Panel_InfoActividad.Visible = false;
        Panel_BOTONES_PIE.Visible = false;

        String datosCapturados = TextBox_BUSCAR.Text.ToUpper();
        String campo = DropDownList_BUSCAR.SelectedValue.ToString();

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

            Panel_ResultadosBusquedaEmpresas.Visible = false;
        }
        else
        {
            Panel_ResultadosBusquedaEmpresas.Visible = true;
            GridView_EmpresasEncontradas.DataSource = tablaResultadosBusqueda;
            GridView_EmpresasEncontradas.DataBind();
        }
    }

    protected void GridView_EmpresasEncontradas_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_EmpresasEncontradas.PageIndex = e.NewPageIndex;

        String datosCapturados = TextBox_BUSCAR.Text.ToUpper();
        String campo = DropDownList_BUSCAR.SelectedValue.ToString();

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
            Panel_RESULTADOS_GRID.Enabled = true;
            Panel_RESULTADOS_GRID.Visible = true;
            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaResultadosBusqueda;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
        }
    }

    protected void GridView_EmpresasEncontradas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal ID_EMPRESA = Convert.ToDecimal(GridView_EmpresasEncontradas.DataKeys[indexSeleccionado].Values["ID"]);

            ConfigurarAreaRseGlobal();

            cargar_GridView_RESULTADOS_BUSQUEDA(ID_EMPRESA);

            CargarInformacionCliente(ID_EMPRESA);
        }
    }

    private byte[] ImprimirPdfPrograma(Decimal ID_PROGRAMA)
    {
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
            using (SqlCommand comando = new SqlCommand("RPT_PROGRAMAS_COMPLEMENTARIOS_PDF_PROGRAMA " + ID_PROGRAMA.ToString(), conn))
            {
                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    DataSet ds = new DataSet();
                    adaptador.Fill(ds);

                    reporte = new ReportDocument();
                    reporte.Load(Server.MapPath("~/Reportes/ProgramasComplementarios/RPT_PROGRAMAS_COMPLEMENTARIOS_PDF_PROGRAMA.rpt"));
                    reporte.SetDataSource(ds.Tables[0]);
                    reporte.DataSourceConnections[0].SetConnection(server, db, user, pass);

                    using (var mStream = (MemoryStream)reporte.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat))
                    {
                        return mStream.ToArray();
                    }
                } 
            }
        } 
        catch (Exception ex)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, ex.Message, Proceso.Error);
            return null;
        }
        finally
        {
            if (conn.State != ConnectionState.Closed)
                conn.Close();
            conn.Dispose();
        }
    }

    protected void Button_Imprimir_Click(object sender, EventArgs e)
    {
        Decimal ID_PROGRAMA = Convert.ToDecimal(HiddenField_ID_PROGRAMA.Value);
        byte[] archivo = ImprimirPdfPrograma(ID_PROGRAMA);

        Response.AddHeader("Content-Disposition", "attachment;FileName=Programa_" + ID_PROGRAMA.ToString() + ".pdf");
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(archivo);
        Response.End();
    }
}