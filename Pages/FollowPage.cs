using Microsoft.Playwright;
using StartNewMakeAccount.Pages;
using System;
using System.Threading.Tasks;

namespace GUI.Pages
{
    public class FollowPage : PageBase
    {
        public FollowPage(IPage page) : base(page)
        {
        }

        public override string? ElementExistOnPage => throw new NotImplementedException();

        public async Task<int> FollowManyAsync(string query, int limit)
        {
            // Navigate to the specified Pinterest search URL
            await Page.GotoAsync($"https://www.pinterest.com/search/users/?q={query}");

            int attempts = 0;
            int success = 0;
            await Task.Delay(TimeSpan.FromSeconds(15));

            while (attempts <= limit && success < limit)
            {
                // Locate user blocks and follow buttons
                var usersBlocks = Page.Locator("[data-test-id='user-rep']");
                var followButtons = Page.Locator("[data-test-id='user-follow-button']");

                // Get the count of user blocks
                var userCount = await usersBlocks.CountAsync();

                for (int i = 0; i < userCount; i++)
                {
                    var userBlock = usersBlocks.Nth(i);
                    Console.WriteLine($"Try follow a: {attempts}, clicked: {success}");

                    // Get the follower count
                    var followersElement = await userBlock.Locator("[data-test-id='user-rep-followers']").TextContentAsync();
                    int followers = 0;
                    if (!string.IsNullOrEmpty(followersElement))
                    {
                        var dirty = followersElement.Replace("followers", "").Trim();
                        int.TryParse(dirty, out followers);
                    }


                    try
                    {
                        await userBlock.Locator("[data-test-id='user-follow-button']").ClickAsync();
                        success++;
                        await Task.Delay(4000); // Wait for a few seconds after clicking
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error clicking follow button: {ex.Message}");
                    }


                    // Check if the limit has been reached
                    if (success >= limit)
                        return success;
                }

                // Scroll down the page
                await Page.EvaluateAsync("window.scrollTo(0, document.body.scrollHeight);");
                attempts++;

                // Check if attempts or success limit has been reached
                if (attempts > limit || success >= limit)
                    return success;
            }

            await Task.Delay(TimeSpan.FromSeconds(15));
            return success;
        }


    }

}
