using System;
using System.Collections.Generic;
using System.Text;

namespace DBEntity
{
    public class EntidadAtencion : EntityBase
    {
        public int CoAtencion { get; set; }
        public DateTime FeAtencion { get; set; }
        public string FeAtencionStr { get; set; }
        public int CoColegiatura { get; set; }
        public int CoEstadoAtencion { get; set; }
        public int CoHorario { get; set; }
    }
}
