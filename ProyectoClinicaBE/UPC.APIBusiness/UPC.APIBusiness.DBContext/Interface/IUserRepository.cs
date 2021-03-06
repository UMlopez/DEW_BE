using DBEntity;

namespace DBContext
{
  public interface IUserRepository
  {
    ResponseBase Register(EntityUser user);
    ResponseBase Login(EntityLogin login);
    ResponseBase ValidarDatosCambioContrase├▒a(EntityUser user);
    ResponseBase CambiarContrase├▒a(EntityUser user);
    ResponseBase ObtenerUsuarioPorDNI(EntityUser user);
  }
}
