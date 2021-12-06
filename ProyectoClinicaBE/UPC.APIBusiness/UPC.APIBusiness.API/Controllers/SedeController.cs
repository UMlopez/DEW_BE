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
  [Route("api/sede")]
  public class SedeController : Controller
  {
    /// <summary>
    /// 
    /// </summary>
    protected readonly ISedeRepository _SedeRepository;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="SedeRepository"></param>
    public SedeController(ISedeRepository SedeRepository)
    {
      _SedeRepository = SedeRepository;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Produces("application/json")]
    [AllowAnonymous]
    [HttpGet]
    [Route("listarsede")]
    public IActionResult ListarSede()
    {
      try
      {
        var ret = _SedeRepository.ListarSedes();
        return Json(ret);
      }
      catch (Exception ex)
      {
        return Json(ex);
      }
    }

  }
}
