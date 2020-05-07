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

namespace Bacchus.MVC.View.SousFamillesView
{
    public partial class ModifySousFamilles : Form
    {
        //private MarquesController MarqueController;
        //private FamillesController FamilleController;
        private SousFamilleController SousFamilleController;

        /// <summary>
        /// The constructor of ModifySousFamille.
        /// </summary>
        /// <param name="FamilleName"></param>
        /// <param name="SousFamilleName"></param>
        public ModifySousFamilles(String FamilleName, string SousFamilleName)
        {
            InitializeComponent();
            this.label_Famille_Name.Text = FamilleName;
            this.label_SousFamille_Name.Text = SousFamilleName;
            SousFamilleController = new SousFamilleController();
        }

        private void button_Modify_Click(object sender, EventArgs e)
        {
            var SousFamilleName = this.textBox_NewSousFamilleName.Text;

            SousFamilleController.IfModifySouFamilleOrNot(this, this.label_Famille_Name.Text, this.label_SousFamille_Name.Text);
        }

        private void button_Cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
