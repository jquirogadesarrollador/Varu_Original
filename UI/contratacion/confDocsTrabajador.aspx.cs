using Brainsbits.LLB.maestras;
using TSHAK.Components;
using System.Web.UI.WebControls;
using System;
using Brainsbits.LLB.contratacion;
using System.Data;
using Brainsbits.LLB;

public partial class _ConfDocsTrabajador : System.Web.UI.Page
{
    private enum Acciones
    {
        Inicio = 0,
        SinConf,
        SIN_ENTREGA,
        CON_ENTREGA,
        SEL_SIN_ENTREGA,
        SEL_CON_ENTREGA,
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

    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = false;
                Panel_FORM_BOTONES_ENCABEZADO.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_CANCELAR.Visible = false;

                Panel_SELECCION_TIPO_CONF.Visible = false;
                Panel_SELECCION_DOCUMENTOS_ENTREGABLES.Visible = false;

                Panel_BOTONES_ABAJO.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
                break;
            case Acciones.SIN_ENTREGA:
                Panel_SELECCION_DOCUMENTOS_ENTREGABLES.Visible = false;
                break;
            case Acciones.SEL_SIN_ENTREGA:
                Panel_SELECCION_DOCUMENTOS_ENTREGABLES.Visible = false;
                break;
            case Acciones.Modificar:
                Button_MODIFICAR.Visible = false;
                Button_GUARDAR.Visible = false;
                Button_CANCELAR.Visible = false;

                Button_MODIFICAR_1.Visible = false;
                Button_GUARDAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;
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

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        { 
            case Acciones.SinConf:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Panel_SELECCION_TIPO_CONF.Visible = true;

                Panel_BOTONES_ABAJO.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;

                break;
            case Acciones.CON_ENTREGA:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Button_MODIFICAR.Visible = true;

                Panel_SELECCION_TIPO_CONF.Visible = true;

                Panel_SELECCION_DOCUMENTOS_ENTREGABLES.Visible = true;

                Panel_BOTONES_ABAJO.Visible = true;
                Button_MODIFICAR_1.Visible = true;
                break;
            case Acciones.SIN_ENTREGA:
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Button_MODIFICAR.Visible = true;

                Panel_SELECCION_TIPO_CONF.Visible = true;

                Panel_BOTONES_ABAJO.Visible = true;
                Button_MODIFICAR_1.Visible = true;
                break;
            case Acciones.SEL_CON_ENTREGA:
                Panel_SELECCION_DOCUMENTOS_ENTREGABLES.Visible = true;
                break;
            case Acciones.Modificar:
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;

                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
                break;
        }

    }

    private void Limpiar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.SinConf:
                RadioButtonList_TIPO_ENTREGA.SelectedIndex = -1;

                foreach (ListItem item in CheckBoxList_DOCUMENTOS_SELECCION.Items)
                {
                    item.Selected = false;
                }

                foreach (ListItem item in CheckBoxList_DOCUEMENTOS_CONTRATACION.Items)
                {
                    item.Selected = false;
                }

                DropDownList_CONTACTO_SELECCION.Items.Clear();
                TextBox_EMAIL_SELECCION.Text = "";

                DropDownList_CONTACTO_CONTRATACION.Items.Clear();
                TextBox_EMAIL_CONTRATACION.Text = "";

                break;
            case Acciones.CON_ENTREGA:
                foreach (ListItem item in CheckBoxList_DOCUMENTOS_SELECCION.Items)
                {
                    item.Selected = false;
                }

                foreach (ListItem item in CheckBoxList_DOCUEMENTOS_CONTRATACION.Items)
                {
                    item.Selected = false;
                }

                DropDownList_CONTACTO_SELECCION.Items.Clear();
                TextBox_EMAIL_SELECCION.Text = "";

                DropDownList_CONTACTO_CONTRATACION.Items.Clear();
                TextBox_EMAIL_CONTRATACION.Text = "";
                break;
            case Acciones.SEL_CON_ENTREGA:
                foreach (ListItem item in CheckBoxList_DOCUMENTOS_SELECCION.Items)
                {
                    item.Selected = false;
                }

                foreach (ListItem item in CheckBoxList_DOCUEMENTOS_CONTRATACION.Items)
                {
                    item.Selected = false;
                }

                DropDownList_CONTACTO_SELECCION.Items.Clear();
                TextBox_EMAIL_SELECCION.Text = "";

                DropDownList_CONTACTO_CONTRATACION.Items.Clear();
                TextBox_EMAIL_CONTRATACION.Text = "";
                break;
        }

    }

    private void Activar(Acciones accion)
    { 
        switch(accion)
        {
            case Acciones.SinConf:
                RadioButtonList_TIPO_ENTREGA.Enabled = true;
                break;
            case Acciones.CON_ENTREGA:
                CheckBoxList_DOCUMENTOS_SELECCION.Enabled = true;
                DropDownList_CONTACTO_SELECCION.Enabled = true;
                CheckBoxList_DOCUEMENTOS_CONTRATACION.Enabled = true;
                DropDownList_CONTACTO_CONTRATACION.Enabled = true;
                break;
            case Acciones.SEL_CON_ENTREGA:
                CheckBoxList_DOCUMENTOS_SELECCION.Enabled = true;
                DropDownList_CONTACTO_SELECCION.Enabled = true;
                CheckBoxList_DOCUEMENTOS_CONTRATACION.Enabled = true;
                DropDownList_CONTACTO_CONTRATACION.Enabled = true;
                break;
            case Acciones.Modificar:
                RadioButtonList_TIPO_ENTREGA.Enabled = true;
                CheckBoxList_DOCUMENTOS_SELECCION.Enabled = true;
                DropDownList_CONTACTO_SELECCION.Enabled = true;
                CheckBoxList_DOCUEMENTOS_CONTRATACION.Enabled = true;
                DropDownList_CONTACTO_CONTRATACION.Enabled = true;
                break;
        }
    }

    private void SeleccionarDocsSeleccion(DataRow fila)
    {
        String[] listaDocsSeleccionados = fila["DOCUMENTOS_SELECCION"].ToString().Trim().Split(';');

        foreach (ListItem item in CheckBoxList_DOCUMENTOS_SELECCION.Items)
        {
            Boolean seleccionar = false;

            for (int i = 0; i < listaDocsSeleccionados.Length; i++)
            {
                if (item.Value == listaDocsSeleccionados[i])
                {
                    seleccionar = true;
                    break;
                }
            }

            item.Selected = seleccionar;
        }

    }

    private void SeleccionarDocsContratacion(DataRow fila)
    {
        String[] listaDocsSeleccionados = fila["DOCUMENTOS_CONTRATACION"].ToString().Trim().Split(';');

        foreach (ListItem item in CheckBoxList_DOCUEMENTOS_CONTRATACION.Items)
        {
            Boolean seleccionar = false;

            for (int i = 0; i < listaDocsSeleccionados.Length; i++)
            {
                if (item.Value == listaDocsSeleccionados[i])
                {
                    seleccionar = true;
                    break;
                }
            }

            item.Selected = seleccionar;
        }
    }

    private void SeleccionarEmailSeleccion(DataRow fila)
    {
        Decimal ID_CONTACTO = Convert.ToDecimal(fila["ID_CONTACTO_SELECCION"]);
        contactos _contactos = new contactos(Session["idEmpresa"].ToString());
        DataTable tablaContacto = _contactos.ObtenerContactosPorRegistro(ID_CONTACTO);
        DataRow filaContacto = tablaContacto.Rows[0];

        cargar_DropDownList_CONTACTO_SELECCION();
        DropDownList_CONTACTO_SELECCION.SelectedValue = fila["ID_CONTACTO_SELECCION"].ToString().Trim();
        TextBox_EMAIL_SELECCION.Text = filaContacto["CONT_MAIL"].ToString().Trim();
    }

    private void SeleccionarEmailContratacion(DataRow fila)
    {
        Decimal ID_CONTACTO = Convert.ToDecimal(fila["ID_CONTACTO_CONTRATACION"]);
        contactos _contactos = new contactos(Session["idEmpresa"].ToString());
        DataTable tablaContacto = _contactos.ObtenerContactosPorRegistro(ID_CONTACTO);
        DataRow filaContacto = tablaContacto.Rows[0];

        cargar_DropDownList_CONTACTO_CONTRATACION();
        DropDownList_CONTACTO_CONTRATACION.SelectedValue = fila["ID_CONTACTO_CONTRATACION"].ToString().Trim();
        TextBox_EMAIL_CONTRATACION.Text = filaContacto["CONT_MAIL"].ToString().Trim();
    }

    private void Cargar(DataTable tablaConf)
    {
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);

        DataRow fila = tablaConf.Rows[0];

        HiddenField_ID_CONFIGURACION.Value = fila["ID_CONFIGURACION"].ToString().Trim();

        if (fila["ENTREGA_DOCUMENTOS"].ToString().ToUpper() == "TRUE")
        {
            Mostrar(Acciones.CON_ENTREGA);
            
            RadioButtonList_TIPO_ENTREGA.SelectedValue = "CON";

            SeleccionarDocsSeleccion(fila);
            SeleccionarDocsContratacion(fila);

            SeleccionarEmailSeleccion(fila);
            SeleccionarEmailContratacion(fila);
        }
        else
        {
            Mostrar(Acciones.SIN_ENTREGA);

            RadioButtonList_TIPO_ENTREGA.SelectedValue = "SIN";
        }

    }

    private void CargarDatosConfiguracionEntraga()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);

        HiddenField_ID_EMPRESA.Value = ID_EMPRESA.ToString();

        ConfDocEntregable _confDocEntregable = new ConfDocEntregable(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaConf = _confDocEntregable.ObtenerPorEmpresa(ID_EMPRESA);

        if (tablaConf.Rows.Count <= 0)
        {
            if (_confDocEntregable.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _confDocEntregable.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La empresa no tienen configurada la entrega de documentos. Por favor realice la configuración y guarde los datos.", Proceso.Error);

                Ocultar(Acciones.Inicio);
                Mostrar(Acciones.SinConf);
                Limpiar(Acciones.SinConf);
                Activar(Acciones.SinConf);
            }

            HiddenField_ID_CONFIGURACION.Value = "";
        }
        else
        {
            Cargar(tablaConf);
        }
    }

    private void cargar_DropDownList_CONTACTO_SELECCION()
    {
        DropDownList_CONTACTO_SELECCION.Items.Clear();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_CONTACTO_SELECCION.Items.Add(item);

        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        contactos _contactos = new contactos(Session["idEmpresa"].ToString());
        tabla.proceso pr = tabla.proceso.ContactoSeleccion;
        DataTable tablaContactosOriginal = _contactos.ObtenerContactosPorIdEmpresa(ID_EMPRESA, pr);

        foreach (DataRow fila in tablaContactosOriginal.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["CONT_NOM"].ToString(), fila["REGISTRO"].ToString());
            DropDownList_CONTACTO_SELECCION.Items.Add(item);
        }

        DropDownList_CONTACTO_SELECCION.DataBind();
    }

    private void cargar_DropDownList_CONTACTO_CONTRATACION()
    {
        DropDownList_CONTACTO_CONTRATACION.Items.Clear();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        DropDownList_CONTACTO_CONTRATACION.Items.Add(item);

        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        contactos _contactos = new contactos(Session["idEmpresa"].ToString());
        tabla.proceso pr = tabla.proceso.ContactoContratacion;
        DataTable tablaContactosOriginal = _contactos.ObtenerContactosPorIdEmpresa(ID_EMPRESA, pr);

        foreach (DataRow fila in tablaContactosOriginal.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["CONT_NOM"].ToString(), fila["REGISTRO"].ToString());
            DropDownList_CONTACTO_CONTRATACION.Items.Add(item);
        }

        DropDownList_CONTACTO_CONTRATACION.DataBind();
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                CargarDatosConfiguracionEntraga();
                break;
            case Acciones.CON_ENTREGA:
                cargar_DropDownList_CONTACTO_SELECCION();
                cargar_DropDownList_CONTACTO_CONTRATACION();
                break;
            case Acciones.SEL_CON_ENTREGA:
                cargar_DropDownList_CONTACTO_SELECCION();
                cargar_DropDownList_CONTACTO_CONTRATACION();
                break;
        }
    }

    private void Desactivar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                RadioButtonList_TIPO_ENTREGA.Enabled = false;
                CheckBoxList_DOCUMENTOS_SELECCION.Enabled = false;
                DropDownList_CONTACTO_SELECCION.Enabled = false;
                TextBox_EMAIL_SELECCION.Enabled = false;

                CheckBoxList_DOCUEMENTOS_CONTRATACION.Enabled = false;
                DropDownList_CONTACTO_CONTRATACION.Enabled = false;
                TextBox_EMAIL_CONTRATACION.Enabled = false;

                break;
            case Acciones.SIN_ENTREGA:
                
                break;
        }
    }

    private void Iniciar()
    {
        Configurar();
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = "CONFIGURACIÓN ENTREGA DOCUMENTOS TRABAJADOR";

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

    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Modificar);
        Mostrar(Acciones.Modificar);
        Activar(Acciones.Modificar);
    }

    private void Guardar()
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        
        Boolean ENTREGA_DOCUMENTOS = false;
        if (RadioButtonList_TIPO_ENTREGA.SelectedValue == "CON")
        {
            ENTREGA_DOCUMENTOS = true;
        }

        String DOCUMENTOS_SELECCION = null;
        String DOCUMENTOS_CONTRATACION = null;
        Decimal ID_CONTACTO_SELECCION = 0;
        Decimal ID_CONTACTO_CONTRATACION = 0;
        if (ENTREGA_DOCUMENTOS == true)
        { 
            for(int i = 0; i < CheckBoxList_DOCUMENTOS_SELECCION.Items.Count; i++)
            {
                ListItem item = CheckBoxList_DOCUMENTOS_SELECCION.Items[i];

                if (item.Selected == true)
                {
                    if (i == 0)
                    {
                        DOCUMENTOS_SELECCION = item.Value;
                    }
                    else
                    {
                        DOCUMENTOS_SELECCION += ";" + item.Value;
                    }
                }
            }

            for (int i = 0; i < CheckBoxList_DOCUEMENTOS_CONTRATACION.Items.Count; i++)
            {
                ListItem item = CheckBoxList_DOCUEMENTOS_CONTRATACION.Items[i];

                if (item.Selected == true)
                {
                    if (i == 0)
                    {
                        DOCUMENTOS_CONTRATACION = item.Value;
                    }
                    else
                    {
                        DOCUMENTOS_CONTRATACION += ";" + item.Value;
                    }
                }
            }

            ID_CONTACTO_SELECCION = Convert.ToDecimal(DropDownList_CONTACTO_SELECCION.SelectedValue);
            ID_CONTACTO_CONTRATACION = Convert.ToDecimal(DropDownList_CONTACTO_CONTRATACION.SelectedValue);
        }

        ConfDocEntregable _confDocEntregable = new ConfDocEntregable(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Decimal ID_CONFIGURACION = _confDocEntregable.Adicionar(ID_EMPRESA, ENTREGA_DOCUMENTOS, DOCUMENTOS_SELECCION, DOCUMENTOS_CONTRATACION, ID_CONTACTO_SELECCION, ID_CONTACTO_CONTRATACION);

        if (ID_CONFIGURACION <= 0)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _confDocEntregable.MensajeError, Proceso.Error);
        }
        else
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La configuración de documentos entregables se realizaó correctamente.", Proceso.Correcto);

            CargarDatosConfiguracionEntraga();
        }
    }

    private void Actualizar()
    {
        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);
        Decimal ID_CONFIGURACION = Convert.ToDecimal(HiddenField_ID_CONFIGURACION.Value);

        Boolean ENTREGA_DOCUMENTOS = false;
        if (RadioButtonList_TIPO_ENTREGA.SelectedValue == "CON")
        {
            ENTREGA_DOCUMENTOS = true;
        }

        String DOCUMENTOS_SELECCION = null;
        String DOCUMENTOS_CONTRATACION = null;
        Decimal ID_CONTACTO_SELECCION = 0;
        Decimal ID_CONTACTO_CONTRATACION = 0;
        if (ENTREGA_DOCUMENTOS == true)
        {
            for (int i = 0; i < CheckBoxList_DOCUMENTOS_SELECCION.Items.Count; i++)
            {
                ListItem item = CheckBoxList_DOCUMENTOS_SELECCION.Items[i];

                if (item.Selected == true)
                {
                    if (i == 0)
                    {
                        DOCUMENTOS_SELECCION = item.Value;
                    }
                    else
                    {
                        DOCUMENTOS_SELECCION += ";" + item.Value;
                    }
                }
            }

            for (int i = 0; i < CheckBoxList_DOCUEMENTOS_CONTRATACION.Items.Count; i++)
            {
                ListItem item = CheckBoxList_DOCUEMENTOS_CONTRATACION.Items[i];

                if (item.Selected == true)
                {
                    if (i == 0)
                    {
                        DOCUMENTOS_CONTRATACION = item.Value;
                    }
                    else
                    {
                        DOCUMENTOS_CONTRATACION += ";" + item.Value;
                    }
                }
            }

            ID_CONTACTO_SELECCION = Convert.ToDecimal(DropDownList_CONTACTO_SELECCION.SelectedValue);
            ID_CONTACTO_CONTRATACION = Convert.ToDecimal(DropDownList_CONTACTO_CONTRATACION.SelectedValue);
        }

        ConfDocEntregable _confDocEntregable = new ConfDocEntregable(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Boolean verificador = _confDocEntregable.Actualizar(ID_CONFIGURACION, ENTREGA_DOCUMENTOS, DOCUMENTOS_SELECCION, DOCUMENTOS_CONTRATACION, ID_CONTACTO_SELECCION, ID_CONTACTO_CONTRATACION);

        if (verificador == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _confDocEntregable.MensajeError, Proceso.Error);
        }
        else
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La configuración de documentos entregables se realizaó correctamente.", Proceso.Correcto);

            CargarDatosConfiguracionEntraga();
        }
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        Boolean verificado = true;

        if (RadioButtonList_TIPO_ENTREGA.SelectedValue == "CON")
        {
            Boolean seleccion = false;
            Boolean contratacion = false;
            foreach (System.Web.UI.WebControls.ListItem item in CheckBoxList_DOCUMENTOS_SELECCION.Items)
            {
                if (item.Selected == true)
                {
                    seleccion = true;
                    break;
                }
            }

            foreach (System.Web.UI.WebControls.ListItem item in CheckBoxList_DOCUEMENTOS_CONTRATACION.Items)
            {
                if (item.Selected == true)
                {
                    contratacion = true;
                    break;
                }
            }

            if (seleccion == false && contratacion == false)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar por lo menos un documento para poder continuar.", Proceso.Advertencia);
                verificado = false;
            }
        }

        if (verificado == true)
        { 
            if(String.IsNullOrEmpty(HiddenField_ID_CONFIGURACION.Value) == true)
            {
                Guardar();
            }
            else
            {
                Actualizar();
            }
        }
    }

    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {

    }

    protected void RadioButtonList_TIPO_ENTREGA_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (RadioButtonList_TIPO_ENTREGA.SelectedValue == "SIN")
        {
            Ocultar(Acciones.SEL_SIN_ENTREGA);
        }
        else
        {
            Mostrar(Acciones.SEL_CON_ENTREGA);
            Activar(Acciones.SEL_CON_ENTREGA);
            Limpiar(Acciones.SEL_CON_ENTREGA);
            Cargar(Acciones.SEL_CON_ENTREGA);
        }
    }
    protected void DropDownList_CONTACTO_SELECCION_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_CONTACTO_SELECCION.SelectedIndex <= 0)
        {
            TextBox_EMAIL_SELECCION.Text = "";
        }
        else
        {
            Decimal ID_CONTACTO = Convert.ToDecimal(DropDownList_CONTACTO_SELECCION.SelectedValue);
            contactos _contactos = new contactos(Session["idEmpresa"].ToString());
            DataTable tablaContacto = _contactos.ObtenerContactosPorRegistro(ID_CONTACTO);
            DataRow filaContacto = tablaContacto.Rows[0];

            TextBox_EMAIL_SELECCION.Text = filaContacto["CONT_MAIL"].ToString().Trim();
        }
    }
    protected void DropDownList_CONTACTO_CONTRATACION_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_CONTACTO_CONTRATACION.SelectedIndex <= 0)
        {
            TextBox_EMAIL_CONTRATACION.Text = "";
        }
        else
        {
            Decimal ID_CONTACTO = Convert.ToDecimal(DropDownList_CONTACTO_CONTRATACION.SelectedValue);
            contactos _contactos = new contactos(Session["idEmpresa"].ToString());
            DataTable tablaContacto = _contactos.ObtenerContactosPorRegistro(ID_CONTACTO);
            DataRow filaContacto = tablaContacto.Rows[0];

            TextBox_EMAIL_CONTRATACION.Text = filaContacto["CONT_MAIL"].ToString().Trim();
        }
    }
}