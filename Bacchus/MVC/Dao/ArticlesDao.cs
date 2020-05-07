using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Bacchus.MVC.Model;
using System.Globalization;

namespace Bacchus.MVC.Dao
{
    class ArticlesDao
    {
        private SQLiteConnection Connection;
        private String Query;

        /// <summary>
        /// The constructor of ArticlesDao.
        /// </summary>
        public ArticlesDao()
        {
            this.Connection = Util.GetConnection.ConnectionSQLite();
            this.Query = null;
        }

        public void EmptyArticles()
        {
            Query = "DELETE FROM Articles; DELETE FROM sqlite_sequence WHERE name = 'Articles'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();
        }

        public List<Articles> GetAllArticles()
        {
            Query = "select * from Articles;";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();

            var Reader = Command.ExecuteReader();
            var Articles = new List<Articles>();
            MarquesDao MarquesDao = new MarquesDao();
            FamillesDao FamillesDao = new FamillesDao();
            SousFamillesDao SousFamillesDao = new SousFamillesDao();


            while (Reader.Read())
            {
                var RefArticle = Reader.GetString(0);
                var Description = Reader.GetString(1);
                var SousFamille = SousFamillesDao.FindSousFamilleByRefSousFamille(Reader.GetInt32(2));
                var Marque = MarquesDao.FindMarqueByRefMarque(Reader.GetInt32(3));
                
                var PrixHTs = Reader.GetString(4);
                var PrixHT = double.Parse(PrixHTs);
            
                var Quantite = Reader.GetInt32(5);
                Articles.Add(new Articles(RefArticle, Description, FamillesDao.FindFamillesByRefSousFamille(SousFamille.RefSousFamille), SousFamille, Marque, PrixHT , Quantite));
            }

            Reader.Close();

            return Articles;
        }

        public Boolean FindArticleByRefArticle(string RefArticle)
        {
            Query = "SELECT * FROM Articles WHERE RefArticle = '" + RefArticle + "'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            var Reader = Command.ExecuteReader();

            if (Reader.Read())
                return true;
            else
                return false;
        }

        public Articles FindArticlesByRefArticle(string RefArticle)
        {
            Query = "SELECT * FROM Articles WHERE RefArticle = '" + RefArticle + "'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            var Reader = Command.ExecuteReader();

            if (Reader.Read())
            {
                var Description = Reader.GetString(1);
                var RefSousFamille = Reader.GetInt32(2);
                var RefMarque = Reader.GetInt32(3);
                var PrixHTs = Reader.GetString(4);
                var PrixHT = double.Parse(PrixHTs);
                var Quantite = Reader.GetInt32(5);
                FamillesDao FamillesDao = new FamillesDao();
                SousFamillesDao SousFamillesDao = new SousFamillesDao();
                MarquesDao MarquesDao = new MarquesDao();

                Articles Article = new Articles(RefArticle, Description, FamillesDao.FindFamillesByRefSousFamille(RefSousFamille),
                    SousFamillesDao.FindSousFamilleByRefSousFamille(RefSousFamille),
                    MarquesDao.FindMarqueByRefMarque(RefMarque), PrixHT, Quantite);
                Reader.Close();
                return Article;
            }
            else
                return null;
        }

        public Boolean FindArticleByFamilleName(string FamilleName)
        {
            Query = "SELECT * FROM Articles WHERE RefSousFamille = ( SELECT RefSousFamille FROM SousFamilles WHERE RefFamille = (SELECT RefFamille FROM Familles WHERE Nom = '" + FamilleName + "'))";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            var Reader = Command.ExecuteReader();

            if (Reader.Read())
                return true;
            else
                return false;
        }

        public void AddArticle(Articles Article)
        {
            Query = "INSERT INTO Articles (RefArticle, Description, RefSousFamille, RefMarque, PrixHT" +
                    ", Quantite) values('" + Article.RefArticle + "','" + Article.Description + "','" + Article.SousFamille.RefSousFamille + "'," +
                    "'" + Article.Marque.RefMarque + "','" + Article.PrixHT + "','" + Article.Quantite + "')";
            
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();
        }

        public List<Articles> FindArticlesByMarqueName(string MarqueName)
        {
            Query = "SELECT* FROM Articles WHERE RefMarque = (SELECT RefMarque FROM Marques WHERE Nom = '" + MarqueName + "')";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            var Reader = Command.ExecuteReader();

            List<Articles> Articles = new List<Articles>();
            FamillesDao FamillesDao = new FamillesDao();
            SousFamillesDao SousFamillesDao = new SousFamillesDao();
            MarquesDao MarquesDao = new MarquesDao();
            while (Reader.Read())
            {
                var RefArticle = Reader.GetString(0);
                var Description = Reader.GetString(1);
                var RefSousFamille = Reader.GetInt32(2);
                var RefMarque = Reader.GetInt32(3);
                var PrixHTs = Reader.GetString(4);
                var PrixHT = double.Parse(PrixHTs);
                var Quantite = Reader.GetInt32(5);

                Articles Article = new Articles(RefArticle, Description, FamillesDao.FindFamillesByRefSousFamille(RefSousFamille),
                    SousFamillesDao.FindSousFamilleByRefSousFamille(RefSousFamille),
                    MarquesDao.FindMarqueByRefMarque(RefMarque), PrixHT, Quantite);
                Articles.Add(Article);
            }
            Reader.Close();
            return Articles;
        }

        public List<Articles> FindArticlesBySousFamilleName(string SousFamilleName)
        {
            Query = "SELECT * FROM Articles WHERE RefSousFamille = (SELECT RefSousFamille FROM SousFamilles WHERE Nom = '" + SousFamilleName + "')";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            var Reader = Command.ExecuteReader();

            List<Articles> Articles = new List<Articles>();
            FamillesDao FamillesDao = new FamillesDao();
            SousFamillesDao SousFamillesDao = new SousFamillesDao();
            MarquesDao MarquesDao = new MarquesDao();
            while (Reader.Read())
            {
                var RefArticle = Reader.GetString(0);
                var Description = Reader.GetString(1);
                var RefSousFamille = Reader.GetInt32(2);
                var RefMarque = Reader.GetInt32(3);
                var PrixHTs = Reader.GetString(4);
                var PrixHT = double.Parse(PrixHTs);
                var Quantite = Reader.GetInt32(5);

                Articles Article = new Articles(RefArticle, Description, FamillesDao.FindFamillesByRefSousFamille(RefSousFamille),
                    SousFamillesDao.FindSousFamilleByRefSousFamille(RefSousFamille),
                    MarquesDao.FindMarqueByRefMarque(RefMarque), PrixHT, Quantite);
                Articles.Add(Article);
            }
            Reader.Close();
            return Articles;
        }

        public void DeleteArticle(Articles Article)
        {
            Query = "DELETE FROM Articles WHERE RefArticle = '" + Article.RefArticle + "'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();
        }

        public void ModifyArticle(Articles Article)
        {
            Query = "UPDATE Articles SET Description = '" + Article.Description + "', RefSousFamille = '" + Article.SousFamille.RefSousFamille + "', " +
                "RefMarque = '" + Article.Marque.RefMarque + "', PrixHT = '" + Article.PrixHT + "', Quantite = '" + Article.Quantite + "' WHERE RefArticle = '"+ Article.RefArticle +"'";
            Console.WriteLine(Query);
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();
        }

        /// <summary>
        /// Verify the exist of an article by the reference.
        /// </summary>
        /// <param name="RefArticle"></param>
        /// <returns>True if exists. False if not.</returns>






        /// <summary>
        /// Add an article to the database.
        /// </summary>
        /// <param name="Article"></param>
        /**public void AddArticle(Articles Article)
        {
            Query = "INSERT INTO Articles (RefArticle, Description, RefSousFamille, RefMarque, PrixHT" +
                    ", Quantite) values('" + Article.RefArticle + "','" + Article.Description + "','" + Article.SousFamille.RefSousFamille + "'," +
                    "'" + Article.Marque.RefMarque + "','" + Article.PrixHT + "','" + Article.Quantite + "')";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();
        }

        /// <summary>
        /// Delete an article from the databse.
        /// </summary>
        /// <param name="Article"></param>
        public void DeleteArticle(Articles Article)
        {
            Query = "DELETE FROM Articles WHERE RefArticle = '" + Article.RefArticle + "'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();
        }

        /// <summary>
        /// Update the values of an article.
        /// </summary>
        /// <param name="Article"></param>
        public void ModifyArticle(Articles Article)
        {
            Query = "UPDATE Articles SET Description = '" + Article.Description + "', RefSousFamille = '" + Article.SousFamille.RefSousFamille + "', " +
                "RefMarque = '" + Article.Marque.RefMarque + "', PrixHT = '" + Article.PrixHT + "', Quantite = '" + Article.Quantite + "' WHERE RefArticle = '" + Article.RefArticle + "'";
            Console.WriteLine(Query);
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();
        }

        /// <summary>
        /// Get all the articles in the database.
        /// </summary>
        /// <returns>A list of class <c>Articles</c>c>.</returns>
        public List<Articles> GetAllArticles()
        {
            Query = "SELECT * FROM Articles";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();

            var Reader = Command.ExecuteReader();
            var Articles = new List<Articles>();
            MarquesController MarqueController = new MarquesController();
            FamillesController FamilleController = new FamillesController();
            SousFamillesController SousFamilleController = new SousFamillesController();

            while (Reader.Read())
            {
                var RefArticle = Reader.GetString(0);
                var Description = Reader.GetString(1);
                var SousFamille = SousFamilleController.FindSousFamilleByRefSousFamille(Reader.GetInt32(2));
                var Marque = MarqueController.FindMarqueByRefMarque(Reader.GetInt32(3));
                var PrixHTS = Reader.GetString(4);
                var PrixHT = float.Parse(PrixHTS, CultureInfo.InvariantCulture.NumberFormat);
                var Quantite = Reader.GetInt32(5);

                Articles.Add(new Articles(RefArticle, Description, FamilleController.FindFamillesByRefSousFamille(SousFamille.RefSousFamille), SousFamille, Marque, PrixHT, Quantite));
            }

            Reader.Close();

            return Articles;
        }

        /// <summary>
        /// Clear all the articles in the database.
        /// </summary>
        public void EmptyArticles()
        {
            Query = "DELETE FROM Articles; DELETE FROM sqlite_sequence WHERE name = 'Articles'";
            SQLiteCommand Command = new SQLiteCommand(Query, Connection);
            Command.ExecuteNonQuery();
        }**/

    }
}
