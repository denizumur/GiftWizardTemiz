// GiftWizardTemiz.WebApi/Program.cs

using GiftWizardTemiz.Application.Abstractions; // IAiService i�in
using GiftWizardTemiz.Application.Features.GiftFinder; // IGiftFinderService i�in
using GiftWizardTemiz.Application.Features.GiftFinder.Services;
using GiftWizardTemiz.Infrastructure.Services; // GeminiService i�in

var builder = WebApplication.CreateBuilder(args);

// ----- SERV�SLER� KAYDETME B�L�M� -----
builder.Services.AddControllers();
builder.Services.AddHttpClient();
// Her IGiftFinderService istendi�inde, ona bir GiftFinderService ver.
builder.Services.AddScoped<IGiftFinderService, GiftFinderService>();

// Her IAiService istendi�inde, ona bir GeminiService ver. (EKS�K OLAN SATIR BUYDU)
builder.Services.AddScoped<IAiService, GeminiService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// ----- UYGULAMAYI OLU�TURMA -----
var app = builder.Build();


// ----- M�DDLEWARE P�PELINE -----
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseDefaultFiles(); // index.html gibi dosyalar� varsay�lan olarak tan�r
app.UseStaticFiles();  // wwwroot klas�r�ndeki dosyalar�n sunulmas�n� sa�lar

app.MapControllers();


// ----- UYGULAMAYI �ALI�TIRMA -----
app.Run();