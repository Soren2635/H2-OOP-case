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
        static void Opret_Kunde(string Fornavn, string Efternavn, string Adresse, int FK_PostNr, string Oprettelsesdato)
        {
            var connection = new SqlConnection("Trusted_Connection = true; Server = localhost; Database = BankDB; Connection Timeout = 30");
            SqlCommand cmd;
            connection.Open();

            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO Kunde(Fornavn, Efternavn, Adresse, FK_PostNr, Oprettelsesdato) values('" + Fornavn + "', '" + Efternavn + "', '" + Adresse + "', '" + FK_PostNr + "', '" + Oprettelsesdato + "');";
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
        static void selectData()
        {
            SqlConnection connection = new SqlConnection("Trusted_Connection = true; Server = localhost; Database = BankDB; Connection Timeout = 30");
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand("SELECT * FROM Kunde", connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
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
        static void deleteKunde(int i)
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
    }
}
