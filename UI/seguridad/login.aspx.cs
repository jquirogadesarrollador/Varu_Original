using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brainsbits.LLB.maestras;
using TSHAK.Components;
using Brainsbits.LLB.Alertas;
using Brainsbits.LLB.seguridad;
using System.Data;

public partial class seguridad_Login : System.Web.UI.Page
{
    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }

    private enum Acciones
    {
        Inicio = 0,
        CambioPassword = 1,
        RecuperarPassword = 2
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

    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_PANEL_CONTENEDOR_INICIO_SESION.Visible = false;
                Panel_CONTENEDOR_RECUPERAR_PASSWORD.Visible = false;
                Panel_CAMBIO_PASSWORD_OBLIGATORIO.Visible = false;
                break;
        }
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_PANEL_CONTENEDOR_INICIO_SESION.Visible = true;
                break;
            case Acciones.CambioPassword:
                Panel_CAMBIO_PASSWORD_OBLIGATORIO.Visible = true;
                break;
            case Acciones.RecuperarPassword:
                Panel_CONTENEDOR_RECUPERAR_PASSWORD.Visible = true;
                break;
        }
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                TextBox_NombreUsuario.Text = "";
                TextBox_Pasword.Text = "";
                break;
            case Acciones.CambioPassword:
                TextBox_USU_LOG_CAMBIO.Text = "";
                TextBox_CEDULA_CAMBIO.Text = "";
                TextBox_USU_PSW_ANT_CAMBIO.Text = "";
                TextBox_USU_PSW_NUEVO_CAMBIO.Text = "";
                TextBox_CONFIRMACION_PSW_NUEVO_CAMBIO.Text = "";
                break;
            case Acciones.RecuperarPassword:
                TextBox_RecordarPSW_Usuario.Text = "";
                TextBox_Cedula.Text = "";
                DropDownList_RecordarPSW_Empresa.SelectedIndex = 0;
                break;
        }
    }

    private void Iniciar()
    {
        Configurar();

        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);

        if (Session["SESSION_CADUCADA"] != null)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La Sesión en este equipo se cerró porque no se detectó actividad sobre el sistema. Por favor vuelva a ingresar al Sistema con sus datos de acceso.", Proceso.Advertencia);
            Session.Remove("SESSION_CADUCADA");
        }
    }

    private void Configurar()
    {
        configurar_paneles_popup(Panel_FONDO_MENSAJE, Panel_MENSAJES);
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

    private void EnviarNotificaciones()
    {
        Alerta _alerta = new Alerta(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        _alerta.enviarNotificacionesObjetosContrato();
    }

    protected void Button_ACEPTAR_Click(object sender, EventArgs e)
    {
        Decimal ID_EMPRESA;

        Boolean verificador = true;

        Session.Add("idEmpresa", "1");

        ID_EMPRESA = 1;

        if (verificador == true)
        {
            String LOGON_USER = Request.ServerVariables["LOGON_USER"].ToString();
            String REMOTE_USER = Request.ServerVariables["REMOTE_USER"].ToString();
            String LOCAL_ADDR = Request.ServerVariables["LOCAL_ADDR"].ToString();
            String REMOTE_ADDR = Request.ServerVariables["REMOTE_ADDR"].ToString();
            String REMOTE_HOST = Request.ServerVariables["REMOTE_HOST"].ToString();
            String HTTP_USER_AGENT = Request.ServerVariables["HTTP_USER_AGENT"].ToString();

            usuario usuario = new usuario(ID_EMPRESA.ToString());

            if (usuario.IniciarSesion(TextBox_NombreUsuario.Text, TextBox_Pasword.Text, LOGON_USER, REMOTE_USER, LOCAL_ADDR, REMOTE_ADDR, REMOTE_HOST, HTTP_USER_AGENT) == false)
            {
                String[] mensajeError = usuario.MensajeError.Split(':');
                if (mensajeError[0] == "8")
                {
                    Ocultar(Acciones.Inicio);
                    Mostrar(Acciones.CambioPassword);
                    Cargar(Acciones.CambioPassword);

                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, mensajeError[1], Proceso.Advertencia);
                }
                else
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, mensajeError[1], Proceso.Error);
                }
            }
            else
            {
                Session.Remove("USU_LOG");
                Session.Remove("USU_ID");
                Session.Remove("USU_TIPO");
                Session.Add("USU_LOG", TextBox_NombreUsuario.Text);
                Session.Add("USU_TIPO", usuario.TipoUsuario);
                Session.Remove("NOM_EMPLEADO");
                Session.Add("NOM_EMPLEADO", usuario.NombreUsuario);

                if (usuario.TipoUsuario == "PUBLICO")
                {
                    tools _tools = new tools();
                    SecureQueryString QueryStringSeguro_seleccion;
                    QueryStringSeguro_seleccion = new SecureQueryString(_tools.byteParaQueryStringSeguro());

                    QueryStringSeguro_seleccion["img_area"] = "seleccion";
                    QueryStringSeguro_seleccion["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
                    QueryStringSeguro_seleccion["nombre_modulo"] = "HOJA TRABAJO SELECCIÓN";
                    QueryStringSeguro_seleccion["accion"] = "inicial";


                    Response.Redirect("~/seleccionCliente/hojaTrabajoSeleccion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro_seleccion.ToString()));
                }
                else
                {
                    EnviarNotificaciones();
                    Response.Redirect("~/areas/areas.aspx");
                }
            }
        }
    }

    protected void LinkButton_RecordarContraseña_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.RecuperarPassword);
        Cargar(Acciones.RecuperarPassword);
    }

    protected void Button_Ingresar_Click(object sender, EventArgs e)
    {
        Decimal ID_EMPRESA;

        if (DropDownList_RecordarPSW_Empresa.SelectedValue.Equals("1"))
        {
            Session.Remove("idEmpresa");
            Session.Add("idEmpresa", "1");
            ID_EMPRESA = 1;
        }
        else
        {
            Session.Remove("idEmpresa");
            Session.Add("idEmpresa", "3");
            ID_EMPRESA = 3;
        }

        usuario usuario = new usuario(ID_EMPRESA.ToString());

        Boolean correcto = usuario.ObtenerClaveUsuarioPorUsuLogCedula(TextBox_RecordarPSW_Usuario.Text, TextBox_Cedula.Text);

        if (!(String.IsNullOrEmpty(usuario.MensajeError)))
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, usuario.MensajeError, Proceso.Error);
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Su contraseña fue enviada al correo registrado en el sistema.", Proceso.Correcto);
        }
    }

    protected void Button_CAMBIAR_PSW_Click(object sender, EventArgs e)
    {
        String USU_LOG = TextBox_USU_LOG_CAMBIO.Text.Trim();

        Boolean correcto = true;

        usuario _usuario = new usuario(Session["idEmpresa"].ToString());

        DataTable tablaInfoUsuario = _usuario.ObtenerUsuarioPorUsuLog(USU_LOG);

        if (tablaInfoUsuario.Rows.Count <= 0)
        {
            if (_usuario.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _usuario.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Nombre de usuario no registrado, No se pudo cambiar el password.", Proceso.Advertencia);
            }

            correcto = false;
        }
        else
        {
            DataRow filaInfoUsuario = tablaInfoUsuario.Rows[0];

            correcto = _usuario.ActualizarClaveUsuarioDesdeInicioSesion(filaInfoUsuario, TextBox_CEDULA_CAMBIO.Text.Trim(), TextBox_USU_PSW_ANT_CAMBIO.Text.Trim(), TextBox_USU_PSW_NUEVO_CAMBIO.Text.Trim());

            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);

            if (correcto == false)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _usuario.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El password fue correctamente actualizado. Por favor ingrese al sistema utilizando el nuevo password.", Proceso.Correcto);
            }
        }
    }
}