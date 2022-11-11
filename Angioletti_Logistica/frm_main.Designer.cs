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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lbl_title = new System.Windows.Forms.Label();
            this.pan_data = new System.Windows.Forms.Panel();
            this.chk_random = new System.Windows.Forms.CheckBox();
            this.nud_tot = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.nud_max = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.nud_min = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_gen = new System.Windows.Forms.Button();
            this.btn_chnge_table = new System.Windows.Forms.Button();
            this.nud_cons = new System.Windows.Forms.NumericUpDown();
            this.nud_prod = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.nud_delay = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_execute = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cmb_execute = new System.Windows.Forms.ComboBox();
            this.dgv_main = new System.Windows.Forms.DataGridView();
            this.list_execution = new System.Windows.Forms.ListBox();
            this.btn_espandi = new System.Windows.Forms.Button();
            this.pan_ex = new System.Windows.Forms.Panel();
            this.btn_esp_dat = new System.Windows.Forms.Button();
            this.pan_data.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_max)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_min)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_cons)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_prod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_delay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_main)).BeginInit();
            this.pan_ex.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbl_title
            // 
            this.lbl_title.Dock = System.Windows.Forms.DockStyle.Top;
            this.lbl_title.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            this.lbl_title.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.lbl_title.Location = new System.Drawing.Point(0, 0);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(1750, 50);
            this.lbl_title.TabIndex = 0;
            this.lbl_title.Text = "LOGISTICA";
            this.lbl_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pan_data
            // 
            this.pan_data.Controls.Add(this.chk_random);
            this.pan_data.Controls.Add(this.nud_tot);
            this.pan_data.Controls.Add(this.label5);
            this.pan_data.Controls.Add(this.nud_max);
            this.pan_data.Controls.Add(this.label4);
            this.pan_data.Controls.Add(this.nud_min);
            this.pan_data.Controls.Add(this.label3);
            this.pan_data.Controls.Add(this.btn_gen);
            this.pan_data.Controls.Add(this.btn_chnge_table);
            this.pan_data.Controls.Add(this.nud_cons);
            this.pan_data.Controls.Add(this.nud_prod);
            this.pan_data.Controls.Add(this.label2);
            this.pan_data.Controls.Add(this.label1);
            this.pan_data.Location = new System.Drawing.Point(25, 50);
            this.pan_data.Name = "pan_data";
            this.pan_data.Size = new System.Drawing.Size(1118, 105);
            this.pan_data.TabIndex = 1;
            // 
            // chk_random
            // 
            this.chk_random.AutoSize = true;
            this.chk_random.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.chk_random.Location = new System.Drawing.Point(252, 50);
            this.chk_random.Name = "chk_random";
            this.chk_random.Size = new System.Drawing.Size(83, 25);
            this.chk_random.TabIndex = 13;
            this.chk_random.Text = "Casuale";
            this.chk_random.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.chk_random.UseVisualStyleBackColor = true;
            this.chk_random.CheckedChanged += new System.EventHandler(this.chk_random_CheckedChanged);
            // 
            // nud_tot
            // 
            this.nud_tot.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nud_tot.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nud_tot.Location = new System.Drawing.Point(462, 20);
            this.nud_tot.Maximum = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            this.nud_tot.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nud_tot.Name = "nud_tot";
            this.nud_tot.Size = new System.Drawing.Size(50, 25);
            this.nud_tot.TabIndex = 12;
            this.nud_tot.Value = new decimal(new int[] {
            750,
            0,
            0,
            0});
            this.nud_tot.ValueChanged += new System.EventHandler(this.nud_tot_ValueChanged);
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(252, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(205, 25);
            this.label5.TabIndex = 11;
            this.label5.Text = "Totale merce da trasportare:";
            // 
            // nud_max
            // 
            this.nud_max.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nud_max.Location = new System.Drawing.Point(878, 64);
            this.nud_max.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.nud_max.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nud_max.Name = "nud_max";
            this.nud_max.Size = new System.Drawing.Size(50, 25);
            this.nud_max.TabIndex = 10;
            this.nud_max.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nud_max.ValueChanged += new System.EventHandler(this.Conflitto_nud);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(693, 64);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(190, 25);
            this.label4.TabIndex = 9;
            this.label4.Text = "Costo massimo trasporti:";
            // 
            // nud_min
            // 
            this.nud_min.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nud_min.Location = new System.Drawing.Point(878, 20);
            this.nud_min.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.nud_min.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nud_min.Name = "nud_min";
            this.nud_min.Size = new System.Drawing.Size(50, 25);
            this.nud_min.TabIndex = 8;
            this.nud_min.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nud_min.ValueChanged += new System.EventHandler(this.Conflitto_nud);
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(693, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(190, 25);
            this.label3.TabIndex = 7;
            this.label3.Text = "Costo minimo trasporti:";
            // 
            // btn_gen
            // 
            this.btn_gen.Enabled = false;
            this.btn_gen.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_gen.Location = new System.Drawing.Point(953, 15);
            this.btn_gen.Name = "btn_gen";
            this.btn_gen.Size = new System.Drawing.Size(75, 75);
            this.btn_gen.TabIndex = 6;
            this.btn_gen.TabStop = false;
            this.btn_gen.Text = "Genera nuovi dati";
            this.btn_gen.UseVisualStyleBackColor = true;
            this.btn_gen.Click += new System.EventHandler(this.btn_gen_Click);
            // 
            // btn_chnge_table
            // 
            this.btn_chnge_table.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_chnge_table.Location = new System.Drawing.Point(537, 14);
            this.btn_chnge_table.Name = "btn_chnge_table";
            this.btn_chnge_table.Size = new System.Drawing.Size(75, 75);
            this.btn_chnge_table.TabIndex = 5;
            this.btn_chnge_table.TabStop = false;
            this.btn_chnge_table.Text = "Nuova Tabella";
            this.btn_chnge_table.UseVisualStyleBackColor = true;
            this.btn_chnge_table.Click += new System.EventHandler(this.btn_chnge_table_Click);
            // 
            // nud_cons
            // 
            this.nud_cons.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nud_cons.Location = new System.Drawing.Point(172, 65);
            this.nud_cons.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nud_cons.Minimum = new decimal(new int[] {
            2,
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
            this.nud_cons.ValueChanged += new System.EventHandler(this.change_tot_max);
            // 
            // nud_prod
            // 
            this.nud_prod.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nud_prod.Location = new System.Drawing.Point(172, 20);
            this.nud_prod.Maximum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.nud_prod.Minimum = new decimal(new int[] {
            2,
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
            this.nud_prod.ValueChanged += new System.EventHandler(this.change_tot_max);
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(2, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(175, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Numero produttori:";
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(2, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(175, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Numero consumatori:";
            // 
            // nud_delay
            // 
            this.nud_delay.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.nud_delay.Increment = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.nud_delay.Location = new System.Drawing.Point(185, 2);
            this.nud_delay.Maximum = new decimal(new int[] {
            5000,
            0,
            0,
            0});
            this.nud_delay.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.nud_delay.Name = "nud_delay";
            this.nud_delay.Size = new System.Drawing.Size(50, 25);
            this.nud_delay.TabIndex = 18;
            this.nud_delay.Value = new decimal(new int[] {
            1500,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label7.Location = new System.Drawing.Point(0, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(205, 25);
            this.label7.TabIndex = 17;
            this.label7.Text = "Tempo per un passaggio:";
            // 
            // btn_execute
            // 
            this.btn_execute.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btn_execute.Enabled = false;
            this.btn_execute.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_execute.Location = new System.Drawing.Point(0, 71);
            this.btn_execute.Name = "btn_execute";
            this.btn_execute.Size = new System.Drawing.Size(325, 34);
            this.btn_execute.TabIndex = 16;
            this.btn_execute.TabStop = false;
            this.btn_execute.Text = "Esegui";
            this.btn_execute.UseVisualStyleBackColor = true;
            this.btn_execute.Click += new System.EventHandler(this.btn_execute_Click);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(0, 40);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(154, 25);
            this.label6.TabIndex = 15;
            this.label6.Text = "Algoritmo :";
            // 
            // cmb_execute
            // 
            this.cmb_execute.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmb_execute.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.cmb_execute.FormattingEnabled = true;
            this.cmb_execute.Items.AddRange(new object[] {
            "Tutti",
            "Nord-Ovest",
            "Minimi Costi",
            "Vogel",
            "Russel"});
            this.cmb_execute.Location = new System.Drawing.Point(111, 40);
            this.cmb_execute.Name = "cmb_execute";
            this.cmb_execute.Size = new System.Drawing.Size(214, 25);
            this.cmb_execute.TabIndex = 14;
            // 
            // dgv_main
            // 
            this.dgv_main.AllowUserToAddRows = false;
            this.dgv_main.AllowUserToDeleteRows = false;
            this.dgv_main.AllowUserToResizeColumns = false;
            this.dgv_main.BackgroundColor = System.Drawing.SystemColors.ActiveCaption;
            this.dgv_main.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_main.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgv_main.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgv_main.EnableHeadersVisualStyles = false;
            this.dgv_main.Location = new System.Drawing.Point(25, 175);
            this.dgv_main.Name = "dgv_main";
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.ControlLightLight;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgv_main.RowHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgv_main.RowHeadersWidth = 120;
            this.dgv_main.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgv_main.RowTemplate.Height = 25;
            this.dgv_main.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
            this.dgv_main.Size = new System.Drawing.Size(1350, 600);
            this.dgv_main.TabIndex = 2;
            this.dgv_main.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_main_CellLeave);
            this.dgv_main.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgv_main_EditingControlShowing);
            // 
            // list_execution
            // 
            this.list_execution.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.list_execution.FormattingEnabled = true;
            this.list_execution.Location = new System.Drawing.Point(1400, 175);
            this.list_execution.Name = "list_execution";
            this.list_execution.Size = new System.Drawing.Size(325, 563);
            this.list_execution.TabIndex = 3;
            this.list_execution.Visible = false;
            // 
            // btn_espandi
            // 
            this.btn_espandi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_espandi.Location = new System.Drawing.Point(1400, 744);
            this.btn_espandi.Name = "btn_espandi";
            this.btn_espandi.Size = new System.Drawing.Size(325, 31);
            this.btn_espandi.TabIndex = 6;
            this.btn_espandi.TabStop = false;
            this.btn_espandi.Text = "Espandi";
            this.btn_espandi.UseVisualStyleBackColor = true;
            this.btn_espandi.Visible = false;
            this.btn_espandi.Click += new System.EventHandler(this.btn_espandi_Click);
            // 
            // pan_ex
            // 
            this.pan_ex.Controls.Add(this.nud_delay);
            this.pan_ex.Controls.Add(this.cmb_execute);
            this.pan_ex.Controls.Add(this.btn_execute);
            this.pan_ex.Controls.Add(this.label7);
            this.pan_ex.Controls.Add(this.label6);
            this.pan_ex.Location = new System.Drawing.Point(1400, 50);
            this.pan_ex.Name = "pan_ex";
            this.pan_ex.Size = new System.Drawing.Size(325, 105);
            this.pan_ex.TabIndex = 19;
            // 
            // btn_esp_dat
            // 
            this.btn_esp_dat.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.btn_esp_dat.Location = new System.Drawing.Point(1261, 60);
            this.btn_esp_dat.Name = "btn_esp_dat";
            this.btn_esp_dat.Size = new System.Drawing.Size(89, 75);
            this.btn_esp_dat.TabIndex = 20;
            this.btn_esp_dat.TabStop = false;
            this.btn_esp_dat.Text = "Esecuzione";
            this.btn_esp_dat.UseVisualStyleBackColor = true;
            this.btn_esp_dat.Click += new System.EventHandler(this.btn_esp_Click);
            // 
            // frm_main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(1750, 800);
            this.Controls.Add(this.btn_esp_dat);
            this.Controls.Add(this.pan_ex);
            this.Controls.Add(this.btn_espandi);
            this.Controls.Add(this.list_execution);
            this.Controls.Add(this.dgv_main);
            this.Controls.Add(this.pan_data);
            this.Controls.Add(this.lbl_title);
            this.Name = "frm_main";
            this.Text = "Logistica - Angioletti";
            this.Load += new System.EventHandler(this.frm_main_Load);
            this.pan_data.ResumeLayout(false);
            this.pan_data.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_tot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_max)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_min)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_cons)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_prod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_delay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_main)).EndInit();
            this.pan_ex.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Label lbl_title;
        private Panel pan_data;
        private Button btn_gen;
        private Button btn_chnge_table;
        private NumericUpDown nud_cons;
        private NumericUpDown nud_prod;
        private Label label2;
        private Label label1;
        private DataGridView dgv_main;
        private NumericUpDown nud_max;
        private Label label4;
        private NumericUpDown nud_min;
        private Label label3;
        private CheckBox chk_random;
        private NumericUpDown nud_tot;
        private Label label5;
        private Button btn_execute;
        private Label label6;
        private ComboBox cmb_execute;
        private ListBox list_execution;
        private Button btn_espandi;
        private NumericUpDown nud_delay;
        private Label label7;
        private Panel pan_ex;
        private Button btn_esp_dat;
    }
}