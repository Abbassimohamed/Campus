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
    public partial class Gestioncommande : DevExpress.XtraEditors.XtraForm
    {
        List<ligne_bc> dartt = new List<ligne_bc>();
        public static Double prixtotc, prix_ht, prix_rem;
        public static Double test;
        public static int numbl, id_clt = 0;
        public static int idpiece = 0, idrow = 0;
        ArtService artser = new ArtService();
        reservationservice reservser = new reservationservice();
        BCService DBC = new BCService();
        ClientService DC = new ClientService();
        ArtService DS = new ArtService();
   
        private static Nouveau_agent newclt;
        private static Consultclt listclt;
        private static Gestioncommande instance;
        public static Gestioncommande Instance()
        {
            if (instance == null)

                instance = new Gestioncommande();

            return instance;

        }
        public Gestioncommande()
        {
            InitializeComponent();
            dartt = new List<ligne_bc>();
            dartt.Clear();
            getlastindex();
            
        }
    
        private void getlastindex()
        {
            string date1 = System.DateTime.Today.ToShortDateString();
            string year = date1.Substring(6, 4);
            string lastindex = DBC.incrementerBc().ToString();
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
                    tnumcommandebase.Text = DBC.incrementerBc().ToString("00000");
                }
            }
        }
        private void Duplicata_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void Duplicata_Activated(object sender, EventArgs e)
        {
          
        }

        private void tileItem1_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage1;
            getlastindex();
            clients();
            articles();
            dartt = new List<ligne_bc>();
            dartt.Clear();
        }

        private void tileItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage2;
            List<bon_commande> bcmds = new List<bon_commande>();
            bcmds = DBC.getAllBbyetat("en cours");
            getallistCmds(bcmds);
        }

        private void Gestioncommande_Load(object sender, EventArgs e)
        {
            clients();
            articles();
            simpleButton1.Enabled = true;
            simpleButton2.Enabled = false;
            simpleButton3.Enabled = false;

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
        private void getalldatatable()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = dartt;
            gridView1.Columns[0].Visible =false;
            gridView1.Columns[1].Visible = false;
            gridView1.Columns[2].Caption = "Code";
            gridView1.Columns[3].Caption = "Designation";
            gridView1.Columns[4].Caption = "Quantite";
            gridView1.Columns[5].Caption = "Puv";
            gridView1.Columns[6].Caption = "Total sans remise";
            gridView1.Columns[7].Caption = "Remise";
            gridView1.Columns[8].Caption = "Total";



        }
        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == xtraTabPage1)
            {
                getlastindex();
                clients();
                articles();
                dartt = new List<ligne_bc>();
                dartt.Clear();


            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPage2)
            {
                List<bon_commande> bcmds = new List<bon_commande>();
                bcmds = DBC.getAllBbyetat("en cours");
                getallistCmds(bcmds);
            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPage3)
            {
                List<bon_commande> bcmds = new List<bon_commande>();
                bcmds = DBC.getAllBbyetat("validé");
                getallistCmdsvalid(bcmds);

            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPage4)
            {
                List<bon_commande> bcmds = new List<bon_commande>();
                bcmds = DBC.getAllBbyetat("Annulé");
                getallistCmdsannul(bcmds);
            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPage5)
            {
                List<bon_commande> bcmds = new List<bon_commande>();
                bcmds = DBC.getAllBbyetat("livré");
                getallistCmdslivred(bcmds);
            }
        }
        private void getallistCmds(List<bon_commande> bcmds)
        {
            gridControl2.DataSource = null;
            gridView2.Columns.Clear();
            gridControl2.DataSource = bcmds;
            gridView2.Columns[0].Visible = false;
            gridView2.Columns[1].Caption = "Numéro B.C";
            gridView2.Columns[2].Caption = "Date";
            gridView2.Columns[3].Caption = "Etat";
            gridView2.Columns[4].Visible = false;
            gridView2.Columns[5].Caption = "Remise";
            gridView2.Columns[6].Caption = "Total";
            gridView2.Columns[7].Visible = false;
            gridView2.Columns[8].Caption = "Client";        
            gridView2.Columns[9].Caption = "N° devis";
            gridView2.Columns[10].Caption = "Commentaire";



        }
        private void getallistCmdsvalid(List<bon_commande> bcmds)
        {
            gridControl3.DataSource = null;
            gridView3.Columns.Clear();
            gridControl3.DataSource = bcmds;
            gridView3.Columns[0].Visible = false;
            gridView3.Columns[1].Caption = "Numéro B.C";
            gridView3.Columns[2].Caption = "Date";
            gridView3.Columns[3].Caption = "Etat";
            gridView3.Columns[4].Visible = false;
            gridView3.Columns[5].Caption = "Remise";
            gridView3.Columns[6].Caption = "Total";
            gridView3.Columns[7].Visible = false;
            gridView3.Columns[8].Caption = "Client";
            gridView3.Columns[9].Caption = "N° devis";
            gridView3.Columns[10].Caption = "Commentaire";



        }
        private void getallistCmdslivred(List<bon_commande> bcmds)
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
        private void getallistCmdsannul(List<bon_commande> bcmds)
        {
            gridControl4.DataSource = null;
            gridView4.Columns.Clear();
            gridControl4.DataSource = bcmds;
            gridView4.Columns[0].Visible = false;
            gridView4.Columns[1].Caption = "Numéro B.C";
            gridView4.Columns[2].Caption = "Date";
            gridView4.Columns[3].Caption = "Etat";
            gridView4.Columns[4].Visible = false;
            gridView4.Columns[5].Caption = "Remise";
            gridView4.Columns[6].Caption = "Total";
            gridView4.Columns[7].Visible = false;
            gridView4.Columns[8].Caption = "Client";
            gridView4.Columns[9].Caption = "N° devis";
            gridView4.Columns[10].Caption = "Commentaire";



        }
        private void verifarticle(Livre art,double qte)
        {
            int find = 0;
            long count = 0;
            count = dartt.LongCount();
            if(count!=0)
            {
               foreach(ligne_bc lbc in dartt)
                {
                   
                    if(lbc.code_art==art.codeart)
                    {
                        DialogResult dialogResult = MessageBox.Show("L'article est déja ajoutée à la commande client!! 'Oui' pour additioner la quantité 'Non' pour annuler   ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (dialogResult == DialogResult.Yes)
                        {
                            lbc.quantite_article += qte;
                            Double pvhorstva = 0;
                            pvhorstva = Convert.ToDouble(tpu.Text.Replace('.', ',')) *  Convert.ToDouble( lbc.quantite_article);
                            Double prixremis = 0;
                            prixremis = pvhorstva - (pvhorstva * Convert.ToDouble(tremise.Text.Replace('.', ',')) / 100);
                            lbc.totvente = pvhorstva;
                            
                            lbc.remise = Convert.ToDouble(tremise.Text.Replace('.', ','));
                            lbc.prixremis = prixremis;
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
                           
                        }

                    }
                   
                }
               if(find!=1)
                {
                    Double pvhorstva = 0;

                    pvhorstva = Convert.ToDouble(tpu.Text.Replace('.', ',')) * Convert.ToDouble(tquantit.Text.Replace('.', ','));
                    Double prixremis = 0;
                    prixremis = pvhorstva - (pvhorstva * Convert.ToDouble(tremise.Text.Replace('.', ',')) / 100);
                    //Double pvtva = prixremis + ((prixremis * Convert.ToDouble(ttva.Text.Replace('.', ','))) / 100);

                    ligne_bc lBc = new ligne_bc();
                    lBc.code_art = art.codeart; ;
                    lBc.designation_article = mdesign.Text;
                    lBc.quantite_article = Convert.ToDouble(tquantit.Text.Replace('.', ','));
                    lBc.puv = Convert.ToDouble(tpu.Text.Replace('.', ','));
                    lBc.totvente = pvhorstva;
                    lBc.id_bc = int.Parse(tnumcommandebase.Text);
                    lBc.remise = Convert.ToDouble(tremise.Text.Replace('.', ','));
                    lBc.prixremis = prixremis;
                    lBc.idauteur = Convert.ToInt32(textEdit1.Text);
                    lBc.qtitrest= Convert.ToDouble(tquantit.Text.Replace('.', ','));
                    lBc.qtitservi = 0;
                    dartt.Add(lBc);
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

                ligne_bc lBc = new ligne_bc();
                lBc.code_art = art.codeart; ;
                lBc.designation_article = mdesign.Text;
                lBc.quantite_article = Convert.ToDouble(tquantit.Text.Replace('.', ','));
                lBc.puv = Convert.ToDouble(tpu.Text.Replace('.', ','));
                lBc.totvente = pvhorstva;
                lBc.id_bc = int.Parse(tnumcommandebase.Text);
                lBc.remise = Convert.ToDouble(tremise.Text.Replace('.', ','));
                lBc.prixremis = prixremis;
                lBc.idauteur = Convert.ToInt32(textEdit1.Text);
                lBc.qtitrest = Convert.ToDouble(tquantit.Text.Replace('.', ','));
                lBc.qtitservi = 0;
                dartt.Add(lBc);
                getalldatatable();

                updatesum();
                lookUpEdit2.EditValue = null;
                mdesign.Text = null;
                tquantit.Text = null;
                tremise.Text = null;
                tpu.Text = null;
                
            }


           
        }
        private void simpleButton1_Click(object sender, EventArgs e)
        {

            try
            {

                Livre s = (Livre)lookUpEdit2.GetSelectedDataRow();
                client c = (client)lookUpEdit1.GetSelectedDataRow();

                if (artser.checkavailab(s.codeart, Convert.ToDouble(tquantit.Text.Replace('.',','))) == 1)
                {
             
                    verifarticle(s, Convert.ToDouble(tquantit.Text));
                 
                }
                else
                {
                    List<reservation> reservs = new List<reservation>();
                    reservs = reservser.getallreservbyarticle(s.codeart);
                    if(reservs.LongCount()==0)
                    {
                        MessageBox.Show("La quantité demandé n'est pas disponible en stock,la quantité en stock de l'article" + s.titre + "est de " + s.quantitenstock + " .Veuillez ne pas dépasser cette quantité ");
                        
                    }
                    else
                    {
                       
                        foreach(reservation rsv in reservs)
                        {
                            bon_commande bcmd = new bon_commande();
                            bcmd = DBC.GetAllBcBynum((Int32)rsv.ncmd);
                            if(bcmd.etat=="en cours")
                            {
                                
                                MessageBox.Show("L'article "+ s.titre +" est réservée par la commande "+ bcmd.numero_bc +"qui n'est pas encore validé.\n veuillez valider ou annuler les commandes en cours");

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

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {

                Livre s = (Livre)lookUpEdit2.GetSelectedDataRow();
                client c = (client)lookUpEdit1.GetSelectedDataRow();
                if (artser.checkavailab(s.codeart, Convert.ToDouble(tquantit.Text.Replace('.', ','))) == 1)
                {
                    ligne_bc lBc = (ligne_bc)gridView1.GetRow(gridView1.FocusedRowHandle);
                    int pos = dartt.IndexOf(lBc);
                    dartt.RemoveAt(pos);
                    Double pvhorstva = 0;

                    pvhorstva = Convert.ToDouble(tpu.Text.Replace('.', ',')) * Convert.ToDouble(tquantit.Text.Replace('.', ','));
                    Double prixremis = 0;
                    prixremis = pvhorstva - (pvhorstva * Convert.ToDouble(tremise.Text.Replace('.', ',')) / 100);
                    //Double pvtva = prixremis + ((prixremis * Convert.ToDouble(ttva.Text.Replace('.', ','))) / 100);
                    Double qtemarge = 0;
                    qtemarge = Convert.ToDouble(tquantit.Text.Replace('.', ',')) - Convert.ToDouble( lBc.quantite_article);
                    lBc.code_art = s.codeart; ;
                    lBc.designation_article = mdesign.Text;
                    lBc.quantite_article = Convert.ToDouble(tquantit.Text.Replace('.', ','));
                    lBc.puv = Convert.ToDouble(tpu.Text.Replace('.', ','));
                    lBc.totvente = pvhorstva;
                    lBc.id_bc = int.Parse(tnumcommandebase.Text);
                    lBc.remise = Convert.ToDouble(tremise.Text.Replace('.', ','));
                    lBc.prixremis = prixremis;
                    lBc.idauteur = Convert.ToInt32(textEdit1.Text);
                    lBc.qtitrest= lBc.qtitrest+qtemarge;
                    
                    dartt.Add(lBc);
                    getalldatatable();

                    updatesum();
                    lookUpEdit2.EditValue = null;
                    mdesign.Text = null;
                    tquantit.Text = null;
                    tremise.Text = null;
                    tpu.Text = null;
                }
                else
                {
                    MessageBox.Show("La quantité demandé n'est pas disponible en stock,la quantité en stock de l'article" + s.titre + "est de " + s.quantitenstock + " .Veuillez ne pas dépasser cette quantité ");
                }

            }
            catch (Exception except)
            {
                MessageBox.Show("vérifier les valeurs entrées");
            }
         
         
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Vous etes sur de vouloir annuler la commande en cours!!? ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                ClearAllForm(this);
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
                dartt.Clear();
                getlastindex();

            }
            else if (dialogResult == DialogResult.No)
            {
               
            }
           
        }

        private void textEdit6_EditValueChanged(object sender, EventArgs e)
        {
            updatesum();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
                int count = gridView1.DataRowCount;
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {

                    ligne_bc lbl = (ligne_bc)gridView1.GetRow(gridView1.FocusedRowHandle);


                    lookUpEdit2.EditValue = lbl.code_art;
                    mdesign.Text = lbl.designation_article;
                    tquantit.Text = lbl.quantite_article.ToString();
                    tpu.Text = lbl.puv.ToString();
                    textEdit1.Text = lbl.idauteur.ToString();
                    tremise.Text = lbl.remise.ToString();
                    simpleButton1.Enabled = false;
                    simpleButton2.Enabled = true;
                    simpleButton3.Enabled = true;
                }
            }
            catch (Exception except)
            { }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

            bon_commande bcmd = DBC.GetAllBcBynum(num);

            if (bcmd != null)
            {

                if (bcmd.etat == "validé")
                {
                    XtraMessageBox.Show("Cette bon de commande est déja validé");
                }
                else if(bcmd.etat == "en cours")
                {

                    bcmd.etat = "validé";
                    DBC.modifierBC(bcmd);

                    List<bon_commande> bcmds = new List<bon_commande>();
                    bcmds = DBC.getAllBbyetat("en cours");
                    getallistCmds(bcmds);

                }
                else if(bcmd.etat == "Annulé")
                {
                    MessageBox.Show("Vous ne pouvez pas validé une commande annulé");

                }


            }
        }
        private void upadetallANNULER(int num)
        {
           
            bon_commande bcmd = DBC.GetAllBcBynum(num);
            List<reservation> reservations = new List<reservation>();
            reservations = reservser.getallreservationbycmd((int)bcmd.numero_bc);
            if (bcmd != null)
            {

                if (bcmd.etat == "Annulé"|| bcmd.etat== "validé")
                {
                    XtraMessageBox.Show("Bon de commande est déja Annulé");
                }
                else
                {

                    bcmd.etat = "Annulé";
                    DBC.modifierBC(bcmd);

                    foreach (reservation reserv in reservations)
                    {
                        artser.annulerreserver(reserv.article, Convert.ToDouble(reserv.quantit));

                    }

                    MessageBox.Show("La commande a été annulé.La quantité est disponible en stock");
                    List<bon_commande> bcmds = new List<bon_commande>();
                    bcmds = DBC.getAllBbyetat("en cours");
                    getallistCmds(bcmds);

                }


            }
        }
        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
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

        private void gridControl2_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {

                int count = gridView2.DataRowCount;
                if (count != 0 && gridView2.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                   
                    Point pt = this.Location;
                    pt.Offset(this.Left + e.X, this.Top + e.Y);
                    popupMenu1.ShowPopup(this.barManager1, Control.MousePosition);
                }
            }
        }

        private void simpleButton10_Click(object sender, EventArgs e)
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

        private void simpleButton9_Click(object sender, EventArgs e)
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

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            int count = gridView2.DataRowCount;
            if (count != 0 && gridView2.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                bon_commande row = (bon_commande)gridView2.GetRow(gridView2.FocusedRowHandle);
                if(row.etat=="Annulé")
                {

                    DialogResult dialogResult = MessageBox.Show("Vous etes sure de supprimer la commande en cours?!!", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.Yes)
                    {

                        DBC.supprimerBc(row);
                        List<ligne_bc> lignebcs = new List<ligne_bc>();
                        lignebcs = DBC.getLbcByCodeBC((int)row.numero_bc);
                        DBC.supprimerLbc(lignebcs);
                    

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
                        DBC.supprimerBc(row);
                        List<ligne_bc> lignebcs = new List<ligne_bc>();
                        lignebcs = DBC.getLbcByCodeBC((int)row.numero_bc);
                        DBC.supprimerLbc(lignebcs);
                        upadetallANNULER((int)row.numero_bc);

                    }
                    else if (dialogResult == DialogResult.No)
                    {

                    }
                }
                
            }
        }

        private void gridControl2_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int count = gridView2.DataRowCount;
                if (count != 0 && gridView2.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    bon_commande bcmd = (bon_commande)gridView2.GetRow(gridView2.FocusedRowHandle);
                    UpdateCmd updatecmd = new UpdateCmd(bcmd);
                    updatecmd.ShowDialog();
                }
            }
            catch (Exception exc)
            { }

        }

        private void tileItem3_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage3;
            List<bon_commande> bcmds = new List<bon_commande>();
            bcmds = DBC.getAllBbyetat("validé");
            getallistCmdsvalid(bcmds);
        }

        private void tileItem4_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage4;
            List<bon_commande> bcmds = new List<bon_commande>();
            bcmds = DBC.getAllBbyetat("Annulé");
            getallistCmdsannul(bcmds);
        }

        private void tileItem5_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage5;
            List<bon_commande> bcmds = new List<bon_commande>();
            bcmds = DBC.getAllBbyetat("livré");
            getallistCmdslivred(bcmds);
        }

        private void gridControl3_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int count = gridView3.DataRowCount;
                if (count != 0 && gridView3.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    bon_commande bcmd = (bon_commande)gridView3.GetRow(gridView3.FocusedRowHandle);
                    UpdateCmd updatecmd = new UpdateCmd(bcmd);
                    updatecmd.ShowDialog();
                }
            }
            catch (Exception exc)
            { }
        }

        private void gridControl5_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                int count = gridView5.DataRowCount;
                if (count != 0 && gridView5.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    bon_commande bcmd = (bon_commande)gridView5.GetRow(gridView5.FocusedRowHandle);
                    UpdateCmd updatecmd = new UpdateCmd(bcmd);
                    updatecmd.ShowDialog();
                }
            }
            catch (Exception exc)
            { }
        }

        private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {
            Livre s = (Livre)lookUpEdit2.GetSelectedDataRow();
            if (s != null)
            {
                mdesign.Text = s.titre;
                tpu.Text = s.pvpublic.ToString();
                textEdit1.Text = s.auteur.ToString();
                simpleButton1.Enabled = true;
                simpleButton2.Enabled = false;
                simpleButton3.Enabled = false;

            }
        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                int count = gridView1.DataRowCount;
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {

                    ligne_bc lBc = (ligne_bc)gridView1.GetRow(gridView1.FocusedRowHandle);
                    dartt.Remove(lBc);
                    MessageBox.Show("Ligne commande supprimée, cliquez sur valider");
                    gridControl1.DataSource = null;
                    gridView1.Columns.Clear();
                    gridControl1.DataSource = dartt;
                    updatesum();
                    lookUpEdit2.EditValue = null;
                    mdesign.Text = "";
                    tquantit.Text =null;
                    tpu.Text = null;
                    tremise.Text = null;
                    textEdit1.Text = null;
                }
            }
            catch (Exception exc)
            { }
        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Valider la commande en cours ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {

                client rowView1 = (client)lookUpEdit1.GetSelectedDataRow();
                int numer_bc = int.Parse(tnumcommandebase.Text);
                bon_commande dt_blo = new bon_commande();
                dt_blo = DBC.GetAllBcBynum(numer_bc);
                if (dt_blo != null)
                {
                    XtraMessageBox.Show("Il existe un bon commande avec ce numéro");
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
                            MessageBox.Show("Entrer le numéro de BC");
                        }
                        else
                        {
                            string etat = "en cours";

                            test = 0;

                            prixtotc = 0;


                            for (int i = 0; i < gridView1.DataRowCount; i++)
                            {

                                ligne_bc lBc = new ligne_bc();
                                lBc = (ligne_bc)gridView1.GetRow(i);
                                Double pnetvente = Convert.ToDouble(lBc.quantite_article.ToString().Replace('.', ',')) * Convert.ToDouble(lBc.puv.ToString().Replace('.', ','));

                                prixtotc += (double)lBc.prixremis;


                                DBC.ajouterLBc(lBc);
                            
                                reservation reservat = new reservation();
                                reservat.ncmd = int.Parse(tnumcommandebase.Text);
                                reservat.article = lBc.code_art;
                                reservat.quantit = lBc.quantite_article;
                                reservat.date = System.DateTime.Now;
                                artser.reserver(lBc.code_art, Convert.ToDouble(lBc.quantite_article));
                                reservser.addReservation(reservat);
                            


                            }
                            bon_commande Bc = new bon_commande();
                            client dt = new client();
                            dt = (client)lookUpEdit1.GetSelectedDataRow();

                            Bc.numero_bc = int.Parse(tnumcommandebase.Text);
                            Bc.date_ajout = dateEdit1.DateTime;
                            Bc.etat = etat;
                            Bc.client = dt.raisonsoc;
                            Bc.id_clt = dt.codeclient.ToString();
                            Bc.montant = prixtotc;
                            Bc.remise = Convert.ToDouble(textEdit6.Text.Replace('.', ','));
                            Bc.prixremise = Convert.ToDouble(textEdit9.Text.Replace('.', ','));
                            Bc.comment = memoEdit2.Text;
                            DBC.ajouterBc(Bc);
                          
                            MessageBox.Show("Ajouté avec succés la quantité en commande est réservée");
                            ClearAllForm(this);
                            gridControl1.DataSource = null;
                            gridView1.Columns.Clear();
                            dartt.Clear();
                            getlastindex();

                        }
                    }

                }
            }
            else if (dialogResult == DialogResult.No)
            {

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

                    ligne_bc lBc = (ligne_bc)gridView1.GetRow(i);
                    prixtotc += Convert.ToDouble(lBc.prixremis.ToString().Replace('.', ','));
                    textEdit8.Text = prixtotc.ToString();
                    prix_rem = prixtotc - prixtotc * (Convert.ToDouble(textEdit6.Text)) / 100;
                    textEdit9.Text = prix_rem.ToString();
                }

               


            }
            catch (Exception xce)
            {
            }
        }

    }
}