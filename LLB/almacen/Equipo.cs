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
    public class Equipo
    {
        #region variables
        String _empresa = null;
        String _usuario = null;
        String _mensaje_error = null;

        Decimal _id_index = 0;
        Decimal _id_detalle_entrega = 0;
        Decimal _id_asignacion_sc = 0;
        Decimal _id_lote = 0;
        Decimal _id_documento = 0;
        Decimal _id_equipo = 0;
        Decimal _sc_aiu = 0;
        Decimal _sc_iva = 0;
        Decimal _sc_valor = 0;
        Decimal _costo_unidad = 0;
        Decimal _id_producto = 0;
        DateTime _fecha_proyecta_entrega = new DateTime();
        String _tipo_entrega = null;
        Int32 _cantidad_total = 0;

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

        public Decimal ID_INDEX
        {
            get { return _id_index; }
            set { _id_index = value; }
        }

        public Decimal ID_DETALLE_ENTREGA
        {
            get { return _id_detalle_entrega; }
            set { _id_detalle_entrega = value; }
        }

        public Decimal ID_ASIGNACION_SC
        {
            get { return _id_asignacion_sc; }
            set { _id_asignacion_sc = value; }
        }

        public Decimal ID_LOTE
        {
            get { return _id_lote; }
            set { _id_lote = value; }
        }

        public Decimal ID_DOCUMENTO
        {
            get { return _id_documento; }
            set { _id_documento = value; }
        }

        public Decimal ID_EQUIPO
        {
            get { return _id_equipo; }
            set { _id_equipo = value; }
        }

        public Decimal SC_AIU
        {
            get { return _sc_aiu; }
            set { _sc_aiu = value; }
        }

        public Decimal SC_IVA
        {
            get { return _sc_iva; }
            set { _sc_iva = value; }
        }

        public Decimal SC_VALOR
        {
            get { return _sc_valor; }
            set { _sc_valor = value; }
        }

        public Decimal COSTO_UNIDAD
        {
            get { return _costo_unidad; }
            set { _costo_unidad = value; }
        }

        public Decimal ID_PRODUCTO
        {
            get { return _id_producto; }
            set { _id_producto = value; }
        }

        public DateTime FECHA_PROYECTADA_ENTREGA
        {
            get { return _fecha_proyecta_entrega; }
            set { _fecha_proyecta_entrega = value; }
        }

        public String TIPO_ENTREGA
        {
            get { return _tipo_entrega; }
            set { _tipo_entrega = value; }
        }

        public Int32 CANTIDAD_TOTAL
        {
            get { return _cantidad_total; }
            set { _cantidad_total = value; }
        }
        #endregion propiedades

        #region constructores
        public Equipo(String idEmpresa, String usuario)
        {
            Empresa = idEmpresa;
            Usuario = usuario;
        }

        public Equipo()
        {
        }
        #endregion constructores

        #region metodos
        #endregion metodos
    }
}
