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
    public partial class GestionReglement : DevExpress.XtraEditors.XtraForm
    {
        public GestionReglement()
        {
            InitializeComponent();
        }
        private static NewClient newclt;
            private static Consultclt listclt;
        private static GestionReglement instance;
        public static GestionReglement Instance()
        {
            if (instance == null)

                instance = new GestionReglement();

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
            newclt = NewClient.Instance();
            newclt.MdiParent = this;
            newclt.Show();
            newclt.BringToFront();
            splashScreenManager1.CloseWaitForm();
        }

        private void tileItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            listclt = Consultclt.Instance();
            listclt.MdiParent = this;
            listclt.Show();
            listclt.BringToFront();
            splashScreenManager1.CloseWaitForm();
        }

        private void tileItem3_ItemClick(object sender, TileItemEventArgs e)
        {
            this.Close();
          
        }
    }
}