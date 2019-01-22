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
    public partial class GestionSociete : DevExpress.XtraEditors.XtraForm
    {
        public GestionSociete()
        {
            InitializeComponent();
            radioGroup1.SelectedIndex = 1;
        }
        OpenFileDialog ofd = new OpenFileDialog();
        SocieteService socserv = new SocieteService();
        BankService bankser = new BankService();
        private static List<Banque> banks = new List<Banque>();
        private static GestionSociete instance;
        public static GestionSociete Instance()
        {
            if (instance == null)
                instance = new GestionSociete();
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
            trsoc.Enabled = false;
            tadr.Enabled = false;
            tcodepod.Enabled = false;
            ttel.Enabled = false;
            tmob.Enabled = false;
            tville.Enabled = false;
            tfax.Enabled = false;
            temail.Enabled = false;
            tweb.Enabled = false;
            tlogo.Enabled = false;
            pictureEdit2.Enabled = false;
            tformjuridiq.Enabled = false;
            tres.Enabled = false;
            tmatfisc.Enabled = false;
            radioGroup1.Enabled = false;
            tnumtva.Enabled = false;
            tactivity.Enabled = false;
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            societe soc = new societe();
            SocieteService socser = SocieteService.Instance();
            soc.rsoc = trsoc.Text;
            soc.adresse = tadr.Text;
            soc.codpost = tcodepod.Text;
            soc.tel = ttel.Text;
            soc.mobile = tmob.Text;      
            soc.ville = tville.Text;
            soc.fax = tfax.Text;
            soc.mail = temail.Text;
            soc.siteweb = tweb.Text;
            if (pictureEdit2.EditValue != null)
            {
                soc.logo = ReadFile(tlogo.Text);
            }
            if(radioGroup1.SelectedIndex==0)
            {
                soc.assujetitva = "false";
                
            }
            soc.formejuridique = tformjuridiq.Text;
            soc.nomresp = tres.Text;
            soc.matfisc = tmatfisc.Text;
            try
            {
                soc.numtva = Convert.ToDouble(tnumtva.Text);
            }
            catch(Exception exc)
            { }
            soc.activite = tactivity.Text;

            socser.addsociete(soc);

            MessageBox.Show("Ajouté avec succés");
            //ClearAllForm(this);
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            trsoc.Enabled = true;
            tadr.Enabled = true;
            tcodepod.Enabled = true;
            ttel.Enabled = true;
            tmob.Enabled = true;
            tville.Enabled = true;
            tfax.Enabled = true;
            temail.Enabled = true;
            tweb.Enabled = true;
            tlogo.Enabled = true;
            pictureEdit2.Enabled = true;
            tformjuridiq.Enabled = true;
            tres.Enabled = true;
            tmatfisc.Enabled = true;
            radioGroup1.Enabled = true;
            tnumtva.Enabled = true;
            tactivity.Enabled = true;
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
            try
            {
                societe soc = new societe();
                soc = socserv.findlastfr();
                trsoc.Text = soc.rsoc;
                tadr.Text = soc.adresse ;
                tcodepod.Text =soc.codpost ;
                ttel.Text=soc.tel ;
                tmob.Text = soc.mobile;
                tville.Text = soc.ville;
                tfax.Text = soc.fax;
                temail.Text=soc.mail;
                tweb.Text=soc.siteweb;
                trsoc.Enabled = false;
                tadr.Enabled = false;
                tcodepod.Enabled = false;
                ttel.Enabled = false;
                tmob.Enabled = false;
                tville.Enabled = false;
                tfax.Enabled = false;
                temail.Enabled = false;
                tweb.Enabled = false;
                if (soc.logo==null)
                {
                    tlogo.Text = null;
                    tlogo.Enabled = false;
                }
                else
                {
                    pictureEdit2.Image = Image.FromFile(tlogo.Text);
                    pictureEdit2.Enabled = false;
                }
            
                if (soc.assujetitva == "false")
                {
                    radioGroup1.SelectedIndex = 0;

                }
                else
                {
                    radioGroup1.SelectedIndex = 1;
                }
                radioGroup1.Enabled = false;
                tformjuridiq.Text=soc.formejuridique;
                tres.Text=soc.nomresp;
                tmatfisc.Text=soc.matfisc;
                tformjuridiq.Enabled = false;
                tres.Enabled = false;
                tmatfisc.Enabled = false;
                try
                {
                    tnumtva.Text = soc.numtva.ToString();
                    tnumtva.Enabled=false;
                }
                catch (Exception exc)
                { }
                tactivity.Text = soc.activite;
                tactivity.Enabled=false;
            }
            catch(Exception exc)
            { }
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
                    tlogo.Text = ofd.FileName;
                    pictureEdit2.Image = Image.FromFile(ofd.FileName);
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            pictureEdit2.EditValue = null;
            tlogo.Text = null;
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

        private void gridControl1_Click(object sender, EventArgs e)
        {
            int count = 0;
            count = gridView1.RowCount;
            if(count!=0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                Banque bnq = (Banque)gridView1.GetRow(gridView1.FocusedRowHandle);

                tbanque.Text = bnq.nombanque;
                tsolde.Text =  bnq.soldeinitial.ToString();
                trib.Text = bnq.rib;
                tcodebnq.Text = bnq.idbanque.ToString();
            }
           
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            Banque bnq = new Banque();
            bnq.nombanque =tbanque.Text;
            bnq.rib = trib.Text;
            bnq.soldeinitial =Convert.ToDouble(tsolde.Text);
            bankser.addbanque(bnq);
            banks = bankser.getbanks();
            fillgrid(banks);
        }
        private void fillgrid(List<Banque> banks)
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = banks;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Caption = "Banque";
            gridView1.Columns[2].Caption = "R.I.B";
            gridView1.Columns[3].Caption = "Solde initial";
        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("le compte bancaire va etre modifier veuillez confirmer l'opération ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                Banque bnq = new Banque();
                bnq.idbanque =Convert.ToInt32( tcodebnq.Text);
                bnq.nombanque = tbanque.Text;
                bnq.soldeinitial =Convert.ToDouble( tsolde.Text);
                bnq.rib = trib.Text;
                bankser.updatebank(bnq);
                banks = bankser.getbanks();
                fillgrid(banks);
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }

        private void tileItem1_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage1;

        }

        private void tileItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            xtraTabControl1.SelectedTabPage = xtraTabPage2;
        }

        private void xtraTabControl1_Click(object sender, EventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == xtraTabPage2)
            {
                banks = bankser.getbanks();
                fillgrid(banks);

            }

        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("le compte bancaire va etre supprimer veuillez confirmer l'opération ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                bankser.deletebank(Convert.ToInt32( tcodebnq.Text));
                banks = bankser.getbanks();
                fillgrid(banks);
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }
    }
}