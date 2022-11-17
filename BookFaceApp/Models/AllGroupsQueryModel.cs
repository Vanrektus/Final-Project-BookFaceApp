using BookFaceApp.Core.Models.Group;
using BookFaceApp.Core.Models.Publication;

namespace BookFaceApp.Models
{
    public class AllGroupsQueryModel
    {
        public const int GroupsPerPage = 3;

        public string? Category { get; set; }

        public string? SearchTerm { get; set; }

        public GroupSorting Sorting { get; set; }

        public int CurrentPage { get; set; } = 1;

        public int TotalGroupsCount { get; set; }

        public IEnumerable<string> Categories { get; set; } = Enumerable.Empty<string>();

        public IEnumerable<GroupViewModel> Groups { get; set; } = Enumerable.Empty<GroupViewModel>();
    }
}
