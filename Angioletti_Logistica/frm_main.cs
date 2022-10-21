using System.Data;
using System.Threading;
using System.Data.Common;
using System.Windows.Forms;

namespace Angioletti_Logistica
{
    public partial class frm_main : Form
    {
        Color color { get; set; }

        int saved_ncol { get; set; }
        int saved_nrows { get; set; }
        List<List<int>> saved_values { get; set; } = new List<List<int>>();

        public frm_main()
        {
            InitializeComponent();
        }

        private void frm_main_Load(object sender, EventArgs e)
        {
            this.ClientSize = new System.Drawing.Size(1400, 800);
            cmb_execute.SelectedIndex = 0;
            color = dgv_main.DefaultCellStyle.SelectionBackColor;
            dgv_main.DefaultCellStyle.SelectionForeColor = Color.White;
        }

        #region  input
        private void btn_chnge_table_Click(object sender, EventArgs e)
        {
            GenerateTable((int)nud_cons.Value, (int)nud_prod.Value);

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
            btn_execute.Enabled = true;
        }

        public void GenerateTable(int rows, int columns)
        {
            #region setup
            dgv_main.Columns.Clear();
            dgv_main.Rows.Clear();
            DataGridViewCell dataGridViewCell = new DataGridViewTextBoxCell();
            #endregion
            #region columns

            for (int i = 0; i < rows; i++)
            {
                DataGridViewColumn column = new DataGridViewColumn();
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                column.HeaderText = $"P{i + 1}";
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
            for (int i = 0; i < columns; i++)
            {
                DataGridViewRow row = (DataGridViewRow)dgv_main.RowTemplate.Clone();
                row.CreateCells(dgv_main);

                row.HeaderCell.Value = $"Up{i + 1}";

                for (int x = 0; x < nud_cons.Value; x++)
                {
                    row.Cells[x].Value = 1;
                    row.Cells[x].ValueType = typeof(int);
                }

                dgv_main.Rows.Add(row);
            }

            DataGridViewRow last_row = (DataGridViewRow)dgv_main.RowTemplate.Clone();
            last_row.CreateCells(dgv_main);
            last_row.HeaderCell.Value = $"Disponibili";
            last_row.ReadOnly = true;
            dgv_main.Rows.Add(last_row);
            #endregion
        }

        public void DatiCasuali(int Tot)
        {

            dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value = Tot * 10;
            Random rng = new Random();

            #region consumatori
            int[] dati_u = new int[dgv_main.Columns.Count - 1];
            int Tot_u = Tot + dgv_main.Columns.Count - 2;


            for (int i = 0; i < dati_u.Count(); i++)
            {
                int a = 0;
                do
                {
                    a = rng.Next(1, Tot_u);
                } while (dati_u.Contains(a) || dati_u.Contains(a + 1) || dati_u.Contains(a - 1));

                dati_u[i] = a;
            }


            dati_u = dati_u.OrderBy(n => n).ToArray();

            for (int i = 0; i < dgv_main.Columns.Count - 2; i++)
                dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[i].Value = (dati_u[i] - 1 - (i == 0 ? 0 : dati_u[i - 1])) * 10;
            dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 2].Value = (Tot_u - dati_u[dati_u.Count() - 2]) * 10;
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
                } while (dati_p.Contains(a) || dati_p.Contains(a + 1) || dati_p.Contains(a - 1));

                dati_p[i] = a;
            }


            dati_p = dati_p.OrderBy(n => n).ToArray();

            for (int i = 0; i < dgv_main.Rows.Count - 2; i++)
                dgv_main.Rows[i].Cells[dgv_main.Columns.Count - 1].Value = (dati_p[i] - 1 - (i == 0 ? 0 : dati_p[i - 1])) * 10;
            dgv_main.Rows[dgv_main.Rows.Count - 2].Cells[dgv_main.Columns.Count - 1].Value = (Tot_p - dati_p[dati_p.Count() - 2]) * 10;
            #endregion
        }

        private void Conflitto_nud(object sender, EventArgs e)
        {
            if (sender == null || sender.GetType() != typeof(NumericUpDown))
                return;

            NumericUpDown nud = sender as NumericUpDown;

            if (nud.Name == "nud_min")
            {
                if (nud.Value >= nud_max.Value)
                    nud_max.Value = nud.Value + 1;
            }

            if (nud.Name == "nud_max")
            {
                if (nud.Value <= nud_min.Value)
                    nud_min.Value = nud.Value - 1;
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

        private void btn_gen_Click(object sender, EventArgs e)
        {

            if (dgv_main.Columns.Count < 3 || dgv_main.Rows.Count < 3)
            {
                MessageBox.Show("Si è verificato un errore, prova a generare una nuova tabella", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                btn_gen.Enabled = false;
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
        }
        #endregion

        #region esecuzione
        private void btn_execute_Click(object sender, EventArgs e)
        {
            if (dgv_main.Rows.Count <= 2 || dgv_main.Columns.Count <= 2)
            {
                btn_execute.Enabled = false;
                return;
            }

            #region interfaccia
            this.CenterToScreen();
            this.ClientSize = new System.Drawing.Size(1750, 800);
            btn_espandi.Visible = true;
            list_execution.Visible = true;
            list_execution.Items.Clear();
            dgv_main.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv_main.DefaultCellStyle.SelectionBackColor = dgv_main.DefaultCellStyle.BackColor;
            dgv_main.ClearSelection();
            Lock_interface(true);
            #endregion

            save_table_datas();

            #region esecuzione
            switch (cmb_execute.SelectedIndex)
            {
                case 0:
                    {
                        Thread T_ex = new Thread(() => NordOvest((int)nud_delay.Value));
                        T_ex.Start();
                        do
                        {
                            Application.DoEvents();
                        } while (T_ex.ThreadState == ThreadState.Running);
                        Thread.Sleep(1000);
                        load_table_datas();
                        break;
                    }
                case 1:
                    {
                        NordOvest((int)nud_delay.Value);
                        break;
                    }
            }
            #endregion

            /*this.ClientSize = new System.Drawing.Size(1400, 800);*/
            Lock_interface(false);
            btn_execute.Enabled = false;
            dgv_main.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv_main.DefaultCellStyle.SelectionBackColor = color;

        }

        public void save_table_datas()
        {
            saved_ncol = dgv_main.Columns.Count - 1;
            saved_nrows = dgv_main.Rows.Count - 1;

            saved_values = new List<List<int>>();
            for (int x = 0; x < dgv_main.Columns.Count; x++)
                saved_values.Add(new List<int>());

            for (int x = 0; x < dgv_main.Columns.Count; x++)
                for (int y = 0; y < dgv_main.Rows.Count; y++)
                    saved_values[x].Add((int)dgv_main.Rows[y].Cells[x].Value);

        }

        public void load_table_datas()
        {
            GenerateTable(saved_ncol, saved_nrows);

            for (int x = 0; x < saved_nrows + 1; x++)
                for (int y = 0; y < saved_ncol + 1; y++)
                    dgv_main.Rows[x].Cells[y].Value = saved_values[y][x];
        }

        public void ColorCells(int rowIndex, int columnsIndex, bool active)
        {
            Color color = active ? Color.Khaki : Color.White;
            Color header_color = active ? Color.DarkKhaki : Color.White;
            Color fore_color = active ? Color.Red : Color.Black;

            dgv_main.ClearSelection();
            dgv_main.Columns[columnsIndex].HeaderCell.Style.BackColor = header_color;
            dgv_main.Rows[rowIndex].HeaderCell.Style.BackColor = header_color;

            dgv_main.Columns[columnsIndex].DefaultCellStyle.BackColor = color;
            dgv_main.Rows[rowIndex].DefaultCellStyle.BackColor = color;

            dgv_main.Rows[0].Cells[0].Style.ForeColor = fore_color;

            Application.DoEvents();
        }

        public void Lock_interface(bool enable)
        {
            foreach (Control a in Controls)
            {
                a.Enabled = !enable;
            }

            dgv_main.Enabled = true;
            dgv_main.ReadOnly = enable;
            list_execution.Enabled = true;
        }

        #region algoritmi
        public void NordOvest(int delay = 1000)
        {
            list_execution.Invoke(new Action(() =>
            {
                list_execution.Items.Add($"Risoluzione tramite metodo del nord ovest");
                list_execution.Items.Add("");
            }));


            int tot = (int)dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value;
            int res = 0;

            dgv_main.DefaultCellStyle.BackColor = Color.White;
            Application.DoEvents();
            System.Threading.Thread.Sleep(delay / 2);

            do
            {

                ColorCells(0, 0, true);

                System.Threading.Thread.Sleep(delay / 2);


                string fornitore = dgv_main.Rows[0].HeaderCell.Value.ToString();
                string produttore = dgv_main.Columns[0].HeaderCell.Value.ToString();
                int prezzo = (int)dgv_main.Rows[0].Cells[0].Value;
                int val_f = (int)dgv_main.Rows[0].Cells[dgv_main.Columns.Count - 1].Value;
                int val_p = (int)dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[0].Value;
                int spesa = -1;

                if (val_p == val_f)
                {
                    spesa = prezzo * val_f;

                    dgv_main.Invoke(new Action(() =>
                    {
                        dgv_main.Rows.RemoveAt(0);
                        dgv_main.Columns.RemoveAt(0);

                        dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value = tot - val_f;
                    }));
                }
                else if (val_p > val_f)
                {
                    spesa = prezzo * val_f;

                    dgv_main.Invoke(new Action(() =>
                    {
                        dgv_main.Rows.RemoveAt(0);
                        dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[0].Value = (int)dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[0].Value - val_f;

                        dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value = tot - val_f;
                    }));
                }
                else if (val_f > val_p)
                {
                    spesa = prezzo * val_p;

                    dgv_main.Invoke(new Action(() =>
                    {
                        dgv_main.Columns.RemoveAt(0);
                        dgv_main.Rows[0].Cells[dgv_main.Columns.Count - 1].Value = (int)dgv_main.Rows[0].Cells[dgv_main.Columns.Count - 1].Value - val_p;

                        dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value = tot - val_p;
                    }));
                }

                list_execution.Invoke(new Action(() =>
                {
                    list_execution.Items.Add($"{val_f} prodotti da {produttore} a {fornitore} a costo {prezzo} per un totale di {spesa}");
                }));

                tot = (int)dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value;
                res += spesa;

                ColorCells(0, 0, false);
                System.Threading.Thread.Sleep(delay / 2);


            } while (tot != 0);

            list_execution.Invoke(new Action(() =>
            {
                list_execution.Items.Add("");
                list_execution.Items.Add($"Spesa totale utilizzando il metodo del nord ovest: {res}");
                list_execution.Items.Add("");
                list_execution.Items.Add("_______________________________________________________________________________________________________");
                list_execution.Items.Add("");
            }));

        }
        #endregion
        #endregion

        #region editing celle
        private void dgv_main_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            dgv_main.EditingControl.KeyPress += new KeyPressEventHandler(EditingControl_KeyDown);
        }

        void EditingControl_KeyDown(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;

        }

        private void dgv_main_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (!uint.TryParse(dgv_main.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out uint n))
            {
                MessageBox.Show("Devi inserire un intero senza segno", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dgv_main.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = (new Random()).Next((int)nud_min.Value, (int)nud_max.Value);
            }
        }
        #endregion

        private void btn_espandi_Click(object sender, EventArgs e)
        {
            (new frm_vis(list_execution)).ShowDialog();
        }

    }
}