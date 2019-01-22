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
    public partial class GestionDeVente : DevExpress.XtraEditors.XtraForm
    {
        private static Gestioncommande bc;
      
        
        private static Gestionbonlivraison newbl;
        private static Gestionfacturation gestfact;
        private static Gestionavoir gestavoir;
        private static ReglementForm reglement;
        private static GestionDeVente instance;
        public static GestionDeVente Instance()
        {
            if (instance == null)

                instance = new GestionDeVente();

            return instance;

        }

        public GestionDeVente()
        {
            InitializeComponent();
        }

        private void tileItem3_ItemClick(object sender, TileItemEventArgs e)
        {
           try
            {

                splashScreenManager1.ShowWaitForm();
                bc = Gestioncommande.Instance();
                    bc.MdiParent = this;
                    bc.Show();
                    bc.BringToFront();
                splashScreenManager1.CloseWaitForm();

            }

            catch (Exception exc)
            { }



        }

  

      
        private void tileItem4_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {

                splashScreenManager1.ShowWaitForm();
                newbl = Gestionbonlivraison.Instance();
                newbl.MdiParent = this;
                newbl.Show();
                newbl.BringToFront();
                splashScreenManager1.CloseWaitForm();

            }

            catch (Exception exc)
            { }

          
           
        }

    
        
        private void tileItem6_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {

                splashScreenManager1.ShowWaitForm();
                gestfact = Gestionfacturation.Instance();
                gestfact.MdiParent = this;
                gestfact.Show();
                gestfact.BringToFront();
                splashScreenManager1.CloseWaitForm();

            }

            catch (Exception exc)
            { }


          
         
        }

        private void GestionDeVente_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void tileItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            this.Close();
           
        }

        private void tileItem5_ItemClick(object sender, TileItemEventArgs e)
        {
            //try
            //{

                splashScreenManager1.ShowWaitForm();
                gestavoir = Gestionavoir.Instance();
                gestavoir.MdiParent = this;
                gestavoir.Show();
                gestavoir.BringToFront();
                splashScreenManager1.CloseWaitForm();

            //}

            //catch (Exception exc)
            //{ }

        }

        private void tileItem7_ItemClick(object sender, TileItemEventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            reglement = ReglementForm.Instance();
            reglement.MdiParent = this;
            reglement.Show();
            reglement.BringToFront();
            splashScreenManager1.CloseWaitForm();
        }
    }
}