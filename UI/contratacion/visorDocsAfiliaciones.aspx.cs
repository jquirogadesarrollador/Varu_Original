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

        Decimal ID_CONTRATO = Convert.ToDecimal(QueryStringSeguro["contrato"]);
        String ENTIDAD_AFILIACION = QueryStringSeguro["afiliacion"];

        afiliacion _afiliacion = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaResutato = _afiliacion.ObtenerDocsRadicacionPorCOntratoYEntidad(ID_CONTRATO, ENTIDAD_AFILIACION);
        DataRow filaInfoAdjunto = tablaResutato.Rows[0];

        Response.Clear();

        Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", "RADICACION_" + ENTIDAD_AFILIACION + "_" + ID_CONTRATO.ToString() + filaInfoAdjunto["ARCHIVO_RADICACION_EXTENSION"]));
        Response.ContentType = filaInfoAdjunto["ARCHIVO_RADICACION_TYPE"].ToString().Trim();
        Response.BinaryWrite((byte[])filaInfoAdjunto["ARCHIVO_RADICACION"]);

       Response.End();
    }
}