using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
  public   class UserService
    {
        public UserService()
        {

        }
        NewCampusEntities db = new NewCampusEntities();
        private static UserService instance;
        public static UserService Instance()
        {
            if (instance == null)

                instance = new UserService();
            return instance;

        }
        public void addUser(Droit dr)
        {
            db.Droit.Add(dr);
            db.SaveChanges();
        }

        public List<Droit> getallinfo()
        {
            return db.Droit.ToList();
        }
        public Droit getuserinfo(string login,string password)
        {
            return db.Droit.Where(aa=>aa.login ==login && aa.password==password).FirstOrDefault();
        }
        public Droit getuserbycode(int code)
        {
            return db.Droit.Where(aa => aa.code == code).FirstOrDefault();
        }
        public void updateDroit(Droit dr)
        {
            db.Droit.Attach(db.Droit.Single(x => x.code == dr.code));
            db.Entry(db.Droit.Single(x => x.code == dr.code)).CurrentValues.SetValues(dr);
            db.SaveChanges();

        }
        public void deleteuser(Droit dr)
        {
            db.Droit.Remove(dr);
        }
    }
}
