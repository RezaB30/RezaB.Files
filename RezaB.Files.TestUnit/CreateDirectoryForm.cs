using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RezaB.Files.TestUnit
{
    public partial class CreateDirectoryForm : Form
    {
        public string NewDirectoryName { get; private set; }

        public CreateDirectoryForm()
        {
            InitializeComponent();
        }

        private void CreateButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            NewDirectoryName = NewDirectoryNameTextbox.Text;
            Close();
        }
    }
}
