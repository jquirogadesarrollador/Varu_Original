using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brainsbits.LLB.seguridad;
using System.Data;

public partial class seguridad_cambioPassword : System.Web.UI.Page
{
    private enum Acciones
    {
        Inicio = 0,
        CargarPerfil,
        NuevoPerfil,
        Modificar,
        SeleccionDeCargoSinPerfil
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


    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = false;

                Panel_FORM_BOTONES.Visible = false;
                Button_Cambiar.Visible = false;
                Button_CAMBIAR_1.Visible = false;

                Panel_Formulario.Visible = false;

                Panel_BOTONES_PIE.Visible = false;
                break;
        }
    }

    private void Desactivar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                TextBox_USU_LOG.Enabled = false;
                break;
        }
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_FORM_BOTONES.Visible = true;
                Button_Cambiar.Visible = true;

                Panel_Formulario.Visible = true;

                Panel_BOTONES_PIE.Visible = true;
                Button_CAMBIAR_1.Visible = true;
                break;
        }
    }

    private void Limpiar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                TextBox_USU_LOG.Text = "";
                TextBox_USU_MAIL.Text = "";
                TextBox_USU_PSW_ANT.Text = "";
                TextBox_USU_PSW_NEW.Text = "";
                TextBox_USU_PSW_NEW_CONF.Text = "";
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

    private void CargarDatosUsuarioConectado()
    {
        usuario _usu = new usuario(Session["idEmpresa"].ToString());

        DataTable tablaUsu = _usu.ObtenerUsuarioPorUsuLog(Session["USU_LOG"].ToString());

        if (tablaUsu.Rows.Count <= 0)
        {
            if (_usu.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _usu.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información del USUARIO seleccionado.", Proceso.Advertencia);
            }

            Panel_Formulario.Visible = false;
        }
        else
        {
            DataRow filaUsu = tablaUsu.Rows[0];

            TextBox_USU_LOG.Text = Session["USU_LOG"].ToString();

            if ((DBNull.Value.Equals(filaUsu["USU_MAIL"]) == false))
            {
                if ((filaUsu["USU_MAIL"].ToString().ToUpper() == "DESCONOCIDO@DESCONOCIDO.COM") || (filaUsu["USU_MAIL"].ToString().ToUpper() == "USUARIO@CORREO.COM"))
                {
                    TextBox_USU_MAIL.Text = "";
                }
                else
                {
                    TextBox_USU_MAIL.Text = filaUsu["USU_MAIL"].ToString();
                }
            }
            else
            {
                TextBox_USU_MAIL.Text = "";
            }

            TextBox_USU_PSW_ANT.Text = "";
            TextBox_USU_PSW_NEW.Text = "";
            TextBox_USU_PSW_NEW_CONF.Text = "";
        }
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                CargarDatosUsuarioConectado();
                break;
        }
    }

    private void Iniciar()
    {
        Configurar();
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Limpiar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Title = "CAMBIO DE PASSWORD";
        if (IsPostBack == false)
        {
            Iniciar();
        }
    }

    protected void Button_Cambiar_Click(object sender, EventArgs e)
    {
        String USU_LOG = Session["USU_LOG"].ToString();
        String USU_CEDULA = null;

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

            if (filaInfoUsuario["USU_TIPO"].ToString().ToUpper() == "PLANTA")
            {
                USU_CEDULA = filaInfoUsuario["NUM_DOC_IDENTIDAD"].ToString().Trim();
            }
            else
            {
                USU_CEDULA = filaInfoUsuario["NUM_DOC_IDENTIDAD_EXTERNO"].ToString().Trim();
            }
            
            correcto = _usuario.ActualizarClaveUsuarioDesdeInicioSesion(filaInfoUsuario, USU_CEDULA, TextBox_USU_PSW_ANT.Text.Trim(), TextBox_USU_PSW_NEW.Text);

            if (correcto == false)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _usuario.MensajeError, Proceso.Error);
            }
            else
            {
                Ocultar(Acciones.Inicio);
                Desactivar(Acciones.Inicio);
                Mostrar(Acciones.Inicio);
                Limpiar(Acciones.Inicio);
                Cargar(Acciones.Inicio);

                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El password fue correctamente actualizado. A partir deeste momento debe ingresar al sistema utilizando el nuevo password.", Proceso.Correcto);
            }
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
}