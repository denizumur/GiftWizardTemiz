
using GiftWizardTemiz.Application.Abstractions;
using GiftWizardTemiz.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GiftWizardTemiz.Infrastructure.Services;

#region API Veri Modelleri

public class GeminiRequest
{
    [JsonPropertyName("contents")]
    public List<RequestContent> Contents { get; set; }
}

public class RequestContent
{
    [JsonPropertyName("parts")]
    public List<RequestPart> Parts { get; set; }
}

public class RequestPart
{
    [JsonPropertyName("text")]
    public string Text { get; set; }
}

public class GeminiResponse
{
    [JsonPropertyName("candidates")]
    public List<Candidate> Candidates { get; set; }
}

public class Candidate
{
    [JsonPropertyName("content")]
    public ResponseContent Content { get; set; }
}

public class ResponseContent
{
    [JsonPropertyName("parts")]
    public List<ResponsePart> Parts { get; set; }
}

public class ResponsePart
{
    [JsonPropertyName("text")]
    public string Text { get; set; }
}
#endregion

public class GeminiService : IAiService
{
    private readonly System.Net.Http.IHttpClientFactory _httpClientFactory;
    private readonly IConfiguration _configuration;

    public GeminiService(System.Net.Http.IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClientFactory = httpClientFactory;
        _configuration = configuration;
    }

    public async Task<string> GetGiftIdeasAsSingleTextAsync(GiftProfile profile)
    {
        var apiKey = _configuration["Gemini:ApiKey"];
        if (string.IsNullOrEmpty(apiKey))
        {
            return "Hata: Gemini API anahtarı bulunamadı.";
        }

        var httpClient = _httpClientFactory.CreateClient();
        var apiUrl = $"https://generativelanguage.googleapis.com/v1beta/models/gemini-1.5-flash-latest:generateContent?key={apiKey}";

        var interestsText = string.Join(", ", profile.Interests);

        var prompt = $$"""
Sen, hediye seçimi konusunda uzman, psikoloji ve trendlerden anlayan bir hediye danışmanısın. Görevin, sana verilen profil bilgilerini derinlemesine analiz ederek, son derece isabetli ve kişiye özel 5 hediye fikri sunmaktır.

Aşağıdaki profil için analiz yap ve sonuçları belirtilen JSON formatında dön:
- Yaş Aralığı: {{profile.AgeRange}}
- Cinsiyet: {{profile.Gender}}
- İlişki: {{profile.Relationship}}
- Özel Gün: {{profile.Occasion}}
- İlgi Alanları: {{interestsText}}
- Kişisel Tarz: {{profile.Style}}
- Bütçe Aralığı: {{profile.PriceRange}}

İstediğim Çıktı Formatı:
'idea' ve 'reasoning' alanları olan bir JSON array formatı.
- 'idea': Yaratıcı ve spesifik ürün fikri.
- 'reasoning': Bu fikri neden bu profile önerdiğini, profilin en az 2-3 farklı özelliğine (örneğin hem ilgi alanı hem de ilişki türü gibi) atıfta bulunarak ikna edici bir dille açıkla.

Kesinlikle sadece JSON array'i dön. Öncesinde veya sonrasında 'Elbette, işte JSON:' gibi hiçbir açıklama metni ekleme.

Örnek JSON:
[
    { "idea": "Yıldız Haritası Posteri", "reasoning": "Yıl dönümü için romantik bir hediye. {{profile.Relationship}} ilişkinizin başladığı günün yıldız haritası, modern tarzına uygun ve anlamlı bir hatıra olacaktır." },
    { "idea": "3. Nesil Kahve Demleme Seti", "reasoning": "{{interestsText}} arasında kahve olması ve teknolojiye ilgisi, onu yeni demleme teknikleri denemekten mutlu edecektir. Bu set, {{profile.AgeRange}} yaş aralığı için popüler ve sofistike bir seçimdir." }
]
""";

        var requestBody = new GeminiRequest
        {
            Contents = new List<RequestContent>
            {
                new RequestContent
                {
                    Parts = new List<RequestPart> { new RequestPart { Text = prompt } }
                }
            }
        };

        var response = await httpClient.PostAsJsonAsync(apiUrl, requestBody);

        if (!response.IsSuccessStatusCode)
        {
            return $"API Hatası: {await response.Content.ReadAsStringAsync()}";
        }

        var geminiResponse = await response.Content.ReadFromJsonAsync<GeminiResponse>();
        var resultText = geminiResponse?.Candidates?.FirstOrDefault()?.Content?.Parts?.FirstOrDefault()?.Text ?? "Yapay zekadan fikir alınamadı.";

        return resultText.Trim();
    }
}
