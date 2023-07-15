namespace BirdPlatForm.ViewModel
{
    public class FeedbackModel
    {


        public int? ProductId { get; set; }
        public int? Rate { get; set; }

        public string   Detail { get; set; }
        public int? OrderDetailId { get; set; }
        public IFormFile[] ImageFile { get; set; }
    }
    public class FeedbackReponse
    {
        public int ProductId { get; set; }
        public int Rate { get; set; }
        public string? Detail { get; set; }
        public string UserName { get; set; }
        public List<string?> imgFeedback { get; set; }
        public string imgAvatar { get; set; }
        public DateTime CreateDate { get; set; }

    }
    public class FeedbackUser
    {
        public int feedbackID { get; set; }
        public string Username { get; set; }
        public int rate { get; set; }
        public string Detail { get; set; }
        public DateTime CreateDate { get; set; }
        public List<string> imgFeedback { get; set; }
        public string imgAvatar { get; set; }
        public int productId { get; set; }
        public int Quantity { get; set; }
        public string productName { get; set; }
        public string imgProduct { get; set; }
    }
}
