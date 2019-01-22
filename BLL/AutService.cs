using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    public class AutService
    {
        NewCampusEntities db = new NewCampusEntities();
        public AutService()
        { }
        private static AutService instance;
        public static AutService Instance()
        {
            if (instance == null)

                instance = new AutService();
            return instance;

        }

        public void addauteur(auteur aut)
        {
            db.auteur.Add(aut);
            db.SaveChanges();
        }
        public int findlastaut()
        {
            int numaut= 0;
            auteur aut = new auteur();
            aut = db.auteur.ToList().LastOrDefault();
            if (aut == null)
            {
                numaut = 1;
            }
            else
            {
                numaut = (int)aut.numeroaut;
            }
          
            return numaut;
        }
        public List<auteur> getallauthor()
        {
          return  db.auteur.ToList();
        }
        public void deleteauth(auteur aut)
        {
            db.auteur.Remove(aut);
            db.SaveChanges();
        }
        public void updateaut(auteur aut)
        {
            auteur thisaut = new auteur();
            thisaut= db.auteur.Where(aa => aa.numeroaut == aut.numeroaut).FirstOrDefault();
            thisaut.nom = aut.nom;

            thisaut.prenom = aut.prenom;
            thisaut.adr = aut.adr;
            thisaut.email = aut.email;
            thisaut.institution = aut.institution;
            thisaut.ville = aut.ville;
            thisaut.web = aut.web;
            thisaut.specialite = aut.specialite;
            thisaut.codepostal = aut.codepostal;
            db.SaveChanges();
        }
    }
}
