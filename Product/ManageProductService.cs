

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Data.SqlClient;
using System;
using BirdPlatFormEcommerce.Etities;
using Azure.Core;
using Newtonsoft.Json;

namespace BirdPlatFormEcommerce.Product
{
    public class ManageProductService : IManageProductService
    {
        private readonly SwpContext _context;

        private readonly string datajson;
        private const string USER_CONTENT_FOLDER_NAME = "user-content";


        public ManageProductService(SwpContext context)
        {
            _context = context;
          
         
        }

        public async Task<int> AddImages(int productId, ProductImageCreateRequest request)
        {
           var image = new TbImage()
                {
                    
                        Caption = "Image",
                        CreateDate = DateTime.Now,
                        IsDefault = request.IsDefault,
                        SortOrder = request.SortOrder,
                       ProductId = productId,
                        
                    
                };
            if(request.ImageFile != null)
            {
      //          image.ImagePath = await this.SaveFile(request.ImageFile);
                image.FileSize = request.ImageFile.Length;
            }
            //}
            _context.TbImages.Add(image);
            await _context.SaveChangesAsync();
            return image.ProductId;

        }

        public async Task<int> Create(CreateProductViewModel request)
        {

      //   var product = JsonConvert.DeserializeObject<TbProduct>(datajson);
            var tbShops = new List<TbShop>();
            tbShops.Add(new TbShop()
            {

                UserId = request.UserId,
                ShopName = request.ShopName,
                CreateDate = DateTime.Now
            });
            //var findP = _context.TbProducts.Find(request.ProductId);
            //if (findP != null) throw new Exception("Product has existed!!");
            
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

                CateId = request.CateId
            };

;
          
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
                     //    ImagePath = await this.SaveFile(request.ThumbnailImage),
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

        //private async Task<string> SaveFile(IFormFile file)
        //{
        //    var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim();
        //    var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
        //    await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
        //    return "/"+ USER_CONTENT_FOLDER_NAME + "/"+ fileName;
        //}
    }
}