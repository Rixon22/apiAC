using System;

namespace API.Entities;

public class Photo
{
    public int Id { get; set; }
    public string ImageUrl { get; set; } = null!;
    public string? PublicId { get; set; }

    // Navigational property
    public Member Member { get; set; } = null!;
    public string MemberId { get; set; } = null!;
}
