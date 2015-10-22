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
using Microsoft.VisualBasic;


namespace FTP_Client
{
    public partial class ftp : Form
    {
        private string start_dir = @"C:\";
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
            LocalListDirectory(local_tv, start_dir);
        }

        private void connect_btn_Click(object sender, EventArgs e)
        {
            ftp ftp_client = new ftp("ftp://" + host_txtbx.Text, username_txtbx.Text, password_txtbx.Text);

            host = ftp_client.host;
            user = ftp_client.user;
            password = ftp_client.password;

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(host);

            request.Method = WebRequestMethods.Ftp.ListDirectory;

            request.Credentials = new NetworkCredential(ftp_client.user, ftp_client.password);

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            RemoteListDirectory(remote_tv);
        }

        private void LocalListDirectory(TreeView tree_view, string path)
        {
            tree_view.Nodes.Clear();

            DirectoryInfo root_directory_info = new DirectoryInfo(path);

            tree_view.Nodes.Add(CreateDirectoryNode(root_directory_info));
        }

        private TreeNode CreateDirectoryNode(DirectoryInfo directory_info)
        {
            TreeNode directory_node = new TreeNode(directory_info.Name);

            try
            {
                foreach (var directory in directory_info.GetDirectories())
                {
                    directory_node.Nodes.Add(CreateDirectoryNode(directory));
                }
                foreach (var file in directory_info.GetFiles())
                {
                    directory_node.Nodes.Add(new TreeNode(file.Name));
                }
                return directory_node;
            }
            catch (UnauthorizedAccessException)
            {
                return new TreeNode("Unavailable Node");
            }
            catch (PathTooLongException)
            {
                return new TreeNode("Unavailable Node");
            }
        }

        private void RemoteListDirectory (TreeView tree_view)
        {
            string path = host;

            tree_view.Nodes.Clear();

            tree_view.Nodes.Add(RecursiveDirectory(path, "/"));
            
        }

        private FtpWebRequest StreamCreater (string path)
        {
            ftp ftp_client = new ftp(host, user, password);

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path);

            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            request.Credentials = new NetworkCredential(ftp_client.user, ftp_client.password);

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


        private void Upload (string remote_file, string local_file)
        {
            try
            {
                // Get the object used to communicate with the server.
                FtpWebRequest ftp_request = (FtpWebRequest)WebRequest.Create(host + "/" + remote_file);
                ftp_request.Method = WebRequestMethods.Ftp.UploadFile;

                // This example assumes the FTP site uses anonymous logon.
                ftp_request.Credentials = new NetworkCredential(user, password);

                ftp_request.UseBinary = true;


                byte[] byte_stream = File.ReadAllBytes(local_file);
                ftp_request.ContentLength = byte_stream.Length;
                using (Stream s = ftp_request.GetRequestStream())
                {
                    s.Write(byte_stream, 0, byte_stream.Length);
                }

                FtpWebResponse response = (FtpWebResponse)ftp_request.GetResponse();

                response.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());

            }
        }




        private string GetFileName (string local_file)
        {
            string file_name = null;
            char[] just_name = local_file.ToCharArray();

            for (int i = (local_file.Length - 1); i > 0; i--)
            {
                if (just_name[i - 1] == '\\')
                {
                    while (i != local_file.Length)
                    {
                        file_name += just_name[i];
                        i++;
                    }
                    break;
                }
            }

            return file_name;

        }

        private void Delete(string remote_file)
        {
            try
            {
                FtpWebRequest ftp_request = (FtpWebRequest)WebRequest.Create(host + "/" + remote_file);
                ftp_request.Method = WebRequestMethods.Ftp.DeleteFile;

                ftp_request.Credentials = new NetworkCredential(user, password);

                FtpWebResponse response = (FtpWebResponse)ftp_request.GetResponse();

                response.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());

            }

        }

        private void Rename(string remote_file, string new_file_name)
        {
            try
            {
                FtpWebRequest ftp_request = (FtpWebRequest)WebRequest.Create(host + "/" + remote_file);
                ftp_request.Method = WebRequestMethods.Ftp.Rename;

                ftp_request.Credentials = new NetworkCredential(user, password);

                ftp_request.RenameTo = new_file_name;

                FtpWebResponse response = (FtpWebResponse)ftp_request.GetResponse();

                response.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
        }

        private void upload_btn_Click(object sender, EventArgs e)
        {
            local_site_txtbx.Text = (local_tv.SelectedNode.FullPath.ToString());
            remote_site_txtbx.Text = remote_tv.SelectedNode.FullPath.ToString();

            string new_file_name = GetFileName(local_site_txtbx.Text);
            string remote_file = remote_site_txtbx.Text + "\\" + new_file_name;
            string local_file = local_site_txtbx.Text;

            Upload(remote_file, local_file);

            RemoteListDirectory(remote_tv);
        }

        private void delete_btn_Click(object sender, EventArgs e)
        {
            remote_site_txtbx.Text = remote_tv.SelectedNode.FullPath.ToString();

            Delete(remote_site_txtbx.Text);

            RemoteListDirectory(remote_tv);

        }

        private void rename_btn_Click(object sender, EventArgs e)
        {
            remote_site_txtbx.Text = remote_tv.SelectedNode.FullPath.ToString();


            string new_file_name = Microsoft.VisualBasic.Interaction.InputBox("Enter new file/directory name", "Rename", "", -1, -1);


            Rename(remote_site_txtbx.Text, new_file_name);

            RemoteListDirectory(remote_tv);

        }

        private void remote_tv_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                remote_site_txtbx.Text = remote_tv.SelectedNode.FullPath.ToString();
            }
            catch (Exception)
            { }
        }

        private void local_tv_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                local_site_txtbx.Text = (local_tv.SelectedNode.FullPath.ToString());
            }
            catch (Exception)
            { }
        }

      }
}
