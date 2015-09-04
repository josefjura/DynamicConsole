namespace DynamicConsole.Tests
{
    using global::DynamicConsole.Commands.Input;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public class CommandLineParseTests
    {
        [TestMethod]
        public void Positive_ReadEmpty()
        {
            var line = "";

            var result = CommandInput.Parse(line);

            Assert.IsNotNull(result);
            Assert.AreEqual(string.Empty, result.Keyword);
            Assert.IsNotNull(result.Parameters);
            Assert.AreEqual(0, result.Parameters.Count);
        }

        [TestMethod]
        public void Positive_ReadOnlyKeyword()
        {
            var keyword = "keyword";

            var result = CommandInput.Parse(keyword);

            Assert.IsNotNull(result);
            Assert.AreEqual(keyword, result.Keyword);
            Assert.IsNotNull(result.Parameters);
            Assert.AreEqual(0, result.Parameters.Count);
        }

        [TestMethod]
        public void Positive_ReadSingleIndexParameter()
        {
            var keyword = "keyword";
            var parameterValue = "value";
            var line = $"{keyword} {parameterValue}";

            var result = CommandInput.Parse(line);

            Assert.IsNotNull(result);
            Assert.AreEqual(keyword, result.Keyword);
            Assert.IsNotNull(result.Parameters);
            Assert.AreEqual(1, result.Parameters.Count);
            Assert.IsFalse(result.Parameters[0].IsNamed);
            Assert.AreEqual(parameterValue, result.Parameters[0].Value);
        }

        [TestMethod]
        public void Positive_ReadMoreIndexParameters()
        {
            var keyword = "keyword";
            var parameter1Value = "value1";
            var parameter2Value = "value1";
            var parameter3Value = "value1";
            var line = $"{keyword} {parameter1Value} {parameter2Value} {parameter3Value}";

            var result = CommandInput.Parse(line);

            Assert.IsNotNull(result);
            Assert.AreEqual(keyword, result.Keyword);
            Assert.IsNotNull(result.Parameters);
            Assert.AreEqual(3, result.Parameters.Count);

            Assert.IsFalse(result.Parameters[0].IsNamed);
            Assert.AreEqual(parameter1Value, result.Parameters[0].Value);

            Assert.IsFalse(result.Parameters[1].IsNamed);
            Assert.AreEqual(parameter2Value, result.Parameters[1].Value);

            Assert.IsFalse(result.Parameters[2].IsNamed);
            Assert.AreEqual(parameter3Value, result.Parameters[2].Value);
        }

        [TestMethod]
        public void Positive_ReadSingleNamedParameter()
        {
            var keyword = "keyword";
            var parameterName = "name";
            var parameterValue = "value";
            var line = $"{keyword} -{parameterName}:{parameterValue}";

            var result = CommandInput.Parse(line);

            Assert.IsNotNull(result);
            Assert.AreEqual(keyword, result.Keyword);
            Assert.IsNotNull(result.Parameters);
            Assert.AreEqual(1, result.Parameters.Count);
            Assert.IsTrue(result.Parameters[0].IsNamed);
            Assert.AreEqual(parameterName, result.Parameters[0].Name);
            Assert.AreEqual(parameterValue, result.Parameters[0].Value);
        }

        [TestMethod]
        public void Positive_ReadMoreNamedParameters()
        {
            var keyword = "keyword";
            var parameter1Name = "name";
            var parameter1Value = "value";
            var parameter2Name = "name";
            var parameter2Value = "value";
            var parameter3Name = "name";
            var parameter3Value = "value";
            var line =
                $"{keyword} -{parameter1Name}:{parameter1Value} -{parameter2Name}:{parameter2Value} -{parameter3Name}:{parameter3Value} ";

            var result = CommandInput.Parse(line);

            Assert.IsNotNull(result);
            Assert.AreEqual(keyword, result.Keyword);
            Assert.IsNotNull(result.Parameters);
            Assert.AreEqual(3, result.Parameters.Count);
            Assert.IsTrue(result.Parameters[0].IsNamed);
            Assert.AreEqual(parameter1Name, result.Parameters[0].Name);
            Assert.AreEqual(parameter1Value, result.Parameters[0].Value);
            Assert.IsTrue(result.Parameters[1].IsNamed);
            Assert.AreEqual(parameter2Name, result.Parameters[1].Name);
            Assert.AreEqual(parameter2Value, result.Parameters[1].Value);
            Assert.IsTrue(result.Parameters[2].IsNamed);
            Assert.AreEqual(parameter3Name, result.Parameters[2].Name);
            Assert.AreEqual(parameter3Value, result.Parameters[2].Value);
        }

        [TestMethod]
        public void Positive_ReadSingleSwitchParameters()
        {
            var keyword = "keyword";
            var parameter1Name = "name";
            var line = $"{keyword} -{parameter1Name}";

            var result = CommandInput.Parse(line);

            Assert.IsNotNull(result);
            Assert.AreEqual(keyword, result.Keyword);
            Assert.IsNotNull(result.Parameters);
            Assert.AreEqual(1, result.Parameters.Count);

            Assert.IsTrue(result.Parameters[0].IsNamed);
            Assert.AreEqual(null, result.Parameters[0].Value);

        }

        [TestMethod]
        public void Positive_ReadManySwitchParameters()
        {
            var keyword = "keyword";
            var parameter1Name = "name1";
            var parameter2Name = "name2";
            var line = $"{keyword} -{parameter1Name} -{parameter2Name}";

            var result = CommandInput.Parse(line);

            Assert.IsNotNull(result);
            Assert.AreEqual(keyword, result.Keyword);
            Assert.IsNotNull(result.Parameters);
            Assert.AreEqual(2, result.Parameters.Count);

            Assert.IsTrue(result.Parameters[0].IsNamed);
            Assert.AreEqual(null, result.Parameters[0].Value);

            Assert.IsTrue(result.Parameters[1].IsNamed);
            Assert.AreEqual(null, result.Parameters[1].Value);
        }


        [TestMethod]
        public void Positive_ReadMixedParametersVar1()
        {
            var keyword = "keywordValue";
            var parameter1Value = "value1";
            var parameter2Name = "name2";
            var parameter2Value = "value2";
            var parameter3Name = "name3";
            var line = $"{keyword} {parameter1Value} -{parameter2Name}:{parameter2Value} -{parameter3Name}";

            var result = CommandInput.Parse(line);

            Assert.IsNotNull(result);
            Assert.AreEqual(keyword, result.Keyword);
            Assert.IsNotNull(result.Parameters);
            Assert.AreEqual(3, result.Parameters.Count);

            Assert.IsFalse(result.Parameters[0].IsNamed);
            Assert.AreEqual(parameter1Value, result.Parameters[0].Value);

            Assert.IsTrue(result.Parameters[1].IsNamed);
            Assert.AreEqual(parameter2Name, result.Parameters[1].Name);
            Assert.AreEqual(parameter2Value, result.Parameters[1].Value);

            Assert.IsTrue(result.Parameters[2].IsNamed);
            Assert.AreEqual(parameter3Name, result.Parameters[2].Name);
            Assert.AreEqual(null, result.Parameters[2].Value);
        }

        [TestMethod]
        public void Positive_ReadMixedParametersVar2()
        {
            var keyword = "keyword";
            var parameter1Value = "value";
            var parameter2Name = "name";
            var parameter2Value = "value";
            var parameter3Name = "name";
            var line = $"{keyword} {parameter1Value} -{parameter3Name} -{parameter2Name}:{parameter2Value}";

            var result = CommandInput.Parse(line);

            Assert.IsNotNull(result);
            Assert.AreEqual(keyword, result.Keyword);
            Assert.IsNotNull(result.Parameters);
            Assert.AreEqual(3, result.Parameters.Count);

            Assert.IsFalse(result.Parameters[0].IsNamed);
            Assert.AreEqual(parameter1Value, result.Parameters[0].Value);

            Assert.IsTrue(result.Parameters[2].IsNamed);
            Assert.AreEqual(parameter2Name, result.Parameters[2].Name);
            Assert.AreEqual(parameter2Value, result.Parameters[2].Value);

            Assert.IsTrue(result.Parameters[1].IsNamed);
            Assert.AreEqual(parameter3Name, result.Parameters[1].Name);
            Assert.AreEqual(null, result.Parameters[1].Value);
        }

    }
}