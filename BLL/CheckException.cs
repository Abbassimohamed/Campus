using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
namespace BLL
{
    public class CheckException
    {
        public CheckException()
        {

        }
        reservationservice reserser = reservationservice.Instance();
        BCService bcser = new BCService();
        private static CheckException instance;
        public static CheckException Instance()
        {
            if (instance == null)

                instance = new CheckException();
            return instance;

        }
        public void checkcommandeavailability()
        {
            List<bon_commande> bcmds = new List<bon_commande>();
            bcmds = bcser.getAllBbyetat("en cours");
            foreach(bon_commande bcmd in bcmds)
            {
                DateTime nowadate = System.DateTime.Now;
                
            }


        }
    }
}
