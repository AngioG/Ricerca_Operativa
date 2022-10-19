namespace Angioletti_Logistica
{
    partial class frm_vis
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
            this.list_main = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // list_main
            // 
            this.list_main.Dock = System.Windows.Forms.DockStyle.Fill;
            this.list_main.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.list_main.FormattingEnabled = true;
            this.list_main.ItemHeight = 25;
            this.list_main.Location = new System.Drawing.Point(0, 0);
            this.list_main.Name = "list_main";
            this.list_main.Size = new System.Drawing.Size(600, 800);
            this.list_main.TabIndex = 0;
            // 
            // frm_vis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 800);
            this.Controls.Add(this.list_main);
            this.Name = "frm_vis";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.frm_vis_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private ListBox list_main;
    }
}