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
    public partial class modifierBL : DevExpress.XtraEditors.XtraForm
    {
        List<ligne_bl> darttab = new List<ligne_bl>();
        public static int idpiece = 0;
        public static int numligne = 0;
        BLService BLiv = new BLService();
        ArtService art = new ArtService();
        ClientService cli = new ClientService();
        public static Double prixtotc = 0;
        //public static DataTable darttab;
        public static string codepiece = "";
        public static Double prixtot = 0;
        public modifierBL(bon_livraison BL)
        {
            InitializeComponent();

            //lookUpEdit2.Text = "";
            prixtotc = 0;
            tnumcommandebase.Text = BL.numero_bl.ToString();
            BL = BLiv.GetBLBynum(int.Parse(tnumcommandebase.Text));
            lookUpEdit1.EditValue = BL.client;
            //dateEdit1.DateTime = (DateTime)BL.date_ajout;
            //tnBLmd.Text = BL.id.ToString();
            darttab = BLiv.getLblByCodeBL((int)BL.numero_bl);
            gridControl1.DataSource = darttab;
            //textEdit1.Text = BL.mode_livraison;
            //textEdit2.Text = BL.moyen_livraison;
            //textEdit3.Text = BL.lieu_livraison;
            textBox5.Text = BL.montant_BL.ToString();
            //articles();




        }


        private void simpleButton1_Click(object sender, EventArgs e)
        {

            {
                //try
                //{
                if (mdesign.Text == "")
                {
                    mdesign.Text = "";
                }
                if (tpu.Text == "")
                {
                    tpu.Text = "0";
                }
                if (tremise.Text == "")
                {
                    tremise.Text = "0";
                }
                if (tquantit.Text == "")
                {
                    tquantit.Text = "0";
                }


                if (lookUpEdit1.EditValue == null)
                {
                    lookUpEdit1.EditValue = "";

                }
                if (lookUpEdit2.EditValue == null)
                {
                    lookUpEdit2.EditValue = "";

                }
                Double pv = Convert.ToDouble(tpu.Text.Replace('.', ',')) * Convert.ToDouble(tquantit.Text.Replace('.', ','));
                Double prixremis = pv - ((pv * Convert.ToDouble(tremise.Text.Replace('.', ','))) / 100);

                Livre s = (Livre)lookUpEdit2.GetSelectedDataRow();
                client c = (client)lookUpEdit1.GetSelectedDataRow();


                ligne_bl lbl = new ligne_bl();
                lbl.code_art = s.codeart; ;
                lbl.designation_article = mdesign.Text;
                lbl.quantite_article = Convert.ToDouble(tquantit.Text.Replace('.', ',')); 
                lbl.puv = Convert.ToDouble(tpu.Text.Replace('.', ','));
                //lbl.remise = Convert.ToDouble(tremise.Text.Replace('.', ','));
                //lbl.prixRemise = Convert.ToDouble(tpu.Text.Replace('.', ',')) - Convert.ToDouble(tpu.Text.Replace('.', ',')) * Convert.ToDouble(tremise.Text.Replace('.', ',')) / 100;
                //lbl.total=pv.ToString();
                lbl.total = prixremis;
                lbl.id_bl = int.Parse(tnumcommandebase.Text);

                darttab.Add(lbl);
                getalldatatable();
                updatesum();
                lookUpEdit2.EditValue = null;
                mdesign.Text = "";
                tpu.Text = "0";
                tremise.Text = "0";
                tquantit.Text = "0";

            }
        }






        private void getalldatatable()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = darttab;



        }


        private void simpleButton4_Click(object sender, EventArgs e)
        {
            bon_livraison bliv = new bon_livraison();
            bliv = BLiv.GetBLBynum(int.Parse(tnumcommandebase.Text));
            if (bliv.etat == "validée")
            {
                MessageBox.Show("BL validée, vous ne pouvez pas effectuer des changements");
                this.Close();
            }

            else if (bliv.etat == "facturée")
            {
                MessageBox.Show("BL facturée, vous ne pouvez pas effectuer des changements");
                this.Close();
            }
            else if (bliv.etat == "en cours")
            {
                if (verifierQuantite())
                {
                    client cli = (client)lookUpEdit1.GetSelectedDataRow();
                    int numer_BL = int.Parse(tnumcommandebase.Text);
                    bon_livraison dt_blo = new bon_livraison();

                    string timbre = 0.500.ToString();

                    if (tnumcommandebase.Text == "")
                    {
                        MessageBox.Show("Entrer le numero de BL");
                    }
                    else
                    {
                        
                        List<ligne_bl> lB = new List<ligne_bl>();
                        bon_livraison bl = BLiv.GetBLBynum(int.Parse(tnumcommandebase.Text));
                        lB = BLiv.getLblByCodeBL((int)bl.numero_bl);
                        BLiv.supprimerLbl(lB);
                        prixtotc = 0;
                        double prix_rem = 0;

                        for (int i = 0; i < gridView1.RowCount; i++)
                        {
                            ligne_bl lBL = new ligne_bl();
                            lBL = (ligne_bl)gridView1.GetRow(i);
                            Double total = Convert.ToDouble(lBL.quantite_article.ToString().Replace('.', ',')) * Convert.ToDouble(lBL.puv.ToString().Replace('.', ','));
                            prixtotc += total;
                            lBL.id_bl = int.Parse(tnumcommandebase.Text);
                            BLiv.ajouterLBL(lBL);


                        }


                        bon_livraison BL = BLiv.GetBLBynum(int.Parse(tnumcommandebase.Text));

                        if (cli != null)
                        {
                            BL.client = cli.raisonsoc;
                            BL.id_clt = cli.codeclient.ToString();
                        }
                        BL.numero_bl = int.Parse(tnumcommandebase.Text);
                   
                        BL.montant_BL = prixtotc;
                      
                        BL.date_ajout = dateEdit1.DateTime.ToString();
                        BLiv.modifier(BL);

                        gridControl1.DataSource = null;
                        gridView1.Columns.Clear();
                        darttab.Clear();
                        BLNOCommandeBL BLom = new BLNOCommandeBL();

                        MessageBox.Show("la Livraison est modifiée avec succées veuillez Actualiser la liste des commandes clients");
                        this.Close();
                    }

                }
            }


            //listecmds lb = new listecmds();
            //lb.Show();as
        }




        private void clients()
        {

            lookUpEdit1.Properties.DataSource = null;
            List<client> Allclients = new List<client>();
            Allclients = cli.listclts();

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
            Allarticle = art.getallart();

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

            clients();
            articles();
        }

        private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {
            Livre s = (Livre)lookUpEdit2.GetSelectedDataRow();
            if (s != null)
            {
                mdesign.Text = s.titre;

                tpu.Text = s.pvpublic.ToString();
            }
        }


        private void gridControl1_Click(object sender, EventArgs e)
        {
            try
            {
                int count = gridView1.DataRowCount;
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {

                    ligne_bl lBL = (ligne_bl)gridView1.GetRow(gridView1.FocusedRowHandle);


                    lookUpEdit2.EditValue = lBL.code_art;
                    mdesign.Text = lBL.designation_article;
                    tquantit.Text = lBL.quantite_article.ToString();
                    tpu.Text = lBL.puv.ToString();
                    //tremise.Text = lBL.remise.ToString();
                }
            }
            catch (Exception except)
            { }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            {
                try
                {
                    Double pv = Convert.ToDouble(tpu.Text.Replace('.', ',')) * Convert.ToDouble(tquantit.Text.Replace('.', ','));
                    Double prixremis = pv - ((pv * Convert.ToDouble(tremise.Text.Replace('.', ','))) / 100);



                    Livre s = (Livre)lookUpEdit2.GetSelectedDataRow();

                    //client c = (client)lookUpEdit1.GetSelectedDataRow();
                    ligne_bl lBL = (ligne_bl)gridView1.GetRow(gridView1.FocusedRowHandle);
                    int pos = darttab.IndexOf(lBL);
                    darttab.RemoveAt(pos);

                    lBL = new ligne_bl();
                    lBL.code_art = s.codeart;
                    lBL.designation_article = mdesign.Text;
                    lBL.quantite_article = Convert.ToDouble(tquantit.Text.Replace('.', ','));
                    lBL.puv = Convert.ToDouble(tpu.Text.Replace('.', ',')); 
                    //lBL.remise = Convert.ToDouble(tremise.Text.Replace('.', ',')); 
                    //lBL.prixRemise = Convert.ToDouble(tpu.Text.Replace('.', ',')) - Convert.ToDouble(tpu.Text.Replace('.', ',')) * Convert.ToDouble(tremise.Text.Replace('.', ',')) / 100;

                    lBL.total = prixremis;
                    lBL.id_bl = int.Parse(tnumcommandebase.Text);
                    darttab.Insert(pos, lBL);
                    gridControl1.DataSource = null;
                    gridControl1.DataSource = darttab;
                    //gridControl1.Refresh();
                    gridView1.OptionsView.ShowAutoFilterRow = true;

                    updatesum();
                    lookUpEdit2.EditValue = null;
                    mdesign.Text = "";
                    tpu.Text = "0";
                    tremise.Text = "0";
                    tquantit.Text = "0";

                }
                catch (Exception et) { }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {
                int count = gridView1.DataRowCount;
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    ligne_bl row = (ligne_bl)gridView1.GetRow(gridView1.FocusedRowHandle);
                    darttab.Remove(row);
                    gridControl1.DataSource = null;
                    gridControl1.DataSource = darttab;
                    MessageBox.Show("la livraison a été mise à jour");

                    updatesum();
                    lookUpEdit2.EditValue = null;
                    mdesign.Text = "";
                    tpu.Text = "0";
                    tremise.Text = "0";
                    tquantit.Text = "0";


                }

            }
            catch (Exception exc)
            { }
        }

        public void updatesum()
        {
            try
            {
                prixtotc = 0;

                for (int i = 0; i < gridView1.RowCount; i++)
                {

                    ligne_bl lbl = (ligne_bl)gridView1.GetRow(i);
                    prixtotc += Convert.ToDouble(lbl.total.ToString().Replace('.', ','));

                    //prix_rem += Convert.ToDouble(lbl.prix_sans_remise.ToString().Replace('.', ','));
                    //prix_ht += Convert.ToDouble(row1[2].ToString().Replace('.', ',')) * Convert.ToDouble(row1[5].ToString().Replace('.', ','));
                }

                textBox5.Text = prixtotc.ToString();


            }
            catch (Exception xce)
            {
            }
        }

        private void modifierCommande_Load(object sender, EventArgs e)
        {


            clients();
            articles();


            try
            {
                getalldatatable();
            }
            catch (Exception exception)
            { }

        }




        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            client s = (client)lookUpEdit1.GetSelectedDataRow();

        }

        public Boolean verifierQuantite()
        {
            Boolean result = true;
            string msg = "Quantité demandée supérieure à la quantite du produit :\n";
            for (int i = 0; i < gridView1.DataRowCount; i++)
            {

                ligne_bl row = (ligne_bl)gridView1.GetRow(i);
                Livre s = BLiv.GetProdByQtRest(row.code_art.ToString());
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

    }
}