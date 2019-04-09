using System.IO;

namespace BackupTool.FileProcess
{
    class FileHelper
    {
        private FileHelper _instance;
        public FileHelper Instance
        {
            get
            {
                if(_instance != null)
                {
                    return _instance;
                }
                else
                {
                    _instance = new FileHelper();
                    return _instance;
                }
            }
        }

        public void CopyOneFile(string src, string dest)
        {
            File.Copy(src, dest, true);
        }

        public void CopyFolder(string srcDir, string destDir)
        {
            if (!Directory.Exists(destDir))
            {
                Directory.CreateDirectory(destDir);
            }

            string[] files = Directory.GetFiles(srcDir);
            foreach(string file in files)
            {
                string fileName = file.Substring(srcDir.Length + 1);
                File.Copy(Path.Combine(srcDir, fileName), Path.Combine(destDir,fileName), true);
            }
        }
    }
}