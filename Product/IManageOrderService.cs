

using BirdPlatFormEcommerce.NEntity;

namespace BirdPlatFormEcommerce.Product
{
    public interface IManageOrderService
    {

      

        Task<List<TbOrderDetail>> GetListOrderDetail(int orderId);
    }
}



