using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
   public class emplacementService
    {
        public emplacementService()
        { }
        public static emplacementService instance;
        NewCampusEntities db = new NewCampusEntities();
        public static emplacementService Instance()
        {
            if (instance == null)

                instance = new emplacementService();
            return instance;

        }
        public bool addemp(emplacement emp)
        {
            bool check = false;
            check = checklist(emp.empdesign);
            if (check == false)
            {
                db.emplacement.Add(emp);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }

        }
        public List<emplacement> getallemp()
        {
            return db.emplacement.ToList();
        }
        public void deleteemp(int numero)
        {
            emplacement emp1 = new emplacement();
            emp1 = db.emplacement.Where(aa => aa.idemp.Equals(numero)).FirstOrDefault();
            db.emplacement.Remove(emp1);
            db.SaveChanges();
        }
        public void updateemp(emplacement emp)
        {
            emplacement emp1 = new emplacement();
            emp1 = db.emplacement.Where(aa => aa.idemp.Equals(emp.idemp)).FirstOrDefault();
            emp1.empdesign = emp.empdesign;
            db.SaveChanges();



        }
        public bool checklist(string design)
        {
            bool check = false;
            List<emplacement> specs = new List<emplacement>();
            specs = db.emplacement.ToList();
            foreach (emplacement sp in specs)
            {
                if (sp.empdesign.Equals(design))
                {
                    check = true;
                }
            }
            return check;
        }
    }
}
