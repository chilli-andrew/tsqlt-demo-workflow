namespace tSQLt.Helpers
{
    public class TestResult
    {
        public int Id { get; set; }
        public string Class { get; set; }
        public string TestCase { get; set; }
        public string Name { get; set; }
        public string TranName { get; set; }
        public string Result { get; set; }
        public string Msg { get; set; }

        public TestResult(int id, string @class, string testCase, string name,string tranName,string result,string msg)
        {
            Id = id;
            Class = @class;
            TestCase = testCase;
            Name = name;
            TranName = tranName;
            Result = result;
            Msg = msg;
        }
    }
}