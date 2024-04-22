using System.Runtime.InteropServices.JavaScript;

namespace Shared.Common;

public abstract class BaseEntity
{
    public Guid? CreatedBy { get; set; }
    public Guid? LatModifiedBy { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? LastModifiedAt { get; set; }
}