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
    public partial class FormImport : Form
    {
        public String FilePath = string.Empty;
        ImportController  ImportController;

        public FormImport()
        {
            InitializeComponent();
            ImportController = new ImportController();
        }

        private void button_ChoisirUnFichier_Click(object sender, EventArgs e)
        {
            ImportController ImportController = new ImportController();
            FilePath = ImportController.ChooseFile();
            label_FichierImporte.Text = "FileName: " + System.IO.Path.GetFileName(FilePath);
        }

        private void FormImport_Load(object sender, EventArgs e)
        {

        }

        private void Button_ModeEcrasement_Click(object sender, EventArgs e)
        {
            
            this.progressBar.Visible = true;
            ImportController.CsvImport(true, FilePath, this);
            Console.WriteLine(FilePath);
            ((FormMain)Owner).LoadListView();
            this.Close();
        }

        private void button_ModeAjout_Click(object sender, EventArgs e)
        {
            this.progressBar.Visible = true;
            ImportController.CsvImport(false, FilePath, this);
            ((FormMain)Owner).LoadListView();
            this.Close();
        }
    }
}
