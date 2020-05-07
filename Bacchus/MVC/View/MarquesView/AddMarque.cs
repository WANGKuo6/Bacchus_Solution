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

namespace Bacchus.MVC.View.MarquesView
{
    public partial class AddMarque : Form
    {
        private MarqueController MarqueController;
        public AddMarque()
        {
            InitializeComponent();
            MarqueController = new MarqueController();
        }

        private void button_Add_Click(object sender, EventArgs e)
        {
            var MarqueName = this.textBox_MarqueName.Text;

            if (MarqueName == "")
            {
                MessageBox.Show("Please enter the Marque Name!", "ERROR");
                return;
            }

            if (MarqueName == "Familles")
            {
                MessageBox.Show("MarqueName can't be Familles!", "ERROR");
                this.textBox_MarqueName.Text = "";
                return;
            }

            if (MarqueName == "Marques")
            {
                MessageBox.Show("MarqueName can't be Marques!", "ERROR");
                this.textBox_MarqueName.Text = "";
                return;
            }

            if (MarqueName == "Familles")
            {
                MessageBox.Show("MarqueName can't be Familles!", "ERROR");
                this.textBox_MarqueName.Text = "";
                return;
            }

            if (MarqueName == "Tous les articles")
            {
                MessageBox.Show("MarqueName can't be Tous les articles!", "ERROR");
                this.textBox_MarqueName.Text = "";
                return;
            }

            MarqueController.AddMarque(MarqueName, this);

           
        }

        private void button_Cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
