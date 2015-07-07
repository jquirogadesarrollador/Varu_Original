using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;
using System.IO;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seleccion;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB.comercial;

namespace Brainsbits.LLB.contabilidad
{
    public class contabilidad
    {
        #region varialbes
        String _empresa = null;
        String _mensaje_error = null;
        String _usuario = null;
        private Int32 _num_recaja = 0;
        private Int32 _num_factura = 0;
        private Int32 _num_nota = 0;
        #endregion varialbes

        #region propiedades
        public String Empresa
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

        public Int32 Num_Recaja
        {
            get { return _num_recaja; }
            set { _num_recaja = value; }
        }

        public Int32 Num_Factura
        {
            get { return _num_factura; }
            set { _num_factura = value; }
        }
        public Int32 Num_Nota
        {
            get { return _num_nota; }
            set { _num_nota = value; }
        }
        #endregion propiedades

        #region constructores
        public contabilidad(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        public DataTable ObtenerDatosHojaTrabajoContabilidad(String FILTRO, String VALOR, String TIPO, Decimal ID_EMPRESA = 0)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_datos_contabilizacion ";

            sql += "'" + FILTRO + "', '" + VALOR + "', '" + TIPO + "', '" + ID_EMPRESA.ToString() + "'";

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

        public DataTable ObtenerDatosHojaTrabajoContabilidadMasivo(String FILTRO, String VALOR)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_datos_contabilizacion_masivo ";

            sql += "'" + FILTRO + "', '" + VALOR + "'";

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

        public DataTable ObtenerDatosHojaTrabajoContabilidadNominas(String FILTRO, String VALOR, String PERCONT)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_datos_contabilizacionNominas ";

            sql += "'" + FILTRO + "', '" + VALOR + "', '" + PERCONT + "'";

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

        public DataTable ObtenerDatosHojaTrabajoReversarContabilidad(String FILTRO, String VALOR)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_datos_reversar_contabilizacion ";

            sql += "'" + FILTRO + "', '" + VALOR + "'";

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

        public DataTable ObtenerDatosArchivoPlano(Decimal ID_EMPRESA, Int32 PERIODO, String TIPO_PERIODO, String MARCADOR)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_datos_plano_nomina_contabilidad ";

            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            if (PERIODO != 0)
            {
                sql += PERIODO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo PERIODO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO_PERIODO)))
            {
                sql += "'" + TIPO_PERIODO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo TIPO_PERIODO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(MARCADOR)))
            {
                sql += "'" + MARCADOR.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo TIPO_PERIODO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(Usuario)))
            {
                sql += "'" + Usuario.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo Usuario es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + tabla.VAR_NIT_SERTEMPO_CONTABLE.ToString() + "', ";
            sql += "'" + tabla.VAR_NIT_SENA.ToString() + "', ";
            sql += "'" + tabla.VAR_NIT_ICBF.ToString() + "'";

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


        public DataTable GenerarPlanoMasivo(Decimal ID_EMPRESA, Int32 ANIO, Int32 MES, String NOMB_MES, Int32 CONSECUTIVO, String GASTOS, String TIPO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_datos_plano_masivo_nomina_contabilidad ";

            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                sql += "0, ";
            }

            if (ANIO != 0)
            {
                sql += ANIO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo AÑO es requerido para la consulta.";
                ejecutar = false;
            }

            if (MES != 0)
            {
                sql += MES.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo MES es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOMB_MES)))
            {
                sql += "'" + NOMB_MES.ToString() + "', ";
            }
            else
            {
                MensajeError = "Debe definir el nombre del mes de contabilizacion que va a realizar.";
                ejecutar = false;
            }

            if (CONSECUTIVO != 0)
            {
                if (CONSECUTIVO < 99)
                {
                    sql += CONSECUTIVO.ToString() + ", ";
                }
                else
                {
                    MensajeError = "El campo CONSECUTIVO no debe ser mayor de 99.";
                    ejecutar = false;
                }
            }
            else
            {
                MensajeError = "El campo CONSECUTIVO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(GASTOS)))
            {
                sql += "'" + GASTOS.ToString() + "', ";
            }
            else
            {
                MensajeError = "La exclusion de nomina de gastos debe ser definida.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO)))
            {
                sql += "'" + TIPO.ToString() + "', ";
            }
            else
            {
                MensajeError = "Debe definir el tipo de contabilizacion que va a realizar.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(Usuario)))
            {
                sql += "'" + Usuario.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo Usuario es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + tabla.VAR_NIT_SERTEMPO_CONTABLE.ToString() + "', ";
            sql += "'" + tabla.VAR_NIT_SENA.ToString() + "', ";
            sql += "'" + tabla.VAR_NIT_ICBF.ToString() + "'";

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

        public DataTable GenerarDetallePlanoMasivo(Decimal ID_EMPRESA, Int32 ANIO, Int32 MES, String GASTOS, String TIPO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_detalle_datos_plano_masivo_nomina_contabilidad ";

            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                sql += "0, ";
            }

            if (ANIO != 0)
            {
                sql += ANIO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo AÑO es requerido para la consulta.";
                ejecutar = false;
            }

            if (MES != 0)
            {
                sql += MES.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo MES es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(GASTOS)))
            {
                sql += "'" + GASTOS.ToString() + "', ";
            }
            else
            {
                MensajeError = "La exclusion de nomina de gastos debe ser definida.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO)))
            {
                sql += "'" + TIPO.ToString() + "', ";
            }
            else
            {
                MensajeError = "Debe definir el tipo de contabilizacion que va a realizar.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(Usuario)))
            {
                sql += "'" + Usuario.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo Usuario es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + tabla.VAR_NIT_SERTEMPO_CONTABLE.ToString() + "', ";
            sql += "'" + tabla.VAR_NIT_SENA.ToString() + "', ";
            sql += "'" + tabla.VAR_NIT_ICBF.ToString() + "'";

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

        public DataTable GenerarConsolidadoPlanoMasivo(Decimal ID_EMPRESA, Int32 ANIO, Int32 MES, String GASTOS, String TIPO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_consolidado_datos_plano_masivo_nomina_contabilidad ";

            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                sql += "0, ";
            }

            if (ANIO != 0)
            {
                sql += ANIO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo AÑO es requerido para la consulta.";
                ejecutar = false;
            }

            if (MES != 0)
            {
                sql += MES.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo MES es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(GASTOS)))
            {
                sql += "'" + GASTOS.ToString() + "', ";
            }
            else
            {
                MensajeError = "La exclusion de nomina de gastos debe ser definida.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO)))
            {
                sql += "'" + TIPO.ToString() + "', ";
            }
            else
            {
                MensajeError = "Debe definir el tipo de contabilizacion que va a realizar.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(Usuario)))
            {
                sql += "'" + Usuario.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo Usuario es requerido para la consulta.";
                ejecutar = false;
            }

            sql += "'" + tabla.VAR_NIT_SERTEMPO_CONTABLE.ToString() + "', ";
            sql += "'" + tabla.VAR_NIT_SENA.ToString() + "', ";
            sql += "'" + tabla.VAR_NIT_ICBF.ToString() + "'";

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


        public DataTable ObtenerDatosArchivoPlanoDetalle(Decimal ID_EMPRESA, Int32 PERIODO, String TIPO_PERIODO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_datos_plano_nomina_contabilidad_resumen ";

            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            if (PERIODO != 0)
            {
                sql += PERIODO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo PERIODO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO_PERIODO)))
            {
                sql += "'" + TIPO_PERIODO.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo TIPO_PERIODO es requerido para la consulta.";
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

        public DataTable ObtenerDatosArchivoPlanoArl(Decimal ID_EMPRESA, Int32 PERIODO, String TIPO_PERIODO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_datos_plano_nomina_contabilidad_arl ";

            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            if (PERIODO != 0)
            {
                sql += PERIODO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo PERIODO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO_PERIODO)))
            {
                sql += "'" + TIPO_PERIODO.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo TIPO_PERIODO es requerido para la consulta.";
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

        public DataTable ObtenerDatosArchivoPlanoProvisiones(Decimal ID_EMPRESA, Int32 PERIODO, String TIPO_PERIODO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_datos_plano_nomina_contabilidad_provisiones ";

            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            if (PERIODO != 0)
            {
                sql += PERIODO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo PERIODO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO_PERIODO)))
            {
                sql += "'" + TIPO_PERIODO.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo TIPO_PERIODO es requerido para la consulta.";
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


        public DataTable ObtenerDatosHojaTrabajoReversar(Int32 APROBACION)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_datos_periodo_reversar_contabilizacion ";

            if (APROBACION > 0)
            {
                sql += APROBACION.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo APROBACION es requerido para la consulta.";
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

        public String PlanoPeriodoRegistrar(Decimal ID_EMPRESA, Int32 PERIODO, String TIPO_PERIODO, String ARCHIVO, String MARCADOR)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            Boolean ejecutar = true;
            String Plano = "N";

            sql = "usp_datos_plano_contabilidad_registrar ";

            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            if (PERIODO > 0)
            {
                sql += PERIODO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo PERIODO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO_PERIODO)))
            {
                sql += "'" + TIPO_PERIODO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo TIPO_PERIODO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ARCHIVO)))
            {
                sql += "'" + ARCHIVO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ARCHIVO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(MARCADOR)))
            {
                sql += "'" + MARCADOR.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ARCHIVO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(Usuario)))
            {
                sql += "'" + Usuario.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo Usuario es requerido para la consulta.";
                ejecutar = false;
            }

            if (ejecutar == true)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);
                    Plano = "S";
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
            return Plano;
        }

        public String ValidarPlanoContabilidad(Decimal ID_EMPRESA, Int32 PERIODO, String TIPO_PERIODO, String APROBACION, String NOM_PLANO)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            Boolean ejecutar = true;
            String Validado = "N";

            sql = "usp_validar_plano_contabilidad ";

            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            if (PERIODO != 0)
            {
                sql += PERIODO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo PERIODO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TIPO_PERIODO)))
            {
                sql += "'" + TIPO_PERIODO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo TIPO_PERIODO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(APROBACION)))
            {
                sql += "'" + APROBACION.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo APROBACION es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOM_PLANO)))
            {
                sql += "'" + NOM_PLANO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo NOM_PLANO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(Usuario)))
            {
                sql += "'" + Usuario.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo Usuario es requerido para la consulta.";
                ejecutar = false;
            }

            if (ejecutar == true)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);
                    Validado = "S";
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
            return Validado;
        }


        public String ReversarContabilizacion(Decimal APROBACION)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            Boolean ejecutar = true;
            String Reversado = "N";

            sql = "usp_reversar_contabilizacion ";

            if (APROBACION > 0)
            {
                sql += APROBACION.ToString() + "";
            }
            else
            {
                MensajeError = "El campo APROBACION es requerido para la consulta.";
                ejecutar = false;
            }

            if (ejecutar == true)
            {
                try
                {
                    Reversado = Convert.ToString(conexion.ExecuteScalar(sql));
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
            return Reversado;
        }

        public DataTable CargarPeriodosEmpresa(Int32 ID_EMPRESA, String Reporte)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_periodo_contable_memos_empresa ";

            if (ID_EMPRESA > 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(Reporte)))
            {
                sql += "'" + Reporte.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo Reporte es requerido para la consulta.";
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

        public DataTable CargarRegional(String tipo, String filtro, String filtro_reg)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_traer_regional_por_filtro ";

            if (!(String.IsNullOrEmpty(tipo)))
            {
                sql += "'" + tipo.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo tipo es requerido para la consulta.";
                ejecutar = false;
            }


            sql += "'" + filtro.ToString() + "', ";

            sql += "'" + filtro_reg.ToString() + "'";

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


        public DataTable ObtenerDatosPagosContabilidad(String FILTRO, String VALOR, String CONTAB)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_datos_contabilizacion_pagos ";

            sql += "'" + FILTRO + "', '" + VALOR + "', '" + CONTAB + "'";

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

        public String ObtenerConsecutivoEgreso()
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            Boolean ejecutar = true;
            String Egreso = "0";

            sql = "usp_traer_consecutivo_egreso ";

            if (ejecutar == true)
            {
                try
                {
                    Egreso = Convert.ToString(conexion.ExecuteScalar(sql));
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
            return Egreso;
        }

        public DataTable ObtenerEmpresasPagoContabilidad(Int32 EGRESO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_traer_empresas_pago_contabilidad ";

            sql += EGRESO + "";

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


        public DataTable ObtenerDatosArchivoPlanoPagos(Int32 PAGO, Int32 EGRESO, DateTime FECHA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;
            tools fecha = new tools();

            sql = "usp_datos_plano_pagos_contabilidad ";

            if (PAGO > 0)
            {
                sql += PAGO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo PAGO es requerido para la consulta.";
                ejecutar = false;
            }

            if (EGRESO > 0)
            {
                sql += EGRESO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo EGRESO es requerido para la consulta.";
                ejecutar = false;
            }
            sql += "'" + fecha.obtenerStringConFormatoFechaSQLServer(FECHA) + "'";

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

        public String ContabilizarPagos(Int32 ID_PAGOS, Int32 EGRESO, String PLANO)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            Boolean ejecutar = true;
            String Contabilizado = "N";

            sql = "usp_contabilizar_pago ";

            if (ID_PAGOS > 0)
            {
                sql += ID_PAGOS.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PAGOS es requerido para la consulta.";
                ejecutar = false;
            }

            if (EGRESO > 0)
            {
                sql += EGRESO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo EGRESO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(PLANO)))
            {
                sql += "'" + PLANO.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo PLANO es requerido para la consulta.";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(Usuario)))
            {
                sql += "'" + Usuario.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo Usuario es requerido para la consulta.";
                ejecutar = false;
            }

            if (ejecutar == true)
            {
                try
                {
                    Contabilizado = Convert.ToString(conexion.ExecuteScalar(sql));
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
            return Contabilizado;
        }

        public String ReversarContabilizacionPagos(Int32 ID_PAGOS)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            Boolean ejecutar = true;
            String Reversado = "N";

            sql = "usp_reversar_contabilizar_pago ";

            if (ID_PAGOS > 0)
            {
                sql += ID_PAGOS.ToString() + "";
            }
            else
            {
                MensajeError = "El campo ID_PAGOS es requerido para la consulta.";
                ejecutar = false;
            }


            if (ejecutar == true)
            {
                try
                {
                    Reversado = Convert.ToString(conexion.ExecuteScalar(sql));
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
            return Reversado;
        }

        public DataTable ObtenerDatosHojaTrabajoContabilidadRecibosCaja(String FILTRO, String VALOR, String FECHA_INICIAL, String FECHA_FINAL, String CONTABILIZADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            tools _tools = new tools();


            sql = "usp_datos_contabilizacion_recibos_de_caja ";

            sql += "'" + FILTRO + "', '" + VALOR + "', '" + FECHA_INICIAL.ToString() + "', '" + FECHA_FINAL.ToString() + "', '" + CONTABILIZADO + "'";

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

        public DataTable ObtenerDatosArchivoPlanoRecibosCaja(IList<contabilidad> recibos_caja, String ANIO, String MES)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String RECIBOS_CAJA = "";
            String sql = null;

            sql = "usp_datos_plano_recibos_caja_contabilidad ";

            if (recibos_caja.Count > 0)
            {
                foreach (contabilidad recaja in recibos_caja)
                {
                    if (RECIBOS_CAJA.Length > 0)
                    {
                        RECIBOS_CAJA += ",";
                    }
                    RECIBOS_CAJA += recaja.Num_Recaja.ToString();
                }
            }

            sql += "'" + RECIBOS_CAJA.ToString() + "', '" + ANIO + "', '" + MES + "', ";
            sql += "'" + Usuario.ToString() + "'";

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

        public String ContabilizarRecibosCaja(IList<contabilidad> recibos_caja, String ARCHIVO_PLANO)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            Boolean ejecutar = true;
            String Contabilizado = "N";
            String RECIBOS_CAJA = "";

            sql = "usp_contabilizar_recibos_caja ";

            if (recibos_caja.Count > 0)
            {
                foreach (contabilidad recaja in recibos_caja)
                {
                    if (RECIBOS_CAJA.Length > 0)
                    {
                        RECIBOS_CAJA += ",";
                    }
                    RECIBOS_CAJA += recaja.Num_Recaja.ToString();
                }
            }

            sql += "'" + RECIBOS_CAJA.ToString() + "', ";
            sql += "'" + ARCHIVO_PLANO.ToString() + "', ";
            sql += "'" + Usuario.ToString() + "'";

            if (ejecutar == true)
            {
                try
                {
                    Contabilizado = Convert.ToString(conexion.ExecuteScalar(sql));
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
            return Contabilizado;
        }

        public String ReversarContabilizacionRecibosCaja(String PLANO)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            Boolean ejecutar = true;
            String Reversado = "N";

            sql = "usp_reversar_contabilizar_recibos_caja ";

            sql += "'" + PLANO.ToString() + "'";

            if (ejecutar == true)
            {
                try
                {
                    Reversado = Convert.ToString(conexion.ExecuteScalar(sql));
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
            return Reversado;
        }

        public DataTable ObtenerDatosHojaTrabajoContabilidadFacturas(String FILTRO, String VALOR, String FECHA_INICIAL, String FECHA_FINAL, String CONTABILIZADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            tools _tools = new tools();


            sql = "usp_datos_contabilizacion_facturas ";

            sql += "'" + FILTRO + "', '" + VALOR + "', '" + FECHA_INICIAL.ToString() + "', '" + FECHA_FINAL.ToString() + "', '" + CONTABILIZADO + "'";

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

        public DataTable ObtenerDatosArchivoPlanoFacturas(IList<contabilidad> facturas, String ANIO, String MES)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String FACTURAS = "";
            String sql = null;

            sql = "usp_datos_plano_facturas_contabilidad ";

            if (facturas.Count > 0)
            {
                foreach (contabilidad _facturas in facturas)
                {
                    if (FACTURAS.Length > 0)
                    {
                        FACTURAS += ",";
                    }
                    FACTURAS += _facturas.Num_Factura.ToString();
                }
            }

            sql += "'" + FACTURAS.ToString() + "', '" + ANIO + "', '" + MES + "', ";
            sql += "'" + Usuario.ToString() + "'";

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

        public String ContabilizarFacturas(IList<contabilidad> recibos_caja, String ARCHIVO_PLANO)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            Boolean ejecutar = true;
            String Contabilizado = "N";
            String RECIBOS_CAJA = "";

            sql = "usp_contabilizar_facturas ";

            if (recibos_caja.Count > 0)
            {
                foreach (contabilidad recaja in recibos_caja)
                {
                    if (RECIBOS_CAJA.Length > 0)
                    {
                        RECIBOS_CAJA += ",";
                    }
                    RECIBOS_CAJA += recaja.Num_Recaja.ToString();
                }
            }

            sql += "'" + RECIBOS_CAJA.ToString() + "', ";
            sql += "'" + ARCHIVO_PLANO.ToString() + "', ";
            sql += "'" + Usuario.ToString() + "'";

            if (ejecutar == true)
            {
                try
                {
                    Contabilizado = Convert.ToString(conexion.ExecuteScalar(sql));
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
            return Contabilizado;
        }

        public String ReversarContabilizacionFacturas(String PLANO)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            Boolean ejecutar = true;
            String Reversado = "N";

            sql = "usp_reversar_contabilizar_facturas ";

            sql += "'" + PLANO.ToString() + "'";

            if (ejecutar == true)
            {
                try
                {
                    Reversado = Convert.ToString(conexion.ExecuteScalar(sql));
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
            return Reversado;
        }



        public DataTable ObtenerDatosHojaTrabajoContabilidadNotas(String FILTRO, String VALOR, String FECHA_INICIAL, String FECHA_FINAL, String CONTABILIZADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            tools _tools = new tools();

            sql = "usp_datos_contabilizacion_facturas ";

            sql += "'" + FILTRO + "', '" + VALOR + "', '" + FECHA_INICIAL.ToString() + "', '" + FECHA_FINAL.ToString() + "', '" + CONTABILIZADO + "'";

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

        public DataTable ObtenerDatosArchivoPlanoNotas(IList<contabilidad> notas, String ANIO, String MES)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String NOTAS = "";
            String sql = null;

            sql = "usp_datos_plano_notas_contabilidad ";

            if (notas.Count > 0)
            {
                foreach (contabilidad _notas in notas)
                {
                    if (NOTAS.Length > 0)
                    {
                        NOTAS += ",";
                    }
                    NOTAS += _notas.Num_Nota.ToString();
                }
            }

            sql += "'" + NOTAS.ToString() + "', '" + ANIO + "', '" + MES + "', ";
            sql += "'" + Usuario.ToString() + "'";

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

        public String ContabilizarNotas(IList<contabilidad> notas, String ARCHIVO_PLANO)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            Boolean ejecutar = true;
            String Contabilizado = "N";
            String NOTAS = "";

            sql = "usp_contabilizar_notas ";

            if (notas.Count > 0)
            {
                foreach (contabilidad nota in notas)
                {
                    if (NOTAS.Length > 0)
                    {
                        NOTAS += ",";
                    }
                    NOTAS += nota.Num_Nota.ToString();
                }
            }

            sql += "'" + NOTAS.ToString() + "', ";
            sql += "'" + ARCHIVO_PLANO.ToString() + "', ";
            sql += "'" + Usuario.ToString() + "'";

            if (ejecutar == true)
            {
                try
                {
                    Contabilizado = Convert.ToString(conexion.ExecuteScalar(sql));
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
            return Contabilizado;
        }

        public String ReversarContabilizacionNotas(String PLANO)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            Boolean ejecutar = true;
            String Reversado = "N";

            sql = "usp_reversar_contabilizar_notas ";

            sql += "'" + PLANO.ToString() + "'";

            if (ejecutar == true)
            {
                try
                {
                    Reversado = Convert.ToString(conexion.ExecuteScalar(sql));
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
            return Reversado;
        }
        #endregion metodos
    }
}
