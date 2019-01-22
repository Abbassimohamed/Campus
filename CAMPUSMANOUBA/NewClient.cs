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
using System.Threading;
namespace CAMPUSMANOUBA
{
    public partial class NewClient : DevExpress.XtraEditors.XtraForm
    {
        public NewClient()
        {
            InitializeComponent();
            labelControl2.Visible = false;

        }

        private static NewClient instance;

        public static NewClient Instance()
        {
            if (instance == null)

                instance = new NewClient();
            return instance;

        }
        private void simpleButton4_Click(object sender, EventArgs e)
        {
            ClearAllForm(this);
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
          client   cl = new client();
            ClientService cltservice = ClientService.Instance();
            int numclt = cltservice.findlastclient();

            cl.numerocl = numclt + 1;
            cl.raisonsoc = traissoc.Text;
            cl.resp = tresp.Text;
            cl.qualite = lookUpEdit1.EditValue.ToString();
            cl.tel = ttel.Text;
            cl.mobile = tmobile.Text;
            cl.adresse = tadr.Text;
            cl.codepostal = Convert.ToInt32(tcodepost.Text);
            cl.ville = tville.Text;
            cl.web = tweb.Text;
            cl.email = temail.Text;
            cl.fax = tfax.Text;
       
            cltservice.addclient(cl);
            MessageBox.Show("Ajouté avec succés");
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

        private void labelControl2_Click(object sender, EventArgs e)
        {

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