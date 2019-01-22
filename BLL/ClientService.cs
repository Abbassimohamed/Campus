using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;

namespace BLL
{
    public class ClientService
    {
        NewCampusEntities db = new NewCampusEntities();
        public ClientService()
        { }
        private static ClientService instance;
        public static ClientService Instance()
        {
            if (instance == null)

                instance = new ClientService();
            return instance;

        }

        public void deleteclt(client clt)
        {
            db.client.Remove(clt);
            db.SaveChanges();
        }
        public void addclient(client clt)
        {
            db.client.Add(clt);
            db.SaveChanges();
        }
        public int findlastclient()
        {
            int numclt = 0;
            client clt = new client();
            clt = db.client.ToList().LastOrDefault();
            if (clt == null)
            {
                numclt = 1;
            }
            else
            {
                numclt = (int)clt.numerocl;
            }
          
            return numclt;
        }
        public List<client> findclientstartbyletter(string a)
        {


           return db.client.Where(aa => aa.raisonsoc.Substring(0, 1) == a).ToList();


        }
        public client getClientByRaisonSoc(string rais)
        {
            return(db.client.Where(aa => aa.raisonsoc == rais).FirstOrDefault());
        }
        public client getClientByNumero(int numcl)
        {
            return (db.client.Where(aa => aa.codeclient == numcl).FirstOrDefault());
        }
        public List<client> listclts()
        {
            return db.client.ToList();
        }

    }
}
