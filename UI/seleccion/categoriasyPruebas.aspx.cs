using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brainsbits.LLB.maestras;
using TSHAK.Components;
using Brainsbits.LLB.seleccion;
using System.Data;
using System.IO;
using Brainsbits.LLB;
using Brainsbits.LLB.seguridad;

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

    #region CARGA DE GRILLAS Y DROPS
    private void cargar_GridView_RESULTADOS_BUSQUEDA(DataTable tablaCategorias)
    {
        GridView_RESULTADOS_BUSQUEDA.DataSource = tablaCategorias;
        GridView_RESULTADOS_BUSQUEDA.DataBind(); 
    }
    private void cargar_GridView_GRILLA_PRUEBAS(DataTable tablaPruebas)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        
        GridView_GRILLA_PRUEBAS.DataSource = tablaPruebas;
        GridView_GRILLA_PRUEBAS.DataBind();

        HyperLink linkAdjunto;
        DataRow filainfoPrueba;

        for (int i = 0; i < tablaPruebas.Rows.Count; i++)
        {
            filainfoPrueba = tablaPruebas.Rows[i];
            linkAdjunto = GridView_GRILLA_PRUEBAS.Rows[i].FindControl("HyperLink_MANUAL_ADJUNTO") as HyperLink;

            if (filainfoPrueba["ARCHIVO_EXTENSION"] != DBNull.Value)
            {
                QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
                QueryStringSeguro["prueba"] = filainfoPrueba["ID_PRUEBA"].ToString();

                linkAdjunto.NavigateUrl = "~/seleccion/visorManualesPruebas.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
                linkAdjunto.Text = "Ver manual";
                linkAdjunto.Visible = true;
            }
            else
            {
                linkAdjunto.Visible = false;
            }
        }
    }
    #endregion

    #region CONFIGURAR CONTROLES
    private void configurarPanelesGenerales(Boolean bPanel_RESULTADOS_GRID, Boolean bPanel_FORMULARIO_CATEGORIAS, Boolean bPanel_FORMULARIO_PRUEBAS, Boolean bPanel_BOTONES_CATEGORIA, Boolean bPanel_BOTONES_PRUEBAS)
    {
        Panel_RESULTADOS_GRID.Visible = bPanel_RESULTADOS_GRID;
        Panel_FORMULARIO_CATEGORIAS.Visible = bPanel_FORMULARIO_CATEGORIAS;
        Panel_FORMULARIO_PRUEBAS.Visible = bPanel_FORMULARIO_PRUEBAS;
        Panel_BOTONES_CATEGORIA.Visible = bPanel_BOTONES_CATEGORIA;
        Panel_BOTONES_PRUEBAS.Visible = bPanel_BOTONES_PRUEBAS;
    }
    private void configurarBotonesCategorias(Boolean bNuevo, Boolean bModificar, Boolean bGuardar, Boolean bCancelar)
    {
        Button_NUEVA_CATEGOORIA.Visible = bNuevo;
        Button_NUEVA_CATEGOORIA.Enabled = bNuevo;
        Button_NUEVA_CATEGORIA.Visible = bNuevo;
        Button_NUEVA_CATEGORIA.Enabled = bNuevo;

        Button_MODIFICAR_CATEGORIA.Visible = bModificar;
        Button_MODIFICAR_CATEGORIA.Enabled = bModificar;

        Button_GUARDAR_CATEGORIA.Visible = bGuardar;
        Button_GUARDAR_CATEGORIA.Enabled = bGuardar;

        Button_CANCELAR_CATEGORIA.Visible = bCancelar;
        Button_CANCELAR_CATEGORIA.Enabled = bCancelar;
    }
    private void configurarBotonesPruebas(Boolean bNuevo, Boolean bGuardar, Boolean bCancelar)
    {
        Button_NNUEVA_PRUEBA.Visible = bNuevo;
        Button_NNUEVA_PRUEBA.Enabled = bNuevo;

        Button_GUARDAR_PRUEBA.Visible = bGuardar;
        Button_GUARDAR_PRUEBA.Enabled = bGuardar;

        Button_CANCELAR_PRUEBA.Visible = bCancelar;
        Button_CANCELAR_PRUEBA.Enabled = bCancelar;
    }
    private void configurarInfoAdicionalModulo(Boolean ver, String texto)
    {
        Panel_INFO_ADICIONAL_MODULO.Visible = ver;
        Label_INFO_ADICIONAL_MODULO.Text = texto;
    }
    private void configurarMensajes(Boolean mostrarMensaje, System.Drawing.Color color)
    {
        Panel_MENSAJES.Visible = mostrarMensaje;
        Label_MENSAJE.Visible = mostrarMensaje;
        Label_MENSAJE.ForeColor = color;
    }
    private void configurarMensajesPruebas(Boolean mostrarMensaje, System.Drawing.Color color)
    {
        Panel_MENSAJES_PRUEBAS.Visible = mostrarMensaje;
        Label_MENSAJES_PRUEBAS.Visible = mostrarMensaje;
        Label_MENSAJES_PRUEBAS.ForeColor = color;
    }
    #endregion
    #region SE EJECUTAN AL CARGAR LA PAGINA
    private void cargar_interfaz_inicial()
    {
        configurarInfoAdicionalModulo(false, "");

        categoriaPruebas _cat = new categoriaPruebas(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaCat = _cat.ObtenerSelCatPruebasTodas();

        if (tablaCat.Rows.Count <= 0)
        {
            configurarMensajes(true, System.Drawing.Color.Red);
            Label_MENSAJE.Text = "No se encontraron Categorías de prueba.";

            configurarMensajesPruebas(false, System.Drawing.Color.Red);

            configurarPanelesGenerales(false, false, false, false, false);
        }
        else
        {
            configurarMensajes(false, System.Drawing.Color.Red);

            configurarMensajesPruebas(false, System.Drawing.Color.Red);

            cargar_GridView_RESULTADOS_BUSQUEDA(tablaCat);

            configurarPanelesGenerales(true, false, false, false, false);
        }
    }
    private void cargar_interfaz_nueva_categoria()
    {
        configurarInfoAdicionalModulo(true, "Nueva Categoría de prueba");

        configurarMensajes(false, System.Drawing.Color.Red);
        configurarMensajesPruebas(false, System.Drawing.Color.Red);

        TextBox_ID_CATEGORIA.Text = "";

        configurarPanelesGenerales(false, true, false, true, false);

        Panel_CONTROL_REGISTRO.Visible = false;
        Panel_ID_CATEGORIA.Visible = false;
        Panel_DATOS_CATEGORIA.Visible = true;

        configurarBotonesCategorias(false, false, true, true);
    }
    private void cargar_datos_categoria(DataRow filaInfoCategoria)
    {
        TextBox_USU_CRE.Text = filaInfoCategoria["USU_CRE"].ToString();
        try
        {
            TextBox_FCH_CRE.Text = DateTime.Parse(filaInfoCategoria["FCH_CRE"].ToString()).ToShortDateString();
            TextBox_HOR_CRE.Text = DateTime.Parse(filaInfoCategoria["FCH_CRE"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_CRE.Text = "";
            TextBox_HOR_CRE.Text = "";
        }
        TextBox_USU_MOD.Text = filaInfoCategoria["USU_MOD"].ToString();
        try
        {
            TextBox_FCH_MOD.Text = DateTime.Parse(filaInfoCategoria["FCH_MOD"].ToString()).ToShortDateString();
            TextBox_HOR_MOD.Text = DateTime.Parse(filaInfoCategoria["FCH_MOD"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_MOD.Text = "";
            TextBox_HOR_MOD.Text = "";
        }

        TextBox_NOM_CATEGORIA.Text = filaInfoCategoria["NOM_CATEGORIA"].ToString().Trim();
        TextBox_Observaciones.Text = filaInfoCategoria["OBS_CATEGORIA"].ToString().Trim();

        TextBox_ID_CATEGORIA.Text = filaInfoCategoria["ID_CATEGORIA"].ToString().Trim();
    }
    private void cargar_interfaz_cargar_categoria(Boolean modificar)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        int ID_CATEGORIA = Convert.ToInt32(QueryStringSeguro["categoria"]);

        categoriaPruebas _categoriaPruebas = new categoriaPruebas(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaInfoCategoria = new DataTable();

        tablaInfoCategoria = _categoriaPruebas.ObtenerSelCatPruebasPorIdCat(ID_CATEGORIA);

        if (tablaInfoCategoria.Rows.Count <= 0)
        {
            if (_categoriaPruebas.MensajeError == null)
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = "ADVERETENCIA: No se encontró información sobre la categoría.";
            }
            else
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = _categoriaPruebas.MensajeError;
            }

            configurarPanelesGenerales(false, false, false, false, false);

            configurarBotonesCategorias(true, false, false, false);
        }
        else
        {
            configurarMensajes(false, System.Drawing.Color.Red);

            DataRow informacionCategoria = tablaInfoCategoria.Rows[0];

            Page.Header.Title = informacionCategoria["NOM_CATEGORIA"].ToString();

            configurarInfoAdicionalModulo(true, "Categoría: " + informacionCategoria["NOM_CATEGORIA"].ToString().Trim());

            cargar_datos_categoria(informacionCategoria);

            if (modificar == true)
            {
                Panel_CONTROL_REGISTRO.Visible = false;
                Panel_ID_CATEGORIA.Visible = true;

                Panel_FORMULARIO_CATEGORIAS.Enabled = true;

                configurarBotonesCategorias(false, false, true, true);
            }
            else
            {
                Panel_CONTROL_REGISTRO.Visible = true;
                Panel_ID_CATEGORIA.Visible = true;

                Panel_FORMULARIO_CATEGORIAS.Enabled = false;

                configurarBotonesCategorias(true, true, false, false);
            }

            DataTable tablaPruebas = _categoriaPruebas.ObtenerSelPruebasPorIdCat(ID_CATEGORIA);

            if (tablaPruebas.Rows.Count <= 0)
            {
                if (_categoriaPruebas.MensajeError != null)
                {
                    configurarMensajes(true, System.Drawing.Color.Red);
                    Label_MENSAJE.Text = _categoriaPruebas.MensajeError;

                    configurarPanelesGenerales(false, false, false, true, true);
                }
                else
                {
                    configurarPanelesGenerales(false, true, false, true, true);
                }

                Panel_PRUEBAS_CONFIGURADAS_ACTUALMENTE.Visible = false;

                configurarMensajesPruebas(true, System.Drawing.Color.Red);
                Label_MENSAJES_PRUEBAS.Text = "ADVERTENCIA: No existen pruebas para esta categoría.";

                configurarBotonesPruebas(true, false, false);
            }
            else
            {
                configurarMensajesPruebas(false, System.Drawing.Color.Red);

                cargar_GridView_GRILLA_PRUEBAS(tablaPruebas);
                Panel_PRUEBAS_CONFIGURADAS_ACTUALMENTE.Visible = true;

                configurarPanelesGenerales(false, true, true, true, true);

                configurarBotonesPruebas(true, false, false);
            }

            Panel_NUEVA_PRUEBA.Visible = false;

            if (modificar == true)
            {
                configurarPanelesGenerales(false, true, false, true, false);

                configurarMensajesPruebas(false, System.Drawing.Color.Red);
            }
        }
    }
    private void cargar_datos_prueba(DataRow filaInfoPrueba)
    {

        TextBox_ID_PRUEBA.Text = filaInfoPrueba["ID_PRUEBA"].ToString().Trim();

        TextBox_NOM_PRUEBA.Text = filaInfoPrueba["NOM_PRUEBA"].ToString().Trim();

        TextBox_Comentarios.Text = filaInfoPrueba["OBS_PRUEBA"].ToString().Trim();

        if (filaInfoPrueba["ARCHIVO_EXTENSION"] != DBNull.Value)
        {
            tools _tools = new tools();
            SecureQueryString QueryStringSeguro;
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

            QueryStringSeguro["prueba"] = filaInfoPrueba["ID_PRUEBA"].ToString();

            HyperLink_ARCHIVO_PRUEBA_SELECCIONADA.NavigateUrl = "~/seleccion/visorManualesPruebas.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            HyperLink_ARCHIVO_PRUEBA_SELECCIONADA.Text = "Ver manual";
            Panel_ARCHIVO_ACTUAL.Visible = true;
        }
        else
        {
            Panel_ARCHIVO_ACTUAL.Visible = false;
        }

    }
    private void cargar_interfaz_cargar_prueba(Boolean modificar)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        int ID_CATEGORIA = Convert.ToInt32(QueryStringSeguro["categoria"]);
        Decimal ID_PRUEBA = Convert.ToDecimal(QueryStringSeguro["prueba"]); 

        categoriaPruebas _categoriaPruebas = new categoriaPruebas(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaInfoCategoria = new DataTable();

        tablaInfoCategoria = _categoriaPruebas.ObtenerSelCatPruebasPorIdCat(ID_CATEGORIA);

        if (tablaInfoCategoria.Rows.Count <= 0)
        {
            if (_categoriaPruebas.MensajeError == null)
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = "ADVERETENCIA: No se encontró información sobre la categoría.";
            }
            else
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = _categoriaPruebas.MensajeError;
            }

            configurarPanelesGenerales(false, false, false, false, false);

            configurarBotonesCategorias(true, false, false, false);
        }
        else
        {
            configurarMensajes(false, System.Drawing.Color.Red);

            DataRow informacionCategoria = tablaInfoCategoria.Rows[0];

            Page.Header.Title = informacionCategoria["NOM_CATEGORIA"].ToString();

            configurarInfoAdicionalModulo(true, "Categoría: " + informacionCategoria["NOM_CATEGORIA"].ToString().Trim());

            cargar_datos_categoria(informacionCategoria);

            Panel_CONTROL_REGISTRO.Visible = true;
            Panel_ID_CATEGORIA.Visible = true;

            Panel_FORMULARIO_CATEGORIAS.Enabled = false;

            configurarBotonesCategorias(false, false, false, false);

            configurarPanelesGenerales(false, true, false, false, false);

            DataTable tablaInfoPrueba = _categoriaPruebas.ObtenerSelPruebasPorIdPrueba(Convert.ToInt32(ID_PRUEBA));

            if (tablaInfoPrueba.Rows.Count <= 0)
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = "ADVERTENCIA: no se encontró información de la prueba seleccionada." + _categoriaPruebas.MensajeError;

                configurarPanelesGenerales(false, false, false, false, false);
            }
            else
            {
                DataRow filaInfoPrueba = tablaInfoPrueba.Rows[0];

                Panel_PRUEBAS_CONFIGURADAS_ACTUALMENTE.Visible = false;

                cargar_datos_prueba(filaInfoPrueba);

                if (modificar == true)
                {
                    TextBox_NOM_PRUEBA.Enabled = true;
                    TextBox_Comentarios.Enabled = true;

                    Panel_FILEUPLOAD_ARCHIVO.Visible = true;

                    configurarBotonesPruebas(false, true, true);
                }
                else
                {
                    TextBox_NOM_PRUEBA.Enabled = false;
                    TextBox_Comentarios.Enabled = false;

                    Panel_FILEUPLOAD_ARCHIVO.Visible = false;

                    configurarBotonesPruebas(true, false, false);
                }

                configurarMensajes(false, System.Drawing.Color.Red);
                configurarMensajesPruebas(false, System.Drawing.Color.Red);

                configurarPanelesGenerales(false, true, true, false, true);
            }
        }
    }
    private void cargar_interfaz_nueva_prueba()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        int ID_CATEGORIA = Convert.ToInt32(QueryStringSeguro["categoria"]);
        Decimal ID_PRUEBA = Convert.ToDecimal(QueryStringSeguro["prueba"]);

        categoriaPruebas _categoriaPruebas = new categoriaPruebas(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaInfoCategoria = new DataTable();

        tablaInfoCategoria = _categoriaPruebas.ObtenerSelCatPruebasPorIdCat(ID_CATEGORIA);

        if (tablaInfoCategoria.Rows.Count <= 0)
        {
            if (_categoriaPruebas.MensajeError == null)
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = "ADVERETENCIA: No se encontró información sobre la categoría.";
            }
            else
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = _categoriaPruebas.MensajeError;
            }

            configurarPanelesGenerales(false, false, false, false, false);

            configurarBotonesCategorias(true, false, false, false);
        }
        else
        {
            configurarMensajes(false, System.Drawing.Color.Red);

            DataRow informacionCategoria = tablaInfoCategoria.Rows[0];

            Page.Header.Title = informacionCategoria["NOM_CATEGORIA"].ToString();

            configurarInfoAdicionalModulo(true, "Categoría: " + informacionCategoria["NOM_CATEGORIA"].ToString().Trim());

            cargar_datos_categoria(informacionCategoria);

            Panel_CONTROL_REGISTRO.Visible = true;
            Panel_ID_CATEGORIA.Visible = true;

            Panel_FORMULARIO_CATEGORIAS.Enabled = false;

            configurarBotonesCategorias(false, false, false, false);

            configurarPanelesGenerales(false, true, false, false, false);


            TextBox_ID_PRUEBA.Text = "";
            Panel_ID_PRUEBA.Visible = false;

            Panel_ARCHIVO_ACTUAL.Visible = false;

            configurarPanelesGenerales(false, true, true, false, true);

            configurarMensajes(false, System.Drawing.Color.Red);
            configurarMensajesPruebas(false, System.Drawing.Color.Red);

            Panel_PRUEBAS_CONFIGURADAS_ACTUALMENTE.Visible = false;

            configurarBotonesPruebas(false, true, true);
        }
    }
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = "CATEGORÍA DE PRUEBAS Y PRUEBAS";
       
        if (IsPostBack == false)
        {
            tools _tools = new tools();
            SecureQueryString QueryStringSeguro;
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

            String accion = QueryStringSeguro["accion"].ToString();

            if (accion == "inicial")
            {
                cargar_interfaz_inicial();
            }
            else
            {
                if (accion == "nuevaCategoria")
                {
                    cargar_interfaz_nueva_categoria();
                }
                else
                {
                    if (accion == "cargarCategoria")
                    {
                        cargar_interfaz_cargar_categoria(false);
                    }
                    else
                    {
                        if (accion == "modificarCategoria")
                        {
                            cargar_interfaz_cargar_categoria(true);
                        }
                        else
                        {
                            if (accion == "cargarNuevaCategoria")
                            {
                                cargar_interfaz_cargar_categoria(false);

                                configurarMensajes(true, System.Drawing.Color.Green);
                                Label_MENSAJE.Text = "La categoría fue creada correctamente y se le asignó el ID: " + QueryStringSeguro["categoria"] + ".";
                            }
                            else
                            {
                                if (accion == "cargarModificadaCategoria")
                                {
                                    cargar_interfaz_cargar_categoria(false);

                                    configurarMensajes(true, System.Drawing.Color.Green);
                                    Label_MENSAJE.Text = "La categoría con ID " + QueryStringSeguro["categoria"] + " fue modificada correctamente.";
                                }
                                else
                                {
                                    if (accion == "modificarPrueba")
                                    {
                                        cargar_interfaz_cargar_prueba(true);
                                    }
                                    else
                                    {
                                        if (accion == "nuevaPrueba")
                                        {
                                            cargar_interfaz_nueva_prueba();
                                        }
                                        else 
                                        {
                                            if (accion == "cargarNuevaPrueba")
                                            {
                                                cargar_interfaz_cargar_categoria(false);

                                                configurarMensajes(true, System.Drawing.Color.Green);
                                                Label_MENSAJE.Text = "La prueba con ID " + QueryStringSeguro["prueba"] + " fue modificada correctamente.";
                                            }
                                            else
                                            {
                                                if (accion == "cargarModificadaPrueba")
                                                {
                                                    cargar_interfaz_cargar_categoria(false);

                                                    configurarMensajes(true, System.Drawing.Color.Green);
                                                    Label_MENSAJE.Text = "La prueba con ID " + QueryStringSeguro["prueba"] + " fue modificada correctamente.";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }









        }
    }
    protected void Button_NUEVA_CATEGOORIA_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = "seleccion";
        QueryStringSeguro["nombre_area"] = "SELECCIÓN Y REQUISICIONES";
        QueryStringSeguro["nombre_modulo"] = "CATEGORÍA DE PRUEBAS";
        QueryStringSeguro["accion"] = "nuevaCategoria";

        Response.Redirect("~/seleccion/categoriasyPruebas.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }
    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;

        categoriaPruebas _cat = new categoriaPruebas(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaCat = _cat.ObtenerSelCatPruebasTodas();

        cargar_GridView_RESULTADOS_BUSQUEDA(tablaCat);
    }
    protected void GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged(object sender, EventArgs e)
    {
        String ID_CATEGORIA = GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["ID_CATEGORIA"].ToString();
        
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = "seleccion";
        QueryStringSeguro["nombre_area"] = "SELECCIÓN Y REQUISICIONES";
        QueryStringSeguro["nombre_modulo"] = "CATEGORÍA DE PRUEBAS";
        QueryStringSeguro["accion"] = "cargarCategoria";
        QueryStringSeguro["categoria"] = ID_CATEGORIA;

        Response.Redirect("~/seleccion/categoriasyPruebas.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }
    protected void Button_NUEVA_CATEGORIA_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = "seleccion";
        QueryStringSeguro["nombre_area"] = "SELECCIÓN Y REQUISICIONES";
        QueryStringSeguro["nombre_modulo"] = "CATEGORÍA DE PRUEBAS";
        QueryStringSeguro["accion"] = "nuevaCategoria";

        Response.Redirect("~/seleccion/categoriasyPruebas.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }
    protected void Button_MODIFICAR_CATEGORIA_Click(object sender, EventArgs e)
    {
        String ID_CATEGORIA = TextBox_ID_CATEGORIA.Text.Trim();

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = "seleccion";
        QueryStringSeguro["nombre_area"] = "SELECCIÓN Y REQUISICIONES";
        QueryStringSeguro["nombre_modulo"] = "CATEGORÍA DE PRUEBAS";
        QueryStringSeguro["accion"] = "modificarCategoria";
        QueryStringSeguro["categoria"] = ID_CATEGORIA;

        Response.Redirect("~/seleccion/categoriasyPruebas.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }
    protected void Button_GUARDAR_CATEGORIA_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String accion = QueryStringSeguro["accion"].ToString();

        categoriaPruebas _catPruebas = new categoriaPruebas(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
       
        Decimal ID_CATEGORIA = 0;

        String NOM_CATEGORIA = TextBox_NOM_CATEGORIA.Text.ToUpper().Trim();
        String OBSERVACIONES = TextBox_Observaciones.Text.ToUpper().Trim();

        String USU_CRE = Session["USU_LOG"].ToString();
        String USU_MOD = Session["USU_LOG"].ToString();

        Boolean verificador = true;

        if (String.IsNullOrEmpty(TextBox_ID_CATEGORIA.Text) == true)
        {
            ID_CATEGORIA = _catPruebas.AdicionarSelCatPruebas(NOM_CATEGORIA, OBSERVACIONES);

            if (ID_CATEGORIA == 0)
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = _catPruebas.MensajeError;
            }
            else
            {
                QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

                QueryStringSeguro["img_area"] = "seleccion";
                QueryStringSeguro["nombre_area"] = "SELECCIÓN Y REQUISICIONES";
                QueryStringSeguro["nombre_modulo"] = "CATEGORIA DE PRUEBAS Y PRUEBAS";
                QueryStringSeguro["accion"] = "cargarNuevaCategoria";
                QueryStringSeguro["categoria"] = ID_CATEGORIA.ToString();

                Response.Redirect("~/seleccion/categoriasyPruebas.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
            }
        }
        else
        {
            ID_CATEGORIA = Convert.ToDecimal(TextBox_ID_CATEGORIA.Text);

            verificador = _catPruebas.ActualizarSelCatPruebas(Convert.ToInt32(ID_CATEGORIA), NOM_CATEGORIA, OBSERVACIONES);

            if (verificador == false)
            {
                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = _catPruebas.MensajeError;
            }
            else
            {
                QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

                QueryStringSeguro["img_area"] = "seleccion";
                QueryStringSeguro["nombre_area"] = "SELECCIÓN Y REQUISICIONES";
                QueryStringSeguro["nombre_modulo"] = "CATEGORIAS DE PRUEBAS";
                QueryStringSeguro["accion"] = "cargarModificadaCategoria";
                QueryStringSeguro["categoria"] = ID_CATEGORIA.ToString();

                Response.Redirect("~/seleccion/categoriasyPruebas.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
            }
        }
    }
    protected void Button_CANCELAR_CATEGORIA_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = "seleccion";
        QueryStringSeguro["nombre_area"] = "SELECCIÓN Y REQUISICIONES";
        QueryStringSeguro["nombre_modulo"] = "CATEGORÍA DE PRUEBAS";
        QueryStringSeguro["accion"] = "inicial";

        Response.Redirect("~/seleccion/categoriasyPruebas.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }
    protected void Button_NNUEVA_PRUEBA_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        String ID_CATEGORIA = TextBox_ID_CATEGORIA.Text;

        QueryStringSeguro["img_area"] = "seleccion";
        QueryStringSeguro["nombre_area"] = "SELECCIÓN Y REQUISICIONES";
        QueryStringSeguro["nombre_modulo"] = "CATEGORÍA DE PRUEBAS";
        QueryStringSeguro["accion"] = "nuevaPrueba";
        QueryStringSeguro["categoria"] = ID_CATEGORIA;

        Response.Redirect("~/seleccion/categoriasyPruebas.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }
    protected void Button_MODIFICAR_PRUEBA_Click(object sender, EventArgs e)
    {

    }
    protected void Button_GUARDAR_PRUEBA_Click(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_CATEGORIA = Convert.ToDecimal(QueryStringSeguro["categoria"]);

        String NOM_PRUEBA = TextBox_NOM_PRUEBA.Text.Trim();
        String OBS_PRUEBA = TextBox_Comentarios.Text.Trim();

        Byte[] ARCHIVO = null;
        Int32 ARCHIVO_TAMANO = 0;
        String ARCHIVO_EXTENSION = null;
        String ARCHIVO_TYPE = null;
        if (FileUpload_ARCHIVO.HasFile == true)
        {
            using (BinaryReader reader = new BinaryReader(FileUpload_ARCHIVO.PostedFile.InputStream))
            {
                ARCHIVO = reader.ReadBytes(FileUpload_ARCHIVO.PostedFile.ContentLength);
                ARCHIVO_TAMANO = FileUpload_ARCHIVO.PostedFile.ContentLength;
                ARCHIVO_TYPE = FileUpload_ARCHIVO.PostedFile.ContentType;
                ARCHIVO_EXTENSION = _tools.obtenerExtensionArchivo(FileUpload_ARCHIVO.PostedFile.FileName);
            }
        }

        categoriaPruebas _pruebas = new categoriaPruebas(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Decimal ID_PRUEBA = 0;

        if (String.IsNullOrEmpty(TextBox_ID_PRUEBA.Text) == true)
        {
            ID_PRUEBA = _pruebas.AdicionarSelPruebas(NOM_PRUEBA, ID_CATEGORIA, OBS_PRUEBA, ARCHIVO, ARCHIVO_EXTENSION, ARCHIVO_TAMANO, ARCHIVO_TYPE);

            if (_pruebas.MensajeError != null)
            {
                configurarInfoAdicionalModulo(false, "");

                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = _pruebas.MensajeError;
            }
            else
            {
                QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

                QueryStringSeguro["img_area"] = "seleccion";
                QueryStringSeguro["nombre_area"] = "SELECCIÓN Y REQUISICIONES";
                QueryStringSeguro["nombre_modulo"] = "CATEGORÍA DE PRUEBAS Y PRUEBAS";
                QueryStringSeguro["accion"] = "cargarNuevaPrueba";
                QueryStringSeguro["categoria"] = ID_CATEGORIA.ToString();
                QueryStringSeguro["prueba"] = ID_PRUEBA.ToString();

                Response.Redirect("~/seleccion/categoriasyPruebas.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
            }
        }
        else
        {
            Boolean verificador = true;

            ID_PRUEBA = Convert.ToDecimal(TextBox_ID_PRUEBA.Text);

            verificador = _pruebas.ActualizarSelPrueba(ID_PRUEBA, NOM_PRUEBA, OBS_PRUEBA, ARCHIVO, ARCHIVO_EXTENSION, ARCHIVO_TAMANO, ARCHIVO_TYPE);

            if (verificador == false)
            {
                configurarInfoAdicionalModulo(false, "");

                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = _pruebas.MensajeError;
            }
            else
            {
                QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

                QueryStringSeguro["img_area"] = "seleccion";
                QueryStringSeguro["nombre_area"] = "SELECCIÓN Y REQUISICIONES";
                QueryStringSeguro["nombre_modulo"] = "CATEGORÍA DE PRUEBAS Y PRUEBAS";
                QueryStringSeguro["accion"] = "cargarModificadaPrueba";
                QueryStringSeguro["categoria"] = ID_CATEGORIA.ToString();
                QueryStringSeguro["prueba"] = ID_PRUEBA.ToString();

                Response.Redirect("~/seleccion/categoriasyPruebas.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
            }
        }
    }
    protected void Button_CANCELAR_PRUEBA_Click(object sender, EventArgs e)
    {

    }
    protected void GridView_GRILLA_PRUEBAS_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"].ToString());

        int filaSeleccionada = Convert.ToInt32(e.CommandArgument);

        Decimal ID_CATEGORIA = Convert.ToDecimal(GridView_GRILLA_PRUEBAS.DataKeys[filaSeleccionada].Values["ID_CATEGORIA"]);
        Decimal ID_PRUEBA = Convert.ToDecimal(GridView_GRILLA_PRUEBAS.DataKeys[filaSeleccionada].Values["ID_PRUEBA"]);

        if (e.CommandName == "modificar")
        {
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
            QueryStringSeguro["img_area"] = "seleccion";
            QueryStringSeguro["nombre_area"] = "SELECCIÓN Y REQUISICIONES";
            QueryStringSeguro["nombre_modulo"] = "CATEGORÍAS Y PRUEBAS";
            QueryStringSeguro["accion"] = "modificarPrueba";
            QueryStringSeguro["categoria"] = ID_CATEGORIA.ToString();
            QueryStringSeguro["prueba"] = ID_PRUEBA.ToString();

            Response.Redirect("~/seleccion/categoriasyPruebas.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
        }
    }
}