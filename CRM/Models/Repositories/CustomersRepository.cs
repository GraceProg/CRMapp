using System.Data;

using CRM.Models.DBClasses;

using Microsoft.Data.SqlClient;

namespace CRM.Models.Repositories
{
    public class CustomersRepository : BaseRepository
    {
        public CustomersRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public List<Customer> GetAll()
        {
            var customers = GetData<Customer>(storedProcedureName: "GetCustomers");
            return customers;
        }

        public Customer Get(int customerNumber)
        {
            var parameters = new[]
            {
                new SqlParameter() { ParameterName = "CustomerNumber", SqlDbType = SqlDbType.Int, Value = customerNumber }
            };
            var customers = GetData<Customer>(storedProcedureName: "GetCustomer", parameters);
            return customers.FirstOrDefault();
        }

        public int Save(Customer customer)
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
                new SqlParameter() { ParameterName = "UserId", SqlDbType = SqlDbType.VarChar, Value = string.IsNullOrWhiteSpace(customer.UserId) ? "xx" : customer.UserId },
            };
            return RunCommand(storedProcedureName: "SaveCustomer", parameters);
        }


        public int Delete(int customerNumber)
        {
            var parameters = new[]
            {
                new SqlParameter() { ParameterName = "CustomerNumber", SqlDbType = SqlDbType.Int, Value = customerNumber }
            };
            return RunCommand(storedProcedureName: "DeleteCustomer", parameters);
        }

    }
}
