using DataXML;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

public class PersonService
{
    private readonly string xmlFilePath;

    public PersonService(string filePath)
    {
        xmlFilePath = filePath;
        var directory = Path.GetDirectoryName(filePath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    public void SaveToXml(List<Person> people)
    {
        var serializer = new XmlSerializer(typeof(List<Person>));
        using (var writer = new StreamWriter(xmlFilePath))
        {
            serializer.Serialize(writer, people);
        }
    }
    public List<Person> LoadFromXml()
    {
        if (!File.Exists(xmlFilePath))
        {
            return new List<Person>();
        }

        var serializer = new XmlSerializer(typeof(List<Person>));
        using (var reader = new StreamReader(xmlFilePath))
        {
            return (List<Person>)serializer.Deserialize(reader);
        }
    }
}
