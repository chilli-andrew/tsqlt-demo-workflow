namespace tSQLt.Helpers
{
    public class tSQLtTest
    {
        public int SchemaId { get; set; }
        public string TestClassName { get; set; }
        public int ObjectId { get; set; }
        public string Name { get; set; }

        public tSQLtTest(int schemaId, string testClassName, int objectId, string name)
        {
            SchemaId = schemaId;
            TestClassName = testClassName;
            ObjectId = objectId;
            Name = name;
        }

        public string GetTestMethodName()
        {
            var testName = Name;
            //var testName = TestClassName + Name;
            return testName.Replace(" ", "_").Replace("-", "_"); 
        }
        
        public string GetFullName()
        {
            return string.Format("[{0}].[{1}]", TestClassName, Name);
        }
    }
}