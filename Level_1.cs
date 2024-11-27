using System;
using System.Drawing;
using System.Windows.Forms;

namespace CyberguardGame
{
    public partial class Level_1 : Form
    {
        private Button btnBack;
        private Panel optionsPanel;   // Panel z kontrolkami do zaznaczania
        private Panel scorePanel;     // Panel z punktami
        private CheckBox checkBox1, checkBox2, checkBox3;
        private Label panelTitle, scoreLabel;
        private int totalPoints = 0;  // Zmienna przechowująca punkty
        private System.Windows.Forms.Timer inactivityTimer;      // Timer do monitorowania aktywności
        private System.Windows.Forms.Timer windowsDefenderTimer; // Timer dla WINDOWS DEFENDER
        private int inactivityCounter = 0;                       // Licznik bezczynności w sekundach
        private bool checkBox1Checked = false;
        private bool checkBox2Checked = false;
        private bool checkBox3Checked = false;
        private bool windowsDefenderActivated = false;

        public Level_1()
        {
            InitializeComponent();
            LevelScreen();
            InitializeTimers();
        }

        private void LevelScreen()
        {
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

            // Panel z licznikiem punktów
            scorePanel = new Panel();
            scorePanel.Size = new System.Drawing.Size(200, 50);
            scorePanel.Location = new System.Drawing.Point(this.ClientSize.Width - scorePanel.Width - 20, 20);
            scorePanel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            scorePanel.BackColor = SystemColors.WindowFrame;
            scorePanel.BorderStyle = BorderStyle.FixedSingle;
            scorePanel.Padding = new Padding(10);
            scorePanel.MouseMove += new MouseEventHandler(ResetInactivityCounter);

            // Etykieta do wyświetlania punktów
            scoreLabel = new Label();
            scoreLabel.Text = "PUNKTY: " + totalPoints;
            scoreLabel.Location = new System.Drawing.Point(10, 10);
            scoreLabel.AutoSize = true;
            scoreLabel.Font = new Font("Snap ITC", 11);
            scoreLabel.ForeColor = SystemColors.HighlightText;

            // Dodanie etykiety do panelu punktów
            scorePanel.Controls.Add(scoreLabel);

            // Dodanie panelu punktów do formularza
            this.Controls.Add(scorePanel);
        }

        private void InitializeTimers()
        {
            // Timer dla monitorowania bezczynności
            inactivityTimer = new System.Windows.Forms.Timer();
            inactivityTimer.Interval = 2000; // Co 2 sekundy
            inactivityTimer.Tick += new EventHandler(InactivityTimer_Tick);
            inactivityTimer.Start();

            // Timer dla WINDOWS DEFENDER
            windowsDefenderTimer = new System.Windows.Forms.Timer();
            windowsDefenderTimer.Interval = 15000; // 15 sekund
            windowsDefenderTimer.Tick += new EventHandler(WindowsDefenderTimer_Tick);
        }

        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            // Jeżeli kontrolka jest zaznaczona i nie była jeszcze zaznaczona
            if (sender == checkBox1 && !checkBox1Checked)
            {
                checkBox1Checked = true; // Ustawiamy flagę na true, żeby nie dawać ponownie punktów
                totalPoints += 100;
                windowsDefenderActivated = true;
                windowsDefenderTimer.Start(); // Startujemy timer dla WINDOWS DEFENDER
            }
            else if (sender == checkBox2 && !checkBox2Checked)
            {
                checkBox2Checked = true;
                totalPoints += 100;
            }
            else if (sender == checkBox3 && !checkBox3Checked)
            {
                checkBox3Checked = true;
                totalPoints += 100;
            }

            // Zaktualizowanie punktów w etykiecie
            scoreLabel.Text = "PUNKTY: " + totalPoints;

            // Resetowanie licznika bezczynności, jeśli którakolwiek kontrolka została zaznaczona
            ResetInactivityCounter(null, null);
        }

        private void InactivityTimer_Tick(object sender, EventArgs e)
        {
            // Sprawdzamy, czy którakolwiek kontrolka nie jest zaznaczona
            if (!checkBox1.Checked || !checkBox2.Checked || !checkBox3.Checked)
            {
                inactivityCounter += 2; // Liczymy 2 sekundy co 2 sekundy

                if (inactivityCounter >= 10) // Po 10 sekundach bezczynności
                {
                    totalPoints -= 5;  // Odejmowanie punktów co 2 sekundy
                    scoreLabel.Text = "PUNKTY: " + totalPoints;
                }
            }
        }

        private void WindowsDefenderTimer_Tick(object sender, EventArgs e)
        {
            // Kontrolka "WINDOWS DEFENDER" ma się wyłączyć po 15 sekundach
            checkBox1.Checked = false;
            windowsDefenderActivated = false;
            MessageBox.Show("Kontrolka WINDOWS DEFENDER została wyłączona.", "Informacja", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Zatrzymanie timera
            windowsDefenderTimer.Stop();

            // Startujemy nowy timer dla bezczynności po wyłączeniu kontrolki
            inactivityTimer.Start();
        }

        private void ResetInactivityCounter(object sender, EventArgs e)
        {
            inactivityCounter = 0; // Resetowanie licznika bezczynności
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Czy jesteś pewny? Gra nie zostanie zapisana!", "Wyjście do menu głównego.", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}