using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class AvoirService
    {
        NewCampusEntities db = new NewCampusEntities();
        public AvoirService()
        {

        }
        public void addAvoir(Avoir avoir)
        {
            db.Avoir.Add(avoir);
            db.SaveChanges();
            
        }
        public void addligneAvoir(ligne_avoir ligneav)
        {
            db.ligne_avoir.Add(ligneav);
            db.SaveChanges();
        }
        public void updateAvoir(Avoir avoir)
        {
            db.Avoir.Attach(db.Avoir.Single(x => x.id == avoir.id));
            db.Entry(db.Avoir.Single(x => x.id == avoir.id)).CurrentValues.SetValues(avoir);
            //db.piece.ApplyCurrentValues(Cab);
            db.SaveChanges();
        }
        public void deleteAvoir(Avoir avoir)
        {
            db.Avoir.Remove(avoir);
            db.SaveChanges();
        }
        public List<Avoir> Getallavoirs()
        {
            return db.Avoir.ToList();
        }
        public List<ligne_avoir> getlavbycodeav(int numavoir)
        {
            return db.ligne_avoir.Where(aa => aa.id_avoir == numavoir).ToList();
        }
        public Avoir getavoirbycode(int numavoir)
        {
            return db.Avoir.Where(aa => aa.numeroavoir == numavoir).FirstOrDefault();

        }
        public int incrementerAvoir()
        {
            try
            {
                int numavoir = (int)(from d in db.Avoir select d.numeroavoir).Max() + 1;
                return numavoir;
            }
                catch(Exception ec)
            {
                return 1;
            }
            
        }
    }
}
