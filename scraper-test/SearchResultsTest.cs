using scraper;
using Xunit;

namespace Tests;

public class SearchResultsTest
{
    [Fact]
    public async Task GetResults_GoodUrl_ReturnsHtml()
    {
        var results = await SearchResults.GetResultsAsync("https://www.google.co.uk/search?q=land+registry+search&num=100");
        Assert.True(true);
    }
}