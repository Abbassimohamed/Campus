using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
  public   class PublicationService
    {
        NewCampusEntities db = new NewCampusEntities();
        public PublicationService()
        { }
        private static PublicationService instance;
        public static PublicationService Instance()
        {
            if (instance == null)

                instance = new PublicationService();
            return instance;

        }


        public void addfichepub(Dossierpublication file)
        {
            db.Dossierpublication.Add(file);
            db.SaveChanges();
        }
        public int findlastfiche()
        {
            int numart = 0;
            Dossierpublication art = new Dossierpublication();
            art = db.Dossierpublication.ToList().LastOrDefault();
            if (art == null)
            {
                numart = 1;
            }
            else
            {
                numart = (int)art.id;
            }

            return numart;
        }
        public List<Dossierpublication> getallfile()
        {
            return db.Dossierpublication.ToList();
        }
     
         public void removefile(Dossierpublication art)
        {

            db.Dossierpublication.Remove(art);
            db.SaveChanges();
        }
        public void updatedossier(Dossierpublication art)
        {

            Dossierpublication dossier = db.Dossierpublication.Where(aa => aa.ndossier == art.ndossier).FirstOrDefault();
            dossier = art;
            db.SaveChanges();

        }

    }
}
