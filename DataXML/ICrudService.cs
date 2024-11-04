namespace DataXML
{
    public interface ICrudService
    {
        List<Person> Load();
        void Save(List<Person> people);
    }
}
