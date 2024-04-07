using UssJuniorTest.Core;
using UssJuniorTest.Core.DTO;
using UssJuniorTest.Core.Models;
using UssJuniorTest.Extensions;
using UssJuniorTest.Infrastructure.Repositories;

namespace UssJuniorTest.Infrastructure.Services.DrivesLogsAggregationService
{
    public class DrivesLogsAggregationService
    {

        private readonly CarRepository _carRepository;
        private readonly PersonRepository _personRepository;
        private readonly DriveLogRepository _driveLogRepository;

        public DrivesLogsAggregationService(CarRepository carRepository,
            PersonRepository personRepository,
            DriveLogRepository driveLogRepository)
        {
            _carRepository = carRepository;
            _personRepository = personRepository;
            _driveLogRepository = driveLogRepository;
        }

        public async Task<IEnumerable<CarPersonData>> GetDataFromQuery(DrivesLogsQueryParameters parameters)
        {
            var filters = GetFiltersFromParameters(parameters);

            var resultData = await GetData(filters);

            if (parameters.PersonNameOrder && !parameters.CarModelOrder)
                resultData = resultData.OrderBy(d => d.Person.Name);
            else if (parameters.CarModelOrder && !parameters.PersonNameOrder)
                resultData = resultData.OrderBy(d => d.Car.Model);
            else if(parameters.PersonNameOrder && parameters.CarModelOrder)
                resultData = resultData.OrderBy(d => (d.Person.Name, d.Car.Model));

            if (parameters.RequirePage)
                resultData = resultData.GetPage(parameters.Page, parameters.PageSize);

            return resultData;
        }

        private IEnumerable<DrivesLogsFilter> GetFiltersFromParameters(DrivesLogsQueryParameters parameters)
        {
            var filters = new List<DrivesLogsFilter>();

            if (parameters.StartDateTime.HasValue)
                filters.Add(new DrivesLogsFilter((c, p, l) => l.StartDateTime >= parameters.StartDateTime.Value));

            if (parameters.EndDateTime.HasValue)
                filters.Add(new DrivesLogsFilter((c, p, l) => l.EndDateTime <= parameters.EndDateTime.Value));

            if (parameters.PersonName is not null)
                filters.Add(new DrivesLogsFilter((c, p, l) => p.Name.ToLower() == parameters.PersonName.ToLower()));

            if (parameters.CarModel is not null)
                filters.Add(new DrivesLogsFilter((c, p, l) => c.Model.ToLower() == parameters.CarModel.ToLower()));

            return filters;
        }


        private async Task<IEnumerable<CarPersonData>> GetData(IEnumerable<DrivesLogsFilter> filters)
        {
            var carIdCache = new Dictionary<long, Car>();
            var personIdCache = new Dictionary<long, Person>();

            var carPersonLogs = new Dictionary<long, Dictionary<long, CarPersonData>>();
            var driveLogs = await _driveLogRepository.GetAll();
            foreach (var log in driveLogs)
            {
                var car = await GetItemById(log.CarId, carIdCache, _carRepository);
                var person = await GetItemById(log.PersonId, personIdCache, _personRepository);

                if (!filters.All(f=>f.Apply(car, person, log)))
                    continue;

                if (!carPersonLogs.ContainsKey(log.CarId))
                {
                    carPersonLogs[log.CarId] = new Dictionary<long, CarPersonData>();
                    carPersonLogs[log.CarId][log.PersonId] = new CarPersonData(car, person, log.Duration);
                }
                else
                {
                    if (!carPersonLogs[log.CarId].ContainsKey(log.PersonId))
                        carPersonLogs[log.CarId][log.PersonId] = new CarPersonData(car, person, log.Duration);
                    else
                        carPersonLogs[log.CarId][log.PersonId].DriveTime.Add(log.Duration);
                }
            }

            return carPersonLogs.SelectMany(d => d.Value.Select(p => p.Value));
        }

        private async Task<T> GetItemById<T>(long id, Dictionary<long, T> cache, IRepository<T> repository) where T : Model
        {
            var item = cache.ContainsKey(id) ? cache[id] : await repository.Get(id);
            if (!cache.ContainsKey(id))
                cache[id] = item;

            return item;
        }
    }
}
