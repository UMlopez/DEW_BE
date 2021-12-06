using DBContext;
using DBEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace UPC.APIBusiness.API.Controllers
{
  /// <summary>
  /// 
  /// </summary>
  [Produces("application/json")]
  [Route("api/cita")]
  [ApiController]
  public class CitaController : Controller
  {
    /// <summary>
    /// Constructor
    /// </summary>
    protected readonly ICitaRepository _CitaRepository;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="CitaRepository"></param>
    public CitaController(ICitaRepository CitaRepository)
    {
      _CitaRepository = CitaRepository;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Produces("application/json")]
    [AllowAnonymous]
    [HttpGet]
    [Route("listarespecialidades")]
    public ActionResult ListarEspecialidades()
    {
      try
      {
        var ret = _CitaRepository.ListarEspecialidades();
        return Json(ret);
      }
      catch (Exception ex)
      {
        return Json(ex);
      }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="COESPECIALIDAD"></param>
    /// <returns></returns>
    [Produces("application/json")]
    [AllowAnonymous]
    [HttpGet]
    [Route("listarmedicos")]
    public ActionResult ListarMedicos(int COESPECIALIDAD)
    {
      try
      {
        var ret = _CitaRepository.ListarMedicos(COESPECIALIDAD);
        return Json(ret);
      }
      catch (Exception ex)
      {
        return Json(ex);
      }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="COCOLEGIATURA"></param>
    /// <returns></returns>
    [Produces("application/json")]
    [AllowAnonymous]
    [HttpGet]
    [Route("listarhorariosdias")]
    public ActionResult ListarHorariosDias(int COCOLEGIATURA)
    {
      try
      {
        var ret = _CitaRepository.ListarHorariosDias(COCOLEGIATURA);
        return Json(ret);
      }
      catch (Exception ex)
      {
        return Json(ex);
      }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="COCOLEGIATURA"></param>
    /// <param name="FeHorarioStr"></param>
    /// <returns></returns>
    [Produces("application/json")]
    [AllowAnonymous]
    [HttpGet]
    [Route("listarhorarioshoras")]
    public ActionResult ListarHorariosHoras(int COCOLEGIATURA, string FeHorarioStr)
    {
      try
      {
        DateTime FeHorario = DateTime.ParseExact(FeHorarioStr, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
        var ret = _CitaRepository.ListarHorariosHoras(COCOLEGIATURA, FeHorario);
        return Json(ret);
      }
      catch (Exception ex)
      {
        return Json(ex);
      }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="atencion"></param>
    /// <returns></returns>
    [Produces("application/json")]
    [Authorize]
    [HttpPost]
    [Route("programarcita")]
    public ActionResult ProgramarCita(EntidadAtencion atencion)
    {
      try
      {
        var identity = User.Identity as ClaimsIdentity;
        IEnumerable<Claim> claims = identity.Claims;

        var userId = claims.Where(p => p.Type == "client_codigo_usuario").FirstOrDefault()?.Value;

        atencion.UsuarioCrea = int.Parse(userId);

        var ret = _CitaRepository.ProgramarCita(atencion);

        return Json(ret);
      }
      catch (Exception ex)
      {
        throw ex;
      }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="atencion"></param>
    /// <returns></returns>
    [Produces("application/json")]
    [Authorize]
    [HttpPost]
    [Route("reprogramarcita")]
    public ActionResult ReprogramarCita(EntidadAtencion atencion)
    {
      try
      {
        var identity = User.Identity as ClaimsIdentity;
        IEnumerable<Claim> claims = identity.Claims;

        var userId = claims.Where(p => p.Type == "client_codigo_usuario").FirstOrDefault()?.Value;

        atencion.UsuarioCrea = int.Parse(userId);

        var ret = new ResponseBase();

        atencion.CoEstadoAtencion = 4; //Anular cita por repro
        ret = _CitaRepository.AnularCita(atencion);

        ret = _CitaRepository.ProgramarCita(atencion);

        return Json(ret);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="atencion"></param>
    /// <returns></returns>
    [Produces("application/json")]
    [Authorize]
    [HttpPost]
    [Route("anularcita")]
    public ActionResult AnularCita(EntidadAtencion atencion)
    {
      try
      {
        var identity = User.Identity as ClaimsIdentity;
        IEnumerable<Claim> claims = identity.Claims;

        var userId = claims.Where(p => p.Type == "client_codigo_usuario").FirstOrDefault()?.Value;

        atencion.UsuarioCrea = int.Parse(userId);

        var ret = new ResponseBase();

        atencion.CoEstadoAtencion = 3; //Anular cita por pedido del usuario
        ret = _CitaRepository.AnularCita(atencion);

        return Json(ret);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="pago"></param>
    /// <returns></returns>
    [Produces("application/json")]
    [Authorize]
    [HttpPost]
    [Route("pagarcita")]
    public ActionResult PagarCita(EntidadPago pago)
    {
      try
      {
        var identity = User.Identity as ClaimsIdentity;
        IEnumerable<Claim> claims = identity.Claims;

        var userId = claims.Where(p => p.Type == "client_codigo_usuario").FirstOrDefault()?.Value;

        pago.UsuarioCrea = int.Parse(userId);

        var ret = new ResponseBase();

        pago.FePago = DateTime.Now;
        pago.SsPago = 100;
        pago.CoMedioPago = 1; //Medio Electrónico
        pago.CoMoneda = 1; //Soles
        ret = _CitaRepository.PagarCita(pago);

        return Json(ret);
      }
      catch (Exception ex)
      {
        throw ex;
      }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Produces("application/json")]
    [Authorize]
    [HttpGet]
    [Route("listarmiscitas")]
    public ActionResult ListarMisCitas()
    {
      try
      {
        var identity = User.Identity as ClaimsIdentity;
        IEnumerable<Claim> claims = identity.Claims;

        var userId = claims.Where(p => p.Type == "client_codigo_usuario").FirstOrDefault()?.Value;
        int IdUsuario = int.Parse(userId);

        var ret = _CitaRepository.ListarMisCitas(IdUsuario);
        return Json(ret);
      }
      catch (Exception ex)
      {
        return Json(ex);
      }

    }
  }
}
