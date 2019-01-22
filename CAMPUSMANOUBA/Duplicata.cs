using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace CAMPUSMANOUBA
{
    public partial class Duplicata : DevExpress.XtraEditors.XtraForm
    {
        public Duplicata()
        {
            InitializeComponent();
        }
        private static Duplicata instance;
        public static Duplicata Instance()
        {
            if (instance == null)

                instance = new Duplicata();

            return instance;

        }

        private void Duplicata_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void Duplicata_Activated(object sender, EventArgs e)
        {
          
        }
    }
}