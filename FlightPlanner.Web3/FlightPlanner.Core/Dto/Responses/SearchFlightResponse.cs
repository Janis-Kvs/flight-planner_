namespace FlightPlanner.Core.Dto.Responses
{
    public class SearchFlightResponse
    {
        public int Page { get; set; }

        public int TotalItems { get; set; }

        public FlightResponse[] Items { get; set; }

        public SearchFlightResponse(FlightResponse[] flights)
        {
            Items = flights;
            TotalItems = flights.Length;
        }
    }
}
