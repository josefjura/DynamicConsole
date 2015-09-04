namespace DynamicConsole.Tests
{
    using System;

    using global::DynamicConsole.Commands.Input;
    using global::DynamicConsole.Commands.Signatures;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class TypeSignaturePartTests
    {
        [TestMethod]
        public void IntegerTest()
        {
            var tsp = new TypeSignaturePart("test", TypeCode.Int32);
            var result = false;

            var positiveStringTest = new Parameter { Value = "1234" };
            result = tsp.CanParse(positiveStringTest);
            Assert.IsTrue(result);

            var negativeStringTest = new Parameter { Value = "x" };
            result = tsp.CanParse(negativeStringTest);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DecimalTest()
        {
            var tsp = new TypeSignaturePart("test", TypeCode.Decimal);
            var result = false;

            var positiveStringTest = new Parameter { Value = "12.3" };
            result = tsp.CanParse(positiveStringTest);
            Assert.IsTrue(result);

            var negativeStringTest = new Parameter { Value = "x" };
            result = tsp.CanParse(negativeStringTest);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void DateTimeTest()
        {
            var tsp = new TypeSignaturePart("test", TypeCode.DateTime);
            var result = false;

            var positiveStringTest = new Parameter { Value = "01/01/15" };
            result = tsp.CanParse(positiveStringTest);
            Assert.IsTrue(result);

            var negativeStringTest = new Parameter { Value = "x" };
            result = tsp.CanParse(negativeStringTest);
            Assert.IsFalse(result);
        }
    }
}