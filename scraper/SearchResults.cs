using System.Text.Json.Serialization;
using HtmlAgilityPack;
using PuppeteerExtraSharp;
using PuppeteerExtraSharp.Plugins.ExtraStealth;
using PuppeteerSharp;

namespace scraper;

public static class SearchResults
{
    public static async Task<List<string>> GetResultsAsync(string url)
    {
        var extra = new PuppeteerExtra(); 
        extra.Use(new StealthPlugin());   
        // var browserFetcher = new BrowserFetcher();
        // await browserFetcher.DownloadAsync();
        // await using var browser = await Puppeteer.LaunchAsync(
        //     new LaunchOptions { Headless = true });

        var browser = await extra.LaunchAsync(new LaunchOptions()
        {
            Headless = true,
            ExecutablePath = "/usr/bin/chromium-browser",
            IgnoreDefaultArgs = true 
        });

        await using var page = await browser.NewPageAsync();
        await page.GoToAsync(url);
        await Task.Delay(1000);
        var htmlString = await page.GetContentAsync();

        HtmlDocument htmlDoc = new();
        htmlDoc.LoadHtml(htmlString);
        return htmlDoc.DocumentNode.SelectNodes("//div[contains(@class,'notranslate')]//cite")!
            .Select(n => n.ChildNodes[0].InnerText)
            .ToList();
    }
}
