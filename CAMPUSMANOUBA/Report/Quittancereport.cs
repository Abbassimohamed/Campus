using System;
using BLL;
using DAL;

namespace CAMPUSMANOUBA.Report
{
    public partial class Quittancereport : DevExpress.XtraScheduler.Reporting.XtraSchedulerReport
    {
        SocieteService socser = new SocieteService();
        societe soc = new societe();
        client cl = new client();
        ClientService clser = new ClientService();
        QuittanceService quitser = new QuittanceService();
        public static Quittance quit = new Quittance();
        public Quittancereport(string nquittance)
        {
            InitializeComponent();
          
            quit = quitser.findquitbycode(nquittance);

        }
        private void Detailc()
        {
            cl = clser.getClientByNumero((int)quit.idclient);
            soc = socser.findlastfr();
            xrnquit.Text = quit.Nquiitance.ToString(); 
            xrtext.Text=soc.rsoc+", Immatriculée au "+soc.matfisc+"et situér au "+ soc.adresse + ", atteste avoir reçu paiement:";
            xrnomclient.Text = cl.raisonsoc;
            xrnetapayer.Text = quit.netapayer.ToString();
            xrraison.Text = "Facture N°" + quit.nfact;
            xrmodepaie.Text = quit.mode;
            xrdatequit.Text = quit.datequittance.ToString();
            xrLabel14.Text = "Tunis le :" + System.DateTime.Today;


        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            Detailc();
        }
    }
}
