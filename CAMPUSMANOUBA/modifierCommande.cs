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
    public partial class modifierCommande : DevExpress.XtraEditors.XtraForm
    {
        List<ligne_bc> darttab = new List<ligne_bc>();
        public static int idpiece = 0;
        public static int numligne = 0;
        BCService BC = new BCService();
        ArtService art = new ArtService();
        ClientService cli = new ClientService();
        public static Double prixtotc = 0;
        //public static DataTable darttab;
        public static string codepiece = "";
        public static Double prixtot = 0;
        public modifierCommande(bon_commande bc)
        {
            InitializeComponent();

            //lookUpEdit2.Text = "";
            prixtotc = 0;
            tnumcommandebase.Text = bc.numero_bc.ToString();
            bc = BC.GetAllBcBynum(int.Parse(tnumcommandebase.Text));
            lookUpEdit1.EditValue = bc.client;
            dateEdit1.DateTime =Convert.ToDateTime( bc.date_ajout);
            textBox5.Text = bc.montant.ToString();
            //tnbcmd.Text = bc.id.ToString();
            darttab = BC.getLbcByCodeBC((int)bc.numero_bc);
            gridControl1.DataSource = darttab;
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
                //if (ttva.Text == "")
                //{
                //    ttva.Text = "0";
                //}
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
                Double pvhorstva = Convert.ToDouble(tpu.Text.Replace('.', ',')) * Convert.ToDouble(tquantit.Text.Replace('.', ','));
                //Double prixremis = pvhorstva - ((pvhorstva * Convert.ToDouble(tremise.Text.Replace('.', ','))) / 100);
                //Double pvtva = prixremis + ((prixremis * Convert.ToDouble(ttva.Text.Replace('.', ','))) / 100);


                Livre s = (Livre)lookUpEdit2.GetSelectedDataRow();
                client c = (client)lookUpEdit1.GetSelectedDataRow();


                ligne_bc lBc = new ligne_bc();
                lBc.code_art = s.codeart; ;
                lBc.designation_article = mdesign.Text;
                lBc.quantite_article =Convert.ToDouble( tquantit.Text.Replace('.',','));
                lBc.puv = Convert.ToDouble(tpu.Text.Replace('.', ','));
                lBc.totvente = pvhorstva;
                lBc.id_bc = int.Parse(tnumcommandebase.Text);

                darttab.Add(lBc);
                getalldatatable();

                updatesum();

                lookUpEdit2.EditValue = null;
                mdesign.Text = "";
                tquantit.Text = "0";
                tpu.Text = "0";



                //}
                //catch (Exception except)
                //{
                //    MessageBox.Show("vérifier les valeurs entrées");
                //}
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
            client cli = (client)lookUpEdit1.GetSelectedDataRow();
            int numer_bc = int.Parse(tnumcommandebase.Text);
            bon_commande dt_blo = new bon_commande();

            if (tnumcommandebase.Text == "")
            {
                MessageBox.Show("Entrer le numero de BC");
            }
            else
            {

                List<ligne_bc> lbc = new List<ligne_bc>();
                bon_commande bc = BC.GetAllBcBynum(int.Parse(tnumcommandebase.Text));
                lbc = BC.getLbcByCodeBC((int)bc.numero_bc);

                BC.supprimerLbc(lbc);


                prixtotc = 0;

                for (int i = 0; i < gridView1.RowCount; i++)
                {
                    ligne_bc lBc = new ligne_bc();
                    lBc = (ligne_bc)gridView1.GetRow(i);
                    Double pnetvente = Convert.ToDouble(lBc.quantite_article.ToString().Replace('.', ',')) * Convert.ToDouble(lBc.puv.ToString().Replace('.', ','));

                    prixtotc += pnetvente;

                    lBc.id_bc = int.Parse(tnumcommandebase.Text);
                    BC.ajouterLBc(lBc);


                }


                bon_commande Bc = BC.GetAllBcBynum(int.Parse(tnumcommandebase.Text));

                Bc.date_ajout = dateEdit1.DateTime;

                if (cli != null)
                {
                    Bc.client = cli.raisonsoc;
                    Bc.id_clt = cli.codeclient.ToString();
                }
                Bc.montant = prixtotc;
                BC.modifierBC(Bc);
                gridControl1.DataSource = null;
                gridView1.Columns.Clear();
                darttab.Clear();
                UpdateCmd bcom = new UpdateCmd();


                MessageBox.Show("la commande est modifiée avec succées veuillez Actualiser la liste des commandes clients");
                this.Close();
            }


            //listecmds lb = new listecmds();
            //lb.Show();
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

                    ligne_bc lBc = (ligne_bc)gridView1.GetRow(gridView1.FocusedRowHandle);


                    lookUpEdit2.EditValue = lBc.code_art;
                    mdesign.Text = lBc.designation_article;
                    tquantit.Text = lBc.quantite_article.ToString();
                    tpu.Text = lBc.puv.ToString();
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
                    Double pvhorstva = Convert.ToDouble(tpu.Text.Replace('.', ',')) * Convert.ToDouble(tquantit.Text.Replace('.', ','));

                    Livre s = (Livre)lookUpEdit2.GetSelectedDataRow();

                    client c = (client)lookUpEdit1.GetSelectedDataRow();
                    ligne_bc lBc = (ligne_bc)gridView1.GetRow(gridView1.FocusedRowHandle);
                    int pos = darttab.IndexOf(lBc);
                    darttab.RemoveAt(pos);

                    lBc = new ligne_bc();
                    lBc.code_art = s.codeart;
                    lBc.designation_article = mdesign.Text;
                    lBc.quantite_article =Convert.ToDouble( tquantit.Text.Replace('.', ','));
                    lBc.puv =Convert.ToDouble( tpu.Text.Replace('.',','));
                    lBc.totvente = pvhorstva;
                    lBc.id_bc = int.Parse(tnumcommandebase.Text);
                    darttab.Insert(pos, lBc);
                    gridControl1.DataSource = null;
                    gridControl1.DataSource = darttab;
                    //gridControl1.Refresh();
                    gridView1.OptionsView.ShowAutoFilterRow = true;
                    updatesum();

                    lookUpEdit2.EditValue = null;
                    mdesign.Text = "";
                    tquantit.Text = "0";
                    tpu.Text = "0";
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
                    ligne_bc row = (ligne_bc)gridView1.GetRow(gridView1.FocusedRowHandle);
                    darttab.Remove(row);
                    gridControl1.DataSource = null;
                    gridControl1.DataSource = darttab;
                    MessageBox.Show("la commande a été mise à jour");
                    updatesum();

                    lookUpEdit2.EditValue = null;
                    mdesign.Text = "";
                    tquantit.Text = "0";
                    tpu.Text = "0";


                }

            }
            catch (Exception exc)
            { }
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

        public void updatesum()
        {
            try
            {
                prixtotc = 0;
                for (int i = 0; i < gridView1.RowCount; i++)
                {

                    ligne_bc lBc = (ligne_bc)gridView1.GetRow(i);
                    prixtotc += Convert.ToDouble(lBc.totvente.ToString().Replace('.', ','));
                    //prix_ht += Convert.ToDouble(row1[2].ToString().Replace('.', ',')) * Convert.ToDouble(row1[5].ToString().Replace('.', ','));
                }

                textBox5.Text = prixtotc.ToString();


            }
            catch (Exception xce)
            {
            }
        }


    }
}