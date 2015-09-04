﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DynamicConsole.Tests
{
    using System.Collections.Generic;

    using global::DynamicConsole.Commands.Attributes;
    using global::DynamicConsole.Commands.Errors;
    using global::DynamicConsole.Commands.Input;
    using global::DynamicConsole.Commands.Signatures;

    [TestClass]
    public class TypeSignatureTests
    {
        [TestMethod]
        public void SingleIndexParameter()
        {
            var value = "TestValueParam1";

            var sigNameIndex = new TypedSignature<SingleIndexParameterSignature>(
                "test",
                "TestComand",
                (data, errors) => data.Param1 == value);

            TypeSignatureAssert.LineRuns(true, sigNameIndex, $"test {value}");
            TypeSignatureAssert.LineRuns(true, sigNameIndex, $"test -param1 {value}");
            TypeSignatureAssert.LineRuns(false, sigNameIndex, $"test");
        }

        [TestMethod]
        public void SingleNamedParameter()
        {
            var value = "TestValueParam1";

            var sigName = new TypedSignature<SingleNameParameterSignature>(
                "test",
                "TestComand",
                (data, errors) => true);

            TypeSignatureAssert.LineRuns(true, sigName, $"test");
            TypeSignatureAssert.LineRuns(true, sigName, $"test -param1 {value}");
            // Can run as the param1 parameter is not mandatory, but should crash because of the unidentified parameters
            TypeSignatureAssert.LineRuns(false, sigName, $"test {value}");
        }

        [TestMethod]
        public void SingleTokenParameter()
        {
            var sigName = new TypedSignature<SingleTokenParameterSignature>(
                "test",
                "TestComand",
                (data, errors) => true);

            TypeSignatureAssert.LineRuns(true, sigName, $"test param1");
            TypeSignatureAssert.LineRuns(false, sigName, $"test");
            TypeSignatureAssert.LineRuns(false, sigName, $"test notParam1");
        }
    }
}
