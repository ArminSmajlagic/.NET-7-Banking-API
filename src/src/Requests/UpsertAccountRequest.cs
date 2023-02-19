namespace src.Requests
{
    public class UpsertAccountRequest
    {
        public int? id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
    }
}
