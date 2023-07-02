

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Data.SqlClient;
using System;
using BirdPlatFormEcommerce.DEntity;
using Azure.Core;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace BirdPlatFormEcommerce.Product
{
    public class ManageProductService : IManageOrderService
    {
        private readonly SwpDataContext _context;

      


        public ManageProductService(SwpDataContext context)
        {
            _context = context;
          
         
        }

        public async Task<List<TbOrderDetail>> GetListOrderDetail(int orderId)
        {
          

            var tb_orderDetail = await _context.TbOrderDetails.Where( x=> x.OrderId == orderId).ToListAsync();
            var orderDetails =  tb_orderDetail.Select(tb_orderDetail => new TbOrderDetail()
  
          
            {
                OrderId = orderId,
                Id = tb_orderDetail.Id,
                ProductId = tb_orderDetail.ProductId,
                Quantity = tb_orderDetail.Quantity,
                Discount = tb_orderDetail.Discount,
                ProductPrice = tb_orderDetail.ProductPrice,
                DiscountPrice = tb_orderDetail.DiscountPrice,
                Total = tb_orderDetail.Total,
                DateOrder = tb_orderDetail.DateOrder,
                ToConfirm = tb_orderDetail.ToConfirm,



            }).ToList();
            return orderDetails;
        }
    }
    }
