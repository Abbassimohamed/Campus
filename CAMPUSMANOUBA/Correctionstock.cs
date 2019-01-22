using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DAL;
using BLL;
namespace CAMPUSMANOUBA
{
    public partial class Correctionstock : Form
    {
        private static Livre thisliv;
        ArtService artser = new ArtService();
        public Correctionstock()
        {
            InitializeComponent();
        }
        public Correctionstock(Livre liv)
        {
            InitializeComponent();
            tcodearticle.Text = liv.codeart;
            mtitre.Text = liv.titre;
            tqtact.Text = liv.quantitenstock.ToString() ;
            tqtreel.Text = liv.qtitreelle.ToString();
            tstockalert.Text = liv.stockalert.ToString();
            thisliv = liv;
        }

        private void Correctionstock_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Voulez vous confirmer la mise à jour article ", "Attention", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

            if (dialogResult == DialogResult.Yes)
            {
                thisliv.quantitenstock =Convert.ToDouble( tqtact.Text.Replace('.',','));
                thisliv.qtitreelle =Convert.ToDouble( tqtreel.Text.Replace('.', ','));
                artser.updateart(thisliv);
                this.Close();
                
            }
            else if (dialogResult == DialogResult.No)
            {

            }
        }
    }
}
