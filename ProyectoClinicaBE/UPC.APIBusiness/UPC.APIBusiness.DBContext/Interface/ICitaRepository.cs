using DBEntity;
using System;

namespace DBContext
{
  public interface ICitaRepository
  {
    ResponseBase ListarEspecialidades();
    ResponseBase ListarMedicos(int coespecialidad);
    ResponseBase ListarHorariosDias(int cocolegiatura);
    ResponseBase ListarHorariosHoras(int cocolegiatura, DateTime FeHorario);
    ResponseBase ProgramarCita(EntidadAtencion atencion);
    ResponseBase AnularCita(EntidadAtencion atencion);
    ResponseBase PagarCita(EntidadPago pago);
    ResponseBase ListarMisCitas(int idusuario);
  }
}
