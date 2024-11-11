namespace CyberguardGame
{
    partial class MainGame
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            BtnStart = new Button();
            BtnControls = new Button();
            BtnRules = new Button();
            SuspendLayout();
            // 
            // BtnStart
            // 
            BtnStart.Location = new Point(439, 390);
            BtnStart.Margin = new Padding(5);
            BtnStart.Name = "BtnStart";
            BtnStart.Size = new Size(125, 40);
            BtnStart.TabIndex = 0;
            BtnStart.Text = "START";
            BtnStart.UseVisualStyleBackColor = true;
            BtnStart.Click += BtnStart_Click;
            // 
            // BtnControls
            // 
            BtnControls.Location = new Point(439, 440);
            BtnControls.Margin = new Padding(5);
            BtnControls.Name = "BtnControls";
            BtnControls.Size = new Size(125, 40);
            BtnControls.TabIndex = 1;
            BtnControls.Text = "STEROWANIE";
            BtnControls.UseVisualStyleBackColor = true;
            BtnControls.Click += BtnControls_Click;
            // 
            // BtnRules
            // 
            BtnRules.Location = new Point(439, 490);
            BtnRules.Margin = new Padding(5);
            BtnRules.Name = "BtnRules";
            BtnRules.Size = new Size(125, 40);
            BtnRules.TabIndex = 2;
            BtnRules.Text = "ZASADY GRY";
            BtnRules.UseVisualStyleBackColor = true;
            BtnRules.Click += BtnRules_Click;
            // 
            // MainGame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackgroundImage = Properties.Resources.JEMA_GER_1426_24_v1;
            ClientSize = new Size(1008, 681);
            Controls.Add(BtnRules);
            Controls.Add(BtnControls);
            Controls.Add(BtnStart);
            Name = "MainGame";
            Text = "CYBERGUARD GAME";
            ResumeLayout(false);
        }

        #endregion

        private Button BtnStart;
        private Button BtnControls;
        private Button BtnRules;
    }
}
