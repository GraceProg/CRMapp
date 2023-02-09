using System.Data;

using CRM.Models.DBClasses;

using Microsoft.Data.SqlClient;

namespace CRM.Models
{
    public class Repository : IDisposable
    {
        public IConfiguration Configuration { get; }

        public Repository(IConfiguration  configuration)
        {
            Configuration = configuration;
        }
        public void Dispose()
        {
        }

        private List<T> GetData<T>(string storedProcedureName, SqlParameter[]? parameters = null) where T : new()
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
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    using (var adapter = new SqlDataAdapter(command))
                    {
                        var ds = new DataSet();
                        adapter.Fill(ds);
                        return ds.ToList<T>();
                    }
                }
            }
        }
        private int RunCommand(string storedProcedureName, SqlParameter[]? parameters = null)
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
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    return command.ExecuteNonQuery();
                }
            }
        }


        public List<Customer> GetCustomers()
        {
            var customers = GetData<Customer>(storedProcedureName: "GetCustomers");
            return customers;
        }
        public List<CustomerCall> GetCustomerCalls()
        {
            var customers = GetCustomers();
            var customerCalls = GetData<CustomerCall>(storedProcedureName: "GetCustomerCalls");
            foreach(var customerCall in customerCalls)
            {
                var customer = customers.FirstOrDefault(c => c.Number == customerCall.CustomerNumber);
                customerCall.CustomerName = customer?.Name;
            }
            return customerCalls;
        }


        public Customer GetCustomer(int customerNumber)
        {
            var parameters = new[]
            {
                new SqlParameter() { ParameterName = "CustomerNumber", SqlDbType = SqlDbType.Int, Value = customerNumber }
            };
            var customers = GetData<Customer>(storedProcedureName: "GetCustomer", parameters);
            return customers.FirstOrDefault();
        }

        public void SaveCustomer(Customer customer)
        {
            var parameters = new[]
            {
                new SqlParameter() { ParameterName = "Number", SqlDbType = SqlDbType.Int, Value = customer.Number },
                new SqlParameter() { ParameterName = "Name", SqlDbType = SqlDbType.VarChar, Value = customer.Name },
                new SqlParameter() { ParameterName = "SurName", SqlDbType = SqlDbType.VarChar, Value = customer.SurName },
                new SqlParameter() { ParameterName = "Address", SqlDbType = SqlDbType.VarChar, Value = customer.Address },
                new SqlParameter() { ParameterName = "PostalCode", SqlDbType = SqlDbType.VarChar, Value = customer.PostalCode },
                new SqlParameter() { ParameterName = "Country", SqlDbType = SqlDbType.VarChar, Value = customer.Country },
                new SqlParameter() { ParameterName = "DateOfBirth", SqlDbType = SqlDbType.DateTime, Value = customer.DateOfBirth },
                new SqlParameter() { ParameterName = "UserId", SqlDbType = SqlDbType.Int, Value = customer.UserId },
            };
            var customers = RunCommand(storedProcedureName: "SaveCustomer", parameters);
        }
    }
}
