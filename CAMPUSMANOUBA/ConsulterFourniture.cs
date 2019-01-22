using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using BLL;
using DAL;
using System.Threading;
namespace CAMPUSMANOUBA
{
    public partial class ConsulterFourniture : DevExpress.XtraEditors.XtraForm
    {
        public ConsulterFourniture()
        {
            InitializeComponent();
        }

        private static NewFourniture nFourniture;
        private FournitureService fourser = new FournitureService();
        List<Fourniture> listarts = new List<Fourniture>();


        AutService ser = AutService.Instance();
        sousfamilleservice sousfmser = sousfamilleservice.Instance();
        familleservice fmser = familleservice.Instance();
        FrService frser = FrService.Instance();
        private static ConsulterFourniture instance;
        

        public static ConsulterFourniture Instance()
        {
            if (instance == null)

                instance = new ConsulterFourniture();
            return instance;

        }

        private void AjoutFr_Load(object sender, EventArgs e)
        {
            List<Fourniture> listfour = new List<Fourniture>();
            listfour = fourser.findlastten();
            fillgrid(listfour);


        }



        private void Consultfr_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            List<Fourniture> articles = new List<Fourniture>();
            if (lookfamille.EditValue == null && looksfamille.EditValue == null && lookfour.EditValue == null)
            {
                articles = fourser.getallart();
                fillgrid(articles);

            }
            else if (lookfamille.EditValue == null && looksfamille.EditValue == null && lookfour.EditValue != null)
            {
                articles = fourser.findbyfr(lookfour.Text);
                fillgrid(articles);
            }
           
            //else if (lookfamille.EditValue == null && looksfamille.EditValue == null && lookimp.EditValue != null && lookfour.EditValue != null)
            //{
            //    articles = artser.findbyautimp(lookfour.Text, lookimp.Text);
            //    fillgrid(articles);
            //}

            else if (lookfamille.EditValue != null && looksfamille.EditValue == null  && lookfour.EditValue == null)
            {
                articles = fourser.findbyfamily(lookfamille.Text);
                fillgrid(articles);
            }
            else if (lookfamille.EditValue != null && looksfamille.EditValue == null  && lookfour.EditValue != null)
            {
                articles = fourser.findbyfmfr(lookfamille.Text, lookfour.Text);
                fillgrid(articles);
            }
            
            
            else if (lookfamille.EditValue != null && looksfamille.EditValue != null  && lookfour.EditValue == null)
            {
                articles = fourser.findbyfmsousfm(lookfamille.Text, looksfamille.Text);
                fillgrid(articles);
            }
            else if (lookfamille.EditValue != null && looksfamille.EditValue != null  && lookfour.EditValue != null)
            {
                articles = fourser.findbyfamsfmfr(lookfamille.Text, looksfamille.Text, lookfour.Text);
                fillgrid(articles);
            }
            
            
            else
            {
                MessageBox.Show("something goes wrong");
            }

            splashScreenManager1.CloseWaitForm();
        }
        public void fillfamily()
        {
            List<famille> sps = new List<famille>();
            sps = fmser.getallfamille();
            lookfamille.Properties.ValueMember = "idfamille";
            lookfamille.Properties.DisplayMember = "familledesign";
            lookfamille.Properties.DataSource = sps;
            lookfamille.Properties.PopulateColumns();
            lookfamille.Properties.Columns["idfamille"].Visible = false;
            lookfamille.Properties.Columns["familledesign"].Caption = "Famille";
        }
        public void fillfrs()
        {
            List<fournisseur> sps = new List<fournisseur>();
            sps = frser.getallfr();
            lookfour.Properties.ValueMember = "numerofr";
            lookfour.Properties.DisplayMember = "raisonfr";
            lookfour.Properties.DataSource = sps;
            lookfour.Properties.PopulateColumns();
            lookfour.Properties.Columns[0].Visible = false;
            lookfour.Properties.Columns[1].Caption = "numerofr";
            lookfour.Properties.Columns[2].Caption = "raisonfr";
            lookfour.Properties.Columns[3].Visible = false;
            lookfour.Properties.Columns[4].Visible = false;
            lookfour.Properties.Columns[5].Visible = false;
            lookfour.Properties.Columns[6].Visible = false;
            lookfour.Properties.Columns[7].Visible = false;
            lookfour.Properties.Columns[8].Visible = false;
            lookfour.Properties.Columns[9].Visible = false;
            lookfour.Properties.Columns[10].Visible = false;
            lookfour.Properties.Columns[11].Visible = false;

        }

        private void lookfamille_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                famille fm = new famille();
                fm = (famille)lookfamille.GetSelectedDataRow();
                List<sousfamille> soufm = new List<sousfamille>();
                soufm = sousfmser.getallsousfamilleByfamille(fm.idfamille);


                looksfamille.Properties.ValueMember = "idsousfamille";
                looksfamille.Properties.DisplayMember = "sousfamilledesign";
                looksfamille.Properties.DataSource = soufm;
                looksfamille.Properties.PopulateColumns();
                looksfamille.Properties.Columns["idsousfamille"].Visible = false;
                looksfamille.Properties.Columns["familleid"].Visible = false;
                looksfamille.Properties.Columns["familledesign"].Visible = false;
                looksfamille.Properties.Columns["sousfamilledesign"].Caption = "Sous famille";


            }
            catch (Exception exce)
            { }
        }
        //public void getallauthor()
        //{
        //    List<fournisseur> authors = new List<fournisseur>();
        //    authors = ser.getallauthor();

        //    lookfour.Properties.ValueMember = "codeauteur";
        //    lookfour.Properties.DisplayMember = "nom";
        //    lookfour.Properties.DataSource = authors;
        //    lookfour.Properties.PopulateColumns();
        //    lookfour.Properties.Columns["codeauteur"].Visible = false;
        //    lookfour.Properties.Columns["numeroaut"].Visible = false;
        //    lookfour.Properties.Columns["nom"].Caption = "Nom";
        //    lookfour.Properties.Columns["prenom"].Visible = false;
        //    lookfour.Properties.Columns["tel"].Visible = false;
        //    lookfour.Properties.Columns["adr"].Visible = false;
        //    lookfour.Properties.Columns["email"].Visible = false;
        //    lookfour.Properties.Columns["institution"].Visible = false;
        //    lookfour.Properties.Columns["specialite"].Visible = false;
        //    lookfour.Properties.Columns["ville"].Visible = false;
        //    lookfour.Properties.Columns["codepostal"].Visible = false;
        //    lookfour.Properties.Columns["web"].Visible = false;
        //    lookfour.Properties.Columns["image"].Visible = false;


        //}

        private void ConsultArt_Activated(object sender, EventArgs e)
        {
            //getallauthor();
            fillfamily();
            fillfrs();
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            try
            {
                //lookfour.EditValue = null;
                lookfamille.EditValue = null;
                looksfamille.EditValue = null;
                lookfour.EditValue = null;
            }
            catch (Exception except)
            { }

        }

        private void fillgrid(List<Fourniture> four)
        {

            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = four;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Caption = "Code";
            gridView1.Columns[2].Caption = "Famille";
            gridView1.Columns[3].Visible = false;
            gridView1.Columns[4].Caption = "Sous famille";
            gridView1.Columns[5].Visible = false;
            gridView1.Columns[6].Caption = "Désignation";
            gridView1.Columns[7].Caption = "Fournisseur";

            gridView1.Columns[8].Caption = "Quantité en stock";
            gridView1.Columns[9].Caption = "Prix";
            gridView1.Columns[10].Caption = "Image";
            gridView1.Columns[11].Visible = false;
           
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            List<Fourniture> listfour = new List<Fourniture>();
            listfour = fourser.getallart();
            fillgrid(listarts);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            //Thread.Sleep(500);
            List<Fourniture> listfour = new List<Fourniture>();
            listfour = fourser.findlastten();
            fillgrid(listfour);
            splashScreenManager1.CloseWaitForm();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            nFourniture = NewFourniture.Instance();
            nFourniture.MdiParent = this;
            nFourniture.Show();
            nFourniture.BringToFront();
            splashScreenManager1.CloseWaitForm();


           
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
                if (MessageBox.Show("Vous etes sure de supprimer cet élément ??", "Suppression des Fournitures", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {  //foreach(Fourniture f in (Fourniture)gridView1.GetRow(gridView1.FocusedRowHandle))
                    fourser.removeart((Fourniture)gridView1.GetRow(gridView1.FocusedRowHandle));
                    List<Fourniture> arts = new List<Fourniture>();
                    arts = fourser.getallart();
                    fillgrid(arts);
                }
            }

            else
            {

                if (MessageBox.Show("Vous etes sure de supprimer toute la selection ??", "Suppression des Fournitures", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    
                    foreach (int i in gridView1.GetSelectedRows())
                    {
                        Fourniture four = (Fourniture)gridView1.GetRow(i);

                        fourser.removeart((four));

                    }
                    



                    List<Fourniture> arts = new List<Fourniture>();
                    arts = fourser.getallart();
                    fillgrid(arts);
                }
            }



        }

        private void gridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {

                if (gridView1.GetSelectedRows() != null)
                {

                    Fourniture art = (Fourniture)gridView1.GetRow(gridView1.FocusedRowHandle);

                    fourser.updatefourniture(art);

                }
            }

        }


        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Fourniture art = new Fourniture();
            art = (Fourniture)gridView1.GetRow(gridView1.FocusedRowHandle);
            MessageBox.Show("" + art.codefour);
            UpdateFourniture updart = new UpdateFourniture(art);
            updart.ShowDialog();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {

        }
    }




}