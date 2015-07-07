using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;

namespace Brainsbits.LLB.contratacion
{
    public class incapacidad
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
        public incapacidad(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion

        #region metodos

        public decimal Adicionar(Decimal idEmpleado,
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
            DateTime fchIniNom,
            DateTime fchTerNom
            )
        {
            Conexion conexion = new Conexion(Empresa);
            decimal id_incapacidad = 0;
            try
            {
                id_incapacidad = conexion.ExecuteEscalarParaAdicionarIncapacidad(idEmpleado, fecha, tipoIncapacidad, claseIncapacidad, prorroga, severo, observaciones,
                valorReconocido, numeroAutorizacion, fechaInicialIncapacidad, fechaFinalIncapacidad, codigoCie10, diasIncapacidad, carencia, idConcepto,
                transcripcionFechaRadicacion, transcripcionFechaSeguimiento, transcripcionNumero, transcripcionValor, transcripcion_notas, transcripcion_VoBo,
                liquidacionFechaRadicacion, liquidacionFechaSeguimiento, liquidacionNumero, liquidacionValor, liquidacionNotas, liquidacionVoBo,
                reliquidacionFechaRadicacion, reliquidacionFechaSeguimiento, reliquidacionNumero, reliquidacionValor, reliquidacionNotas, reliquidacionVoBo,
                cobroFechaRadicacion, cobroFechaSeguimiento, cobroNumero, cobroValor, cobroNotas, cobroVoBo,
                pagoFechaRadicacion, pagoFechaSeguimiento, pagoNumero, pagoValor, pagoNotas, pagoVoBo,
                objetadaFechaRadicacion, objetadaFechaSeguimiento, objetadaNumero, objetadaValor, objetadaNotas, objetadaVoBo,
                negadaFechaRadicacion, negadaFechaSeguimiento, negadaNumero, negadaValor, negadaNotas, negadaVoBo,
                estado, estadoTramite, tramitadaPor, archivo, archivoTamaño, archivoExtension, archivoTipo, Usuario,
                fchIniNom, fchTerNom);
            }
            catch (Exception e)
            {
                throw new Exception("Error al adicionar incapacidad." + e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }

            return id_incapacidad;

        }

        public bool Actualizar(Decimal idIncapacidad,
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
            DateTime fchIniNom,
            DateTime fchTerNom)
        {
            bool actualizado = true;

            Conexion conexion = new Conexion(Empresa);
            try
            {
                conexion.ExecuteEscalarParaActualizarIncapacidad(idIncapacidad,
                    fecha,
                    tipoIncapacidad,
                    claseIncapacidad,
                    prorroga,
                    severo,
                    observaciones,
                    valorReconocido,
                    numeroAutorizacion,
                    fechaInicialIncapacidad,
                    fechaFinalIncapacidad,
                    codigoCie10,
                    diasIncapacidad,
                    carencia,
                    idConcepto,
                    transcripcionFechaRadicacion,
                    transcripcionFechaSeguimiento,
                    transcripcionNumero,
                    transcripcionValor,
                    transcripcion_notas,
                    transcripcion_VoBo,
                    liquidacionFechaRadicacion,
                    liquidacionFechaSeguimiento,
                    liquidacionNumero,
                    liquidacionValor,
                    liquidacionNotas,
                    liquidacionVoBo,
                    reliquidacionFechaRadicacion,
                    reliquidacionFechaSeguimiento,
                    reliquidacionNumero,
                    reliquidacionValor,
                    reliquidacionNotas,
                    reliquidacionVoBo,
                    cobroFechaRadicacion,
                    cobroFechaSeguimiento,
                    cobroNumero,
                    cobroValor,
                    cobroNotas,
                    cobroVoBo,
                    pagoFechaRadicacion,
                    pagoFechaSeguimiento,
                    pagoNumero,
                    pagoValor,
                    pagoNotas,
                    pagoVoBo,
                    objetadaFechaRadicacion,
                    objetadaFechaSeguimiento,
                    objetadaNumero,
                    objetadaValor,
                    objetadaNotas,
                    objetadaVoBo,
                    negadaFechaRadicacion,
                    negadaFechaSeguimiento,
                    negadaNumero,
                    negadaValor,
                    negadaNotas,
                    negadaVoBo,
                    estado,
                    estadoTramite,
                    tramitadaPor,
                    archivo,
                    archivoTamaño,
                    archivoExtension,
                    archivoTipo,
                    Usuario,
                    fchIniNom,
                    fchTerNom);

            }
            catch (Exception e)
            {
                actualizado = false;
                throw new Exception("Error al actualizar incapacidad." + e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return actualizado;
        }

        public DataTable ObtenerPorIdContrato(Decimal ID_CONTRATO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_nom_reg_incapacidades_ObtenerPorIdContrato ";

            if (ID_CONTRATO != 0)
            {
                sql += ID_CONTRATO;
                informacion += "ID_CONTRATO = " + ID_CONTRATO;
            }
            else
            {
                MensajeError += "El campo ID_CONTRATO no puede ser nulo. \n";
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
                    _auditoria.Adicionar(Usuario, tabla.NOM_REG_INCAPACIDADES, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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

        public DataTable ObtenerPorIdIncapacidad(Decimal ID_INCAPACIDAD)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;
            String informacion = null;
            Boolean ejecutar = true;

            sql = "usp_nom_reg_incapacidades_ObtenerPorIdIncapacidad ";

            if (ID_INCAPACIDAD != 0)
            {
                sql += ID_INCAPACIDAD;
                informacion += "ID_INCAPACIDAD = " + ID_INCAPACIDAD;
            }
            else
            {
                MensajeError += "El campo ID_INCAPACIDAD no puede ser nulo. \n";
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
                    _auditoria.Adicionar(Usuario, tabla.NOM_REG_INCAPACIDADES, tabla.ACCION_CONSULTAR.ToString(), sql, informacion, conexion);
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

        public DataTable ObtenerPagosDeNominaPorIdEmpleado(Decimal ID_EMPLEADO)
        {
            Conexion conexion = new Conexion(Empresa);
            DataSet _dataSet = new DataSet();
            DataView _dataView = new DataView();
            DataTable _dataTable = new DataTable();
            String sql = null;

            sql = "usp_nom_reg_incapacidades_ObtenerPagosDeNominaPorIdEmpleado ";
            sql += ID_EMPLEADO;

            try
            {
                _dataSet = conexion.ExecuteReader(sql);
                _dataView = _dataSet.Tables[0].DefaultView;
                _dataTable = _dataView.Table;
            }
            catch (Exception e)
            {
                throw new Exception("Error al consultar los pagos de nomina para la incapacidad. " + e.Message);
            }
            finally
            {
                conexion.Desconectar();
            }
            return _dataTable;
        }
        #endregion metodos
    }
}