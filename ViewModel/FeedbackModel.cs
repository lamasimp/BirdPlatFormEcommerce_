namespace BirdPlatForm.ViewModel
{
    public class FeedbackModel
    {


        public int ProductId { get; set; }



        public int Rate { get; set; }

        public string? Detail { get; set; }
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

    }
}
