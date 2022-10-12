using System.Data;
using System.Windows.Forms;

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
            #region setup
            dgv_main.Columns.Clear();
            DataGridViewCell dataGridViewCell = new DataGridViewTextBoxCell();
            dgv_main.AutoGenerateColumns = false;
            #endregion
            #region columns

            for (int i = 0; i < nud_cons.Value; i++)
            {
                DataGridViewColumn column = new DataGridViewColumn();
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                column.HeaderText = $"P{i+1}";
                column.CellTemplate = dataGridViewCell;
                dgv_main.Columns.Add(column);
            }

            DataGridViewColumn last_column = new DataGridViewColumn();
            last_column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            last_column.HeaderText = "Necessari";
            last_column.CellTemplate = dataGridViewCell;
            last_column.ReadOnly = true;
            dgv_main.Columns.Add(last_column);
            #endregion
            #region rows
            dgv_main.RowHeadersWidth = dgv_main.Columns[0].Width;

            for (int i = 0; i < nud_prod.Value; i++)
            {
                DataGridViewRow row = (DataGridViewRow) dgv_main.RowTemplate.Clone();
                row.CreateCells(dgv_main);

                row.HeaderCell.Value = $"Up{i + 1}";

                for (int x = 0; x < nud_cons.Value; x++)
                {
                    row.Cells[x].Value = 1;
                }

                dgv_main.Rows.Add(row);
            }

            DataGridViewRow last_row = (DataGridViewRow)dgv_main.RowTemplate.Clone();
            last_row.CreateCells(dgv_main);
            last_row.HeaderCell.Value = $"Disponibili";
            last_row.ReadOnly = true;
            dgv_main.Rows.Add(last_row);
            #endregion
            #region genera dati
            if (dgv_main.Columns.Count == 0)
            {
                MessageBox.Show("Devi prima creare una tabella", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Random rng = new Random();

            for (int r = 0; r < dgv_main.Rows.Count - 1; r++)
                for (int c = 0; c < dgv_main.Columns.Count - 1; c++)
                {
                    int n = rng.Next((int)nud_min.Value, (int)nud_max.Value);
                    dgv_main.Rows[r].Cells[c].Value = n;
                }

            if (chk_random.Checked)
            {
                int min = (int)(dgv_main.Columns.Count > dgv_main.Rows.Count ? dgv_main.Columns.Count - 1 : dgv_main.Rows.Count - 1);
                min = (2 * min + 1) * 10;
                DatiCasuali(new Random().Next(min, 150));
            }
            else
            {
                DatiCasuali((int)nud_tot.Value / 10);
            }
            #endregion
        }

        public void DatiCasuali(int Tot)
        {
            dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value = Tot*10;
            Random rng = new Random();

            #region consumatori
            int[] dati_u = new int[dgv_main.Columns.Count-1];
            int Tot_u = Tot + dgv_main.Columns.Count - 2;

            
            for (int i = 0; i < dati_u.Count(); i++)
            {
                int a = 0;
                do
                {
                    a = rng.Next(1, Tot_u);
                } while (dati_u.Contains(a) || dati_u.Contains(a+1) || dati_u.Contains(a-1));

                dati_u[i] = a;
            }


            dati_u = dati_u.OrderBy(n => n).ToArray();

            for (int i = 0; i < dgv_main.Columns.Count - 2; i++)
                dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[i].Value = (dati_u[i] - 1 - (i == 0 ? 0 : dati_u[i-1]))*10;
            dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 2].Value = (Tot_u - dati_u[dati_u.Count() - 2])*10;
            #endregion
            #region produttori
            int[] dati_p = new int[dgv_main.Rows.Count - 1];
            int Tot_p = Tot + dgv_main.Rows.Count - 2;


            for (int i = 0; i < dati_p.Count(); i++)
            {
                int a = 0;
                do
                {
                    a = rng.Next(1, Tot_p);
                } while (dati_p.Contains(a) || dati_p.Contains(a+1) || dati_p.Contains(a-1));

                dati_p[i] = a;
            }


            dati_p = dati_p.OrderBy(n => n).ToArray();

            for (int i = 0; i < dgv_main.Rows.Count - 2; i++)
                dgv_main.Rows[i].Cells[dgv_main.Columns.Count - 1].Value = (dati_p[i] - 1 - (i == 0 ? 0 : dati_p[i - 1]))*10;
            dgv_main.Rows[dgv_main.Rows.Count - 2].Cells[dgv_main.Columns.Count - 1].Value = (Tot_p - dati_p[dati_p.Count() - 2])*10;
            #endregion
        }

        private void Conflitto_nud(object sender, EventArgs e)
        {
            if (sender == null || sender.GetType() != typeof(NumericUpDown))
                return;

            NumericUpDown nud = sender as NumericUpDown;

            if(nud.Name == "nud_min")
            {
                if(nud.Value >= nud_max.Value)
                    nud_max.Value = nud.Value + 1;
            }

            if (nud.Name == "nud_max")
            {
                if (nud.Value <= nud_min.Value)
                    nud_min.Value = nud.Value - 1;
            }
        }

        private void btn_gen_Click(object sender, EventArgs e)
        {
            if(dgv_main.Columns.Count == 0)
            {
                MessageBox.Show("Devi prima creare una tabella", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Random rng = new Random();

            for (int r = 0; r < dgv_main.Rows.Count-1; r++)
                for (int c = 0; c < dgv_main.Columns.Count-1; c++)        
                {
                    int n = rng.Next((int)nud_min.Value, (int)nud_max.Value);
                    dgv_main.Rows[r].Cells[c].Value = n;
                }

            if (chk_random.Checked)
            {
                int min = (int)(dgv_main.Columns.Count > dgv_main.Rows.Count ? dgv_main.Columns.Count - 1 : dgv_main.Rows.Count - 1);
                min = (2 * min + 1) * 10;
                DatiCasuali(new Random().Next(min, 150));
            }
            else
            {
                DatiCasuali((int)nud_tot.Value / 10);
            }
        }

        private void chk_random_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_random.Checked)
                nud_tot.Enabled = false;
            else
                nud_tot.Enabled = true;
        }

        private void change_tot_max(object sender, EventArgs e)
        {
            int max = (int)(nud_prod.Value > nud_cons.Value ? nud_prod.Value : nud_cons.Value);

            max = (2 * max + 1) * 10;

            if (nud_tot.Value < max)
                nud_tot.Value = max;
        }

        private void nud_tot_ValueChanged(object sender, EventArgs e)
        {
            /*nud_prod.Maximum = Math.Floor(nud_tot.Value / 20);
            nud_cons.Maximum = Math.Floor(nud_tot.Value / 20);*/
            var val = nud_tot.Value;

            if (nud_prod.Value >= Math.Floor(val / 20))
                nud_prod.Value = Math.Floor(val / 20);

            if (nud_cons.Value >= Math.Floor(val / 20))
                nud_cons.Value = Math.Floor(val / 20);

            nud_tot.Value = val;
        }
    }
}