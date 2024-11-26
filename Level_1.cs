using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyberguardGame
{
    public partial class Level_1 : Form
    {

        private Button btnBack;

        public Level_1()
        {
            InitializeComponent();
            LevelScreen();
        }

        private void LevelScreen()
        {
            btnBack = new Button();
            btnBack.Text = "KONIEC";
            btnBack.Size = new System.Drawing.Size(100, 50);
            btnBack.Location = new System.Drawing.Point(this.ClientSize.Width - btnBack.Width - 20, this.ClientSize.Height - btnBack.Height - 20);
            btnBack.Click += new EventHandler(BtnBack_Click);
            btnBack.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.Controls.Add(btnBack);
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    } 
}