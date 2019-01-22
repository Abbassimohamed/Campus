using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DAL;
using BLL;
namespace CAMPUSMANOUBA
{
    public partial class Consultclt : DevExpress.XtraEditors.XtraForm
    {
        public Consultclt()
        {
            InitializeComponent();
        }

        private static Consultclt instance;
        ClientService clser = ClientService.Instance();
        public static Consultclt Instance()
        {
            if (instance == null)

                instance = new Consultclt();
            return instance;

        }
        private void AjoutFr_Load(object sender, EventArgs e)
        {
            List<client> clts = new List<client>();
            clts = clser.listclts();
          
            fillgrid(clts);
        }

      

        private void Consultfr_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }
        private void fillgrid(List<client> clts)
        {

            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = clts;
            //gridView1.Columns["codeclient"].Visible = false;
            //gridView1.Columns["numerocl"].Caption = "Numero";
            //gridView1.Columns["raisonsoc"].Caption = "Raison sociale";
            //gridView1.Columns["resp"].Caption = "Responsable";
            //gridView1.Columns["qualite"].Caption = "Mobile";
            //gridView1.Columns["tel"].Visible = false;
            //gridView1.Columns["mobile"].Visible = false;
            //gridView1.Columns["adresse"].Visible = false;
            //gridView1.Columns["codepostal"].Visible = false;
            //gridView1.Columns["ville"].Visible = false;
            //gridView1.Columns["web"].Visible = false;
            //gridView1.Columns["email"].Visible = false;
            //gridView1.Columns["fax"].Caption = "Fax";


        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            List<client> clts = new List<client>();
            clts = clser.listclts();
            fillgrid(clts);
            splashScreenManager1.CloseWaitForm();
        }

        private void Consultfr_Activated(object sender, EventArgs e)
        {
            List<client> clts = new List<client>();
            clts = clser.listclts();
            fillgrid(clts);

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

            int count = gridView1.GetSelectedRows().Count();
            if (count == 0)
            {
                MessageBox.Show("Veuillez sélectionner un ou plusieurs element");
            }
            else if (count == 1)
            {
                if (MessageBox.Show("Vous etes sure de supprimer cet élément ??", "Suppression des fournisseurs", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    splashScreenManager1.ShowWaitForm();
                    clser.deleteclt((client)gridView1.GetRow(gridView1.FocusedRowHandle));
                    List<client> clts = new List<client>();
                    clts = clser.listclts();
                    fillgrid(clts);
                    splashScreenManager1.CloseWaitForm();
                }
            }
          
            else
            {

                if (MessageBox.Show("Vous etes sure de supprimer toute la selection ??", "Suppression des fournisseurs", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    splashScreenManager1.ShowWaitForm();
                    foreach (int i in gridView1.GetSelectedRows())
                    {
                        client cl = (client)gridView1.GetRow(i);

                        clser.deleteclt((cl));
                      
                    }
                    List<client> clts = new List<client>();
                    clts = clser.listclts();
                    fillgrid(clts);
                    splashScreenManager1.CloseWaitForm();
                }
            }
            
          
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            Home.newcl = NewClient.Instance();
            Home.newcl.MdiParent = Home.ActiveForm;
            Home.newcl.Show();
            Home.newcl.BringToFront();
            splashScreenManager1.CloseWaitForm();
        }
    }


}