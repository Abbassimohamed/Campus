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
using System.IO;

namespace CAMPUSMANOUBA
{
    public partial class UpdateFourniture : DevExpress.XtraEditors.XtraForm
    {
        private static Fourniture art1;
        public UpdateFourniture(Fourniture art)
        {
            InitializeComponent();
            try
            {
                fillfamily();
                fillfrs();
                //fillmagasin();
                //getallauthor();
                getarticle(art);
               
            }
            catch (Exception e)
            { }

        }

        private static string codegen = "";
        private static NewArticle instance;
        familleservice fmser = new familleservice();
        sousfamilleservice sousfmser = new sousfamilleservice();
        FrService frser = new FrService();
        emplacementService empser = new emplacementService();
        AutService ser = new AutService();
        FournitureService fourser = new FournitureService();
        public static NewArticle Instance()
        {
            if (instance == null)

                instance = new NewArticle();
            return instance;

        }
        OpenFileDialog ofd = new OpenFileDialog();
        byte[] ReadFile(string sPath)
        {
            byte[] data = null;
            FileInfo fInfo = new FileInfo(sPath);
            long numBytes = fInfo.Length;
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fStream);
            data = br.ReadBytes((int)numBytes);
            return data;
        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            ClearAllForm(this);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Fourniture art = new Fourniture();

            famille fm = (famille)lookfamille.GetSelectedDataRow();
            sousfamille sfm = (sousfamille)looksfamille.GetSelectedDataRow();
            //auteur aut = (auteur)lookaut.GetSelectedDataRow();
            //emplacement emp = (emplacement)lookabc.GetSelectedDataRow();
            //emplacement emp1 = (emplacement)lookord.GetSelectedDataRow();
            fournisseur fr = (fournisseur)lookimp.GetSelectedDataRow();

            art.codefour = tcodeart.Text;
            //art.isbn = tisbn.Text;
            art.famille = fm.familledesign;
            art.idfamille = fm.idfamille;
            art.sousfamille = sfm.sousfamilledesign;
            art.idsousfamille = sfm.idsousfamille;
            art.designation = ttitre.Text;
            //art.dateedition = dateEdit1.DateTime;
            ///////////////////art.fournisseur = fr.raisonfr;
            ///////////art.idfournisseur = fr.codefr;
            //art.auteur = aut.nom;
            //art.idauteur = aut.codeauteur;
            art.quantitenstock = Convert.ToDouble(tquantit.Text.Replace('.', ','));
            //art.depotlegal = Convert.ToDouble(tdepotleg.Text.Replace('.', ','));
            //art.pvpublic = Convert.ToDouble(tpvpub.Text.Replace('.', ','));
            art.prixAchat = Convert.ToDouble(tpvpub.Text.Replace('.', ','));
            //art.droitaut = Convert.ToDouble(tdroitaut.Text.Replace('.', ','));
            //art.abscice = emp.empdesign;
            //art.ordonne = emp1.empdesign;
            if (textEdit3.Text != null)
            {
                art.image = ReadFile(textEdit3.Text);
            }

            fourser.updatefourniture(art);
            //MessageBox.Show(art.titre);

            ClearAllForm(this);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void NewClient_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {

        }

        private void NewArticle_Load(object sender, EventArgs e)
        {


        }

        private void simpleButton6_Click_1(object sender, EventArgs e)
        {
            familyadd family = new familyadd();
            family.ShowDialog();
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            underfamilyadd undfamily = new underfamilyadd();
            undfamily.ShowDialog();
        }

        private void lookUpEdit1_EditValueChanged_1(object sender, EventArgs e)
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

                //codegeneration();
            }
            catch (Exception exce)
            { }

        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {

        }
        //private void fillmagasin()
        //{
        //    List<emplacement> sps = new List<emplacement>();
        //    sps = empser.getallemp();
        //    lookabc.Properties.ValueMember = "idemp";
        //    lookabc.Properties.DisplayMember = "empdesign";
        //    lookabc.Properties.DataSource = sps;
        //    lookabc.Properties.PopulateColumns();
        //    lookabc.Properties.Columns["idemp"].Visible = false;
        //    lookabc.Properties.Columns["empdesign"].Caption = "emplacement";

        //    lookord.Properties.ValueMember = "idemp";
        //    lookord.Properties.DisplayMember = "empdesign";
        //    lookord.Properties.DataSource = sps;
        //    lookord.Properties.PopulateColumns();
        //    lookord.Properties.Columns["idemp"].Visible = false;
        //    lookord.Properties.Columns["empdesign"].Caption = "emplacement";

        //}
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
            List<fournisseur> frs = new List<fournisseur>();
            frs = frser.getallfr();
            lookimp.Properties.ValueMember = "codefr";
            lookimp.Properties.DisplayMember = "raisonfr";
            lookimp.Properties.DataSource = frs;
            lookimp.Properties.PopulateColumns();
            lookimp.Properties.Columns["codefr"].Visible = false;
            lookimp.Properties.Columns["numerofr"].Caption = "Numero";
            lookimp.Properties.Columns["raisonfr"].Caption = "Raison sociale";
            lookimp.Properties.Columns["responsable"].Visible = false;
            lookimp.Properties.Columns["mobile"].Visible = false;
            lookimp.Properties.Columns["tel"].Visible = false;
            lookimp.Properties.Columns["adress"].Visible = false;
            lookimp.Properties.Columns["ville"].Visible = false;
            lookimp.Properties.Columns["email"].Visible = false;
            lookimp.Properties.Columns["web"].Visible = false;
            lookimp.Properties.Columns["codepostal"].Visible = false;
            lookimp.Properties.Columns["image"].Visible = false;
        }
        //public void getallauthor()
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

        //public void codegeneration()
        //{
        //    if (lookfamille.EditValue != null && looksfamille.EditValue != null && lookaut.EditValue != null)
        //    {
        //        try
        //        {

        //            auteur aut = (auteur)lookaut.GetSelectedDataRow();
        //            codegen = lookfamille.Text.ToString().Substring(0, 1);
        //            codegen += looksfamille.Text.ToString().Substring(0, 1);
        //            codegen += aut.numeroaut.ToString().Substring(0, 3);
        //            int numliv = 0;
        //            numliv = artser.findnumartbyaut(Convert.ToInt32(aut.numeroaut)) + 1;
        //            codegen += numliv;
        //            tcodeart.Text = codegen.ToUpper();
        //            barCodeControl1.Text = codegen.ToUpper();
        //        }
        //        catch (Exception ex)
        //        {
        //            MessageBox.Show(ex.Message);

        //        }

        //    }



        //}

        private void looksfamille_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void looksfamille_EditValueChanged_1(object sender, EventArgs e)
        {
            //codegeneration();
        }

        private void lookaut_EditValueChanged(object sender, EventArgs e)
        {
            //codegeneration();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ofd.Title = "joindre des fichiers";
            if (ofd.CheckFileExists == true)
            {
                ofd.Filter = "";
                ofd.ShowDialog();
                if (ofd.FileName != "")
                {
                    FileInfo fi = new FileInfo(ofd.FileName);
                    textEdit3.Text = ofd.FileName;
                    pictureEdit1.Image = Image.FromFile(ofd.FileName);
                }
            }
        }

        private void simpleButton5_Click_1(object sender, EventArgs e)
        {
            textEdit3.Text = null;
            pictureEdit1.Image = null;
        }

        private void NewArticle_Activated(object sender, EventArgs e)
        {

        }

        private void getarticle(Fourniture art)
        {  
            
            tcodeart.Text = art.codefour;
            //tisbn.Text = art.isbn;
            lookfamille.EditValue = art.famille;
            looksfamille.EditValue = art.sousfamille;
            ttitre.Text = art.designation;
            //dateEdit1.DateTime = (DateTime)art.dateedition;
            lookimp.EditValue = art.fournisseur;


            tquantit.Text = art.quantitenstock.ToString();
            //tdepotleg.Text = art.depotlegal.ToString();
            tpvpub.Text = art.prixAchat.ToString();
            //tpvpro.Text = art.pvpromo.ToString();
            //tdroitaut.Text = art.droitaut.ToString();
            //lookabc.EditValue = art.abscice;
            //lookord.EditValue = art.ordonne;

            if (art.image != null)
            {
                pictureEdit1.EditValue = art.image;
            }
        }

        private void lookfamille_Click(object sender, EventArgs e)
        {

        }

        private void labelControl5_Click(object sender, EventArgs e)
        {

        }
    }

}