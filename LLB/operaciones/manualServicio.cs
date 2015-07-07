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

namespace Brainsbits.LLB.operaciones
{
    public class ManualServicio
    {
        #region variables

        public enum ListaSecciones
        {
            Comercial = 0,
            Seleccion,
            Contratacion,
            Nomina,
            Desconocida,
            General,
            Financiera
        }

        public enum AccionesManual
        {
            Crear = 0,
            Insertar,
            Actualizar,
            Eliminar
        }

        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        private Decimal _ID_ADICIONAL = 0;
        private Decimal _ID_EMPRESA = 0;
        private String _TITULO = null;
        private String _DESCRIPCION = null;
        private ListaSecciones _AREA;
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

        public Decimal ID_ADICIONAL
        {
            get { return _ID_ADICIONAL; }
            set { _ID_ADICIONAL = value; }
        }

        public Decimal ID_EMPRESA
        {
            get { return _ID_EMPRESA; }
            set { _ID_EMPRESA = value; }
        }

        public String TITULO
        {
            get { return _TITULO; }
            set { _TITULO = value; }
        }

        public String DESCRIPCION
        {
            get { return _DESCRIPCION; }
            set { _DESCRIPCION = value; }
        }

        public ListaSecciones AREA
        {
            get { return _AREA; }
            set { _AREA = value; }
        }

        #endregion propiedades

        #region constructores
        public ManualServicio()
        {

        }
        public ManualServicio(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos
        public DataTable ObtenerVersionamientoManualPorEmpresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_oper_versionamiento_manual_obtenerPorIdEmpresa ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo. \n";
                ejecutar = false;
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

        public DataTable ObtenerVersionamientoManualPorEmpresa(Decimal ID_EMPRESA, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_oper_versionamiento_manual_obtenerPorIdEmpresa ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo. \n";
                ejecutar = false;
            }

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

        public DataTable ObtenerAdicionalesPorEmpresaYArea(Decimal ID_EMPRESA, String AREA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_oper_adicionales_manual_obtenerPorIdEmpresaIdArea_activos ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo. \n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(AREA) == false)
            {
                sql += "'" + AREA + "'";
            }
            else
            {
                MensajeError += "El campo AREA no puede ser nulo. \n";
                ejecutar = false;
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

        public DataTable ObtenerAdicionalesPorEmpresaYArea(Decimal ID_EMPRESA, String AREA, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_oper_adicionales_manual_obtenerPorIdEmpresaIdArea_activos ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo. \n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(AREA) == false)
            {
                sql += "'" + AREA + "'";
            }
            else
            {
                MensajeError += "El campo AREA no puede ser nulo. \n";
                ejecutar = false;
            }

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

        public DataTable ObtenerAdicionalesPorEmpresaSinArea(Decimal ID_EMPRESA, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_oper_adicionales_manual_obtenerPorIdEmpresa_activos ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo. \n";
                ejecutar = false;
            }
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


        public Decimal AdicionarVersionamientoParaManualPorEmpresa(Decimal ID_EMPRESA,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Decimal registro = 0;

            tools _tools = new tools();

            sql = "usp_oper_versionamiento_manual_adicionar ";

            #region validaciones

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "Usuario = '" + Usuario + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    registro = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.OPER_VERSIONAMIENTO_MANUAL, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    registro = 0;
                }
            }

            return registro;
        }



        public Decimal AdicionarAdicionalPorEmpresaYVersionamiento(Decimal ID_EMPRESA,
            String AREA,
            String TITULO,
            String DESCRIPCION,
            Decimal ID_VERSIONAMIENTO,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Decimal registro = 0;

            tools _tools = new tools();

            sql = "usp_oper_adicionales_manual_adicionar ";

            #region validaciones

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(AREA) == false)
            {
                sql += "'" + AREA + "', ";
                informacion += "AREA = '" + AREA + "', ";
            }
            else
            {
                MensajeError += "El campo AREA no puede ser 0\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TITULO) == false)
            {
                sql += "'" + TITULO + "', ";
                informacion += "TITULO = '" + TITULO + "', ";
            }
            else
            {
                MensajeError += "El campo TITULO no puede ser 0\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(DESCRIPCION) == false)
            {
                sql += "'" + DESCRIPCION + "', ";
                informacion += "DESCRIPCION = '" + DESCRIPCION + "', ";
            }
            else
            {
                MensajeError += "El campo DESCRIPCION no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_VERSIONAMIENTO != 0)
            {
                sql += ID_VERSIONAMIENTO + ", ";
                informacion += "ID_VERSIONAMIENTO = '" + ID_VERSIONAMIENTO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_VERSIONAMIENTO no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "Usuario = '" + Usuario + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    registro = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.OPER_ADICIONALES_MANUAL, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    registro = 0;
                }
            }

            return registro;
        }

        public Decimal AdicionarHistorialModificacionesHistorial(Decimal ID_EMPRESA,
            String AREA,
            String CAMPO,
            String DESCRIPCION_CAMPO,
            String VALOR_ANTERIOR,
            String VALOR_NUEVO,
            String ACCION,
            Decimal ID_VERSIONAMIENTO,
            Conexion conexion)
        {
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;
            Decimal registro = 0;

            tools _tools = new tools();

            sql = "usp_oper_hist_modificaciones_manual_adicionar ";

            #region validaciones

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(AREA) == false)
            {
                sql += "'" + AREA + "', ";
                informacion += "AREA = '" + AREA + "', ";
            }
            else
            {
                MensajeError += "El campo AREA no puede ser 0\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(CAMPO) == false)
            {
                sql += "'" + CAMPO + "', ";
                informacion += "CAMPO = '" + CAMPO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "CAMPO = 'null', ";
            }

            if (String.IsNullOrEmpty(DESCRIPCION_CAMPO) == false)
            {
                sql += "'" + DESCRIPCION_CAMPO + "', ";
                informacion += "DESCRIPCION_CAMPO = '" + DESCRIPCION_CAMPO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "DESCRIPCION_CAMPO = 'null', ";
            }

            if (String.IsNullOrEmpty(VALOR_ANTERIOR) == false)
            {
                sql += "'" + VALOR_ANTERIOR + "', ";
                informacion += "VALOR_ANTERIOR = '" + VALOR_ANTERIOR + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "VALOR_ANTERIOR = 'null', ";
            }

            if (String.IsNullOrEmpty(VALOR_NUEVO) == false)
            {
                sql += "'" + VALOR_NUEVO + "', ";
                informacion += "VALOR_NUEVO = '" + VALOR_NUEVO + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "VALOR_NUEVO = 'null', ";
            }

            if (String.IsNullOrEmpty(ACCION) == false)
            {
                sql += "'" + ACCION + "', ";
                informacion += "ACCION = '" + ACCION + "', ";
            }
            else
            {
                MensajeError += "El campo ACCION no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_VERSIONAMIENTO != 0)
            {
                sql += ID_VERSIONAMIENTO + ", ";
                informacion += "ID_VERSIONAMIENTO = '" + ID_VERSIONAMIENTO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_VERSIONAMIENTO no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "Usuario = '" + Usuario + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    registro = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.OPER_HIST_MODIFICACIONES_MANUAL, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    registro = 0;
                }
            }

            return registro;
        }

        public Boolean DesactivarAdicionalPorId(Decimal ID_ADICIONAL, Conexion conexion)
        {
            String sql = null;
            Boolean ejecutar = true;
            int numRegistrosAfectados = 0;

            String informacion = String.Empty;

            sql = "usp_oper_adicionales_manual_desactivar ";
            informacion = sql;

            #region validaciones
            if (ID_ADICIONAL != 0)
            {
                sql += ID_ADICIONAL + ", ";
                informacion += "ID_ADICIONAL = '" + ID_ADICIONAL.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ADICIONAL no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_MOD = '" + Usuario + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    numRegistrosAfectados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.OPER_ADICIONALES_MANUAL, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    ejecutar = false;
                }
            }

            return ejecutar;
        }

        public Boolean ActualizarAdicional(Decimal ID_ADICIONAL,
            String TITULO,
            String DESCRIPCION,
            Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_oper_adicionales_manual_actualizar ";

            #region validaciones

            if (ID_ADICIONAL != 0)
            {
                sql += ID_ADICIONAL + ", ";
                informacion += "ID_ADICIONAL = '" + ID_ADICIONAL + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ADICIONAL no puede ser 0\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TITULO) == false)
            {
                sql += "'" + TITULO + "', ";
                informacion += "TITULO = '" + TITULO + "', ";
            }
            else
            {
                MensajeError += "El campo TITULO no puede ser 0\n";
                ejecutar = false;
            }


            if (String.IsNullOrEmpty(DESCRIPCION) == false)
            {
                sql += "'" + DESCRIPCION + "', ";
                informacion += "DESCRIPCION = '" + DESCRIPCION + "', ";
            }
            else
            {
                MensajeError += "El campo DESCRIPCION no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + Usuario.ToString() + "'";
            informacion += "Usuario = '" + Usuario + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.OPER_ADICIONALES_MANUAL, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    cantidadRegistrosActualizados = 0;
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public DataTable ObtenerInfoRegistroTabla(Dictionary<String, String> diccionarioCampos, String nombreTabla, String identificadorWhere, String valorIdentificadorWhere, Conexion conexion)
        {
            String camposConsulta = String.Empty;
            foreach (KeyValuePair<String, String> campo in diccionarioCampos)
            {
                if (String.IsNullOrEmpty(camposConsulta) == true)
                {
                    camposConsulta = "RTRIM(LTRIM(" + campo.Key + ")) AS " + campo.Key;
                }
                else
                {
                    camposConsulta += ", RTRIM(LTRIM(" + campo.Key + ")) AS " + campo.Key;
                }
            }

            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_armar_consulta_select ";
            sql += "'" + camposConsulta + "', ";
            sql += "'" + nombreTabla + "', ";
            sql += "'" + identificadorWhere + "', ";
            sql += "'" + valorIdentificadorWhere + "'";

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

            return _dataTable;
        }


        public Decimal DeterminarYRegistrarModificacionesEnCampos(Dictionary<String, String> diccionarioCampos, DataTable tablaDatosAnteriores, DataTable tablaDatosNuevos, Decimal ID_VERSIONAMIENTO, Decimal ID_EMPRESA, ListaSecciones area, AccionesManual accion, Conexion conexion)
        {
            DataRow filaDatoAnterior = tablaDatosAnteriores.Rows[0];
            DataRow filaDatoNuevo = tablaDatosNuevos.Rows[0];

            foreach (KeyValuePair<String, String> campo in diccionarioCampos)
            {
                if (filaDatoAnterior[campo.Key].Equals(filaDatoNuevo[campo.Key]) == false)
                {
                    if (ID_VERSIONAMIENTO == 0)
                    {
                        ID_VERSIONAMIENTO = AdicionarVersionamientoParaManualPorEmpresa(ID_EMPRESA, conexion);

                        if (ID_VERSIONAMIENTO <= 0)
                        {
                            ID_VERSIONAMIENTO = 0;
                            return -1;
                        }
                    }

                    if (AdicionarHistorialModificacionesHistorial(ID_EMPRESA, area.ToString(), campo.Key, campo.Value, filaDatoAnterior[campo.Key].ToString().Trim(), filaDatoNuevo[campo.Key].ToString().Trim(), accion.ToString(), ID_VERSIONAMIENTO, conexion) <= 0)
                    {
                        ID_VERSIONAMIENTO = 0;
                        return -1;
                    }
                }
            }

            return ID_VERSIONAMIENTO;
        }

        public Decimal RegistrarDesactivacionRegistroTabla(Decimal ID_EMPRESA, ListaSecciones area, AccionesManual accion, String campo, String descripcionCampo, String valor, Decimal ID_VERSIONAMIENTO, Conexion conexion)
        {
            if (ID_VERSIONAMIENTO == 0)
            {
                ID_VERSIONAMIENTO = AdicionarVersionamientoParaManualPorEmpresa(ID_EMPRESA, conexion);

                if (ID_VERSIONAMIENTO <= 0)
                {
                    ID_VERSIONAMIENTO = 0;
                    return -1;
                }
            }

            if (AdicionarHistorialModificacionesHistorial(ID_EMPRESA, area.ToString(), campo, descripcionCampo, valor, null, accion.ToString(), ID_VERSIONAMIENTO, conexion) <= 0)
            {
                ID_VERSIONAMIENTO = 0;
                return -1;
            }

            return ID_VERSIONAMIENTO;
        }

        public Decimal RegistrarInsersionRegistroTabla(Decimal ID_VERSIONAMIENTO, ListaSecciones area, String campo, String descripcionCampo, String valor, AccionesManual accion, Decimal ID_EMPRESA, Conexion conexion)
        {
            if (ID_VERSIONAMIENTO == 0)
            {
                ID_VERSIONAMIENTO = AdicionarVersionamientoParaManualPorEmpresa(ID_EMPRESA, conexion);

                if (ID_VERSIONAMIENTO <= 0)
                {
                    ID_VERSIONAMIENTO = 0;
                    return -1;
                }
            }

            if (AdicionarHistorialModificacionesHistorial(ID_EMPRESA, area.ToString(), campo, descripcionCampo, null, valor, accion.ToString(), ID_VERSIONAMIENTO, conexion) <= 0)
            {
                ID_VERSIONAMIENTO = 0;
                return -1;
            }

            return ID_VERSIONAMIENTO;
        }

        public Boolean ActualizarManualParaEmpresa(Decimal ID_EMPRESA, List<ManualServicio> listaAdicionales)
        {
            Decimal ID_VERSIONAMIENTO = 0;
            Decimal ID_ADICIONAL = 0;

            Boolean correcto = true;
            Boolean verificado = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                Dictionary<String, String> diccionarioCampos = new Dictionary<String, String>();
                diccionarioCampos.Add("TITULO", "Título");
                diccionarioCampos.Add("DESCRIPCION", "Descripción");
                DataTable tablaAdicionalesTodos = ObtenerAdicionalesPorEmpresaSinArea(ID_EMPRESA, conexion);

                for (int i = 0; i < tablaAdicionalesTodos.Rows.Count; i++)
                {
                    DataRow FilaAdicional = tablaAdicionalesTodos.Rows[i];
                    Decimal ID_ADICIONAL_TABLA = Convert.ToDecimal(FilaAdicional["ID_ADICIONAL"]);
                    String TITULO_TABLA = FilaAdicional["TITULO"].ToString().Trim();
                    String DESCRIPCION_TABLA = FilaAdicional["DESCRIPCION"].ToString().Trim();
                    ListaSecciones AREA_TABLA = (ListaSecciones)Enum.Parse(typeof(ListaSecciones), FilaAdicional["AREA"].ToString().Trim());

                    verificado = true;
                    foreach (ManualServicio adicional in listaAdicionales)
                    {
                        if (adicional.ID_ADICIONAL == ID_ADICIONAL_TABLA)
                        {
                            verificado = false;

                            DataTable tablaDatosAnteriores = ObtenerInfoRegistroTabla(diccionarioCampos, "oper_adicionales_manual", "ID_ADICIONAL", ID_ADICIONAL_TABLA.ToString(), conexion);

                            if (ActualizarAdicional(adicional.ID_ADICIONAL, adicional.TITULO, adicional.DESCRIPCION, conexion) == false)
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                                ID_VERSIONAMIENTO = 0;
                                break;
                            }

                            DataTable tablaDatosNuevos = ObtenerInfoRegistroTabla(diccionarioCampos, "oper_adicionales_manual", "ID_ADICIONAL", ID_ADICIONAL_TABLA.ToString(), conexion);
                            ID_VERSIONAMIENTO = DeterminarYRegistrarModificacionesEnCampos(diccionarioCampos, tablaDatosAnteriores, tablaDatosNuevos, ID_VERSIONAMIENTO, ID_EMPRESA, AREA_TABLA, AccionesManual.Actualizar, conexion);
                            if (ID_VERSIONAMIENTO == -1)
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                                ID_VERSIONAMIENTO = 0;
                                break;
                            }

                            break;
                        }
                    }

                    if ((verificado == true) && (correcto == true))
                    {
                        ID_VERSIONAMIENTO = RegistrarDesactivacionRegistroTabla(ID_EMPRESA, AREA_TABLA, AccionesManual.Eliminar, "ADICIONAL", "Descripción Adicional", TITULO_TABLA, ID_VERSIONAMIENTO, conexion);
                        if (ID_VERSIONAMIENTO == -1)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            ID_VERSIONAMIENTO = 0;
                            break;
                        }
                        else
                        {
                            if (DesactivarAdicionalPorId(ID_ADICIONAL_TABLA, conexion) == false)
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                                ID_VERSIONAMIENTO = 0;
                                break;
                            }
                        }
                    }
                }

                if (correcto == true)
                {
                    foreach (ManualServicio adicional in listaAdicionales)
                    {
                        if (adicional.ID_ADICIONAL == 0)
                        {
                            ID_VERSIONAMIENTO = RegistrarInsersionRegistroTabla(ID_VERSIONAMIENTO, adicional.AREA, "ADICIONAL", "Descripción Adicional", adicional.TITULO, AccionesManual.Insertar, adicional.ID_EMPRESA, conexion);
                            if (ID_VERSIONAMIENTO == -1)
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                                ID_VERSIONAMIENTO = 0;
                                break;
                            }
                            else
                            {
                                ID_ADICIONAL = AdicionarAdicionalPorEmpresaYVersionamiento(ID_EMPRESA, adicional.AREA.ToString(), adicional.TITULO, adicional.DESCRIPCION, ID_VERSIONAMIENTO, conexion);
                                if (ID_ADICIONAL <= 0)
                                {
                                    conexion.DeshacerTransaccion();
                                    correcto = false;
                                    ID_VERSIONAMIENTO = 0;
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
                MensajeError = ex.Message;
                conexion.DeshacerTransaccion();
                correcto = false;
                ID_VERSIONAMIENTO = 0;
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }

        public Decimal CrearManualParaEmpresa(Decimal ID_EMPRESA, List<ManualServicio> listaAdicionales)
        {
            Decimal ID_VERSIONAMIENTO = 0;
            Decimal ID_ADICIONAL = 0;
            Decimal ID_HISTORIAL = 0;

            Boolean correcto = true;
            Boolean verificado = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                ID_VERSIONAMIENTO = AdicionarVersionamientoParaManualPorEmpresa(ID_EMPRESA, conexion);

                if (ID_VERSIONAMIENTO <= 0)
                {
                    correcto = false;
                    ID_VERSIONAMIENTO = 0;
                    conexion.DeshacerTransaccion();
                }
                else
                {
                    DataTable tablaAdicionalesTodos = ObtenerAdicionalesPorEmpresaSinArea(ID_EMPRESA, conexion);
                    for (int i = 0; i < tablaAdicionalesTodos.Rows.Count; i++)
                    {
                        DataRow FilaAdicional = tablaAdicionalesTodos.Rows[i];
                        Decimal ID_ADICIONAL_TABLA = Convert.ToDecimal(FilaAdicional["ID_ADICIONAL"]);
                        String TITULO_TABLA = FilaAdicional["TITULO"].ToString().Trim();
                        String DESCRIPCION_TABLA = FilaAdicional["DESCRIPCION"].ToString().Trim();
                        ListaSecciones AREA_TABLA = (ListaSecciones)Enum.Parse(typeof(ListaSecciones), FilaAdicional["AREA"].ToString().Trim());

                        verificado = true;
                        foreach (ManualServicio adicional in listaAdicionales)
                        {
                            if (adicional.ID_ADICIONAL == ID_ADICIONAL_TABLA)
                            {
                                verificado = false;

                                if (ActualizarAdicional(adicional.ID_ADICIONAL, adicional.TITULO, adicional.DESCRIPCION, conexion) == false)
                                {
                                    conexion.DeshacerTransaccion();
                                    correcto = false;
                                    ID_VERSIONAMIENTO = 0;
                                }

                                break;
                            }
                        }

                        if ((verificado == true) && (correcto == true))
                        {
                            if (DesactivarAdicionalPorId(ID_ADICIONAL_TABLA, conexion) == false)
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                                ID_VERSIONAMIENTO = 0;
                                break;
                            }
                        }
                    }
                    foreach (ManualServicio adicional in listaAdicionales)
                    {
                        if (adicional.ID_ADICIONAL == 0)
                        {
                            ID_ADICIONAL = AdicionarAdicionalPorEmpresaYVersionamiento(ID_EMPRESA, adicional.AREA.ToString(), adicional.TITULO, adicional.DESCRIPCION, ID_VERSIONAMIENTO, conexion);
                            if (ID_ADICIONAL <= 0)
                            {
                                conexion.DeshacerTransaccion();
                                correcto = false;
                                ID_VERSIONAMIENTO = 0;
                                break;
                            }
                        }
                    }

                    if (correcto == true)
                    {
                        ID_HISTORIAL = AdicionarHistorialModificacionesHistorial(ID_EMPRESA, ListaSecciones.General.ToString(), null, null, null, null, AccionesManual.Crear.ToString(), ID_VERSIONAMIENTO, conexion);

                        if (ID_HISTORIAL <= 0)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            ID_VERSIONAMIENTO = 0;
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
                MensajeError = ex.Message;
                conexion.DeshacerTransaccion();
                ID_VERSIONAMIENTO = 0;
            }
            finally
            {
                conexion.Desconectar();
            }

            return ID_VERSIONAMIENTO;

        }

        public DataTable ObtenerModificacionesManualPorEmpresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_oper_hist_modificaciones_obtenerPorIdEmpresa ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo. \n";
                ejecutar = false;
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

            _dataTable.Columns.Add("CAMBIO");

            for (int i = 0; i < _dataTable.Rows.Count; i++)
            {
                DataRow _dataRow = _dataTable.Rows[i];

                if (_dataRow["ACCION"].ToString().Trim() == AccionesManual.Crear.ToString())
                {
                    _dataRow["CAMBIO"] = "Creación del manual";
                }
                else
                {
                    if (_dataRow["ACCION"].ToString().Trim() == AccionesManual.Actualizar.ToString())
                    {
                        _dataRow["CAMBIO"] = "Dato: " + _dataRow["DESCRIPCION_CAMPO"].ToString().Trim() + ", " + _dataRow["VALOR_NUEVO"].ToString().Trim();
                    }
                    else
                    {
                        if (_dataRow["ACCION"].ToString().Trim() == AccionesManual.Eliminar.ToString())
                        {
                            _dataRow["CAMBIO"] = "Dato: " + _dataRow["DESCRIPCION_CAMPO"].ToString().Trim() + ", " + _dataRow["VALOR_ANTERIOR"].ToString().Trim();
                        }
                        else
                        {
                            if (_dataRow["ACCION"].ToString().Trim() == AccionesManual.Insertar.ToString())
                            {
                                _dataRow["CAMBIO"] = "Dato: " + _dataRow["DESCRIPCION_CAMPO"].ToString().Trim() + ", " + _dataRow["VALOR_NUEVO"].ToString().Trim();
                            }
                        }
                    }
                }
            }
            return _dataTable;
        }

        public DataTable ObtenerModificacionesManualPorEmpresa(Decimal ID_EMPRESA, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_oper_hist_modificaciones_obtenerPorIdEmpresa ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA;
            }
            else
            {
                MensajeError += "El campo ID_EMPRESA no puede ser nulo. \n";
                ejecutar = false;
            }

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

            _dataTable.Columns.Add("CAMBIO");

            for (int i = 0; i < _dataTable.Rows.Count; i++)
            {
                DataRow _dataRow = _dataTable.Rows[i];

                if (_dataRow["ACCION"].ToString().Trim() == AccionesManual.Crear.ToString())
                {
                    _dataRow["CAMBIO"] = "Creación del manual";
                }
                else
                {
                    if (_dataRow["ACCION"].ToString().Trim() == AccionesManual.Actualizar.ToString())
                    {
                        _dataRow["CAMBIO"] = "Dato: " + _dataRow["DESCRIPCION_CAMPO"].ToString().Trim() + ", " + _dataRow["VALOR_NUEVO"].ToString().Trim();
                    }
                    else
                    {
                        if (_dataRow["ACCION"].ToString().Trim() == AccionesManual.Eliminar.ToString())
                        {
                            _dataRow["CAMBIO"] = "Dato: " + _dataRow["DESCRIPCION_CAMPO"].ToString().Trim() + ", " + _dataRow["VALOR_ANTERIOR"].ToString().Trim();
                        }
                        else
                        {
                            if (_dataRow["ACCION"].ToString().Trim() == AccionesManual.Insertar.ToString())
                            {
                                _dataRow["CAMBIO"] = "Dato: " + _dataRow["DESCRIPCION_CAMPO"].ToString().Trim() + ", " + _dataRow["VALOR_NUEVO"].ToString().Trim();
                            }
                        }
                    }
                }
            }
            return _dataTable;
        }

        public Boolean EmpresaConManualDeServicioCreado(Decimal ID_EMPRESA, Conexion conexion)
        {
            DataTable tablaVersionamientoManual = ObtenerVersionamientoManualPorEmpresa(ID_EMPRESA, conexion);

            if (tablaVersionamientoManual.Rows.Count <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public DataTable ObtenerArchivoManualServicioPorVersion(Decimal ID_VERSIONAMIENTO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_oper_docs_versionamiento_manual_obtenerPorIdVersionamiento ";

            if (ID_VERSIONAMIENTO != 0)
            {
                sql += ID_VERSIONAMIENTO;
            }
            else
            {
                MensajeError += "El campo ID_VERSIONAMIENTO no puede ser nulo. \n";
                ejecutar = false;
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

        public DataTable ObtenerArchivoManualServicioPorVersion(Decimal ID_VERSIONAMIENTO, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_oper_docs_versionamiento_manual_obtenerPorIdVersionamiento ";

            if (ID_VERSIONAMIENTO != 0)
            {
                sql += ID_VERSIONAMIENTO;
            }
            else
            {
                MensajeError += "El campo ID_VERSIONAMIENTO no puede ser nulo. \n";
                ejecutar = false;
            }

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
        #endregion metodos
    }
}
