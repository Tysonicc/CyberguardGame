using System;
using System.Windows.Forms;

namespace CyberguardGame
{
    public partial class MainGame : Form
    {
        /** Etykieta do wyswietlania informacji */
        private Label infoLabel;

        /** Przycisk powrotu */
        private Button btnBack;
        /** Drugi przycisk powrotu */
        private Button btnBack_2;
        /** Przycisk poziomu 1 */
        private Button btnLevel1;
        /** Przycisk poziomu 2 */
        private Button btnLevel2;
        /** Przycisk poziomu 3 */
        private Button btnLevel3;
        /** Przycisk powrotu do menu glownego */
        private Button btnReturnToMainMenu;

        /** Glowny panel  */
        private Panel mainPanel;

        /** Flaga do sledzenia ukoñczenia poziomu 1 */
        private bool level1Completed = false;
        /** Flaga do sledzenia ukoñczenia poziomu 2 */
        private bool level2Completed = false;
        
        /** Aktywny poziom */
        private Form activeLevel = new Form();                                                       

        private MainGame()
        {
            InitializeComponent();                                                                  /** Inicjalizacja komponentow */
            StartScreen();                                                                          /** Wywolanie metody do wyswietlania ekranu glownego */

            /** Ustawienie tla formularzu */
            this.BackgroundImage = Properties.Resources.JEMA_GER_1426_24_v1;
            this.BackgroundImageLayout = ImageLayout.Stretch;
        }

        /** Metoda, ktora otwiera dany poziom */
        private void OpenLevel(Form LevelForm, object btnSender)
        {
            /** Sprawdzenie, czy aktywny poziom istnieje i zamkniecie go */
            if (activeLevel != null)
            {
                mainPanel.Controls.Remove(activeLevel);
                activeLevel.Close();
            }

            activeLevel = LevelForm;                                                                /** Ustawienie nowego poziomu jako aktywnego */
            
            LevelForm.TopLevel = false;                                                             /** Ustawienie danego poziomu jako podrzednego */
            LevelForm.FormBorderStyle = FormBorderStyle.None;                                       /** Usuniecie ramki */
            LevelForm.Dock = DockStyle.Fill;                                                        /** Wypelnienie panelu */
            
            mainPanel.Controls.Add(LevelForm);                                                      /** Dodanie poziomu do panelu */
            mainPanel.Tag = LevelForm;                                                              /** Ustawienie tagu panelu */
            
            LevelForm.BringToFront();                                                               /** Przeniesienie poziomu na wierzch */
            LevelForm.Show();                                                                       /** Wyswietlenie danego poziomu  */
            LevelForm.Focus();                                                                      /** Ustawienie fokusu na danym poziomie */
        }
        
        /** Metoda, ktora sluzy do wyswietlania ekranu startowego z danymi przyciskami w danym stylu  */
        private void StartScreen()
        {
            /** Dodanie i wystylizowanie panelu do formularza */
            mainPanel = new Panel();
            mainPanel.Size = this.ClientSize;
            mainPanel.Location = new System.Drawing.Point(0, 0);
            mainPanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            mainPanel.BackColor = Color.Transparent;
            this.Controls.Add(mainPanel);

            /** Dodanie i wystylizowanie etykiety potrzebnej do wyswietlania informacji */
            infoLabel = new Label();
            infoLabel.Size = new System.Drawing.Size(500, 200);
            infoLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            infoLabel.Location = new System.Drawing.Point((this.ClientSize.Width - infoLabel.Width) / 2, (this.ClientSize.Height - infoLabel.Height) / 2);
            infoLabel.Font = new System.Drawing.Font("Arial", 12);
            infoLabel.Visible = false;
            infoLabel.Anchor = AnchorStyles.None;
            mainPanel.Controls.Add(infoLabel);

            /** Dodanie i wystylizowanie przycisku powrotu */
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
            btnBack.Font = new System.Drawing.Font("Snap ITC", 10);
            btnBack.ForeColor = SystemColors.HighlightText;
            mainPanel.Controls.Add(btnBack);

            /** Dodanie i wystylizowanie drugiego przycisku powrotu */
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
            btnBack_2.Font = new System.Drawing.Font("Snap ITC", 10);
            btnBack_2.ForeColor = SystemColors.HighlightText;
            mainPanel.Controls.Add(btnBack_2);

            /** Dodanie i wystylizowanie przycisku poziomu 1 */
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

            /** Dodanie i wystylizowanie przycisku poziomu 2 */
            btnLevel2 = new Button();
            btnLevel2.Text = "POZIOM 2";
            btnLevel2.Size = new System.Drawing.Size(125, 40);
            btnLevel2.Location = new System.Drawing.Point(439, 435);
            btnLevel2.Click += new EventHandler(BtnLevel2_Click);
            btnLevel2.Visible = false;
            btnLevel2.Enabled = false;
            btnLevel2.BackColor = SystemColors.WindowFrame;
            btnLevel2.FlatAppearance.BorderColor = Color.Black;
            btnLevel2.FlatAppearance.BorderSize = 1;
            btnLevel2.FlatAppearance.MouseOverBackColor = Color.DarkGray;
            btnLevel2.FlatStyle = FlatStyle.Flat;
            btnLevel2.Font = new System.Drawing.Font("Snap ITC", 11);
            btnLevel2.ForeColor = SystemColors.HighlightText;
            mainPanel.Controls.Add(btnLevel2);

            /** Dodanie i wystylizowanie przycisku poziomu 3 */
            btnLevel3 = new Button();
            btnLevel3.Text = "POZIOM 3";
            btnLevel3.Size = new System.Drawing.Size(125, 40);
            btnLevel3.Location = new System.Drawing.Point(439, 485);
            btnLevel3.Click += new EventHandler(BtnLevel3_Click);
            btnLevel3.Visible = false;
            btnLevel3.Enabled = false;
            btnLevel3.BackColor = SystemColors.WindowFrame;
            btnLevel3.FlatAppearance.BorderColor = Color.Black;
            btnLevel3.FlatAppearance.BorderSize = 1;
            btnLevel3.FlatAppearance.MouseOverBackColor = Color.DarkGray;
            btnLevel3.FlatStyle = FlatStyle.Flat;
            btnLevel3.Font = new System.Drawing.Font("Snap ITC", 11);
            btnLevel3.ForeColor = SystemColors.HighlightText;
            mainPanel.Controls.Add(btnLevel3);

            /** Dodanie i wystylizowanie przycisku powrotu do glownego menu */
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
            btnReturnToMainMenu.Font = new System.Drawing.Font("Snap ITC", 10);
            btnReturnToMainMenu.ForeColor = SystemColors.HighlightText;
            mainPanel.Controls.Add(btnReturnToMainMenu);
        }

        /** Metoda obslugujaca klikniecie przycisku startu */
        private void BtnStart_Click(object sender, EventArgs e)
        {
            /** Wyswietlenie przyciskow z poziomami i przycisku powrotu do glownego menu */
            btnLevel1.Visible = true;
            btnLevel2.Visible = true;
            btnLevel3.Visible = true;
            btnReturnToMainMenu.Visible = true;

            /** Ukrycie innych przyciskow */
            foreach (Control control in this.Controls)
            {
                if (control is Button && control != btnLevel1 && control != btnLevel2 && control != btnLevel3 && control != btnReturnToMainMenu)
                {
                    control.Visible = false;                                                        /** Reszta przyciskow ukryta */
                }
            }
        }

        /** Metoda obslugujaca klikniecie przycisku poziomu 1 */
        private void BtnLevel1_Click(object sender, EventArgs e)
        {
            Level_1 level_1 = new Level_1();                                                        /** Tworzenie nowego obiektu poziomu 1 */
            level_1.FormClosed += (s, args) => {btnLevel2.Enabled = true;};                         /** Ustawienie zdarzenia, ktore wlaczy przycisk poziomu 2 po zamknieciu poziomu 1 */
            OpenLevel(level_1, sender);                                                             /** Otworzenie poziomu 1 */
        }

        /** Metoda obslugujaca klikniecie przycisku poziomu 2 */
        private void BtnLevel2_Click(object sender, EventArgs e)
        {
            Level_2 level_2 = new Level_2();                                                        /** Tworzenie nowego obiektu poziomu 2 */
            level_2.FormClosed += (s, args) => OpenLevel(new Level_3(), sender);                    /** Ustawienie zdarzenia, ktore wlaczy przycisk poziomu 3 po zamknieciu poziomu 2 */
            OpenLevel(level_2, sender);                                                             /** Otworzenie poziomu 2 */
        }

        /** Metoda obslugujaca klikniecie przycisku poziomu 3 */
        private void BtnLevel3_Click(object sender, EventArgs e)
        {
            Level_3 level_3 = new Level_3();                                                        /** Tworzenie nowego obiektu poziomu 2 */
            OpenLevel(level_3, sender);                                                             /** Otworzenie poziomu 3 */
        }

        /** Metoda obslugujaca klikniecie przycisku do wyswietlania informacji o sterowaniu */
        private void BtnControls_Click(object sender, EventArgs e)
        {
            /** Definiowanie tekstu z informacjami o sterowaniu */
            string controls = "STEROWANIE:\n" +
                              "W - RUCH W GÓRÊ\n" +
                              "A - RUCH W LEWO\n" +
                              "S - RUCH W DÓ£\n" +
                              "D - RUCH W PRAWO\n" +
                              "LPM - LEWY PRZYCISK MYSZY";

            infoLabel.Text = controls;                                                              /** Ustawienie tekstu etykiety na tekst sterowania */
            ShowInfoScreen();                                                                       /** Wywolanie metody do wyswietlania ekranu z informacjami */
        }

        /** Metoda obslugujaca klikniecie przycisku do wyswietlania zasad gry */
        private void BtnRules_Click(object sender, EventArgs e)
        {
            string rules = "Wcielasz siê w rolê specjalisty ds. bezpieczeñstwa sieciowego, chroni¹c wirtualne biuro przed zagro¿eniami.";
            infoLabel.Text = rules;                                                                 /** Ustawienie tekstu etykiety na tekst z zasadami gry */
            ShowInfoScreen();                                                                       /** Wywolanie metody do wyswietlania ekranu z informacjami */
        }

        /** Metoda do wyswietlania ekranu z informacjami  */
        private void ShowInfoScreen()
        {
            infoLabel.Visible = true;                                                               /** Widocznosz etykiety */
            btnBack_2.Visible = true;                                                               /** Widocznosz drugiego przycisku powrotu */
            
            /** Wystylizowanie etykiety potrzebnej do wyswietlania informacji */
            infoLabel.BackColor = Color.Black;
            infoLabel.BorderStyle = BorderStyle.FixedSingle;
            infoLabel.Padding = new Padding(10);
            infoLabel.BackColor = SystemColors.WindowFrame;
            infoLabel.FlatStyle = FlatStyle.Flat;
            infoLabel.Font = new System.Drawing.Font("Snap ITC", 16);
            infoLabel.ForeColor = SystemColors.HighlightText;

            /** Ukrycie wszystkich przyciskow oprocz drugiego przycisku powrotu */
            foreach (Control control in this.Controls)
            {
                if (control is Button && control != btnBack_2)
                {
                    control.Visible = false;                                                        /** Reszta przyciskow ukryta */
                }
            }
        }

        /** Metoda obslugujaca klikniecie przycisku powrotu */
        private void BtnBack_Click(object sender, EventArgs e)
        {
            infoLabel.Visible = false;                                                              /** Widocznosz etykiety */
            btnBack.Visible = false;                                                                /** Widocznosz przycisku powrotu */

            /** Przywrocenie widocznosci przyciskow poziomow i przycisku powrotu do menu glownego */
            foreach (Control control in this.Controls)
            {
                if (control is Button && (control == btnLevel1 || control == btnLevel2 || control == btnLevel3 || control == btnReturnToMainMenu))
                {
                    control.Visible = true;                                                         /** Widocznosz przyciskow */
                }
            }
        }

        /** Metoda obslugujaca klikniecie drugiego przycisku powrotu */
        private void BtnBack_Click_2(object sender, EventArgs e)
        {
            infoLabel.Visible = false;                                                              /** Widocznosz etykiety */
            btnBack_2.Visible = false;                                                              /** Widocznosz przycisku powrotu */

            /** Przywrocenie widocznosci przyciskow poziomow i przycisku powrotu do menu glownego */
            foreach (Control control in this.Controls)
            {
                if (control is Button && (control == BtnStart || control == BtnControls || control == BtnRules))
                {
                    control.Visible = true;                                                         /** Widocznosz przyciskow */
                }
            }
        }

        /** Metoda obslugujaca klikniecie przycisku powrotu do menu glownego */
        private void BtnReturnToMainMenu_Click(object sender, EventArgs e)
        {
            /** Ukrycie przyciskow z poziomami i przycisku powrotu do glownego menu */
            btnLevel1.Visible = false;
            btnLevel2.Visible = false;
            btnLevel3.Visible = false;
            btnReturnToMainMenu.Visible = false;

            /** Przywrocenie widocznosci przyciskow startowych i kontrolnych */
            BtnStart.Visible = true;
            BtnControls.Visible = true;
            BtnRules.Visible = true;
        }

        /** Metoda do wyswietlania glownego panelu */
        public void ShowMainPanel()
        {
            mainPanel.Visible = true;                                                               /** Ustawienie widocznosci glownego panelu na prawde */

            /** Przywrocenie widocznosci przyciskow w glownym panelu */
            foreach (Control control in mainPanel.Controls)
            {
                if (control is Button button && (button == BtnStart || button == BtnControls || button == BtnRules))
                {
                    button.Visible = true;
                }
            }

            /** Sprawdzenie, czy aktywny poziom istnieje */
            if (activeLevel != null)
            {
                mainPanel.Controls.Remove(activeLevel);                                             /** Usuniecie aktywnego poziomu z glownego panelu */
                activeLevel.Close();                                                                /** Zamkniecie aktywnego poziomu */
                activeLevel = null;                                                                 /** Ustawienie aktywnego poziomu na wartosz null */
            }
        }
    }
}