using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bacchus.MVC.Model;
using System.Data.SQLite;
using Bacchus.MVC.Util;

namespace Bacchus.MVC.Dao
{
    class MarquesDao
    {
        private SQLiteConnection Connection;

        private String Query;

        public MarquesDao()
        {
            this.Connection = Util.GetConnection.ConnectionSQLite();

            this.Query = null;

        }

        public Marques FindMarqueByRefMarque(int RefMarque)
        {
            Query = "SELECT * FROM Marques WHERE RefMarque = '" + RefMarque + "'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();

            var Reader = Command.ExecuteReader();
            if (Reader.Read())
            {
                var MarqueName = Reader.GetString(1);
                Marques Marque = new Marques(RefMarque, MarqueName);
                Reader.Close();
                return Marque;
            }
            else
                return null;
        }

        public void EmptyMarques()
        {
            Query = "DELETE FROM Marques; DELETE FROM sqlite_sequence WHERE name = 'Marques'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();
        }

        public List<Marques> GetAllMarques()
        {
            Query = "SELECT * FROM Marques";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();

            var Reader = Command.ExecuteReader();
            var Marques = new List<Marques>();

            while (Reader.Read())
            {
                var RefMarque = Reader.GetInt32(0);
                var MarqueName = Reader.GetString(1);

                Marques.Add(new Marques(RefMarque, MarqueName));
            }

            Reader.Close();

            return Marques;
        }

        public Boolean FindMarqueByMarqueName(string MarqueName)
        {
            Query = "SELECT * FROM Marques WHERE Nom = '" + MarqueName + "'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            var Reader = Command.ExecuteReader();

            if (Reader.Read())
                return true;
            else
                return false;
        }

        public int GetRefMarque(string MarqueName)
        {
            Query = "SELECT RefMarque FROM Marques WHERE Nom = '" + MarqueName + "'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();

            var RefMarque = Command.ExecuteScalar().ToString().Trim();

            return Int32.Parse(RefMarque);
        }

        public void AddMarque(Marques Marque)
        {
            Query = "INSERT INTO Marques (Nom) values('" + Marque.MarqueName + "')";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();
            Marque.RefMarque = GetRefMarque(Marque.MarqueName);
        }

        public List<Marques> FindMarquesBySousFamilleName(string SousFamilleName)
        {
            Query = "SELECT * FROM Marques WHERE RefMarque IN (SELECT RefMarque FROM Articles WHERE RefSousFamille = (SELECT RefSousFamille FROM SousFamilles WHERE Nom = '" + SousFamilleName + "'))";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            var Reader = Command.ExecuteReader();

            List<Marques> Marques = new List<Marques>();
            while (Reader.Read())
            {
                var RefMarque = Reader.GetInt32(0);
                var MarqueName = Reader.GetString(1);

                Marques Marque = new Marques(RefMarque, MarqueName);
                Marques.Add(Marque);
            }
            Reader.Close();
            return Marques;
        }

        public Marques FindMarquesByMarqueName(string MarqueName)
        {
            Query = "SELECT * FROM Marques WHERE Nom = '" + MarqueName + "'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            var Reader = Command.ExecuteReader();

            if (Reader.Read())
            {
                var RefMarque = Reader.GetInt32(0);
                Marques Marque = new Marques(RefMarque, MarqueName);
                Reader.Close();
                return Marque;
            }
            else
                return null;
        }

        public int GetMaxRefMarque()
        {
            Query = "SELECT MAX(RefMarque) FROM Marques";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();

            var MaxRefMarque = Command.ExecuteScalar().ToString().Trim();

            return Int32.Parse(MaxRefMarque);
        }

        public void DeleteMarque(Marques Marque)
        {
            Query = "DELETE FROM Marques WHERE Nom = '" + Marque.MarqueName + "' ; DELETE FROM Articles WHERE RefMarque = '" + Marque.RefMarque + "'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();
        }

        public void ModifyMarque(Marques Marque)
        {
            Query = "UPDATE Marques SET Nom = '" + Marque.MarqueName + "' WHERE RefMarque = '" + Marque.RefMarque + "'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();
        }




    }
}
