namespace BirdPlatFormEcommerce.Product
{
    public interface IHomeViewProductService
    {
        Task<List<HomeViewProductModel>> GetAllByQuantitySold();
        Task<List<HomeViewProductModel>> GetProductByRateAndQuantitySold();

//        Task<DetailProductViewModel> GetProductById(int id);

        Task<List<HomeViewProductModel>> GetProductByShopId(int shopId);
    }
}
