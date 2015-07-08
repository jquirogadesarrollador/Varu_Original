using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brainsbits.LLB.seleccion;
using System.Data;
using System.Collections.Generic;
using Brainsbits.LLB;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;
using TSHAK.Components;
using System.Web;

public partial class _PreguntasRefLaboral : System.Web.UI.Page
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
    private System.Drawing.Color colorSeleccionado = System.Drawing.ColorTranslator.FromHtml("#699DF0");

    private enum Acciones
    {
        Inicio = 0,
        Nuevo,
        Cargar,
        ModificarPreg,
        ModificarCat,
        CargarPreg
    }

    private enum AccionesGrilla
    { 
        Ninguna,
        Nuevo,
        Modificar,
        Eliminar
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }

    private enum ProcesoForm
    {
        Inicio = 1, 
        EditarCat, 
        EditarPreg
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

                Panel_FORM_BOTONES.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_MODIFICAR_PREGUNTAS.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_CANCELAR.Visible = false;

                Panel_Categorias.Visible = false;
                Button_NUEVA_CAT.Visible = false;
                Button_GUARDAR_CAT.Visible = false;
                Button_CANCELAR_CAT.Visible = false;

                Panel_PREGUNTAS.Visible = false;
                Button_NUEVA_PREGUNTA.Visible = false;
                Button_GUARDAR_PREGUNTA.Visible = false;
                Button_CANCELAR_PREGUNTA.Visible = false;

                Panel_FORM_BOTONES_ABAJO.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                Button_MODIFICAR_PREGUNTAS_1.Visible = false;

                GridView_CATEGORIAS.Columns[0].Visible = false;
                GridView_CATEGORIAS.Columns[1].Visible = false;
                GridView_CATEGORIAS.Columns[2].Visible = false;

                GridView_PREGUNTAS.Columns[0].Visible = false;
                GridView_PREGUNTAS.Columns[1].Visible = false;
                break;
            case Acciones.ModificarPreg:
                Button_MODIFICAR.Visible = false;
                Button_MODIFICAR_1.Visible = false; 
                Button_MODIFICAR_PREGUNTAS.Visible = false;
                Button_MODIFICAR_PREGUNTAS_1.Visible = false;

                Button_NUEVA_PREGUNTA.Visible = false;
                Button_GUARDAR_PREGUNTA.Visible = false;
                Button_CANCELAR_PREGUNTA.Visible = false;   
                break;
            case Acciones.ModificarCat:
                Button_GUARDAR.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_MODIFICAR_PREGUNTAS.Visible = false;
                Button_CANCELAR.Visible = false;

                Button_GUARDAR_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_MODIFICAR_PREGUNTAS_1.Visible = false;
                Button_CANCELAR_1.Visible = false;

                Button_NUEVA_CAT.Visible = false;
                Button_GUARDAR_CAT.Visible = false;
                Button_CANCELAR_CAT.Visible = false;

                Panel_PREGUNTAS.Visible = false;
                Panel_FORM_BOTONES_ABAJO.Visible = false;

                Button_NUEVA_PREGUNTA.Visible = false;
                Button_GUARDAR_PREGUNTA.Visible = false;
                Button_CANCELAR_PREGUNTA.Visible = false;

                GridView_CATEGORIAS.Columns[0].Visible = false;
                break;
            case Acciones.CargarPreg:
                Button_GUARDAR.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR.Visible = false;
                Button_CANCELAR_1.Visible = false;

                GridView_PREGUNTAS.Columns[0].Visible = false;
                GridView_PREGUNTAS.Columns[1].Visible = false;
                break;
        }
    }

    private void Desactivar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                break;
        }
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_FORM_BOTONES.Visible = true;
                Panel_FORM_BOTONES_ABAJO.Visible = true;
                Button_MODIFICAR.Visible = true;
                Button_MODIFICAR_1.Visible = true;
                Panel_Categorias.Visible = true;

                GridView_CATEGORIAS.Columns[0].Visible = true;
                break;
            case Acciones.Nuevo:
                Panel_FORM_BOTONES.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_PREGUNTAS.Visible = true;

                Button_NUEVA_PREGUNTA.Visible = true;

                Panel_FORM_BOTONES_ABAJO.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;

                GridView_PREGUNTAS.Columns[0].Visible = true;
                GridView_PREGUNTAS.Columns[1].Visible = true;
                break;
            case Acciones.Cargar:
                Panel_FORM_BOTONES.Visible = true;
                Button_MODIFICAR.Visible = true;

                Panel_PREGUNTAS.Visible = true;

                Panel_FORM_BOTONES_ABAJO.Visible = true;
                Button_MODIFICAR_1.Visible = true;
                break;
            case Acciones.ModificarPreg:
                Panel_FORM_BOTONES.Visible = true;
                Panel_FORM_BOTONES_ABAJO.Visible = true;

                Button_GUARDAR.Visible = true;
                Button_GUARDAR_1.Visible = true;

                Button_CANCELAR.Visible = true;
                Button_CANCELAR_1.Visible = true;

                Panel_PREGUNTAS.Visible = true;
                Button_NUEVA_PREGUNTA.Visible = true;

                GridView_PREGUNTAS.Columns[0].Visible = true;
                GridView_PREGUNTAS.Columns[1].Visible = true;
                break;
            case Acciones.ModificarCat:
                Panel_Categorias.Visible = true;
                Button_NUEVA_CAT.Visible = true;

                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;

                GridView_CATEGORIAS.Columns[1].Visible = true;
                GridView_CATEGORIAS.Columns[2].Visible = true;
                break;
            case Acciones.CargarPreg:
                Button_MODIFICAR.Visible = true;
                Button_MODIFICAR_1.Visible = true;
                Button_MODIFICAR_PREGUNTAS.Visible = true;
                Button_MODIFICAR_PREGUNTAS_1.Visible = true;

                Panel_PREGUNTAS.Visible = true;

                break;
        }
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

    private void CargarGrillaPreguntasDesdeTabla(DataTable tablaPreguntas)
    {
        GridView_PREGUNTAS.DataSource = tablaPreguntas;
        GridView_PREGUNTAS.DataBind();

        for (int i = 0; i < GridView_PREGUNTAS.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_PREGUNTAS.Rows[i];
            DataRow filaTabla = tablaPreguntas.Rows[i];

            TextBox texto = filaGrilla.FindControl("TextBox_PREGUNTA") as TextBox;
            texto.Text = filaTabla["CONTENIDO"].ToString().Trim();
        }
    }


    private void CargarGrillaCategoriasDesdeTabla(DataTable tablaCategorias)
    {
        GridView_CATEGORIAS.DataSource = tablaCategorias;
        GridView_CATEGORIAS.DataBind();

        for (int i = 0; i < GridView_CATEGORIAS.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_CATEGORIAS.Rows[i];
            DataRow filaTabla = tablaCategorias.Rows[i];

            TextBox texto = filaGrilla.FindControl("TextBox_NOMBRE_CAT") as TextBox;
            texto.Text = filaTabla["NOMBRE_CAT"].ToString().Trim();
        }
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

    private void cargarGrillaCategorias()
    {
        referencia _referencia = new referencia(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCategorias = _referencia.ObtenerCategoriasActivas();

        if (tablaCategorias.Rows.Count <= 0)
        {
            if (_referencia.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _referencia.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron Categorías Activas. Por favor Cree la Categorías correspondientes.", Proceso.Advertencia);
            }

            GridView_CATEGORIAS.DataSource = null;
            GridView_CATEGORIAS.DataBind();

            foreach (GridView filaGrilla in GridView_CATEGORIAS.Rows)
            {
                filaGrilla.BackColor = System.Drawing.Color.Transparent;
            }

            Panel_Categorias.Visible = false;
        }
        else
        {
            CargarGrillaCategoriasDesdeTabla(tablaCategorias);

            inhabilitarFilasGrilla(GridView_CATEGORIAS, 3);
        }
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                cargarGrillaCategorias();

                HiddenField_ID_CATEGORIA_SEL.Value = "";
                HiddenField_ID_PREGUNTA_SEL.Value = "";
                HiddenField_PROCESO.Value = ProcesoForm.Inicio.ToString();
                break;
            case Acciones.ModificarPreg:
                HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_FILA_SELECCIONADA_GRILLA.Value = "";
                HiddenField_ID_PREGUNTA.Value = "";
                HiddenField_PREGUNTA.Value = "";

                HiddenField_PROCESO.Value = ProcesoForm.EditarPreg.ToString();
                HiddenField_ID_PREGUNTA_SEL.Value = "";

                break;
            case Acciones.ModificarCat:
                HiddenField_ACCION_GRILLA_CAT.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_PROCESO.Value = ProcesoForm.EditarCat.ToString();
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
        Page.Header.Title = "PREGUNTAS PARA REFERENCIA LABORAL";

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

    private void ActualizarPreguntas()
    {
        Decimal ID_CATEGORIA = Convert.ToDecimal(HiddenField_ID_CATEGORIA_SEL.Value);

        List<preguntaReferencia> listaPreguntas = new List<preguntaReferencia>();
        preguntaReferencia preguntaReferenciaParaLista;

        for (int i = 0; i < GridView_PREGUNTAS.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_PREGUNTAS.Rows[i];
            preguntaReferenciaParaLista = new preguntaReferencia();

            TextBox dato = filaGrilla.FindControl("TextBox_PREGUNTA") as TextBox;
            preguntaReferenciaParaLista.CONTENIDO = dato.Text;

            preguntaReferenciaParaLista.ID_PREGUNTA = Convert.ToDecimal(GridView_PREGUNTAS.DataKeys[i].Values["ID_PREGUNTA"]);

            preguntaReferenciaParaLista.ID_CATEGORIA = Convert.ToDecimal(GridView_PREGUNTAS.DataKeys[i].Values["ID_CATEGORIA"]);

            listaPreguntas.Add(preguntaReferenciaParaLista);
        }

        referencia _referencia = new referencia(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean verificador = _referencia.ActualizarPreguntas(listaPreguntas, ID_CATEGORIA);

        if (verificador == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _referencia.MensajeError, Proceso.Error);
        }
        else
        {
            HiddenField_PROCESO.Value = ProcesoForm.Inicio.ToString();

            for(int i = 0; i < GridView_CATEGORIAS.Rows.Count; i++)
            {
                if(Convert.ToDecimal(GridView_CATEGORIAS.DataKeys[i].Values["ID_CATEGORIA"]) == ID_CATEGORIA)
                {
                    GridView_CATEGORIAS.Rows[i].BackColor = colorSeleccionado;
                }
                else
                {
                    GridView_CATEGORIAS.Rows[i].BackColor = System.Drawing.Color.Transparent;
                }
            }

            CargarPreguntas(ID_CATEGORIA);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Las Preguntas asociadas a la categoría seleccionada, fueron actualizadas correctamente.", Proceso.Correcto);
        }
    }

    private void ActualizarCategorias()
    {
        List<CategoriaReferencia> listaCategorias = new List<CategoriaReferencia>();

        for (int i = 0; i < GridView_CATEGORIAS.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_CATEGORIAS.Rows[i];
            CategoriaReferencia categoriaParaLista = new CategoriaReferencia();

            Decimal ID_CATEGORIA = Convert.ToDecimal(GridView_CATEGORIAS.DataKeys[i].Values["ID_CATEGORIA"]);
            categoriaParaLista.ID_CATEGORIA = ID_CATEGORIA;

            TextBox datoNombre = filaGrilla.FindControl("TextBox_NOMBRE_cAT") as TextBox;
            categoriaParaLista.NOMBRE_CAT = datoNombre.Text.Trim();

            listaCategorias.Add(categoriaParaLista);
        }


        referencia _ref = new referencia(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean verificador = _ref.ActualizarCategorias(listaCategorias);

        if (verificador == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _ref.MensajeError, Proceso.Error);
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Las Categorías fueron actualizadas correctamente.", Proceso.Correcto);
        }

    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        if (HiddenField_PROCESO.Value == ProcesoForm.EditarCat.ToString())
        {
            if (HiddenField_ACCION_GRILLA_CAT.Value != AccionesGrilla.Ninguna.ToString())
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe Terminar las acciones sobre la Grilla de Categorías para poder continuar.", Proceso.Advertencia);
            }
            else
            {
                ActualizarCategorias();
            }
        }
        else
        {
            if (HiddenField_PROCESO.Value == ProcesoForm.EditarPreg.ToString())
            {
                if (HiddenField_ACCION_GRILLA.Value == AccionesGrilla.Ninguna.ToString())
                {
                    ActualizarPreguntas();
                }
                else
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Para poder guardar los cambios primero debe terminar las acciones sobre la grilla de PREGUNTAS.", Proceso.Advertencia);
                }
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se pudo determinar la información a guardar.", Proceso.Error);
            }
        }
    }
    
    private void Modificar()
    {
        Ocultar(Acciones.ModificarCat);

    }
    
    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.ModificarCat);
        Mostrar(Acciones.ModificarCat);
        Cargar(Acciones.ModificarCat);
    }
    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        if (HiddenField_PROCESO.Value == ProcesoForm.EditarPreg.ToString())
        {
            Decimal ID_CATEGORIA = Convert.ToDecimal(HiddenField_ID_CATEGORIA_SEL.Value);

            HiddenField_ID_CATEGORIA_SEL.Value = ID_CATEGORIA.ToString();
            HiddenField_PROCESO.Value = ProcesoForm.Inicio.ToString();

            for (int i = 0; i < GridView_CATEGORIAS.Rows.Count; i++)
            {
                if (Convert.ToDecimal(GridView_CATEGORIAS.DataKeys[i].Values["ID_CATEGORIA"]) == ID_CATEGORIA)
                {
                    GridView_CATEGORIAS.Rows[i].BackColor = colorSeleccionado;
                }
                else
                {
                    GridView_CATEGORIAS.Rows[i].BackColor = System.Drawing.Color.Transparent;
                }
            }

            CargarPreguntas(ID_CATEGORIA);
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);
        }
    }

    private DataTable obtenerTablaDeGrillaPreguntas()
    {
        DataTable tablaResultado = new DataTable();
        tablaResultado.Columns.Add("ID_PREGUNTA");
        tablaResultado.Columns.Add("ID_CATEGORIA");
        tablaResultado.Columns.Add("CONTENIDO");

        for (int i = 0; i < GridView_PREGUNTAS.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_PREGUNTAS.Rows[i];

            Decimal ID_PREGUNTA = Convert.ToDecimal(GridView_PREGUNTAS.DataKeys[i].Values["ID_PREGUNTA"]);

            Decimal ID_CATEGORIA = Convert.ToDecimal(GridView_PREGUNTAS.DataKeys[i].Values["ID_CATEGORIA"]);

            TextBox dato = filaGrilla.FindControl("TextBox_PREGUNTA") as TextBox;
            String CONTENIDO = dato.Text;

            DataRow filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_PREGUNTA"] = ID_PREGUNTA;
            filaTablaResultado["ID_CATEGORIA"] = ID_CATEGORIA;
            filaTablaResultado["CONTENIDO"] = CONTENIDO;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }

    private void ActivarFilaGrilla(GridView grilla, int fila, int colInicio)
    {
        for (int i = 0; i < grilla.Columns.Count; i++)
        {
            grilla.Rows[fila].Cells[i].Enabled = true;
        }
    }

    protected void Button_NUEVA_PREGUNTA_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerTablaDeGrillaPreguntas();
        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_PREGUNTA"] = 0;
        filaNueva["ID_CATEGORIA"] = Convert.ToDecimal(HiddenField_ID_CATEGORIA_SEL.Value);
        filaNueva["CONTENIDO"] = "";

        tablaDesdeGrilla.Rows.Add(filaNueva);

        CargarGrillaPreguntasDesdeTabla(tablaDesdeGrilla);

        inhabilitarFilasGrilla(GridView_PREGUNTAS, 2);
        ActivarFilaGrilla(GridView_PREGUNTAS, GridView_PREGUNTAS.Rows.Count - 1, 2);

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_SELECCIONADA_GRILLA.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();
        HiddenField_ID_PREGUNTA.Value = null;
        HiddenField_PREGUNTA.Value = null;

        GridView_PREGUNTAS.Columns[0].Visible = false;
        GridView_PREGUNTAS.Columns[1].Visible = false;

        Button_NUEVA_PREGUNTA.Visible = false;
        Button_GUARDAR_PREGUNTA.Visible = true;
        Button_CANCELAR_PREGUNTA.Visible = true;
    }

    private void inhabilitarFilaGrilla(GridView grilla, int fila, int colInicio)
    {
        for (int j = colInicio; j < grilla.Columns.Count; j++)
        {
            grilla.Rows[fila].Cells[j].Enabled = false;
        }
    }

    protected void Button_GUARDAR_PREGUNTA_Click(object sender, EventArgs e)
    {
        int FILA_SELECCIONADA = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA.Value);

        inhabilitarFilaGrilla(GridView_PREGUNTAS, FILA_SELECCIONADA, 2);

        GridView_PREGUNTAS.Columns[0].Visible = true;
        GridView_PREGUNTAS.Columns[1].Visible = true;

        Button_GUARDAR_PREGUNTA.Visible = false;
        Button_CANCELAR_PREGUNTA.Visible = false;
        Button_NUEVA_PREGUNTA.Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
    }
    protected void Button_CANCELAR_PREGUNTA_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = obtenerTablaDeGrillaPreguntas();

        if (HiddenField_ACCION_GRILLA.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }
        else
        {
            if (HiddenField_ACCION_GRILLA.Value == AccionesGrilla.Modificar.ToString())
            {
                DataRow filaGrilla = tablaGrilla.Rows[Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA.Value)];

                filaGrilla["ID_PREGUNTA"] = Convert.ToDecimal(HiddenField_ID_PREGUNTA.Value);
                filaGrilla["ID_CATEGORIA"] = Convert.ToDecimal(HiddenField_ID_CATEGORIA_SEL.Value);
                filaGrilla["CONTENIDO"] = HiddenField_PREGUNTA.Value;
            }
        }

        CargarGrillaPreguntasDesdeTabla(tablaGrilla);

        inhabilitarFilasGrilla(GridView_PREGUNTAS, 2);

        GridView_PREGUNTAS.Columns[0].Visible = true;
        GridView_PREGUNTAS.Columns[1].Visible = true;

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();

        Button_NUEVA_PREGUNTA.Visible = true;
        Button_GUARDAR_PREGUNTA.Visible = false;
        Button_CANCELAR_PREGUNTA.Visible = false;
    }
    protected void GridView_PREGUNTAS_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "modificar")
        {
            HiddenField_FILA_SELECCIONADA_GRILLA.Value = indexSeleccionado.ToString();

            ActivarFilaGrilla(GridView_PREGUNTAS, indexSeleccionado, 2);

            TextBox dato;

            HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Modificar.ToString();
            HiddenField_FILA_SELECCIONADA_GRILLA.Value = indexSeleccionado.ToString();

            HiddenField_ID_PREGUNTA.Value = GridView_PREGUNTAS.DataKeys[indexSeleccionado].Values["ID_PREGUNTA"].ToString();

            dato = GridView_PREGUNTAS.Rows[indexSeleccionado].FindControl("TextBox_PREGUNTA") as TextBox;
            HiddenField_PREGUNTA.Value = dato.Text;

            GridView_PREGUNTAS.Columns[0].Visible = false;
            GridView_PREGUNTAS.Columns[1].Visible = false;

            Button_NUEVA_PREGUNTA.Visible = false;
            Button_GUARDAR_PREGUNTA.Visible = true;
            Button_CANCELAR_PREGUNTA.Visible = true;
        }
        else
        {
            if (e.CommandName == "eliminar")
            {
                DataTable tablaDesdeGrilla = obtenerTablaDeGrillaPreguntas();

                tablaDesdeGrilla.Rows.RemoveAt(indexSeleccionado);

                CargarGrillaPreguntasDesdeTabla(tablaDesdeGrilla);

                inhabilitarFilasGrilla(GridView_PREGUNTAS, 2);

                GridView_PREGUNTAS.Columns[0].Visible = true;
                GridView_PREGUNTAS.Columns[1].Visible = true;

                HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_FILA_SELECCIONADA_GRILLA.Value = null;
                HiddenField_ID_PREGUNTA.Value = "";
                HiddenField_PREGUNTA.Value = "";

                Button_NUEVA_PREGUNTA.Visible = true;
                Button_GUARDAR_PREGUNTA.Visible = false;
                Button_CANCELAR_PREGUNTA.Visible = false;
            }
        }
    }
    protected void Button_MODIFICAR_PREGUNTAS_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.ModificarPreg);
        Mostrar(Acciones.ModificarPreg);
        Cargar(Acciones.ModificarPreg);
    }

    private DataTable obtenerTablaDeGrillaCat()
    {
        DataTable tablaResultado = new DataTable();
        tablaResultado.Columns.Add("ID_CATEGORIA");
        tablaResultado.Columns.Add("NOMBRE_CAT");

        for (int i = 0; i < GridView_CATEGORIAS.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_CATEGORIAS.Rows[i];

            Decimal ID_CATEGORIA = Convert.ToDecimal(GridView_CATEGORIAS.DataKeys[i].Values["ID_CATEGORIA"]);

            TextBox dato = filaGrilla.FindControl("TextBox_NOMBRE_CAT") as TextBox;
            String NOMBRE_CAT = dato.Text;

            DataRow filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_CATEGORIA"] = ID_CATEGORIA;
            filaTablaResultado["NOMBRE_CAT"] = NOMBRE_CAT;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }

    protected void Button_NUEVA_CAT_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerTablaDeGrillaCat();
        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_CATEGORIA"] = 0;
        filaNueva["NOMBRE_CAT"] = "";

        tablaDesdeGrilla.Rows.Add(filaNueva);

        CargarGrillaCategoriasDesdeTabla(tablaDesdeGrilla);

        inhabilitarFilasGrilla(GridView_CATEGORIAS, 3);
        ActivarFilaGrilla(GridView_CATEGORIAS, GridView_CATEGORIAS.Rows.Count - 1, 3);

        HiddenField_ACCION_GRILLA_CAT.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_SELECCIONADA_GRILLA_CAT.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();
        HiddenField_ID_CATEGORIA.Value = "";
        HiddenField_NOMBRE_CAT.Value = "";

        GridView_CATEGORIAS.Columns[0].Visible = false;
        GridView_CATEGORIAS.Columns[1].Visible = false;
        GridView_CATEGORIAS.Columns[2].Visible = false;

        Button_NUEVA_CAT.Visible = false;
        Button_GUARDAR_CAT.Visible = true;
        Button_CANCELAR_CAT.Visible = true;
    }
    protected void Button_GUARDAR_CAT_Click(object sender, EventArgs e)
    {
        int FILA_SELECCIONADA = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_CAT.Value);

        inhabilitarFilaGrilla(GridView_CATEGORIAS, FILA_SELECCIONADA, 3);

        GridView_CATEGORIAS.Columns[0].Visible = false;
        GridView_CATEGORIAS.Columns[1].Visible = true;
        GridView_CATEGORIAS.Columns[2].Visible = true;

        Button_GUARDAR_CAT.Visible = false;
        Button_CANCELAR_CAT.Visible = false;
        Button_NUEVA_CAT.Visible = true;

        HiddenField_ACCION_GRILLA_CAT.Value = AccionesGrilla.Ninguna.ToString();
    }
    protected void Button_CANCELAR_CAT_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = obtenerTablaDeGrillaCat();

        if (HiddenField_ACCION_GRILLA_CAT.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }
        else
        {
            if (HiddenField_ACCION_GRILLA_CAT.Value == AccionesGrilla.Modificar.ToString())
            {
                DataRow filaGrilla = tablaGrilla.Rows[Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_CAT.Value)];

                filaGrilla["ID_CATEGORIA"] = HiddenField_ID_CATEGORIA.Value;
                filaGrilla["NOMBRE_cAT"] = HiddenField_NOMBRE_CAT.Value;
            }
        }

        CargarGrillaCategoriasDesdeTabla(tablaGrilla);

        inhabilitarFilasGrilla(GridView_CATEGORIAS, 3);

        GridView_CATEGORIAS.Columns[0].Visible = false;
        GridView_CATEGORIAS.Columns[1].Visible = true;
        GridView_CATEGORIAS.Columns[2].Visible = true;

        HiddenField_ACCION_GRILLA_CAT.Value = AccionesGrilla.Ninguna.ToString();

        Button_NUEVA_CAT.Visible = true;
        Button_GUARDAR_CAT.Visible = false;
        Button_CANCELAR_CAT.Visible = false;
    }

    private void CargarPreguntas(Decimal ID_CATEGORIA)
    {
        HiddenField_ID_CATEGORIA.Value = ID_CATEGORIA.ToString();
        HiddenField_PROCESO.Value = ProcesoForm.Inicio.ToString();

        HiddenField_ACCION_GRILLA.Value = AccionesGrilla.Ninguna.ToString();
        HiddenField_ACCION_GRILLA_CAT.Value = AccionesGrilla.Ninguna.ToString();

        Ocultar(Acciones.CargarPreg);
        Mostrar(Acciones.CargarPreg);

        referencia _ref = new referencia(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaPreguntas = _ref.ObtenerPreguntasActivas(ID_CATEGORIA);

        if (tablaPreguntas.Rows.Count <= 0)
        {
            if (_ref.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _ref.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontraron preguntas configuradas para la Categoría seleccionada.", Proceso.Advertencia);
            }

            GridView_PREGUNTAS.DataSource = null;
            GridView_PREGUNTAS.DataBind();

            Panel_PREGUNTAS.Visible = false;
        }
        else
        {
            CargarGrillaPreguntasDesdeTabla(tablaPreguntas);

            inhabilitarFilasGrilla(GridView_PREGUNTAS, 2);
        }
    }

    protected void GridView_CATEGORIAS_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "modificar")
        {
            HiddenField_FILA_SELECCIONADA_GRILLA_CAT.Value = indexSeleccionado.ToString();

            ActivarFilaGrilla(GridView_CATEGORIAS, indexSeleccionado, 3);

            HiddenField_ACCION_GRILLA_CAT.Value = AccionesGrilla.Modificar.ToString();
            HiddenField_FILA_SELECCIONADA_GRILLA_CAT.Value = indexSeleccionado.ToString();

            HiddenField_ID_CATEGORIA.Value = GridView_CATEGORIAS.DataKeys[indexSeleccionado].Values["ID_CATEGORIA"].ToString();

            TextBox dato = GridView_CATEGORIAS.Rows[indexSeleccionado].FindControl("TextBox_NOMBRE_CAT") as TextBox;
            HiddenField_NOMBRE_CAT.Value = dato.Text;

            GridView_CATEGORIAS.Columns[0].Visible = false;
            GridView_CATEGORIAS.Columns[1].Visible = false;
            GridView_CATEGORIAS.Columns[2].Visible = false;

            Button_NUEVA_CAT.Visible = false;
            Button_GUARDAR_CAT.Visible = true;
            Button_CANCELAR_CAT.Visible = true;
        }
        else
        {
            if (e.CommandName == "eliminar")
            {
                DataTable tablaDesdeGrilla = obtenerTablaDeGrillaCat();

                tablaDesdeGrilla.Rows.RemoveAt(indexSeleccionado);

                CargarGrillaCategoriasDesdeTabla(tablaDesdeGrilla);

                inhabilitarFilasGrilla(GridView_CATEGORIAS, 3);

                GridView_CATEGORIAS.Columns[0].Visible = false;
                GridView_CATEGORIAS.Columns[1].Visible = true;
                GridView_CATEGORIAS.Columns[2].Visible = true;

                HiddenField_ACCION_GRILLA_CAT.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_FILA_SELECCIONADA_GRILLA_CAT.Value = null;
                HiddenField_ID_CATEGORIA.Value = "";
                HiddenField_NOMBRE_CAT.Value = "";

                Button_NUEVA_CAT.Visible = true;
                Button_GUARDAR_CAT.Visible = false;
                Button_CANCELAR_CAT.Visible = false;
            }
            else
            {
                if (e.CommandName == "seleccionar")
                {
                    Decimal ID_CATEGORIA = Convert.ToDecimal(GridView_CATEGORIAS.DataKeys[indexSeleccionado].Values["ID_CATEGORIA"]);

                    HiddenField_ID_CATEGORIA_SEL.Value = ID_CATEGORIA.ToString();
                    HiddenField_PROCESO.Value = ProcesoForm.Inicio.ToString();

                    foreach (GridViewRow filaGrilla in GridView_CATEGORIAS.Rows)
                    {
                        filaGrilla.BackColor = System.Drawing.Color.Transparent;
                    }

                    GridView_CATEGORIAS.Rows[indexSeleccionado].BackColor = colorSeleccionado;

                    CargarPreguntas(ID_CATEGORIA);
                }
            }
        }
    }
}