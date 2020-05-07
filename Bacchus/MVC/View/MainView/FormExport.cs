using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Bacchus.MVC.Controller;

namespace Bacchus.MVC.View.MainView
{
    public partial class FormExport : Form
    {
        public String FilePath = string.Empty;
        public FormExport()
        {
            InitializeComponent();
        }

        private void button_ChoisirUnFichier_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "csv files (*.csv)|*.csv";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FilePath = openFileDialog.FileName;
                    label_FichierExporte.Text = "FileName: " + System.IO.Path.GetFileName(FilePath);
                }
            }
        }

        private void button_ExportToFichier_Click(object sender, EventArgs e)
        {
            ExportController ExportController = new ExportController();
            this.progressBar.Visible = true;
            ExportController.WriteFile(FilePath, this);
            this.Close();

        }
    }
}
