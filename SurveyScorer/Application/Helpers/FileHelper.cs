using System;
using System.IO;

namespace SurveyScorer.Application.Helpers
{
    public class FileHelper
    {
        public static string WriteToFile(byte[] data, string directory, string fileName = null, string fileExtension = null)
        {
            try
            {
                if (!Directory.Exists(directory))
                    throw new DirectoryNotFoundException(string.Format("Directory not found : {0}", directory));
                var filePath = GetFullPath(directory, GetFileNameWithTimeStamp(fileName, fileExtension));
                File.WriteAllBytes(filePath, data);
                return filePath;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string WriteToFile(string data, string directory, string fileName = null, string fileExtension = null)
        {
            try
            {
                if (!Directory.Exists(directory))
                    throw new DirectoryNotFoundException(string.Format("Directory not found : {0}", directory));
                var filePath = GetFullPath(directory, GetFileNameWithTimeStamp(fileName, fileExtension));
                File.AppendAllText(filePath, data);
                return filePath;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string GetFullPath(string directory, string filename)
        {
            return Path.Combine(directory, filename.TrimStart('/', '\\'));
        }

        public static string CreateDirectoryWithTimestamp(string root, string name)
        {
            var newDirName = GetFileNameWithTimeStamp(name, null, true);
            var fullPath = Path.Combine(root, newDirName.TrimStart('/', '\\'));
            Directory.CreateDirectory(fullPath);
            return fullPath;
        }

        public static string GetFileNameWithTimeStamp(string fileName, string fileExtension, bool isDirectory = false)
        {
            var file = string.Format(@"{0}_", string.IsNullOrEmpty(fileName) ? "Report" : fileName);
            file = string.Format("{0}{1}_{2}", file, DateTime.Now.ToString("yyyyMMdd"), DateTime.Now.ToString("HH.mm.ss.ffffff"));
            return isDirectory ? file : string.Format("{0}.{1}", file, string.IsNullOrEmpty(fileExtension) ? "txt" : fileExtension);
        }
    }
}
