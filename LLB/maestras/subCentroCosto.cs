using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.SqlClient;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.comercial
{
    public class subCentroCosto
    {
        #region varialbes

        String _empresa = null;
        String _mensaje_error = null;
        String _usuario = null;
        Decimal _ID_SUB_C = 0;
        Decimal _ID_SERVICIO = 0;
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

        public Decimal ID_SUB_C
        {
            get { return _ID_SUB_C; }
            set { _ID_SUB_C = value; }
        }

        public Decimal ID_SERVICIO
        {
            get { return _ID_SERVICIO; }
            set { _ID_SERVICIO = value; }
        }
        #endregion propiedades

        #region constructores
        public subCentroCosto(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public DataTable ObtenerSubCentrosDeCostoPorIdEmpresaIdCentroCosto(Decimal ID_EMPRESA, Decimal ID_CENTRO_C)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_sub_centros_obtenerPorIdEmpresaIdCentroCosto ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C;
            }
            else
            {
                MensajeError = "El campo ID_CENTRO_C no puede ser 0\n";
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

        public DataTable ObtenerSubCentrosDeCostoPorIdSubCosto(Decimal ID_SUB_C)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_sub_centros_obtenerPorIdSubC ";

            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C;
            }
            else
            {
                MensajeError = "El campo ID_SUB_C no puede ser 0\n";
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

        public DataTable ObtenerSubCentrosDeCostoPorIdSubCosto(Decimal ID_SUB_C, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_sub_centros_obtenerPorIdSubC ";

            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C;
            }
            else
            {
                MensajeError = "El campo ID_SUB_C no puede ser 0\n";
                ejecutar = false;
            }

            if (ejecutar == true)
            {
                try
                {
                    _dataSet = conexion.ExecuteReaderConTransaccion(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }
            return _dataTable;
        }

        public DataTable ObtenerSubCentrosDeCostoPorIdEmpresaConInfoDeCCyCiudad(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_sub_centros_obtenerPorIdEmpresa_con_info_de_cc_y_ciudad ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser 0\n";
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

        public DataTable ObtenerSubCentroDeCostoPorIdEmpresaIdCentroCConInfoDeCCyCiudad(Decimal ID_EMPRESA, Decimal ID_CENTRO_C)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_sub_centros_obtenerPorIdEmpresaIdCentroC_con_info_de_cc_y_ciudad ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C;
            }
            else
            {
                MensajeError = "El campo ID_CENTRO_C no puede ser 0\n";
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

        public DataTable ObtenerSubCentroDeCostoPorIdSubCConInfoDeCCyCiudad(Decimal ID_SUB_C)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_sub_centros_obtenerPorIdSubCentro_con_info_de_cc_y_ciudad ";

            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C;
            }
            else
            {
                MensajeError = "El campo ID_SUB_C no puede ser 0\n";
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

        public DataTable ObtenerSubCentroDeCostoPorNombreIdCentroC(String NOM_SUB_C, Decimal ID_CENTRO_C)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_sub_centros_obtenerPorNombre_Id_CentroC ";

            if (String.IsNullOrEmpty(NOM_SUB_C) == false)
            {
                sql += "'" + NOM_SUB_C + "', ";
            }
            else
            {
                MensajeError = "El campo NOM_SUB_C no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C;
            }
            else
            {
                MensajeError = "El campo ID_CENTRO_C no puede ser 0\n";
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

        public DataSet AdicionarSUBCC(Decimal ID_EMPRESA, Decimal ID_CENTRO_C, String NOM_SUB_C)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();

            sql = "usp_ven_sub_centros_adicionar_V2 ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
                informacion += "ID_EMPRESA= '" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C.ToString() + ", ";
                informacion += "ID_CENTRO_C= '" + ID_CENTRO_C.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CENTRO_C no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOM_SUB_C)))
            {
                sql += "'" + NOM_SUB_C + "', ";
                informacion += "NOM_SUB_C = '" + NOM_SUB_C.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOM_SUB_C no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_SUB_CENTROS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

            return _dataSet;
        }

        public DataSet ActualizarSubCC(Decimal ID_SUB_C,
            Decimal ID_EMPRESA,
            Decimal ID_CENTRO_C,
            String NOM_SUB_C,
            Boolean OCULTAR_SUB_C)
        {
            String sql = null;

            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();

            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_ven_sub_centros_actualizar_V2 ";

            #region validaciones
            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C.ToString() + ", ";
                informacion += "ID_SUB_C= '" + ID_CENTRO_C.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_SUB_C no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
                informacion += "ID_EMPRESA= '" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C.ToString() + ", ";
                informacion += "ID_CENTRO_C= '" + ID_CENTRO_C.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CENTRO_C no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOM_SUB_C)))
            {
                sql += "'" + NOM_SUB_C + "', ";
                informacion += "NOM_SUB_C = '" + NOM_SUB_C.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOM_SUB_C no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "', ";

            if (OCULTAR_SUB_C == true)
            {
                sql += "'OCULTO'";
                informacion += "ESTADO = 'OCULTO'";
            }
            else
            {
                sql += "'ACTIVO'";
                informacion += "ESTADO = 'ACTIVO'";
            }
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_SUB_CENTROS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

            return _dataSet;
        }

        #endregion metodos
    }
}