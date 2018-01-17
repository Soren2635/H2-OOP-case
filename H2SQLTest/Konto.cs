using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace H2SQLTest
{
    class Konto
    {
        // Opret_Konto
        static void addKonto(int FK_KundeNr, int FK_KontoTypeID, decimal Saldo, string Oprettelsesdato)
        {
            var connection = new SqlConnection("Trusted_Connection = true; Server = localhost; Database = BankDB; Connection Timeout = 30");
            SqlCommand cmd;
            connection.Open();

            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO Konto(FK_KundeNr, FK_KontoTypeID, Saldo, Oprettelsesdato) values('" + FK_KundeNr + "', '" + FK_KontoTypeID + "', '" + Saldo + "', '" + Oprettelsesdato + "');";
                cmd.ExecuteNonQuery();

                Console.WriteLine("Hej Kunde Nr. " + FK_KundeNr + ", vi har tilføjet Kontotypen: " + FK_KontoTypeID + ", med en saldo på " + Saldo + ". D." + Oprettelsesdato + "");
                Console.ReadKey();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (connection.State == System.Data.ConnectionState.Open)
                {
                    connection.Close();
                }
            }
        } // Opret_Konto

        // Vis_Konto_Data
        static void selectKundeKonti(int i)
        {
            var connection = new SqlConnection("Trusted_Connection = true; Server = localhost; Database = BankDB; Connection Timeout = 30");
            using (SqlCommand cmd = new SqlCommand("SELECT KundeNr, Fornavn, Efternavn, KontoNr, Saldo FROM Kunde INNER JOIN Konto ON Kunde.KundeNr = Konto.FK_KundeNr WHERE KundeNr = @i", connection))
            {
                // skal kunne selecte den bestemte kundes konti!
            }
        } // Vis_Konto_Data
    }
}
