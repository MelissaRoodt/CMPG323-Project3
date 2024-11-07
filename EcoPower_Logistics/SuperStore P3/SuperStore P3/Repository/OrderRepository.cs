using Data;
using Microsoft.Extensions.Options;
using Models;

namespace EcoPower_Logistics.Repository
{
    public class OrderRepository: GenericRepository<Order>, IOrderRepository
    {
        public OrderRepository(SuperStoreContext context) : base(context)
        {
        }
        //Will auto implement additional method defined in IOrderRepository here
    }
}
