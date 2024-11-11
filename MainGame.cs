using System;
using System.Windows.Forms;

namespace CyberguardGame
{
    public partial class MainGame : Form
    {
        private Label infoLabel;
        private Button btnBack;
        private Button btnBack_2;
        private Button btnLevel1;
        private Button btnLevel2;
        private Button btnLevel3;
        private Button btnReturnToMainMenu; // Przycisk powrotu do menu g³ównego

        private bool level1Completed = false;
        private bool level2Completed = false;

        public MainGame()
        {
            InitializeComponent();
            CreateStartScreen();

            this.BackgroundImage = Properties.Resources.JEMA_GER_1426_24_v1;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void CreateStartScreen()
        {
            // Etykieta do wyœwietlania informacji
            infoLabel = new Label();
            infoLabel.Size = new System.Drawing.Size(500, 200);
            infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            infoLabel.Location = new System.Drawing.Point((this.ClientSize.Width - infoLabel.Width) / 2, (this.ClientSize.Height - infoLabel.Height) / 2 - 50);
            infoLabel.Font = new System.Drawing.Font("Arial", 12);
            infoLabel.Visible = false;
            infoLabel.Anchor = AnchorStyles.None;
            this.Controls.Add(infoLabel);

            // Przycisk Powrót
            btnBack = new Button();
            btnBack.Text = "POWRÓT";
            btnBack.Size = new System.Drawing.Size(100, 50);
            btnBack.Location = new System.Drawing.Point(this.ClientSize.Width - btnBack.Width - 20, this.ClientSize.Height - btnBack.Height - 20);
            btnBack.Click += new EventHandler(BtnBack_Click);
            btnBack.Visible = false;
            btnBack.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.Controls.Add(btnBack);

            // Przycisk Powrót 2
            btnBack_2 = new Button();
            btnBack_2.Text = "POWRÓT";
            btnBack_2.Size = new System.Drawing.Size(120, 50);
            btnBack_2.Location = new System.Drawing.Point(this.ClientSize.Width - btnBack.Width - 20, this.ClientSize.Height - btnBack.Height - 20);
            btnBack_2.Click += new EventHandler(BtnBack_Click_2);
            btnBack_2.Visible = false;
            btnBack_2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.Controls.Add(btnBack_2);

            // Przycisk Poziom 1
            btnLevel1 = new Button();
            btnLevel1.Text = "POZIOM 1";
            btnLevel1.Size = new System.Drawing.Size(125, 40);
            btnLevel1.Location = new System.Drawing.Point(439, 385);
            btnLevel1.Click += new EventHandler(BtnLevel1_Click);
            btnLevel1.Visible = false;
            this.Controls.Add(btnLevel1);

            // Przycisk Poziom 2
            btnLevel2 = new Button();
            btnLevel2.Text = "POZIOM 2";
            btnLevel2.Size = new System.Drawing.Size(125, 40);
            btnLevel2.Location = new System.Drawing.Point(439, 435);
            btnLevel2.Click += new EventHandler(BtnLevel2_Click);
            btnLevel2.Visible = false;
            btnLevel2.Enabled = false; // Zablokowany domyœlnie
            this.Controls.Add(btnLevel2);

            // Przycisk Poziom 3
            btnLevel3 = new Button();
            btnLevel3.Text = "POZIOM 3";
            btnLevel3.Size = new System.Drawing.Size(125, 40);
            btnLevel3.Location = new System.Drawing.Point(439, 485);
            btnLevel3.Click += new EventHandler(BtnLevel3_Click);
            btnLevel3.Visible = false;
            btnLevel3.Enabled = false; // Zablokowany domyœlnie
            this.Controls.Add(btnLevel3);

            // Przycisk Powrotu do Menu G³ównego
            btnReturnToMainMenu = new Button();
            btnReturnToMainMenu.Text = "POWRÓT";
            btnReturnToMainMenu.Size = new System.Drawing.Size(100, 50);
            btnReturnToMainMenu.Location = new System.Drawing.Point(this.ClientSize.Width - btnReturnToMainMenu.Width - 20, this.ClientSize.Height - btnReturnToMainMenu.Height - 20);
            btnReturnToMainMenu.Click += new EventHandler(BtnReturnToMainMenu_Click);
            btnReturnToMainMenu.Visible = false;
            btnReturnToMainMenu.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            this.Controls.Add(btnReturnToMainMenu);
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            // Wyœwietlanie przycisków poziomów trudnoœci oraz przycisku Powrotu do Menu
            btnLevel1.Visible = true;
            btnLevel2.Visible = true;
            btnLevel3.Visible = true;
            btnReturnToMainMenu.Visible = true;

            // Ukrywa przyciski ekranu startowego
            foreach (Control control in this.Controls)
            {
                if (control is Button && control != btnLevel1 && control != btnLevel2 && control != btnLevel3 && control != btnReturnToMainMenu)
                {
                    control.Visible = false;
                }
            }
        }

        private void BtnLevel1_Click(object sender, EventArgs e)
        {
            infoLabel.Text = "Rozpoczêto Poziom 1!";
            ShowInfoScreen();
            level1Completed = true; // Symulujemy ukoñczenie poziomu
            btnLevel2.Enabled = true; // Odblokowanie poziomu 2
        }

        private void BtnLevel2_Click(object sender, EventArgs e)
        {
            if (level1Completed)
            {
                infoLabel.Text = "Rozpoczêto Poziom 2!";
                ShowInfoScreen();
                level2Completed = true; // Symulujemy ukoñczenie poziomu
                btnLevel3.Enabled = true; // Odblokowanie poziomu 3
            }
        }

        private void BtnLevel3_Click(object sender, EventArgs e)
        {
            if (level1Completed && level2Completed)
            {
                infoLabel.Text = "Rozpoczêto Poziom 3!";
                ShowInfoScreen();
            }
        }

        private void BtnControls_Click(object sender, EventArgs e)
        {
            string controls = "STEROWANIE:\n" +
                              "W - ruch w górê\n" +
                              "A - ruch w lewo\n" +
                              "S - ruch w dó³\n" +
                              "D - ruch w prawo\n\n" +
                              "Lewy przycisk myszy - klikanie na elementy";
            infoLabel.Text = controls;
            ShowInfoScreen_2();
        }

        private void BtnRules_Click(object sender, EventArgs e)
        {
            string rules = "Wcielasz siê w rolê specjalisty ds. bezpieczeñstwa sieciowego, chroni¹c wirtualne biuro przed zagro¿eniami.";
            infoLabel.Text = rules;
            ShowInfoScreen_2();
        }

        private void ShowInfoScreen()
        {
            infoLabel.Visible = true;
            btnBack.Visible = true;

            foreach (Control control in this.Controls)
            {
                if (control is Button && control != btnBack)
                {
                    control.Visible = false;
                }
            }
        }
        private void ShowInfoScreen_2()
        {
            infoLabel.Visible = true;
            btnBack_2.Visible = true;

            foreach (Control control in this.Controls)
            {
                if (control is Button && control != btnBack_2)
                {
                    control.Visible = false;
                }
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            infoLabel.Visible = false;
            btnBack.Visible = false;

            foreach (Control control in this.Controls)
            {
                if (control is Button && (control == btnLevel1 || control == btnLevel2 || control == btnLevel3 || control == btnReturnToMainMenu))
                {
                    control.Visible = true;
                }
            }
        }

        private void BtnBack_Click_2(object sender, EventArgs e)
        {
            infoLabel.Visible = false;
            btnBack.Visible = false;

            foreach (Control control in this.Controls)
            {
                if (control is Button && (control == BtnStart || control == BtnControls || control == BtnRules))
                {
                    control.Visible = true;
                }
            }
        }

        private void BtnReturnToMainMenu_Click(object sender, EventArgs e)
        {
            // Ukrywanie przycisków poziomów trudnoœci i przycisku Powrót do Menu
            btnLevel1.Visible = false;
            btnLevel2.Visible = false;
            btnLevel3.Visible = false;
            btnReturnToMainMenu.Visible = false;

            // Przywracanie przycisków g³ównego menu
            //BtnStart.Visible = true;
            BtnStart.Visible = true;
            BtnControls.Visible = true;
            BtnRules.Visible = true;
        }
    }
}