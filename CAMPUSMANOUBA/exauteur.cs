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

namespace CAMPUSMANOUBA
{
    public partial class exauteur : DevExpress.XtraEditors.XtraForm
    {
        public exauteur()
        {
            InitializeComponent();
        }
        public static exauteur instance;
        public static exauteur Instance()
        {
            if (instance == null)

                instance = new exauteur();
            return instance;

        }
        private void exauteur_Load(object sender, EventArgs e)
        {

        }
    }
}