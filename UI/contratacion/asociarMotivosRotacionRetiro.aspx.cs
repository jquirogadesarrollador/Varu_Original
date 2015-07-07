using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brainsbits.LLB.seleccion;
using System.Data;
using Brainsbits.LLB.maestras;
using TSHAK.Components;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB;

public partial class _AsociacionMotivosRotacionRetiro : System.Web.UI.Page
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
    private String[] arregloDeColores = { "B1B1B1", "D2D2D2" };

    private enum Acciones
    {
        Inicio = 0,
        Nuevo,
        Cargar,
        Modificar
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
                Panel_FORM_BOTONES.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_CANCELAR.Visible = false;

                Panel_GrillaMotivosRotacion.Visible = false;

                Panel_BOTONES_PIE.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                break;
            case Acciones.Modificar:
                Button_MODIFICAR.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_CANCELAR.Visible = false;

                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR.Visible = false;
                break;
        }
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = true;

                Panel_FORM_BOTONES.Visible = true;
                Button_MODIFICAR.Visible = true;

                Panel_GrillaMotivosRotacion.Visible = true;

                Panel_BOTONES_PIE.Visible = true;
                Button_MODIFICAR_1.Visible = true;
                break;
            case Acciones.Modificar:
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;

                break;
        }
    }

    private void CargarGrillaMotivosRotacionDesdeTabla(DataTable tablaCat)
    {
        GridView_MotivosRotacion.DataSource = tablaCat;
        GridView_MotivosRotacion.DataBind();

        Int32 contadorDeColor = 0;
        System.Drawing.Color colorActual = System.Drawing.Color.Transparent;
        String TITULO_MAESTRA_ROTACION = string.Empty; ;

        for (int i = 0; i < GridView_MotivosRotacion.Rows.Count; i++)
        {
            DataRow filaTabla = tablaCat.Rows[i];

            if (i == 0)
            {
                contadorDeColor = 0;
                colorActual = System.Drawing.ColorTranslator.FromHtml("#" + arregloDeColores[contadorDeColor]);
                TITULO_MAESTRA_ROTACION = filaTabla["TITULO_MAESTRA_ROTACION"].ToString().Trim();
            }

            GridViewRow filaGrilla = GridView_MotivosRotacion.Rows[i];

            CheckBox check = filaGrilla.FindControl("CheckBox_Configurado") as CheckBox;

            if (DBNull.Value.Equals(filaTabla["ID_ROTACION_EMPRESA"]) == false)
            {
                check.Checked = true;
            }

            if (TITULO_MAESTRA_ROTACION == filaTabla["TITULO_MAESTRA_ROTACION"].ToString().Trim())
            {
                filaGrilla.Cells[0].BackColor = colorActual;
            }
            else
            {
                contadorDeColor += 1;
                if (contadorDeColor >= arregloDeColores.Length)
                {
                    contadorDeColor = 0;
                }
                TITULO_MAESTRA_ROTACION = filaTabla["TITULO_MAESTRA_ROTACION"].ToString().Trim();
                colorActual = System.Drawing.ColorTranslator.FromHtml("#" + arregloDeColores[contadorDeColor]);

                filaGrilla.Cells[0].BackColor = colorActual;
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

    private void cargar_GridView_MotivosRotacion()
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        MotivoRotacionRetiro _motivo = new MotivoRotacionRetiro(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaCat = _motivo.ObtenerAsociacionMotivosEmpresa(ID_EMPRESA);

        if (tablaCat.Rows.Count <= 0)
        {
            if (_motivo.MensajeError == null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se han configurado las categorías y motivos de rotación y retiro.", Proceso.Advertencia);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _motivo.MensajeError, Proceso.Error);
            }

            GridView_MotivosRotacion.DataSource = null;
            GridView_MotivosRotacion.DataBind();
        }
        else
        {
            CargarGrillaMotivosRotacionDesdeTabla(tablaCat);

            inhabilitarFilasGrilla(GridView_MotivosRotacion, 0);
        }
    }

    private void CargarIdEmpresa()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

        HiddenField_ID_EMPRESA.Value = ID_EMPRESA.ToString();
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:

                CargarIdEmpresa();

                cargar_GridView_MotivosRotacion();
                break;
        }
    }

    private void Iniciar()
    {
        Configurar();
        Ocultar(Acciones.Inicio);
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

    private void Activar(Acciones accion)
    { 
        switch(accion)
        {
            case Acciones.Modificar:
                habilitarFilasGrilla(GridView_MotivosRotacion, 0);
                break;
        }
    }

    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Modificar);
        Mostrar(Acciones.Modificar);
        Activar(Acciones.Modificar);
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        if (GridView_MotivosRotacion.Rows.Count <= 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar por lo menos un Motivo de Rotación y Retiro.", Proceso.Advertencia);
        }
        else
        {
            List<MotivoRotacionEmpresa> listaMotivosRotacionAsociados = new List<MotivoRotacionEmpresa>();

            for (int i = 0; i < GridView_MotivosRotacion.Rows.Count; i++)
            {
                GridViewRow filaGrilla = GridView_MotivosRotacion.Rows[i];

                CheckBox check = filaGrilla.FindControl("CheckBox_Configurado") as CheckBox;

                if (check.Checked == true)
                {
                    MotivoRotacionEmpresa _motivoParaLista = new MotivoRotacionEmpresa();

                    _motivoParaLista.ACTIVO = true;
                    _motivoParaLista.ID_DETALLE_ROTACION = Convert.ToDecimal(GridView_MotivosRotacion.DataKeys[i].Values["ID_DETALLE_ROTACION"]);
                    _motivoParaLista.ID_EMPRESA = ID_EMPRESA;

                    Decimal ID_ROTACION_EMPRESA = 0;
                    if (GridView_MotivosRotacion.DataKeys[i].Values["ID_ROTACION_EMPRESA"].ToString().Trim() != "")
                    {
                        ID_ROTACION_EMPRESA = Convert.ToDecimal(GridView_MotivosRotacion.DataKeys[i].Values["ID_ROTACION_EMPRESA"]);
                    }
                    _motivoParaLista.ID_ROTACION_EMPRESA = ID_ROTACION_EMPRESA;

                    listaMotivosRotacionAsociados.Add(_motivoParaLista);
                }
            }

            MotivoRotacionRetiro _motivo = new MotivoRotacionRetiro(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            Boolean verificado = _motivo.ActualizarMotivosRotacionEmpresa(ID_EMPRESA, listaMotivosRotacionAsociados);

            if (verificado == false)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _motivo.MensajeError, Proceso.Error);
            }
            else
            {
                Ocultar(Acciones.Inicio);
                Mostrar(Acciones.Inicio);
                Cargar(Acciones.Inicio);

                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Los motivos de Rotación y Retiro asociados a la empresa se actualizaron correctamente.", Proceso.Correcto);
            }
        }
    }

    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }
}