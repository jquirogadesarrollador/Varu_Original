using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;

using Brainsbits.LDA;
using Brainsbits.LLB.maestras;
using Brainsbits.LLB.seguridad;

namespace Brainsbits.LLB.almacen
{
    public class EquipoEntradaTemp
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        private enum TiposDeDocumento
        {
            ENTREGA = 0,
            FACTURA
        }

        private enum TiposMovimientos
        {
            ENTRADA,
            SALIDA
        }

        private enum EstadosDocumento
        {
            COMPLETO = 0
        }

        private enum EstadosAsignacionSC
        {
            ABIERTA = 0,
            TERMINADA,
            CANCELADA
        }

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
        public EquipoEntradaTemp(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }

        public EquipoEntradaTemp()
        {
        }
        #endregion constructores

        #region metodos
        public Decimal AdicionarAlmEquipoEntradaTemp(Decimal ID_DOCUMENTO,
            String MARCA,
            String MODELO,
            String SERIE,
            String IMEI,
            Decimal NUMERO_CELULAR,
            DateTime FECHA,
            Decimal ID_LOTE,
            Conexion conexion)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "USP_ALM_EQUIPOS_ENTRADA_TEMP_ADICIONAR ";

            #region validaciones
            if (ID_DOCUMENTO != 0)
            {
                sql += ID_DOCUMENTO + ", ";
                informacion += "ID_DOCUMENTO= '" + ID_DOCUMENTO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo ID_DOCUMENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(MARCA)))
            {
                sql += "'" + MARCA + "', ";
                informacion += "MARCA = '" + MARCA.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo MARCA no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(MODELO)))
            {
                sql += "'" + MODELO + "', ";
                informacion += "MODELO= '" + MODELO.ToString() + "', ";
            }
            else
            {
                MensajeError += "El campo MODELO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(SERIE)))
            {
                sql += "'" + SERIE + "', ";
                informacion += "SERIE= '" + SERIE.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "SERIE= 'null', ";
            }

            if (!(String.IsNullOrEmpty(IMEI)))
            {
                sql += "'" + IMEI + "', ";
                informacion += "IMEI= '" + IMEI.ToString() + "', ";
            }
            else
            {
                sql += "null, ";
                informacion += "IMEI= 'null', ";
            }

            if (NUMERO_CELULAR != 0)
            {
                sql += NUMERO_CELULAR + ", ";
                informacion += "NUMERO_CELULAR = '" + NUMERO_CELULAR.ToString() + "', ";
            }
            else
            {
                sql += "0, ";
                informacion += "NUMERO_CELULAR = '0', ";
            }

            sql += "'" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA) + "', ";
            informacion += "FECHA_REGISTRO = '" + _tools.obtenerStringConFormatoFechaSQLServer(FECHA) + "', ";

            if (ID_LOTE != 0)
            {
                sql += ID_LOTE + ", ";
                informacion += "ID_LOTE = '" + ID_LOTE + "', ";
            }
            else
            {
                MensajeError += "El campo ID_LOTE no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "'";
            informacion += "USU_CRE= '" + Usuario.ToString() + "'";

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ALM_EQUIPOS_ENTRADA_TEMP, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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
        #endregion metodos
    }
}
