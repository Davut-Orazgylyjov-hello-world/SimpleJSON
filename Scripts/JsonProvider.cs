using System;
using System.IO;
using SimpleJSON;
using UnityEngine;

public class JsonProvider
{
    public JSONNode JsonNode { get; private set; }
    private string _path;

    public JsonProvider(string pathToFile, string fileName)
    {
        fileName = ConvertFileNameToJsonFormat(fileName);
        _path = Path.Combine(pathToFile, fileName);
        
        if (IsValidPath(pathToFile) && IsFileExistsCorrect(_path))
            ReadJsonFile();;
    }

    private string ConvertFileNameToJsonFormat(string fileName)
    {
        if (!fileName.EndsWith(".json"))
            return fileName + ".json";
        return fileName;
    }


    private static bool IsFileExistsCorrect(string path)
    {
        if(File.Exists(path))
            return true;

        Debug.LogError($"File path is incorrect: {path}");
        return false;
    }

    private bool IsValidPath(string path, bool allowRelativePaths = false)
    {
        bool isValid = true;

        try
        {
            string fullPath = Path.GetFullPath(path);

            if (allowRelativePaths)
            {
                isValid = Path.IsPathRooted(path);
            }
            else
            {
                string root = Path.GetPathRoot(path);
                isValid = string.IsNullOrEmpty(root.Trim(new char[] {'\\', '/'})) == false;
            }
        }
        catch (Exception exception)
        {
            Debug.LogError($"ERROR VALID PATH: {path} \t {exception}");
            return false;
        }

        return true;
    }

    public void ReadJsonFile()
    {
        string jsonStr = File.ReadAllText(_path);
        JsonNode = JSONNode.Parse(jsonStr);
    }

    public void WriteJsonFile(string jsonData)
    {
        Debug.Log("Сохранить файл");
        File.WriteAllText(_path, jsonData);
    }
}