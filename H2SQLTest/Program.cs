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
            //selectData();
            //deleteKunde(7); //Slet kunde
            //addKonto(3, 2, -100000, "2017-01-01"); //Tilføj konto til kunde
            selectKundeKonti(2);

            //checkSaldo(3);
            Console.ReadKey();
        }
    }
}
