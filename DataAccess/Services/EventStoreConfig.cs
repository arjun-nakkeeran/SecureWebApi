using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Services
{
    internal class EventStoreConfig
    {
        public static string EventStoreFilePath = Path.Combine(Path.GetTempPath(), "EventStore.txt");

    }
}
