namespace BookFaceApp.Core.Models.Publication
{
    public class PublicationQueryModel
    {
        public int TotalPublicationsCount { get; set; }

        public List<PublicationViewModel> Publications { get; set; } = new List<PublicationViewModel>();
    }
}
