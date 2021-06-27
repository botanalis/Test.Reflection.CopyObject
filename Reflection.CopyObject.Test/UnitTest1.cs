using System.Collections.Generic;
using NUnit.Framework;
using Reflection.CopyObject.Test.Data;

namespace Reflection.CopyObject.Test
{
    public class Tests
    {
        private MyReflectionObject _myRelfection;
        [SetUp]
        public void Setup()
        {
            _myRelfection = new MyReflectionObject();
        }

        [Test]
        public void Test1()
        {
            var tempClass = TemplateClass.GetTestClass();
            var dataClass = TestClass.GetTestClass();
            
            var resultClass = _myRelfection.CopyObject(dataClass, tempClass);
            
            Assert.Pass();
        }

        [Test]
        public void Test2()
        {
            var class1 = TemplateClass.GetTestClass();
            class1.FieldStr = "123";
           
            var class2 =  TemplateClass.GetTestClass();
            class2.FieldStr = "222";

            List<DiffContent> result = _myRelfection.GetDiffContents(class1, class2);
            
            Assert.Pass();
        }
    }
}