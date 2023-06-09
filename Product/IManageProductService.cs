namespace BirdPlatFormEcommerce.Product
{
    public interface IManageProductService
    {
        Task<int> Create(CreateProductModel request);
    }
}
