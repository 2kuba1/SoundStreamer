using Shared.Common;

namespace Music.Core.Entities;

public class Music : BaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string ThumbnailUrl { get; set; }
    public string FileUrl { get; set; }
}