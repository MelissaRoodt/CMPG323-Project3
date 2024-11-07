using Data;
using Models;

namespace EcoPower_Logistics.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(SuperStoreContext context) : base(context)
        {
        }
        //Will auto implement additional method defined in IProductRepository here
    }
}
