using Microsoft.Playwright;

namespace StartNewMakeAccount.Pages
{
    public class SkipPage : PageBase
    {

        public SkipPage(IPage driver) : base(driver)
        {
        }    
       

        public override string ElementExistOnPage { get;  } =  "button[data-test-id='nux-ext-skip-btn']";

        public async Task<bool> Skip()
        {
            try
            {
                var clear = await Page.QuerySelectorAsync(this.ElementExistOnPage);

                if (clear is not null)
                    await clear.ClickAsync();

                Console.WriteLine("press skip");
                return true;
            }
            catch(Exception ex) 
            {
                return HandleException(ex);
            }
        }
    }
}
