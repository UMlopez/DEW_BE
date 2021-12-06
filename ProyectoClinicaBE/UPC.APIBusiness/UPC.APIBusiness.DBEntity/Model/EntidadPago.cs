using System;
using System.Collections.Generic;
using System.Text;

namespace DBEntity
{
    public class EntidadPago : EntityBase
    {
        public int CoPago { get; set; }
        public int CoAtencion { get; set; }
        public DateTime FePago { get; set; }
        public decimal SsPago { get; set; }
        public int CoMoneda { get; set; }
        public int CoMedioPago { get; set; }
        public string TxReferencia { get; set; }      
    }
}
