using BookFaceApp.Core.Models.Publication;

namespace BookFaceApp.Models
{
    public class AllPublicationsQueryModel
    {
        public const int PublicationsPerPage = 3;

        public string? Category { get; set; }

        public string? SearchTerm { get; set; }

        public PublicationSorting Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalPublicationsCount { get; set; }

        public IEnumerable<string> Categories { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<PublicationViewModel> Publications { get; set; } = Enumerable.Empty<PublicationViewModel>();
    }
}
