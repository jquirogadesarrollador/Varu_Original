using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using System.Text;
using System.IO;

using Brainsbits.LLB;
using Brainsbits.LLB.contratacion;

public partial class contratacion_GenerarAutoliquidacion : System.Web.UI.Page
{
    #region variables
    private enum Acciones
    {
        Iniciar = 0,
        Generar
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }
    #endregion variables

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
        Session.Remove("Reg1TipoRegistro");
        Session.Remove("Reg2TipoRegistro");
        Session.Remove("Inconsistencias");
        Session.Remove("Novedades");

        Session.Remove("Reliquidaciones");
        Session.Remove("Nomina");
        Session.Remove("Vacaciones");
        Session.Remove("Liquidacion");
        Session.Remove("RetirosLps");
        Session.Remove("RetirosNominaMesesAnteriores");
        Session.Remove("DiferenciasSeguridadSocial");

        DataTable dataTable_reg1_tipo_registro = new DataTable();
        Session["Reg1TipoRegistro"] = dataTable_reg1_tipo_registro;

        DataTable dataTable_reg2_tipo_registro = new DataTable();
        Session["Reg2TipoRegistro"] = dataTable_reg2_tipo_registro;

        DataTable dataTable_inconsistencias = new DataTable();
        Session["Inconsistencias"] = dataTable_inconsistencias;

        DataTable dataTable_novedades = new DataTable();
        Session["Novedades"] = dataTable_novedades;

        DataTable dataTable_reliquidaciones = new DataTable();
        Session["Reliquidaciones"] = dataTable_reliquidaciones;

        DataTable dataTable_nomina = new DataTable();
        Session["Nomina"] = dataTable_nomina;

        DataTable dataTable_vacaciones = new DataTable();
        Session["Vacaciones"] = dataTable_vacaciones;

        DataTable dataTable_liquidacion = new DataTable();
        Session["Liquidacion"] = dataTable_liquidacion;

        DataTable dataTable_retiros_lps = new DataTable();
        Session["RetirosLps"] = dataTable_retiros_lps;

        DataTable dataTable_retiros_nomina_meses_anteriores = new DataTable();
        Session["RetirosNominaMesesAnteriores"] = dataTable_retiros_nomina_meses_anteriores;

        DataTable dataTable_diferencias_seguridad_social = new DataTable();
        Session["DiferenciasSeguridadSocial"] = dataTable_diferencias_seguridad_social;

        Cargar();
        Inicializar();
        Ocultar();
    }

    private void Cargar()
    {
        string[] meses = {"Seleccione", "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio", "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"};
        ListItem listItem = new ListItem();

        for(int i=0; i<meses.Length; i++)
        {
            DropDownList_meses.Items.Add(new ListItem(meses[i].ToString(),i.ToString()));
        }
        
        for (int i = 0; i < 10; i++)
        {
            DropDownList_años.Items.Add(new ListItem(((System.DateTime.Now.Year + 1) - i).ToString(), ((System.DateTime.Now.Year + 1) - i).ToString()));
        }
    }
    
    private void Inicializar()
    {
        DropDownList_meses.SelectedValue = System.DateTime.Now.Month.ToString();
        DropDownList_años.SelectedValue = System.DateTime.Now.Year.ToString();
    }

    private void Ocultar()
    {
        Panel_INFO_ADICIONAL_MODULO.Visible = false;
        UpdatePane_MENSAJE.Visible = false;
    }

    private void Identificar()
    {
      
            Autoliquidacion autoliquidacion = new Autoliquidacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

                try
                {
                    if (autoliquidacion.Identificar(this.DropDownList_años.Text, this.DropDownList_meses.SelectedValue))
                    {
                        GridView_empresas.DataSource = autoliquidacion.ObtenerPorPeriodoContable(this.DropDownList_años.Text, this.DropDownList_meses.SelectedValue);
                        GridView_empresas.DataBind();
                    }
                    else Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No es posible identificar las empresas para autoliquidación", Proceso.Error);
                }
                catch (Exception e)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, e.Message, Proceso.Error);
                }
    }

    private void Cargar(DataTable dataTable)
    {

    }

    private void Marcar(DataTable dataTable)
    {

    }

    private void Cargar(GridView gridView)
    {
        Autoliquidacion autoliquidacion = new Autoliquidacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        gridView.DataSource = autoliquidacion.ObtenerPorPeriodoContable(this.DropDownList_años.Text, this.DropDownList_meses.SelectedValue);
        gridView.DataBind();
        autoliquidacion.Dispose();
        Bloquear(gridView);
    }

    private void Bloquear(GridView gridView)
    {
        foreach (GridViewRow gridViewRow in gridView.Rows)
        {
            if (gridViewRow.Cells[6].Text.Equals("Pagado")) gridViewRow.Enabled = false;
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

    private void Marcar(GridView  gridView, bool marcar)
    {
        foreach (GridViewRow gridViewRow in gridView.Rows)
        {
            CheckBox liquidado = (CheckBox)gridViewRow.Cells[0].FindControl("CheckBox_liquidar");
            liquidado.Checked = marcar;
            if (gridViewRow.Cells[6].Text.Equals("Pagado")) liquidado.Checked = false;
        }
    }

    #endregion metodos

    #region eventos


    private void Liquidar(string empresas)
    {
        try
        {
            Autoliquidacion autoliquidacion = new Autoliquidacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            autoliquidacion.Liquidar(this.DropDownList_años.Text, this.DropDownList_meses.SelectedValue, empresas);

            if (autoliquidacion.Reg1TipoRegistro != null) Session["Reg1TipoRegistro"] = autoliquidacion.Reg1TipoRegistro;
            else Button_plano.Enabled = false;

            if (autoliquidacion.Reg2TipoRegistro != null) Session["Reg2TipoRegistro"] = autoliquidacion.Reg2TipoRegistro;
            if (autoliquidacion.Inconsistencias != null) Session["Inconsistencias"] = autoliquidacion.Inconsistencias;
            if (autoliquidacion.Novedades != null) Session["Novedades"] = autoliquidacion.Novedades;
            if (autoliquidacion.Reliquidaciones != null) Session["Reliquidaciones"] = autoliquidacion.Reliquidaciones;
            if (autoliquidacion.Nomina != null) Session["Nomina"] = autoliquidacion.Nomina;
            if (autoliquidacion.Vacaciones != null) Session["Vacaciones"] = autoliquidacion.Vacaciones;
            if (autoliquidacion.Liquidacion != null) Session["Liquidacion"] = autoliquidacion.Liquidacion;
            if (autoliquidacion.RetirosLps != null) Session["RetirosLps"] = autoliquidacion.RetirosLps;
            if (autoliquidacion.RetirosNominaMesesAnteriores != null) Session["RetirosNominaMesesAnteriores"] = autoliquidacion.RetirosNominaMesesAnteriores;
            if (autoliquidacion.DiferenciasSeguridadSocial != null) Session["DiferenciasSeguridadSocial"] = autoliquidacion.DiferenciasSeguridadSocial;


            autoliquidacion.Dispose();

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El proceso de liquidacion de Autoliquidación ha finalizado", Proceso.Correcto);

        }
        catch (Exception ex)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, ex.Message, Proceso.Error);
        }
    }

    private void Descargar(DataTable dataTable, string nombreArchivo)
    {
        Response.ClearContent();
        Response.AddHeader("content-disposition", "attachment;filename=" + nombreArchivo + DateTime.Now.ToString("ddMMyyyy") + ".xls");
        Response.ContentType = "application/vnd.ms-excel";
        if (dataTable != null)
        {
            foreach (DataColumn dc in dataTable.Columns)
            {
                Response.Write(dc.ColumnName + "\t");
            }
            Response.Write(System.Environment.NewLine);
            foreach (DataRow dr in dataTable.Rows)
            {
                for (int i = 0; i < dataTable.Columns.Count; i++)
                {
                    Response.Write(dr[i].ToString() + "\t");
                }
                Response.Write("\n");
            }
            Response.End();
        }
    }

    private void Descargar(DataTable dataTableReg1TipoRegistro, DataTable dataTableReg2TipoRegistro)
    {
        StringBuilder stringBuilder = new StringBuilder();
        string registro = null;
        foreach (DataRow dataRow in dataTableReg1TipoRegistro.Rows)
        {
            registro = string.Empty;
            foreach (DataColumn dataColumn in dataTableReg1TipoRegistro.Columns)
            {
                registro += dataRow[dataColumn];
            }
            stringBuilder.AppendLine(registro.ToString().Replace(",","."));
        }

        foreach (DataRow dataRow in dataTableReg2TipoRegistro.Rows)
        {
            registro = string.Empty;
            foreach (DataColumn dataColumn in dataTableReg2TipoRegistro.Columns)
            {
                registro += dataRow[dataColumn];
            }
            stringBuilder.AppendLine(registro.ToString().Replace(",", "."));
        }

        StringWriter sw;
        HtmlTextWriter htw;
        sw = new StringWriter(stringBuilder);
        htw = new HtmlTextWriter(sw);
        Response.Clear(); 
        Response.ContentType = "text/plain"; 
        Response.AddHeader("Content-Disposition", "attachment;filename=Plano_" + DateTime.Now.ToString("ddMMyyyy") + ".txt"); 
        Response.Output.Write(sw.ToString());
        Response.End();
    }

    private string Leer(GridView gridView)
    {
        string empresas = null;
        foreach (GridViewRow gridViewRow in gridView.Rows)
        {
            CheckBox liquidado = (CheckBox)gridViewRow.Cells[0].FindControl("CheckBox_liquidar");
            if ((liquidado.Checked.Equals(true)) && ((gridViewRow.Cells[6].Text.ToString().Equals("Pendiente")) || (gridViewRow.Cells[6].Text.ToString().Equals("Liquidado")))) empresas += gridViewRow.Cells[2].Text.ToString() + ",";
        }
        return empresas;
    }

    protected void Button_identificar_Click(object sender, EventArgs e)
    {
        Identificar();
        Bloquear(GridView_empresas);
    }
    
    protected void Button_inconsistencias_Click(object sender, EventArgs e)
    {
        Descargar((DataTable)Session["Inconsistencias"], "Inconsistencias_");
    }
    
    protected void Button_novedades_Click(object sender, EventArgs e)
    {
        Descargar((DataTable)Session["Novedades"], "Novedades_");
    }
    
    protected void Button_plano_Click(object sender, EventArgs e)
    {
        Descargar((DataTable)Session["Reg1TipoRegistro"], (DataTable)Session["Reg2TipoRegistro"]);
    }
    
    protected void Button_reliquidaciones_Click(object sender, EventArgs e)
    {
        Descargar((DataTable)Session["Reliquidaciones"], "Reliquidaciones_");
    }
    
    protected void Button_nomina_Click(object sender, EventArgs e)
    {
        Descargar((DataTable)Session["Nomina"], "Nomina_");
    }
    
    protected void Button_vacaciones_Click(object sender, EventArgs e)
    {
        Descargar((DataTable)Session["Vacaciones"], "Vacaciones_");
    }
    
    protected void Button_liquidacion_Click(object sender, EventArgs e)
    {
        Descargar((DataTable)Session["Liquidacion"], "Liquidacion_");
    }
    
    protected void CheckBox_marcar_CheckedChanged(object sender, EventArgs e)
    {
        Marcar(GridView_empresas, CheckBox_marcar.Checked);
        if (CheckBox_marcar.Checked) CheckBox_marcar.Text = "Desmarcar";
        else CheckBox_marcar.Text = "Marcar";
    }
    
    protected void Button_CERRAR_MENSAJE_Click(object sender, EventArgs e)
    {

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
    }

    protected void Button_LIQUIDAR_Click(object sender, EventArgs e)
    {
        string empresas = null;
        empresas = Leer(GridView_empresas);

        if (!string.IsNullOrEmpty(empresas))
        {
            Liquidar(empresas);
            Cargar(GridView_empresas);
        }
        else Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No existen empresas seleccionadas para liquidar", Proceso.Error);

    }

    protected void Button_retiros_lps_Click(object sender, EventArgs e)
    {
        Descargar((DataTable)Session["RetirosLps"], "RetirosLps_");
    }

    protected void Button_retiros_nomina_meses_anteriores_Click(object sender, EventArgs e)
    {
        Descargar((DataTable)Session["RetirosNominaMesesAnteriores"], "RetirosNominaMesesAnteriores_");
    }


    protected void Button_pagar_Click(object sender, EventArgs e)
    {
        string empresas = null;
        empresas = Pagadas(GridView_empresas);
        if (!string.IsNullOrEmpty(empresas))
        {
            Pagar(empresas);
            Cargar(GridView_empresas);
        }
        else Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No existen empresas seleccionadas para liquidar", Proceso.Error);

    }

    private void Pagar(string empresas)
    {
        try
        {
            Autoliquidacion autoliquidacion = new Autoliquidacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            autoliquidacion.Actualizar(this.DropDownList_años.Text, this.DropDownList_meses.SelectedValue, empresas, "Pagado");
            autoliquidacion.Dispose();

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El proceso de actualización de estado para las empresas, ha finalizado", Proceso.Correcto);

        }
        catch (Exception ex)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, ex.Message, Proceso.Error);
        }
    }

    private string Pagadas(GridView gridView)
    {
        string empresas = null;
        foreach (GridViewRow gridViewRow in gridView.Rows)
        {
            CheckBox pagado = (CheckBox)gridViewRow.Cells[0].FindControl("CheckBox_pagar");
            if ((pagado.Checked.Equals(true)) && (gridViewRow.Cells[6].Text.ToString().Equals("Liquidado"))) empresas += gridViewRow.Cells[2].Text.ToString() + ",";
        }
        return empresas;

    }

    protected void Button_diferencias_seguridad_social_Click(object sender, EventArgs e)
    {
        Descargar((DataTable)Session["DiferenciasSeguridadSocial"], "DiferenciasSeguridadSocial_");
    }

    #endregion eventos
}