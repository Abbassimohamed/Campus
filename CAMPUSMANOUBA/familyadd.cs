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
    public partial class familyadd : DevExpress.XtraEditors.XtraForm
    {
       
      
        public familyadd()
        {
            InitializeComponent();
        }
        familleservice spserv = familleservice.Instance();
        private static int idfam=0;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            famille family = new famille();

            family.familledesign = textEdit1.Text;
          if( spserv.addfamille(family)==true)
            { List<famille> sps = new List<famille>();
                sps = spserv.getallfamille();
                fillgrid(sps);
            }
            else
            {
                MessageBox.Show("déja existe");
            }
        }
        private void fillgrid(List<famille> sp)
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
          sps=  spserv.getallfamille();
            fillgrid(sps);
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
                famille famil = new famille();
                famil.idfamille= idfam;
                famil.familledesign = textEdit1.Text;
                spserv.updatefamille(famil);
                List<famille> familles = new List<famille>();
                familles = spserv.getallfamille();
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
               
                spserv.deletefamily(idfam);
                List<famille> sps = new List<famille>();
                sps = spserv.getallfamille();
                fillgrid(sps);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionnez une famille");
            }
        }
    }
}