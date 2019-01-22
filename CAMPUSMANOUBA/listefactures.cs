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
using DAL;
using BLL;

namespace CAMPUSMANOUBA
{
    public partial class listefactures : DevExpress.XtraEditors.XtraForm
    {
        BLService DB = new BLService();
        private static listefactures instance;
        public listefactures()
        {
            InitializeComponent();
            Liste_cde_clt();
        }
        public static listefactures Instance()
        {
            if (instance == null)

                instance = new listefactures();

            return instance;

        }
        //sql_gmao fun = new sql_gmao();
        public static int id_fact, id_clt, idfactt;
        public static int id_bl;
        public static Double tottc = 0;
        public static string etat_commande, id_factRass;
        private void liste_cde_client_Activated(object sender, EventArgs e)
        {

            Liste_cde_clt();

            gridView1.OptionsView.ShowAutoFilterRow = true;
            gridView1.BestFitColumns();
            gridView1.OptionsBehavior.Editable = false;
            gridView1.OptionsView.EnableAppearanceEvenRow = true;
        }

        private void Liste_cde_clt()
        {

            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = DB.getAllFacture();
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[1].Visible = false;
            this.gridView1.Columns[2].Caption = "Numero facture";
            this.gridView1.Columns[3].Caption = "Etat Facture";
            this.gridView1.Columns[4].Caption = "Nom Client";
            this.gridView1.Columns[5].Visible = false;
            this.gridView1.Columns[6].Caption = "Montant Total";
            this.gridView1.Columns[7].Visible = false;
            this.gridView1.Columns[8].Visible = false;
            this.gridView1.Columns[9].Visible = false;
            this.gridView1.Columns[10].Visible = false;
            this.gridView1.Columns[11].Visible = false;
            this.gridView1.Columns[12].Caption = "Code Client";




        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            int count = gridView1.DataRowCount;
            DialogResult dialogResult = XtraMessageBox.Show("Sure de vouloir supprimer la facture ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    foreach (int i in gridView1.GetSelectedRows())
                    {
                        facturevente row = (facturevente)gridView1.GetRow(i);
                        //int code = Convert.ToInt32(row[0]);

                        string b = "";
                        string a = "ajoute";
                        //sql_gmao dd = new sql_gmao();




                        List<piece_fact> lpf = new List<piece_fact>();
                        lpf = DB.getLFByCodeFact(int.Parse(row.numero_fact.ToString()));
                        DB.supprimerLF(lpf);
                        DB.supprimerFact(row);
                    }
                    Liste_cde_clt();
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Liste_cde_clt();
        }

        private void liste_cde_client_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void listefactures_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }
        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                facturevente row = (facturevente)gridView1.GetRow(gridView1.FocusedRowHandle);
                //idfactt = Convert.ToInt32(row[22].ToString());
                id_fact = Convert.ToInt32(row.numero_fact.ToString());
                ///////////////facturing fc = new facturing();
                ////////////////////fc.ShowDialog();
            }
            catch (Exception exception)
            {
            }
        }





    }
}