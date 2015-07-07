using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;

using Brainsbits.LLB;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB.maestras;

public partial class contratacion_casosSeveros : System.Web.UI.Page
{
    #region varialbles
    private enum Acciones
    {
        Inicio = 0,
        BusquedaEncontro = 1,
        BusquedaNoEncontro = 2,
        Adiciona = 3,
        Guarda = 4,
        Modifica = 5,
        Visualiza = 6,
        BusquedaEncontroCasosSeveros = 7,
        BusquedaNoEncontroCasosSeveros = 8
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1
    }

    private enum Datos
    {
        Contrato = 0,
        Incapacidad = 1
    }
    #endregion varialbles

    #region constructor
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Iniciar();
        }
    }
    #endregion constructor

    #region metodos
    private void Iniciar()
    {
        Ocultar();
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }

    private void Ocultar()
    {
        this.Panel_INFO_ADICIONAL_MODULO.Visible = false;
        this.Panel_FORM_BOTONES_ENCABEZADO.Visible = false;
        this.Panel_MENSAJES.Visible = false;
        this.Panel_RESULTADOS_GRID.Visible = false;
        this.Panel_CONTROL_REGISTRO.Visible = false;
        this.Panel_FORMULARIO.Visible = false;
        this.Panel_DATOS.Visible = false;
        this.Panel_FORM_BOTONES_PIE.Visible = false;
        this.Panel_DATOS_CONTRATO.Visible = false;
        this.Panel_RESULTADOS_GRID_CASOS_SEVEROS.Visible = false;

        Button_NUEVO.Visible = false;
        Button_GUARDAR.Visible = false;
        Button_MODIFICAR.Visible = false;
        Button_CANCELAR.Visible = false;

        Button_GUARDAR_1.Visible = false;
        Button_MODIFICAR_1.Visible = false;
        Button_CANCELAR_1.Visible = false;

        TextBox_ID.Visible = false;
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                break;
            case Acciones.Adiciona:
                Panel_INFO_ADICIONAL_MODULO.Visible = true;
                Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                Panel_FORMULARIO.Visible = true;
                Panel_DATOS.Visible = true;
                Panel_FORM_BOTONES_PIE.Visible = true;
                Panel_DATOS_CONTRATO.Visible = true;
                Button_GUARDAR.Visible = true;
                Button_CANCELAR.Visible = true;
                Button_GUARDAR_1.Visible = true;
                Button_CANCELAR_1.Visible = true;
                break;
            case Acciones.BusquedaEncontro:
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                this.Panel_RESULTADOS_GRID.Visible = true;
                break;
            case Acciones.BusquedaNoEncontro:
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                this.Panel_MENSAJES.Visible = true;
                break;
            case Acciones.Guarda:
                this.Panel_INFO_ADICIONAL_MODULO.Visible = true;
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                this.Panel_MENSAJES.Visible = true;
                this.Panel_FORMULARIO.Visible = true;
                this.Panel_DATOS_CONTRATO.Visible = true;
                this.Panel_DATOS.Visible = true;
                this.Panel_FORM_BOTONES_PIE.Visible = true;
                this.Button_MODIFICAR.Visible = true;
                this.Button_MODIFICAR_1.Visible = true;
                this.Button_CANCELAR.Visible = true;
                this.Button_CANCELAR_1.Visible = true;
                this.Button_NUEVO.Visible = true;
                break;
            case Acciones.Modifica:
                this.Panel_INFO_ADICIONAL_MODULO.Visible = true;
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                this.Panel_CONTROL_REGISTRO.Visible = true;
                this.Panel_FORMULARIO.Visible = true;
                this.Panel_DATOS_CONTRATO.Visible = true;
                this.Panel_DATOS.Visible = true;
                this.Panel_FORM_BOTONES_PIE.Visible = true;
                this.Button_GUARDAR.Visible = true;
                this.Button_GUARDAR_1.Visible = true;
                this.Button_CANCELAR.Visible = true;
                this.Button_CANCELAR_1.Visible = true;
                break;
            case Acciones.Visualiza:
                this.Panel_INFO_ADICIONAL_MODULO.Visible = true;
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                this.Panel_CONTROL_REGISTRO.Visible = true;
                this.Panel_DATOS_CONTRATO.Visible = true;
                this.Panel_FORMULARIO.Visible = true;
                this.Panel_DATOS.Visible = true;
                this.Panel_FORM_BOTONES_PIE.Visible = true;
                if (this.TextBox_ACTIVO.Text.ToString().Trim() == "S")
                {
                    this.Button_MODIFICAR.Visible = true;
                    this.Button_MODIFICAR_1.Visible = true;
                }

                this.Button_CANCELAR.Visible = true;
                this.Button_CANCELAR_1.Visible = true;
                break;
            case Acciones.BusquedaEncontroCasosSeveros:
                this.Panel_INFO_ADICIONAL_MODULO.Visible = true;
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                this.Panel_DATOS_CONTRATO.Visible = true;
                this.Panel_RESULTADOS_GRID_CASOS_SEVEROS.Visible = true;
                this.Panel_FORM_BOTONES_PIE.Visible = true;
                if (this.TextBox_ACTIVO.Text.ToString().Trim() == "S") this.Button_NUEVO.Visible = true;
                this.Button_CANCELAR.Visible = true;
                this.Button_CANCELAR_1.Visible = true;
                break;
            case Acciones.BusquedaNoEncontroCasosSeveros:
                this.Panel_FORM_BOTONES_ENCABEZADO.Visible = true;
                this.Panel_FORM_BOTONES_PIE.Visible = true;
                if (this.TextBox_ACTIVO.Text.ToString().Trim() == "S") this.Button_NUEVO.Visible = true;
                this.Panel_MENSAJES.Visible = true;
                break;
        }
    }

    private void Cargar(Acciones accion)
    {
        ListItem item = new ListItem("Ninguno", "0");
        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        switch (accion)
        {
            case Acciones.Inicio:
                DropDownList_BUSCAR.Items.Add(item);
                item = new ListItem("Número de documento", "NUMERO_DOCUMENTO");
                DropDownList_BUSCAR.Items.Add(item);
                item = new ListItem("Nombre", "NOMBRE");
                DropDownList_BUSCAR.Items.Add(item);
                DropDownList_BUSCAR.DataBind();
                break;
        }
    }

    private void Cargar(DataTable dataTable)
    {
        if (dataTable.Rows.Count > 0)
        {
            foreach (DataRow _dataRow in dataTable.Rows)
            {
                if (!String.IsNullOrEmpty(_dataRow["FCH_CRE"].ToString())) this.TextBox_FCH_CRE.Text = _dataRow["FCH_CRE"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["USU_CRE"].ToString())) this.TextBox_USU_CRE.Text = _dataRow["USU_CRE"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["FCH_MOD"].ToString())) this.TextBox_FCH_MOD.Text = _dataRow["FCH_MOD"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["USU_MOD"].ToString())) this.TextBox_USU_MOD.Text = _dataRow["USU_MOD"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["REGISTRO"].ToString())) this.TextBox_ID.Text = _dataRow["REGISTRO"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["FECHA_R"].ToString())) this.TextBox_FECHA_R.Text = _dataRow["FECHA_R"].ToString();
                if (!String.IsNullOrEmpty(_dataRow["OBS_REG"].ToString())) this.TextBox_OBS_REG.Text = _dataRow["OBS_REG"].ToString();
            }
        }
        dataTable.Dispose();
    }

    private void Cargar(GridView gridView)
    {
        if (gridView.Rows.Count > 0)
        {
            if (!String.IsNullOrEmpty(this.GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["NUM_CONTRATO"].ToString())) this.TextBox_NUM_CONTRATO.Text = this.GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["NUM_CONTRATO"].ToString();
            if (!String.IsNullOrEmpty(this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[2].Text)) this.TextBox_ID_EMPLEADO.Text = this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[2].Text;
            if (!String.IsNullOrEmpty(this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[3].Text)) this.TextBox_ACTIVO.Text = this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[3].Text;
            if (!String.IsNullOrEmpty(this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[4].Text)) this.TextBox_APELLIDOS.Text = this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[4].Text;
            if (!String.IsNullOrEmpty(this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[5].Text)) this.TextBox_NOMBRES.Text = this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[5].Text;
            if (!String.IsNullOrEmpty(this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[6].Text)) this.TextBox_NUM_DOC_IDENTIDAD.Text = this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[6].Text;
            if (!String.IsNullOrEmpty(this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[7].Text)) this.TextBox_RAZ_SOCIAL.Text = this.GridView_RESULTADOS_BUSQUEDA.SelectedRow.Cells[7].Text;
        }
    }

    private void Bloquear(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Guarda:
                this.Panel_FORMULARIO.Enabled = false;
                break;
            case Acciones.Visualiza:
                this.Panel_FORMULARIO.Enabled = false;
                break;
        }
    }

    private void Desbloquear(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Adiciona:
                this.Panel_FORMULARIO.Enabled = true;
                break;
            case Acciones.Modifica:
                this.Panel_FORMULARIO.Enabled = true;
                break;
        }
    }

    protected void Buscar()
    {
        Ocultar();
        registroContrato _contrato = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable _dataTable = new DataTable();
        switch (this.DropDownList_BUSCAR.SelectedValue)
        {
            case "NUMERO_DOCUMENTO":
                _dataTable = _contrato.ObtenerPorNumeroIdentificacion(this.TextBox_BUSCAR.Text);
                break;

            case "NOMBRE":
                _dataTable = _contrato.ObtenerPorNombre(this.TextBox_BUSCAR.Text);
                break;
        }

        if (_dataTable.Rows.Count > 0)
        {
            GridView_RESULTADOS_BUSQUEDA.DataSource = _dataTable;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
            Mostrar(Acciones.BusquedaEncontro);
        }
        else
        {
            if (!String.IsNullOrEmpty(_contrato.MensajeError)) Informar(Label_MENSAJE, "Error: Consulte con el Administrador: " + _contrato.MensajeError, Proceso.Error);
            else Informar(Label_MENSAJE, "ADVERTENCIA: No se encontró información para " + this.DropDownList_BUSCAR.SelectedItem + " : " + this.TextBox_BUSCAR.Text, Proceso.Correcto);
            Mostrar(Acciones.BusquedaNoEncontro);
        }
        _dataTable.Dispose();
    }

    private void Informar(Label label, String mensaje, Proceso proceso)
    {
        label.Text = mensaje;
        switch (proceso)
        {
            case Proceso.Correcto:
                label.ForeColor = System.Drawing.Color.Green;
                break;
            case Proceso.Error:
                label.ForeColor = System.Drawing.Color.Red;
                break;
        }
    }

    private void Limpiar()
    {
        TextBox_FCH_CRE.Text = String.Empty;
        TextBox_HOR_CRE.Text = String.Empty;
        TextBox_USU_CRE.Text = String.Empty;
        TextBox_FCH_MOD.Text = String.Empty;
        TextBox_HOR_MOD.Text = String.Empty;
        TextBox_USU_MOD.Text = String.Empty;
        TextBox_ID.Text = String.Empty;
        TextBox_FECHA_R.Text = String.Empty;
        TextBox_OBS_REG.Text = String.Empty;
        TextBox_BUSCAR.Text = String.Empty;
        this.DropDownList_BUSCAR.Items.Clear();
    }

    private void Adicionar()
    {
        Limpiar();
        Ocultar();
        Cargar(Acciones.Inicio);
        Mostrar(Acciones.Adiciona);
        Desbloquear(Acciones.Adiciona);
        this.TextBox_FECHA_R.Focus();
    }

    private void Guardar()
    {
        casoSevero _casoSevero = new casoSevero(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        Decimal ID = _casoSevero.Adicionar(Convert.ToDecimal(TextBox_ID_EMPLEADO.Text), Convert.ToDateTime(TextBox_FECHA_R.Text),
            this.TextBox_OBS_REG.Text);
        if (ID == 0)
        {
            if (!String.IsNullOrEmpty(_casoSevero.MensajeError)) Informar(Label_MENSAJE, "Error: Consulte con el Administrador: " + _casoSevero.MensajeError, Proceso.Error);
        }
        else
        {
            Informar(Label_MENSAJE, "El registro fue creado correctamente y se le asignó el ID: " + ID.ToString() + ".", Proceso.Correcto);
            TextBox_ID.Text = ID.ToString();
        }
        Ocultar();
        Mostrar(Acciones.Guarda);
        Bloquear(Acciones.Guarda);
    }

    private void Modificar()
    {
        casoSevero _casoSevero = new casoSevero(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        if (_casoSevero.Actualizar(Convert.ToDecimal(TextBox_ID.Text), Convert.ToDecimal(TextBox_ID_EMPLEADO.Text),
            Convert.ToDateTime(TextBox_FECHA_R.Text), TextBox_OBS_REG.Text))
        {
            Informar(Label_MENSAJE, "El registro fue actualizada correctamente.", Proceso.Correcto);
            TextBox_ID.Text = ID.ToString();
        }
        else
        {
            if (!String.IsNullOrEmpty(_casoSevero.MensajeError)) Informar(Label_MENSAJE, "Error: Consulte con el Administrador: " + _casoSevero.MensajeError, Proceso.Error);
        }
        Ocultar();
        Mostrar(Acciones.Guarda);
        Bloquear(Acciones.Guarda);
    }

    private void configurarCaracteresAceptadosBusqueda(Boolean letras, Boolean numeros)
    {
        FilteredTextBoxExtender_TextBox_BUSCAR_Numbers.Enabled = numeros;
        FilteredTextBoxExtender_TextBox_BUSCAR_Letras.Enabled = letras;
    }
    #endregion metodos

    #region eventos
    protected void Button_NUEVO_Click(object sender, EventArgs e)
    {
        Adicionar();
    }

    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar();
        Mostrar(Acciones.Modifica);
        Desbloquear(Acciones.Modifica);
        this.TextBox_FECHA_R.Focus();
    }

    protected void Button_GUARDAR_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(this.TextBox_ID.Text)) Guardar();
        else Modificar();
    }

    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        Limpiar();
        Ocultar();
        Cargar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
    }

    protected void DropDownList_BUSCAR_SelectedIndexChanged(object sender, EventArgs e)
    {
        switch (DropDownList_BUSCAR.SelectedValue)
        {
            case "":
                configurarCaracteresAceptadosBusqueda(false, false);
                break;
            case "NUMERO_DOCUMENTO":
                configurarCaracteresAceptadosBusqueda(false, true);
                break;
            case "NOMBRE":
                configurarCaracteresAceptadosBusqueda(true, false);
                break;
        }
    }

    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        Buscar();
    }

    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;
        Buscar();
    }

    protected void GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged(object sender, EventArgs e)
    {
        Ocultar();
        casoSevero _casoSevero = new casoSevero(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable _dataTable = new DataTable();
        _dataTable = _casoSevero.ObtenerPorIdEmpleado(Convert.ToDecimal(this.GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["ID_EMPLEADO"].ToString()));

        Cargar(this.GridView_RESULTADOS_BUSQUEDA);
        if (_dataTable.Rows.Count > 0)
        {
            GridView_RESULTADOS_BUSQUEDA_CASOS_SEVEROS.DataSource = _dataTable;
            GridView_RESULTADOS_BUSQUEDA_CASOS_SEVEROS.DataBind();
            Mostrar(Acciones.BusquedaEncontroCasosSeveros);
        }
        else
        {
            if (!String.IsNullOrEmpty(_casoSevero.MensajeError)) Informar(Label_MENSAJE, "Error: Consulte con el Administrador: " + _casoSevero.MensajeError, Proceso.Error);
            Adicionar();
        }
        _dataTable.Dispose();
    }

    protected void GridView_RESULTADOS_BUSQUEDA_CASOS_SEVEROS_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;
        Buscar();
    }

    protected void GridView_RESULTADOS_BUSQUEDA_CASOS_SEVEROS_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!String.IsNullOrEmpty(this.GridView_RESULTADOS_BUSQUEDA_CASOS_SEVEROS.SelectedDataKey["REGISTRO"].ToString()))
        {
            sancion _sancion = new sancion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            Cargar(_sancion.ObtenerPorRegistro(Convert.ToDecimal(this.GridView_RESULTADOS_BUSQUEDA_CASOS_SEVEROS.SelectedDataKey["REGISTRO"].ToString())));
        }

        Ocultar();
        Mostrar(Acciones.Visualiza);
        Bloquear(Acciones.Visualiza);
    }
    #endregion eventos
}