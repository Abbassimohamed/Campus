using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.IO;
using System.Timers;

using DAL;
using BLL;

namespace CAMPUSMANOUBA
{
    public partial class Fichepublication : DevExpress.XtraEditors.XtraForm
    {
        public Fichepublication()
        {
            InitializeComponent();
        }
        private static Fichepublication instance;
        PublicationService pubser = PublicationService.Instance();
        ficheauteurservice fichautser = ficheauteurservice.Instance();
        FrService frser = FrService.Instance();
        Agentservice agser = Agentservice.Instance();
        fichesuivieservice fichser = fichesuivieservice.Instance();
        AutService autser = new AutService();
        private static Nouveau_agent gestsoc;
        public static Fichepublication Instance()
        {
            if (instance == null)

                instance = new Fichepublication();
            return instance;

        }
        public static string filename, ext;
        public static long numBytes;
        byte[] imgdatapresentation, imgdataresum, imgdatapropocouv,imgdataphotoaut;
        byte[] ReadFile(string sPath)
             
        {
            //Initialize byte array with a null value initially.
            byte[] data = null;
            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(sPath);
            numBytes = fInfo.Length;
            filename = Path.GetFileName(sPath);
            ext = Path.GetExtension(filename);
            //Open FileStream to read file
            FileStream fStream = new FileStream(sPath, FileMode.Open, FileAccess.Read);
            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);
            data = br.ReadBytes((int)numBytes);
            return data;
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (tnom.Text == "" || tprenom.Text == "")

            {
                xtraTabControl1.SelectedTabPage = xtraTabPage2;
                if (tnom.Text == "")
                {
                    dxErrorProvider1.Dispose();
                    dxErrorProvider1.SetError(tnom, "Champ obligatoire");
                }
                 else if (tprenom.Text == "")
                {
                    dxErrorProvider1.Dispose();
                    dxErrorProvider1.SetError(tprenom, "Champ obligatoire");
                }
            }
           
            else
            {
                byte[] extention1, extention2, extention3, extention4;

                Image word, excel, powerpoint, access, pdf, txt, pic, other;
                word = (System.Drawing.Image)Properties.Resources.word;
                excel = (System.Drawing.Image)Properties.Resources.excel;
                powerpoint = (System.Drawing.Image)Properties.Resources.powerpoint;
                access = (System.Drawing.Image)Properties.Resources.access;
                pdf = (System.Drawing.Image)Properties.Resources.pdf;
                txt = (System.Drawing.Image)Properties.Resources.txt;
                pic = (System.Drawing.Image)Properties.Resources.pic;
                other = (System.Drawing.Image)Properties.Resources.format_inconnu;

                Dossierpublication newdossier = new Dossierpublication();
                newdossier.ndossier = tndossier.Text;
                newdossier.titre = ttitre.Text;
                newdossier.discipline = comboBoxdiscipline.Text;
                newdossier.datedepo = dateEditdepot.DateTime;
                if (checkBoxcd.Checked == true)
                {
                    newdossier.support = "CD";
                }
                if (checkBoxtp.Checked == true)
                {
                    newdossier.support = "Tirage papier";
                }
                newdossier.nbpage = Convert.ToInt32(Tnbpage.Text);
                newdossier.format = comboBoxformat.Text;

                newdossier.formatpapierinter = comboBox1.Text;

                if (toggleSwitch6.IsOn)
                {
                    newdossier.nbpagecolor = Convert.ToInt32(tnbpgcolor.Text);
                }
                else
                {
                    newdossier.nbpagecolor = null;
                }
                newdossier.couverture = comboBox2.Text;
                newdossier.finition = tfinition.Text;
                newdossier.nboncommande = Convert.ToInt32(tnbbcmd.Text);
                fournisseur fr = (fournisseur)lookimp.GetSelectedDataRow();
                newdossier.idimp = fr.codefr;
                newdossier.imp = fr.raisonfr;
                if (toggleSwitch5.IsOn)
                {
                    newdossier.reedition = true;
                    newdossier.nedition = Convert.ToInt32(tnedit.Text);
                }
                else
                {
                    newdossier.reedition = false;
                    newdossier.nedition = null;
                }

                newdossier.datecommande = dateEditcommande.DateTime;
                newdossier.tirage = ttirage.Text;
                if (togglecoedit.IsOn)
                {
                    newdossier.coedition = true;
                    newdossier.coediteur = tcoediteur.Text;
                    if (toggleSwitch7.IsOn)
                    {
                        newdossier.contratcoedition = true;
                    }
                    else
                    {
                        newdossier.contratcoedition = false;
                    }

                    if (toggleSwitch8.IsOn)
                    {
                        newdossier.bcmdcoedit = true;
                    }
                    else
                    {
                        newdossier.bcmdcoedit = false;
                    }

                }
                else
                {
                    newdossier.coedition = true;
                }


                ficheauteur fichaut = new ficheauteur();
                fichaut.nom = tnom.Text;
                fichaut.prenom = tprenom.Text;
                fichaut.telephone = ttel.Text;
                fichaut.fax = tfax.Text;
                fichaut.email = temail.Text;
                ImageConverter converter = new ImageConverter();
                if (toggleSwitchprlivre.IsOn)
                {
                    fichaut.presentation = true;
                    imgdatapresentation = ReadFile(tprlivre.Text);

                    if (ext == ".pdf")
                    {
                        extention1 = (byte[])converter.ConvertTo(pdf, typeof(byte[]));
                    }
                    else if (ext == ".doc" || ext == ".docx")
                    {
                        extention1 = (byte[])converter.ConvertTo(word, typeof(byte[]));
                    }
                    else if (ext == ".xls" || ext == ".xlsx")
                    {
                        extention1 = (byte[])converter.ConvertTo(excel, typeof(byte[]));
                    }
                    else if (ext == ".ppt" || ext == ".pptx")
                    {
                        extention1 = (byte[])converter.ConvertTo(powerpoint, typeof(byte[]));
                    }
                    else if (ext == ".mdb" || ext == ".accdb")
                    {
                        extention1 = (byte[])converter.ConvertTo(access, typeof(byte[]));
                    }
                    else if (ext == ".txt")
                    {
                        extention1 = (byte[])converter.ConvertTo(txt, typeof(byte[]));
                    }
                    else if (ext == ".gif" || ext == ".jpg" || ext == ".JPEG" || ext == ".png" || ext == ".bmp")
                    {
                        extention1 = (byte[])converter.ConvertTo(pic, typeof(byte[]));
                    }
                    else
                    {
                        extention1 = (byte[])converter.ConvertTo(other, typeof(byte[]));
                    }
                    fichaut.extpr = extention1;

                    fichaut.fichepresentation = imgdatapresentation;
                }
                else
                {
                    fichaut.presentation = false;
                }
                if (toggleSwitchresume.IsOn)
                {
                    fichaut.biographie = true;
                    imgdataresum = ReadFile(tresum.Text);

                    if (ext == ".pdf")
                    {
                        extention2 = (byte[])converter.ConvertTo(pdf, typeof(byte[]));
                    }
                    else if (ext == ".doc" || ext == ".docx")
                    {
                        extention2 = (byte[])converter.ConvertTo(word, typeof(byte[]));
                    }
                    else if (ext == ".xls" || ext == ".xlsx")
                    {
                        extention2 = (byte[])converter.ConvertTo(excel, typeof(byte[]));
                    }
                    else if (ext == ".ppt" || ext == ".pptx")
                    {
                        extention2 = (byte[])converter.ConvertTo(powerpoint, typeof(byte[]));
                    }
                    else if (ext == ".mdb" || ext == ".accdb")
                    {
                        extention2 = (byte[])converter.ConvertTo(access, typeof(byte[]));
                    }
                    else if (ext == ".txt")
                    {
                        extention2 = (byte[])converter.ConvertTo(txt, typeof(byte[]));
                    }
                    else if (ext == ".gif" || ext == ".jpg" || ext == ".JPEG" || ext == ".png" || ext == ".bmp")
                    {
                        extention2 = (byte[])converter.ConvertTo(pic, typeof(byte[]));
                    }
                    else
                    {
                        extention2 = (byte[])converter.ConvertTo(other, typeof(byte[]));
                    }
                    fichaut.extbiog = extention2;
                    fichaut.resumebiographie = imgdataresum;
                }
                else
                {
                    fichaut.biographie = false;
                }
                if (toggleSwitchprop.IsOn)
                {
                    fichaut.couverture = true;
                    imgdatapropocouv = ReadFile(tpropcouv.Text);

                    if (ext == ".pdf")
                    {
                        extention3 = (byte[])converter.ConvertTo(pdf, typeof(byte[]));
                    }
                    else if (ext == ".doc" || ext == ".docx")
                    {
                        extention3 = (byte[])converter.ConvertTo(word, typeof(byte[]));
                    }
                    else if (ext == ".xls" || ext == ".xlsx")
                    {
                        extention3 = (byte[])converter.ConvertTo(excel, typeof(byte[]));
                    }
                    else if (ext == ".ppt" || ext == ".pptx")
                    {
                        extention3 = (byte[])converter.ConvertTo(powerpoint, typeof(byte[]));
                    }
                    else if (ext == ".mdb" || ext == ".accdb")
                    {
                        extention3 = (byte[])converter.ConvertTo(access, typeof(byte[]));
                    }
                    else if (ext == ".txt")
                    {
                        extention3 = (byte[])converter.ConvertTo(txt, typeof(byte[]));
                    }
                    else if (ext == ".gif" || ext == ".jpg" || ext == ".JPEG" || ext == ".png" || ext == ".bmp")
                    {
                        extention3 = (byte[])converter.ConvertTo(pic, typeof(byte[]));
                    }
                    else
                    {
                        extention3 = (byte[])converter.ConvertTo(other, typeof(byte[]));
                    }
                    fichaut.extphoto = extention3;
                    fichaut.propositioncouvert = imgdatapropocouv;

                }
                else
                {
                    fichaut.couverture = false;
                }
                if (toggleSwitchphoto.IsOn)
                {
                    fichaut.photoauteur = true;
                    imgdataphotoaut = ReadFile(tphotoaut.Text);

                    if (ext == ".pdf")
                    {
                        extention4 = (byte[])converter.ConvertTo(pdf, typeof(byte[]));

                    }
                    else if (ext == ".doc" || ext == ".docx")
                    {
                        extention4 = (byte[])converter.ConvertTo(word, typeof(byte[]));
                    }
                    else if (ext == ".xls" || ext == ".xlsx")
                    {
                        extention4 = (byte[])converter.ConvertTo(excel, typeof(byte[]));
                    }
                    else if (ext == ".ppt" || ext == ".pptx")
                    {
                        extention4 = (byte[])converter.ConvertTo(powerpoint, typeof(byte[]));
                    }
                    else if (ext == ".mdb" || ext == ".accdb")
                    {
                        extention4 = (byte[])converter.ConvertTo(access, typeof(byte[]));
                    }
                    else if (ext == ".txt")
                    {
                        extention4 = (byte[])converter.ConvertTo(txt, typeof(byte[]));
                    }
                    else if (ext == ".gif" || ext == ".jpg" || ext == ".JPEG" || ext == ".png" || ext == ".bmp")
                    {
                        extention4 = (byte[])converter.ConvertTo(pic, typeof(byte[]));
                    }
                    else
                    {
                        extention4 = (byte[])converter.ConvertTo(other, typeof(byte[]));
                    }
                    fichaut.extcouv = extention4;
                }
                else
                {
                    fichaut.photoauteur = false;

                }
                fichaut.photo = imgdataphotoaut;
                pubser.addfichepub(newdossier);
                fichautser.addficheauteur(fichaut);
                MessageBox.Show("Fiche de publication ajoutée avec succées");
                ClearAllForm(this);
            }

        }
        private void simpleButton3_Click(object sender, EventArgs e)
        {

        }

        private void panelControl4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void labelControl34_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Joindre un fichier";
            if (ofd.CheckFileExists == true)
            {
                ofd.Filter = "";
                ofd.ShowDialog();
                if (ofd.FileName != "")
                {
                    // 5MO max pour un fichier
                    FileInfo fi = new FileInfo(ofd.FileName);
                    if (fi.Length < 5000000)
                    {
                        tprlivre.Text = ofd.FileName;
                    }
                    else
                    {
                        XtraMessageBox.Show("Le fichier selectionné dépasse la taille autorisée(5 MO)", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void simpleButton8_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Joindre un fichier";
            if (ofd.CheckFileExists == true)
            {
                ofd.Filter = "";
                ofd.ShowDialog();
                if (ofd.FileName != "")
                {
                    // 5MO max pour un fichier
                    FileInfo fi = new FileInfo(ofd.FileName);
                    if (fi.Length < 5000000)
                    {
                        tresum.Text = ofd.FileName;
                    }
                    else
                    {
                        XtraMessageBox.Show("Le fichier selectionné dépasse la taille autorisée(5 MO)", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Joindre un fichier";
            if (ofd.CheckFileExists == true)
            {
                ofd.Filter = "";
                ofd.ShowDialog();
                if (ofd.FileName != "")
                {
                    // 5MO max pour un fichier
                    FileInfo fi = new FileInfo(ofd.FileName);
                    if (fi.Length < 5000000)
                    {
                        tpropcouv.Text = ofd.FileName;
                    }
                    else
                    {
                        XtraMessageBox.Show("Le fichier selectionné dépasse la taille autorisée(5 MO)", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void simpleButton10_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Joindre un fichier";
            if (ofd.CheckFileExists == true)
            {
                ofd.Filter = "";
                ofd.ShowDialog();
                if (ofd.FileName != "")
                {
                    // 5MO max pour un fichier
                    FileInfo fi = new FileInfo(ofd.FileName);
                    if (fi.Length < 5000000)
                    {
                        tphotoaut.Text = ofd.FileName;
                    }
                    else
                    {
                        XtraMessageBox.Show("Le fichier selectionné dépasse la taille autorisée(5 MO)", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void toggleSwitch1_Toggled(object sender, EventArgs e)
        {
            if (toggleSwitchprlivre.IsOn)
            {
                tprlivre.Enabled = true;
                simpleButton7.Enabled = true;
            }
            else
            {
                tprlivre.Enabled = false;
                simpleButton7.Enabled = false;
            }
        }

        private void toggleSwitch5_Toggled(object sender, EventArgs e)
        {
            if (toggleSwitch5.IsOn)
            {
                tnedit.Enabled = true;
                labelControl20.Enabled = true;
            }
            else
            {
                tnedit.Enabled = false;
                labelControl20.Enabled = false;
            }
        }

        private void toggleSwitch6_Toggled(object sender, EventArgs e)
        {
            if (togglecoedit.IsOn)
            {
                tcoediteur.Enabled = true;
                toggleSwitch8.Enabled = true;
                toggleSwitch7.Enabled = true;
                labelControl21.Enabled = true;
                labelControl22.Enabled = true;
                labelControl23.Enabled = true;
            }
            else
            {
                tcoediteur.Enabled = false;
                toggleSwitch8.Enabled = false;
                toggleSwitch7.Enabled = false;
                labelControl21.Enabled = false;
                labelControl22.Enabled = false;
                labelControl23.Enabled = false;
            }
        }

        private void toggleSwitch2_Toggled(object sender, EventArgs e)
        {
            if (toggleSwitchresume.IsOn)
            {
                tresum.Enabled = true;
                simpleButton8.Enabled = true;
            }
            else
            {
                tresum.Enabled = false;
                simpleButton8.Enabled = false;
            }
        }

        private void Fichepublication_Load(object sender, EventArgs e)
        {
            Fournisseurs();
            //fillagent();
        }
        private void AutoComplet()
        {

            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            List<string> numeros = new List<string>();
            numeros = fichser.findndosiiers();

            foreach (string a in numeros)
            {
                collection.Add(a.ToString());
            }
            textEdit1.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textEdit1.MaskBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            textEdit1.MaskBox.AutoCompleteCustomSource = collection;
        }
        private void toggleSwitch3_Toggled(object sender, EventArgs e)
        {
            if (toggleSwitchprop.IsOn)
            {
                tpropcouv.Enabled = true;
                simpleButton9.Enabled = true;
            }
            else
            {
                tpropcouv.Enabled = false;
                simpleButton9.Enabled = false;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = XtraMessageBox.Show("Sure de vouloir Annuler l'opération ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                ClearAllForm(this);

            }
        }

        private void tndossier_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void toggleSwitch4_Toggled(object sender, EventArgs e)
        {
            if (toggleSwitchphoto.IsOn)
            {
                tphotoaut.Enabled = true;
                simpleButton10.Enabled = true;
            }
            else
            {
                tphotoaut.Enabled = false;
                simpleButton10.Enabled = false;
            }
        }

        private void tnom_TextChanged(object sender, EventArgs e)
        {
            try
            {
                AutoCompletnom();
            }
            catch(Exception exc)
            { }
            
        }

        private void toggleSwitch6_Toggled_1(object sender, EventArgs e)
        {
            if (toggleSwitch6.IsOn)
            {
                tnbpgcolor.Enabled = true;
                labelControl11.Enabled = true;
            }
            else
            {
                tnbpgcolor.Enabled = false;
                labelControl11.Enabled = false;
            }
        }

        private void tprenom_TextChanged(object sender, EventArgs e)
        {
            AutoCompletprenom();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            //splashScreenManager1.ShowWaitForm();

            //Home.newagent.MdiParent = Home.ActiveForm;
            //Home.newagent.Show();
            //Home.newagent.BringToFront();

            //splashScreenManager1.CloseWaitForm();
            splashScreenManager1.ShowWaitForm();
            gestsoc = Nouveau_agent.Instance();
            //gestsoc.MdiParent = this;
            //gestsoc.Show();
            //gestsoc.BringToFront();
            //splashScreenManager1.CloseWaitForm();

            if (gestsoc.WindowState == FormWindowState.Minimized)
            {
                gestsoc.WindowState = FormWindowState.Maximized;
            }
            else

                gestsoc.Show();

            splashScreenManager1.CloseWaitForm();
        }
        private void fillagent()
        {
            List<agent> agents = new List<agent>();
            agents = agser.getallagent();
            lookUpEdit1.Properties.ValueMember = "idagent";
            lookUpEdit1.Properties.DisplayMember = "nom";
            lookUpEdit1.Properties.DataSource = agents;
            lookUpEdit1.Properties.PopulateColumns();
            lookUpEdit1.Properties.Columns["idagent"].Visible = false;
            lookUpEdit1.Properties.Columns["cin"].Visible = false;
            lookUpEdit1.Properties.Columns["nom"].Caption = "Nom";
            lookUpEdit1.Properties.Columns["prenom"].Caption = "Prenom";
            lookUpEdit1.Properties.Columns["numtel"].Visible = false;
            lookUpEdit1.Properties.Columns["email"].Visible = false;
            lookUpEdit1.Properties.Columns["specialite"].Visible = false;
        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            try
            {
                agent ag = (agent)lookUpEdit1.GetSelectedDataRow();

                fichesuivi fichesui = new fichesuivi();
                fichesui.idagent = ag.idagent;
                fichesui.agent = ag.nom;
                fichesui.ndossier = textEdit1.Text;
                fichesui.datereception = dateEdit1.DateTime;
                fichesui.datemaj = dateEdit2.DateTime;
                fichesui.observation = memoEdit1.Text;
                fichser.addfichesuivie(fichesui);
                MessageBox.Show("Ajouté avec succées");
                string ndossier = textEdit1.Text;
                List<fichesuivi> fis = new List<fichesuivi>();
                fis = fichser.findallfichesuiviebyndossier(ndossier);
                fillgrid(fis);

            }
            catch (Exception exc)
            { }

        }

        private void simpleButton11_Click(object sender, EventArgs e)
        {
            try
            {
                agent ag = (agent)lookUpEdit1.GetSelectedDataRow();

                fichesuivi fs = new fichesuivi();
                fs.idfiche = Convert.ToInt32(textEdit2.Text);
                fs.idagent = ag.idagent;
                fs.agent = ag.nom;
                fs.datereception = dateEdit1.DateTime;
                fs.datemaj = dateEdit2.DateTime;
                fs.observation = memoEdit1.Text;
                fs.ndossier = textEdit1.Text;

                fichser.updatefichesuivi(fs);
                MessageBox.Show("Mise a jour effectuée avec succées");
                string ndossier = textEdit1.Text;
                List<fichesuivi> fis = new List<fichesuivi>();
                fis = fichser.findallfichesuiviebyndossier(ndossier);
                fillgrid(fis);
            }
            catch (Exception exc)
            { }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            try
            {
                agent ag = (agent)lookUpEdit1.GetSelectedDataRow();

            fichesuivi fs = new fichesuivi();
            fs.idfiche = Convert.ToInt32(textEdit2.Text);
            fs.idagent = ag.idagent;
            fs.agent = ag.nom;
            fs.datereception = dateEdit1.DateTime;
            fs.datemaj = dateEdit2.DateTime;
            fs.observation = memoEdit1.Text;

            fichser.deletefile(fs);
            MessageBox.Show("Supprimé avec succées");
            string ndossier = textEdit1.Text;
            List<fichesuivi> fis = new List<fichesuivi>();
            fis = fichser.findallfichesuiviebyndossier(ndossier);
            fillgrid(fis);
        }
            catch (Exception exc)
            { }
}
        private void fillgrid(List<fichesuivi> fichesuivis)
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = fichesuivis;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Caption = "N° dossier";
            gridView1.Columns[2].Visible = false;
            gridView1.Columns[3].Caption = "Agent";
            gridView1.Columns[4].Visible = false;
            gridView1.Columns[5].Caption = "Observation";
            gridView1.Columns[6].Caption = "Date ";


        }

        private void xtraTabControl1_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            if (xtraTabControl1.SelectedTabPage == xtraTabPage3)
            {
                
                if(tndossier.Text!=null)
                {
                    textEdit1.Text = tndossier.Text;
                    List<fichesuivi> fis = new List<fichesuivi>();
                    fis = fichser.findallfichesuiviebyndossier(tndossier.Text);
                    fillgrid(fis);
                }
                fillagent();
            }
        }

        private void gridView1_Click(object sender, EventArgs e)
        {
            if (gridView1.RowCount != 0)
            {
                fichesuivi fs = (fichesuivi)gridView1.GetRow(gridView1.FocusedRowHandle);
                textEdit1.Text = fs.ndossier;
                lookUpEdit1.EditValue = fs.idagent;
                dateEdit1.DateTime = Convert.ToDateTime(fs.datereception);
                dateEdit2.DateTime = Convert.ToDateTime(fs.datemaj);
                textEdit2.Text = fs.idfiche.ToString();
                memoEdit1.Text = fs.observation;


            }
        }

        private void textEdit1_TextChanged(object sender, EventArgs e)
        {
            AutoComplet();
        }

        private void Fichepublication_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void simpleButton3_Click_1(object sender, EventArgs e)
        {

        }

        private void Fournisseurs()
        {
            //get All article
            lookimp.Properties.DataSource = null;
            List<fournisseur> Allfrs = new List<fournisseur>();
            Allfrs = frser.getallfr();

            lookimp.Properties.ValueMember = "numerofr";
            lookimp.Properties.DisplayMember = "raisonfr";
            lookimp.Properties.DataSource = Allfrs;
            lookimp.Properties.PopulateColumns();
            lookimp.Properties.Columns[0].Visible = false;
            lookimp.Properties.Columns[1].Caption = "Code fournisseur";
            lookimp.Properties.Columns[2].Caption = "Raison sociale";
            lookimp.Properties.Columns[3].Visible = false;
            lookimp.Properties.Columns[4].Visible = false;
            lookimp.Properties.Columns[5].Visible = false;
            lookimp.Properties.Columns[6].Visible = false;
            lookimp.Properties.Columns[7].Visible = false;
            lookimp.Properties.Columns[8].Visible = false;
            lookimp.Properties.Columns[9].Visible = false;
            lookimp.Properties.Columns[10].Visible = false;
            lookimp.Properties.Columns[11].Visible = false;




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
     
        private void AutoCompletnom()
        {

            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            List<auteur> numeros = new List<auteur>();
            numeros = autser.getallauthor() ;

            foreach (auteur cl in numeros)
            {
                collection.Add(cl.nom.ToString());
            }
            tnom.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tnom.MaskBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            tnom.MaskBox.AutoCompleteCustomSource = collection;
        }
        private void AutoCompletprenom()
        {

            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            List<auteur> numeros = new List<auteur>();
            numeros = autser.getallauthor();

            foreach (auteur cl in numeros)
            {
                collection.Add(cl.nom.ToString());
            }
            tnom.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            tnom.MaskBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            tnom.MaskBox.AutoCompleteCustomSource = collection;
        }

    }


}