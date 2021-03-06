using Dapper;
using DBEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBContext
{
  public class UserRepository : BaseRepository, IUserRepository
  {
    public ResponseBase Register(EntityUser user)
    {
      var returnEntity = new ResponseBase();

      try
      {
        using (var db = GetSqlConnection())
        {
          const string sql = @"usp_InsertarUsuario";

          var p = new DynamicParameters();
          p.Add(name: "@IDUSUARIO", dbType: DbType.Int32, direction: ParameterDirection.Output);
          p.Add(name: "@LOGINUSUARIO", value: user.LoginUsuario, dbType: DbType.String, direction: ParameterDirection.Input);
          p.Add(name: "@PASSWORDUSUARIO", value: user.PasswordUsuario, dbType: DbType.String, direction: ParameterDirection.Input);
          p.Add(name: "@IDPERFIL", value: int.Parse(user.IdPerfil), dbType: DbType.Int32, direction: ParameterDirection.Input);
          p.Add(name: "@NOMBRES", value: user.Nombres, dbType: DbType.String, direction: ParameterDirection.Input);
          p.Add(name: "@APELLIDOPATERNO", value: user.ApellidoPaterno, dbType: DbType.String, direction: ParameterDirection.Input);
          p.Add(name: "@APELLIDOMATERNO", value: user.ApellidoMaterno, dbType: DbType.String, direction: ParameterDirection.Input);
          p.Add(name: "@DOCUMENTOIDENTIDAD", value: user.DocumentoIdentidad, dbType: DbType.String, direction: ParameterDirection.Input);
          p.Add(name: "@TXCORREO", value: user.TxCorreo, dbType: DbType.String, direction: ParameterDirection.Input);
          p.Add(name: "@NUTELEFONO", value: user.NuTelefono, dbType: DbType.String, direction: ParameterDirection.Input);
          p.Add(name: "@USUARIOCREA", value: 1, dbType: DbType.Int32, direction: ParameterDirection.Input);

          db.Query<EntityUser>(sql: sql, param: p, commandType: CommandType.StoredProcedure).FirstOrDefault();

          int idUsuario = p.Get<int>("@IDUSUARIO");
          user.IdUsuario = idUsuario;

          if (idUsuario > 0)
          {
            returnEntity.isSuccess = true;
            returnEntity.errorCode = "0000";
            returnEntity.errorMessage = string.Empty;
            returnEntity.data = user;
          }
          else
          {
            returnEntity.isSuccess = false;
            returnEntity.errorCode = "0001";
            returnEntity.errorMessage = "No se pudo registrar al usuario";
            returnEntity.data = null;
          }
        }
      }
      catch (Exception ex)
      {
        returnEntity.isSuccess = false;
        returnEntity.errorCode = "0001";
        returnEntity.errorMessage = ex.Message;
        returnEntity.data = null;
      }

      return returnEntity;
    }

    public ResponseBase Login(EntityLogin login)
    {
      var returnEntity = new ResponseBase();
      var entityUser = new EntityLoginResponse();

      try
      {
        using (var db = GetSqlConnection())
        {
          const string sql = @"usp_user_login";

          var p = new DynamicParameters();
          p.Add(name: "@LOGINUSUARIO", value: login.LoginUsuario, dbType: DbType.String, direction: ParameterDirection.Input);
          p.Add(name: "@PASSWORDUSUARIO", value: login.PasswordUsuario, dbType: DbType.String, direction: ParameterDirection.Input);

          entityUser = db.Query<EntityLoginResponse>(sql: sql, param: p, commandType: CommandType.StoredProcedure).FirstOrDefault();

          if (entityUser != null)
          {
            returnEntity.isSuccess = true;
            returnEntity.errorCode = "0000";
            returnEntity.errorMessage = string.Empty;
            returnEntity.data = entityUser;
          }
          else
          {
            returnEntity.isSuccess = false;
            returnEntity.errorCode = "0000";
            returnEntity.errorMessage = string.Empty;
            returnEntity.data = null;
          }
        }
      }
      catch (Exception ex)
      {
        returnEntity.isSuccess = false;
        returnEntity.errorCode = "0001";
        returnEntity.errorMessage = ex.Message;
        returnEntity.data = null;
      }

      return returnEntity;
    }

    public ResponseBase ValidarDatosCambioContrase??a(EntityUser user)
    {
      var returnEntity = new ResponseBase();

      try
      {
        using (var db = GetSqlConnection())
        {
          const string sql = @"usp_ValidarDatosCambioContrase??a";

          var p = new DynamicParameters();
          p.Add(name: "@DOCUMENTOIDENTIDAD", value: user.DocumentoIdentidad, dbType: DbType.String, direction: ParameterDirection.Input);
          p.Add(name: "@TXCORREO", value: user.TxCorreo, dbType: DbType.String, direction: ParameterDirection.Input);
          p.Add(name: "@PASSWORDUSUARIO", value: user.PasswordUsuario, dbType: DbType.String, direction: ParameterDirection.Input);
          p.Add(name: "@MENSAJE", dbType: DbType.String, direction: ParameterDirection.Output, size: 255);
          p.Add(name: "@IDUSUARIO", dbType: DbType.Int32, direction: ParameterDirection.Output);

          db.Query<EntityUser>(sql: sql, param: p, commandType: CommandType.StoredProcedure).FirstOrDefault();

          db.Execute(sql: sql, param: p, commandType: CommandType.StoredProcedure);

          string mensaje = p.Get<string>("@MENSAJE");
          int idUsuario = p.Get<int>("@IDUSUARIO");

          if (idUsuario != 0)
          {
            returnEntity.isSuccess = true;
            returnEntity.errorCode = "0000";
            returnEntity.errorMessage = string.Empty;
            returnEntity.data = idUsuario;
          }
          else
          {
            returnEntity.isSuccess = false;
            returnEntity.errorCode = "0001";
            returnEntity.errorMessage = mensaje;
            returnEntity.data = null;
          }
        }
      }
      catch (Exception ex)
      {
        returnEntity.isSuccess = false;
        returnEntity.errorCode = "0001";
        returnEntity.errorMessage = ex.Message;
        returnEntity.data = null;
      }

      return returnEntity;
    }

    public ResponseBase CambiarContrase??a(EntityUser user)
    {
      var returnEntity = new ResponseBase();

      try
      {
        using (var db = GetSqlConnection())
        {
          const string sql = @"usp_CambiarContrase??a";

          var p = new DynamicParameters();
          p.Add(name: "@IDUSUARIO", value: user.IdUsuario, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
          p.Add(name: "@PASSWORDUSUARIO", value: user.PasswordUsuario, dbType: DbType.String, direction: ParameterDirection.Input);

          db.Query<EntityUser>(sql: sql, param: p, commandType: CommandType.StoredProcedure).FirstOrDefault();

          int idUsuario = p.Get<int>("@IDUSUARIO");

          if (idUsuario > 0)
          {
            returnEntity.isSuccess = true;
            returnEntity.errorCode = "0000";
            returnEntity.errorMessage = string.Empty;
            returnEntity.data = new
            {
              id = idUsuario
            };
          }
          else
          {
            returnEntity.isSuccess = false;
            returnEntity.errorCode = "0001";
            returnEntity.errorMessage = "La contrase??a no pudo ser modificada";
            returnEntity.data = null;
          }
        }
      }
      catch (Exception ex)
      {
        returnEntity.isSuccess = false;
        returnEntity.errorCode = "0001";
        returnEntity.errorMessage = ex.Message;
        returnEntity.data = null;
      }

      return returnEntity;
    }

    public ResponseBase ObtenerUsuarioPorDNI(EntityUser user)
    {
      var returnEntity = new ResponseBase();

      try
      {
        using (var db = GetSqlConnection())
        {
          const string sql = @"usp_ObtenerUsuarioPorDNI";

          var p = new DynamicParameters();
          p.Add(name: "@IDUSUARIO", value: user.IdUsuario, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
          p.Add(name: "@DOCUMENTOIDENTIDAD", value: user.DocumentoIdentidad, dbType: DbType.String, direction: ParameterDirection.Input);

          db.Query<EntityUser>(sql: sql, param: p, commandType: CommandType.StoredProcedure).FirstOrDefault();

          int idUsuario = p.Get<int>("@IDUSUARIO");

          if (idUsuario > 0)
          {
            returnEntity.isSuccess = true;
            returnEntity.errorCode = "0000";
            returnEntity.errorMessage = string.Empty;
            returnEntity.data = idUsuario;
          }
          else
          {
            returnEntity.isSuccess = true;
            returnEntity.errorCode = "0000";
            returnEntity.errorMessage = string.Empty;
            returnEntity.data = null;
          }
        }
      }
      catch (Exception ex)
      {
        returnEntity.isSuccess = false;
        returnEntity.errorCode = "0001";
        returnEntity.errorMessage = ex.Message;
        returnEntity.data = null;
      }

      return returnEntity;
    }
  }
}
