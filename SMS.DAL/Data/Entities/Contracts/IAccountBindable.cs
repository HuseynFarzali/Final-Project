using SMS.DAL.Data.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DAL.Data.Entities.Contracts
{
    public interface IAccountBindable
    {
        public int? AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
        public bool Bound { get; set; }
    }
}
