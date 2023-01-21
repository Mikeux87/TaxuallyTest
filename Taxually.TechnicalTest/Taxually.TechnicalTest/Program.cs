using Microsoft.Extensions.Http;
using Taxually.TechnicalTest;
using Taxually.TechnicalTest.Interfaces;
using Taxually.TechnicalTest.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddHttpClient<TaxuallyHttpClient>("TaxuallyHttpClient", client => client.BaseAddress = new Uri("https://api.uktax.gov.uk"));
    //.AddTypedClient<TaxuallyQueueClient>("TaxuallyQueueClient", client => client);

builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("TaxuallyHttpClient"));
//builder.Services.AddScoped(sp => sp.GetRequiredService<ITypedHttpClientFactory>().CreateClient("TaxuallyQueueClient"));

// Add services to the container.
builder.Services.AddTransient<IVatRegistrationService, VatRegistrationService>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
