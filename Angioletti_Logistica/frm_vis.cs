﻿using System;
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
        private ListBox _dati { get; }

        public frm_vis()
        {
            InitializeComponent();
        }

        public frm_vis(ListBox list)
        {
            _dati = list;
            InitializeComponent();
        }

        private void frm_vis_Load(object sender, EventArgs e)
        {
            foreach (var item in _dati.Items)
                list_main.Items.Add(item);
        }
    }
}
