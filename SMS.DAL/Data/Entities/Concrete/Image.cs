using SMS.DAL.Data.Entities.Abstract;
using SMS.DAL.Data.Entities.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.DAL.Data.Entities.Concrete
{
    public class Image : Entity
    {
        public string? ImageName { get; set; }
        public string? ImageDescription { get; set; }
        public string ContentType { get; set; }
        public byte[] Data { get; set; }
    }
}
