using System;
using Orchard;

namespace MK.BookStore.Services {
    public interface IDateTimeService : IDependency {
        DateTime Now { get; }
    }
}