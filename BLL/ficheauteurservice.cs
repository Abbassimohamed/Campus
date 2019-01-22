using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLL;
using DAL;
namespace BLL
{
    public class ficheauteurservice
    {
        NewCampusEntities db = new NewCampusEntities();
        public ficheauteurservice()
        {
        }
        private static ficheauteurservice instance;
        public static ficheauteurservice Instance()
        {
            if (instance == null)

                instance = new ficheauteurservice();
            return instance;

        }

        public void addficheauteur(ficheauteur aut)
        {
            db.ficheauteur.Add(aut);
            db.SaveChanges();
        }
     
    
        public void deleteficheauth(ficheauteur aut)
        {
            db.ficheauteur.Remove(aut);
            db.SaveChanges();
        }
        public ficheauteur findbynfolder(int ndossier)
        {
            return db.ficheauteur.Where(aa => aa.ndossier == ndossier).FirstOrDefault();
           
            
        }
    }
}
