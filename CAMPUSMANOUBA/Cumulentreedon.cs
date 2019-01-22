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
using CAMPUSMANOUBA.Report;
using DevExpress.XtraReports.UI;
namespace CAMPUSMANOUBA
{
    public partial class Cumulentreedon : DevExpress.XtraEditors.XtraForm
    {
        public Cumulentreedon()
        {
            InitializeComponent();
        }
        private static Cumulentreedon instance;
        public static Cumulentreedon Instance()
        {
            if (instance == null)

                instance = new Cumulentreedon();
            return instance;

        }
        BonRetourServices cmser = BonRetourServices.Instance();
        ArtService artser = ArtService.Instance();
        ClientService frser = ClientService.Instance();
        AutService autser = AutService.Instance();
      
        public static List<Livre> darticle = new List<Livre>();

        public static List<Piece_bonret> piececumuls = new List<Piece_bonret>();

        private void articles()
        {
           
            lookUpEdit1.Properties.DataSource = null;
            List<Livre> Allarticle = new List<Livre>();
            Allarticle = artser.getallart();

            lookUpEdit1.Properties.ValueMember = "codeart";
            lookUpEdit1.Properties.DisplayMember = "titre";
            lookUpEdit1.Properties.DataSource = Allarticle;
            lookUpEdit1.Properties.PopulateColumns();
            lookUpEdit1.Properties.Columns["Rang"].Visible = false;
            lookUpEdit1.Properties.Columns["codeart"].Visible = false;
            lookUpEdit1.Properties.Columns["titre"].Caption = "titre";
            lookUpEdit1.Properties.Columns["dateedition"].Visible = false;
            lookUpEdit1.Properties.Columns["imprimerie"].Visible = false;
            lookUpEdit1.Properties.Columns["auteur"].Visible = false;
            lookUpEdit1.Properties.Columns["isbn"].Caption = "ISBN";
            lookUpEdit1.Properties.Columns["codeabarre"].Visible = false;
            lookUpEdit1.Properties.Columns["quantitenstock"].Visible = false;
            lookUpEdit1.Properties.Columns["prixachat"].Visible = false;
            lookUpEdit1.Properties.Columns["pvpublic"].Visible = false;
            lookUpEdit1.Properties.Columns["pvetudiant"].Visible = false;
            lookUpEdit1.Properties.Columns["pvfoire"].Visible = false;
            lookUpEdit1.Properties.Columns["pvdepositaire"].Visible = false;
            lookUpEdit1.Properties.Columns["image"].Visible = false;
            lookUpEdit1.Properties.Columns["emplacement"].Visible = false;



        }
        private void articles2()
        {
            lookUpEdit3.Properties.DataSource = null;
            List<Livre> Allarticle = new List<Livre>();
            Allarticle = artser.getallart();

            lookUpEdit3.Properties.ValueMember = "codeart";
            lookUpEdit3.Properties.DisplayMember = "titre";
            lookUpEdit3.Properties.DataSource = Allarticle;
            lookUpEdit3.Properties.PopulateColumns();
            lookUpEdit3.Properties.Columns["Rang"].Visible = false;
            lookUpEdit3.Properties.Columns["codeart"].Visible = false;
            lookUpEdit3.Properties.Columns["titre"].Caption = "titre";
            lookUpEdit3.Properties.Columns["dateedition"].Visible = false;
            lookUpEdit3.Properties.Columns["imprimerie"].Visible = false;
            lookUpEdit3.Properties.Columns["auteur"].Visible = false;
            lookUpEdit3.Properties.Columns["isbn"].Caption = "ISBN";
            lookUpEdit3.Properties.Columns["codeabarre"].Visible = false;
            lookUpEdit3.Properties.Columns["quantitenstock"].Visible = false;
            lookUpEdit3.Properties.Columns["prixachat"].Visible = false;
            lookUpEdit3.Properties.Columns["pvpublic"].Visible = false;
            lookUpEdit3.Properties.Columns["pvetudiant"].Visible = false;
            lookUpEdit3.Properties.Columns["pvfoire"].Visible = false;
            lookUpEdit3.Properties.Columns["pvdepositaire"].Visible = false;
            lookUpEdit3.Properties.Columns["image"].Visible = false;
            lookUpEdit3.Properties.Columns["emplacement"].Visible = false;

        }
        private void clients()
        {
            //get All article
            lookUpEdit2.Properties.DataSource = null;
            List<client> Allfrs = new List<client>();
            Allfrs = frser.listclts();

            lookUpEdit2.Properties.ValueMember = "numerocl";
            lookUpEdit2.Properties.DisplayMember = "raisonsoc";
            lookUpEdit2.Properties.DataSource = Allfrs;
            lookUpEdit2.Properties.PopulateColumns();
            lookUpEdit2.Properties.Columns[0].Visible = false;
            lookUpEdit2.Properties.Columns[1].Caption = "Code client";
            lookUpEdit2.Properties.Columns[2].Caption = "Raison sociale";
            lookUpEdit2.Properties.Columns[3].Visible = false;
            lookUpEdit2.Properties.Columns[4].Visible = false;
            lookUpEdit2.Properties.Columns[5].Visible = false;
            lookUpEdit2.Properties.Columns[6].Visible = false;
            lookUpEdit2.Properties.Columns[7].Visible = false;
            lookUpEdit2.Properties.Columns[8].Visible = false;
            lookUpEdit2.Properties.Columns[9].Visible = false;
            lookUpEdit2.Properties.Columns[10].Visible = false;
            lookUpEdit2.Properties.Columns[11].Visible = false;
            lookUpEdit2.Properties.Columns[11].Visible = false;




        }
        private void CumulEntree_Load(object sender, EventArgs e)
        {
            darticle = artser.getallart();
            dateEdit1.DateTime = System.DateTime.Today;
            dateEdit4.DateTime = System.DateTime.Today;
            articles();
            clients();
            fillgrid2(dateEdit4.DateTime);
            radioGroup1.SelectedIndex = 0;
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                Piece_bonret pccumul = new Piece_bonret();
                Livre art = (Livre)lookUpEdit1.GetSelectedDataRow();
                pccumul.Codearticle = art.codeart;
                pccumul.nomart = art.titre;

                pccumul.quantite = Convert.ToDouble(tqtit.Text.Replace('.', ','));
              
                piececumuls.Add(pccumul);
                fillgrid(piececumuls);
            }
            catch (Exception except)
            {

            }


        }

        private void fillgrid(List<Piece_bonret> piececumuls)
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = piececumuls;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Visible = false;
            gridView1.Columns[2].Caption = "Code article";
            gridView1.Columns[3].Caption = "Titre";
            gridView1.Columns[4].Caption = "Quantite";
            gridView1.Columns[5].Visible = false;


        }

        private void labelControl3_Click(object sender, EventArgs e)
        {

        }

        private void panelControl3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount == 0)
            {
                MessageBox.Show("Veuillez insérer au moins une ligne !!");
            }
            else
            {
                if (lookUpEdit2.EditValue==null)
                {
                    dxErrorProvider1.Dispose();
                    dxErrorProvider1.SetError(lookUpEdit2, "Champ obligatoire");
                }
                else
                {
                    DialogResult dialogResult = XtraMessageBox.Show("Veuillez valider le bon de retour ", "Attention", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.OK)
                    {

                        dxErrorProvider1.Dispose();
                        client fr = new client();
                        fr = (client)lookUpEdit2.GetSelectedDataRow();
                        BonRetour newbr = new BonRetour();
                        List<Piece_bonret> listpiece = new List<Piece_bonret>();
                        newbr.numerocumul = cmser.getlastbretour() + 1;
                        newbr.idfr = fr.codeclient;
                        newbr.nomfr = fr.raisonsoc;
                        newbr.date = dateEdit1.DateTime;
                        newbr.type = "don";
                        try
                        {
                            foreach (Piece_bonret pccuml in piececumuls)
                            {
                                pccuml.Codecumul = newbr.numerocumul;
                                pccuml.date = newbr.date;
                                listpiece.Add(pccuml);
                                string codearticle = pccuml.Codearticle;
                                Livre article = new Livre();
                                article = artser.getArticleByCode(codearticle);
                                article.quantitenstock =Convert.ToDouble( article.quantitenstock) +Convert.ToDouble( pccuml.quantite);
                              
                                artser.updateart(article);

                            }
                            cmser.addbonret(newbr, listpiece);
                            MessageBox.Show("Stock mis à jour avec succées");
                            //BonEntreeReport br = new BonEntreeReport();
                            //br.ShowPreview();

                            ClearAllForm(xtraTabPage2);
                            piececumuls.Clear();
                            gridControl1.DataSource = null;
                            gridView1.Columns.Clear();
                        }
                        catch
                        {

                        }
                    }
                }
               
            }
        }
        
        private void gridView1_Click(object sender, EventArgs e)
        {
            int count = gridView1.RowCount;
            if(count !=0)
            {
                Piece_bonret pccumulent = new Piece_bonret();
                pccumulent = (Piece_bonret)gridView1.GetRow(gridView1.FocusedRowHandle);
                lookUpEdit1.EditValue = pccumulent;
                tqtit.Text = pccumulent.quantite.ToString();
            }
           
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (lookUpEdit1.EditValue != null)
            {

                Livre artc = new Livre();
                artc = (Livre)lookUpEdit1.GetSelectedDataRow();
                Piece_bonret pc = piececumuls.Find(aa => aa.Codearticle == artc.codeart);
                piececumuls.Remove(pc);
                Piece_bonret pcnew = new Piece_bonret();
                pcnew.Codearticle = artc.codeart;
                pcnew.nomart = artc.titre;
                pcnew.quantite = Convert.ToDouble(tqtit.Text.Replace('.', ','));
             
                piececumuls.Add(pcnew);
                fillgrid(piececumuls);

            }
            else
            {
                MessageBox.Show("Veuillez séléctionner une ligne d'abord");
            }

        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            int count = gridView1.GetSelectedRows().Count();
            if (count != 0)
            {
                foreach (int i in gridView1.GetSelectedRows())
                {
                    Piece_bonret pccumul = (Piece_bonret)gridView1.GetRow(i);
                    piececumuls.Remove(pccumul);
                    fillgrid(piececumuls);
                }
            }
            else
            {
                MessageBox.Show("Veuillez séléctionner au moins une ligne");
            }
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = XtraMessageBox.Show("Sure de vouloir Annuler l'opération ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                ClearAllForm(xtraTabControl1.SelectedTabPage);

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

        public void fillgrid2(DateTime date)
        {


            gridControl2.DataSource = null;
            gridView2.Columns.Clear();
            gridControl2.DataSource = cmser.getallretourbymonth(date,"don");

            gridView2.Columns["numerocumul"].Caption = "Numero";
            gridView2.Columns["code"].Visible = false;
            gridView2.Columns["date"].Caption = "Date d'ajout";
            gridView2.Columns["idfr"].Visible = false;
            gridView2.Columns["nomfr"].Caption = "Fournisseur";
            gridView2.Columns["type"].Visible = false;

        }

        private void gridView2_Click(object sender, EventArgs e)
        {
            try
            {
                int count = 0;
                count = gridView2.GetSelectedRows().Count();
                if (count != 0)
                {
                    BonRetour cm = (BonRetour)gridView2.GetRow(gridView2.FocusedRowHandle);
                    fillgrid3((int)cm.numerocumul);
                }
            }
            catch (Exception exc)
            { }

        }

        private void fillgrid3(int numero)
        {
            gridControl3.DataSource = null;
            gridView3.Columns.Clear();
            gridControl3.DataSource = cmser.getallpiecebycodebr(numero);

            gridView3.Columns[0].Visible = false;
            gridView3.Columns[1].Caption = "Numero Bon retour";
            gridView3.Columns[2].Caption = "Code article";
            gridView3.Columns[3].Caption = "Nom article";
            gridView3.Columns[4].Caption = "Quantite";
          
            gridView3.Columns[5].Visible = false;
        }
        private void fillgrid4(List<Piece_bonret> lists)
        {
            gridControl4.DataSource = null;
            gridView4.Columns.Clear();
            gridControl4.DataSource = lists;

            gridView4.Columns[0].Visible = false;
            gridView4.Columns[1].Caption = "Numero Bon retour";
            gridView4.Columns[2].Caption = "Code article";
            gridView4.Columns[3].Caption = "Nom article";
            gridView4.Columns[4].Caption = "Quantite";
           
            gridView4.Columns[5].Visible = false;
        }
        private void dateEdit4_EditValueChanged(object sender, EventArgs e)
        {
            gridControl3.DataSource = null;
            gridView3.Columns.Clear();
            fillgrid2(dateEdit4.DateTime);
        }



        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == xtraTabPage3)
            {
                articles2();
            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPage1)
            {
                gridControl3.DataSource = null;
                gridView3.Columns.Clear();
                fillgrid2(dateEdit4.DateTime);
            }

        }

        private void lookUpEdit3_EditValueChanged(object sender, EventArgs e)
        {
            Livre art = new Livre();
            art = (Livre)lookUpEdit3.GetSelectedDataRow();
            List<Piece_bonret> pccums = new List<Piece_bonret>();
            pccums = cmser.getallretourbypiece(art.codeart);
            fillgrid4(pccums);
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            Livre art = new Livre();
            art =(Livre) lookUpEdit3.GetSelectedDataRow();
            List<Piece_bonret> pccumuls = new List<Piece_bonret>();
            pccumuls =cmser.getallretourcodeartbetweendate(art.codeart, dateEdit2.DateTime, dateEdit3.DateTime);
            fillgrid4(pccumuls);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            int count = gridView2.GetSelectedRows().Count();
            if (count != 0)
            {
                foreach (int i in gridView2.GetSelectedRows())
                {
                    BonRetour cmt = (BonRetour)gridView2.GetRow(i);
                   cmser.deleteretourbycode((int)cmt.numerocumul);
                    gridView2.DeleteRow(i);
                    gridControl3.DataSource = null;
                    gridView3.Columns.Clear();
                }
            }
            else
            {
                MessageBox.Show("Veuillez séléctionner au moins une ligne");
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage2;
        }

        private void groupControl2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }
        public void fillgrid2all()
        {


            gridControl2.DataSource = null;
            gridView2.Columns.Clear();
            gridControl2.DataSource = cmser.getallret();

            gridView2.Columns["numerocumul"].Caption = "Numero";
            gridView2.Columns["code"].Visible = false;
            gridView2.Columns["date"].Caption = "Date d'ajout";
            gridView2.Columns["idfr"].Visible = false;
            gridView2.Columns["nomfr"].Caption = "Client";
            gridView2.Columns["type"].Visible = false;

        }
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            gridControl3.DataSource = null;
            gridView3.Columns.Clear();
            fillgrid2all();
        }
        private void auteurs()
        {
            //get All article
            lookUpEdit2.Properties.DataSource = null;
            List<auteur> Allfrs = new List<auteur>();
            Allfrs = autser.getallauthor();

            lookUpEdit2.Properties.ValueMember = "numeroaut";
            lookUpEdit2.Properties.DisplayMember = "nom";
            lookUpEdit2.Properties.DataSource = Allfrs;
            lookUpEdit2.Properties.PopulateColumns();
            lookUpEdit2.Properties.Columns[0].Visible = false;
            lookUpEdit2.Properties.Columns[1].Caption = "Code auteur";
            lookUpEdit2.Properties.Columns[2].Caption = "Nom";
            lookUpEdit2.Properties.Columns[3].Caption = "Prénom";
            lookUpEdit2.Properties.Columns[4].Visible = false;
            lookUpEdit2.Properties.Columns[5].Visible = false;
            lookUpEdit2.Properties.Columns[6].Visible = false;
            lookUpEdit2.Properties.Columns[7].Visible = false;
            lookUpEdit2.Properties.Columns[8].Visible = false;
            lookUpEdit2.Properties.Columns[9].Visible = false;
            lookUpEdit2.Properties.Columns[10].Visible = false;
            lookUpEdit2.Properties.Columns[11].Visible = false;
            lookUpEdit2.Properties.Columns[12].Visible = false;
            lookUpEdit2.Properties.Columns[12].Visible = false;




        }

        private void radioGroup1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (radioGroup1.EditValue.ToString() == "Auteur")
            {
                labelControl10.BringToFront();
                lookUpEdit2.Properties.DataSource = null;
                lookUpEdit2.Properties.Columns.Clear();
                auteurs();
            }
            else
            {
                labelControl7.BringToFront();
                lookUpEdit2.Properties.DataSource = null;
                lookUpEdit2.Properties.Columns.Clear();
                clients();
            }
        }

        private void Cumulentreedon_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void tileItem3_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage3;
        }

        private void tileItem1_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage2;
        }

        private void tileItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage1;
        }
    }
}
