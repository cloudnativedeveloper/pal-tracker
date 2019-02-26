using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PalTracker
{
    public class TimeEntryContext : DbContext
    {
        public TimeEntryContext(DbContextOptions dbContextOptions)
            : base (dbContextOptions)
        {
        }

        public DbSet<TimeEntryRecord> TimeEntryRecords { get; set; }
    }
}
