using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SMS.BLL.Data_Transfer_Objects;
using SMS.BLL.Services.Contracts;
using System.Runtime.CompilerServices;

namespace SMS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessageCrudService _messageCrudService;

        public MessagesController(IMessageCrudService messageCrudService)
        {
            _messageCrudService = messageCrudService;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var messages = await _messageCrudService.GetRange(
                includePathQuery: query => query
                    .Include(message => message.AppUser)!
                    .Include(message => message.Chat)!);

            return Ok(messages);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get([FromRoute] int id)
        {
            var message = await _messageCrudService.Get(
                id: id,
                includePathQuery: query => query
                    .Include(message => message.AppUser)!
                    .Include(message => message.Chat)!);

            return Ok(message);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MessageCreateDto dto)
        {
            if (ModelState.IsValid is false)
                return BadRequest(ModelState);

            await _messageCrudService.Add(dto);

            var createdMessage = (await _messageCrudService.GetRange(
                includePathQuery: query => query
                    .Include(message => message.AppUser)!
                    .Include(message => message.Chat)!,
                
                predicate: message => message.AppUserId == dto.AppUserId && message.ChatId == dto.ChatId && message.MessageContent == dto.MessageContent)).Single();

            return Ok(createdMessage);
        }

        [HttpPost("all")]
        public async Task<IActionResult> Post([FromBody] IEnumerable<MessageCreateDto> dtos)
        {
            if (ModelState.IsValid is false)
                return BadRequest(ModelState);

            await _messageCrudService.AddRange(dtos);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put([FromRoute] int id, [FromBody] MessageUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _messageCrudService.Update(id, dto);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            await _messageCrudService.Delete(id);
            return Ok();
        }
    }
}
