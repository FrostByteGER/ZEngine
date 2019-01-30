using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace AssetForge.Windows
{
    public partial class ContentBrowser : DockContent
    {
        private string _packagesFile;
        private IEnumerable<string> _packageResources;
        private List<string> _packageExtensions = new List<string>
        {
            ".pkg",
            ".cfg",
            ".ini"
        };

        public ContentBrowser()
        {
            InitializeComponent();
        }

        private void ContentBrowser_Load(object sender, System.EventArgs e)
        {
            imageList1.Images.Add("dir", DefaultIcons.FolderSmall);
            SetRootDirectory(AppDomain.CurrentDomain.BaseDirectory, _packageExtensions);
        }

        private void SetRootDirectory(string root, IEnumerable<string> filters = null)
        {
            if (!File.GetAttributes(root).HasFlag(FileAttributes.Directory)) throw new DirectoryNotFoundException("Path is not a directory!");
            projectTreeView.BeginUpdate();
            projectTreeView.Nodes.Clear();

            var stack = new Stack<TreeNode>();
            var rootDirectory = new DirectoryInfo(root);
            var node = new TreeNode(rootDirectory.Name)
            {
                Tag = rootDirectory,
                ImageKey = GetAssociatedIcon(root)
            };
            stack.Push(node);

            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                var directoryInfo = (DirectoryInfo)currentNode.Tag;
                foreach (var directory in directoryInfo.GetDirectories())
                {
                    var childDirectoryNode = new TreeNode(directory.Name)
                    {
                        Tag = directory,
                        ImageKey = GetAssociatedIcon(directory.FullName)
                    };
                    currentNode.Nodes.Add(childDirectoryNode);
                    stack.Push(childDirectoryNode);
                }
                var files = directoryInfo.EnumerateFiles();
                if (filters != null) files = files.Where(f => filters.Any(fl => f.Extension == fl));
                foreach (var file in files)
                {
                    var childNode = new TreeNode(file.Name);
                    GetAssociatedIcon(file.FullName);
                    childNode.ImageKey = file.Extension;
                    childNode.SelectedImageKey = file.Extension;
                    currentNode.Nodes.Add(childNode);
                }
            }

            projectTreeView.Nodes.Add(node);
            projectTreeView.EndUpdate();
        }

        private void DisplayCurrentFolderContent(TreeNode currentNode)
        {
            var directoryInfo = (DirectoryInfo)currentNode.Tag;
            currentFolderView.BeginUpdate();
            currentFolderView.Clear();
            foreach (var dir in directoryInfo.EnumerateDirectories())
            {
                currentFolderView.Items.Add(dir.Name + dir.Extension, dir.Name, GetAssociatedIcon(dir.FullName));
            }

            foreach (var file in directoryInfo.EnumerateFiles())
            {
                currentFolderView.Items.Add(file.Name + file.Extension, file.Name, GetAssociatedIcon(file.FullName));
            }
            currentFolderView.EndUpdate();
        }

        private string GetAssociatedIcon(string path)
        {
            if (File.GetAttributes(path).HasFlag(FileAttributes.Directory))
            {
                return "dir";
            }

            var file = new FileInfo(path);

            if (!imageList1.Images.ContainsKey(file.Extension))
            {
                var iconForFile = Icon.ExtractAssociatedIcon(file.FullName);
                if (iconForFile == null) throw new NullReferenceException("No Icon found for File!");
                imageList1.Images.Add(file.Extension, iconForFile);
            }
            return file.Extension;
        }

        private void LoadPackages_OnClick(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog
            {
                Title = "Choose a Packages File to Load",
                InitialDirectory = @"C:\Users",
                Filter = @"All files (*.*)|*.*|Packages (*.pkg)|*.pkg|Packages (*.cfg)|*.cfg|Packages (*.ini)|*.ini",
                FilterIndex = 2,
                RestoreDirectory = true,
                Multiselect = false
            };
            if (dlg.ShowDialog(this) == DialogResult.OK)
            {
                Console.WriteLine(dlg.FileName);
                Console.WriteLine(dlg.SafeFileName);
                _packagesFile = dlg.FileName;
                SetRootDirectory(Path.GetDirectoryName(_packagesFile), _packageExtensions);
            }
        }

        private void listView1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                
                if (currentFolderView.FocusedItem.Bounds.Contains(e.Location))
                    emptyContextMenuStrip.Show(Cursor.Position);
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node == null)
                return;
            switch (e.Button)
            {
                case MouseButtons.Left:
                    DisplayCurrentFolderContent(e.Node);
                    break;
                case MouseButtons.Right:
                    folderContextMenuStrip.Show(Cursor.Position);
                    break;
            }
        }

        private static DirectoryInfo CreateNewFolder(string path)
        {
            const string name = "New Folder";
            string current = name;
            int i = 0;
            while (Directory.Exists(Path.Combine(path, current))) {
                i++;
                current = $"{name} {i}";
            }
            return Directory.CreateDirectory(Path.Combine(path, current));
        }

        private void newFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dirInfo = (DirectoryInfo) projectTreeView.SelectedNode.Tag;
            try
            {
                var newDir = CreateNewFolder(dirInfo.FullName);
                var childDirectoryNode = new TreeNode(newDir.Name)
                {
                    Tag = newDir,
                    ImageKey = GetAssociatedIcon(newDir.FullName)
                };
                projectTreeView.SelectedNode.Nodes.Add(childDirectoryNode);
                projectTreeView.SelectedNode.Expand();
                projectTreeView.Sort();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }

        private void deleteFolderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var dirInfo = (DirectoryInfo)projectTreeView.SelectedNode.Tag;
            if (projectTreeView.SelectedNode.Parent == null)
            {
                MessageBox.Show("Cannot delete the root content folder!", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (dirInfo.EnumerateFiles().Any() || dirInfo.EnumerateDirectories().Any())
                if (MessageBox.Show("Warning: Deleting this folder will delete all of its content! Are you sure you want to proceed?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) != DialogResult.Yes)
                    return;

            try
            {
                dirInfo.Delete(true);
                DisplayCurrentFolderContent(projectTreeView.SelectedNode.Parent);
                projectTreeView.Nodes.Remove(projectTreeView.SelectedNode);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
        }
    }
}
