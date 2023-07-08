namespace BirdPlatFormEcommerce.ViewModel
{
    public class Shopreport
    {
        public int shopId { get; set; }
        public string shopname { get; set; }
        public bool IsVerifi { get; set; }
        public List<ShopreportModel> reports { get; set; }
    }
}
