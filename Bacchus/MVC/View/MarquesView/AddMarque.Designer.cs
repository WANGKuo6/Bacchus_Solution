namespace Bacchus.MVC.View.MarquesView
{
    partial class AddMarque
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.button_Cancle = new System.Windows.Forms.Button();
            this.button_Add = new System.Windows.Forms.Button();
            this.label_NewMarque = new System.Windows.Forms.Label();
            this.textBox_MarqueName = new System.Windows.Forms.TextBox();
            this.label_MarqueName = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button_Cancle);
            this.panel1.Controls.Add(this.button_Add);
            this.panel1.Controls.Add(this.label_NewMarque);
            this.panel1.Controls.Add(this.textBox_MarqueName);
            this.panel1.Controls.Add(this.label_MarqueName);
            this.panel1.Location = new System.Drawing.Point(258, 166);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(285, 118);
            this.panel1.TabIndex = 17;
            // 
            // button_Cancle
            // 
            this.button_Cancle.Location = new System.Drawing.Point(167, 81);
            this.button_Cancle.Margin = new System.Windows.Forms.Padding(2);
            this.button_Cancle.Name = "button_Cancle";
            this.button_Cancle.Size = new System.Drawing.Size(69, 22);
            this.button_Cancle.TabIndex = 15;
            this.button_Cancle.Text = "Cancle";
            this.button_Cancle.UseVisualStyleBackColor = true;
            this.button_Cancle.Click += new System.EventHandler(this.button_Cancle_Click);
            // 
            // button_Add
            // 
            this.button_Add.Location = new System.Drawing.Point(57, 81);
            this.button_Add.Margin = new System.Windows.Forms.Padding(2);
            this.button_Add.Name = "button_Add";
            this.button_Add.Size = new System.Drawing.Size(69, 22);
            this.button_Add.TabIndex = 2;
            this.button_Add.Text = "Add";
            this.button_Add.UseVisualStyleBackColor = true;
            this.button_Add.Click += new System.EventHandler(this.button_Add_Click);
            // 
            // label_NewMarque
            // 
            this.label_NewMarque.AutoSize = true;
            this.label_NewMarque.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label_NewMarque.Location = new System.Drawing.Point(9, 6);
            this.label_NewMarque.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_NewMarque.Name = "label_NewMarque";
            this.label_NewMarque.Size = new System.Drawing.Size(92, 17);
            this.label_NewMarque.TabIndex = 14;
            this.label_NewMarque.Text = "NewMarque";
            // 
            // textBox_MarqueName
            // 
            this.textBox_MarqueName.Location = new System.Drawing.Point(108, 40);
            this.textBox_MarqueName.Margin = new System.Windows.Forms.Padding(2);
            this.textBox_MarqueName.Name = "textBox_MarqueName";
            this.textBox_MarqueName.Size = new System.Drawing.Size(164, 20);
            this.textBox_MarqueName.TabIndex = 1;
            // 
            // label_MarqueName
            // 
            this.label_MarqueName.AutoSize = true;
            this.label_MarqueName.Location = new System.Drawing.Point(24, 44);
            this.label_MarqueName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label_MarqueName.Name = "label_MarqueName";
            this.label_MarqueName.Size = new System.Drawing.Size(71, 13);
            this.label_MarqueName.TabIndex = 0;
            this.label_MarqueName.Text = "MarqueName";
            // 
            // AddMarque
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.panel1);
            this.Name = "AddMarque";
            this.Text = "AddMarque";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button_Cancle;
        private System.Windows.Forms.Button button_Add;
        private System.Windows.Forms.Label label_NewMarque;
        internal System.Windows.Forms.TextBox textBox_MarqueName;
        private System.Windows.Forms.Label label_MarqueName;
    }
}