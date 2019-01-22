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
    public partial class GestionsUtilisateurs : DevExpress.XtraEditors.XtraForm
    {
        public GestionsUtilisateurs()
        {
            InitializeComponent();

        }
      private static   List<Droit> dts = new List<Droit>();
        private static    Droit dr = new Droit();
        UserService userser = new UserService();
        private static GestionsUtilisateurs instance;
        public static GestionsUtilisateurs Instance()
        {
            if (instance == null)

                instance = new GestionsUtilisateurs();

            return instance;

        }

        private void Duplicata_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void Duplicata_Activated(object sender, EventArgs e)
        {

        }

        private void tileItem1_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage1;
           
        }

     

        private void Gestioncommande_Load(object sender, EventArgs e)
        {
          
            dts = userser.getallinfo();
            getalldatatable(dts);
        }


        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == xtraTabPage1)
            {

            }
            if (xtraTabControl1.SelectedTabPage == xtraTabPage2)
            {
                dts = userser.getallinfo();
                getalldatatable(dts);
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
        public void validateform(Control Ctrl)
        {
            foreach (Control ctrl in Ctrl.Controls)
            {
                BaseEdit editor = ctrl as BaseEdit;
                if (editor == null)
                    editor.EditValue = "";


            }
        }
        private void getalldatatable(List<Droit> dts)
        {

            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = dts;

            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Caption = "Nom";
            gridView1.Columns[2].Caption = "Prénom";
            gridView1.Columns[3].Caption = "Gsm";
            gridView1.Columns[4].Caption = "Email";
            gridView1.Columns[5].Caption = "Fonction";
            gridView1.Columns[6].Caption = "Type";
            gridView1.Columns[7].Visible = false;
            gridView1.Columns[8].Visible = false;
            gridView1.Columns[9].Visible = false;
            gridView1.Columns[10].Visible = false;
            gridView1.Columns[11].Visible = false;
            gridView1.Columns[12].Visible = false;
            gridView1.Columns[13].Visible = false;
            gridView1.Columns[14].Visible = false;
            gridView1.Columns[15].Visible = false;
            gridView1.Columns[16].Visible = false;
            gridView1.Columns[17].Visible = false;
            gridView1.Columns[18].Visible = false;
            gridView1.Columns[19].Visible = false;
            gridView1.Columns[20].Visible = false;
            gridView1.Columns[21].Visible = false;
            gridView1.Columns[22].Visible = false;
        }


        private void pictureEdit1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tileItem2_ItemClick_1(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage2;       
            getalldatatable(dts);

        }

        private void textEdit5_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tusability.Text == "Administrateur")
            {
                groupControl1.Enabled = false;
            }
            else
            {
                groupControl1.Enabled = true;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {

            Droit dr1 = new Droit();

            dr1.nom = tnom.Text;
            dr1.prenom = tprenom.Text;
            dr1.gsm = tgsm.Text;
            dr1.email = temail.Text;
            dr1.fonction = tfonction.Text;
            dr1.utilisabilite = tusability.Text;
            dr1.login = tpseudo.Text;
            dr1.password = tpassword.Text;
            if (tusability.Text == "Administrateur")
            {

                dr1.GestClt = "true";
                dr1.GestAut = "true";
                dr1.GestSt = "true";
                dr1.GestDup = "true";
                dr1.GestStat = "true";
                dr1.GestRapp = "true";
                dr1.GestCompta = "true";
                dr1.GestPub = "true";
                dr1.GestAdmin = "true";
                dr1.GestFr = "true";
                dr1.GestVente = "true";
                dr1.GestFourniture = "true";
                dr1.GestDon = "true";
                dr1.GestDevis = "true";
            }
            else
            {
                if (gestclt.Checked == true)
                {
                    dr1.GestClt = "true";
                }
                else
                {
                    dr1.GestClt = "false";
                }
                if (gestaut.Checked == true)
                {
                    dr1.GestAut = "true";
                }
                else
                {
                    dr1.GestAut = "false";
                }
                if (gestst.Checked == true)
                {
                    dr1.GestSt = "true";
                }
                else
                {
                    dr1.GestSt = "false";
                }
                if (gestvt.Checked == true)
                {
                    dr1.GestVente = "true";
                }
                else
                {
                    dr1.GestVente = "false";
                }
                if (dup.Checked == true)
                {
                    dr1.GestDup = "true";
                }
                else
                {
                    dr1.GestDup = "false";
                }
                if (stat.Checked == true)
                {
                    dr1.GestStat = "true";
                }
                else
                {
                    dr1.GestStat = "false";
                }
                if (rap.Checked == true)
                {
                    dr1.GestRapp = "true";
                }
                else
                {
                    dr1.GestRapp = "false";
                }
                if (compta.Checked == true)
                {
                    dr1.GestCompta = "true";
                }
                else
                {
                    dr1.GestCompta = "false";
                }
                if (pub.Checked == true)
                {
                    dr1.GestPub = "true";
                }
                else
                {
                    dr1.GestPub = "false";
                }
                if (administ.Checked == true)
                {
                    dr1.GestAdmin = "true";
                }
                else
                {
                    dr1.GestAdmin = "false";
                }
                if (gestfr.Checked == true)
                {
                    dr1.GestFr = "true";
                }
                else
                {
                    dr1.GestFr = "false";
                }
                if (Gestdon.Checked == true)
                {
                    dr1.GestDon = "true";
                }
                else
                {
                    dr1.GestDon = "false";
                }
                if (gestdevis.Checked == true)
                {
                    dr1.GestDevis = "true";
                }
                else
                {
                    dr1.GestDevis = "false";
                }
                if (gestfour.Checked == true)
                {
                    dr1.GestFourniture = "true";
                }
                else
                {
                    dr1.GestFourniture = "false";
                }
            }
            DialogResult dialogResult = MessageBox.Show("Voulez vous enregistrer les informations ?", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                userser.addUser(dr1);
                MessageBox.Show("Utilisateur ajouté avec succés");
                ClearAllForm(this);
            }

        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage1;
            simpleButton1.Enabled = false;
            int count = 0;
            count = gridView1.RowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
               
                dr = (Droit)gridView1.GetRow(gridView1.FocusedRowHandle);
                tnom.Text = dr.nom;
                tprenom.Text=dr.prenom ;
                tgsm.Text = dr.gsm;
                temail.Text = dr.email;
                tfonction.Text = dr.fonction;
                tusability.Text = dr.utilisabilite;
                tpseudo.Text = dr.login;
                tpassword.Text = dr.password;
                if (dr.utilisabilite == "Administrateur")
                {



                }
                else
                {
                    if (dr.GestClt == "true")

                    {
                        gestclt.Checked = true;
                    }

                    if (dr.GestAut == "true")
                    {
                        gestaut.Checked = true;
                    }

                    if (dr.GestSt == "true")
                    {
                        gestst.Checked = true;
                    }

                    if (dr.GestVente == "true")
                    {
                        gestvt.Checked = true;
                    }

                    if (dr.GestDup == "true")
                    {
                        dup.Checked = true;
                    }

                    if (dr.GestStat == "true")
                    {
                        stat.Checked = true;
                    }

                    if (dr.GestRapp == "true")
                    {
                        rap.Checked = true;
                    }

                    if (dr.GestCompta == "true")
                    {
                        compta.Checked = true;
                    }

                    if (dr.GestPub == "true")
                    {
                        pub.Checked = true;
                    }

                    if (dr.GestAdmin == "true")
                    {
                        administ.Checked = true;
                    }

                    if (dr.GestFr == "true")
                    {
                        gestfr.Checked = true;
                    }


                }
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Droit dr2 = new Droit();
            dr2.code = dr.code;
            dr2.nom = tnom.Text;
            dr2.prenom = tprenom.Text;
            dr2.gsm = tgsm.Text;
            dr2.email = temail.Text;
            dr2.fonction = tfonction.Text;
            dr2.utilisabilite = tusability.Text;
            dr2.login = tpseudo.Text;
            dr2.password = tpassword.Text;
            if (tusability.Text == "Administrateur")
            {

                dr2.GestClt = "true";
                dr2.GestAut = "true";
                dr2.GestSt = "true";
                dr2.GestDup = "true";
                dr2.GestStat = "true";
                dr2.GestRapp = "true";
                dr2.GestCompta = "true";
                dr2.GestPub = "true";
                dr2.GestAdmin = "true";
                dr2.GestFr = "true";
                dr2.GestVente = "true";
                dr2.GestFourniture = "true";
                dr2.GestDon = "true";
                dr2.GestDevis = "true";
            }
            else
            {
                if (gestclt.Checked == true)
                {
                    dr2.GestClt = "true";
                }
                else
                {
                    dr2.GestClt = "false";
                }
                if (gestaut.Checked == true)
                {
                    dr2.GestAut = "true";
                }
                else
                {
                    dr2.GestAut = "false";
                }
                if (gestst.Checked == true)
                {
                    dr2.GestSt = "true";
                }
                else
                {
                    dr2.GestSt = "false";
                }
                if (gestvt.Checked == true)
                {
                    dr2.GestVente = "true";
                }
                else
                {
                    dr2.GestVente = "false";
                }
                if (dup.Checked == true)
                {
                    dr2.GestDup = "true";
                }
                else
                {
                    dr2.GestDup = "false";
                }
                if (stat.Checked == true)
                {
                    dr2.GestStat = "true";
                }
                else
                {
                    dr2.GestStat = "false";
                }
                if (rap.Checked == true)
                {
                    dr2.GestRapp = "true";
                }
                else
                {
                    dr2.GestRapp = "false";
                }
                if (compta.Checked == true)
                {
                    dr2.GestCompta = "true";
                }
                else
                {
                    dr2.GestCompta = "false";
                }
                if (pub.Checked == true)
                {
                    dr2.GestPub = "true";
                }
                else
                {
                    dr2.GestPub = "false";
                }
                if (administ.Checked == true)
                {
                    dr2.GestAdmin = "true";
                }
                else
                {
                    dr2.GestAdmin = "false";
                }
                if (gestfr.Checked == true)
                {
                    dr2.GestFr = "true";
                }
                else
                {
                    dr2.GestFr = "false";
                }
                if (Gestdon.Checked == true)
                {
                    dr2.GestDon = "true";
                }
                else
                {
                    dr2.GestDon = "false";
                }
                if (gestdevis.Checked == true)
                {
                    dr2.GestDevis = "true";
                }
                else
                {
                    dr2.GestDevis = "false";
                }
                if (gestfour.Checked == true)
                {
                    dr2.GestFourniture = "true";
                }
                else
                {
                    dr2.GestFourniture = "false";
                }
            }
            
            DialogResult dialogResult = MessageBox.Show("Voulez vous enregistrer les modifications ?", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                userser.updateDroit(dr2);
                MessageBox.Show("Utilisateur modifié avec succés");
                ClearAllForm(this);
                simpleButton1.Enabled = true;

            }
          
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Voulez vous annuler l'opération?", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                ClearAllForm(this);
                simpleButton1.Enabled = true;

            }
           
          
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Voulez vous Supprimer l'utilisateur suivant?", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                int count = 0;
                count = gridView1.RowCount;
                if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
                {
                    Droit dr3 = new Droit();
                         
                    dr3 = (Droit)gridView1.GetRow(gridView1.FocusedRowHandle);
                    userser.deleteuser(dr3);
                    MessageBox.Show("Supprimé avec succés");
                }
            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {

            dts = userser.getallinfo();
            getalldatatable(dts);
        }
    }
}