using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.contratacion
{
    public class cajaCompensacionFamiliar
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
        public cajaCompensacionFamiliar(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos




        public Boolean EliminarCoberturaEntidad(Decimal idEntidad,
            String tipoEntidad,
            Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;
            int numRegistrosAfectados = 0;

            sql = "usp_con_entidades_ciudades_eliminar_de_entidad ";

            #region validaciones

            sql += idEntidad + ", '" + tipoEntidad + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    numRegistrosAfectados = conexion.ExecuteNonQuery(sql);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutar = false;
                    numRegistrosAfectados = 0;
                }
            }

            return true;
        }

        public Boolean AsignarCiudadAEntidad(Decimal idEntidad,
            String idCiudad,
            String tipoEntidad,
            Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;
            int numRegistrosAfectados = 0;

            sql = "USP_CON_ENTIDADES_CIUDADES_ADICIONAR ";

            sql += idEntidad + ", '" + idCiudad + "', '" + tipoEntidad + "',  '" + Usuario + "'";

            if (ejecutar)
            {
                try
                {
                    numRegistrosAfectados = conexion.ExecuteNonQuery(sql);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutar = false;
                    numRegistrosAfectados = 0;
                }
            }

            if (numRegistrosAfectados == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }



        public Decimal AdicionarConCobertura(String NIT,
            String DIG_VER,
            String COD_ENTIDAD,
            String NOM_ENTIDAD,
            String DIR_ENTIDAD,
            String TEL_ENTIDAD,
            String CONTACTO,
            String CARGO,
            List<String> listaCiudades)
        {
            Boolean correcto = true;

            Decimal ID_CAJA_C = 0;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                ID_CAJA_C = Adicionar(NIT, DIG_VER, COD_ENTIDAD, NOM_ENTIDAD, DIR_ENTIDAD, TEL_ENTIDAD, CONTACTO, CARGO, conexion);

                if (ID_CAJA_C <= 0)
                {
                    ID_CAJA_C = 0;
                    conexion.DeshacerTransaccion();
                    correcto = false;
                }
                else
                {
                    if (EliminarCoberturaEntidad(ID_CAJA_C, "CCF", conexion) == false)
                    {
                        conexion.DeshacerTransaccion();
                        ID_CAJA_C = 0;
                        correcto = false;
                    }
                    else
                    {
                        foreach (String c in listaCiudades)
                        {
                            if (AsignarCiudadAEntidad(ID_CAJA_C, c, "CCF", conexion) == false)
                            {
                                conexion.DeshacerTransaccion();
                                ID_CAJA_C = 0;
                                correcto = false;
                                break;
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
                ID_CAJA_C = 0;
                correcto = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            if (correcto == true)
            {
                return ID_CAJA_C;
            }
            else
            {
                return 0;
            }
        }

        public Decimal Adicionar(String NIT, String DIG_VER, String COD_ENTIDAD, String NOM_ENTIDAD, String DIR_ENTIDAD,
            String TEL_ENTIDAD, String CONTACTO, String CARGO, Conexion conexion)
        {
            String sql = null;
            String ID = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = false;

            sql = "usp_con_ent_ccf_adicionar ";

            #region validaciones

            if (!(String.IsNullOrEmpty(NIT)))
            {
                sql += "'" + NIT + "', ";
                informacion += "NIT = '" + NIT + "', ";
            }
            else
            {
                MensajeError += "El campo NIT no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DIG_VER)))
            {
                sql += "'" + DIG_VER + "', ";
                informacion += "DIG_VER = '" + DIG_VER + "', ";
            }
            else
            {
                MensajeError += "El campo DIGITO DE VERIFICACION no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(COD_ENTIDAD)))
            {
                sql += "'" + COD_ENTIDAD + "', ";
                informacion += "COD_ENTIDAD = '" + COD_ENTIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo CODIGO DE ENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOM_ENTIDAD)))
            {
                sql += "'" + NOM_ENTIDAD + "', ";
                informacion += "NOM_ENTIDAD = '" + NOM_ENTIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE DE ENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DIR_ENTIDAD)))
            {
                sql += "'" + DIR_ENTIDAD + "', ";
                informacion += "DIR_ENTIDAD = '" + DIR_ENTIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo DIRECCION DE ENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TEL_ENTIDAD)))
            {
                sql += "'" + TEL_ENTIDAD + "', ";
                informacion += "TEL_ENTIDAD = '" + TEL_ENTIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo TELEFONO DE ENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CONTACTO)))
            {
                sql += "'" + CONTACTO + "', ";
                informacion += "CONTACTO = '" + CONTACTO + "', ";
            }
            else
            {
                MensajeError += "El campo CONTACTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CARGO)))
            {
                sql += "'" + CARGO + "', ";
                informacion += "CARGO = '" + CARGO + "', ";
            }
            else
            {
                MensajeError += "El campo CARGO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "' ";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    #region adicionar entidad
                    ID = conexion.ExecuteScalar(sql);
                    #endregion adicionar entidad

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    if (!(_auditoria.Adicionar(Usuario, tabla.CON_ENT_CAJAS_C, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                    {
                        ejecutadoCorrectamente = false;
                        MensajeError = "Error al momento de registrar la auditoria del sistema.";
                    }
                    else
                    {
                        ejecutadoCorrectamente = true;
                    }
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    ejecutadoCorrectamente = false;
                    MensajeError = e.Message;
                }
            }

            if (ejecutadoCorrectamente)
            {
                return Convert.ToDecimal(ID);
            }
            else
            {
                return 0;
            }
        }

        public Boolean ActualizarConCobertura(Decimal ID_CCF,
            String NIT,
            String DIG_VER,
            String COD_ENTIDAD,
            String NOM_ENTIDAD,
            String DIR_ENTIDAD,
            String TEL_ENTIDAD,
            String CONTACTO,
            String CARGO,
            bool ESTADO,
            List<String> listaCiudades)
        {
            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                if (Actualizar(ID_CCF, NIT, DIG_VER, COD_ENTIDAD, NOM_ENTIDAD, DIR_ENTIDAD, TEL_ENTIDAD, CONTACTO, CARGO, ESTADO, conexion) == false)
                {
                    conexion.DeshacerTransaccion();
                    correcto = false;
                }
                else
                {
                    if (EliminarCoberturaEntidad(ID_CCF, "CCF", conexion) == false)
                    {
                        conexion.DeshacerTransaccion();
                        correcto = false;
                    }
                    else
                    {
                        foreach (String c in listaCiudades)
                        {
                            if (AsignarCiudadAEntidad(ID_CCF, c, "CCF", conexion) == false)
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
                    conexion.AceptarTransaccion();
                }
            }
            catch
            {
                conexion.DeshacerTransaccion();
                correcto = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            if (correcto == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        public Boolean Actualizar(Decimal ID_CCF,
            String NIT,
            String DIG_VER,
            String COD_ENTIDAD,
            String NOM_ENTIDAD,
            String DIR_ENTIDAD,
            String TEL_ENTIDAD,
            String CONTACTO,
            String CARGO,
            bool ESTADO,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            sql = "usp_con_ent_ccf_actualizar ";

            #region validaciones

            if (ID_CCF != 0)
            {
                sql += ID_CCF + ", ";
                informacion += "ID_CCF = '" + ID_CCF + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CCF no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NIT)))
            {
                sql += "'" + NIT + "', ";
                informacion += "NIT = '" + NIT + "', ";
            }
            else
            {
                MensajeError += "El campo NIT no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DIG_VER)))
            {
                sql += "'" + DIG_VER + "', ";
                informacion += "DIG_VER = '" + DIG_VER + "', ";
            }
            else
            {
                MensajeError += "El campo DIGITO DE VERIFICACION no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(COD_ENTIDAD)))
            {
                sql += "'" + COD_ENTIDAD + "', ";
                informacion += "COD_ENTIDAD = '" + COD_ENTIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo CODIGO DE ENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOM_ENTIDAD)))
            {
                sql += "'" + NOM_ENTIDAD + "', ";
                informacion += "NOM_ENTIDAD = '" + NOM_ENTIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE DE ENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DIR_ENTIDAD)))
            {
                sql += "'" + DIR_ENTIDAD + "', ";
                informacion += "DIR_ENTIDAD = '" + DIR_ENTIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo DIRECCION DE ENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TEL_ENTIDAD)))
            {
                sql += "'" + TEL_ENTIDAD + "', ";
                informacion += "TEL_ENTIDAD = '" + TEL_ENTIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo TELEFONO DE ENTIDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CONTACTO)))
            {
                sql += "'" + CONTACTO + "', ";
                informacion += "CONTACTO = '" + CONTACTO + "', ";
            }
            else
            {
                MensajeError += "El campo CONTACTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(CARGO)))
            {
                sql += "'" + CARGO + "', ";
                informacion += "CARGO = '" + CARGO + "', ";
            }
            else
            {
                MensajeError += "El campo CARGO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (ESTADO) sql += "'S'";
            else sql += "'N'";
            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    #region actualizar entidad
                    if (conexion.ExecuteNonQuery(sql) == 0)
                    {
                        ejecutadoCorrectamente = false;
                    }
                    #endregion adicionar entidad

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    if (!(_auditoria.Adicionar(Usuario, tabla.CON_ENT_CAJAS_C, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                    {
                        ejecutadoCorrectamente = false;
                        MensajeError = "Error: Al momento de registrar la audotoria del sitema.";
                    }
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutadoCorrectamente = false;
                }
            }

            if (ejecutadoCorrectamente) return true;
            else return false;
        }

        public DataTable ObtenerPorNombre(String NOMBRE)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_ent_ccf_obtenerPorNombre ";

            if (!(String.IsNullOrEmpty(NOMBRE)))
            {
                sql += "'" + NOMBRE + "'";
                informacion += "NOMBRE = '" + NOMBRE + "'";
            }
            else
            {
                MensajeError += "El campo NOMBRE no puede ser nulo. \n";
                ejecutar = false;
            }


            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_ENT_CAJAS_C, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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

        public DataTable ObtenerPorIdCCF(Decimal ID_CCF)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_ent_ccf_obtenerPorIdCCF ";

            if (ID_CCF != 0)
            {
                sql += ID_CCF;
                informacion += "ID_CCF = " + ID_CCF;
            }
            else
            {
                MensajeError += "El campo ID_CCF no puede ser nulo. \n";
                ejecutar = false;
            }


            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_ENT_CAJAS_C, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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



        public DataTable ObtenerCiudadesEntidad(Decimal idEntidad,
            String tipoEntidad)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "USP_CON_ENTIDADES_CIUDADES_OBTENER_DATOS ";

            sql += idEntidad + ", '" + tipoEntidad + "'";

            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_ENT_CAJAS_C, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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



        public DataTable ObtenerTodasLasCajasCompensacionFamiliar()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_con_ent_cajas_c_obtenerTodas ";

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

        public DataTable ObtenerPorCiudad(String CIUDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_ent_cajas_c_obtenerTodas_por_ciudad ";

            if (!(String.IsNullOrEmpty(CIUDAD)))
            {
                sql += "'" + CIUDAD + "'";
                informacion += "CIUDAD = '" + CIUDAD + "'";
            }
            else
            {
                MensajeError += "El campo CIUDAD no puede ser nulo. \n";
                ejecutar = false;
            }


            if (ejecutar)
            {
                try
                {
                    _dataSet = conexion.ExecuteReader(sql);
                    _dataView = _dataSet.Tables[0].DefaultView;
                    _dataTable = _dataView.Table;

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_ENT_CAJAS_C, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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

        #endregion metodos
    }
}
