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
using BLL;
using DAL;
namespace CAMPUSMANOUBA
{
    public partial class Accueiltile : DevExpress.XtraEditors.XtraForm
    {
        private int hr, min, sec;
        private static Gestionfacturation gestfact;
        private static Gestionbonlivraison newbl;
        public static GestionDeVente sale;
        public static Duplicata dup;
        public static GestionAuteurs autmanage;
        public static GestionFournisseurs gestfr;
        public static Gestionclient gestclt;
        public static Gestionagent gestagent;
        public static GestionFourniture newfour;
        public static GestionDons gdon;
        public static Dossierpub dpub;
        public static GestionStock geststock;
        public static GestionAdministration admingesta;
        public static GestionCompta gestcompta;
        public static Cumulentreedon cmlentdon;
        public static Cumulsortiedon cmlsordon;
        public static Rapportextraitnew rapp;
        public static Gestiondevis gestdev;
        public static Invetappro inv;
     UserService userser = new UserService();

        public Accueiltile()
        {
            InitializeComponent();
            timer1.Start();
        }
        
        private static Accueiltile instance;

        public static Accueiltile Instance()
        {
            if (instance == null)

                instance = new Accueiltile();
            return instance;

        }
        private void tileItem6_ItemClick(object sender, TileItemEventArgs e)
        {
            gestfr = GestionFournisseurs.Instance();
            if (gestfr.WindowState == FormWindowState.Minimized)
            {
                gestfr.WindowState = FormWindowState.Maximized;
            }
            else

                gestfr.Show();
        }

        private void tileItem1_ItemClick(object sender, TileItemEventArgs e)
        {
            dup = Duplicata.Instance();
            if (dup.WindowState == FormWindowState.Minimized)
            {
                dup.WindowState = FormWindowState.Maximized;
            }
            else

                dup.Show();
           
        }

        private void tileItem14_ItemClick(object sender, TileItemEventArgs e)
        {
          
        }

        private void tileItem16_ItemClick(object sender, TileItemEventArgs e)
        {
            geststock = GestionStock.Instance();
            if (geststock.WindowState == FormWindowState.Minimized)
            {
                geststock.WindowState = FormWindowState.Maximized;
            }
            else

                geststock.Show();

        }

        private void tileItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            sale= GestionDeVente.Instance();
            if (sale.WindowState ==FormWindowState.Minimized)
            {
                sale.WindowState = FormWindowState.Maximized;
            }
            else

                sale.Show();
           
        
        }

        private void tileItem4_ItemClick(object sender, TileItemEventArgs e)
        {
            autmanage = GestionAuteurs.Instance();
            if (autmanage.WindowState == FormWindowState.Minimized)
            {
                autmanage.WindowState = FormWindowState.Maximized;
            }
            else

                autmanage.Show();

        }

        private void tileItem5_ItemClick(object sender, TileItemEventArgs e)
        {
            gestclt = Gestionclient.Instance();
            if (gestclt.WindowState == FormWindowState.Minimized)
            {
                gestclt.WindowState = FormWindowState.Maximized;
            }
            else

                gestclt.Show();
        }

        private void tileItem3_ItemClick(object sender, TileItemEventArgs e)
        {
            Rapportextraitnew rpcl = new Rapportextraitnew();
            rpcl.ShowDialog();
        }

        private void Accueiltile_Load(object sender, EventArgs e)
        {
            Droit dr1 = new Droit();
            dr1 = userser.getuserbycode(Login.codedroit);
            tileItem19.Text =" Bonjour Mr/Mme " +dr1.nom+" "+dr1.prenom;
           
            if(dr1.utilisabilite != "Administrateur")
            {
                
          if(dr1.GestClt == "false")
            {
                tileItem5.Visible = false;
            }
            if (dr1.GestAut == "false")
            {
                    tileItem4.Visible = false;
                }
            if (dr1.GestSt == "false")
            {
                    tileItem16.Visible = false;
                }
            if (dr1.GestDup == "false")
            {
                    tileItem1.Visible = false;
                }
            if (dr1.GestStat == "false")
            {
                    tileItem14.Visible = false;
                }
            if (dr1.GestRapp == "false")
            {
                    tileItem3.Visible = false;
                }
            if (dr1.GestCompta == "false")
            {
                    tileItem26.Visible = false;
                }
            if (dr1.GestPub == "false")
            {
                    tileItem21.Visible = false;
                }

            if (dr1.GestAdmin == "false")
            {
                    tileItem15.Visible = false;
                }
            if (dr1.GestFr == "false")
            {
                    tileItem6.Visible = false;
                }
            if (dr1.GestVente == "false")
            {
                    tileItem2.Visible = false;
                }
                if (dr1.GestDon== "false")
                {
                    tileItem25.Visible = false;
                }
                if (dr1.GestFourniture == "false")
                {
                    tileItem22.Visible = false;
                }
                if (dr1.GestDevis == "false")
                {
                    tileItem20.Visible = false;
                }
            }
        }

        private void tileItem10_ItemClick(object sender, TileItemEventArgs e)
        {
            
            rapp = Rapportextraitnew.Instance();
            if (rapp.WindowState == FormWindowState.Minimized)
            {
                rapp.WindowState = FormWindowState.Maximized;
            }
            else

                rapp.Show();
            rapp.WindowState = FormWindowState.Maximized;
           

        }

        private void tileItem11_ItemClick(object sender, TileItemEventArgs e)
        {
            try
            {
                
                gestfact = Gestionfacturation.Instance();
                if (gestfact.WindowState == FormWindowState.Minimized)
                {
                    gestfact.WindowState = FormWindowState.Maximized;
                }
                else

                    gestfact.Show();
                gestfact.WindowState = FormWindowState.Maximized;
               
            }

            catch (Exception exc)
            { }
            


        }

        private void tileItem19_ItemClick(object sender, TileItemEventArgs e)
        {
            popupMenu1.ShowPopup(this.barManager1, Control.MousePosition);
        }

        private void tileItem22_ItemClick(object sender, TileItemEventArgs e)
        {
          
            newfour = GestionFourniture.Instance();
            if (newfour.WindowState == FormWindowState.Minimized)
            {
                newfour.WindowState = FormWindowState.Maximized;
            }
            else

                newfour.Show();
            newfour.WindowState = FormWindowState.Maximized;
           
        }

        private void tileItem25_ItemClick(object sender, TileItemEventArgs e)
        {
            gdon = GestionDons.Instance();
            if (gdon.WindowState == FormWindowState.Minimized)
            {
                gdon.WindowState = FormWindowState.Maximized;
            }
            else

                gdon.Show();
            gdon.WindowState = FormWindowState.Maximized;
            
        }

        private void tileItem21_ItemClick(object sender, TileItemEventArgs e)
        {
            
            dpub = Dossierpub.Instance();
            if (dpub.WindowState == FormWindowState.Minimized)
            {
                dpub.WindowState = FormWindowState.Maximized;
            }
            else

                dpub.Show();
            dpub.WindowState = FormWindowState.Maximized;
            
        }

        private void tileItem8_ItemClick(object sender, TileItemEventArgs e)
        {

            try
            {
                
                newbl = Gestionbonlivraison.Instance();
            if (newbl.WindowState == FormWindowState.Minimized)
            {
                newbl.WindowState = FormWindowState.Maximized;
            }
            else

            newbl.Show();
            newbl.WindowState = FormWindowState.Maximized;
                
            }

            catch (Exception exc)
            { }
        }

        private void tileItem24_ItemClick(object sender, TileItemEventArgs e)
        {
            this.Close();
        }

        private void tileItem15_ItemClick(object sender, TileItemEventArgs e)
        {
          
            admingesta = GestionAdministration.Instance();
            if (admingesta.WindowState == FormWindowState.Minimized)
            {
                admingesta.WindowState = FormWindowState.Maximized;
            }
            else

                admingesta.Show();
            admingesta.WindowState = FormWindowState.Maximized;
            

        }

        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelControl1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login log = new Login();
            log.Show();

        }

        private void Accueiltile_FormClosing(object sender, FormClosingEventArgs e)
        {
            MessageBox.Show("Veuillez se déconnecter");
            e.Cancel = true;
        }

        private void tileItem18_ItemClick(object sender, TileItemEventArgs e)
        {

           
           popupMenu1.ShowPopup(this.barManager1, Control.MousePosition);
          
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Hide();
            Login log = new Login();
            log.Show();
        }

        private void tileItem9_ItemClick(object sender, TileItemEventArgs e)
        {
          
            cmlentdon = Cumulentreedon.Instance();
            if (cmlentdon.WindowState == FormWindowState.Minimized)
            {
                cmlentdon.WindowState = FormWindowState.Maximized;
            }
            else

                cmlentdon.Show();
            cmlentdon.WindowState = FormWindowState.Maximized;
          
        }

        private void tileItem12_ItemClick(object sender, TileItemEventArgs e)
        {
           
            cmlsordon = Cumulsortiedon.Instance();
            if (cmlsordon.WindowState == FormWindowState.Minimized)
            {
                cmlsordon.WindowState = FormWindowState.Maximized;
            }
            else

                cmlsordon.Show();
            cmlsordon.WindowState = FormWindowState.Maximized;
          
        }

        private void tileItem20_ItemClick(object sender, TileItemEventArgs e)
        {
           
            gestdev = Gestiondevis.Instance();
            if (gestdev.WindowState == FormWindowState.Minimized)
            {
                gestdev.WindowState = FormWindowState.Maximized;
            }
            else

                gestdev.Show();
            gestdev.WindowState = FormWindowState.Maximized;
           
        }

        private void tileItem17_ItemClick(object sender, TileItemEventArgs e)
        {
            
            gestdev = Gestiondevis.Instance();
            if (gestdev.WindowState == FormWindowState.Minimized)
            {
                gestdev.WindowState = FormWindowState.Maximized;
            }
            else

                gestdev.Show();
            gestdev.WindowState = FormWindowState.Maximized;
           
        }

        private void tileControl1_Click(object sender, EventArgs e)
        {

        }

        private void tileItem26_ItemClick(object sender, TileItemEventArgs e)
        {
            gestcompta = GestionCompta.Instance();
            if (gestcompta.WindowState == FormWindowState.Minimized)
            {
                gestcompta.WindowState = FormWindowState.Maximized;
            }
            else

                gestcompta.Show();
            gestcompta.WindowState = FormWindowState.Maximized;
        }

        private void tileItem13_ItemClick(object sender, TileItemEventArgs e)
        {

            inv = Invetappro.Instance();
            if (inv.WindowState == FormWindowState.Minimized)
            {
                inv.WindowState = FormWindowState.Maximized;
            }
            else

                inv.Show();
            inv.WindowState = FormWindowState.Maximized;

        }

        private void tileItem7_ItemClick(object sender, TileItemEventArgs e)
        {
          
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            hr = System.DateTime.Now.Hour;
            
            min = System.DateTime.Now.Minute;
            sec = DateTime.Now.Second;

            if (hr > 12)
                hr -= 12;

            if (sec % 2 == 0)
            {
                tileItem7.Text = +hr + ":" + min.ToString("00");
            }
            else
            {
                tileItem7.Text = hr + ":" + min.ToString("00") ;
            }
        }
    }
}