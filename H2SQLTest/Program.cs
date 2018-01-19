using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace H2SQLTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //addKunde("Bear", "Grylls", "Skoven 1", 2750, "2018-01-01"); //Tilføj kunde
            Kunde.selectData();
            //deleteKunde(7); //Slet kunde
            //addKonto(3, 2, -100000, "2017-01-01"); //Tilføj konto til kunde
            //Kunde.selectKundeKonti(2);
            //Konto.selectKonto(2);
            //Konto.selectTrans(2);
            Konto.retRente(0.6M, 1);

            //checkSaldo(3);
            Console.ReadKey();
        }
    }
}
