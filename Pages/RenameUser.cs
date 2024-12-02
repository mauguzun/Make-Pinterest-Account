using Microsoft.Playwright;
using StartNewMakeAccount.Pages;

namespace MakeNewAccount.Pages
{
    public class RenameUser : PageBase
    {
        public override string ElementExistOnPage => "div[data-test-id='nux_name_done_btn']";

        public RenameUser(IPage driver) : base(driver) { }

        public async Task<bool> RenameAsync(string prettyName)
        {
            try
            {
                // Click the edit name button
                var editNameButton = await Page.QuerySelectorAsync("[data-test-id='nux-edit-name-btn']");
                if (editNameButton != null)
                {
                    await editNameButton.ClickAsync();
                }

                // Find the name input field
                var nameInput = await Page.QuerySelectorAsync("#name");
                if (nameInput != null)
                {
                    // Clear the input field by sending backspace 50 times
                    for (int i = 0; i < 50; i++)
                    {
                        await nameInput.PressAsync("Backspace");
                    }

                    // Type the new name
                    await nameInput.TypeAsync(prettyName);

                    // Click the update button
                    var updateButton = await Page.QuerySelectorAsync("div[data-test-id='nux-update-name-btn']");
                    if (updateButton != null)
                    {
                        await updateButton.ClickAsync();
                    }

                    // Click the "done" button
                    var doneButton = await Page.QuerySelectorAsync("[data-test-id='nux_name_done_btn']");
                    if (doneButton != null)
                    {
                        await doneButton.ClickAsync();
                    }

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                return HandleException(ex); // Assuming HandleException is a method that handles the exception.
            }
        }

    }
}
