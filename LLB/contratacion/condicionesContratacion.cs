using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.comercial;
using Brainsbits.LLB.operaciones;

namespace Brainsbits.LLB.contratacion
{
    public class condicionesContratacion
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        private Dictionary<String, String> diccionarioCamposVenPContratacion;
        private Dictionary<String, String> diccionarioCamposConRegElementosTrabajo;


        private Decimal _REGISTRO_CON_REG_ELEMENTO_TRABAJO = 0;
        private Decimal _REGISTRO_VEN_P_CONTRATACION = 0;
        private Decimal _ID_PRODUCTO = 0;
        private int _CANTIDAD = 0;
        private String _ID_PERIODICIDAD = null;
        private String _FACTURAR_A = null;
        private Decimal _VALOR = 0;
        private Boolean _PRIMERA_ENTREGA = false;
        private String _AJUSTE_A = null;
        private DateTime _FECHA_INICIO = new DateTime();
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

        public Decimal REGISTRO_CON_REG_ELEMENTO_TRABAJO
        {
            get { return _REGISTRO_CON_REG_ELEMENTO_TRABAJO; }
            set { _REGISTRO_CON_REG_ELEMENTO_TRABAJO = value; }
        }

        public Decimal REGISTRO_VEN_P_CONTRATACION
        {
            get { return _REGISTRO_VEN_P_CONTRATACION; }
            set { _REGISTRO_VEN_P_CONTRATACION = value; }
        }

        public Decimal ID_PRODUCTO
        {
            get { return _ID_PRODUCTO; }
            set { _ID_PRODUCTO = value; }
        }

        public int CANTIDAD
        {
            get { return _CANTIDAD; }
            set { _CANTIDAD = value; }
        }

        public String ID_PERIODICIDAD
        {
            get { return _ID_PERIODICIDAD; }
            set { _ID_PERIODICIDAD = value; }
        }

        public String FACTURAR_A
        {
            get { return _FACTURAR_A; }
            set { _FACTURAR_A = value; }
        }

        public Decimal VALOR
        {
            get { return _VALOR; }
            set { _VALOR = value; }
        }

        public Boolean PRIMERA_ENTREGA
        {
            get { return _PRIMERA_ENTREGA; }
            set { _PRIMERA_ENTREGA = value; }
        }

        public String AJUSTE_A
        {
            get { return _AJUSTE_A; }
            set { _AJUSTE_A = value; }
        }

        public DateTime FECHA_INICIO
        {
            get { return _FECHA_INICIO; }
            set { _FECHA_INICIO = value; }
        }
        #endregion propiedades

        #region constructores
        public condicionesContratacion(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;

            diccionarioCamposVenPContratacion = new Dictionary<string, string>();

            diccionarioCamposVenPContratacion.Add("DOC_TRAB", "Documentos Entregados al Trabajador");
            diccionarioCamposVenPContratacion.Add("OBS_CTE", "Requerimientos del Cliente");
            diccionarioCamposVenPContratacion.Add("RIESGO", "Riesgo");


            diccionarioCamposConRegElementosTrabajo = new Dictionary<string, string>();

            diccionarioCamposConRegElementosTrabajo.Add("CANTIDAD", "Cantidad");
            diccionarioCamposConRegElementosTrabajo.Add("PERIODICIDAD", "Periodicidad");
            diccionarioCamposConRegElementosTrabajo.Add("FACTURADO", "Indicador de estado de facturación");
            diccionarioCamposConRegElementosTrabajo.Add("FACTURADO_A", "Entidad a la que se debe facturar");
            diccionarioCamposConRegElementosTrabajo.Add("VALOR", "Valor a facturar");

        }

        public condicionesContratacion()
        {
        }
        #endregion

        #region metodos

        private Boolean ActualizarImplementosExamenes(Decimal registroVenPContratacion, List<condicionesContratacion> listaImplementosExamenes, Conexion conexion)
        {
            Boolean correcto = true;

            DataTable tablaImplementosExamenesActivos = obtenerImplementosYExamenesActivosPorRegistroVenPContratacion(registroVenPContratacion, conexion);

            Boolean implementoEncontrado = false;
            foreach (DataRow implementoExamenActivo in tablaImplementosExamenesActivos.Rows)
            {
                implementoEncontrado = false;
                Decimal registroConRegElementoTrabajoActivo = Convert.ToDecimal(implementoExamenActivo["REGISTRO_CON_REG_ELEMENTO_TRABAJO"]);

                foreach (condicionesContratacion implemento in listaImplementosExamenes)
                {
                    if (implemento.REGISTRO_CON_REG_ELEMENTO_TRABAJO == registroConRegElementoTrabajoActivo)
                    {
                        implementoEncontrado = true;
                        break;
                    }
                }

                if (implementoEncontrado == false)
                {
                    if (desactivarImplementoOExamen(registroConRegElementoTrabajoActivo, conexion) == false)
                    {
                        correcto = false;
                        break;
                    }
                }
            }

            foreach (condicionesContratacion implemento in listaImplementosExamenes)
            {
                if (implemento.REGISTRO_CON_REG_ELEMENTO_TRABAJO == 0)
                {
                    if (AdicionarDotacionExamenMedico(registroVenPContratacion, implemento.ID_PRODUCTO, implemento.CANTIDAD, implemento._ID_PERIODICIDAD, implemento.FACTURAR_A, implemento.VALOR, implemento.PRIMERA_ENTREGA, implemento.AJUSTE_A, implemento.FECHA_INICIO, conexion) <= 0)
                    {
                        correcto = false;
                        break;
                    }
                }
                else
                {
                    if (actualizarConRegElementosTrabajo(implemento.REGISTRO_CON_REG_ELEMENTO_TRABAJO, implemento.ID_PRODUCTO, implemento.CANTIDAD, implemento.ID_PERIODICIDAD, implemento.FACTURAR_A, implemento.VALOR, implemento.AJUSTE_A, implemento.FECHA_INICIO, conexion) == false)
                    {
                        correcto = false;
                        break;
                    }
                }
            }

            return correcto;
        }

        public Decimal AdicionarCondicionContratacionUnificada(Decimal ID_EMPRESA,
            String DOC_TRAB,
            Decimal ID_PERFIL,
            String RIESGO,
            String OBS_CTE,
            Decimal ID_SUB_C,
            Decimal ID_CENTRO_C,
            String ID_CIUDAD,
            Decimal ID_SERVICIO,
            List<condicionesContratacion> listaImplementosExamenes, DataTable clausulas)
        {
            Decimal registroVenPContratacion = 0;

            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                registroVenPContratacion = adicionarVenPContratacionUnificada(ID_EMPRESA, DOC_TRAB, ID_PERFIL, RIESGO, OBS_CTE, ID_SUB_C, ID_CENTRO_C, ID_CIUDAD, ID_SERVICIO, conexion);
                if (registroVenPContratacion <= 0)
                {
                    conexion.DeshacerTransaccion();
                    correcto = false;
                    registroVenPContratacion = 0;
                }
                else
                {
                    if (ActualizarImplementosExamenes(registroVenPContratacion, listaImplementosExamenes, conexion) == false)
                    {
                        conexion.DeshacerTransaccion();
                        correcto = false;
                        registroVenPContratacion = 0;
                    }
                    else if (AdicionarClausualas(clausulas, conexion).Equals(false))
                    {
                        conexion.DeshacerTransaccion();
                        correcto = false;
                        registroVenPContratacion = 0;
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
                registroVenPContratacion = 0;
            }
            finally
            {
                conexion.Desconectar();
            }

            return registroVenPContratacion;
        }

        private Decimal adicionarCondicionContratacionASubCentroCentroCCiudad(Decimal ID_EMPRESA, String DOC_TRAB, Decimal ID_PERFIL, String RIESGO, String OBS_CTE, Decimal ID_SUB_C, Decimal ID_CENTRO_C, String ID_CIUDAD, Conexion conexion)
        {
            Decimal registro = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_ven_p_contratacion_adicionar ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = " + ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DOC_TRAB)))
            {
                sql += "'" + DOC_TRAB + "', ";
                informacion += "DOC_TRAB = '" + DOC_TRAB.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo DOC_TRAB no puede ser nulo. \n";
                ejecutar = false;
            }

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
                informacion += "ID_PERFIL = " + ID_PERFIL.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(RIESGO) == false)
            {
                sql += "'" + RIESGO + "', ";
                informacion += "RIESGO = '" + RIESGO + "', ";
            }
            else
            {
                MensajeError = "El campo RIESGO no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(OBS_CTE)))
            {
                sql += "'" + OBS_CTE + "', ";
                informacion += "OBS_CTE = '" + OBS_CTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBS_CTE no puede ser nulo. \n";
                ejecutar = false;
            }

            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C + ", ";
                informacion += "ID_SUB_C = " + ID_SUB_C.ToString() + ", ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_SUB_C = null, ";
            }

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C + ", ";
                informacion += "ID_CENTRO_C = " + ID_CENTRO_C.ToString() + ", ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_CENTRO_C = null, ";
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = " + ID_CIUDAD + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_CIUDAD = null, ";
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                try
                {
                    registro = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_P_CONTRATACION, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria

                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    registro = 0;
                }
            }
            else
            {
                registro = 0;
            }

            return registro;
        }

        private bool EliminarClausulas(Decimal idPerfil, Conexion conexion)
        {
            bool eliminado = true;
            string sql = null;

            try
            {
                sql = "usp_con_reg_clausulas_perfil_eliminar ";
                sql += idPerfil.ToString();
                conexion.ExecuteNonQuery(sql);
            }
            catch (Exception e)
            {
                throw new Exception("Error al eliminar clausulas para el Perfil. Revisar: " + e.Message);
            }
            return eliminado;
        }

        private Boolean AdicionarClausualas(DataTable dataTable, Conexion conexion)
        {
            bool adicionado = true;
            string sql = null;

            foreach (DataRow dataRow in dataTable.Rows)
            {
                try
                {
                    sql = "usp_con_reg_clausulas_perfil_adicionar ";
                    sql += dataRow["ID_CLAUSULA"];
                    sql += ", " + dataRow["ID_PERFIL"];
                    sql += ", '" + Usuario + "'";
                    conexion.ExecuteNonQuery(sql);

                }
                catch (Exception e)
                {
                    throw new Exception("Error al registrar clausulas para el Perfil. Revisar: " + e.Message);
                }
            }
            return adicionado;
        }

        private Boolean ActualizarClausualas(DataTable dataTable, Conexion conexion)
        {
            bool actualizado = true;
            bool eliminado = false;
            string sql = null;

            foreach (DataRow dataRow in dataTable.Rows)
            {
                try
                {
                    if (eliminado.Equals(false))
                    {
                        sql = "usp_con_reg_clausulas_perfil_eliminar ";
                        sql += dataRow["ID_PERFIL"];
                        conexion.ExecuteNonQuery(sql);
                        eliminado = true;
                    }

                    sql = "usp_con_reg_clausulas_perfil_adicionar ";
                    sql += dataRow["ID_CLAUSULA"];
                    sql += ", " + dataRow["ID_PERFIL"];
                    sql += ", '" + Usuario + "'";
                    conexion.ExecuteNonQuery(sql);
                }
                catch (Exception e)
                {
                    throw new Exception("Error al registrar clausulas para el Perfil. Revisar: " + e.Message);
                }
            }
            return actualizado;
        }

        private Decimal adicionarVenPContratacionUnificada(Decimal ID_EMPRESA,
            String DOC_TRAB,
            Decimal ID_PERFIL,
            String RIESGO,
            String OBS_CTE,
            Decimal ID_SUB_C,
            Decimal ID_CENTRO_C,
            String ID_CIUDAD,
             Decimal ID_SERVICIO,
            Conexion conexion)
        {
            Decimal registro = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_ven_p_contratacion_adicionarUnificado ";
            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA + ", ";
                informacion += "ID_EMPRESA = '" + ID_EMPRESA.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DOC_TRAB)))
            {
                sql += "'" + DOC_TRAB + "', ";
                informacion += "DOC_TRAB = '" + DOC_TRAB.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo DOC_TRAB no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(OBS_CTE)))
            {
                sql += "'" + OBS_CTE + "', ";
                informacion += "OBS_CTE = '" + OBS_CTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBS_CTE no puede ser nulo. \n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
                informacion += "ID_PERFIL = " + ID_PERFIL.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C + ", ";
                informacion += "ID_SUB_C = " + ID_SUB_C.ToString() + ", ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_SUB_C = null, ";
            }

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C + ", ";
                informacion += "ID_CENTRO_C = " + ID_CENTRO_C.ToString() + ", ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_CENTRO_C = null, ";
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
                informacion += "ID_CIUDAD = " + ID_CIUDAD + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_CIUDAD = null, ";
            }

            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO + ", ";
                informacion += "ID_SERVICIO = '" + ID_SERVICIO.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "ID_SERVICIO = null, ";
            }

            if (String.IsNullOrEmpty(RIESGO) == false)
            {
                sql += "'" + RIESGO + "'";
                informacion += "RIESGO = '" + RIESGO + "'";
            }
            else
            {
                MensajeError = "El campo RIESGO no puede ser 0\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    registro = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_P_CONTRATACION, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria

                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    registro = 0;
                }
            }

            return registro;
        }

        private Decimal AdicionarClausula(decimal idPefil, Conexion conexion)
        {
            Decimal registro = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_reg_clausulas_perfil_adicionar ";
            if (idPefil != 0)
            {
                sql += idPefil + ", ";
                informacion += "idPefil = '" + idPefil.ToString() + "', ";
            }
            else
            {
                MensajeError = "El campo ID PERFIL no puede ser 0\n";
                ejecutar = false;
            }


            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                try
                {
                    registro = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_CLAUSULAS_PERFIL, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria

                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    registro = 0;
                }
            }
            return registro;
        }

        private Boolean actualizarCondicionContratacionPorRegistro(Decimal REGISTRO, String DOC_TRAB, String RIESGO, String OBS_CTE, Conexion conexion)
        {
            int numero_registros_actualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_ven_p_contratacion_actualizar ";

            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO = " + REGISTRO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo REGISTRO no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DOC_TRAB)))
            {
                sql += "'" + DOC_TRAB + "', ";
                informacion += "DOC_TRAB = '" + DOC_TRAB.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo DOC_TRAB no puede ser nulo. \n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(RIESGO) == false)
            {
                sql += "'" + RIESGO + "', ";
                informacion += "RIESGO = '" + RIESGO + "', ";
            }
            else
            {
                MensajeError = "El campo RIESGO no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(OBS_CTE)))
            {
                sql += "'" + OBS_CTE + "', ";
                informacion += "OBS_CTE = '" + OBS_CTE.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo OBS_CTE no puede ser nulo. \n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "' ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "'";

            if (ejecutar)
            {
                try
                {
                    numero_registros_actualizados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_P_CONTRATACION, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria

                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    numero_registros_actualizados = 0;
                }
            }
            else
            {
                numero_registros_actualizados = 0;
            }

            if (numero_registros_actualizados <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private Boolean actualizarConRegElementosTrabajo(Decimal REGISTRO,
            Decimal ID_PRODUCTO,
            Int32 CANTIDAD,
            String PERIODICIDAD,
            String FACTURADO_A,
            Decimal VALOR,
            String AJUSTE_A,
            DateTime FECHA_AJUSTE,
            Conexion conexion)
        {
            int numero_registros_actualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_reg_elementos_trabajo_actualizar ";
            if (REGISTRO != 0)
            {
                sql += REGISTRO + ", ";
                informacion += "REGISTRO = " + REGISTRO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo REGISTRO no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_PRODUCTO != 0)
            {
                sql += ID_PRODUCTO + ", ";
                informacion += "ID_PRODUCTO = " + ID_PRODUCTO.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PRODUCTO no puede ser 0\n";
                ejecutar = false;
            }

            if (CANTIDAD != 0)
            {
                sql += CANTIDAD + ", ";
                informacion += "CANTIDAD = " + CANTIDAD + ", ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "CANTIDAD = 'NULL', ";
            }

            if (!(String.IsNullOrEmpty(PERIODICIDAD)))
            {
                sql += "'" + PERIODICIDAD + "', ";
                informacion += "PERIODICIDAD = '" + PERIODICIDAD + "', ";
            }
            else
            {
                MensajeError += "El campo PERIODICIDAD no puede ser nulo. \n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(FACTURADO_A) == false)
            {
                sql += "'" + FACTURADO_A + "', ";
                informacion += "FACTURADO_A = '" + FACTURADO_A + "', ";
            }
            else
            {
                MensajeError = "El campo FACTURADO_A no puede ser 0\n";
                ejecutar = false;
            }

            if (VALOR != 0)
            {
                sql += VALOR.ToString().Replace(",", ".") + ", ";
                informacion += "VALOR = '" + VALOR.ToString().Replace(',', '.') + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "VALOR = 'NULL', ";
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_MOD = '" + Usuario.ToString() + "', ";

            if (String.IsNullOrEmpty(AJUSTE_A) == false)
            {
                sql += "'" + AJUSTE_A + "', ";
                informacion += "AJUSTE_A = '" + AJUSTE_A + "', ";
            }
            else
            {
                MensajeError = "El campo AJUSTE_A no puede ser 0\n";
                ejecutar = false;
            }

            if (FECHA_AJUSTE == new DateTime())
            {
                sql += "NULL";
                informacion += "FECHA_AJUSTE = 'NULL'";
            }
            else
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_AJUSTE) + "'";
                informacion += "FECHA_AJUSTE = '" + FECHA_AJUSTE.ToShortDateString() + "'";
            }

            if (ejecutar)
            {
                try
                {
                    numero_registros_actualizados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_ELEMEMENTOS_TRABAJO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria

                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    numero_registros_actualizados = 0;
                }
            }
            else
            {
                numero_registros_actualizados = 0;
            }

            if (numero_registros_actualizados <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public Boolean ActualizarCondicionContratacion(Decimal REGISTRO_CONDICION,
            Decimal ID_PERFIL,
            Decimal ID_EMPRESA,
            Decimal ID_SUB_C,
            Decimal ID_CENTRO_C,
            String ID_CIUDAD,
            String DOC_TRAB,
            String RIESGO,
            String OBS_CTE,
            List<condicionesContratacion> listaImplementosExamenes, DataTable clausulas)
        {
            Decimal ID_VERSIONAMIENTO = 0;
            Boolean realizarVersionamientoManual = true;
            Boolean continuarNormalmente = true;

            Boolean correcto = true;
            ManualServicio _manual = new ManualServicio(Empresa, Usuario);
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                realizarVersionamientoManual = _manual.EmpresaConManualDeServicioCreado(ID_EMPRESA, conexion); 

                DataTable tablaDatosAnteriores = _manual.ObtenerInfoRegistroTabla(diccionarioCamposVenPContratacion, "VEN_P_CONTRATACION", "REGISTRO", REGISTRO_CONDICION.ToString(), conexion);

                if (actualizarCondicionContratacionPorRegistro(REGISTRO_CONDICION, DOC_TRAB, RIESGO, OBS_CTE, conexion) == false)
                {
                    conexion.DeshacerTransaccion();
                    correcto = false;
                }
                else
                {
                    DataTable tablaDatosNuevos = _manual.ObtenerInfoRegistroTabla(diccionarioCamposVenPContratacion, "VEN_P_CONTRATACION", "REGISTRO", REGISTRO_CONDICION.ToString(), conexion);

                    if (realizarVersionamientoManual == true)
                    {
                        ID_VERSIONAMIENTO = _manual.DeterminarYRegistrarModificacionesEnCampos(diccionarioCamposVenPContratacion, tablaDatosAnteriores, tablaDatosNuevos, ID_VERSIONAMIENTO, ID_EMPRESA, ManualServicio.ListaSecciones.Contratacion, ManualServicio.AccionesManual.Actualizar, conexion);
                        if (ID_VERSIONAMIENTO == -1)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                            continuarNormalmente = false;
                            ID_VERSIONAMIENTO = 0;
                        }
                    }

                    if (correcto == true && continuarNormalmente == true)
                    {
                        if (ActualizarImplementosExamenes(REGISTRO_CONDICION, listaImplementosExamenes, conexion) == false)
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
                        }
                        else if (ActualizarClausualas(clausulas, conexion).Equals(false))
                        {
                            conexion.DeshacerTransaccion();
                            correcto = false;
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
                MensajeError = ex.Message;
                correcto = false;
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }

        public DataTable ObtenerCondicionContratacionPorIdPerfil(Decimal ID_PERFIL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_p_contratacion_buscarPorIdPerfil ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL;
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
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

        public DataTable ObtenerExamenesVSCargosPorIdEmpresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_examenes_vs_cargos_por_id_empresa ";

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

        public DataTable ObtenerBancosVSCiudadesPorIdEmpresa(Decimal ID_EMPRESA)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_obtener_bancos_vs_ciudad_por_id_empresa ";

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

        public DataTable ObtenerCondicionContratacionPorIdPerfil(Decimal ID_PERFIL, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_p_contratacion_buscarPorIdPerfil ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL;
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
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

        public DataTable ObtenerCondicionContratacionPorRegistro(Decimal REGISTRO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_p_contratacion_obtener_por_registro ";

            if (REGISTRO != 0)
            {
                sql += REGISTRO;
            }
            else
            {
                MensajeError = "El campo REGISTRO no puede ser 0\n";
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

        public DataTable ObtenerCondicionContratacionPorRegistro(Decimal REGISTRO, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_p_contratacion_obtener_por_registro ";

            if (REGISTRO != 0)
            {
                sql += REGISTRO;
            }
            else
            {
                MensajeError = "El campo REGISTRO no puede ser 0\n";
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

        public DataTable ObtenerCondicionComercialPorIdPerfilIdSubC(Decimal ID_PERFIL, Decimal ID_SUB_C)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_p_contratacion_buscarPorIdPerfilIdSubC ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
                ejecutar = false;
            }

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

        public DataTable ObtenerCondicionContratacionPorIdPerfilIdSubCIdServicio(Decimal ID_PERFIL, Decimal ID_SUB_C, Decimal ID_SERVICIO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_p_contratacion_buscarPorIdPerfilIdSubCIdServicio ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C + ", ";
            }
            else
            {
                MensajeError = "El campo ID_SUB_C no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO;
            }
            else
            {
                MensajeError = "El campo ID_SERVICIO no puede ser 0\n";
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

        public DataTable ObtenerCondicionContratacionPorUbicacion(Decimal ID_PERFIL, String ID_CIUDAD, Decimal ID_CENTRO_C, Decimal ID_SUB_C, Decimal ID_SERVICIO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_p_contratacion_ObtenerPorUbicacion ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
            }
            else
            {
                sql += "NULL, ";
            }

            if (ID_CENTRO_C != 0)
            {
                sql += ID_SUB_C + ", ";
            }
            else
            {
                sql += "NULL, ";
            }

            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C + ", ";
            }
            else
            {
                sql += "NULL, ";
            }

            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO;
            }
            else
            {
                sql += "NULL";
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

        public DataTable ObtenerCondicionComercialPorIdPerfilIdCentroC(Decimal ID_PERFIL, Decimal ID_CENTRO_C)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_p_contratacion_buscarPorIdPerfilIdCentroC ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
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

        public DataTable ObtenerCondicionContratacionPorIdPerfilIdCentroCIdServicio(Decimal ID_PERFIL, Decimal ID_CENTRO_C, Decimal ID_SERVICIO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_p_contratacion_buscarPorIdPerfilIdCentroCIdServicio ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C + ", ";
            }
            else
            {
                MensajeError = "El campo ID_CENTRO_C no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO;
            }
            else
            {
                MensajeError = "El campo ID_SERVICIO no puede ser 0\n";
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

        public DataTable ObtenerCondicionComercialPorIdPerfilIdCiudad(Decimal ID_PERFIL, String ID_CIUDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_p_contratacion_buscarPorIdPerfilIdCiudad ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "'";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo. \n";
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

        public DataTable ObtenerCondicionContratacionPorIdPerfilIdCiudadIdServicio(Decimal ID_PERFIL, String ID_CIUDAD, Decimal ID_SERVICIO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_p_contratacion_buscarPorIdPerfilIdCiudadIdServicio ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo. \n";
                ejecutar = false;
            }

            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO;
            }
            else
            {
                MensajeError = "El campo ID_SERVICIO no puede ser 0\n";
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

        public DataTable obtenerProductosSegunTipoServicioComplementario(Decimal TIPO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_reg_producto_obtener_por_tipo ";

            if (TIPO != 0)
            {
                sql += TIPO.ToString();
            }
            else
            {
                MensajeError = "El campo TIPO no puede ser 0\n";
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

        public DataTable obtenerProductoTipoExamenMedico(Decimal TIPO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_alm_reg_producto_obtener_examenes_medicos ";

            #region validaciones
            if (TIPO != 0)
            {
                sql += TIPO;
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo TIPO no puede ser nulo.";
            }
            #endregion validaciones
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

        public DataTable obtenerSubCentrosConServicioEspecialConfigurado(Decimal ID_EMPRESA, Decimal ID_SERVICIO_COMPLEMENTARIO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_ven_sub_centros_obtener_con_servicio_complementario_asociado ";

            if (ID_EMPRESA != 0)
            {
                sql += ID_EMPRESA.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_EMPRESA no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_SERVICIO_COMPLEMENTARIO != 0)
            {
                sql += ID_SERVICIO_COMPLEMENTARIO.ToString();
            }
            else
            {
                MensajeError = "El campo ID_SERVICIO_COMPLEMENTARIO no puede ser 0\n";
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

        private Decimal obtenerIdConRegServicioPerfil(Decimal ID_SUB_C, Decimal ID_CENTRO_C, String ID_CIUDAD, Decimal ID_SERVICIO, Decimal ID_PERFIL, Conexion conexion)
        {
            Decimal registro = 0;

            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_servicio_perfil_obtener_id ";

            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C.ToString() + ", ";
            }
            else
            {
                sql += "null, ";
            }

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C.ToString() + ", ";
            }
            else
            {
                sql += "null, ";
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
            }
            else
            {
                sql += "null, ";
            }

            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO.ToString() + ", ";
            }
            else
            {
                sql += "null, ";
            }

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL.ToString() + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";

            if (ejecutar == true)
            {
                try
                {
                    registro = Convert.ToDecimal(conexion.ExecuteScalar(sql));
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            return registro;
        }

        private Decimal AdicionarDotacionExamenMedico(Decimal REGISTRO_VEN_P_CONTRATACION,
            Decimal ID_PRODUCTO,
            Int32 CANTIDAD,
            String PERIODICIDAD,
            String FACTURADO_A,
            Decimal VALOR,
            Boolean PRIMERA_ENTREGA,
            String AJUSTE_A,
            DateTime FECHA_AJUSTE,
            Conexion conexion)
        {
            Decimal registro = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_reg_elementos_trabajos_adicionar ";

            if (REGISTRO_VEN_P_CONTRATACION != 0)
            {
                sql += REGISTRO_VEN_P_CONTRATACION + ", ";
                informacion += "REGISTRO_VEN_P_CONTRATACION = '" + REGISTRO_VEN_P_CONTRATACION.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO_VEN_P_CONTRATACION no puede ser nulo. \n";
                ejecutar = false;
            }

            if (ID_PRODUCTO != 0)
            {
                sql += ID_PRODUCTO + ", ";
                informacion += "ID_PRODUCTO = '" + ID_PRODUCTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PRODUCTO no puede ser nulo. \n";
                ejecutar = false;
            }

            if (CANTIDAD != 0)
            {
                sql += CANTIDAD + ", ";
                informacion += "CANTIDAD = '" + CANTIDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo CANTIDAD no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(PERIODICIDAD)))
            {
                sql += "'" + PERIODICIDAD + "', ";
                informacion += "PERIODICIDAD = '" + PERIODICIDAD.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo PERIODICIDAD no puede ser nulo. \n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(FACTURADO_A)))
            {
                sql += "'" + FACTURADO_A + "', ";
                informacion += "FACTURADO_A = '" + FACTURADO_A + "', ";
            }
            else
            {
                MensajeError += "El campo FACTURADO_A no puede ser nulo. \n";
                ejecutar = false;
            }

            if (VALOR != 0)
            {
                sql += VALOR.ToString().Replace(".", "").Replace(",", ".") + ", ";
            }
            else
            {
                sql += "null, ";
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario + "', ";

            if (PRIMERA_ENTREGA == true)
            {
                sql += "'True', ";
            }
            else
            {
                sql += "'False', ";
            }

            if (!(String.IsNullOrEmpty(AJUSTE_A)))
            {
                sql += "'" + AJUSTE_A + "', ";
                informacion += "AJUSTE_A = '" + AJUSTE_A + "', ";
            }
            else
            {
                MensajeError += "El campo AJUSTE_A no puede ser nulo. \n";
                ejecutar = false;
            }

            if (FECHA_AJUSTE != new DateTime())
            {
                sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA_AJUSTE) + "'";
                informacion += "FECHA_AJUSTE = '" + FECHA_AJUSTE.ToShortDateString() + "'";
            }
            else
            {
                sql += "NULL";
                informacion += "FECHA_AJUSTE = 'NULL'";
            }

            if (ejecutar)
            {
                try
                {
                    registro = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_ELEMEMENTOS_TRABAJO, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    registro = 0;
                }
            }
            else
            {
                registro = 0;
            }

            return registro;
        }

        public DataTable obtenerClausulasPorPerfil(Decimal ID_PERFIL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_clausulas_perfil_obtener_por_id_perfil ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL;
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
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

        public DataTable obtenerClausulasPorPerfil(Decimal ID_PERFIL, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_clausulas_perfil_obtener_por_id_perfil ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL;
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
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

        public DataTable obtenerClausulasPorIdCluasula(Decimal ID_CON_REG_CLUASULAS_PERFIL)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_clausulas_perfil_obtener_por_id_clausula ";

            if (ID_CON_REG_CLUASULAS_PERFIL != 0)
            {
                sql += ID_CON_REG_CLUASULAS_PERFIL;
            }
            else
            {
                MensajeError = "El campo ID_CON_REG_CLUASULAS_PERFIL no puede ser 0\n";
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

        public DataTable obtenerImplementosYExamenesPorPerfilCiudad(Decimal ID_PERFIL, String ID_CIUDAD, Decimal ID_SERVICIO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_elementos_trabajo_obtener_implementos_y_examenes_configurados_ciudad ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ID_CIUDAD) == false)
            {
                sql += "'" + ID_CIUDAD + "', ";
            }
            else
            {
                MensajeError = "El campo ID_CIUDAD no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO;
            }
            else
            {
                sql += "null";
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

        public DataTable obtenerImplementosYExamenesPorPerfilCentroC(Decimal ID_PERFIL, Decimal ID_CENTRO_C, Decimal ID_SERVICIO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_elementos_trabajo_obtener_implementos_y_examenes_configurados_centro_c ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C + ", ";
            }
            else
            {
                MensajeError = "El campo ID_CENTRO_C no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO;
            }
            else
            {
                sql += "null";
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

        public DataTable obtenerImplementosYExamenesActivosPorRegistroVenPContratacion(Decimal registroVenPContratacion, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_elementos_trabajo_obtenerActivosUnificados ";

            if (registroVenPContratacion != 0)
            {
                sql += registroVenPContratacion;
            }
            else
            {
                MensajeError = "El campo REGISTRO_VEN_P_CONTRATACION no puede ser 0\n";
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

        public DataTable obtenerImplementosOExamenesActivosPorRegistroVenPContratacionYTipo(Decimal registroVenPContratacion,
            String TIPO_SERVICIO_COMPLEMENTARIO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_elementos_trabajo_obtenerImplementosOExamenesActivosUnificados ";

            if (registroVenPContratacion != 0)
            {
                sql += registroVenPContratacion + ", ";
            }
            else
            {
                MensajeError = "El campo REGISTRO_VEN_P_CONTRATACION no puede ser 0\n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(TIPO_SERVICIO_COMPLEMENTARIO) == false)
            {
                sql += "'" + TIPO_SERVICIO_COMPLEMENTARIO + "'";
            }
            else
            {
                MensajeError = "El campo TIPO_SERVICIO_COMPLEMENTARIO no puede ser 0 o vacio.\n";
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

        public DataTable obtenerImplementosYExamenesPorPerfilSubC(Decimal ID_PERFIL, Decimal ID_SUB_C, Decimal ID_SERVICIO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_elementos_trabajo_obtener_implementos_y_examenes_configurados_suub_c ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C + ", ";
            }
            else
            {
                MensajeError = "El campo ID_SUB_C no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO;
            }
            else
            {
                sql += "null";
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

        public Decimal AdicionarClausula(Decimal ID_PERFIL_1, String NOMBRE_1, String ENCABEZADO_1, String DESCRIPCION_1, Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_clausulas_adicionar ";

            #region validaciones
            if (ID_PERFIL_1 != 0)
            {
                sql += ID_PERFIL_1 + ", ";
                informacion += "ID_PERFIL = '" + ID_PERFIL_1.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PERFIL no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(NOMBRE_1)))
            {
                sql += "'" + NOMBRE_1 + "', ";
                informacion += "NOMBRE = '" + NOMBRE_1.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo NOMBRE no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ENCABEZADO_1)))
            {
                sql += "'" + ENCABEZADO_1 + "', ";
                informacion += "ENCABEZADO = '" + ENCABEZADO_1.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ENCABEZADO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(DESCRIPCION_1)))
            {
                sql += "'" + DESCRIPCION_1 + "', ";
                informacion += "DESCRIPCION = '" + DESCRIPCION_1.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo DESCRIPCION no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteEscalarParaAdicionarClausula(ID_PERFIL_1, NOMBRE_1, ENCABEZADO_1, DESCRIPCION_1, Usuario);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_CLAUSULAS_PERFIL, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (!(String.IsNullOrEmpty(identificador))) return Convert.ToDecimal(identificador);
            else return 0;
        }

        public DataTable obtenerExamenesBasicosFaltantesParaPerfilServicioYCiudad(Decimal ID_PERFIL, String ID_CIUDAD, Decimal ID_SERVICIO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_producto_obtener_examenes_basicos_faltante_para_perfil_Y_ciudad ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO;
            }
            else
            {
                sql += "NULL";
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

        public DataTable obtenerExamenesBasicosFaltantesParaPerfilServicioYCentroC(Decimal ID_PERFIL, Decimal ID_CENTRO_C, Decimal ID_SERVICIO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_producto_obtener_examenes_basicos_faltante_para_perfil_Y_ciudad ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C + ", ";
            }
            else
            {
                MensajeError = "El campo ID_CENTRO_C no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO;
            }
            else
            {
                sql += "NULL";
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

        public DataTable obtenerExamenesBasicosFaltantesParaPerfilServicioYSubC(Decimal ID_PERFIL, Decimal ID_SUB_C, Decimal ID_SERVICIO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_producto_obtener_examenes_basicos_faltante_para_perfil_Y_ciudad ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C + ", ";
            }
            else
            {
                MensajeError = "El campo ID_SUB_C no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO;
            }
            else
            {
                sql += "NULL";
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

        public DataTable obtenerExamenesBasicosTodos(String TIPO_SERVICIO_COMPLEMENTARIO) 
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_producto_obtener_examenes_basicos_todos ";

            #region validaciones
            if (String.IsNullOrEmpty(TIPO_SERVICIO_COMPLEMENTARIO) == false)
            {
                sql += "'" + TIPO_SERVICIO_COMPLEMENTARIO + "'";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El TIPO DE SERVICIO COMPLEMENTARIO no puede ser vacio.";
            }
            #endregion validaciones

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

        public DataTable obtenerProductoPorIdProducto(Decimal ID_PRODUCTO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_producto_obtener_producto_por_id_producto ";

            if (ID_PRODUCTO != 0)
            {
                sql += ID_PRODUCTO;
            }
            else
            {
                MensajeError = "El campo ID_PRODUCTO no puede ser 0\n";
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

        public Boolean desactivarClausula(Decimal ID_CON_REG_CLAUSULAS_PERFIL, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_reg_clausulas_perfil_desactivar ";

            if (ID_CON_REG_CLAUSULAS_PERFIL != 0)
            {
                sql += ID_CON_REG_CLAUSULAS_PERFIL;
                informacion += "ID_CON_REG_CLAUSULAS_PERFIL = '" + ID_CON_REG_CLAUSULAS_PERFIL.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo ID_CON_REG_CLAUSULAS_PERFIL no puede ser 0\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);
                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_CLAUSULAS_PERFIL, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public Boolean desactivarImplementoOExamen(Decimal REGISTRO, Conexion conexion)
        {
            Int32 cantidadRegistrosActualizados = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_reg_elementos_trabajo_desactivar_implemento_o_examen ";

            if (REGISTRO != 0)
            {
                sql += REGISTRO;
                informacion += "REGISTRO = '" + REGISTRO.ToString() + "'";
            }
            else
            {
                MensajeError = "El campo REGISTRO no puede ser 0\n";
                ejecutar = false;
            }

            if (ejecutar)
            {
                try
                {
                    cantidadRegistrosActualizados = conexion.ExecuteNonQuery(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_ELEMEMENTOS_TRABAJO, tabla.ACCION_ACTUALIZAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                }
            }

            if (cantidadRegistrosActualizados > 0) return true;
            else return false;
        }

        public DataTable ObtenerExamenesParametrizadosParaCiudad(Decimal ID_PERFIL, String ID_CIUDAD, Decimal ID_SERVICIO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_elementos_trabajo_obtener_examenes_configurados_ciudad ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO;
            }
            else
            {
                sql += "null";
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

        public DataTable ObtenerExamenesParametrizadosParaCiudad(Decimal ID_PERFIL, String ID_CIUDAD, Decimal ID_SERVICIO, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_elementos_trabajo_obtener_examenes_configurados_ciudad ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(ID_CIUDAD)))
            {
                sql += "'" + ID_CIUDAD + "', ";
            }
            else
            {
                MensajeError += "El campo ID_CIUDAD no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO;
            }
            else
            {
                sql += "null";
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

        public DataTable ObtenerExamenesParametrizadosParaCentroC(Decimal ID_PERFIL, Decimal ID_CENTRO_C, Decimal ID_SERVICIO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_elementos_trabajo_obtener_examenes_configurados_centro_c ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C + ", ";
            }
            else
            {
                MensajeError = "El campo ID_CENTRO_C no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO;
            }
            else
            {
                sql += "null";
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

        public DataTable ObtenerExamenesParametrizadosParaCentroC(Decimal ID_PERFIL, Decimal ID_CENTRO_C, Decimal ID_SERVICIO, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_elementos_trabajo_obtener_examenes_configurados_centro_c ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_CENTRO_C != 0)
            {
                sql += ID_CENTRO_C + ", ";
            }
            else
            {
                MensajeError = "El campo ID_CENTRO_C no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO;
            }
            else
            {
                sql += "null";
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

        public DataTable ObtenerExamenesParametrizadosParaSubC(Decimal ID_PERFIL, Decimal ID_SUB_C, Decimal ID_SERVICIO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_elementos_trabajo_obtener_examenes_configurados_sub_c ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C + ", ";
            }
            else
            {
                MensajeError = "El campo ID_SUB_C no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO;
            }
            else
            {
                sql += "null";
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

        public DataTable ObtenerExamenesParametrizadosParaSubC(Decimal ID_PERFIL, Decimal ID_SUB_C, Decimal ID_SERVICIO, Conexion conexion)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            Boolean ejecutar = true;

            sql = "usp_con_reg_elementos_trabajo_obtener_examenes_configurados_sub_c ";

            if (ID_PERFIL != 0)
            {
                sql += ID_PERFIL + ", ";
            }
            else
            {
                MensajeError = "El campo ID_PERFIL no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_SUB_C != 0)
            {
                sql += ID_SUB_C + ", ";
            }
            else
            {
                MensajeError = "El campo ID_SUB_C no puede ser 0\n";
                ejecutar = false;
            }

            if (ID_SERVICIO != 0)
            {
                sql += ID_SERVICIO;
            }
            else
            {
                sql += "null";
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

        public Boolean replicarCondicionesContratacionDesdeCiudadACiudades(Decimal ID_EMPRESA, Decimal ID_PERFIL, String ID_CIUDAD_FUENTE, String[] IDS_CIUDADES_REPLICAR)
        {
            DataTable tablaCondicionesContratacionFuente = ObtenerCondicionComercialPorIdPerfilIdCiudad(ID_PERFIL, ID_CIUDAD_FUENTE);
            DataRow filaCondicionContratacionFuente = tablaCondicionesContratacionFuente.Rows[0];

            DataTable tablaImplementosFuente = obtenerImplementosYExamenesPorPerfilCiudad(ID_PERFIL, ID_CIUDAD_FUENTE, 0);

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                Decimal ID_CON_REG_SERVICIO_PERFIL = 0;
                Decimal ID_SERVICIO_COMPLEMENTARIO = 0;
                Decimal CANTIDAD = 0;
                Decimal VALOR = 0;
                Decimal ID_PRODUCTO = 0;
                foreach (String ID_CIUDAD in IDS_CIUDADES_REPLICAR)
                {
                    if (adicionarCondicionContratacionASubCentroCentroCCiudad(ID_EMPRESA, filaCondicionContratacionFuente["DOC_TRAB"].ToString().Trim(), ID_PERFIL, filaCondicionContratacionFuente["RIESGO"].ToString(), filaCondicionContratacionFuente["OBS_CTE"].ToString().Trim(), 0, 0, ID_CIUDAD, conexion) == 0)
                    {
                        conexion.DeshacerTransaccion();
                        return false;
                    }

                    ID_CON_REG_SERVICIO_PERFIL = obtenerIdConRegServicioPerfil(0, 0, ID_CIUDAD, 0, ID_PERFIL, conexion);
                    if (ID_CON_REG_SERVICIO_PERFIL == 0)
                    {
                        conexion.DeshacerTransaccion();
                        return false;
                    }
                    else
                    {
                        foreach (DataRow filaFuente in tablaImplementosFuente.Rows)
                        {
                            try
                            {
                                ID_SERVICIO_COMPLEMENTARIO = Convert.ToDecimal(filaFuente["ID_SERVICIO_COMPLEMENTARIO"]);
                            }
                            catch
                            {
                                ID_SERVICIO_COMPLEMENTARIO = 0;
                            }
                            try
                            {
                                CANTIDAD = Convert.ToDecimal(filaFuente["CANTIDAD"]);
                            }
                            catch
                            {
                                CANTIDAD = 0;
                            }
                            try
                            {
                                VALOR = Convert.ToDecimal(filaFuente["VALOR"]);
                            }
                            catch
                            {
                                VALOR = 0;
                            }
                            try
                            {
                                ID_PRODUCTO = Convert.ToDecimal(filaFuente["ID_PRODUCTO"]);
                            }
                            catch
                            {
                                ID_PRODUCTO = 0;
                            }

                        }
                    }
                }

                conexion.AceptarTransaccion();
                return true;
            }
            catch (Exception ex)
            {
                conexion.DeshacerTransaccion();
                MensajeError = ex.Message;
                return false;
            }
            finally
            {
                conexion.Desconectar();
            }
        }

        public Boolean replicarCondicionesContratacionDesdeCiudadACiudadesConServicio(Decimal ID_EMPRESA, Decimal ID_PERFIL, String ID_CIUDAD_FUENTE, Decimal ID_SERVICIO_FUENTE, String[] IDS_CIUDADES_REPLICAR, Decimal[] IDS_SERVICIOS_REPLICAR)
        {
            DataTable tablaCondicionesContratacionFuente = ObtenerCondicionContratacionPorIdPerfilIdCiudadIdServicio(ID_PERFIL, ID_CIUDAD_FUENTE, ID_SERVICIO_FUENTE);
            DataRow filaCondicionContratacionFuente = tablaCondicionesContratacionFuente.Rows[0];

            DataTable tablaImplementosFuente = obtenerImplementosYExamenesPorPerfilCiudad(ID_PERFIL, ID_CIUDAD_FUENTE, ID_SERVICIO_FUENTE);

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                Decimal ID_CON_REG_SERVICIO_PERFIL = 0;
                Decimal ID_SERVICIO_COMPLEMENTARIO = 0;
                Decimal CANTIDAD = 0;
                Decimal VALOR = 0;
                Decimal ID_PRODUCTO = 0;

                for (int i = 0; i < IDS_CIUDADES_REPLICAR.Length; i++)
                {
                    ID_CON_REG_SERVICIO_PERFIL = obtenerIdConRegServicioPerfil(0, 0, IDS_CIUDADES_REPLICAR[i], IDS_SERVICIOS_REPLICAR[i], ID_PERFIL, conexion);
                    if (ID_CON_REG_SERVICIO_PERFIL == 0)
                    {
                        conexion.DeshacerTransaccion();
                        return false;
                    }
                    else
                    {
                        foreach (DataRow filaFuente in tablaImplementosFuente.Rows)
                        {
                            try
                            {
                                ID_SERVICIO_COMPLEMENTARIO = Convert.ToDecimal(filaFuente["ID_SERVICIO_COMPLEMENTARIO"]);
                            }
                            catch
                            {
                                ID_SERVICIO_COMPLEMENTARIO = 0;
                            }
                            try
                            {
                                CANTIDAD = Convert.ToDecimal(filaFuente["CANTIDAD"]);
                            }
                            catch
                            {
                                CANTIDAD = 0;
                            }
                            try
                            {
                                VALOR = Convert.ToDecimal(filaFuente["VALOR"]);
                            }
                            catch
                            {
                                VALOR = 0;
                            }
                            try
                            {
                                ID_PRODUCTO = Convert.ToDecimal(filaFuente["ID_PRODUCTO"]);
                            }
                            catch
                            {
                                ID_PRODUCTO = 0;
                            }

                        }
                    }
                }

                conexion.AceptarTransaccion();
                return true;
            }
            catch (Exception ex)
            {
                conexion.DeshacerTransaccion();
                MensajeError = ex.Message;
                return false;
            }
            finally
            {
                conexion.Desconectar();
            }
        }

        public Boolean replicarCondicionesContratacionDesdeCentroCACentrosC(Decimal ID_EMPRESA, Decimal ID_PERFIL, Decimal ID_CENTRO_C_FUENTE, Decimal[] IDS_CENTROS_C_REPLICAR)
        {
            DataTable tablaCondicionesContratacionFuente = ObtenerCondicionComercialPorIdPerfilIdCentroC(ID_PERFIL, ID_CENTRO_C_FUENTE);
            DataRow filaCondicionContratacionFuente = tablaCondicionesContratacionFuente.Rows[0];

            DataTable tablaImplementosFuente = obtenerImplementosYExamenesPorPerfilCentroC(ID_PERFIL, ID_CENTRO_C_FUENTE, 0);

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                Decimal ID_CON_REG_SERVICIO_PERFIL = 0;
                Decimal ID_SERVICIO_COMPLEMENTARIO = 0;
                Decimal CANTIDAD = 0;
                Decimal VALOR = 0;
                Decimal ID_PRODUCTO = 0;
                foreach (Decimal ID_CENTRO_C in IDS_CENTROS_C_REPLICAR)
                {
                    if (adicionarCondicionContratacionASubCentroCentroCCiudad(ID_EMPRESA, filaCondicionContratacionFuente["DOC_TRAB"].ToString().Trim(), ID_PERFIL, filaCondicionContratacionFuente["RIESGO"].ToString(), filaCondicionContratacionFuente["OBS_CTE"].ToString().Trim(), 0, ID_CENTRO_C, null, conexion) == 0)
                    {
                        conexion.DeshacerTransaccion();
                        return false;
                    }

                    ID_CON_REG_SERVICIO_PERFIL = obtenerIdConRegServicioPerfil(0, ID_CENTRO_C, null, 0, ID_PERFIL, conexion);
                    if (ID_CON_REG_SERVICIO_PERFIL == 0)
                    {
                        conexion.DeshacerTransaccion();
                        return false;
                    }
                    else
                    {
                        foreach (DataRow filaFuente in tablaImplementosFuente.Rows)
                        {
                            try
                            {
                                ID_SERVICIO_COMPLEMENTARIO = Convert.ToDecimal(filaFuente["ID_SERVICIO_COMPLEMENTARIO"]);
                            }
                            catch
                            {
                                ID_SERVICIO_COMPLEMENTARIO = 0;
                            }
                            try
                            {
                                CANTIDAD = Convert.ToDecimal(filaFuente["CANTIDAD"]);
                            }
                            catch
                            {
                                CANTIDAD = 0;
                            }
                            try
                            {
                                VALOR = Convert.ToDecimal(filaFuente["VALOR"]);
                            }
                            catch
                            {
                                VALOR = 0;
                            }
                            try
                            {
                                ID_PRODUCTO = Convert.ToDecimal(filaFuente["ID_PRODUCTO"]);
                            }
                            catch
                            {
                                ID_PRODUCTO = 0;
                            }

                        }
                    }
                }

                conexion.AceptarTransaccion();
                return true;
            }
            catch (Exception ex)
            {
                conexion.DeshacerTransaccion();
                MensajeError = ex.Message;
                return false;
            }
            finally
            {
                conexion.Desconectar();
            }
        }

        public Boolean replicarCondicionesContratacionDesdeCentroCACentrosCConServicio(Decimal ID_EMPRESA, Decimal ID_PERFIL, Decimal ID_CENTRO_C_FUENTE, Decimal ID_SERVICIO_FUENTE, Decimal[] IDS_CENTROS_C_REPLICAR, Decimal[] IDS_SERVICIOS_REPLICAR)
        {
            DataTable tablaCondicionesContratacionFuente = ObtenerCondicionContratacionPorIdPerfilIdCentroCIdServicio(ID_PERFIL, ID_CENTRO_C_FUENTE, ID_SERVICIO_FUENTE);
            DataRow filaCondicionContratacionFuente = tablaCondicionesContratacionFuente.Rows[0];

            DataTable tablaImplementosFuente = obtenerImplementosYExamenesPorPerfilCentroC(ID_PERFIL, ID_CENTRO_C_FUENTE, ID_SERVICIO_FUENTE);

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                Decimal ID_CON_REG_SERVICIO_PERFIL = 0;
                Decimal ID_SERVICIO_COMPLEMENTARIO = 0;
                Decimal CANTIDAD = 0;
                Decimal VALOR = 0;
                Decimal ID_PRODUCTO = 0;
                for (int i = 0; i < IDS_CENTROS_C_REPLICAR.Length; i++)
                {
                    ID_CON_REG_SERVICIO_PERFIL = obtenerIdConRegServicioPerfil(0, IDS_CENTROS_C_REPLICAR[i], null, IDS_SERVICIOS_REPLICAR[i], ID_PERFIL, conexion);
                    if (ID_CON_REG_SERVICIO_PERFIL == 0)
                    {
                        conexion.DeshacerTransaccion();
                        return false;
                    }
                    else
                    {
                        foreach (DataRow filaFuente in tablaImplementosFuente.Rows)
                        {
                            try
                            {
                                ID_SERVICIO_COMPLEMENTARIO = Convert.ToDecimal(filaFuente["ID_SERVICIO_COMPLEMENTARIO"]);
                            }
                            catch
                            {
                                ID_SERVICIO_COMPLEMENTARIO = 0;
                            }
                            try
                            {
                                CANTIDAD = Convert.ToDecimal(filaFuente["CANTIDAD"]);
                            }
                            catch
                            {
                                CANTIDAD = 0;
                            }
                            try
                            {
                                VALOR = Convert.ToDecimal(filaFuente["VALOR"]);
                            }
                            catch
                            {
                                VALOR = 0;
                            }
                            try
                            {
                                ID_PRODUCTO = Convert.ToDecimal(filaFuente["ID_PRODUCTO"]);
                            }
                            catch
                            {
                                ID_PRODUCTO = 0;
                            }

                        }
                    }
                }

                conexion.AceptarTransaccion();
                return true;
            }
            catch (Exception ex)
            {
                conexion.DeshacerTransaccion();
                MensajeError = ex.Message;
                return false;
            }
            finally
            {
                conexion.Desconectar();
            }
        }

        public Boolean replicarCondicionesContratacionDesdeSubCASubsC(Decimal ID_EMPRESA, Decimal ID_PERFIL, Decimal ID_SUB_C_FUENTE, Decimal[] IDS_SUBS_C_REPLICAR)
        {
            DataTable tablaCondicionesContratacionFuente = ObtenerCondicionComercialPorIdPerfilIdSubC(ID_PERFIL, ID_SUB_C_FUENTE);
            DataRow filaCondicionContratacionFuente = tablaCondicionesContratacionFuente.Rows[0];

            DataTable tablaImplementosFuente = obtenerImplementosYExamenesPorPerfilSubC(ID_PERFIL, ID_SUB_C_FUENTE, 0);

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                Decimal ID_CON_REG_SERVICIO_PERFIL = 0;
                Decimal ID_SERVICIO_COMPLEMENTARIO = 0;
                Decimal CANTIDAD = 0;
                Decimal VALOR = 0;
                Decimal ID_PRODUCTO = 0;
                foreach (Decimal ID_SUB_C in IDS_SUBS_C_REPLICAR)
                {
                    if (adicionarCondicionContratacionASubCentroCentroCCiudad(ID_EMPRESA, filaCondicionContratacionFuente["DOC_TRAB"].ToString().Trim(), ID_PERFIL, filaCondicionContratacionFuente["RIESGO"].ToString(), filaCondicionContratacionFuente["OBS_CTE"].ToString().Trim(), ID_SUB_C, 0, null, conexion) == 0)
                    {
                        conexion.DeshacerTransaccion();
                        return false;
                    }

                    ID_CON_REG_SERVICIO_PERFIL = obtenerIdConRegServicioPerfil(ID_SUB_C, 0, null, 0, ID_PERFIL, conexion);
                    if (ID_CON_REG_SERVICIO_PERFIL == 0)
                    {
                        conexion.DeshacerTransaccion();
                        return false;
                    }
                    else
                    {
                        foreach (DataRow filaFuente in tablaImplementosFuente.Rows)
                        {
                            try
                            {
                                ID_SERVICIO_COMPLEMENTARIO = Convert.ToDecimal(filaFuente["ID_SERVICIO_COMPLEMENTARIO"]);
                            }
                            catch
                            {
                                ID_SERVICIO_COMPLEMENTARIO = 0;
                            }
                            try
                            {
                                CANTIDAD = Convert.ToDecimal(filaFuente["CANTIDAD"]);
                            }
                            catch
                            {
                                CANTIDAD = 0;
                            }
                            try
                            {
                                VALOR = Convert.ToDecimal(filaFuente["VALOR"]);
                            }
                            catch
                            {
                                VALOR = 0;
                            }
                            try
                            {
                                ID_PRODUCTO = Convert.ToDecimal(filaFuente["ID_PRODUCTO"]);
                            }
                            catch
                            {
                                ID_PRODUCTO = 0;
                            }

                        }
                    }
                }

                conexion.AceptarTransaccion();
                return true;
            }
            catch (Exception ex)
            {
                conexion.DeshacerTransaccion();
                MensajeError = ex.Message;
                return false;
            }
            finally
            {
                conexion.Desconectar();
            }
        }

        public Decimal ReplicarCondicion(Decimal registroVenPContratacion,
            String ID_CIUDAD_R,
            Decimal ID_CENTRO_C_R,
            Decimal ID_SUB_C_R,
            Decimal ID_SERVICIO_R,
            Conexion conexion)
        {
            Decimal registro = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_ven_p_contratacion_Replicar ";

            if (registroVenPContratacion != 0)
            {
                sql += registroVenPContratacion + ", ";
                informacion += "REGISTRO_VEN_P_CONTRATACION = '" + registroVenPContratacion.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO_VEN_P_CONTRATACION no puede ser nulo. \n";
                ejecutar = false;
            }

            if (String.IsNullOrEmpty(ID_CIUDAD_R) == false)
            {
                sql += "'" + ID_CIUDAD_R + "', ";
                informacion += "ID_CIUDAD = '" + ID_CIUDAD_R + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_CIUDAD = 'NULL', ";
            }

            if (ID_CENTRO_C_R != 0)
            {
                sql += ID_CENTRO_C_R + ", ";
                informacion += "ID_CENTRO_C = '" + ID_CENTRO_C_R + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_CENTRO_C = 'NULL', ";
            }

            if (ID_SUB_C_R != 0)
            {
                sql += ID_SUB_C_R + ", ";
                informacion += "ID_SUB_C = '" + ID_SUB_C_R + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_SUB_C = 'NULL', ";
            }

            if (ID_SERVICIO_R != 0)
            {
                sql += ID_SERVICIO_R + ", ";
                informacion += "ID_SERVICIO = '" + ID_SERVICIO_R + "', ";
            }
            else
            {
                sql += "NULL, ";
                informacion += "ID_SERVICIO = 'NULL', ";
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario + "'";

            if (ejecutar)
            {
                try
                {
                    registro = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.VEN_P_CONTRATACION, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    registro = 0;
                }
            }
            else
            {
                registro = 0;
            }

            return registro;
        }

        public Decimal ReplicarImplementoExamen(Decimal registroConRegElementoFuente,
            Decimal registroVenPContratacionNuevo,
            Conexion conexion)
        {
            Decimal registro = 0;
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_con_reg_elementos_trabajo_Replicar ";

            if (registroConRegElementoFuente != 0)
            {
                sql += registroConRegElementoFuente + ", ";
                informacion += "REGISTRO_CON_REG_ELEMENTO_FUENTE = '" + registroConRegElementoFuente.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO_CON_REG_ELEMENTO_FUENTE no puede ser nulo. \n";
                ejecutar = false;
            }
            if (registroVenPContratacionNuevo != 0)
            {
                sql += registroVenPContratacionNuevo + ", ";
                informacion += "REGISTRO_VEN_P_CONTRATACION_NUEVO = '" + registroVenPContratacionNuevo.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo REGISTRO_VEN_P_CONTRATACION_NUEVO no puede ser nulo. \n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE = '" + Usuario + "'";

            if (ejecutar)
            {
                try
                {
                    registro = Convert.ToDecimal(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.CON_REG_ELEMEMENTOS_TRABAJO, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
                    #endregion auditoria
                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    registro = 0;
                }
            }
            else
            {
                registro = 0;
            }

            return registro;
        }

        public Boolean ReplicarCondicionesContratacionAUbicacion(Decimal registroVenPContratacion,
            String ID_CIUDAD_R,
            Decimal ID_CENTRO_C_R,
            Decimal ID_SUB_C_R,
            Decimal ID_SERVICIO_R,
            DataTable tablaImplementosFuente,
            Conexion conexion)
        {
            Boolean correcto = true;
            MensajeError = null;

            try
            {
                Decimal registroVenPContratacionNueva = ReplicarCondicion(registroVenPContratacion, ID_CIUDAD_R, ID_CENTRO_C_R, ID_SUB_C_R, ID_SERVICIO_R, conexion);

                if (registroVenPContratacionNueva <= 0)
                {
                    correcto = false;
                }
                else
                {
                    foreach (DataRow filaImplemento in tablaImplementosFuente.Rows)
                    {
                        Decimal REGISTRO_CON_REG_ELEMENTO_TRABAJO_NUEVO = ReplicarImplementoExamen(Convert.ToDecimal(filaImplemento["REGISTRO"]), registroVenPContratacionNueva, conexion);

                        if (REGISTRO_CON_REG_ELEMENTO_TRABAJO_NUEVO <= 0)
                        {
                            correcto = false;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                correcto = false;
                MensajeError = ex.Message;
            }

            return correcto;
        }

        public Boolean ReplicarCondicionesContratacionAUbicacionMasivo(Decimal registroVenPContratacion,
            DataTable tablaUbicaciones)
        {
            Boolean correcto = true;

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                DataTable tablaImplementosFuente = obtenerImplementosYExamenesActivosPorRegistroVenPContratacion(registroVenPContratacion, conexion);

                if (MensajeError != null)
                {
                    correcto = false;
                    conexion.DeshacerTransaccion();
                }
                else
                {
                    foreach (DataRow filaTabla in tablaUbicaciones.Rows)
                    {
                        String ID_CIUDAD_R = null;
                        if ((filaTabla["ID_CIUDAD_R"].ToString() != "") && (filaTabla["ID_CIUDAD_R"].ToString() != "0"))
                        {
                            ID_CIUDAD_R = filaTabla["ID_CIUDAD_R"].ToString();
                        }

                        Decimal ID_CENTRO_C_R = Convert.ToDecimal(filaTabla["ID_CENTRO_C_R"]);
                        Decimal ID_SUB_C_R = Convert.ToDecimal(filaTabla["ID_SUB_C_R"]);
                        Decimal ID_SERVICIO_R = Convert.ToDecimal(filaTabla["ID_SERVICIO_R"]);

                        if (ReplicarCondicionesContratacionAUbicacion(registroVenPContratacion, ID_CIUDAD_R, ID_CENTRO_C_R, ID_SUB_C_R, ID_SERVICIO_R, tablaImplementosFuente, conexion) == false)
                        {
                            correcto = false;
                            conexion.DeshacerTransaccion();
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

        public Boolean replicarCondicionesContratacionDesdeSubCASubsCConServicio(Decimal ID_EMPRESA,
            Decimal ID_PERFIL,
            Decimal ID_SUB_C_FUENTE,
            Decimal ID_SERVICIO_FUENTE,
            Decimal[] IDS_SUBS_C_REPLICAR,
            Decimal[] IDS_SERVICIOS_REPLICAR)
        {
            DataTable tablaCondicionesContratacionFuente = ObtenerCondicionContratacionPorIdPerfilIdSubCIdServicio(ID_PERFIL, ID_SUB_C_FUENTE, ID_SERVICIO_FUENTE);
            DataRow filaCondicionContratacionFuente = tablaCondicionesContratacionFuente.Rows[0];

            DataTable tablaImplementosFuente = obtenerImplementosYExamenesPorPerfilSubC(ID_PERFIL, ID_SUB_C_FUENTE, ID_SERVICIO_FUENTE);

            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            try
            {
                Decimal ID_CON_REG_SERVICIO_PERFIL = 0;
                Decimal ID_SERVICIO_COMPLEMENTARIO = 0;
                Decimal CANTIDAD = 0;
                Decimal VALOR = 0;
                Decimal ID_PRODUCTO = 0;
                for (int i = 0; i < IDS_SUBS_C_REPLICAR.Length; i++)
                {
                    ID_CON_REG_SERVICIO_PERFIL = obtenerIdConRegServicioPerfil(IDS_SUBS_C_REPLICAR[i], 0, null, IDS_SERVICIOS_REPLICAR[i], ID_PERFIL, conexion);
                    if (ID_CON_REG_SERVICIO_PERFIL == 0)
                    {
                        conexion.DeshacerTransaccion();
                        return false;
                    }
                    else
                    {
                        foreach (DataRow filaFuente in tablaImplementosFuente.Rows)
                        {
                            try
                            {
                                ID_SERVICIO_COMPLEMENTARIO = Convert.ToDecimal(filaFuente["ID_SERVICIO_COMPLEMENTARIO"]);
                            }
                            catch
                            {
                                ID_SERVICIO_COMPLEMENTARIO = 0;
                            }
                            try
                            {
                                CANTIDAD = Convert.ToDecimal(filaFuente["CANTIDAD"]);
                            }
                            catch
                            {
                                CANTIDAD = 0;
                            }
                            try
                            {
                                VALOR = Convert.ToDecimal(filaFuente["VALOR"]);
                            }
                            catch
                            {
                                VALOR = 0;
                            }
                            try
                            {
                                ID_PRODUCTO = Convert.ToDecimal(filaFuente["ID_PRODUCTO"]);
                            }
                            catch
                            {
                                ID_PRODUCTO = 0;
                            }

                        }
                    }
                }

                conexion.AceptarTransaccion();
                return true;
            }
            catch (Exception ex)
            {
                conexion.DeshacerTransaccion();
                MensajeError = ex.Message;
                return false;
            }
            finally
            {
                conexion.Desconectar();
            }
        }


        public DataTable ObtenerComRequerimientoPorIdRequerimiento(Decimal ID_REQUERIMIENTO)
        {
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_contratacion_con_requerimientos_obtener_por_id ";

            if (ID_REQUERIMIENTO != 0)
            {
                sql += ID_REQUERIMIENTO;
                informacion += "ID_REQUERIMIENTO = '" + ID_REQUERIMIENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_REQUERIMIENTO no puede ser nulo\n";
                ejecutar = false;
            }

            informacion += "USU_LOG = '" + Usuario + "'";

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
                    _auditoria.Adicionar(Usuario, tabla.CON_REQUERIMIENTOS, tabla.ACCION_CONSULTAR, sql, informacion, conexion);
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
