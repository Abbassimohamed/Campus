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
    public partial class CumulSort : DevExpress.XtraEditors.XtraForm
    {
        public CumulSort()
        {
            InitializeComponent();
        }
        private static CumulSort instance;
        public static CumulSort Instance()
        {
            if (instance == null)

                instance = new CumulSort();
            return instance;

        }
        CumulSortieServices cmser = CumulSortieServices.Instance();
        ArtService artser = ArtService.Instance();
        FrService frser = FrService.Instance();
        public static List<Livre> darticle = new List<Livre>();

        public static List<Piece_cumulsortie> piececumuls = new List<Piece_cumulsortie>();

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
        private void CumulEntree_Load(object sender, EventArgs e)
        {
            darticle = artser.getallart();
            
            dateEdit4.DateTime = System.DateTime.Today;
           
           
            fillgrid2(dateEdit4.DateTime);
        }

      

        private void labelControl3_Click(object sender, EventArgs e)
        {

        }

        private void panelControl3_Paint(object sender, PaintEventArgs e)
        {

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
            gridView3.Columns[5].Caption = "Prix Unitaire";
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

        }

        private void lookUpEdit3_EditValueChanged(object sender, EventArgs e)
        {
            Livre art = new Livre();
            art = (Livre)lookUpEdit3.GetSelectedDataRow();
            List<Piece_cumulsortie> pccums = new List<Piece_cumulsortie>();
            pccums = cmser.getallcumulbypiece(art.codeart);
            fillgrid4(pccums);
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            Livre art = new Livre();
            art =(Livre) lookUpEdit3.GetSelectedDataRow();
            List<Piece_cumulsortie> pccumuls = new List<Piece_cumulsortie>();
            pccumuls =cmser.getallcumulcodeartbetweendate(art.codeart, dateEdit2.DateTime, dateEdit3.DateTime);
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

        private void CumulSort_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }
    }
}
