// GiftWizardTemiz.Application/Abstractions/IAiService.cs
using GiftWizardTemiz.Domain.Entities; // GiftProfile'ı kullanmak için

namespace GiftWizardTemiz.Application.Abstractions;

public interface IAiService
{
    Task<string> GetGiftIdeasAsSingleTextAsync(GiftProfile profile);
}