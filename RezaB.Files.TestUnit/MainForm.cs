using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RezaB.Files.TestUnit
{
    public partial class MainForm : Form
    {
        private readonly int toggleHeightDiff;
        public MainForm()
        {
            InitializeComponent();
            FileManagerTypeCombobox.SelectedIndex = 0;
            if (File.Exists("settings.ini"))
            {
                try
                {
                    var fileContents = File.ReadAllLines("settings.ini");
                    var settings = fileContents.Select(line => line.Split('\t')).ToDictionary(parts => parts[0], parts => parts[1]);

                    FileManagerTypeCombobox.SelectedItem = settings[FileManagerTypeCombobox.Name];
                    LocalRootTextbox.Text = settings[LocalRootTextbox.Name];
                    FTPRootTextbox.Text = settings[FTPRootTextbox.Name];
                    FTPUsernameTextbox.Text = settings[FTPUsernameTextbox.Name];
                    FTPPasswordTextbox.Text = settings[FTPPasswordTextbox.Name];
                    OverwriteExistingCheckbox.Checked = Convert.ToBoolean(settings[OverwriteExistingCheckbox.Name]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error loading settings from settings.ini", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }

            toggleHeightDiff = FolderStructureGroupbox.Height - FolderStructurePanel.Height;
        }

        private void LocalRootBrowseButton_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.RootFolder = Environment.SpecialFolder.MyComputer;
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                LocalRootTextbox.Text = dialog.SelectedPath;
            }
        }

        private void SaveSettingsButton_Click(object sender, EventArgs e)
        {
            var settings = new Dictionary<string, string>();
            settings.Add(FileManagerTypeCombobox.Name, FileManagerTypeCombobox.Text);
            settings.Add(OverwriteExistingCheckbox.Name, OverwriteExistingCheckbox.Checked.ToString());
            settings.Add(LocalRootTextbox.Name, LocalRootTextbox.Text);
            settings.Add(FTPRootTextbox.Name, FTPRootTextbox.Text);
            settings.Add(FTPUsernameTextbox.Name, FTPUsernameTextbox.Text);
            settings.Add(FTPPasswordTextbox.Name, FTPPasswordTextbox.Text);
            var fileContents = string.Join(Environment.NewLine, settings.Select(s => $"{s.Key}\t{s.Value}"));
            File.WriteAllText("settings.ini", fileContents);
        }

        private void ReloadButton_Click(object sender, EventArgs e)
        {
            FolderStructureTreeview.Nodes.Clear();
            InternalFileManager = GetFileManager();
            FolderStructureTreeview.PathSeparator = InternalFileManager.PathSeparator;
            var list = GetDirectoryAndFileList();
            if (list != null)
            {
                var rootNode = FolderStructureTreeview.Nodes.Add("<-root->");
                rootNode.ContextMenuStrip = DirectoryNodeContextmenustrip;
                rootNode.Nodes.AddRange(list);
                rootNode.Expand();
                RefreshPath();
            }
        }

        private void FolderStructureTreeview_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            EnterPath(e.Node);
        }

        private void FolderStructureTreeview_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            FolderStructureTreeview.SelectedNode = e.Node;
        }

        private void DirectoryNodeContextmenustrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            (sender as ContextMenuStrip).Close();
            if (e.ClickedItem == CreateDirectoryMenuItem)
            {
                CreateDirectory(FolderStructureTreeview.SelectedNode);
            }
            else if (e.ClickedItem == UploadFileMenuItem)
            {
                UploadFile(FolderStructureTreeview.SelectedNode);
            }
            else if (e.ClickedItem == RemoveDirectoryMenuItem)
            {
                RemoveDirectory(FolderStructureTreeview.SelectedNode);
            }
        }

        private void ToggleSettingsButton_Click(object sender, EventArgs e)
        {
            if (FolderStructureGroupbox.Dock != DockStyle.Fill)
                FolderStructureGroupbox.Dock = DockStyle.Fill;
            else
                FolderStructureGroupbox.Dock = DockStyle.None;
        }

        private void FolderStructureGroupbox_Resize(object sender, EventArgs e)
        {
            FolderStructurePanel.Height = FolderStructureGroupbox.Height - toggleHeightDiff;
        }

        private void FileNodeContextmenustrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            (sender as ContextMenuStrip).Close();
            if (e.ClickedItem == DownloadFileMenuItem)
            {
                DownloadFile(FolderStructureTreeview.SelectedNode);
            }
            else if (e.ClickedItem == RemoveFileMenuItem)
            {
                RemoveFile(FolderStructureTreeview.SelectedNode);
            }
        }
    }
}
