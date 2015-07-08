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

        Decimal ID_ADJUNTO = Convert.ToDecimal(QueryStringSeguro["id_adjunto"]);

        Programa _aprograma = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaInfo = _aprograma.ObtenerArchivoAdjuntoActividad(ID_ADJUNTO);
        DataRow filaInfoAdjunto = tablaInfo.Rows[0];

        Response.Clear();
        Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", "ADJUNTO_ACTIVIDAD_" + ID_ADJUNTO.ToString() + filaInfoAdjunto["ARCHIVO_EXTENSION"]));
        Response.ContentType = filaInfoAdjunto["ARCHIVO_TYPE"].ToString().Trim();
        Response.BinaryWrite((byte[])filaInfoAdjunto["ARCHIVO"]);
        Response.End();
    }
}