using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Angioletti_Logistica
{
    public partial class frm_vis : Form
    {

        public frm_vis()
        {
            InitializeComponent();
        }

        public frm_vis(ListBox list)
        {
            InitializeComponent();

            refresh_table(list);
        }

        public void refresh_table(ListBox list)
        {
            list_main.Items.Clear();

            foreach (var item in list.Items)
            {
                if (item.ToString().Length > 0 && int.TryParse(item.ToString().Substring(0, 1), out _))
                    list_main.Items.Add("Trasportati " + item.ToString());
                else
                    list_main.Items.Add(item);
            }

            int visibleItems = list_main.ClientSize.Height / list_main.ItemHeight;
            list_main.TopIndex = Math.Max(list_main.Items.Count - visibleItems + 1, 0);
        }

        private void frm_vis_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.Form.Espandi = null;
        }
    }
}
