using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookFaceApp.Core.Models.Request
{
    public class RequestViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Status { get; set; } = null!;

        public bool IsActive { get; set; } = true;
    }
}
