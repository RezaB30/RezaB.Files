using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezaB.Files.Local
{
    public class LocalFileManager : ILocalFileManager
    {
        private string _rootPath;
        private Stack<string> navigationList;

        public string CurrentPath
        {
            get
            {
                return $"{string.Join(PathSeparator, new[] { _rootPath }.Concat(navigationList.Reverse()))}{PathSeparator}";
            }
        }

        public string PathSeparator
        {
            get
            {
                return "\\";
            }
        }

        public LocalFileManager(string rootPath)
        {
            if (rootPath.EndsWith(PathSeparator))
                rootPath = rootPath.Remove(rootPath.Length - PathSeparator.Length);
            _rootPath = rootPath;
            navigationList = new Stack<string>();
        }

        public FileManagerResult<bool> CreateDirectory(string directoryPath)
        {
            try
            {
                Directory.CreateDirectory(CurrentPath + directoryPath);
                return new FileManagerResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new FileManagerResult<bool>(false, ex);
            }
        }

        public FileManagerResult<bool> DirectoryExists(string directoryPath)
        {
            try
            {
                var result = Directory.Exists(CurrentPath + directoryPath);
                return new FileManagerResult<bool>(result);
            }
            catch (Exception ex)
            {
                return new FileManagerResult<bool>(false, ex);
            }
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

        public FileManagerResult<bool> FileExists(string fileName)
        {
            try
            {
                var result = File.Exists(CurrentPath + fileName);
                return new FileManagerResult<bool>(result);
            }
            catch (Exception ex)
            {
                return new FileManagerResult<bool>(false, ex);
            }
        }

        public FileManagerResult<IEnumerable<string>> GetDirectoryList()
        {
            try
            {
                var list = Directory.GetDirectories(CurrentPath);
                var result = list.Select(item => new DirectoryInfo(item).Name).ToArray();
                return new FileManagerResult<IEnumerable<string>>(result);
            }
            catch (Exception ex)
            {
                return new FileManagerResult<IEnumerable<string>>(ex);
            }
        }

        public FileManagerResult<Stream> GetFile(string fileName)
        {
            try
            {
                using (var fileStream = new FileStream(CurrentPath + fileName, FileMode.Open))
                {
                    var result = new MemoryStream();
                    fileStream.CopyTo(result);
                    if (result.CanSeek)
                        result.Seek(0, SeekOrigin.Begin);
                    return new FileManagerResult<Stream>(result);
                }
            }
            catch (Exception ex)
            {
                return new FileManagerResult<Stream>(ex);
            }
        }

        public FileManagerResult<IEnumerable<string>> GetFileList()
        {
            try
            {
                var list = Directory.GetFiles(CurrentPath);
                var result = list.Select(item => new FileInfo(item).Name).ToArray();
                return new FileManagerResult<IEnumerable<string>>(result);
            }
            catch (Exception ex)
            {
                return new FileManagerResult<IEnumerable<string>>(ex);
            }
        }

        public FileManagerResult<bool> GoToRootDirectory()
        {
            navigationList.Clear();
            return new FileManagerResult<bool>(true);
        }

        public FileManagerResult<bool> RemoveDirectory(string directoryPath)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(directoryPath))
                {
                    return new FileManagerResult<bool>(new Exception("Directory name is empty."));
                }
                Directory.Delete(CurrentPath + directoryPath, true);
                return new FileManagerResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new FileManagerResult<bool>(false, ex);
            }
        }

        public FileManagerResult<bool> RemoveFile(string fileName)
        {
            try
            {
                File.Delete(CurrentPath + fileName);
                return new FileManagerResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new FileManagerResult<bool>(false, ex);
            }
        }

        public FileManagerResult<bool> SaveFile(string fileName, Stream fileContents, bool forceOverwrite = false)
        {
            try
            {
                if (!forceOverwrite)
                {
                    if (FileExists(fileName).Result)
                        return new FileManagerResult<bool>(false, new Exception("File name exists."));
                }
                using (var fileStream = File.Create(CurrentPath + fileName))
                {
                    fileContents.CopyTo(fileStream);
                    fileContents.Flush();
                    fileStream.Flush();
                    fileStream.Close();
                    return new FileManagerResult<bool>(true);
                }
            }
            catch (Exception ex)
            {
                return new FileManagerResult<bool>(ex);
            }
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
    }
}
