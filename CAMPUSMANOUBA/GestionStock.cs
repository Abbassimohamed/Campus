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
    public partial class GestionStock : DevExpress.XtraEditors.XtraForm
    {
        public GestionStock()
        {
            InitializeComponent();
        }
        private static NewArticle newart;
            private static ConsultArt listart;
        private static CumulEntree cument;
        private static Invetappro inappro;
        private static GestionStock instance;
   
        public static GestionStock Instance()
        {
            if (instance == null)

                instance = new GestionStock();

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
            newart = NewArticle.Instance();
            newart.MdiParent = this;
            newart.Show();
            newart.BringToFront();
            splashScreenManager1.CloseWaitForm();
        }

        private void tileItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            listart = ConsultArt.Instance();
            listart.MdiParent = this;
            listart.Show();
            listart.BringToFront();
            splashScreenManager1.CloseWaitForm();
        }

        private void tileItem3_ItemClick(object sender, TileItemEventArgs e)
        {
            familyadd fmadd = new familyadd();
            fmadd.ShowDialog();
        }

        private void tileItem5_ItemClick(object sender, TileItemEventArgs e)
        {
            underfamilyadd unfmadd = new underfamilyadd();
            unfmadd.ShowDialog();
        }

        private void tileItem4_ItemClick(object sender, TileItemEventArgs e)
        {
            Empmanage empmng = new Empmanage();
            empmng.ShowDialog();
        }

        private void tileItem6_ItemClick(object sender, TileItemEventArgs e)
        {
            this.Close();
        
        }

        private void tileItem7_ItemClick(object sender, TileItemEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            cument = CumulEntree.Instance();
            cument.MdiParent = this;
            cument.WindowState = FormWindowState.Maximized;
            cument.Show();
            cument.BringToFront();
            splashScreenManager1.CloseWaitForm();
        }

        private void tileItem8_ItemClick(object sender, TileItemEventArgs e)
        {

            splashScreenManager1.ShowWaitForm();
            inappro = Invetappro.Instance();
            inappro.MdiParent = this;
            inappro.WindowState = FormWindowState.Maximized;
            inappro.Show();
            inappro.BringToFront();
            splashScreenManager1.CloseWaitForm();
        }
    }
}