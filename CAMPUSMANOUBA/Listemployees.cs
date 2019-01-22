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
using BLL;
using DAL;
namespace CAMPUSMANOUBA
{
    public partial class Listemployees : DevExpress.XtraEditors.XtraForm
    {
        public Listemployees()
        {
            InitializeComponent();

        }
        private static Listemployees instance;
        public static Listemployees Instance()
        {
            if (instance == null)

                instance = new Listemployees();
            return instance;

        }
        Agentservice agser = Agentservice.Instance();
    

    
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

        private void simpleButton7_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = XtraMessageBox.Show("Sure de vouloir Annuler l'opération ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                ClearAllForm(this);

            }
        }

        private void simpleButton6_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = XtraMessageBox.Show("Sure de vouloir Annuler l'opération ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                ClearAllForm(this);

            }
        }

     

        private void Nouveau_agent_Load(object sender, EventArgs e)
        {
            fillgrid();
        }
        private void fillgrid()
        {
            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = agser.getallagent();
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Caption = "CIN N°";
            gridView1.Columns[2].Caption = "Nom";
            gridView1.Columns[3].Caption = "Prenom";
            gridView1.Columns[4].Caption = "Tel";
            gridView1.Columns[5].Caption = "email";
            gridView1.Columns[6].Caption = "Specialité";
        }

     

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = XtraMessageBox.Show("Sure de vouloir supprimer ces enregistrements ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                agent ag = new agent();
                ag = (agent)gridView1.GetRow(gridView1.FocusedRowHandle);
                agser.deleteagent(ag);
                MessageBox.Show("Supprimés avec succées");

            }
         
            
        }

        private void Nouveau_agent_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }
    }
}