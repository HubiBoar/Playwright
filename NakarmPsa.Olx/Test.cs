using Microsoft.Playwright.NUnit;

namespace NakarmPsa.Olx;

//dotnet test -- Playwright.LaunchOptions.Headless=false

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    private const int Range = 16;
    private const int Max = 10000;
    
    public static IEnumerable<(int index, int repeat)> GetTestCases() =>
        Enumerable.Repeat(Enumerable.Range(1, Range), Max).SelectMany((x, i) => x.Select(k => (index: k, repeat: (i * Range) + k)));
    
    [TestCaseSource(nameof(GetTestCases))]
    public async Task NakarmPsa((int index, int repeat) value)
    {
        await TestContext.Progress.WriteLineAsync($"Animal Number: {value.index} Total: {value.repeat}");
        
        await Page.GotoAsync("https://nakarmpsa.olx.pl/");

        // create a locator
        await Page.Locator("div.sort-select").ClickAsync();

        await Page.Locator("#petVotesLeastFed").ClickAsync();

        var animals = Page.Locator("div.olx-pet-list-inner");

        var topAnimal = animals.Locator($"""div[style="order: {value.index};"]""");

        await topAnimal.Locator("div.single-pet-control-feed_button").ClickAsync();

        await Task.Delay(100);
    }
}