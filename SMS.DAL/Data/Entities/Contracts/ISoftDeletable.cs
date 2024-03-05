using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DAL.Data.Entities.Contracts
{
    public interface ISoftDeletable
    {
        public bool IsDeleted { get; set; }
    }
}
