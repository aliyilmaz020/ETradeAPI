using System.ComponentModel.DataAnnotations;

namespace ETradeAPI.Domain.Entities.Common
{
    public abstract class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual DateTime? UpdatedDate { get; set; }
    }
}
