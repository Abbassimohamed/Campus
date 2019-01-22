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
namespace CAMPUSMANOUBA
{
    public partial class UpdateCmd : DevExpress.XtraEditors.XtraForm
    {



        public UpdateCmd()
        {
            InitializeComponent();

        }
        List<ligne_bc> dartt = new List<ligne_bc>();
        ArtService artser = new ArtService();
        reservationservice reservser = new reservationservice();
        ClientService DC = ClientService.Instance();
        ArtService DS = ArtService.Instance();
        public static Double prixtotc, prix_rem;
        BCService dbc =new BCService();
        public static bon_commande bcmdcoming = new bon_commande();
        public UpdateCmd(bon_commande cmd)
        {
            InitializeComponent();
           
            tnumcommandebase.Text = cmd.numero_bc.ToString();
            lookUpEdit1.EditValue = cmd.id_clt;
            dateEdit2.EditValue = cmd.date_ajout;
            memoEdit2.EditValue = cmd.comment;
           
            List<ligne_bc> lbcs = new List<ligne_bc>();
            lbcs = dbc.getLbcByCodeBC((int)cmd.numero_bc);
            dartt = lbcs;
            if(cmd.etat == "livré" || cmd.etat == "annulé")
            {
                simpleButton8.Enabled = false;
                simpleButton9.Enabled = false;

            }
            getalldatatable(dartt);
            updatesum();
            bcmdcoming = cmd;


        }
        private static UpdateCmd instance;
        public static UpdateCmd Instance()
        {
            if (instance == null)

                instance = new UpdateCmd();

            return instance;

        }
        private void clients()
        {

            lookUpEdit1.Properties.DataSource = null;
            List<client> Allclients = new List<client>();
            Allclients = DC.listclts();

            lookUpEdit1.Properties.ValueMember = "codeclient";
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
        private void getalldatatable(List<ligne_bc> bcmds)
        {
            gridControl2.DataSource = null;
            gridView2.Columns.Clear();
            gridControl2.DataSource = bcmds;
            gridView2.Columns[0].Visible = false;
            gridView2.Columns[1].Visible = false;
            gridView2.Columns[2].Caption = "Code";
            gridView2.Columns[3].Caption = "Designation";
            gridView2.Columns[4].Caption = "Quantite";
            gridView2.Columns[5].Caption = "Puv";
            gridView2.Columns[6].Caption = "Total sans remise";
            gridView2.Columns[7].Caption = "Remise";
            gridView2.Columns[8].Caption = "Total";
            gridView2.Columns[9].Visible =false;
            gridView2.Columns[10].Caption = "Quantité servie";
            gridView2.Columns[11].Caption = "Quantité restante";

        }
        public void updatesum()
        {
            try
            {
                prixtotc = 0;
                prix_rem = 0;
                for (int i = 0; i < gridView2.RowCount; i++)
                {

                    ligne_bc lbl = (ligne_bc)gridView2.GetRow(i);
                    prixtotc += Convert.ToDouble(lbl.prixremis.ToString().Replace('.', ','));
                    textEdit8.Text = prixtotc.ToString();
                    prix_rem = prixtotc - prixtotc * (Convert.ToDouble(textEdit6.Text)) / 100;
                    textEdit9.Text = prix_rem.ToString();
                }



            }
            catch (Exception xce)
            {
            }
        }

        private void gridControl2_Click(object sender, EventArgs e)
        {
            try
            {
                int count = gridView2.DataRowCount;
                if (count != 0 && gridView2.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {

                    ligne_bc lbl = (ligne_bc)gridView2.GetRow(gridView2.FocusedRowHandle);
                    lookUpEdit2.EditValue = lbl.code_art;
                    mdesign.Text = lbl.designation_article;
                    textEdit1.Text = lbl.quantite_article.ToString();
                    tpu.Text = lbl.puv.ToString();
                    textEdit2.Text = lbl.remise.ToString();
                    textEdit3.Text = lbl.idauteur.ToString();
                    simpleButton7.Enabled = false;
                    simpleButton4.Enabled = true;
                    simpleButton6.Enabled = true;
                }
            }
            catch (Exception except)
            { }
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            if (gridView2.RowCount != 0)
            {
                DialogResult dialogResult = MessageBox.Show("Vous etes sure de confirmer la modification effectué", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (dialogResult == DialogResult.Yes)
                {
                    client cl = (client)lookUpEdit1.GetSelectedDataRow();
                    List<ligne_bc> lbcs = new List<ligne_bc>();
                    lbcs = dbc.getLbcByCodeBC(Convert.ToInt32(tnumcommandebase.Text));
                    foreach(ligne_bc lbc in lbcs)
                    {
                        artser.annulerreserver(lbc.code_art, (double)lbc.quantite_article);
                        reservser.deleteallreserv(Convert.ToInt32(tnumcommandebase.Text));
                    }
                    dbc.supprimerLbc(lbcs);
                    bon_commande bc = new bon_commande();
                    bc.numero_bc = bcmdcoming.numero_bc;
                    bc.id = bcmdcoming.id;
                    bc.numerodevis = bcmdcoming.numerodevis;
                    bc.client = cl.raisonsoc;
                    bc.id_clt = cl.codeclient.ToString();
                    bc.comment = memoEdit2.Text;
                    bc.date_ajout = dateEdit2.DateTime;
                    bc.montant = Convert.ToDouble(textEdit8.Text);
                    bc.remise = Convert.ToDouble(textEdit6.Text);
                    bc.prixremise = Convert.ToDouble(textEdit9.Text);
                    bc.etat = bcmdcoming.etat;
                   

                    dbc.modifierBC(bc);

                    for (int i = 0; i < gridView2.RowCount; i++)
                    {
                        ligne_bc lbc = (ligne_bc)gridView2.GetRow(i);
                        artser.reserver(lbc.code_art,(double)lbc.quantite_article);
                        dbc.ajouterLBc(lbc);
                        reservation reservat = new reservation();
                        reservat.ncmd = int.Parse(tnumcommandebase.Text);
                        reservat.article = lbc.code_art;
                        reservat.quantit = lbc.quantite_article;
                        reservat.date = System.DateTime.Now;
                      
                        reservser.addReservation(reservat);
                    }

                    MessageBox.Show("Modification effectuée avec succés");
                }
                else if (dialogResult == DialogResult.No)
                {

                }
            }
            else
            {
                MessageBox.Show("La liste des articles est vide .Veuillez la remplir");

            }
        }

        private void lookUpEdit2_EditValueChanged(object sender, EventArgs e)
        {
            Livre s = (Livre)lookUpEdit2.GetSelectedDataRow();
            if (s != null)
            {
                simpleButton7.Enabled = true;
                simpleButton4.Enabled = false;
                simpleButton6.Enabled = false;
                mdesign.Text = s.titre;
                tpu.Text = s.pvpublic.ToString();
                textEdit3.Text = s.auteur.ToString();
            }
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Vous etes sure d'annuler la modification en cours?", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                MessageBox.Show("Modification annulé");

            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            try
            {

                Livre s = (Livre)lookUpEdit2.GetSelectedDataRow();
                client c = (client)lookUpEdit1.GetSelectedDataRow();
                if (artser.checkavailab(s.codeart, Convert.ToDouble(tquantit.Text.Replace('.', ','))) == 1)
                {
                    Double pvhorstva = 0;

                    pvhorstva = Convert.ToDouble(tpu.Text.Replace('.', ',')) * Convert.ToDouble(tquantit.Text.Replace('.', ','));
                    Double prixremis = 0;
                    prixremis = pvhorstva - (pvhorstva * Convert.ToDouble(tremise.Text.Replace('.', ',')) / 100);
                    //Double pvtva = prixremis + ((prixremis * Convert.ToDouble(ttva.Text.Replace('.', ','))) / 100);

                    ligne_bc lBc = new ligne_bc();
                    lBc.code_art = s.codeart; ;
                    lBc.designation_article = mdesign.Text;
                    lBc.quantite_article = Convert.ToDouble(tquantit.Text.Replace('.', ','));
                    lBc.puv = Convert.ToDouble(tpu.Text.Replace('.', ','));
                    lBc.totvente = pvhorstva;
                    lBc.id_bc = int.Parse(tnumcommandebase.Text);
                    lBc.remise = Convert.ToDouble(tremise.Text.Replace('.', ','));
                    lBc.prixremis = prixremis;
                    lBc.idauteur = Convert.ToInt32(textEdit1.Text);

                    dartt.Add(lBc);
                    getalldatatable(dartt);

                    updatesum();
                    lookUpEdit2.EditValue = null;
                    mdesign.Text = null;
                    tquantit.Text = null;
                    tremise.Text = null;
                    tpu.Text = null;
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
                            bcmd = dbc.GetAllBcBynum((Int32)rsv.ncmd);
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
    
        private void upadetallANNULER(int num)
        {

            bon_commande bcmd = dbc.GetAllBcBynum(num);
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
                    dbc.modifierBC(bcmd);

                    foreach (reservation reserv in reservations)
                    {
                        artser.annulerreserver(reserv.article, Convert.ToDouble(reserv.quantit));

                    }

                    MessageBox.Show("La commande a été annulé.La quantité est disponible en stock");
                

                }


            }
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            //try
            //{

            Livre s = (Livre)lookUpEdit2.GetSelectedDataRow();
                client c = (client)lookUpEdit1.GetSelectedDataRow();
                ligne_bc lBc = (ligne_bc)gridView2.GetRow(gridView2.FocusedRowHandle);
                int pos = dartt.IndexOf(lBc);
                dartt.RemoveAt(pos);
                Double qttocheckfor = 0;
                qttocheckfor = Convert.ToDouble(textEdit1.Text.Replace('.', ','))-Convert.ToDouble( lBc.quantite_article);
              
                if (artser.checkavailab(s.codeart, qttocheckfor) == 1 )
                {
                   
                    Double pvhorstva = 0;

                    pvhorstva = Convert.ToDouble(tpu.Text.Replace('.', ',')) * Convert.ToDouble(textEdit1.Text.Replace('.', ','));
                    Double prixremis = 0;
                    prixremis = pvhorstva - (pvhorstva * Convert.ToDouble(textEdit2.Text.Replace('.', ',')) / 100);
                    //Double pvtva = prixremis + ((prixremis * Convert.ToDouble(ttva.Text.Replace('.', ','))) / 100);

                    lBc.code_art = s.codeart; ;
                    lBc.designation_article = mdesign.Text;
                    lBc.quantite_article = Convert.ToDouble(textEdit1.Text.Replace('.', ','));
                    lBc.puv = Convert.ToDouble(tpu.Text.Replace('.', ','));
                    lBc.totvente = pvhorstva;
                    lBc.id_bc = int.Parse(tnumcommandebase.Text);
                    lBc.remise = Convert.ToDouble(textEdit2.Text.Replace('.', ','));
                    lBc.prixremis = prixremis;
                    lBc.idauteur =Convert.ToInt32( textEdit3.Text);
                    lBc.qtitrest = lBc.qtitrest+qttocheckfor;
                    lBc.qtitservi = lBc.qtitservi;
                    dartt.Add(lBc);
                    getalldatatable(dartt);

                    updatesum();
                    lookUpEdit2.EditValue = null;
                    mdesign.Text = null;
                    textEdit1.Text = null;
                    textEdit2.Text = null;
                    textEdit3.Text = null;
                    tpu.Text = null;
                }
                else
                {
                    MessageBox.Show("La quantité demandé n'est pas disponible en stock,la quantité en stock de l'article" + s.titre + "est de " + s.quantitenstock + " .Veuillez ne pas dépasser cette quantité ");
                }

            //}
            //catch (Exception except)
            //{
            //    MessageBox.Show("vérifier les valeurs entrées");
            //}

        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            try
            {
                int count = gridView2.DataRowCount;
                if (count != 0 && gridView2.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {

                    ligne_bc lBc = (ligne_bc)gridView2.GetRow(gridView2.FocusedRowHandle);
                    dartt.Remove(lBc);
                    MessageBox.Show("Ligne commande supprimée, cliquez sur valider");
                    gridControl2.DataSource = null;
                    gridView2.Columns.Clear();
                    gridControl2.DataSource = dartt;
                    updatesum();
                    lookUpEdit2.EditValue = null;
                    mdesign.Text = "";
                    textEdit1.Text = null;
                    tpu.Text = null;
                    textEdit2.Text = null;
                    textEdit3.Text = null;
                }
            }
            catch (Exception exc)
            { }
        }

        private void textEdit6_EditValueChanged(object sender, EventArgs e)
        {
            updatesum();
        }

        private void UpdateCmd_Load(object sender, EventArgs e)
        {
            clients();
            articles();
            simpleButton7.Enabled = true;
            simpleButton4.Enabled = false;
            simpleButton6.Enabled = false;
        }
    }
}