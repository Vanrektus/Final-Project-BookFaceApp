using BookFaceApp.Core.Contracts;
using BookFaceApp.Core.Models.Group;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookFaceApp.Core.Services
{
    public class RequestService : IRequestService
    {
        public Task<IEnumerable<GroupViewModel>> GetAllRequestsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
