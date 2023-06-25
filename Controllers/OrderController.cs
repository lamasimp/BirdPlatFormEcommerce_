using AutoMapper;
using BirdPlatFormEcommerce.Entity;
using BirdPlatFormEcommerce.Helper.Mail;
using BirdPlatFormEcommerce.Order;
using BirdPlatFormEcommerce.Order.Requests;
using BirdPlatFormEcommerce.Order.Responses;
using BirdPlatFormEcommerce.Payment;
using BirdPlatFormEcommerce.Payment.Requests;
using BirdPlatFormEcommerce.Payment.Responses;
using BirdPlatFormEcommerce.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.X9;
using System.Net;

namespace BirdPlatFormEcommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderController> _logger;
        private readonly IVnPayService _vnPayService;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;
        private readonly SwpContext _context;

        public OrderController(IOrderService orderService, IMapper mapper, ILogger<OrderController> logger, IVnPayService vnPayService, IConfiguration configuration, IMailService mailService, SwpContext swp)
        {
            _orderService = orderService;
            _mapper = mapper;
            _logger = logger;
            _vnPayService = vnPayService;
            _configuration = configuration;
            _mailService = mailService;
            _context = swp;
        }

        [HttpPost("Create")]

        public async Task<ActionResult<OrderRespponse>> CreateOrder([FromBody] CreateOrderModel request)
        {
            var userId = User.FindFirst("UserId")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var order = await _orderService.CreateOrder(Int32.Parse(userId), request);
            var response = _mapper.Map<OrderRespponse>(order);

            return Ok(response);
        }

        [HttpGet("{id:int}")]
        [Authorize]
        public async Task<ActionResult<OrderRespponse>> GetOrder([FromRoute] int id)
        {
            var order = await _orderService.GetOrder(id);
            if (order == null)
            {
                return NotFound();
            }

            var response = _mapper.Map<OrderRespponse>(order);
            return Ok(response);
        }


        [HttpPost("{id:int}/Pay/")]
        [Authorize]
        public async Task<ActionResult<PaymentResponse>> CreatePayment([FromRoute] int id, [FromBody] PayOrderModel request)
        {
            var order = await _orderService.GetOrder(id);
            if (order == null || order.Status == true)
            {
                return NotFound("Order not found");
            }

            var paymentUrl = await _orderService.PayOrder(order, request.Method);

            // send confirmation email
            string listProductHtml = "";
            foreach (TbOrderDetail item in order.TbOrderDetails)
            {
                listProductHtml += $"<li>{item.Product?.Name} - <del>{item.ProductPrice:n0}</del> $ {item.DiscountPrice:n0} $ - x{item.Quantity}</li>";
            }
            var emailBody = $@"<div><h3>THÔNG TIN ĐƠN HÀNG CỦA BẠN </h3> 
                                    <ul>{listProductHtml} </ul>
                                <div>
                                    <span>Tổng tiền: </span> <strong>{order.TotalPrice:n0} VND</strong>
                                </div>
                                <p>Xin trân trọng cảm ơn</p>
                                </div>";

            var mailRequest = new MailRequest()
            {
                ToEmail = order.User.Email ?? string.Empty,
                Subject = "[BIRD TRADING PALTFORM] XÁC NHẬN ĐƠN HÀNG",
                Body = emailBody
            };

            await _mailService.SendEmailAsync(mailRequest);

            var response = _mapper.Map<PaymentResponse>(order.Payment);
            response.PaymentUrl = paymentUrl;

            return Ok(response);
        }

        [HttpGet("PaymentCallback/{paymentId:int}")]
        public async Task<ActionResult> PaymentCallback([FromRoute] int paymentId, [FromQuery] VnPaymentCallbackModel request)
        {
            if (!request.Success)
            {
                return Redirect(_configuration["Payment:Failed"]);
            }

            var order = await _orderService.GetOrderByPaymentId(paymentId);
            if (order == null || order.Status == true)
            {
                return NotFound("Order not found");
            }

            await _orderService.CompleteOrder(order);

            return Redirect(_configuration["Payment:SuccessUrl"]);

        }
        [HttpGet("{userId}")]
        public IActionResult GetOrders(int userId)
        {
            var query = from o in _context.TbOrders
                        where o.UserId == userId && o.Status == false && o.Payment.PaymentMethod == "Vnpay"
                        join od in _context.TbOrderDetails on o.OrderId equals od.OrderId
                        join p in _context.TbProducts on od.ProductId equals p.ProductId
                        join s in _context.TbShops on p.ShopId equals s.ShopId
                        join i in _context.TbImages on p.ProductId equals i.ProductId
                        where i.IsDefault == true
                        select new Inforproduct
                        {
                            OrderID = o.OrderId,
                            ProductID = od.ProductId ,
                            ProductName = p.Name,
                            Quantity = (int)od.Quantity,
                            SubTotal = (decimal)od.SubTotal,
                            ShopID = (int)p.ShopId,
                            ShopName = s.ShopName,
                            ImagePath = i.ImagePath
                        };

            var orders = query.ToList();
            return Ok(orders);
        }

    }
}


