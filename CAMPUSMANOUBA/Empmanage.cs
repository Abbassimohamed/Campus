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
    public partial class Empmanage : DevExpress.XtraEditors.XtraForm
    {
       
      
        public Empmanage()
        {
            InitializeComponent();
        }
        emplacementService empservice = emplacementService.Instance();
        private static int idemp=0;
        private void simpleButton1_Click(object sender, EventArgs e)
        {
            emplacement emplace = new emplacement();

            emplace.empdesign = textEdit1.Text;
          if( empservice.addemp(emplace)==true)
            { List<emplacement> listemp = new List<emplacement>();
                listemp = empservice.getallemp();
                fillgrid(listemp);
            }
            else
            {
                MessageBox.Show("déja existe");
            }
        }
        private void fillgrid(List<emplacement> sp)
        {

            gridControl1.DataSource = null;
            gridView1.Columns.Clear();
            gridControl1.DataSource = sp;
            gridView1.Columns[0].Visible = false;
            gridView1.Columns[1].Caption = "Désignation";
        }

        private void Specialiteadd_Load(object sender, EventArgs e)
        {
            List<emplacement> sps = new List<emplacement>();
          sps=  empservice.getallemp();
            fillgrid(sps);
        }


        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {

            int count = gridView1.DataRowCount;
            if (count != 0 && gridView1.FocusedRowHandle != DevExpress.XtraGrid.GridControl.AutoFilterRowHandle)
            {
                emplacement emplace =(emplacement) gridView1.GetRow(gridView1.FocusedRowHandle);
                textEdit1.Text = emplace.empdesign;
                idemp = emplace.idemp;
            }
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

            if (idemp != 0)
            {
                emplacement emp = new emplacement();
                emp.idemp= idemp;
                emp.empdesign = textEdit1.Text;
                empservice.updateemp(emp);
                List<emplacement> emps = new List<emplacement>();
                emps = empservice.getallemp();
                fillgrid(emps);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionnez une famille");
            }
           
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (idemp != 0)
            {
               
                empservice.deleteemp(idemp);
                List<emplacement> sps = new List<emplacement>();
                sps = empservice.getallemp();
                fillgrid(sps);
            }
            else
            {
                MessageBox.Show("Veuillez sélectionnez une famille");
            }
        }
    }
}