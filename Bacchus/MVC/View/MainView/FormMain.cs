using Bacchus.MVC.Controller;
using Bacchus.MVC.Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Bacchus.MVC.View.ArticlesView;
using System.Text;
using Bacchus.MVC.View.MarquesView;
using Bacchus.MVC.View.FamillesView;
using Bacchus.MVC.View.SousFamillesView;

namespace Bacchus.MVC.View.MainView
{
    public partial class FormMain : Form
    {
        private TreeNode Node;
        private List<string> FamilleNames;
        private List<string> SousFamilleNames;
        private List<string> MarqueNames;
        private ArticlesController ArticlesController;
        private ExportController ExportController;
        private ImportController ImportController;
        private ModelListController ModelListController;
        private SousFamilleController SousFamilleController;
        private MarqueController MarqueController;
        private FamilleController FamilleController;

        public FormMain()
        {
            InitializeComponent();
            this.FamilleNames = new List<string>();
            this.SousFamilleNames = new List<string>();
            this.MarqueNames = new List<string>();
            ArticlesController = new ArticlesController();
            ExportController = new ExportController();
            ImportController = new ImportController();
            ModelListController = new ModelListController();
            SousFamilleController = new SousFamilleController();
            MarqueController = new MarqueController();
            FamilleController = new FamilleController();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void importerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormImport FormImport = new FormImport { StartPosition = FormStartPosition.CenterParent };
            FormImport.ShowDialog(this);
        }

        private void exporterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormExport FormExport = new FormExport { StartPosition = FormStartPosition.CenterParent };
            FormExport.ShowDialog(this);
        }

        public void LoadListView()
        {
            ModelList ModelList = new ModelList();
            ListView.GridLines = true;
            ListView.View = System.Windows.Forms.View.Details;
            ModelList = ModelListController.GetAllModelList();

            Node = TreeView.SelectedNode;

            if (Node.Text.Equals("Tous les articles"))
            {
                ListView.Clear();

                ListView.Columns.Add("Référence", "Référence");
                ListView.Columns.Add("Description", "Description");
                ListView.Columns.Add("Famille", "Famille");
                ListView.Columns.Add("Sous-famille", "Sous-famille");
                ListView.Columns.Add("Marque", "Marque");
                ListView.Columns.Add("Prix", "Prix H.T.");
                ListView.Columns.Add("Quantité", "Quantité");

                foreach (var Article in ModelList.Articles)
                {
                    ListView.Items.Add(new ListViewItem(Article.InfoTable()));
                }

                ListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                ListView.Text = "Articles";
                ToolStripStatusLabel1.Text = "Tous les articles: " + ModelList.Articles.Count;
            }

            else if (Node.Text.Equals("Familles"))
            {
                ListView.Clear();

                ListView.Columns.Add("Description", "Description");

                this.ListView.ListViewItemSorter = new Util.ListViewItemComparer(0, ListView.Sorting);


                if (Node.Nodes.Count == 0)
                {
                    foreach (var Famille in ModelList.Familles)
                    {
                        ListView.Items.Add(new ListViewItem(Famille.InfoTable()));
                        TreeView.Nodes[1].Nodes.Add(Famille.FamilleName);
                        FamilleNames.Add(Famille.FamilleName);
                    }
                }
                else
                {
                    foreach (var Famille in ModelList.Familles)
                    {
                        ListView.Items.Add(new ListViewItem(Famille.InfoTable()));
                        FamilleNames.Add(Famille.FamilleName);
                    }
                }

                ListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                ListView.Text = "Familles";
                ToolStripStatusLabel1.Text = "Familles: " + ModelList.Familles.Count;
            }

            else if (Node.Text.Equals("Marques"))
            {
                ListView.Clear();

                ListView.Columns.Add("Description", "Description");

                this.ListView.ListViewItemSorter = new Util.ListViewItemComparer(0, ListView.Sorting);

                if (Node.Nodes.Count == 0)
                {
                    foreach (var Marque in ModelList.Marques)
                    {
                        ListView.Items.Add(new ListViewItem(Marque.InfoTable()));
                        TreeView.Nodes[2].Nodes.Add(Marque.MarqueName);
                        MarqueNames.Add(Marque.MarqueName);
                    }
                }
                else
                {
                    foreach (var Marque in ModelList.Marques)
                    {
                        ListView.Items.Add(new ListViewItem(Marque.InfoTable()));
                        MarqueNames.Add(Marque.MarqueName);
                    }
                }

                ListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                ListView.Text = "Marques";
                ToolStripStatusLabel1.Text = "Marques: " + ModelList.Marques.Count;
            }

            else if (FamilleNames.Contains(Node.Text))
            {
                List<SousFamilles> SousFamilles = SousFamilleController.FindSousFamillesByFamilleName(Node.Text);
                ListView.Clear();
                ListView.Columns.Add("Description", "Description");

                this.ListView.ListViewItemSorter = new Util.ListViewItemComparer(0, ListView.Sorting);

                if (Node.Nodes.Count == 0)
                {
                    foreach (var SousFamille in SousFamilles)
                    {
                        TreeView.Nodes[1].Nodes[FamilleNames.IndexOf(Node.Text)].Nodes.Add(SousFamille.SousFamilleName);
                        ListView.Items.Add(new ListViewItem(SousFamille.InfoTable()));
                        SousFamilleNames.Add(SousFamille.SousFamilleName);
                    }
                }
                else
                {
                    foreach (var SousFamille in SousFamilles)
                    {
                        ListView.Items.Add(new ListViewItem(SousFamille.InfoTable()));
                        SousFamilleNames.Add(SousFamille.SousFamilleName);
                    }
                }

                ListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                ListView.Text = "SousFamilles";
                ToolStripStatusLabel1.Text = "SousFamilles de " + Node.Text + ": " + SousFamilles.Count;
            }

            else if (MarqueNames.Contains(Node.Text))
            {
                List<Articles> Articles = ArticlesController.FindArticlesByMarqueName(Node.Text);
                ListView.Clear();

                ListView.Columns.Add("Référence", "Référence");
                ListView.Columns.Add("Description", "Description");
                ListView.Columns.Add("Famille", "Famille");
                ListView.Columns.Add("Sous-famille", "Sous-famille");
                ListView.Columns.Add("Marque", "Marque");
                ListView.Columns.Add("Prix", "Prix H.T.");
                ListView.Columns.Add("Quantité", "Quantité");

                foreach (var Article in Articles)
                {
                    ListView.Items.Add(new ListViewItem(Article.InfoTable()));
                }

                ListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                ListView.Text = "Articles";
                ToolStripStatusLabel1.Text = "Articles de Marque " + Node.Text + ": " + Articles.Count;
            }

            else if (SousFamilleNames.Contains(Node.Text))
            {
                List<Articles> Articles = ArticlesController.FindArticlesBySousFamilleName(Node.Text);
                ListView.Clear();

                ListView.Columns.Add("Référence", "Référence");
                ListView.Columns.Add("Description", "Description");
                ListView.Columns.Add("Famille", "Famille");
                ListView.Columns.Add("Sous-famille", "Sous-famille");
                ListView.Columns.Add("Marque", "Marque");
                ListView.Columns.Add("Prix", "Prix H.T.");
                ListView.Columns.Add("Quantité", "Quantité");

                foreach (var Article in Articles)
                {
                    ListView.Items.Add(new ListViewItem(Article.InfoTable()));
                }

                ListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                ListView.Text = "Articles";
                ToolStripStatusLabel1.Text = "Articles de Sous-famille " + Node.Text + ": " + Articles.Count;
            }

            TreeView.ExpandAll();
            ListView.Sort();
            ListView.Show();
        }

        private void actualiserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LoadListView();
        }

        private void TreeView_MouseRightClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                TreeView.ContextMenuStrip = null;
                
                contextMenuStrip_TreeView.Show(TreeView, e.Location);
            }
        }

        private void UpdateTreeView()
        {
            if (TreeView.Nodes[1].Nodes.Count != 0)
                TreeView.Nodes[1].Nodes.Clear();
            if (TreeView.Nodes[2].Nodes.Count != 0)
                TreeView.Nodes[2].Nodes.Clear();
        }

        private void viderLaBaseDeDonnéesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ModelListController ModelListController = new ModelListController();
            ModelListController.EmptyDataBase();
            LoadListView();
            UpdateTreeView();
        }

        private void TreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            LoadListView();
        }

        private void Modify_Article()
        {
            Articles Article = ArticlesController.Modify_Article(ListView.FocusedItem);
            if (Article != null)
            {
                ModifyArticle ModifyArticle = new ModifyArticle(Article) { StartPosition = FormStartPosition.CenterParent };
                ModifyArticle.ShowDialog(this);
            }
            else
                MessageBox.Show("Article doesn't exist!");
        }

        private void ListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Modify_Article();
        }

        private void ListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            int ColumnIndex = e.Column;
            SetGroups(ListView.Columns[ColumnIndex].Text, ColumnIndex);
        }

        private void SetGroups(string ColumnName, int ColumnIndex)
        {
            ModelList ModelList = new ModelList();
            ModelList = ModelListController.GetAllModelList();
            this.ListView.Groups.Clear();

            if (TreeView.SelectedNode == TreeView.Nodes[0])
            {
                switch (ColumnName)
                {
                    case "Description":
                        for (int i = 0; i < 26; i++)
                        {
                            string Group = ((char)(65 + i)).ToString();
                            this.ListView.Groups.Add(new ListViewGroup(Group, HorizontalAlignment.Left));
                        }
                        this.ListView.Groups.Add(new ListViewGroup("Others", HorizontalAlignment.Left));
                        //foreach (var Article in ModelList.Articles)
                        //{
                        for (int k = 0; k < ModelList.Articles.Count; k++)
                        {
                            var HeadLetter = (int)Encoding.Default.GetBytes(this.ListView.Items[k].SubItems[ColumnIndex].Text.Split()[0])[0];
                            if (HeadLetter <= 90 && HeadLetter >= 65)
                                this.ListView.Items[k].Group = this.ListView.Groups[(HeadLetter - 65)];
                            else if (HeadLetter >= 97 && HeadLetter <= 122)
                                this.ListView.Items[k].Group = this.ListView.Groups[(HeadLetter - 97)];
                            else
                                this.ListView.Items[k].Group = this.ListView.Groups[26];
                        }
                        //}
                        break;

                    case "Famille":
                        for (int i = 0; i < ModelList.Familles.Count; i++)
                        {
                            this.ListView.Groups.Add(new ListViewGroup(ModelList.Familles[i].FamilleName, HorizontalAlignment.Left));
                        }
                        var j = 0;
                        foreach (var Famille in ModelList.Familles)
                        {
                            for (int k = 0; k < ModelList.Articles.Count; k++)
                            {
                                if (this.ListView.Items[k].SubItems[ColumnIndex].Text == Famille.FamilleName)
                                    this.ListView.Items[k].Group = this.ListView.Groups[j];
                            }
                            j++;
                        }
                        break;

                    case "Sous-famille":
                        for (int i = 0; i < ModelList.SousFamilles.Count; i++)
                        {
                            this.ListView.Groups.Add(new ListViewGroup(ModelList.SousFamilles[i].SousFamilleName, HorizontalAlignment.Left));
                        }
                        j = 0;
                        foreach (var SousFamille in ModelList.SousFamilles)
                        {
                            for (int k = 0; k < ModelList.Articles.Count; k++)
                            {
                                if (this.ListView.Items[k].SubItems[ColumnIndex].Text == SousFamille.SousFamilleName)
                                    this.ListView.Items[k].Group = this.ListView.Groups[j];
                            }
                            j++;
                        }
                        break;

                    case "Marque":
                        for (int i = 0; i < ModelList.Marques.Count; i++)
                        {
                            this.ListView.Groups.Add(new ListViewGroup(ModelList.Marques[i].MarqueName, HorizontalAlignment.Left));
                        }
                        j = 0;
                        foreach (var Marque in ModelList.Marques)
                        {
                            for (int k = 0; k < ModelList.Articles.Count; k++)
                            {
                                if (this.ListView.Items[k].SubItems[ColumnIndex].Text == Marque.MarqueName)
                                    this.ListView.Items[k].Group = this.ListView.Groups[j];
                            }
                            j++;
                        }
                        break;

                    default:
                        if (ListView.Sorting == SortOrder.Ascending)
                            ListView.Sorting = SortOrder.Descending;
                        else
                            ListView.Sorting = SortOrder.Ascending;

                        ListView.Sort();
                        this.ListView.ListViewItemSorter = new Util.ListViewItemComparer(ColumnIndex, ListView.Sorting);
                        break;
                }
            }

            else if (TreeView.SelectedNode == TreeView.Nodes[1] || TreeView.SelectedNode.Parent == TreeView.Nodes[1] || TreeView.SelectedNode == TreeView.Nodes[2])
            {
                if (ColumnName == "Description")
                {
                    if (ListView.Sorting == SortOrder.Ascending)
                        ListView.Sorting = SortOrder.Descending;
                    else
                        ListView.Sorting = SortOrder.Ascending;

                    ListView.Sort();
                    this.ListView.ListViewItemSorter = new Util.ListViewItemComparer(ColumnIndex, ListView.Sorting);
                }
            }

            else if (TreeView.SelectedNode.Parent.Parent == TreeView.Nodes[1])
            {
                switch (ColumnName)
                {
                    case "Description":

                        for (int i = 0; i < 26; i++)
                        {
                            string Group = ((char)(65 + i)).ToString();
                            this.ListView.Groups.Add(new ListViewGroup(Group, HorizontalAlignment.Left));
                        }

                        this.ListView.Groups.Add(new ListViewGroup("Others", HorizontalAlignment.Left));

                        for (int k = 0; k < ArticlesController.FindArticlesBySousFamilleName(TreeView.SelectedNode.Text).Count; k++)
                        {
                            var HeadLetter = (int)Encoding.Default.GetBytes(this.ListView.Items[k].SubItems[ColumnIndex].Text.Split()[0])[0];
                            if (HeadLetter >= 65 && HeadLetter <= 90)
                                this.ListView.Items[k].Group = this.ListView.Groups[(HeadLetter - 65)];
                            else if (HeadLetter >= 97 && HeadLetter <= 122)
                                this.ListView.Items[k].Group = this.ListView.Groups[(HeadLetter - 97)];
                            else
                                //change
                                this.ListView.Items[k].Group = this.ListView.Groups[26];
                        }

                        break;

                    case "Famille":

                        this.ListView.Groups.Add(new ListViewGroup(TreeView.SelectedNode.Parent.Text, HorizontalAlignment.Left));

                        for (int k = 0; k < ArticlesController.FindArticlesBySousFamilleName(TreeView.SelectedNode.Text).Count; k++)
                        {
                            if (this.ListView.Items[k].SubItems[ColumnIndex].Text == TreeView.SelectedNode.Parent.Text)
                                this.ListView.Items[k].Group = this.ListView.Groups[0];
                        }

                        break;

                    case "Sous-famille":

                        this.ListView.Groups.Add(new ListViewGroup(TreeView.SelectedNode.Text, HorizontalAlignment.Left));

                        for (int k = 0; k < ArticlesController.FindArticlesBySousFamilleName(TreeView.SelectedNode.Text).Count; k++)
                        {
                            if (this.ListView.Items[k].SubItems[ColumnIndex].Text == TreeView.SelectedNode.Text)
                                this.ListView.Items[k].Group = this.ListView.Groups[0];
                        }
                        break;

                    case "Marque":

                        for (int i = 0; i < MarqueController.FindMarquesBySousFamilleName(TreeView.SelectedNode.Text).Count; i++)
                        {
                            this.ListView.Groups.Add(new ListViewGroup(MarqueController.FindMarquesBySousFamilleName(TreeView.SelectedNode.Text)[i].MarqueName, HorizontalAlignment.Left));
                        }
                        var j = 0;
                        foreach (var Marque in MarqueController.FindMarquesBySousFamilleName(TreeView.SelectedNode.Text))
                        {
                            for (int k = 0; k < ArticlesController.FindArticlesBySousFamilleName(TreeView.SelectedNode.Text).Count; k++)
                            {
                                if (this.ListView.Items[k].SubItems[ColumnIndex].Text == Marque.MarqueName)
                                    this.ListView.Items[k].Group = this.ListView.Groups[j];
                            }
                            j++;
                        }
                        break;

                    default:
                        if (ListView.Sorting == SortOrder.Ascending)
                            ListView.Sorting = SortOrder.Descending;
                        else
                            ListView.Sorting = SortOrder.Ascending;

                        ListView.Sort();
                        this.ListView.ListViewItemSorter = new Util.ListViewItemComparer(ColumnIndex, ListView.Sorting);
                        break;
                }
            }

            else if (TreeView.SelectedNode.Parent == TreeView.Nodes[2])
            {
                switch (ColumnName)
                {
                    case "Description":
                        for (int i = 0; i < 26; i++)
                        {
                            string Group = ((char)(65 + i)).ToString();
                            this.ListView.Groups.Add(new ListViewGroup(Group, HorizontalAlignment.Left));
                        }

                        this.ListView.Groups.Add(new ListViewGroup("Others", HorizontalAlignment.Left));

                        for (int k = 0; k < ArticlesController.FindArticlesByMarqueName(TreeView.SelectedNode.Text).Count; k++)
                        {
                            var HeadLetter = (int)Encoding.Default.GetBytes(this.ListView.Items[k].SubItems[ColumnIndex].Text.Split()[0])[0];
                            if (HeadLetter <= 90 && HeadLetter >= 65)
                                this.ListView.Items[k].Group = this.ListView.Groups[(HeadLetter - 65)];
                            else if (HeadLetter >= 97 && HeadLetter <= 122)
                                this.ListView.Items[k].Group = this.ListView.Groups[(HeadLetter - 97)];
                            else
                                this.ListView.Items[k].Group = this.ListView.Groups[26];
                        }

                        break;

                    case "Famille":
                        for (int i = 0; i < FamilleController.FindFamillesByMarqueName(TreeView.SelectedNode.Text).Count; i++)
                        {
                            this.ListView.Groups.Add(new ListViewGroup(FamilleController.FindFamillesByMarqueName(TreeView.SelectedNode.Text)[i].FamilleName, HorizontalAlignment.Left));
                        }
                        var j = 0;
                        foreach (var Famille in FamilleController.FindFamillesByMarqueName(TreeView.SelectedNode.Text))
                        {
                            for (int k = 0; k < ArticlesController.FindArticlesByMarqueName(TreeView.SelectedNode.Text).Count; k++)
                            {
                                if (this.ListView.Items[k].SubItems[ColumnIndex].Text == Famille.FamilleName)
                                    this.ListView.Items[k].Group = this.ListView.Groups[j];
                            }
                            j++;
                        }
                        break;

                    case "Sous-famille":
                        for (int i = 0; i < SousFamilleController.FindSousFamillesByMarqueName(TreeView.SelectedNode.Text).Count; i++)
                        {
                            this.ListView.Groups.Add(new ListViewGroup(SousFamilleController.FindSousFamillesByMarqueName(TreeView.SelectedNode.Text)[i].SousFamilleName, HorizontalAlignment.Left));
                        }
                        j = 0;
                        foreach (var SousFamille in SousFamilleController.FindSousFamillesByMarqueName(TreeView.SelectedNode.Text))
                        {
                            for (int k = 0; k < ArticlesController.FindArticlesByMarqueName(TreeView.SelectedNode.Text).Count; k++)
                            {
                                if (this.ListView.Items[k].SubItems[ColumnIndex].Text == SousFamille.SousFamilleName)
                                    this.ListView.Items[k].Group = this.ListView.Groups[j];
                            }
                            j++;
                        }
                        break;

                    case "Marque":
                        this.ListView.Groups.Add(new ListViewGroup(MarqueController.FindMarquesByMarqueName(TreeView.SelectedNode.Text).MarqueName, HorizontalAlignment.Left));

                        for (int k = 0; k < ArticlesController.FindArticlesByMarqueName(TreeView.SelectedNode.Text).Count; k++)
                        {
                            if (this.ListView.Items[k].SubItems[ColumnIndex].Text == MarqueController.FindMarquesByMarqueName(TreeView.SelectedNode.Text).MarqueName)
                                this.ListView.Items[k].Group = this.ListView.Groups[0];
                        }

                        break;

                    default:
                        if (ListView.Sorting == SortOrder.Ascending)
                            ListView.Sorting = SortOrder.Descending;
                        else
                            ListView.Sorting = SortOrder.Ascending;

                        ListView.Sort();
                        this.ListView.ListViewItemSorter = new Util.ListViewItemComparer(ColumnIndex, ListView.Sorting);
                        break;
                }
            }
        }

        private void Delete_Article()
        {
            ArticlesController.Delete_Article(ListView.FocusedItem);
            
        }

        private void ListView_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    LoadListView();
                    break;
                case Keys.Delete:
                    Delete_Article();
                    LoadListView();
                    break;
                case Keys.Enter:
                    Modify_Article();
                    break;
            }
        }

        public void ListView_MouseRightClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ListView.ContextMenuStrip = null;
                ContextMenuStrip_ListView.Show(ListView, e.Location);
            }
        }

        private void Add_Article()
        {
            AddArticle AddArticle = new AddArticle() { StartPosition = FormStartPosition.CenterParent };
            AddArticle.ShowDialog(this);
        }

        private string Add_Famille()
        {
            AddFamille AddFamille = new AddFamille() { StartPosition = FormStartPosition.CenterParent };
            AddFamille.ShowDialog(this);

            if (AddFamille.textBox_FamilleName.Text != "")
                FamilleNames.Add(AddFamille.textBox_FamilleName.Text);

            return AddFamille.textBox_FamilleName.Text;
        }


        private string Add_Marque()
        {
            AddMarque AddMarque = new AddMarque() { StartPosition = FormStartPosition.CenterParent };
            AddMarque.ShowDialog(this);

            if (AddMarque.textBox_MarqueName.Text != "")
                MarqueNames.Add(AddMarque.textBox_MarqueName.Text);

            return AddMarque.textBox_MarqueName.Text;
        }

        private string Add_SousFamille_CurrentNode()
        {
            AddSousFamille AddSousFamille = new AddSousFamille() { StartPosition = FormStartPosition.CenterParent };
            AddSousFamille.label_Famille_Name.Text = TreeView.SelectedNode.Text;
            AddSousFamille.ShowDialog(this);

            if (AddSousFamille.textBox_SousFamilleName.Text != "")
                SousFamilleNames.Add(AddSousFamille.textBox_SousFamilleName.Text);

            return AddSousFamille.textBox_SousFamilleName.Text;
        }

        private string Add_SousFamille_ParentNode()
        {
            AddSousFamille AddSousFamille = new AddSousFamille() { StartPosition = FormStartPosition.CenterParent };
            AddSousFamille.label_Famille_Name.Text = TreeView.SelectedNode.Parent.Text;
            AddSousFamille.ShowDialog(this);

            if (AddSousFamille.textBox_SousFamilleName.Text != "")
                SousFamilleNames.Add(AddSousFamille.textBox_SousFamilleName.Text);

            return AddSousFamille.textBox_SousFamilleName.Text;
        }



        private void ajouterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Node = TreeView.SelectedNode;

            if (Node.Text.Equals("Tous les articles"))
                MessageBox.Show("You can't add articles in the TreeView!");

            else if (Node.Text.Equals("Familles"))
            {
                var FamilleName = Add_Famille();
                if (FamilleName != "")
                    TreeView.Nodes[1].Nodes.Add(new TreeNode(FamilleName));
                LoadListView();
            }

            else if (Node.Text.Equals("Marques"))
            {
                var MarqueName = Add_Marque();
                if (MarqueName != "")
                    TreeView.Nodes[2].Nodes.Add(new TreeNode(MarqueName));
                LoadListView();
            }

            else if (FamilleNames.Contains(Node.Text))
            {
                var FamilleName = Add_SousFamille_CurrentNode();
                if (FamilleName != "")
                    TreeView.SelectedNode.Nodes.Add(new TreeNode(FamilleName));
                LoadListView();
            }

            else if (MarqueNames.Contains(Node.Text))
            {
                var MarqueName = Add_Marque();
                if (MarqueName != "")
                    TreeView.Nodes[2].Nodes.Add(new TreeNode(MarqueName));
                LoadListView();
            }

            else if (SousFamilleNames.Contains(Node.Text))
            {
                var SousFamilleName = Add_SousFamille_ParentNode();
                if (SousFamilleName != "")
                    TreeView.Nodes[1].Nodes[FamilleNames.IndexOf(TreeView.SelectedNode.Parent.Text)].Nodes.Add(new TreeNode(SousFamilleName));
                LoadListView();
            }

            ListView.Sort();
            ListView.Show();
        }

        private void toolStripMenuItemAjouter_Click(object sender, EventArgs e)
        {
            Console.WriteLine("123");
            AddArticle AddArticle = new AddArticle() { StartPosition = FormStartPosition.CenterParent };
            AddArticle.ShowDialog(this);
            LoadListView();
        }

        private void toolStripMenuItemSupprimer_Click(object sender, EventArgs e)
        {
            Delete_Article();
            LoadListView();
        }

        private void ToolStripMenuItemModifier_Click(object sender, EventArgs e)
        {
            Modify_Article();
            LoadListView();
        }

        private void Delete_Famille()
        {
            var FamilleName = TreeView.SelectedNode.Text;

            FamilleController.Delete_Famille(FamilleName, FamilleNames);
        }


        private void Delete_Marque()
        {
            var MarqueName = TreeView.SelectedNode.Text;

            MarqueController.Delete_Marque(MarqueName, MarqueNames);
        }

        private void Delete_SousFamille()
        {
            var SousFamilleName = TreeView.SelectedNode.Text;
            SousFamilleController.Delete_SousFamille(SousFamilleName, SousFamilleNames);
        }

        private void supprimerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Node = TreeView.SelectedNode;

            if (Node.Text.Equals("Tous les articles"))
                MessageBox.Show("You can't delete articles in the TreeView!");

            else if (Node.Text.Equals("Familles"))
                MessageBox.Show("Please select a famille!");

            else if (Node.Text.Equals("Marques"))
                MessageBox.Show("Please select a marque!");

            else if (FamilleNames.Contains(Node.Text))
            {
                Delete_Famille();
                TreeView.Nodes[1].Nodes.Remove(TreeView.SelectedNode);
                LoadListView();
            }

            else if (MarqueNames.Contains(Node.Text))
            {
                Delete_Marque();
                TreeView.Nodes[2].Nodes.Remove(TreeView.SelectedNode);
                LoadListView();
            }

            else if (SousFamilleNames.Contains(Node.Text))
            {
                Delete_SousFamille();
                TreeView.SelectedNode.Parent.Nodes.Remove(TreeView.SelectedNode);
                LoadListView();
            }

            ListView.Sort();
            ListView.Show();
        }

        private string Modify_Famille()
        {
            var FamilleName = TreeView.SelectedNode.Text;

            return FamilleController.Modify_Famille(FamilleName, FamilleNames, TreeView.SelectedNode.Text, this);
        }

        private string Modify_Marque()
        {
            var MarqueName = TreeView.SelectedNode.Text;

            return MarqueController.Modify_Marque(MarqueName, MarqueNames, TreeView.SelectedNode.Text, this);
        }

        private string Modify_SousFamille()
        {
            var SousFamilleName = TreeView.SelectedNode.Text;

            return SousFamilleController.Modify_SousFamille(SousFamilleName, SousFamilleNames, TreeView.SelectedNode.Parent.Text, TreeView.SelectedNode.Text, this);
        }

        private void modifierToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Node = TreeView.SelectedNode;

            if (Node.Text.Equals("Tous les articles"))
                MessageBox.Show("You can't modify articles in the TreeView!");

            else if (Node.Text.Equals("Familles"))
                MessageBox.Show("Please select a famille!");

            else if (Node.Text.Equals("Marques"))
                MessageBox.Show("Please select a marque!");

            else if (FamilleNames.Contains(Node.Text))
            {
                var FamilleName = Modify_Famille();
                if (FamilleName != "")
                    TreeView.SelectedNode.Text = FamilleName;
                LoadListView();
            }

            else if (MarqueNames.Contains(Node.Text))
            {
                var MarqueName = Modify_Marque();
                if (MarqueName != "")
                    TreeView.SelectedNode.Text = MarqueName;
                LoadListView();
            }

            else if (SousFamilleNames.Contains(Node.Text))
            {
                var SousFamilleName = Modify_SousFamille();
                if (SousFamilleName != "")
                    TreeView.SelectedNode.Text = SousFamilleName;
                LoadListView();
            }

            ListView.Sort();
            ListView.Show();
        }

        private void TreeView_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.F5:
                    UpdateTreeView();
                    break;
            }
        }

    }
}
