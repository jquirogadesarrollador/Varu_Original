using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB.almacen;
using System.Configuration;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using Brainsbits.LLB.maestras;
using TSHAK.Components;
using Brainsbits.LLB.seguridad;

public partial class _EntregasEmpleados : System.Web.UI.Page
{
    private enum Acciones
    {
        Inicio = 0,
        Buscar,
        CargarPendientes,
        AdjuntarProducto, 
        ImprimirEntrega,
        AdjuntarEquipo
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

    private enum TiposEntrega
    {
        INICIAL = 0,
        PROGRAMADA,
        ESPECIAL
    }

    private enum EstadosAsignacionSC
    { 
        ABIERTA = 0,
        TERMINADA,
        CANCELADA
    }

    private enum TiposAjusteA
    { 
        CONTRATO,
        FECHA
    }

    ReportDocument reporte;

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
                Button_CANCELAR.Visible = false;
                Button_IMPRIMIR.Visible = false;

                Panel_RESULTADOS_GRID.Visible = false;

                Panel_InformacionEmpleadoSeleccionado.Visible = false;

                Panel_GrillaEntregasProximas.Visible = false;

                Panel_ConfiguracionProducto.Visible = false;

                Panel_AdjuntosAEntrega.Visible = false;

                Panel_FORM_BOTONES_ABAJO.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                Button_IMPRIMIR_1.Visible = false;

                Panel_ConfiguracionEquipos.Visible = false;
                Panel_GrillaEquipos.Visible = false;
                Panel_EquiposAdjuntosAEntrega.Visible = false;
                break;
            case Acciones.AdjuntarProducto:
                Panel_ConfiguracionProducto.Visible = false;
                Panel_ConfiguracionEquipos.Visible = false;
                break;
            case Acciones.ImprimirEntrega:
                Button_GUARDAR.Visible = false;
                Button_CANCELAR.Visible = false;

                GridView_Entregas.Columns[0].Visible = false;
                Panel_ConfiguracionProducto.Visible = false;

                GridView_AdjuntosAEntrega.Columns[0].Visible = false;

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
             
                break;
            case Acciones.AdjuntarProducto:
                DropDownList_TallaProducto.Enabled = false;
                DropDownList_Proveedor.Enabled = false;

                DropDownList_Factura.Enabled = false;

                TextBox_CantidadProducto.Enabled = false;

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
            case Acciones.Buscar:
                Panel_FORM_BOTONES.Visible = true;

                Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.CargarPendientes:
                Panel_FORM_BOTONES.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_InformacionEmpleadoSeleccionado.Visible = true;

                Panel_GrillaEntregasProximas.Visible = true;

                Panel_FORM_BOTONES_ABAJO.Visible = true;
                Button_CANCELAR_1.Visible = true;
                break;
            case Acciones.AdjuntarProducto:
                Panel_ConfiguracionProducto.Visible = true;
                Button_AdjuntarAEntrega.Visible = true;
                break;
            case Acciones.AdjuntarEquipo:
                Panel_ConfiguracionEquipos.Visible = true;
                Button_AdjuntarEquipos.Visible = true;
                break;
            case Acciones.ImprimirEntrega:
                Button_IMPRIMIR.Visible = true;
                Button_IMPRIMIR_1.Visible = true;
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

        item = new System.Web.UI.WebControls.ListItem("NÚMERO IDENTIFICACIÓN", "NUM_DOC_IDENTIFICACION");
        DropDownList_BUSCAR.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("NOMBRES", "NOMBRES");
        DropDownList_BUSCAR.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("APELLIDOS", "APELLIDOS");
        DropDownList_BUSCAR.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("EMPRESA", "RAZ_SOCIAL");
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
                HiddenField_FILTRO_DROP.Value = String.Empty;
                HiddenField_FILTRO_DATO.Value = String.Empty;

                HiddenField_ID_SOLICITUD.Value = "";
                HiddenField_ID_CONTRATO.Value = "";
                HiddenField_ID_EMPLEADO.Value = "";
                HiddenField_ID_EMPRESA.Value = "";
                HiddenField_ID_OCUPACION.Value = "";

                iniciar_seccion_de_busqueda();
                break;
        }
    }

    private void CargarEmpleadoInicial(Decimal ID_EMPLEADO)
    {
        usuario _usuario = new usuario(Session["idEmpresa"].ToString());
        
        DataTable tablaDatos = _usuario.ObtenerEmpleadoPorIdEmpleado(ID_EMPLEADO);

        if (tablaDatos.Rows.Count <= 0)
        {
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);

            if (_usuario.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _usuario.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información del Empleado parametrizado.", Proceso.Error);
            }
        }
        else
        {
            DataRow filaDatos = tablaDatos.Rows[0];

            Decimal ID_SOLICITUD = Convert.ToDecimal(filaDatos["ID_SOLICITUD"]);
            Decimal ID_CONTRATO = Convert.ToDecimal(filaDatos["ID_CONTRATO"]);
            Decimal ID_EMPRESA = Convert.ToDecimal(filaDatos["ID_EMPRESA"]);
            Decimal ID_OCUPACION = Convert.ToDecimal(filaDatos["ID_OCUPACION"]);
            String ID_CIUDAD = filaDatos["ID_CIUDAD"].ToString().Trim();

            Cargar(ID_SOLICITUD, ID_CONTRATO, ID_EMPLEADO, ID_EMPRESA, ID_OCUPACION, ID_CIUDAD);   
        }
    }

    private void Iniciar()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_EMPLEADO = 0;
        
        try
        {
            ID_EMPLEADO = Convert.ToDecimal(QueryStringSeguro["ID_EMPLEADO"]);
        }
        catch
        {
            ID_EMPLEADO = 0;
        }

        Configurar();

        if (ID_EMPLEADO <= 0)
        {
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);
        }
        else
        {
            CargarEmpleadoInicial(ID_EMPLEADO);
        }
    }   

    protected void Page_Load(object sender, EventArgs e)
    {
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

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        Decimal ID_EMPLEADO = Convert.ToDecimal(HiddenField_ID_EMPLEADO.Value);
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        List<Entrega> listaProductosEntrega = new List<Entrega>();
        List<Equipo> listaEquiposEntrega = new List<Equipo>();

        for (int i = 0; i < GridView_AdjuntosAEntrega.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_AdjuntosAEntrega.Rows[i];


            Decimal ID_INDEX = Convert.ToDecimal(GridView_AdjuntosAEntrega.DataKeys[i].Values["ID_INDEX"]);
            Decimal ID_DETALLE_ENTREGA = Convert.ToDecimal(GridView_AdjuntosAEntrega.DataKeys[i].Values["ID_DETALLE_ENTREGA"]);
            Decimal ID_ASIGNACION_SC = Convert.ToDecimal(GridView_AdjuntosAEntrega.DataKeys[i].Values["ID_ASIGNACION_SC"]);
            Decimal ID_LOTE = Convert.ToDecimal(GridView_AdjuntosAEntrega.DataKeys[i].Values["ID_LOTE"]);
            Decimal ID_DOCUMENTO = Convert.ToDecimal(GridView_AdjuntosAEntrega.DataKeys[i].Values["ID_DOCUMENTO"]);
            Decimal ID_PRODUCTO = Convert.ToDecimal(GridView_AdjuntosAEntrega.DataKeys[i].Values["ID_PRODUCTO"]);
            Int32 CANTIDAD_TOTAL = Convert.ToInt32(GridView_AdjuntosAEntrega.DataKeys[i].Values["CANTIDAD_TOTAL"]);
            DateTime FECHA_PROYECTADA_ENTREGA = Convert.ToDateTime(GridView_AdjuntosAEntrega.DataKeys[i].Values["FECHA_PROYECTADA_ENTREGA"]);
            String TIPO_ENTREGA = GridView_AdjuntosAEntrega.DataKeys[i].Values["TIPO_ENTREGA"].ToString();

            TextBox textoCantidad = filaGrilla.FindControl("TextBox_Cantidad") as TextBox;
            Int32 CANTIDAD = Convert.ToInt32(textoCantidad.Text);

            TextBox textoTalla = filaGrilla.FindControl("TextBox_Talla") as TextBox;
            String TALLA = textoTalla.Text;

            Entrega _entregaParaLista = new Entrega();

            _entregaParaLista.CANTIDAD = CANTIDAD;
            _entregaParaLista.ID_ASIGNACION_SC = ID_ASIGNACION_SC;
            _entregaParaLista.ID_DETALLE_ENTREGA = ID_DETALLE_ENTREGA;
            _entregaParaLista.ID_DOCUMENTO = ID_DOCUMENTO;
            _entregaParaLista.ID_INDEX = ID_INDEX;
            _entregaParaLista.ID_LOTE = ID_LOTE;
            _entregaParaLista.TALLA = TALLA;
            _entregaParaLista.ID_PRODUCTO = ID_PRODUCTO;
            _entregaParaLista.CANTIDAD_TOTAL = CANTIDAD_TOTAL;
            _entregaParaLista.FECHA_PROYECTADA_ENTREGA = FECHA_PROYECTADA_ENTREGA;
            _entregaParaLista.TIPO_ENTREGA = TIPO_ENTREGA;

            listaProductosEntrega.Add(_entregaParaLista);
        }


        for (int i = 0; i < GridView_EquiposAdjuntosAEntrega.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_EquiposAdjuntosAEntrega.Rows[i];


            Decimal ID_INDEX = Convert.ToDecimal(GridView_EquiposAdjuntosAEntrega.DataKeys[i].Values["ID_INDEX"]);
            Decimal ID_DETALLE_ENTREGA = Convert.ToDecimal(GridView_EquiposAdjuntosAEntrega.DataKeys[i].Values["ID_DETALLE_ENTREGA"]);
            Decimal ID_ASIGNACION_SC = Convert.ToDecimal(GridView_EquiposAdjuntosAEntrega.DataKeys[i].Values["ID_ASIGNACION_SC"]);
            Decimal ID_LOTE = Convert.ToDecimal(GridView_EquiposAdjuntosAEntrega.DataKeys[i].Values["ID_LOTE"]);
            Decimal ID_DOCUMENTO = Convert.ToDecimal(GridView_EquiposAdjuntosAEntrega.DataKeys[i].Values["ID_DOCUMENTO"]);
            Decimal ID_EQUIPO = Convert.ToDecimal(GridView_EquiposAdjuntosAEntrega.DataKeys[i].Values["ID_EQUIPO"]);
            Decimal ID_PRODUCTO = Convert.ToDecimal(GridView_EquiposAdjuntosAEntrega.DataKeys[i].Values["ID_PRODUCTO"]);
            DateTime FECHA_PROYECTADA_ENTREGA = Convert.ToDateTime(GridView_EquiposAdjuntosAEntrega.DataKeys[i].Values["FECHA_PROYECTADA_ENTREGA"]);
            String TIPO_ENTREGA = GridView_EquiposAdjuntosAEntrega.DataKeys[i].Values["TIPO_ENTREGA"].ToString();
            Int32 CANTIDAD_TOTAL = Convert.ToInt32(GridView_EquiposAdjuntosAEntrega.DataKeys[i].Values["CANTIDAD_TOTAL"]);

            Equipo _equipoParaLista = new Equipo();

            _equipoParaLista.FECHA_PROYECTADA_ENTREGA = FECHA_PROYECTADA_ENTREGA;
            _equipoParaLista.ID_ASIGNACION_SC = ID_ASIGNACION_SC;
            _equipoParaLista.ID_DETALLE_ENTREGA = ID_DETALLE_ENTREGA;
            _equipoParaLista.ID_DOCUMENTO = ID_DOCUMENTO;
            _equipoParaLista.ID_EQUIPO = ID_EQUIPO;
            _equipoParaLista.ID_INDEX = ID_INDEX;
            _equipoParaLista.ID_LOTE = ID_LOTE;
            _equipoParaLista.ID_PRODUCTO = ID_PRODUCTO;
            _equipoParaLista.TIPO_ENTREGA = TIPO_ENTREGA;
            _equipoParaLista.CANTIDAD_TOTAL = CANTIDAD_TOTAL;

            listaEquiposEntrega.Add(_equipoParaLista);
        }

        Entrega _entrega = new Entrega(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Decimal ID_DOCUMENTO_ENTREGA = _entrega.AdicionarEntregaProductos(ID_EMPLEADO, listaProductosEntrega, listaEquiposEntrega);

        if (ID_DOCUMENTO_ENTREGA <= 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _entrega.MensajeError, Proceso.Error);
        }
        else
        {
            Ocultar(Acciones.ImprimirEntrega);
            Mostrar(Acciones.ImprimirEntrega);

            HiddenField_ID_DOCUMENTO_ENTREGA.Value = ID_DOCUMENTO_ENTREGA.ToString();

            DataTable tablaParaGrillaPendientes = ObtenerTablaConDatosDePendientes(ID_EMPLEADO);

            if (tablaParaGrillaPendientes.Rows.Count > 0)
            {
                Cargar_GridView_Entregas_desdeTabla(tablaParaGrillaPendientes);
            }
            else
            {
                Panel_GrillaEntregasProximas.Visible = false;
            }

            inhabilitarFilasGrilla(GridView_Entregas, 1);

            GridView_Entregas.Columns[0].Visible = false;

            Panel_AdjuntosAEntrega.Visible = false;
            GridView_AdjuntosAEntrega.DataSource = null;
            GridView_AdjuntosAEntrega.DataBind();

            Panel_EquiposAdjuntosAEntrega.Visible = false;
            GridView_EquiposAdjuntosAEntrega.DataSource = null;
            GridView_EquiposAdjuntosAEntrega.DataBind();

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La entrega se registró correctamente, ahora puede imprimir el acta de entrega.", Proceso.Correcto);
        }
    }

    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }

    private byte[] ImprimirPdfEntrega(Decimal ID_DOCUMENTO)
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
            using (SqlCommand comando = new SqlCommand("RPT_COMPRAS_PDF_ENTREGA_DOT_EPP_EQUIPO " + ID_DOCUMENTO.ToString(), conn))
            {
                using (SqlDataAdapter adaptador = new SqlDataAdapter(comando))
                {
                    DataSet ds = new DataSet();
                    adaptador.Fill(ds);

                    reporte = new ReportDocument();

                    reporte.Load(Server.MapPath("~/Reportes/ComprasInventarios/RPT_COMPRAS_PDF_ENTREGA_DOT_EPP_EQUIPO.rpt"));
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

    protected void Button_IMPRIMIR_Click(object sender, EventArgs e)
    {
        Decimal ID_DOCUMENTO = Convert.ToDecimal(HiddenField_ID_DOCUMENTO_ENTREGA.Value);
        byte[] archivo = ImprimirPdfEntrega(ID_DOCUMENTO);

        Response.AddHeader("Content-Disposition", "attachment;FileName=entrega_" + ID_DOCUMENTO.ToString() + ".pdf");
        Response.Clear();
        Response.Buffer = true;
        Response.ContentType = "application/pdf";
        Response.BinaryWrite(archivo);
        Response.End();
    }

    protected void DropDownList_BUSCAR_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_BUSCAR.SelectedValue == "")
        {
            configurarCaracteresAceptadosBusqueda(true, true);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "NUM_DOC_IDENTIFICACION")
        {
            configurarCaracteresAceptadosBusqueda(false, true);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "NOMBRES")
        {
            configurarCaracteresAceptadosBusqueda(true, false);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "APELLIDOS")
        {
            configurarCaracteresAceptadosBusqueda(true, false);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "RAZ_SOCIAL")
        {
            configurarCaracteresAceptadosBusqueda(true, false);
        }

        TextBox_BUSCAR.Text = "";

        TextBox_BUSCAR.Focus();
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

        String datosCapturados = HiddenField_FILTRO_DATO.Value;
        String campo = HiddenField_FILTRO_DROP.Value;

        registroContrato _registroContrato = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaResultadosBusqueda = new DataTable();

        if (campo == "NUM_DOC_IDENTIFICACION")
        {
            tablaResultadosBusqueda = _registroContrato.ObtenerContratoPorNumDocIdentidadSoloActivos(datosCapturados);
        }
        else
        {
            if (campo == "NOMBRES")
            {
                tablaResultadosBusqueda = _registroContrato.ObtenerContratoPorNombreSoloActivos(datosCapturados); 
            }
            else
            {
                if (campo == "APELLIDOS")
                {
                    tablaResultadosBusqueda = _registroContrato.ObtenerContratoPorApellidosSoloActivos(datosCapturados); 
                }
                else
                {
                    if (campo == "RAZ_SOCIAL")
                    {
                        tablaResultadosBusqueda = _registroContrato.ObtenerContratoPorRazSocialSoloActivos(datosCapturados); 
                    }
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
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registron que cumplieran los parametros de busqueda.", Proceso.Advertencia);
            }

            GridView_RESULTADOS_BUSQUEDA.DataSource = null;
            GridView_RESULTADOS_BUSQUEDA.DataBind();

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

        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Buscar);

        Buscar();
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

    private void Cargar_DropDownList_Proveedor(DropDownList drop, DataTable tablaProveedores)
    {
        drop.Items.Clear();

        drop.Items.Add(new ListItem("Seleccione...", ""));

        foreach(DataRow filaProveedor in tablaProveedores.Rows)
        {
            drop.Items.Add(new ListItem(filaProveedor["NOMBRE_PROVEEDOR"].ToString(), filaProveedor["ID_PROVEEDOR"].ToString()));
        }

        drop.DataBind();
    }   

    protected void DropDownList_TallaProducto_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_TallaProducto.SelectedIndex <= 0)
        {
            Cargar_DropDownList_Vacio(DropDownList_Proveedor);
            Cargar_DropDownList_Vacio(DropDownList_Factura);

            TextBox_CantidadProducto.Text = "";

            Label_CantidadMax.Text = "0";

            Label_CantidadDisponible.Text = "0";

            RangeValidator_TextBox_CantidadProducto.MaximumValue = "0";
            RangeValidator_TextBox_CantidadProducto.MinimumValue = "0";
        }
        else
        {
            Entrega _entrega = new Entrega(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            
            Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
            String ID_CIUDAD = HiddenField_ID_CIUDAD.Value;
            Decimal ID_PRODUCTO = Convert.ToDecimal(HiddenField_ID_PRODUCTO_SELECCIONADO.Value);
            DataTable tablaProveedores = _entrega.ObtenerProveedoresPorProductoCiudadEmpresaYTalla(ID_EMPRESA, ID_CIUDAD, ID_PRODUCTO, DropDownList_TallaProducto.SelectedValue);

            Cargar_DropDownList_Proveedor(DropDownList_Proveedor, tablaProveedores);

            Cargar_DropDownList_Vacio(DropDownList_Factura);

            TextBox_CantidadProducto.Text = "";

            Label_CantidadMax.Text = "0";

            Label_CantidadDisponible.Text = "0";

            RangeValidator_TextBox_CantidadProducto.MaximumValue = "0";
            RangeValidator_TextBox_CantidadProducto.MinimumValue = "0";
        }
    }

    private void Cargar_DropDownList_Factura(DropDownList drop, DataTable tablaFacturas)
    {
        drop.Items.Clear();

        drop.Items.Add(new ListItem("Seleccione...", ""));

        foreach (DataRow filaFactura in tablaFacturas.Rows)
        {
            drop.Items.Add(new ListItem("FACT: " + filaFactura["NUMERO_DOCUMENTO"].ToString() + " - LOT: " + filaFactura["ID_LOTE"].ToString() + " - " + Convert.ToDateTime(filaFactura["FECHA_REGISTRO"]).ToShortDateString(), filaFactura["ID_DOCUMENTO"].ToString() + ":" + filaFactura["ID_LOTE"].ToString()));
        }

        drop.DataBind();
    }

    protected void DropDownList_Proveedor_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_Proveedor.SelectedIndex <= 0)
        {
            Cargar_DropDownList_Vacio(DropDownList_Factura);

            TextBox_CantidadProducto.Text = "";

            Label_CantidadMax.Text = "0";

            Label_CantidadDisponible.Text = "0";

            RangeValidator_TextBox_CantidadProducto.MaximumValue = "0";
            RangeValidator_TextBox_CantidadProducto.MinimumValue = "0";
        }
        else
        {
            Entrega _entrega = new Entrega(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
            String ID_CIUDAD = HiddenField_ID_CIUDAD.Value;
            Decimal ID_PRODUCTO = Convert.ToDecimal(HiddenField_ID_PRODUCTO_SELECCIONADO.Value);
            Decimal ID_PROVEEDOR = Convert.ToDecimal(DropDownList_Proveedor.SelectedValue);

            DataTable tablaFacturas = _entrega.ObtenerFacturasPorProveedorProductoCiudadEmpresaYTalla(ID_EMPRESA, ID_CIUDAD, ID_PRODUCTO, DropDownList_TallaProducto.SelectedValue, ID_PROVEEDOR);

            Cargar_DropDownList_Factura(DropDownList_Factura, tablaFacturas);

            TextBox_CantidadProducto.Text = "";

            Label_CantidadMax.Text = "0";

            Label_CantidadDisponible.Text = "0";

            RangeValidator_TextBox_CantidadProducto.MaximumValue = "0";
            RangeValidator_TextBox_CantidadProducto.MinimumValue = "0";
        }
    }

    private Int32 ObtieneCantidadRealDisponibleEnLote(Int32 cantidadEnLote, Decimal ID_LOTE)
    {
        DataTable tablaGrillaAdjuntos = ObtenerDataTableDesde_GridView_AdjuntosAEntrega();

        Int32 cantidadAdjunta = 0;

        foreach (DataRow filaGrilla in tablaGrillaAdjuntos.Rows)
        {
            Decimal ID_LOTE_GRILLA = Convert.ToInt32(filaGrilla["ID_LOTE"]);
            Int32 CANTIDAD_GRILLA =Convert.ToInt32(filaGrilla["CANTIDAD"]);

            if(ID_LOTE_GRILLA == ID_LOTE)
            {
                cantidadAdjunta += CANTIDAD_GRILLA;
            }
        }

        return (cantidadEnLote - cantidadAdjunta);
    }

    private Int32 ObtieneCantidadRealDisponibleEnLoteEquipos(Int32 cantidadEnLote, Decimal ID_LOTE)
    {
        DataTable tablaGrillaAdjuntos = ObtenerDataTableDesde_GridView_EquiposAdjuntosAEntrega(); 

        Int32 cantidadAdjunta = 0;

        foreach (DataRow filaGrilla in tablaGrillaAdjuntos.Rows)
        {
            Decimal ID_LOTE_GRILLA = Convert.ToInt32(filaGrilla["ID_LOTE"]);

            if (ID_LOTE_GRILLA == ID_LOTE)
            {
                cantidadAdjunta += 1;
            }
        }

        return (cantidadEnLote - cantidadAdjunta);
    }

    protected void DropDownList_Factura_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_Factura.SelectedIndex <= 0)
        {
            TextBox_CantidadProducto.Text = "";

            Label_CantidadMax.Text = "0";

            Label_CantidadDisponible.Text = "0";

            RangeValidator_TextBox_CantidadProducto.MaximumValue = "0";
            RangeValidator_TextBox_CantidadProducto.MinimumValue = "0";
        }
        else
        {
            Entrega _entrega = new Entrega(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            String[] datosArray = DropDownList_Factura.SelectedValue.Split(':');

            Decimal ID_DOCUMENTO = Convert.ToDecimal(datosArray[0]);
            Decimal ID_LOTE = Convert.ToDecimal(datosArray[1]);

            DataTable tablaInfoLote = _entrega.ObtenerCantidadesEnLote(ID_DOCUMENTO, ID_LOTE);

            DataRow filaInfoLote = tablaInfoLote.Rows[0];

            Int32 CANTIDAD_TOTAL = Convert.ToInt32(HiddenField_CANTIDAD_TOTAL.Value);
            Int32 CANTIDAD_ENTREGADA = Convert.ToInt32(HiddenField_CANTIDAD_ENTREGADA.Value);
            Int32 CANTIDAD_DISPONIBLE_LOTE = ObtieneCantidadRealDisponibleEnLoteEquipos(Convert.ToInt32(filaInfoLote["CANTIDAD_DISPONIBLE"]), ID_LOTE);

            TextBox_CantidadProducto.Text = "";

            Label_CantidadMax.Text = (CANTIDAD_TOTAL - CANTIDAD_ENTREGADA).ToString();

            Label_CantidadDisponible.Text = CANTIDAD_DISPONIBLE_LOTE.ToString();

            if ((CANTIDAD_TOTAL - CANTIDAD_ENTREGADA) < CANTIDAD_DISPONIBLE_LOTE)
            {
                RangeValidator_TextBox_CantidadProducto.MaximumValue = (CANTIDAD_TOTAL - CANTIDAD_ENTREGADA).ToString();
            }
            else
            {
                RangeValidator_TextBox_CantidadProducto.MaximumValue = CANTIDAD_DISPONIBLE_LOTE.ToString();
            }
            
            RangeValidator_TextBox_CantidadProducto.MinimumValue = "1";
        }
    }

    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;

        Buscar();
    }

    private DataTable obtenerEstructuraTablaPendientes()
    {
        DataTable tablaResultado = new DataTable();

        tablaResultado.Columns.Add("ID_INDEX");
        tablaResultado.Columns.Add("ID_ASIGNACION_SC");
        tablaResultado.Columns.Add("ID_EMPLEADO");
        tablaResultado.Columns.Add("ID_PRODUCTO");
        tablaResultado.Columns.Add("NOMBRE_PRODUCTO");
        tablaResultado.Columns.Add("CANTIDAD_TOTAL");
        tablaResultado.Columns.Add("CANTIDAD_ENTREGADA_INICIAL");
        tablaResultado.Columns.Add("CANTIDAD_ENTREGADA");
        tablaResultado.Columns.Add("FECHA_PROYECTADA");
        tablaResultado.Columns.Add("TIPO_ENTERGA");
        tablaResultado.Columns.Add("NOMBRE_SERVICIO_COMPLEMENTARIO");

        return tablaResultado;
    }

    private DateTime ObtenerFechaProyectadaDeEntrega(DateTime fechaBase, String periodicidad)
    { 
        Int32 periodicidadEntera = Convert.ToInt32(periodicidad);
        DateTime fechaResultado = fechaBase.AddMonths(periodicidadEntera);

        return fechaResultado;
    }

    private Int32 ObtenerResultadoCuandoEspecificoMayorQueContrato(Int32 numeroEspecifico, Int32 numeroContrato, Int32 periodicidadEntero)
    {
        Int32 numeroResultado = 0;

        if ((numeroContrato + periodicidadEntero) >= numeroEspecifico)
        {
            numeroResultado = numeroEspecifico;
        }
        else
        {
            numeroResultado = ObtenerResultadoCuandoEspecificoMayorQueContrato((numeroEspecifico - periodicidadEntero), numeroContrato, periodicidadEntero);
        }

        return numeroResultado;
    }

    private Int32 ObtenerResultadoCuandoEspecificoMenorQueContrato(Int32 numeroEspecifico, Int32 numeroContrato, Int32 periodicidadEntero)
    {
        Int32 numeroResultado = 0;

        if ((numeroEspecifico + periodicidadEntero) >= numeroContrato)
        {
            numeroResultado = numeroEspecifico + periodicidadEntero;
            if (numeroResultado > 1231)
            {
                numeroResultado -= 1200;
            }
        }
        else
        {
            numeroResultado = ObtenerResultadoCuandoEspecificoMenorQueContrato((numeroEspecifico + periodicidadEntero), numeroContrato, periodicidadEntero);
        }

        return numeroResultado;
    }

    private int[] ObtenerMesYDia(String mesYDia)
    {
        if (mesYDia.Length == 3)
        {
            mesYDia = "0" + mesYDia;
        }

        int mes = Convert.ToInt32(mesYDia.Substring(0, 2));
        int dia = Convert.ToInt32(mesYDia.Substring(2, 2));

        int[] resultado = new Int32[2];

        resultado[0] = mes;
        resultado[1] = dia;

        return resultado;
    }

    private DateTime ObtenerFechaProyectadaDeEntregaAjsutadaAFechaEspecifica(DateTime fechaEspecifica, DateTime fechaContrato, String periodicidad)
    {
        DateTime fechaResultado = new DateTime();

        Int32 periodicidadEntero = (Convert.ToInt32(periodicidad) * 100);

        Int32 numeroEspecifico = (fechaEspecifica.Month * 100) + fechaEspecifica.Day;
        Int32 numeroContrato = (fechaContrato.Month * 100) + fechaContrato.Day;

        Int32 numeroResultado = 0;
        if (numeroEspecifico > numeroContrato)
        {
            numeroResultado = ObtenerResultadoCuandoEspecificoMayorQueContrato(numeroEspecifico, numeroContrato, periodicidadEntero);
            int[] mesYdia = ObtenerMesYDia(numeroResultado.ToString());

            fechaResultado = new DateTime(DateTime.Now.Year, mesYdia[0], mesYdia[1]);
        }
        else
        {
            if (numeroEspecifico < numeroContrato)
            {
                numeroResultado = ObtenerResultadoCuandoEspecificoMenorQueContrato(numeroEspecifico, numeroContrato, periodicidadEntero);
                int[] mesYdia = ObtenerMesYDia(numeroResultado.ToString());

                if (numeroResultado < numeroContrato)
                {
                    fechaResultado = new DateTime(DateTime.Now.Year + 1, mesYdia[0], mesYdia[1]);
                }
                else
                {
                    fechaResultado = new DateTime(DateTime.Now.Year, mesYdia[0], mesYdia[1]);
                }
            }
        }

        return fechaResultado;
    }

    private DataTable ObtenerTablaFinalDePendientes(DataTable tablaInfoContrato, Decimal ID_EMPLEADO)
    {
        Boolean correcto = true;

        DataRow filaContrato = tablaInfoContrato.Rows[0];

        DateTime FECHA_INICIA_CONTRATO = Convert.ToDateTime(filaContrato["FECHA_INICIA"]);

        DataTable tablaParaGrilla = obtenerEstructuraTablaPendientes();

        Entrega _entrega = new Entrega(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaConfiguracionEntregas = _entrega.ObtenerConfiguracionEntregasPorEmpleado(ID_EMPLEADO);

        Int32 contador = 0;

        if (tablaConfiguracionEntregas.Rows.Count <= 0)
        {
            if (_entrega.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _entrega.MensajeError, Proceso.Error);
                correcto = false;
            }
        }

        if (correcto == true)
        {

            for (int i = 0; i < tablaConfiguracionEntregas.Rows.Count; i++)
            {
                DataRow filaConfiguracion = tablaConfiguracionEntregas.Rows[i];

                if (filaConfiguracion["PRIMERA_ENTREGA"].ToString() == "True")
                {
                    _entrega.MensajeError = null;

                    DataTable tablaInformacionEntregaInicial = _entrega.ObtenerInformacionPrimeraEntregaProductoYEmpleado(ID_EMPLEADO, Convert.ToDecimal(filaConfiguracion["ID_PRODUCTO"]), TiposEntrega.INICIAL.ToString());
                    if (_entrega.MensajeError != null)
                    {
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _entrega.MensajeError, Proceso.Error);
                        correcto = false;
                        break;
                    }

                    if (tablaInformacionEntregaInicial.Rows.Count <= 0)
                    {
                        DataRow filaParaGrilla = tablaParaGrilla.NewRow();

                        filaParaGrilla["ID_INDEX"] = contador;
                        contador += 1;
                        filaParaGrilla["ID_ASIGNACION_SC"] = 0;
                        filaParaGrilla["ID_EMPLEADO"] = ID_EMPLEADO;
                        filaParaGrilla["ID_PRODUCTO"] = filaConfiguracion["ID_PRODUCTO"];
                        filaParaGrilla["NOMBRE_PRODUCTO"] = filaConfiguracion["NOMBRE_PRODUCTO"];
                        filaParaGrilla["CANTIDAD_TOTAL"] = filaConfiguracion["CANTIDAD"];
                        filaParaGrilla["CANTIDAD_ENTREGADA_INICIAL"] = 0;
                        filaParaGrilla["CANTIDAD_ENTREGADA"] = 0;
                        filaParaGrilla["FECHA_PROYECTADA"] = FECHA_INICIA_CONTRATO;
                        filaParaGrilla["TIPO_ENTERGA"] = TiposEntrega.INICIAL.ToString();
                        filaParaGrilla["NOMBRE_SERVICIO_COMPLEMENTARIO"] = filaConfiguracion["NOMBRE_SERVICIO_COMPLEMENTARIO"];

                        tablaParaGrilla.Rows.Add(filaParaGrilla);
                    }
                    else
                    {
                        DataRow filaInformacionEntregaInicial = tablaInformacionEntregaInicial.Rows[0];

                        if (filaInformacionEntregaInicial["ESTADO"].ToString() == EstadosAsignacionSC.ABIERTA.ToString())
                        {
                            DataRow filaParaGrilla = tablaParaGrilla.NewRow();

                            filaParaGrilla["ID_INDEX"] = contador;
                            contador += 1;
                            filaParaGrilla["ID_ASIGNACION_SC"] = filaInformacionEntregaInicial["ID_ASIGNACION_SC"];
                            filaParaGrilla["ID_EMPLEADO"] = ID_EMPLEADO;
                            filaParaGrilla["ID_PRODUCTO"] = filaInformacionEntregaInicial["ID_PRODUCTO"];
                            filaParaGrilla["NOMBRE_PRODUCTO"] = filaInformacionEntregaInicial["NOMBRE_PRODUCTO"];
                            filaParaGrilla["CANTIDAD_TOTAL"] = filaInformacionEntregaInicial["CANTIDAD_TOTAL"];
                            filaParaGrilla["CANTIDAD_ENTREGADA_INICIAL"] = filaInformacionEntregaInicial["CANTIDAD_ENTREGADA"];
                            filaParaGrilla["CANTIDAD_ENTREGADA"] = filaInformacionEntregaInicial["CANTIDAD_ENTREGADA"];
                            filaParaGrilla["FECHA_PROYECTADA"] = Convert.ToDateTime(filaInformacionEntregaInicial["FCH_PROYECTA_ENTREGA"]);
                            filaParaGrilla["TIPO_ENTERGA"] = TiposEntrega.INICIAL.ToString();
                            filaParaGrilla["NOMBRE_SERVICIO_COMPLEMENTARIO"] = filaConfiguracion["NOMBRE_SERVICIO_COMPLEMENTARIO"];

                            tablaParaGrilla.Rows.Add(filaParaGrilla);
                        }
                    }
                }
                else
                {
                    _entrega.MensajeError = null;
                    DataTable tablaInformacionEntregasProgramadasProducto = _entrega.ObtenerInformacionPrimeraEntregaProductoYEmpleado(ID_EMPLEADO, Convert.ToDecimal(filaConfiguracion["ID_PRODUCTO"]), TiposEntrega.PROGRAMADA.ToString());
                    if (_entrega.MensajeError != null)
                    {
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _entrega.MensajeError, Proceso.Error);
                        correcto = false;
                        break;
                    }

                    if (tablaInformacionEntregasProgramadasProducto.Rows.Count <= 0)
                    {
                        String AJUSTE_A = filaConfiguracion["AJUSTE_A"].ToString();

                        if (AJUSTE_A == TiposAjusteA.CONTRATO.ToString())
                        {
                            DateTime FECHA_PROYECTADA_ENTREGA = ObtenerFechaProyectadaDeEntrega(FECHA_INICIA_CONTRATO, filaConfiguracion["PERIODICIDAD"].ToString());

                            DataRow filaParaGrilla = tablaParaGrilla.NewRow();

                            filaParaGrilla["ID_INDEX"] = contador;
                            contador += 1;
                            filaParaGrilla["ID_ASIGNACION_SC"] = 0;
                            filaParaGrilla["ID_EMPLEADO"] = ID_EMPLEADO;
                            filaParaGrilla["ID_PRODUCTO"] = filaConfiguracion["ID_PRODUCTO"];
                            filaParaGrilla["NOMBRE_PRODUCTO"] = filaConfiguracion["NOMBRE_PRODUCTO"];
                            filaParaGrilla["CANTIDAD_TOTAL"] = filaConfiguracion["CANTIDAD"];
                            filaParaGrilla["CANTIDAD_ENTREGADA_INICIAL"] = 0;
                            filaParaGrilla["CANTIDAD_ENTREGADA"] = 0;
                            filaParaGrilla["FECHA_PROYECTADA"] = FECHA_PROYECTADA_ENTREGA;
                            filaParaGrilla["TIPO_ENTERGA"] = TiposEntrega.PROGRAMADA.ToString();
                            filaParaGrilla["NOMBRE_SERVICIO_COMPLEMENTARIO"] = filaConfiguracion["NOMBRE_SERVICIO_COMPLEMENTARIO"];

                            tablaParaGrilla.Rows.Add(filaParaGrilla);
                        }
                        else
                        {
                            DateTime FECHA_PROYECTADA_ENTREGA = ObtenerFechaProyectadaDeEntregaAjsutadaAFechaEspecifica(Convert.ToDateTime(filaConfiguracion["FECHA_AJUSTE"]), FECHA_INICIA_CONTRATO, filaConfiguracion["PERIODICIDAD"].ToString());

                            DataRow filaParaGrilla = tablaParaGrilla.NewRow();

                            filaParaGrilla["ID_INDEX"] = contador;
                            contador += 1;
                            filaParaGrilla["ID_ASIGNACION_SC"] = 0;
                            filaParaGrilla["ID_EMPLEADO"] = ID_EMPLEADO;
                            filaParaGrilla["ID_PRODUCTO"] = filaConfiguracion["ID_PRODUCTO"];
                            filaParaGrilla["NOMBRE_PRODUCTO"] = filaConfiguracion["NOMBRE_PRODUCTO"];
                            filaParaGrilla["CANTIDAD_TOTAL"] = filaConfiguracion["CANTIDAD"];
                            filaParaGrilla["CANTIDAD_ENTREGADA_INICIAL"] = 0;
                            filaParaGrilla["CANTIDAD_ENTREGADA"] = 0;
                            filaParaGrilla["FECHA_PROYECTADA"] = FECHA_PROYECTADA_ENTREGA;
                            filaParaGrilla["TIPO_ENTERGA"] = TiposEntrega.PROGRAMADA.ToString();
                            filaParaGrilla["NOMBRE_SERVICIO_COMPLEMENTARIO"] = filaConfiguracion["NOMBRE_SERVICIO_COMPLEMENTARIO"];

                            tablaParaGrilla.Rows.Add(filaParaGrilla);
                        }
                    }
                    else
                    { 
                        Boolean existeAbierta = false;
                        DateTime fechaUltimaEntrega = new DateTime();

                        for (int j = 0; j < tablaInformacionEntregasProgramadasProducto.Rows.Count; j++)
                        {
                            DataRow filaEntregaProgramada = tablaInformacionEntregasProgramadasProducto.Rows[j];
                            DateTime fechaProyectadaEntrega = Convert.ToDateTime(filaEntregaProgramada["FCH_PROYECTA_ENTREGA"]);

                            if (fechaProyectadaEntrega > fechaUltimaEntrega)
                            {
                                fechaUltimaEntrega = fechaProyectadaEntrega;
                            }

                            if (filaEntregaProgramada["ESTADO"].ToString() == EstadosAsignacionSC.ABIERTA.ToString())
                            {
                                existeAbierta = true;

                                DataRow filaParaGrilla = tablaParaGrilla.NewRow();

                                filaParaGrilla["ID_INDEX"] = contador;
                                contador += 1;
                                filaParaGrilla["ID_ASIGNACION_SC"] = filaEntregaProgramada["ID_ASIGNACION_SC"];
                                filaParaGrilla["ID_EMPLEADO"] = ID_EMPLEADO;
                                filaParaGrilla["ID_PRODUCTO"] = filaEntregaProgramada["ID_PRODUCTO"];
                                filaParaGrilla["NOMBRE_PRODUCTO"] = filaEntregaProgramada["NOMBRE_PRODUCTO"];
                                filaParaGrilla["CANTIDAD_TOTAL"] = filaEntregaProgramada["CANTIDAD_TOTAL"];
                                filaParaGrilla["CANTIDAD_ENTREGADA_INICIAL"] = filaEntregaProgramada["CANTIDAD_ENTREGADA"];
                                filaParaGrilla["CANTIDAD_ENTREGADA"] = filaEntregaProgramada["CANTIDAD_ENTREGADA"];
                                filaParaGrilla["FECHA_PROYECTADA"] = filaEntregaProgramada["FCH_PROYECTA_ENTREGA"];
                                filaParaGrilla["TIPO_ENTERGA"] = TiposEntrega.PROGRAMADA.ToString();
                                filaParaGrilla["NOMBRE_SERVICIO_COMPLEMENTARIO"] = filaConfiguracion["NOMBRE_SERVICIO_COMPLEMENTARIO"];

                                tablaParaGrilla.Rows.Add(filaParaGrilla);
                            }

                            if (existeAbierta == false)
                            {
                                DateTime FECHA_PROXIMA_ENTREGA = new DateTime();

                                if (filaConfiguracion["AJUSTE_A"].ToString() == TiposAjusteA.CONTRATO.ToString())
                                {
                                    FECHA_PROXIMA_ENTREGA = ObtenerFechaProyectadaDeEntregaAjsutadaAFechaEspecifica(FECHA_INICIA_CONTRATO, fechaUltimaEntrega.AddDays(1), filaConfiguracion["PERIODICIDAD"].ToString());
                                }
                                else
                                {
                                    DateTime fechaAjusteA = Convert.ToDateTime(filaConfiguracion["FECHA_AJUSTE"]);
                                    FECHA_PROXIMA_ENTREGA = ObtenerFechaProyectadaDeEntregaAjsutadaAFechaEspecifica(fechaAjusteA, fechaUltimaEntrega.AddDays(1), filaConfiguracion["PERIODICIDAD"].ToString());
                                }

                                DataRow filaParaGrilla = tablaParaGrilla.NewRow();

                                filaParaGrilla["ID_INDEX"] = contador;
                                contador += 1;
                                filaParaGrilla["ID_ASIGNACION_SC"] = 0;
                                filaParaGrilla["ID_EMPLEADO"] = ID_EMPLEADO;
                                filaParaGrilla["ID_PRODUCTO"] = filaConfiguracion["ID_PRODUCTO"];
                                filaParaGrilla["NOMBRE_PRODUCTO"] = filaConfiguracion["NOMBRE_PRODUCTO"];
                                filaParaGrilla["CANTIDAD_TOTAL"] = filaConfiguracion["CANTIDAD"];
                                filaParaGrilla["CANTIDAD_ENTREGADA_INICIAL"] = 0;
                                filaParaGrilla["CANTIDAD_ENTREGADA"] = 0;
                                filaParaGrilla["FECHA_PROYECTADA"] = FECHA_PROXIMA_ENTREGA;
                                filaParaGrilla["TIPO_ENTERGA"] = TiposEntrega.PROGRAMADA.ToString();
                                filaParaGrilla["NOMBRE_SERVICIO_COMPLEMENTARIO"] = filaConfiguracion["NOMBRE_SERVICIO_COMPLEMENTARIO"];

                                tablaParaGrilla.Rows.Add(filaParaGrilla);
                            }
                        }
                    }
                }
            }
        }

        if (correcto == true)
        {
            _entrega.MensajeError = null;
            DataTable tablaInformacionEntregasEspecialesProducto = _entrega.ObtenerInformacionEntregasPorEmpleadoyTipoEntrega(ID_EMPLEADO, TiposEntrega.ESPECIAL.ToString());
            if (_entrega.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _entrega.MensajeError, Proceso.Error);
                correcto = false;
            }

            if (correcto == true)
            {
                for (int i = 0; i < tablaInformacionEntregasEspecialesProducto.Rows.Count; i++)
                {
                    DataRow filainformacionEntregaEspecial = tablaInformacionEntregasEspecialesProducto.Rows[i];

                    if (filainformacionEntregaEspecial["ESTADO"].ToString() == EstadosAsignacionSC.ABIERTA.ToString())
                    {
                        DataRow filaParaGrilla = tablaParaGrilla.NewRow();

                        filaParaGrilla["ID_INDEX"] = contador;
                        contador += 1;
                        filaParaGrilla["ID_ASIGNACION_SC"] = filainformacionEntregaEspecial["ID_ASIGNACION_SC"];
                        filaParaGrilla["ID_EMPLEADO"] = ID_EMPLEADO;
                        filaParaGrilla["ID_PRODUCTO"] = filainformacionEntregaEspecial["ID_PRODUCTO"];
                        filaParaGrilla["NOMBRE_PRODUCTO"] = filainformacionEntregaEspecial["NOMBRE_PRODUCTO"];
                        filaParaGrilla["CANTIDAD_TOTAL"] = filainformacionEntregaEspecial["CANTIDAD_TOTAL"];
                        filaParaGrilla["CANTIDAD_ENTREGADA_INICIAL"] = filainformacionEntregaEspecial["CANTIDAD_ENTREGADA"];
                        filaParaGrilla["CANTIDAD_ENTREGADA"] = filainformacionEntregaEspecial["CANTIDAD_ENTREGADA"];
                        filaParaGrilla["FECHA_PROYECTADA"] = filainformacionEntregaEspecial["FCH_PROYECTA_ENTREGA"];
                        filaParaGrilla["TIPO_ENTERGA"] = TiposEntrega.ESPECIAL.ToString();
                        filaParaGrilla["NOMBRE_SERVICIO_COMPLEMENTARIO"] = filainformacionEntregaEspecial["NOMBRE_SERVICIO_COMPLEMENTARIO"];

                        tablaParaGrilla.Rows.Add(filaParaGrilla);
                    }
                }
            }
        }

        return tablaParaGrilla;
    }

    private void Cargar_GridView_Entregas_desdeTabla(DataTable tablaParaGrilla)
    {
        GridView_Entregas.DataSource = tablaParaGrilla;
        GridView_Entregas.DataBind();


        for (int i = 0; i < GridView_Entregas.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_Entregas.Rows[i];
            DataRow filaTabla = tablaParaGrilla.Rows[i];

            TextBox textoNombreProducto = filaGrilla.FindControl("TextBox_NombreProducto") as TextBox;
            textoNombreProducto.Text = filaTabla["NOMBRE_PRODUCTO"].ToString();
            TextBox textoCantidadTotal = filaGrilla.FindControl("TextBox_Cantidad") as TextBox;
            textoCantidadTotal.Text = filaTabla["CANTIDAD_TOTAL"].ToString();
            TextBox textoCantidadEntregada = filaGrilla.FindControl("TextBox_CantidadEntregada") as TextBox;
            textoCantidadEntregada.Text = filaTabla["CANTIDAD_ENTREGADA"].ToString();
            TextBox textoFechaProyectada = filaGrilla.FindControl("TextBox_FechaProyectada") as TextBox;
            textoFechaProyectada.Text = Convert.ToDateTime(filaTabla["FECHA_PROYECTADA"]).ToShortDateString();
            TextBox textoTipoEntrega = filaGrilla.FindControl("TextBox_TipoEntrega") as TextBox;
            textoTipoEntrega.Text = filaTabla["TIPO_ENTERGA"].ToString();
        }
    }

    private DataTable ObtenerTablaConDatosDePendientes(Decimal ID_EMPLEADO)
    {
        DataTable tablaParaGrilla = new DataTable();

        registroContrato _registroContrato = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInformacionContrato = _registroContrato.ObtenerInformacionCotratoParaEntregas(ID_EMPLEADO);
        
        if(tablaInformacionContrato.Rows.Count <= 0)
        {
            if(_registroContrato.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _registroContrato.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información del contrato del empleado seleccionado.", Proceso.Error);    
            }
        }
        else
        {
            tablaParaGrilla = ObtenerTablaFinalDePendientes(tablaInformacionContrato, ID_EMPLEADO);
        }

        return tablaParaGrilla;
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

    private void inhabilitarFilasGrilla(GridView grilla, int colInicio, int colFin)
    {
        if (colFin > grilla.Columns.Count)
        {
            colFin = grilla.Columns.Count;
        }

        for (int i = 0; i < grilla.Rows.Count; i++)
        {
            for (int j = colInicio; j < colFin; j++)
            {
                grilla.Rows[i].Cells[j].Enabled = false;
            }
        }
    }

    private void Limpiar(Acciones accion)
    {
        switch(accion)
        {
            case Acciones.CargarPendientes:
                GridView_AdjuntosAEntrega.DataSource = null;
                GridView_AdjuntosAEntrega.DataBind();
                break;
        }
    }

    private void CargarDatosEmpleado(Decimal ID_EMPLEADO)
    {
        Boolean correcto = true;

        registroContrato _registro = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaInformacionEmpleado = _registro.ObtenerInformacionCotratoParaEntregas(ID_EMPLEADO);

        if (_registro.MensajeError != null)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _registro.MensajeError, Proceso.Error);
            correcto = false;
        }
        else
        {
            if (tablaInformacionEmpleado.Rows.Count <= 0)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información del Empleado seleccionado.", Proceso.Error);
                correcto = false;
            }
            else
            {
                DataRow filaEmpleado = tablaInformacionEmpleado.Rows[0];

                Label_NombresEmpleado.Text = filaEmpleado["NOMBRE_EMPLEADO"].ToString();
                Label_NumDocIdentidadEmpleado.Text = filaEmpleado["DOCUMENTO_IDENTIDAD"].ToString();
                Label_Empresa.Text = filaEmpleado["RAZ_SOCIAL"].ToString();
                Label_Cargo.Text = filaEmpleado["NOM_OCUPACION"].ToString();

                Label_TallaCamisa.Text = filaEmpleado["TALLA_CAMISA"].ToString();
                Label_TallaPantalon.Text = filaEmpleado["TALLA_PANTALON"].ToString();
                Label_TallaZapatos.Text = filaEmpleado["TALLA_ZAPATOS"].ToString();
            }
        }

        if (correcto == false)
        {
            Label_NombresEmpleado.Text = "Desconocido";
            Label_NumDocIdentidadEmpleado.Text = "Desconocido";
            Label_Empresa.Text = "Desconocida";
            Label_Cargo.Text = "Desconocido";

            Label_TallaCamisa.Text = "Desconocida";
            Label_TallaPantalon.Text = "Desconocida";
            Label_TallaZapatos.Text = "Desconocida";
        }
    }

    private void Cargar(Decimal ID_SOLICITUD, Decimal ID_CONTRATO, Decimal ID_EMPLEADO, Decimal ID_EMPRESA, Decimal ID_OCUPACION, String ID_CIUDAD)
    {
        HiddenField_ID_SOLICITUD.Value = ID_SOLICITUD.ToString();
        HiddenField_ID_CONTRATO.Value = ID_CONTRATO.ToString();
        HiddenField_ID_EMPLEADO.Value = ID_EMPLEADO.ToString();
        HiddenField_ID_EMPRESA.Value = ID_EMPRESA.ToString();
        HiddenField_ID_OCUPACION.Value = ID_OCUPACION.ToString();
        HiddenField_ID_CIUDAD.Value = ID_CIUDAD;

        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.CargarPendientes);
        Limpiar(Acciones.CargarPendientes);

        CargarDatosEmpleado(ID_EMPLEADO);

        DataTable tablaParaGrillaPendientes = ObtenerTablaConDatosDePendientes(ID_EMPLEADO);

        if (tablaParaGrillaPendientes.Rows.Count > 0)
        {
            Cargar_GridView_Entregas_desdeTabla(tablaParaGrillaPendientes);
        }
        else
        {
            Panel_GrillaEntregasProximas.Visible = false;
        }

        inhabilitarFilasGrilla(GridView_Entregas, 1);

        GridView_EquiposAdjuntosAEntrega.DataSource = null;
        GridView_EquiposAdjuntosAEntrega.DataBind();
    }

    protected void GridView_RESULTADOS_BUSQUEDA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal ID_SOLICITUD = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_SOLICITUD"]);
            Decimal ID_CONTRATO = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_CONTRATO"]);
            Decimal ID_EMPLEADO = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_EMPLEADO"]);
            Decimal ID_EMPRESA = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_EMPRESA"]);
            Decimal ID_OCUPACION = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_OCUPACION"]);
            String ID_CIUDAD = GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_CIUDAD"].ToString();

            Cargar(ID_SOLICITUD, ID_CONTRATO, ID_EMPLEADO, ID_EMPRESA, ID_OCUPACION, ID_CIUDAD);
        }
    }

    private void Activar(Acciones accion)
    {
        switch (accion)
        { 
            case Acciones.AdjuntarProducto:
                DropDownList_TallaProducto.Enabled = true;
                break;
        }
    }

    private void Cargar_DropDownList_TallaProducto(DropDownList drop, DataTable tablaTallas)
    {
        drop.Items.Clear();

        drop.Items.Add(new ListItem("Seleccione...", ""));

        foreach (DataRow filaTalla in tablaTallas.Rows)
        {
            if ((Convert.ToInt32(filaTalla["ENTRADAS"]) - (Convert.ToInt32(filaTalla["SALIDAS"]))) > 0)
            {
                drop.Items.Add(new ListItem(filaTalla["TALLA"].ToString(), filaTalla["TALLA"].ToString()));
            }
        }

        drop.DataBind();
    }

    private void CargarFormularioAdjuntar(Decimal ID_INDEX, Decimal ID_PRODUCTO, Decimal ID_ASIGNACION_SC, Int32 CANTIDAD_TOTAL, Int32 CANTIDAD_ENTREGADA, String FECHA_PROYECTADA_ENTREGA, String TIPO_ENTREGA, String NOMBRE_PRODUCTO)
    {
        HiddenField_ID_INDEX.Value = ID_INDEX.ToString();
        HiddenField_ID_PRODUCTO_SELECCIONADO.Value = ID_PRODUCTO.ToString();
        HiddenField_ID_ASIGNACION_SC.Value = ID_ASIGNACION_SC.ToString();
        HiddenField_CANTIDAD_TOTAL.Value = CANTIDAD_TOTAL.ToString();
        HiddenField_CANTIDAD_ENTREGADA.Value = CANTIDAD_ENTREGADA.ToString();
        HiddenField_FECHA_PROYECTADA_ENTREGA.Value = FECHA_PROYECTADA_ENTREGA;
        HiddenField_TIPO_ENTREGA.Value = TIPO_ENTREGA;

        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        String ID_CIUDAD = HiddenField_ID_CIUDAD.Value;

        Entrega _entrega = new Entrega(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaTallas = _entrega.ObtenerEntradasSalidasProductoPorCiudadYEmpresa(ID_EMPRESA, ID_CIUDAD, ID_PRODUCTO); 

        Cargar_DropDownList_TallaProducto(DropDownList_TallaProducto, tablaTallas);

        Cargar_DropDownList_Vacio(DropDownList_Proveedor);
        Cargar_DropDownList_Vacio(DropDownList_Factura);

        Label_NombreProducto.Text = NOMBRE_PRODUCTO;

        TextBox_CantidadProducto.Text = "";

        Label_CantidadMax.Text = "0";
        Label_CantidadDisponible.Text = "0";

        RangeValidator_TextBox_CantidadProducto.MaximumValue = "0";
        RangeValidator_TextBox_CantidadProducto.MinimumValue = "0";
    }

    private void CargarFormularioAdjuntarEquipos(Decimal ID_INDEX, Decimal ID_PRODUCTO, Decimal ID_ASIGNACION_SC, Int32 CANTIDAD_TOTAL, Int32 CANTIDAD_ENTREGADA, String FECHA_PROYECTADA_ENTREGA, String TIPO_ENTREGA, String NOMBRE_PRODUCTO)
    {
        HiddenField_ID_INDEX_EQUIPOS.Value = ID_INDEX.ToString();
        HiddenField_ID_ASIGNACION_SC_EQUIPOS.Value = ID_ASIGNACION_SC.ToString();
        HiddenField_ID_PRODUCTO_SELECCIONADO_EQUIPOS.Value = ID_PRODUCTO.ToString();
        HiddenField_CANTIDAD_TOTAL_EQUIPOS.Value = CANTIDAD_TOTAL.ToString();
        HiddenField_CANTIDAD_ENTREGADA_EQUIPOS.Value = CANTIDAD_ENTREGADA.ToString();
        HiddenField_FECHA_PROYECTADA_ENTREGA_EQUIPOS.Value = FECHA_PROYECTADA_ENTREGA;
        HiddenField_TIPO_ENTREGA_EQUIPOS.Value = TIPO_ENTREGA;

        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        String ID_CIUDAD = HiddenField_ID_CIUDAD.Value;

        Entrega _entrega = new Entrega(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaTallas = _entrega.ObtenerEntradasSalidasProductoPorCiudadYEmpresa(ID_EMPRESA, ID_CIUDAD, ID_PRODUCTO);

        Cargar_DropDownList_TallaProducto(DropDownList_TallaEquipos, tablaTallas);

        Label_NombreProductoEquipos.Text = NOMBRE_PRODUCTO;

        Cargar_DropDownList_Vacio(DropDownList_ProveedorEquipos);
        Cargar_DropDownList_Vacio(DropDownList_FacturaEquipos);

        Label_CantidadMaxEquipos.Text = "0";
        Label_CantidadDisponibleEquipos.Text = "0";

        Panel_GrillaEquipos.Visible = false;
    }

    private void Cargar_DropDownList_Vacio(DropDownList drop)
    {
        drop.Items.Clear();

        drop.Items.Add(new ListItem("Seleccione...",""));

        drop.DataBind();
    }

    protected void GridView_Entregas_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal ID_INDEX = Convert.ToDecimal(GridView_Entregas.DataKeys[indexSeleccionado].Values["ID_INDEX"]);
            Decimal ID_PRODUCTO = Convert.ToDecimal(GridView_Entregas.DataKeys[indexSeleccionado].Values["ID_PRODUCTO"]);
            Decimal ID_ASIGNACION_SC = Convert.ToDecimal(GridView_Entregas.DataKeys[indexSeleccionado].Values["ID_ASIGNACION_SC"]);

            TextBox textoNombreProducto = GridView_Entregas.Rows[indexSeleccionado].FindControl("TextBox_NombreProducto") as TextBox;

            TextBox textoCantidadTotal = GridView_Entregas.Rows[indexSeleccionado].FindControl("TextBox_Cantidad") as TextBox;
            TextBox textoCantidadEntregada = GridView_Entregas.Rows[indexSeleccionado].FindControl("TextBox_CantidadEntregada") as TextBox;
            TextBox TextoFechaProyectada = GridView_Entregas.Rows[indexSeleccionado].FindControl("TextBox_FechaProyectada") as TextBox;
            TextBox TextoTipoEntrega = GridView_Entregas.Rows[indexSeleccionado].FindControl("TextBox_TipoEntrega") as TextBox;

            String NOMBRE_SERVICIO_COMPLEMENTARIO  = GridView_Entregas.DataKeys[indexSeleccionado].Values["NOMBRE_SERVICIO_COMPLEMENTARIO"].ToString();
     
            Ocultar(Acciones.AdjuntarProducto);
           
            if(NOMBRE_SERVICIO_COMPLEMENTARIO == "EQUIPOS")
            {
                Mostrar(Acciones.AdjuntarEquipo);
                CargarFormularioAdjuntarEquipos(ID_INDEX, ID_PRODUCTO, ID_ASIGNACION_SC, Convert.ToInt32(textoCantidadTotal.Text), Convert.ToInt32(textoCantidadEntregada.Text), TextoFechaProyectada.Text, TextoTipoEntrega.Text, textoNombreProducto.Text);

                if (DropDownList_TallaEquipos.Items.Count <= 1)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron existencias del producto.", Proceso.Advertencia);

                    Panel_ConfiguracionEquipos.Visible = false;
                }
            }
            else
            {
                Mostrar(Acciones.AdjuntarProducto);
                CargarFormularioAdjuntar(ID_INDEX, ID_PRODUCTO, ID_ASIGNACION_SC, Convert.ToInt32(textoCantidadTotal.Text), Convert.ToInt32(textoCantidadEntregada.Text), TextoFechaProyectada.Text, TextoTipoEntrega.Text, textoNombreProducto.Text);

                if (DropDownList_TallaProducto.Items.Count <= 1)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron existencias del producto.", Proceso.Advertencia);

                    Panel_ConfiguracionProducto.Visible = false;
                }
            }

            GridView_AdjuntosAEntrega.Columns[0].Visible = false;
            GridView_EquiposAdjuntosAEntrega.Columns[0].Visible = false;
        }
    }

    private DataTable ObtenerEstructuraDataTablePara_GridView_AdjuntosAEntrega()
    {
        DataTable tablaParaGrilla = new DataTable();

        tablaParaGrilla.Columns.Add("ID_DETALLE_ENTREGA");
        tablaParaGrilla.Columns.Add("ID_INDEX");
        tablaParaGrilla.Columns.Add("ID_ASIGNACION_SC");
        tablaParaGrilla.Columns.Add("ID_LOTE");
        tablaParaGrilla.Columns.Add("ID_DOCUMENTO");
        tablaParaGrilla.Columns.Add("NOMBRE_PRODUCTO");
        tablaParaGrilla.Columns.Add("TALLA");
        tablaParaGrilla.Columns.Add("CANTIDAD");
        tablaParaGrilla.Columns.Add("NOMBRE_PROVEEDOR");
        tablaParaGrilla.Columns.Add("FACTURA_LOTE");
        tablaParaGrilla.Columns.Add("ID_PRODUCTO");
        tablaParaGrilla.Columns.Add("CANTIDAD_TOTAL");
        tablaParaGrilla.Columns.Add("FECHA_PROYECTADA_ENTREGA");
        tablaParaGrilla.Columns.Add("TIPO_ENTREGA");

        return tablaParaGrilla;
    }


    private DataTable ObtenerEstructuraDataTablePara_GridView_EquiposAdjuntosAEntrega()
    {
        DataTable tablaParaGrilla = new DataTable();

        tablaParaGrilla.Columns.Add("ID_DETALLE_ENTREGA");
        tablaParaGrilla.Columns.Add("ID_INDEX");
        tablaParaGrilla.Columns.Add("ID_ASIGNACION_SC");
        tablaParaGrilla.Columns.Add("ID_LOTE");
        tablaParaGrilla.Columns.Add("ID_DOCUMENTO");
        tablaParaGrilla.Columns.Add("ID_PRODUCTO");
        tablaParaGrilla.Columns.Add("ID_EQUIPO");
        tablaParaGrilla.Columns.Add("MARCA");
        tablaParaGrilla.Columns.Add("MODELO");
        tablaParaGrilla.Columns.Add("SERIE");
        tablaParaGrilla.Columns.Add("IMEI");
        tablaParaGrilla.Columns.Add("NUMERO_CELULAR");
        tablaParaGrilla.Columns.Add("NOMBRE_PROVEEDOR");
        tablaParaGrilla.Columns.Add("FACTURA_LOTE");
        tablaParaGrilla.Columns.Add("FECHA_PROYECTADA_ENTREGA");
        tablaParaGrilla.Columns.Add("TIPO_ENTREGA");
        tablaParaGrilla.Columns.Add("CANTIDAD_TOTAL");

        return tablaParaGrilla;
    }

    private DataTable ObtenerDataTableDesde_GridView_AdjuntosAEntrega()
    {
        DataTable tablaParaGrilla = ObtenerEstructuraDataTablePara_GridView_AdjuntosAEntrega();

        for (int i = 0; i < GridView_AdjuntosAEntrega.Rows.Count; i++)
        {

            DataRow filaParaGrilla = tablaParaGrilla.NewRow();
            GridViewRow filaGrilla = GridView_AdjuntosAEntrega.Rows[i];
            
            filaParaGrilla["ID_DETALLE_ENTREGA"] = Convert.ToDecimal(GridView_AdjuntosAEntrega.DataKeys[i].Values["ID_DETALLE_ENTREGA"]);
            filaParaGrilla["ID_INDEX"] = Convert.ToDecimal(GridView_AdjuntosAEntrega.DataKeys[i].Values["ID_INDEX"]);
            filaParaGrilla["ID_ASIGNACION_SC"] = Convert.ToDecimal(GridView_AdjuntosAEntrega.DataKeys[i].Values["ID_ASIGNACION_SC"]);
            filaParaGrilla["ID_LOTE"] = Convert.ToDecimal(GridView_AdjuntosAEntrega.DataKeys[i].Values["ID_LOTE"]);
            filaParaGrilla["ID_DOCUMENTO"] = Convert.ToDecimal(GridView_AdjuntosAEntrega.DataKeys[i].Values["ID_DOCUMENTO"]);
            filaParaGrilla["ID_PRODUCTO"] = Convert.ToDecimal(GridView_AdjuntosAEntrega.DataKeys[i].Values["ID_PRODUCTO"]);
            filaParaGrilla["CANTIDAD_TOTAL"] = Convert.ToInt32(GridView_AdjuntosAEntrega.DataKeys[i].Values["CANTIDAD_TOTAL"]);
            filaParaGrilla["FECHA_PROYECTADA_ENTREGA"] = Convert.ToDateTime(GridView_AdjuntosAEntrega.DataKeys[i].Values["FECHA_PROYECTADA_ENTREGA"]);
            filaParaGrilla["TIPO_ENTREGA"] = GridView_AdjuntosAEntrega.DataKeys[i].Values["TIPO_ENTREGA"].ToString();

            TextBox textoNombreProducto = filaGrilla.FindControl("TextBox_NombreProducto") as TextBox;
            filaParaGrilla["NOMBRE_PRODUCTO"] = textoNombreProducto.Text;

            TextBox textoTalla = filaGrilla.FindControl("TextBox_Talla") as TextBox;
            filaParaGrilla["TALLA"] = textoTalla.Text;

            TextBox textoCantidad = filaGrilla.FindControl("TextBox_Cantidad") as TextBox;
            filaParaGrilla["CANTIDAD"] = Convert.ToInt32(textoCantidad.Text);

            TextBox textoNombreProveedor = filaGrilla.FindControl("TextBox_NombreProveedor") as TextBox;
            filaParaGrilla["NOMBRE_PROVEEDOR"] = textoNombreProveedor.Text;

            TextBox textoFacturaLote = filaGrilla.FindControl("TextBox_FacturaLote") as TextBox;
            filaParaGrilla["FACTURA_LOTE"] = textoFacturaLote.Text;

            tablaParaGrilla.Rows.Add(filaParaGrilla);
        }

        return tablaParaGrilla;
    }


    private DataTable ObtenerDataTableDesde_GridView_EquiposAdjuntosAEntrega()
    {
        DataTable tablaParaGrilla = ObtenerEstructuraDataTablePara_GridView_EquiposAdjuntosAEntrega();

        for (int i = 0; i < GridView_EquiposAdjuntosAEntrega.Rows.Count; i++)
        {

            DataRow filaParaGrilla = tablaParaGrilla.NewRow();
            GridViewRow filaGrilla = GridView_EquiposAdjuntosAEntrega.Rows[i];

            filaParaGrilla["ID_DETALLE_ENTREGA"] = Convert.ToDecimal(GridView_EquiposAdjuntosAEntrega.DataKeys[i].Values["ID_DETALLE_ENTREGA"]);
            filaParaGrilla["ID_INDEX"] = Convert.ToDecimal(GridView_EquiposAdjuntosAEntrega.DataKeys[i].Values["ID_INDEX"]);
            filaParaGrilla["ID_ASIGNACION_SC"] = Convert.ToDecimal(GridView_EquiposAdjuntosAEntrega.DataKeys[i].Values["ID_ASIGNACION_SC"]);
            filaParaGrilla["ID_LOTE"] = Convert.ToDecimal(GridView_EquiposAdjuntosAEntrega.DataKeys[i].Values["ID_LOTE"]);
            filaParaGrilla["ID_DOCUMENTO"] = Convert.ToDecimal(GridView_EquiposAdjuntosAEntrega.DataKeys[i].Values["ID_DOCUMENTO"]);
            filaParaGrilla["ID_PRODUCTO"] = Convert.ToDecimal(GridView_EquiposAdjuntosAEntrega.DataKeys[i].Values["ID_PRODUCTO"]);
            filaParaGrilla["ID_EQUIPO"] = Convert.ToDecimal(GridView_EquiposAdjuntosAEntrega.DataKeys[i].Values["ID_EQUIPO"]);
            filaParaGrilla["FECHA_PROYECTADA_ENTREGA"] = Convert.ToDateTime(GridView_EquiposAdjuntosAEntrega.DataKeys[i].Values["FECHA_PROYECTADA_ENTREGA"]);
            filaParaGrilla["TIPO_ENTREGA"] = GridView_EquiposAdjuntosAEntrega.DataKeys[i].Values["TIPO_ENTREGA"].ToString();
            filaParaGrilla["CANTIDAD_TOTAL"] = GridView_EquiposAdjuntosAEntrega.DataKeys[i].Values["CANTIDAD_TOTAL"].ToString();


            TextBox textoMarca = filaGrilla.FindControl("TextBox_MARCA") as TextBox;
            filaParaGrilla["MARCA"] = textoMarca.Text;

            TextBox textoModelo = filaGrilla.FindControl("TextBox_MODELO") as TextBox;
            filaParaGrilla["MODELO"] = textoModelo.Text;

            TextBox textoSerie = filaGrilla.FindControl("TextBox_SERIE") as TextBox;
            filaParaGrilla["SERIE"] = textoSerie.Text;

            TextBox textoImei = filaGrilla.FindControl("TextBox_IMEI") as TextBox;
            filaParaGrilla["IMEI"] = textoImei.Text;

            TextBox textoNumCelular = filaGrilla.FindControl("TextBox_NUM_CELULAR") as TextBox;
            filaParaGrilla["NUMERO_CELULAR"] = textoNumCelular.Text;

            TextBox textoNombreProveedor = filaGrilla.FindControl("TextBox_NombreProveedor") as TextBox;
            filaParaGrilla["NOMBRE_PROVEEDOR"] = textoNombreProveedor.Text;

            TextBox textoFacturaLote = filaGrilla.FindControl("TextBox_FacturaLote") as TextBox;
            filaParaGrilla["FACTURA_LOTE"] = textoFacturaLote.Text;

            tablaParaGrilla.Rows.Add(filaParaGrilla);
        }

        return tablaParaGrilla;
    }

    private void Cargar_GridView_AdjuntosAEntrega_DesdeTabla(DataTable tablaParaGrilla)
    {
        GridView_AdjuntosAEntrega.DataSource = tablaParaGrilla;
        GridView_AdjuntosAEntrega.DataBind();


        for (int i = 0; i < GridView_AdjuntosAEntrega.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_AdjuntosAEntrega.Rows[i];
            DataRow filaTabla = tablaParaGrilla.Rows[i];

            TextBox textoNombreProducto = filaGrilla.FindControl("TextBox_NombreProducto") as TextBox;
            textoNombreProducto.Text = filaTabla["NOMBRE_PRODUCTO"].ToString();

            TextBox textoTalla = filaGrilla.FindControl("TextBox_Talla") as TextBox;
            textoTalla.Text = filaTabla["TALLA"].ToString();

            TextBox textoCantidad = filaGrilla.FindControl("TextBox_Cantidad") as TextBox;
            textoCantidad.Text = filaTabla["CANTIDAD"].ToString();

            TextBox textoNombreProveedor = filaGrilla.FindControl("TextBox_NombreProveedor") as TextBox;
            textoNombreProveedor.Text = filaTabla["NOMBRE_PROVEEDOR"].ToString();

            TextBox textoFacturaLote = filaGrilla.FindControl("TextBox_FacturaLote") as TextBox;
            textoFacturaLote.Text = filaTabla["FACTURA_LOTE"].ToString();
        }
    }


    private void Cargar_GridView_EquiposAdjuntosAEntrega_DesdeTabla(DataTable tablaParaGrilla)
    {
        GridView_EquiposAdjuntosAEntrega.DataSource = tablaParaGrilla;
        GridView_EquiposAdjuntosAEntrega.DataBind();

        for (int i = 0; i < GridView_EquiposAdjuntosAEntrega.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_EquiposAdjuntosAEntrega.Rows[i];
            DataRow filaTabla = tablaParaGrilla.Rows[i];

            TextBox textoMarca = filaGrilla.FindControl("TextBox_MARCA") as TextBox;
            textoMarca.Text = filaTabla["MARCA"].ToString();

            TextBox textoModelo = filaGrilla.FindControl("TextBox_MODELO") as TextBox;
            textoModelo.Text = filaTabla["MODELO"].ToString(); ;

            TextBox textoSerie = filaGrilla.FindControl("TextBox_SERIE") as TextBox;
            textoSerie.Text = filaTabla["SERIE"].ToString();

            TextBox textoImei = filaGrilla.FindControl("TextBox_IMEI") as TextBox;
            textoImei.Text = filaTabla["IMEI"].ToString();

            TextBox textoNumCelular = filaGrilla.FindControl("TextBox_NUM_CELULAR") as TextBox;
            textoNumCelular.Text = filaTabla["NUMERO_CELULAR"].ToString();

            TextBox textoNombreProveedor = filaGrilla.FindControl("TextBox_NombreProveedor") as TextBox;
            textoNombreProveedor.Text = filaTabla["NOMBRE_PROVEEDOR"].ToString();

            TextBox textoFacturaLote = filaGrilla.FindControl("TextBox_FacturaLote") as TextBox;
            textoFacturaLote.Text = filaTabla["FACTURA_LOTE"].ToString();
        }
    }


    private Boolean AdjuntarEntregaA_GridView_AdjuntosAEntrega(Decimal ID_INDEX,
            Decimal ID_ASIGNACION_SC,
            Decimal ID_LOTE,
            Decimal ID_DOCUMENTO,
            String NOMBRE_PRODUCTO,
            String TALLA,
            Int32 CANTIDAD,
            String NOMBRE_PROVEEDOR,
            String FACTURA_LOTE,
            Decimal ID_PRODUCTO, 
            Int32 CANTIDAD_TOTAL,
            DateTime FECHA_PROYECTADA_ENTREGA,
            String TIPO_ENTREGA)
    {
        Boolean adjuntado = false;

        DataTable tablaGrilla = ObtenerDataTableDesde_GridView_AdjuntosAEntrega();


        foreach (DataRow filaTabla in tablaGrilla.Rows)
        { 
            Decimal ID_INDEX_TABLA = Convert.ToDecimal(filaTabla["ID_INDEX"]);
            Decimal ID_ASIGNACION_SC_TABLA = Convert.ToDecimal(filaTabla["ID_ASIGNACION_SC"]);
            Decimal ID_LOTE_TABLA = Convert.ToDecimal(filaTabla["ID_LOTE"]);
            Decimal ID_DOCUMENTO_TABLA = Convert.ToDecimal(filaTabla["ID_DOCUMENTO"]);
            Int32 CANTIDAD_TABLA = Convert.ToInt32(filaTabla["CANTIDAD"]);

            if ((ID_INDEX == ID_INDEX_TABLA) && (ID_ASIGNACION_SC == ID_ASIGNACION_SC_TABLA) && (ID_LOTE == ID_LOTE_TABLA) && (ID_DOCUMENTO == ID_DOCUMENTO_TABLA))
            {
                filaTabla["CANTIDAD"] = CANTIDAD_TABLA + CANTIDAD;
                adjuntado = true;
                break;
            }
        }

        if (adjuntado == false)
        {
            DataRow filaParaGrilla = tablaGrilla.NewRow();

            filaParaGrilla["ID_DETALLE_ENTREGA"] = 0;
            filaParaGrilla["ID_INDEX"] = ID_INDEX;
            filaParaGrilla["ID_ASIGNACION_SC"] = ID_ASIGNACION_SC;
            filaParaGrilla["ID_LOTE"] = ID_LOTE;
            filaParaGrilla["ID_DOCUMENTO"] = ID_DOCUMENTO;
            filaParaGrilla["NOMBRE_PRODUCTO"] = NOMBRE_PRODUCTO;
            filaParaGrilla["TALLA"] = TALLA;
            filaParaGrilla["CANTIDAD"] = CANTIDAD;
            filaParaGrilla["NOMBRE_PROVEEDOR"] = NOMBRE_PROVEEDOR;
            filaParaGrilla["FACTURA_LOTE"] = FACTURA_LOTE;
            filaParaGrilla["ID_PRODUCTO"] = ID_PRODUCTO;
            filaParaGrilla["CANTIDAD_TOTAL"] = CANTIDAD_TOTAL;
            filaParaGrilla["FECHA_PROYECTADA_ENTREGA"] = FECHA_PROYECTADA_ENTREGA;
            filaParaGrilla["TIPO_ENTREGA"] = TIPO_ENTREGA;

            tablaGrilla.Rows.Add(filaParaGrilla);
            adjuntado = true;
        }

        if (adjuntado == true)
        {
            Cargar_GridView_AdjuntosAEntrega_DesdeTabla(tablaGrilla);
        }
        return adjuntado;
    }


    private Boolean AdjuntarEntregaEquiposA_GridView_EquiposAdjuntosAEntrega(Decimal ID_INDEX,
            Decimal ID_ASIGNACION_SC,
            Decimal ID_LOTE,
            Decimal ID_DOCUMENTO,
            String NOMBRE_PRODUCTO,
            String TALLA,
            String NOMBRE_PROVEEDOR,
            String FACTURA_LOTE,
            Decimal ID_PRODUCTO,
            Int32 CANTIDAD_TOTAL,
            DateTime FECHA_PROYECTADA_ENTREGA,
            String TIPO_ENTREGA)
    {
        Boolean adjuntado = false;

        DataTable tablaGrilla = ObtenerDataTableDesde_GridView_EquiposAdjuntosAEntrega(); 

        for (int i = 0; i < GridView_SeleccionarEquipos.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_SeleccionarEquipos.Rows[i];

            CheckBox checkSeleccionar = filaGrilla.FindControl("CheckBox_SeleccionEquipo") as CheckBox;

            if (tablaGrilla.Select("ID_EQUIPO = '" + GridView_SeleccionarEquipos.DataKeys[i].Values["ID_EQUIPO"].ToString() + "'").Length <= 0)
            {
                if (checkSeleccionar.Checked == true)
                {
                    DataRow filaParaGrilla = tablaGrilla.NewRow();

                    filaParaGrilla["ID_DETALLE_ENTREGA"] = 0;

                    filaParaGrilla["ID_INDEX"] = ID_INDEX;

                    filaParaGrilla["ID_ASIGNACION_SC"] = ID_ASIGNACION_SC;

                    filaParaGrilla["ID_LOTE"] = ID_LOTE;

                    filaParaGrilla["ID_DOCUMENTO"] = ID_DOCUMENTO;

                    filaParaGrilla["ID_PRODUCTO"] = ID_PRODUCTO;

                    filaParaGrilla["ID_EQUIPO"] = Convert.ToDecimal(GridView_SeleccionarEquipos.DataKeys[i].Values["ID_EQUIPO"]);

                    TextBox textoMarca = filaGrilla.FindControl("TextBox_MARCA") as TextBox;
                    filaParaGrilla["MARCA"] = textoMarca.Text;

                    TextBox textoModelo = filaGrilla.FindControl("TextBox_MODELO") as TextBox;
                    filaParaGrilla["MODELO"] = textoModelo.Text;

                    TextBox textoSerie = filaGrilla.FindControl("TextBox_SERIE") as TextBox;
                    filaParaGrilla["SERIE"] = textoSerie.Text;

                    TextBox textoImei = filaGrilla.FindControl("TextBox_IMEI") as TextBox;
                    filaParaGrilla["IMEI"] = textoImei.Text;

                    TextBox textoNumCelular = filaGrilla.FindControl("TextBox_NUM_CELULAR") as TextBox;
                    filaParaGrilla["NUMERO_CELULAR"] = textoNumCelular.Text;

                    filaParaGrilla["NOMBRE_PROVEEDOR"] = NOMBRE_PROVEEDOR;

                    filaParaGrilla["FACTURA_LOTE"] = FACTURA_LOTE;

                    filaParaGrilla["FECHA_PROYECTADA_ENTREGA"] = FECHA_PROYECTADA_ENTREGA;

                    filaParaGrilla["TIPO_ENTREGA"] = TIPO_ENTREGA;

                    filaParaGrilla["CANTIDAD_TOTAL"] = CANTIDAD_TOTAL;

                    tablaGrilla.Rows.Add(filaParaGrilla);

                    adjuntado = true;
                }
            }
            else
            {
                adjuntado = true;
            }
        }

        if (adjuntado == true)
        {
            Cargar_GridView_EquiposAdjuntosAEntrega_DesdeTabla(tablaGrilla);
        }
        return adjuntado;
    }

    private void Sincronizar_GridView_Entregas_y_GridView_AdjuntosAEntrega()
    { 
        DataTable tablaGrillaAdjuntos = ObtenerDataTableDesde_GridView_AdjuntosAEntrega();
        DataTable tablaGrillaEquiposAdjuntos = ObtenerDataTableDesde_GridView_EquiposAdjuntosAEntrega(); 
        for (int i = 0; i < GridView_Entregas.Rows.Count; i++)
        {
            GridViewRow filaGrillaEntregas = GridView_Entregas.Rows[i];

            Decimal ID_INDEX_GRILLA_ENTREGAS = Convert.ToDecimal(GridView_Entregas.DataKeys[i].Values["ID_INDEX"]);
            Decimal ID_ASIGNACION_SC_GRILLA_ENTREGAS = Convert.ToDecimal(GridView_Entregas.DataKeys[i].Values["ID_ASIGNACION_SC"]);
            Decimal ID_PRODUCTO_GRILLA_ENTREGAS = Convert.ToDecimal(GridView_Entregas.DataKeys[i].Values["ID_PRODUCTO"]);

            String NOMBRE_SERVICIO_COMPLEMENTARIO = GridView_Entregas.DataKeys[i].Values["NOMBRE_SERVICIO_COMPLEMENTARIO"].ToString();

            Int32 CANTIDAD_ENTREGADA_INICIAL_GRILLA_ENTREGAS = Convert.ToInt32(GridView_Entregas.DataKeys[i].Values["CANTIDAD_ENTREGADA_INICIAL"]);
            
            TextBox textoCantidadTotal = filaGrillaEntregas.FindControl("TextBox_Cantidad") as TextBox;
            Int32 CANTIDAD_TOTAL_GRILLA_ENTREGAS = Convert.ToInt32(textoCantidadTotal.Text);

            TextBox textoCantidadEntregada = filaGrillaEntregas.FindControl("TextBox_CantidadEntregada") as TextBox;
            Int32 CANTIDAD_ENTREGADA_GRILLA_ENTREGAS = Convert.ToInt32(textoCantidadEntregada.Text);

            if (NOMBRE_SERVICIO_COMPLEMENTARIO.ToUpper() == "EQUIPOS")
            {
                DataRow[] filasEncontradasGrillaEquiposAdjuntos = tablaGrillaEquiposAdjuntos.Select("ID_INDEX = '" + ID_INDEX_GRILLA_ENTREGAS.ToString() + "'");

                Int32 CANTIDAD_ADJUNTADA = 0;
                foreach (DataRow filaGrillaEquiposAdjunto in filasEncontradasGrillaEquiposAdjuntos)
                {
                    CANTIDAD_ADJUNTADA += 1;
                }

                CANTIDAD_ENTREGADA_GRILLA_ENTREGAS = CANTIDAD_ENTREGADA_INICIAL_GRILLA_ENTREGAS + CANTIDAD_ADJUNTADA;
                textoCantidadEntregada.Text = CANTIDAD_ENTREGADA_GRILLA_ENTREGAS.ToString();

                if (CANTIDAD_TOTAL_GRILLA_ENTREGAS <= CANTIDAD_ENTREGADA_GRILLA_ENTREGAS)
                {
                    filaGrillaEntregas.Cells[0].Enabled = false;
                }
                else
                {
                    filaGrillaEntregas.Cells[0].Enabled = true;
                }     
            }
            else
            {
                DataRow[] filasEncontradasGrillaAdjuntos = tablaGrillaAdjuntos.Select("ID_INDEX = '" + ID_INDEX_GRILLA_ENTREGAS.ToString() + "'");
                Int32 CANTIDAD_ADJUNTADA = 0;

                foreach (DataRow filaGrillaAdjuntos in filasEncontradasGrillaAdjuntos)
                {
                    CANTIDAD_ADJUNTADA += Convert.ToInt32(filaGrillaAdjuntos["CANTIDAD"]);
                }

                CANTIDAD_ENTREGADA_GRILLA_ENTREGAS = CANTIDAD_ENTREGADA_INICIAL_GRILLA_ENTREGAS + CANTIDAD_ADJUNTADA;
                textoCantidadEntregada.Text = CANTIDAD_ENTREGADA_GRILLA_ENTREGAS.ToString();

                if (CANTIDAD_TOTAL_GRILLA_ENTREGAS <= CANTIDAD_ENTREGADA_GRILLA_ENTREGAS)
                {
                    filaGrillaEntregas.Cells[0].Enabled = false;
                }
                else
                {
                    filaGrillaEntregas.Cells[0].Enabled = true;
                }     
            } 
        }
    }

    protected void Button_AdjuntarAEntrega_Click(object sender, EventArgs e)
    {
        Decimal ID_INDEX = Convert.ToDecimal(HiddenField_ID_INDEX.Value);
        Decimal ID_ASIGNACION_SC = Convert.ToDecimal(HiddenField_ID_ASIGNACION_SC.Value);
        Decimal ID_PRODUCTO = Convert.ToDecimal(HiddenField_ID_PRODUCTO_SELECCIONADO.Value);
        Int32 CANTIDAD_ADJUNTAR = Convert.ToInt32(TextBox_CantidadProducto.Text);
        
        Int32 CANTIDAD_TOTAL = Convert.ToInt32(HiddenField_CANTIDAD_TOTAL.Value);
        DateTime FECHA_PROYECTADA_ENTREGA = Convert.ToDateTime(HiddenField_FECHA_PROYECTADA_ENTREGA.Value);
        String TIPO_ENTREGA = HiddenField_TIPO_ENTREGA.Value;

        String[] datosArray = DropDownList_Factura.SelectedValue.Split(':');
        Decimal ID_DOCUMENTO = Convert.ToDecimal(datosArray[0]);
        Decimal ID_LOTE = Convert.ToDecimal(datosArray[1]);

        String TALLA = DropDownList_TallaProducto.SelectedValue;
        String FACTURA_LOTE = DropDownList_Factura.SelectedItem.Text;
        String NOMBRE_PRODUCTO = Label_NombreProducto.Text;
        String NOMBRE_PROVEEDOR = DropDownList_Proveedor.SelectedItem.Text;

        Boolean resultado = AdjuntarEntregaA_GridView_AdjuntosAEntrega(ID_INDEX, ID_ASIGNACION_SC, ID_LOTE, ID_DOCUMENTO, NOMBRE_PRODUCTO, TALLA, CANTIDAD_ADJUNTAR, NOMBRE_PROVEEDOR, FACTURA_LOTE, ID_PRODUCTO, CANTIDAD_TOTAL, FECHA_PROYECTADA_ENTREGA, TIPO_ENTREGA);

        if (resultado == true)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Se Adjunto el producto correctamente a la entrega.", Proceso.Correcto);

            Sincronizar_GridView_Entregas_y_GridView_AdjuntosAEntrega();

            Panel_AdjuntosAEntrega.Visible = true;
            inhabilitarFilasGrilla(GridView_AdjuntosAEntrega, 1);

            Panel_ConfiguracionProducto.Visible = false;
        }

        if ((GridView_AdjuntosAEntrega.Rows.Count > 0) || (GridView_EquiposAdjuntosAEntrega.Rows.Count > 0))
        {
            Button_GUARDAR.Visible = true;
            Button_GUARDAR_1.Visible = true;
        }
        else
        {
            Button_GUARDAR.Visible = false;
            Button_GUARDAR_1.Visible = false;
        }

        GridView_AdjuntosAEntrega.Columns[0].Visible = true;
        GridView_EquiposAdjuntosAEntrega.Columns[0].Visible = true;
    }

    protected void GridView_AdjuntosAEntrega_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "eliminar")
        {
            DataTable tablaDesdeGrilla = ObtenerDataTableDesde_GridView_AdjuntosAEntrega();

            tablaDesdeGrilla.Rows.RemoveAt(indexSeleccionado);

            Cargar_GridView_AdjuntosAEntrega_DesdeTabla(tablaDesdeGrilla);
            inhabilitarFilasGrilla(GridView_AdjuntosAEntrega, 1);

            Sincronizar_GridView_Entregas_y_GridView_AdjuntosAEntrega();

        }
    }
    protected void DropDownList_TallaEquipos_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_TallaEquipos.SelectedIndex <= 0)
        {
            Cargar_DropDownList_Vacio(DropDownList_ProveedorEquipos);
            Cargar_DropDownList_Vacio(DropDownList_FacturaEquipos);

            Label_CantidadMaxEquipos.Text = "0";

            Label_CantidadDisponibleEquipos.Text = "0";

            GridView_SeleccionarEquipos.DataSource = null;
            GridView_SeleccionarEquipos.DataBind();
        }
        else
        {
            Entrega _entrega = new Entrega(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
            String ID_CIUDAD = HiddenField_ID_CIUDAD.Value;
            Decimal ID_PRODUCTO = Convert.ToDecimal(HiddenField_ID_PRODUCTO_SELECCIONADO_EQUIPOS.Value);
            
            DataTable tablaProveedores = _entrega.ObtenerProveedoresPorProductoCiudadEmpresaYTalla(ID_EMPRESA, ID_CIUDAD, ID_PRODUCTO, DropDownList_TallaEquipos.SelectedValue);

            Cargar_DropDownList_Proveedor(DropDownList_ProveedorEquipos, tablaProveedores);

            Cargar_DropDownList_Vacio(DropDownList_FacturaEquipos);

            Label_CantidadMaxEquipos.Text = "0";

            Label_CantidadDisponibleEquipos.Text = "0";

            GridView_SeleccionarEquipos.DataSource = null;
            GridView_SeleccionarEquipos.DataBind();
        }
    }
    protected void DropDownList_ProveedorEquipos_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_ProveedorEquipos.SelectedIndex <= 0)
        {
            Cargar_DropDownList_Vacio(DropDownList_FacturaEquipos);

            Label_CantidadMaxEquipos.Text = "0";

            Label_CantidadDisponibleEquipos.Text = "0";

            GridView_SeleccionarEquipos.DataSource = null;
            GridView_SeleccionarEquipos.DataBind();
        }
        else
        {
            Entrega _entrega = new Entrega(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
            String ID_CIUDAD = HiddenField_ID_CIUDAD.Value;
            Decimal ID_PRODUCTO = Convert.ToDecimal(HiddenField_ID_PRODUCTO_SELECCIONADO_EQUIPOS.Value);
            Decimal ID_PROVEEDOR = Convert.ToDecimal(DropDownList_ProveedorEquipos.SelectedValue);

            DataTable tablaFacturas = _entrega.ObtenerFacturasPorProveedorProductoCiudadEmpresaYTalla(ID_EMPRESA, ID_CIUDAD, ID_PRODUCTO, DropDownList_TallaEquipos.SelectedValue, ID_PROVEEDOR);

            Cargar_DropDownList_Factura(DropDownList_FacturaEquipos, tablaFacturas);

            Label_CantidadMaxEquipos.Text = "0";

            Label_CantidadDisponibleEquipos.Text = "0";

            GridView_SeleccionarEquipos.DataSource = null;
            GridView_SeleccionarEquipos.DataBind();
        }
    }

    private void Cargar_GridView_SeleccionarEquipos_DesdeDataTable(DataTable tablaEquipos)
    {
        GridView_SeleccionarEquipos.DataSource = tablaEquipos;
        GridView_SeleccionarEquipos.DataBind();

        DataTable tablaEquiposAdjuntos = ObtenerDataTableDesde_GridView_EquiposAdjuntosAEntrega(); 

        for (int i = 0; i < GridView_SeleccionarEquipos.Rows.Count; i++)
        {
            DataRow filaTabla = tablaEquipos.Rows[i];


            CheckBox checkSeleccionar = GridView_SeleccionarEquipos.Rows[i].FindControl("CheckBox_SeleccionEquipo") as CheckBox;

            TextBox textoMarca = GridView_SeleccionarEquipos.Rows[i].FindControl("TextBox_MARCA") as TextBox;
            textoMarca.Text = filaTabla["MARCA"].ToString();

            TextBox textoModelo = GridView_SeleccionarEquipos.Rows[i].FindControl("TextBox_MODELO") as TextBox;
            textoModelo.Text = filaTabla["MODELO"].ToString();

            TextBox textoSerie = GridView_SeleccionarEquipos.Rows[i].FindControl("TextBox_SERIE") as TextBox;
            textoSerie.Text = filaTabla["SERIE"].ToString();

            TextBox textoImei = GridView_SeleccionarEquipos.Rows[i].FindControl("TextBox_IMEI") as TextBox;
            textoImei.Text = filaTabla["IMEI"].ToString();

            TextBox textoNumCelular = GridView_SeleccionarEquipos.Rows[i].FindControl("TextBox_NUM_CELULAR") as TextBox;
            textoNumCelular.Text = filaTabla["NUMERO_CELULAR"].ToString();

            if (tablaEquiposAdjuntos.Select("ID_EQUIPO = '" + filaTabla["ID_EQUIPO"].ToString() + "'").Length > 0)
            {
                checkSeleccionar.Checked = true;
                GridView_SeleccionarEquipos.Rows[i].Enabled = false;
            }
            else
            {
                checkSeleccionar.Checked = false;
                GridView_SeleccionarEquipos.Rows[i].Enabled = true;
            }
        }
    }

    protected void DropDownList_FacturaEquipos_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_FacturaEquipos.SelectedIndex <= 0)
        {
            Label_CantidadMaxEquipos.Text = "0";

            Label_CantidadDisponibleEquipos.Text = "0";

            Panel_GrillaEquipos.Visible = false;

            GridView_SeleccionarEquipos.DataSource = null;
            GridView_SeleccionarEquipos.DataBind();
        }
        else
        {
            Entrega _entrega = new Entrega(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            String[] datosArray = DropDownList_FacturaEquipos.SelectedValue.Split(':');

            Decimal ID_DOCUMENTO = Convert.ToDecimal(datosArray[0]);
            Decimal ID_LOTE = Convert.ToDecimal(datosArray[1]);

            DataTable tablaInfoLote = _entrega.ObtenerCantidadesEnLote(ID_DOCUMENTO, ID_LOTE);

            DataRow filaInfoLote = tablaInfoLote.Rows[0];

            Int32 CANTIDAD_TOTAL = Convert.ToInt32(HiddenField_CANTIDAD_TOTAL_EQUIPOS.Value);
            Int32 CANTIDAD_ENTREGADA = Convert.ToInt32(HiddenField_CANTIDAD_ENTREGADA_EQUIPOS.Value);

            Int32 CANTIDAD_DISPONIBLE_LOTE = ObtieneCantidadRealDisponibleEnLote(Convert.ToInt32(filaInfoLote["CANTIDAD_DISPONIBLE"]), ID_LOTE);

            Label_CantidadMaxEquipos.Text = (CANTIDAD_TOTAL - CANTIDAD_ENTREGADA).ToString();

            Label_CantidadDisponibleEquipos.Text = CANTIDAD_DISPONIBLE_LOTE.ToString();

            DataTable tablaEuiposDisponibles = _entrega.ObtenerEquiposDisponiblesEnLote(ID_DOCUMENTO, ID_LOTE);

            Panel_GrillaEquipos.Visible = true;
            Cargar_GridView_SeleccionarEquipos_DesdeDataTable(tablaEuiposDisponibles);
            inhabilitarFilasGrilla(GridView_SeleccionarEquipos, 0, GridView_SeleccionarEquipos.Columns.Count - 1);
        }
    }

    protected void CheckBox_SeleccionEquipo_CheckedChanged(object sender, EventArgs e)
    {
        Boolean permiteMasSeleccionaes = false;

        Int32 cantidadMax = Convert.ToInt32(Label_CantidadMaxEquipos.Text);

        Int32 cantidadSeleccionada = 0;

        DataTable tablaEquiposAdjuntos = ObtenerDataTableDesde_GridView_EquiposAdjuntosAEntrega(); 

        for(int i = 0; i < GridView_SeleccionarEquipos.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_SeleccionarEquipos.Rows[i];

            CheckBox checkSeleccionar = filaGrilla.FindControl("CheckBox_SeleccionEquipo") as CheckBox;
            Decimal ID_EQUIPO = Convert.ToDecimal(GridView_SeleccionarEquipos.DataKeys[i].Values["ID_EQUIPO"]);

            if (checkSeleccionar.Checked == true)
            {
                if (tablaEquiposAdjuntos.Select("ID_EQUIPO = '" + ID_EQUIPO.ToString() + "'").Length <= 0)
                {
                    cantidadSeleccionada += 1;
                }
            }
        }


        if (cantidadMax > cantidadSeleccionada)
        {
            permiteMasSeleccionaes = true;
        }

        foreach (GridViewRow filaGrilla in GridView_SeleccionarEquipos.Rows)
        {
            CheckBox checkSeleccionar = filaGrilla.FindControl("CheckBox_SeleccionEquipo") as CheckBox;

            if (checkSeleccionar.Checked == false)
            {
                if (permiteMasSeleccionaes == true)
                {
                    checkSeleccionar.Enabled = true;
                }
                else
                {
                    checkSeleccionar.Enabled = false;
                }
            }
            else
            {
                checkSeleccionar.Enabled = true;
            }
        }
    }

    protected void Button_AdjuntarEquipos_Click(object sender, EventArgs e)
    {
        Boolean continuar = false;
        foreach (GridViewRow filaGrilla in GridView_SeleccionarEquipos.Rows)
        {
            CheckBox checkSeleccionar = filaGrilla.FindControl("CheckBox_SeleccionEquipo") as CheckBox;

            if (checkSeleccionar.Checked == true)
            {
                continuar = true;
                break;
            }
        }

        if (continuar == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se han seleccionado equipos para adjuntar.", Proceso.Advertencia);
        }
        else
        {
            Decimal ID_INDEX = Convert.ToDecimal(HiddenField_ID_INDEX_EQUIPOS.Value);
            Decimal ID_ASIGNACION_SC = Convert.ToDecimal(HiddenField_ID_ASIGNACION_SC_EQUIPOS.Value);
            Decimal ID_PRODUCTO = Convert.ToDecimal(HiddenField_ID_PRODUCTO_SELECCIONADO_EQUIPOS.Value);
            Int32 CANTIDAD_TOTAL = Convert.ToInt32(HiddenField_CANTIDAD_TOTAL_EQUIPOS.Value);

            DateTime FECHA_PROYECTADA_ENTREGA = Convert.ToDateTime(HiddenField_FECHA_PROYECTADA_ENTREGA_EQUIPOS.Value);
            String TIPO_ENTREGA = HiddenField_TIPO_ENTREGA_EQUIPOS.Value;

            String[] datosArray = DropDownList_FacturaEquipos.SelectedValue.Split(':');
            Decimal ID_DOCUMENTO = Convert.ToDecimal(datosArray[0]);
            Decimal ID_LOTE = Convert.ToDecimal(datosArray[1]);

            String TALLA = DropDownList_TallaEquipos.SelectedValue;
            String FACTURA_LOTE = DropDownList_FacturaEquipos.SelectedItem.Text;
            String NOMBRE_PRODUCTO = Label_NombreProductoEquipos.Text;
            String NOMBRE_PROVEEDOR = DropDownList_ProveedorEquipos.SelectedItem.Text;

            Boolean resultado = AdjuntarEntregaEquiposA_GridView_EquiposAdjuntosAEntrega(ID_INDEX, ID_ASIGNACION_SC, ID_LOTE, ID_DOCUMENTO, NOMBRE_PRODUCTO, TALLA, NOMBRE_PROVEEDOR, FACTURA_LOTE, ID_PRODUCTO, CANTIDAD_TOTAL, FECHA_PROYECTADA_ENTREGA, TIPO_ENTREGA);

            if (resultado == true)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Se Adjunto el equipo(s) correctamente a la entrega.", Proceso.Correcto);

                Sincronizar_GridView_Entregas_y_GridView_AdjuntosAEntrega();

                Panel_EquiposAdjuntosAEntrega.Visible = true;
                inhabilitarFilasGrilla(GridView_EquiposAdjuntosAEntrega, 1);

                Panel_ConfiguracionEquipos.Visible = false;
            }

            if ((GridView_AdjuntosAEntrega.Rows.Count > 0) || (GridView_EquiposAdjuntosAEntrega.Rows.Count > 0))
            {
                Button_GUARDAR.Visible = true;
                Button_GUARDAR_1.Visible = true;
            }
            else
            {
                Button_GUARDAR.Visible = false;
                Button_GUARDAR_1.Visible = false;
            }   
        }

        GridView_AdjuntosAEntrega.Columns[0].Visible = true;
        GridView_EquiposAdjuntosAEntrega.Columns[0].Visible = true;
    }

    protected void GridView_EquiposAdjuntosAEntrega_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "eliminar")
        {
            DataTable tablaDesdeGrilla = ObtenerDataTableDesde_GridView_EquiposAdjuntosAEntrega(); 

            tablaDesdeGrilla.Rows.RemoveAt(indexSeleccionado);

            Cargar_GridView_EquiposAdjuntosAEntrega_DesdeTabla(tablaDesdeGrilla);

            inhabilitarFilasGrilla(GridView_EquiposAdjuntosAEntrega, 1);

            Sincronizar_GridView_Entregas_y_GridView_AdjuntosAEntrega();
        }
    }
}