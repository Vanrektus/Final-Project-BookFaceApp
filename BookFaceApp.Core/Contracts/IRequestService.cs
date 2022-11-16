using BookFaceApp.Core.Models.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookFaceApp.Core.Contracts
{
    public interface IRequestService
    {
        Task<IEnumerable<GroupViewModel>> GetAllRequestsAsync();
    }
}
