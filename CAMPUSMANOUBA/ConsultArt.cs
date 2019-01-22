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
    public partial class ConsultArt : DevExpress.XtraEditors.XtraForm
    {
        public ConsultArt()
        {
            InitializeComponent();
        }
        private ArtService artser = new ArtService();
         List<Livre> listarts = new List<Livre>();

        NewArticle art = NewArticle.Instance();
        AutService ser =AutService.Instance();
        sousfamilleservice sousfmser = sousfamilleservice.Instance();
        familleservice fmser = familleservice.Instance();
        FrService frser = FrService.Instance();
        private static ConsultArt instance;

        public static ConsultArt Instance()
        {
            if (instance == null)

            instance = new ConsultArt();
            return instance;

        }
       
        private void AjoutFr_Load(object sender, EventArgs e)
        {
            List<InfoLivre> listarts = new List<InfoLivre>();
            listarts = artser.findlastten();
            fillgrid(listarts);

        
        }

      

        private void Consultfr_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        //private void simpleButton5_Click(object sender, EventArgs e)
        //{
        //    splashScreenManager1.ShowWaitForm();
        //    List<Article> articles = new List<Article>();
        //    if (lookfamille.EditValue == null && looksfamille.EditValue == null && lookimp.EditValue == null && lookaut.EditValue == null)
        //    {
        //        articles = artser.getallart();
        //        fillgrid(articles);

        //    }
        //    else if (lookfamille.EditValue == null && looksfamille.EditValue == null && lookimp.EditValue == null && lookaut.EditValue != null)
        //    {
        //        articles = artser.findbyaut(lookaut.Text);
        //        fillgrid(articles);
        //    }
        //    else if (lookfamille.EditValue == null && looksfamille.EditValue == null && lookimp.EditValue != null && lookaut.EditValue == null)
        //    {
        //        articles = artser.findbyfr(lookimp.Text);
        //        fillgrid(articles);
        //    }
        //    else if (lookfamille.EditValue == null && looksfamille.EditValue == null && lookimp.EditValue != null && lookaut.EditValue != null)
        //    {
        //        articles = artser.findbyautimp(lookaut.Text,lookimp.Text);
        //        fillgrid(articles);
        //    }

        //    else if (lookfamille.EditValue != null && looksfamille.EditValue == null && lookimp.EditValue == null && lookaut.EditValue == null)
        //    {
        //        articles = artser.findbyfamily(lookfamille.Text);
        //        fillgrid(articles);
        //    }
        //    else if (lookfamille.EditValue != null && looksfamille.EditValue == null && lookimp.EditValue == null && lookaut.EditValue != null)
        //    {
        //        articles = artser.findbyfmaut(lookfamille.Text,lookaut.Text);
        //        fillgrid(articles);
        //    }
        //    else if (lookfamille.EditValue != null && looksfamille.EditValue == null && lookimp.EditValue != null && lookaut.EditValue == null)
        //    {
        //        articles = artser.findbyfmimp(lookfamille.Text, lookimp.Text);
        //        fillgrid(articles);
        //    }
        //    else if (lookfamille.EditValue != null && looksfamille.EditValue == null && lookimp.EditValue != null && lookaut.EditValue != null)
        //    {
        //        articles = artser.findbyfamautimpr(lookfamille.Text,lookaut.Text, lookimp.Text);
        //        fillgrid(articles);

        //    }
        //    else if (lookfamille.EditValue != null && looksfamille.EditValue != null && lookimp.EditValue == null && lookaut.EditValue == null)
        //    {
        //        articles = artser.findbyfmsousfm(lookfamille.Text, looksfamille.Text);
        //        fillgrid(articles);
        //    }
        //    else if (lookfamille.EditValue != null && looksfamille.EditValue != null && lookimp.EditValue == null && lookaut.EditValue != null)
        //    {
        //        articles = artser.findbyfamsfmaut(lookfamille.Text, looksfamille.Text, lookaut.Text);
        //        fillgrid(articles);
        //    }
        //    else if (lookfamille.EditValue != null && looksfamille.EditValue != null && lookimp.EditValue != null && lookaut.EditValue == null)
        //    {
        //        articles = artser.findbyfamsfimp(lookfamille.Text, looksfamille.Text, lookimp.Text);
        //        fillgrid(articles);
        //    }
        //    else if (lookfamille.EditValue != null && looksfamille.EditValue != null && lookimp.EditValue != null && lookaut.EditValue != null)
        //    {
        //        articles = artser.findbyfamsfmautimpr(lookfamille.Text, looksfamille.Text, lookaut.Text,lookimp.Text);
        //        fillgrid(articles);
        //    }
        //    else
        //    {
        //        MessageBox.Show("something goes wrong");
        //    }

        //    splashScreenManager1.CloseWaitForm();
        //}
        ////public void fillfamily()
        //{
        //    List<famille> sps = new List<famille>();
        //    sps = fmser.getallfamille();
        //    lookfamille.Properties.ValueMember = "idfamille";
        //    lookfamille.Properties.DisplayMember = "familledesign";
        //    lookfamille.Properties.DataSource = sps;
        //    lookfamille.Properties.PopulateColumns();
        //    lookfamille.Properties.Columns["idfamille"].Visible = false;
        //    lookfamille.Properties.Columns["familledesign"].Caption = "Famille";
        //}
        //public void fillfrs()
        //{
        //    List<fournisseur> sps = new List<fournisseur>();
        //    sps = frser.getallfr();
        //    lookimp.Properties.ValueMember = "numerofr";
        //    lookimp.Properties.DisplayMember = "raisonfr";
        //    lookimp.Properties.DataSource = sps;
        //    lookimp.Properties.PopulateColumns();
        //    lookimp.Properties.Columns[0].Visible = false;
        //    lookimp.Properties.Columns[1].Caption = "numerofr";
        //    lookimp.Properties.Columns[2].Caption = "raisonfr";
        //    lookimp.Properties.Columns[3].Visible = false;
        //    lookimp.Properties.Columns[4].Visible = false;
        //    lookimp.Properties.Columns[5].Visible = false;
        //    lookimp.Properties.Columns[6].Visible = false;
        //    lookimp.Properties.Columns[7].Visible = false;
        //    lookimp.Properties.Columns[8].Visible = false;
        //    lookimp.Properties.Columns[9].Visible = false;
        //    lookimp.Properties.Columns[10].Visible = false;
        //    lookimp.Properties.Columns[11].Visible = false;

        //}

        //private void lookfamille_EditValueChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        famille fm = new famille();
        //        fm = (famille)lookfamille.GetSelectedDataRow();
        //        List<sousfamille> soufm = new List<sousfamille>();
        //        soufm = sousfmser.getallsousfamilleByfamille(fm.idfamille);


        //        looksfamille.Properties.ValueMember = "idsousfamille";
        //        looksfamille.Properties.DisplayMember = "sousfamilledesign";
        //        looksfamille.Properties.DataSource = soufm;
        //        looksfamille.Properties.PopulateColumns();
        //        looksfamille.Properties.Columns["idsousfamille"].Visible = false;
        //        looksfamille.Properties.Columns["familleid"].Visible = false;
        //        looksfamille.Properties.Columns["familledesign"].Visible = false;
        //        looksfamille.Properties.Columns["sousfamilledesign"].Caption = "Sous famille";


        //    }
        //    catch (Exception exce)
        //    { }
        //}
        ////public void getallauthor()
        //{
        //    List<auteur> authors = new List<auteur>();
        //    authors = ser.getallauthor();

        //    lookaut.Properties.ValueMember = "codeauteur";
        //    lookaut.Properties.DisplayMember = "nom";
        //    lookaut.Properties.DataSource = authors;
        //    lookaut.Properties.PopulateColumns();
        //    lookaut.Properties.Columns["codeauteur"].Visible = false;
        //    lookaut.Properties.Columns["numeroaut"].Visible = false;
        //    lookaut.Properties.Columns["nom"].Caption = "Nom";
        //    lookaut.Properties.Columns["prenom"].Visible = false;
        //    lookaut.Properties.Columns["tel"].Visible = false;
        //    lookaut.Properties.Columns["adr"].Visible = false;
        //    lookaut.Properties.Columns["email"].Visible = false;
        //    lookaut.Properties.Columns["institution"].Visible = false;
        //    lookaut.Properties.Columns["specialite"].Visible = false;
        //    lookaut.Properties.Columns["ville"].Visible = false;
        //    lookaut.Properties.Columns["codepostal"].Visible = false;
        //    lookaut.Properties.Columns["web"].Visible = false;
        //    lookaut.Properties.Columns["image"].Visible = false;


        //}

        private void ConsultArt_Activated(object sender, EventArgs e)
        {
            //getallauthor();
            //fillfamily();
            //fillfrs();
        }

        //private void simpleButton6_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        lookimp.EditValue = null;
        //        lookfamille.EditValue = null;
        //        looksfamille.EditValue = null;
        //        lookaut.EditValue = null;
        //    }
        //    catch (Exception except)
        //    { }

        //}

        private void fillgrid(List<InfoLivre> arts)
        {
          
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = arts;
            gridView1.Columns[0].Caption = "Rang";
            gridView1.Columns[1].Caption = "Code";
            gridView1.Columns[2].Caption = "Titre";
          
            gridView1.Columns[3].Caption = "Imprimerie";
            gridView1.Columns[4].Caption = "Auteur";
            gridView1.Columns[5].Caption = "I.S.B.N";
            gridView1.Columns[6].Caption = "Code a barre";
            gridView1.Columns[7].Caption = "Quantité en stock";
                    

        }

     

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
          
            List<InfoLivre> listarts = new List<InfoLivre>();
            listarts = artser.findlastten();
            fillgrid(listarts);
            splashScreenManager1.CloseWaitForm();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            art = NewArticle.Instance();
            art.MdiParent = GestionStock.Instance();
            art.Show();
            art.BringToFront();
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
                if (MessageBox.Show("Vous etes sure de supprimer cet élément ??", "Suppression des articles", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Livre liv = new Livre();
                    InfoLivre livbref = (InfoLivre)gridView1.GetRow(gridView1.FocusedRowHandle);
                    liv = artser.getArticleByCode(livbref.codeart);

                    artser.removeart(liv);
                    List<InfoLivre> arts = new List<InfoLivre>();
                    arts = artser.findlastten();
                    fillgrid(arts);
                }
            }

            else
            {

                if (MessageBox.Show("Vous etes sure de supprimer toute la selection ??", "Suppression des articles", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    foreach (int i in gridView1.GetSelectedRows())
                    {
                        findArtbref_Result art = (findArtbref_Result)gridView1.GetRow(i);
                        Livre liv = new Livre();
                     
                        liv = artser.getArticleByCode(art.codeart);
                        artser.removeart((liv));

                    }
                    
                    List<InfoLivre> arts = new List<InfoLivre>();
                    arts = artser.findlastten();
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

                    Livre art = (Livre)gridView1.GetRow(gridView1.FocusedRowHandle);

                    artser.updateart(art);

                }
            }

        }

       
        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            Livre art = new Livre();
            art = (Livre)gridView1.GetRow(gridView1.FocusedRowHandle);
            MessageBox.Show(""+art.codeart);
            UpdateArticle updart = new UpdateArticle(art);
            updart.ShowDialog();
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            findArtbref_Result livbref = (findArtbref_Result)gridView1.GetRow(gridView1.FocusedRowHandle);
           
            DetailArticle detail = new DetailArticle(livbref.codeart);
            detail.ShowDialog();
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            //string FileName = "C:\\MyFiles\\Grid.xls";
            //MyGridControl.ExportToXls(FileName)
            gridView1.Print();
        }
    }



    
}