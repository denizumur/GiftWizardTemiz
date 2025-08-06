

using GiftWizardTemiz.Application.Abstractions; 
using GiftWizardTemiz.Application.Features.GiftFinder; 
using GiftWizardTemiz.Application.Features.GiftFinder.Services;
using GiftWizardTemiz.Infrastructure.Services; 
var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddHttpClient();

builder.Services.AddScoped<IGiftFinderService, GiftFinderService>();


builder.Services.AddScoped<IAiService, GeminiService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.UseDefaultFiles(); // index.html gibi dosyalarý varsayýlan olarak tanýr
app.UseStaticFiles();  // wwwroot klasöründeki dosyalarýn sunulmasýný saðlar

app.MapControllers();


app.Run();