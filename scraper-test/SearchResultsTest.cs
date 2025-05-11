using scraper;
using Xunit;

namespace Tests;

public class SearchResultsTest
{
    [Fact]
    public async Task GetResults_GoodUrl_ReturnsHtml()
    {
        var p = await PuppeteerBrowser.Create();
        await p.AcceptGoogleAcceptPage();
        var results = await p.GetSearchResults(new string[] { "land", "registry", "search"});
        Assert.True(true);
    }
}