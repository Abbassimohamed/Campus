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
using DAL;
using BLL;
using CAMPUSMANOUBA.Report;
using DevExpress.XtraReports.UI;

namespace CAMPUSMANOUBA
{
    public partial class Gestionbonlivraison : DevExpress.XtraEditors.XtraForm
    {
        public Gestionbonlivraison()
        {
            InitializeComponent();
            dartt = new List<ligne_bl>();
            dartt.Clear();
            getlastindex();

        }

        public static int id_bl;
        List<ligne_bl> dartt = new List<ligne_bl>();
        public static Double prixtotc, prix_rem;
        public static Double test;
        public static int numbl, id_clt = 0;
        public static int idpiece = 0, idrow = 0;
        BLService DBL = new BLService();
        ArtService artser = new ArtService();
        reservationservice reservser = new reservationservice();
        BCService DBc = new BCService();
        ClientService DC = new ClientService();
        ArtService DS = new ArtService();
        private static BLNOCommandeBL  newbl;
        private static listebls listbl;
        private static Gestionbonlivraison instance;
        public static Gestionbonlivraison Instance()
        {
            if (instance == null)

            instance = new Gestionbonlivraison();
            return instance;

        }
        private void getlastindex()
        {
            string date1 = System.DateTime.Today.ToShortDateString();
            string year = date1.Substring(6, 4);
            string lastindex = DBL.incrementerBL().ToString();
            if(lastindex=="1")
            {
                tnumcommandebase.Text = year + 1.ToString("00000");

            }
            else
            {

            
            string lastindexyear = lastindex.Substring(0, 4);
            if (lastindexyear != year)
            {
                tnumcommandebase.Text = year + 1.ToString("00000");
            }
            else
            {
                tnumcommandebase.Text = DBL.incrementerBL().ToString("00000");
            }
            }
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
        private void getallistCmds(List<bon_commande> bcmds)
        {

            gridControl5.DataSource = null;
            gridView5.Columns.Clear();
            gridControl5.DataSource = bcmds;
            gridView5.Columns[0].Visible = false;
            gridView5.Columns[1].Caption = "Numéro B.C";
            gridView5.Columns[2].Caption = "Date";
            gridView5.Columns[3].Caption = "Etat";
            gridView5.Columns[4].Visible = false;
            gridView5.Columns[5].Caption = "Remise";
            gridView5.Columns[6].Caption = "Total";
            gridView5.Columns[7].Visible = false;
            gridView5.Columns[8].Caption = "Client";
            gridView5.Columns[9].Caption = "N° devis";
            gridView5.Columns[10].Caption = "Commentaire";

        }
        private void Duplicata_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }
        private void Liste_bl_clt(List<bon_livraison> bls)
        {          
            gridControl2.DataSource = null;
            gridView2.Columns.Clear();
            gridControl2.DataSource = bls;
            this.gridView2.Columns[0].Visible = false;
            this.gridView2.Columns[1].Caption = "Numéro B.L";
            this.gridView2.Columns[2].Caption = "Date Ajout";
            this.gridView2.Columns[3].Caption = "Etat";
            this.gridView2.Columns[4].Caption = "Montant";
            this.gridView2.Columns[5].Caption = "Remise" ;
            this.gridView2.Columns[6].Caption = "Total"; 
            this.gridView2.Columns[7].Visible = false;
            this.gridView2.Columns[8].Caption = "Client";
            this.gridView2.Columns[9].Visible = false;
            this.gridView2.Columns[10].Visible = false;
            this.gridView2.Columns[11].Visible = false;

            gridView2.OptionsView.ShowAutoFilterRow = true;
            //updatesum();
        }
        private void Liste_bl_clt1(List<bon_livraison> bls)
        {
            gridControl3.DataSource = null;
            gridView3.Columns.Clear();
            gridControl3.DataSource = bls;
            this.gridView3.Columns[0].Visible = false;
            this.gridView3.Columns[1].Caption = "Numéro B.L";
            this.gridView3.Columns[2].Caption = "Date Ajout";
            this.gridView3.Columns[3].Caption = "Etat";
            this.gridView3.Columns[4].Caption = "Montant";
            this.gridView3.Columns[5].Caption = "Remise";
            this.gridView3.Columns[6].Caption = "Total";
            this.gridView3.Columns[7].Visible = false;
            this.gridView3.Columns[8].Caption = "Client";
            this.gridView3.Columns[9].Visible = false;
            this.gridView3.Columns[10].Visible = false;
            this.gridView3.Columns[11].Visible = false;

            gridView3.OptionsView.ShowAutoFilterRow = true;
            //updatesum();
        }
        private void Liste_bl_clt2(List<bon_livraison> bls)
        {
            gridControl4.DataSource = null;
            gridView4.Columns.Clear();
            gridControl4.DataSource = bls;
            this.gridView4.Columns[0].Visible = false;
            this.gridView4.Columns[1].Caption = "Numéro B.L";
            this.gridView4.Columns[2].Caption = "Date Ajout";
            this.gridView4.Columns[3].Caption = "Etat";
            this.gridView4.Columns[4].Caption = "Montant";
            this.gridView4.Columns[5].Caption = "Remise";
            this.gridView4.Columns[6].Caption = "Total"; 
            this.gridView4.Columns[7].Visible = false;
            this.gridView4.Columns[8].Caption = "Client";
            this.gridView4.Columns[9].Visible = false;
            this.gridView4.Columns[10].Visible = false;
            this.gridView4.Columns[11].Visible = false;
            gridView4.OptionsView.ShowAutoFilterRow = true;
            //updatesum();
        }
        private void Duplicata_Activated(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage1;
        }
        public  void updatesum()
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
        private void tileItem1_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage1;
        }
        private void tileItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage2;
        }
        private void Gestioncommande_Load(object sender, EventArgs e)
        {
            getlastindex();
            clients();
            articles();
            simpleButton1.Enabled = true;
            simpleButton2.Enabled = false;
            simpleButton3.Enabled = false;
        }
        private void tileItem3_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage3;
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            if(xtraTabControl1.SelectedTabPage==xtraTabPage1)
            {
                getlastindex();
                clients();
                articles();
            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPage2)
            {
                List<bon_livraison> bls = new List<bon_livraison>();               
                bls = DBL.getAllBLbytype("en cours");
                Liste_bl_clt(bls);
            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPage3)
            {
                List<bon_livraison> bls = new List<bon_livraison>();
                bls = DBL.getAllBLbytype("facturée");
                Liste_bl_clt1(bls);
            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPage4)
            {
                List<bon_livraison> bls = new List<bon_livraison>();
                bls = DBL.getAllBLnotfacured();
                Liste_bl_clt2(bls);
            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPage5)
            {
                List<bon_commande> bcmds = new List<bon_commande>();
                bcmds = DBc.getAllBbyetat("validé");
                getallistCmds(bcmds);
            }

        }
        private void clients()
        {

            lookUpEdit1.Properties.DataSource = null;
            List<client> Allclients = new List<client>();
            Allclients = DC.listclts();

            lookUpEdit1.Properties.ValueMember = "numerocl";
            lookUpEdit1.Properties.DisplayMember = "raisonsoc";
            lookUpEdit1.Properties.DataSource = Allclients;
            lookUpEdit1.Properties.PopulateColumns();
            lookUpEdit1.Properties.Columns["codeclient"].Visible = false;
            lookUpEdit1.Properties.Columns["numerocl"].Caption = "Numéro client";
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
        private void articles()
        {
            //get All client
            lookUpEdit2.Properties.DataSource = null;
            List<Livre> Allarticle = new List<Livre>();
            Allarticle = DS.getallart();

            lookUpEdit2.Properties.ValueMember = "codeart";
            lookUpEdit2.Properties.DisplayMember = "titre";
            lookUpEdit2.Properties.DataSource = Allarticle;
            lookUpEdit2.Properties.PopulateColumns();
            lookUpEdit2.Properties.Columns["Rang"].Visible = false;
            lookUpEdit2.Properties.Columns["codeart"].Visible = false;
            lookUpEdit2.Properties.Columns["titre"].Caption = "titre";
            lookUpEdit2.Properties.Columns["dateedition"].Visible = false;
            lookUpEdit2.Properties.Columns["imprimerie"].Visible = false;
            lookUpEdit2.Properties.Columns["auteur"].Visible = false;
            lookUpEdit2.Properties.Columns["isbn"].Caption = "ISBN";
            lookUpEdit2.Properties.Columns["codeabarre"].Visible = false;
            lookUpEdit2.Properties.Columns["quantitenstock"].Visible = false;
            lookUpEdit2.Properties.Columns["prixachat"].Visible = false;
            lookUpEdit2.Properties.Columns["pvpublic"].Visible = false;
            lookUpEdit2.Properties.Columns["pvetudiant"].Visible = false;
            lookUpEdit2.Properties.Columns["pvfoire"].Visible = false;
            lookUpEdit2.Properties.Columns["pvdepositaire"].Visible = false;
            lookUpEdit2.Properties.Columns["image"].Visible = false;
            lookUpEdit2.Properties.Columns["emplacement"].Visible = false;



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
        public void validateform(Control Ctrl)
        {
            foreach (Control ctrl in Ctrl.Controls)
            {
                BaseEdit editor = ctrl as BaseEdit;
                if (editor == null)
                    editor.EditValue = "";


            }
        }
        private void tileItem4_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage4;
        }

        private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {
            Livre s = (Livre)lookUpEdit2.GetSelectedDataRow();
            if (s != null)
            {
                textEdit3.Text = s.codeart;
                tpu.Text = s.pvpublic.ToString();
                textEdit1.Text = s.auteur.ToString();
                simpleButton1.Enabled = true;
                simpleButton2.Enabled = false;
                simpleButton3.Enabled = false;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            try
            {

                Livre s = (Livre)lookUpEdit2.GetSelectedDataRow();
                client c = (client)lookUpEdit1.GetSelectedDataRow();

                if (artser.checkavailab(s.codeart, Convert.ToDouble(tquantit.Text.Replace('.', ','))) == 1)
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
                            bcmd = DBc.GetAllBcBynum((Int32)rsv.ncmd);
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
                           
                            tquantit.Text = null;
                            tremise.Text = null;
                            tpu.Text = null;
                            find = 1;
                            break;

                        }
                        else if (dialogResult == DialogResult.No)
                        {

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
                    lBl.designation_article = lookUpEdit2.EditValue.ToString();
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
                lBl.designation_article = lookUpEdit2.EditValue.ToString();
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
             
                tquantit.Text = null;
                tremise.Text = null;
                tpu.Text = null;

            }
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
            gridView1.Columns[11].Visible = false;

        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                Double pv = Convert.ToDouble(tpu.Text.Replace('.', ',')) * Convert.ToDouble(tquantit.Text.Replace('.', ','));
                double total = pv - pv * Convert.ToDouble(tremise.Text.Replace('.', ',')) / 100;

                Livre s = (Livre)lookUpEdit2.GetSelectedDataRow();
                client c = (client)lookUpEdit1.GetSelectedDataRow();

                ligne_bl lbl = (ligne_bl)gridView1.GetRow(gridView1.FocusedRowHandle);
                int a = dartt.IndexOf(lbl);
                dartt.RemoveAt(a);
                string codep = s.codeart;
                //string codeclt = c.numerocl.ToString(); 

                lbl = new ligne_bl();
                lbl.code_art = s.codeart;
                lbl.designation_article = lookUpEdit2.EditValue.ToString();
                lbl.quantite_article = Convert.ToDouble(tquantit.Text.Replace('.', ','));
                lbl.puv = Convert.ToDouble(tpu.Text.Replace('.', ','));

                lbl.total = pv;
                lbl.id_bl = int.Parse(tnumcommandebase.Text);
                lbl.etatpaiement = "Non Règlé";
                lbl.remise = Convert.ToDouble(tremise.Text.Replace('.', ','));
                lbl.prixremise = total;
                dartt.Insert(a, lbl);//.Add(lbl);

                getalldatatable();
                updatesum();
                lookUpEdit2.EditValue = null;
               
                tpu.Text = null;

                tquantit.Text = null;
                tremise.Text = null;
            }
            catch (Exception et) { }
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
                  
                    tquantit.Text = lbl.quantite_article.ToString();
                    tpu.Text = lbl.puv.ToString();
                    textEdit1.Text = lbl.idauteur.ToString();
                    simpleButton1.Enabled = false;
                    simpleButton2.Enabled = true;
                    simpleButton3.Enabled = true;

                }
            }
            catch (Exception except)
            { }
        }
        private void pictureEdit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void simpleButton6_Click(object sender, EventArgs e)
        {
           
            List<int> LidBl = new List<int>();
            foreach (int i in gridView2.GetSelectedRows())
            {
                
                bon_livraison BL = (bon_livraison)gridView2.GetRow(i);
                if (BL.etat == "validée")
                {
                    LidBl.Add(Convert.ToInt32(BL.numero_bl.ToString()));
                }
                else if (BL.etat == "en cours")
                {
                    XtraMessageBox.Show("BL " + BL.numero_bl + " non validée");
                }
                else
                    XtraMessageBox.Show("BL " + BL.numero_bl + " déja Facturée");
            }
            if (LidBl.Count != 0)
            {

                FactureVente factvent = new FactureVente(LidBl);
                factvent.ShowDialog();

            }
           
        }
        private void gridControl2_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {

                int count = gridView2.DataRowCount;
                if (count != 0 && gridView2.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    bon_livraison row = (bon_livraison)gridView2.GetRow(gridView2.FocusedRowHandle);
                    id_bl = Convert.ToInt32(row.id);
                    Point pt = this.Location;
                    pt.Offset(this.Left + e.X, this.Top + e.Y);
                    popupMenu1.ShowPopup(this.barManager1, Control.MousePosition);
                }
            }
        }
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            upadetall();
        }
        private void upadetall()
        {
            int count = gridView2.DataRowCount;
            if (count != 0 && gridView2.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                bon_livraison row = (bon_livraison)gridView2.GetRow(gridView2.FocusedRowHandle);
                string etat = row.etat;
                if (etat == "validée")
                {
                    XtraMessageBox.Show("Ce Bl est déja validée");
                }
                else
                {
                   
                    id_bl = Convert.ToInt32(row.numero_bl);
                    bon_livraison bl = DBL.GetBLBynum(id_bl);
                    if (bl != null)
                    {

                        List<ligne_bl> lbl = DBL.getLblByCodeBL(Convert.ToInt32(bl.numero_bl));
                        foreach (ligne_bl rw in lbl)
                        {
                            Livre s = DS.getArticleByCode(rw.code_art.ToString());
                            double q = Convert.ToDouble(s.quantitenstock) - Convert.ToDouble(rw.quantite_article.ToString());
                            s.quantitenstock = q;
                            DS.updateart(s);                           
                        }
                        bl.etat = "validée";
                        DBL.modifier(bl);

                        List<bon_livraison> bls = new List<bon_livraison>();
                        bls = DBL.getAllBL();

                        Liste_bl_clt(bls);
                    }

                    
                }
            }
        }
        private void gridControl2_Click(object sender, EventArgs e)
        {
            try
            { int count = gridView2.DataRowCount;
                if (count != 0 && gridView2.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                bon_livraison bliv = (bon_livraison)gridView2.GetRow(gridView2.FocusedRowHandle);
                memoEdit2.Text = bliv.comment;
            }

            }
            catch (Exception except)
            {

            }
        }

        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int count = gridView2.DataRowCount;
                if (count != 0 && gridView2.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    bon_livraison bliv = (bon_livraison)gridView2.GetRow(gridView2.FocusedRowHandle);

                    BLNOCommandeBL bl = new BLNOCommandeBL(Convert.ToInt32(bliv.numero_bl));
                    bl.ShowDialog();
                  
                    
                }

            }
            catch (Exception except)
            {

            }
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            List<bon_livraison> bls = new List<bon_livraison>();
            bls = DBL.getAllBL();

            Liste_bl_clt(bls);
        }

        private void simpleButton6_Click_1(object sender, EventArgs e)
        {
            upadetall();
        }

        private void tileItem5_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage5;
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
           
            int count = gridView5.GetSelectedRows().Count();
            if (count != 0)
            {
                DialogResult dialogResult = MessageBox.Show("Voulez vous confirmer la livraison en cours ?!!", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (dialogResult == DialogResult.Yes)
                {
                    List<bon_commande> bcmds = new List<bon_commande>();

                    foreach (int i in gridView5.GetSelectedRows())
                    {
                        bon_commande bcmd = (bon_commande)gridView5.GetRow(i);
                        bcmds.Add(bcmd);


                    }
                    BLNOCommandeBL cmdbl = new BLNOCommandeBL(bcmds);
                    cmdbl.ShowDialog();
                }
                else if (dialogResult == DialogResult.No)
                {

                }

                else
                {
                    MessageBox.Show("Veuillez sélèctionner au moins une commande");

                }
                
            }
       
        }
     
        private void simpleButton12_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Vous etes sure de valider la commande en cours?!!", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                int count = gridView2.DataRowCount;
                if (count != 0 && gridView2.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    bon_commande row = (bon_commande)gridView2.GetRow(gridView2.FocusedRowHandle);
                    upadetall(Convert.ToInt32(row.numero_bc));
                }

            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }
        private void upadetall(int num)
        {

            bon_commande bcmd = DBc.GetAllBcBynum(num);

            if (bcmd != null)
            {

                if (bcmd.etat == "validé")
                {
                    XtraMessageBox.Show("Cette bon de commande est déja validé");
                }
                else if (bcmd.etat == "en cours")
                {

                    bcmd.etat = "validé";
                    DBc.modifierBC(bcmd);

                    List<bon_commande> bcmds = new List<bon_commande>();
                    bcmds = DBc.getAllBc();
                    getallistCmds(bcmds);

                }
                else if (bcmd.etat == "Annulé")
                {
                    MessageBox.Show("Vous ne pouvez pas validé une commande annulé");

                }
                
            }
        }
        private void upadetallANNULER(int num)
        {

            bon_commande bcmd = DBc.GetAllBcBynum(num);
            List<reservation> reservations = new List<reservation>();
            reservations = reservser.getallreservationbycmd((int)bcmd.numero_bc);
          
            if (bcmd != null)
            {

                if (bcmd.etat == "Annulé" || bcmd.etat == "validé")
                {
                    XtraMessageBox.Show("Cette bon de commande est déja Annulée");
                }
                else
                {

                    bcmd.etat = "Annulé";
                    DBc.modifierBC(bcmd);

                    foreach (reservation reserv in reservations)
                    {
                        DS.annulerreserver(reserv.article, Convert.ToDouble(reserv.quantit));

                    }

                    MessageBox.Show("La commande a été annulé.La quantité est disponible en stock");
                    List<bon_commande> bcmds = new List<bon_commande>();
                    bcmds = DBc.getAllBc();
                    getallistCmds(bcmds);

                }


            }
        }
        private void ANNULERBL(int num)
        {

            bon_livraison bliv = DBL.GetBLBynum(num);
       

            if (bliv != null)
            {

                if (bliv.etat == "Annulé" || bliv.etat == "validé")
                {
                    if(bliv.etat== "Annulé")
                    {
                        XtraMessageBox.Show("Cet bon de commande est déja Annulé");
                    }
                    else if(bliv.etat == "validé")
                    {
                        XtraMessageBox.Show("La quantité est déja livré veuillez passer un avoir  ");
                    }
                   
                }
                else
                {

                    bliv.etat = "Annulé";
                    DBL.modifier(bliv);

                List<ligne_bl> blivs=new List<ligne_bl>();
                    blivs = DBL.getLblByCodeBL(num);
                    foreach(ligne_bl lbl in blivs)
                    {
                       if(lbl.idcmd ==null)
                        {
                            DS.annulerreservbliv(lbl.code_art, Convert.ToDouble(lbl.quantite_article));
                        }
                       else
                        {
                            DS.annulerreservblivcmdstay(lbl.code_art, Convert.ToDouble(lbl.quantite_article));
                        }
                       
                    }
                       

                  

                    MessageBox.Show("La livraison a été annulée.La quantité est disponible en stock");
                    List<bon_livraison> blivrs = new List<bon_livraison>();
                    blivrs = DBL.getAllBL();
                    Liste_bl_clt(blivrs);

                }


            }
        }
        private void simpleButton11_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Vous etes sure d'annuler la commande en cours?!!", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                int count = gridView2.DataRowCount;
                if (count != 0 && gridView2.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    bon_commande row = (bon_commande)gridView2.GetRow(gridView2.FocusedRowHandle);
                    upadetallANNULER((int)row.numero_bc);

                }

            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Vous etes sure d'annuler le bon de livraison en cours?!!", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                int count = gridView2.DataRowCount;
                if (count != 0 && gridView2.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    bon_livraison row = (bon_livraison)gridView2.GetRow(gridView2.FocusedRowHandle);
                    ANNULERBL((int)row.numero_bl);

                }

            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void textEdit3_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                lookUpEdit2.EditValue = textEdit3.Text;
            }
            catch (Exception except)
            {

            }
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
                    ClearAllForm(this);
                   
                    tpu.Text = null;
                    tquantit.Text = null;
                    tremise.Text = null;

                }

            }
            catch (Exception exc)
            { }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            if (verifierQuantite())
            {

                client rowView1 = (client)lookUpEdit1.GetSelectedDataRow();
                int numer_bl = int.Parse(tnumcommandebase.Text);
                bon_livraison dt_blo = new bon_livraison();
                dt_blo = DBL.GetBLBynum(numer_bl);
                if (dt_blo != null)
                {
                    XtraMessageBox.Show("Il existe un bon livraison avec ce numéro");
                }
                else
                {
                    if (rowView1 == null)
                    {
                        XtraMessageBox.Show("Choisir un client SVP");
                    }
                    else
                    {
                        if (tnumcommandebase.Text == "")
                        {
                            MessageBox.Show("Entrer le numero de BL");
                        }
                        else
                        {
                            string etat = "en cours";

                            test = 0;
                          
                            prixtotc = 0;
                            double prix_rem = 0;

                            for (int i = 0; i < gridView1.DataRowCount; i++)
                            {
                                ligne_bl lBl = new ligne_bl();
                                lBl = (ligne_bl)gridView1.GetRow(i);
                               
                                prixtotc += (double)lBl.prixremise  ;                             
                                lBl.id_bl = int.Parse(tnumcommandebase.Text);
                                DBL.ajouterLBL(lBl);
                                artser.reserveretlivrer(lBl.code_art, Convert.ToDouble(lBl.quantite_article));
                            }
                            bon_livraison Bl = new bon_livraison();
                            client dt = new client();
                            dt = (client)lookUpEdit1.GetSelectedDataRow();
                            Bl.numero_bl = int.Parse(tnumcommandebase.Text);
                            Bl.date_ajout = dateEdit1.DateTime.ToString();
                            Bl.etat = etat;
                            Bl.client = dt.raisonsoc;
                            Bl.id_clt = dt.codeclient.ToString();
                            Bl.montant_BL = prixtotc;
                            Bl.remise = Convert.ToDouble(tremise.Text.Replace('.', ','));
                            Bl.prixremise = Convert.ToDouble(textEdit9.Text);
                            Bl.comment = memoEdit1.Text;
                            DBL.ajouterBL(Bl);
                            BlReport blrep = new BlReport((int)Bl.numero_bl);
                            blrep.ShowRibbonPreview();
                            ClearAllForm(this);
                            gridControl1.DataSource = null;
                            gridView1.Columns.Clear();
                            dartt.Clear();
                            getlastindex();

                        }
                    }
                }
            }
        }
    }
}