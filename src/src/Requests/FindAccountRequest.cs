namespace src.Requests
{
    public class FindAccountRequest
    {
        public int? page { get; set; }
        public int? size { get; set; }
        public int? offset { get; set; }
        public string? orderBy { get; set; }
    }
}
