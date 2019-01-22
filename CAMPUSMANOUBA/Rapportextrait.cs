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
    public partial class Rapportextrait : DevExpress.XtraEditors.XtraForm
    {
        public Rapportextrait()
        {
            InitializeComponent();
        }
        public static Rapportextrait instance;
        public static Rapportextrait Instance()
        {
            if (instance == null)

                instance = new Rapportextrait();
            return instance;

        }
        private static int indice = 0;//article;
        ArtService DS = new ArtService();
        AutService ser = new AutService();
        BLService blser = new BLService();
        private void articles()
        {
            //get All client
            lookart.Properties.DataSource = null;
            List<Livre> Allarticle = new List<Livre>();
            Allarticle = DS.getallart();

            lookart.Properties.ValueMember = "codeart";
            lookart.Properties.DisplayMember = "codeart";
            lookart.Properties.DataSource = Allarticle;
            lookart.Properties.PopulateColumns();
            lookart.Properties.Columns["idarticle"].Visible = false;
            lookart.Properties.Columns["codeart"].Caption = "Code article";
            lookart.Properties.Columns["isbn"].Caption = "ISBN";
            lookart.Properties.Columns["famille"].Visible = false;
            lookart.Properties.Columns["idfamille"].Visible = false;
            lookart.Properties.Columns["sousfamille"].Visible = false;
            lookart.Properties.Columns["idsousfamille"].Visible = false;
            lookart.Properties.Columns["titre"].Caption = "Titre";
            lookart.Properties.Columns["dateedition"].Visible = false;
            lookart.Properties.Columns["imprimerie"].Visible = false;
            lookart.Properties.Columns["idimprim"].Visible = false;
            lookart.Properties.Columns["auteur"].Visible = false;
            lookart.Properties.Columns["idauteur"].Visible = false;
            lookart.Properties.Columns["quantitenstock"].Visible = false;
            lookart.Properties.Columns["depotlegal"].Visible = false;
            lookart.Properties.Columns["pvpublic"].Visible = false;
            lookart.Properties.Columns["pvpromo"].Visible = false;
            lookart.Properties.Columns["droitaut"].Visible = false;
            lookart.Properties.Columns["abscice"].Visible = false;
            lookart.Properties.Columns["ordonne"].Visible = false;
            lookart.Properties.Columns["image"].Visible = false;


        }
        public void getallauthor()
        {
            List<auteur> authors = new List<auteur>();
            authors = ser.getallauthor();

            lookaut.Properties.ValueMember = "codeauteur";
            lookaut.Properties.DisplayMember = "nom";
            lookaut.Properties.DataSource = authors;
            lookaut.Properties.PopulateColumns();
            lookaut.Properties.Columns["codeauteur"].Visible = false;
            lookaut.Properties.Columns["numeroaut"].Visible = false;
            lookaut.Properties.Columns["nom"].Caption = "Nom";
            lookaut.Properties.Columns["prenom"].Visible = false;
            lookaut.Properties.Columns["tel"].Visible = false;
            lookaut.Properties.Columns["adr"].Visible = false;
            lookaut.Properties.Columns["email"].Visible = false;
            lookaut.Properties.Columns["institution"].Visible = false;
            lookaut.Properties.Columns["specialite"].Visible = false;
            lookaut.Properties.Columns["ville"].Visible = false;
            lookaut.Properties.Columns["codepostal"].Visible = false;
            lookaut.Properties.Columns["web"].Visible = false;
            lookaut.Properties.Columns["image"].Visible = false;


        }
        private void tileItem1_ItemClick(object sender, TileItemEventArgs e)
        {
            indice = 0;
            labelControl1.BringToFront();
            lookaut.BringToFront();

        }

        private void lookart_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
              
        }

        private void Rapportextrait_Load(object sender, EventArgs e)
        {

            articles();
            getallauthor();
        }

        private void tileItem2_ItemClick(object sender, TileItemEventArgs e)
        {
            indice = 1;
            labelControl2.BringToFront();
            lookart.BringToFront();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            if (indice != 0)
            {
                //article
                try
                {
                    Livre article =(Livre) lookart.GetSelectedDataRow();


                }
                catch (Exception exc)
                { }
            }
            else
            {
                //auteur
                try
                {
                    auteur aut = new auteur();
                    aut = (auteur)lookaut.GetSelectedDataRow();
                    int codeaut = (int)aut.numeroaut;
                    List<piece_fact> piecefacts = new List<piece_fact>();
                   
                    piecefacts = blser.getpiecefacts(codeaut);
                    gridControl1.DataSource = piecefacts;
                }
                catch (Exception exc)
                { }

            }
        }
    }
}