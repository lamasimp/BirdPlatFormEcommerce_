

using BirdPlatFormEcommerce.Entity;

namespace BirdPlatFormEcommerce.Product
{
    public interface IManageProductService
    {
        Task<int> Create(CreateProductViewModel request);
       
        Task<int> AddImages(int productId, ProductImageCreateRequest request);

        Task<int> RemoveImages(int imageId);

        Task<int> UpdateImages(int imageId, string caption, bool isDefault);

        Task<List<TbImage>> GetListImage(int productId);
    }
}
