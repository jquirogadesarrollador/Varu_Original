using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.operaciones;

namespace Brainsbits.LLB.seleccion
{
    public class MotivoRotacionRetiro
    {

        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        private Decimal _id_maestra_rotacion = 0;
        private Decimal _id_detalle_rotacion = 0;
        private String _titulo = null;
        private Boolean _activo = true;
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

        public Decimal ID_MAESTRA_ROTACION
        {
            get { return _id_maestra_rotacion; }
            set { _id_maestra_rotacion = value; }
        }

        public Decimal ID_DETALLE_ROTACION
        {
            get { return _id_detalle_rotacion; }
            set { _id_detalle_rotacion = value; }
        }

        public String TITULO
        {
            get { return _titulo; }
            set { _titulo = value; }
        }

        public Boolean ACTIVO
        {
            get { return _activo; }
            set { _activo = value; }
        }

        #endregion propiedades

        #region constructores
        public MotivoRotacionRetiro()
        {

        }

        public MotivoRotacionRetiro(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public DataTable ObtenerCategoriasRotacionRetiroTodas()
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_maestra_rotacion_retiros_obtenerTodos";

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

        public DataTable ObtenerCategoriaPorId(Decimal ID_MAESTRA_ROTACION)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_maestra_rotacion_retiros_obtenerPorId ";

            sql += ID_MAESTRA_ROTACION;

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

        public DataTable ObtenerMotivosActivosDeCategoria(Decimal ID_MAESTRA_ROTACION)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_detalle_rotacion_retiros_obtenerPorIdMaestraRotacion ";

            sql += ID_MAESTRA_ROTACION;

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

        public DataTable ObtenerMotivosActivosDeCategoria(Decimal ID_MAESTRA_ROTACION, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_detalle_rotacion_retiros_obtenerPorIdMaestraRotacion ";

            sql += ID_MAESTRA_ROTACION;

            if (ejecutar)
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

        public DataTable ObtenerAsociacionMotivosEmpresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_rotacion_retiros_empresa_ObtenermotivosRelacionados ";

            sql += ID_EMPRESA;

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

        public DataTable ObtenerMotivoEmpresaPorId(Decimal ID_ROTACION_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_rotacion_retiros_empresa_ObtenerPorId ";

            sql += ID_ROTACION_EMPRESA;

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

        public Decimal AdicionarCategoria(String TITULO, Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Decimal ID_MAESTRA_ROTACION = 0;

            sql = "usp_sel_reg_maestra_rotacion_retiro_adicionar ";

            if (String.IsNullOrEmpty(TITULO) == false)
            {
                sql += "'" + TITULO + "', ";
                informacion += "TITULO = '" + TITULO + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                try
                {
                    ID_MAESTRA_ROTACION = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_MAESTRA_ROTACION_RETIROS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_MAESTRA_ROTACION = 0;
                }
            }

            return ID_MAESTRA_ROTACION;
        }

        public Decimal AdicionarMotivo(Decimal ID_MAESTRA_ROTACION, String TITULO, Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Decimal ID_DETALLE_ROTACION = 0;

            sql = "usp_sel_reg_detalle_rotacion_retiro_adicionar ";

            if (ID_MAESTRA_ROTACION != 0)
            {
                sql += ID_MAESTRA_ROTACION + ", ";
                informacion += "ID_MAESTRA_ROTACION = '" + ID_MAESTRA_ROTACION + "', ";
            }
            else
            {
                MensajeError += "El campo ID_MAESTRA_ROTACION no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TITULO) == false)
            {
                sql += "'" + TITULO + "', ";
                informacion += "TITULO = '" + TITULO + "', ";
            }
            else
            {
                MensajeError += "El campo TIPO no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                try
                {
                    ID_DETALLE_ROTACION = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_DETALLE_ROTACION_RETIROS, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_DETALLE_ROTACION = 0;
                }
            }

            return ID_DETALLE_ROTACION;
        }

        public Decimal GuardarCategoriaYSusMotivos(String TITULO_CATEGORIA,
            List<MotivoRotacionRetiro> listaMotivos)
        {
            Boolean correcto = true;

            Decimal ID_MAESTRA_ROTACION = 0;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                ID_MAESTRA_ROTACION = AdicionarCategoria(TITULO_CATEGORIA, conexion);

                if (ID_MAESTRA_ROTACION <= 0)
                {
                    correcto = false;
                    conexion.DeshacerTransaccion();
                    ID_MAESTRA_ROTACION = 0;
                }
                else
                {
                    foreach (MotivoRotacionRetiro m in listaMotivos)
                    {
                        Decimal ID_DETALLE_ROTACION = AdicionarMotivo(ID_MAESTRA_ROTACION, m.TITULO, conexion);

                        if (ID_DETALLE_ROTACION <= 0)
                        {
                            correcto = false;
                            conexion.DeshacerTransaccion();
                            ID_MAESTRA_ROTACION = 0;
                            break;
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
                correcto = false;
                MensajeError = ex.Message;
                ID_MAESTRA_ROTACION = 0;
                conexion.DeshacerTransaccion();
            }
            finally
            {
                conexion.Desconectar();
            }

            return ID_MAESTRA_ROTACION;
        }

        public Boolean ActualizarCategoria(Decimal ID_MAESTRA_ROTACION,
            String TITULO,
            Boolean ACTIVO,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_maestra_rotacion_retiros_actualizar ";

            #region validaciones

            if (ID_MAESTRA_ROTACION != 0)
            {
                sql += ID_MAESTRA_ROTACION + ", ";
                informacion += "ID_MAESTRA_ROTACION = '" + ID_MAESTRA_ROTACION + "', ";
            }
            else
            {
                MensajeError = "El campo ID_MAESTRA_ROTACION no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TITULO) == false)
            {
                sql += "'" + TITULO + "', ";
                informacion += "TITULO = '" + TITULO + "', ";
            }
            else
            {
                MensajeError = "El campo TITULO no puede ser vacio.";
                ejecutar = false;
            }

            if (ACTIVO == true)
            {
                sql += "'True', ";
                informacion += "ACTIVO = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "ACTIVO = 'False', ";
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
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_MAESTRA_ROTACION_RETIROS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

            return ejecutadoCorrectamente;
        }

        public Boolean InhabilitarMoivo(Decimal ID_DETALLE_ROTACION,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_detalle_rotacion_retiros_inhabilitar ";

            #region validaciones

            if (ID_DETALLE_ROTACION != 0)
            {
                sql += ID_DETALLE_ROTACION + ", ";
                informacion += "ID_DETALLE_ROTACION = '" + ID_DETALLE_ROTACION + "', ";
            }
            else
            {
                MensajeError = "El campo ID_DETALLE_ROTACION no puede ser vacio.";
                ejecutar = false;
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
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_MAESTRA_ROTACION_RETIROS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

            return ejecutadoCorrectamente;
        }


        public Boolean InhabilitarMotivoAsociadoAEmpresa(Decimal ID_ROTACION_EMPRESA,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_rotacion_rotacion_retiros_empresa_inhabilitar ";

            #region validaciones

            if (ID_ROTACION_EMPRESA != 0)
            {
                sql += ID_ROTACION_EMPRESA + ", ";
                informacion += "ID_ROTACION_EMPRESA = '" + ID_ROTACION_EMPRESA + "', ";
            }
            else
            {
                MensajeError = "El campo ID_ROTACION_EMPRESA no puede ser vacio.";
                ejecutar = false;
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
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_MAESTRA_ROTACION_RETIROS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

            return ejecutadoCorrectamente;
        }

        public Boolean ActualizarMotivo(Decimal ID_DETALLE_ROTACION,
            String TITULO,
            Boolean ACTIVO,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_detalle_rotacion_retiros_actualizar ";

            #region validaciones

            if (ID_DETALLE_ROTACION != 0)
            {
                sql += ID_DETALLE_ROTACION + ", ";
                informacion += "ID_DETALLE_ROTACION = '" + ID_DETALLE_ROTACION + "', ";
            }
            else
            {
                MensajeError = "El campo ID_DETALLE_ROTACION no puede ser vacio.";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TITULO) == false)
            {
                sql += "'" + TITULO + "', ";
                informacion += "TITULO = '" + TITULO + "', ";
            }
            else
            {
                MensajeError = "El campo TITULO no puede ser vacio.";
                ejecutar = false;
            }

            if (ACTIVO == true)
            {
                sql += "'True', ";
                informacion += "ACTIVO = 'True', ";
            }
            else
            {
                sql += "'False', ";
                informacion += "ACTIVO = 'False', ";
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
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_DETALLE_ROTACION_RETIROS, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

            return ejecutadoCorrectamente;
        }

        public Boolean ActualizarCategoriaYSusMotivos(Decimal ID_MAESTRA_ROTACION,
            String TITULO_CATEGORIA,
            Boolean ACTIVO_CATEGORIA,
            List<MotivoRotacionRetiro> listaMotivos)
        {
            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                if (ActualizarCategoria(ID_MAESTRA_ROTACION, TITULO_CATEGORIA, ACTIVO_CATEGORIA, conexion) == false)
                {
                    conexion.DeshacerTransaccion();
                    correcto = false;
                }
                else
                {
                    DataTable tablaMotivosActuales = ObtenerMotivosActivosDeCategoria(ID_MAESTRA_ROTACION, conexion);
                    Boolean motivoEncontrado = false;
                    foreach (DataRow filaMotivosActuales in tablaMotivosActuales.Rows)
                    {
                        motivoEncontrado = false;
                        Decimal ID_DETALLE_ROTACION_ACTUAL = Convert.ToDecimal(filaMotivosActuales["ID_DETALLE_ROTACION"]);

                        foreach (MotivoRotacionRetiro m in listaMotivos)
                        {
                            if (ID_DETALLE_ROTACION_ACTUAL == m.ID_DETALLE_ROTACION)
                            {
                                motivoEncontrado = true;
                                break;
                            }
                        }

                        if (motivoEncontrado == false)
                        {
                            if (InhabilitarMoivo(ID_DETALLE_ROTACION_ACTUAL, conexion) == false)
                            {
                                correcto = false;
                                conexion.DeshacerTransaccion();
                                break;
                            }
                        }
                    }

                    if (correcto == true)
                    {
                        foreach (MotivoRotacionRetiro m in listaMotivos)
                        {
                            if (m.ID_DETALLE_ROTACION == 0)
                            {
                                Decimal ID_DETALLE_ROTACION = AdicionarMotivo(ID_MAESTRA_ROTACION, m.TITULO, conexion);

                                if (ID_DETALLE_ROTACION <= 0)
                                {
                                    conexion.DeshacerTransaccion();
                                    correcto = false;
                                    break;
                                }
                            }
                            else
                            {
                                if (ActualizarMotivo(m.ID_DETALLE_ROTACION, m.TITULO, m.ACTIVO, conexion) == false)
                                {
                                    correcto = false;
                                    conexion.DeshacerTransaccion();
                                    break;
                                }
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
                correcto = false;
                MensajeError = ex.Message;
                conexion.DeshacerTransaccion();
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }

        public DataTable ObtenerMotivosActivosEmpresa(Decimal ID_EMPRESA, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_rotacion_retiros_empresa_ObtenerPorIdEmpresaActivos ";

            sql += ID_EMPRESA;

            if (ejecutar)
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


        public DataTable ObtenerMotivosActivosEmpresa(Decimal ID_EMPRESA)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_rotacion_retiros_empresa_ObtenerPorIdEmpresaActivos ";

            sql += ID_EMPRESA;

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);

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
                finally
                {
                    conexion.Desconectar();
                }
            }

            return _dataTable;
        }


        public Decimal AdicionarMotivoAsociadoAEmpresa(Decimal ID_EMPRESA, Decimal ID_DETALLE_ROTACION, Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Decimal ID_ROTACION_EMPRESA = 0;

            sql = "usp_sel_reg_rotacion_retiros_empresa_adicionar ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_DETALLE_ROTACION != 0)
            {
                sql += ID_DETALLE_ROTACION + ", ";
                informacion += "ID_DETALLE_ROTACION = '" + ID_DETALLE_ROTACION + "', ";
            }
            else
            {
                MensajeError += "El campo ID_DETALLE_ROTACION no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                try
                {
                    ID_ROTACION_EMPRESA = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_ROTACION_EMPRESA, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_ROTACION_EMPRESA = 0;
                }
            }

            return ID_ROTACION_EMPRESA;
        }

        public Boolean ActualizarMotivosRotacionEmpresa(Decimal ID_EMPRESA, List<MotivoRotacionEmpresa> listaMotivosAsociados)
        {
            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                DataTable tablaMotivoAsociadosActuales = ObtenerMotivosActivosEmpresa(ID_EMPRESA, conexion);

                Boolean motivoExiste = false;
                foreach (DataRow filaMotivosActuales in tablaMotivoAsociadosActuales.Rows)
                {
                    Decimal ID_ROTACION_EMPRESA_ACTUAL = Convert.ToDecimal(filaMotivosActuales["ID_ROTACION_EMPRESA"]);

                    motivoExiste = false;

                    foreach (MotivoRotacionEmpresa m in listaMotivosAsociados)
                    {
                        if (ID_ROTACION_EMPRESA_ACTUAL == m.ID_ROTACION_EMPRESA)
                        {
                            motivoExiste = true;
                            break;
                        }
                    }

                    if (motivoExiste == false)
                    {
                        if (InhabilitarMotivoAsociadoAEmpresa(ID_ROTACION_EMPRESA_ACTUAL, conexion) == false)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            break;
                        }
                    }
                }

                if (correcto == true)
                {
                    foreach (MotivoRotacionEmpresa m in listaMotivosAsociados)
                    {
                        if (m.ID_ROTACION_EMPRESA == 0)
                        {
                            Decimal ID_ROTACION_EMPRESA = AdicionarMotivoAsociadoAEmpresa(ID_EMPRESA, m.ID_DETALLE_ROTACION, conexion);

                            if (ID_ROTACION_EMPRESA <= 0)
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

        public DataTable ObtenerResultadosEntrevistaDeRetiroParaEmpleado(Decimal ID_EMPLEADO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_detalle_rotacion_empleado_obtenerPorIdEmpeado ";

            sql += ID_EMPLEADO;

            if (ejecutar)
            {
                Conexion conexion = new Conexion(Empresa);

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
                finally
                {
                    conexion.Desconectar();
                }
            }

            return _dataTable;
        }

        public DataTable ObtenerResultadosEntrevistaDeRetiroParaEmpleado(Decimal ID_EMPLEADO, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_sel_reg_detalle_rotacion_empleado_obtenerPorIdEmpeado ";

            sql += ID_EMPLEADO;

            if (ejecutar)
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

        public Decimal AdicionarMaestraEntrevistaRetiro(Decimal ID_EMPLEADO, String OBSERVACIONES, Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Decimal ID_MAESTRA_ROTACION_EMPLEADO = 0;

            sql = "usp_sel_reg_maestra_rotacion_empleado_adicionar ";

            if (ID_EMPLEADO != 0)
            {
                sql += ID_EMPLEADO + ", ";
                informacion += "ID_EMPLEADO = '" + ID_EMPLEADO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPLEADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(OBSERVACIONES) == false)
            {
                sql += "'" + OBSERVACIONES + "', ";
                informacion += "OBSERVACIONES = '" + OBSERVACIONES + "', ";
            }
            else
            {
                MensajeError += "El campo OBSERVACIONES no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                try
                {
                    ID_MAESTRA_ROTACION_EMPLEADO = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_MAESTRA_ROTACION_EMPLEADO, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_MAESTRA_ROTACION_EMPLEADO = 0;
                }
            }

            return ID_MAESTRA_ROTACION_EMPLEADO;
        }

        public Boolean InhabilitarResultadoEntrevistaRetiro(Decimal ID_DETALLE_ROTACION_EMPLEADO,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Boolean ejecutadoCorrectamente = true;

            tools _tools = new tools();

            sql = "usp_sel_reg_detalle_rotacion_empleado_inhabilitar ";

            #region validaciones

            if (ID_DETALLE_ROTACION_EMPLEADO != 0)
            {
                sql += ID_DETALLE_ROTACION_EMPLEADO + ", ";
                informacion += "ID_DETALLE_ROTACION_EMPLEADO = '" + ID_DETALLE_ROTACION_EMPLEADO + "', ";
            }
            else
            {
                MensajeError = "El campo ID_DETALLE_ROTACION_EMPLEADO no puede ser vacio.";
                ejecutar = false;
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
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_DETALLE_ROTACION_EMPLEADO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
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

            return ejecutadoCorrectamente;
        }

        public Decimal AdicionarResultadoEntrevistaRetiro(Decimal ID_MAESTRA_ROTACION_EMPLEADO,
            Decimal ID_ROTACION_EMPRESA,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            Decimal ID_DETALLE_ROTACION_EMPLEADO = 0;

            sql = "usp_sel_reg_detalle_rotacion_empleado_adicionar ";

            if (ID_MAESTRA_ROTACION_EMPLEADO != 0)
            {
                sql += ID_MAESTRA_ROTACION_EMPLEADO + ", ";
                informacion += "ID_MAESTRA_ROTACION_EMPLEADO = '" + ID_MAESTRA_ROTACION_EMPLEADO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_MAESTRA_ROTACION_EMPLEADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_ROTACION_EMPRESA != 0)
            {
                sql += ID_ROTACION_EMPRESA + ", ";
                informacion += "ID_ROTACION_EMPRESA = '" + ID_ROTACION_EMPRESA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ROTACION_EMPRESA no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                try
                {
                    ID_DETALLE_ROTACION_EMPLEADO = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.SEL_REG_DETALLE_ROTACION_EMPLEADO, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ID_DETALLE_ROTACION_EMPLEADO = 0;
                }
            }

            return ID_DETALLE_ROTACION_EMPLEADO;
        }


        public Decimal ActualizarResultadosEntrevistaRetiroDeEmpleado(Decimal ID_MAESTRA_ROTACION_EMPLEADO,
            Decimal ID_EMPLEADO,
            String OBSERVACIONES,
            List<EntrevistaRotacionEmpleado> listaResultadosEntrevista)
        {
            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                if (ID_MAESTRA_ROTACION_EMPLEADO <= 0)
                {
                    ID_MAESTRA_ROTACION_EMPLEADO = AdicionarMaestraEntrevistaRetiro(ID_EMPLEADO, OBSERVACIONES, conexion);

                    if (ID_MAESTRA_ROTACION_EMPLEADO <= 0)
                    {
                        conexion.DeshacerTransaccion();
                        correcto = false;
                        ID_MAESTRA_ROTACION_EMPLEADO = 0;
                    }
                }

                if (correcto == true)
                {
                    DataTable tablaResultadosActuales = ObtenerResultadosEntrevistaDeRetiroParaEmpleado(ID_EMPLEADO, conexion);

                    Boolean resultadoEncontrado = false;
                    foreach (DataRow filaAtual in tablaResultadosActuales.Rows)
                    {
                        resultadoEncontrado = false;

                        Decimal ID_DETALLE_ROTACION_EMPLEADO_ACTUAL = Convert.ToDecimal(filaAtual["ID_DETALLE_ROTACION_EMPLEADO"]);

                        foreach (EntrevistaRotacionEmpleado e in listaResultadosEntrevista)
                        {
                            if (ID_DETALLE_ROTACION_EMPLEADO_ACTUAL == e.ID_DETALLE_ROTACION_EMPLEADO)
                            {
                                resultadoEncontrado = true;
                                break;
                            }
                        }

                        if (resultadoEncontrado == false)
                        {
                            if (InhabilitarResultadoEntrevistaRetiro(ID_DETALLE_ROTACION_EMPLEADO_ACTUAL, conexion) == false)
                            {
                                conexion.DeshacerTransaccion();
                                ID_MAESTRA_ROTACION_EMPLEADO = 0;
                                correcto = false;
                                break;
                            }
                        }
                    }

                    if (correcto == true)
                    {
                        foreach (EntrevistaRotacionEmpleado e in listaResultadosEntrevista)
                        {
                            if (e.ID_DETALLE_ROTACION_EMPLEADO == 0)
                            {
                                Decimal ID_DETALLE_ROTACION_EMPLEADO = AdicionarResultadoEntrevistaRetiro(ID_MAESTRA_ROTACION_EMPLEADO, e.ID_ROTACION_EMPRESA, conexion);

                                if (ID_DETALLE_ROTACION_EMPLEADO <= 0)
                                {
                                    conexion.DeshacerTransaccion();
                                    correcto = false;
                                    ID_MAESTRA_ROTACION_EMPLEADO = 0;
                                    break;
                                }
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
                correcto = false;
                MensajeError = ex.Message;
                conexion.DeshacerTransaccion();
                ID_MAESTRA_ROTACION_EMPLEADO = 0;
            }
            finally
            {
                conexion.Desconectar();
            }

            return ID_MAESTRA_ROTACION_EMPLEADO;
        }
        #endregion metodos
    }
}
