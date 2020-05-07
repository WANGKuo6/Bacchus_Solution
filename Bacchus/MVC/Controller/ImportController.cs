using Bacchus.MVC.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bacchus.MVC.Dao;
using System.Windows.Forms;
using Bacchus.MVC.View.MainView;
using System.IO;
using System.Globalization;
using Bacchus.MVC.Model;
using System.Globalization;

namespace Bacchus.MVC.Controller
{
    class ImportController
    {
        public String FilePath = string.Empty;

         public ImportController()
        {

        }

        public String ChooseFile()
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "csv files (*.csv)|*.csv";
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    FilePath = openFileDialog.FileName;
                }
                return FilePath;
            }
        }

        public void CsvImport(bool Flag, string ParamFilePath, FormImport FormImport)
        {
            ModelList ModelList = new ModelList();
            ModelListController ModelListController = new ModelListController();
            ModelList = ModelListController.GetAllModelList();
            ArticlesDao ArticleDao = new ArticlesDao();
            FamillesDao FamilleDao = new FamillesDao();
            MarquesDao MarqueDao = new MarquesDao();
            SousFamillesDao SousFamilleDao = new SousFamillesDao();
            Articles Article = new Articles();
            Marques Marque = new Marques();
            SousFamilles SousFamille = new SousFamilles();
            Familles Famille = new Familles();
            try
            {
                int AddedArticles = 0;
                int ExistingArticles = 0;

                if (Flag == true)
                {
                    ModelList.Articles.Clear();
                    ModelList.Familles.Clear();
                    ModelList.Marques.Clear();
                    ModelList.SousFamilles.Clear();

                    ArticleDao.EmptyArticles();
                    SousFamilleDao.EmptySousFamilles();
                    MarqueDao.EmptyMarques();
                    FamilleDao.EmptyFamilles();
                }

                if (ParamFilePath == "")
                {
                    if (MessageBox.Show("Please choose a file!", "ERROR") == DialogResult.OK)
                    {
                        FormImport.label_FichierImporte.Text = "FileName: " + System.IO.Path.GetFileName(ChooseFile());
                    }
                }

                using (var StreamReader = new StreamReader(ParamFilePath, Encoding.Default))
                {
                    var NbLines = File.ReadAllLines(ParamFilePath).Length;

                    FormImport.progressBar.Maximum = NbLines;
                    FormImport.progressBar.Value = 1;

                    StreamReader.ReadLine();

                    string Line;
                    while ((Line = StreamReader.ReadLine()) != null)
                    {
                        var Values = Line.Split(';');
                        var Description = Values[0].Trim();
                        var RefArticle = Values[1].Trim();
                        var MarqueName = Values[2].Trim();
                        var FamilleName = Values[3].Trim();
                        var SousFamilleName = Values[4].Trim();
                        var Prix = Values[5].Trim();

                        if (Prix.IndexOf("\"") >= 0)
                            Prix = Prix.Replace("\"", "");

                        if (Prix.EndsWith(","))
                            Prix = Prix.Remove(Prix.Length - 1, 1);

                        if (Prix.IndexOf(",") != -1)
                        {
                            int StartIndex = 0;
                            int Count = 0;
                            while (true)
                            {
                                int Index = Prix.IndexOf(",", StartIndex);
                                if (Index != -1)
                                {
                                    Count++;
                                    StartIndex = Index + 1;
                                }
                                else
                                    break;
                            }

                            StartIndex = 0;
                            for (int i = 0; i < Count - 1; i++)
                            {
                                int Index = Prix.IndexOf(",", StartIndex);
                                StartIndex = Index + 1;
                                Prix = Prix.Remove(Index, Index);
                            }

                            Prix = Prix.Replace(",", ".");
                        }

                        var PrixHT = double.Parse(Prix, new CultureInfo("en-US"));

                        if (ArticleDao.FindArticleByRefArticle(RefArticle))
                        {
                            ExistingArticles += 1;
                            if (Flag == false)
                            {
                                Article = ArticleDao.FindArticlesByRefArticle(RefArticle);
                                if (Article.Description != Description)
                                    Article.Description = Description;
                                if (Article.PrixHT != PrixHT)
                                    Article.PrixHT = PrixHT;
                                if (Article.Quantite != 1)
                                    Article.Quantite = 1;
                                if (!MarqueDao.FindMarqueByMarqueName(MarqueName))
                                {
                                    Marque = new Marques(MarqueName);
                                    ModelList.Marques.Add(Marque);
                                    MarqueDao.AddMarque(Marque);
                                    Article.Marque = Marque;
                                }
                                if (!SousFamilleDao.FindSousFamilleBySousFamilleName(SousFamilleName))
                                {
                                    SousFamille = new SousFamilles(SousFamilleName);
                                    ModelList.SousFamilles.Add(SousFamille);
                                    SousFamilleDao.AddSousFamille(SousFamille, Famille);
                                    Article.SousFamille = SousFamille;
                                }
                            }
                            continue;
                        }
                        else
                        {
                            AddedArticles += 1;
                        }

                        if (!MarqueDao.FindMarqueByMarqueName(MarqueName))
                        {
                            Marque = new Marques(MarqueName);
                            ModelList.Marques.Add(Marque);
                            MarqueDao.AddMarque(Marque);
                        }

                        if (!ArticleDao.FindArticleByFamilleName(FamilleName))
                        {
                            Famille = new Familles(FamilleName);
                            ModelList.Familles.Add(Famille);
                            FamilleDao.AddFamille(Famille);
                        }

                        if (!SousFamilleDao.FindSousFamilleBySousFamilleName(SousFamilleName))
                        {
                            SousFamille = new SousFamilles(SousFamilleName);
                            ModelList.SousFamilles.Add(SousFamille);
                            SousFamilleDao.AddSousFamille(SousFamille, Famille);
                        }

                        Article = new Articles(RefArticle, Description, FamilleDao.FindFamillesByRefSousFamille(SousFamille.RefSousFamille), SousFamille, Marque, PrixHT, 1);
                        ModelList.Articles.Add(Article);
                        ArticleDao.AddArticle(Article);

                        FormImport.progressBar.Value++;
                    }
                    StreamReader.Close();
                }

                string Message = "Nombre d'articles ajoutés " + AddedArticles + "\n" +
                                 "Nombre d'articles anomalies " + ExistingArticles;

                MessageBox.Show(" Import success!\n" + Message, System.IO.Path.GetFileName(ParamFilePath));

                String FileDirectory = Path.GetDirectoryName(ParamFilePath);
                //if (FileDirectory != null)
                    //FormMain_FileSystemWatcher();
            }
            catch (System.IO.IOException)
            {
                MessageBox.Show("Please close the selected file!");
            }
        }


    }

}
