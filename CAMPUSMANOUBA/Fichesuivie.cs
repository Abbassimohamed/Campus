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
    public partial class Fichesuivie : DevExpress.XtraEditors.XtraForm
    {
        public Fichesuivie()
        {
            InitializeComponent();
        }
        Agentservice agser =Agentservice.Instance();
        fichesuivieservice fichser = fichesuivieservice.Instance();
        private static Fichesuivie instance;
        public static Fichesuivie Instance()
        {
            if (instance == null)

                instance = new Fichesuivie();
            return instance;

        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            try
            {
                agent ag = (agent)lookUpEdit1.GetSelectedDataRow();
                
                fichesuivi fichesui = new fichesuivi();
                fichesui.idagent = ag.idagent;
                fichesui.agent = ag.nom;
                fichesui.ndossier =textEdit1.Text;
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
            catch(Exception exc)
            { }
           
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            splashScreenManager1.ShowWaitForm();
            
            Home.newagent.MdiParent = Home.ActiveForm;
            Home.newagent.Show();
            Home.newagent.BringToFront();

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
        private void Fichesuivie_Load(object sender, EventArgs e)
        {
            fillagent();
        }

        private void AutoComplet()
        {

            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
            List<string> numeros = new List<string>();
            numeros = fichser.findndosiiers();
       
            foreach(string a in numeros)
            {
                collection.Add(a.ToString());
            }
            textEdit1.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textEdit1.MaskBox.AutoCompleteMode = AutoCompleteMode.Suggest;
            textEdit1.MaskBox.AutoCompleteCustomSource = collection;
        }

        private void textEdit1_TextChanged(object sender, EventArgs e)
        {
            AutoComplet();
        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton5_Click(object sender, EventArgs e)
        {
            try
            {
                agent ag = (agent)lookUpEdit1.GetSelectedDataRow();

                fichesuivi fs = new fichesuivi();
                fs.idfiche =Convert.ToInt32( textEdit2.Text);
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

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            //try
            //{
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
            //}
            //catch (Exception exc)
            //{ }
        }

        private void textEdit1_Validated(object sender, EventArgs e)
        {
            string ndossier = textEdit1.Text;
            List<fichesuivi> fis = new List<fichesuivi>();
            fis = fichser.findallfichesuiviebyndossier(ndossier);
            fillgrid(fis);



        }

        private void Fichesuivie_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }
    }
}