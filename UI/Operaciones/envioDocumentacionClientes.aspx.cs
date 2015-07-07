using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Brainsbits.LLB.comercial;
using System.Data;
using Brainsbits.LLB.operaciones;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB;
using ICSharpCode.SharpZipLib.Zip;
using Brainsbits.LLB.seleccion;
using System.IO;
using Brainsbits.LLB.seguridad;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using Brainsbits.LLB.contratacion;
using TSHAK.Components;

public partial class _Default : System.Web.UI.Page
{
    private String NOMBRE_AREA = tabla.NOMBRE_AREA_OPERACIONES;
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

    private System.Drawing.Color colorContratoActivo = System.Drawing.ColorTranslator.FromHtml("#50CE04");
    private System.Drawing.Color colorSeleccionado = System.Drawing.ColorTranslator.FromHtml("#699DF0");

    private enum ClaseContrato
    {
        O_L = 0,
        I,
        T_F,
        L_C_C_D_A,
        L_S_C_D_A_C_V
    }

    private enum EntidadesAfiliacion
    {
        ARP = 0,
        EPS = 1,
        CAJA = 2,
        AFP = 3
    }

    private enum AccionesEnvio
    { 
        correo = 0,
        download
    }

    private enum Acciones
    {
        Inicio = 0,
        Empleados,
        EmpleadoSeleccionado,
        ContratoSeleccionado,
        BuscarEmpleado
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
                Panel_SELECICON_EMPRESA.Visible = false;
                Panel_SELECCION_EMPLEADO.Visible = false;
                Panel_DATOS_TRABAJADOR.Visible = false;
                Panel_INFORMACION_CONTRATOS.Visible = false;
                Panel_DOCUMENTACION.Visible = false;
                Panel_BOTONES_ABAJO.Visible = false;
                Button_VERIFICAR_DOCUMENTOS.Visible = false;
                Button_ENVIAR_DOCUMENTOS.Visible = false;
                break;
            case Acciones.EmpleadoSeleccionado:
                Panel_DATOS_TRABAJADOR.Visible = false;
                Panel_INFORMACION_CONTRATOS.Visible = false;
                Panel_DOCUMENTACION.Visible = false;
                Panel_BOTONES_ABAJO.Visible = false;
                Button_VERIFICAR_DOCUMENTOS.Visible = false;
                Button_ENVIAR_DOCUMENTOS.Visible = false;
                break;
            case Acciones.BuscarEmpleado:
                Panel_DATOS_TRABAJADOR.Visible = false;
                Panel_INFORMACION_CONTRATOS.Visible = false;
                Panel_DOCUMENTACION.Visible = false;
                Panel_BOTONES_ABAJO.Visible = false;
                break;
        }
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_SELECICON_EMPRESA.Visible = true;
                break;
            case Acciones.Empleados:
                Panel_SELECICON_EMPRESA.Visible = true;
                Panel_SELECCION_EMPLEADO.Visible = true;
                break;
            case Acciones.EmpleadoSeleccionado:
                Panel_DATOS_TRABAJADOR.Visible = true;
                Panel_INFORMACION_CONTRATOS.Visible = true;
                break;
            case Acciones.ContratoSeleccionado:
                Panel_DOCUMENTACION.Visible = true;
                Panel_BOTONES_ABAJO.Visible = true;
                Button_VERIFICAR_DOCUMENTOS.Visible = true;
                Button_ENVIAR_DOCUMENTOS.Visible = true;
                break;
        }
    }

    private void Desactivar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                TextBox_EMAIL_SELECCION.Enabled = false;
                TextBox_EMAIL_CONTRATACION.Enabled = false;
                break;
        }
    }

    private void cargar_DropDownList_ID_EMPRESA()
    {
        DropDownList_ID_EMPRESA.Items.Clear();

        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaEmpresasActivas = _cliente.ObtenerTodasLasEmpresasActivas();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione", "");
        DropDownList_ID_EMPRESA.Items.Add(item);

        foreach (DataRow fila in tablaEmpresasActivas.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["RAZ_SOCIAL"].ToString(), fila["ID_EMPRESA"].ToString());
            DropDownList_ID_EMPRESA.Items.Add(item);
        }

        DropDownList_ID_EMPRESA.DataBind();
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
        tabla.proceso pr = tabla.proceso.ContactoSeleccion;
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
                cargar_DropDownList_ID_EMPRESA();
                break;
            case Acciones.Empleados:
                DropDownList_ID_TRABAJADOR.Items.Add(new System.Web.UI.WebControls.ListItem("Seleccione...", ""));
                break;
        }
    }

    private void Iniciar()
    {
        Configurar();
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = "ENVÍO DE DOCUEMNTOS A CLIENTES";

        if (IsPostBack == false)
        {
            Iniciar();
        }
    }
    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {

    }

    private void Limpiar(Acciones accion)
    {
        switch (accion)
        { 
            case Acciones.Empleados:
                TextBox_BUSCADOR_TRABAJADOR.Text = "";
                DropDownList_ID_TRABAJADOR.Items.Clear();
                break;
            case Acciones.EmpleadoSeleccionado:
                foreach (System.Web.UI.WebControls.ListItem item in CheckBoxList_DOCUMENTOS_SELECCION.Items)
                {
                    item.Selected = false;
                }
                foreach (System.Web.UI.WebControls.ListItem item in CheckBoxList_DOCUEMENTOS_CONTRATACION.Items)
                {
                    item.Selected = false;
                }

                TextBox_EMAIL_SELECCION.Text = "";
                TextBox_EMAIL_CONTRATACION.Text = "";
                break;
            case Acciones.ContratoSeleccionado:
                foreach (System.Web.UI.WebControls.ListItem item in CheckBoxList_DOCUMENTOS_SELECCION.Items)
                {
                    item.Selected = false;
                }
                foreach (System.Web.UI.WebControls.ListItem item in CheckBoxList_DOCUEMENTOS_CONTRATACION.Items)
                {
                    item.Selected = false;
                }
                 TextBox_EMAIL_SELECCION.Text = "";
                TextBox_EMAIL_CONTRATACION.Text = "";
                break;
        }
    }

    protected void DropDownList_ID_EMPRESA_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_ID_EMPRESA.SelectedIndex <= 0)
        {
            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);

            HiddenField_ID_EMPRESA.Value = "";
            HiddenField_ID_SOLICITUD.Value = "";
            HiddenField_ID_EMPLEADO.Value = "";
            HiddenField_ID_CONTRATO.Value = "";
            HiddenField_ID_PERFIL.Value = "";
            HiddenField_ID_REFERENCIA.Value = "";
            HiddenField_ID_REQUERIMIENTO.Value = "";

        }
        else
        {
            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.Empleados);
            Limpiar(Acciones.Empleados);
            Cargar(Acciones.Empleados);

            Decimal ID_EMPRESA = Convert.ToDecimal(DropDownList_ID_EMPRESA.SelectedValue);
            HiddenField_ID_EMPRESA.Value = ID_EMPRESA.ToString();
            HiddenField_ID_CONTRATO.Value = "";
            HiddenField_ID_EMPLEADO.Value = "";
            HiddenField_ID_PERFIL.Value = "";
            HiddenField_ID_REFERENCIA.Value = "";
            HiddenField_ID_REQUERIMIENTO.Value = "";
        } 
    }

    private void cargar_DropDownList_ID_TRABAJADOR(String dato)
    {
        Ocultar(Acciones.BuscarEmpleado);

        Decimal ID_EMPRESA = Convert.ToDecimal(HiddenField_ID_EMPRESA.Value);

        DropDownList_ID_TRABAJADOR.Items.Clear();
        
        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem ("Seleccione...", "");
        DropDownList_ID_TRABAJADOR.Items.Add(item);

        Operacion _operacion = new Operacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaTrabajadores = _operacion.BuscarInformacionContratosEmpresa(dato, ID_EMPRESA);

        foreach (DataRow fila in tablaTrabajadores.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOMBRES"].ToString().Trim() + " " + fila["APELLIDOS"].ToString(), fila["ID_SOLICITUD"].ToString().Trim());
            DropDownList_ID_TRABAJADOR.Items.Add(item);
        }

        DropDownList_ID_TRABAJADOR.DataBind();
    }

    protected void Button_BUSCADOR_TRABAJADOR_Click(object sender, EventArgs e)
    {
        String datoABuscar = TextBox_BUSCADOR_TRABAJADOR.Text;

        cargar_DropDownList_ID_TRABAJADOR(datoABuscar);
    }

    private void Cargar(DataTable tablaContratos)
    {
        GridView_CONTRATOS.DataSource = tablaContratos;
        GridView_CONTRATOS.DataBind();

        GridViewRow fila;
        for (int i = 0; i < GridView_CONTRATOS.Rows.Count; i++)
        {
            fila = GridView_CONTRATOS.Rows[i];
            if (GridView_CONTRATOS.DataKeys[i].Values["CONTRATO_ESTADO"].ToString() == "Activo")
            {
                GridView_CONTRATOS.Rows[i].BackColor = colorContratoActivo;
            }
            else
            {
                GridView_CONTRATOS.Rows[i].BackColor = System.Drawing.Color.Transparent;
            }
        }
    }

    protected void DropDownList_ID_TRABAJADOR_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_ID_TRABAJADOR.SelectedIndex <= 0)
        {
            Ocultar(Acciones.EmpleadoSeleccionado);

            HiddenField_ID_EMPLEADO.Value = "";
            HiddenField_ID_CONTRATO.Value = "";
            HiddenField_ID_PERFIL.Value = "";
            HiddenField_ID_SOLICITUD.Value = "";
            HiddenField_ID_REFERENCIA.Value = "";
            HiddenField_ID_REQUERIMIENTO.Value = "";
        }
        else
        {
            Ocultar(Acciones.EmpleadoSeleccionado);
            Mostrar(Acciones.EmpleadoSeleccionado);

            Decimal ID_SOLICITUD = Convert.ToDecimal(DropDownList_ID_TRABAJADOR.SelectedValue);
            HiddenField_ID_SOLICITUD.Value = ID_SOLICITUD.ToString();

            HiddenField_ID_EMPLEADO.Value = "";
            HiddenField_ID_CONTRATO.Value = "";
            HiddenField_ID_PERFIL.Value = "";
            HiddenField_ID_REFERENCIA.Value = "";
            HiddenField_ID_REQUERIMIENTO.Value = "";

            radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaSolicitud = _radicacionHojasDeVida.ObtenerRegSolicitudesingresoPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));
            DataRow filaSolicitud = tablaSolicitud.Rows[0];

            registroContrato _registroContrato = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaContratos = _registroContrato.ObtenerContratosPorIdSolicitud(ID_SOLICITUD);

            Label_NOMBRE_TRABAJADOR.Text = filaSolicitud["NOMBRES"].ToString().Trim() + " " + filaSolicitud["APELLIDOS"].ToString().Trim();
            Label_TIP_DOC_IDENTIDAD.Text = filaSolicitud["TIP_DOC_IDENTIDAD"].ToString().Trim() + " ";
            Label_NUM_DOC_IDENTIDAD.Text = filaSolicitud["NUM_DOC_IDENTIDAD"].ToString().Trim();
            Label_RAZ_SOCIAL.Text = DropDownList_ID_EMPRESA.SelectedItem.Text;

            Cargar(tablaContratos);
        }
    }
    protected void DropDownList_CONTACTO_SELECCION_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(DropDownList_CONTACTO_SELECCION.SelectedIndex <= 0)
        {
            TextBox_EMAIL_SELECCION.Text = "";
        }
        else
        {
            Decimal ID_CONTACTO = Convert.ToDecimal(DropDownList_CONTACTO_SELECCION.SelectedValue);
            contactos _contactos = new contactos(Session["idEmpresa"].ToString());
            DataTable tablaContacto = _contactos.ObtenerContactosPorRegistro(ID_CONTACTO);
            DataRow filaContacto = tablaContacto.Rows[0];

            TextBox_EMAIL_SELECCION.Text = filaContacto["CONT_MAIL"].ToString().Trim() + "sasasa";
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

            TextBox_EMAIL_CONTRATACION.Text = filaContacto["CONT_MAIL"].ToString().Trim() + "asasasa";
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

    private Dictionary<String, byte[]> ObtenerArchivosSeleccionados(String prefijoNombreArchivo)
    {
        Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);
        Decimal ID_PERFIL = Convert.ToDecimal(HiddenField_ID_PERFIL.Value);
        Decimal ID_REFERENCIA = 0;
        if (String.IsNullOrEmpty(HiddenField_ID_REFERENCIA.Value) == false)
        {
            ID_REFERENCIA = Convert.ToDecimal(HiddenField_ID_REFERENCIA.Value);
        }
        Decimal ID_REQUERIMIENTO = Convert.ToDecimal(HiddenField_ID_REQUERIMIENTO.Value);
        Decimal ID_CONTRATO = Convert.ToDecimal(HiddenField_ID_CONTRATO.Value);
        Decimal ID_EMPLEADO = Convert.ToDecimal(HiddenField_ID_EMPLEADO.Value);

        Dictionary<String, byte[]> listaArchivos = new Dictionary<String, byte[]>();

        maestrasInterfaz _maestrasInterfaz = new maestrasInterfaz();

        if (CheckBoxList_DOCUMENTOS_SELECCION.Items[0].Selected == true)
        {
            listaArchivos.Add(prefijoNombreArchivo + "INFORME_SELECCION.pdf", _maestrasInterfaz.GenerarPDFEntrevista(ID_SOLICITUD, ID_PERFIL));
        }

        if (CheckBoxList_DOCUMENTOS_SELECCION.Items[1].Selected == true)
        {
            Dictionary<String, byte[]> archivosPruebas = _maestrasInterfaz.ObtenerArchivosPruebas(prefijoNombreArchivo, ID_PERFIL, ID_SOLICITUD);

            foreach (KeyValuePair<String, byte[]> archivoPrueba in archivosPruebas)
            {
                listaArchivos.Add(archivoPrueba.Key, archivoPrueba.Value);
            }
        }

        if (CheckBoxList_DOCUMENTOS_SELECCION.Items[2].Selected == true)
        {
            listaArchivos.Add(prefijoNombreArchivo + "CONFIRMACION_REFERENCIAS_LABORALES.pdf", _maestrasInterfaz.GenerarPDFReferencia(ID_REFERENCIA, ID_SOLICITUD));
        }




        if (CheckBoxList_DOCUEMENTOS_CONTRATACION.Items[0].Selected == true)
        {
            Dictionary<String, byte[]> archivosExamenes = _maestrasInterfaz.ObtenerArchivosExamenes(prefijoNombreArchivo, ID_SOLICITUD, ID_REQUERIMIENTO);

            foreach (KeyValuePair<String, byte[]> archivoExamen in archivosExamenes)
            {
                listaArchivos.Add(archivoExamen.Key, archivoExamen.Value);
            }
        }


        if (CheckBoxList_DOCUEMENTOS_CONTRATACION.Items[1].Selected == true)
        {
            byte[] archivoExamenes = _maestrasInterfaz.GenerarPDFExamenes(ID_CONTRATO, ID_SOLICITUD, ID_REQUERIMIENTO);
            if (archivoExamenes != null)
            {
                listaArchivos.Add(prefijoNombreArchivo + "AUTOS_RECOMENDACION.pdf", archivoExamenes);
            }
        }

        if (CheckBoxList_DOCUEMENTOS_CONTRATACION.Items[2].Selected == true)
        {
            listaArchivos.Add(prefijoNombreArchivo + "CONTRATO.pdf", _maestrasInterfaz.GenerarPDFContrato(ID_CONTRATO));
        }

        if (CheckBoxList_DOCUEMENTOS_CONTRATACION.Items[3].Selected == true)
        {
            byte[] archivoClausulas = _maestrasInterfaz.GenerarPDFClausulas(ID_CONTRATO);

            if (archivoClausulas != null)
            {
                listaArchivos.Add(prefijoNombreArchivo + "CLAUSULAS_CONTRATO.pdf", archivoClausulas);
            }
        }


        if (CheckBoxList_DOCUEMENTOS_CONTRATACION.Items[4].Selected == true)
        {
            Dictionary<String, byte[]> archivosAfiliaciones = _maestrasInterfaz.ObtenerArchivosAfiliaciones(prefijoNombreArchivo, ID_CONTRATO);

            foreach (KeyValuePair<String, byte[]> archivoAfiliacion in archivosAfiliaciones)
            {
                listaArchivos.Add(archivoAfiliacion.Key, archivoAfiliacion.Value);
            }
        }


        return listaArchivos;
    }

    private void ArmarYEnviarArchivos(AccionesEnvio accion)
    {
        String prefijoNombreArchivo = Label_NUM_DOC_IDENTIDAD.Text.Trim() + "-" + HiddenField_ID_CONTRATO.Value + "-";

        Dictionary<String, byte[]> listaArchivos = ObtenerArchivosSeleccionados(prefijoNombreArchivo);

        if (accion == AccionesEnvio.download)
        {
            Response.Clear();
            Response.BufferOutput = false; 
            String archiveName = String.Format(prefijoNombreArchivo + "DOCUMENTACION_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd"));
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "filename=" + archiveName);

            using (ZipOutputStream s = new ZipOutputStream(Response.OutputStream))
            {
                s.SetLevel(9); 

                foreach (KeyValuePair<String, byte[]> archivo in listaArchivos)
                {
                    ZipEntry entry = new ZipEntry(archivo.Key);



                    entry.DateTime = DateTime.Now;
                    s.PutNextEntry(entry);

                    s.Write(archivo.Value, 0, (int)archivo.Value.Length);
                }


                s.Finish();

                s.Close();
            }

            Response.Close();
        }
    }

    protected void Button_VERIFICAR_DOCUMENTOS_Click(object sender, EventArgs e)
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
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar por lo menos un documento para enviar.", Proceso.Advertencia);
        }
        else
        {
            ArmarYEnviarArchivos(AccionesEnvio.download);
        }
    }
    protected void Button_ENVIAR_DOCUMENTOS_Click(object sender, EventArgs e)
    {

    }

    private void poner_color_a_grilla_contratos()
    {
        for (int i = 0; i < GridView_CONTRATOS.Rows.Count; i++)
        {
            if (GridView_CONTRATOS.DataKeys[i].Values["CONTRATO_ESTADO"].ToString() == "Activo")
            {
                GridView_CONTRATOS.Rows[i].BackColor = colorContratoActivo;
            }
            else
            {
                GridView_CONTRATOS.Rows[i].BackColor = System.Drawing.Color.Transparent;
            }
        }
    }

    protected void GridView_CONTRATOS_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        Mostrar(Acciones.ContratoSeleccionado);
        Limpiar(Acciones.ContratoSeleccionado);

        int index = Convert.ToInt32(e.CommandArgument);

        poner_color_a_grilla_contratos();
        GridView_CONTRATOS.Rows[index].BackColor = colorSeleccionado;

        Decimal ID_EMPLEADO = Convert.ToDecimal(GridView_CONTRATOS.DataKeys[index].Values["ID_EMPLEADO"]);
        HiddenField_ID_EMPLEADO.Value = ID_EMPLEADO.ToString();

        Operacion _operacion = new Operacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaContrato = _operacion.ObtenerInformacionContratoPorIdEmpleado(ID_EMPLEADO);
        DataRow filaContrato = tablaContrato.Rows[0];

        HiddenField_ID_CONTRATO.Value = filaContrato["ID_CONTRATO"].ToString();
        HiddenField_ID_SOLICITUD.Value = filaContrato["ID_SOLICITUD"].ToString();
        HiddenField_ID_PERFIL.Value = filaContrato["ID_PERFIL_REQUISICION"].ToString();
        HiddenField_ID_REFERENCIA.Value = filaContrato["ID_REFERENCIA"].ToString();
        HiddenField_ID_REQUERIMIENTO.Value = filaContrato["ID_REQUERIMIENTO"].ToString();

        cargar_DropDownList_CONTACTO_SELECCION();
        cargar_DropDownList_CONTACTO_CONTRATACION();
    }
}