using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using Brainsbits.LLB;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seleccion;
using System.IO;
using Brainsbits.LLB.seguridad;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using Brainsbits.LLB.contratacion;
using Brainsbits.LDA;
using Brainsbits.LLB.comercial;
using Brainsbits.LLB.operaciones;
using Brainsbits.LLB.programasRseGlobal;
using System.Configuration;

/// <summary>
/// Summary description for maestras
/// </summary>
public class maestrasInterfaz : System.Web.UI.Page
{
    private String _mensaje_error;

    private String _div_seccion_manual = "<div style=\"font-size: 9px; line-height: 10px;\">";
    private String _div_titulo_manual = "<div style=\"text-align:justify; font-weight:bold; font-size: 10px line-height: 11px;\">";
    private String _div_subTitulo_manual = "<div style=\"text-align:justify; font-weight:bold; font-size: 9px; line-height: 10px;\">";
    private String _div_textoJustificado_manual = "<div style=\"text-align:justify; font-size: 9px; line-height: 10px;\">";

    private Boolean enDesarrollo = true;

    private enum TiposNodoProgramaGeneral
    {
        PROGRAMA = 1,
        ACTIVIDAD,
        SUBPROGRAMA
    }

    private struct infoNodoProgramaGeneral
    {
        public TiposNodoProgramaGeneral TIPO_NODO;
        public String SUB_NOMBRE;
        public String SUB_DESCRIPCION;

        public String ACT_TIPO;
        public String ACT_NOMBRE;
        public String ACT_DESCRIPCION;
        public String ACT_SECTOR;
    }


    private enum ClaseContrato
    {
        O_L = 0,
        I,
        T_F,
        L_C_C_D_A,
        L_S_C_D_A_C_V
    }

    private enum EntidadesAfiliacion
    {
        ARP = 0,
        EPS = 1,
        CAJA = 2,
        AFP = 3
    }

    private Decimal GLO_ID_SUB_C = 0;
    private Decimal GLO_ID_CENTRO_C = 0;
    private String GLO_ID_CIUDAD = null;

    public String MensajeError
    {
        get { return _mensaje_error; }
        set { _mensaje_error = value; }
    }

    public maestrasInterfaz()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public Boolean verificarSessionesSeguridad()
    {
        Boolean resultado = true;

        if (Session["USU_LOG"] == null)
        {
            resultado = false;
        }

        if (Session["idEmpresa"] == null)
        {
            resultado = false;
        }

        return resultado;
    }

    public Boolean verificarHoraInicialMenorQueHoraFinal(int hora_inicial, int minutos_inicial, int hora_final, int minutos_final)
    {
        if (hora_inicial > hora_final)
        {
            return false;
        }
        else
        {
            if (hora_inicial < hora_final)
            {
                return true;
            }
            else
            {
                if (minutos_inicial > minutos_final)
                {
                    return false;
                }
                else
                {
                    if (minutos_inicial < minutos_final)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }

    /// <summary>
    /// Finds a Control recursively. Note finds the first match and exists
    /// </summary>
    /// <param name="ContainerCtl"></param>
    /// <param name="IdToFind"></param>
    /// <returns></returns>
    private Control FindControlRecursive(Control Root, string Id)
    {
        if (Root.ID == Id) return Root;

        foreach (Control Ctl in Root.Controls)
        {
            Control FoundCtl = FindControlRecursive(Ctl, Id);
            if (FoundCtl != null) return FoundCtl;
        }

        return null;
    }


    public int RolPermisos(Control pagina, DataTable tablaInformacionPermisos)
    {
        int contadorPermisos = 0;

        /*
         * validamos seguridad general e interna para cada formulario
        */
        if (ConfigurationManager.AppSettings["habilitarSeguridad"] == "true")
        {
            //la seguridad general está habilitada
            //ahora se debe comprobar a nivel area y formulario
            //si la tabla con informacion de permisos no tiene filas es porque no se han definido los permisos para el formulario 
            // en cuestion
            if (tablaInformacionPermisos.Rows.Count <= 0)
            {
                contadorPermisos = 0;
            }
            else
            { 
                //si tiene objetos configurados
                //revisasmos configuracion interna
                DataRow filaPermiso = tablaInformacionPermisos.Rows[0];

                if (filaPermiso["MODULO_ACTIVO"].ToString().ToUpper() == "FALSE")
                {
                    contadorPermisos = 0;
                }
                else
                { 
                    //EL FORMULARIO ESTA ACTIVO
                    //REVISAMOS SEGURIDAD INTERNA
                    if (filaPermiso["SEGURIDAD_HABILITADA_MODULO"].ToString().ToUpper() == "FALSE")
                    {
                        contadorPermisos = 1;
                    }
                    else
                    { 
                        //tiene la seguridad interna y general activa, entonces se debe verificar el acceso
                        foreach (DataRow fila in tablaInformacionPermisos.Rows)
                        {
                            // Acciones a seguir si el acceso es para un boton
                            if (fila["TIPO"].ToString() == "Button")
                            {
                                String[] nombresBotones;
                                Button boton;

                                nombresBotones = fila["PARAMETROS"].ToString().Split(';');

                                if (fila["PERMISO"].ToString() == "1")
                                {
                                    contadorPermisos += 1;
                                }
                                else
                                {
                                    foreach (String nombreBoton in nombresBotones)
                                    {
                                        boton = FindControlRecursive(pagina, nombreBoton) as Button;

                                        boton.Enabled = false;
                                        boton.Visible = false;
                                    }
                                }
                            }

                            // Acciones a seguir si el acceso es para una grilla
                            if (fila["TIPO"].ToString() == "GridView")
                            {
                                String[] parametros = fila["PARAMETROS"].ToString().Split(':');

                                GridView grilla = FindControlRecursive(pagina, parametros[0]) as GridView;

                                if (fila["PERMISO"].ToString() == "0")
                                {
                                    grilla.Columns[Convert.ToInt32(parametros[1])].Visible = false;

                                    //foreach (GridViewRow filaGrilla in grilla.Rows)
                                    //{
                                    //    filaGrilla.Cells[Convert.ToInt32(parametros[1])].Enabled = false;
                                    //}
                                }
                                else
                                {
                                    contadorPermisos += 1;
                                }
                            }

                            //acciones a seguir si el acceso es para un checkbox
                            if (fila["TIPO"].ToString() == "RadioButtonList")
                            {
                                String[] parametros = fila["PARAMETROS"].ToString().Split(':');

                                RadioButtonList objRadioButtonList = FindControlRecursive(pagina, parametros[0]) as RadioButtonList;

                                if (fila["PERMISO"].ToString() == "0")
                                {
                                    objRadioButtonList.Items[Convert.ToInt32(parametros[1])].Enabled = false;
                                    objRadioButtonList.Items[Convert.ToInt32(parametros[1])].Attributes.Add("title", "Usted no tiene permisos para acceder a este objeto.");
                                }
                                else
                                {
                                    contadorPermisos += 1;
                                }
                            }


                            //acciones a seguir si el acceso es para un checkbox
                            if (fila["TIPO"].ToString() == "Panel")
                            {
                                String par = fila["PARAMETROS"].ToString();

                                Panel objPanel = FindControlRecursive(pagina, par) as Panel;

                                if (fila["PERMISO"].ToString() == "0")
                                {
                                    objPanel.Visible = false;
                                }
                                else
                                {
                                    contadorPermisos += 1;
                                }
                            }

                            // Acciones a seguir si el acceso es de lectura
                            if (fila["TIPO"].ToString() == "Ninguno")
                            {
                                if (fila["PERMISO"].ToString() == "1")
                                {
                                    contadorPermisos += 1;
                                }
                            }
                        }
                    }
                }
            }
        }
        else
        {
            //no esta la seguridad general activa
            //solo nos debemos preocupar si el form esta inactivo
            if (tablaInformacionPermisos.Rows.Count <= 0)
            {
                contadorPermisos = 0;
            }
            else
            {
                //si tiene objetos configurados
                //revisasmos configuracion interna
                DataRow filaPermiso = tablaInformacionPermisos.Rows[0];

                if (filaPermiso["MODULO_ACTIVO"].ToString().ToUpper() == "FALSE")
                {
                    contadorPermisos = 0;
                }
                else
                {
                    contadorPermisos = 1;    
                }
            }
        }

        return contadorPermisos;
    }


    /// <summary>
    /// HECHO POR CESAR PULIDO
    /// EL DIA 18 DE DICIMEBRE DE 2012
    /// PARA GENERAR LA ENTRVISTA CON O SIN COMPETENCIAS
    /// </summary>
    /// <returns></returns>
    public byte[] GenerarPDFEntrevista(Decimal ID_SOLICITUD, Decimal ID_PERFIL)
    {
        //ok
        tools _tools = new tools();

        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaSolicitud = _radicacionHojasDeVida.ObtenerRegSolicitudesingresoPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));
        DataRow filaSolicitud = tablaSolicitud.Rows[0];

        String NOMBRE_ASPIRANTE = filaSolicitud["NOMBRES"].ToString().Trim() + " " + filaSolicitud["APELLIDOS"].ToString().Trim();
        
        String DOC_IDENTIDAD_ASPIRANTE = filaSolicitud["TIP_DOC_IDENTIDAD"].ToString().Trim() + " " + filaSolicitud["NUM_DOC_IDENTIDAD"].ToString().Trim();
        
        int EDAD_ASPIRANTE = 0;
        if (DBNull.Value.Equals(filaSolicitud["FCH_NACIMIENTO"]) == false)
        {
            try
            { EDAD_ASPIRANTE = _tools.ObtenerEdadDesdeFechaNacimiento(Convert.ToDateTime(filaSolicitud["FCH_NACIMIENTO"])); }
            catch { EDAD_ASPIRANTE = 0; }
        }

        String TIPO_VIVIENDA_ASPIRANTE = "Desconocida";
        if(DBNull.Value.Equals(filaSolicitud["TIPO_VIVIENDA"]) == false)
        {
            TIPO_VIVIENDA_ASPIRANTE = filaSolicitud["TIPO_VIVIENDA"].ToString().Trim();
        }

        String CIUDAD_ASPIRANTE = "Desconocida";
        if (DBNull.Value.Equals(filaSolicitud["NOMBRE_CIUDAD"]) == false)
        {
            CIUDAD_ASPIRANTE = filaSolicitud["NOMBRE_CIUDAD"].ToString().Trim();
        }

        String DIRECCION_ASPIRANTE = filaSolicitud["DIR_ASPIRANTE"].ToString().Trim();

        String SECTOR_ASPIRANTE = filaSolicitud["SECTOR"].ToString().Trim();

        String ESTRATO_ASPIRANTE = "Desconocido";
        if (DBNull.Value.Equals(filaSolicitud["ESTRATO"]) == false) { ESTRATO_ASPIRANTE = filaSolicitud["ESTRATO"].ToString().Trim(); }

        String TELEFONOS_ASPIRANTE = filaSolicitud["TEL_ASPIRANTE"].ToString();

        String ASPIRACION_SALARIAL_ASPIRANTE;
        try { ASPIRACION_SALARIAL_ASPIRANTE = Convert.ToInt32(filaSolicitud["ASPIRACION_SALARIAL"]).ToString(); }
        catch { ASPIRACION_SALARIAL_ASPIRANTE = "Desconocido."; }

        String ESTADO_CIVIL = "Desconocido";
        if (DBNull.Value.Equals(filaSolicitud["ESTADO_CIVIL"]) == false)
        {
            ESTADO_CIVIL = filaSolicitud["ESTADO_CIVIL"].ToString().Trim();
        }
        
        String EMAIL_ASPIRANTE = filaSolicitud["E_MAIL"].ToString().Trim();

        //FEMENINO, MASCULINO
        String SEXO = "Desconocido.";
        String LIBRETA_MILITAR = "Desconocida.";
        if (DBNull.Value.Equals(filaSolicitud["SEXO"]) == false)
        {
            if (filaSolicitud["SEXO"].ToString().ToUpper() == "F")
            {
                SEXO = "Femenino";
                LIBRETA_MILITAR = "No Aplica";
            }
            else
            {
                if (filaSolicitud["SEXO"].ToString().ToUpper() == "M")
                {
                    SEXO = "Masculino";
                    LIBRETA_MILITAR = filaSolicitud["LIB_MILITAR"].ToString();
                }
            }
        }

        //cargo al que aspira el candidato (cargo generico)
        String CARGO_APLICA = "Desconocido";
        Decimal ID_OCUPACION = 0;
        perfil _perfil = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaPerfil = new DataTable();
        Decimal ID_ASSESMENT_CENTER = 0;

        if (ID_PERFIL <= 0)
        {
            CARGO_APLICA = "Entrevista por Producción.";
            ID_ASSESMENT_CENTER = 0;
        }
        else
        {
            tablaPerfil = _perfil.ObtenerPorRegistro(ID_PERFIL);
            if (tablaPerfil.Rows.Count <= 0)
            {
                CARGO_APLICA = "Desconocido.";
                ID_ASSESMENT_CENTER = 0;
            }
            else
            {
                DataRow filaPerfil = tablaPerfil.Rows[0];
                try
                {
                    ID_OCUPACION = Convert.ToDecimal(filaPerfil["ID_OCUPACION"]);
                }
                catch
                {
                    ID_OCUPACION = 0;
                }

                if (ID_OCUPACION > 0)
                {
                    cargo _cargo = new cargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                    DataTable tablaOcupacionAspira = _cargo.ObtenerOcupacionPorIdOcupacion(ID_OCUPACION);

                    if (tablaOcupacionAspira.Rows.Count > 0)
                    {
                        DataRow filaOcupacionAspira = tablaOcupacionAspira.Rows[0];
                        CARGO_APLICA = filaOcupacionAspira["NOM_OCUPACION"].ToString().Trim();
                    }
                }

                //DETERMINAMOS SI SE TIENE UN ID_ASSESMENT_CENTER ASOCIADO AL PERFIL
                if(filaPerfil["TIPO_ENTREVISTA"].ToString().Trim() == "A")
                {
                    try
                    {
                        ID_ASSESMENT_CENTER = Convert.ToDecimal(filaPerfil["ID_ASSESMENT_CENTAR"]);
                    }
                    catch
                    {
                        ID_ASSESMENT_CENTER = 0;
                    }
                }
                else
                {
                    ID_ASSESMENT_CENTER = 0;
                }
            }
        }
        
        //si la entrevista basica existe ya
        hojasVida _hojasVida = new hojasVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaEntrevista = _hojasVida.ObtenerSelRegEntrevistasPorIdSolicitud(ID_SOLICITUD);

        String FECHA_ENTREVISTA = "Desconocida.";
        String COMPOSICION_FAMILIAR_ASPIRANTE = "Desconocida.";
        String INFO_ACADEMICA_ASPIRANTE = "Desconocida.";
        String EXPERIENCIA_LAB_ASPIRANTE = "Desconocida.";
        String CONCEPTO_GENERAL = "Desconodido.";
        String USUARIO_ENTREVISTADOR = Session["USU_LOG"].ToString();

        Decimal ID_ENTREVISTA = 0;

        if (tablaEntrevista.Rows.Count > 0)
        {
            DataRow filaEntrevista = tablaEntrevista.Rows[0];

            ID_ENTREVISTA = Convert.ToDecimal(filaEntrevista["REGISTRO"]);

            try
            {
                FECHA_ENTREVISTA = Convert.ToDateTime(filaEntrevista["FCH_ENTREVISTA"]).ToShortDateString();
            }
            catch
            {
                FECHA_ENTREVISTA = "Desconocida.";
            }

            COMPOSICION_FAMILIAR_ASPIRANTE = filaEntrevista["COM_C_FAM"].ToString().Trim();
            INFO_ACADEMICA_ASPIRANTE = filaEntrevista["COM_C_ACA"].ToString().Trim();
            EXPERIENCIA_LAB_ASPIRANTE = filaEntrevista["COM_F_LAB"].ToString().Trim();
            CONCEPTO_GENERAL = filaEntrevista["COM_C_GEN"].ToString().Trim();

            if (DBNull.Value.Equals(filaEntrevista["USU_MOD"]) == false)
            {
                USUARIO_ENTREVISTADOR = filaEntrevista["USU_MOD"].ToString().Trim();
            }
            else
            {
                USUARIO_ENTREVISTADOR = filaEntrevista["USU_CRE"].ToString().Trim();
            }
        }

        String NIVEL_ESCOLARIDAD = "Desconocido";
        if (DBNull.Value.Equals(filaSolicitud["NIVEL_ESCOLARIDAD"]) == false)
        {
            if (filaSolicitud["NIVEL_ESCOLARIDAD"].ToString().Trim() == "NO REQUERIDO")
            {
                NIVEL_ESCOLARIDAD = "NO APLICA";
            }
            else
            {
                NIVEL_ESCOLARIDAD = filaSolicitud["NIVEL_ESCOLARIDAD"].ToString().Trim();
            }
        }

        String PROFESION_ASPIRANTE = "Desconocida";
        if (DBNull.Value.Equals(filaSolicitud["ID_NUCLEO_FORMACION"]) == false)
        {
            PROFESION_ASPIRANTE = filaSolicitud["ID_NUCLEO_FORMACION"].ToString().Trim();
        }

        String ESPECIALIZACION_ASPIRANTE = "Desconocida";
        if (DBNull.Value.Equals(filaSolicitud["AREA_INTERES"]) == false)
        {
            ESPECIALIZACION_ASPIRANTE = filaSolicitud["AREA_INTERES"].ToString().Trim().ToUpper();
        }

        String CABEZA_FAMILIA = "Desconocido";
        if (DBNull.Value.Equals(filaSolicitud["C_FMLIA"]) == false)
        {
            if (filaSolicitud["C_FMLIA"].ToString().Trim() == "S")
            {
                CABEZA_FAMILIA = "SI";
            }
            else
            {
                CABEZA_FAMILIA = "NO";
            }
        }

        String NUM_HIJOS = "Desconocido";
        if (DBNull.Value.Equals(filaSolicitud["NRO_HIJOS"]) == false)
        {
            NUM_HIJOS = filaSolicitud["NRO_HIJOS"].ToString().Trim();
        }

        /*
         * Generación del archi de informe de selección
         * Stream con el contenido del pdf.
        */
        String html_encabezado = "<html>";
        html_encabezado += "<head>";
        html_encabezado += "</head>";
        html_encabezado += "<body>";

        String html_pie = "</body>";
        html_pie += "</html>";

        //para el concepto general en entrevista o resultado de competencias.
        String html_concepto = "<div style=\"text-align: left; margin: 0 0 0 20px; text-decoration: underline; font-weight: bold;\">";
        html_concepto += "CONCEPTO DEL ENTREVISTADOR";
        html_concepto += "</div>";
        html_concepto += "<br />";
        html_concepto += "<div style=\"text-align: justify;\">";
        html_concepto += "[CONCEPTO_GENERAL]";
        html_concepto += "</div>";

        //En esta variable cargamos el documento plantilla
        StreamReader archivo_original = new StreamReader(Server.MapPath(@"~\plantillas_reportes\entrevista.htm"));

        String html_formato_entrevista = html_encabezado + archivo_original.ReadToEnd();

        archivo_original.Dispose();
        archivo_original.Close();

        // -----------------------------------
        html_formato_entrevista = html_formato_entrevista.Replace("[CARGO_APLICA]", CARGO_APLICA);
        html_formato_entrevista = html_formato_entrevista.Replace("[FECHA_ENTREVISTA]", FECHA_ENTREVISTA);
        // -----------------------------------

        // -----------------------------------
        html_formato_entrevista = html_formato_entrevista.Replace("[NOMBRE_ASPIRANTE]", NOMBRE_ASPIRANTE);
        
        html_formato_entrevista = html_formato_entrevista.Replace("[DOC_IDENTIDAD_ASPIRANTE]", DOC_IDENTIDAD_ASPIRANTE);
        
        if (EDAD_ASPIRANTE > 0) { html_formato_entrevista = html_formato_entrevista.Replace("[EDAD_ASPIRANTE]", EDAD_ASPIRANTE.ToString() + " Años."); }
        else { html_formato_entrevista = html_formato_entrevista.Replace("[EDAD_ASPIRANTE]", "Desconocida."); }

        html_formato_entrevista = html_formato_entrevista.Replace("[DIRECCION_ASPIRANTE]", DIRECCION_ASPIRANTE);

        html_formato_entrevista = html_formato_entrevista.Replace("[TIPO_VIVIENDA_ASPIRANTE]", TIPO_VIVIENDA_ASPIRANTE);

        html_formato_entrevista = html_formato_entrevista.Replace("[CIUDAD_ASPIRANTE]", CIUDAD_ASPIRANTE);

        html_formato_entrevista = html_formato_entrevista.Replace("[SECTOR_ASPIRANTE]", SECTOR_ASPIRANTE);

        html_formato_entrevista = html_formato_entrevista.Replace("[ESTRATO_ASPIRANTE]", ESTRATO_ASPIRANTE);

        html_formato_entrevista = html_formato_entrevista.Replace("[TELEFONOS_ASPIRANTE]", TELEFONOS_ASPIRANTE);

        html_formato_entrevista = html_formato_entrevista.Replace("[ASPIRACION_SALARIAL_ASPIRANTE]", ASPIRACION_SALARIAL_ASPIRANTE);

        html_formato_entrevista = html_formato_entrevista.Replace("[ESTADO_CIVIL_ASPIRANTE]", ESTADO_CIVIL);

        html_formato_entrevista = html_formato_entrevista.Replace("[EMAIL_ASPIRANTE]", EMAIL_ASPIRANTE);

        html_formato_entrevista = html_formato_entrevista.Replace("[LIBRETA_MILITAR]", LIBRETA_MILITAR);
        // -----------------------------------


        // -----------------------------------
        html_formato_entrevista = html_formato_entrevista.Replace("[COMPOSICION_FAMILIAR_ASPIRANTE]", COMPOSICION_FAMILIAR_ASPIRANTE);
        html_formato_entrevista = html_formato_entrevista.Replace("[INFO_ACADEMICA_ASPIRANTE]", INFO_ACADEMICA_ASPIRANTE);
        html_formato_entrevista = html_formato_entrevista.Replace("[EXPERIENCIA_LAB_ASPIRANTE]", EXPERIENCIA_LAB_ASPIRANTE);
        // -----------------------------------

        // -----------------------------------
        html_formato_entrevista = html_formato_entrevista.Replace("[NIVEL_ESCOLARIDAD]", NIVEL_ESCOLARIDAD);
        html_formato_entrevista = html_formato_entrevista.Replace("[PROFESION_ASPIRANTE]", PROFESION_ASPIRANTE);
        html_formato_entrevista = html_formato_entrevista.Replace("[ESPECIALIZACION_ASPIRANTE]", ESPECIALIZACION_ASPIRANTE);
        html_formato_entrevista = html_formato_entrevista.Replace("[CABEZA_FAMILIA]", CABEZA_FAMILIA);
        html_formato_entrevista = html_formato_entrevista.Replace("[NUM_HIJOS]", NUM_HIJOS);
        // -----------------------------------


        //TABLA DE COMPOSICION FAMILIAR
        String html_tabla_composicion_familiar = "<table border=\"1\" cellpadding=\"1\" cellspacing=\"0\" width=\"100%\" align=\"center\" style=\"font-size:8px; line-height:9px;\">";
        html_tabla_composicion_familiar += "<tr>";
        html_tabla_composicion_familiar += "<td width=\"14%\" style=\"text-align:center; font-weight: bold;\">";
        html_tabla_composicion_familiar += "PARENTESCO";
        html_tabla_composicion_familiar += "</td>";
        html_tabla_composicion_familiar += "<td width=\"25%\" style=\"text-align:center; font-weight: bold;\">";
        html_tabla_composicion_familiar += "NOMBRES Y APELLIDOS";
        html_tabla_composicion_familiar += "</td>";
        html_tabla_composicion_familiar += "<td width=\"10%\" style=\"text-align:center; font-weight: bold; \">";
        html_tabla_composicion_familiar += "EDAD";
        html_tabla_composicion_familiar += "</td>";
        html_tabla_composicion_familiar += "<td width=\"20%\" style=\"text-align:center; font-weight: bold; \">";
        html_tabla_composicion_familiar += "¿A QUÉ SE DEDICA?";
        html_tabla_composicion_familiar += "</td>";
        html_tabla_composicion_familiar += "<td width=\"19%\" style=\"text-align:center; font-weight: bold; \">";
        html_tabla_composicion_familiar += "VIVE EN";
        html_tabla_composicion_familiar += "</td>";
        html_tabla_composicion_familiar += "<td width=\"12%\" style=\"text-align:center; font-weight: bold;\">";
        html_tabla_composicion_familiar += "VIVE CON EL CANDIDATO";
        html_tabla_composicion_familiar += "</td>";
        html_tabla_composicion_familiar += "</tr>";

        DataTable tablainfofamiliar = _hojasVida.ObtenerSelRegComposicionFamiliar(ID_ENTREVISTA);

        foreach(DataRow filaComposicion in tablainfofamiliar.Rows)
        {
            html_tabla_composicion_familiar += "<tr>";
            html_tabla_composicion_familiar += "<td width=\"14%\" style=\"text-align:left;\">";
            html_tabla_composicion_familiar += filaComposicion["ID_TIPO_FAMILIAR"].ToString().Trim();
            html_tabla_composicion_familiar += "</td>";
            html_tabla_composicion_familiar += "<td width=\"25%\" style=\"text-align:left;\">";
            html_tabla_composicion_familiar += filaComposicion["NOMBRES"].ToString().Trim() + " " + filaComposicion["APELLIDOS"].ToString().Trim();
            html_tabla_composicion_familiar += "</td>";
            html_tabla_composicion_familiar += "<td width=\"10%\" style=\"text-align:center;\">";
            try
            {
                html_tabla_composicion_familiar += _tools.ObtenerEdadDesdeFechaNacimiento(Convert.ToDateTime(filaComposicion["FECHA_NACIMIENTO"]));
            }
            catch
            {
                html_tabla_composicion_familiar += "Desconocida";
            }
            html_tabla_composicion_familiar += "</td>";
            html_tabla_composicion_familiar += "<td width=\"20%\" style=\"text-align:left;\">";
            html_tabla_composicion_familiar += filaComposicion["PROFESION"].ToString().Trim();
            html_tabla_composicion_familiar += "</td>";
            html_tabla_composicion_familiar += "<td width=\"19%\" style=\"text-align:left;\">";
            if (filaComposicion["ID_CIUDAD"].ToString().Trim() == "EXTRA")
            {
                html_tabla_composicion_familiar += "Extranjero";
            }
            else
            {
                html_tabla_composicion_familiar += filaComposicion["NOMBRE_CIUDAD"].ToString().Trim();
            }
            html_tabla_composicion_familiar += "</td>";
            html_tabla_composicion_familiar += "<td width=\"12%\" style=\"text-align:center;\">";
            if(filaComposicion["VIVE_CON_EL"].ToString().Trim().ToUpper() == "TRUE")
            {
                html_tabla_composicion_familiar += "SI";
            }
            else
            {
                html_tabla_composicion_familiar += "NO";
            }
            html_tabla_composicion_familiar += "</td>";
            html_tabla_composicion_familiar += "</tr>";  
        }

        html_tabla_composicion_familiar += "</table>";
        html_formato_entrevista = html_formato_entrevista.Replace("[TABLA_COMPOSICION_FAMILIAR]", html_tabla_composicion_familiar);


        //EDUCACION FORMAL
        String html_tabla_educacion_formal = "<table border=\"1\" cellpadding=\"1\" cellspacing=\"0\" width=\"100%\" align=\"center\" style=\"font-size:8px; line-height:9px;\">";
        html_tabla_educacion_formal += "<tr>";
        html_tabla_educacion_formal += "<td width=\"40%\" style=\"text-align:center; font-weight: bold;\">";
        html_tabla_educacion_formal += "Grado de Instrucción alcanzado:<\br>Especialización, Profesional, Bachiller.";
        html_tabla_educacion_formal += "</td>";
        html_tabla_educacion_formal += "<td width=\"25%\" style=\"text-align:center; font-weight: bold;\">";
        html_tabla_educacion_formal += "Institución";
        html_tabla_educacion_formal += "</td>";
        html_tabla_educacion_formal += "<td width=\"12%\" style=\"text-align:center; font-weight: bold; \">";
        html_tabla_educacion_formal += "Año";
        html_tabla_educacion_formal += "</td>";
        html_tabla_educacion_formal += "<td width=\"23%\" style=\"text-align:center; font-weight: bold; \">";
        html_tabla_educacion_formal += "Observaciones";
        html_tabla_educacion_formal += "</td>";
        html_tabla_educacion_formal += "</tr>";

        DataTable tablaEducacionFormal = _hojasVida.ObtenerSelRegInformacionAcademica(ID_ENTREVISTA, "FORMAL");

        foreach (DataRow filaEducacionFormal in tablaEducacionFormal.Rows)
        {
            html_tabla_educacion_formal += "<tr>";
            html_tabla_educacion_formal += "<td width=\"40%\" style=\"text-align:left;\">";
            html_tabla_educacion_formal += filaEducacionFormal["NOMBRE_NIVEL_ACADEMICO"].ToString().Trim();
            html_tabla_educacion_formal += "</td>";
            html_tabla_educacion_formal += "<td width=\"25%\" style=\"text-align:left;\">";
            html_tabla_educacion_formal += filaEducacionFormal["INSTITUCION"].ToString().Trim();
            html_tabla_educacion_formal += "</td>";
            html_tabla_educacion_formal += "<td width=\"12%\" style=\"text-align:center;\">";
            html_tabla_educacion_formal += filaEducacionFormal["ANNO"].ToString().Trim();
            html_tabla_educacion_formal += "</td>";
            html_tabla_educacion_formal += "<td width=\"23%\" style=\"text-align:justify;\">";
            html_tabla_educacion_formal += filaEducacionFormal["OBSERVACIONES"].ToString().Trim();
            html_tabla_educacion_formal += "</td>";
            html_tabla_educacion_formal += "</tr>";
        }

        html_tabla_educacion_formal += "</table>";
        html_formato_entrevista = html_formato_entrevista.Replace("[TABLA_EDUCACION_FORMAL]", html_tabla_educacion_formal);




        //EDUCACION NO FORMAL
        String html_tabla_educacion_no_formal = "<table border=\"1\" cellpadding=\"1\" cellspacing=\"0\" width=\"100%\" align=\"center\" style=\"font-size:8px; line-height:9px;\">";
        html_tabla_educacion_no_formal += "<tr>";
        html_tabla_educacion_no_formal += "<td width=\"40%\" style=\"text-align:center; font-weight: bold;\">";
        html_tabla_educacion_no_formal += "Cursos libres - Diplomados";
        html_tabla_educacion_no_formal += "</td>";
        html_tabla_educacion_no_formal += "<td width=\"25%\" style=\"text-align:center; font-weight: bold;\">";
        html_tabla_educacion_no_formal += "Institución";
        html_tabla_educacion_no_formal += "</td>";
        html_tabla_educacion_no_formal += "<td width=\"12%\" style=\"text-align:center; font-weight: bold; \">";
        html_tabla_educacion_no_formal += "Duración";
        html_tabla_educacion_no_formal += "</td>";
        html_tabla_educacion_no_formal += "<td width=\"23%\" style=\"text-align:center; font-weight: bold; \">";
        html_tabla_educacion_no_formal += "Observaciones";
        html_tabla_educacion_no_formal += "</td>";
        html_tabla_educacion_no_formal += "</tr>";

        DataTable tablaEducacionNoFormal = _hojasVida.ObtenerSelRegInformacionAcademica(ID_ENTREVISTA, "NO FORMAL");

        foreach (DataRow filaEducacionNoFormal in tablaEducacionNoFormal.Rows)
        {
            html_tabla_educacion_no_formal += "<tr>";
            html_tabla_educacion_no_formal += "<td width=\"40%\" style=\"text-align:left;\">";
            html_tabla_educacion_no_formal += filaEducacionNoFormal["CURSO"].ToString().Trim();
            html_tabla_educacion_no_formal += "</td>";
            html_tabla_educacion_no_formal += "<td width=\"25%\" style=\"text-align:left;\">";
            html_tabla_educacion_no_formal += filaEducacionNoFormal["INSTITUCION"].ToString().Trim();
            html_tabla_educacion_no_formal += "</td>";
            html_tabla_educacion_no_formal += "<td width=\"12%\" style=\"text-align:center;\">";
            html_tabla_educacion_no_formal += filaEducacionNoFormal["DURACION"].ToString().Trim() + " " + filaEducacionNoFormal["UNIDAD_DURACION"].ToString().Trim();
            html_tabla_educacion_no_formal += "</td>";
            html_tabla_educacion_no_formal += "<td width=\"23%\" style=\"text-align:justify;\">";
            html_tabla_educacion_no_formal += filaEducacionNoFormal["OBSERVACIONES"].ToString().Trim();
            html_tabla_educacion_no_formal += "</td>";
            html_tabla_educacion_no_formal += "</tr>";
        }

        html_tabla_educacion_no_formal += "</table>";
        html_formato_entrevista = html_formato_entrevista.Replace("[TABLA_EDUCACION_NO_FORMAL]", html_tabla_educacion_no_formal);



        //EXPERIENCIA LABORAL
        String html_tabla_experiencia_laboral = "";
        DataTable tablaExperiencia = _hojasVida.ObtenerSelRegExperienciaLaboral(ID_ENTREVISTA);

        Int32 contador = 1;
        foreach (DataRow filaExperienciaLaboral in tablaExperiencia.Rows)
        {
            if (contador == 1)
            {
                html_tabla_experiencia_laboral += "<table border=\"1\" cellpadding=\"1\" cellspacing=\"0\" width=\"100%\" align=\"center\" style=\"font-size:8px; line-height:9px;\">";
            }
            else
            {
                html_tabla_experiencia_laboral += "<br><table border=\"1\" cellpadding=\"1\" cellspacing=\"0\" width=\"100%\" align=\"center\" style=\"font-size:8px; line-height:9px;\">";
            }
            
            html_tabla_experiencia_laboral += "<tr>";
            html_tabla_experiencia_laboral += "<td width=\"50%\" style=\"text-align:left;\">";
            html_tabla_experiencia_laboral += contador.ToString() + ". EMPRESA:";
            html_tabla_experiencia_laboral += "</td>";
            html_tabla_experiencia_laboral += "<td width=\"50%\" style=\"text-align:left;\">";
            html_tabla_experiencia_laboral += filaExperienciaLaboral["EMPRESA"].ToString().Trim();
            html_tabla_experiencia_laboral += "</td>";
            html_tabla_experiencia_laboral += "</tr>";

            html_tabla_experiencia_laboral += "<tr>";
            html_tabla_experiencia_laboral += "<td width=\"50%\" style=\"text-align:left;\">";
            html_tabla_experiencia_laboral += "CARGO DESEMPEÑADO:";
            html_tabla_experiencia_laboral += "</td>";
            html_tabla_experiencia_laboral += "<td width=\"50%\" style=\"text-align:left;\">";
            html_tabla_experiencia_laboral += filaExperienciaLaboral["CARGO"].ToString().Trim();
            html_tabla_experiencia_laboral += "</td>";
            html_tabla_experiencia_laboral += "</tr>";

            html_tabla_experiencia_laboral += "<tr>";
            html_tabla_experiencia_laboral += "<td width=\"50%\" style=\"text-align:left;\">";
            html_tabla_experiencia_laboral += "FUNCIONES REALIZADAS:";
            html_tabla_experiencia_laboral += "</td>";
            html_tabla_experiencia_laboral += "<td width=\"50%\" style=\"text-align:left;\">";
            html_tabla_experiencia_laboral += filaExperienciaLaboral["FUNCIONES"].ToString().Trim();
            html_tabla_experiencia_laboral += "</td>";
            html_tabla_experiencia_laboral += "</tr>";

            html_tabla_experiencia_laboral += "<tr>";
            html_tabla_experiencia_laboral += "<td width=\"50%\" style=\"text-align:left;\">";
            html_tabla_experiencia_laboral += "FECHA INGRESO:";
            html_tabla_experiencia_laboral += "</td>";
            html_tabla_experiencia_laboral += "<td width=\"50%\" style=\"text-align:left;\">";
            html_tabla_experiencia_laboral += Convert.ToDateTime(filaExperienciaLaboral["FECHA_INGRESO"]).ToShortDateString();
            html_tabla_experiencia_laboral += "</td>";
            html_tabla_experiencia_laboral += "</tr>";

            html_tabla_experiencia_laboral += "<tr>";
            html_tabla_experiencia_laboral += "<td width=\"50%\" style=\"text-align:left;\">";
            html_tabla_experiencia_laboral += "FECHA RETIRO:";
            html_tabla_experiencia_laboral += "</td>";
            html_tabla_experiencia_laboral += "<td width=\"50%\" style=\"text-align:left;\">";
            try
            {
                html_tabla_experiencia_laboral += Convert.ToDateTime(filaExperienciaLaboral["FECHA_RETIRO"]).ToShortDateString();
            }
            catch
            {
                html_tabla_experiencia_laboral += "";
            }
            html_tabla_experiencia_laboral += "</td>";
            html_tabla_experiencia_laboral += "</tr>";




            html_tabla_experiencia_laboral += "<tr>";
            html_tabla_experiencia_laboral += "<td width=\"50%\" style=\"text-align:left;\">";
            html_tabla_experiencia_laboral += "TIEMPO TRABAJADO:";
            html_tabla_experiencia_laboral += "</td>";
            html_tabla_experiencia_laboral += "<td width=\"50%\" style=\"text-align:left;\">";
            Boolean correcto = true;
            DateTime fechaIngreso;
            DateTime fechaRetiro;
            try
            {
                fechaIngreso = Convert.ToDateTime(filaExperienciaLaboral["FECHA_INGRESO"]);
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
                    fechaRetiro = Convert.ToDateTime(filaExperienciaLaboral["FECHA_RETIRO"]);
                    conContratoVigente = false;
                }
                catch
                {
                    conContratoVigente = true;
                    fechaRetiro = Convert.ToDateTime(DateTime.Now.ToShortDateString());
                }

                if (fechaRetiro < fechaIngreso)
                {
                    html_tabla_experiencia_laboral += "Error en fechas.";
                }
                else
                {
                    if (conContratoVigente == true)
                    {
                        html_tabla_experiencia_laboral += "Lleva trabajando: " + _tools.DiferenciaFechas(fechaRetiro, fechaIngreso);
                    }
                    else
                    {
                        html_tabla_experiencia_laboral += "Trabajó: " + _tools.DiferenciaFechas(fechaRetiro, fechaIngreso);
                    }
                }
            }
            else
            {
                html_tabla_experiencia_laboral += "Desconocido.";
            }
            html_tabla_experiencia_laboral += "</td>";
            html_tabla_experiencia_laboral += "</tr>";

            html_tabla_experiencia_laboral += "<tr>";
            html_tabla_experiencia_laboral += "<td width=\"50%\" style=\"text-align:left;\">";
            html_tabla_experiencia_laboral += "MOTIVO_RETIRO:";
            html_tabla_experiencia_laboral += "</td>";
            html_tabla_experiencia_laboral += "<td width=\"50%\" style=\"text-align:left;\">";
            html_tabla_experiencia_laboral += filaExperienciaLaboral["MOTIVO_RETIRO"].ToString().Trim();
            html_tabla_experiencia_laboral += "</td>";
            html_tabla_experiencia_laboral += "</tr>";

            html_tabla_experiencia_laboral += "<tr>";
            html_tabla_experiencia_laboral += "<td width=\"50%\" style=\"text-align:left;\">";
            html_tabla_experiencia_laboral += "ULTIMO SALARIO:";
            html_tabla_experiencia_laboral += "</td>";
            html_tabla_experiencia_laboral += "<td width=\"50%\" style=\"text-align:left;\">";
            html_tabla_experiencia_laboral += String.Format("$ {0:N2}", Convert.ToDecimal(filaExperienciaLaboral["ULTIMO_SALARIO"]));
            html_tabla_experiencia_laboral += "</td>";
            html_tabla_experiencia_laboral += "</tr>";

            html_tabla_experiencia_laboral += "</table>";

            contador += 1;
        }

        html_formato_entrevista = html_formato_entrevista.Replace("[TABLA_EXPERIENCIA_LABORAL]", html_tabla_experiencia_laboral);

        //ahora: si es entrevista por produccion se debe determinar que pruebas se han aplicado al candidato
        //y mostrarlas en el informe de seleccion
        //lo mismo con habilidades / competencias
        
        pruebaPerfil _pruebaPerfil = new pruebaPerfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        FabricaAssesment _fabrica = new FabricaAssesment(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaPruebas = new DataTable();
        DataTable tablaAssesmentCenter = new DataTable();
        DataTable tablaCompetencias = new DataTable();
        String TIPO_ENTREVISTA = String.Empty;
        if (ID_PERFIL <= 0)
        {
            //ES ENTREVISTA POR PRODUCCION
            tablaPruebas = _pruebaPerfil.ObtenerAplicadasAIdSolicitudConResultados(ID_SOLICITUD);
            tablaCompetencias = _fabrica.ObtenerAplicacionCompetenciasPorSolicitudIngreso(ID_SOLICITUD);
            if (tablaCompetencias.Rows.Count <= 0)
            {
                TIPO_ENTREVISTA = "B";
            }   
            else
            {
                TIPO_ENTREVISTA = "A";
            }
        }
        else
        {
            //es entrevista asociada a perfil entonces cargamos lo pertinente a ese perfil
            tablaPruebas = _pruebaPerfil.ObtenerPorIdPerfilConResultadosIdSolicitud(ID_PERFIL, ID_SOLICITUD);

            if (ID_ASSESMENT_CENTER > 0)
            {
                TIPO_ENTREVISTA = "A";
                //se tiene un assesmentcenter associado
                tablaAssesmentCenter = _fabrica.ObtenerAssesmentCentePorId(ID_ASSESMENT_CENTER);
                tablaCompetencias = _fabrica.ObtenerCompetenciasAssesmentCenteActivos(ID_ASSESMENT_CENTER, ID_SOLICITUD);
            }
            else
            {
                TIPO_ENTREVISTA = "B";
            }
        }

        //AHORA SI SEGUN LOS ObTENIDOS DE LAS TABLAS SE MUESTRA EN EL INFORME.
        if (tablaPruebas.Rows.Count > 0)
        {
            String html_resultados_pruebas;
            html_resultados_pruebas = "<br />";
            html_resultados_pruebas += "<div style=\"text-align: left; margin: 0 0 0 20px; text-decoration: underline; font-weight: bold;\">";
            html_resultados_pruebas += "RESULTADO DE PRUEBAS PSICOTÉCNICAS";
            html_resultados_pruebas += "</div>";

            for (int i = 0; i < tablaPruebas.Rows.Count; i++)
            {
                DataRow filaPrueba = tablaPruebas.Rows[i];

                String NOM_PRUEBA = filaPrueba["NOM_PRUEBA"].ToString().Trim();
                String RESULTADO = filaPrueba["RESULTADOS"].ToString().Trim();

                html_resultados_pruebas += "<br />";
                html_resultados_pruebas += "<div style=\"text-align: left; margin: 0 0 0 20px; font-weight: bold; font-size:9px;\">";
                html_resultados_pruebas += NOM_PRUEBA;
                html_resultados_pruebas += "</div>";
                html_resultados_pruebas += "<div style=\"text-align: justify;\">";
                if (String.IsNullOrEmpty(RESULTADO) == false)
                {
                    html_resultados_pruebas += RESULTADO;
                }
                else
                {
                    html_resultados_pruebas += "Desconocido.";
                }
                html_resultados_pruebas += "</div>";
            }

            html_resultados_pruebas += "<br />";
            html_formato_entrevista = html_formato_entrevista.Replace("[TABLA_RESULTADO_PRUEBAS]", html_resultados_pruebas);
        }
        else
        {
            html_formato_entrevista = html_formato_entrevista.Replace("[TABLA_RESULTADO_PRUEBAS]", "<br />");
        }

        //FORMATO DE ASSESMENT CENTER
        if (TIPO_ENTREVISTA.Contains("A") == true)
        {
            String NOMBRE_ASSESMENT = "";
            if (tablaAssesmentCenter.Rows.Count > 0)
            {
                DataRow filaAssesment = tablaAssesmentCenter.Rows[0];
                NOMBRE_ASSESMENT = filaAssesment["NOMBRE_ASSESMENT"].ToString().Trim();
            }

            //En esta variable cargamos el documento plantilla por habilidades
            archivo_original = new StreamReader(Server.MapPath(@"~\plantillas_reportes\formato_evaluacion_assesmentcenter.htm"));

            String html_formato_assesment_center = archivo_original.ReadToEnd();

            archivo_original.Dispose();
            archivo_original.Close();

            String html_tabla_assesment = "<table border=\"1\" cellpadding=\"1\" cellspacing=\"0\" width=\"100%\" align=\"center\" style=\"font-size:8px; line-height:9px;\">";
            html_tabla_assesment += "<tr>";
            html_tabla_assesment += "<td width=\"25%\" style=\"text-align:center; font-weight: bold;\">";
            html_tabla_assesment += "COMPETENCIA / HABILIDAD";
            html_tabla_assesment += "</td>";
            html_tabla_assesment += "<td width=\"35%\" style=\"text-align:center; font-weight: bold;\">";
            html_tabla_assesment += "CALIFICACIÓN";
            html_tabla_assesment += "</td>";
            html_tabla_assesment += "<td width=\"40%\" style=\"text-align:center; font-weight: bold; \">";
            html_tabla_assesment += "OBSERVACIONES";
            html_tabla_assesment += "</td>";
            html_tabla_assesment += "</tr>";

            foreach (DataRow filaCalificacionCompetencias in tablaCompetencias.Rows)
            {
                html_tabla_assesment += "<tr>";
                html_tabla_assesment += "<td width=\"25%\" style=\"text-align:left; \">";
                html_tabla_assesment += filaCalificacionCompetencias["COMPETENCIA"].ToString().Trim();
                html_tabla_assesment += "</td>";
                html_tabla_assesment += DevuelveTdsConCalificacionSegunDataRow(filaCalificacionCompetencias);
                html_tabla_assesment += "<td width=\"40%\" style=\"text-align:justify; \">";
                html_tabla_assesment += filaCalificacionCompetencias["OBSERVACIONES"].ToString().Trim();
                html_tabla_assesment += "</td>";
            }

            html_tabla_assesment += "</table>";

            html_formato_assesment_center = html_formato_assesment_center.Replace("[NOMBRE_ASSESMENT]", NOMBRE_ASSESMENT);
            html_formato_assesment_center = html_formato_assesment_center.Replace("[TABLA_DE_ASSESMENT_CENTER]", html_tabla_assesment);
            html_formato_entrevista = html_formato_entrevista.Replace("[FORMATO_EVALUACION_ASSESMENTCENTER]", html_formato_assesment_center);
        }
        else
        {
            html_formato_entrevista = html_formato_entrevista.Replace("[FORMATO_EVALUACION_ASSESMENTCENTER]", "");
        }

        // concepto general
        html_concepto = html_concepto.Replace("[CONCEPTO_GENERAL]", CONCEPTO_GENERAL);
        // concepto general 
        html_formato_entrevista = html_formato_entrevista.Replace("[CONCEPTO_GENERAL]", html_concepto);




        // USUARIO QUE HIZO LA ENTREVISTA
        usuario _usuario = new usuario(Session["idEmpresa"].ToString());
        DataTable tablaUsuario = _usuario.ObtenerUsuarioPorUsuLog(USUARIO_ENTREVISTADOR); //ACA VA ES EL DE LA ENTREVISTA
        DataRow filaUsuario = tablaUsuario.Rows[0];

        if (filaUsuario["USU_TIPO"].ToString().ToUpper() == "PLANTA")
        {
            html_formato_entrevista = html_formato_entrevista.Replace("[NOMBRE_PSICOLOGO]", filaUsuario["NOMBRES"].ToString().Trim() + " " + filaUsuario["APELLIDOS"].ToString().Trim());
        }
        else
        {
            html_formato_entrevista = html_formato_entrevista.Replace("[NOMBRE_PSICOLOGO]", filaUsuario["NOMBRES_EXTERNO"].ToString().Trim() + " " + filaUsuario["APELLIDOS_EXTERNO"].ToString().Trim());
        }

        html_formato_entrevista = html_formato_entrevista.Replace("[CARGO_SICOLOGO]", "Psicólogo de Selección");


        html_formato_entrevista += html_pie;


        //creamos un configuramos el documento de pdf
        //(tamaño de la hoja,margen izq, margen der, margin arriba margen abajo)
        iTextSharp.text.Document document = new iTextSharp.text.Document(new Rectangle(595, 842), 50, 50, 80, 45);

        using (MemoryStream streamArchivo = new MemoryStream())
        {
            iTextSharp.text.pdf.PdfWriter writer = PdfWriter.GetInstance(document, streamArchivo);

            // Our custom Header and Footer is done using Event Handler
            pdfEvents PageEventHandler = new pdfEvents();
            writer.PageEvent = PageEventHandler;

            // Define the page header
            // Define the page header
            if (Session["idEmpresa"].ToString() == "1")
            {
                PageEventHandler.dirImagenHeader = Server.MapPath("~/imagenes/reportes/logo_sertempo.png");
            }
            else
            {
                PageEventHandler.dirImagenHeader = Server.MapPath("~/imagenes/reportes/logo_eficiencia.png");
            }

            PageEventHandler.fechaImpresion = DateTime.Now;
            PageEventHandler.tipoDocumento = "entrevista";

            document.Open();

            //capturamos el archivo temporal del response
            String tempFile = Path.GetTempFileName();

            //y lo llenamos con el html de la plantilla
            using (StreamWriter tempwriter = new StreamWriter(tempFile, false))
            {
                tempwriter.Write(html_formato_entrevista);
            }

            //leeemos el archivo temporal y lo colocamos en el documento de pdf
            List<IElement> htmlarraylist = HTMLWorker.ParseToList(new StreamReader(tempFile), new StyleSheet());

            foreach (IElement element in htmlarraylist)
            {
                if (element.Chunks.Count > 0)
                {
                    if (element.Chunks[0].Content == "linea para paginacion de pdf")
                    {
                        document.NewPage();
                    }
                    else
                    {
                        document.Add(element);
                    }
                }
                else
                {
                    document.Add(element);
                }
            }

            //limpiamos todo
            document.Close();

            writer.Close();

            return streamArchivo.ToArray();
        }
    }

    private String DevuelveTdsConCalificacionSegunDataRow(DataRow filaCalificacion)
    {
        String html_tabla_habilidades;


        if (filaCalificacion["CALIFICACION"].ToString().Trim() == "CUMPLE")
        {
            Decimal NIVEL_CALIFICAICON = 0;
            try
            {
                NIVEL_CALIFICAICON = Convert.ToDecimal(filaCalificacion["NIVEL_CALIFICACION"]);
            }
            catch
            {
                NIVEL_CALIFICAICON = 0;
            }

            html_tabla_habilidades = "<td width=\"35%\" style=\"text-align:center; \">";
            html_tabla_habilidades += "<b>CUMPLE</b> con un nivel de " + NIVEL_CALIFICAICON.ToString() + "/5 (" + ((NIVEL_CALIFICAICON / 5)* 100).ToString().Trim() + "%).";
            html_tabla_habilidades += "</td>";

            return html_tabla_habilidades;
        }
        else
        {
            if (filaCalificacion["CALIFICACION"].ToString().Trim() == "NO CUMPLE")
            {
                html_tabla_habilidades = "<td width=\"35%\" style=\"text-align:center; font-weight: bold; \">";
                html_tabla_habilidades += "NO CUMPLE";
                html_tabla_habilidades += "</td>";

                return html_tabla_habilidades;
            }
            else
            {
                html_tabla_habilidades = "<td width=\"35%\" style=\"text-align:center; font-weight: bold; \">";
                html_tabla_habilidades += "DESCONOCIDA";
                html_tabla_habilidades += "</td>";

                return html_tabla_habilidades;
            }
        }
    }

    private void CargarSubProgramasYActividadesDeProgramaGeneralEnLista(Decimal ID_PROGRAMA_GENERAL, Decimal ID_DETALLE_GENERAL_PADRE, Dictionary<String, infoNodoProgramaGeneral> diccionario, String numeracion)
    {
        int contador = 1;

        Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaDetallesProgramaGeneral = _programa.ObtenerDetallesProgramaGeneralPorIdPadre(ID_PROGRAMA_GENERAL, ID_DETALLE_GENERAL_PADRE);

        //recorremos agregando los subprogramas al nodo padre
        foreach (DataRow fila in tablaDetallesProgramaGeneral.Rows)
        {
            if (fila["TIPO"].ToString().Trim() == TiposNodoProgramaGeneral.SUBPROGRAMA.ToString())
            {
                infoNodoProgramaGeneral nodo = new infoNodoProgramaGeneral();

                nodo.TIPO_NODO = TiposNodoProgramaGeneral.SUBPROGRAMA;
                nodo.SUB_DESCRIPCION = fila["DESCRIPCION_SUB_PROGRAMA"].ToString().Trim();
                nodo.SUB_NOMBRE = fila["NOMBRE_SUB_PROGRAMA"].ToString().Trim();

                Decimal ID_DETALLE_GENERAL = Convert.ToDecimal(fila["ID_DETALLE_GENERAL"]);
                
                if (numeracion == null)
                {
                    diccionario.Add(contador.ToString() + ":" + TiposNodoProgramaGeneral.SUBPROGRAMA.ToString(), nodo);
                    CargarSubProgramasYActividadesDeProgramaGeneralEnLista(ID_PROGRAMA_GENERAL, ID_DETALLE_GENERAL, diccionario, contador.ToString());
                }
                else 
                { 
                    diccionario.Add(numeracion + "." + contador.ToString() + ":" + TiposNodoProgramaGeneral.SUBPROGRAMA.ToString(), nodo);
                    CargarSubProgramasYActividadesDeProgramaGeneralEnLista(ID_PROGRAMA_GENERAL, ID_DETALLE_GENERAL, diccionario, numeracion + "." + contador.ToString());
                }
            }

            contador += 1;
        }

        //RECORREMOS AGREGANDO AHORA LAS ACTIVIDADES
        contador = 1;
        foreach (DataRow fila in tablaDetallesProgramaGeneral.Rows)
        {
            if (fila["TIPO"].ToString().Trim() == TiposNodoProgramaGeneral.ACTIVIDAD.ToString())
            {
                infoNodoProgramaGeneral nodo = new infoNodoProgramaGeneral();

                nodo.TIPO_NODO = TiposNodoProgramaGeneral.ACTIVIDAD;
                nodo.ACT_DESCRIPCION = fila["DESCRIPCION_ACTIVIDAD"].ToString().Trim();
                nodo.ACT_NOMBRE = fila["NOMBRE_ACTIVIDAD"].ToString().Trim();
                nodo.ACT_SECTOR = fila["SECTOR_ACTIVIDAD"].ToString().Trim();
                nodo.ACT_TIPO = fila["TIPO_ACTIVIDAD"].ToString().Trim();
                
                if (numeracion == null)
                {
                    diccionario.Add(contador.ToString() + ":" + TiposNodoProgramaGeneral.ACTIVIDAD.ToString(), nodo);
                }
                else
                {
                    diccionario.Add(numeracion + "." + contador.ToString() + ":" + TiposNodoProgramaGeneral.ACTIVIDAD.ToString(), nodo);
                }
            }
            contador += 1;
        }
    }

    private String GetTablaHerramientas(Int32 nivel, String numeracion, String tipo_herramienta, infoNodoProgramaGeneral infoNodo)
    { 
        Int32 TAMANO_TABLA = 0;
        if (nivel == 1)
        {
            TAMANO_TABLA = 100;
        }
        else
        {
            Int32 porcentaje = 5 * (nivel - 1);
            TAMANO_TABLA = 100 - porcentaje;
        }

        String html_tabla = "<table border=\"0\" cellpadding=\"1\" cellspacing=\"0\" width=\"[TAMANO_TABLA]%\" align=\"right\">";
        html_tabla += "<tr>";
        html_tabla += "<td width=\"[TAMANO_TD_NUMERACION]\" valign=\"top\" style=\"text-align:left;\">";
        html_tabla += "[NUMERACION]";
        html_tabla += "</td>";
        html_tabla += "<td style=\"text-align: justify;\">";
        html_tabla += "[TEXTO]";
        html_tabla += "</td>";
        html_tabla += "</tr>";
        html_tabla += "</table>";

        html_tabla = html_tabla.Replace("[TAMANO_TABLA]", TAMANO_TABLA.ToString());

        if (tipo_herramienta == "SUBPROGRAMA") 
        { 
            html_tabla = html_tabla.Replace("[NUMERACION]", numeracion);
            html_tabla = html_tabla.Replace("[TEXTO]", "<b>" + infoNodo.SUB_NOMBRE + ":</b> " + infoNodo.SUB_DESCRIPCION);
            html_tabla = html_tabla.Replace("[TAMANO_TD_NUMERACION]", "5");
        }
        else 
        {
            html_tabla = html_tabla.Replace("[NUMERACION]", "•");
            html_tabla = html_tabla.Replace("[TEXTO]", "<b>" + infoNodo.ACT_TIPO + " - " + infoNodo.ACT_NOMBRE + ":</b> " + infoNodo.ACT_DESCRIPCION + "<b>SECTOR:</b> " + infoNodo.ACT_SECTOR);
            html_tabla = html_tabla.Replace("[TAMANO_TD_NUMERACION]", "3");
        }

        return html_tabla;
    }

    public byte[] GenerarProgramaGeneral(Decimal ID_PROGRAMA_GENERAL, String URL_SERVER)
    {
        tools _tools = new tools();

        //OBTENEMOS LA MAESTRA DEL PROGRAMA GENERAL
        Programa _programa = new Programa(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaPrograma = _programa.ObtenerPrograGeneralMaestraPorId(ID_PROGRAMA_GENERAL);
        DataRow filaPrograma = tablaPrograma.Rows[0];

        String html_encabezado = "<html>";
        html_encabezado += "<head>";
        html_encabezado += "</head>";
        html_encabezado += "<body>";

        String html_pie = "</body>";
        html_pie += "</html>";

        //En esta variable cargamos el documento plantilla
        StreamReader archivo_original = new StreamReader(Server.MapPath(@"~\plantillas_reportes\programa_general_rse_global.htm"));

        String html_formato_programa = html_encabezado + archivo_original.ReadToEnd();

        archivo_original.Dispose();
        archivo_original.Close();

        String NOMBRE_PROGRAMA = filaPrograma["TITULO"].ToString().Trim();
        
        //String URL_IMAGEN_PROGRAMA = "http://" + Request.ServerVariables["HTTP_HOST"].ToString() + filaPrograma["DIR_IMAGEN"].ToString().Replace(",","");
        //String URL_SERVER
        String URL_IMAGEN_PROGRAMA = "http://" + URL_SERVER + filaPrograma["DIR_IMAGEN"].ToString().Replace("~", "");

        String ANNO_PROGRAMA = filaPrograma["ANNO"].ToString().Trim();

        String PARTE_INICIAL = filaPrograma["TEXTO_CABECERA"].ToString().Trim();
        String PARTE_FINAL = filaPrograma["TEXTO_FINAL"].ToString().Trim();

        html_formato_programa = html_formato_programa.Replace("[NOMBRE_PROGRAMA]", NOMBRE_PROGRAMA);
        html_formato_programa = html_formato_programa.Replace("[URL_IMAGEN_PROGRAMA]", URL_IMAGEN_PROGRAMA);
        html_formato_programa = html_formato_programa.Replace("[ANNO_PROGRAMA]", ANNO_PROGRAMA);

        html_formato_programa = html_formato_programa.Replace("[PARTE_INICIAL]", PARTE_INICIAL);
        html_formato_programa = html_formato_programa.Replace("[PARTE_FINAL]", PARTE_FINAL);


        //-----------------------------------------------------------------------------------
        //ahora vamos a generar una lista apropiada para mostrar los subprograma y actividades
        Dictionary<String, infoNodoProgramaGeneral> diccionarioPrograma = new Dictionary<String, infoNodoProgramaGeneral>();

        CargarSubProgramasYActividadesDeProgramaGeneralEnLista(ID_PROGRAMA_GENERAL, 0, diccionarioPrograma, null);

        String html_titulo_herramientas_programa = "<div style=\"text-align: center; font-weight: bold; font-size:14px;\">";
        html_titulo_herramientas_programa += "HERRAMIENTAS DEL PROGRAMA";
        html_titulo_herramientas_programa += "</div>";
        
        html_titulo_herramientas_programa += "<br>";

        String html_herramientas = String.Empty;
        foreach(KeyValuePair<String, infoNodoProgramaGeneral> herramienta in diccionarioPrograma)
        {
            //key = NUMERO:TIPOHERRAMIENTA
            //   NUMERO = ejemplo [1.2.3] o [1] o [1.1]
            //   TIPOHERRAMIENTA = [SUBPROGRAMA] o [ACTIVIDAD]
            String[] numeracion_tipo = herramienta.Key.Split(':');

            //determinnamos el nivel de numeracion segun el Lenght del arreglo resulktante: ejemplo [1.1.2] es de nivel 3
            //esto es para determinar el nivel de sangria del parrafo
            String[] numeracion_separada = numeracion_tipo[0].Split('.');

            html_herramientas += GetTablaHerramientas(numeracion_separada.Length, numeracion_tipo[0], numeracion_tipo[1], herramienta.Value);
        }
        //-----------------------------------------------------------------------------------

        html_formato_programa = html_formato_programa.Replace("[ESTRUCTURA_PROGRAMA]", html_herramientas);
 
        html_formato_programa += html_pie;

        //creamos un configuramos el documento de pdf
        //(tamaño de la hoja,margen izq, margen der, margin arriba margen abajo)
        iTextSharp.text.Document document = new iTextSharp.text.Document(new Rectangle(595, 842), 50, 50, 80, 45);

        using (MemoryStream streamArchivo = new MemoryStream())
        {
            iTextSharp.text.pdf.PdfWriter writer = PdfWriter.GetInstance(document, streamArchivo);

            // Our custom Header and Footer is done using Event Handler
            pdfEvents PageEventHandler = new pdfEvents();
            writer.PageEvent = PageEventHandler;

            // Define the page header
            // Define the page header
            if (Session["idEmpresa"].ToString() == "1")
            {
                PageEventHandler.dirImagenHeader = Server.MapPath("~/imagenes/reportes/logo_sertempo.png");
            }
            else
            {
                PageEventHandler.dirImagenHeader = Server.MapPath("~/imagenes/reportes/logo_eficiencia.png");
            }

            PageEventHandler.fechaImpresion = DateTime.Now;
            PageEventHandler.tipoDocumento = "entrevista";

            document.Open();

            //capturamos el archivo temporal del response
            String tempFile = Path.GetTempFileName();

            //y lo llenamos con el html de la plantilla
            using (StreamWriter tempwriter = new StreamWriter(tempFile, false))
            {
                tempwriter.Write(html_formato_programa);
            }

            //leeemos el archivo temporal y lo colocamos en el documento de pdf
            List<IElement> htmlarraylist = HTMLWorker.ParseToList(new StreamReader(tempFile), new StyleSheet());

            foreach (IElement element in htmlarraylist)
            {
                if (element.Chunks.Count > 0)
                {
                    if (element.Chunks[0].Content == "linea para paginacion de pdf")
                    {
                        document.NewPage();
                    }
                    else
                    {
                        document.Add(element);
                    }
                }
                else
                {
                    document.Add(element);
                }
            }

            //limpiamos todo
            document.Close();

            writer.Close();

            return streamArchivo.ToArray();
        }

    }

    public byte[] GenerarPDFEntrevistaRetiro(Decimal ID_EMPLEADO, Decimal ID_SOLICITUD, Decimal ID_EMPRESA)
    {
        String USULOG_ENTREVISTA = Session["USU_LOG"].ToString();

        tools _tools = new tools();

        // OBTENEMOS INFORMACION DE USUARIO
        usuario _usuario = new usuario(Session["idEmpresa"].ToString());
        DataTable tablaUsuario = _usuario.ObtenerUsuarioPorUsuLog(USULOG_ENTREVISTA); //ACA VA ES EL DE LA ENTREVISTA
        DataRow filaUsuario = tablaUsuario.Rows[0];

        String NOMBRE_DILIGENCIA = "";
        if (filaUsuario["USU_TIPO"].ToString().ToUpper() == "PLANTA")
        {
            NOMBRE_DILIGENCIA = filaUsuario["NOMBRES"].ToString().Trim() + " " + filaUsuario["APELLIDOS"].ToString().Trim();
        }
        else
        {
            NOMBRE_DILIGENCIA = filaUsuario["NOMBRES_EXTERNO"].ToString().Trim() + " " + filaUsuario["APELLIDOS_EXTERNO"].ToString().Trim();
        }

        //OBTENEMOS LA INFORMACION DEL CLIENTE
        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaCliente = _cliente.ObtenerEmpresaConIdEmpresa(ID_EMPRESA);
        DataRow filaCliente = tablaCliente.Rows[0];

        String RAZ_SOCIAL = filaCliente["RAZ_SOCIAL"].ToString().Trim();
        String NIT_EMPRESA = filaCliente["NIT_EMPRESA"].ToString().Trim() + "-" + filaCliente["DIG_VER"].ToString().Trim();
        String DIR_EMPRESA = filaCliente["DIR_EMP"].ToString().Trim() + " " + filaCliente["ID_CIUDAD_EMPRESA"].ToString().Trim();

        //OBTENEMOS LA INFORMACION DE LA SOLICITUD DE INGRESO
        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaSolicitud = _radicacionHojasDeVida.ObtenerRegSolicitudesingresoPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));
        DataRow filaSolicitud = tablaSolicitud.Rows[0];

        String NOMBRE_ASPIRANTE = filaSolicitud["NOMBRES"].ToString().Trim() + " " + filaSolicitud["APELLIDOS"].ToString().Trim();
        String DOC_IDENTIDAD_ASPIRANTE = filaSolicitud["TIP_DOC_IDENTIDAD"].ToString().Trim() + " " + filaSolicitud["NUM_DOC_IDENTIDAD"].ToString().Trim();
        int EDAD_ASPIRANTE = 0;
        if (DBNull.Value.Equals(filaSolicitud["FCH_NACIMIENTO"]) == false)
        {
            try
            {
                EDAD_ASPIRANTE = _tools.ObtenerEdadDesdeFechaNacimiento(Convert.ToDateTime(filaSolicitud["FCH_NACIMIENTO"]));
            }
            catch
            {
                EDAD_ASPIRANTE = 0;
            }
        }
        String DIRECCION_ASPIRANTE = filaSolicitud["DIR_ASPIRANTE"].ToString().Trim();
        String CIUDAD_ASPIRANTE = filaSolicitud["NOMBRE_CIUDAD"].ToString().Trim();
        String SECTOR_ASPIRANTE = filaSolicitud["SECTOR"].ToString();
        String TELEFONOS_ASPIRANTE = filaSolicitud["TEL_ASPIRANTE"].ToString();
        String EMAIL_ASPIRANTE = filaSolicitud["E_MAIL"].ToString().Trim();

       
        //DATOS DEL MOTIVO DE RETIRO
        MotivoRotacionRetiro _motivo = new MotivoRotacionRetiro(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaResultadosEntrevista = _motivo.ObtenerResultadosEntrevistaDeRetiroParaEmpleado(ID_EMPLEADO);

        //CREAMOS LA TABLA DE MOTIVOS DE RETIRO
        Boolean yaSeTieneObservaciones = false;
        String OBSERVACIONES = null;

        String html_tabla_motivos = "<table border=\"1\" cellpadding=\"1\" cellspacing=\"0\" width=\"95%\" align=\"center\" style=\"font-size:8px; line-height:9px;\">";
        html_tabla_motivos += "<tr>";
        html_tabla_motivos += "<td width=\"50%\" style=\"text-align:center;\">";
        html_tabla_motivos += "CATEGORÍA";
        html_tabla_motivos += "</td>";
        html_tabla_motivos += "<td width=\"50%\" style=\"text-align:center;\">";
        html_tabla_motivos += "MOTIVO";
        html_tabla_motivos += "</td>";
        html_tabla_motivos += "</tr>";
        foreach (DataRow filaMotivo in tablaResultadosEntrevista.Rows)
        {
            if (yaSeTieneObservaciones == false)
            {
                //SOLO UNA VEZ
                OBSERVACIONES = filaMotivo["OBSERVACIONES"].ToString().Trim();
                yaSeTieneObservaciones = true;
            }

            Decimal ID_DETALLE_ROTACION_EMPLEADO = Convert.ToDecimal(filaMotivo["ID_DETALLE_ROTACION_EMPLEADO"]);
            Decimal ID_MAESTRA_ROTACION_EMPLEADO = Convert.ToDecimal(filaMotivo["ID_MAESTRA_ROTACION_EMPLEADO"]);
            Decimal ID_ROTACION_EMPRESA = Convert.ToDecimal(filaMotivo["ID_ROTACION_EMPRESA"]);

            //OBTENEMOS DATOS COMPLEMENTARIOS POR MEDIO DE ID_ROTACION_EMPRESA
            DataTable tablaInfoComplementaria = _motivo.ObtenerMotivoEmpresaPorId(ID_ROTACION_EMPRESA);
            DataRow filaInfoComplementaria = tablaInfoComplementaria.Rows[0];

            html_tabla_motivos += "<tr>";
            html_tabla_motivos += "<td width=\"50%\" style=\"text-align:justify;\">";
            html_tabla_motivos += filaInfoComplementaria["TITULO_MAESTRA_ROTACION"];
            html_tabla_motivos += "</td>";
            html_tabla_motivos += "<td width=\"50%\" style=\"text-align:justify;\">";
            html_tabla_motivos += filaInfoComplementaria["TITULO"];
            html_tabla_motivos += "</td>";
            html_tabla_motivos += "</tr>";
        }
        html_tabla_motivos += "</table>";


        /*
         * Generación del archi de informe de entrevista de retiro
         * Stream con el contenido del pdf.
        */
        String html_encabezado = "<html>";
        html_encabezado += "<head>";
        html_encabezado += "</head>";
        html_encabezado += "<body>";

        String html_pie = "</body>";
        html_pie += "</html>";


        //En esta variable cargamos el documento plantilla
        StreamReader archivo_original = new StreamReader(Server.MapPath(@"~\plantillas_reportes\entrevista_retiro.htm"));

        String html_formato_entrevista = html_encabezado + archivo_original.ReadToEnd();

        archivo_original.Dispose();
        archivo_original.Close();

        html_formato_entrevista = html_formato_entrevista.Replace("[RAZ_SOCIAL]", RAZ_SOCIAL);
        html_formato_entrevista = html_formato_entrevista.Replace("[NIT_EMPRESA]", NIT_EMPRESA);
        html_formato_entrevista = html_formato_entrevista.Replace("[DIR_EMPRESA]", DIR_EMPRESA);

        html_formato_entrevista = html_formato_entrevista.Replace("[NOMBRE_ASPIRANTE]", NOMBRE_ASPIRANTE);
        html_formato_entrevista = html_formato_entrevista.Replace("[DOC_IDENTIDAD_ASPIRANTE]", DOC_IDENTIDAD_ASPIRANTE);
        if (EDAD_ASPIRANTE > 0)
        {
            html_formato_entrevista = html_formato_entrevista.Replace("[EDAD_ASPIRANTE]", EDAD_ASPIRANTE.ToString() + " Años.");
        }
        else
        {
            html_formato_entrevista = html_formato_entrevista.Replace("[EDAD_ASPIRANTE]", "Desconocida.");
        }
        html_formato_entrevista = html_formato_entrevista.Replace("[DIRECCION_ASPIRANTE]", DIRECCION_ASPIRANTE);
        html_formato_entrevista = html_formato_entrevista.Replace("[CIUDAD_ASPIRANTE]", CIUDAD_ASPIRANTE);
        html_formato_entrevista = html_formato_entrevista.Replace("[SECTOR_ASPIRANTE]", SECTOR_ASPIRANTE);
        html_formato_entrevista = html_formato_entrevista.Replace("[TELEFONOS_ASPIRANTE]", TELEFONOS_ASPIRANTE);
        html_formato_entrevista = html_formato_entrevista.Replace("[EMAIL_ASPIRANTE]", EMAIL_ASPIRANTE);

        html_formato_entrevista = html_formato_entrevista.Replace("[TABLA_MOTIVOS_RETIRO]", html_tabla_motivos);

        html_formato_entrevista = html_formato_entrevista.Replace("[OBSERVACIONES]", OBSERVACIONES);

        html_formato_entrevista = html_formato_entrevista.Replace("[NOMBRE_DILIGENCIA]", NOMBRE_DILIGENCIA);

        html_formato_entrevista += html_pie;


        //creamos un configuramos el documento de pdf
        //(tamaño de la hoja,margen izq, margen der, margin arriba margen abajo)
        iTextSharp.text.Document document = new iTextSharp.text.Document(new Rectangle(595, 842), 50, 50, 80, 45);

        using (MemoryStream streamArchivo = new MemoryStream())
        {
            iTextSharp.text.pdf.PdfWriter writer = PdfWriter.GetInstance(document, streamArchivo);

            // Our custom Header and Footer is done using Event Handler
            pdfEvents PageEventHandler = new pdfEvents();
            writer.PageEvent = PageEventHandler;

            // Define the page header
            // Define the page header
            if (Session["idEmpresa"].ToString() == "1")
            {
                PageEventHandler.dirImagenHeader = Server.MapPath("~/imagenes/reportes/logo_sertempo.png");
            }
            else
            {
                PageEventHandler.dirImagenHeader = Server.MapPath("~/imagenes/reportes/logo_eficiencia.png");
            }

            PageEventHandler.fechaImpresion = DateTime.Now;
            PageEventHandler.tipoDocumento = "entrevista_retiro";

            document.Open();

            //capturamos el archivo temporal del response
            String tempFile = Path.GetTempFileName();

            //y lo llenamos con el html de la plantilla
            using (StreamWriter tempwriter = new StreamWriter(tempFile, false))
            {
                tempwriter.Write(html_formato_entrevista);
            }

            //leeemos el archivo temporal y lo colocamos en el documento de pdf
            List<IElement> htmlarraylist = HTMLWorker.ParseToList(new StreamReader(tempFile), new StyleSheet());

            foreach (IElement element in htmlarraylist)
            {
                if (element.Chunks.Count > 0)
                {
                    if (element.Chunks[0].Content == "linea para paginacion de pdf")
                    {
                        document.NewPage();
                    }
                    else
                    {
                        document.Add(element);
                    }
                }
                else
                {
                    document.Add(element);
                }
            }

            //limpiamos todo
            document.Close();

            writer.Close();

            return streamArchivo.ToArray();
        }
    }

    /// <summary>
    /// hecha por cesar pulido
    /// el dia 15 de abril de 2013
    /// para generar pdf de cuenta apertura
    /// </summary>
    /// <param name="FECHA_APERTURA"></param>
    /// <param name="NOMBRE_BANCO"></param>
    /// <param name="NOMBRE_TRABAJADOR"></param>
    /// <param name="NUMERO_DOCUMENTO_IDENTIDAD"></param>
    /// <param name="NOMBRE_JEFE"></param>
    /// <param name="CARGO_JEFE"></param>
    /// <param name="TELEFONO_JEFE"></param>
    /// <param name="NUMERO_ROTATIVO"></param>
    /// <param name="CODIGO_NOMINA"></param>
    /// <param name="NUMERO_CUENTA"></param>
    /// <returns></returns>
    public byte[] GenerarPDFAperturaBancoBogotaCreditRotativo(DateTime FECHA_APERTURA,
        String NOMBRE_BANCO,
        String NOMBRE_TRABAJADOR,
        String NUMERO_DOCUMENTO_IDENTIDAD,
        String NOMBRE_JEFE,
        String CARGO_JEFE,
        String TELEFONO_JEFE,
        String NUMERO_ROTATIVO,
        String CODIGO_NOMINA, 
        String NUMERO_CUENTA)
    {
        String html_encabezado = "<html>";
        html_encabezado += "<head>";
        html_encabezado += "</head>";
        html_encabezado += "<body>";

        String html_pie = "</body>";
        html_pie += "</html>";


        //variables obtenidas de la session
        String EMPRESA;
        String NIT_EMPRESA;
        if (Session["idEmpresa"].ToString() == "1")
        {
            EMPRESA = tabla.VAR_NOMBRE_SERTEMPO;
            NIT_EMPRESA = tabla.VAR_NIT_SERTEMPO;
        }
        else
        {
            EMPRESA = tabla.VAR_NOMBRE_EYS;
            NIT_EMPRESA = tabla.VAR_NIT_EYS;
        }

        //En esta variable cargamos el documento plantilla
        StreamReader archivo_original = new StreamReader(Server.MapPath(@"~\plantillas_reportes\apertura_cuenta.htm"));

        String html_formato_apertura = html_encabezado + archivo_original.ReadToEnd();

        archivo_original.Dispose();
        archivo_original.Close();

        html_formato_apertura = html_formato_apertura.Replace("[FECHA_APERTURA]", FECHA_APERTURA.ToLongDateString());
        html_formato_apertura = html_formato_apertura.Replace("[NOMBRE_BANCO]", NOMBRE_BANCO);
        html_formato_apertura = html_formato_apertura.Replace("[NOMBRE_TRABAJADOR]", NOMBRE_TRABAJADOR);
        html_formato_apertura = html_formato_apertura.Replace("[NUMERO_DOCUMENTO_IDENTIDAD]", NUMERO_DOCUMENTO_IDENTIDAD);
        html_formato_apertura = html_formato_apertura.Replace("[EMPRESA]", EMPRESA);
        html_formato_apertura = html_formato_apertura.Replace("[NIT_EMPRESA]", NIT_EMPRESA);
        html_formato_apertura = html_formato_apertura.Replace("[NUMERO_ROTATIVO]", NUMERO_ROTATIVO);
        html_formato_apertura = html_formato_apertura.Replace("[CODIGO_NOMINA]", CODIGO_NOMINA);
        html_formato_apertura = html_formato_apertura.Replace("[NUMERO_CUENTA]", NUMERO_CUENTA);

        html_formato_apertura = html_formato_apertura.Replace("[NOMBRE_JEFE]", NOMBRE_JEFE);
        html_formato_apertura = html_formato_apertura.Replace("[CARGO_JEFE]", CARGO_JEFE);
        html_formato_apertura = html_formato_apertura.Replace("[TELEFONO_JEFE]", TELEFONO_JEFE);

        html_formato_apertura += html_pie;


        //creamos un configuramos el documento de pdf
        //(tamaño de la hoja,margen izq, margen der, margin arriba margen abajo)
        iTextSharp.text.Document document = new iTextSharp.text.Document(new Rectangle(595, 842), 65, 65, 160, 45);

        using (MemoryStream streamArchivo = new MemoryStream())
        {
            iTextSharp.text.pdf.PdfWriter writer = PdfWriter.GetInstance(document, streamArchivo);

            // Our custom Header and Footer is done using Event Handler
            pdfEvents PageEventHandler = new pdfEvents();
            writer.PageEvent = PageEventHandler;

            // Define the page header
            // Define the page header
            if (Session["idEmpresa"].ToString() == "1")
            {
                PageEventHandler.dirImagenHeader = Server.MapPath("~/imagenes/reportes/logo_sertempo.png");
            }
            else
            {
                PageEventHandler.dirImagenHeader = Server.MapPath("~/imagenes/reportes/logo_eficiencia.png");
            }

            PageEventHandler.fechaImpresion = DateTime.Now;
            PageEventHandler.tipoDocumento = "apertura_cuenta";

            document.Open();

            //capturamos el archivo temporal del response
            String tempFile = Path.GetTempFileName();

            //y lo llenamos con el html de la plantilla
            using (StreamWriter tempwriter = new StreamWriter(tempFile, false))
            {
                tempwriter.Write(html_formato_apertura);
            }

            //leeemos el archivo temporal y lo colocamos en el documento de pdf
            List<IElement> htmlarraylist = HTMLWorker.ParseToList(new StreamReader(tempFile), new StyleSheet());

            foreach (IElement element in htmlarraylist)
            {
                if (element.Chunks.Count > 0)
                {
                    if (element.Chunks[0].Content == "linea para paginacion de pdf")
                    {
                        document.NewPage();
                    }
                    else
                    {
                        document.Add(element);
                    }
                }
                else
                {
                    document.Add(element);
                }
            }

            //limpiamos todo
            document.Close();

            writer.Close();

            return streamArchivo.ToArray();
        }
    }

    public byte[] GenerarPDFAperturaBancoBancolombia(String CIUDAD, 
        DateTime FECHA_APERTURA,
        String NOMBRE_BANCO,
        String NUM_CONVENIO,
        String CUENTA_SERTEMPO,
        String NOMBRE_COMPLETO_EMPLEADO,
        String NUMERO_IDENTIFICACION_EMPLEADO,
        String CARGO_EMPLEADO,
        Decimal ID_REQUERIMIENTO,
        String EMPRESA_USUARIA,
        String NOMBRE_EMPRESA,

        String NOMBRE_JEFE)
    {
        String SUELDO_BASICO;
        String html_encabezado = "<html>";
        html_encabezado += "<head>";
        html_encabezado += "</head>";
        html_encabezado += "<body>";

        String html_pie = "</body>";
        html_pie += "</html>";

        //obtenermos de la requisicion el salario
        //cargo, 
        //tipo contrato,
        //y no me acuerdo que mas.
        requisicion _req = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaReq = _req.ObtenerComRequerimientoPorIdRequerimiento(ID_REQUERIMIENTO);
        DataRow filaReq = tablaReq.Rows[0];

        try
        {
            SUELDO_BASICO = Convert.ToDecimal(filaReq["SALARIO"]).ToString("C");
        }
        catch
        {
            SUELDO_BASICO = "Desconocido.";
        }


        ////variables obtenidas de la session
        //String EMPRESA;
        //String NIT_EMPRESA;
        //if (Session["idEmpresa"].ToString() == "1")
        //{
        //    EMPRESA = tabla.VAR_NOMBRE_SERTEMPO;
        //    NIT_EMPRESA = tabla.VAR_NIT_SERTEMPO;
        //}
        //else
        //{
        //    EMPRESA = tabla.VAR_NOMBRE_EYS;
        //    NIT_EMPRESA = tabla.VAR_NIT_EYS;
        //}

        //En esta variable cargamos el documento plantilla
        StreamReader archivo_original = new StreamReader(Server.MapPath(@"~\plantillas_reportes\apertura_cuenta_bancolombia.htm"));

        String html_formato_apertura = html_encabezado + archivo_original.ReadToEnd();

        archivo_original.Dispose();
        archivo_original.Close();

        html_formato_apertura = html_formato_apertura.Replace("[CIUDAD]", CIUDAD);
        html_formato_apertura = html_formato_apertura.Replace("[FECHA_APERTURA]", FECHA_APERTURA.ToLongDateString());
        html_formato_apertura = html_formato_apertura.Replace("[NOMBRE_BANCO]", NOMBRE_BANCO);
        html_formato_apertura = html_formato_apertura.Replace("[NUM_CONVENIO]", NUM_CONVENIO);
        html_formato_apertura = html_formato_apertura.Replace("[CUENTA_SERTEMPO]", CUENTA_SERTEMPO);
        html_formato_apertura = html_formato_apertura.Replace("[NOMBRE_COMPLETO_EMPLEADO]", NOMBRE_COMPLETO_EMPLEADO);
        html_formato_apertura = html_formato_apertura.Replace("[NUMERO_IDENTIFICACION_EMPLEADO]", NUMERO_IDENTIFICACION_EMPLEADO);
        html_formato_apertura = html_formato_apertura.Replace("[CARGO_EMPLEADO]", CARGO_EMPLEADO);
        html_formato_apertura = html_formato_apertura.Replace("[SUELDO_BASICO]", SUELDO_BASICO);
        html_formato_apertura = html_formato_apertura.Replace("[EMPRESA_USUARIA]", EMPRESA_USUARIA);
        html_formato_apertura = html_formato_apertura.Replace("[NOMBRE_JEFE]", NOMBRE_JEFE);

        html_formato_apertura += html_pie;


        //creamos un configuramos el documento de pdf
        //(tamaño de la hoja,margen izq, margen der, margin arriba margen abajo)
        iTextSharp.text.Document document = new iTextSharp.text.Document(new Rectangle(595, 842), 65, 65, 160, 45);

        using (MemoryStream streamArchivo = new MemoryStream())
        {
            iTextSharp.text.pdf.PdfWriter writer = PdfWriter.GetInstance(document, streamArchivo);

            // Our custom Header and Footer is done using Event Handler
            pdfEvents PageEventHandler = new pdfEvents();
            writer.PageEvent = PageEventHandler;

            // Define the page header
            // Define the page header
            if (Session["idEmpresa"].ToString() == "1")
            {
                PageEventHandler.dirImagenHeader = Server.MapPath("~/imagenes/reportes/logo_sertempo.png");
            }
            else
            {
                PageEventHandler.dirImagenHeader = Server.MapPath("~/imagenes/reportes/logo_eficiencia.png");
            }

            PageEventHandler.fechaImpresion = DateTime.Now;
            PageEventHandler.tipoDocumento = "apertura_cuenta";

            document.Open();

            //capturamos el archivo temporal del response
            String tempFile = Path.GetTempFileName();

            //y lo llenamos con el html de la plantilla
            using (StreamWriter tempwriter = new StreamWriter(tempFile, false))
            {
                tempwriter.Write(html_formato_apertura);
            }

            //leeemos el archivo temporal y lo colocamos en el documento de pdf
            List<IElement> htmlarraylist = HTMLWorker.ParseToList(new StreamReader(tempFile), new StyleSheet());

            foreach (IElement element in htmlarraylist)
            {
                if (element.Chunks.Count > 0)
                {
                    if (element.Chunks[0].Content == "linea para paginacion de pdf")
                    {
                        document.NewPage();
                    }
                    else
                    {
                        document.Add(element);
                    }
                }
                else
                {
                    document.Add(element);
                }
            }

            //limpiamos todo
            document.Close();

            writer.Close();

            return streamArchivo.ToArray();
        }
    }




    /// <summary>
    /// hecho por cesar puldio
    /// el dia 16 de abril de 2013
    /// para generar pdf de apertura de cuenta
    /// </summary>
    public byte[] GenerarPDFAperturaBancoAvvilla(String CIUDAD,
        DateTime FECHA_APERTURA,
        String NOMBRE_BANCO,
        String NOMBRE_EMPLEADO,
        String NUMERO_IDENTIFICACION_EMPLEADO,
        String NOMBRE_COMPLETO_EMPLEADO,
        Decimal ID_SOLICITUD,
        Decimal ID_REQUERIMIENTO,
        String NOMBRE_JEFE,
        String CARGO_JEFE,
        String CUENTA_MATRIZ)
    {
        String html_encabezado = "<html>";
        html_encabezado += "<head>";
        html_encabezado += "</head>";
        html_encabezado += "<body>";

        String html_pie = "</body>";
        html_pie += "</html>";


        //variables obtenidas de la session
        String NOMBRE_EMPRESA;
        String NIT_EMPRESA;
        if (Session["idEmpresa"].ToString() == "1")
        {
            NOMBRE_EMPRESA = tabla.VAR_NOMBRE_SERTEMPO;
            NIT_EMPRESA = tabla.VAR_NIT_SERTEMPO;
        }
        else
        {
            NOMBRE_EMPRESA = tabla.VAR_NOMBRE_EYS;
            NIT_EMPRESA = tabla.VAR_NIT_EYS;
        }

        String FECHA_INGRESO;
        String SUELDO_BASICO;
        String TIPO_CONTRATO;
        Decimal ID_PERFIL;
        String CARGO_EMPLEADO;

        //FECHA DE INGRESO DESDE SOLICITUD DE INGRESO
        radicacionHojasDeVida _rad = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaRad = _rad.ObtenerRegSolicitudesingresoPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));
        DataRow filaRad = tablaRad.Rows[0];

        try
        {
            FECHA_INGRESO = Convert.ToDateTime(filaRad["F_ING_C"]).ToShortDateString();
        }
        catch
        {
            FECHA_INGRESO = "Desconocida.";
        }


        //obtenermos de la requisicion el salario
        //cargo, 
        //tipo contrato,
        //y no me acuerdo que mas.
        requisicion _req = new requisicion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaReq = _req.ObtenerComRequerimientoPorIdRequerimiento(ID_REQUERIMIENTO);
        DataRow filaReq = tablaReq.Rows[0];

        try
        {
            SUELDO_BASICO = Convert.ToDecimal(filaReq["SALARIO"]).ToString("C");
        }
        catch
        {
            SUELDO_BASICO = "Desconocido.";
        }

        if (DBNull.Value.Equals(filaReq["NOMBRE_HORARIO"]) == false)
        {
            TIPO_CONTRATO = filaReq["NOMBRE_HORARIO"].ToString().Trim();
        }
        else
        {
            TIPO_CONTRATO = "Desconocido.";
        }

        try
        {
            ID_PERFIL = Convert.ToDecimal(filaReq["REGISTRO_PERFIL"]);
            perfil _perfil = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaPerfil = _perfil.ObtenerPorRegistro(ID_PERFIL);
            DataRow filaPerfil = tablaPerfil.Rows[0];

            if (DBNull.Value.Equals(filaPerfil["NOM_OCUPACION"]) == false)
            {
                CARGO_EMPLEADO = filaPerfil["NOM_OCUPACION"].ToString().Trim();
            }
            else
            {
                CARGO_EMPLEADO = "Desconocido.";
            }
        }
        catch
        {
            CARGO_EMPLEADO = "Desconocido.";
        }
        
       

        //En esta variable cargamos el documento plantilla
        StreamReader archivo_original = new StreamReader(Server.MapPath(@"~\plantillas_reportes\apertura_cuenta_avvillas.htm"));

        String html_formato_apertura = html_encabezado + archivo_original.ReadToEnd();

        archivo_original.Dispose();
        archivo_original.Close();

        html_formato_apertura = html_formato_apertura.Replace("[CIUDAD]", CIUDAD);
        html_formato_apertura = html_formato_apertura.Replace("[FECHA_APERTURA]", FECHA_APERTURA.ToLongDateString());
        html_formato_apertura = html_formato_apertura.Replace("[NOMBRE_BANCO]", NOMBRE_BANCO);
        html_formato_apertura = html_formato_apertura.Replace("[NIT_EMPRESA]", NIT_EMPRESA);
        html_formato_apertura = html_formato_apertura.Replace("[NOMBRE_EMPRESA]", NOMBRE_EMPRESA);
        html_formato_apertura = html_formato_apertura.Replace("[CUENTA_MATRIZ]", CUENTA_MATRIZ);

        html_formato_apertura = html_formato_apertura.Replace("[NOMBRE_EMPLEADO]", NOMBRE_EMPLEADO);
        html_formato_apertura = html_formato_apertura.Replace("[NUMERO_IDENTIFICACION_EMPLEADO]", NUMERO_IDENTIFICACION_EMPLEADO);
        html_formato_apertura = html_formato_apertura.Replace("[NOMBRE_COMPLETO_EMPLEADO]", NOMBRE_COMPLETO_EMPLEADO);

        html_formato_apertura = html_formato_apertura.Replace("[FECHA_INGRESO]", FECHA_INGRESO);
        html_formato_apertura = html_formato_apertura.Replace("[SUELDO_BASICO]", SUELDO_BASICO);
        html_formato_apertura = html_formato_apertura.Replace("[TIPO_CONTRATO]", TIPO_CONTRATO);
        html_formato_apertura = html_formato_apertura.Replace("[CARGO_EMPLEADO]", CARGO_EMPLEADO);

        html_formato_apertura = html_formato_apertura.Replace("[NOMBRE_JEFE]", NOMBRE_JEFE);
        html_formato_apertura = html_formato_apertura.Replace("[CARGO_JEFE]", CARGO_JEFE);

        html_formato_apertura += html_pie;


        //creamos un configuramos el documento de pdf
        //(tamaño de la hoja,margen izq, margen der, margin arriba margen abajo)
        iTextSharp.text.Document document = new iTextSharp.text.Document(new Rectangle(595, 842), 55, 55, 80, 45);

        using (MemoryStream streamArchivo = new MemoryStream())
        {
            iTextSharp.text.pdf.PdfWriter writer = PdfWriter.GetInstance(document, streamArchivo);

            // Our custom Header and Footer is done using Event Handler
            pdfEvents PageEventHandler = new pdfEvents();
            writer.PageEvent = PageEventHandler;

            // Define the page header
            // Define the page header
            if (Session["idEmpresa"].ToString() == "1")
            {
                PageEventHandler.dirImagenHeader = Server.MapPath("~/imagenes/reportes/logo_sertempo.png");
            }
            else
            {
                PageEventHandler.dirImagenHeader = Server.MapPath("~/imagenes/reportes/logo_eficiencia.png");
            }

            PageEventHandler.fechaImpresion = DateTime.Now;
            PageEventHandler.tipoDocumento = "apertura_cuenta_avvillas";

            document.Open();

            //capturamos el archivo temporal del response
            String tempFile = Path.GetTempFileName();

            //y lo llenamos con el html de la plantilla
            using (StreamWriter tempwriter = new StreamWriter(tempFile, false))
            {
                tempwriter.Write(html_formato_apertura);
            }

            //leeemos el archivo temporal y lo colocamos en el documento de pdf
            List<IElement> htmlarraylist = HTMLWorker.ParseToList(new StreamReader(tempFile), new StyleSheet());

            foreach (IElement element in htmlarraylist)
            {
                if (element.Chunks.Count > 0)
                {
                    if (element.Chunks[0].Content == "linea para paginacion de pdf")
                    {
                        document.NewPage();
                    }
                    else
                    {
                        document.Add(element);
                    }
                }
                else
                {
                    document.Add(element);
                }
            }

            //limpiamos todo
            document.Close();

            writer.Close();

            return streamArchivo.ToArray();
        }
    }


    /// <summary>
    /// HECHO POR CESAR PULIDO
    /// EL DIA 18 DE DICIEMBRE DE 2012
    /// PARA GENERAR LA REFERENCIA
    /// </summary>
    /// <returns></returns>
    public byte[] GenerarPDFReferencia(Decimal ID_REFERENCIA, Decimal ID_SOLICITUD)
    {
        tools _tools = new tools();

        String html_encabezado = "<html>";
        html_encabezado += "<body>";

        String html_pie = "</body>";
        html_pie += "</html>";

        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaSolicitud = _radicacionHojasDeVida.ObtenerRegSolicitudesingresoPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));
        DataRow filaSolicitud = tablaSolicitud.Rows[0];

        referencia _referencia = new referencia(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaReferencia = _referencia.ObtenerPorIdReferencia(ID_REFERENCIA);
        DataRow filaReferencia = tablaReferencia.Rows[0];

        String NOMBRE_CANDIDATO = filaSolicitud["NOMBRES"].ToString().Trim() + " " + filaSolicitud["APELLIDOS"].ToString().Trim();
        String NUM_DOC_IDENTIDAD_CANDIDATO = filaSolicitud["NUM_DOC_IDENTIDAD"].ToString().Trim();

        String EMPRESA_DONDE_TRABAJO_CANDIDATO = filaReferencia["EMPRESA_TRABAJO"].ToString().Trim();
        String TELEFONO_EMPRESA = filaReferencia["NUM_TELEFONO"].ToString().Trim();

        String EMPRESA_TEMPORAL = filaReferencia["EMPRESA_TEMPORAL"].ToString().Trim();
        String NUM_TELEFONO_TEMPORAL = filaReferencia["NUM_TELEFONO_TEMPOAL"].ToString().Trim();

        String TIPO_CONTRATO = filaReferencia["TIPO_CONTRATO"].ToString().Trim();


        String FECHA_INGRESO_CANDIDATO = "";
        if (DBNull.Value.Equals(filaReferencia["FECHA_INGRESO"]) == false)
        {
            FECHA_INGRESO_CANDIDATO = Convert.ToDateTime(filaReferencia["FECHA_INGRESO"]).ToShortDateString();
        }
        String FECHA_RETIRO_CANDIDATO = "";
        if (DBNull.Value.Equals(filaReferencia["FECHA_RETIRO"]) == false)
        {
            FECHA_RETIRO_CANDIDATO = Convert.ToDateTime(filaReferencia["FECHA_RETIRO"]).ToShortDateString();
        }

        String ULTIMO_CARGO_CANDIDATO = filaReferencia["ULTIMO_CARGO"].ToString().Trim();
        
        String NOMBRE_INFORMANTE = filaReferencia["NOMBRE_INFORMANTE"].ToString().Trim();
        String CARGO_INFORMANTE = filaReferencia["CARGO_INFORMANTE"].ToString().Trim();

        String NOMBRE_JEFE = filaReferencia["NOMBRE_JEFE"].ToString().Trim();
        String CARGO_JEFE = filaReferencia["CARGO_JEFE"].ToString().Trim();

        String ULTIMO_SALARIO_CANDIDATO = "Desconocido.";
        try
        {
            ULTIMO_SALARIO_CANDIDATO = Convert.ToDecimal(filaReferencia["ULTIMO_SALARIO"]).ToString();
        }
        catch
        {
            ULTIMO_SALARIO_CANDIDATO = "Desconocido.";
        }

        String COMISIONES = filaReferencia["COMISIONES"].ToString().Trim();
        String BONOS = filaReferencia["BONOS"].ToString().Trim();

        String MOTIVO_RETIRO = filaReferencia["MOTIVO_RETIRO"].ToString().Trim();

        String USUARIO_REFERENCIADOR = Session["USU_LOG"].ToString();
        String NOMBRE_REFERENCIADOR = "Desconocido.";
        if (DBNull.Value.Equals(filaReferencia["USU_MOD"]) == false)
        {
            USUARIO_REFERENCIADOR = filaReferencia["USU_MOD"].ToString().Trim();
        }
        else
        {
            USUARIO_REFERENCIADOR = filaReferencia["USU_CRE"].ToString().Trim();
        }

        String CUALIDADES_CALIFICACION = filaReferencia["CUALIDADES_CALIFICACION"].ToString().Trim();

        usuario _usuario = new usuario(Session["idEmpresa"].ToString());
        DataTable tablaUsuario = _usuario.ObtenerUsuarioPorUsuLog(USUARIO_REFERENCIADOR);
        DataRow filaUsuario = tablaUsuario.Rows[0];
        if (filaUsuario["USU_TIPO"].ToString().ToUpper() == "PLANTA")
        {
            NOMBRE_REFERENCIADOR = filaUsuario["NOMBRES"].ToString().Trim() + " " + filaUsuario["APELLIDOS"].ToString().Trim();
        }
        else
        {
            NOMBRE_REFERENCIADOR = filaUsuario["NOMBRES_EXTERNO"].ToString().Trim() + " " + filaUsuario["APELLIDOS_EXTERNO"].ToString().Trim();
        }

        //En esta variable cargamos el documento plantilla
        StreamReader archivo_original = new StreamReader(Server.MapPath(@"~\plantillas_reportes\referencia.htm"));

        String html_formato_referencia = html_encabezado + archivo_original.ReadToEnd();

        archivo_original.Dispose();
        archivo_original.Close();

        html_formato_referencia = html_formato_referencia.Replace("[NOMBRE_CANDIDATO]", NOMBRE_CANDIDATO);
        html_formato_referencia = html_formato_referencia.Replace("[NUM_DOC_IDENTIDAD_CANDIDATO]", NUM_DOC_IDENTIDAD_CANDIDATO);

        html_formato_referencia = html_formato_referencia.Replace("[EMPRESA_DONDE_TRABAJO_CANDIDATO]", EMPRESA_DONDE_TRABAJO_CANDIDATO);
        html_formato_referencia = html_formato_referencia.Replace("[TELEFONO_EMPRESA]", TELEFONO_EMPRESA);

        html_formato_referencia = html_formato_referencia.Replace("[EMPRESA_TEMPORAL]", EMPRESA_TEMPORAL);
        html_formato_referencia = html_formato_referencia.Replace("[TELEFONO_TEMPORAL]", NUM_TELEFONO_TEMPORAL);

        if (TIPO_CONTRATO == "OBRA O LABOR")
        {
            html_formato_referencia = html_formato_referencia.Replace("[OBRA_LABOR]", " X ");
            html_formato_referencia = html_formato_referencia.Replace("[FIJO]", "___");
            html_formato_referencia = html_formato_referencia.Replace("[INDEFINIDO]", "___");
        }
        else
        {
            if (TIPO_CONTRATO == "FIJO")
            {
                html_formato_referencia = html_formato_referencia.Replace("[OBRA_LABOR]", "___");
                html_formato_referencia = html_formato_referencia.Replace("[FIJO]", " X ");
                html_formato_referencia = html_formato_referencia.Replace("[INDEFINIDO]", "___");
            }
            else
            {
                if (TIPO_CONTRATO == "INDEFINIDO")
                {
                    html_formato_referencia = html_formato_referencia.Replace("[OBRA_LABOR]", "___");
                    html_formato_referencia = html_formato_referencia.Replace("[FIJO]", "___");
                    html_formato_referencia = html_formato_referencia.Replace("[INDEFINIDO]", " X ");
                }
                else
                {
                    html_formato_referencia = html_formato_referencia.Replace("[OBRA_LABOR]", "___");
                    html_formato_referencia = html_formato_referencia.Replace("[FIJO]", "___");
                    html_formato_referencia = html_formato_referencia.Replace("[INDEFINIDO]", "___");
                }
            }   
        }
       
        html_formato_referencia = html_formato_referencia.Replace("[FECHA_INGRESO_CANDIDATO]", FECHA_INGRESO_CANDIDATO);
        html_formato_referencia = html_formato_referencia.Replace("[FECHA_RETIRO_CANDIDATO]", FECHA_RETIRO_CANDIDATO);

        html_formato_referencia = html_formato_referencia.Replace("[ULTIMO_CARGO_CANDIDATO]", ULTIMO_CARGO_CANDIDATO);

        html_formato_referencia = html_formato_referencia.Replace("[NOMBRE_INFORMANTE]", NOMBRE_INFORMANTE);
        html_formato_referencia = html_formato_referencia.Replace("[CARGO_INFORMANTE]", CARGO_INFORMANTE);

        html_formato_referencia = html_formato_referencia.Replace("[NOMBRE_JEFE]", NOMBRE_JEFE);
        html_formato_referencia = html_formato_referencia.Replace("[CARGO_JEFE]", CARGO_JEFE);

        html_formato_referencia = html_formato_referencia.Replace("[ULTIMO_SALARIO_CANDIDATO]", ULTIMO_SALARIO_CANDIDATO);
        html_formato_referencia = html_formato_referencia.Replace("[COMISIONES]", COMISIONES);
        html_formato_referencia = html_formato_referencia.Replace("[BONOS]", BONOS);

        html_formato_referencia = html_formato_referencia.Replace("[MOTIVO_RETIRO]", MOTIVO_RETIRO);

        html_formato_referencia = html_formato_referencia.Replace("[NOMBRE_REFERENCIADOR]", NOMBRE_REFERENCIADOR);

        if (DBNull.Value.Equals(filaReferencia["FCH_MOD"]) == false)
        {
            html_formato_referencia = html_formato_referencia.Replace("[FECHA_REFERECIA]", Convert.ToDateTime(filaReferencia["FCH_MOD"]).ToShortDateString());
        }
        else
        {
            html_formato_referencia = html_formato_referencia.Replace("[FECHA_REFERECIA]", Convert.ToDateTime(filaReferencia["FCH_CRE"]).ToShortDateString());
        }

        //ya esta la informacion de la referecia basica ahora le adicionadmos al informe los datos de las preguntas
        DataTable tablaPreguntasRespuestas = _referencia.ObtenerPreguntasRespuestasReferencia(ID_REFERENCIA);
        if (tablaPreguntasRespuestas.Rows.Count > 0)
        {
            int contadorPreguntas = 0;
            //recorrido por las preguntas
            String html_tabla_preguntas = "";
            html_tabla_preguntas += "<table border=\"1\" cellpadding=\"1\" cellspacing=\"0\" width=\"100%\" align=\"center\">";
            html_tabla_preguntas += "<tr>";
            html_tabla_preguntas += "<td width=\"15%\" style=\"text-align:center; font-weight: bold;\">";
            html_tabla_preguntas += "#";
            html_tabla_preguntas += "</td>";
            html_tabla_preguntas += "<td style=\"text-align:center; font-weight: bold;\">";
            html_tabla_preguntas += "CUESTIONARIO";
            html_tabla_preguntas += "</td>";
            html_tabla_preguntas += "<td style=\"text-align:center; font-weight: bold;\">";
            html_tabla_preguntas += "RESPUESTA";
            html_tabla_preguntas += "</td>";
            html_tabla_preguntas += "</tr>";
            for (int i = 0; i < tablaPreguntasRespuestas.Rows.Count; i++)
            {
                DataRow filaPregunta = tablaPreguntasRespuestas.Rows[i];

                contadorPreguntas += 1;

                String textoPregunta = filaPregunta["CONTENIDO"].ToString().Trim();
                String textoRespuesta = filaPregunta["RESPUESTA"].ToString().Trim();

                html_tabla_preguntas += "<tr>";
                html_tabla_preguntas += "<td width=\"15%\" style=\"text-align:center;\">";
                html_tabla_preguntas += contadorPreguntas.ToString();
                html_tabla_preguntas += "</td>";
                html_tabla_preguntas += "<td style=\"text-align:justify;\">";
                html_tabla_preguntas += textoPregunta;
                html_tabla_preguntas += "</td>";
                html_tabla_preguntas += "<td style=\"text-align:center;\">";
                html_tabla_preguntas += textoRespuesta;
                html_tabla_preguntas += "</td>";
                html_tabla_preguntas += "</tr>";
            }
            html_tabla_preguntas += "</table>";

            html_formato_referencia = html_formato_referencia.Replace("[TABLA_CUESTIONARIO]", html_tabla_preguntas);
        }


        if (String.IsNullOrEmpty(CUALIDADES_CALIFICACION) == false)
        {
            String html_tabla_preguntas = "";
            html_tabla_preguntas += "<table border=\"1\" cellpadding=\"1\" cellspacing=\"0\" width=\"80%\" align=\"center\">";
            html_tabla_preguntas += "<tr>";
            html_tabla_preguntas += "<td width=\"50%\" style=\"text-align:center; font-weight: bold;\">";
            html_tabla_preguntas += "CUALIDAD";
            html_tabla_preguntas += "</td>";
            html_tabla_preguntas += "<td width=\"12%\" style=\"text-align:center; font-weight: bold;\">";
            html_tabla_preguntas += "EXCELENTE";
            html_tabla_preguntas += "</td>";
            html_tabla_preguntas += "<td width=\"13%\" style=\"text-align:center; font-weight: bold;\">";
            html_tabla_preguntas += "BUENO";
            html_tabla_preguntas += "</td>";
            html_tabla_preguntas += "<td width=\"12%\" style=\"text-align:center; font-weight: bold;\">";
            html_tabla_preguntas += "REGULAR";
            html_tabla_preguntas += "</td>";
            html_tabla_preguntas += "<td width=\"13%\" style=\"text-align:center; font-weight: bold;\">";
            html_tabla_preguntas += "MALO";
            html_tabla_preguntas += "</td>";
            html_tabla_preguntas += "</tr>";

            String[] cualidadesCalificacionesArray = CUALIDADES_CALIFICACION.Split(';');

            foreach(String cualidadCalificacion in cualidadesCalificacionesArray)
            {
                String CUALIDAD = cualidadCalificacion.Split(':')[0];
                String CALIFICACION = cualidadCalificacion.Split(':')[1];

                html_tabla_preguntas += "<tr>";
                html_tabla_preguntas += "<td width=\"50%\" style=\"text-align:left;\">";
                html_tabla_preguntas += CUALIDAD;
                html_tabla_preguntas += "</td>";
                html_tabla_preguntas += "<td width=\"12%\" style=\"text-align:justify;\">";
                if(CALIFICACION == "EXCELENTE")
                {
                    html_tabla_preguntas += "X";    
                }
                html_tabla_preguntas += "</td>";

                html_tabla_preguntas += "<td width=\"13%\" style=\"text-align:center;\">";
                if (CALIFICACION == "BUENO")
                {
                    html_tabla_preguntas += "X";
                }
                html_tabla_preguntas += "</td>";
                html_tabla_preguntas += "<td width=\"12%\" style=\"text-align:center;\">";
                if (CALIFICACION == "REGULAR")
                {
                    html_tabla_preguntas += "X";
                }
                html_tabla_preguntas += "</td>";
                html_tabla_preguntas += "<td width=\"13%\" style=\"text-align:center;\">";
                if (CALIFICACION == "MALO")
                {
                    html_tabla_preguntas += "X";
                }
                html_tabla_preguntas += "</td>";
                html_tabla_preguntas += "</tr>";
            }

            html_tabla_preguntas += "</table>";

            html_formato_referencia = html_formato_referencia.Replace("[TABLA_CUALIDADES]", html_tabla_preguntas);
        }

        html_formato_referencia += html_pie;


        //creamos un configuramos el documento de pdf
        //(tamaño de la hoja,margen izq, margen der, margin arriba margen abajo)
        iTextSharp.text.Document document = new iTextSharp.text.Document(new Rectangle(595, 842), 50, 50, 80, 45);

        using (MemoryStream streamArchivo = new MemoryStream())
        {
            iTextSharp.text.pdf.PdfWriter writer = PdfWriter.GetInstance(document, streamArchivo);

            // Our custom Header and Footer is done using Event Handler
            pdfEvents PageEventHandler = new pdfEvents();
            writer.PageEvent = PageEventHandler;

            // Define the page header
            // Define the page header
            if (Session["idEmpresa"].ToString() == "1")
            {
                PageEventHandler.dirImagenHeader = Server.MapPath("~/imagenes/reportes/logo_sertempo.png");
            }
            else
            {
                PageEventHandler.dirImagenHeader = Server.MapPath("~/imagenes/reportes/logo_eficiencia.png");
            }

            PageEventHandler.fechaImpresion = DateTime.Now;
            PageEventHandler.tipoDocumento = "referencia";

            document.Open();

            //capturamos el archivo temporal del response
            String tempFile = Path.GetTempFileName();

            //y lo llenamos con el html de la plantilla
            using (StreamWriter tempwriter = new StreamWriter(tempFile, false))
            {
                tempwriter.Write(html_formato_referencia);
            }

            //leeemos el archivo temporal y lo colocamos en el documento de pdf
            List<IElement> htmlarraylist = HTMLWorker.ParseToList(new StreamReader(tempFile), new StyleSheet());

            foreach (IElement element in htmlarraylist)
            {
                if (element.Chunks.Count > 0)
                {
                    if (element.Chunks[0].Content == "linea para paginacion de pdf")
                    {
                        document.NewPage();
                    }
                    else
                    {
                        document.Add(element);
                    }
                }
                else
                {
                    document.Add(element);
                }
            }

            //limpiamos todo
            document.Close();

            writer.Close();

            return streamArchivo.ToArray();
        }
    }

    /// <summary>
    /// HECHO POR CESAR
    /// EL DIA 18 DE DICIEMBRE DE 2012
    /// PARA GENERAR LOS ARCHIVOS DE LAS PRUEBAS
    /// </summary>
    /// <param name="prefijoNombreArchivo"></param>
    /// <returns></returns>
    public Dictionary<String, byte[]> ObtenerArchivosPruebas(String prefijoNombreArchivo, Decimal ID_PERFIL, Decimal ID_SOLICITUD)
    {
        Dictionary<String, byte[]> listaArchivos = new Dictionary<string, byte[]>();

        //obtenemos las pruebas por perfil y con los resultados asociados
        /*
            SRAP.ARCHIVO_PRUEBA, 
            SRAP.ARCHIVO_EXTENSION, 
            SRAP.ARCHIVO_TAMANO, 
            SRAP.ARCHIVO_TYPE,  
        */
        pruebaPerfil _pruebaPerfil = new pruebaPerfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaPruebas = _pruebaPerfil.ObtenerPorIdPerfilConResultadosIdSolicitud(ID_PERFIL, ID_SOLICITUD);

        for (int i = 0; i < tablaPruebas.Rows.Count; i++)
        {
            DataRow filaArchivo = tablaPruebas.Rows[i];

            if (DBNull.Value.Equals(filaArchivo["ARCHIVO_PRUEBA"]) == false)
            {
                String NOM_PRUEBA = filaArchivo["NOM_PRUEBA"].ToString().Trim();
                NOM_PRUEBA = NOM_PRUEBA.Replace(' ', '_');

                byte[] ARCHIVO = (byte[])filaArchivo["ARCHIVO_PRUEBA"];
                String EXTENSION = filaArchivo["ARCHIVO_EXTENSION"].ToString().Trim();

                listaArchivos.Add((prefijoNombreArchivo + "PRUEBA-" + NOM_PRUEBA + EXTENSION).Replace(' ', '_'), ARCHIVO);
            }
        }

        return listaArchivos;
    }

    /// <summary>
    /// HECHO POR CESAR PULIDO
    /// EL DIA 18 DE DICIEMBRE DE 2012
    /// PARA OBTENER LOS ARCHIVOS DE LOS RESULTADOS DE EXAMENES
    /// </summary>
    /// <param name="prefijoNombreArchivo"></param>
    /// <returns></returns>
    public Dictionary<String, byte[]> ObtenerArchivosExamenes(String prefijoNombreArchivo, Decimal ID_SOLICITUD, Decimal ID_REQUERIMIENTO)
    {
        Dictionary<String, byte[]> listaArchivos = new Dictionary<string, byte[]>();

        ordenExamenes _ordenes = new ordenExamenes(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaOrdenes = _ordenes.ObtenerConRegOrdenExamenPorSolicitud(Convert.ToInt32(ID_REQUERIMIENTO), Convert.ToInt32(ID_SOLICITUD));

        for (int i = 0; i < tablaOrdenes.Rows.Count; i++)
        {
            DataRow filaArchivo = tablaOrdenes.Rows[i];

            if (DBNull.Value.Equals(filaArchivo["ARCHIVO_EXAMEN"]) == false)
            {
                String NOM_EXAMEN = filaArchivo["NOMBRE"].ToString().Trim();
                NOM_EXAMEN = NOM_EXAMEN.Replace(' ', '_');

                byte[] ARCHIVO = (byte[])filaArchivo["ARCHIVO_EXAMEN"];
                String EXTENSION = filaArchivo["ARCHIVO_EXTENSION"].ToString().Trim();

                listaArchivos.Add((prefijoNombreArchivo + "EXAMEN_MEDICO-" + NOM_EXAMEN + EXTENSION).Replace(' ', '_'), ARCHIVO);
            }
        }

        return listaArchivos;
    }

    /// <summary>
    /// HECHO POR CESAR PULIDO
    /// EL DIA 19 DE DICIEMBRE DE 2012
    /// PARA OBTENER LOS ARCHIVOS DE LOS AUTOS DE RECOMENDACION
    /// </summary>
    /// <returns></returns>
    public byte[] GenerarPDFExamenes(Decimal ID_CONTRATO, Decimal ID_SOLICITUD, Decimal ID_REQUERIMIENTO)
    {
        Boolean hayAutos = false;
        String armadoDeAutos = "";

        ordenExamenes _ordenes = new ordenExamenes(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaOrdenes = _ordenes.ObtenerConRegOrdenExamenPorSolicitud(Convert.ToInt32(ID_REQUERIMIENTO), Convert.ToInt32(ID_SOLICITUD));
        foreach (DataRow filaExamenes in tablaOrdenes.Rows)
        {
            //OBSERVACIONES ES EL CAMPO DONDE ESTA ALMACENADO EL AUTO DE RECOMENDACION SI EXISE
            if (!(String.IsNullOrEmpty(filaExamenes["OBSERVACIONES"].ToString().Trim())))
            {
                if (hayAutos == false)
                {
                    armadoDeAutos = filaExamenes["OBSERVACIONES"].ToString().Trim();
                }
                else
                {
                    armadoDeAutos += "; " + filaExamenes["OBSERVACIONES"].ToString().Trim();
                }
                hayAutos = true;
            }
        }

        if (hayAutos)
        {
            /*
            ---------------TAGS ENCONTRADOS EN AUTOS_RECOMENDACION-------------------
            [FECHA_AUTO]			= FECHA DE LA CABECERA DE LA APERTURA DE CUENTA
            [NOMBRE_TRABAJADOR]		= NOMBRE DEL BANCO
            [TELEFONO_TRABAJADOR]		= TELEFONO DEL TRABAJADOR
            [TIPO_DOCUMENTO_IDENTIDAD]	= tipo de documento del trabajador
            [NUMERO_DOCUMENTO_IDENTIDAD]	= numero del documento de identidad del trabajador
            [AUTOS_RECOMENDACION]		= nombre analista que firma
            [NOMBRE_EMPLEADOR]		= cartgo del que firma
            */

            radicacionHojasDeVida _sol = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaSol = _sol.ObtenerRegSolicitudesingresoPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));
            DataRow filaSol = tablaSol.Rows[0];

            //DATOS DEL INFORME
            DateTime FECHA_AUTO = DateTime.Now;
            String NOMBRE_TRABAJADOR = filaSol["NOMBRES"].ToString().Trim() + " " + filaSol["APELLIDOS"].ToString().Trim();
            String TELEFONO_TRABAJADOR = filaSol["TEL_ASPIRANTE"].ToString().Trim();
            String TIPO_DOCUMENTO_IDENTIDAD = filaSol["TIP_DOC_IDENTIDAD"].ToString().Trim();
            String NUMERO_DOCUMENTO_IDENTIDAD = filaSol["NUM_DOC_IDENTIDAD"].ToString().Trim();
            String AUTOS_RECOMENDACION = armadoDeAutos;
            String NOMBRE_EMPLEADOR = null;

            if (Session["idEmpresa"].ToString() == "1")
            {
                NOMBRE_EMPLEADOR = tabla.VAR_NOMBRE_SERTEMPO;
            }
            else
            {
                NOMBRE_EMPLEADOR = tabla.VAR_NOMBRE_EYS;
            }

            //En esta variable cargamos el documento plantilla
            StreamReader archivo = new StreamReader(Server.MapPath(@"~\plantillas_reportes\autos_recomendacion.htm"));

            String html = archivo.ReadToEnd();

            archivo.Dispose();
            archivo.Close();

            html = html.Replace("[FECHA_AUTO]", FECHA_AUTO.ToLongDateString());
            html = html.Replace("[NOMBRE_TRABAJADOR]", NOMBRE_TRABAJADOR);
            html = html.Replace("[TELEFONO_TRABAJADOR]", TELEFONO_TRABAJADOR);
            html = html.Replace("[TIPO_DOCUMENTO_IDENTIDAD]", TIPO_DOCUMENTO_IDENTIDAD);
            html = html.Replace("[NUMERO_DOCUMENTO_IDENTIDAD]", NUMERO_DOCUMENTO_IDENTIDAD);
            html = html.Replace("[AUTOS_RECOMENDACION]", AUTOS_RECOMENDACION);
            html = html.Replace("[NOMBRE_EMPLEADOR]", NOMBRE_EMPLEADOR);
            html = html.Replace("[DIR_FIRMA_SALUD]", tabla.DIR_IMAGENES_PARA_PDF + "/firma_autos_recomendacion.jpg");

            //creamos un configuramos el documento de pdf
            //(tamaño de la hoja,margen izq, margen der, margin arriba margen abajo)
            iTextSharp.text.Document document = new iTextSharp.text.Document(new Rectangle(612, 397), 25, 25, 75, 30);

            using (MemoryStream streamArchivo = new MemoryStream())
            {
                iTextSharp.text.pdf.PdfWriter writer = PdfWriter.GetInstance(document, streamArchivo);

                // Our custom Header and Footer is done using Event Handler
                pdfEvents PageEventHandler = new pdfEvents();
                writer.PageEvent = PageEventHandler;

                // Define the page header
                if (Session["idEmpresa"].ToString() == "1")
                {
                    PageEventHandler.dirImagenHeader = Server.MapPath("~/imagenes/reportes/logo_sertempo.png");
                }
                else
                {
                    PageEventHandler.dirImagenHeader = Server.MapPath("~/imagenes/reportes/logo_eficiencia.png");
                }
                PageEventHandler.fechaImpresion = DateTime.Now;
                PageEventHandler.tipoDocumento = "autos_recomendacion";

                document.Open();

                //capturamos el archivo temporal del response
                String tempFile = Path.GetTempFileName();

                //y lo llenamos con el html de la plantilla
                using (StreamWriter tempwriter = new StreamWriter(tempFile, false))
                {
                    tempwriter.Write(html);
                }

                //leeemos el archivo temporal y lo colocamos en el documento de pdf
                List<IElement> htmlarraylist = HTMLWorker.ParseToList(new StreamReader(tempFile), new StyleSheet());

                foreach (IElement element in htmlarraylist)
                {
                    if (element.Chunks.Count > 0)
                    {
                        if (element.Chunks[0].Content == "linea para paginacion de pdf")
                        {
                            document.NewPage();
                        }
                        else
                        {
                            document.Add(element);
                        }
                    }
                    else
                    {
                        document.Add(element);
                    }
                }

                //limpiamos todo
                document.Close();

                writer.Close();

                return streamArchivo.ToArray();
            }
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// hecho por cesar pulido
    /// el dia 19 de diciembre de 2012
    /// para generar el archivo del contrato
    /// </summary>
    /// <param name="filaInfoContrato"></param>
    /// <returns></returns>
    private byte[] ImprimirContratoO_L_COMPLETO(DataRow filaInfoContrato)
    {
        tools _tools = new tools();

        Boolean CarnetIncluido = false;

        //En esta variable cargamos el documento plantilla segun la empresa de session
        StreamReader archivo;

        if (Session["idEmpresa"].ToString() == "1")
        {
            if (CarnetIncluido == true)
            {
                archivo = new StreamReader(Server.MapPath(@"~\plantillas_reportes\contrato_sertempo_obra_labor.htm"));
            }
            else
            {
                archivo = new StreamReader(Server.MapPath(@"~\plantillas_reportes\contrato_sertempo_obra_labor_carnet_aparte.htm"));
            }
        }
        else
        {
            if (CarnetIncluido == true)
            {
                archivo = new StreamReader(Server.MapPath(@"~\plantillas_reportes\contrato_eys_labor_contratada.htm"));
            }
            else
            {
                archivo = new StreamReader(Server.MapPath(@"~\plantillas_reportes\contrato_eys_labor_contratada_carnet_aparte.htm"));
            }
        }


        String html = archivo.ReadToEnd();

        archivo.Dispose();
        archivo.Close();

        if (Session["idEmpresa"].ToString() == "1")
        {
            html = html.Replace("[DIR_LOGO_EMPLEADOR]", tabla.DIR_IMAGENES_PARA_PDF + "/logo_sertempo.png");
            html = html.Replace("[MENSAJE_LOGO]", "SERVICIOS TEMPORALES PROFESIONALES");
            html = html.Replace("[NOMBRE_EMPLEADOR]", tabla.VAR_NOMBRE_SERTEMPO);
            html = html.Replace("[DOMICILO_EMPLEADOR]", tabla.VAR_DOMICILIO_SERTEMPO);
            html = html.Replace("[DESCRIPCION_CARGO]", filaInfoContrato["DSC_FUNCIONES"].ToString().Trim().ToUpper());
            html = html.Replace("[SERVICIO_RESPECTIVO]", filaInfoContrato["DESCRIPCION"].ToString().Trim().ToUpper());
            html = html.Replace("[EMPRESA_USUARIA]", filaInfoContrato["RAZ_SOCIAL"].ToString().Trim().ToUpper());
            html = html.Replace("[DIR_FIRMA_EMPLEADOR]", tabla.DIR_IMAGENES_PARA_PDF + "/firma_contrato_empleador_sertempo.jpg");
        }
        else
        {
            html = html.Replace("[DIR_LOGO_EMPLEADOR]", tabla.DIR_IMAGENES_PARA_PDF + "/logo_eficiencia.jpg");
            html = html.Replace("[NOMBRE_EMPLEADOR]", tabla.VAR_NOMBRE_EYS);
            html = html.Replace("[DOMICILO_EMPLEADOR]", tabla.VAR_DOMICILIO_EYS);
            html = html.Replace("[FUNCION_CARGO]", filaInfoContrato["DSC_FUNCIONES"].ToString().Trim().ToUpper());
            html = html.Replace("[ACTIVIDAD_CONTRATADA]", filaInfoContrato["DSC_FUNCIONES"].ToString().Trim().ToUpper());
            html = html.Replace("[EMPRESA_DESTACA]", filaInfoContrato["RAZ_SOCIAL"].ToString().Trim().ToUpper());
            html = html.Replace("[DIR_FIRMA_EMPLEADOR]", tabla.DIR_IMAGENES_PARA_PDF + "/firma_contrato_empleador_eys.jpg");
        }


        html = html.Replace("[CARGO_TRABAJADOR]", filaInfoContrato["NOM_OCUPACION"].ToString().Trim().ToUpper());
        html = html.Replace("[NOMBRE_TRABAJADOR]", filaInfoContrato["APELLIDOS"].ToString().Trim().ToUpper() + " " + filaInfoContrato["NOMBRES"].ToString().Trim().ToUpper());
        html = html.Replace("[TIPO_DOCUMENTO_IDENTIDAD]", filaInfoContrato["TIP_DOC_IDENTIDAD"].ToString().Trim().ToUpper());
        html = html.Replace("[DOC_IDENTIFICACION]", filaInfoContrato["NUM_DOC_IDENTIDAD"].ToString().Trim().ToUpper());
        html = html.Replace("[SALARIO]", Convert.ToDecimal(filaInfoContrato["SALARIO"]).ToString());
        html = html.Replace("[PERIODO_PAGO]", "??");
        html = html.Replace("[FECHA_INICIACION]", Convert.ToDateTime(filaInfoContrato["FECHA_INICIA"]).ToLongDateString());
        html = html.Replace("[CARNE_VALIDO_HASTA]", Convert.ToDateTime(filaInfoContrato["FECHA_TERMINA"]).ToLongDateString());

        //esto es para obtener la ciudad de impresión del contrato
        //OBTENEMOS LA CIUDAD DE FIRMA, DESDE LA CIUDAD DEL USU_LOG
        usuario _usuario = new usuario(Session["idEmpresa"].ToString());
        DataTable tablaInfoUsuario = _usuario.ObtenerInicioSesionPorUsuLog(Session["USU_LOG"].ToString());
        if (tablaInfoUsuario.Rows.Count <= 0)
        {
            html = html.Replace("[CIUDAD_FIRMA]", "Desconocida");
        }
        else
        {
            DataRow filaInfoUsuario = tablaInfoUsuario.Rows[0];
            html = html.Replace("[CIUDAD_FIRMA]", filaInfoUsuario["NOMBRE_CIUDAD"].ToString());
        }

        DateTime fechaHoy = DateTime.Now;
        html = html.Replace("[DIAS_FIRMA]", fechaHoy.Day.ToString());
        html = html.Replace("[MES_FIRMA]", _tools.obtenerNombreMes(fechaHoy.Month));
        html = html.Replace("[ANNO_FIRMA]", fechaHoy.Year.ToString());




        //creamos un configuramos el documento de pdf
        //(tamaño de la hoja,margen izq, margen der, margin arriba margen abajo)
        iTextSharp.text.Document document = new iTextSharp.text.Document(new Rectangle(612, 936), 15, 15, 5, 15);

        using (MemoryStream streamArchivo = new MemoryStream())
        {
            iTextSharp.text.pdf.PdfWriter writer = PdfWriter.GetInstance(document, streamArchivo);

            // Our custom Header and Footer is done using Event Handler
            pdfEvents PageEventHandler = new pdfEvents();
            writer.PageEvent = PageEventHandler;

            PageEventHandler.tipoDocumento = "contrato";

            document.Open();


            //capturamos el archivo temporal del response
            String tempFile = Path.GetTempFileName();

            //y lo llenamos con el html de la plantilla
            using (StreamWriter tempwriter = new StreamWriter(tempFile, false))
            {
                tempwriter.Write(html);
            }

            //leeemos el archivo temporal y lo colocamos en el documento de pdf
            List<IElement> htmlarraylist = HTMLWorker.ParseToList(new StreamReader(tempFile), new StyleSheet());

            foreach (IElement element in htmlarraylist)
            {
                if (element.Chunks.Count > 0)
                {
                    if (element.Chunks[0].Content == "linea para paginacion de pdf")
                    {
                        document.NewPage();
                    }
                    else
                    {
                        document.Add(element);
                    }
                }
                else
                {
                    document.Add(element);
                }
            }

            //limpiamos todo
            document.Close();

            writer.Close();

            return streamArchivo.ToArray();
        }
    }

    /// <summary>
    /// HECHO POR CESAR PULIDO
    /// EL DIA 19 DE DICIEMBRE DE 2012
    /// PARA OBTENER UN ARCHIVO CON TEXTO DE CONTRATO NO ENCONTRADO.
    /// </summary>
    /// <returns></returns>
    private byte[] GenerarPDFSinContrato()
    {
        String html_encabezado = "<html>";
        html_encabezado += "<body>";

        String html_pie = "</body>";
        html_pie += "</html>";

        //En esta variable cargamos el documento plantilla
        StreamReader archivo_original = new StreamReader(Server.MapPath(@"~\plantillas_reportes\sincontrato.htm"));

        String html_formato_sincontrato = html_encabezado + archivo_original.ReadToEnd();

        archivo_original.Dispose();
        archivo_original.Close();

        html_formato_sincontrato += html_pie;


        //creamos un configuramos el documento de pdf
        //(tamaño de la hoja,margen izq, margen der, margin arriba margen abajo)
        iTextSharp.text.Document document = new iTextSharp.text.Document(new Rectangle(595, 842), 50, 50, 80, 45);

        using (MemoryStream streamArchivo = new MemoryStream())
        {
            iTextSharp.text.pdf.PdfWriter writer = PdfWriter.GetInstance(document, streamArchivo);

            // Our custom Header and Footer is done using Event Handler
            pdfEvents PageEventHandler = new pdfEvents();
            writer.PageEvent = PageEventHandler;

            PageEventHandler.tipoDocumento = "desconocido";

            document.Open();

            //capturamos el archivo temporal del response
            String tempFile = Path.GetTempFileName();

            //y lo llenamos con el html de la plantilla
            using (StreamWriter tempwriter = new StreamWriter(tempFile, false))
            {
                tempwriter.Write(html_formato_sincontrato);
            }

            //leeemos el archivo temporal y lo colocamos en el documento de pdf
            List<IElement> htmlarraylist = HTMLWorker.ParseToList(new StreamReader(tempFile), new StyleSheet());

            foreach (IElement element in htmlarraylist)
            {
                if (element.Chunks.Count > 0)
                {
                    if (element.Chunks[0].Content == "linea para paginacion de pdf")
                    {
                        document.NewPage();
                    }
                    else
                    {
                        document.Add(element);
                    }
                }
                else
                {
                    document.Add(element);
                }
            }

            //limpiamos todo
            document.Close();

            writer.Close();

            return streamArchivo.ToArray();
        }
    }

    /// <summary>
    /// HECHO POR CESAR PULIDO
    /// EL DIA 19 DE DICIEMBRE DE 2012
    /// PARA OBTENER EL ARCHIVO DEL CONTRATO
    /// </summary>
    /// <returns></returns>
    public byte[] GenerarPDFContrato(Decimal ID_CONTRATO)
    {
        //OBTENEMOS LA INFORMACION NECESARIA PARA IMPRIMIR EL CONTRATO
        registroContrato _registroContrato = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoContrato = _registroContrato.ObtenerInfoParaImprimirContrato(ID_CONTRATO);

        //cargamos info del contrato
        DataRow filaInfoContrato = tablaInfoContrato.Rows[0];

        // Determinando que clase de contrato es
        if (filaInfoContrato["CLASE_CONTRATO"].ToString().Trim().ToUpper() == ClaseContrato.I.ToString())
        {
            // No se tienen formatos para este tipo de contrato
        }
        else
        {
            if (filaInfoContrato["CLASE_CONTRATO"].ToString().Trim().ToUpper() == ClaseContrato.L_C_C_D_A.ToString())
            {
                // No se tienen formatos para este tipo de contrato
            }
            else
            {
                if (filaInfoContrato["CLASE_CONTRATO"].ToString().Trim().ToUpper() == ClaseContrato.L_S_C_D_A_C_V.ToString())
                {
                    // No se tienen formatos para este tipo de contrato
                }
                else
                {
                    if (filaInfoContrato["CLASE_CONTRATO"].ToString().Trim().ToUpper() == ClaseContrato.O_L.ToString())
                    {
                        return ImprimirContratoO_L_COMPLETO(filaInfoContrato);
                    }
                    else
                    {
                        if (filaInfoContrato["CLASE_CONTRATO"].ToString().Trim().ToUpper() == ClaseContrato.T_F.ToString())
                        {
                            // No se tienen formatos para este tipo de contrato
                        }
                    }
                }
            }
        }

        return GenerarPDFSinContrato();
    }

    /// <summary>
    /// hecho por cesar pulido
    /// el dia 9 de diciembre de 2012
    /// para obtener el archivo de las clausulas
    /// </summary>
    /// <returns></returns>
    public byte[] GenerarPDFClausulas(Decimal ID_CONTRATO)
    {
        //obtenemos la informacion necesaria de las clausulas
        registroContrato _registroContrato = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoClausulas = _registroContrato.ObtenerInfoParaImprimirClausulas(ID_CONTRATO);

        if (tablaInfoClausulas.Rows.Count > 0)
        {
            tools _tools = new tools();

            //como existen clausulas antes de mostrarlas se debe actualizar el estado de impresion de clausulas
            DataTable tablaCon = _registroContrato.ObtenerConRegContratosPorRegistro(Convert.ToInt32(ID_CONTRATO));

            DataRow fila = tablaCon.Rows[0];

            //En esta variable cargamos el documento plantilla
            StreamReader archivo_original = new StreamReader(Server.MapPath(@"~\plantillas_reportes\clausulas.htm"));

            String html_clausula = archivo_original.ReadToEnd();

            archivo_original.Dispose();
            archivo_original.Close();

            String html_completo = "<html><body>";

            Int32 contadorClausulas = 0;

            //tenemos que recorrer todas las clausulas e imprimirlas en hojas diferentes pero un mismo documento
            foreach (DataRow filaClausula in tablaInfoClausulas.Rows)
            {
                //como se tiene un clausula entonces al html_compleo le adicionamos una plantilla contenido de clausula
                if (contadorClausulas == 0)
                {
                    html_completo = html_clausula;
                }
                else
                {
                    html_completo += "<div>linea para paginacion de pdf</div>";
                    html_completo += html_clausula;
                }

                //despues de haber agregado la plantilla se procede a reemplazar los tags
                if (Session["idEmpresa"].ToString() == "1")
                {
                    html_completo = html_completo.Replace("[NOMBRE_EMPRESA]", tabla.VAR_NOMBRE_SERTEMPO);
                }
                else
                {
                    html_completo = html_completo.Replace("[NOMBRE_EMPRESA]", tabla.VAR_NOMBRE_EYS);
                }
                html_completo = html_completo.Replace("[NOMBRE_TRABAJADOR]", filaClausula["NOMBRES"].ToString().Trim() + " " + filaClausula["APELLIDOS"].ToString().Trim());
                html_completo = html_completo.Replace("[NOMBRE_CLAUSULA]", filaClausula["NOMBRE"].ToString().Trim());
                html_completo = html_completo.Replace("[ENCABEZADO_CLAUSULA]", filaClausula["ENCABEZADO"].ToString().Trim());
                html_completo = html_completo.Replace("[CONTENIDO_CLAUSULA]", filaClausula["DESCRIPCION"].ToString().Trim());
                html_completo = html_completo.Replace("[CIUDAD_FIRMA]", "BOGOTA");
                html_completo = html_completo.Replace("[DIAS]", DateTime.Now.Day.ToString());
                html_completo = html_completo.Replace("[MES]", _tools.obtenerNombreMes(DateTime.Now.Month));
                html_completo = html_completo.Replace("[ANNO]", DateTime.Now.Year.ToString());

                contadorClausulas += 1;
            }

            html_completo += "</body></html>";


            //creamos un configuramos el documento de pdf
            //(tamaño de la hoja,margen izq, margen der, margin arriba margen abajo)
            iTextSharp.text.Document document = new iTextSharp.text.Document(new Rectangle(595, 842), 40, 40, 80, 40);

            using (MemoryStream streamArchivo = new MemoryStream())
            {
                iTextSharp.text.pdf.PdfWriter writer = PdfWriter.GetInstance(document, streamArchivo);

                // Our custom Header and Footer is done using Event Handler
                pdfEvents PageEventHandler = new pdfEvents();
                writer.PageEvent = PageEventHandler;

                // Define the page header
                // Define the page header
                if (Session["idEmpresa"].ToString() == "1")
                {
                    PageEventHandler.dirImagenHeader = Server.MapPath("~/imagenes/reportes/logo_sertempo.png");
                }
                else
                {
                    PageEventHandler.dirImagenHeader = Server.MapPath("~/imagenes/reportes/logo_eficiencia.png");
                }

                PageEventHandler.fechaImpresion = DateTime.Now;
                PageEventHandler.tipoDocumento = "clausula";

                document.Open();

                //capturamos el archivo temporal del response
                String tempFile = Path.GetTempFileName();

                //y lo llenamos con el html de la plantilla
                using (StreamWriter tempwriter = new StreamWriter(tempFile, false))
                {
                    tempwriter.Write(html_completo);
                }

                //leeemos el archivo temporal y lo colocamos en el documento de pdf
                List<IElement> htmlarraylist = HTMLWorker.ParseToList(new StreamReader(tempFile), new StyleSheet());

                foreach (IElement element in htmlarraylist)
                {
                    if (element.Chunks.Count > 0)
                    {
                        if (element.Chunks[0].Content == "linea para paginacion de pdf")
                        {
                            document.NewPage();
                        }
                        else
                        {
                            document.Add(element);
                        }
                    }
                    else
                    {
                        document.Add(element);
                    }
                }

                //limpiamos todo
                document.Close();

                writer.Close();

                return streamArchivo.ToArray();
            }
        }
        else
        {
            return null;
        }
    }

    /// <summary>
    /// hecho por cesar pulido
    /// el dia 19 de diciembre de 2012
    /// para obtener archivo de la afiliacio a arp
    /// </summary>
    /// <param name="ID_AFILIACION_ARP"></param>
    /// <param name="ID_SOLICITUD"></param>
    /// <param name="ID_EMPLEADO"></param>
    /// <returns></returns>
    private String cargar_arp(Decimal ID_AFILIACION_ARP, Decimal ID_SOLICITUD, Decimal ID_EMPLEADO, Decimal ID_CONTRATO)
    {
        afiliacion _afilicaion = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        /* revisado por fecha de radicacion */
        DataTable tablaARPParaReq = _afilicaion.ObtenerconafiliacionArpPorRegistro(Convert.ToInt32(ID_AFILIACION_ARP));

        DataRow filaARP;
        if (tablaARPParaReq.Rows.Count > 0)
        {
            String TABLA_AFILIACION_ARP = "<br />";

            filaARP = tablaARPParaReq.Rows[0];

            String FECHA_RADICACION = "Desconocida.";
            if (DBNull.Value.Equals(filaARP["FECHA_RADICACION"]) == false)
            {
                FECHA_RADICACION = Convert.ToDateTime(filaARP["FECHA_RADICACION"]).ToShortDateString();
            }

            String FECHA_INICIACION = "Desconocida.";
            if (DBNull.Value.Equals(filaARP["FECHA_R"]) == false)
            {
                FECHA_INICIACION = Convert.ToDateTime(filaARP["FECHA_R"]).ToShortDateString();
            }

            String ENTIDAD = filaARP["NOM_ENTIDAD"].ToString().Trim();

            String OBSERVACIONES = filaARP["OBSERVACIONES"].ToString().Trim();

            TABLA_AFILIACION_ARP += "<div style=\"text-align: left; margin: 0 0 0 20px; text-decoration: underline; font-weight: bold;\">";
            TABLA_AFILIACION_ARP += "AFILIACIÓN: ADMINISTRACIÓN RIESGOS LABORALES";
            TABLA_AFILIACION_ARP += "</div>";
            TABLA_AFILIACION_ARP += "<br />";
            TABLA_AFILIACION_ARP += "<table border=\"1\" cellpadding=\"2\" cellspacing=\"0\" width=\"100%\">";
            TABLA_AFILIACION_ARP += "<tr>";
            TABLA_AFILIACION_ARP += "<td style=\"font-weight:bold; width:180px;\">";
            TABLA_AFILIACION_ARP += "FECHA RADICACIÓN:";
            TABLA_AFILIACION_ARP += "</td>";
            TABLA_AFILIACION_ARP += "<td>";
            TABLA_AFILIACION_ARP += FECHA_RADICACION;
            TABLA_AFILIACION_ARP += "</td>";
            TABLA_AFILIACION_ARP += "<td style=\"font-weight:bold; width:180px;\">";
            TABLA_AFILIACION_ARP += "FECHA INICIACIÓN:";
            TABLA_AFILIACION_ARP += "</td>";
            TABLA_AFILIACION_ARP += "<td>";
            TABLA_AFILIACION_ARP += FECHA_INICIACION;
            TABLA_AFILIACION_ARP += "</td>";
            TABLA_AFILIACION_ARP += "</tr>";
            TABLA_AFILIACION_ARP += "<tr>";
            TABLA_AFILIACION_ARP += "<td style=\"font-weight:bold; width:180px;\">";
            TABLA_AFILIACION_ARP += "ENTIDAD:";
            TABLA_AFILIACION_ARP += "</td>";
            TABLA_AFILIACION_ARP += "<td colspan=\"3\">";
            TABLA_AFILIACION_ARP += ENTIDAD;
            TABLA_AFILIACION_ARP += "</td>";
            TABLA_AFILIACION_ARP += "</tr>";
            TABLA_AFILIACION_ARP += "<tr>";
            TABLA_AFILIACION_ARP += "<td style=\"font-weight:bold; width:180px;\">";
            TABLA_AFILIACION_ARP += "OBSERVACIONES:";
            TABLA_AFILIACION_ARP += "</td>";
            TABLA_AFILIACION_ARP += "<td colspan=\"3\">";
            TABLA_AFILIACION_ARP += OBSERVACIONES;
            TABLA_AFILIACION_ARP += "</td>";
            TABLA_AFILIACION_ARP += "</tr>";
            TABLA_AFILIACION_ARP += "</table>";

            return TABLA_AFILIACION_ARP;
        }
        else
        {
            return null;
        }
    }

    private String cargar_eps(Decimal ID_AFILIACION_EPS, Decimal ID_SOLICITUD, Decimal ID_EMPLEADO, Decimal ID_CONTRATO)
    {
        afiliacion _afilicaion = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        /* revisado por cabios en fecha rqadicacion */
        DataTable tablaAfiliacion = _afilicaion.ObtenerconafiliacionEpsPorRegistro(Convert.ToInt32(ID_AFILIACION_EPS));

        if (tablaAfiliacion.Rows.Count > 0)
        {
            String TABLA_AFILIACION_EPS = "<br />";

            DataRow fila = tablaAfiliacion.Rows[0];

            String FECHA_RADICACION = "Desconocida.";
            if (DBNull.Value.Equals(fila["FECHA_RADICACION"]) == false)
            {
                FECHA_RADICACION = Convert.ToDateTime(fila["FECHA_RADICACION"]).ToShortDateString();
            }

            String FECHA_INICIACION = "Desconocida.";
            if (DBNull.Value.Equals(fila["FECHA_R"]) == false)
            {
                FECHA_INICIACION = Convert.ToDateTime(fila["FECHA_R"]).ToShortDateString();
            }

            String ENTIDAD = fila["NOM_ENTIDAD"].ToString().Trim();

            String OBSERVACIONES = fila["OBSERVACIONES"].ToString().Trim();

            TABLA_AFILIACION_EPS += "<div style=\"text-align: left; margin: 0 0 0 20px; text-decoration: underline; font-weight: bold;\">";
            TABLA_AFILIACION_EPS += "AFILIACIÓN: ENTIDAD PROMOTORA DE SALUD";
            TABLA_AFILIACION_EPS += "</div>";
            TABLA_AFILIACION_EPS += "<br />";
            TABLA_AFILIACION_EPS += "<table border=\"1\" cellpadding=\"2\" cellspacing=\"0\" width=\"100%\">";
            TABLA_AFILIACION_EPS += "<tr>";
            TABLA_AFILIACION_EPS += "<td style=\"font-weight:bold; width:180px;\">";
            TABLA_AFILIACION_EPS += "FECHA RADICACIÓN:";
            TABLA_AFILIACION_EPS += "</td>";
            TABLA_AFILIACION_EPS += "<td>";
            TABLA_AFILIACION_EPS += FECHA_RADICACION;
            TABLA_AFILIACION_EPS += "</td>";
            TABLA_AFILIACION_EPS += "<td style=\"font-weight:bold; width:180px;\">";
            TABLA_AFILIACION_EPS += "FECHA INICIACIÓN:";
            TABLA_AFILIACION_EPS += "</td>";
            TABLA_AFILIACION_EPS += "<td>";
            TABLA_AFILIACION_EPS += FECHA_INICIACION;
            TABLA_AFILIACION_EPS += "</td>";
            TABLA_AFILIACION_EPS += "</tr>";
            TABLA_AFILIACION_EPS += "<tr>";
            TABLA_AFILIACION_EPS += "<td style=\"font-weight:bold; width:180px;\">";
            TABLA_AFILIACION_EPS += "ENTIDAD:";
            TABLA_AFILIACION_EPS += "</td>";
            TABLA_AFILIACION_EPS += "<td colspan=\"3\">";
            TABLA_AFILIACION_EPS += ENTIDAD;
            TABLA_AFILIACION_EPS += "</td>";
            TABLA_AFILIACION_EPS += "</tr>";
            TABLA_AFILIACION_EPS += "<tr>";
            TABLA_AFILIACION_EPS += "<td style=\"font-weight:bold; width:180px;\">";
            TABLA_AFILIACION_EPS += "OBSERVACIONES:";
            TABLA_AFILIACION_EPS += "</td>";
            TABLA_AFILIACION_EPS += "<td colspan=\"3\">";
            TABLA_AFILIACION_EPS += OBSERVACIONES;
            TABLA_AFILIACION_EPS += "</td>";
            TABLA_AFILIACION_EPS += "</tr>";
            TABLA_AFILIACION_EPS += "</table>";

            return TABLA_AFILIACION_EPS;
        }
        else
        {
            return null;
        }
    }

    private String cargar_caja(Decimal ID_AFILIACION_CAJA, Decimal ID_SOLICITUD, Decimal ID_EMPLEADO)
    {
        afiliacion _afilicaion = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        /* REVISADO POR CAMBIOS EN FECHA DE RADICACION */
        DataTable tablaAfiliacion = _afilicaion.ObtenerconafiliacionCajasCPorRegistro(Convert.ToInt32(ID_AFILIACION_CAJA));

        if (tablaAfiliacion.Rows.Count > 0)
        {
            String TABLA_AFILIACION_CCF = "<br />";

            DataRow fila = tablaAfiliacion.Rows[0];

            String FECHA_RADICACION = "Desconocida.";
            if (DBNull.Value.Equals(fila["FECHA_RADICACION"]) == false)
            {
                FECHA_RADICACION = Convert.ToDateTime(fila["FECHA_RADICACION"]).ToShortDateString();
            }

            String FECHA_INICIACION = "Desconocida.";
            if (DBNull.Value.Equals(fila["FECHA_R"]) == false)
            {
                FECHA_INICIACION = Convert.ToDateTime(fila["FECHA_R"]).ToShortDateString();
            }

            String ENTIDAD = fila["NOM_ENTIDAD"].ToString().Trim();

            String OBSERVACIONES = fila["OBSERVACIONES"].ToString().Trim();

            TABLA_AFILIACION_CCF += "<div style=\"text-align: left; margin: 0 0 0 20px; text-decoration: underline; font-weight: bold;\">";
            TABLA_AFILIACION_CCF += "AFILIACIÓN: CAJA DE COMPENSACIÓN FAMILIAR";
            TABLA_AFILIACION_CCF += "</div>";
            TABLA_AFILIACION_CCF += "<br />";
            TABLA_AFILIACION_CCF += "<table border=\"1\" cellpadding=\"2\" cellspacing=\"0\" width=\"100%\">";
            TABLA_AFILIACION_CCF += "<tr>";
            TABLA_AFILIACION_CCF += "<td style=\"font-weight:bold; width:180px;\">";
            TABLA_AFILIACION_CCF += "FECHA RADICACIÓN:";
            TABLA_AFILIACION_CCF += "</td>";
            TABLA_AFILIACION_CCF += "<td>";
            TABLA_AFILIACION_CCF += FECHA_RADICACION;
            TABLA_AFILIACION_CCF += "</td>";
            TABLA_AFILIACION_CCF += "<td style=\"font-weight:bold; width:180px;\">";
            TABLA_AFILIACION_CCF += "FECHA INICIACIÓN:";
            TABLA_AFILIACION_CCF += "</td>";
            TABLA_AFILIACION_CCF += "<td>";
            TABLA_AFILIACION_CCF += FECHA_INICIACION;
            TABLA_AFILIACION_CCF += "</td>";
            TABLA_AFILIACION_CCF += "</tr>";
            TABLA_AFILIACION_CCF += "<tr>";
            TABLA_AFILIACION_CCF += "<td style=\"font-weight:bold; width:180px;\">";
            TABLA_AFILIACION_CCF += "ENTIDAD:";
            TABLA_AFILIACION_CCF += "</td>";
            TABLA_AFILIACION_CCF += "<td colspan=\"3\">";
            TABLA_AFILIACION_CCF += ENTIDAD;
            TABLA_AFILIACION_CCF += "</td>";
            TABLA_AFILIACION_CCF += "</tr>";
            TABLA_AFILIACION_CCF += "<tr>";
            TABLA_AFILIACION_CCF += "<td style=\"font-weight:bold; width:180px;\">";
            TABLA_AFILIACION_CCF += "OBSERVACIONES:";
            TABLA_AFILIACION_CCF += "</td>";
            TABLA_AFILIACION_CCF += "<td colspan=\"3\">";
            TABLA_AFILIACION_CCF += OBSERVACIONES;
            TABLA_AFILIACION_CCF += "</td>";
            TABLA_AFILIACION_CCF += "</tr>";
            TABLA_AFILIACION_CCF += "</table>";

            return TABLA_AFILIACION_CCF;
        }
        else
        {
            return null;
        }
    }

    private String cargar_afp(Decimal ID_AFILIACION_F_PENSIONES, Decimal ID_SOLICITUD, Decimal ID_EMPLEADO)
    {
        afiliacion _afilicaion = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        /* revisado por cambios en fecha de radicacion */
        DataTable tablaAfiliacion = _afilicaion.ObtenerconafiliacionfpensionesPorRegistro(Convert.ToInt32(ID_AFILIACION_F_PENSIONES));

        if (tablaAfiliacion.Rows.Count > 0)
        {
            String TABLA_AFILIACION_AFP = "<br />";

            DataRow fila = tablaAfiliacion.Rows[0];

            String FECHA_RADICACION = "Desconocida.";
            if (DBNull.Value.Equals(fila["FECHA_RADICACION"]) == false)
            {
                FECHA_RADICACION = Convert.ToDateTime(fila["FECHA_RADICACION"]).ToShortDateString();
            }

            String FECHA_INICIACION = "Desconocida.";
            if (DBNull.Value.Equals(fila["FECHA_R"]) == false)
            {
                FECHA_INICIACION = Convert.ToDateTime(fila["FECHA_R"]).ToShortDateString();
            }

            String ENTIDAD = fila["NOM_ENTIDAD"].ToString().Trim();

            String OBSERVACIONES = fila["OBSERVACIONES"].ToString().Trim();

            TABLA_AFILIACION_AFP += "<div style=\"text-align: left; margin: 0 0 0 20px; text-decoration: underline; font-weight: bold;\">";
            TABLA_AFILIACION_AFP += "AFILIACIÓN: ADMINISTRADORA DE FONDOS DE PENSIONES";
            TABLA_AFILIACION_AFP += "</div>";
            TABLA_AFILIACION_AFP += "<br />";
            TABLA_AFILIACION_AFP += "<table border=\"1\" cellpadding=\"2\" cellspacing=\"0\" width=\"100%\">";
            TABLA_AFILIACION_AFP += "<tr>";
            TABLA_AFILIACION_AFP += "<td style=\"font-weight:bold; width:180px;\">";
            TABLA_AFILIACION_AFP += "FECHA RADICACIÓN:";
            TABLA_AFILIACION_AFP += "</td>";
            TABLA_AFILIACION_AFP += "<td>";
            TABLA_AFILIACION_AFP += FECHA_RADICACION;
            TABLA_AFILIACION_AFP += "</td>";
            TABLA_AFILIACION_AFP += "<td style=\"font-weight:bold; width:180px;\">";
            TABLA_AFILIACION_AFP += "FECHA INICIACIÓN:";
            TABLA_AFILIACION_AFP += "</td>";
            TABLA_AFILIACION_AFP += "<td>";
            TABLA_AFILIACION_AFP += FECHA_INICIACION;
            TABLA_AFILIACION_AFP += "</td>";
            TABLA_AFILIACION_AFP += "</tr>";
            TABLA_AFILIACION_AFP += "<tr>";
            TABLA_AFILIACION_AFP += "<td style=\"font-weight:bold; width:180px;\">";
            TABLA_AFILIACION_AFP += "ENTIDAD:";
            TABLA_AFILIACION_AFP += "</td>";
            TABLA_AFILIACION_AFP += "<td colspan=\"3\">";
            TABLA_AFILIACION_AFP += ENTIDAD;
            TABLA_AFILIACION_AFP += "</td>";
            TABLA_AFILIACION_AFP += "</tr>";
            TABLA_AFILIACION_AFP += "<tr>";
            TABLA_AFILIACION_AFP += "<td style=\"font-weight:bold; width:180px;\">";
            TABLA_AFILIACION_AFP += "OBSERVACIONES:";
            TABLA_AFILIACION_AFP += "</td>";
            TABLA_AFILIACION_AFP += "<td colspan=\"3\">";
            TABLA_AFILIACION_AFP += OBSERVACIONES;
            TABLA_AFILIACION_AFP += "</td>";
            TABLA_AFILIACION_AFP += "</tr>";
            TABLA_AFILIACION_AFP += "</table>";

            return TABLA_AFILIACION_AFP;
        }
        else
        {
            return null;
        }
    }


    /// <summary>
    /// HECHO POR CESAR PULIDO
    /// EL DIA 19 DE DICIEMBRE DE 2012
    /// PARA OBTENER EL ARCHIVO CON LA INFORMACION DE LAS AFICLIACIONES DEL EMPLEADO
    /// </summary>
    /// <param name="ID_SOLICITUD"></param>
    /// <param name="ID_REQUERIMIENTO"></param>
    /// <param name="ID_EMPLEADO"></param>
    /// <returns></returns>
    public byte[] GenerarPDFAfiliaciones(Decimal ID_SOLICITUD, Decimal ID_REQUERIMIENTO, Decimal ID_EMPLEADO, Decimal ID_CONTRATO)
    {
        tools _tools = new tools();

        registroContrato _registroContrato = new registroContrato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaInfoContrato = _registroContrato.obtenerInfoNomEmpleadoPorIdSolicitudIdRequerimiento(ID_SOLICITUD, ID_REQUERIMIENTO, ID_EMPLEADO);

        DataRow filaInfoContrato = tablaInfoContrato.Rows[0];

        Decimal ID_AFILIACION_ARP = Convert.ToDecimal(filaInfoContrato["ID_ARP"]);
        Decimal ID_AFILIACION_CAJA_C = Convert.ToDecimal(filaInfoContrato["ID_CAJA_C"]);
        Decimal ID_AFILIACION_EPS = Convert.ToDecimal(filaInfoContrato["ID_EPS"]);
        Decimal ID_AFILIACION_F_PENSIONES = Convert.ToDecimal(filaInfoContrato["ID_F_PENSIONES"]);

        radicacionHojasDeVida _radicacionHojasDeVida = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaSolicitud = _radicacionHojasDeVida.ObtenerRegSolicitudesingresoPorIdSolicitud(Convert.ToInt32(ID_SOLICITUD));
        DataRow filaSolicitud = tablaSolicitud.Rows[0];

        String NOMBRE_ASPIRANTE = filaSolicitud["NOMBRES"].ToString().Trim() + " " + filaSolicitud["APELLIDOS"].ToString().Trim();
        String DOC_IDENTIDAD_ASPIRANTE = filaSolicitud["TIP_DOC_IDENTIDAD"].ToString().Trim() + " " + filaSolicitud["NUM_DOC_IDENTIDAD"].ToString().Trim();
        String DIRECCION_ASPIRANTE = filaSolicitud["DIR_ASPIRANTE"].ToString().Trim();
        String CIUDAD_ASPIRANTE = filaSolicitud["NOMBRE_CIUDAD"].ToString().Trim();
        String SECTOR_ASPIRANTE = filaSolicitud["SECTOR"].ToString();
        String TELEFONOS_ASPIRANTE = filaSolicitud["TEL_ASPIRANTE"].ToString();
        String ASPIRACION_SALARIAL_ASPIRANTE;
        try { ASPIRACION_SALARIAL_ASPIRANTE = Convert.ToInt32(filaSolicitud["ASPIRACION_SALARIAL"]).ToString(); }
        catch { ASPIRACION_SALARIAL_ASPIRANTE = "Desconocido."; }
        String EMAIL_ASPIRANTE = filaSolicitud["E_MAIL"].ToString().Trim();

        int EDAD_ASPIRANTE = 0;
        if (DBNull.Value.Equals(filaSolicitud["FCH_NACIMIENTO"]) == false)
        {
            try
            {
                EDAD_ASPIRANTE = _tools.ObtenerEdadDesdeFechaNacimiento(Convert.ToDateTime(filaSolicitud["FCH_NACIMIENTO"]));
            }
            catch
            {
                EDAD_ASPIRANTE = 0;
            }
        }

        //FEMENINO, MASCULINO
        String SEXO = "Desconocido.";
        String LIBRETA_MILITAR = "Desconocida.";
        if (DBNull.Value.Equals(filaSolicitud["SEXO"]) == false)
        {
            if (filaSolicitud["SEXO"].ToString().ToUpper() == "F")
            {
                SEXO = "Femenino";
                LIBRETA_MILITAR = "No Aplica";
            }
            else
            {
                if (filaSolicitud["SEXO"].ToString().ToUpper() == "M")
                {
                    SEXO = "Masculino";
                    LIBRETA_MILITAR = filaSolicitud["LIB_MILITAR"].ToString();
                }
            }
        }

        //cargo al que aspira el candidato (cargo generico)
        cargo _cargo = new cargo(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaOcupacionAspira = _cargo.ObtenerOcupacionPorIdOcupacion(ID_SOLICITUD);
        String CARGO_APLICA = "Desconocido";
        if (tablaOcupacionAspira.Rows.Count > 0)
        {
            DataRow filaOcupacionAspira = tablaOcupacionAspira.Rows[0];
            CARGO_APLICA = filaOcupacionAspira["NOM_OCUPACION"].ToString().Trim();
        }


        //En esta variable cargamos el documento plantilla
        StreamReader archivo_original = new StreamReader(Server.MapPath(@"~\plantillas_reportes\informeAfiliaciones.htm"));

        String html_formato_afilicaciones = archivo_original.ReadToEnd();

        archivo_original.Dispose();
        archivo_original.Close();

        String html_encabezado = "<html>";
        html_encabezado += "<body>";

        String html_pie = "</body>";
        html_pie += "</html>";

        html_formato_afilicaciones = html_encabezado + html_formato_afilicaciones;



        html_formato_afilicaciones = html_formato_afilicaciones.Replace("[NOMBRE_TRABAJADOR]", NOMBRE_ASPIRANTE);
        html_formato_afilicaciones = html_formato_afilicaciones.Replace("[DOC_IDENTIDAD_TRABAJADOR]", DOC_IDENTIDAD_ASPIRANTE);
        if (EDAD_ASPIRANTE > 0)
        {
            html_formato_afilicaciones = html_formato_afilicaciones.Replace("[EDAD_TRABAJADOR]", EDAD_ASPIRANTE.ToString() + " Años.");
        }
        else
        {
            html_formato_afilicaciones = html_formato_afilicaciones.Replace("[EDAD_ASPIRANTE]", "Desconocida.");
        }
        html_formato_afilicaciones = html_formato_afilicaciones.Replace("[DIRECCION_TRABAJADOR]", DIRECCION_ASPIRANTE);
        html_formato_afilicaciones = html_formato_afilicaciones.Replace("[CIUDAD_TRABAJADOR]", CIUDAD_ASPIRANTE);
        html_formato_afilicaciones = html_formato_afilicaciones.Replace("[SECTOR_TRABAJADOR]", SECTOR_ASPIRANTE);
        html_formato_afilicaciones = html_formato_afilicaciones.Replace("[TELEFONOS_TRABAJADOR]", TELEFONOS_ASPIRANTE);
        html_formato_afilicaciones = html_formato_afilicaciones.Replace("[ASPIRACION_SALARIAL_TRABAJADOR]", ASPIRACION_SALARIAL_ASPIRANTE);
        html_formato_afilicaciones = html_formato_afilicaciones.Replace("[EMAIL_TRABAJADOR]", EMAIL_ASPIRANTE);
        html_formato_afilicaciones = html_formato_afilicaciones.Replace("[LIBRETA_MILITAR_TRABAJADOR]", LIBRETA_MILITAR);


        //reemplazamos tag de arp
        String TABLA_AFILIACION_ARP = cargar_arp(ID_AFILIACION_ARP, ID_SOLICITUD, ID_EMPLEADO, ID_CONTRATO);
        if (TABLA_AFILIACION_ARP != null)
        {
            html_formato_afilicaciones = html_formato_afilicaciones.Replace("[TABLA_AFILIACION_ARP]", TABLA_AFILIACION_ARP);
        }
        else
        {
            html_formato_afilicaciones = html_formato_afilicaciones.Replace("[TABLA_AFILIACION_ARP]", "");
        }

        //reemplazamos tag de eps
        String TABLA_AFILIACION_EPS = cargar_eps(ID_AFILIACION_EPS, ID_SOLICITUD, ID_EMPLEADO, ID_CONTRATO);
        if (TABLA_AFILIACION_EPS != null)
        {
            html_formato_afilicaciones = html_formato_afilicaciones.Replace("[TABLA_AFILIACION_EPS]", TABLA_AFILIACION_EPS);
        }
        else
        {
            html_formato_afilicaciones = html_formato_afilicaciones.Replace("[TABLA_AFILIACION_EPS]", "");
        }


        //reemplazamos tag de caja de compensacion
        String TABLA_AFILIACION_CCF = cargar_caja(ID_AFILIACION_CAJA_C, ID_SOLICITUD, ID_EMPLEADO);
        if (TABLA_AFILIACION_CCF != null)
        {
            html_formato_afilicaciones = html_formato_afilicaciones.Replace("[TABLA_AFILIACION_CCF]", TABLA_AFILIACION_CCF);
        }
        else
        {
            html_formato_afilicaciones = html_formato_afilicaciones.Replace("[TABLA_AFILIACION_CCF]", "");
        }


        //reemplazamos tag de FONDO DE PENSIONES
        String TABLA_AFILIACION_AFP = cargar_afp(ID_AFILIACION_F_PENSIONES, ID_SOLICITUD, ID_EMPLEADO);
        if (TABLA_AFILIACION_AFP != null)
        {
            html_formato_afilicaciones = html_formato_afilicaciones.Replace("[TABLA_AFILIACION_AFP]", TABLA_AFILIACION_AFP);
        }
        else
        {
            html_formato_afilicaciones = html_formato_afilicaciones.Replace("[TABLA_AFILIACION_AFP]", "");
        }

        html_formato_afilicaciones += html_pie;

        //creamos un configuramos el documento de pdf
        //(tamaño de la hoja,margen izq, margen der, margin arriba margen abajo)
        iTextSharp.text.Document document = new iTextSharp.text.Document(new Rectangle(595, 842), 40, 40, 80, 40);

        using (MemoryStream streamArchivo = new MemoryStream())
        {
            iTextSharp.text.pdf.PdfWriter writer = PdfWriter.GetInstance(document, streamArchivo);

            // Our custom Header and Footer is done using Event Handler
            pdfEvents PageEventHandler = new pdfEvents();
            writer.PageEvent = PageEventHandler;

            // Define the page header
            // Define the page header
            if (Session["idEmpresa"].ToString() == "1")
            {
                PageEventHandler.dirImagenHeader = Server.MapPath("~/imagenes/reportes/logo_sertempo.png");
            }
            else
            {
                PageEventHandler.dirImagenHeader = Server.MapPath("~/imagenes/reportes/logo_eficiencia.png");
            }

            PageEventHandler.fechaImpresion = DateTime.Now;
            PageEventHandler.tipoDocumento = "afiliaciones";

            document.Open();

            //capturamos el archivo temporal del response
            String tempFile = Path.GetTempFileName();

            //y lo llenamos con el html de la plantilla
            using (StreamWriter tempwriter = new StreamWriter(tempFile, false))
            {
                tempwriter.Write(html_formato_afilicaciones);
            }

            //leeemos el archivo temporal y lo colocamos en el documento de pdf
            List<IElement> htmlarraylist = HTMLWorker.ParseToList(new StreamReader(tempFile), new StyleSheet());

            foreach (IElement element in htmlarraylist)
            {
                if (element.Chunks.Count > 0)
                {
                    if (element.Chunks[0].Content == "linea para paginacion de pdf")
                    {
                        document.NewPage();
                    }
                    else
                    {
                        document.Add(element);
                    }
                }
                else
                {
                    document.Add(element);
                }
            }

            //limpiamos todo
            document.Close();

            writer.Close();

            return streamArchivo.ToArray();
        }
    }

    /// <summary>
    /// hecho por cesar pulido
    /// el dia 19 de diciembr de 2012
    /// para obtener los archivos de las afiliaciones
    /// </summary>
    /// <param name="prefijoNombreArchivo"></param>
    /// <returns></returns>
    public Dictionary<String, byte[]> ObtenerArchivosAfiliaciones(String prefijoNombreArchivo, Decimal ID_CONTRATO)
    {
        Dictionary<String, byte[]> listaArchivos = new Dictionary<string, byte[]>();

        /*
            ,[ARCHIVO_RADICACION]
            ,[ARCHIVO_RADICACION_EXTENSION]
            ,[ARCHIVO_RADICACION_TAMANO]
            ,[ARCHIVO_RADICACION_TYPE]
        */
        afiliacion _afiliacion = new afiliacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        // ARP
        DataTable TablaArchivoRadicacionARP = _afiliacion.ObtenerDocsRadicacionPorCOntratoYEntidad(ID_CONTRATO, EntidadesAfiliacion.ARP.ToString());
        if (TablaArchivoRadicacionARP.Rows.Count > 0)
        {
            DataRow fila = TablaArchivoRadicacionARP.Rows[0];

            byte[] ARCHIVO_RADICACION = (byte[])fila["ARCHIVO_RADICACION"];
            String ARCHIVO_RADICACION_EXTENSION = fila["ARCHIVO_RADICACION_EXTENSION"].ToString();

            listaArchivos.Add(prefijoNombreArchivo + "AFILIACION_ARP" + ARCHIVO_RADICACION_EXTENSION, ARCHIVO_RADICACION);
        }

        // EPS
        DataTable TablaArchivoRadicacionEPS = _afiliacion.ObtenerDocsRadicacionPorCOntratoYEntidad(ID_CONTRATO, EntidadesAfiliacion.EPS.ToString());
        if (TablaArchivoRadicacionEPS.Rows.Count > 0)
        {
            DataRow fila = TablaArchivoRadicacionEPS.Rows[0];

            byte[] ARCHIVO_RADICACION = (byte[])fila["ARCHIVO_RADICACION"];
            String ARCHIVO_RADICACION_EXTENSION = fila["ARCHIVO_RADICACION_EXTENSION"].ToString();

            listaArchivos.Add(prefijoNombreArchivo + "AFILIACION_EPS" + ARCHIVO_RADICACION_EXTENSION, ARCHIVO_RADICACION);
        }

        // CCF
        DataTable TablaArchivoRadicacionCCF = _afiliacion.ObtenerDocsRadicacionPorCOntratoYEntidad(ID_CONTRATO, EntidadesAfiliacion.CAJA.ToString());
        if (TablaArchivoRadicacionCCF.Rows.Count > 0)
        {
            DataRow fila = TablaArchivoRadicacionCCF.Rows[0];

            byte[] ARCHIVO_RADICACION = (byte[])fila["ARCHIVO_RADICACION"];
            String ARCHIVO_RADICACION_EXTENSION = fila["ARCHIVO_RADICACION_EXTENSION"].ToString();

            listaArchivos.Add(prefijoNombreArchivo + "AFILIACION_CCF" + ARCHIVO_RADICACION_EXTENSION, ARCHIVO_RADICACION);
        }


        // AFP
        DataTable TablaArchivoRadicacionAFP = _afiliacion.ObtenerDocsRadicacionPorCOntratoYEntidad(ID_CONTRATO, EntidadesAfiliacion.AFP.ToString());
        if (TablaArchivoRadicacionAFP.Rows.Count > 0)
        {
            DataRow fila = TablaArchivoRadicacionAFP.Rows[0];

            byte[] ARCHIVO_RADICACION = (byte[])fila["ARCHIVO_RADICACION"];
            String ARCHIVO_RADICACION_EXTENSION = fila["ARCHIVO_RADICACION_EXTENSION"].ToString();

            listaArchivos.Add(prefijoNombreArchivo + "AFILIACION_AFP" + ARCHIVO_RADICACION_EXTENSION, ARCHIVO_RADICACION);
        }

        return listaArchivos;
    }


    public byte[] GenerarPDFEntregaDotacionyEPPs(Decimal id_documento)
    {
        Brainsbits.LLB.almacen.documento _documento = new Brainsbits.LLB.almacen.documento(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());

        DataTable tablaDocumento = _documento.ObtenerAlmRegDocumentoPorId(Convert.ToInt32(id_documento));

        Boolean hayAutos = false;
        String armadoDeAutos = "";

        ordenExamenes _ordenes = new ordenExamenes(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        DataTable tablaOrdenes = _ordenes.ObtenerConRegOrdenExamenPorSolicitud(Convert.ToInt32(0), Convert.ToInt32(0));
        foreach (DataRow filaExamenes in tablaOrdenes.Rows)
        {
            //OBSERVACIONES ES EL CAMPO DONDE ESTA ALMACENADO EL AUTO DE RECOMENDACION SI EXISE
            if (!(String.IsNullOrEmpty(filaExamenes["OBSERVACIONES"].ToString().Trim())))
            {
                if (hayAutos == false)
                {
                    armadoDeAutos = filaExamenes["OBSERVACIONES"].ToString().Trim();
                }
                else
                {
                    armadoDeAutos += "; " + filaExamenes["OBSERVACIONES"].ToString().Trim();
                }
                hayAutos = true;
            }
        }

        if (hayAutos)
        {
            /*
            ---------------TAGS ENCONTRADOS EN AUTOS_RECOMENDACION-------------------
            [FECHA_AUTO]			= FECHA DE LA CABECERA DE LA APERTURA DE CUENTA
            [NOMBRE_TRABAJADOR]		= NOMBRE DEL BANCO
            [TELEFONO_TRABAJADOR]		= TELEFONO DEL TRABAJADOR
            [TIPO_DOCUMENTO_IDENTIDAD]	= tipo de documento del trabajador
            [NUMERO_DOCUMENTO_IDENTIDAD]	= numero del documento de identidad del trabajador
            [AUTOS_RECOMENDACION]		= nombre analista que firma
            [NOMBRE_EMPLEADOR]		= cartgo del que firma
            */

            radicacionHojasDeVida _sol = new radicacionHojasDeVida(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaSol = _sol.ObtenerRegSolicitudesingresoPorIdSolicitud(Convert.ToInt32(0));
            DataRow filaSol = tablaSol.Rows[0];

            //DATOS DEL INFORME
            DateTime FECHA_AUTO = DateTime.Now;
            String NOMBRE_TRABAJADOR = filaSol["NOMBRES"].ToString().Trim() + " " + filaSol["APELLIDOS"].ToString().Trim();
            String TELEFONO_TRABAJADOR = filaSol["TEL_ASPIRANTE"].ToString().Trim();
            String TIPO_DOCUMENTO_IDENTIDAD = filaSol["TIP_DOC_IDENTIDAD"].ToString().Trim();
            String NUMERO_DOCUMENTO_IDENTIDAD = filaSol["NUM_DOC_IDENTIDAD"].ToString().Trim();
            String AUTOS_RECOMENDACION = armadoDeAutos;
            String NOMBRE_EMPLEADOR = null;

            if (Session["idEmpresa"].ToString() == "1")
            {
                NOMBRE_EMPLEADOR = tabla.VAR_NOMBRE_SERTEMPO;
            }
            else
            {
                NOMBRE_EMPLEADOR = tabla.VAR_NOMBRE_EYS;
            }

            //En esta variable cargamos el documento plantilla
            StreamReader archivo = new StreamReader(Server.MapPath(@"~\plantillas_reportes\autos_recomendacion.htm"));

            String html = archivo.ReadToEnd();

            archivo.Dispose();
            archivo.Close();

            html = html.Replace("[FECHA_AUTO]", FECHA_AUTO.ToLongDateString());
            html = html.Replace("[NOMBRE_TRABAJADOR]", NOMBRE_TRABAJADOR);
            html = html.Replace("[TELEFONO_TRABAJADOR]", TELEFONO_TRABAJADOR);
            html = html.Replace("[TIPO_DOCUMENTO_IDENTIDAD]", TIPO_DOCUMENTO_IDENTIDAD);
            html = html.Replace("[NUMERO_DOCUMENTO_IDENTIDAD]", NUMERO_DOCUMENTO_IDENTIDAD);
            html = html.Replace("[AUTOS_RECOMENDACION]", AUTOS_RECOMENDACION);
            html = html.Replace("[NOMBRE_EMPLEADOR]", NOMBRE_EMPLEADOR);
            html = html.Replace("[DIR_FIRMA_SALUD]", tabla.DIR_IMAGENES_PARA_PDF + "/firma_autos_recomendacion.jpg");

            //creamos un configuramos el documento de pdf
            //(tamaño de la hoja,margen izq, margen der, margin arriba margen abajo)
            iTextSharp.text.Document document = new iTextSharp.text.Document(new Rectangle(612, 397), 25, 25, 75, 30);

            using (MemoryStream streamArchivo = new MemoryStream())
            {
                iTextSharp.text.pdf.PdfWriter writer = PdfWriter.GetInstance(document, streamArchivo);

                // Our custom Header and Footer is done using Event Handler
                pdfEvents PageEventHandler = new pdfEvents();
                writer.PageEvent = PageEventHandler;

                // Define the page header
                if (Session["idEmpresa"].ToString() == "1")
                {
                    PageEventHandler.dirImagenHeader = Server.MapPath("~/imagenes/reportes/logo_sertempo.png");
                }
                else
                {
                    PageEventHandler.dirImagenHeader = Server.MapPath("~/imagenes/reportes/logo_eficiencia.png");
                }
                PageEventHandler.fechaImpresion = DateTime.Now;
                PageEventHandler.tipoDocumento = "autos_recomendacion";

                document.Open();

                //capturamos el archivo temporal del response
                String tempFile = Path.GetTempFileName();

                //y lo llenamos con el html de la plantilla
                using (StreamWriter tempwriter = new StreamWriter(tempFile, false))
                {
                    tempwriter.Write(html);
                }

                //leeemos el archivo temporal y lo colocamos en el documento de pdf
                List<IElement> htmlarraylist = HTMLWorker.ParseToList(new StreamReader(tempFile), new StyleSheet());

                foreach (IElement element in htmlarraylist)
                {
                    if (element.Chunks.Count > 0)
                    {
                        if (element.Chunks[0].Content == "linea para paginacion de pdf")
                        {
                            document.NewPage();
                        }
                        else
                        {
                            document.Add(element);
                        }
                    }
                    else
                    {
                        document.Add(element);
                    }
                }

                //limpiamos todo
                document.Close();

                writer.Close();

                return streamArchivo.ToArray();
            }
        }
        else
        {
            return null;
        }
    }



    /*
     * 
     * metodos para generar el manual de servicio en pdf
     * 
    */

    /// <summary>
    /// hecho por cesar pulido
    /// el dia 16 de enro de 2013
    /// para generar un html apropiado para un titulo de manual de servicio
    /// </summary>
    /// <param name="titulo"></param>
    /// <returns></returns>
    public String GenerarTituloManualServicio(String titulo)
    { 
        // verifiada por _datos
        String plantillaTitulo = "<div style=\"text-align:justify; font-weight:bold;\">";
        plantillaTitulo += "<b>[TITULO]</b>";
        plantillaTitulo += "</div>";

        plantillaTitulo = plantillaTitulo.Replace("[TITULO]", titulo);

        return plantillaTitulo;
    }

    /// <summary>
    /// hecho por cesar pulido
    /// el dia 16 de enero de 2013
    /// para escribir en el pdf del manual la seccion de identificacion del cliente
    /// </summary>
    /// <param name="ID_EMPRESA"></param>
    /// <param name="htmlSeccion"></param>
    /// <param name="_datos"></param>
    /// <returns></returns>
    private String CargarIdentificacionCliente(Decimal ID_EMPRESA, String htmlSeccion, Conexion _datos)
    {
        //verificado por _datos

        cliente _cliente = new cliente(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        //ok ----------------------
        DataTable tablaCliente = _cliente.ObtenerEmpresaConIdEmpresa(ID_EMPRESA, _datos);
        DataRow filaCliente = tablaCliente.Rows[0];

        htmlSeccion = htmlSeccion.Replace("[RAZ_SOCIAL]", filaCliente["RAZ_SOCIAL"].ToString().Trim());
        htmlSeccion = htmlSeccion.Replace("[ACT_ECO]", filaCliente["ACT_ECO"].ToString().Trim());
        htmlSeccion = htmlSeccion.Replace("[NIT]", filaCliente["NIT_EMPRESA"].ToString().Trim() + "-" + filaCliente["DIG_VER"].ToString().Trim());
        htmlSeccion = htmlSeccion.Replace("[REP_LEGAL]", filaCliente["NOM_REP_LEGAL"].ToString().Trim());
        htmlSeccion = htmlSeccion.Replace("[NUM_DOC_REP_LEGAL]", filaCliente["TIP_DOC_REP_LEGAL"].ToString().Trim() + " " + filaCliente["CC_REP_LEGAL"].ToString().Trim());
        htmlSeccion = htmlSeccion.Replace("[DIR_CIU_EMPRESA]", filaCliente["DIR_EMP"].ToString().Trim() + " (" + filaCliente["ID_CIUDAD_EMPRESA"].ToString().Trim() + ").");
        htmlSeccion = htmlSeccion.Replace("[TEL_EMPRESA]", filaCliente["TEL_EMP"].ToString().Trim() + " / " + filaCliente["TEL_EMP1"].ToString().Trim() + " CEL: " + filaCliente["NUM_CELULAR"].ToString());

        return htmlSeccion;
    }

    /// <summary>
    /// hecho por cesar pulido
    /// el dia 16 de enero de 2013
    /// para adicionar al pdf del manual la seccion de identificacion del cliente
    /// </summary>
    /// <param name="ID_EMPRESA"></param>
    /// <param name="contadorTitulo"></param>
    /// <param name="_datos"></param>
    /// <returns></returns>
    public String ObtenerSeccionIdentificacionCliente(Decimal ID_EMPRESA, int contadorTitulo, Conexion _datos)
    {
        // verificada por _datos

        String tituloSeccion = "IDENTIFICACION DEL CLIENTE";
        String seccion = _div_seccion_manual;

        if(contadorTitulo == 0)
        {
            //ok
            seccion += GenerarTituloManualServicio(tituloSeccion);
        }
        else
        {
            //ok
            seccion += GenerarTituloManualServicio(contadorTitulo.ToString() + ". " + tituloSeccion);    
        }

        seccion += "<br>";
        seccion += "<table width=\"75%\" style=\"margin:0 auto; text-align:center;\" align=\"center\">";
        seccion += "<tr>";
        seccion += "  <td width=\"30%\" style=\"text-align:left;\">";
        seccion += "    Razón Social:";
        seccion += "  </td>";
        seccion += "  <td width=\"70%\" style=\"font-weight:bold; text-align:left;\">";
        seccion += "    [RAZ_SOCIAL]";
        seccion += "  </td>";
        seccion += "</tr>";
        seccion += "<tr>";
        seccion += "  <td width=\"30%\" style=\"text-align:left;\">";
        seccion += "    Actividad Económica:";
        seccion += "  </td>";
        seccion += "  <td width=\"70%\" style=\"font-weight:bold; text-align:left;\">";
        seccion += "    [ACT_ECO]";
        seccion += "  </td>";
        seccion += "</tr>";
        seccion += "<tr>";
        seccion += "  <td width=\"30%\" style=\"text-align:left;\">";
        seccion += "    NIT:";
        seccion += "  </td>";
        seccion += "  <td width=\"70%\" style=\"font-weight:bold; text-align:left;\">";
        seccion += "    [NIT]";
        seccion += "  </td>";
        seccion += "</tr>";
        seccion += "<tr>";
        seccion += "  <td width=\"30%\" style=\"text-align:left;\">";
        seccion += "    Representante Legal:";
        seccion += "  </td>";
        seccion += "  <td width=\"70%\" style=\"font-weight:bold; text-align:left;\">";
        seccion += "    [REP_LEGAL]";
        seccion += "  </td>";
        seccion += "</tr>";
        seccion += "<tr>";
        seccion += "  <td width=\"30%\" style=\"text-align:left;\">";
        seccion += "    Documento Identidad Representante Legal:";
        seccion += "  </td>";
        seccion += "  <td width=\"70%\" style=\"font-weight:bold; text-align:left;\">";
        seccion += "    [NUM_DOC_REP_LEGAL]";
        seccion += "  </td>";
        seccion += "</tr>";
        seccion += "<tr>";
        seccion += "  <td width=\"30%\" style=\"text-align:left;\">";
        seccion += "    Dirección y Ciudad:";
        seccion += "  </td>";
        seccion += "  <td width=\"70%\" style=\"font-weight:bold; text-align:left;\">";
        seccion += "    [DIR_CIU_EMPRESA]";
        seccion += "  </td>";
        seccion += "</tr>";
        seccion += "<tr>";
        seccion += "  <td width=\"30%\" style=\"text-align:left;\">";
        seccion += "    Telefono:";
        seccion += "  </td>";
        seccion += "  <td width=\"70%\" style=\"font-weight:bold; text-align:left;\">";
        seccion += "    [TEL_EMPRESA]";
        seccion += "  </td>";
        seccion += "</tr>";
        seccion += "</table>";

        //ok---------------------------------
        seccion = CargarIdentificacionCliente(ID_EMPRESA, seccion, _datos);

        seccion += "</div>";
        return seccion;
    }

    /// <summary>
    /// hecho por cesar pulido
    /// el dia 17 de enero de 2013
    /// para cargar n el pdf la seccion de modificaciones
    /// </summary>
    /// <param name="ID_EMPRESA"></param>
    /// <param name="_datos"></param>
    /// <returns></returns>
    public String CargarModificacionesManual(Decimal ID_EMPRESA, Conexion _datos)
    {
        // verificado por _datos

        String htmlSeccion = String.Empty;

        ManualServicio _manual = new ManualServicio(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        //ok --------------------------
        DataTable tablaModificaciones = _manual.ObtenerModificacionesManualPorEmpresa(ID_EMPRESA, _datos);

        foreach (DataRow fila in tablaModificaciones.Rows)
        {
            htmlSeccion += "<tr>";
            htmlSeccion += "  <td width=\"5%\" style=\"text-align:center;\">";
            htmlSeccion += fila["VERSION_MAYOR"].ToString().Trim() + "." + fila["VERSION_MENOR"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"12%\" style=\"text-align:center;\">";
            htmlSeccion += Convert.ToDateTime(fila["FECHA_EMISION"]).ToShortDateString();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"13%\" style=\"text-align:center;\">";
            htmlSeccion += fila["AREA"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"11%\" style=\"text-align:center;\">";
            htmlSeccion += fila["ACCION"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"49%\" style=\"text-align:justify;\">";
            htmlSeccion += fila["CAMBIO"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"10%\" style=\"text-align:center;\">";
            htmlSeccion += fila["USU_CRE"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "</tr>";
        }

        return htmlSeccion;
    }

    /// <summary>
    /// hecho por cesar pulido
    /// el dia 17 de enero de 2013
    /// para adcionar al pdf la seccion de control de modificaciones
    /// </summary>
    /// <param name="ID_EMPRESA"></param>
    /// <param name="contadorTitulo"></param>
    /// <param name="_datos"></param>
    /// <returns></returns>
    public String ObtenerSeccionControlModificaciones(Decimal ID_EMPRESA, int contadorTitulo, Conexion _datos)
    {
        String tituloSeccion = "CONTROL DE MODIFICACIONES";
        String seccion = _div_seccion_manual;

        if (contadorTitulo == 0)
        {
            //ok
            seccion += GenerarTituloManualServicio(tituloSeccion);
        }
        else
        {
            //ok
            seccion += GenerarTituloManualServicio(contadorTitulo.ToString() + ". " + tituloSeccion);
        }

        seccion += "<br>";
        seccion += "<table width=\"100%\" border=\"1\" cellpadding=\"2\" cellspacing=\"0\">";
        seccion += "<tr bgcolor=\"#dddddd\">";
        seccion += "  <td width=\"5%\" style=\"text-align:center; font-weight:bold;\">";
        seccion += "    V.";
        seccion += "  </td>";
        seccion += "  <td width=\"12%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Fecha Emisión";
        seccion += "  </td>";
        seccion += "  <td width=\"13%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Area";
        seccion += "  </td>";
        seccion += "  <td width=\"11%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Acción";
        seccion += "  </td>";
        seccion += "  <td width=\"49%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Cambio";
        seccion += "  </td>";
        seccion += "  <td width=\"10%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Usuario";
        seccion += "  </td>";
        seccion += "</tr>";

        //ok
        seccion += CargarModificacionesManual(ID_EMPRESA, _datos);

        seccion += "</table>";

        seccion += "</div>";

        return seccion;
    }

    /// <summary>
    /// hecho por cesar pulido
    /// el dia 17 de enero de 2013
    /// para cargar en el pdf los contactos comerciales
    /// </summary>
    /// <param name="ID_EMPRESA"></param>
    /// <param name="_datos"></param>
    /// <returns></returns>
    public String CargarContactosComerciales(Decimal ID_EMPRESA, Conexion _datos)
    {
        contactos _contacto = new contactos(Session["idEmpresa"].ToString());
        //ok--------------------
        DataTable tablaContactos = _contacto.ObtenerContactosPorIdEmpresa(ID_EMPRESA, tabla.proceso.ContactoComercial, _datos);

        String htmlSeccion = String.Empty;

        foreach (DataRow fila in tablaContactos.Rows)
        {
            htmlSeccion += "<tr>";
            htmlSeccion += "  <td width=\"20%\" style=\"text-align:left;\">";
            htmlSeccion += fila["CONT_NOM"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"15%\" style=\"text-align:left;\">";
            htmlSeccion += fila["NOMBRE_CIUDAD"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"25%\" style=\"text-align:left;\">";
            htmlSeccion += fila["CONT_CARGO"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"15%\" style=\"text-align:center;\">";
            htmlSeccion += fila["CONT_TEL"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"25%\" style=\"text-align:left;\">";
            htmlSeccion += fila["CONT_MAIL"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "</tr>";
        }

        return htmlSeccion;
    }

    /// <summary>
    /// hecho por cesar pulido
    /// el dia 17 de enro de 2013
    /// para genera un html acorde a un subtitulo del manual de servicio
    /// </summary>
    /// <param name="Subtitulo"></param>
    /// <returns></returns>
    public String GenerarSubTituloManualServicio(String Subtitulo)
    {
        //verificado por _datos

        String plantillaTitulo = _div_subTitulo_manual;
        plantillaTitulo += "<b>[SUBTITULO]</b>";
        plantillaTitulo += "</div>";

        plantillaTitulo = plantillaTitulo.Replace("[SUBTITULO]", Subtitulo);

        return plantillaTitulo;
    }

    /// <summary>
    /// hecho por cesar puliod}el dia 17 de enro de 2013
    /// para generar un html acorde a un texto justificado en nel manual de servvico
    /// </summary>
    /// <param name="texto"></param>
    /// <returns></returns>
    public String GenerarTextoJustificadoManualServicio(String texto)
    {
        //verificado por _datos

        String palntillaTexto = _div_textoJustificado_manual;
        palntillaTexto += "[TEXTO]";
        palntillaTexto += "</div>";

        palntillaTexto = palntillaTexto.Replace("[TEXTO]", texto);

        return palntillaTexto;
    }

    /// <summary>
    /// hecho por cesar pulido
    /// el dia 17 de enero de enero de 2013
    /// para cargar en el pdf la seccion de unidad de negocio
    /// </summary>
    /// <param name="ID_EMPRESA"></param>
    /// <param name="_datos"></param>
    /// <returns></returns>
    public String CargarUnidadDeNegocio(Decimal ID_EMPRESA, Conexion _datos)
    {
        //verificado por _datos

        seguridad _seguridad = new seguridad(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        //ok -----------------
        DataTable tablaContratosOriginal = _seguridad.ObtenerUsuariosPorEmpresa(ID_EMPRESA, _datos);

        String htmlSeccion = String.Empty;

        foreach (DataRow fila in tablaContratosOriginal.Rows)
        {
            htmlSeccion += "<tr>";
            htmlSeccion += "  <td width=\"20%\" style=\"text-align:center;\">";
            htmlSeccion += fila["USU_LOG"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"30%\" style=\"text-align:justify;\">";
            htmlSeccion += fila["NOMBRES_EMPLEADO"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"50%\" style=\"text-align:justify;\">";
            htmlSeccion += fila["UNIDAD_NEGOCIO"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "</tr>";
        }

        return htmlSeccion;

    }

    /// <summary>
    /// hecho por cesar pulido
    /// el dia 17 de enro de 2013
    /// para cargar la cobertura en el pdf
    /// </summary>
    /// <param name="ID_EMPRESA"></param>
    /// <param name="_datos"></param>
    /// <returns></returns>
    public String CargarCobertura(Decimal ID_EMPRESA, Conexion _datos)
    {
        //verificado por _Datos

        cobertura _cobertura = new cobertura(Session["idEmpresa"].ToString());
        //ok---------------------------------
        DataTable tablaCobertura = _cobertura.obtenerCoberturaDeUnCliente(ID_EMPRESA, _datos);

        String htmlSeccion = String.Empty;

        foreach (DataRow fila in tablaCobertura.Rows)
        {
            htmlSeccion += "<tr>";
            htmlSeccion += "  <td width=\"15%\" style=\"text-align:center;\">";
            htmlSeccion += fila["Código Ciudad"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"20%\" style=\"text-align:left;\">";
            htmlSeccion += fila["Regional"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"32%\" style=\"text-align:left;\">";
            htmlSeccion += fila["Departamento"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"33%\" style=\"text-align:left;\">";
            htmlSeccion += fila["Ciudad"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "</tr>";
        }

        return htmlSeccion;
    }

    /// <summary>
    /// hecho por cesar pulido
    /// el dia 17 de enero de 2013
    /// para cargar los adicionales de cada una de las secciones del manual de servicio
    /// </summary>
    /// <param name="ID_EMPRESA"></param>
    /// <param name="seccion"></param>
    /// <param name="contadorTitulo"></param>
    /// <param name="contadorSubtitulo"></param>
    /// <param name="_datos"></param>
    /// <returns></returns>
    private String CargarAdicionales(Decimal ID_EMPRESA, ManualServicio.ListaSecciones seccion, int contadorTitulo, int contadorSubtitulo, Conexion _datos)
    {
        //verificado por _datos

        ManualServicio _manual = new ManualServicio(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        //ok---------------------
        DataTable tablaAdicionales = _manual.ObtenerAdicionalesPorEmpresaYArea(ID_EMPRESA, seccion.ToString(), _datos);

        String htmlSeccion = String.Empty;

        for (int i = 0; i < tablaAdicionales.Rows.Count; i++)
        {
            DataRow fila = tablaAdicionales.Rows[i];
            contadorSubtitulo += 1;

            if(i > 0)
            {
                htmlSeccion += "<br>";
            }

            //ok
            htmlSeccion += GenerarSubTituloManualServicio(contadorTitulo + "." + contadorSubtitulo + " " + fila["TITULO"].ToString().Trim());

            htmlSeccion += "<br>";

            //ok
            htmlSeccion += GenerarTextoJustificadoManualServicio(fila["DESCRIPCION"].ToString().Trim());
        }

        return htmlSeccion;
    }

    /// <summary>
    /// hecho por cesar pulido
    /// el dia 17 de enro de 2013
    /// para adicionar en el pdf la seccion de seleccion
    /// </summary>
    /// <param name="ID_EMPRESA"></param>
    /// <param name="contadorTitulo"></param>
    /// <param name="_datos"></param>
    /// <returns></returns>
    public String ObtenerSeccionGestionComercial(Decimal ID_EMPRESA, int contadorTitulo, Conexion _datos)
    {
        String tituloSeccion = "GESTIÓN COMERCIAL";
        String seccion = _div_seccion_manual;

        int contadorSubSecciones = 0;

        if (contadorTitulo == 0)
        {
            //ok
            seccion += GenerarTituloManualServicio(tituloSeccion);
        }
        else
        {
            //ok
            seccion += GenerarTituloManualServicio(contadorTitulo.ToString() + ". " + tituloSeccion);
        }

        seccion += "<br>";

        contadorSubSecciones += 1;

        //ok
        seccion += GenerarSubTituloManualServicio(contadorTitulo + "." + contadorSubSecciones + " " + "CONTÁCTOS COMERCIALES");

        seccion += "<br>";

        seccion += "<table width=\"100%\" border=\"1\" cellpadding=\"2\" cellspacing=\"0\">";
        seccion += "<tr bgcolor=\"#dddddd\">";
        seccion += "  <td width=\"20%\" style=\"text-align:center; font-weight:bold;\">";
        seccion += "    Contácto";
        seccion += "  </td>";
        seccion += "  <td width=\"15%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Ciudad";
        seccion += "  </td>";
        seccion += "  <td width=\"25%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Cargo";
        seccion += "  </td>";
        seccion += "  <td width=\"15%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Teléfono";
        seccion += "  </td>";
        seccion += "  <td width=\"25%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    E-Mail";
        seccion += "  </td>";
        seccion += "</tr>";

        //ok
        seccion += CargarContactosComerciales(ID_EMPRESA, _datos);

        seccion += "</table>";

        seccion += "<br>";

        contadorSubSecciones += 1;

        //ok
        seccion += GenerarSubTituloManualServicio(contadorTitulo + "." + contadorSubSecciones + " " + "UNIDAD DE NEGOCIO");

        seccion += "<br>";

        seccion += "<table width=\"100%\" border=\"1\" cellpadding=\"2\" cellspacing=\"0\">";
        seccion += "<tr bgcolor=\"#dddddd\">";
        seccion += "  <td width=\"20%\" style=\"text-align:center; font-weight:bold;\">";
        seccion += "    Login Usuario";
        seccion += "  </td>";
        seccion += "  <td width=\"30%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Empleado";
        seccion += "  </td>";
        seccion += "  <td width=\"50%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Unidad de Negocio";
        seccion += "  </td>";
        seccion += "</tr>";

        //ok
        seccion += CargarUnidadDeNegocio(ID_EMPRESA, _datos);

        seccion += "</table>";

        seccion += "<br>";

        contadorSubSecciones += 1;

        //ok
        seccion += GenerarSubTituloManualServicio(contadorTitulo + "." + contadorSubSecciones + " " + "COBERTURA");

        seccion += "<br>";

        seccion += "<table width=\"100%\" border=\"1\" cellpadding=\"2\" cellspacing=\"0\">";
        seccion += "<tr bgcolor=\"#dddddd\">";
        seccion += "  <td width=\"15%\" style=\"text-align:center; font-weight:bold;\">";
        seccion += "    Cód. Ciudad";
        seccion += "  </td>";
        seccion += "  <td width=\"20%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Regional";
        seccion += "  </td>";
        seccion += "  <td width=\"32%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Departamento";
        seccion += "  </td>";
        seccion += "  <td width=\"33%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Ciudad";
        seccion += "  </td>";
        seccion += "</tr>";

        //ok
        seccion += CargarCobertura(ID_EMPRESA, _datos);

        seccion += "</table>";

        seccion += "<br>";

        //ok
        seccion += CargarAdicionales(ID_EMPRESA, ManualServicio.ListaSecciones.Comercial, contadorTitulo, contadorSubSecciones, _datos);



        seccion += "</div>";

        return seccion;
    }

    /// <summary>
    /// hecho por cesar pulido
    /// el dia 17 de enero de 2013
    /// para cargar en el pdf la seccion de cargos y perfiles
    /// </summary>
    /// <param name="ID_EMPRESA"></param>
    /// <returns></returns>
    public String CargarCargosYPerfiles(Decimal ID_EMPRESA, Conexion _datos)
    {
        //verificado por _datos
        perfil _perfil = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        //ok--------------------
        DataTable tablaPerfiles = _perfil.ObtenerVenDPerfilesConOcupacionPorIdEmpresa(ID_EMPRESA, _datos);

        String htmlSeccion = String.Empty;

        foreach (DataRow fila in tablaPerfiles.Rows)
        {
            htmlSeccion += "<tr>";
            htmlSeccion += "  <td width=\"30%\" style=\"text-align:justify;\">";
            htmlSeccion += fila["NOM_OCUPACION"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"12%\" style=\"text-align:center;\">";
            htmlSeccion += fila["EDAD_MAX"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"12%\" style=\"text-align:center;\">";
            htmlSeccion += fila["EDAD_MAX"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"10%\" style=\"text-align:center;\">";
            htmlSeccion += fila["SEXO"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"18%\" style=\"text-align:center;\">";
            htmlSeccion += fila["EXPERIENCIA"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"18%\" style=\"text-align:center;\">";
            htmlSeccion += fila["NIV_ESTUDIOS"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "</tr>";
        }

        return htmlSeccion;
    }

    /// <summary>
    /// hecho por cear pulido
    /// el dia 17 de enero de 2013
    /// para crear una tabla acorde a laos datos de perfiles docuemntos y pruebas
    /// </summary>
    /// <returns></returns>
    private DataTable ConfigurarTablaPerfilesDocumentosPruebas()
    {
        DataTable tablaTemp = new DataTable();

        tablaTemp.Columns.Add("REGISTRO");
        tablaTemp.Columns.Add("NOM_OCUPACION");
        tablaTemp.Columns.Add("DOCUMENTOS_REQUERIDOS");
        tablaTemp.Columns.Add("PRUEBAS_APLICADAS");

        return tablaTemp;
    }

    /// <summary>
    /// hecho por cear pulido
    /// el dia 17 de enero de 2013
    /// paracargar en el pdf la seccion de docuemntos y pruebas aplicadas a un perfil
    /// </summary>
    /// <param name="ID_EMPRESA"></param>
    /// <returns></returns>
    private String CargarDocumentosPruebasPerfiles(Decimal ID_EMPRESA, Conexion _datos)
    {
        DataTable tablaPerfilesDocumentosPruebas = ConfigurarTablaPerfilesDocumentosPruebas();

        //capturamos los perfils de la empresa
        perfil _perfil = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        //ok -----------------------
        DataTable tablaPerfiles = _perfil.ObtenerVenDPerfilesConOcupacionPorIdEmpresa(ID_EMPRESA, _datos);

        String htmlSeccion = String.Empty;

        //recorremos cada uno de los perfiles de la tabla
        foreach (DataRow filaPerfil in tablaPerfiles.Rows)
        {
            Decimal ID_PERFIL = Convert.ToDecimal(filaPerfil["REGISTRO"]);

            //CAPTURAMMOS DOCUEMNTOS REQUERIDOS PARA ESTE PERFIL
            documentoPerfil _documentoPerfil = new documentoPerfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            //ok ------------------------
            DataTable tablaDocumentos = _documentoPerfil.ObtenerPorIdPerfil(ID_PERFIL, _datos);

            String DOCUMENTOS_REQUERIDOS = String.Empty;

            foreach (DataRow filaDocumento in tablaDocumentos.Rows)
            {
                if (String.IsNullOrEmpty(DOCUMENTOS_REQUERIDOS) == true)
                {
                    DOCUMENTOS_REQUERIDOS = filaDocumento["Documento"].ToString().Trim();
                }
                else
                {
                    DOCUMENTOS_REQUERIDOS += ", " + filaDocumento["Documento"].ToString().Trim();
                }
            }

            //CAPTURAMOS LAS PRUEBAS QUE DEBEN SER APLICADAS AL PERFIL
            pruebaPerfil _pruebaPerfil = new pruebaPerfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            //ok ---------------------------------
            DataTable tablaPruebas = _pruebaPerfil.ObtenerPorIdPerfil(ID_PERFIL, _datos);

            String PRUEBAS_APLICADAS = String.Empty;

            foreach (DataRow filaPrueba in tablaPruebas.Rows)
            {
                if (String.IsNullOrEmpty(PRUEBAS_APLICADAS) == true)
                {
                    PRUEBAS_APLICADAS = filaPrueba["Prueba"].ToString().Trim();
                }
                else
                {
                    PRUEBAS_APLICADAS += ", " + filaPrueba["Prueba"].ToString().Trim();
                }
            }

            //ahora creamos la fila que se le ira asignando a la tabla para llenar a grilla de docuemntos y pruebas por perfil
            DataRow filaPerfilDocumentosPruebas = tablaPerfilesDocumentosPruebas.NewRow();

            /*
                tablaTemp.Columns.Add("REGISTRO");
                tablaTemp.Columns.Add("NOM_OCUPACION");
                tablaTemp.Columns.Add("DOCUMENTOS_REQUERIDOS");
                tablaTemp.Columns.Add("PRUEBAS_APLICADAS"); 
            */
            filaPerfilDocumentosPruebas["REGISTRO"] = filaPerfil["REGISTRO"];
            filaPerfilDocumentosPruebas["NOM_OCUPACION"] = filaPerfil["NOM_OCUPACION"];
            if (String.IsNullOrEmpty(DOCUMENTOS_REQUERIDOS) == true)
            {
                filaPerfilDocumentosPruebas["DOCUMENTOS_REQUERIDOS"] = "Ninguno.";
            }
            else
            {
                filaPerfilDocumentosPruebas["DOCUMENTOS_REQUERIDOS"] = DOCUMENTOS_REQUERIDOS;
            }

            if (String.IsNullOrEmpty(PRUEBAS_APLICADAS) == true)
            {
                filaPerfilDocumentosPruebas["PRUEBAS_APLICADAS"] = "Ninguna.";
            }
            else
            {
                filaPerfilDocumentosPruebas["PRUEBAS_APLICADAS"] = PRUEBAS_APLICADAS;
            }

            //asignamos la fila a la tabla final
            tablaPerfilesDocumentosPruebas.Rows.Add(filaPerfilDocumentosPruebas);
            tablaPerfilesDocumentosPruebas.AcceptChanges();
        }

        //tablaTemp.Columns.Add("REGISTRO");
        //tablaTemp.Columns.Add("NOM_OCUPACION");
        //tablaTemp.Columns.Add("DOCUMENTOS_REQUERIDOS");
        //tablaTemp.Columns.Add("PRUEBAS_APLICADAS");

        foreach(DataRow fila in tablaPerfilesDocumentosPruebas.Rows)
        {
            htmlSeccion += "<tr>";
            htmlSeccion += "  <td width=\"25%\" style=\"text-align:left;\">";
            htmlSeccion += fila["NOM_OCUPACION"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"42%\" style=\"text-align:justify;\">";
            htmlSeccion += fila["DOCUMENTOS_REQUERIDOS"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"33%\" style=\"text-align:justify;\">";
            htmlSeccion += fila["PRUEBAS_APLICADAS"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "</tr>";
        }

        return htmlSeccion;
    }

    /// <summary>
    /// hecho por cesar pulido
    /// el dia 17 de enero de 2013
    /// para cargar en el pdf la seccion de condiciones de envio en seleccion
    /// </summary>
    /// <param name="ID_EMPRESA"></param>
    /// <returns></returns>
    private String CargarCondicionesEnvioSeleccion(Decimal ID_EMPRESA, Conexion _datos)
    {
        envioCandidato _envioCandidato = new envioCandidato(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        //ok -----------------
        DataTable tablaCondicionesEnvio = _envioCandidato.ObtenerTodosLosContactosParaEnvioDeCandidatosPorIdEmpresa(ID_EMPRESA, _datos);

        String htmlSeccion = String.Empty;

        foreach (DataRow fila in tablaCondicionesEnvio.Rows)
        {
            htmlSeccion += "<tr>";
            htmlSeccion += "  <td width=\"12%\" style=\"text-align:center;\">";
            htmlSeccion += fila["NOMBRE_CIUDAD"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"13%\" style=\"text-align:left;\">";
            htmlSeccion += fila["NOMBRE_CONTACTO"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"16%\" style=\"text-align:left;\">";
            htmlSeccion += fila["MAIL_CONTACTO"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"16%\" style=\"text-align:justify;\">";
            htmlSeccion += fila["DIR_ENVIO"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"13%\" style=\"text-align:center;\">";
            htmlSeccion += fila["TEL_ENVIO"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"30%\" style=\"text-align:justify;\">";
            htmlSeccion += fila["COND_ENVIO"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "</tr>";
        }

        return htmlSeccion;
    }

    /// <summary>
    /// hecho por cesar pulido
    /// el dia 17 de enero de 2013
    /// para adicionar en el pdf la seccion del proceso de selecicon
    /// </summary>
    /// <param name="ID_EMPRESA"></param>
    /// <param name="contadorTitulo"></param>
    /// <returns></returns>
    public String ObtenerSeccionProcesoSeleccion(Decimal ID_EMPRESA, int contadorTitulo, Conexion _datos)
    {
        //verificado por _Datos

        String tituloSeccion = "PROCESO DE SELECCIÓN";
        String seccion = _div_seccion_manual;

        int contadorSubSecciones = 0;

        if (contadorTitulo == 0)
        {
            //ok
            seccion += GenerarTituloManualServicio(tituloSeccion);
        }
        else
        {
            //ok
            seccion += GenerarTituloManualServicio(contadorTitulo.ToString() + ". " + tituloSeccion);
        }

        seccion += "<br>";

        contadorSubSecciones += 1;

        //ok
        seccion += GenerarSubTituloManualServicio(contadorTitulo + "." + contadorSubSecciones + " " + "CARGOS Y PERFILES");

        seccion += "<br>";

        seccion += "<table width=\"100%\" border=\"1\" cellpadding=\"2\" cellspacing=\"0\">";
        seccion += "<tr bgcolor=\"#dddddd\">";
        seccion += "  <td width=\"30%\" style=\"text-align:center; font-weight:bold;\">";
        seccion += "    Cargo";
        seccion += "  </td>";
        seccion += "  <td width=\"12%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Edad Max.";
        seccion += "  </td>";
        seccion += "  <td width=\"12%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Edad Min.";
        seccion += "  </td>";
        seccion += "  <td width=\"10%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Sexo";
        seccion += "  </td>";
        seccion += "  <td width=\"18%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Experiencia";
        seccion += "  </td>";
        seccion += "  <td width=\"18%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Nivel Estudios";
        seccion += "  </td>";
        seccion += "</tr>";

        //ok
        seccion += CargarCargosYPerfiles(ID_EMPRESA, _datos);

        seccion += "</table>";



        seccion += "<br>";

        contadorSubSecciones += 1;

        //ok
        seccion += GenerarSubTituloManualServicio(contadorTitulo + "." + contadorSubSecciones + " " + "DOCUMENTOS Y PRUEBAS APLICADAS A PERFILES");

        seccion += "<br>";

        seccion += "<table width=\"100%\" border=\"1\" cellpadding=\"2\" cellspacing=\"0\">";
        seccion += "<tr bgcolor=\"#dddddd\">";
        seccion += "  <td width=\"25%\" style=\"text-align:center; font-weight:bold;\">";
        seccion += "    Cargo";
        seccion += "  </td>";
        seccion += "  <td width=\"42%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Documentos Requeridos";
        seccion += "  </td>";
        seccion += "  <td width=\"33%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Pruebas Aplicadas";
        seccion += "  </td>";
        seccion += "</tr>";

        //ok
        seccion += CargarDocumentosPruebasPerfiles(ID_EMPRESA, _datos);

        seccion += "</table>";




        seccion += "<br>";

        contadorSubSecciones += 1;

        //ok
        seccion += GenerarSubTituloManualServicio(contadorTitulo + "." + contadorSubSecciones + " " + "CONDICIONES DE ENVÍO");

        seccion += "<br>";

        seccion += "<table width=\"100%\" border=\"1\" cellpadding=\"2\" cellspacing=\"0\">";
        seccion += "<tr bgcolor=\"#dddddd\">";
        seccion += "  <td width=\"12%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Ciudad";
        seccion += "  </td>";
        seccion += "  <td width=\"13%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Contácto";
        seccion += "  </td>";
        seccion += "  <td width=\"16%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    E-Mail";
        seccion += "  </td>";
        seccion += "  <td width=\"16%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Dirección Envío";
        seccion += "  </td>";
        seccion += "  <td width=\"13%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Telefono Envío";
        seccion += "  </td>";
        seccion += "  <td width=\"30%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Condiciones";
        seccion += "  </td>";
        seccion += "</tr>";

        //ok
        seccion += CargarCondicionesEnvioSeleccion(ID_EMPRESA, _datos);

        seccion += "</table>";




        seccion += "<br>";

        //ok
        seccion += CargarAdicionales(ID_EMPRESA, ManualServicio.ListaSecciones.Seleccion, contadorTitulo, contadorSubSecciones, _datos);

        
        
        seccion += "</div>";
        
        return seccion;
    }

    /// <summary>
    /// hecho por cear pulido 
    /// el dia 17 de enro de 2013
    /// para inicializar la tabla con estructura apropiada para condiciones de contratacion
    /// </summary>
    /// <returns></returns>
    private DataTable ConfigurarTablaPerfilesCondicionesContratacion()
    {
        DataTable tablaTemp = new DataTable();

        tablaTemp.Columns.Add("REGISTRO");
        tablaTemp.Columns.Add("PERFIL");
        tablaTemp.Columns.Add("NOM_SUB_C");
        tablaTemp.Columns.Add("NOM_CC");
        tablaTemp.Columns.Add("NOMBRE_CIUDAD");
        tablaTemp.Columns.Add("PORCENTAJE_RIESGO");
        tablaTemp.Columns.Add("DOC_TRAB");
        tablaTemp.Columns.Add("OBS_CTE");

        return tablaTemp;
    }

    private String CargarCondicionesContratacion(Decimal ID_EMPRESA, Conexion _datos)
    {
        DataTable tablaPerfilesCondiciones = ConfigurarTablaPerfilesCondicionesContratacion();

        //capturamos los perfils de la empresa
        perfil _perfil = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        //ok -----------------------
        DataTable tablaPerfiles = _perfil.ObtenerVenDPerfilesConOcupacionPorIdEmpresa(ID_EMPRESA, _datos);

        String htmlSeccion = String.Empty;

        //recorremos cada uno de los perfiles de la tabla
        foreach (DataRow filaPerfil in tablaPerfiles.Rows)
        {
            Decimal ID_PERFIL = Convert.ToDecimal(filaPerfil["REGISTRO"]);
            String PERFIL = filaPerfil["NOM_OCUPACION"].ToString().Trim() + " (Entre " + filaPerfil["EDAD_MIN"].ToString().Trim() + " y " + filaPerfil["EDAD_MAX"].ToString().Trim() + ").";

            //CAPTURAMOS LA INFORMACION DE CONDICIONES DE CONTRATACION ASOCIADAS AL PERFIL SELECCIONADO
            condicionesContratacion _condicionesContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            // ok ------------
            DataTable tablaCondicionContratacion = _condicionesContratacion.ObtenerCondicionContratacionPorIdPerfil(ID_PERFIL, _datos);

            foreach (DataRow filaCondicion in tablaCondicionContratacion.Rows)
            {
                DataRow filaPerfilCondicion = tablaPerfilesCondiciones.NewRow();

                filaPerfilCondicion["REGISTRO"] = ID_PERFIL;
                filaPerfilCondicion["PERFIL"] = PERFIL;

                if (filaCondicion["ID_CIUDAD"] != DBNull.Value)
                {
                    filaPerfilCondicion["NOM_SUB_C"] = "NO APLICA";
                    filaPerfilCondicion["NOM_CC"] = "NO APLICA";

                    ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
                    //ok -------------------------
                    DataTable tablaCiudad = _ciudad.ObtenerCiudadPorIdCiudad(filaCondicion["ID_CIUDAD"].ToString(), _datos);
                    DataRow filaCiudad = tablaCiudad.Rows[0];

                    filaPerfilCondicion["NOMBRE_CIUDAD"] = filaCiudad["NOMBRE_CIUDAD"];
                }
                else
                {
                    if (filaCondicion["ID_CENTRO_C"] != DBNull.Value)
                    {
                        filaPerfilCondicion["NOM_SUB_C"] = "NO APLICA";

                        centroCosto _centroCosto = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                        //ok ----------------------
                        DataTable tablaCC = _centroCosto.ObtenerCentrosDeCostoPorIdCentroCosto(Convert.ToDecimal(filaCondicion["ID_CENTRO_C"]), _datos);
                        DataRow filaCC = tablaCC.Rows[0];

                        filaPerfilCondicion["NOM_CC"] = filaCC["NOM_CC"];

                        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
                        // ok --------------------
                        DataTable tablaCiudad = _ciudad.ObtenerCiudadPorIdCiudad(filaCC["ID_CIUDAD"].ToString(), _datos);
                        DataRow filaCiudad = tablaCiudad.Rows[0];

                        filaPerfilCondicion["NOMBRE_CIUDAD"] = filaCiudad["NOMBRE_CIUDAD"];
                    }
                    else
                    {
                        if (filaCondicion["ID_SUB_C"] != DBNull.Value)
                        {
                            subCentroCosto _subCentroCosto = new subCentroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                            //ok ----------------
                            DataTable tablaSubC = _subCentroCosto.ObtenerSubCentrosDeCostoPorIdSubCosto(Convert.ToDecimal(filaCondicion["ID_SUB_C"]), _datos);
                            DataRow filaSubC = tablaSubC.Rows[0];

                            filaPerfilCondicion["NOM_SUB_C"] = filaSubC["NOM_SUB_C"];

                            centroCosto _centroCosto = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                            // ok -------------
                            DataTable tablaCC = _centroCosto.ObtenerCentrosDeCostoPorIdCentroCosto(Convert.ToDecimal(filaSubC["ID_CENTRO_C"]), _datos);
                            DataRow filaCC = tablaCC.Rows[0];

                            filaPerfilCondicion["NOM_CC"] = filaCC["NOM_CC"];

                            ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
                            //ok --------------
                            DataTable tablaCiudad = _ciudad.ObtenerCiudadPorIdCiudad(filaCC["ID_CIUDAD"].ToString(), _datos);
                            DataRow filaCiudad = tablaCiudad.Rows[0];

                            filaPerfilCondicion["NOMBRE_CIUDAD"] = filaCiudad["NOMBRE_CIUDAD"];
                        }
                    }
                }

                filaPerfilCondicion["PORCENTAJE_RIESGO"] = filaCondicion["PORCENTAJE_RIESGO"].ToString().Trim() + "%";
                filaPerfilCondicion["DOC_TRAB"] = filaCondicion["DOC_TRAB"].ToString().Trim();
                filaPerfilCondicion["OBS_CTE"] = filaCondicion["OBS_CTE"].ToString().Trim();

                //asignamos la fila a la tabla final
                tablaPerfilesCondiciones.Rows.Add(filaPerfilCondicion);
                tablaPerfilesCondiciones.AcceptChanges();
            }
        }

        //tablaTemp.Columns.Add("REGISTRO");
        //tablaTemp.Columns.Add("PERFIL");
        //tablaTemp.Columns.Add("NOM_SUB_C");
        //tablaTemp.Columns.Add("NOM_CC");
        //tablaTemp.Columns.Add("NOMBRE_CIUDAD");
        //tablaTemp.Columns.Add("PORCENTAJE_RIESGO");
        //tablaTemp.Columns.Add("DOC_TRAB");
        //tablaTemp.Columns.Add("OBS_CTE");

        foreach (DataRow fila in tablaPerfilesCondiciones.Rows)
        {
            htmlSeccion += "<tr>";
            htmlSeccion += "  <td width=\"16%\" style=\"text-align:left;\">";
            htmlSeccion += fila["PERFIL"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"12%\" style=\"text-align:center;\">";
            htmlSeccion += fila["NOM_SUB_C"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"12%\" style=\"text-align:center;\">";
            htmlSeccion += fila["NOM_CC"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"12%\" style=\"text-align:center;\">";
            htmlSeccion += fila["NOMBRE_CIUDAD"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"7%\" style=\"text-align:center;\">";
            htmlSeccion += fila["PORCENTAJE_RIESGO"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"20%\" style=\"text-align:justify;\">";
            htmlSeccion += fila["DOC_TRAB"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"21%\" style=\"text-align:justify;\">";
            htmlSeccion += fila["OBS_CTE"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "</tr>";
        }

        return htmlSeccion;
    }

    /// <summary>
    /// hecho por cesar pulido
    /// el dia 17 de enero de 2013
    /// para configurar una tabla apropiada para perfiles y examenes aplicados
    /// </summary>
    /// <returns></returns>
    private DataTable ConfigurarTablaPerfilesExamenes()
    {
        DataTable tablaTemp = new DataTable();

        tablaTemp.Columns.Add("REGISTRO");
        tablaTemp.Columns.Add("PERFIL");
        tablaTemp.Columns.Add("NOM_SUB_C");
        tablaTemp.Columns.Add("NOM_CC");
        tablaTemp.Columns.Add("NOMBRE_CIUDAD");
        tablaTemp.Columns.Add("EXAMENES_MEDICOS_REQUERIDOS");

        return tablaTemp;
    }

    /// <summary>
    /// hecho por cesar puliod
    /// el dia 17 de enero de 2013
    /// para cargar las variables globales de sub centro centro o ciudad acorde a la condicion de contratacion seleccionada
    /// </summary>
    /// <param name="filaCondicion"></param>
    private void CargarVariablesCiudadCentroYSubCentro(DataRow filaCondicion)
    {
        if ((DBNull.Value.Equals(filaCondicion["ID_SUB_C"]) == false) && (filaCondicion["ID_SUB_C"].ToString().Trim() != "0"))
        {
            GLO_ID_SUB_C = Convert.ToDecimal(filaCondicion["ID_SUB_C"]);
            GLO_ID_CENTRO_C = 0;
            GLO_ID_CIUDAD = null;
        }
        else
        {
            if ((DBNull.Value.Equals(filaCondicion["ID_CENTRO_C"]) == false) && (filaCondicion["ID_CENTRO_C"].ToString().Trim() != "0"))
            {
                GLO_ID_SUB_C = 0;
                GLO_ID_CENTRO_C = Convert.ToDecimal(filaCondicion["ID_CENTRO_C"]);
                GLO_ID_CIUDAD = null;
            }
            else
            {
                GLO_ID_SUB_C = 0;
                GLO_ID_CENTRO_C = 0;
                GLO_ID_CIUDAD = filaCondicion["ID_CIUDAD"].ToString().Trim();
            }
        }
    }

    /// <summary>
    /// hecho por cesar pulido
    /// el dia 17 de enero de 2013
    /// para cargar la seccion de examenes medicos por condicion de contratacion
    /// </summary>
    /// <param name="ID_EMPRESA"></param>
    /// <returns></returns>
    private String CargarExamenesMedicosContratacion(Decimal ID_EMPRESA, Conexion _datos)
    {
        DataTable tablaPerfilesExamenes = ConfigurarTablaPerfilesExamenes();

        //capturamos los perfils de la empresa
        perfil _perfil = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        //ok -------------
        DataTable tablaPerfiles = _perfil.ObtenerVenDPerfilesConOcupacionPorIdEmpresa(ID_EMPRESA, _datos);

        String htmlSeccion = String.Empty;

        //recorremos cada uno de los perfiles de la tabla
        foreach (DataRow filaPerfil in tablaPerfiles.Rows)
        {
            Decimal ID_PERFIL = Convert.ToDecimal(filaPerfil["REGISTRO"]);
            String PERFIL = filaPerfil["NOM_OCUPACION"].ToString().Trim() + " (Entre " + filaPerfil["EDAD_MIN"].ToString().Trim() + " y " + filaPerfil["EDAD_MAX"].ToString().Trim() + ").";

            //CAPTURAMOS LA INFORMACION DE CONDICIONES DE CONTRATACION ASOCIADAS AL PERFIL SELECCIONADO
            condicionesContratacion _condicionesContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            //ok ---------------
            DataTable tablaCondicionContratacion = _condicionesContratacion.ObtenerCondicionContratacionPorIdPerfil(ID_PERFIL, _datos);

            foreach (DataRow filaCondicion in tablaCondicionContratacion.Rows)
            {
                //ok
                CargarVariablesCiudadCentroYSubCentro(filaCondicion);

                DataTable tablaInfoExamenesParametrizados;
                if (GLO_ID_SUB_C != 0)
                {
                    //ok --------------------
                    tablaInfoExamenesParametrizados = _condicionesContratacion.ObtenerExamenesParametrizadosParaSubC(ID_PERFIL, GLO_ID_SUB_C, 0, _datos);
                }
                else
                {
                    if (GLO_ID_CENTRO_C != 0)
                    {
                        //ok -----------
                        tablaInfoExamenesParametrizados = _condicionesContratacion.ObtenerExamenesParametrizadosParaCentroC(ID_PERFIL, GLO_ID_CENTRO_C, 0, _datos);
                    }
                    else
                    {
                        //ok --------------
                        tablaInfoExamenesParametrizados = _condicionesContratacion.ObtenerExamenesParametrizadosParaCiudad(ID_PERFIL, GLO_ID_CIUDAD, 0, _datos);
                    }
                }

                String EXAMENES_MEDICOS_REQUERIDOS = String.Empty;

                foreach (DataRow filaInfoExamen in tablaInfoExamenesParametrizados.Rows)
                {
                    if (String.IsNullOrEmpty(EXAMENES_MEDICOS_REQUERIDOS) == true)
                    {
                        EXAMENES_MEDICOS_REQUERIDOS = "<b>" + filaInfoExamen["NOMBRE"].ToString().Trim() + "</b> - Facturar a: " + filaInfoExamen["FACTURADO_A"].ToString().Trim();
                    }
                    else
                    {
                        EXAMENES_MEDICOS_REQUERIDOS += "<br><b>" + filaInfoExamen["NOMBRE"].ToString().Trim() + "</b> - Facturar a: " + filaInfoExamen["FACTURADO_A"].ToString().Trim();
                    }
                }

                DataRow filaPerfilExamen = tablaPerfilesExamenes.NewRow();

                filaPerfilExamen["REGISTRO"] = ID_PERFIL;
                filaPerfilExamen["PERFIL"] = PERFIL;

                if (filaCondicion["ID_CIUDAD"] != DBNull.Value)
                {
                    filaPerfilExamen["NOM_SUB_C"] = "NO APLICA";
                    filaPerfilExamen["NOM_CC"] = "NO APLICA";

                    ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
                    //ok ---------
                    DataTable tablaCiudad = _ciudad.ObtenerCiudadPorIdCiudad(filaCondicion["ID_CIUDAD"].ToString(), _datos);
                    DataRow filaCiudad = tablaCiudad.Rows[0];

                    filaPerfilExamen["NOMBRE_CIUDAD"] = filaCiudad["NOMBRE_CIUDAD"];
                }
                else
                {
                    if (filaCondicion["ID_CENTRO_C"] != DBNull.Value)
                    {
                        filaPerfilExamen["NOM_SUB_C"] = "NO APLICA";

                        centroCosto _centroCosto = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                        // ok -----------
                        DataTable tablaCC = _centroCosto.ObtenerCentrosDeCostoPorIdCentroCosto(Convert.ToDecimal(filaCondicion["ID_CENTRO_C"]), _datos);
                        DataRow filaCC = tablaCC.Rows[0];

                        filaPerfilExamen["NOM_CC"] = filaCC["NOM_CC"];

                        ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
                        // ok ...................
                        DataTable tablaCiudad = _ciudad.ObtenerCiudadPorIdCiudad(filaCC["ID_CIUDAD"].ToString(), _datos);
                        DataRow filaCiudad = tablaCiudad.Rows[0];

                        filaPerfilExamen["NOMBRE_CIUDAD"] = filaCiudad["NOMBRE_CIUDAD"];
                    }
                    else
                    {
                        if (filaCondicion["ID_SUB_C"] != DBNull.Value)
                        {
                            subCentroCosto _subCentroCosto = new subCentroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                            //ok -------------
                            DataTable tablaSubC = _subCentroCosto.ObtenerSubCentrosDeCostoPorIdSubCosto(Convert.ToDecimal(filaCondicion["ID_SUB_C"]), _datos);
                            DataRow filaSubC = tablaSubC.Rows[0];

                            filaPerfilExamen["NOM_SUB_C"] = filaSubC["NOM_SUB_C"];

                            centroCosto _centroCosto = new centroCosto(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
                            //ok ---------
                            DataTable tablaCC = _centroCosto.ObtenerCentrosDeCostoPorIdCentroCosto(Convert.ToDecimal(filaSubC["ID_CENTRO_C"]), _datos);
                            DataRow filaCC = tablaCC.Rows[0];

                            filaPerfilExamen["NOM_CC"] = filaCC["NOM_CC"];

                            ciudad _ciudad = new ciudad(Session["idEmpresa"].ToString());
                            //ok -------
                            DataTable tablaCiudad = _ciudad.ObtenerCiudadPorIdCiudad(filaCC["ID_CIUDAD"].ToString(), _datos);
                            DataRow filaCiudad = tablaCiudad.Rows[0];

                            filaPerfilExamen["NOMBRE_CIUDAD"] = filaCiudad["NOMBRE_CIUDAD"];
                        }
                    }
                }

                if (String.IsNullOrEmpty(EXAMENES_MEDICOS_REQUERIDOS) == true)
                {
                    filaPerfilExamen["EXAMENES_MEDICOS_REQUERIDOS"] = "Ninguno";
                }
                else
                {
                    filaPerfilExamen["EXAMENES_MEDICOS_REQUERIDOS"] = EXAMENES_MEDICOS_REQUERIDOS;
                }

                tablaPerfilesExamenes.Rows.Add(filaPerfilExamen);
                tablaPerfilesExamenes.AcceptChanges();
            }
        }

        //tablaTemp.Columns.Add("REGISTRO");
        //tablaTemp.Columns.Add("PERFIL");
        //tablaTemp.Columns.Add("NOM_SUB_C");
        //tablaTemp.Columns.Add("NOM_CC");
        //tablaTemp.Columns.Add("NOMBRE_CIUDAD");
        //tablaTemp.Columns.Add("EXAMENES_MEDICOS_REQUERIDOS");

        foreach (DataRow fila in tablaPerfilesExamenes.Rows)
        {
            htmlSeccion += "<tr>";
            htmlSeccion += "  <td width=\"23%\" style=\"text-align:left;\">";
            htmlSeccion += fila["PERFIL"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"13%\" style=\"text-align:center;\">";
            htmlSeccion += fila["NOM_SUB_C"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"13%\" style=\"text-align:center;\">";
            htmlSeccion += fila["NOM_CC"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"13%\" style=\"text-align:center;\">";
            htmlSeccion += fila["NOMBRE_CIUDAD"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"38%\" style=\"text-align:justify;\">";
            htmlSeccion += fila["EXAMENES_MEDICOS_REQUERIDOS"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "</tr>";
        }

        return htmlSeccion;
    }
    
    /// <summary>
    /// hecho por cesar pulido
    /// el dia 17 de nero de 2013
    /// para configurar una tabla acorde a las clausulas adicionales del contrato
    /// </summary>
    /// <returns></returns>
    private DataTable ConfigurarTablaPerfilesClausulas()
    {
        DataTable tablaTemp = new DataTable();

        tablaTemp.Columns.Add("REGISTRO");
        tablaTemp.Columns.Add("PERFIL");
        tablaTemp.Columns.Add("CLAUSULAS_ADICIONALES");

        return tablaTemp;
    }

    /// <summary>
    /// hecho por cesar pulido
    /// el dia 17 de enero de 2013
    /// para cargar en el pdf las clausulas adicionales al contrato
    /// </summary>
    /// <param name="ID_EMPRESA"></param>
    /// <returns></returns>
    private String CargarClausulasAdicionales(Decimal ID_EMPRESA, Conexion _datos)
    {
        DataTable tablaPerfilesClausulas = ConfigurarTablaPerfilesClausulas();

        //capturamos los perfils de la empresa
        perfil _perfil = new perfil(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        //ok -------------------
        DataTable tablaPerfiles = _perfil.ObtenerVenDPerfilesConOcupacionPorIdEmpresa(ID_EMPRESA, _datos);

        String htmlSeccion = string.Empty;

        //recorremos cada uno de los perfiles de la tabla
        foreach (DataRow filaPerfil in tablaPerfiles.Rows)
        {
            Decimal ID_PERFIL = Convert.ToDecimal(filaPerfil["REGISTRO"]);
            String PERFIL = filaPerfil["NOM_OCUPACION"].ToString().Trim() + " (Entre " + filaPerfil["EDAD_MIN"].ToString().Trim() + " y " + filaPerfil["EDAD_MAX"].ToString().Trim() + ").";

            condicionesContratacion _condicionesContratacion = new condicionesContratacion(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            //ok ----------------
            DataTable tablaClausulas = _condicionesContratacion.obtenerClausulasPorPerfil(ID_PERFIL, _datos);

            if (tablaClausulas.Rows.Count > 0)
            {
                String CLAUSULAS = String.Empty;
                foreach (DataRow filaClausula in tablaClausulas.Rows)
                {
                    if (String.IsNullOrEmpty(CLAUSULAS) == true)
                    {
                        CLAUSULAS = filaClausula["NOMBRE"].ToString().Trim();
                    }
                    else
                    {
                        CLAUSULAS += "<br>" + filaClausula["NOMBRE"].ToString().Trim();
                    }
                }

                DataRow filaPerfilClausula = tablaPerfilesClausulas.NewRow();

                filaPerfilClausula["REGISTRO"] = ID_PERFIL;
                filaPerfilClausula["PERFIL"] = PERFIL;
                if (String.IsNullOrEmpty(CLAUSULAS) == true)
                {
                    filaPerfilClausula["CLAUSULAS_ADICIONALES"] = "Ninguna.";
                }
                else
                {
                    filaPerfilClausula["CLAUSULAS_ADICIONALES"] = CLAUSULAS;
                }

                tablaPerfilesClausulas.Rows.Add(filaPerfilClausula);
                tablaPerfilesClausulas.AcceptChanges();
            }
        }

        //tablaTemp.Columns.Add("REGISTRO");
        //tablaTemp.Columns.Add("PERFIL");
        //tablaTemp.Columns.Add("CLAUSULAS_ADICIONALES");

        foreach (DataRow fila in tablaPerfilesClausulas.Rows)
        {
            htmlSeccion += "<tr>";
            htmlSeccion += "  <td width=\"30%\" style=\"text-align:left;\">";
            htmlSeccion += fila["PERFIL"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "  <td width=\"70%\" style=\"text-align:justify;\">";
            htmlSeccion += fila["CLAUSULAS_ADICIONALES"].ToString().Trim();
            htmlSeccion += "  </td>";
            htmlSeccion += "</tr>";
        }

        return htmlSeccion;
    }

    /// <summary>
    /// hecho por cesar pulio 
    /// el dia 17 de  enro de 2013
    /// para obtener la lista de docuemntos que deben ser enviados al cliente en seleccion
    /// </summary>
    /// <param name="documentosSeleccion"></param>
    /// <param name="filaInfoEnvioDocumentos"></param>
    /// <returns></returns>
    private String ObtenerDocumentosSeleccion(String documentosSeleccion, DataRow filaInfoEnvioDocumentos)
    {
        String mensaje_seleccion = "<div style=\"text-align:justify;\">";
        mensaje_seleccion += "Enviar al E-Mail <b>[CORREO_SELECCION]</b> ([NOMBRE_CONTATCTO_SELECCION] - [CARGO_CONTATCTO_SELECCION]):";
        mensaje_seleccion += "[LISTA_DOCUMENTOS_SELECCION]";
        mensaje_seleccion += "</div>";
        mensaje_seleccion += "<br>";

        mensaje_seleccion = mensaje_seleccion.Replace("[CORREO_SELECCION]", filaInfoEnvioDocumentos["CONT_MAIL_SELECCION"].ToString().Trim());
        mensaje_seleccion = mensaje_seleccion.Replace("[NOMBRE_CONTATCTO_SELECCION]", filaInfoEnvioDocumentos["CONT_NOM_SELECCION"].ToString().Trim());
        mensaje_seleccion = mensaje_seleccion.Replace("[CARGO_CONTATCTO_SELECCION]", filaInfoEnvioDocumentos["CONT_CARGO_SELECCION"].ToString().Trim());

        documentosSeleccion = documentosSeleccion.Replace("INFORME_SELECCION", "Informe Selección");
        documentosSeleccion = documentosSeleccion.Replace("ARCHIVOS_PRUEBAS", "Archivos Pruebas");
        documentosSeleccion = documentosSeleccion.Replace("REFERENCIA_LABORAL", "Confirmación Referencia Laboral");

        String listaDocumentosSeleccion = "<ul>";

        foreach (String documento in documentosSeleccion.Split(';'))
        {
            listaDocumentosSeleccion += "<li>" + documento + "</li>";
        }
        listaDocumentosSeleccion += "</ul>";

        mensaje_seleccion = mensaje_seleccion.Replace("[LISTA_DOCUMENTOS_SELECCION]", listaDocumentosSeleccion);

        return mensaje_seleccion;
    }

    /// <summary>
    /// hecho por cesar pulio 
    /// el dia 17 de  enro de 2013
    /// para obtener la lista de docuemntos que deben ser enviados al cliente en contraacion
    /// </summary>
    /// <param name="documentosContratacion"></param>
    /// <param name="filaInfoEnvioDocumentos"></param>
    /// <returns></returns>
    private String ObtenerDocumentosContratacion(String documentosContratacion, DataRow filaInfoEnvioDocumentos)
    {
        String mensaje_contratacion = "<div style=\"text-align:justify;\">";
        mensaje_contratacion += "Enviar al E-Mail <b>[CORREO_CONTRATACION]</b> ([NOMBRE_CONTATCTO_CONTRATACION] - [CARGO_CONTATCTO_CONTRATACION]):";
        mensaje_contratacion += "[LISTA_DOCUMENTOS_CONTRATACION]";
        mensaje_contratacion += "</div>";

        mensaje_contratacion = mensaje_contratacion.Replace("[CORREO_CONTRATACION]", filaInfoEnvioDocumentos["CONT_MAIL_CONTRATACION"].ToString().Trim());
        mensaje_contratacion = mensaje_contratacion.Replace("[NOMBRE_CONTATCTO_CONTRATACION]", filaInfoEnvioDocumentos["CONT_NOM_CONTRATACION"].ToString().Trim());
        mensaje_contratacion = mensaje_contratacion.Replace("[CARGO_CONTATCTO_CONTRATACION]", filaInfoEnvioDocumentos["CONT_CARGO_CONTRATACION"].ToString().Trim());

        documentosContratacion = documentosContratacion.Replace("ARCHIVOS_EXAMENES", "Examenes Medicos -Resultados-");
        documentosContratacion = documentosContratacion.Replace("EXAMENES", "Examenes Medicos -Autos Recomendación-");
        documentosContratacion = documentosContratacion.Replace("CONTRATO", "Contrato Laboral");
        documentosContratacion = documentosContratacion.Replace("CLAUSULAS", "Clausulas Adicionales");
        documentosContratacion = documentosContratacion.Replace("ARCHIVOS_AFILIACIONES", "Afiliaciones");

        String listaDocumentosContratacion = "<ul>";

        foreach (String documento in documentosContratacion.Split(';'))
        {
            listaDocumentosContratacion += "<li>" + documento + "</li>";
        }
        listaDocumentosContratacion += "</ul>";

        mensaje_contratacion = mensaje_contratacion.Replace("[LISTA_DOCUMENTOS_CONTRATACION]", listaDocumentosContratacion);

        return mensaje_contratacion;
    }

    /// <summary>
    /// hecho por cesar pulido
    /// el dia 17 de enero de 2013
    /// para cargar en el pdf el envio de docuemntos al cliente
    /// </summary>
    /// <param name="ID_EMPRESA"></param>
    /// <returns></returns>
    private String CargarEnvioDocumentosContratacion(Decimal ID_EMPRESA, Conexion _datos)
    {
        ConfDocEntregable _confDocEntregable = new ConfDocEntregable(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
        //ok ---------------
        DataTable tablaConf = _confDocEntregable.ObtenerPorEmpresa(ID_EMPRESA, _datos);

        if (tablaConf.Rows.Count <= 0)
        {
            return "<b>No se ha realizado la configuración de envío de documentos al cliente.</b>";
        }
        else
        {
            DataRow filaConf = tablaConf.Rows[0];
            if (filaConf["ENTREGA_DOCUMENTOS"].ToString().Trim().ToUpper() == "FALSE")
            {
                return  "<b>El cliente no requiere envío de documentación de los trabajadores.</b>";
            }
            else
            {
                String mensaje = "<div style=\"text-align:justify;\">";
                mensaje += "El cliente requiere el envío de la siguiente documentación vía E-Mail:";
                mensaje += "</div>";
                mensaje += "<br>";
                mensaje += "[DOCUMENTOS_SELECCION]";
                mensaje += "[DOCUMENTOS_CONTRATACION]";

                if (DBNull.Value.Equals(filaConf["DOCUMENTOS_SELECCION"]) == true)
                {
                    mensaje = mensaje.Replace("[DOCUMENTOS_SELECCION]", "");
                }
                else
                {
                    mensaje = mensaje.Replace("[DOCUMENTOS_SELECCION]", ObtenerDocumentosSeleccion(filaConf["DOCUMENTOS_SELECCION"].ToString().Trim(), filaConf));
                }


                if (DBNull.Value.Equals(filaConf["DOCUMENTOS_CONTRATACION"]) == true)
                {
                    mensaje = mensaje.Replace("[DOCUMENTOS_CONTRATACION]", "");
                }
                else
                {
                    mensaje = mensaje.Replace("[DOCUMENTOS_CONTRATACION]", ObtenerDocumentosContratacion(filaConf["DOCUMENTOS_CONTRATACION"].ToString().Trim(), filaConf));
                }

                return mensaje;
            }
        }
    }

    /// <summary>
    /// hecho por cesar pulido
    /// el dia 17 d e enero de 2013
    /// para obtener los datos de la seccion de contratacion y incluirla en el pdf
    /// </summary>
    /// <param name="ID_EMPRESA"></param>
    /// <param name="contadorTitulo"></param>
    /// <returns></returns>
    public String ObtenerSeccionContratacion(Decimal ID_EMPRESA, int contadorTitulo, Conexion _datos)
    {
        String tituloSeccion = "CONTRATACIÓN Y RELACIONES LABORALES";
        String seccion = _div_seccion_manual;

        int contadorSubSecciones = 0;

        if (contadorTitulo == 0)
        { //ok 
            seccion += GenerarTituloManualServicio(tituloSeccion);
        }
        else
        {
            //ok 
            seccion += GenerarTituloManualServicio(contadorTitulo.ToString() + ". " + tituloSeccion);
        }

        seccion += "<br>";

        contadorSubSecciones += 1;

        //ok
        seccion += GenerarSubTituloManualServicio(contadorTitulo + "." + contadorSubSecciones + " " + "INFORMACIÓN BÁSICA DE CONDICIONES DE CONTRATACIÓN POR PERFIL");

        seccion += "<br>";

        seccion += "<table width=\"100%\" border=\"1\" cellpadding=\"2\" cellspacing=\"0\">";
        seccion += "<tr bgcolor=\"#dddddd\">";
        seccion += "  <td width=\"16%\" style=\"text-align:center; font-weight:bold;\">";
        seccion += "    Perfil";
        seccion += "  </td>";
        seccion += "  <td width=\"12%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Sub Centro";
        seccion += "  </td>";
        seccion += "  <td width=\"12%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Centro Costo";
        seccion += "  </td>";
        seccion += "  <td width=\"12%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Ciudad";
        seccion += "  </td>";
        seccion += "  <td width=\"7%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Riesgo %";
        seccion += "  </td>";
        seccion += "  <td width=\"20%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Docs. Entregados al Trabajador";
        seccion += "  </td>";
        seccion += "  <td width=\"21%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Requeriminetos CLiente";
        seccion += "  </td>";
        seccion += "</tr>";

        //ok 
        seccion += CargarCondicionesContratacion(ID_EMPRESA, _datos);

        seccion += "</table>";




        seccion += "<br>";

        contadorSubSecciones += 1;
         
        //ok
        seccion += GenerarSubTituloManualServicio(contadorTitulo + "." + contadorSubSecciones + " " + "EXÁMENES MEDICOS DE INGRESO POR PERFIL");

        seccion += "<br>";

        seccion += "<table width=\"100%\" border=\"1\" cellpadding=\"2\" cellspacing=\"0\">";
        seccion += "<tr bgcolor=\"#dddddd\">";
        seccion += "  <td width=\"23%\" style=\"text-align:center; font-weight:bold;\">";
        seccion += "    Perfil";
        seccion += "  </td>";
        seccion += "  <td width=\"13%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Sub Centro";
        seccion += "  </td>";
        seccion += "  <td width=\"13%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Centro Costo";
        seccion += "  </td>";
        seccion += "  <td width=\"13%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Ciudad";
        seccion += "  </td>";
        seccion += "  <td width=\"38%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Exam. Medicos Requeridos";
        seccion += "  </td>";
        seccion += "</tr>";

        //ok 
        seccion += CargarExamenesMedicosContratacion(ID_EMPRESA, _datos);

        seccion += "</table>";




        seccion += "<br>";

        contadorSubSecciones += 1;
        //ok
        seccion += GenerarSubTituloManualServicio(contadorTitulo + "." + contadorSubSecciones + " " + "CLAUSULAS ADICIONALES CONTRATO");

        seccion += "<br>";

        seccion += "<table width=\"100%\" border=\"1\" cellpadding=\"2\" cellspacing=\"0\">";
        seccion += "<tr bgcolor=\"#dddddd\">";
        seccion += "  <td width=\"30%\" style=\"text-align:center; font-weight:bold;\">";
        seccion += "    Perfil";
        seccion += "  </td>";
        seccion += "  <td width=\"70%\" style=\"font-weight:bold; text-align:center;\">";
        seccion += "    Clausulas Adicionales";
        seccion += "  </td>";
        seccion += "</tr>";

        //ok 
        seccion += CargarClausulasAdicionales(ID_EMPRESA, _datos);

        seccion += "</table>";




        seccion += "<br>";

        contadorSubSecciones += 1;
        //ok
        seccion += GenerarSubTituloManualServicio(contadorTitulo + "." + contadorSubSecciones + " " + "ENVÍO DE DOCUMENTOS AL CLIENTE");

        seccion += "<br>";

        seccion += CargarEnvioDocumentosContratacion(ID_EMPRESA, _datos);




        seccion += "<br>";
        //ok
        seccion += CargarAdicionales(ID_EMPRESA, ManualServicio.ListaSecciones.Contratacion, contadorTitulo, contadorSubSecciones, _datos);




        seccion += "</div>";

        return seccion;
    }

    /// <summary>
    /// HECHO POR CESAR PULIDO
    /// EL DIA DE 16 DE ENERO DE 2013
    /// PARA OBTENER UN ARREGLO DE BYTES QUE CONFORMAN EL ARCHIVO DE MANUAL DE SERVICIO DE UNA EMPRESA
    /// </summary>
    /// <param name="ID_VERSIONAMIENTO"></param>
    /// <param name="ID_EMPRESA"></param>
    /// <param name="_datos"></param>
    /// <returns></returns>
    public byte[] GenerarPDFManualServicio(Decimal ID_VERSIONAMIENTO, String versionManual, DateTime fechaEmision, DateTime fechaAPartirDe, Decimal ID_EMPRESA, Conexion _datos)
    {
        //vrificada por _datos

        int contadorSeccion = 0;
        String html_completo = String.Empty;

        contadorSeccion += 1;
        //ok---------------
        String seccionIdentificacionCliente = ObtenerSeccionIdentificacionCliente(ID_EMPRESA, contadorSeccion, _datos);
        html_completo += seccionIdentificacionCliente;

        html_completo += "<br>";

        //ok
        String seccionControlModificaciones = ObtenerSeccionControlModificaciones(ID_EMPRESA, 0, _datos);
        html_completo += seccionControlModificaciones;

        html_completo += "<br>";

        contadorSeccion += 1;
        //ok
        String seccionGestionComercial = ObtenerSeccionGestionComercial(ID_EMPRESA, contadorSeccion, _datos);
        html_completo += seccionGestionComercial;

        html_completo += "<br>";

        contadorSeccion += 1;
        String seccionProcesoSeleccion = ObtenerSeccionProcesoSeleccion(ID_EMPRESA, contadorSeccion, _datos);
        html_completo += seccionProcesoSeleccion;

        html_completo += "<br>";

        contadorSeccion += 1;
        String seccionContratacion = ObtenerSeccionContratacion(ID_EMPRESA, contadorSeccion, _datos);
        html_completo += seccionContratacion;

        //creamos un configuramos el documento de pdf
        //(tamaño de la hoja,margen izq, margen der, margin arriba margen abajo)
        iTextSharp.text.Document document = new iTextSharp.text.Document(new Rectangle(595, 842), 40, 40, 110, 40);

        using (MemoryStream streamArchivo = new MemoryStream())
        {
            iTextSharp.text.pdf.PdfWriter writer = PdfWriter.GetInstance(document, streamArchivo);

            // Our custom Header and Footer is done using Event Handler
            pdfEvents PageEventHandler = new pdfEvents();
            writer.PageEvent = PageEventHandler;

            // Define the page header
            // Define the page header
            


            // ojojojojojojo
            PageEventHandler.dirImagenHeader = Server.MapPath("~/imagenes/reportes/logo_sertempo.png");
            // ojojojojojojo

            PageEventHandler.fechaApartirDe = fechaAPartirDe;
            PageEventHandler.versionManual = versionManual;
            PageEventHandler.fechaImpresion = DateTime.Now;
            PageEventHandler.tipoDocumento = "manualServicio";

            document.Open();

            //capturamos el archivo temporal del response
            String tempFile = Path.GetTempFileName();

            //y lo llenamos con el html de la plantilla
            using (StreamWriter tempwriter = new StreamWriter(tempFile, false))
            {
                tempwriter.Write(html_completo);
            }

            //leeemos el archivo temporal y lo colocamos en el documento de pdf
            List<IElement> htmlarraylist = HTMLWorker.ParseToList(new StreamReader(tempFile), new StyleSheet());

            foreach (IElement element in htmlarraylist)
            {
                if (element.Chunks.Count > 0)
                {
                    if (element.Chunks[0].Content == "linea para paginacion de pdf")
                    {
                        document.NewPage();
                    }
                    else
                    {
                        document.Add(element);
                    }
                }
                else
                {
                    document.Add(element);
                }
            }

            //limpiamos todo
            document.Close();

            writer.Close();

            return streamArchivo.ToArray();
        }
    }

    public Boolean CargarEnBdElManualServicioActual(Decimal ID_EMPRESA)
    {
        Boolean resultado = true;
        Boolean correcto = true;

        Conexion _datos = new Conexion(Session["idEmpresa"].ToString().Trim());
        _datos.IniciarTransaccion();

        try
        {
            //capturamos el historial de versionamineto de la empresa
            ManualServicio _manualServicio = new ManualServicio(Session["idEmpresa"].ToString(), Session["USU_LOG"].ToString());
            DataTable tablaManual = _manualServicio.ObtenerVersionamientoManualPorEmpresa(ID_EMPRESA, _datos);

            if (tablaManual.Rows.Count <= 0)
            {
                //si no se ha creado manual se deveulve true
                resultado = true;
                correcto = true;
            }
            else
            {
                DataRow filaVersionamientoManual = tablaManual.Rows[tablaManual.Rows.Count - 1];
                Decimal ID_VERSIONAMIENTO = Convert.ToDecimal(filaVersionamientoManual["ID_VERSIONAMIENTO"].ToString());
                DateTime fechaEmisionManual = Convert.ToDateTime(filaVersionamientoManual["FECHA_EMISION"]); 
                String version = filaVersionamientoManual["VERSION_MAYOR"].ToString().Trim() + "." + filaVersionamientoManual["VERSION_MENOR"].ToString().Trim();
                DateTime fechaApartirDe = Convert.ToDateTime(filaVersionamientoManual["APLICAR_A_PARTIR"]);

                //revisamos si esa version ya tiene manual en pdf generado o sino lo generamos y lo guardamos en bd
                DataTable tablaArchivoVersion = _manualServicio.ObtenerArchivoManualServicioPorVersion(ID_VERSIONAMIENTO, _datos);

                if (tablaArchivoVersion.Rows.Count > 0)
                {
                    //el archivo ya esta guardado
                    resultado = true;
                    correcto = true;
                }
                else
                { 
                    //toca generar el archivo y guardarlo
                    maestrasInterfaz _maestrasInterfaz = new maestrasInterfaz();
                    byte[] archivoManual = _maestrasInterfaz.GenerarPDFManualServicio(ID_VERSIONAMIENTO, version, fechaEmisionManual, fechaApartirDe, ID_EMPRESA, _datos);

                    Decimal ID_DOCUMENTO = Convert.ToDecimal(_datos.ExecuteEscalarParaAdicionarDocsManualServixo(ID_VERSIONAMIENTO, archivoManual,".pdf",archivoManual.Length, "application/pdf", Session["USU_LOG"].ToString()));

                    if (ID_DOCUMENTO <= 0)
                    {
                        resultado = false;
                        correcto = false;
                        _datos.DeshacerTransaccion();
                    }
                    else
                    {
                        resultado = true;
                        correcto = true;
                    }
                }
            }

            if (correcto == true)
            {
                _datos.AceptarTransaccion();
            }

        }
        catch(Exception ex)
        {
            _datos.DeshacerTransaccion();
            resultado = false;
            MensajeError = ex.Message;
        }
        finally
        {
            _datos.Desconectar();
        }

        return resultado;
    }
}