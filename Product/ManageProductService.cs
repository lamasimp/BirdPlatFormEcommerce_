
using BirdPlatFormEcommerce.FileService;
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
                image.ImagePath = await this.SaveFile(request.ImageFile);
                image.FileSize = request.ImageFile.Length;
            }
            //}
            _context.TbImages.Add(image);
            await _context.SaveChangesAsync();
            return image.ProductId;

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


                Shop = new TbShop()

                {
                    ShopName = request.ShopName,
                    CreateDate =  DateTime.Now
                    }
                

            };
            //_context.TbProducts.Add(product);
            //await _context.SaveChangesAsync();

            //          var  productId = product.ProductId;
            //        var thumbnailImageDto = images.FirstOrDefault();

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

            //var thumbnailImage = new TbImage
            //{
            //    ProductId = productId,
            //    ImagePath = thumbnailImageDto.ImagePath,
            //    FileSize = thumbnailImageDto.FileSize,
            //    Caption = thumbnailImageDto.Caption,
            //    IsDefault = true
            //};

            //_context.TbImages.Add(thumbnailImage);
            // await _context.SaveChangesAsync();

            //var otherImages = images.Skip(1).Take(5);


            //foreach(var imageDto in otherImages )
            //{
            //    var image = new TbImage
            //    {

            //        ProductId = productId,
            //        ImagePath = imageDto.ImagePath,
            //        FileSize = imageDto.FileSize,
            //        Caption = imageDto.Caption,
            //        IsDefault = true
            //    };

            //    _context.TbImages.Add(image);
            //}
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