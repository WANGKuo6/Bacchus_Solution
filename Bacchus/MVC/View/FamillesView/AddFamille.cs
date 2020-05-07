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

namespace Bacchus.MVC.View.FamillesView
{
    public partial class AddFamille : Form
    {
        FamilleController FamilleController;
        public AddFamille()
        {
            InitializeComponent();

            FamilleController = new FamilleController();
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            var FamilleName = this.textBox_FamilleName.Text;

            if (FamilleName == "")
            {
                MessageBox.Show("Please enter the Famille Name!", "ERROR");
                return;
            }

            if (FamilleName == "Familles")
            {
                MessageBox.Show("FamilleName can't be Familles!");
                this.textBox_FamilleName.Text = "";
                return;
            }

            if (FamilleName == "Marques")
            {
                MessageBox.Show("FamilleName can't be Marques!");
                this.textBox_FamilleName.Text = "";
                return;
            }

            if (FamilleName == "Tous les articles")
            {
                MessageBox.Show("FamilleName can't be Tous les articles!");
                this.textBox_FamilleName.Text = "";
                return;
            }

            FamilleController.AddFamille(FamilleName, this);
        }

        private void button_Cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
