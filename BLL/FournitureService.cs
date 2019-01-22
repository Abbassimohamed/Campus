using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    public class FournitureService
    {
        NewCampusEntities db = new NewCampusEntities();
        CumulSortieServices cmsortser = CumulSortieServices.Instance();
        CumulEntServices cmentser = CumulEntServices.Instance();
        BonRetourServices brser = BonRetourServices.Instance();
        public FournitureService()
        { }
        private static FournitureService instance;
        public static FournitureService Instance()
        {
            if (instance == null)

                instance = new FournitureService();
            return instance;

        }
        //crud
        public void addFourniture (Fourniture art)
        {
            db.Fourniture.Add(art);
            db.SaveChanges();
        }
        public Fourniture getfourniturebycode(string code)
        {
          return  db.Fourniture.Where(aa=>aa.codefour==code).FirstOrDefault();
           
        }
        public int findlastart()
        {
            int numart = 0;
            Fourniture art = new Fourniture();
            art = db.Fourniture.ToList().LastOrDefault();
            if (art == null)
            {
                numart = 1;
            }
            else
            {
                numart = (int)art.idfour;
            }

            return numart;
        }
        public List<Fourniture> getallart()
        {
            return db.Fourniture.ToList();
        }
        public List<Cumul> getallcumulbyfourniture(string codeart)
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
            return cums.OrderBy(aa => aa.date).ToList();
        }
        //find option method
        public List<Cumul> getallcumulbyarticlebetweendate(string codeart, DateTime date1, DateTime date2)
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
            return cums.Where(aa => aa.date >= date1 && aa.date <= date2).OrderBy(aa => aa.date).ToList();
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
            return cums.Where(aa => aa.date >= date1).OrderBy(aa => aa.date).ToList();
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

        public List<Fourniture> findlastten()
        {
            List<Fourniture> arts = new List<Fourniture>();
            List<Fourniture> lastarts = new List<Fourniture>();

            arts = db.Fourniture.ToList();
            int x = (int)arts.LongCount();

            if (x <= 10)
            {
                return arts;
            }
            else
            {
                for (int i = 0; i < 10; i++)
                {
                    lastarts[i] = arts[x - i];
                }
                return lastarts;
            }


        }
        public void removeListart(List<Fourniture> art)
        {
            foreach (Fourniture f in art)
            { 
                if (f.idfour>0)
                { 
                   Fourniture fou = db.Fourniture.Single(a => a.idfour == f.idfour);
                   db.Fourniture.Remove(fou);
                   db.SaveChanges();
                }
            }
            
        }

        public void removeart(Fourniture art)
        {
            
            db.Fourniture.Remove(art);
           
            db.SaveChanges();
        }
        public void updatefourniture(Fourniture art)
        {
            
            db.Database.ExecuteSqlCommand("update Fourniture set quantitenstock='" + art.quantitenstock + "' where codefour='" + art.codefour + "'");
            db.SaveChanges();

        }
        
        //find option method
        public List<Fourniture> findbyfamily(string value)
        {
            return db.Fourniture.Where(aa => aa.famille.Equals(value)).ToList();
        }
        public List<Fourniture> findbysousfamille(string value)
        {
            return db.Fourniture.Where(aa => aa.sousfamille.Equals(value)).ToList();
        }
        //public List<Fourniture> findbyaut(string value)
        //{
        //    return db.Fourniture.Where(aa => aa.auteur.Equals(value)).ToList();
        //}
        public List<Fourniture> findbyfr(string value)
        {
            return db.Fourniture.Where(aa => aa.fournisseur.Equals(value)).ToList();
        }

        public List<Fourniture> findbyfmsousfm(string value, string value1)
        {
            return db.Fourniture.Where(aa => aa.famille.Equals(value) && aa.sousfamille.Equals(value1)).ToList();
        }
        //public List<Fourniture> findbyfmimp(string value, string value1)
        //{
        //    return db.Fourniture.Where(aa => aa.famille.Equals(value) && aa.imprimerie.Equals(value1)).ToList();
        //}
        public List<Fourniture> findbyfmfr(string value, string value1)
        {
            return db.Fourniture.Where(aa => aa.famille.Equals(value) && aa.fournisseur.Equals(value1)).ToList();
        }
        //public List<Fourniture> findbyautimp(string value, string value1)
        //{
        //    return db.Fourniture.Where(aa => aa.auteur.Equals(value) && aa.imprimerie.Equals(value1)).ToList();
        //}

        public List<Fourniture> findbyfamsfmfr(string value, string value1, string value2)
        {
            return db.Fourniture.Where(aa => aa.famille.Equals(value) && aa.sousfamille.Equals(value1) && aa.fournisseur.Equals(value2)).ToList();
        }
        //public List<Fourniture> findbyfamsfimp(string value, string value1, string value2)
        //{
        //    return db.Fourniture.Where(aa => aa.famille.Equals(value) && aa.sousfamille.Equals(value1) && aa.imprimerie.Equals(value2)).ToList();
        //}
        //public List<Fourniture> findbyfamautimpr(string value, string value1, string value2)
        //{
        //    return db.Fourniture.Where(aa => aa.famille.Equals(value) && aa.auteur.Equals(value1) && aa.imprimerie.Equals(value2)).ToList();
        //}

        //public List<Article> findbyfamsfmautimpr(string value, string value1, string value2, string value3)
        //{
        //    return db.Article.Where(aa => aa.famille.Equals(value) && aa.sousfamille.Equals(value1) && aa.auteur.Equals(value2) && aa.imprimerie.Equals(value3)).ToList();
        //}

    }
}
