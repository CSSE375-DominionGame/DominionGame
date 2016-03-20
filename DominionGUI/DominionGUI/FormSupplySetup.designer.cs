namespace DominionGUI
{
    partial class FormSupplySetup
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
            this.lbl_instructions = new System.Windows.Forms.Label();
            this.btn_ok = new System.Windows.Forms.Button();
            this.ckls_cards = new System.Windows.Forms.CheckedListBox();
            this.btn_randomize = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_instructions
            // 
            this.lbl_instructions.AutoSize = true;
            this.lbl_instructions.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_instructions.Location = new System.Drawing.Point(59, 9);
            this.lbl_instructions.Name = "lbl_instructions";
            this.lbl_instructions.Size = new System.Drawing.Size(196, 20);
            this.lbl_instructions.TabIndex = 0;
            this.lbl_instructions.Text = "Select supply cards (<=10)";
            // 
            // btn_ok
            // 
            this.btn_ok.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ok.Location = new System.Drawing.Point(174, 434);
            this.btn_ok.Name = "btn_ok";
            this.btn_ok.Size = new System.Drawing.Size(135, 55);
            this.btn_ok.TabIndex = 1;
            this.btn_ok.Text = "OK";
            this.btn_ok.UseVisualStyleBackColor = true;
            this.btn_ok.Click += new System.EventHandler(this.btn_ok_Click);
            // 
            // ckls_cards
            // 
            this.ckls_cards.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckls_cards.FormattingEnabled = true;
            this.ckls_cards.Location = new System.Drawing.Point(11, 32);
            this.ckls_cards.Name = "ckls_cards";
            this.ckls_cards.Size = new System.Drawing.Size(293, 382);
            this.ckls_cards.TabIndex = 2;
            this.ckls_cards.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ckls_cards_ItemCheck);
            this.ckls_cards.SelectedIndexChanged += new System.EventHandler(this.ckls_cards_SelectedIndexChanged);
            // 
            // btn_randomize
            // 
            this.btn_randomize.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_randomize.Location = new System.Drawing.Point(11, 434);
            this.btn_randomize.Name = "btn_randomize";
            this.btn_randomize.Size = new System.Drawing.Size(135, 55);
            this.btn_randomize.TabIndex = 3;
            this.btn_randomize.Text = "Randomize";
            this.btn_randomize.UseVisualStyleBackColor = true;
            this.btn_randomize.Click += new System.EventHandler(this.btn_randomize_Click);
            // 
            // FormSupplySetup
            // 
            this.AcceptButton = this.btn_ok;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(321, 501);
            this.Controls.Add(this.btn_randomize);
            this.Controls.Add(this.ckls_cards);
            this.Controls.Add(this.btn_ok);
            this.Controls.Add(this.lbl_instructions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormSupplySetup";
            this.Text = "Supply Setup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_instructions;
        private System.Windows.Forms.Button btn_ok;
        private System.Windows.Forms.CheckedListBox ckls_cards;
        private System.Windows.Forms.Button btn_randomize;
    }
}