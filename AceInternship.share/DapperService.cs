﻿using Dapper;
using System.Data;
using System.Data.SqlClient;

namespace AceInternship.share
{
    public class DapperService
    {
        private readonly string _connectionString;

        public DapperService(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<M> Query<M>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var lst = db.Query<M>(query, param).ToList();
            return lst;
        }
        public M QueryFirstOrDefault<M>(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            /*var item2 = db.QueryFirstOrDefault<M>(query, param);*/
            var item = db.Query<M>(query, param).FirstOrDefault();
            return item!;
        }

        public int Execute(string query, object? param = null)
        {
            using IDbConnection db = new SqlConnection(_connectionString);
            var item = db.Execute(query, param);
            return item;
        }


    }
}
