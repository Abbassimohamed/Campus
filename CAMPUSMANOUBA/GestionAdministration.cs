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
    public partial class GestionAdministration : DevExpress.XtraEditors.XtraForm
    {
        public GestionAdministration()
        {
            InitializeComponent();
        }
        private static GestionsUtilisateurs usergesta;
            private static Consultclt listclt;
        private static GestionSociete gestsoc ;
        private static GestionAdministration instance;
        public static GestionAdministration Instance()
        {
            if (instance == null)

                instance = new GestionAdministration();

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
            gestsoc = GestionSociete.Instance();
            gestsoc.MdiParent = this;
            gestsoc.Show();
            gestsoc.BringToFront();
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

        private void tileItem4_ItemClick(object sender, TileItemEventArgs e)
        {
            
            splashScreenManager1.ShowWaitForm();
            usergesta = GestionsUtilisateurs.Instance();
            usergesta.MdiParent = this;
            usergesta.Show();
            usergesta.BringToFront();
            splashScreenManager1.CloseWaitForm();
        }

        private void tileControl1_Click(object sender, EventArgs e)
        {

        }

        private void tileItem5_ItemClick(object sender, TileItemEventArgs e)
        {

        }
    }
}