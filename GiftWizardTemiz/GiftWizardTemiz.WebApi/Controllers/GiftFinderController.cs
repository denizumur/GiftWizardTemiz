using GiftWizardTemiz.Application.Features.GiftFinder;
using GiftWizardTemiz.Application.Features.GiftFinder.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GiftWizardTemiz.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GiftFinderController : ControllerBase
{
    private readonly IGiftFinderService _giftFinderService;

    // 1. Dependency Injection ile Servisi Alıyoruz
    public GiftFinderController(IGiftFinderService giftFinderService)
    {
        _giftFinderService = giftFinderService;
    }

    // 2. HTTP POST Endpoint'imizi Oluşturuyoruz
    [HttpPost("suggestions")]
    public async Task<IActionResult> GetSuggestions([FromBody] GiftProfileDto profileDto)
    {
        // 3. İsteği Application Katmanına Paslıyoruz
        var suggestions = await _giftFinderService.GetSuggestionsAsync(profileDto);

        // 4. Sonucu 200 OK olarak geri dönüyoruz
        return Ok(suggestions);
    }
}