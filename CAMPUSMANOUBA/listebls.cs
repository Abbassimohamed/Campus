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
using DevExpress.XtraBars;
using BLL;
using DAL;

namespace CAMPUSMANOUBA
{
    public partial class listebls : DevExpress.XtraEditors.XtraForm
    {
        BLService DBL = new BLService();
        ArtService DS = new ArtService();
        private static listebls instance;
        public static int id_factt, id_fact, id_clt, displaybl, idcmd, idfcmd, idcmande, idbl;
        public static int id_bl;
        public static DataTable data;
        public Double tottc, totalfactureht, totalfacturettc;
        public static string etat_commande, client, num_bl, num_facture;

        public listebls()
        {
            InitializeComponent();
            Liste_bl_clt();

        }

        public static listebls Instance()
        {
            if (instance == null)

                instance = new listebls();

            return instance;

        }


        private void Liste_bl_clt()
        {
            tottc = 0;
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = DBL.getAllBL();
            this.gridView1.Columns[0].Visible = false;
            this.gridView1.Columns[1].Caption = "numéro BL";
            this.gridView1.Columns[2].Caption = "Date Ajout";
            this.gridView1.Columns[3].Caption = "Etat";
            this.gridView1.Columns[4].Caption = "montant";
            this.gridView1.Columns[5].Visible = false;
            this.gridView1.Columns[6].Caption = "client"; ;
            this.gridView1.Columns[7].Visible = false;
            this.gridView1.Columns[8].Visible = false;
            this.gridView1.Columns[9].Visible = false;
            this.gridView1.Columns[10].Visible = false;
            this.gridView1.Columns[11].Visible = false;
            this.gridView1.Columns[12].Visible = false;
            gridView1.OptionsView.ShowAutoFilterRow = true;
            updatesum();

        }




        private void simpleButton4_Click(object sender, EventArgs e)
        {
            //Tnumfact.Visible = true;
            //simpleButton5.Visible = true;
            //labelControl1.Visible = true;
            //labelControl2.Visible=true;
            //dateEdit1.Visible=true;
            //labelControl3.Visible = true;
            //textEdit1.Visible = true;
            //string num_fact = NumFact().ToString();
            //num_facture = DateTime.Now.Year.ToString().Substring(2, 2) + " /" + num_fact;
            //Tnumfact.Text = num_fact;
            //DateTime.Now.Year.ToString().Substring(2,2)+" /"+ 
            List<int> LidBl = new List<int>();
            foreach (int i in gridView1.GetSelectedRows())
            {

                //DataRow row = gridView1.GetDataRow(i);
                bon_livraison BL = (bon_livraison)gridView1.GetRow(i);
                if (BL.etat == "validée")
                {
                    LidBl.Add(Convert.ToInt32(BL.numero_bl.ToString()));
                }
                else if (BL.etat == "en cours")
                {
                    XtraMessageBox.Show("BL " + BL.numero_bl + " non validée");
                }
                else
                    XtraMessageBox.Show("BL " + BL.numero_bl + " déja Facturée");
            }
            if (LidBl.Count != 0)
            {

                FactureVente factvent = new FactureVente(LidBl);
                factvent.ShowDialog();

            }

        }

        //

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {

            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            bon_livraison bl = (bon_livraison)gridView1.GetRow(gridView1.FocusedRowHandle);
            displaybl = Convert.ToInt32(bl.numero_bl.ToString());
            //////////////idcmd = Convert.ToInt32(row[11].ToString());
            ////////////////////////////////////////////BLReport report = new BLReport(displaybl);
            ////////////////////////////////////////////report.ShowPreview();

            modifierBL modifierb = new modifierBL(bl);
            modifierb.Show();




        }
        public void updatesum()
        {
            tottc = 0;

            for (int i = 0; i < gridView1.RowCount; i++)
            {

                //DataRow row1 = gridView1.GetDataRow(i);
                bon_livraison bl = new bon_livraison();
                bl = (bon_livraison)gridView1.GetRow(i);
                tottc += Convert.ToDouble(bl.montant_BL.ToString().Replace('.', ','));

            }


            textBox1.Text = tottc.ToString();

        }
        //private int get_maxfact()
        //{
        //    int y = 0;
        //    int x = 0;
        //    DataTable dt = new DataTable();
        //    DataTable dtbl = new DataTable();
        //    DataTable data = new DataTable();
        //    dt = fun.getcountcmd("facturevente");

        //    if (dt.Rows.Count == 0)
        //    {
        //        data = fun.getcurrentvalue("facturevente");

        //        if (Convert.ToInt32(data.Rows[0][0]) == 0)
        //        {
        //            fun.resetautoincrement("facturevente", 0);
        //            y = Convert.ToInt32(data.Rows[0][0]);
        //        }
        //        else
        //        {
        //            fun.resetautoincrement("facturevente", 0);
        //            data = fun.getcurrentvalue("facturevente");
        //            y = Convert.ToInt32(data.Rows[0][0]);
        //        }
        //    }
        //    else
        //    {
        //        dtbl = fun.get_max_Factvente();
        //        x = Convert.ToInt32(dtbl.Rows[0][0]);
        //        fun.resetautoincrement("facturevente", x);
        //        data = fun.getcurrentvalue("facturevente");
        //        y = Convert.ToInt32(data.Rows[0][0]);

        //    }

        //    return y;

        //}
        private void simpleButton2_Click(object sender, EventArgs e)
        {
            /////////sql_gmao dd = new sql_gmao();
            int count = gridView1.DataRowCount;
            DialogResult dialogResult = XtraMessageBox.Show("Sure de vouloir supprimer le BL ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    foreach (int i in gridView1.GetSelectedRows())
                    {
                        //System.Data.DataRow row = gridView1.GetDataRow(i);
                        bon_livraison row = (bon_livraison)gridView1.GetRow(i);
                        if (row.etat != "validée")
                        {
                            int code = Convert.ToInt32(row.numero_bl);
                            List<ligne_bl> lbl = new List<ligne_bl>();
                            lbl = DBL.getLblByCodeBL(code);
                            DBL.supprimerBL(row);//supprimer les pieces de commande fournisseur
                            DBL.supprimerLbl(lbl);
                        }
                        else
                        {
                            MessageBox.Show("BL numéro " + row.numero_bl + " est validée, elle ne sera pas supprimée");
                        }

                    }
                    Liste_bl_clt();
                }
            }
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            gridControl1.ShowRibbonPrintPreview();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Liste_bl_clt();
        }

        private void barButtonItem3_ItemClick_1(object sender, ItemClickEventArgs e)
        {
            upadetall();
        }


        private void upadetall()
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                bon_livraison row = (bon_livraison)gridView1.GetRow(gridView1.FocusedRowHandle);
                string etat = row.etat;
                if (etat == "validée")
                {
                    XtraMessageBox.Show("Ce Bl est déja validée");
                }
                else
                {
                    id_bl = Convert.ToInt32(row.numero_bl);
                    bon_livraison bl = DBL.GetBLBynum(id_bl);
                    if (bl != null)
                    {

                        List<ligne_bl> lbl = DBL.getLblByCodeBL(Convert.ToInt32(bl.numero_bl));
                        foreach (ligne_bl rw in lbl)
                        {
                            Livre s = DS.getArticleByCode(rw.code_art.ToString());
                            double q = Convert.ToDouble(s.quantitenstock) - Convert.ToDouble(rw.quantite_article.ToString());
                            s.quantitenstock = q;
                            DS.updateart(s);


                        }
                        bl.etat = "validée";
                        DBL.modifier(bl);

                        Liste_bl_clt();
                    }







                }
            }
        }

        private void gridControl1_Click_1(object sender, EventArgs e)
        {

        }

        //public int NumFact()
        //{

        //    int y = 0;
        //    int x = 0;
        //    DataTable dt = new DataTable();
        //    DataTable dr = new DataTable();
        //    dr = fun.ListFact();
        //    if (dr.Rows.Count == 0)
        //    {
        //        return 1;
        //    }

        //    dt = fun.max_num_Factvent();
        //    if (dt.Rows.Count == 0)
        //    {
        //        return 1;
        //    }
        //    string max = dt.Rows[0]["max"].ToString();
        //    if (dt.Rows[0]["max"].ToString() == "")
        //    {

        //        y = 1;

        //    }
        //    else
        //    {
        //        y = int.Parse(dt.Rows[0]["max"].ToString()) + 1;

        //    }

        //    return y;
        //}



        private void listebls_Load(object sender, EventArgs e)
        {

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void barButtonItem2_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
        private void listebls_Activated(object sender, EventArgs e)
        {
            Liste_bl_clt();
        }
        private void listebls_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void listebls_MouseEnter(object sender, EventArgs e)
        {
            Liste_bl_clt();
        }
        private void listebls_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }


        private void gridControl1_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {

                int count = gridView1.DataRowCount;
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    bon_livraison row = (bon_livraison)gridView1.GetRow(gridView1.FocusedRowHandle);
                    id_bl = Convert.ToInt32(row.id);
                    Point pt = this.Location;
                    pt.Offset(this.Left + e.X, this.Top + e.Y);
                    popupMenu1.ShowPopup(this.barManager1, Control.MousePosition);
                }
            }

        }

        private void barButtonItem3_ItemClick(object sender, ItemClickEventArgs e)
        {
            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                System.Data.DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
                id_bl = Convert.ToInt32(row[0]);
                string etat = row[3].ToString();
                if (etat == "validée")
                {
                    XtraMessageBox.Show("Ce Bl est déja validée");
                }
                else
                {
                    //////////////////////////////////////////////////////fun.update_etat_bonlivraison("validée", id_bl);
                    //////////////////////////////////////////////////////DataTable dtt = fun.get_Allprodbybl(id_bl.ToString());
                    //////////////////////////////////////////////////////foreach (DataRow rw in dtt.Rows)
                    //////////////////////////////////////////////////////{
                    //////////////////////////////////////////////////////    fun.update_sousstock_after_accept2(double.Parse(rw[3].ToString().Replace('.', ',')), rw[1].ToString());

                    //////////////////////////////////////////////////////}
                }
                Liste_bl_clt();

            }
        }


    }
}
