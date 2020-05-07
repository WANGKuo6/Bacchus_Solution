using Bacchus.MVC.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bacchus.MVC.Dao
{
    class SousFamillesDao
    {
        private SQLiteConnection Connection;
        private FamillesDao FamilleDao;
        private String Query;

        /// <summary>
        /// The constructor of SousFamillesDao.
        /// </summary>
        public SousFamillesDao()
        {
            this.Connection = Util.GetConnection.ConnectionSQLite();
            this.FamilleDao = new FamillesDao();
            this.Query = null;
        }

        public void EmptySousFamilles()
        {
            Query = "DELETE FROM SousFamilles; DELETE FROM sqlite_sequence WHERE name = 'SousFamilles'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();
        }

        public SousFamilles FindSousFamilleByRefSousFamille(int RefSousFamille)
        {
            Query = "SELECT * FROM SousFamilles WHERE RefSousFamille = '" + RefSousFamille + "'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();

            var Reader = Command.ExecuteReader();
            if (Reader.Read())
            {
                var RefFamille = Reader.GetInt32(1);
                var SousFamilleName = Reader.GetString(2);

                SousFamilles SousFamille = new SousFamilles(RefSousFamille, RefFamille, SousFamilleName);
                Reader.Close();
                return SousFamille;
            }
            else
                return null;
        }

        public void DeleteSousFamille(SousFamilles SousFamille)
        {
            Query = "DELETE FROM SousFamilles WHERE Nom = '" + SousFamille.SousFamilleName + "'; DELETE FROM Articles WHERE RefSousFamille = '" + SousFamille.RefSousFamille + "'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();
        }

        public List<SousFamilles> GetAllSousFamilles()
        {
            Query = "SELECT * FROM SousFamilles";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();

            var Reader = Command.ExecuteReader();
            var SousFamilles = new List<SousFamilles>();

            while (Reader.Read())
            {
                var RefSousFamille = Reader.GetInt32(0);
                var RefFamille = Reader.GetInt32(1);
                var SousFamilleName = Reader.GetString(2);

                SousFamilles.Add(new SousFamilles(RefSousFamille, RefFamille, SousFamilleName));
            }

            Reader.Close();

            return SousFamilles;
        }

        public Boolean FindSousFamilleBySousFamilleName(string SousFamilleName)
        {
            Query = "SELECT * FROM SousFamilles WHERE Nom = '" + SousFamilleName + "'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            var Reader = Command.ExecuteReader();

            if (Reader.Read())
                return true;
            else
                return false;
        }

        public void AddSousFamille(SousFamilles SousFamille, Familles Famille)
        {
            Query = "INSERT INTO SousFamilles (RefFamille, Nom) values('" + Famille.RefFamille + "', '" + SousFamille.SousFamilleName + "')";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();
            SousFamille.RefSousFamille = GetRefSousFamille(SousFamille.SousFamilleName);
            SousFamille.RefFamille = FamilleDao.GetRefFamille(Famille.FamilleName);
        }

        public int GetRefSousFamille(string SousFamilleName)
        {
            Query = "SELECT RefSousFamille FROM SousFamilles WHERE Nom = '" + SousFamilleName + "'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();

            var RefSousFamille = Command.ExecuteScalar().ToString().Trim();

            return Int32.Parse(RefSousFamille);
        }

        public List<SousFamilles> FindSousFamillesByFamilleName(string FamilleName)
        {
            Query = "SELECT * FROM SousFamilles WHERE RefFamille = (SELECT RefFamille FROM Familles WHERE Nom = '" + FamilleName + "')";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            var Reader = Command.ExecuteReader();

            List<SousFamilles> SousFamilles = new List<SousFamilles>();
            while (Reader.Read())
            {
                var RefSousFamille = Reader.GetInt32(0);
                var RefFamille = Reader.GetInt32(1);
                var SousFamilleName = Reader.GetString(2);

                SousFamilles SousFamille = new SousFamilles(RefSousFamille, RefFamille, SousFamilleName);
                SousFamilles.Add(SousFamille);
            }
            Reader.Close();
            return SousFamilles;
        }

        public List<SousFamilles> FindSousFamillesByMarqueName(string MarqueName)
        {
            Query = "SELECT * FROM SousFamilles WHERE RefSousFamille IN (SELECT RefSousFamille FROM Articles WHERE RefMarque = (SELECT RefMarque FROM Marques WHERE Nom = '" + MarqueName + "'))";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            var Reader = Command.ExecuteReader();

            List<SousFamilles> SousFamilles = new List<SousFamilles>();
            while (Reader.Read())
            {
                var RefSousFamille = Reader.GetInt32(0);
                var RefFamille = Reader.GetInt32(1);
                var SousFamilleName = Reader.GetString(2);

                SousFamilles SousFamille = new SousFamilles(RefSousFamille, RefFamille, SousFamilleName);
                SousFamilles.Add(SousFamille);
            }
            Reader.Close();
            return SousFamilles;
        }

        public SousFamilles FindSousFamillesBySousFamilleName(string SousFamilleName)
        {
            Query = "SELECT * FROM SousFamilles WHERE Nom = '" + SousFamilleName + "'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            var Reader = Command.ExecuteReader();

            if (Reader.Read())
            {
                var RefSousFamille = Reader.GetInt32(0);
                var RefFamille = Reader.GetInt32(1);

                SousFamilles SousFamille = new SousFamilles(RefSousFamille, RefFamille, SousFamilleName);
                Reader.Close();
                return SousFamille;
            }
            else
                return null;
        }

        public void ModifySousFamille(SousFamilles SousFamille)
        {
            Query = "UPDATE SousFamilles SET Nom = '" + SousFamille.SousFamilleName + "' WHERE RefSousFamille = '" + SousFamille.RefSousFamille + "'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();
        }

        public int GetMaxRefSousFamille()
        {
            Query = "SELECT MAX(RefSousFamille) FROM SousFamilles";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();

            var MaxRefSousFamille = Command.ExecuteScalar().ToString().Trim();

            return Int32.Parse(MaxRefSousFamille);
        }


    }
}
