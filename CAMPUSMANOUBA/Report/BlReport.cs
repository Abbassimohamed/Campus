
using System;
using System.Collections.Generic;
using BLL;
using DAL;
namespace CAMPUSMANOUBA.Report
{
    public partial class BlReport : DevExpress.XtraScheduler.Reporting.XtraSchedulerReport
    {
      private static    bon_livraison bl = new bon_livraison();
        societe soc = new societe();
          SocieteService socser = new SocieteService();
        ClientService clserv = new ClientService();
        BLService blserv = new BLService();
        List<ligne_bl> lbls = new List<ligne_bl>();
        private static int numerobl;
        private static double prixtot, quantit,prsansrem;
        public BlReport(int numbl)
        {
            InitializeComponent();
            numerobl = numbl;


        }
        private void GetHeader(int numbl)
        {
            
            bl = blserv.GetBLBynum(numbl);
            LabNumero.Text = bl.numero_bl.ToString();
            LabDate.Text = bl.date_ajout.ToString();
            getclientbyid(Convert.ToInt32( bl.id_clt));
        }
        private void getclientbyid(int idclt)
        {
            client cl = new client();
            cl = clserv.getClientByNumero(idclt);
            xrcodcl.Text = cl.numerocl.ToString();
            xrnom.Text = cl.raisonsoc;
            xradr.Text = cl.adresse;
            xrville.Text = cl.ville;
        }
        private void GetDetails(int numbl)
        {
            //
            lbls = blserv.getLblByCodeBL(numbl);
            xrTable1.BeginInit();
            int inde = xrTable1.Rows.Count - 1;

            xrTable1.InsertRowBelow(xrTable1.Rows[inde]);

            //int gd = xrTable1.Rows.Count;
            xrTable1.Rows[0].Cells[0].Text = "";
            xrTable1.Rows[0].Cells[1].Text = "";
            xrTable1.Rows[0].Cells[2].Text = "";
            xrTable1.Rows[0].Cells[3].Text = "";
            xrTable1.Rows[0].Cells[4].Text = "";

            xrTable1.EndInit();

           
            //
            for (int i = 0; i < lbls.Count; i++)
            {

                xrTable1.BeginInit();
                int indx = xrTable1.Rows.Count - 1;
                xrTable1.InsertRowBelow(xrTable1.Rows[i]);
                int a = i + 1;
                //code
                xrTable1.Rows[a].Cells[0].Text = lbls[i].code_art.ToString();
                xrTable1.Rows[a].Cells[0].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTable1.Rows[a].Cells[0].Multiline = true;
                //description
                xrTable1.Rows[a].Cells[1].Text = lbls[i].designation_article;
                xrTable1.Rows[a].Cells[1].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                xrTable1.Rows[a].Cells[1].Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0);
                xrTable1.Rows[a].Cells[1].Multiline = true;
                //unite
                quantit +=(double) lbls[i].quantite_article;
                xrTable1.Rows[a].Cells[2].Text = lbls[i].quantite_article.ToString();
                xrTable1.Rows[a].Cells[2].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTable1.Rows[a].Cells[2].Multiline = true;
                prsansrem =Convert.ToDouble( lbls[i].quantite_article) * Convert.ToDouble( lbls[i].puv);
                prixtot += prsansrem;
                xrTable1.Rows[a].Cells[3].Text = Convert.ToDouble(lbls[i].puv).ToString("0.000");
                xrTable1.Rows[a].Cells[3].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                xrTable1.Rows[a].Cells[3].Multiline = true;

                xrTable1.Rows[a].Cells[4].Text = prsansrem.ToString("0.000") ;
                xrTable1.Rows[a].Cells[4].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                xrTable1.Rows[a].Cells[4].Multiline = true;


                xrTable1.EndInit();

            }
            
            if (xrTable1.Rows.Count < 13)
            {
                int g = 13 - xrTable1.Rows.Count;
                int index1 = xrTable1.Rows.Count + g;
                for (int k = xrTable1.Rows.Count; k < index1; k++)
                {
                    xrTable1.BeginInit();
                    int index2 = xrTable1.Rows.Count - 1;

                    xrTable1.InsertRowBelow(xrTable1.Rows[index2]);

                    int gd = xrTable1.Rows.Count;
                    xrTable1.Rows[k].Cells[0].Text = "";
                    xrTable1.Rows[k].Cells[1].Text = "";
                    xrTable1.Rows[k].Cells[2].Text = "";
                    xrTable1.Rows[k].Cells[3].Text = "";
                    xrTable1.Rows[k].Cells[4].Text = "";

                    xrTable1.EndInit();
                }
            }

            xrLabel42.Text = prixtot.ToString();

            xrLabel43.Text =quantit.ToString();
        }
        private void GetFooter()
        {
            soc = socser.findlastfr();
            xrTEL1.Text = soc.tel;
            xrfax.Text = soc.fax;
            xradress.Text = soc.adresse;
            xrMF.Text = soc.matfisc;
        }

        private void TopMargin_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            GetHeader(numerobl);
            GetDetails(numerobl);
            GetFooter();
        }
    }
}
