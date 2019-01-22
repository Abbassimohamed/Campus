using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class ReglementService
    {
        NewCampusEntities db = new NewCampusEntities();
        public ReglementService()
        { }
        private static ReglementService instance;
        public static ReglementService Instance()
        {
            if (instance == null)

                instance = new ReglementService();
            return instance;

        }

        public void addReglement(Reglement reg)
        {
            db.Reglement.Add(reg);
            db.SaveChanges();
        }
        public void updateReglement(Reglement reg)
        {
            db.Reglement.Attach(db.Reglement.Single(x => x.id == reg.id));
            db.Entry(db.Reglement.Single(x => x.id == reg.id)).CurrentValues.SetValues(reg);
            //db.piece.ApplyCurrentValues(Cab);
            db.SaveChanges();
        }
        public List<Reglement> findallreg()
        {
            return db.Reglement.ToList();
        }
        public void removeReg(Reglement reglement)
        {
            db.Reglement.Remove(reglement);
            db.SaveChanges();
        }
    }
}
