// GiftWizardTemiz.Application/Features/GiftFinder/Services/GiftFinderService.cs
using GiftWizardTemiz.Application.Abstractions;
using GiftWizardTemiz.Application.Features.GiftFinder.Dtos;
using GiftWizardTemiz.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GiftWizardTemiz.Application.Features.GiftFinder.Services;

public class AiGiftSuggestion
{
    [JsonPropertyName("idea")]
    public string Idea { get; set; }

    [JsonPropertyName("reasoning")]
    public string Reasoning { get; set; }
}

public class GiftFinderService : IGiftFinderService
{
    private readonly IAiService _aiService;

    public GiftFinderService(IAiService aiService)
    {
        _aiService = aiService;
    }

    public async Task<List<GiftSuggestionDto>> GetSuggestionsAsync(GiftProfileDto profileDto)
    {
        var giftProfile = new GiftProfile
        {
            AgeRange = profileDto.AgeRange,
            Gender = (Gender)profileDto.Gender, // Gelen sayıyı enum'a çeviriyoruz
            Interests = profileDto.Interests,
            Style = profileDto.Style,
            Occasion = profileDto.Occasion,
            Relationship = profileDto.Relationship,
            PriceRange = profileDto.PriceRange
        };

        var ideasAsJson = await _aiService.GetGiftIdeasAsSingleTextAsync(giftProfile);

        var suggestions = new List<GiftSuggestionDto>();
        try
        {
            // --- YENİ EKLENEN TEMİZLEME KODU ---
            var startIndex = ideasAsJson.IndexOf('[');
            var endIndex = ideasAsJson.LastIndexOf(']');

            if (startIndex == -1 || endIndex == -1)
            {
                // Eğer cevapta [ veya ] yoksa, bu geçerli bir JSON array değildir.
                throw new JsonException("Gelen cevapta JSON array bulunamadı. Gelen Ham Metin: " + ideasAsJson);
            }

            // Sadece [ ile ] arasındaki saf JSON'ı alıyoruz.
            var cleanJson = ideasAsJson.Substring(startIndex, endIndex - startIndex + 1);
            // ------------------------------------

            // Artık temizlenmiş JSON'ı C# nesnelerine dönüştürüyoruz
            var aiSuggestions = JsonSerializer.Deserialize<List<AiGiftSuggestion>>(cleanJson);

            foreach (var aiSuggestion in aiSuggestions)
            {
                suggestions.Add(new GiftSuggestionDto
                {
                    ProductId = Guid.NewGuid(),
                    ProductName = aiSuggestion.Idea.ToUpper(),
                    ProductImageUrl = "[https://via.placeholder.com/150](https://via.placeholder.com/150)",
                    Price = new Random().Next(50, 500),
                    Reasoning = aiSuggestion.Reasoning
                });
            }
        }
        catch (Exception ex) // Hata tipini daha genel tuttuk ki her şeyi yakalayabilelim
        {
            suggestions.Add(new GiftSuggestionDto
            {
                ProductName = "Yapay Zeka Cevabı İşlenirken Hata Oluştu",
                Reasoning = ex.Message
            });
        }

        return suggestions;
    }
}