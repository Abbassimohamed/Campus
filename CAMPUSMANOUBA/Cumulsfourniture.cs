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
    public partial class Cumulsfourniture : DevExpress.XtraEditors.XtraForm
    {
        public Cumulsfourniture()
        {
            InitializeComponent();
        }
        private static Cumulsfourniture instance;
        public static Cumulsfourniture Instance()
        {
            if (instance == null)

                instance = new Cumulsfourniture();
            return instance;

        }
        public static List<Fourniture> darticle = new List<Fourniture>();
        FournitureService artser = FournitureService.Instance();
        private void 
            
            
            _Load(object sender, EventArgs e)
        {
            darticle = artser.getallart();
            articles2();
        }
        private void articles2()
        {
            //get All article
            lookUpEdit3.Properties.DataSource = null;
            lookUpEdit3.Properties.ValueMember = "codefour";
            lookUpEdit3.Properties.DisplayMember = "designation";
            lookUpEdit3.Properties.DataSource = darticle;
            lookUpEdit3.Properties.PopulateColumns();
            lookUpEdit3.Properties.Columns["idfour"].Visible = false;
            lookUpEdit3.Properties.Columns["codefour"].Caption = "Code article";
            lookUpEdit3.Properties.Columns["famille"].Visible = false;
            lookUpEdit3.Properties.Columns["idfamille"].Visible = false;
            lookUpEdit3.Properties.Columns["sousfamille"].Visible = false;
            lookUpEdit3.Properties.Columns["idsousfamille"].Visible = false;
            lookUpEdit3.Properties.Columns["designation"].Caption = "Designation";
            lookUpEdit3.Properties.Columns["fournisseur"].Visible = false;
            lookUpEdit3.Properties.Columns["quantitenstock"].Visible = false;
            lookUpEdit3.Properties.Columns["prixAchat"].Visible = false;
            lookUpEdit3.Properties.Columns["image"].Visible = false;
            lookUpEdit3.Properties.Columns["seuil"].Visible = false;

        }
        private void lookUpEdit3_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                dateEdit2.EditValue = null;
                dateEdit3.EditValue = null;
                Fourniture art = (Fourniture)lookUpEdit3.GetSelectedDataRow();
                List<Cumul> cumuls = artser.getallcumulbyfourniture(art.codefour);
                fillgrid(cumuls);
            }
            catch (Exception exc)
            { }

        }
        private void fillgrid(List<Cumul> piececumuls)
        {
            gridControl4.DataSource = null;
            gridView4.Columns.Clear();
            gridControl4.DataSource = piececumuls;
            gridView4.Columns[0].Caption = "Date";
            gridView4.Columns[1].Caption = "Entrée";
            gridView4.Columns[2].Caption = "Retour";
            gridView4.Columns[3].Caption = "Sortie";
            gridView4.Columns[4].Caption = "N.B.E";
            gridView4.Columns[5].Caption = "N.B.S";
            gridView4.Columns[6].Caption = "N.B.L";
            gridView4.Columns[7].Visible = false;
            gridView4.Columns[8].Caption = "N.fact";
            gridView4.Columns[9].Caption = "Observation";
         

        }

        private void simpleButton9_Click(object sender, EventArgs e)
        {
            if (lookUpEdit3.EditValue != null && dateEdit2.EditValue != null && dateEdit3.EditValue != null)
            {
                Fourniture art = (Fourniture)lookUpEdit3.GetSelectedDataRow();
                DateTime date1 =new DateTime();
                date1 = dateEdit2.DateTime;
                DateTime date2 = new DateTime();
                date2 = dateEdit3.DateTime;
                List<Cumul> cumuls = artser.getallcumulbyarticlebetweendate(art.codefour,date1,date2);
                fillgrid(cumuls);
            }
            else if(lookUpEdit3.EditValue != null && dateEdit2.EditValue != null)
            {
                Fourniture art = (Fourniture)lookUpEdit3.GetSelectedDataRow();
                DateTime date1 = new DateTime();
                date1 = dateEdit2.DateTime;
               
                List<Cumul> cumuls = artser.getallcumulbyarticleafterndate(art.codefour, date1);
                fillgrid(cumuls);
            }
            else if (lookUpEdit3.EditValue != null && dateEdit3.EditValue != null)
            {
                Fourniture art = (Fourniture)lookUpEdit3.GetSelectedDataRow();
                DateTime date1 = new DateTime();
                date1 = dateEdit2.DateTime;

                List<Cumul> cumuls = artser.getallcumulbyarticlebeforedate(art.codefour, date1);
                fillgrid(cumuls);
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            ClearAllForm(this);
            gridControl4.DataSource = null;
            gridView4.Columns.Clear();
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

        private void Cumulsfourniture_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            this.Parent = null;
            e.Cancel = true;
        }
    }
}