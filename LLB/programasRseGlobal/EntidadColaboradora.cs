using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB.seleccion;

using System.IO;

namespace Brainsbits.LLB.programasRseGlobal
{
    public class EntidadColaboradora
    {
        #region variables
        private Decimal _id_entidad_colabora = 0;
        private Decimal _id_entidad = 0;
        private String _descripcion = null;


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

        public Decimal ID_ENTIDAD_COLABORA
        {
            get { return _id_entidad_colabora; }
            set { _id_entidad_colabora = value; }
        }

        public Decimal ID_ENTIDAD
        {
            get { return _id_entidad; }
            set { _id_entidad = value; }
        }

        public String DESCRIPCION
        {
            get { return _descripcion; }
            set { _descripcion = value; }
        }

        #endregion propiedades

        #region constructores
        public EntidadColaboradora()
        {

        }
        public EntidadColaboradora(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        public DataTable ObtenerTodasEntidadesPorArea(Programa.Areas area)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_entidades_colaboradoras_obtenerTodasPorArea ";

            sql += "'" + area.ToString() + "'";

            if (ejecutar)
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

        public Decimal Adicionar(String NOMBRE,
            String DESCRIPCION,
            Programa.Areas ID_AREA)
        {
            String sql = null;
            Decimal idRecuperado = 0;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_prog_entidades_colaboradoras_adicionar ";

            if (String.IsNullOrEmpty(NOMBRE) == false)
            {
                sql += "'" + NOMBRE + "', ";
                informacion += "NOMBRE = '" + NOMBRE + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(DESCRIPCION) == false)
            {
                sql += "'" + DESCRIPCION + "', ";
                informacion += "DESCRIPCION = '" + DESCRIPCION + "', ";
            }
            else
            {
                MensajeError += "El campo DESCRIPCION no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + ID_AREA.ToString() + "', ";
            informacion += "AREA = '" + ID_AREA.ToString() + "', ";

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    idRecuperado = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_ENTIDADES_COLABORADORAS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    idRecuperado = 0;
                }
                finally
                {
                    conexion.Desconectar();
                }
            }

            return idRecuperado;
        }

        public DataTable ObtenerEntidadPorId(Decimal ID_ENTIDAD, Programa.Areas AREA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_entidades_colaboradoras_obtenerPorId ";

            if (ID_ENTIDAD != 0)
            {
                sql += ID_ENTIDAD + ", ";
            }
            else
            {
                ejecutar = false;
            }

            sql += "'" + AREA.ToString() + "'";

            if (ejecutar)
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

        public Boolean Actualizar(Decimal ID_ENTIDAD,
            String NOMBRE,
            String DESCRIPCION,
            Boolean ACTIVO)
        {
            Conexion conexion = new Conexion(Empresa);
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_prog_entidades_colaboradoras_actualizar ";

            #region validaciones

            if (ID_ENTIDAD != 0)
            {
                sql += ID_ENTIDAD + ", ";
                informacion += "ID_ENTIDAD = '" + ID_ENTIDAD + "', ";
            }
            else
            {
                MensajeError = "El campo ID_ENTIDAD no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NOMBRE) == false)
            {
                sql += "'" + NOMBRE + "', ";
                informacion += "NOMBRE = '" + NOMBRE + "', ";
            }
            else
            {
                MensajeError = "El campo NOMBRE no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(DESCRIPCION) == false)
            {
                sql += "'" + DESCRIPCION + "', ";
                informacion += "DESCRIPCION = '" + DESCRIPCION + "', ";
            }
            else
            {
                MensajeError = "El campo DESCRIPCION no puede ser vacio.";
                ejecutar = false;
            }

            if (ACTIVO == true)
            {
                sql += "'True', ";
            }
            else
            {
                sql += "'False', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    conexion.ExecuteNonQuery(sql);

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.PROG_ENTIDADES_COLABORADORAS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                }
                catch (Exception e)
                {
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

            return ejecutadoCorrectamente;
        }

        public DataTable ObtenerEntidadesPorNombre(String NOMBRE, Programa.Areas AREA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_entidades_colaboradoras_obtenerPorNombre ";

            if (String.IsNullOrEmpty(NOMBRE) == false)
            {
                sql += "'" + NOMBRE + "', ";
            }
            else
            {
                ejecutar = false;
            }

            sql += "'" + AREA.ToString() + "'";

            if (ejecutar)
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

        public DataTable ObtenerTodasEntidadesPorAreaYEstado(Programa.Areas area, Boolean ACTIVO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_prog_entidades_colaboradoras_obtenerPorAreaYEstado ";

            sql += "'" + area.ToString() + "', ";

            if (ACTIVO == true)
            {
                sql += "'True'";
            }
            else
            {
                sql += "'False'";
            }

            if (ejecutar)
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
        #endregion metodos

    }
}
