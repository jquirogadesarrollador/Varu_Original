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
using AjaxControlToolkit;
using Brainsbits.LLB.seguridad;

public partial class _ProgramaGeneral : System.Web.UI.Page
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

    private Int32 numActividadesEnArbol = 0;

    private enum Acciones
    {
        Inicio = 0,
        Nuevo,
        SubProgramaYActividad,
        Subprograma,
        AccionesSubprogramaActividad, 
        Actividad,
        Cargar,
        Modificar
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }

    private enum TiposNodo
    { 
        PROGRAMA = 1,
        ACTIVIDAD,
        SUBPROGRAMA
    }

    private enum Listas
    {
        SubProgramas = 0,
        Actividades,
        EstadosSubProgramas,
        EstadosActividades,
        TiposActividad,
        SectoresActividad
    }

    private void configurar_paneles_popup(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_mensaje.CssClass = "panel_popup";
    }

    private void configurar_paneles_popup_esquema_programa(Panel panel_fondo, Panel panel_contenido)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_contenido.CssClass = "panel_popup_esquema_prog";
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

    private void MostrarFormularioCreacionSubprogramaActividad(Panel panel_fondo, Panel panel_contenido)
    {
        panel_fondo.Style.Add("display", "block");
        panel_contenido.Style.Add("display", "block");

        panel_fondo.Visible = true;
        panel_contenido.Visible = true;
    }

    private void MostrarFormularioConfirmacionEliminacionActividadSubprograma(Panel panel_fondo, Panel panel_contenido)
    {
        panel_fondo.Style.Add("display", "block");
        panel_contenido.Style.Add("display", "block");

        panel_fondo.Visible = true;
        panel_contenido.Visible = true;
    }

    private void Configurar()
    {
        configurar_paneles_popup(Panel_FONDO_MENSAJE, Panel_MENSAJES);

        configurar_paneles_popup(Panel_FONDO_MENSAJE_ARBOL, Panel_MENSAJES_ARBOL);

        configurar_paneles_popup_esquema_programa(Panel_FONDO_NUEVO_SUBPROGAMA, Panel_CONTENIDO_NUEVO_SUBPROGRAMA);
        configurar_paneles_popup_esquema_programa(Panel_FONDO_ACTIVIDAD, Panel_CONTENIDO_ACTIVIDAD);

        configurar_paneles_popup_esquema_programa(Panel_FONDO_CONFIRMACION_ELIMINACION, Panel_CONTENIDO_CONFIRMACION_ELIMINACION);

        CKEditor_PrimeraParte.config.toolbar = new object[]
        {
            new object[] { "Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript"},
            new object[] { "NumberedList", "BulletedList", "-", "Outdent", "Indent", "Blockquote" },
            new object[] { "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock" },
            new object[] { "BidiLtr", "BidiRtl", "-", "Undo", "Redo", "-", "SelectAll" },
            "/",
            new object[] { "Styles", "Format", "Font", "FontSize" },
            new object[] { "TextColor", "BGColor" },
            new object[] { "ShowBlocks" },
            new object[] { "Source"}
        };

        CKEditorControl_ParteFinal.config.toolbar = new object[]
        {
            new object[] { "Bold", "Italic", "Underline", "Strike", "-", "Subscript", "Superscript"},
            new object[] { "NumberedList", "BulletedList", "-", "Outdent", "Indent", "Blockquote" },
            new object[] { "JustifyLeft", "JustifyCenter", "JustifyRight", "JustifyBlock" },
            new object[] { "BidiLtr", "BidiRtl", "-", "Undo", "Redo", "-", "SelectAll" },
            "/",
            new object[] { "Styles", "Format", "Font", "FontSize" },
            new object[] { "TextColor", "BGColor" },
            new object[] { "ShowBlocks" },
            new object[] { "Source"}
        };
    }

    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = false;

                Panel_FORM_BOTONES_ENCABEZADO.Visible = false;
                    
                Panel_RESULTADOS_GRID.Visible = false;
                Button_NUEVO.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_IMPRIMIR.Visible = false;

                PanelNombrePrograma.Visible = false;

                Panel_ImagenPrograma.Visible = false;
                Panel_SubirArchivoImagenPrograma.Visible = false;

                Panel_CabeceraHtml.Visible = false;
                
                Panel_ArbolPrograma.Visible = false;

                Panel_ParteFinalPrograma.Visible = false;

                Panel_BOTONES_ACCION_PIE.Visible = false;
                Button_NUEVO_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                Button_IMPRIMIR_1.Visible = false;
                break;
            case Acciones.Modificar:
                Button_NUEVO.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_IMPRIMIR.Visible = false;

                Button_NUEVO_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                Button_IMPRIMIR_1.Visible = false;
                break;
        }
    }

    private void Desactivar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:

                TextBox_NombrePrograma.Enabled = false;

                CKEditor_PrimeraParte.Enabled = false;

                DropDownList_IdSubPrograma.Enabled = false;
                DropDownList_EstadoSubPrograma.Enabled = false;
                TextBox_DescripcionSubPrograma.Enabled = false;

                DropDownList_IdActividad.Enabled = false;
                DropDownList_Tipo.Enabled = false;
                DropDownList_Sector.Enabled = false;
                DropDownList_EstadoActividad.Enabled = false;
                TextBox_DescripcionActividad.Enabled = false;

                CKEditorControl_ParteFinal.Enabled = false;

                break;
            case Acciones.Subprograma:
                DropDownList_EstadoSubPrograma.Enabled = false;
                TextBox_DescripcionSubPrograma.Enabled = false;
                break;
            case Acciones.Actividad:
                DropDownList_Tipo.Enabled = false;
                DropDownList_Sector.Enabled = false;
                DropDownList_EstadoActividad.Enabled = false;
                TextBox_DescripcionActividad.Enabled = false;
                break;
        }
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Button_NUEVO.Visible = true;

                Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.Nuevo:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                PanelNombrePrograma.Visible = true;

                Panel_ImagenPrograma.Visible = true;
                Panel_SubirArchivoImagenPrograma.Visible = true;

                Panel_CabeceraHtml.Visible = true;

                Panel_ArbolPrograma.Visible = true;
                
                Panel_ParteFinalPrograma.Visible = true;

                Panel_BOTONES_ACCION_PIE.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
                break;
            case Acciones.Cargar:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                if (HiddenField_ANNO.Value == DateTime.Now.Year.ToString())
                {
                    Button_MODIFICAR.Visible = true;
                }
                Button_IMPRIMIR.Visible = true;

                Panel_CabeceraHtml.Visible = true;

                PanelNombrePrograma.Visible = true;

                Panel_ImagenPrograma.Visible = true;

                Panel_ArbolPrograma.Visible = true;

                Panel_ParteFinalPrograma.Visible = true;

                Panel_BOTONES_ACCION_PIE.Visible = true;
                if (HiddenField_ANNO.Value == DateTime.Now.Year.ToString())
                {
                    Button_MODIFICAR_1.Visible = true;
                }
                Button_IMPRIMIR_1.Visible = true;
                break;
            case Acciones.Modificar:
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_SubirArchivoImagenPrograma.Visible = true;

                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
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

    private void cargar_GridView_RESULTADOS_BUSQUEDA()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Programa.Areas AREA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);

        Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaProgramasGen = _programa.ObtenerProgramasGeneralesPorArea(AREA);

        if (tablaProgramasGen.Rows.Count <= 0)
        {
            if (_programa.MensajeError == null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron registros de Programas Generales.", Proceso.Advertencia);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _programa.MensajeError, Proceso.Error);
            }

            Panel_RESULTADOS_GRID.Visible = false;
        }
        else
        {
            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaProgramasGen;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
        }
    }

    private void Cargar(Listas lista, DropDownList drop)
    {
        switch (lista)
        {
            case Listas.SubProgramas:
                SubPrograma _sub = new SubPrograma(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                Programa.Areas AREA_SUBPROGRAMA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);
                DataTable tablaSubProgramas = _sub.ObtenerSubProgramasPorArea(AREA_SUBPROGRAMA);

                drop.Items.Clear();
                drop.Items.Add(new ListItem("Seleccione...", ""));

                foreach (DataRow fila in tablaSubProgramas.Rows)
                {
                    if (fila["ACTIVO"].ToString().Trim() == "True")
                    {
                        drop.Items.Add(new ListItem(fila["NOMBRE"].ToString().Trim(), fila["ID_SUB_PROGRAMA"].ToString().Trim()));
                    }
                }

                drop.DataBind();
                break;
            case Listas.Actividades:

                ActividadRseGlobal _act = new ActividadRseGlobal(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

                Programa.Areas AREA_ACTIVIDAD = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);
                DataTable tablaActividad = _act.ObtenerActividadesPorArea(AREA_ACTIVIDAD);

                drop.Items.Clear();
                drop.Items.Add(new ListItem("Seleccione...", ""));

                foreach (DataRow fila in tablaActividad.Rows)
                {
                    if (fila["ACTIVO"].ToString().Trim() == "True")
                    {
                        drop.Items.Add(new ListItem(fila["NOMBRE"].ToString().Trim(), fila["ID_ACTIVIDAD"].ToString().Trim()));
                    }
                }

                drop.DataBind();
                break;
            case Listas.EstadosSubProgramas:
                drop.Items.Clear();
                parametro _parametroSP = new parametro(Session["idEmpresa"].ToString());
                DataTable tablaParametrosSP = _parametroSP.ObtenerParametrosPorTabla(tabla.PARAMETROS_ESTADO_SUB_PROGRAMA);
                ListItem itemSP = new ListItem("Seleccione...", "");
                drop.Items.Add(itemSP);
                foreach (DataRow fila in tablaParametrosSP.Rows)
                {
                    itemSP = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
                    drop.Items.Add(itemSP);
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

                Programa.Areas AREA_TIPO_ACTIVIDAD = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);

                drop.Items.Clear();
                TipoActividad _tipoActividad = new TipoActividad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                DataTable tablaParametrosTA = _tipoActividad.ObtenerTiposActividadPorAreayEstado(AREA_TIPO_ACTIVIDAD, true);
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
        }
    }

    private void DeterminarSiSeDebeOcultarBotonNuevo()
    {
        Int32 annoActual = DateTime.Now.Year;

        Int32 ultimoAnnoGrilla = 0;

        if (GridView_RESULTADOS_BUSQUEDA.Rows.Count > 0)
        {
            ultimoAnnoGrilla = Convert.ToInt32(GridView_RESULTADOS_BUSQUEDA.DataKeys[0].Values["ANNO"]);
        }

        if (ultimoAnnoGrilla < annoActual)
        {
            Button_NUEVO.Visible = true;
            Button_NUEVO_1.Visible = true;
        }
        else
        {
            Button_NUEVO.Visible = false;
            Button_NUEVO_1.Visible = false;
        }
    }

    private Decimal CargarNuevoPrograma()
    {
        Int32 annoActual = DateTime.Now.Year;
        HiddenField_ANNO.Value = annoActual.ToString();

        Programa.Areas idArea = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);

        Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Decimal ID_PROGRAMA_GENERAL = _programa.AdicionarRegistromaestraProgramaGeneral(null, null, null, annoActual, idArea, null);

        if (ID_PROGRAMA_GENERAL <= 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _programa.MensajeError, Proceso.Error);

            ID_PROGRAMA_GENERAL = 0;
        }

        return ID_PROGRAMA_GENERAL;
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                ConfigurarAreaRseGlobal();

                cargar_GridView_RESULTADOS_BUSQUEDA();

                DeterminarSiSeDebeOcultarBotonNuevo();
                break;
            case Acciones.Nuevo:

                HiddenField_ANNO.Value = DateTime.Now.Year.ToString();

                GridView_EsquemaPrograma.DataSource = null;
                GridView_EsquemaPrograma.DataBind();

                Label_TituloProgramaGeneral.Text = "Actividades que comprenden el Programa General para el año " + HiddenField_ANNO.Value;
                
                HiddenField_TIPO_NODO_SELECCIONADO.Value = TiposNodo.PROGRAMA.ToString();
                HiddenField_ID_NODO_SELECCIONADO.Value = "0";

                CKEditorControl_ParteFinal.Text = "";
                CKEditorControl_ParteFinal.Text = "";

                HiddenField_ImagenPrograma.Value = "";

                break;
            case Acciones.Subprograma:
                Cargar(Listas.SubProgramas, DropDownList_IdSubPrograma);
                Cargar(Listas.EstadosSubProgramas, DropDownList_EstadoSubPrograma);
                break;
            case Acciones.Actividad:
                Cargar(Listas.Actividades, DropDownList_IdActividad);
                Cargar(Listas.TiposActividad, DropDownList_Tipo);
                Cargar(Listas.SectoresActividad, DropDownList_Sector);
                Cargar(Listas.EstadosActividades, DropDownList_EstadoActividad);
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

    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;

        cargar_GridView_RESULTADOS_BUSQUEDA();
    }

    private void DibujarImagenPrograma()
    {
        System.Drawing.Image imgPhoto = System.Drawing.Image.FromFile(MapPath(HiddenField_ImagenPrograma.Value));

        decimal ancho = imgPhoto.Size.Width;
        decimal alto = imgPhoto.Size.Height;

        if (ancho > 500)
        {
            
            alto = (alto * (((500 * 100) / ancho) / 100));
            ancho = 500;
        }

        if (alto > 500)
        {
            ancho = (ancho * (((500 * 100) / alto) / 100));
            alto = 500;
        }

        Image_Programa.ImageUrl = HiddenField_ImagenPrograma.Value;
        Image_Programa.Width = new Unit(Convert.ToInt32(ancho));
        Image_Programa.Height = new Unit(Convert.ToInt32(alto));

    }

    private DataTable ObtenerEstructuraTablaPrograma()
    {
        DataTable tablaResultado = new DataTable();

        tablaResultado.Columns.Add("INDEX", typeof(Int32));
        tablaResultado.Columns.Add("ID_DETALLE_GENERAL", typeof(decimal));
        tablaResultado.Columns.Add("ID_PROGRAMA_GENERAL", typeof(decimal));
        tablaResultado.Columns.Add("TIPO", typeof(string));
        tablaResultado.Columns.Add("ID_DETALLE_GENERAL_PADRE", typeof(decimal));
        tablaResultado.Columns.Add("ID_SUBPROGRAMA", typeof(decimal));
        tablaResultado.Columns.Add("ID_ACTIVIDAD", typeof(decimal));
        tablaResultado.Columns.Add("DESCRIPCION", typeof(string));
        tablaResultado.Columns.Add("URL_ICON", typeof(string));
        tablaResultado.Columns.Add("NOMBRE", typeof(string));
        tablaResultado.Columns.Add("NUMERACION", typeof(string));

        return tablaResultado;

    }

    private void Cargar_GridView_EsquemaPrograma_DesdeTabla(DataTable tablaPrograma)
    {
        GridView_EsquemaPrograma.DataSource = tablaPrograma;
        GridView_EsquemaPrograma.DataBind();

        for (int i = 0; i < GridView_EsquemaPrograma.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_EsquemaPrograma.Rows[i];
            DataRow filaTabla = tablaPrograma.Rows[i];


            Label textoNumeracion = filaGrilla.FindControl("Label_numeracionPrograma") as Label;
            textoNumeracion.Text = filaTabla["NUMERACION"].ToString();

            Label textoNombre = filaGrilla.FindControl("Label_NombreSubprogramaActividad") as Label;
            textoNombre.Text = filaTabla["NOMBRE"].ToString();

            Label textoDescripcion = filaGrilla.FindControl("Label_DescripcionProgramaActividad") as Label;
            textoDescripcion.Text = filaTabla["DESCRIPCION"].ToString();

            Image imagen = filaGrilla.FindControl("Image_ProgramaActividad") as Image;
            imagen.ImageUrl = filaTabla["URL_ICON"].ToString();
        }

    }

    private void Cargar(Decimal ID_PROGRAMA_GENERAL, Int32 ANNO, String ID_AREA)
    {
        HiddenField_ID_PROGRAMA_GENERAL.Value = ID_PROGRAMA_GENERAL.ToString();
        HiddenField_ANNO.Value = ANNO.ToString();

        Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaPrograma = _programa.ObtenerPrograGeneralMaestraPorId(ID_PROGRAMA_GENERAL);

        if (tablaPrograma.Rows.Count <= 0)
        {
            if (_programa.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _programa.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información del ProgramaGeneral Seleccionado.", Proceso.Advertencia);
            }
        }
        else
        {
            DataRow filaPrograma = tablaPrograma.Rows[0];

            TextBox_NombrePrograma.Text = filaPrograma["TITULO"].ToString().Trim();
            CKEditor_PrimeraParte.Text = filaPrograma["TEXTO_CABECERA"].ToString().Trim();
            CKEditorControl_ParteFinal.Text = filaPrograma["TEXTO_FINAL"].ToString().Trim();

            DataTable tablaSubprogramasActividades = ObtenerEstructuraTablaPrograma();

            Cargar(tablaSubprogramasActividades);
            Cargar_GridView_EsquemaPrograma_DesdeTabla(tablaSubprogramasActividades);

            if (DBNull.Value.Equals(filaPrograma["DIR_IMAGEN"]) == true)
            {
                HiddenField_ImagenPrograma.Value = "";
                Image_Programa.ImageUrl = "";
            }
            else
            {
                HiddenField_ImagenPrograma.Value = filaPrograma["DIR_IMAGEN"].ToString().Trim();

                DibujarImagenPrograma();
            }
        }
    }

    private void ConfigurarBotonesAccionSobreEsquemaPrograma(Boolean bEliminar, Boolean bNuevoPrograma, Boolean bnuevaActividad)
    {
        ImageButton_AdicionarSubprograma.Visible = bNuevoPrograma;
        ImageButton_AdicionarActividad.Visible = bnuevaActividad;

        GridView_EsquemaPrograma.Columns[0].Visible = bEliminar;
        GridView_EsquemaPrograma.Columns[1].Visible = bNuevoPrograma;
        GridView_EsquemaPrograma.Columns[2].Visible = bnuevaActividad;
    }

    protected void GridView_RESULTADOS_BUSQUEDA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "seleccionar")
        {
            Decimal ID_PROGRAMA_GENERAL = Convert.ToDecimal(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_PROGRAMA_GENERAL"]); ;
            Int32 ANNO = Convert.ToInt32(GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ANNO"]);
            String ID_AREA = GridView_RESULTADOS_BUSQUEDA.DataKeys[indexSeleccionado].Values["ID_AREA"].ToString().Trim();

            HiddenField_ID_PROGRAMA_GENERAL.Value = ID_PROGRAMA_GENERAL.ToString();
            HiddenField_ANNO.Value = ANNO.ToString();

            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.Cargar);
            Desactivar(Acciones.Inicio);
            Limpiar(Acciones.Cargar);

            Cargar(ID_PROGRAMA_GENERAL, ANNO, ID_AREA);

            ConfigurarBotonesAccionSobreEsquemaPrograma(false, false, false);
        }
    }

    protected void ajaxFileUpload_OnUploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
    {
        string filePath = "~/imagenes/programasGenerales/" + e.FileName;

        var ajaxFileUpload = (AjaxFileUpload)sender;
        ajaxFileUpload.SaveAs(MapPath(filePath));

        e.PostedUrl = Page.ResolveUrl(filePath);
    }

    private void CargarSubProgramaSeleccionado()
    {
        Decimal ID_DETALLE_GENERAL = Convert.ToDecimal(HiddenField_ID_NODO_SELECCIONADO.Value);

        Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfo = _programa.ObtenerDetalleProgramaGeneralPorIdDetalle(ID_DETALLE_GENERAL);

        DataRow filaInfo = tablaInfo.Rows[0];

        Cargar(Listas.SubProgramas, DropDownList_IdSubPrograma);
        DropDownList_IdSubPrograma.SelectedValue = filaInfo["ID_SUBPROGRAMA"].ToString().Trim();

        Cargar(Listas.EstadosSubProgramas, DropDownList_EstadoSubPrograma);
        DropDownList_EstadoSubPrograma.SelectedValue = filaInfo["ACTIVO_SUB_PROGRAMA"].ToString().Trim();

        TextBox_DescripcionSubPrograma.Text = filaInfo["DESCRIPCION_SUB_PROGRAMA"].ToString().Trim();
    }

    private void CargarActividadSeleccionada()
    {
        Decimal ID_DETALLE_GENERAL = Convert.ToDecimal(HiddenField_ID_NODO_SELECCIONADO.Value);

        Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfo = _programa.ObtenerDetalleProgramaGeneralPorIdDetalle(ID_DETALLE_GENERAL);

        DataRow filaInfo = tablaInfo.Rows[0];

        Cargar(Listas.Actividades, DropDownList_IdActividad);
        DropDownList_IdActividad.SelectedValue = filaInfo["ID_ACTIVIDAD"].ToString().Trim();

        Cargar(Listas.TiposActividad, DropDownList_Tipo);
        DropDownList_Tipo.SelectedValue = filaInfo["TIPO_ACTIVIDAD"].ToString().Trim();

        Cargar(Listas.SectoresActividad, DropDownList_Sector);
        DropDownList_Sector.SelectedValue = filaInfo["SECTOR_ACTIVIDAD"].ToString().Trim();

        Cargar(Listas.EstadosActividades, DropDownList_EstadoActividad);
        DropDownList_EstadoActividad.SelectedValue = filaInfo["ACTIVO_ACTIVIDAD"].ToString().Trim();

        TextBox_DescripcionActividad.Text = filaInfo["DESCRIPCION_ACTIVIDAD"].ToString().Trim();
    }

    private void Activar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Nuevo:
                TextBox_NombrePrograma.Enabled = true;
                CKEditor_PrimeraParte.Enabled = true;
                CKEditorControl_ParteFinal.Enabled = true;
                break;
            case Acciones.Subprograma:
                DropDownList_IdSubPrograma.Enabled = true;
                break;
            case Acciones.Actividad:
                DropDownList_IdActividad.Enabled = true;
                break;
            case Acciones.Modificar:
                TextBox_NombrePrograma.Enabled = true;
                CKEditor_PrimeraParte.Enabled = true;
                CKEditorControl_ParteFinal.Enabled = true;

                break;
        }
    }

    private void Limpiar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Nuevo:
                TextBox_NombrePrograma.Text = ""; 
                CKEditor_PrimeraParte.Text = "";
                CKEditorControl_ParteFinal.Text = "";
                break;
            case Acciones.Subprograma:
                DropDownList_IdSubPrograma.Items.Clear();
                DropDownList_EstadoSubPrograma.Items.Clear();
                TextBox_DescripcionSubPrograma.Text = "";
                break;
            case Acciones.Actividad:
                DropDownList_IdActividad.Items.Clear();
                DropDownList_Tipo.Items.Clear();
                DropDownList_Sector.Items.Clear();
                DropDownList_EstadoActividad.Items.Clear();
                TextBox_DescripcionActividad.Text = "";
                break;
            case Acciones.Cargar:
                TextBox_NombrePrograma.Text = "";
                CKEditor_PrimeraParte.Text = "";
                CKEditorControl_ParteFinal.Text = "";
                break;
        }
    }

    private void NuevoProgramaGeneral()
    {
        Decimal ID_PROGRAMA_GENERAL = CargarNuevoPrograma();

        if (ID_PROGRAMA_GENERAL > 0)
        {
            HiddenField_ID_PROGRAMA_GENERAL.Value = ID_PROGRAMA_GENERAL.ToString();

            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.Nuevo);
            Desactivar(Acciones.Inicio);
            Activar(Acciones.Nuevo);
            Limpiar(Acciones.Nuevo);
            Cargar(Acciones.Nuevo);
        }
    }
    protected void Button_NUEVO_Click(object sender, EventArgs e)
    {
        NuevoProgramaGeneral();
    }
    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Modificar);
        Mostrar(Acciones.Modificar);
        Activar(Acciones.Modificar);

        ConfigurarBotonesAccionSobreEsquemaPrograma(true, true, true);
    }

    private void ContarActividadesEnArbol()
    {
        numActividadesEnArbol = 0;

        if (GridView_EsquemaPrograma.Rows.Count <= 0)
        {
            numActividadesEnArbol = 0;
        }
        else
        {
            for (int i = 0; i < GridView_EsquemaPrograma.Rows.Count; i++)
            {
                if (GridView_EsquemaPrograma.DataKeys[i].Values["TIPO"].ToString() == TiposNodo.ACTIVIDAD.ToString())
                {
                    numActividadesEnArbol += 1;
                }
            }
        }
    }

    private void Actualizar()
    {
        Decimal ID_PROGRAMA_GENERAL = Convert.ToDecimal(HiddenField_ID_PROGRAMA_GENERAL.Value);
        String NOMBRE_PROGRAMA = TextBox_NombrePrograma.Text.Trim();

        String HTML_PRIMERA_PARTE = CKEditor_PrimeraParte.Text;
        String HTML_ULTIMA_PARTE = CKEditorControl_ParteFinal.Text;

        Boolean correcto = true;

        ContarActividadesEnArbol();

        String DIRECCION_IMAGEN_PROGRAMA = "";
        if (numActividadesEnArbol <= 0)
        {
            correcto = false;
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Para poder guardar el Programa General, debe incluir Subprogramas y Actividades en la Sección ESQUEMA DEL PROGRAMA.", Proceso.Advertencia);
        }
        else
        { 
            if (HiddenField_ImagenPrograma.Value == "")
            {
                if (FileUpload_ImagenPrograma.HasFile == false)
                {
                    correcto = false;
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe especificar una imagen para el Programa.", Proceso.Advertencia);
                }
                else
                {
                    string filePath = "~/imagenes/programasGenerales/" + FileUpload_ImagenPrograma.FileName;

                    FileUpload_ImagenPrograma.SaveAs(MapPath(filePath));

                    DIRECCION_IMAGEN_PROGRAMA = filePath;
                }
            }
            else
            {
                if (FileUpload_ImagenPrograma.HasFile == true)
                {
                    string filePath = "~/imagenes/programasGenerales/" + FileUpload_ImagenPrograma.FileName;

                    FileUpload_ImagenPrograma.SaveAs(MapPath(filePath));

                    DIRECCION_IMAGEN_PROGRAMA = filePath;
                }
                else
                {
                    DIRECCION_IMAGEN_PROGRAMA = HiddenField_ImagenPrograma.Value;  
                }   
            }


            if (correcto == true)
            {
                Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

                Boolean verificado = _programa.ActualizarProgramaGeneral(ID_PROGRAMA_GENERAL, NOMBRE_PROGRAMA, HTML_PRIMERA_PARTE, HTML_ULTIMA_PARTE, DIRECCION_IMAGEN_PROGRAMA);

                if (verificado == false)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _programa.MensajeError, Proceso.Error);
                }
                else
                {
                    Ocultar(Acciones.Inicio);
                    Mostrar(Acciones.Cargar);
                    Desactivar(Acciones.Inicio);
                    Limpiar(Acciones.Cargar);

                    Int32 ANNO = Convert.ToInt32(HiddenField_ANNO.Value);

                    Cargar(ID_PROGRAMA_GENERAL, ANNO, HiddenField_ID_AREA.Value);

                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El Programa General: " + NOMBRE_PROGRAMA + " fue actualizado correctamente.", Proceso.Correcto);
                }
            }
        }   
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        Actualizar();
    }
    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }

    protected void DropDownList_IdSubPrograma_SelectedIndexChanged(object sender, EventArgs e)
    {
        Programa.Areas AREA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);

        if (DropDownList_IdSubPrograma.SelectedIndex <= 0)
        {
            DropDownList_EstadoSubPrograma.SelectedIndex = 0;
            TextBox_DescripcionSubPrograma.Text = "";
        }
        else
        {
            SubPrograma _sub = new SubPrograma(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaSub = _sub.ObtenerSubProgramasPorId(Convert.ToDecimal(DropDownList_IdSubPrograma.SelectedValue), AREA);

            DataRow filaSub = tablaSub.Rows[0];

            DropDownList_EstadoSubPrograma.SelectedValue = filaSub["ACTIVO"].ToString().Trim();

            TextBox_DescripcionSubPrograma.Text = filaSub["DESCRIPCION"].ToString().Trim();
        }
    }

    private void CargarSubProgramasYActividades(Int32 index, Decimal ID_DETALLE_GENERAL_PADRE, DataTable tablaPrograma, String numeracion)
    {
        Int32 contadorNumeracionInterna = 1;

        Decimal ID_PROGRAMA_GENERAL = Convert.ToDecimal(HiddenField_ID_PROGRAMA_GENERAL.Value);

        Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaDetallesProgramaGeneral = _programa.ObtenerDetallesProgramaGeneralPorIdPadre(ID_PROGRAMA_GENERAL, ID_DETALLE_GENERAL_PADRE);

        foreach (DataRow fila in tablaDetallesProgramaGeneral.Rows)
        {
            if (fila["TIPO"].ToString().Trim() == TiposNodo.SUBPROGRAMA.ToString())
            {
                DataRow filaSubprogramaActividad = tablaPrograma.NewRow();


                filaSubprogramaActividad["INDEX"] = index;
                index += 1;

                filaSubprogramaActividad["ID_DETALLE_GENERAL"] = fila["ID_DETALLE_GENERAL"];
                filaSubprogramaActividad["ID_PROGRAMA_GENERAL"] = ID_PROGRAMA_GENERAL;
                filaSubprogramaActividad["TIPO"] = fila["TIPO"];
                filaSubprogramaActividad["ID_DETALLE_GENERAL_PADRE"] = fila["ID_DETALLE_GENERAL_PADRE"];

                if (DBNull.Value.Equals(fila["ID_SUBPROGRAMA"]) == true)
                {
                    filaSubprogramaActividad["ID_SUBPROGRAMA"] = 0;
                }
                else
                {
                    filaSubprogramaActividad["ID_SUBPROGRAMA"] = fila["ID_SUBPROGRAMA"];
                }

                if (DBNull.Value.Equals(fila["ID_ACTIVIDAD"]) == true)
                {
                    filaSubprogramaActividad["ID_ACTIVIDAD"] = 0;
                }
                else
                {
                    filaSubprogramaActividad["ID_ACTIVIDAD"] = fila["ID_ACTIVIDAD"];
                }
               
                filaSubprogramaActividad["DESCRIPCION"] = fila["DESCRIPCION_SUB_PROGRAMA"].ToString().Trim() + ".";
                filaSubprogramaActividad["URL_ICON"] = urlImgPrograma;
                filaSubprogramaActividad["NOMBRE"] = fila["NOMBRE_SUB_PROGRAMA"];
                
                filaSubprogramaActividad["NUMERACION"] = numeracion + contadorNumeracionInterna + ".";

                tablaPrograma.Rows.Add(filaSubprogramaActividad);

                CargarSubProgramasYActividades(index, Convert.ToDecimal(fila["ID_DETALLE_GENERAL"]), tablaPrograma, numeracion + contadorNumeracionInterna + ".");
                contadorNumeracionInterna += 1;
            }
        }

        foreach (DataRow fila in tablaDetallesProgramaGeneral.Rows)
        {
            if (fila["TIPO"].ToString().Trim() == TiposNodo.ACTIVIDAD.ToString())
            {
                DataRow filaSubprogramaActividad = tablaPrograma.NewRow();


                filaSubprogramaActividad["INDEX"] = index;
                index += 1;

                filaSubprogramaActividad["ID_DETALLE_GENERAL"] = fila["ID_DETALLE_GENERAL"];
                filaSubprogramaActividad["ID_PROGRAMA_GENERAL"] = ID_PROGRAMA_GENERAL;
                filaSubprogramaActividad["TIPO"] = fila["TIPO"];
                filaSubprogramaActividad["ID_DETALLE_GENERAL_PADRE"] = fila["ID_DETALLE_GENERAL_PADRE"];

                if (DBNull.Value.Equals(fila["ID_SUBPROGRAMA"]) == true)
                {
                    filaSubprogramaActividad["ID_SUBPROGRAMA"] = 0;
                }
                else
                {
                    filaSubprogramaActividad["ID_SUBPROGRAMA"] = fila["ID_SUBPROGRAMA"];
                }

                if (DBNull.Value.Equals(fila["ID_ACTIVIDAD"]) == true)
                {
                    filaSubprogramaActividad["ID_ACTIVIDAD"] = 0;
                }
                else
                {
                    filaSubprogramaActividad["ID_ACTIVIDAD"] = fila["ID_ACTIVIDAD"];
                }

                filaSubprogramaActividad["DESCRIPCION"] = fila["TIPO_ACTIVIDAD"].ToString().Trim() + ": " + fila["DESCRIPCION_ACTIVIDAD"].ToString().Trim() + ".<BR>SECTOR: " + fila["SECTOR_ACTIVIDAD"].ToString().Trim() + ".";
                filaSubprogramaActividad["URL_ICON"] = urlImgActividad;
                filaSubprogramaActividad["NOMBRE"] = fila["NOMBRE_ACTIVIDAD"];

                filaSubprogramaActividad["NUMERACION"] = numeracion + contadorNumeracionInterna + ".";
                contadorNumeracionInterna += 1;

                tablaPrograma.Rows.Add(filaSubprogramaActividad);
            }
        }
    }

    private void CargarArbolDePrograma(DataTable tablaPrograma)
    {
        Int32 contador = 0;
        Int32 numeracion = 1;

        Label_TituloProgramaGeneral.Text = "Actividades que comprenden el Programa General para el año " + HiddenField_ANNO.Value;

        CargarSubProgramasYActividades(contador, 0, tablaPrograma, "");
    }

    private void Cargar(DataTable tablaPrograma)
    {
        Decimal ID_PROGRAMA_GENERAL = Convert.ToDecimal(HiddenField_ID_PROGRAMA_GENERAL.Value);

        Ocultar(Acciones.SubProgramaYActividad);

        CargarArbolDePrograma(tablaPrograma);
    }

    protected void Button_GUARDAR_SUB_PROGRAMA_Click(object sender, EventArgs e)
    {
        Decimal ID_PROGRAMA_GENERAL = Convert.ToDecimal(HiddenField_ID_PROGRAMA_GENERAL.Value);
        Decimal ID_SUB_PROGRAMA = Convert.ToDecimal(DropDownList_IdSubPrograma.SelectedValue);

        Programa.Areas AREA_SUBPROGRAMA = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);
        Int32 ANNO = Convert.ToInt32(HiddenField_ANNO.Value);

        SubPrograma _subprograma = new SubPrograma(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable TablaSubPrograma = _subprograma.ObtenerSubProgramasPorId(ID_SUB_PROGRAMA, AREA_SUBPROGRAMA);
        DataRow filaSubPrograma = TablaSubPrograma.Rows[0];

        String NOMBRE_SUBPROGRAMA = filaSubPrograma["NOMBRE"].ToString().Trim();


        Decimal ID_ACTIVIDAD = 0;

        Decimal ID_DETALLE_GENERAL_PADRE = 0;
        if (HiddenField_TIPO_NODO_SELECCIONADO.Value == TiposNodo.PROGRAMA.ToString())
        {
            ID_DETALLE_GENERAL_PADRE = 0;
        }
        else
        {
            ID_DETALLE_GENERAL_PADRE = Convert.ToDecimal(HiddenField_ID_NODO_SELECCIONADO.Value);
        }

        Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean verificado = _programa.AdicionarDetalleGeneral(ID_PROGRAMA_GENERAL, TiposNodo.SUBPROGRAMA.ToString(), ID_DETALLE_GENERAL_PADRE, ID_SUB_PROGRAMA, ID_ACTIVIDAD);

        ocultar_mensaje(Panel_FONDO_NUEVO_SUBPROGAMA, Panel_CONTENIDO_NUEVO_SUBPROGRAMA);

        if (verificado == true)
        {
            DataTable tablaEsquemaPrograma = ObtenerEstructuraTablaPrograma();

            Cargar(tablaEsquemaPrograma);
            Cargar_GridView_EsquemaPrograma_DesdeTabla(tablaEsquemaPrograma);


            Informar(Panel_FONDO_MENSAJE_ARBOL, Image_MENSAJE_ARBOL_POPUP, Panel_MENSAJES_ARBOL, Label_MENSAJE_ARBOL, "El Programa para el año " + ANNO + " fue correctamente actualizado.", Proceso.Correcto);
        }
        else
        {
            Informar(Panel_FONDO_MENSAJE_ARBOL, Image_MENSAJE_ARBOL_POPUP, Panel_MENSAJES_ARBOL, Label_MENSAJE_ARBOL, "El Programa para el año " + ANNO + " no puedo ser actualizado: " + _programa.MensajeError, Proceso.Error);
        }
    }

    protected void Button_CANCELAR_SUB_PROGRAMA_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_NUEVO_SUBPROGAMA, Panel_CONTENIDO_NUEVO_SUBPROGRAMA);
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
            ActividadRseGlobal _act = new ActividadRseGlobal(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaAct = _act.ObtenerActividadPorId(Convert.ToDecimal(DropDownList_IdActividad.SelectedValue), AREA);

            DataRow filaAct = tablaAct.Rows[0];

            DropDownList_Tipo.SelectedValue = filaAct["TIPO"].ToString().Trim();
            DropDownList_Sector.SelectedValue = filaAct["SECTOR"].ToString().Trim();
            DropDownList_EstadoActividad.SelectedValue = filaAct["ACTIVO"].ToString().Trim();

            TextBox_DescripcionActividad.Text = filaAct["DESCRIPCION"].ToString().Trim();
        }
    }

    protected void Button_GAURDAR_ACTIVIDAD_Click(object sender, EventArgs e)
    {
        Decimal ID_PROGRAMA_GENERAL = Convert.ToDecimal(HiddenField_ID_PROGRAMA_GENERAL.Value);
        Decimal ID_ACTIVIDAD = Convert.ToDecimal(DropDownList_IdActividad.SelectedValue);

        Programa.Areas AREA_ACTIVIDAD = (Programa.Areas)Enum.Parse(typeof(Programa.Areas), HiddenField_ID_AREA.Value);
        
        Int32 ANNO = Convert.ToInt32(HiddenField_ANNO.Value);

        ActividadRseGlobal _actividad = new ActividadRseGlobal(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable TablaActividad = _actividad.ObtenerActividadPorId(ID_ACTIVIDAD, AREA_ACTIVIDAD);
        DataRow filaActividad = TablaActividad.Rows[0];

        String NOMBRE_ACTIVIDAD = filaActividad["NOMBRE"].ToString().Trim();

        Decimal ID_SUB_PROGRAMA = 0;

        Decimal ID_DETALLE_GENERAL_PADRE = 0;
        if (HiddenField_TIPO_NODO_SELECCIONADO.Value == TiposNodo.PROGRAMA.ToString())
        {
            ID_DETALLE_GENERAL_PADRE = 0;
        }
        else
        {
            ID_DETALLE_GENERAL_PADRE = Convert.ToDecimal(HiddenField_ID_NODO_SELECCIONADO.Value);
        }

        Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean verificado = _programa.AdicionarDetalleGeneral(ID_PROGRAMA_GENERAL, TiposNodo.ACTIVIDAD.ToString(), ID_DETALLE_GENERAL_PADRE, ID_SUB_PROGRAMA, ID_ACTIVIDAD);

        ocultar_mensaje(Panel_FONDO_ACTIVIDAD, Panel_CONTENIDO_ACTIVIDAD);

        if (verificado == true)
        {
            DataTable tablaEsquemaPrograma = ObtenerEstructuraTablaPrograma();
            Cargar(tablaEsquemaPrograma);
            Cargar_GridView_EsquemaPrograma_DesdeTabla(tablaEsquemaPrograma);

            Informar(Panel_FONDO_MENSAJE_ARBOL, Image_MENSAJE_ARBOL_POPUP, Panel_MENSAJES_ARBOL, Label_MENSAJE_ARBOL, "El Programa para el año " + ANNO + " fue correctamente actualizado.", Proceso.Correcto);
        }
        else
        {
            Informar(Panel_FONDO_MENSAJE_ARBOL, Image_MENSAJE_ARBOL_POPUP, Panel_MENSAJES_ARBOL, Label_MENSAJE_ARBOL, "El Programa para el año " + ANNO + " no puedo ser actualizado: " + _programa.MensajeError, Proceso.Error);
        }
    }
    protected void Button_CANCELAR_ACTIVIDAD_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_ACTIVIDAD, Panel_CONTENIDO_ACTIVIDAD);
    }

    protected void AjaxFileUpload_ImagePrograma_OnUploadComplete(object sender, AjaxFileUploadEventArgs e)
    {
        string filePath = "~/imagenes/programasGenerales/" + e.FileName;

        var ajaxFileUpload = (AjaxFileUpload)sender;
        ajaxFileUpload.SaveAs(MapPath(filePath));

        e.PostedUrl = Page.ResolveUrl(filePath);
        Image_Programa.ImageUrl = Page.ResolveUrl(filePath);
    }
    protected void Button_CERRAR_MENSAJE_ARBOL_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE_ARBOL, Panel_MENSAJES_ARBOL);
    }

    protected void Button_IMPRIMIR_Click(object sender, EventArgs e)
    {
        Decimal ID_PROGRAMA_GENERAL = Convert.ToDecimal(HiddenField_ID_PROGRAMA_GENERAL.Value);

        maestrasInterfaz _maestrasInterfaz = new maestrasInterfaz();

        byte[] datosArchivo = _maestrasInterfaz.GenerarProgramaGeneral(ID_PROGRAMA_GENERAL, Request.ServerVariables["HTTP_HOST"].ToString());

        String filename = "PROGRAMA_GENERAL";
        filename = filename.Replace(' ', '_');

        Response.Clear();
        Response.BufferOutput = false; 
        Response.ContentType = "application/pdf";
        Response.AddHeader("Content-Disposition", "attachment;FileName=" + filename + ".pdf");
        Response.BinaryWrite(datosArchivo);
        Response.End();
    }
    protected void ImageButton_AdicionarSubprograma_Click(object sender, ImageClickEventArgs e)
    {
        HiddenField_TIPO_NODO_SELECCIONADO.Value = TiposNodo.PROGRAMA.ToString();
        HiddenField_ID_NODO_SELECCIONADO.Value = "";

        MostrarFormularioCreacionSubprogramaActividad(Panel_FONDO_NUEVO_SUBPROGAMA, Panel_CONTENIDO_NUEVO_SUBPROGRAMA);
        Cargar(Acciones.Subprograma);
        Activar(Acciones.Subprograma);
        TextBox_DescripcionSubPrograma.Text = "";
    }

    private Int32 contarDependenciasDeIdDetalleGeneral(Decimal ID_DETALLE_GENERAL_PADRE)
    {
        Int32 contadorDependancias = 0;

        for (int i = 0; i < GridView_EsquemaPrograma.Rows.Count; i++)
        {
            Decimal ID_DETALLE_GENERAL_PADRE_GRILLA = 0;
            try
            {
                ID_DETALLE_GENERAL_PADRE_GRILLA = Convert.ToDecimal(GridView_EsquemaPrograma.DataKeys[i].Values["ID_DETALLE_GENERAL_PADRE"]);
            }
            catch
            {
                ID_DETALLE_GENERAL_PADRE_GRILLA = 0;
            }

            if (ID_DETALLE_GENERAL_PADRE == ID_DETALLE_GENERAL_PADRE_GRILLA)
            {
                contadorDependancias += 1;
            }
        }

        return contadorDependancias;
    }

    protected void GridView_EsquemaPrograma_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        Decimal ID_PROGRAMA_GENERAL = Convert.ToDecimal(GridView_EsquemaPrograma.DataKeys[indexSeleccionado].Values["ID_PROGRAMA_GENERAL"]); ;
        Decimal ID_DETALLE_GENERAL = Convert.ToDecimal(GridView_EsquemaPrograma.DataKeys[indexSeleccionado].Values["ID_DETALLE_GENERAL"]);

        HiddenField_ID_PROGRAMA_GENERAL.Value = ID_PROGRAMA_GENERAL.ToString();
        HiddenField_ID_NODO_SELECCIONADO.Value = ID_DETALLE_GENERAL.ToString();

        if (e.CommandName == "nuevo_programa")
        {
            HiddenField_TIPO_NODO_SELECCIONADO.Value = TiposNodo.SUBPROGRAMA.ToString();
            MostrarFormularioCreacionSubprogramaActividad(Panel_FONDO_NUEVO_SUBPROGAMA, Panel_CONTENIDO_NUEVO_SUBPROGRAMA);

            Cargar(Acciones.Subprograma);
            Activar(Acciones.Subprograma);
            TextBox_DescripcionSubPrograma.Text = "";
        }
        else
        {
            if (e.CommandName == "nueva_actividad")
            {
                HiddenField_TIPO_NODO_SELECCIONADO.Value = TiposNodo.ACTIVIDAD.ToString();
                MostrarFormularioCreacionSubprogramaActividad(Panel_FONDO_ACTIVIDAD, Panel_CONTENIDO_ACTIVIDAD);

                Cargar(Acciones.Actividad);
                Activar(Acciones.Actividad);
                TextBox_DescripcionActividad.Text = "";
            }
            else
            {
                if (e.CommandName == "eliminar")
                {
                    Boolean programaActualizado = false;

                    GridViewRow filaGrilla = GridView_EsquemaPrograma.Rows[indexSeleccionado];
                    Label textoNombreEntidad = filaGrilla.FindControl("Label_NombreSubprogramaActividad") as Label;
                    Label textoDescripcionEntidad = filaGrilla.FindControl("Label_DescripcionProgramaActividad") as Label;

                    if (GridView_EsquemaPrograma.DataKeys[indexSeleccionado].Values["TIPO"].ToString() == TiposNodo.ACTIVIDAD.ToString())
                    {
                        Programa _prog = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                        DataTable tablaAsignaciones = _prog.ObtenerAsignacionesDeUnaActividadAProgramaEspecifico(ID_DETALLE_GENERAL);

                        if (tablaAsignaciones.Rows.Count <= 0)
                        {
                            if (_prog.MensajeError != null)
                            {
                                Informar(Panel_FONDO_MENSAJE_ARBOL, Image_MENSAJE_ARBOL_POPUP, Panel_MENSAJES_ARBOL, Label_MENSAJE_ARBOL, _prog.MensajeError, Proceso.Error);
                            }
                            else
                            {
                                Label_TipoEntidadEliminacion.Text = TiposNodo.ACTIVIDAD.ToString();
                                Label1_IdentificadorEntidadEliminacion.Text = ID_DETALLE_GENERAL.ToString();

                                Label_DescripcionEntidadEliminacion.Text = textoNombreEntidad.Text + ": " + textoDescripcionEntidad.Text;

                                MostrarFormularioConfirmacionEliminacionActividadSubprograma(Panel_FONDO_CONFIRMACION_ELIMINACION, Panel_CONTENIDO_CONFIRMACION_ELIMINACION);

                            }
                        }
                        else
                        {
                            Informar(Panel_FONDO_MENSAJE_ARBOL, Image_MENSAJE_ARBOL_POPUP, Panel_MENSAJES_ARBOL, Label_MENSAJE_ARBOL, "La Actividad que intenta eliminar ya fue asignada en uno o más Programas Específicos, no puede ser eliminada del Programa General", Proceso.Advertencia);
                        }
                    }
                    else
                    {
                        if (contarDependenciasDeIdDetalleGeneral(ID_DETALLE_GENERAL) <= 0)
                        {
                            Label_TipoEntidadEliminacion.Text = TiposNodo.SUBPROGRAMA.ToString();
                            Label1_IdentificadorEntidadEliminacion.Text = ID_DETALLE_GENERAL.ToString();

                            Label_DescripcionEntidadEliminacion.Text = textoNombreEntidad.Text + ": " + textoDescripcionEntidad.Text;

                            MostrarFormularioConfirmacionEliminacionActividadSubprograma(Panel_FONDO_CONFIRMACION_ELIMINACION, Panel_CONTENIDO_CONFIRMACION_ELIMINACION);


                        }
                        else
                        {
                            Informar(Panel_FONDO_MENSAJE_ARBOL, Image_MENSAJE_ARBOL_POPUP, Panel_MENSAJES_ARBOL, Label_MENSAJE_ARBOL, "El Subprograma seleccionado tiene Actividades o SubProgramas Asociados, no puede ser eliminado.", Proceso.Advertencia);
                        }
                    }

                    if (programaActualizado == true)
                    {
                        DataTable tablaEsquemaPrograma = ObtenerEstructuraTablaPrograma();
                        Cargar(tablaEsquemaPrograma);
                        Cargar_GridView_EsquemaPrograma_DesdeTabla(tablaEsquemaPrograma);
                    }
                }
            }
        }
    }

    protected void GridView_EsquemaPrograma_PreRender(object sender, EventArgs e)
    {
        for (int i = 0; i < GridView_EsquemaPrograma.Rows.Count; i++)
        {
            if (GridView_EsquemaPrograma.DataKeys[i].Values["TIPO"].ToString() == TiposNodo.ACTIVIDAD.ToString())
            {
                GridView_EsquemaPrograma.Rows[i].Cells[1].Enabled = false;
                GridView_EsquemaPrograma.Rows[i].Cells[1].Text = "";
                GridView_EsquemaPrograma.Rows[i].Cells[2].Enabled = false;
                GridView_EsquemaPrograma.Rows[i].Cells[2].Text = "";
            }
            else
            {
                GridView_EsquemaPrograma.Rows[i].Cells[1].Enabled = true;
                GridView_EsquemaPrograma.Rows[i].Cells[2].Enabled = true;
            }
        }
    }

    protected void ImageButton_AdicionarActividad_Click(object sender, ImageClickEventArgs e)
    {
        HiddenField_TIPO_NODO_SELECCIONADO.Value = TiposNodo.PROGRAMA.ToString();
        HiddenField_ID_NODO_SELECCIONADO.Value = "";

        MostrarFormularioCreacionSubprogramaActividad(Panel_FONDO_ACTIVIDAD, Panel_CONTENIDO_ACTIVIDAD);
        Cargar(Acciones.Actividad);
        Activar(Acciones.Actividad);

        TextBox_DescripcionActividad.Text = "";
    }
    protected void Button_CancelarEliminacion_Click(object sender, EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_CONFIRMACION_ELIMINACION, Panel_CONTENIDO_CONFIRMACION_ELIMINACION);
    }
    protected void Button_AceptarEliminacion_Click(object sender, EventArgs e)
    {
        Programa _prog = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Decimal ID_DETALLE_GENERAL = Convert.ToDecimal(Label1_IdentificadorEntidadEliminacion.Text);

        Boolean programaActualizado = false;

        if (Label_TipoEntidadEliminacion.Text == TiposNodo.ACTIVIDAD.ToString())
        {
            if (_prog.DesactivarProgGeneralDetalle(ID_DETALLE_GENERAL) == false)
            {
                Informar(Panel_FONDO_MENSAJE_ARBOL, Image_MENSAJE_ARBOL_POPUP, Panel_MENSAJES_ARBOL, Label_MENSAJE_ARBOL, _prog.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE_ARBOL, Image_MENSAJE_ARBOL_POPUP, Panel_MENSAJES_ARBOL, Label_MENSAJE_ARBOL, "El Programa General fue actualizado correctamente.", Proceso.Correcto);
                programaActualizado = true;
            }
        }
        else
        {
            if (_prog.DesactivarProgGeneralDetalle(ID_DETALLE_GENERAL) == false)
            {
                Informar(Panel_FONDO_MENSAJE_ARBOL, Image_MENSAJE_ARBOL_POPUP, Panel_MENSAJES_ARBOL, Label_MENSAJE_ARBOL, _prog.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE_ARBOL, Image_MENSAJE_ARBOL_POPUP, Panel_MENSAJES_ARBOL, Label_MENSAJE_ARBOL, "El Programa General fue actualizado correctamente.", Proceso.Correcto);
                programaActualizado = true;
            }
        }

        if (programaActualizado == true)
        {
            DataTable tablaEsquemaPrograma = ObtenerEstructuraTablaPrograma();
            Cargar(tablaEsquemaPrograma);
            Cargar_GridView_EsquemaPrograma_DesdeTabla(tablaEsquemaPrograma);
        }

        ocultar_mensaje(Panel_FONDO_CONFIRMACION_ELIMINACION, Panel_CONTENIDO_CONFIRMACION_ELIMINACION);
    }
}
