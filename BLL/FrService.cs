using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    public class FrService
    {
        NewCampusEntities db = new NewCampusEntities();
        public FrService()
        { }
        private static FrService instance;
        public static FrService Instance()
        {
            if (instance == null)

                instance = new FrService();
                return instance;

        }

        public void addfr(fournisseur fr)
        {
            db.fournisseur.Add(fr);
            db.SaveChanges();
        }
        public int findlastfr()
        {
            int numfr= 0;
            fournisseur fr = new fournisseur();
            fr = db.fournisseur.ToList().LastOrDefault();
            if (fr == null)
            {
                numfr = 1;
            }
            else
            {
                numfr = (int)fr.numerofr;
            }
          
            return numfr;
        }
        public List<fournisseur> getallfr()
        {
            return db.fournisseur.ToList();
        }
        public void deletefr(fournisseur fr)
        {
            db.fournisseur.Remove(fr);
            db.SaveChanges();
        }
        public fournisseur getfournisseubyname(string name)
        {
            return db.fournisseur.Where(aa => aa.raisonfr == name).FirstOrDefault();
        }
    }
}
