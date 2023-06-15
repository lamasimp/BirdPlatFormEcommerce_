namespace BirdPlatFormEcommerce.Product
{
    public interface IHomeViewProductService
    {
        
        Task<List<HomeViewProductModel>> GetProductByRateAndQuantitySold();

        Task<DetailProductViewModel> GetProductById(int id);

        Task<List<HomeViewProductModel>> GetProductByShopId(int shopId);

        Task<List<HomeViewProductModel>> GetAllProduct();

        Task<DetailShopViewProduct> GetShopById(int id);
    }
}
