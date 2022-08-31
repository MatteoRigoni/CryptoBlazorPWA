using GloboCrypto.WebAPI.Services.Events;
using GloboCrypto.WebAPI.Services.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GloboCrypto.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<IEnumerable<EventLogEntry>> DumpLogs()
        {
            return await _eventService.GetAllEvents();
        }
    }
}
