using Bacchus.MVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bacchus.MVC.Dao;
using System.Windows.Forms;
using Bacchus.MVC.View.SousFamillesView;
using Bacchus.MVC.View.MarquesView;
using Bacchus.MVC.View.ArticlesView;


namespace Bacchus.MVC.Controller
{
    class ArticlesController
    {
        private ArticlesDao ArticlesDao;
        private SousFamillesDao SousFamillesDao;
        private MarquesDao MarquesDao;
        private FamillesDao FamillesDao;
        
        public ArticlesController()
        {
            ArticlesDao = new ArticlesDao();
            SousFamillesDao = new SousFamillesDao();
            MarquesDao = new MarquesDao();
            FamillesDao = new FamillesDao();
        }

        public List<Articles> FindArticlesByMarqueName(string MarqueName)
        {
            ArticlesDao ArticlesDao = new ArticlesDao();

            return ArticlesDao.FindArticlesByMarqueName(MarqueName);
        }

        public List<Articles> FindArticlesBySousFamilleName(string SousFamilleName)
        {

            return ArticlesDao.FindArticlesBySousFamilleName(SousFamilleName);
        }

        public Articles FindArticlesByRefArticle(string RefArticle)
        {
            return ArticlesDao.FindArticlesByRefArticle(RefArticle);
        }

        public Boolean FindArticleByRefArticle(string RefArticle)
        {
            return ArticlesDao.FindArticleByRefArticle(RefArticle);
        }

        /**public void DeleteArticle(Articles Article)
        {
            ArticlesDao.DeleteArticle(Article);
        }**/

        public void Delete_Article(ListViewItem ListViewItem)
        {
            if (ListViewItem == null)
                MessageBox.Show("Please select an article!");
            else
            {
                var RefArticle = ListViewItem.Text;
                if (FindArticleByRefArticle(RefArticle))
                {
                    ArticlesDao.DeleteArticle(FindArticlesByRefArticle(RefArticle));
                    if (!FindArticleByRefArticle(RefArticle))
                        MessageBox.Show("Delete succeed!");
                }
                else
                    MessageBox.Show("Article doesn't exist!");
            }
        }

        public Articles Modify_Article(ListViewItem ListViewItem)
        {
            if (ListViewItem == null)
            {
                MessageBox.Show("Please select an article!");
                return null;
            }
            else
            {
                return FindArticlesByRefArticle(ListViewItem.Text);
            }
        }

        public void FindAndModifyArticle(String RefArticle, String Description, String MarqueName, String FamilleName, String SousFamilleName,String PrixHT, String Quantite, ModifyArticle Form)
        {
            if (Description == "")
                MessageBox.Show("Please enter the Description!", "ERROR");

            else if (MarqueName == "")
                MessageBox.Show("Please enter the Marque Name!", "ERROR");

            else if (FamilleName == "")
                MessageBox.Show("Please enter the Famille Name!", "ERROR");

            else if (SousFamilleName == "")
                MessageBox.Show("Please enter the SousFamille Name!", "ERROR");

            else if (PrixHT == "")
                MessageBox.Show("Please enter the Price!", "ERROR");

            else if (Quantite == "")
                MessageBox.Show("Please enter the Quantite!", "ERROR");

            else if (!PrixHT.Contains(","))
                MessageBox.Show("In our software, you must enter a price with ',' instead of '.'. If you want to enter an integer, please enter like '5,0'.");

            else if (!int.TryParse(Quantite, out var Number))
                MessageBox.Show("Please enter the right quantity!");

            else
            {
                if (!SousFamillesDao.FindSousFamilleBySousFamilleName(SousFamilleName))
                {
                    if (MessageBox.Show("SousFamille doesn't exsite! Do you want to add a new SousFamille?", "Confirm Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        AddSousFamille AddSousFamille = new AddSousFamille { StartPosition = FormStartPosition.CenterParent };
                        AddSousFamille.ShowDialog(Form);
                    }
                    else
                        return;
                }
                if (!MarquesDao.FindMarqueByMarqueName(MarqueName))
                {
                    if (MessageBox.Show("Marque doesn't exsite! Do you want to add a new Marque?", "Confirm Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        AddMarque AddMarque = new AddMarque { StartPosition = FormStartPosition.CenterParent };
                        AddMarque.ShowDialog(Form);
                    }
                    else
                        return;
                }

                SousFamilles SousFamille = SousFamillesDao.FindSousFamillesBySousFamilleName(SousFamilleName);
                Marques Marque = MarquesDao.FindMarquesByMarqueName(MarqueName);
                
                Articles Article = new Articles(RefArticle, Description, FamillesDao.FindFamillesByRefSousFamille(SousFamille.RefSousFamille), SousFamille, Marque, double.Parse(PrixHT), int.Parse(Quantite));

                if (!CompareArticles(ArticlesDao.FindArticlesByRefArticle(Article.RefArticle), Article))
                {
                    ArticlesDao.ModifyArticle(Article);
                    if (ArticlesDao.FindArticleByRefArticle(Article.RefArticle))
                    {
                        MessageBox.Show("Modify succeed!");
                        Form.Close();
                    }
                }
                else
                {
                    MessageBox.Show("The article can't be the same before and after modification!");
                    return;
                }
            }
        }

        public Boolean CompareArticles(Articles Article, Articles NewArticle)
        {
            if (Article.Description != NewArticle.Description ||
                Article.PrixHT != NewArticle.PrixHT || Article.Quantite != NewArticle.Quantite || Article.Marque.RefMarque
                != NewArticle.Marque.RefMarque || Article.SousFamille.RefSousFamille != NewArticle.SousFamille.RefSousFamille)
                return false;
            else
                return true;
        }

        public void AddArticle(String RefArticle, String Description, String MarqueName, String FamilleName, String SousFamilleName, String PrixHT, String Quantite, Form Form)
        {
            if (RefArticle == "")
                MessageBox.Show("Please enter the RefArticle!", "ERROR");

            else if (Description == "")
                MessageBox.Show("Please enter the Description!", "ERROR");

            else if (PrixHT == "")
                MessageBox.Show("Please enter the Price!", "ERROR");

            else if (Quantite == "")
                MessageBox.Show("Please enter the Quantite!", "ERROR");

            else if (MarqueName == "")
                MessageBox.Show("Please enter the Marque Name!", "ERROR");

            else if (FamilleName == "")
                MessageBox.Show("Please enter the Famille Name!", "ERROR");

            else if (SousFamilleName == "")
                MessageBox.Show("Please enter the SousFamille Name!", "ERROR");

            else if (RefArticle.Length >= 15)
                MessageBox.Show("RefArticle is too long! The maximum length of the RefArticle is 15!");

            else if (!PrixHT.Contains(","))
                MessageBox.Show("In our software, you must enter a price with ',' instead of '.'. If you want to enter an integer, please enter like '5,0'.");

            //else if (!int.TryParse(PrixHT, out var Prix))
            //  MessageBox.Show("Please enter the right price!");

            else if (!int.TryParse(Quantite, out var Number))
                MessageBox.Show("Please enter the right quantity!");

            else if (FindArticleByRefArticle(RefArticle))
                MessageBox.Show("Article Already exsited!", "ERROR");

            else
            {
                if (!SousFamillesDao.FindSousFamilleBySousFamilleName(SousFamilleName))
                {
                    var Result = MessageBox.Show("SousFamille doesn't exsite! Do you want to add a new SousFamille?", "Confirm Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (Result == DialogResult.OK)
                    {
                        AddSousFamille AddSousFamille = new AddSousFamille { StartPosition = FormStartPosition.CenterParent };
                        AddSousFamille.ShowDialog(Form);
                    }
                    else
                        return;

                }
                if (!MarquesDao.FindMarqueByMarqueName(MarqueName))
                {
                    var Result = MessageBox.Show("Marque doesn't exsite! Do you want to add a new Marque?", "Confirm Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (Result == DialogResult.OK)
                    {
                        Marques NewMarque = new Marques(MarqueName);
                        NewMarque.RefMarque = MarquesDao.GetMaxRefMarque() + 1;
                        MarquesDao.AddMarque(NewMarque);
                    }
                    else
                        return;
                }

                SousFamilles SousFamille = SousFamillesDao.FindSousFamillesBySousFamilleName(SousFamilleName);
                Marques Marque = MarquesDao.FindMarquesByMarqueName(MarqueName);
                Articles Article = new Articles(RefArticle, Description, FamillesDao.FindFamillesByRefSousFamille(SousFamille.RefSousFamille), SousFamille, Marque, double.Parse(PrixHT), int.Parse(Quantite));
                ArticlesDao.AddArticle(Article);

                if (ArticlesDao.FindArticleByRefArticle(RefArticle))
                {
                    MessageBox.Show("Add Article succeed!");
                    Form.Close();
                }
            }
        }

        /**public void Delete_Article()
        {
            if (ListView.FocusedItem == null)
                MessageBox.Show("Please select an article!");
            else
            {
                var RefArticle = ListView.FocusedItem.Text;
                if (ArticlesController.FindArticleByRefArticle(RefArticle))
                {
                    ArticlesController.DeleteArticle(ArticlesController.FindArticlesByRefArticle(RefArticle));
                    if (!ArticlesController.FindArticleByRefArticle(RefArticle))
                        MessageBox.Show("Delete succeed!");
                }
                else
                    MessageBox.Show("Article doesn't exist!");
            }
        }**/
    }
}
