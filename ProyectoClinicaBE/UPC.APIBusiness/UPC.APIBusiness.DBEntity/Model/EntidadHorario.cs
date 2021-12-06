using System;
using System.Collections.Generic;
using System.Text;

namespace DBEntity
{
    public class EntidadHorario : EntityBase
    {
        public int CoHorario { get; set; }
        public int CoColegiatura { get; set; }
        public string FeHorario { get; set; }
        public string FeHoraInicio { get; set; }
        public string FeHoraFin { get; set; }
    }
}
