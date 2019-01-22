using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class SpecialityService
    {
        public SpecialityService()
        { }
        public static SpecialityService instance;
        NewCampusEntities db =new NewCampusEntities();
        public static SpecialityService Instance()
        {
            if (instance == null)

                instance = new SpecialityService();
            return instance;

        }
        public bool addspeciality(specialite sp)
        {
            bool check = false;
            check = checklist(sp.designation);
            if (check == false)
            {
                db.specialite.Add(sp);
                db.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }            
               
        }
        public List<specialite> getallspeciality()
        {
            return db.specialite.ToList();
        }
        public void deletesp(int numero)
        {
            specialite sp1 = new specialite();
            sp1= db.specialite.Where(aa => aa.idsp.Equals(numero)).FirstOrDefault();
            db.specialite.Remove(sp1);
            db.SaveChanges();
        }
        public void updatespec(specialite sp)
        {
            specialite sp1 = new specialite();
            sp1= db.specialite.Where(aa=> aa.idsp.Equals(sp.idsp)).FirstOrDefault();
            sp1.designation = sp.designation;
            db.SaveChanges();



        }
        public bool checklist(string design)
        {
            bool check = false;
            List<specialite> specs = new List<specialite>();
            specs = db.specialite.ToList();
            foreach (specialite sp in specs)
            {
                if (sp.designation.Equals(design))
                {
                    check = true;
                }
            }
            return check;
        }
    }
}
