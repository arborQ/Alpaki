namespace Alpaki.CrossCutting.Requests
{
    public interface IOrderedRequest
    {
        public bool Asc { get; set; }

        public string OrderBy { get; set; }
    }
}
