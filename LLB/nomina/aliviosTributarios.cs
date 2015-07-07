using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.IO;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.nomina
{
    public class aliviosTributarios
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
        public aliviosTributarios(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        public DataTable ObtenerAliviosLista()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_lista_par_alivios ";

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

        public DataTable ObtenerAliviosEmpleado(Int32 ID_EMPLEADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_alivios_tributarios_por_id_empleado ";

            #region validaciones
            if (ID_EMPLEADO > 0)
            {
                sql += ID_EMPLEADO.ToString() + "";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO es requerido para la consulta.";
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

        public DataTable ObtenerAliviosId(Decimal ID_ALIVIO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_alivios_tributarios_por_id_alivio ";

            #region validaciones
            if (ID_ALIVIO > 0)
            {
                sql += ID_ALIVIO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_ALIVIO es requerido para la consulta.";
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

        public DataTable ObtenerEmpleadosNombre(String NOMBRE)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alivios_obtenerEmpleadoPorNombre ";

            #region validaciones
            if (!(String.IsNullOrEmpty(NOMBRE)))
            {
                sql += "'" + NOMBRE.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo NOMBRE es requerido para la consulta.";
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

        public DataTable ObtenerEmpleadosIdentificacion(String CEDULA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alivios_obtenerEmpleadoPorDocumento ";

            #region validaciones
            if (!(String.IsNullOrEmpty(CEDULA)))
            {
                sql += "'" + CEDULA.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo CEDULA es requerido para la consulta.";
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

        public Boolean AdicionarAlivio(Int32 ID_PAR_ALIVIO_TRIBUTARIO, Int32 ID_EMPLEADO, Decimal VALOR, Decimal PORCENTAJE, Int32 AÑO_GRAVABLE)
        {
            Conexion conexion = new Conexion(Empresa);
            Boolean _adicionado = false;
            Int32 _adicionados = 0;
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alivios_tributarios_adicionar ";

            #region validaciones
            if (ID_PAR_ALIVIO_TRIBUTARIO > 0)
            {
                sql += ID_PAR_ALIVIO_TRIBUTARIO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PAR_ALIVIO_TRIBUTARIO es requerido para la consulta.";
                ejecutar = false;
            }

            if (ID_EMPLEADO > 0)
            {
                sql += ID_EMPLEADO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO es requerido para la consulta.";
                ejecutar = false;
            }

            sql += VALOR.ToString().Replace(',', '.') + ", ";
            sql += PORCENTAJE.ToString().Replace(',', '.') + ", ";
            sql += AÑO_GRAVABLE.ToString().Replace(',', '.') + ", ";

            if (!(String.IsNullOrEmpty(Usuario)))
            {
                sql += "'" + Usuario.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo USUARIO es requerido para la consulta.";
                ejecutar = false;
            }

            #endregion

            if (ejecutar == true)
            {
                try
                {
                    _adicionados = Convert.ToInt32(conexion.ExecuteScalar(sql));
                    if (_adicionados > 0) _adicionado = true;
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
            return _adicionado;
        }

        public Boolean ActualizarAlivio(Int32 ID_ALIVIO_TRIBUTARIO, Int32 ID_PAR_ALIVIO_TRIBUTARIO, Int32 ID_EMPLEADO, Decimal VALOR, Decimal PORCENTAJE, Int32 AÑO_GRAVABLE, String ACTIVO)
        {
            Conexion conexion = new Conexion(Empresa);
            Boolean _actualizado = false;
            Int32 _actualizados = 0;
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alivios_tributarios_actualizar ";

            #region validaciones
            if (ID_ALIVIO_TRIBUTARIO > 0)
            {
                sql += ID_ALIVIO_TRIBUTARIO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_ALIVIO_TRIBUTARIO es requerido para la consulta.";
                ejecutar = false;
            }

            if (ID_PAR_ALIVIO_TRIBUTARIO > 0)
            {
                sql += ID_PAR_ALIVIO_TRIBUTARIO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PAR_ALIVIO_TRIBUTARIO es requerido para la consulta.";
                ejecutar = false;
            }

            if (ID_EMPLEADO > 0)
            {
                sql += ID_EMPLEADO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO es requerido para la consulta.";
                ejecutar = false;
            }

            sql += VALOR.ToString().Replace(',', '.') + ", ";
            sql += PORCENTAJE.ToString().Replace(',', '.') + ", ";
            sql += AÑO_GRAVABLE.ToString() + ", ";
            sql += "'" + ACTIVO + "', ";

            if (!(String.IsNullOrEmpty(Usuario)))
            {
                sql += "'" + Usuario.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo USUARIO es requerido para la consulta.";
                ejecutar = false;
            }

            #endregion

            if (ejecutar == true)
            {
                try
                {
                    _actualizados = Convert.ToInt32(conexion.ExecuteScalar(sql));
                    if (_actualizados > 0) _actualizado = true;
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
            return _actualizado;
        }

        public Boolean EliminarAlivio(Int32 ID_ALIVIO_TRIBUTARIO)
        {
            Conexion conexion = new Conexion(Empresa);
            Boolean _eliminado = false;
            Int32 _eliminados = 0;
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alivios_tributarios_eliminar ";

            #region validaciones
            if (ID_ALIVIO_TRIBUTARIO > 0)
            {
                sql += ID_ALIVIO_TRIBUTARIO.ToString() + "";
            }
            else
            {
                MensajeError = "El campo ID_ALIVIO_TRIBUTARIO es requerido para la consulta.";
                ejecutar = false;
            }


            #endregion

            if (ejecutar == true)
            {
                try
                {
                    _eliminados = Convert.ToInt32(conexion.ExecuteScalar(sql));
                    if (_eliminados > 0) _eliminado = true;
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
            return _eliminado;
        }


        public DataTable ObtenerExcluyentes(Int32 ALIVIO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alivios_excluyentes ";

            #region validaciones
            if (ALIVIO > 0)
            {
                sql += ALIVIO.ToString() + "";
            }
            else
            {
                MensajeError = "El campo ALIVIO es requerido para la consulta.";
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

        public Decimal ObtenerValorSalud(Int32 ALIVIO, Int32 ID_EMPLEADO, Int32 ANIO)
        {
            Conexion conexion = new Conexion(Empresa);
            Decimal ValorSalud = 0;
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alivios_calcular_salud ";

            #region validaciones
            if (ALIVIO > 0)
            {
                sql += ALIVIO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ALIVIO es requerido para la consulta.";
                ejecutar = false;
            }

            if (ID_EMPLEADO > 0)
            {
                sql += ID_EMPLEADO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPLEADO es requerido para la consulta.";
                ejecutar = false;
            }

            if (ANIO > 0)
            {
                sql += ANIO.ToString() + "";
            }
            else
            {
                MensajeError = "El campo AÑO es requerido para la consulta.";
                ejecutar = false;
            }

            #endregion

            if (ejecutar == true)
            {
                try
                {
                    ValorSalud = Convert.ToDecimal(conexion.ExecuteScalar(sql));
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
            return ValorSalud;
        }
        #endregion metodos
    }
}
