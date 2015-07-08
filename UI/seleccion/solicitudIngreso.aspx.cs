using System;
using System.Web.UI.WebControls;
using System.Data;
using Brainsbits.LLB.seleccion;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB;
using System.Collections.Generic;
using Brainsbits.LLB.seguridad;
using TSHAK.Components;
using System.Web;

public partial class _SolicitudIngreso : System.Web.UI.Page
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
        Nuevo,
        Cargar,
        Modificar
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

    private enum AccionesForm
    { 
        Ninguna = 0,
        Nuevo, 
        Modificar, 
        Cargar
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

    private void Ocultar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_INFO_ADICIONAL_MODULO.Visible = false;
                Panel_BUSQUEDA_CEDULA.Visible = false;

                Panel_FORM_BOTONES.Visible = false;
                Button_MODIFICAR.Visible = false;
                Button_CANCELAR.Visible = false;

                Panel_FORMULARIO.Visible = false;
                Panel_EstadoCandidato.Visible = false;
                Panel_Pestanas.Visible = false;

                GridView_EducacionFormal.Columns[0].Visible = false;
                GridView_EducacionFormal.Columns[1].Visible = false;
                Panel_BotonesEducacionFormal.Visible = false;
                Button_NuevoEF.Visible = false;
                Button_GuardarEF.Visible = false;
                Button_CancelarEF.Visible = false;

                GridView_EducacionNoFormal.Columns[0].Visible = false;
                GridView_EducacionNoFormal.Columns[1].Visible = false;
                Panel_BotonesEducacionNoFormal.Visible = false;
                Button_NuevoENF.Visible = false;
                Button_GuardarENF.Visible = false;
                Button_CancelarENF.Visible = false;

                ImageButton_BUSCADOR_CARGO.Visible = false;
                GridView_ExperienciaLaboral.Columns[0].Visible = false;
                GridView_ExperienciaLaboral.Columns[1].Visible = false;
                Panel_BotonesExperienciaLaboral.Visible = false;
                Button_NuevoEmpleo.Visible = false;
                Button_GuardarEmpleo.Visible = false;
                Button_CancelarEmpleo.Visible = false;

                GridView_ComposicionFamiliar.Columns[0].Visible = false;
                GridView_ComposicionFamiliar.Columns[1].Visible = false;
                Panel_Botones_ComposicionFamiliar.Visible = false;
                Button_NUEVA_COMPOSICIONFAMILIAR.Visible = false;
                Button_GUARDAR_COMPOSICIONFAMILIAR.Visible = false;
                Button_CANCELAR_COMPOSICIONFAMILIAR.Visible = false;

                Panel_Botones_Pestanas.Visible = false;
                Button_Anterior.Visible = false;
                Button_Siguiente.Visible = false;
                Button_Guardar.Visible = false;

                Panel_FORM_BOTONES_1.Visible = false;
                Button_MODIFICAR_1.Visible = false;
                Button_CANCELAR_1.Visible = false;

                Button_DevolverEnCliente.Visible = false;

                break;
            case Acciones.Modificar:
                Button_MODIFICAR.Visible = false;
                Panel_CONTROL_REGISTRO.Visible = false;

                Button_GuardarEF.Visible = false;
                Button_CancelarEF.Visible = false;

                Button_GuardarENF.Visible = false;
                Button_CancelarENF.Visible = false;

                Button_GuardarEmpleo.Visible = false;
                Button_CancelarEmpleo.Visible = false;

                Button_GUARDAR_COMPOSICIONFAMILIAR.Visible = false;
                Button_CANCELAR_COMPOSICIONFAMILIAR.Visible = false;

                Button_Anterior.Visible = false;

                Button_MODIFICAR_1.Visible = false;

                Button_DevolverEnCliente.Visible = false;
                break;
        }
    }

    private void Desactivar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                TextBox_FCH_CRE.Enabled = false;
                TextBox_FCH_MOD.Enabled = false;
                TextBox_HOR_CRE.Enabled = false;
                TextBox_HOR_MOD.Enabled = false;
                TextBox_USU_CRE.Enabled = false;
                TextBox_USU_MOD.Enabled = false;

                TextBox_FCH_NACIMIENTO.Enabled = false;
                DropDownList_PaisNacimiento.Enabled = false;
                DropDownList_SEXO.Enabled = false;

                DropDownList_RH.Enabled = false;

                DropDownList_TIP_DOC_IDENTIDAD.Enabled = false;
                TextBox_NUM_DOC_IDENTIDAD.Enabled = false;
                DropDownList_DEPARTAMENTO_CEDULA.Enabled = false;
                DropDownList_CIU_CEDULA.Enabled = false;
                TextBox_NOMBRES.Enabled = false;
                TextBox_APELLIDOS.Enabled = false;
                TextBox_LIB_MILITAR.Enabled = false;
                DropDownList_CAT_LIC_COND.Enabled = false;

                DropDownList_DEPARTAMENTO_ASPIRANTE.Enabled = false;
                DropDownList_CIU_ASPIRANTE.Enabled = false;
                TextBox_DIR_ASPIRANTE.Enabled = false;
                TextBox_SECTOR.Enabled = false;
                DropDownList_TipoVivienda.Enabled = false;
                DropDownList_ESTRATO.Enabled = false;

                DropDownList_NIV_EDUCACION.Enabled = false;
                DropDownList_nucleo_formacion.Enabled = false;

                TextBox_BUSCADOR_CARGO.Enabled = false;
                DropDownList_ID_OCUPACION.Enabled = false;
                DropDownList_EXPERIENCIA.Enabled = false;
                TextBox_ASPIRACION_SALARIAL.Enabled = false;
                DropDownList_AREAS_ESPECIALIZACION.Enabled = false;

                DropDownList_ESTADO_CIVIL.Enabled = false;
                TextBox_NUM_HIJOS.Enabled = false;
                DropDownList_CabezaFamilia.Enabled = false;

                TextBox_TEL_ASPIRANTE.Enabled = false;
                TextBox_CEL_ASPIRANTE.Enabled = false;
                TextBox_E_MAIL.Enabled = false;

                DropDownList_Talla_Camisa.Enabled = false;
                DropDownList_Talla_Pantalon.Enabled = false;
                DropDownList_talla_zapatos.Enabled = false;
                DropDownList_ID_FUENTE.Enabled = false;
                DropDownList_ComoSeEntero.Enabled = false;
                break;
        }
    }

    private void Mostrar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                Panel_BUSQUEDA_CEDULA.Visible = true;
                break;
            case Acciones.Nuevo:
                Panel_FORM_BOTONES.Visible = true;

                Button_CANCELAR.Visible = true;

                Panel_FORMULARIO.Visible = true;
                Panel_EstadoCandidato.Visible = true;

                Panel_Pestanas.Visible = true;
                
                GridView_EducacionFormal.Columns[0].Visible = true;
                GridView_EducacionFormal.Columns[1].Visible = true;
                Panel_BotonesEducacionFormal.Visible = true;
                Button_NuevoEF.Visible = true;

                GridView_EducacionNoFormal.Columns[0].Visible = true;
                GridView_EducacionNoFormal.Columns[1].Visible = true;
                Panel_BotonesEducacionNoFormal.Visible = true;
                Button_NuevoENF.Visible = true;

                ImageButton_BUSCADOR_CARGO.Visible = true;
                GridView_ExperienciaLaboral.Columns[0].Visible = true;
                GridView_ExperienciaLaboral.Columns[1].Visible = true;
                Panel_BotonesExperienciaLaboral.Visible = true;
                Button_NuevoEmpleo.Visible = true;

                GridView_ComposicionFamiliar.Columns[0].Visible = true;
                GridView_ComposicionFamiliar.Columns[1].Visible = true;
                Panel_Botones_ComposicionFamiliar.Visible = true;
                Button_NUEVA_COMPOSICIONFAMILIAR.Visible = true;

                Panel_Botones_Pestanas.Visible = true;
                Button_Siguiente.Visible = true;
                Button_Guardar.Visible = true;
                break;
            case Acciones.Cargar:
                Panel_FORM_BOTONES.Visible = true;
                Button_MODIFICAR.Visible = true;

                Panel_FORMULARIO.Visible = true;
                Panel_CONTROL_REGISTRO.Visible = true;
                Panel_EstadoCandidato.Visible = true;
                Panel_Pestanas.Visible = true;

                Panel_FORM_BOTONES_1.Visible = true;
                Button_MODIFICAR_1.Visible = true;
                break;
            case Acciones.Modificar:
                Button_CANCELAR.Visible = true;

                HiddenField_ACCION_GRILLA_INFOACADEMMICA_EF.Value = AccionesGrilla.Ninguna.ToString();
                GridView_EducacionFormal.Columns[0].Visible = true;
                GridView_EducacionFormal.Columns[1].Visible = true;
                Panel_BotonesEducacionFormal.Visible = true;
                Button_NuevoEF.Visible = true;

                HiddenField_ACCION_GRILLA_INFOACADEMMIC_ENF.Value = AccionesGrilla.Ninguna.ToString();
                GridView_EducacionNoFormal.Columns[0].Visible = true;
                GridView_EducacionNoFormal.Columns[1].Visible = true;
                Panel_BotonesEducacionNoFormal.Visible = true;
                Button_NuevoENF.Visible = true;

                ImageButton_BUSCADOR_CARGO.Visible = true;
                TextBox_BUSCADOR_CARGO.Text = "";
                HiddenField_ACCION_GRILLA_EXPLABORAL.Value = AccionesGrilla.Ninguna.ToString();
                GridView_ExperienciaLaboral.Columns[0].Visible = true;
                GridView_ExperienciaLaboral.Columns[1].Visible = true;
                Panel_BotonesExperienciaLaboral.Visible = true;
                Button_NuevoEmpleo.Visible = true;

                HiddenField_ACCION_GRILLA_COMPOSICIONFAMILIAR.Value = AccionesGrilla.Ninguna.ToString();
                GridView_ComposicionFamiliar.Columns[0].Visible = true;
                GridView_ComposicionFamiliar.Columns[1].Visible = true;
                Panel_Botones_ComposicionFamiliar.Visible = true;
                Button_NUEVA_COMPOSICIONFAMILIAR.Visible = true;

                Panel_Botones_Pestanas.Visible = true;
                Button_Siguiente.Visible = true;
                Button_Guardar.Visible = true;

                Button_CANCELAR_1.Visible = true;
                break;
        }
    }

    private void cargar_DropDownList_PaisNacimiento()
    {
        DropDownList_PaisNacimiento.Items.Clear();

        Pais _pais = new Pais(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaPaises = _pais.ObtenerTodos();

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_PaisNacimiento.Items.Add(item);

        foreach (DataRow fila in tablaPaises.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_PAIS"].ToString());
            DropDownList_PaisNacimiento.Items.Add(item);
        }

        DropDownList_PaisNacimiento.DataBind();
    }

    private void cargar_DropDownList_SEXO()
    {
        DropDownList_SEXO.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_SEXO);

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_SEXO.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_SEXO.Items.Add(item);
        }
        DropDownList_SEXO.DataBind();
    }




    private void cargar_DropDownList_RH()
    {
        DropDownList_RH.Items.Clear();

        DropDownList_RH.Items.Add(new ListItem("Seleccione...", ""));

        DropDownList_RH.Items.Add(new ListItem("O-", "O-"));
        DropDownList_RH.Items.Add(new ListItem("O+", "O+"));
        DropDownList_RH.Items.Add(new ListItem("A-", "A-"));
        DropDownList_RH.Items.Add(new ListItem("A+", "A+"));
        DropDownList_RH.Items.Add(new ListItem("B-", "B-"));
        DropDownList_RH.Items.Add(new ListItem("B+", "B+"));
        DropDownList_RH.Items.Add(new ListItem("AB-", "AB-"));
        DropDownList_RH.Items.Add(new ListItem("AB+", "AB+"));

        DropDownList_RH.DataBind();
    }



    private void cargamos_DropDownList_TIP_DOC_IDENTIDAD()
    {
        DropDownList_TIP_DOC_IDENTIDAD.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_TIP_DOC_ID);

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_TIP_DOC_IDENTIDAD.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_TIP_DOC_IDENTIDAD.Items.Add(item);
        }
        DropDownList_TIP_DOC_IDENTIDAD.DataBind();
    }

    private void cargar_DropDownList_DEPARTAMENTO_CEDULA()
    {
        DropDownList_DEPARTAMENTO_CEDULA.Items.Clear();

        departamento _departamento = new departamento(Session["idEmpresa"].ToString());
        DataTable tablaDepartamentos = _departamento.ObtenerTodasLosDepartamentos();

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_DEPARTAMENTO_CEDULA.Items.Add(item);

        foreach (DataRow fila in tablaDepartamentos.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_DEPARTAMENTO"].ToString());
            DropDownList_DEPARTAMENTO_CEDULA.Items.Add(item);
        }

        DropDownList_DEPARTAMENTO_CEDULA.DataBind();
    }

    private void Cargar_DropDownListVacio(DropDownList drop)
    {
        drop.Items.Clear();

        drop.Items.Add(new ListItem("Seleccione...", ""));

        drop.DataBind();
    }

    private void Cargar_DropDownList_CAT_LIC_COND()
    {
        DropDownList_CAT_LIC_COND.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_CAT_LICENCIA_CONDUCCION);

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_CAT_LIC_COND.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_CAT_LIC_COND.Items.Add(item);
        }
        DropDownList_CAT_LIC_COND.DataBind();
    }

    private void cargar_DropDownList_DEPARTAMENTO_ASPIRANTE()
    {
        DropDownList_DEPARTAMENTO_ASPIRANTE.Items.Clear();

        departamento _departamento = new departamento(Session["idEmpresa"].ToString());
        DataTable tablaDepartamentos = _departamento.ObtenerTodasLosDepartamentos();

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_DEPARTAMENTO_ASPIRANTE.Items.Add(item);

        foreach (DataRow fila in tablaDepartamentos.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_DEPARTAMENTO"].ToString());
            DropDownList_DEPARTAMENTO_ASPIRANTE.Items.Add(item);
        }

        DropDownList_DEPARTAMENTO_ASPIRANTE.DataBind();
    }

    private void cargar_DropDownList_TipoVivienda()
    {
        DropDownList_TipoVivienda.Items.Clear();

        DropDownList_TipoVivienda.Items.Add(new ListItem("Seleccione...", ""));
        DropDownList_TipoVivienda.Items.Add(new ListItem("ARRIENDO", "ARRIENDO"));
        DropDownList_TipoVivienda.Items.Add(new ListItem("FAMILIAR", "FAMILIAR"));
        DropDownList_TipoVivienda.Items.Add(new ListItem("PROPIA", "PROPIA"));

        DropDownList_TipoVivienda.DataBind();
    }

    private void cargar_DropDownList_NIV_EDUCACION()
    {
        DropDownList_NIV_EDUCACION.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerNivEstudiosParametros();

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_NIV_EDUCACION.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_NIV_EDUCACION.Items.Add(item);
        }

        DropDownList_NIV_EDUCACION.DataBind();
    }

    private void cargar_DropDownList_NUCLEO_FORMACION()
    {
        DropDownList_nucleo_formacion.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla("NUCLEO_FORMACION");

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_nucleo_formacion.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_nucleo_formacion.Items.Add(item);
        }
        DropDownList_nucleo_formacion.DataBind();
    }

    private void cargar_DropDownList_EXPERIENCIA()
    {
        DropDownList_EXPERIENCIA.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_EXPERIENCIA);

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_EXPERIENCIA.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_EXPERIENCIA.Items.Add(item);
        }
        DropDownList_EXPERIENCIA.DataBind();
    }

    private void cargar_DropDownList_AREAS_ESPECIALIZACION()
    {
        DropDownList_AREAS_ESPECIALIZACION.Items.Clear();

        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaAreasEspecializacion = _radicacionHojasDeVida.ObtenerAreasInteresLaboral();

        ListItem item = new ListItem("Ninguna", "0");
        DropDownList_AREAS_ESPECIALIZACION.Items.Add(item);

        int contador = 0;

        foreach (DataRow fila in tablaAreasEspecializacion.Rows)
        {
            if (contador > 0)
            {
                item = new ListItem(fila["DESCRIPCION"].ToString(), fila["ID_AREAINTERES"].ToString());
                DropDownList_AREAS_ESPECIALIZACION.Items.Add(item);
            }
            contador += 1;
        }

        DropDownList_AREAS_ESPECIALIZACION.DataBind();
    }

    private void cargamos_DropDownList_ESTADO_CIVIL()
    {
        DropDownList_ESTADO_CIVIL.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla(tabla.PARAMETROS_ESTADO_CIVIL);

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_ESTADO_CIVIL.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_ESTADO_CIVIL.Items.Add(item);
        }
        DropDownList_ESTADO_CIVIL.DataBind();
    }

    private void Cargar_DropDownList_CabezaFamilia()
    {
        DropDownList_CabezaFamilia.Items.Clear();

        DropDownList_CabezaFamilia.Items.Add(new ListItem("Seleccione...",""));

        DropDownList_CabezaFamilia.Items.Add(new ListItem("NO", "N"));
        DropDownList_CabezaFamilia.Items.Add(new ListItem("SI", "S"));

        DropDownList_CabezaFamilia.DataBind();
    }

    private void cargar_DropDownList_TALLAS_CAMISA()
    {
        DropDownList_Talla_Camisa.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla("TALLA_ROPA");

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_Talla_Camisa.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_Talla_Camisa.Items.Add(item);
        }
        DropDownList_Talla_Camisa.DataBind();
    }

    private void cargar_DropDownList_TALLAS_PANTALON()
    {

        DropDownList_Talla_Pantalon.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla("TALLA_ROPA");

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_Talla_Pantalon.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_Talla_Pantalon.Items.Add(item);
        }

        DropDownList_Talla_Pantalon.DataBind();
    }

    private void cargar_DropDownList_TALLAS_ZAPATOS()
    {
        DropDownList_talla_zapatos.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerParametrosPorTabla("TALLA_ROPA");

        ListItem item = new ListItem("Ninguno", "");
        DropDownList_talla_zapatos.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            DropDownList_talla_zapatos.Items.Add(item);
        }
        DropDownList_talla_zapatos.DataBind();
    }

    private void cargar_DropDownList_ID_FUENTE()
    {
        DropDownList_ID_FUENTE.Items.Clear();

        fuentesReclutamiento _fuentesReclutamiento = new fuentesReclutamiento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaFuentes = _fuentesReclutamiento.ObtenerRecFuentesTodos();

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_ID_FUENTE.Items.Add(item);

        foreach (DataRow fila in tablaFuentes.Rows)
        {
            item = new ListItem(fila["NOM_FUENTE"].ToString(), fila["ID_FUENTE"].ToString());
            DropDownList_ID_FUENTE.Items.Add(item);
        }

        DropDownList_ID_FUENTE.DataBind();
    }

    private void Cargar_DropDownList_ComoSeEntero()
    {
        DropDownList_ComoSeEntero.Items.Clear();

        DropDownList_ComoSeEntero.Items.Add(new ListItem("Seleccione...",""));
        
        DropDownList_ComoSeEntero.Items.Add(new ListItem("TELEVISIÓN", "TELEVISIÓN"));
        DropDownList_ComoSeEntero.Items.Add(new ListItem("RADIO", "RADIO"));
        DropDownList_ComoSeEntero.Items.Add(new ListItem("BUSCADOR DE EMPLEO EN INTERNET", "BUSCADOR DE EMPLEO EN INTERNET"));
        DropDownList_ComoSeEntero.Items.Add(new ListItem("UN AMIGO", "UN AMIGO"));
        DropDownList_ComoSeEntero.Items.Add(new ListItem("PRENSA", "PRENSA"));
        DropDownList_ComoSeEntero.Items.Add(new ListItem("REDES SOCIALES", "FACEBOOK"));
        DropDownList_ComoSeEntero.Items.Add(new ListItem("LLAMADA TELEFÓNICA", "LLAMADA TELEFÓNICA"));

        DropDownList_ComoSeEntero.DataBind();
    }

    private void SetActivoTab(Int32 index)
    {
        Tab_DatosBasicos.CssClass = "botonInactivoOk";
        Tab_Ubicacion.CssClass = "botonInactivoOk";
        Tab_Educacion.CssClass = "botonInactivoOk";
        Tab_DatosLaborales.CssClass = "botonInactivoOk";
        Tab_Familia.CssClass = "botonInactivoOk";
        Tab_Adicionales.CssClass = "botonInactivoOk";

        MainView.ActiveViewIndex = index;

        switch (index)
        { 
            case 0:
                Tab_DatosBasicos.CssClass = "botonActivo";
                break;
            case 1:
                Tab_Ubicacion.CssClass = "botonActivo";
                break;
            case 2:
                Tab_Educacion.CssClass = "botonActivo";
                break;
            case 3:
                Tab_DatosLaborales.CssClass = "botonActivo";
                break;
            case 4:
                Tab_Familia.CssClass = "botonActivo";
                break;
            case 5:
                Tab_Adicionales.CssClass = "botonActivo";
                break;
        }

        if ((HiddenField_EstadoDatosBasicos.Value == "N") && (index != 0))
        {
            Tab_DatosBasicos.CssClass = "botonInactivoFail";
        }

        if ((HiddenField_EstadoUbicacion.Value == "N") && (index != 1))
        {
            Tab_Ubicacion.CssClass = "botonInactivoFail";
        }

        if ((HiddenField_EstadoEducacion.Value == "N") && (index != 2))
        {
            Tab_Educacion.CssClass = "botonInactivoFail";
        }

        if ((HiddenField_EstadoDatosLaborales.Value == "N") && (index != 3))
        {
            Tab_DatosLaborales.CssClass = "botonInactivoFail";
        }

        if ((HiddenField_EstadoFamilia.Value == "N") && (index != 4))
        {
            Tab_Familia.CssClass = "botonInactivoFail";
        }

        if ((HiddenField_DatosAdicionales.Value == "N") && (index != 5))
        {
            Tab_Adicionales.CssClass = "botonInactivoFail";
        }
    }

    private void Cargar(Acciones accion)
    {
        switch (accion)
        {
            case Acciones.Inicio:
                    HiddenField_AccionSobreFormulario.Value = AccionesForm.Ninguna.ToString();
                break;
            case Acciones.Nuevo:
                HiddenField_ID_SOLICITUD.Value = "";

                HiddenField_EstadoDatosBasicos.Value = "N";
                HiddenField_EstadoUbicacion.Value = "N";
                HiddenField_EstadoEducacion.Value = "N";
                HiddenField_EstadoDatosLaborales.Value = "N";
                HiddenField_EstadoFamilia.Value = "N";
                HiddenField_DatosAdicionales.Value = "N";

                Tab_DatosBasicos.CssClass = "botonActivo";
                Tab_Ubicacion.CssClass = "botonInactivoFail";
                Tab_Educacion.CssClass = "botonInactivoFail";
                Tab_DatosLaborales.CssClass = "botonInactivoFail";
                Tab_Familia.CssClass = "botonInactivoFail";
                Tab_Adicionales.CssClass = "botonInactivoFail";
                Tab_Ubicacion.CssClass = "botonInactivoFail";

                TextBox_FCH_NACIMIENTO.Text = "";
                cargar_DropDownList_PaisNacimiento();
                cargar_DropDownList_SEXO();

                cargar_DropDownList_RH();

                cargamos_DropDownList_TIP_DOC_IDENTIDAD();
                TextBox_NUM_DOC_IDENTIDAD.Text = TextBox_VALIDAR_CEDULA.Text;
                cargar_DropDownList_DEPARTAMENTO_CEDULA();
                Cargar_DropDownListVacio(DropDownList_CIU_CEDULA);
                TextBox_NOMBRES.Text = "";
                TextBox_APELLIDOS.Text = "";
                TextBox_LIB_MILITAR.Text = "";
                Cargar_DropDownList_CAT_LIC_COND();
                cargar_DropDownList_DEPARTAMENTO_ASPIRANTE();
                Cargar_DropDownListVacio(DropDownList_CIU_ASPIRANTE);
                TextBox_DIR_ASPIRANTE.Text = "";
                TextBox_SECTOR.Text = "";
                cargar_DropDownList_TipoVivienda();
                cargar_DropDownList_NIV_EDUCACION();
                cargar_DropDownList_NUCLEO_FORMACION();

                HiddenField_ACCION_GRILLA_INFOACADEMMICA_EF.Value = AccionesGrilla.Ninguna.ToString();
                GridView_EducacionFormal.DataSource = null;
                GridView_EducacionFormal.DataBind();

                HiddenField_ACCION_GRILLA_INFOACADEMMIC_ENF.Value = AccionesGrilla.Ninguna.ToString();
                GridView_EducacionNoFormal.DataSource = null;
                GridView_EducacionNoFormal.DataBind();

                TextBox_BUSCADOR_CARGO.Text = "";
                Cargar_DropDownListVacio(DropDownList_ID_OCUPACION);
                cargar_DropDownList_EXPERIENCIA();
                TextBox_ASPIRACION_SALARIAL.Text = "";
                cargar_DropDownList_AREAS_ESPECIALIZACION();

                HiddenField_ACCION_GRILLA_EXPLABORAL.Value = AccionesGrilla.Ninguna.ToString();
                GridView_ExperienciaLaboral.DataSource = null;
                GridView_ExperienciaLaboral.DataBind();

                cargamos_DropDownList_ESTADO_CIVIL();
                TextBox_NUM_HIJOS.Text = "0";
                Cargar_DropDownList_CabezaFamilia();
                HiddenField_ACCION_GRILLA_COMPOSICIONFAMILIAR.Value = AccionesGrilla.Ninguna.ToString();
                GridView_ComposicionFamiliar.DataSource = null;
                GridView_ComposicionFamiliar.DataBind();

                TextBox_TEL_ASPIRANTE.Text = "";
                TextBox_CEL_ASPIRANTE.Text = "";
                TextBox_E_MAIL.Text = "";

                cargar_DropDownList_TALLAS_CAMISA();
                cargar_DropDownList_TALLAS_PANTALON();
                cargar_DropDownList_TALLAS_ZAPATOS();
                cargar_DropDownList_ID_FUENTE();
                Cargar_DropDownList_ComoSeEntero();

                HiddenField_IndexTab.Value = "0";
                SetActivoTab(Convert.ToInt32(HiddenField_IndexTab.Value));

                HiddenField_AccionSobreFormulario.Value = AccionesForm.Nuevo.ToString();

                Label_EstadoAspirante.Text = "ASPIRANTE";
                Label_FechaIngreso.Text = DateTime.Now.ToShortDateString();
                break;
            case Acciones.Modificar:
                HiddenField_IndexTab.Value = "0";
                SetActivoTab(0);

                HiddenField_AccionSobreFormulario.Value = AccionesForm.Modificar.ToString();
                break;
        }
    }

    private void Iniciar()
    {
        Configurar();
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Mostrar(Acciones.Inicio);
        Cargar(Acciones.Inicio);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            Iniciar();
        }
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

    private void Activar(Acciones accion)
    { 
        switch(accion)
        {
            case Acciones.Nuevo:
                TextBox_FCH_NACIMIENTO.Enabled = true;
                DropDownList_PaisNacimiento.Enabled = true;
                DropDownList_SEXO.Enabled = true;

                DropDownList_RH.Enabled = true;

                DropDownList_TIP_DOC_IDENTIDAD.Enabled = true;
                TextBox_NOMBRES.Enabled = true;
                TextBox_APELLIDOS.Enabled = true;
                TextBox_LIB_MILITAR.Enabled = true;
                DropDownList_CAT_LIC_COND.Enabled = true;

                DropDownList_DEPARTAMENTO_ASPIRANTE.Enabled = true;
                DropDownList_CIU_ASPIRANTE.Enabled = true;
                TextBox_DIR_ASPIRANTE.Enabled = true;
                TextBox_SECTOR.Enabled = true;
                DropDownList_TipoVivienda.Enabled = true;
                DropDownList_ESTRATO.Enabled = true;
                DropDownList_nucleo_formacion.Enabled = true;

                TextBox_BUSCADOR_CARGO.Enabled = true;
                DropDownList_ID_OCUPACION.Enabled = true;
                DropDownList_EXPERIENCIA.Enabled = true;
                TextBox_ASPIRACION_SALARIAL.Enabled = true;
                DropDownList_AREAS_ESPECIALIZACION.Enabled = true;

                DropDownList_ESTADO_CIVIL.Enabled = true;
                DropDownList_CabezaFamilia.Enabled = true;

                TextBox_TEL_ASPIRANTE.Enabled = true;
                TextBox_CEL_ASPIRANTE.Enabled = true;
                TextBox_E_MAIL.Enabled = true;
                DropDownList_Talla_Camisa.Enabled = true;
                DropDownList_Talla_Pantalon.Enabled = true;
                DropDownList_talla_zapatos.Enabled = true;

                DropDownList_ID_FUENTE.Enabled = true;
                DropDownList_ComoSeEntero.Enabled = true;
                break;
            case Acciones.Modificar:
                TextBox_FCH_NACIMIENTO.Enabled = true;
                DropDownList_PaisNacimiento.Enabled = true;
                DropDownList_SEXO.Enabled = true;

                DropDownList_RH.Enabled = true;

                DropDownList_TIP_DOC_IDENTIDAD.Enabled = true;
                TextBox_NUM_DOC_IDENTIDAD.Enabled = true;
                if (DropDownList_PaisNacimiento.SelectedValue == "170")
                {
                    DropDownList_DEPARTAMENTO_CEDULA.Enabled = true;
                    DropDownList_CIU_CEDULA.Enabled = true;

                    RFV_DropDownList_CIU_CEDULA.Enabled = true;
                    ValidatorCalloutExtender_DropDownList_CIU_CEDULA.Enabled = true;
                }
        
                TextBox_NOMBRES.Enabled = true;
                TextBox_APELLIDOS.Enabled = true;

                if (DropDownList_SEXO.SelectedValue == "M") 
                {
                    TextBox_LIB_MILITAR.Enabled = true;
                    RequiredFieldValidator_TextBox_LIB_MILITAR.Enabled = true;
                    ValidatorCalloutExtender_TextBox_LIB_MILITAR.Enabled = true;
                }

                DropDownList_CAT_LIC_COND.Enabled = true;

                DropDownList_DEPARTAMENTO_ASPIRANTE.Enabled = true;
                DropDownList_CIU_ASPIRANTE.Enabled = true;
                TextBox_DIR_ASPIRANTE.Enabled = true;
                TextBox_SECTOR.Enabled = true;
                DropDownList_TipoVivienda.Enabled = true;
                DropDownList_ESTRATO.Enabled = true;
                DropDownList_nucleo_formacion.Enabled = true;

                TextBox_BUSCADOR_CARGO.Enabled = true;
                DropDownList_ID_OCUPACION.Enabled = true;
                DropDownList_EXPERIENCIA.Enabled = true;
                TextBox_ASPIRACION_SALARIAL.Enabled = true;
                DropDownList_AREAS_ESPECIALIZACION.Enabled = true;

                DropDownList_ESTADO_CIVIL.Enabled = true;
                DropDownList_CabezaFamilia.Enabled = true;

                TextBox_TEL_ASPIRANTE.Enabled = true;
                TextBox_CEL_ASPIRANTE.Enabled = true;
                TextBox_E_MAIL.Enabled = true;
                DropDownList_Talla_Camisa.Enabled = true;
                DropDownList_Talla_Pantalon.Enabled = true;
                DropDownList_talla_zapatos.Enabled = true;

                DropDownList_ID_FUENTE.Enabled = true;
                DropDownList_ComoSeEntero.Enabled = true;
                break;
        }
    }

    private void CargarNuevoRegistro()
    {
        Ocultar(Acciones.Inicio);
        Desactivar(Acciones.Inicio);
        Mostrar(Acciones.Nuevo);
        Activar(Acciones.Nuevo);
        Cargar(Acciones.Nuevo);
    }


    protected void Button_VALIDAR_CEDULA_Click(object sender, EventArgs e)
    {
        String NUM_DOC_IDENTIDAD = TextBox_VALIDAR_CEDULA.Text;

        DataTable tablaInfoSolicitud;

        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        tablaInfoSolicitud = _radicacionHojasDeVida.ObtenerRegSolicitudesingresoPorNumDocIdentidadValAcoset(NUM_DOC_IDENTIDAD);

        if (String.IsNullOrEmpty(_radicacionHojasDeVida.MensajeError) == false)
        {
            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _radicacionHojasDeVida.MensajeError, Proceso.Error);

            Ocultar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
        }
        else
        {
            if (tablaInfoSolicitud.Rows.Count <= 0)
            {
                CargarNuevoRegistro();
            }
            else
            {
                DataRow filaSolicitud = tablaInfoSolicitud.Rows[0];
                Decimal ID_SOLICITUD = Convert.ToDecimal(filaSolicitud["ID_SOLICITUD"]);

                Ocultar(Acciones.Inicio);
                Desactivar(Acciones.Inicio);
                Mostrar(Acciones.Cargar);

                HiddenField_AccionSobreFormulario.Value = AccionesForm.Cargar.ToString();

                Cargar(ID_SOLICITUD);
            }
        }
    }
    protected void Button_MODIFICAR_Click(object sender, EventArgs e)
    {
        Ocultar(Acciones.Modificar);
        Mostrar(Acciones.Modificar);
        Activar(Acciones.Modificar);
        Cargar(Acciones.Modificar);
    }

    protected void Button_CANCELAR_Click(object sender, EventArgs e)
    {
        if (String.IsNullOrEmpty(HiddenField_ID_SOLICITUD.Value) == false)
        {
            Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);

            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Cargar);

            HiddenField_AccionSobreFormulario.Value = AccionesForm.Cargar.ToString();

            Cargar(ID_SOLICITUD);
        }
        else
        {
            Ocultar(Acciones.Inicio);
            Desactivar(Acciones.Inicio);
            Mostrar(Acciones.Inicio);
            Cargar(Acciones.Inicio);
        }
    }

    private Boolean IsFechaCorrecta(String valor)
    {
        Boolean resultado = true;
        DateTime fecha;

        if (String.IsNullOrEmpty(valor) == true)
        {
            resultado = false;
        }
        else
        {
            resultado = true;
        }

        if (resultado == true)
        {
            try
            {
                fecha = Convert.ToDateTime(valor);
            }
            catch
            {
                resultado = false;
            }
        }

        return resultado;
    }

    private Boolean IsDropCorrecto(DropDownList drop)
    {
        if (drop.SelectedIndex <= 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private Boolean IsNotNull(String valor)
    {
        if (String.IsNullOrEmpty(valor) == true)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private Boolean ComprobarDatosBasicos(Boolean informar)
    { 
        Boolean correcto = true;

        if(IsFechaCorrecta(TextBox_FCH_NACIMIENTO.Text) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La fecha de nacimiento no tiene un formato correcto.", Proceso.Advertencia);
                }
            }
        }

        if (IsDropCorrecto(DropDownList_PaisNacimiento) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar un valor para el país de nacimiento del aspirante.", Proceso.Advertencia);
                }
            }
        }

        if (IsDropCorrecto(DropDownList_SEXO) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar un valor para el sexo del aspirante.", Proceso.Advertencia);
                }
            }
        }

        if (IsDropCorrecto(DropDownList_RH) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar un valor para el RH del aspirante.", Proceso.Advertencia);
                }
            }
        }

        if (IsDropCorrecto(DropDownList_TIP_DOC_IDENTIDAD) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar un valor para el tipo de documento de identidad del aspirante.", Proceso.Advertencia);
                }
            }
        }

        if (IsNotNull(TextBox_NUM_DOC_IDENTIDAD.Text) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe digitar un valor el número de documento de identidad del aspirante.", Proceso.Advertencia);
                }
            }
        }

        if (DropDownList_TIP_DOC_IDENTIDAD.SelectedValue != "CE")
        {
            if (IsDropCorrecto(DropDownList_CIU_CEDULA) == false)
            {
                correcto = false;
                if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
                {
                    if (informar == true)
                    {
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar un valor para la ciudad de expedición del documento de identidad.", Proceso.Advertencia);
                    }
                }
            }
        }

        if (IsNotNull(TextBox_NOMBRES.Text) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe digitar los nombres.", Proceso.Advertencia);
                }
            }
        }

        if (IsNotNull(TextBox_APELLIDOS.Text) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe digitar los apellidos.", Proceso.Advertencia);
                }
            }
        }

        if (DropDownList_SEXO.SelectedValue == "M")
        {
            if (IsNotNull(TextBox_LIB_MILITAR.Text) == false)
            {
                correcto = false;
                if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
                {
                    if (informar == true)
                    {
                        Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe digitar el numero de libreta militar.", Proceso.Advertencia);
                    }
                }
            }

        }

        if((IsNotNull(TextBox_TEL_ASPIRANTE.Text) == false) && (IsNotNull(TextBox_CEL_ASPIRANTE.Text) == false))
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe digitar por lo menos uno de los siguients datos: -Teléfono- - Celular-.", Proceso.Advertencia);
                }
            }
        }

        if (correcto == true)
        { 
            HiddenField_EstadoDatosBasicos.Value = "S";
        }

        return correcto;
    }

    private Boolean ComprobarUbicacion(Boolean informar)
    {
        Boolean correcto = true;

        if (IsDropCorrecto(DropDownList_CIU_ASPIRANTE) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar un valor para la ciudad donde vive.", Proceso.Advertencia);
                }
            }
        }

        if (IsNotNull(TextBox_DIR_ASPIRANTE.Text) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe digitar  un valor para dirección.", Proceso.Advertencia);
                }
            }
        }

        if (IsNotNull(TextBox_SECTOR.Text) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe digitar  un valor para Barrio.", Proceso.Advertencia);
                }
            }
        }

        if (IsDropCorrecto(DropDownList_TipoVivienda) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar un valor para el tipo de vivienda.", Proceso.Advertencia);
                }
            }
        }

        if (IsDropCorrecto(DropDownList_ESTRATO) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar un valor para el estrato.", Proceso.Advertencia);
                }
            }
        }

        if (correcto == true)
        {
            HiddenField_EstadoUbicacion.Value = "S";
        }

        return correcto;
    }

    private Boolean ComprobarEducacion(Boolean informar)
    {
        Boolean correcto = true;

        if (IsDropCorrecto(DropDownList_NIV_EDUCACION) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar el nivel de escolaridad.", Proceso.Advertencia);
                }
            }
        }

        if (IsDropCorrecto(DropDownList_nucleo_formacion) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar la profesión.", Proceso.Advertencia);
                }
            }
        }

        if (HiddenField_ACCION_GRILLA_INFOACADEMMICA_EF.Value != AccionesGrilla.Ninguna.ToString())
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe terminar las acciones sobre la grilla de información académica formal.", Proceso.Advertencia);
                }
            }
        }

        if (GridView_EducacionFormal.Rows.Count <= 0)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe crear por lo menos un registro en la grilla de infrmación académica formal.", Proceso.Advertencia);
                }
            }
        }

        if (HiddenField_ACCION_GRILLA_INFOACADEMMIC_ENF.Value != AccionesGrilla.Ninguna.ToString())
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe terminar las acciones sobre la grilla de información académica no formal.", Proceso.Advertencia);
                }
            }
        }

        if (correcto == true)
        {
            HiddenField_EstadoEducacion.Value = "S";
        }

        return correcto;
    }

    private Boolean ComprabarDatosLaborales(Boolean informar)
    {
        Boolean correcto = true;

        if (IsDropCorrecto(DropDownList_ID_OCUPACION) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar el cargoa al que aplica.", Proceso.Advertencia);
                }
            }
        }

        if (IsDropCorrecto(DropDownList_EXPERIENCIA) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar la experiencia.", Proceso.Advertencia);
                }
            }
        }

        if (IsNotNull(TextBox_ASPIRACION_SALARIAL.Text) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe digitar el salario que aspira.", Proceso.Advertencia);
                }
            }
        }

        if (IsDropCorrecto(DropDownList_AREAS_ESPECIALIZACION) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar el área de interés.", Proceso.Advertencia);
                }
            }
        }

        if(HiddenField_ACCION_GRILLA_EXPLABORAL.Value != AccionesGrilla.Ninguna.ToString())
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe terminar las acciones sobre la grilla de experiencia laboral.", Proceso.Advertencia);
                }
            }
        }

        if (correcto == true)
        {
            HiddenField_EstadoDatosLaborales.Value = "S";
        }

        return correcto;
    }

    private Boolean ComprobarFamilia(Boolean informar)
    {
        Boolean correcto = true;

        if (IsDropCorrecto(DropDownList_ESTADO_CIVIL) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar el estado civil", Proceso.Advertencia);
                }
            }
        }

        if(IsNotNull(TextBox_NUM_HIJOS.Text) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe digitar el numero de hijos.", Proceso.Advertencia);
                }
            }
        }

        if (IsDropCorrecto(DropDownList_CabezaFamilia) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar si es o no cabeza de familia.", Proceso.Advertencia);
                }
            }
        }

        if (HiddenField_ACCION_GRILLA_COMPOSICIONFAMILIAR.Value != AccionesGrilla.Ninguna.ToString())
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe terminar las acciones sobre la grilla de composición familiar.", Proceso.Advertencia);
                }
            }
        }

        if (correcto == true)
        {
            HiddenField_EstadoFamilia.Value = "S";
        }

        return correcto;
    }

    private Boolean ComprobarAdicionales(Boolean informar)
    {
        Boolean correcto = true;

        if (IsDropCorrecto(DropDownList_Talla_Camisa) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar la talla de camisa.", Proceso.Advertencia);
                }
            }
        }

        if (IsDropCorrecto(DropDownList_Talla_Pantalon) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar la talla de pantalón.", Proceso.Advertencia);
                }
            }
        }

        if (IsDropCorrecto(DropDownList_talla_zapatos) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar la talla de zapatos.", Proceso.Advertencia);
                }
            }
        }

        if (IsDropCorrecto(DropDownList_ID_FUENTE) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar la fuente de reclutamiento.", Proceso.Advertencia);
                }
            }
        }

        if (IsDropCorrecto(DropDownList_ComoSeEntero) == false)
        {
            correcto = false;
            if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
            {
                if (informar == true)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "Debe seleccionar la opción 'Cómo se entero de nosotros'.", Proceso.Advertencia);
                }
            }
        }

        if (correcto == true)
        {
            HiddenField_DatosAdicionales.Value = "S";
        }

        return correcto;
    }

    private Boolean ComprobarDatosPestaña(Int32 index, Boolean informar)
    {
        Boolean resultado = true;
        switch (index)
        { 
            case 0:
                resultado = ComprobarDatosBasicos(informar);
                break;
            case 1:
                resultado = ComprobarUbicacion(informar);
                break;
            case 2:
                resultado = ComprobarEducacion(informar);
                break;
            case 3:
                resultado = ComprabarDatosLaborales(informar);
                break;
            case 4:
                resultado = ComprobarFamilia(informar);
                break;
            case 5:
                resultado = ComprobarAdicionales(informar);
                break;
        }

        return resultado;
    }

    protected void Tab_DatosBasicos_Click(object sender, EventArgs e)
    {
        ComprobarTodasLasPestanas(false);

        Boolean resultado = true;
        if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
        {
            resultado = ComprobarDatosPestaña(Convert.ToInt32(HiddenField_IndexTab.Value), true);
        }

        if (resultado == true)
        {
            HiddenField_IndexTab.Value = "0";
            SetActivoTab(0);
        }
      
    }
    protected void DropDownList_PaisNacimiento_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_PaisNacimiento.SelectedIndex <= 0)
        {
            DropDownList_TIP_DOC_IDENTIDAD.SelectedIndex = 0;
            DropDownList_TIP_DOC_IDENTIDAD.Enabled = true;

            DropDownList_DEPARTAMENTO_CEDULA.SelectedIndex = 0;
            Cargar_DropDownListVacio(DropDownList_CIU_CEDULA);
            DropDownList_DEPARTAMENTO_CEDULA.Enabled = true;

            RFV_DropDownList_CIU_CEDULA.Enabled = true;
            ValidatorCalloutExtender_DropDownList_CIU_CEDULA.Enabled = true;
        }
        else
        {
            if (DropDownList_PaisNacimiento.SelectedValue.Trim() != "170")
            {
                DropDownList_TIP_DOC_IDENTIDAD.SelectedValue = "CE";
                DropDownList_TIP_DOC_IDENTIDAD.Enabled = false;

                DropDownList_DEPARTAMENTO_CEDULA.SelectedIndex = 0;
                DropDownList_DEPARTAMENTO_CEDULA.Enabled = false;
                Cargar_DropDownListVacio(DropDownList_CIU_CEDULA);
                DropDownList_CIU_CEDULA.Enabled = false;

                RFV_DropDownList_CIU_CEDULA.Enabled = false;
                ValidatorCalloutExtender_DropDownList_CIU_CEDULA.Enabled = false;

            }
            else
            {
                DropDownList_TIP_DOC_IDENTIDAD.SelectedIndex = 0;
                DropDownList_TIP_DOC_IDENTIDAD.Enabled = true;

                DropDownList_DEPARTAMENTO_CEDULA.SelectedIndex = 0;
                DropDownList_DEPARTAMENTO_CEDULA.Enabled = true;
                Cargar_DropDownListVacio(DropDownList_CIU_CEDULA);
                DropDownList_CIU_CEDULA.Enabled = true;

                RFV_DropDownList_CIU_CEDULA.Enabled = true;
                ValidatorCalloutExtender_DropDownList_CIU_CEDULA.Enabled = true;
            }
        }
    }

    private void cargar_DropDownList_CIU_CEDULA(String idDepartamento)
    {
        DropDownList_CIU_CEDULA.Items.Clear();

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
        DataTable tablaCiudades = _ciudad.ObtenerCiudadesPorIdDepartamento(idDepartamento);

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_CIU_CEDULA.Items.Add(item);

        foreach (DataRow fila in tablaCiudades.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_CIUDAD"].ToString());
            DropDownList_CIU_CEDULA.Items.Add(item);
        }

        DropDownList_CIU_CEDULA.DataBind();
    }

    protected void DropDownList_DEPARTAMENTO_CEDULA_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_DEPARTAMENTO_CEDULA.SelectedValue == "")
        {
            Cargar_DropDownListVacio(DropDownList_CIU_ASPIRANTE);

            DropDownList_DEPARTAMENTO_CEDULA.Focus();
        }
        else
        {
            String ID_DEPARTAMENTO = DropDownList_DEPARTAMENTO_CEDULA.SelectedValue.ToString();
            cargar_DropDownList_CIU_CEDULA(ID_DEPARTAMENTO);
            DropDownList_CIU_CEDULA.Enabled = true;

            DropDownList_CIU_CEDULA.Focus();
        }
    }
    protected void DropDownList_SEXO_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_SEXO.SelectedValue == "M")
        {
            TextBox_LIB_MILITAR.Text = "";
            TextBox_LIB_MILITAR.Enabled = true;
            RequiredFieldValidator_TextBox_LIB_MILITAR.Enabled = true;
            ValidatorCalloutExtender_TextBox_LIB_MILITAR.Enabled = true;
        }
        else
        {
            TextBox_LIB_MILITAR.Text = "";
            TextBox_LIB_MILITAR.Enabled = false;
            RequiredFieldValidator_TextBox_LIB_MILITAR.Enabled = false;
            ValidatorCalloutExtender_TextBox_LIB_MILITAR.Enabled = false;
        }
    }
    protected void Tab_Ubicacion_Click(object sender, EventArgs e)
    {
        ComprobarTodasLasPestanas(false);

        Boolean resultado = true;
        if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
        {
            resultado = ComprobarDatosPestaña(Convert.ToInt32(HiddenField_IndexTab.Value), true);
        }

        if (resultado == true)
        {
            HiddenField_IndexTab.Value = "1";
            SetActivoTab(1);
        }
    }
    protected void Tab_Educacion_Click(object sender, EventArgs e)
    {
        ComprobarTodasLasPestanas(false);

        Boolean resultado = true;
        if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
        {
            resultado = ComprobarDatosPestaña(Convert.ToInt32(HiddenField_IndexTab.Value), true);
        }

        if (resultado == true)
        {
            HiddenField_IndexTab.Value = "2";
            SetActivoTab(2);
        }
    }
    protected void Tab_DatosLaborales_Click(object sender, EventArgs e)
    {
        ComprobarTodasLasPestanas(false);

        Boolean resultado = true;
        if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
        {
            resultado = ComprobarDatosPestaña(Convert.ToInt32(HiddenField_IndexTab.Value), true);
        }

        if (resultado == true)
        {
            HiddenField_IndexTab.Value = "3";
            SetActivoTab(3);
        }
    }
    protected void Tab_Familia_Click(object sender, EventArgs e)
    {
        ComprobarTodasLasPestanas(false);

        Boolean resultado = true;
        if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
        {
            resultado = ComprobarDatosPestaña(Convert.ToInt32(HiddenField_IndexTab.Value), true);
        }

        if (resultado == true)
        {
            HiddenField_IndexTab.Value = "4";
            SetActivoTab(4);
        }
    }
    protected void Tab_Adicionales_Click(object sender, EventArgs e)
    {
        ComprobarTodasLasPestanas(false);

        Boolean resultado = true;
        if ((HiddenField_AccionSobreFormulario.Value == AccionesForm.Nuevo.ToString()) || (HiddenField_AccionSobreFormulario.Value == AccionesForm.Modificar.ToString()))
        {
            resultado = ComprobarDatosPestaña(Convert.ToInt32(HiddenField_IndexTab.Value), true);
        }

        if (resultado == true)
        {
            HiddenField_IndexTab.Value = "5";
            SetActivoTab(5);
        }
    }

    private void cargar_DropDownList_CIU_ASPIRANTE(String idDepartamento)
    {
        DropDownList_CIU_ASPIRANTE.Items.Clear();

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
        DataTable tablaCiudades = _ciudad.ObtenerCiudadesPorIdDepartamento(idDepartamento);

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_CIU_ASPIRANTE.Items.Add(item);

        foreach (DataRow fila in tablaCiudades.Rows)
        {
            item = new ListItem(fila["NOMBRE"].ToString(), fila["ID_CIUDAD"].ToString());
            DropDownList_CIU_ASPIRANTE.Items.Add(item);
        }

        DropDownList_CIU_ASPIRANTE.DataBind();
    }

    protected void DropDownList_DEPARTAMENTO_ASPIRANTE_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (DropDownList_DEPARTAMENTO_ASPIRANTE.SelectedValue == "")
        {
            Cargar_DropDownListVacio(DropDownList_CIU_ASPIRANTE);
        }
        else
        {
            String ID_DEPARTAMENTO = DropDownList_DEPARTAMENTO_ASPIRANTE.SelectedValue.ToString();
            cargar_DropDownList_CIU_ASPIRANTE(ID_DEPARTAMENTO);
            DropDownList_CIU_ASPIRANTE.Enabled = true;
        }
    }
    protected void GridView_EducacionFormal_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "modificar")
        {
            HiddenField_FILA_SELECCIONADA_GRILLA_INFOACADEMICA_EF.Value = indexSeleccionado.ToString();

            ActivarFilaGrilla(GridView_EducacionFormal, indexSeleccionado, 2);

            HiddenField_ACCION_GRILLA_INFOACADEMMICA_EF.Value = AccionesGrilla.Modificar.ToString();
            HiddenField_FILA_SELECCIONADA_GRILLA_INFOACADEMICA_EF.Value = indexSeleccionado.ToString();

            GridViewRow filaGrilla = GridView_EducacionFormal.Rows[indexSeleccionado];

            HiddenField_ID_INFO_ACADEMICA_EF.Value = GridView_EducacionFormal.DataKeys[indexSeleccionado].Values["ID_INFO_ACADEMICA"].ToString();

            DropDownList dropnivAcademico = filaGrilla.FindControl("DropDownList_NivAcademico") as DropDownList;
            HiddenField_NIV_ACADEMICO_EF.Value = dropnivAcademico.SelectedValue;

            TextBox textoInstitucion = filaGrilla.FindControl("TextBox_Institucion") as TextBox;
            HiddenField_INSTITUCION_EF.Value = textoInstitucion.Text;


            TextBox textoAnno = filaGrilla.FindControl("TextBox_Anno") as TextBox;
            if (String.IsNullOrEmpty(textoAnno.Text) == false)
            {
                HiddenField_ANNO_EF.Value = Convert.ToInt32(textoAnno.Text).ToString();
            }
            else
            {
                HiddenField_ANNO_EF.Value = "";
            }

            TextBox textoObservaciones = filaGrilla.FindControl("TextBox_Observaciones") as TextBox;
            HiddenField_OBSERVACIONES_EF.Value = textoObservaciones.Text;

            HiddenField_ACTIVO_EF.Value = "True";

            GridView_EducacionFormal.Columns[0].Visible = false;
            GridView_EducacionFormal.Columns[1].Visible = false;

            Button_NuevoEF.Visible = false;
            Button_GuardarEF.Visible = true;
            Button_CancelarEF.Visible = true;
        }
        else
        {
            if (e.CommandName == "eliminar")
            {
                DataTable tablaDesdeGrilla = obtenerTablaDeGrillaEducacionFormal();

                tablaDesdeGrilla.Rows.RemoveAt(indexSeleccionado);

                Cargar_Grilla_informacionEducativaFormal_Desdetabla(tablaDesdeGrilla);

                inhabilitarFilasGrilla(GridView_EducacionFormal, 2);

                GridView_EducacionFormal.Columns[0].Visible = true;
                GridView_EducacionFormal.Columns[1].Visible = true;

                HiddenField_ACCION_GRILLA_INFOACADEMMICA_EF.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_FILA_SELECCIONADA_GRILLA_INFOACADEMICA_EF.Value = null;

                Button_NuevoEF.Visible = true;
                Button_GuardarEF.Visible = false;
                Button_CancelarEF.Visible = false;
            }
        }

        SeleccionarDropNivelEscolaridad();
    }

    private DataTable ConfigurarTablaParaGrillaEducacionFormal()
    {
        DataTable tablaResultado = new DataTable();

        tablaResultado.Columns.Add("ID_INFO_ACADEMICA");
        tablaResultado.Columns.Add("NIVEL_ACADEMICO");
        tablaResultado.Columns.Add("INSTITUCION");
        tablaResultado.Columns.Add("ANNO");
        tablaResultado.Columns.Add("OBSERVACIONES");
        tablaResultado.Columns.Add("ACTIVO");

        return tablaResultado;
    }

    private DataTable obtenerTablaDeGrillaEducacionFormal()
    {
        DataTable tablaResultado = ConfigurarTablaParaGrillaEducacionFormal();

        DataRow filaTablaResultado;

        Decimal ID_INFO_ACADEMICA;
        String NIVEL_ACADEMICO;
        String INSTITUCION;
        String ANNO;
        String OBSERVACIONES;
        Boolean ACTIVO = true;

        for (int i = 0; i < GridView_EducacionFormal.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_EducacionFormal.Rows[i];

            ID_INFO_ACADEMICA = Convert.ToDecimal(GridView_EducacionFormal.DataKeys[i].Values["ID_INFO_ACADEMICA"]);

            DropDownList dropnivAcademico = filaGrilla.FindControl("DropDownList_NivAcademico") as DropDownList;
            NIVEL_ACADEMICO = dropnivAcademico.SelectedValue;

            TextBox textoInstitucion = filaGrilla.FindControl("TextBox_Institucion") as TextBox;
            INSTITUCION = textoInstitucion.Text;

            TextBox textoAnno = filaGrilla.FindControl("TextBox_Anno") as TextBox;
            if (String.IsNullOrEmpty(textoAnno.Text) == false)
            {
                ANNO = Convert.ToInt32(textoAnno.Text).ToString();
            }
            else
            {
                ANNO = "";
            }

            TextBox textoObservaciones = filaGrilla.FindControl("TextBox_Observaciones") as TextBox;
            OBSERVACIONES = textoObservaciones.Text;

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_INFO_ACADEMICA"] = ID_INFO_ACADEMICA;
            filaTablaResultado["NIVEL_ACADEMICO"] = NIVEL_ACADEMICO;
            filaTablaResultado["INSTITUCION"] = INSTITUCION;
            filaTablaResultado["ANNO"] = ANNO;
            filaTablaResultado["OBSERVACIONES"] = OBSERVACIONES;
            filaTablaResultado["ACTIVO"] = ACTIVO;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }

    private void cargar_DropDownList_NIV_EDUCACION(DropDownList drop)
    {
        drop.Items.Clear();

        parametro _parametro = new parametro(Session["idEmpresa"].ToString());

        DataTable tablaParametros = _parametro.ObtenerNivEstudiosParametros();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        drop.Items.Add(item);

        foreach (DataRow fila in tablaParametros.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["DESCRIPCION"].ToString(), fila["CODIGO"].ToString());
            drop.Items.Add(item);
        }

        drop.DataBind();
    }

    private void Cargar_Grilla_informacionEducativaFormal_Desdetabla(DataTable tablainformacion)
    {
        GridView_EducacionFormal.DataSource = tablainformacion;
        GridView_EducacionFormal.DataBind();

        for (int i = 0; i < GridView_EducacionFormal.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_EducacionFormal.Rows[i];
            DataRow filaTabla = tablainformacion.Rows[i];

            DropDownList droptipoNivEducacion = filaGrilla.FindControl("DropDownList_NivAcademico") as DropDownList;
            cargar_DropDownList_NIV_EDUCACION(droptipoNivEducacion);
            droptipoNivEducacion.SelectedValue = filaTabla["NIVEL_ACADEMICO"].ToString().Trim();

            TextBox textoInstitucion = filaGrilla.FindControl("TextBox_Institucion") as TextBox;
            textoInstitucion.Text = filaTabla["INSTITUCION"].ToString().Trim();

            TextBox textoAnno = filaGrilla.FindControl("TextBox_Anno") as TextBox;
            textoAnno.Text = filaTabla["ANNO"].ToString().Trim();

            TextBox textoobservaciones = filaGrilla.FindControl("TextBox_Observaciones") as TextBox;
            textoobservaciones.Text = filaTabla["OBSERVACIONES"].ToString().Trim();
        }

        SeleccionarDropNivelEscolaridad();
    }

    private void inhabilitarFilasGrilla(GridView grilla, int colInicio)
    {
        for (int i = 0; i < grilla.Rows.Count; i++)
        {
            for (int j = colInicio; j < grilla.Columns.Count; j++)
            {
                grilla.Rows[i].Cells[j].Enabled = false;
            }
        }
    }

    private void ActivarFilaGrilla(GridView grilla, int fila, int colInicio)
    {
        for (int i = 0; i < grilla.Columns.Count; i++)
        {
            grilla.Rows[fila].Cells[i].Enabled = true;
        }
    }

    protected void Button_NuevoEF_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerTablaDeGrillaEducacionFormal();
        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_INFO_ACADEMICA"] = 0;
        filaNueva["NIVEL_ACADEMICO"] = "";
        filaNueva["INSTITUCION"] = "";
        filaNueva["ANNO"] = "";
        filaNueva["OBSERVACIONES"] = "";
        filaNueva["ACTIVO"] = "True";

        tablaDesdeGrilla.Rows.Add(filaNueva);

        Cargar_Grilla_informacionEducativaFormal_Desdetabla(tablaDesdeGrilla);

        inhabilitarFilasGrilla(GridView_EducacionFormal, 2);
        ActivarFilaGrilla(GridView_EducacionFormal, GridView_EducacionFormal.Rows.Count - 1, 2);

        HiddenField_ACCION_GRILLA_INFOACADEMMICA_EF.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_SELECCIONADA_GRILLA_INFOACADEMICA_EF.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();
        HiddenField_ID_INFO_ACADEMICA_EF.Value = null;
        HiddenField_INSTITUCION_EF.Value = null;
        HiddenField_ANNO_EF.Value = null;
        HiddenField_OBSERVACIONES_EF.Value = null;

        GridView_EducacionFormal.Columns[0].Visible = false;
        GridView_EducacionFormal.Columns[1].Visible = false;

        Button_NuevoEF.Visible = false;
        Button_GuardarEF.Visible = true;
        Button_CancelarEF.Visible = true;
    }

    private void inhabilitarFilaGrilla(GridView grilla, int fila, int colInicio)
    {
        for (int j = colInicio; j < grilla.Columns.Count; j++)
        {
            grilla.Rows[fila].Cells[j].Enabled = false;
        }
    }

    private void SeleccionarDropNivelEscolaridad()
    {
        String ID_NIVEL = "0";

        foreach (GridViewRow filaGrilla in GridView_EducacionFormal.Rows)
        {
            DropDownList dropNivelEscolaridad = filaGrilla.FindControl("DropDownList_NivAcademico") as DropDownList;

            try
            {
                if (Convert.ToInt32(ID_NIVEL) < Convert.ToInt32(dropNivelEscolaridad.SelectedValue))
                {
                    ID_NIVEL = dropNivelEscolaridad.SelectedValue;
                }
            }
            catch
            { 
            }
        }

        try
        {
            DropDownList_NIV_EDUCACION.SelectedValue = ID_NIVEL;
        }
        catch
        {
            DropDownList_NIV_EDUCACION.ClearSelection();
        }
    }

    protected void Button_GuardarEF_Click(object sender, EventArgs e)
    {
        int FILA_SELECCIONADA = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_INFOACADEMICA_EF.Value);

        inhabilitarFilaGrilla(GridView_EducacionFormal, FILA_SELECCIONADA, 2);

        GridView_EducacionFormal.Columns[0].Visible = true;
        GridView_EducacionFormal.Columns[1].Visible = true;

        Button_GuardarEF.Visible = false;
        Button_CancelarEF.Visible = false;
        Button_NuevoEF.Visible = true;

        HiddenField_ACCION_GRILLA_INFOACADEMMICA_EF.Value = AccionesGrilla.Ninguna.ToString();

        SeleccionarDropNivelEscolaridad();
    }

    protected void Button_CancelarEF_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = obtenerTablaDeGrillaEducacionFormal();

        if (HiddenField_ACCION_GRILLA_INFOACADEMMICA_EF.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }
        else
        {
            if (HiddenField_ACCION_GRILLA_INFOACADEMMICA_EF.Value == AccionesGrilla.Modificar.ToString())
            {
                DataRow filaGrilla = tablaGrilla.Rows[Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_INFOACADEMICA_EF.Value)];

                filaGrilla["ID_INFO_ACADEMICA"] = HiddenField_ID_INFO_ACADEMICA_EF.Value;
                filaGrilla["NIVEL_ACADEMICO"] = HiddenField_NIV_ACADEMICO_EF.Value;
                filaGrilla["INSTITUCION"] = HiddenField_INSTITUCION_EF.Value;
                filaGrilla["ANNO"] = HiddenField_ANNO_EF.Value;
                filaGrilla["OBSERVACIONES"] = HiddenField_OBSERVACIONES_EF.Value;
                filaGrilla["ACTIVO"] = "True";
            }
        }

        Cargar_Grilla_informacionEducativaFormal_Desdetabla(tablaGrilla);

        inhabilitarFilasGrilla(GridView_EducacionFormal, 2);

        GridView_EducacionFormal.Columns[0].Visible = true;
        GridView_EducacionFormal.Columns[1].Visible = true;

        HiddenField_ACCION_GRILLA_INFOACADEMMICA_EF.Value = AccionesGrilla.Ninguna.ToString();

        Button_NuevoEF.Visible = true;
        Button_GuardarEF.Visible = false;
        Button_CancelarEF.Visible = false;

        SeleccionarDropNivelEscolaridad();
    }

    private DataTable ConfigurarTablaParaGrillaEducacionNoFormal()
    {
        DataTable tablaResultado = new DataTable();

        tablaResultado.Columns.Add("ID_INFO_ACADEMICA");
        tablaResultado.Columns.Add("CURSO");
        tablaResultado.Columns.Add("INSTITUCION");
        tablaResultado.Columns.Add("DURACION");
        tablaResultado.Columns.Add("UNIDAD_DURACION");
        tablaResultado.Columns.Add("OBSERVACIONES");
        tablaResultado.Columns.Add("ACTIVO");

        return tablaResultado;
    }

    private DataTable obtenerTablaDeGrillaEducacionNoFormal()
    {
        DataTable tablaResultado = ConfigurarTablaParaGrillaEducacionNoFormal();

        DataRow filaTablaResultado;

        Decimal ID_INFO_ACADEMICA;
        String CURSO;
        String INSTITUCION;
        String DURACION;
        String UNIDAD_DURACION;
        String OBSERVACIONES;
        Boolean ACTIVO;

        for (int i = 0; i < GridView_EducacionNoFormal.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_EducacionNoFormal.Rows[i];

            ID_INFO_ACADEMICA = Convert.ToDecimal(GridView_EducacionNoFormal.DataKeys[i].Values["ID_INFO_ACADEMICA"]);

            TextBox textoCurso = filaGrilla.FindControl("TextBox_Curso") as TextBox;
            CURSO = textoCurso.Text;

            TextBox textoInstitucion = filaGrilla.FindControl("TextBox_Institucion") as TextBox;
            INSTITUCION = textoInstitucion.Text;

            TextBox textoDuracion = filaGrilla.FindControl("TextBox_Duracion") as TextBox;
            if (String.IsNullOrEmpty(textoDuracion.Text) == false)
            {
                DURACION = Convert.ToDecimal(textoDuracion.Text).ToString();
            }
            else
            {
                DURACION = "";
            }

            DropDownList dropUnidadDuracion = filaGrilla.FindControl("DropDownList_UnidadDuracion") as DropDownList;
            UNIDAD_DURACION = dropUnidadDuracion.SelectedValue;

            TextBox textoobservaciones = filaGrilla.FindControl("TextBox_Observaciones") as TextBox;
            OBSERVACIONES = textoobservaciones.Text;

            ACTIVO = true;

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_INFO_ACADEMICA"] = ID_INFO_ACADEMICA;
            filaTablaResultado["CURSO"] = CURSO;
            filaTablaResultado["INSTITUCION"] = INSTITUCION;
            filaTablaResultado["DURACION"] = DURACION;
            filaTablaResultado["UNIDAD_DURACION"] = UNIDAD_DURACION;
            filaTablaResultado["OBSERVACIONES"] = OBSERVACIONES;
            filaTablaResultado["ACTIVO"] = "True";

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }

    private void cargar_DropDownList_UnidadDuracion(DropDownList drop)
    {
        drop.Items.Clear();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        drop.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("HORAS", "HORAS");
        drop.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("DIAS", "DIAS");
        drop.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("SEMANAS", "SEMANAS");
        drop.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("MESES", "MESES");
        drop.Items.Add(item);

        item = new System.Web.UI.WebControls.ListItem("AÑOS", "AÑOS");
        drop.Items.Add(item);

        drop.DataBind();
    }

    private void Cargar_Grilla_informacionEducativaNoFormal_Desdetabla(DataTable tablainformacion)
    {
        GridView_EducacionNoFormal.DataSource = tablainformacion;
        GridView_EducacionNoFormal.DataBind();

        for (int i = 0; i < GridView_EducacionNoFormal.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_EducacionNoFormal.Rows[i];
            DataRow filaTabla = tablainformacion.Rows[i];

            TextBox textoCurso = filaGrilla.FindControl("TextBox_Curso") as TextBox;
            textoCurso.Text = filaTabla["CURSO"].ToString().Trim();

            TextBox textoInstitucion = filaGrilla.FindControl("TextBox_Institucion") as TextBox;
            textoInstitucion.Text = filaTabla["INSTITUCION"].ToString().Trim();

            TextBox textoDuracion = filaGrilla.FindControl("TextBox_Duracion") as TextBox;
            if (String.IsNullOrEmpty(filaTabla["DURACION"].ToString().Trim()) == false)
            {
                textoDuracion.Text = Convert.ToDecimal(filaTabla["DURACION"]).ToString();
            }
            else
            {
                textoDuracion.Text = "";
            }

            DropDownList droptipoUnidadDuracion = filaGrilla.FindControl("DropDownList_UnidadDuracion") as DropDownList;
            cargar_DropDownList_UnidadDuracion(droptipoUnidadDuracion);
            droptipoUnidadDuracion.SelectedValue = filaTabla["UNIDAD_DURACION"].ToString().Trim();

            TextBox textoobservaciones = filaGrilla.FindControl("TextBox_Observaciones") as TextBox;
            textoobservaciones.Text = filaTabla["OBSERVACIONES"].ToString().Trim();
        }
    }

    protected void Button_NuevoENF_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerTablaDeGrillaEducacionNoFormal();

        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_INFO_ACADEMICA"] = 0;
        filaNueva["CURSO"] = "";
        filaNueva["INSTITUCION"] = "";
        filaNueva["DURACION"] = "";
        filaNueva["UNIDAD_DURACION"] = "";
        filaNueva["OBSERVACIONES"] = "";
        filaNueva["ACTIVO"] = "True";

        tablaDesdeGrilla.Rows.Add(filaNueva);

        Cargar_Grilla_informacionEducativaNoFormal_Desdetabla(tablaDesdeGrilla);

        inhabilitarFilasGrilla(GridView_EducacionNoFormal, 2);
        ActivarFilaGrilla(GridView_EducacionNoFormal, GridView_EducacionNoFormal.Rows.Count - 1, 2);

        HiddenField_ACCION_GRILLA_INFOACADEMMIC_ENF.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_SELECCIONADA_GRILLA_INFOACADEMICA_ENF.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();
        HiddenField_ID_INFO_ACADEMICA_ENF.Value = null;
        HiddenField_CURSO_ENF.Value = null;
        HiddenField_DURACION_ENF.Value = null;
        HiddenField_UNIDAD_DURACION_ENF.Value = null;
        HiddenField_ACTIVO_ENF.Value = null;

        GridView_EducacionNoFormal.Columns[0].Visible = false;
        GridView_EducacionNoFormal.Columns[1].Visible = false;

        Button_NuevoENF.Visible = false;
        Button_GuardarENF.Visible = true;
        Button_CancelarENF.Visible = true;
    }
    protected void Button_GuardarENF_Click(object sender, EventArgs e)
    {
        int FILA_SELECCIONADA = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_INFOACADEMICA_ENF.Value);

        inhabilitarFilaGrilla(GridView_EducacionNoFormal, FILA_SELECCIONADA, 2);

        GridView_EducacionNoFormal.Columns[0].Visible = true;
        GridView_EducacionNoFormal.Columns[1].Visible = true;

        Button_GuardarENF.Visible = false;
        Button_CancelarENF.Visible = false;
        Button_NuevoENF.Visible = true;

        HiddenField_ACCION_GRILLA_INFOACADEMMIC_ENF.Value = AccionesGrilla.Ninguna.ToString();
    }
    protected void Button_CancelarENF_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = obtenerTablaDeGrillaEducacionNoFormal();

        if (HiddenField_ACCION_GRILLA_INFOACADEMMIC_ENF.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }
        else
        {
            if (HiddenField_ACCION_GRILLA_INFOACADEMMIC_ENF.Value == AccionesGrilla.Modificar.ToString())
            {
                DataRow filaGrilla = tablaGrilla.Rows[Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_INFOACADEMICA_ENF.Value)];

                filaGrilla["ID_INFO_ACADEMICA"] = HiddenField_ID_INFO_ACADEMICA_ENF.Value;
                filaGrilla["CURSO"] = HiddenField_CURSO_ENF.Value;
                filaGrilla["INSTITUCION"] = HiddenField_INSTITUCION_ENF.Value;
                filaGrilla["DURACION"] = HiddenField_DURACION_ENF.Value;
                filaGrilla["UNIDAD_DURACION"] = HiddenField_UNIDAD_DURACION_ENF.Value;
                filaGrilla["OBSERVACIONES"] = HiddenField_OBSERVACIONES_ENF.Value;
                filaGrilla["ACTIVO"] = "True";
            }
        }

        Cargar_Grilla_informacionEducativaNoFormal_Desdetabla(tablaGrilla);

        inhabilitarFilasGrilla(GridView_EducacionNoFormal, 2);

        GridView_EducacionNoFormal.Columns[0].Visible = true;
        GridView_EducacionNoFormal.Columns[1].Visible = true;

        HiddenField_ACCION_GRILLA_INFOACADEMMIC_ENF.Value = AccionesGrilla.Ninguna.ToString();

        Button_NuevoENF.Visible = true;
        Button_GuardarENF.Visible = false;
        Button_CancelarENF.Visible = false;
    }

    private void cargar_DropDownList_ID_OCUPACION_FILTRADO()
    {
        DropDownList_ID_OCUPACION.Items.Clear();

        ListItem item = new ListItem("Seleccione...", "");
        DropDownList_ID_OCUPACION.Items.Add(item);

        cargo _cargo = new cargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        String NOMBRE_OCUPACION_A_BUSCAR = TextBox_BUSCADOR_CARGO.Text.ToUpper().Trim();

        DataTable tablaCargosEncontrados = _cargo.ObtenerRecOcupacionesPorNomOcupacion(NOMBRE_OCUPACION_A_BUSCAR);

        foreach (DataRow fila in tablaCargosEncontrados.Rows)
        {
            item = new ListItem(fila["NOM_OCUPACION"].ToString(), fila["ID_OCUPACION"].ToString());
            DropDownList_ID_OCUPACION.Items.Add(item);
        }
        DropDownList_ID_OCUPACION.DataBind();
    }

    protected void ImageButton_BUSCADOR_CARGO_Click(object sender, System.Web.UI.ImageClickEventArgs e)
    {
        cargar_DropDownList_ID_OCUPACION_FILTRADO();
    }
    protected void GridView_ExperienciaLaboral_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "modificar")
        {
            HiddenField_FILA_SELECCIONADA_GRILLA_EXPLABORAL.Value = indexSeleccionado.ToString();

            ActivarFilaGrilla(GridView_ExperienciaLaboral, indexSeleccionado, 2);

            GridViewRow filaGrilla = GridView_ExperienciaLaboral.Rows[indexSeleccionado];

            HiddenField_ACCION_GRILLA_EXPLABORAL.Value = AccionesGrilla.Modificar.ToString();
            HiddenField_FILA_SELECCIONADA_GRILLA_EXPLABORAL.Value = indexSeleccionado.ToString();


            HiddenField_ID_EXPERIENCIA.Value = GridView_ExperienciaLaboral.DataKeys[indexSeleccionado].Values["ID_EXPERIENCIA"].ToString();

            TextBox textoEmpresa = filaGrilla.FindControl("TextBox_Empresa") as TextBox;
            HiddenField_EMPRESA_EL.Value = textoEmpresa.Text;

            TextBox textoCargo = filaGrilla.FindControl("TextBox_Cargo") as TextBox;
            HiddenField_CARGO_EL.Value = textoCargo.Text;

            TextBox textoFunciones = filaGrilla.FindControl("TextBox_FuncionesRealizadas") as TextBox;
            HiddenField_FUNCIONES_EL.Value = textoFunciones.Text;

            TextBox textoFechaIngreso = filaGrilla.FindControl("TextBox_FechaIngreso") as TextBox;
            HiddenField_FECHA_INGRESO_EL.Value = textoFechaIngreso.Text;

            TextBox textoFechaRetiro = filaGrilla.FindControl("TextBox_FechaRetiro") as TextBox;
            HiddenField_FECHA_RETIRO_EL.Value = textoFechaRetiro.Text;

            DropDownList dropMotivoRetiro = filaGrilla.FindControl("DropDownList_MotivoRetiro") as DropDownList;
            HiddenField_MOTIVO_RETIRO_EL.Value = dropMotivoRetiro.SelectedValue;

            TextBox textoUltimoSalario = filaGrilla.FindControl("TextBox_Ultimosalario") as TextBox;
            HiddenField_ULTIMO_SALARIO_EL.Value = textoUltimoSalario.Text;

            HiddenField_ACTIVO_EL.Value = "True";

            GridView_ExperienciaLaboral.Columns[0].Visible = false;
            GridView_ExperienciaLaboral.Columns[1].Visible = false;

            Button_NuevoEmpleo.Visible = false;
            Button_GuardarEmpleo.Visible = true;
            Button_CancelarEmpleo.Visible = true;
        }
        else
        {
            if (e.CommandName == "eliminar")
            {
                DataTable tablaDesdeGrilla = obtenerTablaDeGrillaExperienciaLaboral();

                tablaDesdeGrilla.Rows.RemoveAt(indexSeleccionado);

                Cargar_Grilla_ExperienciaLaboral_Desdetabla(tablaDesdeGrilla);

                inhabilitarFilasGrilla(GridView_ExperienciaLaboral, 2);

                GridView_ExperienciaLaboral.Columns[0].Visible = true;
                GridView_ExperienciaLaboral.Columns[1].Visible = true;

                HiddenField_ACCION_GRILLA_EXPLABORAL.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_FILA_SELECCIONADA_GRILLA_EXPLABORAL.Value = null;

                Button_NuevoEmpleo.Visible = true;
                Button_GuardarEmpleo.Visible = false;
                Button_CancelarEmpleo.Visible = false;
            }
        }
    }
    protected void TextBox_FechaIngreso_TextChanged(object sender, EventArgs e)
    {
        Int32 indexSeleccionado = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_EXPLABORAL.Value);

        TextBox textoFechaIngreso = GridView_ExperienciaLaboral.Rows[indexSeleccionado].FindControl("TextBox_FechaIngreso") as TextBox;
        TextBox textoFechaRetiro = GridView_ExperienciaLaboral.Rows[indexSeleccionado].FindControl("TextBox_FechaRetiro") as TextBox;
        Label labelTiempoTrabajado = GridView_ExperienciaLaboral.Rows[indexSeleccionado].FindControl("Label_TiempoTrabajado") as Label;

        Boolean correcto = true;
        DateTime fechaIngreso;
        DateTime fechaRetiro;
        try
        {
            fechaIngreso = Convert.ToDateTime(textoFechaIngreso.Text);
        }
        catch
        {
            correcto = false;
            fechaIngreso = new DateTime();
        }

        if (correcto == true)
        {
            Boolean conContratoVigente = true;
            try
            {
                fechaRetiro = Convert.ToDateTime(textoFechaRetiro.Text);
                conContratoVigente = false;
            }
            catch
            {
                conContratoVigente = true;
                fechaRetiro = Convert.ToDateTime(DateTime.Now.ToShortDateString());
            }

            if (fechaRetiro < fechaIngreso)
            {
                labelTiempoTrabajado.Text = "Error en fechas.";
            }
            else
            {
                tools _tools = new tools();

                if (conContratoVigente == true)
                {
                    labelTiempoTrabajado.Text = "Lleva trabajando: " + _tools.DiferenciaFechas(fechaRetiro, fechaIngreso);
                }
                else
                {
                    labelTiempoTrabajado.Text = "Trabajó: " + _tools.DiferenciaFechas(fechaRetiro, fechaIngreso);
                }
            }
        }
        else
        {
            labelTiempoTrabajado.Text = "Desconocido.";
        }
    }

    private DataTable ConfigurarTablaParaGrillaExperienciaLaboral()
    {
        DataTable tablaResultado = new DataTable();

        tablaResultado.Columns.Add("ID_EXPERIENCIA");
        tablaResultado.Columns.Add("EMPRESA");
        tablaResultado.Columns.Add("CARGO");
        tablaResultado.Columns.Add("FUNCIONES");
        tablaResultado.Columns.Add("FECHA_INGRESO");
        tablaResultado.Columns.Add("FECHA_RETIRO");
        tablaResultado.Columns.Add("MOTIVO_RETIRO");
        tablaResultado.Columns.Add("ULTIMO_SALARIO");
        tablaResultado.Columns.Add("ACTIVO");

        return tablaResultado;
    }

    private DataTable obtenerTablaDeGrillaExperienciaLaboral()
    {
        DataTable tablaResultado = ConfigurarTablaParaGrillaExperienciaLaboral();

        DataRow filaTablaResultado;

        Decimal ID_EXPERIENCIA;
        String EMPRESA;
        String CARGO;
        String FUNCIONES;
        String FECHA_INGRESO;
        String FECHA_RETIRO;
        String MOTIVO_RETIRO;
        String ULTIMO_SALARIO;
        Boolean ACTIVO;

        for (int i = 0; i < GridView_ExperienciaLaboral.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ExperienciaLaboral.Rows[i];

            ID_EXPERIENCIA = Convert.ToDecimal(GridView_ExperienciaLaboral.DataKeys[i].Values["ID_EXPERIENCIA"]);

            TextBox textoEmpresa = filaGrilla.FindControl("TextBox_Empresa") as TextBox;
            EMPRESA = textoEmpresa.Text;

            TextBox textoCargo = filaGrilla.FindControl("TextBox_Cargo") as TextBox;
            CARGO = textoCargo.Text;

            TextBox textoFunciones = filaGrilla.FindControl("TextBox_FuncionesRealizadas") as TextBox;
            FUNCIONES = textoFunciones.Text;

            TextBox textoFechaIngreso = filaGrilla.FindControl("TextBox_FechaIngreso") as TextBox;
            FECHA_INGRESO = textoFechaIngreso.Text;

            TextBox textoFechaRetiro = filaGrilla.FindControl("TextBox_FechaRetiro") as TextBox;
            FECHA_RETIRO = textoFechaRetiro.Text;

            DropDownList dropMotivoRetiro = filaGrilla.FindControl("DropDownList_MotivoRetiro") as DropDownList;
            MOTIVO_RETIRO = dropMotivoRetiro.Text;

            TextBox textoUltimoSalario = filaGrilla.FindControl("TextBox_Ultimosalario") as TextBox;
            ULTIMO_SALARIO = textoUltimoSalario.Text;

            ACTIVO = true;

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_EXPERIENCIA"] = ID_EXPERIENCIA;
            filaTablaResultado["EMPRESA"] = EMPRESA;
            filaTablaResultado["CARGO"] = CARGO;
            filaTablaResultado["FUNCIONES"] = FUNCIONES;
            filaTablaResultado["FECHA_INGRESO"] = FECHA_INGRESO;
            filaTablaResultado["FECHA_RETIRO"] = FECHA_RETIRO;
            filaTablaResultado["MOTIVO_RETIRO"] = MOTIVO_RETIRO;
            filaTablaResultado["ULTIMO_SALARIO"] = ULTIMO_SALARIO;
            filaTablaResultado["ACTIVO"] = "True";

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }

    protected void Button_NuevoEmpleo_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerTablaDeGrillaExperienciaLaboral();

        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_EXPERIENCIA"] = 0;
        filaNueva["EMPRESA"] = "";
        filaNueva["CARGO"] = "";
        filaNueva["FUNCIONES"] = "";
        filaNueva["FECHA_INGRESO"] = "";
        filaNueva["FECHA_RETIRO"] = "";
        filaNueva["MOTIVO_RETIRO"] = "";
        filaNueva["ULTIMO_SALARIO"] = "";
        filaNueva["ACTIVO"] = "True";

        tablaDesdeGrilla.Rows.Add(filaNueva);

        Cargar_Grilla_ExperienciaLaboral_Desdetabla(tablaDesdeGrilla);

        inhabilitarFilasGrilla(GridView_ExperienciaLaboral, 2);
        ActivarFilaGrilla(GridView_ExperienciaLaboral, GridView_ExperienciaLaboral.Rows.Count - 1, 2);

        HiddenField_ACCION_GRILLA_EXPLABORAL.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_SELECCIONADA_GRILLA_EXPLABORAL.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();

        GridView_ExperienciaLaboral.Columns[0].Visible = false;
        GridView_ExperienciaLaboral.Columns[1].Visible = false;

        Button_NuevoEmpleo.Visible = false;
        Button_GuardarEmpleo.Visible = true;
        Button_CancelarEmpleo.Visible = true;
    }
    protected void Button_GuardarEmpleo_Click(object sender, EventArgs e)
    {
        int FILA_SELECCIONADA = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_EXPLABORAL.Value);

        inhabilitarFilaGrilla(GridView_ExperienciaLaboral, FILA_SELECCIONADA, 2);

        GridView_ExperienciaLaboral.Columns[0].Visible = true;
        GridView_ExperienciaLaboral.Columns[1].Visible = true;

        Button_GuardarEmpleo.Visible = false;
        Button_CancelarEmpleo.Visible = false;
        Button_NuevoEmpleo.Visible = true;

        HiddenField_ACCION_GRILLA_EXPLABORAL.Value = AccionesGrilla.Ninguna.ToString();
    }

    private void Cargar_Grilla_ExperienciaLaboral_Desdetabla(DataTable tablainformacion)
    {
        GridView_ExperienciaLaboral.DataSource = tablainformacion;
        GridView_ExperienciaLaboral.DataBind();

        for (int i = 0; i < GridView_ExperienciaLaboral.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ExperienciaLaboral.Rows[i];
            DataRow filaTabla = tablainformacion.Rows[i];

            TextBox textoEmpresa = filaGrilla.FindControl("TextBox_Empresa") as TextBox;
            textoEmpresa.Text = filaTabla["EMPRESA"].ToString().Trim();

            TextBox textoInstitucion = filaGrilla.FindControl("TextBox_Cargo") as TextBox;
            textoInstitucion.Text = filaTabla["CARGO"].ToString().Trim();

            TextBox textoFunciones = filaGrilla.FindControl("TextBox_FuncionesRealizadas") as TextBox;
            textoFunciones.Text = filaTabla["FUNCIONES"].ToString().Trim();

            TextBox textoFechaInicio = filaGrilla.FindControl("TextBox_FechaIngreso") as TextBox;
            if (String.IsNullOrEmpty(filaTabla["FECHA_INGRESO"].ToString().Trim()) == false)
            {
                textoFechaInicio.Text = Convert.ToDateTime(filaTabla["FECHA_INGRESO"]).ToShortDateString();
            }
            else
            {
                textoFechaInicio.Text = "";
            }

            TextBox textoFechaRetiro = filaGrilla.FindControl("TextBox_FechaRetiro") as TextBox;
            if (String.IsNullOrEmpty(filaTabla["FECHA_RETIRO"].ToString().Trim()) == false)
            {
                textoFechaRetiro.Text = Convert.ToDateTime(filaTabla["FECHA_RETIRO"]).ToShortDateString();
            }
            else
            {
                textoFechaRetiro.Text = "";
            }

            Label labelTiempoTrabajado = filaGrilla.FindControl("Label_TiempoTrabajado") as Label;

            Boolean correcto = true;
            DateTime fechaIngreso;
            DateTime fechaRetiro;
            try
            {
                fechaIngreso = Convert.ToDateTime(textoFechaInicio.Text);
            }
            catch
            {
                correcto = false;
                fechaIngreso = new DateTime();
            }

            if (correcto == true)
            {
                Boolean conContratoVigente = true;
                try
                {
                    fechaRetiro = Convert.ToDateTime(textoFechaRetiro.Text);
                    conContratoVigente = false;
                }
                catch
                {
                    conContratoVigente = true;
                    fechaRetiro = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                }

                if (fechaRetiro < fechaIngreso)
                {
                    labelTiempoTrabajado.Text = "Error en fechas.";
                }
                else
                {
                    tools _tools = new tools();

                    if (conContratoVigente == true)
                    {
                        labelTiempoTrabajado.Text = "Lleva trabajando: " + _tools.DiferenciaFechas(fechaRetiro, fechaIngreso);
                    }
                    else
                    {
                        labelTiempoTrabajado.Text = "Trabajó: " + _tools.DiferenciaFechas(fechaRetiro, fechaIngreso);
                    }
                }
            }
            else
            {
                labelTiempoTrabajado.Text = "Tiempo Desconocido.";
            }



            DropDownList dropMotivoRetiro = filaGrilla.FindControl("DropDownList_MotivoRetiro") as DropDownList;
            try
            {
                dropMotivoRetiro.SelectedValue = filaTabla["MOTIVO_RETIRO"].ToString().Trim();
            }
            catch
            {
                dropMotivoRetiro.SelectedIndex = 0;
            }


            TextBox textoUltimoSalario = filaGrilla.FindControl("TextBox_Ultimosalario") as TextBox;
            textoUltimoSalario.Text = filaTabla["ULTIMO_SALARIO"].ToString();
        }
    }

    protected void Button_CancelarEmpleo_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = obtenerTablaDeGrillaExperienciaLaboral();

        if (HiddenField_ACCION_GRILLA_EXPLABORAL.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }
        else
        {
            if (HiddenField_ACCION_GRILLA_EXPLABORAL.Value == AccionesGrilla.Modificar.ToString())
            {
                DataRow filaGrilla = tablaGrilla.Rows[Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_EXPLABORAL.Value)];

                filaGrilla["ID_EXPERIENCIA"] = HiddenField_ID_EXPERIENCIA.Value;
                filaGrilla["EMPRESA"] = HiddenField_EMPRESA_EL.Value;
                filaGrilla["CARGO"] = HiddenField_CARGO_EL.Value;
                filaGrilla["FUNCIONES"] = HiddenField_FUNCIONES_EL.Value;
                filaGrilla["FECHA_INGRESO"] = HiddenField_FECHA_INGRESO_EL.Value;
                filaGrilla["FECHA_RETIRO"] = HiddenField_FECHA_RETIRO_EL.Value;
                filaGrilla["MOTIVO_RETIRO"] = HiddenField_MOTIVO_RETIRO_EL.Value;
                filaGrilla["ULTIMO_SALARIO"] = HiddenField_ULTIMO_SALARIO_EL.Value;
                filaGrilla["ACTIVO"] = "True";
            }
        }

        Cargar_Grilla_ExperienciaLaboral_Desdetabla(tablaGrilla);

        inhabilitarFilasGrilla(GridView_ExperienciaLaboral, 2);

        GridView_ExperienciaLaboral.Columns[0].Visible = true;
        GridView_ExperienciaLaboral.Columns[1].Visible = true;

        HiddenField_ACCION_GRILLA_EXPLABORAL.Value = AccionesGrilla.Ninguna.ToString();

        Button_NuevoEmpleo.Visible = true;
        Button_GuardarEmpleo.Visible = false;
        Button_CancelarEmpleo.Visible = false;
    }
    protected void GridView_ComposicionFamiliar_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "modificar")
        {
            HiddenField_FILA_SELECCIONADA_GRILLA_COMPOSIIONFAMILIAR.Value = indexSeleccionado.ToString();

            GridViewRow filaGrilla = GridView_ComposicionFamiliar.Rows[indexSeleccionado];

            ActivarFilaGrilla(GridView_ComposicionFamiliar, indexSeleccionado, 2);

            HiddenField_ACCION_GRILLA_COMPOSICIONFAMILIAR.Value = AccionesGrilla.Modificar.ToString();
            HiddenField_FILA_SELECCIONADA_GRILLA_COMPOSIIONFAMILIAR.Value = indexSeleccionado.ToString();

            HiddenField_ID_COMPOSICION.Value = GridView_ComposicionFamiliar.DataKeys[indexSeleccionado].Values["ID_COMPOSICION"].ToString();

            DropDownList dropTipoFamiliar = filaGrilla.FindControl("DropDownList_TipoFamiliar") as DropDownList;
            HiddenField_ID_TIPO_FAMILIAR.Value = dropTipoFamiliar.SelectedValue;

            TextBox textoNombres = filaGrilla.FindControl("TextBox_NombresFamiliar") as TextBox;
            HiddenField_NOMBRES.Value = textoNombres.Text;

            TextBox textoApellidos = filaGrilla.FindControl("TextBox_ApellidosFamiliar") as TextBox;
            HiddenField_APELLIDOS.Value = textoApellidos.Text;

            TextBox textoFechaNacimiento = filaGrilla.FindControl("TextBox_FechaNacimientoFamiliar") as TextBox;
            try
            {
                HiddenField_FECHA_NACIMIENTO.Value = Convert.ToDateTime(textoFechaNacimiento.Text).ToShortDateString();
            }
            catch
            {
                HiddenField_FECHA_NACIMIENTO.Value = "";
            }

            TextBox textoProfesion = filaGrilla.FindControl("TextBox_ProfesionFamiliar") as TextBox;
            HiddenField_PROFESION.Value = textoProfesion.Text;

            CheckBox checkExtranjero = filaGrilla.FindControl("CheckBox_Extranjero") as CheckBox;
            DropDownList dropCiudad = filaGrilla.FindControl("DropDownList_CiudadFamiliar") as DropDownList;
            if (checkExtranjero.Checked == true)
            {
                HiddenField_ID_CIUDAD.Value = "EXTRA";
            }
            else
            {
                HiddenField_ID_CIUDAD.Value = dropCiudad.SelectedValue;
            }

            CheckBox checkViveConEl = filaGrilla.FindControl("CheckBox_ViveConElFamiliar") as CheckBox;
            if (checkViveConEl.Checked == true)
            {
                HiddenField_VIVE_CON_EL.Value = "True";
            }
            else
            {
                HiddenField_VIVE_CON_EL.Value = "False";
            }

            HiddenField_ACTIVO.Value = "True";

            GridView_ComposicionFamiliar.Columns[0].Visible = false;
            GridView_ComposicionFamiliar.Columns[1].Visible = false;

            Button_NUEVA_COMPOSICIONFAMILIAR.Visible = false;
            Button_GUARDAR_COMPOSICIONFAMILIAR.Visible = true;
            Button_CANCELAR_COMPOSICIONFAMILIAR.Visible = true;
        }
        else
        {
            if (e.CommandName == "eliminar")
            {
                DataTable tablaDesdeGrilla = obtenerTablaDeGrillaFamiliares();

                tablaDesdeGrilla.Rows.RemoveAt(indexSeleccionado);

                Cargar_Grilla_Composicionfamiliar_Desdetabla(tablaDesdeGrilla);

                inhabilitarFilasGrilla(GridView_ComposicionFamiliar, 2);

                GridView_ComposicionFamiliar.Columns[0].Visible = true;
                GridView_ComposicionFamiliar.Columns[1].Visible = true;

                HiddenField_ACCION_GRILLA_COMPOSICIONFAMILIAR.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_FILA_SELECCIONADA_GRILLA_COMPOSIIONFAMILIAR.Value = null;

                Button_NUEVA_COMPOSICIONFAMILIAR.Visible = true;
                Button_GUARDAR_COMPOSICIONFAMILIAR.Visible = false;
                Button_CANCELAR_COMPOSICIONFAMILIAR.Visible = false;
            }
        }

        ObtenerNumeroDeHijos();
    }
    protected void TextBox_FechaNacimientoFamiliar_TextChanged(object sender, EventArgs e)
    {
        tools _tools = new tools();

        Int32 indexSeleccionado = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_COMPOSIIONFAMILIAR.Value);

        TextBox textoFechaNacimiento = GridView_ComposicionFamiliar.Rows[indexSeleccionado].FindControl("TextBox_FechaNacimientoFamiliar") as TextBox;
        Label LabelEdad = GridView_ComposicionFamiliar.Rows[indexSeleccionado].FindControl("Label_Edad") as Label;

        if (textoFechaNacimiento.Text.Length > 0)
        {
            try
            {
                LabelEdad.Text = "Edad: " + _tools.ObtenerEdadDesdeFechaNacimiento(Convert.ToDateTime(textoFechaNacimiento.Text)).ToString();
            }
            catch
            {
                LabelEdad.Text = "Edad: Desconocida.";
            }
        }
        else
        {
            LabelEdad.Text = "Edad: Desconocida.";
        }
    }

    protected void CheckBox_Extranjero_CheckedChanged(object sender, EventArgs e)
    {
        Int32 indexSeleccionado = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_COMPOSIIONFAMILIAR.Value);

        CheckBox checkExtranjero = GridView_ComposicionFamiliar.Rows[indexSeleccionado].FindControl("CheckBox_Extranjero") as CheckBox;
        Panel panelViveEn = GridView_ComposicionFamiliar.Rows[indexSeleccionado].FindControl("Panel_ViveEn") as Panel;

        if (checkExtranjero.Checked == true)
        {
            panelViveEn.Visible = false;
        }
        else
        {
            panelViveEn.Visible = true;
        }
    }

    protected void DropDownList_DepartamentoFamiliar_SelectedIndexChanged(object sender, EventArgs e)
    {
        DropDownList drop = (DropDownList)sender;

        Int32 indexSeleccionadoEnGrilla = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_COMPOSIIONFAMILIAR.Value);
        GridViewRow filaGrilla = GridView_ComposicionFamiliar.Rows[indexSeleccionadoEnGrilla];

        DropDownList dropCiudad = filaGrilla.FindControl("DropDownList_CiudadFamiliar") as DropDownList;

        if (drop.SelectedValue == "")
        {
            dropCiudad.Items.Clear();
        }
        else
        {
            String ID_DEPARTAMENTO = drop.SelectedValue.ToString();
            cargar_DropDownList_CIUDAD(ID_DEPARTAMENTO, dropCiudad);
        }
    }

    private DataTable ConfigurarTablaFamiliares()
    {
        DataTable tablaResultado = new DataTable();

        tablaResultado.Columns.Add("ID_COMPOSICION");
        tablaResultado.Columns.Add("ID_TIPO_FAMILIAR");
        tablaResultado.Columns.Add("NOMBRES");
        tablaResultado.Columns.Add("APELLIDOS");
        tablaResultado.Columns.Add("FECHA_NACIMIENTO");
        tablaResultado.Columns.Add("PROFESION");
        tablaResultado.Columns.Add("ID_CIUDAD");
        tablaResultado.Columns.Add("VIVE_CON_EL");
        tablaResultado.Columns.Add("ACTIVO");

        return tablaResultado;
    }

    private DataTable obtenerTablaDeGrillaFamiliares()
    {
        DataTable tablaResultado = ConfigurarTablaFamiliares();

        DataRow filaTablaResultado;

        Decimal ID_COMPOSICION;
        String ID_TIPO_FAMILIAR;
        String NOMBRES;
        String APELLIDOS;
        String FECHA_NACIMIENTO;
        String PROFESION;
        String ID_CIUDAD;
        String VIVE_CON_EL;
        Boolean ACTIVO;

        for (int i = 0; i < GridView_ComposicionFamiliar.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ComposicionFamiliar.Rows[i];

            ID_COMPOSICION = Convert.ToDecimal(GridView_ComposicionFamiliar.DataKeys[i].Values["ID_COMPOSICION"]);

            DropDownList dropTipoFamiliar = filaGrilla.FindControl("DropDownList_TipoFamiliar") as DropDownList;
            ID_TIPO_FAMILIAR = dropTipoFamiliar.SelectedValue;

            TextBox textoNombres = filaGrilla.FindControl("TextBox_NombresFamiliar") as TextBox;
            NOMBRES = textoNombres.Text;

            TextBox textoApellidos = filaGrilla.FindControl("TextBox_ApellidosFamiliar") as TextBox;
            APELLIDOS = textoApellidos.Text;

            TextBox textoFechaNacimiento = filaGrilla.FindControl("TextBox_FechaNacimientoFamiliar") as TextBox;
            try
            {
                FECHA_NACIMIENTO = Convert.ToDateTime(textoFechaNacimiento.Text).ToShortDateString();
            }
            catch
            {
                FECHA_NACIMIENTO = null;
            }

            TextBox textoProfesion = filaGrilla.FindControl("TextBox_ProfesionFamiliar") as TextBox;
            PROFESION = textoProfesion.Text;

            CheckBox checkExtranjero = filaGrilla.FindControl("CheckBox_Extranjero") as CheckBox;
            DropDownList dropCiudad = filaGrilla.FindControl("DropDownList_CiudadFamiliar") as DropDownList;
            if (checkExtranjero.Checked == true)
            {
                ID_CIUDAD = "EXTRA";
            }
            else
            {
                ID_CIUDAD = dropCiudad.SelectedValue;
            }

            CheckBox checkViveConEl = filaGrilla.FindControl("CheckBox_ViveConElFamiliar") as CheckBox;
            if (checkViveConEl.Checked == true)
            {
                VIVE_CON_EL = "True";
            }
            else
            {
                VIVE_CON_EL = "False";
            }

            ACTIVO = true;

            filaTablaResultado = tablaResultado.NewRow();

            filaTablaResultado["ID_COMPOSICION"] = ID_COMPOSICION;
            filaTablaResultado["ID_TIPO_FAMILIAR"] = ID_TIPO_FAMILIAR;
            filaTablaResultado["NOMBRES"] = NOMBRES;
            filaTablaResultado["APELLIDOS"] = APELLIDOS;
            filaTablaResultado["FECHA_NACIMIENTO"] = FECHA_NACIMIENTO;
            filaTablaResultado["PROFESION"] = PROFESION;
            filaTablaResultado["ID_CIUDAD"] = ID_CIUDAD;
            filaTablaResultado["VIVE_CON_EL"] = VIVE_CON_EL;
            filaTablaResultado["ACTIVO"] = ACTIVO;

            tablaResultado.Rows.Add(filaTablaResultado);
        }

        return tablaResultado;
    }

    private void Cargar_DropDownList_TipoFamiliar(DropDownList drop)
    {
        drop.Items.Clear();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccionae...", "");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("ABUELA", "ABUELA");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("ABUELO", "ABUELO");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("MAMA", "MAMA");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("PAPA", "PAPA");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("ESPOSA", "ESPOSA");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("ESPOSO", "ESPOSO");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("HERMANA", "HERMANA");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("HERMANO", "HERMANO");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("HIJA", "HIJA");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("HIJO", "HIJO");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("HIJASTRA", "HIJASTRA");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("HIJASTRO", "HIJASTRO");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("TIA", "TIA");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("TIO", "TIO");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("PRIMA", "PRIMA");
        drop.Items.Add(item);
        item = new System.Web.UI.WebControls.ListItem("PRIMO", "PRIMO");
        drop.Items.Add(item);

        drop.DataBind();
    }

    private void cargar_DropDownList_DEPARTAMENTO(DropDownList drop)
    {
        drop.Items.Clear();

        departamento _departamento = new departamento(Session["idEmpresa"].ToString());
        DataTable tablaDepartamentos = _departamento.ObtenerTodasLosDepartamentos();

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione...", "");
        drop.Items.Add(item);

        foreach (DataRow fila in tablaDepartamentos.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOMBRE"].ToString(), fila["ID_DEPARTAMENTO"].ToString());
            drop.Items.Add(item);
        }

        drop.DataBind();
    }

    private void cargar_DropDownList_CIUDAD(String idDepartamento, DropDownList drop)
    {
        drop.Items.Clear();

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
        DataTable tablaCiudades = _ciudad.ObtenerCiudadesPorIdDepartamento(idDepartamento);

        System.Web.UI.WebControls.ListItem item = new System.Web.UI.WebControls.ListItem("Seleccione Ciudad", "");
        drop.Items.Add(item);

        foreach (DataRow fila in tablaCiudades.Rows)
        {
            item = new System.Web.UI.WebControls.ListItem(fila["NOMBRE"].ToString(), fila["ID_CIUDAD"].ToString());
            drop.Items.Add(item);
        }

        drop.DataBind();
    }

    private void ObtenerNumeroDeHijos()
    {
        Int32 numHijosActual = 0;

        foreach (GridViewRow filaGrilla in GridView_ComposicionFamiliar.Rows)
        {
            DropDownList dropFamiliar = filaGrilla.FindControl("DropDownList_TipoFamiliar") as DropDownList;

            if ((dropFamiliar.SelectedValue == "HIJO") || (dropFamiliar.SelectedValue == "HIJA"))
            {
                numHijosActual += 1;
            }
        }

        TextBox_NUM_HIJOS.Text = numHijosActual.ToString();
    }

    private void Cargar_Grilla_Composicionfamiliar_Desdetabla(DataTable tablaComposicion)
    {
        tools _tools = new tools();

        GridView_ComposicionFamiliar.DataSource = tablaComposicion;
        GridView_ComposicionFamiliar.DataBind();

        for (int i = 0; i < GridView_ComposicionFamiliar.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ComposicionFamiliar.Rows[i];
            DataRow filaTabla = tablaComposicion.Rows[i];

            DropDownList droptipoFamiliar = filaGrilla.FindControl("DropDownList_TipoFamiliar") as DropDownList;
            Cargar_DropDownList_TipoFamiliar(droptipoFamiliar);
            droptipoFamiliar.SelectedValue = filaTabla["ID_TIPO_FAMILIAR"].ToString().Trim();

            TextBox textoNombres = filaGrilla.FindControl("TextBox_NombresFamiliar") as TextBox;
            textoNombres.Text = filaTabla["NOMBRES"].ToString().Trim();

            TextBox textoApellidos = filaGrilla.FindControl("TextBox_ApellidosFamiliar") as TextBox;
            textoApellidos.Text = filaTabla["APELLIDOS"].ToString().Trim();

            TextBox textoFechaFamiliar = filaGrilla.FindControl("TextBox_FechaNacimientoFamiliar") as TextBox;
            Label labelEdad = filaGrilla.FindControl("Label_Edad") as Label;
            if (String.IsNullOrEmpty(filaTabla["FECHA_NACIMIENTO"].ToString().Trim()) == false)
            {
                textoFechaFamiliar.Text = Convert.ToDateTime(filaTabla["FECHA_NACIMIENTO"]).ToShortDateString();

                try
                {
                    labelEdad.Text = "Edad: " + _tools.ObtenerEdadDesdeFechaNacimiento(Convert.ToDateTime(filaTabla["FECHA_NACIMIENTO"])).ToString();
                }
                catch
                {
                    labelEdad.Text = "Edad: Desconocida.";
                }
            }
            else
            {
                textoFechaFamiliar.Text = "";
                labelEdad.Text = "Edad: Desconocida.";
            }

            TextBox textoProfesion = filaGrilla.FindControl("TextBox_ProfesionFamiliar") as TextBox;
            textoProfesion.Text = filaTabla["PROFESION"].ToString().Trim();

            CheckBox checkExtranjero = filaGrilla.FindControl("CheckBox_Extranjero") as CheckBox;
            Panel panelViveEn = filaGrilla.FindControl("Panel_ViveEn") as Panel;
            DropDownList dropDepartamento = filaGrilla.FindControl("DropDownList_DepartamentoFamiliar") as DropDownList;
            DropDownList dropCiudad = filaGrilla.FindControl("DropDownList_CiudadFamiliar") as DropDownList;
            if (String.IsNullOrEmpty(filaTabla["ID_CIUDAD"].ToString().Trim()) == false)
            {
                if (filaTabla["ID_CIUDAD"].ToString().Trim() == "EXTRA")
                {
                    checkExtranjero.Checked = true;
                    panelViveEn.Visible = false;
                    cargar_DropDownList_DEPARTAMENTO(dropDepartamento);
                    dropCiudad.Items.Clear();
                }
                else
                {
                    DataRow filaInfoCiudadYDepartamento = obtenerDatosCiudadViveFamiliar(filaTabla["ID_CIUDAD"].ToString().Trim());
                    cargar_DropDownList_DEPARTAMENTO(dropDepartamento);
                    dropDepartamento.SelectedValue = filaInfoCiudadYDepartamento["ID_DEPARTAMENTO"].ToString().Trim();
                    cargar_DropDownList_CIUDAD(filaInfoCiudadYDepartamento["ID_DEPARTAMENTO"].ToString().Trim(), dropCiudad);
                    dropCiudad.SelectedValue = filaInfoCiudadYDepartamento["ID_CIUDAD"].ToString().Trim();
                }
            }
            else
            {
                checkExtranjero.Checked = false;
                panelViveEn.Visible = true;
                cargar_DropDownList_DEPARTAMENTO(dropDepartamento);
                dropCiudad.Items.Clear();
            }

            CheckBox check = filaGrilla.FindControl("CheckBox_ViveConElFamiliar") as CheckBox;
            if (filaTabla["VIVE_CON_EL"].ToString().Trim().ToUpper() == "TRUE")
            {
                check.Checked = true;
            }
            else
            {
                check.Checked = false;
            }
        }

        ObtenerNumeroDeHijos();
    }

    private DataRow obtenerDatosCiudadViveFamiliar(String idCiudad)
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

    protected void Button_NUEVA_COMPOSICIONFAMILIAR_Click(object sender, EventArgs e)
    {
        DataTable tablaDesdeGrilla = obtenerTablaDeGrillaFamiliares();

        DataRow filaNueva = tablaDesdeGrilla.NewRow();

        filaNueva["ID_COMPOSICION"] = 0;
        filaNueva["ID_TIPO_FAMILIAR"] = "";
        filaNueva["NOMBRES"] = "";
        filaNueva["APELLIDOS"] = "";
        filaNueva["FECHA_NACIMIENTO"] = "";
        filaNueva["PROFESION"] = "";
        filaNueva["ID_CIUDAD"] = "";
        filaNueva["VIVE_CON_EL"] = "";
        filaNueva["ACTIVO"] = "True";

        tablaDesdeGrilla.Rows.Add(filaNueva);

        Cargar_Grilla_Composicionfamiliar_Desdetabla(tablaDesdeGrilla);

        inhabilitarFilasGrilla(GridView_ComposicionFamiliar, 2);
        ActivarFilaGrilla(GridView_ComposicionFamiliar, GridView_ComposicionFamiliar.Rows.Count - 1, 2);

        HiddenField_ACCION_GRILLA_COMPOSICIONFAMILIAR.Value = AccionesGrilla.Nuevo.ToString();
        HiddenField_FILA_SELECCIONADA_GRILLA_COMPOSIIONFAMILIAR.Value = (tablaDesdeGrilla.Rows.Count - 1).ToString();

        GridView_ComposicionFamiliar.Columns[0].Visible = false;
        GridView_ComposicionFamiliar.Columns[1].Visible = false;

        Button_NUEVA_COMPOSICIONFAMILIAR.Visible = false;
        Button_GUARDAR_COMPOSICIONFAMILIAR.Visible = true;
        Button_CANCELAR_COMPOSICIONFAMILIAR.Visible = true;
    }

    protected void Button_GUARDAR_COMPOSICIONFAMILIAR_Click(object sender, EventArgs e)
    {
        int FILA_SELECCIONADA = Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_COMPOSIIONFAMILIAR.Value);

        inhabilitarFilaGrilla(GridView_ComposicionFamiliar, FILA_SELECCIONADA, 2);

        GridView_ComposicionFamiliar.Columns[0].Visible = true;
        GridView_ComposicionFamiliar.Columns[1].Visible = true;

        Button_GUARDAR_COMPOSICIONFAMILIAR.Visible = false;
        Button_CANCELAR_COMPOSICIONFAMILIAR.Visible = false;
        Button_NUEVA_COMPOSICIONFAMILIAR.Visible = true;

        HiddenField_ACCION_GRILLA_COMPOSICIONFAMILIAR.Value = AccionesGrilla.Ninguna.ToString();

        ObtenerNumeroDeHijos();
    }

    protected void Button_CANCELAR_COMPOSICIONFAMILIAR_Click(object sender, EventArgs e)
    {
        DataTable tablaGrilla = obtenerTablaDeGrillaFamiliares();

        if (HiddenField_ACCION_GRILLA_COMPOSICIONFAMILIAR.Value == AccionesGrilla.Nuevo.ToString())
        {
            tablaGrilla.Rows.RemoveAt(tablaGrilla.Rows.Count - 1);
        }
        else
        {
            if (HiddenField_ACCION_GRILLA_COMPOSICIONFAMILIAR.Value == AccionesGrilla.Modificar.ToString())
            {
                DataRow filaGrilla = tablaGrilla.Rows[Convert.ToInt32(HiddenField_FILA_SELECCIONADA_GRILLA_COMPOSIIONFAMILIAR.Value)];

                filaGrilla["ID_COMPOSICION"] = HiddenField_ID_COMPOSICION.Value;
                filaGrilla["ID_TIPO_FAMILIAR"] = HiddenField_ID_TIPO_FAMILIAR.Value;
                filaGrilla["NOMBRES"] = HiddenField_NOMBRES.Value;
                filaGrilla["APELLIDOS"] = HiddenField_APELLIDOS.Value;
                filaGrilla["FECHA_NACIMIENTO"] = HiddenField_FECHA_NACIMIENTO.Value;
                filaGrilla["PROFESION"] = HiddenField_PROFESION.Value;
                filaGrilla["ID_CIUDAD"] = HiddenField_ID_CIUDAD.Value;
                filaGrilla["VIVE_CON_EL"] = HiddenField_VIVE_CON_EL.Value;
                filaGrilla["ACTIVO"] = "True";
            }
        }

        Cargar_Grilla_Composicionfamiliar_Desdetabla(tablaGrilla);

        inhabilitarFilasGrilla(GridView_ComposicionFamiliar, 2);

        GridView_ComposicionFamiliar.Columns[0].Visible = true;
        GridView_ComposicionFamiliar.Columns[1].Visible = true;

        HiddenField_ACCION_GRILLA_COMPOSICIONFAMILIAR.Value = AccionesGrilla.Ninguna.ToString();

        Button_NUEVA_COMPOSICIONFAMILIAR.Visible = true;
        Button_GUARDAR_COMPOSICIONFAMILIAR.Visible = false;
        Button_CANCELAR_COMPOSICIONFAMILIAR.Visible = false;

        ObtenerNumeroDeHijos();
    }
    protected void GridView_EducacionNoFormal_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        int indexSeleccionado = Convert.ToInt32(e.CommandArgument);

        if (e.CommandName == "modificar")
        {
            HiddenField_FILA_SELECCIONADA_GRILLA_INFOACADEMICA_ENF.Value = indexSeleccionado.ToString();

            GridViewRow filaGrilla = GridView_EducacionNoFormal.Rows[indexSeleccionado];

            ActivarFilaGrilla(GridView_EducacionNoFormal, indexSeleccionado, 2);

            HiddenField_ACCION_GRILLA_INFOACADEMMIC_ENF.Value = AccionesGrilla.Modificar.ToString();
            HiddenField_FILA_SELECCIONADA_GRILLA_INFOACADEMICA_ENF.Value = indexSeleccionado.ToString();


            HiddenField_ID_INFO_ACADEMICA_ENF.Value = GridView_EducacionNoFormal.DataKeys[indexSeleccionado].Values["ID_INFO_ACADEMICA"].ToString();

            TextBox textoCurso = filaGrilla.FindControl("TextBox_Curso") as TextBox;
            HiddenField_CURSO_ENF.Value = textoCurso.Text;

            TextBox textoInstitucion = filaGrilla.FindControl("TextBox_Institucion") as TextBox;
            HiddenField_INSTITUCION_ENF.Value = textoInstitucion.Text;

            TextBox textoDuracion = filaGrilla.FindControl("TextBox_Duracion") as TextBox;
            if (String.IsNullOrEmpty(textoDuracion.Text) == false)
            {
                HiddenField_DURACION_ENF.Value = Convert.ToDecimal(textoDuracion.Text).ToString();
            }
            else
            {
                HiddenField_DURACION_ENF.Value = "";
            }

            DropDownList dropUnidadDuracion = filaGrilla.FindControl("DropDownList_UnidadDuracion") as DropDownList;
            HiddenField_UNIDAD_DURACION_ENF.Value = dropUnidadDuracion.SelectedValue;

            TextBox textoobservaciones = filaGrilla.FindControl("TextBox_Observaciones") as TextBox;
            HiddenField_OBSERVACIONES_ENF.Value = textoobservaciones.Text;

            GridView_EducacionNoFormal.Columns[0].Visible = false;
            GridView_EducacionNoFormal.Columns[1].Visible = false;

            Button_NuevoENF.Visible = false;
            Button_GuardarENF.Visible = true;
            Button_CancelarENF.Visible = true;
        }
        else
        {
            if (e.CommandName == "eliminar")
            {
                DataTable tablaDesdeGrilla = obtenerTablaDeGrillaEducacionNoFormal();

                tablaDesdeGrilla.Rows.RemoveAt(indexSeleccionado);

                Cargar_Grilla_informacionEducativaNoFormal_Desdetabla(tablaDesdeGrilla);

                inhabilitarFilasGrilla(GridView_EducacionNoFormal, 2);

                GridView_EducacionNoFormal.Columns[0].Visible = true;
                GridView_EducacionNoFormal.Columns[1].Visible = true;

                HiddenField_ACCION_GRILLA_INFOACADEMMIC_ENF.Value = AccionesGrilla.Ninguna.ToString();
                HiddenField_FILA_SELECCIONADA_GRILLA_INFOACADEMICA_ENF.Value = null;

                Button_NuevoENF.Visible = true;
                Button_GuardarENF.Visible = false;
                Button_CancelarENF.Visible = false;
            }
        }
    }
    protected void Button_Anterior_Click(object sender, EventArgs e)
    {
        Int32 indexActual = Convert.ToInt32(HiddenField_IndexTab.Value);

        ComprobarTodasLasPestanas(false);
        Boolean resultado = ComprobarDatosPestaña(indexActual, true);

        if (resultado == true)
        {
            indexActual -= 1;
            HiddenField_IndexTab.Value = indexActual.ToString();
            SetActivoTab(indexActual);

            if (indexActual <= 0)
            {
                Button_Anterior.Visible = false;
            }
        }

        Button_Siguiente.Visible = true;
    }

    private Boolean GuardarDatosParciales()
    {
        radicacionHojasDeVida _rad = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());


        String APELLIDOS = null;
        if(String.IsNullOrEmpty(TextBox_APELLIDOS.Text.Trim()) == false)
        {
            APELLIDOS = TextBox_APELLIDOS.Text.Trim().ToUpper();
        }

        String NOMBRES = null;
        if (String.IsNullOrEmpty(TextBox_NOMBRES.Text.Trim()) == false)
        {
            NOMBRES = TextBox_NOMBRES.Text.Trim().ToUpper();
        }

        String TIP_DOC_IDENTIDAD = null;
        if(DropDownList_TIP_DOC_IDENTIDAD.SelectedIndex > 0)
        {
            TIP_DOC_IDENTIDAD = DropDownList_TIP_DOC_IDENTIDAD.SelectedValue;
        }

        String NUM_DOC_IDENTIDAD = null;
        if (String.IsNullOrEmpty(TextBox_NUM_DOC_IDENTIDAD.Text.Trim()) == false)
        {
            NUM_DOC_IDENTIDAD = TextBox_NUM_DOC_IDENTIDAD.Text.Trim().ToUpper();
        }

        String CIU_CEDULA = null;
        if (DropDownList_CIU_CEDULA.SelectedIndex > 0)
        {
            CIU_CEDULA = DropDownList_CIU_CEDULA.SelectedValue;
        }

        String LIB_MILITAR = null;
        if (String.IsNullOrEmpty(TextBox_LIB_MILITAR.Text) == false)
        {
            LIB_MILITAR = TextBox_LIB_MILITAR.Text;
        }

        String CAT_LIC_COND = null;
        if (DropDownList_CAT_LIC_COND.SelectedIndex > 0)
        {
            CAT_LIC_COND = DropDownList_CAT_LIC_COND.SelectedValue;
        }

        String DIR_ASPIRANTE = null;
        if (String.IsNullOrEmpty(TextBox_DIR_ASPIRANTE.Text.Trim()) == false)
        {
            DIR_ASPIRANTE = TextBox_DIR_ASPIRANTE.Text.Trim().ToUpper();
        }

        String SECTOR = null;
        if (String.IsNullOrEmpty(TextBox_SECTOR.Text.Trim()) == false)
        {
            SECTOR = TextBox_SECTOR.Text.Trim().ToUpper();
        }

        String CIU_ASPIRANTE = null;
        if (DropDownList_CIU_ASPIRANTE.SelectedIndex > 0)
        {
            CIU_ASPIRANTE = DropDownList_CIU_ASPIRANTE.SelectedValue;
        }

        String TEL_ASPIRANTE = null;
        if (String.IsNullOrEmpty(TextBox_TEL_ASPIRANTE.Text) == false)
        {
            TEL_ASPIRANTE = TextBox_TEL_ASPIRANTE.Text;
        }

        String SEXO = null;
        if (DropDownList_SEXO.SelectedIndex > 0)
        {
            SEXO = DropDownList_SEXO.SelectedValue;
        }

        DateTime FCH_NACIMIENTO = new DateTime();
        if (String.IsNullOrEmpty(TextBox_FCH_NACIMIENTO.Text.Trim()) == false)
        {
            FCH_NACIMIENTO = Convert.ToDateTime(TextBox_FCH_NACIMIENTO.Text);
        }

        int ID_FUENTE = 0;
        if (DropDownList_ID_FUENTE.SelectedIndex > 0)
        {
            ID_FUENTE = Convert.ToInt32(DropDownList_ID_FUENTE.SelectedValue);
        }

        String CONDUCTO = null;

        int NIV_EDUCACION = 0;
        if (DropDownList_NIV_EDUCACION.SelectedIndex > 0)
        {
            NIV_EDUCACION = Convert.ToInt32(DropDownList_NIV_EDUCACION.SelectedValue);
        }

        String E_MAIL = null;
        if (String.IsNullOrEmpty(TextBox_E_MAIL.Text.Trim()) == false)
        {
            E_MAIL = TextBox_E_MAIL.Text.Trim().ToUpper();
        }

        int ID_AREASINTERES = Convert.ToInt32(DropDownList_AREAS_ESPECIALIZACION.SelectedValue);


        Decimal ASPIRACION_SALARIAL = 0; 
        if(String.IsNullOrEmpty(TextBox_ASPIRACION_SALARIAL.Text.Trim())  == false)
        {
            ASPIRACION_SALARIAL = Convert.ToDecimal(TextBox_ASPIRACION_SALARIAL.Text);
        }

        String EXPERIENCIA = null;
        if(DropDownList_EXPERIENCIA.SelectedIndex > 0)
        {
            EXPERIENCIA = DropDownList_EXPERIENCIA.SelectedValue;
        }

        String ID_GRUPOS_PRIMARIOS = null;
        Decimal ID_OCUPACION = 0;
        if(DropDownList_ID_OCUPACION.SelectedIndex > 0)
        {
            ID_OCUPACION = Convert.ToDecimal(DropDownList_ID_OCUPACION.SelectedValue);
            cargo _cargo = new cargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaInfoCargo = _cargo.ObtenerOcupacionPorIdOcupacion(ID_OCUPACION);
            try
            {
                DataRow filaCargo = tablaInfoCargo.Rows[0];
                ID_GRUPOS_PRIMARIOS = filaCargo["COD_OCUPACION"].ToString().Trim();
            }
            catch
            {
                ID_GRUPOS_PRIMARIOS = null;
            }
        }

        String NUCLEO_FORMACION = null;
        if(DropDownList_nucleo_formacion.SelectedIndex > 0)
        {
            NUCLEO_FORMACION = DropDownList_nucleo_formacion.SelectedValue;
        }

        String TALLA_CAMISA = null;
        if(DropDownList_Talla_Camisa.SelectedIndex > 0)
        {
            TALLA_CAMISA = DropDownList_Talla_Camisa.SelectedValue;
        }

        String TALLA_PANTALON = null;
        if(DropDownList_Talla_Pantalon.SelectedIndex  > 0)
        {
            TALLA_PANTALON = DropDownList_Talla_Pantalon.SelectedValue;
        }

        String TALLA_ZAPATOS = null;
        if(DropDownList_talla_zapatos.SelectedIndex > 0)
        {
            TALLA_ZAPATOS = DropDownList_talla_zapatos.SelectedValue;
        }

        int ESTRATO = 0;
        if(DropDownList_ESTRATO.SelectedIndex > 0)
        {
            ESTRATO = Convert.ToInt32(DropDownList_ESTRATO.SelectedValue);
        }

        int NRO_HIJOS = 0;
        if(String.IsNullOrEmpty(TextBox_NUM_HIJOS.Text.Trim()) == false)
        {
            NRO_HIJOS = Convert.ToInt32(TextBox_NUM_HIJOS.Text);
        }

        Boolean C_FMLIA = false;
        if (DropDownList_CabezaFamilia.SelectedValue == "S")
        {
            C_FMLIA = true;
        }

        String CEL_ASPIRANTE = null;
        if (String.IsNullOrEmpty(TextBox_CEL_ASPIRANTE.Text) == false)
        {
            CEL_ASPIRANTE = TextBox_CEL_ASPIRANTE.Text.Trim();
        }

        String ESTADO_CIVIL = null;
        if(DropDownList_ESTADO_CIVIL.SelectedIndex > 0)
        {
            ESTADO_CIVIL = DropDownList_ESTADO_CIVIL.SelectedValue;
        }

        Int32 ID_PAIS = 0;
        if(DropDownList_PaisNacimiento.SelectedIndex > 0)
        {
            ID_PAIS = Convert.ToInt32(DropDownList_PaisNacimiento.SelectedValue);
        }

        String TIPO_VIVIENDA = null;
        if(DropDownList_TipoVivienda.SelectedIndex > 0)
        {
            TIPO_VIVIENDA = DropDownList_TipoVivienda.SelectedValue;
        }

        String FUENTE_CONOCIMIENTO = null;
        if(DropDownList_ComoSeEntero.SelectedIndex > 0)
        {
            FUENTE_CONOCIMIENTO = DropDownList_ComoSeEntero.SelectedValue;
        }

        List<FormacionAcademica> listaFormacionAcademica = new List<FormacionAcademica>();
        for (int i = 0; i < GridView_EducacionFormal.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_EducacionFormal.Rows[i];

            String TIPO_EDUCACION = "FORMAL";

            Decimal ID_INFO_ACADEMICA = Convert.ToDecimal(GridView_EducacionFormal.DataKeys[i].Values["ID_INFO_ACADEMICA"]);

            DropDownList dropnivAcademico = filaGrilla.FindControl("DropDownList_NivAcademico") as DropDownList;
            String NIVEL_ACADEMICO = dropnivAcademico.SelectedValue;

            TextBox textoInstitucion = filaGrilla.FindControl("TextBox_Institucion") as TextBox;
            String INSTITUCION = textoInstitucion.Text;

            Int32 ANNO = 0;
            TextBox textoAnno = filaGrilla.FindControl("TextBox_Anno") as TextBox;
            if (String.IsNullOrEmpty(textoAnno.Text) == false)
            {
                ANNO = Convert.ToInt32(textoAnno.Text);
            }
            else
            {
                ANNO = 0;
            }

            TextBox textoObservaciones = filaGrilla.FindControl("TextBox_Observaciones") as TextBox;
            String OBSERVACIONES = textoObservaciones.Text;

            FormacionAcademica _formacionParaLista = new FormacionAcademica();

            _formacionParaLista.ACTIVO = true;
            _formacionParaLista.ANNO = ANNO;
            _formacionParaLista.CURSO = null;
            _formacionParaLista.DURACION = 0;
            _formacionParaLista.ID_INFO_ACADEMICA = ID_INFO_ACADEMICA;
            _formacionParaLista.INSTITUCION = INSTITUCION;
            _formacionParaLista.NIVEL_ACADEMICO = NIVEL_ACADEMICO;
            _formacionParaLista.OBSERVACIONES = OBSERVACIONES;
            _formacionParaLista.REGISTRO_ENTREVISTA = 0;
            _formacionParaLista.TIPO_EDUCACION = TIPO_EDUCACION;
            _formacionParaLista.UNIDAD_DURACION = null;

            listaFormacionAcademica.Add(_formacionParaLista);
        }

        for (int i = 0; i < GridView_EducacionNoFormal.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_EducacionNoFormal.Rows[i];

            String TIPO_EDUCACION = "NO FORMAL";

            Decimal ID_INFO_ACADEMICA = Convert.ToDecimal(GridView_EducacionNoFormal.DataKeys[i].Values["ID_INFO_ACADEMICA"]);

            TextBox textoCurso = filaGrilla.FindControl("TextBox_Curso") as TextBox;
            String CURSO = textoCurso.Text;

            TextBox textoInstitucion = filaGrilla.FindControl("TextBox_Institucion") as TextBox;
            String INSTITUCION = textoInstitucion.Text;

            Decimal DURACION = 0;
            TextBox textoDuracion = filaGrilla.FindControl("TextBox_Duracion") as TextBox;
            if (String.IsNullOrEmpty(textoDuracion.Text) == false)
            {
                DURACION = Convert.ToDecimal(textoDuracion.Text);
            }
            else
            {
                DURACION = 0;
            }

            DropDownList dropUnidadDuracion = filaGrilla.FindControl("DropDownList_UnidadDuracion") as DropDownList;
            String UNIDAD_DURACION = dropUnidadDuracion.SelectedValue;

            TextBox textoobservaciones = filaGrilla.FindControl("TextBox_Observaciones") as TextBox;
            String OBSERVACIONES = textoobservaciones.Text;

            FormacionAcademica _formacionParaLista = new FormacionAcademica();

            _formacionParaLista.ACTIVO = true;
            _formacionParaLista.ANNO = 0;
            _formacionParaLista.CURSO = CURSO;
            _formacionParaLista.DURACION = DURACION;
            _formacionParaLista.ID_INFO_ACADEMICA = ID_INFO_ACADEMICA;
            _formacionParaLista.INSTITUCION = INSTITUCION;
            _formacionParaLista.NIVEL_ACADEMICO = null;
            _formacionParaLista.OBSERVACIONES = OBSERVACIONES;
            _formacionParaLista.REGISTRO_ENTREVISTA = 0;
            _formacionParaLista.TIPO_EDUCACION = TIPO_EDUCACION;
            _formacionParaLista.UNIDAD_DURACION = UNIDAD_DURACION;

            listaFormacionAcademica.Add(_formacionParaLista);

        }

        List<ExperienciaLaboral> listaExperiencia = new List<ExperienciaLaboral>();
        for (int i = 0; i < GridView_ExperienciaLaboral.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ExperienciaLaboral.Rows[i];

            Decimal ID_EXPERIENCIA = Convert.ToDecimal(GridView_ExperienciaLaboral.DataKeys[i].Values["ID_EXPERIENCIA"]);

            TextBox textoEmpresa = filaGrilla.FindControl("TextBox_Empresa") as TextBox;
            String EMPRESA = textoEmpresa.Text;

            TextBox textoCargo = filaGrilla.FindControl("TextBox_Cargo") as TextBox;
            String CARGO = textoCargo.Text;

            TextBox textoFunciones = filaGrilla.FindControl("TextBox_FuncionesRealizadas") as TextBox;
            String FUNCIONES = textoFunciones.Text;

            DateTime FECHA_INGRESO;
            TextBox textoFechaIngreso = filaGrilla.FindControl("TextBox_FechaIngreso") as TextBox;
            try
            {
                FECHA_INGRESO = Convert.ToDateTime(textoFechaIngreso.Text);
            }
            catch
            {
                FECHA_INGRESO = new DateTime();
            }

            DateTime FECHA_RETIRO;
            TextBox textoFechaRetiro = filaGrilla.FindControl("TextBox_FechaRetiro") as TextBox;
            try
            {
                FECHA_RETIRO = Convert.ToDateTime(textoFechaRetiro.Text);
            }
            catch
            {
                FECHA_RETIRO = new DateTime();
            }

            DropDownList dropMotivoRetiro = filaGrilla.FindControl("DropDownList_MotivoRetiro") as DropDownList;
            String MOTIVO_RETIRO = null;
            if (String.IsNullOrEmpty(dropMotivoRetiro.SelectedValue) == false)
            {
                MOTIVO_RETIRO = dropMotivoRetiro.SelectedValue;
            }

            Decimal ULTIMO_SALARIO = 0;
            TextBox textoUltimoSalario = filaGrilla.FindControl("TextBox_Ultimosalario") as TextBox;
            try
            {
                ULTIMO_SALARIO = Convert.ToDecimal(textoUltimoSalario.Text);
            }
            catch
            {
                ULTIMO_SALARIO = 0;
            }

            ExperienciaLaboral _experienciaParaLista = new ExperienciaLaboral();

            _experienciaParaLista.ACTIVO = true;
            _experienciaParaLista.CARGO = CARGO;
            _experienciaParaLista.EMPRESA_CLIENTE = EMPRESA;
            _experienciaParaLista.FECHA_INGRESO = FECHA_INGRESO;
            _experienciaParaLista.FECHA_RETIRO = FECHA_RETIRO;
            _experienciaParaLista.FUNCIONES = FUNCIONES;
            _experienciaParaLista.ID_EXPERIENCIA = ID_EXPERIENCIA;
            _experienciaParaLista.MOTIVO_RETIRO = MOTIVO_RETIRO;
            _experienciaParaLista.REGISTRO_ENTREVISTA = 0;
            _experienciaParaLista.ULTIMO_SALARIO = ULTIMO_SALARIO;

            listaExperiencia.Add(_experienciaParaLista);
        }

        List<ComposicionFamiliar> listaComposicionFamiliar = new List<ComposicionFamiliar>();
        for (int i = 0; i < GridView_ComposicionFamiliar.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ComposicionFamiliar.Rows[i];

            Decimal ID_COMPOSICION = Convert.ToDecimal(GridView_ComposicionFamiliar.DataKeys[i].Values["ID_COMPOSICION"]);

            DropDownList dropTipoFamiliar = filaGrilla.FindControl("DropDownList_TipoFamiliar") as DropDownList;
            String ID_TIPO_FAMILIAR = dropTipoFamiliar.SelectedValue;

            TextBox textoNombres = filaGrilla.FindControl("TextBox_NombresFamiliar") as TextBox;
            String NOMBRES_FAMILIAR = textoNombres.Text;

            TextBox textoApellidos = filaGrilla.FindControl("TextBox_ApellidosFamiliar") as TextBox;
            String APELLIDOS_FAMILIAR = textoApellidos.Text;

            DateTime FECHA_NACIMIENTO = new DateTime();
            TextBox textoFechaNacimiento = filaGrilla.FindControl("TextBox_FechaNacimientoFamiliar") as TextBox;
            try
            {
                FECHA_NACIMIENTO = Convert.ToDateTime(textoFechaNacimiento.Text);
            }
            catch
            {
                FECHA_NACIMIENTO = new DateTime();
            }

            TextBox textoProfesion = filaGrilla.FindControl("TextBox_ProfesionFamiliar") as TextBox;
            String PROFESION = textoProfesion.Text;

            CheckBox checkExtrajero = filaGrilla.FindControl("CheckBox_Extranjero") as CheckBox;
            DropDownList dropCiudad = filaGrilla.FindControl("DropDownList_CiudadFamiliar") as DropDownList;
            String ID_CIUDAD = dropCiudad.SelectedValue;
            if (checkExtrajero.Checked == true)
            {
                ID_CIUDAD = "EXTRA";
            }

            Boolean VIVE_CON_EL = false;
            CheckBox checkViveConEl = filaGrilla.FindControl("CheckBox_ViveConElFamiliar") as CheckBox;
            if (checkViveConEl.Checked == true)
            {
                VIVE_CON_EL = true;
            }

            Boolean ACTIVO = true;

            ComposicionFamiliar _composicionParaLista = new ComposicionFamiliar();

            _composicionParaLista.ACTIVO = ACTIVO;
            _composicionParaLista.APELLIDOS = APELLIDOS_FAMILIAR;
            _composicionParaLista.FECHA_NACIMIENTO = FECHA_NACIMIENTO;
            _composicionParaLista.ID_CIUDAD = ID_CIUDAD;
            _composicionParaLista.ID_COMPOSICION = ID_COMPOSICION;
            _composicionParaLista.ID_TIPO_FAMILIAR = ID_TIPO_FAMILIAR;
            _composicionParaLista.NOMBRES = NOMBRES_FAMILIAR;
            _composicionParaLista.PROFESION = PROFESION;
            _composicionParaLista.REGISTRO_ENTREVISTA = 0;
            _composicionParaLista.VIVE_CON_EL = VIVE_CON_EL;

            listaComposicionFamiliar.Add(_composicionParaLista);
        }


        String RH = null;
        if (String.IsNullOrEmpty(DropDownList_RH.SelectedValue) == false)
        {
            RH = DropDownList_RH.SelectedValue;
        }

        Dictionary<String, String> listaCamposValidarRestricciones = new Dictionary<String, String>();
        listaCamposValidarRestricciones.Add("APELLIDOS", APELLIDOS);
        listaCamposValidarRestricciones.Add("NOMBRES", NOMBRES);
        listaCamposValidarRestricciones.Add("DIRECCIÓN", DIR_ASPIRANTE);
        listaCamposValidarRestricciones.Add("BARRIO", SECTOR);
        listaCamposValidarRestricciones.Add("TELÉFONO", TEL_ASPIRANTE);
        listaCamposValidarRestricciones.Add("E-MAIL", E_MAIL);
        listaCamposValidarRestricciones.Add("CELULAR", CEL_ASPIRANTE);

        CrtRestriccionPalabra _restricciones = new CrtRestriccionPalabra(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        _restricciones.listaPalabrasEntrada = listaCamposValidarRestricciones;
        _restricciones.ComprobarListaPalabras();

        if (_restricciones.listaPalabrasSalida.Count <= 0)
        {
            DataTable tablaComprobacion = _rad.ObtenerRegSolicitudesingresoPorNumDocIdentidad(NUM_DOC_IDENTIDAD);

            if (tablaComprobacion.Rows.Count > 0)
            {
                DataRow filaComprobacion = tablaComprobacion.Rows[0];

                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El número de documento " + NUM_DOC_IDENTIDAD + " ya se encuentra registrado en la Base de Datos.<br>Fecha Registro: " + filaComprobacion["FECHA_R"].ToString().Trim() + ", Usuario: " + filaComprobacion["USU_CRE"].ToString().Trim() + ", Nombre Candidato: " + filaComprobacion["NOMBRES"].ToString().Trim() + " " + filaComprobacion["APELLIDOS"].ToString().Trim() + ".<BR>NO SE PUDO CREAR EL REGISTRO.", Proceso.Advertencia);
                return false;
            }
            else
            {
                Decimal ID_SOLICITUD = _rad.AdicionarRegSolicitudesingreso(DateTime.Now, APELLIDOS, NOMBRES, TIP_DOC_IDENTIDAD, NUM_DOC_IDENTIDAD, CIU_CEDULA, LIB_MILITAR, CAT_LIC_COND, DIR_ASPIRANTE, SECTOR, CIU_ASPIRANTE, TEL_ASPIRANTE, SEXO, FCH_NACIMIENTO, ID_GRUPOS_PRIMARIOS, ID_FUENTE, CONDUCTO, NIV_EDUCACION, E_MAIL, ID_AREASINTERES, ASPIRACION_SALARIAL, EXPERIENCIA, ID_OCUPACION, NUCLEO_FORMACION, TALLA_CAMISA, TALLA_PANTALON, TALLA_ZAPATOS, ESTRATO, NRO_HIJOS, C_FMLIA, CEL_ASPIRANTE, ESTADO_CIVIL, ID_PAIS, TIPO_VIVIENDA, FUENTE_CONOCIMIENTO, listaFormacionAcademica, listaExperiencia, listaComposicionFamiliar, RH);

                if (ID_SOLICITUD <= 0)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _rad.MensajeError, Proceso.Error);
                    return false;
                }
                else
                {
                    HiddenField_ID_SOLICITUD.Value = ID_SOLICITUD.ToString();
                    HiddenField_AccionSobreFormulario.Value = AccionesForm.Modificar.ToString();
                    return true;
                }
            }
        }
        else
        {
            Int32 contador = 0;
            String mensaje = "";
            foreach (KeyValuePair<String, String> restriccion in _restricciones.listaPalabrasSalida)
            {
                if (contador <= 0)
                {
                    mensaje = restriccion.Value;
                }
                else
                {
                    mensaje += "</br>" + restriccion.Value;
                }

                contador += 1;
            }

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, mensaje, Proceso.Advertencia);
            return false;
        }
    }

    private Boolean ActualizarDatosParciales()
    {
        radicacionHojasDeVida _rad = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);

        String APELLIDOS = null;
        if(String.IsNullOrEmpty(TextBox_APELLIDOS.Text.Trim()) == false)
        {
            APELLIDOS = TextBox_APELLIDOS.Text.Trim().ToUpper();
        }

        String NOMBRES = null;
        if (String.IsNullOrEmpty(TextBox_NOMBRES.Text.Trim()) == false)
        {
            NOMBRES = TextBox_NOMBRES.Text.Trim().ToUpper();
        }

        String TIP_DOC_IDENTIDAD = null;
        if(DropDownList_TIP_DOC_IDENTIDAD.SelectedIndex > 0)
        {
            TIP_DOC_IDENTIDAD = DropDownList_TIP_DOC_IDENTIDAD.SelectedValue;
        }

        String NUM_DOC_IDENTIDAD = null;
        if (String.IsNullOrEmpty(TextBox_NUM_DOC_IDENTIDAD.Text.Trim()) == false)
        {
            NUM_DOC_IDENTIDAD = TextBox_NUM_DOC_IDENTIDAD.Text.Trim().ToUpper();
        }

        String CIU_CEDULA = null;
        if (DropDownList_CIU_CEDULA.SelectedIndex > 0)
        {
            CIU_CEDULA = DropDownList_CIU_CEDULA.SelectedValue;
        }

        String LIB_MILITAR = null;
        if (String.IsNullOrEmpty(TextBox_LIB_MILITAR.Text) == false)
        {
            LIB_MILITAR = TextBox_LIB_MILITAR.Text;
        }

        String CAT_LIC_COND = null;
        if (DropDownList_CAT_LIC_COND.SelectedIndex > 0)
        {
            CAT_LIC_COND = DropDownList_CAT_LIC_COND.SelectedValue;
        }

        String DIR_ASPIRANTE = null;
        if (String.IsNullOrEmpty(TextBox_DIR_ASPIRANTE.Text.Trim()) == false)
        {
            DIR_ASPIRANTE = TextBox_DIR_ASPIRANTE.Text.Trim().ToUpper();
        }

        String SECTOR = null;
        if (String.IsNullOrEmpty(TextBox_SECTOR.Text.Trim()) == false)
        {
            SECTOR = TextBox_SECTOR.Text.Trim().ToUpper();
        }

        String CIU_ASPIRANTE = null;
        if (DropDownList_CIU_ASPIRANTE.SelectedIndex > 0)
        {
            CIU_ASPIRANTE = DropDownList_CIU_ASPIRANTE.SelectedValue;
        }

        String TEL_ASPIRANTE = null;
        if (String.IsNullOrEmpty(TextBox_TEL_ASPIRANTE.Text) == false)
        {
            TEL_ASPIRANTE = TextBox_TEL_ASPIRANTE.Text;
        }

        String SEXO = null;
        if (DropDownList_SEXO.SelectedIndex > 0)
        {
            SEXO = DropDownList_SEXO.SelectedValue;
        }

        DateTime FCH_NACIMIENTO = new DateTime();
        if (String.IsNullOrEmpty(TextBox_FCH_NACIMIENTO.Text.Trim()) == false)
        {
            FCH_NACIMIENTO = Convert.ToDateTime(TextBox_FCH_NACIMIENTO.Text);
        }

        int ID_FUENTE = 0;
        if (DropDownList_ID_FUENTE.SelectedIndex > 0)
        {
            ID_FUENTE = Convert.ToInt32(DropDownList_ID_FUENTE.SelectedValue);
        }

        String CONDUCTO = null;
        
        int NIV_EDUCACION = 0;
        if (DropDownList_NIV_EDUCACION.SelectedIndex > 0)
        {
            NIV_EDUCACION = Convert.ToInt32(DropDownList_NIV_EDUCACION.SelectedValue);
        }

        String E_MAIL = null;
        if (String.IsNullOrEmpty(TextBox_E_MAIL.Text.Trim()) == false)
        {
            E_MAIL = TextBox_E_MAIL.Text.Trim().ToUpper();
        }

        int ID_AREASINTERES = Convert.ToInt32(DropDownList_AREAS_ESPECIALIZACION.SelectedValue);
        
        Decimal ASPIRACION_SALARIAL = 0; 
        if(String.IsNullOrEmpty(TextBox_ASPIRACION_SALARIAL.Text.Trim())  == false)
        {
            ASPIRACION_SALARIAL = Convert.ToDecimal(TextBox_ASPIRACION_SALARIAL.Text);
        }

        String EXPERIENCIA = null;
        if(DropDownList_EXPERIENCIA.SelectedIndex > 0)
        {
            EXPERIENCIA = DropDownList_EXPERIENCIA.SelectedValue;
        }

        String ID_GRUPOS_PRIMARIOS = null;
        Decimal ID_OCUPACION = 0;
        if(DropDownList_ID_OCUPACION.SelectedIndex > 0)
        {
            ID_OCUPACION = Convert.ToDecimal(DropDownList_ID_OCUPACION.SelectedValue);
            cargo _cargo = new cargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaInfoCargo = _cargo.ObtenerOcupacionPorIdOcupacion(ID_OCUPACION);
            try
            {
                DataRow filaCargo = tablaInfoCargo.Rows[0];
                ID_GRUPOS_PRIMARIOS = filaCargo["COD_OCUPACION"].ToString().Trim();
            }
            catch
            {
                ID_GRUPOS_PRIMARIOS = null;
            }
        }

        String NUCLEO_FORMACION = null;
        if(DropDownList_nucleo_formacion.SelectedIndex > 0)
        {
            NUCLEO_FORMACION = DropDownList_nucleo_formacion.SelectedValue;
        }

        String TALLA_CAMISA = null;
        if(DropDownList_Talla_Camisa.SelectedIndex > 0)
        {
            TALLA_CAMISA = DropDownList_Talla_Camisa.SelectedValue;
        }

        String TALLA_PANTALON = null;
        if(DropDownList_Talla_Pantalon.SelectedIndex  > 0)
        {
            TALLA_PANTALON = DropDownList_Talla_Pantalon.SelectedValue;
        }

        String TALLA_ZAPATOS = null;
        if(DropDownList_talla_zapatos.SelectedIndex > 0)
        {
            TALLA_ZAPATOS = DropDownList_talla_zapatos.SelectedValue;
        }

        int ESTRATO = 0;
        if(DropDownList_ESTRATO.SelectedIndex > 0)
        {
            ESTRATO = Convert.ToInt32(DropDownList_ESTRATO.SelectedValue);
        }

        int NRO_HIJOS = 0;
        if(String.IsNullOrEmpty(TextBox_NUM_HIJOS.Text.Trim()) == false)
        {
            NRO_HIJOS = Convert.ToInt32(TextBox_NUM_HIJOS.Text);
        }

        Boolean C_FMLIA = false;
        if (DropDownList_CabezaFamilia.SelectedValue == "S")
        {
            C_FMLIA = true;
        }

        String CEL_ASPIRANTE = null;
        if (String.IsNullOrEmpty(TextBox_CEL_ASPIRANTE.Text) == false)
        {
            CEL_ASPIRANTE = TextBox_CEL_ASPIRANTE.Text.Trim();
        }

        String ESTADO_CIVIL = null;
        if(DropDownList_ESTADO_CIVIL.SelectedIndex > 0)
        {
            ESTADO_CIVIL = DropDownList_ESTADO_CIVIL.SelectedValue;
        }

        Int32 ID_PAIS = 0;
        if(DropDownList_PaisNacimiento.SelectedIndex > 0)
        {
            ID_PAIS = Convert.ToInt32(DropDownList_PaisNacimiento.SelectedValue);
        }

        String TIPO_VIVIENDA = null;
        if(DropDownList_TipoVivienda.SelectedIndex > 0)
        {
            TIPO_VIVIENDA = DropDownList_TipoVivienda.SelectedValue;
        }

        String FUENTE_CONOCIMIENTO = null;
        if(DropDownList_ComoSeEntero.SelectedIndex > 0)
        {
            FUENTE_CONOCIMIENTO = DropDownList_ComoSeEntero.SelectedValue;
        }

        List<FormacionAcademica> listaFormacionAcademica = new List<FormacionAcademica>();
        for (int i = 0; i < GridView_EducacionFormal.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_EducacionFormal.Rows[i];

            String TIPO_EDUCACION = "FORMAL";

            Decimal ID_INFO_ACADEMICA = Convert.ToDecimal(GridView_EducacionFormal.DataKeys[i].Values["ID_INFO_ACADEMICA"]);

            DropDownList dropnivAcademico = filaGrilla.FindControl("DropDownList_NivAcademico") as DropDownList;
            String NIVEL_ACADEMICO = dropnivAcademico.SelectedValue;

            TextBox textoInstitucion = filaGrilla.FindControl("TextBox_Institucion") as TextBox;
            String INSTITUCION = textoInstitucion.Text;

            Int32 ANNO = 0;
            TextBox textoAnno = filaGrilla.FindControl("TextBox_Anno") as TextBox;
            if (String.IsNullOrEmpty(textoAnno.Text) == false)
            {
                ANNO = Convert.ToInt32(textoAnno.Text);
            }
            else
            {
                ANNO = 0;
            }

            TextBox textoObservaciones = filaGrilla.FindControl("TextBox_Observaciones") as TextBox;
            String OBSERVACIONES = textoObservaciones.Text;

            FormacionAcademica _formacionParaLista = new FormacionAcademica();

            _formacionParaLista.ACTIVO = true;
            _formacionParaLista.ANNO = ANNO;
            _formacionParaLista.CURSO = null;
            _formacionParaLista.DURACION = 0;
            _formacionParaLista.ID_INFO_ACADEMICA = ID_INFO_ACADEMICA;
            _formacionParaLista.INSTITUCION = INSTITUCION;
            _formacionParaLista.NIVEL_ACADEMICO = NIVEL_ACADEMICO;
            _formacionParaLista.OBSERVACIONES = OBSERVACIONES;
            _formacionParaLista.REGISTRO_ENTREVISTA = 0;
            _formacionParaLista.TIPO_EDUCACION = TIPO_EDUCACION;
            _formacionParaLista.UNIDAD_DURACION = null;

            listaFormacionAcademica.Add(_formacionParaLista);
        }

        for (int i = 0; i < GridView_EducacionNoFormal.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_EducacionNoFormal.Rows[i];

            String TIPO_EDUCACION = "NO FORMAL";

            Decimal ID_INFO_ACADEMICA = Convert.ToDecimal(GridView_EducacionNoFormal.DataKeys[i].Values["ID_INFO_ACADEMICA"]);

            TextBox textoCurso = filaGrilla.FindControl("TextBox_Curso") as TextBox;
            String CURSO = textoCurso.Text;

            TextBox textoInstitucion = filaGrilla.FindControl("TextBox_Institucion") as TextBox;
            String INSTITUCION = textoInstitucion.Text;

            Decimal DURACION = 0;
            TextBox textoDuracion = filaGrilla.FindControl("TextBox_Duracion") as TextBox;
            if (String.IsNullOrEmpty(textoDuracion.Text) == false)
            {
                DURACION = Convert.ToDecimal(textoDuracion.Text);
            }
            else
            {
                DURACION = 0;
            }

            DropDownList dropUnidadDuracion = filaGrilla.FindControl("DropDownList_UnidadDuracion") as DropDownList;
            String UNIDAD_DURACION = dropUnidadDuracion.SelectedValue;

            TextBox textoobservaciones = filaGrilla.FindControl("TextBox_Observaciones") as TextBox;
            String OBSERVACIONES = textoobservaciones.Text;

            FormacionAcademica _formacionParaLista = new FormacionAcademica();

            _formacionParaLista.ACTIVO = true;
            _formacionParaLista.ANNO = 0;
            _formacionParaLista.CURSO = CURSO;
            _formacionParaLista.DURACION = DURACION;
            _formacionParaLista.ID_INFO_ACADEMICA = ID_INFO_ACADEMICA;
            _formacionParaLista.INSTITUCION = INSTITUCION;
            _formacionParaLista.NIVEL_ACADEMICO = null;
            _formacionParaLista.OBSERVACIONES = OBSERVACIONES;
            _formacionParaLista.REGISTRO_ENTREVISTA = 0;
            _formacionParaLista.TIPO_EDUCACION = TIPO_EDUCACION;
            _formacionParaLista.UNIDAD_DURACION = UNIDAD_DURACION;

            listaFormacionAcademica.Add(_formacionParaLista);
        }

        List<ExperienciaLaboral> listaExperiencia = new List<ExperienciaLaboral>();
        for (int i = 0; i < GridView_ExperienciaLaboral.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ExperienciaLaboral.Rows[i];

            Decimal ID_EXPERIENCIA = Convert.ToDecimal(GridView_ExperienciaLaboral.DataKeys[i].Values["ID_EXPERIENCIA"]);

            TextBox textoEmpresa = filaGrilla.FindControl("TextBox_Empresa") as TextBox;
            String EMPRESA = textoEmpresa.Text;

            TextBox textoCargo = filaGrilla.FindControl("TextBox_Cargo") as TextBox;
            String CARGO = textoCargo.Text;

            TextBox textoFunciones = filaGrilla.FindControl("TextBox_FuncionesRealizadas") as TextBox;
            String FUNCIONES = textoFunciones.Text;

            DateTime FECHA_INGRESO;
            TextBox textoFechaIngreso = filaGrilla.FindControl("TextBox_FechaIngreso") as TextBox;
            try
            {
                FECHA_INGRESO = Convert.ToDateTime(textoFechaIngreso.Text);
            }
            catch
            {
                FECHA_INGRESO = new DateTime();
            }

            DateTime FECHA_RETIRO;
            TextBox textoFechaRetiro = filaGrilla.FindControl("TextBox_FechaRetiro") as TextBox;
            try
            {
                FECHA_RETIRO = Convert.ToDateTime(textoFechaRetiro.Text);
            }
            catch
            {
                FECHA_RETIRO = new DateTime();
            }

            DropDownList dropMotivoRetiro = filaGrilla.FindControl("DropDownList_MotivoRetiro") as DropDownList;
            String MOTIVO_RETIRO = null;
            if (String.IsNullOrEmpty(dropMotivoRetiro.SelectedValue) == false)
            {
                MOTIVO_RETIRO = dropMotivoRetiro.SelectedValue;
            }

            Decimal ULTIMO_SALARIO = 0;
            TextBox textoUltimoSalario = filaGrilla.FindControl("TextBox_Ultimosalario") as TextBox;
            try
            {
                ULTIMO_SALARIO = Convert.ToDecimal(textoUltimoSalario.Text);
            }
            catch
            {
                ULTIMO_SALARIO = 0;
            }

            ExperienciaLaboral _experienciaParaLista = new ExperienciaLaboral();

            _experienciaParaLista.ACTIVO = true;
            _experienciaParaLista.CARGO = CARGO;
            _experienciaParaLista.EMPRESA_CLIENTE = EMPRESA;
            _experienciaParaLista.FECHA_INGRESO = FECHA_INGRESO;
            _experienciaParaLista.FECHA_RETIRO = FECHA_RETIRO;
            _experienciaParaLista.FUNCIONES = FUNCIONES;
            _experienciaParaLista.ID_EXPERIENCIA = ID_EXPERIENCIA;
            _experienciaParaLista.MOTIVO_RETIRO = MOTIVO_RETIRO;
            _experienciaParaLista.REGISTRO_ENTREVISTA = 0;
            _experienciaParaLista.ULTIMO_SALARIO = ULTIMO_SALARIO;

            listaExperiencia.Add(_experienciaParaLista);
        }

        List<ComposicionFamiliar> listaComposicionFamiliar = new List<ComposicionFamiliar>();
        for (int i = 0; i < GridView_ComposicionFamiliar.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ComposicionFamiliar.Rows[i];

            Decimal ID_COMPOSICION = Convert.ToDecimal(GridView_ComposicionFamiliar.DataKeys[i].Values["ID_COMPOSICION"]);

            DropDownList dropTipoFamiliar = filaGrilla.FindControl("DropDownList_TipoFamiliar") as DropDownList;
            String ID_TIPO_FAMILIAR = dropTipoFamiliar.SelectedValue;

            TextBox textoNombres = filaGrilla.FindControl("TextBox_NombresFamiliar") as TextBox;
            String NOMBRES_FAMILIAR = textoNombres.Text;

            TextBox textoApellidos = filaGrilla.FindControl("TextBox_ApellidosFamiliar") as TextBox;
            String APELLIDOS_FAMILIAR = textoApellidos.Text;

            DateTime FECHA_NACIMIENTO = new DateTime();
            TextBox textoFechaNacimiento = filaGrilla.FindControl("TextBox_FechaNacimientoFamiliar") as TextBox;
            try
            {
                FECHA_NACIMIENTO = Convert.ToDateTime(textoFechaNacimiento.Text);
            }
            catch
            {
                FECHA_NACIMIENTO = new DateTime();
            }

            TextBox textoProfesion = filaGrilla.FindControl("TextBox_ProfesionFamiliar") as TextBox;
            String PROFESION = textoProfesion.Text;

            CheckBox checkExtrajero = filaGrilla.FindControl("CheckBox_Extranjero") as CheckBox;
            DropDownList dropCiudad = filaGrilla.FindControl("DropDownList_CiudadFamiliar") as DropDownList;
            String ID_CIUDAD = dropCiudad.SelectedValue;
            if (checkExtrajero.Checked == true)
            {
                ID_CIUDAD = "EXTRA";
            }

            Boolean VIVE_CON_EL = false;
            CheckBox checkViveConEl = filaGrilla.FindControl("CheckBox_ViveConElFamiliar") as CheckBox;
            if (checkViveConEl.Checked == true)
            {
                VIVE_CON_EL = true;
            }

            Boolean ACTIVO = true;

            ComposicionFamiliar _composicionParaLista = new ComposicionFamiliar();

            _composicionParaLista.ACTIVO = ACTIVO;
            _composicionParaLista.APELLIDOS = APELLIDOS_FAMILIAR;
            _composicionParaLista.FECHA_NACIMIENTO = FECHA_NACIMIENTO;
            _composicionParaLista.ID_CIUDAD = ID_CIUDAD;
            _composicionParaLista.ID_COMPOSICION = ID_COMPOSICION;
            _composicionParaLista.ID_TIPO_FAMILIAR = ID_TIPO_FAMILIAR;
            _composicionParaLista.NOMBRES = NOMBRES_FAMILIAR;
            _composicionParaLista.PROFESION = PROFESION;
            _composicionParaLista.REGISTRO_ENTREVISTA = 0;
            _composicionParaLista.VIVE_CON_EL = VIVE_CON_EL;

            listaComposicionFamiliar.Add(_composicionParaLista);
        }


        String RH = null;
        if (String.IsNullOrEmpty(DropDownList_RH.SelectedValue) == false)
        {
            RH = DropDownList_RH.SelectedValue;
        }

        Dictionary<String, String> listaCamposValidarRestricciones = new Dictionary<String, String>();
        listaCamposValidarRestricciones.Add("APELLIDOS", APELLIDOS);
        listaCamposValidarRestricciones.Add("NOMBRES", NOMBRES);
        listaCamposValidarRestricciones.Add("DIRECCIÓN", DIR_ASPIRANTE);
        listaCamposValidarRestricciones.Add("BARRIO", SECTOR);
        listaCamposValidarRestricciones.Add("TELÉFONO", TEL_ASPIRANTE);
        listaCamposValidarRestricciones.Add("E-MAIL", E_MAIL);
        listaCamposValidarRestricciones.Add("CELULAR", CEL_ASPIRANTE);

        CrtRestriccionPalabra _restricciones = new CrtRestriccionPalabra(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        _restricciones.listaPalabrasEntrada = listaCamposValidarRestricciones;
        _restricciones.ComprobarListaPalabras();

        if (_restricciones.listaPalabrasSalida.Count <= 0)
        {

            DataTable tablaComprobacion = _rad.ObtenerRegSolicitudesingresoPorNumDocIdentidadOmitiendoIdSolicitud(NUM_DOC_IDENTIDAD, ID_SOLICITUD);

            if (tablaComprobacion.Rows.Count > 0)
            {
                DataRow filaComprobacion = tablaComprobacion.Rows[0];

                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El número de documento " + NUM_DOC_IDENTIDAD + " ya se encuentra registrado en la Base de Datos.<br>Fecha Registro: " + filaComprobacion["FECHA_R"].ToString().Trim() + ", Usuario: " + filaComprobacion["USU_CRE"].ToString().Trim() + ", Nombre Candidato: " + filaComprobacion["NOMBRES"].ToString().Trim() + " " + filaComprobacion["APELLIDOS"].ToString().Trim() + ".<BR>NO SE PUDO EDITAR EL REGISTRO.", Proceso.Advertencia);
                return false;
            }
            else
            {
                Boolean verificador = true;
                verificador = _rad.ActualizarRegSolicitudesingreso(ID_SOLICITUD, APELLIDOS, NOMBRES, TIP_DOC_IDENTIDAD, NUM_DOC_IDENTIDAD, CIU_CEDULA, LIB_MILITAR, CAT_LIC_COND, DIR_ASPIRANTE, SECTOR, CIU_ASPIRANTE, TEL_ASPIRANTE, SEXO, FCH_NACIMIENTO, ID_GRUPOS_PRIMARIOS, ID_FUENTE, CONDUCTO, NIV_EDUCACION, E_MAIL, ID_AREASINTERES, ASPIRACION_SALARIAL, EXPERIENCIA, ID_OCUPACION, NUCLEO_FORMACION, TALLA_CAMISA, TALLA_PANTALON, TALLA_ZAPATOS, ESTRATO, NRO_HIJOS, C_FMLIA, CEL_ASPIRANTE, ESTADO_CIVIL, ID_PAIS, TIPO_VIVIENDA, FUENTE_CONOCIMIENTO, listaFormacionAcademica, listaExperiencia, listaComposicionFamiliar, RH);

                if (verificador == false)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _rad.MensajeError, Proceso.Error);
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        else
        {
            Int32 contador = 0;
            String mensaje = "";
            foreach (KeyValuePair<String, String> restriccion in _restricciones.listaPalabrasSalida)
            {
                if (contador <= 0)
                {
                    mensaje = restriccion.Value;
                }
                else
                {
                    mensaje += "</br>" + restriccion.Value;
                }

                contador += 1;
            }

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, mensaje, Proceso.Advertencia);
            return false;
        }
    }

    protected void Button_Siguiente_Click(object sender, EventArgs e)
    {
        Int32 indexActual = Convert.ToInt32(HiddenField_IndexTab.Value);

        ComprobarTodasLasPestanas(false);
        Boolean resultado = ComprobarDatosPestaña(indexActual, true);

        if (resultado == true)
        {
            if (String.IsNullOrEmpty(HiddenField_ID_SOLICITUD.Value) == true)
            {
                resultado = GuardarDatosParciales();
            }
            else
            {
                resultado = ActualizarDatosParciales();
            }

            if (resultado == true)
            {
                indexActual += 1;
                HiddenField_IndexTab.Value = indexActual.ToString();
                SetActivoTab(indexActual);

                if (indexActual >= 5)
                {
                    Button_Siguiente.Visible = false;
                }
            }
        }

        Button_Anterior.Visible = true;
    }

    private Boolean ComprobarTodasLasPestanas(Boolean informar)
    {
        Boolean resultado = true;

        
        if (ComprobarDatosPestaña(0, informar) == false)
        {
            resultado = false;
            HiddenField_EstadoDatosBasicos.Value = "N";
        }
        else
        {
            HiddenField_EstadoDatosBasicos.Value = "S";
        }

        if (ComprobarDatosPestaña(1, informar) == false)
        {
            resultado = false;
            HiddenField_EstadoUbicacion.Value = "N";
        }
        else
        {
            HiddenField_EstadoUbicacion.Value = "S";
        }

        if (ComprobarDatosPestaña(2, informar) == false)
        {
            resultado = false;
            HiddenField_EstadoEducacion.Value = "N";
        }
        else
        {
            HiddenField_EstadoEducacion.Value = "S";
        }

        if (ComprobarDatosPestaña(3, informar) == false)
        {
            resultado = false;
            HiddenField_EstadoDatosLaborales.Value = "N";
        }
        else
        {
            HiddenField_EstadoDatosLaborales.Value = "S";
        }

        if (ComprobarDatosPestaña(4, informar) == false)
        {
            resultado = false;
            HiddenField_EstadoFamilia.Value = "N";
        }
        else
        {
            HiddenField_EstadoFamilia.Value = "S";
        }

        if (ComprobarDatosPestaña(5, informar) == false)
        {
            resultado = false;
            HiddenField_DatosAdicionales.Value = "N";
        }
        else
        {
            HiddenField_DatosAdicionales.Value = "S";        
        }

        return resultado;
    }

    private void Guardar()
    {
        radicacionHojasDeVida _rad = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        

        String APELLIDOS = TextBox_APELLIDOS.Text.ToUpper(); 
        String NOMBRES = TextBox_NOMBRES.Text.ToUpper();
        String TIP_DOC_IDENTIDAD = DropDownList_TIP_DOC_IDENTIDAD.SelectedValue;
        String NUM_DOC_IDENTIDAD = TextBox_NUM_DOC_IDENTIDAD.Text.Trim();

        String CIU_CEDULA = null;
        if(DropDownList_CIU_CEDULA.SelectedIndex > 0)
        {
            CIU_CEDULA = DropDownList_CIU_CEDULA.SelectedValue;
        }

        String LIB_MILITAR = null;
        if(String.IsNullOrEmpty(TextBox_LIB_MILITAR.Text) == false)
        {
            LIB_MILITAR = TextBox_LIB_MILITAR.Text;
        }

        String CAT_LIC_COND = null;
        if(DropDownList_CAT_LIC_COND.SelectedIndex > 0)
        {
            CAT_LIC_COND = DropDownList_CAT_LIC_COND.SelectedValue;
        }

        String DIR_ASPIRANTE = TextBox_DIR_ASPIRANTE.Text.Trim().ToUpper();
        String SECTOR = TextBox_SECTOR.Text.Trim().ToUpper();
        String CIU_ASPIRANTE = DropDownList_CIU_ASPIRANTE.SelectedValue;
        
        String TEL_ASPIRANTE = null;
        if(String.IsNullOrEmpty(TextBox_TEL_ASPIRANTE.Text) == false)
        {
            TEL_ASPIRANTE = TextBox_TEL_ASPIRANTE.Text;
        }

        String SEXO = DropDownList_SEXO.SelectedValue;
        DateTime FCH_NACIMIENTO = Convert.ToDateTime(TextBox_FCH_NACIMIENTO.Text);
        int ID_FUENTE = Convert.ToInt32(DropDownList_ID_FUENTE.SelectedValue);
        String CONDUCTO = null;
        int NIV_EDUCACION = Convert.ToInt32(DropDownList_NIV_EDUCACION.SelectedValue);
        String E_MAIL = TextBox_E_MAIL.Text.Trim();
        int ID_AREASINTERES = Convert.ToInt32(DropDownList_AREAS_ESPECIALIZACION.SelectedValue); 
        Decimal ASPIRACION_SALARIAL = Convert.ToDecimal(TextBox_ASPIRACION_SALARIAL.Text);
        String EXPERIENCIA = DropDownList_EXPERIENCIA.SelectedValue;

        String ID_GRUPOS_PRIMARIOS = null;
        Decimal ID_OCUPACION = Convert.ToDecimal(DropDownList_ID_OCUPACION.SelectedValue); 
        cargo _cargo = new cargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoCargo = _cargo.ObtenerOcupacionPorIdOcupacion(ID_OCUPACION);
        try
        {
            DataRow filaCargo = tablaInfoCargo.Rows[0];
            ID_GRUPOS_PRIMARIOS = filaCargo["COD_OCUPACION"].ToString().Trim();
        }
        catch
        {
            ID_GRUPOS_PRIMARIOS = null;
        }
        
        String NUCLEO_FORMACION = DropDownList_nucleo_formacion.SelectedValue;
        String TALLA_CAMISA = DropDownList_Talla_Camisa.SelectedValue;  
        String TALLA_PANTALON = DropDownList_Talla_Pantalon.SelectedValue;
        String TALLA_ZAPATOS = DropDownList_talla_zapatos.SelectedValue;
        int ESTRATO = Convert.ToInt32(DropDownList_ESTRATO.SelectedValue);
        int NRO_HIJOS = Convert.ToInt32(TextBox_NUM_HIJOS.Text);

        Boolean C_FMLIA = false; 
        if(DropDownList_CabezaFamilia.SelectedValue == "S")
        {
            C_FMLIA = true;            
        }

        String CEL_ASPIRANTE = null;
        if(String.IsNullOrEmpty(TextBox_CEL_ASPIRANTE.Text) == false)
        {
            CEL_ASPIRANTE = TextBox_CEL_ASPIRANTE.Text.Trim();
        }

        String ESTADO_CIVIL = DropDownList_ESTADO_CIVIL.SelectedValue;
        Int32 ID_PAIS = Convert.ToInt32(DropDownList_PaisNacimiento.SelectedValue);

        String TIPO_VIVIENDA = DropDownList_TipoVivienda.SelectedValue;

        String FUENTE_CONOCIMIENTO = DropDownList_ComoSeEntero.SelectedValue;

        List<FormacionAcademica> listaFormacionAcademica = new List<FormacionAcademica>();
        for (int i = 0; i < GridView_EducacionFormal.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_EducacionFormal.Rows[i];

            String TIPO_EDUCACION = "FORMAL";

            Decimal ID_INFO_ACADEMICA = Convert.ToDecimal(GridView_EducacionFormal.DataKeys[i].Values["ID_INFO_ACADEMICA"]);

            DropDownList dropnivAcademico = filaGrilla.FindControl("DropDownList_NivAcademico") as DropDownList;
            String NIVEL_ACADEMICO = dropnivAcademico.SelectedValue;

            TextBox textoInstitucion = filaGrilla.FindControl("TextBox_Institucion") as TextBox;
            String INSTITUCION = textoInstitucion.Text;

            Int32 ANNO = 0;
            TextBox textoAnno = filaGrilla.FindControl("TextBox_Anno") as TextBox;
            if (String.IsNullOrEmpty(textoAnno.Text) == false)
            {
                ANNO = Convert.ToInt32(textoAnno.Text);
            }
            else
            {
                ANNO = 0;
            }

            TextBox textoObservaciones = filaGrilla.FindControl("TextBox_Observaciones") as TextBox;
            String OBSERVACIONES = textoObservaciones.Text;

            FormacionAcademica _formacionParaLista = new FormacionAcademica();

            _formacionParaLista.ACTIVO = true;
            _formacionParaLista.ANNO = ANNO;
            _formacionParaLista.CURSO = null;
            _formacionParaLista.DURACION = 0;
            _formacionParaLista.ID_INFO_ACADEMICA = ID_INFO_ACADEMICA;
            _formacionParaLista.INSTITUCION = INSTITUCION;
            _formacionParaLista.NIVEL_ACADEMICO = NIVEL_ACADEMICO;
            _formacionParaLista.OBSERVACIONES = OBSERVACIONES;
            _formacionParaLista.REGISTRO_ENTREVISTA = 0;
            _formacionParaLista.TIPO_EDUCACION = TIPO_EDUCACION;
            _formacionParaLista.UNIDAD_DURACION = null;

            listaFormacionAcademica.Add(_formacionParaLista);
        }

        for (int i = 0; i < GridView_EducacionNoFormal.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_EducacionNoFormal.Rows[i];

            String TIPO_EDUCACION = "NO FORMAL";

            Decimal ID_INFO_ACADEMICA = Convert.ToDecimal(GridView_EducacionNoFormal.DataKeys[i].Values["ID_INFO_ACADEMICA"]);

            TextBox textoCurso = filaGrilla.FindControl("TextBox_Curso") as TextBox;
            String CURSO = textoCurso.Text;

            TextBox textoInstitucion = filaGrilla.FindControl("TextBox_Institucion") as TextBox;
            String INSTITUCION = textoInstitucion.Text;

            Decimal DURACION = 0;
            TextBox textoDuracion = filaGrilla.FindControl("TextBox_Duracion") as TextBox;
            if (String.IsNullOrEmpty(textoDuracion.Text) == false)
            {
                DURACION = Convert.ToDecimal(textoDuracion.Text);
            }
            else
            {
                DURACION = 0;
            }

            DropDownList dropUnidadDuracion = filaGrilla.FindControl("DropDownList_UnidadDuracion") as DropDownList;
            String UNIDAD_DURACION = dropUnidadDuracion.SelectedValue;

            TextBox textoobservaciones = filaGrilla.FindControl("TextBox_Observaciones") as TextBox;
            String OBSERVACIONES = textoobservaciones.Text;

            FormacionAcademica _formacionParaLista = new FormacionAcademica();

            _formacionParaLista.ACTIVO = true;
            _formacionParaLista.ANNO = 0;
            _formacionParaLista.CURSO = CURSO;
            _formacionParaLista.DURACION = DURACION;
            _formacionParaLista.ID_INFO_ACADEMICA = ID_INFO_ACADEMICA;
            _formacionParaLista.INSTITUCION = INSTITUCION;
            _formacionParaLista.NIVEL_ACADEMICO = null;
            _formacionParaLista.OBSERVACIONES = OBSERVACIONES;
            _formacionParaLista.REGISTRO_ENTREVISTA = 0;
            _formacionParaLista.TIPO_EDUCACION = TIPO_EDUCACION;
            _formacionParaLista.UNIDAD_DURACION = UNIDAD_DURACION;

            listaFormacionAcademica.Add(_formacionParaLista);

        }


        List<ExperienciaLaboral> listaExperiencia = new List<ExperienciaLaboral>();
        for (int i = 0; i < GridView_ExperienciaLaboral.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ExperienciaLaboral.Rows[i];

            Decimal ID_EXPERIENCIA = Convert.ToDecimal(GridView_ExperienciaLaboral.DataKeys[i].Values["ID_EXPERIENCIA"]);

            TextBox textoEmpresa = filaGrilla.FindControl("TextBox_Empresa") as TextBox;
            String EMPRESA = textoEmpresa.Text;

            TextBox textoCargo = filaGrilla.FindControl("TextBox_Cargo") as TextBox;
            String CARGO = textoCargo.Text;

            TextBox textoFunciones = filaGrilla.FindControl("TextBox_FuncionesRealizadas") as TextBox;
            String FUNCIONES = textoFunciones.Text;

            DateTime FECHA_INGRESO;
            TextBox textoFechaIngreso = filaGrilla.FindControl("TextBox_FechaIngreso") as TextBox;
            try
            {
                FECHA_INGRESO = Convert.ToDateTime(textoFechaIngreso.Text);
            }
            catch
            { 
                FECHA_INGRESO = new DateTime();
            }

            DateTime FECHA_RETIRO;
            TextBox textoFechaRetiro = filaGrilla.FindControl("TextBox_FechaRetiro") as TextBox;
            try
            {
                FECHA_RETIRO = Convert.ToDateTime(textoFechaRetiro.Text);
            }
            catch
            {
                FECHA_RETIRO = new DateTime();
            }

            DropDownList dropMotivoRetiro = filaGrilla.FindControl("DropDownList_MotivoRetiro") as DropDownList;
            String MOTIVO_RETIRO = null;
            if (String.IsNullOrEmpty(dropMotivoRetiro.SelectedValue) == false)
            {
                MOTIVO_RETIRO = dropMotivoRetiro.SelectedValue;
            }
            
            Decimal ULTIMO_SALARIO = 0;
            TextBox textoUltimoSalario = filaGrilla.FindControl("TextBox_Ultimosalario") as TextBox;
            try
            {
                ULTIMO_SALARIO = Convert.ToDecimal(textoUltimoSalario.Text);
            }
            catch
            {
                ULTIMO_SALARIO = 0;
            }

            ExperienciaLaboral _experienciaParaLista = new ExperienciaLaboral();

            _experienciaParaLista.ACTIVO = true;
            _experienciaParaLista.CARGO = CARGO;
            _experienciaParaLista.EMPRESA_CLIENTE = EMPRESA;
            _experienciaParaLista.FECHA_INGRESO = FECHA_INGRESO;
            _experienciaParaLista.FECHA_RETIRO = FECHA_RETIRO;
            _experienciaParaLista.FUNCIONES = FUNCIONES;
            _experienciaParaLista.ID_EXPERIENCIA = ID_EXPERIENCIA;
            _experienciaParaLista.MOTIVO_RETIRO = MOTIVO_RETIRO;
            _experienciaParaLista.REGISTRO_ENTREVISTA = 0;
            _experienciaParaLista.ULTIMO_SALARIO = ULTIMO_SALARIO;

            listaExperiencia.Add(_experienciaParaLista);
        }

        List<ComposicionFamiliar> listaComposicionFamiliar = new List<ComposicionFamiliar>();
        for (int i = 0; i < GridView_ComposicionFamiliar.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ComposicionFamiliar.Rows[i];

            Decimal ID_COMPOSICION = Convert.ToDecimal(GridView_ComposicionFamiliar.DataKeys[i].Values["ID_COMPOSICION"]);

            DropDownList dropTipoFamiliar = filaGrilla.FindControl("DropDownList_TipoFamiliar") as DropDownList;
            String ID_TIPO_FAMILIAR = dropTipoFamiliar.SelectedValue;

            TextBox textoNombres = filaGrilla.FindControl("TextBox_NombresFamiliar") as TextBox;
            String NOMBRES_FAMILIAR = textoNombres.Text;

            TextBox textoApellidos = filaGrilla.FindControl("TextBox_ApellidosFamiliar") as TextBox;
            String APELLIDOS_FAMILIAR = textoApellidos.Text;

            DateTime FECHA_NACIMIENTO = new DateTime();
            TextBox textoFechaNacimiento = filaGrilla.FindControl("TextBox_FechaNacimientoFamiliar") as TextBox;
            try
            {
                FECHA_NACIMIENTO = Convert.ToDateTime(textoFechaNacimiento.Text);
            }
            catch
            {
                FECHA_NACIMIENTO = new DateTime();
            }

            TextBox textoProfesion = filaGrilla.FindControl("TextBox_ProfesionFamiliar") as TextBox;
            String PROFESION = textoProfesion.Text;

            CheckBox checkExtrajero = filaGrilla.FindControl("") as CheckBox;
            DropDownList dropCiudad = filaGrilla.FindControl("DropDownList_CiudadFamiliar") as DropDownList;
            String ID_CIUDAD = dropCiudad.SelectedValue;

            Boolean VIVE_CON_EL = false;
            CheckBox checkViveConEl = filaGrilla.FindControl("CheckBox_ViveConElFamiliar") as CheckBox;
            if (checkViveConEl.Checked == true)
            {
                VIVE_CON_EL = true;
            }

            Boolean ACTIVO = true;

            ComposicionFamiliar _composicionParaLista = new ComposicionFamiliar();

            _composicionParaLista.ACTIVO = ACTIVO;
            _composicionParaLista.APELLIDOS = APELLIDOS_FAMILIAR;
            _composicionParaLista.FECHA_NACIMIENTO = FECHA_NACIMIENTO;
            _composicionParaLista.ID_CIUDAD = ID_CIUDAD;
            _composicionParaLista.ID_COMPOSICION = ID_COMPOSICION;
            _composicionParaLista.ID_TIPO_FAMILIAR = ID_TIPO_FAMILIAR;
            _composicionParaLista.NOMBRES = NOMBRES_FAMILIAR;
            _composicionParaLista.PROFESION = PROFESION;
            _composicionParaLista.REGISTRO_ENTREVISTA = 0;
            _composicionParaLista.VIVE_CON_EL = VIVE_CON_EL;

            listaComposicionFamiliar.Add(_composicionParaLista);
        }


        String RH = null;
        if (String.IsNullOrEmpty(DropDownList_RH.SelectedValue) == false)
        {
            RH = DropDownList_RH.SelectedValue;
        }

        Dictionary<String, String> listaCamposValidarRestricciones = new Dictionary<String, String>();
        listaCamposValidarRestricciones.Add("APELLIDOS", APELLIDOS);
        listaCamposValidarRestricciones.Add("NOMBRES", NOMBRES);
        listaCamposValidarRestricciones.Add("DIRECCIÓN", DIR_ASPIRANTE);
        listaCamposValidarRestricciones.Add("BARRIO", SECTOR);
        listaCamposValidarRestricciones.Add("TELÉFONO", TEL_ASPIRANTE);
        listaCamposValidarRestricciones.Add("E-MAIL", E_MAIL);
        listaCamposValidarRestricciones.Add("CELULAR", CEL_ASPIRANTE);

        CrtRestriccionPalabra _restricciones = new CrtRestriccionPalabra(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        
        _restricciones.listaPalabrasEntrada = listaCamposValidarRestricciones;
        _restricciones.ComprobarListaPalabras();

        if (_restricciones.listaPalabrasSalida.Count <= 0)
        {
            DataTable tablaComprobacion = _rad.ObtenerRegSolicitudesingresoPorNumDocIdentidad(NUM_DOC_IDENTIDAD);

            if (tablaComprobacion.Rows.Count > 0)
            {
                DataRow filaComprobacion = tablaComprobacion.Rows[0];

                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El número de documento " + NUM_DOC_IDENTIDAD + " ya se encuentra registrado en la Base de Datos.<br>Fecha Registro: " + filaComprobacion["FECHA_R"].ToString().Trim() + ", Usuario: " + filaComprobacion["USU_CRE"].ToString().Trim() + ", Nombre Candidato: " + filaComprobacion["NOMBRES"].ToString().Trim() + " " + filaComprobacion["APELLIDOS"].ToString().Trim() + ".<BR>NO SE PUDO CREAR EL REGISTRO.", Proceso.Advertencia);
            }
            else
            {
                Decimal ID_SOLICITUD = _rad.AdicionarRegSolicitudesingreso(DateTime.Now, APELLIDOS, NOMBRES, TIP_DOC_IDENTIDAD, NUM_DOC_IDENTIDAD, CIU_CEDULA, LIB_MILITAR, CAT_LIC_COND, DIR_ASPIRANTE, SECTOR, CIU_ASPIRANTE, TEL_ASPIRANTE, SEXO, FCH_NACIMIENTO, ID_GRUPOS_PRIMARIOS, ID_FUENTE, CONDUCTO, NIV_EDUCACION, E_MAIL, ID_AREASINTERES, ASPIRACION_SALARIAL, EXPERIENCIA, ID_OCUPACION, NUCLEO_FORMACION, TALLA_CAMISA, TALLA_PANTALON, TALLA_ZAPATOS, ESTRATO, NRO_HIJOS, C_FMLIA, CEL_ASPIRANTE, ESTADO_CIVIL, ID_PAIS, TIPO_VIVIENDA, FUENTE_CONOCIMIENTO, listaFormacionAcademica, listaExperiencia, listaComposicionFamiliar, RH);

                if (ID_SOLICITUD <= 0)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _rad.MensajeError, Proceso.Error);
                }
                else
                {
                    Ocultar(Acciones.Inicio);
                    Desactivar(Acciones.Inicio);
                    Mostrar(Acciones.Cargar);

                    Cargar(ID_SOLICITUD);

                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La Solicitud de ingreso de " + NOMBRES + " " + APELLIDOS + " fue registrada correctamente.", Proceso.Correcto);
                }
            }
        }
        else
        {
            Int32 contador = 0;
            String mensaje = "";
            foreach (KeyValuePair<String, String> restriccion in _restricciones.listaPalabrasSalida)
            {
                if (contador <= 0)
                {
                    mensaje = restriccion.Value;
                }
                else
                {
                    mensaje += "</br>" + restriccion.Value;
                }

                contador += 1;
            }

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, mensaje, Proceso.Advertencia);
        }
    }

    private void CargarControlRegistros(DataRow filaTablaInfoSolicitud)
    {
        Panel_CONTROL_REGISTRO.Visible = true;
        Panel_CONTROL_REGISTRO.Enabled = false;

        TextBox_USU_CRE.Text = filaTablaInfoSolicitud["USU_CRE"].ToString();
        try
        {
            TextBox_FCH_CRE.Text = DateTime.Parse(filaTablaInfoSolicitud["FCH_CRE"].ToString()).ToShortDateString();
            TextBox_HOR_CRE.Text = DateTime.Parse(filaTablaInfoSolicitud["FCH_CRE"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_CRE.Text = "";
            TextBox_HOR_CRE.Text = "";
        }
        TextBox_USU_MOD.Text = filaTablaInfoSolicitud["USU_MOD"].ToString();
        try
        {
            TextBox_FCH_MOD.Text = DateTime.Parse(filaTablaInfoSolicitud["FCH_MOD"].ToString()).ToShortDateString();
            TextBox_HOR_MOD.Text = DateTime.Parse(filaTablaInfoSolicitud["FCH_MOD"].ToString()).ToShortTimeString();
        }
        catch
        {
            TextBox_FCH_MOD.Text = "";
            TextBox_HOR_MOD.Text = "";
        }
    }

    private DataRow obtenerIdDepartamentoIdCiudad(String idCiudad)
    {
        DataRow resultado = null;

        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());

        DataTable tablaIdDepartamentoIdCiudad = _ciudad.ObtenerIdDepartamentoConIdCiudad(idCiudad);

        if (tablaIdDepartamentoIdCiudad.Rows.Count > 0)
        {
            resultado = tablaIdDepartamentoIdCiudad.Rows[0];
        }

        return resultado;
    }

    private void CargarDatosBasicos(DataRow filaSolicitud)
    {
        Label_FechaIngreso.Text = Convert.ToDateTime(filaSolicitud["FECHA_R"].ToString().Trim()).ToShortDateString();

        if (filaSolicitud["ARCHIVO"].ToString().Trim().ToUpper() == "EN CLIENTE")
        {
            Label_EstadoAspirante.Text = "EN CLIENTE (Req: " + filaSolicitud["ID_REQUERIMIENTO"].ToString().Trim() + ")";
            Button_DevolverEnCliente.Visible = true;
        }
        else
        {
            Label_EstadoAspirante.Text = filaSolicitud["ARCHIVO"].ToString().Trim();
        }

        try
        {
            TextBox_FCH_NACIMIENTO.Text = Convert.ToDateTime(filaSolicitud["FCH_NACIMIENTO"]).ToShortDateString();
        }
        catch
        {
            TextBox_FCH_NACIMIENTO.Text = "";
        }
        

        cargar_DropDownList_PaisNacimiento();
        try { DropDownList_PaisNacimiento.SelectedValue = filaSolicitud["ID_PAIS"].ToString(); }
        catch { DropDownList_PaisNacimiento.SelectedIndex = 0; }
        if (DropDownList_PaisNacimiento.SelectedValue != "170")
        {
            RFV_DropDownList_CIU_CEDULA.Enabled = false;
            ValidatorCalloutExtender_DropDownList_CIU_CEDULA.Enabled = false;
        }

        cargar_DropDownList_SEXO();
        try { DropDownList_SEXO.SelectedValue = filaSolicitud["SEXO"].ToString(); }
        catch { DropDownList_SEXO.SelectedIndex = 0; }
        if (DropDownList_SEXO.SelectedValue != "M")
        {
            RequiredFieldValidator_TextBox_LIB_MILITAR.Enabled = false;
            ValidatorCalloutExtender_TextBox_LIB_MILITAR.Enabled = false;
        }

        cargar_DropDownList_RH();
        try { DropDownList_RH.SelectedValue = filaSolicitud["FACTOR_RH"].ToString(); }
        catch { DropDownList_RH.ClearSelection(); }

        cargamos_DropDownList_TIP_DOC_IDENTIDAD();
        try { DropDownList_TIP_DOC_IDENTIDAD.SelectedValue = filaSolicitud["TIP_DOC_IDENTIDAD"].ToString(); }
        catch { DropDownList_TIP_DOC_IDENTIDAD.SelectedIndex = 0; }

        TextBox_NUM_DOC_IDENTIDAD.Text = filaSolicitud["NUM_DOC_IDENTIDAD"].ToString().Trim();

        cargar_DropDownList_DEPARTAMENTO_CEDULA();
        Cargar_DropDownListVacio(DropDownList_CIU_CEDULA);
        if(DBNull.Value.Equals(filaSolicitud["CIU_CEDULA"]) == false)
        {
            DataRow filaInfoCiudadYDepartamento = obtenerIdDepartamentoIdCiudad(filaSolicitud["CIU_CEDULA"].ToString().Trim());
            if (filaInfoCiudadYDepartamento != null)
            {
                try 
                { 
                    DropDownList_DEPARTAMENTO_CEDULA.SelectedValue = filaInfoCiudadYDepartamento["ID_DEPARTAMENTO"].ToString().Trim(); 

                    cargar_DropDownList_CIU_CEDULA(filaInfoCiudadYDepartamento["ID_DEPARTAMENTO"].ToString().Trim());
                    DropDownList_CIU_CEDULA.SelectedValue = filaInfoCiudadYDepartamento["ID_CIUDAD"].ToString().Trim();
                }
                catch 
                { 
                    DropDownList_DEPARTAMENTO_CEDULA.SelectedIndex = 0; 
                    DropDownList_CIU_CEDULA.SelectedIndex = 0;
                }
            }
        }

        TextBox_NOMBRES.Text = filaSolicitud["NOMBRES"].ToString().Trim().ToUpper();
        TextBox_APELLIDOS.Text = filaSolicitud["APELLIDOS"].ToString().Trim().ToUpper();

        TextBox_LIB_MILITAR.Text = filaSolicitud["LIB_MILITAR"].ToString().Trim();

        Cargar_DropDownList_CAT_LIC_COND();
        try { DropDownList_CAT_LIC_COND.SelectedValue = filaSolicitud["CAT_LIC_COND"].ToString(); }
        catch { DropDownList_CAT_LIC_COND.SelectedIndex = 0; }

        TextBox_TEL_ASPIRANTE.Text = filaSolicitud["TEL_ASPIRANTE"].ToString().Trim();
        TextBox_CEL_ASPIRANTE.Text = filaSolicitud["CEL_ASPIRANTE"].ToString().Trim();

        TextBox_E_MAIL.Text = filaSolicitud["E_MAIL"].ToString().Trim();

    }

    private void CargarUbicacion(DataRow filaSolicitud)
    {
        cargar_DropDownList_DEPARTAMENTO_ASPIRANTE();
        Cargar_DropDownListVacio(DropDownList_CIU_ASPIRANTE);
        if (DBNull.Value.Equals(filaSolicitud["CIU_ASPIRANTE"]) == false)
        {
            DataRow filaInfoCiudadYDepartamento = obtenerIdDepartamentoIdCiudad(filaSolicitud["CIU_ASPIRANTE"].ToString().Trim());
            if (filaInfoCiudadYDepartamento != null)
            {
                try
                {
                    DropDownList_DEPARTAMENTO_ASPIRANTE.SelectedValue = filaInfoCiudadYDepartamento["ID_DEPARTAMENTO"].ToString().Trim();

                    cargar_DropDownList_CIU_ASPIRANTE(filaInfoCiudadYDepartamento["ID_DEPARTAMENTO"].ToString().Trim());
                    DropDownList_CIU_ASPIRANTE.SelectedValue = filaInfoCiudadYDepartamento["ID_CIUDAD"].ToString().Trim();
                }
                catch
                {
                    DropDownList_DEPARTAMENTO_ASPIRANTE.SelectedIndex = 0;
                    DropDownList_CIU_ASPIRANTE.SelectedIndex = 0;
                }
            }
        }

        TextBox_DIR_ASPIRANTE.Text = filaSolicitud["DIR_ASPIRANTE"].ToString().Trim();

        TextBox_SECTOR.Text = filaSolicitud["SECTOR"].ToString().Trim();

        cargar_DropDownList_TipoVivienda();
        try
        {
            DropDownList_TipoVivienda.SelectedValue = filaSolicitud["TIPO_VIVIENDA"].ToString().Trim();
        }
        catch
        {
            DropDownList_TipoVivienda.SelectedIndex = 0;
        }

        try
        {
            DropDownList_ESTRATO.SelectedValue = filaSolicitud["ESTRATO"].ToString().Trim();
        }
        catch
        {
            DropDownList_ESTRATO.SelectedIndex = 0;
        }
    }

    private void CargarComposicionFamiliar(Decimal ID_ENTREVISTA)
    {
        hojasVida _hojasVida = new hojasVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablainfofamiliar = _hojasVida.ObtenerSelRegComposicionFamiliar(ID_ENTREVISTA);

        if (tablainfofamiliar.Rows.Count <= 0)
        {
            if (_hojasVida.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _hojasVida.MensajeError, Proceso.Error);
            }

            GridView_ComposicionFamiliar.DataSource = null;
            GridView_ComposicionFamiliar.DataBind();
        }
        else
        {
            Cargar_Grilla_Composicionfamiliar_Desdetabla(tablainfofamiliar);
        }
    }

    private void CargarEducacionFormal(Decimal ID_ENTREVISTA)
    {
        hojasVida _hojasVida = new hojasVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaEducacion = _hojasVida.ObtenerSelRegInformacionAcademica(ID_ENTREVISTA, "FORMAL");

        if (tablaEducacion.Rows.Count <= 0)
        {
            if (_hojasVida.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _hojasVida.MensajeError, Proceso.Error);
            }

            GridView_EducacionFormal.DataSource = null;
            GridView_EducacionFormal.DataBind();
        }
        else
        {
            Cargar_Grilla_informacionEducativaFormal_Desdetabla(tablaEducacion);
        }
    }

    private void CargarEducacionNoFormal(Decimal ID_ENTREVISTA)
    {
        hojasVida _hojasVida = new hojasVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaEducacion = _hojasVida.ObtenerSelRegInformacionAcademica(ID_ENTREVISTA, "NO FORMAL");

        if (tablaEducacion.Rows.Count <= 0)
        {
            if (_hojasVida.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _hojasVida.MensajeError, Proceso.Error);
            }

            GridView_EducacionNoFormal.DataSource = null;
            GridView_EducacionNoFormal.DataBind();
        }
        else
        {
            Cargar_Grilla_informacionEducativaNoFormal_Desdetabla(tablaEducacion);
        }
    }

    private void CargarEducacion(DataRow filaSolicitud, Decimal ID_ENTREVISTA)
    {
        cargar_DropDownList_NIV_EDUCACION();
        try { DropDownList_NIV_EDUCACION.SelectedValue = filaSolicitud["NIV_EDUCACION"].ToString().Trim(); }
        catch { DropDownList_NIV_EDUCACION.SelectedIndex = 0; }

        cargar_DropDownList_NUCLEO_FORMACION();
        try { DropDownList_nucleo_formacion.SelectedValue = filaSolicitud["ID_NUCLEO_FORMACION"].ToString().Trim(); }
        catch { DropDownList_nucleo_formacion.SelectedIndex = 0; }

        HiddenField_ACCION_GRILLA_INFOACADEMMICA_EF.Value = AccionesGrilla.Ninguna.ToString();
        HiddenField_ACCION_GRILLA_INFOACADEMMIC_ENF.Value = AccionesGrilla.Ninguna.ToString();

        GridView_EducacionFormal.DataSource = null;
        GridView_EducacionNoFormal.DataSource = null;

        if (ID_ENTREVISTA <= 0)
        {
            GridView_EducacionFormal.DataSource = null;
            GridView_EducacionFormal.DataBind();
        }
        else
        {
            CargarEducacionFormal(ID_ENTREVISTA);
            CargarEducacionNoFormal(ID_ENTREVISTA);   
        }
    }

    private void Cargar_DropDownList_ID_OCUPACION_SoloUno(Decimal ID_OCUPACION)
    {
        cargo _cargo = new cargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaCargo = _cargo.ObtenerOcupacionPorIdOcupacion(ID_OCUPACION);

        DropDownList_ID_OCUPACION.Items.Clear();

        DropDownList_ID_OCUPACION.Items.Add(new ListItem("Seleccione...",""));

        if (tablaCargo.Rows.Count <= 0)
        {
            if (_cargo.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _cargo.MensajeError, Proceso.Error);
            }
        }
        else
        {
            DataRow filaOcupacion = tablaCargo.Rows[0];

            DropDownList_ID_OCUPACION.Items.Add(new ListItem(filaOcupacion["NOM_OCUPACION"].ToString(),ID_OCUPACION.ToString()));
        }

        DropDownList_ID_OCUPACION.DataBind();
    }

    private void CargarExperienciaLaboral(Decimal ID_ENTREVISTA)
    {
        hojasVida _hojasVida = new hojasVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaExperiencia = _hojasVida.ObtenerSelRegExperienciaLaboral(ID_ENTREVISTA);

        if (tablaExperiencia.Rows.Count <= 0)
        {
            if (_hojasVida.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _hojasVida.MensajeError, Proceso.Error);
            }

            GridView_ExperienciaLaboral.DataSource = null;
            GridView_ExperienciaLaboral.DataBind();
        }
        else
        {
            Cargar_Grilla_ExperienciaLaboral_Desdetabla(tablaExperiencia);
        }
    }

    private void CargarDatosLaborales(DataRow filaSolicitud, Decimal ID_ENTREVISTA)
    {
        TextBox_BUSCADOR_CARGO.Text = "";

        Decimal ID_OCUPACION = 0;
        try
        {
            ID_OCUPACION = Convert.ToDecimal(filaSolicitud["ID_OCUPACION"]);
        }
        catch
        {
            ID_OCUPACION = 0;
        }

        if(ID_OCUPACION == 0)
        {
            Cargar_DropDownListVacio(DropDownList_ID_OCUPACION);
        }
        else
        {
            Cargar_DropDownList_ID_OCUPACION_SoloUno(ID_OCUPACION);
            try { DropDownList_ID_OCUPACION.SelectedValue = ID_OCUPACION.ToString(); }
            catch { DropDownList_ID_OCUPACION.SelectedIndex = 0; }
        }

        cargar_DropDownList_EXPERIENCIA();
        try { DropDownList_EXPERIENCIA.SelectedValue = filaSolicitud["EXPERIENCIA"].ToString(); }
        catch { DropDownList_EXPERIENCIA.SelectedIndex = 0; }

        try
        {
            TextBox_ASPIRACION_SALARIAL.Text = Convert.ToDecimal(filaSolicitud["ASPIRACION_SALARIAL"]).ToString();
        }
        catch
        {
            TextBox_ASPIRACION_SALARIAL.Text = "";
        }

        cargar_DropDownList_AREAS_ESPECIALIZACION();
        try { DropDownList_AREAS_ESPECIALIZACION.SelectedValue = filaSolicitud["ID_AREAINTERES"].ToString(); }
        catch { DropDownList_AREAS_ESPECIALIZACION.SelectedIndex = 0; }

        HiddenField_ACCION_GRILLA_EXPLABORAL.Value = AccionesGrilla.Ninguna.ToString();

        if (ID_ENTREVISTA <= 0)
        {
            GridView_ExperienciaLaboral.DataSource = null;
        }
        else
        {
            CargarExperienciaLaboral(ID_ENTREVISTA);
        }
    }

    private void CargarFamilia(DataRow filaSolicitud, Decimal ID_ENTREVISTA)
    {
        cargamos_DropDownList_ESTADO_CIVIL();
        try { DropDownList_ESTADO_CIVIL.SelectedValue = filaSolicitud["ESTADO_CIVIL"].ToString().Trim(); }
        catch { DropDownList_ESTADO_CIVIL.SelectedIndex = 0; }

        Cargar_DropDownList_CabezaFamilia();
        try { DropDownList_CabezaFamilia.SelectedValue = filaSolicitud["C_FMLIA"].ToString(); }
        catch { DropDownList_CabezaFamilia.SelectedIndex = 0; }

        HiddenField_ACCION_GRILLA_COMPOSICIONFAMILIAR.Value = AccionesGrilla.Ninguna.ToString();

        if (ID_ENTREVISTA <= 0)
        {
            GridView_ComposicionFamiliar.DataSource = null;
        }
        else
        {
            CargarComposicionFamiliar(ID_ENTREVISTA);
        }

        ObtenerNumeroDeHijos();
    }

    private void CargardatosAdicionales(DataRow filaSolicitud)
    {
        cargar_DropDownList_TALLAS_CAMISA();
        try { DropDownList_Talla_Camisa.SelectedValue = filaSolicitud["TALLA_CAMISA"].ToString(); }
        catch { DropDownList_Talla_Camisa.SelectedValue = ""; }

        cargar_DropDownList_TALLAS_PANTALON();
        try { DropDownList_Talla_Pantalon.SelectedValue = filaSolicitud["TALLA_PANTALON"].ToString(); }
        catch { DropDownList_Talla_Pantalon.SelectedValue = ""; }

        cargar_DropDownList_TALLAS_ZAPATOS();
        try { DropDownList_talla_zapatos.SelectedValue = filaSolicitud["TALLA_ZAPATOS"].ToString(); }
        catch { DropDownList_talla_zapatos.SelectedValue = ""; }

        cargar_DropDownList_ID_FUENTE();
        try { DropDownList_ID_FUENTE.SelectedValue = filaSolicitud["ID_FUENTE"].ToString().Trim(); }
        catch { DropDownList_ID_FUENTE.SelectedIndex = 0; }

        Cargar_DropDownList_ComoSeEntero();
        try { DropDownList_ComoSeEntero.SelectedValue = filaSolicitud["FUENTE_CONOCIMIENTO"].ToString(); }
        catch { DropDownList_ComoSeEntero.SelectedIndex = 0; }

    }

    private void Cargar(Decimal ID_SOLICITUD)
    {
        HiddenField_ID_SOLICITUD.Value = ID_SOLICITUD.ToString();

        Decimal ID_ENTREVISTA = 0;

        radicacionHojasDeVida _rad = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaSolicitud = _rad.ObtenerRegSolicitudesingresoPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));
        DataRow filaSolicitud = tablaSolicitud.Rows[0];

        hojasVida _hoja = new hojasVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaEntrevista = _hoja.ObtenerSelRegEntrevistasPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));

        if(tablaEntrevista.Rows.Count > 0)
        {
            DataRow filaEntrevista = tablaEntrevista.Rows[0];

            ID_ENTREVISTA = Convert.ToDecimal(filaEntrevista["REGISTRO"]);
        }

        CargarControlRegistros(filaSolicitud);

        CargarDatosBasicos(filaSolicitud);

        CargarUbicacion(filaSolicitud);

        CargarEducacion(filaSolicitud, ID_ENTREVISTA);
        inhabilitarFilasGrilla(GridView_EducacionFormal, 2);
        inhabilitarFilasGrilla(GridView_EducacionNoFormal, 2);

        CargarDatosLaborales(filaSolicitud, ID_ENTREVISTA);
        inhabilitarFilasGrilla(GridView_ExperienciaLaboral, 2);

        CargarFamilia(filaSolicitud, ID_ENTREVISTA);
        inhabilitarFilasGrilla(GridView_ComposicionFamiliar, 2);

        CargardatosAdicionales(filaSolicitud);

        ComprobarTodasLasPestanas(false);
        HiddenField_IndexTab.Value = "0";
        SetActivoTab(0);
    }

    private void Actualizar()
    {
        radicacionHojasDeVida _rad = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        Decimal ID_SOLICITUD = Convert.ToDecimal(HiddenField_ID_SOLICITUD.Value);

        String APELLIDOS = TextBox_APELLIDOS.Text.ToUpper();
        String NOMBRES = TextBox_NOMBRES.Text.ToUpper();
        String TIP_DOC_IDENTIDAD = DropDownList_TIP_DOC_IDENTIDAD.SelectedValue;
        String NUM_DOC_IDENTIDAD = TextBox_NUM_DOC_IDENTIDAD.Text.Trim();

        String CIU_CEDULA = null;
        if (DropDownList_CIU_CEDULA.SelectedIndex > 0)
        {
            CIU_CEDULA = DropDownList_CIU_CEDULA.SelectedValue;
        }

        String LIB_MILITAR = null;
        if (String.IsNullOrEmpty(TextBox_LIB_MILITAR.Text) == false)
        {
            LIB_MILITAR = TextBox_LIB_MILITAR.Text;
        }

        String CAT_LIC_COND = null;
        if (DropDownList_CAT_LIC_COND.SelectedIndex > 0)
        {
            CAT_LIC_COND = DropDownList_CAT_LIC_COND.SelectedValue;
        }

        String DIR_ASPIRANTE = TextBox_DIR_ASPIRANTE.Text.Trim().ToUpper();
        String SECTOR = TextBox_SECTOR.Text.Trim().ToUpper();
        String CIU_ASPIRANTE = DropDownList_CIU_ASPIRANTE.SelectedValue;

        String TEL_ASPIRANTE = null;
        if (String.IsNullOrEmpty(TextBox_TEL_ASPIRANTE.Text) == false)
        {
            TEL_ASPIRANTE = TextBox_TEL_ASPIRANTE.Text;
        }

        String SEXO = DropDownList_SEXO.SelectedValue;
        DateTime FCH_NACIMIENTO = Convert.ToDateTime(TextBox_FCH_NACIMIENTO.Text);
        int ID_FUENTE = Convert.ToInt32(DropDownList_ID_FUENTE.SelectedValue);
        String CONDUCTO = null;
        int NIV_EDUCACION = Convert.ToInt32(DropDownList_NIV_EDUCACION.SelectedValue);
        String E_MAIL = TextBox_E_MAIL.Text.Trim();
        int ID_AREASINTERES = Convert.ToInt32(DropDownList_AREAS_ESPECIALIZACION.SelectedValue);
        Decimal ASPIRACION_SALARIAL = Convert.ToDecimal(TextBox_ASPIRACION_SALARIAL.Text);
        String EXPERIENCIA = DropDownList_EXPERIENCIA.SelectedValue;

        String ID_GRUPOS_PRIMARIOS = null;
        Decimal ID_OCUPACION = Convert.ToDecimal(DropDownList_ID_OCUPACION.SelectedValue);
        cargo _cargo = new cargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoCargo = _cargo.ObtenerOcupacionPorIdOcupacion(ID_OCUPACION);
        try
        {
            DataRow filaCargo = tablaInfoCargo.Rows[0];
            ID_GRUPOS_PRIMARIOS = filaCargo["COD_OCUPACION"].ToString().Trim();
        }
        catch
        {
            ID_GRUPOS_PRIMARIOS = null;
        }

        String NUCLEO_FORMACION = DropDownList_nucleo_formacion.SelectedValue;
        String TALLA_CAMISA = DropDownList_Talla_Camisa.SelectedValue;
        String TALLA_PANTALON = DropDownList_Talla_Pantalon.SelectedValue;
        String TALLA_ZAPATOS = DropDownList_talla_zapatos.SelectedValue;
        int ESTRATO = Convert.ToInt32(DropDownList_ESTRATO.SelectedValue);
        int NRO_HIJOS = Convert.ToInt32(TextBox_NUM_HIJOS.Text);

        Boolean C_FMLIA = false;
        if (DropDownList_CabezaFamilia.SelectedValue == "S")
        {
            C_FMLIA = true;
        }

        String CEL_ASPIRANTE = null;
        if (String.IsNullOrEmpty(TextBox_CEL_ASPIRANTE.Text) == false)
        {
            CEL_ASPIRANTE = TextBox_CEL_ASPIRANTE.Text.Trim();
        }

        String ESTADO_CIVIL = DropDownList_ESTADO_CIVIL.SelectedValue;
        Int32 ID_PAIS = Convert.ToInt32(DropDownList_PaisNacimiento.SelectedValue);

        String TIPO_VIVIENDA = DropDownList_TipoVivienda.SelectedValue;

        String FUENTE_CONOCIMIENTO = DropDownList_ComoSeEntero.SelectedValue;

        List<FormacionAcademica> listaFormacionAcademica = new List<FormacionAcademica>();
        for (int i = 0; i < GridView_EducacionFormal.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_EducacionFormal.Rows[i];

            String TIPO_EDUCACION = "FORMAL";

            Decimal ID_INFO_ACADEMICA = Convert.ToDecimal(GridView_EducacionFormal.DataKeys[i].Values["ID_INFO_ACADEMICA"]);

            DropDownList dropnivAcademico = filaGrilla.FindControl("DropDownList_NivAcademico") as DropDownList;
            String NIVEL_ACADEMICO = dropnivAcademico.SelectedValue;

            TextBox textoInstitucion = filaGrilla.FindControl("TextBox_Institucion") as TextBox;
            String INSTITUCION = textoInstitucion.Text;

            Int32 ANNO = 0;
            TextBox textoAnno = filaGrilla.FindControl("TextBox_Anno") as TextBox;
            if (String.IsNullOrEmpty(textoAnno.Text) == false)
            {
                ANNO = Convert.ToInt32(textoAnno.Text);
            }
            else
            {
                ANNO = 0;
            }

            TextBox textoObservaciones = filaGrilla.FindControl("TextBox_Observaciones") as TextBox;
            String OBSERVACIONES = textoObservaciones.Text;

            FormacionAcademica _formacionParaLista = new FormacionAcademica();

            _formacionParaLista.ACTIVO = true;
            _formacionParaLista.ANNO = ANNO;
            _formacionParaLista.CURSO = null;
            _formacionParaLista.DURACION = 0;
            _formacionParaLista.ID_INFO_ACADEMICA = ID_INFO_ACADEMICA;
            _formacionParaLista.INSTITUCION = INSTITUCION;
            _formacionParaLista.NIVEL_ACADEMICO = NIVEL_ACADEMICO;
            _formacionParaLista.OBSERVACIONES = OBSERVACIONES;
            _formacionParaLista.REGISTRO_ENTREVISTA = 0;
            _formacionParaLista.TIPO_EDUCACION = TIPO_EDUCACION;
            _formacionParaLista.UNIDAD_DURACION = null;

            listaFormacionAcademica.Add(_formacionParaLista);
        }

        for (int i = 0; i < GridView_EducacionNoFormal.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_EducacionNoFormal.Rows[i];

            String TIPO_EDUCACION = "NO FORMAL";

            Decimal ID_INFO_ACADEMICA = Convert.ToDecimal(GridView_EducacionNoFormal.DataKeys[i].Values["ID_INFO_ACADEMICA"]);

            TextBox textoCurso = filaGrilla.FindControl("TextBox_Curso") as TextBox;
            String CURSO = textoCurso.Text;

            TextBox textoInstitucion = filaGrilla.FindControl("TextBox_Institucion") as TextBox;
            String INSTITUCION = textoInstitucion.Text;

            Decimal DURACION = 0;
            TextBox textoDuracion = filaGrilla.FindControl("TextBox_Duracion") as TextBox;
            if (String.IsNullOrEmpty(textoDuracion.Text) == false)
            {
                DURACION = Convert.ToDecimal(textoDuracion.Text);
            }
            else
            {
                DURACION = 0;
            }

            DropDownList dropUnidadDuracion = filaGrilla.FindControl("DropDownList_UnidadDuracion") as DropDownList;
            String UNIDAD_DURACION = dropUnidadDuracion.SelectedValue;

            TextBox textoobservaciones = filaGrilla.FindControl("TextBox_Observaciones") as TextBox;
            String OBSERVACIONES = textoobservaciones.Text;

            FormacionAcademica _formacionParaLista = new FormacionAcademica();

            _formacionParaLista.ACTIVO = true;
            _formacionParaLista.ANNO = 0;
            _formacionParaLista.CURSO = CURSO;
            _formacionParaLista.DURACION = DURACION;
            _formacionParaLista.ID_INFO_ACADEMICA = ID_INFO_ACADEMICA;
            _formacionParaLista.INSTITUCION = INSTITUCION;
            _formacionParaLista.NIVEL_ACADEMICO = null;
            _formacionParaLista.OBSERVACIONES = OBSERVACIONES;
            _formacionParaLista.REGISTRO_ENTREVISTA = 0;
            _formacionParaLista.TIPO_EDUCACION = TIPO_EDUCACION;
            _formacionParaLista.UNIDAD_DURACION = UNIDAD_DURACION;

            listaFormacionAcademica.Add(_formacionParaLista);
        }


        List<ExperienciaLaboral> listaExperiencia = new List<ExperienciaLaboral>();
        for (int i = 0; i < GridView_ExperienciaLaboral.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ExperienciaLaboral.Rows[i];

            Decimal ID_EXPERIENCIA = Convert.ToDecimal(GridView_ExperienciaLaboral.DataKeys[i].Values["ID_EXPERIENCIA"]);

            TextBox textoEmpresa = filaGrilla.FindControl("TextBox_Empresa") as TextBox;
            String EMPRESA = textoEmpresa.Text;

            TextBox textoCargo = filaGrilla.FindControl("TextBox_Cargo") as TextBox;
            String CARGO = textoCargo.Text;

            TextBox textoFunciones = filaGrilla.FindControl("TextBox_FuncionesRealizadas") as TextBox;
            String FUNCIONES = textoFunciones.Text;

            DateTime FECHA_INGRESO;
            TextBox textoFechaIngreso = filaGrilla.FindControl("TextBox_FechaIngreso") as TextBox;
            try
            {
                FECHA_INGRESO = Convert.ToDateTime(textoFechaIngreso.Text);
            }
            catch
            {
                FECHA_INGRESO = new DateTime();
            }

            DateTime FECHA_RETIRO;
            TextBox textoFechaRetiro = filaGrilla.FindControl("TextBox_FechaRetiro") as TextBox;
            try
            {
                FECHA_RETIRO = Convert.ToDateTime(textoFechaRetiro.Text);
            }
            catch
            {
                FECHA_RETIRO = new DateTime();
            }

            DropDownList dropMotivoRetiro = filaGrilla.FindControl("DropDownList_MotivoRetiro") as DropDownList;
            String MOTIVO_RETIRO = null;
            if (String.IsNullOrEmpty(dropMotivoRetiro.SelectedValue) == false)
            {
                MOTIVO_RETIRO = dropMotivoRetiro.SelectedValue;
            }

            Decimal ULTIMO_SALARIO = 0;
            TextBox textoUltimoSalario = filaGrilla.FindControl("TextBox_Ultimosalario") as TextBox;
            try
            {
                ULTIMO_SALARIO = Convert.ToDecimal(textoUltimoSalario.Text);
            }
            catch
            {
                ULTIMO_SALARIO = 0;
            }

            ExperienciaLaboral _experienciaParaLista = new ExperienciaLaboral();

            _experienciaParaLista.ACTIVO = true;
            _experienciaParaLista.CARGO = CARGO;
            _experienciaParaLista.EMPRESA_CLIENTE = EMPRESA;
            _experienciaParaLista.FECHA_INGRESO = FECHA_INGRESO;
            _experienciaParaLista.FECHA_RETIRO = FECHA_RETIRO;
            _experienciaParaLista.FUNCIONES = FUNCIONES;
            _experienciaParaLista.ID_EXPERIENCIA = ID_EXPERIENCIA;
            _experienciaParaLista.MOTIVO_RETIRO = MOTIVO_RETIRO;
            _experienciaParaLista.REGISTRO_ENTREVISTA = 0;
            _experienciaParaLista.ULTIMO_SALARIO = ULTIMO_SALARIO;

            listaExperiencia.Add(_experienciaParaLista);
        }

        List<ComposicionFamiliar> listaComposicionFamiliar = new List<ComposicionFamiliar>();
        for (int i = 0; i < GridView_ComposicionFamiliar.Rows.Count; i++)
        {
            GridViewRow filaGrilla = GridView_ComposicionFamiliar.Rows[i];

            Decimal ID_COMPOSICION = Convert.ToDecimal(GridView_ComposicionFamiliar.DataKeys[i].Values["ID_COMPOSICION"]);

            DropDownList dropTipoFamiliar = filaGrilla.FindControl("DropDownList_TipoFamiliar") as DropDownList;
            String ID_TIPO_FAMILIAR = dropTipoFamiliar.SelectedValue;

            TextBox textoNombres = filaGrilla.FindControl("TextBox_NombresFamiliar") as TextBox;
            String NOMBRES_FAMILIAR = textoNombres.Text;

            TextBox textoApellidos = filaGrilla.FindControl("TextBox_ApellidosFamiliar") as TextBox;
            String APELLIDOS_FAMILIAR = textoApellidos.Text;

            DateTime FECHA_NACIMIENTO = new DateTime();
            TextBox textoFechaNacimiento = filaGrilla.FindControl("TextBox_FechaNacimientoFamiliar") as TextBox;
            try
            {
                FECHA_NACIMIENTO = Convert.ToDateTime(textoFechaNacimiento.Text);
            }
            catch
            {
                FECHA_NACIMIENTO = new DateTime();
            }

            TextBox textoProfesion = filaGrilla.FindControl("TextBox_ProfesionFamiliar") as TextBox;
            String PROFESION = textoProfesion.Text;

            CheckBox checkExtrajero = filaGrilla.FindControl("") as CheckBox;
            DropDownList dropCiudad = filaGrilla.FindControl("DropDownList_CiudadFamiliar") as DropDownList;
            String ID_CIUDAD = dropCiudad.SelectedValue;

            Boolean VIVE_CON_EL = false;
            CheckBox checkViveConEl = filaGrilla.FindControl("CheckBox_ViveConElFamiliar") as CheckBox;
            if (checkViveConEl.Checked == true)
            {
                VIVE_CON_EL = true;
            }

            Boolean ACTIVO = true;

            ComposicionFamiliar _composicionParaLista = new ComposicionFamiliar();

            _composicionParaLista.ACTIVO = ACTIVO;
            _composicionParaLista.APELLIDOS = APELLIDOS_FAMILIAR;
            _composicionParaLista.FECHA_NACIMIENTO = FECHA_NACIMIENTO;
            _composicionParaLista.ID_CIUDAD = ID_CIUDAD;
            _composicionParaLista.ID_COMPOSICION = ID_COMPOSICION;
            _composicionParaLista.ID_TIPO_FAMILIAR = ID_TIPO_FAMILIAR;
            _composicionParaLista.NOMBRES = NOMBRES_FAMILIAR;
            _composicionParaLista.PROFESION = PROFESION;
            _composicionParaLista.REGISTRO_ENTREVISTA = 0;
            _composicionParaLista.VIVE_CON_EL = VIVE_CON_EL;

            listaComposicionFamiliar.Add(_composicionParaLista);
        }


        string RH = null;
        if (String.IsNullOrEmpty(DropDownList_RH.SelectedValue) == false)
        {
            RH = DropDownList_RH.SelectedValue;
        }

        Dictionary<String, String> listaCamposValidarRestricciones = new Dictionary<String, String>();
        listaCamposValidarRestricciones.Add("APELLIDOS", APELLIDOS);
        listaCamposValidarRestricciones.Add("NOMBRES", NOMBRES);
        listaCamposValidarRestricciones.Add("DIRECCIÓN", DIR_ASPIRANTE);
        listaCamposValidarRestricciones.Add("BARRIO", SECTOR);
        listaCamposValidarRestricciones.Add("TELÉFONO", TEL_ASPIRANTE);
        listaCamposValidarRestricciones.Add("E-MAIL", E_MAIL);
        listaCamposValidarRestricciones.Add("CELULAR", CEL_ASPIRANTE);

        CrtRestriccionPalabra _restricciones = new CrtRestriccionPalabra(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        
        _restricciones.listaPalabrasEntrada = listaCamposValidarRestricciones;
        _restricciones.ComprobarListaPalabras();

        if (_restricciones.listaPalabrasSalida.Count <= 0)
        {

            DataTable tablaComprobacion = _rad.ObtenerRegSolicitudesingresoPorNumDocIdentidadOmitiendoIdSolicitud(NUM_DOC_IDENTIDAD, ID_SOLICITUD);

            if (tablaComprobacion.Rows.Count > 0)
            {
                DataRow filaComprobacion = tablaComprobacion.Rows[0];

                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "El número de documento " + NUM_DOC_IDENTIDAD + " ya se encuentra registrado en la Base de Datos.<br>Fecha Registro: " + filaComprobacion["FECHA_R"].ToString().Trim() + ", Usuario: " + filaComprobacion["USU_CRE"].ToString().Trim() + ", Nombre Candidato: " + filaComprobacion["NOMBRES"].ToString().Trim() + " " + filaComprobacion["APELLIDOS"].ToString().Trim() + ".<BR>NO SE PUDO EDITAR EL REGISTRO.", Proceso.Advertencia);
            }
            else
            {
                Boolean verificador = true;
                verificador = _rad.ActualizarRegSolicitudesingreso(ID_SOLICITUD, APELLIDOS, NOMBRES, TIP_DOC_IDENTIDAD, NUM_DOC_IDENTIDAD, CIU_CEDULA, LIB_MILITAR, CAT_LIC_COND, DIR_ASPIRANTE, SECTOR, CIU_ASPIRANTE, TEL_ASPIRANTE, SEXO, FCH_NACIMIENTO, ID_GRUPOS_PRIMARIOS, ID_FUENTE, CONDUCTO, NIV_EDUCACION, E_MAIL, ID_AREASINTERES, ASPIRACION_SALARIAL, EXPERIENCIA, ID_OCUPACION, NUCLEO_FORMACION, TALLA_CAMISA, TALLA_PANTALON, TALLA_ZAPATOS, ESTRATO, NRO_HIJOS, C_FMLIA, CEL_ASPIRANTE, ESTADO_CIVIL, ID_PAIS, TIPO_VIVIENDA, FUENTE_CONOCIMIENTO, listaFormacionAcademica, listaExperiencia, listaComposicionFamiliar, RH);

                if (verificador == false)
                {
                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _rad.MensajeError, Proceso.Error);
                }
                else
                {
                    Ocultar(Acciones.Inicio);
                    Desactivar(Acciones.Inicio);
                    Mostrar(Acciones.Cargar);

                    Cargar(ID_SOLICITUD);

                    Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La solicitud de ingreso de " + NOMBRES + " " + APELLIDOS + " Se actualizazó correctamente.", Proceso.Correcto);
                }
            }
        }
        else
        {
            Int32 contador = 0;
            String mensaje = "";
            foreach (KeyValuePair<String, String> restriccion in _restricciones.listaPalabrasSalida)
            {
                if (contador <= 0)
                {
                    mensaje = restriccion.Value;
                }
                else
                {
                    mensaje += "</br>" + restriccion.Value;
                }

                contador += 1;
            }

            Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, mensaje, Proceso.Advertencia);
        }
    }

    protected void Button_Guardar_Click(object sender, EventArgs e)
    {
        if (ComprobarTodasLasPestanas(true) == true)
        {
            if (String.IsNullOrEmpty(HiddenField_ID_SOLICITUD.Value) == true)
            {
                Guardar();
            }
            else
            {
                Actualizar();       
            }
        }
    }

    protected void Button_DevolverEnCliente_Click(object sender, EventArgs e)
    {
        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaSolicitud = _radicacionHojasDeVida.ObtenerRegSolicitudesingresoPorIdSolicitud(Convert.ToInt32(HiddenField_ID_SOLICITUD.Value));

        if (tablaSolicitud.Rows.Count <= 0)
        {
            if (_radicacionHojasDeVida.MensajeError != null)
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _radicacionHojasDeVida.MensajeError, Proceso.Error);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "No se encontró información ", Proceso.Advertencia);
            }
        }
        else
        {
            DataRow filaSolicitud = tablaSolicitud.Rows[0];

            Int32 ID_REQUERIMIENTO = Convert.ToInt32(filaSolicitud["ID_REQUERIMIENTO"]);
            Int32 ID_SOLICITUD = Convert.ToInt32(filaSolicitud["ID_SOLICITUD"]);

            Boolean resultado = _radicacionHojasDeVida.ActualizarEstadoRegSolicitudesIngreso(ID_REQUERIMIENTO, ID_SOLICITUD, "DISPONIBLE");

            if (resultado == true)
            {
                Ocultar(Acciones.Inicio);
                Desactivar(Acciones.Inicio);
                Mostrar(Acciones.Cargar);

                Cargar(ID_SOLICITUD);

                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, "La solicitud de ingreso de " + TextBox_NOMBRES.Text + " " + TextBox_APELLIDOS.Text + " Se actualizazó correctamente. El candidato ahora se encuentra DISPONIBLE.", Proceso.Correcto);
            }
            else
            {
                Informar(Panel_FONDO_MENSAJE, Image_MENSAJE_POPUP, Panel_MENSAJES, Label_MENSAJE, _radicacionHojasDeVida.MensajeError, Proceso.Error);
            }
        }
    }
}