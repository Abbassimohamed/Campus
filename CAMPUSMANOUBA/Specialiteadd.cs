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
    public partial class Specialiteadd : DevExpress.XtraEditors.XtraForm
    {
       
      
        public Specialiteadd()
        {
            InitializeComponent();
        }
        SpecialityService spserv = SpecialityService.Instance();
        private static int idspec=0;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            specialite sp = new specialite();
       
            sp.designation = textEdit1.Text;
          if( spserv.addspeciality(sp)==true)
            { List<specialite> sps = new List<specialite>();
                sps = spserv.getallspeciality();
                fillgrid(sps);
            }
            else
            {
                MessageBox.Show("déja existe");
            }
        }
        private void fillgrid(List<specialite> sp)
        {

            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = sp;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Caption = "Désignation";
        }

        private void Specialiteadd_Load(object sender, EventArgs e)
        {
            List<specialite> sps = new List<specialite>();
          sps=  spserv.getallspeciality();
            fillgrid(sps);
        }


        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                specialite sp =(specialite) gridView1.GetRow(gridView1.FocusedRowHandle);
                textEdit1.Text = sp.designation;
                idspec = sp.idsp;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            if (idspec != 0)
            {
                specialite sp = new specialite();
                sp.idsp = idspec;
                sp.designation = textEdit1.Text;
                spserv.updatespec(sp);
                List<specialite> sps = new List<specialite>();
                sps = spserv.getallspeciality();
                fillgrid(sps);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionnez une spécialité");
            }
           
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (idspec != 0)
            {
               
                spserv.deletesp(idspec);
                List<specialite> sps = new List<specialite>();
                sps = spserv.getallspeciality();
                fillgrid(sps);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionnez une spécialité");
            }
        }
    }
}