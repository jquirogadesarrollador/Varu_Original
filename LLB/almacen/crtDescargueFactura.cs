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
    public class crtDescargueFactura
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
        public crtDescargueFactura(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }
        #endregion constructores

        #region metodos

        public Decimal AdicionarAlmCrtDescargueInventario(Decimal ID_DOCUMENTO,
            Decimal ID_ORDEN,
            Decimal ID_PRODUCTO,
            Decimal ID_DETALLE,
            Decimal ID_BODEGA,
            Decimal ID_LOTE,
            String REFERENCIA_PRODUCTO,
            String TALLA,
            Int32 CANTIDAD_DESCARGADA,
            Conexion conexion,
            String REEMBOLSO)
        {
            String sql = null;
            String identificador = null;
            String informacion = null;
            Boolean ejecutar = true;

            tools _tools = new tools();

            sql = "usp_alm_crt_descargue_inventario_adicionar ";

            #region validaciones
            if (ID_DOCUMENTO != 0)
            {
                sql += ID_DOCUMENTO + ", ";
                informacion += "ID_DOCUMENTO = '" + ID_DOCUMENTO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_DOCUMENTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_ORDEN != 0)
            {
                sql += ID_ORDEN + ", ";
                informacion += "ID_ORDEN = '" + ID_ORDEN + "', ";
            }
            else
            {
                MensajeError += "El campo ID_ORDEN no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_PRODUCTO != 0)
            {
                sql += ID_PRODUCTO + ", ";
                informacion += "ID_PRODUCTO = '" + ID_PRODUCTO + "', ";
            }
            else
            {
                MensajeError += "El campo ID_PRODUCTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_DETALLE != 0)
            {
                sql += ID_DETALLE + ", ";
                informacion += "ID_DETALLE = '" + ID_DETALLE + "', ";
            }
            else
            {
                MensajeError += "El campo ID_DETALLE no puede ser nulo\n";
                ejecutar = false;
            }

            if (ID_BODEGA != 0)
            {
                sql += ID_BODEGA + ", ";
                informacion += "ID_BODEGA = '" + ID_BODEGA + "', ";
            }
            else
            {
                MensajeError += "El campo ID_BODEGA no puede ser nulo\n";
                ejecutar = false;
            }

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

            if (!(String.IsNullOrEmpty(REFERENCIA_PRODUCTO)))
            {
                sql += "'" + REFERENCIA_PRODUCTO + "', ";
                informacion += "REFERENCIA_PRODUCTO = '" + REFERENCIA_PRODUCTO + "', ";
            }
            else
            {
                MensajeError += "El campo REFERENCIA_PRODUCTO no puede ser nulo\n";
                ejecutar = false;
            }

            if (!(String.IsNullOrEmpty(TALLA)))
            {
                if ((TALLA == "NA") || (TALLA == "N/A"))
                {
                    sql += "'N/A', ";
                    informacion += "TALLA = 'N/A', ";
                }
                else
                {
                    sql += "'" + TALLA + "', ";
                    informacion += "TALLA = '" + TALLA + "', ";
                }
            }
            else
            {
                sql += "'N/A', ";
                informacion += "TALLA = 'N/A', ";
            }


            if (CANTIDAD_DESCARGADA != 0)
            {
                sql += CANTIDAD_DESCARGADA + ", ";
                informacion += "CANTIDAD_DESCARGADA = '" + CANTIDAD_DESCARGADA + "', ";
            }
            else
            {
                MensajeError += "El campo CANTIDAD_DESCARGADA no puede ser nulo\n";
                ejecutar = false;
            }

            sql += "'" + Usuario + "', ";
            informacion += "USU_CRE = '" + Usuario.ToString() + "', ";

            if (String.IsNullOrEmpty(REEMBOLSO) == false)
            {
                sql += "'" + REEMBOLSO + "'";
            }
            else
            {
                ejecutar = false;
                MensajeError = "El campo REEMBOLSO no puede ser vacio.";
            }

            #endregion validaciones

            if (ejecutar)
            {
                try
                {
                    identificador = conexion.ExecuteScalar(sql);

                    #region auditoria
                    auditoria _auditoria = new auditoria(Empresa);
                    _auditoria.Adicionar(Usuario, tabla.ALM_CRT_DESCARGUE_INVENTARIO, tabla.ACCION_ADICIONAR, sql, informacion, conexion);
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

        #endregion
    }
}
