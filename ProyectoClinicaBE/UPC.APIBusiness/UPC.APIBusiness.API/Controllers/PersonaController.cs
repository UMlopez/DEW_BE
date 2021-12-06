using DBContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UPC.APIBusiness.API.Controllers
{
  /// <summary>
  /// 
  /// </summary>
  [Produces("application/json")]
  [Route("api/persona")]
  public class PersonaController : Controller
  {
    /// <summary>
    /// 
    /// </summary>
    protected readonly IPersonaRepository _PersonaRepository;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="PersonaRepository"></param>
    public PersonaController(IPersonaRepository PersonaRepository)
    {
      _PersonaRepository = PersonaRepository;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="CoDni"></param>
    /// <returns></returns>
    [Produces("application/json")]
    [AllowAnonymous]
    [HttpGet]
    [Route("obtenerpersona")]
    public ActionResult ObtenerPersona(int CoDni)
    {
      try
      {
        var ret = _PersonaRepository.ObtenerPersona(CoDni);
        return Json(ret);
      }
      catch (Exception ex)
      {
        return Json(ex);
      }

    }
  }
}
