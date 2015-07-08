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

        Decimal ID_DOCUMENTO = Convert.ToDecimal(QueryStringSeguro["id_documento"]);

        FabricaAssesment _fabrica = new FabricaAssesment(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaDocumento = _fabrica.ObtenerDocumentoAssesmentPorIdDocumento(ID_DOCUMENTO);

        DataRow filaDocumento = tablaDocumento.Rows[0];

        Response.Clear();
        Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", "Documento_Assesment_" + ID_DOCUMENTO.ToString() + filaDocumento["ARCHIVO_EXTENSION"]));

        Response.ContentType = filaDocumento["ARCHIVO_TYPE"].ToString().Trim();

        Response.BinaryWrite((byte[])filaDocumento["ARCHIVO_DOCUMENTO"]);

        Response.End();
    }
}