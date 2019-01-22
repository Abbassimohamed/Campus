using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class Cumul
    {
        public Cumul()
        { }
        private static Cumul instance;
        public static Cumul Instance()
        {
            if (instance == null)

                instance = new Cumul();
            return instance;

        }
        public DateTime date
        {
            get;
            set;
        }
        public string entree
        {
            get;
            set;

        }

        public string retour
        {
            get;
            set;

        }
        public string sortie
        {
            get;
            set;
        }
        public string nbe
        {
            get;
            set;
        }
        public string nbs
        {
            get;
            set;
        }

        public string nbl
        {
            get;
            set;
        }
        public string nbr
        {
            get;
            set;
        }
        public string nfact
        {
            get;
            set;
        }
        public string nomtier
        {
            get;
            set;
        }


    }
}
