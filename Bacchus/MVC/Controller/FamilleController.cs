using Bacchus.MVC.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bacchus.MVC.Model;
using System.Windows.Forms;
using Bacchus.MVC.View.FamillesView;

namespace Bacchus.MVC.Controller
{
    class FamilleController
    {
        private FamillesDao FamillesDao;
        private MarquesDao MarquesDao;
        private SousFamillesDao SousFamillesDao;
        public FamilleController()
        {
            FamillesDao = new FamillesDao();
            MarquesDao = new MarquesDao();
            SousFamillesDao = new SousFamillesDao();
        }
        

        public List<Familles> FindFamillesByMarqueName(string MarqueName)
        {
            return FamillesDao.FindFamillesByMarqueName(MarqueName);
        }

        public void Delete_Famille(String FamilleName, List<String> FamilleNames)
        {
            //

            if (FamillesDao.FindFamilleByFamilleName(FamilleName))
            {
                FamillesDao.DeleteFamille(FamillesDao.FindFamillesByFamilleName(FamilleName));
                FamilleNames.Remove(FamilleName);
                if (!FamillesDao.FindFamilleByFamilleName(FamilleName))
                    MessageBox.Show("Delete succeed!");
            }
            else
                MessageBox.Show("Famille doesn't exist!");
        }

        public string Modify_Famille(String FamilleName, List<String> FamilleNames, String SelectedNode, Form Form)
        {
            
            if (FamillesDao.FindFamilleByFamilleName(FamilleName))
            {
                ModifyFamille ModifyFamille = new ModifyFamille(SelectedNode) { StartPosition = FormStartPosition.CenterParent };
                ModifyFamille.ShowDialog(Form);

                if (ModifyFamille.textBox_NewFamilleName.Text != "")
                {
                    var Index = FamilleNames.IndexOf(FamilleName);
                    FamilleNames.Remove(FamilleName);
                    FamilleNames.Insert(Index, ModifyFamille.textBox_NewFamilleName.Text);
                    FamilleName = ModifyFamille.textBox_NewFamilleName.Text;
                }
            }
            else
                MessageBox.Show("Famille doesn't exist!");

            return FamilleName;
        }

        public void IfModifyFamilleOrNot(String FamilleName, String LabelFamilleName, ModifyFamille Form)
        {
            if (FamilleName == "")
            {
                MessageBox.Show("Please enter the Famille Name!", "ERROR");
                return;
            }

            else if (FamilleName == LabelFamilleName)
            {
                MessageBox.Show("FamilleName can't be the same before and after modification!", "ERROR");
                return;
            }

            else if (FamilleName == "Familles")
            {
                MessageBox.Show("FamilleName can't be Familles!");
                Form.textBox_NewFamilleName.Text = "";
                return;
            }

            else if (FamilleName == "Marques")
            {
                MessageBox.Show("FamilleName can't be Marques!");
                Form.textBox_NewFamilleName.Text = "";
                return;
            }

            else if (FamilleName == "Tous les articles")
            {
                MessageBox.Show("FamilleName can't be Tous les articles!");
                Form.textBox_NewFamilleName.Text = "";
                return;
            }

            else if (MarquesDao.FindMarqueByMarqueName(FamilleName))
            {
                MessageBox.Show("This name has already been used by a Marque!", "ERROR");
                Form.textBox_NewFamilleName.Text = "";
                return;
            }

            else if (SousFamillesDao.FindSousFamilleBySousFamilleName(FamilleName))
            {
                MessageBox.Show("This name has already been used by a SousFamille!", "ERROR");
                Form.textBox_NewFamilleName.Text = "";
                return;
            }

            else if (FamillesDao.FindFamilleByFamilleName(FamilleName))
            {
                MessageBox.Show("SousFamille Already exsited!", "ERROR");
                Form.textBox_NewFamilleName.Text = "";
                return;
            }
            else
            {
                Familles Famille = FamillesDao.FindFamillesByFamilleName(LabelFamilleName);
                Famille.FamilleName = FamilleName;
                FamillesDao.ModifyFamille(Famille);

                if (FamillesDao.FindFamilleByFamilleName(Famille.FamilleName))
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

        public void AddFamille(String FamilleName, AddFamille AddFamille)
        {
            if (MarquesDao.FindMarqueByMarqueName(FamilleName))
            {
                MessageBox.Show("FamilleName already used by a Marque!", "ERROR");
                AddFamille.textBox_FamilleName.Text = "";
                return;
            }

            if (SousFamillesDao.FindSousFamilleBySousFamilleName(FamilleName))
            {
                MessageBox.Show("FamilleName already used by a SousFamille!", "ERROR");
                AddFamille.textBox_FamilleName.Text = "";
                return;
            }

            if (FamillesDao.FindFamilleByFamilleName(FamilleName))
            {
                MessageBox.Show("Famille Already exsited!", "ERROR");
                AddFamille.textBox_FamilleName.Text = "";
                return;
            }
            else
            {
                Familles Famille = new Familles(FamilleName);
                Famille.RefFamille = FamillesDao.GetMaxRefFamille() + 1;
                FamillesDao.AddFamille(Famille);

                if (FamillesDao.FindFamilleByFamilleName(FamilleName))
                {
                    MessageBox.Show("Add Famille succeed!");
                    AddFamille.Close();
                }
            }
        }
    }
}
