using scraper;
using Xunit;

namespace Tests;

public class SearchResultsTest
{
    [Fact]
    public async Task GetResults_GoodUrl_ReturnsHtml()
    {
        var p = await PuppeteerBrowser.Create();
        await p.AcceptGoogleAcceptPageAsync();
        var results = await p.GetSearchResultsAsync("land+registry+search");
        Assert.True(results.Length > 90);
        Assert.NotEmpty(results.Where(r => r.Contains("land")));
    }
}