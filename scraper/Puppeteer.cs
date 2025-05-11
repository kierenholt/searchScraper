using PuppeteerExtraSharp;
using PuppeteerExtraSharp.Plugins.ExtraStealth;
using PuppeteerSharp;

namespace scraper;

public class PuppeteerBrowser
{
    private readonly IBrowser _browser;
    private readonly IPage _page;

    private PuppeteerBrowser(IBrowser browser, IPage page)
    {
        _browser = browser;
        _page = page;
    }


    public static async Task<PuppeteerBrowser> Create()
    {
        var extra = new PuppeteerExtra();
        extra.Use(new StealthPlugin());
        // var browserFetcher = new BrowserFetcher();
        // await browserFetcher.DownloadAsync();
        // await using var browser = await Puppeteer.LaunchAsync(
        //     new LaunchOptions { Headless = true });

        var browser = await extra.LaunchAsync(new LaunchOptions()
        {
            Headless = false,
            ExecutablePath = "/usr/bin/chromium-browser"
        });

        var page = await browser.NewPageAsync();
        await page.SetUserAgentAsync(RandomUserAgent.RandomUa.RandomUserAgent);
        await page.SetViewportAsync(new ViewPortOptions()
        {
            DeviceScaleFactor = 1,
            Width = 1920 + new Random().Next(100),
            Height = 3000 + new Random().Next(100),
            IsLandscape = false,
            IsMobile = false
        });

        return new PuppeteerBrowser(browser, page);
    }

    public async Task AcceptGoogleAcceptPage()
    {
        await _page.GoToAsync("https://www.google.com", waitUntil: WaitUntilNavigation.Load);
        await Task.Delay(3000 + new Random().Next(2000));
        await _page.EvaluateExpressionAsync("Array.from(document.querySelectorAll(\"button\")).filter(e => e.textContent == \"Accept all\")[0]?.click()");
    }

    public async Task<string[]> GetSearchResults(string[] searchTerms, int limit = 100)
    {
        //await _page.TypeAsync("")
        await _page.GoToAsync($"https://www.google.co.uk/search?q={string.Join("+", searchTerms)}&num={limit.ToString()}", waitUntil: WaitUntilNavigation.Load);
        await Task.Delay(1000 + new Random().Next(2000));
        await _page.EvaluateExpressionAsync("Array.from(document.querySelectorAll(\"input[value='Accept all']\"))[1]?.click()");
        await Task.Delay(1000 + new Random().Next(2000));
        var command = """
        var css = Array.from(document.styleSheets).filter(s => s.ownerNode.parentElement.tagName == "BODY")[0];
        var className = Array.from(css.cssRules).filter(r => r.cssText.indexOf("color: rgb(13, 101, 45)") != -1)[0].selectorText;
        Array.from(document.querySelectorAll(className)).map(e => e.textContent.trim());
        """;
        var resultsList = await _page.EvaluateExpressionAsync<string[]>(command);
        return resultsList;
    }
}