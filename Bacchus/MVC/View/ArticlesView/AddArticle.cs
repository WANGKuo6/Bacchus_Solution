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
using Bacchus.MVC.Model;

namespace Bacchus.MVC.View.ArticlesView
{
    public partial class AddArticle : Form
    {
        private ArticlesController ArticlesController;
        private ModelListController ModelListController;
        private ModelList ModelList;
        public AddArticle()
        {
            InitializeComponent();
            ArticlesController = new ArticlesController();
            ModelListController = new ModelListController();
            ModelList = new ModelList();
            ModelList = ModelListController.GetAllModelList();
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            var RefArticle = this.textBox_RefArticle.Text;
            var Description = this.textBoxDescription.Text;
            var MarqueName = this.comboBox_Marque.Text;
            var FamilleName = this.comboBox_Famille.Text;
            var SousFamilleName = this.comboBox_SousFamille.Text;
            var PrixHT = this.textBox_PrixHT.Text;
            var Quantite = this.textBox_Quantite.Text;

            ArticlesController.AddArticle(RefArticle, Description, MarqueName, FamilleName, SousFamilleName, PrixHT, Quantite, this);
        }

        private void AddArticle_Load(object sender, EventArgs e)
        {
            this.comboBox_Marque.DataSource = this.ModelList.Marques;
            this.comboBox_Marque.DisplayMember = "MarqueName";
            this.comboBox_Marque.ValueMember = "MarqueName";

            this.comboBox_Famille.DataSource = this.ModelList.Familles;
            this.comboBox_Famille.DisplayMember = "FamilleName";
            this.comboBox_Famille.ValueMember = "FamilleName";

            this.comboBox_SousFamille.DataSource = this.ModelList.SousFamilles;
            this.comboBox_SousFamille.DisplayMember = "SousFamilleName";
            this.comboBox_SousFamille.ValueMember = "SousFamilleName";

            this.comboBox_Marque.SelectedIndex = -1;
            this.comboBox_Famille.SelectedIndex = -1;
            this.comboBox_SousFamille.SelectedIndex = -1;
        }
    }
}
