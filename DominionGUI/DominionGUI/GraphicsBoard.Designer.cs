namespace DominionGUI
{
    partial class GraphicsBoard
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        private static int infoLabelsyValue = 550;

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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphicsBoard));
            this.exitButton = new System.Windows.Forms.Button();
            this.skipTurnButton = new System.Windows.Forms.Button();
            this.deckImage = new System.Windows.Forms.PictureBox();
            this.discardImage = new System.Windows.Forms.PictureBox();
            this.decksize = new System.Windows.Forms.Label();
            this.actionleft = new System.Windows.Forms.Label();
            this.buyleft = new System.Windows.Forms.Label();
            this.moneyleft = new System.Windows.Forms.Label();
            this.discardsize = new System.Windows.Forms.Label();
            this.playerLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.deckImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.discardImage)).BeginInit();
            this.SuspendLayout();
            //
            // playerLabel
            //
            this.playerLabel.AutoSize = true;
            this.playerLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.playerLabel.Location = new System.Drawing.Point((this.Width), 25);
            this.playerLabel.Name = "player";
            this.playerLabel.Size = new System.Drawing.Size(148, 31);
            this.playerLabel.TabIndex = 24;
            this.playerLabel.Text = "It's Player 1's turn.";

            // 
            // button1
            // 
            this.exitButton.Location = new System.Drawing.Point(30, 450);
            this.exitButton.Margin = new System.Windows.Forms.Padding(2);
            this.exitButton.Name = "button1";
            this.exitButton.Text = "Exit Game";
            this.exitButton.Font = new System.Drawing.Font("Times New Roman", 14, System.Drawing.FontStyle.Regular);
            this.exitButton.Size = new System.Drawing.Size(100, 50);
            this.exitButton.TabIndex = 0;
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.exitButtonClick);
            //
            // skip Button
            //
            this.skipTurnButton.Location = new System.Drawing.Point(1150, 450);
            this.skipTurnButton.Margin = new System.Windows.Forms.Padding(2);
            this.skipTurnButton.Name = "end Phase";
            this.skipTurnButton.Size = new System.Drawing.Size(150, 50);
            this.skipTurnButton.TabIndex = 16;
            this.skipTurnButton.Font = new System.Drawing.Font("Times New Roman", 14, System.Drawing.FontStyle.Regular);
            this.skipTurnButton.Text = "End Phase";
            this.skipTurnButton.Click += new System.EventHandler(this.SkipPhase);
            this.skipTurnButton.Enabled = false;

            // 
            // pictureBox1
            // 
            this.deckImage.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.deckImage.Location = new System.Drawing.Point(15, 600);
            this.deckImage.Name = "pictureBox1";
            this.deckImage.Size = new System.Drawing.Size(127, 185);
            this.deckImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.deckImage.TabIndex = 17;
            this.deckImage.TabStop = false;
            // 
            // pictureBox2
            // 
            this.discardImage.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.discardImage.Location = new System.Drawing.Point(1200, 600);
            this.discardImage.Name = "pictureBox2";
            this.discardImage.Size = new System.Drawing.Size(127, 185);
            this.discardImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.discardImage.TabIndex = 18;
            this.discardImage.TabStop = false;
            // 
            // deck
            // 
            this.decksize.AutoSize = true;
            this.decksize.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.decksize.Location = new System.Drawing.Point(15, infoLabelsyValue);
            this.decksize.Name = "deck";
            this.decksize.Size = new System.Drawing.Size(148, 31);
            this.decksize.TabIndex = 19;
            this.decksize.Text = "Deck size: " + DominionCards.GameBoard.getInstance().turnOrder.Peek().getDeck().Count;
            // 
            // actionleft
            // 
            this.actionleft.AutoSize = true;
            this.actionleft.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.actionleft.Location = new System.Drawing.Point(315, infoLabelsyValue);
            this.actionleft.Name = "actionleft";
            this.actionleft.Size = new System.Drawing.Size(158, 31);
            this.actionleft.TabIndex = 20;
            this.actionleft.Text = "Action Left: ";
            // 
            // buyleft
            // 
            this.buyleft.AutoSize = true;
            this.buyleft.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.buyleft.Location = new System.Drawing.Point(598, infoLabelsyValue);
            this.buyleft.Name = "buyleft";
            this.buyleft.Size = new System.Drawing.Size(129, 31);
            this.buyleft.TabIndex = 21;
            this.buyleft.Text = "Buy Left: ";
            // 
            // moneyleft
            // 
            this.moneyleft.AutoSize = true;
            this.moneyleft.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.moneyleft.Location = new System.Drawing.Point(882, infoLabelsyValue);
            this.moneyleft.Name = "moneyleft";
            this.moneyleft.Size = new System.Drawing.Size(163, 31);
            this.moneyleft.TabIndex = 22;
            this.moneyleft.Text = "Money Left: ";
            // 
            // discarddeck
            // 
            this.discardsize.AutoSize = true;
            this.discardsize.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.discardsize.Location = new System.Drawing.Point(1165, infoLabelsyValue);
            this.discardsize.Name = "discarddeck";
            this.discardsize.Size = new System.Drawing.Size(248, 31);
            this.discardsize.TabIndex = 23;
            this.discardsize.Text = "Discard Deck size: ";
            // 
            // MainBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1874, 1054);
            this.Controls.Add(this.discardsize);
            this.Controls.Add(this.decksize);
            this.Controls.Add(this.skipTurnButton);
            this.Controls.Add(this.actionleft);
            this.Controls.Add(this.buyleft);
            this.Controls.Add(this.moneyleft);
            this.Controls.Add(this.discardImage);
            this.Controls.Add(this.deckImage);
            this.Controls.Add(this.exitButton);
            this.Controls.Add(this.playerLabel);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "MainBoard";
            this.Text = "Dominion Game Board";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainBoard_Load);
            ((System.ComponentModel.ISupportInitialize)(this.deckImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.discardImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exitButton;
        private System.Windows.Forms.PictureBox deckImage;
        private System.Windows.Forms.PictureBox discardImage;
        private System.Windows.Forms.Label decksize;
        private System.Windows.Forms.Label actionleft;
        private System.Windows.Forms.Label buyleft;
        private System.Windows.Forms.Label moneyleft;
        private System.Windows.Forms.Label discardsize;
        private System.Windows.Forms.Label playerLabel;
        private System.Windows.Forms.Button skipTurnButton;
    }
}