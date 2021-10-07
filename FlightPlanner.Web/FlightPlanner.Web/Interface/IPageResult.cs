namespace FlightPlanner.Web.Interface
{
    public interface IPageResult<T>
    {
        public int Page { get; set; }
        public int TotalItems { get; set; }
        public T[] Items { get; set; }
    }
}
