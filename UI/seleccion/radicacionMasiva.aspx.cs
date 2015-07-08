using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using Brainsbits.LLB.seleccion;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB;
using TSHAK.Components;

public partial class _RadicacionMasiva : System.Web.UI.Page
{
    private String NOMBRE_AREA = tabla.NOMBRE_AREA_SELECCION;

    private void RolPermisos()
    {
        #region variables
        int contadorPermisos = 0;
        #endregion variables

        seguridad _seguridad = new seguridad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        tools _tools = new tools();

        String rutaScript = _tools.obtenerRutaVerdaderaScript(Request.ServerVariables["SCRIPT_NAME"]);

        DataTable tablaInformacionPermisos = _seguridad.ObtenerPermisosBotones(NOMBRE_AREA, rutaScript);

        maestrasInterfaz _maestrasInterfaz = new maestrasInterfaz();

        contadorPermisos = _maestrasInterfaz.RolPermisos(this, tablaInformacionPermisos);

        if (contadorPermisos <= 0)
        {
            SecureQueryString QueryStringSeguro;
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

            QueryStringSeguro["img_area"] = "restringido";
            QueryStringSeguro["nombre_area"] = "ACCESO RESTRINGIDO";
            QueryStringSeguro["nombre_modulo"] = "ACCESO RESTRINGIDO";
            QueryStringSeguro["accion"] = "inicial";

            Response.Redirect("~/sinPermisos/sinPermisos.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
        }
        else
        {
            Session["URL_ANTERIOR"] = HttpContext.Current.Request.RawUrl;
        }
    }

    protected void Page_LoadComplete(object sender, EventArgs e)
    {
        RolPermisos();
    }

    private enum Acciones
    {
        Inicio = 0,
        Resultados
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1,
        Advertencia = 2
    }

    private enum AccionesGrilla
    {
        Ninguna,
        Nuevo,
        Modificar,
        Eliminar
    }

    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_UploadFile.Visible = false;

                Panel_FORM_BOTONES.Visible = false;

                Panel_GrillaResultadosCargue.Visible = false;

                Panel_FORM_BOTONES_PIE.Visible = false;
                break;
            case Acciones.Resultados:
                Panel_UploadFile.Visible = false;
                Panel_GrillaResultadosCargue.Visible = false;

                break;
        }
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_UploadFile.Visible = true;
                break;
            case Acciones.Resultados:
                Panel_FORM_BOTONES.Visible = true;
                Panel_GrillaResultadosCargue.Visible = true;
                Panel_FORM_BOTONES_PIE.Visible = true;
                break;
        }
    }

    private void configurar_paneles_popup(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.CssClass = "panel_fondo_popup";

        panel_mensaje.CssClass = "panel_popup";
    }

    private void Configurar()
    {
        configurar_paneles_popup(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    private void Iniciar()
    {
        Configurar();
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        this.Title = "RADICACIÓN MASIVA";
        if (IsPostBack == false)
        {
            Iniciar();
        }
    }

    private void Informar(Panel panel_fondo, System.Web.UI.WebControls.Image imagen_mensaje, Panel panel_mensaje, Label label_mensaje, String mensaje, Proceso proceso)
    {
        panel_fondo.Style.Add("display", "block");
        panel_mensaje.Style.Add("display", "block");

        label_mensaje.Font.Bold = true;

        switch (proceso)
        {
            case Proceso.Correcto:
                label_mensaje.ForeColor = System.Drawing.Color.Green;
                imagen_mensaje.ImageUrl = "~/imagenes/plantilla/ok_popup.png";
                break;
            case Proceso.Error:
                label_mensaje.ForeColor = System.Drawing.Color.Red;
                imagen_mensaje.ImageUrl = "~/imagenes/plantilla/error_popup.png";
                break;
            case Proceso.Advertencia:
                label_mensaje.ForeColor = System.Drawing.Color.Orange;
                imagen_mensaje.ImageUrl = "~/imagenes/plantilla/advertencia_popup.png";
                break;
        }

        panel_fondo.Visible = true;
        panel_mensaje.Visible = true;


        label_mensaje.Text = mensaje;
    }

    private DataTable IniciarTablaErrores()
    {
        DataTable tablaTemp = new DataTable();

        tablaTemp.Columns.Add("CODIGO_ERROR");
        tablaTemp.Columns.Add("LINEA");
        tablaTemp.Columns.Add("CAMPO");
        tablaTemp.Columns.Add("DESCRIPCION");

        return tablaTemp;
    }

    private Boolean IsNumeric(String dato)
    { 
        Regex reNum = new Regex(@"^\d+$");
        if (reNum.Match(dato).Success)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ProcesarArchivo()
    {
        DataTable tablaErrores = IniciarTablaErrores();

        Int32 contador = 0;
        Int32 contadorOmitidos = 0;
        Int32 contadorErrores = 0;
        Int32 contadorIngresados = 0;

        using (StreamReader archivo = new StreamReader(FileUpload_ArchivoPlano.PostedFile.InputStream))
        {
            String linea;
           
            radicacionHojasDeVida _rad = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            while ((linea = archivo.ReadLine()) != null)
            {
                contador += 1;
                String[] campos = linea.Split(';');

                DataRow fila;

                if ((campos.Length < 4) || (campos.Length > 4))
                {
                    fila = tablaErrores.NewRow();
                    fila["CODIGO_ERROR"] = "ERROR_NUM_CAMPOS";
                    fila["LINEA"] = contador;
                    fila["CAMPO"] = null;
                    fila["DESCRIPCION"] = "El numero de campos de la linea (" + campos.Length.ToString() + ") no corresponde al establecido (4).";

                    tablaErrores.Rows.Add(fila);

                    contadorErrores += 1;
                }
                else
                { 
                    if ((campos[0].ToUpper() != "CC") && (campos[0].ToUpper() != "TI") && (campos[0].ToUpper() != "CE"))
                    {
                        fila = tablaErrores.NewRow();
                        fila["CODIGO_ERROR"] = "ERROR_TIPO_DOC_IDENTIDAD";
                        fila["LINEA"] = contador;
                        fila["CAMPO"] = "TIPO_DOCUMENTO_IDENTIDAD";
                        fila["DESCRIPCION"] = "Solo se admiten los siguientes valores CC - CE - TI - PA";

                        tablaErrores.Rows.Add(fila);

                        contadorErrores += 1;
                    }
                    else
                    {
                        if (IsNumeric(campos[1]) == false)
                        {
                            fila = tablaErrores.NewRow();
                            fila["CODIGO_ERROR"] = "ERROR_NON_NUMERIC";
                            fila["LINEA"] = contador;
                            fila["CAMPO"] = "NUMERO_DOCUMENTO";
                            fila["DESCRIPCION"] = "Solo se admiten numeros, sin espacios, lineas, puntos y comas.";

                            tablaErrores.Rows.Add(fila);

                            contadorErrores += 1;
                        }
                        else 
                        {
                            if (String.IsNullOrEmpty(campos[2]) == true)
                            {
                                fila = tablaErrores.NewRow();
                                fila["CODIGO_ERROR"] = "ERROR_NULL";
                                fila["LINEA"] = contador;
                                fila["CAMPO"] = "NOMBRES_ASPIRANTE ";
                                fila["DESCRIPCION"] = "El campos no puede ser nulo o vacio.";

                                tablaErrores.Rows.Add(fila);

                                contadorErrores += 1;
                            }
                            else 
                            {
                                if (String.IsNullOrEmpty(campos[3]) == true)
                                {
                                    fila = tablaErrores.NewRow();
                                    fila["CODIGO_ERROR"] = "ERROR_NULL";
                                    fila["LINEA"] = contador;
                                    fila["CAMPO"] = "APELLIDOS_ASPIRANTE ";
                                    fila["DESCRIPCION"] = "El campos no puede ser nulo o vacio.";

                                    tablaErrores.Rows.Add(fila);

                                    contadorErrores += 1;
                                }
                                else
                                {
                                    _rad.MensajeError = null;

                                    Int32 contadorRegSol = _rad.ObtenerNumRegSolicitudesPorTipDocAndNumDoc(campos[0], campos[1]);
                                    if (contadorRegSol <= 0)
                                    {
                                        if (_rad.MensajeError != null)
                                        {
                                            fila = tablaErrores.NewRow();
                                            fila["CODIGO_ERROR"] = "ERROR_BD";
                                            fila["LINEA"] = contador;
                                            fila["CAMPO"] = null;
                                            fila["DESCRIPCION"] = "Eror en Base de datos al intentar consultar si el registro ya se encuentra: " + _rad.MensajeError;

                                            tablaErrores.Rows.Add(fila);

                                            contadorErrores += 1;
                                        }
                                        else
                                        {
                                            if (_rad.AdicionarRegistroRegSolicitudIngresoMasivo(campos[3], campos[2], campos[0], campos[1]) <= 0)
                                            {
                                                fila = tablaErrores.NewRow();
                                                fila["CODIGO_ERROR"] = "ERROR_BD";
                                                fila["LINEA"] = contador;
                                                fila["CAMPO"] = null;
                                                fila["DESCRIPCION"] = "Eror en Base de datos al intentar ingresar el registro del aspirante: " + _rad.MensajeError;

                                                tablaErrores.Rows.Add(fila);

                                                contadorErrores += 1;
                                            }
                                            else
                                            {
                                                contadorIngresados += 1;
                                            }
                                        }
                                    }
                                    else
                                    { 
                                        contadorOmitidos += 1;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        Label_TotalRegistroProcesados.Text = contador.ToString();
        Label_RegistrosErroneos.Text = contadorErrores.ToString();
        Label_RegistrosInsertados.Text = contadorIngresados.ToString();
        Label_RegistrosOmitidos.Text = contadorOmitidos.ToString();

        GridView_ResultadosCargue.DataSource = tablaErrores;
        GridView_ResultadosCargue.DataBind();

        Ocultar(Acciones.Resultados);
        Mostrar(Acciones.Resultados);

        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El archivo fue procesado, por favor verifique la información en la sección de Resultados del Cargue.", Proceso.Correcto);
    }

    protected void Button_Cargar_Click(object sender, EventArgs e)
    {
        if (FileUpload_ArchivoPlano.HasFile == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Para poder continuar debe seleccionar el archivo plano con los registros que desea cargar en la base de datos.", Proceso.Advertencia);
        }
        else
        {
            String extension = Path.GetExtension(FileUpload_ArchivoPlano.PostedFile.FileName).Substring(1);

            if ((extension.ToUpper() != "csv".ToUpper()) && (extension.ToUpper() != "txt".ToUpper()))
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Para la radicación masiva solo se admiten archivos planos .txt y .csv, El archivo es: ." + extension , Proceso.Advertencia);
            }
            else
            { 
                ProcesarArchivo();
                
            }
        }
    }

    protected void Button_NUEVO_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
    }

    private void ocultar_mensaje(Panel panel_fondo, Panel panel_mensaje)
    {
        panel_fondo.Style.Add("display", "none");
        panel_mensaje.Style.Add("display", "none");

        panel_fondo.Visible = false;
        panel_mensaje.Visible = false;
    }

    protected void Button_CERRAR_MENSAJE_Click(object sender, System.EventArgs e)
    {
        ocultar_mensaje(Panel_FONDO_MENSAJE, Panel_MENSAJES);
    }

    protected void LinkButton__Click(object sender, EventArgs e)
    {

    }
}