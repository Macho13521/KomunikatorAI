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
            this.SuspendLayout();
            // 
            // info_rozmowa
            // 
            this.info_rozmowa.AutoSize = true;
            this.info_rozmowa.Location = new System.Drawing.Point(405, 58);
            this.info_rozmowa.Name = "info_rozmowa";
            this.info_rozmowa.Size = new System.Drawing.Size(35, 13);
            this.info_rozmowa.TabIndex = 0;
            this.info_rozmowa.Text = "label1";
            // 
            // Rozmowa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.info_rozmowa);
            this.Name = "Rozmowa";
            this.Text = "Rozmowa";
            this.Load += new System.EventHandler(this.Rozmowa_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label info_rozmowa;
    }
}