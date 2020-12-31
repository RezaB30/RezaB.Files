using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezaB.Files.FTP.SFTP
{
    public class SFTPClient : FTPClientBase
    {
        private readonly string _host;
        private readonly string _root;
        private string _subrootPath
        {
            get
            {
                var path = string.Join("/", navigationList.Reverse());
                if (!string.IsNullOrEmpty(path))
                    path += "/";
                if (!string.IsNullOrEmpty(_root))
                    path = _root + path;
                return path;
            }
        }

        internal SFTPClient(string url, string username, string password) : base(url, username, password)
        {
            var hostAndRoot = url.ToLower().StartsWith("sftp://") ? url.Substring(7) : url;
            var parts = hostAndRoot.Split('/');
            _host = parts.FirstOrDefault();
            _root = parts.Count() > 1 ? $"{string.Join("/", parts.Skip(1))}/" : string.Empty;
        }

        public override FileManagerResult<bool> CreateDirectory(string directoryPath)
        {
            try
            {
                using (var client = CreateClient())
                {
                    client.Connect();
                    client.CreateDirectory($"{_subrootPath}{directoryPath}");
                    client.Disconnect();
                }
                return new FileManagerResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new FileManagerResult<bool>(false, ex);
            }
        }

        public override FileManagerResult<bool> DirectoryExists(string directoryPath)
        {
            if (directoryPath.Contains("."))
            {
                return new FileManagerResult<bool>(false);
            }
            if (string.IsNullOrEmpty(directoryPath))
            {
                return new FileManagerResult<bool>(true);
            }
            try
            {
                using (var client = CreateClient())
                {
                    client.Connect();
                    client.ChangeDirectory($"{_subrootPath}{directoryPath}");
                    client.Disconnect();
                }
                return new FileManagerResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new FileManagerResult<bool>(false, ex);
            }
        }

        public override FileManagerResult<bool> FileExists(string fileName)
        {
            try
            {
                using (var client = CreateClient())
                {
                    client.Connect();
                    client.GetAttributes($"{_subrootPath}{fileName}");
                    client.Disconnect();
                }
                return new FileManagerResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new FileManagerResult<bool>(false, ex);
            }
        }

        public override FileManagerResult<IEnumerable<string>> GetDirectoryList()
        {
            try
            {
                using (var client = CreateClient())
                {
                    client.Connect();
                    var list = client.ListDirectory(_subrootPath);
                    client.Disconnect();

                    return new FileManagerResult<IEnumerable<string>>(list.Where(item => item.IsDirectory && item.Name != ".." && item.Name != ".").Select(item => item.Name).ToArray());
                }
            }
            catch (Exception ex)
            {
                return new FileManagerResult<IEnumerable<string>>(ex);
            }
        }

        public override FileManagerResult<Stream> GetFile(string fileName)
        {
            try
            {
                using (var client = CreateClient())
                {
                    client.Connect();
                    Stream output = new MemoryStream();
                    client.DownloadFile($"{_subrootPath}{fileName}", output);
                    output.Seek(0, SeekOrigin.Begin);
                    client.Disconnect();

                    return new FileManagerResult<Stream>(output);
                }
            }
            catch (Exception ex)
            {
                return new FileManagerResult<Stream>(ex);
            }
        }

        public override FileManagerResult<IEnumerable<string>> GetFileList()
        {
            try
            {
                using (var client = CreateClient())
                {
                    client.Connect();
                    var list = client.ListDirectory(_subrootPath);
                    client.Disconnect();

                    return new FileManagerResult<IEnumerable<string>>(list.Where(item => !item.IsDirectory && item.Name != ".." && item.Name != ".").Select(item => item.Name).ToArray());
                }
            }
            catch (Exception ex)
            {
                return new FileManagerResult<IEnumerable<string>>(ex);
            }
        }

        public override FileManagerResult<bool> RemoveDirectory(string directoryPath)
        {
            if (string.IsNullOrWhiteSpace(directoryPath))
            {
                return new FileManagerResult<bool>(new Exception("Directory name is empty."));
            }
            return RemoveDirectoryInternal(directoryPath, true);
        }

        public override FileManagerResult<bool> RemoveFile(string fileName)
        {
            try
            {
                using (var client = CreateClient())
                {
                    client.Connect();
                    client.DeleteFile($"{_subrootPath}{fileName}");
                    client.Disconnect();

                    return new FileManagerResult<bool>(true);
                }
            }
            catch (Exception ex)
            {
                return new FileManagerResult<bool>(false, ex);
            }
        }

        public override FileManagerResult<bool> SaveFile(string fileName, Stream fileContents, bool forceOverwrite = false)
        {
            try
            {
                if (fileContents.CanSeek)
                    fileContents.Seek(0, SeekOrigin.Begin);

                using (var client = CreateClient())
                {
                    client.Connect();
                    client.UploadFile(fileContents, $"{_subrootPath}{fileName}", forceOverwrite);
                    client.Disconnect();

                    return new FileManagerResult<bool>(true);
                }
            }
            catch (Exception ex)
            {
                return new FileManagerResult<bool>(false, ex);
            }
        }

        #region Private Methods
        private SftpClient CreateClient()
        {
            return new SftpClient(_host, _username, _password);
        }

        private FileManagerResult<bool> RemoveDirectoryInternal(string directoryPath, bool deleteAll)
        {
            try
            {
                using (var client = CreateClient())
                {
                    if (deleteAll)
                    {
                        // enter directory
                        var results = EnterDirectoryPath(directoryPath);
                        if (!results.Result)
                            return results;

                        {
                            // list files
                            var fileList = GetFileList();
                            if (fileList.InternalException != null)
                            {
                                StepBackOneDirectory();
                                return new FileManagerResult<bool>(false, fileList.InternalException);
                            }

                            // delete files from list
                            foreach (var file in fileList.Result)
                            {
                                results = RemoveFile(file);
                                if (!results.Result)
                                {
                                    StepBackOneDirectory();
                                    return results;
                                }
                            }
                        }

                        {
                            // list directories
                            var directoryList = GetDirectoryList();
                            if (directoryList.InternalException != null)
                            {
                                StepBackOneDirectory();
                                return new FileManagerResult<bool>(false, directoryList.InternalException);
                            }

                            // delete directories from list
                            foreach (var directory in directoryList.Result)
                            {
                                results = RemoveDirectoryInternal(directory, deleteAll);
                                if (!results.Result)
                                {
                                    StepBackOneDirectory();
                                    return results;
                                }
                            }
                        }

                        StepBackOneDirectory();
                        results = RemoveDirectoryInternal(directoryPath, false);
                        return results;
                    }
                    else
                    {
                        client.Connect();
                        client.DeleteDirectory($"{_subrootPath}{directoryPath}");
                        client.Disconnect();
                    }

                    return new FileManagerResult<bool>(true);
                }
            }
            catch (Exception ex)
            {
                return new FileManagerResult<bool>(false, ex);
            }
        }
        #endregion
    }
}
