using System.IO;
using SimpleJSON;
using UnityEngine;

public class JsonProvider
{
    public JSONNode JsonNode { get; private set; }
    private string _path;

    public JsonProvider(string pathToFile, string fileName)
    {
        LoadJsonNode(pathToFile, fileName);
    }

    private void LoadJsonNode(string pathToFile, string fileName)
    {
        //  Debug.Log($"TEST: {pathToFile}");
        // Debug.Log($"TEST: {fileName}");
        _path = Path.Combine(pathToFile, fileName);
        // Debug.Log($"TEST: {_path}");
        ReadJsonFile();
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