using Microsoft.Playwright;

namespace StartNewMakeAccount.Pages
{
    public abstract class PageBase 
    {
        protected readonly IPage Page;
        public abstract string? ElementExistOnPage { get; }
        protected PageBase(IPage driver) => this.Page = driver;

        public bool HandleException(Exception ex)
        {
            Console.WriteLine($" {ex.StackTrace} exception  {ex.Message}");
            return false;
        }
    }
}
