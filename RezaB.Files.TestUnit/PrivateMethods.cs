using RezaB.Files.FTP;
using RezaB.Files.Local;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RezaB.Files.TestUnit
{
    public partial class MainForm
    {
        private IFileManager InternalFileManager { get; set; }

        private IFileManager GetFileManager()
        {
            if (FileManagerTypeCombobox.SelectedItem as string == "Local")
            {
                return new LocalFileManager(LocalRootTextbox.Text);
            }
            else
            {
                return FTPClientFactory.CreateFTPClient(FTPRootTextbox.Text, FTPUsernameTextbox.Text, FTPPasswordTextbox.Text);
            }
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error in file manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void RefreshPath()
        {
            CurrentPathTextbox.Text = InternalFileManager.CurrentPath;
        }

        private TreeNode[] GetDirectoryAndFileList()
        {
            var directoryList = InternalFileManager.GetDirectoryList();
            var fileList = InternalFileManager.GetFileList();
            if (directoryList.InternalException != null || fileList.InternalException != null)
            {
                var errorMessage = (directoryList.InternalException?.Message ?? string.Empty) + (fileList.InternalException?.Message ?? string.Empty);
                ShowErrorMessage(errorMessage);
                return null;
            }
            return directoryList.Result.Select(item => new TreeNode(item) { ContextMenuStrip = DirectoryNodeContextmenustrip }).Concat(fileList.Result.Select(item => new TreeNode(item) { ContextMenuStrip = FileNodeContextmenustrip })).ToArray();
        }

        private bool EnterPath(TreeNode node)
        {
            var path = node.FullPath.Replace("<-root->", "");
            if (path.StartsWith("\\") || path.StartsWith("/"))
            {
                path = path.Substring(1);
            }
            InternalFileManager.GoToRootDirectory();
            var result = InternalFileManager.EnterDirectoryPath(path);
            if (result.InternalException != null)
            {
                ShowErrorMessage(result.InternalException.Message);
                return false;
            }
            else if (result.Result)
            {
                var list = GetDirectoryAndFileList();
                if (list != null)
                {
                    node.Nodes.Clear();
                    node.Nodes.AddRange(list);
                    node.Expand();
                    RefreshPath();
                }
            }

            return true;
        }

        private void CreateDirectory(TreeNode node)
        {
            var dialog = new CreateDirectoryForm();
            dialog.Owner = this;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                EnterPath(node);
                var result = InternalFileManager.CreateDirectory(dialog.NewDirectoryName);
                if (result.InternalException != null)
                {
                    ShowErrorMessage(result.InternalException.Message);
                }
                else if (result.Result)
                {
                    var newListNodes = GetDirectoryAndFileList();
                    if (newListNodes != null)
                    {
                        node.Nodes.Clear();
                        node.Nodes.AddRange(newListNodes);
                        node.Expand();
                    }
                }
            }
        }

        private void UploadFile(TreeNode node)
        {
            var dialog = new OpenFileDialog();
            dialog.CheckPathExists = dialog.CheckFileExists = true;
            dialog.Multiselect = false;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (var fileStream = new FileStream(dialog.FileName, FileMode.Open))
                {
                    if (EnterPath(node))
                    {
                        var result = InternalFileManager.SaveFile(dialog.SafeFileName, fileStream, OverwriteExistingCheckbox.Checked);
                        if (result.InternalException != null)
                        {
                            ShowErrorMessage(result.InternalException.Message);
                        }
                        else if (result.Result)
                        {
                            var newListNodes = GetDirectoryAndFileList();
                            if (newListNodes != null)
                            {
                                node.Nodes.Clear();
                                node.Nodes.AddRange(newListNodes);
                                node.Expand();
                            }
                        }
                    }
                }
            }
        }

        private void RemoveDirectory(TreeNode node)
        {
            if (MessageBox.Show("Are you sure?", "Delete Directory", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var selectedDirectoryName = node.Text;
                var parentNode = node.Parent;
                if (parentNode == null)
                {
                    ShowErrorMessage("Can not delete root.");
                    return;
                }
                if (EnterPath(parentNode))
                {
                    var result = InternalFileManager.RemoveDirectory(selectedDirectoryName);
                    if (result.InternalException != null)
                    {
                        ShowErrorMessage(result.InternalException.Message);
                    }
                    else if (result.Result)
                    {
                        EnterPath(parentNode);
                    }
                }
            }
        }

        private void DownloadFile(TreeNode node)
        {
            var dialog = new FolderBrowserDialog();
            dialog.RootFolder = Environment.SpecialFolder.MyComputer;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                using (var result = InternalFileManager.GetFile(node.Text))
                {
                    if (result.InternalException != null)
                    {
                        ShowErrorMessage(result.InternalException.Message);
                        return;
                    }

                    using (var fileStream = File.Create($"{dialog.SelectedPath}\\{node.Text}"))
                    {
                        result.Result.CopyTo(fileStream);
                        result.Result.Flush();
                        fileStream.Flush();
                        fileStream.Close();
                    }
                }
            }
        }

        private void RemoveFile(TreeNode node)
        {
            if (MessageBox.Show("Are you sure?", "Delete File", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var selectedFileName = node.Text;
                var parentNode = node.Parent;
                if (EnterPath(parentNode))
                {
                    var result = InternalFileManager.RemoveFile(selectedFileName);
                    if (result.InternalException != null)
                    {
                        ShowErrorMessage(result.InternalException.Message);
                        return;
                    }
                    else if (result.Result)
                    {
                        EnterPath(parentNode);
                    }
                }
            }
        }
    }
}
