using System;

using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Brainsbits.LDA
{
    public class Conexion
    {
        #region variables
        enum Empresas
        {
            gvaru = 1,
        }
        private SqlConnection _sqlConnection;
        private SqlTransaction _sqlTransaction;
        private SqlCommand _sqlCommand;
        private string _empresa = null;
        #endregion variables

        #region propiedades

        public string Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        #endregion propiedades

        #region constructores
        public Conexion(String empresa)
        {
            Empresa = empresa.ToString();
            Conectar();
        }
        #endregion constructores

        #region metodos
        private void Conectar()
        {
            if (_sqlConnection == null)
            {
                _sqlConnection = new SqlConnection();
                if (Empresa == Convert.ToInt32(Empresas.gvaru).ToString()) _sqlConnection.ConnectionString = ConfigurationManager.ConnectionStrings["gvaru"].ConnectionString;

                try
                {
                    _sqlConnection.Open();
                }
                catch (DataException e)
                {
                    throw new Exception("Error al intentar conexion con la base de datos: " + e.Message);
                }
            }
        }

        public void Desconectar()
        {
            try
            {
                _sqlConnection.Dispose();
                _sqlConnection.Close();
            }
            catch (DataException e)
            {

                throw new Exception("Error al intentar desconectarse de la base de datos: " + e.Message);
            }
        }

        public void IniciarTransaccion()
        {
            try
            {
                _sqlTransaction = _sqlConnection.BeginTransaction();
            }
            catch (DataException e)
            {
                throw new Exception("Error al intentar iniciar transacción: " + e.Message);
            }
        }

        public void AceptarTransaccion()
        {
            try
            {
                _sqlTransaction.Commit();
            }
            catch (DataException e)
            {
                throw new Exception("Error al aceptar la transacción: " + e.Message);
            }
        }

        public void DeshacerTransaccion()
        {
            try
            {
                _sqlTransaction.Rollback();
            }
            catch (DataException e)
            {
                throw new Exception("Error al aceptar la transacción: " + e.Message);
            }
        }

        public Int32 ExecuteNonQuery(String sql)
        {
            _sqlCommand = new SqlCommand();
            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = sql;
            _sqlCommand.CommandTimeout = 0;
            _sqlCommand.Transaction = _sqlTransaction;

            Int32 cantidadRegistrosAfectados = 0;

            try
            {
                cantidadRegistrosAfectados = _sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteNonQuery): " + e.Message.ToString());
            }
            return cantidadRegistrosAfectados;
        }

        public DataSet ExecuteReader(String sql)
        {
            SqlDataAdapter _sqlDataAdapter = new SqlDataAdapter(sql, _sqlConnection);

            DataSet _dataSet = new DataSet();
            try
            {
                _sqlDataAdapter.SelectCommand.CommandTimeout = 0;
                _sqlDataAdapter.Fill(_dataSet);
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteReader): " + e.Message.ToString());
            }
            return _dataSet;
        }

        public DataSet ExecuteReaderConTransaccion(String sql)
        {
            _sqlCommand = new SqlCommand();
            _sqlCommand.CommandTimeout = 0;
            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = sql;
            _sqlCommand.Transaction = _sqlTransaction;


            SqlDataAdapter _sqlDataAdapter = new SqlDataAdapter(_sqlCommand);

            DataSet _dataSet = new DataSet();
            try
            {
                _sqlDataAdapter.Fill(_dataSet);
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteReader): " + e.Message.ToString());
            }
            return _dataSet;
        }

        public String ExecuteScalar(String sql)
        {
            String valor = null;
            _sqlCommand = new SqlCommand();
            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = sql;
            _sqlCommand.CommandTimeout = 0;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = _sqlCommand.ExecuteScalar().ToString();
            }
            catch (NullReferenceException)
            {
                valor = null;
            }

            catch (SqlException e)
            {
                switch (e.Number)
                {
                    case 50000:
                        throw new Exception(e.Message);
                    default:
                        throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
                }
            }

            return valor;
        }

        public String ExecuteEscalarParaAdicionarPrueba(String NOM_PRUEBA,
            Decimal ID_CATEGORIA,
            String OBS_PRUEBA,
            Byte[] ARCHIVO,
            String ARCHIVO_EXTENSION,
            Decimal ARCHIVO_TAMANO,
            String ARCHIVO_TYPE,
            String USU_CRE)
        {
            String valor = null;
            String query = @"usp_sel_pruebas_adicionar_con_manual @NOM_PRUEBA, @ID_CATEGORIA, @OBS_PRUEBA, @ARCHIVO, @ARCHIVO_EXTENSION, @ARCHIVO_TAMANO, @ARCHIVO_TYPE, @USU_CRE";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@NOM_PRUEBA", NOM_PRUEBA);
            _sqlCommand.Parameters.AddWithValue("@ID_CATEGORIA", ID_CATEGORIA);
            _sqlCommand.Parameters.AddWithValue("@OBS_PRUEBA", OBS_PRUEBA);

            if (ARCHIVO == null)
            {
                SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
                archivoParam.Value = DBNull.Value;
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", DBNull.Value);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", DBNull.Value);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", DBNull.Value);
            }
            else
            {
                SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
                archivoParam.Value = ARCHIVO;

                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", ARCHIVO_EXTENSION);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", ARCHIVO_TAMANO);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", ARCHIVO_TYPE);
            }

            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = _sqlCommand.ExecuteScalar().ToString();
            }
            catch (NullReferenceException)
            {
                valor = null;
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }

        public Int32 ExecuteNonQueryParaActualizarPruebaConManual(Decimal ID_PRUEBA,
            String NOM_PRUEBA,
            String OBS_PRUEBA,
            Byte[] ARCHIVO,
            String ARCHIVO_EXTENSION,
            Decimal ARCHIVO_TAMANO,
            String ARCHIVO_TYPE,
            String USU_MOD)
        {
            string query = @"usp_sel_pruebas_actualizar_con_manual @ID_PRUEBA, @NOM_PRUEBA, @OBS_PRUEBA, @ARCHIVO, @ARCHIVO_EXTENSION, @ARCHIVO_TAMANO, @ARCHIVO_TYPE, @USU_MOD";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_PRUEBA", ID_PRUEBA);
            _sqlCommand.Parameters.AddWithValue("@NOM_PRUEBA", NOM_PRUEBA);
            _sqlCommand.Parameters.AddWithValue("@OBS_PRUEBA", OBS_PRUEBA);

            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", ARCHIVO_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", ARCHIVO_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", ARCHIVO_TYPE);
            _sqlCommand.Parameters.AddWithValue("@USU_MOD", USU_MOD);

            SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
            archivoParam.Value = ARCHIVO;

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            Int32 cantidadRegistrosAfectados = 0;

            try
            {
                cantidadRegistrosAfectados = _sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteNonQuery): " + e.Message.ToString());
            }

            return cantidadRegistrosAfectados;
        }

        public Int32 ExecuteNonQueryParaAdicionarPruebaConImagen(Decimal ID_SOLICITUD,
            Decimal ID_PRUEBA,
            Decimal ID_CATEGORIA,
            DateTime FECHA_R,
            String OBS_PRUEBA,
            String RESULTADOS,
            String USU_CRE,
            Byte[] ARCHIVO_PRUEBA,
            String ARCHIVO_EXTENSION,
            Decimal ARCHIVO_TAMANO,
            String ARCHIVO_TYPE)
        {
            string query = @"usp_sel_reg_aplicacion_pruebas_adicionar_con_imagen @ID_SOLICITUD, @ID_PRUEBA, @ID_CATEGORIA, @FECHA_R, @OBS_PRUEBA, @RESULTADOS, @USU_CRE, @ARCHIVO_PRUEBA, @ARCHIVO_TAMANO, @ARCHIVO_EXTENSION, @ARCHIVO_TYPE ";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_SOLICITUD", ID_SOLICITUD);
            _sqlCommand.Parameters.AddWithValue("@ID_PRUEBA", ID_PRUEBA);
            _sqlCommand.Parameters.AddWithValue("@ID_CATEGORIA", ID_CATEGORIA);
            _sqlCommand.Parameters.AddWithValue("@FECHA_R", FECHA_R);
            _sqlCommand.Parameters.AddWithValue("@OBS_PRUEBA", OBS_PRUEBA);
            _sqlCommand.Parameters.AddWithValue("@RESULTADOS", RESULTADOS);
            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);

            SqlParameter imageParam = _sqlCommand.Parameters.Add("@ARCHIVO_PRUEBA", System.Data.SqlDbType.Image);
            imageParam.Value = ARCHIVO_PRUEBA;
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", ARCHIVO_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", ARCHIVO_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", ARCHIVO_TYPE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;

            _sqlCommand.Transaction = _sqlTransaction;

            Int32 cantidadRegistrosAfectados = 0;

            try
            {
                cantidadRegistrosAfectados = _sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteNonQuery): " + e.Message.ToString());
            }

            return cantidadRegistrosAfectados;
        }

        public Int32 ExecuteNonQueryParaAdicionarSelPruebaConManual(String NOM_PRUEBA, int ID_CATEGORIA, String OBS_PRUEBA, String USU_CRE, Byte[] ARCHIVO_PRUEBA)
        {
            string query = @"usp_sel_pruebas_adicionar_con_manual @NOM_PRUEBA, @ID_CATEGORIA, @OBS_PRUEBA, @ARCHIVO_PRUEBA, @ARCHIVO_TAMANO, @ARCHIVO_EXTENSION, @ARCHIVO_TYPE, @USU_CRE";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@NOM_PRUEBA", NOM_PRUEBA);
            _sqlCommand.Parameters.AddWithValue("@ID_CATEGORIA", ID_CATEGORIA);
            _sqlCommand.Parameters.AddWithValue("@OBS_PRUEBA", OBS_PRUEBA);
            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);

            SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO_PRUEBA", System.Data.SqlDbType.VarBinary);
            archivoParam.Value = ARCHIVO_PRUEBA;

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            Int32 cantidadRegistrosAfectados = 0;

            try
            {
                cantidadRegistrosAfectados = _sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteNonQuery): " + e.Message.ToString());
            }

            return cantidadRegistrosAfectados;
        }

        public String ExecuteEscalarParaAdicionarDescargoConArchivo(Decimal ID_EMPLEADO, DateTime FECHA_R, String OBS_REG, DateTime FECHA_SOLICITUD, DateTime FECHA_ACTA, DateTime FECHA_CIERRE, String MOTIVO, Byte[] ARCHIVO_ACTA, Int32 ARCHIVO_TAMANO, String ARCHIVO_EXTENSION, String ARCHIVO_TYPE, String USU_CRE)
        {
            String valor = null;
            String query = @"usp_reg_acta_desc_adicionar_con_archivo @ID_EMPLEADO, @FECHA_R, @OBS_REG, @FECHA_SOLICITUD, @FECHA_ACTA, @FECHA_CIERRE, @MOTIVO, @ARCHIVO_ACTA, @ARCHIVO_TAMANO, @ARCHIVO_EXTENSION, @ARCHIVO_TYPE, @USU_CRE";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_EMPLEADO", ID_EMPLEADO);
            _sqlCommand.Parameters.AddWithValue("@FECHA_R", FECHA_R);

            if (String.IsNullOrEmpty(OBS_REG) == false)
            {
                _sqlCommand.Parameters.AddWithValue("@OBS_REG", OBS_REG);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@OBS_REG", DBNull.Value);
            }

            _sqlCommand.Parameters.AddWithValue("@FECHA_SOLICITUD", FECHA_SOLICITUD);
            _sqlCommand.Parameters.AddWithValue("@FECHA_ACTA", FECHA_ACTA);
            if (FECHA_CIERRE == new DateTime())
            {
                _sqlCommand.Parameters.AddWithValue("@FECHA_CIERRE", DBNull.Value);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@FECHA_CIERRE", FECHA_CIERRE);
            }
            _sqlCommand.Parameters.AddWithValue("@MOTIVO", MOTIVO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", ARCHIVO_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", ARCHIVO_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", ARCHIVO_TYPE);
            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);

            SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO_ACTA", System.Data.SqlDbType.VarBinary);
            archivoParam.Value = ARCHIVO_ACTA;

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = _sqlCommand.ExecuteScalar().ToString();
            }
            catch (NullReferenceException)
            {
                valor = null;
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }

        public String ExecuteEscalarParaAdicionarTutela(Decimal ID_SOLICITUD,
            DateTime FECHA_R,
            DateTime FECHA_NOTIFICACION,
            DateTime FECHA_PLAZO_CONTESTA,
            DateTime FECHA_CONTESTACION,
            String PETICION,
            Byte[] ARCHIVO_PETICION,
            String ARCHIVO_PETICION_EXTENSION,
            Int32 ARCHIVO_PETICION_TAMANO,
            String ARCHIVO_PETICION_TYPE,
            String FALLO,
            String RESULTADO_FALLO,
            Byte[] ARCHIVO_FALLO,
            String ARCHIVO_FALLO_EXTENSION,
            Int32 ARCHIVO_FALLO_TAMANO,
            String ARCHIVO_FALLO_TYPE,
            Decimal RESPONSABLE,
            Decimal ID_EMPRESA,
            Decimal REGISTRO_CONTRATO,
            String CONSIDERACIONES,
            String USU_CRE)
        {
            String valor = null;
            String query = @"usp_con_reg_tutelas_adicionar @ID_SOLICITUD, @FECHA_R, @FECHA_NOTIFICACION, @FECHA_PLAZO_CONTESTA, @FECHA_CONTESTACION, @PETICION, @ARCHIVO_PETICION, @ARCHIVO_PETICION_EXTENSION, @ARCHIVO_PETICION_TAMANO, @ARCHIVO_PETICION_TYPE, @FALLO, @RESULTADO_FALLO, @ARCHIVO_FALLO, @ARCHIVO_FALLO_EXTENSION, @ARCHIVO_FALLO_TAMANO, @ARCHIVO_FALLO_TYPE, @RESPONSABLE, @ID_EMPRESA, @REGISTRO_CONTRATO, @CONSIDERACIONES, @USU_CRE";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_SOLICITUD", ID_SOLICITUD);

            SqlParameter archivoParam = _sqlCommand.Parameters.Add("@FECHA_R", System.Data.SqlDbType.DateTime);
            archivoParam.Value = FECHA_R;

            SqlParameter archivoParam1 = _sqlCommand.Parameters.Add("@FECHA_NOTIFICACION", System.Data.SqlDbType.DateTime);
            archivoParam1.Value = FECHA_NOTIFICACION;

            SqlParameter archivoParam2 = _sqlCommand.Parameters.Add("@FECHA_PLAZO_CONTESTA", System.Data.SqlDbType.DateTime);
            archivoParam2.Value = FECHA_PLAZO_CONTESTA;

            if (FECHA_CONTESTACION == new DateTime())
            {
                _sqlCommand.Parameters.AddWithValue("@FECHA_CONTESTACION", DBNull.Value);
            }
            else
            {
                SqlParameter archivoParam3 = _sqlCommand.Parameters.Add("@FECHA_CONTESTACION", System.Data.SqlDbType.DateTime);
                archivoParam3.Value = FECHA_CONTESTACION;
            }

            if (String.IsNullOrEmpty(PETICION) == true)
            {
                _sqlCommand.Parameters.AddWithValue("@PETICION", DBNull.Value);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@PETICION", PETICION);
            }

            if (ARCHIVO_PETICION == null)
            {
                SqlParameter archivoParam4 = _sqlCommand.Parameters.Add("@ARCHIVO_PETICION", System.Data.SqlDbType.VarBinary);
                archivoParam4.Value = DBNull.Value;
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_PETICION_EXTENSION", DBNull.Value);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_PETICION_TAMANO", DBNull.Value);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_PETICION_TYPE", DBNull.Value);
            }
            else
            {
                SqlParameter archivoParam4 = _sqlCommand.Parameters.Add("@ARCHIVO_PETICION", System.Data.SqlDbType.VarBinary);
                archivoParam4.Value = ARCHIVO_PETICION;

                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_PETICION_EXTENSION", ARCHIVO_PETICION_EXTENSION);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_PETICION_TAMANO", ARCHIVO_PETICION_TAMANO);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_PETICION_TYPE", ARCHIVO_PETICION_TYPE);
            }

            if (String.IsNullOrEmpty(FALLO) == true)
            {
                _sqlCommand.Parameters.AddWithValue("@FALLO", DBNull.Value);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@FALLO", FALLO);
            }

            if (String.IsNullOrEmpty(RESULTADO_FALLO) == true)
            {
                _sqlCommand.Parameters.AddWithValue("@RESULTADO_FALLO", DBNull.Value);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@RESULTADO_FALLO", RESULTADO_FALLO);
            }

            if (ARCHIVO_FALLO == null)
            {
                SqlParameter archivoParam5 = _sqlCommand.Parameters.Add("@ARCHIVO_FALLO", System.Data.SqlDbType.VarBinary);
                archivoParam5.Value = DBNull.Value;
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_FALLO_EXTENSION", DBNull.Value);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_FALLO_TAMANO", DBNull.Value);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_FALLO_TYPE", DBNull.Value);
            }
            else
            {
                SqlParameter archivoParam5 = _sqlCommand.Parameters.Add("@ARCHIVO_FALLO", System.Data.SqlDbType.VarBinary);
                archivoParam5.Value = ARCHIVO_FALLO;

                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_FALLO_EXTENSION", ARCHIVO_FALLO_EXTENSION);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_FALLO_TAMANO", ARCHIVO_FALLO_TAMANO);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_FALLO_TYPE", ARCHIVO_FALLO_TYPE);
            }

            _sqlCommand.Parameters.AddWithValue("@RESPONSABLE", RESPONSABLE);
            _sqlCommand.Parameters.AddWithValue("@ID_EMPRESA", ID_EMPRESA);
            _sqlCommand.Parameters.AddWithValue("@REGISTRO_CONTRATO", REGISTRO_CONTRATO);

            if (String.IsNullOrEmpty(CONSIDERACIONES) == true)
            {
                _sqlCommand.Parameters.AddWithValue("@CONSIDERACIONES", DBNull.Value);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@CONSIDERACIONES", CONSIDERACIONES);
            }

            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = _sqlCommand.ExecuteScalar().ToString();
            }
            catch (NullReferenceException)
            {
                valor = null;
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }

        public String ExecuteEscalarParaAdicionarAdjuntoActo(Decimal ID_TUTELA,
            Decimal ID_DEMANDA,
            Decimal ID_DERECHO,
            DateTime FECHA_R,
            DateTime FECHA_ADJUNTO,
            String TITULO,
            String DESCRIPCION,
            Byte[] ARCHIVO_ADJUNTO,
            String ARCHIVO_ADJUNTO_EXTENSION,
            Decimal ARCHIVO_ADJUNTO_TAMANO,
            String ARCHIVO_ADJUNTO_TYPE,
            String USU_CRE)
        {
            String valor = null;
            String query = @"usp_reg_adjuntos_actos_juridicos_adicionar @ID_TUTELA, @ID_DEMANDA, @ID_DERECHO, @FECHA_R, @FECHA_ADJUNTO, @TITULO, @DESCRIPCION, @ARCHIVO_ADJUNTO, @ARCHIVO_ADJUNTO_EXTENSION, @ARCHIVO_ADJUNTO_TAMANO, @ARCHIVO_ADJUNTO_TYPE, @USU_CRE";

            _sqlCommand = new SqlCommand();

            if (ID_TUTELA == 0)
            {
                _sqlCommand.Parameters.AddWithValue("@ID_TUTELA", DBNull.Value);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@ID_TUTELA", ID_TUTELA);
            }

            if (ID_DEMANDA == 0)
            {
                _sqlCommand.Parameters.AddWithValue("@ID_DEMANDA", DBNull.Value);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@ID_DEMANDA", ID_DEMANDA);
            }

            if (ID_DERECHO == 0)
            {
                _sqlCommand.Parameters.AddWithValue("@ID_DERECHO", DBNull.Value);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@ID_DERECHO", ID_DERECHO);
            }

            _sqlCommand.Parameters.AddWithValue("@FECHA_R", FECHA_R);
            _sqlCommand.Parameters.AddWithValue("@FECHA_ADJUNTO", FECHA_ADJUNTO);
            _sqlCommand.Parameters.AddWithValue("@TITULO", TITULO);
            _sqlCommand.Parameters.AddWithValue("@DESCRIPCION", DESCRIPCION);

            if (ARCHIVO_ADJUNTO == null)
            {
                SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO_ADJUNTO", System.Data.SqlDbType.VarBinary);
                archivoParam.Value = DBNull.Value;
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_ADJUNTO_EXTENSION", DBNull.Value);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_ADJUNTO_TAMANO", DBNull.Value);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_ADJUNTO_TYPE", DBNull.Value);
            }
            else
            {
                SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO_ADJUNTO", System.Data.SqlDbType.VarBinary);
                archivoParam.Value = ARCHIVO_ADJUNTO;

                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_ADJUNTO_EXTENSION", ARCHIVO_ADJUNTO_EXTENSION);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_ADJUNTO_TAMANO", ARCHIVO_ADJUNTO_TAMANO);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_ADJUNTO_TYPE", ARCHIVO_ADJUNTO_TYPE);
            }

            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = _sqlCommand.ExecuteScalar().ToString();
            }
            catch (NullReferenceException)
            {
                valor = null;
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }





        public String ExecuteEscalarParaAdicionarAdjuntoInvestigacionesAdministrativas(Decimal ID_INVESTIGACION,
            DateTime FECHA_R,
            DateTime FECHA_ADJUNTO,
            String TITULO,
            String DESCRIPCION,
            Byte[] ARCHIVO_ADJUNTO,
            String ARCHIVO_ADJUNTO_EXTENSION,
            Decimal ARCHIVO_ADJUNTO_TAMANO,
            String ARCHIVO_ADJUNTO_TYPE,
            String USU_CRE)
        {
            String valor = null;
            String query = @"usp_reg_adjuntos_investigaciones_administrativas_adicionar @ID_INVESTIGACION, @FECHA_R, @FECHA_ADJUNTO, @TITULO, @DESCRIPCION, @ARCHIVO_ADJUNTO, @ARCHIVO_ADJUNTO_EXTENSION, @ARCHIVO_ADJUNTO_TAMANO, @ARCHIVO_ADJUNTO_TYPE, @USU_CRE";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_INVESTIGACION", ID_INVESTIGACION);
            _sqlCommand.Parameters.AddWithValue("@FECHA_R", FECHA_R);
            _sqlCommand.Parameters.AddWithValue("@FECHA_ADJUNTO", FECHA_ADJUNTO);
            _sqlCommand.Parameters.AddWithValue("@TITULO", TITULO);
            _sqlCommand.Parameters.AddWithValue("@DESCRIPCION", DESCRIPCION);

            if (ARCHIVO_ADJUNTO == null)
            {
                SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO_ADJUNTO", System.Data.SqlDbType.VarBinary);
                archivoParam.Value = DBNull.Value;
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_ADJUNTO_EXTENSION", DBNull.Value);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_ADJUNTO_TAMANO", DBNull.Value);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_ADJUNTO_TYPE", DBNull.Value);
            }
            else
            {
                SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO_ADJUNTO", System.Data.SqlDbType.VarBinary);
                archivoParam.Value = ARCHIVO_ADJUNTO;

                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_ADJUNTO_EXTENSION", ARCHIVO_ADJUNTO_EXTENSION);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_ADJUNTO_TAMANO", ARCHIVO_ADJUNTO_TAMANO);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_ADJUNTO_TYPE", ARCHIVO_ADJUNTO_TYPE);
            }

            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = _sqlCommand.ExecuteScalar().ToString();
            }
            catch (NullReferenceException)
            {
                valor = null;
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }

        public String ExecuteEscalarParaAdicionarAdjuntoRequerimientoMinisterio(Decimal ID_REQUERIMIENTO,
            DateTime FECHA_R,
            DateTime FECHA_ADJUNTO,
            String TITULO,
            String DESCRIPCION,
            Byte[] ARCHIVO_ADJUNTO,
            String ARCHIVO_ADJUNTO_EXTENSION,
            Decimal ARCHIVO_ADJUNTO_TAMANO,
            String ARCHIVO_ADJUNTO_TYPE,
            String USU_CRE)
        {
            String valor = null;
            String query = @"usp_reg_adjuntos_requerimientos_ministerio_adicionar @ID_REQUERIMIENTO, @FECHA_R, @FECHA_ADJUNTO, @TITULO, @DESCRIPCION, @ARCHIVO_ADJUNTO, @ARCHIVO_ADJUNTO_EXTENSION, @ARCHIVO_ADJUNTO_TAMANO, @ARCHIVO_ADJUNTO_TYPE, @USU_CRE";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_REQUERIMIENTO", ID_REQUERIMIENTO);
            _sqlCommand.Parameters.AddWithValue("@FECHA_R", FECHA_R);
            _sqlCommand.Parameters.AddWithValue("@FECHA_ADJUNTO", FECHA_ADJUNTO);
            _sqlCommand.Parameters.AddWithValue("@TITULO", TITULO);
            _sqlCommand.Parameters.AddWithValue("@DESCRIPCION", DESCRIPCION);

            if (ARCHIVO_ADJUNTO == null)
            {
                SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO_ADJUNTO", System.Data.SqlDbType.VarBinary);
                archivoParam.Value = DBNull.Value;
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_ADJUNTO_EXTENSION", DBNull.Value);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_ADJUNTO_TAMANO", DBNull.Value);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_ADJUNTO_TYPE", DBNull.Value);
            }
            else
            {
                SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO_ADJUNTO", System.Data.SqlDbType.VarBinary);
                archivoParam.Value = ARCHIVO_ADJUNTO;

                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_ADJUNTO_EXTENSION", ARCHIVO_ADJUNTO_EXTENSION);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_ADJUNTO_TAMANO", ARCHIVO_ADJUNTO_TAMANO);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_ADJUNTO_TYPE", ARCHIVO_ADJUNTO_TYPE);
            }

            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = _sqlCommand.ExecuteScalar().ToString();
            }
            catch (NullReferenceException)
            {
                valor = null;
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }

        public String ExecuteEscalarParaAdicionarDemanda(Decimal REGISTRO_CONTRATO,
            Decimal ID_EMPRESA,
            String RADICADO,
            Decimal ID_SOLICITUD,
            DateTime FECHA_R,
            DateTime FECHA_NOTIFICACION,
            DateTime FECHA_PLAZO_CONTESTA,
            DateTime FECHA_CONTESTACION,
            String NUM_JUZGADO,
            String ESPECIALIDAD_JUZGADO,
            String CAT_JUZGADO,
            String ID_CIUDAD_JUZGADO,
            Decimal ID_ABOGADO,
            String PAZYPALVO,
            String DEMANDANTE,
            String DEMANDADO,
            String MAS_DEMANDANTES,
            Decimal PRETENSIONES,
            String TIPO_PROCESO,
            String CONSIDERACIONES,
            String USU_CRE,
            String MOTIVO_DEMANDA)
        {
            String valor = null;
            String query = @"usp_con_reg_demandas_adicionar @REGISTRO_CONTRATO, @ID_EMPRESA, @RADICADO, @ID_SOLICITUD, @FECHA_R, @FECHA_NOTIFICACION, @FECHA_PLAZO_CONTESTA, @FECHA_CONTESTACION, @NUM_JUZGADO, @ESPECIALIDAD_JUZGADO, @CAT_JUZGADO, @ID_CIUDAD_JUZGADO, @ID_ABOGADO, @PAZYSALVO, @DEMANDANTE, @DEMANDADO, @MAS_DEMANDANTES, @PRETENSIONES, @TIPO_PROCESO, @CONSIDERACIONES, @USU_CRE, @MOTIVO_DEMANDA";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@REGISTRO_CONTRATO", REGISTRO_CONTRATO);
            _sqlCommand.Parameters.AddWithValue("@ID_EMPRESA", ID_EMPRESA);
            _sqlCommand.Parameters.AddWithValue("@RADICADO", RADICADO);
            _sqlCommand.Parameters.AddWithValue("@ID_SOLICITUD", ID_SOLICITUD);
            _sqlCommand.Parameters.AddWithValue("@FECHA_R", FECHA_R);
            _sqlCommand.Parameters.AddWithValue("@FECHA_NOTIFICACION", FECHA_NOTIFICACION);
            _sqlCommand.Parameters.AddWithValue("@FECHA_PLAZO_CONTESTA", FECHA_PLAZO_CONTESTA);
            if (FECHA_CONTESTACION == new DateTime())
            {
                _sqlCommand.Parameters.AddWithValue("@FECHA_CONTESTACION", DBNull.Value);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@FECHA_CONTESTACION", FECHA_CONTESTACION);
            }

            _sqlCommand.Parameters.AddWithValue("@NUM_JUZGADO", NUM_JUZGADO);
            _sqlCommand.Parameters.AddWithValue("@ESPECIALIDAD_JUZGADO", ESPECIALIDAD_JUZGADO);
            _sqlCommand.Parameters.AddWithValue("@CAT_JUZGADO", CAT_JUZGADO);
            _sqlCommand.Parameters.AddWithValue("@ID_CIUDAD_JUZGADO", ID_CIUDAD_JUZGADO);
            _sqlCommand.Parameters.AddWithValue("@ID_ABOGADO", ID_ABOGADO);
            _sqlCommand.Parameters.AddWithValue("@PAZYSALVO", PAZYPALVO);
            _sqlCommand.Parameters.AddWithValue("@DEMANDANTE", DEMANDANTE);
            _sqlCommand.Parameters.AddWithValue("@DEMANDADO", DEMANDADO);

            if (MAS_DEMANDANTES == null)
            {
                _sqlCommand.Parameters.AddWithValue("@MAS_DEMANDANTES", DBNull.Value);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@MAS_DEMANDANTES", MAS_DEMANDANTES);
            }

            _sqlCommand.Parameters.AddWithValue("@PRETENSIONES", PRETENSIONES);
            _sqlCommand.Parameters.AddWithValue("@TIPO_PROCESO", TIPO_PROCESO);
            if (String.IsNullOrEmpty(CONSIDERACIONES) == false)
            {
                _sqlCommand.Parameters.AddWithValue("@CONSIDERACIONES", CONSIDERACIONES);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@CONSIDERACIONES", DBNull.Value);
            }

            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);
            _sqlCommand.Parameters.AddWithValue("@MOTIVO_DEMANDA", MOTIVO_DEMANDA);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = _sqlCommand.ExecuteScalar().ToString();
            }
            catch (NullReferenceException)
            {
                valor = null;
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }

        public String ExecuteEscalarParaAdicionarCondicionNegociacionAbogado(Decimal ID_ACTO_JURIDICO,
            DateTime FECHA_R,
            Byte[] ARCHIVO_CONDICION,
            String ARCHIVO_CONDICION_EXTENSION,
            Decimal ARCHIVO_CONDICION_TAMANO,
            String ARCHIVO_CONDICION_TYPE,
            Decimal VALOR,
            String USU_CRE)
        {
            String valor = null;
            String query = @"usp_reg_condiciones_abogados_adicionar @ID_ACTO_JURIDICO, @FECHA_R, @ARCHIVO_CONDICION, @ARCHIVO_CONDICION_EXTENSION, @ARCHIVO_CONDICION_TAMANO, @ARCHIVO_CONDICION_TYPE, @VALOR, @USU_CRE";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_ACTO_JURIDICO", ID_ACTO_JURIDICO);
            _sqlCommand.Parameters.AddWithValue("@FECHA_R", FECHA_R);

            SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO_CONDICION", System.Data.SqlDbType.VarBinary);
            archivoParam.Value = ARCHIVO_CONDICION;

            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_CONDICION_EXTENSION", ARCHIVO_CONDICION_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_CONDICION_TAMANO", ARCHIVO_CONDICION_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_CONDICION_TYPE", ARCHIVO_CONDICION_TYPE);
            _sqlCommand.Parameters.AddWithValue("@VALOR", VALOR);

            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = _sqlCommand.ExecuteScalar().ToString();
            }
            catch (NullReferenceException)
            {
                valor = null;
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }


        public String ExecuteEscalarParaAdicionarProcesoDisciplianario(Decimal REGISTRO_DESCARGO,
            DateTime FECHA_R,
            DateTime FECHA_PROCESO,
            String TIPO_PROCESO,
            String MOTIVO,
            String DESCRIPCION,
            Decimal DIAS_SANCION,
            Byte[] ARCHIVO_REGISTRO,
            String ARCHIVO_REGISTRO_EXTENSION,
            Decimal ARCHIVO_REGISTRO_TAMANO,
            String ARCHIVO_REGISTRO_TYPE,
            String USU_CRE)
        {
            String valor = null;
            String query = @"usp_con_reg_proceso_disciplinario_adicionar @REGISTRO_DESCARGO, @FECHA_R, @FECHA_PROCESO, @TIPO_PROCESO, @MOTIVO, @DESCRIPCION, @DIAS_SANCION, @ARCHIVO_REGISTRO, @ARCHIVO_REGISTRO_EXTENSION, @ARCHIVO_REGISTRO_TAMANO, @ARCHIVO_REGISTRO_TYPE, @USU_CRE";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@REGISTRO_DESCARGO", REGISTRO_DESCARGO);
            _sqlCommand.Parameters.AddWithValue("@FECHA_R", FECHA_R);
            _sqlCommand.Parameters.AddWithValue("@FECHA_PROCESO", FECHA_PROCESO);
            _sqlCommand.Parameters.AddWithValue("@TIPO_PROCESO", TIPO_PROCESO);
            _sqlCommand.Parameters.AddWithValue("@MOTIVO", MOTIVO);
            _sqlCommand.Parameters.AddWithValue("@DESCRIPCION", DESCRIPCION);

            if (DIAS_SANCION == 0)
            {
                _sqlCommand.Parameters.AddWithValue("@DIAS_SANCION", DBNull.Value);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@DIAS_SANCION", DIAS_SANCION);
            }

            SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO_REGISTRO", System.Data.SqlDbType.VarBinary);
            archivoParam.Value = ARCHIVO_REGISTRO;

            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_REGISTRO_EXTENSION", ARCHIVO_REGISTRO_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_REGISTRO_TAMANO", ARCHIVO_REGISTRO_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_REGISTRO_TYPE", ARCHIVO_REGISTRO_TYPE);

            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = _sqlCommand.ExecuteScalar().ToString();
            }
            catch (NullReferenceException)
            {
                valor = null;
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }

        public String ExecuteEscalarParaAdicionarClausula(Decimal ID_PERFIL,
            String NOMBRE,
            String ENCABEZADO,
            String DESCRIPCION,
            String USU_CRE)
        {
            String valor = null;
            String query = @"usp_con_reg_clausulas_adicionar @ID_PERFIL, @NOMBRE, @ENCABEZADO, @DESCRIPCION, @USU_CRE";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_PERFIL", ID_PERFIL);
            _sqlCommand.Parameters.AddWithValue("@NOMBRE", NOMBRE);
            _sqlCommand.Parameters.AddWithValue("@ENCABEZADO", ENCABEZADO);
            _sqlCommand.Parameters.AddWithValue("@DESCRIPCION", DESCRIPCION);

            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = _sqlCommand.ExecuteScalar().ToString();
            }
            catch (NullReferenceException)
            {
                valor = null;
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }


        public String ExecuteEscalarParaAdicionarArchivoFallecimiento(Decimal ID_CONTROL_F,
            DateTime FECHA_R,
            String TIPO_DOCUMENTO,
            Byte[] ARCHIVO,
            String ARCHIVO_EXTENSION,
            Decimal ARCHIVO_TAMANO,
            String ARCHIVO_TYPE,
            String USU_CRE)
        {
            String valor = null;
            String query = @"usp_reg_archivos_control_fallecidos_adicionar @ID_CONTROL_F, @FECHA_R, @TIPO_DOCUMENTO, @ARCHIVO, @ARCHIVO_EXTENSION, @ARCHIVO_TAMANO, @ARCHIVO_TYPE, @USU_CRE";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_CONTROL_F", ID_CONTROL_F);
            _sqlCommand.Parameters.AddWithValue("@FECHA_R", FECHA_R);
            _sqlCommand.Parameters.AddWithValue("@TIPO_DOCUMENTO", TIPO_DOCUMENTO);

            SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
            archivoParam.Value = ARCHIVO;

            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", ARCHIVO_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", ARCHIVO_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", ARCHIVO_TYPE);

            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = _sqlCommand.ExecuteScalar().ToString();
            }
            catch (NullReferenceException)
            {
                valor = null;
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }

        public Int32 ExecuteNonQueryParaAdicionarArchivoNotificacionProcesoDisciplianario(Decimal ID_PROCESO,
            Byte[] ARCHIVO_NOTIFICACION,
            String ARCHIVO_NOTIFICACION_EXTENSION,
            Decimal ARCHIVO_NOTIFICACION_TAMANO,
            String ARCHIVO_NOTIFICACION_TYPE,
            String USU_MOD)
        {
            String query = @"usp_con_reg_proceso_disciplinario_actualizar_notificacion @ID_PROCESO, @ARCHIVO_NOTIFICACION, @ARCHIVO_NOTIFICACION_EXTENSION, @ARCHIVO_NOTIFICACION_TAMANO, @ARCHIVO_NOTIFICACION_TYPE, @USU_MOD";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_PROCESO", ID_PROCESO);

            SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO_NOTIFICACION", System.Data.SqlDbType.VarBinary);
            archivoParam.Value = ARCHIVO_NOTIFICACION;

            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_NOTIFICACION_EXTENSION", ARCHIVO_NOTIFICACION_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_NOTIFICACION_TAMANO", ARCHIVO_NOTIFICACION_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_NOTIFICACION_TYPE", ARCHIVO_NOTIFICACION_TYPE);

            _sqlCommand.Parameters.AddWithValue("@USU_MOD", USU_MOD);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            Int32 cantidadRegistrosAfectados = 0;

            try
            {
                cantidadRegistrosAfectados = _sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteNonQuery): " + e.Message.ToString());
            }

            return cantidadRegistrosAfectados;
        }

        public Int32 ExecuteNonQueryParaActualizarDescargoConArchivo(Decimal REGISTRO, Decimal ID_EMPLEADO, DateTime FECHA_R, String OBS_REG, DateTime FECHA_SOLICITUD, DateTime FECHA_ACTA, DateTime FECHA_CIERRE, String MOTIVO, Byte[] ARCHIVO_ACTA, Int32 ARCHIVO_TAMANO, String ARCHIVO_EXTENSION, String ARCHIVO_TYPE, String USU_MOD)
        {
            string query = @"usp_reg_acta_desc_actualizar_con_archivo @REGISTRO, @ID_EMPLEADO, @FECHA_R, @OBS_REG, @FECHA_SOLICITUD, @FECHA_ACTA, @FECHA_CIERRE, @MOTIVO, @ARCHIVO_ACTA, @ARCHIVO_TAMANO, @ARCHIVO_EXTENSION, @ARCHIVO_TYPE, @USU_MOD";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@REGISTRO", REGISTRO);
            _sqlCommand.Parameters.AddWithValue("@ID_EMPLEADO", ID_EMPLEADO);
            _sqlCommand.Parameters.AddWithValue("@FECHA_R", FECHA_R);

            if (String.IsNullOrEmpty(OBS_REG) == false)
            {
                _sqlCommand.Parameters.AddWithValue("@OBS_REG", OBS_REG);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@OBS_REG", DBNull.Value);
            }

            _sqlCommand.Parameters.AddWithValue("@FECHA_SOLICITUD", FECHA_SOLICITUD);
            _sqlCommand.Parameters.AddWithValue("@FECHA_ACTA", FECHA_ACTA);

            if (FECHA_CIERRE == new DateTime())
            {
                _sqlCommand.Parameters.AddWithValue("@FECHA_CIERRE", DBNull.Value);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@FECHA_CIERRE", FECHA_CIERRE);
            }

            _sqlCommand.Parameters.AddWithValue("@MOTIVO", MOTIVO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", ARCHIVO_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", ARCHIVO_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", ARCHIVO_TYPE);
            _sqlCommand.Parameters.AddWithValue("@USU_MOD", USU_MOD);

            SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO_ACTA", System.Data.SqlDbType.VarBinary);
            archivoParam.Value = ARCHIVO_ACTA;

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            Int32 cantidadRegistrosAfectados = 0;

            try
            {
                cantidadRegistrosAfectados = _sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteNonQuery): " + e.Message.ToString());
            }

            return cantidadRegistrosAfectados;
        }

        public Int32 ExecuteNonQueryParaActualizarTutelaConArchivos(Decimal ID_TUTELA,
            DateTime FECHA_R,
            DateTime FECHA_NOTIFICACION,
            DateTime FECHA_PLAZO_CONTESTA,
            DateTime FECHA_CONTESTACION,
            String PETICION,
            Byte[] ARCHIVO_PETICION,
            String ARCHIVO_PETICION_EXTENSION,
            Int32 ARCHIVO_PETICION_TAMANO,
            String ARCHIVO_PETICION_TYPE,
            String FALLO,
            String RESULTADO_FALLO,
            Byte[] ARCHIVO_FALLO,
            String ARCHIVO_FALLO_EXTENSION,
            Int32 ARCHIVO_FALLO_TAMANO,
            String ARCHIVO_FALLO_TYPE,
            Decimal RESPONSABLE,
            Decimal ID_EMPRESA,
            Decimal REGISTRO_CONTRATO,
            String CONSIDERACIONES,
            String USU_MOD)
        {
            string query = @"usp_con_reg_tutelas_actualizar_con_archivos @ID_TUTELA, @FECHA_R, @FECHA_NOTIFICACION, @FECHA_PLAZO_CONTESTA, @FECHA_CONTESTACION, @PETICION, @ARCHIVO_PETICION, @ARCHIVO_PETICION_EXTENSION, @ARCHIVO_PETICION_TAMANO, @ARCHIVO_PETICION_TYPE, @FALLO, @RESULTADO_FALLO, @ARCHIVO_FALLO, @ARCHIVO_FALLO_EXTENSION, @ARCHIVO_FALLO_TAMANO, @ARCHIVO_FALLO_TYPE, @RESPONSABLE, @ID_EMPRESA, @REGISTRO_CONTRATO, @CONSIDERACIONES, @USU_MOD";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_TUTELA", ID_TUTELA);

            SqlParameter archivoPeticionParam = _sqlCommand.Parameters.Add("@FECHA_R", System.Data.SqlDbType.DateTime);
            archivoPeticionParam.Value = FECHA_R;

            SqlParameter archivoPeticionParam1 = _sqlCommand.Parameters.Add("@FECHA_NOTIFICACION", System.Data.SqlDbType.DateTime);
            archivoPeticionParam1.Value = FECHA_NOTIFICACION;

            SqlParameter archivoPeticionParam2 = _sqlCommand.Parameters.Add("@FECHA_PLAZO_CONTESTA", System.Data.SqlDbType.DateTime);
            archivoPeticionParam2.Value = FECHA_PLAZO_CONTESTA;

            if (FECHA_CONTESTACION == new DateTime())
            {
                _sqlCommand.Parameters.AddWithValue("@FECHA_CONTESTACION", DBNull.Value);
            }
            else
            {
                SqlParameter archivoPeticionParam3 = _sqlCommand.Parameters.Add("@FECHA_CONTESTACION", System.Data.SqlDbType.DateTime);
                archivoPeticionParam3.Value = FECHA_CONTESTACION;
            }

            if (String.IsNullOrEmpty(PETICION) == true)
            {
                _sqlCommand.Parameters.AddWithValue("@PETICION", DBNull.Value);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@PETICION", PETICION);
            }

            SqlParameter archivoPeticionPara4 = _sqlCommand.Parameters.Add("@ARCHIVO_PETICION", System.Data.SqlDbType.VarBinary);
            archivoPeticionPara4.Value = ARCHIVO_PETICION;

            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_PETICION_EXTENSION", ARCHIVO_PETICION_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_PETICION_TAMANO", ARCHIVO_PETICION_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_PETICION_TYPE", ARCHIVO_PETICION_TYPE);

            if (String.IsNullOrEmpty(FALLO) == true)
            {
                _sqlCommand.Parameters.AddWithValue("@FALLO", DBNull.Value);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@FALLO", FALLO);
            }

            if (String.IsNullOrEmpty(RESULTADO_FALLO) == true)
            {
                _sqlCommand.Parameters.AddWithValue("@RESULTADO_FALLO", DBNull.Value);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@RESULTADO_FALLO", RESULTADO_FALLO);
            }

            SqlParameter archivoFalloParam = _sqlCommand.Parameters.Add("@ARCHIVO_FALLO", System.Data.SqlDbType.VarBinary);
            archivoFalloParam.Value = ARCHIVO_FALLO;

            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_FALLO_EXTENSION", ARCHIVO_FALLO_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_FALLO_TAMANO", ARCHIVO_FALLO_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_FALLO_TYPE", ARCHIVO_FALLO_TYPE);

            _sqlCommand.Parameters.AddWithValue("@RESPONSABLE", RESPONSABLE);
            _sqlCommand.Parameters.AddWithValue("@ID_EMPRESA", ID_EMPRESA);
            _sqlCommand.Parameters.AddWithValue("@REGISTRO_CONTRATO", REGISTRO_CONTRATO);

            if (String.IsNullOrEmpty(CONSIDERACIONES) == true)
            {
                _sqlCommand.Parameters.AddWithValue("@CONSIDERACIONES", DBNull.Value);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@CONSIDERACIONES", CONSIDERACIONES);
            }

            _sqlCommand.Parameters.AddWithValue("@USU_MOD", USU_MOD);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            Int32 cantidadRegistrosAfectados = 0;

            try
            {
                cantidadRegistrosAfectados = _sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteNonQuery): " + e.Message.ToString());
            }

            return cantidadRegistrosAfectados;
        }

        public Int32 ExecuteNonQueryParaActualizarTutelaConArchivoPeticion(Decimal ID_TUTELA,
            DateTime FECHA_R,
            DateTime FECHA_NOTIFICACION,
            DateTime FECHA_PLAZO_CONTESTA,
            DateTime FECHA_CONTESTACION,
            String PETICION,
            Byte[] ARCHIVO_PETICION,
            String ARCHIVO_PETICION_EXTENSION,
            Int32 ARCHIVO_PETICION_TAMANO,
            String ARCHIVO_PETICION_TYPE,
            String FALLO,
            String RESULTADO_FALLO,
            Decimal RESPONSABLE,
            Decimal ID_EMPRESA,
            Decimal REGISTRO_CONTRATO,
            String CONSIDERACIONES,
            String USU_MOD)
        {
            string query = @"usp_con_reg_tutelas_actualizar_con_archivo_peticion @ID_TUTELA, @FECHA_R, @FECHA_NOTIFICACION, @FECHA_PLAZO_CONTESTA, @FECHA_CONTESTACION, @PETICION, @ARCHIVO_PETICION, @ARCHIVO_PETICION_EXTENSION, @ARCHIVO_PETICION_TAMANO, @ARCHIVO_PETICION_TYPE, @FALLO, @RESULTADO_FALLO,  @RESPONSABLE, @ID_EMPRESA, @REGISTRO_CONTRATO, @CONSIDERACIONES, @USU_MOD";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_TUTELA", ID_TUTELA);

            SqlParameter archivoPeticionParam = _sqlCommand.Parameters.Add("@FECHA_R", System.Data.SqlDbType.DateTime);
            archivoPeticionParam.Value = FECHA_R;

            SqlParameter archivoPeticionParam1 = _sqlCommand.Parameters.Add("@FECHA_NOTIFICACION", System.Data.SqlDbType.DateTime);
            archivoPeticionParam1.Value = FECHA_NOTIFICACION;

            SqlParameter archivoPeticionParam2 = _sqlCommand.Parameters.Add("@FECHA_PLAZO_CONTESTA", System.Data.SqlDbType.DateTime);
            archivoPeticionParam2.Value = FECHA_PLAZO_CONTESTA;

            if (FECHA_CONTESTACION == new DateTime())
            {
                _sqlCommand.Parameters.AddWithValue("@FECHA_CONTESTACION", DBNull.Value);
            }
            else
            {
                SqlParameter archivoPeticionParam3 = _sqlCommand.Parameters.Add("@FECHA_CONTESTACION", System.Data.SqlDbType.DateTime);
                archivoPeticionParam3.Value = FECHA_CONTESTACION;
            }

            if (String.IsNullOrEmpty(PETICION) == true)
            {
                _sqlCommand.Parameters.AddWithValue("@PETICION", DBNull.Value);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@PETICION", PETICION);
            }

            SqlParameter archivoPeticionParam4 = _sqlCommand.Parameters.Add("@ARCHIVO_PETICION", System.Data.SqlDbType.VarBinary);
            archivoPeticionParam4.Value = ARCHIVO_PETICION;

            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_PETICION_EXTENSION", ARCHIVO_PETICION_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_PETICION_TAMANO", ARCHIVO_PETICION_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_PETICION_TYPE", ARCHIVO_PETICION_TYPE);

            if (String.IsNullOrEmpty(FALLO) == true)
            {
                _sqlCommand.Parameters.AddWithValue("@FALLO", DBNull.Value);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@FALLO", FALLO);
            }

            if (String.IsNullOrEmpty(RESULTADO_FALLO) == true)
            {
                _sqlCommand.Parameters.AddWithValue("@RESULTADO_FALLO", DBNull.Value);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@RESULTADO_FALLO", RESULTADO_FALLO);
            }

            _sqlCommand.Parameters.AddWithValue("@RESPONSABLE", RESPONSABLE);
            _sqlCommand.Parameters.AddWithValue("@ID_EMPRESA", ID_EMPRESA);
            _sqlCommand.Parameters.AddWithValue("@REGISTRO_CONTRATO", REGISTRO_CONTRATO);

            if (String.IsNullOrEmpty(CONSIDERACIONES) == true)
            {
                _sqlCommand.Parameters.AddWithValue("@CONSIDERACIONES", DBNull.Value);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@CONSIDERACIONES", CONSIDERACIONES);
            }

            _sqlCommand.Parameters.AddWithValue("@USU_MOD", USU_MOD);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            Int32 cantidadRegistrosAfectados = 0;

            try
            {
                cantidadRegistrosAfectados = _sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteNonQuery): " + e.Message.ToString());
            }

            return cantidadRegistrosAfectados;
        }


        public Int32 ExecuteNonQueryParaActualizarTutelaConArchivoFallo(Decimal ID_TUTELA,
            DateTime FECHA_R,
            DateTime FECHA_NOTIFICACION,
            DateTime FECHA_PLAZO_CONTESTA,
            DateTime FECHA_CONTESTACION,
            String PETICION,
            String FALLO,
            String RESULTADO_FALLO,
            Byte[] ARCHIVO_FALLO,
            String ARCHIVO_FALLO_EXTENSION,
            Int32 ARCHIVO_FALLO_TAMANO,
            String ARCHIVO_FALLO_TYPE,
            Decimal RESPONSABLE,
            Decimal ID_EMPRESA,
            Decimal REGISTRO_CONTRATO,
            String CONSIDERACIONES,
            String USU_MOD)
        {
            string query = @"usp_con_reg_tutelas_actualizar_con_archivo_fallo @ID_TUTELA, @FECHA_R, @FECHA_NOTIFICACION, @FECHA_PLAZO_CONTESTA, @FECHA_CONTESTACION, @PETICION, @FALLO, @RESULTADO_FALLO, @ARCHIVO_FALLO, @ARCHIVO_FALLO_EXTENSION, @ARCHIVO_FALLO_TAMANO, @ARCHIVO_FALLO_TYPE, @RESPONSABLE, @ID_EMPRESA, @REGISTRO_CONTRATO, @CONSIDERACIONES, @USU_MOD";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_TUTELA", ID_TUTELA);

            SqlParameter archivoFalloParam = _sqlCommand.Parameters.Add("@FECHA_R", System.Data.SqlDbType.DateTime);
            archivoFalloParam.Value = FECHA_R;

            SqlParameter archivoFalloParam1 = _sqlCommand.Parameters.Add("@FECHA_NOTIFICACION", System.Data.SqlDbType.DateTime);
            archivoFalloParam1.Value = FECHA_NOTIFICACION;

            SqlParameter archivoFalloParam2 = _sqlCommand.Parameters.Add("@FECHA_PLAZO_CONTESTA", System.Data.SqlDbType.DateTime);
            archivoFalloParam2.Value = FECHA_PLAZO_CONTESTA;

            if (FECHA_CONTESTACION == new DateTime())
            {
                _sqlCommand.Parameters.AddWithValue("@FECHA_CONTESTACION", DBNull.Value);
            }
            else
            {
                SqlParameter archivoFalloParam3 = _sqlCommand.Parameters.Add("@FECHA_CONTESTACION", System.Data.SqlDbType.DateTime);
                archivoFalloParam3.Value = FECHA_CONTESTACION;
            }

            if (String.IsNullOrEmpty(PETICION) == true)
            {
                _sqlCommand.Parameters.AddWithValue("@PETICION", DBNull.Value);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@PETICION", PETICION);
            }

            if (String.IsNullOrEmpty(FALLO) == true)
            {
                _sqlCommand.Parameters.AddWithValue("@FALLO", DBNull.Value);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@FALLO", FALLO);
            }

            if (String.IsNullOrEmpty(RESULTADO_FALLO) == true)
            {
                _sqlCommand.Parameters.AddWithValue("@RESULTADO_FALLO", DBNull.Value);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@RESULTADO_FALLO", RESULTADO_FALLO);
            }

            SqlParameter archivoFalloPara4 = _sqlCommand.Parameters.Add("@ARCHIVO_FALLO", System.Data.SqlDbType.VarBinary);
            archivoFalloPara4.Value = ARCHIVO_FALLO;

            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_FALLO_EXTENSION", ARCHIVO_FALLO_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_FALLO_TAMANO", ARCHIVO_FALLO_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_FALLO_TYPE", ARCHIVO_FALLO_TYPE);

            _sqlCommand.Parameters.AddWithValue("@RESPONSABLE", RESPONSABLE);
            _sqlCommand.Parameters.AddWithValue("@ID_EMPRESA", ID_EMPRESA);
            _sqlCommand.Parameters.AddWithValue("@REGISTRO_CONTRATO", REGISTRO_CONTRATO);

            if (String.IsNullOrEmpty(CONSIDERACIONES) == true)
            {
                _sqlCommand.Parameters.AddWithValue("@CONSIDERACIONES", DBNull.Value);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@CONSIDERACIONES", CONSIDERACIONES);
            }

            _sqlCommand.Parameters.AddWithValue("@USU_MOD", USU_MOD);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            Int32 cantidadRegistrosAfectados = 0;

            try
            {
                cantidadRegistrosAfectados = _sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteNonQuery): " + e.Message.ToString());
            }

            return cantidadRegistrosAfectados;
        }

        public Int32 ExecuteNonQueryParaActualizarPruebaConImagen(Decimal REGISTRO,
            Decimal ID_PRUEBA,
            Decimal ID_CATEGORIA,
            DateTime FECHA_R,
            String OBS_PRUEBA,
            String RESULTADOS,
            String USU_MOD,
            Byte[] ARCHIVO_PRUEBA,
            String ARCHIVO_EXTENSION,
            Decimal ARCHIVO_TAMANO,
            String ARCHIVO_TYPE)
        {
            string query = @"usp_sel_reg_aplicacion_pruebas_actualizar_con_imagen @REGISTRO, @ID_PRUEBA, @ID_CATEGORIA, @FECHA_R, @OBS_PRUEBA, @RESULTADOS, @USU_MOD, @ARCHIVO_PRUEBA, @ARCHIVO_TAMANO, @ARCHIVO_EXTENSION, @ARCHIVO_TYPE ";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@REGISTRO", REGISTRO);
            _sqlCommand.Parameters.AddWithValue("@ID_PRUEBA", ID_PRUEBA);
            _sqlCommand.Parameters.AddWithValue("@ID_CATEGORIA", ID_CATEGORIA);
            _sqlCommand.Parameters.AddWithValue("@FECHA_R", FECHA_R);
            _sqlCommand.Parameters.AddWithValue("@OBS_PRUEBA", OBS_PRUEBA);
            _sqlCommand.Parameters.AddWithValue("@RESULTADOS", RESULTADOS);
            _sqlCommand.Parameters.AddWithValue("@USU_MOD", USU_MOD);

            SqlParameter imageParam = _sqlCommand.Parameters.Add("@ARCHIVO_PRUEBA", System.Data.SqlDbType.Image);
            imageParam.Value = ARCHIVO_PRUEBA;
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", ARCHIVO_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", ARCHIVO_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", ARCHIVO_TYPE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;

            _sqlCommand.Transaction = _sqlTransaction;

            Int32 cantidadRegistrosAfectados = 0;

            try
            {
                cantidadRegistrosAfectados = _sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteNonQuery): " + e.Message.ToString());
            }

            return cantidadRegistrosAfectados;
        }

        public Int32 ExecuteNonQueryParaActualizarArchivoInvestigacionAdministrativa(Decimal ID_ARCHIVO_I,
            Byte[] ARCHIVO,
            String ARCHIVO_EXTENSION,
            Decimal ARCHIVO_TAMANO,
            String ARCHIVO_TYPE,
            String USU_MOD)
        {
            string query = @"usp_reg_archivos_investigaciones_administrativas_actualizar @ID_ARCHIVO_I, @ARCHIVO, @ARCHIVO_EXTENSION, @ARCHIVO_TAMANO, @ARCHIVO_TYPE, @USU_MOD";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_ARCHIVO_I", ID_ARCHIVO_I);

            SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
            archivoParam.Value = ARCHIVO;

            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", ARCHIVO_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", ARCHIVO_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", ARCHIVO_TYPE);

            _sqlCommand.Parameters.AddWithValue("@USU_MOD", USU_MOD);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;

            _sqlCommand.Transaction = _sqlTransaction;

            Int32 cantidadRegistrosAfectados = 0;

            try
            {
                cantidadRegistrosAfectados = _sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteNonQuery): " + e.Message.ToString());
            }

            return cantidadRegistrosAfectados;
        }




        public String ExecuteEscalarParaAdicionarArchivosAbogado(Decimal ID_ABOGADO,
            Byte[] ARCHIVO_ADJUNTO,
            String ARCHIVO_ADJUNTO_EXTENSION,
            Decimal ARCHIVO_ADJUNTO_TAMANO,
            String ARCHIVO_ADJUNTO_TYPE,
            String USU_CRE)
        {
            String valor = null;
            String query = @"USP_REG_ABOGADO_ARCHIVO_ADICIONAR @ID_ABOGADO, @ARCHIVO_ADJUNTO, @ARCHIVO_ADJUNTO_EXTENSION, @ARCHIVO_ADJUNTO_TAMANO, @ARCHIVO_ADJUNTO_TYPE, @USU_CRE";

            _sqlCommand = new SqlCommand();

            if (ID_ABOGADO == 0)
            {
                _sqlCommand.Parameters.AddWithValue("@ID_ABOGADO", DBNull.Value);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@ID_ABOGADO", ID_ABOGADO);
            }

            if (ARCHIVO_ADJUNTO == null)
            {
                SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO_ADJUNTO", System.Data.SqlDbType.VarBinary);
                archivoParam.Value = DBNull.Value;
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_ADJUNTO_EXTENSION", DBNull.Value);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_ADJUNTO_TAMANO", DBNull.Value);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_ADJUNTO_TYPE", DBNull.Value);
            }
            else
            {
                SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO_ADJUNTO", System.Data.SqlDbType.VarBinary);
                archivoParam.Value = ARCHIVO_ADJUNTO;

                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_ADJUNTO_EXTENSION", ARCHIVO_ADJUNTO_EXTENSION);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_ADJUNTO_TAMANO", ARCHIVO_ADJUNTO_TAMANO);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_ADJUNTO_TYPE", ARCHIVO_ADJUNTO_TYPE);
            }

            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = _sqlCommand.ExecuteScalar().ToString();
            }
            catch (NullReferenceException)
            {
                valor = null;
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }


        public String ExecuteEscalarParaAdicionaNuevaActaComite(DateTime FECHA_R,
             DateTime FECHA_ACTA,
             String TITULO,
             String TIPO_DOCUMENTO,
             Byte[] ARCHIVO,
             String ARCHIVO_EXTENSION,
             Decimal ARCHIVO_TAMANO,
             String ARCHIVO_TYPE,
             String ACTIVO,
             String USU_CRE)
        {
            String valor = null;
            String query = @"usp_reg_comite_convivencia_adicionar_acta_comite @FECHA_R, @FECHA_ACTA, @TITULO, @TIPO_DOCUMENTO, @ARCHIVO, @ARCHIVO_EXTENSION, @ARCHIVO_TAMANO, @ARCHIVO_TYPE, @ACTIVO, @USU_CRE";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@FECHA_R", FECHA_R);
            _sqlCommand.Parameters.AddWithValue("@FECHA_ACTA", FECHA_ACTA);

            if (String.IsNullOrEmpty(TITULO) == false)
            {
                _sqlCommand.Parameters.AddWithValue("@TITULO", TITULO);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@TITULO", DBNull.Value);
            }

            _sqlCommand.Parameters.AddWithValue("@TIPO_DOCUMENTO", TIPO_DOCUMENTO);

            if (ARCHIVO == null)
            {
                SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
                archivoParam.Value = DBNull.Value;
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", DBNull.Value);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", DBNull.Value);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", DBNull.Value);
            }
            else
            {
                SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
                archivoParam.Value = ARCHIVO;

                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", ARCHIVO_EXTENSION);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", ARCHIVO_TAMANO);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", ARCHIVO_TYPE);
            }

            _sqlCommand.Parameters.AddWithValue("@ACTIVO", ACTIVO);
            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = _sqlCommand.ExecuteScalar().ToString();
            }
            catch (NullReferenceException)
            {
                valor = null;
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }


        public String ExecuteEscalarParaAdicionaNuevoDocumentoLegal(DateTime FECHA_R,
             DateTime FECHA_INICIAL,
             DateTime FECHA_FINAL,
             String TIPO_DOCUMENTO,
             Byte[] ARCHIVO,
             String ARCHIVO_EXTENSION,
             Decimal ARCHIVO_TAMANO,
             String ARCHIVO_TYPE,
             String ACTIVO,
             String USU_CRE)
        {
            String valor = null;
            String query = @"usp_reg_documentos_legales_adicionar_documento_nuevo @FECHA_R, @FECHA_INICIAL, @FECHA_FINAL, @TIPO_DOCUMENTO, @ARCHIVO, @ARCHIVO_EXTENSION, @ARCHIVO_TAMANO, @ARCHIVO_TYPE, @ACTIVO, @USU_CRE";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@FECHA_R", FECHA_R);
            _sqlCommand.Parameters.AddWithValue("@FECHA_INICIAL", FECHA_INICIAL);

            if (FECHA_FINAL == new DateTime())
            {
                _sqlCommand.Parameters.AddWithValue("@FECHA_FINAL", DBNull.Value);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@FECHA_FINAL", FECHA_FINAL);
            }

            _sqlCommand.Parameters.AddWithValue("@TIPO_DOCUMENTO", TIPO_DOCUMENTO);

            if (ARCHIVO == null)
            {
                SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
                archivoParam.Value = DBNull.Value;
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", DBNull.Value);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", DBNull.Value);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", DBNull.Value);
            }
            else
            {
                SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
                archivoParam.Value = ARCHIVO;

                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", ARCHIVO_EXTENSION);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", ARCHIVO_TAMANO);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", ARCHIVO_TYPE);
            }

            _sqlCommand.Parameters.AddWithValue("@ACTIVO", ACTIVO);
            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = _sqlCommand.ExecuteScalar().ToString();
            }
            catch (NullReferenceException)
            {
                valor = null;
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }


        public String ExecuteEscalarParaAdicionaArchivoInvestigacionAdministrativa(Decimal ID_INVESTIGACION,
             DateTime FECHA_R,
             String TIPO_DOCUMENTO,
             Byte[] ARCHIVO,
             String ARCHIVO_EXTENSION,
             Decimal ARCHIVO_TAMANO,
             String ARCHIVO_TYPE,
             String USU_CRE)
        {
            String valor = null;
            String query = @"usp_reg_archivos_investigaciones_administrativas_adicionar @ID_INVESTIGACION, @FECHA_R, @TIPO_DOCUMENTO, @ARCHIVO, @ARCHIVO_EXTENSION, @ARCHIVO_TAMANO, @ARCHIVO_TYPE, @USU_CRE";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_INVESTIGACION", ID_INVESTIGACION);
            _sqlCommand.Parameters.AddWithValue("@FECHA_R", FECHA_R);
            _sqlCommand.Parameters.AddWithValue("@TIPO_DOCUMENTO", TIPO_DOCUMENTO);

            if (ARCHIVO == null)
            {
                SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
                archivoParam.Value = DBNull.Value;
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", DBNull.Value);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", DBNull.Value);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", DBNull.Value);
            }
            else
            {
                SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
                archivoParam.Value = ARCHIVO;

                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", ARCHIVO_EXTENSION);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", ARCHIVO_TAMANO);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", ARCHIVO_TYPE);
            }

            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = _sqlCommand.ExecuteScalar().ToString();
            }
            catch (NullReferenceException)
            {
                valor = null;
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }


        public String ExecuteEscalarParaAdicionaArchivoRequermientoMinisterio(Decimal ID_REQUERIMIENTO_M,
             DateTime FECHA_R,
             String TIPO_DOCUMENTO,
             Byte[] ARCHIVO,
             String ARCHIVO_EXTENSION,
             Decimal ARCHIVO_TAMANO,
             String ARCHIVO_TYPE,
             String USU_CRE)
        {
            String valor = null;
            String query = @"usp_reg_archivos_requerimientos_ministerio_adicionar @ID_REQUERIMINETO_M, @FECHA_R, @TIPO_DOCUMENTO, @ARCHIVO, @ARCHIVO_EXTENSION, @ARCHIVO_TAMANO, @ARCHIVO_TYPE, @USU_CRE";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_REQUERIMINETO_M", ID_REQUERIMIENTO_M);
            _sqlCommand.Parameters.AddWithValue("@FECHA_R", FECHA_R);
            _sqlCommand.Parameters.AddWithValue("@TIPO_DOCUMENTO", TIPO_DOCUMENTO);

            if (ARCHIVO == null)
            {
                SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
                archivoParam.Value = DBNull.Value;
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", DBNull.Value);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", DBNull.Value);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", DBNull.Value);
            }
            else
            {
                SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
                archivoParam.Value = ARCHIVO;

                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", ARCHIVO_EXTENSION);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", ARCHIVO_TAMANO);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", ARCHIVO_TYPE);
            }

            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = _sqlCommand.ExecuteScalar().ToString();
            }
            catch (NullReferenceException)
            {
                valor = null;
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }

        public String ExecuteEscalarParaAdicionaArchivoCotizacionOrdenCompra(Decimal ID_ORDEN,
             DateTime FECHA_R,
             Byte[] ARCHIVO,
             String ARCHIVO_EXTENSION,
             Decimal ARCHIVO_TAMANO,
             String ARCHIVO_TYPE,
             String USU_CRE)
        {
            String valor = null;
            String query = @"usp_alm_reg_cotizaciones_adicionar @ID_ORDEN, @FECHA_R, @ARCHIVO, @ARCHIVO_EXTENSION, @ARCHIVO_TAMANO, @ARCHIVO_TYPE, @USU_CRE";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_ORDEN", ID_ORDEN);
            _sqlCommand.Parameters.AddWithValue("@FECHA_R", FECHA_R);

            SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
            archivoParam.Value = ARCHIVO;

            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", ARCHIVO_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", ARCHIVO_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", ARCHIVO_TYPE);

            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = _sqlCommand.ExecuteScalar().ToString();
            }
            catch (NullReferenceException)
            {
                valor = null;
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }

        public String ExecuteEscalarParaAdicionarDocsAfiliacion(Decimal ID_CONTRATO
            , String ENTIDAD_AFILIACION
            , Byte[] ARCHIVO
            , String ARCHIVO_EXTENSION
            , Decimal ARCHIVO_TAMANO
            , String ARCHIVO_TYPE
            , String USU_CRE)
        {
            String valor = null;
            String query = @"usp_reg_docs_radicacion_afiliaciones_adicionar @ID_CONTRATO, @ENTIDAD_AFILIACION, @ARCHIVO, @ARCHIVO_EXTENSION, @ARCHIVO_TAMANO, @ARCHIVO_TYPE, @USU_CRE";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_CONTRATO", ID_CONTRATO);
            _sqlCommand.Parameters.AddWithValue("@ENTIDAD_AFILIACION", ENTIDAD_AFILIACION);
            _sqlCommand.Parameters.AddWithValue("@FECHA_R", DateTime.Now);

            SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
            archivoParam.Value = ARCHIVO;

            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", ARCHIVO_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", ARCHIVO_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", ARCHIVO_TYPE);

            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = _sqlCommand.ExecuteScalar().ToString();
            }
            catch (NullReferenceException)
            {
                valor = null;
            }
            catch (SqlException e)
            {
                valor = null;
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }

        public Int32 ExecuteNonQueryParaActualizarConRegExamenesEmpleadoConArchivo(Decimal REGISTRO,
            Decimal ID_ORDEN,
            Decimal ID_EXAMEN,
            Decimal COSTO,
            String VALIDADO,
            DateTime FECHA_EXAMEN,
            String USU_MOD,
            byte[] ARCHIVO_EXAMEN,
            String ARCHIVO_EXTENSION,
            Decimal ARCHIVO_TAMANO,
            String ARCHIVO_TYPE)
        {
            string query = @"USP_CON_REG_EXAMENES_EMPLEADO_ACTUALIZAR_CON_ARCHIVO @REGISTRO, @ID_ORDEN, @ID_EXAMEN, @COSTO, @VALIDADO, @FECHA_EXAMEN, @USU_MOD, @ARCHIVO_EXAMEN, @ARCHIVO_EXTENSION, @ARCHIVO_TAMANO, @ARCHIVO_TYPE";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@REGISTRO", REGISTRO);
            _sqlCommand.Parameters.AddWithValue("@ID_ORDEN", ID_ORDEN);
            _sqlCommand.Parameters.AddWithValue("@ID_EXAMEN", ID_EXAMEN);
            _sqlCommand.Parameters.AddWithValue("@COSTO", COSTO);
            _sqlCommand.Parameters.AddWithValue("@VALIDADO", VALIDADO);
            _sqlCommand.Parameters.AddWithValue("@FECHA_EXAMEN", FECHA_EXAMEN);
            _sqlCommand.Parameters.AddWithValue("@USU_MOD", USU_MOD);

            SqlParameter imageParam = _sqlCommand.Parameters.Add("@ARCHIVO_EXAMEN", System.Data.SqlDbType.Image);
            imageParam.Value = ARCHIVO_EXAMEN;
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", ARCHIVO_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", ARCHIVO_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", ARCHIVO_TYPE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;

            _sqlCommand.Transaction = _sqlTransaction;

            Int32 cantidadRegistrosAfectados = 0;

            try
            {
                cantidadRegistrosAfectados = _sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteNonQuery): " + e.Message.ToString());
            }

            return cantidadRegistrosAfectados;
        }

        public String ExecuteEscalarParaAdicionarDocsManualServixo(Decimal ID_VERSIONAMIENTO
            , Byte[] ARCHIVO
            , String ARCHIVO_EXTENSION
            , Decimal ARCHIVO_TAMANO
            , String ARCHIVO_TYPE
            , String USU_CRE)
        {
            String valor = null;
            String query = @"usp_oper_docs_versionamiento_manual_adicionar @ID_VERSIONAMIENTO, @ARCHIVO, @ARCHIVO_EXTENSION, @ARCHIVO_TAMANO, @ARCHIVO_TYPE, @USU_CRE";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_VERSIONAMIENTO", ID_VERSIONAMIENTO);

            SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
            archivoParam.Value = ARCHIVO;

            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", ARCHIVO_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", ARCHIVO_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", ARCHIVO_TYPE);

            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = _sqlCommand.ExecuteScalar().ToString();
            }
            catch (NullReferenceException)
            {
                valor = null;
            }
            catch (SqlException e)
            {
                valor = null;
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }

        public Decimal ExecuteEscalarParaAdicionarDocsAdjuntosDetalleActividad(Decimal ID_DETALLE,
         String TIPO_ARCHIVO,
         byte[] ARCHIVO,
         String ARCHIVO_EXTENSION,
         Decimal ARCHIVO_TAMANO,
         String ARCHIVO_TYPE,
         String USU_CRE,
         String TITULO,
         String DESCRIPCION)
        {
            Decimal valor = 0;
            String query = @"usp_prog_archivos_adjuntos_actividades_adicionar @ID_DETALLE, @TIPO_ARCHIVO, @ARCHIVO, @ARCHIVO_EXTENSION, @ARCHIVO_TAMANAO, @ARCHIVO_TYPE, @USU_CRE, @TITULO, @DESCRIPCION";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_DETALLE", ID_DETALLE);
            _sqlCommand.Parameters.AddWithValue("@TIPO_ARCHIVO", TIPO_ARCHIVO);

            SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
            archivoParam.Value = ARCHIVO;

            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", ARCHIVO_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANAO", ARCHIVO_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", ARCHIVO_TYPE);

            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);

            if (String.IsNullOrEmpty(TITULO) == false)
            {
                _sqlCommand.Parameters.AddWithValue("@TITULO", TITULO);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@TITULO", DBNull.Value);
            }

            if (String.IsNullOrEmpty(TITULO) == false)
            {
                _sqlCommand.Parameters.AddWithValue("@DESCRIPCION", DESCRIPCION);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@DESCRIPCION", DBNull.Value);
            }

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = Convert.ToDecimal(_sqlCommand.ExecuteScalar().ToString());
            }
            catch (NullReferenceException)
            {
                valor = 0;
            }
            catch (SqlException e)
            {
                valor = 0;
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }

        public Decimal ExecuteEscalarParaAdicionarDocsSeguimientoCompromiso(Decimal ID_MAESTRA_COMPROMISO,
             String SEGUIMIENTO,
             String DESCRIPCION,
             byte[] ARCHIVO,
             String ARCHIVO_EXTENSION,
             Decimal ARCHIVO_TAMANO,
             String ARCHIVO_TYPE,
             String USU_CRE,
             DateTime FCH_CRE)
        {
            Decimal valor = 0;
            String query = @"usp_prog_seguimiento_compromisos__adicionar @ID_MAESTRA_COMPROMISO, @SEGUIMIENTO, @DESCRIPCION, @ARCHIVO, @ARCHIVO_EXTENSION, @ARCHIVO_TAMANO, @ARCHIVO_TYPE, @USU_CRE, @FCH_CRE";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_MAESTRA_COMPROMISO", ID_MAESTRA_COMPROMISO);
            _sqlCommand.Parameters.AddWithValue("@SEGUIMIENTO", SEGUIMIENTO);
            _sqlCommand.Parameters.AddWithValue("@DESCRIPCION", DESCRIPCION);


            if (ARCHIVO == null)
            {
                SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
                archivoParam.Value = DBNull.Value;
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", DBNull.Value);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", DBNull.Value);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", DBNull.Value);
            }
            else
            {
                SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
                archivoParam.Value = ARCHIVO;
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", ARCHIVO_EXTENSION);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", ARCHIVO_TAMANO);
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", ARCHIVO_TYPE);
            }

            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);
            _sqlCommand.Parameters.AddWithValue("@FCH_CRE", FCH_CRE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = Convert.ToDecimal(_sqlCommand.ExecuteScalar().ToString());
            }
            catch (NullReferenceException)
            {
                valor = 0;
            }
            catch (SqlException e)
            {
                valor = 0;
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }

        public Decimal ExecuteEscalarParaAdicionarDocsParaAssesmentCenter(Decimal ID_ASSESMENT,
            byte[] ARCHIVO_DOCUMENTO,
            String ARCHIVO_EXTENSION,
            Decimal ARCHIVO_TAMANO,
            String ARCHIVO_TYPE,
            String USU_CRE)
        {
            Decimal valor = 0;
            String query = @"usp_sel_reg_documentos_assesment_adicionar @ID_ASSESMENT, @ARCHIVO_DOCUMENTO, @ARCHIVO_EXTENSION, @ARCHIVO_TAMANO, @ARCHIVO_TYPE, @USU_CRE";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_ASSESMENT", ID_ASSESMENT);

            SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO_DOCUMENTO", System.Data.SqlDbType.VarBinary);
            archivoParam.Value = ARCHIVO_DOCUMENTO;

            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", ARCHIVO_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", ARCHIVO_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", ARCHIVO_TYPE);

            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = Convert.ToDecimal(_sqlCommand.ExecuteScalar().ToString());
            }
            catch (NullReferenceException)
            {
                valor = 0;
            }
            catch (SqlException e)
            {
                valor = 0;
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }


        public Decimal ExecuteEscalarParaAdicionarClausula(string idTipoClausula, string idEstado, string descripcion,
            decimal idEmpresa, decimal idOcupacion, Byte[] archivo, Int32 archivoTamaño, string archivoExtension,
            string archivoTipo, string usuario)
        {
            Decimal valor = 0;
            String query = @"usp_clausulas_adicionar @ID_TIPO_CLAUSULA, @ID_ESTADO, @DESCRIPCION, @ID_EMPRESA, @ID_OCUPACION, 
                @ARCHIVO, @TAMAÑO, @EXTENSION, @TIPO, @USU_CRE";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_TIPO_CLAUSULA", idTipoClausula);
            _sqlCommand.Parameters.AddWithValue("@ID_ESTADO", idEstado);
            _sqlCommand.Parameters.AddWithValue("@DESCRIPCION", descripcion);
            _sqlCommand.Parameters.AddWithValue("@ID_EMPRESA", idEmpresa);
            _sqlCommand.Parameters.AddWithValue("@ID_OCUPACION", idOcupacion);
            SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
            archivoParam.Value = archivo;
            _sqlCommand.Parameters.AddWithValue("@TAMAÑO", archivoTamaño);
            _sqlCommand.Parameters.AddWithValue("@EXTENSION", archivoExtension);
            _sqlCommand.Parameters.AddWithValue("@TIPO", archivoTipo);
            _sqlCommand.Parameters.AddWithValue("@USU_CRE", usuario);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;

            try
            {
                valor = Convert.ToDecimal(_sqlCommand.ExecuteScalar().ToString());
            }
            catch (NullReferenceException)
            {
                valor = 0;
            }
            catch (SqlException e)
            {
                valor = 0;
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }

        public Decimal ExecuteEscalarParaAdicionarClausulaYEmpleadoClausula(String idTipoClausula,
            String idEstado,
            String descripcion,
            Decimal idEmpresa,
            Decimal idOcupacion,
            Byte[] archivo,
            Int32 archivoTamaño,
            String archivoExtension,
            String archivoTipo,
            String usuario,
            Decimal idContrato)
        {
            Decimal valor = 0;
            String query = @"usp_empleado_clausulas_adicionar_clausula_y_clausula_empleado @ID_TIPO_CLAUSULA, @ID_ESTADO, @DESCRIPCION, @ID_EMPRESA, @ID_OCUPACION, @ARCHIVO, @TAMAÑO, @EXTENSION, @TIPO, @USU_CRE, @ID_CONTRATO";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_TIPO_CLAUSULA", idTipoClausula);
            _sqlCommand.Parameters.AddWithValue("@ID_ESTADO", idEstado);
            _sqlCommand.Parameters.AddWithValue("@DESCRIPCION", descripcion);
            _sqlCommand.Parameters.AddWithValue("@ID_EMPRESA", idEmpresa);
            _sqlCommand.Parameters.AddWithValue("@ID_OCUPACION", idOcupacion);
            SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
            archivoParam.Value = archivo;
            _sqlCommand.Parameters.AddWithValue("@TAMAÑO", archivoTamaño);
            _sqlCommand.Parameters.AddWithValue("@EXTENSION", archivoExtension);
            _sqlCommand.Parameters.AddWithValue("@TIPO", archivoTipo);
            _sqlCommand.Parameters.AddWithValue("@USU_CRE", usuario);
            _sqlCommand.Parameters.AddWithValue("@ID_CONTRATO", idContrato);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = Convert.ToDecimal(_sqlCommand.ExecuteScalar().ToString());
            }
            catch (NullReferenceException)
            {
                valor = 0;
            }
            catch (SqlException e)
            {
                valor = 0;
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }

        public bool ExecuteEscalarParaActualizarClausula(decimal idClausula, string idTipoClausula, string idEstado, string descripcion,
            decimal idEmpresa, decimal idOcupacion, Byte[] archivo, Int32 archivoTamaño, string archivoExtension,
            string archivoTipo, string usuario)
        {
            Decimal valor = 0;
            bool actualizado = true;
            String query;

            _sqlCommand = new SqlCommand();

            if (archivo != null)
            {
                query = @"usp_clausulas_actualizarConArchivo @ID_CLAUSULA, @ID_TIPO_CLAUSULA, @ID_ESTADO, @DESCRIPCION, @ID_EMPRESA, @ID_OCUPACION, 
                @ARCHIVO, @TAMAÑO, @EXTENSION, @TIPO, @USU_MOD";

                _sqlCommand.Parameters.AddWithValue("@ID_CLAUSULA", idClausula);
                _sqlCommand.Parameters.AddWithValue("@ID_TIPO_CLAUSULA", idTipoClausula);
                _sqlCommand.Parameters.AddWithValue("@ID_ESTADO", idEstado);
                _sqlCommand.Parameters.AddWithValue("@DESCRIPCION", descripcion);
                _sqlCommand.Parameters.AddWithValue("@ID_EMPRESA", idEmpresa);
                _sqlCommand.Parameters.AddWithValue("@ID_OCUPACION", idOcupacion);

                SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
                archivoParam.Value = archivo == null ? (object)DBNull.Value : archivo;
                _sqlCommand.Parameters.AddWithValue("@TAMAÑO", archivoTamaño);
                _sqlCommand.Parameters.AddWithValue("@EXTENSION", archivoExtension == null ? (object)DBNull.Value : archivoExtension);
                _sqlCommand.Parameters.AddWithValue("@TIPO", archivoTipo == null ? (object)DBNull.Value : archivoTipo);
                _sqlCommand.Parameters.AddWithValue("@USU_MOD", usuario);
            }
            else
            {
                query = @"usp_clausulas_actualizarSinArchivo @ID_CLAUSULA, @ID_TIPO_CLAUSULA, @ID_ESTADO, @DESCRIPCION, @ID_EMPRESA, @ID_OCUPACION, 
                @USU_MOD";
                _sqlCommand.Parameters.AddWithValue("@ID_CLAUSULA", idClausula);
                _sqlCommand.Parameters.AddWithValue("@ID_TIPO_CLAUSULA", idTipoClausula);
                _sqlCommand.Parameters.AddWithValue("@ID_ESTADO", idEstado);
                _sqlCommand.Parameters.AddWithValue("@DESCRIPCION", descripcion);
                _sqlCommand.Parameters.AddWithValue("@ID_EMPRESA", idEmpresa);
                _sqlCommand.Parameters.AddWithValue("@ID_OCUPACION", idOcupacion);
                _sqlCommand.Parameters.AddWithValue("@USU_MOD", usuario);
            }

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;

            try
            {
                valor = Convert.ToDecimal(_sqlCommand.ExecuteScalar().ToString());
            }
            catch (NullReferenceException)
            {
                actualizado = false;
                throw new Exception("Error originado en base de datos(método ExecuteScalar): ");
            }
            catch (SqlException e)
            {
                actualizado = false;
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return actualizado;
        }


        public String ExecuteEscalarParaAdicionaDocumentoProveedor(Decimal ID_PROVEEDOR,
            byte[] ARCHIVO,
            String ARCHIVO_EXTENSION,
            Decimal ARCHIVO_TAMANO,
            String ARCHIVO_TYPE,
            String TIPO_DOC,
            String DESCRIPCION,
            String USU_CRE)
        {
            String valor = null;
            String query = @"usp_alm_reg_docs_proveedor_adicionar @ID_PROVEEDOR, @ARCHIVO, @ARCHIVO_EXTENSION, @ARCHIVO_TAMANO, @ARCHIVO_TYPE, @TIPO_DOC, @DESCRIPCION, @USU_CRE";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_PROVEEDOR", ID_PROVEEDOR);

            SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
            archivoParam.Value = ARCHIVO;

            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", ARCHIVO_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", ARCHIVO_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", ARCHIVO_TYPE);

            _sqlCommand.Parameters.AddWithValue("@TIPO_DOC", TIPO_DOC);
            _sqlCommand.Parameters.AddWithValue("@DESCRIPCION", DESCRIPCION);

            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = _sqlCommand.ExecuteScalar().ToString();
            }
            catch (NullReferenceException)
            {
                valor = null;
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }

        public Int32 ExecuteNonQueryParaActualizarDocumentoParaProveedor(Decimal ID_DOCUMENTO_PROV,
            byte[] ARCHIVO,
            String ARCHIVO_EXTENSION,
            Decimal ARCHIVO_TAMANO,
            String ARCHIVO_TYPE,
            String TIPO_DOC,
            String DESCRIPCION,
            String USU_MOD)
        {
            string query = @"usp_alm_reg_docs_proveedor_actualizar @ID_DOCUMENTO_PROV, @ARCHIVO, @ARCHIVO_EXTENSION, @ARCHIVO_TAMANO, @ARCHIVO_TYPE, @TIPO_DOC,  @DESCRIPCION, @USU_MOD";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_DOCUMENTO_PROV", ID_DOCUMENTO_PROV);

            SqlParameter imageParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
            imageParam.Value = ARCHIVO;

            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", ARCHIVO_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", ARCHIVO_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", ARCHIVO_TYPE);
            _sqlCommand.Parameters.AddWithValue("@TIPO_DOC", TIPO_DOC);
            _sqlCommand.Parameters.AddWithValue("@DESCRIPCION", DESCRIPCION);
            _sqlCommand.Parameters.AddWithValue("@USU_MOD", USU_MOD);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;

            _sqlCommand.Transaction = _sqlTransaction;

            Int32 cantidadRegistrosAfectados = 0;

            try
            {
                cantidadRegistrosAfectados = _sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteNonQuery): " + e.Message.ToString());
            }

            return cantidadRegistrosAfectados;
        }


        public String ExecuteEscalarParaAdicionaDocumentoCalificacionProveedor(Decimal ID_CALIFICACION,
             String TIPO,
             byte[] ARCHIVO,
             String ARCHIVO_EXTENSION,
             Decimal ARCHIVO_TAMANO,
             String ARCHIVO_TYPE,
             String USU_CRE)
        {
            String valor = null;
            String query = @"usp_alm_reg_docs_calificacion_prov_adicionar @ID_CALIFICACION, @TIPO, @ARCHIVO, @ARCHIVO_EXTENSION, @ARCHIVO_TAMANO, @ARCHIVO_TYPE, @USU_CRE";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_CALIFICACION", ID_CALIFICACION);
            _sqlCommand.Parameters.AddWithValue("@TIPO", TIPO);

            SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
            archivoParam.Value = ARCHIVO;

            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", ARCHIVO_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", ARCHIVO_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", ARCHIVO_TYPE);

            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = _sqlCommand.ExecuteScalar().ToString();
            }
            catch (NullReferenceException)
            {
                valor = null;
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }


        public Int32 ExecuteNonQueryParaAjustarPresupuestoDetalleActividad(Decimal ID_DETALLE,
            Decimal PRESUPUESTO_APROBADO,
            String ID_ESTADO,
            String USU_MOD,
            Byte[] ARCHIVO,
            String ARCHIVO_EXTENSION,
            Decimal ARCHIVO_TAMANO,
            String ARCHIVO_TYPE)
        {
            string query = @"usp_prog_detalle_actividades_ajustar_presupuesto @ID_DETALLE, @PRESUPUESTO_APROBADO, @ID_ESTADO, @USU_MOD, @ARCHIVO, @ARCHIVO_EXTENSION, @ARCHIVO_TAMANO, @ARCHIVO_TYPE";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_DETALLE", ID_DETALLE);
            _sqlCommand.Parameters.AddWithValue("@PRESUPUESTO_APROBADO", PRESUPUESTO_APROBADO);
            _sqlCommand.Parameters.AddWithValue("@ID_ESTADO", ID_ESTADO);
            _sqlCommand.Parameters.AddWithValue("@USU_MOD", USU_MOD);

            if (ARCHIVO != null)
            {
                SqlParameter imageParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
                imageParam.Value = ARCHIVO;
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO", DBNull.Value);
            }

            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", ARCHIVO_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", ARCHIVO_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", ARCHIVO_TYPE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;

            _sqlCommand.Transaction = _sqlTransaction;

            Int32 cantidadRegistrosAfectados = 0;

            try
            {
                cantidadRegistrosAfectados = _sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteNonQuery): " + e.Message.ToString());
            }

            return cantidadRegistrosAfectados;
        }

        public Int32 ExecuteNonQueryParaReprogramarActividad(Decimal ID_DETALLE,
           DateTime FECHA_ACTIVIDAD,
           String HORA_INICIO,
            String HORA_FIN,
            String MOTIVO_REPROGRAMACION,
            String USU_MOD,
            String TIPO_REPROGRAMACION,
           Byte[] ARCHIVO,
           String ARCHIVO_EXTENSION,
           Decimal ARCHIVO_TAMANO,
           String ARCHIVO_TYPE)
        {
            string query = @"usp_prog_detalle_actividades_reprogramar @ID_DETALLE, @FECHA_ACTIVIDAD, @HORA_INICIO, @HORA_FIN, @MOTIVO_REPROGRAMACION, @USU_MOD, @TIPO_REPROGRAMACION, @ARCHIVO, @ARCHIVO_EXTENSION, @ARCHIVO_TAMANO, @ARCHIVO_TYPE";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_DETALLE", ID_DETALLE);
            _sqlCommand.Parameters.AddWithValue("@FECHA_ACTIVIDAD", FECHA_ACTIVIDAD);
            _sqlCommand.Parameters.AddWithValue("@HORA_INICIO", HORA_INICIO);
            _sqlCommand.Parameters.AddWithValue("@HORA_FIN", HORA_FIN);
            _sqlCommand.Parameters.AddWithValue("@MOTIVO_REPROGRAMACION", MOTIVO_REPROGRAMACION);
            _sqlCommand.Parameters.AddWithValue("@USU_MOD", USU_MOD);
            _sqlCommand.Parameters.AddWithValue("@TIPO_REPROGRAMACION", TIPO_REPROGRAMACION);

            if (ARCHIVO != null)
            {
                SqlParameter imageParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
                imageParam.Value = ARCHIVO;
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO", DBNull.Value);
            }

            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", ARCHIVO_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", ARCHIVO_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", ARCHIVO_TYPE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;

            _sqlCommand.Transaction = _sqlTransaction;

            Int32 cantidadRegistrosAfectados = 0;

            try
            {
                cantidadRegistrosAfectados = _sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteNonQuery): " + e.Message.ToString());
            }

            return cantidadRegistrosAfectados;
        }

        public Int32 ExecuteNonQueryParaCancelacionDeActividad(Decimal ID_DETALLE,
             String ID_ESTADO,
             String MOTIVO_CANCELACION,
             String USU_MOD,
             String TIPO_CANCELACION,
             Byte[] ARCHIVO,
             String ARCHIVO_EXTENSION,
             Decimal ARCHIVO_TAMANO,
             String ARCHIVO_TYPE)
        {
            string query = @"usp_prog_detalle_actividades_cancelar @ID_DETALLE, @ID_ESTADO, @MOTIVO_CANCELACION, @USU_MOD, @TIPO_CANCELACION, @ARCHIVO_CANCELACION, @ARCHIVO_CANCELACION_EXTENSION, @ARCHIVO_CANCELACION_TAMANO, @ARCHIVO_CANCELACION_TYPE";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_DETALLE", ID_DETALLE);
            _sqlCommand.Parameters.AddWithValue("@ID_ESTADO", ID_ESTADO);
            _sqlCommand.Parameters.AddWithValue("@MOTIVO_CANCELACION", MOTIVO_CANCELACION);
            _sqlCommand.Parameters.AddWithValue("@USU_MOD", USU_MOD);
            _sqlCommand.Parameters.AddWithValue("@TIPO_CANCELACION", TIPO_CANCELACION);

            if (ARCHIVO != null)
            {
                SqlParameter imageParam = _sqlCommand.Parameters.Add("@ARCHIVO_CANCELACION", System.Data.SqlDbType.VarBinary);
                imageParam.Value = ARCHIVO;
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@ARCHIVO_CANCELACION", DBNull.Value);
            }

            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_CANCELACION_EXTENSION", ARCHIVO_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_CANCELACION_TAMANO", ARCHIVO_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_CANCELACION_TYPE", ARCHIVO_TYPE);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;

            _sqlCommand.Transaction = _sqlTransaction;

            Int32 cantidadRegistrosAfectados = 0;

            try
            {
                cantidadRegistrosAfectados = _sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                throw new Exception("Error originado en base de datos(método ExecuteNonQuery): " + e.Message.ToString());
            }

            return cantidadRegistrosAfectados;
        }

        public Decimal ExecuteEscalarParaAdicionarDocsAdjuntosAccidentes(Decimal ID_ACCIDENTE,
               String TIPO_ARCHIVO,
               byte[] ARCHIVO,
               String ARCHIVO_EXTENSION,
               Decimal ARCHIVO_TAMANO,
               String ARCHIVO_TYPE,
               String USU_CRE,
               String TITULO,
               String DESCRIPCION)
        {
            Decimal valor = 0;
            String query = @"usp_prog_archivos_adjuntos_investigaciones_adicionar @ID_ACCIDENTE, @TIPO_ARCHIVO, @ARCHIVO, @ARCHIVO_EXTENSION, @ARCHIVO_TAMANO, @ARCHIVO_TYPE, @USU_CRE, @TITULO, @DESCRIPCION";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_ACCIDENTE", ID_ACCIDENTE);
            _sqlCommand.Parameters.AddWithValue("@TIPO_ARCHIVO", TIPO_ARCHIVO);

            SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
            archivoParam.Value = ARCHIVO;

            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_EXTENSION", ARCHIVO_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TAMANO", ARCHIVO_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@ARCHIVO_TYPE", ARCHIVO_TYPE);

            _sqlCommand.Parameters.AddWithValue("@USU_CRE", USU_CRE);

            if (String.IsNullOrEmpty(TITULO) == false)
            {
                _sqlCommand.Parameters.AddWithValue("@TITULO", TITULO);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@TITULO", DBNull.Value);
            }

            if (String.IsNullOrEmpty(TITULO) == false)
            {
                _sqlCommand.Parameters.AddWithValue("@DESCRIPCION", DESCRIPCION);
            }
            else
            {
                _sqlCommand.Parameters.AddWithValue("@DESCRIPCION", DBNull.Value);
            }

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = Convert.ToDecimal(_sqlCommand.ExecuteScalar().ToString());
            }
            catch (NullReferenceException)
            {
                valor = 0;
            }
            catch (SqlException e)
            {
                valor = 0;
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }

        public Decimal ExecuteEscalarParaActualizarClausulasEmpleado(Decimal ID_EMPLEADO,
             Decimal ID_CLAUSULA,
             byte[] ARCHIVO,
             String ARCHIVO_EXTENSION,
             Decimal ARCHIVO_TAMANO,
             String ARCHIVO_TYPE,
             String USU_MOD)
        {
            Decimal valor = 0;
            String query = @"usp_empleado_clausulas_contratar_actualizar @ID_EMPLEADO, @ID_CLAUSULA, @ARCHIVO, @TAMANO, @EXTENSION, @TIPO, @USU_MOD ";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@ID_EMPLEADO", ID_EMPLEADO);
            _sqlCommand.Parameters.AddWithValue("@ID_CLAUSULA", ID_CLAUSULA);
            SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
            archivoParam.Value = ARCHIVO;
            _sqlCommand.Parameters.AddWithValue("@TAMANO", ARCHIVO_TAMANO);
            _sqlCommand.Parameters.AddWithValue("@EXTENSION", ARCHIVO_EXTENSION);
            _sqlCommand.Parameters.AddWithValue("@TIPO", ARCHIVO_TYPE);
            _sqlCommand.Parameters.AddWithValue("@USU_MOD", USU_MOD);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;
            _sqlCommand.Transaction = _sqlTransaction;

            try
            {
                valor = Convert.ToDecimal(_sqlCommand.ExecuteScalar().ToString());
            }
            catch (NullReferenceException)
            {
                valor = 0;
            }
            catch (SqlException e)
            {
                valor = 0;
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }

        public Decimal ExecuteEscalarParaAdicionarIncapacidad(Decimal idEmpleado, DateTime fecha, String tipoIncapacidad, String claseIncapacidad, String prorroga, String severo,
             String observaciones, Decimal valorReconocido, String numeroAutorizacion, DateTime fechaInicialIncapacidad, DateTime fechaFinalIncapacidad,
             String codigoCie10, Int32 diasIncapacidad, String carencia, Decimal idConcepto,
             DateTime transcripcionFechaRadicacion, DateTime transcripcionFechaSeguimiento, string transcripcionNumero, decimal transcripcionValor, string transcripcion_notas, bool transcripcion_VoBo,
             DateTime liquidacionFechaRadicacion, DateTime liquidacionFechaSeguimiento, string liquidacionNumero, decimal liquidacionValor, string liquidacionNotas, bool liquidacionVoBo,
             DateTime reliquidacionFechaRadicacion, DateTime reliquidacionFechaSeguimiento, string reliquidacionNumero, decimal reliquidacionValor, string reliquidacionNotas, bool reliquidacionVoBo,
             DateTime cobroFechaRadicacion, DateTime cobroFechaSeguimiento, string cobroNumero, decimal cobroValor, string cobroNotas, bool cobroVoBo,
             DateTime pagoFechaRadicacion, DateTime pagoFechaSeguimiento, string pagoNumero, decimal pagoValor, string pagoNotas, bool pagoVoBo,
             DateTime objetadaFechaRadicacion, DateTime objetadaFechaSeguimiento, string objetadaNumero, decimal objetadaValor, string objetadaNotas, bool objetadaVoBo,
             DateTime negadaFechaRadicacion, DateTime negadaFechaSeguimiento, string negadaNumero, decimal negadaValor, string negadaNotas, bool negadaVoBo,
             string estado, string estadoTramite, string tramitadaPor, Byte[] archivo, Int32 archivoTamaño, string archivoExtension, string archivoTipo, string usuario,
             DateTime fchIniNom, DateTime fchTerNom)
        {
            Decimal valor = 0;

            String query = @"usp_nom_reg_incapacidades_adicionar @idEmpleado, @fecha, @tipoIncapacidad, @claseIncapacidad, @prorroga, @severo, @observaciones,
                @valorReconocido, @numeroAutorizacion, @fechaInicialIncapacidad, @fechaFinalIncapacidad, @codigoCie10, @diasIncapacidad, @carencia, @idConcepto, 
                @transcripcionFechaRadicacion, @transcripcionFechaSeguimiento, @transcripcionNumero, @transcripcionValor, @transcripcion_notas, @transcripcion_VoBo,
                @liquidacionFechaRadicacion, @liquidacionFechaSeguimiento, @liquidacionNumero, @liquidacionValor, @liquidacionNotas, @liquidacionVoBo, 
                @reliquidacionFechaRadicacion, @reliquidacionFechaSeguimiento, @reliquidacionNumero, @reliquidacionValor, @reliquidacionNotas, @reliquidacionVoBo,
                @cobroFechaRadicacion, @cobroFechaSeguimiento, @cobroNumero, @cobroValor, @cobroNotas, @cobroVoBo, 
                @pagoFechaRadicacion, @pagoFechaSeguimiento, @pagoNumero, @pagoValor, @pagoNotas, @pagoVoBo,
                @objetadaFechaRadicacion, @objetadaFechaSeguimiento, @objetadaNumero, @objetadaValor, @objetadaNotas, @objetadaVoBo,
                @negadaFechaRadicacion, @negadaFechaSeguimiento, @negadaNumero, @negadaValor, @negadaNotas, @negadaVoBo,
                @estado, @estadoTramite, @tramitadaPor, @archivo, @archivoExtension, @archivoTamaño, @archivoTipo, @usuario, 
                @fch_ini_nom, @fch_ter_nom";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@idEmpleado", idEmpleado);
            _sqlCommand.Parameters.AddWithValue("@fecha", fecha);
            _sqlCommand.Parameters.AddWithValue("@tipoIncapacidad", tipoIncapacidad);
            _sqlCommand.Parameters.AddWithValue("@claseIncapacidad", claseIncapacidad);
            _sqlCommand.Parameters.AddWithValue("@prorroga", prorroga);
            _sqlCommand.Parameters.AddWithValue("@severo", severo);
            _sqlCommand.Parameters.AddWithValue("@observaciones", observaciones);
            _sqlCommand.Parameters.AddWithValue("@valorReconocido", valorReconocido);
            _sqlCommand.Parameters.AddWithValue("@numeroAutorizacion", numeroAutorizacion);
            _sqlCommand.Parameters.AddWithValue("@fechaInicialIncapacidad", fechaInicialIncapacidad);
            _sqlCommand.Parameters.AddWithValue("@fechaFinalIncapacidad", fechaFinalIncapacidad);
            _sqlCommand.Parameters.AddWithValue("@codigoCie10", codigoCie10);
            _sqlCommand.Parameters.AddWithValue("@diasIncapacidad", diasIncapacidad);
            _sqlCommand.Parameters.AddWithValue("@carencia", carencia);
            _sqlCommand.Parameters.AddWithValue("@idConcepto", idConcepto);

            if (transcripcionFechaRadicacion == new DateTime()) _sqlCommand.Parameters.AddWithValue("@transcripcionFechaRadicacion", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@transcripcionFechaRadicacion", transcripcionFechaRadicacion);
            if (transcripcionFechaSeguimiento == new DateTime()) _sqlCommand.Parameters.AddWithValue("@transcripcionFechaSeguimiento", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@transcripcionFechaSeguimiento", transcripcionFechaSeguimiento);
            _sqlCommand.Parameters.AddWithValue("@transcripcionNumero", transcripcionNumero);
            _sqlCommand.Parameters.AddWithValue("@transcripcionValor", transcripcionValor);
            _sqlCommand.Parameters.AddWithValue("@transcripcion_notas", transcripcion_notas);
            if (transcripcion_VoBo) _sqlCommand.Parameters.AddWithValue("@transcripcion_VoBo", "S");
            else _sqlCommand.Parameters.AddWithValue("@transcripcion_VoBo", "N");

            if (liquidacionFechaRadicacion == new DateTime()) _sqlCommand.Parameters.AddWithValue("@liquidacionFechaRadicacion", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@liquidacionFechaRadicacion", liquidacionFechaRadicacion.ToShortDateString());
            if (liquidacionFechaSeguimiento == new DateTime()) _sqlCommand.Parameters.AddWithValue("@liquidacionFechaSeguimiento", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@liquidacionFechaSeguimiento", liquidacionFechaSeguimiento.ToShortDateString());
            _sqlCommand.Parameters.AddWithValue("@liquidacionNumero", liquidacionNumero);
            _sqlCommand.Parameters.AddWithValue("@liquidacionValor", liquidacionValor);
            _sqlCommand.Parameters.AddWithValue("@liquidacionNotas", liquidacionNotas);
            if (liquidacionVoBo) _sqlCommand.Parameters.AddWithValue("@liquidacionVoBo", "S");
            else _sqlCommand.Parameters.AddWithValue("@liquidacionVoBo", "N");

            if (reliquidacionFechaRadicacion == new DateTime()) _sqlCommand.Parameters.AddWithValue("@reliquidacionFechaRadicacion", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@reliquidacionFechaRadicacion", reliquidacionFechaRadicacion.ToShortDateString());
            if (reliquidacionFechaSeguimiento == new DateTime()) _sqlCommand.Parameters.AddWithValue("@reliquidacionFechaSeguimiento", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@reliquidacionFechaSeguimiento", reliquidacionFechaSeguimiento.ToShortDateString());
            _sqlCommand.Parameters.AddWithValue("@reliquidacionNumero", reliquidacionNumero);
            _sqlCommand.Parameters.AddWithValue("@reliquidacionValor", reliquidacionValor);
            _sqlCommand.Parameters.AddWithValue("@reliquidacionNotas", reliquidacionNotas);
            if (reliquidacionVoBo) _sqlCommand.Parameters.AddWithValue("@reliquidacionVoBo", "S");
            else _sqlCommand.Parameters.AddWithValue("@reliquidacionVoBo", "N");

            if (cobroFechaRadicacion == new DateTime()) _sqlCommand.Parameters.AddWithValue("@cobroFechaRadicacion", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@cobroFechaRadicacion", cobroFechaRadicacion.ToShortDateString());
            if (cobroFechaSeguimiento == new DateTime()) _sqlCommand.Parameters.AddWithValue("@cobroFechaSeguimiento", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@cobroFechaSeguimiento", cobroFechaSeguimiento.ToShortDateString());
            _sqlCommand.Parameters.AddWithValue("@cobroNumero", cobroNumero);
            _sqlCommand.Parameters.AddWithValue("@cobroValor", cobroValor);
            _sqlCommand.Parameters.AddWithValue("@cobroNotas", cobroNotas);
            if (cobroVoBo) _sqlCommand.Parameters.AddWithValue("@cobroVoBo", "S");
            else _sqlCommand.Parameters.AddWithValue("@cobroVoBo", "N");

            if (pagoFechaRadicacion == new DateTime()) _sqlCommand.Parameters.AddWithValue("@pagoFechaRadicacion", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@pagoFechaRadicacion", pagoFechaRadicacion.ToShortDateString());
            if (pagoFechaSeguimiento == new DateTime()) _sqlCommand.Parameters.AddWithValue("@pagoFechaSeguimiento", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@pagoFechaSeguimiento", pagoFechaSeguimiento.ToShortDateString());
            _sqlCommand.Parameters.AddWithValue("@pagoNumero", pagoNumero);
            _sqlCommand.Parameters.AddWithValue("@pagoValor", pagoValor);
            _sqlCommand.Parameters.AddWithValue("@pagoNotas", pagoNotas);
            if (pagoVoBo) _sqlCommand.Parameters.AddWithValue("@pagoVoBo", "S");
            else _sqlCommand.Parameters.AddWithValue("@pagoVoBo", "N");

            if (objetadaFechaRadicacion == new DateTime()) _sqlCommand.Parameters.AddWithValue("@objetadaFechaRadicacion", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@objetadaFechaRadicacion", objetadaFechaRadicacion.ToShortDateString());
            if (objetadaFechaSeguimiento == new DateTime()) _sqlCommand.Parameters.AddWithValue("@objetadaFechaSeguimiento", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@objetadaFechaSeguimiento", objetadaFechaSeguimiento.ToShortDateString());
            _sqlCommand.Parameters.AddWithValue("@objetadaNumero", objetadaNumero);
            _sqlCommand.Parameters.AddWithValue("@objetadaValor", objetadaValor);
            _sqlCommand.Parameters.AddWithValue("@objetadaNotas", objetadaNotas);
            if (objetadaVoBo) _sqlCommand.Parameters.AddWithValue("@objetadaVoBo", "S");
            else _sqlCommand.Parameters.AddWithValue("@objetadaVoBo", "N");

            if (negadaFechaRadicacion == new DateTime()) _sqlCommand.Parameters.AddWithValue("@negadaFechaRadicacion", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@negadaFechaRadicacion", negadaFechaRadicacion.ToShortDateString());
            if (negadaFechaSeguimiento == new DateTime()) _sqlCommand.Parameters.AddWithValue("@negadaFechaSeguimiento", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@negadaFechaSeguimiento", negadaFechaSeguimiento.ToShortDateString());
            _sqlCommand.Parameters.AddWithValue("@negadaNumero", negadaNumero);
            _sqlCommand.Parameters.AddWithValue("@negadaValor", negadaValor);
            _sqlCommand.Parameters.AddWithValue("@negadaNotas", negadaNotas);
            if (negadaVoBo) _sqlCommand.Parameters.AddWithValue("@negadaVoBo", "S");
            else _sqlCommand.Parameters.AddWithValue("@negadaVoBo", "N");


            _sqlCommand.Parameters.AddWithValue("@estado", estado);
            _sqlCommand.Parameters.AddWithValue("@estadoTramite", estadoTramite);
            _sqlCommand.Parameters.AddWithValue("@tramitadaPor", tramitadaPor);
            SqlParameter archivoParam = _sqlCommand.Parameters.Add("@archivo", System.Data.SqlDbType.VarBinary);
            archivoParam.Value = archivo;
            _sqlCommand.Parameters.AddWithValue("@archivoExtension", archivoExtension);
            _sqlCommand.Parameters.AddWithValue("@archivoTamaño", archivoTamaño);
            _sqlCommand.Parameters.AddWithValue("@archivoTipo", archivoTipo);
            _sqlCommand.Parameters.AddWithValue("@usuario", usuario);
            _sqlCommand.Parameters.AddWithValue("@fch_ini_nom", fchIniNom);
            _sqlCommand.Parameters.AddWithValue("@fch_ter_nom", fchTerNom);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;

            try
            {
                valor = Convert.ToDecimal(_sqlCommand.ExecuteScalar().ToString());
            }
            catch (NullReferenceException)
            {
                valor = 0;
            }
            catch (SqlException e)
            {
                valor = 0;
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }


        public Decimal ExecuteEscalarParaActualizarIncapacidad(Decimal idIncapacidad,
            DateTime fecha,
            String tipoIncapacidad,
            String claseIncapacidad,
            String prorroga,
            String severo,
             String observaciones,
            Decimal valorReconocido,
            String numeroAutorizacion,
            DateTime fechaInicialIncapacidad,
            DateTime fechaFinalIncapacidad,
             String codigoCie10,
            Int32 diasIncapacidad,
            String carencia,
            Decimal idConcepto,
             DateTime transcripcionFechaRadicacion,
            DateTime transcripcionFechaSeguimiento,
            string transcripcionNumero,
            decimal transcripcionValor,
            string transcripcion_notas,
            bool transcripcion_VoBo,
             DateTime liquidacionFechaRadicacion,
            DateTime liquidacionFechaSeguimiento,
            string liquidacionNumero,
            decimal liquidacionValor,
            string liquidacionNotas,
            bool liquidacionVoBo,
             DateTime reliquidacionFechaRadicacion,
            DateTime reliquidacionFechaSeguimiento,
            string reliquidacionNumero,
            decimal reliquidacionValor,
            string reliquidacionNotas,
            bool reliquidacionVoBo,
             DateTime cobroFechaRadicacion,
            DateTime cobroFechaSeguimiento,
            string cobroNumero,
            decimal cobroValor,
            string cobroNotas,
            bool cobroVoBo,
             DateTime pagoFechaRadicacion,
            DateTime pagoFechaSeguimiento,
            string pagoNumero,
            decimal pagoValor,
            string pagoNotas,
            bool pagoVoBo,
             DateTime objetadaFechaRadicacion,
            DateTime objetadaFechaSeguimiento,
            string objetadaNumero,
            decimal objetadaValor,
            string objetadaNotas,
            bool objetadaVoBo,
             DateTime negadaFechaRadicacion,
            DateTime negadaFechaSeguimiento,
            string negadaNumero,
            decimal negadaValor,
            string negadaNotas,
            bool negadaVoBo,
             string estado,
            string estadoTramite,
            string tramitadaPor,
            Byte[] archivo,
            Int32 archivoTamaño,
            string archivoExtension,
            string archivoTipo,
            string usuario,
             DateTime fchIniNom,
            DateTime fchTerNom
            )
        {
            Decimal valor = 0;

            String query = @"usp_nom_reg_incapacidades_actualizar @idIncapacidad, @fecha, @tipoIncapacidad, @claseIncapacidad, @prorroga, @severo, @observaciones,
                @valorReconocido, @numeroAutorizacion, @fechaInicialIncapacidad, @fechaFinalIncapacidad, @codigoCie10, @diasIncapacidad, @carencia, @idConcepto, 
                @transcripcionFechaRadicacion, @transcripcionFechaSeguimiento, @transcripcionNumero, @transcripcionValor, @transcripcion_notas, @transcripcion_VoBo,
                @liquidacionFechaRadicacion, @liquidacionFechaSeguimiento, @liquidacionNumero, @liquidacionValor, @liquidacionNotas, @liquidacionVoBo, 
                @reliquidacionFechaRadicacion, @reliquidacionFechaSeguimiento, @reliquidacionNumero, @reliquidacionValor, @reliquidacionNotas, @reliquidacionVoBo,
                @cobroFechaRadicacion, @cobroFechaSeguimiento, @cobroNumero, @cobroValor, @cobroNotas, @cobroVoBo, 
                @pagoFechaRadicacion, @pagoFechaSeguimiento, @pagoNumero, @pagoValor, @pagoNotas, @pagoVoBo,
                @objetadaFechaRadicacion, @objetadaFechaSeguimiento, @objetadaNumero, @objetadaValor, @objetadaNotas, @objetadaVoBo,
                @negadaFechaRadicacion, @negadaFechaSeguimiento, @negadaNumero, @negadaValor, @negadaNotas, @negadaVoBo,
                @estado, @estadoTramite, @tramitadaPor, @archivo, @archivoExtension, @archivoTamaño, @archivoTipo, @usuario, @fch_ini_nom, @fch_ter_nom";

            _sqlCommand = new SqlCommand();

            _sqlCommand.Parameters.AddWithValue("@idIncapacidad", idIncapacidad);
            _sqlCommand.Parameters.AddWithValue("@fecha", fecha);
            _sqlCommand.Parameters.AddWithValue("@tipoIncapacidad", tipoIncapacidad);
            _sqlCommand.Parameters.AddWithValue("@claseIncapacidad", claseIncapacidad);
            _sqlCommand.Parameters.AddWithValue("@prorroga", prorroga);
            _sqlCommand.Parameters.AddWithValue("@severo", severo);
            _sqlCommand.Parameters.AddWithValue("@observaciones", observaciones);
            _sqlCommand.Parameters.AddWithValue("@valorReconocido", valorReconocido);
            _sqlCommand.Parameters.AddWithValue("@numeroAutorizacion", numeroAutorizacion);
            _sqlCommand.Parameters.AddWithValue("@fechaInicialIncapacidad", fechaInicialIncapacidad);
            _sqlCommand.Parameters.AddWithValue("@fechaFinalIncapacidad", fechaFinalIncapacidad);
            _sqlCommand.Parameters.AddWithValue("@codigoCie10", codigoCie10);
            _sqlCommand.Parameters.AddWithValue("@diasIncapacidad", diasIncapacidad);
            _sqlCommand.Parameters.AddWithValue("@carencia", carencia);
            _sqlCommand.Parameters.AddWithValue("@idConcepto", idConcepto);

            if (transcripcionFechaRadicacion == new DateTime()) _sqlCommand.Parameters.AddWithValue("@transcripcionFechaRadicacion", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@transcripcionFechaRadicacion", transcripcionFechaRadicacion.ToShortDateString());
            if (transcripcionFechaSeguimiento == new DateTime()) _sqlCommand.Parameters.AddWithValue("@transcripcionFechaSeguimiento", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@transcripcionFechaSeguimiento", transcripcionFechaSeguimiento.ToShortDateString());
            _sqlCommand.Parameters.AddWithValue("@transcripcionNumero", transcripcionNumero);
            _sqlCommand.Parameters.AddWithValue("@transcripcionValor", transcripcionValor);
            _sqlCommand.Parameters.AddWithValue("@transcripcion_notas", transcripcion_notas);
            if (transcripcion_VoBo) _sqlCommand.Parameters.AddWithValue("@transcripcion_VoBo", "S");
            else _sqlCommand.Parameters.AddWithValue("@transcripcion_VoBo", "N");

            if (liquidacionFechaRadicacion == new DateTime()) _sqlCommand.Parameters.AddWithValue("@liquidacionFechaRadicacion", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@liquidacionFechaRadicacion", liquidacionFechaRadicacion.ToShortDateString());
            if (liquidacionFechaSeguimiento == new DateTime()) _sqlCommand.Parameters.AddWithValue("@liquidacionFechaSeguimiento", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@liquidacionFechaSeguimiento", liquidacionFechaSeguimiento.ToShortDateString());
            _sqlCommand.Parameters.AddWithValue("@liquidacionNumero", liquidacionNumero);
            _sqlCommand.Parameters.AddWithValue("@liquidacionValor", liquidacionValor);
            _sqlCommand.Parameters.AddWithValue("@liquidacionNotas", liquidacionNotas);
            if (liquidacionVoBo) _sqlCommand.Parameters.AddWithValue("@liquidacionVoBo", "S");
            else _sqlCommand.Parameters.AddWithValue("@liquidacionVoBo", "N");


            if (reliquidacionFechaRadicacion == new DateTime()) _sqlCommand.Parameters.AddWithValue("@reliquidacionFechaRadicacion", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@reliquidacionFechaRadicacion", reliquidacionFechaRadicacion.ToShortDateString());
            if (reliquidacionFechaSeguimiento == new DateTime()) _sqlCommand.Parameters.AddWithValue("@reliquidacionFechaSeguimiento", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@reliquidacionFechaSeguimiento", reliquidacionFechaSeguimiento.ToShortDateString());
            _sqlCommand.Parameters.AddWithValue("@reliquidacionNumero", reliquidacionNumero);
            _sqlCommand.Parameters.AddWithValue("@reliquidacionValor", reliquidacionValor);
            _sqlCommand.Parameters.AddWithValue("@reliquidacionNotas", reliquidacionNotas);
            if (reliquidacionVoBo) _sqlCommand.Parameters.AddWithValue("@reliquidacionVoBo", "S");
            else _sqlCommand.Parameters.AddWithValue("@reliquidacionVoBo", "N");

            if (cobroFechaRadicacion == new DateTime()) _sqlCommand.Parameters.AddWithValue("@cobroFechaRadicacion", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@cobroFechaRadicacion", cobroFechaRadicacion.ToShortDateString());
            if (cobroFechaSeguimiento == new DateTime()) _sqlCommand.Parameters.AddWithValue("@cobroFechaSeguimiento", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@cobroFechaSeguimiento", cobroFechaSeguimiento.ToShortDateString());
            _sqlCommand.Parameters.AddWithValue("@cobroNumero", cobroNumero);
            _sqlCommand.Parameters.AddWithValue("@cobroValor", cobroValor);
            _sqlCommand.Parameters.AddWithValue("@cobroNotas", cobroNotas);
            if (cobroVoBo) _sqlCommand.Parameters.AddWithValue("@cobroVoBo", "S");
            else _sqlCommand.Parameters.AddWithValue("@cobroVoBo", "N");

            if (pagoFechaRadicacion == new DateTime()) _sqlCommand.Parameters.AddWithValue("@pagoFechaRadicacion", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@pagoFechaRadicacion", pagoFechaRadicacion.ToShortDateString());
            if (pagoFechaSeguimiento == new DateTime()) _sqlCommand.Parameters.AddWithValue("@pagoFechaSeguimiento", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@pagoFechaSeguimiento", pagoFechaSeguimiento.ToShortDateString());
            _sqlCommand.Parameters.AddWithValue("@pagoNumero", pagoNumero);
            _sqlCommand.Parameters.AddWithValue("@pagoValor", pagoValor);
            _sqlCommand.Parameters.AddWithValue("@pagoNotas", pagoNotas);
            if (pagoVoBo) _sqlCommand.Parameters.AddWithValue("@pagoVoBo", "S");
            else _sqlCommand.Parameters.AddWithValue("@pagoVoBo", "N");

            if (objetadaFechaRadicacion == new DateTime()) _sqlCommand.Parameters.AddWithValue("@objetadaFechaRadicacion", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@objetadaFechaRadicacion", objetadaFechaRadicacion.ToShortDateString());
            if (objetadaFechaSeguimiento == new DateTime()) _sqlCommand.Parameters.AddWithValue("@objetadaFechaSeguimiento", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@objetadaFechaSeguimiento", objetadaFechaSeguimiento.ToShortDateString());
            _sqlCommand.Parameters.AddWithValue("@objetadaNumero", objetadaNumero);
            _sqlCommand.Parameters.AddWithValue("@objetadaValor", objetadaValor);
            _sqlCommand.Parameters.AddWithValue("@objetadaNotas", objetadaNotas);
            if (objetadaVoBo) _sqlCommand.Parameters.AddWithValue("@objetadaVoBo", "S");
            else _sqlCommand.Parameters.AddWithValue("@objetadaVoBo", "N");

            if (negadaFechaRadicacion == new DateTime()) _sqlCommand.Parameters.AddWithValue("@negadaFechaRadicacion", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@negadaFechaRadicacion", negadaFechaRadicacion.ToShortDateString());
            if (negadaFechaSeguimiento == new DateTime()) _sqlCommand.Parameters.AddWithValue("@negadaFechaSeguimiento", DBNull.Value);
            else _sqlCommand.Parameters.AddWithValue("@negadaFechaSeguimiento", negadaFechaSeguimiento.ToShortDateString());
            _sqlCommand.Parameters.AddWithValue("@negadaNumero", negadaNumero);
            _sqlCommand.Parameters.AddWithValue("@negadaValor", negadaValor);
            _sqlCommand.Parameters.AddWithValue("@negadaNotas", negadaNotas);
            if (negadaVoBo) _sqlCommand.Parameters.AddWithValue("@negadaVoBo", "S");
            else _sqlCommand.Parameters.AddWithValue("@negadaVoBo", "N");

            _sqlCommand.Parameters.AddWithValue("@estado", estado);
            _sqlCommand.Parameters.AddWithValue("@estadoTramite", estadoTramite);
            _sqlCommand.Parameters.AddWithValue("@tramitadaPor", tramitadaPor);

            SqlParameter archivoParam;
            if (archivo != null)
            {
                archivoParam = _sqlCommand.Parameters.Add("@archivo", System.Data.SqlDbType.VarBinary);
                archivoParam.Value = archivo;
                _sqlCommand.Parameters.AddWithValue("@archivoExtension", archivoExtension);
                _sqlCommand.Parameters.AddWithValue("@archivoTamaño", archivoTamaño);
                _sqlCommand.Parameters.AddWithValue("@archivoTipo", archivoTipo);
            }
            else
            {
                archivoParam = _sqlCommand.Parameters.Add("@archivo", System.Data.SqlDbType.VarBinary);
                archivoParam.Value = DBNull.Value;
                _sqlCommand.Parameters.AddWithValue("@archivoExtension", DBNull.Value);
                _sqlCommand.Parameters.AddWithValue("@archivoTamaño", DBNull.Value);
                _sqlCommand.Parameters.AddWithValue("@archivoTipo", DBNull.Value);
            }
            _sqlCommand.Parameters.AddWithValue("@usuario", usuario);
            _sqlCommand.Parameters.AddWithValue("@fch_ini_nom", fchIniNom);
            _sqlCommand.Parameters.AddWithValue("@fch_ter_nom", fchTerNom);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;

            try
            {
                valor = Convert.ToDecimal(_sqlCommand.ExecuteScalar().ToString());
            }
            catch (NullReferenceException)
            {
                valor = 0;
            }
            catch (SqlException e)
            {
                valor = 0;
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }
        public Decimal ExecuteEscalarParaAdicionarImagenes(
            string Operacion,
            string id,
            string NOMBRE,
            Byte[] ARCHIVO,
            string archivoExtension,
            Int32 archivoTamaño,
            string archivoTipo)
        {
            Decimal valor = 0;

            String query = @"USP_SALVAR_PAR_IMAGENES @OPERACION, @ID, @NOMBRE, @ARCHIVO,@EXTENCION,@TAMAÑO,@TIPO";

            _sqlCommand = new SqlCommand();
            _sqlCommand.Parameters.AddWithValue("@OPERACION", Operacion);
            _sqlCommand.Parameters.AddWithValue("@ID", id);
            _sqlCommand.Parameters.AddWithValue("@NOMBRE", NOMBRE);
            SqlParameter archivoParam = _sqlCommand.Parameters.Add("@ARCHIVO", System.Data.SqlDbType.VarBinary);
            archivoParam.Value = ARCHIVO;
            _sqlCommand.Parameters.AddWithValue("@EXTENCION", archivoExtension);
            _sqlCommand.Parameters.AddWithValue("@TAMAÑO", archivoTamaño);
            _sqlCommand.Parameters.AddWithValue("@TIPO", archivoTamaño);

            _sqlCommand.Connection = _sqlConnection;
            _sqlCommand.CommandText = query;

            try
            {
                valor = Convert.ToDecimal(_sqlCommand.ExecuteScalar().ToString());
            }
            catch (NullReferenceException)
            {
                valor = 0;
            }
            catch (SqlException e)
            {
                valor = 0;
                throw new Exception("Error originado en base de datos(método ExecuteScalar): " + e.Message.ToString());
            }

            return valor;
        }

        #endregion metodos
    }
}