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

using Brainsbits.LLB.contratacion;

using TSHAK.Components;

public partial class seleccion_visorPrueba : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal id_incapacidad = Convert.ToDecimal(QueryStringSeguro["id_incapacidad"]);


        incapacidad Incapacidad = new incapacidad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable dataTable = Incapacidad.ObtenerPorIdIncapacidad(id_incapacidad);
        if (dataTable.Rows.Count > 0)
        {
            DataRow dataRow = dataTable.Rows[0];
            Response.Clear();
            Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", "Incapacidad" + dataRow["REGISTRO"].ToString() + dataRow["EXTENSION"].ToString()));

            Response.ContentType = dataRow["TIPO"].ToString().Trim();
            Response.BinaryWrite((byte[])dataRow["ARCHIVO"]);
            Response.End();
        }
    }
}