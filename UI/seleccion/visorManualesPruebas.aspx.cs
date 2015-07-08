using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seleccion;

using TSHAK.Components;

public partial class seleccion_visorPrueba : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal REGISTRO = Convert.ToDecimal(QueryStringSeguro["prueba"]);

        categoriaPruebas _categoriaPruebas = new categoriaPruebas(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaPrueba = _categoriaPruebas.ObtenerSelPruebasPorIdPrueba(Convert.ToInt32(REGISTRO));

        DataRow filaPrueba = tablaPrueba.Rows[0];

        Response.Clear();
        Response.AddHeader("content-disposition", string.Format("attachment;filename={0}", "MANUAL_PRUEBA_" + filaPrueba["ID_PRUEBA"].ToString() + filaPrueba["ARCHIVO_EXTENSION"].ToString().Trim()));

        Response.ContentType = filaPrueba["ARCHIVO_TYPE"].ToString().Trim();

        Response.BinaryWrite((byte[])filaPrueba["ARCHIVO"]);

       Response.End();
    }
}