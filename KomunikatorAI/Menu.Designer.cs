namespace KomunikatorAI
{
    partial class Menu
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
            this.przywitanie = new System.Windows.Forms.Label();
            this.nowyznajomy = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.listaznajomych = new System.Windows.Forms.ListBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.zaproszeniaznajomych = new System.Windows.Forms.ListBox();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // przywitanie
            // 
            this.przywitanie.AutoSize = true;
            this.przywitanie.Location = new System.Drawing.Point(406, 49);
            this.przywitanie.Name = "przywitanie";
            this.przywitanie.Size = new System.Drawing.Size(35, 13);
            this.przywitanie.TabIndex = 0;
            this.przywitanie.Text = "label1";
            // 
            // nowyznajomy
            // 
            this.nowyznajomy.Location = new System.Drawing.Point(48, 49);
            this.nowyznajomy.Name = "nowyznajomy";
            this.nowyznajomy.Size = new System.Drawing.Size(100, 20);
            this.nowyznajomy.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(154, 49);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Dodaj";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listaznajomych
            // 
            this.listaznajomych.FormattingEnabled = true;
            this.listaznajomych.Location = new System.Drawing.Point(48, 210);
            this.listaznajomych.Name = "listaznajomych";
            this.listaznajomych.Size = new System.Drawing.Size(181, 121);
            this.listaznajomych.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(48, 353);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 4;
            this.button2.Text = "Czat";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(154, 353);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 5;
            this.button3.Text = "Usuń";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // zaproszeniaznajomych
            // 
            this.zaproszeniaznajomych.FormattingEnabled = true;
            this.zaproszeniaznajomych.Location = new System.Drawing.Point(590, 210);
            this.zaproszeniaznajomych.Name = "zaproszeniaznajomych";
            this.zaproszeniaznajomych.Size = new System.Drawing.Size(181, 121);
            this.zaproszeniaznajomych.TabIndex = 6;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(590, 353);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "Akceptuj";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(696, 353);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 8;
            this.button5.Text = "Odrzuć";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(96, 185);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Lista znajomych";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(616, 185);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(133, 13);
            this.label2.TabIndex = 10;
            this.label2.Text = "Zaproszenia do znajomych";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(713, 12);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 11;
            this.button6.Text = "Odśwież";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.zaproszeniaznajomych);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.listaznajomych);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.nowyznajomy);
            this.Controls.Add(this.przywitanie);
            this.Name = "Menu";
            this.Text = "Menu";
            this.Load += new System.EventHandler(this.Menu_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label przywitanie;
        private System.Windows.Forms.TextBox nowyznajomy;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ListBox listaznajomych;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ListBox zaproszeniaznajomych;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button6;
    }
}