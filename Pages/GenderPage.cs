using Microsoft.Playwright;
using StartNewMakeAccount;
using StartNewMakeAccount.Pages;

namespace MakeNewAccount.Pages
{
    public class GenderPage : PageBase
    {

        public GenderPage(IPage driver) : base(driver)
        {
        }

        public override string ElementExistOnPage => "label[for='female']";

        public async Task<bool> Select()
        {
            var button = await Page.QuerySelectorAsync(this.ElementExistOnPage);
            if (button is not null)
                await button.ClickAsync();

            return (button is not null);
        }
    }
}