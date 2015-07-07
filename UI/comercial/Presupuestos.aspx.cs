using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brainsbits.LLB.comercial;
using System.Data;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;
using TSHAK.Components;
using Brainsbits.LLB;

public partial class _Presupuesto : System.Web.UI.Page
{

    private String NOMBRE_AREA = tabla.NOMBRE_AREA_GESTION_COMERCIAL;

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

    private System.Drawing.Color colorSeleccionado = System.Drawing.ColorTranslator.FromHtml("#699DF0");

    private enum Acciones
    {
        Inicio = 0,
        SeleccionAnio,
        SeleccionRegional, 
        SeleccionCiudad,
        Actualizar
    }

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

                Panel_PresupuestoRegional.Visible = false;
                
                Panel_Ciudades.Visible = false;

                Panel_DetallesPorAnio.Visible = false;

                Panel_FORM_BOTONES_PIE.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                break;
            case Acciones.SeleccionAnio:
                Panel_Ciudades.Visible = false;
                Panel_DetallesPorAnio.Visible = false;
                Panel_FORM_BOTONES_PIE.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                break;
            case Acciones.SeleccionRegional:
                Panel_DetallesPorAnio.Visible = false;
                Panel_FORM_BOTONES_PIE.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                break;
            case Acciones.SeleccionCiudad:
                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                break;
            case Acciones.Actualizar:
                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                break;
        }

    }

    private void Desactivar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                TextBox_PresupuestoEnero.Enabled = false;
                TextBox_PresupuestoFebrero.Enabled = false;
                TextBox_PresupuestoMarzo.Enabled = false;
                TextBox_PresupuestoAbril.Enabled = false;
                TextBox_PresupuestoMayo.Enabled = false;
                TextBox_PresupuestoJunio.Enabled = false;
                TextBox_PresupuestoJulio.Enabled = false;
                TextBox_PresupuestoAgosto.Enabled = false;
                TextBox_PresupuestoSeptiembre.Enabled = false;
                TextBox_PresupuestoOctubre.Enabled = false;
                TextBox_PresupuestoNoviembre.Enabled = false;
                TextBox_PresupuestoDiciembre.Enabled = false;
                break;
            case Acciones.SeleccionCiudad:
                TextBox_PresupuestoEnero.Enabled = false;
                TextBox_PresupuestoFebrero.Enabled = false;
                TextBox_PresupuestoMarzo.Enabled = false;
                TextBox_PresupuestoAbril.Enabled = false;
                TextBox_PresupuestoMayo.Enabled = false;
                TextBox_PresupuestoJunio.Enabled = false;
                TextBox_PresupuestoJulio.Enabled = false;
                TextBox_PresupuestoAgosto.Enabled = false;
                TextBox_PresupuestoSeptiembre.Enabled = false;
                TextBox_PresupuestoOctubre.Enabled = false;
                TextBox_PresupuestoNoviembre.Enabled = false;
                TextBox_PresupuestoDiciembre.Enabled = false;
                break;
        }
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = true;
                Panel_PresupuestoRegional.Visible = true;
                break;
            case Acciones.SeleccionRegional:
                Panel_Ciudades.Visible = true;
                break;
            case Acciones.SeleccionCiudad:
                Panel_DetallesPorAnio.Visible = true;
                Panel_FORM_BOTONES_PIE.Visible = true;
                break;
            case Acciones.Actualizar:
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
                break;
        }
    }

    private void CargarAnios()
    {
        Presupuesto _presupuesto = new Presupuesto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaAnios = _presupuesto.ObtenerAniosYPresupuestos();

        if (_presupuesto.MensajeError != null)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _presupuesto.MensajeError, Proceso.Error);
        }
        else
        {
            GridView_Anios.DataSource = tablaAnios;
            GridView_Anios.DataBind();
        }
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                this.Title = "PRESUPUESTOS";
                HiddenField_ANIO.Value = "";
                HiddenField_ID_CIUDAD.Value = "";
                HiddenField_ID_REGIONAL.Value = "";

                CargarAnios();

                GridView_Regionales.DataSource = null;
                GridView_Regionales.DataBind();

                GridView_Ciudades.DataSource = null;
                GridView_Ciudades.DataBind();

                TextBox_PresupuestoEnero.Text = "";
                TextBox_PresupuestoFebrero.Text = "";
                TextBox_PresupuestoMarzo.Text = "";
                TextBox_PresupuestoAbril.Text = "";
                TextBox_PresupuestoMayo.Text = "";
                TextBox_PresupuestoJunio.Text = "";
                TextBox_PresupuestoJulio.Text = "";
                TextBox_PresupuestoAgosto.Text = "";
                TextBox_PresupuestoSeptiembre.Text = "";
                TextBox_PresupuestoOctubre.Text = "";
                TextBox_PresupuestoNoviembre.Text = "";
                TextBox_PresupuestoDiciembre.Text = "";

                Label_INFO_ADICIONAL_MODULO.Text = "SELECCIÓN DE PRESUPUESTOS";
                break;
            case Acciones.SeleccionAnio:
                GridView_Ciudades.DataSource = null;
                GridView_Ciudades.DataBind();

                TextBox_PresupuestoEnero.Text = "";
                TextBox_PresupuestoFebrero.Text = "";
                TextBox_PresupuestoMarzo.Text = "";
                TextBox_PresupuestoAbril.Text = "";
                TextBox_PresupuestoMayo.Text = "";
                TextBox_PresupuestoJunio.Text = "";
                TextBox_PresupuestoJulio.Text = "";
                TextBox_PresupuestoAgosto.Text = "";
                TextBox_PresupuestoSeptiembre.Text = "";
                TextBox_PresupuestoOctubre.Text = "";
                TextBox_PresupuestoNoviembre.Text = "";
                TextBox_PresupuestoDiciembre.Text = "";
                break;
            case Acciones.SeleccionRegional:
                TextBox_PresupuestoEnero.Text = "";
                TextBox_PresupuestoFebrero.Text = "";
                TextBox_PresupuestoMarzo.Text = "";
                TextBox_PresupuestoAbril.Text = "";
                TextBox_PresupuestoMayo.Text = "";
                TextBox_PresupuestoJunio.Text = "";
                TextBox_PresupuestoJulio.Text = "";
                TextBox_PresupuestoAgosto.Text = "";
                TextBox_PresupuestoSeptiembre.Text = "";
                TextBox_PresupuestoOctubre.Text = "";
                TextBox_PresupuestoNoviembre.Text = "";
                TextBox_PresupuestoDiciembre.Text = "";
                break;
            case Acciones.SeleccionCiudad:
                TextBox_PresupuestoEnero.Text = "";
                TextBox_PresupuestoFebrero.Text = "";
                TextBox_PresupuestoMarzo.Text = "";
                TextBox_PresupuestoAbril.Text = "";
                TextBox_PresupuestoMayo.Text = "";
                TextBox_PresupuestoJunio.Text = "";
                TextBox_PresupuestoJulio.Text = "";
                TextBox_PresupuestoAgosto.Text = "";
                TextBox_PresupuestoSeptiembre.Text = "";
                TextBox_PresupuestoOctubre.Text = "";
                TextBox_PresupuestoNoviembre.Text = "";
                TextBox_PresupuestoDiciembre.Text = "";
                break;
        }
    }

    private void Activar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.SeleccionCiudad:
                TextBox_PresupuestoEnero.Enabled = true;
                TextBox_PresupuestoFebrero.Enabled = true;
                TextBox_PresupuestoMarzo.Enabled = true;
                TextBox_PresupuestoAbril.Enabled = true;
                TextBox_PresupuestoMayo.Enabled = true;
                TextBox_PresupuestoJunio.Enabled = true;
                TextBox_PresupuestoJulio.Enabled = true;
                TextBox_PresupuestoAgosto.Enabled = true;
                TextBox_PresupuestoSeptiembre.Enabled = true;
                TextBox_PresupuestoOctubre.Enabled = true;
                TextBox_PresupuestoNoviembre.Enabled = true;
                TextBox_PresupuestoDiciembre.Enabled = true;
                break;
            case Acciones.Actualizar:
                TextBox_PresupuestoEnero.Enabled = true;
                TextBox_PresupuestoFebrero.Enabled = true;
                TextBox_PresupuestoMarzo.Enabled = true;
                TextBox_PresupuestoAbril.Enabled = true;
                TextBox_PresupuestoMayo.Enabled = true;
                TextBox_PresupuestoJunio.Enabled = true;
                TextBox_PresupuestoJulio.Enabled = true;
                TextBox_PresupuestoAgosto.Enabled = true;
                TextBox_PresupuestoSeptiembre.Enabled = true;
                TextBox_PresupuestoOctubre.Enabled = true;
                TextBox_PresupuestoNoviembre.Enabled = true;
                TextBox_PresupuestoDiciembre.Enabled = true;
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

    private void CargarRegionalesPresupuestos(Int32 anio)
    {
        HiddenField_ANIO.Value = anio.ToString();
        HiddenField_ID_REGIONAL.Value = null;
        HiddenField_ID_CIUDAD.Value = null;
        HiddenField_ANIO_CON_PRESUPUESTO.Value = "NO";

        Ocultar(Acciones.SeleccionAnio);
        Cargar(Acciones.SeleccionAnio);

        Presupuesto _presupuesto = new Presupuesto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaRegionales = _presupuesto.ObtenerRegionalesConPresupuestoPorAnio(anio);

        if (tablaRegionales.Rows.Count <= 0)
        {
            if (_presupuesto.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _presupuesto.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información de regionales.", Proceso.Advertencia);
            }

            GridView_Regionales.DataSource = null;
            GridView_Regionales.DataBind();
        }
        else
        {
            GridView_Regionales.DataSource = tablaRegionales;
            GridView_Regionales.DataBind();
        }
    }

    protected void GridView_Anios_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Int32 anio = Convert.ToInt32(GridView_Anios.DataKeys[indexSeleccionado].Values["ANIO"]);
            HiddenField_ANIO.Value = anio.ToString();

            CargarRegionalesPresupuestos(anio);

            for(Int32 i = 0; i < GridView_Anios.Rows.Count; i++)
            { 
                GridViewRow filaGrilla = GridView_Anios.Rows[i];
                Int32 anioGrilla = Convert.ToInt32(GridView_Anios.DataKeys[i].Values["ANIO"]);

                if (anioGrilla == anio)
                {
                    filaGrilla.BackColor = colorSeleccionado;
                }
                else
                {
                    filaGrilla.BackColor = System.Drawing.Color.Transparent; ;
                }
            }
        }
    }
    protected void GridView_Anios_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_Anios.PageIndex = e.NewPageIndex;

        HiddenField_ANIO.Value = "";
        HiddenField_ID_REGIONAL.Value = "";
        HiddenField_ID_CIUDAD.Value = "";
        HiddenField_ANIO_CON_PRESUPUESTO.Value = "NO";

        GridView_Regionales.DataSource = null;
        GridView_Regionales.DataBind();

        Panel_Ciudades.Visible = false;
        Panel_DetallesPorAnio.Visible = false;

        CargarAnios();
    }

    private void CargarCiudadesPresupuestos(Decimal ID_REGIONAL)
    {
        Int32 anio = Convert.ToInt32(HiddenField_ANIO.Value);

        HiddenField_ID_REGIONAL.Value = ID_REGIONAL.ToString();
        HiddenField_ID_CIUDAD.Value = null;
        HiddenField_ANIO_CON_PRESUPUESTO.Value = "NO";

        Ocultar(Acciones.SeleccionRegional);
        Mostrar(Acciones.SeleccionRegional);
        Cargar(Acciones.SeleccionRegional);

        Presupuesto _presupuesto = new Presupuesto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCiudades = _presupuesto.ObtenerCiudadesConPresupuestoPorIdRegional(ID_REGIONAL, anio);

        if (tablaCiudades.Rows.Count <= 0)
        {
            if (_presupuesto.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _presupuesto.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información de ciudades asociadas a la regional seleccionada.", Proceso.Advertencia);
            }

            GridView_Ciudades.DataSource = null;
            GridView_Ciudades.DataBind();
        }
        else
        {
            GridView_Ciudades.DataSource = tablaCiudades;
            GridView_Ciudades.DataBind();
        }   
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal ID_REGIONAL = Convert.ToInt32(GridView_Regionales.DataKeys[indexSeleccionado].Values["ID_REGIONAL"]);
            HiddenField_ID_REGIONAL.Value = ID_REGIONAL.ToString();

            GridView_Ciudades.PageIndex = 0;

            CargarCiudadesPresupuestos(ID_REGIONAL);

            for (Int32 i = 0; i < GridView_Regionales.Rows.Count; i++)
            {
                GridViewRow filaGrilla = GridView_Regionales.Rows[i];
                Decimal idRegionalGrilla = Convert.ToDecimal(GridView_Regionales.DataKeys[i].Values["ID_REGIONAL"]);

                if (idRegionalGrilla == ID_REGIONAL)
                {
                    filaGrilla.BackColor = colorSeleccionado;
                }
                else
                {
                    filaGrilla.BackColor = System.Drawing.Color.Transparent;
                }
            }
        }
    }
    protected void GridView_Ciudades_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        Decimal ID_REGIONAL = Convert.ToDecimal(HiddenField_ID_REGIONAL.Value);

        HiddenField_ID_CIUDAD.Value = "";
        HiddenField_ANIO_CON_PRESUPUESTO.Value = "NO";

        GridView_Ciudades.PageIndex = e.NewPageIndex;

        CargarCiudadesPresupuestos(ID_REGIONAL);

        Panel_DetallesPorAnio.Visible = false;
    }

    private Decimal buscarPresupuestoMesEnTabla(DataTable tablaPresupuestos, Int32 mes)
    {
        DataRow[] filasPresupuestoMes = tablaPresupuestos.Select("MES = " + mes.ToString());
        Decimal presupuesto = 0.00M;
        if (filasPresupuestoMes.Length >= 1)
        {
            DataRow filaPresupuesto = filasPresupuestoMes[0];

            presupuesto = Convert.ToDecimal(filaPresupuesto["PRESUPUESTO"]);
        }

        return presupuesto;
    }
    private void CargarTextBoxsAnios(DataTable tablaPresupuestos)
    {
        TextBox_PresupuestoEnero.Text = buscarPresupuestoMesEnTabla(tablaPresupuestos, 1).ToString();

        TextBox_PresupuestoFebrero.Text = buscarPresupuestoMesEnTabla(tablaPresupuestos, 2).ToString();

        TextBox_PresupuestoMarzo.Text = buscarPresupuestoMesEnTabla(tablaPresupuestos, 3).ToString();

        TextBox_PresupuestoAbril.Text = buscarPresupuestoMesEnTabla(tablaPresupuestos, 4).ToString();

        TextBox_PresupuestoMayo.Text = buscarPresupuestoMesEnTabla(tablaPresupuestos, 5).ToString();

        TextBox_PresupuestoJunio.Text = buscarPresupuestoMesEnTabla(tablaPresupuestos, 6).ToString();

        TextBox_PresupuestoJulio.Text = buscarPresupuestoMesEnTabla(tablaPresupuestos, 7).ToString();

        TextBox_PresupuestoAgosto.Text = buscarPresupuestoMesEnTabla(tablaPresupuestos, 8).ToString();

        TextBox_PresupuestoSeptiembre.Text = buscarPresupuestoMesEnTabla(tablaPresupuestos, 9).ToString();

        TextBox_PresupuestoOctubre.Text = buscarPresupuestoMesEnTabla(tablaPresupuestos, 10).ToString();

        TextBox_PresupuestoNoviembre.Text = buscarPresupuestoMesEnTabla(tablaPresupuestos, 11).ToString();

        TextBox_PresupuestoDiciembre.Text = buscarPresupuestoMesEnTabla(tablaPresupuestos, 12).ToString();
    }

    private void CargarPresupuestosAnioParCiudadSeleccionada(String ID_CIUDAD)
    {
        Int32 anio = Convert.ToInt32(HiddenField_ANIO.Value);
        Decimal ID_REGIONAL = Convert.ToDecimal(HiddenField_ID_REGIONAL.Value);

        HiddenField_ID_CIUDAD.Value = ID_CIUDAD;

        Ocultar(Acciones.SeleccionCiudad);
        Mostrar(Acciones.SeleccionCiudad);

        Presupuesto _presupuesto = new Presupuesto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaPresupuestos = _presupuesto.ObtenerPresupuestosAnioDeUnaCiudad(ID_CIUDAD, anio);

        if (tablaPresupuestos.Rows.Count <= 0)
        {
            if (_presupuesto.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _presupuesto.MensajeError, Proceso.Error);

                Panel_DetallesPorAnio.Visible = false;
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se han parametrizado presupuestos para el año " + anio.ToString() + " para la ciudad seleccionada.", Proceso.Advertencia);
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;

                Cargar(Acciones.SeleccionCiudad);
                Activar(Acciones.SeleccionCiudad);
            }

            HiddenField_ANIO_CON_PRESUPUESTO.Value = "NO";
        }
        else
        {
            Desactivar(Acciones.SeleccionCiudad);
            CargarTextBoxsAnios(tablaPresupuestos);

            Button_MODIFICAR_1.Visible = Visible;

            HiddenField_ANIO_CON_PRESUPUESTO.Value = "SI";
        }   
    }

    protected void GridView_Ciudades_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            String ID_CIUDAD = GridView_Ciudades.DataKeys[indexSeleccionado].Values["ID_CIUDAD"].ToString();
            HiddenField_ID_CIUDAD.Value = ID_CIUDAD.ToString();

            CargarPresupuestosAnioParCiudadSeleccionada(ID_CIUDAD);

            for (Int32 i = 0; i < GridView_Ciudades.Rows.Count; i++)
            {
                GridViewRow filaGrilla = GridView_Ciudades.Rows[i];
                String idCiudadGrilla = GridView_Ciudades.DataKeys[i].Values["ID_CIUDAD"].ToString();

                if (idCiudadGrilla == ID_CIUDAD)
                {
                    filaGrilla.BackColor = colorSeleccionado;
                }
                else
                {
                    filaGrilla.BackColor = System.Drawing.Color.Transparent;
                }
            }
        }
    }
    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Actualizar);
        Mostrar(Acciones.Actualizar);
        Activar(Acciones.Actualizar);
    }

    private void CargarInterfazDespuesDeUpdate()
    {
        Int32 ANIO = Convert.ToInt32(HiddenField_ANIO.Value);
        Decimal ID_REGIONAL = Convert.ToDecimal(HiddenField_ID_REGIONAL.Value);
        String ID_CIUDAD = HiddenField_ID_CIUDAD.Value;

        CargarAnios();
        for (Int32 i = 0; i < GridView_Anios.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_Anios.Rows[i];
            Int32 anioGrilla = Convert.ToInt32(GridView_Anios.DataKeys[i].Values["ANIO"]);

            if (anioGrilla == ANIO)
            {
                filaGrilla.BackColor = colorSeleccionado;
            }
            else
            {
                filaGrilla.BackColor = System.Drawing.Color.Transparent; ;
            }
        }

        CargarRegionalesPresupuestos(ANIO);
        for (Int32 i = 0; i < GridView_Regionales.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_Regionales.Rows[i];
            Decimal idRegionalGrilla = Convert.ToDecimal(GridView_Regionales.DataKeys[i].Values["ID_REGIONAL"]);

            if (idRegionalGrilla == ID_REGIONAL)
            {
                filaGrilla.BackColor = colorSeleccionado;
            }
            else
            {
                filaGrilla.BackColor = System.Drawing.Color.Transparent;
            }
        }

        CargarCiudadesPresupuestos(ID_REGIONAL);
        for (Int32 i = 0; i < GridView_Ciudades.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_Ciudades.Rows[i];
            String idCiudadGrilla = GridView_Ciudades.DataKeys[i].Values["ID_CIUDAD"].ToString();

            if (idCiudadGrilla == ID_CIUDAD)
            {
                filaGrilla.BackColor = colorSeleccionado;
            }
            else
            {
                filaGrilla.BackColor = System.Drawing.Color.Transparent;
            }
        }

        CargarPresupuestosAnioParCiudadSeleccionada(ID_CIUDAD);
    }

    private void Guardar()
    {
        Int32 ANIO = Convert.ToInt32(HiddenField_ANIO.Value);
        Decimal ID_REGIONAL = Convert.ToDecimal(HiddenField_ID_REGIONAL.Value);
        String ID_CIUDAD = HiddenField_ID_CIUDAD.Value;

        Decimal P_ENERO = Convert.ToDecimal(TextBox_PresupuestoEnero.Text);
        Decimal P_FEBRERO = Convert.ToDecimal(TextBox_PresupuestoFebrero.Text);
        Decimal P_MARZO = Convert.ToDecimal(TextBox_PresupuestoMarzo.Text);
        Decimal P_ABRIL = Convert.ToDecimal(TextBox_PresupuestoAbril.Text);
        Decimal P_MAYO = Convert.ToDecimal(TextBox_PresupuestoMayo.Text);
        Decimal P_JUNIO = Convert.ToDecimal(TextBox_PresupuestoJunio.Text);
        Decimal P_JULIO = Convert.ToDecimal(TextBox_PresupuestoJulio.Text);
        Decimal P_AGOSTO = Convert.ToDecimal(TextBox_PresupuestoAgosto.Text);
        Decimal P_SEPTIEMBRE = Convert.ToDecimal(TextBox_PresupuestoSeptiembre.Text);
        Decimal P_OCTUBRE = Convert.ToDecimal(TextBox_PresupuestoOctubre.Text);
        Decimal P_NOVIEMBRE = Convert.ToDecimal(TextBox_PresupuestoNoviembre.Text);
        Decimal P_DICIEMBRE = Convert.ToDecimal(TextBox_PresupuestoDiciembre.Text);

        Presupuesto _presupuesto = new Presupuesto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean correcto = _presupuesto.AdicionarActualizarPresupuestosAniosCiudad(ANIO, ID_CIUDAD, P_ENERO, P_FEBRERO, P_MARZO, P_ABRIL, P_MAYO, P_JUNIO, P_JULIO, P_AGOSTO, P_SEPTIEMBRE, P_OCTUBRE, P_NOVIEMBRE, P_DICIEMBRE);

        if (correcto == true)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La información para el año y ciudad seleccioanda fue almacenada correctamente.", Proceso.Correcto);

            CargarInterfazDespuesDeUpdate();
        }
        else
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _presupuesto.MensajeError, Proceso.Error);
        }
    }

    private void Modificar()
    {
        Int32 ANIO = Convert.ToInt32(HiddenField_ANIO.Value);
        Decimal ID_REGIONAL = Convert.ToDecimal(HiddenField_ID_REGIONAL.Value);
        String ID_CIUDAD = HiddenField_ID_CIUDAD.Value;

        Decimal P_ENERO = Convert.ToDecimal(TextBox_PresupuestoEnero.Text);
        Decimal P_FEBRERO = Convert.ToDecimal(TextBox_PresupuestoFebrero.Text);
        Decimal P_MARZO = Convert.ToDecimal(TextBox_PresupuestoMarzo.Text);
        Decimal P_ABRIL = Convert.ToDecimal(TextBox_PresupuestoAbril.Text);
        Decimal P_MAYO = Convert.ToDecimal(TextBox_PresupuestoMayo.Text);
        Decimal P_JUNIO = Convert.ToDecimal(TextBox_PresupuestoJunio.Text);
        Decimal P_JULIO = Convert.ToDecimal(TextBox_PresupuestoJulio.Text);
        Decimal P_AGOSTO = Convert.ToDecimal(TextBox_PresupuestoAgosto.Text);
        Decimal P_SEPTIEMBRE = Convert.ToDecimal(TextBox_PresupuestoSeptiembre.Text);
        Decimal P_OCTUBRE = Convert.ToDecimal(TextBox_PresupuestoOctubre.Text);
        Decimal P_NOVIEMBRE = Convert.ToDecimal(TextBox_PresupuestoNoviembre.Text);
        Decimal P_DICIEMBRE = Convert.ToDecimal(TextBox_PresupuestoDiciembre.Text);

        Presupuesto _presupuesto = new Presupuesto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean correcto = _presupuesto.AdicionarActualizarPresupuestosAniosCiudad(ANIO, ID_CIUDAD, P_ENERO, P_FEBRERO, P_MARZO, P_ABRIL, P_MAYO, P_JUNIO, P_JULIO, P_AGOSTO, P_SEPTIEMBRE, P_OCTUBRE, P_NOVIEMBRE, P_DICIEMBRE);

        if (correcto == true)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La información para el año y ciudad seleccioanda se actualizaron correctamente.", Proceso.Correcto);

            CargarInterfazDespuesDeUpdate();
        }
        else
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _presupuesto.MensajeError, Proceso.Error);
        }
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        if (HiddenField_ANIO_CON_PRESUPUESTO.Value == "SI")
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
        if (HiddenField_ANIO_CON_PRESUPUESTO.Value == "SI")
        {
            String ID_CIUDAD = HiddenField_ID_CIUDAD.Value;

            CargarPresupuestosAnioParCiudadSeleccionada(ID_CIUDAD);
        }
        else
        {
            GridView_Anios.PageIndex = 0;

            HiddenField_ANIO.Value = "";
            HiddenField_ID_REGIONAL.Value = "";
            HiddenField_ID_CIUDAD.Value = "";
            HiddenField_ANIO_CON_PRESUPUESTO.Value = "NO";

            GridView_Regionales.DataSource = null;
            GridView_Regionales.DataBind();

            Panel_Ciudades.Visible = false;
            Panel_DetallesPorAnio.Visible = false;

            CargarAnios();
        }
    }
}