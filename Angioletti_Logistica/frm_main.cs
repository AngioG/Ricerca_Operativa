using System.Data;
using System.Threading;
using System.Data.Common;
using System.Windows.Forms;

namespace Angioletti_Logistica
{

    public partial class frm_main : Form
    {
        Color color { get; set; }

        bool esecuzione { get; set; } = false;

        /*int saved_ncol { get; set; }
        int saved_nrows { get; set; }*/
        List<List<int>> saved_values { get; set; } = new List<List<int>>();

        public frm_main()
        {
            InitializeComponent();
#if DEBUG
            cmb_execute.SelectedIndex = 4;
#endif
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

            if(!chk_gen.Checked)
            {
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
            btn_execute.Enabled = true;
            btn_gen.Enabled = true;
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

                column.HeaderText = $"D{i + 1}";
                column.CellTemplate = dataGridViewCell;
                dgv_main.Columns.Add(column);
            }

            DataGridViewColumn last_column = new DataGridViewColumn();
            last_column.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            last_column.HeaderText = "Necessari";
            last_column.CellTemplate = dataGridViewCell;
            dgv_main.Columns.Add(last_column);
            #endregion
            #region rows
            for (int i = 0; i < columns; i++)
            {
                DataGridViewRow row = (DataGridViewRow)dgv_main.RowTemplate.Clone();
                row.CreateCells(dgv_main);

                row.HeaderCell.Value = $"Up{i + 1}";

                for (int x = 0; x < nud_cons.Value + 1; x++)
                {
                    row.Cells[x].Value = 0;
                    row.Cells[x].ValueType = typeof(int);
                }

                dgv_main.Rows.Add(row);
            }

            DataGridViewRow last_row = (DataGridViewRow)dgv_main.RowTemplate.Clone();
            last_row.CreateCells(dgv_main);
            last_row.HeaderCell.Value = $"Disponibili";
            for (int x = 0; x < nud_cons.Value + 1; x++)
            {
                last_row.Cells[x].Value = 0;
                last_row.Cells[x].ValueType = typeof(int);
            }
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
                    int n = rng.Next((int)nud_min.Value, (int)nud_max.Value + 1);
                    dgv_main.Rows[r].Cells[c].Value = n;
                }

            if (chk_random.Checked)
            {
                int min = (int)(dgv_main.Columns.Count > dgv_main.Rows.Count ? dgv_main.Columns.Count - 1 : dgv_main.Rows.Count - 1);
                min = (2 * min + 1) * 10;
                DatiCasuali(new Random().Next(min, 150 + 1));
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
                MessageBox.Show("Devi prima creare una tabella", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            for (int x = 0; x < dgv_main.Rows.Count; x++)
                for (int y = 0; y < dgv_main.Columns.Count; y++)
                    if ((int)dgv_main.Rows[x].Cells[y].Value == 0)
                    {
                        MessageBox.Show("La tabella non può contenere uno zero", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

            #region Controllo totali
            var col = 0;
            for (int i = 0; i < dgv_main.Rows.Count - 1; i++)
                col += (int)dgv_main.Rows[i].Cells[dgv_main.Columns.Count - 1].Value;

            var row = 0;
            for (int i = 0; i < dgv_main.Columns.Count - 1; i++)
                row += (int)dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[i].Value;

            if (col != row)
            {
                DialogResult dr = MessageBox.Show($"I valori dei totali non coincidono, aumentare casualmente la merce {(col > row ? "disponibile dei produttori" : "rischiesta dai consumatori")}?", "ATTENZIONE", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (dr == DialogResult.No || dr == DialogResult.Abort)
                    return;


                if (col > row)
                {
                    Random rng = new Random();

                    if ((col - row) % 10 != 0)
                    {
                        var RandomCell = dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[rng.Next(0, dgv_main.Columns.Count)];
                        RandomCell.Value = (int)RandomCell.Value + (col - row) % 10;

                        row += (col - row) % 10;
                    }


                    while (col != row)
                    {
                        var RandomCell = dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[rng.Next(0, dgv_main.Columns.Count)];
                        RandomCell.Value = (int)RandomCell.Value + 10;

                        row += 10;
                    }
                }
                else
                {
                    Random rng = new Random();

                    if ((row - col) % 10 != 0)
                    {
                        var RandomCell = dgv_main.Rows[rng.Next(0, dgv_main.Rows.Count)].Cells[dgv_main.Columns.Count - 1];
                        RandomCell.Value = (int)RandomCell.Value + (row - col) % 10;

                        col += (row - col) % 10;
                    }


                    while (col != row)
                    {
                        var RandomCell = dgv_main.Rows[rng.Next(0, dgv_main.Rows.Count - 1)].Cells[dgv_main.Columns.Count - 1];
                        RandomCell.Value = (int)RandomCell.Value + 10;

                        col += 10;
                    }
                }

                

            }
            dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value = col;

            Application.DoEvents();
            #endregion

            #region interfaccia
            this.ClientSize = new System.Drawing.Size(1750, 800);
            this.CenterToScreen();
            btn_espandi.Visible = true;
            list_execution.Visible = true;
            list_execution.Items.Clear();
            dgv_main.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgv_main.DefaultCellStyle.SelectionBackColor = dgv_main.DefaultCellStyle.BackColor;
            dgv_main.ClearSelection();

            cmb_execute.Enabled = false;
            btn_esp_dat.Enabled = false;
            #endregion

            save_table_datas();

            #region esecuzione
            switch (cmb_execute.SelectedIndex)
            {
                case 0:
                    {
                        await Task.Run(() => NordOvest());
                        Thread.Sleep(1000);
                        load_table_datas();

                        await Task.Run(() => MinimiCosti());
                        Thread.Sleep(1000);
                        load_table_datas();

                        await Task.Run(() => Vogel());
                        Thread.Sleep(1000);
                        load_table_datas();

                        await Task.Run(() => Russell());
                        Thread.Sleep(1000);
                        load_table_datas();

                        break;
                    }
                case 1:
                    {
                        await Task.Run(() => NordOvest());
                        Thread.Sleep(1000);
                        load_table_datas();
                        break;
                    }
                case 2:
                    {
                        await Task.Run(() => MinimiCosti());
                        Thread.Sleep(1000);
                        load_table_datas();
                        break;
                    }
                case 3:
                    {
                        await Task.Run(() => Vogel());
                        Thread.Sleep(1000);
                        load_table_datas();

                        break;
                    }
                case 4:
                    {
                        await Task.Run(() => Russell());
                        Thread.Sleep(1000);
                        load_table_datas();

                        break;
                    }
            }
            #endregion

            /*this.ClientSize = new System.Drawing.Size(1400, 800);*/
            cmb_execute.Enabled = true;

            btn_esp_dat.Enabled = true;
            btn_esp_dat.Enabled = true;
            dgv_main.DefaultCellStyle.SelectionForeColor = Color.White;
            dgv_main.DefaultCellStyle.SelectionBackColor = color;

        }

        public void save_table_datas()
        {

            saved_values = new List<List<int>>();
            for (int x = 0; x < dgv_main.Columns.Count; x++)
                saved_values.Add(new List<int>());

            for (int x = 0; x < dgv_main.Columns.Count; x++)
                for (int y = 0; y < dgv_main.Rows.Count; y++)
                    saved_values[x].Add((int)(dgv_main.Rows[y].Cells[x].Value));

        }

        public void load_table_datas(List<List<int>> datas = null)
        {
            if (datas == null)
                datas = saved_values;

            GenerateTable(datas.Count - 1, datas[0].Count - 1);

            for (int x = 0; x < datas[0].Count; x++)
                for (int y = 0; y < datas.Count; y++)
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


        #region algoritmi
        public void NordOvest()
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


            System.Threading.Thread.Sleep((int)nud_delay.Value / 2);

            do
            {
                dgv_main.ClearSelection();
                dgv_main.Invoke(new Action(() =>
                {
                    ColorCells(0, 0, true);
                }));

                System.Threading.Thread.Sleep((int)nud_delay.Value / 2);


                string cliente = dgv_main.Rows[0].HeaderCell.Value.ToString();
                string produttore = dgv_main.Columns[0].HeaderCell.Value.ToString();
                int prezzo = (int)dgv_main.Rows[0].Cells[0].Value;
                int val_c = (int)dgv_main.Rows[0].Cells[dgv_main.Columns.Count - 1].Value;
                int val_p = (int)dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[0].Value;
                int spesa = -1;

                if (val_p == val_c)
                {
                    spesa = prezzo * val_c;

                    dgv_main.Invoke(new Action(() =>
                    {
                        dgv_main.Rows.RemoveAt(0);
                        dgv_main.Columns.RemoveAt(0);

                        dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value = tot - val_c;
                        dgv_main.ClearSelection();
                    }));


                    list_execution.Invoke(new Action(() =>
                    {
                        list_execution.Items.Add($"{val_c} prodotti da {produttore} a {cliente} a costo {prezzo} per un totale di {spesa}");
                    }));
                }
                else if (val_p > val_c)
                {
                    spesa = prezzo * val_c;

                    dgv_main.Invoke(new Action(() =>
                    {
                        dgv_main.Rows.RemoveAt(0);
                        dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[0].Value = (int)dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[0].Value - val_c;

                        dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value = tot - val_c;
                        dgv_main.ClearSelection();
                    }));


                    list_execution.Invoke(new Action(() =>
                    {
                        list_execution.Items.Add($"{val_c} prodotti da {produttore} a {cliente} a costo {prezzo} per un totale di {spesa}");
                    }));
                }
                else if (val_c > val_p)
                {
                    spesa = prezzo * val_p;

                    dgv_main.Invoke(new Action(() =>
                    {
                        dgv_main.Columns.RemoveAt(0);
                        dgv_main.Rows[0].Cells[dgv_main.Columns.Count - 1].Value = (int)dgv_main.Rows[0].Cells[dgv_main.Columns.Count - 1].Value - val_p;

                        dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value = tot - val_p;
                        dgv_main.ClearSelection();
                    }));


                    list_execution.Invoke(new Action(() =>
                    {
                        list_execution.Items.Add($"{val_p} prodotti da {produttore} a {cliente} a costo {prezzo} per un totale di {spesa}");
                    }));
                }


                tot = (int)dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value;
                res += spesa;


                dgv_main.Invoke(new Action(() =>
                {
                    ColorCells(0, 0, false);
                }));
                System.Threading.Thread.Sleep((int)nud_delay.Value / 2);


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

        public void MinimiCosti()
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


            System.Threading.Thread.Sleep((int)nud_delay.Value / 2);

            do
            {
                dgv_main.ClearSelection();

                int X = 0;
                int Y = 0;

                for (int x = 0; x < dgv_main.Rows.Count - 1; x++)
                    for (int y = 0; y < dgv_main.Columns.Count - 1; y++)
                    {
                        if ((int)dgv_main.Rows[X].Cells[Y].Value < (int)dgv_main.Rows[x].Cells[y].Value)
                            continue;

                        if ((int)dgv_main.Rows[X].Cells[Y].Value > (int)dgv_main.Rows[x].Cells[y].Value)
                        {
                            X = x;
                            Y = y;
                            continue;
                        }

                        //Se i valori sono uguali si prende il valore più alto per eliminare una riga/colonna
                        int A = new int[] { (int)dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[Y].Value, (int)dgv_main.Rows[X].Cells[dgv_main.Columns.Count - 1].Value }.Max();

                        int a = new int[] { (int)dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[y].Value, (int)dgv_main.Rows[x].Cells[dgv_main.Columns.Count - 1].Value }.Max();

                        if (A > a)
                        {
                            X = x;
                            Y = y;
                        }

                    }



                dgv_main.Invoke(new Action(() =>
                {
                    ColorCells(X, Y, true);
                }));

                System.Threading.Thread.Sleep((int)nud_delay.Value / 2);

                //Fino a qui è giusto
                string cliente = dgv_main.Rows[X].HeaderCell.Value.ToString();
                string produttore = dgv_main.Columns[Y].HeaderCell.Value.ToString();
                int prezzo = (int)dgv_main.Rows[X].Cells[Y].Value;
                int val_c = (int)dgv_main.Rows[X].Cells[dgv_main.Columns.Count - 1].Value;
                int val_p = (int)dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[Y].Value;
                int spesa = -1;

                if (val_p == val_c)
                {
                    spesa = prezzo * val_c;

                    dgv_main.Invoke(new Action(() =>
                    {
                        dgv_main.Rows.RemoveAt(X);
                        dgv_main.Columns.RemoveAt(Y);

                        dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value = tot - val_c;
                        dgv_main.ClearSelection();
                    }));


                    list_execution.Invoke(new Action(() =>
                    {
                        list_execution.Items.Add($"{val_c} prodotti da {produttore} a {cliente} a costo {prezzo} per un totale di {spesa}");
                    }));
                }
                else if (val_p > val_c)
                {
                    spesa = prezzo * val_c;

                    dgv_main.Invoke(new Action(() =>
                    {
                        dgv_main.Rows.RemoveAt(X);
                        dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[Y].Value = (int)dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[Y].Value - val_c;

                        dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value = tot - val_c;
                        dgv_main.ClearSelection();
                    }));


                    list_execution.Invoke(new Action(() =>
                    {
                        list_execution.Items.Add($"{val_c} prodotti da {produttore} a {cliente} a costo {prezzo} per un totale di {spesa}");
                    }));
                }
                else if (val_c > val_p)
                {
                    spesa = prezzo * val_p;

                    dgv_main.Invoke(new Action(() =>
                    {
                        dgv_main.Columns.RemoveAt(Y);
                        dgv_main.Rows[X].Cells[dgv_main.Columns.Count - 1].Value = (int)dgv_main.Rows[X].Cells[dgv_main.Columns.Count - 1].Value - val_p;

                        dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value = tot - val_p;
                        dgv_main.ClearSelection();
                    }));


                    list_execution.Invoke(new Action(() =>
                    {
                        list_execution.Items.Add($"{val_p} prodotti da {produttore} a {cliente} a costo {prezzo} per un totale di {spesa}");
                    }));
                }


                tot = (int)dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value;
                res += spesa;


                dgv_main.Invoke(new Action(() =>
                {
                    ColorCells(X, Y, false);
                }));
                System.Threading.Thread.Sleep((int)nud_delay.Value / 2);


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

        public void Vogel()
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


            System.Threading.Thread.Sleep(((int)nud_delay.Value) / 2);

            do
            {
                dgv_main.ClearSelection();

                dgv_main.Invoke(new Action(() =>
                {
                    CalcolaScarti(first);
                }));
                System.Threading.Thread.Sleep(((int)nud_delay.Value) / 2);

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
                    ColorCells(X, Y, true);
                }));

                System.Threading.Thread.Sleep(((int)nud_delay.Value) / 2);

                string cliente = dgv_main.Rows[X].HeaderCell.Value.ToString();
                string produttore = dgv_main.Columns[Y].HeaderCell.Value.ToString();
                int prezzo = (int)dgv_main.Rows[X].Cells[Y].Value;
                int val_c = (int)dgv_main.Rows[X].Cells[dgv_main.Columns.Count - 2].Value;
                int val_p = (int)dgv_main.Rows[dgv_main.Rows.Count - 2].Cells[Y].Value;
                int spesa = -1;

                if (val_p == val_c)
                {
                    spesa = prezzo * val_c;

                    dgv_main.Invoke(new Action(() =>
                    {
                        dgv_main.Rows.RemoveAt(X);
                        dgv_main.Columns.RemoveAt(Y);

                        dgv_main.Rows[dgv_main.Rows.Count - 2].Cells[dgv_main.Columns.Count - 2].Value = tot - val_c;
                        dgv_main.ClearSelection();
                    }));

                    list_execution.Invoke(new Action(() =>
                    {
                        list_execution.Items.Add($"{val_c} prodotti da {produttore} a {cliente} a costo {prezzo} per un totale di {spesa}");
                    }));
                }
                else if (val_p > val_c)
                {
                    spesa = prezzo * val_c;

                    dgv_main.Invoke(new Action(() =>
                    {
                        dgv_main.Rows.RemoveAt(X);
                        dgv_main.Rows[dgv_main.Rows.Count - 2].Cells[Y].Value = (int)dgv_main.Rows[dgv_main.Rows.Count - 2].Cells[Y].Value - val_c;

                        dgv_main.Rows[dgv_main.Rows.Count - 2].Cells[dgv_main.Columns.Count - 2].Value = tot - val_c;
                        dgv_main.ClearSelection();
                    }));

                    list_execution.Invoke(new Action(() =>
                    {
                        list_execution.Items.Add($"{val_c} prodotti da {produttore} a {cliente} a costo {prezzo} per un totale di {spesa}");
                    }));
                }
                else if (val_c > val_p)
                {
                    spesa = prezzo * val_p;

                    dgv_main.Invoke(new Action(() =>
                    {
                        dgv_main.Columns.RemoveAt(Y);
                        dgv_main.Rows[X].Cells[dgv_main.Columns.Count - 2].Value = (int)dgv_main.Rows[X].Cells[dgv_main.Columns.Count - 2].Value - val_p;

                        dgv_main.Rows[dgv_main.Rows.Count - 2].Cells[dgv_main.Columns.Count - 2].Value = tot - val_p;
                        dgv_main.ClearSelection();
                    }));

                    list_execution.Invoke(new Action(() =>
                    {
                        list_execution.Items.Add($"{val_p} prodotti da {produttore} a {cliente} a costo {prezzo} per un totale di {spesa}");
                    }));
                }



                tot = (int)dgv_main.Rows[dgv_main.Rows.Count - 2].Cells[dgv_main.Columns.Count - 2].Value;
                res += spesa;


                dgv_main.Invoke(new Action(() =>
                {
                    ColorCells(X, Y, false);
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

            if (dgv_main.Columns.Count - 2 == 1)
            {
                for (int j = 0; j < dgv_main.Rows.Count - 2; j++)
                {
                    dgv_main.Rows[j].Cells[2].Value = dgv_main.Rows[j].Cells[0].Value;
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




            if (dgv_main.Rows.Count - 2 == 1)
            {
                for (int j = 0; j < dgv_main.Columns.Count - 2; j++)
                {
                    dgv_main.Rows[2].Cells[j].Value = dgv_main.Rows[0].Cells[j].Value;
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

        public async void Russell()
        {
            list_execution.Invoke(new Action(() =>
            {
                list_execution.Items.Add($"Risoluzione tramite metodo di Russell");
                list_execution.Items.Add("");
            }));


            int tot = (int)dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value;
            int res = 0;

            dgv_main.Invoke(new Action(() =>
            {
                dgv_main.DefaultCellStyle.BackColor = Color.White;
            }));

            var tab_vecchia = new List<List<int>>();

            System.Threading.Thread.Sleep(((int)nud_delay.Value) / 3);

            do
            {
               
                dgv_main.ClearSelection();

                #region Calcolo scarti
                List<CellData> Massimi = new List<CellData>();
                for (int x = 0; x < dgv_main.Rows.Count - 1; x++)
                {
                    int ym = 0;
                    for (int y = 1; y < dgv_main.Columns.Count - 1; y++)
                        if ((int)dgv_main.Rows[x].Cells[y].Value > (int)dgv_main.Rows[x].Cells[ym].Value)
                            ym = y;

                    Massimi.Add(new CellData(x, ym, (int)dgv_main.Rows[x].Cells[ym].Value));
                }

                for (int y = 0; y < dgv_main.Columns.Count - 1; y++)
                {
                    int xm = 0;
                    for (int x = 1; x < dgv_main.Rows.Count - 1; x++)
                        if ((int)dgv_main.Rows[x].Cells[y].Value > (int)dgv_main.Rows[xm].Cells[y].Value)
                            xm = x;

                    bool found = false;
                    foreach (var c in Massimi)
                        if (c.CompareTo(new CellData(xm, y, 0)) == 0)
                        {
                            found = true; break;
                        }
                    if(!found)
                    Massimi.Add(new CellData(xm, y, (int)dgv_main.Rows[xm].Cells[y].Value));
                }

                foreach (var c in Massimi)
                    list_execution.Invoke(new Action(() =>
                    {
                        dgv_main.Rows[c.x].Cells[c.y].Style.ForeColor = Color.Red;
                    }));
                #endregion

                System.Threading.Thread.Sleep(((int)nud_delay.Value) / 3);

                #region Salvataggio dati in lista
                tab_vecchia = new List<List<int>>();
                for (int x = 0; x < dgv_main.Columns.Count - 1; x++)
                    tab_vecchia.Add(new List<int>());

                for (int y = 0; y < dgv_main.Columns.Count - 1; y++)
                    for (int x = 0; x < dgv_main.Rows.Count - 1; x++)
                    {
                        tab_vecchia[y].Add((int)(dgv_main.Rows[x].Cells[y].Value));

                        int value = (int)(dgv_main.Rows[x].Cells[y].Value);
                        value -= Massimi.Where(c => c.y == y).Max(c => c.Value);
                        value -= Massimi.Where(c => c.x == x).Max(c => c.Value);

                        list_execution.Invoke(new Action(() =>
                            {
                                dgv_main.Rows[x].Cells[y].Value = value;
                            }));


                        list_execution.Invoke(new Action(() =>
                        {
                            dgv_main.Rows[x].Cells[y].Style.ForeColor = Color.Black;
                        }));
                    }
                #endregion

                int X = 0;
                int Y = 0;
                int min = 1;

                for (int y = 0; y < dgv_main.Columns.Count - 1; y++)
                    for (int x = 0; x < dgv_main.Rows.Count - 1; x++)
                        if ((int)dgv_main.Rows[x].Cells[y].Value < min)
                        {
                            min = (int)dgv_main.Rows[x].Cells[y].Value;
                            X = x;
                            Y = y;
                        }




                dgv_main.Invoke(new Action(() =>
                {
                    ColorCells(X, Y, true);
                }));

                System.Threading.Thread.Sleep(((int)nud_delay.Value) / 3);

                string cliente = dgv_main.Rows[X].HeaderCell.Value.ToString();
                string produttore = dgv_main.Columns[Y].HeaderCell.Value.ToString();
                int prezzo = tab_vecchia[Y][X];
                int val_c = (int)dgv_main.Rows[X].Cells[dgv_main.Columns.Count - 1].Value;
                int val_p = (int)dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[Y].Value;
                int spesa = -1;

                if (val_p == val_c)
                {
                    spesa = prezzo * val_c;

                    dgv_main.Invoke(new Action(() =>
                    {
                        dgv_main.Rows.RemoveAt(X);
                        dgv_main.Columns.RemoveAt(Y);

                        dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value = tot - val_c;
                        dgv_main.ClearSelection();
                    }));

                    foreach (var column in tab_vecchia)
                        column.RemoveAt(X);

                    tab_vecchia.RemoveAt(Y);

                    list_execution.Invoke(new Action(() =>
                    {
                        list_execution.Items.Add($"{val_c} prodotti da {produttore} a {cliente} a costo {prezzo} per un totale di {spesa}");
                    }));
                }
                else if (val_p > val_c)
                {
                    spesa = prezzo * val_c;

                    dgv_main.Invoke(new Action(() =>
                    {
                        dgv_main.Rows.RemoveAt(X);
                        dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[Y].Value = (int)dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[Y].Value - val_c;

                        dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value = tot - val_c;
                        dgv_main.ClearSelection();
                    }));

                    foreach (var column in tab_vecchia)
                        column.RemoveAt(X);


                    list_execution.Invoke(new Action(() =>
                    {
                        list_execution.Items.Add($"{val_c} prodotti da {produttore} a {cliente} a costo {prezzo} per un totale di {spesa}");
                    }));
                }
                else if (val_c > val_p)
                {
                    spesa = prezzo * val_p;

                    dgv_main.Invoke(new Action(() =>
                    {
                        dgv_main.Columns.RemoveAt(Y);
                        dgv_main.Rows[X].Cells[dgv_main.Columns.Count - 1].Value = (int)dgv_main.Rows[X].Cells[dgv_main.Columns.Count - 1].Value - val_p;

                        dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value = tot - val_p;
                        dgv_main.ClearSelection();
                    }));

                    tab_vecchia.RemoveAt(Y);

                    list_execution.Invoke(new Action(() =>
                    {
                        list_execution.Items.Add($"{val_p} prodotti da {produttore} a {cliente} a costo {prezzo} per un totale di {spesa}");
                    }));
                }

                
                

                tot = (int)dgv_main.Rows[dgv_main.Rows.Count - 1].Cells[dgv_main.Columns.Count - 1].Value;
                res += spesa;


                dgv_main.Invoke(new Action(() =>
                {
                    ColorCells(X, Y, false);
                }));



                for (int x = 0; x < dgv_main.Rows.Count - 1; x++)
                    for (int y = 0; y < dgv_main.Columns.Count - 1; y++)
                        dgv_main.Invoke(new Action(() =>
                        {
                            dgv_main.Rows[x].Cells[y].Value = tab_vecchia[y][x];
                        }));


                var breakpoint = 0;


            } while (tot != 0);



            list_execution.Invoke(new Action(() =>
            {
                list_execution.Items.Add("");
                list_execution.Items.Add($"Spesa totale utilizzando il metodo di Russell: {res}");
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
            if (dgv_main.Rows[e.RowIndex].Cells[e.ColumnIndex].Value == null)
                dgv_main.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = 1;

            if (!int.TryParse(dgv_main.Rows[e.RowIndex].Cells[e.ColumnIndex].Value.ToString(), out int n))
            {
                MessageBox.Show("Devi inserire un intero senza segno", "Errore", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dgv_main.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = (new Random()).Next((int)nud_min.Value, (int)nud_max.Value);
                return;
            }



            if (e.RowIndex != dgv_main.Rows.Count - 1 && e.ColumnIndex != dgv_main.Columns.Count - 1)
            {
                if (n > (int)nud_max.Value)
                    dgv_main.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = (int)nud_max.Value;

                if (n < (int)nud_min.Value)
                    dgv_main.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = (int)nud_min.Value;

                return;
            }

        }
        #endregion

        private void btn_espandi_Click(object sender, EventArgs e)
        {
            (new frm_vis(list_execution)).ShowDialog();
        }

        private void btn_esp_Click(object sender, EventArgs e)
        {
            if (!esecuzione)
            {
                


                this.ClientSize = new System.Drawing.Size(1750, 800);
                this.CenterToScreen();

                btn_esp_dat.Text = "Modifica dati";
                list_execution.Visible = true;
                pan_ex.Visible = true;
                btn_espandi.Visible = true;
                list_execution.Items.Clear();

                /*dgv_main.ReadOnly = true;
                dgv_main.ClearSelection();

                pan_data.Enabled = false;*/

                esecuzione = true;
            }
            else
            {
                this.ClientSize = new System.Drawing.Size(1450, 800);
                this.CenterToScreen();

                btn_esp_dat.Text = "Esecuzione";
                list_execution.Visible = false;
                btn_espandi.Visible = false;
                pan_ex.Visible = false;

                dgv_main.ReadOnly = false;
                dgv_main.ClearSelection();

                pan_data.Enabled = true;

                esecuzione = false;
            }
        }
    }

    public class CellData : IComparable<CellData>
    {
        public int x { get; set; }
        public int y { get; set; }
        public int Value { get; set; }

        public CellData(int x, int y, int value)
        {
            this.x = x;
            this.y = y;
            Value = value;
        }

        public int CompareTo(CellData? other)
        {
            if (other == null) return -1;

            if (x == other.x && y == other.y) return 0;

            return 1;
        }
    }
}