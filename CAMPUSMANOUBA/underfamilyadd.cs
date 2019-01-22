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
namespace CAMPUSMANOUBA
{
    public partial class underfamilyadd : DevExpress.XtraEditors.XtraForm
    {
       
      
        public underfamilyadd()
        {
            InitializeComponent();
        }
        familleservice  sfser= familleservice.Instance();
        sousfamilleservice spserv = sousfamilleservice.Instance();
        private static int idfam=0;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            famille fm = new famille();
            fm =(famille) lookUpEdit1.GetSelectedDataRow();
            sousfamille sfamily = new sousfamille();

            sfamily.sousfamilledesign = textEdit1.Text;
            sfamily.familledesign = fm.familledesign;
            sfamily.familleid = fm.idfamille;
          if( spserv.addsousfamille(sfamily)==true)
            { List<sousfamille> sps = new List<sousfamille>();
                sps = spserv.getallsousfamilleByfamille(fm.idfamille);
                fillgrid(sps);
            }
            else
            {
                MessageBox.Show("déja existe");
            }
        }
        private void fillgrid(List<sousfamille> sp)
        {

            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = sp;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Caption = "Désignation";
        }

        private void Specialiteadd_Load(object sender, EventArgs e)
        {
            List<famille> sps = new List<famille>();
            sps = sfser.getallfamille();
            //List<sousfamille> sps = new List<sousfamille>();
            //sps = sfser.getallfamille();
            fillfamily();
            //fillgrid(sps);
        }


        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                famille family =(famille) gridView1.GetRow(gridView1.FocusedRowHandle);
                textEdit1.Text = family.familledesign;
                idfam = family.idfamille;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            if (idfam != 0)
            {
                sousfamille famil = new sousfamille();
                famil.idsousfamille= idfam;
                famil.sousfamilledesign = textEdit1.Text;
                spserv.updatesousfamille(famil);
                List<sousfamille> familles = new List<sousfamille>();
                familles = spserv.getallsousfamille();
                fillgrid(familles);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionnez une famille");
            }
           
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (idfam != 0)
            {
               
                spserv.deletesousfamily(idfam);
                List<sousfamille> sps = new List<sousfamille>();
                sps = spserv.getallsousfamille();
                fillgrid(sps);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionnez une famille");
            }
        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void labelControl5_Click(object sender, EventArgs e)
        {

        }
        public void fillfamily()
        {
            List<famille> sps = new List<famille>();
            sps = sfser.getallfamille();
            lookUpEdit1.Properties.ValueMember = "familledesign";
            lookUpEdit1.Properties.DisplayMember = "familledesign";
            lookUpEdit1.Properties.DataSource = sps;
            lookUpEdit1.Properties.PopulateColumns();
            lookUpEdit1.Properties.Columns["idfamille"].Visible = false;
            lookUpEdit1.Properties.Columns["familledesign"].Caption = "Famille";
        }

        private void lookUpEdit1_EditValueChanged(object sender, EventArgs e)
        {

            famille f = new famille();
            f = (famille)lookUpEdit1.GetSelectedDataRow();
            List<sousfamille> sfamille = new List<sousfamille>();
            sfamille = spserv.getallsousfamilleByfamille(f.idfamille);
            if(sfamille.LongCount() !=0)
            {
                fillgrid(sfamille);
            }
            
        }
    }
}