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
using Brainsbits.LLB.operaciones;

public partial class visorManualServicio : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_VERSIONAMIENTO = Convert.ToDecimal(QueryStringSeguro["version"]);

        ManualServicio _manualServicio = new ManualServicio(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaResultado = _manualServicio.ObtenerArchivoManualServicioPorVersion(ID_VERSIONAMIENTO);
        DataRow filaInfoResultado = tablaResultado.Rows[0];

        String nombreArchivo = "ManualServicio_Ver-" + filaInfoResultado["VERSION_MAYOR"].ToString().Trim() + "." + filaInfoResultado["VERSION_MENOR"].ToString() + "-" + filaInfoResultado["RAZ_SOCIAL"].ToString().Trim().Replace(' ','_');
        
        Response.Clear();

        Response.AddHeader("content-disposition", String.Format("attachment;filename={0}", nombreArchivo + filaInfoResultado["ARCHIVO_EXTENSION"].ToString().Trim()));
        Response.ContentType = filaInfoResultado["ARCHIVO_TYPE"].ToString().Trim();
        Response.BinaryWrite((byte[])filaInfoResultado["ARCHIVO"]);

        Response.End();
    }
}