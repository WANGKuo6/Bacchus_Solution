using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bacchus.MVC.View.MainView;
using Bacchus.MVC.Model;
using System.Windows.Forms;
using System.IO;
using Bacchus.MVC.Dao;

namespace Bacchus.MVC.Controller
{
    class ExportController
    {
        /// <summary>
        /// Write all the data to a csv file.
        /// </summary>
        /// <param name="FilePath"></param>
        /// <param name="FormExport"></param>
        public static void WriteFile(string ParamFilePath, FormExport FormExport)
        {
            try
            {
                ModelList ModelList = new ModelList();
                ModelListController ModelListController = new ModelListController();
                ModelList = ModelListController.GetAllModelList();
                FamillesDao FamillesDao = new FamillesDao();

                string HeaderCsv = null;

                if (ParamFilePath == "")
                {
                    if (MessageBox.Show("Please choose a file!", "ERROR") == DialogResult.OK)
                    {
                        using (OpenFileDialog openFileDialog = new OpenFileDialog())
                        {
                            openFileDialog.Filter = "csv files (*.csv)|*.csv";
                            openFileDialog.RestoreDirectory = true;

                            if (openFileDialog.ShowDialog() == DialogResult.OK)
                            {
                                ParamFilePath = openFileDialog.FileName;
                                FormExport.label_FichierExporte.Text = "FileName: " + System.IO.Path.GetFileName(ParamFilePath);
                            }
                        }
                    }
                }

                using (var StreamReader = new StreamReader(ParamFilePath, Encoding.Default))
                {
                    HeaderCsv = StreamReader.ReadLine();
                    StreamReader.Close();
                }

                using (var FileStream = new FileStream(ParamFilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite))
                {
                    FileStream.SetLength(0);
                    using (var StreamWriter = new StreamWriter(FileStream, Encoding.Default))
                    {
                        FormExport.progressBar.Maximum = ModelList.Articles.Count + 1;
                        FormExport.progressBar.Value = 1;

                        StreamWriter.WriteLine(HeaderCsv);
                        foreach (var Article in ModelList.Articles)
                        {
                            StreamWriter.WriteLine(Article.Description + ";" + Article.RefArticle + ";" + Article.Marque.MarqueName
                                + ";" + FamillesDao.FindFamillesByRefFamille(Article.SousFamille.RefFamille).FamilleName + ";" + Article.SousFamille.SousFamilleName + ";" + Article.PrixHT);
                            FormExport.progressBar.Value++;
                        }
                    }
                }

                string Message = ModelList.Articles.Count + " articles have been added!";

                MessageBox.Show(" Export success!\n" + Message, System.IO.Path.GetFileName(ParamFilePath));
            }
            catch
            {
                MessageBox.Show("Please close the selected file!");
            }

        }
    }
}
