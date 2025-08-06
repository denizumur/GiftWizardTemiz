namespace GiftWizardTemiz.Application.Features.GiftFinder.Dtos;

public class GiftProfileDto
{
    public string AgeRange { get; set; } = string.Empty; 
    public int Gender { get; set; }
    public List<string> Interests { get; set; } = new(); 
    public string? Style { get; set; }
    public string Occasion { get; set; } = string.Empty; 
    public string Relationship { get; set; } = string.Empty; 
    public string PriceRange { get; set; } = string.Empty; 
}