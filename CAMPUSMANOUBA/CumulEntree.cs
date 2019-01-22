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
    public partial class CumulEntree : DevExpress.XtraEditors.XtraForm
    {
        public CumulEntree()
        {
            InitializeComponent();
        }
        private static CumulEntree instance;
        public static CumulEntree Instance()
        {
            if (instance == null)

                instance = new CumulEntree();
            return instance;

        }
        CumulEntServices cmser = CumulEntServices.Instance();
        ArtService artser = ArtService.Instance();
        FrService frser = FrService.Instance();
        public static List<Livre> darticle = new List<Livre>();

        public static List<Piece_cumulent> piececumuls = new List<Piece_cumulent>();

        private void articles(List<Livre> allarticle)
        {
          

            
            //get All article
          lookUpEdit1.Properties.ValueMember = "codeart";
            lookUpEdit1.Properties.DisplayMember = "titre";
            lookUpEdit1.Properties.DataSource = allarticle;
            lookUpEdit1.Properties.PopulateColumns();
            lookUpEdit1.Properties.Columns["Rang"].Visible = false;
            lookUpEdit1.Properties.Columns["codeart"].Visible = false;
            lookUpEdit1.Properties.Columns["titre"]. Caption = "titre";
            lookUpEdit1.Properties.Columns["dateedition"].Visible = false;
            lookUpEdit1.Properties.Columns["imprimerie"].Visible = false;
            lookUpEdit1.Properties.Columns["auteur"].Visible = false;
            lookUpEdit1.Properties.Columns["isbn"].Caption = "ISBN";
            lookUpEdit1.Properties.Columns["codeabarre"]. Visible = false;
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
            //get All article
            lookUpEdit3.Properties.DataSource = null;


            lookUpEdit3.Properties.ValueMember = "codeart";
            lookUpEdit3.Properties.DisplayMember = "codeart";
            lookUpEdit3.Properties.DataSource = darticle;
            //lookUpEdit3.Properties.PopulateColumns();
            //lookUpEdit3.Properties.Columns["idarticle"].Visible = false;
            //lookUpEdit3.Properties.Columns["codeart"].Caption = "Code article";
            //lookUpEdit3.Properties.Columns["isbn"].Caption = "ISBN";
            //lookUpEdit3.Properties.Columns["famille"].Visible = false;
            //lookUpEdit3.Properties.Columns["idfamille"].Visible = false;
            //lookUpEdit3.Properties.Columns["sousfamille"].Visible = false;
            //lookUpEdit3.Properties.Columns["idsousfamille"].Visible = false;
            //lookUpEdit3.Properties.Columns["titre"].Caption = "titre";
            //lookUpEdit3.Properties.Columns["dateedition"].Visible = false;
            //lookUpEdit3.Properties.Columns["imprimerie"].Visible = false;
            //lookUpEdit3.Properties.Columns["idimprim"].Visible = false;
            //lookUpEdit3.Properties.Columns["auteur"].Visible = false;
            //lookUpEdit3.Properties.Columns["idauteur"].Visible = false;
            //lookUpEdit3.Properties.Columns["quantitenstock"].Visible = false;
            //lookUpEdit3.Properties.Columns["depotlegal"].Visible = false;
            //lookUpEdit3.Properties.Columns["pvpublic"].Visible = false;
            //lookUpEdit3.Properties.Columns["pvpromo"].Visible = false;
            //lookUpEdit3.Properties.Columns["droitaut"].Visible = false;
            //lookUpEdit3.Properties.Columns["abscice"].Visible = false;
            //lookUpEdit3.Properties.Columns["ordonne"].Visible = false;
            //lookUpEdit3.Properties.Columns["image"].Visible = false;


        }
        private void Fournisseurs()
        {
            //get All article
            lookUpEdit2.Properties.DataSource = null;
            List<fournisseur> Allfrs = new List<fournisseur>();
            Allfrs = frser.getallfr();

            lookUpEdit2.Properties.ValueMember = "numerofr";
            lookUpEdit2.Properties.DisplayMember = "raisonfr";
            lookUpEdit2.Properties.DataSource = Allfrs;
            lookUpEdit2.Properties.PopulateColumns();
            lookUpEdit2.Properties.Columns[0].Visible = false;
            lookUpEdit2.Properties.Columns[1].Caption = "Code fournisseur";
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




        }
        private void CumulEntree_Load(object sender, EventArgs e)
        {
            darticle = artser.getallart();
            dateEdit1.DateTime = System.DateTime.Today;
            dateEdit4.DateTime = System.DateTime.Today;
            articles(darticle);
            Fournisseurs();
            fillgrid2(dateEdit4.DateTime);
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                Piece_cumulent pccumul = new Piece_cumulent();
                Livre art = (Livre)lookUpEdit1.GetSelectedDataRow();
                pccumul.Codearticle = art.codeart;
                pccumul.nomart = art.titre;
                pccumul.date = Convert.ToDateTime(dateEdit1.Text);
                pccumul.quantite = Convert.ToDouble(tqtit.Text.Replace('.', ','));
                pccumul.pua = Convert.ToDouble(tpu.Text.Replace('.', ','));
                piececumuls.Add(pccumul);
                fillgrid(piececumuls);
            }
            catch (Exception except)
            {

            }


        }

        private void fillgrid(List<Piece_cumulent> piececumuls)
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = piececumuls;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Visible = false;
            gridView1.Columns[2].Caption = "Code article";
            gridView1.Columns[3].Caption = "Titre";
            gridView1.Columns[4].Caption = "Quantite";
            gridView1.Columns[5].Caption = "Prix unitaire";

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
                    DialogResult dialogResult = XtraMessageBox.Show("Veuillez confirmer l'opération !!", "Attention", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.OK)
                    {

                        dxErrorProvider1.Dispose();
                        fournisseur fr = new fournisseur();
                        fr = (fournisseur)lookUpEdit2.GetSelectedDataRow();
                        CumulEnt newcumul = new CumulEnt();
                        List<Piece_cumulent> listpiece = new List<Piece_cumulent>();
                        newcumul.numerocumul = cmser.getlastcumul() + 1;
                        newcumul.idfr = fr.codefr;
                        newcumul.nomfr = fr.raisonfr;
                        newcumul.date = dateEdit1.DateTime;
                        newcumul.type = "article";
                        try
                        {
                            foreach (Piece_cumulent pccuml in piececumuls)
                            {
                                pccuml.Codecumul = newcumul.numerocumul;
                                pccuml.date = newcumul.date;
                                listpiece.Add(pccuml);
                                string codearticle = pccuml.Codearticle;
                                Livre article = new Livre();
                                article = artser.getArticleByCode(codearticle);
                                article.quantitenstock = article.quantitenstock + pccuml.quantite;
                            //article.quantitedisponible =Convert.ToDouble( article.quantitenstock) +Convert.ToDouble( pccuml.quantite);
                              
                              artser.updateart(article);
                            }
                            cmser.addcumul(newcumul, listpiece);
                            MessageBox.Show("Stock mis à jour");
                            //BonEntreeReport br = new BonEntreeReport();
                            //br.ShowPreview();
                            ClearAllForm(xtraTabPage2);
                            piececumuls.Clear();
                            gridControl1.DataSource = null;
                            gridView1.Columns.Clear();
                            dateEdit1.EditValue=System.DateTime.Today;
                    }
                        catch(Exception except)
                    {

                    }
                }
                }
               
            }
        }
        
        private void gridView1_Click(object sender, EventArgs e)
        {
            int count = gridView1.RowCount;
            if(count !=0 )
            {
                Piece_cumulent pccumulent = new Piece_cumulent();
                pccumulent = (Piece_cumulent)gridView1.GetRow(gridView1.FocusedRowHandle);
                lookUpEdit1.EditValue = pccumulent;
                tqtit.Text = pccumulent.quantite.ToString();
                tpu.Text = pccumulent.pua.ToString();

            }


        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (lookUpEdit1.EditValue != null)
            {

                Livre artc = new Livre();
                artc = (Livre)lookUpEdit1.GetSelectedDataRow();
                Piece_cumulent pc = piececumuls.Find(aa => aa.Codearticle == artc.codeart);
                piececumuls.Remove(pc);
                Piece_cumulent pcnew = new Piece_cumulent();
                pcnew.Codearticle = artc.codeart;
                pcnew.nomart = artc.titre;
                pcnew.quantite = Convert.ToDouble(tqtit.Text.Replace('.', ','));
                pcnew.pua = Convert.ToDouble(tpu.Text.Replace('.', ','));
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
                    Piece_cumulent pccumul = (Piece_cumulent)gridView1.GetRow(i);
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
            gridControl2.DataSource = cmser.getallcumulbymonth(date,"article");

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
                    CumulEnt cm = (CumulEnt)gridView2.GetRow(gridView2.FocusedRowHandle);
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
            gridControl3.DataSource = cmser.getallpiecebycodecumul(numero);

            gridView3.Columns[0].Visible = false;
            gridView3.Columns[1].Caption = "Numero cumul";
            gridView3.Columns[2].Caption = "Code article";
            gridView3.Columns[3].Caption = "Nom article";
            gridView3.Columns[4].Caption = "Quantite";
            gridView3.Columns[5].Caption = "Prix Unitaire";
            gridView3.Columns[6].Visible = false;
        }
        private void fillgrid4(List<Piece_cumulent> lists)
        {
            gridControl4.DataSource = null;
            gridView4.Columns.Clear();
            gridControl4.DataSource = lists;

            gridView4.Columns[0].Visible = false;
            gridView4.Columns[1].Caption = "Numero cumul";
            gridView4.Columns[2].Caption = "Code article";
            gridView4.Columns[3].Caption = "Nom article";
            gridView4.Columns[4].Caption = "Quantite";
            gridView4.Columns[5].Caption = "Prix Unitaire";
            gridView4.Columns[6].Visible = false;
        }
        private void dateEdit4_EditValueChanged(object sender, EventArgs e)
        {
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
                fillgrid2(dateEdit4.DateTime);
            }

        }

        private void lookUpEdit3_EditValueChanged(object sender, EventArgs e)
        {
            Livre art = new Livre();
            art = (Livre)lookUpEdit3.GetSelectedDataRow();
            List<Piece_cumulent> pccums = new List<Piece_cumulent>();
            pccums = cmser.getallcumulbypiece(art.codeart);
            fillgrid4(pccums);
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            try
            {
                Livre art = new Livre();
                art = (Livre)lookUpEdit3.GetSelectedDataRow();
                List<Piece_cumulent> pccumuls = new List<Piece_cumulent>();
                pccumuls = cmser.getallcumulcodeartbetweendate(art.codeart, dateEdit2.DateTime, dateEdit3.DateTime);
                fillgrid4(pccumuls);
            }
         catch(Exception excep)
            {  }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            int count = gridView2.GetSelectedRows().Count();
            if (count != 0)
            {
                foreach (int i in gridView2.GetSelectedRows())
                {
                    CumulEnt cmt = (CumulEnt)gridView2.GetRow(i);
                   cmser.deletecumulbycode((int)cmt.numerocumul);
                    gridView2.DeleteRow(i);
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

        private void CumulEntree_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void simpleButton1_Click_1(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage2;
        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {
            int count = gridView2.GetSelectedRows().Count();
            if (count != 0)
            {
                foreach (int i in gridView2.GetSelectedRows())
                {
                    CumulEnt cmt = (CumulEnt)gridView2.GetRow(i);
                    cmser.deletecumulbycode((int)cmt.numerocumul);
                    gridView2.DeleteRow(i);
                }
            }
            else
            {
                MessageBox.Show("Veuillez séléctionner au moins une ligne");
            }
        }

        private void dateEdit4_EditValueChanged_1(object sender, EventArgs e)
        {
              fillgrid2(dateEdit4.DateTime);
        }

        private void tileItem1_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage2;
        }

        private void tileItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage1;
        }

        private void tileItem3_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage3;
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {
            Livre s = (Livre)lookUpEdit1.GetSelectedDataRow();
            if (s != null)
            {
                textEdit3.Text = s.isbn;
                tpu.Text = s.pvpublic.ToString();
                simpleButton4.Enabled = true;
                simpleButton5.Enabled = false;
                simpleButton6.Enabled = false;
               
            }
        }

       
    }
}
