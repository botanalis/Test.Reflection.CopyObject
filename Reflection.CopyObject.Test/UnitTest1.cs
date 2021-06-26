using NUnit.Framework;
using Reflection.CopyObject.Test.Data;

namespace Reflection.CopyObject.Test
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var tempClass = TemplateClass.GetTestClass();
            var dataClass = TestClass.GetTestClass();
            var myRelfection = new MyReflectionObject();
            var resultClass = myRelfection.CopyObject(dataClass, tempClass);
            
            Assert.Pass();
        }
    }
}