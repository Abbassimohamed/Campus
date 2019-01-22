using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
 public   class CumulSortieServices
    {
        public CumulSortieServices()
        { }
        NewCampusEntities db = new NewCampusEntities();
        private static CumulSortieServices instance;
        public static CumulSortieServices Instance()
        {
            if (instance == null)

                instance = new CumulSortieServices();
            return instance;

        }
        public void addcumul(Cumulsortie cment,List<Piece_cumulsortie> listcumul)
        {
            db.Cumulsortie.Add(cment);
            foreach (Piece_cumulsortie cumul_piece in listcumul)
            {
                db.Piece_cumulsortie.Add(cumul_piece);
            }

            db.SaveChanges();
        }
        public Cumulsortie getcumulsortiebycode(int code)
        {
            return db.Cumulsortie.Where(aa => aa.numerocumul == code).FirstOrDefault();
        }
        public List<Cumulsortie> getallcumul()
        {
            return db.Cumulsortie.ToList();
        }
        public List<Piece_cumulsortie> getallpiecebycodecumul(int codecumul)
        {
            List<Piece_cumulsortie> piececumulsortie = new List<Piece_cumulsortie>();
            piececumulsortie = db.Piece_cumulsortie.Where(aa => aa.Codecumul == codecumul).ToList();
            return piececumulsortie;
        }
        public List<Piece_cumulsortie> getallcumulbypiece(string Codearticle)
        {
            List<Piece_cumulsortie> piececumulsortie = new List<Piece_cumulsortie>();
            piececumulsortie = db.Piece_cumulsortie.Where(aa => aa.Codearticle == Codearticle).ToList();
            return piececumulsortie;
        }
        public List<Piece_cumulsortie> getallcumulcodeartbetweendate(string Codearticle,DateTime date1,DateTime date2)
        {
            List<Piece_cumulsortie> piececumulsortie = new List<Piece_cumulsortie>();
            piececumulsortie = db.Piece_cumulsortie.Where(aa => aa.date >= date1 && aa.date<=date2 && aa.Codearticle==Codearticle).ToList();
            return piececumulsortie;
        }
        public int getlastcumul()
        {
            if (db.Cumulsortie.Count() != 0)
            {
                return (int) db.Cumulsortie.Max( aa=>aa.numerocumul);
            }
            else
            {
                return 0;
            }
        }
        public List<Cumulsortie> getallcumulbymonth(DateTime date, string type)
        {
            return db.Cumulsortie.Where(aa => aa.date.Value.Month == date.Month && aa.date.Value.Year == date.Year && aa.type == type).ToList();
        }
        public List<Cumulsortie> getallcumulbytype( string type)
        {
            return db.Cumulsortie.Where(aa=> aa.type == type).ToList();
        }
        public void deletecumulbycode(int numerocumul)
        {
            Cumulsortie cmsortie = new Cumulsortie();
            cmsortie = (Cumulsortie) db.Cumulsortie.Where(aa => aa.numerocumul==numerocumul).FirstOrDefault();
            List<Piece_cumulsortie> pccms = new List<Piece_cumulsortie>();
            pccms = db.Piece_cumulsortie.Where(aa => aa.Codecumul == numerocumul).ToList();
            foreach (Piece_cumulsortie pc in pccms)
            {
                db.Piece_cumulsortie.Remove(pc);
            }
            db.Cumulsortie.Remove(cmsortie);
            db.SaveChanges();
        }

    }
}
