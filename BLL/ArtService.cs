using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.Data;
using System.Data.SqlClient;
namespace BLL
{
    public class ArtService
    {
        NewCampusEntities db = new NewCampusEntities();
        CumulSortieServices cmsortser = CumulSortieServices.Instance();
        CumulEntServices cmentser = CumulEntServices.Instance();
        BonRetourServices brser = BonRetourServices.Instance();
        public ArtService()
        { }
        private static ArtService instance;
        public static ArtService Instance()
        {
            if (instance == null)

                instance = new ArtService();
            return instance;

        }
        //crud
        public void addarticle(Livre art)
        {
            db.Livre.Add(art);
            db.SaveChanges();
        }
        public int findlastart()
        {
            int numart= 0;
            Livre art = new Livre();
            art = db.Livre.ToList().LastOrDefault();
            if (art == null)
            {
                numart = 1;
            }
            else
            {
                numart = (int) art.Rang;
            }
          
            return numart;
        }
        public List<Livre> getallart()
        {
          return  db.Livre.ToList();
        }
        public List<Livre> getarticlestartby(string  vartext)
        {
            List<Livre> arts = new List<Livre>();
            using (NewCampusEntities db = new NewCampusEntities())
            {
                return db.spFindlistartstartby(vartext).ToList();
            }

        }
        //public List<Livre> getallartbyaut(int idaut)
        //{
        //    return db.Livre.Where(aa=>aa.idauteur== idaut).ToList();
        //}
        public int findnumartbyaut(int numaut)
        {
            int numliv = 0;
            List<Livre> articles = new List<Livre>();
            articles = db.Livre.Where(aa => aa.auteur == numaut.ToString()).ToList();
            numliv =(int) articles.LongCount();
            return numliv;
        }
        public List<InfoLivre> findlastten()
        {
           List<InfoLivre> arts = new List<InfoLivre>();
            arts = db.InfoLivre.ToList();
           return arts;
           
        }
     
        public void removeart(Livre art)
        {

            db.Livre.Remove(art);
            db.SaveChanges();
        }
        public void updateart(Livre art)
        {
            db.Livre.Attach(db.Livre.Single(x => x.Rang == art.Rang));
            db.Entry(db.Livre.Single(x => x.Rang == art.Rang)).CurrentValues.SetValues(art);

            db.SaveChanges();
           
        }
        public List<Cumul> getallcumulbyarticle(string codeart)
        {
            List<Piece_cumulent> entrees = db.Piece_cumulent.Where(aa => aa.Codearticle == codeart).ToList();
            List<Piece_cumulsortie> sorties = db.Piece_cumulsortie.Where(aa => aa.Codearticle == codeart).ToList();
            List<Piece_bonret> retours = db.Piece_bonret.Where(bb => bb.Codearticle == codeart).ToList();
            List<Cumul> cums = new List<Cumul>();
            foreach(Piece_cumulent pcent in entrees)
            {
                Cumul cm =new Cumul();
                cm.date = (DateTime)pcent.date;
                cm.entree = pcent.quantite.ToString();
                cm.nbe = pcent.Codecumul.ToString();
                cm.nbs = "";
                cm.nbl = "";
                cm.nbr= "";

                cm.nfact = "";
                cm.sortie = "";
                cm.retour = "";
                CumulEnt cment = new CumulEnt();
                cment= cmentser.getcumulbycode((Int32)pcent.Codecumul);
                cm.nomtier = cment.nomfr;
                cums.Add(cm);
            }
            foreach (Piece_cumulsortie pcent in sorties)
            {
                Cumul cm = new Cumul();

                cm.date = (DateTime)pcent.date;
                cm.sortie= pcent.quantite.ToString();
                cm.nbs= pcent.Codecumul.ToString();
                cm.nbe = "";
                cm.nbl = "";
                cm.nbr = "";
                cm.nfact = "";
                cm.entree = "";
                cm.retour = "";
                Cumulsortie cmsort = new Cumulsortie();
                cmsort = cmsortser.getcumulsortiebycode((Int32)pcent.Codecumul);
                
                cm.nomtier = cmsort.nomfr;
                cums.Add(cm);
            }
            foreach (Piece_bonret pcent in retours)
            {
                Cumul cm = new Cumul();

                cm.date = (DateTime)pcent.date;
                cm.retour = pcent.quantite.ToString();
                cm.nbr = pcent.Codecumul.ToString();
                cm.nbe = "";
                cm.nbl = "";
                cm.nbs = "";
                cm.nfact = "";
                cm.entree = "";
                cm.sortie = "";
                BonRetour br = new BonRetour();
                br= brser.getretourbycode((Int32)pcent.Codecumul);
                cm.nomtier = br.nomfr;
                cums.Add(cm);
            }
            return cums.OrderBy(aa=>aa.date).ToList();
        }
        //find option method
        public List<Cumul> getallcumulbyarticlebetweendate(string codeart,DateTime date1,DateTime date2)
        {
            List<Piece_cumulent> entrees = db.Piece_cumulent.Where(aa => aa.Codearticle == codeart).ToList();
            List<Piece_cumulsortie> sorties = db.Piece_cumulsortie.Where(aa => aa.Codearticle == codeart).ToList();
            List<Piece_bonret> retours = db.Piece_bonret.Where(bb => bb.Codearticle == codeart).ToList();
            List<Cumul> cums = new List<Cumul>();
            foreach (Piece_cumulent pcent in entrees)
            {
                Cumul cm = new Cumul();
                cm.date = (DateTime)pcent.date;
                cm.entree = pcent.quantite.ToString();
                cm.nbe = pcent.Codecumul.ToString();
                cm.nbs = "";
                cm.nbl = "";
                cm.nbr = "";

                cm.nfact = "";
                cm.sortie = "";
                cm.retour = "";
                CumulEnt cment = new CumulEnt();
                cment = cmentser.getcumulbycode((Int32)pcent.Codecumul);
                cm.nomtier = cment.nomfr;
                cums.Add(cm);
            }
            foreach (Piece_cumulsortie pcent in sorties)
            {
                Cumul cm = new Cumul();

                cm.date = (DateTime)pcent.date;
                cm.sortie = pcent.quantite.ToString();
                cm.nbs = pcent.Codecumul.ToString();
                cm.nbe = "";
                cm.nbl = "";
                cm.nbr = "";
                cm.nfact = "";
                cm.entree = "";
                cm.retour = "";
                Cumulsortie cmsort = new Cumulsortie();
                cmsort = cmsortser.getcumulsortiebycode((Int32)pcent.Codecumul);

                cm.nomtier = cmsort.nomfr;
                cums.Add(cm);
            }
            foreach (Piece_bonret pcent in retours)
            {
                Cumul cm = new Cumul();

                cm.date = (DateTime)pcent.date;
                cm.retour = pcent.quantite.ToString();
                cm.nbr = pcent.Codecumul.ToString();
                cm.nbe = "";
                cm.nbl = "";
                cm.nbs = "";
                cm.nfact = "";
                cm.entree = "";
                cm.sortie = "";
                BonRetour br = new BonRetour();
                br = brser.getretourbycode((Int32)pcent.Codecumul);
                cm.nomtier = br.nomfr;
                cums.Add(cm);
            }
            return cums.Where(aa=>aa.date>=date1 && aa.date<=date2).OrderBy(aa => aa.date).ToList();
        }
        public List<Cumul> getallcumulbyarticleafterndate(string codeart, DateTime date1)
        {
            List<Piece_cumulent> entrees = db.Piece_cumulent.Where(aa => aa.Codearticle == codeart).ToList();
            List<Piece_cumulsortie> sorties = db.Piece_cumulsortie.Where(aa => aa.Codearticle == codeart).ToList();
            List<Piece_bonret> retours = db.Piece_bonret.Where(bb => bb.Codearticle == codeart).ToList();
            List<Cumul> cums = new List<Cumul>();
            foreach (Piece_cumulent pcent in entrees)
            {
                Cumul cm = new Cumul();
                cm.date = (DateTime)pcent.date;
                cm.entree = pcent.quantite.ToString();
                cm.nbe = pcent.Codecumul.ToString();
                cm.nbs = "";
                cm.nbl = "";
                cm.nbr = "";

                cm.nfact = "";
                cm.sortie = "";
                cm.retour = "";
                CumulEnt cment = new CumulEnt();
                cment = cmentser.getcumulbycode((Int32)pcent.Codecumul);
                cm.nomtier = cment.nomfr;
                cums.Add(cm);
            }
            foreach (Piece_cumulsortie pcent in sorties)
            {
                Cumul cm = new Cumul();

                cm.date = (DateTime)pcent.date;
                cm.sortie = pcent.quantite.ToString();
                cm.nbs = pcent.Codecumul.ToString();
                cm.nbe = "";
                cm.nbl = "";
                cm.nbr = "";
                cm.nfact = "";
                cm.entree = "";
                cm.retour = "";
                Cumulsortie cmsort = new Cumulsortie();
                cmsort = cmsortser.getcumulsortiebycode((Int32)pcent.Codecumul);

                cm.nomtier = cmsort.nomfr;
                cums.Add(cm);
            }
            foreach (Piece_bonret pcent in retours)
            {
                Cumul cm = new Cumul();

                cm.date = (DateTime)pcent.date;
                cm.retour = pcent.quantite.ToString();
                cm.nbr = pcent.Codecumul.ToString();
                cm.nbe = "";
                cm.nbl = "";
                cm.nbs = "";
                cm.nfact = "";
                cm.entree = "";
                cm.sortie = "";
                BonRetour br = new BonRetour();
                br = brser.getretourbycode((Int32)pcent.Codecumul);
                cm.nomtier = br.nomfr;
                cums.Add(cm);
            }
            return cums.Where(aa => aa.date >= date1 ).OrderBy(aa => aa.date).ToList();
        }
        public List<Cumul> getallcumulbyarticlebeforedate(string codeart, DateTime date1)
        {
            List<Piece_cumulent> entrees = db.Piece_cumulent.Where(aa => aa.Codearticle == codeart).ToList();
            List<Piece_cumulsortie> sorties = db.Piece_cumulsortie.Where(aa => aa.Codearticle == codeart).ToList();
            List<Piece_bonret> retours = db.Piece_bonret.Where(bb => bb.Codearticle == codeart).ToList();
            List<Cumul> cums = new List<Cumul>();
            foreach (Piece_cumulent pcent in entrees)
            {
                Cumul cm = new Cumul();
                cm.date = (DateTime)pcent.date;
                cm.entree = pcent.quantite.ToString();
                cm.nbe = pcent.Codecumul.ToString();
                cm.nbs = "";
                cm.nbl = "";
                cm.nbr = "";

                cm.nfact = "";
                cm.sortie = "";
                cm.retour = "";
                CumulEnt cment = new CumulEnt();
                cment = cmentser.getcumulbycode((Int32)pcent.Codecumul);
                cm.nomtier = cment.nomfr;
                cums.Add(cm);
            }
            foreach (Piece_cumulsortie pcent in sorties)
            {
                Cumul cm = new Cumul();

                cm.date = (DateTime)pcent.date;
                cm.sortie = pcent.quantite.ToString();
                cm.nbs = pcent.Codecumul.ToString();
                cm.nbe = "";
                cm.nbl = "";
                cm.nbr = "";
                cm.nfact = "";
                cm.entree = "";
                cm.retour = "";
                Cumulsortie cmsort = new Cumulsortie();
                cmsort = cmsortser.getcumulsortiebycode((Int32)pcent.Codecumul);

                cm.nomtier = cmsort.nomfr;
                cums.Add(cm);
            }
            foreach (Piece_bonret pcent in retours)
            {
                Cumul cm = new Cumul();

                cm.date = (DateTime)pcent.date;
                cm.retour = pcent.quantite.ToString();
                cm.nbr = pcent.Codecumul.ToString();
                cm.nbe = "";
                cm.nbl = "";
                cm.nbs = "";
                cm.nfact = "";
                cm.entree = "";
                cm.sortie = "";
                BonRetour br = new BonRetour();
                br = brser.getretourbycode((Int32)pcent.Codecumul);
                cm.nomtier = br.nomfr;
                cums.Add(cm);
            }
            return cums.Where(aa => aa.date <= date1).OrderBy(aa => aa.date).ToList();
        }

        public List<Livre> findbyfamily(string value)
        {
            return db.Livre.Where(aa => aa.famille.Equals(value)).ToList();
        }
        //public List<Livre> findbysousfamille(string value)
        //{
        //    return db.Livre.Where(aa => aa.sousfamille.Equals(value)).ToList();
        //}
        public List<Livre> findbyaut(string value)
        {
            return db.Livre.Where(aa => aa.auteur.Equals(value)).ToList();
        }
        public List<Livre> findbyfr(string value)
        {
            return db.Livre.Where(aa => aa.imprimerie.Equals(value)).ToList();
        }

        //public List<Livre> findbyfmsousfm( string value,  string value1)
        //{
        //    return db.Livre.Where(aa => aa.famille.Equals(value) && aa.sousfamille.Equals(value1)).ToList();
        //}
        public List<Livre> findbyfmimp(string value, string value1)
        {
            return db.Livre.Where(aa => aa.famille.Equals(value) && aa.imprimerie.Equals(value1)).ToList();
        }
        public List<Livre> findbyfmaut(string value, string value1)
        {
            return db.Livre.Where(aa => aa.famille.Equals(value) && aa.auteur.Equals(value1)).ToList();
        }
        public List<Livre> findbyautimp(string value, string value1)
        {
            return db.Livre.Where(aa => aa.auteur.Equals(value) && aa.imprimerie.Equals(value1)).ToList();
        }

        public List<Livre> findbyfamsfmaut(string value, string value1,string value2)
        {
            return db.Livre.Where(aa => aa.famille.Equals(value) && aa.auteur.Equals(value2)).ToList();
        }
        public List<Livre> findbyfamsfimp(string value, string value1, string value2)
        {
            return db.Livre.Where(aa => aa.famille.Equals(value) && aa.imprimerie.Equals(value2)).ToList();
        }
        public List<Livre> findbyfamautimpr(string value, string value1, string value2)
        {
            return db.Livre.Where(aa => aa.famille.Equals(value) && aa.auteur.Equals(value1) && aa.imprimerie.Equals(value2)).ToList();
        }

        public List<Livre> findbyfamsfmautimpr(string value, string value1, string value2,string value3)
        {
            return db.Livre.Where(aa => aa.famille.Equals(value)  && aa.auteur.Equals(value2) && aa.imprimerie.Equals(value3)).ToList();
        }

        public Livre getArticleByCode(string code)
        {
            return (db.Livre.Where(a => a.codeart == code).FirstOrDefault());

        }

        public String modifierStock(Livre Cab)
        {
            String resultat = "OK";
            using (NewCampusEntities db = new NewCampusEntities())
            {
                if (this.testModifierStock(Cab) == false)
                {
                    try
                    {
                        db.Livre.Attach(db.Livre.Single(x => x.Rang == Cab.Rang));
                        db.Entry(db.Livre.Single(x => x.Rang == Cab.Rang)).CurrentValues.SetValues(Cab);
                        //db.piece.ApplyCurrentValues(Cab);
                        db.SaveChanges();
                    }
                    catch (Exception e)
                    {
                        resultat = e.Message;
                    }
                }
                else
                {
                    resultat = "piece inexistante";
                }
            }
            return resultat;
        }

        public Boolean testModifierStock(Livre p)
        {
            Boolean trouve = true;
            using (NewCampusEntities db = new NewCampusEntities())
            {
                try
                {

                    var cx = db.Livre.Single(c => c.Rang == p.Rang);
                    if (cx.Rang == p.Rang)
                    {
                        trouve = false;
                    }

                }
                catch (Exception)
                {
                    trouve = false;
                }
            }
            return trouve;
        }
        public void reserver(string codearticle,double quantit)
        {
            NewCampusEntities db = new NewCampusEntities();
            Livre art = db.Livre.Where(aa => aa.codeart == codearticle).Single();
            art.quantitenstock = art.quantitenstock - quantit;         
            db.SaveChanges();

        }
        public void reserveretlivrer(string codearticle, double quantit)
        {
            NewCampusEntities db = new NewCampusEntities();
            Livre art = db.Livre.Where(aa => aa.codeart == codearticle).Single();
            art.quantitenstock = art.quantitenstock - quantit;
           
            db.SaveChanges();

        }
        public void livrer(string codearticle, double quantit)
        {
            NewCampusEntities db = new NewCampusEntities();
            Livre art = db.Livre.Where(aa => aa.codeart == codearticle).Single();
       
            art.quantitenstock = art.quantitenstock - quantit;
            db.SaveChanges();

        }
        public void annulerreserver(string codearticle, double quantit)
        {
            NewCampusEntities db = new NewCampusEntities();
            Livre art = db.Livre.Where(aa => aa.codeart == codearticle).Single();
            art.quantitenstock = art.quantitenstock + quantit;
            db.SaveChanges();

        }
        public void annulerreservbliv(string codearticle, double quantit)
        {
            NewCampusEntities db = new NewCampusEntities();
            Livre art = db.Livre.Where(aa => aa.codeart == codearticle).Single();
            art.quantitenstock = art.quantitenstock + quantit;
        
            db.SaveChanges();

        }
        public void annulerreservblivcmdstay(string codearticle, double quantit)
        {
            NewCampusEntities db = new NewCampusEntities();
            Livre art = db.Livre.Where(aa => aa.codeart == codearticle).Single();
          
            art.quantitenstock = art.quantitenstock + quantit;
            db.SaveChanges();

        }
        public void annulerbliv(string codearticle, double quantit)
        {
            NewCampusEntities db = new NewCampusEntities();
            Livre art = db.Livre.Where(aa => aa.codeart == codearticle).Single();
            art.quantitenstock = art.quantitenstock + quantit;
            art.quantitenstock = art.quantitenstock + quantit;
            db.SaveChanges();

        }
        public int checkavailab(string codearticle, double quantit)
        {
            NewCampusEntities db = new NewCampusEntities();
            Livre art = db.Livre.Where(c => c.codeart == codearticle).First();
            if(art.quantitenstock>=quantit )
            {
                return 1;
            }
            else
            {
                return 0;
            }

            
        }
        public List<getartinfo_Result> getarticlesinfo()
        {

            return db.getartinfo().ToList() ;
        }
      
    }
}
