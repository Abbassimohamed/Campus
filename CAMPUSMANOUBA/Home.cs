using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using System.Threading;
using System.Diagnostics;
using DevExpress.XtraEditors;
namespace CAMPUSMANOUBA
{
    public partial class Home: DevExpress.XtraBars.Ribbon.RibbonForm
    {
      
        public static NewFr newfr;
        public static ModifierFr mdfr;
        public static NewClient newcl;
        public static ModifierClient modcl;
        public static NewAuthor newauthor;
        public static NewArticle newart;
        public static NewFourniture newfour;
        public static ConsultArt csart;
        public static Consultclt cclt;
        public static ConsulterFourniture csfour;
        public static Consultfr consultfournisseur;
        public static Consutaut csaut;
        //public static BLNOCommandeBL newBL = BLNOCommandeBL.Instance();
        public static CumulEntree cmt;
        public static CumulSort cmtsortie;
        public static CumulEntreefr cmtentreefr;
        public static CumulSortiefr cmtsortiefr;
        public static Fichepublication fichepub;
        public static Nouveau_agent newagent;
        public static Fichesuivie fichesuivi;
        public static Cumulentreedon newbr;
        public static Cumulsortiedon newbs;
        public static Cumulsarticle cumulsart;
        public static Cumulsfourniture cmfour;
        public static BLNOCommandeBL newBL;
        public static UpdateCmd newBC;
        public static listebls listeBl;
        public static listefactures listefact ;
        public static listecmds listecmd;    
        public static Accueiltile acc= new Accueiltile();

        public static int wait = 0;
        private static Home instance;
        public static Home Instance()
        {
            if (instance == null)

                instance = new Home();
            return instance;

        }
        public Home()
        {
            InitializeComponent();
          
      

          
        }

        private void barButtonItem1_ItemClick(object sender, ItemClickEventArgs e)
        {
         
            splashScreenManager2.ShowWaitForm();
            newfr = NewFr.Instance();


            newfr.MdiParent = Home.ActiveForm;
                newfr.Show();
                newfr.BringToFront();
          
            
            splashScreenManager2.CloseWaitForm();
        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {
           
            splashScreenManager2.ShowWaitForm();
            consultfournisseur = Consultfr.Instance();


            consultfournisseur.MdiParent = Home.ActiveForm;
                consultfournisseur.Show();
                consultfournisseur.BringToFront();
           

               
            splashScreenManager2.CloseWaitForm();
        }

        private void barButtonItem4_ItemClick(object sender, ItemClickEventArgs e)
        {
          
            splashScreenManager2.ShowWaitForm();
             newcl = NewClient.Instance();

            newcl.MdiParent = Home.ActiveForm;
                newcl.Show();
                newcl.BringToFront();
           
            splashScreenManager2.CloseWaitForm();
        }

        private void Home_Load(object sender, EventArgs e)
        {
            Thread.Sleep(2000);

            acc.MdiParent = this;
            acc.Show();
            acc.BringToFront();
        }

        private void barButtonItem6_ItemClick(object sender, ItemClickEventArgs e)
        {
           
            splashScreenManager2.ShowWaitForm();
            newauthor = NewAuthor.Instance();
            newauthor.MdiParent = Home.ActiveForm;
                newauthor.Show();
                newauthor.BringToFront();
           
            splashScreenManager2.CloseWaitForm();
        }

        private void barMdiChildrenListItem1_ListItemClick(object sender, ListItemClickEventArgs e)
        {

        }

        private void barButtonItem8_ItemClick(object sender, ItemClickEventArgs e)
        {

          
            splashScreenManager2.ShowWaitForm();
            newart = NewArticle.Instance();
            newart.MdiParent = Home.ActiveForm;
                newart.Show();
                newart.BringToFront();
          
            splashScreenManager2.CloseWaitForm();
        }

        private void barButtonItem5_ItemClick(object sender, ItemClickEventArgs e)
        {
           
            splashScreenManager2.ShowWaitForm();
            cclt = Consultclt.Instance();

            cclt.MdiParent = Home.ActiveForm;
            cclt.Show();
            cclt.BringToFront();

            splashScreenManager2.CloseWaitForm();
        }

        private void barButtonItem36_ItemClick(object sender, ItemClickEventArgs e)
        {
           
            Empmanage emp = new Empmanage();
            emp.ShowDialog();
         

        }

        private void barButtonItem9_ItemClick(object sender, ItemClickEventArgs e)
        {
           
            splashScreenManager2.ShowWaitForm();
            csart = ConsultArt.Instance();
           
                 csart.MdiParent = Home.ActiveForm;
                csart.Show();
                csart.BringToFront();
          
            splashScreenManager2.CloseWaitForm();
          
    
        }

        private void barButtonItem7_ItemClick(object sender, ItemClickEventArgs e)
        {
          
            splashScreenManager2.ShowWaitForm();
            csaut = Consutaut.Instance();
            
                csaut.MdiParent = Home.ActiveForm;
                csaut.Show();
                csaut.BringToFront();
         
            splashScreenManager2.CloseWaitForm();

        }

        private void barButtonItem22_ItemClick(object sender, ItemClickEventArgs e)
        {
            
            splashScreenManager2.ShowWaitForm();
            fichesuivi = Fichesuivie.Instance();
           
            fichesuivi.MdiParent = Home.ActiveForm;
            fichesuivi.Show();
            fichesuivi.BringToFront();

            splashScreenManager2.CloseWaitForm();
        }

        private void barButtonItem12_ItemClick(object sender, ItemClickEventArgs e)
        {
            
            splashScreenManager2.ShowWaitForm();
            newfour = NewFourniture.Instance();
           
                         newfour.MdiParent = Home.ActiveForm;
                newfour.Show();
                newfour.BringToFront();
          
            splashScreenManager2.CloseWaitForm();
        }

        private void barButtonItem13_ItemClick(object sender, ItemClickEventArgs e)
        {
           
            splashScreenManager2.ShowWaitForm();
            csfour = ConsulterFourniture.Instance();
        
              csfour.MdiParent = Home.ActiveForm;
                csfour.Show();
                csfour.BringToFront();
           
            splashScreenManager2.CloseWaitForm();

        }

        private void barButtonItem23_ItemClick(object sender, ItemClickEventArgs e)
        {
            splashScreenManager2.ShowWaitForm();
            newBL = BLNOCommandeBL.Instance();
            newBL.MdiParent = Home.ActiveForm;
            newBL.Show();
            newBL.BringToFront();

            splashScreenManager2.CloseWaitForm();

        }

        private void barButtonItem17_ItemClick(object sender, ItemClickEventArgs e)
        {
           
            splashScreenManager2.ShowWaitForm();
            cmt = CumulEntree.Instance();
           
            cmt.MdiParent = Home.ActiveForm;
            cmt.Show();
            cmt.BringToFront();

            splashScreenManager2.CloseWaitForm();
        }

        private void barButtonItem19_ItemClick(object sender, ItemClickEventArgs e)
        {
           
            splashScreenManager2.ShowWaitForm();
            cmtsortie = CumulSort.Instance();
          
            cmtsortie.MdiParent = Home.ActiveForm;
            cmtsortie.Show();
            cmtsortie.BringToFront();

            splashScreenManager2.CloseWaitForm();
        }

        private void barButtonItem14_ItemClick(object sender, ItemClickEventArgs e)
        {
            
            splashScreenManager2.ShowWaitForm();
            cmtentreefr = CumulEntreefr.Instance();
     
            cmtentreefr.MdiParent = Home.ActiveForm;
            cmtentreefr.Show();
            cmtentreefr.BringToFront();

            splashScreenManager2.CloseWaitForm();
        }

        private void barButtonItem15_ItemClick(object sender, ItemClickEventArgs e)
        {
          
            splashScreenManager2.ShowWaitForm();
            cmtsortiefr = CumulSortiefr.Instance();
         
            cmtsortiefr.MdiParent = Home.ActiveForm;
            cmtsortiefr.Show();
            cmtsortiefr.BringToFront();

            splashScreenManager2.CloseWaitForm();
        }

        private void barButtonItem21_ItemClick(object sender, ItemClickEventArgs e)
        {
           
            splashScreenManager2.ShowWaitForm();
            fichepub = Fichepublication.Instance();
          
            fichepub.MdiParent = Home.ActiveForm;
            fichepub.Show();
            fichepub.BringToFront();

            splashScreenManager2.CloseWaitForm();
        }

        private void barButtonItem41_ItemClick(object sender, ItemClickEventArgs e)
        {
           
            splashScreenManager2.ShowWaitForm();
            newagent = Nouveau_agent.Instance();
           
            newagent.MdiParent = Home.ActiveForm;
            newagent.Show();
            newagent.BringToFront();

            splashScreenManager2.CloseWaitForm();
        }

        private void barButtonItem45_ItemClick(object sender, ItemClickEventArgs e)
        {
           
            splashScreenManager2.ShowWaitForm();
            cumulsart = Cumulsarticle.Instance();
          
            cumulsart.MdiParent = Home.ActiveForm;
            cumulsart.Show();
            cumulsart.BringToFront();

            splashScreenManager2.CloseWaitForm();
        }

        private void barButtonItem46_ItemClick(object sender, ItemClickEventArgs e)
        {
          
            splashScreenManager2.ShowWaitForm();
            newbs = Cumulsortiedon.Instance();
     
            newbs.MdiParent = Home.ActiveForm;
            newbs.Show();
            newbs.BringToFront();

            splashScreenManager2.CloseWaitForm();
        }

        private void barButtonItem48_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem47_ItemClick(object sender, ItemClickEventArgs e)
        {
          
            splashScreenManager2.ShowWaitForm();
            newbr = Cumulentreedon.Instance();
             newbr.MdiParent = Home.ActiveForm;
            newbr.Show();
            newbr.BringToFront();

            splashScreenManager2.CloseWaitForm();
        }

        private void barButtonItem49_ItemClick(object sender, ItemClickEventArgs e)
        {
           
            splashScreenManager2.ShowWaitForm();
            cmfour = Cumulsfourniture.Instance();   
         
            cmfour.MdiParent = Home.ActiveForm;
            cmfour.Show();
            cmfour.BringToFront();

            splashScreenManager2.CloseWaitForm();
        }

        private void Home_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (XtraForm frm in this.MdiChildren)
            {
                frm.Close();
            }
            this.Close();
            Process.GetCurrentProcess().Kill();
        }

        private void barButtonItem37_ItemClick(object sender, ItemClickEventArgs e)
        {
            splashScreenManager2.ShowWaitForm();
            newBC = UpdateCmd.Instance();
            newBC.MdiParent = Home.ActiveForm;
            newBC.Show();
            newBC.BringToFront();


            splashScreenManager2.CloseWaitForm();
        }

        private void barButtonItem38_ItemClick(object sender, ItemClickEventArgs e)
        {
            splashScreenManager2.ShowWaitForm();
        
                listecmd =  listecmds.Instance();
                listecmd.MdiParent = Home.ActiveForm;
                listecmd.Show();
                listecmd.BringToFront();
          
            splashScreenManager2.CloseWaitForm();

        }

        private void barButtonItem24_ItemClick(object sender, ItemClickEventArgs e)
        {
            splashScreenManager2.ShowWaitForm();

            listeBl = listebls.Instance();
            listeBl.MdiParent = Home.ActiveForm;
            listeBl.Show();
            listeBl.BringToFront();

            splashScreenManager2.CloseWaitForm();

        }

        private void barButtonItem25_ItemClick(object sender, ItemClickEventArgs e)
        {
            splashScreenManager2.ShowWaitForm();

            listeBl = listebls.Instance();
            listeBl.MdiParent = Home.ActiveForm;
            listeBl.Show();
            listeBl.BringToFront();

            splashScreenManager2.CloseWaitForm();

        }

        private void barButtonItem26_ItemClick(object sender, ItemClickEventArgs e)
        {
            splashScreenManager2.ShowWaitForm();
            listefact =  listefactures.Instance();
            listefact.MdiParent = Home.ActiveForm;
            listefact.Show();
            listefact.BringToFront();
            splashScreenManager2.CloseWaitForm();

        }

        private void barButtonItem27_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void ribbonStatusBar_Click(object sender, EventArgs e)
        {

        }

        private void tileItem3_ItemClick(object sender, TileItemEventArgs e)
        {

        }

        private void ribbon_Click(object sender, EventArgs e)
        {

        }

        private void tileItem1_ItemClick(object sender, TileItemEventArgs e)
        {

        }

        private void tileItem2_ItemClick(object sender, TileItemEventArgs e)
        {

        }

        private void tileItem15_ItemClick(object sender, TileItemEventArgs e)
        {

        }

        private void tileItem14_ItemClick(object sender, TileItemEventArgs e)
        {

        }

        private void barButtonItem50_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void barButtonItem55_ItemClick(object sender, ItemClickEventArgs e)
        {
           

            acc.MdiParent = this;
            acc.Show();
            acc.BringToFront();
        }

        private void barButtonItem51_ItemClick(object sender, ItemClickEventArgs e)
        {
          

            acc.MdiParent = this;
            acc.Show();
            acc.BringToFront();
        }

        private void barButtonItem52_ItemClick(object sender, ItemClickEventArgs e)
        {
          

            acc.MdiParent = this;
            acc.Show();
            acc.BringToFront();
        }

        private void barButtonItem53_ItemClick(object sender, ItemClickEventArgs e)
        {
          
            acc.MdiParent = this;
            acc.Show();
            acc.BringToFront();
        }

        private void barButtonItem54_ItemClick(object sender, ItemClickEventArgs e)
        {
           
            acc.MdiParent = this;
            acc.Show();
            acc.BringToFront();
        }

        private void barButtonItem56_ItemClick(object sender, ItemClickEventArgs e)
        {
          
            acc.MdiParent = this;
            acc.Show();
            acc.BringToFront();
        }

        private void barButtonItem57_ItemClick(object sender, ItemClickEventArgs e)
        {
          
            acc.MdiParent = this;
            acc.Show();
            acc.BringToFront();
        }
    }
}