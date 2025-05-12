using PuppeteerExtraSharp;
using PuppeteerExtraSharp.Plugins.ExtraStealth;
using PuppeteerSharp;

namespace scraper;

public class PuppeteerBrowser : IAsyncDisposable
{
    private IBrowser? _browser = null;
    private IPage? _page = null;


    public async Task Init()
    {
        var extra = new PuppeteerExtra();
        extra.Use(new StealthPlugin());

        _browser = await extra.LaunchAsync(new LaunchOptions()
        {
            Headless = false,
            ExecutablePath = Environment.GetEnvironmentVariable("CHROMIUM")
        });

        _page = await _browser.NewPageAsync();
        await _page.SetUserAgentAsync(RandomUserAgent.RandomUa.RandomUserAgent);
        await _page.SetViewportAsync(new ViewPortOptions()
        {
            DeviceScaleFactor = 1,
            Width = 1920 + new Random().Next(100),
            Height = 3000 + new Random().Next(100),
            IsLandscape = false,
            IsMobile = false
        });
    }

    public async Task AcceptGoogleAcceptPageAsync()
    {
        await _page!.GoToAsync("https://www.google.co.uk", waitUntil: WaitUntilNavigation.Load);
        await Task.Delay(1000 + new Random().Next(1000));
        await _page.EvaluateExpressionAsync("Array.from(document.querySelectorAll(\"button\")).filter(e => e.textContent == \"Accept all\")[0]?.click()");
    }

    public async Task<string[]> GetSearchResultsAsync(string needle, int limit = 100)
    {
        await _page!.GoToAsync($"https://www.google.co.uk/search?q={needle}&num={limit}", waitUntil: WaitUntilNavigation.Load);
        await Task.Delay(1000 + new Random().Next(1000));
        await _page.EvaluateExpressionAsync("Array.from(document.querySelectorAll(\"input[value='Accept all']\"))[1]?.click()");
        await Task.Delay(1000 + new Random().Next(1000));
        var command = """
        var css = Array.from(document.styleSheets).filter(s => s.ownerNode.parentElement.tagName == "BODY")[0];
        var className = Array.from(css.cssRules).filter(r => r.cssText.indexOf("color: rgb(13, 101, 45)") != -1)[0].selectorText;
        Array.from(document.querySelectorAll(className)).map(e => e.textContent.trim());
        """;
        var resultsList = await _page.EvaluateExpressionAsync<string[]>(command);
        return resultsList;
    }

    public async ValueTask DisposeAsync()
    {
        if (_browser != null)
        {
            await _browser!.CloseAsync();
            await _browser.DisposeAsync();
            _browser = null;
        }
        _page = null;
    }
}