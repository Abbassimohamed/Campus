using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
 public   class CumulEntServices
    {
        public CumulEntServices()
        { }
        NewCampusEntities db = new NewCampusEntities();
        private static CumulEntServices instance;
        public static CumulEntServices Instance()
        {
            if (instance == null)

                instance = new CumulEntServices();
            return instance;

        }
        public void addcumul(CumulEnt cment,List<Piece_cumulent> listcumul)
        {
            db.CumulEnt.Add(cment);
            foreach (Piece_cumulent cumul_piece in listcumul)
            {
                db.Piece_cumulent.Add(cumul_piece);
            }

            db.SaveChanges();
        }
    
        public List<CumulEnt> getallcumul()
        {
            return db.CumulEnt.ToList();
        }
        public List<Piece_cumulent> getallpiecebycodecumul(int codecumul)
        {
            List<Piece_cumulent> piececumulent = new List<Piece_cumulent>();
            piececumulent = db.Piece_cumulent.Where(aa => aa.Codecumul == codecumul).ToList();
            return piececumulent;
        }
        public List<Piece_cumulent> getallcumulbypiece(string Codearticle)
        {
            List<Piece_cumulent> piececumulent = new List<Piece_cumulent>();
            piececumulent = db.Piece_cumulent.Where(aa => aa.Codearticle == Codearticle).ToList();
            return piececumulent;
        }
        public List<Piece_cumulent> getallcumulcodeartbetweendate(string Codearticle,DateTime date1,DateTime date2)
        {
            List<Piece_cumulent> piececumulent = new List<Piece_cumulent>();
            piececumulent = db.Piece_cumulent.Where(aa => aa.date >= date1 && aa.date<=date2 && aa.Codearticle==Codearticle).ToList();
            return piececumulent;
        }
        public int getlastcumul()
        {
            if (db.CumulEnt.Count() != 0)
            {
                return (int) db.CumulEnt.Max( aa=>aa.numerocumul);
            }
            else
            {
                return 0;
            }
        }
        public List<CumulEnt> getallcumulbymonth(DateTime date,string type)
        {
            return db.CumulEnt.Where(aa=>aa.date.Value.Month==date.Month && aa.date.Value.Year==date.Year && aa.type==type).ToList();
        }
        public CumulEnt  getcumulbycode( int code)
        {
            return db.CumulEnt.Where(aa => aa.numerocumul==code).FirstOrDefault();
        }
        public void deletecumulbycode(int numerocumul)
        {
            CumulEnt cment = new CumulEnt();
            cment=(CumulEnt) db.CumulEnt.Where(aa => aa.numerocumul==numerocumul).FirstOrDefault();
            List<Piece_cumulent> pccms = new List<Piece_cumulent>();
            pccms = db.Piece_cumulent.Where(aa => aa.Codecumul == numerocumul).ToList();
            foreach (Piece_cumulent pc in pccms)
            {
                db.Piece_cumulent.Remove(pc);
            }
            db.CumulEnt.Remove(cment);
            db.SaveChanges();
        }

    }
}
