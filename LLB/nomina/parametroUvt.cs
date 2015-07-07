using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.nomina
{
    public class parametroUvt
    {
        #region variables
        String _empresa = null;
        String _mensaje_error = null;
        String _usuario = null;
        #endregion variables

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
        #endregion propiedades

        #region constructores
        public parametroUvt(String idEmpresa, String usuario)
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

            sql = "usp_param_uvt_obtener_todos ";

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

        public DataTable ObtenerPorId(Int32 ID_UVT)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_param_uvt_obtener_registro ";

            #region validaciones
            if (ID_UVT != 0)
            {
                sql += ID_UVT.ToString() + " ";
            }
            else
            {
                MensajeError = "El campo ID_UVT es requerido para la consulta.";
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

        public Decimal Adicionar(String DESCRIPCION, Decimal RANGO_1, Decimal RANGO_2, Int32 UVTS_ADD, Decimal PORCENTAJE, Int32 ANIO)
        {
            String sql = null;
            String idRecuperado = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_param_uvt_adicionar ";

            if (!(String.IsNullOrEmpty(DESCRIPCION)))
            {
                sql += "'" + DESCRIPCION + "', ";
                informacion += "DESCRIPCION = '" + DESCRIPCION.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo DESCRIPCION no puede ser nulo\n";
                ejecutar = false;
            }

            if (RANGO_1 != 0)
            {
                sql += "'" + RANGO_1.ToString() + "', ";
                informacion += "RANGO_1= '" + RANGO_1.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo RANGO_1 debe ser mayor a cero\n";
                ejecutar = false;
            }

            if (RANGO_2 > RANGO_1)
            {
                sql += "'" + RANGO_2.ToString() + "', ";
                informacion += "RANGO_2= '" + RANGO_2.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo RANGO_2 debe ser mayor a RANGO_1\n";
                ejecutar = false;
            }


            if (UVTS_ADD >= 0)
            {
                sql += "'" + UVTS_ADD.ToString() + "', ";
                informacion += "UVTS_ADD= '" + UVTS_ADD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo UVTS_ADD debe ser mayor o igual a 0\n";
                ejecutar = false;
            }

            if (PORCENTAJE >= 0)
            {
                sql += "'" + PORCENTAJE.ToString() + "', ";
                informacion += "PORCENTAJE= '" + PORCENTAJE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo PORCENTAJE debe ser mayor o igual a 0\n";
                ejecutar = false;
            }

            if (ANIO > 0)
            {
                sql += "'" + ANIO.ToString() + "', ";
                informacion += "ANIO= '" + ANIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ANIO debe ser mayor a cero\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    idRecuperado = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PAR_UVT, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

        public Boolean Actualizar(Int32 ID_UVT, String DESCRIPCION, Decimal RANGO_1, Decimal RANGO_2, Int32 UVTS_ADD, Decimal PORCENTAJE, Int32 ANIO)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_param_uvt_actualizar ";

            #region validaciones

            if (ID_UVT != 0)
            {
                sql += ID_UVT.ToString() + ", ";
                informacion += "ID_UVT= '" + ID_UVT.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_UVT no puede ser menor o igual a cero\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DESCRIPCION)))
            {
                sql += "'" + DESCRIPCION + "', ";
                informacion += "DESCRIPCION = '" + DESCRIPCION.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo DESCRIPCION no puede ser nulo\n";
                ejecutar = false;
            }

            if (RANGO_1 != 0)
            {
                sql += "'" + RANGO_1.ToString() + "', ";
                informacion += "RANGO_1= '" + RANGO_1.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo RANGO_1 debe ser mayor a cero\n";
                ejecutar = false;
            }

            if (RANGO_2 > RANGO_1)
            {
                sql += "'" + RANGO_2.ToString() + "', ";
                informacion += "RANGO_2= '" + RANGO_2.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo RANGO_2 debe ser mayor a RANGO_1\n";
                ejecutar = false;
            }


            if (UVTS_ADD >= 0)
            {
                sql += "'" + UVTS_ADD.ToString() + "', ";
                informacion += "UVTS_ADD= '" + UVTS_ADD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo UVTS_ADD debe ser mayor o igual a 0\n";
                ejecutar = false;
            }

            if (PORCENTAJE >= 0)
            {
                sql += "'" + PORCENTAJE.ToString() + "', ";
                informacion += "PORCENTAJE= '" + PORCENTAJE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo PORCENTAJE debe ser mayor o igual a 0\n";
                ejecutar = false;
            }

            if (ANIO > 0)
            {
                sql += "'" + ANIO.ToString() + "', ";
                informacion += "ANIO= '" + ANIO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ANIO debe ser mayor a cero\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "' ";

            #endregion validaciones

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
        #endregion metodos
    }
}
