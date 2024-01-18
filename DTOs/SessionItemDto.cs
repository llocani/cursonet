namespace DTOs
{
    public class SessionItemDto
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public virtual UserItemDto? User { get; set; }
        public DateTime OpenedAt { get; set; }
    }
}
