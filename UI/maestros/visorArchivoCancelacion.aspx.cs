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

        Decimal ID_DETALLE = Convert.ToDecimal(QueryStringSeguro["id_detalle"]);

        Programa _prog = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaInfoDetalle = _prog.ObtenerDetalleActividadesPorIdDetalle(ID_DETALLE);

        DataRow filaInfoDetalle = tablaInfoDetalle.Rows[0];

        Response.Clear();

        Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", "ARCHIVO_CANCELACION_" + ID_DETALLE.ToString() + filaInfoDetalle["ARCHIVO_CANCELACION_EXTENSION"]));
        Response.ContentType = filaInfoDetalle["ARCHIVO_CANCELACION_TYPE"].ToString().Trim();
        Response.BinaryWrite((byte[])filaInfoDetalle["ARCHIVO_CANCELACION"]);
        Response.End();
    }
}