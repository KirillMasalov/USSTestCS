using Microsoft.AspNetCore.Mvc;

namespace UssJuniorTest.Core.DTO
{
    public class DrivesLogsGetModel
    {
        public string? PersonName { get; set; }
        public string? CarModel { get; set; }
        public bool CarModelOrder { get; set; }
        public bool PersonNameOrder { get; set; }
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }

        public int? Page { get; set; }
        public int? PageSize { get; set; }

    }
}
