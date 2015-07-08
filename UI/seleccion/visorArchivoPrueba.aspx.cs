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

        Decimal REGISTRO = Convert.ToDecimal(QueryStringSeguro["registro"]);

        hojasVida _hojasVida = new hojasVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaPruebaAplicadaYa = _hojasVida.ObtenerSelRegAplicacionPrueebasObtenerPorRegistro(REGISTRO);
        DataRow filaPruebaAplicadaYa = tablaPruebaAplicadaYa.Rows[0];

        Int32 ID_SOLICITUD = Convert.ToInt32(filaPruebaAplicadaYa["ID_SOLICITUD"]);
        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaSolicitud = _radicacionHojasDeVida.ObtenerRegSolicitudesingresoPorIdSolicitud(ID_SOLICITUD);
        DataRow filaSolicitud = tablaSolicitud.Rows[0];

        String NOMBRE_ARCHIVO = filaSolicitud["NUM_DOC_IDENTIDAD"].ToString().Trim() + "-PRUEBA-" + filaPruebaAplicadaYa["NOM_PRUEBA"].ToString().Trim();
        NOMBRE_ARCHIVO = NOMBRE_ARCHIVO.Replace(' ','_');

        Response.Clear();
        Response.AddHeader("content-disposition", string.Format("attachment;filename={0}",NOMBRE_ARCHIVO + filaPruebaAplicadaYa["ARCHIVO_EXTENSION"].ToString().Trim()));

        Response.ContentType = filaPruebaAplicadaYa["ARCHIVO_TYPE"].ToString().Trim();

        Response.BinaryWrite((byte[])filaPruebaAplicadaYa["ARCHIVO_PRUEBA"]);

        Response.End();
    }
}