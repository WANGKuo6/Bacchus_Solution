using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bacchus.MVC.Dao;
using Bacchus.MVC.Model;
using Bacchus.MVC.Model;

namespace Bacchus.MVC.Controller
{
    class ModelListController
    {
        private ArticlesDao ArticleDao;
        private MarquesDao MarqueDao;
        private FamillesDao FamilleDao;
        private SousFamillesDao SousFamilleDao;

        public ModelListController()
        {
            ArticleDao = new ArticlesDao();
            MarqueDao = new MarquesDao();
            FamilleDao = new FamillesDao();
            SousFamilleDao = new SousFamillesDao();
        }

        public ModelList GetAllModelList()
        {
            ModelList ModelList = new ModelList();

            ModelList.Articles = ArticleDao.GetAllArticles();
            ModelList.Familles = FamilleDao.GetAllFamilles();
            ModelList.Marques = MarqueDao.GetAllMarques();
            ModelList.SousFamilles = SousFamilleDao.GetAllSousFamilles();
            return ModelList;
        }

        /// <summary>
        /// Empty the database.
        /// </summary>
        public void EmptyDataBase()
        {
            ArticleDao.EmptyArticles();

            FamilleDao.EmptyFamilles();

            SousFamilleDao.EmptySousFamilles();

            MarqueDao.EmptyMarques();

        }
    }
}
