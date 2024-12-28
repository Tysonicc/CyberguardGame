using System;
using System.Drawing;
using System.Windows.Forms;

namespace CyberguardGame
{
    public partial class Level_1 : Form
    {
        public Button btnBack;                                  /** Przycisk powrotu */
        
        public Panel optionsPanel;                              /** Panel z kontrolkami */
        public Panel scorePanel, timePanel;                     /** Panele z punktami i czasem */
        public Panel mazePanel;                                 /** Panel do wyświetlania labiryntu */
        public Panel responsePanel;                             /** Panel do wyświetlania wiadomości */
        public Panel emailPanel;                                /** Panel do wyświetlania maila */
        public Panel suspiciousLinkPanel;                       /** Panel do wyświetlania podejrzanego linku */
        public Panel emailPanel_2;                              /** Panel do wyświetlania drugiego maila */
        public Panel completionPanel;                           /** Panel informacyjny po ukończeniu poziomu */
        public Panel notificationPanel;                         /** Panel powiadomienia o wygranej */

        public Label scoreLabel, timerLabel;                    /** Etykieta do pokazywania czasu i wyniku punktowego */
        public Label panelTitle;                                /** Etykieta wyświetlająca zabezpieczenia serwera */

        public CheckBox checkBox1, checkBox2, checkBox3;        /** Kontrolki do zaznaczania */

        public Maze maze;                                       /** Obiekt labiryntu */
        public Player player;                                   /** Obiekt gracza */
        
        public const int cellWidth = 40;                        /** Szerokość komórki */
        public const int cellHeight = 40;                       /** Wysokość komórki */

        public int totalPoints = 0;                             /** Zmienna do przechowywania punktów */
        public int elapsedTime = 0;                             /** Licznik czasu w sekundach */

        public System.Windows.Forms.Timer gameTimer;            /** Timer do liczenia czasu */
        public System.Windows.Forms.Timer defenderTimer;        /** Timer do odznaczania Windows Defender */
        public System.Windows.Forms.Timer responseTimer;        /** Timer do odliczania czasu na odpowiedź */
        public System.Windows.Forms.Timer antiVirusTimer;       /** Timer do zarządzania kontrolką Antywirus */
        public System.Windows.Forms.Timer emailTimer;           /** Timer do wyświetlania maila */
        public System.Windows.Forms.Timer suspiciousLinkTimer;  /** Timer do wyświetlania panelu z podejrzanym linkiem */
        public System.Windows.Forms.Timer emailTimer_2;         /** Timer do wyświetlania maila */
        public System.Windows.Forms.Timer notificationTimer;    /** Timer do wyświetlania powiadomienia */

        public bool checkBox1Checked = false;                   /** Flaga dla checkboxa 1 */
        public bool checkBox2Checked = false;                   /** Flaga dla checkboxa 2 */
        public bool checkBox3Checked = false;                   /** Flaga dla checkboxa 3 */
        public bool isMazeCompleted = false;                    /** Flaga dla ukończenia labiryntu */
        public bool serverSecuredMessageShown = false;          /** Flaga dla wyświetlenia komunikatu o zabezpieczeniu serwera */
        public bool isResponsePanelShown = false;               /** Flaga dla wyświetlenia odpowiedzi */
        public bool isEmailPanelShown = false;                  /** Flaga dla wyświetlenia maila */
        public bool isSuspiciousLinkPanelShown = false;         /** Flaga dla wyświetlenia podejrzanego linku */
        public bool isEmailPanel2Shown = false;                 /** Flaga dla wyświetlenia drugiego maila */
        public bool isNotificationPanelShown = false;           /** Flaga dla wyświetlenia powiadomienia o wygranej */
        public bool isNotificationShown = false;                /** Flaga dla wyświetlenia powiadomienia o wygaśnięciu kontrolki */

        public Form activeLevel = new Form();

        /** Konstruktor formularzu Level_1 */
        public Level_1()
        {
            InitializeComponent();                              /** Inicjalizacja komponentów formularza */
            InitializeGame();                                   /** Inicjalizacja gry  */
            InitializeTimer();                                  /** Inicjalizacja timera */
        }

        /** Metoda, która służy do otwierania danego poziomu */
        public void OpenLevel(Form levelForm)
        {
            if (activeLevel != null)
            {
                activeLevel.Close();                            /** Zamknięcie aktualnego poziomu, jeżeli jest otwarty */
            }

            activeLevel = levelForm;                            /** Ustawienie nowego poziomu jako aktywnego */
            levelForm.TopLevel = false;                         /** Ustawienie poziomu jako podrzędnego */
            levelForm.FormBorderStyle = FormBorderStyle.None;   /** Usuwanie ramki */
            levelForm.Dock = DockStyle.Fill;                    /** Wypełnianie panelu */

            this.Controls.Add(levelForm);                       /** Dodawanie poziomu do formularza */
            levelForm.BringToFront();                           /** Przeniesienie poziomu na wierzch */
            levelForm.Show();                                   /** Pokazanie poziomu */
        }

        /** Metoda, która służy do inicjalizowania poziomu gry */
        public void InitializeGame()
        {
            /** Tworzenie nowego obiektu labiryntu na podstawie poziomu łatwego */
            maze = new Maze(MazeLevel.Easy.Width, MazeLevel.Easy.Height);
            maze.GenerateMaze(MazeLevel.Easy.Grid, MazeLevel.Easy.EndPoint);
            
            /** Ustawienie gracza w punkcie startowym */
            player = new Player(MazeLevel.Easy.StartPoint.X, MazeLevel.Easy.StartPoint.Y);

            /** Tworzenie panelu do wyświetlania labiryntu */
            mazePanel = new BufforedPanel();
            mazePanel.Size = new Size(MazeLevel.Easy.Width * cellWidth + 50,  MazeLevel.Easy.Height * cellHeight + 50);
            mazePanel.Location = new Point((this.ClientSize.Width - mazePanel.Width) / 2, (this.ClientSize.Height - mazePanel.Height) / 2);

            /** Ustawienie właściwości panelu labiryntu */
            mazePanel.BackColor = Color.LightSteelBlue;
            mazePanel.BorderStyle = BorderStyle.Fixed3D;
            mazePanel.Padding = new Padding(10);

            this.Controls.Add(mazePanel);

            /** Podpięcie zdarzenia malowania panelu labiryntu */
            mazePanel.Paint += new PaintEventHandler(MazePanel_Paint);

            this.KeyDown += new KeyEventHandler(Level_1_KeyDown);
            this.KeyPreview = true;

            /** Dodanie i wystylizowanie przycisku zakończenia poziomu */
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

            /** Dodanie i wystylizowanie panelu, który przechowuje punktacje */
            scorePanel = new Panel();
            scorePanel.Size = new System.Drawing.Size(200, 50);
            scorePanel.Location = new System.Drawing.Point(this.ClientSize.Width - scorePanel.Width - 20, 20);
            scorePanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            scorePanel.BackColor = SystemColors.WindowFrame;
            scorePanel.BorderStyle = BorderStyle.FixedSingle;
            scorePanel.Padding = new Padding(10);
            this.Controls.Add(scorePanel);

            /** Dodanie i wystylizowanie etykiety przechowywującej punkty  */
            scoreLabel = new Label();
            scoreLabel.Text = "PUNKTY: " + totalPoints;
            scoreLabel.Location = new System.Drawing.Point(10, 10);
            scoreLabel.AutoSize = true;
            scoreLabel.Font = new Font("Snap ITC", 11);
            scoreLabel.ForeColor = SystemColors.HighlightText;
            scorePanel.Controls.Add(scoreLabel);

            /** Dodanie i wystylizowanie panelu, który przechowuje timer */
            timePanel = new Panel();
            timePanel.Size = new System.Drawing.Size(200, 50);
            timePanel.Location = new System.Drawing.Point(this.ClientSize.Width - timePanel.Width - 20, 80);
            timePanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            timePanel.BackColor = SystemColors.WindowFrame;
            timePanel.BorderStyle = BorderStyle.FixedSingle;
            timePanel.Padding = new Padding(10);
            this.Controls.Add(timePanel);

            /** Dodanie i wystylizowanie etykiety przechowywującej timer */
            timerLabel = new Label();
            timerLabel.Text = "CZAS: 0 s";
            timerLabel.Location = new System.Drawing.Point(10, 10);
            timerLabel.AutoSize = true;
            timerLabel.Font = new Font("Snap ITC", 11);
            timerLabel.ForeColor = SystemColors.HighlightText;
            timePanel.Controls.Add(timerLabel);
        }

        /** Metoda, która służy do inicjalizacji timera */
        public void InitializeTimer()
        {
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1000;                          /** Ustawienie interwału timera na 1 sekundę */
            gameTimer.Tick += GameTimer_Tick;                   /** Podpięcie zdarzenia tick do metody obsługującej */
            gameTimer.Start();                                  /** Rozpoczęcie odliczania timera */
        }

        /** Metoda, która służy do uruchomienia metody tick timera */
        public void GameTimer_Tick(object sender, EventArgs e)
        {
            elapsedTime++;                                      /** Zwiększanie czasu gry o 1 sekundę */
            timerLabel.Text = "CZAS: " + elapsedTime + " s";    /** Aktualizacja etykiety czasu */

            /** Jeżeli czas gry osiągnął 60 sekund -> zakończ grę */
            if (elapsedTime >= 60)
            {
                EndGame();
            }
        }

        /** Metoda odpowiedzialna za malowanie panelu labiryntu */
        public void MazePanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;                                                        /** Uzyskanie obiektu Graphics do rysowania na panelu */
        
            int margin = 10;                                                                /** Ustawienie marginesu dla rysowania */
            int offsetX = (mazePanel.Width - (maze.Width * cellWidth)) / 2;                 /** Wyśrodkowanie labiryntu w poziomie */
            int offsetY = (mazePanel.Height - (maze.Height * cellHeight)) / 2;              /** Wyśrodkowanie labiryntu w pionie */

            for (int y = 0; y < maze.Height; y++)
            {
                for (int x = 0; x < maze.Width; x++)
                {
                    /** Sprawdzenie, czy jest to pozycja gracza */
                    if (x == player.X && y == player.Y)
                    {
                        /** Ustawienie tła na ciemno-szare dla pola, na którym stoi gracz */
                        g.FillRectangle(Brushes.ForestGreen, offsetX + x * cellWidth, offsetY + y * cellHeight, cellWidth, cellHeight);
                    }
                    else
                    {
                        /** Ustawienie tła na czarne dla pozostałych pól */
                        g.FillRectangle(Brushes.Black, offsetX + x * cellWidth, offsetY + y * cellHeight, cellWidth, cellHeight);
                    }

                    string text = maze.Grid[y, x] == 1 ? "1" : "0";                         /** Ustawienie tekstu na "1" dla ściany, "0" dla przestrzeni */

                    Font font = new Font("Arial", 16);
                    Brush textBrush = Brushes.Green;
                    g.DrawString(text, font, textBrush, offsetX + x * cellWidth + cellWidth / 4, offsetY + y * cellHeight + cellHeight / 4);
                }
            }

            Font playerFont = new Font("Arial", 16);
            Brush playerBrush = Brushes.Black;
            g.DrawString("0", playerFont, playerBrush, offsetX + player.X * cellWidth + cellWidth / 4, offsetY + player.Y * cellHeight + cellHeight / 4);

            /** Sprawdzenie, czy wiadomość o zabezpieczeniu serwera została już wyświetlona */
            if (!serverSecuredMessageShown)
            {                                                
                MessageBox.Show("Zabezpiecz serwer!", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                serverSecuredMessageShown = true; 
            }
        }

        /** Metoda, która służy do obsługi zdarzeń naciśnięcia danego klawisza oraz do rozpoczęcia innych metod */
        public void Level_1_KeyDown(object sender, KeyEventArgs e)
        {
            /** Jeżeli czas gry osiągnął 60 sekund -> zakończ grę */
            if (elapsedTime >= 60)
            {
                EndGame();
            }
            
            player.Move(e.KeyCode, maze);                                               /** Przekazanie obiektu maze do metody Move */
            mazePanel.Invalidate();                                                     /** Odświeżenie panelu */

            if (player.X == maze.EndPoint.X && player.Y == maze.EndPoint.Y)             /** Sprawdzenie, czy gracz osiągnął punkt końcowy */
            {            
                totalPoints += 100; 
                scoreLabel.Text = "PUNKTY: " + totalPoints;
                mazePanel.Visible = false;                                              /** Ukrycie panelu z labiryntem po jego ukończeniu */
                
                ShowOptionsPanel();                                                     /** Pokazanie panelu z kontrolkami */ 

                isMazeCompleted = true;

                if (!isResponsePanelShown)                                              /** Jeśli panel z odpowiedzią nie był wyświetlony -> uruchom timer */
                {
                    StartResponseTimer();
                    isResponsePanelShown = true;
                }

                if (!isEmailPanelShown)                                                 /** Jeśli panel z mailem nie był wyświetlony -> uruchom timer */
                {
                    StartEmailTimer();
                    isEmailPanelShown = true;
                }

                if (!isSuspiciousLinkPanelShown)                                        /** Jeśli panel z podejrzanym linkiem nie był wyświetlony -> uruchom timer */
                {
                    StartSuspiciousLinkTimer();
                    isSuspiciousLinkPanelShown = true;
                }

                if (!isEmailPanel2Shown)                                                /** Jeśli panel z drugim mailem nie był wyświetlony -> uruchom timer */
                {
                    StartEmailTimer_2();
                    isEmailPanel2Shown = true;
                }

                if (!isNotificationPanelShown)                                          /** Jeśli panel z powiadomieniem o wygranej nie był wyświetlony -> uruchom timer */
                {
                    StartNotificationTimer();
                    isNotificationPanelShown = true;
                }
            }
        }

        /** Metoda, która inicjalizuje nowy timer do obsługi czasu dla odpowiedzi */
        public void StartResponseTimer()
        {
            responseTimer = new System.Windows.Forms.Timer();
            responseTimer.Interval = 8000;                                              /** Ustawienie interwału timera na 5 sekund */
            responseTimer.Tick += ResponseTimer_Tick;                                   /** Podpięcie zdarzenia tick do metody obsługującej */
            responseTimer.Start();                                                      /** Rozpoczęcie odliczania timera */
        }

        /** Metoda, która jest wywoływana, gdy timer osiągnie ustawiony interwał -> pokaż panel z odpowiedzią */
        public void ResponseTimer_Tick(object sender, EventArgs e)
        { 
            responseTimer.Stop();                                                       /** Zatrzymanie timera */
            ShowResponsePanel();                                                        /** Wyświetlanie metody z wyświetlaniem panelu */
        }

        /** Metoda służąca do wyświetlenia i wystylizowania panelu odpowiedzi */
        public void ShowResponsePanel()
        {
            /** Dodanie i wystylizowanie panelu z odpowiedzią */
            responsePanel = new Panel();
            responsePanel.Size = new System.Drawing.Size(250, 150);
            int leftMargin = 20;
            int bottomMargin = 20;
            responsePanel.Location = new System.Drawing.Point(leftMargin, this.ClientSize.Height - responsePanel.Height - bottomMargin);
            responsePanel.BackColor = Color.LightBlue;
            responsePanel.BorderStyle = BorderStyle.FixedSingle;
            responsePanel.Padding = new Padding(10);

            /** Dodanie etykiet do odpowiedzi */ 
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

            /** Dodanie i wystylizowanie przycisków wyboru */
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

        /** Metoda służąca do obsługi odpowiedzi przez użytkownika */
        public void HandleResponse(bool isAgreed)
        {
            if (isAgreed)                                           /** Jeśli użytkownik się zgodzi */
            {
                checkBox3.Checked = false;                          /** Odznaczenie kontrolki Antywirusa */
                totalPoints -= 50;                                  /** Odejmowanie punktów z głównego licznika*/
                StartAntiVirusTimer();                              /** Włączenie timera dotyczącego Antywirusa */
            }
            else
            {
                totalPoints += 100;                                 /** Dodawanie punktów do głównego licznika */
            }

            scoreLabel.Text = "PUNKTY: " + totalPoints;             /** Aktualizacja etykiety z punktami */

            this.Controls.Remove(responsePanel);                    /** Wyłączenie panelu z odpowiedzią */
        }

        /** Metoda, która inicjalizuje nowy timer do obsługi czasu dla maila */
        public void StartEmailTimer()
        {
            emailTimer = new System.Windows.Forms.Timer();
            emailTimer.Interval = 15000;                                                /** Ustawienie interwału timera na 10 sekund */
            emailTimer.Tick += EmailTimer_Tick;                                         /** Podpięcie zdarzenia tick do metody obsługującej */
            emailTimer.Start();                                                         /** Rozpoczęcie odliczania timera */
        }

        /** Metoda, która jest wywoływana, gdy timer osiągnie ustawiony interwał -> pokaż panel z mailem */
        public void EmailTimer_Tick(object sender, EventArgs e)
        {
            emailTimer.Stop();                                                          /** Zatrzymanie timera */
            ShowEmailPanel();                                                           /** Wyświetlanie metody z wyświetlaniem panelu */
        }

        /** Metoda służąca do wyświetlenia i wystylizowania panelu maila */
        public void ShowEmailPanel()
        {
            /** Dodanie i wystylizowanie panelu z mailem */
            emailPanel = new Panel();
            emailPanel.Size = new System.Drawing.Size(250, 150);
            emailPanel.Location = new System.Drawing.Point((this.ClientSize.Width - emailPanel.Width) / 2, (this.ClientSize.Height - emailPanel.Height) / 2);
            emailPanel.BackColor = Color.LightGray;
            emailPanel.BorderStyle = BorderStyle.FixedSingle;
            emailPanel.Padding = new Padding(10);

            /** Dodanie etykiet do maila */
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

            /** Dodanie i wystylizowanie przycisków wyboru */
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

        /** Metoda służąca do obsługi odpowiedzi przez użytkownika */
        public void HandleEmailResponse(bool isAgreed)
        {
            if (isAgreed)                                           /** Jeśli użytkownik się zgodzi */
            {
                totalPoints += 100;                                 /** Dodawanie punktów do głównego licznika */
            }
            else
            {
                totalPoints -= 50;                                  /** Odejmowanie punktów z głównego licznika*/
            }

            scoreLabel.Text = "PUNKTY: " + totalPoints;             /** Aktualizacja etykiety z punktami */
            this.Controls.Remove(emailPanel);                       /** Wyłączenie panelu z mailem */
        }

        /** Metoda, która inicjalizuje nowy timer do obsługi czasu dla podejrzanego linku */
        public void StartSuspiciousLinkTimer()
        {
            suspiciousLinkTimer = new System.Windows.Forms.Timer();
            suspiciousLinkTimer.Interval = 20000;
            suspiciousLinkTimer.Tick += SuspiciousLinkTimer_Tick;
            suspiciousLinkTimer.Start();
        }

        /** Metoda, która jest wywoływana, gdy timer osiągnie ustawiony interwał -> pokaż panel z podejrzanym linkiem */
        public void SuspiciousLinkTimer_Tick(object sender, EventArgs e)
        {
            suspiciousLinkTimer.Stop();
            ShowSuspiciousLinkPanel();
        }

        /** Metoda służąca do wyświetlenia i wystylizowania panelu z podejrzanym linkiem */
        public void ShowSuspiciousLinkPanel()
        {
            /** Dodanie i wystylizowanie panelu z podejrzanym linkiem */
            suspiciousLinkPanel = new Panel();
            suspiciousLinkPanel.Size = new System.Drawing.Size(250, 60);
            suspiciousLinkPanel.Location = new System.Drawing.Point(this.ClientSize.Width - suspiciousLinkPanel.Width - 20, (this.ClientSize.Height - suspiciousLinkPanel.Height) / 2);
            suspiciousLinkPanel.BackColor = Color.LightGray;
            suspiciousLinkPanel.BorderStyle = BorderStyle.FixedSingle;
            suspiciousLinkPanel.Padding = new Padding(10);

            /** Dodanie etykiety do panelu */
            Label linkLabel = new Label();
            linkLabel.Text = "www.tanieauta-Polska.com";
            linkLabel.Location = new Point(30, 10);
            linkLabel.AutoSize = true;
            suspiciousLinkPanel.Controls.Add(linkLabel);

            /** Dodanie i wystylizowanie przycisków wyboru */
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

        /** Metoda służąca do obsługi odpowiedzi przez użytkownika */
        public void HandleSuspiciousLinkResponse(bool isAgreed)
        {
            if (isAgreed)
            {
                totalPoints -= 50;
                scoreLabel.Text = "PUNKTY: " + totalPoints;
                checkBox3.Checked = false;

                for (int i = 1; i <= 10; i++)
                {
                    MessageBox.Show("Uwaga! Sprawdź zabezpieczenia!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                totalPoints += 100;
                scoreLabel.Text = "PUNKTY: " + totalPoints;
            }

            suspiciousLinkPanel.Visible = false;
        }

        /** Metoda, która inicjalizuje nowy timer do obsługi czasu dla drugiego maila */
        public void StartEmailTimer_2()
        {
            emailTimer_2 = new System.Windows.Forms.Timer();
            emailTimer_2.Interval = 28000;
            emailTimer_2.Tick += EmailTimer_Tick_2;
            emailTimer_2.Start();
        }

        /** Metoda, która jest wywoływana, gdy timer osiągnie ustawiony interwał -> pokaż panel z drugim mailem */
        public void EmailTimer_Tick_2(object sender, EventArgs e)
        {
            emailTimer_2.Stop();
            ShowEmailPanel_2();
        }

        /** Metoda służąca do wyświetlenia i wystylizowania panelu drugiego maila */
        public void ShowEmailPanel_2()
        {
            /** Dodanie i wystylizowanie panelu z drugim mailem */
            emailPanel_2 = new System.Windows.Forms.Panel();
            emailPanel_2.Size = new System.Drawing.Size(250, 150);
            emailPanel_2.Location = new System.Drawing.Point((this.ClientSize.Width - emailPanel_2.Width) / 2 + 200, (this.ClientSize.Height - emailPanel_2.Height) / 2 + 250);
            emailPanel_2.BackColor = Color.LightBlue;
            emailPanel_2.BorderStyle = BorderStyle.FixedSingle;
            emailPanel_2.Padding = new Padding(10);

            /** Dodanie etykiet do drugiego maila */
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

            /** Dodanie i wystylizowanie przycisków wyboru */
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

        /** Metoda służąca do obsługi odpowiedzi przez użytkownika */
        public void HandleEmailResponse_2(bool isAgreed)
        {
            if (isAgreed)
            {
                totalPoints -= 50;
                scoreLabel.Text = "PUNKTY: " + totalPoints;
                checkBox2.Checked = false;

                ResetMaze();                                        /** Resetowanie labiryntu */
            }
            else
            {
                totalPoints += 100;
                scoreLabel.Text = "PUNKTY: " + totalPoints;
            }

            emailPanel_2.Visible = false;
        }

        /** Metoda, która inicjalizuje nowy timer do obsługi czasu dla powiadomienia o wygranej */
        public void StartNotificationTimer()
        {
            notificationTimer = new System.Windows.Forms.Timer();
            notificationTimer.Interval = 32000;
            notificationTimer.Tick += NotificationTimer_Tick;
            notificationTimer.Start();
        }

        /** Metoda, która jest wywoływana, gdy timer osiągnie ustawiony interwał -> pokaż panel z powiadomieniem o wygranej */
        public void NotificationTimer_Tick(object sender, EventArgs e)
        {
            notificationTimer.Stop();
            ShowNotificationPanel();
        }

        /** Metoda służąca do wyświetlenia i wystylizowania panelu z powiadomieniem o wygranej */
        public void ShowNotificationPanel()
        {
            /** Dodanie i wystylizowanie panelu z powiadomieniem o wygranej */
            notificationPanel = new Panel();
            notificationPanel.Size = new System.Drawing.Size(250, 100);
            notificationPanel.Location = new System.Drawing.Point((this.ClientSize.Width - notificationPanel.Width) / 2, 20);
            notificationPanel.BackColor = Color.LightBlue;
            notificationPanel.BorderStyle = BorderStyle.FixedSingle;
            notificationPanel.Padding = new Padding(10);

            /** Dodanie etykiet do panelu z powiadomieniem o wygranej */
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

            /** Dodanie i wystylizowanie przycisków wyboru */
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

        /** Metoda służąca do obsługi odpowiedzi przez użytkownika */
        public void HandleNotificationResponse(bool isAgreed)
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

        /** Metoda, która inicjalizuje nowy timer do obsługi czasu dla znikającej kontrolki Antywirusa */
        public void StartAntiVirusTimer()
        {
            antiVirusTimer = new System.Windows.Forms.Timer();
            antiVirusTimer.Interval = 14000;
            antiVirusTimer.Tick += AntiVirusTimer_Tick;
            antiVirusTimer.Start();
        }

        /** Metoda, która jest wywoływana, gdy timer osiągnie ustawiony interwał */
        public void AntiVirusTimer_Tick(object sender, EventArgs e)
        {
            antiVirusTimer.Stop();

         
            if (!checkBox3.Checked)
            {
              
                totalPoints -= 50;
            }
            else
            {
                
                totalPoints += 200;
            }

            scoreLabel.Text = "PUNKTY: " + totalPoints;
        }

        /** Metoda, która pokazuje panel z zabezpieczeniami serwera */
        public void ShowOptionsPanel()
        {
            /** Dodanie i wystylizowanie panelu z kontrolkami do zaznaczania */
            optionsPanel = new Panel();
            optionsPanel.Size = new System.Drawing.Size(200, 150);
            optionsPanel.Location = new System.Drawing.Point(20, 20);
            optionsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            optionsPanel.BackColor = Color.LightGray;
            optionsPanel.BorderStyle = BorderStyle.Fixed3D;
            optionsPanel.Padding = new Padding(10);

            /** Dodanie etykiety do panelu */
            panelTitle = new Label();
            panelTitle.Text = "ZABEZPIECZENIA SERWERA";
            panelTitle.Font = new Font("Tahoma", 8, FontStyle.Bold);
            panelTitle.ForeColor = Color.Black;
            panelTitle.Location = new Point(10, 10);
            panelTitle.AutoSize = true;

            optionsPanel.Controls.Add(panelTitle);

            /** Dodanie i wystylizowanie checkboxów do panelu */
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

        /** Metoda, która inicjalizuje nowy timer do obsługi czasu dla znikającej kontrolki Windows Defender */
        public void StartDefenderTimer()
        {
            if (defenderTimer != null)
            {
                defenderTimer.Stop();
            }

            defenderTimer = new System.Windows.Forms.Timer();
            defenderTimer.Interval = 18000;
            defenderTimer.Tick += DefenderTimer_Tick;
            defenderTimer.Start();
        }

        /** Metoda, która jest wywoływana, gdy timer osiągnie ustawiony interwał*/
        public void DefenderTimer_Tick(object sender, EventArgs e)
        {
            defenderTimer.Stop();

            checkBox1.Checked = false;
            checkBox1Checked = false;
            totalPoints -= 50;
            scoreLabel.Text = "PUNKTY: " + totalPoints;

            NotifyWindowsDefenderDisabled();
        }

        

        /** Metoda, która służy do zresetowania labiryntu -> pokaż labirynt jeszcze raz */
        public void ResetMaze()
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

        /** Metoda, która służy do sprawdzania poprawności zaznaczenia kontrolek zabezpieczeń */
        public void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (sender == checkBox1 && !checkBox1Checked)
            {
                checkBox1Checked = true;
                totalPoints += 200;
                scoreLabel.Text = "PUNKTY: " + totalPoints;

                StartDefenderTimer();
            }
            else if (sender == checkBox2 && !checkBox2Checked)
            {
                checkBox2Checked = true;
                totalPoints += 200;
                scoreLabel.Text = "PUNKTY: " + totalPoints;
            }
            else if (sender == checkBox3 && !checkBox3Checked)
            {
                checkBox3Checked = true;
                totalPoints += 200;
                scoreLabel.Text = "PUNKTY: " + totalPoints;
            }
        }

        /** Metoda służąca do wyjścia do menu głównego */
        public void BtnBack_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Czy jesteś pewny? Gra nie zostanie zapisana!", "Wyjście do menu głównego.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        /** Metoda, która służy dla gracza do przypomnienia o pilnowaniu zaznaczania kontrolek */
        public void NotifyWindowsDefenderDisabled()
        {
            if (!isNotificationShown)
            {
                MessageBox.Show("Prawdopodobny atak, sprawdź wszystkie zabezpieczenia!", "Zabezpieczenie wyłączone!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                isNotificationShown = true;
            }
            
        }

        /** Metoda, która kończy rozgrywkę */
        public void EndGame()
        {
            /** Wyłączenie wszystkich kontrolek */
            mazePanel.Visible = false;
            scorePanel.Visible = false;
            timePanel.Visible = false;
            optionsPanel.Visible = false;
            panelTitle.Visible = false;
            notificationPanel.Visible = false;

            foreach (Control control in this.Controls)
            {
                if (control is Panel && control != completionPanel)
                {
                    control.Visible = false;
                }
            }

            /** Dodanie nowego panelu */
            completionPanel = new Panel();
            completionPanel.Size = new Size(400, 170);
            completionPanel.Location = new Point((this.ClientSize.Width - completionPanel.Width) / 2, (this.ClientSize.Height - completionPanel.Height) / 2);
            completionPanel.BackColor = Color.Black;
            completionPanel.BorderStyle = BorderStyle.FixedSingle;
            completionPanel.Padding = new Padding(10);

            /** Dodanie etykiet */
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

            /** Dodanie przycisków wyboru */
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
            btnExit.Text = "WYJDŹ DO MENU";
            btnExit.Location = new Point(200, 90);
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
                this.Close();
            };
            
            completionPanel.Controls.Add(btnExit);

            this.Controls.Add(completionPanel);
        }
    }
}