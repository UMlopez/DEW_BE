using System;
using System.Collections.Generic;
using System.Text;

namespace DBEntity
{
    public class EntidadCita
    {
        public int CoAtencion { get; set; }
        public string FechaCita { get; set; }
        public string HoraCita { get; set; }
        public string Especialidad { get; set; }
        public string Medico { get; set; }
        public bool Pago { get; set; }
        public string Estado { get; set; }
    }
}
