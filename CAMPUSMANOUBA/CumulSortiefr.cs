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
    public partial class CumulSortiefr : DevExpress.XtraEditors.XtraForm
    {
        public CumulSortiefr()
        {
            InitializeComponent();
        }
        Agentservice agser = Agentservice.Instance();
        private static CumulSortiefr instance;
        public static CumulSortiefr Instance()
        {
            if (instance == null)

                instance = new CumulSortiefr();
            return instance;

        }
        CumulSortieServices cmser = CumulSortieServices.Instance();
        FournitureService artser = FournitureService.Instance();
        FrService frser = FrService.Instance();
        public static List<Fourniture> darticle = new List<Fourniture>();

        public static List<Piece_cumulsortie> piececumuls = new List<Piece_cumulsortie>();

        private void articles()
        {
            //get All article
            lookUpEdit1.Properties.DataSource = null;
            lookUpEdit1.Properties.ValueMember = "codefour";
            lookUpEdit1.Properties.DisplayMember = "designation";
            lookUpEdit1.Properties.DataSource = darticle;
            lookUpEdit1.Properties.PopulateColumns();
            lookUpEdit1.Properties.Columns["idfour"].Visible = false;
            lookUpEdit1.Properties.Columns["codefour"].Caption = "Code article";
            lookUpEdit1.Properties.Columns["famille"].Visible = false;
            lookUpEdit1.Properties.Columns["idfamille"].Visible = false;
            lookUpEdit1.Properties.Columns["sousfamille"].Visible = false;
            lookUpEdit1.Properties.Columns["idsousfamille"].Visible = false;
            lookUpEdit1.Properties.Columns["designation"].Caption = "Designation";
            lookUpEdit1.Properties.Columns["fournisseur"].Visible = false;
            lookUpEdit1.Properties.Columns["quantitenstock"].Visible = false;
            lookUpEdit1.Properties.Columns["prixAchat"].Visible = false;
            lookUpEdit1.Properties.Columns["image"].Visible = false;
            lookUpEdit1.Properties.Columns["seuil"].Visible = false;
          

        }
        private void fillagent()
        {

            List<agent> agents = new List<agent>();
            agents = agser.getallagent();
            lookUpEdit2.Properties.ValueMember = "idagent";
            lookUpEdit2.Properties.DisplayMember = "nom";
            lookUpEdit2.Properties.DataSource = agents;
            lookUpEdit2.Properties.PopulateColumns();
            lookUpEdit2.Properties.Columns["idagent"].Visible = false;
            lookUpEdit2.Properties.Columns["cin"].Visible = false;
            lookUpEdit2.Properties.Columns["nom"].Caption = "Nom";
            lookUpEdit2.Properties.Columns["prenom"].Caption = "Prenom";
            lookUpEdit2.Properties.Columns["numtel"].Visible = false;
            lookUpEdit2.Properties.Columns["email"].Visible = false;
            lookUpEdit2.Properties.Columns["specialite"].Visible = false;
        }
        private void articles2()
        {
            //get All article
            lookUpEdit3.Properties.DataSource = null;
            lookUpEdit3.Properties.ValueMember = "codefour";
            lookUpEdit3.Properties.DisplayMember = "designation";
            lookUpEdit3.Properties.DataSource = darticle;
            lookUpEdit3.Properties.PopulateColumns();
            lookUpEdit3.Properties.Columns["idfour"].Visible = false;
            lookUpEdit3.Properties.Columns["codefour"].Caption = "Code article";
            lookUpEdit3.Properties.Columns["famille"].Visible = false;
            lookUpEdit3.Properties.Columns["idfamille"].Visible = false;
            lookUpEdit3.Properties.Columns["sousfamille"].Visible = false;
            lookUpEdit3.Properties.Columns["idsousfamille"].Visible = false;
            lookUpEdit3.Properties.Columns["designation"].Caption = "Designation";
            lookUpEdit3.Properties.Columns["fournisseur"].Visible = false;
            lookUpEdit3.Properties.Columns["quantitenstock"].Visible = false;
            lookUpEdit3.Properties.Columns["prixAchat"].Visible = false;
            lookUpEdit3.Properties.Columns["image"].Visible = false;
            lookUpEdit3.Properties.Columns["seuil"].Visible = false;

        }
    
        private void CumulEntree_Load(object sender, EventArgs e)
        {
            darticle = artser.getallart();
            dateEdit1.DateTime = System.DateTime.Today;
            dateEdit4.DateTime = System.DateTime.Today;
            articles();
            fillagent();
            
           
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                Piece_cumulsortie pccumul = new Piece_cumulsortie();
                Fourniture art = (Fourniture)lookUpEdit1.GetSelectedDataRow();
                pccumul.Codearticle = art.codefour;
                pccumul.nomart = art.designation;

                pccumul.quantite = Convert.ToDouble(tqtit.Text.Replace('.', ','));
                pccumul.puv= 0;
                piececumuls.Add(pccumul);
                fillgrid(piececumuls);
            }
            catch (Exception except)
            {

            }


        }

        private void fillgrid(List<Piece_cumulsortie> piececumuls)
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
            gridView1.Columns[6].Visible = false;

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
                if (lookUpEdit2.EditValue == null)
                {
                    dxErrorProvider1.Dispose();
                    dxErrorProvider1.SetError(lookUpEdit2, "Champ obligatoire");
                }
                else
                {
                    DialogResult dialogResult = XtraMessageBox.Show("Veuillez valider le bon de sortie", "Attention", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.OK)
                    {
                        agent ag = (agent)lookUpEdit2.GetSelectedDataRow();

                        dxErrorProvider1.Dispose();
                        Cumulsortie newcumul = new Cumulsortie();
                        List<Piece_cumulsortie> listpiece = new List<Piece_cumulsortie>();
                        newcumul.numerocumul = cmser.getlastcumul() + 1;

                        newcumul.nomfr = ag.nom;
                        newcumul.date = dateEdit1.DateTime;
                        newcumul.type = "fourniture";
                        try
                        {
                            foreach (Piece_cumulsortie pccuml in piececumuls)
                            {
                                pccuml.Codecumul = newcumul.numerocumul;
                                pccuml.date = newcumul.date;
                                listpiece.Add(pccuml);
                                string codearticle = pccuml.Codearticle;
                                Fourniture fr = new Fourniture();
                                fr = artser.getfourniturebycode(codearticle);
                                fr.quantitenstock = fr.quantitenstock - pccuml.quantite;

                                artser.updatefourniture(fr);
                            }
                            cmser.addcumul(newcumul, listpiece);
                            MessageBox.Show("Stock mis à jour");
                            //BonSortieReport br = new BonSortieReport();
                           // br.ShowPreview();
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
            Piece_cumulsortie pccumulent = new Piece_cumulsortie();
            pccumulent = (Piece_cumulsortie)gridView1.GetRow(gridView1.FocusedRowHandle);
            lookUpEdit1.EditValue = pccumulent;
            tqtit.Text = pccumulent.quantite.ToString();
           


        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            if (lookUpEdit1.EditValue != null)
            {

                Fourniture artc = new Fourniture();
                artc = (Fourniture)lookUpEdit1.GetSelectedDataRow();
                Piece_cumulsortie pc = piececumuls.Find(aa => aa.Codearticle == artc.codefour);
                piececumuls.Remove(pc);
                Piece_cumulsortie pcnew = new Piece_cumulsortie();
                pcnew.Codearticle = artc.codefour;
                pcnew.nomart = artc.designation;
                pcnew.quantite = Convert.ToDouble(tqtit.Text.Replace('.', ','));
                pcnew.puv = 0;
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
                    Piece_cumulsortie pccumul = (Piece_cumulsortie)gridView1.GetRow(i);
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
            gridControl2.DataSource = cmser.getallcumulbymonth(date,"fourniture");

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
                    Cumulsortie cm = (Cumulsortie)gridView2.GetRow(gridView2.FocusedRowHandle);
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
            gridView3.Columns[5].Visible = false;
            gridView3.Columns[6].Visible = false;
        }
        private void fillgrid4(List<Piece_cumulsortie> lists)
        {
            gridControl4.DataSource = null;
            gridView4.Columns.Clear();
            gridControl4.DataSource = lists;

            gridView4.Columns[0].Visible = false;
            gridView4.Columns[1].Caption = "Numero cumul";
            gridView4.Columns[2].Caption = "Code article";
            gridView4.Columns[3].Caption = "Nom article";
            gridView4.Columns[4].Caption = "Quantite";
            gridView4.Columns[5].Visible = false;
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
            Fourniture art = new Fourniture();
            art = (Fourniture)lookUpEdit3.GetSelectedDataRow();
            List<Piece_cumulsortie> pccums = new List<Piece_cumulsortie>();
            pccums = cmser.getallcumulbypiece(art.codefour);
            fillgrid4(pccums);
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            Fourniture art = new Fourniture();
            art =(Fourniture) lookUpEdit3.GetSelectedDataRow();
            List<Piece_cumulsortie> pccumuls = new List<Piece_cumulsortie>();
            pccumuls =cmser.getallcumulcodeartbetweendate(art.codefour, dateEdit2.DateTime, dateEdit3.DateTime);
            fillgrid4(pccumuls);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            int count = gridView2.GetSelectedRows().Count();
            if (count != 0)
            {
                foreach (int i in gridView2.GetSelectedRows())
                {
                    Cumulsortie cmt = (Cumulsortie)gridView2.GetRow(i);
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

        private void CumulSortiefr_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
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

        private void simpleButton10_Click(object sender, EventArgs e)
        {

        }
    }
}
