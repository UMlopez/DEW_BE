using Dapper;
using DBEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DBContext
{
  public class AtencionRepositorio : BaseRepository, IAtencionRepository
  {
    public List<EntidadAtencion> ListarAtenciones()
    {
      var listadoAtencion = new List<EntidadAtencion>();
      try
      {
        using (var db = GetSqlConnection())
        {
          const string sql = @"usp_ListarAtenciones";
          listadoAtencion = db.Query<EntidadAtencion>(
              sql: sql,
              commandType: CommandType.StoredProcedure
          ).ToList();

        }
      }
      catch (Exception ex)
      {
      }
      return listadoAtencion;
    }
  }
}
