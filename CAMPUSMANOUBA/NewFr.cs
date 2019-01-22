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
    public partial class NewFr : DevExpress.XtraEditors.XtraForm
    {
        public NewFr()
        {
            InitializeComponent();
        }
        OpenFileDialog ofd = new OpenFileDialog();
        private static NewFr instance;

        public static NewFr Instance()
        {
            if (instance == null)

                instance = new NewFr();
            return instance;

        }
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
            fournisseur fr = new fournisseur();
            FrService frser = FrService.Instance();
            fr.raisonfr = trsoc.Text;
            fr.numerofr = (int)frser.findlastfr() + 1;
            fr.responsable = tres.Text;
            fr.tel = ttel.Text;
            fr.mobile = tmob.Text;
            fr.adress = tadr.Text;
            fr.ville = tville.Text;
            fr.codepostal = tcodepod.Text;
            fr.email = temail.Text;
            fr.web = tweb.Text;
            if (pictureEdit2.EditValue != null)
            {
                fr.image = ReadFile(textEdit6.Text);
            }
           
            frser.addfr(fr);

            MessageBox.Show("Ajouté avec succés");
            ClearAllForm(this);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {

        }

        private void NewFr_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void labelControl14_Click(object sender, EventArgs e)
        {


        }

        private void labelControl5_Click(object sender, EventArgs e)
        {

        }

        private void NewFr_Load(object sender, EventArgs e)
        {

        }

        private void labelControl11_Click(object sender, EventArgs e)
        {

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

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            pictureEdit2.EditValue = null;
            textEdit6.Text = null;
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
    }
}