using AutoMapper;
using SMS.BLL.Data_Transfer_Objects;
using SMS.BLL.Services.Contracts;
using SMS.BLL.Services.Main;
using SMS.DAL.Data.Database_Context;
using SMS.DAL.Data.Entities.Concrete;
using SMS.DAL.Repositories.Main.Contracts;
using SMS.WebTools.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.BLL.Services
{
    [Injectible(
        Lifetime = Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped,
        Implements = typeof(IMessageCrudService))]
    public class MessageCrudServiceImpl : CoreCrudServiceImpl<Message, MessageDto, MessageCreateDto, MessageUpdateDto, CoreDbContext>, IMessageCrudService
    {
        public MessageCrudServiceImpl(IMapper mapper, IRepository<Message, CoreDbContext> repository) : base(mapper, repository)
        {
        }
    }
}
