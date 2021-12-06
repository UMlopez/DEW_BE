using DBEntity;

namespace DBContext
{
  public interface IUserRepository
  {
    ResponseBase Register(EntityUser user);
    ResponseBase Login(EntityLogin login);
    ResponseBase ValidarDatosCambioContraseña(EntityUser user);
    ResponseBase CambiarContraseña(EntityUser user);
    ResponseBase ObtenerUsuarioPorDNI(EntityUser user);
  }
}
