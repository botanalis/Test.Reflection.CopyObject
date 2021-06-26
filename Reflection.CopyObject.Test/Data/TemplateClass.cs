using System.Collections.Generic;

namespace Reflection.CopyObject.Test.Data
{
    public class TemplateClass
    {
        public static TestClass1 GetTestClass()
        {
            return new TestClass1();
        }
        
        public class TestClass1
        {
            public string FieldStr { get; set; }
            // public int FieldInt { get; set; }
            // public bool FieldBool { get; set; }

            public List<TestClass2> FieldList { get; set; }

           
        }

        public class TestClass2
        {
            public string FieldStr2 { get; set; }
            // public int FieldInt2 { get; set; }
            // public bool FieldBool2 { get; set; }
            public List<TestClass3> FieldList2 { get; set; }
          
        }
    
        public class TestClass3
        {
            public string FieldStr3 { get; set; }
            // public int FieldInt3 { get; set; }
            // public bool FieldBool3 { get; set; }
        }
    }
}