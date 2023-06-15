

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Data.SqlClient;
using System;
using BirdPlatFormEcommerce.Entity;
using Azure.Core;
using Newtonsoft.Json;

namespace BirdPlatFormEcommerce.Product
{
    public class ManageProductService : IManageProductService
    {
        private readonly SwpContext _context;

      


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

    
            var tbShops = new List<TbShop>();
            tbShops.Add(new TbShop()
            {

                UserId = request.UserId,
                ShopName = request.ShopName,
                CreateDate = DateTime.Now
            });
            
            
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

            
            _context.TbProducts.Add(product);
            await _context.SaveChangesAsync();
            return product.ProductId;
            }

        public async Task<int> Delete(int productId)
        {
            var product = await _context.TbProducts.FindAsync(productId);
            if (product == null) throw new Exception("Can not find product.");
            _context.TbProducts.Remove(product);
           return await _context.SaveChangesAsync();
            
        }

        public Task<List<TbImage>> GetListImage(int productId)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveImages(int imageId)
        {
            throw new NotImplementedException();
        }

        public async Task<int> Update(UpdateProductViewModel request)
        {
            var product = await _context.TbProducts.FindAsync(request.ProductId);
          
            if (product == null) throw new Exception("Can not found.");
            product.Name = request.Name;
            product.Price = request.Price;
            product.DiscountPercent = request.DiscountPercent;
            product.SoldPrice = (int)Math.Round((decimal)(product.Price - request.Price / (100 * request.DiscountPercent)));
            product.Status = request.Status;
            product.Decription = request.Decription;
            product.Detail = request.Detail;
            return await _context.SaveChangesAsync();
            
        }

        public Task<int> UpdateImages(int imageId, string caption, bool isDefault)
        {
            throw new NotImplementedException();
        }

        
    }
}