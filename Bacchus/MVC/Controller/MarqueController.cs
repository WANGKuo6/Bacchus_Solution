using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bacchus.MVC.Dao;
using Bacchus.MVC.Model;
using System.Windows.Forms;
using Bacchus.MVC.View.MarquesView;

namespace Bacchus.MVC.Controller
{
    class MarqueController
    {
        private MarquesDao MarquesDao;
        private FamillesDao FamillesDao;
        private SousFamillesDao SousFamillesDao;

        public MarqueController()
        {
            MarquesDao = new MarquesDao();
            FamillesDao = new FamillesDao();
            SousFamillesDao = new SousFamillesDao();
        }

        public List<Marques> FindMarquesBySousFamilleName(string SousFamilleName)
        {
            return MarquesDao.FindMarquesBySousFamilleName(SousFamilleName);
        }

        public Marques FindMarquesByMarqueName(string MarqueName)
        {
            return MarquesDao.FindMarquesByMarqueName(MarqueName);
        }

        public void Delete_Marque(String MarqueName, List<String> MarqueNames)
        { 

            if (MarquesDao.FindMarqueByMarqueName(MarqueName))
            {
                MarquesDao.DeleteMarque(MarquesDao.FindMarquesByMarqueName(MarqueName));
                MarqueNames.Remove(MarqueName);
                if (!MarquesDao.FindMarqueByMarqueName(MarqueName))
                    MessageBox.Show("Delete succeed!");
            }
            else
                MessageBox.Show("Marque doesn't exist!");
        }

        public string Modify_Marque(String MarqueName, List<String> MarqueNames, String SelectedNode, Form Form)
        {

            if (MarquesDao.FindMarqueByMarqueName(MarqueName))
            {
                ModifyMarque ModifyMarque = new ModifyMarque(SelectedNode) { StartPosition = FormStartPosition.CenterParent };
                ModifyMarque.ShowDialog(Form);

                if (ModifyMarque.textBox_NewMarqueName.Text != "")
                {
                    var Index = MarqueNames.IndexOf(MarqueName);
                    MarqueNames.Remove(MarqueName);
                    MarqueNames.Insert(Index, ModifyMarque.textBox_NewMarqueName.Text);
                    MarqueName = ModifyMarque.textBox_NewMarqueName.Text;
                }
            }
            else
                MessageBox.Show("Marque doesn't exist!");

            return MarqueName;
        }

        public void AddMarque(String MarqueName, AddMarque AddMarque)
        {
            if (FamillesDao.FindFamilleByFamilleName(MarqueName))
            {
                MessageBox.Show("This name has already been used by a Famille!", "ERROR");
                AddMarque.textBox_MarqueName.Text = "";
                return;
            }

            if (SousFamillesDao.FindSousFamilleBySousFamilleName(MarqueName))
            {
                MessageBox.Show("This name has already been used by a SousFamille!", "ERROR");
                AddMarque.textBox_MarqueName.Text = "";
                return;
            }

            if (MarquesDao.FindMarqueByMarqueName(MarqueName))
            {
                MessageBox.Show("Marque Already exsited!", "ERROR");
                AddMarque.textBox_MarqueName.Text = "";
                return;
            }
            else
            {
                Marques Marque = new Marques(MarqueName);
                Marque.RefMarque = MarquesDao.GetMaxRefMarque() + 1;
                MarquesDao.AddMarque(Marque);
                if (MarquesDao.FindMarqueByMarqueName(MarqueName))
                {
                    MessageBox.Show("Add Marque succeed!");
                    AddMarque.Close();
                }
            }
        }

        public void ModifyMarque(String MarqueName, ModifyMarque ModifyMarque, String LabelMarqueName)
        {

            if (FamillesDao.FindFamilleByFamilleName(MarqueName))
            {
                MessageBox.Show("This name has already been used by a Famille!", "ERROR");
                ModifyMarque.textBox_NewMarqueName.Text = "";
                return;
            }

            else if (SousFamillesDao.FindSousFamilleBySousFamilleName(MarqueName))
            {
                MessageBox.Show("This name has already been used by a SousFamille!", "ERROR");
                ModifyMarque.textBox_NewMarqueName.Text = "";
                return;
            }

            else if (MarquesDao.FindMarqueByMarqueName(MarqueName))
            {
                MessageBox.Show("Marque Already exsited!", "ERROR");
                ModifyMarque.textBox_NewMarqueName.Text = "";
                return;
            }
            else
            {
                Marques Marque = FindMarquesByMarqueName(LabelMarqueName);
                Marque.MarqueName = MarqueName;
                MarquesDao.ModifyMarque(Marque);

                if (MarquesDao.FindMarqueByMarqueName(Marque.MarqueName))
                {
                    MessageBox.Show("Modify succeed!");
                    ModifyMarque.Close();
                }
                else
                {
                    MessageBox.Show("Modify Failed!");
                }
            }
        }
    }
}
