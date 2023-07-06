

using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;
using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Server.IISIntegration;
using Microsoft.Data.SqlClient;
using System;
using BirdPlatFormEcommerce.NEntity;
using Azure.Core;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace BirdPlatFormEcommerce.Product
{
    public class ManageProductService : IManageOrderService
    {
        private readonly SwpDataBaseContext _context;

      


        public ManageProductService(SwpDataBaseContext context)
        {
            _context = context;
          
         
        }

       
    }
    }
