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
    public partial class FactureVente : DevExpress.XtraEditors.XtraForm
    {
        BLService DBL = new BLService();
        ClientService DC = new ClientService();
        ArtService DS = new ArtService();
        List<piece_fact> darttab;
        //sql_gmao fun = new sql_gmao();
        double prixtotc = 0;
        int numligne = 0;
        List<int> _LidBl;
        int Idfact = 0;
        public static int codeclt;
        public FactureVente(int idfact)
        {
            InitializeComponent();
            Idfact = idfact;

        }
        public FactureVente(List<int> LidBl)
        {
            InitializeComponent();
            Tnumfact.Text = DBL.NumFact().ToString();
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

                        //DataRow dr = darttab.NewRow();
                        piece_fact dr = new piece_fact();
                        dr.code_piece_u = row1.code_art.ToString();
                        dr.libelle_piece_u = row1.designation_article.ToString();
                        dr.quantite_piece_u = row1.quantite_article;
                        dr.puv = row1.puv;
                        dr.pv = row1.total;
                        dr.idauteur = row1.idauteur;
                        //dr.remise = row1.remise;
                      
                        darttab.Add(dr);


                    }
                }
            }
            lookUpEdit1.EditValue =codeclt;
            getalldatatable();
            updatesum();
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

            this.gridView1.Columns[0].Caption = "Code article";
            this.gridView1.Columns[1].Caption = "Désignation";
            this.gridView1.Columns[2].Caption = "Quantité";
            this.gridView1.Columns[3].Visible = false;
            this.gridView1.Columns[4].Visible = false;
            this.gridView1.Columns[5].Caption = "PUV";
            this.gridView1.Columns[6].Caption = "Prix ttc";
            this.gridView1.Columns[7].Visible = false;
            this.gridView1.Columns[8].Caption = "remise";
            this.gridView1.Columns[9].Caption = "TVA";
            this.gridView1.Columns[10].Caption = "unité";
            this.gridView1.Columns[11].Caption = "Qte restante";
            this.gridView1.Columns[12].Visible =false;
            gridView1.OptionsView.ShowAutoFilterRow = true;

        }
        public void updatesum()
        {
            prixtotc = 0;
            //double.TryParse(textBox5.Text.Replace('.', ','), out prixtotc);

            for (int i = 0; i < gridView1.RowCount; i++)
            {

                piece_fact row1 = (piece_fact)gridView1.GetRow(i);
                prixtotc += Convert.ToDouble(row1.pv.ToString().Replace('.', ','));

            }


            textBox1.Text = prixtotc.ToString("0.000");

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
                dt_fact_n = DBL.GetFactBynum(int.Parse(Tnumfact.Text));
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
                            piece_fact row1 = new piece_fact();
                            row1 = (piece_fact)gridView1.GetRow(i);
                            prix_r = Convert.ToDouble(row1.pv.ToString().Replace('.', ','));                         
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
                        fac.montant_hr = tt_hr;
                        fac.montant = tt_r;
                        fac.L_num_bl = textEdit1.Text;
                        DBL.ajouterFacture(fac);
                        foreach (int i in _LidBl)
                        {
                            bon_livraison bl = new bon_livraison();
                            bl = DBL.GetBLBynum(i);
                            bl.etat = "facturée";
                            DBL.modifier(bl);




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

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }
    }
}