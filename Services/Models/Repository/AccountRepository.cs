using Dapper;
using Microsoft.Extensions.Configuration;
using Services.Interfaces;
using Services.Models.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Models.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private IConfiguration configuration;

        public AccountRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public Task<int> Create(Account entity)
        {
            string namestore = "InsertAccount";
            using (var connection = new SqlConnection(configuration.GetConnectionString("ConnectCuaSang")))
            {
                connection.Open();
                var p = new DynamicParameters();
                p.Add("@name", entity.Name);
                p.Add("@email", entity.Email);
                p.Add("@phone", entity.Phone);
                p.Add("@pw", entity.Pw);
                p.Add("@role", entity.Role);
                p.Add("@result", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                connection.Execute(namestore, p, commandType: CommandType.StoredProcedure);
                return Task.FromResult(p.Get<int>("@result"));
                //return 0 => bị trùng email, phone : return id
            }
        }

        public Task<Account> GetById(int id)
        {
            string namestore = "GetByIdAccount";
            using (var connection = new SqlConnection(configuration.GetConnectionString("ConnectCuaSang")))
            {
                connection.Open();
                var p = new DynamicParameters();
                p.Add("@id", id);
                Account result = connection.Query<Account>(namestore, p, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return Task.FromResult(result);
            }
        }

        public Task<IEnumerable<Account>> GetData(int page, int rows)
        {
            string namestore = "GetAccount";
            using (var connection = new SqlConnection(configuration.GetConnectionString("ConnectCuaSang")))
            {
                connection.Open();
                var p = new DynamicParameters();
                p.Add("@page", page);
                p.Add("@rows", rows);
                IEnumerable<Account> result = connection.Query<Account>(namestore, p, commandType: CommandType.StoredProcedure);
                return Task.FromResult(result);
            }
        }

        public Task<Account> Login(Account account)
        {
            string namestore = "LoginAccount";
            using (var connection = new SqlConnection(configuration.GetConnectionString("ConnectCuaSang")))
            {
                connection.Open();
                var p = new DynamicParameters();
                p.Add("@email", account.Email);
                p.Add("@pw", account.Pw);
                Account result = connection.Query<Account>(namestore, p, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return Task.FromResult(result);
            }
        }

        public Task<int> Modify(Account entity)
        {
            string namestore = "UpdateAccount";
            using (var connection = new SqlConnection(configuration.GetConnectionString("ConnectCuaSang")))
            {
                connection.Open();
                var p = new DynamicParameters();
                p.Add("@id", entity.Id);
                p.Add("@name", entity.Name);
                p.Add("@email", entity.Email);
                p.Add("@phone", entity.Phone);
                p.Add("@pw", entity.Pw);
                p.Add("@role", entity.Role);
                p.Add("@result", dbType: DbType.Int32, direction: ParameterDirection.ReturnValue);
                connection.Execute(namestore, p, commandType: CommandType.StoredProcedure);
                return Task.FromResult(p.Get<int>("@result"));
                //return 0 bị trùng email,phone: return 1
            }
        }

        public Task<bool> Remove(int id)
        {
            string namestore = "RemoveAccount";
            using (var connection = new SqlConnection(configuration.GetConnectionString("ConnectCuaSang")))
            {
                connection.Open();
                var p = new DynamicParameters();
                p.Add("@id", id);
                int result = connection.Execute(namestore, p, commandType: CommandType.StoredProcedure);
                return Task.FromResult(result > 0);
                //return số dòng thành công
            }
        }
    }
}
