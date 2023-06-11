using BirdPlatFormEcommerce.Entities;

namespace BirdPlatFormEcommerce.Product
{
    public interface IManageProductService
    {
        Task<int> Create(CreateProductViewModel request);
       
        Task<int> AddImages(int productId, List<IFormFile> files);

        Task<int> RemoveImages(int imageId);

        Task<int> UpdateImages(int imageId, string caption, bool isDefault);

        Task<List<TbImage>> GetListImage(int productId);
    }
}
