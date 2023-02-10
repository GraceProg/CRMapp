using System.Data;

using CRM.Models.DBClasses;

using Microsoft.Data.SqlClient;

namespace CRM.Models.Repositories
{
    public abstract class BaseRepository : IDisposable 
    {
        protected IConfiguration Configuration { get; }

        public BaseRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected List<T> GetData<T>(string storedProcedureName, SqlParameter[]? parameters = null) where T : new()
        {
            var connectionString = Configuration.GetConnectionString("crm");
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var command = con.CreateCommand())
                {
                    if (parameters?.Any() == true)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    command.CommandText = storedProcedureName;
                    command.CommandType = CommandType.StoredProcedure;
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds);
                        return ds.ToList<T>();
                    }
                }
            }
        }
        protected int RunCommand(string storedProcedureName, SqlParameter[]? parameters = null)
        {
            var connectionString = Configuration.GetConnectionString("crm");
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var command = con.CreateCommand())
                {
                    if (parameters?.Any() == true)
                    {
                        command.Parameters.AddRange(parameters);
                    }
                    command.CommandText = storedProcedureName;
                    command.CommandType = CommandType.StoredProcedure;
                    return command.ExecuteNonQuery();
                }
            }
        }


        public void Dispose()
        {
        }


    }
}
