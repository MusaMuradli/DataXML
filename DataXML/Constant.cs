namespace DataXML
{
    public static class Constant
    {
        static Constant()
        {
            FolderPath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot","persons.xml");
            
        }
        public static string FolderPath = "";
    }
}
