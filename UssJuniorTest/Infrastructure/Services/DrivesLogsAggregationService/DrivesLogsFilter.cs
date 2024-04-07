using UssJuniorTest.Core.Models;

namespace UssJuniorTest.Infrastructure.Services.DrivesLogsAggregationService
{
    public class DrivesLogsFilter
    {
        private Func<Car, Person, DriveLog, bool> _filter;

        public DrivesLogsFilter(Func<Car, Person, DriveLog, bool> inFilter)
        {
            _filter = inFilter;
        }

        public bool Apply(Car car, Person person, DriveLog log)
        {
            return _filter(car, person, log);
        }
    }
}
