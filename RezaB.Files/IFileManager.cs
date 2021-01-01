using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RezaB.Files
{
    /// <summary>
    /// File management interface.
    /// </summary>
    public interface IFileManager
    {
        #region Properties
        /// <summary>
        /// Gets the current path.
        /// </summary>
        string CurrentPath { get; }
        /// <summary>
        /// Gets the path separator.
        /// </summary>
        string PathSeparator { get; }
        #endregion
        #region Navigation
        /// <summary>
        /// Steps back on directory on path.
        /// </summary>
        FileManagerResult<bool> StepBackOneDirectory();
        /// <summary>
        /// Goes to root directory.
        /// </summary>
        FileManagerResult<bool> GoToRootDirectory();
        /// <summary>
        /// Enters a path that is <b>under current directory</b>.
        /// </summary>
        /// <param name="directoryPath">The path to go to.</param>
        FileManagerResult<bool> EnterDirectoryPath(string directoryPath);
        /// <summary>
        /// Gets the list of directories in the current path.
        /// </summary>
        /// <returns></returns>
        FileManagerResult<IEnumerable<string>> GetDirectoryList();
        /// <summary>
        /// Gets the list of files in the current path.
        /// </summary>
        /// <returns></returns>
        FileManagerResult<IEnumerable<string>> GetFileList();
        /// <summary>
        /// Checks if the path exists <b>under current directory</b>.
        /// </summary>
        /// <param name="directoryPath">The path to check.</param>
        /// <returns></returns>
        FileManagerResult<bool> DirectoryExists(string directoryPath);
        /// <summary>
        /// Checks if a file exists in the current directory.
        /// </summary>
        /// <param name="fileName">The name of the file to check.</param>
        /// <returns></returns>
        FileManagerResult<bool> FileExists(string fileName);
        #endregion
        #region Directory Actions
        /// <summary>
        /// Creates a directory based on <b>the current path</b>.
        /// </summary>
        /// <param name="directoryPath">The directory path to create.</param>
        FileManagerResult<bool> CreateDirectory(string directoryPath);
        /// <summary>
        /// Removes a directory based on <b>the current path</b>.
        /// </summary>
        /// <param name="directoryPath">The directory path to remove.</param>
        FileManagerResult<bool> RemoveDirectory(string directoryPath);
        #endregion
        #region File Actions
        /// <summary>
        /// Retreives a file in the current directory.
        /// </summary>
        /// <param name="fileName">The name of the file to retrieve.</param>
        /// <returns></returns>
        FileManagerResult<Stream> GetFile(string fileName);
        /// <summary>
        /// Saves a given file in the current directory.
        /// </summary>
        /// <param name="fileName">The name of the file.</param>
        /// <param name="fileContents">The content of the file.</param>
        /// <param name="forceOverwrite">Should overwrite if exists.</param>
        FileManagerResult<bool> SaveFile(string fileName, Stream fileContents, bool forceOverwrite = false);
        /// <summary>
        /// Removes a file from the current directory.
        /// </summary>
        /// <param name="fileName">The name of the file to remove.</param>
        FileManagerResult<bool> RemoveFile(string fileName);
        #endregion
    }
}
