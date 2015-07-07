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
using Brainsbits.LLB.programasRseGlobal;
using Brainsbits.LLB.comercial;
using Brainsbits.LLB.seguridad;

public partial class _BienestarSocial : System.Web.UI.Page
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
    private String img_area = "operaciones";
    private String nombre_area = "OPERACIONES";
    private Int32 proceso = (int)tabla.proceso.ContactoOperaciones;
    private Programa.Areas AREA_PROGRAMA = Programa.Areas.OPERACIONES;
    private String page_header = "HOJA DE TRABAJO -OPERACIONES-";

    private System.Drawing.Color colorAmarillo = System.Drawing.ColorTranslator.FromHtml("#FFF200");
    private System.Drawing.Color colorVerde = System.Drawing.ColorTranslator.FromHtml("#50CE04");
    private System.Drawing.Color colorRojo = System.Drawing.ColorTranslator.FromHtml("#F8523A");
    private System.Drawing.Color colorGris = System.Drawing.ColorTranslator.FromHtml("#cccccc");

    private String dirIconAprobadoEnEspera = "~/imagenes/IconosCalendario/IconAprobadoEnEspera.png";
    private String convencionAprobadoEnEspera = "Actividad creada correctamente, a la espera de ser ejecutada.";

    private String dirIconCanceladoCliente = "~/imagenes/IconosCalendario/IconCanceladoCliente.png";
    private String convencionCanceladoClinte = "Actividad cancelada por cliente.";

    private String dirIconCanceladoSertempo = "~/imagenes/IconosCalendario/IconCanceladoSertempo.png";
    private String convencionCanceladoSertempo = "Actividad cancelada por Empleador.";

    private String dirIconCorrecto = "~/imagenes/IconosCalendario/IconCorrecto.png";
    private String convencionCorrecto = "Actividad ejecutada correctamente y el encargado ya registró en el sistema los resultados.";

    private String dirIconNoEjecutada = "~/imagenes/IconosCalendario/IconNoEjecutada.png";
    private String convencionNoEjecutada = "(1)La actividad debe ser ejecutada hoy ó (2) la actividad ya se ejecutó pero el encargado no ha reportado los resultados.";

    private String dirIconReprogCancCliente = "~/imagenes/IconosCalendario/IconReprogCancCliente.png";
    private String convencionReprogCancCliente = "La actividad fue cancelada por el cliente, y fue reprogramada una ó más veces.";

    private String dirIconReprogCancSertempo = "~/imagenes/IconosCalendario/IconReprogCancSertempo.png";
    private String convencionReprogCancSertempo = "La actividad fue cancelada por el empleador, y fue reprogramada una ó más veces.";

    private String dirIconReprogCorrecto = "~/imagenes/IconosCalendario/IconReprogCorrecto.png";
    private String convencionReprogCorrecto = "La actividad fue creada correctamente, el encargado registró resultados, y esta actividad fue reprogramada una ó más veces.";

    private String dirIconReprogEspera = "~/imagenes/IconosCalendario/IconReprogEspera.png";
    private String convencionReprogEspera = "La actividad se encuentra a la espera de ser ejecutada, y esta actividad fue reprogramada una ó más veces.";

    private String dirIconReprogNoEjecutada = "~/imagenes/IconosCalendario/IconReprogNoEjecutada.png";
    private String convencionReprogNoEjecutada = "A la actividad no se le han registrado resultados, y además fue reprogramada una ó más veces.";

    private String dirIconReprogramado = "~/imagenes/IconosCalendario/IconReprogramado.png";
    private String convencionReprogramado = "Actividad Reprogramada.";

    private enum Acciones
    { 
        Inicio = 0
    }

    private enum Listas
    {
        Meses = 1,
        Regionales,
        Ciudaddes,
        Empresas,
        Encargados,
        EstadosActividad
    }

    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_BOTONES_INTERNOS.Visible = false;
                Panel_GRILLA_CALENDARIO.Visible = false;
                break;
        }
    }

    private void Mostrar(Acciones accion)
    { 
        switch(accion)
        {
            case Acciones.Inicio:
                Panel_BOTONES_INTERNOS.Visible = true;
                Panel_GRILLA_CALENDARIO.Visible = true;
                break;
        }
    }

    private void cargar_menu_botones_modulos_internos()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        QueryStringSeguro["img_area"] = img_area;
        QueryStringSeguro["nombre_area"] = nombre_area;
        QueryStringSeguro["accion"] = "inicial";

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
        link.ID = "link_programa_general";
        QueryStringSeguro["nombre_modulo"] = "PROGRAMA GENERAL";
        QueryStringSeguro["proceso"] = proceso.ToString();
        link.NavigateUrl = "~/maestros/programaGeneral.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bAdminProgramaGeneralEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bAdminProgramaGeneralAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bAdminProgramaGeneralEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);





        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_2_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_presupuestos";
        QueryStringSeguro["nombre_modulo"] = "ASIGNACIÓN DE PRESUPUESTO";
        QueryStringSeguro["proceso"] = proceso.ToString();
        link.NavigateUrl = "~/maestros/PresupuestoPrograma.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bAsignacionPresupuestosEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bAsignacionPresupuestosAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bAsignacionPresupuestosEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);






        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_3_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_programasyactividades";
        QueryStringSeguro["nombre_modulo"] = "ADMINISTRACIÓN PROGRAMA ESPECIFICO POR EMPRESA";
        QueryStringSeguro["proceso"] = proceso.ToString();

        link.NavigateUrl = "~/maestros/AdminProgramasActividades.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bAdminProgramasActiviadesEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bAdminProgramasActiviadesAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bAdminProgramasActiviadesEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);







        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_4_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_hojaVida";
        QueryStringSeguro["nombre_modulo"] = "HOJA DE VIDA DEL COLABORADOR";
        link.NavigateUrl = "~/contratacion/hojaVida.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bHojaVidaEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bHojaVidaAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bHojaVidaEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);







        celdaTabla = new TableCell();
        celdaTabla.ID = "cell_5_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_informe_programa_clientes";
        QueryStringSeguro["nombre_modulo"] = "GENERACIÓN DE INFORME PARA CLIENTES";
        QueryStringSeguro["proceso"] = proceso.ToString();

        link.NavigateUrl = "~/maestros/InformesClientes.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bInformeProgramaClientesEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bInformeProgramaClientesAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bInformeProgramaClientesEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);


        Table_MENU.Rows.Add(filaTabla);








        contadorFilas = 0;

        filaTabla = new TableRow();
        filaTabla.ID = "menu1_row_" + contadorFilas.ToString();









        celdaTabla = new TableCell();
        celdaTabla.ID = "m1_cell_1_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_compromisos";
        QueryStringSeguro["nombre_modulo"] = "COMPROMISOS";
        QueryStringSeguro["proceso"] = proceso.ToString();
        link.NavigateUrl = "~/maestros/Compromisos.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bCompromisosEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bCompromisosAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bCompromisosEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);






        celdaTabla = new TableCell();
        celdaTabla.ID = "m1_cell_2_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_envio_documentacion";
        QueryStringSeguro["nombre_modulo"] = "ENVÍO DE DOCUMENTACIÓN A CLIENTES";
        link.NavigateUrl = "~/Operaciones/envioDocumentacionClientes.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bDocumentacionClientesEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bDocumentacionClientesAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bDocumentacionClientesEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);






        celdaTabla = new TableCell();
        celdaTabla.ID = "m1_cell_3_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_manual_servicio";
        QueryStringSeguro["nombre_modulo"] = "GENERAR MANUAL DE SERVICIO";
        link.NavigateUrl = "~/Operaciones/ManualServicio.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bManualServicioEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bManualServicioAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bManualServicioEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);






        celdaTabla = new TableCell();
        celdaTabla.ID = "m1_cell_4_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_administracion";
        QueryStringSeguro["nombre_modulo"] = "ADMINISTRACIÓN";
        QueryStringSeguro["proceso"] = proceso.ToString();
        link.NavigateUrl = "~/Operaciones/administracion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.CssClass = "botones_menu_principal";
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bMenuAdministracionEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bMenuAdministracionAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bMenuAdministracionEstandar.png'");
        imagen.CssClass = "botones_menu_principal";
        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);

        




        celdaTabla = new TableCell();
        celdaTabla.ID = "m1_cell_5_row_" + contadorFilas.ToString();
        link = new HyperLink();
        link.ID = "link_reportes";
        QueryStringSeguro["nombre_modulo"] = "REPORTES";
        QueryStringSeguro["proceso"] = proceso.ToString();
        link.NavigateUrl = "~/Reportes/operaciones.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
        link.Target = "_blank";
        imagen = new Image();
        imagen.ImageUrl = "~/imagenes/areas/bReportesEstandar.png";
        imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bReportesAccion.png'");
        imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bReportesEstandar.png'");

        imagen.CssClass = "botones_menu_principal";
        link.CssClass = "botones_menu_principal";

        link.Controls.Add(imagen);

        celdaTabla.Controls.Add(link);

        filaTabla.Cells.Add(celdaTabla);



        Table_MENU_1.Rows.Add(filaTabla);
    }

    private void Cargar(Listas lista, DropDownList drop)
    {
        switch (lista)
        { 
            case Listas.Meses:
                drop.Items.Clear();

                drop.Items.Add(new ListItem("Enero", "1"));
                drop.Items.Add(new ListItem("Febrero", "2"));
                drop.Items.Add(new ListItem("Marzo", "3"));
                drop.Items.Add(new ListItem("Abril", "4"));
                drop.Items.Add(new ListItem("Mayo", "5"));
                drop.Items.Add(new ListItem("Junio", "6"));
                drop.Items.Add(new ListItem("Julio", "7"));
                drop.Items.Add(new ListItem("Agosto", "8"));
                drop.Items.Add(new ListItem("Septiembre", "9"));
                drop.Items.Add(new ListItem("Octubre", "10"));
                drop.Items.Add(new ListItem("Noviembre", "11"));
                drop.Items.Add(new ListItem("Diciembre", "12"));

                drop.DataBind();
                break;
            case Listas.Regionales:
                regional _reg = new regional(Session["idEmpresa"].ToString());

                DataTable tablaRegionales = _reg.ObtenerTodasLasRegionales();

                drop.Items.Add(new ListItem("Todas", "*"));

                foreach (DataRow filaTabla in tablaRegionales.Rows)
                {
                    drop.Items.Add(new ListItem(filaTabla["NOMBRE"].ToString(), filaTabla["ID_REGIONAL"].ToString()));
                }
                break;
            case Listas.Empresas:
                cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

                DataTable tablaEmpresas = _cliente.ObtenerTodasLasEmpresasActivas();

                drop.Items.Add(new ListItem("Todas", "*"));

                foreach (DataRow filaTabla in tablaEmpresas.Rows)
                {
                    drop.Items.Add(new ListItem(filaTabla["RAZ_SOCIAL"].ToString(), filaTabla["ID_EMPRESA"].ToString()));
                }

                break;
            case Listas.Encargados:

                Programa _prog = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

                DataTable tablaEncargados = _prog.ObtenerUsuariosSistemaActivos();

                drop.Items.Add(new ListItem("Todos", "*"));

                foreach (DataRow fila in tablaEncargados.Rows)
                {
                    drop.Items.Add(new ListItem(fila["NOMBRE_USUARIO"].ToString(), fila["USU_LOG"].ToString()));
                }
                break;
            case Listas.EstadosActividad:
                drop.Items.Add(new ListItem("Todos", "*"));
                drop.Items.Add(new ListItem(Programa.EstadosDetalleActividad.APROBADA.ToString(), Programa.EstadosDetalleActividad.APROBADA.ToString()));
                drop.Items.Add(new ListItem(Programa.EstadosDetalleActividad.CANCELADA.ToString(), Programa.EstadosDetalleActividad.CANCELADA.ToString()));
                drop.Items.Add(new ListItem(Programa.EstadosDetalleActividad.CREADA.ToString(), Programa.EstadosDetalleActividad.CREADA.ToString()));
                drop.Items.Add(new ListItem(Programa.EstadosDetalleActividad.TERMINADA.ToString(), Programa.EstadosDetalleActividad.TERMINADA.ToString()));
                break;
        }
    }

    private void CargarAnnoActual()
    {
        TextBox_Anno.Text = DateTime.Now.Year.ToString();
    }

    private Int32 ObtienerDiaSemana(DateTime fecha)
    {
        if ((fecha.DayOfWeek.ToString().ToUpper() == "LUNES") || (fecha.DayOfWeek.ToString().ToUpper() == "MONDAY"))
        {
            return 1;
        }
        else
        {
            if ((fecha.DayOfWeek.ToString().ToUpper() == "MARTES") || (fecha.DayOfWeek.ToString().ToUpper() == "TUESDAY"))
            {
                return 2;
            }
            else
            {
                if ((fecha.DayOfWeek.ToString().ToUpper() == "MIERCOLES") || (fecha.DayOfWeek.ToString().ToUpper() == "WEDNESDAY"))
                {
                    return 3;
                }
                else
                {   
                    if ((fecha.DayOfWeek.ToString().ToUpper() == "JUEVES") || (fecha.DayOfWeek.ToString().ToUpper() == "THURSDAY"))
                    {
                        return 4;
                    }
                    else
                    {
                        if ((fecha.DayOfWeek.ToString().ToUpper() == "VIERNES") || (fecha.DayOfWeek.ToString().ToUpper() == "FRIDAY"))
                        {
                            return 5;
                        }
                        else
                        {
                            if ((fecha.DayOfWeek.ToString().ToUpper() == "SABADO") || (fecha.DayOfWeek.ToString().ToUpper() == "SATURDAY"))
                            {
                                return 6;
                            }
                            else
                            {
                                return 0;
                            }
                        }
                    }
                }
            }   
        }
    }

    private DataTable ObtenerTablaParaGrillaCalendario()
    {
        DataTable tablaTemp = new DataTable();

        tablaTemp.Columns.Add("domingo");
        tablaTemp.Columns.Add("lunes");
        tablaTemp.Columns.Add("martes");
        tablaTemp.Columns.Add("miercoles");
        tablaTemp.Columns.Add("jueves");
        tablaTemp.Columns.Add("viernes");
        tablaTemp.Columns.Add("sabado");
        tablaTemp.Columns.Add("semana");

        return tablaTemp;
    }

    private void DibujarGrillaActividades(GridView grilla, DataTable tablaActividades)
    {
        grilla.DataSource = tablaActividades;
        grilla.DataBind();

        for (int i = 0; i < grilla.Rows.Count; i++)
        {
            tools _tools = new tools();
            SecureQueryString QueryStringSeguro;
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

            QueryStringSeguro["img_area"] = img_area;
            QueryStringSeguro["nombre_area"] = nombre_area;
            QueryStringSeguro["nombre_modulo"] = "VISOR DE ACTIVIDADES";
            QueryStringSeguro["accion"] = "inicial";

            DataRow filaTabla = tablaActividades.Rows[i];

            QueryStringSeguro["id_detalle"] = filaTabla["ID_DETALLE"].ToString().Trim();

            QueryStringSeguro["proceso"] = QueryStringSeguro["proceso"] = proceso.ToString();

            HyperLink linkTituloActividad = grilla.Rows[i].FindControl("HyperLink_NombreActividad") as HyperLink;
            linkTituloActividad.Text = filaTabla["NOMBRE"].ToString().Trim();
            linkTituloActividad.NavigateUrl = "~/maestros/visorActividades.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            linkTituloActividad.ToolTip = filaTabla["NOMBRE_CIUDAD"].ToString().Trim() + " / " + filaTabla["HORA_INICIO"].ToString().Trim() + " a " + filaTabla["HORA_FIN"].ToString().Trim();

            HyperLink linkHorariosActividad = grilla.Rows[i].FindControl("HyperLink_HorarioActividad") as HyperLink;
            linkHorariosActividad.Text = "Desde:<br>" + filaTabla["HORA_INICIO"].ToString().Trim() + "<br>Hasta:<br>" + filaTabla["HORA_FIN"].ToString().Trim();
            linkHorariosActividad.NavigateUrl = "~/maestros/visorActividades.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            linkHorariosActividad.ToolTip = filaTabla["NOMBRE_CIUDAD"].ToString().Trim() + " / " + filaTabla["HORA_INICIO"].ToString().Trim() + " a " + filaTabla["HORA_FIN"].ToString().Trim();

            HyperLink link = grilla.Rows[i].FindControl("HyperLink_Atividad") as HyperLink;
            link.Text = filaTabla["RAZ_SOCIAL"].ToString() + " - " + filaTabla["NOMBRE_CIUDAD"].ToString().Trim();
            link.NavigateUrl = "~/maestros/visorActividades.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.ToolTip = filaTabla["NOMBRE_CIUDAD"].ToString().Trim() + " / " + filaTabla["HORA_INICIO"].ToString().Trim() + " a " + filaTabla["HORA_FIN"].ToString().Trim();


            Image imagen = grilla.Rows[i].FindControl("Image_EstadoActividad") as Image;

            if (filaTabla["ID_ESTADO"].ToString().Trim() == Programa.EstadosDetalleActividad.CREADA.ToString())
            {
            }
            else
            {
                if (filaTabla["ID_ESTADO"].ToString().Trim() == Programa.EstadosDetalleActividad.APROBADA.ToString())
                {

                    if (Convert.ToInt32(filaTabla["CONTADOR_REPROGRAMACIONES"]) <= 0)
                    {
                        if (Convert.ToDateTime(Convert.ToDateTime(filaTabla["FECHA_ACTIVIDAD"]).ToShortDateString()) <= Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                        {
                            imagen.ImageUrl = dirIconNoEjecutada;
                            imagen.ToolTip = convencionNoEjecutada;
                        }
                        else
                        {
                            imagen.ImageUrl = dirIconAprobadoEnEspera;
                            imagen.ToolTip = convencionAprobadoEnEspera;
                        }
                    }
                    else
                    {
                        if (Convert.ToDateTime(Convert.ToDateTime(filaTabla["FECHA_ACTIVIDAD"]).ToShortDateString()) <= Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                        {
                            imagen.ImageUrl = dirIconReprogNoEjecutada;
                            imagen.ToolTip = convencionReprogNoEjecutada;
                        }
                        else
                        {
                            imagen.ImageUrl = dirIconReprogEspera;
                            imagen.ToolTip = convencionReprogEspera;
                        }
                    }
                }
                else
                {
                    if (filaTabla["ID_ESTADO"].ToString().Trim() == Programa.EstadosDetalleActividad.CANCELADA.ToString())
                    {

                        if (Convert.ToInt32(filaTabla["CONTADOR_REPROGRAMACIONES"]) <= 0)
                        {
                            if (filaTabla["TIPO_CANCELACION"].ToString().Trim().ToUpper() == "SERTEMPO")
                            {
                                imagen.ImageUrl = dirIconCanceladoSertempo;
                                imagen.ToolTip = convencionCanceladoSertempo;
                            }
                            else
                            {
                                imagen.ImageUrl = dirIconCanceladoCliente;
                                imagen.ToolTip = convencionCanceladoClinte;
                            }
                        }
                        else
                        {
                            if (filaTabla["TIPO_CANCELACION"].ToString().Trim().ToUpper() == "SERTEMPO")
                            {
                                imagen.ImageUrl = dirIconReprogCancSertempo;
                                imagen.ToolTip = convencionReprogCancSertempo;
                            }
                            else
                            {
                                imagen.ImageUrl = dirIconReprogCancCliente;
                                imagen.ToolTip = convencionReprogCancCliente;
                            }
                        }
                    }
                    else
                    {
                        if (Convert.ToInt32(filaTabla["CONTADOR_REPROGRAMACIONES"]) <= 0)
                        {
                            imagen.ImageUrl = dirIconCorrecto;
                            imagen.ToolTip = convencionCorrecto;
                        }
                        else
                        {
                            imagen.ImageUrl = dirIconReprogCorrecto;
                            imagen.ToolTip = convencionReprogCorrecto;
                        }
                    }
                }
            }
        }
    }

    private void CargarGrillaCalendarioDesdeTabla(DataTable tablaParaGrilla)
    {
        GridView_CALENDARIO.DataSource = tablaParaGrilla;
        GridView_CALENDARIO.DataBind();

        Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        for (int i = 0; i < tablaParaGrilla.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_CALENDARIO.Rows[i];
            DataRow filaTabla = tablaParaGrilla.Rows[i];

            String diaDomingo = filaTabla["domingo"].ToString().Split('/')[0];
            Label labelDiaDomingoGrilla = filaGrilla.FindControl("Label_NumeroDiaDomingo") as Label;
            labelDiaDomingoGrilla.Text = diaDomingo;
            GridView grillaDomingo = filaGrilla.FindControl("GridView_ActividadesDomingo") as GridView;
            DataTable tablaActividadesDomingo = _programa.ObtenerDetalleActividadesPorFecha(Convert.ToDateTime(filaTabla["domingo"]), AREA_PROGRAMA, DropDownList_Encargado.SelectedValue, DropDownList_Empresa.SelectedValue, DropDownList_Regional.SelectedValue, DropDownList_Ciudad.SelectedValue, DropDownList_EstadoActividad.SelectedValue);
            DibujarGrillaActividades(grillaDomingo, tablaActividadesDomingo);

            String diaLunes = filaTabla["lunes"].ToString().Split('/')[0];
            Label labelDiaLunesGrilla = filaGrilla.FindControl("Label_NumeroDiaLunes") as Label;
            labelDiaLunesGrilla.Text = diaLunes;
            GridView grillaLunes = filaGrilla.FindControl("GridView_ActividadesLunes") as GridView;
            DataTable tablaActividadesLunes = _programa.ObtenerDetalleActividadesPorFecha(Convert.ToDateTime(filaTabla["lunes"]), AREA_PROGRAMA, DropDownList_Encargado.SelectedValue, DropDownList_Empresa.SelectedValue, DropDownList_Regional.SelectedValue, DropDownList_Ciudad.SelectedValue, DropDownList_EstadoActividad.SelectedValue);
            DibujarGrillaActividades(grillaLunes, tablaActividadesLunes);

            String diaMartes = filaTabla["martes"].ToString().Split('/')[0];
            Label labelDiaMartesGrilla = filaGrilla.FindControl("Label_NumeroDiaMartes") as Label;
            labelDiaMartesGrilla.Text = diaMartes;
            GridView grillaMartes = filaGrilla.FindControl("GridView_ActividadesMartes") as GridView;
            DataTable tablaActividadesMartes = _programa.ObtenerDetalleActividadesPorFecha(Convert.ToDateTime(filaTabla["martes"]), AREA_PROGRAMA, DropDownList_Encargado.SelectedValue, DropDownList_Empresa.SelectedValue, DropDownList_Regional.SelectedValue, DropDownList_Ciudad.SelectedValue, DropDownList_EstadoActividad.SelectedValue);
            DibujarGrillaActividades(grillaMartes, tablaActividadesMartes);

            String diaMiercoles = filaTabla["miercoles"].ToString().Split('/')[0];
            Label labelDiaMiercolesGrilla = filaGrilla.FindControl("Label_NumeroDiaMiercoles") as Label;
            labelDiaMiercolesGrilla.Text = diaMiercoles;
            GridView grillaMiercoles = filaGrilla.FindControl("GridView_ActividadesMiercoles") as GridView;
            DataTable tablaActividadesMiercoles = _programa.ObtenerDetalleActividadesPorFecha(Convert.ToDateTime(filaTabla["miercoles"]), AREA_PROGRAMA, DropDownList_Encargado.SelectedValue, DropDownList_Empresa.SelectedValue, DropDownList_Regional.SelectedValue, DropDownList_Ciudad.SelectedValue, DropDownList_EstadoActividad.SelectedValue);
            DibujarGrillaActividades(grillaMiercoles, tablaActividadesMiercoles);

            String diaJueves = filaTabla["jueves"].ToString().Split('/')[0];
            Label labelDiaJuevesGrilla = filaGrilla.FindControl("Label_NumeroDiaJueves") as Label;
            labelDiaJuevesGrilla.Text = diaJueves;
            GridView grillaJueves = filaGrilla.FindControl("GridView_ActividadesJueves") as GridView;
            DataTable tablaActividadesJueves = _programa.ObtenerDetalleActividadesPorFecha(Convert.ToDateTime(filaTabla["jueves"]), AREA_PROGRAMA, DropDownList_Encargado.SelectedValue, DropDownList_Empresa.SelectedValue, DropDownList_Regional.SelectedValue, DropDownList_Ciudad.SelectedValue, DropDownList_EstadoActividad.SelectedValue);
            DibujarGrillaActividades(grillaJueves, tablaActividadesJueves);

            String diaViernes = filaTabla["viernes"].ToString().Split('/')[0];
            Label labelDiaViernesGrilla = filaGrilla.FindControl("Label_NumeroDiaViernes") as Label;
            labelDiaViernesGrilla.Text = diaViernes;
            GridView grillaViernes = filaGrilla.FindControl("GridView_ActividadesViernes") as GridView;
            DataTable tablaActividadesViernes = _programa.ObtenerDetalleActividadesPorFecha(Convert.ToDateTime(filaTabla["viernes"]), AREA_PROGRAMA, DropDownList_Encargado.SelectedValue, DropDownList_Empresa.SelectedValue, DropDownList_Regional.SelectedValue, DropDownList_Ciudad.SelectedValue, DropDownList_EstadoActividad.SelectedValue);
            DibujarGrillaActividades(grillaViernes, tablaActividadesViernes);

            String diaSabado = filaTabla["sabado"].ToString().Split('/')[0];
            Label labelDiaSabadoGrilla = filaGrilla.FindControl("Label_NumeroDiaSabado") as Label;
            labelDiaSabadoGrilla.Text = diaSabado;
            GridView grillaSabado = filaGrilla.FindControl("GridView_ActividadesSabado") as GridView;
            DataTable tablaActividadesSabado = _programa.ObtenerDetalleActividadesPorFecha(Convert.ToDateTime(filaTabla["sabado"]), AREA_PROGRAMA, DropDownList_Encargado.SelectedValue, DropDownList_Empresa.SelectedValue, DropDownList_Regional.SelectedValue, DropDownList_Ciudad.SelectedValue, DropDownList_EstadoActividad.SelectedValue);
            DibujarGrillaActividades(grillaSabado, tablaActividadesSabado);
            
        }
    }

    private void CargarMesEnGrilla()
    {
        DataTable tablaParaGrilla = ObtenerTablaParaGrillaCalendario();

        Int32 diasEnMes = DateTime.DaysInMonth(Convert.ToInt32(TextBox_Anno.Text), Convert.ToInt32(DropDownList_Mes.SelectedValue));
        Int32 QueDiaCaeElPrimeroDelMes = ObtienerDiaSemana(new DateTime(Convert.ToInt32(TextBox_Anno.Text), Convert.ToInt32(DropDownList_Mes.SelectedValue), 1));

        DateTime fechaCalendarioActual = new DateTime(Convert.ToInt32(TextBox_Anno.Text), Convert.ToInt32(DropDownList_Mes.SelectedValue), 1);
        DateTime fechaCalendarioAnterior = fechaCalendarioActual.AddMonths(-1);
        Int32 diasEnMesAnterior = DateTime.DaysInMonth(fechaCalendarioAnterior.Year, fechaCalendarioAnterior.Month);
        Int32 annoAnterior = fechaCalendarioAnterior.Year;
        Int32 mesAnterior = fechaCalendarioAnterior.Month;

        DataRow filaGrilla = tablaParaGrilla.NewRow();
     
        Int32 contadorDeDiasSemana = 0;
        Int32 contadorSemanasDelMes = 0;

        filaGrilla["semana"] = contadorSemanasDelMes;

        if(QueDiaCaeElPrimeroDelMes > 0)
        {
            for(int i = diasEnMesAnterior - (ObtienerDiaSemana(fechaCalendarioActual) -1); i <= diasEnMesAnterior; i++)
            {
                DateTime fechaARecorrer = new DateTime(annoAnterior, mesAnterior, i);

                filaGrilla[contadorDeDiasSemana] = fechaARecorrer.ToShortDateString();

                contadorDeDiasSemana += 1;
            }
        }

        for (int i = 1; i <= diasEnMes; i++)
        {
            DateTime fechaARecorrer = new DateTime(Convert.ToInt32(TextBox_Anno.Text), Convert.ToInt32(DropDownList_Mes.SelectedValue), i);

            filaGrilla[contadorDeDiasSemana] = fechaARecorrer.ToShortDateString();

            if (contadorDeDiasSemana >= 6)
            {
                tablaParaGrilla.Rows.Add(filaGrilla);
                tablaParaGrilla.AcceptChanges();

                filaGrilla = tablaParaGrilla.NewRow();

                contadorSemanasDelMes += 1;

                filaGrilla["semana"] = contadorSemanasDelMes;

                contadorDeDiasSemana = 0;
            }
            else
            {
                contadorDeDiasSemana += 1;
            }
        }

        DateTime fechaCalendarioSiguiente = fechaCalendarioActual.AddMonths(1);
        Int32 annoSiguiente = fechaCalendarioSiguiente.Year;
        Int32 mesSiguiente = fechaCalendarioSiguiente.Month;
        if(contadorDeDiasSemana > 0)
        {
            Int32 diasRestantesCalendario = 7 - contadorDeDiasSemana;
            for (int i = 1; i <= diasRestantesCalendario; i++)
            {
                DateTime fechaARecorrer = new DateTime(annoSiguiente, mesSiguiente, i);

                filaGrilla[contadorDeDiasSemana] = fechaARecorrer.ToShortDateString();

                contadorDeDiasSemana += 1;
            }

            tablaParaGrilla.Rows.Add(filaGrilla);
            tablaParaGrilla.AcceptChanges();
        } 
  
        CargarGrillaCalendarioDesdeTabla(tablaParaGrilla);
    }

    private void CargarMesActual()
    {
        int mesActual = DateTime.Now.Month;

        DropDownList_Mes.SelectedValue = mesActual.ToString();
    }

    private void CargarGrillaInicial()
    {
        Cargar(Listas.Meses, DropDownList_Mes);

        Cargar(Listas.Regionales, DropDownList_Regional);
        CargarDropDownList_Vacio(DropDownList_Ciudad);
        Cargar(Listas.Empresas, DropDownList_Empresa);
        Cargar(Listas.Encargados, DropDownList_Encargado);
        Cargar(Listas.EstadosActividad, DropDownList_EstadoActividad);

        CargarAnnoActual();
        CargarMesActual();

        CargarMesEnGrilla();
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:

                CargarGrillaInicial();
                break;
        }
    }

    private void Iniciar()
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = page_header;

        cargar_menu_botones_modulos_internos();

        if (IsPostBack == false)
        {
            Iniciar();
        }
    }

    private void AumentarUnMes()
    {
        Int32 anno = 2012;

        try
        {
            anno = Convert.ToInt32(TextBox_Anno.Text);
        }
        catch
        {
            anno = 2012;
        }

        if (DropDownList_Mes.SelectedValue == "12")
        {
            DropDownList_Mes.SelectedValue = "1";
            anno += 1;
            if (anno > 3000)
            {
                anno = 3000;
                DropDownList_Mes.SelectedValue = "12";
            }
            TextBox_Anno.Text = anno.ToString();
        }
        else
        {
            DropDownList_Mes.SelectedIndex = DropDownList_Mes.SelectedIndex + 1;
        }

        CargarMesEnGrilla();
    }

    protected void Button_Siguiente_Click(object sender, EventArgs e)
    {
        AumentarUnMes();
    }

    private void DisminuirUnMes()
    {
        Int32 anno = 2012;

        try
        {
            anno = Convert.ToInt32(TextBox_Anno.Text);
        }
        catch
        {
            anno = 2012;
        }

        if (DropDownList_Mes.SelectedValue == "1")
        {
            DropDownList_Mes.SelectedValue = "12";
            anno -= 1;
            if (anno < 2012)
            {
                anno = 2012;
                DropDownList_Mes.SelectedValue = "1";
            }
            TextBox_Anno.Text = anno.ToString();
        }
        else
        {
            DropDownList_Mes.SelectedIndex = DropDownList_Mes.SelectedIndex - 1;
        }

        CargarMesEnGrilla();
    }

    protected void Button_Anterior_Click(object sender, EventArgs e)
    {
        DisminuirUnMes();
    }

    protected void DropDownList_Mes_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarMesEnGrilla();
    }

    protected void TextBox_Anno_TextChanged(object sender, EventArgs e)
    {
        CargarMesEnGrilla();
    }

    private void CargarDropDownList_Vacio(DropDownList drop)
    {
        drop.Items.Clear();
        drop.Items.Add(new ListItem("Todos", "*"));
        drop.DataBind();
    }

    private void Cargar(DropDownList dropDownList, DataTable dataTable, Boolean conTodos)
    {
        dropDownList.Items.Clear();

        if (conTodos == true)
        {
            dropDownList.Items.Add(new ListItem("Todos", "*"));
        }

        foreach (DataRow dataRow in dataTable.Rows)
        {
            dropDownList.Items.Add(new ListItem(dataRow["nombre"].ToString(), dataRow["id"].ToString()));
        }

        dropDownList.DataBind();
    }

    protected void DropDownList_Regional_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_Regional.SelectedValue == "*")
        {
            CargarDropDownList_Vacio(DropDownList_Ciudad);
        }
        else
        {
            Reporte _reporte = new Reporte(Session["idEmpresa"].ToString());
            Cargar(DropDownList_Ciudad, _reporte.ListarCiudadesPorRegional(DropDownList_Regional.SelectedValue), true);
        }

        CargarMesEnGrilla();
    }
    protected void DropDownList_Ciudad_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarMesEnGrilla();
    }
    protected void DropDownList_Empresa_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarMesEnGrilla();
    }
    protected void DropDownList_Encargado_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarMesEnGrilla();
    }
    protected void DropDownList_EstadoActividad_SelectedIndexChanged(object sender, EventArgs e)
    {
        CargarMesEnGrilla();
    }
}