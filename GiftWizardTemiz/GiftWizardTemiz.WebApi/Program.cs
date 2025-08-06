// GiftWizardTemiz.WebApi/Program.cs

using GiftWizardTemiz.Application.Abstractions; // IAiService için
using GiftWizardTemiz.Application.Features.GiftFinder; // IGiftFinderService için
using GiftWizardTemiz.Application.Features.GiftFinder.Services;
using GiftWizardTemiz.Infrastructure.Services; // GeminiService için

var builder = WebApplication.CreateBuilder(args);

// ----- SERVÝSLERÝ KAYDETME BÖLÜMÜ -----
builder.Services.AddControllers();
builder.Services.AddHttpClient();
// Her IGiftFinderService istendiðinde, ona bir GiftFinderService ver.
builder.Services.AddScoped<IGiftFinderService, GiftFinderService>();

// Her IAiService istendiðinde, ona bir GeminiService ver. (EKSÝK OLAN SATIR BUYDU)
builder.Services.AddScoped<IAiService, GeminiService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// ----- UYGULAMAYI OLUÞTURMA -----
var app = builder.Build();


// ----- MÝDDLEWARE PÝPELINE -----
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseDefaultFiles(); // index.html gibi dosyalarý varsayýlan olarak tanýr
app.UseStaticFiles();  // wwwroot klasöründeki dosyalarýn sunulmasýný saðlar

app.MapControllers();


// ----- UYGULAMAYI ÇALIÞTIRMA -----
app.Run();