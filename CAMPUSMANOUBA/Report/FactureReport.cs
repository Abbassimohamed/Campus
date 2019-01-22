using System;
using System.Collections.Generic;
using BLL;
using DAL;
namespace CAMPUSMANOUBA.Report
{
    public partial class FactureReport : DevExpress.XtraScheduler.Reporting.XtraSchedulerReport
    {
        private static facturevente fact = new facturevente();
        societe soc = new societe();
        SocieteService socser = new SocieteService();
        ClientService clserv = new ClientService();
        BLService blserv = new BLService();
        BCService bcserv = new BCService();
        List<piece_fact> lpfacts = new List<piece_fact>();
        private static int numerofact;
        private static double prixtot, quantit, prsansrem;
        public FactureReport(int numfact)
        {
            InitializeComponent();
            numerofact = numfact;
        }
        private void GetHeader(int numfact)
        {

            fact = blserv.GetFactBynum(numfact);
            LabNumero.Text = fact.numero_fact.ToString();
            LabDate.Text = fact.date_ajout.ToString();
            getclientbyid(Convert.ToInt32(fact.id_clt));
            if(fact.RefCmd.Length > 9)
            {
                string lastcmd = fact.RefCmd.Substring(0, 9);
                bon_commande bcmd = new bon_commande();
                bcmd = bcserv.getAllBcbycode(Convert.ToInt32(lastcmd));
                xrdatecmd.Text = bcmd.date_ajout.ToString();
            }
           
            if (fact.L_num_bl.Length>9)
            {
                string lastbl = fact.L_num_bl.Substring(0, 9);
                bon_livraison bl = new bon_livraison();
                bl = blserv.GetBLBynum(Convert.ToInt32(lastbl));
                xrdatebl.Text = bl.date_ajout.ToString();

            }



            xrcmds.Text = fact.RefCmd;
            xrbls.Text = fact.L_num_bl;
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
        private void GetDetails(int numfact)
        {
            //
            lpfacts = blserv.getLFByCodeFact(numfact);
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
            for (int i = 0; i < lpfacts.Count; i++)
            {

                xrTable1.BeginInit();
                int indx = xrTable1.Rows.Count - 1;
                xrTable1.InsertRowBelow(xrTable1.Rows[i]);
                int a = i + 1;
                //code
                xrTable1.Rows[a].Cells[0].Text = lpfacts[i].code_piece_u.ToString();
                xrTable1.Rows[a].Cells[0].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTable1.Rows[a].Cells[0].Multiline = true;
                //description
                xrTable1.Rows[a].Cells[1].Text = lpfacts[i].libelle_piece_u;
                xrTable1.Rows[a].Cells[1].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
                xrTable1.Rows[a].Cells[1].Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0);
                xrTable1.Rows[a].Cells[1].Multiline = true;
                //unite
                quantit += (double)lpfacts[i].quantite_piece_u;
                xrTable1.Rows[a].Cells[2].Text = lpfacts[i].quantite_piece_u.ToString();
                xrTable1.Rows[a].Cells[2].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
                xrTable1.Rows[a].Cells[2].Multiline = true;
                prsansrem = Convert.ToDouble(lpfacts[i].quantite_piece_u) * Convert.ToDouble(lpfacts[i].puv);
                prixtot += prsansrem;
                xrTable1.Rows[a].Cells[3].Text = Convert.ToDouble(lpfacts[i].puv).ToString("0.000");
                xrTable1.Rows[a].Cells[3].TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
                xrTable1.Rows[a].Cells[3].Multiline = true;

                xrTable1.Rows[a].Cells[4].Text = prsansrem.ToString("0.000");
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
            xrLabel42.Text = fact.montant_hr.ToString();
            xrLabel43.Text = fact.remise.ToString();
            xrLabel15.Text = fact.montant.ToString();

        }

        private void GetFooter()
        {
            soc = socser.findlastfr();
            xrTEL1.Text = soc.tel;
            xrfax.Text = soc.fax;
            xradress.Text = soc.adresse;
            xrMF.Text = soc.matfisc;
            xrLabel51.Text = NumberToWords1(Convert.ToDouble(fact.montant).ToString("0.000"));
        }

        private void ReportFooter_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            GetHeader(numerofact);
            GetDetails(numerofact);
            GetFooter();
        }

        public static String NumberToWords1(String T)
        {
            String s1 = "";
            String s2 = "";
            String s = "";
            int i = 0;
            //String T= number.ToString();

            while (T[i] != ',' && i < T.Length)
            {
                s1 += T[i];
                i++;
            }
            int j = i + 1;
            while (j < T.Length)
            {


                s2 += T[j];
                j++;

            }
            //  MessageBox.Show(s2);
            s = NumberToWords(Convert.ToInt32(s1)) + " DINARS, " + NumberToWords(Convert.ToInt32(s2)) + " millimes";

            return (s);
        }
        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 1)
            {
                words += NumberToWords(number / 1000) + " mille ";
                number %= 1000;
            }
            if ((number / 1000) == 1)
            {
                words += " mille ";
                number %= 1000;
            }

            if ((number / 100) > 1)
            {
                words += NumberToWords(number / 100) + " cent ";
                number %= 100;
            }
            if ((number / 100) == 1)
            {
                words += " cent ";
                number %= 100;
            }
            if (number > 0)
            {
                // if (words != "")
                //;

                var unitsMap = new[] { "zero", "un", "deux", "trois", "quatre", "cinq", "six", "sept", "huit", "neuf", "dix", "onze", "douze", "treize", "quatorze", "quinze", "seize", "dix-sept", "dix-huit", "dix-neuf" };
                var tensMap = new[] { "zero", "dix", "vingt", "trente", "quarante", "cinquante", "soixante", "soixante-dix", "quatre-vingts", "quatre-vingt-dix" };

                if (number < 20)
                    words += unitsMap[number];

                else
                {
                    if (number > 60)
                    {
                        if ((number / 10) <= 7)
                        {
                            if (number % 7 == 0)
                            {
                                words += tensMap[7];
                            }
                            else
                            {
                                words += tensMap[6];
                                words += unitsMap[number - 60];
                            }
                        }
                        if ((number / 10) > 7)
                        {
                            if (number % 8 == 0)
                            {
                                words += tensMap[8];
                            }
                            else
                            {
                                words += tensMap[8];
                                words += unitsMap[number - 80];
                            }
                        }
                        return words;
                    }
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }
    }
}
