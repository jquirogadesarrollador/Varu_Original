using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.IO;

using Brainsbits.LDA;
using Brainsbits.LLB.seguridad;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seleccion;
using Brainsbits.LLB.contratacion;
using Brainsbits.LLB.comercial;

namespace Brainsbits.LLB.nomina
{
    public class ValidacionNominaMasivamente
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
        public ValidacionNominaMasivamente(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }

        public ValidacionNominaMasivamente()
        {

        }
        #endregion constructores


        public Int32 ValidaMasivamenteUnaEmpresaPeriodoNomLiquidado(Decimal idEmpresa, Conexion conexion)
        {
            Boolean ejecutar = true;
            String sql = null;
            String informacion = null;

            Int32 resultado = 0;

            sql = "usp_validacion_masiva_periodos_liquidados_nom ";

            #region validaciones
            sql += idEmpresa + ", ";
            informacion = "ID_EMPRESA ='" + idEmpresa + "', ";

            sql += "'" + Usuario + "'";
            informacion += "USU_VALIDA = '" + Usuario + "'";

            #endregion

            if (ejecutar == true)
            {
                try
                {
                    resultado = Convert.ToInt32(conexion.ExecuteScalar(sql));

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.NOM_PERIODO, tabla.ACCION_VALIDAR, sql, informacion, conexion);
                    #endregion auditoria

                }
                catch (Exception e)
                {
                    MensajeError = e.Message;
                    resultado = 0;
                }
            }

            return resultado;
        }


        public Boolean ValidarMasivamentePeriodosNomLiquidados(List<Decimal> listaEmpresas)
        {
            Conexion conexion = new Conexion(Empresa);
            conexion.IniciarTransaccion();

            Boolean correcto = true;

            Int32 resualtdoValidacioIndividual = 0;

            Int32 bien = 0;
            Int32 bloqueos = 0;
            Int32 reliquidaciones = 0;
            Int32 bloueosReliquidaciones = 0;

            try
            {
                foreach (Decimal e in listaEmpresas)
                {
                    resualtdoValidacioIndividual = ValidaMasivamenteUnaEmpresaPeriodoNomLiquidado(e, conexion);
                    if (resualtdoValidacioIndividual == 0)
                    {
                        conexion.DeshacerTransaccion();
                        correcto = false;
                        break;
                    }
                    else
                    {
                        if (resualtdoValidacioIndividual == 1)
                        {
                            bien += 1;
                        }

                        if (resualtdoValidacioIndividual == 2)
                        {
                            bloqueos += 1;
                        }

                        if (resualtdoValidacioIndividual == 3)
                        {
                            reliquidaciones += 1;
                        }

                        if (resualtdoValidacioIndividual == 4)
                        {
                            bloueosReliquidaciones += 1;
                        }
                    }
                }

                if (correcto == true)
                {
                    conexion.AceptarTransaccion();

                    MensajeError = bien.ToString() + " Empresas vaidadas correctamente.<br>" + (bloqueos + reliquidaciones + bloueosReliquidaciones).ToString() + " Empresas que no puedieron ser validadas por periodos BLOQUEADOS o empleados marcados para RELIQUIDAR.";
                }
            }
            catch (Exception ex)
            {
                MensajeError = ex.Message;
                correcto = false;
                conexion.DeshacerTransaccion();
            }
            finally
            {
                conexion.Desconectar();
            }

            return correcto;
        }
    }
}
