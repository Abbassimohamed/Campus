using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
 public   class BonRetourServices
    {
        public BonRetourServices()
        { }
        NewCampusEntities db = new NewCampusEntities();
        private static BonRetourServices instance;
        public static BonRetourServices Instance()
        {
            if (instance == null)

                instance = new BonRetourServices();
            return instance;

        }
        public void addbonret(BonRetour br,List<Piece_bonret> listbr)
        {
            db.BonRetour.Add(br);
            foreach (Piece_bonret piec_br in listbr)
            {
                db.Piece_bonret.Add(piec_br);
            }

            db.SaveChanges();
        }
    
        public List<BonRetour> getallret()
        {
            return db.BonRetour.ToList();
        }
        public List<Piece_bonret> getallpiecebycodebr(int codebr)
        {
            List<Piece_bonret> piecebrs = new List<Piece_bonret>();
            piecebrs = db.Piece_bonret.Where(aa => aa.Codecumul == codebr).ToList();
            return piecebrs;
        }
     
        public List<Piece_bonret> getallretourbypiece(string Codearticle)
        {
            List<Piece_bonret> piecebrs = new List<Piece_bonret>();
            piecebrs = db.Piece_bonret.Where(aa => aa.Codearticle == Codearticle).ToList();
            return piecebrs;
        }
        public List<Piece_bonret> getallretourcodeartbetweendate(string Codearticle,DateTime date1,DateTime date2)
        {
            List<Piece_bonret> piecebrs = new List<Piece_bonret>();
            piecebrs = db.Piece_bonret.Where(aa => aa.date >= date1 && aa.date<=date2 && aa.Codearticle==Codearticle).ToList();
            return piecebrs;
        }
        public int getlastbretour()
        {
            if (db.BonRetour.Count() != 0)
            {
                return (int) db.BonRetour.Max( aa=>aa.numerocumul);
            }
            else
            {
                return 0;
            }
        }
        public BonRetour getretourbycode(int code)
        {
            return db.BonRetour.Where(aa => aa.numerocumul == code).FirstOrDefault();
        }
        public List<BonRetour> getallretourbymonth(DateTime date,string type)
        {
            return db.BonRetour.Where(aa=>aa.date.Value.Month==date.Month && aa.date.Value.Year==date.Year && aa.type==type).ToList();
        }
        public void deleteretourbycode(int numerocumul)
        {
            BonRetour cment = new BonRetour();
            cment=(BonRetour) db.BonRetour.Where(aa => aa.numerocumul==numerocumul).FirstOrDefault();
            List<Piece_bonret> pccms = new List<Piece_bonret>();
            pccms = db.Piece_bonret.Where(aa => aa.Codecumul == numerocumul).ToList();
            foreach (Piece_bonret pc in pccms)
            {
                db.Piece_bonret.Remove(pc);
            }
            db.BonRetour.Remove(cment);
            db.SaveChanges();
        }

    }
}
