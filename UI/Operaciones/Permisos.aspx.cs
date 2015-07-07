using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Brainsbits.LLB.operaciones;
using Brainsbits.LLB;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;
using System.Data;
using TSHAK.Components;

public partial class Operaciones_Permisos : System.Web.UI.Page
{
    private String NOMBRE_AREA = tabla.NOMBRE_AREA_GESTION_COMERCIAL;
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

    #region variables
    private enum Acciones
    {
        Inicia = 0,
        Adiciona,
        Guarda,
        Modifica,
        Cancela,
        Selecciona
    }
    private enum Proceso
    {
        Correcto = 0,
        Error,
        Advertencia
    }
    #endregion variables

    #region constructor
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack) Iniciar();
    }
    
    #endregion constructor

    #region metodos
    private void Iniciar()
    {
        Configurar();
        Cargar(Acciones.Inicia);
        Bloquear();
        Desbloquear(Acciones.Inicia);
        Ocultar();
        Mostrar(Acciones.Inicia);
    }
    
    private void Configurar()
    {
        TreeView_seccion.Attributes.Add("onclick", "javascript:OnTreeClick(event);");
        Configurar(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }
    
    private void Configurar(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_mensaje.CssClass = "panel_popup";
    }
    
    private void Ocultar()
    {
        Button_NUEVO.Visible = false;
        Button_GUARDAR.Visible=false;
        Button_MODIFICAR.Visible = false;
        Button_CANCELAR.Visible = false;
    }
    
    private void Ocultar(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.Style.Add("display", "none");
        panel_mensaje.Style.Add("display", "none");

        panel_fondo.Visible = false;
        panel_mensaje.Visible = false;
    }
    
    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicia:
            case Acciones.Cancela:
                Button_NUEVO.Visible = true;
                break;
            case Acciones.Adiciona:
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;
                break;
            case Acciones.Guarda:
                Button_MODIFICAR.Visible = true;
                Button_NUEVO.Visible = true;
                break;
            case Acciones.Modifica:
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;
                break;
            case Acciones.Selecciona:
                Button_MODIFICAR.Visible = true;
                break;
        }
    }
    
    private void Cargar(Acciones accion)
    {
        try
        {
            switch (accion)
            {
                case Acciones.Inicia:
                case Acciones.Guarda:
                    Permiso permiso = new Permiso(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                    Cargar(permiso);
                    break;

            }
        }
        catch (Exception e)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, e.Message, Proceso.Advertencia);
        }
    }

    private void Cargar(Permiso.Procesos proceso)
    {
        DropDownList_Proceso.SelectedValue = proceso.ToString();
        Permiso permiso = new Permiso(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        permiso.ObtenerPorProceso(proceso);
        foreach (TreeNode treeNode in TreeView_seccion.Nodes)
        {
            if (treeNode.ChildNodes.Count > 0)
            {
                foreach (TreeNode childNodes in treeNode.ChildNodes)
                {
                    if ((childNodes.Value.Equals(Permiso.Procesos.Comercial.ToString())) && (permiso.Comercial)) childNodes.Checked = true;
                    if ((childNodes.Value.Equals(Permiso.Procesos.Seleccion.ToString())) && (permiso.Seleccion)) childNodes.Checked = true;
                    if ((childNodes.Value.Equals(Permiso.Procesos.Contratacion.ToString())) && (permiso.Contratacion)) childNodes.Checked = true;
                    if ((childNodes.Value.Equals(Permiso.Procesos.Nomina.ToString())) && (permiso.Nomina)) childNodes.Checked = true;
                    if ((childNodes.Value.Equals(Permiso.Procesos.Facturacion.ToString())) && (permiso.Facturacion)) childNodes.Checked = true;
                    if ((childNodes.Value.Equals(Permiso.Procesos.Contabilidad.ToString())) && (permiso.Contabilidad)) childNodes.Checked = true;
                    if ((childNodes.Value.Equals(Permiso.Procesos.Financiera.ToString())) && (permiso.Financiera)) childNodes.Checked = true;
                    if ((childNodes.Value.Equals(Permiso.Procesos.Juridica.ToString())) && (permiso.Juridica)) childNodes.Checked = true;
                    if ((childNodes.Value.Equals(Permiso.Procesos.SaludIntegral.ToString())) && (permiso.SaludIntegral)) childNodes.Checked = true;
                    if ((childNodes.Value.Equals(Permiso.Procesos.Operaciones.ToString())) && (permiso.Operaciones)) childNodes.Checked = true;
                    if ((childNodes.Value.Equals(Permiso.Procesos.BienestarSocial.ToString())) && (permiso.BienestarSocial)) childNodes.Checked = true;
                    if ((childNodes.Value.Equals(Permiso.Procesos.Rse.ToString())) && (permiso.Rse)) childNodes.Checked = true;
                    if ((childNodes.Value.Equals(Permiso.Procesos.ComprasEInventario.ToString())) && (permiso.ComprasEInventario)) childNodes.Checked = true;

                    if (childNodes.ChildNodes.Count > 0)
                    {
                        foreach (TreeNode tn in childNodes.ChildNodes)
                        {
                            if ((tn.Value.Equals(Permiso.Secciones.ComercialCobertura.ToString())) && (permiso.ComercialCobertura)) tn.Checked = true;
                            if ((tn.Value.Equals(Permiso.Secciones.ComercialCondicionesEconomicas.ToString())) && (permiso.ComercialCondicionesEconomicas)) tn.Checked = true;
                            if ((tn.Value.Equals(Permiso.Secciones.ComercialContactos.ToString())) && (permiso.ComercialContactos)) tn.Checked = true;
                            if ((tn.Value.Equals(Permiso.Secciones.ComercialUnidadNegocio.ToString())) && (permiso.ComercialUnidadNegocio)) tn.Checked = true;
                        }
                    }
                }
            }
        }
    }
    
    private void Cargar(Permiso permiso)
    {
        GridView_procesos_con_permisos.DataSource = permiso.ObtenerTodosLosProcesos();
        GridView_procesos_con_permisos.DataBind();
    }
    
    private void Bloquear()
    {
        GridView_procesos_con_permisos.Enabled = false;
        DropDownList_Proceso.Enabled = false;
        TreeView_seccion.Enabled = false;
    }
    
    private void Desbloquear(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicia:
            case Acciones.Cancela:
                GridView_procesos_con_permisos.Enabled = true;
                break;
            case Acciones.Adiciona:
                DropDownList_Proceso.Enabled = true;
                TreeView_seccion.Enabled = true;
                break;
            case Acciones.Modifica:
                TreeView_seccion.Enabled = true;
                break;
            case Acciones.Guarda:
                GridView_procesos_con_permisos.Enabled = true;
                break;
        }
    }
    
    private void Buscar()
    {
    }

    private bool Buscar(string proceso)
    {
        bool encontrado = false;
        foreach(GridViewRow gridViewRow in this.GridView_procesos_con_permisos.Rows)
        {
            if (encontrado.Equals(false))
            {
                if (gridViewRow.Cells[1].Text.Equals(proceso)) encontrado = true;
            }
        }
        return encontrado;
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

    private void Guardar()
    {
        if (Validar())
        {
            List<Permiso> permisos = new List<Permiso>();
            Permiso permiso;
            foreach (TreeNode treeNode in TreeView_seccion.CheckedNodes)
            {
                if (treeNode.Text != "Secciones del Manual del Cliente")
                {
                    if (treeNode.ChildNodes.Count > 0)
                    {
                        foreach (TreeNode childNodes in treeNode.ChildNodes)
                        {
                            if (childNodes.Checked)
                            {
                                permiso = new Permiso(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                                permiso.Proceso = DropDownList_Proceso.SelectedValue.ToString();
                                permiso.ProcesoPermitido = treeNode.Value;
                                permiso.Seccion = childNodes.Value;
                                permisos.Add(permiso);
                            }
                        }
                    }
                }
            }
            permiso = new Permiso(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            try
            {
                permiso.Adicionar(permisos);
                Bloquear();
                Desbloquear(Acciones.Guarda);
                Ocultar();
                Mostrar(Acciones.Guarda);
                Cargar(Acciones.Guarda);
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Los datos han sido almacenados correctamente.", Proceso.Correcto);
            }
            catch (Exception e)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Los datos NO han sido almacenados. " + e.Message, Proceso.Error);
            }
        }
    }

    private void Actualizar()
    {
        if (Validar())
        {
            List<Permiso> permisos = new List<Permiso>();
            Permiso permiso;
            foreach (TreeNode treeNode in TreeView_seccion.CheckedNodes)
            {
                if (treeNode.Text != "Secciones del Manual del Cliente")
                {
                    if (treeNode.ChildNodes.Count > 0)
                    {
                        foreach (TreeNode childNodes in treeNode.ChildNodes)
                        {
                            if (childNodes.Checked)
                            {
                                permiso = new Permiso(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                                permiso.Proceso = DropDownList_Proceso.SelectedValue.ToString();
                                permiso.ProcesoPermitido = treeNode.Value;
                                permiso.Seccion = childNodes.Value;
                                permisos.Add(permiso);
                            }
                        }
                    }
                }
            }
            permiso = new Permiso(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            try
            {
                permiso.Actualizar(permisos);
                Bloquear();
                Desbloquear(Acciones.Guarda);
                Ocultar();
                Mostrar(Acciones.Guarda);
                Cargar(Acciones.Guarda);
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Los datos han sido actualizados correctamente.", Proceso.Correcto);
            }
            catch (Exception e)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Los datos NO han sido actualizados. " + e.Message, Proceso.Error);
            }
        }
    }
    
    private void Limpiar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicia:
            case Acciones.Cancela:
                DropDownList_Proceso.SelectedValue = "Seleccione";
                foreach (TreeNode treeNode in TreeView_seccion.Nodes)
                {
                    treeNode.Checked = false;
                    if (treeNode.ChildNodes.Count > 0) Marcar(treeNode, false);
                }       
                break;
        }
    }

    private void Marcar(TreeNode treeNode, bool seleccionado)
    {
        foreach (TreeNode childNodes in treeNode.ChildNodes)
        {
            childNodes.Checked = seleccionado;
            if (treeNode.ChildNodes.Count > 0) Marcar(childNodes, seleccionado);
        }                                
    }

    private bool Validar()
    {
        bool valido = true;
        if (this.DropDownList_Proceso.SelectedValue.Equals("Seleccione"))
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar un proceso.", Proceso.Advertencia);
            valido = false;
        }

        if (valido)
        {
            if (TreeView_seccion.CheckedNodes.Count.Equals(0))
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar por lo menos una sección del Manual del Cliente.", Proceso.Advertencia);
                valido = false;
            }
        }
        return valido;
    }
    #endregion metodos

    #region eventos
    
    protected void Button_NUEVO_Click(object sender, EventArgs e)
    {
        HiddenField_accion.Value = Acciones.Adiciona.ToString();
        Limpiar(Acciones.Inicia);
        Bloquear();
        Desbloquear(Acciones.Adiciona);
        Ocultar();
        Mostrar(Acciones.Adiciona);
    }
    
    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        if (HiddenField_accion.Value.Equals(Acciones.Adiciona.ToString())) Guardar();
        else 
        {
            if (HiddenField_accion.Value.Equals(Acciones.Modifica.ToString())) Actualizar();
        }
    }
    
    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        HiddenField_accion.Value = Acciones.Modifica.ToString();
        if (DropDownList_Proceso.Text.Equals("Seleccione")) Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar de proceso de: Procesos con permisos", Proceso.Advertencia);
        else
        {
            Bloquear();
            Desbloquear(Acciones.Modifica);
            Ocultar();
            Mostrar(Acciones.Modifica);
        }
    }
    
    protected void Button_COPIAR_Click(object sender, EventArgs e)
    {

    }
    
    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        Limpiar(Acciones.Cancela);
        Bloquear();
        Desbloquear(Acciones.Cancela);
        Ocultar();
        Mostrar(Acciones.Cancela);
    }
    
    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {
        Ocultar(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }
    
    protected void DropDownList_Proceso_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (Buscar(DropDownList_Proceso.Text))
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Los permisos para el proceso " + DropDownList_Proceso.Text.ToString() + " ya estan configurados. Si desea actualizarlo seleccionelo de la lista Procesos con permisos", Proceso.Advertencia);
            DropDownList_Proceso.SelectedValue = "Seleccione";
        }
    }

    protected void GridView_procesos_con_permisos_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "Select")
        {
            Cargar((Permiso.Procesos)Enum.Parse(typeof(Permiso.Procesos), GridView_procesos_con_permisos.DataKeys[index].Values["PROCESO"].ToString()));
            Mostrar(Acciones.Selecciona);
        }
    }

    #endregion eventos
}