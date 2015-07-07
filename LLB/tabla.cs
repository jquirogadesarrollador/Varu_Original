using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brainsbits.LLB
{
    public static class tabla
    {
        #region variables

        #region tablas

        public const String ALM_DOCUMENTOS_TEMP = "ALM_DOCUMENTOS_TEMP";
        public const String ALM_INVENTARIO_TEMP = "ALM_INVENTARIO_TEMP";

        public const String ALM_EQUIPOS = "ALM_EQUIPOS";
        public const String ALM_EQUIPOS_ENTRADA_TEMP = "ALM_EQUIPOS_ENTRADA_TEMP";


        public const String INV_CIERRE_INVENTARIO = "INV_CIERRE_INVENTARIO";
        public const String INV_DETALLE_CIERRE = "INV_DETALLE_CIERRE";

        public const String ALM_ORDEN_COMPRA = "ALM_ORDEN_COMPRA";
        public const String ALM_NEGOCIACION_PROVEEDOR = "ALM_NEGOCIACION_PROVEEDOR";
        public const String ALM_LOTE = "ALM_LOTE";
        public const String ALM_DOCUMENTOS = "ALM_DOCUMENTOS";
        public const String ALM_DETALLE_ORDEN = "ALM_DETALLE_ORDEN";
        public const String ALM_CRITERIOS_CALIFICACION = "ALM_CRITERIOS_CALIFICACION";
        public const String ALM_CATEGORIA = "ALM_CATEGORIA";
        public const String ALM_CALIFICACION_PROVEEDORES = "ALM_CALIFICACION_PROVEEDORES";
        public const String ALM_DETALLE_CALIFICACION_PROVEEDORES = "ALM_DETALLE_CALIFICACION_PROVEEDORES";

        public const String ALM_REG_PRODUCTO_PROVEEDOR = "ALM_REG_PRODUCTO_PROVEEDOR";
        public const String CON_BANCO_EMPRESA = "CON_BANCO_EMPRESA";
        public const String ALM_REG_PRODUCTO = "ALM_REG_PRODUCTO";
        public const String ALM_REG_BODEGA = "ALM_REG_BODEGA";
        public const String ALM_INVENTARIO = "ALM_INVENTARIO";
        public const String ALM_REG_CATALOGO = "ALM_REG_CATALOGO";
        public const String CON_REG_SERVICIO_PERFIL = "CON_REG_SERVICIO_PERFIL";
        public const String CON_REG_ELEMEMENTOS_TRABAJO = "CON_REG_ELEMEMENTOS_TRABAJO";
        public const String CON_REG_ENTREGAS_SERVICIOS_COMPLEMENTARIOS = "CON_REG_ENTREGAS_SERVICIOS_COMPLEMENTARIOS";
        public const String ALM_REG_PROVEEDOR = "ALM_REG_PROVEEDOR";
        public const String CON_REG_AUTO_RECOMENDACIONES = "CON_REG_AUTO_RECOMENDACIONES";
        public const String CON_REG_EXAMENES_EMPLEADO = "CON_REG_EXAMENES_EMPLEADO";

        public const String ESC_ASIGNACION_SERVICIOS_COMPLEMENTARIOS = "ESC_ASIGNACION_SERVICIOS_COMPLEMENTARIOS";
        public const String ESC_DETALLE_CONTENIDO_ASIGNACION = "ESC_DETALLE_CONTENIDO_ASIGNACION";
        public const String ESC_ASIGNACION_S_C_POR_APROBAR = "ESC_ASIGNACION_S_C_POR_APROBAR";

        public const String ALM_EQUIPOS_ENTREGA = "ALM_EQUIPOS_ENTREGA";

        public const String CON_REG_CONTRATOS_TEMPORAL = "CON_REG_CONTRATOS_TEMPORAL";

        public const String CON_CONFIGURACON_DOCS_ENTREGABLES = "CON_CONFIGURACON_DOCS_ENTREGABLES";

        public const String CON_REG_ORDEN_EXAMEN = "CON_REG_ORDEN_EXAMEN";
        public const String SERTEMPO_2_Recep_Aspiran_dbo_AREAS_INTERES_LABORAL = "SERTEMPO_2_Recep_Aspiran_dbo_AREAS_INTERES_LABORAL";
        public const String SERTEMPO_2_Recep_Aspiran_dbo_REG_ASPIRANTE = "SERTEMPO_2_Recep_Aspiran_dbo_REG_ASPIRANTE";
        public const String CRT_MODULOS = "CRT_MODULOS";
        public const String CRT_APLICACIONES = "CRT_APLICACION";
        public const String CRT_PERMISOS = "CRT_PERMISOS";
        public const String CRT_PERFIL = "CRT_PERFIL";
        public const String CRT_UNIDAD_SOPORTE = "CRT_UNIDAD_SOPORTE";
        public const String CRT_UNIDAD_NEGOCIO = "CRT_UNIDAD_NEGOCIO";
        public const String SEL_REG_DOCUMENTOS_PERFIL = "SEL_REG_DOCUMENTOS_PERFIL";

        public const String SEL_REG_DOCUMENTOS = "SEL_REG_DOCUMENTOS";

        public const String SEL_REG_REFERENCIAS = "SEL_REG_REFERENCIAS";
        public const String SEL_REG_PREGUNTAS_REFERENCIA = "SEL_REG_PREGUNTAS_REFERENCIA";

        public const String SEL_REG_CAT_REF = "SEL_REG_CAT_REF";

        public const String SEL_REG_RESPUESTAS_REFERENCIA = "SEL_REG_RESPUESTAS_REFERENCIA";
        public const String SEL_CONTROL_REQUISITOS = "SEL_CONTROL_REQUISITOS";

        public const String SEL_REG_DICCIONARIO_COMPETENCIAS = "SEL_REG_DICCIONARIO_COMPETENCIAS";
        public const String SEL_DICCIONARIO_HABILIDADES = "SEL_DICCIONARIO_HABILIDADES";
        public const String SEL_HABILIDADES_PERFIL = "SEL_HABILIDADES_PERFIL";

        public const String OPER_VERSIONAMIENTO_MANUAL = "OPER_VERSIONAMIENTO_MANUAL";
        public const String OPER_ADICIONALES_MANUAL = "OPER_ADICIONALES_MANUAL";
        public const String OPER_HIST_MODIFICACIONES_MANUAL = "OPER_HIST_MODIFICACIONES_MANUAL";

        public const String VEN_EMPRESAS = "VEN_EMPRESAS";
        public const String VEN_CONTACTOS = "VEN_CONTACTOS";
        public const String VEN_SECCION = "VEN_SECCION";
        public const String VEN_REG_HV_EMPRESA = "VEN_REG_HV_EMPRESA";
        public const String VEN_GRUPOEMPRESARIAL = "VEN_GRUPOEMPRESARIAL";
        public const String VEN_COBERTURA = "VEN_COBERTURA";
        public const String VEN_DIVISION = "VEN_DIVISION";
        public const String VEN_CLASE = "VEN_CLASE";
        public const String VEN_ACTIVIDAD = "VEN_ACTIVIDAD";
        public const String VEN_SERVICIO_COMPLEMENTARIO = "VEN_SERVICIO_COMPLEMENTARIO";
        public const String VEN_SERVICIO_POR_EMPRESA = "VEN_SERVICIO_POR_EMPRESA";
        public const String VEN_SERVICIO = "VEN_SERVICIO";
        public const String VEN_DETALLE_SERVICIO = "VEN_DETALLE_SERVICIO";

        public const String NOM_INC_CHQ_SUB = "NOM_INC_CHQ_SUB";
        public const String CRT_PROGRAMAS = "CRT_PROGRAMAS";
        public const String VEN_DET_FACTURAS_HST = "VEN_DET_FACTURAS_HST";
        public const String NOM_IBG_ACH = "NOM_IBG_ACH";
        public const String NOM_INTERFACE_CENTRO_C = "NOM_INTERFACE_CENTRO_C";
        //public const String dtproperties = "dtproperties";
        public const String NOM_INTERFASE_CON_NOM = "NOM_INTERFASE_CON_NOM";
        public const String soporte = "soporte";
        public const String rec_ocupaciones_copia_2008 = "rec_ocupaciones_copia_2008";
        public const String VEN_REG_NOTAS_CR = "VEN_REG_NOTAS_CR";
        public const String NOM_INT_NOMINA_FTE_1 = "NOM_INT_NOMINA_FTE_1";
        public const String NOM_INT_NOMINA_FTE_2 = "NOM_INT_NOMINA_FTE_2";
        public const String NOM_INT_NOMINA_FTE_3 = "NOM_INT_NOMINA_FTE_3";
        public const String VEN_SUB_CENTROS_912 = "VEN_SUB_CENTROS_912";
        public const String VEN_REG_NOTAS_DB = "VEN_REG_NOTAS_DB";
        public const String NOM_INT_NOMINA_MST = "NOM_INT_NOMINA_MST";
        public const String NOM_Int_A_Capital_Det = "NOM_Int_A_Capital_Det";
        public const String NOM_Int_A_Capital_Mst = "NOM_Int_A_Capital_Mst";
        public const String HST_NOM_NOMINA_NOVEDADES = "HST_NOM_NOMINA_NOVEDADES";
        public const String NOM_Int_Bancos = "NOM_Int_Bancos";
        public const String NOM_MST_BASE_NOM = "NOM_MST_BASE_NOM";
        public const String NOM_REG_PER_CAR = "NOM_REG_PER_CAR";
        public const String REG_GESTANTES_CASOS_SEVEROS = "REG_GESTANTES_CASOS_SEVEROS";
        public const String NOM_LIQ_CES = "NOM_LIQ_CES";
        public const String HST_NOM_PRE_NOMINA = "HST_NOM_PRE_NOMINA";
        public const String AUDIT_EVENT = "AUDIT_EVENT";
        public const String NOM_LIQ_PRIMA = "NOM_LIQ_PRIMA";
        public const String NOM_PERIODO = "NOM_PERIODO";
        public const String NOM_EMPLEADOS = "NOM_EMPLEADOS";
        public const String INCOMPLETE_EVENT = "INCOMPLETE_EVENT";
        public const String NOM_LIQ_VAC = "NOM_LIQ_VAC";
        public const String productos = "productos";
        public const String CON_REG_CONTRATOS = "CON_REG_CONTRATOS";
        public const String CIUDAD1 = "CIUDAD1";
        public const String Anulacion_Facturas = "Anulacion_Facturas";
        public const String NOM_NOMINA_DEFINITIVA = "NOM_NOMINA_DEFINITIVA";
        public const String NOM_NOMINA_NOVEDADES = "NOM_NOMINA_NOVEDADES";
        public const String NOM_MEMOS_NOVEDADES = "NOM_MEMOS_NOVEDADES";
        public const String NOM_PAGOS_NOM = "NOM_PAGOS_NOM";
        public const String DESC_NOVEDADES = "DESC_NOVEDADES";
        public const String CMS_InfoObjects6 = "CMS_InfoObjects6";
        public const String VEN_SUB_CENTROS_1733 = "VEN_SUB_CENTROS_1733";
        public const String NOM_IBG_ACH_VILLAO = "NOM_IBG_ACH_VILLAO";
        public const String NOM_PAR_GENERALES = "NOM_PAR_GENERALES";
        public const String NOM_IBG_VILLAO = "NOM_IBG_VILLAO";
        public const String NOM_PRE_NOMINA = "NOM_PRE_NOMINA";
        public const String BASE_EMPLE_AUTOLIQ = "BASE_EMPLE_AUTOLIQ";
        public const String NOM_REG_ANT_CES = "NOM_REG_ANT_CES";
        public const String REGIONAL = "REGIONAL";
        public const String CMS_RELATIONS6 = "CMS_RELATIONS6";
        public const String NOM_REG_LICENCIAS = "NOM_REG_LICENCIAS";
        public const String CMS_LOCKS6 = "CMS_LOCKS6";
        public const String NOM_REG_TRASLADOS = "NOM_REG_TRASLADOS";
        public const String NOM_REG_VACACIONES = "NOM_REG_VACACIONES";
        public const String PLAN_CUENTAS = "PLAN_CUENTAS";
        public const String REC_COM_FUENTES = "REC_COM_FUENTES";
        public const String REC_EVA_FUENTES = "REC_EVA_FUENTES";
        public const String REC_FUENTES = "REC_FUENTES";
        public const String REC_FUNCIONARIOS = "REC_FUNCIONARIOS";
        public const String REC_OCUPACIONES = "REC_OCUPACIONES";
        public const String REC_OCUPACIONES_FUENTE = "REC_OCUPACIONES_FUENTE";
        public const String DF_BANCOS = "DF_BANCOS";
        public const String CRUCE1 = "CRUCE1";
        public const String REG_ACOSET = "REG_ACOSET";
        public const String VEN_D_NOMINA = "VEN_D_NOMINA";
        public const String REG_ACTA_DESC = "REG_ACTA_DESC";
        public const String REG_EXAMENES = "REG_EXAMENES";
        public const String MEDIOS_TRANSPORTE = "MEDIOS_TRANSPORTE";
        public const String REG_HIST_ACADEMICA = "REG_HIST_ACADEMICA";
        public const String REG_HIST_FAMILIAR = "REG_HIST_FAMILIAR";
        public const String NOM_REG_DESC_NOMINA = "NOM_REG_DESC_NOMINA";
        public const String REG_HIST_LABORAL = "REG_HIST_LABORAL";
        public const String REG_LLAMADOS_ATENCION = "REG_LLAMADOS_ATENCION";
        public const String REG_REGISTROS_HOJA_VIDA = "REG_REGISTROS_HOJA_VIDA";
        public const String REG_SANSIONES = "REG_SANSIONES";
        public const String TMPALARMAS = "TMPALARMAS";
        public const String REG_SOLICITUDES_INGRESO = "REG_SOLICITUDES_INGRESO";

        public const String SEL_REG_DATOS_HIJOS = "SEL_REG_DATOS_HIJOS";

        public const String SEL_CAT_PRUEBAS = "SEL_CAT_PRUEBAS";
        public const String VEN_ENT_FINANCIERAS = "VEN_ENT_FINANCIERAS";
        public const String SEL_ENTREVISTA_RETIRO = "SEL_ENTREVISTA_RETIRO";
        public const String SEL_EVAL_DESEMPENO = "SEL_EVAL_DESEMPENO";
        public const String REP_REPORTES = "REP_REPORTES";
        public const String SEL_FUNC_SEGURIDAD = "SEL_FUNC_SEGURIDAD";
        public const String SEL_PRUEBAS = "SEL_PRUEBAS";
        public const String REP_DERECHOS = "REP_DERECHOS";
        public const String SEL_PRUEBAS_OCUPACION = "SEL_PRUEBAS_OCUPACION";
        public const String SEL_REG_APLICACION_PRUEBAS = "SEL_REG_APLICACION_PRUEBAS";
        public const String automatizacion_parametros = "automatizacion_parametros";
        public const String NOM_REG_RETIROS_TMP = "NOM_REG_RETIROS_TMP";
        public const String REP_ESTADISTICA = "REP_ESTADISTICA";
        public const String SEL_REG_ENTREVISTAS = "SEL_REG_ENTREVISTAS";
        public const String SEL_REG_ESTUDIO_SEGURIDAD = "SEL_REG_ESTUDIO_SEGURIDAD";
        public const String SEL_REG_VISITAS_DOMICILIARIAS = "SEL_REG_VISITAS_DOMICILIARIAS";
        public const String SEL_SICOLOGOS = "SEL_SICOLOGOS";
        public const String CTB_ING = "CTB_ING";
        public const String VEN_CIUDADES = "VEN_CIUDADES";
        public const String SEL_TRABAJADORES_SOCIALES = "SEL_TRABAJADORES_SOCIALES";
        public const String CTB_IND = "CTB_IND";
        public const String SO_REG_EXA = "SO_REG_EXA";
        public const String VEN_MST_FACTURAS_HST = "VEN_MST_FACTURAS_HST";
        public const String tmp_datos = "tmp_datos";
        public const String NOM_INC_PRE_NOMINA = "NOM_INC_PRE_NOMINA";
        public const String SO_REG_EXA_EMPLE = "SO_REG_EXA_EMPLE";
        public const String VEN_DET_FACTURAS = "VEN_DET_FACTURAS";
        public const String VEN_AUX_DET_FAC = "VEN_AUX_DET_FAC";
        public const String tmpempleados2 = "tmpempleados2";
        public const String BK_1129570730 = "BK_1129570730";
        public const String NOM_TABLA_RET_FUENTE = "NOM_TABLA_RET_FUENTE";
        public const String tmpregcontratos = "tmpregcontratos";
        public const String VEN_AUX_DET_PAGOS = "VEN_AUX_DET_PAGOS";
        public const String VEN_CARTERA = "VEN_CARTERA";
        public const String NOM_DET_BASE_NOM = "NOM_DET_BASE_NOM";
        public const String automatizacion_empleados = "automatizacion_empleados";
        public const String faltante472 = "faltante472";
        public const String VEN_CARTERA_HIS = "VEN_CARTERA_HIS";
        public const String VEN_CC_EMPRESAS = "VEN_CC_EMPRESAS";
        public const String TMPFINAL = "TMPFINAL";
        public const String CRT_ROL = "CRT_ROL";
        public const String CRT_PERMISOS_BOTONES = "CRT_PERMISOS_BOTONES";
        public const String CRT_EMPRESAS_POR_USUARIO = "CRT_EMPRESAS_POR_USUARIO";
        public const String VEN_CIERRE = "VEN_CIERRE";
        public const String tmp_VEN_MST_FACTURAS = "tmp_VEN_MST_FACTURAS";
        public const String VEN_CLA_ESP = "VEN_CLA_ESP";
        public const String tmp_contratos1 = "tmp_contratos1";
        public const String tmp_pruebas = "tmp_pruebas";
        public const String VEN_CLV_MAN_EMP = "VEN_CLV_MAN_EMP";
        public const String tmp_contratos_fch_cre = "tmp_contratos_fch_cre";
        public const String automatizacion_comprobantes_pago = "automatizacion_comprobantes_pago";
        public const String tmp_contratos_registro = "tmp_contratos_registro";
        public const String AUX_VAL = "AUX_VAL";
        public const String tmp_contratos_fch_mod = "tmp_contratos_fch_mod";
        public const String vacaciones_NOM_PAGOS_NOM = "vacaciones_NOM_PAGOS_NOM";
        public const String VEN_DET_PAGOS = "VEN_DET_PAGOS";
        public const String AUX_ALARMAS = "AUX_ALARMAS";
        public const String VEN_CONCEPTOS_IVA = "VEN_CONCEPTOS_IVA";
        public const String VEN_D_PERFILES = "VEN_D_PERFILES";


        public const String CON_REG_EMBARGOS = "CON_REG_EMBARGOS";


        public const String VEN_PRESUPUESTOS_ANIO = "VEN_PRESUPUESTOS_ANIO";

        public const String VEN_D_CALIFICACIONES_ENTREVISTA_PERFIL = "VEN_D_CALIFICACIONES_ENTREVISTA_PERFIL";
        public const String VEN_D_COMPETENCIAS_ENTREVISTA_PERFIL = "VEN_D_COMPETENCIAS_ENTREVISTA_PERFIL";

        public const String TMPFINAL_DANARANJO = "TMPFINAL_DANARANJO";
        public const String VEN_EJECUTIVOS = "VEN_EJECUTIVOS";
        public const String tmp_requisiciones_no_cumplidas_2 = "tmp_requisiciones_no_cumplidas_2";
        public const String NOM_AUTOLIQ_DET = "NOM_AUTOLIQ_DET";
        public const String VEN_P_FACTURACION = "VEN_P_FACTURACION";
        public const String VEN_Est_Facturacion = "VEN_Est_Facturacion";
        public const String VEN_Est_Ret_Fte = "VEN_Est_Ret_Fte";
        public const String ALARMAS_0901 = "ALARMAS_0901";
        public const String DF_CIUDADES = "DF_CIUDADES";
        public const String VEN_ALI_PAGOS = "VEN_ALI_PAGOS";
        public const String VEN_Est_Ret_Ica = "VEN_Est_Ret_Ica";
        public const String VEN_ISCD = "VEN_ISCD";
        public const String tmp_requisiciones_no_cumplidas_1 = "tmp_requisiciones_no_cumplidas_1";
        public const String VEN_DET_PAGOS_HST = "VEN_DET_PAGOS_HST";
        public const String CON_LPS_REVERSADAS = "CON_LPS_REVERSADAS";
        public const String VEN_AUX_ALI_PAGOS = "VEN_AUX_ALI_PAGOS";
        public const String VEN_ISCG = "VEN_ISCG";
        public const String VEN_MAN_SERV = "VEN_MAN_SERV";
        public const String VEN_P_BIENESTAR = "VEN_P_BIENESTAR";
        public const String VEN_P_CONTRATACION = "VEN_P_CONTRATACION";
        public const String VEN_P_ENVIO_CANDIDATOS = "VEN_P_ENVIO_CANDIDATOS";
        public const String VEN_REG_PAGOS_HST = "VEN_REG_PAGOS_HST";
        public const String VEN_P_PAGO_CTE = "VEN_P_PAGO_CTE";
        public const String VEN_D_CONTAB = "VEN_D_CONTAB";
        public const String NOM_MOD_INCAPACIDADES = "NOM_MOD_INCAPACIDADES";
        public const String VEN_P_PAGO_TRAB = "VEN_P_PAGO_TRAB";
        public const String AUDITORIA = "AUDITORIA";
        public const String NOM_NOMINA_DEFINITIVA_HST = "NOM_NOMINA_DEFINITIVA_HST";
        public const String VEN_P_SALUD_OCU = "VEN_P_SALUD_OCU";
        public const String TMPFINAL_BETA = "TMPFINAL_BETA";
        public const String NOM_RESUMEN_MES = "NOM_RESUMEN_MES";
        public const String BS_REG_CAP = "BS_REG_CAP";
        public const String CON_AFILIACION_EPS = "CON_AFILIACION_EPS";
        public const String VEN_REG_PAGOS = "VEN_REG_PAGOS";
        public const String NOM_RES_EMPLE = "NOM_RES_EMPLE";
        public const String NOM_INCAP_BOR = "NOM_INCAP_BOR";
        public const String VEN_R_CONTRATOS = "VEN_R_CONTRATOS";
        public const String VEN_R_SERVICIOS_RESPECTIVOS = "VEN_R_SERVICIOS_RESPECTIVOS";
        public const String VEN_HIST_ENVIO_DEVOLUCIONES = "VEN_HIST_ENVIO_DEVOLUCIONES";
        public const String CON_DIAGNOSTICOS = "CON_DIAGNOSTICOS";
        public const String BS_AFILIADOS_FONDO = "BS_AFILIADOS_FONDO";
        public const String VEN_P_NOMINA = "VEN_P_NOMINA";
        public const String LPS_CUARENTENA_2007 = "LPS_CUARENTENA_2007";
        public const String VEN_ULT_DOCUMENTOS = "VEN_ULT_DOCUMENTOS";
        public const String MenuPermisos = "MenuPermisos";
        public const String BS_ENTREGA_DOC = "BS_ENTREGA_DOC";
        public const String BS_REG_ACTIVIDADES = "BS_REG_ACTIVIDADES";
        public const String CIUDAD = "CIUDAD";
        public const String BS_REG_ASIS_CAP = "BS_REG_ASIS_CAP";
        public const String CRT_OCU_CNO = "CRT_OCU_CNO";
        public const String Menu_Web = "Menu_Web";
        public const String NOM_DET_BASE_NOM_HST = "NOM_DET_BASE_NOM_HST";
        public const String NOM_REG_INCAPACIDADES = "NOM_REG_INCAPACIDADES";
        public const String CON_AFILIACION_ARP = "CON_AFILIACION_ARP";
        public const String CON_AFILIACION_CAJAS_C = "CON_AFILIACION_CAJAS_C";
        public const String CON_AFILIACION_F_PENSIONES = "CON_AFILIACION_F_PENSIONES";
        public const String CRT_OCU_INT = "CRT_OCU_INT";
        public const String CON_ASP_ENVIADOS_CLIENTE = "CON_ASP_ENVIADOS_CLIENTE";
        public const String CON_AUX_CONTRATOS_LIQ = "CON_AUX_CONTRATOS_LIQ";
        public const String CON_ENT_CAJAS_C = "CON_ENT_CAJAS_C";
        public const String VEN_DET_NOTAS_CR = "VEN_DET_NOTAS_CR";
        public const String NOM_BASE_EMPLEADOS = "NOM_BASE_EMPLEADOS";
        public const String CON_CONTRATOS_LIQ = "CON_CONTRATOS_LIQ";
        public const String VEN_MST_FACTURAS = "VEN_MST_FACTURAS";
        public const String CON_ENT_ARP = "CON_ENT_ARP";
        public const String NOM_AUTOLIQ_DET_HST = "NOM_AUTOLIQ_DET_HST";
        public const String CON_ENT_EPS = "CON_ENT_EPS";
        public const String automatizacion_lps = "automatizacion_lps";
        public const String CON_ENT_F_PENSIONES = "CON_ENT_F_PENSIONES";
        public const String CON_NOVEDADES_CONT = "CON_NOVEDADES_CONT";
        public const String NOM_IBG_DF = "NOM_IBG_DF";
        public const String AUX_EST_NOMINA = "AUX_EST_NOMINA";
        public const String CRT_USUARIOS = "CRT_USUARIOS";
        public const String CRT_REG_USUARIOS_NO_PLANTA = "CRT_REG_USUARIOS_NO_PLANTA";
        public const String NOM_MST_BASE_NOM_HST = "NOM_MST_BASE_NOM_HST";
        public const String CON_REQUERIMIENTOS = "CON_REQUERIMIENTOS";
        public const String fenix_ciiu = "fenix_ciiu";
        public const String VEN_DET_NOTAS_DB = "VEN_DET_NOTAS_DB";
        public const String CON_REQ_SEG = "CON_REQ_SEG";
        public const String CRT_VAL_AUTO = "CRT_VAL_AUTO";
        public const String T_AUX_NOM_DEF = "T_AUX_NOM_DEF";
        public const String VEN_D_FAC_PAG = "VEN_D_FAC_PAG";
        public const String NOM_AUX_CUO_ACUM_ASEO = "NOM_AUX_CUO_ACUM_ASEO";
        public const String NOM_AUX_DED_ACUM_ASEO = "NOM_AUX_DED_ACUM_ASEO";
        public const String NOM_AUX_DEV_ACUM_ASEO = "NOM_AUX_DEV_ACUM_ASEO";
        public const String NOM_AUX_LIQ_CES = "NOM_AUX_LIQ_CES";
        public const String CRT_DERECHOS = "CRT_DERECHOS";
        public const String VEN_AUX_DET_NOTAS_CR = "VEN_AUX_DET_NOTAS_CR";
        public const String VEN_SUB_CENTROS = "VEN_SUB_CENTROS";
        public const String NOM_AUX_LIQ_PRIMA = "NOM_AUX_LIQ_PRIMA";
        public const String NOM_PAGOS_NOM_HST = "NOM_PAGOS_NOM_HST";
        public const String NOM_AUX_LIQ_VAC = "NOM_AUX_LIQ_VAC";
        public const String NOM_AUX_NOV_NOM = "NOM_AUX_NOV_NOM";
        public const String NOM_NOMINA_NOVEDADES20110429 = "NOM_NOMINA_NOVEDADES20110429";
        public const String NOM_CHEQUES_SUB_FAM = "NOM_CHEQUES_SUB_FAM";
        public const String NOM_NOVEDADES_BKP_SPN = "NOM_NOVEDADES_BKP_SPN";
        public const String NOM_CHEQUES_SUB_N_PAG = "NOM_CHEQUES_SUB_N_PAG";
        public const String NOM_CHEQUES_SUB_PAGADOS = "NOM_CHEQUES_SUB_PAGADOS";
        public const String NOM_AUTOLIQ_NOTAS = "NOM_AUTOLIQ_NOTAS";
        public const String NOM_CONCEPTOS_AUTOMATICOS = "NOM_CONCEPTOS_AUTOMATICOS";
        public const String NOM_CONCEPTOS_EMPLEADO = "NOM_CONCEPTOS_EMPLEADO";
        public const String VEN_AUX_DET_NOTAS_DB = "VEN_AUX_DET_NOTAS_DB";
        public const String NOM_CONCEPTOS_NOMINA = "NOM_CONCEPTOS_NOMINA";
        public const String clientes = "clientes";
        public const String NOM_MEMOS_NOMINA = "NOM_MEMOS_NOMINA";
        public const String DEPARTAMENTO1 = "DEPARTAMENTO1";
        public const String DEPARTAMENTO = "DEPARTAMENTO";
        public const String NOM_IBG = "NOM_IBG";
        public const String SEL_REG_PRUEBAS_PERFIL = "SEL_REG_PRUEBAS_PERFIL";
        public const String CON_REG_CLAUSULAS_PERFIL = "CON_REG_CLAUSULAS_PERFIL";

        public const String CON_REG_PROCESOS_DISCIPLINARIOS = "CON_REG_PROCESOS_DISCIPLINARIOS";

        public const String CON_REG_TUTELAS = "CON_REG_TUTELAS";
        public const String CON_REG_DEMANDAS = "CON_REG_DEMANDAS";
        public const String CON_REG_DERECHOS_PETICION = "CON_REG_DERECHOS_PETICION";

        public const String REG_ADJUNTOS_ACTOS_JURIDICOS = "REG_ADJUNTOS_ACTOS_JURIDICOS";
        public const String REG_ADJUNTOS_INVESTIGACIONES_ADMINISTRATIVAS = "REG_ADJUNTOS_INVESTIGACIONES_ADMINISTRATIVAS";
        public const String REG_ADJUNTOS_REQUERIMIENTOS_MINISTERIO = "REG_ADJUNTOS_REQUERIMIENTOS_MINISTERIO";

        public const String REG_ABOGADOS = "REG_ABOGADOS";
        public const String REG_ABOGADO_ARCHIVO = "REG_ABOGADO_ARCHIVO";
        public const String TERCEROS_DESCUENTOS = "TERCEROS_DESCUENTOS";


        public const String CON_REG_DOCUMENTOS_ENTREGABLES = "CON_REG_DOCUMENTOS_ENTREGABLES";


        public const String PAR_MODELO_SOPORTES = "PAR_MODELO_SOPORTES";
        public const String PAR_MODELO_SOPORTES_DETALLE = "PAR_MODELO_SOPORTES_DETALLE";

        public const String REG_CONTROL_FALLECIDOS = "REG_CONTROL_FALLECIDOS";

        public const String REG_DOCUMENTOS_LEGALES = "REG_DOCUMENTOS_LEGALES";

        public const String REG_COMITE_CONVIVENCIA = "REG_COMITE_CONVIVENCIA";

        public const String CON_REG_INVSTIGACIONES_ADMINISTRATIVAS = "CON_REG_INVSTIGACIONES_ADMINISTRATIVAS";

        public const String CON_REG_REQUERIMIENTOS_MINISTERIO = "CON_REG_REQUERIMIENTOS_MINISTERIO";
        public const String TERCEROS = "TERCEROS";
        public const String TERCEROS_CREDITOS = "TERCEROS_CREDITOS";

        public const String VEN_D_NOMINA_INCAPACIDADES = "VEN_D_NOMINA_INCAPACIDADES";

        public const String REG_AUDITORIA_CONTRATOS = "REG_AUDITORIA_CONTRATOS";

        public const String ALM_FIRMAS_ORDEN_COMPRA = "ALM_FIRMAS_ORDEN_COMPRA";

        public const String VEN_EMPRESAS_RIESGOS = "VEN_EMPRESAS_RIESGOS";

        public const String VEN_REG_CAMBIO_RAZ_SOCIAL = "VEN_REG_CAMBIO_RAZ_SOCIAL";

        public const String CON_CONFIGURACION_DOCS_ENTREGABLES = "CON_CONFIGURACION_DOCS_ENTREGABLES";

        public const String PROG_PRESUPUESTOS = "PROG_PRESUPUESTOS";

        public const String PROG_PRESUPUESTOS_GENERALES = "PROG_PRESUPUESTOS_GENERALES";

        public const String GH_PROGRAMACION_VACACIONES = "GH_PROGRAMACION_VACACIONES";

        public const String PROG_SUB_PROGRAMAS = "PROG_SUB_PROGRAMAS";
        public const String PROG_ACTIVIDADES = "PROG_ACTIVIDADES";
        public const String PROG_MAESTRA_PROGRAMAS = "PROG_MAESTRA_PROGRAMAS";
        public const String PROG_DETALLE_SUB_PROGRAMAS = "PROG_DETALLE_SUB_PROGRAMAS";
        public const String PROG_DETALLE_ACTIVIDADES = "PROG_DETALLE_ACTIVIDADES";
        public const String PROG_CONTROL_ASISTENCIA_ACTIVIDADES = "PROG_CONTROL_ASISTENCIA_ACTIVIDADES";
        public const String PROG_GENERAL_MAESTRA = "PROG_GENERAL_MAESTRA";
        public const String PROG_GENERAL_DETALLE = "PROG_GENERAL_DETALLE";
        public const String PROG_ENTIDADES_COLABORADORAS = "PROG_ENTIDADES_COLABORADORAS";
        public const String PROG_ENTIDADES_ACTIVIDADES = "PROG_ENTIDADES_ACTIVIDADES";

        public const String PROG_MOTIVOS_CANCELACION_REPROGRAMACION = "PROG_MOTIVOS_CANCELACION_REPROGRAMACION";

        public const String PROG_CORTES_INFORMES_CLIENTES = "PROG_CORTES_INFORMES_CLIENTES";

        public const String PROG_TIPOS_ACTIVIDAD = "PROG_TIPOS_ACTIVIDAD";

        public const String PROG_MAESTRA_COMPROMISOS = "PROG_MAESTRA_COMPROMISOS";

        public const String PROG_SEGUIMIENTO_CASOS_SEVEROS = "PROG_SEGUIMIENTO_CASOS_SEVEROS";

        public const String PROG_RECOMENDACIONES_CASOS_SEVEROS = "PROG_RECOMENDACIONES_CASOS_SEVEROS";

        public const String PROG_DIAGNOSTICOS_ADICIONALES_CASOS_SEVEROS = "PROG_DIAGNOSTICOS_ADICIONALES_CASOS_SEVEROS";

        public const String PROG_DET_SEG_CASOS_SEVEROSS = "PROG_DET_SEG_CASOS_SEVEROSS";

        public const String PROG_SEGUIMIENTO_ACCIDENTES = "PROG_SEGUIMIENTO_ACCIDENTES";

        public const String SEL_REG_MAESTRA_ROTACION_RETIROS = "SEL_REG_MAESTRA_ROTACION_RETIROS";
        public const String SEL_REG_DETALLE_ROTACION_RETIROS = "SEL_REG_DETALLE_ROTACION_RETIROS";

        public const String SEL_REG_ROTACION_EMPRESA = "SEL_REG_ROTACION_EMPRESA";
        public const String SEL_REG_MAESTRA_ROTACION_EMPLEADO = "SEL_REG_MAESTRA_ROTACION_EMPLEADO";
        public const String SEL_REG_DETALLE_ROTACION_EMPLEADO = "SEL_REG_DETALLE_ROTACION_EMPLEADO";

        public const String SEL_REG_COMPOSICION_FAMILIAR = "SEL_REG_COMPOSICION_FAMILIAR";
        public const String SEL_REG_INFORMACION_ACADEMICA = "SEL_REG_INFORMACION_ACADEMICA";
        public const String SEL_REG_EXPERIENCIA_LABORAL = "SEL_REG_EXPERIENCIA_LABORAL";
        public const String SEL_REG_CALIFICACION_HABILIDADES = "SEL_REG_CALIFICACION_HABILIDADES";

        public const String SEL_REG_ASSESMENT_CENTER = "SEL_REG_ASSESMENT_CENTER";
        public const String SEL_REG_COMPETENCIAS_ASSESMENT = "SEL_REG_COMPETENCIAS_ASSESMENT";

        public const String ALM_CRT_ENVIO_ORDENES = "ALM_CRT_ENVIO_ORDENES";

        public const String ALM_CRT_PERMISOS_SOLICITUDES_PLANTA = "ALM_CRT_PERMISOS_SOLICITUDES_PLANTA";
        public const String ALM_REG_SOLICITUDES_PLANTA = "ALM_REG_SOLICITUDES_PLANTA";

        public const String SO_CAUSAS_BASICAS_PERDIDAS_POR_ACCIDENTE = "SO_CAUSAS_BASICAS_PERDIDAS_POR_ACCIDENTE";

        public const String GH_EVALUACIONES = "GH_EVALUACIONES";

        public const String NOM_CONTROL_PERIODOS = "NOM_CONTROL_PERIODOS";

        #region nomina
        public const String PAR_RETENCION = "PAR_RETENCION";
        public const String PAR_IVA = "PAR_IVA";
        public const String PAR_UVT = "PAR_UVT";
        public const String PAR_SALARIALES = "PAR_SALARIALES";
        public const String PAR_LIQUIDACION_HORAS = "PAR_LIQUIDACION_HORAS";
        public const String PAR_MODELO_FACTURA = "PAR_MODELO_FACTURA";
        public const String PAR_MODELO_FACTURA_DETALLE = "PAR_MODELO_FACTURA_DETALLE";
        public const String PAR_MODELO_NOMINA = "PAR_MODELO_NOMINA";
        public const String PAR_PRESTACIONALES = "PAR_PRESTACIONALES";
        public const String PAR_SEGURIDAD_SOCIAL = "PAR_SEGURIDAD_SOCIAL";
        public const String PAR_UNIDAD_REPORTE = "PAR_UNIDAD_REPORTE";
        public const String PAR_PARAFISCALES = "PAR_PARAFISCALES";
        public const String PAR_PERIODOS_PAGO = "PAR_PERIODOS_PAGO";
        public const String PAR_INCAPACIDADES = "PAR_INCAPACIDADES";
        public const String PAR_AUSENTISMOS = "PAR_AUSENTISMOS";
        #endregion nomina

        #region pagos
        public const String PAGOS = "PAGOS";
        public const String PAGOS_EMPLEADOS = "PAGOS_EMPLEADOS";
        public const String PAGOS_EMPLEADOS_PLANOS = "PAGOS_EMPLEADOS_PLANOS";
        public const String PAGOS_EMPLEADOS_REVERSADOS = "PAGOS_EMPLEADOS_REVERSADOS";
        public const String PAGOS_EMPLEADOS_RECHAZOS = "PAGOS_EMPLEADOS_RECHAZOS";
        #endregion pagos

        public const String ALM_REF_PRODUCTOS_PROVEEDOR = "ALM_REF_PRODUCTOS_PROVEEDOR";

        public const String ALM_CRT_DESCARGUE_INVENTARIO = "ALM_CRT_DESCARGUE_INVENTARIO";

        #endregion tablas

        #region parametros

        public const String PARAMETROS_ESTADO_RETIRO = "ESTADO_RETIRO";
        public const String PARAMETROS_ESTADO_INCAPACIDAD = "ESTADO_INCAPACIDAD";
        public const String PARAMETROS_ESTADO_INCAPACIDAD_TRAMITE = "ESTADO_INCAPACIDAD_TRAMITE";
        public const String PARAMETROS_INCAPACIDAD_TRAMITADA_POR = "INCAPACIDAD_TRAMITADA_POR";

        public const String PARAMETROS_TIPOS_CLAUSULA = "TIPOS_CLAUSULA";
        public const String PARAMETROS_TIPOS_CLAUSULA_ESTADOS = "TIPOS_CLAUSULA_ESTADO";

        public const String PARAMETROS_TIPO_CUENTA = "TIPO_CUENTA";

        public const String PARAMETROS_CHEQUE_TIPO = "CHEQUE_TIPO";

        public const String PARAMETROS_ESTADO_AUTORIZACION = "ESTADO_AUTORIZACION";

        public const String PARAMETROS_TABLA_AUDITORIA = "TABLA_AUDITORIA";
        public const String PARAMETROS_ESTADO_CONTACTO = "ESTADO_CONTACTO";
        public const String PARAMETROS_ESTADO_EMPRESA = "ESTADO_EMPRESA";

        public const String PARAMETROS_ESTADO_SUB_PROGRAMA = "ESTADO_SUB_PROGRAMA";
        public const String PARAMETROS_TIPOS_ACTIVIDAD = "TIPOS_ACTIVIDAD";
        public const String PARAMETROS_SECTORES_ACTIVIDAD = "SECTORES_ACTIVIDAD";
        public const String PARAMETROS_ESTADO_ACTIVIDAD_RSE_GLOBAL = "ESTADO_ACTIVIDAD_RSE_GLOBAL";

        public const String PARAMETROS_ESTADO_ROLES = "ESTADO_ROLES";
        public const String PARAMETROS_ESTADO_USUARIOS = "ESTADO_USUARIOS";
        public const String PARAMETROS_ESTADO_CARGO = "ESTADO_CARGOS";
        public const String PARAMETROS_REGIMEN_EMPRESA = "REGIMEN_EMPRESA";
        public const String PARAMETROS_MODELO_SOPORTE = "MODELO_SOPORTE";
        public const String PARAMETROS_MODELO_FACTURA = "MODELO_FACTURA";

        public const String PARAMETROS_CAT_LICENCIA_CONDUCCION = "CAT_LICENCIA_CONDUCCION";
        public const String PARAMETROS_SEXO = "SEXO";
        public const String PARAMETROS_NIV_ESTUDIOS = "NIV_ESTUDIOS";
        public const String PARAMETROS_TIPO_REQ = "TIPO_REQ";

        public const String PARAMETROS_MOTIVO_CANCELA_REQ = "MOTIVO_CANCELA_REQ ";
        public const String PARAMETROS_MOTIVO_CUMPLIDO_REQ = "MOTIVO_CUMPLIDO_REQ ";

        public const String PARAMETROS_TIP_DOC_ID = "TIP_DOC_ID";
        public const String PARAMETROS_SEXOREQ = "SEXOREQ";
        public const String PARAMETROS_ESTADO_TRABAJADORES = "ESTADO_TRABAJADORES";
        public const String PARAMETROS_ESTADO_TRABAJADORES_HV = "ESTADO_TRABAJADORES_HV";
        public const String PARAMETROS_ALERTAS_REQUISICIONES = "ALERTAS_REQUISICIONES";
        public const String PARAMETROS_PERIODO_PAGO = "PERIODO_PAGO";
        public const String PARAMETROS_TABLA_RIESGOS = "TABLA_RIESGOS";

        public const String PARAMETROS_ESTADO_CIVIL = "ESTADO_CIVIL";

        public const String PARAMETROS_TIPO_PROVEEDOR = "TIPO_PROVEEDOR";

        public const String PARAMETROS_EXPERIENCIA = "EXPERIENCIA";
        public const String PARAMETROS_TIPO_REFERENCIA_LABORAL = "TIPO_REFERENCIA_LABORAL";

        public const String PARAMETROS_CLASE_CONTRATO = "CLASE_CONTRATO";
        public const String PARAMETROS_TIPO_CONTRATO = "TIPO_CONTRATO";
        public const String PARAMETROS_DESCRIPCION_SALARIO = "DESCRIPCION_SALARIO";


        public const String PARAMETROS_PERIODO_ENTREGA_OBJETO = "PERIODO_ENTREGA_OBJETO";

        public const String PARAMETROS_FACTURACION_EXAMENES = "FACTURACION_EXAMENES";

        public const String PARAMETROS_PROCESO_DISCIPLINARIO = "PROCESO_DISCIPLINARIO";
        public const String PARAMETROS_MOTIVO_PROCESO_DISCIPLINARIO = "MOTIVO_PROCESO_DISCIPLINARIO";
        public const String PARAMETROS_MOTIVO_DERECHO_PETICION = "MOTIVO_DERECHO_PETICION";

        public const String PARAMETROS_TIPO_PROCESO_DEMANDA = "TIPO_PROCESO_DEMANDA";
        public const String PARAMETROS_MOTIVO_DEMANDA = "MOTIVO_DEMANDA";

        public const String PARAMETROS_DESCUENTOS_CREDITOS = "DESCUENTOS_CREDITOS";

        public const String PARAMETROS_SERVICIOS_RESPECTIVOS = "SERVICIOS_RESPECTIVOS";

        public const String PARAMETROS_UNIDAD_NEGOCIO = "UNIDAD_NEGOCIO";

        public const String PARAMETROS_CUENTA_FORMATO_APERTURA_CUENTA = "CUENTA_FORMATO_APERTURA_CUENTA";

        public const String PARAMETROS_DIAS_NIVELES_REQUERIMIENTO = "DIAS_NIVELES_REQUERIMIENTO";
        public const String PARAMETROS_PORCENTAJES_NIVELES_REQUERIMIENTO = "PORCENTAJES_NIVELES_REQUERIMIENTO";

        public const String PARAMETROS_CALIFICACIONES_REFERENCIA = "CALIFICACIONES_REFERENCIA";

        public const String PARAMETROS_TIPOS_DOCS_PROV = "TIPOS_DOCS_PROV";

        public const String PARAMETROS_RIESGO_GENERADOR = "RIESGO_GENERADOR";
        public const String PARAMETROS_TIPO_ACCIDENTE = "TIPO_ACCIDENTE";
        public const String PARAMETROS_TIPO_LESION = "TIPO_LESION";
        public const String PARAMETROS_PARTE_CUERPO = "PARTE_CUERPO";
        public const String PARAMETROS_AGENTE_LESION = "AGENTE_LESION";
        public const String PARAMETROS_MECANISMO_LESION = "MECANISMO_LESION";
        public const String PARAMETROS_SITIO = "SITIO";
        public const String PARAMETROS_CONDICION_INSEGURA = "CONDICION_INSEGURA";
        public const String PARAMETROS_ACTO_INSEGURO = "ACTO_INSEGURO";

        public const String PARAMETROS_TIPO_EMBARGO = "TIPO_EMBARGO";
        public const String PARAMETROS_CONCEPTO_EMBARGO = "CONCEPTO_EMBARGO";

        public const String SERVICIO_EMPRESA_TEMPORAL = "TEMPORAL";
        public const String SERVICIO_EMPRESA_OUTSORUCING = "OUTSOURCING";

        public const String ACCION_ADICIONAR = "ADICIONAR";
        public const String ACCION_ACTUALIZAR = "ACTUALIZAR";
        public const String ACCION_ANULAR = "ANULAR";
        public const String ACCION_ELIMINAR = "ELIMINAR";
        public const String ACCION_CONSULTAR = "CONSULTAR";
        public const String ACCION_INICIO_SESION = "INICIO_SESION";
        public const String ACCION_LIQUIDAR = "LIQUIDAR";
        public const String ACCION_RELIQUIDAR = "RELIQUIDAR";
        public const String ACCION_VALIDAR = "VALIDAR";
        public const String ACCION_REVERSAR = "REVERSAR";
        public const String ACCION_AUTORIZAR = "AUTORIZAR";
        public const String ACCION_DESBLOQUEAR = "DESBLOQUEAR";
        public const String ACCION_RECHAZAR = "RECHAZAR";
        public const String ACCION_REACTIVAR_RECHAZO = "REACTIVAR_RECHAZO";


        public const String PARAMETROS_SERVIDOR_WEB = "SERVIDOR_WEB";

        public const String PARAMETROS_NUMERO_CUENTA = "NUMERO_CUENTA";

        public const String PARAMETROS_PERIODO_CARENCIA = "PERIODO_CARENCIA";

        public const String PARAMETROS_TIPO_INCAPACIDAD = "TIPO_INCAPACIDAD";

        public const String PARAMETROS_CASO_SEVERO = "CASO_SEVERO";

        public const String PARAMETROS_CLASE_INCAPACIDAD = "CLASE_INCAPACIDAD";

        public const String PARAMETROS_PRORROGA = "PRORROGA";

        public const String PARAMETROS_TALLA_ROPA = "TALLA_ROPA";

        public const String PARAMETROS_IVA_ORDEN_COMPRA = "IVA_ORDEN_COMPRA";

        public const String PARAMETROS_AVISO_OBJETO_CONTRATO = "AVISO_OBJETO_CONTRATO";

        public const String PARAMETROS_FORMA_PAGO = "FORMA_PAGO";

        public const String PARAMETROS_TIPO_PENSIONADO = "TIPO_PENSIONADO";

        public const String PARAMETROS_ROL_INFORMAR_DESCARTE_PROCESO_CONTRATACION = "ROL_INFORMAR_DESCARTE_PROCESO_CONTRATACION";

        public const String PARAMETROS_NUMEROS_JUZGADOS = "NUMEROS_JUZGADOS";
        public const String PARAMETROS_ESPECIALIDADES_JUZGADOS = "ESPECIALIDADES_JUZGADOS";
        public const String PARAMETROS_CATEGORIAS_JUZGADOS = "CATEGORIAS_JUZGADOS";

        public const String PARAMETROS_RESULTADOS_ACTOS_JURIDICOS = "RESULTADOS_ACTOS_JURIDICOS";

        public const String PARAMETROS_MOTIVO_FALLECIMIENTO = "MOTIVO_FALLECIMIENTO";
        public const String PAR_CONTABLE_PUC = "PAR_CONTABLE_PUC";

        public const String PARAMETROS_ENTIDADES_INVESTIGAN = "ENTIDADES_INVESTIGAN";

        public const String PARAMETROS_NUCLEO_FORMACION = "NUCLEO_FORMACION";

        public const String PARAMETROS_MOVIMIENTO_INVENTARIO = "MOVIMIENTO_INVENTARIO";

        public const String PARAMETROS_NATURALEZA_JURIDICA = "NATURALEZA_JURIDICA";

        public const String PARAMETROS_PROCESO_ORDEN_COMPRA = "PROCESO_ORDEN_COMPRA";

        public const String PARAMETROS_BASE_HORA_EXTRAS = "BASE_HORA_EXTRAS";

        public const String PARAMETROS_CALCULO_RETENCION_FUENTE = "CALCULO_RETENCION_FUENTE";

        public const String PARAMETROS_REPORTAR_POR = "REPORTAR_POR";

        public const String PARAMETROS_ENTIDAD_ACOSET = "ENTIDAD_ACOSET";


        public const String PARAMETROS_DESCARTE_SELECCION = "DESCARTE_SELECCION";

        #endregion parametros

        public enum proceso
        {
            ContactoComercial = 1,
            ContactoSeleccion = 2,
            ContactoContratacion = 3,
            ContactoNominaFacturacion = 4,
            Nomina = 5,
            Facturacion = 6,
            ContactoRse = 7,
            ContactoGlobalSalud = 8,
            ContactoBienestarSocial = 9,
            ContactoContabilidad = 10,
            ContactoJuridica = 11,
            ContactoFinanciera = 12,
            ContactoSaludIntegral = 13,
            ContactoOperaciones = 14,
            ContactoComprasEInventario = 15,
            ContactoLiquidacionPrestaciones = 16,
            Contabilidad = 17,
            Financiera = 18,
            ContactoGestionHumana = 19
        }

        public enum tipoParametroContable
        {
            PUC = 1,
            CentroCosto = 2
        }

        public enum tipoDropMovimientos
        {
            drop_Empresa = 1,
            drop_Ciudades = 2,
            drop_CentrosCosto = 3,
            drop_SubCentrosCosto = 4,
            drop_Periodos = 5,
            drop_Empleados = 6,
            drop_Conceptos = 7
        }

        public const String DIR_IMAGENES_PARA_PDF = "http://localhost:9090/imagenes/reportes";

        public const String VAR_NOMBRE_SERTEMPO = "Servicios Temporales Profesionales Bogotá S.A.";
        public const String VAR_DOMICILIO_SERTEMPO = "Cll 77 # 14 - 31 bogotá D.C.";
        public const String VAR_NIT_SERTEMPO = "860,058,975-6";
        public const String VAR_NIT_SERTEMPO_CONTABLE = "860058975";
        public const String VAR_DIGITO_VER_SERTEMPO = "6";
        public const String VAR_TELEFONO_SERTEMPO = "321-70-88";

        public const String VAR_NOMBRE_EYS = "EFICIENCIA & SERVICIOS LTDA";
        public const String VAR_DOMICILIO_EYS = "Cll 82 # 18 - 12 bogotá D.C.";
        public const String VAR_NIT_EYS = "830,060,026-9";
        public const String VAR_DIGITO_VER_EYS = "9";
        public const String VAR_NIT_EYS_CONTABLE = "830060026";
        public const String VAR_TELEFONO_EYS = "321-70-88";

        public const String VAR_NIT_SENA = "899999034";
        public const String VAR_NIT_ICBF = "899999239";

        public const String VAR_DOC_EXISTENCIA_REPRESENTACION = "EXISTENCIA_REPRESENTACION";
        public const String VAR_DOC_LICENCIA_FUNCIONAMIENTO = "LICENCIA_FUNCIONAMIENTO";
        public const String VAR_DOC_POLIZA_CUMPLIMIENTO = "POLIZA_CUMPLIMIENTO";
        public const String VAR_DOC_REGLAMENTO_INTERNO = "REGLAMENTO_INTERNO";
        public const String VAR_DOC_REGLAMENTO_HIGIENE_SEGURIDAD = "REGLAMENTO_HIGIENE_SEGURIDAD";
        public const String VAR_DOC_AUTORIZACION_HORAS_EXTRAS = "AUTORIZACION_HORAS_EXTRAS";
        public const String VAR_DOC_COPASO = "COPASO";
        public const String VAR_DOC_MANUAL_CONVIVENCIA = "MANUAL_CONVIVENCIA";

        public const String VAR_EVALUACION_PROVEEDORES = "EVALUACIÓN";
        public const String VAR_REEVALUACION_PROVEEDORES = "REEVALUACIÓN";

        public const Decimal VAR_IVA = 16;

        public const String VAR_ESTADO_ORDEN_COMPRA_FALTA_AUTORIZACION = "FALTA AUTORIZACIÓN";
        public const String VAR_ESTADO_ORDEN_COMPRA_ENVIADA_A_PROVEEDOR = "ENVIADA A PROVEEDOR";
        public const String VAR_ESTADO_ORDEN_COMPRA_ADJUNTADO_FACTURA = "DESCARGANDO A INVENTARIO";
        public const String VAR_ESTADO_ORDEN_COMPRA_FINALIZADA = "COMPLETADA";
        public const String VAR_ESTADO_ORDEN_COMPRA_CANCELADA = "CERRADA";

        public const String VAR_ESTADO_PROCESO_REG_SOLICITUDES_INGRESO = "AUDITADO SOLICITUD DE INGRESO";
        public const String VAR_ESTADO_PROCESO_CON_AFILIACION_ARP = "AUDITADO AFILIACIÓN DE ARL";
        public const String VAR_ESTADO_PROCESO_CON_AFILIACION_EPS = "AUDITADO AFILIACIÓN DE EPS";
        public const String VAR_ESTADO_PROCESO_CON_AFILIACION_CCF = "AUDITADO AFILIACIÓN DE CAJA DE C.";
        public const String VAR_ESTADO_PROCESO_CON_AFILIACION_FONDO = "AUDITADO AFILIACIÓN DE FONDE DE P.";
        public const String VAR_ESTADO_PROCESO_CONCEPTOS_FIJOS = "AUDITADO CONCEPTOS FIJOS";
        public const String VAR_ESTADO_PROCESO_CONTRATO = "AUDITADO DATOS CONTRATO";
        public const String VAR_ESTADO_PROCESO_EXAMENES = "AUDITADO DATOS EXAMENES Y AUTOS";
        public const String VAR_ESTADO_PROCESO_ENVIO_ARCHIVOS = "AUDITADO ENVÍO DE DOCUMENTACIÓN";

        public const int VAR_NUM_REEVALUACIONES_PROVEEDOR_PERMITIDAS = 2;

        public const Decimal VAR_MARGEN_AVISO_EMPLEADOS = 10;

        public const String NOMBRE_AREA_GESTION_COMERCIAL = "COMERCIAL";
        public const String NOMBRE_AREA_SELECCION = "RECLUTAMIENTO, SELECCIÓN Y REQUISICIONES";
        public const String NOMBRE_AREA_CONTRATACION = "CONTRATACIÓN Y RELACIONES LABORALES";
        public const String NOMBRE_AREA_LIQUIDACION_NOMINA = "LIQUIDACIÓN DE NÓMINA";
        public const String NOMBRE_AREA_FINANCIERA = "FINANCIERA";
        public const String NOMBRE_AREA_JURIDICA = "JURÍDICA";
        public const String NOMBRE_AREA_SALUD_INTEGRAL = "SALUD INTEGRAL";
        public const String NOMBRE_AREA_OPERACIONES = "OPERACIONES";
        public const String NOMBRE_AREA_BIENESTAR_SOCIAL = "BIENESTAR SOCIAL";
        public const String NOMBRE_AREA_RESPONSABILIDAD_SOCIAL_EMPRESARIAL = "RESPONSABILIDAD SOCIAL EMPRESARIAL";
        public const String NOMBRE_AREA_COMPRAS_INVENTARIO = "COMPRAS E INVENTARIO";
        public const String NOMBRE_AREA_GESTION_HUMANA = "GESTIÓN HUMANA";
        public const String NOMBRE_AREA_CONTABILIDAD = "CONTABILIDAD";
        public const String NOMBRE_AREA_ADMINISTRACION = "ADMINISTRACIÓN";

        public const Decimal VAR_ID_EMPRESA_SERTEMPO = 205;

        public const Decimal VAR_ID_EMPRESA_EYS = 34;

        public const String VAR_PASS_PreferredPasswordLength = "10";
        public const String VAR_PASS_MinimumNumericCharacters = "1";
        public const String VAR_PASS_MinimumSymbolCharacters = "1";
        public const String VAR_PASS_RequiresUpperAndLowerCaseCharacters = "true";

        public static DateTime fechaPublicacion = new DateTime(2012, 9, 21, 9, 30, 00, 0);

        public enum EntregaAjusteA
        {
            CONTRATO = 0,
            FECHA
        }

        public static TimeSpan inicioJornadaDiurna = new TimeSpan(6, 0, 0);
        public static TimeSpan finJornadaDiurna = new TimeSpan(21, 59, 59);
        public static TimeSpan inicioJornadaNocturna = new TimeSpan(22, 0, 0);
        public static TimeSpan finJornadaNocturna = new TimeSpan(5, 59, 59);
        #endregion variables
    }
}
