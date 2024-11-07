using Data;
using Microsoft.Extensions.Options;
using Models;
namespace EcoPower_Logistics.Repository
{
    public class CustomerRepository: GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(SuperStoreContext context) : base(context)
        {
        }

        //Will auto implement additional method defined in ICustomer here

    }
}
