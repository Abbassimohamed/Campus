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
    public partial class listecmds : DevExpress.XtraEditors.XtraForm
    {
        BCService DBL = new BCService();
        ArtService DS = new ArtService();
        private static listecmds instance;
        public static int id_factt, id_fact, id_clt, displaybl, idcmd, idfcmd, idcmande, idbl;
        public static int id_bl;
        public static DataTable data;
        public Double tottc, totalfactureht, totalfacturettc;
        public static string etat_commande, client, num_bl, num_facture;

        public listecmds()
        {
            InitializeComponent();
            Liste_cmd_clt();

        }

        public static listecmds Instance()
        {
            if (instance == null)

                instance = new listecmds();

            return instance;

        }


        private void Liste_cmd_clt()
        {
            tottc = 0;
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = DBL.getAllBc();
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[1].Caption = "numéro BC";
            this.gridView1.Columns[2].Caption = "Date Ajout";
            this.gridView1.Columns[3].Caption = "etat";
            this.gridView1.Columns[4].Caption = "montant";
            this.gridView1.Columns[5].Visible = false;
            this.gridView1.Columns[6].Caption = "client"; ;

            gridView1.OptionsView.ShowAutoFilterRow = true;
         

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            Home.newBC= UpdateCmd.Instance();
            Home.newBC.MdiParent = Home.ActiveForm;
            Home.newBC.Show();
            Home.newBC.BringToFront();
            splashScreenManager1.CloseWaitForm();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {

            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            bon_commande bc = (bon_commande)gridView1.GetRow(gridView1.FocusedRowHandle);
            displaybl = Convert.ToInt32(bc.numero_bc.ToString());
            //////////////idcmd = Convert.ToInt32(row[11].ToString());
            ////////////////////////////////////////////BLReport report = new BLReport(displaybl);
            ////////////////////////////////////////////report.ShowPreview();
            modifierCommande passercmd = new modifierCommande(bc);
            passercmd.Show();



        }
     
        private void simpleButton2_Click(object sender, EventArgs e)
        {

            int count = gridView1.DataRowCount;
            DialogResult dialogResult = XtraMessageBox.Show("Sure de vouloir supprimer le BC ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    foreach (int i in gridView1.GetSelectedRows())
                    {

                        bon_commande row = (bon_commande)gridView1.GetRow(i);
                        int code = Convert.ToInt32(row.numero_bc);
                        List<ligne_bc> lbl = new List<ligne_bc>();
                        lbl = DBL.getLbcByCodeBC(code);
                        DBL.supprimerBc(row);
                        DBL.supprimerLbc(lbl);

                    }
                    Liste_cmd_clt();
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Liste_cmd_clt();
        }



        private void listebls_Load(object sender, EventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }


        private void listebls_Activated(object sender, EventArgs e)
        {
            Liste_cmd_clt();
        }
        private void listebls_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void listebls_MouseEnter(object sender, EventArgs e)
        {
            Liste_cmd_clt();
        }
        private void listebls_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            Liste_cmd_clt();

        }

        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button == System.Windows.Forms.MouseButtons.Right)
                {

                    int count = gridView1.DataRowCount;
                    if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                    {
                        System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                        id_bl = Convert.ToInt32(row[0]);
                        Point pt = this.Location;
                        pt.Offset(this.Left + e.X, this.Top + e.Y);
                        popupMenu1.ShowPopup(this.barManager1, Control.MousePosition);
                    }
                }
            }
            catch (Exception ce)
            {
            }
        }


    }
}