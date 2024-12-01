using System;
using System.Windows.Forms;

namespace CyberguardGame{

    public partial class MainGame : Form{
        
        private Label infoLabel;
        
        private Button btnBack;
        private Button btnBack_2;
        private Button btnLevel1;
        private Button btnLevel2;
        private Button btnLevel3;
        private Button btnReturnToMainMenu;

        private Panel mainPanel;

        private bool level1Completed = false;
        private bool level2Completed = false;

        private Form activeLevel = new Form();

        public MainGame(){
            InitializeComponent();
            StartScreen();

            this.BackgroundImage = Properties.Resources.JEMA_GER_1426_24_v1;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        private void OpenLevel(Form LevelForm, object btnSender){
            
            if (activeLevel != null){
                mainPanel.Controls.Remove(activeLevel);
                activeLevel.Close();
            }

            activeLevel = LevelForm;
            
            LevelForm.TopLevel = false;
            LevelForm.FormBorderStyle = FormBorderStyle.None;
            LevelForm.Dock = DockStyle.Fill;
            
            mainPanel.Controls.Add(LevelForm);
            mainPanel.Tag = LevelForm;
            
            LevelForm.BringToFront();
            LevelForm.Show();
            LevelForm.Focus();
        }
        
        private void StartScreen(){
            
            mainPanel = new Panel();
            mainPanel.Size = this.ClientSize;
            mainPanel.Location = new System.Drawing.Point(0, 0);
            mainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainPanel.BackColor = Color.Transparent;
            this.Controls.Add(mainPanel);

            infoLabel = new Label();
            infoLabel.Size = new System.Drawing.Size(500, 200);
            infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            infoLabel.Location = new System.Drawing.Point((this.ClientSize.Width - infoLabel.Width) / 2, (this.ClientSize.Height - infoLabel.Height) / 2);
            infoLabel.Font = new System.Drawing.Font("Arial", 12);
            infoLabel.Visible = false;
            infoLabel.Anchor = AnchorStyles.None;
            mainPanel.Controls.Add(infoLabel);

            btnBack = new Button();
            btnBack.Text = "POWRÓT";
            btnBack.Size = new System.Drawing.Size(100, 50);
            btnBack.Location = new System.Drawing.Point(this.ClientSize.Width - btnBack.Width - 20, this.ClientSize.Height - btnBack.Height - 20);
            btnBack.Click += new EventHandler(BtnBack_Click);
            btnBack.Visible = false;
            btnBack.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnBack.BackColor = SystemColors.WindowFrame;
            btnBack.FlatAppearance.BorderColor = Color.Black;
            btnBack.FlatAppearance.BorderSize = 1;
            btnBack.FlatAppearance.MouseOverBackColor = Color.DarkGray;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new System.Drawing.Font("Snap ITC", 11);
            btnBack.ForeColor = SystemColors.HighlightText;
            mainPanel.Controls.Add(btnBack);

            btnBack_2 = new Button();
            btnBack_2.Text = "POWRÓT";
            btnBack_2.Size = new System.Drawing.Size(100, 50);
            btnBack_2.Location = new System.Drawing.Point(this.ClientSize.Width - btnBack.Width - 20, this.ClientSize.Height - btnBack.Height - 20);
            btnBack_2.Click += new EventHandler(BtnBack_Click_2);
            btnBack_2.Visible = false;
            btnBack_2.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnBack_2.BackColor = SystemColors.WindowFrame;
            btnBack_2.FlatAppearance.BorderColor = Color.Black;
            btnBack_2.FlatAppearance.BorderSize = 1;
            btnBack_2.FlatAppearance.MouseOverBackColor = Color.DarkGray;
            btnBack_2.FlatStyle = FlatStyle.Flat;
            btnBack_2.Font = new System.Drawing.Font("Snap ITC", 11);
            btnBack_2.ForeColor = SystemColors.HighlightText;
            mainPanel.Controls.Add(btnBack_2);

            btnLevel1 = new Button();
            btnLevel1.Text = "POZIOM 1";
            btnLevel1.Size = new System.Drawing.Size(125, 40);
            btnLevel1.Location = new System.Drawing.Point(439, 385);
            btnLevel1.Click += new EventHandler(BtnLevel1_Click);
            btnLevel1.Visible = false;
            btnLevel1.BackColor = SystemColors.WindowFrame;
            btnLevel1.FlatAppearance.BorderColor = Color.Black;
            btnLevel1.FlatAppearance.BorderSize = 1;
            btnLevel1.FlatAppearance.MouseOverBackColor = Color.DarkGray;
            btnLevel1.FlatStyle = FlatStyle.Flat;
            btnLevel1.Font = new System.Drawing.Font("Snap ITC", 11);
            btnLevel1.ForeColor = SystemColors.HighlightText;
            mainPanel.Controls.Add(btnLevel1);

            btnLevel2 = new Button();
            btnLevel2.Text = "POZIOM 2";
            btnLevel2.Size = new System.Drawing.Size(125, 40);
            btnLevel2.Location = new System.Drawing.Point(439, 435);
            btnLevel2.Click += new EventHandler(BtnLevel2_Click);
            btnLevel2.Visible = false;
            btnLevel2.Enabled = false;                                          // Zablokowany domyœlnie
            btnLevel2.BackColor = SystemColors.WindowFrame;
            btnLevel2.FlatAppearance.BorderColor = Color.Black;
            btnLevel2.FlatAppearance.BorderSize = 1;
            btnLevel2.FlatAppearance.MouseOverBackColor = Color.DarkGray;
            btnLevel2.FlatStyle = FlatStyle.Flat;
            btnLevel2.Font = new System.Drawing.Font("Snap ITC", 11);
            btnLevel2.ForeColor = SystemColors.HighlightText;
            mainPanel.Controls.Add(btnLevel2);

            btnLevel3 = new Button();
            btnLevel3.Text = "POZIOM 3";
            btnLevel3.Size = new System.Drawing.Size(125, 40);
            btnLevel3.Location = new System.Drawing.Point(439, 485);
            btnLevel3.Click += new EventHandler(BtnLevel3_Click);
            btnLevel3.Visible = false;
            btnLevel3.Enabled = false;                                          // Zablokowany domyœlnie
            btnLevel3.BackColor = SystemColors.WindowFrame;
            btnLevel3.FlatAppearance.BorderColor = Color.Black;
            btnLevel3.FlatAppearance.BorderSize = 1;
            btnLevel3.FlatAppearance.MouseOverBackColor = Color.DarkGray;
            btnLevel3.FlatStyle = FlatStyle.Flat;
            btnLevel3.Font = new System.Drawing.Font("Snap ITC", 11);
            btnLevel3.ForeColor = SystemColors.HighlightText;
            mainPanel.Controls.Add(btnLevel3);

            btnReturnToMainMenu = new Button();
            btnReturnToMainMenu.Text = "POWRÓT";
            btnReturnToMainMenu.Size = new System.Drawing.Size(100, 50);
            btnReturnToMainMenu.Location = new System.Drawing.Point(this.ClientSize.Width - btnReturnToMainMenu.Width - 20, this.ClientSize.Height - btnReturnToMainMenu.Height - 20);
            btnReturnToMainMenu.Click += new EventHandler(BtnReturnToMainMenu_Click);
            btnReturnToMainMenu.Visible = false;
            btnReturnToMainMenu.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnReturnToMainMenu.BackColor = SystemColors.WindowFrame;
            btnReturnToMainMenu.FlatAppearance.BorderColor = Color.Black;
            btnReturnToMainMenu.FlatAppearance.BorderSize = 1;
            btnReturnToMainMenu.FlatAppearance.MouseOverBackColor = Color.DarkGray;
            btnReturnToMainMenu.FlatStyle = FlatStyle.Flat;
            btnReturnToMainMenu.Font = new System.Drawing.Font("Snap ITC", 11);
            btnReturnToMainMenu.ForeColor = SystemColors.HighlightText;
            mainPanel.Controls.Add(btnReturnToMainMenu);
        }

        private void BtnStart_Click(object sender, EventArgs e){
            
            btnLevel1.Visible = true;
            btnLevel2.Visible = true;
            btnLevel3.Visible = true;
            btnReturnToMainMenu.Visible = true;

            foreach (Control control in this.Controls){
                
                if (control is Button && control != btnLevel1 && control != btnLevel2 && control != btnLevel3 && control != btnReturnToMainMenu){
                    
                    control.Visible = false;
                }
            }
        }

        private void BtnLevel1_Click(object sender, EventArgs e){
            
            OpenLevel(new Level_1(), sender);
            btnLevel2.Enabled = true;
        }

        private void BtnLevel2_Click(object sender, EventArgs e){

        }

        private void BtnLevel3_Click(object sender, EventArgs e){
            
        }

        private void BtnControls_Click(object sender, EventArgs e){
            string controls = "STEROWANIE:\n" +
                              "W - RUCH W GÓRÊ\n" +
                              "A - RUCH W LEWO\n" +
                              "S - RUCH W DÓ£\n" +
                              "D - RUCH W PRAWO\n" +
                              "LPM - LEWY PRZYCISK MYSZY";

            infoLabel.Text = controls;
            ShowInfoScreen();
        }

        private void BtnRules_Click(object sender, EventArgs e){
            
            string rules = "Wcielasz siê w rolê specjalisty ds. bezpieczeñstwa sieciowego, chroni¹c wirtualne biuro przed zagro¿eniami.";
            infoLabel.Text = rules;
            ShowInfoScreen();
        }

        private void ShowInfoScreen(){
            
                                                                        // Ustawienie widocznoœci etykiety oraz przycisku powrotu
            infoLabel.Visible = true;
            btnBack_2.Visible = true;

                                                                        // Ustawienie t³a i ramki dla infoLabel
            infoLabel.BackColor = Color.Black;                          // Ustawienie bia³ego t³a
            infoLabel.BorderStyle = BorderStyle.FixedSingle;            // Dodanie ramki
            infoLabel.Padding = new Padding(10);                        // Ustawienie marginesów wewnêtrznych, aby tekst nie dotyka³ krawêdzi
            infoLabel.BackColor = SystemColors.WindowFrame;
            infoLabel.FlatStyle = FlatStyle.Flat;
            infoLabel.Font = new System.Drawing.Font("Snap ITC", 16);
            infoLabel.ForeColor = SystemColors.HighlightText;

                                                                        // Ukrywanie innych przycisków
            foreach (Control control in this.Controls){
                
                if (control is Button && control != btnBack_2){
                    
                    control.Visible = false;
                }
            }
        }

        private void BtnBack_Click(object sender, EventArgs e){

            infoLabel.Visible = false;
            btnBack.Visible = false;

            foreach (Control control in this.Controls){

                if (control is Button && (control == btnLevel1 || control == btnLevel2 || control == btnLevel3 || control == btnReturnToMainMenu)){

                    control.Visible = true;
                }
            }
        }

        private void BtnBack_Click_2(object sender, EventArgs e){

            infoLabel.Visible = false;
            btnBack_2.Visible = false;

            foreach (Control control in this.Controls){

                if (control is Button && (control == BtnStart || control == BtnControls || control == BtnRules)){

                    control.Visible = true;
                }
            }
        }

        private void BtnReturnToMainMenu_Click(object sender, EventArgs e){

            // Ukrywanie przycisków poziomów trudnoœci i przycisku Powrót do Menu
            btnLevel1.Visible = false;
            btnLevel2.Visible = false;
            btnLevel3.Visible = false;
            btnReturnToMainMenu.Visible = false;

            // Przywracanie przycisków g³ównego menu
            BtnStart.Visible = true;
            BtnControls.Visible = true;
            BtnRules.Visible = true;
        }

        public void ShowMainPanel(){

            mainPanel.Visible = true;

            foreach (Control control in mainPanel.Controls){

                if (control is Button button && (button == BtnStart || button == BtnControls || button == BtnRules)){

                    button.Visible = true;
                }
            }

            if (activeLevel != null){

                mainPanel.Controls.Remove(activeLevel);
                activeLevel.Close();
                activeLevel = null;
            }
        }
    }
}