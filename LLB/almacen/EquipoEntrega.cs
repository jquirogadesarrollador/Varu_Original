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
    public class EquipoEntrega
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        Decimal _id_lote = 0;
        String _estado_dev = null;
        Decimal _id_documento = 0;
        Decimal _id_producto = 0;
        Decimal _id_detalle_entregas = 0;
        Decimal _id_equipo_entrega = 0;
        Decimal _id_equipo = 0;
        String _detalle_dev = null;

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

        public Decimal ID_LOTE
        {
            get { return _id_lote; }
            set { _id_lote = value; }
        }

        public String ESTADO_DEV
        {
            get { return _estado_dev; }
            set { _estado_dev = value; }
        }

        public Decimal ID_DOCUMENTO
        {
            get { return _id_documento; }
            set { _id_documento = value; }
        }

        public Decimal ID_PRODUCTO
        {
            get { return _id_producto; }
            set { _id_producto = value; }
        }

        public Decimal ID_DETALLE_ENTREGAS
        {
            get { return _id_detalle_entregas; }
            set { _id_detalle_entregas = value; }
        }

        public Decimal ID_EQUIPO_ENTREGA
        {
            get { return _id_equipo_entrega; }
            set { _id_equipo_entrega = value; }
        }

        public Decimal ID_EQUIPO
        {
            get { return _id_equipo; }
            set { _id_equipo = value; }
        }

        public String DETALLE_DEV
        {
            get { return _detalle_dev; }
            set { _detalle_dev = value; }
        }
        #endregion propiedades

        #region constructores
        public EquipoEntrega(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }

        public EquipoEntrega()
        {
        }
        #endregion constructores

        #region metodos
        #endregion metodos
    }
}
