using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class DevisService
    {

        public Livre GetProdByQtRest(string code)
        {
            NewCampusEntities db = new NewCampusEntities();
            Livre p = new Livre();
            try
            {
                //return db.Article.Where(aa => aa.code_piece == code && (int.Parse(aa.quantite_piece)) < qt).FirstOrDefault();
                return db.Livre.Where(aa => aa.codeart == code).FirstOrDefault();

            }
            catch (Exception)
            {
                p = new Livre();
            }
            return p;

        }



        public devis GetDevisBynum(int num)
        {
            // NewCampusEntities db = new NewCampusEntities();
            devis Bl = new devis();
            try
            {

                using (NewCampusEntities db = new NewCampusEntities())
                {
                    return db.devis.Where(aa => aa.numero_devis == num).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                Bl = new devis();
            }
            return Bl;

        }

        public List<devis> getAlldevisbytype(string type)
        {
            //NewCampusEntities  db = new NewCampusEntities ();
            List<devis> bl = new List<devis>();
            try
            {
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    bl = (from d in db.devis where d.etat == type select d).ToList();
                }
            }
            catch (Exception)
            {
                bl = new List<devis>();
            }
            return bl;
        }
        public List<devis> getAlldevisnotvalid()
        {
            //NewCampusEntities  db = new NewCampusEntities ();
            List<devis> bl = new List<devis>();
            try
            {
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    bl = (from d in db.devis where d.etat != "validé" select d).ToList();
                }
            }
            catch (Exception)
            {
                bl = new List<devis>();
            }
            return bl;
        }

        public String ajouterDevis(devis Bl)
        {
            String resultat = "Ok";

            using (NewCampusEntities db = new NewCampusEntities())
            {

                if (this.testAjouterDevis(Bl) == false)
                {

                    try
                    {

                        db.devis.Add(Bl);
                        db.SaveChanges();



                    }
                    catch (Exception e)
                    {
                        resultat = e.Message;
                    }
                }
                else
                {
                    resultat = "Devis existante, verifiez le champs num devis ";
                }

            }
            return resultat;
        }

        public Boolean testAjouterDevis(devis b)
        {
            Boolean trouve = true;
            //string s = cli.code;
            using (NewCampusEntities db = new NewCampusEntities())
            {
                try
                {
                    var cx = db.devis.Single(c => c.numero_devis.Equals(b.numero_devis));
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



        public String ajouterLDevis(ligne_devis lBl)
        {
            String resultat = "Ok";

            using (NewCampusEntities db = new NewCampusEntities())
            {

                if (this.testAjouterLDevis(lBl) == false)
                {

                    try
                    {

                        db.ligne_devis.Add(lBl);
                        db.SaveChanges();



                    }
                    catch (Exception e)
                    {
                        resultat = e.Message;
                    }
                }
                else
                {
                    resultat = "LDevis existante, verifiez le champs num Bl ";
                }

            }
            return resultat;
        }

        public Boolean testAjouterLDevis(ligne_devis b)
        {
            Boolean trouve = true;
            //string s = cli.code;
            using (NewCampusEntities db = new NewCampusEntities())
            {
                try
                {
                    var cx = db.ligne_devis.Single(c => c.id.Equals(b.id));
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


        public ligne_devis getLdevisById(int b)
        {
            ligne_devis lBl = new ligne_devis();
            try
            {
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    return db.ligne_devis.Where(aa => aa.id == b).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                lBl = new ligne_devis();
            }
            return lBl;


        }

        public List<ligne_devis> getLDevisByCodeDevis(int b)
        {
            List<ligne_devis> lBl = new List<ligne_devis>();
            try
            {
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    lBl = db.ligne_devis.Where(aa => aa.id_devis == b).ToList();
                }
            }
            catch (Exception)
            {
                lBl = new List<ligne_devis>();
            }
            return lBl;


        }
        public String supprimerDevis(devis bl)
        {
            String resultat = "Devis supprimé";

            using (NewCampusEntities db = new NewCampusEntities())
            {
                try
                {
                    db.devis.Attach(bl);// selectionne l'element dans la base pour le supprimer dans l'etape suivante
                    db.devis.Remove(bl);
                    db.SaveChanges();

                }
                catch (Exception)
                {
                    resultat = resultat + "," + bl.numero_devis;
                    db.Dispose();
                }
            }

            return resultat;
        }


        public string supprimerLDevis(List<ligne_devis> listp)
        {
            String resultat = "OK";

            foreach (ligne_devis aX in listp)
            {
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    try
                    {
                        if (aX.id > 0)
                        {
                            ligne_devis artX = db.ligne_devis.Single(a => a.id == aX.id);
                            db.ligne_devis.Remove(artX);
                            db.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        resultat = resultat + "," + aX.id_devis;
                        db.Dispose();
                    }
                }
            }
            return resultat;
        }

        public List<devis> getAllDevis()
        {
            //NewCampusEntities  db = new NewCampusEntities ();
            List<devis> bl = new List<devis>();
            try
            {
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    bl = (from d in db.devis select d).ToList();
                }
            }
            catch (Exception)
            {
                bl = new List<devis>();
            }
            return bl;
        }


        public int incrementerDevis()
        {
            try
            {

                using (NewCampusEntities db = new NewCampusEntities())
                {
                    //int numbl = ((from d in db.devis select d.numero_devis).Max())+1;
                    int numbl = (int)(from d in db.devis select d.numero_devis).Max() + 1;

                    return numbl;
                }
            }
            catch (Exception exp)
            {
                return 1;
            }
        }
        public void modifier(devis bl)
        {
            using (NewCampusEntities db = new NewCampusEntities())
            {
                db.devis.Attach(db.devis.Single(x => x.id == bl.id));
                db.Entry(db.devis.Single(x => x.id == bl.id)).CurrentValues.SetValues(bl);
                //db.piece.ApplyCurrentValues(Cab);
                db.SaveChanges();



            }



        }







   

    }
}
