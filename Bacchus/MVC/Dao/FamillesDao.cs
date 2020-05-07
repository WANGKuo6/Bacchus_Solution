using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Bacchus.MVC.Model;
namespace Bacchus.MVC.Dao
{
    class FamillesDao
    {
        private SQLiteConnection Connection;
        private String Query;
        public FamillesDao()
        {
            this.Connection = Util.GetConnection.ConnectionSQLite();
            this.Query = null;
        }

        public Familles FindFamillesByRefFamille(int RefFamille)
        {
            Query = "SELECT * FROM Familles WHERE RefFamille = '" + RefFamille + "'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            var Reader = Command.ExecuteReader();

            if (Reader.Read())
            {
                var FamilleName = Reader.GetString(1);
                Familles Famille = new Familles(RefFamille, FamilleName);
                Reader.Close();
                return Famille;
            }
            else
                return null;
        }

        public Familles FindFamillesByRefSousFamille(int RefSousFamille)
        {
            Query = "SELECT * FROM Familles WHERE RefFamille = (SELECT RefFamille FROM SousFamilles WHERE RefSousFamille = '" + RefSousFamille + "')";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            var Reader = Command.ExecuteReader();

            if (Reader.Read())
            {
                var RefFamille = Reader.GetInt32(0);
                var FamilleName = Reader.GetString(1);
                Familles Famille = new Familles(RefFamille, FamilleName);
                Reader.Close();
                return Famille;
            }
            else
                return null;
        }

        public void EmptyFamilles()
        {
            Query = "DELETE FROM Familles; DELETE FROM sqlite_sequence WHERE name = 'Familles'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();
        }

        public List<Familles> GetAllFamilles()
        {
            Query = "SELECT * FROM Familles";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();

            var Reader = Command.ExecuteReader();
            var Familles = new List<Familles>();

            while (Reader.Read())
            {
                var RefFamille = Reader.GetInt32(0);
                var FamilleName = Reader.GetString(1);

                Familles.Add(new Familles(RefFamille, FamilleName));
            }

            Reader.Close();

            return Familles;
        }

        public void AddFamille(Familles Famille)
        {
            Query = "INSERT INTO Familles (Nom) values('" + Famille.FamilleName + "')";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();
            Famille.RefFamille = GetRefFamille(Famille.FamilleName);
        }

        public int GetRefFamille(string FamilleName)
        {
            Query = "SELECT RefFamille FROM Familles WHERE Nom = '" + FamilleName + "'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();

            var RefFamille = Command.ExecuteScalar().ToString().Trim();

            return Int32.Parse(RefFamille);
        }

        public List<Familles> FindFamillesByMarqueName(string MarqueName)
        {
            Query = "SELECT * FROM Familles WHERE RefFamille IN(SELECT RefFamille FROM SousFamilles WHERE RefSousFamille IN (SELECT RefSousFamille FROM Articles WHERE RefMarque = (SELECT RefMarque FROM Marques WHERE Nom = '" + MarqueName + "')))";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            var Reader = Command.ExecuteReader();

            List<Familles> Familles = new List<Familles>();
            while (Reader.Read())
            {
                var RefFamille = Reader.GetInt32(0);
                var FamilleName = Reader.GetString(1);

                Familles Famille = new Familles(RefFamille, FamilleName);
                Familles.Add(Famille);
            }
            Reader.Close();
            return Familles;
        }

        public Boolean FindFamilleByFamilleName(string FamilleName)
        {
            Query = "SELECT * FROM Familles WHERE Nom = '" + FamilleName + "'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            var Reader = Command.ExecuteReader();

            if (Reader.Read())
                return true;
            else
                return false;
        }

        public void DeleteFamille(Familles Famille)
        {
            Query = "DELETE FROM Familles WHERE Nom = '" + Famille.FamilleName + "' ; DELETE FROM Articles WHERE RefSousFamille IN (SELECT RefSousFamille FROM SousFamilles WHERE RefFamille = '" + Famille.RefFamille + "'); DELETE FROM SousFamilles WHERE RefFamille = '" + Famille.RefFamille + "'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();
        }

        public Familles FindFamillesByFamilleName(string FamilleName)
        {
            Query = "SELECT * FROM Familles WHERE Nom = '" + FamilleName + "'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            var Reader = Command.ExecuteReader();

            if (Reader.Read())
            {
                var RefFamille = Reader.GetInt32(0);
                Familles Famille = new Familles(RefFamille, FamilleName);
                Reader.Close();
                return Famille;
            }
            else
                return null;
        }

        public void ModifyFamille(Familles Famille)
        {
            Query = "UPDATE Familles SET Nom = '" + Famille.FamilleName + "' WHERE RefFamille = '" + Famille.RefFamille + "'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();
        }

        public int GetMaxRefFamille()
        {
            Query = "SELECT MAX(RefFamille) FROM Familles";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();

            var MaxRefFamille = Command.ExecuteScalar().ToString().Trim();

            return Int32.Parse(MaxRefFamille);
        }

        


    }
}
