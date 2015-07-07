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

public partial class seleccion_visorPrueba : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal REGISTRO = Convert.ToDecimal(QueryStringSeguro["registro"]);

        descargo _descargo = new descargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoDescargo = _descargo.ObtenerPorRegistro(REGISTRO);

        DataRow filaInfoDescargo = tablaInfoDescargo.Rows[0];



        Response.Clear();
        Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", "DESCARGO_" + REGISTRO.ToString() + filaInfoDescargo["ARCHIVO_EXTENSION"].ToString().Trim()));

        Response.ContentType = filaInfoDescargo["ARCHIVO_TYPE"].ToString().Trim();

       Response.BinaryWrite((byte[])filaInfoDescargo["ARCHIVO_ACTA"]);

       Response.End();
    }
}