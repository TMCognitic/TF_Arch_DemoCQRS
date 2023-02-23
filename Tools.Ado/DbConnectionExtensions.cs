using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Reflection;

namespace Tools.Ado
{
    public static class DbConnectionExtensions
    {
        public static object? ExecuteScalar(this DbConnection dbConnection, string query, bool isStoredProcedure, object? parameters)
        {
            ArgumentNullException.ThrowIfNull(dbConnection);

            using (DbCommand dbCommand = CreateCommand(dbConnection, query, isStoredProcedure, parameters))
            {
                dbConnection.Open();
                object? result = dbCommand.ExecuteScalar();
                return result is DBNull ? null : result;
            }
        }

        public static int ExecuteNonQuery(this DbConnection dbConnection, string query, bool isStoredProcedure, object? parameters)
        {
            ArgumentNullException.ThrowIfNull(dbConnection);

            using (DbCommand dbCommand = CreateCommand(dbConnection, query, isStoredProcedure, parameters))
            {
                dbConnection.Open();
                return dbCommand.ExecuteNonQuery();
            }
        }

        public static IEnumerable<TResult> ExecuteReader<TResult>(this DbConnection dbConnection, string query, bool isStoredProcedure, Func<IDataRecord, TResult> selector, object? parameters)
        {
            ArgumentNullException.ThrowIfNull(dbConnection);
            ArgumentNullException.ThrowIfNull(selector);

            using (DbCommand dbCommand = CreateCommand(dbConnection, query, isStoredProcedure, parameters))
            {
                dbConnection.Open();
                using(DbDataReader reader = dbCommand.ExecuteReader())
                {
                    while(reader.Read())
                    {
                        yield return selector(reader);
                    }
                }
            }
        }

        private static DbCommand CreateCommand(DbConnection dbConnection, string query, bool isStoredProcedure, object? parameters)
        {
            DbCommand dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = query;

            if(isStoredProcedure)
            {
                dbCommand.CommandType = CommandType.StoredProcedure;
            }

            if(parameters is not null)
            {
                Type type = parameters.GetType();

                foreach(PropertyInfo property in type.GetProperties().Where(p => p.CanRead))
                {
                    DbParameter dbParameter = dbCommand.CreateParameter();
                    dbParameter.ParameterName = property.Name;
                    dbParameter.Value = property.GetMethod!.Invoke(parameters, null) ?? DBNull.Value;
                    dbCommand.Parameters.Add(dbParameter);
                }
            }

            return dbCommand;
        }
    }
}