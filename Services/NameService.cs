using StartNewMakeAccount.Models.Email;
using System;
using System.Globalization;

namespace StartNewMakeAccount
{
    public class NameService : BaseFileService
    {
        public NameService(string path = "Data/names.txt") : base(path)
        {
        }

        public string PrettyName()
        {
            string prettyName = _data[new Random().Next(0, 3000)];
            string res = prettyName.ToLower().Trim();
            return new CultureInfo("en-US").TextInfo.ToTitleCase(res);
        }

        public  string GetFollowName()
        {
            string prettyName = _data[new Random().Next(0, 3000)];
            string res = prettyName.ToLower().Trim();
            return new CultureInfo("en-US").TextInfo.ToTitleCase(res);
        }
    }

    public sealed class EmailService : BaseFileService
    {
        public EmailService(string path = "Data/emails.txt") : base(path)
        {
        }

        public string[] GetEmails() => this._data;
    }
}
