using DataXML;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace DataXML;

public class PersonXmlService : ICrudService
{
    private readonly string xmlFilePath;

    public PersonXmlService()
    {
        xmlFilePath = Constant.FolderPath;
        var directory = Path.GetDirectoryName(Constant.FolderPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }
    }

    public void Save(List<Person> people)
    {
        var serializer = new XmlSerializer(typeof(List<Person>));
        using (var writer = new StreamWriter(xmlFilePath))
        {
            serializer.Serialize(writer, people);
        }
    }
    public List<Person> Load()
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
