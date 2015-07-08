using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seleccion;
using Brainsbits.LLB.contratacion;

using TSHAK.Components;
using Brainsbits.LLB.programasRseGlobal;

public partial class seleccion_visorPrueba : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_HIST_AJUSTE = Convert.ToDecimal(QueryStringSeguro["ID_HIST_AJUSTE"]);

        Programa _prog = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaInfoHistorial = _prog.ObtenerRegistroHistorialAjustePresupuesto(ID_HIST_AJUSTE);

        DataRow filaInfoHist = tablaInfoHistorial.Rows[0];

        Response.Clear();

        Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", "HISTORIAL_AJUSTE_PRESUPUESTO_" + ID_HIST_AJUSTE.ToString() + filaInfoHist["ARCHIVO_EXTENSION"]));
        Response.ContentType = filaInfoHist["ARCHIVO_TYPE"].ToString().Trim();
        Response.BinaryWrite((byte[])filaInfoHist["ARCHIVO"]);
        Response.End();
    }
}