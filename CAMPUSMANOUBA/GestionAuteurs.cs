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
    public partial class GestionAuteurs : DevExpress.XtraEditors.XtraForm
    {
        public GestionAuteurs()
        {
            InitializeComponent();
        }
        private static NewAuthor newauteur;
            private static Consutaut listaut;
        private static GestionAuteurs instance;
        public static GestionAuteurs Instance()
        {
            if (instance == null)

                instance = new GestionAuteurs();

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
            newauteur = NewAuthor.Instance();
            newauteur.MdiParent = this;
            newauteur.Show();
            newauteur.BringToFront();
            splashScreenManager1.CloseWaitForm();
        }

        private void tileItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            listaut = Consutaut.Instance();
            listaut.MdiParent = this;
            listaut.Show();
            listaut.BringToFront();
            splashScreenManager1.CloseWaitForm();
        }

        private void tileItem3_ItemClick(object sender, TileItemEventArgs e)
        {
            this.Close();
          
        }
    }
}