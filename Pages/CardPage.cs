using Microsoft.Playwright;

namespace StartNewMakeAccount.Pages
{
    public class CardPage : PageBase
    {
        public CardPage(IPage driver) : base(driver)
        {
        }

        public override string ElementExistOnPage => "[data-test-id='nux-picker-topic']";

        public async Task<bool> SelectAsync()
        {
            int result = 0;

            // Select cards using the locator for Playwright
            var cards = await Page.Locator("[data-test-id='nux-picker-topic']").ElementHandlesAsync();

            for (int i = 0; i < cards.Count; i++)
            {
                if (result > 5)
                {
                    // Locate the 'done' button using Playwright
                    var button = await Page.QuerySelectorAsync("[data-test-id='nux-picker-done-btn'] button") ??
                                 await Page.QuerySelectorAsync("button[type='submit']");

                    if (button != null)
                    {
                        // Perform the check operation
                        await button.ClickAsync();
                        Console.WriteLine("Await new user was registered");

                        // Sleep for 15 seconds
                        await Task.Delay(TimeSpan.FromSeconds(15));

                        return true;
                    }
                    return false;
                }

                try
                {
                    // Perform a click on the card using Playwright
                    await cards[i].ClickAsync();
                    result++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    // Refresh cards if an exception occurs
                    cards = await Page.Locator(".NuxInterest").ElementHandlesAsync();
                }
            }

            return false;
        }

    }
}
