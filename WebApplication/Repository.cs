using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using ResultOf;
using WebApplication.Models;

namespace WebApplication
{
    
    public class RepositoryUtils : IRepositoryUtils
    {
        private readonly string connString;

        public RepositoryUtils(IOptions<DBSettings> settingsOptions)
        {
            var settings = settingsOptions.Value;
            connString = $"Data Source={settings.DataSource};Initial Catalog={settings.DataBase};" +
                         $"Persist Security Info=True;User ID={settings.Username};Password={settings.Password}";
        }

        public SqlConnection GetDbConnection() => new SqlConnection(connString);
    }

    public interface IRepositoryUtils
    {
        SqlConnection GetDbConnection();
    }

    public class Repository : IRepository
    {
        private IRepositoryUtils utils;
        private string tableName = "t_users";

        public Repository(IRepositoryUtils utils)
        {
            this.utils = utils;
        }
        
        public async Task<Result<List<UsersInfo>>> SortUser(SortInfo info)
        {
            using (var connection = utils.GetDbConnection())
            {
                await connection.OpenAsync();
                var sql = "";
                if (info.IsDescending == false) {sql = $"SELECT * FROM {tableName} ORDER BY {info.SortConditionField} DESC";}
                if (info.IsDescending == true) {sql = $"SELECT * FROM {tableName} ORDER BY {info.SortConditionField}";}
                var cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                return await Result.Of(async () =>
                {
                    var reader = await cmd.ExecuteReaderAsync();
                    var str = new List<UsersInfo>();
                    while (reader.Read())
                    {
                        var res = new UsersInfo
                        {
                            PhoneNumber = (string)reader[TableFields.PhoneNumber],
                            FullName = (string)reader[TableFields.FullName],
                            AdditionalNumbers = (string)reader[TableFields.AdditionalNumbers],
                            Email = (string)reader[TableFields.Email],
                            PositionHeld = (string)reader[TableFields.PositionHeld]
                        };
                        str.Add(res);
                    }

                    return str;
                });
            }
        }
        
        public async Task<Result<List<UsersInfo>>> GetUsers()
        {
            using (var connection = utils.GetDbConnection())
            {
                await connection.OpenAsync();
                var sql = $"SELECT * FROM {tableName}";
                var cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                return await Result.Of(async () =>
                {
                    var reader = await cmd.ExecuteReaderAsync();
                    var str = new List<UsersInfo>();
                    while (reader.Read())
                    {
                        var res = new UsersInfo
                        {
                            PhoneNumber = (string)reader[TableFields.PhoneNumber],
                            FullName = (string)reader[TableFields.FullName],
                            AdditionalNumbers = (string)reader[TableFields.AdditionalNumbers],
                            Email = (string)reader[TableFields.Email],
                            PositionHeld = (string)reader[TableFields.PositionHeld]
                        };
                        
                        str.Add(res);
                    }

                    return str;
                });
            }
        }
        
        public async Task<Result<List<UsersInfo>>> SelectUser(SelectInfo info)
        {
            using (var connection = utils.GetDbConnection())
            {
                await connection.OpenAsync();
                var sql = $"SELECT * FROM {tableName} WHERE {info.SelectConditionField} = @SelectConditionValue";
                var cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.Add("@SelectConditionValue", SqlDbType.NVarChar).Value = info.SelectConditionValue;
                return await Result.Of(async () =>
                {
                    var reader = await cmd.ExecuteReaderAsync();
                    var str = new List<UsersInfo>();
                    while (reader.Read())
                    {
                        var res = new UsersInfo
                        {
                            PhoneNumber = (string)reader[TableFields.PhoneNumber],
                            FullName = (string)reader[TableFields.FullName],
                            AdditionalNumbers = (string)reader[TableFields.AdditionalNumbers],
                            Email = (string)reader[TableFields.Email],
                            PositionHeld = (string)reader[TableFields.PositionHeld]
                        };
                        str.Add(res);
                    }
                    return str;
                });
            }
        }
        
        public async Task<Result<int>> DeleteUser(DeleteInfo info)
        {
            Result<int> result;
            using (var connection = utils.GetDbConnection())
            {
                await connection.OpenAsync();
                var sql = $"DELETE FROM {tableName} WHERE {info.DeletedField} = @deletedValue";
                var cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.Add("@deletedValue", SqlDbType.NVarChar).Value = info.DeletedValue;
                result = await Result.Of(async() => await cmd.ExecuteNonQueryAsync());
            }
            
            return result;
        }

        
        public async Task<Result<int>> ChangeUser(UsersInfo info)
        {
            bool isNULL = true; 
            Result<int> result;
            using (var connection = utils.GetDbConnection())
            {
                
                await connection.OpenAsync();
                var cmd = connection.CreateCommand();
                cmd.Parameters.Add("@phoneNumber", SqlDbType.NVarChar).Value = info.PhoneNumber;
                var builder = new StringBuilder();
                var sql = $"UPDATE {tableName} SET";
                if (info.PhoneNumber != null)
                {
                    cmd.Parameters.Add("@phoneNumber", SqlDbType.NVarChar).Value = info.PhoneNumber;
                    builder.Append(" phoneNumber = @phoneNumber");
                    isNULL = false;
                }
                if (info.FullName != null)
                {
                    cmd.Parameters.Add("@fullName", SqlDbType.NVarChar).Value = info.FullName;
                    builder.Append(", fullName = @fullName");
                    isNULL = false;
                }

                if (info.Email != null)
                {
                    cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = info.Email;
                    builder.Append(", email = @email");
                    isNULL = false;
                }

                if (info.PositionHeld != null)
                {
                    cmd.Parameters.Add("@positionHeld", SqlDbType.NVarChar).Value = info.PositionHeld;
                    builder.Append(", positionHeld = @positionHeld");
                    isNULL = false;
                }

                if (info.AdditionalNumbers != null)
                {
                    cmd.Parameters.Add("@additionalNumbers", SqlDbType.NVarChar).Value = info.AdditionalNumbers;
                    builder.Append(", additionalNumbers = @additionalNumbers");
                    isNULL = false;
                }

                sql = sql+builder.ToString() + " WHERE phoneNumber = @userPhoneToChange";
                cmd.CommandText = sql;
                cmd.Parameters.Add("@userPhoneToChange", SqlDbType.NVarChar).Value = info.UserPhoneToChange;


                result = await Result.Of(async() => await cmd.ExecuteNonQueryAsync());
            }

            if (isNULL) return 0;
            return result;
        }

        public async Task<Result<int>> InsertUser(UsersInfo info)
        {
            Result<int> result;
            using (var connection = utils.GetDbConnection())
            {
                await connection.OpenAsync();
                var sql = $"INSERT INTO {tableName}(phoneNumber, fullName, email, positionHeld, additionalNumbers) VALUES( @phoneNumber, @fullName, @email, @positionHeld, @additionalNumbers)";
                var cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                cmd.Parameters.Add("@phoneNumber", SqlDbType.NVarChar).Value = info.PhoneNumber;
                cmd.Parameters.Add("@fullName", SqlDbType.NVarChar).Value = info.FullName; 
                cmd.Parameters.Add("@email", SqlDbType.NVarChar).Value = info.Email;
                cmd.Parameters.Add("@positionHeld", SqlDbType.NVarChar).Value = info.PositionHeld;
                cmd.Parameters.Add("@additionalNumbers", SqlDbType.NVarChar).Value = info.AdditionalNumbers;
               

                result = await Result.Of(async () => await cmd.ExecuteNonQueryAsync());
            }

            return result;
        }
    }

    public interface IRepository
    {
        Task<Result<List<UsersInfo>>> SelectUser(SelectInfo info);
        Task<Result<int>> InsertUser(UsersInfo info);
        Task<Result<int>> DeleteUser(DeleteInfo info);
        Task<Result<int>> ChangeUser(UsersInfo info);
        Task<Result<List<UsersInfo>>> SortUser(SortInfo info);
        Task<Result<List<UsersInfo>>> GetUsers();
    }
}