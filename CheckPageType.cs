using GUI.Pages;
using MakeNewAccount.Pages;
using Microsoft.Playwright;
using StartNewMakeAccount.Pages;

namespace StartNewMakeAccount
{

    public class CheckPageType
    {
        private Dictionary<RegisterPageType, bool> _pageTypes = new Dictionary<RegisterPageType, bool>();
        private string _prettyName;
        private IPage _page;

        public CheckPageType(IPage page, string prettyName)
        {
            _prettyName = prettyName;
            _page = page;
        }


        protected string password = $"trance_333";


        public bool CardPageTrue() =>
        this._pageTypes.ContainsKey(RegisterPageType.Card);


        public async Task CheckPage()
        {

            if (!_pageTypes.ContainsKey(RegisterPageType.RenameUser) && (await _page.Locator(new RenameUser(_page).ElementExistOnPage).CountAsync()) != 0)
            {
                _pageTypes[RegisterPageType.RenameUser] = await new RenameUser(_page).RenameAsync(_prettyName);
                return;
            }

            if (!_pageTypes.ContainsKey(RegisterPageType.Skip) && ( await _page.Locator(new SkipPage(_page).ElementExistOnPage).CountAsync() != 0))
            {
                _pageTypes[RegisterPageType.Skip] = await new SkipPage(_page).Skip();
                return;
            }

            if (!_pageTypes.ContainsKey(RegisterPageType.Country) && (await _page.Locator(new SkipCountryPage(_page).ElementExistOnPage).CountAsync() != 0))
              
            {
                _pageTypes[RegisterPageType.SkipCountry] = await new SkipCountryPage(_page).Skip();
                return;
            }

            if (!_pageTypes.ContainsKey(RegisterPageType.CountryOneMoreTime) && (await _page.Locator(new SkipCountryPage(_page).ElementExistOnPage).CountAsync() != 0))
            {
                _pageTypes[RegisterPageType.CountryOneMoreTime] = await  new SkipCountryPage(_page).Skip();
                return;
            }

            if (!_pageTypes.ContainsKey(RegisterPageType.Gender) && (await _page.Locator(new GenderPage(_page).ElementExistOnPage).CountAsync() != 0))
            {
                _pageTypes[RegisterPageType.SkipCountry] = await new GenderPage(_page).Select();
                return;
            }

             
            if (!_pageTypes.ContainsKey(RegisterPageType.Card) && (await _page.Locator(new CardPage(_page).ElementExistOnPage).CountAsync() != 0))
            {
                _pageTypes[RegisterPageType.SkipCountry] = await new CardPage(_page).SelectAsync();
            }



          

        }

    }
}
