using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace CAMPUSMANOUBA
{
    public partial class ModifierFr : DevExpress.XtraEditors.XtraForm
    {
        public ModifierFr()
        {
            InitializeComponent();
        }

        private static ModifierFr instance;

        public static ModifierFr Instance()
        {
            if (instance == null)

                instance = new ModifierFr();
            return instance;

        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

        }

        private void ModifierFr_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }
    }
}