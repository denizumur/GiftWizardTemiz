using GiftWizardTemiz.Application.Features.GiftFinder;
using GiftWizardTemiz.Application.Features.GiftFinder.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace GiftWizardTemiz.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GiftFinderController : ControllerBase
{
    private readonly IGiftFinderService _giftFinderService;

 
    public GiftFinderController(IGiftFinderService giftFinderService)
    {
        _giftFinderService = giftFinderService;
    }

    
    [HttpPost("suggestions")]
    public async Task<IActionResult> GetSuggestions([FromBody] GiftProfileDto profileDto)
    {
        
        var suggestions = await _giftFinderService.GetSuggestionsAsync(profileDto);

        
        return Ok(suggestions);
    }
}