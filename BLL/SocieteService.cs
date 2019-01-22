using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    public class SocieteService
    {
        NewCampusEntities db = new NewCampusEntities();
        public SocieteService()
        { }
        private static SocieteService instance;
        public static SocieteService Instance()
        {
            if (instance == null)

                instance = new SocieteService();
                return instance;

        }

        public void addsociete(societe soc)
        {
            db.societe.Add(soc);
            db.SaveChanges();
        }
        public societe findlastfr()
        {
            return db.societe.FirstOrDefault();
        }
 
        public void updatesoc(societe soc)
        {
           
                db.societe.Attach(db.societe.Single(x => x.id == soc.id));
                db.Entry(db.societe.Single(x => x.id == soc.id)).CurrentValues.SetValues(soc);
                //db.piece.ApplyCurrentValues(Cab);
                db.SaveChanges();



           
        }
    }
}
