namespace src.Models.Common
{
    public interface IEntity<TKey>
    {
        TKey id { get; }
        DateTime created { get; set; }
        DateTime? updated { get; set; }
        bool deleted { get; set; }
    }
}
