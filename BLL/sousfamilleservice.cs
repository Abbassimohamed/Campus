using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
   public class sousfamilleservice
    {
        public sousfamilleservice()
        { }
        public static sousfamilleservice instance;
        NewCampusEntities db = new NewCampusEntities();
        public static sousfamilleservice Instance()
        {
            if (instance == null)

                instance = new sousfamilleservice();
            return instance;

        }
        public bool addsousfamille(sousfamille sfamil)
        {
            bool check = false;
            check = checklist(sfamil.sousfamilledesign);
            if (check == false)
            {
                db.sousfamille.Add(sfamil);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        public List<sousfamille> getallsousfamille()
        {
            return db.sousfamille.ToList();
        }
        public List<sousfamille> getallsousfamilleByfamille(int idfamille)
        {
            List<sousfamille> sfs = new List<sousfamille>();
            sfs= db.sousfamille.Where(aa=> aa.familleid==idfamille).ToList();
            return sfs;
        }
        public void deletesousfamily(int numero)
        {
            sousfamille fam1 = new sousfamille();
            fam1 = db.sousfamille.Where(aa => aa.idsousfamille.Equals(numero)).FirstOrDefault();
            db.sousfamille.Remove(fam1);
            db.SaveChanges();
        }
        public void updatesousfamille(sousfamille fam)
        {
            sousfamille fam1 = new sousfamille();
            fam1 = db.sousfamille.Where(aa => aa.idsousfamille.Equals(fam.idsousfamille)).FirstOrDefault();
            fam1.sousfamilledesign = fam.sousfamilledesign;
            db.SaveChanges();



        }
        public bool checklist(string design)
        {
            bool check = false;
            List<sousfamille> specs = new List<sousfamille>();
            specs = db.sousfamille.ToList();
            foreach (sousfamille sp in specs)
            {
                if (sp.sousfamilledesign.Equals(design))
                {
                    check = true;
                }
            }
            return check;
        }

    }
}
