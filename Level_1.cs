using System;
using System.Drawing;
using System.Windows.Forms;

namespace CyberguardGame
{
    public partial class Level_1 : Form
    {
        private Button btnBack;                             
        
        private Panel optionsPanel;                             // Panel z kontrolkami
        private Panel scorePanel, timePanel;                    // Panele z punktami i czasem
        private Panel mazePanel;                                // Panel do wyświetlania labiryntu
        private Panel responsePanel;                            // Panel do wyświetlania wiadomości
        private Panel emailPanel;                               // Panel do wyświetlania maila
        private Panel suspiciousLinkPanel;                      // Panel do wyświetlania podejrzanego linku
        private Panel emailPanel_2;                             // Panel do wyświetlania maila 2
        private Panel completionPanel;                          // Panel informacyjny po ukończeniu poziomu
        private Panel notificationPanel;

        private Label scoreLabel, timerLabel;
        private Label panelTitle;

        private CheckBox checkBox1, checkBox2, checkBox3;

        private Maze maze;                                      // Obiekt labiryntu
        private Player player;                                  // Obiekt gracza
        
        private const int cellWidth = 40;                       // Szerokość komórki
        private const int cellHeight = 40;                      // Wysokość komórki

        private int totalPoints = 0;                            // Zmienna do przechowywania punktów
        private int elapsedTime = 0;                            // Licznik czasu w sekundach        

        private System.Windows.Forms.Timer gameTimer;           // Timer do liczenia czasu
        private System.Windows.Forms.Timer defenderTimer;       // Timer do odznaczania Windows Defender
        private System.Windows.Forms.Timer responseTimer;       // Timer do odliczania czasu na odpowiedź
        private System.Windows.Forms.Timer antiVirusTimer;      // Timer do zarządzania kontrolką Antywirus
        private System.Windows.Forms.Timer emailTimer;          // Timer do wyświetlania maila
        private System.Windows.Forms.Timer suspiciousLinkTimer; // Timer do wyświetlania panelu z podejrzanym linkiem
        private System.Windows.Forms.Timer emailTimer_2;        // Timer do wyświetlania maila
        private System.Windows.Forms.Timer notificationTimer;   // Timer do wyświetlania powiadomienia

        private bool checkBox1Checked = false;
        private bool checkBox2Checked = false;
        private bool checkBox3Checked = false;
        private bool isMazeCompleted = false;
        private bool serverSecuredMessageShown = false;
        private bool isResponsePanelShown = false;
        private bool isEmailPanelShown = false;
        private bool isSuspiciousLinkPanelShown = false;
        private bool isEmailPanel2Shown = false;
        private bool isNotificationPanelShown = false;

        private Form activeLevel = new Form();

        public Level_1()
        {
            InitializeComponent();
            InitializeGame();
            InitializeTimer();
        }

        private void OpenLevel(Form levelForm)
        {
            if (activeLevel != null)
            {
                activeLevel.Close();                                    // Zamknij aktualny poziom, jeśli istnieje
            }

            activeLevel = levelForm;                                    // Ustaw nowy poziom jako aktywny
            levelForm.TopLevel = false;                                 // Ustaw poziom jako podrzędny
            levelForm.FormBorderStyle = FormBorderStyle.None;           // Usuń ramkę
            levelForm.Dock = DockStyle.Fill;                            // Wypełnij panel

            this.Controls.Add(levelForm);                               // Dodaj poziom do formularza
            levelForm.BringToFront();                                   // Przenieś poziom na wierzch
            levelForm.Show();                                           // Pokaż poziom
        }

        private void InitializeGame()
        {
            maze = new Maze(MazeLevel.Easy.Width, MazeLevel.Easy.Height);
            maze.GenerateMaze(MazeLevel.Easy.Grid, MazeLevel.Easy.EndPoint);

            player = new Player(MazeLevel.Easy.StartPoint.X, MazeLevel.Easy.StartPoint.Y);              // Ustawienie gracza w punkcie startowym

            mazePanel = new BufforedPanel();
            mazePanel.Size = new Size(MazeLevel.Easy.Width * cellWidth + 50,  MazeLevel.Easy.Height * cellHeight + 50);
            mazePanel.Location = new Point((this.ClientSize.Width - mazePanel.Width) / 2, (this.ClientSize.Height - mazePanel.Height) / 2);
            
            mazePanel.BackColor = Color.LightSteelBlue;
            mazePanel.BorderStyle = BorderStyle.Fixed3D;
            mazePanel.Padding = new Padding(10);

            this.Controls.Add(mazePanel);

            mazePanel.Paint += new PaintEventHandler(MazePanel_Paint);

            this.KeyDown += new KeyEventHandler(Level_1_KeyDown);
            this.KeyPreview = true;

            btnBack = new Button();
            btnBack.Text = "KONIEC";
            btnBack.Size = new System.Drawing.Size(100, 50);
            btnBack.Location = new System.Drawing.Point(this.ClientSize.Width - btnBack.Width - 20, this.ClientSize.Height - btnBack.Height - 20);
            btnBack.Click += new EventHandler(BtnBack_Click);
            btnBack.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnBack.BackColor = SystemColors.WindowFrame;
            btnBack.FlatAppearance.BorderColor = Color.Black;
            btnBack.FlatAppearance.BorderSize = 1;
            btnBack.FlatAppearance.MouseOverBackColor = Color.DarkGray;
            btnBack.FlatStyle = FlatStyle.Flat;
            btnBack.Font = new System.Drawing.Font("Snap ITC", 11);
            btnBack.ForeColor = SystemColors.HighlightText;
            this.Controls.Add(btnBack);

            scorePanel = new System.Windows.Forms.Panel();
            scorePanel.Size = new System.Drawing.Size(200, 50);
            scorePanel.Location = new System.Drawing.Point(this.ClientSize.Width - scorePanel.Width - 20, 20);
            scorePanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            scorePanel.BackColor = SystemColors.WindowFrame;
            scorePanel.BorderStyle = BorderStyle.FixedSingle;
            scorePanel.Padding = new Padding(10);
            this.Controls.Add(scorePanel);

            scoreLabel = new Label();
            scoreLabel.Text = "PUNKTY: " + totalPoints;
            scoreLabel.Location = new System.Drawing.Point(10, 10);
            scoreLabel.AutoSize = true;
            scoreLabel.Font = new Font("Snap ITC", 11);
            scoreLabel.ForeColor = SystemColors.HighlightText;
            scorePanel.Controls.Add(scoreLabel);

            timePanel = new System.Windows.Forms.Panel();
            timePanel.Size = new System.Drawing.Size(200, 50);
            timePanel.Location = new System.Drawing.Point(this.ClientSize.Width - timePanel.Width - 20, 80);
            timePanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            timePanel.BackColor = SystemColors.WindowFrame;
            timePanel.BorderStyle = BorderStyle.FixedSingle;
            timePanel.Padding = new Padding(10);
            this.Controls.Add(timePanel);

            timerLabel = new Label();
            timerLabel.Text = "CZAS: 0 s";
            timerLabel.Location = new System.Drawing.Point(10, 10);
            timerLabel.AutoSize = true;
            timerLabel.Font = new Font("Snap ITC", 11);
            timerLabel.ForeColor = SystemColors.HighlightText;
            timePanel.Controls.Add(timerLabel);
        }

        private void InitializeTimer()
        {
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1000;
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            elapsedTime++;
            timerLabel.Text = "CZAS: " + elapsedTime + " s";

            if (elapsedTime >= 60)
            {
                EndGame();
            }
        }

        private void MazePanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
        
            int margin = 10;
            int offsetX = (mazePanel.Width - (maze.Width * cellWidth)) / 2;
            int offsetY = (mazePanel.Height - (maze.Height * cellHeight)) / 2;

            for (int y = 0; y < maze.Height; y++)
            {
                for (int x = 0; x < maze.Width; x++)
                {
                    // Sprawdzenie, czy to pozycja gracza
                    if (x == player.X && y == player.Y)
                    {
                        // Ustawienie tła na ciemno-szare dla pola, na którym stoi gracz
                        g.FillRectangle(Brushes.ForestGreen, offsetX + x * cellWidth, offsetY + y * cellHeight, cellWidth, cellHeight);
                    }
                    else
                    {
                        // Ustawienie tła na czarne dla pozostałych pól
                        g.FillRectangle(Brushes.Black, offsetX + x * cellWidth, offsetY + y * cellHeight, cellWidth, cellHeight);
                    }

                    string text = maze.Grid[y, x] == 1 ? "1" : "0";                         // Ustawienie tekstu na "1" dla ściany, "0" dla przestrzeni

                    Font font = new Font("Arial", 16);
                    Brush textBrush = Brushes.Green;
                    g.DrawString(text, font, textBrush, offsetX + x * cellWidth + cellWidth / 4, offsetY + y * cellHeight + cellHeight / 4);
                }
            }

            Font playerFont = new Font("Arial", 16);
            Brush playerBrush = Brushes.Black;
            g.DrawString("0", playerFont, playerBrush, offsetX + player.X * cellWidth + cellWidth / 4, offsetY + player.Y * cellHeight + cellHeight / 4);

            if (!serverSecuredMessageShown)
            {                                                
                MessageBox.Show("Zabezpiecz serwer!", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                serverSecuredMessageShown = true; 
            }
        }

        private void Level_1_KeyDown(object sender, KeyEventArgs e)
        {
            if (elapsedTime >= 60)
            {
                EndGame();
            }
            
            player.Move(e.KeyCode, maze);                                               // Przekazanie obiektu maze
            mazePanel.Invalidate();                                                     // Odświeżenie panelu

            if (player.X == maze.EndPoint.X && player.Y == maze.EndPoint.Y)             // Sprawdzenie, czy gracz osiągnął punkt końcowy
            {            
                totalPoints += 100; 
                scoreLabel.Text = "PUNKTY: " + totalPoints;
                mazePanel.Visible = false;
                
                ShowOptionsPanel();

                isMazeCompleted = true;

                if (!isResponsePanelShown)
                {
                    StartResponseTimer();
                    isResponsePanelShown = true;
                }

                if (!isEmailPanelShown)
                {
                    StartEmailTimer();
                    isEmailPanelShown = true;
                }

                if (!isSuspiciousLinkPanelShown)
                {
                    StartSuspiciousLinkTimer();
                    isSuspiciousLinkPanelShown = true;
                }

                if (!isEmailPanel2Shown)
                {
                    StartEmailTimer_2();
                    isEmailPanel2Shown = true;
                }

                if (!isNotificationPanelShown)
                {
                    StartNotificationTimer();
                    isNotificationPanelShown = true;
                }
            }
        }

        // Pierwsza odpowiedź
        private void StartResponseTimer()
        {
            responseTimer = new System.Windows.Forms.Timer();
            responseTimer.Interval = 3000;
            responseTimer.Tick += ResponseTimer_Tick;
            responseTimer.Start();
        }

        private void ResponseTimer_Tick(object sender, EventArgs e)
        { 
            responseTimer.Stop();
            ShowResponsePanel();
        }

        private void ShowResponsePanel()
        {
            responsePanel = new System.Windows.Forms.Panel();
            responsePanel.Size = new System.Drawing.Size(250, 150);
            int leftMargin = 20;
            int bottomMargin = 20;
            responsePanel.Location = new System.Drawing.Point(leftMargin, this.ClientSize.Height - responsePanel.Height - bottomMargin);
            responsePanel.BackColor = Color.LightBlue;
            responsePanel.BorderStyle = BorderStyle.FixedSingle;
            responsePanel.Padding = new Padding(10);

            Label fromLabel = new Label();
            fromLabel.Text = "Od: Pracownik Marek";
            fromLabel.AutoSize = true;
            fromLabel.Location = new Point(10, 10);
            responsePanel.Controls.Add(fromLabel);

            Label toLabel = new Label();
            toLabel.Text = "Do: Serwerownia";
            toLabel.AutoSize = true;
            toLabel.Location = new Point(10, 30);
            responsePanel.Controls.Add(toLabel);

            Label titleLabel = new Label();
            titleLabel.Text = "Tytuł: Pliki";
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(10, 50);
            responsePanel.Controls.Add(titleLabel);

            Label contentLabel = new Label();
            contentLabel.Text = "Treść: Cześć, mogę przesłać pliki?";
            contentLabel.AutoSize = true;
            contentLabel.Location = new Point(10, 70);
            responsePanel.Controls.Add(contentLabel);

            Label sourceLabel = new Label();
            sourceLabel.Text = "Źródło: Nieznane";
            sourceLabel.AutoSize = true;
            sourceLabel.Location = new Point(10, 90);
            responsePanel.Controls.Add(sourceLabel);

            Button btnAgree = new Button();
            btnAgree.Text = "Zgadzam się";
            btnAgree.Location = new Point(20, 120);
            btnAgree.Click += (s, args) => { HandleResponse(true); };
            btnAgree.AutoSize = true;
            responsePanel.Controls.Add(btnAgree);

            Button btnDisagree = new Button();
            btnDisagree.Text = "Nie zgadzam się";
            btnDisagree.Location = new Point(120, 120);
            btnDisagree.Click += (s, args) => { HandleResponse(false); };
            btnDisagree.AutoSize = true;
            responsePanel.Controls.Add(btnDisagree);

            this.Controls.Add(responsePanel);
        }

        private void HandleResponse(bool isAgreed)
        {
            if (isAgreed)
            {
                checkBox3.Checked = false;
                totalPoints -= 100;
                StartAntiVirusTimer();
            }
            else
            {
                totalPoints += 50;
            }

            scoreLabel.Text = "PUNKTY: " + totalPoints;

            this.Controls.Remove(responsePanel);
        }

        // Pierwszy e-mail
        private void StartEmailTimer()
        {
            emailTimer = new System.Windows.Forms.Timer();
            emailTimer.Interval = 7000;
            emailTimer.Tick += EmailTimer_Tick;
            emailTimer.Start();
        }

        private void EmailTimer_Tick(object sender, EventArgs e)
        {
            emailTimer.Stop();
            ShowEmailPanel();
        }

        private void ShowEmailPanel()
        {
            emailPanel = new System.Windows.Forms.Panel();
            emailPanel.Size = new System.Drawing.Size(250, 150);
            emailPanel.Location = new System.Drawing.Point((this.ClientSize.Width - emailPanel.Width) / 2, (this.ClientSize.Height - emailPanel.Height) / 2);
            emailPanel.BackColor = Color.LightGray;
            emailPanel.BorderStyle = BorderStyle.FixedSingle;
            emailPanel.Padding = new Padding(10);

            Label fromLabel = new Label();
            fromLabel.Text = "Od: Prezes Janusz";
            fromLabel.AutoSize = true;
            fromLabel.Location = new Point(10, 10);
            emailPanel.Controls.Add(fromLabel);

            Label toLabel = new Label();
            toLabel.Text = "Do: Serwerownia";
            toLabel.AutoSize = true;
            toLabel.Location = new Point(10, 30);
            emailPanel.Controls.Add(toLabel);

            Label titleLabel = new Label();
            titleLabel.Text = "Tytuł: Kopia zapasowa";
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(10, 50);
            emailPanel.Controls.Add(titleLabel);

            Label contentLabel = new Label();
            contentLabel.Text = "Treść: Witam, proszę zrobić kopie danych";
            contentLabel.AutoSize = true;
            contentLabel.Location = new Point(10, 70);
            emailPanel.Controls.Add(contentLabel);

            Button btnAgree = new Button();
            btnAgree.Text = "Zrób kopie";
            btnAgree.Location = new Point(30, 100);
            btnAgree.Click += (s, args) => { HandleEmailResponse(true); };
            emailPanel.Controls.Add(btnAgree);

            Button btnDisagree = new Button();
            btnDisagree.Text = "Nie rób kopii";
            btnDisagree.Location = new Point(130, 100);
            btnDisagree.Click += (s, args) => { HandleEmailResponse(false); };
            emailPanel.Controls.Add(btnDisagree);

            this.Controls.Add(emailPanel);
        }

        private void HandleEmailResponse(bool isAgreed)
        {
            if (isAgreed)
            {
                totalPoints += 50;
            }
            else
            {
                totalPoints -= 100;
            }

            scoreLabel.Text = "PUNKTY: " + totalPoints;
            this.Controls.Remove(emailPanel);
        }

        // Podejrzany link
        private void StartSuspiciousLinkTimer()
        {
            suspiciousLinkTimer = new System.Windows.Forms.Timer();
            suspiciousLinkTimer.Interval = 10000;
            suspiciousLinkTimer.Tick += SuspiciousLinkTimer_Tick;
            suspiciousLinkTimer.Start();
        }

        private void SuspiciousLinkTimer_Tick(object sender, EventArgs e)
        {
            suspiciousLinkTimer.Stop();
            ShowSuspiciousLinkPanel();
        }

        private void ShowSuspiciousLinkPanel()
        {
            suspiciousLinkPanel = new System.Windows.Forms.Panel();
            suspiciousLinkPanel.Size = new System.Drawing.Size(250, 60);

            suspiciousLinkPanel.Location = new System.Drawing.Point(this.ClientSize.Width - suspiciousLinkPanel.Width - 20, (this.ClientSize.Height - suspiciousLinkPanel.Height) / 2);

            suspiciousLinkPanel.BackColor = Color.LightGray;
            suspiciousLinkPanel.BorderStyle = BorderStyle.FixedSingle;
            suspiciousLinkPanel.Padding = new Padding(10);

            Label linkLabel = new Label();
            linkLabel.Text = "www.tanieauta-Polska.com";
            linkLabel.Location = new Point(30, 10);
            linkLabel.AutoSize = true;
            suspiciousLinkPanel.Controls.Add(linkLabel);

            Button btnAgree = new Button();
            btnAgree.Text = "Sprawdzam";
            btnAgree.Location = new Point(30, 30);
            btnAgree.Click += (s, args) => { HandleSuspiciousLinkResponse(true); };
            suspiciousLinkPanel.Controls.Add(btnAgree);

            Button btnDisagree = new Button();
            btnDisagree.Text = "Odrzucam";
            btnDisagree.Location = new Point(130, 30);
            btnDisagree.Click += (s, args) => { HandleSuspiciousLinkResponse(false); };
            suspiciousLinkPanel.Controls.Add(btnDisagree);

            this.Controls.Add(suspiciousLinkPanel);
        }

        private void HandleSuspiciousLinkResponse(bool isAgreed)
        {
            if (isAgreed)
            {
                totalPoints -= 100;
                scoreLabel.Text = "PUNKTY: " + totalPoints;
                checkBox3.Checked = false;

                for (int i = 1; i <= 10; i++)
                {
                    MessageBox.Show("MessageBox " + i + ": Uwaga! Podejrzany link!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                totalPoints += 200;
                scoreLabel.Text = "PUNKTY: " + totalPoints;
            }

            suspiciousLinkPanel.Visible = false;
        }

        // Antywirus
        private void StartAntiVirusTimer()
        {
            antiVirusTimer = new System.Windows.Forms.Timer();
            antiVirusTimer.Interval = 8000;
            antiVirusTimer.Tick += AntiVirusTimer_Tick;
            antiVirusTimer.Start();
        }

        private void AntiVirusTimer_Tick(object sender, EventArgs e)
        {
            antiVirusTimer.Stop();

         
            if (!checkBox3.Checked)
            {
              
                totalPoints -= 50;
            }
            else
            {
                
                totalPoints += 75;
            }

            scoreLabel.Text = "PUNKTY: " + totalPoints;
        }

        // Zabezpieczenia serwera - panel z kontrolkami
        private void ShowOptionsPanel()
        {
            optionsPanel = new System.Windows.Forms.Panel();
            optionsPanel.Size = new System.Drawing.Size(200, 150);
            optionsPanel.Location = new System.Drawing.Point(20, 20);
            optionsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            optionsPanel.BackColor = Color.LightGray;
            optionsPanel.BorderStyle = BorderStyle.Fixed3D;
            optionsPanel.Padding = new Padding(10);

            panelTitle = new Label();
            panelTitle.Text = "ZABEZPIECZENIA SERWERA";
            panelTitle.Font = new Font("Tahoma", 8, FontStyle.Bold);
            panelTitle.ForeColor = Color.Black;
            panelTitle.Location = new Point(10, 10);
            panelTitle.AutoSize = true;

            optionsPanel.Controls.Add(panelTitle);

            checkBox1 = new CheckBox();
            checkBox1.Text = "WINDOWS DEFENDER";
            checkBox1.Location = new System.Drawing.Point(10, 40);
            checkBox1.AutoSize = true;
            checkBox1.CheckedChanged += new EventHandler(CheckBox_CheckedChanged);

            checkBox2 = new CheckBox();
            checkBox2.Text = "FIREWALL";
            checkBox2.Location = new System.Drawing.Point(10, 70);
            checkBox2.AutoSize = true;
            checkBox2.CheckedChanged += new EventHandler(CheckBox_CheckedChanged);

            checkBox3 = new CheckBox();
            checkBox3.Text = "ANTYWIRUS";
            checkBox3.Location = new System.Drawing.Point(10, 100);
            checkBox3.AutoSize = true;
            checkBox3.CheckedChanged += new EventHandler(CheckBox_CheckedChanged);

            optionsPanel.Controls.Add(checkBox1);
            optionsPanel.Controls.Add(checkBox2);
            optionsPanel.Controls.Add(checkBox3);

            this.Controls.Add(optionsPanel);

            StartDefenderTimer();
        }

        // Defender
        private void StartDefenderTimer()
        {
            if (defenderTimer != null)
            {
                defenderTimer.Stop();
            }

            defenderTimer = new System.Windows.Forms.Timer();
            defenderTimer.Interval = 15000;
            defenderTimer.Tick += DefenderTimer_Tick;
            defenderTimer.Start();
        }

        private void DefenderTimer_Tick(object sender, EventArgs e)
        {
            defenderTimer.Stop();

            checkBox1.Checked = false;
            checkBox1Checked = false;
            totalPoints -= 100;
            scoreLabel.Text = "PUNKTY: " + totalPoints;

            NotifyWindowsDefenderDisabled();
        }

        // Drugi e-mail
        private void StartEmailTimer_2()
        {
            emailTimer_2 = new System.Windows.Forms.Timer();
            emailTimer_2.Interval = 15000;
            emailTimer_2.Tick += EmailTimer_Tick_2;
            emailTimer_2.Start();
        }

        private void EmailTimer_Tick_2(object sender, EventArgs e)
        {
            emailTimer_2.Stop();
            ShowEmailPanel_2();
        }

        private void ShowEmailPanel_2()
        {
            emailPanel_2 = new System.Windows.Forms.Panel();
            emailPanel_2.Size = new System.Drawing.Size(250, 150);
            emailPanel_2.Location = new System.Drawing.Point((this.ClientSize.Width - emailPanel_2.Width) / 2 + 200, (this.ClientSize.Height - emailPanel_2.Height) / 2 + 250);

            emailPanel_2.BackColor = Color.LightBlue;
            emailPanel_2.BorderStyle = BorderStyle.FixedSingle;
            emailPanel_2.Padding = new Padding(10);

            Label fromLabel = new Label();
            fromLabel.Text = "Od: Nieznany Gość";
            fromLabel.AutoSize = true;
            fromLabel.Location = new Point(10, 10);
            emailPanel_2.Controls.Add(fromLabel);

            Label toLabel = new Label();
            toLabel.Text = "Do: Serwerownia";
            toLabel.AutoSize = true;
            toLabel.Location = new Point(10, 30);
            emailPanel_2.Controls.Add(toLabel);

            Label titleLabel = new Label();
            titleLabel.Text = "Tytuł: Prośba o dostęp";
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(10, 50);
            emailPanel_2.Controls.Add(titleLabel);

            Label contentLabel = new Label();
            contentLabel.Text = "Treść: Cześć, dasz dostęp do danych?";
            contentLabel.AutoSize = true;
            contentLabel.Location = new Point(10, 70);
            emailPanel_2.Controls.Add(contentLabel);

            Button btnAgree = new Button(); 
            btnAgree.Text = "Zgadzam się"; 
            btnAgree.Location = new Point(30, 100); 
            btnAgree.Click += (s, args) => { HandleEmailResponse_2(true); };
            btnAgree.AutoSize = true;
            emailPanel_2.Controls.Add(btnAgree); 

            Button btnDisagree = new Button(); 
            btnDisagree.Text = "Nie zgadzam się"; 
            btnDisagree.Location = new Point(130, 100); 
            btnDisagree.Click += (s, args) => { HandleEmailResponse_2(false); };
            btnDisagree.AutoSize = true;
            emailPanel_2.Controls.Add(btnDisagree);

            this.Controls.Add(emailPanel_2);
        }

        private void HandleEmailResponse_2(bool isAgreed)
        {
            if (isAgreed)
            {
                totalPoints -= 100;
                scoreLabel.Text = "PUNKTY: " + totalPoints;
                checkBox2.Checked = false;

                ResetMaze();
            }
            else
            {
                totalPoints += 100;
                scoreLabel.Text = "PUNKTY: " + totalPoints;
            }

            emailPanel_2.Visible = false;
        }

        // Powiadomienie o wygranej
        private void StartNotificationTimer()
        {
            notificationTimer = new System.Windows.Forms.Timer();
            notificationTimer.Interval = 20000;
            notificationTimer.Tick += NotificationTimer_Tick;
            notificationTimer.Start();
        }

        private void NotificationTimer_Tick(object sender, EventArgs e)
        {
            notificationTimer.Stop();
            ShowNotificationPanel();
        }

        private void ShowNotificationPanel()
        {
            notificationPanel = new System.Windows.Forms.Panel();
            notificationPanel.Size = new System.Drawing.Size(250, 100);
            notificationPanel.Location = new System.Drawing.Point((this.ClientSize.Width - notificationPanel.Width) / 2, 20);
            notificationPanel.BackColor = Color.LightBlue;
            notificationPanel.BorderStyle = BorderStyle.FixedSingle;
            notificationPanel.Padding = new Padding(10);

            Label messageLabel = new Label();
            messageLabel.Text = "GRATULACJE! WYGRAŁEŚ 1MLN DOLARÓW!";
            messageLabel.Location = new Point(5, 20);
            messageLabel.AutoSize = true;
            notificationPanel.Controls.Add(messageLabel);

            Label messageLabel_2 = new Label();
            messageLabel_2.Text = "Czy chcesz wypłacić na konto?";
            messageLabel_2.Location = new Point(10, 40);
            messageLabel_2.AutoSize = true;
            notificationPanel.Controls.Add(messageLabel_2);

            Button btnAgree = new Button();
            btnAgree.Text = "Wypłać";
            btnAgree.Location = new Point(30, 70);
            btnAgree.Click += (s, args) => { HandleNotificationResponse(true); };
            btnAgree.AutoSize = true;
            notificationPanel.Controls.Add(btnAgree);

            Button btnDisagree = new Button();
            btnDisagree.Text = "Nie wypłacaj";
            btnDisagree.Location = new Point(130, 70);
            btnDisagree.Click += (s, args) => { HandleNotificationResponse(false); };
            btnDisagree.AutoSize = true;
            notificationPanel.Controls.Add(btnDisagree);

            this.Controls.Add(notificationPanel);
        }

        private void HandleNotificationResponse(bool isAgreed)
        {
            if (isAgreed)
            {
                totalPoints -= 50;
                scoreLabel.Text = "PUNKTY: " + totalPoints;
                checkBox3.Checked = false;
            }
            else
            {
                totalPoints += 100;
                scoreLabel.Text = "PUNKTY: " + totalPoints;
            }

            notificationPanel.Visible = false;
        }

        // Reset labiryntu
        private void ResetMaze()
        {
            player = new Player(MazeLevel.Easy.StartPoint.X, MazeLevel.Easy.StartPoint.Y);
            mazePanel.Visible = true;

            if (!isMazeCompleted)
            {
                elapsedTime = 0;
                timerLabel.Text = "CZAS: " + elapsedTime + " s";
                gameTimer.Start();
            }
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == checkBox1 && !checkBox1Checked)
            {
                checkBox1Checked = true;
                totalPoints += 100;
                scoreLabel.Text = "PUNKTY: " + totalPoints;

                StartDefenderTimer();
            }
            else if (sender == checkBox2 && !checkBox2Checked)
            {
                checkBox2Checked = true;
                totalPoints += 100;
                scoreLabel.Text = "PUNKTY: " + totalPoints;
            }
            else if (sender == checkBox3 && !checkBox3Checked)
            {
                checkBox3Checked = true;
                totalPoints += 100;
                scoreLabel.Text = "PUNKTY: " + totalPoints;
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Czy jesteś pewny? Gra nie zostanie zapisana!", "Wyjście do menu głównego.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void NotifyWindowsDefenderDisabled()
        {
            MessageBox.Show("Prawdopodobny atak, sprawdź wszystkie zabezpieczenia.", "Zabezpieczenie wyłączone", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void EndGame()
        {
            mazePanel.Visible = false;
            scorePanel.Visible = false;
            timePanel.Visible = false;
            optionsPanel.Visible = false;
            panelTitle.Visible = false;

            completionPanel = new System.Windows.Forms.Panel();
            completionPanel.Size = new Size(400, 170);
            completionPanel.Location = new Point((this.ClientSize.Width - completionPanel.Width) / 2, (this.ClientSize.Height - completionPanel.Height) / 2);
            completionPanel.BackColor = Color.Black;
            completionPanel.BorderStyle = BorderStyle.FixedSingle;
            completionPanel.Padding = new Padding(10);

            Label completionLabel = new Label();
            completionLabel.Text = "PRZESZEDŁEŚ POZIOM 1!";
            completionLabel.AutoSize = true;
            completionLabel.Location = new Point(80, 10);            
            completionLabel.BackColor = Color.Transparent;
            completionLabel.FlatStyle = FlatStyle.Flat;
            completionLabel.Font = new System.Drawing.Font("Snap ITC", 11);
            completionLabel.ForeColor = SystemColors.HighlightText;
            completionPanel.Controls.Add(completionLabel);

            Label completionLabel_2 = new Label();
            completionLabel_2.Text = "CO CHCESZ TERAZ ZROBIĆ?";
            completionLabel_2.AutoSize = true;
            completionLabel_2.Location = new Point(80, 50);
            completionLabel_2.BackColor = Color.Transparent;
            completionLabel_2.FlatStyle = FlatStyle.Flat;
            completionLabel_2.Font = new System.Drawing.Font("Snap ITC", 11);
            completionLabel_2.ForeColor = SystemColors.HighlightText;
            completionPanel.Controls.Add(completionLabel_2);

            Button btnContinue = new Button();
            btnContinue.Text = "START POZIOM 2";
            btnContinue.Location = new Point(10, 90);
            btnContinue.AutoSize = true;
            btnContinue.BackColor = SystemColors.WindowFrame;
            btnContinue.FlatAppearance.BorderColor = Color.Black;
            btnContinue.FlatAppearance.BorderSize = 1;
            btnContinue.FlatAppearance.MouseOverBackColor = Color.DarkGray;
            btnContinue.FlatStyle = FlatStyle.Flat;
            btnContinue.Font = new System.Drawing.Font("Snap ITC", 11);
            btnContinue.ForeColor = SystemColors.HighlightText;
            btnContinue.Click += (s, args) =>
            {
                Level_2 level_2 = new Level_2();
                OpenLevel(level_2);
            };
            
            completionPanel.Controls.Add(btnContinue);

            Button btnExit = new Button();
            btnExit.Text = "WRACAM DO MENU";
            btnExit.Location = new Point(190, 90);
            btnExit.AutoSize = true;
            btnExit.BackColor = SystemColors.WindowFrame;
            btnExit.FlatAppearance.BorderColor = Color.Black;
            btnExit.FlatAppearance.BorderSize = 1;
            btnExit.FlatAppearance.MouseOverBackColor = Color.DarkGray;
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.Font = new System.Drawing.Font("Snap ITC", 11);
            btnExit.ForeColor = SystemColors.HighlightText;
            btnExit.Click += (s, args) =>
            {
                Application.Exit();
            };
            
            completionPanel.Controls.Add(btnExit);

            this.Controls.Add(completionPanel);
        }
    }
}