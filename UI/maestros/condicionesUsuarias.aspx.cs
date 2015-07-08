using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;  
using Brainsbits.LLB;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.comercial;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.nomina;
using Brainsbits.LLB.contratacion;

using TSHAK.Components;
using Brainsbits.LLB.seleccion;

public partial class _Default : System.Web.UI.Page
{
 
    #region variables
    private enum Acciones
    {
        Inicio = 0,
        BusquedaEncontro = 1,
        BusquedaNoEncontro = 2,
        Adiciona = 3,
        Guarda = 4,
        Modifica = 5,
        Visualiza = 6,
        Error = 7
    }

    private enum Proceso
    {
        Correcto = 0,
        Error = 1
    }

    #endregion variables

    #region CARGA DE DROPDOWNLIST Y GRIDS
    private void llenarGridCobertura(Decimal idEmpresa)
    {
        cobertura _cobertura = new cobertura(Session["idEmpresa"].ToString());

        DataTable tablaCobertura = _cobertura.obtenerCoberturaDeUnCliente(idEmpresa);

        if (tablaCobertura.Rows.Count > 0)
        {
            GridView_COVERTURA.DataSource = tablaCobertura;
            GridView_COVERTURA.DataBind();
        }

        DataTable tabla_temp = new DataTable();
        tabla_temp.Columns.Add("Código Ciudad");
        tabla_temp.Columns.Add("Regional");
        tabla_temp.Columns.Add("Departamento");
        tabla_temp.Columns.Add("Ciudad");

        DataRow fila_temp;
        foreach (DataRow filaOriginal in tablaCobertura.Rows)
        {
            fila_temp = tabla_temp.NewRow();

            fila_temp["Código Ciudad"] = filaOriginal["Código Ciudad"].ToString();
            fila_temp["Regional"] = filaOriginal["Regional"].ToString();
            fila_temp["Departamento"] = filaOriginal["Departamento"].ToString();
            fila_temp["Ciudad"] = filaOriginal["Ciudad"].ToString();
            tabla_temp.Rows.Add(fila_temp);
        }

        Session["dt_GRID_COVERTURA"] = tabla_temp;

    }
    private void cargar_DropDownList_BUSCAR()
    {
        DropDownList_BUSCAR.Items.Clear();

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_BUSCAR.Items.Add(item);
        item = new ListItem("Código de Cliente", "COD_EMPRESA");
        DropDownList_BUSCAR.Items.Add(item);
        item = new ListItem("Razón social", "RAZ_SOCIAL");
        DropDownList_BUSCAR.Items.Add(item);
        DropDownList_BUSCAR.DataBind();
    }
    #endregion CARGA DE DROPDOWNLIST Y GRIDS

    #region CONFIGURACION DE CONTROLES
    private void iniciar_seccion_de_busqueda()
    {
        TextBox_BUSCAR.Text = "";
        cargar_DropDownList_BUSCAR();
    }
    
    private void configurarInfoAdicionalModulo(Boolean ver, String texto)
    {
        Panel_INFO_ADICIONAL_MODULO.Visible = ver;
        Label_INFO_ADICIONAL_MODULO.Text = texto;
    }
    
    private void configurarMensajes(Boolean mostrarMensaje, System.Drawing.Color color)
    {
        Panel_MENSAJES.Visible = mostrarMensaje;
        Label_MENSAJE.Visible = mostrarMensaje;
        Label_MENSAJE.ForeColor = color;
    }
    
    private void configurarCaracteresAceptadosBusqueda(Boolean letras, Boolean numeros)
    {
        FilteredTextBoxExtender_TextBox_BUSCAR_Numbers.Enabled = numeros;
        FilteredTextBoxExtender_TextBox_BUSCAR_Letras.Enabled = letras;
    }

    private void mostrar_botones_internos_segun_proceso()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        int proceso = Convert.ToInt32(QueryStringSeguro["proceso"]);
        String reg = QueryStringSeguro["reg"];
        int empresa = Convert.ToInt32(Session["idEmpresa"]);

        TableRow filaTabla;
        TableCell celdaTabla;
        HyperLink link;
        Image imagen;

        int contadorFilas = 0;

        #region ContactoSeleccion
        if (proceso == ((int)tabla.proceso.ContactoSeleccion))
        {
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
            QueryStringSeguro["img_area"] = "seleccion";
            QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
            QueryStringSeguro["accion"] = "inicial";
            QueryStringSeguro["reg"] = reg;

            filaTabla = new TableRow();
            filaTabla.ID = "row_" + contadorFilas.ToString();

            celdaTabla = new TableCell();
            celdaTabla.ID = "cell_1_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_condiciones_envio";
            QueryStringSeguro["nombre_modulo"] = "CONDICIONES DE ENVIO";
            link.NavigateUrl = "~/seleccion/condicionesSeleccion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bCondicionesEnvioEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bCondicionesEnvioAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bCondicionesEnvioEstandar.png'");

            imagen.CssClass = "botones_menu_principal";
            link.CssClass = "botones_menu_principal";

            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);


            celdaTabla = new TableCell();
            celdaTabla.ID = "cell_2_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_definicion_perfiles";
            QueryStringSeguro["nombre_modulo"] = "DEFINICIÓN DE PERFILES";
            link.NavigateUrl = "~/seleccion/perfiles.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bDefinicionPerfilesEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bDefinicionPerfilesAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bDefinicionPerfilesEstandar.png'");

            imagen.CssClass = "botones_menu_principal";
            link.CssClass = "botones_menu_principal";

            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);


            celdaTabla = new TableCell();
            celdaTabla.ID = "cell_3_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_contactos_seleccion";
            QueryStringSeguro["nombre_modulo"] = "CONTÁCTOS DE SELECCIÓN";
            QueryStringSeguro["proceso"] = ((int)tabla.proceso.ContactoSeleccion).ToString();
            link.NavigateUrl = "~/maestros/contactos.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bContactosSeleccionEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bContactosSeleccionAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bContactosSeleccionEstandar.png'");

            imagen.CssClass = "botones_menu_principal";
            link.CssClass = "botones_menu_principal";

            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);




            Table_MENU.Rows.Add(filaTabla);
        }
        #endregion ContactoSeleccion

        #region ContactoContratacion
        if (proceso == ((int)tabla.proceso.ContactoContratacion))
        {
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
            QueryStringSeguro["img_area"] = "contratacion";
            QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
            QueryStringSeguro["accion"] = "inicial";
            QueryStringSeguro["reg"] = reg;

            filaTabla = new TableRow();
            filaTabla.ID = "row_" + contadorFilas.ToString();



            celdaTabla = new TableCell();
            celdaTabla.ID = "cell_1_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_condiciones_envio";
            QueryStringSeguro["nombre_modulo"] = "CONDICIONES CONTRATACIÓN";
            QueryStringSeguro["grid_pagina"] = "0";
            QueryStringSeguro["filtro"] = "SIN_FILTRO";
            QueryStringSeguro["drop"] = String.Empty;
            QueryStringSeguro["dato"] = String.Empty;

            if (empresa == 1)
            {
                link.NavigateUrl = "~/contratacion/condicionesContratacion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            }
            else
            {
                link.NavigateUrl = "~/contratacion/condicionesContratacion.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            }
            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bCondicionesContratacionEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bCondicionesContratacionAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bCondicionesContratacionEstandar.png'");

            imagen.CssClass = "botones_menu_principal";
            link.CssClass = "botones_menu_principal";

            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);


            celdaTabla = new TableCell();
            celdaTabla.ID = "cell_2_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_centros_costo";
            QueryStringSeguro["nombre_modulo"] = "CENTROS DE COSTOS";
            link.NavigateUrl = "~/contratacion/centrosCosto.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bCentrosCostoEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bCentrosCostoAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bCentrosCostoEstandar.png'");

            imagen.CssClass = "botones_menu_principal";
            link.CssClass = "botones_menu_principal";

            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);


            celdaTabla = new TableCell();
            celdaTabla.ID = "cell_3_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_contactos";
            QueryStringSeguro["nombre_modulo"] = "CONTÁCTOS DE CONTRATACIÓN";
            QueryStringSeguro["proceso"] = ((int)tabla.proceso.ContactoContratacion).ToString();
            link.NavigateUrl = "~/maestros/contactos.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bContactosContratacionEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bContactosContratacionAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bContactosContratacionEstandar.png'");

            imagen.CssClass = "botones_menu_principal";
            link.CssClass = "botones_menu_principal";

            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);



            celdaTabla = new TableCell();
            celdaTabla.ID = "cell_4_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_bancos";
            QueryStringSeguro["nombre_modulo"] = "CONFIGURAR BANCOS";
            link.NavigateUrl = "~/contratacion/bancosPorCiudad.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bConfigurarBancosEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bConfigurarBancosAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bConfigurarBancosEstandar.png'");

            imagen.CssClass = "botones_menu_principal";
            link.CssClass = "botones_menu_principal";

            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);


            celdaTabla = new TableCell();
            celdaTabla.ID = "cell_5_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_configuracion_documentos_empresa_usuaria";
            QueryStringSeguro["nombre_modulo"] = "CONFIGURACION DOCUMENTOS EMPRESA USUARIA";
            link.NavigateUrl = "~/contratacion/configuracionDocumentosEmpresaUsuaria.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bConfiguracionDocumentosEmpresaUsuariaEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bConfiguracionDocumentosEmpresaUsuariaAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bConfiguracionDocumentosEmpresaUsuariaEstandar.png'");

            imagen.CssClass = "botones_menu_principal";
            link.CssClass = "botones_menu_principal";

            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);

            Table_MENU.Rows.Add(filaTabla);

            contadorFilas = 0;
            filaTabla = new TableRow();
            filaTabla.ID = "t1_row_" + contadorFilas.ToString();

            celdaTabla = new TableCell();
            celdaTabla.ID = "t1_cell_1_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_rotacion_retiro";
            QueryStringSeguro["nombre_modulo"] = "ASOCIACIÓN DE MOTIVOS DE ROTACIÓN Y RETIRO";
            QueryStringSeguro["proceso"] = ((int)tabla.proceso.ContactoContratacion).ToString();
            QueryStringSeguro["ID_EMPRESA"] = "CONFIGURACIÓN MOTIVOS DE ROTACIÓN Y RETIRO";

            link.NavigateUrl = "~/contratacion/asociarMotivosRotacionRetiro.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bAsociarMotivosRotacionRetiroEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bAsociarMotivosRotacionRetiroAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bAsociarMotivosRotacionRetiroEstandar.png'");

            imagen.CssClass = "botones_menu_principal";
            link.CssClass = "botones_menu_principal";

            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);


            celdaTabla = new TableCell();
            celdaTabla.ID = "t1_cell_2_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_clausulas";
            QueryStringSeguro["nombre_modulo"] = "ADMINISTRACIÓN DE CLAUSULAS";
            link.NavigateUrl = "~/contratacion/Clausulas.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.CssClass = "botones_menu_principal";
            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bBibliotecaClausulasEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bBibliotecaClausulasAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bBibliotecaClausulasEstandar.png'");
            imagen.CssClass = "botones_menu_principal";
            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);



            Table_MENU_1.Rows.Add(filaTabla);
        }
        #endregion ContactoContratacion

        #region contacto nomina
        if (proceso == ((int)tabla.proceso.ContactoNominaFacturacion))
        {
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
            QueryStringSeguro["img_area"] = "nomina";
            QueryStringSeguro["nombre_area"] = "NÓMINA Y FACTURACIÓN";
            QueryStringSeguro["accion"] = "inicial";
            QueryStringSeguro["reg"] = reg;

            filaTabla = new TableRow();
            filaTabla.ID = "row_" + contadorFilas.ToString();

            celdaTabla = new TableCell();
            celdaTabla.ID = "cell_1_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_condiciones_nomina";
            QueryStringSeguro["nombre_modulo"] = "CONDICIONES DE NÓMINA";
            if (empresa == 1)
            {
                link.NavigateUrl = "~/nomina/condicionesNomina.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            }
            else
            {
                link.NavigateUrl = "~/nomina/condicionesNomina.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            }

            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bMenuCondicionesNominaEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bMenuCondicionesNominaAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bMenuCondicionesNominaEstandar.png'");

            imagen.CssClass = "botones_menu_principal";
            link.CssClass = "botones_menu_principal";

            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);

            celdaTabla = new TableCell();
            celdaTabla.ID = "cell_2_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_contactos";
            QueryStringSeguro["nombre_modulo"] = "CONTÁCTOS DE NÓMINA";
            QueryStringSeguro["proceso"] = ((int)tabla.proceso.ContactoNominaFacturacion).ToString();
            link.NavigateUrl = "~/maestros/contactos.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bContactosNominaEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bContactosNominaAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bContactosNominaEstandar.png'");

            imagen.CssClass = "botones_menu_principal";
            link.CssClass = "botones_menu_principal";

            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);

            Table_MENU.Rows.Add(filaTabla);
        }
        #endregion contacto nomina

        #region nomina
        if (proceso == ((int)tabla.proceso.Nomina))
        {
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
            QueryStringSeguro["img_area"] = "nomina";
            QueryStringSeguro["nombre_area"] = "NÓMINA Y FACTURACIÓN";
            QueryStringSeguro["accion"] = "inicial";
            QueryStringSeguro["reg"] = reg;

            filaTabla = new TableRow();
            filaTabla.ID = "row_" + contadorFilas.ToString();

            celdaTabla = new TableCell();
            celdaTabla.ID = "cell_3_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_novedades_nomina";
            QueryStringSeguro["nombre_modulo"] = "CAPTURA NOVEDADES DE NOMINA";
            link.NavigateUrl = "~/nomina/novedadesNomina.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.CssClass = "botones_menu_principal";
            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bNovedadesNominasEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bNovedadesNominasAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bNovedadesNominasEstandar.png'");
            imagen.CssClass = "botones_menu_principal";
            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);


            celdaTabla = new TableCell();
            celdaTabla.ID = "cell_1_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_liquidaciones_nomina";
            QueryStringSeguro["nombre_modulo"] = "LIQUIDACION DE NOMINA";
            link.NavigateUrl = "~/nomina/liquidacionNomina.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.CssClass = "botones_menu_principal";
            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bLiquidacionNominaEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bLiquidacionNominaAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bLiquidacionNominaEstandar.png'");
            imagen.CssClass = "botones_menu_principal";
            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);

            celdaTabla = new TableCell();
            celdaTabla.ID = "cell_4_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_novedades_memorandos";
            QueryStringSeguro["nombre_modulo"] = "CAPTURA NOVEDADES MEMORANDOS";
            link.NavigateUrl = "~/nomina/novedadesMemos.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.CssClass = "botones_menu_principal";
            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bNovedadesMemosEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bNovedadesMemosAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bNovedadesMemosEstandar.png'");
            imagen.CssClass = "botones_menu_principal";
            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);

            celdaTabla = new TableCell();
            celdaTabla.ID = "cell_2_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_liquidacion_memorandos";
            QueryStringSeguro["nombre_modulo"] = "LIQUIDACION DE MEMORANDOS";
            link.NavigateUrl = "~/nomina/liquidacionMemorandos.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.CssClass = "botones_menu_principal";
            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bLiquidacionMemosEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bLiquidacionMemosAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bLiquidacionMemosEstandar.png'");

            imagen.CssClass = "botones_menu_principal";
            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);

            Table_MENU.Rows.Add(filaTabla);



            contadorFilas += 1;
            filaTabla = new TableRow();
            filaTabla.ID = "row_2" + contadorFilas.ToString();


            contadorFilas += 1;
            filaTabla = new TableRow();
            filaTabla.ID = "row_" + contadorFilas.ToString();


            celdaTabla = new TableCell();
            celdaTabla.ID = "cell_3_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_primas";
            QueryStringSeguro["nombre_modulo"] = "LIQUIDACIÓN PRIMAS";
            link.NavigateUrl = "~/nomina/liquidacionPrimas.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.CssClass = "botones_menu_principal";
            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bLiquidacionPrimasEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bLiquidacionPrimasAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bLiquidacionPrimasEstandar.png'");
            imagen.CssClass = "botones_menu_principal";
            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);
            

            celdaTabla = new TableCell();
            celdaTabla.ID = "cell_31_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_condiciones_nomina";
            QueryStringSeguro["nombre_modulo"] = "CONDICIONES DE NÓMINA";
            if (empresa == 1)
            {
                link.NavigateUrl = "~/nomina/condicionesNomina.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            }
            else
            {
                link.NavigateUrl = "~/nomina/condicionesNomina.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            }

            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bMenuCondicionesNominaEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bMenuCondicionesNominaAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bMenuCondicionesNominaEstandar.png'");

            imagen.CssClass = "botones_menu_principal";
            link.CssClass = "botones_menu_principal";

            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);


            celdaTabla = new TableCell();
            celdaTabla.ID = "cell_41_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_contactos_seleccion";
            QueryStringSeguro["nombre_modulo"] = "CONTÁCTOS DE NÓMINA";
            QueryStringSeguro["proceso"] = ((int)tabla.proceso.Nomina).ToString();
            link.NavigateUrl = "~/maestros/contactos.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bContactosNominaEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bContactosNominaAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bContactosNominaEstandar.png'");

            imagen.CssClass = "botones_menu_principal";
            link.CssClass = "botones_menu_principal";

            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);
          
            
            Table_MENU_2.Rows.Add(filaTabla);
        }
        #endregion nomina

        #region rse
        if (proceso == ((int)tabla.proceso.ContactoRse))
        {
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
            QueryStringSeguro["img_area"] = "rse";
            QueryStringSeguro["nombre_area"] = "RESPONSABILIDAD SOCIAL EMPRESARIAL";
            QueryStringSeguro["accion"] = "inicial";
            QueryStringSeguro["reg"] = reg;

            filaTabla = new TableRow();
            filaTabla.ID = "row_" + contadorFilas.ToString();

            celdaTabla = new TableCell();
            celdaTabla.ID = "cell_1_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_presupuestos";
            QueryStringSeguro["nombre_modulo"] = "ASIGNACIÓN DE PRESUPUESTOS";
            QueryStringSeguro["proceso"] = QueryStringSeguro["proceso"] = ((int)tabla.proceso.ContactoRse).ToString();

            link.NavigateUrl = "~/maestros/AsignacionPresupuestosProgramas.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.CssClass = "botones_menu_principal";
            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bAsignacionPresupuestosEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bAsignacionPresupuestosAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bAsignacionPresupuestosEstandar.png'");
            imagen.CssClass = "botones_menu_principal";
            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);

            celdaTabla = new TableCell();
            celdaTabla.ID = "cell_2_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_programasyactividades";
            QueryStringSeguro["nombre_modulo"] = "ADMINISTRACIÓN DE PROGRAMAS Y ACTIVIDADES";
            QueryStringSeguro["proceso"] = QueryStringSeguro["proceso"] = ((int)tabla.proceso.ContactoRse).ToString();

            link.NavigateUrl = "~/maestros/AdminProgramasActividades.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.CssClass = "botones_menu_principal";
            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bAdminProgramasActiviadesEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bAdminProgramasActiviadesAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bAdminProgramasActiviadesEstandar.png'");
            imagen.CssClass = "botones_menu_principal";
            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);

            Table_MENU.Rows.Add(filaTabla);
        }
        #endregion rse

        #region global salud
        if (proceso == ((int)tabla.proceso.ContactoGlobalSalud))
        {
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
            QueryStringSeguro["img_area"] = "globalsalud";
            QueryStringSeguro["nombre_area"] = "SALUD INTEGRAL";
            QueryStringSeguro["accion"] = "inicial";
            QueryStringSeguro["reg"] = reg;

            filaTabla = new TableRow();
            filaTabla.ID = "row_" + contadorFilas.ToString();

            celdaTabla = new TableCell();
            celdaTabla.ID = "cell_1_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_presupuestos";
            QueryStringSeguro["nombre_modulo"] = "ASIGNACIÓN DE PRESUPUESTOS";
            QueryStringSeguro["proceso"] = QueryStringSeguro["proceso"] = ((int)tabla.proceso.ContactoGlobalSalud).ToString();

            link.NavigateUrl = "~/maestros/AsignacionPresupuestosProgramas.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.CssClass = "botones_menu_principal";
            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bAsignacionPresupuestosEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bAsignacionPresupuestosAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bAsignacionPresupuestosEstandar.png'");
            imagen.CssClass = "botones_menu_principal";
            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);

            celdaTabla = new TableCell();
            celdaTabla.ID = "cell_2_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_programasyactividades";
            QueryStringSeguro["nombre_modulo"] = "ADMINISTRACIÓN DE PROGRAMAS Y ACTIVIDADES";
            QueryStringSeguro["proceso"] = QueryStringSeguro["proceso"] = ((int)tabla.proceso.ContactoGlobalSalud).ToString();

            link.NavigateUrl = "~/maestros/AdminProgramasActividades.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.CssClass = "botones_menu_principal";
            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bAdminProgramasActiviadesEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bAdminProgramasActiviadesAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bAdminProgramasActiviadesEstandar.png'");
            imagen.CssClass = "botones_menu_principal";
            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);

            Table_MENU.Rows.Add(filaTabla);
        }
        #endregion global salud

        #region BIENESTAR
        if (proceso == ((int)tabla.proceso.ContactoBienestarSocial))
        {
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
            QueryStringSeguro["img_area"] = "bienestarsocial";
            QueryStringSeguro["nombre_area"] = "BIENESTAR SOCIAL";
            QueryStringSeguro["accion"] = "inicial";
            QueryStringSeguro["reg"] = reg;

            filaTabla = new TableRow();
            filaTabla.ID = "row_" + contadorFilas.ToString();

            celdaTabla = new TableCell();
            celdaTabla.ID = "cell_1_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_presupuestos";
            QueryStringSeguro["nombre_modulo"] = "ASIGNACIÓN DE PRESUPUESTOS";
            QueryStringSeguro["proceso"] = QueryStringSeguro["proceso"] = ((int)tabla.proceso.ContactoBienestarSocial).ToString();

            link.NavigateUrl = "~/maestros/AsignacionPresupuestosProgramas.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.CssClass = "botones_menu_principal";
            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bAsignacionPresupuestosEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bAsignacionPresupuestosAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bAsignacionPresupuestosEstandar.png'");
            imagen.CssClass = "botones_menu_principal";
            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);


            celdaTabla = new TableCell();
            celdaTabla.ID = "cell_2_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_programasyactividades";
            QueryStringSeguro["nombre_modulo"] = "ADMINISTRACIÓN DE PROGRAMAS Y ACTIVIDADES";
            QueryStringSeguro["proceso"] = QueryStringSeguro["proceso"] = ((int)tabla.proceso.ContactoBienestarSocial).ToString();

            link.NavigateUrl = "~/maestros/AdminProgramasActividades.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.CssClass = "botones_menu_principal";
            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bAdminProgramasActiviadesEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bAdminProgramasActiviadesAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bAdminProgramasActiviadesEstandar.png'");
            imagen.CssClass = "botones_menu_principal";
            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);

            Table_MENU.Rows.Add(filaTabla);
        }
        #endregion BIENESTAR

        #region LiquidacionPrestaciones
        if (proceso == ((int)tabla.proceso.ContactoLiquidacionPrestaciones))
        {
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());
            QueryStringSeguro["img_area"] = "nomina";
            QueryStringSeguro["nombre_area"] = "NÓMINA Y FACTURACIÓN";
            QueryStringSeguro["accion"] = "inicial";
            QueryStringSeguro["reg"] = reg;

            filaTabla = new TableRow();
            filaTabla.ID = "row_" + contadorFilas.ToString();




            celdaTabla = new TableCell();
            celdaTabla.ID = "cell_1_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_liquidaciones_cesantias";
            QueryStringSeguro["nombre_modulo"] = "LIQUIDACION DE CESANTIAS";
            link.NavigateUrl = "~/nomina/liquidacionCesantias.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.CssClass = "botones_menu_principal";
            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bLiquidacionCesantiasEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bLiquidacionCesantiasAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bLiquidacionCesantiasEstandar.png'");
            imagen.CssClass = "botones_menu_principal";
            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);


            celdaTabla = new TableCell();
            celdaTabla.ID = "cell_4_row_" + contadorFilas.ToString();
            link = new HyperLink();
            link.ID = "link_liquidacion_vacaciones";
            QueryStringSeguro["nombre_modulo"] = "LIQUIDACION VACACIONES";
            link.NavigateUrl = "~/nomina/liquidacionVacaciones.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString());
            link.CssClass = "botones_menu_principal";
            link.Target = "_blank";
            imagen = new Image();
            imagen.ImageUrl = "~/imagenes/areas/bLiquidacionVacacionesEstandar.png";
            imagen.Attributes.Add("onmouseover", "this.src='../imagenes/areas/bLiquidacionVacacionesAccion.png'");
            imagen.Attributes.Add("onmouseout", "this.src='../imagenes/areas/bLiquidacionVacacionesEstandar.png'");
            imagen.CssClass = "botones_menu_principal";
            link.Controls.Add(imagen);

            celdaTabla.Controls.Add(link);

            filaTabla.Cells.Add(celdaTabla);

            Table_MENU.Rows.Add(filaTabla);
        }
        #endregion LiquidacionPrestaciones
    }
    #endregion CONFIGURACION DE CONTROLES

    #region FUNCIONES SE EJECUTAN AL INICIAR LA PAGINA
    private void iniciar_interfaz_inicial()
    {
        configurarInfoAdicionalModulo(false, "");

        Panel_BOTONES_INTERNOS.Visible = false;

        configurarMensajes(false, System.Drawing.Color.Red);

        Panel_RESULTADOS_GRID.Visible = false;

        Panel_FormularioSeleccion.Visible = false;
        Panel_FormularioContratacion.Visible = false;
        Panel_FORMULARIO.Visible = false;
    }

    private void CargarTablaConfiguradaParaExamenesMedicosContratacion(DataTable tablaOriginal)
    {
        DataTable tablaResultado = new DataTable();

        tablaResultado.Columns.Add("NOMBRE_EXAMEN");
        tablaResultado.Columns.Add("CARGOS");

        String NOMBRE_EXAMEN = string.Empty;
        String LISTA_CARGOS = String.Empty;

        for (int i = 0; i < tablaOriginal.Rows.Count; i++)
        { 
            DataRow filaOriginal = tablaOriginal.Rows[i];

            if (NOMBRE_EXAMEN != filaOriginal["NOMBRE"].ToString())
            {
                if (i > 0)
                {
                    DataRow filaResultado = tablaResultado.NewRow();

                    filaResultado["NOMBRE_EXAMEN"] = NOMBRE_EXAMEN;
                    filaResultado["CARGOS"] = LISTA_CARGOS;

                    tablaResultado.Rows.Add(filaResultado);
                }

                if (filaOriginal["BASICO"].ToString().Trim() == "S")
                {
                    LISTA_CARGOS = "TODOS";
                }
                else
                {
                    LISTA_CARGOS = filaOriginal["NOM_OCUPACION"].ToString().Trim();
                }

                NOMBRE_EXAMEN = filaOriginal["NOMBRE"].ToString();
            }
            else
            {
                if (filaOriginal["BASICO"].ToString().Trim() == "S")
                {
                    LISTA_CARGOS = "TODOS";
                }
                else
                {
                    LISTA_CARGOS += "•" + filaOriginal["NOM_OCUPACION"].ToString().Trim();
                }
            }
        }

        GridView_ExamenesMedicosContratacion.DataSource = tablaResultado;
        GridView_ExamenesMedicosContratacion.DataBind();

        for (int i = 0; i < GridView_ExamenesMedicosContratacion.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ExamenesMedicosContratacion.Rows[i];
            DropDownList listaCargos = filaGrilla.FindControl("DropDownList_Cargos") as DropDownList;

            DataRow filaTabla = tablaResultado.Rows[(GridView_ExamenesMedicosContratacion.PageIndex * GridView_ExamenesMedicosContratacion.PageSize) + i];

            String[] arrayCargos = filaTabla["CARGOS"].ToString().Trim().Split('•');

            listaCargos.Items.Clear();

            listaCargos.Items.Add("Seleccionar el cargo");
            foreach (String cargo in arrayCargos)
            {
                listaCargos.Items.Add(cargo);
            }
        }
    }

    private void CargarTablaConfiguradaParaPruebasCargosSeleccion(DataTable tablaOriginal)
    {
        DataTable tablaResultado = new DataTable();

        tablaResultado.Columns.Add("NOMBRE_PRUEBA");
        tablaResultado.Columns.Add("CARGOS");

        String NOMBRE_PRUEBA = string.Empty;
        String LISTA_CARGOS = String.Empty;

        for (int i = 0; i < tablaOriginal.Rows.Count; i++)
        {
            DataRow filaOriginal = tablaOriginal.Rows[i];

            if (NOMBRE_PRUEBA != filaOriginal["NOM_PRUEBA"].ToString().Trim())
            {
                if (i > 0)
                {
                    DataRow filaResultado = tablaResultado.NewRow();

                    filaResultado["NOMBRE_PRUEBA"] = NOMBRE_PRUEBA;
                    filaResultado["CARGOS"] = LISTA_CARGOS;

                    tablaResultado.Rows.Add(filaResultado);
                }
    
                LISTA_CARGOS = filaOriginal["NOM_OCUPACION"].ToString().Trim();
               
                NOMBRE_PRUEBA = filaOriginal["NOM_PRUEBA"].ToString().Trim();
            }
            else
            {
                LISTA_CARGOS += "•" + filaOriginal["NOM_OCUPACION"].ToString().Trim();
            }
        }

        GridView_PruebasCargos.DataSource = tablaResultado;
        GridView_PruebasCargos.DataBind();

        for (int i = 0; i < GridView_PruebasCargos.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_PruebasCargos.Rows[i];
            DropDownList listaCargos = filaGrilla.FindControl("DropDownList_Cargos") as DropDownList;

            DataRow filaTabla = tablaResultado.Rows[(GridView_PruebasCargos.PageIndex * GridView_PruebasCargos.PageSize) + i];

            String[] arrayCargos = filaTabla["CARGOS"].ToString().Trim().Split('•');

            listaCargos.Items.Clear();

            foreach (String cargo in arrayCargos)
            {
                listaCargos.Items.Add(cargo);
            }
        }
    }

    private void CargarTablaConfiguradaParaBancosContratacion(DataTable tablaOriginal)
    {
        DataTable tablaResultado = new DataTable();

        tablaResultado.Columns.Add("NOMBRE_CIUDAD");
        tablaResultado.Columns.Add("BANCOS");

        String NOMBRE_CIUDAD = string.Empty;
        String LISTA_BANCOS = String.Empty;

        for (int i = 0; i < tablaOriginal.Rows.Count; i++)
        {
            DataRow filaOriginal = tablaOriginal.Rows[i];

            if (NOMBRE_CIUDAD != filaOriginal["NOMBRE"].ToString())
            {
                if (i > 0)
                {
                    DataRow filaResultado = tablaResultado.NewRow();

                    filaResultado["NOMBRE_CIUDAD"] = NOMBRE_CIUDAD;
                    filaResultado["BANCOS"] = LISTA_BANCOS;

                    tablaResultado.Rows.Add(filaResultado);
                }

                LISTA_BANCOS = filaOriginal["NOM_BANCO"].ToString().Trim();

                NOMBRE_CIUDAD = filaOriginal["NOMBRE"].ToString();
            }
            else
            {
                LISTA_BANCOS += "•" + filaOriginal["NOM_BANCO"].ToString().Trim();
            }
        }

        GridView_BancosContratacion.DataSource = tablaResultado;
        GridView_BancosContratacion.DataBind();

        for (int i = 0; i < GridView_BancosContratacion.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_BancosContratacion.Rows[i];
            DropDownList listaBancos = filaGrilla.FindControl("DropDownList_Bancos") as DropDownList;

            DataRow filaTabla = tablaResultado.Rows[(GridView_BancosContratacion.PageIndex * GridView_BancosContratacion.PageSize) + i];

            String[] arrayBancos = filaTabla["BANCOS"].ToString().Trim().Split('•');

            listaBancos.Items.Clear();

            listaBancos.Items.Add("Seleccionar el banco");
            foreach (String banco in arrayBancos)
            {
                listaBancos.Items.Add(banco);
            }
        }
    }

    private void CargarFormularioContratacion(Decimal ID_EMPRESA, tabla.proceso proceso)
    {
        contactos _contacto = new contactos(Session["idEmpresa"].ToString());
        DataTable tablaContactosOriginal = _contacto.ObtenerContactosPorIdEmpresa(ID_EMPRESA,  proceso);

        HiddenField_contratacion_idEmpresa.Value = ID_EMPRESA.ToString();

        GridView_ContactosContratacion.DataSource = tablaContactosOriginal;
        GridView_ContactosContratacion.DataBind();

        condicionesContratacion _condContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaExamenesMedicos = _condContratacion.ObtenerExamenesVSCargosPorIdEmpresa(Convert.ToDecimal(HiddenField_contratacion_idEmpresa.Value));
        CargarTablaConfiguradaParaExamenesMedicosContratacion(tablaExamenesMedicos);

        DataTable tablaBancosPorCiudad = _condContratacion.ObtenerBancosVSCiudadesPorIdEmpresa(Convert.ToDecimal(HiddenField_contratacion_idEmpresa.Value));
        CargarTablaConfiguradaParaBancosContratacion(tablaBancosPorCiudad);

        Cargar(GridView_contratacion_cobertura, Convert.ToDecimal(HiddenField_contratacion_idEmpresa.Value));
    }

    private void Cargar(GridView gridView, decimal IdEmpresa)
    {
        cobertura cobertura = new cobertura(Session["idEmpresa"].ToString());
        DataTable dataTable = cobertura.obtenerCoberturaDeUnCliente(IdEmpresa);

        if (dataTable.Rows.Count > 0)
        {
            gridView.DataSource = dataTable;
            gridView.DataBind();
        }
        if (dataTable != null) dataTable.Dispose();
    }

    private void Cargar(GridView gridView, decimal IdEmpresa, string idCiudad)
    {
            centroCosto centroCosto = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable dataTable = centroCosto.ObtenerCentrosDeCostoPorIdEmpresaIdCiudad(IdEmpresa, idCiudad);

            if (dataTable.Rows.Count > 0)
            {
                gridView.DataSource = dataTable;
                gridView.DataBind();
            }
            if (dataTable != null) dataTable.Dispose();
    }

    private void Cargar(GridView gridView, decimal IdEmpresa, decimal idCentroCosto)
    {
        subCentroCosto subCentroCosto = new subCentroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable dataTable = subCentroCosto.ObtenerSubCentrosDeCostoPorIdEmpresaIdCentroCosto(IdEmpresa, idCentroCosto);

        if (dataTable.Rows.Count > 0)
        {
            gridView.DataSource = dataTable;
            gridView.DataBind();
        }
        if (dataTable != null) dataTable.Dispose();
    }

    private void CargarFormularioComercial(DataRow informacionEmpresa, cliente _cliente)
    {
        Panel_CONTROL_REGISTRO.Visible = true;
        Panel_CONTROL_REGISTRO.Enabled = false;
        TextBox_USU_CRE.Text = informacionEmpresa["USU_CRE"].ToString();
        try
        {
            TextBox_FCH_CRE.Text = DateTime.Parse(informacionEmpresa["FCH_CRE"].ToString()).ToShortDateString();
            TextBox_HOR_CRE.Text = DateTime.Parse(informacionEmpresa["FCH_CRE"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_CRE.Text = "";
            TextBox_HOR_CRE.Text = "";
        }
        TextBox_USU_MOD.Text = informacionEmpresa["USU_MOD"].ToString();
        try
        {
            TextBox_FCH_MOD.Text = DateTime.Parse(informacionEmpresa["FCH_MOD"].ToString()).ToShortDateString();
            TextBox_HOR_MOD.Text = DateTime.Parse(informacionEmpresa["FCH_MOD"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_MOD.Text = "";
            TextBox_HOR_MOD.Text = "";
        }

        Panel_COD_EMPRESA.Visible = true;
        TextBox_COD_EMPRESA.Enabled = true;
        TextBox_COD_EMPRESA.ReadOnly = true;
        TextBox_COD_EMPRESA.Text = informacionEmpresa["COD_EMPRESA"].ToString().Trim();

        TextBox_FCH_INGRESO.Text = DateTime.Parse(informacionEmpresa["FCH_INGRESO"].ToString().Trim()).ToShortDateString().Trim();
        TextBox_FCH_INGRESO.ReadOnly = true;
        TextBox_NIT_EMPRESA.Text = informacionEmpresa["NIT_EMPRESA"].ToString().Trim();
        TextBox_NIT_EMPRESA.ReadOnly = true;
        TextBox_DIG_VER.Text = informacionEmpresa["DIG_VER"].ToString().Trim();
        TextBox_DIG_VER.ReadOnly = true;
        TextBox_RAZ_SOCIAL.Text = informacionEmpresa["RAZ_SOCIAL"].ToString().Trim();
        TextBox_RAZ_SOCIAL.ReadOnly = true;
        TextBox_DIR_EMP.Text = informacionEmpresa["DIR_EMP"].ToString().Trim();
        TextBox_DIR_EMP.ReadOnly = true;

        _cliente.MensajeError = null;
        DataRow filaInfoCiudadEmpresa = obtenerDatosCiudadCliente(informacionEmpresa["CIU_EMP"].ToString().Trim());
        if (filaInfoCiudadEmpresa != null)
        {
            TextBox_CIUDAD_CLIENTE.Text = filaInfoCiudadEmpresa["NOMBRE_CIUDAD"].ToString();
        }
        else
        {
            TextBox_CIUDAD_CLIENTE.Text = "";
        }

        TextBox_TEL_EMP.Text = informacionEmpresa["TEL_EMP"].ToString().Trim();
        TextBox_TEL_EMP.ReadOnly = true;
        TextBox_TEL_EMP_1.Text = informacionEmpresa["TEL_EMP1"].ToString().Trim();
        TextBox_TEL_EMP_1.ReadOnly = true;
        TextBox_CEL_EMP.Text = informacionEmpresa["NUM_CELULAR"].ToString().Trim();
        TextBox_CEL_EMP.ReadOnly = true;

        _cliente.MensajeError = null;
        DataRow filaInfoActividadEmpresa = obtenerDatosActividadCliente(informacionEmpresa["ID_ACTIVIDAD"].ToString().Trim());
        if (filaInfoActividadEmpresa != null)
        {
            TextBox_ACTIVIDAD_ECONOMICA.Text = filaInfoActividadEmpresa["NOMBRE_ACTIVIDAD"].ToString();
        }
        else
        {
            TextBox_ACTIVIDAD_ECONOMICA.Text = "";
        }
        TextBox_DES_ACTIVIDAD.Text = informacionEmpresa["ACT_ECO"].ToString().Trim();

        TextBox_NUM_EMPLEADOS.Text = informacionEmpresa["NUM_EMPLEADOS"].ToString().Trim();

        llenarGridCobertura(Convert.ToDecimal(informacionEmpresa["ID_EMPRESA"]));
        GridView_COVERTURA.Columns[0].Visible = false;

        _cliente.MensajeError = null;
        DataRow filaInfoCiudadYDepartamento = obtenerDatosCiudadOriginoNegocio(informacionEmpresa["CIU_ORG_NEG"].ToString().Trim());
        if (filaInfoCiudadYDepartamento != null)
        {
            TextBox_CIUDAD_ORIGINO.Text = filaInfoCiudadYDepartamento["NOMBRE_CIUDAD"].ToString();
        }
        else
        {
            TextBox_CIUDAD_ORIGINO.Text = "";
        }

        TextBox_NOM_REP_LEGAL.Text = informacionEmpresa["NOM_REP_LEGAL"].ToString().Trim();
        TextBox_NOM_REP_LEGAL.ReadOnly = true;
        TextBox_CC_REP_LEGAL.Text = informacionEmpresa["CC_REP_LEGAL"].ToString().Trim();
        TextBox_CC_REP_LEGAL.ReadOnly = true;

        _cliente.MensajeError = null;
        filaInfoCiudadYDepartamento = obtenerDatosCiudadOriginoNegocio(informacionEmpresa["ID_CIUDAD_CC_REP_LEGAL"].ToString().Trim());
        if (filaInfoCiudadYDepartamento != null)
        {
            TextBox_CIUDAD_REPRESENTANTE.Text = filaInfoCiudadYDepartamento["NOMBRE_CIUDAD"].ToString();
        }
        else
        {
            TextBox_CIUDAD_REPRESENTANTE.Text = "";
        }

        DataRow filaInfoEjecutivo = obtenerDatosRepresentanteSertempo(Convert.ToDecimal(informacionEmpresa["ID_EJECUTIVO"]));
        if (filaInfoEjecutivo != null)
        {
            TextBox_REPRESENTANTE_SERTEMPO.Text = filaInfoEjecutivo["NOM_EJECUTIVO"].ToString();
        }
        else
        {
            TextBox_REPRESENTANTE_SERTEMPO.Text = "";
        }

        DataRow filaInfoEmpleado;
        try
        {
            filaInfoEmpleado = obtenerDatosEmpleado(Convert.ToDecimal(informacionEmpresa["ID_PSICOLOGO"]));
        }
        catch
        {
            filaInfoEmpleado = null;
        }

        if (filaInfoEmpleado != null)
        {
            TextBox_SICOLOGO.Text = filaInfoEmpleado["NOMBRE_EMPLEADO"].ToString();
        }
        else
        {
            TextBox_SICOLOGO.Text = "";
        }

        try
        {
            filaInfoEmpleado = obtenerDatosEmpleado(Convert.ToDecimal(informacionEmpresa["ID_ANALISTA_NOMINA"]));
        }
        catch
        {
            filaInfoEmpleado = null;
        }

        if (filaInfoEmpleado != null)
        {
            TextBox_ANALISTA.Text = filaInfoEmpleado["NOMBRE_EMPLEADO"].ToString();
        }
        else
        {
            TextBox_ANALISTA.Text = "";
        }

        try
        {
            filaInfoEmpleado = obtenerDatosEmpleado(Convert.ToDecimal(informacionEmpresa["ID_GESTOR_COMERCIAL"]));
        }
        catch
        {
            filaInfoEmpleado = null;
        }

        if (filaInfoEmpleado != null)
        {
            TextBox_GESTOR.Text = filaInfoEmpleado["NOMBRE_EMPLEADO"].ToString();
        }
        else
        {
            TextBox_GESTOR.Text = "";
        }

        Panel_FORMULARIO.Enabled = false;
    }


    private void CargarSeleccion(Decimal ID_EMPRESA,tabla.proceso proceso)
    {
        contactos _contacto = new contactos(Session["idEmpresa"].ToString());
        DataTable tablaContactosOriginal = _contacto.ObtenerContactosPorIdEmpresa(ID_EMPRESA, proceso);

        GridView_ContactosSeleccion.DataSource = tablaContactosOriginal;
        GridView_ContactosSeleccion.DataBind();

        pruebaPerfil  _pp = new pruebaPerfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaPruebas = _pp.ObtenerPruebasVSCargoPorEmpresa(ID_EMPRESA);
        CargarTablaConfiguradaParaPruebasCargosSeleccion(tablaPruebas);

        envioCandidato _envioCandidato = new envioCandidato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaCondicionesEnvio = _envioCandidato.ObtenerTodosLosContactosParaEnvioDeCandidatosPorIdEmpresa(ID_EMPRESA);

        if (tablaCondicionesEnvio.Rows.Count > 0)
        {
            GridView_CondEnvioSeleccion.DataSource = tablaCondicionesEnvio;
            GridView_CondEnvioSeleccion.DataBind();
        }
        else
        {
            GridView_CondEnvioSeleccion.DataSource = null;
            GridView_CondEnvioSeleccion.DataBind();
        }
    }

    private void iniciar_interfaz_para_cliente_existente()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        int reg = Convert.ToInt32(QueryStringSeguro["reg"]);
        int proceso = Convert.ToInt32(QueryStringSeguro["proceso"]);

        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaEmpresa = _cliente.ObtenerEmpresaConIdEmpresa(reg);

        Panel_Formulario_Nomina.Visible = false;
        Panel_FormularioContratacion.Visible = false;
        Panel_FormularioSeleccion.Visible = false;
        Panel_FORMULARIO.Visible = false;

        if (_cliente.MensajeError != null)
        {
            configurarInfoAdicionalModulo(false, "");

            configurarMensajes(true, System.Drawing.Color.Red);
            Label_MENSAJE.Text = _cliente.MensajeError;

            Panel_RESULTADOS_GRID.Visible = false;

            Panel_BOTONES_INTERNOS.Visible = false;
        }
        else
        {
            if (tablaEmpresa.Rows.Count <= 0)
            {
                configurarInfoAdicionalModulo(false, "");

                configurarMensajes(true, System.Drawing.Color.Red);
                Label_MENSAJE.Text = "ADVERTENCIA: No se encontró una empresa con el ID: " + reg.ToString();

                Panel_RESULTADOS_GRID.Visible = false;

                Panel_BOTONES_INTERNOS.Visible = false;
            }
            else
            {
                configurarMensajes(false, System.Drawing.Color.Green);

                DataRow informacionEmpresa = tablaEmpresa.Rows[0];

                Page.Header.Title = informacionEmpresa["RAZ_SOCIAL"].ToString();

                configurarInfoAdicionalModulo(true, informacionEmpresa["RAZ_SOCIAL"].ToString());

                mostrar_botones_internos_segun_proceso();

                Panel_BOTONES_INTERNOS.Visible = true;
                Panel_BOTONES_INTERNOS.Enabled = true;

                Panel_RESULTADOS_GRID.Visible = false;

                tabla.proceso pr = (tabla.proceso)proceso;

                if (proceso == ((int)tabla.proceso.ContactoContratacion))
                {
                    Panel_FormularioContratacion.Visible = true;

                    CargarFormularioContratacion(Convert.ToDecimal(reg), pr);
                }
                else
                {
                    if (proceso == ((int)tabla.proceso.ContactoComercial))
                    {
                        Label_ObjetivosArea.Text = String.Empty;

                        Panel_FORMULARIO.Visible = true;

                        CargarFormularioComercial(informacionEmpresa, _cliente);
                    }
                    else
                    {
                        if (proceso == ((int)tabla.proceso.Nomina))
                        {
                            Panel_Formulario_Nomina.Visible = true;


                            Cargar(Convert.ToDecimal(reg));
                        }
                        else 
                        {
                            if (proceso == ((int)tabla.proceso.ContactoSeleccion))
                            {
                                Panel_FormularioSeleccion.Visible = true;

                                CargarSeleccion(Convert.ToDecimal(reg), pr);
                            }
                        }

                    }
                }
            }
        }
    }
    #endregion FUNCIONES SE EJECUTAN AL INICIAR LA PAGINA

    #region CONDICIONES_NOMINA
    private void Cargar(Decimal ID_EMPRESA)
    {
        Panel_Formulario_Nomina.Visible = true;

        contactos _contacto = new contactos(Session["idEmpresa"].ToString());
        DataTable tablaContactosOriginal = _contacto.ObtenerContactosPorIdEmpresa(ID_EMPRESA, tabla.proceso.Nomina);

        GridView_CONTACTOSNOMINA.DataSource = tablaContactosOriginal;
        GridView_CONTACTOSNOMINA.DataBind();

        Cargar(DropDownList_BAS_HOR_EXT, tabla.PARAMETROS_BASE_HORA_EXTRAS);
        Cargar(DropDownList_ID_PERIODO_PAGO, tabla.PARAMETROS_PERIODO_PAGO);
        Cargar(DropDownList_CALCULO_RETENCION_FUENTE, tabla.PARAMETROS_CALCULO_RETENCION_FUENTE);

        Label_MENSAJE_CC.Text = "Por favor seleccionar una ciudad de la lista de cobertura.";
        this.Label_MENSAJE_SUB_CC.Text = "Por favor seleccionar un centro de costo de la lista de centros de costo.";

        condicionNomina _condicionNomina = new condicionNomina(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable _dataTable = _condicionNomina.ObtenerPorIdEmpresa(ID_EMPRESA);
        if (String.IsNullOrEmpty(_condicionNomina.MensajeError))
        {
            if (_dataTable.Rows.Count != 0)
            {
                foreach (DataRow _dataRow in _dataTable.Rows)
                {
                    Label_INFO_ADICIONAL_MODULO.Text = !String.IsNullOrEmpty(_dataRow["RAZ_SOCIAL"].ToString()) ? _dataRow["RAZ_SOCIAL"].ToString() : String.Empty;
                    TextBox_FCH_CRE.Text = !String.IsNullOrEmpty(_dataRow["FCH_CRE"].ToString()) ? _dataRow["FCH_CRE"].ToString() : String.Empty;
                    TextBox_USU_CRE.Text = !String.IsNullOrEmpty(_dataRow["USU_CRE"].ToString()) ? _dataRow["USU_CRE"].ToString() : String.Empty;
                    TextBox_FCH_MOD.Text = !String.IsNullOrEmpty(_dataRow["FCH_MOD"].ToString()) ? _dataRow["FCH_MOD"].ToString() : String.Empty;
                    TextBox_USU_MOD.Text = !String.IsNullOrEmpty(_dataRow["USU_MOD"].ToString()) ? _dataRow["USU_MOD"].ToString() : String.Empty;
                    TextBox_REGISTRO.Text = !String.IsNullOrEmpty(_dataRow["REGISTRO"].ToString()) ? _dataRow["REGISTRO"].ToString() : String.Empty;

                    if (!String.IsNullOrEmpty(_dataRow["ID_PERIODO_PAGO"].ToString()))
                    {
                        DropDownList_ID_PERIODO_PAGO.SelectedValue = _dataRow["ID_PERIODO_PAGO"].ToString();
                        Cargar(CheckBoxList_PERIODO_PAGO_SUB_TRANSPORTE, _dataRow["ID_PERIODO_PAGO"].ToString());
                        for (int i = 0; i < CheckBoxList_PERIODO_PAGO_SUB_TRANSPORTE.Items.Count; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    if (!String.IsNullOrEmpty(_dataRow["PAG_SUB_TRANS_PERIDO_1"].ToString()))
                                        CheckBoxList_PERIODO_PAGO_SUB_TRANSPORTE.Items[i].Selected = _dataRow["PAG_SUB_TRANS_PERIDO_1"].Equals(true) ? true : false;
                                    break;
                                case 1:
                                    if (!String.IsNullOrEmpty(_dataRow["PAG_SUB_TRANS_PERIDO_2"].ToString()))
                                        CheckBoxList_PERIODO_PAGO_SUB_TRANSPORTE.Items[i].Selected = _dataRow["PAG_SUB_TRANS_PERIDO_2"].Equals(true) ? true : false;
                                    break;
                                case 2:
                                    if (!String.IsNullOrEmpty(_dataRow["PAG_SUB_TRANS_PERIDO_3"].ToString()))
                                        CheckBoxList_PERIODO_PAGO_SUB_TRANSPORTE.Items[i].Selected = _dataRow["PAG_SUB_TRANS_PERIDO_3"].Equals(true) ? true : false;
                                    break;
                                case 3:
                                    if (!String.IsNullOrEmpty(_dataRow["PAG_SUB_TRANS_PERIDO_4"].ToString()))
                                        CheckBoxList_PERIODO_PAGO_SUB_TRANSPORTE.Items[i].Selected = _dataRow["PAG_SUB_TRANS_PERIDO_4"].Equals(true) ? true : false;
                                    break;
                            }
                        }
                    }
                    else DropDownList_ID_PERIODO_PAGO.SelectedValue = "0";

                    TextBox_FECHA_PAGOS.Text = !String.IsNullOrEmpty(_dataRow["FECHA_PAGOS"].ToString()) ? _dataRow["FECHA_PAGOS"].ToString() : String.Empty;

                    if (!String.IsNullOrEmpty(_dataRow["CALC_PROM_DOMINICAL"].ToString()))
                        this.CheckBox_CALC_PROM_DOMINICAL.Checked = _dataRow["CALC_PROM_DOMINICAL"].Equals(true) ? true : false;
                    if (!String.IsNullOrEmpty(_dataRow["AJUSTAR_SMLV"].ToString()))
                        this.CheckBox_AJUSTAR_SMLV.Checked = _dataRow["AJUSTAR_SMLV"].Equals(true) ? true : false;

                    if (!String.IsNullOrEmpty(_dataRow["PAGA_SUB_TRANSPORTE"].ToString()))
                        this.CheckBox_PAGAR_SUB_TRANSPORTE.Checked = _dataRow["PAGA_SUB_TRANSPORTE"].Equals(true) ? true : false;

                    if (!String.IsNullOrEmpty(_dataRow["MOSTRAR_UNIFICADA"].ToString()))
                        this.CheckBox_MOSTRAR_UNIFICADA.Checked = _dataRow["MOSTRAR_UNIFICADA"].Equals(true) ? true : false;
                    if (!String.IsNullOrEmpty(_dataRow["DES_SEG_SOC_TRAB"].ToString()))
                        this.CheckBox_DES_SEG_SOC_TRAB.Checked = _dataRow["DES_SEG_SOC_TRAB"].Equals(true) ? true : false;

                    if (!String.IsNullOrEmpty(_dataRow["FACT_PARAF_ULTIMO_PERIODO"].ToString()))
                        this.CheckBox_FACT_PARAFISCALES.Checked = _dataRow["FACT_PARAF_ULTIMO_PERIODO"].Equals(true) ? true : false;
                    if (!String.IsNullOrEmpty(_dataRow["SABADO_NO_HABIL"].ToString()))
                        this.CheckBox_SABADO_NO_HABIL.Checked = _dataRow["SABADO_NO_HABIL"].Equals(true) ? true : false;
                    if (!String.IsNullOrEmpty(_dataRow["LIQUIDAR_ORDINARIAS_ULTIMO_PERIODO"].ToString()))
                        this.CheckBox_ORDINARIAS_ULTIMO_PERIODO.Checked = _dataRow["LIQUIDAR_ORDINARIAS_ULTIMO_PERIODO"].Equals(true) ? true : false;

                    DropDownList_BAS_HOR_EXT.SelectedValue = !String.IsNullOrEmpty(_dataRow["BAS_HOR_EXT"].ToString()) ? _dataRow["BAS_HOR_EXT"].ToString() : "0";
                    TextBox_FCH_INI_PRI_PER_NOM.Text = !String.IsNullOrEmpty(_dataRow["FCH_INI_PRI_PER_NOM"].ToString()) ? _dataRow["FCH_INI_PRI_PER_NOM"].ToString() : String.Empty;
                    DropDownList_CALCULO_RETENCION_FUENTE.SelectedValue = !String.IsNullOrEmpty(_dataRow["CALCULO_RETENCION_FUENTE"].ToString()) ? _dataRow["CALCULO_RETENCION_FUENTE"].ToString() : "0";

                    TextBox_ULT_PERIODO.Text = !String.IsNullOrEmpty(_dataRow["ULT_PERIODO"].ToString()) ? _dataRow["ULT_PERIODO"].ToString() : "0";
                    TextBox_ULT_PERIODO_MEM.Text = !String.IsNullOrEmpty(_dataRow["ULT_PERIODO_MEM"].ToString()) ? _dataRow["ULT_PERIODO_MEM"].ToString() : "0";
                    TextBox_FCH_ULT_LIQ_PER.Text = !String.IsNullOrEmpty(_dataRow["FCH_ULT_LIQ_PER"].ToString()) ? _dataRow["FCH_ULT_LIQ_PER"].ToString() : String.Empty;
                    TextBox_FCH_ULT_LIQ_MEM.Text = !String.IsNullOrEmpty(_dataRow["FCH_ULT_LIQ_MEM"].ToString()) ? _dataRow["FCH_ULT_LIQ_MEM"].ToString() : String.Empty;

                    cobertura _cobertura = new cobertura(Session["idEmpresa"].ToString());

                    DataTable _dataTableCobertura = _cobertura.obtenerCoberturaDeUnCliente(ID_EMPRESA);

                    if (_dataTableCobertura.Rows.Count == 0)
                    {
                        Informar(Label_MENSAJE_COBERTURA, "ADVERTENCIA: La empresa no tiene configurada actualmente una cobertura.", Proceso.Error);
                    }
                    else Panel_MENSAJE_COBERTURA.Visible = false;
                    GridView_COBERTURA.DataSource = _dataTableCobertura;
                    GridView_COBERTURA.DataBind();

                    _dataTableCobertura.Dispose();

                    llenarGridIncapacidades(Convert.ToDecimal(TextBox_REGISTRO.Text));
                    Mostrar();
                    Bloquear();
                }
            }
        }
        else
        {
            Informar(Label_MENSAJE, "Error: Consulte con el Administrador: " + _condicionNomina.MensajeError, Proceso.Error);
            this.Panel_MENSAJES.Visible = true;
        }
    }

    private void Cargar(Decimal ID_EMPRESA, String ID_CIUDAD)
    {
        this.TextBox_REGISTRO.Text = String.Empty;
        condicionNomina _condicionNomina = new condicionNomina(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable _dataTable = _condicionNomina.ObtenerPorIdEmpresaIdCiudad(ID_EMPRESA, ID_CIUDAD);
        if (String.IsNullOrEmpty(_condicionNomina.MensajeError))
        {
            if (_dataTable.Rows.Count != 0)
            {
                foreach (DataRow _dataRow in _dataTable.Rows)
                {
                    TextBox_FCH_CRE.Text = !String.IsNullOrEmpty(_dataRow["FCH_CRE"].ToString()) ? _dataRow["FCH_CRE"].ToString() : String.Empty;
                    TextBox_USU_CRE.Text = !String.IsNullOrEmpty(_dataRow["USU_CRE"].ToString()) ? _dataRow["USU_CRE"].ToString() : String.Empty;
                    TextBox_FCH_MOD.Text = !String.IsNullOrEmpty(_dataRow["FCH_MOD"].ToString()) ? _dataRow["FCH_MOD"].ToString() : String.Empty;
                    TextBox_USU_MOD.Text = !String.IsNullOrEmpty(_dataRow["USU_MOD"].ToString()) ? _dataRow["USU_MOD"].ToString() : String.Empty;
                    TextBox_REGISTRO.Text = !String.IsNullOrEmpty(_dataRow["REGISTRO"].ToString()) ? _dataRow["REGISTRO"].ToString() : String.Empty;

                    if (!String.IsNullOrEmpty(_dataRow["ID_PERIODO_PAGO"].ToString()))
                    {
                        DropDownList_ID_PERIODO_PAGO.SelectedValue = _dataRow["ID_PERIODO_PAGO"].ToString();
                        Cargar(CheckBoxList_PERIODO_PAGO_SUB_TRANSPORTE, _dataRow["ID_PERIODO_PAGO"].ToString());
                        for (int i = 0; i < CheckBoxList_PERIODO_PAGO_SUB_TRANSPORTE.Items.Count; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    if (!String.IsNullOrEmpty(_dataRow["PAG_SUB_TRANS_PERIDO_1"].ToString()))
                                        CheckBoxList_PERIODO_PAGO_SUB_TRANSPORTE.Items[i].Selected = _dataRow["PAG_SUB_TRANS_PERIDO_1"].Equals(true) ? true : false;
                                    break;
                                case 1:
                                    if (!String.IsNullOrEmpty(_dataRow["PAG_SUB_TRANS_PERIDO_2"].ToString()))
                                        CheckBoxList_PERIODO_PAGO_SUB_TRANSPORTE.Items[i].Selected = _dataRow["PAG_SUB_TRANS_PERIDO_2"].Equals(true) ? true : false;
                                    break;
                                case 2:
                                    if (!String.IsNullOrEmpty(_dataRow["PAG_SUB_TRANS_PERIDO_3"].ToString()))
                                        CheckBoxList_PERIODO_PAGO_SUB_TRANSPORTE.Items[i].Selected = _dataRow["PAG_SUB_TRANS_PERIDO_3"].Equals(true) ? true : false;
                                    break;
                                case 3:
                                    if (!String.IsNullOrEmpty(_dataRow["PAG_SUB_TRANS_PERIDO_4"].ToString()))
                                        CheckBoxList_PERIODO_PAGO_SUB_TRANSPORTE.Items[i].Selected = _dataRow["PAG_SUB_TRANS_PERIDO_4"].Equals(true) ? true : false;
                                    break;
                            }
                        }
                    }
                    else DropDownList_ID_PERIODO_PAGO.SelectedValue = "0";

                    TextBox_FECHA_PAGOS.Text = !String.IsNullOrEmpty(_dataRow["FECHA_PAGOS"].ToString()) ? _dataRow["FECHA_PAGOS"].ToString() : String.Empty;

                    if (!String.IsNullOrEmpty(_dataRow["CALC_PROM_DOMINICAL"].ToString()))
                        this.CheckBox_CALC_PROM_DOMINICAL.Checked = _dataRow["CALC_PROM_DOMINICAL"].Equals(true) ? true : false;
                    if (!String.IsNullOrEmpty(_dataRow["AJUSTAR_SMLV"].ToString()))
                        this.CheckBox_AJUSTAR_SMLV.Checked = _dataRow["AJUSTAR_SMLV"].Equals(true) ? true : false;
                    if (!String.IsNullOrEmpty(_dataRow["MOSTRAR_UNIFICADA"].ToString()))
                        this.CheckBox_MOSTRAR_UNIFICADA.Checked = _dataRow["MOSTRAR_UNIFICADA"].Equals(true) ? true : false;
                    if (!String.IsNullOrEmpty(_dataRow["DES_SEG_SOC_TRAB"].ToString()))
                        this.CheckBox_DES_SEG_SOC_TRAB.Checked = _dataRow["DES_SEG_SOC_TRAB"].Equals(true) ? true : false;
                    if (!String.IsNullOrEmpty(_dataRow["FACT_PARAF_ULTIMO_PERIODO"].ToString()))
                        this.CheckBox_FACT_PARAFISCALES.Checked = _dataRow["FACT_PARAF_ULTIMO_PERIODO"].Equals(true) ? true : false;
                    if (!String.IsNullOrEmpty(_dataRow["SABADO_NO_HABIL"].ToString()))
                        this.CheckBox_SABADO_NO_HABIL.Checked = _dataRow["SABADO_NO_HABIL"].Equals(true) ? true : false;
                    if (!String.IsNullOrEmpty(_dataRow["LIQUIDAR_ORDINARIAS_ULTIMO_PERIODO"].ToString()))
                        this.CheckBox_ORDINARIAS_ULTIMO_PERIODO.Checked = _dataRow["LIQUIDAR_ORDINARIAS_ULTIMO_PERIODO"].Equals(true) ? true : false;

                    DropDownList_BAS_HOR_EXT.SelectedValue = !String.IsNullOrEmpty(_dataRow["BAS_HOR_EXT"].ToString()) ? _dataRow["BAS_HOR_EXT"].ToString() : "0";
                    TextBox_FCH_INI_PRI_PER_NOM.Text = !String.IsNullOrEmpty(_dataRow["FCH_INI_PRI_PER_NOM"].ToString()) ? _dataRow["FCH_INI_PRI_PER_NOM"].ToString() : String.Empty;
                    DropDownList_CALCULO_RETENCION_FUENTE.SelectedValue = !String.IsNullOrEmpty(_dataRow["CALCULO_RETENCION_FUENTE"].ToString()) ? _dataRow["CALCULO_RETENCION_FUENTE"].ToString() : "0";

                    TextBox_ULT_PERIODO.Text = !String.IsNullOrEmpty(_dataRow["ULT_PERIODO"].ToString()) ? _dataRow["ULT_PERIODO"].ToString() : "0";
                    TextBox_ULT_PERIODO_MEM.Text = !String.IsNullOrEmpty(_dataRow["ULT_PERIODO_MEM"].ToString()) ? _dataRow["ULT_PERIODO_MEM"].ToString() : "0";
                    TextBox_FCH_ULT_LIQ_PER.Text = !String.IsNullOrEmpty(_dataRow["FCH_ULT_LIQ_PER"].ToString()) ? _dataRow["FCH_ULT_LIQ_PER"].ToString() : String.Empty;
                    TextBox_FCH_ULT_LIQ_MEM.Text = !String.IsNullOrEmpty(_dataRow["FCH_ULT_LIQ_MEM"].ToString()) ? _dataRow["FCH_ULT_LIQ_MEM"].ToString() : String.Empty;

                    if (!String.IsNullOrEmpty(_dataRow["ORDINARIAS_ULTIMO_PERIODO"].ToString()))
                        this.CheckBox_ORDINARIAS_ULTIMO_PERIODO.Checked = _dataRow["ORDINARIAS_ULTIMO_PERIODO"].Equals(true) ? true : false;

                    llenarGridIncapacidades(Convert.ToDecimal(TextBox_REGISTRO.Text));
                    Mostrar();
                    Bloquear();
                }
            }
            else
            {
                Informar(Label_MENSAJE, "Advertencia: La ciudad seleccionada no cuenta con condiciones de nómina." + _condicionNomina.MensajeError, Proceso.Error);
            }
        }
        else
        {
            Informar(Label_MENSAJE, "Error: Consulte con el Administrador: " + _condicionNomina.MensajeError, Proceso.Error);
            this.Panel_MENSAJES.Visible = true;
        }
    }

    private void Cargar(Decimal ID_EMPRESA, Decimal ID_CENTRO_C)
    {
        this.TextBox_REGISTRO.Text = String.Empty;
        condicionNomina _condicionNomina = new condicionNomina(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable _dataTable = _condicionNomina.ObtenerPorIdEmpresaIdCC(ID_EMPRESA, ID_CENTRO_C);
        if (String.IsNullOrEmpty(_condicionNomina.MensajeError))
        {
            if (_dataTable.Rows.Count != 0)
            {
                foreach (DataRow _dataRow in _dataTable.Rows)
                {
                    TextBox_FCH_CRE.Text = !String.IsNullOrEmpty(_dataRow["FCH_CRE"].ToString()) ? _dataRow["FCH_CRE"].ToString() : String.Empty;
                    TextBox_USU_CRE.Text = !String.IsNullOrEmpty(_dataRow["USU_CRE"].ToString()) ? _dataRow["USU_CRE"].ToString() : String.Empty;
                    TextBox_FCH_MOD.Text = !String.IsNullOrEmpty(_dataRow["FCH_MOD"].ToString()) ? _dataRow["FCH_MOD"].ToString() : String.Empty;
                    TextBox_USU_MOD.Text = !String.IsNullOrEmpty(_dataRow["USU_MOD"].ToString()) ? _dataRow["USU_MOD"].ToString() : String.Empty;
                    TextBox_REGISTRO.Text = !String.IsNullOrEmpty(_dataRow["REGISTRO"].ToString()) ? _dataRow["REGISTRO"].ToString() : String.Empty;

                    if (!String.IsNullOrEmpty(_dataRow["ID_PERIODO_PAGO"].ToString()))
                    {
                        DropDownList_ID_PERIODO_PAGO.SelectedValue = _dataRow["ID_PERIODO_PAGO"].ToString();
                        Cargar(CheckBoxList_PERIODO_PAGO_SUB_TRANSPORTE, _dataRow["ID_PERIODO_PAGO"].ToString());
                        for (int i = 0; i < CheckBoxList_PERIODO_PAGO_SUB_TRANSPORTE.Items.Count; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    if (!String.IsNullOrEmpty(_dataRow["PAG_SUB_TRANS_PERIDO_1"].ToString()))
                                        CheckBoxList_PERIODO_PAGO_SUB_TRANSPORTE.Items[i].Selected = _dataRow["PAG_SUB_TRANS_PERIDO_1"].Equals(true) ? true : false;
                                    break;
                                case 1:
                                    if (!String.IsNullOrEmpty(_dataRow["PAG_SUB_TRANS_PERIDO_2"].ToString()))
                                        CheckBoxList_PERIODO_PAGO_SUB_TRANSPORTE.Items[i].Selected = _dataRow["PAG_SUB_TRANS_PERIDO_2"].Equals(true) ? true : false;
                                    break;
                                case 2:
                                    if (!String.IsNullOrEmpty(_dataRow["PAG_SUB_TRANS_PERIDO_3"].ToString()))
                                        CheckBoxList_PERIODO_PAGO_SUB_TRANSPORTE.Items[i].Selected = _dataRow["PAG_SUB_TRANS_PERIDO_3"].Equals(true) ? true : false;
                                    break;
                                case 3:
                                    if (!String.IsNullOrEmpty(_dataRow["PAG_SUB_TRANS_PERIDO_4"].ToString()))
                                        CheckBoxList_PERIODO_PAGO_SUB_TRANSPORTE.Items[i].Selected = _dataRow["PAG_SUB_TRANS_PERIDO_4"].Equals(true) ? true : false;
                                    break;
                            }
                        }
                    }
                    else DropDownList_ID_PERIODO_PAGO.SelectedValue = "0";

                    TextBox_FECHA_PAGOS.Text = !String.IsNullOrEmpty(_dataRow["FECHA_PAGOS"].ToString()) ? _dataRow["FECHA_PAGOS"].ToString() : String.Empty;

                    if (!String.IsNullOrEmpty(_dataRow["CALC_PROM_DOMINICAL"].ToString()))
                        this.CheckBox_CALC_PROM_DOMINICAL.Checked = _dataRow["CALC_PROM_DOMINICAL"].Equals(true) ? true : false;
                    if (!String.IsNullOrEmpty(_dataRow["AJUSTAR_SMLV"].ToString()))
                        this.CheckBox_AJUSTAR_SMLV.Checked = _dataRow["AJUSTAR_SMLV"].Equals(true) ? true : false;
                    if (!String.IsNullOrEmpty(_dataRow["MOSTRAR_UNIFICADA"].ToString()))
                        this.CheckBox_MOSTRAR_UNIFICADA.Checked = _dataRow["MOSTRAR_UNIFICADA"].Equals(true) ? true : false;
                    if (!String.IsNullOrEmpty(_dataRow["DES_SEG_SOC_TRAB"].ToString()))
                        this.CheckBox_DES_SEG_SOC_TRAB.Checked = _dataRow["DES_SEG_SOC_TRAB"].Equals(true) ? true : false;
                    if (!String.IsNullOrEmpty(_dataRow["FACT_PARAF_ULTIMO_PERIODO"].ToString()))
                        this.CheckBox_FACT_PARAFISCALES.Checked = _dataRow["FACT_PARAF_ULTIMO_PERIODO"].Equals(true) ? true : false;
                    if (!String.IsNullOrEmpty(_dataRow["SABADO_NO_HABIL"].ToString()))
                        this.CheckBox_SABADO_NO_HABIL.Checked = _dataRow["SABADO_NO_HABIL"].Equals(true) ? true : false;
                    if (!String.IsNullOrEmpty(_dataRow["LIQUIDAR_ORDINARIAS_ULTIMO_PERIODO"].ToString()))
                        this.CheckBox_ORDINARIAS_ULTIMO_PERIODO.Checked = _dataRow["LIQUIDAR_ORDINARIAS_ULTIMO_PERIODO"].Equals(true) ? true : false;

                    DropDownList_BAS_HOR_EXT.SelectedValue = !String.IsNullOrEmpty(_dataRow["BAS_HOR_EXT"].ToString()) ? _dataRow["BAS_HOR_EXT"].ToString() : "0";
                    TextBox_FCH_INI_PRI_PER_NOM.Text = !String.IsNullOrEmpty(_dataRow["FCH_INI_PRI_PER_NOM"].ToString()) ? _dataRow["FCH_INI_PRI_PER_NOM"].ToString() : String.Empty;
                    DropDownList_CALCULO_RETENCION_FUENTE.SelectedValue = !String.IsNullOrEmpty(_dataRow["CALCULO_RETENCION_FUENTE"].ToString()) ? _dataRow["CALCULO_RETENCION_FUENTE"].ToString() : "0";

                    TextBox_ULT_PERIODO.Text = !String.IsNullOrEmpty(_dataRow["ULT_PERIODO"].ToString()) ? _dataRow["ULT_PERIODO"].ToString() : "0";
                    TextBox_ULT_PERIODO_MEM.Text = !String.IsNullOrEmpty(_dataRow["ULT_PERIODO_MEM"].ToString()) ? _dataRow["ULT_PERIODO_MEM"].ToString() : "0";
                    TextBox_FCH_ULT_LIQ_PER.Text = !String.IsNullOrEmpty(_dataRow["FCH_ULT_LIQ_PER"].ToString()) ? _dataRow["FCH_ULT_LIQ_PER"].ToString() : String.Empty;
                    TextBox_FCH_ULT_LIQ_MEM.Text = !String.IsNullOrEmpty(_dataRow["FCH_ULT_LIQ_MEM"].ToString()) ? _dataRow["FCH_ULT_LIQ_MEM"].ToString() : String.Empty;

                    llenarGridIncapacidades(Convert.ToDecimal(TextBox_REGISTRO.Text));
                    Mostrar();
                    Bloquear();
                }
            }
            else
            {
                Informar(Label_MENSAJE, "Advertencia: El centro de costo seleccionado no cuenta con condiciones de nómina." + _condicionNomina.MensajeError, Proceso.Error);
            }
        }
        else
        {
            Informar(Label_MENSAJE, "Error: Consulte con el Administrador: " + _condicionNomina.MensajeError, Proceso.Error);
            this.Panel_MENSAJES.Visible = true;
        }
    }
    
    private void CargarCondicionesSubCentro(Decimal ID_EMPRESA, Decimal ID_SUB_C)
    {
        this.TextBox_REGISTRO.Text = String.Empty;
        condicionNomina _condicionNomina = new condicionNomina(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable _dataTable = _condicionNomina.ObtenerPorIdEmpresaIdSubCC(ID_EMPRESA, ID_SUB_C);
        if (String.IsNullOrEmpty(_condicionNomina.MensajeError))
        {
            if (_dataTable.Rows.Count != 0)
            {
                this.Panel_MENSAJES.Visible = false;

                foreach (DataRow _dataRow in _dataTable.Rows)
                {
                    TextBox_FCH_CRE.Text = !String.IsNullOrEmpty(_dataRow["FCH_CRE"].ToString()) ? _dataRow["FCH_CRE"].ToString() : String.Empty;
                    TextBox_USU_CRE.Text = !String.IsNullOrEmpty(_dataRow["USU_CRE"].ToString()) ? _dataRow["USU_CRE"].ToString() : String.Empty;
                    TextBox_FCH_MOD.Text = !String.IsNullOrEmpty(_dataRow["FCH_MOD"].ToString()) ? _dataRow["FCH_MOD"].ToString() : String.Empty;
                    TextBox_USU_MOD.Text = !String.IsNullOrEmpty(_dataRow["USU_MOD"].ToString()) ? _dataRow["USU_MOD"].ToString() : String.Empty;
                    TextBox_REGISTRO.Text = !String.IsNullOrEmpty(_dataRow["REGISTRO"].ToString()) ? _dataRow["REGISTRO"].ToString() : String.Empty;

                    if (!String.IsNullOrEmpty(_dataRow["ID_PERIODO_PAGO"].ToString()))
                    {
                        DropDownList_ID_PERIODO_PAGO.SelectedValue = _dataRow["ID_PERIODO_PAGO"].ToString();
                        Cargar(CheckBoxList_PERIODO_PAGO_SUB_TRANSPORTE, _dataRow["ID_PERIODO_PAGO"].ToString());
                        for (int i = 0; i < CheckBoxList_PERIODO_PAGO_SUB_TRANSPORTE.Items.Count; i++)
                        {
                            switch (i)
                            {
                                case 0:
                                    if (!String.IsNullOrEmpty(_dataRow["PAG_SUB_TRANS_PERIDO_1"].ToString()))
                                        CheckBoxList_PERIODO_PAGO_SUB_TRANSPORTE.Items[i].Selected = _dataRow["PAG_SUB_TRANS_PERIDO_1"].Equals(true) ? true : false;
                                    break;
                                case 1:
                                    if (!String.IsNullOrEmpty(_dataRow["PAG_SUB_TRANS_PERIDO_2"].ToString()))
                                        CheckBoxList_PERIODO_PAGO_SUB_TRANSPORTE.Items[i].Selected = _dataRow["PAG_SUB_TRANS_PERIDO_2"].Equals(true) ? true : false;
                                    break;
                                case 2:
                                    if (!String.IsNullOrEmpty(_dataRow["PAG_SUB_TRANS_PERIDO_3"].ToString()))
                                        CheckBoxList_PERIODO_PAGO_SUB_TRANSPORTE.Items[i].Selected = _dataRow["PAG_SUB_TRANS_PERIDO_3"].Equals(true) ? true : false;
                                    break;
                                case 3:
                                    if (!String.IsNullOrEmpty(_dataRow["PAG_SUB_TRANS_PERIDO_4"].ToString()))
                                        CheckBoxList_PERIODO_PAGO_SUB_TRANSPORTE.Items[i].Selected = _dataRow["PAG_SUB_TRANS_PERIDO_4"].Equals(true) ? true : false;
                                    break;
                            }
                        }
                    }
                    else DropDownList_ID_PERIODO_PAGO.SelectedValue = "0";

                    TextBox_FECHA_PAGOS.Text = !String.IsNullOrEmpty(_dataRow["FECHA_PAGOS"].ToString()) ? _dataRow["FECHA_PAGOS"].ToString() : String.Empty;

                    if (!String.IsNullOrEmpty(_dataRow["CALC_PROM_DOMINICAL"].ToString()))
                        this.CheckBox_CALC_PROM_DOMINICAL.Checked = _dataRow["CALC_PROM_DOMINICAL"].Equals(true) ? true : false;
                    if (!String.IsNullOrEmpty(_dataRow["AJUSTAR_SMLV"].ToString()))
                        this.CheckBox_AJUSTAR_SMLV.Checked = _dataRow["AJUSTAR_SMLV"].Equals(true) ? true : false;
                    if (!String.IsNullOrEmpty(_dataRow["MOSTRAR_UNIFICADA"].ToString()))
                        this.CheckBox_MOSTRAR_UNIFICADA.Checked = _dataRow["MOSTRAR_UNIFICADA"].Equals(true) ? true : false;
                    if (!String.IsNullOrEmpty(_dataRow["DES_SEG_SOC_TRAB"].ToString()))
                        this.CheckBox_DES_SEG_SOC_TRAB.Checked = _dataRow["DES_SEG_SOC_TRAB"].Equals(true) ? true : false;
                    if (!String.IsNullOrEmpty(_dataRow["FACT_PARAF_ULTIMO_PERIODO"].ToString()))
                        this.CheckBox_FACT_PARAFISCALES.Checked = _dataRow["FACT_PARAF_ULTIMO_PERIODO"].Equals(true) ? true : false;
                    if (!String.IsNullOrEmpty(_dataRow["SABADO_NO_HABIL"].ToString()))
                        this.CheckBox_SABADO_NO_HABIL.Checked = _dataRow["SABADO_NO_HABIL"].Equals(true) ? true : false;
                    if (!String.IsNullOrEmpty(_dataRow["LIQUIDAR_ORDINARIAS_ULTIMO_PERIODO"].ToString()))
                        this.CheckBox_ORDINARIAS_ULTIMO_PERIODO.Checked = _dataRow["LIQUIDAR_ORDINARIAS_ULTIMO_PERIODO"].Equals(true) ? true : false;

                    DropDownList_BAS_HOR_EXT.SelectedValue = !String.IsNullOrEmpty(_dataRow["BAS_HOR_EXT"].ToString()) ? _dataRow["BAS_HOR_EXT"].ToString() : "0";
                    TextBox_FCH_INI_PRI_PER_NOM.Text = !String.IsNullOrEmpty(_dataRow["FCH_INI_PRI_PER_NOM"].ToString()) ? _dataRow["FCH_INI_PRI_PER_NOM"].ToString() : String.Empty;
                    DropDownList_CALCULO_RETENCION_FUENTE.SelectedValue = !String.IsNullOrEmpty(_dataRow["CALCULO_RETENCION_FUENTE"].ToString()) ? _dataRow["CALCULO_RETENCION_FUENTE"].ToString() : "0";

                    TextBox_ULT_PERIODO.Text = !String.IsNullOrEmpty(_dataRow["ULT_PERIODO"].ToString()) ? _dataRow["ULT_PERIODO"].ToString() : "0";
                    TextBox_ULT_PERIODO_MEM.Text = !String.IsNullOrEmpty(_dataRow["ULT_PERIODO_MEM"].ToString()) ? _dataRow["ULT_PERIODO_MEM"].ToString() : "0";
                    TextBox_FCH_ULT_LIQ_PER.Text = !String.IsNullOrEmpty(_dataRow["FCH_ULT_LIQ_PER"].ToString()) ? _dataRow["FCH_ULT_LIQ_PER"].ToString() : String.Empty;
                    TextBox_FCH_ULT_LIQ_MEM.Text = !String.IsNullOrEmpty(_dataRow["FCH_ULT_LIQ_MEM"].ToString()) ? _dataRow["FCH_ULT_LIQ_MEM"].ToString() : String.Empty;

                    TextBox_PORCENTAJE_FACT.Text = !String.IsNullOrEmpty(_dataRow["PORCENTAJE_FACT"].ToString()) ? String.Format("{0:N2}", Convert.ToDecimal(_dataRow["PORCENTAJE_FACT"].ToString())) : "0";

                    llenarGridIncapacidades(Convert.ToDecimal(TextBox_REGISTRO.Text));
                    Mostrar();
                    Bloquear();
                }
            }
            else
            {
                Informar(Label_MENSAJE, "Advertencia: El sub de costo seleccionado no cuenta con condiciones de nómina." + _condicionNomina.MensajeError, Proceso.Error);
            }
        }
        else
        {
            Informar(Label_MENSAJE, "Error: Consulte con el Administrador: " + _condicionNomina.MensajeError, Proceso.Error);
            this.Panel_MENSAJES.Visible = true;
        }
    }

    private void Cargar(DropDownList dropDownList, DataTable _dataTable, String id)
    {
        dropDownList.Items.Clear();
        ListItem item = new ListItem("Seleccione", "");
        dropDownList.Items.Add(item);

        foreach (DataRow _dataRow in _dataTable.Rows)
        {
            item = new ListItem(_dataRow["NOMBRE"].ToString(), _dataRow[id].ToString());
            dropDownList.Items.Add(item);
        }

        dropDownList.DataBind();
    }

    private void Cargar(DropDownList dropDownList, DataTable _dataTable, String id, String descripcion)
    {
        dropDownList.Items.Clear();
        ListItem item = new ListItem("Seleccione", "");
        dropDownList.Items.Add(item);

        foreach (DataRow _dataRow in _dataTable.Rows)
        {
            item = new ListItem(_dataRow[descripcion].ToString(), _dataRow[id].ToString());
            dropDownList.Items.Add(item);
        }

        dropDownList.DataBind();
    }

    private void Cargar(DropDownList dropDownList, String tabla)
    {
        ListItem item = new ListItem("Seleccione", "");
        parametro _parametro = new parametro(Session["idEmpresa"].ToString());
        DataTable _dataTable = _parametro.ObtenerParametrosPorTabla(tabla);
        dropDownList.Items.Add(item);
        foreach (DataRow _dataRow in _dataTable.Rows)
        {
            item = new ListItem(_dataRow["DESCRIPCION"].ToString(), _dataRow["CODIGO"].ToString());
            dropDownList.Items.Add(item);
        }
        dropDownList.DataBind();
        _dataTable.Dispose();
    }

    protected void Cargar(CheckBoxList checkBoxList, String idPeriodoPago)
    {
        condicionNomina _condicionNomina = new condicionNomina(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        checkBoxList.DataSource = _condicionNomina.obtenerPeriodosDescuentoSubTranportePorPeriodoPago(idPeriodoPago);
        checkBoxList.DataTextField = "descripcion";
        checkBoxList.DataValueField = "codigo";
        checkBoxList.DataBind();

    }

    private void llenarGridIncapacidades(Decimal registro)
    {
        incapadadConceptosNomina _incapadadConceptosNomina = new incapadadConceptosNomina(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable _dataTable = _incapadadConceptosNomina.ObtenerPorRegistro(registro);

        DataTable _dataTableIncapacidad = new DataTable();
        _dataTableIncapacidad.Columns.Add("Código");
        _dataTableIncapacidad.Columns.Add("Concepto");
        _dataTableIncapacidad.Columns.Add("Porcentaje");
        _dataTableIncapacidad.Columns.Add("Cod_Concepto");

        DataRow _dataRowIncapacidad;
        foreach (DataRow _dataRow in _dataTable.Rows)
        {
            _dataRowIncapacidad = _dataTableIncapacidad.NewRow();

            _dataRowIncapacidad["Código"] = _dataRow["Codigo"].ToString();
            _dataRowIncapacidad["Concepto"] = _dataRow["Concepto"].ToString();
            _dataRowIncapacidad["Porcentaje"] = _dataRow["Porcentaje"].ToString();
            _dataRowIncapacidad["Cod_Concepto"] = _dataRow["Cod_Concepto"].ToString();

            _dataTableIncapacidad.Rows.Add(_dataRowIncapacidad);
        }
        Session["dataTableIncapacidades"] = _dataTableIncapacidad;
        GridView_ID_CONCEPTO_INCAPACIDAD.DataSource = _dataTableIncapacidad;
        GridView_ID_CONCEPTO_INCAPACIDAD.DataBind();
    }

    private void Informar(Label label, String mensaje, Proceso proceso)
    {
        label.Text = mensaje;
        switch (proceso)
        {
            case Proceso.Correcto:
                label.ForeColor = System.Drawing.Color.Green;
                break;
            case Proceso.Error:
                label.ForeColor = System.Drawing.Color.Red;
                break;
        }
    }

    private void Mostrar()
    {
        Panel_INFO_ADICIONAL_MODULO.Visible = true;
        Panel_FORM_BOTONES.Visible = true;
    }

    private void Bloquear()
    {
        Panel_incapacidades.Enabled = false;
        Panel_DATOS.Enabled = false;
    }
    #endregion CONDICIONES_NOMINA

    #region METODOS PARA OBTENERDATOS DE LA BD
    private DataRow obtenerDatosCiudadCliente(String idCiudad)
    {
        DataRow resultado = null;

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());

        DataTable tablaEmpresa = _ciudad.ObtenerCiudadPorIdCiudad(idCiudad);

        if (tablaEmpresa.Rows.Count > 0)
        {
            resultado = tablaEmpresa.Rows[0];
        }

        return resultado;
    }

    private DataRow obtenerDatosActividadCliente(String idActividad)
    {
        DataRow resultado = null;

        actividad _actividad = new actividad(Session["idEmpresa"].ToString());

        DataTable tablaActividad = _actividad.ObtenerActividPorIdActividad(idActividad);

        if (tablaActividad.Rows.Count > 0)
        {
            resultado = tablaActividad.Rows[0];
        }

        return resultado;
    }

    private DataRow obtenerDatosCiudadOriginoNegocio(String idCiudad)
    {
        DataRow resultado = null;

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());

        DataTable tablaIdDepartamento = _ciudad.ObtenerIdDepartamentoConIdCiudad(idCiudad);

        if (tablaIdDepartamento.Rows.Count > 0)
        {
            resultado = tablaIdDepartamento.Rows[0];
        }

        return resultado;
    }

    private DataRow obtenerDatosRepresentanteSertempo(Decimal ID_EJECUTIVO)
    {
        DataRow resultado = null;

        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaDatosEjecutivo = _cliente.obtenerDatosEjecutivoPorIdEjecutivo(ID_EJECUTIVO);

        if (tablaDatosEjecutivo.Rows.Count > 0)
        {
            resultado = tablaDatosEjecutivo.Rows[0];
        }

        return resultado;
    }

    private DataRow obtenerDatosEmpleado(Decimal ID_EMPLEADO)
    {
        DataRow resultado = null;

        usuario _usuario = new usuario(Session["idEmpresa"].ToString());

        DataTable tablaDatosEjecutivo = _usuario.ObtenerEmpleadoPorIdEmpleado(ID_EMPLEADO);

        if (tablaDatosEjecutivo.Rows.Count > 0)
        {
            resultado = tablaDatosEjecutivo.Rows[0];
        }

        return resultado;
    }
    #endregion METODOS PARA OBTENERDATOS DE LA BD

    private void CargarInformacionObjetivosPorArea()
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        int proceso = Convert.ToInt32(QueryStringSeguro["proceso"]);

        Panel_ObjetivosArea.Visible = false;
        Label_ObjetivosArea.Text = String.Empty;

        if (proceso == ((int)tabla.proceso.ContactoContratacion))
        {
            Panel_ObjetivosArea.Visible = true;
            Label_ObjetivosArea.Text = "Especifica los parámetros de contratación por empresa usuaria con alcance a la ciudad donde se origina la contratación de personal.";
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Header.Title = "CONDICIONES USUARIAS";

        if (IsPostBack == false)
        {
            tools _tools = new tools();
            SecureQueryString QueryStringSeguro;
            QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

            CargarInformacionObjetivosPorArea();

            configurarCaracteresAceptadosBusqueda(false, false);

            String accion = QueryStringSeguro["accion"].ToString();

            iniciar_seccion_de_busqueda();

            if (accion == "inicial")
            {
                iniciar_interfaz_inicial();
            }
            else
            {
                if (accion == "cargar")
                {
                    iniciar_interfaz_para_cliente_existente();
                }
            }
        }
    }

    protected void GridView_RESULTADOS_BUSQUEDA_SelectedIndexChanged(object sender, EventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        String ID_EMPRESA = GridView_RESULTADOS_BUSQUEDA.SelectedDataKey["ID"].ToString();
        int proceso = Convert.ToInt32(QueryStringSeguro["proceso"]);

        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro());

        if (proceso == ((int)tabla.proceso.ContactoSeleccion))
        {
            QueryStringSeguro["img_area"] = "seleccion";
            QueryStringSeguro["nombre_area"] = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
            QueryStringSeguro["nombre_modulo"] = "CONDICIONES USUARIAS DE SELECCIÓN";
        }
        else
        {
            if (proceso == ((int)tabla.proceso.ContactoContratacion))
            {
                QueryStringSeguro["img_area"] = "contratacion";
                QueryStringSeguro["nombre_area"] = "CONTRATACIÓN Y RELACIONES LABORALES";
                QueryStringSeguro["nombre_modulo"] = "CONDICIONES USUARIAS DE CONTRATACIÓN";
            }
            else
            {
                if (proceso == ((int)tabla.proceso.ContactoNominaFacturacion))
                {
                    QueryStringSeguro["img_area"] = "nomina";
                    QueryStringSeguro["nombre_area"] = "NÓMINA Y FACTURACIÓN";
                    QueryStringSeguro["nombre_modulo"] = "CONDICIONES USUARIAS DE NÓMINA";
                }
                else
                {
                    if (proceso == ((int)tabla.proceso.Nomina))
                    {
                        QueryStringSeguro["img_area"] = "nomina";
                        QueryStringSeguro["nombre_area"] = "NÓMINA Y FACTURACIÓN";
                        QueryStringSeguro["nombre_modulo"] = "NOMINA";
                    }
                    else
                    {
                        if (proceso == ((int)tabla.proceso.ContactoRse))
                        {
                            QueryStringSeguro["img_area"] = "rse";
                            QueryStringSeguro["nombre_area"] = "RESPONSABILIDAD SOCIAL EMPRESARIAL";
                            QueryStringSeguro["nombre_modulo"] = "CONDICIONES USUARIAS DE -RSE-";
                        }
                        else
                        {
                            if (proceso == ((int)tabla.proceso.ContactoBienestarSocial))
                            {
                                QueryStringSeguro["img_area"] = "bienestarsocial";
                                QueryStringSeguro["nombre_area"] = "BIENESTAR SOCIAL";
                                QueryStringSeguro["nombre_modulo"] = "CONDICIONES USUARIAS DE BIENESTAR SOCIAL";
                            }
                            else
                            {
                                if (proceso == ((int)tabla.proceso.ContactoGlobalSalud))
                                {
                                    QueryStringSeguro["img_area"] = "globalsalud";
                                    QueryStringSeguro["nombre_area"] = "SALUD INTEGRAL";
                                    QueryStringSeguro["nombre_modulo"] = "CONDICIONES USUARIAS DE BIENESTAR SOCIAL";
                                }
                                else
                                {
                                    if (proceso == ((int)tabla.proceso.ContactoLiquidacionPrestaciones))
                                    {
                                        QueryStringSeguro["img_area"] = "nomina";
                                        QueryStringSeguro["nombre_area"] = "NÓMINA Y FACTURACIÓN";
                                        QueryStringSeguro["nombre_modulo"] = "LIQUIDACIÓN PRIMAS CESANTÍAS Y VACACIONES";
                                    } 
                                }
                            }
                        }
                    }
                }
            }
        }

        QueryStringSeguro["accion"] = "cargar";
        QueryStringSeguro["reg"] = ID_EMPRESA;
        QueryStringSeguro["proceso"] = proceso.ToString();

        Response.Redirect("~/maestros/condicionesUsuarias.aspx?data=" + HttpUtility.UrlEncode(QueryStringSeguro.ToString()));
    }

    protected void DropDownList_BUSCAR_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_BUSCAR.SelectedValue == "")
        {
            configurarCaracteresAceptadosBusqueda(false, false);
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "COD_EMPRESA")
            {
                configurarCaracteresAceptadosBusqueda(false, true);
            }
            else
            {
                if (DropDownList_BUSCAR.SelectedValue == "RAZ_SOCIAL")
                {
                    configurarCaracteresAceptadosBusqueda(true, false);
                }
            }
        }
        TextBox_BUSCAR.Text = "";
    }

    protected void Button_BUSCAR_Click(object sender, EventArgs e)
    {
        configurarInfoAdicionalModulo(false, "");

        String datosCapturados = TextBox_BUSCAR.Text.ToUpper();
        String campo = DropDownList_BUSCAR.SelectedValue.ToString();

        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaResultadosBusqueda = new DataTable();

        if (DropDownList_BUSCAR.SelectedValue == "RAZ_SOCIAL")
        {
            tablaResultadosBusqueda = _cliente.ObtenerEmpresaConRazSocial(datosCapturados);
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "COD_EMPRESA")
            {
                tablaResultadosBusqueda = _cliente.ObtenerEmpresaConCodEmpresa(datosCapturados);
            }
        }

        if (tablaResultadosBusqueda.Rows.Count <= 0)
        {
            configurarMensajes(true, System.Drawing.Color.Red);
            if (_cliente.MensajeError != null)
            {
                Label_MENSAJE.Text = _cliente.MensajeError;
            }
            else
            {
                Label_MENSAJE.Text = "ADVERTENCIA: No se encontraron registros que cumplieran los datos de busqueda.";
            }

            Panel_BOTONES_INTERNOS.Visible = false;

            Panel_RESULTADOS_GRID.Visible = false;

            Panel_FormularioContratacion.Visible = false;
            Panel_FORMULARIO.Visible = false;
        }
        else
        {
            configurarMensajes(false, System.Drawing.Color.Green);

            Panel_RESULTADOS_GRID.Visible = true;
            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaResultadosBusqueda;
            GridView_RESULTADOS_BUSQUEDA.DataBind();

            Panel_BOTONES_INTERNOS.Visible = false;

            Panel_FormularioContratacion.Visible = false;
            Panel_FORMULARIO.Visible = false;
        }
    }

    protected void GridView_RESULTADOS_BUSQUEDA_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_RESULTADOS_BUSQUEDA.PageIndex = e.NewPageIndex;

        String datosCapturados = TextBox_BUSCAR.Text.ToUpper();
        String campo = DropDownList_BUSCAR.SelectedValue.ToString();

        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaResultadosBusqueda = new DataTable();

        if (DropDownList_BUSCAR.SelectedValue == "RAZ_SOCIAL")
        {
            tablaResultadosBusqueda = _cliente.ObtenerEmpresaConRazSocial(datosCapturados);
        }
        else
        {
            if (DropDownList_BUSCAR.SelectedValue == "COD_EMPRESA")
            {
                tablaResultadosBusqueda = _cliente.ObtenerEmpresaConCodEmpresa(datosCapturados);
            }
        }

        if (tablaResultadosBusqueda.Rows.Count <= 0)
        {
            configurarMensajes(true, System.Drawing.Color.Red);
            if (_cliente.MensajeError != null)
            {
                Label_MENSAJE.Text = _cliente.MensajeError;
            }
            else
            {
                Label_MENSAJE.Text = "ADVERTENCIA: No se encontraron registros que cumplieran los datos de busqueda.";
            }
        }
        else
        {
            Panel_RESULTADOS_GRID.Enabled = true;
            Panel_RESULTADOS_GRID.Visible = true;
            GridView_RESULTADOS_BUSQUEDA.DataSource = tablaResultadosBusqueda;
            GridView_RESULTADOS_BUSQUEDA.DataBind();
        }
    }

    protected void GridView_BancosContratacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_BancosContratacion.PageIndex = e.NewPageIndex;

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
        int proceso = Convert.ToInt32(QueryStringSeguro["proceso"]);

        condicionesContratacion _condContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaBancosPorCiudad = _condContratacion.ObtenerBancosVSCiudadesPorIdEmpresa(ID_EMPRESA);
        CargarTablaConfiguradaParaBancosContratacion(tablaBancosPorCiudad);
    }

    protected void GridView_ExamenesMedicosContratacion_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_ExamenesMedicosContratacion.PageIndex = e.NewPageIndex;

        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"]);

        Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
        int proceso = Convert.ToInt32(QueryStringSeguro["proceso"]);

        condicionesContratacion _condContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaExamenesMedicos = _condContratacion.ObtenerExamenesVSCargosPorIdEmpresa(ID_EMPRESA);
        CargarTablaConfiguradaParaExamenesMedicosContratacion(tablaExamenesMedicos);
    }

    protected void GridView_SUB_CENTROS_DE_COSTO_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"].ToString());
        this.Label_CC.Text = String.Empty;

        if (e.CommandName == "subcentrocosto")
        {
            int fila = Convert.ToInt32(e.CommandArgument);

            Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
            Decimal ID_SUB_C = Convert.ToDecimal(this.GridView_SUB_CENTROS_DE_COSTO.DataKeys[fila].Value);
            this.HiddenField_subCC.Value = ID_SUB_C.ToString();
            CargarCondicionesSubCentro(ID_EMPRESA, ID_SUB_C);
            Label_INFO_ADICIONAL_MODULO.Text = "Sub centro de costo - " + GridView_SUB_CENTROS_DE_COSTO.DataKeys[fila].Values["NOM_SUB_C"].ToString();
        }
    }

    protected void GridView_SUB_CENTROS_DE_COSTO_SelectedIndexChanged(object sender, EventArgs e)
    {
        HiddenField_subCC.Value = GridView_SUB_CENTROS_DE_COSTO.SelectedDataKey["ID_SUB_C"].ToString();
    }

    protected void GridView_CENTROS_DE_COSTO_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"].ToString());
        this.Label_CC.Text = String.Empty;

        if (e.CommandName == "centrocosto")
        {
            int fila = Convert.ToInt32(e.CommandArgument);

            Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
            Decimal ID_CENTRO_C = Convert.ToDecimal(GridView_CENTROS_DE_COSTO.DataKeys[fila].Value);
            subCentroCosto _subCentroCosto = new subCentroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            DataTable tablaSubCC = _subCentroCosto.ObtenerSubCentrosDeCostoPorIdEmpresaIdCentroCosto(ID_EMPRESA, ID_CENTRO_C);
            this.HiddenField_cc.Value = ID_CENTRO_C.ToString();

            Cargar(ID_EMPRESA, ID_CENTRO_C);

            if (tablaSubCC.Rows.Count == 0) Informar(Label_MENSAJE_CC, "ADVERTENCIA: El Centro de costos seleccionado no tiene sub centros.", Proceso.Error);

            GridView_SUB_CENTROS_DE_COSTO.DataSource = tablaSubCC;
            GridView_SUB_CENTROS_DE_COSTO.DataBind();
            if (GridView_SUB_CENTROS_DE_COSTO.Rows.Count > 0) this.Panel_MENSAJE_SUB_CC.Visible = false;
            else this.Panel_MENSAJE_SUB_CC.Visible = true;
            tablaSubCC.Dispose();
            Label_INFO_ADICIONAL_MODULO.Text = "Centro de costo - " + GridView_CENTROS_DE_COSTO.DataKeys[fila].Values["NOM_CC"].ToString();
        }
    }

    protected void GridView_CENTROS_DE_COSTO_SelectedIndexChanged(object sender, EventArgs e)
    {
        HiddenField_subCC.Value = GridView_SUB_CENTROS_DE_COSTO.SelectedDataKey["ID_SUB_C"].ToString();
    }

    protected void GridView_COBERTURA_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        tools _tools = new tools();
        SecureQueryString QueryStringSeguro;
        QueryStringSeguro = new SecureQueryString(_tools.byteParaQueryStringSeguro(), Request["data"].ToString());
        Label_MENSAJE_COBERTURA.Text = String.Empty;

        if (e.CommandName == "Ciudad")
        {
            int fila = Convert.ToInt32(e.CommandArgument);

            Decimal ID_EMPRESA = Convert.ToDecimal(QueryStringSeguro["reg"]);
            String ID_CIUDAD = GridView_COBERTURA.DataKeys[fila].Values["Código Ciudad"].ToString();

            centroCosto _centroCosto = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

            DataTable _dataTableCC = _centroCosto.ObtenerCentrosDeCostoPorIdEmpresaIdCiudad(ID_EMPRESA, ID_CIUDAD);
            this.HiddenField_cobertura.Value = ID_CIUDAD;
            Cargar(ID_EMPRESA, ID_CIUDAD);

            if (_dataTableCC.Rows.Count == 0) Informar(Label_MENSAJE_COBERTURA, "ADVERTENCIA: La Ciudad no tiene centros de costo actualmente.", Proceso.Error);

            GridView_CENTROS_DE_COSTO.DataSource = _dataTableCC;
            GridView_CENTROS_DE_COSTO.DataBind();
            if (GridView_CENTROS_DE_COSTO.Rows.Count > 0) this.Panel_MENSAJE_CC.Visible = false;
            else this.Panel_MENSAJE_CC.Visible = true;
            _dataTableCC.Dispose();
            this.Label_INFO_ADICIONAL_MODULO.Text = "Ciudad - " + GridView_COBERTURA.DataKeys[fila].Values["Ciudad"].ToString();
        }
    }

    protected void GridView_COBERTURA_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow row = GridView_COBERTURA.SelectedRow;
        HiddenField_cobertura.Value = GridView_COBERTURA.SelectedDataKey["Código Ciudad"].ToString();
        HiddenField_cc.Value = String.Empty;
        HiddenField_subCC.Value = String.Empty;
    }

    protected void GridView_contratacion_cobertura_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        this.GridView_contratacion_subCC.DataSource = null;
        this.GridView_contratacion_subCC.DataBind();

        if (e.CommandName == "Ciudad")
        {
            int i = Convert.ToInt32(e.CommandArgument);
            HiddenField_contratacion_idCiudad.Value = GridView_contratacion_cobertura.DataKeys[i].Values["Código Ciudad"].ToString();
            Cargar(GridView_contratacion_cc, Convert.ToDecimal(HiddenField_contratacion_idEmpresa.Value), HiddenField_contratacion_idCiudad.Value);
        }

    }

    protected void GridView_contratacion_cc_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "CentroCosto")
        {
            int i = Convert.ToInt32(e.CommandArgument);
            HiddenField_contratacion_idCentroCosto.Value = GridView_contratacion_cc.DataKeys[i].Values["ID_CENTRO_C"].ToString();
            Cargar(this.GridView_contratacion_subCC, Convert.ToDecimal(HiddenField_contratacion_idEmpresa.Value), Convert.ToDecimal(HiddenField_contratacion_idCentroCosto.Value));
        }
    }

    protected void GridView_contratacion_cobertura_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_contratacion_cobertura.PageIndex = e.NewPageIndex;
        Cargar(GridView_contratacion_cobertura, Convert.ToDecimal(this.HiddenField_contratacion_idEmpresa.Value));
    }

    protected void GridView_contratacion_cc_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_contratacion_cc.PageIndex = e.NewPageIndex;
        Cargar(GridView_contratacion_cc, Convert.ToDecimal(HiddenField_contratacion_idEmpresa.Value), HiddenField_contratacion_idCiudad.Value);
    }

    protected void GridView_contratacion_subCC_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView_contratacion_subCC.PageIndex = e.NewPageIndex;
        Cargar(this.GridView_contratacion_subCC, Convert.ToDecimal(HiddenField_contratacion_idEmpresa.Value), Convert.ToDecimal(HiddenField_contratacion_idCentroCosto.Value));
    }
    protected void GridView_ID_CONCEPTO_INCAPACIDAD_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void GridView_ID_CONCEPTO_INCAPACIDAD_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView_ContactosContratacion_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
