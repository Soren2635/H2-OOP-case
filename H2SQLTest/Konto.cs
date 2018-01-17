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

    }
}
