using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class BLService
    {
        
        public Livre GetProdByQtRest(string code)
        {
            NewCampusEntities db = new NewCampusEntities();
            Livre p = new Livre();
            try
            {
                return db.Livre.Where(aa => aa.codeart == code ).FirstOrDefault();
            
            }
            catch (Exception)
            {
                p = new Livre();
            }
            return p;

        }



        public  bon_livraison GetBLBynum(int num)
        {
           // NewCampusEntities db = new NewCampusEntities();
            bon_livraison Bl = new bon_livraison();
            try
            {
                
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    return db.bon_livraison.Where(aa => aa.numero_bl== num ) .FirstOrDefault();
                }
            }
            catch (Exception)
            {
                Bl = new bon_livraison();
            }
            return Bl;

        }


        public String ajouterBL(bon_livraison Bl)
        {
            String resultat = "Ok";

            using (NewCampusEntities db = new NewCampusEntities())
            {

                if (this.testAjouterBl(Bl) == false)
                {

                    try
                    {

                        db.bon_livraison.Add(Bl);
                        db.SaveChanges();



                    }
                    catch (Exception e)
                    {
                        resultat = e.Message;
                    }
                }
                else
                {
                    resultat = "BL existante, verifiez le champs num Bl ";
                }

            }
            return resultat;
        }

        public Boolean testAjouterBl(bon_livraison b)
        {
            Boolean trouve = true;
            //string s = cli.code;
            using (NewCampusEntities db = new NewCampusEntities())
            {
                try
                {
                    var cx = db.bon_livraison.Single(c => c.numero_bl.Equals(b.numero_bl));
                    if (cx == null)
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

        public String ajouterLBL(ligne_bl lBl)
        {
            String resultat = "Ok";

            using (NewCampusEntities db = new NewCampusEntities())
            {

                if (this.testAjouterLBl(lBl) == false)
                {

                    try
                    {

                        db.ligne_bl.Add(lBl);
                        db.SaveChanges();



                    }
                    catch (Exception e)
                    {
                        resultat = e.Message;
                    }
                }
                else
                {
                    resultat = "LBL existante, verifiez le champs num Bl ";
                }

            }
            return resultat;
        }

        public Boolean testAjouterLBl(ligne_bl b)
        {
            Boolean trouve = true;
            //string s = cli.code;
            using (NewCampusEntities db = new NewCampusEntities())
            {
                try
                {
                    var cx = db.ligne_bl.Single(c => c.id.Equals(b.id));
                    if (cx == null)
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
      
        public ligne_bl getLblById(int b)
        {
            ligne_bl lBl = new ligne_bl();
            try
            {
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    return db.ligne_bl.Where(aa => aa.id == b).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                lBl = new ligne_bl();
            }
            return lBl;


        }

        public List<ligne_bl> getLblByCodeBL(int b)
        {
            List <ligne_bl> lBl = new List < ligne_bl>();
            try
            {
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    lBl = db.ligne_bl.Where(aa => aa.id_bl == b).ToList();
                }
            }
            catch (Exception)
            {
                lBl = new List<ligne_bl>();
            }
            return lBl;


        }
        public ligne_bl getLblByCodeBLandart(int b,string codeart)
        {
            ligne_bl lBl = new ligne_bl();
            try
            {
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    lBl = db.ligne_bl.Where(aa => aa.id_bl == b && aa.code_art==codeart).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                lBl =new ligne_bl();
            }
            return lBl;


        }
        public String supprimerBL(bon_livraison bl)
        {
            String resultat = "BL supprimé";

            using (NewCampusEntities db = new NewCampusEntities())
            {
                try
                {
                    db.bon_livraison.Attach(bl);// selectionne l'element dans la base pour le supprimer dans l'etape suivante
                    db.bon_livraison.Remove(bl);
                    db.SaveChanges();

                }
                catch (Exception)
                {
                    resultat = resultat + "," + bl.numero_bl;
                    db.Dispose();
                }
            }

            return resultat;
        }
        
        public string supprimerLbl(List<ligne_bl > listp)
        {
            String resultat = "OK";

            foreach (ligne_bl aX in listp)
            {
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    try
                    {
                        if (aX.id > 0)
                        {
                            ligne_bl artX = db.ligne_bl.Single(a => a.id == aX.id);
                            db.ligne_bl.Remove(artX);
                            db.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        resultat = resultat + "," + aX.id_bl;
                        db.Dispose();
                    }
                }
            }
            return resultat;
        }

        public List<bon_livraison> getAllBLbytype(string type)
        {
            //NewCampusEntities  db = new NewCampusEntities ();
            List<bon_livraison> bl = new List<bon_livraison>();
            try
            {
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    bl = (from d in db.bon_livraison where d.etat==type select d).ToList();
                }
            }
            catch (Exception)
            {
                bl = new List<bon_livraison>();
            }
            return bl;
        }
        public List<bon_livraison> getAllBLnotfacured()
        {
            //NewCampusEntities  db = new NewCampusEntities ();
            List<bon_livraison> bl = new List<bon_livraison>();
            try
            {
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    bl = (from d in db.bon_livraison where d.etat != "facturée" select d).ToList();
                }
            }
            catch (Exception)
            {
                bl = new List<bon_livraison>();
            }
            return bl;
        }

        public List<bon_livraison> getAllBL()
        {
            //NewCampusEntities  db = new NewCampusEntities ();
            List<bon_livraison> bl = new List<bon_livraison>();
            //try
            //{
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    bl = (from d in db.bon_livraison select d).ToList();
                }
            //}
            //catch (Exception)
            //{
            //    bl = new List<bon_livraison>();
            //}
            return bl;
        }

        public int incrementerBL()
        {
            try
            { 

            using (NewCampusEntities db = new NewCampusEntities())
            {
                //int numbl = ((from d in db.bon_livraison select d.numero_bl).Max())+1;
                    int numbl = (int)(from d in db.bon_livraison select d.numero_bl).Max() + 1;

                return numbl;
            }
            }
            catch(Exception exp)
            {
                return 1;
            }
        }
        public void modifier (bon_livraison bl)
        {
            using (NewCampusEntities db = new NewCampusEntities())
            {
                db.bon_livraison.Attach(db.bon_livraison.Single(x => x.id== bl.id));
                db.Entry(db.bon_livraison.Single(x => x.id == bl.id)).CurrentValues.SetValues(bl);
                //db.piece.ApplyCurrentValues(Cab);
                db.SaveChanges();



            }



         }

       public void deletelbl(int numbl)
        {
            using (NewCampusEntities db = new NewCampusEntities())
            {
              List<  ligne_bl> lbls = db.ligne_bl.Where(a => a.id_bl == numbl).ToList();
                foreach(ligne_bl lbl in lbls)
                {
                     db.ligne_bl.Remove(lbl);
                
                }
                db.SaveChanges();
            }
        }

        #region facture

        public facturevente GetFactBynum(int num)
        {
            // NewCampusEntities db = new NewCampusEntities();
            facturevente Bl = new facturevente();
            try
            {
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    return db.facturevente.Where(aa => aa.numero_fact == num).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                Bl = new facturevente();
            }
            return Bl;

        }

        public List<facturevente>  GetFactByetat(string etat)
        {

            using (NewCampusEntities db = new NewCampusEntities())
            {
                return db.facturevente.Where(aa => aa.etat == etat).ToList();
            }

        }
        public Boolean testAjouterLF(piece_fact b)
        {
            Boolean trouve = true;
            //string s = cli.code;
            using (NewCampusEntities db = new NewCampusEntities())
            {
                try
                {
                    var cx = db.piece_fact.Single(c => c.id_piece.Equals(b.id_piece));
                    if (cx == null)
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


        public String ajouterLF(piece_fact lBl)
        {
            String resultat = "Ok";

            using (NewCampusEntities db = new NewCampusEntities())
            {

                if (this.testAjouterLF(lBl) == false)
                {

                    try
                    {

                        db.piece_fact.Add(lBl);
                        db.SaveChanges();



                    }
                    catch (Exception e)
                    {
                        resultat = e.Message;
                    }
                }
                else
                {
                    resultat = "LF existante ";
                }

            }
            return resultat;
        }


        public Boolean testAjouterFacture(facturevente b)
        {
            Boolean trouve = true;
            //string s = cli.code;
            using (NewCampusEntities db = new NewCampusEntities())
            {
                try
                {
                    var cx = db.facturevente.Single(c => c.id.Equals(b.id));
                    if (cx == null)
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

        public List<piece_fact> getpiecefacts(int codeart)
        {
            List<piece_fact> pcfacts = new List<piece_fact>();

            using (NewCampusEntities db = new NewCampusEntities())
            {
                 pcfacts = (from p in db.piece_fact
                            where p.idauteur == codeart 
                            select p).ToList();

            }
            return pcfacts;
            
            }

        public String ajouterFacture(facturevente lBl)
        {
            String resultat = "Ok";

            using (NewCampusEntities db = new NewCampusEntities())
            {

                if (this.testAjouterFacture(lBl) == false)
                {

                    try
                    {

                        db.facturevente.Add(lBl);
                        db.SaveChanges();



                    }
                    catch (Exception e)
                    {
                        resultat = e.Message;
                    }
                }
                else
                {
                    resultat = "LF existante ";
                }

            }
            return resultat;
        }



        public List<facturevente> getAllFacture()
        {
            //NewCampusEntities  db = new NewCampusEntities ();
            List<facturevente> bl = new List<facturevente>();
            try
            {
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    bl = (from d in db.facturevente select d).ToList();
                }
            }
            catch (Exception)
            {
                bl = new List<facturevente>();
            }
            return bl;
        }



        public String supprimerFact(facturevente f)
        {

            String resultat = "Facture supprimé";

            using (NewCampusEntities db = new NewCampusEntities())
            {
                try
                {
                    db.facturevente.Attach(f);// selectionne l'element dans la base pour le supprimer dans l'etape suivante
                    db.facturevente.Remove(f);
                    db.SaveChanges();

                }
                catch (Exception)
                {
                    resultat = resultat + "," + f.numero_fact;
                    db.Dispose();
                }
            }

            return resultat;
        }


        public string supprimerLF(List<piece_fact> listp)
        {
            String resultat = "OK";

            foreach (piece_fact aX in listp)
            {
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    try
                    {
                        if (aX.id_piece > 0)
                        {
                            piece_fact artX = db.piece_fact.Single(a => a.id_piece == aX.id_piece);
                            db.piece_fact.Remove(artX);
                            db.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        //resultat = resultat + "," + aX.id_commande;
                        db.Dispose();
                    }
                }
            }
            return resultat;
        }

        public void updatefact(facturevente fact)
        {
            using (NewCampusEntities db = new NewCampusEntities())
            {
                db.facturevente.Attach(db.facturevente.Single(x => x.id == fact.id));
                db.Entry(db.facturevente.Single(x => x.id == fact.id)).CurrentValues.SetValues(fact);
                //db.piece.ApplyCurrentValues(Cab);
                db.SaveChanges();
            }
        }
        public List<piece_fact> getLFByCodeFact(int b)
        {
            List<piece_fact> lBl = new List<piece_fact>();
            try
            {
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    lBl = db.piece_fact.Where(aa => aa.id_fact == b).ToList();
                }
            }
            catch (Exception)
            {
                lBl = new List<piece_fact>();
            }
            return lBl;


        }
        public List<piece_fact> rapportventearticle(Livre art,DateTime date1, DateTime date2)
        {
                List<piece_fact> lBlst = new List<piece_fact>();         
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    return  db.spRapportArt(art.codeart, date1, date2).ToList();                  
                }
          
        }
        public int NumFact()
        {
            try
            {

                using (NewCampusEntities db = new NewCampusEntities())
                {
                    ////int numbl = ((from d in db.bon_livraison select d.numero_bl).Max())+1;
                    //int numf = (int)(from d in db.facturevente select d.numero_fact).Max() + 1;
                 int numf =(int) db.facturevente.Max(aa => aa.numero_fact)+1;
                    return numf;
                }
            }
            catch (Exception exp)
            {
                return 1;
            }
        }
        #endregion



        #region bon sortie

        public bon_sortie GetBSBynum(int num)
        {
            // NewCampusEntities db = new NewCampusEntities();
            bon_sortie Bl = new bon_sortie();
            try
            {
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    return db.bon_sortie.Where(aa => aa.numero_bs == num).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                Bl = new bon_sortie();
            }
            return Bl;

        }


        public Boolean testAjouterLBS(ligne_bs b)
        {
            Boolean trouve = true;
            //string s = cli.code;
            using (NewCampusEntities db = new NewCampusEntities())
            {
                try
                {
                    var cx = db.ligne_bs.Single(c => c.id.Equals(b.id));
                    if (cx == null)
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


        public String ajouterLBS(ligne_bs lBl)
        {
            String resultat = "Ok";

            using (NewCampusEntities db = new NewCampusEntities())
            {

                if (this.testAjouterLBS(lBl) == false)
                {

                    try
                    {

                        db.ligne_bs.Add(lBl);
                        db.SaveChanges();



                    }
                    catch (Exception e)
                    {
                        resultat = e.Message;
                    }
                }
                else
                {
                    resultat = "LF existante ";
                }

            }
            return resultat;
        }


        public Boolean testAjouterBS(bon_sortie b)
        {
            Boolean trouve = true;
            //string s = cli.code;
            using (NewCampusEntities db = new NewCampusEntities())
            {
                try
                {
                    var cx = db.bon_sortie.Single(c => c.id.Equals(b.id));
                    if (cx == null)
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


        public String ajouterBS(bon_sortie lBl)
        {
            String resultat = "Ok";

            using (NewCampusEntities db = new NewCampusEntities())
            {

                if (this.testAjouterBS(lBl) == false)
                {

                    try
                    {

                        db.bon_sortie.Add(lBl);
                        db.SaveChanges();



                    }
                    catch (Exception e)
                    {
                        resultat = e.Message;
                    }
                }
                else
                {
                    resultat = "LF existante ";
                }

            }
            return resultat;
        }



        public List<bon_sortie> getAllBS()
        {
            //NewCampusEntities  db = new NewCampusEntities ();
            List<bon_sortie> bl = new List<bon_sortie>();
            try
            {
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    bl = (from d in db.bon_sortie select d).ToList();
                }
            }
            catch (Exception)
            {
                bl = new List<bon_sortie>();
            }
            return bl;
        }



        public String supprimerBS(bon_sortie f)
        {

            String resultat = "Bon sortie supprimé";

            using (NewCampusEntities db = new NewCampusEntities())
            {
                try
                {
                    db.bon_sortie.Attach(f);// selectionne l'element dans la base pour le supprimer dans l'etape suivante
                    db.bon_sortie.Remove(f);
                    db.SaveChanges();

                }
                catch (Exception)
                {
                    resultat = resultat + "," + f.numero_bs;
                    db.Dispose();
                }
            }

            return resultat;
        }


        public string supprimerLBS(List<ligne_bs> listp)
        {
            String resultat = "OK";

            foreach (ligne_bs aX in listp)
            {
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    try
                    {
                        if (aX.id > 0)
                        {
                            ligne_bs artX = db.ligne_bs.Single(a => a.id == aX.id);
                            db.ligne_bs.Remove(artX);
                            db.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        resultat = resultat + "," + aX.id;
                        db.Dispose();
                    }
                }
            }
            return resultat;
        }


        public List<ligne_bs> getLBSByCodeFact(int b)
        {
            List<ligne_bs> lBl = new List<ligne_bs>();
            try
            {
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    lBl = db.ligne_bs.Where(aa => aa.id_bs == b).ToList();
                }
            }
            catch (Exception)
            {
                lBl = new List<ligne_bs>();
            }
            return lBl;


        }

        public int NumBS()
        {
            try
            {

                using (NewCampusEntities db = new NewCampusEntities())
                {
                    //int numbl = ((from d in db.bon_livraison select d.numero_bl).Max())+1;
                    int numf = (int)(from d in db.bon_sortie select d.numero_bs).Max() + 1;

                    return numf;
                }
            }
            catch (Exception exp)
            {
                return 1;
            }
        }
        #endregion

    }
}
