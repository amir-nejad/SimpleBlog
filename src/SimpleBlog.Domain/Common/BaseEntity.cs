namespace SimpleBlog.Domain.Common
{
    public abstract class BaseEntity
    {
        public DateTime CreatedDateUtc { get; protected set; } = DateTime.UtcNow;

        public DateTime UpdatedDateUtc { get; protected set; } = DateTime.UtcNow;
    }
}
