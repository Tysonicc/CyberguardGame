using System;
using System.Drawing;
using System.Windows.Forms;

namespace CyberguardGame
{
    public partial class Level_3 : Form
    {
        /** Przycisk powrotu */
        private Button btnBack;

        /** Panel z kontrolkami */
        private Panel optionsPanel;
        /** Panele z punktami i czasem */
        private Panel scorePanel, timePanel;
        /** Panel do wyswietlania labiryntu */
        private Panel mazePanel;
        /** Panel do wyswietlania wiadomosci */
        private Panel responsePanel;
        /** Panel do wyswietlania drugiej wiadomosci */
        private Panel responsePanel_2;
        /** Panel do wyswietlania maila */
        private Panel emailPanel;
        /** Panel do wyswietlania podejrzanego linku */
        private Panel suspiciousLinkPanel;
        /** Panel do wyswietlania drugiego podejrzanego linku */
        private Panel suspiciousLinkPanel_2;
        /** Panel do wyswietlania drugiego maila */
        private Panel emailPanel_2;
        /** Panel do wyswietlania trzeciego maila */
        private Panel emailPanel_3;
        /** Panel informacyjny po ukończeniu poziomu */
        private Panel completionPanel;
        /** Panel powiadomienia o wygranej */
        private Panel notificationPanel;
        /** Panel powiadomienia o drugiej wygranej */
        private Panel notificationPanel_2;

        /** Etykieta do pokazywania czasu i wyniku punktowego */
        private Label scoreLabel, timerLabel;
        /** Etykieta wyswietlajaca zabezpieczenia serwera */
        private Label panelTitle;
        /** Kontrolki do zaznaczania */
        private CheckBox checkBox1, checkBox2, checkBox3;

        /** Obiekt labiryntu */
        private Maze maze;
        /** Obiekt gracza */
        private Player player;

        /** Szerokosz komorki */
        private const int cellWidth = 40;
        /** Wysokosz komorki */
        private const int cellHeight = 40;

        /** Zmienna do przechowywania punktow */
        private int totalPoints = 0;
        /** Licznik czasu w sekundach */
        private int elapsedTime = 0;

        /** Timer do liczenia czasu */
        private System.Windows.Forms.Timer gameTimer;
        /** Timer do odznaczania Windows Defender */
        private System.Windows.Forms.Timer defenderTimer;
        /** Timer do odliczania czasu na odpowiedz */
        private System.Windows.Forms.Timer responseTimer;
        /** Timer do odliczania czasu na drugą odpowiedz */
        private System.Windows.Forms.Timer responseTimer_2;
        /** Timer do zarzadzania kontrolka Antywirus */
        private System.Windows.Forms.Timer antiVirusTimer;
        /** Timer do wyswietlania maila */
        private System.Windows.Forms.Timer emailTimer;
        /** Timer do wyswietlania panelu z podejrzanym linkiem */
        private System.Windows.Forms.Timer suspiciousLinkTimer;
        /** Timer do wyswietlania panelu z drugim podejrzanym linkiem */
        private System.Windows.Forms.Timer suspiciousLinkTimer_2;
        /** Timer do wyswietlania maila */
        private System.Windows.Forms.Timer emailTimer_2;
        /** Timer do wyswietlania maila */
        private System.Windows.Forms.Timer emailTimer_3;
        /** Timer do wyswietlania powiadomienia */
        private System.Windows.Forms.Timer notificationTimer;
        /** Timer do wyswietlania drugiego powiadomienia */
        private System.Windows.Forms.Timer notificationTimer_2;

        /** Flaga dla checkboxa 1 */
        private bool checkBox1Checked = false;
        /** Flaga dla checkboxa 2 */
        private bool checkBox2Checked = false;
        /** Flaga dla checkboxa 3 */
        private bool checkBox3Checked = false;
        /** Flaga dla ukończenia labiryntu */
        private bool isMazeCompleted = false;
        /** Flaga dla wyswietlenia komunikatu o zabezpieczeniu serwera */
        private bool serverSecuredMessageShown = false;
        /** Flaga dla wyswietlenia odpowiedzi */
        private bool isResponsePanelShown = false;
        /** Flaga dla wyswietlenia drugiej odpowiedzi */
        private bool isResponsePanel2Shown = false;
        /** Flaga dla wyswietlenia maila */
        private bool isEmailPanelShown = false;
        /** Flaga dla wyswietlenia podejrzanego linku */
        private bool isSuspiciousLinkPanelShown = false;
        /** Flaga dla wyswietlenia drugiego podejrzanego linku */
        private bool isSuspiciousLinkPanelShown_2 = false;
        /** Flaga dla wyswietlenia drugiego maila */
        private bool isEmailPanel2Shown = false;
        /** Flaga dla wyswietlenia trzeciego maila */
        private bool isEmailPanel3Shown = false;
        /** Flaga dla wyswietlenia powiadomienia o wygranej */
        private bool isNotificationPanelShown = false;
        /** Flaga dla wyswietlenia powiadomienia o drugiej wygranej */
        private bool isNotificationPanel2Shown = false;
        /** Flaga dla wyswietlenia powiadomienia o wygasnieciu kontrolki */
        private bool isNotificationShown = false;

        private Form activeLevel = new Form();

        /** Konstruktor formularzu Level_3 */
        public Level_3()
        {
            InitializeComponent();                              /** Inicjalizacja komponentow formularza */
            InitializeGame();                                   /** Inicjalizacja gry  */
            InitializeTimer();                                  /** Inicjalizacja timera */
        }

        /** Metoda, ktora sluzy do otwierania danego poziomu */
        private void OpenLevel(Form levelForm)
        {
            if (activeLevel != null)
            {
                activeLevel.Close();                            /** Zamkniecie aktualnego poziomu, jezeli jest otwarty */
            }

            activeLevel = levelForm;                            /** Ustawienie nowego poziomu jako aktywnego */
            levelForm.TopLevel = false;                         /** Ustawienie poziomu jako podrzednego */
            levelForm.FormBorderStyle = FormBorderStyle.None;   /** Usuwanie ramki */
            levelForm.Dock = DockStyle.Fill;                    /** Wypelnianie panelu */

            this.Controls.Add(levelForm);                       /** Dodawanie poziomu do formularza */
            levelForm.BringToFront();                           /** Przeniesienie poziomu na wierzch */
            levelForm.Show();                                   /** Pokazanie poziomu */
        }

        /** Metoda, ktora sluzy do inicjalizowania poziomu gry */
        private void InitializeGame()
        {
            /** Tworzenie nowego obiektu labiryntu na podstawie poziomu latwego */
            maze = new Maze(MazeLevel.Hard.Width, MazeLevel.Hard.Height);
            maze.GenerateMaze(MazeLevel.Hard.Grid, MazeLevel.Hard.EndPoint);

            /** Ustawienie gracza w punkcie startowym */
            player = new Player(MazeLevel.Hard.StartPoint.X, MazeLevel.Hard.StartPoint.Y);

            /** Tworzenie panelu do wyswietlania labiryntu */
            mazePanel = new BufforedPanel();
            mazePanel.Size = new Size(MazeLevel.Hard.Width * cellWidth + 50, MazeLevel.Hard.Height * cellHeight + 50);
            mazePanel.Location = new Point((this.ClientSize.Width - mazePanel.Width) / 2, (this.ClientSize.Height - mazePanel.Height) / 2);

            /** Ustawienie wlasciwosci panelu labiryntu */
            mazePanel.BackColor = Color.LightSteelBlue;
            mazePanel.BorderStyle = BorderStyle.Fixed3D;
            mazePanel.Padding = new Padding(10);

            this.Controls.Add(mazePanel);

            /** Podpiecie zdarzenia malowania panelu labiryntu */
            mazePanel.Paint += new PaintEventHandler(MazePanel_Paint);

            this.KeyDown += new KeyEventHandler(Level_3_KeyDown);
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

            /** Dodanie i wystylizowanie panelu, ktory przechowuje punktacje */
            scorePanel = new Panel();
            scorePanel.Size = new System.Drawing.Size(200, 50);
            scorePanel.Location = new System.Drawing.Point(this.ClientSize.Width - scorePanel.Width - 20, 20);
            scorePanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            scorePanel.BackColor = SystemColors.WindowFrame;
            scorePanel.BorderStyle = BorderStyle.FixedSingle;
            scorePanel.Padding = new Padding(10);
            this.Controls.Add(scorePanel);

            /** Dodanie i wystylizowanie etykiety przechowywujacej punkty  */
            scoreLabel = new Label();
            scoreLabel.Text = "PUNKTY: " + totalPoints;
            scoreLabel.Location = new System.Drawing.Point(10, 10);
            scoreLabel.AutoSize = true;
            scoreLabel.Font = new Font("Snap ITC", 11);
            scoreLabel.ForeColor = SystemColors.HighlightText;
            scorePanel.Controls.Add(scoreLabel);

            /** Dodanie i wystylizowanie panelu, ktory przechowuje timer */
            timePanel = new Panel();
            timePanel.Size = new System.Drawing.Size(200, 50);
            timePanel.Location = new System.Drawing.Point(this.ClientSize.Width - timePanel.Width - 20, 80);
            timePanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            timePanel.BackColor = SystemColors.WindowFrame;
            timePanel.BorderStyle = BorderStyle.FixedSingle;
            timePanel.Padding = new Padding(10);
            this.Controls.Add(timePanel);

            /** Dodanie i wystylizowanie etykiety przechowywujacej timer */
            timerLabel = new Label();
            timerLabel.Text = "CZAS: 0 s";
            timerLabel.Location = new System.Drawing.Point(10, 10);
            timerLabel.AutoSize = true;
            timerLabel.Font = new Font("Snap ITC", 11);
            timerLabel.ForeColor = SystemColors.HighlightText;
            timePanel.Controls.Add(timerLabel);
        }

        /** Metoda, ktora sluzy do inicjalizacji timera */
        private void InitializeTimer()
        {
            gameTimer = new System.Windows.Forms.Timer();
            gameTimer.Interval = 1000;                          /** Ustawienie interwalu timera na 1 sekunde */
            gameTimer.Tick += GameTimer_Tick;                   /** Podpiecie zdarzenia tick do metody obslugujacej */
            gameTimer.Start();                                  /** Rozpoczecie odliczania timera */
        }

        /** Metoda, ktora sluzy do uruchomienia metody tick timera */
        private void GameTimer_Tick(object sender, EventArgs e)
        {
            elapsedTime++;                                      /** Zwiekszanie czasu gry o 1 sekunde */
            timerLabel.Text = "CZAS: " + elapsedTime + " s";    /** Aktualizacja etykiety czasu */

            /** Jezeli czas gry osiagnal 60 sekund -> zakończ gre */
            if (elapsedTime >= 60)
            {
                EndGame();
            }
        }

        /** Metoda odpowiedzialna za malowanie panelu labiryntu */
        private void MazePanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;                                                        /** Uzyskanie obiektu Graphics do rysowania na panelu */

            int margin = 10;                                                                /** Ustawienie marginesu dla rysowania */
            int offsetX = (mazePanel.Width - (maze.Width * cellWidth)) / 2;                 /** Wysrodkowanie labiryntu w poziomie */
            int offsetY = (mazePanel.Height - (maze.Height * cellHeight)) / 2;              /** Wysrodkowanie labiryntu w pionie */

            for (int y = 0; y < maze.Height; y++)
            {
                for (int x = 0; x < maze.Width; x++)
                {
                    /** Sprawdzenie, czy jest to pozycja gracza */
                    if (x == player.X && y == player.Y)
                    {
                        /** Ustawienie tla na ciemno-szare dla pola, na ktorym stoi gracz */
                        g.FillRectangle(Brushes.ForestGreen, offsetX + x * cellWidth, offsetY + y * cellHeight, cellWidth, cellHeight);
                    }
                    else
                    {
                        /** Ustawienie tla na czarne dla pozostalych pol */
                        g.FillRectangle(Brushes.Black, offsetX + x * cellWidth, offsetY + y * cellHeight, cellWidth, cellHeight);
                    }

                    string text = maze.Grid[y, x] == 1 ? "1" : "0";                         /** Ustawienie tekstu na "1" dla sciany, "0" dla przestrzeni */

                    Font font = new Font("Arial", 16);
                    Brush textBrush = Brushes.Green;
                    g.DrawString(text, font, textBrush, offsetX + x * cellWidth + cellWidth / 4, offsetY + y * cellHeight + cellHeight / 4);
                }
            }

            Font playerFont = new Font("Arial", 16);
            Brush playerBrush = Brushes.Black;
            g.DrawString("0", playerFont, playerBrush, offsetX + player.X * cellWidth + cellWidth / 4, offsetY + player.Y * cellHeight + cellHeight / 4);

            /** Sprawdzenie, czy wiadomosz o zabezpieczeniu serwera zostala juz wyswietlona */
            if (!serverSecuredMessageShown)
            {
                MessageBox.Show("Zabezpiecz serwer!", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);
                serverSecuredMessageShown = true;
            }
        }

        /** Metoda, ktora sluzy do obslugi zdarzeń nacisniecia danego klawisza oraz do rozpoczecia innych metod */
        private void Level_3_KeyDown(object sender, KeyEventArgs e)
        {
            /** Jezeli czas gry osiagnal 60 sekund -> zakończ gre */
            if (elapsedTime >= 60)
            {
                EndGame();
            }

            player.Move(e.KeyCode, maze);                                               /** Przekazanie obiektu maze do metody Move */
            mazePanel.Invalidate();                                                     /** Odswiezenie panelu */

            if (player.X == maze.EndPoint.X && player.Y == maze.EndPoint.Y)             /** Sprawdzenie, czy gracz osiagnal punkt końcowy */
            {
                totalPoints += 100;
                scoreLabel.Text = "PUNKTY: " + totalPoints;
                mazePanel.Visible = false;                                              /** Ukrycie panelu z labiryntem po jego ukończeniu */

                ShowOptionsPanel();                                                     /** Pokazanie panelu z kontrolkami */

                isMazeCompleted = true;

                if (!isResponsePanelShown)                                              /** Jesli panel z odpowiedzia nie byl wyswietlony -> uruchom timer */
                {
                    StartResponseTimer();
                    isResponsePanelShown = true;
                }

                if (!isResponsePanel2Shown)                                              /** Jesli panel z drugą odpowiedzia nie byl wyswietlony -> uruchom timer */
                {
                    StartResponseTimer_2();
                    isResponsePanel2Shown = true;
                }

                if (!isEmailPanelShown)                                                 /** Jesli panel z mailem nie byl wyswietlony -> uruchom timer */
                {
                    StartEmailTimer();
                    isEmailPanelShown = true;
                }

                if (!isSuspiciousLinkPanelShown)                                        /** Jesli panel z podejrzanym linkiem nie byl wyswietlony -> uruchom timer */
                {
                    StartSuspiciousLinkTimer();
                    isSuspiciousLinkPanelShown = true;
                }

                if (!isEmailPanel2Shown)                                                /** Jesli panel z drugim mailem nie byl wyswietlony -> uruchom timer */
                {
                    StartEmailTimer_2();
                    isEmailPanel2Shown = true;
                }

                if (!isNotificationPanelShown)                                          /** Jesli panel z powiadomieniem o wygranej nie byl wyswietlony -> uruchom timer */
                {
                    StartNotificationTimer();
                    isNotificationPanelShown = true;
                }

                if (!isNotificationPanel2Shown)                                          /** Jesli panel z powiadomieniem o drugiej wygranej nie byl wyswietlony -> uruchom timer */
                {
                    StartNotificationTimer_2();
                    isNotificationPanel2Shown = true;
                }

                if (!isSuspiciousLinkPanelShown_2)                                      /** Jesli panel z drugim podejrzanym linkiem nie byl wyswietlony -> uruchom timer */
                {
                    StartSuspiciousLinkTimer_2();
                    isSuspiciousLinkPanelShown_2 = true;
                }

                if (!isEmailPanel3Shown)                                                /** Jesli panel z drugim mailem nie byl wyswietlony -> uruchom timer */
                {
                    StartEmailTimer_3();
                    isEmailPanel3Shown = true;
                }
            }
        }

        /** Metoda, ktora inicjalizuje nowy timer do obslugi czasu dla odpowiedzi */
        private void StartResponseTimer()
        {
            responseTimer = new System.Windows.Forms.Timer();
            responseTimer.Interval = 3000;                                              /** Ustawienie interwalu timera na 5 sekund */
            responseTimer.Tick += ResponseTimer_Tick;                                   /** Podpiecie zdarzenia tick do metody obslugujacej */
            responseTimer.Start();                                                      /** Rozpoczecie odliczania timera */
        }

        /** Metoda, ktora jest wywolywana, gdy timer osiagnie ustawiony interwal -> pokaz panel z odpowiedzia */
        private void ResponseTimer_Tick(object sender, EventArgs e)
        {
            responseTimer.Stop();                                                       /** Zatrzymanie timera */
            ShowResponsePanel();                                                        /** Wyswietlanie metody z wyswietlaniem panelu */
        }

        /** Metoda sluzaca do wyswietlenia i wystylizowania panelu odpowiedzi */
        private void ShowResponsePanel()
        {
            /** Dodanie i wystylizowanie panelu z odpowiedzia */
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

            /** Dodanie i wystylizowanie przyciskow wyboru */
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

        /** Metoda sluzaca do obslugi odpowiedzi przez uzytkownika */
        private void HandleResponse(bool isAgreed)
        {
            if (isAgreed)                                           /** Jesli uzytkownik sie zgodzi */
            {
                checkBox3.Checked = false;                          /** Odznaczenie kontrolki Antywirusa */
                totalPoints -= 100;                                  /** Odejmowanie punktow z glownego licznika*/
                StartAntiVirusTimer();                              /** Wlaczenie timera dotyczacego Antywirusa */
            }
            else
            {
                totalPoints += 100;                                 /** Dodawanie punktow do glownego licznika */
            }

            scoreLabel.Text = "PUNKTY: " + totalPoints;             /** Aktualizacja etykiety z punktami */

            this.Controls.Remove(responsePanel);                    /** Wylaczenie panelu z odpowiedzia */
        }

        /** Metoda, ktora inicjalizuje nowy timer do obslugi czasu dla odpowiedzi */
        private void StartResponseTimer_2()
        {
            responseTimer_2 = new System.Windows.Forms.Timer();
            responseTimer_2.Interval = 32000;                                              /** Ustawienie interwalu timera na 5 sekund */
            responseTimer_2.Tick += ResponseTimer_Tick_2;                                   /** Podpiecie zdarzenia tick do metody obslugujacej */
            responseTimer_2.Start();                                                      /** Rozpoczecie odliczania timera */
        }

        /** Metoda, ktora jest wywolywana, gdy timer osiagnie ustawiony interwal -> pokaz panel z odpowiedzia */
        private void ResponseTimer_Tick_2(object sender, EventArgs e)
        {
            responseTimer_2.Stop();                                                       /** Zatrzymanie timera */
            ShowResponsePanel_2();                                                        /** Wyswietlanie metody z wyswietlaniem panelu */
        }

        /** Metoda sluzaca do wyswietlenia i wystylizowania panelu odpowiedzi */
        private void ShowResponsePanel_2()
        {
            /** Dodanie i wystylizowanie panelu z odpowiedzia */
            responsePanel_2 = new Panel();
            responsePanel_2.Size = new System.Drawing.Size(250, 150);
            int leftMargin = 20;
            int bottomMargin = 20;
            responsePanel_2.Location = new System.Drawing.Point(leftMargin, this.ClientSize.Height - responsePanel_2.Height - bottomMargin);
            responsePanel_2.BackColor = Color.LightBlue;
            responsePanel_2.BorderStyle = BorderStyle.FixedSingle;
            responsePanel_2.Padding = new Padding(10);

            /** Dodanie etykiet do odpowiedzi */
            Label fromLabel = new Label();
            fromLabel.Text = "Od: Sekretarka Natalia";
            fromLabel.AutoSize = true;
            fromLabel.Location = new Point(10, 10);
            responsePanel_2.Controls.Add(fromLabel);

            Label toLabel = new Label();
            toLabel.Text = "Do: Serwerownia";
            toLabel.AutoSize = true;
            toLabel.Location = new Point(10, 30);
            responsePanel_2.Controls.Add(toLabel);

            Label titleLabel = new Label();
            titleLabel.Text = "Tytuł: Dokumenty";
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(10, 50);
            responsePanel_2.Controls.Add(titleLabel);

            Label contentLabel = new Label();
            contentLabel.Text = "Treść: Cześć, przesyłam dokumenty?";
            contentLabel.AutoSize = true;
            contentLabel.Location = new Point(10, 70);
            responsePanel_2.Controls.Add(contentLabel);

            Label sourceLabel = new Label();
            sourceLabel.Text = "Źródło: Nieznane";
            sourceLabel.AutoSize = true;
            sourceLabel.Location = new Point(10, 90);
            responsePanel_2.Controls.Add(sourceLabel);

            /** Dodanie i wystylizowanie przyciskow wyboru */
            Button btnAgree = new Button();
            btnAgree.Text = "Zgadzam się";
            btnAgree.Location = new Point(20, 120);
            btnAgree.Click += (s, args) => { HandleResponse_2(true); };
            btnAgree.AutoSize = true;
            responsePanel_2.Controls.Add(btnAgree);

            Button btnDisagree = new Button();
            btnDisagree.Text = "Nie zgadzam się";
            btnDisagree.Location = new Point(120, 120);
            btnDisagree.Click += (s, args) => { HandleResponse_2(false); };
            btnDisagree.AutoSize = true;
            responsePanel_2.Controls.Add(btnDisagree);

            this.Controls.Add(responsePanel_2);
        }

        /** Metoda sluzaca do obslugi odpowiedzi przez uzytkownika */
        private void HandleResponse_2(bool isAgreed)
        {
            if (isAgreed)                                           /** Jesli uzytkownik sie zgodzi */
            {
                checkBox3.Checked = false;                          /** Odznaczenie kontrolki Antywirusa */
                totalPoints -= 100;                                  /** Odejmowanie punktow z glownego licznika*/
            }
            else
            {
                totalPoints += 100;                                 /** Dodawanie punktow do glownego licznika */
            }

            scoreLabel.Text = "PUNKTY: " + totalPoints;             /** Aktualizacja etykiety z punktami */

            this.Controls.Remove(responsePanel_2);                    /** Wylaczenie panelu z odpowiedzia */
        }

        /** Metoda, ktora inicjalizuje nowy timer do obslugi czasu dla maila */
        private void StartEmailTimer()
        {
            emailTimer = new System.Windows.Forms.Timer();
            emailTimer.Interval = 9000;                                                /** Ustawienie interwalu timera na 10 sekund */
            emailTimer.Tick += EmailTimer_Tick;                                         /** Podpiecie zdarzenia tick do metody obslugujacej */
            emailTimer.Start();                                                         /** Rozpoczecie odliczania timera */
        }

        /** Metoda, ktora jest wywolywana, gdy timer osiagnie ustawiony interwal -> pokaz panel z mailem */
        private void EmailTimer_Tick(object sender, EventArgs e)
        {
            emailTimer.Stop();                                                          /** Zatrzymanie timera */
            ShowEmailPanel();                                                           /** Wyswietlanie metody z wyswietlaniem panelu */
        }

        /** Metoda sluzaca do wyswietlenia i wystylizowania panelu maila */
        private void ShowEmailPanel()
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

            /** Dodanie i wystylizowanie przyciskow wyboru */
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

        /** Metoda sluzaca do obslugi odpowiedzi przez uzytkownika */
        private void HandleEmailResponse(bool isAgreed)
        {
            if (isAgreed)                                           /** Jesli uzytkownik sie zgodzi */
            {
                totalPoints += 100;                                 /** Dodawanie punktow do glownego licznika */
            }
            else
            {
                totalPoints -= 100;                                  /** Odejmowanie punktow z glownego licznika*/
            }

            scoreLabel.Text = "PUNKTY: " + totalPoints;             /** Aktualizacja etykiety z punktami */
            this.Controls.Remove(emailPanel);                       /** Wylaczenie panelu z mailem */
        }

        /** Metoda, ktora inicjalizuje nowy timer do obslugi czasu dla podejrzanego linku */
        private void StartSuspiciousLinkTimer()
        {
            suspiciousLinkTimer = new System.Windows.Forms.Timer();
            suspiciousLinkTimer.Interval = 12000;
            suspiciousLinkTimer.Tick += SuspiciousLinkTimer_Tick;
            suspiciousLinkTimer.Start();
        }

        /** Metoda, ktora jest wywolywana, gdy timer osiagnie ustawiony interwal -> pokaz panel z podejrzanym linkiem */
        private void SuspiciousLinkTimer_Tick(object sender, EventArgs e)
        {
            suspiciousLinkTimer.Stop();
            ShowSuspiciousLinkPanel();
        }

        /** Metoda sluzaca do wyswietlenia i wystylizowania panelu z podejrzanym linkiem */
        private void ShowSuspiciousLinkPanel()
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

            /** Dodanie i wystylizowanie przyciskow wyboru */
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

        /** Metoda sluzaca do obslugi odpowiedzi przez uzytkownika */
        private void HandleSuspiciousLinkResponse(bool isAgreed)
        {
            if (isAgreed)
            {
                totalPoints -= 100;
                scoreLabel.Text = "PUNKTY: " + totalPoints;
                checkBox3.Checked = false;

                for (int i = 1; i <= 15; i++)
                {
                    MessageBox.Show("Uwaga! Wirus!", "Alert", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                totalPoints += 100;
                scoreLabel.Text = "PUNKTY: " + totalPoints;
            }

            suspiciousLinkPanel.Visible = false;
        }

        /** Metoda, ktora inicjalizuje nowy timer do obslugi czasu dla drugiego podejrzanego linku */
        private void StartSuspiciousLinkTimer_2()
        {
            suspiciousLinkTimer_2 = new System.Windows.Forms.Timer();
            suspiciousLinkTimer_2.Interval = 34000;
            suspiciousLinkTimer_2.Tick += SuspiciousLinkTimer_Tick_2;
            suspiciousLinkTimer_2.Start();
        }

        /** Metoda, ktora jest wywolywana, gdy timer osiagnie ustawiony interwal -> pokaz panel z drugim podejrzanym linkiem */
        private void SuspiciousLinkTimer_Tick_2(object sender, EventArgs e)
        {
            suspiciousLinkTimer_2.Stop();
            ShowSuspiciousLinkPanel_2();
        }

        /** Metoda sluzaca do wyswietlenia i wystylizowania panelu z drugim podejrzanym linkiem */
        private void ShowSuspiciousLinkPanel_2()
        {
            /** Dodanie i wystylizowanie panelu z drugim podejrzanym linkiem */
            suspiciousLinkPanel_2 = new Panel();
            suspiciousLinkPanel_2.Size = new System.Drawing.Size(250, 60);
            int leftMargin = 20;
            suspiciousLinkPanel_2.Location = new System.Drawing.Point(leftMargin, (this.ClientSize.Height - suspiciousLinkPanel_2.Height) / 2);
            suspiciousLinkPanel_2.BackColor = Color.LightGray;
            suspiciousLinkPanel_2.BorderStyle = BorderStyle.FixedSingle;
            suspiciousLinkPanel_2.Padding = new Padding(10);

            /** Dodanie etykiety do panelu */
            Label linkLabel = new Label();
            linkLabel.Text = "www.nieDobryPudelek.com";
            linkLabel.Location = new Point(30, 10);
            linkLabel.AutoSize = true;
            suspiciousLinkPanel_2.Controls.Add(linkLabel);

            /** Dodanie i wystylizowanie przyciskow wyboru */
            Button btnAgree = new Button();
            btnAgree.Text = "Wchodzę!";
            btnAgree.Location = new Point(30, 30);
            btnAgree.Click += (s, args) => { HandleSuspiciousLinkResponse_2(true); };
            suspiciousLinkPanel_2.Controls.Add(btnAgree);

            Button btnDisagree = new Button();
            btnDisagree.Text = "Odrzucam";
            btnDisagree.Location = new Point(130, 30);
            btnDisagree.Click += (s, args) => { HandleSuspiciousLinkResponse_2(false); };
            suspiciousLinkPanel_2.Controls.Add(btnDisagree);

            this.Controls.Add(suspiciousLinkPanel_2);
        }

        /** Metoda sluzaca do obslugi odpowiedzi przez uzytkownika */
        private void HandleSuspiciousLinkResponse_2(bool isAgreed)
        {
            if (isAgreed)
            {
                totalPoints -= 100;
                scoreLabel.Text = "PUNKTY: " + totalPoints;
                checkBox3.Checked = false;
            }
            else
            {
                totalPoints += 100;
                scoreLabel.Text = "PUNKTY: " + totalPoints;
            }

            suspiciousLinkPanel_2.Visible = false;
        }

        /** Metoda, ktora inicjalizuje nowy timer do obslugi czasu dla drugiego maila */
        private void StartEmailTimer_2()
        {
            emailTimer_2 = new System.Windows.Forms.Timer();
            emailTimer_2.Interval = 22000;
            emailTimer_2.Tick += EmailTimer_Tick_2;
            emailTimer_2.Start();
        }

        /** Metoda, ktora jest wywolywana, gdy timer osiagnie ustawiony interwal -> pokaz panel z drugim mailem */
        private void EmailTimer_Tick_2(object sender, EventArgs e)
        {
            emailTimer_2.Stop();
            ShowEmailPanel_2();
        }

        /** Metoda sluzaca do wyswietlenia i wystylizowania panelu drugiego maila */
        private void ShowEmailPanel_2()
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

            /** Dodanie i wystylizowanie przyciskow wyboru */
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

        /** Metoda sluzaca do obslugi odpowiedzi przez uzytkownika */
        private void HandleEmailResponse_2(bool isAgreed)
        {
            if (isAgreed)
            {
                totalPoints -= 100;
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

        /** Metoda, ktora inicjalizuje nowy timer do obslugi czasu dla trzeciego maila */
        private void StartEmailTimer_3()
        {
            emailTimer_2 = new System.Windows.Forms.Timer();
            emailTimer_2.Interval = 30000;
            emailTimer_2.Tick += EmailTimer_Tick_3;
            emailTimer_2.Start();
        }

        /** Metoda, ktora jest wywolywana, gdy timer osiagnie ustawiony interwal -> pokaz panel z trzecim mailem */
        private void EmailTimer_Tick_3(object sender, EventArgs e)
        {
            emailTimer_2.Stop();
            ShowEmailPanel_3();
        }

        /** Metoda sluzaca do wyswietlenia i wystylizowania panelu trzeciego maila */
        private void ShowEmailPanel_3()
        {
            /** Dodanie i wystylizowanie panelu z trzecim mailem */
            emailPanel_2 = new System.Windows.Forms.Panel();
            emailPanel_2.Size = new System.Drawing.Size(250, 150);
            int leftMargin = 20;
            int bottomMargin = 20;
            emailPanel_2.Location = new System.Drawing.Point(leftMargin, this.ClientSize.Height - responsePanel.Height - bottomMargin);
            emailPanel_2.BackColor = Color.LightGray;
            emailPanel_2.BorderStyle = BorderStyle.FixedSingle;
            emailPanel_2.Padding = new Padding(10);

            /** Dodanie etykiet do trzeciego maila */
            Label fromLabel = new Label();
            fromLabel.Text = "Od: Pudelek.pl";
            fromLabel.AutoSize = true;
            fromLabel.Location = new Point(10, 10);
            emailPanel_2.Controls.Add(fromLabel);

            Label toLabel = new Label();
            toLabel.Text = "Do: Serwerownia";
            toLabel.AutoSize = true;
            toLabel.Location = new Point(10, 30);
            emailPanel_2.Controls.Add(toLabel);

            Label titleLabel = new Label();
            titleLabel.Text = "Tytuł: Prosba o udostepnienie";
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(10, 50);
            emailPanel_2.Controls.Add(titleLabel);

            Label contentLabel = new Label();
            contentLabel.Text = "Treść: Cześć, dasz pliki?";
            contentLabel.AutoSize = true;
            contentLabel.Location = new Point(10, 70);
            emailPanel_2.Controls.Add(contentLabel);

            /** Dodanie i wystylizowanie przyciskow wyboru */
            Button btnAgree = new Button();
            btnAgree.Text = "Zgadzam się";
            btnAgree.Location = new Point(30, 100);
            btnAgree.Click += (s, args) => { HandleEmailResponse_3(true); };
            btnAgree.AutoSize = true;
            emailPanel_2.Controls.Add(btnAgree);

            Button btnDisagree = new Button();
            btnDisagree.Text = "Nie zgadzam się";
            btnDisagree.Location = new Point(130, 100);
            btnDisagree.Click += (s, args) => { HandleEmailResponse_3(false); };
            btnDisagree.AutoSize = true;
            emailPanel_2.Controls.Add(btnDisagree);

            this.Controls.Add(emailPanel_2);
        }

        /** Metoda sluzaca do obslugi odpowiedzi przez uzytkownika */
        private void HandleEmailResponse_3(bool isAgreed)
        {
            if (isAgreed)
            {
                totalPoints -= 100;
                scoreLabel.Text = "PUNKTY: " + totalPoints;
                checkBox2.Checked = false;
            }
            else
            {
                totalPoints += 100;
                scoreLabel.Text = "PUNKTY: " + totalPoints;
            }

            emailPanel_2.Visible = false;
        }

        /** Metoda, ktora inicjalizuje nowy timer do obslugi czasu dla powiadomienia o wygranej */
        private void StartNotificationTimer()
        {
            notificationTimer = new System.Windows.Forms.Timer();
            notificationTimer.Interval = 30000;
            notificationTimer.Tick += NotificationTimer_Tick;
            notificationTimer.Start();
        }

        /** Metoda, ktora jest wywolywana, gdy timer osiagnie ustawiony interwal -> pokaz panel z powiadomieniem o wygranej */
        private void NotificationTimer_Tick(object sender, EventArgs e)
        {
            notificationTimer.Stop();
            ShowNotificationPanel();
        }

        /** Metoda sluzaca do wyswietlenia i wystylizowania panelu z powiadomieniem o wygranej */
        private void ShowNotificationPanel()
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
            messageLabel.Text = "GRATULACJE! WYGRAlEŚ 1MLN DOLARÓW!";
            messageLabel.Location = new Point(5, 20);
            messageLabel.AutoSize = true;
            notificationPanel.Controls.Add(messageLabel);

            Label messageLabel_2 = new Label();
            messageLabel_2.Text = "Czy chcesz wypłacić na konto?";
            messageLabel_2.Location = new Point(10, 40);
            messageLabel_2.AutoSize = true;
            notificationPanel.Controls.Add(messageLabel_2);

            /** Dodanie i wystylizowanie przyciskow wyboru */
            Button btnAgree = new Button();
            btnAgree.Text = "Wyplać";
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

        /** Metoda sluzaca do obslugi odpowiedzi przez uzytkownika */
        private void HandleNotificationResponse(bool isAgreed)
        {
            if (isAgreed)
            {
                totalPoints -= 75;
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

        /** Metoda, ktora inicjalizuje nowy timer do obslugi czasu dla powiadomienia o wygranej */
        private void StartNotificationTimer_2()
        {
            notificationTimer_2 = new System.Windows.Forms.Timer();
            notificationTimer_2.Interval = 40000;
            notificationTimer_2.Tick += NotificationTimer_Tick_2;
            notificationTimer_2.Start();
        }

        /** Metoda, ktora jest wywolywana, gdy timer osiagnie ustawiony interwal -> pokaz panel z powiadomieniem o wygranej */
        private void NotificationTimer_Tick_2(object sender, EventArgs e)
        {
            notificationTimer_2.Stop();
            ShowNotificationPanel_2();
        }

        /** Metoda sluzaca do wyswietlenia i wystylizowania panelu z powiadomieniem o wygranej */
        private void ShowNotificationPanel_2()
        {
            /** Dodanie i wystylizowanie panelu z powiadomieniem o wygranej */
            notificationPanel_2 = new Panel();
            notificationPanel_2.Size = new System.Drawing.Size(250, 100);
            notificationPanel_2.Location = new System.Drawing.Point((this.ClientSize.Width - notificationPanel_2.Width) / 2, 20);
            notificationPanel_2.BackColor = Color.LightBlue;
            notificationPanel_2.BorderStyle = BorderStyle.FixedSingle;
            notificationPanel_2.Padding = new Padding(10);

            /** Dodanie etykiet do panelu z powiadomieniem o wygranej */
            Label messageLabel = new Label();
            messageLabel.Text = "GRATULACJE! WYGRAlEŚ FERRARI!";
            messageLabel.Location = new Point(5, 20);
            messageLabel.AutoSize = true;
            notificationPanel_2.Controls.Add(messageLabel);

            Label messageLabel_2 = new Label();
            messageLabel_2.Text = "Czy chcesz odebrać kluczyki?";
            messageLabel_2.Location = new Point(10, 40);
            messageLabel_2.AutoSize = true;
            notificationPanel_2.Controls.Add(messageLabel_2);

            /** Dodanie i wystylizowanie przyciskow wyboru */
            Button btnAgree = new Button();
            btnAgree.Text = "Odbierz";
            btnAgree.Location = new Point(30, 70);
            btnAgree.Click += (s, args) => { HandleNotificationResponse_2(true); };
            btnAgree.AutoSize = true;
            notificationPanel_2.Controls.Add(btnAgree);

            Button btnDisagree = new Button();
            btnDisagree.Text = "Nie odbieraj";
            btnDisagree.Location = new Point(130, 70);
            btnDisagree.Click += (s, args) => { HandleNotificationResponse_2(false); };
            btnDisagree.AutoSize = true;
            notificationPanel_2.Controls.Add(btnDisagree);

            this.Controls.Add(notificationPanel_2);
        }

        /** Metoda sluzaca do obslugi odpowiedzi przez uzytkownika */
        private void HandleNotificationResponse_2(bool isAgreed)
        {
            if (isAgreed)
            {
                totalPoints -= 100;
                scoreLabel.Text = "PUNKTY: " + totalPoints;
                checkBox3.Checked = false;
            }
            else
            {
                totalPoints += 100;
                scoreLabel.Text = "PUNKTY: " + totalPoints;
            }

            notificationPanel_2.Visible = false;
        }

        /** Metoda, ktora inicjalizuje nowy timer do obslugi czasu dla znikajacej kontrolki Antywirusa */
        private void StartAntiVirusTimer()
        {
            antiVirusTimer = new System.Windows.Forms.Timer();
            antiVirusTimer.Interval = 18000;
            antiVirusTimer.Tick += AntiVirusTimer_Tick;
            antiVirusTimer.Start();
        }

        /** Metoda, ktora jest wywolywana, gdy timer osiagnie ustawiony interwal */
        private void AntiVirusTimer_Tick(object sender, EventArgs e)
        {
            antiVirusTimer.Stop();


            if (!checkBox3.Checked)
            {

                totalPoints -= 100;
            }
            else
            {

                totalPoints += 100;
            }

            scoreLabel.Text = "PUNKTY: " + totalPoints;
        }

        /** Metoda, ktora pokazuje panel z zabezpieczeniami serwera */
        private void ShowOptionsPanel()
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

            /** Dodanie i wystylizowanie checkboxow do panelu */
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

        /** Metoda, ktora inicjalizuje nowy timer do obslugi czasu dla znikajacej kontrolki Windows Defender */
        private void StartDefenderTimer()
        {
            if (defenderTimer != null)
            {
                defenderTimer.Stop();
            }

            defenderTimer = new System.Windows.Forms.Timer();
            defenderTimer.Interval = 16000;
            defenderTimer.Tick += DefenderTimer_Tick;
            defenderTimer.Start();
        }

        /** Metoda, ktora jest wywolywana, gdy timer osiagnie ustawiony interwal*/
        private void DefenderTimer_Tick(object sender, EventArgs e)
        {
            defenderTimer.Stop();

            checkBox1.Checked = false;
            checkBox1Checked = false;
            totalPoints -= 100;
            scoreLabel.Text = "PUNKTY: " + totalPoints;

            NotifyWindowsDefenderDisabled();
        }



        /** Metoda, ktora sluzy do zresetowania labiryntu -> pokaz labirynt jeszcze raz */
        private void ResetMaze()
        {
            player = new Player(MazeLevel.Hard.StartPoint.X, MazeLevel.Hard.StartPoint.Y);
            mazePanel.Visible = true;

            if (!isMazeCompleted)
            {
                elapsedTime = 0;
                timerLabel.Text = "CZAS: " + elapsedTime + " s";
                gameTimer.Start();
            }
        }

        /** Metoda, ktora sluzy do sprawdzania poprawnosci zaznaczenia kontrolek zabezpieczeń */
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

        /** Metoda sluzaca do wyjscia do menu glownego */
        private void BtnBack_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Czy jesteś pewny? Gra nie zostanie zapisana!", "Wyjście do menu głównego.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }

        /** Metoda, ktora sluzy dla gracza do przypomnienia o pilnowaniu zaznaczania kontrolek */
        private void NotifyWindowsDefenderDisabled()
        {
            if (!isNotificationShown)
            {
                MessageBox.Show("Prawdopodobny atak, sprawdź wszystkie zabezpieczenia!", "Zabezpieczenie wyłączone!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                isNotificationShown = true;
            }

        }

        /** Metoda, ktora kończy rozgrywke */
        private void EndGame()
        {
            /** Wylaczenie wszystkich kontrolek */
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
            completionLabel.Text = "PRZESZEDlEŚ POZIOM 1!";
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

            /** Dodanie przycisku wyboru */
            Button btnExit = new Button();
            btnExit.Text = "WYJDŹ Z GRY";
            btnExit.Location = new Point(70, 90);
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
