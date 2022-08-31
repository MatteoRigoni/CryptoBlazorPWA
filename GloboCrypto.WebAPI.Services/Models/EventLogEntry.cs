using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GloboCrypto.WebAPI.Services.Models
{
    public class EventLogEntry
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string UserId { get; set; }
        public string CoinId { get; set; }
        public EventLogEntryType EventType { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
    }
}
