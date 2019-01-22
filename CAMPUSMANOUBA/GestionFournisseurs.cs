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
    public partial class GestionFournisseurs : DevExpress.XtraEditors.XtraForm
    {
        public GestionFournisseurs()
        {
            InitializeComponent();
        }
        private static NewFr newfr;
            private static Consultfr listfr;
        private static GestionFournisseurs instance;
        public static GestionFournisseurs Instance()
        {
            if (instance == null)

                instance = new GestionFournisseurs();

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

        private void tileItem1_ItemClick(object sender, TileItemEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            newfr = NewFr.Instance();
            newfr.MdiParent = this;
            newfr.Show();
            newfr.BringToFront();
            splashScreenManager1.CloseWaitForm();
        }

        private void tileItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            listfr = Consultfr.Instance();
            listfr.MdiParent = this;
            listfr.Show();
            listfr.BringToFront();
            splashScreenManager1.CloseWaitForm();
        }

        private void tileItem3_ItemClick(object sender, TileItemEventArgs e)
        {
            this.Close();
          
        }
    }
}