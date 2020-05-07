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

namespace Bacchus.MVC.View.MarquesView
{
    public partial class ModifyMarque : Form
    {
        //private FamillesController FamilleController;
       // private SousFamillesController SousFamilleController;
        private MarqueController MarqueController;

        
        public ModifyMarque(string MarqueName)
        {
            InitializeComponent();
            this.label_Marque_Name.Text = MarqueName;

            this.MarqueController = new MarqueController();

        }

        private void button_Modify_Click(object sender, EventArgs e)
        {
            var MarqueName = this.textBox_NewMarqueName.Text;

            if (MarqueName == "")
            {
                MessageBox.Show("Please enter the Marque Name!", "ERROR");
                return;
            }

            else if (MarqueName == this.label_Marque_Name.Text)
            {
                MessageBox.Show("MarqueName can't be the same before and after modification!", "ERROR");
                this.textBox_NewMarqueName.Text = "";
                return;
            }

            else if (MarqueName == "Familles")
            {
                MessageBox.Show("MarqueName can't be Familles!", "ERROR");
                this.textBox_NewMarqueName.Text = "";
                return;
            }

            else if (MarqueName == "Marques")
            {
                MessageBox.Show("MarqueName can't be Marques!", "ERROR");
                this.textBox_NewMarqueName.Text = "";
                return;
            }

            else if (MarqueName == "Tous les articles")
            {
                MessageBox.Show("MarqueName can't be Tous les articles!", "ERROR");
                this.textBox_NewMarqueName.Text = "";
                return;
            }

            MarqueController.ModifyMarque(MarqueName, this, this.label_Marque_Name.Text);
        }

        private void button_Cancle_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
