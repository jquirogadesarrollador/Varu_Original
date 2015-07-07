using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.nomina;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.nomina
{
    public class parametroSalarial
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
        public parametroSalarial(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        public DataTable ObtenerTodos()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_param_salariales_obtener_todos ";

            #region validaciones

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

        public DataTable ObtenerPorId(Int32 ID_SALARIALES)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_param_salariales_obtener_registro ";

            #region validaciones
            if (ID_SALARIALES != 0)
            {
                sql += ID_SALARIALES.ToString() + " ";
            }
            else
            {
                MensajeError = "El campo ID_SALARIALES es requerido para la consulta.";
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

        public Decimal Adicionar(Int32 ANIO, Decimal SMMLV, Decimal PORC_SMMLV, Decimal SUB_TRANS, Decimal PORC_SUB_TRANS, Decimal UVT, Decimal SMMLV_SALARIO_INTEGRAL)
        {
            String sql = null;
            String idRecuperado = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_param_salariales_adicionar ";

            #region validaciones
            if (ANIO > 0)
            {
                sql += "'" + ANIO + "', ";
                informacion += "ANIO = '" + ANIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ANIO no puede ser menor de cero\n";
                ejecutar = false;
            }

            if (SMMLV > 0)
            {
                sql += "'" + SMMLV.ToString() + "', ";
                informacion += "SMMLV= '" + SMMLV.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SMMLV debe ser mayor a cero\n";
                ejecutar = false;
            }

            sql += "'" + PORC_SMMLV.ToString() + "', ";
            informacion += "PORC_SMMLV= '" + PORC_SMMLV.ToString() + "', ";

            if (SUB_TRANS > 0)
            {
                sql += "'" + SUB_TRANS.ToString() + "', ";
                informacion += "SUB_TRANS= '" + SUB_TRANS.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SUB_TRANS debe ser mayor a 0\n";
                ejecutar = false;
            }

            sql += "'" + PORC_SUB_TRANS.ToString() + "', ";
            informacion += "PORC_SUB_TRANS= '" + PORC_SUB_TRANS.ToString() + "', ";

            if (UVT > 0)
            {
                sql += "'" + UVT.ToString() + "', ";
                informacion += "UVT= '" + UVT.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo UVT debe ser mayor a 0\n";
                ejecutar = false;
            }

            if (SMMLV_SALARIO_INTEGRAL > 0)
            {
                sql += "'" + SMMLV_SALARIO_INTEGRAL.ToString() + "', ";
                informacion += "SMMLV_SALARIO_INTEGRAL= '" + SMMLV_SALARIO_INTEGRAL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SMMLV_SALARIO_INTEGRAL debe ser mayor a 0\n";
                ejecutar = false;
            }


            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    idRecuperado = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PAR_SALARIALES, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

            if (!(String.IsNullOrEmpty(idRecuperado))) return Convert.ToDecimal(idRecuperado);
            else return 0;
        }

        public Boolean Actualizar(Int32 ID_SALARIALES, Int32 ANIO, Decimal SMMLV, Decimal PORC_SMMLV, Decimal SUB_TRANS, Decimal PORC_SUB_TRANS, Decimal UVT, Decimal SMMLV_SALARIO_INTEGRAL)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_param_salariales_actualizar ";

            #region validaciones

            if (ID_SALARIALES != 0)
            {
                sql += ID_SALARIALES.ToString() + ", ";
                informacion += "ID_SALARIALES= '" + ID_SALARIALES.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SALARIALES no puede ser menor o igual a cero\n";
                ejecutar = false;
            }


            if (ANIO > 0)
            {
                sql += "'" + ANIO + "', ";
                informacion += "ANIO = '" + ANIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ANIO no puede ser menor de cero\n";
                ejecutar = false;
            }

            if (SMMLV > 0)
            {
                sql += "'" + SMMLV.ToString() + "', ";
                informacion += "SMMLV= '" + SMMLV.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SMMLV debe ser mayor a cero\n";
                ejecutar = false;
            }

            sql += "'" + PORC_SMMLV.ToString() + "', ";
            informacion += "PORC_SMMLV= '" + PORC_SMMLV.ToString() + "', ";

            if (SUB_TRANS > 0)
            {
                sql += "'" + SUB_TRANS.ToString() + "', ";
                informacion += "SUB_TRANS= '" + SUB_TRANS.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SUB_TRANS debe ser mayor a 0\n";
                ejecutar = false;
            }

            sql += "'" + PORC_SUB_TRANS.ToString() + "', ";
            informacion += "PORC_SUB_TRANS= '" + PORC_SUB_TRANS.ToString() + "', ";

            if (UVT > 0)
            {
                sql += "'" + UVT.ToString() + "', ";
                informacion += "UVT= '" + UVT.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo UVT debe ser mayor a 0\n";
                ejecutar = false;
            }

            if (SMMLV_SALARIO_INTEGRAL >= 0)
            {
                sql += "'" + SMMLV_SALARIO_INTEGRAL.ToString() + "', ";
                informacion += "SMMLV_SALARIO_INTEGRAL= '" + SMMLV_SALARIO_INTEGRAL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo SMMLV_SALARIO_INTEGRAL debe ser mayor a 0\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "' ";

            #endregion

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PAR_UVT, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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


        public DataTable ObtenerSalarioMinimo(int ANIO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_par_salariales_obtenerSMLV ";

            if (ANIO != 0)
            {
                sql += ANIO;
                informacion += "ANIO = " + ANIO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ANIO no puede ser 0\n";
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
                    _auditoria.Adicionar(Usuario, tabla.PAR_SALARIALES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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

        public DataTable ObtenerSalarioIntegral(int ANIO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_par_salariales_obtenerSMMLV_INTEGRAL ";

            if (ANIO != 0)
            {
                sql += ANIO;
                informacion += "ANIO = " + ANIO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ANIO no puede ser 0\n";
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
                    _auditoria.Adicionar(Usuario, tabla.PAR_SALARIALES, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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


        public DataRow ObtenerPorAño(int ANIO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataRow dataRow;
            String sql = null;

            sql = "usp_par_salariales_obtenerPorAño " + ANIO.ToString();

            try
            {
                dataRow = conexion.ExecuteReader(sql).Tables[0].DefaultView.Table.Rows[0];

            }
            catch (Exception e)
            {
                throw new Exception("Error al obtener parametros salariasles por año. " + e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }

            return dataRow;
        }

        #endregion metodos
    }
}
