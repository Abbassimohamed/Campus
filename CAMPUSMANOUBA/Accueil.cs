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
    public partial class Accueil : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Accueil()
        {
            InitializeComponent();
        }

        public static Consultfr consultfournisseur = Consultfr.Instance();
        public static NewFr newfr = NewFr.Instance();
        public static ModifierFr mdfr = ModifierFr.Instance();
        public static NewClient newcl = NewClient.Instance();
        public static ModifierClient modcl = ModifierClient.Instance();

        
        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (newcl == null)
            {
                newcl = new NewClient();
                newcl.MdiParent = Accueil.ActiveForm;
                newcl.Show();
                newcl.BringToFront();
            }
            else
            {

                newcl.MdiParent = Accueil.ActiveForm;
                newcl.Show();
                newcl.BringToFront();
            }
        }

        private void Accueil_Load(object sender, EventArgs e)
        {

        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (consultfournisseur == null)
            {
                consultfournisseur = new Consultfr();
                consultfournisseur.MdiParent = Accueil.ActiveForm;
                consultfournisseur.Show();
                consultfournisseur.BringToFront();
            }
            else
            {

                consultfournisseur.MdiParent = Accueil.ActiveForm;
                consultfournisseur.Show();
                consultfournisseur.BringToFront();
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if ( newfr== null)
            {
                newfr = new NewFr();
                newfr.MdiParent = Accueil.ActiveForm;
                newfr.Show();
                newfr.BringToFront();
            }
            else
            {

                newfr.MdiParent = Accueil.ActiveForm;
                newfr.Show();
                newfr.BringToFront();
            }
        }

        private void Catégorie_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void xtraScrollableControl1_Click(object sender, EventArgs e)
        {

        }
    }
}