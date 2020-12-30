
namespace RezaB.Files.TestUnit
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.FileManagerTypeGroupbox = new System.Windows.Forms.GroupBox();
            this.OverwriteExistingCheckbox = new System.Windows.Forms.CheckBox();
            this.SaveSettingsButton = new System.Windows.Forms.Button();
            this.FTPSettingsGroupbox = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.FTPRootTextbox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.FTPUsernameTextbox = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.FTPPasswordTextbox = new System.Windows.Forms.TextBox();
            this.LocalSettingsGroupbox = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.LocalRootTextbox = new System.Windows.Forms.TextBox();
            this.LocalRootBrowseButton = new System.Windows.Forms.Button();
            this.FileManagerTypeCombobox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.FolderStructureGroupbox = new System.Windows.Forms.GroupBox();
            this.CurrentPathTextbox = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.ReloadButton = new System.Windows.Forms.Button();
            this.FolderStructurePanel = new System.Windows.Forms.Panel();
            this.FolderStructureTreeview = new System.Windows.Forms.TreeView();
            this.DirectoryNodeContextmenustrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.CreateDirectoryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.UploadFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveDirectoryMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FileNodeContextmenustrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DownloadFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RemoveFileMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToggleSettingsButton = new System.Windows.Forms.Button();
            this.FileManagerTypeGroupbox.SuspendLayout();
            this.FTPSettingsGroupbox.SuspendLayout();
            this.LocalSettingsGroupbox.SuspendLayout();
            this.FolderStructureGroupbox.SuspendLayout();
            this.FolderStructurePanel.SuspendLayout();
            this.DirectoryNodeContextmenustrip.SuspendLayout();
            this.FileNodeContextmenustrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // FileManagerTypeGroupbox
            // 
            this.FileManagerTypeGroupbox.Controls.Add(this.OverwriteExistingCheckbox);
            this.FileManagerTypeGroupbox.Controls.Add(this.SaveSettingsButton);
            this.FileManagerTypeGroupbox.Controls.Add(this.FTPSettingsGroupbox);
            this.FileManagerTypeGroupbox.Controls.Add(this.LocalSettingsGroupbox);
            this.FileManagerTypeGroupbox.Controls.Add(this.FileManagerTypeCombobox);
            this.FileManagerTypeGroupbox.Controls.Add(this.label1);
            this.FileManagerTypeGroupbox.Location = new System.Drawing.Point(12, 12);
            this.FileManagerTypeGroupbox.Name = "FileManagerTypeGroupbox";
            this.FileManagerTypeGroupbox.Size = new System.Drawing.Size(471, 249);
            this.FileManagerTypeGroupbox.TabIndex = 0;
            this.FileManagerTypeGroupbox.TabStop = false;
            this.FileManagerTypeGroupbox.Text = "File Manager Type";
            // 
            // OverwriteExistingCheckbox
            // 
            this.OverwriteExistingCheckbox.AutoSize = true;
            this.OverwriteExistingCheckbox.Location = new System.Drawing.Point(233, 21);
            this.OverwriteExistingCheckbox.Name = "OverwriteExistingCheckbox";
            this.OverwriteExistingCheckbox.Size = new System.Drawing.Size(134, 17);
            this.OverwriteExistingCheckbox.TabIndex = 1;
            this.OverwriteExistingCheckbox.Text = "Overwrite Existing Files";
            this.OverwriteExistingCheckbox.UseVisualStyleBackColor = true;
            // 
            // SaveSettingsButton
            // 
            this.SaveSettingsButton.Location = new System.Drawing.Point(384, 215);
            this.SaveSettingsButton.Name = "SaveSettingsButton";
            this.SaveSettingsButton.Size = new System.Drawing.Size(75, 23);
            this.SaveSettingsButton.TabIndex = 4;
            this.SaveSettingsButton.Text = "Save";
            this.SaveSettingsButton.UseVisualStyleBackColor = true;
            this.SaveSettingsButton.Click += new System.EventHandler(this.SaveSettingsButton_Click);
            // 
            // FTPSettingsGroupbox
            // 
            this.FTPSettingsGroupbox.Controls.Add(this.label3);
            this.FTPSettingsGroupbox.Controls.Add(this.FTPRootTextbox);
            this.FTPSettingsGroupbox.Controls.Add(this.label5);
            this.FTPSettingsGroupbox.Controls.Add(this.FTPUsernameTextbox);
            this.FTPSettingsGroupbox.Controls.Add(this.label4);
            this.FTPSettingsGroupbox.Controls.Add(this.FTPPasswordTextbox);
            this.FTPSettingsGroupbox.Location = new System.Drawing.Point(6, 104);
            this.FTPSettingsGroupbox.Name = "FTPSettingsGroupbox";
            this.FTPSettingsGroupbox.Size = new System.Drawing.Size(459, 105);
            this.FTPSettingsGroupbox.TabIndex = 3;
            this.FTPSettingsGroupbox.TabStop = false;
            this.FTPSettingsGroupbox.Text = "FTP/SFTP File Manager Settings";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "FTP/SFTP Root:";
            // 
            // FTPRootTextbox
            // 
            this.FTPRootTextbox.Location = new System.Drawing.Point(100, 19);
            this.FTPRootTextbox.Name = "FTPRootTextbox";
            this.FTPRootTextbox.Size = new System.Drawing.Size(272, 20);
            this.FTPRootTextbox.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 74);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 7;
            this.label5.Text = "Password:";
            // 
            // FTPUsernameTextbox
            // 
            this.FTPUsernameTextbox.Location = new System.Drawing.Point(100, 45);
            this.FTPUsernameTextbox.Name = "FTPUsernameTextbox";
            this.FTPUsernameTextbox.Size = new System.Drawing.Size(272, 20);
            this.FTPUsernameTextbox.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 48);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Username:";
            // 
            // FTPPasswordTextbox
            // 
            this.FTPPasswordTextbox.Location = new System.Drawing.Point(100, 71);
            this.FTPPasswordTextbox.Name = "FTPPasswordTextbox";
            this.FTPPasswordTextbox.Size = new System.Drawing.Size(272, 20);
            this.FTPPasswordTextbox.TabIndex = 2;
            // 
            // LocalSettingsGroupbox
            // 
            this.LocalSettingsGroupbox.Controls.Add(this.label2);
            this.LocalSettingsGroupbox.Controls.Add(this.LocalRootTextbox);
            this.LocalSettingsGroupbox.Controls.Add(this.LocalRootBrowseButton);
            this.LocalSettingsGroupbox.Location = new System.Drawing.Point(6, 46);
            this.LocalSettingsGroupbox.Name = "LocalSettingsGroupbox";
            this.LocalSettingsGroupbox.Size = new System.Drawing.Size(459, 52);
            this.LocalSettingsGroupbox.TabIndex = 2;
            this.LocalSettingsGroupbox.TabStop = false;
            this.LocalSettingsGroupbox.Text = "Local File Manager Settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 22);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Local Root:";
            // 
            // LocalRootTextbox
            // 
            this.LocalRootTextbox.Location = new System.Drawing.Point(100, 19);
            this.LocalRootTextbox.Name = "LocalRootTextbox";
            this.LocalRootTextbox.ReadOnly = true;
            this.LocalRootTextbox.Size = new System.Drawing.Size(272, 20);
            this.LocalRootTextbox.TabIndex = 0;
            // 
            // LocalRootBrowseButton
            // 
            this.LocalRootBrowseButton.Location = new System.Drawing.Point(378, 17);
            this.LocalRootBrowseButton.Name = "LocalRootBrowseButton";
            this.LocalRootBrowseButton.Size = new System.Drawing.Size(75, 23);
            this.LocalRootBrowseButton.TabIndex = 1;
            this.LocalRootBrowseButton.Text = "Browse";
            this.LocalRootBrowseButton.UseVisualStyleBackColor = true;
            this.LocalRootBrowseButton.Click += new System.EventHandler(this.LocalRootBrowseButton_Click);
            // 
            // FileManagerTypeCombobox
            // 
            this.FileManagerTypeCombobox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.FileManagerTypeCombobox.FormattingEnabled = true;
            this.FileManagerTypeCombobox.Items.AddRange(new object[] {
            "Local",
            "FTP/SFTP"});
            this.FileManagerTypeCombobox.Location = new System.Drawing.Point(106, 19);
            this.FileManagerTypeCombobox.Name = "FileManagerTypeCombobox";
            this.FileManagerTypeCombobox.Size = new System.Drawing.Size(121, 21);
            this.FileManagerTypeCombobox.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Type:";
            // 
            // FolderStructureGroupbox
            // 
            this.FolderStructureGroupbox.Controls.Add(this.ToggleSettingsButton);
            this.FolderStructureGroupbox.Controls.Add(this.CurrentPathTextbox);
            this.FolderStructureGroupbox.Controls.Add(this.label6);
            this.FolderStructureGroupbox.Controls.Add(this.ReloadButton);
            this.FolderStructureGroupbox.Controls.Add(this.FolderStructurePanel);
            this.FolderStructureGroupbox.Location = new System.Drawing.Point(12, 267);
            this.FolderStructureGroupbox.Name = "FolderStructureGroupbox";
            this.FolderStructureGroupbox.Size = new System.Drawing.Size(471, 269);
            this.FolderStructureGroupbox.TabIndex = 1;
            this.FolderStructureGroupbox.TabStop = false;
            this.FolderStructureGroupbox.Text = "Folder Structure";
            this.FolderStructureGroupbox.Resize += new System.EventHandler(this.FolderStructureGroupbox_Resize);
            // 
            // CurrentPathTextbox
            // 
            this.CurrentPathTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.CurrentPathTextbox.Location = new System.Drawing.Point(4, 66);
            this.CurrentPathTextbox.Name = "CurrentPathTextbox";
            this.CurrentPathTextbox.ReadOnly = true;
            this.CurrentPathTextbox.Size = new System.Drawing.Size(463, 13);
            this.CurrentPathTextbox.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(69, 13);
            this.label6.TabIndex = 2;
            this.label6.Text = "Current Path:";
            // 
            // ReloadButton
            // 
            this.ReloadButton.Location = new System.Drawing.Point(3, 19);
            this.ReloadButton.Name = "ReloadButton";
            this.ReloadButton.Size = new System.Drawing.Size(75, 23);
            this.ReloadButton.TabIndex = 0;
            this.ReloadButton.Text = "Reload";
            this.ReloadButton.UseVisualStyleBackColor = true;
            this.ReloadButton.Click += new System.EventHandler(this.ReloadButton_Click);
            // 
            // FolderStructurePanel
            // 
            this.FolderStructurePanel.BackColor = System.Drawing.SystemColors.Window;
            this.FolderStructurePanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FolderStructurePanel.Controls.Add(this.FolderStructureTreeview);
            this.FolderStructurePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FolderStructurePanel.Location = new System.Drawing.Point(3, 85);
            this.FolderStructurePanel.Name = "FolderStructurePanel";
            this.FolderStructurePanel.Size = new System.Drawing.Size(465, 181);
            this.FolderStructurePanel.TabIndex = 0;
            // 
            // FolderStructureTreeview
            // 
            this.FolderStructureTreeview.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.FolderStructureTreeview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FolderStructureTreeview.Location = new System.Drawing.Point(0, 0);
            this.FolderStructureTreeview.Name = "FolderStructureTreeview";
            this.FolderStructureTreeview.Size = new System.Drawing.Size(463, 179);
            this.FolderStructureTreeview.TabIndex = 0;
            this.FolderStructureTreeview.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.FolderStructureTreeview_NodeMouseClick);
            this.FolderStructureTreeview.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.FolderStructureTreeview_NodeMouseDoubleClick);
            // 
            // DirectoryNodeContextmenustrip
            // 
            this.DirectoryNodeContextmenustrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CreateDirectoryMenuItem,
            this.UploadFileMenuItem,
            this.RemoveDirectoryMenuItem});
            this.DirectoryNodeContextmenustrip.Name = "DirectoryNodeContextmenustrip";
            this.DirectoryNodeContextmenustrip.Size = new System.Drawing.Size(169, 70);
            this.DirectoryNodeContextmenustrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.DirectoryNodeContextmenustrip_ItemClicked);
            // 
            // CreateDirectoryMenuItem
            // 
            this.CreateDirectoryMenuItem.Name = "CreateDirectoryMenuItem";
            this.CreateDirectoryMenuItem.Size = new System.Drawing.Size(168, 22);
            this.CreateDirectoryMenuItem.Text = "Create Directory";
            // 
            // UploadFileMenuItem
            // 
            this.UploadFileMenuItem.Name = "UploadFileMenuItem";
            this.UploadFileMenuItem.Size = new System.Drawing.Size(168, 22);
            this.UploadFileMenuItem.Text = "Upload File";
            // 
            // RemoveDirectoryMenuItem
            // 
            this.RemoveDirectoryMenuItem.Name = "RemoveDirectoryMenuItem";
            this.RemoveDirectoryMenuItem.Size = new System.Drawing.Size(168, 22);
            this.RemoveDirectoryMenuItem.Text = "Remove Directory";
            // 
            // FileNodeContextmenustrip
            // 
            this.FileNodeContextmenustrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DownloadFileMenuItem,
            this.RemoveFileMenuItem});
            this.FileNodeContextmenustrip.Name = "FileNodeContextmenustrip";
            this.FileNodeContextmenustrip.Size = new System.Drawing.Size(150, 48);
            this.FileNodeContextmenustrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.FileNodeContextmenustrip_ItemClicked);
            // 
            // DownloadFileMenuItem
            // 
            this.DownloadFileMenuItem.Name = "DownloadFileMenuItem";
            this.DownloadFileMenuItem.Size = new System.Drawing.Size(149, 22);
            this.DownloadFileMenuItem.Text = "Download File";
            // 
            // RemoveFileMenuItem
            // 
            this.RemoveFileMenuItem.Name = "RemoveFileMenuItem";
            this.RemoveFileMenuItem.Size = new System.Drawing.Size(149, 22);
            this.RemoveFileMenuItem.Text = "Remove File";
            // 
            // ToggleSettingsButton
            // 
            this.ToggleSettingsButton.Location = new System.Drawing.Point(359, 19);
            this.ToggleSettingsButton.Name = "ToggleSettingsButton";
            this.ToggleSettingsButton.Size = new System.Drawing.Size(100, 23);
            this.ToggleSettingsButton.TabIndex = 1;
            this.ToggleSettingsButton.Text = "Toggle Settings";
            this.ToggleSettingsButton.UseVisualStyleBackColor = true;
            this.ToggleSettingsButton.Click += new System.EventHandler(this.ToggleSettingsButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(495, 548);
            this.Controls.Add(this.FolderStructureGroupbox);
            this.Controls.Add(this.FileManagerTypeGroupbox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RezaB.Files Test Unit";
            this.FileManagerTypeGroupbox.ResumeLayout(false);
            this.FileManagerTypeGroupbox.PerformLayout();
            this.FTPSettingsGroupbox.ResumeLayout(false);
            this.FTPSettingsGroupbox.PerformLayout();
            this.LocalSettingsGroupbox.ResumeLayout(false);
            this.LocalSettingsGroupbox.PerformLayout();
            this.FolderStructureGroupbox.ResumeLayout(false);
            this.FolderStructureGroupbox.PerformLayout();
            this.FolderStructurePanel.ResumeLayout(false);
            this.DirectoryNodeContextmenustrip.ResumeLayout(false);
            this.FileNodeContextmenustrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox FileManagerTypeGroupbox;
        private System.Windows.Forms.ComboBox FileManagerTypeCombobox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox LocalRootTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button LocalRootBrowseButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox FTPRootTextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox FTPPasswordTextbox;
        private System.Windows.Forms.TextBox FTPUsernameTextbox;
        private System.Windows.Forms.GroupBox LocalSettingsGroupbox;
        private System.Windows.Forms.GroupBox FTPSettingsGroupbox;
        private System.Windows.Forms.Button SaveSettingsButton;
        private System.Windows.Forms.GroupBox FolderStructureGroupbox;
        private System.Windows.Forms.Panel FolderStructurePanel;
        private System.Windows.Forms.Button ReloadButton;
        private System.Windows.Forms.TreeView FolderStructureTreeview;
        private System.Windows.Forms.TextBox CurrentPathTextbox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox OverwriteExistingCheckbox;
        private System.Windows.Forms.ContextMenuStrip DirectoryNodeContextmenustrip;
        private System.Windows.Forms.ContextMenuStrip FileNodeContextmenustrip;
        private System.Windows.Forms.ToolStripMenuItem CreateDirectoryMenuItem;
        private System.Windows.Forms.ToolStripMenuItem UploadFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RemoveDirectoryMenuItem;
        private System.Windows.Forms.ToolStripMenuItem DownloadFileMenuItem;
        private System.Windows.Forms.ToolStripMenuItem RemoveFileMenuItem;
        private System.Windows.Forms.Button ToggleSettingsButton;
    }
}

