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
    public partial class NewAuthor : DevExpress.XtraEditors.XtraForm
    {
        public NewAuthor()
        {
            InitializeComponent();
        }
        OpenFileDialog ofd = new OpenFileDialog();
        SpecialityService spserv = SpecialityService.Instance();
        AutService autoser = AutService.Instance();
      
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
        private static NewAuthor instance;

        public static NewAuthor Instance()
        {
            if (instance == null)

                instance = new NewAuthor();
            return instance;

        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            try
            {


            specialite spec =(specialite) lookUpEdit1.GetSelectedDataRow();
            auteur aut = new auteur();
            aut.nom = tnom.Text;
            aut.prenom = tprenom.Text;
            aut.adr = tadr.Text;
            aut.email = temail.Text;
            aut.institution = tinst.Text;
            aut.ville = tville.Text;
            aut.web = tweb.Text;
            aut.specialite = spec.designation;
            aut.codepostal = tcodep.Text;
            if (pictureEdit2.EditValue != null)
            {
                aut.image = ReadFile(textEdit6.Text);
            }

            autoser.addauteur(aut);
            MessageBox.Show("Ajouté avec succés");
            ClearAllForm(this);

            }catch(Exception exc)
            { }
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

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            Specialiteadd categ = new Specialiteadd();
            categ.ShowDialog();
        }

        private void NewAuthor_Load(object sender, EventArgs e)
        {

            fillspecs();
        }

        public void fillspecs()
        {
            List<specialite> sps = new List<specialite>();
            sps = spserv.getallspeciality();
            lookUpEdit1.Properties.ValueMember = "designation";
            lookUpEdit1.Properties.DisplayMember = "designation";
            lookUpEdit1.Properties.DataSource = sps;
            lookUpEdit1.Properties.PopulateColumns();
            lookUpEdit1.Properties.Columns["idsp"].Visible = false;
            lookUpEdit1.Properties.Columns["designation"].Caption = "Spécialité";
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            ofd.Title = "joindre des fichiers";
            if (ofd.CheckFileExists == true)
            {
                ofd.Filter = "";
                ofd.ShowDialog();
                if (ofd.FileName != "")
                {
                    FileInfo fi = new FileInfo(ofd.FileName);
                    textEdit6.Text = ofd.FileName;
                    pictureEdit2.Image = Image.FromFile(ofd.FileName);
                }
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

        private void simpleButton1_Click(object sender, EventArgs e)
        {

        }
    }
}