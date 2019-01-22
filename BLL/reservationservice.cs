using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class reservationservice
    {
        public reservationservice()
        { }
        private static reservationservice instance;
        public static reservationservice Instance()
        {
            if (instance == null)

                instance = new reservationservice();
            return instance;

        }
        public void addReservation(reservation reservat)
        {
            using (NewCampusEntities db =new  NewCampusEntities())
            {
                db.reservation.Add(reservat);
                db.SaveChanges();
            }

        }
        public string updatereservation(reservation rsv)
        {
         
            using (NewCampusEntities db = new NewCampusEntities())
            {
             
                db.reservation.Attach(db.reservation.Single(x => x.id == rsv.id));
                db.Entry(db.reservation.Single(x => x.id == rsv.id)).CurrentValues.SetValues(rsv);
                db.SaveChanges();
                

            }
            return rsv.article;
        }
        public void deletereservation(reservation reservat)
        {
            using (NewCampusEntities db = new NewCampusEntities())
            {
                db.reservation.Remove(reservat);
                db.SaveChanges();
            }

        }
        public void deleteallreserv(int numcmd)
        {
            List<reservation> rsvs = new List<reservation>();
            
            using (NewCampusEntities db = new NewCampusEntities())
            {
              rsvs=  db.reservation.Where(aa=>aa.ncmd==numcmd).ToList();
                foreach(reservation rsv in rsvs)
                {
                    db.reservation.Remove(rsv);
                }
                db.SaveChanges();
            }
        }
        public List<reservation> getallreservationbycmd(int numbc)
        {
            using (NewCampusEntities db = new NewCampusEntities())
            {
                return db.reservation.Where(aa => aa.ncmd == numbc).ToList();
            }
                
        } 
        public List<reservation> getallreservbyarticle(string codeart)
        {
            using (NewCampusEntities db = new NewCampusEntities())
            {
                return db.reservation.Where(aa => aa.article == codeart).ToList();
            }
        }
        public reservation getreserv(string codeart,int idcmd)
        {
            using (NewCampusEntities db = new NewCampusEntities())
            {
                return db.reservation.Where(aa => aa.article == codeart &&  aa.ncmd==idcmd).FirstOrDefault();
            }
        }
    }
}
