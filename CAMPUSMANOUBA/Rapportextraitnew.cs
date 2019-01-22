using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Data.SqlTypes;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DAL;
using BLL;
namespace CAMPUSMANOUBA
{
    public partial class Rapportextraitnew : DevExpress.XtraEditors.XtraForm
    {
        public Rapportextraitnew()
        {
            InitializeComponent();
          
    }
        private void articles2()
        {
            //get All article
            lookUpEdit3.Properties.DataSource = null;
            lookUpEdit3.Properties.ValueMember = "codeart";
            lookUpEdit3.Properties.DisplayMember = "codeart";
            lookUpEdit3.Properties.DataSource = darticle;
            lookUpEdit3.Properties.PopulateColumns();
            lookUpEdit3.Properties.Columns["idarticle"].Visible = false;
            lookUpEdit3.Properties.Columns["codeart"].Caption = "Code article";
            lookUpEdit3.Properties.Columns["isbn"].Caption = "ISBN";
            lookUpEdit3.Properties.Columns["famille"].Visible = false;
            lookUpEdit3.Properties.Columns["idfamille"].Visible = false;
            lookUpEdit3.Properties.Columns["sousfamille"].Visible = false;
            lookUpEdit3.Properties.Columns["idsousfamille"].Visible = false;
            lookUpEdit3.Properties.Columns["titre"].Caption = "titre";
            lookUpEdit3.Properties.Columns["dateedition"].Visible = false;
            lookUpEdit3.Properties.Columns["imprimerie"].Visible = false;
            lookUpEdit3.Properties.Columns["idimprim"].Visible = false;
            lookUpEdit3.Properties.Columns["auteur"].Visible = false;
            lookUpEdit3.Properties.Columns["idauteur"].Visible = false;
            lookUpEdit3.Properties.Columns["quantitenstock"].Visible = false;
            lookUpEdit3.Properties.Columns["depotlegal"].Visible = false;
            lookUpEdit3.Properties.Columns["pvpublic"].Visible = false;
            lookUpEdit3.Properties.Columns["pvpromo"].Visible = false;
            lookUpEdit3.Properties.Columns["droitaut"].Visible = false;
            lookUpEdit3.Properties.Columns["abscice"].Visible = false;
            lookUpEdit3.Properties.Columns["ordonne"].Visible = false;
            lookUpEdit3.Properties.Columns["image"].Visible = false;

        }
        public static List<Livre> darticle = new List<Livre>();
        ArtService artser = ArtService.Instance();
        public static Rapportextraitnew instance;
        public static Rapportextraitnew Instance()
        {
            if (instance == null)

                instance = new Rapportextraitnew();
            return instance;

        }
        private static int indice = 0;//article;
        ArtService DS = new ArtService();
        AutService ser = new AutService();
        BLService blser = new BLService();
        public static exauteur ex=new exauteur();
        public static     List<Livre> artsfind = new List<Livre>();
        public void getallauthor()
        {
            List<auteur> authors = new List<auteur>();
            authors = ser.getallauthor();

            lookaut.Properties.ValueMember = "codeauteur";
            lookaut.Properties.DisplayMember = "nom";
            lookaut.Properties.DataSource = authors;
            lookaut.Properties.PopulateColumns();
            lookaut.Properties.Columns["codeauteur"].Visible = false;
            lookaut.Properties.Columns["numeroaut"].Visible = false;
            lookaut.Properties.Columns["nom"].Caption = "Nom";
            lookaut.Properties.Columns["prenom"].Visible = false;
            lookaut.Properties.Columns["tel"].Visible = false;
            lookaut.Properties.Columns["adr"].Visible = false;
            lookaut.Properties.Columns["email"].Visible = false;
            lookaut.Properties.Columns["institution"].Visible = false;
            lookaut.Properties.Columns["specialite"].Visible = false;
            lookaut.Properties.Columns["ville"].Visible = false;
            lookaut.Properties.Columns["codepostal"].Visible = false;
            lookaut.Properties.Columns["web"].Visible = false;
            lookaut.Properties.Columns["image"].Visible = false;


        }
        private void tileItem1_ItemClick(object sender, TileItemEventArgs e)
        {
            indice = 0;
            this.panelControl8.Hide();
            this.panelControl2.Show();
            this.panelControl2.Dock = DockStyle.Fill;

            textEdit1.Text = "25";

        }

        private void Rapportextrait_Load(object sender, EventArgs e)
        {

       
            getallauthor();
            this.panelControl8.Hide();
            this.panelControl2.Show();
            this.panelControl2.Dock = DockStyle.Fill;
            textEdit1.Text = "25";
        }

        private void tileItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            indice = 1;
            this.panelControl2.Hide();
            this.panelControl8.Show();
            this.panelControl8.Dock = DockStyle.Fill;
            darticle = artser.getallart();
            articles2();

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            try
            {

                if(dateEdit1.EditValue==null)
                {
                    dxErrorProvider1.SetError(dateEdit1, "Error", DevExpress.XtraEditors.DXErrorProvider.ErrorType.User1);
                }
                else
                {
                    dxErrorProvider1.Dispose();
                }
                if (dateEdit2.EditValue == null)
                {
                    dxErrorProvider1.SetError(dateEdit2, "Error", DevExpress.XtraEditors.DXErrorProvider.ErrorType.User1);
                }
                else
                {
                    dxErrorProvider1.Dispose();
                }
                int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {

                List<piece_fact> pcfact = new List<piece_fact>();
                    List<manuel> manuels = new List<manuel>();
                foreach (int i in gridView1.GetSelectedRows())
                {

                        Livre art = new Livre();
                    art = (Livre)gridView1.GetRow(i);
                    pcfact.AddRange(blser.rapportventearticle(art, dateEdit1.DateTime, dateEdit2.DateTime));

                  
                }
                    double total=0;
                    double quantit = 0;
                    double percentage = 0;
                    foreach (piece_fact pfact in pcfact)
                    {
                        facturevente factv = new facturevente();
                        factv= blser.GetFactBynum((int)pfact.id_fact);
                        manuel man = new manuel();
                        man.numfacture =(int) pfact.id_fact;
                        man.quantit =(double) pfact.quantite_piece_u;
                        man.montant= (double)pfact.pv;
                        man.nquit = factv.quittance;
                        man.datequitt =Convert.ToDateTime( factv.datequittance);
                        manuels.Add(man);
                        total+= (double)pfact.pv;
                        quantit += (double)pfact.quantite_piece_u;
                    }
                    gridControl2.DataSource = null;
                    gridView2.Columns.Clear();
                    gridControl2.DataSource = manuels;
                    gridView2.Columns[0].Caption = "Facture";
                    gridView2.Columns[1].Caption = "Date quittance";
                    gridView2.Columns[2].Caption = "Quantité";
                    gridView2.Columns[3].Caption = "N° Quittance";
                    gridView2.Columns[4].Caption = "Montant";
                    labelControl9.Text = quantit.ToString();
                    labelControl10.Text = total.ToString();
                    percentage = total * Convert.ToDouble(textEdit1.Text)/ 100;
                    labelControl13.Text = percentage.ToString();
                }

            }catch(Exception ex)
            {
               
            }
        }
       
        private void fillgrid(List<Livre> arts)
        {

            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = arts;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Caption = "Code";
            gridView1.Columns[2].Caption = "ISBN";
            gridView1.Columns[3].Visible = false;
            gridView1.Columns[4].Visible = false;
            gridView1.Columns[5].Visible = false;
            gridView1.Columns[6].Visible = false;
            gridView1.Columns[7].Caption = "Titre";
            gridView1.Columns[8].Visible = false;
            gridView1.Columns[9].Visible = false;
            gridView1.Columns[10].Visible = false;
            gridView1.Columns[11].Visible = false;
            gridView1.Columns[12].Visible = false;
            gridView1.Columns[13].Visible = false;
            gridView1.Columns[14].Visible = false;
            gridView1.Columns[15].Visible = false;
            gridView1.Columns[16].Visible = false;
            gridView1.Columns[17].Visible = false;
            gridView1.Columns[18].Visible = false;
            gridView1.Columns[19].Visible = false;
            gridView1.Columns[20].Visible = false;
            gridView1.Columns[21].Visible = false;
            gridView1.Columns[22].Visible = false;
            gridView1.Columns[23].Visible = false;

        }
        
        private void lookaut_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                auteur aut = new auteur();
                aut = (auteur)lookaut.GetSelectedDataRow();
                int codeaut = (int)aut.numeroaut;
                List<Livre> piecefacts = new List<Livre>();
                //piecefacts = DS.getallartbyaut(codeaut);
                fillgrid(piecefacts);
            }
            catch(Exception except)
            {

            }
          
        }

        private void tileItem3_ItemClick(object sender, TileItemEventArgs e)
        {
            this.Close();
        }

        private void panelControl3_Paint(object sender, PaintEventArgs e)
        {

        }
        private void fillgrid1(List<Cumul> piececumuls)
        {
            gridControl4.DataSource = null;
            gridView4.Columns.Clear();
            gridControl4.DataSource = piececumuls;
            gridView4.Columns[0].Caption = "Date";
            gridView4.Columns[1].Caption = "Entrée";
            gridView4.Columns[2].Caption = "Retour";
            gridView4.Columns[3].Caption = "Sortie";
            gridView4.Columns[4].Caption = "N.B.E";
            gridView4.Columns[5].Caption = "N.B.S";
            gridView4.Columns[6].Caption = "N.B.L";
            gridView4.Columns[7].Visible = false;
            gridView4.Columns[8].Caption = "N.fact";
            gridView4.Columns[9].Caption = "Observation";


        }

        private void lookUpEdit3_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                dateEdit2.EditValue = null;
                dateEdit3.EditValue = null;
                Livre art = (Livre)lookUpEdit3.GetSelectedDataRow();
                List<Cumul> cumuls = artser.getallcumulbyarticle(art.codeart);
                fillgrid1(cumuls);
            }
            catch (Exception exc)
            { }
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {

            if (lookUpEdit3.EditValue != null && dateEdit2.EditValue != null && dateEdit3.EditValue != null)
            {
                Livre art = (Livre)lookUpEdit3.GetSelectedDataRow();
                DateTime date1 = new DateTime();
                date1 = dateEdit2.DateTime;
                DateTime date2 = new DateTime();
                date2 = dateEdit3.DateTime;
                List<Cumul> cumuls = artser.getallcumulbyarticlebetweendate(art.codeart, date1, date2);
                fillgrid1(cumuls);
            }
            else if (lookUpEdit3.EditValue != null && dateEdit2.EditValue != null)
            {
                Livre art = (Livre)lookUpEdit3.GetSelectedDataRow();
                DateTime date1 = new DateTime();
                date1 = dateEdit2.DateTime;

                List<Cumul> cumuls = artser.getallcumulbyarticleafterndate(art.codeart, date1);
                fillgrid1(cumuls);
            }
            else if (lookUpEdit3.EditValue != null && dateEdit3.EditValue != null)
            {
                Livre art = (Livre)lookUpEdit3.GetSelectedDataRow();
                DateTime date1 = new DateTime();
                date1 = dateEdit2.DateTime;

                List<Cumul> cumuls = artser.getallcumulbyarticlebeforedate(art.codeart, date1);
                fillgrid1(cumuls);
            }
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            ClearAllForm(this);
            gridControl4.DataSource = null;
            gridView4.Columns.Clear();
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

        private void simpleButton10_Click(object sender, EventArgs e)
        {

        }

        private void lookart_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void panelControl3_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void textEdit2_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                string codearticle;
                codearticle = textEdit2.Text;
                Livre art = new Livre();
                List<Livre> arts = new List<Livre>();
                art = artser.getArticleByCode(codearticle);
                textEdit3.Text = art.titre;
            }
               catch(Exception ex)
            { }
          
          
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            gridControl1.DataSource = null;
        }

        private void panelControl11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {

            string codearticle;
            codearticle = textEdit2.Text;
            Livre art = new Livre();
       
            art = artser.getArticleByCode(codearticle);
            if(!artsfind.Contains(art))
            {
                artsfind.Add(art);
            }
          
            fillgrid(artsfind);
            textEdit3.Text = "";
            textEdit2.Text = "";
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Sure de supprimer cette ligne ?", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {

                gridView1.DeleteRow(gridView1.FocusedRowHandle);
            }
            else if (dialogResult == DialogResult.No)
            {

            }
          
        }
    }
}