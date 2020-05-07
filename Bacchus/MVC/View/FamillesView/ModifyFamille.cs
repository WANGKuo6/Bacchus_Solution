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

namespace Bacchus.MVC.View.FamillesView
{
    public partial class ModifyFamille : Form
    {
        //private MarqueController MarqueController;
        private FamilleController FamilleController;
       // private SousFamilleController SousFamilleController;

        
        /// <summary>
        /// The constructor of ModifyFamille.
        /// </summary>
        /// <param name="FamilleName"></param>
        public ModifyFamille(string FamilleName)
        {
            InitializeComponent();

            this.label_Famille_Name.Text = FamilleName;

            FamilleController = new FamilleController();
        }

        private void button_Modify_Click(object sender, EventArgs e)
        {
            var FamilleName = this.textBox_NewFamilleName.Text;

            FamilleController.IfModifyFamilleOrNot(FamilleName, this.label_Famille_Name.Text, this);
        }

        private void button_Cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
