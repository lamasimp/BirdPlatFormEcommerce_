using BirdPlatFormEcommerce.Entities;
using BirdPlatFormEcommerce.FileService;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace BirdPlatFormEcommerce.Product
{
    public class ManageProductService : IManageProductService
    {
        private readonly SwpContext _context;
        private readonly IStorageService _storageService;
        
        public ManageProductService(SwpContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
         
        }

        public Task<int> AddImages(int productId, List<IFormFile> files)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Create(CreateProductViewModel request)
        {
            var product = new TbProduct()
            {
                Name = request.ProductName,
             
                Price = request.Price,
                DiscountPercent = request.DiscountPercent,
                SoldPrice = (int)Math.Round((decimal)(request.Price - request.Price / 100 * request.DiscountPercent)),
                Decription = request.Decription,
                Detail = request.Detail,
              
                Quantity = request.Quantity,
                ShopId = request.ShopId,
                CateId = request.CateId,
              

                //TbPosts = new List<TbPost> { 
                //    new TbPost()
                //    {
                //          CreateDate =  DateTime.Now
                //    }
                //}
       
            };

            //Save image
            if (request.ThumbnailImage != null)
            {
                product.TbImages = new List<TbImage>()
                {
                    new TbImage()
                    {
                        Caption = "Thumbnail image",
                        CreateDate = DateTime.Now,
                         FileSize = request.ThumbnailImage.Length,
                         ImagePath = await this.SaveFile(request.ThumbnailImage),
                        IsDefault = true,
                        SortOrder =1
                    }
                };
            }
                _context.TbProducts.Add(product);
                 await _context.SaveChangesAsync();
            return product.ProductId;
            }

       

        public Task<List<TbImage>> GetListImage(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveImages(int imageId)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateImages(int imageId, string caption, bool isDefault)
        {
            throw new NotImplementedException();
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }
    }
}