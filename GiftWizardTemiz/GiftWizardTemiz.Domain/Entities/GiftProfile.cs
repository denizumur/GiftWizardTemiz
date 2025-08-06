namespace GiftWizardTemiz.Domain.Entities;

public enum Gender { Unspecified, Female, Male }

public class GiftProfile
{
    public Guid Id { get; set; }
    public string AgeRange { get; set; } = string.Empty; 
    public Gender Gender { get; set; }
    public List<string> Interests { get; set; } = new(); 
    public string? Style { get; set; }
    public string Occasion { get; set; } = string.Empty; 
    public string Relationship { get; set; } = string.Empty; 
    public string PriceRange { get; set; } = string.Empty; 
}