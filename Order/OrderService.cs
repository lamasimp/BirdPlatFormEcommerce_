using BirdPlatFormEcommerce.Entity;
using BirdPlatFormEcommerce.Helper.Mail;
using BirdPlatFormEcommerce.Order.Requests;
using BirdPlatFormEcommerce.Payment;
using Microsoft.EntityFrameworkCore;

namespace BirdPlatFormEcommerce.Order
{
    public class OrderService : IOrderService
    {
        private readonly SwpContext _context;
        private readonly IVnPayService _vnPayService;
        private readonly IMailService _mailService;

        public OrderService(SwpContext context, IVnPayService vnPayService, IMailService mailService)
        {
            _context = context;
            _vnPayService = vnPayService;
            _mailService = mailService;
        }
        public async Task<TbOrder> CompleteOrder(TbOrder order)
        {
            // Update product quantity
            var orderDetails = order.TbOrderDetails;

            foreach (var item in orderDetails)
            {
                var product = _context.TbProducts.FirstOrDefault(prod => prod.ProductId == item.ProductId);
                if (product == null)
                {
                    throw new Exception("Product not found");
                }
                if (product.Quantity < item.Quantity)
                {
                    throw new Exception($"Product {product.ProductId} is out of stock");
                }

                product.Quantity -= item.Quantity;
                product.QuantitySold += item.Quantity;

                _context.TbProducts.Update(product);

                // Create profit
                var profit = new TbProfit
                {
                    OrderId = order.OrderId,
                    Total = item.SubTotal,
                    ShopId = (int)product.ShopId,
                    Orderdate = order.OrderDate,
                };

                _context.TbProfits.Update(profit);
            }

            order.Status = true;
            _context.TbOrders.Update(order);

            // save changes
            _context.SaveChanges();

            // send email
            string listProductHtml = "";
            foreach (TbOrderDetail item in order.TbOrderDetails)
            {
                listProductHtml += $"<li>{item.Product?.Name} - <del>{item.ProductPrice:n0}</del> VND {item.DiscountPrice:n0} VND - x{item.Quantity}</li>";
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
                Subject = "[BIRD TRADING PALTFORM] THANH TOÁN THÀNH CÔNG",
                Body = emailBody
            };

            await _mailService.SendEmailAsync(mailRequest);

            return order;
        }

        public async Task<TbOrder> CreateOrder(int userId, CreateOrderModel orderModel)
        {

            // validate user
            var user = await _context.TbUsers.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var items = new List<TbOrderDetail>();
            foreach (var requestItem in orderModel.Items)
            {
                var quantity = requestItem.Quantity;
                var productId = requestItem.ProductId;

                var product = await _context.TbProducts.FirstOrDefaultAsync(p => p.ProductId == productId);
                if (product == null)
                {
                    throw new Exception($"Product {productId} not found");
                }

                if (product.Quantity - product.QuantitySold < quantity)
                {
                    throw new Exception($"Product {productId} is out of stock");
                }

                var orderItem = new TbOrderDetail
                {
                    ProductId = productId,
                    Quantity = quantity,
                    ProductPrice = product.Price,
                    DiscountPrice = (1 - product.DiscountPercent * (decimal)0.01) * product.Price
                };

                orderItem.SubTotal = orderItem.DiscountPrice * quantity;

                items.Add(orderItem);
            }

            var order = new TbOrder()
            {
                UserId = userId,
                Status = false,
                OrderDate = DateTime.Now,
                Note = orderModel.Note,
                AddressID = orderModel.AddressID,
                TbOrderDetails = items,
                TotalPrice = items.Sum(item => item.SubTotal ?? 0)
            };


            await _context.TbOrders.AddAsync(order);
            await _context.SaveChangesAsync();

            return order;
        }

        public async Task<TbOrder?> GetOrder(int orderId)
        {
            return await _context.TbOrders
                .Include(order => order.TbOrderDetails)
                .ThenInclude(orderItem => orderItem.Product)
                .Include(order => order.Payment)
                .Include(order => order.User)
                .FirstOrDefaultAsync(order => order.OrderId == orderId);
        }

        public async Task<TbOrder?> GetOrderByPaymentId(int paymentId)
        {
            return await _context.TbOrders.Include(order => order.TbOrderDetails)
                            .ThenInclude(orderItem => orderItem.Product)
                            .Include(order => order.Payment)
                            .Include(order => order.User)
                            .FirstOrDefaultAsync(order => order.PaymentId == paymentId);
        }

        public async Task<string?> PayOrder(TbOrder order, PaymentMethod method)
        {

            var payment = new TbPayment()
            {
                UserId = order.UserId,
                PaymentMethod = method.ToString(),
                PaymentDate = DateTime.Now,
                Amount = order.TotalPrice
            };

            order.Payment = payment;
            _context.TbOrders.Update(order);
            await _context.SaveChangesAsync();

            var paymentUrl = PaymentMethod.VnPay.Equals(method)
              ? _vnPayService.CreatePaymentUrl(payment)
              : null;

            return paymentUrl;
        }

        public async Task<TbOrder> UpdateOrder(TbOrder order)
        {
            _context.TbOrders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }
    }
}
