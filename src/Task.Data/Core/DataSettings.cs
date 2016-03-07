namespace Task.Data.Core
{
    public class DataSettings
    {
        public Dapper Dapper { get; set; }
    }

    public class Dapper
    {
        public string ConnectionString { get; set; }
    }
}
