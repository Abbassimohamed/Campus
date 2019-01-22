using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using BLL;
namespace BLL
{
 public  class fichesuivieservice
    {
        public static fichesuivieservice instance;
        NewCampusEntities db = new NewCampusEntities();
        public static fichesuivieservice Instance()
        {
            if (instance == null)

                instance = new fichesuivieservice();
            return instance;

        }
        public void addfichesuivie(fichesuivi fichesui)
        {
            db.fichesuivi.Add(fichesui);
            db.SaveChanges();
        }
        public List<fichesuivi>  findallfichesuivie()
        {
            return db.fichesuivi.ToList();
        }
        public fichesuivi findfichesuivie(int ns)
        {
            return db.fichesuivi.Where(aa => aa.idfiche == ns).FirstOrDefault();
        }
        public List<fichesuivi> findallfichesuiviebyndossier(string ndossier )
        {
            return db.fichesuivi.Where(aa=>aa.ndossier==ndossier).ToList();
        }
        public List<string> findndosiiers()
        {
            List<Dossierpublication> fichesui = db.Dossierpublication.ToList();
            List<string> numeros = new List<string>();
            foreach(Dossierpublication dossier in fichesui)
            {
                numeros.Add(dossier.ndossier);
            }
            return numeros;
        }
    
        public void updatefichesuivi(fichesuivi fs)
        {

            db.fichesuivi.Attach(db.fichesuivi.Single(x => x.idfiche == fs.idfiche));
            db.Entry(db.fichesuivi.Single(x => x.idfiche == fs.idfiche)).CurrentValues.SetValues(fs);
            db.SaveChanges();


        }
        public void deletefile(fichesuivi fs)
        {
           fichesuivi file= findfichesuivie(fs.idfiche);
            db.fichesuivi.Remove(file);
           
            db.SaveChanges();


        }
    }
}
