using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;

using DAL;
using BLL;
using System.Data.Entity;
namespace CAMPUSMANOUBA
{
    public partial class Consutaut : DevExpress.XtraEditors.XtraForm
    {
        public Consutaut()
        {
            InitializeComponent();
        
  
        }

        private static Consutaut instance;
        AutService autser = new AutService();
        public static Consutaut Instance()
        {
            if (instance == null)

                instance = new Consutaut();
            return instance;

        }
        private void AjoutFr_Load(object sender, EventArgs e)
        {

            List<auteur> auteurs = new List<auteur>();
            auteurs = autser.getallauthor();
            fillgrid(auteurs);
         
        }

      

        private void Consultfr_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }
        private void fillgrid(List<auteur> auteurs)
        {

            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = auteurs;
            gridView1.Columns["cin"].Caption = "N° CIN";
            gridView1.Columns["codeauteur"].Visible = false;
            gridView1.Columns["numeroaut"].Caption = "Code auteur";
            gridView1.Columns["nom"].Caption = "Nom";
            gridView1.Columns["prenom"].Caption = "Prenom";
            gridView1.Columns["tel"].Caption = "Mobile";
            gridView1.Columns["adr"].Visible = false;
            gridView1.Columns["email"].Visible = false;
            gridView1.Columns["institution"].Visible = false;
            gridView1.Columns["specialite"].Visible = false;
            gridView1.Columns["ville"].Visible = false;
            gridView1.Columns["codepostal"].Visible = false;
            gridView1.Columns["web"].Visible = false;
            gridView1.Columns["image"].Caption = "Image";
         
            gridView1.Columns["image"].BestFit();




        }
     
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            List<auteur> auteurs = new List<auteur>();
            auteurs = autser.getallauthor();
            fillgrid(auteurs);
            splashScreenManager1.CloseWaitForm();
        }

        private void Consultfr_Activated(object sender, EventArgs e)
        {
            List<auteur> auteurs = new List<auteur>();
            auteurs = autser.getallauthor();
            fillgrid(auteurs);

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
                    autser.deleteauth((auteur)gridView1.GetRow(gridView1.FocusedRowHandle));
                    List<auteur> auts = new List<auteur>();
                    auts = autser.getallauthor();
                    fillgrid(auts);
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
                        auteur aut = (auteur)gridView1.GetRow(i);

                        autser.deleteauth((aut));

                    }
                    
                    List<auteur> auts = new List<auteur>();
                    auts = autser.getallauthor();
                    fillgrid(auts);
                    splashScreenManager1.CloseWaitForm();
                }
            }
            
          
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            Home.newauthor = NewAuthor.Instance();
            Home.newauthor.MdiParent = Home.ActiveForm;
            Home.newauthor.Show();
            Home.newauthor.BringToFront();
            splashScreenManager1.CloseWaitForm();
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView1.GetSelectedRows() != null)
            {
                auteur aut = (auteur)gridView1.GetRow(gridView1.FocusedRowHandle);

                autser.updateaut(aut);
                List<auteur> auteurs = new List<auteur>();
                auteurs = autser.getallauthor();
                fillgrid(auteurs);
            }
        }

        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {

                Point pt = this.Location;
                pt.Offset(this.Left + e.X, this.Top + e.Y);
                popupMenu1.ShowPopup(this.barManager1, Control.MousePosition);
            }
        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyData == Keys.Enter)
            {

                if (gridView1.GetSelectedRows() != null)
                {
                     
                    auteur aut = (auteur)gridView1.GetRow(gridView1.FocusedRowHandle);

                    autser.updateaut(aut);
                  
                }
            }
            
        

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {

        }
    }


}