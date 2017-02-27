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
                var filePath = directory + GetFileNameWithTimeStamp(fileName, fileExtension);
                File.WriteAllBytes(filePath, data);
                return filePath;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string GetFileNameWithTimeStamp(string fileName, string fileExtension, bool isDirectory = false)
        {
            var file = string.Format(@"\{0}_", string.IsNullOrEmpty(fileName) ? "ArgonDataFile" : fileName);
            file = string.Format("{0}{1}_{2}", file, DateTime.Now.ToString("yyyyMMMMdd"), DateTime.Now.ToString("HH.mm.ss.ffffff"));
            return isDirectory ? file : string.Format("{0}.{1}", file, string.IsNullOrEmpty(fileExtension) ? "txt" : fileExtension);
        }
    }
}
