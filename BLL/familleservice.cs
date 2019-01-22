using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class familleservice
    {
        public familleservice()
        { }
        public static familleservice instance;
        NewCampusEntities db = new NewCampusEntities();
        public static familleservice Instance()
        {
            if (instance == null)

                instance = new familleservice();
            return instance;

        }
        public bool addfamille(famille famil)
        {
            bool check = false;
            check = checklist(famil.familledesign);
            if (check == false)
            {
                db.famille.Add(famil);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        public List<famille> getallfamille()
        {
            return db.famille.ToList();
        }
        public void deletefamily(int numero)
        {
            famille fam1 = new famille();
            fam1 = db.famille.Where(aa => aa.idfamille.Equals(numero)).FirstOrDefault();
            db.famille.Remove(fam1);
            db.SaveChanges();
        }
        public void updatefamille(famille fam)
        {
            famille fam1 = new famille();
            fam1 = db.famille.Where(aa => aa.idfamille.Equals(fam.idfamille)).FirstOrDefault();
            fam1.familledesign = fam.familledesign;
            db.SaveChanges();



        }
        public bool checklist(string design)
        {
            bool check = false;
            List<famille> specs = new List<famille>();
            specs = db.famille.ToList();
            foreach (famille sp in specs)
            {
                if (sp.familledesign.Equals(design))
                {
                    check = true;
                }
            }
            return check;
        }
     
    }
}
