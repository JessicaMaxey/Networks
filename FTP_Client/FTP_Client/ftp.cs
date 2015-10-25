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
        //The starting directory for the local side
        private string true_start_dir = @"C:\";
        private string start_dir = @"C:\";

        //various data members
        private string host = null;
        private string user = null;
        private string password = null;

        //ctor
        public ftp (string host_IP, string user_name, string pass_word)
        {
            host = host_IP; user = user_name; password = pass_word;
        }

        public ftp()
        {
            InitializeComponent();

        }


        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                true_start_dir = Microsoft.VisualBasic.Interaction.InputBox("Enter new file/directory name\n Start directory is C: by default.", "Rename", "", -1, -1);

                if (true_start_dir == null || true_start_dir == "" || true_start_dir == "C:")
                {
                    true_start_dir = "C:\\";
                    MessageBox.Show("Program is gathering directories and files, please wait. (press ok to continue)");

                    //gathers the starting directory location and fills in the treeview with data
                    LocalListDirectory(local_tv, true_start_dir);

                }
                else
                {

                    MessageBox.Show("Program is gathering directories and files, please wait. (press ok to continue)");

                    //gathers the starting directory location and fills in the treeview with data
                    LocalListDirectory(local_tv, true_start_dir);

                    start_dir = FixStartingDirectory(true_start_dir);


                }

                MessageBox.Show("Instructions: Double click the file or directory you would like to use before clicking on an operation(upload, download, etc.)\nUpload – This will up load a directory / subdirectories / files to the server. \nDelete – This will delete a single item on the server, you must have no files under the directory for you to be able to delete the directory. \nRename – This will rename an item on the server. \nDownload – This will download a single file from the server to the specified directory on the client.It will not download directories.");



            }
            catch (Exception p)
            {
                MessageBox.Show("Something went wrong when creating local treeview. " + p.ToString());
            }
        }

        private string FixStartingDirectory (string start_dir)
        {
            string new_dir_string = null;
            char[] just_name = start_dir.ToCharArray();
            int j = 0;

            for (int i = (start_dir.Length - 1); i > 0; i--)
            {
                //goes backwards and finds where the name begins
                if (just_name[i - 1] == '\\')
                {
                    while (j != i )
                    {
                        //goes forwards and adds name to the end
                        new_dir_string += just_name[j];
                        j++;
                    }
                    break;
                }
            }


            return new_dir_string;
        }

        private void connect_btn_Click(object sender, EventArgs e)
        {
            try
            {
                //creates the ftp client
                ftp ftp_client = new ftp("ftp://" + host_txtbx.Text, username_txtbx.Text, password_txtbx.Text);

                //sets the data members so that they can be used in the rest of the program
                host = ftp_client.host;
                user = ftp_client.user;
                password = ftp_client.password;

                //creates ftp request
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(host);

                //Specify the type of ftp request
                request.Method = WebRequestMethods.Ftp.ListDirectory;

                //login to the ftp server with username and password 
                request.Credentials = new NetworkCredential(ftp_client.user, ftp_client.password);

                //establish return communication with ftp server
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();

                //creates the treeview for the server
                RemoteListDirectory(remote_tv);
            }
            catch(Exception p)
            {
                MessageBox.Show("Something went wrong when trying to connect, " + p.ToString());
            }
        }

        //recursively creates nodes for the treeview
        private void LocalListDirectory(TreeView tree_view, string path)
        {
            //resets the data that is already in the treeview
            tree_view.Nodes.Clear();

            //gets directory info based on the path passed in
            DirectoryInfo root_directory_info = new DirectoryInfo(path);

            //recursively creates the nodes 
            tree_view.Nodes.Add(CreateDirectoryNode(root_directory_info));
        }

        //creates nodes to populate tree view for client "local site"
        private TreeNode CreateDirectoryNode(DirectoryInfo directory_info)
        {
            //creates the node
            TreeNode directory_node = new TreeNode(directory_info.Name);

            //recursively goes through all the directories and files
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

        //creates treeview for server "remote site"
        private void RemoteListDirectory (TreeView tree_view)
        {
            //sets the starting point for the ftp server's home directory
            string path = host;

            //resets the data that is already in the treeview
            tree_view.Nodes.Clear();

            //recursively creates the nodes 
            tree_view.Nodes.Add(RecursiveDirectory(path, "/"));
            
        }

        //creates ftp request that can be use again and again for the recursive function directory or file
        private FtpWebRequest StreamCreater (string path)
        {
            //creates the ftp client
            ftp ftp_client = new ftp(host, user, password);

            //creates ftp request
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(path);

            //Specify the type of ftp request
            request.Method = WebRequestMethods.Ftp.ListDirectoryDetails;

            //login to the ftp server with username and password 
            request.Credentials = new NetworkCredential(ftp_client.user, ftp_client.password);

            return request;
        }

        //to decide if the path is that of a file or a directory on the server 
        private List<Tuple<bool,string>> DirectoryOrFile (string path)
        {
            //creates the ftp stream connection with the server
            var request = StreamCreater(path);

            int header_size = 49;

            //store the list of results for files and directories
            List<Tuple<bool, string>> result_list = new List<Tuple<bool, string>>();

            //establish return communication with ftp server
            using (var response = request.GetResponse())
            {
                //get the ftp server's response stream
                using (var reader = new StreamReader(response.GetResponseStream()))
                {
                    //loop until at the end of stream
                    while (!reader.EndOfStream)
                    {
                        string just_name = null;
                        bool dir = false;

                        //gets name
                        string results = (reader.ReadLine());

                        //create array so it will be easy to check if directory
                        char[] parsed_results = results.ToCharArray();

                        //if the first char is a d, then it is a directory
                        if (parsed_results[0] == 'd')
                        {
                            dir = true;
                        }

                        //backtracks through the array to store just the name
                        for (int i = header_size; i < parsed_results.Length; i++)
                        {
                            just_name += parsed_results[i];
                        }

                        //add to list, storing both the name and if it is a dir or not
                        Tuple<bool, string> finished = new Tuple<bool, string>(dir, just_name);

                        result_list.Add(finished);
                    }
                }
            }
            return result_list;
        }

        //popluates the treeview for the server "remote site"
        private TreeNode RecursiveDirectory (string path, string name)
        {
            var directory_node = new TreeNode(name);
            string directory = null;
            string file = null;

            //checks to see if the path contains a file or dir
            var directory_results = DirectoryOrFile(path);


            foreach (var tuple in directory_results)
            {
                if (tuple.Item1 == true)
                {
                    //if dir, then loop through this function again until file
                    directory = tuple.Item2;
                    directory_node.Nodes.Add(RecursiveDirectory((path + '/' + directory), directory));
                }
                else
                {
                    //if file, then add to the treeview
                    file = tuple.Item2;
                    directory_node.Nodes.Add(new TreeNode(file));
                }
            }

            return directory_node;
        }

        //creates dir and files recursivly on the server from the client
        private void RecursiveServerDirectory(string local_path, string remote_path)
        {

            //looks at local directory and determines if file or dir
            try
            {
                string[] files = Directory.GetFiles(local_path, "*.*");
                string[] subDirs = Directory.GetDirectories(local_path);
                CreateDirectory(remote_path);


                foreach (string subDir in subDirs)
                {
                    //if dir, create a new dir folder on server and loop through again until file
                    CreateDirectory(remote_path + "/" + Path.GetFileName(subDir));
                    RecursiveServerDirectory(subDir, remote_path + "/" + Path.GetFileName(subDir));
                }

                foreach (string file in files)
                {
                    //if file, then go ahead and upload to server
                    Upload(remote_path + "/" + Path.GetFileName(file), file);
                }
            }
            catch (Exception)
            {
                Upload(remote_path, local_path);
            }

        }

        //uploads files to server
        public void Upload(string remote_file, string local_file)
        {
            try
            {
                //creates the ftp client
                FtpWebRequest ftp_request = (FtpWebRequest)WebRequest.Create(host + "/" + remote_file);

                //Specify the type of ftp request
                ftp_request.Method = WebRequestMethods.Ftp.UploadFile;

                //login to the ftp server with username and password 
                ftp_request.Credentials = new NetworkCredential(user, password);

                ftp_request.UseBinary = true;

                //begin upload process
                byte[] byte_stream = File.ReadAllBytes(local_file);
                ftp_request.ContentLength = byte_stream.Length;


                using (Stream s = ftp_request.GetRequestStream())
                {
                    s.Write(byte_stream, 0, byte_stream.Length);
                }

                //establish return communication with ftp server
                FtpWebResponse response = (FtpWebResponse)ftp_request.GetResponse();

                response.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong while trying to upload file to server. " + e.ToString());

            }
        }

        //creates a new dir file on the server of the same name as the one on the client
        public void CreateDirectory(string new_directory)
        {
            try
            {
                //creates the ftp client
                FtpWebRequest ftp_request = (FtpWebRequest)WebRequest.Create(host + "/" + new_directory);
                //login to the ftp server with username and password
                ftp_request.Credentials = new NetworkCredential(user, password);

                //sets various options for the ftp request
                ftp_request.UseBinary = true;
                ftp_request.UsePassive = true;
                ftp_request.KeepAlive = true;

                //Specify the type of ftp request
                ftp_request.Method = WebRequestMethods.Ftp.MakeDirectory;

                //establish return communication with ftp server
                FtpWebResponse ftp_response = (FtpWebResponse)ftp_request.GetResponse();

                ftp_response.Close();
                ftp_request = null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Something went wrong creating a new directory on server, " + e.ToString());
            }
            return;
        }


        //Gets the files name so when it is uploaded to the server it has the same name
        private string GetFileName (string local_file)
        {
            string file_name = null;
            char[] just_name = local_file.ToCharArray();

            for (int i = (local_file.Length - 1); i > 0; i--)
            {
                //goes backwards and finds where the name begins
                if (just_name[i - 1] == '\\')
                {
                    while (i != local_file.Length)
                    {
                        //goes forwards and adds name to the end
                        file_name += just_name[i];
                        i++;
                    }
                    break;
                }
            }

            return file_name;

        }

        //for deleting file on server
        private void Delete(string remote_file)
        {
            try
            {
                //creates the ftp client
                FtpWebRequest ftp_request = (FtpWebRequest)WebRequest.Create(host + "/" + remote_file);
                //Specify the type of ftp request
                ftp_request.Method = WebRequestMethods.Ftp.DeleteFile;

                //login to the ftp server with username and password 
                ftp_request.Credentials = new NetworkCredential(user, password);

                //establish return communication with ftp server
                FtpWebResponse response = (FtpWebResponse)ftp_request.GetResponse();

                response.Close();
            }
            catch (Exception e)
            {
                try
                {
                    //creates the ftp client
                    FtpWebRequest ftp_request = (FtpWebRequest)WebRequest.Create(host + "/" + remote_file);
                    //Specify the type of ftp request
                    ftp_request.Method = WebRequestMethods.Ftp.RemoveDirectory;

                    //login to the ftp server with username and password 
                    ftp_request.Credentials = new NetworkCredential(user, password);

                    //establish return communication with ftp server
                    FtpWebResponse response = (FtpWebResponse)ftp_request.GetResponse();

                }
                catch (Exception p)
                {
                    MessageBox.Show("Something went wrong when trying to delete file from server. " + p.ToString());
                }
            }

        }



        //for renaming a file that is on the server
        private void Rename(string remote_file, string new_file_name)
        {
            try
            {
                //creates the ftp client
                FtpWebRequest ftp_request = (FtpWebRequest)WebRequest.Create(host + "/" + remote_file);
                //Specify the type of ftp request
                ftp_request.Method = WebRequestMethods.Ftp.Rename;

                //login to the ftp server with username and password
                ftp_request.Credentials = new NetworkCredential(user, password);

                //sets what the new name of the file will be
                ftp_request.RenameTo = new_file_name;

                //establish return communication with ftp server
                FtpWebResponse response = (FtpWebResponse)ftp_request.GetResponse();

                response.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong while trying to rename file on server" + e.ToString());
            }
        }


        private void upload_server_btn_Click(object sender, EventArgs e)
        {
            try
            {
                string local_file = null;

                //gets the directory path from the text box that was populated by the user double clicking on
                //what file/dir they wanted in the tree view
                //from the local treeview
                local_site_txtbx.Text = (local_tv.SelectedNode.FullPath.ToString());
                //from the remote treeview
                remote_site_txtbx.Text = remote_tv.SelectedNode.FullPath.ToString();



                //gets the name of the file from the client that is to be upload to server
                string new_file_name = GetFileName(local_site_txtbx.Text);
                //appends the name of the file to the end of the server location
                string remote_file = remote_site_txtbx.Text + "\\" + new_file_name;
                //easier variable to work with
                if (start_dir == "C:\\")
                {
                    local_file = local_site_txtbx.Text;
                }
                else
                {
                    local_file = start_dir + local_site_txtbx.Text;
                }
                //creates the files and directories on the server
                RecursiveServerDirectory(local_file, remote_file);

                //refreshs the treeview to reflect the new files/dir on server
                RemoteListDirectory(remote_tv);
            }
            catch (Exception)
            {
                try
                {
                    string local_file = null;

                    //gets the name of the file from the client that is to be upload to server
                    string new_file_name = GetFileName(local_site_txtbx.Text);
                    //appends the name of the file to the end of the server location
                    string remote_file = remote_site_txtbx.Text + "\\" + new_file_name;
                    //easier variable to work with
                    if (start_dir == "C:\\")
                    {
                        local_file = local_site_txtbx.Text;
                    }
                    else
                    {
                        local_file = start_dir + local_site_txtbx.Text;
                    }

                    //creates the files and directories on the server
                    RecursiveServerDirectory(local_file, remote_file);

                    //refreshs the treeview to reflect the new files/dir on server
                    RemoteListDirectory(remote_tv);
                }
                catch (Exception p)
                {
                    MessageBox.Show("Something went wrong when trying to upload file to server" + p.ToString());
                }
            }
        }
        
        private void delete_server_btn_Click(object sender, EventArgs e)
        {
            try
            {
                //gets the directory path from the text box that was populated by the user double clicking on
                //what file/dir they wanted in the tree view
                //from the remote treeview
                remote_site_txtbx.Text = remote_tv.SelectedNode.FullPath.ToString();

                //deletes the specfied file from server
                Delete(remote_site_txtbx.Text);

                //refreshs the treeview to reflect the new files/dir on server
                RemoteListDirectory(remote_tv);
            }
            catch (Exception)
            {
                try
                {
                    //deletes the specfied file from server
                    Delete(remote_site_txtbx.Text);

                    //refreshs the treeview to reflect the new files/dir on server
                    RemoteListDirectory(remote_tv);
                }
                catch (Exception p)
                {
                    MessageBox.Show("Something went wrong when trying to delete file from server. " + p.ToString());
                }

            }

        }

        private void rename_server_btn_Click(object sender, EventArgs e)
        {
            try
            {
                //gets the directory path from the text box that was populated by the user double clicking on
                //what file/dir they wanted in the tree view
                remote_site_txtbx.Text = remote_tv.SelectedNode.FullPath.ToString();

                //gets input from user on what the new name of the file should be
                string new_file_name = Microsoft.VisualBasic.Interaction.InputBox("Enter new file/directory name", "Rename", "", -1, -1);

                //renames the file on the server with the specified new name
                Rename(remote_site_txtbx.Text, new_file_name);

                //refreshs the treeview to reflect the new files/dir on server
                RemoteListDirectory(remote_tv);
            }
            catch (Exception)
            {
                try
                {
                    //gets input from user on what the new name of the file should be
                    string new_file_name = Microsoft.VisualBasic.Interaction.InputBox("Enter new file/directory name", "Rename", "", -1, -1);

                    //renames the file on the server with the specified new name
                    Rename(remote_site_txtbx.Text, new_file_name);

                    //refreshs the treeview to reflect the new files/dir on server
                    RemoteListDirectory(remote_tv);
                }
                catch (Exception p)
                {
                    MessageBox.Show("Something went wrong while trying to rename file on server" + p.ToString());
                }
            }

        }

        //Downloading the file from the server to the client
        private void Download (string remote_file, string local_file)
        {
            try
            {
                int bytes_read = 0;
                byte[] buffer = new byte[2048];

                //creates the ftp client
                FtpWebRequest ftp_request = (FtpWebRequest)WebRequest.Create(host + "/" + remote_file);
                //Specify the type of ftp request
                ftp_request.Method = WebRequestMethods.Ftp.DownloadFile;

                //login to the ftp server with username and password
                ftp_request.Credentials = new NetworkCredential(user, password);

                ftp_request.UseBinary = true;

                //establish return communication with ftp server
                Stream reader = ftp_request.GetResponse().GetResponseStream();

                //open a file stream to read the file for download
                FileStream file_stream = new FileStream(local_file, FileMode.Create);

                //download the file by sending the buffered data until the download is complete
                while (true)
                {
                    //buffer for the download data
                    bytes_read = reader.Read(buffer, 0, buffer.Length);

                    if (bytes_read == 0)
                        break;

                    //writing data
                    file_stream.Write(buffer, 0, bytes_read);
                }

                file_stream.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong when downloading file from server." + e.ToString());

            }
        }


        private void upload_client_btn_Click(object sender, EventArgs e)
        {
            try
            {
                string local_file = null;

                //gets the directory path from the text box that was populated by the user double clicking on
                //what file/dir they wanted in the tree view
                local_site_txtbx.Text = (local_tv.SelectedNode.FullPath.ToString());
                remote_site_txtbx.Text = remote_tv.SelectedNode.FullPath.ToString();

                //gets the name of the file from the client that is to be upload to server
                string new_file_name = GetFileName(remote_site_txtbx.Text);

                //easier variable to work with
                string remote_file = remote_site_txtbx.Text;
                //appends the name of the file to the end of the client location
                if (start_dir == "C:\\")
                {
                    local_file = local_site_txtbx.Text + "\\" + new_file_name;
                }
                else
                {
                    local_file = start_dir + local_site_txtbx.Text + "\\" + new_file_name;
                }
         

                //downloads the file from the server to the client
                Download(remote_file, local_file);

                MessageBox.Show("Refreshing local treeview, may take a moment (press ok to continue.)");

                //refreshs the treeview to reflect the new files/dir on client
                LocalListDirectory(local_tv, true_start_dir);

                MessageBox.Show("Complete. Thank you for waiting.");
            }
            catch (Exception)
            {
                try
                {
                    string local_file = null;
                    //gets the name of the file from the client that is to be upload to server
                    string new_file_name = GetFileName(remote_site_txtbx.Text);

                    //easier variable to work with
                    string remote_file = remote_site_txtbx.Text;
                    //appends the name of the file to the end of the client location
                    if (start_dir == "C:\\")
                    {
                        local_file = local_site_txtbx.Text + "\\" + new_file_name;
                    }
                    else
                    {
                        local_file = start_dir + local_site_txtbx.Text + "\\" + new_file_name;
                    }

                    //downloads the file from the server to the client
                    Download(remote_file, local_file);

                    MessageBox.Show("Refreshing local treeview, may take a moment (press ok to continue.)");

                    //refreshs the treeview to reflect the new files/dir on client
                    LocalListDirectory(local_tv, true_start_dir);

                    MessageBox.Show("Complete. Thank you for waiting.");
                }
                catch (Exception p)
                {
                    MessageBox.Show("Something went wrong when downloading file from server." + p.ToString());
                }
            }

        }

        //handler for the double clicking the server treeview nodes
        private void remote_tv_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            try
            {
                remote_site_txtbx.Text = remote_tv.SelectedNode.FullPath.ToString();
            }
            catch (Exception)
            { }
        }

        //handler for the double clicking the client treeview nodes
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
