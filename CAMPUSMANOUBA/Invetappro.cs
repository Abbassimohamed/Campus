using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL;
using DAL;
using DevExpress.XtraGrid.Views.Grid;
namespace CAMPUSMANOUBA
{
    public partial class Invetappro : Form
    {
        ArtService artser = new ArtService();
        public Invetappro()
        {
            InitializeComponent();

          
        }
        private static Invetappro instance;

        public static Invetappro Instance()
        {
            if (instance == null)

                instance = new Invetappro();

            return instance;

        }
        public List<getartinfo_Result> getarticlesinfo()
        {
           return artser.getarticlesinfo();
        }
      public void fillgrid (List<getartinfo_Result> getarts)
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = getarts;
            gridView1.Columns[0].Caption = "Code";
            gridView1.Columns[1].Caption = "Titre";
            gridView1.Columns[2].Caption = "Qtité actuelle";
            gridView1.Columns[3].Caption = "Qtite réelle";
            gridView1.Columns[4].Caption = "Stock alerte";
         
        }

        private void Invetappro_Load(object sender, EventArgs e)
        {
            fillgrid(getarticlesinfo());
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string quantite = View.GetRowCellDisplayText(e.RowHandle, View.Columns["quantitenstock"]);
                if (Convert.ToDouble(quantite) < 30)
                {
                    e.Appearance.BackColor = Color.Salmon;
                    e.Appearance.BackColor2 = Color.SeaShell;
                }
            }
        }

        private void tileItem1_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage1;
        }

        private void tileItem2_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage2;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Livre liv = new Livre();
            getartinfo_Result art = (getartinfo_Result)gridView1.GetRow(gridView1.FocusedRowHandle);
            liv = artser.getArticleByCode(art.codeart);
            Correctionstock corrst = new Correctionstock(liv);

            corrst.ShowDialog();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void gridView1_RowClick(object sender, RowClickEventArgs e)
        {
            Livre liv = new Livre();
            getartinfo_Result art = (getartinfo_Result)gridView1.GetRow(gridView1.FocusedRowHandle);
            liv = artser.getArticleByCode(art.codeart);
            Correctionstock corrst = new Correctionstock(liv);
            corrst.ShowDialog();

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            fillgrid(getarticlesinfo());
        }
    }
}
