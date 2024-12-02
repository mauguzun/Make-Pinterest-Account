using Microsoft.Playwright;
using StartNewMakeAccount;
using StartNewMakeAccount.Pages;
using System.Xml.Linq;

namespace StartNewMakeAccountPages
{
    public class FirstPage : PageBase
    {

        private const string PASSWORD = "qwerty123";

        public FirstPage(IPage page) : base(page)
        {
        }

        public override string? ElementExistOnPage => null;

        public async Task<bool> MakeEmailAndPasswordAsync(string email)
        {
            try
            {
                await Page.GotoAsync("http://pinterest.com");
                var signButton = Page.Locator("[data-test-id=\"simple-signup-button\"]");

                if (signButton is null)
                    throw new Exception("button signup not founded");

                await signButton.ClickAsync();

                await Page.Locator("input[name='id']").FillAsync(email);
                await Page.Locator("input[name='password']").FillAsync(PASSWORD);

                var birthday =  Page.Locator("#birthdate");
                if(birthday is not null)
                  await  birthday.PressSequentiallyAsync("11/07/1982");

                var ages = await Page.QuerySelectorAsync("[name=age]");
                if (ages != null)
                {
                    await ages.FillAsync(new Random().Next(2, 17).ToString());
                }
                await Page.Locator("[data-test-id=\"registerFormSubmitButton\"]").ClickAsync();
                File.AppendAllText("data/" + DateTime.Now.ToString("yyyyMMdd") + ".txt", $"{email}:{PASSWORD}{Environment.NewLine}");

                return true;
            }
            catch (Exception ex)
            {
                 return HandleException(ex);
            }

        }

    }
}
