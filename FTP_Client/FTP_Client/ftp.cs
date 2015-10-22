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
    public partial class ftp : Form
    {
        private string host = null;
        private string user = null;
        private string password = null;

        public ftp (string host_IP, string user_name, string pass_word) { host = host_IP; user = user_name; password = pass_word; }

        public ftp()
        {

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ftp ftp_client = new ftp("ftp://127.0.0.1", "user", "password");

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://127.0.0.1/");

            request.Method = WebRequestMethods.Ftp.ListDirectory;

            request.Credentials = new NetworkCredential(ftp_client.user, ftp_client.password);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            LocalListDirectory(local_tv, @"C:\Users\Jess\Documents\Repos");
            RemoteListDirectory(remote_tv);
        }


        private void LocalListDirectory(TreeView tree_view, string path)
        {
            tree_view.Nodes.Clear();

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

            tree_view.Nodes.Add(node);
        }

        private void RemoteListDirectory (TreeView tree_view)
        {
            string path = "ftp://127.0.0.1";
            tree_view.Nodes.Clear();

            tree_view.Nodes.Add(RecursiveDirectory(path, "/"));
            
        }

        private FtpWebRequest StreamCreater (string path)
        {
            ftp ftp_client = new ftp("ftp://127.0.0.1", "user", "password");

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path);

            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            request.Credentials = new NetworkCredential(ftp_client.user, ftp_client.password);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            return request;
        }
        private List<Tuple<bool,string>> DirectoryOrFile (string path)
        {
            var request = StreamCreater(path);

            int header_size = 49;
            List<Tuple<bool, string>> result_list = new List<Tuple<bool, string>>();

            using (var response = request.GetResponse())
            {
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    while (!reader.EndOfStream)
                    {
                        string just_name = null;
                        bool dir = false;

                        string results = (reader.ReadLine());

                        char[] parsed_results = results.ToCharArray();

                        if (parsed_results[0] == 'd')
                        {
                            dir = true;
                        }

                        for (int i = header_size; i < parsed_results.Length; i++)
                        {
                            just_name += parsed_results[i];
                        }

                        Tuple<bool, string> finished = new Tuple<bool, string>(dir, just_name);

                        result_list.Add(finished);
                    }
                }
            }
            return result_list;
        }

        private TreeNode RecursiveDirectory (string path, string name)
        {
            var directory_node = new TreeNode(name);
            string directory = null;
            string file = null;
            var directory_results = DirectoryOrFile(path);

            foreach (var tuple in directory_results)
            {
                if (tuple.Item1 == true)
                {
                    directory = tuple.Item2;
                    directory_node.Nodes.Add(RecursiveDirectory((path + '/' + directory), directory));
                }
                else
                {
                    file = tuple.Item2;
                    directory_node.Nodes.Add(new TreeNode(file));
                }
            }



            return directory_node;
        }

        private void connect_btn_Click(object sender, EventArgs e)
        { 
        }


        //LoginToServer
        //CreateFileOnServer
        //ReadFileFromServer "display"
        //UpdateFileOnServer
        //DeleteFileOnServer

        

    }
}
