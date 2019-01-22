using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class Agentservice
    {
        NewCampusEntities db = new NewCampusEntities();
        public Agentservice()
        { }
        private static Agentservice instance;
        public static Agentservice Instance()
        {
            if (instance == null)

                instance = new Agentservice();
            return instance;

        }
        public void addagent(agent ag)
        {
            db.agent.Add(ag);
            db.SaveChanges();
        }
        public void updateagent(agent ag)
        {
            
                db.agent.Attach(db.agent.Single(x => x.idagent == ag.idagent));
                db.Entry(db.agent.Single(x => x.idagent == ag.idagent)).CurrentValues.SetValues(ag);
                db.SaveChanges();
           

            }
        public List<agent> getallagent()
        {
            return db.agent.ToList();
        }
        public agent getagentbycin(string cin)
        {
            return db.agent.Where(aa => aa.cin == cin).FirstOrDefault();
        }
        public agent getagentbyid(int id)
        {
            return db.agent.Where(aa => aa.idagent == id).FirstOrDefault();
        }
        public void deleteagent(agent ag)
        {
            db.agent.Remove(ag);
            db.SaveChanges();           
        }
    }
}
