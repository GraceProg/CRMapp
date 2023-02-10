using System.Data;
using CRM.Models.DBClasses;
using Microsoft.Data.SqlClient;

namespace CRM.Models.Repositories
{
    public class CustomerCallsRepository : BaseRepository
    {
        public CustomerCallsRepository(IConfiguration configuration) : base(configuration)
        {
        }


        public List<CustomerCall> GetAll()
        {
            using (var customersRepo = new CustomersRepository(this.Configuration))
            {
                var customers = customersRepo.GetAll();
                var customerCalls = GetData<CustomerCall>(storedProcedureName: "GetCustomerCalls");
                foreach (var customerCall in customerCalls)
                {
                    var customer = customers.FirstOrDefault(c => c.Number == customerCall.CustomerNumber);
                    customerCall.Customer = customer;
                }
                return customerCalls;
            }
        }
        public CustomerCall? Get(Guid id)
        {
            using (var customersRepo = new CustomersRepository(this.Configuration))
            {
                var parameters = new[]
                {
                    new SqlParameter() { ParameterName = "id", SqlDbType = SqlDbType.UniqueIdentifier, Value = id }
                };
                var calls = GetData<CustomerCall>(storedProcedureName: "GetCall", parameters);
                var call = calls?.FirstOrDefault();
                if(call != null)
                {
                    call.Customer = customersRepo.Get(call.CustomerNumber);
                }
                return call;
            }
        }

        public int Save(CustomerCall customer)
        {
            var parameters = new[]
            {
                new SqlParameter() { ParameterName = "Id", SqlDbType = SqlDbType.UniqueIdentifier, Value = customer.Id },
                new SqlParameter() { ParameterName = "CustomerNumber", SqlDbType = SqlDbType.Int, Value = customer.CustomerNumber },
                new SqlParameter() { ParameterName = "TimeofCall", SqlDbType = SqlDbType.DateTime, Value = customer.TimeofCall },
                new SqlParameter() { ParameterName = "Notes", SqlDbType = SqlDbType.VarChar, Value = customer.Notes },
                new SqlParameter() { ParameterName = "Subject", SqlDbType = SqlDbType.VarChar, Value = customer.Subject },
            };
            return RunCommand(storedProcedureName: "SaveCall", parameters);
        }


        public int Delete(Guid id)
        {
            var parameters = new[]
            {
                new SqlParameter() { ParameterName = "id", SqlDbType = SqlDbType.UniqueIdentifier, Value = id }
            };
            return RunCommand(storedProcedureName: "DeleteCall", parameters);
        }

    }
}
