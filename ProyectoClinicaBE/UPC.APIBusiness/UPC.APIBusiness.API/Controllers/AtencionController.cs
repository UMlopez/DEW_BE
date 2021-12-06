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
  [Route("api/atencion")]
  public class AtencionController : Controller
  {
    /// <summary>
    /// 
    /// </summary>
    protected readonly IAtencionRepository _AtencionRepositorio;
    /// <summary>
    /// 
    /// </summary>
    /// <param name="AtencionRepositorio"></param>
    public AtencionController(IAtencionRepository AtencionRepositorio)
    {
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Produces("application/json")]
    [AllowAnonymous]
    [HttpGet]
    [Route("listaratenciones")]
    public ActionResult ListarAtenciones()
    {
      var ret = _AtencionRepositorio.ListarAtenciones();
      return Json(ret);
    }
  }
}
