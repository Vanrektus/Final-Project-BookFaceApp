using BookFaceApp.Core.Models.Profile;
using BookFaceApp.Core.Models.Publication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookFaceApp.Core.Contracts
{
    public interface IProfileService
    {
        Task<IEnumerable<PublicationViewModel>> GetMyProfilePublicationsAsync(string userId);

        Task<IEnumerable<PublicationViewModel>> GetUserProfilePublicationsAsync(string username);

        Task<IEnumerable<AllUsersViewModel>> GetAllUsersAsync();
    }
}
