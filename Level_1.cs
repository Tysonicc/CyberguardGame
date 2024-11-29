using System;
using System.Drawing;
using System.Windows.Forms;

namespace CyberguardGame
{
    public partial class Level_1 : Form
    {
        private Button btnBack; // Przycisk "KONIEC"
        private Panel optionsPanel;   // Panel z kontrolkami
        private Panel scorePanel, timePanel; // Panele z punktami i czasem
        private Label scoreLabel, timerLabel;
        private Maze maze;            // Obiekt labiryntu
        private Player player;        // Obiekt gracza
        private const int cellWidth = 40; // Szerokość komórki w pikselach
        private const int cellHeight = 40; // Wysokość komórki w pikselach
        private Panel mazePanel;      // Panel do wyświetlania labiryntu
        private int totalPoints = 0;  // Zmienna do przechowywania punktów

        private System.Windows.Forms.Timer gameTimer;      // Timer do liczenia czasu
        private int elapsedTime = 0;  // Licznik czasu w sekundach

        // Flagi dla checkboxów
        private bool checkBox1Checked = false;
        private bool checkBox2Checked = false;
        private bool checkBox3Checked = false;

        // Kontrolki dla panelu
        private CheckBox checkBox1, checkBox2, checkBox3;
        private Label panelTitle;

        public Level_1()
        {
            InitializeComponent();
            InitializeGame();
            InitializeTimer(); // Inicjalizacja timera
        }

        private void InitializeGame()
        {
            // Tworzenie labiryntu z poziomu łatwego
            maze = new Maze(MazeLevel.Easy.Width, MazeLevel.Easy.Height);
            maze.GenerateMaze(MazeLevel.Easy.Grid, MazeLevel.Easy.EndPoint);

            // Ustawienie gracza w punkcie startowym
            player = new Player(MazeLevel.Easy.StartPoint.X, MazeLevel.Easy.StartPoint.Y);

            // Tworzenie panelu do wyświetlania labiryntu
            mazePanel = new Panel();
            mazePanel.Size = new Size(
                MazeLevel.Easy.Width * cellWidth + 20,  // +20 dla marginesów
                MazeLevel.Easy.Height * cellHeight + 20 // +20 dla marginesów
            );
            mazePanel.Location = new Point(
                (this.ClientSize.Width - mazePanel.Width) / 2,
                (this.ClientSize.Height - mazePanel.Height) / 2
            );
            mazePanel.BackColor = Color.LightSteelBlue; // Kolor przypominający Windows XP
            mazePanel.BorderStyle = BorderStyle.Fixed3D; // Obwódka 3D
            mazePanel.Padding = new Padding(10); // Dodanie wewnętrznych marginesów

            this.Controls.Add(mazePanel); // Dodawanie panelu do kontrolek formularza

            // Ustawienie obsługi zdarzenia Paint dla panelu labiryntu
            mazePanel.Paint += new PaintEventHandler(MazePanel_Paint);

            // Ustawienie obsługi zdarzenia KeyDown dla formularza
            this.KeyDown += new KeyEventHandler(Level_1_KeyDown);
            this.KeyPreview = true; // Umożliwia formularzowi odbieranie zdarzeń klawiatury przed kontrolkami

            // Przyciski powrotu
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

            // Panel z licznikiem punktów
            scorePanel = new Panel();
            scorePanel.Size = new System.Drawing.Size(200, 50);
            scorePanel.Location = new System.Drawing.Point(this.ClientSize.Width - scorePanel.Width - 20, 20);
            scorePanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            scorePanel.BackColor = SystemColors.WindowFrame;
            scorePanel.BorderStyle = BorderStyle.FixedSingle;
            scorePanel.Padding = new Padding(10);
            this.Controls.Add(scorePanel);

            // Etykieta do wyświetlania punktów
            scoreLabel = new Label();
            scoreLabel.Text = "PUNKTY: " + totalPoints;
            scoreLabel.Location = new System.Drawing.Point(10, 10);
            scoreLabel.AutoSize = true;
            scoreLabel.Font = new Font("Snap ITC", 11);
            scoreLabel.ForeColor = SystemColors.HighlightText;
            scorePanel.Controls.Add(scoreLabel);

            // Panel z licznikiem czasu
            timePanel = new Panel();
            timePanel.Size = new System.Drawing.Size(200, 50);
            timePanel.Location = new System.Drawing.Point(this.ClientSize.Width - timePanel.Width - 20, 80);
            timePanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            timePanel.BackColor = SystemColors.WindowFrame;
            timePanel.BorderStyle = BorderStyle.FixedSingle;
            timePanel.Padding = new Padding(10);
            this.Controls.Add(timePanel);

            // Etykieta do wyświetlania czasu
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
            gameTimer.Interval = 1000; // Interwał 1 sekunda
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Start(); // Start licznika czasu
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            elapsedTime++; // Zwiększanie czasu o 1 sekundę
            timerLabel.Text = "CZAS: " + elapsedTime + " s"; // Aktualizacja etykiety
        }

        private void MazePanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Rysowanie labiryntu z uwzględnieniem marginesów
            int margin = 10; // Margines wewnętrzny
            for (int y = 0; y < maze.Height; y++)
            {
                for (int x = 0; x < maze.Width; x++)
                {
                    if (maze.Grid[y, x] == 1) // Ściana
                    {
                        g.FillRectangle(
                            Brushes.Black,
                            x * cellWidth + margin,
                            y * cellHeight + margin,
                            cellWidth,
                            cellHeight
                        );
                    }
                    else if (x == maze.EndPoint.X && y == maze.EndPoint.Y) // Punkt końcowy
                    {
                        g.FillRectangle(
                            Brushes.Red,
                            x * cellWidth + margin,
                            y * cellHeight + margin,
                            cellWidth,
                            cellHeight
                        );
                    }
                }
            }

            // Rysowanie gracza
            g.FillRectangle(
                Brushes.Blue,
                player.X * cellWidth + margin,
                player.Y * cellHeight + margin,
                cellWidth,
                cellHeight
            );
        }

        private void Level_1_KeyDown(object sender, KeyEventArgs e)
        {
            // Ruch gracza
            player.Move(e.KeyCode, maze); // Przekazanie obiektu maze
            mazePanel.Invalidate(); // Odświeżenie panelu

            // Sprawdzenie, czy gracz osiągnął punkt końcowy
            if (player.X == maze.EndPoint.X && player.Y == maze.EndPoint.Y)
            {
                totalPoints += 100; // Dodanie 100 punktów
                scoreLabel.Text = "PUNKTY: " + totalPoints; // Aktualizacja etykiety punktów
                mazePanel.Visible = false; // Ukrycie panelu labiryntu
                ShowOptionsPanel(); // Wyświetlenie panelu z kontrolkami
            }
        }

        private void ShowOptionsPanel()
        {
            // Panel z kontrolkami - lewy górny róg
            optionsPanel = new Panel();
            optionsPanel.Size = new System.Drawing.Size(200, 150);
            optionsPanel.Location = new System.Drawing.Point(20, 20);
            optionsPanel.Anchor = AnchorStyles.Top | AnchorStyles.Left;
            optionsPanel.BackColor = Color.LightGray;
            optionsPanel.BorderStyle = BorderStyle.Fixed3D;
            optionsPanel.Padding = new Padding(10);
            optionsPanel.MouseMove += new MouseEventHandler(ResetInactivityCounter);

            // Etykieta tytułowa panelu
            panelTitle = new Label();
            panelTitle.Text = "ZABEZPIECZENIA SERWERA";
            panelTitle.Font = new Font("Tahoma", 8, FontStyle.Bold);
            panelTitle.ForeColor = Color.Black;
            panelTitle.Location = new Point(10, 10);
            panelTitle.AutoSize = true;

            // Dodanie tytułowej etykiety do panelu
            optionsPanel.Controls.Add(panelTitle);

            // CheckBox 1
            checkBox1 = new CheckBox();
            checkBox1.Text = "WINDOWS DEFENDER";
            checkBox1.Location = new System.Drawing.Point(10, 40);
            checkBox1.AutoSize = true;
            checkBox1.CheckedChanged += new EventHandler(CheckBox_CheckedChanged);
            checkBox1.MouseMove += new MouseEventHandler(ResetInactivityCounter);

            // CheckBox 2
            checkBox2 = new CheckBox();
            checkBox2.Text = "FIREWALL";
            checkBox2.Location = new System.Drawing.Point(10, 70);
            checkBox2.AutoSize = true;
            checkBox2.CheckedChanged += new EventHandler(CheckBox_CheckedChanged);
            checkBox2.MouseMove += new MouseEventHandler(ResetInactivityCounter);

            // CheckBox 3
            checkBox3 = new CheckBox();
            checkBox3.Text = "ANTYWIRUS";
            checkBox3.Location = new System.Drawing.Point(10, 100);
            checkBox3.AutoSize = true;
            checkBox3.CheckedChanged += new EventHandler(CheckBox_CheckedChanged);
            checkBox3.MouseMove += new MouseEventHandler(ResetInactivityCounter);

            // Dodanie kontrolki CheckBox do panelu
            optionsPanel.Controls.Add(checkBox1);
            optionsPanel.Controls.Add(checkBox2);
            optionsPanel.Controls.Add(checkBox3);

            // Dodanie panelu z kontrolkami do formularza
            this.Controls.Add(optionsPanel);
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Jeżeli kontrolka jest zaznaczona i nie była jeszcze zaznaczona
            if (sender == checkBox1 && !checkBox1Checked)
            {
                checkBox1Checked = true; // Ustawiamy flagę na true, żeby nie dawać ponownie punktów
                totalPoints += 100;
                scoreLabel.Text = "PUNKTY: " + totalPoints; // Zaktualizowanie punktów
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

        private void ResetInactivityCounter(object sender, EventArgs e)
        {
            // Resetowanie licznika bezczynności
        }
    }
}