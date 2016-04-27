using System.Collections.Generic;
using System.Collections;
namespace DominionCards
{
    partial class SelectCardsForm
    {
        List<Card> cardsToList;
        string instructions;
        int numToSelect;
        public SelectCardsForm(List<Card> cards, string inst, int num) : base()
        {
            checkboxes = new List<CardCheckBox>();
            cardsToList = cards;
            instructions = inst;
            numToSelect = num;
            InitializeComponent();
            
        }
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
            for (int i = 0; i < cardsToList.Count; i++)
            {
                checkboxes.Add(new CardCheckBox((Card) cardsToList[i]));
                checkboxes[i].AutoSize = true;
                checkboxes[i].Location = new System.Drawing.Point(100, 60 + i*25);
                checkboxes[i].Name = "checkBox" + i;
                checkboxes[i].Size = new System.Drawing.Size(65, 21);
                checkboxes[i].TabIndex = 0;
                checkboxes[i].Text = cardsToList[i].GetType().Name;
                checkboxes[i].UseVisualStyleBackColor = true;
            }
            this.label1 = new System.Windows.Forms.Label();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(219, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = instructions;
            //
            // Button1
            //
            this.button1 = new System.Windows.Forms.Button();
            this.button1.AutoSize = true;
            this.button1.Location = new System.Drawing.Point(120, 60 + checkboxes.Count * 25); //510);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(10, 30);
            this.button1.TabIndex = 2;
            this.button1.Text = "Select";
            this.button1.Click += new System.EventHandler(this.SelectAndMutate);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(300, 100 + checkboxes.Count * 25);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            for (int i = 0; i < checkboxes.Count; i++)
            {
                this.Controls.Add(checkboxes[i]);
            }
            this.Name = "Form1";
            this.Text = "Choose Card";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.PerformLayout();

        }

        #endregion
        List<CardCheckBox> checkboxes;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
    }


}