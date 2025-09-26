using System.ComponentModel.DataAnnotations;

namespace CoreDriven.Utils.Spec;
public record BaseQueryParams
{
    [Range(1, int.MaxValue)] 
    public int PageNumber { get; set; } = 1; 
    [Range(1, 100)] 
    public int PageSize { get; set; } = 10;
    public string SortBy { get; set; } ="Id"; 
    public bool SortDesc { get; set; } = true;
    public string? Value { get; set; } 
    
}