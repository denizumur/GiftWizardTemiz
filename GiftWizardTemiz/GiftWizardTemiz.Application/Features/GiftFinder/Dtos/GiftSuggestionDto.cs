namespace GiftWizardTemiz.Application.Features.GiftFinder.Dtos;

public class GiftSuggestionDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty; // Başlangıç değeri eklendi
    public string ProductImageUrl { get; set; } = string.Empty; // Başlangıç değeri eklendi
    public decimal Price { get; set; }
    public string Reasoning { get; set; } = string.Empty; // Başlangıç değeri eklendi
}