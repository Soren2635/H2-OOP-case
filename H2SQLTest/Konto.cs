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
        public static void addKonto(int FK_KundeNr, int FK_KontoTypeID, decimal Saldo, string Oprettelsesdato)
        {
            var connection = new SqlConnection("Trusted_Connection = true; Server = localhost; Database = BankDB; Connection Timeout = 30");
            SqlCommand cmd;
            connection.Open();
            try
            {
                cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO Konto(FK_KundeNr, FK_KontoTypeID, Saldo, Konto_Oprettelsesdato) values('" + FK_KundeNr + "', '" + FK_KontoTypeID + "', '" + Saldo + "', '" + Oprettelsesdato + "');";
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

        // Vis enkelt konto
        public static void selectKonto(int i)
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
        } // Vis enkelt konto

        // Vis transaktioner
        public static void selectTrans(int i)
        {
            var connection = new SqlConnection("Trusted_Connection = true; Server = localhost; Database = BankDB; Connection Timeout = 30");
            SqlCommand cmd;
            connection.Close();
            cmd = new SqlCommand("SELECT Kunde.KundeNr, Kunde.Efternavn, Konto.KontoNr, Transaktioner.TransaktionsID, Transaktioner.Beløb, Transaktioner.Dato FROM Transaktioner Join Konto ON Transaktioner.FK_KontoNr = Konto.KontoNr JOIN Kunde ON Konto.FK_KundeNr = KundeNr WHERE Konto.KontoNr = @i", connection);
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
        } // Vis transaktioner

        // Ret rentesats
        public static void retRente(decimal i, int o)
        {
            var connection = new SqlConnection("Trusted_Connection = true; Server = localhost; Database = BankDB; Connection Timeout = 30");
            SqlCommand cmd;

            try
            {
                cmd = new SqlCommand("UPDATE Kontotype SET rente = @i WHERE Kontotype.ID = @o", connection);

                cmd.Parameters.Add("@i", System.Data.SqlDbType.Decimal);
                cmd.Parameters["@i"].Value = i;
                cmd.Parameters.Add("@o", System.Data.SqlDbType.Int);
                cmd.Parameters["@o"].Value = o;
                connection.Open();

                cmd.ExecuteNonQuery();
                Console.WriteLine("Renten er blevet opdaret til " +i);
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
        } // Ret rentesats

        // Udregn rente
        public static void udregnRente(decimal i, int o)
        {
            var connection = new SqlConnection("Trusted_Connection = true; Server = localhost; Database = BankDB; Connection Timeout = 30");
            SqlCommand cmd;
            connection.Close();
            cmd = new SqlCommand("SELECT Kunde.KundeNr, Kunde.Efternavn, Konto.KontoNr, Konto.Konto_Oprettelsesdato, Konto.Saldo, Kontotype.KontoType, Kontotype.Rente FROM Kunde JOIN Konto ON Kunde.KundeNr = Konto.FK_KundeNr JOIN Kontotype ON dbo.Kontotype.ID = Konto.FK_KontoTypeID WHERE KundeNr = @i", connection);
            cmd.Parameters.Add("@i", System.Data.SqlDbType.Int);
            cmd.Parameters["@i"].Value = i;
            connection.Open();
        } // Udregn rente

        // Ret rentesats
        public static void kontiPostering(decimal i, int o)
        {
            var connection = new SqlConnection("Trusted_Connection = true; Server = localhost; Database = BankDB; Connection Timeout = 30");
            SqlCommand cmd;
            try
            {
                cmd = new SqlCommand("UPDATE Konto SET rente = @i WHERE Kontotype.ID = @o", connection);
                cmd.Parameters.Add("@i", System.Data.SqlDbType.Decimal);
                cmd.Parameters["@i"].Value = i;
                cmd.Parameters.Add("@o", System.Data.SqlDbType.Int);
                cmd.Parameters["@o"].Value = o;
                connection.Open();
                cmd.ExecuteNonQuery();
                Console.WriteLine("Renten er blevet opdaret til " + i);
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
        } // Ret rentesats
    }
}
