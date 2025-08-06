// GiftWizardTemiz.Application/Features/GiftFinder/IGiftFinderService.cs
using GiftWizardTemiz.Application.Features.GiftFinder.Dtos;

namespace GiftWizardTemiz.Application.Features.GiftFinder;

public interface IGiftFinderService
{
    Task<List<GiftSuggestionDto>> GetSuggestionsAsync(GiftProfileDto profileDto);
}