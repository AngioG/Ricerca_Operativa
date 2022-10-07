namespace Angioletti_Logistica
{
    public partial class frm_main : Form
    {
        public frm_main()
        {
            InitializeComponent();
        }

        private void frm_main_Load(object sender, EventArgs e)
        {
        }

        private void btn_chnge_table_Click(object sender, EventArgs e)
        {
            int x = 0;
            int y = 0;
            while (x < nud_cons.Value)
            {
                DataGridViewColumn col = new DataGridViewColumn();

                col.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                col.Name = $"col{x+1}";
                col.HeaderText = $"D{x+1}";

                dgv_main.Columns.Add(col);

                x++;
            }

            while(y < nud_prod.Value)
            {
                dgv_main.Rows.Add();

                y++;
            }
        }
    }
}