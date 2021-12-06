using DBContext;
using DBEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using UPC.E31B.APIBusiness.API.Security;

namespace UPC.Business.API.Controllers
{
  /// <summary>
  /// 
  /// </summary>
  [Produces("application/json")]
  [Route("api/usuario")]
  [ApiController]
  public class UserController : Controller
  {

    /// <summary>
    /// Constructor
    /// </summary>
    protected readonly IUserRepository _UserRepository;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="UserRepository"></param>
    public UserController(IUserRepository UserRepository)
    {
      _UserRepository = UserRepository;

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [Produces("application/json")]
    [AllowAnonymous]
    [HttpPost]
    [Route("register")]
    public ActionResult Register(EntityUser user)
    {
      user.LoginUsuario = user.DocumentoIdentidad;

      var ret = new ResponseBase();

      ret = _UserRepository.ObtenerUsuarioPorDNI(user);

      if (ret.data != null)
      {
        ret.isSuccess = false;
        ret.errorMessage = "Ya existe un usuario con DNI ingresado.";
        return Json(ret);
      }

      ret = _UserRepository.Register(user);

      return Json(ret);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="login"></param>
    /// <returns></returns>
    [Produces("application/json")]
    [AllowAnonymous]
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> Login(EntityLogin login)
    {
      var ret = _UserRepository.Login(login);

      if (ret.data != null)
      {
        var responseLogin = ret.data as EntityLoginResponse;
        var userId = responseLogin.idusuario.ToString();
        var userDoc = responseLogin.documentoidentidad;

        var token = JsonConvert
                            .DeserializeObject<AccessToken>(
                                await new Authentication()
                                .GenerateToken(userDoc, userId)
                                ).access_token;

        responseLogin.token = token;
        ret.data = responseLogin;
      }

      return Json(ret);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    [Produces("application/json")]
    [AllowAnonymous]
    [HttpPost]
    [Route("cambiarcontraseña")]
    public ActionResult CambiarContraseña(EntityUser user)
    {
      var ret = new ResponseBase();

      if (user.PasswordUsuario != user.ConfirmPasswordUsuario)
      {
        ret.isSuccess = false;
        ret.errorCode = "0001";
        ret.errorMessage = "Las contraseñas deben coincidir";
        ret.data = null;

        return Json(ret);
      }

      ret = _UserRepository.ValidarDatosCambioContraseña(user);

      if (ret.errorCode == "0001")
      {
        return Json(ret);
      }

      user.IdUsuario = (int)ret.data;

      ret = _UserRepository.CambiarContraseña(user);

      return Json(ret);
    }
  }
}
