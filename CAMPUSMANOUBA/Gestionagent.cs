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
    public partial class Gestionagent : DevExpress.XtraEditors.XtraForm
    {
        public Gestionagent()
        {
            InitializeComponent();
        }
        private static Nouveau_agent newclt;
        private static Consultclt listclt;
        private static Gestionagent instance;
        public static Gestionagent Instance()
        {
            if (instance == null)

                instance = new Gestionagent();

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
            newclt = Nouveau_agent.Instance();
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