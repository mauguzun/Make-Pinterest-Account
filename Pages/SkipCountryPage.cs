using Microsoft.Playwright;

namespace StartNewMakeAccount.Pages
{
    public class SkipCountryPage : SkipPage
    {
        public SkipCountryPage(IPage driver) : base(driver)
        {
        }

        public override string ElementExistOnPage => "[data-test-id='nux-locale-country-next-btn']";
    }
}
