using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;


namespace Brainsbits.LLB.comercial
{
    public class condicionComercial
    {
        #region variables
        enum Empresas
        {
            Sertempo = 1,
            EYS = 3
        }
        String _empresa = null;
        String _mensaje_error = null;
        String _usuario = null;
        Decimal _ID_SERVICIO = 0;
        Decimal _ID_DETALLE_SERVICIO = 0;
        Decimal _ID_SERVICIO_COMPLEMENTARIO = 0;
        Decimal _AIU = 0;
        Decimal _IVA = 0;
        Decimal _VALOR = 0;
        String _ACCION = null;

        #endregion variables

        #region propiedades
        private String Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        public String MensajeError
        {
            get { return _mensaje_error; }
            set { _mensaje_error = value; }
        }

        public String Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }

        public Decimal ID_DETALLE_SERVICIO
        {
            get { return _ID_DETALLE_SERVICIO; }
            set { _ID_DETALLE_SERVICIO = value; }
        }
        public Decimal ID_SERVICIO
        {
            get { return _ID_SERVICIO; }
            set { _ID_SERVICIO = value; }
        }

        public Decimal ID_SERVICIO_COMPLEMENTARIO
        {
            get { return _ID_SERVICIO_COMPLEMENTARIO; }
            set { _ID_SERVICIO_COMPLEMENTARIO = value; }
        }

        public Decimal AIU
        {
            get { return _AIU; }
            set { _AIU = value; }
        }

        public Decimal IVA
        {
            get { return _IVA; }
            set { _IVA = value; }
        }

        public Decimal VALOR
        {
            get { return _VALOR; }
            set { _VALOR = value; }
        }

        public String ACCION
        {
            get { return _ACCION; }
            set { _ACCION = value; }
        }
        #endregion propiedades

        #region constructores
        public condicionComercial(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos

        #region CondicionesComerciales
        public Decimal Adicionar(Decimal ID_EMPRESA, String FACTURA, String REGIMEN, String SOLO_DEV, String VAC_PARAF, int DIAS_VNC,
            Decimal AD_NOM, Decimal AD_PENSION, Decimal AD_SALUD, Decimal AD_RIESGOS, Decimal AD_APO_SENA, Decimal AD_APO_ICBF,
            Decimal AD_APO_CAJA, Decimal AD_VACACIONES, Decimal AD_CESANTIA, Decimal AD_INT_CES, Decimal AD_PRIMA,
            Decimal AD_SEG_VID, String SUB_PENSION, String SUB_SALUD, String SUB_RIESGOS, String SUB_SENA, String SUB_ICBF,
            String SUB_CAJA, String SUB_VACACIONES, String SUB_CESANTIAS, String SUB_INT_CES, String SUB_PRIMA,
            String SUB_SEG_VID, String MOD_SOPORTE, String MOD_FACTURA, String OBS_FACT, String RET_VAC, String RET_CES,
            String RET_INT_CES, String RET_PRIM, String USU_CRE, String APL_MTZ, String ID_CIUDAD, Decimal ID_CENTRO_C, Decimal ID_SUB_C,
            List<servicio> SERVICIO, List<detalleServicio> DETALLE_SERVICIO)
        {
            String registro = null;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_ven_p_facturacion_adicionar ";

            #region validaciones
            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = " + ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FACTURA)))
            {
                sql += "'" + FACTURA + "', ";
                informacion += "FACTURA = '" + FACTURA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FACTURA NOMINA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(REGIMEN)))
            {
                sql += "'" + REGIMEN + "', ";
                informacion += "REGIMEN = '" + REGIMEN.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGIMEN no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SOLO_DEV)))
            {
                sql += "'" + SOLO_DEV + "', ";
                informacion += "SOLO_DEV = '" + SOLO_DEV.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ADMINISTRACION SOLO DEVENGADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(VAC_PARAF)))
            {
                sql += "'" + VAC_PARAF + "', ";
                informacion += "VAC_PARAF = '" + VAC_PARAF.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo VACACIONES PARAFISCALES no puede ser nulo\n";
                ejecutar = false;
            }

            if (DIAS_VNC != 0)
            {
                sql += DIAS_VNC + ", ";
                informacion += "DIAS_VNC = " + DIAS_VNC.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo PLAZO FACTURA no puede ser 0\n";
                ejecutar = false;
            }

            if (AD_NOM != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AD_NOM) + ", ";
                informacion += "AD_NOM = " + AD_NOM.ToString() + ", ";
            }
            else
            {
                sql += "0, ";
                informacion += "AD_NOM = '" + AD_NOM.ToString() + "', ";
            }

            if (AD_PENSION != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AD_PENSION) + ", ";
                informacion += "AD_PENSION = " + AD_PENSION.ToString() + ", ";
            }
            else
            {
                sql += "0, ";
                informacion += "AD_PENSION = '" + AD_PENSION.ToString() + "', ";
            }

            if (AD_SALUD != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AD_SALUD) + ", ";
                informacion += "AD_SALUD = " + AD_SALUD.ToString() + ", ";
            }
            else
            {
                sql += "0, ";
                informacion += "AD_SALUD = '" + AD_SALUD.ToString() + "', ";
            }

            if (AD_RIESGOS != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AD_RIESGOS) + ", ";
                informacion += "AD_RIESGOS = " + AD_RIESGOS.ToString() + ", ";
            }
            else
            {
                sql += "0, ";
                informacion += "AD_RIESGOS = '" + AD_RIESGOS.ToString() + "', ";
            }

            if (AD_APO_SENA != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AD_APO_SENA) + ", ";
                informacion += "AD_RIESGOS = " + AD_APO_SENA.ToString() + ", ";
            }
            else
            {
                sql += "0, ";
                informacion += "AD_RIESGOS = '" + AD_APO_SENA.ToString() + "', ";
            }

            if (AD_APO_ICBF != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AD_APO_ICBF) + ", ";
                informacion += "AD_APO_ICBF = " + AD_APO_ICBF.ToString() + ", ";
            }
            else
            {
                sql += "0, ";
                informacion += "AD_APO_ICBF = '" + AD_APO_ICBF.ToString() + "', ";
            }

            if (AD_APO_CAJA != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AD_APO_CAJA) + ", ";
                informacion += "AD_APO_CAJA = " + AD_APO_CAJA.ToString() + ", ";
            }
            else
            {
                sql += "0, ";
                informacion += "AD_APO_CAJA = '" + AD_APO_CAJA.ToString() + "', ";
            }

            if (AD_VACACIONES != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AD_VACACIONES) + ", ";
                informacion += "AD_VACACIONES = " + AD_VACACIONES.ToString() + ", ";
            }
            else
            {
                sql += "0, ";
                informacion += "AD_VACACIONES = '" + AD_VACACIONES.ToString() + "', ";
            }

            if (AD_CESANTIA != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AD_CESANTIA) + ", ";
                informacion += "AD_CESANTIA = " + AD_CESANTIA.ToString() + ", ";
            }
            else
            {
                sql += "0, ";
                informacion += "AD_CESANTIA = '" + AD_CESANTIA.ToString() + "', ";
            }

            if (AD_INT_CES != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AD_INT_CES) + ", ";
                informacion += "AD_INT_CES = " + AD_INT_CES.ToString() + ", ";
            }
            else
            {
                sql += "0, ";
                informacion += "AD_INT_CES = '" + AD_INT_CES.ToString() + "', ";
            }

            if (AD_PRIMA != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AD_PRIMA) + ", ";
                informacion += "AD_PRIMA = " + AD_PRIMA.ToString() + ", ";
            }
            else
            {
                sql += "0, ";
                informacion += "AD_PRIMA = '" + AD_PRIMA.ToString() + "', ";
            }

            if (AD_SEG_VID != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AD_SEG_VID) + ", ";
                informacion += "AD_SEG_VID = " + AD_SEG_VID.ToString() + ", ";
            }
            else
            {
                sql += "0, ";
                informacion += "AD_SEG_VID = '" + AD_SEG_VID.ToString() + "', ";
            }

            if (!(String.IsNullOrEmpty(SUB_PENSION)))
            {
                sql += "'" + SUB_PENSION + "', ";
                informacion += "SUB_PENSION = '" + SUB_PENSION.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo INCLUYE SUBSIDIO DE TRANSPORTE PARA PENSION no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SUB_SALUD)))
            {
                sql += "'" + SUB_SALUD + "', ";
                informacion += "SUB_SALUD = '" + SUB_SALUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo INCLUYE SUBSIDIO DE TRANSPORTE PARA SALUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SUB_RIESGOS)))
            {
                sql += "'" + SUB_RIESGOS + "', ";
                informacion += "SUB_RIESGOS = '" + SUB_RIESGOS.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo INCLUYE SUBSIDIO DE TRANSPORTE PARA RIESGOS PROFESIONALES no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SUB_SENA)))
            {
                sql += "'" + SUB_SENA + "', ";
                informacion += "SUB_SENA = '" + SUB_SENA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo INCLUYE SUBSIDIO DE TRANSPORTE PARA SENA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SUB_ICBF)))
            {
                sql += "'" + SUB_ICBF + "', ";
                informacion += "SUB_ICBF = '" + SUB_ICBF.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo INCLUYE SUBSIDIO DE TRANSPORTE PARA ICBF no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SUB_CAJA)))
            {
                sql += "'" + SUB_CAJA + "', ";
                informacion += "SUB_CAJA = '" + SUB_CAJA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo INCLUYE SUBSIDIO DE TRANSPORTE PARA CAJA DE COMPENSACION FAMILIAR no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SUB_VACACIONES)))
            {
                sql += "'" + SUB_VACACIONES + "', ";
                informacion += "SUB_VACACIONES = '" + SUB_VACACIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo INCLUYE SUBSIDIO DE TRANSPORTE PARA VACACIONES no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SUB_CESANTIAS)))
            {
                sql += "'" + SUB_CESANTIAS + "', ";
                informacion += "SUB_CESANTIAS = '" + SUB_CESANTIAS.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo INCLUYE SUBSIDIO DE TRANSPORTE PARA CESANTIAS no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SUB_INT_CES)))
            {
                sql += "'" + SUB_INT_CES + "', ";
                informacion += "SUB_INT_CES = '" + SUB_INT_CES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo INCLUYE SUBSIDIO DE TRANSPORTE PARA INTERESES DE CESANTIAS no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SUB_PRIMA)))
            {
                sql += "'" + SUB_PRIMA + "', ";
                informacion += "SUB_PRIMA = '" + SUB_PRIMA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo INCLUYE SUBSIDIO DE TRANSPORTE PARA PRIMA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SUB_SEG_VID)))
            {
                sql += "'" + SUB_SEG_VID + "', ";
                informacion += "SUB_SEG_VID = '" + SUB_SEG_VID.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo INCLUYE SUBSIDIO DE TRANSPORTE PARA SEGURO DE VIDA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(MOD_SOPORTE)))
            {
                sql += "'" + MOD_SOPORTE + "', ";
                informacion += "MOD_SOPORTE = '" + MOD_SOPORTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo MODELOS DE SOPORTE no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(MOD_FACTURA)))
            {
                sql += "'" + MOD_FACTURA + "', ";
                informacion += "MOD_FACTURA = '" + MOD_FACTURA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo MODELOS DE FACTURA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(OBS_FACT)))
            {
                sql += "'" + OBS_FACT + "', ";
                informacion += "OBS_FACT = '" + OBS_FACT.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBS_FACT = 'null', ";
            }

            if (!(String.IsNullOrEmpty(RET_VAC)))
            {
                sql += "'" + RET_VAC + "', ";
                informacion += "RET_VAC = '" + RET_VAC.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SE COBRA AL RETIRO VACACIONES no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(RET_CES)))
            {
                sql += "'" + RET_CES + "', ";
                informacion += "RET_CES = '" + RET_CES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SE COBRA AL RETIRO CESANTIAS no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(RET_INT_CES)))
            {
                sql += "'" + RET_INT_CES + "', ";
                informacion += "RET_INT_CES = '" + RET_INT_CES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SE COBRA AL RETIRO INTERESES CESANTIAS no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(RET_PRIM)))
            {
                sql += "'" + RET_PRIM + "', ";
                informacion += "RET_PRIM = '" + RET_PRIM.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SE COBRA AL RETIRO PRIMA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(USU_CRE)))
            {
                sql += "'" + USU_CRE + "', ";
                informacion += "USU_CRE = '" + USU_CRE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo USUARIO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(APL_MTZ)))
            {
                sql += "'" + APL_MTZ + "', ";
                informacion += "APL_MTZ = '" + APL_MTZ.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo MONETIZACION no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_CIUDAD = Null, ";
            }

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C + ", ";
                informacion += "ID_CENTRO_C = " + ID_CENTRO_C.ToString() + ", ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_CENTRO_C = Null, ";
            }

            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C;
                informacion += "ID_SUB_C = " + ID_SUB_C.ToString();
            }
            else
            {
                sql += "null";
                informacion += "ID_SUB_C = Null";
            }

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    registro = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(USU_CRE, tabla.VEN_P_FACTURACION, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria

                    validarLista(SERVICIO, DETALLE_SERVICIO, ID_EMPRESA, ID_CIUDAD, ID_CENTRO_C, ID_SUB_C, conexion);

                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            if (!(String.IsNullOrEmpty(registro))) return Convert.ToDecimal(registro);
            else return 0;
        }

        public Boolean Actualizar(Decimal REGISTRO, Decimal ID_EMPRESA, String FACTURA, String REGIMEN, String SOLO_DEV, String VAC_PARAF, int DIAS_VNC,
            Decimal AD_NOM, Decimal AD_PENSION, Decimal AD_SALUD, Decimal AD_RIESGOS, Decimal AD_APO_SENA, Decimal AD_APO_ICBF,
            Decimal AD_APO_CAJA, Decimal AD_VACACIONES, Decimal AD_CESANTIA, Decimal AD_INT_CES, Decimal AD_PRIMA,
            Decimal AD_SEG_VID, String SUB_PENSION, String SUB_SALUD, String SUB_RIESGOS, String SUB_SENA, String SUB_ICBF,
            String SUB_CAJA, String SUB_VACACIONES, String SUB_CESANTIAS, String SUB_INT_CES, String SUB_PRIMA,
            String SUB_SEG_VID, String MOD_SOPORTE, String MOD_FACTURA, String OBS_FACT, String RET_VAC, String RET_CES,
            String RET_INT_CES, String RET_PRIM, String USU_MOD, String APL_MTZ, String ID_CIUDAD, Decimal ID_CENTRO_C, Decimal ID_SUB_C,
            List<servicio> SERVICIO, List<detalleServicio> DETALLE_SERVICIO)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_ven_p_facturacion_actualizar ";

            #region validaciones
            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO = " + REGISTRO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo REGISTRO no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = " + ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FACTURA)))
            {
                sql += "'" + FACTURA + "', ";
                informacion += "FACTURA = '" + FACTURA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo FACTURA NOMINA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(REGIMEN)))
            {
                sql += "'" + REGIMEN + "', ";
                informacion += "REGIMEN = '" + REGIMEN.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGIMEN no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SOLO_DEV)))
            {
                sql += "'" + SOLO_DEV + "', ";
                informacion += "SOLO_DEV = '" + SOLO_DEV.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ADMINISTRACION SOLO DEVENGADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(VAC_PARAF)))
            {
                sql += "'" + VAC_PARAF + "', ";
                informacion += "VAC_PARAF = '" + VAC_PARAF.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo VACACIONES PARAFISCALES no puede ser nulo\n";
                ejecutar = false;
            }

            if (DIAS_VNC != 0)
            {
                sql += DIAS_VNC + ", ";
                informacion += "DIAS_VNC = " + DIAS_VNC.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo PLAZO FACTURA no puede ser 0\n";
                ejecutar = false;
            }

            if (AD_NOM != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AD_NOM) + ", ";
                informacion += "AD_NOM = " + AD_NOM.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo PORCENTAJE ADMINISTRACION NOMINA no puede ser 0\n";
                ejecutar = false;
            }

            if (AD_PENSION != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AD_PENSION) + ", ";
                informacion += "AD_PENSION = " + AD_PENSION.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo PORCENTAJE ADMINISTRACION PENSION no puede ser 0\n";
                ejecutar = false;
            }

            if (AD_SALUD != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AD_SALUD) + ", ";
                informacion += "AD_SALUD = " + AD_SALUD.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo PORCENTAJE ADMINISTRACION SALUD no puede ser 0\n";
                ejecutar = false;
            }

            if (AD_RIESGOS != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AD_RIESGOS) + ", ";
                informacion += "AD_RIESGOS = " + AD_RIESGOS.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo PORCENTAJE ADMINISTRACION RIESGOS no puede ser 0\n";
                ejecutar = false;
            }

            if (AD_APO_SENA != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AD_APO_SENA) + ", ";
                informacion += "AD_RIESGOS = " + AD_APO_SENA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo PORCENTAJE APORTES SENA no puede ser 0\n";
                ejecutar = false;
            }

            if (AD_APO_ICBF != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AD_APO_ICBF) + ", ";
                informacion += "AD_APO_ICBF = " + AD_APO_ICBF.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo PORCENTAJE APORTES ICBF no puede ser 0\n";
                ejecutar = false;
            }

            if (AD_APO_CAJA != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AD_APO_CAJA) + ", ";
                informacion += "AD_APO_CAJA = " + AD_APO_CAJA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo PORCENTAJE APORTES CAJA DE COMPENSACION FAMILIAR no puede ser 0\n";
                ejecutar = false;
            }

            if (AD_VACACIONES != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AD_VACACIONES) + ", ";
                informacion += "AD_VACACIONES = " + AD_VACACIONES.ToString() + ", ";
            }
            else
            {
                sql += "0, ";
                informacion += "AD_VACACIONES = '0', ";
            }

            if (AD_CESANTIA != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AD_CESANTIA) + ", ";
                informacion += "AD_CESANTIA = " + AD_CESANTIA.ToString() + ", ";
            }
            else
            {
                sql += "0, ";
                informacion += "AD_CESANTIA = 'O', ";
            }

            if (AD_INT_CES != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AD_INT_CES) + ", ";
                informacion += "AD_INT_CES = " + AD_INT_CES.ToString() + ", ";
            }
            else
            {
                sql += "0, ";
                informacion += "AD_INT_CES = '" + AD_INT_CES.ToString() + "', ";
            }

            if (AD_PRIMA != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AD_PRIMA) + ", ";
                informacion += "AD_PRIMA = " + AD_PRIMA.ToString() + ", ";
            }
            else
            {
                sql += "0, ";
                informacion += "AD_PRIMA = '" + AD_PRIMA.ToString() + "', ";
            }

            if (AD_SEG_VID != 0)
            {
                sql += _tools.convierteComaEnPuntoParaDecimalesEnSQL(AD_SEG_VID) + ", ";
                informacion += "AD_SEG_VID = " + AD_SEG_VID.ToString() + ", ";
            }
            else
            {
                sql += "0, ";
                informacion += "AD_SEG_VID = '" + AD_SEG_VID.ToString() + "', ";
            }

            if (!(String.IsNullOrEmpty(SUB_PENSION)))
            {
                sql += "'" + SUB_PENSION + "', ";
                informacion += "SUB_PENSION = '" + SUB_PENSION.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo INCLUYE SUBSIDIO DE TRANSPORTE PARA PENSION no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SUB_SALUD)))
            {
                sql += "'" + SUB_SALUD + "', ";
                informacion += "SUB_SALUD = '" + SUB_SALUD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo INCLUYE SUBSIDIO DE TRANSPORTE PARA SALUD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SUB_RIESGOS)))
            {
                sql += "'" + SUB_RIESGOS + "', ";
                informacion += "SUB_RIESGOS = '" + SUB_RIESGOS.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo INCLUYE SUBSIDIO DE TRANSPORTE PARA RIESGOS PROFESIONALES no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SUB_SENA)))
            {
                sql += "'" + SUB_SENA + "', ";
                informacion += "SUB_SENA = '" + SUB_SENA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo INCLUYE SUBSIDIO DE TRANSPORTE PARA SENA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SUB_ICBF)))
            {
                sql += "'" + SUB_ICBF + "', ";
                informacion += "SUB_ICBF = '" + SUB_ICBF.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo INCLUYE SUBSIDIO DE TRANSPORTE PARA ICBF no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SUB_CAJA)))
            {
                sql += "'" + SUB_CAJA + "', ";
                informacion += "SUB_CAJA = '" + SUB_CAJA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo INCLUYE SUBSIDIO DE TRANSPORTE PARA CAJA DE COMPENSACION FAMILIAR no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SUB_VACACIONES)))
            {
                sql += "'" + SUB_VACACIONES + "', ";
                informacion += "SUB_VACACIONES = '" + SUB_VACACIONES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo INCLUYE SUBSIDIO DE TRANSPORTE PARA VACACIONES no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SUB_CESANTIAS)))
            {
                sql += "'" + SUB_CESANTIAS + "', ";
                informacion += "SUB_CESANTIAS = '" + SUB_CESANTIAS.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo INCLUYE SUBSIDIO DE TRANSPORTE PARA CESANTIAS no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SUB_INT_CES)))
            {
                sql += "'" + SUB_INT_CES + "', ";
                informacion += "SUB_INT_CES = '" + SUB_INT_CES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo INCLUYE SUBSIDIO DE TRANSPORTE PARA INTERESES DE CESANTIAS no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SUB_PRIMA)))
            {
                sql += "'" + SUB_PRIMA + "', ";
                informacion += "SUB_PRIMA = '" + SUB_PRIMA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo INCLUYE SUBSIDIO DE TRANSPORTE PARA PRIMA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SUB_SEG_VID)))
            {
                sql += "'" + SUB_SEG_VID + "', ";
                informacion += "SUB_SEG_VID = '" + SUB_SEG_VID.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo INCLUYE SUBSIDIO DE TRANSPORTE PARA SEGURO DE VIDA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(MOD_SOPORTE)))
            {
                sql += "'" + MOD_SOPORTE + "', ";
                informacion += "MOD_SOPORTE = '" + MOD_SOPORTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo MODELOS DE SOPORTE no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(MOD_FACTURA)))
            {
                sql += "'" + MOD_FACTURA + "', ";
                informacion += "MOD_FACTURA = '" + MOD_FACTURA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo MODELOS DE FACTURA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(OBS_FACT)))
            {
                sql += "'" + OBS_FACT + "', ";
                informacion += "OBS_FACT = '" + OBS_FACT.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "OBS_FACT = 'null', ";
            }

            if (!(String.IsNullOrEmpty(RET_VAC)))
            {
                sql += "'" + RET_VAC + "', ";
                informacion += "RET_VAC = '" + RET_VAC.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SE COBRA AL RETIRO VACACIONES no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(RET_CES)))
            {
                sql += "'" + RET_CES + "', ";
                informacion += "RET_CES = '" + RET_CES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SE COBRA AL RETIRO CESANTIAS no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(RET_INT_CES)))
            {
                sql += "'" + RET_INT_CES + "', ";
                informacion += "RET_INT_CES = '" + RET_INT_CES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SE COBRA AL RETIRO INTERESES CESANTIAS no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(RET_PRIM)))
            {
                sql += "'" + RET_PRIM + "', ";
                informacion += "RET_PRIM = '" + RET_PRIM.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SE COBRA AL RETIRO PRIMA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(USU_MOD)))
            {
                sql += "'" + USU_MOD + "', ";
                informacion += "USU_MOD = '" + USU_MOD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo USUARIO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(APL_MTZ)))
            {
                sql += "'" + APL_MTZ + "', ";
                informacion += "APL_MTZ = '" + APL_MTZ + "', ";
            }
            else
            {
                MensajeError += "El campo APL_MTZ no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_CIUDAD = Null, ";
            }

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C + ", ";
                informacion += "ID_CENTRO_C = " + ID_CENTRO_C.ToString() + ", ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_CENTRO_C = Null, ";
            }

            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C;
                informacion += "ID_SUB_C = " + ID_SUB_C.ToString();
            }
            else
            {
                sql += "null";
                informacion += "ID_SUB_C = Null";
            }

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(USU_MOD, tabla.VEN_P_FACTURACION, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                    validarLista(SERVICIO, DETALLE_SERVICIO, ID_EMPRESA, ID_CIUDAD, ID_CENTRO_C, ID_SUB_C, conexion);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public DataTable ObtenerCondicionesEconomicasPorId(Decimal ID_EMPRESA, String ID_CIUDAD, Decimal ID_CENTRO_C, Decimal ID_SUB_C)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_ven_p_facturacion_obtener_condiciones_por_id ";

            if (ID_EMPRESA == 0)
            {
                sql += "null, ";
            }
            else
            {
                sql += ID_EMPRESA.ToString() + ", ";
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == true)
            {
                sql += "null, ";
            }
            else
            {
                sql += "'" + ID_CIUDAD + "', ";
            }

            if (ID_CENTRO_C == 0)
            {
                sql += "0, ";
            }
            else
            {
                sql += ID_CENTRO_C.ToString() + ", ";
            }

            if (ID_SUB_C == 0)
            {
                sql += "0";
            }
            else
            {
                sql += ID_SUB_C.ToString();
            }

            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataView = _dataSet.Tables[0].DefaultView;
                _dataTable = _dataView.Table;
            }
            catch (Exception e)
            {
                MensajeError = e.Message;
            }
            finally
            {
                conexion.Desconectar();
            }

            return _dataTable;
        }
        #endregion condicionesComerciales
        public void validarLista(List<servicio> servicios, List<detalleServicio> detalleServicios, Decimal ID_EMPRESA, String ID_CIUDAD, Decimal ID_CENTRO_C, Decimal ID_SUB_C, Conexion datos)
        {
            Decimal idServicio = 0;
            Decimal idServicioPorEmpresa = 0;
            Decimal idDetalleServicio = 0;
            servicio servicio = new servicio(Empresa, Usuario);
            detalleServicio detalleservicio = new detalleServicio(Empresa, Usuario);

            foreach (servicio ser in servicios)
            {
                if (ser.ACCION.Equals("INSERTAR"))
                {
                    idServicio = ser.AdicionarServicio(ser.NOMBRE_SERVICIO, ser.AIU, ser.IVA, ser.VALOR, ser.DESCRIPCION, datos);

                    for (int i = 0; i < detalleServicios.Count; i++)
                    {
                        if (detalleServicios[i].NOMBRE_SERVICIO.Equals(ser.NOMBRE_SERVICIO))
                        {
                            detalleServicios[i].ID_SERVICIO = idServicio;
                        }
                    }
                    idServicioPorEmpresa = AdicionarServiciosPorEmpresa(idServicio, ID_EMPRESA, ID_CIUDAD, ID_CENTRO_C, ID_SUB_C, "S", datos);
                }
                if (ser.ACCION.Equals("NINGUNA"))
                {
                    servicio.ActualizarServicio(ser.ID_SERVICIO, ser.NOMBRE_SERVICIO, ser.AIU, ser.IVA, ser.VALOR, ser.DESCRIPCION, datos);
                }
                if (ser.ACCION.Equals("DESACTIVAR"))
                {
                    idServicioPorEmpresa = ObtenerServiciosPorEmpresaPorIdEmpresaIdServicio(ID_EMPRESA, ser.ID_SERVICIO, datos);
                    ActualizarServiciosPorEmpresa(idServicioPorEmpresa, ser.ID_SERVICIO, ID_EMPRESA, ID_CIUDAD, ID_CENTRO_C, ID_SUB_C, "N", datos);
                }
            }
            foreach (detalleServicio dSer in detalleServicios)
            {
                if (dSer.ACCION.Equals("INSERTAR"))
                {
                    detalleservicio.AdicionarDetalleServicio(dSer.ID_SERVICIO, dSer.ID_SERVICIO_COMPLEMENTARIO, dSer.AIU, dSer.IVA, dSer.VALOR, datos);
                }
                if (dSer.ACCION.Equals("DESACTIVAR"))
                {
                    idDetalleServicio = dSer.ObtenerDetalleServicioPorIdServicioIdServicioComplementario(dSer.ID_SERVICIO, dSer.ID_SERVICIO_COMPLEMENTARIO, datos);
                    detalleservicio.ActualizarDetalleServicio(idDetalleServicio, dSer.ID_SERVICIO, dSer.ID_SERVICIO_COMPLEMENTARIO, dSer.AIU, dSer.IVA, dSer.VALOR, "N", datos);
                }
            }
        }
        #region Servicios_Complementarios

        public DataTable ObtenerServiciosComplementariosPorTipo(String tipo)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_servicio_complementario_buscar_por_tipo ";

            if (!(String.IsNullOrEmpty(tipo)))
            {
                sql += "'" + tipo + "'";
                informacion += "tipo = '" + tipo.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo Tipo no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_SERVICIO_COMPLEMENTARIO, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return _dataTable;
        }
        public DataTable ObtenerServiciosComplementariosPorId(Decimal ID_SERVICIO_COMPLEMENTARIO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "USP_SERVICIO_COMPLEMENTARIO_BUSCAR_POR_ID ";

            if (ID_SERVICIO_COMPLEMENTARIO != 0) sql += ID_SERVICIO_COMPLEMENTARIO;
            else
            {
                MensajeError = "El campo ID_SERVICIO_COMPLEMENTARIO no puede ser 0\n";
                ejecutar = false;
            }

            if (ejecutar == true)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return _dataTable;
        }
        public Decimal AdicionarServiciosComplementarios(String Nombre_Servicio_Complemetario, String Tipo)
        {
            String sql = null;
            String ID_SERVICIO_COMPLEMENTARIO = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_servicio_complementario_adicionar ";

            #region validaciones

            if (!(String.IsNullOrEmpty(Nombre_Servicio_Complemetario)))
            {
                sql += "'" + Nombre_Servicio_Complemetario + "', ";
                informacion += "NOMBRE_SERVICIO_COMLEMENTARIO = '" + Nombre_Servicio_Complemetario.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE_SERVICIO_COMLEMENTARIO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(Tipo)))
            {
                sql += "'" + Tipo + "', ";
                informacion += "TIPO = '" + Tipo.ToString() + "' ";
            }
            else
            {
                MensajeError += "El campo TIPO no puede ser nulo\n";
                ejecutar = false;
            }


            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    ID_SERVICIO_COMPLEMENTARIO = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_SERVICIO_COMPLEMENTARIO, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            if (!(String.IsNullOrEmpty(ID_SERVICIO_COMPLEMENTARIO))) return Convert.ToDecimal(ID_SERVICIO_COMPLEMENTARIO);
            else return 0;
        }
        public Boolean ActualizarServiciosComplementarios(Decimal ID_SERVICIO_COMPLEMENTARIO, String NOMBRE_SERVICIO_COMPLEMENTARIO, String TIPO)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_ven_p_facturacion_actualizar ";

            #region validaciones
            if (ID_SERVICIO_COMPLEMENTARIO != 0)
            {
                sql += ID_SERVICIO_COMPLEMENTARIO + ", ";
                informacion += "ID_SERVICIO_COMPLEMENTARIO = " + ID_SERVICIO_COMPLEMENTARIO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_SERVICIO_COMPLEMENTARIO no puede ser 0\n";
                ejecutar = false;
            }


            if (!(String.IsNullOrEmpty(NOMBRE_SERVICIO_COMPLEMENTARIO)))
            {
                sql += "'" + NOMBRE_SERVICIO_COMPLEMENTARIO + "', ";
                informacion += "NOMBRE_SERVICIO_COMPLEMENTARIO = '" + NOMBRE_SERVICIO_COMPLEMENTARIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE_SERVICIO_COMPLEMENTARIO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO)))
            {
                sql += "'" + TIPO + "', ";
                informacion += "TIPO = '" + TIPO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO no puede ser nulo\n";
                ejecutar = false;
            }


            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_SERVICIO_COMPLEMENTARIO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        #endregion Servicios_Complementarios

        #region servicio_por_empresa

        public DataTable ObtenerServiciosPorEmpresaPorIdEmpresa(Decimal ID_EMPRESA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_servicio_por_empresa_buscar_por_id_empresa_activos ";

            if (ID_EMPRESA != 0)
            {
                sql += "'" + ID_EMPRESA + "'";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_SERVICIO_POR_EMPRESA, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return _dataTable;
        }

        public DataTable ObtenerServiciosPorEmpresaPorIdCiudad(String ID_CIUDAD, Decimal ID_EMPRESA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_servicio_por_empresa_buscar_por_id_ciudad_activos ";

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_EMPRESA != 0)
            {
                sql += "'" + ID_EMPRESA + "'";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_SERVICIO_POR_EMPRESA, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return _dataTable;
        }

        public DataTable ObtenerServiciosPorEmpresaPorIdCentroC(Decimal ID_CENTRO_C)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_servicio_por_empresa_buscar_por_id_centro_c_activos ";

            if (ID_CENTRO_C != 0)
            {
                sql += "'" + ID_CENTRO_C + "'";
                informacion += "ID_CENTRO_C = '" + ID_CENTRO_C.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_CENTRO_C no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_SERVICIO_POR_EMPRESA, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return _dataTable;
        }

        public DataTable ObtenerServiciosPorEmpresaPorIdSubC(Decimal ID_SUB_C)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_servicio_por_empresa_buscar_por_id_sub_c_activos ";

            if (ID_SUB_C != 0)
            {
                sql += "'" + ID_SUB_C + "'";
                informacion += "ID_SUB_C = '" + ID_SUB_C.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_SUB_C no puede ser nulo\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_SERVICIO_POR_EMPRESA, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return _dataTable;
        }

        public Decimal AdicionarServiciosPorEmpresa(Decimal ID_SERVICIO, Decimal ID_EMPRESA, String ID_CIUDAD, Decimal ID_CENTRO_C, Decimal ID_SUB_C, String ACTIVO, Conexion datos)
        {
            String sql = null;
            String id_servicio_por_empresa = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_servicio_por_empresa_adicionar ";

            #region validaciones


            if (ID_EMPRESA == 0)
            {
                sql += "null, ";
                informacion += "ID_EMPRESA = NULL, ";
            }
            else
            {
                sql += ID_EMPRESA.ToString() + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA.ToString() + "' ";
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == true)
            {
                sql += "null, ";
                informacion += "ID_CIUDAD = NULL, ";
            }
            else
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD.ToString() + "' ";
            }

            if (ID_CENTRO_C == 0)
            {
                sql += "null, ";
                informacion += "ID_CENTRO_C = NULL, ";
            }
            else
            {
                sql += ID_CENTRO_C.ToString() + ", ";
                informacion += "ID_CENTRO_C = '" + ID_CENTRO_C.ToString() + "' ";
            }

            if (ID_SUB_C == 0)
            {
                sql += "null, ";
                informacion += "ID_SUB_C = NULL, ";
            }
            else
            {
                sql += ID_SUB_C.ToString() + ", ";
                informacion += "ID_SUB_CENTRO = '" + ID_SUB_C.ToString() + "' ";
            }

            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO + ", ";
                informacion += "ID_SERVICIO = '" + ID_SERVICIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SERVICIO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + ACTIVO + "', ";
            informacion += "ACTIVO = '" + ACTIVO + "' ";

            if (ACTIVO.Equals("N"))
            {
                sql += "'" + System.DateTime.Now + "' ";
                informacion += "FECHA_INACTIVO = '" + System.DateTime.Now + "' ";
            }
            else
            {
                sql += "null";
                informacion += "FECHA_INACTIVO = null";

            }

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    id_servicio_por_empresa = datos.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_SERVICIO_POR_EMPRESA, tabla.ACCION_ADICIONAR, sql, informacion, datos);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (!(String.IsNullOrEmpty(id_servicio_por_empresa))) return Convert.ToDecimal(id_servicio_por_empresa);
            else return 0;
        }



        public Boolean ActualizarServiciosPorEmpresa(Decimal ID_SERVICIO_POR_EMPRESA, Decimal ID_SERVICIO, Decimal ID_EMPRESA, String ID_CIUDAD, Decimal ID_CENTRO_C, Decimal ID_SUB_C, String ACTIVO, Conexion datos)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_servicio_por_empresa_actualizar ";

            #region validaciones

            if (ID_SERVICIO_POR_EMPRESA != 0)
            {
                sql += ID_SERVICIO_POR_EMPRESA + ", ";
                informacion += "ID_SERVICIO_POR_EMPRESA = " + ID_SERVICIO_POR_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_SERVICIO_POR_EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_EMPRESA == 0)
            {
                sql += "null, ";
                informacion += "ID_EMPRESA = null, ";
            }
            else
            {
                sql += ID_EMPRESA.ToString() + ", ";
                informacion += "ID_EMPRESA = " + ID_EMPRESA.ToString() + ", ";
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == true)
            {
                sql += "null, ";
                informacion += "ID_CIUDAD = NULL, ";
            }
            else
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = " + ID_CIUDAD.ToString() + ", ";
            }

            if (ID_CENTRO_C == 0)
            {
                sql += "null, ";
                informacion += "ID_CENTRO_C = NULL, ";
            }
            else
            {
                sql += ID_CENTRO_C.ToString() + ", ";
                informacion += "ID_CENTRO_C = " + ID_CENTRO_C.ToString() + ", ";
            }

            if (ID_SUB_C == 0)
            {
                sql += "null, ";
                informacion += "ID_SUB_C = NULL, ";
            }
            else
            {
                sql += ID_SUB_C.ToString() + ", ";
                informacion += "ID_SUB_C = " + ID_SUB_C.ToString() + ", ";
            }

            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO + ", ";
                informacion += "ID_SERVICIO = " + ID_SERVICIO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_SERVICIO no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + ACTIVO + "',";
            informacion += "ACTIVO = '" + ACTIVO + "' ";

            if (ACTIVO.Equals("N"))
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(System.DateTime.Now) + "'";
                informacion += "FECHA_INACTIVO = '" + _tools.obtenerStringConFormatoFechaSQLServer(System.DateTime.Now) + "' ";
            }
            else
            {
                sql += "null";
                informacion += "FECHA_INACTIVO = null ";
            }
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_SERVICIO_POR_EMPRESA, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public Decimal ObtenerServiciosPorEmpresaPorIdEmpresaIdServicio(Decimal ID_EMPRESA, Decimal ID_SERVICIO, Conexion datos)
        {
            Decimal idServicioPorEmpresa = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_servicio_por_empresa_buscar_por_id_empresa_id_servicio ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO + ", ";
                informacion += "ID_SERVICIO = '" + ID_SERVICIO.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_SERVICIO no puede ser nulo\n";
                ejecutar = false;
            }
            sql += "null";


            if (ejecutar)
            {
                try
                {
                    idServicioPorEmpresa = Convert.ToDecimal(datos.ExecuteScalar(sql));
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_SERVICIO_POR_EMPRESA, tabla.ACCION_CONSULTAR, sql, informacion, datos);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }
            return idServicioPorEmpresa;

        }

        public DataTable obtenerSubCentrosParaReplicar(Decimal ID_EMPRESA, Decimal ID_SUB_C)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_servicio_complementario_subc_para_replicar ";

            #region validaciones
            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C;
            }
            else
            {
                MensajeError += "El campo ID_SUB_C no puede ser nulo\n";
                ejecutar = false;
            }
            #endregion

            if (ejecutar == true)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return _dataTable;
        }


        public DataTable obtenerUbicacionesPosiblesParaReplicar(Decimal ID_EMPRESA,
            Decimal ID_SUB_C,
            Decimal ID_CENTRO_C,
            String ID_CIUDAD,
            Decimal ID_SERVICIO,
            Boolean ES_SERTEMPO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_servicio_complementario_ObtenerUbicacionesPosiblesParaReplicar ";

            #region validaciones
            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C + ", ";
            }
            else
            {
                sql += "NULL, ";
            }

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C + ", ";
            }
            else
            {
                sql += "NULL, ";
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
            }
            else
            {
                sql += "NULL, ";
            }

            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO + ", ";
            }
            else
            {
                sql += "NULL, ";
            }

            if (ES_SERTEMPO == true)
            {
                sql += "'True'";
            }
            else
            {
                sql += "'False'";
            }
            #endregion

            if (ejecutar == true)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return _dataTable;
        }
        public DataTable obtenerCentrosCostoParaReplicar(Decimal ID_EMPRESA, Decimal ID_CENTRO_C)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_servicio_complementario_centroc_para_replicar ";

            #region validaciones
            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C;
            }
            else
            {
                MensajeError += "El campo ID_CENTRO_C no puede ser nulo\n";
                ejecutar = false;
            }
            #endregion

            if (ejecutar == true)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return _dataTable;
        }

        public DataTable obtenerCiudadesParaReplicar(Decimal ID_EMPRESA, String ID_CIUDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            Boolean ejecutar = true;

            sql = "usp_servicio_complementario_ciudades_para_replicar ";

            #region validaciones
            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "'";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo\n";
                ejecutar = false;
            }
            #endregion

            if (ejecutar == true)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return _dataTable;
        }

        #endregion servicio_por_empresa

        #endregion metodos
    }
}
