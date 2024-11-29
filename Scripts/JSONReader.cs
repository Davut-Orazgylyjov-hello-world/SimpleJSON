using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace OrazgylyjovFuteres.SimpleJSON
{
    public static class JSONReader
    {
        public static string[] GetAllFilesInDirectoryNames(string folderPath, string exceptionFile)
        {
            switch (FileSystemSettingsSO.CurrentFolderType)
            {
                case FolderType.StreamingAssets:
                    return GetAllFilesInDirectoryNamesByStreamingAssets(folderPath, exceptionFile);
                case FolderType.Resources:
                    return GetAllFilesInDirectoryNamesByResources(folderPath, exceptionFile);
                default:
                    Debug.LogError($"Files in directory {folderPath} Not founded");
                    return new string[] { };
            }
        }

        private static string[] GetAllFilesInDirectoryNamesByStreamingAssets(string folderPath, string exceptionFile)
        {
            var filesName = new List<string>();
            var dir = new DirectoryInfo(Path.Combine(Application.streamingAssetsPath, folderPath));
            if (!dir.Exists)
            {
                Debug.LogError($"Directory not found: {dir.FullName}");
                return filesName.ToArray();
            }

            FileInfo[] filesInDirectory = dir.GetFiles("*.*");
            foreach (var file in filesInDirectory)
            {
                if (!file.Name.Contains(exceptionFile))
                {
                    filesName.Add(file.Name);
                }
            }

            return filesName.ToArray();
        }


        private static string[] GetAllFilesInDirectoryNamesByResources(string folderPath, string exceptionFile)
        {
            var filesName = new List<string>();
            var resourceFiles = Resources.LoadAll<TextAsset>(folderPath);
            foreach (var resource in resourceFiles)
            {
                if (!resource.name.Contains(exceptionFile))
                {
                    filesName.Add(resource.name);
                }
            }

            return filesName.ToArray();
        }

        public static string GetJsonData(string folderPath, string fileName)
        {
            if (FileSystemSettingsSO.CurrentFolderType == FolderType.StreamingAssets)
            {
                string filePath = CombineToStreamingAssetsPath(fileName, folderPath);
                byte[] fileBytes = GetFileBytes(filePath);
                return System.Text.Encoding.Default.GetString(fileBytes);
            }

            if (FileSystemSettingsSO.CurrentFolderType == FolderType.Resources)
            {
                string fullPath = Path.Combine(folderPath, Path.GetFileNameWithoutExtension(fileName));
                TextAsset resource = Resources.Load<TextAsset>(fullPath);
                if (resource == null)
                {
                    Debug.LogError($"File not found in Resources: {fullPath}");
                    return string.Empty;
                }

                return resource.text;
            }

            return string.Empty;
        }

        private static string CombineToStreamingAssetsPath(string fileName, string folderPath = "")
        {
            return Path.Combine(Application.streamingAssetsPath, folderPath, fileName);
        }

        private static byte[] GetFileBytes(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Debug.LogError($"File not found: {filePath}");
                return Array.Empty<byte>();
            }

            return File.ReadAllBytes(filePath);
        }
    }
}