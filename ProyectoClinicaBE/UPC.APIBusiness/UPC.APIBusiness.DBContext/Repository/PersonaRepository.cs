using Dapper;
using DBContext;
using DBEntity;
using System;
using System.Data;
using System.Linq;

namespace UPC.APIBusiness.DBContext.Repository
{
  public class PersonaRepository : BaseRepository, IPersonaRepository
  {
    public EntidadPersona ObtenerPersona(int CoDni)
    {
      var entidadPersona = new EntidadPersona();
      try
      {
        using (var db = GetSqlConnection())
        {
          const string sql = @"usp_ObtenerPersona";
          var p = new DynamicParameters();
          p.Add(name: "@CODNI", value: CoDni, dbType: DbType.Int32, direction: ParameterDirection.Input);

          entidadPersona = db.Query<EntidadPersona>(sql: sql, param: p, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }
      }
      catch (Exception ex)
      {
      }
      return entidadPersona;
    }
  }
}
