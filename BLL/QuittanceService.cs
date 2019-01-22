using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class QuittanceService
    {
        NewCampusEntities db = new NewCampusEntities();
        public void addQuittance(Quittance quit)
        {
            db.Quittance.Add(quit);

            db.SaveChanges();
        }
        public int NumQuittance()
        {
            try
            {
                   return (int)db.Quittance.Max(aa => aa.Nquiitance) + 1;             
            }
            catch (Exception exp)
            {
                return 1;
            }
                             
        }
        public List<Quittance> findQuitts()
        {
            return db.Quittance.ToList();
        }
        public Quittance findquitbycode(int code)
        {
            return db.Quittance.Where(aa => aa.Nquiitance == code).FirstOrDefault();
        }
        public Quittance findquitbycode(string code)
        {
            return db.Quittance.Where(aa => aa.nfact == code).FirstOrDefault();
        }
    }
}
