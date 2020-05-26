using Dapper;
using Microsoft.Extensions.Configuration;
using Services.Models.Entities;
using Services.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Models.Repository
{
    public class BallotRequestRepository : IBallotRequestRepository
    {
        private IConfiguration configuration;

        public BallotRequestRepository(IConfiguration _configuration)
        {
            configuration = _configuration;
        }

        public Task<int> Create(BallotRequest entity)
        {
            string namestore = "InsertBallot";
            using (var connection = new SqlConnection(configuration.GetConnectionString("ConnectCuaSang")))
            {
                connection.Open();
                var p = new DynamicParameters();
                p.Add("@acc_id", entity.AccId);
                p.Add("@type", entity.Type);
                p.Add("@title", entity.Title);
                p.Add("@content", entity.Content);
                p.Add("@status", entity.Status);
                int id = connection.Query<int>(namestore, p, commandType: CommandType.StoredProcedure).Single();
                return Task.FromResult(id);
                //Return id
            }
        }

        public Task<BallotRequest> GetById(int id)
        {
            string namestore = "GetByIdBallot";
            using (var connection = new SqlConnection(configuration.GetConnectionString("ConnectCuaSang")))
            {
                connection.Open();
                var p = new DynamicParameters();
                p.Add("@id", id);
                var result = connection.Query<BallotRequest>(namestore, p, commandType: CommandType.StoredProcedure).SingleOrDefault();
                return Task.FromResult(result);
            }
        }

        public Task<IEnumerable<BallotRequest>> GetByIdUser(int id)
        {
            string namestore = "GetByIdUserBallot";
            using (var connection = new SqlConnection(configuration.GetConnectionString("ConnectCuaSang")))
            {
                connection.Open();
                var p = new DynamicParameters();
                p.Add("@id", id);
                IEnumerable<BallotRequest> result = connection.Query<BallotRequest>(namestore, p, commandType: CommandType.StoredProcedure);
                return Task.FromResult(result);
            }
        }

        public Task<IEnumerable<BallotRequest>> GetData(int page, int rows)
        {
            string namestore = "GetBallot";
            using (var connection = new SqlConnection(configuration.GetConnectionString("ConnectCuaSang")))
            {
                connection.Open();
                var p = new DynamicParameters();
                p.Add("@page", page);
                p.Add("@rows", rows);
                IEnumerable<BallotRequest> result = connection.Query<BallotRequest>(namestore, p, commandType: CommandType.StoredProcedure);
                return Task.FromResult(result);
            }
        }

        public Task<int> GetRowCount()
        {
            string namestore = "GetRowBallot";
            using (var connection = new SqlConnection(configuration.GetConnectionString("ConnectCuaSang")))
            {
                connection.Open();
                var result = connection.Query<int>(namestore, commandType: CommandType.StoredProcedure).Single();
                return Task.FromResult(result);
            }
            //Return tổng số dòng 
        }

        public Task<int> GetRowCountByUserId(int userId)
        {
            string namestore = "GetRowBallotByUserId";
            using (var connection = new SqlConnection(configuration.GetConnectionString("ConnectCuaSang")))
            {
                connection.Open();
                var p = new DynamicParameters();
                p.Add("@user_id", userId);
                var result = connection.Query<int>(namestore, commandType: CommandType.StoredProcedure).Single();
                return Task.FromResult(result);
            }
            //Return tổng số dòng theo userid
        }

        public Task<int> Modify(BallotRequest entity)
        {
            string namestore = "UpdateBallot";
            using (var connection = new SqlConnection(configuration.GetConnectionString("ConnectCuaSang")))
            {
                connection.Open();
                var p = new DynamicParameters();
                p.Add("@id", entity.Id);
                p.Add("@type", entity.Type);
                p.Add("@title", entity.Title);
                p.Add("@content", entity.Content);
                int result = connection.Execute(namestore, p, commandType: CommandType.StoredProcedure);
                return Task.FromResult(result);
                //Return số dòng thành công
            }
        }

        public Task<bool> Remove(int id)
        {
            string namestore = "RemoveBallot";
            using (var connection = new SqlConnection(configuration.GetConnectionString("ConnectCuaSang")))
            {
                connection.Open();
                var p = new DynamicParameters();
                p.Add("@id", id);
                int result = connection.Execute(namestore, p, commandType: CommandType.StoredProcedure);
                return Task.FromResult(result > 0);
            }
            //return số dòng thành công
        }

        public Task<bool> SetStatus(int id, int status)
        {
            string namestore = "UpdateStatusBallot";
            using (var connection = new SqlConnection(configuration.GetConnectionString("ConnectCuaSang")))
            {
                connection.Open();
                var p = new DynamicParameters();
                p.Add("@id", id);
                p.Add("@status", status);
                int result = connection.Execute(namestore, p, commandType: CommandType.StoredProcedure);
                return Task.FromResult(result > 0);
            }
        }
    }
}
