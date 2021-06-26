using System.Collections.Generic;

namespace Reflection.CopyObject.Test.Data
{

    public class TestClass
    {
        public static TestClass1 GetTestClass()
        {
            return new TestClass1()
            {
                FieldStr = "Class1",
                FieldInt = 1,
                FieldList = new List<TestClass2>()
                {
                    new TestClass2()
                    {
                        FieldStr2 = "Class1-2",
                        FieldInt2 = 12,
                        FieldList2 = new List<TestClass3>()
                        {
                            new TestClass3()
                            {
                                FieldStr3 = "Class1-3-1",
                                FieldInt3 = 11
                            },
                            new TestClass3()
                            {
                                FieldStr3 = "Class1-3-2",
                                FieldInt3 = 12
                            }
                        }
                    },
                    new TestClass2()
                    {
                        FieldStr2 = "Class2-2",
                        FieldInt2 = 22,
                    }
                }
            };
        }
    }

    public class TestClass1
    {
        public string FieldStr { get; set; }
        public int FieldInt { get; set; }
        public bool FieldBool { get; set; }

        public List<TestClass2> FieldList { get; set; }

        public TestClass1()
        {
            this.FieldList = new List<TestClass2>();
        }
    }

    public class TestClass2
    {
        public string FieldStr2 { get; set; }
        public int FieldInt2 { get; set; }
        public bool FieldBool2 { get; set; }
        public List<TestClass3> FieldList2 { get; set; }
        public TestClass2()
        {
            this.FieldList2 = new List<TestClass3>();
        }
    }
    
    public class TestClass3
    {
        public string FieldStr3 { get; set; }
        public int FieldInt3 { get; set; }
        public bool FieldBool3 { get; set; }
    }
}