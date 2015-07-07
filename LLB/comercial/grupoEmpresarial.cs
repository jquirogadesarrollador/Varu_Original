using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;


namespace Brainsbits.LLB.comercial
{
    public class grupoEmpresarial
    {
        #region varialbes
        String _empresa = null;
        String _mensaje_error = null;
        String _usuario = null;

        private Decimal _ID_EMPRESA = 0;
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

        public Decimal ID_EMPRESA
        {
            get { return _ID_EMPRESA; }
            set { _ID_EMPRESA = value; }
        }

        #endregion propiedades

        #region constructores
        public grupoEmpresarial()
        {

        }

        public grupoEmpresarial(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        public DataTable ObtenerTodosLosGruposEmpresariales()
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_grupos_empresariales_obtener_todos";

            if (ejecutar == true)
            {
                Conexion conexion = new Conexion(Empresa);
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

        public DataTable ObtenerTodosLosGruposEmpresarialesPorNombre(String NOMBRE)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_grupos_empresariales_obtener_por_NOMBRE ";

            #region validaciones
            if (String.IsNullOrEmpty(NOMBRE) == false)
            {
                sql += "'" + NOMBRE + "'";
            }
            else
            {
                MensajeError = "El campo NOMBRE no puede ser vacio.";
                ejecutar = false;
            }
            #endregion validaciones

            if (ejecutar == true)
            {
                Conexion conexion = new Conexion(Empresa);
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

        public DataTable ObtenerGruposEmpresarialesPorId(Decimal ID_GRUPOEMPRESARIAL)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_grupos_empresariales_obtener_por_id ";

            #region validaciones
            if (ID_GRUPOEMPRESARIAL != 0)
            {
                sql += ID_GRUPOEMPRESARIAL;
            }
            else
            {
                MensajeError = "El campo ID GRUPO EMPRESARIAL no puede ser 0.";
                ejecutar = false;
            }
            #endregion validaciones
            if (ejecutar == true)
            {
                Conexion conexion = new Conexion(Empresa);
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


        public Decimal AdicionarGrupoEmpresarialConEmpresas(String NOMBRE
            , List<grupoEmpresarial> listEmpresas)
        {
            Decimal ID_GRUPOEMPRESARIAL = 0;

            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                ID_GRUPOEMPRESARIAL = AdicionarGrupoEmpresarial(NOMBRE, conexion);

                if (ID_GRUPOEMPRESARIAL <= 0)
                {
                    correcto = false;
                    conexion.DeshacerTransaccion();
                    ID_GRUPOEMPRESARIAL = 0;
                }
                else
                {
                    if (listEmpresas.Count > 0)
                    {
                        cliente _cliente = new cliente(Empresa, Usuario);

                        foreach (grupoEmpresarial empresa in listEmpresas)
                        {
                            if (_cliente.ActualizarGrupoEmpresarialCliente(empresa.ID_EMPRESA, ID_GRUPOEMPRESARIAL, conexion) == false)
                            {
                                correcto = false;
                                conexion.DeshacerTransaccion();
                                ID_GRUPOEMPRESARIAL = 0;
                            }
                        }
                    }
                }

                if (correcto == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch
            {
                conexion.DeshacerTransaccion();
                ID_GRUPOEMPRESARIAL = 0;
                correcto = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            return ID_GRUPOEMPRESARIAL;
        }

        public Decimal AdicionarGrupoEmpresarial(String NOMBRE, Conexion conexion)
        {
            Decimal ID_GRUPOEMPRESARIAL = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_grupos_empresariales_adicionar ";

            #region validaciones
            if (string.IsNullOrEmpty(NOMBRE) == false)
            {
                sql += "'" + NOMBRE + "', ";
                informacion += "NOMBRE = '" + NOMBRE + "', ";
            }
            else
            {
                MensajeError = "El campo NOMBRE no puede ser vacio.";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    ID_GRUPOEMPRESARIAL = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    if (ID_GRUPOEMPRESARIAL <= 0)
                    {
                        MensajeError = "ERROR: Al intentar ingresar el nuevo registro para el grupo empresarial.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.VEN_GRUPOEMPRESARIAL, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para el registro del grupo empresarial.";
                            ejecutadoCorrectamente = false;
                        }
                    }
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
            }
            else
            {
                ejecutadoCorrectamente = false;
            }

            if (ejecutadoCorrectamente)
            {
                return ID_GRUPOEMPRESARIAL;
            }
            else
            {
                return 0;
            }
        }

        public Boolean ActualizarGrupoEmpresarial(Decimal ID_GRUPOEMPRESARIAL
            , String NOMBRE
            , Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_ven_gruposempresariales_actualizar ";

            #region validaciones

            if (ID_GRUPOEMPRESARIAL != 0)
            {
                sql += ID_GRUPOEMPRESARIAL + ", ";
                informacion += "ID_GRUPOEMPRESARIAL = '" + ID_GRUPOEMPRESARIAL + "', ";
            }
            else
            {
                MensajeError = "El campo ID_GRUPOEMPRESARIAL no puede ser vacio.";
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

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    if (conexion.ExecuteNonQuery(sql) <= 0)
                    {
                        MensajeError = "ERROR: Al intentar actualizar el registro del grupo empresarial.";
                        ejecutadoCorrectamente = false;
                    }
                    else
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.VEN_GRUPOEMPRESARIAL, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                        {
                            MensajeError = "ERROR: Al intentar ingresar la auditoría para el registro del grupo empresarial.";
                            ejecutadoCorrectamente = false;
                        }
                    }
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
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

        public Boolean ActualizarGrupoEmpresarialConEMpresas(Decimal ID_GRUPOEMPRESARIAL
            , String NOMBRE
            , List<grupoEmpresarial> listaEmpresas)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            Boolean correcto = true;
            try
            {
                if (ActualizarGrupoEmpresarial(ID_GRUPOEMPRESARIAL, NOMBRE, conexion) == false)
                {
                    conexion.DeshacerTransaccion();
                    correcto = false;
                }
                else
                {
                    cliente _cliente = new cliente(Empresa, Usuario);
                    DataTable tablaEmpresasAsociadas = _cliente.ObtenerEmpresasAsociadasAGrupo(ID_GRUPOEMPRESARIAL, conexion);

                    Boolean existeEmpresa = false;

                    if (tablaEmpresasAsociadas.Rows.Count > 0)
                    {
                        foreach (DataRow fila in tablaEmpresasAsociadas.Rows)
                        {
                            existeEmpresa = false;

                            foreach (grupoEmpresarial empresa in listaEmpresas)
                            {
                                if (empresa.ID_EMPRESA == Convert.ToDecimal(fila["ID_EMPRESA"]))
                                {
                                    existeEmpresa = true;
                                    break;
                                }
                            }

                            if (existeEmpresa == false)
                            {
                                if (_cliente.DesligarEmpresaDeGrupoEmpresarial(Convert.ToDecimal(fila["ID_EMPRESA"]), conexion) == false)
                                {
                                    conexion.DeshacerTransaccion();
                                    correcto = false;
                                    break;
                                }
                            }
                        }
                    }

                    if (correcto == true)
                    {
                        foreach (grupoEmpresarial empresa in listaEmpresas)
                        {
                            if (_cliente.ActualizarGrupoEmpresarialCliente(empresa.ID_EMPRESA, ID_GRUPOEMPRESARIAL, conexion) == false)
                            {
                                correcto = false;
                                conexion.DeshacerTransaccion();
                            }
                        }
                    }
                }

                if (correcto == true)
                {
                    conexion.AceptarTransaccion();
                }
            }
            catch (Exception ex)
            {
                conexion.DeshacerTransaccion();
                correcto = false;
                MensajeError = ex.Message;
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }

        #endregion metodos

    }
}
