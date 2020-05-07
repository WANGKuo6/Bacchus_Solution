using Bacchus.MVC.Model;
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

namespace Bacchus.MVC.View.ArticlesView
{
    public partial class ModifyArticle : Form
    {
        private Articles Articles;
        private ArticlesController ArticleController;
        private MarqueController MarqueController;
        private FamilleController FamilleController;
        private SousFamilleController SousFamilleController;
        private ModelList ModelList;
        private ModelListController ModelListController;

        public ModifyArticle(Articles Article)
        {
            InitializeComponent();

            Articles = Article;

            //ModelList = new ModelList();

            ArticleController = new ArticlesController();

            MarqueController = new MarqueController();

            FamilleController = new FamilleController();

            SousFamilleController = new SousFamilleController();

            ModelListController = new ModelListController();

            ModelList = ModelListController.GetAllModelList();

        }

        private void ModifyArticle_Load(object sender, EventArgs e)
        {
            this.label_ReferenceArticle.Text = this.Articles.RefArticle;
            this.textBox_Description.Text = this.Articles.Description;
            this.textBox_PrixHT.Text = this.Articles.PrixHT.ToString();
            this.textBox_Quantite.Text = this.Articles.Quantite.ToString();

            this.comboBox_Marque.DataSource = this.ModelList.Marques;
            this.comboBox_Marque.DisplayMember = "MarqueName";
            this.comboBox_Marque.ValueMember = "MarqueName";
            this.comboBox_Marque.SelectedIndex = this.comboBox_Marque.FindString(Articles.Marque.MarqueName);

            this.comboBox_Famille.DataSource = this.ModelList.Familles;
            this.comboBox_Famille.DisplayMember = "FamilleName";
            this.comboBox_Famille.ValueMember = "FamilleName";
            this.comboBox_Famille.SelectedIndex = this.comboBox_Famille.FindString(Articles.Famille.FamilleName);

            this.comboBox_SousFamille.DataSource = SousFamilleController.FindSousFamillesByFamilleName(this.comboBox_Famille.Text);
            this.comboBox_SousFamille.DisplayMember = "SousFamilleName";
            this.comboBox_SousFamille.ValueMember = "SousFamilleName";
            this.comboBox_SousFamille.SelectedIndex = this.comboBox_SousFamille.FindString(Articles.SousFamille.SousFamilleName);
        }

        private void button_Modify_Click(object sender, EventArgs e)
        {
            var Description = this.textBox_Description.Text;
            var MarqueName = this.comboBox_Marque.Text;
            var FamilleName = this.comboBox_Famille.Text;
            var SousFamilleName = this.comboBox_SousFamille.Text;
            var PrixHT = this.textBox_PrixHT.Text;
            var Quantite = this.textBox_Quantite.Text;
            var Refarticle = this.label_ReferenceArticle.Text;
            ArticleController.FindAndModifyArticle(Refarticle, Description, MarqueName, FamilleName, SousFamilleName, PrixHT, Quantite, this);


        }


        private void button_Cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
