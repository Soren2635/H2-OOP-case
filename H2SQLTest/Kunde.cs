using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace H2SQLTest
{
    class Kunde
    {
        // Opret Kunde
        public static void Opret_Kunde(string Fornavn, string Efternavn, string Adresse, int FK_PostNr, string Oprettelsesdato)
        {
            var connection = new SqlConnection("Trusted_Connection = true; Server = localhost; Database = BankDB; Connection Timeout = 30");
            SqlCommand cmd;
            connection.Open();

            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO Kunde(Fornavn, Efternavn, Adresse, FK_PostNr, Kunde_Oprettelsesdato) values('" + Fornavn + "', '" + Efternavn + "', '" + Adresse + "', '" + FK_PostNr + "', '" + Oprettelsesdato + "');";
                cmd.ExecuteNonQuery();

                Console.WriteLine("Tilføjede " + Fornavn + " " + Efternavn + " " + Adresse + " " + FK_PostNr + " " + Oprettelsesdato + " til kunde-databasen.");
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
        } // Opret_Kunde

        //Vis Kunde_Data (mangler sortering)
        public static void selectData()
        {
            SqlConnection connection = new SqlConnection("Trusted_Connection = true; Server = localhost; Database = BankDB; Connection Timeout = 30");
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Kunde", connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                Console.WriteLine(reader.GetValue(i));
                            }
                            Console.WriteLine();
                        }
                    }
                }
            }
        } // Vis Kunde_Data

        // Slet_Kunde (skal ændres til aktivering / deaktivering af kunde
        public static void deleteKunde(int i)
        {
            var connection = new SqlConnection("Trusted_Connection = true; Server = localhost; Database = BankDB; Connection Timeout = 30");
            SqlCommand cmd;
            connection.Close();
            cmd = new SqlCommand("DELETE FROM Kunde WHERE KundeNr = @i", connection);

            cmd.Parameters.Add("@i", System.Data.SqlDbType.Int);
            cmd.Parameters["@i"].Value = i;
            connection.Open();

            int slettet = cmd.ExecuteNonQuery();
            if (slettet > 0)
            {
                Console.WriteLine("Slettet - Tryk enter.");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Ikke fundet - Tryk enter.");
                Console.ReadKey();
            }
            connection.Close();
        } // Slet_Kunde

        // Vis_Konto_Data
        public static void selectKundeKonti(int i)
        {
            var connection = new SqlConnection("Trusted_Connection = true; Server = localhost; Database = BankDB; Connection Timeout = 30");
            SqlCommand cmd;
            connection.Close();
            cmd = new SqlCommand("SELECT Kunde.KundeNr, Kunde.Efternavn, Konto.KontoNr, Konto.Konto_Oprettelsesdato, Konto.Saldo, Kontotype.KontoType, Kontotype.Rente FROM Kunde JOIN Konto ON Kunde.KundeNr = Konto.FK_KundeNr JOIN Kontotype ON dbo.Kontotype.ID = Konto.FK_KontoTypeID WHERE KundeNr = @i", connection);

            cmd.Parameters.Add("@i", System.Data.SqlDbType.Int);
            cmd.Parameters["@i"].Value = i;
            connection.Open();

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    for (int o = 0; o < reader.FieldCount; o++)
                    {
                        Console.WriteLine(reader.GetValue(o));
                    }
                    Console.WriteLine();
                }
            }
        } // Vis_Konto_Data
    }
}
