using Dapper;
using DBContext;
using DBEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace UPC.APIBusiness.DBContext.Repository
{
  public class SedeRepository : BaseRepository, ISedeRepository
  {
    public List<EntidadSede> ListarSedes()
    {
      var entidadesSedes = new List<EntidadSede>();
      try
      {
        using (var db = GetSqlConnection())
        {
          const string sql = @"usp_ListarSedes";
          entidadesSedes = db.Query<EntidadSede>(sql: sql, commandType: CommandType.StoredProcedure).ToList();
        }
      }
      catch (Exception ex)
      {
      }
      return entidadesSedes;
    }
  }
}
