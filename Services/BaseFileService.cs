using System.IO;
using System.Linq;

namespace StartNewMakeAccount.Models.Email
{
    public class BaseFileService
    {
        protected string[] _data;
        public BaseFileService(string path ) =>  this._data = File.ReadAllLines(path).ToArray();

    }
}