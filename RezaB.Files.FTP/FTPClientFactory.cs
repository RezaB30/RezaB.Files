using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezaB.Files.FTP
{
    public static class FTPClientFactory
    {
        public static FTPClientBase CreateFTPClient(string url, string username, string password)
        {
            if (url.ToLower().StartsWith("ftp://"))
            {
                return new FTP.FTPClient(url, username, password);
            }
            else if (url.ToLower().StartsWith("sftp://"))
            {
                return new SFTP.SFTPClient(url, username, password);
            }

            throw new NotSupportedException("Url must start with ftp:// or sftp://");
        }
    }
}
