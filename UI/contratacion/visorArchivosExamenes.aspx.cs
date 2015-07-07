using System;
using System.Data;
using Brainsbits.LLB.maestras;
using TSHAK.Components;
using Brainsbits.LLB.contratacion;

public partial class seleccion_visorPrueba : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal REGISTRO = Convert.ToDecimal(QueryStringSeguro["registro"]);

        examenesEmpleado _examen = new examenesEmpleado(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoExamen = _examen.ObtenerConRegExamenesEmpleadoPorRegistro(Convert.ToInt32(REGISTRO));

        DataRow filaInfoExamen = tablaInfoExamen.Rows[0];

        Response.Clear();
        Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", "RESULTADOS_EXAMEN_" + filaInfoExamen["REGISTRO"].ToString() + filaInfoExamen["ARCHIVO_EXTENSION"].ToString()));

        Response.ContentType = filaInfoExamen["ARCHIVO_TYPE"].ToString().Trim();

        Response.BinaryWrite((byte[])filaInfoExamen["ARCHIVO_EXAMEN"]);

        Response.End();
    }
}