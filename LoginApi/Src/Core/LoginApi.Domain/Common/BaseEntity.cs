using System.ComponentModel.DataAnnotations;

namespace HyBrForex.Domain.Common
{
    public abstract class BaseEntity<TKey>
    {
        [Key]

        public TKey Id { get; set; } = default!;
    }

    public abstract class BaseEntity : BaseEntity<string>
    {
    }
}