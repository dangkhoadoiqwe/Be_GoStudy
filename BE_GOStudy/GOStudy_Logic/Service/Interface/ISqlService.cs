using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GO_Study_Logic.Service.Interface
{
    public interface IConnectionService
    {
        string? Datebase { get; }
    }
    public interface ISqlService
    {
        SqlParameter CreateOutputParameter(string name, SqlDbType type);
        SqlParameter CreateOutputParameter(string name, SqlDbType type, int size);

        (int, string) ExecuteNonQuery(string connectionString, string sqlObjectName,
            params SqlParameter[] parameters);
        Task<(int, string)> ExecuteNonQueryAsync(string connectionString, string sqlObjectName,
            params SqlParameter[] parameters);
        (DataTable, string) FillDataTable(string connectionString, string sqlObjectName,
            params SqlParameter[] parameters);
        Task<(DataTable, string)> FillDataTableAsync(string connectionString, string sqlObjectName,
            params SqlParameter[] parameters);
    }
}