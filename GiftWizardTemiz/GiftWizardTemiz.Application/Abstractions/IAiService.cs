
using GiftWizardTemiz.Domain.Entities; 

namespace GiftWizardTemiz.Application.Abstractions;

public interface IAiService
{
    Task<string> GetGiftIdeasAsSingleTextAsync(GiftProfile profile);
}