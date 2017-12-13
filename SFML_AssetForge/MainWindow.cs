using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SFML_AssetForge
{
    public partial class MainWindow : Form
    {

        private string _packagesFile;
        private IEnumerable<string> _packageResources;
        private List<string> _packageExtensions = new List<string>
        {
            ".pkg",
            ".cfg",
            ".ini"
        };

        public MainWindow()
        {
            InitializeComponent();
        }

        private void MainWindow_Load(object sender, EventArgs e)
        {
            imageList1.Images.Add("dir", DefaultIcons.FolderSmall);
            SetRootDirectory(AppDomain.CurrentDomain.BaseDirectory, _packageExtensions);
        }

        private void SetRootDirectory(string root, IEnumerable<string> filters = null)
        {
            if(!File.GetAttributes(root).HasFlag(FileAttributes.Directory)) throw new DirectoryNotFoundException("Path is not a directory!");
            treeView1.BeginUpdate();
            treeView1.Nodes.Clear();

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

            treeView1.Nodes.Add(node);
            treeView1.EndUpdate();
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

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            pictureBox1.Image = imageList1.Images[1];
            Popup.ShowPopup(this, "About", "Created by Kevin Kuegler\nVersion 1.00");
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void createToolStripMenuItem_Click(object sender, EventArgs e)
        {

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

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {

        }



        #region Useless Shit

        public static class DefaultIcons
        {
            private static readonly Lazy<Icon> LazyFolderIcon = new Lazy<Icon>(FetchIcon, true);

            public static Icon FolderSmall => LazyFolderIcon.Value;

            private static Icon FetchIcon()
            {
                var tmpDir = Directory.CreateDirectory(Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString())).FullName;
                var icon = ExtractFromPath(tmpDir);
                Directory.Delete(tmpDir);
                return icon;
            }

            private static Icon ExtractFromPath(string path)
            {
                SHFILEINFO shinfo = new SHFILEINFO();
                SHGetFileInfo(
                    path,
                    0, ref shinfo, (uint)Marshal.SizeOf(shinfo),
                    SHGFI_ICON | SHGFI_SMALLICON);
                return Icon.FromHandle(shinfo.hIcon);
            }

            //Struct used by SHGetFileInfo function
            [StructLayout(LayoutKind.Sequential)]
            private struct SHFILEINFO
            {
                public IntPtr hIcon;
                public int iIcon;
                public uint dwAttributes;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
                public string szDisplayName;
                [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
                public string szTypeName;
            };

            [DllImport("shell32.dll")]
            private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

            private const uint SHGFI_ICON = 0x100;
            private const uint SHGFI_SMALLICON = 0x000000001;
        }

        #endregion

    }
}