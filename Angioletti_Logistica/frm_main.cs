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
        private async void btn_execute_Click(object sender, EventArgs e)
        {
            if (dgv_main.Rows.Count <= 2 || dgv_main.Columns.Count <= 2)
            {
                btn_execute.Enabled = false;
                return;
            }

            #region interfaccia
            this.ClientSize = new System.Drawing.Size(1750, 800);
            this.CenterToScreen();
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
                        await Task.Run(() => NordOvest((int)nud_delay.Value));
                        Thread.Sleep(1000);
                        load_table_datas();

                        await Task.Run(() => MinimiCosti((int)nud_delay.Value));
                        Thread.Sleep(1000);
                        load_table_datas();

                        await Task.Run(() => Vogel((int)nud_delay.Value));
                        Thread.Sleep(1000);
                        load_table_datas();

                        break;
                    }
                case 1:
                    {
                        await Task.Run(() => NordOvest((int)nud_delay.Value));
                        Thread.Sleep(1000);
                        load_table_datas();
                        break;
                    }
                case 2:
                    {
                        await Task.Run(() => MinimiCosti((int)nud_delay.Value));
                        Thread.Sleep(1000);
                        load_table_datas();
                        break;
                    }
                case 3:
                    {
                        await Task.Run(() => Vogel((int)nud_delay.Value));
                        Thread.Sleep(1000);
                        load_table_datas();

                        break;
                    }
            }
            #endregion

            /*this.ClientSize = new System.Drawing.Size(1400, 800);*/
            Lock_interface(false);
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

        public void ColorCells(int rowIndex, int columnIndex, bool active)
        {
            Color color = active ? Color.Khaki : Color.White;
            Color header_color = active ? Color.DarkKhaki : Color.White;
            Color fore_color = active ? Color.Red : Color.Black;

            dgv_main.Columns[columnIndex].HeaderCell.Style.BackColor = header_color;
            dgv_main.Rows[rowIndex].HeaderCell.Style.BackColor = header_color;

            dgv_main.Columns[columnIndex].DefaultCellStyle.BackColor = color;
            dgv_main.Rows[rowIndex].DefaultCellStyle.BackColor = color;

            dgv_main.Rows[rowIndex].Cells[columnIndex].Style.ForeColor = fore_color;

            dgv_main.ClearSelection();
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

            dgv_main.Invoke(new Action(() =>
            {
                dgv_main.DefaultCellStyle.BackColor = Color.White;
            }));


            System.Threading.Thread.Sleep(delay / 2);

            do
            {
                dgv_main.ClearSelection();
                dgv_main.Invoke(new Action(() =>
                {
                    ColorCells(0, 0, true);
                }));

                System.Threading.Thread.Sleep(delay / 2);


                string cliente = dgv_main.Rows[0].HeaderCell.Value.ToString();
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
                        dgv_main.ClearSelection();
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
                        dgv_main.ClearSelection();
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
                        dgv_main.ClearSelection();
                    }));
                }

                list_execution.Invoke(new Action(() =>
                {
                    list_execution.Items.Add($"{val_f} prodotti da {produttore} a {cliente} a costo {prezzo} per un totale di {spesa}");
                }));

                tot = (int)dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value;
                res += spesa;


                dgv_main.Invoke(new Action(() =>
                {
                    ColorCells(0, 0, false);
                }));
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

        public void MinimiCosti(int delay = 1000)
        {
            list_execution.Invoke(new Action(() =>
            {
                list_execution.Items.Add($"Risoluzione tramite metodo dei minimi costi");
                list_execution.Items.Add("");
            }));


            int tot = (int)dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value;
            int res = 0;

            dgv_main.Invoke(new Action(() =>
            {
                dgv_main.DefaultCellStyle.BackColor = Color.White;
            }));


            System.Threading.Thread.Sleep(delay / 2);

            do
            {
                dgv_main.ClearSelection();

                int X = 0;
                int Y = 0;

                for (int x = 0; x < dgv_main.Rows.Count - 1; x++)
                    for (int y = 0; y < dgv_main.Columns.Count - 1; y++)
                    {
                        if ((int)dgv_main.Rows[Y].Cells[X].Value < (int)dgv_main.Rows[x].Cells[y].Value)
                            continue;

                        if ((int)dgv_main.Rows[Y].Cells[X].Value > (int)dgv_main.Rows[y].Cells[x].Value)
                        {
                            X = x;
                            Y = y;
                            continue;
                        }

                        //Se i valori sono uguali si prende il valore più alto per eliminare una riga/colonna
                        int A = new int[] { (int)dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[X].Value, (int)dgv_main.Rows[Y].Cells[dgv_main.Columns.Count - 1].Value }.Max();

                        int a = new int[] { (int)dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[x].Value, (int)dgv_main.Rows[y].Cells[dgv_main.Columns.Count - 1].Value }.Max();

                        if (A > a)
                        {
                            X = x;
                            Y = y;
                        }

                    }

                dgv_main.Invoke(new Action(() =>
                {
                    ColorCells(Y, X, true);
                }));

                System.Threading.Thread.Sleep(delay / 2);

                //Fino a qui è giusto
                string cliente = dgv_main.Rows[Y].HeaderCell.Value.ToString();
                string produttore = dgv_main.Columns[X].HeaderCell.Value.ToString();
                int prezzo = (int)dgv_main.Rows[Y].Cells[X].Value;
                int val_f = (int)dgv_main.Rows[Y].Cells[dgv_main.Columns.Count - 1].Value;
                int val_p = (int)dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[X].Value;
                int spesa = -1;

                if (val_p == val_f)
                {
                    spesa = prezzo * val_f;

                    dgv_main.Invoke(new Action(() =>
                    {
                        dgv_main.Rows.RemoveAt(Y);
                        dgv_main.Columns.RemoveAt(X);

                        dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value = tot - val_f;
                        dgv_main.ClearSelection();
                    }));
                }
                else if (val_p > val_f)
                {
                    spesa = prezzo * val_f;

                    dgv_main.Invoke(new Action(() =>
                    {
                        dgv_main.Rows.RemoveAt(Y);
                        dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[X].Value = (int)dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[X].Value - val_f;

                        dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value = tot - val_f;
                        dgv_main.ClearSelection();
                    }));
                }
                else if (val_f > val_p)
                {
                    spesa = prezzo * val_p;

                    dgv_main.Invoke(new Action(() =>
                    {
                        dgv_main.Columns.RemoveAt(X);
                        dgv_main.Rows[Y].Cells[dgv_main.Columns.Count - 1].Value = (int)dgv_main.Rows[Y].Cells[dgv_main.Columns.Count - 1].Value - val_p;

                        dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value = tot - val_p;
                        dgv_main.ClearSelection();
                    }));
                }

                list_execution.Invoke(new Action(() =>
                {
                    list_execution.Items.Add($"{val_f} prodotti da {produttore} a {cliente} a costo {prezzo} per un totale di {spesa}");
                }));

                tot = (int)dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value;
                res += spesa;


                dgv_main.Invoke(new Action(() =>
                {
                    ColorCells(Y, X, false);
                }));
                System.Threading.Thread.Sleep(delay / 2);


            } while (tot != 0);

            list_execution.Invoke(new Action(() =>
            {
                list_execution.Items.Add("");
                list_execution.Items.Add($"Spesa totale utilizzando il metodo dei minimi costi: {res}");
                list_execution.Items.Add("");
                list_execution.Items.Add("_______________________________________________________________________________________________________");
                list_execution.Items.Add("");
            }));

        }

        public void Vogel(int delay = 1000)
        {

            list_execution.Invoke(new Action(() =>
            {
                list_execution.Items.Add($"Risoluzione tramite metodo di Vogel");
                list_execution.Items.Add("");
            }));


            int tot = (int)dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value;
            bool first = true;
            int res = 0;

            dgv_main.Invoke(new Action(() =>
            {
                dgv_main.DefaultCellStyle.BackColor = Color.White;
            }));


            System.Threading.Thread.Sleep(delay / 2);

            do
            {
                dgv_main.ClearSelection();

                dgv_main.Invoke(new Action(() =>
                {
                    CalcolaScarti(first);
                }));
                System.Threading.Thread.Sleep(delay / 2);

                first = false;

                int X = -1;
                int Y = -1;
                int scarto = 0;

                for (int x = 0; x < dgv_main.Rows.Count - 2; x++)
                {
                    int val = (int)dgv_main.Rows[x].Cells[dgv_main.Columns.Count - 1].Value;

                    if (val > scarto)
                    {
                        scarto = val;

                        X = x;
                    }

                }

                for (int y = 0; y < dgv_main.Columns.Count - 2; y++)
                {
                    int val = (int)dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[y].Value;

                    if (val > scarto)
                    {
                        scarto = val;

                        X = -1;
                        Y = y;
                    }

                }


                if (X == -1)
                {
                    int min = -1;

                    for (int x = 0; x < dgv_main.Columns.Count - 2; x++)
                    {
                        if ((int)dgv_main.Rows[x].Cells[Y].Value < min || min == -1)
                        {
                            min = (int)dgv_main.Rows[x].Cells[Y].Value;
                            X = x;
                        }
                    }
                }
                else
                {
                    int min = -1;

                    for (int y = 0; y < dgv_main.Columns.Count - 2; y++)
                    {
                        if ((int)dgv_main.Rows[X].Cells[y].Value < min || min == -1)
                        {
                            min = (int)dgv_main.Rows[X].Cells[y].Value;
                            Y = y;
                        }
                    }
                }


                dgv_main.Invoke(new Action(() =>
                {
                    ColorCells(Y, X, true);
                }));

                System.Threading.Thread.Sleep(delay / 2);

                string cliente = dgv_main.Rows[Y].HeaderCell.Value.ToString();
                string produttore = dgv_main.Columns[X].HeaderCell.Value.ToString();
                int prezzo = (int)dgv_main.Rows[Y].Cells[X].Value;
                int val_f = (int)dgv_main.Rows[Y].Cells[dgv_main.Columns.Count - 2].Value;
                int val_p = (int)dgv_main.Rows[dgv_main.Rows.Count - 2].Cells[X].Value;
                int spesa = -1;

                if (val_p == val_f)
                {
                    spesa = prezzo * val_f;

                    dgv_main.Invoke(new Action(() =>
                    {
                        dgv_main.Rows.RemoveAt(Y);
                        dgv_main.Columns.RemoveAt(X);

                        dgv_main.Rows[dgv_main.Rows.Count - 2].Cells[dgv_main.Columns.Count - 2].Value = tot - val_f;
                        dgv_main.ClearSelection();
                    }));
                }
                else if (val_p > val_f)
                {
                    spesa = prezzo * val_f;

                    dgv_main.Invoke(new Action(() =>
                    {
                        dgv_main.Rows.RemoveAt(Y);
                        dgv_main.Rows[dgv_main.Rows.Count - 2].Cells[X].Value = (int)dgv_main.Rows[dgv_main.Rows.Count - 2].Cells[X].Value - val_f;

                        dgv_main.Rows[dgv_main.Rows.Count - 2].Cells[dgv_main.Columns.Count - 2].Value = tot - val_f;
                        dgv_main.ClearSelection();
                    }));
                }
                else if (val_f > val_p)
                {
                    spesa = prezzo * val_p;

                    dgv_main.Invoke(new Action(() =>
                    {
                        dgv_main.Columns.RemoveAt(X);
                        dgv_main.Rows[Y].Cells[dgv_main.Columns.Count - 2].Value = (int)dgv_main.Rows[Y].Cells[dgv_main.Columns.Count - 2].Value - val_p;

                        dgv_main.Rows[dgv_main.Rows.Count - 2].Cells[dgv_main.Columns.Count - 2].Value = tot - val_p;
                        dgv_main.ClearSelection();
                    }));
                }

                list_execution.Invoke(new Action(() =>
                {
                    list_execution.Items.Add($"{val_f} prodotti da {produttore} a {cliente} a costo {prezzo} per un totale di {spesa}");
                }));

                tot = (int)dgv_main.Rows[dgv_main.Rows.Count - 2].Cells[dgv_main.Columns.Count - 2].Value;
                res += spesa;


                dgv_main.Invoke(new Action(() =>
                {
                    ColorCells(Y, X, false);
                }));



            } while (tot != 0);

            list_execution.Invoke(new Action(() =>
            {
                list_execution.Items.Add("");
                list_execution.Items.Add($"Spesa totale utilizzando il metodo di Vogel: {res}");
                list_execution.Items.Add("");
                list_execution.Items.Add("_______________________________________________________________________________________________________");
                list_execution.Items.Add("");
            }));

        }

        public void CalcolaScarti(bool add = false)
        {
            Application.DoEvents();
            if (add)
            {
                DataGridViewColumn column = new DataGridViewColumn();
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                column.HeaderText = $"Scarti";
                column.CellTemplate = new DataGridViewTextBoxCell();
                column.ReadOnly = true;
                dgv_main.Columns.Add(column);


                DataGridViewRow row = (DataGridViewRow)dgv_main.RowTemplate.Clone();
                row.CreateCells(dgv_main);
                row.HeaderCell.Value = $"Scarti";

                for (int x = 0; x < nud_cons.Value; x++)
                {
                    row.Cells[x].Value = 1;
                    row.Cells[x].ValueType = typeof(int);
                }
                row.ReadOnly = true;


                dgv_main.Rows.Add(row);
                Application.DoEvents();
            }



            if (dgv_main.Rows.Count - 2 == 1)
            {
                for (int j = 0; j < dgv_main.Columns.Count - 2; j++)
                {
                    dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[j].Value = dgv_main.Rows[2].Cells[j].Value;
                    Application.DoEvents();
                }

            }
            else for (int i = 0; i < dgv_main.Rows.Count - 2; i++)
            {

                int min1 = -1;
                int min2 = -1;

                for (int j = 0; j < dgv_main.Columns.Count - 2; j++)
                {
                    if ((int)dgv_main.Rows[i].Cells[j].Value < min1 || min1 == -1)
                    {
                        min2 = min1;
                        min1 = (int)dgv_main.Rows[i].Cells[j].Value;
                    }
                    else if ((int)dgv_main.Rows[i].Cells[j].Value < min2 || min2 == -1)
                    {
                        min2 = (int)dgv_main.Rows[i].Cells[j].Value;
                    }


                }

                dgv_main.Rows[i].Cells[dgv_main.Columns.Count - 1].Value = min2 - min1;
                Application.DoEvents();
            }


            if (dgv_main.Columns.Count - 2 == 1)
            {
                for (int j = 0; j < dgv_main.Rows.Count - 2; j++)
                {
                    dgv_main.Rows[dgv_main.Columns.Count - 1].Cells[j].Value = dgv_main.Rows[j].Cells[2].Value;
                    Application.DoEvents();
                }

            }
            else for (int j = 0; j < dgv_main.Columns.Count - 2; j++)
                {

                    int min1 = -1;
                    int min2 = -1;

                    for (int i = 0; i < dgv_main.Rows.Count - 2; i++)
                    {
                        if ((int)dgv_main.Rows[i].Cells[j].Value < min1 || min1 == -1)
                        {
                            min2 = min1;
                            min1 = (int)dgv_main.Rows[i].Cells[j].Value;
                        }
                        else if ((int)dgv_main.Rows[i].Cells[j].Value < min2 || min2 == -1)
                        {
                            min2 = (int)dgv_main.Rows[i].Cells[j].Value;
                        }


                    }

                    dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[j].Value = min2 - min1;
                    Application.DoEvents();
                }


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