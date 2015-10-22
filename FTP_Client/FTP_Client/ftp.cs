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
        private string start_dir = @"C:\";
        private string host = null;
        private string user = null;
        private string password = null;
        private FtpWebRequest ftp_request = null;
        private FtpWebResponse ftp_response = null;
        private Stream ftp_stream = null;
        private int buffer_size = 2048;

        public ftp (string host_IP, string user_name, string pass_word) { host = host_IP; user = user_name; password = pass_word; }

        public ftp()
        {

            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ftp ftp_client = new ftp("ftp://127.0.0.1", "user", "password");
            host = ftp_client.host;
            user = ftp_client.user;
            password = ftp_client.password;

            FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://127.0.0.1/");

            request.Method = WebRequestMethods.Ftp.ListDirectory;

            request.Credentials = new NetworkCredential(ftp_client.user, ftp_client.password);
            request.UsePassive = true;
            request.UseBinary = true;
            request.KeepAlive = false;

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            LocalListDirectory(local_tv, start_dir);
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


        private void Upload (string remote_file, string local_file)
        {
            //try
            //{
            //    ftp_request = (FtpWebRequest)FtpWebRequest.Create(host + '/' + remote_file);

            //    ftp_request.Credentials = new NetworkCredential(user, password);
            //    ftp_request.UseBinary = true;
            //    ftp_request.UsePassive = true;
            //    ftp_request.KeepAlive = true;

            //    ftp_request.Method = WebRequestMethods.Ftp.UploadFile;

            //    ftp_stream = ftp_request.GetRequestStream();

            //    FileStream local_file_stream = new FileStream(local_file, FileMode.Create, FileAccess.ReadWrite);

            //    byte[] byte_buffer = new byte[buffer_size];
            //    int bytes_sent = local_file_stream.Read(byte_buffer, 0, buffer_size);

            //    try
            //    {
            //        while (bytes_sent != 0)
            //        {
            //            ftp_stream.Write(byte_buffer, 0, bytes_sent);
            //            bytes_sent = local_file_stream.Read(byte_buffer, 0, buffer_size);
            //        }
            //    }
            //    catch (Exception e)
            //    {
            //        MessageBox.Show(e.ToString());
            //    }

            //    local_file_stream.Close();
            //    ftp_stream.Close();
            //    ftp_request = null;
            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e.ToString());

            //}

            // Get the object used to communicate with the server.
            FtpWebRequest ftp_request = (FtpWebRequest)WebRequest.Create(host + "/" + remote_file);
            ftp_request.Method = WebRequestMethods.Ftp.UploadFile;

            // This example assumes the FTP site uses anonymous logon.
            ftp_request.Credentials = new NetworkCredential(user, password);

            // Copy the contents of the file to the request stream.
            StreamReader sourceStream = new StreamReader(local_file);
            byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            sourceStream.Close();
            ftp_request.ContentLength = fileContents.Length;

            Stream requestStream = ftp_request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();

            FtpWebResponse response = (FtpWebResponse)ftp_request.GetResponse();

            Console.WriteLine("Upload File Complete, status {0}", response.StatusDescription);

            response.Close();
        }


        private void connect_btn_Click(object sender, EventArgs e)
        { 
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

        private void upload_btn_Click(object sender, EventArgs e)
        {
            local_site_txtbx.Text = (local_tv.SelectedNode.FullPath.ToString());
            remote_site_txtbx.Text = remote_tv.SelectedNode.FullPath.ToString();

            string new_file_name = GetFileName(local_site_txtbx.Text);
            string remote_file = remote_site_txtbx.Text + "\\" + new_file_name;
            string local_file = local_site_txtbx.Text;

            Upload(remote_file, local_file);
        }


        //LoginToServer
        //CreateFileOnServer
        //ReadFileFromServer "display"
        //UpdateFileOnServer
        //DeleteFileOnServer



    }
}
