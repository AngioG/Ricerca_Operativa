namespace Angioletti_Logistica
{
    partial class frm_main
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
            this.lbl_title = new System.Windows.Forms.Label();
            this.pan_controls = new System.Windows.Forms.Panel();
            this.btn_gen = new System.Windows.Forms.Button();
            this.btn_chnge_table = new System.Windows.Forms.Button();
            this.nud_cons = new System.Windows.Forms.NumericUpDown();
            this.nud_prod = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgv_main = new System.Windows.Forms.DataGridView();
            this.pan_controls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_cons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_prod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_main)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_title
            // 
            this.lbl_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_title.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbl_title.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lbl_title.Location = new System.Drawing.Point(0, 0);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(1400, 50);
            this.lbl_title.TabIndex = 0;
            this.lbl_title.Text = "LOGISTICA";
            this.lbl_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pan_controls
            // 
            this.pan_controls.Controls.Add(this.btn_gen);
            this.pan_controls.Controls.Add(this.btn_chnge_table);
            this.pan_controls.Controls.Add(this.nud_cons);
            this.pan_controls.Controls.Add(this.nud_prod);
            this.pan_controls.Controls.Add(this.label2);
            this.pan_controls.Controls.Add(this.label1);
            this.pan_controls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pan_controls.Location = new System.Drawing.Point(0, 50);
            this.pan_controls.Name = "pan_controls";
            this.pan_controls.Size = new System.Drawing.Size(1400, 100);
            this.pan_controls.TabIndex = 1;
            // 
            // btn_gen
            // 
            this.btn_gen.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_gen.Location = new System.Drawing.Point(375, 15);
            this.btn_gen.Name = "btn_gen";
            this.btn_gen.Size = new System.Drawing.Size(75, 75);
            this.btn_gen.TabIndex = 6;
            this.btn_gen.TabStop = false;
            this.btn_gen.Text = "Genera dati casuali";
            this.btn_gen.UseVisualStyleBackColor = true;
            // 
            // btn_chnge_table
            // 
            this.btn_chnge_table.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_chnge_table.Location = new System.Drawing.Point(275, 15);
            this.btn_chnge_table.Name = "btn_chnge_table";
            this.btn_chnge_table.Size = new System.Drawing.Size(75, 75);
            this.btn_chnge_table.TabIndex = 5;
            this.btn_chnge_table.TabStop = false;
            this.btn_chnge_table.Text = "Aggiorna Tabella";
            this.btn_chnge_table.UseVisualStyleBackColor = true;
            this.btn_chnge_table.Click += new System.EventHandler(this.btn_chnge_table_Click);
            // 
            // nud_cons
            // 
            this.nud_cons.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nud_cons.Location = new System.Drawing.Point(185, 60);
            this.nud_cons.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.nud_cons.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_cons.Name = "nud_cons";
            this.nud_cons.Size = new System.Drawing.Size(50, 25);
            this.nud_cons.TabIndex = 4;
            this.nud_cons.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // nud_prod
            // 
            this.nud_prod.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nud_prod.Location = new System.Drawing.Point(185, 15);
            this.nud_prod.Maximum = new decimal(new int[] {
            40,
            0,
            0,
            0});
            this.nud_prod.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_prod.Name = "nud_prod";
            this.nud_prod.Size = new System.Drawing.Size(50, 25);
            this.nud_prod.TabIndex = 2;
            this.nud_prod.Value = new decimal(new int[] {
            3,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(15, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Numero consumatori:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(15, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Numero produttori:";
            // 
            // dgv_main
            // 
            this.dgv_main.AllowUserToAddRows = false;
            this.dgv_main.AllowUserToDeleteRows = false;
            this.dgv_main.AllowUserToResizeColumns = false;
            this.dgv_main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv_main.Location = new System.Drawing.Point(25, 175);
            this.dgv_main.Name = "dgv_main";
            this.dgv_main.RowTemplate.Height = 25;
            this.dgv_main.Size = new System.Drawing.Size(1350, 600);
            this.dgv_main.TabIndex = 2;
            // 
            // frm_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 800);
            this.Controls.Add(this.dgv_main);
            this.Controls.Add(this.pan_controls);
            this.Controls.Add(this.lbl_title);
            this.Name = "frm_main";
            this.Text = "Logistica - Angioletti";
            this.Load += new System.EventHandler(this.frm_main_Load);
            this.pan_controls.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.nud_cons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_prod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_main)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Label lbl_title;
        private Panel pan_controls;
        private Button btn_gen;
        private Button btn_chnge_table;
        private NumericUpDown nud_cons;
        private NumericUpDown nud_prod;
        private Label label2;
        private Label label1;
        private DataGridView dgv_main;
    }
}