using System;
using System.Windows.Forms;

namespace PublicUtilities
{
    public partial class UcOpenFile : UserControl
    {
        public UcOpenFile()
        {
            InitializeComponent();
        }

        private string filter = "Text Files (*.txt)|*.txt|Dictionary Files (*.dic)|*.dic|All Files (*.*)|*.*";
        public string Filter
        {
            get { return this.filter; }
            set
            {
                this.filter = value;
            }
        }

        public string FilePath
        {
            get { return this.txtFile.Text; }
        }

        private void btnOpenFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openDlg = new OpenFileDialog();
            openDlg.CheckFileExists = true;
            openDlg.CheckPathExists = true;
            if (!string.IsNullOrEmpty(filter))
            {
                openDlg.Filter = filter;
            }
            DialogResult dlgResult = openDlg.ShowDialog();
            if (dlgResult == DialogResult.OK)
            {
                this.txtFile.Text = openDlg.FileName;
            }
        }
    }
}
