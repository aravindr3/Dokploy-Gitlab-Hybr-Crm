using System;

namespace HyBrForex.Domain.Common
{
    public abstract class AuditableBaseEntity<TKey> : BaseEntity<TKey>
    {
        public string? CreatedBy { get; set; }
        public DateTime? Created { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }
        public int Status { get; set; }
    }

    public abstract class AuditableBaseEntity : AuditableBaseEntity<string>
    {
    }

    //public abstract class AuditableUlidBaseEntity<TKey> : BaseEntity<TKey>
    //{
    //    protected AuditableUlidBaseEntity()
    //    {
    //        Created = DateTime.Now;
    //    }
    //    public Ulid CreatedBy { get; set; }
    //    public DateTime Created { get; set; }
    //    public Ulid? LastModifiedBy { get; set; }
    //    public DateTime? LastModified { get; set; }
    //    public int Status { get; set; }
    //}

    //public abstract class AuditableUlidBaseEntity : AuditableUlidBaseEntity<Ulid>
    //{
    //}
}