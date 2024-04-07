using System.Formats.Asn1;
using System.Text.Json.Serialization;
using System.Text.Json;
using UssJuniorTest.Core.Models;

namespace UssJuniorTest.Core.DTO
{
    public class CarPersonData
    {
        public long Id { get; private set; }
        public Car Car { get; private set; }
        public Person Person { get; private set; }

        public TimeSpan DriveTime { get; private set; }

        public CarPersonData(Car car, Person person, TimeSpan driveTime)
        {
            Id = car.Id * 2 + person.Id * 3;
            Car = car;
            Person = person;
            DriveTime = driveTime;
        }

        public void AddDriveTime(TimeSpan duration)
        {
            DriveTime += duration;
        }
    }
}
