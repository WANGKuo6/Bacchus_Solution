using Bacchus.MVC.Controller;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bacchus.MVC.View.SousFamillesView
{
    public partial class AddSousFamille : Form
    {
        private SousFamilleController SousFamilleController;
        public AddSousFamille()
        {
            InitializeComponent();
            SousFamilleController = new SousFamilleController();
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            var SousFamilleName = this.textBox_SousFamilleName.Text;
            var FamilleName = this.label_Famille_Name.Text;

            if (SousFamilleName == "")
            {
                MessageBox.Show("Please enter the SousFamille Name!", "ERROR");
                return;
            }

            if (SousFamilleName == FamilleName)
            {
                MessageBox.Show("SousFamilleName and FamilleName can't be the same!", "ERROR");
                this.textBox_SousFamilleName.Text = "";
                return;
            }

            if (SousFamilleName == "Familles")
            {
                MessageBox.Show("SousFamilleName can't be Familles!");
                this.textBox_SousFamilleName.Text = "";
                return;
            }

            if (SousFamilleName == "Marques")
            {
                MessageBox.Show("SousFamilleName can't be Marques!");
                this.textBox_SousFamilleName.Text = "";
                return;
            }

            if (SousFamilleName == "Tous les articles")
            {
                MessageBox.Show("SousFamilleName can't be Tous les articles!", "ERROR");
                this.textBox_SousFamilleName.Text = "";
                return;
            }

            SousFamilleController.AddSousFamille(SousFamilleName, FamilleName, this);

        }

        private void button_Cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
