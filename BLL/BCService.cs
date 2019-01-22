using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class BCService
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
        public bon_commande GetAllBcBynum(int num)
        {
            // NewCampusEntities db = new NewCampusEntities();
            bon_commande Bc= new bon_commande();
            try
            {

                using (NewCampusEntities db = new NewCampusEntities())
                {
                    return db.bon_commande.Where(aa => aa.numero_bc == num).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                Bc= new bon_commande();
            }
            return Bc;

        }
        public String ajouterBc(bon_commande Bl)
        {
            String resultat = "Ok";

            using (NewCampusEntities db = new NewCampusEntities())
            {

                if (this.testAjouterBc(Bl) == false)
                {

                    try
                    {

                        db.bon_commande.Add(Bl);
                        db.SaveChanges();



                    }
                    catch (Exception e)
                    {
                        resultat = e.Message;
                    }
                }
                else
                {
                    resultat = "BL existante, verifiez le champs num Bc";
                }

            }
            return resultat;
        }
        public Boolean testAjouterBc(bon_commande b)
        {
            Boolean trouve = true;
            //string s = cli.code;
            using (NewCampusEntities db = new NewCampusEntities())
            {
                try
                {
                    var cx = db.bon_commande.Single(c => c.numero_bc.Equals(b.numero_bc));
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
        public String ajouterLBc(ligne_bc lBl)
        {
            String resultat = "Ok";

            using (NewCampusEntities db = new NewCampusEntities())
            {

                if (this.testAjouterLBc(lBl) == false)
                {

                    try
                    {

                        db.ligne_bc.Add(lBl);
                        db.SaveChanges();



                    }
                    catch (Exception e)
                    {
                        resultat = e.Message;
                    }
                }
                else
                {
                    resultat = "LBL existante, verifiez le champs num Bc";
                }

            }
            return resultat;
        }
        public Boolean testAjouterLBc(ligne_bc b)
        {
            Boolean trouve = true;
            //string s = cli.code;
            using (NewCampusEntities db = new NewCampusEntities())
            {
                try
                {
                    var cx = db.ligne_bc.Single(c => c.id.Equals(b.id));
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
        public void modifierBC(bon_commande bc)
        {
           
            using (NewCampusEntities db = new NewCampusEntities())
            {

                db.bon_commande.Attach(db.bon_commande.Single(x => x.id == bc.id));
                db.Entry(db.bon_commande.Single(x => x.id == bc.id)).CurrentValues.SetValues(bc);
              
                db.SaveChanges();

                
            }
           
        }
        public void modifierLBC(ligne_bc lignebc)
        {

            using (NewCampusEntities db = new NewCampusEntities())
            {

                db.ligne_bc.Attach(db.ligne_bc.Single(x => x.id == lignebc.id));
                db.Entry(db.ligne_bc.Single(x => x.id == lignebc.id)).CurrentValues.SetValues(lignebc);

                db.SaveChanges();


            }

        }
        public ligne_bc getLbcById(int b)
        {
            ligne_bc lBl = new ligne_bc();
            try
            {
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    return db.ligne_bc.Where(aa => aa.id == b).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                lBl = new ligne_bc();
            }
            return lBl;


        }
        public ligne_bc getLbcBycode(int b)
        {
            ligne_bc lBl = new ligne_bc();
            try
            {
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    return db.ligne_bc.Where(aa => aa.id_bc == b).FirstOrDefault();
                }
            }
            catch (Exception)
            {
                lBl = new ligne_bc();
            }
            return lBl;


        }
        public List<ligne_bc> getLbcByCodeBC(int b)
        {
            List<ligne_bc> lBl = new List<ligne_bc>();
            //try
            //{
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    lBl = db.ligne_bc.Where(aa => aa.id_bc == b).ToList();
                }
            //}
            //catch (Exception)
            //{
            //    lBl = new List<ligne_bc>();
            //}
            return lBl;


        }
        public String supprimerBc(bon_commande bl)
        {
            String resultat = "Bc supprimé";

            using (NewCampusEntities db = new NewCampusEntities())
            {
                try
                {
                    db.bon_commande.Attach(bl);// selectionne l'element dans la base pour le supprimer dans l'etape suivante
                    db.bon_commande.Remove(bl);
                    db.SaveChanges();

                }
                catch (Exception)
                {
                    resultat = resultat + "," + bl.numero_bc;
                    db.Dispose();
                }
            }

            return resultat;
        }
        public string supprimerLbc(List<ligne_bc> listp)
        {
            String resultat = "OK";

            foreach (ligne_bc aX in listp)
            {
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    try
                    {
                        if (aX.id > 0)
                        {
                            ligne_bc artX = db.ligne_bc.Single(a => a.id == aX.id);
                            db.ligne_bc.Remove(artX);
                            db.SaveChanges();
                        }
                    }
                    catch (Exception)
                    {
                        resultat = resultat + "," + aX.id_bc;
                        db.Dispose();
                    }
                }
            }
            return resultat;
        }
        public void supprimerLbcbyone(ligne_bc listp)
        {
          
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    try
                    {
                       
                            ligne_bc artX = db.ligne_bc.Single(a => a.id == listp.id);
                            db.ligne_bc.Remove(artX);
                            db.SaveChanges();
                        
                    }
                    catch (Exception)
                    {

                    }
                }
       
           
        }

        public List<bon_commande> getAllBc()
        {
            //NewCampusEntities  db = new NewCampusEntities ();
            List<bon_commande> Bc= new List<bon_commande>();
            //try
            //{
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    Bc= (from d in db.bon_commande select d).ToList();
                }
            //}
            //catch (Exception)
            //{
            //    Bc= new List<bon_commande>();
            //}
            return Bc;

        }
        public bon_commande getAllBcbycode(int numcmd)
        {
            using (NewCampusEntities db = new NewCampusEntities())
            {
                return db.bon_commande.Where(aa => aa.numero_bc == numcmd).FirstOrDefault();


            }

        }
        public List<bon_commande> getAllBbyetat(string etat)
        {
          
            using (NewCampusEntities db = new NewCampusEntities())
            {
                return db.bon_commande.Where(aa => aa.etat == etat).ToList();
            }
       
        }
        public int incrementerBc()
        {
            try
            {

                using (NewCampusEntities db = new NewCampusEntities())
                {
                    //int numbl = ((from d in db.bon_commande select d.numero_bc).Max())+1;
                    int numbl = (int)(from d in db.bon_commande select d.numero_bc).Max() +1;

                    return numbl;
                }
            }
            catch (Exception exp)
            {
                return 1;
            }
        }
        public ligne_bc findlcmdbyartcmd(string codeart,int lcmd)
        {
          
                
                using (NewCampusEntities db = new NewCampusEntities())
                {
                    return db.ligne_bc.Where(aa => aa.code_art == codeart && aa.id_bc == lcmd).FirstOrDefault();
                    
                }
          
        }


        //#region facture

        //public facturevente GetFactBynum(String num)
        //{
        //    // NewCampusEntities db = new NewCampusEntities();
        //    facturevente Bc= new facturevente();
        //    try
        //    {
        //        using (NewCampusEntities db = new NewCampusEntities())
        //        {
        //            return db.facturevente.Where(aa => aa.numero_fact == num).FirstOrDefault();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        Bc= new facturevente();
        //    }
        //    return Bl;

        //}


        //public Boolean testAjouterLF(piece_fact b)
        //{
        //    Boolean trouve = true;
        //    //string s = cli.code;
        //    using (NewCampusEntities db = new NewCampusEntities())
        //    {
        //        try
        //        {
        //            var cx = db.piece_fact.Single(c => c.id_piece.Equals(b.id_piece));
        //            if (cx == null)
        //            {
        //                trouve = false;
        //            }

        //        }
        //        catch (Exception)
        //        {

        //            trouve = false;

        //        }
        //    }
        //    return trouve;
        //}


        //public String ajouterLF(piece_fact lBl)
        //{
        //    String resultat = "Ok";

        //    using (NewCampusEntities db = new NewCampusEntities())
        //    {

        //        if (this.testAjouterLF(lBl) == false)
        //        {

        //            try
        //            {

        //                db.piece_fact.Add(lBl);
        //                db.SaveChanges();



        //            }
        //            catch (Exception e)
        //            {
        //                resultat = e.Message;
        //            }
        //        }
        //        else
        //        {
        //            resultat = "LF existante ";
        //        }

        //    }
        //    return resultat;
        //}


        //public Boolean testAjouterFacture(facturevente b)
        //{
        //    Boolean trouve = true;
        //    //string s = cli.code;
        //    using (NewCampusEntities db = new NewCampusEntities())
        //    {
        //        try
        //        {
        //            var cx = db.facturevente.Single(c => c.id_fact.Equals(b.id_fact));
        //            if (cx == null)
        //            {
        //                trouve = false;
        //            }

        //        }
        //        catch (Exception)
        //        {

        //            trouve = false;

        //        }
        //    }
        //    return trouve;
        //}


        //public String ajouterFacture(facturevente lBl)
        //{
        //    String resultat = "Ok";

        //    using (NewCampusEntities db = new NewCampusEntities())
        //    {

        //        if (this.testAjouterFacture(lBl) == false)
        //        {

        //            try
        //            {

        //                db.facturevente.Add(lBl);
        //                db.SaveChanges();



        //            }
        //            catch (Exception e)
        //            {
        //                resultat = e.Message;
        //            }
        //        }
        //        else
        //        {
        //            resultat = "LF existante ";
        //        }

        //    }
        //    return resultat;
        //}



        //public List<facturevente> getAllFacture()
        //{
        //    //NewCampusEntities  db = new NewCampusEntities ();
        //    List<facturevente> Bc= new List<facturevente>();
        //    try
        //    {
        //        using (NewCampusEntities db = new NewCampusEntities())
        //        {
        //            Bc= (from d in db.facturevente select d).ToList();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        Bc= new List<facturevente>();
        //    }
        //    return bl;
        //}



        //public String supprimerFact(facturevente f)
        //{

        //    String resultat = "Facture supprimé";

        //    using (NewCampusEntities db = new NewCampusEntities())
        //    {
        //        try
        //        {
        //            db.facturevente.Attach(f);// selectionne l'element dans la base pour le supprimer dans l'etape suivante
        //            db.facturevente.Remove(f);
        //            db.SaveChanges();

        //        }
        //        catch (Exception)
        //        {
        //            resultat = resultat + "," + f.numero_fact;
        //            db.Dispose();
        //        }
        //    }

        //    return resultat;
        //}


        //public string supprimerLF(List<piece_fact> listp)
        //{
        //    String resultat = "OK";

        //    foreach (piece_fact aX in listp)
        //    {
        //        using (NewCampusEntities db = new NewCampusEntities())
        //        {
        //            try
        //            {
        //                if (aX.id_piece > 0)
        //                {
        //                    piece_fact artX = db.piece_fact.Single(a => a.id_piece == aX.id_piece);
        //                    db.piece_fact.Remove(artX);
        //                    db.SaveChanges();
        //                }
        //            }
        //            catch (Exception)
        //            {
        //                resultat = resultat + "," + aX.id_commande;
        //                db.Dispose();
        //            }
        //        }
        //    }
        //    return resultat;
        //}


        //public List<piece_fact> getLFByCodeFact(int b)
        //{
        //    List<piece_fact> lBl = new List<piece_fact>();
        //    try
        //    {
        //        using (NewCampusEntities db = new NewCampusEntities())
        //        {
        //            lBl = db.piece_fact.Where(aa => aa.id_fact == b).ToList();
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        lBl = new List<piece_fact>();
        //    }
        //    return lBl;


        //}
        //#endregion

    }
}
