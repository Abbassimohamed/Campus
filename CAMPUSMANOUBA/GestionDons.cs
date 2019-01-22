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
    public partial class GestionDons : DevExpress.XtraEditors.XtraForm
    {
        public GestionDons()
        {
            InitializeComponent();
        }
     
   
           Cumulentreedon cumlentdon;
            Cumulsortiedon cumlsortdon;

        private static GestionDons instance;
        public static GestionDons Instance()
        {
            if (instance == null)

                instance = new GestionDons();

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
            //try
            //{
                if (cumlsortdon == null)
                {
                    splashScreenManager1.ShowWaitForm();
                    cumlsortdon = Cumulsortiedon.Instance();
                    cumlsortdon.MdiParent = this;
                    cumlsortdon.Show();
                    cumlsortdon.BringToFront();
                    splashScreenManager1.CloseWaitForm();
                }
            
           // }
               
           //catch(Exception exc)
           // { }


        }

     

        private void tileItem3_ItemClick(object sender, TileItemEventArgs e)
        {
            //try
            //{


                
                    splashScreenManager1.ShowWaitForm();
                    cumlentdon = Cumulentreedon.Instance();
                    cumlentdon.MdiParent = this;
                    cumlentdon.Show();
                    cumlentdon.BringToFront();
                    splashScreenManager1.CloseWaitForm();
              
                

             
            //}catch(Exception exc)
            //{ }

        }

        private void tileItem4_ItemClick(object sender, TileItemEventArgs e)
        {
            //try
            //{


                    splashScreenManager1.ShowWaitForm();
                    cumlsortdon = Cumulsortiedon.Instance();
                    cumlsortdon.MdiParent = this;
                    cumlsortdon.Show();
                    cumlsortdon.BringToFront();
                    splashScreenManager1.CloseWaitForm();
             
            //}
            //catch (Exception exc)
            //{ }

            
        }

        private void tileItem6_ItemClick(object sender, TileItemEventArgs e)
        {
            //try
            //{
                if (cumlentdon == null)
                {
                    splashScreenManager1.ShowWaitForm();
                    cumlentdon = Cumulentreedon.Instance();
                    cumlentdon.MdiParent = this;
                    cumlentdon.Show();
                    cumlentdon.BringToFront();
                    splashScreenManager1.CloseWaitForm();
                }
           
            //}
            //catch (Exception exc)
            //{ }
        }

        private void tileItem2_ItemClick_1(object sender, TileItemEventArgs e)
        {
            //try
            //{


                if (cumlsortdon == null)
                {
                    splashScreenManager1.ShowWaitForm();
                    cumlsortdon = Cumulsortiedon.Instance();
                    cumlsortdon.MdiParent = this;
                    cumlsortdon.Show();
                    cumlsortdon.BringToFront();
                    splashScreenManager1.CloseWaitForm();
                }

               
            //}
            //catch (Exception exc)
            //{ }

          
        }

        private void GestionDons_Load(object sender, EventArgs e)
        {
           
        }

        private void tileItem7_ItemClick(object sender, TileItemEventArgs e)
        {
            this.Close();
          
        }
    }
}