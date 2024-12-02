using GUI.Pages;
using Microsoft.Playwright;
using StartNewMakeAccount;
using StartNewMakeAccount.Models.Email;
using StartNewMakeAccountPages;
using System.Net.WebSockets;

namespace MakeNewAccount
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            if (!Directory.Exists("data"))
                Directory.CreateDirectory("data");

            var emails = new EmailService().GetEmails();

            if (emails?.Length < 1)
            {
                Console.WriteLine("Email list empty ");
                return;
            }


            string edgePath = @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe";
            Console.WriteLine("Edge path is [C:\\Program Files (x86)\\Microsoft\\Edge\\Application\\msedge.exe] , n to ovveride ?");

            if (Console.ReadLine() == "n")
            {
                Console.WriteLine("Edge path ?");
                var path = Console.ReadLine();
                if (path is null)
                {
                    Console.WriteLine("Edge path can not be null");
                    return;

                }
                path = path.Trim();
            }

            Console.WriteLine("Show browser?");
            bool show = Console.ReadLine() == "y";

            int emailIndex = 0;
            while (emailIndex < emails.Count())
            {


                var playwright = await Playwright.CreateAsync();
                var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
                {
                    Headless = !show,
                    Args = new[] { "--mute-audio" }, // Correct array syntax for args
                    ExecutablePath = edgePath // Path to Microsoft Edge executable
                });

                int iterationNumber = 0;

                NameService nameService = new NameService();
                var currentName = nameService.PrettyName();

                var emailService = new EmailService();
                var email = emails[emailIndex];
                var page = await browser.NewPageAsync();
                var firstPage = new FirstPage(page);

                if (await firstPage.MakeEmailAndPasswordAsync(email))
                {
                    CheckPageType ac = new CheckPageType(page, currentName);

                    while (ac.CardPageTrue() == true || iterationNumber < 7)
                    {
                        Thread.Sleep(TimeSpan.FromSeconds(7));
                        try
                        {
                            await ac.CheckPage();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        finally
                        {
                            iterationNumber++;
                            if (iterationNumber == 7)
                            {
                                Console.WriteLine($"{iterationNumber} - follow");
                                await new FollowPage(page).FollowManyAsync(new NameService().GetFollowName(), 7);

                            }
                        }
                        Console.WriteLine($"{iterationNumber} - iterationNumber number");

                    }
                    await browser.CloseAsync();
                    iterationNumber++;
                }
                else
                {
                    await browser.CloseAsync();
                }
                emailIndex++;
            }

            Console.WriteLine("Email ended");
        }
    }
}
