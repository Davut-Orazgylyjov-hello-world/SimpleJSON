using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OrazgylyjovFuteres.SimpleJSON
{
    public static class JSONReader
    {
        public static string[] GetAllFilesInDirectoryNames(string folderPath, string exceptionFile)
        {
            var dir = new DirectoryInfo(Path.Combine(Application.streamingAssetsPath, folderPath));
            FileInfo[] filesInDirectory = dir.GetFiles("*.*");
            var filesName = new List<string>();
            foreach (var file in filesInDirectory)
            {
                if (!file.Name.Contains(exceptionFile))
                {
                    Debug.Log(file.Name);
                    filesName.Add(file.Name);
                }
            }

            return filesName.ToArray();
        }

        public static string GetJsonData(string folderPath, string fileName)
        {
            byte[] fileBytes = GetFileBytes(CombineToStreamingAssetsPath(fileName, folderPath));
            return System.Text.Encoding.Default.GetString(fileBytes);
        }

        private static string CombineToStreamingAssetsPath(string fileName, string folderPath = "")
        {
            return Path.Combine(Application.streamingAssetsPath, folderPath, fileName);
        }

        private static byte[] GetFileBytes(string filePath)
        {
            return File.ReadAllBytes(filePath);
        }
    }
}