using Dapper;
using DBEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DBContext
{
  public class CitaRepository : BaseRepository, ICitaRepository
  {
    public ResponseBase ListarEspecialidades()
    {
      var returnEntity = new ResponseBase();
      var entityEspecialidades = new List<EntidadEspecialidad>();

      try
      {
        using (var db = GetSqlConnection())
        {
          const string sql = @"usp_listar_especialidades";

          entityEspecialidades = db.Query<EntidadEspecialidad>(sql: sql, commandType: CommandType.StoredProcedure).ToList();

          if (entityEspecialidades != null)
          {
            returnEntity.isSuccess = true;
            returnEntity.errorCode = "0000";
            returnEntity.errorMessage = string.Empty;
            returnEntity.data = entityEspecialidades;
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

    public ResponseBase ListarMedicos(int coespecialidad)
    {
      var returnEntity = new ResponseBase();
      var entityMedicos = new List<EntidadMedico>();

      try
      {
        using (var db = GetSqlConnection())
        {
          const string sql = @"usp_listar_medicos";
          var p = new DynamicParameters();
          p.Add(name: "@COESPECIALIDAD", value: coespecialidad, dbType: DbType.Int32, direction: ParameterDirection.Input);

          entityMedicos = db.Query<EntidadMedico>(sql: sql, param: p, commandType: CommandType.StoredProcedure).ToList();

          if (entityMedicos != null)
          {
            returnEntity.isSuccess = true;
            returnEntity.errorCode = "0000";
            returnEntity.errorMessage = string.Empty;
            returnEntity.data = entityMedicos;
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

    public ResponseBase ListarHorariosDias(int cocolegiatura)
    {
      var returnEntity = new ResponseBase();
      var entityHorarios = new List<EntidadHorario>();

      try
      {
        using (var db = GetSqlConnection())
        {
          const string sql = @"usp_listar_horarios_dias";
          var p = new DynamicParameters();
          p.Add(name: "@COCOLEGIATURA", value: cocolegiatura, dbType: DbType.Int32, direction: ParameterDirection.Input);

          entityHorarios = db.Query<EntidadHorario>(sql: sql, param: p, commandType: CommandType.StoredProcedure).ToList();

          if (entityHorarios != null)
          {
            returnEntity.isSuccess = true;
            returnEntity.errorCode = "0000";
            returnEntity.errorMessage = string.Empty;
            returnEntity.data = entityHorarios;
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

    public ResponseBase ListarHorariosHoras(int cocolegiatura, DateTime FeHorario)
    {
      var returnEntity = new ResponseBase();
      var entityHorarios = new List<EntidadHorario>();

      try
      {
        using (var db = GetSqlConnection())
        {
          const string sql = @"usp_listar_horarios_horas";
          var p = new DynamicParameters();
          p.Add(name: "@COCOLEGIATURA", value: cocolegiatura, dbType: DbType.Int32, direction: ParameterDirection.Input);
          p.Add(name: "@FEHORARIO", value: FeHorario, dbType: DbType.Date, direction: ParameterDirection.Input);

          entityHorarios = db.Query<EntidadHorario>(sql: sql, param: p, commandType: CommandType.StoredProcedure).ToList();

          if (entityHorarios != null)
          {
            returnEntity.isSuccess = true;
            returnEntity.errorCode = "0000";
            returnEntity.errorMessage = string.Empty;
            returnEntity.data = entityHorarios;
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

    public ResponseBase ProgramarCita(EntidadAtencion atencion)
    {
      var returnEntity = new ResponseBase();
      var entityAtencion = new List<EntidadAtencion>();

      try
      {
        using (var db = GetSqlConnection())
        {
          const string sql = @"usp_programar_cita";
          var p = new DynamicParameters();
          p.Add(name: "@COATENCION", dbType: DbType.Int32, direction: ParameterDirection.Output);
          p.Add(name: "@FEATENCION", dbType: DbType.Date, direction: ParameterDirection.Output);
          p.Add(name: "@COCOLEGIATURA", value: atencion.CoColegiatura, dbType: DbType.Int32, direction: ParameterDirection.Input);
          p.Add(name: "@COHORARIO", value: atencion.CoHorario, dbType: DbType.Int32, direction: ParameterDirection.Input);
          p.Add(name: "@IDUSUARIO", value: atencion.UsuarioCrea, dbType: DbType.Int32, direction: ParameterDirection.Input);
          p.Add(name: "@USUARIOCREA", value: atencion.UsuarioCrea, dbType: DbType.Int32, direction: ParameterDirection.Input);

          db.Query<EntidadAtencion>(sql: sql, param: p, commandType: CommandType.StoredProcedure).FirstOrDefault();

          int CoAtencion = p.Get<int>("@COATENCION");
          DateTime FeAtencion = p.Get<DateTime>("@FEATENCION");

          atencion.CoAtencion = CoAtencion;
          atencion.FeAtencionStr = FeAtencion.ToString("dd/MM/yyyy");

          if (entityAtencion != null)
          {
            returnEntity.isSuccess = true;
            returnEntity.errorCode = "0000";
            returnEntity.errorMessage = string.Empty;
            returnEntity.data = atencion;
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

    public ResponseBase AnularCita(EntidadAtencion atencion)
    {
      var returnEntity = new ResponseBase();

      try
      {
        using (var db = GetSqlConnection())
        {
          const string sql = @"usp_anular_cita";
          var p = new DynamicParameters();
          p.Add(name: "@COATENCION", value: atencion.CoAtencion, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
          p.Add(name: "@COESTADOATENCION", value: atencion.CoEstadoAtencion, dbType: DbType.Int32, direction: ParameterDirection.Input);
          p.Add(name: "@USUARIOMODIFICA", value: atencion.UsuarioCrea, dbType: DbType.Int32, direction: ParameterDirection.Input);

          db.Query<EntidadAtencion>(sql: sql, param: p, commandType: CommandType.StoredProcedure).FirstOrDefault();

          int CoAtencion = p.Get<int>("@COATENCION");

          if (CoAtencion > 0)
          {
            returnEntity.isSuccess = true;
            returnEntity.errorCode = "0000";
            returnEntity.errorMessage = string.Empty;
            returnEntity.data = atencion;
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

    public ResponseBase PagarCita(EntidadPago pago)
    {
      var returnEntity = new ResponseBase();

      try
      {
        using (var db = GetSqlConnection())
        {
          const string sql = @"usp_pagar_cita";
          var p = new DynamicParameters();
          p.Add(name: "@COPAGO", dbType: DbType.Int32, direction: ParameterDirection.Output);
          p.Add(name: "@COATENCION", value: pago.CoAtencion, dbType: DbType.Int32, direction: ParameterDirection.Input);
          p.Add(name: "@FEPAGO", value: pago.FePago, dbType: DbType.Date, direction: ParameterDirection.Input);
          p.Add(name: "@SSPAGO", value: pago.SsPago, dbType: DbType.Decimal, direction: ParameterDirection.Input);
          p.Add(name: "@COMONEDA", value: pago.CoMoneda, dbType: DbType.Int32, direction: ParameterDirection.Input);
          p.Add(name: "@COMEDIOPAGO", value: pago.CoMedioPago, dbType: DbType.Int32, direction: ParameterDirection.Input);
          p.Add(name: "@USUARIOCREA", value: pago.UsuarioCrea, dbType: DbType.Int32, direction: ParameterDirection.Input);

          db.Query<EntidadAtencion>(sql: sql, param: p, commandType: CommandType.StoredProcedure).FirstOrDefault();

          int CoPago = p.Get<int>("@COPAGO");
          pago.CoPago = CoPago;

          if (CoPago > 0)
          {
            returnEntity.isSuccess = true;
            returnEntity.errorCode = "0000";
            returnEntity.errorMessage = string.Empty;
            returnEntity.data = pago;
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

    public ResponseBase ListarMisCitas(int idusuario)
    {
      var returnEntity = new ResponseBase();
      var entityCitas = new List<EntidadCita>();

      try
      {
        using (var db = GetSqlConnection())
        {
          const string sql = @"usp_obtener_miscitas";
          var p = new DynamicParameters();
          p.Add(name: "@IDUSUARIO", value: idusuario, dbType: DbType.Int32, direction: ParameterDirection.Input);

          entityCitas = db.Query<EntidadCita>(sql: sql, param: p, commandType: CommandType.StoredProcedure).ToList();

          if (entityCitas != null)
          {
            returnEntity.isSuccess = true;
            returnEntity.errorCode = "0000";
            returnEntity.errorMessage = string.Empty;
            returnEntity.data = entityCitas;
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
  }
}
