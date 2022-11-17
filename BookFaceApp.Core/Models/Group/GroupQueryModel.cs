namespace BookFaceApp.Core.Models.Group
{
    public class GroupQueryModel
    {
        public int TotalGroupsCount { get; set; }

        public List<GroupViewModel> Groups { get; set; } = new List<GroupViewModel>();
    }
}
