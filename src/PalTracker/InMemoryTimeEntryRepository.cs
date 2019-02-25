using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalTracker
{
    public class InMemoryTimeEntryRepository : ITimeEntryRepository
    {
        private readonly IDictionary<long, TimeEntry> _timeEntries = new Dictionary<long, TimeEntry>();
        //private TimeEntry entered;
        private long entryId = 0;

        public bool Contains(long id)
        {
            return _timeEntries.ContainsKey(id);
        }

        public TimeEntry Create(TimeEntry timeEntry)
        {
            timeEntry.Id = ++entryId;
            _timeEntries.Add(entryId, timeEntry);
            return timeEntry;
        }

        public void Delete(long id)
        {
            _timeEntries.Remove(id);
        }

        public TimeEntry Find(long id)
        {
            return _timeEntries[id];
        }

        public IEnumerable<TimeEntry> List()
        {
            return _timeEntries.Values;
        }

        public TimeEntry Update(long id, TimeEntry timeEntry)
        {
            timeEntry.Id = id;
            _timeEntries[id] = timeEntry;
            return timeEntry;
        }
    }
}
