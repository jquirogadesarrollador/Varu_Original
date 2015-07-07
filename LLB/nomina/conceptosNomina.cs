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
    public class conceptosNomina
    {
        #region variables
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
        public conceptosNomina(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        public Boolean EliminarTarifasAsociadasAConcepto(Decimal idConcepto, Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_nom_tarifas_conceptos_empresas_eliminar_por_concepto ";
            sql += idConcepto;

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteScalar(sql);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if ((String.IsNullOrEmpty(MensajeError)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Int32 AdicionarTarifaConcepto(Decimal idConcepto, Decimal idEmpresa, String tarifa, Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            String id = null;

            tools _tools = new tools();

            sql = "usp_nom_tarifas_conceptos_empresas_adicionar ";
            sql += idConcepto + ", ";

            sql += idEmpresa + ", ";

            sql += tarifa.Replace(',', '.') + ", ";

            sql += "'" + Usuario + "'";

            if (ejecutar)
            {
                try
                {
                    id = conexion.ExecuteScalar(sql);

                    #region auditoria
                    if (id != null)
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        _auditoria.Adicionar(Usuario, tabla.VEN_D_NOMINA_INCAPACIDADES, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    }
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (!(String.IsNullOrEmpty(id)))
            {
                return Convert.ToInt32(id);
            }
            else
            {
                return 0;
            }
        }

        public Decimal Adicionar(String COD_CONCEPTO, String DESC_CONCEPTO, String DESC_ABREV, String TIPO_CONCEPTO,
            Int32 ID_IVA, Int32 ID_RETEFUENTE, Boolean BASE_SALARIO, Boolean BASE_FACTURACION,
            Boolean BASE_HERRAMIENTAS, Boolean COCEPTO_FIJO, Boolean BASE_AJUSTE_SMMLV, Boolean BASE_RETEFTE,
            Boolean APORTE_VOLUNTARIO_PENSION, Boolean BASE_RETEICA, String COD_CONTABLE, Boolean PAGO_CONDICIONAL,
            Boolean PAGO_PROPORCIONAL, Boolean BASE_PROMEDIO_DOMINICAL, Int32 ID_UNIDAD_REPORTE, Boolean ACTIVO,
            Int32 CANT_MAX_MES, Boolean INCAPACIDAD, Decimal PORCENTAJE, Boolean AUTOMATICO,
            Boolean AUSENTISMO, Boolean BASE_CALC_HORAS, Boolean LIQ_POR_VALOR, Boolean EXIGIR_CANTIDAD,
            Boolean BASE_PRIMA, Boolean BASE_CESANTIAS, Boolean BASE_VACACIONES, Boolean ANTICIPO_CESANTIAS,
            Boolean DESC_DIAS_LPS, Boolean DESC_TERCEROS, Decimal VALOR_MAX_MES, List<String> listaTarifas, Boolean POR_DESTAJO,
            Boolean EXCLUIR_CUARENTA,
            Boolean MULTIPLO_OCHO,
            Boolean autol_base_pension,
            Boolean autol_base_salud,
            Boolean autol_base_arl,
            Boolean autol_base_sena,
            Boolean autol_base_icbf,
            Boolean autol_base_caja,
            Boolean autol_dias_base,
            Boolean autol_dias_irp,
            Boolean autol_dias_eg,
            Boolean autol_cree,
            Boolean autol_ley_1393,
            Boolean autol_dias_sln,
            Boolean autol_licencia_maternidad,
            Boolean autol_base_salario,
            Boolean autol_dias_vacaciones_disfrutadas)
        {
            String sql = null;
            String ID = null;
            String informacion = null;
            Boolean ejecutar = true;
            tools _tools = new tools();
            Boolean ejecutadoCorrectamente = true;

            if (CodigoExiste(COD_CONCEPTO))
            {
                MensajeError += "El COD_CONCEPTO ya se encuentra registrado\n";
                ejecutar = false;
            }
            else
            {
                #region validaciones

                sql = "usp_nom_conceptos_nomina_adicionar_V2 ";

                if (!(String.IsNullOrEmpty(COD_CONCEPTO)))
                {
                    sql += "'" + COD_CONCEPTO + "', ";
                    informacion += "COD_CONCEPTO = '" + COD_CONCEPTO + "', ";
                }
                else
                {
                    MensajeError += "El campo COD_CONCEPTO no puede ser nulo\n";
                    ejecutar = false;
                }

                if (!(String.IsNullOrEmpty(DESC_CONCEPTO)))
                {
                    sql += "'" + DESC_CONCEPTO + "', ";
                    informacion += "DESC_CONCEPTO = '" + DESC_CONCEPTO + "', ";
                }
                else
                {
                    MensajeError += "El campo DESC_CONCEPTO no puede ser nulo\n";
                    ejecutar = false;
                }

                if (!(String.IsNullOrEmpty(DESC_ABREV)))
                {
                    sql += "'" + DESC_ABREV + "', ";
                    informacion += "DESC_ABREV = '" + DESC_ABREV + "', ";
                }
                else
                {
                    MensajeError += "El campo DESC_ABREV no puede ser nulo\n";
                    ejecutar = false;
                }

                if (!(String.IsNullOrEmpty(TIPO_CONCEPTO)))
                {
                    sql += "'" + TIPO_CONCEPTO + "', ";
                    informacion += "TIPO_CONCEPTO = '" + TIPO_CONCEPTO + "', ";
                }
                else
                {
                    MensajeError += "El campo TIPO_CONCEPTO no puede ser nulo\n";
                    ejecutar = false;
                }

                if (ID_IVA != 0)
                {
                    sql += "'" + ID_IVA + "', ";
                    informacion += "ID_IVA = '" + ID_IVA + "', ";
                }
                else
                {
                    sql += "Null, ";
                    informacion += "ID_IVA = 'Null', ";
                }

                if (ID_RETEFUENTE != 0)
                {
                    sql += "'" + ID_RETEFUENTE + "', ";
                    informacion += "ID_RETEFUENTE = '" + ID_RETEFUENTE + "', ";
                }
                else
                {
                    sql += "Null, ";
                    informacion += "ID_RETEFUENTE = 'Null', ";
                }

                if (BASE_SALARIO)
                {
                    sql += "1, ";
                    informacion += "BASE_SALARIO = 'True', ";
                }
                else
                {
                    sql += "0, ";
                    informacion += "BASE_SALARIO = 'False', ";
                }

                if (BASE_FACTURACION)
                {
                    sql += "1, ";
                    informacion += "BASE_FACTURACION = 'True', ";
                }
                else
                {
                    sql += "0, ";
                    informacion += "BASE_FACTURACION = 'False', ";
                }

                if (BASE_HERRAMIENTAS)
                {
                    sql += "1, ";
                    informacion += "BASE_HERRAMIENTAS = 'True', ";
                }
                else
                {
                    sql += "0, ";
                    informacion += "BASE_HERRAMIENTAS = 'False', ";
                }

                if (COCEPTO_FIJO)
                {
                    sql += "1, ";
                    informacion += "COCEPTO_FIJO = 'True', ";
                }
                else
                {
                    sql += "0, ";
                    informacion += "COCEPTO_FIJO = 'False', ";
                }

                if (BASE_AJUSTE_SMMLV)
                {
                    sql += "1, ";
                    informacion += "BASE_AJUSTE_SMMLV = 'True', ";
                }
                else
                {
                    sql += "0, ";
                    informacion += "BASE_AJUSTE_SMMLV = 'False', ";
                }

                if (BASE_RETEFTE)
                {
                    sql += "1, ";
                    informacion += "BASE_RETEFTE = 'True', ";
                }
                else
                {
                    sql += "0, ";
                    informacion += "BASE_RETEFTE = 'False', ";
                }

                if (APORTE_VOLUNTARIO_PENSION)
                {
                    sql += "1, ";
                    informacion += "APORTE_VOLUNTARIO_PENSION = 'True', ";
                }
                else
                {
                    sql += "0, ";
                    informacion += "APORTE_VOLUNTARIO_PENSION = 'False', ";
                }

                if (BASE_RETEICA)
                {
                    sql += "1, ";
                    informacion += "BASE_RETEICA = 'True', ";
                }
                else
                {
                    sql += "0, ";
                    informacion += "BASE_RETEICA = 'False', ";
                }

                if (!(String.IsNullOrEmpty(COD_CONTABLE)))
                {
                    sql += "'" + COD_CONTABLE + "', ";
                    informacion += "COD_CONTABLE = '" + COD_CONTABLE + "', ";
                }
                else
                {
                    MensajeError += "El campo COD_CONTABLE no puede ser nulo\n";
                    ejecutar = false;
                }

                if (PAGO_CONDICIONAL)
                {
                    sql += "1, ";
                    informacion += "PAGO_CONDICIONAL = 'True', ";
                }
                else
                {
                    sql += "0, ";
                    informacion += "PAGO_CONDICIONAL = 'False', ";
                }

                if (PAGO_PROPORCIONAL)
                {
                    sql += "1, ";
                    informacion += "PAGO_PROPORCIONAL = 'True', ";
                }
                else
                {
                    sql += "0, ";
                    informacion += "PAGO_PROPORCIONAL = 'False', ";
                }

                if (BASE_PROMEDIO_DOMINICAL)
                {
                    sql += "1, ";
                    informacion += "BASE_PROMEDIO_DOMINICAL = 'True', ";
                }
                else
                {
                    sql += "0, ";
                    informacion += "BASE_PROMEDIO_DOMINICAL = 'False', ";
                }


                if (ID_UNIDAD_REPORTE != 0)
                {
                    sql += "'" + ID_UNIDAD_REPORTE + "', ";
                    informacion += "ID_UNIDAD_REPORTE = '" + ID_UNIDAD_REPORTE + "', ";
                }
                else
                {
                    sql += "Null, ";
                    informacion += "ID_UNIDAD_REPORTE = 'Null', ";
                }

                if (ACTIVO)
                {
                    sql += "1, ";
                    informacion += "ACTIVO = 'True', ";
                }
                else
                {
                    sql += "0, ";
                    informacion += "ACTIVO = 'False', ";
                }

                if (CANT_MAX_MES != 0)
                {
                    sql += "'" + CANT_MAX_MES + "', ";
                    informacion += "CANT_MAX_MES = '" + CANT_MAX_MES + "', ";
                }
                else
                {
                    sql += "Null, ";
                    informacion += "CANT_MAX_MES = 'Null', ";
                }

                if (INCAPACIDAD)
                {
                    sql += "1, ";
                    informacion += "INCAPACIDAD = 'True', ";
                }
                else
                {
                    sql += "0, ";
                    informacion += "INCAPACIDAD = 'False', ";
                }

                if (PORCENTAJE != 0)
                {
                    sql += "'" + _tools.convierteComaEnPuntoParaDecimalesEnSQL(PORCENTAJE).ToString() + "', ";
                    informacion += "PORCENTAJE = '" + _tools.convierteComaEnPuntoParaDecimalesEnSQL(PORCENTAJE).ToString() + "', ";
                }
                else
                {
                    sql += "Null, ";
                    informacion += "PORCENTAJE = 'Null', ";
                }

                if (AUTOMATICO)
                {
                    sql += "1, ";
                    informacion += "AUTOMATICO = 'True', ";
                }
                else
                {
                    sql += "0, ";
                    informacion += "AUTOMATICO = 'False', ";
                }

                if (AUSENTISMO)
                {
                    sql += "1, ";
                    informacion += "AUSENTISMO = 'True', ";
                }
                else
                {
                    sql += "0, ";
                    informacion += "AUSENTISMO = 'False', ";
                }

                if (BASE_CALC_HORAS)
                {
                    sql += "1, ";
                    informacion += "BASE_CALC_HORAS = 'True', ";
                }
                else
                {
                    sql += "0, ";
                    informacion += "BASE_CALC_HORAS = 'False', ";
                }

                if (LIQ_POR_VALOR)
                {
                    sql += "1, ";
                    informacion += "LIQ_POR_VALOR = 'True', ";
                }
                else
                {
                    sql += "0, ";
                    informacion += "LIQ_POR_VALOR = 'False', ";
                }

                if (EXIGIR_CANTIDAD)
                {
                    sql += "1, ";
                    informacion += "EXIGIR_CANTIDAD = 'True', ";
                }
                else
                {
                    sql += "0, ";
                    informacion += "EXIGIR_CANTIDAD = 'False', ";
                }

                sql += "'" + Usuario + "', ";
                informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

                if (BASE_PRIMA)
                {
                    sql += "1, ";
                    informacion += "BASE_PRIMA = 'True', ";
                }
                else
                {
                    sql += "0, ";
                    informacion += "BASE_PRIMA = 'False', ";
                }

                if (BASE_CESANTIAS)
                {
                    sql += "1, ";
                    informacion += "BASE_CESANTIAS = 'True', ";
                }
                else
                {
                    sql += "0, ";
                    informacion += "BASE_CESANTIAS = 'False', ";
                }

                if (BASE_VACACIONES)
                {
                    sql += "1, ";
                    informacion += "BASE_VACACIONES = 'True', ";
                }
                else
                {
                    sql += "0, ";
                    informacion += "BASE_VACACIONES = 'False', ";
                }

                if (ANTICIPO_CESANTIAS)
                {
                    sql += "1, ";
                    informacion += "ANTICIPO_CESANTIAS = 'True', ";
                }
                else
                {
                    sql += "0, ";
                    informacion += "ANTICIPO_CESANTIAS = 'False', ";
                }

                if (DESC_DIAS_LPS)
                {
                    sql += "1, ";
                    informacion += "DESC_DIAS_LPS = 'True', ";
                }
                else
                {
                    sql += "0, ";
                    informacion += "DESC_DIAS_LPS = 'False', ";
                }

                if (DESC_TERCEROS)
                {
                    sql += "1, ";
                    informacion += "DESC_TERCEROS = 'True', ";
                }
                else
                {
                    sql += "0, ";
                    informacion += "DESC_TERCEROS = 'False', ";
                }

                if (VALOR_MAX_MES != 0)
                {
                    sql += "'" + VALOR_MAX_MES + "', ";
                    informacion += "VALOR_MAX_MES = '" + VALOR_MAX_MES + "', ";
                }
                else
                {
                    sql += "Null, ";
                    informacion += "VALOR_MAX_MES = 'Null', ";
                }

                if (POR_DESTAJO == false)
                {
                    sql += "'False', ";
                }
                else
                {
                    sql += "'True', ";
                }

                if (EXCLUIR_CUARENTA == false)
                {
                    sql += "'False', ";
                }
                else
                {
                    sql += "'True', ";
                }

                if (MULTIPLO_OCHO == false)
                {
                    sql += "'False', ";
                }
                else
                {
                    sql += "'True', ";
                }

                if (autol_base_pension == true)
                {
                    sql += "true, ";
                    informacion += "autol_base_pension = 'true', ";
                }
                else
                {
                    sql += "false, ";
                    informacion += "autol_base_pension = 'false', ";
                }

                if (autol_base_salud == true)
                {
                    sql += "true, ";
                    informacion += "autol_base_salud = 'true', ";
                }
                else
                {
                    sql += "false, ";
                    informacion += "autol_base_salud = 'false', ";
                }

                if (autol_base_arl == true)
                {
                    sql += "true, ";
                    informacion += "autol_base_arl = 'true', ";
                }
                else
                {
                    sql += "false, ";
                    informacion += "autol_base_arl = 'false', ";
                }

                if (autol_base_sena == true)
                {
                    sql += "true, ";
                    informacion += "autol_base_sena = 'true', ";
                }
                else
                {
                    sql += "false, ";
                    informacion += "autol_base_sena = 'false', ";
                }

                if (autol_base_icbf == true)
                {
                    sql += "true, ";
                    informacion += "autol_base_icbf = 'true', ";
                }
                else
                {
                    sql += "false, ";
                    informacion += "autol_base_icbf = 'false', ";
                }

                if (autol_base_caja == true)
                {
                    sql += "true, ";
                    informacion += "autol_base_caja = 'true', ";
                }
                else
                {
                    sql += "false, ";
                    informacion += "autol_base_caja = 'false', ";

                }
                if (autol_dias_base == true)
                {
                    sql += "true, ";
                    informacion += "autol_dias_base = 'true', ";
                }
                else
                {
                    sql += "false, ";
                    informacion += "autol_dias_base = 'false', ";
                }

                if (autol_dias_irp == true)
                {
                    sql += "true, ";
                    informacion += "autol_dias_irp = 'true', ";
                }
                else
                {
                    sql += "false, ";
                    informacion += "autol_dias_irp = 'false', ";
                }

                if (autol_dias_eg == true)
                {
                    sql += "true, ";
                    informacion += "autol_dias_eg = 'true', ";
                }
                else
                {
                    sql += "false, ";
                    informacion += "autol_dias_eg = 'false', ";

                }
                if (autol_cree == true)
                {
                    sql += "true, ";
                    informacion += "autol_cree = 'true', ";
                }
                else
                {
                    sql += "false, ";
                    informacion += "autol_cree = 'false', ";
                }

                if (autol_ley_1393 == true)
                {
                    sql += "true, ";
                    informacion += "autol_ley_1393 = 'true', ";
                }
                else
                {
                    sql += "false, ";
                    informacion += "autol_ley_1393 = 'false', ";
                }

                if (autol_dias_sln == true)
                {
                    sql += "true, ";
                    informacion += "autol_dias_sln = 'true', ";
                }
                else
                {
                    sql += "false, ";
                    informacion += "autol_dias_sln = 'false', ";
                }

                if (autol_licencia_maternidad == true)
                {
                    sql += "true, ";
                    informacion += "autol_licencia_maternidad = 'true', ";
                }
                else
                {
                    sql += "false, ";
                    informacion += "autol_licencia_maternidad = 'false', ";
                }

                if (autol_base_salario == true)
                {
                    sql += "true, ";
                    informacion += "autol_base_salario = 'true', ";
                }
                else
                {
                    sql += "false, ";
                    informacion += "autol_base_salario = 'false', ";
                }

                if (autol_dias_vacaciones_disfrutadas == true)
                {
                    sql += "true";
                    informacion += "autol_dias_vacaciones_disfrutadas = 'true'";
                }
                else
                {
                    sql += "false";
                    informacion += "autol_dias_vacaciones_disfrutadas = 'false'";
                }

                #endregion validaciones
            }

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();

                try
                {
                    #region adicionar
                    ID = conexion.ExecuteScalar(sql);
                    if (ID == null)
                    {
                        ejecutadoCorrectamente = false;
                    }
                    #endregion adicionar

                    #region auditoria
                    if (ejecutadoCorrectamente == true)
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.NOM_CONCEPTOS_NOMINA, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                        {
                            ejecutadoCorrectamente = false;
                        }
                    #endregion auditoria
                    }

                    #region TARIFAS
                    if (ejecutadoCorrectamente == true)
                    {
                        if (EliminarTarifasAsociadasAConcepto(Convert.ToDecimal(ID), conexion) == false)
                        {
                            ejecutadoCorrectamente = false;
                        }
                        else
                        {
                            foreach (String t in listaTarifas)
                            {
                                String[] infoTarifa = t.Split('-');

                                if (AdicionarTarifaConcepto(Convert.ToDecimal(ID), Convert.ToDecimal(infoTarifa[0]), infoTarifa[1], conexion) <= 0)
                                {
                                    ejecutadoCorrectamente = false;
                                    break;
                                }
                            }
                        }
                    }
                    #endregion TARIFAS

                    if (ejecutadoCorrectamente == true)
                    {
                        conexion.AceptarTransaccion();
                    }
                    else
                    {
                        conexion.DeshacerTransaccion();
                    }
                }
                catch (Exception e)
                {
                    conexion.DeshacerTransaccion();
                    MensajeError = e.Message;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            if (!(String.IsNullOrEmpty(ID)))
            {
                return Convert.ToDecimal(ID);
            }
            else
            {
                return 0;
            }
        }


        public Boolean Actualizar(Decimal ID_CONCEPTO, String COD_CONCEPTO, String DESC_CONCEPTO, String DESC_ABREV, String TIPO_CONCEPTO,
            Int32 ID_IVA, Int32 ID_RETEFUENTE, Boolean BASE_SALARIO, Boolean BASE_FACTURACION,
            Boolean BASE_HERRAMIENTAS, Boolean COCEPTO_FIJO, Boolean BASE_AJUSTE_SMMLV, Boolean BASE_RETEFTE,
            Boolean APORTE_VOLUNTARIO_PENSION, Boolean BASE_RETEICA, String COD_CONTABLE, Boolean PAGO_CONDICIONAL,
            Boolean PAGO_PROPORCIONAL, Boolean BASE_PROMEDIO_DOMINICAL, Int32 ID_UNIDAD_REPORTE, Boolean ACTIVO,
            Int32 CANT_MAX_MES, Boolean INCAPACIDAD, Decimal PORCENTAJE, Boolean AUTOMATICO,
            Boolean AUSENTISMO, Boolean BASE_CALC_HORAS, Boolean LIQ_POR_VALOR, Boolean EXIGIR_CANTIDAD,
            Boolean BASE_PRIMA, Boolean BASE_CESANTIAS, Boolean BASE_VACACIONES, Boolean ANTICIPO_CESANTIAS,
            Boolean DESC_DIAS_LPS, Boolean DESC_TERCEROS, Decimal VALOR_MAX_MES, List<String> listaTarifas, Boolean POR_DESTAJO, Boolean EXCLUIR_CALCULO_40,
            Boolean MULTIPLO_OCHO,
            Boolean autol_base_pension,
            Boolean autol_base_salud,
            Boolean autol_base_arl,
            Boolean autol_base_sena,
            Boolean autol_base_icbf,
            Boolean autol_base_caja,
            Boolean autol_dias_base,
            Boolean autol_dias_irp,
            Boolean autol_dias_eg,
            Boolean autol_cree,
            Boolean autol_ley_1393,
            Boolean autol_dias_sln,
            Boolean autol_licencia_maternidad,
            Boolean autol_base_salario,
            Boolean autol_dias_vacaciones_disfrutadas)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;
            tools _tools = new tools();

            sql = "usp_nom_conceptos_nomina_actualizar_V2 ";

            #region validaciones

            if (ID_CONCEPTO != 0)
            {
                sql += "'" + ID_CONCEPTO + "', ";
                informacion += "ID_CONCEPTO = '" + ID_CONCEPTO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CONCEPTO no puede ser cero\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(COD_CONCEPTO)))
            {
                sql += "'" + COD_CONCEPTO + "', ";
                informacion += "COD_CONCEPTO = '" + COD_CONCEPTO + "', ";
            }
            else
            {
                MensajeError += "El campo COD_CONCEPTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DESC_CONCEPTO)))
            {
                sql += "'" + DESC_CONCEPTO + "', ";
                informacion += "DESC_CONCEPTO = '" + DESC_CONCEPTO + "', ";
            }
            else
            {
                MensajeError += "El campo DESC_CONCEPTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DESC_ABREV)))
            {
                sql += "'" + DESC_ABREV + "', ";
                informacion += "DESC_ABREV = '" + DESC_ABREV + "', ";
            }
            else
            {
                MensajeError += "El campo DESC_ABREV no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO_CONCEPTO)))
            {
                sql += "'" + TIPO_CONCEPTO + "', ";
                informacion += "TIPO_CONCEPTO = '" + TIPO_CONCEPTO + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO_CONCEPTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_IVA != 0)
            {
                sql += "'" + ID_IVA + "', ";
                informacion += "ID_IVA = '" + ID_IVA + "', ";
            }
            else
            {
                sql += "Null, ";
                informacion += "ID_IVA = 'Null', ";
            }

            if (ID_RETEFUENTE != 0)
            {
                sql += "'" + ID_RETEFUENTE + "', ";
                informacion += "ID_RETEFUENTE = '" + ID_RETEFUENTE + "', ";
            }
            else
            {
                sql += "Null, ";
                informacion += "ID_RETEFUENTE = 'Null', ";
            }

            if (BASE_SALARIO)
            {
                sql += "1, ";
                informacion += "BASE_SALARIO = 'True', ";
            }
            else
            {
                sql += "0, ";
                informacion += "BASE_SALARIO = 'False', ";
            }

            if (BASE_FACTURACION)
            {
                sql += "1, ";
                informacion += "BASE_FACTURACION = 'True', ";
            }
            else
            {
                sql += "0, ";
                informacion += "BASE_FACTURACION = 'False', ";
            }

            if (BASE_HERRAMIENTAS)
            {
                sql += "1, ";
                informacion += "BASE_HERRAMIENTAS = 'True', ";
            }
            else
            {
                sql += "0, ";
                informacion += "BASE_HERRAMIENTAS = 'False', ";
            }

            if (COCEPTO_FIJO)
            {
                sql += "1, ";
                informacion += "COCEPTO_FIJO = 'True', ";
            }
            else
            {
                sql += "0, ";
                informacion += "COCEPTO_FIJO = 'False', ";
            }

            if (BASE_AJUSTE_SMMLV)
            {
                sql += "1, ";
                informacion += "BASE_AJUSTE_SMMLV = 'True', ";
            }
            else
            {
                sql += "0, ";
                informacion += "BASE_AJUSTE_SMMLV = 'False', ";
            }

            if (BASE_RETEFTE)
            {
                sql += "1, ";
                informacion += "BASE_RETEFTE = 'True', ";
            }
            else
            {
                sql += "0, ";
                informacion += "BASE_RETEFTE = 'False', ";
            }

            if (APORTE_VOLUNTARIO_PENSION)
            {
                sql += "1, ";
                informacion += "APORTE_VOLUNTARIO_PENSION = 'True', ";
            }
            else
            {
                sql += "0, ";
                informacion += "APORTE_VOLUNTARIO_PENSION = 'False', ";
            }

            if (BASE_RETEICA)
            {
                sql += "1, ";
                informacion += "BASE_RETEICA = 'True', ";
            }
            else
            {
                sql += "0, ";
                informacion += "BASE_RETEICA = 'False', ";
            }

            if (!(String.IsNullOrEmpty(COD_CONTABLE)))
            {
                sql += "'" + COD_CONTABLE + "', ";
                informacion += "COD_CONTABLE = '" + COD_CONTABLE + "', ";
            }
            else
            {
                MensajeError += "El campo COD_CONTABLE no puede ser nulo\n";
                ejecutar = false;
            }

            if (PAGO_CONDICIONAL)
            {
                sql += "1, ";
                informacion += "PAGO_CONDICIONAL = 'True', ";
            }
            else
            {
                sql += "0, ";
                informacion += "PAGO_CONDICIONAL = 'False', ";
            }

            if (PAGO_PROPORCIONAL)
            {
                sql += "1, ";
                informacion += "PAGO_PROPORCIONAL = 'True', ";
            }
            else
            {
                sql += "0, ";
                informacion += "PAGO_PROPORCIONAL = 'False', ";
            }

            if (BASE_PROMEDIO_DOMINICAL)
            {
                sql += "1, ";
                informacion += "BASE_PROMEDIO_DOMINICAL = 'True', ";
            }
            else
            {
                sql += "0, ";
                informacion += "BASE_PROMEDIO_DOMINICAL = 'False', ";
            }

            if (ID_UNIDAD_REPORTE != 0)
            {
                sql += "'" + ID_UNIDAD_REPORTE + "', ";
                informacion += "ID_UNIDAD_REPORTE = '" + ID_UNIDAD_REPORTE + "', ";
            }
            else
            {
                sql += "Null, ";
                informacion += "ID_UNIDAD_REPORTE = 'Null', ";
            }

            if (ACTIVO)
            {
                sql += "1, ";
                informacion += "ACTIVO = 'True', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ACTIVO = 'False', ";
            }

            if (CANT_MAX_MES != 0)
            {
                sql += "'" + CANT_MAX_MES + "', ";
                informacion += "CANT_MAX_MES = '" + CANT_MAX_MES + "', ";
            }
            else
            {
                sql += "Null, ";
                informacion += "CANT_MAX_MES = 'Null', ";
            }

            if (INCAPACIDAD)
            {
                sql += "1, ";
                informacion += "INCAPACIDAD = 'True', ";
            }
            else
            {
                sql += "0, ";
                informacion += "INCAPACIDAD = 'False', ";
            }

            if (PORCENTAJE != 0)
            {
                sql += "'" + _tools.convierteComaEnPuntoParaDecimalesEnSQL(PORCENTAJE).ToString() + "', ";
                informacion += "PORCENTAJE = '" + _tools.convierteComaEnPuntoParaDecimalesEnSQL(PORCENTAJE).ToString() + "', ";
            }
            else
            {
                sql += "Null, ";
                informacion += "PORCENTAJE = 'Null', ";
            }

            if (AUTOMATICO)
            {
                sql += "1, ";
                informacion += "AUTOMATICO = 'True', ";
            }
            else
            {
                sql += "0, ";
                informacion += "AUTOMATICO = 'False', ";
            }

            if (AUSENTISMO)
            {
                sql += "1, ";
                informacion += "AUSENTISMO = 'True', ";
            }
            else
            {
                sql += "0, ";
                informacion += "AUSENTISMO = 'False', ";
            }

            if (BASE_CALC_HORAS)
            {
                sql += "1, ";
                informacion += "BASE_CALC_HORAS = 'True', ";
            }
            else
            {
                sql += "0, ";
                informacion += "BASE_CALC_HORAS = 'False', ";
            }

            if (LIQ_POR_VALOR)
            {
                sql += "1, ";
                informacion += "LIQ_POR_VALOR = 'True', ";
            }
            else
            {
                sql += "0, ";
                informacion += "LIQ_POR_VALOR = 'False', ";
            }

            if (EXIGIR_CANTIDAD)
            {
                sql += "1, ";
                informacion += "EXIGIR_CANTIDAD = 'True', ";
            }
            else
            {
                sql += "0, ";
                informacion += "EXIGIR_CANTIDAD = 'False', ";
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (BASE_PRIMA)
            {
                sql += "1, ";
                informacion += "BASE_PRIMA = 'True', ";
            }
            else
            {
                sql += "0, ";
                informacion += "BASE_PRIMA = 'False', ";
            }

            if (BASE_CESANTIAS)
            {
                sql += "1, ";
                informacion += "BASE_CESANTIAS = 'True', ";
            }
            else
            {
                sql += "0, ";
                informacion += "BASE_CESANTIAS = 'False', ";
            }

            if (BASE_VACACIONES)
            {
                sql += "1, ";
                informacion += "BASE_VACACIONES = 'True', ";
            }
            else
            {
                sql += "0, ";
                informacion += "BASE_VACACIONES = 'False', ";
            }

            if (ANTICIPO_CESANTIAS)
            {
                sql += "1, ";
                informacion += "ANTICIPO_CESANTIAS = 'True', ";
            }
            else
            {
                sql += "0, ";
                informacion += "ANTICIPO_CESANTIAS = 'False', ";
            }

            if (DESC_DIAS_LPS)
            {
                sql += "1, ";
                informacion += "DESC_DIAS_LPS = 'True', ";
            }
            else
            {
                sql += "0, ";
                informacion += "DESC_DIAS_LPS = 'False', ";
            }

            if (DESC_TERCEROS)
            {
                sql += "1, ";
                informacion += "DESC_TERCEROS = 'True', ";
            }
            else
            {
                sql += "0, ";
                informacion += "DESC_TERCEROS = 'False', ";
            }

            if (VALOR_MAX_MES != 0)
            {
                sql += "'" + VALOR_MAX_MES + "', ";
                informacion += "VALOR_MAX_MES = '" + VALOR_MAX_MES + "', ";
            }
            else
            {
                sql += "Null, ";
                informacion += "VALOR_MAX_MES = 'Null', ";
            }

            if (POR_DESTAJO == false)
            {
                sql += "'False', ";
            }
            else
            {
                sql += "'True', ";
            }

            if (EXCLUIR_CALCULO_40 == false)
            {
                sql += "'False', ";
            }
            else
            {
                sql += "'True', ";
            }

            if (MULTIPLO_OCHO == false)
            {
                sql += "'False', ";
            }
            else
            {
                sql += "'True', ";
            }

            if (autol_base_pension == true)
            {
                sql += "true, ";
                informacion += "autol_base_pension = 'true', ";
            }
            else
            {
                sql += "false, ";
                informacion += "autol_base_pension = 'false', ";
            }

            if (autol_base_salud == true)
            {
                sql += "true, ";
                informacion += "autol_base_salud = 'true', ";
            }
            else
            {
                sql += "false, ";
                informacion += "autol_base_salud = 'false', ";
            }

            if (autol_base_arl == true)
            {
                sql += "true, ";
                informacion += "autol_base_arl = 'true', ";
            }
            else
            {
                sql += "false, ";
                informacion += "autol_base_arl = 'false', ";
            }

            if (autol_base_sena == true)
            {
                sql += "true, ";
                informacion += "autol_base_sena = 'true', ";
            }
            else
            {
                sql += "false, ";
                informacion += "autol_base_sena = 'false', ";
            }

            if (autol_base_icbf == true)
            {
                sql += "true, ";
                informacion += "autol_base_icbf = 'true', ";
            }
            else
            {
                sql += "false, ";
                informacion += "autol_base_icbf = 'false', ";
            }

            if (autol_base_caja == true)
            {
                sql += "true, ";
                informacion += "autol_base_caja = 'true', ";
            }
            else
            {
                sql += "false, ";
                informacion += "autol_base_caja = 'false', ";

            }
            if (autol_dias_base == true)
            {
                sql += "true, ";
                informacion += "autol_dias_base = 'true', ";
            }
            else
            {
                sql += "false, ";
                informacion += "autol_dias_base = 'false', ";
            }

            if (autol_dias_irp == true)
            {
                sql += "true, ";
                informacion += "autol_dias_irp = 'true', ";
            }
            else
            {
                sql += "false, ";
                informacion += "autol_dias_irp = 'false', ";
            }

            if (autol_dias_eg == true)
            {
                sql += "true, ";
                informacion += "autol_dias_eg = 'true', ";
            }
            else
            {
                sql += "false, ";
                informacion += "autol_dias_eg = 'false', ";

            }
            if (autol_cree == true)
            {
                sql += "true, ";
                informacion += "autol_cree = 'true', ";
            }
            else
            {
                sql += "false, ";
                informacion += "autol_cree = 'false', ";
            }

            if (autol_ley_1393 == true)
            {
                sql += "true, ";
                informacion += "autol_ley_1393 = 'true', ";
            }
            else
            {
                sql += "false, ";
                informacion += "autol_ley_1393 = 'false', ";
            }

            if (autol_dias_sln == true)
            {
                sql += "true, ";
                informacion += "autol_dias_sln = 'true', ";
            }
            else
            {
                sql += "false, ";
                informacion += "autol_dias_sln = 'false', ";
            }

            if (autol_licencia_maternidad == true)
            {
                sql += "true, ";
                informacion += "autol_licencia_maternidad = 'true', ";
            }
            else
            {
                sql += "false, ";
                informacion += "autol_licencia_maternidad = 'false', ";
            }

            if (autol_base_salario == true)
            {
                sql += "true, ";
                informacion += "autol_base_salario = 'true', ";
            }
            else
            {
                sql += "false, ";
                informacion += "autol_base_salario = 'false', ";
            }

            if (autol_dias_vacaciones_disfrutadas == true)
            {
                sql += "true";
                informacion += "autol_dias_vacaciones_disfrutadas = 'true'";
            }
            else
            {
                sql += "false";
                informacion += "autol_dias_vacaciones_disfrutadas = 'false'";
            }

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    #region actualizar
                    if (conexion.ExecuteNonQuery(sql) == 0)
                    {
                        ejecutadoCorrectamente = false;
                    }
                    #endregion actualizar

                    #region auditoria
                    if (ejecutadoCorrectamente == true)
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.NOM_CONCEPTOS_NOMINA, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                        {
                            ejecutadoCorrectamente = false;
                        }
                    }
                    #endregion auditoria

                    #region TARIFAS
                    if (ejecutadoCorrectamente == true)
                    {
                        if (EliminarTarifasAsociadasAConcepto(Convert.ToDecimal(ID_CONCEPTO), conexion) == false)
                        {
                            ejecutadoCorrectamente = false;
                        }
                        else
                        {
                            foreach (String t in listaTarifas)
                            {
                                String[] infoTarifa = t.Split('-');

                                if (AdicionarTarifaConcepto(ID_CONCEPTO, Convert.ToDecimal(infoTarifa[0]), infoTarifa[1], conexion) <= 0)
                                {
                                    ejecutadoCorrectamente = false;
                                    break;
                                }
                            }
                        }
                    }
                    #endregion TARIFAS

                    if (ejecutadoCorrectamente == true)
                    {
                        conexion.AceptarTransaccion();
                    }
                    else
                    {
                        conexion.DeshacerTransaccion();
                    }
                }
                catch (Exception e)
                {
                    conexion.DeshacerTransaccion();
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
            }

            if (ejecutadoCorrectamente)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataTable ObtenerPorCodigo(String COD_CONCEPTO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;

            sql = "usp_nom_conceptos_nomina_ObtenerPorCodigo '" + COD_CONCEPTO + "'";

            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataView = _dataSet.Tables[0].DefaultView;
                _dataTable = _dataView.Table;

                #region auditoria
                auditoria _auditoria = new auditoria(Empresa);
                _auditoria.Adicionar(Usuario, tabla.NOM_CONCEPTOS_NOMINA, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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

            return _dataTable;
        }

        public DataTable ObtenerPorNombre(String DESC_CONCEPTO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;

            sql = "usp_nom_conceptos_nomina_ObtenerPorNombre '" + DESC_CONCEPTO + "'";

            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataView = _dataSet.Tables[0].DefaultView;
                _dataTable = _dataView.Table;

                #region auditoria
                auditoria _auditoria = new auditoria(Empresa);
                _auditoria.Adicionar(Usuario, tabla.NOM_CONCEPTOS_NOMINA, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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

            return _dataTable;
        }

        public DataTable ObtenerPorId(Decimal ID_CONCEPTO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;

            sql = "usp_nom_conceptos_nomina_ObtenerPorId '" + ID_CONCEPTO + "'";

            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataView = _dataSet.Tables[0].DefaultView;
                _dataTable = _dataView.Table;

                #region auditoria
                auditoria _auditoria = new auditoria(Empresa);
                _auditoria.Adicionar(Usuario, tabla.NOM_CONCEPTOS_NOMINA, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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

            return _dataTable;
        }

        public DataTable ObtenerConceptosDeIncapacidad()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_nom_conceptos_nomina_ObtenerConceptosDeIncapacidad ";

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

        public DataTable ObtenerConceptosDeAusentismo()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_nom_conceptos_nomina_ObtenerConceptosDeAusentismo ";

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

        public DataTable ObtenerConceptosFijos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_nom_conceptos_nomina_ObtenerConceptosFijos ";

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

        public DataTable ObtenerTarifasPorConcepto(Decimal idConcepto)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_nom_tarifas_conceptos_empresas_obtener_por_concepto ";
            sql += idConcepto;

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
        private Boolean CodigoExiste(String COD_CONCEPTO)
        {
            DataTable _dataTable = ObtenerPorCodigo(COD_CONCEPTO);
            if (_dataTable.Rows.Count > 0) return true;
            else return false;
        }
        #endregion reglas
    }
}
