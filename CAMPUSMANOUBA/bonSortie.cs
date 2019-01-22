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
    public partial class bonSortie : DevExpress.XtraEditors.XtraForm
    {
        BLService DBL = new BLService();
        ClientService DC = new ClientService();
        ArtService DS = new ArtService();
        List<ligne_bs> darttab;
        //sql_gmao fun = new sql_gmao();
        double prixtotc = 0;
        int numligne = 0;
        List<int> _LidBl;
        int Idfact = 0;
        public bonSortie(int idfact)
        {
            InitializeComponent();
            Idfact = idfact;

        }
        public bonSortie(List<int> LidBl)
        {
            InitializeComponent();
            Tnumfact.Text = DBL.NumBS().ToString();
            _LidBl = LidBl;
            dateEdit1.Text = DateTime.Now.ToString();
            darttab = new List<ligne_bs>();
            //darttab.Clear();
            //darttab.Columns.Add("code_art");
            //darttab.Columns.Add("libelle_piece");
            //darttab.Columns.Add("quantite_piece");
            //darttab.Columns.Add("id_clt");
            //darttab.Columns.Add("etat");
            //darttab.Columns.Add("puv");
            //darttab.Columns.Add("totvente");
            //darttab.Columns.Add("id_commande");
            //darttab.Columns.Add("remise");
            //darttab.Columns.Add("ttva");
            //darttab.Columns.Add("unit");
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
                lookUpEdit1.Text = DC.getClientByRaisonSoc(dt.client.ToString()).ToString();
                List<ligne_bl> dt_bl = new List<ligne_bl>();
                dt_bl = DBL.getLblByCodeBL(Convert.ToInt32(idbl.ToString()));

                foreach (ligne_bl row1 in dt_bl)
                {


                    Boolean trouve = false;
                    foreach (ligne_bs pfact in darttab)
                    {

                        if (pfact.code_art.ToString() == row1.code_art.ToString())
                        {
                            trouve = true;
                            pfact.quantite_article = pfact.quantite_article + row1.quantite_article;
                            pfact.total = pfact.total + row1.total;
                        }

                    }
                    if (!trouve)
                    {

                        //DataRow dr = darttab.NewRow();
                        ligne_bs dr = new ligne_bs();
                        dr.code_art = row1.code_art.ToString();
                        dr.designation_article = row1.designation_article.ToString();
                        dr.quantite_article = row1.quantite_article;
                        dr.puv = row1.puv;
                        dr.total = row1.total;
                        //dr.remise = row1.remise.ToString();

                        darttab.Add(dr);


                    }
                }
            }
            getalldatatable();
            updatesum();
        }


        private void clients()
        {
            //get All stock
            lookUpEdit1.Properties.DataSource = null;
            List<client> Allclients = new List<client>();
            Allclients = DC.listclts();

            lookUpEdit1.Properties.ValueMember = "code_clt";
            lookUpEdit1.Properties.DisplayMember = "raison_soc";
            lookUpEdit1.Properties.DataSource = Allclients;
            lookUpEdit1.Properties.PopulateColumns();
            lookUpEdit1.Properties.Columns["code_clt"].Caption = "Code client";
            lookUpEdit1.Properties.Columns["raison_soc"].Caption = "Raison sociale";
            lookUpEdit1.Properties.Columns["responsbale"].Caption = "Responsable";
            lookUpEdit1.Properties.Columns["gsm_clt"].Visible = false;
            lookUpEdit1.Properties.Columns["gsm_clt"].Visible = false;
            lookUpEdit1.Properties.Columns["tel_clt"].Visible = false;
            lookUpEdit1.Properties.Columns["fax_clt"].Visible = false;
            lookUpEdit1.Properties.Columns["adresse_clt"].Visible = false;
            lookUpEdit1.Properties.Columns["cp_clt"].Visible = false;
            lookUpEdit1.Properties.Columns["ville_clt"].Visible = false;
            lookUpEdit1.Properties.Columns["email_clt"].Visible = false;
            lookUpEdit1.Properties.Columns["site_clt"].Visible = false;
            lookUpEdit1.Properties.Columns["tva_clt"].Visible = false;
            lookUpEdit1.Properties.Columns["forme_juriduque"].Visible = false;
            lookUpEdit1.Properties.Columns["mode_pay"].Visible = false;

        }
        private void getalldatatable()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = darttab;

            //this.gridView1.Columns[0].Caption = "Code article";
            //this.gridView1.Columns[1].Caption = "Désignation";
            //this.gridView1.Columns[2].Caption = "Quantité";
            //this.gridView1.Columns[3].Visible = false;
            //this.gridView1.Columns[4].Visible = false;
            //this.gridView1.Columns[5].Caption = "PUV";
            //this.gridView1.Columns[6].Caption = "Prix ttc";
            //this.gridView1.Columns[7].Visible = false;
            //////////////this.gridView1.Columns[8].Caption = "remise";
            //this.gridView1.Columns[9].Caption = "TVA";
            //this.gridView1.Columns[10].Caption = "unité";
            //this.gridView1.Columns[11].Caption = "Qte restante";
            //gridView1.OptionsView.ShowAutoFilterRow = true;

        }
        public void updatesum()
        {
            prixtotc = 0;
            //double.TryParse(textBox5.Text.Replace('.', ','), out prixtotc);

            for (int i = 0; i < gridView1.RowCount; i++)
            {

                ligne_bs row1 = (ligne_bs)gridView1.GetRow(i);
                prixtotc += Convert.ToDouble(row1.total.ToString().Replace('.', ','));

            }


            textBox1.Text = prixtotc.ToString("0.000");

        }

        private void bonSortie_Load(object sender, EventArgs e)
        {
            clients();
            //articles();
        }

        private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {
            //DataRowView rowView = (DataRowView)lookUpEdit2.GetSelectedDataRow();
            //DataRow row = rowView.Row;
            //mdesign.Text = row[1].ToString();
            //tunit.Text = row[2].ToString();
            //tpu.Text = row[8].ToString();
            //textEdit2.Text = row[0].ToString();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {
            //int count = gridView1.DataRowCount;

            //if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            //{
            //    numligne = gridView1.FocusedRowHandle;
            //    piece_fact row = (piece_fact)gridView1.GetRow(gridView1.FocusedRowHandle);
            //    // idpiece = Convert.ToInt32(row[0].ToString());
            //    //  lookUpEdit2.EditValue = row[0];
            //    mdesign.Text = row[1].ToString();
            //    tquantit.Text = row[2].ToString();
            //    tpu.Text = row[5].ToString();
            //    //tunit.Text = row[10].ToString();
            //    //ttva.Text = row[9].ToString();
            //    tremise.Text = row[8].ToString();
            //    textEdit2.Text = row[0].ToString();
            //}
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            //    try
            //    {
            //        if (mdesign.Text == "")
            //        {
            //            mdesign.Text = "";
            //        }
            //        if (tpu.Text == "")
            //        {
            //            tpu.Text = "0";
            //        }
            //        if (ttva.Text == "")
            //        {
            //            ttva.Text = "0";
            //        }
            //        if (tquantit.Text == "")
            //        {
            //            tquantit.Text = "0";
            //        }
            //        if (tunit.Text == "")
            //        {
            //            tunit.Text = "";

            //        }
            //        if (tremise.Text == "")
            //        {
            //            tremise.Text = "0";

            //        }
            //        if (lookUpEdit1.EditValue == null)
            //        {
            //            lookUpEdit1.EditValue = "";

            //        }
            //        if (lookUpEdit2.EditValue == null)
            //        {
            //            lookUpEdit2.EditValue = "";

            //        }

            //        Double pvhorstva = Convert.ToDouble(tpu.Text.Replace('.', ',')) * Convert.ToDouble(tquantit.Text.Replace('.', ','));

            //        Double prixremis = pvhorstva - ((pvhorstva * Convert.ToDouble(tremise.Text.Replace('.', ','))) / 100);
            //        //MessageBox.Show("" + prixremis);
            //        Double pvtva = prixremis + ((prixremis * Convert.ToDouble(ttva.Text.Replace('.', ','))) / 100);
            //        //MessageBox.Show("" + pvtva);

            //        DataRowView rowView = (DataRowView)lookUpEdit2.GetSelectedDataRow();
            //        DataRow row = rowView.Row;
            //        DataRowView rowView1 = (DataRowView)lookUpEdit1.GetSelectedDataRow();
            //        DataRow row1 = rowView1.Row;
            //        string codeclt = row1[0].ToString();
            //        string codep = row[0].ToString();

            //        DataRow newpc = darttab.NewRow();
            //        newpc["code_art"] = textEdit2.Text;
            //        newpc["libelle_piece"] = mdesign.Text;
            //        newpc["quantite_piece"] = Convert.ToDouble(tquantit.Text.Replace('.', ','));
            //        newpc["id_clt"] = codeclt;
            //        newpc["etat"] = "validée";
            //        newpc["puv"] = tpu.Text;
            //        newpc["totvente"] = pvtva.ToString("0.000");
            //        newpc["id_commande"] = Tnumfact.Text;
            //        newpc["remise"] = tremise.Text;
            //        newpc["ttva"] = ttva.Text;
            //        newpc["unit"] = tunit.Text;

            //        darttab.Rows.Add(newpc);
            //        getalldatatable();
            //        updatesum();
            //    }
            //    catch (Exception exception)
            //    {
            //        MessageBox.Show("veuillez remplir les champs vides");
            //    }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    Double pvhorstva = Convert.ToDouble(tpu.Text.Replace('.', ',')) * Convert.ToDouble(tquantit.Text.Replace('.', ','));
            //    //Double prixremis = pvhorstva - ((pvhorstva * Convert.ToDouble(tremise.Text.Replace('.', ','))) / 100);
            //    //Double pvtva = prixremis + ((prixremis * Convert.ToDouble(ttva.Text.Replace('.', ','))) / 100);
            //    //DataRowView rowView = (DataRowView)lookUpEdit2.GetSelectedDataRow();
            //    // DataRow rowview = rowView.Row;
            //    DataRowView rowView1 = (DataRowView)lookUpEdit1.GetSelectedDataRow();
            //    DataRow row1 = rowView1.Row;
            //    // string codep = rowview[0].ToString();
            //    string codeclt = row1[0].ToString();
            //    DataRow row = darttab.NewRow();
            //    row[0] = textEdit2.Text;
            //    row[1] = mdesign.Text;
            //    row[2] = Convert.ToDouble(tquantit.Text.Replace('.', ','));
            //    row[3] = codeclt;
            //    row[4] = "validée";
            //    row[5] = tpu.Text;
            //    row[6] = pvhorstva.ToString("0.000");
            //    row[7] = Tnumfact.Text;
            //    row[8] = tremise.Text;
            //    row[9] = ttva.Text;
            //    row[10] = tunit.Text;

            //    darttab.Rows[numligne].Delete();
            //    darttab.Rows.Add(row);
            //    getalldatatable();
            //    updatesum();
            //    mdesign.Text = "";
            //    tpu.Text = "0";
            //    ttva.Text = "0";
            //    tquantit.Text = "0";
            //    tunit.Text = "";
            //    tremise.Text = "0";
            //    //lookUpEdit1.EditValue = "choisir un client";
            //    getalldatatable();
            //    updatesum();
            //}
            //catch (Exception er)
            //{
            //    MessageBox.Show("vérifier les données entrer");
            //}
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    int count = gridView1.DataRowCount;
            //    if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            //    {
            //        DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            //        darttab.Rows[numligne].Delete();

            //        getalldatatable();
            //        updatesum();

            //    }

            //}
            //catch (Exception exc)
            //{ }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            //try
            //{

            bon_sortie dt_fact_n = new bon_sortie();
            dt_fact_n = DBL.GetBSBynum(int.Parse(Tnumfact.Text));
            if (dt_fact_n != null)
            {
                XtraMessageBox.Show("Il existe une facture avec ce numéro");
            }
            else
            {
                if (dateEdit1.Text == "")
                {
                    MessageBox.Show("Entrer la date du facture");
                }

                else
                {


                    string dd = dateEdit1.Text.Substring(0, 10);//.ToString();
                    dd = dd.Substring(0, 10);
                    double prix_hr = 0, tt_hr = 0, prix_r = 0, tt_r = 0;
                    for (int i = 0; i < gridView1.RowCount; i++)
                    {
                        ligne_bs row1 = new ligne_bs();
                        row1 = (ligne_bs)gridView1.GetRow(i);
                        //prix_hr = Convert.ToDouble(row1.quantite_piece_u.ToString().Replace('.', ',')) * Convert.ToDouble(row1.puv.ToString().Replace('.', ','));
                        //prix_r = prix_hr - Convert.ToDouble(prix_hr * Convert.ToDouble(row1.remise) / 100);
                        prix_r = Convert.ToDouble(row1.total.ToString().Replace('.', ','));
                        //tt_hr += prix_hr;
                        tt_r += prix_r;



                        row1.id_bs = int.Parse(Tnumfact.Text);

                        row1.total = prix_r;



                        DBL.ajouterLBS(row1);


                    }
                    bon_sortie fac = new bon_sortie();
                    client row = (client)lookUpEdit1.GetSelectedDataRow();
                    if (row != null)
                    {
                        fac.id_clt = row.codeclient.ToString();
                        fac.client = row.raisonsoc.ToString();
                    }
                    fac.date_ajout = dateEdit1.DateTime;
                    fac.numero_bs = int.Parse(Tnumfact.Text);
                    fac.totalSansRemise = tt_hr;
                    fac.montant_BS = tt_r;
                    fac.L_num_bl = textEdit1.Text;
                    DBL.ajouterBS(fac);

                }
            }
            this.Close();
            ////////////////////////////////////////FactureReport report = new FactureReport(id_factt);
            ////////////////////////////////////////// report.ShowPreview();

            //}
            //catch (Exception ed)
            //{
            //    MessageBox.Show("Vérifier les données entrées");
            //}
        }
    }
}