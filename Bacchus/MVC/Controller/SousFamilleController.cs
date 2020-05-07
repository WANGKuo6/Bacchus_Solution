using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bacchus.MVC.Model;
using Bacchus.MVC.Dao;
using System.Windows.Forms;
using Bacchus.MVC.View.SousFamillesView;

namespace Bacchus.MVC.Controller
{
    class SousFamilleController
    {
        private SousFamillesDao SousFamillesDao;
        private MarquesDao MarquesDao;
        private FamillesDao FamillesDao;
        

        public SousFamilleController()
        {
            SousFamillesDao = new SousFamillesDao();
            MarquesDao = new MarquesDao();
            FamillesDao = new FamillesDao();
            
        }

        public List<SousFamilles> FindSousFamillesByFamilleName(string FamilleName)
        {
            return SousFamillesDao.FindSousFamillesByFamilleName(FamilleName);
        }

        public List<SousFamilles> FindSousFamillesByMarqueName(string MarqueName)
        {
            return SousFamillesDao.FindSousFamillesByFamilleName(MarqueName);
        }

        public void Delete_SousFamille(String SousFamilleName, List<String> SousFamilleNames)
        {
            
            if (SousFamillesDao.FindSousFamilleBySousFamilleName(SousFamilleName))
            {
                SousFamillesDao.DeleteSousFamille(SousFamillesDao.FindSousFamillesBySousFamilleName(SousFamilleName));
                SousFamilleNames.Remove(SousFamilleName);
                if (!SousFamillesDao.FindSousFamilleBySousFamilleName(SousFamilleName))
                    MessageBox.Show("Delete succeed!");
            }
            else
                MessageBox.Show("SousFamille doesn't exist!");
        }

        public string Modify_SousFamille(String SousFamilleName, List<String> SousFamilleNames, String SelectedParentNode, String SelectedNode, Form Form)
        {

            if (SousFamillesDao.FindSousFamilleBySousFamilleName(SousFamilleName))
            {
                ModifySousFamilles ModifySousFamille = new ModifySousFamilles(SelectedParentNode, SelectedNode) { StartPosition = FormStartPosition.CenterParent };
                ModifySousFamille.ShowDialog(Form);

                if (ModifySousFamille.textBox_NewSousFamilleName.Text != "")
                {
                    var Index = SousFamilleNames.IndexOf(SousFamilleName);
                    SousFamilleNames.Remove(SousFamilleName);
                    SousFamilleNames.Insert(Index, ModifySousFamille.textBox_NewSousFamilleName.Text);
                    SousFamilleName = ModifySousFamille.textBox_NewSousFamilleName.Text;
                }
            }
            else
                MessageBox.Show("Famille doesn't exist!");

            return SousFamilleName;
        }

        public void IfModifySouFamilleOrNot(ModifySousFamilles Form, String LabelFamilleName, String LabelSousFamilleName)
        {
            var SousFamilleName = Form.textBox_NewSousFamilleName.Text;

            if (SousFamilleName == "")
            {
                MessageBox.Show("Please enter the SousFamille Name!", "ERROR");
                return;
            }

            else if (SousFamilleName == LabelFamilleName)
            {
                MessageBox.Show("SousFamilleName and FamilleName can't be the same!", "ERROR");
                Form.textBox_NewSousFamilleName.Text = "";
                return;
            }

            else if (SousFamilleName == LabelSousFamilleName)
            {
                MessageBox.Show("SousFamilleName can't be the same before and after modification!", "ERROR");
                Form.textBox_NewSousFamilleName.Text = "";
                return;
            }

            else if (SousFamilleName == "Familles")
            {
                MessageBox.Show("SousFamilleName can't be Familles!");
                Form.textBox_NewSousFamilleName.Text = "";
                return;
            }

            else if (SousFamilleName == "Marques")
            {
                MessageBox.Show("SousFamilleName can't be Marques!");
                Form.textBox_NewSousFamilleName.Text = "";
                return;
            }

            else if (SousFamilleName == "Tous les articles")
            {
                MessageBox.Show("SousFamilleName can't be Tous les articles!", "ERROR");
                Form.textBox_NewSousFamilleName.Text = "";
                return;
            }

            else if (MarquesDao.FindMarqueByMarqueName(SousFamilleName))
            {
                MessageBox.Show("This name has already been used by a Marque!", "ERROR");
                Form.textBox_NewSousFamilleName.Text = "";
                return;
            }

            else if (FamillesDao.FindFamilleByFamilleName(SousFamilleName))
            {
                MessageBox.Show("This name has already been used by a Famille!", "ERROR");
                Form.textBox_NewSousFamilleName.Text = "";
                return;
            }

            else if (SousFamillesDao.FindSousFamilleBySousFamilleName(SousFamilleName))
            {
                MessageBox.Show("SousFamille Already exsited!", "ERROR");
                Form.textBox_NewSousFamilleName.Text = "";
                return;
            }

            else
            {
                SousFamilles SousFamille = SousFamillesDao.FindSousFamillesBySousFamilleName(LabelSousFamilleName);
                SousFamille.SousFamilleName = SousFamilleName;
                SousFamillesDao.ModifySousFamille(SousFamille);

                if (SousFamillesDao.FindSousFamilleBySousFamilleName(SousFamille.SousFamilleName))
                {
                    MessageBox.Show("Modify succeed!");
                    Form.Close();
                }
                else
                {
                    MessageBox.Show("Modify Failed!");
                    Form.Close();
                }
            }
        }

        public void AddSousFamille(String SousFamilleName, String FamilleName, AddSousFamille AddSousFamille)
        {
            if (MarquesDao.FindMarqueByMarqueName(SousFamilleName))
            {
                MessageBox.Show("This name has already been used by a Marque!", "ERROR");
                AddSousFamille.textBox_SousFamilleName.Text = "";
                return;
            }

            if (FamillesDao.FindFamilleByFamilleName(SousFamilleName))
            {
                MessageBox.Show("This name has already been used by a Famille!", "ERROR");
                AddSousFamille.textBox_SousFamilleName.Text = "";
                return;
            }

            if (SousFamillesDao.FindSousFamilleBySousFamilleName(SousFamilleName))
            {
                MessageBox.Show("SousFamille Already exsited!", "ERROR");
                AddSousFamille.textBox_SousFamilleName.Text = "";
                return;
            }
            else
            {
                if (!FamillesDao.FindFamilleByFamilleName(FamilleName))
                {
                    if (MessageBox.Show("Famille doesn't exsite! Do you want to add a new Famille?", "Confirm Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
                    {
                        Familles NewFamille = new Familles(FamilleName);
                        NewFamille.RefFamille = FamillesDao.GetMaxRefFamille() + 1;
                        FamillesDao.AddFamille(NewFamille);
                        if (FamillesDao.FindFamilleByFamilleName(FamilleName))
                            MessageBox.Show("Add Famille succeed!");
                    }
                }
                SousFamilles SousFamille = new SousFamilles(SousFamilleName);
                Familles Famille = FamillesDao.FindFamillesByFamilleName(FamilleName);
                SousFamille.RefSousFamille = SousFamillesDao.GetMaxRefSousFamille() + 1;

                SousFamillesDao.AddSousFamille(SousFamille, Famille);

                if (SousFamillesDao.FindSousFamilleBySousFamilleName(SousFamilleName))
                {
                    MessageBox.Show("Add SousFamille succeed!");
                    AddSousFamille.Close();
                }
            }
        }
    }
}
