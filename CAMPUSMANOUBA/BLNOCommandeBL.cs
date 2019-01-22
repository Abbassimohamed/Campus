using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BLL;
using DAL;
using System.Threading;
using DevExpress.XtraReports.UI;
using CAMPUSMANOUBA.Report;
namespace CAMPUSMANOUBA
{
    public partial class BLNOCommandeBL : DevExpress.XtraEditors.XtraForm
    {
        List<ligne_bl> dartt = new List<ligne_bl>();
        List<ligne_bl> dartcopie = new List<ligne_bl>();
        List<ligne_bc> lignebcmds = new List<ligne_bc>();
        public static Double prixtotc, prix_rem;
        public static Double test;
        public static int numbl, id_clt = 0, idpiece = 0, idrow = 0;
        public static string Value;
        List<client> clts = new List<client>();
       public static   List<bon_commande> boncmds = new List<bon_commande>();
        BLService DBL = new BLService();
        reservationservice reservser = new reservationservice();
        BCService DBC = new BCService();
        ClientService DC = new ClientService();
        ArtService DS = new ArtService();
        private static BLNOCommandeBL instance;

        public BLNOCommandeBL()
        {
            InitializeComponent();
            dartt = new List<ligne_bl>();
            dartt.Clear();
            //clients();
            articles();
            //tnumcommandebase.Text = DBL.incrementerBL().ToString();            
        }
        private string getlastindexbl()
        {
            string numbc;
            string date1 = System.DateTime.Today.ToShortDateString();
            string year = date1.Substring(6, 4);
            string lastindex = DBL.incrementerBL().ToString();
            if (lastindex == "1")
            {
                numbc = year + 1.ToString("00000");

            }
            else
            {


                string lastindexyear = lastindex.Substring(0, 4);
                if (lastindexyear != year)
                {
                    numbc = year + 1.ToString("00000");
                }
                else
                {
                    numbc = DBL.incrementerBL().ToString("00000");
                }
            }
            return numbc;
        }

        public BLNOCommandeBL(int numbl)
        {
            InitializeComponent();
            clts = DC.listclts();
            //clients();
            //articles();
            Value = "update";
            dartt = new List<ligne_bl>();
            dartt.Clear();
            bon_livraison  bl = new bon_livraison();
            bl = DBL.GetBLBynum(numbl);
            //lookUpEdit1.BringToFront();
            if (bl.etat == "facturée" || bl.etat== "Annulé")
            {
                simpleButton5.Enabled = false;
                simpleButton4.Enabled = false;
                lookUpEdit3.EditValue = Convert.ToInt32(bl.id_clt);
                tnumcommandebase.Text = numbl.ToString();
                dateEdit1.DateTime = Convert.ToDateTime(bl.date_ajout);
                memoEdit1.EditValue = bl.comment;
                textEdit8.Text = bl.montant_BL.ToString();
                textEdit6.Text = bl.remise.ToString();
                textEdit9.Text = bl.prixremise.ToString();
                numcmdtext.Text = bl.id_commande;
                dartt = DBL.getLblByCodeBL(numbl);
                getalldatatable();
            }

            else
            {
                //simpleButton4.Enabled = false;
                lookUpEdit3.EditValue = Convert.ToInt32(bl.id_clt);
                tnumcommandebase.Text = numbl.ToString();
                dateEdit1.DateTime = Convert.ToDateTime(bl.date_ajout);
                memoEdit1.EditValue = bl.comment;
                textEdit8.Text = bl.montant_BL.ToString();
                textEdit6.Text = bl.remise.ToString();
                textEdit9.Text = bl.prixremise.ToString();

                dartt = DBL.getLblByCodeBL(numbl);
                getalldatatable();
            }
            dartcopie = dartt;
            
        }
        public BLNOCommandeBL(List<bon_commande> bcmds)
        {
            InitializeComponent();
            boncmds = bcmds;
            Value = "add";
            string numbl;
            numbl = getlastindexbl();
            tnumcommandebase.Text = numbl;

            this.Text = "Nouveau B.L";
            this.simpleButton4.Text = "Enregistrer B.L";
            this.simpleButton5.Text = "Annuler B.L";
          
            foreach (bon_commande bcmd in bcmds)
            {
                List<ligne_bl> lbls = new List<ligne_bl>();
                List<ligne_bc> lignebcs = new List<ligne_bc>();
                lignebcs = DBC.getLbcByCodeBC((int)bcmd.numero_bc);
                lbls=fillbl(lignebcs,numbl);
                
                dartt.AddRange(lbls);
                client clt = DC.getClientByNumero(Convert.ToInt32( bcmd.id_clt));
                if(!clts.Contains(clt))
                {
                    clts.Add(clt);
                }

                numcmdtext.Text = bcmd.numero_bc.ToString();
            }
         
            lookUpEdit3.BringToFront();
           if(clts.Count==1)
            {
                lookUpEdit3.EditValue = clts[0].codeclient;
            }
           
            getalldatatable();
            dateEdit1.DateTime = System.DateTime.Today;
            updatesum();
        }
        private void finddifference(List<ligne_bc> lbc1,List<ligne_bc> lbc2)
        {
            
        }
        private List<ligne_bl> fillbl(List<ligne_bc> lignebcs,string numbc) 
        {
           
                List<ligne_bl> lbls = new List<ligne_bl>();
                foreach (ligne_bc lbc in lignebcs)
                {  
                  
                if (lbc.qtitrest!=0)
                {

                    Double pvhorstva = 0;

                    pvhorstva = Convert.ToDouble(lbc.puv) * Convert.ToDouble(lbc.qtitrest);
                    Double prixremis = 0;
                    prixremis = pvhorstva - (pvhorstva * Convert.ToDouble(lbc.remise) / 100);
                    ligne_bl lbl = new ligne_bl();
                    lbl.code_art = lbc.code_art;
                    lbl.designation_article = lbc.designation_article;
                    lbl.quantite_article = lbc.qtitrest;
                    lbl.puv = lbc.puv;
                    lbl.total = pvhorstva;
                    lbl.id_bl = int.Parse(numbc);
                    lbl.idauteur = lbc.idauteur;
                    lbl.etatpaiement = "Non Règlé";
                    lbl.remise = lbc.remise;
                    lbl.prixremise = prixremis;
                    lbl.idcmd = lbc.id_bc;
                    lbls.Add(lbl);
                }
                
                }
                
            return lbls;
        }
        public static BLNOCommandeBL Instance()
        {
            if (instance == null)

                instance = new BLNOCommandeBL();

            return instance;

        }

        public Boolean verifierQuantite()
        {
            Boolean result = true;
            string msg = "Quantité demandée supérieure à la quantite du produit :\n";
            for (int i = 0; i < gridView1.DataRowCount; i++)
            {

                ligne_bl row = (ligne_bl)gridView1.GetRow(i);
                Livre s = DBL.GetProdByQtRest(row.code_art.ToString());
                if (s != null && double.Parse(row.quantite_article.ToString()) > (s.quantitenstock))
                {
                    result = false;
                    msg += " Quantité disponible de l'article:" + row.code_art.ToString() + " = " + s.quantitenstock + " --- Quantité demandé : " + row.quantite_article.ToString() + "\n";
                }
            }
            if (result == false)
            {
                MessageBox.Show(msg);
            }
            return result;
        }

        private void updatebl()
        {
          

                client rowView1 = (client)lookUpEdit3.GetSelectedDataRow();
                int numer_bl = int.Parse(tnumcommandebase.Text);
                bon_livraison dt_blo = new bon_livraison();
                dt_blo = DBL.GetBLBynum(numer_bl);


                if (rowView1 == null)
                {
                    XtraMessageBox.Show("Choisir un client SVP");
                }
                else
                {
                   
                        string etat = "en cours";

                        prixtotc = 0;
                        

                        DBL.deletelbl(Convert.ToInt32(tnumcommandebase.Text));

                        for (int i = 0; i < gridView1.DataRowCount; i++)
                        {
                            
                            ligne_bl lBl = new ligne_bl();
                            lBl = (ligne_bl)gridView1.GetRow(i);
                            lBl.id_bl = int.Parse(tnumcommandebase.Text);
                            DBL.ajouterLBL(lBl);

                        }
                        
                        bon_livraison Bl = new bon_livraison();                      
                        Bl.id = dt_blo.id;
                        Bl.numero_bl = dt_blo.numero_bl;
                        Bl.date_ajout = dateEdit1.DateTime.ToString();
                        Bl.etat = etat;
                        Bl.client = rowView1.raisonsoc;
                        Bl.id_clt = rowView1.codeclient.ToString();
                        Bl.montant_BL = Convert.ToDouble(textEdit8.Text.Replace('.', ','));
                        Bl.comment = memoEdit1.Text;
                        Bl.remise = Convert.ToDouble(textEdit6.Text.Replace('.', ','));
                        Bl.prixremise = Convert.ToDouble(textEdit9.Text.Replace('.', ','));
                        DBL.modifier(Bl);
                        this.Close();
                    
                }

            
        }
        private void addbl()
        {

            string idcmd;
                client rowView1 = (client)lookUpEdit3.GetSelectedDataRow();
                int numer_bl = int.Parse(tnumcommandebase.Text);

                  if (rowView1 == null)
                {
                    XtraMessageBox.Show("Choisir un client SVP");
                }
                else
                {                  
                     string etat = "en cours";                
                     for (int i = 0; i < gridView1.DataRowCount; i++)
                     { 
                            ligne_bl lBl = new ligne_bl();
                            lBl = (ligne_bl)gridView1.GetRow(i);
                            //Double total = Convert.ToDouble(lBl.quantite_article.ToString().Replace('.', ',')) * Convert.ToDouble(lBl.puv.ToString().Replace('.', ','));                         
                            lBl.id_bl = int.Parse(tnumcommandebase.Text);
                  
               
                    if (lBl.idcmd!=null)
                    {
                        ligne_bc lbc = new ligne_bc();
                        lbc = DBC.getLbcBycode((int)lBl.idcmd);

                        double qtservi = 0;
                        double qterest = 0;
                        if(lBl.quantite_article>lbc.qtitrest )
                        {
                            DialogResult dialogResult = MessageBox.Show("Vous avez insérer une quantité supèrieure à la quantité en commande! voulez vous continuer l'opération ? ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                            if (dialogResult == DialogResult.Yes)
                            {

                                double qteover =Convert.ToDouble( lBl.quantite_article )- Convert.ToDouble(lbc.qtitrest);

                                DS.reserveretlivrer(lBl.code_art, qteover);

                         
                               
                                DS.livrer(lbc.code_art,(double) lbc.qtitrest);
                                lbc.qtitrest = 0;
                                lbc.qtitservi = lbc.quantite_article;
                                DBC.modifierLBC(lbc);
                                
                                verifycmd((int)lbc.id_bc);
                                MessageBox.Show("opération réussite le stock a été mis à jour");

                            }
                            else if (dialogResult == DialogResult.No)
                            {

                            }
                          
                        }
                        else
                        {
                            qtservi = Convert.ToDouble(lBl.quantite_article);
                            qterest = Convert.ToDouble(lbc.quantite_article) - qtservi;
                            lbc.qtitrest -= lBl.quantite_article;
                            lbc.qtitservi += lBl.quantite_article;
                            DS.livrer(lbc.code_art,qtservi);
                            DBC.modifierLBC(lbc);
                            reservation rsv = new reservation();
                            rsv = reservser.getreserv(lbc.code_art,(int) lbc.id_bc);
                            rsv.quantit -= qtservi;
                            reservser.updatereservation(rsv);
                            verifycmd((int)lbc.id_bc);

                        }

                    }
                    else
                    {

                        DS.reserveretlivrer(lBl.code_art, (double)lBl.quantite_article);
                    }
                      
                    
                     DBL.ajouterLBL(lBl);
                    }                       
                        bon_livraison Bl = new bon_livraison();                       
                        Bl.numero_bl = Convert.ToInt32(tnumcommandebase.Text);
                        Bl.date_ajout = dateEdit1.DateTime.ToString();
                        Bl.etat = etat;
                        Bl.client = rowView1.raisonsoc;
                        Bl.id_clt = rowView1.codeclient.ToString();
                        Bl.montant_BL = Convert.ToDouble(textEdit8.Text.Replace('.', ','));
                        Bl.comment = memoEdit1.Text;
                        Bl.remise = Convert.ToDouble(textEdit6.Text.Replace('.', ','));
                        Bl.prixremise = Convert.ToDouble(textEdit9.Text.Replace('.', ','));
                        Bl.id_commande = numcmdtext.Text;
                        DBL.ajouterBL(Bl);
                        
                        this.Close();
                
            }
        }
        private void verifycmd(int numcmd)
        {
            int etat = 0;
            bon_commande bcmd = DBC.getAllBcbycode(numcmd);
            List<ligne_bc> lbcs = new List<ligne_bc>();
            lbcs = DBC.getLbcByCodeBC(numcmd);
            for(int i=0;i<lbcs.LongCount();i++)
            {
                if (lbcs[i].qtitrest!=0)
                {
                    etat = 1; 
                }

            }
            if(etat==0)
            {
                //modifier l etat de bon de commande de validé vers livré
                bcmd.etat = "livré";
                DBC.modifierBC(bcmd);
                reservser.deleteallreserv(numcmd);
            }

        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if(Value=="update")
            {
                updatebl();
            }
            else
            {
                addbl();
            }
         
        }
        public static void ClearAllForm(Control Ctrl)
        {
            foreach (Control ctrl in Ctrl.Controls)
            {
                BaseEdit editor = ctrl as BaseEdit;
                if (editor != null)
                    editor.EditValue = null;

                ClearAllForm(ctrl);//Recursive
            }

        }

        //private void clients()
        //{

        //    lookUpEdit1.Properties.DataSource = null;
        //    List<client> Allclients = new List<client>();
        //    Allclients = DC.listclts();

        //    lookUpEdit1.Properties.ValueMember = "codeclient";
        //    lookUpEdit1.Properties.DisplayMember = "raisonsoc";
        //    lookUpEdit1.Properties.DataSource = Allclients;
        //    lookUpEdit1.Properties.PopulateColumns();
        //    lookUpEdit1.Properties.Columns["codeclient"].Visible = false;
        //    lookUpEdit1.Properties.Columns["numerocl"].Caption = "Numéro client";
        //    lookUpEdit1.Properties.Columns["raisonsoc"].Caption = "Raison sociale";
        //    lookUpEdit1.Properties.Columns["resp"].Caption = "Responsable";
        //    lookUpEdit1.Properties.Columns["qualite"].Visible = false;
        //    lookUpEdit1.Properties.Columns["tel"].Visible = false;
        //    lookUpEdit1.Properties.Columns["mobile"].Visible = false;
        //    lookUpEdit1.Properties.Columns["adresse"].Visible = false;
        //    lookUpEdit1.Properties.Columns["codepostal"].Visible = false;
        //    lookUpEdit1.Properties.Columns["ville"].Visible = false;
        //    lookUpEdit1.Properties.Columns["web"].Visible = false;
        //    lookUpEdit1.Properties.Columns["email"].Visible = false;
        //    lookUpEdit1.Properties.Columns["fax"].Visible = false;


        //}

        private void clientscmd(List<client> Allclients)
        {

            lookUpEdit3.Properties.DataSource = null;


            lookUpEdit3.Properties.ValueMember = "codeclient";
            lookUpEdit3.Properties.DisplayMember = "raisonsoc";
            lookUpEdit3.Properties.DataSource = Allclients;
            lookUpEdit3.Properties.PopulateColumns();
            lookUpEdit3.Properties.Columns["codeclient"].Visible = false;
            lookUpEdit3.Properties.Columns["numerocl"].Caption = "Numéro client";
            lookUpEdit3.Properties.Columns["raisonsoc"].Caption = "Raison sociale";
            lookUpEdit3.Properties.Columns["resp"].Caption = "Responsable";
            lookUpEdit3.Properties.Columns["qualite"].Visible = false;
            lookUpEdit3.Properties.Columns["tel"].Visible = false;
            lookUpEdit3.Properties.Columns["mobile"].Visible = false;
            lookUpEdit3.Properties.Columns["adresse"].Visible = false;
            lookUpEdit3.Properties.Columns["codepostal"].Visible = false;
            lookUpEdit3.Properties.Columns["ville"].Visible = false;
            lookUpEdit3.Properties.Columns["web"].Visible = false;
            lookUpEdit3.Properties.Columns["email"].Visible = false;
            lookUpEdit3.Properties.Columns["fax"].Visible = false;


        }
        private void articles()
        {
            //get All client
            lookUpEdit2.Properties.DataSource = null;
            List<Livre> Allarticle = new List<Livre>();
            Allarticle = DS.getallart();

            lookUpEdit2.Properties.ValueMember = "codeart";
            lookUpEdit2.Properties.DisplayMember = "codeart";
            lookUpEdit2.Properties.DataSource = Allarticle;
            lookUpEdit2.Properties.PopulateColumns();
            lookUpEdit2.Properties.Columns["idarticle"].Visible = false;
            lookUpEdit2.Properties.Columns["codeart"].Caption = "Code article";
            lookUpEdit2.Properties.Columns["isbn"].Caption = "ISBN";
            lookUpEdit2.Properties.Columns["famille"].Visible = false;
            lookUpEdit2.Properties.Columns["idfamille"].Visible = false;
            lookUpEdit2.Properties.Columns["sousfamille"].Visible = false;
            lookUpEdit2.Properties.Columns["idsousfamille"].Visible = false;
            lookUpEdit2.Properties.Columns["titre"].Caption = "titre";
            lookUpEdit2.Properties.Columns["dateedition"].Visible = false;
            lookUpEdit2.Properties.Columns["imprimerie"].Visible = false;
            lookUpEdit2.Properties.Columns["idimprim"].Visible = false;
            lookUpEdit2.Properties.Columns["auteur"].Visible = false;
            lookUpEdit2.Properties.Columns["idauteur"].Visible = false;
            lookUpEdit2.Properties.Columns["quantitenstock"].Visible = false;
            lookUpEdit2.Properties.Columns["depotlegal"].Visible = false;
            lookUpEdit2.Properties.Columns["pvpublic"].Visible = false;
            lookUpEdit2.Properties.Columns["pvpromo"].Visible = false;
            lookUpEdit2.Properties.Columns["droitaut"].Visible = false;
            lookUpEdit2.Properties.Columns["abscice"].Visible = false;
            lookUpEdit2.Properties.Columns["ordonne"].Visible = false;
            lookUpEdit2.Properties.Columns["image"].Visible = false;


        }

        private void passerCommande_Activated(object sender, EventArgs e)
        {
            //tnumcommandebase.Text = DBL.incrementerBL().ToString();
            //clientscmd(clts);
            //articles();
        }

        private void lookUpEdit2_EditValueChanged1(object sender, EventArgs e)
        {

            Livre s = (Livre)lookUpEdit2.GetSelectedDataRow();
            if (s != null)
            {
                simpleButton1.Enabled = true;
                simpleButton2.Enabled = false;
                simpleButton3.Enabled = false;
                mdesign.Text = s.titre;
                tpu.Text = s.pvpublic.ToString();
                textEdit2.Text = s.auteur.ToString();
            }
        }



        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
                int count = gridView1.DataRowCount;
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {

                    ligne_bl lbl = (ligne_bl)gridView1.GetRow(gridView1.FocusedRowHandle);


                    lookUpEdit2.EditValue = lbl.code_art;
                    mdesign.Text = lbl.designation_article;
                    tquantit.Text = lbl.quantite_article.ToString();
                    tpu.Text = lbl.puv.ToString();
                    tremise.Text = lbl.remise.ToString();
                    textEdit1.Text = lbl.idcmd.ToString();
                    textEdit2.Text = lbl.idauteur.ToString();
                    simpleButton1.Enabled = false;
                    simpleButton2.Enabled = true;
                    simpleButton3.Enabled = true;
                }
            }
            catch (Exception except)
            { }
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                int count = gridView1.DataRowCount;
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {

                    ligne_bl lbl = (ligne_bl)gridView1.GetRow(gridView1.FocusedRowHandle);
                    dartt.Remove(lbl);
                    MessageBox.Show("la commande a été mise à jour");
                    gridControl1.DataSource = null;
                    gridView1.Columns.Clear();
                    gridControl1.DataSource = dartt;
                    updatesum();
                    lookUpEdit2.EditValue = null;
                    mdesign.Text = "";
                    tpu.Text = null;
                    tremise.Text = null;
                    tquantit.Text = null;

                }

            }
            catch (Exception exc)
            { }
        }

        private void getalldatatable()
        {
           

            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = dartt;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Visible = false;
            gridView1.Columns[2].Caption = "Code";
            gridView1.Columns[3].Caption = "Titre";
            gridView1.Columns[4].Caption = "Quantite";
            gridView1.Columns[5].Caption = "puv";
            gridView1.Columns[6].Caption = "Total";
            gridView1.Columns[7].Caption = "Remise";
            gridView1.Columns[8].Caption = "A payer";
            gridView1.Columns[9].Visible = false;
            gridView1.Columns[10].Visible = false;
            gridView1.Columns[11].Caption = "numcmd";
        }

        private void panelControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BLNOCommandeBL_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }
        double tot = 0;
        double tot_rem = 0;
        private void verifarticle(Livre art, double qte)
        {
            int find = 0;
            long count = 0;
            count = dartt.LongCount();
            if (count != 0)
            {
                foreach (ligne_bl lbl in dartt)
                {

                    if (lbl.code_art == art.codeart)
                    {
                        DialogResult dialogResult = MessageBox.Show("L'article est déja ajouté à la livraison !! 'Oui' pour additioner la quantité 'Non' pour annuler   ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dialogResult == DialogResult.Yes)
                        {
                            lbl.quantite_article += qte;
                            Double pvhorstva = 0;
                            pvhorstva = Convert.ToDouble(tpu.Text.Replace('.', ',')) * Convert.ToDouble(lbl.quantite_article);
                            Double prixremis = 0;
                            prixremis = pvhorstva - (pvhorstva * Convert.ToDouble(tremise.Text.Replace('.', ',')) / 100);
                            lbl.total = pvhorstva;

                            lbl.remise = Convert.ToDouble(tremise.Text.Replace('.', ','));
                            lbl.prixremise = prixremis;
                            getalldatatable();
                            updatesum();
                            lookUpEdit2.EditValue = null;
                            mdesign.Text = null;
                            tquantit.Text = null;
                            tremise.Text = null;
                            tpu.Text = null;
                            find = 1;
                            break;

                        }
                        else if (dialogResult == DialogResult.No)
                        {
                            break;
                        }

                    }

                }
                if (find != 1)
                {
                    Double pvhorstva = 0;
                    pvhorstva = Convert.ToDouble(tpu.Text.Replace('.', ',')) * Convert.ToDouble(tquantit.Text.Replace('.', ','));
                    Double prixremis = 0;
                    prixremis = pvhorstva - (pvhorstva * Convert.ToDouble(tremise.Text.Replace('.', ',')) / 100);
                    //Double pvtva = prixremis + ((prixremis * Convert.ToDouble(ttva.Text.Replace('.', ','))) / 100);
                    ligne_bl lBl = new ligne_bl();
                    lBl.code_art = art.codeart; ;
                    lBl.designation_article = mdesign.Text;
                    lBl.quantite_article = Convert.ToDouble(tquantit.Text.Replace('.', ','));
                    lBl.puv = Convert.ToDouble(tpu.Text.Replace('.', ','));
                    lBl.total = pvhorstva;
                    lBl.id_bl = int.Parse(tnumcommandebase.Text);
                    lBl.remise = Convert.ToDouble(tremise.Text.Replace('.', ','));
                    lBl.prixremise = prixremis;
                    lBl.idauteur = Convert.ToInt32(textEdit2.Text);
                    dartt.Add(lBl);
                    getalldatatable();

                    updatesum();
                    lookUpEdit2.EditValue = null;
                    mdesign.Text = null;
                    tquantit.Text = null;
                    tremise.Text = null;
                    tpu.Text = null;

                }

            }
            else
            {
                Double pvhorstva = 0;

                pvhorstva = Convert.ToDouble(tpu.Text.Replace('.', ',')) * Convert.ToDouble(tquantit.Text.Replace('.', ','));
                Double prixremis = 0;
                prixremis = pvhorstva - (pvhorstva * Convert.ToDouble(tremise.Text.Replace('.', ',')) / 100);
                //Double pvtva = prixremis + ((prixremis * Convert.ToDouble(ttva.Text.Replace('.', ','))) / 100);

                ligne_bl lBl = new ligne_bl();
                lBl.code_art = art.codeart; ;
                lBl.designation_article = mdesign.Text;
                lBl.quantite_article = Convert.ToDouble(tquantit.Text.Replace('.', ','));
                lBl.puv = Convert.ToDouble(tpu.Text.Replace('.', ','));
                lBl.total = pvhorstva;
                lBl.id_bl = int.Parse(tnumcommandebase.Text);
                lBl.remise = Convert.ToDouble(tremise.Text.Replace('.', ','));
                lBl.prixremise = prixremis;
                lBl.idauteur = Convert.ToInt32(textEdit1.Text);
                dartt.Add(lBl);
                getalldatatable();
                updatesum();
                lookUpEdit2.EditValue = null;
                mdesign.Text = null;
                tquantit.Text = null;
                tremise.Text = null;
                tpu.Text = null;

            }
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            try
            {

                Livre s = (Livre)lookUpEdit2.GetSelectedDataRow();
                client c = (client)lookUpEdit3.GetSelectedDataRow();

                if (DS.checkavailab(s.codeart, Convert.ToDouble(tquantit.Text.Replace('.', ','))) == 1)
                {

                    verifarticle(s, Convert.ToDouble(tquantit.Text));

                }
                else
                {
                    List<reservation> reservs = new List<reservation>();
                    reservs = reservser.getallreservbyarticle(s.codeart);
                    if (reservs.LongCount() == 0)
                    {
                        MessageBox.Show("La quantité demandé n'est pas disponible en stock,la quantité en stock de l'article" + s.titre + "est de " + s.quantitenstock + " .Veuillez ne pas dépasser cette quantité ");

                    }
                    else
                    {

                        foreach (reservation rsv in reservs)
                        {
                            bon_commande bcmd = new bon_commande();
                            bcmd = DBC.GetAllBcBynum((Int32)rsv.ncmd);
                            if (bcmd.etat == "en cours")
                            {

                                MessageBox.Show("L'article " + s.titre + " est réservée par la commande " + bcmd.numero_bc + "qui n'est pas encore validé.\n veuillez valider ou annuler les commandes en cours");

                            }

                        }


                    }
                }

        }
            catch (Exception except)
            {
                MessageBox.Show("vérifier les valeurs entrées");
            }

}
        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            try
            {
                Double pv = Convert.ToDouble(tpu.Text.Replace('.', ',')) * Convert.ToDouble(tquantit.Text.Replace('.', ','));
                Double totalremise = pv - ((pv * Convert.ToDouble(tremise.Text.Replace('.', ','))) / 100);
                Livre s = (Livre)lookUpEdit2.GetSelectedDataRow();
                client c = (client)lookUpEdit3.GetSelectedDataRow();
                ligne_bl lbl = (ligne_bl)gridView1.GetRow(gridView1.FocusedRowHandle);
                double qtenew = 0;
                qtenew = Convert.ToDouble(tquantit.Text.Replace('.', ',')) - Convert.ToDouble(lbl.quantite_article);
                if (qtenew <= 0)
                {
                    int a = dartt.IndexOf(lbl);
                    dartt.RemoveAt(a);
                    string codep = s.codeart;
                    //string codeclt = c.numerocl.ToString(); 
                    lbl = new ligne_bl();
                    lbl.code_art = s.codeart;
                    lbl.designation_article = mdesign.Text;
                    lbl.quantite_article = Convert.ToDouble(tquantit.Text.Replace('.', ','));
                    lbl.puv = Convert.ToDouble(tpu.Text.Replace('.', ','));
                    lbl.remise = Convert.ToDouble(tremise.Text.Replace('.', ','));
                    lbl.total = pv;
                    lbl.prixremise = totalremise;
                    lbl.idcmd = Convert.ToInt32(textEdit1.Text);
                    lbl.etatpaiement = "Non Règlé";
                    lbl.id_bl = int.Parse(tnumcommandebase.Text);
                    lbl.idauteur = Convert.ToInt32(textEdit2.Text);
                    dartt.Insert(a, lbl);//.Add(lbl);
                    getalldatatable();
                    updatesum();
                    lookUpEdit2.EditValue = null;
                    mdesign.Text = "";
                    tpu.Text = null;
                    tquantit.Text = null;
                    tremise.Text = null;

                }
                else
                {

                    if (DS.checkavailab(s.codeart, qtenew) == 1)
                    {
                        int a = dartt.IndexOf(lbl);
                        dartt.RemoveAt(a);
                        string codep = s.codeart;
                        //string codeclt = c.numerocl.ToString(); 
                        lbl = new ligne_bl();
                        lbl.code_art = s.codeart;
                        lbl.designation_article = mdesign.Text;
                        lbl.quantite_article = Convert.ToDouble(tquantit.Text.Replace('.', ','));
                        lbl.puv = Convert.ToDouble(tpu.Text.Replace('.', ','));
                        lbl.remise = Convert.ToDouble(tremise.Text.Replace('.', ','));
                        lbl.total = pv;
                        lbl.prixremise = totalremise;
                        lbl.idcmd = Convert.ToInt32(textEdit1.Text);
                        lbl.etatpaiement = "Non Règlé";
                        lbl.id_bl = int.Parse(tnumcommandebase.Text);
                        lbl.idauteur = Convert.ToInt32(textEdit2.Text);
                        dartt.Insert(a, lbl);//.Add(lbl);
                        getalldatatable();
                        updatesum();
                        lookUpEdit2.EditValue = null;
                        mdesign.Text = "";
                        tpu.Text = null;
                        tquantit.Text = null;
                        tremise.Text = null;

                    }

                    else
                    {
                        List<reservation> reservs = new List<reservation>();
                        reservs = reservser.getallreservbyarticle(s.codeart);
                        if (reservs.LongCount() == 0)
                        {
                            MessageBox.Show("La quantité demandé n'est pas disponible en stock,la quantité en stock de l'article" + s.titre + "est de " + s.quantitenstock + " .Veuillez ne pas dépasser cette quantité ");

                        }
                        else
                        {

                            foreach (reservation rsv in reservs)
                            {
                                bon_commande bcmd = new bon_commande();
                                bcmd = DBC.GetAllBcBynum((Int32)rsv.ncmd);
                                if (bcmd.etat == "en cours")
                                {

                                    MessageBox.Show("L'article " + s.titre + " est réservée par la commande " + bcmd.numero_bc + "qui n'est pas encore validé.\n veuillez valider ou annuler les commandes en cours");

                                }

                            }


                        }
                    }
                }

                }
                catch (Exception except)
                {
                    MessageBox.Show("vérifier les valeurs entrées");
                }
            }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textEdit6_EditValueChanged(object sender, EventArgs e)
        {
            updatesum();
        }

        public void updatesum()
        {
            try
            {
                prixtotc = 0;
                prix_rem = 0;
                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    ligne_bl lbl = (ligne_bl)gridView1.GetRow(i);
                    prixtotc += Convert.ToDouble(lbl.prixremise.ToString().Replace('.', ','));
                    textEdit8.Text = prixtotc.ToString();
                    prix_rem = prixtotc - prixtotc * (Convert.ToDouble(textEdit6.Text)) / 100;
                    textEdit9.Text = prix_rem.ToString();
                }

            


            }
            catch (Exception xce)
            {
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BLNOCommandeBL_Load(object sender, EventArgs e)
        {
            try
            {
                //tnumcommandebase.Text = DBL.incrementerBL().ToString();
                clientscmd(clts);
                //clients();
                articles();
                simpleButton1.Enabled = true;
                simpleButton2.Enabled = false;
                simpleButton3.Enabled = false;
            }
            catch (Exception excep)
            { }
        }


    }
}
