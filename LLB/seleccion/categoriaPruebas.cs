using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.seleccion
{
    public class categoriaPruebas
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
        public categoriaPruebas(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        #region sel_cat_pruebas
        public Decimal AdicionarSelCatPruebas(String NOM_CATEGORIA, String OBS_CATEGORIA)
        {
            String sql = null;
            String idRecComFuentes = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_sel_cat_pruebas_adicionar ";

            #region validaciones

            if (!(String.IsNullOrEmpty(NOM_CATEGORIA.ToString())))
            {
                sql += "'" + NOM_CATEGORIA + "', ";
                informacion += "NOM_CATEGORIA= '" + NOM_CATEGORIA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOM_CATEGORIA no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(OBS_CATEGORIA.ToString())))
            {
                sql += "'" + OBS_CATEGORIA + "', ";
                informacion += "OBS_CATEGORIA= '" + OBS_CATEGORIA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBS_CATEGORIA no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "' ";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    idRecComFuentes = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_CAT_PRUEBAS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

            if (!(String.IsNullOrEmpty(idRecComFuentes))) return Convert.ToDecimal(idRecComFuentes);
            else return 0;
        }
        public Boolean ActualizarSelCatPruebas(int ID_CATEGORIA, String NOM_CATEGORIA, String OBS_CATEGORIA)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_sel_cat_pruebas_actualizar ";

            #region validaciones
            if (ID_CATEGORIA != 0)
            {
                sql += ID_CATEGORIA + ", ";
                informacion += "ID_CATEGORIA= '" + ID_CATEGORIA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CATEGORIA no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(NOM_CATEGORIA.ToString())))
            {
                sql += "'" + NOM_CATEGORIA + "', ";
                informacion += "NOM_CATEGORIA= '" + NOM_CATEGORIA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOM_CATEGORIA no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(OBS_CATEGORIA.ToString())))
            {
                sql += "'" + OBS_CATEGORIA + "', ";
                informacion += "OBS_CATEGORIA= '" + OBS_CATEGORIA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBS_CATEGORIA no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.SEL_CAT_PRUEBAS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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
        public DataTable ObtenerSelCatPruebasPorIdCat(int ID_CATEGORIA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_sel_cat_pruebas_obtener_por_id_cat ";

            if (ID_CATEGORIA != 0)
            {
                sql += ID_CATEGORIA;
                informacion += "ID_CATEGORIA = '" + ID_CATEGORIA.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_CATEGORIA no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.SEL_CAT_PRUEBAS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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
        public DataTable ObtenerSelCatPruebasPorNomCat(String NOM_CATEGORIA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_sel_cat_pruebas_obtener_por_nom_cat ";

            if (!(String.IsNullOrEmpty(NOM_CATEGORIA.ToString())))
            {
                sql += "'" + NOM_CATEGORIA + "' ";
                informacion += "NOM_CATEGORIA= '" + NOM_CATEGORIA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOM_CATEGORIA no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.SEL_CAT_PRUEBAS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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
        public DataTable ObtenerSelCatPruebasTodas()
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_sel_cat_pruebas_buscar_todas";

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
                    _auditoria.Adicionar(Usuario, tabla.SEL_CAT_PRUEBAS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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
        #endregion

        #region sel_pruebas
        public Decimal AdicionarSelPruebas(String NOM_PRUEBA,
            Decimal ID_CATEGORIA,
            String OBS_PRUEBA,
            Byte[] ARCHIVO,
            String ARCHIVO_EXTENSION,
            Decimal ARCHIVO_TAMANO,
            String ARCHIVO_TYPE)
        {
            String ID = null;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_sel_pruebas_adicionar_con_manual ";

            #region validaciones
            if (String.IsNullOrEmpty(NOM_PRUEBA) == false)
            {
                sql += "'" + NOM_PRUEBA + "', ";
                informacion += "NOM_PRUEBA = '" + NOM_PRUEBA + "', ";
            }
            else
            {
                MensajeError = "El campo NOM_PRUEBA no puede ser vacio.";
                ejecutar = false;
            }

            if (ID_CATEGORIA != 0)
            {
                sql += ID_CATEGORIA + ", ";
                informacion += "ID_CATEGORIA = '" + ID_CATEGORIA + "', ";
            }
            else
            {
                MensajeError = "El campo ID_CATEGORIA no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(OBS_PRUEBA) == false)
            {
                sql += "'" + OBS_PRUEBA + "', ";
                informacion += "OBS_PRUEBA = '" + OBS_PRUEBA + "', ";
            }
            else
            {
                MensajeError = "El campo OBS_PRUEBA no puede ser vacio.";
                ejecutar = false;
            }

            if (ARCHIVO != null)
            {
                sql += "'[DATOS_BINARIOS]', ";
                informacion += "ARCHIVO = '[DATOS_BINARIOS]', ";

                sql += "'" + ARCHIVO_EXTENSION + "', ";
                informacion += "ARCHIVO_EXTENSION = '" + ARCHIVO_EXTENSION + "', ";

                sql += ARCHIVO_TAMANO.ToString() + ", ";
                informacion += "ARCHIVO_TAMANO = '" + ARCHIVO_TAMANO.ToString() + "', ";

                sql += "'" + ARCHIVO_TYPE + "', ";
                informacion += "ARCHIVO_TYPE = '" + ARCHIVO_TYPE + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ARCHIVO = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_EXTENSION = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_TAMANO = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_TYPE = 'NULL', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    #region adicionar adjunto
                    ID = conexion.ExecuteEscalarParaAdicionarPrueba(NOM_PRUEBA, ID_CATEGORIA, OBS_PRUEBA, ARCHIVO, ARCHIVO_EXTENSION, ARCHIVO_TAMANO, ARCHIVO_TYPE, Usuario);
                    #endregion adicionar adjunto

                    #region auditoria
                    if (Convert.ToDecimal(ID) > 0)
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.SEL_PRUEBAS, tabla.ACCION_ADICIONAR, sql, informacion, conexion)))
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = "ERROR: Al intentar ingresar la auditoría.";
                            ejecutadoCorrectamente = false;
                        }
                        else
                        {
                            conexion.AceptarTransaccion();
                        }
                    }
                    else
                    {
                        conexion.DeshacerTransaccion();
                        MensajeError = "ERROR: intenatar ingresar en la bd la prueba.";
                        ejecutadoCorrectamente = false;
                    }
                    #endregion auditoria
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

            if (ejecutadoCorrectamente)
            {
                return Convert.ToDecimal(ID);
            }
            else
            {
                return 0;
            }
        }


        public Boolean ActualizarSelPrueba(Decimal ID_PRUEBA,
            String NOM_PRUEBA,
            String OBS_PRUEBA,
            Byte[] ARCHIVO,
            String ARCHIVO_EXTENSION,
            Decimal ARCHIVO_TAMANO,
            String ARCHIVO_TYPE)
        {
            #region validaciones
            if (ID_PRUEBA == 0)
            {
                MensajeError += "El campo ID_PRUEBA no puede ser 0\n";
                return false;
            }

            if (String.IsNullOrEmpty(NOM_PRUEBA) == true)
            {
                MensajeError += "El campo NOM_PRUEBA no puede ser 0\n";
                return false;
            }

            if (String.IsNullOrEmpty(OBS_PRUEBA) == true)
            {
                MensajeError += "El campo OBS_PRUEBA no puede ser 0\n";
                return false;
            }
            #endregion validaciones

            if (ARCHIVO == null)
            {
                return actualizarPruebaSinManual(ID_PRUEBA, NOM_PRUEBA, OBS_PRUEBA);
            }
            else
            {
                if (actualizarPruebaConManual(ID_PRUEBA, NOM_PRUEBA, OBS_PRUEBA, ARCHIVO, ARCHIVO_EXTENSION, ARCHIVO_TAMANO, ARCHIVO_TYPE) == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        private Boolean actualizarPruebaSinManual(Decimal ID_PRUEBA,
            String NOM_PRUEBA,
            String OBS_PRUEBA)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_sel_pruebas_actualizar ";

            #region validaciones
            if (ID_PRUEBA != 0)
            {
                sql += ID_PRUEBA + ", ";
                informacion += "ID_PRUEBA = '" + ID_PRUEBA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PRUEBA no puede ser 0\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(NOM_PRUEBA) == false)
            {
                sql += "'" + NOM_PRUEBA + "', ";
                informacion += "NOM_PRUEBA = '" + NOM_PRUEBA + "', ";
            }
            else
            {
                MensajeError += "El campo NOM_PRUEBA no puede ser 0\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(OBS_PRUEBA) == false)
            {
                sql += "'" + OBS_PRUEBA + "', ";
                informacion += "OBS_PRUEBA = '" + OBS_PRUEBA + "', ";
            }
            else
            {
                MensajeError += "El campo OBS_PRUEBA no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";
            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    if (conexion.ExecuteNonQuery(sql) == 0)
                    {
                        ejecutadoCorrectamente = false;
                        conexion.DeshacerTransaccion();
                    }
                    else
                    {
                        #region auditoria
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.SEL_PRUEBAS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                        {
                            ejecutadoCorrectamente = false;
                            MensajeError = "ERROR: Al intentar realizar la auditoría.";
                            conexion.DeshacerTransaccion();
                        }
                        else
                        {
                            conexion.AceptarTransaccion();
                        }
                        #endregion auditoria
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


        private Decimal actualizarPruebaConManual(Decimal ID_PRUEBA,
            String NOM_PRUEBA,
            String OBS_PRUEBA,
            Byte[] ARCHIVO,
            String ARCHIVO_EXTENSION,
            Decimal ARCHIVO_TAMANO,
            String ARCHIVO_TYPE)
        {
            Int32 actualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_sel_pruebas_actualizar_con_manual ";

            #region validaciones
            sql += ID_PRUEBA + ", ";
            informacion += "ID_PRUEBA = '" + ID_PRUEBA + "', ";

            sql += "'" + NOM_PRUEBA + "', ";
            informacion += "NOM_PRUEBA = '" + NOM_PRUEBA + "', ";

            sql += "'" + OBS_PRUEBA + "', ";
            informacion += "OBS_PRUEBA = '" + OBS_PRUEBA + "', ";

            if (ARCHIVO != null)
            {
                sql += "'[DATOS_BINARIOS]', ";
                informacion += "ARCHIVO = '[DATOS_BINARIOS]', ";

                sql += "'" + ARCHIVO_EXTENSION + "', ";
                informacion += "ARCHIVO_EXTENSION = '" + ARCHIVO_EXTENSION + "', ";

                sql += ARCHIVO_TAMANO.ToString() + ", ";
                informacion += "ARCHIVO_TAMANO = '" + ARCHIVO_TAMANO.ToString() + "', ";

                sql += "'" + ARCHIVO_TYPE + "', ";
                informacion += "ARCHIVO_TYPE = '" + ARCHIVO_TYPE + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ARCHIVO = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_EXTENSION = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_TAMANO = 'NULL', ";

                sql += "NULL, ";
                informacion += "ARCHIVO_TYPE = 'NULL', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario + "'";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                conexion.IniciarTransaccion();
                try
                {
                    #region actualizar prueba
                    actualizados = conexion.ExecuteNonQueryParaActualizarPruebaConManual(ID_PRUEBA, NOM_PRUEBA, OBS_PRUEBA, ARCHIVO, ARCHIVO_EXTENSION, ARCHIVO_TAMANO, ARCHIVO_TYPE, Usuario);
                    #endregion adicionar prueba

                    #region auditoria
                    if (actualizados > 0)
                    {
                        auditoria _auditoria = new auditoria(Empresa);
                        if (!(_auditoria.Adicionar(Usuario, tabla.SEL_PRUEBAS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion)))
                        {
                            conexion.DeshacerTransaccion();
                            MensajeError = "ERROR: Al intentar ingresar la auditoría.";
                            ejecutadoCorrectamente = false;
                        }
                        else
                        {
                            conexion.AceptarTransaccion();
                        }
                    }
                    else
                    {
                        conexion.DeshacerTransaccion();
                        MensajeError = "ERROR: intenatar actualizar en la bd la prueba.";
                        ejecutadoCorrectamente = false;
                    }
                    #endregion auditoria
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

            if (ejecutadoCorrectamente)
            {
                return actualizados;
            }
            else
            {
                return 0;
            }
        }

        public Decimal AdicionarSelPruebas(String NOM_PRUEBA, int ID_CATEGORIA, String OBS_PRUEBA, Byte[] archivo)
        {
            #region validaciones

            if (String.IsNullOrEmpty(NOM_PRUEBA.ToString()))
            {
                MensajeError += "El campo NOM_CATEGORIA no puede ser nulo\n";
                return 0;
            }

            if (ID_CATEGORIA == 0)
            {
                MensajeError += "El campo ID_CATEGORIA no puede ser nulo\n";
                return 0;
            }

            if (String.IsNullOrEmpty(OBS_PRUEBA.ToString()))
            {
                MensajeError += "El campo OBS_CATEGORIA no puede ser nulo\n";
                return 0;
            }
            #endregion validaciones

            if (archivo == null)
            {
                return ingresarPruebaSinManual(NOM_PRUEBA, ID_CATEGORIA, OBS_PRUEBA);
            }
            else
            {
                return ingresarPruebaConManual(NOM_PRUEBA, ID_CATEGORIA, OBS_PRUEBA, archivo);
            }
        }

        private Decimal ingresarPruebaSinManual(String NOM_PRUEBA, int ID_CATEGORIA, String OBS_PRUEBA)
        {
            String sql = null;
            String idRecComFuentes = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_sel_pruebas_adicionar ";

            #region validaciones

            sql += "'" + NOM_PRUEBA + "', ";
            informacion += "NOM_PRUEBA = '" + NOM_PRUEBA.ToString() + "', ";

            sql += ID_CATEGORIA + ",";
            informacion += "ID_CATEGORIA = '" + ID_CATEGORIA.ToString() + "', ";

            sql += "'" + OBS_PRUEBA + "', ";
            informacion += "OBS_CATEGORIA= '" + OBS_PRUEBA.ToString() + "', ";

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);
                try
                {
                    idRecComFuentes = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_PRUEBAS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

            if (!(String.IsNullOrEmpty(idRecComFuentes)))
            {
                return Convert.ToDecimal(idRecComFuentes);
            }
            else
            {
                return 0;
            }
        }


        private Decimal ingresarPruebaConManual(String NOM_PRUEBA, int ID_CATEGORIA, String OBS_PRUEBA, Byte[] ARCHIVO_PRUEBA)
        {
            String sql = null;
            Int32 idPrueba = 0;
            String informacion = null;


            #region validaciones

            informacion += "NOM_PRUEBA = '" + NOM_PRUEBA.ToString() + "', ";
            informacion += "ID_CATEGORIA = '" + ID_CATEGORIA.ToString() + "', ";
            informacion += "OBS_CATEGORIA= '" + OBS_PRUEBA.ToString() + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            #endregion validaciones

            Conexion conexion = new Conexion(Empresa);
            try
            {
                idPrueba = conexion.ExecuteNonQueryParaAdicionarSelPruebaConManual(NOM_PRUEBA, ID_CATEGORIA, OBS_PRUEBA, Usuario, ARCHIVO_PRUEBA);

                #region auditoria
                auditoria _auditoria = new auditoria(Empresa);
                _auditoria.Adicionar(Usuario, tabla.SEL_PRUEBAS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

            return idPrueba;
        }

        public Boolean ActualizarSelPruebas(int ID_PRUEBA, String NOM_PRUEBA, int ID_CATEGORIA, String OBS_PRUEBA)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_sel_pruebas_actualizar ";

            #region validaciones
            if (ID_PRUEBA != 0)
            {
                sql += ID_PRUEBA + ",";
                informacion += "ID_PRUEBA = '" + ID_PRUEBA.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_PRUEBA no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(NOM_PRUEBA.ToString())))
            {
                sql += "'" + NOM_PRUEBA + "', ";
                informacion += "NOM_CATEGORIA= '" + NOM_PRUEBA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOM_CATEGORIA no puede ser nulo\n";
                ejecutar = false;
            }
            if (ID_CATEGORIA != 0)
            {
                sql += ID_CATEGORIA + ",";
                informacion += "ID_CATEGORIA = '" + ID_CATEGORIA.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_CATEGORIA no puede ser nulo\n";
                ejecutar = false;
            }
            if (!(String.IsNullOrEmpty(OBS_PRUEBA.ToString())))
            {
                sql += "'" + OBS_PRUEBA + "', ";
                informacion += "OBS_CATEGORIA= '" + OBS_PRUEBA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBS_CATEGORIA no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.SEL_PRUEBAS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

        public DataTable ObtenerSelPruebasPorIdPrueba(int ID_PRUEBA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_sel_prueba_por_id_prueba ";

            if (ID_PRUEBA != 0)
            {
                sql += ID_PRUEBA;
                informacion += "ID_PRUEBA = '" + ID_PRUEBA.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_PRUEBA no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.SEL_PRUEBAS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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
        public DataTable ObtenerSelPruebasPorNomPrueba(String NOM_PRUEBA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_sel_pruebas_obtener_por_nom_prueba ";

            if (!(String.IsNullOrEmpty(NOM_PRUEBA.ToString())))
            {
                sql += "'" + NOM_PRUEBA + "' ";
                informacion += "NOM_PRUEBA= '" + NOM_PRUEBA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOM_PRUEBA no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.SEL_PRUEBAS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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
        public DataTable ObtenerSelPruebasPorIdCat(int ID_CATEGORIA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_sel_pruebas_obtener_por_id_categoria ";

            if (ID_CATEGORIA != 0)
            {
                sql += ID_CATEGORIA;
                informacion += "ID_CATEGORIA = '" + ID_CATEGORIA.ToString() + "'";
            }
            else
            {
                MensajeError += "El campo ID_CATEGORIA no puede ser nulo\n";
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
                    _auditoria.Adicionar(Usuario, tabla.SEL_PRUEBAS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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
        public DataTable ObtenerTodosLasPruebas()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_sel_pruebas_obtenerTodas ";

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

        public DataTable ObtenerTodosLasPruebasMasNomcategoria()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_sel_pruebas_obtener_todos_mas_categoria ";

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

        #endregion
        #endregion metodos
    }
}
