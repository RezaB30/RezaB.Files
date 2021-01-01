using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezaB.Files.FTP
{
    /// <summary>
    /// Base class for all FTP clients.
    /// </summary>
    public abstract class FTPClientBase : IFTPClient
    {
        protected readonly string _url;
        protected readonly string _username;
        protected readonly string _password;
        protected readonly Stack<string> navigationList;

        internal FTPClientBase(string url, string username, string password)
        {
            if (url.EndsWith(PathSeparator))
                url = url.Remove(url.Length - PathSeparator.Length);
            _url = url;
            _username = username;
            _password = password;
            navigationList = new Stack<string>();
        }

        public string CurrentPath
        {
            get
            {
                return $"{string.Join(PathSeparator, new[] { _url }.Concat(navigationList.Reverse()))}{PathSeparator}";
            }
        }

        public string PathSeparator
        {
            get
            {
                return "/";
            }
        }

        public abstract FileManagerResult<bool> CreateDirectory(string directoryPath);
        public abstract FileManagerResult<bool> DirectoryExists(string directoryPath);
        public abstract FileManagerResult<bool> FileExists(string fileName);
        public abstract FileManagerResult<IEnumerable<string>> GetDirectoryList();
        public abstract FileManagerResult<Stream> GetFile(string fileName);
        public abstract FileManagerResult<IEnumerable<string>> GetFileList();
        public abstract FileManagerResult<bool> RemoveDirectory(string directoryPath);
        public abstract FileManagerResult<bool> RemoveFile(string fileName);
        public abstract FileManagerResult<bool> SaveFile(string fileName, Stream fileContents, bool forceOverwrite = false);

        public FileManagerResult<bool> GoToRootDirectory()
        {
            navigationList.Clear();
            return new FileManagerResult<bool>(true);
        }

        public FileManagerResult<bool> StepBackOneDirectory()
        {
            if (navigationList.Any())
            {
                navigationList.Pop();
                return new FileManagerResult<bool>(true);
            }

            return new FileManagerResult<bool>(false);
        }

        public FileManagerResult<bool> EnterDirectoryPath(string directoryPath)
        {
            var results = DirectoryExists(directoryPath);
            if (!results.Result)
                return results;
            var collection = directoryPath.Split(new[] { PathSeparator }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in collection)
            {
                navigationList.Push(item);
            }

            return new FileManagerResult<bool>(true);
        }
    }
}
