using DataXML;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace DataXML;

public class PersonJsonService : ICrudService
{
    private readonly string jsonFilePath;

    public PersonJsonService()
    {
        jsonFilePath = Constant.FolderPath;

        var directory = Path.GetDirectoryName(Constant.FolderPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    public void Save(List<Person> people)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var jsonString = JsonSerializer.Serialize(people, options);
        File.WriteAllText(jsonFilePath, jsonString);
    }

    public List<Person> Load()
    {
        if (!File.Exists(jsonFilePath))
        {
            return new List<Person>();
        }

        var jsonString = File.ReadAllText(jsonFilePath);
        return JsonSerializer.Deserialize<List<Person>>(jsonString);
    }
}

