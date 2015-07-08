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
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.programasRseGlobal;

public partial class _Default : System.Web.UI.Page
{


    protected void Page_Load(object sender, EventArgs e)
    {
       Page.Header.Title = "HOJA DE TRABAJO";
       Panel_FORM_BOTONES.Visible = false;


       if (IsPostBack == false)
       {
           Configurar();

           iniciar_seccion_de_busqueda();
           HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "SIN_FILTRO";
           HiddenField_FILTRO_DROP.Value = String.Empty;
           HiddenField_FILTRO_DATO.Value = String.Empty;
           HiddenField_PAGINA_GRID.Value = "0";

           tools _tools = new tools();
           SecureQueryString QueryStringSeguro;
           QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

           String accion = QueryStringSeguro["accion"].ToString();

           if (accion == "inicial")
           {
               iniciar_interfaz_inicial();
           }
       }
    }
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
    private System.Drawing.Color colorNo = System.Drawing.ColorTranslator.FromHtml("#F8523A");
    private System.Drawing.Color colorMedio = System.Drawing.ColorTranslator.FromHtml("#FFF200");
    private System.Drawing.Color colorSi = System.Drawing.ColorTranslator.FromHtml("#50CE04");

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
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

    #region CARGAR DROP Y DRID

    private void cargar_GridView_HOJA_DE_TRABAJO()
    {
        GridView_HOJA_DE_TRABAJO.PageIndex = Convert.ToInt32(HiddenField_PAGINA_GRID.Value);

        requisicion _requisicion = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaRequisicionesHojaTrabajoOriginal = new DataTable();

        if (HiddenField_TIPO_BUSQUEDA_ACTUAL.Value == "SIN_FILTRO")
        {
            tablaRequisicionesHojaTrabajoOriginal = _requisicion.ObtenerTablaRequerimientosUsuario();
        }
        else
        {
            if (HiddenField_FILTRO_DROP.Value == "ID_REGIONAL")
            {
                tablaRequisicionesHojaTrabajoOriginal = _requisicion.ObtenerComRequerimientoPorUsuLogFiltroRegional(HiddenField_FILTRO_DATO.Value);
            }
            else
            {
                if (HiddenField_FILTRO_DROP.Value == "ID_CIUDAD")
                {
                    tablaRequisicionesHojaTrabajoOriginal = _requisicion.ObtenerComRequerimientoPorUsuLogFiltroCiudad(HiddenField_FILTRO_DATO.Value);
                }
                else
                {
                    if (HiddenField_FILTRO_DROP.Value == "ID_CLIENTE")
                    {
                        tablaRequisicionesHojaTrabajoOriginal = _requisicion.ObtenerComRequerimientoPorUsuLogFiltroCliente(HiddenField_FILTRO_DATO.Value);
                    }
                    else
                    {
                        if (HiddenField_FILTRO_DROP.Value == "ID_REQUERIMIENTO")
                        {
                            tablaRequisicionesHojaTrabajoOriginal = _requisicion.ObtenerComRequerimientoPorUsuLogFiltroREQ(HiddenField_FILTRO_DATO.Value);
                        }
                    }
                }
            }
        }

        tablaRequisicionesHojaTrabajoOriginal.AcceptChanges();

        DataRow filaTablaRequisicionesCreada = tablaRequisicionesHojaTrabajoOriginal.NewRow();

        filaTablaRequisicionesCreada["NUMERACION"] = DBNull.Value;
        filaTablaRequisicionesCreada["ID_REQUERIMIENTO"] = DBNull.Value;
        filaTablaRequisicionesCreada["COD_EMPRESA"] = DBNull.Value;
        filaTablaRequisicionesCreada["RAZ_SOCIAL"] = DBNull.Value;
        filaTablaRequisicionesCreada["TIPO_REQ"] = DBNull.Value;
        filaTablaRequisicionesCreada["FECHA_REQUERIDA"] = DBNull.Value;
        filaTablaRequisicionesCreada["COD_OCUPACION"] = DBNull.Value;
        filaTablaRequisicionesCreada["NOM_OCUPACION"] = DBNull.Value;
        filaTablaRequisicionesCreada["CANTIDAD"] = tablaRequisicionesHojaTrabajoOriginal.Compute("SUM(CANTIDAD)", null);
        filaTablaRequisicionesCreada["ENVIADOS"] = tablaRequisicionesHojaTrabajoOriginal.Compute("SUM(ENVIADOS)", null);
        filaTablaRequisicionesCreada["CONTRATAR"] = tablaRequisicionesHojaTrabajoOriginal.Compute("SUM(CONTRATAR)", null);
        filaTablaRequisicionesCreada["FALTAN"] = tablaRequisicionesHojaTrabajoOriginal.Compute("SUM(FALTAN)", null);
        filaTablaRequisicionesCreada["ALERTA"] = "NINGUNA";
        filaTablaRequisicionesCreada["FECHA_R"] = DBNull.Value;
        filaTablaRequisicionesCreada["ESTADO"] = DBNull.Value;
        filaTablaRequisicionesCreada["CUMPLIDO"] = DBNull.Value;
        filaTablaRequisicionesCreada["CANCELADO"] = DBNull.Value;
        filaTablaRequisicionesCreada["USU_CRE"] = DBNull.Value;

        tablaRequisicionesHojaTrabajoOriginal.Rows.Add(filaTablaRequisicionesCreada);

        tablaRequisicionesHojaTrabajoOriginal.AcceptChanges();

        GridView_HOJA_DE_TRABAJO.DataSource = tablaRequisicionesHojaTrabajoOriginal;
        GridView_HOJA_DE_TRABAJO.DataBind();

        DataRow filaParaColocarColor;
        int contadorAlertasBajas = 0;
        int contadorAlertasMedias = 0;
        int contadorAlertasAltas = 0;
        int contadorAlertasNinguna = 0;

        for (int i = 0; i < tablaRequisicionesHojaTrabajoOriginal.Rows.Count; i++)
        {

            filaParaColocarColor = tablaRequisicionesHojaTrabajoOriginal.Rows[i];

            if (filaParaColocarColor["ALERTA"].ToString().Trim() == "ALTA")
            {
                GridView_HOJA_DE_TRABAJO.Rows[i].Cells[0].BackColor = colorNo;
                contadorAlertasAltas += 1;
            }
            else
            {
                if (filaParaColocarColor["ALERTA"].ToString().Trim() == "MEDIA")
                {
                    GridView_HOJA_DE_TRABAJO.Rows[i].Cells[0].BackColor = colorMedio;
                    contadorAlertasMedias += 1;
                }
                else
                {
                    if (filaParaColocarColor["ALERTA"].ToString().Trim() == "BAJA")
                    {
                        GridView_HOJA_DE_TRABAJO.Rows[i].Cells[0].BackColor = colorSi;
                        contadorAlertasBajas += 1;
                    }
                    else
                    {
                        GridView_HOJA_DE_TRABAJO.Rows[i].Cells[0].BackColor = System.Drawing.Color.Gray;
                        contadorAlertasNinguna += 1;
                    }
                }
            }
        }
        for (int j = 0; j < GridView_HOJA_DE_TRABAJO.Rows.Count; j++)
        {
            GridViewRow filaGrilla = GridView_HOJA_DE_TRABAJO.Rows[j];
            String TXT_SI_NO = filaGrilla.Cells[16].Text;

            if (TXT_SI_NO != "&nbsp;")
            {
                GridView_HOJA_DE_TRABAJO.Rows[j].Cells[17].Enabled = false;
                GridView_HOJA_DE_TRABAJO.Rows[j].Cells[18].Enabled = true;
            }
        }

        Label_ALERTA_ALTA.Text = contadorAlertasAltas.ToString();
        Label_ALERTA_MEDIA.Text = contadorAlertasMedias.ToString();
        Label_ALERTA_BAJA.Text = contadorAlertasBajas.ToString();
    }
    #endregion

    #region metodos que se cargan al inciar la pagina

    private void CargarGridAgendaPsicologo(String @Empresa, String @Psicologo, String @Fecha)
    {
        Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable PanelReclutador = ConstructorReclutamiento.CargarAgendaDelPsicologo(Session["idEmpresa"].ToString());
        GridViewAgendaPsicologoPrincipal.DataSource = PanelReclutador;
        GridViewAgendaPsicologoPrincipal.DataBind();

        for (int j = 0; j < GridViewAgendaPsicologoPrincipal.Rows.Count; j++)
        {
           Decimal Valor = Convert.ToDecimal(GridViewAgendaPsicologoPrincipal.DataKeys[j].Values["HORA"].ToString());

           GridView GrillaInterna = GridViewAgendaPsicologoPrincipal.Rows[j].FindControl("GridViewAgendaPsicologoListaContactos") as GridView;
            DataTable PanelReclutadorInterna = ConstructorReclutamiento.AgendaDeContactosEspecial(Session["idEmpresa"].ToString(), Convert.ToString(Valor),DropDownListPsicologo.SelectedValue, TextBoxFechaAgenda.Text);
            GrillaInterna.DataSource = PanelReclutadorInterna;
            GrillaInterna.DataBind();
        }
    }

    private void CargarGridHojaTrabajoReclutador()
    {
        Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable PanelReclutador = ConstructorReclutamiento.CargarGridReclutador(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        GridViewHojaDeTrabajoReclutador.DataSource = PanelReclutador;
        GridViewHojaDeTrabajoReclutador.DataBind();

        for (int j = 0; j < GridViewHojaDeTrabajoReclutador.Rows.Count; j++)
        {
            GridViewRow filaGrilla = GridViewHojaDeTrabajoReclutador.Rows[j];
            int CantidadSolicitada = Convert.ToInt32(filaGrilla.Cells[5].Text);
            int CantidadCitada = Convert.ToInt32(filaGrilla.Cells[10].Text);
            DateTime FECHA_REQUERIDA = Convert.ToDateTime(filaGrilla.Cells[8].Text);


            if (CantidadSolicitada <= CantidadCitada)
            {
                GridViewHojaDeTrabajoReclutador.Rows[j].Cells[0].BackColor = colorSi;
            }
            else
                if (CantidadSolicitada > CantidadCitada)
                {
                    if (FECHA_REQUERIDA < DateTime.Now)
                    {
                        GridViewHojaDeTrabajoReclutador.Rows[j].Cells[0].BackColor = colorNo;
                    }
                }
        }

    }
    private void FormularioPrincipal()
    {
        this.Panel_BOTONES_INTERNOS.Visible = true;
        Hoja_de_Trabajo_Coordinador.Visible = false;
        Hoja_De_Trabajo_Reclutador.Visible = false;
        HojaDeTrabajoRecepcion.Visible = false;
        this.panelPsicologo.Visible = false;

        AspPanleResumenRequerimiento.Visible = false;
        PanelListaDeCandidatos.Visible = false;
        panelAgendarCandidato.Visible = false;
        Panel_FORM_BOTONES.Visible = false;
        GridViewSeguimientoRecepcion.Visible = false;
        AspPanelPorProductividad.Visible = false;
    }
    private void FormularioCoordinador()
    {
        Panel_BOTONES_INTERNOS.Visible = true;
        Hoja_de_Trabajo_Coordinador.Visible =true;
        Hoja_De_Trabajo_Reclutador.Visible = false;
        HojaDeTrabajoRecepcion.Visible = false;
        Panel_FORM_BOTONES.Visible = true;
        GridView_HOJA_DE_TRABAJO.Visible = true;
        GridViewHojaDeTrabajoReclutador.Visible = false;
        GridViewSeguimientoRecepcion.Visible = false;
        this.panelPsicologo.Visible = false;

        AspPanelPorProductividad.Visible = true;
    }
    private void FormularioReclutador()
    {
        Panel_BOTONES_INTERNOS.Visible = true;
        Hoja_de_Trabajo_Coordinador.Visible = false;
        Hoja_De_Trabajo_Reclutador.Visible = true;
        HojaDeTrabajoRecepcion.Visible = false;
        Panel_FORM_BOTONES.Visible = false;
        this.panelPsicologo.Visible = false;
        GridView_HOJA_DE_TRABAJO.Visible = false;
        GridViewHojaDeTrabajoReclutador.Visible = true;
        GridViewSeguimientoRecepcion.Visible = false;
        AspPanelPorProductividad.Visible = false;

        AspPanleResumenRequerimiento.Visible = false;
        PanelListaDeCandidatos.Visible = false;
        

    }
    private void Formulariorecepcion()
    {
        Panel_FORM_BOTONES.Visible = false;
        Panel_BOTONES_INTERNOS.Visible = true;
        Hoja_de_Trabajo_Coordinador.Visible = false;
        Hoja_De_Trabajo_Reclutador.Visible = false;
        HojaDeTrabajoRecepcion.Visible = true;
        AspPanelPorProductividad.Visible = false;
        this.panelPsicologo.Visible = false;

        GridView_HOJA_DE_TRABAJO.Visible = false;
        GridViewHojaDeTrabajoReclutador.Visible = false;
        GridViewSeguimientoRecepcion.Visible = false;

        AspPanleResumenRequerimiento.Visible = false;
        PanelListaDeCandidatos.Visible = false;

    }
    private void FormularioPsicologo()
    {
        BotonesPrincipales.Visible = true;
        panelAgendarCandidato.Visible = false;
        Panel_FORM_BOTONES.Visible = false;
        Panel_BOTONES_INTERNOS.Visible = true;
        Hoja_de_Trabajo_Coordinador.Visible = false;
        Hoja_De_Trabajo_Reclutador.Visible = false;
        HojaDeTrabajoRecepcion.Visible = false;
        AspPanelPorProductividad.Visible = false;
        this.panelPsicologo.Visible = true;

        GridView_HOJA_DE_TRABAJO.Visible = false;
        GridViewHojaDeTrabajoReclutador.Visible = false;
        GridViewSeguimientoRecepcion.Visible = false;

        AspPanleResumenRequerimiento.Visible = false;
        PanelListaDeCandidatos.Visible = false;
    }
    private void iniciar_interfaz_inicial()
    {
        
        GridView_HOJA_DE_TRABAJO.Visible = false;
        Reporte reporte = new Reporte(Session["idEmpresa"].ToString());
        Cargar(DropDownListEmpresa, reporte.ListarClientesIdEmpresa(), "Seleccione un cliente...");
        Cargar(DropDownListReclutador, reporte.ListarReclutadores(), "Seleccione un reclutador...");
        Cargar(ProductividadEmpresa, reporte.ListarClientesIdEmpresa(), "Seleccione un cliente...");
        Cargar(DropDownListCargo, reporte.ListarCargos(DropDownListEmpresa.SelectedValue), "Seleccione un cargo...");
        Cargar(DropDownList1regional, reporte.ListarRegionales(), "Seleccione un regional...");
        Cargar(DropDownListCiudad, reporte.ListarCiudades(), "Seleccione un ciudad...");
        Cargar(DropDownListPsicologo, reporte.ListarPsicologos_RECLUTAMIENTO(), "Seleccione un Psicologo...");
        Cargar(DropDownListPsicologoForm, reporte.ListarPsicologos_RECLUTAMIENTO(), "Seleccione un Psicologo...");
        Cargar(ProductividadReclutador, reporte.ListarReclutadores(), "Seleccione un reclutador...");
        Cargar(ProductividadCargo, reporte.ListarCargos(DropDownListEmpresa.SelectedValue), "Seleccione un cargo...");
        Cargar(ProductividadRegional, reporte.ListarRegionales(), "Seleccione un regional...");

        FormularioPrincipal();




        AgendarContacto.Enabled = false;
    }
    #endregion



    private void Cargar(DropDownList dropDownList, DataTable dataTable, String _seleccione)
    {
        dropDownList.Items.Clear();
        ListItem listItem;
        listItem = new ListItem(_seleccione, "0");
        dropDownList.Items.Add(listItem);
        foreach (DataRow dataRow in dataTable.Rows)
        {
            listItem = new ListItem(dataRow["nombre"].ToString(), dataRow["id"].ToString());
            dropDownList.Items.Add(listItem);
        }
        dropDownList.DataBind();
    }
    private void iniciar_seccion_de_busqueda()
    {
        TextBox_BUSCAR.Text = "";
        cargar_DropDownList_BUSCAR();
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


    protected void GridView_HOJA_DE_TRABAJO_SelectedIndexChanged(object sender, EventArgs e)
    {

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
    public void TextBox_BUSCAR_TextChanged(object sender, EventArgs e)
    {

    }
    public void DropDownListEmpresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reporte reporte = new Reporte(Session["idEmpresa"].ToString());
    }
    public void GrabarSolicitudPorProductividad_Click(object sender, EventArgs e)
    {
        AsignarUsuarioAccion("GUARDAR");
        GridViewSeguimientoReclutamiento_Cargar();
    }
    protected void BtnEditar_Click(object sender, EventArgs e)
    {
        AsignarUsuarioAccion("EDITAR");
    }
    protected void BtnBorrarRequicicion_Click(object sender, EventArgs e)
    {
        AsignarUsuarioAccion("ELIMINAR");
    }
    private void AsignarUsuarioAccion(String @Accion)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE, Panel_MENSAJES);
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);
        Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        ConstructorReclutamiento.Accion = @Accion;
        ConstructorReclutamiento.Id_Empresa = this.DropDownListEmpresa.SelectedValue;
        ConstructorReclutamiento.Id_Requerimiento = this.TextBoxIdRequerimiento.Text;
        ConstructorReclutamiento.Id_usuario_Asignado = this.DropDownListReclutador.SelectedValue;
        ConstructorReclutamiento.Cantidad = this.TextBoxCantidadSolicitada.Text;
        ConstructorReclutamiento.Fecha_Requerida = this.TextBoxFechaRequerida.Text;
        ConstructorReclutamiento.Fecha_Ingreso_Solicitud = Convert.ToString(DateTime.Today);
        ConstructorReclutamiento.Descripcion = this.TextBoxDescripcion.Text;
        ConstructorReclutamiento.Empresa = Session["idEmpresa"].ToString();
        ConstructorReclutamiento.Usuario_Crea_Solicitud = Session["USU_LOG"].ToString();
        ConstructorReclutamiento.Regional = this.DropDownList1regional.SelectedValue;
        ConstructorReclutamiento.Ciudad = DropDownListCiudad.SelectedValue;
        ConstructorReclutamiento.Cargo = this.DropDownListCargo.SelectedValue;
        ConstructorReclutamiento.Tipo_Requerimietno = "REQUERIMIENTO";
        ConstructorReclutamiento.Id_solicitud = HiddenField_ID_SOLICITUD.Value.ToString();
        ConstructorReclutamiento.Id_Usuario_Reclutador= HiddenField_Reclutador_Temporal.Value.ToString();

        ConstructorReclutamiento.SolicitudAccion();
        ACCIONINTERNA("LIMPIAR");
        AsignacionReclutador.Visible = false;
        cargar_GridView_HOJA_DE_TRABAJO();
    }
    private void ACCIONINTERNA(String Accion)
    {
        if (Accion == "LIMPIAR")
        {
            TextBoxCantidadSolicitada.Text = "";
            TextBoxFechaRequerida.Text = "";
            DropDownListReclutador.ClearSelection();
            TextBoxDescripcion.Text = "";
            DropDownListEmpresa.ClearSelection();
            DropDownListCargo.ClearSelection();
            DropDownList1regional.ClearSelection();
            DropDownListCiudad.ClearSelection();
            TextBoxDocumento.Text = "";
            TextBoxApellido.Text = "";
        }

    }
    protected void ButtonCoordinador_Click(object sender, EventArgs e)
    {
        FormularioCoordinador();
        GridViewSeguimiento_Productividad_Cargar();
        cargar_GridView_HOJA_DE_TRABAJO();
        GridViewSeguimientoReclutamiento_Cargar();
    }
    protected void ButtonReclutador_Click(object sender, EventArgs e)
    {
        CargarGridHojaTrabajoReclutador();
        FormularioReclutador();
    }
    protected void GridView_HOJA_DE_TRABAJO_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int filaSeleccionada = Convert.ToInt32(e.CommandArgument);
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);
        Decimal ID_LIQ_NOMINA_EMPLEADOS = Convert.ToDecimal(GridView_HOJA_DE_TRABAJO.DataKeys[filaSeleccionada].Values["ID_REQUERIMIENTO"]);
        String Filtro = Session["idEmpresa"].ToString();
        if (e.CommandName == "Asignar")
        {
            BtnEditar.Visible = false;
            BtnBorrarRequicicion.Visible = false;
            GrabarSolicitudPorProductividad.Visible = true;
            Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable ConceptoGVT = ConstructorReclutamiento.FiltroPorRequerimiento(Session["idEmpresa"].ToString(), Convert.ToString(ID_LIQ_NOMINA_EMPLEADOS));
            DropDownListEmpresa.Enabled = false;
            DropDownListCargo.Enabled = false;
            DropDownList1regional.Enabled = false;
            DropDownListCiudad.Enabled = false;
            DropDownListEmpresa.SelectedItem.Text = ConceptoGVT.Rows[0]["RAZ_SOCIAL"].ToString();
            DropDownListEmpresa.SelectedValue = ConceptoGVT.Rows[0]["ID_EMPRESA"].ToString();
            DropDownListCargo.SelectedItem.Text = ConceptoGVT.Rows[0]["NOM_OCUPACION"].ToString();
            DropDownListCargo.SelectedValue = ConceptoGVT.Rows[0]["ID_OCUPACION"].ToString();
            DropDownList1regional.SelectedItem.Text = ConceptoGVT.Rows[0]["NOMBRE_REGIONAL"].ToString();
            DropDownList1regional.SelectedValue = ConceptoGVT.Rows[0]["ID_REGIONAL"].ToString();
            DropDownListCiudad.SelectedItem.Text = ConceptoGVT.Rows[0]["NOMBRE_CIUDAD"].ToString();
            DropDownListCiudad.SelectedValue = ConceptoGVT.Rows[0]["ID_CIUDAD"].ToString();

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Asigne un usuario reclutador que atenderá la requisición número" + ID_LIQ_NOMINA_EMPLEADOS + ".", Proceso.Correcto);
            AsignacionReclutador.Visible = true;
            TextBoxIdRequerimiento.Text = Convert.ToString(ID_LIQ_NOMINA_EMPLEADOS);
            TextBoxIdRequerimiento.Enabled = false;
            TextBoxCantidadSolicitada.Focus();
            PanelSeguimientoAsistencia.Visible = false;
        }
        if (e.CommandName == "Modificar")
        {
            BtnEditar.Visible = true;
            BtnBorrarRequicicion.Visible = true;
            GrabarSolicitudPorProductividad.Visible = false;
            Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable ConceptoGVT = ConstructorReclutamiento.FiltroPorRequerimiento(Session["idEmpresa"].ToString(), Convert.ToString(ID_LIQ_NOMINA_EMPLEADOS));
            DropDownListEmpresa.Enabled = false;
            DropDownListCargo.Enabled = false;
            DropDownList1regional.Enabled = false;
            DropDownListCiudad.Enabled = false;
            DropDownListEmpresa.SelectedItem.Text = ConceptoGVT.Rows[0]["RAZ_SOCIAL"].ToString();
            DropDownListEmpresa.SelectedValue = ConceptoGVT.Rows[0]["ID_EMPRESA"].ToString();
            DropDownListCargo.SelectedItem.Text = ConceptoGVT.Rows[0]["NOM_OCUPACION"].ToString();
            DropDownListCargo.SelectedValue = ConceptoGVT.Rows[0]["ID_OCUPACION"].ToString();
            DropDownList1regional.SelectedItem.Text = ConceptoGVT.Rows[0]["NOMBRE_REGIONAL"].ToString();
            DropDownList1regional.SelectedValue = ConceptoGVT.Rows[0]["ID_REGIONAL"].ToString();
            DropDownListCiudad.SelectedItem.Text = ConceptoGVT.Rows[0]["NOMBRE_CIUDAD"].ToString();
            DropDownListCiudad.SelectedValue = ConceptoGVT.Rows[0]["ID_CIUDAD"].ToString();
            HiddenField_ID_SOLICITUD.Value = ConceptoGVT.Rows[0]["Id_solicitud"].ToString();
            HiddenField_Reclutador_Temporal.Value = ConceptoGVT.Rows[0]["Id_usuario_Asignado"].ToString();
            TextBoxCantidadSolicitada.Text = ConceptoGVT.Rows[0]["Cantidad"].ToString();
            TextBoxFechaRequerida.Text = ConceptoGVT.Rows[0]["Fecha_Requerida"].ToString();

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Asigne un usuario reclutador que atenderá la requisición número" + ID_LIQ_NOMINA_EMPLEADOS + ".", Proceso.Correcto);
            AsignacionReclutador.Visible = true;
            TextBoxIdRequerimiento.Text = Convert.ToString(ID_LIQ_NOMINA_EMPLEADOS);
            TextBoxIdRequerimiento.Enabled = false;
            TextBoxCantidadSolicitada.Focus();
            PanelSeguimientoAsistencia.Visible = false;      
        }
    }
    protected void GridViewHojaDeTrabajoReclutador_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int filaSeleccionada = Convert.ToInt32(e.CommandArgument);
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);
        Decimal ID_LIQ_NOMINA_EMPLEADOS = Convert.ToDecimal(GridView_HOJA_DE_TRABAJO.DataKeys[filaSeleccionada].Values["ID_REQUERIMIENTO"]);
        String Filtro = Session["idEmpresa"].ToString();
        if (e.CommandName == "Agregar_Contacto")
        {
            Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable ConceptoGVT = ConstructorReclutamiento.FiltroPorRequerimiento(Session["idEmpresa"].ToString(), Convert.ToString(ID_LIQ_NOMINA_EMPLEADOS));
            this.TextBoxResumenSolicitud.Enabled = false;
            TextBoxResumenRequerimiento.Enabled = false;
            TextBoxResumenCantidad.Enabled = false;
            TextBoxRequerimientoCliente.Enabled = false;

            TextBoxResumenCargo.Enabled = false;
            lbl_DescripcionResumenRequerimiento.Enabled = false;

            DropDownListEmpresa.SelectedItem.Text = ConceptoGVT.Rows[0]["RAZ_SOCIAL"].ToString();
            DropDownListEmpresa.SelectedValue = ConceptoGVT.Rows[0]["ID_EMPRESA"].ToString();
            DropDownListCargo.SelectedItem.Text = ConceptoGVT.Rows[0]["NOM_OCUPACION"].ToString();
            DropDownListCargo.SelectedValue = ConceptoGVT.Rows[0]["ID_OCUPACION"].ToString();
            DropDownList1regional.SelectedItem.Text = ConceptoGVT.Rows[0]["NOMBRE_REGIONAL"].ToString();
            DropDownList1regional.SelectedValue = ConceptoGVT.Rows[0]["ID_REGIONAL"].ToString();
            DropDownListCiudad.SelectedItem.Text = ConceptoGVT.Rows[0]["NOMBRE_CIUDAD"].ToString();
            DropDownListCiudad.SelectedValue = ConceptoGVT.Rows[0]["ID_CIUDAD"].ToString();

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Asigne un usuario reclutador que atenderá la requisición número" + ID_LIQ_NOMINA_EMPLEADOS + ".", Proceso.Correcto);
            AsignacionReclutador.Visible = true;
            TextBoxIdRequerimiento.Text = Convert.ToString(ID_LIQ_NOMINA_EMPLEADOS);
            TextBoxIdRequerimiento.Enabled = false;
            TextBoxCantidadSolicitada.Focus();
        }
    }
    protected void GridViewHojaDeTrabajoReclutador_RowCommand1(object sender, GridViewCommandEventArgs e)
    {

        int filaSeleccionada = Convert.ToInt32(e.CommandArgument);
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);
        Decimal Id_Solicitud = Convert.ToDecimal(GridViewHojaDeTrabajoReclutador.DataKeys[filaSeleccionada].Values["id_solicitud"]);
        String Filtro = Session["idEmpresa"].ToString();
        if (e.CommandName == "Agregar_Contacto")
        {
            Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable ConceptoGVT = ConstructorReclutamiento.FiltroPorRequerimientoResumen(Session["idEmpresa"].ToString(), Convert.ToString(Id_Solicitud));

            AspPanleResumenRequerimiento.Visible = true;
            PanelListaDeCandidatos.Visible = true;
            panelAgendarCandidato.Visible = false;
  
            TextBoxResumenSolicitud.Text = ConceptoGVT.Rows[0]["Id_solicitud"].ToString();
            TextBoxResumenRequerimiento.Text = ConceptoGVT.Rows[0]["Id_Requerimiento"].ToString();
            TextBoxResumenCantidad.Text = ConceptoGVT.Rows[0]["Cantidad"].ToString();
            TextBoxResumenCargo.Text = ConceptoGVT.Rows[0]["NOM_OCUPACION"].ToString();
            TextBoxRequerimientoCliente.Text = ConceptoGVT.Rows[0]["RAZ_SOCIAL"].ToString();
            LabelRegional.Text = ConceptoGVT.Rows[0]["Regional_Nombre"].ToString();
            LabelCiudad.Text = ConceptoGVT.Rows[0]["Ciudad_Nombre"].ToString();
            lbl_DescripcionResumenRequerimiento.Text = ConceptoGVT.Rows[0]["Descripcion"].ToString();

            AsignacionReclutador.Visible = false;
            TextBoxIdRequerimiento.Text = Convert.ToString(Id_Solicitud);
            TextBoxIdRequerimiento.Enabled = false;
            TextBoxCantidadSolicitada.Focus();
            GridViewListarContactosPorSolicitudReclutador(TextBoxResumenSolicitud.Text);
            GridViewListarContactosPorSolicitud.Visible = true;
        }
    }



    public void btn_Agrregar_contacto_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE, Panel_MENSAJES);
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);
        Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        String @ACCION = "GUARDAR";
        String @Id_solicitud = this.TextBoxResumenSolicitud.Text;
        String @Apellido = this.TextBoxApellido.Text;
        String @Nombre = this.TextBoxApellido.Text;
        String @Docuemnto = this.TextBoxDocumento.Text;
        String @Telefono = this.TextBoxTelefono.Text;
        String @TextBoxResumenCargo = this.TextBoxResumenCargo.Text;
        String @FechaContacto = Convert.ToString(DateTime.Today);

        ConstructorReclutamiento.SolicitudAccionListarContactos(Session["idEmpresa"].ToString(), @ACCION, @Id_solicitud, @Apellido, @Nombre, @Docuemnto, @Telefono, @TextBoxResumenCargo, @FechaContacto);
        ACCIONINTERNA("LIMPIAR");
        AspPanleResumenRequerimiento.Visible = true;
        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Se agrego correctamente el Candidato.", Proceso.Correcto);
        GridViewListarContactosPorSolicitudReclutador(@Id_solicitud);
        GridViewListarContactosPorSolicitud.Visible = true;
        ACCIONINTERNA("LIMPIAR");
    }
    protected void GridViewListarContactosPorSolicitudReclutador(String @Id_Solicitud_requerimiento)
    {

        AspPanleResumenRequerimiento.Visible = true;
        GridViewListarContactosPorSolicitud.Visible = true;
        Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable PanelReclutador = ConstructorReclutamiento.CargarGrid_Ista_Contactos(Session["idEmpresa"].ToString(), @Id_Solicitud_requerimiento);
        GridViewListarContactosPorSolicitud.DataSource = PanelReclutador;
        GridViewListarContactosPorSolicitud.DataBind();

        for (int j = 0; j < GridViewListarContactosPorSolicitud.Rows.Count; j++)
        {
            GridViewRow filaGrilla = GridViewListarContactosPorSolicitud.Rows[j];
            String TXT_SI_NO = filaGrilla.Cells[7].Text;

            if (TXT_SI_NO == "SI")
            {
                filaGrilla.Enabled = false;
                GridViewListarContactosPorSolicitud.Rows[j].Cells[10].Enabled = false;
            }
            if (TXT_SI_NO == "NO")
            {
                filaGrilla.Enabled = false;
                GridViewListarContactosPorSolicitud.Rows[j].Cells[10].Enabled = false;
            }
            if (TXT_SI_NO == "&nbsp;")
            {
            }
        }
    }

    protected void AgendarContacto_Click(object sender, EventArgs e)
    {
        AspPanleResumenRequerimiento.Visible = true;
        GridViewAgendaPsicologoPrincipal.Visible = true;
        Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable PanelReclutador = ConstructorReclutamiento.CargarAgendaDelPsicologo(Session["idEmpresa"].ToString());
        CargarGridAgendaPsicologo(Session["idEmpresa"].ToString(), DropDownListPsicologo.SelectedValue, TextBoxFechaAgenda.Text);
    }
    private void AgendarCandidato()
    {
        AspPanleResumenRequerimiento.Visible = true;
        Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());


        String @Id_Registro = this.lblIdRegistro.Text;
        String @Fecha_De_Cita = this.TextBoxFechaAgenda.Text;
        String @Psicologo = this.DropDownListPsicologo.SelectedValue;
        String @Usuario_Crea_Registro = Session["USU_LOG"].ToString();
        String @Hora = ""; 
        String @AceptaOferta = this.RadioButtonList1.SelectedValue;

        ConstructorReclutamiento.AgendarContactoPorFecha(Session["idEmpresa"].ToString(), @Id_Registro, @Fecha_De_Cita, @Psicologo, @Usuario_Crea_Registro, @Hora, @AceptaOferta);
        AspPanleResumenRequerimiento.Visible = true;
        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Se agrego correctamente el Candidato.", Proceso.Correcto);
        ACCIONINTERNA("LIMPIAR");
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int filaSeleccionada = Convert.ToInt32(e.CommandArgument);
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);
        String Filtro = Session["idEmpresa"].ToString();
        if (e.CommandName == "Agendar")
        {
            Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            Decimal ID_LIQ_NOMINA_EMPLEADOS = Convert.ToDecimal(GridViewListarContactosPorSolicitud.DataKeys[filaSeleccionada].Values["Id_Solicitud_requerimiento"]);
            Decimal ID_RegistroDeLista = Convert.ToDecimal(GridViewListarContactosPorSolicitud.DataKeys[filaSeleccionada].Values["Id_Registro"]);
            AspPanleResumenRequerimiento.Visible = true;
            PanelListaDeCandidatos.Visible = true;
            panelAgendarCandidato.Visible = true;
            String Id_Solicitud = Convert.ToString(ID_LIQ_NOMINA_EMPLEADOS);
            String Id_Registro = Convert.ToString(ID_RegistroDeLista);
            this.lblIdRegistro.Text = Convert.ToString(Id_Solicitud);
            this.LblRegistrodeLista.Text = Convert.ToString(Id_Registro);
            TextBoxFechaAgenda.Text = "";
            DropDownListPsicologo.ClearSelection();
            GridViewAgendaPsicologoPrincipal.Visible = false;
            RadioButtonList1.ClearSelection();
        }
        if (e.CommandName == "ASISTENCIA")
        {
            Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable PanelReclutador = ConstructorReclutamiento.CargarGridSeguimientoRecepciom(Session["idEmpresa"].ToString(), HiddenField_SEGUIMIENTO_RECEPCION.Value);
            LabelREGISTRO.Text = PanelReclutador.Rows[0]["Id_Solicitud_requerimiento"].ToString();
            txtFechaCitada.Text = PanelReclutador.Rows[0]["FECHA"].ToString();
            txtFechaCitada.Enabled = false;
            TextBoxHora.Text = Convert.ToString(GridViewSeguimientoRecepcion.DataKeys[filaSeleccionada].Values["HORA"]);
            TextBoxHora.Enabled = false;
            TextBoxCandidatoSitado.Text = PanelReclutador.Rows[0]["CANDIDATO"].ToString();
            TextBoxCandidatoSitado.Enabled = false;
            PanelSeguimientoAsistencia.Visible = false;
            seguimientoResepcion(HiddenField_SEGUIMIENTO_RECEPCION.Value);

        }
        if (e.CommandName == "SEGUIMIENTO")
        {
            Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable PanelReclutador = ConstructorReclutamiento.CargarGridSeguimientoRecepciom(Session["idEmpresa"].ToString(), HiddenField_SEGUIMIENTO_RECEPCION.Value);
            ConstructorReclutamiento.SeguimientoContacto(Session["idEmpresa"].ToString(),Convert.ToString(GridViewSeguimientoRecepcion.DataKeys[filaSeleccionada].Values["Id_Registro_Empleado"]));
            seguimientoResepcion(HiddenField_SEGUIMIENTO_RECEPCION.Value);
        }
        if (e.CommandName == "Agendar_Cita")
        {
            AspPanleResumenRequerimiento.Visible = true;
            Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable PanelReclutador = ConstructorReclutamiento.CargarGridSeguimientoRecepciom(Session["idEmpresa"].ToString(), this.TextBoxFiltro.Text);

            String @Id_Registro = this.LblRegistrodeLista.Text;
            String @Fecha_De_Cita = this.TextBoxFechaAgenda.Text;
            String @Psicologo = this.DropDownListPsicologo.SelectedValue;
            String @Usuario_Crea_Registro = Session["USU_LOG"].ToString();
            String @Hora = Convert.ToString(GridViewAgendaPsicologoPrincipal.DataKeys[filaSeleccionada].Values["HORA"]);
            String @AceptaOferta = this.RadioButtonList1.SelectedValue;

            ConstructorReclutamiento.AgendarContactoPorFecha(Session["idEmpresa"].ToString(), @Id_Registro, @Fecha_De_Cita, @Psicologo, @Usuario_Crea_Registro, @Hora, @AceptaOferta);
            AspPanleResumenRequerimiento.Visible = true;
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Se agendo correctamente el Candidato.", Proceso.Correcto);
            CargarGridListaContactos(this.lblIdRegistro.Text);
            lblIdRegistro.Text = "";
            LblRegistrodeLista.Text = "";
            this.TextBoxFechaAgenda.Text = "";
            this.TextBoxFechaAgenda.Enabled = false;
            this.DropDownListPsicologo.ClearSelection();
            this.DropDownListPsicologo.Enabled = false;
            TextBoxNombre.Text = "";
            TextBoxTelefono.Text = "";
            TextBoxApellido.Text = "";
            TextBoxDocumento.Text = "";

            DropDownListFuentesReclutamiento.ClearSelection();
            panelAgendarCandidato.Visible = false;
            
        }

    }
    protected void Agendar(object sender, GridViewCommandEventArgs e)
    {

    }
    protected void ButtonBuscarCandidato_Click(object sender, EventArgs e)
    {
        Formulariorecepcion();
        GridViewSeguimientoRecepcion.Visible = true;
        HiddenField_SEGUIMIENTO_RECEPCION.Value = this.TextBoxFiltro.Text;
        seguimientoResepcion(HiddenField_SEGUIMIENTO_RECEPCION.Value);
        TextBoxFiltro.Text = "";

        tools _tools = new tools();
        Seguimiento_Recepcion_Candidato.Value = this.TextBoxFiltro.Text;
        SecureQueryString QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);
        String Filtro = Session["idEmpresa"].ToString();
        Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable panelSeguimiento_HV = ConstructorReclutamiento.ValidarHV(Session["idEmpresa"].ToString(), HiddenField_SEGUIMIENTO_RECEPCION.Value);
        if (panelSeguimiento_HV.Rows.Count > 0)
        {
            Label10.Visible = true;
            Label20.Visible = true;
            Label11.Visible = true;
            Label12.Visible = true;
            this.LBL_CANDIDATO.Text = panelSeguimiento_HV.Rows[0]["CANDIDATO"].ToString();
            this.LBL_ID_SOLICITUD.Text = panelSeguimiento_HV.Rows[0]["ID_SOLICITUD"].ToString();
            this.LBL_TIPO_DOCUMENTO.Text = panelSeguimiento_HV.Rows[0]["TIP_DOC_IDENTIDAD"].ToString();
            this.lbl_DOCUMENTO.Text = panelSeguimiento_HV.Rows[0]["NUM_DOC_IDENTIDAD"].ToString();
            seguimientoResepcion(this.HiddenField_SEGUIMIENTO_RECEPCION.Value);
        }
        else
        {
            PanelSeguimientoAsistencia.Visible = false;
            AsignacionReclutador.Visible = false;
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El candidato no ha registrado su hoja de vida en el portal de Servicios Temporales Profesionales Bogotá. Favor registrar la hoja de vida para continuar a entrevista.", Proceso.Advertencia);
            GridViewSeguimientoRecepcion.Enabled = false;
        }

    }
    private void seguimientoResepcion(String @filtro)
    {
        Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable PanelReclutador = ConstructorReclutamiento.CargarGridSeguimientoRecepciom(Session["idEmpresa"].ToString(), @filtro);
        GridViewSeguimientoRecepcion.DataSource = PanelReclutador;
        GridViewSeguimientoRecepcion.DataBind();
    }
    protected void ButtonAcisteCita_Click(object sender, EventArgs e)
    {
        String @Accion = "asiste_a_la_cita";
        String @Asiste_Cita = "SI";
        String @Usuario = LabelREGISTRO.Text;
        String @Acepta_Oferta = "";
        Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        Decimal PanelReclutador = ConstructorReclutamiento.SolicitudAccionSeguimientoContactos(Session["idEmpresa"].ToString(), @Accion, @Asiste_Cita, @Usuario, @Acepta_Oferta);
        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Se actualizo la asistencia del Candidato Correctamente.", Proceso.Correcto);
        PanelSeguimientoAsistencia.Visible = false;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Formulariorecepcion();
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue == "SI")
        {
            DropDownListPsicologo.Enabled = true;
            TextBoxFechaAgenda.Enabled = true;
            TextBoxFechaAgenda.Focus();
            Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable PanelReclutador = ConstructorReclutamiento.CargarGrid_Ista_Contactos(Session["idEmpresa"].ToString(), lblIdRegistro.Text);
            GridViewListarContactosPorSolicitud.DataSource = PanelReclutador;
            GridViewListarContactosPorSolicitud.DataBind();
            GridViewListarContactosPorSolicitud.Visible = true;
        }
        else
        {
            DropDownListPsicologo.Enabled = false;
            TextBoxFechaAgenda.Enabled = false;
            AspPanleResumenRequerimiento.Visible = true;
            Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            String @Id_Registro = this.LblRegistrodeLista.Text;
            String @Fecha_De_Cita = "";
            String @Psicologo = "";
            String @Usuario_Crea_Registro = Session["USU_LOG"].ToString();
            String @Hora = "";
            String @AceptaOferta = this.RadioButtonList1.SelectedValue;

            ConstructorReclutamiento.AgendarContactoPorFecha(Session["idEmpresa"].ToString(), @Id_Registro, @Fecha_De_Cita, @Psicologo, @Usuario_Crea_Registro, @Hora, @AceptaOferta);
            AspPanleResumenRequerimiento.Visible = true;
            this.TextBoxFechaAgenda.Text = "";
            this.TextBoxFechaAgenda.Enabled = false;
            this.DropDownListPsicologo.ClearSelection();
            this.DropDownListPsicologo.Enabled = false;
            AspPanleResumenRequerimiento.Visible = true;
            panelAgendarCandidato.Visible = false;
            CargarGridListaContactos(lblIdRegistro.Text);
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Candidato descartado.", Proceso.Correcto);
        }
    }
    private void CargarGridListaContactos(String @ID_Registro)
    {
        Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable PanelReclutador = ConstructorReclutamiento.CargarGrid_Ista_Contactos(Session["idEmpresa"].ToString(), @ID_Registro);
        GridViewListarContactosPorSolicitud.DataSource = PanelReclutador;
        GridViewListarContactosPorSolicitud.DataBind();
        for (int j = 0; j < GridViewListarContactosPorSolicitud.Rows.Count; j++)
        {
            GridViewRow filaGrilla = GridViewListarContactosPorSolicitud.Rows[j];
            String TXT_SI_NO = filaGrilla.Cells[7].Text;

            if (TXT_SI_NO == "SI")
            {
                filaGrilla.Enabled = false;
                GridViewListarContactosPorSolicitud.Rows[j].Cells[9].Enabled = false;
            }
            if (TXT_SI_NO == "NO")
            {
                filaGrilla.Enabled = false;
                GridViewListarContactosPorSolicitud.Rows[j].Cells[9].Enabled = false;
            }
            if (TXT_SI_NO == "&nbsp;")
            {
            }
        }
    }

    protected void TextBoxDocumento_TextChanged(object sender, EventArgs e)
    {
        Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable PanelReclutador = ConstructorReclutamiento.ConsultaCandidatoFiltro(Session["idEmpresa"].ToString(), TextBoxDocumento.Text);
        int j = 0;

        for ( j = 0; j < PanelReclutador.Rows.Count; j++)
        {
            j = j + 1;
        }
        if (j > 0)
        {
            String YaExite = PanelReclutador.Rows[0]["Existe"].ToString();
            String Estado = PanelReclutador.Rows[0]["Estado"].ToString();
            if (YaExite == null)
            {
            }
            else
            {
                if (YaExite != "Existe")
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El El candidato identificado con numero de cedula:  " + TextBoxDocumento.Text + " no se encuentra disponible para esta requisición.", Proceso.Advertencia);
                    TextBoxDocumento.Text = "";
                    TextBoxDocumento.Focus();
                }
                else
                {
                }
            }
        }
    }
    protected void DropDownListPsicologo_SelectedIndexChanged(object sender, EventArgs e)
    {
        AgendarContacto.Enabled = true;
    }
    protected void GridViewSeguimientoRecepcion_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void DropDownListPsicologo_SelectedIndexChanged1(object sender, EventArgs e)
    {
        AgendarContacto.Enabled = true;
    }
    protected void DropDownListPsicologo_TextChanged(object sender, EventArgs e)
    {
        AgendarContacto.Enabled = true;
    }
    protected void btn_Agrregar_contacto_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void ImageButtonAgregarALista_Click(object sender, ImageClickEventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE, Panel_MENSAJES);
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);
        Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        String @ACCION = "GUARDAR";
        String @Id_solicitud = this.TextBoxResumenSolicitud.Text;
        String @Apellido = this.TextBoxApellido.Text;
        String @Nombre = this.TextBoxNombre.Text;
        String @Docuemnto = this.TextBoxDocumento.Text;
        String @Telefono = this.TextBoxTelefono.Text;
        String @TextBoxResumenCargo = this.TextBoxResumenCargo.Text;
        String @FechaContacto = Convert.ToString(DateTime.Today);
        TextBoxNombre.Text ="";
        TextBoxTelefono.Text = "";
        TextBoxApellido.Text = "";
        TextBoxDocumento.Text = "";
        DropDownListFuentesReclutamiento.ClearSelection();

        ConstructorReclutamiento.SolicitudAccionListarContactos(Session["idEmpresa"].ToString(), @ACCION, @Id_solicitud, @Apellido, @Nombre, @Docuemnto, @Telefono, @TextBoxResumenCargo, @FechaContacto);
        AspPanleResumenRequerimiento.Visible = true;
        PanelListaDeCandidatos.Visible = true;
        GridViewListarContactosPorSolicitudReclutador(this.TextBoxResumenSolicitud.Text);
        GridViewListarContactosPorSolicitud.Visible = true;
        CargarGridListaContactos(this.TextBoxResumenSolicitud.Text);
        ACCIONINTERNA("LIMPIAR");
    }
    protected void TextBoxApellido_TextChanged(object sender, EventArgs e)
    {

    }
    protected void GridViewAgendaPsicologoPrincipal_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int filaSeleccionada = Convert.ToInt32(e.CommandArgument);
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);
        String Filtro = Session["idEmpresa"].ToString();

        if (e.CommandName == "AgendarContacto")
        {
            AspPanleResumenRequerimiento.Visible = true;
            Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable PanelReclutador = ConstructorReclutamiento.CargarGridSeguimientoRecepciom(Session["idEmpresa"].ToString(), this.TextBoxFiltro.Text);

            String @Id_Registro = this.LblRegistrodeLista.Text;
            String @Fecha_De_Cita = this.TextBoxFechaAgenda.Text;
            String @Psicologo = this.DropDownListPsicologo.SelectedValue;
            String @Usuario_Crea_Registro = Session["USU_LOG"].ToString();
            String @Hora = Convert.ToString(GridViewAgendaPsicologoPrincipal.DataKeys[filaSeleccionada].Values["HORA"]);
            String @AceptaOferta = this.RadioButtonList1.SelectedValue;

            ConstructorReclutamiento.AgendarContactoPorFecha(Session["idEmpresa"].ToString(), @Id_Registro, @Fecha_De_Cita, @Psicologo, @Usuario_Crea_Registro, @Hora, @AceptaOferta);
            AspPanleResumenRequerimiento.Visible = true;
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Se agendo correctamente el Candidato.", Proceso.Correcto);
            CargarGridListaContactos(this.lblIdRegistro.Text);
            lblIdRegistro.Text = "";
            LblRegistrodeLista.Text = "";
            this.TextBoxFechaAgenda.Text = "";
            this.TextBoxFechaAgenda.Enabled = false;
            this.DropDownListPsicologo.ClearSelection();
            this.DropDownListPsicologo.Enabled = false;
            TextBoxNombre.Text = "";
            TextBoxTelefono.Text = "";
            TextBoxApellido.Text = "";
            TextBoxDocumento.Text = "";

            DropDownListFuentesReclutamiento.ClearSelection();
            panelAgendarCandidato.Visible = false;
            CargarGridHojaTrabajoReclutador();
        }
    }
    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        HiddenField_TIPO_BUSQUEDA_ACTUAL.Value = "CON_FILTRO";
        HiddenField_FILTRO_DROP.Value = DropDownList_BUSCAR.SelectedValue;
        HiddenField_FILTRO_DATO.Value = TextBox_BUSCAR.Text;
        HiddenField_PAGINA_GRID.Value = "0";

        cargar_GridView_HOJA_DE_TRABAJO();
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

        item = new System.Web.UI.WebControls.ListItem("Regional", "ID_REGIONAL");
        DropDownList_BUSCAR.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("Ciudad", "ID_CIUDAD");
        DropDownList_BUSCAR.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("Cliente", "ID_CLIENTE");
        DropDownList_BUSCAR.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("Número de requicición", "ID_REQUERIMIENTO");
        DropDownList_BUSCAR.Items.Add(item);

        DropDownList_BUSCAR.DataBind();
    }
    protected void DropDownList_BUSCAR_SelectedIndexChanged(object sender, EventArgs e)
    {
        Panel_FORM_BOTONES.Visible = true;
        if (DropDownList_BUSCAR.SelectedValue == "")
        {
            configurarCaracteresAceptadosBusqueda(true, true);
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "ID_REGIONAL")
            {
                configurarCaracteresAceptadosBusqueda(true, false);
            }
            else
            {
                if (DropDownList_BUSCAR.SelectedValue == "ID_CIUDAD")
                {
                    configurarCaracteresAceptadosBusqueda(true, false);
                }
                else
                {
                    if (DropDownList_BUSCAR.SelectedValue == "ID_CLIENTE")
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
        }

        TextBox_BUSCAR.Text = "";
    }

    protected void ButtonGrabarProductividad_Click(object sender, EventArgs e)
    {
        AsignacionReclutador.Visible = false;
        ocultar_mensaje(Panel_FONDO_MENSAJE, Panel_MENSAJES);
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);
        Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        ConstructorReclutamiento.Accion = "GUARDAR";
        ConstructorReclutamiento.Id_Empresa = this.ProductividadEmpresa.SelectedValue;
        ConstructorReclutamiento.Id_Requerimiento = "";
        ConstructorReclutamiento.Id_usuario_Asignado = this.ProductividadReclutador.SelectedValue;
        ConstructorReclutamiento.Cantidad = this.ProductividadCantidad.Text;
        ConstructorReclutamiento.Fecha_Requerida = this.ProductividadFechaRequerida.Text;
        ConstructorReclutamiento.Fecha_Ingreso_Solicitud = Convert.ToString(DateTime.Today);
        ConstructorReclutamiento.Descripcion = this.ProductividadDescripcion.Text;
        ConstructorReclutamiento.Empresa = Session["idEmpresa"].ToString();
        ConstructorReclutamiento.Usuario_Crea_Solicitud = Session["USU_LOG"].ToString();
        ConstructorReclutamiento.Regional = this.ProductividadRegional.SelectedValue;
        ConstructorReclutamiento.Ciudad = ProductividadCiudad.SelectedValue;
        ConstructorReclutamiento.Cargo = this.ProductividadCargo.SelectedValue;
        ConstructorReclutamiento.Tipo_Requerimietno = "PRODUCTIVIDAD";
        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Se asignó correctamente el requerimiento por productividad a al usuario: " + this.ProductividadReclutador.SelectedValue + ".", Proceso.Correcto);
        ConstructorReclutamiento.SolicitudAccion();
        ACCIONINTERNA("LIMPIAR");
    }
    protected void ProductividadRegional_SelectedIndexChanged(object sender, EventArgs e)
    {
        Reporte reporte = new Reporte(Session["idEmpresa"].ToString());
        Cargar(ProductividadCiudad, reporte.ListarCiudadesPorRegional(ProductividadRegional.SelectedValue), "Seleccione un ciudad...");
    }
    private void GridViewSeguimiento_Productividad_Cargar()
    {
        Reclutamiento ConstructorReclutamientoPD = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable PanelReclutadorPD = ConstructorReclutamientoPD.ObtenerComRequerimientoPorUsuLog(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        GridViewSeguimiento_Productividad.DataSource = PanelReclutadorPD;
        GridViewSeguimiento_Productividad.DataBind();
    }
    private void GridViewSeguimientoReclutamiento_Cargar()
    {
        Reclutamiento ConstructorReclutamientoPD = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable PanelReclutadorPD = ConstructorReclutamientoPD.ObtenerComRequerimientoPorRequerimiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        GridViewSeguimientoReclutamiento.DataSource = PanelReclutadorPD;
        GridViewSeguimientoReclutamiento.DataBind();
    }


    protected void ButtonPsicologo_Click(object sender, EventArgs e)
    {
        FormularioPsicologo();
    }
    protected void ButtonPsicologofdsf_Click(object sender, EventArgs e)
    {
        GridViewAgendaPsicologo.Visible = true;
        Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable PanelReclutador = ConstructorReclutamiento.CargarAgendaDelPsicologo(Session["idEmpresa"].ToString());
        CargarGridAgendaPsicologoSeguimiento(Session["idEmpresa"].ToString(), DropDownListPsicologo.SelectedValue, TextBoxFechaAgenda.Text);
    }
    private void CargarGridAgendaPsicologoSeguimiento(String @Empresa, String @Psicologo, String @Fecha)
    {
        Reclutamiento ConstructorReclutamiento = new Reclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable PanelReclutador = ConstructorReclutamiento.CargarAgendaDelPsicologo(Session["idEmpresa"].ToString());
        GridViewAgendaPsicologo.DataSource = PanelReclutador;
        GridViewAgendaPsicologo.DataBind();

        for (int j = 0; j < GridViewAgendaPsicologo.Rows.Count; j++)
        {
            Decimal Valor = Convert.ToDecimal(GridViewAgendaPsicologo.DataKeys[j].Values["HORA"].ToString());

            GridView GrillaInterna = GridViewAgendaPsicologo.Rows[j].FindControl("GridViewAgendaPsicologoListaContactosPsicologo") as GridView;
            DataTable PanelReclutadorInterna = ConstructorReclutamiento.AgendaDeContactosEspecial(Session["idEmpresa"].ToString(), Convert.ToString(Valor), DropDownListPsicologoForm.SelectedValue, TextBoxFechaAgendaPsicologo.Text);
            GrillaInterna.DataSource = PanelReclutadorInterna;
            GrillaInterna.DataBind();
        }
    }
    protected void GridViewHojaDeTrabajoReclutador_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewHojaDeTrabajoReclutador.PageIndex = e.NewPageIndex;
        CargarGridHojaTrabajoReclutador();
    }
    protected void GridViewSeguimientoReclutamiento_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridViewSeguimientoReclutamiento.PageIndex = e.NewPageIndex;
        GridViewSeguimientoReclutamiento_Cargar();
    }
}