using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace PalTracker
{
    public class MySqlTimeEntryRepository : ITimeEntryRepository
    {
        private TimeEntryContext _context;

        public MySqlTimeEntryRepository(TimeEntryContext context)
        {
            this._context = context;
        }

        public bool Contains(long id)
        {
            return (FindRecord(id) != null);
        }

        public TimeEntry Create(TimeEntry timeEntry)
        {
            TimeEntryRecord record = _context.TimeEntryRecords.Add(timeEntry.ToRecord()).Entity;
            _context.SaveChanges();
            return record.ToEntity();
        }

        public void Delete(long id)
        {
            _context.TimeEntryRecords.Remove(FindRecord(id));
            _context.SaveChanges();
        }

        public TimeEntry Find(long id)
        {
            return FindRecord(id).ToEntity();
        }

        public IEnumerable<TimeEntry> List()
        {
            return _context.TimeEntryRecords.ToList()
                .Select(record => record.ToEntity());
        }

        public TimeEntry Update(long id, TimeEntry timeEntry)
        {
            TimeEntryRecord record = timeEntry.ToRecord();
            record.Id = id;

            _context.TimeEntryRecords.Update(record);
            _context.SaveChanges();
            return Find(id);
        }

        private TimeEntryRecord FindRecord(long id)
        {
            return _context.TimeEntryRecords.AsNoTracking().FirstOrDefault(q => q.Id == id);
        }
    }
}
