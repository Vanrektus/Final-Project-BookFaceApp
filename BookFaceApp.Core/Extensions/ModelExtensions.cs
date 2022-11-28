using BookFaceApp.Core.Contracts;

namespace BookFaceApp.Core.Extensions
{
    public static class ModelExtensions
    {
        public static string GetInformation(this IPublicationModel publication) 
            => publication.Title.Replace(" ", "-");
    }
}
