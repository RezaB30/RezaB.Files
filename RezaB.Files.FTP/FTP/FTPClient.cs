using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RezaB.Files.FTP.FTP
{
    /// <summary>
    /// FTP client for ftp protocol
    /// </summary>
    public class FTPClient : FTPClientBase
    {
        private readonly ICredentials ftpCredentials;
        /// <summary>
        /// Creates a FTP client.
        /// </summary>
        /// <param name="url">The root directory url that starts with <i>ftp://</i></param>
        /// <param name="username">Server username, set <i>null</i> for anonymous access.</param>
        /// <param name="password">Server password, set <i>null</i> for anonymous access.</param>
        internal FTPClient(string url, string username, string password) : base(url, username, password)
        {
            ftpCredentials = username != null ? new NetworkCredential(username, password) : new NetworkCredential("anonymous", "");
        }

        public override FileManagerResult<bool> CreateDirectory(string directoryPath)
        {
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(CurrentPath + directoryPath);
                request.Method = WebRequestMethods.Ftp.MakeDirectory;
                request.Credentials = ftpCredentials;
                using (var response = (FtpWebResponse)request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            var dataString = reader.ReadToEnd();
                        }
                    }
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
            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(CurrentPath + directoryPath);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = ftpCredentials;
                using (var response = (FtpWebResponse)request.GetResponse())
                {
                    return new FileManagerResult<bool>(true);
                }

            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    return new FileManagerResult<bool>(false);
                }
                else
                {
                    return new FileManagerResult<bool>(false, ex);
                }
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
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(CurrentPath + fileName);
                request.Method = WebRequestMethods.Ftp.GetFileSize;
                request.Credentials = ftpCredentials;
                using (var response = (FtpWebResponse)request.GetResponse())
                {
                    return new FileManagerResult<bool>(true);
                }
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                {
                    return new FileManagerResult<bool>(false);
                }
                else
                {
                    return new FileManagerResult<bool>(false, ex);
                }
            }
            catch (Exception ex)
            {
                return new FileManagerResult<bool>(false, ex);
            }
        }

        public override FileManagerResult<IEnumerable<string>> GetDirectoryList()
        {
            return GetListByType(FTPListType.Directory);
        }

        public override FileManagerResult<Stream> GetFile(string fileName)
        {
            try
            {
                var resultStream = new MemoryStream();
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(CurrentPath + fileName);
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                request.Credentials = ftpCredentials;
                request.KeepAlive = true;
                request.UseBinary = true;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    responseStream.CopyTo(resultStream);
                }

                resultStream.Seek(0, SeekOrigin.Begin);
                return new FileManagerResult<Stream>(resultStream);
            }
            catch (Exception ex)
            {
                return new FileManagerResult<Stream>(ex);
            }
        }

        public override FileManagerResult<IEnumerable<string>> GetFileList()
        {
            return GetListByType(FTPListType.File);
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
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(CurrentPath + fileName);
                request.Method = WebRequestMethods.Ftp.DeleteFile;
                request.Credentials = ftpCredentials;

                using (var response = (FtpWebResponse)request.GetResponse())
                {
                    using (var stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            var dataString = reader.ReadToEnd();
                        }
                    }
                }
                return new FileManagerResult<bool>(true);
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
                // check for overwrite
                if (!forceOverwrite)
                {
                    var fileExists = FileExists(fileName);
                    if (fileExists.Result)
                        return new FileManagerResult<bool>(false, new Exception("File name exists on the server."));
                }

                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(CurrentPath + fileName);
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.Credentials = ftpCredentials;
                request.KeepAlive = true;
                request.UseBinary = true;

                if (fileContents.CanSeek)
                    fileContents.Seek(0, SeekOrigin.Begin);

                using (var requestStream = request.GetRequestStream())
                {
                    var readableLength = Convert.ToInt32(fileContents.Length - fileContents.Position);
                    var buffer = new byte[readableLength];
                    fileContents.Read(buffer, 0, readableLength);
                    fileContents.Flush();

                    requestStream.Write(buffer, 0, buffer.Length);
                    requestStream.Flush();
                    requestStream.Close();
                }
                return new FileManagerResult<bool>(true);
            }
            catch (Exception ex)
            {
                return new FileManagerResult<bool>(false, ex);
            }
        }

        #region Private Methods
        private FileManagerResult<IEnumerable<string>> GetListByType(FTPListType listType)
        {
            try
            {
                FtpWebRequest requestDetails = (FtpWebRequest)WebRequest.Create(CurrentPath);
                requestDetails.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                requestDetails.Credentials = ftpCredentials;

                using (var response = (FtpWebResponse)requestDetails.GetResponse())
                using (var stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    var dataString = reader.ReadToEnd();
                    var data = dataString.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                    FtpWebRequest requestNames = (FtpWebRequest)WebRequest.Create(CurrentPath);
                    requestNames.Method = WebRequestMethods.Ftp.ListDirectory;
                    requestNames.Credentials = ftpCredentials;


                    using (var namesResponse = (FtpWebResponse)requestNames.GetResponse())
                    using (var namesStream = namesResponse.GetResponseStream())
                    using (var namesReader = new StreamReader(namesStream))
                    {
                        var namesString = namesReader.ReadToEnd();
                        var namesList = namesString.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
                        var endResults = data.Select(line => new { Details = line, Name = namesList.FirstOrDefault(nl => line.EndsWith(" " + nl)) });

                        switch (listType)
                        {
                            case FTPListType.File:
                                return new FileManagerResult<IEnumerable<string>>(endResults.Where(line => !line.Details.StartsWith("dr")).Select(line => line.Name));
                            case FTPListType.Directory:
                                return new FileManagerResult<IEnumerable<string>>(endResults.Where(line => line.Details.StartsWith("dr")).Select(line => line.Name));
                            default:
                                return new FileManagerResult<IEnumerable<string>>(new InvalidOperationException("List type is invalid."));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new FileManagerResult<IEnumerable<string>>(ex);
            }

        }

        private FileManagerResult<bool> RemoveDirectoryInternal(string directoryPath, bool deleteAll)
        {
            try
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
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(CurrentPath + directoryPath);
                    request.Method = WebRequestMethods.Ftp.RemoveDirectory;
                    request.Credentials = ftpCredentials;

                    using (var response = (FtpWebResponse)request.GetResponse())
                    {
                        using (var stream = response.GetResponseStream())
                        {
                            using (StreamReader reader = new StreamReader(stream))
                            {
                                var dataString = reader.ReadToEnd();
                            }
                        }
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
