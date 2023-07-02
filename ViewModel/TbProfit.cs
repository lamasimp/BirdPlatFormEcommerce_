namespace BirdPlatFormEcommerce.ViewModel
{
    public class TbProfit
    {
        public int ShopId { get; internal set; }
        public DateTime Orderdate { get; internal set; }
        public int OrderId { get; set; }
        public decimal? Total { get; internal set; }
    }
}
