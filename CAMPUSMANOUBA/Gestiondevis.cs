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
    public partial class Gestiondevis : DevExpress.XtraEditors.XtraForm
    {
        public Gestiondevis()
        {
            InitializeComponent();
            dartt = new List<ligne_devis>();
            dartt.Clear();
            getlastindex();
            simpleButton1.Enabled = true;
            simpleButton2.Enabled = false;
            simpleButton3.Enabled = false;
        }
        public static int id_bl;
        List<ligne_devis> dartt = new List<ligne_devis>();
        public static Double prixtotc, prix_rem;
        public static Double test;
        public static int numbl, id_clt = 0;
        public static int idpiece = 0, idrow = 0;
        ArtService artser = new ArtService();
        reservationservice reservser = new reservationservice();
        DevisService DBL = new DevisService();
        ClientService DC = new ClientService();
        BCService BC = new BCService();
        ArtService DS = new ArtService();
        private static BLNOCommandeBL  newbl;
        private static listebls listbl;
        private static Gestiondevis instance;
        public static Gestiondevis Instance()
        {
            if (instance == null)

                instance = new Gestiondevis();

            return instance;

        }
        private void getlastindex()
        {
            string date1 = System.DateTime.Today.ToShortDateString();
            string year = date1.Substring(6, 4);
            string lastindex = DBL.incrementerDevis().ToString();
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
                    tnumcommandebase.Text = DBL.incrementerDevis().ToString("00000");
                }
            }
        }
        private string  getlastindexcmd()
        {
            string numbc;
            string date1 = System.DateTime.Today.ToShortDateString();
            string year = date1.Substring(6, 4);
            string lastindex = BC.incrementerBc().ToString();
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
                    numbc = BC.incrementerBc().ToString("00000");
                }
                
            }
            return numbc;
        }

        private void Duplicata_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }
        private void Liste_bl_clt(List<devis> bls)
        {          
            gridControl2.DataSource = null;
            gridView2.Columns.Clear();
            gridControl2.DataSource = bls;
            this.gridView2.Columns[0].Visible = false;
            this.gridView2.Columns[1].Caption = "Numéro devis";
            this.gridView2.Columns[2].Caption = "Date Ajout";
            this.gridView2.Columns[3].Visible = false;
            this.gridView2.Columns[4].Caption = "Client";
            this.gridView2.Columns[5].Visible =  false;
            this.gridView2.Columns[6].Caption = "Remise";
            this.gridView2.Columns[7].Caption = "Montant";
            this.gridView2.Columns[8].Caption = "etat";
            this.gridView2.Columns[9].Caption = "Comment";

            gridView2.OptionsView.ShowAutoFilterRow = true;
            //updatesum();
        }
        private void Liste_bl_clt1(List<devis> bls)
        {
            gridControl3.DataSource = null;
            gridView3.Columns.Clear();
            gridControl3.DataSource = bls;
            this.gridView3.Columns[0].Visible = false;
            this.gridView3.Columns[1].Caption = "Numéro devis";
            this.gridView3.Columns[2].Caption = "Date Ajout";
            this.gridView3.Columns[3].Visible = false;
            this.gridView3.Columns[4].Caption = "Client";
            this.gridView3.Columns[5].Visible = false;
            this.gridView3.Columns[6].Caption = "Remise";
            this.gridView3.Columns[7].Caption = "Montant";
            this.gridView3.Columns[8].Caption = "Etat";
            this.gridView3.Columns[9].Caption = "Comment";

            gridView3.OptionsView.ShowAutoFilterRow = true;
            //updatesum();
        }
        private void Liste_bl_clt2(List<devis> bls)
        {
            gridControl4.DataSource = null;
            gridView4.Columns.Clear();
            gridControl4.DataSource = bls;
            this.gridView4.Columns[0].Visible = false;
            this.gridView4.Columns[1].Caption = "Numéro devis";
            this.gridView4.Columns[2].Caption = "Date Ajout";
            this.gridView4.Columns[3].Visible = false;
            this.gridView4.Columns[4].Caption = "Client";
            this.gridView4.Columns[5].Visible = false;
            this.gridView4.Columns[6].Caption = "Remise";
            this.gridView4.Columns[7].Caption = "Montant";
            this.gridView4.Columns[8].Caption = "Etat";
            this.gridView4.Columns[9].Caption = "Comment";
           
            gridView4.OptionsView.ShowAutoFilterRow = true;
            //updatesum();
        }
        private void Duplicata_Activated(object sender, EventArgs e)
        {
          
        }
        public void updatesum()
        {
            try
            {
                prixtotc = 0;
                prix_rem = 0;
                for (int i = 0; i < gridView1.RowCount; i++)
                {

                    ligne_devis lbl = (ligne_devis)gridView1.GetRow(i);
                    prixtotc += Convert.ToDouble(lbl.total.ToString().Replace('.', ','));
                    textEdit8.Text = prixtotc.ToString();
                    prix_rem = prixtotc - prixtotc * (Convert.ToDouble(textEdit6.Text)) / 100;
                    textEdit9.Text = prix_rem.ToString();

                }

            }
            catch (Exception exce)
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
            List<devis> bls = new List<devis>();
            bls = DBL.getAlldevisbytype("en cours");

            Liste_bl_clt(bls);
        }
        private void autocomplete(string text)
        {
            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            List<Livre> arts = new List<Livre>();
            arts = artser.getarticlestartby(text);
            foreach (Livre art in arts)
            {
                collection.Add(art.titre);
            }

            //textEdit2.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //textEdit2.MaskBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            //textEdit2.MaskBox.AutoCompleteCustomSource = collection;
        }
        private void Gestioncommande_Load(object sender, EventArgs e)
        {
            getlastindex();
            clients();
            articles();
        }
        private void tileItem3_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage3;
            List<devis> bls = new List<devis>();
            bls = DBL.getAlldevisbytype("validé");
            Liste_bl_clt1(bls);
        }
        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            if(xtraTabControl1.SelectedTabPage==xtraTabPage1)
            {
                getlastindex();
                clients();
                //articles();
            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPage2)
            {
                List<devis> bls = new List<devis>();
                bls = DBL.getAlldevisbytype("en cours");

                Liste_bl_clt(bls);
            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPage3)
            {
                List<devis> bls = new List<devis>();
                bls = DBL.getAlldevisbytype("validé");
                Liste_bl_clt1(bls);
            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPage4)
            {
                List<devis> bls = new List<devis>();
                bls = DBL.getAlldevisbytype("refusé");
                Liste_bl_clt2(bls);
            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPage5)
            {
                List<devis> bls = new List<devis>();
                bls = DBL.getAlldevisbytype("facturé");
                Liste_bl_clt2(bls);
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
            lookUpEdit2.Properties.DataSource = null;
            List<Livre> Allarticle = new List<Livre>();
            Allarticle = artser.getallart();

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
            List<devis> bls = new List<devis>();
            bls = DBL.getAlldevisbytype("refusé");
            Liste_bl_clt2(bls);
        }
        private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {


            Livre s = (Livre)lookUpEdit2.GetSelectedDataRow();
            if (s != null)
            {
                textEdit2.Text = s.isbn;
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
                Double pv = Convert.ToDouble(tpu.Text.Replace('.', ',')) * Convert.ToDouble(tquantit.Text.Replace('.', ','));
                double total = pv - pv * Convert.ToDouble(tremise.Text.Replace('.',','))/100;
                Livre s = (Livre)lookUpEdit2.GetSelectedDataRow();
                ligne_devis ldevis = new ligne_devis();               
                ldevis.code_art = s.codeart;
                ldevis.designation_article = s.titre;
                ldevis.quantite_article = Convert.ToDouble(tquantit.Text.Replace('.', ','));
                ldevis.puv = Convert.ToDouble(tpu.Text.Replace('.', ','));
                ldevis.idauteur = Convert.ToInt32(textEdit1.Text.Replace('.', ','));
                ldevis.id_devis = int.Parse(tnumcommandebase.Text);
                ldevis.totalsansremise = pv;
                ldevis.remise = Convert.ToDouble(tremise.Text);
                ldevis.total = total;
                dartt.Add(ldevis);
                getalldatatable();
                updatesum();
                lookUpEdit2.EditValue = null;            
                tremise.Text = null;
                tquantit.Text = null;
                tpu.Text = null;
                textEdit1.Text = null;
            }
            catch (Exception except)
            {
                MessageBox.Show("vérifier les valeurs entrées");
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
            gridView1.Columns[6].Caption = "remise";
            gridView1.Columns[7].Visible = false;
            gridView1.Columns[8].Caption = "Total";

        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {
                Double pv = Convert.ToDouble(tpu.Text.Replace('.', ',')) * Convert.ToDouble(tquantit.Text.Replace('.', ','));
                double total = pv - pv * Convert.ToDouble(tremise.Text) / 100;
                Livre s = (Livre)lookUpEdit2.GetSelectedDataRow();
                client c = (client)lookUpEdit1.GetSelectedDataRow();

                ligne_devis ldev = (ligne_devis)gridView1.GetRow(gridView1.FocusedRowHandle);
                int a = dartt.IndexOf(ldev);
                dartt.RemoveAt(a);
                string codep = s.codeart;

                ldev = new ligne_devis();
                ldev.code_art = s.codeart;
                ldev.designation_article = s.titre;
                ldev.quantite_article = Convert.ToDouble(tquantit.Text.Replace('.', ','));
                ldev.puv = Convert.ToDouble(tpu.Text.Replace('.', ','));
                ldev.remise= Convert.ToDouble(tremise.Text.Replace('.', ','));
                ldev.totalsansremise = pv;
                ldev.total = total;
                ldev.id_devis = int.Parse(tnumcommandebase.Text);      
                ldev.idauteur = int.Parse(textEdit1.Text.Replace('.',','));
                dartt.Insert(a,ldev);
                getalldatatable();
                updatesum();
                lookUpEdit2.EditValue = null;
              
                tremise.Text = null;
                tquantit.Text = null;
                tpu.Text = null;
            }
            catch (Exception et)
            { }
        }
        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
             
                int count = gridView1.DataRowCount;
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    ligne_devis ldev = (ligne_devis)gridView1.GetRow(gridView1.FocusedRowHandle);

                    lookUpEdit2.EditValue = ldev.code_art;
                  
                    tquantit.Text = ldev.quantite_article.ToString();
                    tpu.Text = ldev.puv.ToString();
                    tremise.Text = ldev.remise.ToString() ;
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
                    devis row = (devis)gridView2.GetRow(gridView2.FocusedRowHandle);
                    id_bl = Convert.ToInt32(row.id);
                    Point pt = this.Location;
                    pt.Offset(this.Left + e.X, this.Top + e.Y);
                    popupMenu1.ShowPopup(this.barManager1, Control.MousePosition);
                }
            }
        }
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Vous etes sure de valider le devis en cours?!!", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                int count = gridView2.DataRowCount;
                if (count != 0 && gridView2.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    devis row = (devis)gridView2.GetRow(gridView2.FocusedRowHandle);
                    upadetall(Convert.ToInt32(row.numero_devis));
                }

            }
            else if (dialogResult == DialogResult.No)
            {

            }
        } 
        private int checkvalid( devis dev)
        {
            int valid = 0;
            List<ligne_devis> ldevs = new List<ligne_devis>();
            ldevs = DBL.getLDevisByCodeDevis((int)dev.numero_devis);
            foreach(ligne_devis ldev in ldevs )
            {
                if (artser.checkavailab(ldev.code_art, Convert.ToDouble(ldev.quantite_article)) != 1)
                {
                    valid = 1;
                    MessageBox.Show("hello");
                }
            }
            return valid;

              
        }
        private void upadetall(int num)
        {
         
               devis bl = DBL.GetDevisBynum(num);
                if (bl != null)
               {
                if (bl.etat == "validé")
                {
                    XtraMessageBox.Show("Ce devis est déja validé");
                }
                else
                {                   
                    bl.etat = "validé";
                    DBL.modifier(bl);
                 
                    List<devis> bls = new List<devis>();
                    bls = DBL.getAllDevis();

                    Liste_bl_clt(bls);
                    convertdevistocmd(bl);

                }                   
            }
        }
        private void gridControl2_Click(object sender, EventArgs e)
        {
            try
            { int count = gridView2.DataRowCount;
                if (count != 0 && gridView2.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                devis bliv = (devis)gridView2.GetRow(gridView2.FocusedRowHandle);
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
                    devis bliv = (devis)gridView2.GetRow(gridView2.FocusedRowHandle);
                    int valid=checkvalid(bliv);
                    if(valid==0)
                    {
                        BLNOCommandeBL bl = new BLNOCommandeBL(Convert.ToInt32(bliv.numero_devis));
                        bl.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Un ou plusieurs article n'est pas disponible en stock");
                    }
               
                }

            }
            catch (Exception except)
            {

            }
        }
        private void simpleButton9_Click(object sender, EventArgs e)
        {
            List<devis> bls = new List<devis>();
            bls = DBL.getAllDevis();

            Liste_bl_clt(bls);
        }

        private void simpleButton6_Click_1(object sender, EventArgs e)
        {

            int count = gridView2.DataRowCount;
            if (count != 0 && gridView2.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                DialogResult dialogResult = MessageBox.Show("Voulez vous confirmer la commande en cours ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (dialogResult == DialogResult.Yes)
                {

                    devis row = (devis)gridView2.GetRow(gridView2.FocusedRowHandle);
                    upadetall(Convert.ToInt32(row.numero_devis));
                  

                }
                else if (dialogResult == DialogResult.No)
                {

                }
             
            }
        }

        private void tileItem5_ItemClick(object sender, TileItemEventArgs e)
        {
            this.Close();
        }

        private void tileItem6_ItemClick(object sender, TileItemEventArgs e)
        {
            this.Close();
        }

        private void textEdit6_EditValueChanged(object sender, EventArgs e)
        {
            updatesum();
        }

        public Boolean verifierQuantite()
        {
            Boolean result = true;
            string msg = "Quantité demandée supérieure à la quantite du produit :\n";
            for (int i = 0; i < gridView1.DataRowCount; i++)
            {

                ligne_devis row = (ligne_devis)gridView1.GetRow(i);
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

        private void tileItem5_ItemClick_1(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage5;
        }

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {
            //try
            //{
                //autocomplete(textEdit2.Text);
            //}
            //catch(Exception exc)
            //{ }
        }

        private void textEdit2_EditValueChanged_1(object sender, EventArgs e)
        {
        
        }

        private void lookUpEdit2_EditValueChanged_1(object sender, EventArgs e)
        {
            textEdit2.Text = lookUpEdit2.EditValue.ToString();
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            ClearAllForm(this);
        }

        DevisService DBC = new DevisService();
        private void simpleButton8_Click(object sender, EventArgs e)
        {


            int count = gridView2.DataRowCount;
            if (count != 0 && gridView2.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                devis row = (devis)gridView2.GetRow(gridView2.FocusedRowHandle);
                if (row.etat != "Annulé")
                {

                    DialogResult dialogResult = MessageBox.Show("Vous etes sure de supprimer la commande en cours?!!", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {

                        DBC.supprimerDevis(row);
                        List<ligne_devis> lignebcs = new List<ligne_devis>();
                        lignebcs = DBC.getLDevisByCodeDevis((int)row.numero_devis);
                        DBC.supprimerLDevis(lignebcs);


                    }
                    else if (dialogResult == DialogResult.No)
                    {

                    }

                }
                else
                {
                    DialogResult dialogResult = MessageBox.Show("Voulez vous annuler la commande ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {
                        DBC.supprimerDevis(row);
                        List<ligne_devis> lignebcs = new List<ligne_devis>();
                        lignebcs = DBC.getLDevisByCodeDevis((int)row.numero_devis);
                        DBC.supprimerLDevis(lignebcs);
                       // upadetallANNULER((int)row.numero_devis);

                    }
                    else if (dialogResult == DialogResult.No)
                    {

                    }
                }

            }

        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {

        }

        //public static void ClearAllForm(Control Ctrl)
        //{
        //    foreach (Control ctrl in Ctrl.Controls)
        //    {
        //        BaseEdit editor = ctrl as BaseEdit;
        //        if (editor != null)
        //            editor.EditValue = null;

        //        ClearAllForm(ctrl);//Recursive
        //    }

        //}

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                int count = gridView1.DataRowCount;
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {

                    ligne_devis ldev = (ligne_devis)gridView1.GetRow(gridView1.FocusedRowHandle);
                    dartt.Remove(ldev);
                    MessageBox.Show("la commande a été mise à jour");
                    gridControl1.DataSource = null;
                    gridView1.Columns.Clear();
                    gridControl1.DataSource = dartt;
                    updatesum();
                    ClearAllForm(this);
                    
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
                devis dt_blo = new devis();
                dt_blo = DBL.GetDevisBynum(numer_bl);
                if (dt_blo != null)
                {
                    XtraMessageBox.Show("Il existe un devis avec ce numéro");
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
                            MessageBox.Show("Entrer le numero de devis");
                        }
                        else
                        {
                            string etat = "en cours";

                            test = 0;
                          
                            prixtotc = 0;
                            double prix_rem = 0;

                            for (int i = 0; i < gridView1.DataRowCount; i++)
                            {
                                ligne_devis lBl = new ligne_devis();
                                lBl = (ligne_devis)gridView1.GetRow(i);
                                Double total = Convert.ToDouble(lBl.quantite_article.ToString().Replace('.', ',')) * Convert.ToDouble(lBl.puv.ToString().Replace('.', ','));                            
                                prixtotc += total;                             
                               
                                DBL.ajouterLDevis(lBl);
                            }
                            devis dev = new devis();
                            client dt = new client();
                            dt = (client)lookUpEdit1.GetSelectedDataRow();
                            dev.numero_devis = int.Parse(tnumcommandebase.Text);
                            dev.date_ajout = dateEdit1.DateTime;
                            dev.etat = etat;
                            dev.client = dt.raisonsoc;
                            dev.id_clt = dt.codeclient.ToString();
                            dev.montant_devis = Convert.ToDouble(textEdit9.Text);
                            dev.remise =Convert.ToDouble(textEdit6.Text);
                            dev.totalSansRemise= Convert.ToDouble(textEdit8.Text);
                            dev.comment = memoEdit1.Text;
                            DBL.ajouterDevis(dev);
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

        private void convertdevistocmd(devis d)
        {
            List<ligne_devis> ligneDevis = new List<ligne_devis>();
            List<ligne_bc> ligneBC = new List<ligne_bc>();
            bon_commande bcmd = new bon_commande();

            ligneDevis=   DBL.getLDevisByCodeDevis((int) d.numero_devis);
            string numbc ;
            numbc = getlastindexcmd();
            foreach (ligne_devis ldev in ligneDevis)
            {
                ligne_bc lBc = new ligne_bc();
                lBc.code_art = ldev.code_art;
                lBc.designation_article = ldev.designation_article;
                lBc.quantite_article = ldev.quantite_article;
                lBc.puv = ldev.puv;
                lBc.totvente = ldev.totalsansremise;
                lBc.id_bc =Convert.ToInt32( numbc);
                lBc.remise = ldev.remise;
                lBc.prixremis = ldev.total;
                lBc.idauteur = ldev.idauteur;
                lBc.qtitrest= ldev.quantite_article;
                lBc.qtitservi = 0;

                BC.ajouterLBc(lBc);
                reservation reservat = new reservation();
                reservat.ncmd =Convert.ToInt32( numbc);
                reservat.article = lBc.code_art;
                reservat.quantit = lBc.quantite_article;
                reservat.date = System.DateTime.Now;
                artser.reserver(lBc.code_art, Convert.ToDouble(lBc.quantite_article));
                reservser.addReservation(reservat);
            }
            bcmd.numero_bc =Convert.ToInt32( numbc);
            bcmd.date_ajout = System.DateTime.Today;
            bcmd.id_clt = d.id_clt;
            bcmd.client = d.client;
            bcmd.etat = "en cours";
            bcmd.remise = d.remise;
            bcmd.numerodevis = d.numero_devis;
            bcmd.prixremise = d.montant_devis;
            bcmd.montant = d.totalSansRemise;
            BC.ajouterBc(bcmd);
            MessageBox.Show("La commande est enregistrée sous le numéro "+numbc+", la quantité est réservé");
            //upadetall((int)d.numero_devis);
        }
    }
}