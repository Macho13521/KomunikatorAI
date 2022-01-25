namespace KomunikatorAI
{
    partial class Rozmowa
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
            this.info_rozmowa = new System.Windows.Forms.Label();
            this.nowawiadomosc = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.oknorozmowy = new System.Windows.Forms.ListView();
            this.Online = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Lokalnie = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sugestie = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // info_rozmowa
            // 
            this.info_rozmowa.AutoSize = true;
            this.info_rozmowa.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.info_rozmowa.Location = new System.Drawing.Point(248, 9);
            this.info_rozmowa.Name = "info_rozmowa";
            this.info_rozmowa.Size = new System.Drawing.Size(57, 20);
            this.info_rozmowa.TabIndex = 0;
            this.info_rozmowa.Text = "label1";
            // 
            // nowawiadomosc
            // 
            this.nowawiadomosc.Location = new System.Drawing.Point(106, 378);
            this.nowawiadomosc.Name = "nowawiadomosc";
            this.nowawiadomosc.Size = new System.Drawing.Size(470, 50);
            this.nowawiadomosc.TabIndex = 1;
            this.nowawiadomosc.Text = "";
            this.nowawiadomosc.TextChanged += new System.EventHandler(this.nowawiadomosc_TextChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(582, 378);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 50);
            this.button1.TabIndex = 2;
            this.button1.Text = "Wyślij";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // oknorozmowy
            // 
            this.oknorozmowy.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Online,
            this.Lokalnie});
            this.oknorozmowy.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.oknorozmowy.HideSelection = false;
            this.oknorozmowy.Location = new System.Drawing.Point(106, 34);
            this.oknorozmowy.MultiSelect = false;
            this.oknorozmowy.Name = "oknorozmowy";
            this.oknorozmowy.Scrollable = false;
            this.oknorozmowy.ShowGroups = false;
            this.oknorozmowy.Size = new System.Drawing.Size(551, 231);
            this.oknorozmowy.TabIndex = 5;
            this.oknorozmowy.TabStop = false;
            this.oknorozmowy.UseCompatibleStateImageBehavior = false;
            this.oknorozmowy.View = System.Windows.Forms.View.Details;
            // 
            // Online
            // 
            this.Online.Text = "";
            this.Online.Width = 274;
            // 
            // Lokalnie
            // 
            this.Lokalnie.Text = "";
            this.Lokalnie.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Lokalnie.Width = 274;
            // 
            // sugestie
            // 
            this.sugestie.BackColor = System.Drawing.SystemColors.Menu;
            this.sugestie.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sugestie.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.sugestie.FormattingEnabled = true;
            this.sugestie.ItemHeight = 20;
            this.sugestie.Location = new System.Drawing.Point(317, 292);
            this.sugestie.Name = "sugestie";
            this.sugestie.Size = new System.Drawing.Size(156, 80);
            this.sugestie.TabIndex = 9;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(709, 48);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "Sugeruj";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Rozmowa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.sugestie);
            this.Controls.Add(this.oknorozmowy);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.nowawiadomosc);
            this.Controls.Add(this.info_rozmowa);
            this.Name = "Rozmowa";
            this.Text = "Rozmowa";
            this.Load += new System.EventHandler(this.Rozmowa_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label info_rozmowa;
        private System.Windows.Forms.RichTextBox nowawiadomosc;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListView oknorozmowy;
        private System.Windows.Forms.ColumnHeader Online;
        private System.Windows.Forms.ColumnHeader Lokalnie;
        private System.Windows.Forms.ListBox sugestie;
        private System.Windows.Forms.Button button2;
    }
}