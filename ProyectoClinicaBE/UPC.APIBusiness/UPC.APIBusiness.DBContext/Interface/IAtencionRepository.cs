using DBEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DBContext
{
  public interface IAtencionRepository
  {
    List<EntidadAtencion> ListarAtenciones();
  }
}
