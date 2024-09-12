using System.ComponentModel.DataAnnotations.Schema;

namespace SmartMarketDesktop.ViewModels.Entities;

public class BaseEntity
{
    [Column("id")]
    public Guid Id { get; set; }
    [Column("created_at")]
    public DateTime? CreatedDate { get; set; } = DateTime.UtcNow.AddHours(5);
}
