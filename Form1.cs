using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Syncronize
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

            List<Job> job = new List<Job>();
            GetList(textBox1.Text, textBox2.Text, job, includeSubDirectoriesCheckBox.Checked);
            syncAllFilesFirstTime(job);
        }
        public void syncAllFilesFirstTime(List<Job> files)
        {
           
           
            Job job = new Job();

            


            foreach (Job file in files)
            {
                
                string [] arrFiles = Directory.GetFiles((file.Source));
                foreach (string sourceFiles in arrFiles)
                {
                    string strFileName = Path.GetFileName(sourceFiles);
                    string strDesFilePath = string.Format(@"{0}\{1}", file.Destination, strFileName);

                    if (!Directory.Exists(file.Source))
                    {
                        Directory.CreateDirectory(file.Destination);
                    }
                    if (!File.Exists(strDesFilePath))
                        File.Copy(sourceFiles, strDesFilePath, true);

                }
             }

        }
        public static void GetList(string SourcePath, List<string> files, Boolean includeSubDirectories = true)
        {

            if (includeSubDirectories)
            {
                string[] DirectoryList = Directory.GetDirectories(SourcePath);

                foreach (string directory in DirectoryList)
                    if (directory != "")
                    {
                       // Directory.CreateDirectory(SourcePath);
                        GetList(directory, files, includeSubDirectories);
                       /*
                    }

                string[] FileList = Directory.GetFiles(SourcePath);
                foreach (string file in FileList)

                {

                    System.IO.FileInfo fileInfo = new System.IO.FileInfo(file);

                    string fileName = fileInfo.FullName;


                    long byteBoyut = fileInfo.Length / 1024;
                    string size = byteBoyut.ToString("#,##0");
                    string createDate = fileInfo.CreationTime.ToString();
                    string modifiedDate = fileInfo.LastWriteTime.ToString();
                    string relativePath = fileInfo.FullName.Substring(SourcePath.Length);*/




                    Job job = new Job();
                    job.Source = SourcePath;
                    job.Destination = DestinationPath;
                    files.Add(job);
                }

            }
            else
            {
                string[] FileList = Directory.GetFiles(SourcePath);
                Job job = new Job();
                job.Source = SourcePath;
                job.Destination = DestinationPath;
                files.Add(job);
            }
        }
    }
}
