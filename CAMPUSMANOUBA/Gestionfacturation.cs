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

namespace CAMPUSMANOUBA
{
    public partial class Gestionfacturation : DevExpress.XtraEditors.XtraForm
    {
        public Gestionfacturation()
        {
            InitializeComponent();
            dartt = new List<piece_fact>();
            dartt.Clear();
            getlastindex();
            simpleButton16.Enabled = true;
            simpleButton17.Enabled = false;
            simpleButton15.Enabled = false;
        }

        private void getlastindex()
        {
            string date1 = System.DateTime.Today.ToShortDateString();
            string year = date1.Substring(6, 4);
            string lastindex = DBL.NumFact().ToString();
            if (lastindex == "1")
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
                    tnumcommandebase.Text = DBL.NumFact().ToString("00000");
                }
            }
        }

        List<piece_fact> dartt = new List<piece_fact>();
        public static Double prixtotc, prix_rem;
        ArtService DS = new ArtService();
        DevisService devisser = new DevisService();
        BCService bcser = new BCService();
        ClientService DC = new ClientService();
        public static int id_bl;
        BLService DBL = new BLService();
        //private static listebls newclt;
        //private static listefactures listclt;
        private static Gestionfacturation instance;
        public static Gestionfacturation Instance()
        {
            if (instance == null)

                instance = new Gestionfacturation();

            return instance;

        }
        private void Duplicata_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }
        private void Liste_devis(List<devis> bls)
        {
            gridControl6.DataSource = null;
            gridView6.Columns.Clear();
            gridControl6.DataSource = bls;
            this.gridView6.Columns[0].Visible = false;
            this.gridView6.Columns[1].Caption = "Numéro devis";
            this.gridView6.Columns[2].Caption = "Date Ajout";
            this.gridView6.Columns[3].Visible = false;
            this.gridView6.Columns[4].Caption = "Client";
            this.gridView6.Columns[5].Visible = false;
            this.gridView6.Columns[6].Caption = "Remise";
            this.gridView6.Columns[7].Caption = "Montant";
            this.gridView6.Columns[8].Caption = "etat";
            this.gridView6.Columns[9].Caption = "Comment";
            gridView6.OptionsView.ShowAutoFilterRow = true;
            //updatesum();
        }
        private void Duplicata_Activated(object sender, EventArgs e)
        {
          
        }
        private void tileItem1_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage5;
        }

        private void tileItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage2;
        }

        private void Gestioncommande_Load(object sender, EventArgs e)
        {
            List<bon_livraison> bls = new List<bon_livraison>();
            bls = DBL.getAllBLnotfacured();
            Liste_bl_clt(bls);
            clients();
            articles();
        }
        private void getallistCmds(List<bon_commande> bcmds)
        {
            gridControl7.DataSource = null;
            gridView7.Columns.Clear();
            gridControl7.DataSource = bcmds;
            gridView7.Columns[0].Visible = false;
            gridView7.Columns[1].Caption = "Numéro B.C";
            gridView7.Columns[2].Caption = "Date";
            gridView7.Columns[3].Caption = "Etat";
            gridView7.Columns[4].Visible = false;
            gridView7.Columns[5].Caption = "Remise";
            gridView7.Columns[6].Caption = "Total";
            gridView7.Columns[7].Visible = false;
            gridView7.Columns[8].Caption = "Client";
            gridView7.Columns[9].Caption = "N° devis";
            gridView7.Columns[10].Caption = "Commentaire";



        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {

        }

        private void tileItem5_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage3;
        }

        private void tileItem4_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage4;
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            if(xtraTabControl1.SelectedTabPage==xtraTabPage1)
            {
             
                List<bon_livraison> bls = new List<bon_livraison>();
                bls = DBL.getAllBLbytype("validée");
                Liste_bl_clt(bls);
            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPage2)
            {
                Liste_cde_clt();
            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPage3)
            {

            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPage4)
            {

            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPage5)
            {

            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPage6)
            {
              
                List<bon_commande> cmds = new List<bon_commande>();
                cmds = bcser.getAllBbyetat("validé");
                getallistCmds(cmds);
            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPage7)
            {

                List<devis> deviss = new List<devis>();
                deviss = devisser.getAlldevisbytype("validé");
                Liste_devis(deviss);
            }
        }
        
        private void Liste_cde_clt()
        {

            gridControl2.DataSource = null;
            gridView2.Columns.Clear();
            gridControl2.DataSource = DBL.getAllFacture();
            this.gridView2.Columns[0].Visible = false;
            this.gridView2.Columns[1].Caption = "Numero facture";
            this.gridView2.Columns[2].Caption = "Date d'ajout";
            this.gridView2.Columns[3].Visible = false;
            this.gridView2.Columns[4].Caption = "Nom Client";
            this.gridView2.Columns[5].Caption = "Montant HT";
            this.gridView2.Columns[6].Caption = "Remise";
            this.gridView2.Columns[7].Caption = "Montant Total";
            this.gridView2.Columns[8].Visible = false;
            this.gridView2.Columns[9].Visible = false;
            this.gridView2.Columns[10].Visible = false;
            this.gridView2.Columns[11].Visible = false;
            this.gridView2.Columns[12].Caption = "Etat Facture";




        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            Liste_cde_clt();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            int count = gridView1.DataRowCount;
            DialogResult dialogResult = XtraMessageBox.Show("Sure de vouloir supprimer la facture ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    foreach (int i in gridView1.GetSelectedRows())
                    {
                        facturevente row = (facturevente)gridView1.GetRow(i);
                     
                        string b = "";
                        string a = "ajoute";
                     
                        List<piece_fact> lpf = new List<piece_fact>();
                        lpf = DBL.getLFByCodeFact(int.Parse(row.numero_fact.ToString()));
                        DBL.supprimerLF(lpf);
                        DBL.supprimerFact(row);
                    }
                    Liste_cde_clt();
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            List<bon_livraison> bls = new List<bon_livraison>();
            bls = DBL.getAllBLbytype("validée");
            Liste_bl_clt(bls);
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            List<int> LidBl = new List<int>();
            foreach (int i in gridView1.GetSelectedRows())
            {

                bon_livraison BL = (bon_livraison)gridView1.GetRow(i);
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

                FactureV factvent = new FactureV(LidBl);
                factvent.ShowDialog();

            }
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            upadetall();
        }
        private void Liste_bl_clt(List<bon_livraison> bls)
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = bls;
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[1].Caption = "Numéro B.L";
            this.gridView1.Columns[2].Caption = "Date Ajout";
            this.gridView1.Columns[3].Caption = "Etat";
            this.gridView1.Columns[4].Caption = "Montant";
            this.gridView1.Columns[5].Visible = false;
            this.gridView1.Columns[6].Caption = "Client";
            this.gridView1.Columns[7].Visible = false;
            this.gridView1.Columns[8].Visible = false;
            this.gridView1.Columns[9].Visible = false;
            this.gridView1.Columns[10].Visible = false;
       
            gridView1.OptionsView.ShowAutoFilterRow = true;
            //updatesum();
        }

        private void upadetall()
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                bon_livraison row = (bon_livraison)gridView1.GetRow(gridView1.FocusedRowHandle);
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
                        bls = DBL.getAllBLnotfacured();
                        Liste_bl_clt(bls);
                    }


                }
            }
        }

        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {

                int count = gridView1.DataRowCount;
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    bon_livraison row = (bon_livraison)gridView1.GetRow(gridView1.FocusedRowHandle);
                    id_bl = Convert.ToInt32(row.id);
                    Point pt = this.Location;
                    pt.Offset(this.Left + e.X, this.Top + e.Y);
                    popupMenu1.ShowPopup(this.barManager1, Control.MousePosition);
                }
            }
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
                int count = gridView1.DataRowCount;
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    bon_livraison bliv = (bon_livraison)gridView1.GetRow(gridView1.FocusedRowHandle);
                    memoEdit1.Text = bliv.comment;
                }

            }
            catch (Exception except)
            {

            }
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {

        }

        private void tileItem7_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage7;
            List<devis> deviss = new List<devis>();
            deviss = devisser.getAlldevisbytype("en cours");
            Liste_devis(deviss);
        }

        private void simpleButton16_Click(object sender, EventArgs e)
        {
            try
            {

                Double pv = Convert.ToDouble(tpu.Text.Replace('.',',')) * Convert.ToDouble(tquantit.Text.Replace('.', ','));
                double total = pv - pv * Convert.ToDouble(textEdit2.Text.Replace('.', ',')) / 100;
                Livre s = (Livre)lookUpEdit2.GetSelectedDataRow();
                piece_fact  lfact = new piece_fact();
          
                lfact.code_piece_u = s.codeart;
                lfact.libelle_piece_u = s.titre;
                lfact.quantite_piece_u = Convert.ToDouble(tquantit.Text.Replace('.', ','));
                lfact.puv = Convert.ToDouble(tpu.Text.Replace('.', ','));
                lfact.pv = total;
                lfact.id_fact = int.Parse(tnumcommandebase.Text);
                lfact.idauteur= int.Parse(textEdit1.Text);
                lfact.remise = Convert.ToDouble(textEdit2.Text);
               
                dartt.Add(lfact);
                getalldatatable();
                updatesum();
                lookUpEdit2.EditValue = null;
             
                tquantit.Text = null;
                tpu.Text = null;
                textEdit1.Text = null;
                textEdit2.Text= null;


            }
            catch (Exception except)
            {
                MessageBox.Show("vérifier les valeurs entrées");
            }
        }
        public void updatesum()
        {
            try
            {
                prixtotc = 0;
                prix_rem = 0;
                for (int i = 0; i < gridView5.RowCount; i++)
                {

                    piece_fact lbl = (piece_fact)gridView5.GetRow(i);
                    prixtotc += Convert.ToDouble(lbl.pv.ToString().Replace('.', ','));
                    textEdit8.Text = prixtotc.ToString();
                    prix_rem = prixtotc - prixtotc * (Convert.ToDouble(textEdit6.Text)) / 100;
                    textEdit9.Text = prix_rem.ToString();
                }
            }
            catch (Exception xce)
            {
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

        private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {
            Livre s = (Livre)lookUpEdit2.GetSelectedDataRow();
            if (s != null)
            {
               
                textEdit3.Text = s.codeart;
                tpu.Text = s.pvpublic.ToString();
                textEdit1.Text = s.auteur.ToString();
            }
            simpleButton16.Enabled = true;
            simpleButton17.Enabled = false;
            simpleButton15.Enabled = false;
        }

        private void simpleButton29_Click(object sender, EventArgs e)
        {
         
        }

        private void textEdit6_Enter(object sender, EventArgs e)
        {

        }

        private void simpleButton17_Click(object sender, EventArgs e)
        {
            try
            {
                Double pv = Convert.ToDouble(tpu.Text.Replace('.', ',')) * Convert.ToDouble(tquantit.Text.Replace('.', ','));
                double total = pv - pv * Convert.ToDouble(textEdit1.Text) / 100;
                Livre s = (Livre)lookUpEdit2.GetSelectedDataRow();
                client c = (client)lookUpEdit1.GetSelectedDataRow();

                piece_fact ldev = (piece_fact)gridView5.GetRow(gridView5.FocusedRowHandle);
                int a = dartt.IndexOf(ldev);
                dartt.RemoveAt(a);
                string codep = s.codeart;
                //string codeclt = c.numerocl.ToString(); 

                ldev = new piece_fact();
                ldev.code_piece_u = s.codeart;
                ldev.libelle_piece_u = s.titre;
                ldev.quantite_piece_u = Convert.ToDouble(tquantit.Text.Replace('.', ','));
                ldev.puv = Convert.ToDouble(tpu.Text.Replace('.', ','));
                ldev.remise = Convert.ToDouble(textEdit1.Text.Replace('.', ','));
                ldev.pv = total;
                ldev.id_fact = int.Parse(tnumcommandebase.Text);
                dartt.Insert(a, ldev);//.Add(lbl);

                getalldatatable();
                updatesum();
                lookUpEdit2.EditValue = null;
                //mdesign.Text = "";
                tpu.Text = "0";

                tquantit.Text = "0";
            }
            catch (Exception et) { }
        }

        private void simpleButton15_Click(object sender, EventArgs e)
        {
            try
            {
                int count = gridView5.DataRowCount;
                if (count != 0 && gridView5.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {

                    piece_fact ldev = (piece_fact)gridView5.GetRow(gridView5.FocusedRowHandle);
                    dartt.Remove(ldev);
                    MessageBox.Show("la commande a été mise à jour");
                    gridControl5.DataSource = null;
                    gridView5.Columns.Clear();
                    gridControl5.DataSource = dartt;
                    updatesum();
                    ClearAllForm(this);

                }

            }
            catch (Exception exc)
            { }
        }
        private void getalldatatable()
        {
            gridControl5.DataSource = null;
            gridView5.Columns.Clear();
            gridControl5.DataSource = dartt;
          
            gridView5.Columns[0].Visible = false;
            gridView5.Columns[1].Caption = "Code";
            gridView5.Columns[2].Caption = "Titre";
            gridView5.Columns[3].Caption = "Quantite";
            gridView5.Columns[4].Visible = false;
            gridView5.Columns[5].Caption = "puv";
            gridView5.Columns[6].Caption = "remise";
            gridView5.Columns[7].Caption = "Total";
            gridView5.Columns[8].Visible = false; 
            gridView5.Columns[9].Visible = false;
        }


        private void tileItem6_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage1;
            List<bon_livraison> bls = new List<bon_livraison>();
            bls = DBL.getAllBLbytype("validée");
            Liste_bl_clt(bls);
        }

        private void tileItem8_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage6;
            List<bon_commande> cmds = new List<bon_commande>();
            cmds = bcser.getAllBbyetat("en cours");
            getallistCmds(cmds);
        }

        private void labelControl11_Click(object sender, EventArgs e)
        {

        }

        private void textEdit9_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void textEdit6_EditValueChanged(object sender, EventArgs e)
        {
            updatesum();
        }

        private void simpleButton18_Click(object sender, EventArgs e)
        {

        }

        private void gridView5_Click(object sender, EventArgs e)
        {
         
        }

        private void gridControl5_Click(object sender, EventArgs e)
        {
            try
            {
                int count = gridView5.DataRowCount;
                if (count != 0 && gridView5.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {

                    piece_fact ldev = (piece_fact)gridView5.GetRow(gridView5.FocusedRowHandle);
                    lookUpEdit2.EditValue = ldev.code_piece_u;
                    Livre art = (Livre)lookUpEdit2.EditValue;
                    textEdit2.Text = art.isbn;
                    tquantit.Text = ldev.quantite_piece_u.ToString();
                    tpu.Text = ldev.puv.ToString();
                    textEdit2.Text = ldev.remise.ToString();
                    textEdit1.Text = ldev.idauteur.ToString();
                    simpleButton16.Enabled = false;
                    simpleButton17.Enabled = true;
                    simpleButton15.Enabled = true;

                }
            }
            catch (Exception except)
            { }
        }

        private void simpleButton19_Click(object sender, EventArgs e)
        {
            List<devis> Listdevis = new List<devis>();
            foreach (int i in gridView6.GetSelectedRows())
            {

                devis dev = (devis)gridView6.GetRow(i);
          
                    Listdevis.Add(dev);
           
            }
            if (Listdevis.Count != 0)
            {

                FactureV factvent = new FactureV(Listdevis);
                factvent.ShowDialog();

            }
        }

        private void simpleButton23_Click(object sender, EventArgs e)
        {
            List<bon_commande> bcmds = new List<bon_commande>();
            foreach (int i in gridView7.GetSelectedRows())
            {

                bon_commande bcmd = (bon_commande)gridView7.GetRow(i);

                bcmds.Add(bcmd);

            }
            if (bcmds.Count != 0)
            {

                FactureV factvent = new FactureV(bcmds);
                factvent.ShowDialog();

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
    }
}