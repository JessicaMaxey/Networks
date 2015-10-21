using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;



namespace FTP_Client
{
    public partial class Form1 : Form
    {
        FtpWebRequest ftprequest;
        FtpWebResponse ftpresponse;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListDirectory(local_tv, @"C:\Users\Jessica\Documents\Repos");
        }


        private void ListDirectory(TreeView treeView, string path)
        {
            treeView = local_tv;
            treeView.Nodes.Clear();

            var stack = new Stack<TreeNode>();
            var rootDirectory = new DirectoryInfo(path);
            var node = new TreeNode(rootDirectory.Name) { Tag = rootDirectory };
            stack.Push(node);

            while (stack.Count > 0)
            {
                var currentNode = stack.Pop();
                var directoryInfo = (DirectoryInfo)currentNode.Tag;
                foreach (var directory in directoryInfo.GetDirectories())
                {
                    var childDirectoryNode = new TreeNode(directory.Name) { Tag = directory };
                    currentNode.Nodes.Add(childDirectoryNode);
                    stack.Push(childDirectoryNode);
                }
                foreach (var file in directoryInfo.GetFiles())
                    currentNode.Nodes.Add(new TreeNode(file.Name));
            }

            treeView.Nodes.Add(node);
        }

        //LoginToServer
        //CreateFileOnServer
        //ReadFileFromServer "display"
        //UpdateFileOnServer
        //DeleteFileOnServer


    }
}
