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
            // Menu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
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
    }
}