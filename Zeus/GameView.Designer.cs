namespace Zeus
{
    partial class GameView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gbOpponent = new System.Windows.Forms.GroupBox();
            this.lPointsOpponent = new System.Windows.Forms.Label();
            this.lOpponent = new System.Windows.Forms.Label();
            this.gbPlayer = new System.Windows.Forms.GroupBox();
            this.lPointsPlayer = new System.Windows.Forms.Label();
            this.bPaper = new System.Windows.Forms.Button();
            this.bRock = new System.Windows.Forms.Button();
            this.bScissors = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lStatus = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tbMessage = new System.Windows.Forms.TextBox();
            this.lbChatMessages = new System.Windows.Forms.ListBox();
            this.gbOpponent.SuspendLayout();
            this.gbPlayer.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbOpponent
            // 
            this.gbOpponent.Controls.Add(this.lPointsOpponent);
            this.gbOpponent.Controls.Add(this.lOpponent);
            this.gbOpponent.Location = new System.Drawing.Point(12, 12);
            this.gbOpponent.Name = "gbOpponent";
            this.gbOpponent.Size = new System.Drawing.Size(439, 71);
            this.gbOpponent.TabIndex = 0;
            this.gbOpponent.TabStop = false;
            this.gbOpponent.Text = "Gegner";
            // 
            // lPointsOpponent
            // 
            this.lPointsOpponent.AutoSize = true;
            this.lPointsOpponent.Location = new System.Drawing.Point(6, 16);
            this.lPointsOpponent.Name = "lPointsOpponent";
            this.lPointsOpponent.Size = new System.Drawing.Size(53, 13);
            this.lPointsOpponent.TabIndex = 1;
            this.lPointsOpponent.Text = "Punkte: 0";
            // 
            // lOpponent
            // 
            this.lOpponent.AutoSize = true;
            this.lOpponent.Location = new System.Drawing.Point(6, 38);
            this.lOpponent.Name = "lOpponent";
            this.lOpponent.Size = new System.Drawing.Size(16, 13);
            this.lOpponent.TabIndex = 0;
            this.lOpponent.Text = "...";
            // 
            // gbPlayer
            // 
            this.gbPlayer.Controls.Add(this.lPointsPlayer);
            this.gbPlayer.Controls.Add(this.bPaper);
            this.gbPlayer.Controls.Add(this.bRock);
            this.gbPlayer.Controls.Add(this.bScissors);
            this.gbPlayer.Location = new System.Drawing.Point(12, 89);
            this.gbPlayer.Name = "gbPlayer";
            this.gbPlayer.Size = new System.Drawing.Size(439, 83);
            this.gbPlayer.TabIndex = 1;
            this.gbPlayer.TabStop = false;
            this.gbPlayer.Text = "Spieler";
            // 
            // lPointsPlayer
            // 
            this.lPointsPlayer.AutoSize = true;
            this.lPointsPlayer.Location = new System.Drawing.Point(6, 16);
            this.lPointsPlayer.Name = "lPointsPlayer";
            this.lPointsPlayer.Size = new System.Drawing.Size(53, 13);
            this.lPointsPlayer.TabIndex = 3;
            this.lPointsPlayer.Text = "Punkte: 0";
            // 
            // bPaper
            // 
            this.bPaper.Enabled = false;
            this.bPaper.Location = new System.Drawing.Point(310, 39);
            this.bPaper.Name = "bPaper";
            this.bPaper.Size = new System.Drawing.Size(120, 23);
            this.bPaper.TabIndex = 2;
            this.bPaper.Text = "Papier";
            this.bPaper.UseVisualStyleBackColor = true;
            this.bPaper.Click += new System.EventHandler(this.bPaper_Click);
            // 
            // bRock
            // 
            this.bRock.Enabled = false;
            this.bRock.Location = new System.Drawing.Point(159, 39);
            this.bRock.Name = "bRock";
            this.bRock.Size = new System.Drawing.Size(120, 23);
            this.bRock.TabIndex = 1;
            this.bRock.Text = "Stein";
            this.bRock.UseVisualStyleBackColor = true;
            this.bRock.Click += new System.EventHandler(this.bRock_Click);
            // 
            // bScissors
            // 
            this.bScissors.Enabled = false;
            this.bScissors.Location = new System.Drawing.Point(6, 39);
            this.bScissors.Name = "bScissors";
            this.bScissors.Size = new System.Drawing.Size(120, 23);
            this.bScissors.TabIndex = 0;
            this.bScissors.Text = "Schere";
            this.bScissors.UseVisualStyleBackColor = true;
            this.bScissors.Click += new System.EventHandler(this.bScissors_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lStatus);
            this.groupBox1.Location = new System.Drawing.Point(12, 178);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(439, 54);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Status";
            // 
            // lStatus
            // 
            this.lStatus.AutoSize = true;
            this.lStatus.Location = new System.Drawing.Point(6, 16);
            this.lStatus.Name = "lStatus";
            this.lStatus.Size = new System.Drawing.Size(16, 13);
            this.lStatus.TabIndex = 0;
            this.lStatus.Text = "...";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tbMessage);
            this.groupBox2.Controls.Add(this.lbChatMessages);
            this.groupBox2.Location = new System.Drawing.Point(457, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(439, 220);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Chat";
            // 
            // tbMessage
            // 
            this.tbMessage.Enabled = false;
            this.tbMessage.Location = new System.Drawing.Point(6, 185);
            this.tbMessage.MaxLength = 50;
            this.tbMessage.Name = "tbMessage";
            this.tbMessage.Size = new System.Drawing.Size(427, 20);
            this.tbMessage.TabIndex = 1;
            this.tbMessage.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbMessage_KeyDown);
            // 
            // lbChatMessages
            // 
            this.lbChatMessages.FormattingEnabled = true;
            this.lbChatMessages.Location = new System.Drawing.Point(6, 19);
            this.lbChatMessages.Name = "lbChatMessages";
            this.lbChatMessages.Size = new System.Drawing.Size(427, 160);
            this.lbChatMessages.TabIndex = 0;
            // 
            // GameView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(906, 246);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.gbPlayer);
            this.Controls.Add(this.gbOpponent);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "GameView";
            this.Text = "Zeus";
            this.gbOpponent.ResumeLayout(false);
            this.gbOpponent.PerformLayout();
            this.gbPlayer.ResumeLayout(false);
            this.gbPlayer.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbOpponent;
        private System.Windows.Forms.Label lOpponent;
        private System.Windows.Forms.GroupBox gbPlayer;
        private System.Windows.Forms.Button bPaper;
        private System.Windows.Forms.Button bRock;
        private System.Windows.Forms.Button bScissors;
        private System.Windows.Forms.Label lPointsOpponent;
        private System.Windows.Forms.Label lPointsPlayer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lStatus;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListBox lbChatMessages;
        private System.Windows.Forms.TextBox tbMessage;
    }
}