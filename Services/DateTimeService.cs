using System;

namespace MK.BookStore.Services {
    public class DateTimeService : IDateTimeService {
        public DateTime Now {
            get { return DateTime.UtcNow; }
        }
    }
}