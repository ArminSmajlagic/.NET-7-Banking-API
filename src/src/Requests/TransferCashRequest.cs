namespace src.Requests
{
    public class TransferCashRequest
    {
        public int fromId { get; set; }
        public int toId { get; set; }
        public double ammount { get; set; }
    }
}
