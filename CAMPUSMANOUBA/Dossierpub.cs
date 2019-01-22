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
    public partial class Dossierpub : DevExpress.XtraEditors.XtraForm
    {
        public Dossierpub()
        {
            InitializeComponent();
        }
        private static Fichepublication fichepub;
    
        private static Dossierpub instance;
        public static Dossierpub Instance()
        {
            if (instance == null)

                instance = new Dossierpub();

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
            fichepub.xtraTabControl1.SelectedTabPage = fichepub.xtraTabPage1;
            splashScreenManager1.CloseWaitForm();
        }

        private void tileItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            fichepub.xtraTabControl1.SelectedTabPage = fichepub.xtraTabPage2;
            splashScreenManager1.CloseWaitForm();
        }

        private void tileItem3_ItemClick(object sender, TileItemEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            fichepub.xtraTabControl1.SelectedTabPage = fichepub.xtraTabPage3;
            splashScreenManager1.CloseWaitForm();
        }

        private void Dossierpub_Load(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            fichepub = Fichepublication.Instance();
            fichepub.MdiParent = this;
            fichepub.Show();
            fichepub.BringToFront();

            splashScreenManager1.CloseWaitForm();
        }

        private void tileItem4_ItemClick(object sender, TileItemEventArgs e)
        {
            this.Close();
        }
    }
}