using Microsoft.Playwright.NUnit;

namespace NakarmPsa.Olx;

//dotnet test -- Playwright.LaunchOptions.Headless=false

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class Tests : PageTest
{
    [Test]
    [Repeat(1000)]
    public async Task NakarmPsa()
    {
        await Page.GotoAsync("https://nakarmpsa.olx.pl/");

        // create a locator
        await Page.Locator("div.sort-select").ClickAsync();

        await Page.Locator("#petVotesLeastFed").ClickAsync();

        var animals = Page.Locator("div.olx-pet-list-inner");

        var topAnimal = animals.Locator("""div[style="order: 1;"]""");

        await topAnimal.Locator("div.single-pet-control-feed_button").ClickAsync();

        await Task.Delay(2000);
    }
}