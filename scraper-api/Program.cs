using scraper;
using System.Linq;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.MapGet("/api/search/{needle}", async (string needle) =>
{
    if (Environment.GetEnvironmentVariable("MOCK_RESULTS") == "1")
    {
        await Task.Delay(1000);
        var fakeResults = new string[] { "www.gov.uk", "www.info.co.uk", "www.bbc.co.uk", "www.example.com" };
        return Enumerable.Range(0, 100).Select(n => fakeResults[n % 4] + (n + 1).ToString());
    }
    else
    {
        await using var p = new PuppeteerBrowser();
        await p.Init();
        await p.AcceptGoogleAcceptPageAsync();
        return await p.GetSearchResultsAsync(needle);
    }
})
.WithName("search")
.WithOpenApi();

app.Run();
