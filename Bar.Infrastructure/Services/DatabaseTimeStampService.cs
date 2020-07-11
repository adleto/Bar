using Bar.Database;
using Bar.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bar.Infrastructure.Services
{
    public class DatabaseTimeStampService : IDatabaseTimeStamp
    {
        private readonly Context _context;

        public DatabaseTimeStampService(Context context)
        {
            _context = context;
        }

        public DateTime Get()
        {
            return _context.DatabaseTimeStamp.First().TimeStamp;
        }
    }
}
