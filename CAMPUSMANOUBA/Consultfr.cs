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
    public partial class Consultfr : DevExpress.XtraEditors.XtraForm
    {
        public Consultfr()
        {
            InitializeComponent();
        }

        private static Consultfr instance;
        FrService frser = new FrService();
        public static Consultfr Instance()
        {
            if (instance == null)

                instance = new Consultfr();
            return instance;

        }
        private void AjoutFr_Load(object sender, EventArgs e)
        {
            List<fournisseur> frs = new List<fournisseur>();
            frs = frser.getallfr();
            fillgrid(frs);
        }

      

        private void Consultfr_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }
        private void fillgrid(List<fournisseur> frs)
        {

            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = frs;
            gridView1.Columns["codefr"].Visible = false;
            gridView1.Columns["numerofr"].Caption = "Numero";
            gridView1.Columns["raisonfr"].Caption = "Raison sociale";
            gridView1.Columns["responsable"].Caption = "Responsable";
            gridView1.Columns["mobile"].Caption = "Mobile";
            gridView1.Columns["tel"].Visible = false;
            gridView1.Columns["adress"].Visible = false;
            gridView1.Columns["ville"].Visible = false;
            gridView1.Columns["email"].Visible = false;
            gridView1.Columns["web"].Visible = false;
            gridView1.Columns["codepostal"].Visible = false;
            gridView1.Columns["image"].Caption = "Image";

           
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            List<fournisseur> frs = new List<fournisseur>();
            frs = frser.getallfr();
            fillgrid(frs);
            splashScreenManager1.CloseWaitForm();
        }

        private void Consultfr_Activated(object sender, EventArgs e)
        {
            List<fournisseur> frs = new List<fournisseur>();
            frs = frser.getallfr();
            fillgrid(frs);
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
                    frser.deletefr((fournisseur)gridView1.GetRow(gridView1.FocusedRowHandle));
                    List<fournisseur> frs = new List<fournisseur>();
                    frs = frser.getallfr();
                    fillgrid(frs);
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
                        fournisseur four = (fournisseur)gridView1.GetRow(i);

                        frser.deletefr((four));
                      
                    }
                    List<fournisseur> frs = new List<fournisseur>();
                    frs = frser.getallfr();
                    fillgrid(frs);
                    splashScreenManager1.CloseWaitForm();
                }
            }
            
          
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            Home.newfr = NewFr.Instance();
            Home.newfr.MdiParent = Home.ActiveForm;
            Home.newfr.Show();
            Home.newfr.BringToFront();
            splashScreenManager1.CloseWaitForm();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {

        }
    }


}