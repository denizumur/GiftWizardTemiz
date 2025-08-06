namespace GiftWizardTemiz.Domain.Entities;

public enum Gender { Unspecified, Female, Male }

public class GiftProfile
{
    public Guid Id { get; set; }
    public string AgeRange { get; set; } = string.Empty; // Başlangıç değeri eklendi
    public Gender Gender { get; set; }
    public List<string> Interests { get; set; } = new(); // Başlangıç değeri eklendi
    public string? Style { get; set; }
    public string Occasion { get; set; } = string.Empty; // Başlangıç değeri eklendi
    public string Relationship { get; set; } = string.Empty; // Başlangıç değeri eklendi
    public string PriceRange { get; set; } = string.Empty; // Başlangıç değeri eklendi
}