using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.OleDb;
using DevExpress.LookAndFeel;
using System.Diagnostics;
using System.IO;
using System.Configuration;
using DAL;
using BLL;
namespace CAMPUSMANOUBA
{
    public partial class Login : DevExpress.XtraEditors.XtraForm
    {
        public Login()
        {
            InitializeComponent();
        }
        public static int codedroit;
        UserService userser = new UserService();
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void labelControl3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            this.Close();

        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            Droit dr = new Droit();
            string login = textEdit1.Text;
            string password = textEdit2.Text;
            dr = userser.getuserinfo(login,password);
            if(dr!=null)
            {
                codedroit = dr.code;
                this.Hide();
            
                Accueiltile acc = new Accueiltile();
                acc.Show();
            }
            else
            {
                label2.Show();
            }
        }
        private void AutoComplet(List<string> numeros)
        {

            AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
          
            foreach (string a in numeros)
            {
                collection.Add(a.ToString());
            }
            textEdit1.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textEdit1.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textEdit1.MaskBox.AutoCompleteCustomSource = collection;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void textEdit1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                List<string> logins = new List<string>();
                List<Droit> droits = new List<Droit>();
                droits = userser.getallinfo();
                foreach (Droit dr in droits)
                {
                    string a = dr.login;
                    logins.Add(a);

                }
                AutoComplet(logins);
            }catch(Exception exc)
            { }
            

        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Droit dr = new Droit();
            string login = textEdit1.Text;
            string password = textEdit2.Text;
            dr = userser.getuserinfo(login, password);
            if (dr != null)
            {
                codedroit = dr.code;
                this.Hide();

                Accueiltile acc = new Accueiltile();
                acc.Show();
            }
            else
            {
                label2.Show();
            }
        }
    }
}