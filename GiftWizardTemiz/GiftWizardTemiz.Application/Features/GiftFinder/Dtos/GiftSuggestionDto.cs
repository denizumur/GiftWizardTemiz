namespace GiftWizardTemiz.Application.Features.GiftFinder.Dtos;

public class GiftSuggestionDto
{
    public Guid ProductId { get; set; }
    public string ProductName { get; set; } = string.Empty; 
    public string ProductImageUrl { get; set; } = string.Empty; 
    public decimal Price { get; set; }
    public string Reasoning { get; set; } = string.Empty; 
}