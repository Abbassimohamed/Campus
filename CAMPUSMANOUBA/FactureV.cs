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
using CAMPUSMANOUBA.Report;
using DevExpress.XtraReports.UI;

namespace CAMPUSMANOUBA
{
    public partial class FactureV : DevExpress.XtraEditors.XtraForm
    {
        BLService DBL = new BLService();
        BCService DBC = new BCService();
        ClientService DC = new ClientService();
        ArtService DS = new ArtService();
        DevisService devser = new DevisService();
        List<piece_fact> darttab;
        string field;

        double prixtotc = 0, prix_rem=0;
        int numligne = 0;
        List<int> _LidBl;
        List<bon_commande> lbcmd;
        List<devis> lsdevs;
        int Idfact = 0;
        public static int codeclt;
        public FactureV(int idfact)
        {
            InitializeComponent();
            Idfact = idfact;

        }
        public FactureV(List<int> LidBl)
        {
            List<string> numcmds = new List<string>(); ;
            field = "bl";
            InitializeComponent();
            getlastindex();
            _LidBl = LidBl;
            dateEdit1.Text = DateTime.Now.ToString();
            darttab = new List<piece_fact>();
         
            string list_bl = "";
            for (int g = 0; g < LidBl.Count; g++)
            {
                list_bl += LidBl[g] + "/";
            }
            list_bl = list_bl.Substring(0, list_bl.Count() - 1);
            textEdit1.Text = list_bl;


            foreach (int idbl in LidBl)
            {

                bon_livraison dt = new bon_livraison();
                dt = DBL.GetBLBynum(idbl);

                codeclt =Convert.ToInt32( dt.id_clt);
                List<ligne_bl> dt_bl = new List<ligne_bl>();
                dt_bl = DBL.getLblByCodeBL(Convert.ToInt32(idbl.ToString()));

                foreach (ligne_bl row1 in dt_bl)
                {
                    if(row1.idcmd!=null)
                    {
                        if(!numcmds.Contains(row1.idcmd.ToString()))
                        {
                            numcmds.Add(row1.idcmd.ToString());
                        }
                      
                    }
                   
                    Boolean trouve = false;
                    foreach (piece_fact pfact in darttab)
                    {

                        if (pfact.code_piece_u.ToString() == row1.code_art.ToString())
                        {
                            trouve = true;
                            pfact.quantite_piece_u = pfact.quantite_piece_u + row1.quantite_article;
                            pfact.pv = pfact.pv + row1.total;
                        }

                    }
                    if (!trouve)
                    {

                        piece_fact dr = new piece_fact();
                        dr.code_piece_u = row1.code_art.ToString();
                        dr.libelle_piece_u = row1.designation_article.ToString();
                        dr.quantite_piece_u = row1.quantite_article;
                        dr.puv = row1.puv;
                        dr.pv = row1.total;
                        dr.idauteur = row1.idauteur;                     
                    
                        darttab.Add(dr);
                        
                    }
                }
            }
            if(numcmds.LongCount()!=0)
            {
                foreach (string numcmd in numcmds)
                {
                    textEdit4.Text = textEdit4.Text + numcmd + "/";
                }
            }
            string finalnbl;
            string finalcmd;
            if(textEdit1.Text != "")
            {
                finalnbl = textEdit1.Text.Substring(0, 9);            
                bon_livraison bliv = new bon_livraison();
                bliv = DBL.GetBLBynum(Convert.ToInt32( finalnbl));
                dateEdit3.DateTime =Convert.ToDateTime( bliv.date_ajout);
            }
            if(textEdit4.Text != "")
            {
                finalcmd = textEdit4.Text.Substring(0, 9);
                bon_commande bcmd = new bon_commande();
                bcmd = DBC.getAllBcbycode(Convert.ToInt32(finalcmd));
                dateEdit2.DateTime = Convert.ToDateTime(bcmd.date_ajout);
            }
            lookUpEdit1.EditValue =codeclt;
            getalldatatable();
            updatesum();
        }
        public FactureV(List<devis> Listdevis)
        {
            lsdevs = Listdevis;
            field = "devis";
            InitializeComponent();
            getlastindex();
            dateEdit1.Text = DateTime.Now.ToString();
            darttab = new List<piece_fact>();
            
            foreach (devis dev in Listdevis)
            {
                codeclt = Convert.ToInt32(dev.id_clt);
                List<ligne_devis> ldevs = new List<ligne_devis>();
                ldevs = devser.getLDevisByCodeDevis((int)dev.numero_devis);
                   
                foreach (ligne_devis ldev in ldevs)
                {

                   
                    Boolean trouve = false;
                    foreach (piece_fact pfact in darttab)
                    {

                        if (pfact.code_piece_u.ToString() == ldev.code_art.ToString())
                        {
                            trouve = true;
                            pfact.quantite_piece_u = pfact.quantite_piece_u + ldev.quantite_article;
                            pfact.pv = pfact.pv + ldev.total;
                        }

                    }
                    if (!trouve)
                    {

                        piece_fact dr = new piece_fact();
                        dr.code_piece_u = ldev.code_art.ToString();
                        dr.libelle_piece_u = ldev.designation_article.ToString();
                        dr.quantite_piece_u = ldev.quantite_article;
                        dr.puv = ldev.puv;
                        dr.pv = ldev.total;
                        dr.idauteur = ldev.idauteur;

                     
                        darttab.Add(dr);

                   

                    }
                }
            }
            lookUpEdit1.EditValue = codeclt;
            getalldatatable();
            updatesum();
        }
        public FactureV(List<bon_commande> Listcmd)
        {
            lbcmd = Listcmd;
            field = "cmd";
            InitializeComponent();
            getlastindex();
            dateEdit1.Text = DateTime.Now.ToString();
            darttab = new List<piece_fact>();

            foreach (bon_commande bcmd in Listcmd)
            {

                List<ligne_bc> lbcs = new List<ligne_bc>();
                lbcs = DBC.getLbcByCodeBC((int)bcmd.numero_bc);

                foreach (ligne_bc lbc in lbcs)
                {


                    Boolean trouve = false;
                    foreach (piece_fact pfact in darttab)
                    {

                        if (pfact.code_piece_u.ToString() == lbc.code_art.ToString())
                        {
                            trouve = true;
                            pfact.quantite_piece_u = pfact.quantite_piece_u + lbc.quantite_article;
                            pfact.pv = pfact.pv + lbc.prixremis;
                        }

                    }
                    if (!trouve)
                    {

                        piece_fact dr = new piece_fact();
                        dr.code_piece_u = lbc.code_art.ToString();
                        dr.libelle_piece_u = lbc.designation_article.ToString();
                        dr.quantite_piece_u = lbc.quantite_article;
                        dr.puv = lbc.puv;
                        dr.pv = lbc.prixremis;
                        dr.idauteur = lbc.idauteur;

                      
                        darttab.Add(dr);



                    }
                }
            }
            lookUpEdit1.EditValue = codeclt;
            getalldatatable();
            updatesum();
        }
        private void getlastindex()
        {
            string date1 = System.DateTime.Today.ToShortDateString();
            string year = date1.Substring(6, 4);
            string lastindex = DBL.NumFact().ToString();
            if (lastindex == "1")
            {
                Tnumfact.Text = year + 1.ToString("00000");

            }
            else
            {


                string lastindexyear = lastindex.Substring(0, 4);
                if (lastindexyear != year)
                {
                    Tnumfact.Text = year + 1.ToString("00000");
                }
                else
                {
                    Tnumfact.Text = DBL.NumFact().ToString("00000");
                }
            }
        }
        private void clients()
        {
            //get All stock
            lookUpEdit1.Properties.DataSource = null;
            List<client> Allclients = new List<client>();
            Allclients = DC.listclts();

            lookUpEdit1.Properties.ValueMember = "codeclient";
            lookUpEdit1.Properties.DisplayMember = "raisonsoc";
            lookUpEdit1.Properties.DataSource = Allclients;
            lookUpEdit1.Properties.PopulateColumns();
            lookUpEdit1.Properties.Columns["codeclient"].Visible = false;
            lookUpEdit1.Properties.Columns["numerocl"].Caption = "Code client";
            lookUpEdit1.Properties.Columns["raisonsoc"].Caption = "Raison sociale";
            lookUpEdit1.Properties.Columns["resp"].Caption = "Responsable";
            lookUpEdit1.Properties.Columns["qualite"].Visible = false;
            lookUpEdit1.Properties.Columns["tel"].Visible = false;
            lookUpEdit1.Properties.Columns["mobile"].Visible = false;
            lookUpEdit1.Properties.Columns["adresse"].Visible = false;
            lookUpEdit1.Properties.Columns["codepostal"].Visible = false;
            lookUpEdit1.Properties.Columns["ville"].Visible = false;
            lookUpEdit1.Properties.Columns["web"].Visible = false;
            lookUpEdit1.Properties.Columns["email"].Visible = false;
            lookUpEdit1.Properties.Columns["fax"].Visible = false;
         

        }
        private void getalldatatable()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = darttab;
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[1].Caption = "Code article";
            this.gridView1.Columns[2].Caption = "Désignation";
            this.gridView1.Columns[3].Caption = "Quantité";
            this.gridView1.Columns[4].Visible = false;
            this.gridView1.Columns[5].Caption = "PUV";
            this.gridView1.Columns[6].Caption = "remise";
            this.gridView1.Columns[7].Caption = "Total";
            this.gridView1.Columns[8].Visible = false;
            //this.gridView1.Columns[9].Visible = false;

            //this.gridView1.Columns[10].Caption = "unité";
            //this.gridView1.Columns[11].Caption = "Qte restante";
            //this.gridView1.Columns[12].Visible = false;
            gridView1.OptionsView.ShowAutoFilterRow = true;

        }
      

        private void FactureVente_Load(object sender, EventArgs e)
        {
            clients();
            
        }
        
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {

                facturevente dt_fact_n = new facturevente();
                    if (dateEdit1.Text == "")
                    {
                        MessageBox.Show("Entrer la date de la facture");
                    }

                    else
                    {
                
                        string dd = dateEdit1.Text.Substring(0, 10);//.ToString();
                        dd = dd.Substring(0, 10);
                        double prix_hr = 0, tt_hr = 0, prix_r = 0, tt_r = 0;
                        for (int i = 0; i < gridView1.RowCount; i++)
                        {
                            piece_fact row1 = new piece_fact();
                            row1 = (piece_fact)gridView1.GetRow(i);
                            prix_r = Convert.ToDouble(row1.pv.ToString().Replace('.',','));                         
                            tt_r += prix_r;
                            row1.id_fact = int.Parse(Tnumfact.Text);
                            row1.pv = prix_r;
                            DBL.ajouterLF(row1);
                    
                        }
                        facturevente fac = new facturevente();
                        client row = (client)lookUpEdit1.GetSelectedDataRow();
                        if (row != null)
                        {
                            fac.id_clt = row.codeclient.ToString();
                            fac.client = row.raisonsoc.ToString();
                        }
                        fac.date_ajout = dateEdit1.DateTime;
                        fac.numero_fact = int.Parse(Tnumfact.Text);
                        fac.montant_hr = Convert.ToDouble(textEdit8.Text);
                        fac.remise = Convert.ToDouble(textEdit6.Text);
                        fac.montant = Convert.ToDouble(textEdit9.Text);
                        fac.L_num_bl = textEdit1.Text;
                        fac.RefCmd = textEdit4.Text;
                        DBL.ajouterFacture(fac);
                    FactureReport fcrep = new FactureReport(Convert.ToInt32( fac.numero_fact));
                    fcrep.ShowRibbonPreview();
                if(field=="bl")
                {
                    foreach (int i in _LidBl)
                    {
                        bon_livraison bl = new bon_livraison();
                        bl = DBL.GetBLBynum(i);
                        bl.etat = "facturée";
                        DBL.modifier(bl);
                    }

                }
                 else if(field =="devis")
                {
                        foreach (devis dev in lsdevs)
                        {

                            dev.etat = "facturé";
                            devser.modifier(dev);
                        }
                    }
                else if (field == "cmd")
                {
                    foreach (bon_commande bcmd in lbcmd)
                    {

                        bcmd.etat = "livré";
                        DBC.modifierBC(bcmd);
                    }
                }
         

            }
                this.Close();

            }
            catch (Exception ed)
            {
                MessageBox.Show("Vérifier les données entrées");
            }
        }
        public void updatesum()
        {
            try
            {
                prixtotc = 0;
                prix_rem = 0;
                for (int i = 0; i < gridView1.RowCount; i++)
                {

                    piece_fact lfact = (piece_fact)gridView1.GetRow(i);
                    prixtotc += Convert.ToDouble(lfact.pv.ToString().Replace('.', ','));
                    textEdit8.Text = prixtotc.ToString();
                    prix_rem = prixtotc - prixtotc * (Convert.ToDouble(textEdit6.Text)) / 100;
                    textEdit9.Text = prix_rem.ToString();
                }



            }
            catch (Exception xce)
            {
            }
        }
        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }

        private void textEdit10_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void textEdit6_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double montanttotavantremise = Convert.ToDouble(textEdit8.Text);
                if (Convert.ToDouble(textEdit6.Text) >= 0)
                {
                    double montantapresremise = montanttotavantremise - (montanttotavantremise * Convert.ToDouble(textEdit6.Text) / 100);
                    textEdit9.Text = montantapresremise.ToString();
                }
                else
                {
                    MessageBox.Show("Veuillez insérer une valeur valide");
                }
            }
            catch(Exception exc)
            { }
          
        }
    }
}