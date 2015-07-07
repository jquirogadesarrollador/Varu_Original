using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using Brainsbits.LLB.comercial;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB.seleccion;
using Brainsbits.LLB.almacen;
using Brainsbits.LLB;

using TSHAK.Components;

public partial class _Default : System.Web.UI.Page
{

    #region cargar drops y grids

    private void cargar_DropDownList_BUSCAR()
    {
        DropDownList_BUSCAR.Items.Clear();

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_BUSCAR.Items.Add(item);
        item = new ListItem("NÚMERO IDENTIFICACIÓN", "NUM_DOC_IDENTIFICACION");
        DropDownList_BUSCAR.Items.Add(item);
        item = new ListItem("NOMBRES", "NOMBRES");
        DropDownList_BUSCAR.Items.Add(item);
        item = new ListItem("APELLIDOS", "APELLIDOS");
        DropDownList_BUSCAR.Items.Add(item);
        item = new ListItem("EMPRESA", "RAZ_SOCIAL");
        DropDownList_BUSCAR.Items.Add(item);
        DropDownList_BUSCAR.DataBind();
    }
    private void cargar_GridView_Entregas_Configurados(DataTable tablaEntregas)
    {
        if (tablaEntregas.Rows.Count <= 0)
        {
            Label_MENSAJE.Text = "ADVERTENCIA: No se tiene configuradas entregas para este perfil. Verifique por favor";
            configurarMensajes(true, System.Drawing.Color.Red, Label_MENSAJE, Panel_MENSAJES);

        }
        else if (tablaEntregas.Columns.Count > 0)
        {
            GridView_Entregas_Configurados.DataSource = tablaEntregas;
            GridView_Entregas_Configurados.DataBind();
            Panel_MENSAJE_Entregas.Visible = false;
            Panel_Entregas_configurados.Visible = true;
            Panel_Entregas_configurados.Enabled = true;
            Panel_BOTONES_MENU.Visible = true;
        }
        else
        {
            GridView_Entregas_Configurados.DataSource = tablaEntregas;
            GridView_Entregas_Configurados.DataBind();
            
            Panel_MENSAJE_Entregas.Visible = false;
            Panel_Entregas_configurados.Visible = true;
            Panel_Entregas_configurados.Enabled = true;
            Panel_BOTONES_MENU.Visible = true;
        }
    }
    
    #endregion

    #region metodos para configurar controles

    private void configurarBotonesDeAccion(Boolean bImprimir, Boolean bGuardar)
    {
        Button_Imprimir.Visible = bImprimir;
        Button_Imprimir.Enabled = bImprimir;

        Button_Guardar.Visible = bGuardar;
        Button_Guardar.Enabled = bGuardar;
    }
    private void configurarInfoAdicionalModulo(Boolean ver, String texto)
    {
        Panel_INFO_ADICIONAL_MODULO.Visible = ver;
        Label_INFO_ADICIONAL_MODULO.Text = texto;
    }
    private void configurarMensajes(Boolean mostrarMensaje, System.Drawing.Color color, Label control, Panel panel)
    {
        panel.Visible = mostrarMensaje;
        control.Visible = mostrarMensaje;
        control.ForeColor = color;
    }
    private void configurarCaracteresAceptadosBusqueda(Boolean letras, Boolean numeros)
    {
        FilteredTextBoxExtender_TextBox_BUSCAR_Numbers.Enabled = numeros;
        FilteredTextBoxExtender_TextBox_BUSCAR_Letras.Enabled = letras;
    }
    private void cargar_menu_botones_modulos_internos(Boolean cargar_otro)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = "contratacion";
        QueryStringSeguro["nombre_area"] = "CONTRATOS Y RELACIONES LABORALES";

        TableRow filaTabla;
        TableCell celdaTabla;
        HyperLink link;
        Image imagen;

        int contadorFilas = 0;

        filaTabla = new TableRow();
        filaTabla.ID = "row_" + contadorFilas.ToString();

        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_1_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_examenes";
        if (String.IsNullOrEmpty(HiddenField_persona.Value.ToString()))
        {
            QueryStringSeguro["accion"] = "inicial";
            QueryStringSeguro["nombre_modulo"] = "EXAMENES Y CUENTA BANCARIA";
            QueryStringSeguro["proceso"] = ((int)tabla.proceso.ContactoContratacion).ToString();
            link.NavigateUrl = "~/contratacion/Copy of examenesEmpleado.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        }
        else
        {
            if (cargar_otro == false)
            {
                QueryStringSeguro["accion"] = "cargar";
            }
            else
            {
                QueryStringSeguro["accion"] = "cargar_otro";
            }
            QueryStringSeguro["nombre_modulo"] = "EXAMENES Y CUENTA BANCARIA";
            QueryStringSeguro["persona"] = HiddenField_persona.Value;
            link.NavigateUrl = "~/contratacion/examenesEmpleado.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        }

        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bExamenCuentaEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bExamenCuentaAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bExamenCuentaEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);


        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_2_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_afiliaciones";
        if (String.IsNullOrEmpty(HiddenField_persona.Value.ToString()))
        {
            QueryStringSeguro["accion"] = "inicial";
            QueryStringSeguro["nombre_modulo"] = "AFILIACIONES";
            link.NavigateUrl = "~/contratacion/afiliaciones.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        }
        else
        {
            QueryStringSeguro["accion"] = "cargar";
            QueryStringSeguro["nombre_modulo"] = "AFILIACIONES";
            QueryStringSeguro["persona"] = HiddenField_persona.Value;
            link.NavigateUrl = "~/contratacion/afiliaciones.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        }

        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bAfiliacionesEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bAfiliacionesAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bAfiliacionesEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);

        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_3_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_elaborar_contrato";
        if (String.IsNullOrEmpty(HiddenField_persona.Value.ToString()))
        {
            QueryStringSeguro["accion"] = "inicial";
            QueryStringSeguro["nombre_modulo"] = "ELABORAR CONTRATO";
            link.NavigateUrl = "~/contratacion/activarEmpleado.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        }
        else
        {
            QueryStringSeguro["accion"] = "cargar";
            QueryStringSeguro["nombre_modulo"] = "ELABORAR CONTRATO";
            QueryStringSeguro["persona"] = HiddenField_persona.Value;
            link.NavigateUrl = "~/contratacion/activarEmpleado.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        }

        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bElaborarContratoEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bElaborarContratoAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bElaborarContratoEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);


        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_4_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_servicios";
        if (String.IsNullOrEmpty(HiddenField_persona.Value.ToString()))
        {
            QueryStringSeguro["accion"] = "inicial";
            QueryStringSeguro["nombre_modulo"] = "SERVICIOS COMPLEMENTARIOS";
            link.NavigateUrl = "~/contratacion/entregasEmpleado.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        }
        else
        {
            if (cargar_otro == false)
            {
                QueryStringSeguro["accion"] = "cargar";
            }
            else
            {
                QueryStringSeguro["accion"] = "cargar_otro";
            }
            QueryStringSeguro["nombre_modulo"] = "SERVICIOS COMPLEMENTARIOS";
            QueryStringSeguro["persona"] = HiddenField_persona.Value;
            link.NavigateUrl = "~/contratacion/entregasEmpleado.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        }

        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bServiciosComplementariosEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bServiciosComplementariosAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bServiciosComplementariosEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);


        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_5_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_volver";
        QueryStringSeguro["accion"] = "inicial";
        link.NavigateUrl = "~/contratacion/hojaTrabajoContratacion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bVolverHojaVidaEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bVolverHojaVidaAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bVolverHojaVidaEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);

        Table_MENU.Rows.Add(filaTabla);
    }
    #endregion
    
    #region metodos que se ejecutan al cargar la pagina

    private void iniciar_interfaz_inicial()
    {
        configurarInfoAdicionalModulo(false, "");

        Panel_FORM_BOTONES.Visible = true;

        Panel_MENSAJES.Visible = false;
        Panel_MENSAJE_Entregas.Visible = false;

        Panel_RESULTADOS_GRID.Visible = false;

        Panel_BOTONES_MENU.Visible = false;

        Panel_Entregas_configurados.Visible = false;
        
    }
    private void iniciar_interfaz_para_registro_existente()
    {   
        String persona = HiddenField_persona.Value;
        String[] datos = persona.Split(',');

        String id_solicitud = datos[0];
        String id_requerimiento = datos[1];
        String id_empleado = datos[6];
        String id_contrato = datos[5];
        String id_Centro_Costo;
        String id_SubCentro_Costo;
        String id_ciudad;
        String id_servicio;


           


            





                
                
                

                       

                           
                   

                    
                    
    }
    private void iniciar_seccion_de_busqueda()
    {
        TextBox_BUSCAR.Text = "";
        cargar_DropDownList_BUSCAR();
    }

    #endregion

    #region METODOS
    
    protected DataTable ConsultarInventario(String centroCostos, String subcentroCosto, String ciudad, String[] datos, DataRow fila)
    {
        String id_Ciudad;
        DataTable tablaentregas = new DataTable();
        tablaentregas.Columns.Add("ID_PRODUCTO");
        tablaentregas.Columns.Add("ID_LOTE");
        tablaentregas.Columns.Add("CANTIDAD");
        tablaentregas.Columns.Add("TALLA");

        DataRow entrega = tablaentregas.NewRow();

       
        if (!(centroCostos.Equals("0")))
        {
            centroCosto _centroC = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaCentro = _centroC.ObtenerCentrosDeCostoPorIdCentroCosto(Convert.ToDecimal(centroCostos));
            DataRow filaCentros = tablaCentro.Rows[0];

            id_Ciudad = filaCentros["ID_CIUDAD"].ToString();
        }
        else if (!(subcentroCosto.Equals("0")))
        {
            subCentroCosto _SubCentroC = new subCentroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaSub = _SubCentroC.ObtenerSubCentrosDeCostoPorIdSubCosto(Convert.ToDecimal(subcentroCosto));
            DataRow filaSubCentro = tablaSub.Rows[0];

            centroCosto _centroC = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaCentro = _centroC.ObtenerCentrosDeCostoPorIdCentroCosto(Convert.ToDecimal(filaSubCentro["ID_CENTRO_C"].ToString()));
            DataRow filaCentros = tablaCentro.Rows[0];

            id_Ciudad = filaCentros["ID_CIUDAD"].ToString();
        }
        else
        {
            id_Ciudad = ciudad;
        }


        int id_empresa = Convert.ToInt32(datos[3].ToString());
        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
        DataTable tablaCiudad = _ciudad.ObtenerCiudadPorIdCiudad(id_Ciudad);
        DataRow filaCiudad = tablaCiudad.Rows[0];

        String id_regional = filaCiudad["ID_REGIONAL"].ToString();
        bodega _bodega = new bodega(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablabodega = _bodega.ObtenerAlmRegBodegaPorIds(id_regional, id_Ciudad, id_empresa);

        if (tablabodega.Rows.Count <= 0)
        {
            Label_MENSAJE.Text = "ADVERTENCIA: No se encontraron bodegas para la empresa en la ubicación seleccionada. Valide por favor.";
            configurarMensajes(true, System.Drawing.Color.Red, Label_MENSAJE, Panel_MENSAJES);
        }
        else
        {
            DataRow filaBodega = tablabodega.Rows[0];

            lote _lote = new lote(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            HiddenField_idBodega.Value = filaBodega["ID_BODEGA"].ToString();
 
            DataTable tablaLote = _lote.ObtenerAlmLotePorIdProductoBodega(Convert.ToInt32(fila["ID_PRODUCTO"]), Convert.ToInt32(filaBodega["ID_BODEGA"].ToString()));
            if (tablaLote.Rows.Count <= 0)
            {
                Label_MENSAJE.Text = "ADVERTENCIA: No hay existencias del producto " + fila["NOMBRE"].ToString() + " para la empresa seleccionada. Valide por favor.";
                configurarMensajes(true, System.Drawing.Color.Red, Label_MENSAJE, Panel_MENSAJES);
            }
            else
            {
                int contLote;
                foreach (DataRow filalotes in tablaLote.Rows)
                {
                    entrega = tablaentregas.NewRow();

                    contLote = Convert.ToInt32(filalotes["ENTRADAS"]) - Convert.ToInt32(filalotes["SALIDAS"]);
                    
                    entrega[0] = filalotes["ID_PRODUCTO"];
                    entrega[1] = filalotes["ID_LOTE"];
                    entrega[2] = contLote;
                    entrega[3] = filalotes["TALLA"];
                    
                    tablaentregas.Rows.Add(entrega);
                }
            }
        }
        return tablaentregas;
    }
    
    public void activarEmpleado()
    {
        radicacionHojasDeVida _sol = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        registroContrato contrato = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        String valores = HiddenField_persona.Value;
        String[] val = valores.Split(',');

        DataTable tablaCont = contrato.ObtenerConRegContratosPorRegistro(Convert.ToInt32(val[5].ToString()));
        DataRow filacont = tablaCont.Rows[0];

        if (filacont["CONTRATO_IMPRESO"].Equals("S") & filacont["CLAUSULA_IMPRESO"].Equals("S"))
        {
            _sol.ActualizarEstadoRegSolicitudesIngreso(Convert.ToInt32(val[1].ToString()), Convert.ToInt32(val[0].ToString()), "CONTRATADO");
            Label_MENSAJE_Entregas.Text = "Se activo el empleado en el sistema";
            configurarMensajes(true, System.Drawing.Color.Green, Label_MENSAJE_Entregas, Panel_MENSAJE_Entregas);
        }
        else
        {
            Label_MENSAJE_Entregas.Text = "ADVERTENCIA: No se han impreso las clausulas o el contrato. Verifique por favor";
            configurarMensajes(true, System.Drawing.Color.Red, Label_MENSAJE_Entregas, Panel_MENSAJE_Entregas);
        }
    }
    
    #endregion 

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = "SERVICIOS COMPLEMENTARIOS";
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        if (IsPostBack == false)
        {
            
            String accion = QueryStringSeguro["accion"].ToString();
            if (accion == "inicial")
            {
                iniciar_interfaz_inicial();
                iniciar_seccion_de_busqueda();
                Panel_BOTONES_INTERNOS.Visible = true;
                cargar_menu_botones_modulos_internos(false);
            }
            else if (accion == "cargar")
            {
                String persona = QueryStringSeguro["persona"].ToString();
                String[] datos = persona.Split(',');
                String datosPersona;
                String ID_EMPRESA = datos[3];
                registroContrato _cont = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                DataTable tablaCont =  _cont.ObtenerPorNumIdentificacionActivo(datos[4].ToString());
                
                Panel_BOTONES_INTERNOS.Visible = true;

                if (tablaCont.Rows.Count <= 0)
                {
                    Label_MENSAJE.Text = "ADVERTENCIA: A la persona no puede entregarsele dotación ni epp por que no ha sido contratado";
                    configurarMensajes(true, System.Drawing.Color.Red, Label_MENSAJE, Panel_MENSAJES);

                    Panel_Entregas_configurados.Visible = false;
                    Panel_FORM_BOTONES.Visible = false;
                    Panel_RESULTADOS_GRID.Visible = false;
                    Panel_BOTONES_MENU.Visible = false;

                    Panel_MENSAJES.Visible = true;
                    Panel_MENSAJE_Entregas.Visible = false;


                    String ID_SOLICITUD = datos[0];
                    String ID_REQUERIMIENTO = datos[1];
                    String ID_OCUPACION = datos[2];
                    String NUM_DOC_IDENTIDAD = datos[4];

                    radicacionHojasDeVida _solIngreso = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                    DataTable tablasol = _solIngreso.ObtenerRegSolicitudesingresoPorNumDocIdentidad(NUM_DOC_IDENTIDAD);

                    DataRow filaSolIngreso = tablasol.Rows[0];
                    String nombre = filaSolIngreso["NOMBRES"] + " " + filaSolIngreso["APELLIDOS"];

                    cliente _empresa = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                    DataTable tablaEmpresa = _empresa.ObtenerEmpresaConIdEmpresa(Convert.ToDecimal(ID_EMPRESA));
                    DataRow filaEmpresa = tablaEmpresa.Rows[0];

                    cargo _cargo = new cargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                    DataTable tablaOcupacion = _cargo.ObtenerOcupacionPorIdOcupacion(Convert.ToDecimal(ID_OCUPACION));
                    DataRow filaOcupacion = tablaOcupacion.Rows[0];

                    datosPersona = "Nombre: " + nombre + "<br> Numero Identificación: " + NUM_DOC_IDENTIDAD + "<br> Empresa: " + filaEmpresa["RAZ_SOCIAL"] + "<br>Cargo: " + filaOcupacion["NOM_OCUPACION"];

                    HiddenField_persona.Value = persona.Trim();
                    configurarInfoAdicionalModulo(true, datosPersona);
                    cargar_menu_botones_modulos_internos(false);

                    Panel_BOTONES_INTERNOS.Visible = true;
                }
               else
                {
                    DataRow filaCon = tablaCont.Rows[0];
                    
                    Panel_Entregas_configurados.Visible = false;
                    Panel_FORM_BOTONES.Visible = false;
                    Panel_RESULTADOS_GRID.Visible = false;
                    Panel_BOTONES_MENU.Visible = false;

                    Panel_MENSAJES.Visible = false;
                    Panel_MENSAJE_Entregas.Visible = false;

                    String ID_SOLICITUD = datos[0];
                    String ID_REQUERIMIENTO = datos[1];
                    String ID_OCUPACION = datos[2];
                    String NUM_DOC_IDENTIDAD = datos[4];
                    String ID_CONTRATO;
                    String ID_EMPLEADO;

                    if (datos.Length == 7)
                    {
                        ID_CONTRATO = datos[5];
                        ID_EMPLEADO = datos[6];
                    }
                    else
                    { 
                        ID_CONTRATO = filaCon["ID_CONTRATO"].ToString() ;
                        ID_EMPLEADO = filaCon["ID_EMPLEADO"].ToString();
                    }

                    radicacionHojasDeVida _solIngreso = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                    DataTable tablasol = _solIngreso.ObtenerRegSolicitudesingresoPorNumDocIdentidad(NUM_DOC_IDENTIDAD);

                    DataRow filaSolIngreso = tablasol.Rows[0];
                    String nombre = filaSolIngreso["NOMBRES"] + " " + filaSolIngreso["APELLIDOS"];

                    cliente _empresa = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                    DataTable tablaEmpresa = _empresa.ObtenerEmpresaConIdEmpresa(Convert.ToDecimal(ID_EMPRESA));
                    DataRow filaEmpresa = tablaEmpresa.Rows[0];

                    cargo _cargo = new cargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                    DataTable tablaOcupacion = _cargo.ObtenerOcupacionPorIdOcupacion(Convert.ToDecimal(ID_OCUPACION));
                    DataRow filaOcupacion = tablaOcupacion.Rows[0];

                    persona = "Nombre: " + nombre + "<br> Numero Identificación: " + NUM_DOC_IDENTIDAD + "<br> Empresa: " + filaEmpresa["RAZ_SOCIAL"] + "<br>Cargo: " + filaOcupacion["NOM_OCUPACION"];
                    configurarInfoAdicionalModulo(true, persona);

                    HiddenField_persona.Value = ID_SOLICITUD + "," + ID_REQUERIMIENTO + "," + ID_OCUPACION + "," + ID_EMPRESA + "," + NUM_DOC_IDENTIDAD.Trim() + "," + ID_CONTRATO + "," + ID_EMPLEADO;

                    cargar_menu_botones_modulos_internos(false);
                    
                    iniciar_interfaz_para_registro_existente();
                }
            }
        }
    }

    #region eventos

    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {

        String datosCapturados = TextBox_BUSCAR.Text.ToUpper();
        String campo = DropDownList_BUSCAR.SelectedValue.ToString();

        requisicion _requicision = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        
        DataTable tablaResultadosBusqueda = new DataTable();

        if (DropDownList_BUSCAR.SelectedValue == "NUM_DOC_IDENTIFICACION")
        {
            tablaResultadosBusqueda = _requicision.ObtenerPersonasContratadasPorNumDocIdentificacion(datosCapturados);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "NOMBRES")
        {
            tablaResultadosBusqueda = _requicision.ObtenerPersonasContratadasPorNombres(datosCapturados);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "APELLIDOS")
        {
            tablaResultadosBusqueda = _requicision.ObtenerPersonasContratadasPorApellidos(datosCapturados);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "RAZ_SOCIAL")
        {
            tablaResultadosBusqueda = _requicision.ObtenerPersonasContratadasPorEmpresa(datosCapturados);
        }
        if (tablaResultadosBusqueda.Rows.Count <= 0)
        {
            configurarMensajes(true, System.Drawing.Color.Red, Label_MENSAJE, Panel_MENSAJES);
            if (_requicision.MensajeError != null)
            {
                Label_MENSAJE.Text = _requicision.MensajeError;
            }
            else
            {
                Label_MENSAJE.Text = "ADVERTENCIA: No se encontraron registros que cumplieran los datos de busqueda.";
            }

            Panel_RESULTADOS_GRID.Visible = false;
        }
        else
        {
            configurarMensajes(false, System.Drawing.Color.Green, Label_MENSAJE, Panel_MENSAJES);

            Panel_RESULTADOS_GRID.Visible = true;
            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaResultadosBusqueda;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
            DataRow filaParaColocarColor;
            int contadorAlertasBajas = 0;
            int contadorAlertasMedias = 0;
            int contadorAlertasAltas = 0;
            int contadorAlertasNinguna = 0;

            for (int i = 0; i < tablaResultadosBusqueda.Rows.Count; i++)
            {
                filaParaColocarColor = tablaResultadosBusqueda.Rows[i];

                if (filaParaColocarColor["ALERTA"].ToString().Trim() == "ALTA")
                {
                    GridView_RESULTADOS_BUSQUEDA.Rows[i].BackColor = System.Drawing.Color.Red;
                    contadorAlertasAltas += 1;
                }
                else
                {
                    if (filaParaColocarColor["ALERTA"].ToString().Trim() == "MEDIA")
                    {
                        GridView_RESULTADOS_BUSQUEDA.Rows[i].BackColor = System.Drawing.Color.Yellow;
                        contadorAlertasMedias += 1;
                    }
                    else
                    {
                        if (filaParaColocarColor["ALERTA"].ToString().Trim() == "BAJA")
                        {
                            GridView_RESULTADOS_BUSQUEDA.Rows[i].BackColor = System.Drawing.Color.Green;
                            contadorAlertasBajas += 1;
                        }
                        else
                        {
                            GridView_RESULTADOS_BUSQUEDA.Rows[i].BackColor = System.Drawing.Color.Gray;
                            contadorAlertasNinguna += 1;
                        }
                    }
                }

                if (i == (GridView_RESULTADOS_BUSQUEDA.Rows.Count - 1))
                {
                    GridView_RESULTADOS_BUSQUEDA.Rows[i].Cells[1].Text = "";
                }
            }

            Label_ALERTA_ALTA.Text = contadorAlertasAltas.ToString();
            Label_ALERTA_MEDIA.Text = contadorAlertasMedias.ToString();
            Label_ALERTA_BAJA.Text = contadorAlertasBajas.ToString();
        }
    }
    protected void Button_Imprimir_Click(object sender, EventArgs e)
    {
        parametro _parametro = new parametro(Session["idEmpresa"].ToString());
        DataTable TablaservidorWeb = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_SERVIDOR_WEB);
        DataRow filaServidorWeb = TablaservidorWeb.Rows[0];
        String servidorWeb = filaServidorWeb["DESCRIPCION"].ToString();
        String persona = HiddenField_persona.Value;
        String[] datos = persona.Split(',');
        String EMPRESA;
        if (Session["idEmpresa"].ToString().Equals("3"))
        {
            EMPRESA = "EFICIENCIA&SERVICIO";
        }
        else
        {
            EMPRESA = "SERTEMPO";
        }

        Response.Redirect(servidorWeb + "?%2fEntregas&rs:Command=Render&EMPRESA=" + EMPRESA + "&EMPLEADO=" + datos[6].ToString() + "&rs:format=pdf");
        
    }
    protected void Button_Guardar_Click(object sender, EventArgs e)
    {
        EntregaServicioComplementario _entregas = new EntregaServicioComplementario(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        String idEntrega = null;
        String talla = null;
        DropDownList dropTalla;
        TextBox CantidadaEntregar; 
        DataTable tablaConfigurados = new DataTable();
        tablaConfigurados.Columns.Add("ID_PRODUCTO");
        tablaConfigurados.Columns.Add("CANTIDAD_A_ENTREGAR");
        tablaConfigurados.Columns.Add("TALLA");
        
        DataTable tablaLotes = new DataTable();
        tablaLotes.Columns.Add("ID_PRODUCTO");
        tablaLotes.Columns.Add("ID_LOTE");
        tablaLotes.Columns.Add("TALLA");
        tablaLotes.Columns.Add("CONTENIDO_LOTE");
        tablaLotes.Columns.Add("CANTIDAD_CONFIGURADA");

        String[] datos = HiddenField_persona.Value.Split(',');
        int empleado = Convert.ToInt32(datos[6].ToString());
        int id_producto;
        int id_lote;
        int ContenidoLote;
        int cantidadConf;
        DateTime fecha = System.DateTime.Today;

        for (int i = 0; i < GridView_Entregas_Configurados.Rows.Count; i++)
        {
            id_producto = Convert.ToInt32(GridView_Entregas_Configurados.DataKeys[i].Values["ID_PRODUCTO"].ToString());
            dropTalla = GridView_Entregas_Configurados.Rows[i].FindControl("DropDownList_Talla") as DropDownList;
            CantidadaEntregar = GridView_Entregas_Configurados.Rows[i].FindControl("TextBox_Cantidad") as TextBox;

            DataRow productosConfigurados = tablaConfigurados.NewRow();

            productosConfigurados[0] = id_producto;
            productosConfigurados[1] = CantidadaEntregar.Text;
            productosConfigurados[2] = dropTalla.SelectedValue;

            tablaConfigurados.Rows.Add(productosConfigurados);

        }
        for (int i = 0; i < GridView_lotes.Rows.Count; i++)
        {
            id_producto = Convert.ToInt32(GridView_lotes.DataKeys[i].Values["ID_PRODUCTO"].ToString());
            id_lote = Convert.ToInt32(GridView_lotes.DataKeys[i].Values["ID_LOTE"].ToString());
            talla = GridView_lotes.Rows[i].Cells[3].Text;
            ContenidoLote = Convert.ToInt32(GridView_lotes.Rows[i].Cells[2].Text);
            cantidadConf = Convert.ToInt32(GridView_lotes.Rows[i].Cells[4].Text);
            
            DataRow lotesDisponibles = tablaLotes.NewRow();

            lotesDisponibles[0] = id_producto;
            lotesDisponibles[1] = id_lote;
            lotesDisponibles[2] = talla;
            lotesDisponibles[3] = ContenidoLote;
            lotesDisponibles[4] = cantidadConf;

            tablaLotes.Rows.Add(lotesDisponibles);
        }
        if (HiddenField_idBodega.Value.Equals(""))
        {
            Label_MENSAJE_Entregas.Text = "ADVERTENCIA: No se tiene bodega. Verifique con el administrador";
            configurarMensajes(true, System.Drawing.Color.Red, Label_MENSAJE_Entregas, Panel_MENSAJE_Entregas);
        }
        else
        {

            int idbodega = Convert.ToInt32(HiddenField_idBodega.Value);
            String faltantes = HiddenField_Faltantes.Value;
            idEntrega = _entregas.adicionarEntregas(empleado, fecha, idbodega, tablaConfigurados, tablaLotes, datos, faltantes);

            HiddenField_idEntrega.Value = idEntrega;

            if (idEntrega.Equals(""))
            {
                Label_MENSAJE_Entregas.Text = "ADVERTENCIA: No se almaceno correctamente la entrega. Verifique por favor" + _entregas.MensajeError;
                configurarMensajes(true, System.Drawing.Color.Red, Label_MENSAJE_Entregas, Panel_MENSAJE_Entregas);
            }
            else
            {
                Panel_Entregas_configurados.Enabled = false;
                registroContrato _contrato = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                SecureQueryString QueryStringSeguro;
                tools _tools = new tools();
                QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);
                String persona = QueryStringSeguro["persona"].ToString();
                String[] datosEmpleado = persona.Split(',');
                radicacionHojasDeVida _solIngreso = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

                _solIngreso.ActualizarEstadoProcesoRegSolicitudesIngreso(Convert.ToInt32(datosEmpleado[1]), Convert.ToInt32(datosEmpleado[0]), "CONTRATADO", Session["USU_LOG"].ToString());


                Label_MENSAJE_Entregas.Text = "La entrega se almacenó correctamente. Número entrega: " + idEntrega;
                configurarMensajes(true, System.Drawing.Color.Green, Label_MENSAJE_Entregas, Panel_MENSAJE_Entregas);
                activarEmpleado();
            }
        }
    }
   
    protected void GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged(object sender, EventArgs e)
    {

        











    }
    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;
        
        String datosCapturados = TextBox_BUSCAR.Text.ToUpper();
        String campo = DropDownList_BUSCAR.SelectedValue.ToString();

        requisicion _requicision = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaResultadosBusqueda = new DataTable();

        if (DropDownList_BUSCAR.SelectedValue == "NUM_DOC_IDENTIDDAD")
        {
            tablaResultadosBusqueda = _requicision.ObtenerPersonasContratadasPorNumDocIdentificacion(datosCapturados);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "NOMBRES")
        {
            tablaResultadosBusqueda = _requicision.ObtenerPersonasContratadasPorNombres(datosCapturados);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "APELLIDOS")
        {
            tablaResultadosBusqueda = _requicision.ObtenerPersonasContratadasPorApellidos(datosCapturados);
        }
        else if (DropDownList_BUSCAR.SelectedValue == "RAZ_SOCIAL")
        {
            tablaResultadosBusqueda = _requicision.ObtenerPersonasContratadasPorEmpresa(datosCapturados);
        }

        if (tablaResultadosBusqueda.Rows.Count <= 0)
        {
            Panel_MENSAJES.Enabled = true;
            Panel_MENSAJES.Visible = true;
            Label_MENSAJE.ForeColor = System.Drawing.Color.Red;
            if (_requicision.MensajeError != null)
            {
                Label_MENSAJE.Text = _requicision.MensajeError;
            }
            else
            {
                Label_MENSAJE.Text = "ADVERTENCIA: No se encontraron registros que cumpliera con los datos de busqueda.";
            }
        }
        else
        {
            Panel_MENSAJES.Enabled = false;
            Panel_MENSAJES.Visible = false;
            Panel_RESULTADOS_GRID.Enabled = true;
            Panel_RESULTADOS_GRID.Visible = true;
            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaResultadosBusqueda;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
        }
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
    }

    #endregion
}