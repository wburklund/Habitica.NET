using Habitica.NET.Data.Model;
using System.Collections.Generic;

namespace Habitica.NET.Data.Response
{
    public class HabiticaResponse<T>
    {
        public T Data { get; set; }
        public IEnumerable<Notification> Notifications { get; set; }
        public bool Success { get; set; }
    }
}
