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
using DevExpress.XtraReports.UI;
using DAL;
using BLL;
using CAMPUSMANOUBA.Report;
namespace CAMPUSMANOUBA
{
    public partial class ReglementForm : DevExpress.XtraEditors.XtraForm
    {
        public ReglementForm()
        {
            InitializeComponent();
        }
  
        private static ReglementForm instance;
        public static ReglementForm Instance()
        {
            if (instance == null)

                instance = new ReglementForm();

            return instance;

        }
        QuittanceService quitser = new QuittanceService();

        BLService blser = new BLService();
        ClientService clserv = new ClientService();
        BankService bankser = new BankService();
        ReglementService regserv = new ReglementService();
        List<Banque> banks=new List<Banque>();
        private void fillgrid(List<facturevente> fatvents)
        {
            gridControl2.DataSource = null;
            gridView2.Columns.Clear();
            gridControl2.DataSource = fatvents;
            this.gridView2.Columns[0].Visible = false;
            this.gridView2.Columns[1].Caption = "Numero facture";
            this.gridView2.Columns[2].Caption = "Date facture";
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
            this.gridView2.Columns[13].Visible = false;
            this.gridView2.Columns[14].Visible = false;

        }
        private void fillgridreg(List<Reglement> regs)
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = regs;
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[1].Caption = "Date";
            this.gridView1.Columns[2].Caption = "N° facture";
            this.gridView1.Columns[3].Caption = "Code Client";
            this.gridView1.Columns[4].Caption = "Montant";
            this.gridView1.Columns[5].Caption = "Remise";
            this.gridView1.Columns[6].Caption = "Net à payer";
            this.gridView1.Columns[7].Caption = "Type";
            this.gridView1.Columns[8].Caption = "Banque";
            this.gridView1.Columns[9].Caption = "RIB";
            this.gridView1.Columns[10].Caption = "N° doc";
            this.gridView1.Columns[11].Caption = "Date échéance";
          

        }
        private void fillgridquittance(List<Quittance> quitts)
        {
            gridControl3.DataSource = null;
            gridView3.Columns.Clear();
            gridControl3.DataSource = quitts;
            this.gridView3.Columns[0].Visible = false;
            this.gridView3.Columns[1].Caption = "N° quittance";
            this.gridView3.Columns[2].Caption = "Date";
            this.gridView3.Columns[3].Caption = "N° facture";
            this.gridView3.Columns[4].Caption = "Code Client";
            this.gridView3.Columns[5].Caption = "Total";
            this.gridView3.Columns[6].Caption = "Remise";
            this.gridView3.Columns[7].Caption = "Net à payer";
         


        }
        private void lookclients(List<client> clts)
        {

            lookclient.Properties.DataSource = null;          
            lookclient.Properties.ValueMember = "numerocl";
            lookclient.Properties.DisplayMember = "raisonsoc";
            lookclient.Properties.DataSource = clts;
            lookclient.Properties.PopulateColumns();
            lookclient.Properties.Columns["codeclient"].Visible = false;
            lookclient.Properties.Columns["numerocl"].Caption = "Numéro client";
            lookclient.Properties.Columns["raisonsoc"].Caption = "Raison sociale";
            lookclient.Properties.Columns["resp"].Caption = "Responsable";
            lookclient.Properties.Columns["qualite"].Visible = false;
            lookclient.Properties.Columns["tel"].Visible = false;
            lookclient.Properties.Columns["mobile"].Visible = false;
            lookclient.Properties.Columns["adresse"].Visible = false;
            lookclient.Properties.Columns["codepostal"].Visible = false;
            lookclient.Properties.Columns["ville"].Visible = false;
            lookclient.Properties.Columns["web"].Visible = false;
            lookclient.Properties.Columns["email"].Visible = false;
            lookclient.Properties.Columns["fax"].Visible = false;

        }
        private void lookbnq(List<Banque> bnqs)
        {

            lookbanque.Properties.DataSource = null;
            lookbanque.Properties.ValueMember = "idbanque";
            lookbanque.Properties.DisplayMember = "nombanque";
            lookbanque.Properties.DataSource = bnqs;
            lookbanque.Properties.PopulateColumns();
            lookbanque.Properties.Columns["idbanque"].Visible = false;
            lookbanque.Properties.Columns["nombanque"].Caption = "Banque";
            lookbanque.Properties.Columns["rib"].Caption = "R.I.B";
            lookbanque.Properties.Columns["soldeinitial"].Caption = "Solde intitial";
      

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

        private void Reglement_Load(object sender, EventArgs e)
        {
            datesaisie.DateTime = System.DateTime.Today;
            List<facturevente> facts = new List<facturevente>();
            facts = blser.GetFactByetat("non règlé");
            fillgrid(facts);
            List<string> types = new List<string>();
            types.Add("Espèces");
            types.Add("Chèques");
            types.Add("CCP");
            types.Add("Traite");
            types.Add("Autre");
            looktype.Properties.DataSource = types;
            looktype.EditValue = "Espèces";
            banks = bankser.getbanks();
            lookbnq(banks);
        }

   
     

        private void panelControl6_Paint(object sender, PaintEventArgs e)
        {

        }
        private void calculsum()
        {
            try
            {
                double totpayer = 0;
                double remise = 0;
                double netpayer = 0;

                totpayer = Convert.ToDouble(ttot.Text);
                remise = Convert.ToDouble(tremise.Text);
                netpayer = totpayer - totpayer * remise / 100;
                tnetapayer.Text = netpayer.ToString();
            }
          catch(Exception ex)
            { }

        }
        private void simpleButton7_Click(object sender, EventArgs e)
        {
            memoEdit1.Text = "";
            datesaisie.DateTime = System.DateTime.Today;
            int count = gridView2.RowCount;
            double totapayer = 0;
            List<client> clients = new List<client>();
            //if (count != 0 && gridView2.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            //{
                foreach  (int i in gridView2.GetSelectedRows() )
                {
                   
                    facturevente factvent = new facturevente();
                    factvent = (facturevente)gridView2.GetRow(i);
                    memoEdit1.Text = memoEdit1.Text+factvent.numero_fact.ToString() + "/";
                    totapayer +=(double) factvent.montant;
                    client clt = new client();
                    clt = clserv.getClientByNumero(Convert.ToInt32( factvent.id_clt));
                    if(! clients.Contains(clt))
                    {
                        clients.Add(clt);
                    }

                }


                lookclients(clients);
                lookclient.EditValue = clients[0].codeclient;
                ttot.Text = totapayer.ToString();
                calculsum();
                gridControl2.Enabled = false;      
            ////}

        }
   
        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show(" Vous etes sure d'annuler l'opération ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                List<client> clts = new List<client>();
                lookclients(clts);
                ClearAllForm(panelControl4);

                gridControl2.Enabled = true;
                looktype.EditValue = "Espèces";
            }
            else if (dialogResult == DialogResult.No)
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
            List<Reglement> regs = new List<Reglement>();
            regs = regserv.findallreg();
            fillgridreg(regs);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Continuer à enregistrer le règlement", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                Reglement reg = new Reglement();
                Quittance quittance = new Quittance();
                reg.date = datesaisie.DateTime;
                reg.nfacture = memoEdit1.Text;
                reg.montant =Convert.ToDouble( ttot.Text.Replace('.',','));
                reg.remise =Convert.ToDouble( tremise.Text);
                reg.netapayer = Convert.ToDouble(tnetapayer.Text);
                reg.typereglement = looktype.EditValue.ToString();
                client c = (client)lookclient.GetSelectedDataRow();
                reg.idclient = c.codeclient;                   
                if(looktype.EditValue== "Chèques" || looktype.EditValue == "Traite")
                {
                    reg.ndoc = tndoc.Text;
                    reg.echeance = dateecheance.DateTime;

                }else
                    if (looktype.EditValue == "CCP")
                {
                    reg.banque = lookbanque.EditValue.ToString();
                    reg.RIB = trib.Text  ;

                }

            
                regserv.addReglement(reg);
              //  reg.nfacture.Remove(7,5);
                foreach (int i in gridView2.GetSelectedRows())
                    {
                        facturevente factv = (facturevente)gridView2.GetRow(i);
                        factv.etat = "règlé";
                 

                        blser.updatefact(factv);

                    }



             
                MessageBox.Show("Ajouté avec succés");
                Quittancereport quitrep = new Quittancereport(reg.nfacture);
                    //Convert.ToInt32(reg.nfacture));
                quitrep.ShowRibbonPreview();
                ClearAllForm(this);
                datesaisie.DateTime = System.DateTime.Today ;
                List<client> clts = new List<client>();
                lookclients(clts);              
                gridControl2.Enabled = true;
                gridControl2.DataSource = null;
                List<facturevente> facts = new List<facturevente>();
                facts = blser.GetFactByetat("non règlé");
                fillgrid(facts);
                looktype.EditValue = "Espèces";
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void lookUpEdit4_EditValueChanged(object sender, EventArgs e)
        {
            Banque bank = new Banque();
            bank = (Banque)lookbanque.GetSelectedDataRow();
            trib.Text = bank.rib;
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                if(looktype.EditValue == "Espèces")
                {
                    lookbanque.Enabled = false;
                    trib.Enabled = false;
                    tndoc.Enabled = false;
                    dateecheance.Enabled = false;
                }
                else if (looktype.EditValue == "Chèques")
                {
                    lookbanque.Enabled = true;
                    trib.Enabled = true;
                    tndoc.Enabled = true;
                    dateecheance.Enabled = true;
                }
                else if (looktype.EditValue == "CCP")
                {
                    lookbanque.Enabled = true;
                    trib.Enabled = true;
                    tndoc.Enabled = false;
                    dateecheance.Enabled = false;
                }
                else if (looktype.EditValue == "Traite")
                {
                    lookbanque.Enabled = true;
                    trib.Enabled = true;
                    tndoc.Enabled = true;
                    dateecheance.Enabled = false;
                }
                else
                {
                    lookbanque.Enabled = true;
                    trib.Enabled = true;
                    tndoc.Enabled = true;
                    dateecheance.Enabled = true;
                }
                   
            }
            catch(Exception exc)
            {

            }
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            if(xtraTabControl1.SelectedTabPage==xtraTabPage2)
            {
             
                fillgridreg(regserv.findallreg());
            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPage3)
            {

                fillgridquittance(quitser.findQuitts());
            }
        }

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void tremise_EditValueChanged(object sender, EventArgs e)
        {
            calculsum();
        }

        private void tileItem5_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage3;
           
            fillgridquittance(quitser.findQuitts());
        }
    }
}