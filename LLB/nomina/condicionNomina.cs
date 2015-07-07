using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.nomina
{
    public class condicionNomina
    {
        #region variables
        public enum PeriodoNomina
        {
            Mensual = 1,
            Quincenal = 2,
            Semanal = 3,
            Catorcenal = 4
        }

        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;
        #endregion variables

        #region propiedades
        private String Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }
        public String Usuario
        {
            get { return _usuario; }
            set { _usuario = value; }
        }
        public String MensajeError
        {
            get { return _mensaje_error; }
            set { _mensaje_error = value; }
        }
        #endregion propiedades

        #region constructores
        public condicionNomina(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        public Decimal Adicionar(
            Decimal ID_EMPRESA,
            String ID_PERIODO_PAGO,
            String FECHA_PAGOS,
            Boolean PAG_SUB_TRANS_PERIDO_1,
            Boolean PAG_SUB_TRANS_PERIDO_2,
            Boolean PAG_SUB_TRANS_PERIDO_3,
            Boolean PAG_SUB_TRANS_PERIDO_4,
            DateTime FCH_INI_PRI_PER_NOM,
            Boolean CALC_PROM_DOMINICAL,
            Boolean AJUSTAR_SMLV,
            String BAS_HOR_EXT,
            Boolean MOSTRAR_UNIFICADA,
            Boolean DES_SEG_SOC_TRAB,
            String ID_CIUDAD,
            Decimal ID_CENTRO_C,
            Decimal ID_SUB_C,
            String CALCULO_RETENCION_FUENTE,
            Boolean PAGA_SUB_TRANSPORTE,
            Decimal PORCENTAJE_FACTURACION,
            List<incapadadConceptosNomina> incapadadesConceptosNomina,
            Boolean SABADO_NO_HABIL,
            Boolean PAGA_PARAF_FIN_MES,
            Boolean LIQUIDAR_ORDINARIAS_ULTIMO_PERIODO_MES,
            Boolean replicarCondiciones,
            Boolean LIQ_INCAP_SOBRE_BASICO_PRIMEROS_DIAS,
            string llavePeriodo,
            Boolean excluirExtrasDeSubt)
        {
            String sql = null;
            String REGISTRO = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;
            tools _tools = new tools();

            #region validaciones

            sql = "usp_ven_d_nomina_adicionar_V2 ";

            if (ID_EMPRESA != 0)
            {
                sql += "'" + ID_EMPRESA + "', ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser cero\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_PERIODO_PAGO)))
            {
                sql += "'" + ID_PERIODO_PAGO + "', ";
                informacion += "ID_PERIODO_PAGO = '" + ID_PERIODO_PAGO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PERIODO_PAGO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FECHA_PAGOS)))
            {
                sql += "'" + FECHA_PAGOS + "', ";
                informacion += "FECHA_PAGOS = '" + FECHA_PAGOS + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "FECHA_PAGOS = 'null', ";
            }

            sql += "'" + PAG_SUB_TRANS_PERIDO_1 + "', ";
            informacion += "PAG_SUB_TRANS_PERIDO_1 = '" + PAG_SUB_TRANS_PERIDO_1 + "', ";

            sql += "'" + PAG_SUB_TRANS_PERIDO_2 + "', ";
            informacion += "PAG_SUB_TRANS_PERIDO_2 = '" + PAG_SUB_TRANS_PERIDO_2 + "', ";

            sql += "'" + PAG_SUB_TRANS_PERIDO_3 + "', ";
            informacion += "PAG_SUB_TRANS_PERIDO_3 = '" + PAG_SUB_TRANS_PERIDO_3 + "', ";

            sql += "'" + PAG_SUB_TRANS_PERIDO_4 + "', ";
            informacion += "PAG_SUB_TRANS_PERIDO_4 = '" + PAG_SUB_TRANS_PERIDO_4 + "', ";

            if (FCH_INI_PRI_PER_NOM == new DateTime())
            {
                sql += "NULL, ";
                informacion += "FCH_INI_PRI_PER_NOM = 'NULL', ";
            }
            else
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_INI_PRI_PER_NOM) + "', ";
                informacion += "FCH_INI_PRI_PER_NOM = '" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_INI_PRI_PER_NOM) + "', ";
            }

            sql += "'" + CALC_PROM_DOMINICAL + "', ";
            informacion += "CALC_PROM_DOMINICAL = '" + CALC_PROM_DOMINICAL + "', ";

            sql += "'" + AJUSTAR_SMLV + "', ";
            informacion += "AJUSTAR_SMLV = '" + AJUSTAR_SMLV + "', ";

            if (!(String.IsNullOrEmpty(BAS_HOR_EXT)))
            {
                sql += "'" + BAS_HOR_EXT + "', ";
                informacion += "BAS_HOR_EXT = '" + BAS_HOR_EXT + "', ";
            }
            else
            {
                MensajeError += "El campo BAS_HOR_EXT no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + MOSTRAR_UNIFICADA + "', ";
            informacion += "MOSTRAR_UNIFICADA = '" + MOSTRAR_UNIFICADA + "', ";

            sql += "'" + DES_SEG_SOC_TRAB + "', ";
            informacion += "DES_SEG_SOC_TRAB = '" + DES_SEG_SOC_TRAB + "', ";

            if ((String.IsNullOrEmpty(ID_CIUDAD))
                || (ID_CIUDAD.Equals("0")))
            {
                sql += "Null, ";
                informacion += "ID_CIUDAD = 'Null', ";
            }
            else
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD + "', ";
            }

            sql += "'" + ID_CENTRO_C + "', ";
            informacion += "ID_CENTRO_C = '" + ID_CENTRO_C + "', ";

            sql += "'" + ID_SUB_C + "', ";
            informacion += "ID_SUB_C = '" + ID_SUB_C + "', ";

            if (!(String.IsNullOrEmpty(CALCULO_RETENCION_FUENTE)))
            {
                sql += "'" + CALCULO_RETENCION_FUENTE + "', ";
                informacion += "CALCULO_RETENCION_FUENTE = '" + CALCULO_RETENCION_FUENTE + "', ";
            }
            else
            {
                sql += "'Null', ";
                informacion += "CALCULO_RETENCION_FUENTE = 'Null', ";
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            sql += "'" + PAGA_SUB_TRANSPORTE + "',";
            informacion += "PAGA_SUB_TRANSPORTE = '" + PAGA_SUB_TRANSPORTE + "', ";

            sql += PORCENTAJE_FACTURACION.ToString() + ", ";
            informacion += "PORCENTAJE_FACTURACION = '" + PORCENTAJE_FACTURACION.ToString() + "', ";

            sql += "'" + SABADO_NO_HABIL + "', ";
            informacion += "SABADO_NO_HABIL = '" + SABADO_NO_HABIL + "', ";

            sql += "'" + PAGA_PARAF_FIN_MES + "', ";
            informacion += "PAGA_PARAF_FIN_MES = '" + PAGA_PARAF_FIN_MES + "', ";

            sql += "'" + LIQUIDAR_ORDINARIAS_ULTIMO_PERIODO_MES + "', ";
            informacion += "LIQUIDAR_ORDINARIAS_ULTIMO_PERIODO_MES = '" + LIQUIDAR_ORDINARIAS_ULTIMO_PERIODO_MES + "', ";

            sql += "'" + LIQ_INCAP_SOBRE_BASICO_PRIMEROS_DIAS + "', ";
            informacion += "LIQUIDAR_ORDINARIAS_ULTIMO_PERIODO_MES = '" + LIQ_INCAP_SOBRE_BASICO_PRIMEROS_DIAS + "', ";

            sql += "'" + llavePeriodo + "', ";
            informacion += "LLAVE_PERIODO = '" + llavePeriodo + "', ";

            if (excluirExtrasDeSubt == true)
            {
                sql += "'TRUE'";
                informacion += "EXCLUIR_EXTRAS_DE_SUBT = 'TRUE'";
            }
            else
            {
                sql += "'FALSE'";
                informacion += "EXCLUIR_EXTRAS_DE_SUBT = 'FALSE'";
            }
            #endregion validaciones

            Conexion conexion = new Conexion(Empresa);
            if (ejecutar)
            {
                conexion.IniciarTransaccion();
                try
                {
                    #region adicionar
                    REGISTRO = conexion.ExecuteScalar(sql);

                    #endregion adicionar

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    if (!(_auditoria.Adicionar(Usuario, tabla.VEN_D_NOMINA, tabla.ACCION_ADICIONAR, sql, informacion, conexion))) ejecutadoCorrectamente = false;
                    #endregion auditoria

                    #region incapacidad conceptos de nomina
                    foreach (incapadadConceptosNomina _incapadadConceptosNomina in incapadadesConceptosNomina)
                    {
                        if ((_incapadadConceptosNomina.Adicionar(
                            Convert.ToDecimal(REGISTRO),
                            _incapadadConceptosNomina.IdConcepto,
                            _incapadadConceptosNomina.Porcentaje, conexion)) == 0) ejecutadoCorrectamente = false;
                    }
                    #endregion incapacidad conceptos de nomina

                    #region replicacion
                    if ((replicarCondiciones == true) && (ID_SUB_C <= 0))
                    {
                        if (ReplicarParametrosnomina(ID_EMPRESA, ID_CIUDAD, ID_CENTRO_C, Convert.ToDecimal(REGISTRO), conexion) <= 0)
                        {
                            ejecutadoCorrectamente = false;
                        }
                    }
                    #endregion replicacion
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
            }

            if (ejecutadoCorrectamente)
            {
                conexion.AceptarTransaccion();
                conexion.Desconectar();
                return Convert.ToDecimal(REGISTRO);
            }
            else
            {
                conexion.DeshacerTransaccion();
                conexion.Desconectar();
                return 0;
            }
        }

        public Int32 ReplicarParametrosnomina(Decimal idEmpresa, String idCiudad, Decimal idCentroC, Decimal Registro, Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;
            String id = null;

            tools _tools = new tools();

            sql = "usp_ven_d_nomina_replicacion_parametros ";

            #region validaciones
            sql += idEmpresa.ToString() + ", ";

            if (String.IsNullOrEmpty(idCiudad) == false)
            {
                sql += "'" + idCiudad + "', ";
            }
            else
            {
                sql += "NULL, ";
            }

            sql += idCentroC + ", ";

            sql += Registro;

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    id = conexion.ExecuteScalar(sql);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutar = false;
                }
            }

            if (!(String.IsNullOrEmpty(id))) return Convert.ToInt32(id);
            else return 0;
        }

        public Boolean Actualizar(
            Decimal REGISTRO,
            String ID_PERIODO_PAGO,
            String FECHA_PAGOS,
            Boolean PAG_SUB_TRANS_PERIDO_1,
            Boolean PAG_SUB_TRANS_PERIDO_2,
            Boolean PAG_SUB_TRANS_PERIDO_3,
            Boolean PAG_SUB_TRANS_PERIDO_4,
            DateTime FCH_INI_PRI_PER_NOM,
            Boolean CALC_PROM_DOMINICAL,
            Boolean AJUSTAR_SMLV,
            String BAS_HOR_EXT,
            Boolean MOSTRAR_UNIFICADA,
            Boolean DES_SEG_SOC_TRAB,
            String ID_CIUDAD,
            Decimal ID_CENTRO_C,
            Decimal ID_SUB_C,
            String CALCULO_RETENCION_FUENTE,
            Boolean PAGA_SUB_TRANSPORTE,
            Decimal PORCENTAJE_FACTURACION,
            List<incapadadConceptosNomina> incapadadesConceptosNomina,
            Boolean SABADO_NO_HABIL,
            Boolean PAGA_PARAF_FIN_MES,
            Boolean LIQUIDAR_ORDINARIAS_ULTIMO_PERIODO_MES,
            Boolean REPLICAR_PARAMETROS,
            Decimal ID_EMPRESA,
            Boolean LIQ_INCAP_SOBRE_BASICO_PRIMEROS_DIAS,
            String llavePeriodo,
            Boolean excluirExtrasDeSubt)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;
            tools _tools = new tools();

            #region validaciones

            sql = "usp_ven_d_nomina_actualizar_V2 ";

            if (REGISTRO != 0)
            {
                sql += "'" + REGISTRO + "', ";
                informacion += "REGISTRO = '" + REGISTRO + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO no puede ser cero\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_PERIODO_PAGO)))
            {
                sql += "'" + ID_PERIODO_PAGO + "', ";
                informacion += "ID_PERIODO_PAGO = '" + ID_PERIODO_PAGO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PERIODO_PAGO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FECHA_PAGOS)))
            {
                sql += "'" + FECHA_PAGOS + "', ";
                informacion += "FECHA_PAGOS = '" + FECHA_PAGOS + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "FECHA_PAGOS = 'null', ";
            }

            sql += "'" + PAG_SUB_TRANS_PERIDO_1 + "', ";
            informacion += "PAG_SUB_TRANS_PERIDO_1 = '" + PAG_SUB_TRANS_PERIDO_1 + "', ";

            sql += "'" + PAG_SUB_TRANS_PERIDO_2 + "', ";
            informacion += "PAG_SUB_TRANS_PERIDO_2 = '" + PAG_SUB_TRANS_PERIDO_2 + "', ";

            sql += "'" + PAG_SUB_TRANS_PERIDO_3 + "', ";
            informacion += "PAG_SUB_TRANS_PERIDO_3 = '" + PAG_SUB_TRANS_PERIDO_3 + "', ";

            sql += "'" + PAG_SUB_TRANS_PERIDO_4 + "', ";
            informacion += "PAG_SUB_TRANS_PERIDO_4 = '" + PAG_SUB_TRANS_PERIDO_4 + "', ";

            if (FCH_INI_PRI_PER_NOM == new DateTime())
            {
                sql += "NULL, ";
                informacion += "FCH_INI_PRI_PER_NOM = 'NULL', ";
            }
            else
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_INI_PRI_PER_NOM) + "', ";
                informacion += "FCH_INI_PRI_PER_NOM = '" + _tools.obtenerStringConFormatoFechaSQLServer(FCH_INI_PRI_PER_NOM) + "', ";
            }

            sql += "'" + CALC_PROM_DOMINICAL + "', ";
            informacion += "CALC_PROM_DOMINICA = '" + CALC_PROM_DOMINICAL + "', ";

            sql += "'" + AJUSTAR_SMLV + "', ";
            informacion += "AJUSTAR_SMLV = '" + AJUSTAR_SMLV + "', ";

            if (!(String.IsNullOrEmpty(BAS_HOR_EXT)))
            {
                sql += "'" + BAS_HOR_EXT + "', ";
                informacion += "BAS_HOR_EXT = '" + BAS_HOR_EXT + "', ";
            }
            else
            {
                MensajeError += "El campo BAS_HOR_EXT no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + MOSTRAR_UNIFICADA + "', ";
            informacion += "MOSTRAR_UNIFICADA = '" + MOSTRAR_UNIFICADA + "', ";

            sql += "'" + DES_SEG_SOC_TRAB + "', ";
            informacion += "DES_SEG_SOC_TRAB = '" + DES_SEG_SOC_TRAB + "', ";

            if (!(String.IsNullOrEmpty(ID_CIUDAD))
                && !ID_CIUDAD.Equals("0"))
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_CIUDAD = 'Null', ";
            }

            if (ID_CENTRO_C != 0)
            {
                sql += "'" + ID_CENTRO_C + "', ";
                informacion += "ID_CENTRO_C = '" + ID_CENTRO_C + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_CENTRO_C = 'Null', ";
            }

            if (ID_SUB_C != 0)
            {
                sql += "'" + ID_SUB_C + "', ";
                informacion += "ID_SUB_C = '" + ID_SUB_C + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ID_SUB_C = 'Null', ";
            }

            if (!(String.IsNullOrEmpty(CALCULO_RETENCION_FUENTE)))
            {
                sql += "'" + CALCULO_RETENCION_FUENTE + "', ";
                informacion += "CALCULO_RETENCION_FUENTE = '" + CALCULO_RETENCION_FUENTE + "', ";
            }
            else
            {
                sql += "'Null', ";
                informacion += "CALCULO_RETENCION_FUENTE = 'Null', ";
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "', ";

            sql += "'" + PAGA_SUB_TRANSPORTE + "', ";
            informacion += "PAGA_SUB_TRANSPORTE = '" + PAGA_SUB_TRANSPORTE + "', ";

            sql += PORCENTAJE_FACTURACION.ToString() + ", ";
            informacion += "PORCENTAJE_FACTURACION = '" + PORCENTAJE_FACTURACION.ToString() + "', ";

            sql += "'" + SABADO_NO_HABIL + "', ";
            informacion += "SABADO_NO_HABIL = '" + SABADO_NO_HABIL + "', ";

            sql += "'" + PAGA_PARAF_FIN_MES + "', ";
            informacion += "PAGA_PARAF_FIN_MES = '" + PAGA_PARAF_FIN_MES + "', ";

            sql += "'" + LIQUIDAR_ORDINARIAS_ULTIMO_PERIODO_MES + "', ";
            informacion += "LIQUIDAR_ORDINARIAS_ULTIMO_PERIODO_MES = '" + LIQUIDAR_ORDINARIAS_ULTIMO_PERIODO_MES + "', ";

            sql += "'" + LIQ_INCAP_SOBRE_BASICO_PRIMEROS_DIAS + "', ";
            informacion += "LIQ_INCAP_SOBRE_BASICO_PRIMEROS_DIAS = '" + LIQ_INCAP_SOBRE_BASICO_PRIMEROS_DIAS + "', ";

            sql += "'" + llavePeriodo + "', ";
            informacion += "LLAVE_PERIODO = '" + llavePeriodo + "', ";

            if (excluirExtrasDeSubt == true)
            {
                sql += "'TRUE'";
                informacion += "EXCLUIR_EXTRAS_DE_SUBT = 'TRUE'";
            }
            else
            {
                sql += "'FALSE'";
                informacion += "EXCLUIR_EXTRAS_DE_SUBT = 'FALSE'";
            }

            #endregion validaciones

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            if (ejecutar)
            {
                try
                {
                    if (conexion.ExecuteNonQuery(sql) == 0)
                    {
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        #region auditoria
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.VEN_D_NOMINA, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                        {
                            ejecutadoCorrectamente = false;
                            MensajeError = _auditoria.MensajError;
                        }

                        #region incapacidad conceptos de nomina
                        incapadadConceptosNomina incapadadConceptosNomina = new incapadadConceptosNomina(Empresa, Usuario);
                        if (!incapadadConceptosNomina.Eliminar(REGISTRO, conexion))
                        {
                            ejecutadoCorrectamente = false;
                            MensajeError = incapadadConceptosNomina.MensajeError;
                        }

                        foreach (incapadadConceptosNomina _incapadadConceptosNomina in incapadadesConceptosNomina)
                        {
                            if ((_incapadadConceptosNomina.Adicionar(
                                Convert.ToDecimal(REGISTRO),
                                _incapadadConceptosNomina.IdConcepto,
                                _incapadadConceptosNomina.Porcentaje, conexion)) == 0)
                            {
                                ejecutadoCorrectamente = false;
                                MensajeError = _incapadadConceptosNomina.MensajeError;
                            }
                        }
                        #endregion incapacidad conceptos de nomina

                        #region replicacion
                        if (ReplicarParametrosnomina(ID_EMPRESA, ID_CIUDAD, ID_CENTRO_C, REGISTRO, conexion) <= 0)
                        {
                            ejecutadoCorrectamente = false;
                        }
                        #endregion replicacion
                    }
                        #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
            }

            if (ejecutadoCorrectamente)
            {
                conexion.AceptarTransaccion();
                conexion.Desconectar();
                return true;
            }
            else
            {
                conexion.DeshacerTransaccion();
                conexion.Desconectar();
                return false;
            }
        }

        public DataTable ObtenerPorIdEmpresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_ven_d_nomina_obtenerPorIdEmpresa '" + ID_EMPRESA + "'";

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

        public DataTable ObtenerPorIdEmpresaIdCiudad(Decimal ID_EMPRESA, String ID_CIUDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_ven_d_nomina_obtenerPorIdEmpresaIdCiudad "
                + ID_EMPRESA + ", "
                + "'" + ID_CIUDAD + "'";
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

        public DataTable ObtenerPorIdEmpresaIdCC(Decimal ID_EMPRESA, Decimal ID_CENTRO_C)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_ven_d_nomina_obtenerPorIdEmpresaIdCC "
                + ID_EMPRESA + ", "
                + ID_CENTRO_C;

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

        public DataTable ObtenerPorIdEmpresaIdSubCC(Decimal ID_EMPRESA, Decimal ID_SUB_C)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_ven_d_nomina_obtenerPorIdEmpresaIdSubCC "
                + ID_EMPRESA + ", "
                + ID_SUB_C;

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

        public Decimal ValidarPorcentajes(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            Decimal _porcentajes = 0;
            String sql = null;

            sql = "usp_ven_d_nomina_validar_porcentajes " + ID_EMPRESA.ToString() + "";

            try
            {
                _porcentajes = Convert.ToDecimal(conexion.ExecuteScalar(sql));
            }
            catch (Exception e)
            {
                MensajeError = e.Message;
            }
            finally
            {
                conexion.Desconectar();
            }

            return _porcentajes;
        }


        public DataTable ObtenerTodosLosPeriodosPago()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_par_periodos_pago_obtener_todos";

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

        public DataTable ObtenerDatosCOntrolPeriodosPorIDPeriodoPago(decimal idPeriodoPago)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_nom_control_periodos_obtener_activos_segun_id_periodo_pago " + idPeriodoPago.ToString();

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



        #endregion metodos

        #region reglas
        public DataTable obtenerPeriodosDescuentoSubTranportePorPeriodoPago(String periodoPago)
        {
            DataTable dataTable = new DataTable();
            parametro _parametro = new parametro(Empresa);
            if (periodoPago == Convert.ToInt16(PeriodoNomina.Mensual).ToString())
            {
                dataTable = _parametro.ObtenerParametrosPorTabla("DESCUENTO_SUB_TRANS_MENSUAL");
            }
            if (periodoPago == Convert.ToInt16(PeriodoNomina.Quincenal).ToString())
            {
                dataTable = _parametro.ObtenerParametrosPorTabla("DESCUENTO_SUB_TRANS_QUINCENAL");
            }
            if (periodoPago == Convert.ToInt16(PeriodoNomina.Catorcenal).ToString())
            {
                dataTable = _parametro.ObtenerParametrosPorTabla("DESCUENTO_SUB_TRANS_CATORCENAL");
            }
            if (periodoPago == Convert.ToInt16(PeriodoNomina.Semanal).ToString())
            {
                dataTable = _parametro.ObtenerParametrosPorTabla("DESCUENTO_SUB_TRANS_SEMANAL");
            }

            if (dataTable.Rows.Count > 0)
            {
                DataView dataView = dataTable.DefaultView;
                dataView.Sort = "codigo";
                dataTable = dataView.ToTable();
            }
            return dataTable;
        }
        #endregion reglas
    }
}