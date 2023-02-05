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


        private List<T> GetData<T>(string storedProcedureName) where T : new()
        {
            var connectionString = Configuration.GetConnectionString("crm");
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var command = con.CreateCommand())
                {
                    command.CommandText = "GetCustomers";
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
    }
}