using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DAL.Data.Entities.Contracts
{
    public interface IAuthorStample
    {
        public string? CreatedBy { get; set; }
        public string? LastUpdatedBy { get; set; }
    }
}
