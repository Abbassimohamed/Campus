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
    public partial class Nouveau_agent : DevExpress.XtraEditors.XtraForm
    {
        public Nouveau_agent()
        {
            InitializeComponent();

        }
        private static Nouveau_agent instance;
        public static Nouveau_agent Instance()
        {
            if (instance == null)

                instance = new Nouveau_agent();
            return instance;

        }
        Agentservice agser = Agentservice.Instance();
     

        private void simpleButton5_Click(object sender, EventArgs e)
        {
           
            agent ag = new agent();
            ag.cin = tcin.Text;
            ag.nom = tnom.Text;
            ag.prenom = tpr.Text;
            ag.numtel = ttel.Text;
            ag.email = temail.Text;
            ag.specialite = tspec.Text;
            agser.addagent(ag);
            MessageBox.Show("Ajouté avec succées");


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

  
        private void simpleButton6_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = XtraMessageBox.Show("Sure de vouloir Annuler l'opération ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                ClearAllForm(this);

            }
        }

   
      
   

        private void simpleButton2_Click(object sender, EventArgs e)
        {
           
            
        }

        private void Nouveau_agent_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }

        private void simpleButton7_Click_1(object sender, EventArgs e)
        {
            DialogResult dialogResult = XtraMessageBox.Show("Sure de vouloir Annuler l'opération ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                ClearAllForm(this);

            }
        }

        private void simpleButton5_Click_1(object sender, EventArgs e)
        {

        }
    }
}