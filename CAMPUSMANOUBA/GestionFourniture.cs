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
    public partial class GestionFourniture : DevExpress.XtraEditors.XtraForm
    {
        public GestionFourniture()
        {
            InitializeComponent();
        }
     
        private static ConsulterFourniture listfour;
         private static NewFourniture newfr;
           private static CumulEntreefr cumlent;
           private static CumulSortiefr cumlsort;

        private static GestionFourniture instance;
        public static GestionFourniture Instance()
        {
            if (instance == null)

                instance = new GestionFourniture();

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
            newfr = NewFourniture.Instance();
            newfr.MdiParent = this;
            newfr.Show();
            newfr.BringToFront();
            splashScreenManager1.CloseWaitForm();
        }

        private void tileItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            listfour = ConsulterFourniture.Instance();
            listfour.MdiParent = this;
            listfour.Show();
            listfour.BringToFront();
            splashScreenManager1.CloseWaitForm();
        }

        private void tileItem3_ItemClick(object sender, TileItemEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            cumlent = CumulEntreefr.Instance();
            cumlent.MdiParent = this;
            cumlent.Show();
            cumlent.BringToFront();
            splashScreenManager1.CloseWaitForm();
        }

        private void tileItem4_ItemClick(object sender, TileItemEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            cumlsort = CumulSortiefr.Instance();
            cumlsort.MdiParent = this;
            cumlsort.Show();
            cumlsort.BringToFront();
            splashScreenManager1.CloseWaitForm();
        }

        private void GestionFourniture_Load(object sender, EventArgs e)
        {

        }

        private void tileItem5_ItemClick(object sender, TileItemEventArgs e)
        {
            this.Close();
           
        }
    }
}