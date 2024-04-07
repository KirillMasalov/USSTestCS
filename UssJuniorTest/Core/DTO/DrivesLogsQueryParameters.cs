namespace UssJuniorTest.Core.DTO
{
    public class DrivesLogsQueryParameters
    {
        private static readonly int DEFAULT_PAGE_SIZE = 10;

        public string PersonName { get; private set; }
        public string CarModel { get; private set; }
        public bool CarModelOrder { get; private set; }
        public bool PersonNameOrder { get; private set; }
        public DateTime? StartDateTime { get; private set; }
        public DateTime? EndDateTime { get; private set; }


        public bool RequirePage { get; private set; }
        public int Page { get; private set; }
        public int PageSize { get; private set; }

        public DrivesLogsQueryParameters(DrivesLogsGetModel inputModel) 
        {
            PersonName = inputModel.PersonName;
            CarModel = inputModel.CarModel;
            CarModelOrder = inputModel.CarModelOrder;
            PersonNameOrder = inputModel.PersonNameOrder;
            StartDateTime = inputModel.StartDateTime;
            EndDateTime = inputModel.EndDateTime;

            RequirePage = inputModel.Page.HasValue || inputModel.PageSize.HasValue;
            Page = inputModel.Page ?? 1;
            PageSize = inputModel.PageSize ?? DEFAULT_PAGE_SIZE;
        }
    }
}
