

using BirdPlatFormEcommerce.DEntity;

namespace BirdPlatFormEcommerce.Product
{
    public interface IManageOrderService
    {

      

        Task<List<TbOrderDetail>> GetListOrderDetail(int orderId);
    }
}



