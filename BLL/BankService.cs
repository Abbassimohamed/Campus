using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using BLL;
using DAL;
namespace BLL
{
    public class BankService
    {
        NewCampusEntities db = new NewCampusEntities();
        public BankService()
        { }
        private static BankService instance;
        public static BankService Instance()
        {
            if (instance == null)

                instance = new BankService();
            return instance;

        }
        public void addbanque(Banque bank)
        {
            db.Banque.Add(bank);
            db.SaveChanges();
        }
        public List<Banque> getbanks()
        {
            return db.Banque.ToList();
        }
        public Banque getbankbynum(int num)
        {
            return db.Banque.Where(aa => aa.idbanque == num).FirstOrDefault();
        }
        public void deletebank(int id)
        {
           db.Banque.Remove( db.Banque.Where(aa => aa.idbanque == id).FirstOrDefault());
        }
        public void updatebank(Banque bank)
        {
            db.Banque.Attach(db.Banque.Single(x => x.idbanque == bank.idbanque));
            db.Entry(db.Banque.Single(x => x.idbanque == bank.idbanque)).CurrentValues.SetValues(bank);
            
            db.SaveChanges();
            //var sql = @"Update Banque SET nombanque = @nombanque ,rib=@rib,soldeinitial=@soldeinitial WHERE idbanque = @idbanque";

            //db.Database.ExecuteSqlCommand(
            //    sql,
            //    new SqlParameter("@nombanque", bank.nombanque),
            //    new SqlParameter("@rib", bank.rib),
            //    new SqlParameter("@soldeinitial", bank.soldeinitial),
            //    new SqlParameter("@idbanque", bank.idbanque));


        }

    }
}
